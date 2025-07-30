using Common.Crosscutting.Enum;

namespace Beauty.Application.Modules.Account.Permission.ViewModel
{
    public class PermissionActionViewModel
    {
        public short Id { get; set; }
        public PermissionActionTypeEnum ActionType { get; set; }
    }
}