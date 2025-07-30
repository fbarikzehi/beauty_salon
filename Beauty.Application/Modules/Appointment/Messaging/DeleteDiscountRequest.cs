using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class DeleteDiscountRequest : RequestBase
    {
        public int Id { get; set; }
    }
}
