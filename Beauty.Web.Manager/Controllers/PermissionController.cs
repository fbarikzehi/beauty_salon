using Beauty.Application.Modules.Account.Permission.Implementation;
using Beauty.Application.Modules.Account.Permission.Messaging;
using Beauty.Application.Modules.Account.Role.Implementation;
using Beauty.Application.Modules.Account.Role.Messaging;
using Beauty.Application.Modules.Account.Role.ViewModel;
using Common.Crosscutting.Enum;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class PermissionController : BaseController
    {
        private IRoleService _roleService;
        private IPermissionService _permissionService;

        public PermissionController(IRoleService roleService, IPermissionService permissionService)
        {
            _roleService = roleService;
            _permissionService = permissionService;
        }

        public IActionResult Index()
        {
            var viewModel = new RolePermissionIndexViewModel
            {
                Roles = new SelectList(_roleService.FindAll(new FindAllRequest { Type = RoleTypeEnum.Web }).Result.Data, "Id", "PersianTitle")
            };
            return View("Index", viewModel);
        }

        public JsonResult FindAllModify(short roleId)
        {
            var permissions = _permissionService.FindAll(new PermissionFindAllRequest()).Result;
            var response = _roleService.RolePermissionUpdateFindAll(new RolePermissionUpdateFindAllRequest { Id = roleId, Permissions = permissions.Data }).Result;
            return Json(response);
        }

        [HttpPost]
        public JsonResult Update(RolePermissionUpdateRequest request)
        {
            var response = _roleService.RolePermissionUpdate(request);
            return Json(response.Result);
        }
    }
}
