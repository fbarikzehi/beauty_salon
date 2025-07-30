using Beauty.Application.Modules.Service.Implementation;
using Beauty.Application.Modules.Service.Messaging;
using Beauty.Application.Modules.Service.ViewModel;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Manager.Controllers
{
    [Authorize(Policy = "Manager")]
    public class ServiceController : BaseController
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public IActionResult Index()
        {
            var viewModel = new ServiceIndexViewModel { };
            return View("Index", viewModel);
        }

        public JsonResult FindAllByPage(FindAllByPageRequest request)
        {
            var response = _serviceService.FindAllByPage(request);
            return Json(response.Result);
        }

        public IActionResult CreateRange()
        {
            var viewModel = new CreateRangeRequest();
            return View("CreateRange", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateRange(CreateRangeRequest request)
        {
            var response = _serviceService.CreateRange(request).Result;
            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CreateRequest request)
        {
            var response = _serviceService.Create(request);
            return Json(response.Result);
        }

        public IActionResult Update(short id)
        {
            var service = _serviceService.FindById(new FindByIdRequest { Id = id }).Result.Entity;
            var viewModel = new UpdateRequest
            {
                Entity = new ServiceUpdateViewModel
                {
                    Id = service.Id,
                    CurrentPrice = service.CurrentPrice,
                    CustomerMinScore = service.CustomerMinScore,
                    Prepayment = service.Prepayment,
                    Score = service.Score,
                    DurationMinutes = service.DurationMinutes,
                    IsActive = service.IsActive,
                    Title = service.Title,
                }
            };
            return View("Update", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(UpdateRequest request)
        {
            var response = _serviceService.Update(request);
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult Delete(DeleteRequest request)
        {
            var response = _serviceService.Delete(request);
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult ChangeActive(UpdateActiveRequest request)
        {
            var response = _serviceService.UpdateActive(request);
            return Json(response.Result);
        }

        public JsonResult FindAllBySearch(string val)
        {
            var response = _serviceService.FindAllBySearch(new FindAllBySearchRequest { Title = val }).Result;
            return Json(response);
        }
    }
}