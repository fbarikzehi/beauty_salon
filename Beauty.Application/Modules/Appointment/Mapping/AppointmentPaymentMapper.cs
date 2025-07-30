using Beauty.Application.Modules.Appointment.ViewModel;
using Beauty.Model.Appointment;
using Common.Crosscutting.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Appointment.Mapping
{
    public static class AppointmentPaymentMapper
    {
        public static List<AppointmentPaymentViewModel> ToFindViewModel(this ICollection<AppointmentPaymentModel> appointmentPayments)
        {
            return appointmentPayments.Select(appointmentPayment => new AppointmentPaymentViewModel
            {
                Amount = appointmentPayment.Amount.ToString(TypeUtility.CommaFormatted),
                CreateDateTime = appointmentPayment.CreateDateTime.ToShortPersianDateTime(),
            }).ToList();
        }
    }
}
