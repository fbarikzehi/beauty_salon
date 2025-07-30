using System.Collections.Generic;

namespace Beauty.Application.Modules.Calendar.ViewModel
{
    public class CalendarMonthViewModel
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public List<CalendarMonthDayViewModel> Days { get; set; }
    }
}
