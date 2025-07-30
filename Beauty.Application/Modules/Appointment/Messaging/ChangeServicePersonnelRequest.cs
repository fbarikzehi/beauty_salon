using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class ChangeServicePersonnelRequest : RequestBase
    {
        public Guid  PersonnelId { get; set; }
        public Guid AppointmentId { get; set; }
        public int AppointmentServiceId { get; set; }
    }
}
