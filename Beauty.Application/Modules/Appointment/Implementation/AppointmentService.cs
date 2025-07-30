using Beauty.Application.ExternalServices.Sms.Kavenegar;
using Beauty.Application.Modules.Appointment.Mapping;
using Beauty.Application.Modules.Appointment.Messaging;
using Beauty.Application.Modules.Appointment.ViewModel;
using Beauty.Model.Appointment;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Utility;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Appointment.Implementation
{
    public class AppointmentService : ServiceBase<CoreDbContext>, IAppointmentService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly ILogger<AppointmentService> _log;
        public AppointmentService(IHttpContextAccessor httpContextAccessor, ILogger<AppointmentService> log)
        {
            _httpContextAccessor = httpContextAccessor;
            _log = log;
        }

        public async Task<CreateResponse> Create(CreateRequest request)
        {
            var response = new CreateResponse();
            try
            {
                var date = request.Entity.Date.PersianDateStringToDateTime();
                //Update Service 
                if (DbContext.Appointments.Any(x => x.Services.Any(s => s.Id == request.Entity.Service.AppointmentServiceId) && !x.IsCanceled && !x.IsDeleted))
                {
                    var appointment = DbContext.Appointments.Include(x => x.Services).FirstOrDefault(x => x.Services.Any(s => s.Id == request.Entity.Service.AppointmentServiceId));
                    var appointmentService = appointment.Services.FirstOrDefault(x => x.ServiceId == request.Entity.Service.AppointmentServiceId);
                    if (appointmentService != null)
                    {
                        appointmentService.Time = request.Entity.Time.StringToTimeSpan();
                        appointmentService.ServiceId = request.Entity.Service.ServiceId;

                        if (appointment.IsDone)
                            appointment.ReverseDone();
                    }
                }
                //Add Service
                else if (DbContext.Appointments.Any(x => x.CustomerProfileId == request.Entity.CustomerProfileId && x.Date == date && !x.IsCanceled && !x.IsDeleted))
                {

                    var appointment = DbContext.Appointments.Include(x => x.Services).FirstOrDefault(x => x.CustomerProfileId == request.Entity.CustomerProfileId && x.Date == date);
                    if (appointment.Services.Any(x => x.ServiceId == request.Entity.Service.ServiceId))
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.AppointmentServiceExistForCustomer;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                    appointment.Services.Add(new AppointmentServiceModel
                    {
                        AppointmentId = appointment.Id,
                        Time = request.Entity.Time.StringToTimeSpan(),
                        ServiceId = request.Entity.Service.ServiceId,
                        PersonnelProfileId = string.IsNullOrEmpty(request.Entity.Service.PersonnelId) ? null : (Guid?)new Guid(request.Entity.Service.PersonnelId),
                    });

                    if (appointment.IsDone)
                        appointment.ReverseDone();
                }
                //Create Appointment and add service
                else
                {
                    var model = request.Entity.ToCreateModel(_httpContextAccessor);

                    //9936811000
                    string followingCode = $"{DateTime.Now.GetShortStringPersianYear()}{request.Entity.CustomerMobile.Substring(7, 4)}{RandomUtility.GetRandomUshort(1000)}";
                    while (DbContext.Appointments.Any(x => x.FollowingCode == followingCode))
                        followingCode = $"{DateTime.Now.GetShortStringPersianYear()}{request.Entity.CustomerMobile.Substring(7, 4)}{RandomUtility.GetRandomUshort(1000)}";

                    model.FollowingCode = followingCode;
                    DbContext.Appointments.Add(model);
                }
                DbContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentCreateSucceed;
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
                if (!DbContext.Appointments.Any(x => x.Id == request.Entity.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var appointment = request.Entity.ToUpdateModel(DbContext.Appointments.Include(x => x.Services).FirstOrDefault(x => x.Id == request.Entity.Id));

                foreach (var service in request.Entity.Services)
                {
                    if (!appointment.Services.Any(x => x.Id == service.AppointmentServiceId))
                        appointment.Services.Add(new AppointmentServiceModel { ServiceId = service.ServiceId });
                }
                DbContext.Entry(appointment).State = EntityState.Modified;
                DbContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentUpdateSucceed;
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
                response.Entity = DbContext.Appointments
                    .Include(x => x.CustomerProfile).ThenInclude(x => x.Contacts)
                    .Include(x => x.Services).ThenInclude(x => x.Service).ThenInclude(x => x.Prices)
                    .Include(x => x.Services).ThenInclude(x => x.PersonnelProfile).ThenInclude(x => x.Services)
                    .Include(x => x.Payments)
                    .FirstOrDefault(x => x.Id == request.Id).ToFindByIdViewModel();
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

        public async Task<FindAllByDateResponse> FindAllByDate(FindAllByDateRequest request)
        {
            var response = new FindAllByDateResponse();
            try
            {
                var date = request.Date.PersianDateStringToDateTime().Date;
                response.Data = DbContext.Appointments.Include(x => x.CustomerProfile).Include(x => x.Services).ThenInclude(x => x.Service).Where(x => x.Date == date).ToFindAllByDateViewModel();
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

        public async Task<FindAllByStartEndDateResponse> FindAllByStartEndDate(FindAllByStartEndDateRequest request)
        {
            var response = new FindAllByStartEndDateResponse();
            try
            {
                response.Data = DbContext.Appointments.Include(x => x.CustomerProfile).ThenInclude(x => x.Contacts)
                                                          .Include(x => x.Services).ThenInclude(x => x.Service)
                                                          .Where(x => x.Date >= request.Start && x.Date <= request.End).ToFindAllByStartEndDateViewModel();
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

        public async Task<CancelResponse> Cancel(CancelRequest request)
        {
            var response = new CancelResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var appointment = DbContext.Appointments.Find(request.Id);

                if (DbContext.Appointments.Any(x => x.Id == request.Id && (x.IsDeleted || x.IsDone || x.Services.Any(x => x.IsDone))))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppoinmentNotCancel;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                //var appointmentDateTime = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day, 0, 0, 0);
                //var nowDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                if (!appointment.IsDone && !appointment.IsCanceled && !appointment.IsDeleted)
                {
                    appointment.ReverseCanceled();
                    DbContext.Entry(appointment).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentCancelSucceed;
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

        public async Task<DoneResponse> Done(DoneRequest request)
        {
            var response = new DoneResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var appointment = DbContext.Appointments.Include(x => x.Services).FirstOrDefault(x => x.Id == request.Id);

                if (!appointment.IsDone && !appointment.IsCanceled && !appointment.IsDeleted)
                {
                    foreach (var appointmentService in appointment.Services)
                        if (!appointmentService.IsDone)
                            appointmentService.ReverseDone();


                    appointment.ReverseDone();
                    DbContext.Entry(appointment).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentDoneSucceed;
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

        public async Task<DeleteResponse> Delete(DeleteRequest request)
        {
            var response = new DeleteResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Id == request.Id))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var appointment = DbContext.Appointments.Include(x => x.Services).FirstOrDefault(x => x.Id == request.Id);

                if (DbContext.Appointments.Any(x => x.Id == request.Id && (x.IsCanceled || x.IsDone || x.Services.Any(x => x.IsDone))))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppoinmentNotDelete;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                //var appointmentDateTime = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day, 0, 0, 0);
                //var nowDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                if (!appointment.IsDone && !appointment.IsCanceled && !appointment.IsDeleted)
                {
                    foreach (var appointmentService in appointment.Services)
                        appointmentService.ReverseDelete();

                    appointment.ReverseDelete();
                    //appointment.ReverseDone();
                    DbContext.Entry(appointment).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentDeleteSucceed;
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

        public async Task<DoneServiceResponse> DoneService(DoneServiceRequest request)
        {
            var response = new DoneServiceResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Id == request.Appointment && x.Services.Any(s => s.Id == request.AppointmentServiceId)))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var appointment = DbContext.Appointments.Include(x => x.Services).FirstOrDefault(x => x.Id == request.Appointment);
                if (!appointment.Services.FirstOrDefault(x => x.Id == request.AppointmentServiceId).IsDone &&
                    !appointment.Services.FirstOrDefault(x => x.Id == request.AppointmentServiceId).IsDeleted)
                {
                    appointment.Services.FirstOrDefault(x => x.Id == request.AppointmentServiceId).ReverseDone();
                    if (appointment.Services.All(x => x.IsDone)) appointment.ReverseDone();
                    DbContext.Entry(appointment).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.AppointmentServiceDoneSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }

                response.IsSuccess = false;
                response.Message = MessagingResource_fa.AppointmentServiceDoneNotFound;
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

        public async Task<DeleteServiceResponse> DeleteService(DeleteServiceRequest request)
        {
            var response = new DeleteServiceResponse();
            try
            {
                if (!DbContext.Appointments.Include(x => x.Services).Any(x => x.Services.Any(y => y.Id == request.Id)))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var appointmentService = DbContext.Appointments.Include(x => x.Services).FirstOrDefault(x => x.Services.Any(y => y.Id == request.Id)).Services.FirstOrDefault(x => x.Id == request.Id);

                if (!appointmentService.IsDone && !appointmentService.IsDeleted)
                {
                    appointmentService.ReverseDelete();
                    appointmentService.ReverseDone();
                    DbContext.Entry(appointmentService).State = EntityState.Modified;
                    DbContext.SaveChanges();
                }

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentServiceDeleteSucceed;
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

        public async Task<AddPaymentResponse> AddPayment(AddPaymentRequest request)
        {
            var response = new AddPaymentResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Id == request.AppointmentId))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var appointment = DbContext.Appointments.Include(x => x.Payments)
                                                        .Include(x => x.Services).ThenInclude(x => x.Service.Prices)
                                                        .Include(x => x.Services).ThenInclude(x => x.AppointmentServiceDetails).FirstOrDefault(x => x.Id == request.AppointmentId);
                var totalPayment = appointment.Payments.Sum(x => x.Amount) + request.Amount.ToFloatPrice();
                var totalDoneFee = appointment.Services.Where(x => x.IsDone).Sum(x => x.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price) + appointment.Services.Where(x => x.IsDone).Sum(x => x.AppointmentServiceDetails.Sum(x => x.TotalPrice));
                if (totalPayment > totalDoneFee)
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppoinmentPaymentSumCanNotBeGreaterThanTotalPrice;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }

                var payment = new AppointmentPaymentModel
                {
                    Amount = TypeUtility.ToFloatPrice(request.Amount),
                    CreateUser = ClaimManager.GetUserId(_httpContextAccessor)
                };
                payment.SetCreateDateTime(DateTime.Now);
                appointment.Payments.Add(payment);
                DbContext.Entry(appointment).State = EntityState.Modified;
                DbContext.SaveChanges();

                response.Id = payment.Id;
                response.CreateDateTime = payment.CreateDateTime.ToShortPersianDateTime();
                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentAddPaymentSucceed;
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

        public async Task<AddDiscountResponse> AddDiscount(AddDiscountRequest request)
        {
            var response = new AddDiscountResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Id == request.AppointmentId))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var appointment = DbContext.Appointments.Include(x => x.Discounts).FirstOrDefault(x => x.Id == request.AppointmentId);

                var discount = new AppointmentDiscountModel
                {
                    Amount = TypeUtility.ToFloatPrice(request.Amount),

                };
                discount.SetCreateDateTime(DateTime.Now);
                appointment.Discounts.Add(discount);
                DbContext.Entry(appointment).State = EntityState.Modified;
                DbContext.SaveChanges();

                response.Id = discount.Id;
                response.CreateDateTime = discount.CreateDateTime.ToShortPersianDateTime();
                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentAddDiscountSucceed;
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

        public async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request)
        {
            var response = new DeleteDiscountResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Discounts.Any(a => a.Id == request.Id)))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentPaymentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var appointment = DbContext.Appointments.Include(x => x.Discounts).FirstOrDefault(x => x.Discounts.Any(a => a.Id == request.Id));

                var discount = appointment.Discounts.FirstOrDefault(x => x.Id == request.Id);
                discount.ReverseDelete();
                DbContext.Entry(appointment).State = EntityState.Modified;
                DbContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentDeleteDiscountSucceed;
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

        public async Task<DeletePaymentResponse> DeletePayment(DeletePaymentRequest request)
        {
            var response = new DeletePaymentResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Payments.Any(a => a.Id == request.Id)))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentPaymentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
                var appointment = DbContext.Appointments.Include(x => x.Payments).FirstOrDefault(x => x.Payments.Any(a => a.Id == request.Id));

                var payment = appointment.Payments.FirstOrDefault(x => x.Id == request.Id);
                payment.ReverseDelete();
                DbContext.Entry(appointment).State = EntityState.Modified;
                DbContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = MessagingResource_fa.AppoinmentDeletePaymentSucceed;
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

        public async Task<FindAllByPersonnelResponse> FindAllServicesByPersonnelAndDate(FindAllByPersonnelRequest request)
        {
            var response = new FindAllByPersonnelResponse();
            try
            {
                List<AppointmentModel> appointments = DbContext.Appointments
                                                           .Include(x => x.CustomerProfile).ThenInclude(x => x.Contacts)
                                                           .Include(x => x.Services).ThenInclude(x => x.Service).ThenInclude(x => x.Prices)
                                                           .Include(x => x.Services).ThenInclude(x => x.PersonnelProfile).ThenInclude(x => x.Services)
                                                           .Include(x => x.Services).ThenInclude(x => x.Appointment).ThenInclude(x => x.CustomerProfile)
                                                           .Where(x => x.Date.Equals(request.Date.Date) && x.Services.Any(x => x.PersonnelProfileId == request.Id)).ToList();

                //_log.LogError(date.ToString());
                var appointmentPersonnelItems = new List<AppointmentPersonnelItemViewModel>();
                foreach (var appointment in appointments.OrderBy(x => x.Time))
                {
                    appointmentPersonnelItems.AddRange(appointment.Services.Where(x => x.PersonnelProfileId == request.Id && !x.IsDeleted).Select(appointmentService => new AppointmentPersonnelItemViewModel
                    {
                        AppointmentId = appointment.Id,
                        AppointmentServiceId = appointmentService.Id,
                        CustomerFullName = $"{appointment.CustomerProfile.Name} {appointment.CustomerProfile.LastName}",
                        CustomerAvatarUrl = appointment.CustomerProfile.Avatar,
                        ServiceTile = appointmentService.Service.Title,
                        ServicePrice = appointmentService.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price.ToString(TypeUtility.CommaFormatted),
                        TotalPrepayment = appointmentService.Service.Prepayment.ToString(TypeUtility.CommaFormatted),
                        TotalRemainingPrice = (appointmentService.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price - appointmentService.Service.Prepayment).ToString(TypeUtility.CommaFormatted),
                        Time = appointment.Time.ToString(TypeUtility.HoursAndMinutes),
                        Status = appointment.IsCanceled ? AppointmentServiceStatusTypeEnum.Canceled :
                                   (appointmentService.IsDone ? AppointmentServiceStatusTypeEnum.Done : AppointmentServiceStatusTypeEnum.Watting),
                    }));
                }
                response.Data = appointmentPersonnelItems;
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<FindAllByPersonnelResponse> FindAllNotDoneServicesByPersonnelAndDate(FindAllByPersonnelRequest request)
        {
            var response = new FindAllByPersonnelResponse();
            try
            {
                List<AppointmentModel> appointments = DbContext.Appointments
                                                           .Include(x => x.CustomerProfile).ThenInclude(x => x.Contacts)
                                                           .Include(x => x.Services).ThenInclude(x => x.Service).ThenInclude(x => x.Prices)
                                                           .Include(x => x.Services).ThenInclude(x => x.PersonnelProfile).ThenInclude(x => x.Services)
                                                           .Include(x => x.Services).ThenInclude(x => x.Appointment).ThenInclude(x => x.CustomerProfile)
                                                           .Where(x => x.Date.Equals(request.Date.Date) && x.Services.Any(x => x.PersonnelProfileId == request.Id && !x.IsDone)).ToList();

                var appointmentPersonnelItems = new List<AppointmentPersonnelItemViewModel>();
                foreach (var appointment in appointments.OrderBy(x => x.Time))
                {
                    appointmentPersonnelItems.AddRange(appointment.Services.Where(x => x.PersonnelProfileId == request.Id && !x.IsDone && !x.IsDeleted).Select(appointmentService => new AppointmentPersonnelItemViewModel
                    {
                        AppointmentId = appointment.Id,
                        AppointmentServiceId = appointmentService.Id,
                        CustomerFullName = $"{appointment.CustomerProfile.Name} {appointment.CustomerProfile.LastName}",
                        CustomerAvatarUrl = appointment.CustomerProfile.Avatar,
                        ServiceTile = appointmentService.Service.Title,
                        ServicePrice = appointmentService.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price.ToString(TypeUtility.CommaFormatted),
                        TotalPrepayment = appointmentService.Service.Prepayment.ToString(TypeUtility.CommaFormatted),
                        TotalRemainingPrice = (appointmentService.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price - appointmentService.Service.Prepayment).ToString(TypeUtility.CommaFormatted),
                        Time = appointment.Time.ToString(TypeUtility.HoursAndMinutes),
                        Status = appointment.IsCanceled ? AppointmentServiceStatusTypeEnum.Canceled : AppointmentServiceStatusTypeEnum.Watting,
                    }));
                }
                response.Data = appointmentPersonnelItems;
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<AppointmentFindAllSchedulerResponse> FindAllScheduler(AppointmentFindAllSchedulerRequest request)
        {
            var response = new AppointmentFindAllSchedulerResponse();
            try
            {
                var salon = DbContext.Salons.FirstOrDefault();

                var appointmentSchedulerList = new List<AppointmentSchedulerViewModel>();
                var date = request.Date.PersianDateStringToDateTime().Date;
                var appointments = DbContext.Appointments.Include(x => x.CustomerProfile).ThenInclude(x => x.Contacts)
                                                         .Include(x => x.Services).ThenInclude(x => x.Service)
                                                         .Where(x => x.Date == date && !x.IsCanceled && !x.IsDeleted).OrderBy(x => x.Time);

                var personnels = DbContext.Personnels.AsQueryable().AsNoTracking();

                //var OpenTime = new TimeSpan(salon.ClosingTime.Ticks - salon.OpeningTime.Ticks);
                //var timeFractionCount = (int)OpenTime.TotalMinutes / 30;
                //var openTimeMinutes = OpenTime.TotalMinutes;
                AppointmentSchedulerViewModel appointmentScheduler = null;
                foreach (var personnel in personnels)
                {
                    appointmentScheduler = new AppointmentSchedulerViewModel
                    {
                        PersonnelId = personnel.Id,
                        PersonnelFullName = $"{personnel.Name} {personnel.LastName}",
                        Cards = new List<AppointmentCardViewModel>(),
                    };
                    foreach (var appointment in appointments)
                    {
                        foreach (var appointmentService in appointment.Services.Where(s => s.PersonnelProfileId == personnel.Id))
                        {
                            appointmentScheduler.Cards.Add(new AppointmentCardViewModel
                            {
                                AppointmentId = appointment.Id,
                                AppointmentServiceId = appointmentService.Id,
                                CustomerId = appointment.CustomerProfile.Id,
                                CustomerFullName = $"{appointment.CustomerProfile.Name} {appointment.CustomerProfile.LastName}",
                                CustomerMemberCode = appointment.CustomerProfile.MemberCode,
                                CustomerMobile = appointment.CustomerProfile.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile && x.IsActive).Value,
                                ServiceId = appointmentService.ServiceId,
                                ServiceTitle = $"{appointmentService.Service.Title} ({appointmentService.Service.DurationMinutes})",
                                ServiceDurationMinutes = appointmentService.Service.DurationMinutes,
                                StartTime = appointmentService.Time.ToString(TypeUtility.HoursAndMinutes),
                                StartTimeTimeSpan = appointmentService.Time,
                                Date = appointment.Date.ToPersianDate(),
                                FinishTime = (appointmentService.Time + new TimeSpan(0, appointmentService.Service.DurationMinutes, 0)).ToString(TypeUtility.HoursAndMinutes),
                                FinishTimeTimeSpan = appointmentService.Time + new TimeSpan(0, appointmentService.Service.DurationMinutes, 0),
                                Status = appointmentService.Time == new TimeSpan(0, 0, 0) ? null : (bool?)true,
                                IsDone = appointmentService.IsDone,
                            });
                        }
                    }
                    //var cardCount = appointmentScheduler.Cards.Count;
                    //appointmentScheduler.Cards = appointmentScheduler.Cards.OrderBy(x => x.StartTimeTimeSpan).ToList();
                    //var AppointmentCardList = appointmentScheduler.Cards.OrderBy(x => x.StartTimeTimeSpan).ToList();
                    //for (int card = 0; card < cardCount; card++)
                    //{
                    //    if (appointmentScheduler.Cards[card].StartTimeTimeSpan > new TimeSpan(0, 0, 0))
                    //    {
                    //        if (card + 1 < cardCount)
                    //        {
                    //            if (appointmentScheduler.Cards[card].FinishTimeTimeSpan != appointmentScheduler.Cards[card + 1].StartTimeTimeSpan)
                    //                AppointmentCardList.Add(new AppointmentCardViewModel
                    //                {
                    //                    StartTime = appointmentScheduler.Cards[card].FinishTimeTimeSpan.ToString(TypeUtility.HoursAndMinutes),
                    //                    StartTimeTimeSpan = appointmentScheduler.Cards[card].FinishTimeTimeSpan,
                    //                    Status = false,
                    //                });
                    //        }
                    //        else if (appointmentScheduler.Cards[card].FinishTimeTimeSpan < salon.ClosingTime)
                    //        {
                    //            AppointmentCardList.Add(new AppointmentCardViewModel
                    //            {
                    //                StartTime = appointmentScheduler.Cards[card].FinishTimeTimeSpan.ToString(TypeUtility.HoursAndMinutes),
                    //                StartTimeTimeSpan = appointmentScheduler.Cards[card].FinishTimeTimeSpan,
                    //                Status = false,
                    //            });
                    //        }
                    //    }
                    //}
                    //if (AppointmentCardList.Where(x => x.StartTimeTimeSpan > new TimeSpan(0, 0, 0)).Count() == 0)
                    //{
                    //    AppointmentCardList.Add(new AppointmentCardViewModel
                    //    {
                    //        StartTime = salon.OpeningTime.ToString(TypeUtility.HoursAndMinutes),
                    //        StartTimeTimeSpan = salon.OpeningTime,
                    //        Status = false,
                    //    });
                    //}
                    //appointmentScheduler.Cards = AppointmentCardList.OrderBy(x => x.StartTimeTimeSpan).ToList();
                    appointmentScheduler.Cards = appointmentScheduler.Cards.OrderBy(x => x.StartTimeTimeSpan).ToList();
                    appointmentSchedulerList.Add(appointmentScheduler);

                }

                response.Data = appointmentSchedulerList;
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

        public async Task<AppointmentFindDetailResponse> FindDetailByCustomer(AppointmentFindDetailRequest request)
        {
            var response = new AppointmentFindDetailResponse();
            try
            {
                if (request.CustomerId == Guid.Empty)
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.CustomerIsRequierd;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                if (string.IsNullOrEmpty(request.Date))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentDateIsRequierd;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var date = request.Date.PersianDateStringToDateTime().Date;

                var appointment = DbContext.Appointments
                    .Include(x => x.CustomerProfile).ThenInclude(x => x.Contacts)
                    .Include(x => x.Services).ThenInclude(x => x.Service.Prices)
                    .Include(x => x.Services).ThenInclude(x => x.Service.Details)
                    .Include(x => x.Services).ThenInclude(x => x.AppointmentServiceDetails)
                    .Include(x => x.Services).ThenInclude(x => x.PersonnelProfile)
                    .Include(x => x.Payments)
                    .Include(x => x.Discounts)
                    .FirstOrDefault(x => x.CustomerProfileId == request.CustomerId && x.Date == date);

                if (appointment is null)
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExistByCustomerAndDate;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                response.Entity = appointment.ToFindDetailByCustomerViewModel();
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

        public async Task<ChangeServicePersonnelResponse> ChangeServicePersonnel(ChangeServicePersonnelRequest request)
        {
            var response = new ChangeServicePersonnelResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Id == request.AppointmentId && x.Services.Any(s => s.Id == request.AppointmentServiceId)))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var appointment = DbContext.Appointments.Include(x => x.Services).FirstOrDefault(x => x.Id == request.AppointmentId);
                if (!appointment.Services.FirstOrDefault(x => x.Id == request.AppointmentServiceId).IsDeleted)
                {
                    appointment.Services.FirstOrDefault(x => x.Id == request.AppointmentServiceId).PersonnelProfileId = request.PersonnelId;
                    DbContext.Entry(appointment).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }

                response.IsSuccess = false;
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

        public async Task<AppointmentServiceDetailModifyResponse> AppointmentServiceDetailModify(AppointmentServiceDetailModifyRequest request)
        {
            var response = new AppointmentServiceDetailModifyResponse();
            try
            {
                if (!DbContext.Appointments.Any(x => x.Services.Any(s => s.Id == request.AppointmentServiceId)))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var appointment = DbContext.Appointments.Include(x => x.Services).ThenInclude(x => x.AppointmentServiceDetails).FirstOrDefault(x => x.Services.Any(s => s.Id == request.AppointmentServiceId));
                if (!appointment.Services.FirstOrDefault(x => x.Id == request.AppointmentServiceId).IsDeleted)
                {
                    var appointmentServiceDetails = appointment.Services.FirstOrDefault(x => x.Id == request.AppointmentServiceId).AppointmentServiceDetails;
                    //Create
                    if (appointmentServiceDetails.Count == 0)
                    {
                        foreach (var serviceDetail in request.Details)
                        {
                            appointmentServiceDetails.Add(new AppointmentServiceDetailModel
                            {
                                AppointmentServiceId = request.AppointmentServiceId,
                                ServiceDetailId = serviceDetail.ServiceDetailId,
                                Count = serviceDetail.Count,
                                Price = serviceDetail.Price.ToFloatPrice(),
                                TotalPrice = serviceDetail.TotalPrice.ToFloatPrice()
                            });
                        }
                        DbContext.SaveChanges();

                        response.IsSuccess = true;
                        response.Message = MessagingResource_fa.AppointmentServiceDetailsCreateSucceed;
                        response.AlertType = ResponseAlertResource_en.Success;
                        return response;
                    }
                    //Update
                    else if (appointmentServiceDetails.Count > 0)
                    {
                        foreach (var serviceDetail in request.Details)
                        {
                            appointmentServiceDetails.FirstOrDefault(x => x.ServiceDetailId == serviceDetail.ServiceDetailId).Count = serviceDetail.Count;
                            appointmentServiceDetails.FirstOrDefault(x => x.ServiceDetailId == serviceDetail.ServiceDetailId).Price = serviceDetail.Price.ToFloatPrice();
                            appointmentServiceDetails.FirstOrDefault(x => x.ServiceDetailId == serviceDetail.ServiceDetailId).TotalPrice = serviceDetail.TotalPrice.ToFloatPrice();
                        }
                        DbContext.SaveChanges();

                        response.IsSuccess = true;
                        response.Message = MessagingResource_fa.AppointmentServiceDetailsCreateSucceed;
                        response.AlertType = ResponseAlertResource_en.Success;
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.AppointmentServiceDetailsNotExist;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }

                }
                response.IsSuccess = false;
                response.Message = MessagingResource_fa.AppointmentServiceDetailsNotExist;
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

        public async Task<AppointmentServiceDetailFindAllResponse> AppointmentServiceDetailFindAll(AppointmentServiceDetailFindAllRequest request)
        {
            var response = new AppointmentServiceDetailFindAllResponse();
            try
            {
                var appointment = DbContext.Appointments
                    .Include(x => x.Services).ThenInclude(x => x.Service.Details)
                    .Include(x => x.Services).ThenInclude(x => x.AppointmentServiceDetails)
                    .FirstOrDefault(x => x.Services.Any(y => y.Id == request.Id));

                if (appointment is null)
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.AppointmentServiceDetailsNotExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                var appointmentService = appointment.Services.FirstOrDefault(x => x.Id == request.Id);
                if (appointmentService.AppointmentServiceDetails.Count > 0)
                {
                    response.Data = appointmentService.AppointmentServiceDetails.Select(x => new AppointmentServiceDetailViewModel
                    {
                        AppointmentServiceDetailId = x.Id,
                        Title = DbContext.Services.FirstOrDefault(s => s.Details.Any(d => d.Id == x.ServiceDetailId))?.Details.FirstOrDefault(d => d.Id == x.ServiceDetailId)?.Title,
                        Count = x.Count,
                        Price = x.Price.ToString(TypeUtility.CommaFormatted),
                        ServiceDetailId = x.ServiceDetailId,
                        TotalPrice = x.TotalPrice.ToString(TypeUtility.CommaFormatted),
                    }).ToList();
                }
                else
                {
                    var serviceDetails = appointmentService.Service.Details;
                    if (serviceDetails.Count > 0)
                    {
                        response.Data = serviceDetails.Select(x => new AppointmentServiceDetailViewModel
                        {
                            Title = x.Title,
                            Count = 0,
                            Price = x.Price.ToString(TypeUtility.CommaFormatted),
                            ServiceDetailId = x.Id,
                            TotalPrice = "0",
                        }).ToList();
                    }
                }

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

        public async Task<AppointmentDiscountFindAllResponse> AppointmentDiscountFindAll(AppointmentDiscountFindAllRequest request)
        {
            var response = new AppointmentDiscountFindAllResponse();
            try
            {
                response.Data = DbContext.Appointments.Include(x => x.Discounts).FirstOrDefault(x => x.Id == request.Id).Discounts.ToFindAllDiscountViewModel();
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
    }
}
