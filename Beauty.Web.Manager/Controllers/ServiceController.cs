using Beauty.Application.Modules.Line.Implementation;
using Beauty.Application.Modules.Service.Implementation;
using Beauty.Application.Modules.Service.Messaging;
using Beauty.Application.Modules.Service.ViewModel;
using Common.Application.ViewModelBase;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class ServiceController : BaseController
    {
        private readonly IServiceService _serviceService;
        private readonly ILineService _lineService;

        public ServiceController(IServiceService serviceService,ILineService lineService)
        {
            _serviceService = serviceService;
            _lineService = lineService;
        }

        public IActionResult Index()
        {
            var viewModel = new ServiceIndexViewModel
            {
                Headers = new List<DataGridHeader>
                {
                    new DataGridHeader{Title="ردیف"},
                    new DataGridHeader{Title="عنوان"},
                    new DataGridHeader{Title="قیمت(تومان)"},
                    new DataGridHeader{Title="مقدار زمان"},
                    new DataGridHeader{Title="امتیاز"},
                    new DataGridHeader{Title="تعداد مشتریان"},
                    new DataGridHeader{Title="امتیاز کسب شده"},
                    new DataGridHeader{Title="عملیات ها"},
                },
                Lines=new SelectList(_lineService.FindAll(new Application.Modules.Line.Messaging.FindAllRequest()).Result.Data,"Id","Title"),
            };
            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CreateRequest request)
        {
            var response = _serviceService.Create(request);
            return Json(response.Result);
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

        public JsonResult GetData(FindAllByPageRequest request)
        {
            var response = _serviceService.FindAllByPage(request);
            return Json(response.Result);
        }

        public JsonResult GetAll()
        {
            var response = _serviceService.FindAll(new FindAllRequest());
            return Json(response.Result);
        }

        public JsonResult GetById(short id)
        {
            var response = _serviceService.FindById(new FindByIdRequest { Id = id });
            return Json(response.Result);
        }

        public JsonResult FindAllBySearch(string val)
        {
            var response = _serviceService.FindAllBySearch(new FindAllBySearchRequest { Title = val }).Result;
            return Json(response);
        }

        public JsonResult SearchAllByName(string val)
        {
            var response = _serviceService.SearchAllByName(new FindAllBySearchRequest { Title = val }).Result;
            return Json(response);
        }
    }
}