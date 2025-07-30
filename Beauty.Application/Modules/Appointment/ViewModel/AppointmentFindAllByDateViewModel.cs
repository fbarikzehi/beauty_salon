using System;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentFindAllByDateViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string FollowingCode { get; set; }
        public bool IsDone { get; set; }
        public string CreateDateTime { get; set; }
        public List<AppointmentServiceFindViewModel> Services { get; set; }
    }
}
