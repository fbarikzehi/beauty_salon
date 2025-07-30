using Common.Application.MessagingBase;
using Common.Crosscutting.Enum;

namespace Beauty.Application.Modules.Account.Role.Messaging
{
    public class FindAllRequest : RequestBase
    {
        public RoleTypeEnum? Type { get; set; }
    }
}
