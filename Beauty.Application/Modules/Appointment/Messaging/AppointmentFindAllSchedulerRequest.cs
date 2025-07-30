using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class AppointmentFindAllSchedulerRequest : RequestBase
    {
        public string Date { get; set; }

    }
}
