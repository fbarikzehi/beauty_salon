using System;
using System.Collections.Generic;
using System.Text;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentDetailViewModel
    {
        public string CustomerFullName { get; set; }
        public string CustomerMemberCode { get; set; }
        public string CustomerMobile { get; set; }

        public Guid AppointmentId { get; set; }
        public string FollowingCode { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string AppointmentCreateDate { get; set; }

        public List<AppointmentDetailServiceViewModel> Services { get; set; }

        public string TotalPrice { get; set; }
        public string TotalPrepaymentPrice { get; set; }
        public string TotalDiscountPrice { get; set; }
        public string PaidPrepaymentPrice { get; set; }
        public string FinalPayPrice { get; set; }

        public List<AppointmentPaymentViewModel> Payments { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDone { get; set; }
        public bool IsCanceled { get; set; }
    }
}
