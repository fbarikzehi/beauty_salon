using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Customer.Messaging
{
    public class FindAllBySearchRequest : RequestBase
    {
        public string FullName { get; set; }
    }
}
