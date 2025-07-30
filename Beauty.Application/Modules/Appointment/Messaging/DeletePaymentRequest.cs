using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class DeletePaymentRequest : RequestBase
    {
        public long Id { get; set; }
    }
}
