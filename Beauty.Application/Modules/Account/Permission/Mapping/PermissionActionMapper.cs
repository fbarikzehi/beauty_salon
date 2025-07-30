using Beauty.Application.Modules.Account.Permission.ViewModel;
using Beauty.Model.Account.Permission;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Account.Permission.Mapping
{
    public static class PermissionActionMapper
    {
        public static List<PermissionActionViewModel> ToFindAllViewModel(this ICollection<PermissionActionModel> permissionActions)
        {
            return permissionActions.Select(x => new PermissionActionViewModel
            {
                Id = x.Id,
                ActionType = x.ActionType,
            }).ToList();
        }
    }
}
