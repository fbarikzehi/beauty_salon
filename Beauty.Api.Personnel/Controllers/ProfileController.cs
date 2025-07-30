using System;
using System.Linq;
using System.Threading.Tasks;
using Beauty.Application.Modules.Personnel.Implementation;
using Beauty.Application.Modules.Personnel.Messaging;
using Beauty.Application.Modules.Personnel.ViewModel;
using Common.Application;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Api.Personnel.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IPersonnelService _personnelService;

        public ProfileController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }


        [HttpGet]
        [Route("/Profile/Get")]
        public async Task<ResponseBase<PersonnelProfileViewModel>> Get(Guid personnelId)
        {
            var response = new ResponseBase<PersonnelProfileViewModel>();
            var serviceResponse = await _personnelService.FindById(new FindByIdRequest { Id = personnelId });

            response.IsSuccess = serviceResponse.IsSuccess;
            response.Message = serviceResponse.Message;
            if (serviceResponse.IsSuccess)
            {
                serviceResponse.Entity.Id = Guid.Empty;
                response.Data = serviceResponse.Entity;
            }

            return response;
        }
    }
}
