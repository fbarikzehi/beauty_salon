using Beauty.Application.Modules.Calendar.ViewModel;
using Beauty.Model.Setting;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Utility;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Beauty.Application.Modules.Calendar.Mapper
{
    public static class CalendarMapper
    {
        public static CalendarViewModel ToFindViewModel(this CalendarModel calendar)
        {
            var pc = new PersianCalendar();

            var months = new List<CalendarMonthViewModel>();
            for (int month = 1; month <= 12; month++)
            {
                var calendarMonth = new CalendarMonthViewModel { Title = DateTimeUtility.GetNameOfMonth(month), Number = month, Days = new List<CalendarMonthDayViewModel>() };
                var days = pc.GetDaysInMonth(calendar.Year, month);
                for (int day = 1; day <= days; day++)
                {
                    var pDate = $"{calendar.Year}/{month}/{day}";
                    var gDate = pc.ToDateTime(calendar.Year, month, day, 0, 0, 0, 0);
                    var calendarDate = calendar.CalendarDates.Where(x => x.Date == gDate);
                    var holiday = calendarDate.FirstOrDefault(x => x.IsHoliday);
                    calendarMonth.Days.Add(new CalendarMonthDayViewModel
                    {
                        Date = pDate,
                        HolidayType = holiday == null ? HolidayTypeEnum.None : holiday.HolidayType,
                        IsHoliday = holiday != null && holiday.IsHoliday,
                        Number = day,
                        Occasion = string.Join(',', calendarDate.Select(x => x.Occasion)),
                        DayOfWeek = (int)pc.GetDayOfWeek(gDate),
                    });
                }
                months.Add(calendarMonth);
            }

            return new CalendarViewModel
            {
                Id = calendar.Id,
                Year = calendar.Year,
                Months = months
            };
        }
    }
}
