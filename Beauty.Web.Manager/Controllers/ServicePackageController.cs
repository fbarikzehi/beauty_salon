using Beauty.Application.Modules.Service.Implementation;
using Beauty.Application.Modules.Service.Messaging;
using Beauty.Application.Modules.Service.ViewModel;
using Common.Application.ViewModelBase;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class ServicePackageController : BaseController
    {
        private readonly IServiceService _serviceService;

        public ServicePackageController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public IActionResult Index()
        {
            var viewModel = new ServicePackageIndexViewModel
            {
                Headers = new List<DataGridHeader>
                {
                    new DataGridHeader{Title=""},
                    new DataGridHeader{Title="ردیف"},
                    new DataGridHeader{Title="عنوان"},
                    new DataGridHeader{Title="از تاریخ"},
                    new DataGridHeader{Title="تا تاریخ"},
                    new DataGridHeader{Title="تمدید تا تاریخ"},
                    new DataGridHeader{Title="قیمت تخفیف"},
                    new DataGridHeader{Title="عملیات ها"},
                },
            };
            return View("Index", viewModel);
        }

        public JsonResult GetData(ServicePackageFindByPagingRequest request)
        {
            var response = _serviceService.ServicePackageFindByPaging(request).Result;
            return Json(response);
        }

        public IActionResult Create()
        {
            var viewModel = new ServicePackageCreateRequest();
            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(ServicePackageCreateRequest request)
        {
            var response = _serviceService.CreateServicePackage(request);
            return Json(response.Result);
        }

        public IActionResult Update(int packageId)
        {
            var viewModel = new ServicePackageUpdateRequest();
            viewModel.Entity = _serviceService.ServicePackageFindById(new ServicePackageFindByIdRequest { Id = packageId }).Result.Entity;
            return View("Update", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(ServicePackageUpdateRequest request)
        {
            var response = _serviceService.UpdateServicePackage(request);
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult ServicePackageDelete(int packageId)
        {
            var response = _serviceService.ServicePackageDelete(new ServicePackageDeleteRequest { Id = packageId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult ServicePackageDeleteService(int servicePackageItemId)
        {
            var response = _serviceService.ServicePackageDeleteService(new ServicePackageDeleteServiceRequest { Id = servicePackageItemId });
            return Json(response.Result);
        }
    }
}
