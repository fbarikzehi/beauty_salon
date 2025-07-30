using Beauty.Application.ExternalServices.Sms;
using Beauty.Application.ExternalServices.Sms.Kavenegar;
using Beauty.Application.Modules.Customer.Mapping;
using Beauty.Application.Modules.Customer.Messaging;
using Beauty.Application.Modules.Customer.ViewModel;
using Beauty.Model.Customer;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Common.Application.File;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Utility;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Customer.Implementation
{
    public class CustomerService : ServiceBase<CoreDbContext>, ICustomerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CustomerService(IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironmen)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironmen;
        }

        public async Task<CreateResponse> Create(CreateRequest request)
        {
            var response = new CreateResponse();
            try
            {
                //if (DbContext.Customers.Any(x => x.Name == request.Entity.Name && x.LastName == request.Entity.LastName))
                //{
                //    response.IsSuccess = false;
                //    response.Message = MessagingResource_fa.CustomerNameAndLastName;
                //    response.AlertType = ResponseAlertResource_en.Danger;
                //    return response;
                //}

                if (!request.Entity.Contacts.Any(x => x.Type == ContactTypeEnum.Mobile) || string.IsNullOrWhiteSpace(request.Entity.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile)?.Value))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.MobileIsRequired;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                if (request.Entity.Contacts.Any(x => x.Type == ContactTypeEnum.Mobile && x.Value.Length < 11 && x.Value.Length > 11))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.MobileMustBe11Digits;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                foreach (var contact in request.Entity.Contacts)
                {
                    if (DbContext.Customers.Any(x => x.Contacts.Any(c => c.Type == contact.Type && c.Value == contact.Value)))
                    {
                        response.IsSuccess = false;
                        response.Message = $"{MessagingResource_fa.CustomerContactExist}: {contact.Value}";
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                }
                var thisYear = DateTimeUtility.GetPersianYear().ToString().Substring(2, 2);

                var mobile = request.Entity.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile);
                if (mobile is null)
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.MobileIsRequired;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                string memberCode = RandomUtility.GetAlphaNumRandomString(thisYear, 4, mobile.Value.Substring(7, 4));
                while (DbContext.Customers.Any(x => x.MemberCode == memberCode))
                    memberCode = RandomUtility.GetAlphaNumRandomString(thisYear, 4, mobile.Value.Substring(7, 4));

                if (request.Entity.AvatarFile != null)
                {
                    var fileManager = new FileManager(request.Entity.AvatarFile, _hostingEnvironment);
                    var result = fileManager.SaveToHost("customer-avatars", null);
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
                var customer = DbContext.Customers.Add(request.Entity.ToCreateModel(_httpContextAccessor, memberCode));
                if (DbContext.Users.Any(x => x.Id == request.Entity.UserId))
                {
                    var user = DbContext.Users.FirstOrDefault(x => x.Id == request.Entity.UserId);
                    user.FullName = $"{request.Entity.Name} {request.Entity.LastName}";
                }
                DbContext.SaveChanges();

                //Send Sms
                //var createSms = DbContext.SmsMessages.FirstOrDefault(x => x.SystemMessageType == SystemMessageTypeEnum.CustomerCreate);
                //if (createSms != null && createSms.AllowSend)
                //{
                //    var parameters = new string[6]
                //    {
                //        customer.Entity.Name,//0
                //        customer.Entity.LastName,//1
                //        customer.Entity.MemberCode,//2
                //        customer.Entity.User.Username,//3
                //        customer.Entity.User.Username,//4
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
                //        CreateUser = customer.Entity.CreateUser,
                //    });
                //    DbContext.SaveChanges();
                //}

                response.Id = customer.Entity.Id;
                response.MemberCode = customer.Entity.MemberCode;
                response.IsSuccess = true;
                response.Message = MessagingResource_fa.CustomerCreateSucceed;
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
                if (DbContext.Customers.Any(x => x.Id == request.Entity.Id))
                {

                    var customer = DbContext.Customers.Include(x => x.Contacts).FirstOrDefault(x => x.Id == request.Entity.Id);

                    if (!request.Entity.Contacts.Any(x => x.Type == ContactTypeEnum.Mobile) && !customer.Contacts.Any(x => x.Type == ContactTypeEnum.Mobile))
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.MobileIsRequired;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                    foreach (var contact in request.Entity.Contacts)
                    {
                        if (DbContext.Customers.Any(x => x.Contacts.Any(c => c.Type == contact.Type && c.Value == contact.Value && x.Id != request.Entity.Id)))
                        {
                            response.IsSuccess = false;
                            response.Message = $"{MessagingResource_fa.CustomerContactExist}: {contact.Value}";
                            response.AlertType = ResponseAlertResource_en.Danger;
                            return response;
                        }
                    }
                    if (!string.IsNullOrEmpty(customer.Avatar) && request.Entity.AvatarFile != null)
                    {
                        var fileManager = new FileManager(request.Entity.AvatarFile, _hostingEnvironment);
                        var result = fileManager.DeleteFromHost("customer-avatars", customer.Avatar);
                        if (result.IsSuccess)
                        {
                            result = fileManager.SaveToHost("customer-avatars", null);
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

                    customer = request.Entity.ToUpdateModel(customer, _httpContextAccessor);
                    DbContext.Entry(customer).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.CustomerUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.CustomerNotFound;
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

        public async Task<FindByIdResponse> FindById(FindByIdRequest request)
        {
            var response = new FindByIdResponse();
            try
            {
                if (!DbContext.Customers.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.CustomerNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                response.Entity = DbContext.Customers.Include(x => x.User).Include(x => x.Contacts).FirstOrDefault(x => x.Id == request.Id).ToFindByIdModel();
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

        public async Task<FindAllBySearchResponse> FindAllBySearch(FindAllBySearchRequest request)
        {
            var response = new FindAllBySearchResponse();
            try
            {
                var customers = DbContext.Customers.Include(x => x.Contacts).ToList().Where(x => string.Concat(x.Name, " ", x.LastName).Contains(request.FullName)).Distinct().ToFindAllBySearchViewModel();

                response.Data = customers;
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

        public async Task<FindAllByPagingResponse> FindAllByPaging(FindAllByPagingRequest request)
        {
            var response = new FindAllByPagingResponse();
            try
            {
                var dbset = DbContext.Customers.Include(x => x.Contacts).Include(x => x.User).OrderByDescending(x => x.CreateDateTime).AsQueryable();
                Expression<Func<CustomerProfileModel, bool>> predicate = null;


                if (request.Search?.Value.Length > 0 && !string.IsNullOrEmpty(request.Search?.Value))
                {
                    predicate = profile => profile.Name.Contains(request.Search.Value) ||
                                           profile.LastName.Contains(request.Search.Value) ||
                                           profile.User.Username.Contains(request.Search.Value) ||
                                           profile.MemberCode == request.Search.Value;
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

        public async Task<DeleteByIdResponse> DeleteById(DeleteByIdRequest request)
        {
            var response = new DeleteByIdResponse();
            try
            {
                if (DbContext.Customers.Any(x => x.Id == request.Id))
                {

                    var customer = DbContext.Customers.FirstOrDefault(x => x.Id == request.Id);

                    customer.ReverseDelete();
                    DbContext.Entry(customer).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.UserId = customer.UserId;
                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.CustomerDeleteSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.CustomerDeleteFaild;
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

        public async Task<DeleteContactResponse> DeleteContact(DeleteContactRequest request)
        {
            var response = new DeleteContactResponse();
            try
            {
                if (DbContext.Customers.Any(x => x.Contacts.Any(y => y.Id == request.Id)))
                {

                    var customer = DbContext.Customers.Include(x => x.Contacts).FirstOrDefault(x => x.Contacts.Any(y => y.Id == request.Id));

                    customer.Contacts.FirstOrDefault(y => y.Id == request.Id).ReverseDelete();
                    DbContext.Entry(customer).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.CustomerContactDeleteSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.Message = MessagingResource_fa.CustomerUpdateFaild;
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

        public async Task<CustomerChequeCreateResponse> ChequeCreate(CustomerChequeCreateRequest request)
        {
            var response = new CustomerChequeCreateResponse();
            try
            {
                if (!DbContext.Customers.Any(x => x.Id == request.Entity.CustomerProfileId))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.CustomerNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var customer = DbContext.Customers.Include(x => x.CustomerCheques).FirstOrDefault(x => x.Id == request.Entity.CustomerProfileId);
                var cheque = request.Entity.ToCreateModel();
                if (!customer.CustomerCheques.Any(x => x.BankId == cheque.BankId && x.Date == cheque.Date && x.Fee == cheque.Fee && x.Number == cheque.Number))
                {
                    customer.CustomerCheques.Add(cheque);
                    DbContext.SaveChanges();

                    response.Id = cheque.Id;
                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.CustomerChequeCreateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.CustomerChequeAlreadyAdded;
                    response.AlertType = ResponseAlertResource_en.Success;
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

        public async Task<CustomerChequeFindAllByCreateDateResponse> ChequeFindAllByCreateDate(CustomerChequeFindAllByCreateDateRequest request)
        {
            var response = new CustomerChequeFindAllByCreateDateResponse();
            try
            {
                if (!DbContext.Customers.Any(x => x.Id == request.CustomerProfileId))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.CustomerNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var customer = DbContext.Customers.Include(x => x.CustomerCheques).ThenInclude(x => x.Bank).FirstOrDefault(x => x.Id == request.CustomerProfileId);

                var date = request.CreateDateTime.PersianDateStringToDateTime();
                response.Data = customer.CustomerCheques.Where(x => x.CreateDateTime == date).Select(x => new CustomerChequeViewModel
                {
                    Id = x.Id,
                    BankName = x.Bank.Title,
                    Date = x.Date.ToPersianDate(),
                    Fee = x.Fee.ToString(TypeUtility.CommaFormatted),
                    Number = x.Number,
                    CreateDateTime = x.CreateDateTime.ToPersianDate(),
                    Details = x.Details,
                }).ToList();
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
