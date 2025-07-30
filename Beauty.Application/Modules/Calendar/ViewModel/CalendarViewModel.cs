using System.Collections.Generic;

namespace Beauty.Application.Modules.Calendar.ViewModel
{
    public class CalendarViewModel
    {
        public short Id { get; set; }
        public int Year { get; set; }
        public List<CalendarMonthViewModel> Months { get; set; }
    }
}
