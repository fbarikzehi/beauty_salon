using Beauty.Application.Modules.Menu.Implementation;
using Beauty.Application.Modules.Menu.Messaging;
using Beauty.Application.Modules.Salon.Implementation;
using Beauty.Application.Modules.Salon.Messaging;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Manager.Controllers
{
    [Authorize(Policy = "Manager")]
    public class SharedController : BaseController
    {
        private readonly IMenuService _menuService;
        private readonly ISalonService _salonService;

        public SharedController(IMenuService menuService,ISalonService salonService)
        {
            _menuService = menuService;
            _salonService = salonService;
        }

        public JsonResult GetSubmenus(short mId)
        {
            var response = _menuService.FindById(new FindByIdRequest
            {
                Id = mId
            }).Result.Entity;
            return Json(response);
        }

        public JsonResult GetInfo()
        {
            var response = _salonService.Find(new FindRequest()).Result.Entity;
            return Json(response);
        }
    }
}
