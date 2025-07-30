using Beauty.Application.Datatable;
using Beauty.Application.Modules.Account.Role.Implementation;
using Beauty.Application.Modules.Account.User.Implementation;
using Beauty.Application.Modules.Account.User.ViewModel;
using Beauty.Application.Modules.Line.Implementation;
using Beauty.Application.Modules.Personnel.Implementation;
using Beauty.Application.Modules.Personnel.Messaging;
using Beauty.Application.Modules.Personnel.ViewModel;
using Beauty.Application.Modules.Salon.Implementation;
using Beauty.Application.Modules.Service.Implementation;
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
    public class PersonnelController : BaseController
    {
        private readonly IServiceService _serviceService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPersonnelService _personnelService;
        private readonly ISalonService _salonService;
        private readonly ILineService _lineService;
        public PersonnelController(IServiceService serviceService, IPersonnelService personnelService, IUserService userService, IRoleService roleService, ISalonService salonService, ILineService lineService)
        {
            _serviceService = serviceService;
            _personnelService = personnelService;
            _userService = userService;
            _roleService = roleService;
            _salonService = salonService;
            _lineService = lineService;
        }

        public IActionResult Index()
        {
            var viewModel = new PersonnelIndexViewModel
            {
                Headers = new List<DataGridHeader>
                {
                    new DataGridHeader{Title="ردیف"},
                    new DataGridHeader{Title="نام/نام خانودگی"},
                    new DataGridHeader{Title="موبایل"},
                    new DataGridHeader{Title="کد پرسنل"},
                    new DataGridHeader{Title="نام کاربری"},
                    new DataGridHeader{Title="نوع همکاری"},
                    new DataGridHeader{Title="عملیات ها"},
                }
            };
            return View("Index", viewModel);
        }

        public JsonResult GetData(FindAllByPagingRequest request)
        {
            var response = _personnelService.FindAllByPaging(request).Result;
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
            return View("Modify", viewModel);
        }

        public PartialViewResult GetProfile(Guid pId)
        {
            var viewModel = new PersonnelProfileModifyViewModel();
            if (pId != Guid.Empty)
            {
                viewModel = _personnelService.FindByIdModify(new FindByIdModifyRequest { Id = pId }).Result.Entity;
            }
            return PartialView("_Profile", viewModel);
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
        public JsonResult DeleteContact(DeleteContactRequest request)
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
            return PartialView("_Account", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult CreateAccount(PersonnelAccountViewModel viewModel)
        {
            var role = _roleService.FindByTitle(new Application.Modules.Account.Role.Messaging.FindByTitleRequest { Title = "Personnel" }).Result;

            var personnel = _personnelService.FindById(new FindByIdRequest { Id = viewModel.PersonnelId }).Result.Entity;
            var randomUsername = $"{RandomUtility.GetRandomShortString(1000, 9999, 4)}{personnel.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile)?.Value.Substring(7, 4)}";
            var user = _userService.Create(new Application.Modules.Account.User.Messaging.CreateRequest
            {
                Entity = new UserCreateViewModel
                {
                    Username = string.IsNullOrEmpty(viewModel.Username) ? randomUsername : viewModel.Username,
                    Password = string.IsNullOrEmpty(viewModel.Username) ? randomUsername : viewModel.Password,
                    RoleId = role.Entity.Id
                }
            });
            if (user.Result.IsSuccess)
            {
                var response = _personnelService.CreateAccount(new CreateAccountRequest { Id = user.Result.Id, PersonnelId = viewModel.PersonnelId });
                if (!response.Result.IsSuccess)
                    _userService.Delete(new Application.Modules.Account.User.Messaging.DeleteRequest { Id = user.Result.Id });

                return Json(response.Result);
            }

            return Json(user.Result);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UpdateAccount(PersonnelAccountViewModel viewModel)
        {
            var role = _roleService.FindByTitle(new Application.Modules.Account.Role.Messaging.FindByTitleRequest { Title = "Personnel" }).Result;
            var response = _userService.Update(new Application.Modules.Account.User.Messaging.UpdateRequest
            {
                Entity = new UserUpdateViewModel
                {
                    Password = viewModel.Password,
                    Username = viewModel.Username,
                    Id = viewModel.UserId,
                    RoleId = role?.Entity.Id
                }
            });
            return Json(response.Result);
        }

        public PartialViewResult GetServices(Guid pId)
        {
            var viewModel = new PersonnelServicesViewModel { PersonnelId = pId, Lines = new List<Application.Modules.Line.ViewModel.LineViewModel>() };
            if (pId != Guid.Empty)
            {
                viewModel.Services = _personnelService.FindAllServices(new FindAllUpdateServicesRequest { Id = pId }).Result.Data.ToList();

                var personnel = _personnelService.FindById(new FindByIdRequest { Id = pId }).Result.Entity;
                var lines = _lineService.FindAll(new Application.Modules.Line.Messaging.FindAllRequest()).Result.Data;
                if (lines != null)
                    viewModel.Lines = lines.Select(x => new Application.Modules.Line.ViewModel.LineViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Selected = personnel.Lines.Any(y => y.LineId == x.Id)
                    }).ToList();

            }
            return PartialView("_Services", viewModel);
        }

        public JsonResult GetServicesAsJson(Guid pId)
        {
            var response = _personnelService.FindAllServices(new FindAllUpdateServicesRequest { Id = pId });
            return Json(response.Result);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UpdateServices(PersonnelServicesViewModel viewModel)
        {
            var response = _personnelService.UpdateServices(new UpdateServicesRequest { Entity = viewModel });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult AddService(PersonnelServicesViewModel viewModel)
        {
            var response = _personnelService.AddService(new UpdateServicesRequest { Entity = viewModel });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult UpdateLine(UpdateLineRequest request)
        {
            var response = _personnelService.UpdateLine(request);
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DeleteService(PersonnelServicesViewModel viewModel)
        {
            var response = _personnelService.DeleteService(new UpdateServicesRequest { Entity = viewModel });
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
            return PartialView("_Financial", viewModel);
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

            return PartialView("_WorkingTime", viewModel);
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

        public JsonResult GetAll()
        {
            var response = _personnelService.FindAllSelect(new FindAllSelectRequest());
            return Json(response.Result);
        }

    }
}