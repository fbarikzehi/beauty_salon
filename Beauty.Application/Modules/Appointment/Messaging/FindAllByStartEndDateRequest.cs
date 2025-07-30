using System;

namespace Beauty.Application.Modules.Appointment.Messaging
{
    public class FindAllByStartEndDateRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
