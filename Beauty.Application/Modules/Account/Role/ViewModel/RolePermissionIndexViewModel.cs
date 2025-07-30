using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beauty.Application.Modules.Account.Role.ViewModel
{
    public class RolePermissionIndexViewModel
    {
        public SelectList Roles { get; set; }
        public short RoleId { get; set; }
    }
}
