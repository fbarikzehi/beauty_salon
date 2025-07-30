using Beauty.Application.Modules.Service.Mapping;
using Beauty.Application.Modules.Service.Messaging;
using Beauty.Model.Service;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Common.Crosscutting.Utility;
using Common.Logger.Enum;
using Common.Logger.Repository;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Service.Implementation
{
    public class ServiceService : ServiceBase<CoreDbContext>, IServiceService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogRepository _logRepository;

        public ServiceService(IHttpContextAccessor httpContextAccessor, ILogRepository logRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _logRepository = logRepository;
        }

        public async Task<CreateResponse> Create(CreateRequest request)
        {
            var response = new CreateResponse();
            try
            {
                if (DbContext.Services.Any(x => x.Title == request.Entity.Title && !x.IsDeleted))
                {
                    response.Message = MessagingResource_fa.ServiceNameExist;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                if (request.Entity.DurationMinutes?.Length > 3)
                {
                    response.Message = MessagingResource_fa.ServiceDurationIsLong;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                if (request.Entity.Price?.Length < request.Entity.Prepayment?.Length)
                {
                    response.Message = MessagingResource_fa.ServicePrepaymentIsGreaterThanPrice;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                //if ((string.IsNullOrEmpty(request.Entity.Price) || request.Entity.Price?.ToFloatPrice() == 0) &&
                //    (request.Entity.Details == null || request.Entity.Details.Where(x => !string.IsNullOrEmpty(x.Title)).Count() == 0))
                //{
                //    response.Message = MessagingResource_fa.ServicePriceIsRequired;
                //    response.AlertType = ResponseAlertResource_en.Warning;
                //    return response;
                //}

                if (request.Entity.Price?.ToFloatPrice() > 0 && request.Entity.Details?.Where(x => !string.IsNullOrEmpty(x.Title)).Count() > 0)
                {
                    response.Message = MessagingResource_fa.ServicePriceAndDetailsConflict;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                if (request.Entity.LineId == 0)
                {
                    response.Message = MessagingResource_fa.LineIsRequired;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                var model = DbContext.Services.Add(request.Entity.ToCreateModel(_httpContextAccessor));
                DbContext.SaveChanges();

                _logRepository.CreateModificationLog(request.Entity, nameof(ServiceModel), TypeCode.Int16, ModificationTypeEnum.Create, nameof(Create));

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.ServiceCreateSucceed;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;

                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());
                return response;
            }
        }

        public async Task<UpdateResponse> Update(UpdateRequest request)
        {
            var response = new UpdateResponse();
            try
            {
                if (!DbContext.Services.Any(x => x.Id == request.Entity.Id))
                {
                    response.Message = MessagingResource_fa.ServiceNotFound;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                if (DbContext.Services.Any(x => x.Id != request.Entity.Id && x.Title == request.Entity.Title && !x.IsDeleted))
                {
                    response.Message = MessagingResource_fa.ServiceNameExist;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                if (request.Entity.DurationMinutes?.Length > 3)
                {
                    response.Message = MessagingResource_fa.ServiceDurationIsLong;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                if (request.Entity.Price?.Length < request.Entity.Prepayment?.Length)
                {
                    response.Message = MessagingResource_fa.ServicePrepaymentIsGreaterThanPrice;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                //if (request.Entity.Price?.ToFloatPrice() == 0 && (request.Entity.Details == null || request.Entity.Details?.Where(x => !string.IsNullOrEmpty(x.Title)).Count() == 0))
                //{
                //    response.Message = MessagingResource_fa.ServicePriceIsRequired;
                //    response.AlertType = ResponseAlertResource_en.Warning;
                //    return response;
                //}

                if (request.Entity.Price?.ToFloatPrice() > 0 && request.Entity.Details?.Where(x => !string.IsNullOrEmpty(x.Title)).Count() > 0)
                {
                    response.Message = MessagingResource_fa.ServicePriceAndDetailsConflict;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                if (request.Entity.LineId == 0)
                {
                    response.Message = MessagingResource_fa.LineIsRequired;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                var service = DbContext.Services.Include(x => x.Prices).Include(x => x.Details).Include(x => x.Line).FirstOrDefault(x => x.Id == request.Entity.Id).ToUpdateModel(request.Entity);
                DbContext.Entry(service).State = EntityState.Modified;
                DbContext.SaveChanges();
                _logRepository.CreateModificationLog(new { Id = service.Id, Title = service.Title }, nameof(ServiceModel), TypeCode.Int16, ModificationTypeEnum.Create, nameof(Update));

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.ServiceUpdateSucceed;
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
                if (!DbContext.Services.Any(x => x.Id == request.Id))
                {
                    response.Message = MessagingResource_fa.ServiceNotFound;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                var service = DbContext.Services.Find(request.Id);
                service.ReverseDelete();
                DbContext.SaveChanges();
                _logRepository.CreateModificationLog(new { Id = service.Id, Title = service.Title }, nameof(ServiceModel), TypeCode.Int16, ModificationTypeEnum.Create, nameof(Create));

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.ServiceDeleteSucceed;
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
                response.Data = DbContext.Services.Include(x => x.Line).AsQueryable().ToFindAllViewModel();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }

        public async Task<FindAllByPageResponse> FindAllByPage(FindAllByPageRequest request)
        {
            var response = new FindAllByPageResponse();
            try
            {
                var dbset = DbContext.Services.Include(x => x.ServiceRatings).Include(x => x.Prices).Include(x => x.Line).OrderByDescending(x => x.Id).AsQueryable();
                Expression<Func<ServiceModel, bool>> predicate = null;
                ////Title
                if (request.Search?.Value.Length > 0 && !string.IsNullOrEmpty(request.Search?.Value))
                {
                    predicate = service => service.Title.Contains(request.Search.Value);
                }

                var entities = predicate is null ?
                               dbset.Skip(request.Start).Take(request.Length) :
                               dbset.AsExpandable().Where(predicate).Skip(request.Start).Take(request.Length);

                response.Data = entities.ToDataTableViewModel();
                response.Draw = request.Draw;
                response.RecordsTotal = dbset.Count();
                response.RecordsFiltered = dbset.Count();

                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }

        public async Task<FindByIdResponse> FindById(FindByIdRequest request)
        {
            var response = new FindByIdResponse();
            try
            {
                response.Entity = DbContext.Services.Include(x => x.Prices).Include(x => x.Details).Include(x => x.Line).FirstOrDefault(x => x.Id == request.Id).ToFindByIdViewModel();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }

        public async Task<UpdateActiveResponse> UpdateActive(UpdateActiveRequest request)
        {
            var response = new UpdateActiveResponse();
            try
            {
                if (!DbContext.Services.Any(x => x.Id == request.Id))
                {
                    response.Message = MessagingResource_fa.ServiceNotFound;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                var service = DbContext.Services.Find(request.Id);
                service.ReverseActive();
                DbContext.SaveChanges();

                _logRepository.CreateModificationLog(new { Id = service.Id, Title = service.Title }, nameof(ServiceModel), TypeCode.Int16, ModificationTypeEnum.Create, nameof(UpdateActive));

                response.IsSuccess = true;
                response.Message = service.IsActive ? MessagingResource_fa.ServiceActiveSucceed :
                                                    MessagingResource_fa.ServiceDeactiveSucceed;
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

        public async Task<FindAllBySearchResponse> FindAllBySearch(FindAllBySearchRequest request)
        {
            var response = new FindAllBySearchResponse();
            try
            {
                var services = DbContext.Services.Include(x => x.Prices).Include(x => x.Line).Where(x => x.Title.Contains(request.Title)).Distinct().ToFindAllBySearchViewModel();

                response.Data = services;
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }

        public async Task<FindAllBySearchResponse> SearchAllByName(FindAllBySearchRequest request)
        {
            var response = new FindAllBySearchResponse();
            try
            {
                var services = DbContext.Services.Include(x => x.Prices).Where(x => x.Title.Contains(request.Title)).ToSearchAllByNameViewModel();

                response.Data = services;
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }

        public async Task<ServicePackageCreateResponse> CreateServicePackage(ServicePackageCreateRequest request)
        {
            var response = new ServicePackageCreateResponse();
            try
            {
                if (DbContext.ServicePackages.Any(x => x.Title == request.Entity.Title && !x.IsDeleted))
                {
                    response.Message = MessagingResource_fa.ServicePackageNameExist;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                if (request.Entity.Items?.Count(x => x.ServiceId > 0) == 0)
                {
                    response.Message = MessagingResource_fa.ServicePackageItemIsRequired;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                if (request.Entity.DiscountPrice?.ToFloatPrice() <= 0 && request.Entity.Items?.Where(x => !string.IsNullOrEmpty(x.AfterDiscountPrice)).Count() <= 0)
                {
                    response.Message = MessagingResource_fa.ServicePackageDiscountIsRequired;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                if (request.Entity.Items.Where(x => x.ServiceId > 0).GroupBy(x => x.ServiceId).Where(g => g.Count() > 1).Any())
                {
                    response.Message = MessagingResource_fa.ServicePackageItemDuplicates;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                //if (request.Entity.Items.Any(x=>x.AfterDiscountPrice.ToFloatPrice()>Service.Price))
                //{
                //    response.Message = MessagingResource_fa.AfterDiscountPriceMustBeLowrThanServicePrice;
                //    response.AlertType = ResponseAlertResource_en.Warning;
                //    return response;
                //}

                var model = DbContext.ServicePackages.Add(request.Entity.ToCreateModel(_httpContextAccessor));
                DbContext.SaveChanges();

                _logRepository.CreateModificationLog(request.Entity, nameof(ServicePackageModel), TypeCode.Int16, ModificationTypeEnum.Create, nameof(Create));

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.ServicePackageCreateSucceed;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;

                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());
                return response;
            }
        }

        public async Task<ServicePackageUpdateResponse> UpdateServicePackage(ServicePackageUpdateRequest request)
        {
            var response = new ServicePackageUpdateResponse();
            try
            {
                if (DbContext.ServicePackages.Any(x => x.Id != request.Entity.Id && x.Title == request.Entity.Title && !x.IsDeleted))
                {
                    response.Message = MessagingResource_fa.ServicePackageNameExist;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                if (request.Entity.Items?.Count(x => x.ServiceId > 0) == 0)
                {
                    response.Message = MessagingResource_fa.ServicePackageItemIsRequired;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                if (request.Entity.DiscountPrice?.ToFloatPrice() <= 0 && request.Entity.Items?.Where(x => !string.IsNullOrEmpty(x.AfterDiscountPrice)).Count() <= 0)
                {
                    response.Message = MessagingResource_fa.ServicePackageDiscountIsRequired;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }
                if (request.Entity.Items.Where(x => x.ServiceId > 0).GroupBy(x => x.ServiceId).Where(g => g.Count() > 1).Any())
                {
                    response.Message = MessagingResource_fa.ServicePackageItemDuplicates;
                    response.AlertType = ResponseAlertResource_en.Warning;
                    return response;
                }

                var model = DbContext.ServicePackages.Include(x => x.Items).FirstOrDefault(x => x.Id == request.Entity.Id).ToUpdateModel(request.Entity);
                DbContext.Entry(model).State = EntityState.Modified;
                DbContext.SaveChanges();

                _logRepository.CreateModificationLog(request.Entity, nameof(ServicePackageModel), TypeCode.Int16, ModificationTypeEnum.Update, nameof(UpdateServicePackage));

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.ServicePackageUpdateSucceed;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;

                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());
                return response;
            }
        }

        public async Task<ServicePackageDeleteResponse> ServicePackageDelete(ServicePackageDeleteRequest request)
        {
            var response = new ServicePackageDeleteResponse();
            try
            {
                var service = DbContext.ServicePackages.FirstOrDefault(x => x.Id == request.Id);

                if (service != null)
                {
                    service.ReverseDelete();
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.ServicePackageDeleteSucceed;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                response.IsSuccess = false;
                response.Message = MessagingResource_fa.ServicePackageDeleteFaild;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }

        public async Task<ServicePackageDeleteServiceResponse> ServicePackageDeleteService(ServicePackageDeleteServiceRequest request)
        {
            var response = new ServicePackageDeleteServiceResponse();
            try
            {
                var service = DbContext.ServicePackages.Include(x => x.Items).FirstOrDefault(x => x.Items.Any(y => y.Id == request.Id));

                if (service != null)
                {
                    service.Items.FirstOrDefault(x => x.Id == request.Id)?.ReverseDelete();
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.ServicePackageDeleteServiceSucceed;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                response.IsSuccess = false;
                response.Message = MessagingResource_fa.ServicePackageDeleteServiceFaild;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }

        public async Task<ServicePackageFindByPagingResponse> ServicePackageFindByPaging(ServicePackageFindByPagingRequest request)
        {
            var response = new ServicePackageFindByPagingResponse();
            try
            {
                var dbset = DbContext.ServicePackages.Include(x => x.Items).ThenInclude(x => x.Service).OrderByDescending(x => x.Id).AsQueryable();
                Expression<Func<ServicePackageModel, bool>> predicate = null;
                ////Title
                if (request.Search?.Value.Length > 0 && !string.IsNullOrEmpty(request.Search?.Value))
                {
                    predicate = servicePackage => servicePackage.Title.Contains(request.Search.Value);
                }

                var entities = predicate is null ?
                               dbset.Skip(request.Start).Take(request.Length) :
                               dbset.AsExpandable().Where(predicate).Skip(request.Start).Take(request.Length);

                response.Data = entities.ToListViewModel();
                response.Draw = request.Draw;
                response.RecordsTotal = dbset.Count();
                response.RecordsFiltered = dbset.Count();

                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }

        public async Task<ServicePackageFindByIdResponse> ServicePackageFindById(ServicePackageFindByIdRequest request)
        {
            var response = new ServicePackageFindByIdResponse();
            try
            {
                var servicePackage = DbContext.ServicePackages.Include(x => x.Items).ThenInclude(x => x.Service).ThenInclude(x => x.Prices).FirstOrDefault(x => x.Id == request.Id);

                response.Entity = servicePackage.ToFindByIdViewModel();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                _logRepository.CreateExceptionLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, new Exception());

                return response;
            }
        }


    }
}
