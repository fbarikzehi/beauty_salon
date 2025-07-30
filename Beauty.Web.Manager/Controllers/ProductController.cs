using Beauty.Application.Modules.Setting.Implementation;
using Beauty.Application.Modules.Setting.Messaging;
using Beauty.Application.Modules.Setting.ViewModel;
using Common.Application.ViewModelBase;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IUnitService _unitService;

        public ProductController(IProductService productService, IUnitService unitService)
        {
            _productService = productService;
            _unitService = unitService;
        }

        public IActionResult Index()
        {
            var viewModel = new ProductIndexViewModel
            {
                Headers = new List<DataGridHeader>
                {
                    new DataGridHeader{Title="ردیف"},
                    new DataGridHeader{Title="عنوان کالا"},
                    new DataGridHeader{Title="کد"},
                    new DataGridHeader{Title="واحد"},
                    new DataGridHeader{Title="تعداد تصویر"},
                    new DataGridHeader{Title="عملیات ها"},
                }
            };
            return View("Index", viewModel);
        }

        public JsonResult GetData(ProductFindAllByPagingRequest request)
        {
            var response = _productService.FindAllByPaging(request).Result;
            return Json(response);
        }

        [HttpPost]
        public IActionResult Modify(Guid productId)
        {
            var viewModel = new ProductModifyViewModel { Id = productId };
            if (productId != Guid.Empty)
            {
                var product = _productService.FindById(new ProductFindByIdRequest { Id = productId }).Result.Entity;
                if (product != null)
                {
                    viewModel.Name = product.Name;
                    viewModel.Code = product.Code;
                    viewModel.Description = product.Description;
                    viewModel.Images = product.Images;
                    viewModel.UnitId = product.UnitId;
                }
            }
            viewModel.Units = new SelectList(_unitService.FindAll(new UnitFindAllRequest()).Result.Data, "Id", "Title", viewModel.UnitId);
            return View("Modify", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult Create(ProductModifyViewModel viewModel)
        {
            var response = _productService.Create(new ProductCreateRequest { Entity = viewModel });
            return Json(response.Result);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult Update(ProductModifyViewModel viewModel)
        {
            var user = _productService.Update(new ProductUpdateRequest { Entity = viewModel });
            return Json(user.Result);
        }

        [HttpPost]
        public JsonResult Delete(Guid pId)
        {
            var response = _productService.Delete(new ProductDeleteByIdRequest { Id = pId });
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult DeleteIamge(int imageId)
        {
            var response = _productService.DeleteImage(new ProductImageDeleteRequest { Id = imageId }).Result;
            return Json(response);
        }

    }
}
