using Beauty.Application.Modules.Customer.Implementation;
using Beauty.Application.Modules.Customer.Messaging;
using Beauty.Application.Modules.Customer.ViewModel;
using Common.Presentation.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Manager.Controllers
{
    [Authorize(Policy = "Manager")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public JsonResult Create(CustomerProfileCreateViewModel viewModel)
        {
            var response = _customerService.Create(new CreateRequest { Entity = viewModel });
            return Json(response.Result);
        }

        public JsonResult FindAllBySearch(string val)
        {
            var response = _customerService.FindAllBySearch(new FindAllBySearchRequest { FullName = val }).Result;
            return Json(response);
        }
    }
}
