using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class AddPaymentRequest : RequestBase
    {
        public Guid AppointmentId { get; set; }
        public string Amount { get; set; }
    }
}
