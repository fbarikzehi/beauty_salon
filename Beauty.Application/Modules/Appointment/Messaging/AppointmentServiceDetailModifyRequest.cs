using Beauty.Application.Modules.Appointment.ViewModel;
using Common.Application.MessagingBase;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class AppointmentServiceDetailModifyRequest : RequestBase
    {
        public int AppointmentServiceId { get; set; }
        public List<AppointmentServiceDetailModifyViewModel> Details { get; set; }
    }
}
