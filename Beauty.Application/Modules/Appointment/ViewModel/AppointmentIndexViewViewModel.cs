using Common.Crosscutting.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentIndexViewViewModel
    {
        public string DefaultCreateDateTime { get; set; } = DateTime.Now.ToPersianDate();
        public string SalonOpeningTime { get; set; }
        public string SalonClosingTime { get; set; }
        public SelectList Personnels { get; set; }
    }
}
