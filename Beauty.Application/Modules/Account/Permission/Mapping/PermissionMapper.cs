using Beauty.Application.Modules.Account.Permission.ViewModel;
using Beauty.Model.Account.Permission;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Account.Permission.Mapping
{
    public static class PermissionMapper
    {
        public static List<PermissionViewModel> ToFindAllViewModel(this IQueryable<PermissionModel> permissions)
        {
            return permissions.Select(x => new PermissionViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Icon = x.Icon,
                Url=x.Url,
                Area=x.Area,
                Controller=x.Controller,
                Action=x.Action,
                SubPermissions = x.SubPermissions.Select(s => new PermissionViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Icon = s.Icon,
                    Url = s.Url,
                    Area = s.Area,
                    Controller = s.Controller,
                    Action = s.Action,
                    Actions = s.Actions.ToFindAllViewModel(),
                }).ToList(),
                Actions = x.Actions.ToFindAllViewModel(),
            }).ToList();
        }
    }
}
