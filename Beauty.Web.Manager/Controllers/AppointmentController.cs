using Beauty.Application.Modules.Appointment.Implementation;
using Beauty.Application.Modules.Appointment.Messaging;
using Beauty.Application.Modules.Appointment.ViewModel;
using Beauty.Application.Modules.Personnel.Implementation;
using Beauty.Application.Modules.Salon.Implementation;
using Common.Crosscutting.Utility;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
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

        public IActionResult Index()
        {
            var viewModel = new AppointmentCreateViewModel();
            var salon = _salonService.Find(new Application.Modules.Salon.Messaging.FindRequest()).Result.Entity;
            if (salon != null)
            {
                viewModel.DefaultCreateDateTime = DateTime.Now.Date.ToPersianDate();
                viewModel.SalonClosingTime = salon.ClosingTime;
                viewModel.SalonOpeningTime = salon.OpeningTime;
                viewModel.SalonId = salon.Id;
                viewModel.Personnels = _personnelService.FindAllSelect(new Application.Modules.Personnel.Messaging.FindAllSelectRequest()).Result.SelectList;

                //var spOpeningTime = salon.OpeningTime.Split(':');
                //var spClosingTime = salon.ClosingTime.Split(':');
                //var OpenTime = new TimeSpan(new TimeSpan(int.Parse(spClosingTime[0]), int.Parse(spClosingTime[1]), 0).Ticks -
                //        new TimeSpan(int.Parse(spOpeningTime[0]), int.Parse(spOpeningTime[1]), 0).Ticks);
                //viewModel.TimeFractionCount = (int)OpenTime.TotalMinutes / 30;
            }
            return View("Index", viewModel);
        }

        public JsonResult GetEvents(string date, short days)
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToPersianDate();
                days = 0;
            }
            string newDate = date.AddDaysPersianDate(days);
            return Json(new { date = newDate });
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


        public JsonResult GetSchedulerData(AppointmentFindAllSchedulerRequest request)
        {
            var response = _appointmentService.FindAllScheduler(request);
            return Json(response.Result);
        }

        public JsonResult GetCustomerDetails(Guid customerId, string date)
        {
            var response = _appointmentService.FindDetailByCustomer(new AppointmentFindDetailRequest { CustomerId = customerId, Date = date });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult Cancel(Guid appointmentId)
        {
            var response = _appointmentService.Cancel(new CancelRequest { Id = appointmentId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult Done(Guid appointmentId)
        {
            var response = _appointmentService.Done(new DoneRequest { Id = appointmentId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult Delete(Guid appointmentId)
        {
            var response = _appointmentService.Delete(new DeleteRequest { Id = appointmentId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DoneService(Guid appointmentId, int appointmentServiceId)
        {
            var response = _appointmentService.DoneService(new DoneServiceRequest { AppointmentServiceId = appointmentServiceId, Appointment = appointmentId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DeleteService(int appointmentServiceId)
        {
            var response = _appointmentService.DeleteService(new DeleteServiceRequest { Id = appointmentServiceId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult AddPayment(Guid appointmentId, string payamount)
        {
            var response = _appointmentService.AddPayment(new AddPaymentRequest { AppointmentId = appointmentId, Amount = payamount });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DeletePayment(long paymentId)
        {
            var response = _appointmentService.DeletePayment(new DeletePaymentRequest { Id = paymentId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult ChangeServicePersonnel(ChangeServicePersonnelRequest request)
        {
            var response = _appointmentService.ChangeServicePersonnel(request);
            return Json(response.Result);
        }

        public JsonResult GetAppointmentServiceDetails(int appointmentServiceId)
        {
            var response = _appointmentService.AppointmentServiceDetailFindAll(new AppointmentServiceDetailFindAllRequest { Id = appointmentServiceId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult ModifyServiceDetails(AppointmentServiceDetailModifyRequest request)
        {
            var response = _appointmentService.AppointmentServiceDetailModify(request);
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult AddDiscount(Guid appointmentId, string amount)
        {
            var response = _appointmentService.AddDiscount(new AddDiscountRequest { AppointmentId = appointmentId, Amount = amount });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DeleteDiscount(int discountId)
        {
            var response = _appointmentService.DeleteDiscount(new DeleteDiscountRequest { Id = discountId });
            return Json(response.Result);
        }

        public JsonResult GetAllAppointmentDiscount(Guid appointmentId)
        {
            var response = _appointmentService.AppointmentDiscountFindAll(new AppointmentDiscountFindAllRequest { Id = appointmentId });
            return Json(response.Result);
        }

    }
}
