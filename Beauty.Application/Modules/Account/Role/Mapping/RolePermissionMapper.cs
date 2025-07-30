using Beauty.Application.Modules.Account.Permission.ViewModel;
using Beauty.Application.Modules.Account.Role.ViewModel;
using Beauty.Model.Account.Role;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Account.Role.Mapping
{
    public static class RolePermissionMapper
    {
        public static List<RolePermissionUpdateViewModel> ToUpdateViewModel(this RoleModel role, IEnumerable<PermissionViewModel> permissions)
        {
            return permissions.Select(x => new RolePermissionUpdateViewModel
            {

                Id = x.Id,
                Title = x.Title,
                Icon = x.Icon,
                RolePermissionActions = x.Actions.ToUpdateActionViewModel(role),
                RoleSubPermissions = x.SubPermissions.Select(s => new RolePermissionViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Icon = s.Icon,
                    RolePermissionActions = s.Actions.ToUpdateActionViewModel(role),
                }).ToList(),
            }).ToList();
        }

        public static List<RolePermissionActionViewModel> ToUpdateActionViewModel(this List<PermissionActionViewModel> permissionActions, RoleModel role)
        {
            return permissionActions.Select(x => new RolePermissionActionViewModel
            {
                ActionId = x.Id,
                ActionTitle = EnumExtensions<PermissionActionTypeEnum>.GetPersianName(x.ActionType),
                Checked = role.RolePermissions.Any(y => y.RolePermissionActions.Any(a => a.PermissionActionId == x.Id)) ? "checked" : "",
                Selected = role.RolePermissions.Any(y => y.RolePermissionActions.Any(a => a.PermissionActionId == x.Id))
            }).ToList();
        }
    }
}
