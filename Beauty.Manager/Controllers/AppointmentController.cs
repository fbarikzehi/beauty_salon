using Beauty.Application.Modules.Appointment.Implementation;
using Beauty.Application.Modules.Appointment.Messaging;
using Beauty.Application.Modules.Appointment.ViewModel;
using Beauty.Application.Modules.Personnel.Implementation;
using Beauty.Application.Modules.Salon.Implementation;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Beauty.Manager.Controllers
{
    [Authorize(Policy = "Manager")]
    public class AppointmentController : BaseController
    {
        private readonly ISalonService _salonService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPersonnelService _personnelService;
        public AppointmentController(ISalonService salonService, IAppointmentService appointmentService, IPersonnelService personnelService)
        {
            _salonService = salonService;
            _appointmentService = appointmentService;
            _personnelService = personnelService;
        }

        public IActionResult QuickView()
        {
            var viewModel = new AppointmentQuickViewViewModel();
            var salon = _salonService.Find(new Application.Modules.Salon.Messaging.FindRequest()).Result.Entity;
            if (salon != null)
            {
                viewModel.SalonClosingTime = salon.ClosingTime;
                viewModel.SalonOpeningTime = salon.OpeningTime;
                viewModel.Personnels = _personnelService.FindAllSelect(new Application.Modules.Personnel.Messaging.FindAllSelectRequest()).Result.SelectList;
            }
            return View("QuickView", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(AppointmentCreateViewModel viewModel)
        {
            var response = _appointmentService.Create(new CreateRequest { Entity = viewModel });

            //var salon = _salonService.Find(new Application.Modules.Salon.Messaging.FindRequest()).Result.Entity;
            //var smsResult = SmsProvider.Send(string.Format(SmsMessageResource_fa.AppointmentCreate, salon.Title, viewModel.Date, viewModel.Time, response.Result.FollowingCode), viewModel.CustomerMobile, response.Result.Id.ToString());

            return Json(response.Result);
        }

        public JsonResult GetEvents(FindAllByStartEndDateRequest request)
        {
            var personnel = _appointmentService.FindAllByStartEndDate(request).Result.Data;
            return Json(personnel);
        }

        public JsonResult GetById(Guid appId)
        {
            var personnel = _appointmentService.FindById(new FindByIdRequest { Id = appId }).Result.Entity;
            return Json(personnel);
        }


        [HttpPost]
        public JsonResult Cancel(Guid appId)
        {
            var response = _appointmentService.Cancel(new CancelRequest { Id = appId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult Done(Guid appId)
        {
            var response = _appointmentService.Done(new DoneRequest { Id = appId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult Delete(Guid appId)
        {
            var response = _appointmentService.Delete(new DeleteRequest { Id = appId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DoneService(Guid appId, int sId)
        {
            var response = _appointmentService.DoneService(new DoneServiceRequest { AppointmentServiceId = sId, Appointment = appId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult AddPayment(Guid appId, string payamount)
        {
            var response = _appointmentService.AddPayment(new AddPaymentRequest { AppointmentId = appId, Amount = payamount });
            return Json(response.Result);
        }
    }
}
