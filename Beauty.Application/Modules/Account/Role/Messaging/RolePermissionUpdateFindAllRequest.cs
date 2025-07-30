using Beauty.Application.Modules.Account.Permission.ViewModel;
using Common.Application.MessagingBase;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Account.Role.Messaging
{
    public class RolePermissionUpdateFindAllRequest : RequestIdBase<short>
    {
        public IEnumerable<PermissionViewModel> Permissions { get; set; }
    }
}
