using Common.Crosscutting.Enum;

namespace Beauty.Application.Modules.Account.Role.ViewModel
{
    public class RolePermissionActionViewModel
    {
        public PermissionActionTypeEnum ActionType { get; set; }
        public short ActionId { get; set; }
        public string ActionTitle { get; set; }
        public bool Selected { get; set; }
        public string Checked { get;  set; }
    }
}
