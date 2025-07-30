using Beauty.Application.Modules.Account.User.Implementation;
using Beauty.Application.Modules.Account.User.Messaging;
using Beauty.Application.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Beauty.Web.Manager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            var viewModel = new LoginRequest();
            return View("Login", viewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<JsonResult> Login(LoginRequest request)
        {
            var response = _userService.Login(request);
            if (response.Result.IsSuccess)
            {
                //HttpContext.Session.SetString("fullname", response.Result.FullName);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(response.Result.Claims, "Cookies", "userId", "role")));
            }
            return Json(response.Result);

        }

        public IActionResult Denied(string returnUrl)
        {
            var viewModel = new DeniedViewModel { ReturnUrl = returnUrl };
            return View("Denied", viewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}