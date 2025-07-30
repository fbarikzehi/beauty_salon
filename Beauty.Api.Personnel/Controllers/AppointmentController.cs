using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beauty.Application.Modules.Appointment.Implementation;
using Beauty.Application.Modules.Appointment.Messaging;
using Beauty.Application.Modules.Appointment.ViewModel;
using Common.Application;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Api.Personnel.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Route("/Appointment/GetAllNotDoneByDate")]
        public async Task<ResponseBase<List<AppointmentPersonnelItemViewModel>>> GetAllNotDoneByDate(Guid personnelId, DateTime date = default)
        {
            var response = new ResponseBase<List<AppointmentPersonnelItemViewModel>>();
            var serviceResponse = await _appointmentService.FindAllNotDoneServicesByPersonnelAndDate(new FindAllByPersonnelRequest { Id = personnelId, Date = date });

            response.IsSuccess = serviceResponse.IsSuccess;
            response.Message = serviceResponse.Message;
            if (serviceResponse.IsSuccess)
                response.Data = serviceResponse.Data.ToList();

            return response;
        }


        [HttpPost]
        [Route("/Appointment/DoneService")]
        public async Task<ResponseBase<object>> PostDoneService(DoneServiceRequestViewModel viewModel)
        {
            var response = new ResponseBase<object>();

            var serviceResponse = await _appointmentService.DoneService(new DoneServiceRequest { Appointment = viewModel.AppointmentId, AppointmentServiceId = viewModel.AppointmentServiceId });

            response.IsSuccess = serviceResponse.IsSuccess;
            response.Message = serviceResponse.Message;
            return response;
        }
    }
}
