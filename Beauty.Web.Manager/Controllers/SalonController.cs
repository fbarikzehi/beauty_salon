using Beauty.Application.Modules.Salon.Implementation;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Beauty.Application.Modules.Salon.Messaging;
using Beauty.Application.Modules.Salon.ViewModel;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class SalonController : BaseController
    {
        private readonly ISalonService _salonService;

        public SalonController(ISalonService salonService)
        {
            _salonService = salonService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateOpeningAndClosingTime(SalonUpdateOpeningAndClosingViewModel viewModel)
        {
            var response = _salonService.UpdateOpeningAndClosingTime(new UpdateRequest
            {
                Entity = new SalonViewModel
                {
                    Id=viewModel.SalonId,
                    OpeningTime = viewModel.OpeningTime,
                    ClosingTime = viewModel.ClosingTime
                }
            });
            return Json(response.Result);
        }
    }
}
