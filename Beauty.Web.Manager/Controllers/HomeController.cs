using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Manager.Controllers
{
    [Authorize(Policy = "ElevatedRights")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}