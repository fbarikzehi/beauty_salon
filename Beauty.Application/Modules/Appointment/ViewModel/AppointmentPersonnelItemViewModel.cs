using Common.Crosscutting.Enum;
using System;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentPersonnelItemViewModel
    {
        public int AppointmentServiceId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerAvatarUrl { get; set; }
        public string Time { get; set; }
        public string TotalRemainingPrice { get; set; }
        public string ServicePrice { get; set; }
        public string ServiceTile { get; set; }
        public string TotalPrepayment { get; set; }
        public AppointmentServiceStatusTypeEnum Status { get; set; }
        public Guid AppointmentId { get;  set; }
    }
}
