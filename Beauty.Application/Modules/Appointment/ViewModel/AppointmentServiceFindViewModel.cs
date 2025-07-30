using System;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentServiceFindViewModel
    {
        public int Id { get; set; }
        public short ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool IsDone { get; set; }

    }
}
