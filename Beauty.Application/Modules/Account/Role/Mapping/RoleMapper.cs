using Beauty.Application.Modules.Account.Role.ViewModel;
using Beauty.Model.Account.Role;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Account.Role.Mapping
{
    public static class RoleMapper
    {
        public static RoleViewModel ToFindByTitleViewModel(this RoleModel m)
        {
            return new RoleViewModel
            {
                Id = m.Id,
                HomeUrl = m.HomeUrl,
                PersianTitle = m.PersianTitle,
                Title = m.Title,
            };
        }

        public static List<RoleViewModel> ToFindAllViewModel(this IQueryable<RoleModel> roles)
        {
            return roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                HomeUrl = x.HomeUrl,
                PersianTitle = x.PersianTitle,
                Title = x.Title,
            }).ToList();
        }
    }
}
