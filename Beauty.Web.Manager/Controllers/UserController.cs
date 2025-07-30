using Beauty.Application.Modules.Account.Role.Implementation;
using Beauty.Application.Modules.Account.Role.Messaging;
using Beauty.Application.Modules.Account.User.Implementation;
using Beauty.Application.Modules.Account.User.Messaging;
using Beauty.Application.Modules.Account.User.ViewModel;
using Common.Application.ViewModelBase;
using Common.Crosscutting.Enum;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class UserController : BaseController
    {
        private IRoleService _roleService;
        private IUserService _userService;

        public UserController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var viewModel = new UserIndexViewModel
            {
                Headers = new List<DataGridHeader>
                {
                    new DataGridHeader{Title="ردیف"},
                    new DataGridHeader{Title="نام/نام خانودگی"},
                    new DataGridHeader{Title="نام کاربری"},
                    new DataGridHeader{Title="نقش"},
                    new DataGridHeader{Title="وضعیت مسدودیت"},
                    new DataGridHeader{Title="آخرین ورود"},
                    new DataGridHeader{Title="عملیات ها"},
                },
                Roles = new SelectList(_roleService.FindAll(new FindAllRequest { Type = RoleTypeEnum.Web }).Result.Data, "Id", "PersianTitle")
            };
            return View("Index", viewModel);
        }

        public JsonResult GetData(UserFindAllByPagingRequest request)
        {
            var response = _userService.FindAllByPaging(request).Result;
            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CreateRequest request)
        {
            var response = _userService.Create(request);
            return Json(response.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(UpdateRequest request)
        {
            var response = _userService.Update(request);
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult Delete(DeleteRequest request)
        {
            var response = _userService.Delete(request);
            return Json(response.Result);
        }

    }
}
