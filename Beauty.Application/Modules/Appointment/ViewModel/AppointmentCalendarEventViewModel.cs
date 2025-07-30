using System;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentCalendarEventViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string ClassName { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
