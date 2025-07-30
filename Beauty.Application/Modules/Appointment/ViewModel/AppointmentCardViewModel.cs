using System;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentCardViewModel
    {
        public Guid AppointmentId { get; set; }
        public int AppointmentServiceId { get; set; }
        public short ServiceId { get; set; }
        public string ServiceTitle { get; set; }
        public short ServiceDurationMinutes { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public TimeSpan StartTimeTimeSpan { get; set; }
        public string FinishTime { get; set; }
        public TimeSpan FinishTimeTimeSpan { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerMemberCode { get; set; }
        public bool? Status { get; set; }
        public bool IsDone { get; set; }
    }
}
