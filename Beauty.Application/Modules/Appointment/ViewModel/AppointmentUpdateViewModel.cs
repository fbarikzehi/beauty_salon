using System;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentUpdateViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerProfileId { get; set; }
        public string CustomerProfileName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool IsDone { get; set; }
        public string CreateDateTime { get; set; }
        public List<AppointmentServiceViewModel> Services { get; set; }
    }
}
