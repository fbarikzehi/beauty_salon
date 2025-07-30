using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Account.Role.Messaging
{
    public class FindByTitleRequest : RequestBase
    {
        public string Title { get; set; }
    }
}
