using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class FindAllByDateRequest : RequestBase
    {
        public string Date { get; set; }
    }
}
