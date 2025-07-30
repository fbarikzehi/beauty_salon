using Beauty.Application.Modules.Appointment.ViewModel;
using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class DoneServiceRequest
    {
        public int AppointmentServiceId { get; set; }
        public Guid Appointment { get; set; }
    }
}
