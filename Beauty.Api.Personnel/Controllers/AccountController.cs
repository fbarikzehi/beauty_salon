using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beauty.Application.Modules.Account.User.Implementation;
using Beauty.Application.Modules.Account.User.Messaging;
using Beauty.Application.Modules.Account.User.ViewModel;
using Common.Application;
using Common.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Api.Personnel.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/Account/Login")]
        public async Task<LoginResponse> Login(LoginViewModel viewModel)
        {
            var response = new LoginResponse();

            if (!ModelState.IsValid)
                response.Message = ModelState.GetModelFirstError();

            viewModel.IsMobileRequest = true;
            var serviceResponse = await _userService.Login(new LoginRequest { Entity = viewModel });

            response = serviceResponse;
            return response;
        }
    }
}
