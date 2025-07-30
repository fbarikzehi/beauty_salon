using Beauty.Application.Modules.Sms.Mapping;
using Beauty.Application.Modules.Sms.Messaging;
using Beauty.Application.Modules.Sms.ViewModel;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Sms.Implementation
{
    public class SmsService : ServiceBase<CoreDbContext>, ISmsService
    {
        public async Task<SmsCreateResponse> Create(SmsCreateRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SmsUpdateResponse> Update(SmsUpdateRequest request)
        {
            var response = new SmsUpdateResponse();
            try
            {
                var entity = request.Entity;
                if (!DbContext.SmsMessages.Any(x => x.Id == entity.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.SmsMessageNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                if (DbContext.SmsMessages.Any(x => x.Id != entity.Id && x.Title == entity.Title && !x.IsDeleted))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.SmsMessageExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var smsMessage = DbContext.SmsMessages.Include(x => x.Parameters).Include(x => x.SendSchedules).FirstOrDefault(x => x.Id == entity.Id).ToUpdateModel(entity);
                DbContext.Entry(smsMessage).State = EntityState.Modified;
                DbContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.SmsMessageUpdateSucceed;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<SmsFindAllResponse> FindAll(SmsFindAllRequest request)
        {
            var response = new SmsFindAllResponse();
            try
            {
                response.Data = DbContext.SmsMessages.AsQueryable().AsTracking().ToFindAllViewModel();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<SmsFindByIdResponse> FindById(SmsFindByIdRequest request)
        {
            var response = new SmsFindByIdResponse();
            try
            {
                if (!DbContext.SmsMessages.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    return response;
                }
                response.Entity = DbContext.SmsMessages.Include(x => x.Parameters).Include(x => x.SendSchedules).FirstOrDefault(x => x.Id == request.Id).ToUpdateViewModel();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }
    }
}
