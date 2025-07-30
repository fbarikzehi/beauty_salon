using System.Collections.Generic;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentDetailServiceViewModel
    {
        public int AppointmentServiceId { get; set; }
        public string PersonnelFullName { get; set; }
        public short ServiceId { get; set; }
        public string ServiceTitle { get; set; }
        public string ServicePrice { get; set; }
        public bool? IsDone { get; set; }
        public bool HasServiceDetails { get; set; }
        public List<AppointmentServiceDetailViewModel> ServiceDetails { get; set; }
    }
}
