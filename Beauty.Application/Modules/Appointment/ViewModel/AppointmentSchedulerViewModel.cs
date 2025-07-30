using System;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentSchedulerViewModel
    {
        public Guid PersonnelId { get; set; }
        public string PersonnelFullName { get; set; }

        public List<AppointmentCardViewModel> Cards { get; set; }

    }
}
