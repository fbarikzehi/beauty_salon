using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Service.Messaging
{
    public class FindAllBySearchRequest : RequestBase
    {
        public string Title { get; set; }
    }
}
