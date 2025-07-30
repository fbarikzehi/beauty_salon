using Common.Crosscutting.Enum;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Setting
{
    [Table(name: "CalendarDates", Schema = "Setting")]
    public class CalendarDateModel : ValueObjectBase<CalendarDateModel, int>
    {
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string Occasion { get; set; }
        public HolidayTypeEnum HolidayType { get; set; }
        public bool IsHoliday { get; set; }
        public short CalendarId { get; set; }
        [ForeignKey("CalendarId")]
        public virtual CalendarModel Calendar { get; set; }
    }
}
