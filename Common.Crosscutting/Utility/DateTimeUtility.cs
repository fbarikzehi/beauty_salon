using System;
using System.Collections.Generic;
using System.Globalization;

namespace Common.Crosscutting.Utility
{
    public static class DateTimeUtility
    {
        private static readonly PersianCalendar _persianCalendar = new PersianCalendar();

        /// <summary>
        /// Convert DateTime struct to persian calendar date string.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>sample: 1399/12/25</returns>
        public static string ToPersianDate(this DateTime dateTime)
        {
            if (dateTime > DateTime.MinValue)
            {
                string Year = _persianCalendar.GetYear(dateTime).ToString();
                string Month = _persianCalendar.GetMonth(dateTime).ToString().Length == 1 ? "0" + _persianCalendar.GetMonth(dateTime).ToString() : _persianCalendar.GetMonth(dateTime).ToString();
                string Day = _persianCalendar.GetDayOfMonth(dateTime).ToString().Length == 1 ? "0" + _persianCalendar.GetDayOfMonth(dateTime).ToString() : _persianCalendar.GetDayOfMonth(dateTime).ToString();

                return $"{Year}/{Month}/{Day}";
            }
            return string.Empty;
        }
        /// <summary>
        /// Convert Nullable DateTime struct to persian calendar date string.
        /// </summary>
        /// <param name="dateTime">DateTime?</param>
        /// <param name="defaultVal">The default value to return if value is null</param>
        /// <returns></returns>
        public static string ToShortPersianDateNullable(this DateTime? dateTime, string defaultVal)
        {
            if (dateTime == null || dateTime <= DateTime.MinValue)
                return defaultVal;

            var dt = (DateTime)dateTime;

            string Year = _persianCalendar.GetYear(dt).ToString();
            string Month = _persianCalendar.GetMonth(dt).ToString().Length == 1 ? "0" + _persianCalendar.GetMonth(dt).ToString() : _persianCalendar.GetMonth(dt).ToString();
            string Day = _persianCalendar.GetDayOfMonth(dt).ToString().Length == 1 ? "0" + _persianCalendar.GetDayOfMonth(dt).ToString() : _persianCalendar.GetDayOfMonth(dt).ToString();

            return $"{Year}/{Month}/{Day}";
        }
        public static string ToShortPersianDateTimeNullable(this DateTime? dateTime, string defaultVal)
        {
            if (dateTime == null || dateTime <= DateTime.MinValue)
                return defaultVal;

            var dt = (DateTime)dateTime;

            string Year = _persianCalendar.GetYear(dt).ToString();
            string Month = _persianCalendar.GetMonth(dt).ToString().Length == 1 ? "0" + _persianCalendar.GetMonth(dt).ToString() : _persianCalendar.GetMonth(dt).ToString();
            string Day = _persianCalendar.GetDayOfMonth(dt).ToString().Length == 1 ? "0" + _persianCalendar.GetDayOfMonth(dt).ToString() : _persianCalendar.GetDayOfMonth(dt).ToString();

            return $"{Year}/{Month}/{Day} {dt.TimeOfDay.Hours}:{dt.TimeOfDay.Minutes}";
        }

        /// <summary>
        /// Convert DateTime struct to persian calendar short date string .
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>sample: 1399/12/25 20:50</returns>
        public static string ToShortPersianDateTime(this DateTime dateTime)
        {

            var dt = dateTime;

            string Year = _persianCalendar.GetYear(dt).ToString();
            string Month = _persianCalendar.GetMonth(dt).ToString().Length == 1 ? "0" + _persianCalendar.GetMonth(dt).ToString() : _persianCalendar.GetMonth(dt).ToString();
            string Day = _persianCalendar.GetDayOfMonth(dt).ToString().Length == 1 ? "0" + _persianCalendar.GetDayOfMonth(dt).ToString() : _persianCalendar.GetDayOfMonth(dt).ToString();

            return $"{Year}/{Month}/{Day} {dt.TimeOfDay.Hours}:{dt.TimeOfDay.Minutes}";
        }
        public static string ToShortPersianDate(DateTime? dateTime)
        {
            if (dateTime == null || dateTime <= DateTime.MinValue)
                return string.Empty;


            var dt = (DateTime)dateTime;

            string Year = _persianCalendar.GetYear(dt).ToString();
            string Month = _persianCalendar.GetMonth(dt).ToString().Length == 1 ? "0" + _persianCalendar.GetMonth(dt).ToString() : _persianCalendar.GetMonth(dt).ToString();
            string Day = _persianCalendar.GetDayOfMonth(dt).ToString().Length == 1 ? "0" + _persianCalendar.GetDayOfMonth(dt).ToString() : _persianCalendar.GetDayOfMonth(dt).ToString();

            return $"{Year}/{Month}/{Day}";
        }

        public static int GetPersianYear()
        {
            return _persianCalendar.GetYear(DateTime.Now);
        }
        public static int GetIntPersianYear(this DateTime dateTime)
        {
            return _persianCalendar.GetYear(dateTime);
        }
        public static string GetStringPersianYear(this DateTime dateTime)
        {
            return _persianCalendar.GetYear(dateTime).ToString();
        }
        public static string GetShortStringPersianYear(this DateTime dateTime)
        {
            return _persianCalendar.GetYear(dateTime).ToString().Substring(1, 2);
        }
        public static int? GetPersianYear(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            return _persianCalendar.GetYear((DateTime)dateTime);
        }
        public static int GetPersianMonth()
        {
            return _persianCalendar.GetMonth(DateTime.Now);
        }
        public static int GetPersianMonth(this DateTime dateTime)
        {
            return _persianCalendar.GetMonth(dateTime);
        }
        public static int? GetPersianMonth(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            return _persianCalendar.GetMonth((DateTime)dateTime);
        }
        public static int GetPersianDayOfMonth()
        {
            return _persianCalendar.GetDayOfMonth(DateTime.Now);
        }
        public static int GetPersianDayOfMonth(this DateTime dateTime)
        {
            return _persianCalendar.GetDayOfMonth(dateTime);
        }
        public static int? GetPersianDayOfMonth(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            return _persianCalendar.GetDayOfMonth((DateTime)dateTime);
        }

        public static string GetLongPersianDate(this DateTime? dateTime)
        {
            if (dateTime == null || dateTime <= DateTime.MinValue)
                return string.Empty;


            var dt = (DateTime)dateTime;

            string year = _persianCalendar.GetYear(dt).ToString();

            int month = _persianCalendar.GetMonth(dt);

            string day = _persianCalendar.GetDayOfMonth(dt).ToString().Length == 1 ? "0" + _persianCalendar.GetDayOfMonth(dt).ToString() : _persianCalendar.GetDayOfMonth(dt).ToString();

            return $"{day} {GetNameOfMonth(month)} {year}";
        }

        public static string GetNameOfMonth(int month)
        {
            return month switch
            {
                1 => "فروردین",
                2 => "اردیبهشت",
                3 => "خرداد",
                4 => "تیر",
                5 => "مرداد",
                6 => "شهریور",
                7 => "مهر",
                8 => "آبان",
                9 => "آذر",
                10 => "دی",
                11 => "بهمن",
                12 => "اسفند",
                _ => "",
            };
        }
        public static Dictionary<string, int> GetDaysOfWeek()
        {
            var daysOfWeek = new Dictionary<string, int>
            {
                { "شنبه", 1 },
                { "یکشنبه", 2 },
                { "دوشنبه", 3 },
                { "سه شنبه", 4 },
                { "چهارشنبه", 5 },
                { "پنج شنبه", 6 },
                { "جمعه", 7 }
            };

            return daysOfWeek;
        }
        public static List<int> GetYearRange(int? start, int upto)
        {
            var years = new List<int>();

            if (start is null) start = GetIntPersianYear(DateTime.Now);

            for (int i = (int)start; i <= start + upto; i++)
            {
                years.Add(i);
            }
            return years;
        }
        public static DateTime PersianDateToDateTime(int? year, int? month, int? day)
        {

            if (year == null || year == 0)
            {
                year = _persianCalendar.MinSupportedDateTime.Year;
            }

            if (month == null || month == 0)
            {
                month = 1;
            }


            if (day == null || day == 0)
            {
                day = 1;
            }
            else if (month >= 7 && month <= 11 && day > 30)
            {
                day = 30;
            }
            else if (month == 12)
            {
                if (_persianCalendar.IsLeapYear((int)year))
                    day = 30;
                else
                    day = 29;
            }
            var persianDate = $"{year}/{month}/{day}";
            return DateTime.Parse(persianDate, new CultureInfo("fa-IR"));
        }
        public static DateTime PersianDateStringToDateTime(this string persianDate)
        {
            var spPdate = persianDate.PersianDigitsToEnglish().Split('/');
            DateTime dt = new DateTime(
                int.Parse(spPdate[0]),
                int.Parse(spPdate[1]),
                int.Parse(spPdate[2]), _persianCalendar);

#if !DEBUG
    return DateTime.Parse(dt.ToString(CultureInfo.InvariantCulture));
#endif
#if DEBUG
            return dt;
#endif

        }
        public static DateTime? PersianDateStringToAllowNullDateTime(this string persianDate)
        {
            if (string.IsNullOrEmpty(persianDate)) return null;

            var spPdate = persianDate.PersianDigitsToEnglish().Split('/');
            DateTime dt = new DateTime(int.Parse(spPdate[0]), int.Parse(spPdate[1]), int.Parse(spPdate[2]), _persianCalendar);

#if !DEBUG
    return DateTime.Parse(dt.ToString(CultureInfo.InvariantCulture));
#endif
#if DEBUG
            return dt;
#endif

        }
        public static DateTime? PersianDateToAllowNullDateTime(int? year, int? month, int? day)
        {
            if ((year == null || year == 0) && (month == null || month == 0) && (day == null || day == 0))
                return null;

            if (year == null || year == 0)
            {
                year = _persianCalendar.MinSupportedDateTime.Year;
            }

            if (month == null || month == 0)
            {
                month = 1;
            }


            if (day == null || day == 0)
            {
                day = 1;
            }
            else if (month >= 7 && month <= 11 && day > 30)
            {
                day = 30;
            }
            else if (month == 12)
            {
                if (_persianCalendar.IsLeapYear((int)year))
                    day = 30;
                else
                    day = 29;
            }
            var persianDate = $"{year}/{month}/{day}";
            return DateTime.Parse(persianDate, new CultureInfo("fa-IR"));
        }

        public static string FormatLeftDateTimeToPersianTimeString(this DateTime dateTime)
        {
            string lastDateTimeStr = "";
            TimeSpan diff = DateTime.Now.Subtract(dateTime);

            if (diff.Days > 0)
            {
                lastDateTimeStr = $"{diff.Days}  روز و";
            }
            if (diff.Hours > 0)
            {
                lastDateTimeStr += $"{diff.Hours} ساعت و ";
            }
            if (diff.Minutes > 0)
            {
                lastDateTimeStr += $"{diff.Minutes} دقیقه";
            }
            if (diff.Minutes <= 0)
            {
                lastDateTimeStr += $"چند ثانیه ";
            }
            return $"{lastDateTimeStr} قبل ";
        }
        public static string FormatLeftDateTimeToPersianTimeString(this DateTime? dateTime)
        {
            string lastDateTimeStr = "";
            if (dateTime is null) return lastDateTimeStr;

            TimeSpan diff = DateTime.Now.Subtract((DateTime)dateTime);

            if (diff.Days > 0)
            {
                lastDateTimeStr = $"{diff.Days}  روز و";
            }
            if (diff.Hours > 0)
            {
                lastDateTimeStr += $"{diff.Hours} ساعت و ";
            }
            if (diff.Minutes > 0)
            {
                lastDateTimeStr += $"{diff.Minutes} دقیقه";
            }
            if (diff.Minutes <= 0)
            {
                lastDateTimeStr += $"چند ثانیه قبل";
            }
            return $"{lastDateTimeStr} قبل ";
        }

        /// <summary>
        /// if (result < 0)
        //        relationship = "is earlier than";
        //  else if (result == 0)
        //        relationship = "is the same time as";         
        //  else
        //        relationship = "is later than";
        /// </summary>
        public static int ComparePersianDateToDateTime(string persianDate, DateTime dateTime)
        {
            var dateTime1 = persianDate.PersianDateStringToDateTime();
            return DateTime.Compare(dateTime1.Date, dateTime.Date);
        }

        public static int PersianDateToUnix(this string persianStr)
        {
            var datetime = persianStr.PersianDateStringToDateTime();
            int unixTimestamp = (int)datetime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            return unixTimestamp;

        }

        public static TimeSpan StringToTimeSpan(this string time)
        {
            int hours = 0, minutes = 0, seconds = 0;

            if (!string.IsNullOrWhiteSpace(time))
            {
                var sp = time.Split(':');

                if (sp.Length > 0 && int.TryParse(sp[0], out int _))
                    hours = int.Parse(sp[0]);
                if (sp.Length > 1 && int.TryParse(sp[1], out int _))
                    minutes = int.Parse(sp[1]);
                if (sp.Length > 2 && int.TryParse(sp[2], out int _))
                    seconds = int.Parse(sp[2]);
            }

            return new TimeSpan(hours, minutes, seconds);
        }

        public static string TimeSpanToString(this TimeSpan? time, string format)
        {
            if (time is null) return string.Empty;
            return ((TimeSpan)time).ToString(format);
        }

        /// <summary>
        /// Add given days to persian calendar date string.
        /// </summary>
        /// <param name="date">persian calendar string date</param>
        /// <param name="days">if the days param is lower than zero it will subtract the date</param>
        /// <returns>
        /// sample: 1399/12/25 ---> 2 ----> 1399/12/27 ,  1399/12/25 ---> -2 ----> 1399/12/23
        /// </returns>
        public static string AddDaysPersianDate(this string date, double days) => date.PersianDateStringToDateTime().AddDays(days).ToPersianDate();

    }
}
