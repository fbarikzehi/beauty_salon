using Beauty.Application.Modules.Appointment.ViewModel;
using Beauty.Model.Appointment;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Utility;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Appointment.Mapping
{
    public static class AppointmentMapper
    {
        public static AppointmentModel ToCreateModel(this AppointmentCreateViewModel appointment, IHttpContextAccessor httpContextAccessor)
        {
            var model = new AppointmentModel
            {
                CustomerProfileId = appointment.CustomerProfileId,
                Date = appointment.Date.PersianDateStringToDateTime().Date,
                Time = appointment.Time.StringToTimeSpan(),
                FollowingCode = appointment.FollowingCode,
                Services = new List<AppointmentServiceModel>
                {
                   new AppointmentServiceModel
                   {
                      Time = appointment.Time.StringToTimeSpan(),
                      ServiceId = appointment.Service.ServiceId,
                      PersonnelProfileId = string.IsNullOrEmpty(appointment.Service.PersonnelId) ? null : (Guid?)new Guid(appointment.Service.PersonnelId),
                   }
                },
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
            model.SetCreateDateTime(appointment.CreateDateTime.PersianDateStringToDateTime());
            return model;
        }

        public static AppointmentModel ToUpdateModel(this AppointmentUpdateViewModel appointment, AppointmentModel model)
        {

            model.Date = appointment.CreateDateTime.PersianDateStringToDateTime().Date;
            model.CustomerProfileId = appointment.CustomerProfileId;
            model.Time = appointment.Time.StringToTimeSpan();

            return model;
        }

        public static AppointmentViewModel ToFindByIdViewModel(this AppointmentModel appointment)
        {
            var paymentDetials = appointment.Services.Where(x => x.IsDone).Select(appointmentService => new AppointmentPaymentDetialViewModel
            {
                PersonnelFullName = $"{appointmentService.PersonnelProfile.Name} {appointmentService.PersonnelProfile.LastName}",
                PersonnelPercentage = appointmentService.PersonnelProfile.Services.FirstOrDefault(x => x.ServiceId == appointmentService.ServiceId)?.Percentage != null ? appointmentService.PersonnelProfile.Services.FirstOrDefault(x => x.ServiceId == appointmentService.ServiceId).Percentage?.ToString() : "0",
                PersonnelShare = appointmentService.PersonnelProfile.Services.FirstOrDefault(x => x.ServiceId == appointmentService.ServiceId)?.Percentage != null ? ((appointmentService.Service.Prices.OrderByDescending(p => p.Id).FirstOrDefault().Price * (float)appointmentService.PersonnelProfile.Services.FirstOrDefault(x => x.ServiceId == appointmentService.ServiceId).Percentage) / 100).ToString(TypeUtility.CommaFormatted) : "0",
                SalonShare = appointmentService.PersonnelProfile.Services.FirstOrDefault(x => x.ServiceId == appointmentService.ServiceId)?.Percentage != null ? (appointmentService.Service.Prices.OrderByDescending(p => p.Id).FirstOrDefault().Price - (appointmentService.Service.Prices.OrderByDescending(p => p.Id).FirstOrDefault().Price * (float)appointmentService.PersonnelProfile.Services.FirstOrDefault(x => x.ServiceId == appointmentService.ServiceId).Percentage) / 100).ToString(TypeUtility.CommaFormatted) : appointmentService.Service.Prices.OrderByDescending(p => p.Id).FirstOrDefault().Price.ToString(TypeUtility.CommaFormatted),
            });
            return new AppointmentViewModel
            {
                Id = appointment.Id,
                CustomerFullName = $"{appointment.CustomerProfile.Name} {appointment.CustomerProfile.LastName}",
                CustomerProfileId = appointment.CustomerProfileId,
                CustomerMobile = appointment.CustomerProfile.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile)?.Value,
                CreateDateTime = appointment.CreateDateTime.ToShortPersianDateTime(),
                Date = appointment.Date.ToPersianDate(),
                Time = appointment.Time.ToString(TypeUtility.HoursAndMinutes),
                IsDone = appointment.IsDone,
                TotalPrice = appointment.Services.Sum(x => x.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price).ToString(TypeUtility.CommaFormatted),
                TotalPrepayment = appointment.Services.Sum(x => x.Service.Prepayment).ToString(TypeUtility.CommaFormatted),
                TotalRemainingPrice = (appointment.Services.Sum(x => x.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price) - appointment.Services.Sum(x => x.Service.Prepayment)).ToString(TypeUtility.CommaFormatted),
                Services = appointment.Services.ToFindViewModel(),
                Payments = appointment.Payments.ToFindViewModel(),
                PaymentDetials = paymentDetials
            };
        }

        public static List<AppointmentFindAllByDateViewModel> ToFindAllByDateViewModel(this IQueryable<AppointmentModel> appointments)
        {
            return appointments.Select(appointment => new AppointmentFindAllByDateViewModel
            {
                Id = appointment.Id,
                CustomerFullName = $"{appointment.CustomerProfile.Name} {appointment.CustomerProfile.LastName}",
                CustomerId = appointment.CustomerProfileId,
                CreateDateTime = appointment.CreateDateTime.ToShortPersianDateTime(),
                Date = appointment.Date.ToPersianDate(),
                Time = appointment.Time.ToString(TypeUtility.HoursAndMinutes),
                FollowingCode = appointment.FollowingCode,
                IsDone = appointment.IsDone,
                Services = appointment.Services.ToFindAllViewModel(),
            }).ToList();
        }

        public static List<AppointmentDiscountViewModel> ToFindAllDiscountViewModel(this ICollection<AppointmentDiscountModel> discounts)
        {
            if (discounts == null)
                return new List<AppointmentDiscountViewModel>();

            return discounts.Select(x => new AppointmentDiscountViewModel
            {
                Id = x.Id,
                Amount = x.Amount.ToString(TypeUtility.CommaFormatted),
                CreateDateTime = x.CreateDateTime.ToShortPersianDateTime(),
            }).ToList();
        }

        public static List<AppointmentCalendarEventViewModel> ToFindAllByStartEndDateViewModel(this IQueryable<AppointmentModel> appointments)
        {
            //fc-event-danger fc-event-solid-warning
            //fc-event-solid-danger fc-event-light 
            //fc-event-success
            //fc-event-light fc-event-solid-primary
            return appointments.Select(appointment => new AppointmentCalendarEventViewModel
            {
                Id = appointment.Id,
                Start = appointment.Date.ToString(),
                Title = $"{appointment.Time.ToString(TypeUtility.HoursAndMinutes)} | {appointment.CustomerProfile.Name} {appointment.CustomerProfile.LastName}",
                ClassName = GenerateEventClassName(appointment),
                Description = "",
                End = "",
                Url = ""
            }).ToList();
        }

        public static AppointmentDetailViewModel ToFindDetailByCustomerViewModel(this AppointmentModel appointment)
        {
            return new AppointmentDetailViewModel
            {
                AppointmentId = appointment.Id,
                FollowingCode=appointment.FollowingCode,
                AppointmentCreateDate = appointment.CreateDateTime.ToPersianDate(),
                AppointmentDate = appointment.Date.ToPersianDate(),
                AppointmentTime = appointment.Time.ToString(TypeUtility.HoursAndMinutes),
                CustomerFullName = $"{appointment.CustomerProfile?.Name} {appointment.CustomerProfile?.LastName}",
                CustomerMemberCode = appointment.CustomerProfile?.MemberCode,
                CustomerMobile = appointment.CustomerProfile?.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile)?.Value,
                Services = appointment.Services.Where(x => !x.IsDeleted).Select(appointmentService => new AppointmentDetailServiceViewModel
                {
                    AppointmentServiceId = appointmentService.Id,
                    PersonnelFullName = $"{appointmentService.PersonnelProfile?.Name} {appointmentService.PersonnelProfile?.LastName}",
                    ServicePrice = appointmentService.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault()?.Price > 0 ?
                                                                  appointmentService.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault()?.Price.ToString(TypeUtility.CommaFormatted)
                                                                : appointmentService.AppointmentServiceDetails.Sum(x => x.TotalPrice).ToString(TypeUtility.CommaFormatted),
                    HasServiceDetails = appointmentService.Service.Details.Count > 0,
                    ServiceDetails = appointmentService.AppointmentServiceDetails.Count > 0 ? appointmentService.AppointmentServiceDetails.Select(x => new AppointmentServiceDetailViewModel
                    {
                        AppointmentServiceDetailId = x.Id,
                        TotalPrice = x.TotalPrice.ToString(TypeUtility.CommaFormatted)
                    }).ToList() : new List<AppointmentServiceDetailViewModel>(),
                    ServiceId = appointmentService.ServiceId,
                    ServiceTitle = appointmentService.Service.Title,
                    IsDone = appointmentService.IsDone

                }).ToList(),
                TotalPrice = (appointment.Services.Sum(x => x.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price) +
                              appointment.Services.Sum(x => x.AppointmentServiceDetails.Sum(x => x.TotalPrice))).ToString(TypeUtility.CommaFormatted),
                TotalPrepaymentPrice = appointment.Services.Sum(x => x.Service.Prepayment).ToString(TypeUtility.CommaFormatted),
                TotalDiscountPrice = appointment.Discounts.Sum(x => x.Amount).ToString(TypeUtility.CommaFormatted),
                PaidPrepaymentPrice = appointment.Payments.Sum(x => x.Amount).ToString(TypeUtility.CommaFormatted),
                FinalPayPrice = (((appointment.Services.Sum(x => x.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price) +
                                  appointment.Services.Sum(x => x.AppointmentServiceDetails.Sum(x => x.TotalPrice))) - appointment.Payments.Sum(x => x.Amount)) - appointment.Discounts.Sum(x => x.Amount)).ToString(TypeUtility.CommaFormatted),
                Payments = appointment.Payments.Select(p => new AppointmentPaymentViewModel
                {
                    Id = p.Id,
                    Amount = p.Amount.ToString(TypeUtility.CommaFormatted),
                    CreateDateTime = p.CreateDateTime.ToShortPersianDateTime()
                }).ToList(),
                IsDelete = appointment.IsDeleted,
                IsDone = appointment.IsDone,
                IsCanceled = appointment.IsCanceled,

            };
        }

        private static string GenerateEventClassName(AppointmentModel appointment)
        {
            var appointmentDateTime = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day, appointment.Time.Hours, appointment.Time.Minutes, 0);
            var nowDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, 0);
            if (appointmentDateTime < nowDateTime)//passed
            {
                if (appointment.IsCanceled)
                    return "fc-event-info fc-event-light-warning";
                else if (appointment.IsDone)
                    return "fc-event-info fc-event-light-green";
                else
                    return "fc-event-info fc-event-gray";
            }
            else if (appointmentDateTime == nowDateTime)//now
            {
                if (appointment.IsCanceled)
                    return "fc-event-info fc-event-solid-warning";
                else if (appointment.IsDone)
                    return "fc-event-light fc-event-solid-green";
                else
                    return "fc-event-info fc-event-solid-green";
            }
            else//not passed
            {
                if (appointment.IsCanceled)
                    return "fc-event-info fc-event-solid-warning";
                else if (appointment.IsDone)
                    return "fc-event-light fc-event-solid-green";
                else
                    return "fc-event-danger fc-event-light";
            }

        }

    }
}
