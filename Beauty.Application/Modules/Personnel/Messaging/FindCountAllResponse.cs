using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Personnel.Messaging
{
    public class FindCountAllResponse : ResponseBase
    {
        public int Count { get; set; }
    }
}
