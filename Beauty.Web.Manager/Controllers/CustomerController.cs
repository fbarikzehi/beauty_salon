using Beauty.Application.Modules.Account.Role.Implementation;
using Beauty.Application.Modules.Account.User.Implementation;
using Beauty.Application.Modules.Account.User.Messaging;
using Beauty.Application.Modules.Account.User.ViewModel;
using Beauty.Application.Modules.Customer.Implementation;
using Beauty.Application.Modules.Customer.Messaging;
using Beauty.Application.Modules.Customer.ViewModel;
using Common.Application.ViewModelBase;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Utility;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public CustomerController(ICustomerService customerService, IUserService userService, IRoleService roleService)
        {
            _customerService = customerService;
            _userService = userService;
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            var viewModel = new CustomerIndexViewModel
            {
                Headers = new List<DataGridHeader>
                {
                    new DataGridHeader{Title="ردیف"},
                    new DataGridHeader{Title="نام/نام خانودگی"},
                    new DataGridHeader{Title="موبایل"},
                    new DataGridHeader{Title="کد مشتری"},
                    new DataGridHeader{Title="نام کاربری"},
                    new DataGridHeader{Title="عملیات ها"},
                }
            };
            return View("Index", viewModel);
        }

        public JsonResult GetData(FindAllByPagingRequest request)
        {
            var response = _customerService.FindAllByPaging(request).Result;
            return Json(response);
        }

        [HttpPost]
        public IActionResult Modify(Guid customerId)
        {
            var viewModel = new CustomerModifyViewModel { Id = customerId };
            var customer = _customerService.FindById(new FindByIdRequest { Id = customerId }).Result;
            if (customer.IsSuccess)
            {
                viewModel.Id = customer.Entity.Id;
                viewModel.Avatar = customer.Entity.Avatar;
                viewModel.Name = customer.Entity.Name;
                viewModel.LastName = customer.Entity.LastName;
                viewModel.Birthdate = customer.Entity.Birthdate;
                viewModel.User = customer.Entity.User;
                viewModel.Contacts = customer.Entity.Contacts;
                viewModel.MemberCode = customer.Entity.MemberCode;
            }
            return View("Modify", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult CreateProfile(CustomerModifyViewModel viewModel)
        {
            var role = _roleService.FindByTitle(new Application.Modules.Account.Role.Messaging.FindByTitleRequest { Title = "Customer" });
            if (!role.Result.IsSuccess)
                return Json(role.Result);

            var randomUsername = $"{RandomUtility.GetRandomShortString(1000, 9999, 4)}{viewModel.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile)?.Value.Substring(7, 4)}";
            var user = _userService.Create(new Application.Modules.Account.User.Messaging.CreateRequest
            {
                Entity = new UserCreateViewModel
                {
                    Username = string.IsNullOrEmpty(viewModel.User.Username) ? randomUsername : viewModel.User.Username,
                    Password = string.IsNullOrEmpty(viewModel.User.Username) ? randomUsername : viewModel.User.Password,
                    RoleId = role.Result.Entity.Id,
                    FullName = $"{viewModel.Name} {viewModel.LastName}"
                }
            });
            if (!user.Result.IsSuccess)
                return Json(user.Result);

            viewModel.UserId = user.Result.Id;
            var response = _customerService.Create(new Application.Modules.Customer.Messaging.CreateRequest { Entity = viewModel });
            if (!response.Result.IsSuccess)
                _userService.Delete(new DeleteRequest { Id = user.Result.Id });

            return Json(response.Result);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UpdateProfile(CustomerModifyViewModel viewModel)
        {
            var role = _roleService.FindByTitle(new Application.Modules.Account.Role.Messaging.FindByTitleRequest { Title = "Customer" }).Result;
            var user = _userService.Update(new Application.Modules.Account.User.Messaging.UpdateRequest
            {
                Entity = new UserUpdateViewModel
                {
                    Username = viewModel.User.Username,
                    Password = viewModel.User.Password,
                    Id = viewModel.User.Id,
                    RoleId = role?.Entity.Id
                }
            });
            if (!user.Result.IsSuccess)
                return Json(user.Result);

            var response = _customerService.Update(new Application.Modules.Customer.Messaging.UpdateRequest { Entity = viewModel });
            return Json(response.Result);
        }

        public JsonResult FindAllBySearch(string val)
        {
            var response = _customerService.FindAllBySearch(new FindAllBySearchRequest { FullName = val }).Result;
            return Json(response);
        }

        [HttpPost]
        public JsonResult Delete(Guid cId)
        {
            var response = _customerService.DeleteById(new DeleteByIdRequest { Id = cId });
            if (response.Result.IsSuccess && response.Result.UserId != null)
                _userService.Delete(new DeleteRequest { Id = (Guid)response.Result.UserId });

            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DeleteContact(DeleteContactRequest request)
        {
            var response = _customerService.DeleteContact(request);
            return Json(response.Result);
        }

        public JsonResult GetAllCheque(CustomerChequeFindAllByCreateDateRequest request)
        {
            var response = _customerService.ChequeFindAllByCreateDate(request);
            return Json(response.Result);
        }
    }
}
