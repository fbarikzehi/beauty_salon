using Beauty.Application.Modules.Account.Permission.Implementation;
using Beauty.Application.Modules.Account.Permission.Messaging;
using Beauty.Application.Modules.Account.Role.Implementation;
using Beauty.Application.Modules.Account.Role.Messaging;
using Beauty.Application.Modules.Salon.Implementation;
using Beauty.Application.Modules.Setting.Implementation;
using Beauty.Application.ViewComponentViewModel;
using Beauty.Application.ViewModel;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty.Web.Manager.ViewComponents
{
    public class Menu : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly ISalonService _salonService;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Menu(ISettingService settingService,
                    ISalonService salonService,
                    IRoleService roleService,
                    IPermissionService permissionService,
                    IHttpContextAccessor httpContextAccessor)
        {
            _settingService = settingService;
            _salonService = salonService;
            _roleService = roleService;
            _permissionService = permissionService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                //var setting = _settingService.Find(new FindRequest()).Result.Entity;
                //var salon = _salonService.Find(new Application.Modules.Salon.Messaging.FindRequest()).Result.Entity;
                var viewModel = new MenuViewComponentViewModel { Menus = new List<MenuViewModel>() };

                var roleId = ClaimManager.GetByType(ClaimOptions.RoleIdClaimType, HttpContext.User.Claims);
                if (short.TryParse(roleId, out short _))
                {
                    var permissions = _permissionService.FindAll(new PermissionFindAllRequest()).Result;
                    var rolePermissions = _roleService.RolePermissionFindAll(new RolePermissionFindAllRequest { Id = short.Parse(roleId), Permissions = permissions.Data }).Result.Data;

                    foreach (var rolePermission in rolePermissions)
                    {
                        if ((string.IsNullOrEmpty(rolePermission.Url) && rolePermission.RoleSubPermissions.Count > 0) || !string.IsNullOrEmpty(rolePermission.Url))
                        {
                            var subMenus = new List<SubMenuViewModel>();
                            foreach (var item in rolePermission.RoleSubPermissions)
                                subMenus.Add(new SubMenuViewModel { Icon = item.Icon, Title = item.Title, Url = item.Url });

                            viewModel.Menus.Add(new MenuViewModel { Icon = rolePermission.Icon, Title = rolePermission.Title, Url = rolePermission.Url, SubMenus = subMenus });
                        }

                    }
                    //SalonTitle = salon.Title,
                    //SalonLogo = salon.Logo,
                    //Version = setting.Version,
                }
                return View(viewModel);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}
