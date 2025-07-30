using Beauty.Application.Modules.Personnel.Mapping;
using Beauty.Application.Modules.Personnel.Messaging;
using Beauty.Application.Modules.Personnel.ViewModel;
using Common.Crosscutting.Utility;
using Beauty.Model.Personnel;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Common.Application.File;
using Common.Crosscutting.Enum;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Beauty.Application.ExternalServices.Sms.Kavenegar;

namespace Beauty.Application.Modules.Personnel.Implementation
{
    public class PersonnelService : ServiceBase<CoreDbContext>, IPersonnelService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PersonnelService(IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironmen)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironmen;
        }

        public async Task<CreateResponse> Create(CreateRequest request)
        {
            var response = new CreateResponse();
            try
            {
                if (DbContext.Personnels.Any(x => x.Name == request.Entity.Name && x.LastName == request.Entity.LastName))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.PersonnelNameAndLastName;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var mobile = request.Entity.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile);
                if (mobile is null)
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.MobileIsRequired;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                foreach (var contact in request.Entity.Contacts)
                {
                    if (DbContext.Personnels.Any(x => x.Contacts.Any(c => c.Type == contact.Type && c.Value == contact.Value)))
                    {
                        response.IsSuccess = false;
                        response.Message = $"{MessagingResource_fa.PersonnelContactExist}: {contact.Value}";
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                }

                ushort code = RandomUtility.GetRandomUshort(ushort.MaxValue);
                while (DbContext.Personnels.Any(x => x.Code == code))
                    code = RandomUtility.GetRandomUshort(ushort.MaxValue);

                if (request.Entity.FileAvatar != null)
                {
                    var fileManager = new FileManager(request.Entity.FileAvatar, _hostingEnvironment);
                    var result = fileManager.SaveToHost("personnel-avatars", null);
                    if (result.IsSuccess)
                        request.Entity.Avatar = result.SavedFileName;
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = result.Message;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                }

                var personnel = DbContext.Personnels.Add(request.Entity.ToCreateModel(_httpContextAccessor, code));
                DbContext.SaveChanges();

                //Send Sms
                //var createSms = DbContext.SmsMessages.FirstOrDefault(x => x.SystemMessageType == SystemMessageTypeEnum.PersonnelCreate);
                //if (createSms != null && createSms.AllowSend)
                //{
                //    var parameters = new string[6]
                //    {
                //        personnel.Entity.Name,//0
                //        personnel.Entity.LastName,//1
                //        personnel.Entity.Code.ToString(),//2
                //        personnel.Entity.User.Username,//3
                //        personnel.Entity.User.Username,//4
                //        Environment.NewLine //5
                //    };
                //    var smsResult = SmsProvider.Send(string.Format(createSms.Text, parameters), mobile.Value);
                //    DbContext.SmsHistories.Add(new Model.Sms.SmsHistoryModel
                //    {
                //        MessageId = smsResult.Messageid,
                //        ReceptorNumber = smsResult.Receptor,
                //        SenderNumber = smsResult.Sender,
                //        Status = smsResult.Status,
                //        StatusText = smsResult.StatusText,
                //        Text = smsResult.Message,
                //        CreateUser = personnel.Entity.CreateUser,
                //    });
                //    DbContext.SaveChanges();
                //}

                response.Id = personnel.Entity.Id;
                response.Code = personnel.Entity.Code;
                response.IsSuccess = true;
                response.Message = MessagingResource_fa.PersonnelCreateSucceed;
                response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<DeleteContactResponse> DeleteContact(DeleteContactRequest request)
        {
            var response = new DeleteContactResponse();
            try
            {
                if (DbContext.Personnels.Any(x => x.Contacts.Any(y => y.Id == request.Id)))
                {

                    var entity = DbContext.Personnels.Include(x => x.Contacts).FirstOrDefault(x => x.Contacts.Any(y => y.Id == request.Id));

                    entity.Contacts.FirstOrDefault(y => y.Id == request.Id).ReverseDelete();
                    DbContext.Entry(entity).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.PersonnelContactDeleteSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.PersonnelUpdateFaild;
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

        public async Task<FindByIdModifyResponse> FindByIdModify(FindByIdModifyRequest request)
        {
            var response = new FindByIdModifyResponse();
            try
            {
                if (!DbContext.Personnels.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.PersonnelNameAndLastName;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                response.Entity = DbContext.Personnels.Include(x => x.Contacts).Include(x => x.User).FirstOrDefault(x => x.Id == request.Id).ToFindByIdModifyViewModel();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
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
                if (DbContext.Personnels.Any(x => x.Id == request.Entity.Id))
                {

                    var personnel = DbContext.Personnels.Include(x => x.Contacts).FirstOrDefault(x => x.Id == request.Entity.Id);

                    if (!request.Entity.Contacts.Any(x => x.Type == ContactTypeEnum.Mobile) && !personnel.Contacts.Any(x => x.Type == ContactTypeEnum.Mobile))
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.MobileIsRequired;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                    foreach (var contact in request.Entity.Contacts)
                    {
                        if (DbContext.Personnels.Any(x => x.Contacts.Any(c => c.Type == contact.Type && c.Value == contact.Value && x.Id != request.Entity.Id)))
                        {
                            response.IsSuccess = false;
                            response.Message = $"{MessagingResource_fa.PersonnelContactExist}: {contact.Value}";
                            response.AlertType = ResponseAlertResource_en.Danger;
                            return response;
                        }
                    }
                    if (!string.IsNullOrEmpty(personnel.Avatar) && request.Entity.FileAvatar != null)
                    {
                        var fileManager = new FileManager(request.Entity.FileAvatar, _hostingEnvironment);
                        var result = fileManager.DeleteFromHost("personnel-avatars", personnel.Avatar);
                        if (result.IsSuccess)
                        {
                            result = fileManager.SaveToHost("personnel-avatars", null);
                            if (result.IsSuccess)
                                request.Entity.Avatar = result.SavedFileName;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = result.Message;
                            response.AlertType = ResponseAlertResource_en.Danger;
                            return response;
                        }
                    }

                    personnel = request.Entity.ToUpdateModel(personnel, _httpContextAccessor);
                    DbContext.Entry(personnel).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.PersonnelUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.PersonnelUpdateFaild;
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

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request)
        {
            var response = new CreateAccountResponse();
            try
            {
                if (!DbContext.Personnels.Any(x => x.Id == request.PersonnelId))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.PersonnelMustCreateFirst;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var personnel = DbContext.Personnels.Include(x => x.User).FirstOrDefault(x => x.Id == request.PersonnelId);
                personnel.UserId = request.Id;
                DbContext.Entry(personnel).State = EntityState.Modified;
                DbContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.PersonnelAccountCreateSucceed;
                response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<FindByIdResponse> FindById(FindByIdRequest request)
        {
            var response = new FindByIdResponse();
            try
            {
                if (!DbContext.Personnels.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.PersonnelNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                response.Entity = DbContext.Personnels.Include(x => x.User)
                                                      .Include(x => x.Contacts)
                                                      .Include(x => x.AppointmentServices)
                                                      .Include(x => x.LinePersonnels)
                                                      .ThenInclude(x => x.Line).FirstOrDefault(x => x.Id == request.Id).ToFindByIdModel();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<FindAllUpdateServicesResponse> FindAllServices(FindAllUpdateServicesRequest request)
        {
            var response = new FindAllUpdateServicesResponse();
            try
            {
                if (!DbContext.Personnels.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.PersonnelNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var personnel = DbContext.Personnels.Include(x => x.Services).ThenInclude(x => x.Service).FirstOrDefault(p => p.Id == request.Id);

                var list = new List<PersonnelServiceItemsViewModel>();

                foreach (var service in personnel.Services)
                {
                    list.Add(new PersonnelServiceItemsViewModel
                    {
                        ServiceId = service.ServiceId,
                        ServiceTitle = service.Service?.Title,
                    });
                }

                response.Data = list;
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<UpdateServicesResponse> UpdateServices(UpdateServicesRequest request)
        {
            var response = new UpdateServicesResponse();
            try
            {
                if (DbContext.Personnels.Any(x => x.Id == request.Entity.PersonnelId))
                {

                    var personnel = DbContext.Personnels.Include(x => x.Services).FirstOrDefault(x => x.Id == request.Entity.PersonnelId);

                    PersonnelServiceModel personnelService = null;
                    foreach (var service in request.Entity.Services)
                    {
                        if (service.Selected && !personnel.Services.Any(x => x.ServiceId == service.ServiceId))
                        {
                            personnel.Services.Add(new PersonnelServiceModel { ServiceId = service.ServiceId });
                        }
                        else if (!service.Selected && personnel.Services.Any(x => x.ServiceId == service.ServiceId))
                        {
                            personnelService = personnel.Services.FirstOrDefault(x => x.ServiceId == service.ServiceId);
                            personnelService.ReverseDelete();
                            DbContext.Entry(personnelService).State = EntityState.Modified;
                        }
                    }
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.PersonnelServicesUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.PersonnelUpdateFaild;
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

        public async Task<UpdateServicesResponse> AddService(UpdateServicesRequest request)
        {
            var response = new UpdateServicesResponse();
            try
            {
                if (DbContext.Personnels.Any(x => x.Id == request.Entity.PersonnelId))
                {
                    var personnel = DbContext.Personnels.Include(x => x.Services).FirstOrDefault(x => x.Id == request.Entity.PersonnelId);
                    if (personnel.Services.Any(x => x.ServiceId == request.Entity.ServiceId))
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.PersonnelServiceAlreadyAdded;
                        response.AlertType = ResponseAlertResource_en.Warning;

                        return response;
                    }

                    personnel.Services.Add(new PersonnelServiceModel { ServiceId = request.Entity.ServiceId });
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.PersonnelServiceAddedSuccessfuly;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.PersonnelNotFound;
                    response.AlertType = ResponseAlertResource_en.Warning;
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

        public async Task<UpdateServicesResponse> DeleteService(UpdateServicesRequest request)
        {
            var response = new UpdateServicesResponse();
            try
            {
                if (DbContext.Personnels.Any(x => x.Id == request.Entity.PersonnelId))
                {
                    var personnel = DbContext.Personnels.Include(x => x.Services).FirstOrDefault(x => x.Id == request.Entity.PersonnelId);
                    if (!personnel.Services.Any(x => x.ServiceId == request.Entity.ServiceId))
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.PersonnelServiceNotFound;
                        response.AlertType = ResponseAlertResource_en.Warning;

                        return response;
                    }

                    var personnelService = personnel.Services.FirstOrDefault(x => x.ServiceId == request.Entity.ServiceId);
                    personnelService.ReverseDelete();
                    DbContext.Entry(personnelService).State = EntityState.Modified;

                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.PersonnelServiceDeletedSuccessfuly;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.PersonnelNotFound;
                    response.AlertType = ResponseAlertResource_en.Warning;
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

        public async Task<FindAllPercentageServicesResponse> FindAllPercentageServices(FindAllPercentageServicesRequest request)
        {
            var response = new FindAllPercentageServicesResponse();
            try
            {
                if (!DbContext.Personnels.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.PersonnelNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                response.Data = DbContext.Personnels.Include(x => x.Services).ThenInclude(x => x.Service).FirstOrDefault(p => p.Id == request.Id).Services.ToFindAllPercentageServiceModel();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<UpdateFinancialResponse> UpdateFinancial(UpdateFinancialRequest request)
        {
            var response = new UpdateFinancialResponse();
            try
            {
                if (DbContext.Personnels.Any(x => x.Id == request.Entity.PersonnelId))
                {

                    var entity = DbContext.Personnels.Include(x => x.Services).FirstOrDefault(x => x.Id == request.Entity.PersonnelId);

                    if (request.Entity.CooperationType == CooperationTypeEnum.Salary)
                    {
                        if (entity.CooperationType != CooperationTypeEnum.Salary)
                        {
                            entity.CooperationType = CooperationTypeEnum.Salary;
                            foreach (var personnelService in entity.Services)
                                personnelService.Percentage = null;
                        }
                        var salary = request.Entity.Salary.ToFloatPrice();
                        if (salary > 0)
                            entity.Salary = salary;
                    }
                    else if (request.Entity.CooperationType == CooperationTypeEnum.Percentage)
                    {
                        if (entity.CooperationType != CooperationTypeEnum.Percentage)
                        {
                            entity.CooperationType = CooperationTypeEnum.Percentage;
                            entity.Salary = 0;
                        }

                        foreach (var personnelService in entity.Services)
                            personnelService.Percentage = request.Entity.Services.FirstOrDefault(x => x.PersonnelServiceId == personnelService.Id).Percentage;
                    }
                    else
                    {
                        if (entity.CooperationType != CooperationTypeEnum.SalaryPercentage)
                            entity.CooperationType = CooperationTypeEnum.SalaryPercentage;

                        var salary = request.Entity.Salary.ToFloatPrice();
                        if (salary > 0)
                            entity.Salary = salary;

                        foreach (var personnelService in entity.Services)
                            personnelService.Percentage = request.Entity.Services.FirstOrDefault(x => x.PersonnelServiceId == personnelService.Id).Percentage;
                    }
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.PersonnelFinancialUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.PersonnelUpdateFaild;
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

        public async Task<FindAllByPagingResponse> FindAllByPaging(FindAllByPagingRequest request)
        {
            var response = new FindAllByPagingResponse();
            try
            {
                var dbset = DbContext.Personnels.Include(x => x.Contacts).Include(x => x.User).OrderByDescending(x => x.CreateDateTime).AsQueryable();
                Expression<Func<PersonnelProfileModel, bool>> predicate = null;


                if (request.Search?.Value.Length > 0 && !string.IsNullOrEmpty(request.Search?.Value))
                {
                    var code = ushort.TryParse(request.Search.Value, out ushort _) ? ushort.Parse(request.Search.Value) : 0;
                    predicate = profile => profile.Name.Contains(request.Search.Value) ||
                                           profile.LastName.Contains(request.Search.Value) ||
                                           profile.User.Username.Contains(request.Search.Value) ||
                                           profile.Code == code;
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
                return response;
            }
        }

        public async Task<FindCountAllResponse> FindCountAll(FindCountAllRequest request)
        {
            var response = new FindCountAllResponse();
            try
            {
                response.Count = DbContext.Personnels.Count(x => !x.IsDeleted);
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<DeleteByIdResponse> DeleteById(DeleteByIdRequest request)
        {
            var response = new DeleteByIdResponse();
            try
            {
                if (DbContext.Personnels.Any(x => x.Id == request.Id))
                {

                    var personnel = DbContext.Personnels.FirstOrDefault(x => x.Id == request.Id);

                    personnel.ReverseDelete();
                    DbContext.Entry(personnel).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.PersonnelDeleteSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.PersonnelDeleteFaild;
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

        public async Task<DeleteRangeByIdResponse> DeleteRangeById(DeleteRangeByIdRequest request)
        {
            var response = new DeleteRangeByIdResponse();
            try
            {
                PersonnelProfileModel personnel = null;
                foreach (var e in request.Entities)
                {
                    if (DbContext.Personnels.Any(x => x.Id == e.Id))
                    {
                        personnel = DbContext.Personnels.FirstOrDefault(x => x.Id == e.Id);

                        personnel.ReverseDelete();
                        DbContext.Entry(personnel).State = EntityState.Modified;
                    }
                }
                DbContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.PersonnelDeleteSucceed;
                response.AlertType = ResponseAlertResource_en.Success;

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

        public async Task<FindAllUpdateWorkingTimeResponse> FindAllUpdateWorkingTime(FindAllUpdateWorkingTimeRequest request)
        {
            var response = new FindAllUpdateWorkingTimeResponse();
            try
            {
                if (!DbContext.Personnels.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.PersonnelNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                response.Data = DbContext.Personnels.Include(x => x.WorkingTimes).FirstOrDefault(p => p.Id == request.Id).WorkingTimes.ToFindAllUpdateWorkingViewModel();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<UpdateWorkingTimeResponse> UpdateWorkingTime(UpdateWorkingTimeRequest request)
        {
            var response = new UpdateWorkingTimeResponse();
            try
            {
                if (DbContext.Personnels.Any(x => x.Id == request.Entity.PersonnelProfileId))
                {

                    var personnel = DbContext.Personnels.Include(x => x.WorkingTimes).FirstOrDefault(x => x.Id == request.Entity.PersonnelProfileId);
                    var salon = DbContext.Salons.FirstOrDefault();

                    PersonnelWorkingTimeModel model = new PersonnelWorkingTimeModel();
                    if (request.Entity.Selected && !personnel.WorkingTimes.Any(x => x.Id == request.Entity.Id))
                    {
                        model = request.Entity.ToCreateModel(salon);
                        personnel.WorkingTimes.Add(model);
                    }
                    else if (!request.Entity.Selected && personnel.WorkingTimes.Any(x => x.Id == request.Entity.Id))
                    {
                        var personnelWorkingTime = personnel.WorkingTimes.FirstOrDefault(x => x.Id == request.Entity.Id);
                        personnelWorkingTime.ReverseDelete();
                        DbContext.Entry(personnelWorkingTime).State = EntityState.Modified;
                    }
                    DbContext.SaveChanges();

                    response.Id = model.Id;
                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.PersonnelWorkingTimeUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.PersonnelUpdateFaild;
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

        public async Task<UpdateRangeWorkingTimeResponse> UpdateRangeWorkingTime(UpdateRangeWorkingTimeRequest request)
        {
            var response = new UpdateRangeWorkingTimeResponse();
            try
            {
                var salon = DbContext.Salons.FirstOrDefault();

                PersonnelProfileModel personnel = null;
                PersonnelWorkingTimeModel model = null;
                foreach (var item in request.Entities)
                {
                    if (DbContext.Personnels.Any(x => x.Id == item.PersonnelProfileId))
                    {
                        personnel = DbContext.Personnels.Include(x => x.WorkingTimes).FirstOrDefault(x => x.Id == item.PersonnelProfileId);
                        if (item.Selected && !personnel.WorkingTimes.Any(x => x.Id == item.Id))
                        {
                            model = item.ToCreateModel(salon);
                            personnel.WorkingTimes.Add(model);
                        }
                        else if (!item.Selected && personnel.WorkingTimes.Any(x => x.Id == item.Id))
                        {
                            model = personnel.WorkingTimes.FirstOrDefault(x => x.Id == item.Id);
                            model.ReverseDelete();
                            DbContext.Entry(model).State = EntityState.Modified;
                        }
                    }
                }
                DbContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.PersonnelWorkingTimeUpdateSucceed;
                response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<FindAllSelectResponse> FindAllSelect(FindAllSelectRequest request)
        {
            var response = new FindAllSelectResponse();
            try
            {
                response.SelectList = new SelectList(DbContext.Personnels.AsQueryable().AsNoTracking().Select(x => new { Id = x.Id, FullName = $"{x.Name} {x.LastName}" }).ToList(), "Id", "FullName");
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<UpdateLineResponse> UpdateLine(UpdateLineRequest request)
        {
            var response = new UpdateLineResponse();
            try
            {
                if (DbContext.Personnels.Any(x => x.Id == request.PersonnelId))
                {

                    var personnel = DbContext.Personnels.Include(x => x.LinePersonnels).FirstOrDefault(x => x.Id == request.PersonnelId);

                    if (request.Select)
                        personnel.LinePersonnels.Add(new Model.Line.LinePersonnelModel { LineId = request.LineId });
                    else
                        personnel.LinePersonnels.FirstOrDefault(x => x.LineId == request.LineId)?.ReverseDelete();

                    DbContext.Entry(personnel).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.IsSuccess = false;
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
    }
}
