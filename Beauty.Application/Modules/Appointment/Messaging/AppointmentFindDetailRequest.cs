using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class AppointmentFindDetailRequest : RequestBase
    {
        public Guid CustomerId { get; set; }
        public string Date { get; set; }
    }
}
