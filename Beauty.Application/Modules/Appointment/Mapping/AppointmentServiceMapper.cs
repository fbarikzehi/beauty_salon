using Beauty.Application.Modules.Appointment.ViewModel;
using Beauty.Model.Appointment;
using Common.Crosscutting.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Appointment.Mapping
{
    public static class AppointmentServiceMapper
    {
        //public static AppointmentServiceModel ToCreateModel(this AppointmentServiceViewModel appointmentService)
        //{
        //    return new AppointmentServiceModel
        //    {
        //        ServiceId = appointmentService.ServiceId,
        //        PersonnelProfileId = string.IsNullOrEmpty(appointmentService.PersonnelId) ? null : (Guid?)new Guid(appointmentService.PersonnelId),
        //    };
        //}
        public static List<AppointmentServiceFindViewModel> ToFindAllViewModel(this ICollection<AppointmentServiceModel> appointmentServices)
        {
            return appointmentServices.Select(appointmentService => new AppointmentServiceFindViewModel
            {
                Id = appointmentService.Id,
                ServiceId = appointmentService.ServiceId,
                ServiceName = appointmentService.Service.Title,
                IsDone = appointmentService.IsDone,
            }).ToList();
        }

        public static List<AppointmentServiceViewModel> ToFindViewModel(this ICollection<AppointmentServiceModel> appointmentServices)
        {
            return appointmentServices.Select(appointmentService => new AppointmentServiceViewModel
            {
                AppointmentServiceId = appointmentService.Id,
                ServiceId = appointmentService.ServiceId,
                ServiceTitle = appointmentService.Service.Title,
                PersonnelFullname = $"{appointmentService.PersonnelProfile.Name} {appointmentService.PersonnelProfile.LastName}",
                Price = appointmentService.Service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price.ToString(TypeUtility.CommaFormatted),
                Prepayment = appointmentService.Service.Prepayment.ToString(TypeUtility.CommaFormatted),
                IsDone = appointmentService.IsDone,
                DoneAction = !appointmentService.IsDone,
            }).ToList();
        }
    }
}
