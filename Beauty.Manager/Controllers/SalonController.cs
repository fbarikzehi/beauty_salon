using Beauty.Application.Modules.Salon.Implementation;
using Beauty.Application.Modules.Salon.Messaging;
using Beauty.Application.Modules.Salon.ViewModel;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Beauty.Manager.Controllers
{
    [Authorize(Policy = "Manager")]
    public class SalonController : BaseController
    {
        private readonly ISalonService _salonService;

        public SalonController(ISalonService salonService)
        {
            _salonService = salonService;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public PartialViewResult GetSalonInfo(Guid sId)
        {
            var viewModel = new SalonViewModel();
            viewModel = _salonService.Find(new FindRequest()).Result.Entity;
            if (viewModel.Contacts is null)
                viewModel.Contacts = new List<SalonContactViewModel>();

            return PartialView("_SalonInfo", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UpdateSalon(SalonViewModel viewModel)
        {
            var response = _salonService.Update(new UpdateRequest { Entity = viewModel });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DeleteContact(DeleteContactRequest request)
        {
            var response = _salonService.DeleteContact(request);
            return Json(response.Result);
        }

        public PartialViewResult GetWorkingDateTimes(Guid sId)
        {
            var viewModel = new SalonViewModel();
            viewModel = _salonService.Find(new FindRequest()).Result.Entity;

            return PartialView("_WorkingDateTimes", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult UpdateWorkingDateTimes(SalonViewModel viewModel)
        {
            var response = _salonService.UpdateWorkingDateTimes(new UpdateWorkingDateTimesRequest { Entity = viewModel });
            return Json(response.Result);
        }
    }
}
