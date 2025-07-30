using Beauty.Application.Datatable;
using Beauty.Application.Modules.Account.Role.Implementation;
using Beauty.Application.Modules.Account.User.Implementation;
using Beauty.Application.Modules.Account.User.ViewModel;
using Beauty.Application.Modules.Personnel.Implementation;
using Beauty.Application.Modules.Personnel.Messaging;
using Beauty.Application.Modules.Personnel.ViewModel;
using Beauty.Application.Modules.Salon.Implementation;
using Beauty.Application.Modules.Service.Implementation;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Manager.Controllers
{
    [Authorize(Policy = "Manager")]
    public class PersonnelController : BaseController
    {
        private readonly IServiceService _serviceService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPersonnelService _personnelService;
        private readonly ISalonService _salonService;

        public PersonnelController(IServiceService serviceService, IPersonnelService personnelService, IUserService userService, IRoleService roleService,ISalonService salonService)
        {
            _serviceService = serviceService;
            _personnelService = personnelService;
            _userService = userService;
            _roleService = roleService;
            _salonService = salonService;
        }

        public IActionResult Index()
        {
            var viewModel = new PersonnelIndexViewModel { TotalPersonnel = _personnelService.FindCountAll(new FindCountAllRequest()).Result.Count };
            return View("Index", viewModel);
        }

        [HttpPost]
        public JsonResult GetData()
        {
            var response = _personnelService.FindAllByPaging(new FindAllByPagingRequest { Entity = HttpContext.GetDatatableParameters() }).Result.Entity;
            return Json(response);
        }

        [HttpPost]
        public IActionResult Modify(Guid personnelId)
        {
            var viewModel = new PersonnelModifyViewModel { PersonnelId = personnelId };
            var personnel = _personnelService.FindById(new FindByIdRequest { Id = personnelId }).Result.Entity;
            if (personnel?.User != null)
            {
                viewModel.PersonnelCode = personnel.Code;
            }
            return View("Modify/Modify", viewModel);
        }

        public PartialViewResult GetProfile(Guid pId)
        {
            var viewModel = new PersonnelProfileModifyViewModel();
            if (pId != Guid.Empty)
            {
                viewModel = _personnelService.FindByIdModify(new FindByIdModifyRequest { Id = pId }).Result.Entity;
            }
            return PartialView("Modify/_Profile", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult CreateProfile(PersonnelProfileModifyViewModel viewModel)
        {
            var response = _personnelService.Create(new CreateRequest { Entity = viewModel });
            return Json(response.Result);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UpdateProfile(PersonnelProfileModifyViewModel viewModel)
        {
            var response = _personnelService.Update(new UpdateRequest { Entity = viewModel });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DeleteProfileContact(DeleteContactRequest request)
        {
            var response = _personnelService.DeleteContact(request);
            return Json(response.Result);
        }

        public PartialViewResult GetAccount(Guid pId)
        {
            var viewModel = new PersonnelAccountViewModel();
            if (pId != Guid.Empty)
            {
                var personnel = _personnelService.FindById(new FindByIdRequest { Id = pId }).Result.Entity;
                if (personnel?.User != null)
                {
                    viewModel.UserId = personnel.User.Id;
                    viewModel.Username = personnel.User.Username;
                }
                viewModel.PersonnelId = pId;
            }
            return PartialView("Modify/_Account", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult CreateAccount(PersonnelAccountViewModel viewModel)
        {
            var role = _roleService.FindByTitle(new Application.Modules.Account.Role.Messaging.FindByTitleRequest { Title = "Personnel" }).Result;
            var user = _userService.Create(new Application.Modules.Account.User.Messaging.CreateRequest { Entity = new UserCreateViewModel { Username = viewModel.Username, Password = viewModel.Password, RoleId = role.Entity.Id } });
            var response = _personnelService.CreateAccount(new CreateAccountRequest { Id = user.Result.Id, PersonnelId = viewModel.PersonnelId });

            return Json(user.Result);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UpdateAccount(PersonnelAccountViewModel viewModel)
        {
            var response = _userService.Update(new Application.Modules.Account.User.Messaging.UpdateRequest
            {
                Entity = new UserUpdateViewModel
                {
                    Password = viewModel.Password,
                    Username = viewModel.Username,
                    Id = viewModel.UserId
                }
            });
            return Json(response.Result);
        }

        public PartialViewResult GetServices(Guid pId)
        {
            var viewModel = new PersonnelServicesViewModel { PersonnelId = pId };
            if (pId != Guid.Empty)
            {
                viewModel.Services = _personnelService.FindAllServices(new FindAllUpdateServicesRequest { Id = pId }).Result.Data.ToList();
            }
            return PartialView("Modify/_Services", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UpdateServices(PersonnelServicesViewModel viewModel)
        {
            var response = _personnelService.UpdateServices(new UpdateServicesRequest { Entity = viewModel });
            return Json(response.Result);
        }

        public PartialViewResult GetFinancial(Guid pId)
        {
            var viewModel = new PersonnelFinancialViewModel { PersonnelId = pId };
            var personnel = _personnelService.FindById(new FindByIdRequest { Id = pId }).Result.Entity;
            if (personnel != null)
            {
                viewModel.CooperationType = personnel.CooperationType;
            }
            return PartialView("Modify/_Financial", viewModel);
        }

        public JsonResult GetServicePercentageData(Guid pId)
        {
            var response = _personnelService.FindAllPercentageServices(new FindAllPercentageServicesRequest { Id = pId }).Result.Data.ToList();
            return Json(response);
        }

        public JsonResult GetSalary(Guid pId)
        {
            var personnel = _personnelService.FindById(new FindByIdRequest { Id = pId }).Result.Entity;
            return Json(personnel.Salary);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UpdateFinancial(PersonnelFinancialViewModel viewModel)
        {
            var response = _personnelService.UpdateFinancial(new UpdateFinancialRequest { Entity = viewModel });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult Delete(Guid pId)
        {
            var response = _personnelService.DeleteById(new DeleteByIdRequest { Id = pId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DeleteRange(List<PersonnelDeleteByIdRangeViewModel> viewModel)
        {
            var response = _personnelService.DeleteRangeById(new DeleteRangeByIdRequest { Entities = viewModel });
            return Json(response.Result);
        }

        public PartialViewResult GetWorkingTime(Guid pId)
        {
            var viewModel = new List<PersonnelWorkingTimeViewModel>();
            var personnelWorkingTimes = _personnelService.FindAllUpdateWorkingTime(new FindAllUpdateWorkingTimeRequest { Id = pId }).Result.Data.ToList();
            if (personnelWorkingTimes != null)
            {
                viewModel = personnelWorkingTimes;
            }
            var salon = _salonService.Find(new Application.Modules.Salon.Messaging.FindRequest()).Result.Entity;
            ViewBag.OpeningTime = salon.OpeningTime;
            ViewBag.ClosingTime = salon.ClosingTime;

            return PartialView("Modify/_WorkingTime", viewModel);
        }

        [HttpPost]
        public JsonResult UpdateWorkingTime(PersonnelWorkingTimeViewModel viewModel)
        {
            var response = _personnelService.UpdateWorkingTime(new UpdateWorkingTimeRequest { Entity = viewModel });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult UpdateRangeWorkingTime(List<PersonnelWorkingTimeViewModel> viewModel)
        {
            var response = _personnelService.UpdateRangeWorkingTime(new UpdateRangeWorkingTimeRequest { Entities = viewModel });
            return Json(response.Result);
        }

    }
}