using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Account.Role.Messaging
{
    public class RolePermissionUpdateRequest : RequestBase
    {
        public short RoleId { get; set; }
        public short PermissionId { get; set; }
        public short PermissionActionId { get; set; }
        public bool IsSelected { get; set; }
        public bool SelectedAll { get; set; }
    }
}
