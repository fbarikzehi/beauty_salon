using System;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerProfileId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerMobile { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string CreateDateTime { get; set; }
        public string TotalRemainingPrice { get; set; }
        public string TotalPrice { get; set; }
        public string TotalPrepayment { get; set; }
        public bool IsDone { get; set; }
        public List<AppointmentServiceViewModel> Services { get; set; }
        public List<AppointmentPaymentViewModel> Payments { get; set; }
        public IEnumerable<AppointmentPaymentDetialViewModel> PaymentDetials { get; set; }
    }
}
