using Beauty.Application.Modules.Line.Mapping;
using Beauty.Application.Modules.Line.Messaging;
using Beauty.Model.Line;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Common.Logger.Enum;
using Common.Logger.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Line.Implementation
{
    public class LineService : ServiceBase<CoreDbContext>, ILineService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogRepository _logRepository;

        public LineService(IHttpContextAccessor httpContextAccessor, ILogRepository logRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _logRepository = logRepository;
        }

        public async Task<CreateResponse> Create(CreateRequest request)
        {
            var response = new CreateResponse();
            try
            {
                if (DbContext.Lines.Any(x => x.Title == request.Entity.Title && !x.IsDeleted))
                {
                    response.Message = MessagingResource_fa.LineExist;
                    return response;
                }
                var model = DbContext.Lines.Add(request.Entity.ToCreateModel(_httpContextAccessor));
                DbContext.SaveChanges();

                _logRepository.CreateModificationLog(request.Entity, nameof(LineModel), TypeCode.Int16, ModificationTypeEnum.Create, nameof(Create));

                response.Data = model.Entity.Id;
                response.IsSuccess = true;
                response.Message = MessagingResource_fa.ServiceCreateSucceed;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;

                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());
                return response;
            }
        }

        public async Task<UpdateResponse> Update(UpdateRequest request)
        {
            var response = new UpdateResponse();
            try
            {
                if (!DbContext.Lines.Any(x => x.Id == request.Entity.Id))
                {
                    response.Message = MessagingResource_fa.LineNotFound;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                if (DbContext.Lines.Any(x => x.Id != request.Entity.Id && x.Title == request.Entity.Title && !x.IsDeleted))
                {
                    response.Message = MessagingResource_fa.LineExist;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                var line = DbContext.Lines.Find(request.Entity.Id).ToUpdateModel(request.Entity);
                DbContext.Entry(line).State = EntityState.Modified;
                DbContext.SaveChanges();
                _logRepository.CreateModificationLog(new { Id = line.Id, Title = line.Title }, nameof(LineModel), TypeCode.Int16, ModificationTypeEnum.Update, nameof(Update));

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.LineUpdateSucceed;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Error;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }

        public async Task<DeleteResponse> Delete(DeleteRequest request)
        {
            var response = new DeleteResponse();
            try
            {
                if (!DbContext.Lines.Any(x => x.Id == request.Id))
                {
                    response.Message = MessagingResource_fa.LineNotFound;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                var line = DbContext.Lines.Find(request.Id);
                line.ReverseDelete();
                DbContext.SaveChanges();
                _logRepository.CreateModificationLog(new { Id = line.Id, Title = line.Title }, nameof(LineModel), TypeCode.Int16, ModificationTypeEnum.Delete, nameof(Delete));

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.LineDeleteSucceed;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Error;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return response;
            }
        }

        public async Task<FindAllResponse> FindAll(FindAllRequest request)
        {
            var response = new FindAllResponse();
            try
            {
                response.Data = DbContext.Lines.Include(x => x.Services).AsTracking().ToFindAllViewModel();
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
