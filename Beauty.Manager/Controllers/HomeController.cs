using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Manager.Controllers
{
    [Authorize(Policy = "Manager")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}