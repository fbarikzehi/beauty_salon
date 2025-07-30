using Beauty.Application.Modules.Calendar.Implementation;
using Beauty.Application.Modules.Calendar.Messaging;
using Beauty.Application.Modules.Sms.Implementation;
using Beauty.Application.Modules.Sms.Messaging;
using Beauty.Application.Modules.Sms.ViewModel;
using Common.Crosscutting.Utility;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class SmsController : BaseController
    {
        private readonly ISmsService _smsService;
        private readonly ICalendarService _calendarService;

        public SmsController(ISmsService smsService, ICalendarService calendarService)
        {
            _smsService = smsService;
            _calendarService = calendarService;
        }
        public IActionResult Index()
        {
            var viewModel = new List<SmsMessageViewModel>();
            viewModel = _smsService.FindAll(new SmsFindAllRequest()).Result.Data;
            return View("Index", viewModel);
        }

        [HttpPost]
        public IActionResult Modify(int smsId)
        {
            var viewModel = _smsService.FindById(new SmsFindByIdRequest { Id = smsId }).Result.Entity;
            return View("Modify", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateMessage(SmsMessageViewModel viewModel)
        {
            var response = _smsService.Update(new SmsUpdateRequest { Entity = viewModel });
            return Json(response.Result);
        }

        public JsonResult GetCalendar(int year)
        {
            if (year == 0)
                year = DateTimeUtility.GetPersianYear();

            var response = _calendarService.FindByYear(new CalendarFindByYearRequest { Year = year });
            return Json(response.Result);
        }
    }
}
