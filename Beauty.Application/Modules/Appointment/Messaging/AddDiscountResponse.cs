using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class AddDiscountResponse : ResponseBase
    {
        public int Id { get; set; }
        public string CreateDateTime { get; set; }
    }
}
