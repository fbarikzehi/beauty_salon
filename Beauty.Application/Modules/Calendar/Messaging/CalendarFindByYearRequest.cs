using Common.Application.MessagingBase;

namespace Beauty.Application.Modules.Calendar.Messaging
{
    public class CalendarFindByYearRequest : RequestBase
    {
        public int Year { get; set; }
    }
}
