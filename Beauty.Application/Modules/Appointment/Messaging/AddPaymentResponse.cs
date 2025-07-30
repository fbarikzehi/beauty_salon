using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class AddPaymentResponse : ResponseBase
    {
        public long Id { get; set; }
        public string CreateDateTime { get; set; }
    }
}
