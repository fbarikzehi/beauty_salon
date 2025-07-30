using Beauty.Application.Modules.Line.Implementation;
using Beauty.Application.Modules.Line.ViewModel;
using Common.Presentation.Framework;
using Beauty.Application.Modules.Line.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class LineController : BaseController
    {
        private readonly ILineService _lineService;

        public LineController(ILineService lineService)
        {
            _lineService = lineService;
        }

        public IActionResult Index()
        {
            var viewModel = new LineIndexViewModel { Lines = new List<LineViewModel>() };
            viewModel.Lines = _lineService.FindAll(new FindAllRequest()).Result.Data;
            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CreateRequest request)
        {
            var response = _lineService.Create(request);
            return Json(response.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(UpdateRequest request)
        {
            var response = _lineService.Update(request);
            return Json(response.Result);
        }

        [HttpPost]
        public JsonResult Delete(DeleteRequest request)
        {
            var response = _lineService.Delete(request);
            return Json(response.Result);
        }
    }
}
