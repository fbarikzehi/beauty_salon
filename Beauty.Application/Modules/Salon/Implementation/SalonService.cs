using Beauty.Application.Modules.Salon.Mapping;
using Beauty.Application.Modules.Salon.Messaging;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Common.Application.File;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Salon.Implementation
{
    public class SalonService : ServiceBase<CoreDbContext>, ISalonService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SalonService(IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironmen)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironmen;
        }

        public async Task<FindResponse> Find(FindRequest request)
        {
            var response = new FindResponse();
            try
            {
                response.Entity = DbContext.Salons.Include(x => x.Contacts).FirstOrDefault().ToViewModel();
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

        public async Task<UpdateResponse> Update(UpdateRequest request)
        {
            var response = new UpdateResponse();
            try
            {
                if (DbContext.Salons.Any(x => x.Id == request.Entity.Id))
                {

                    var salon = DbContext.Salons.Include(x => x.Contacts).FirstOrDefault(x => x.Id == request.Entity.Id);

                    if (!request.Entity.Contacts.Any(x => x.Type == ContactTypeEnum.Phone) && !salon.Contacts.Any(x => x.Type == ContactTypeEnum.Phone))
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.PhoneIsRequired;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }

                    if (request.Entity.FileLogo != null)
                    {
                        var fileManager = new FileManager(request.Entity.FileLogo, _hostingEnvironment);
                        var result = fileManager.DeleteFromHost("salon-logo", salon.Logo);
                        if (result.IsSuccess)
                        {
                            result = fileManager.SaveToHost("salon-logo", null);
                            if (result.IsSuccess)
                                request.Entity.Logo = result.SavedFileName;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = result.Message;
                            response.AlertType = ResponseAlertResource_en.Danger;
                            return response;
                        }
                    }

                    salon = request.Entity.ToUpdateModel(salon);
                    DbContext.Entry(salon).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.SalonInfoUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }
                else
                {
                    DbContext.Salons.Add(request.Entity.ToCreateModel(_httpContextAccessor));
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.SalonInfoUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<UpdateResponse> UpdateOpeningAndClosingTime(UpdateRequest request)
        {
            var response = new UpdateResponse();
            try
            {
                if (DbContext.Salons.Any(x => x.Id == request.Entity.Id))
                {
                    var salon = DbContext.Salons.FirstOrDefault(x => x.Id == request.Entity.Id);

                    salon.OpeningTime = request.Entity.OpeningTime.StringToTimeSpan();
                    salon.ClosingTime = request.Entity.ClosingTime.StringToTimeSpan();
                    DbContext.Entry(salon).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.SalonInfoUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }
                else
                {
                    DbContext.Salons.Add(request.Entity.ToCreateModel(_httpContextAccessor));
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.SalonInfoUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<DeleteContactResponse> DeleteContact(DeleteContactRequest request)
        {
            var response = new DeleteContactResponse();
            try
            {
                if (DbContext.Salons.Any(x => x.Contacts.Any(y => y.Id == request.Id)))
                {

                    var entity = DbContext.Salons.Include(x => x.Contacts).FirstOrDefault(x => x.Contacts.Any(y => y.Id == request.Id));

                    entity.Contacts.FirstOrDefault(y => y.Id == request.Id).ReverseDelete();
                    DbContext.Entry(entity).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.SalonContactDeleteSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.SalonInfoUpdateFaild;
                    response.AlertType = ResponseAlertResource_en.Danger;
                }

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

        public async Task<UpdateWorkingDateTimesResponse> UpdateWorkingDateTimes(UpdateWorkingDateTimesRequest request)
        {
            var response = new UpdateWorkingDateTimesResponse();
            try
            {
                if (DbContext.Salons.Any(x => x.Id == request.Entity.Id))
                {

                    var salon = DbContext.Salons.Include(x => x.WorkingDateTimes).FirstOrDefault(x => x.Id == request.Entity.Id);

                   
                    DbContext.Entry(salon).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.SalonInfoUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }
                else
                {
                    DbContext.Salons.Add(request.Entity.ToCreateModel(_httpContextAccessor));
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.SalonInfoUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }

              
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
