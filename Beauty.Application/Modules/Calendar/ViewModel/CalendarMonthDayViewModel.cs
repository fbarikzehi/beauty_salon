using Common.Crosscutting.Enum;

namespace Beauty.Application.Modules.Calendar.ViewModel
{
    public class CalendarMonthDayViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Date { get; set; }
        public string Occasion { get; set; }
        public HolidayTypeEnum HolidayType { get; set; }
        public bool IsHoliday { get; set; }
        public int DayOfWeek { get; set; }
    }
}
