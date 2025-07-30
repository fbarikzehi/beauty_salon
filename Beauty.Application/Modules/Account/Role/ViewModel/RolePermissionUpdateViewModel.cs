using System.Collections.Generic;

namespace Beauty.Application.Modules.Account.Role.ViewModel
{
    public class RolePermissionUpdateViewModel
    {
        public short Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public List<RolePermissionViewModel> RoleSubPermissions { get; set; }
        public List<RolePermissionActionViewModel> RolePermissionActions { get; set; }
    }
}
