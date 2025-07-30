using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class FindAllByPersonnelRequest : RequestIdBase<Guid>
    {
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
