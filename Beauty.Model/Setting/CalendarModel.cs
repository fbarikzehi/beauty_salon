using Common.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Setting
{
    [Table(name: "Calendars", Schema = "Setting")]
    public class CalendarModel : EntityBase<short>
    {
        public short Year { get; set; }

        public virtual ICollection<CalendarDateModel> CalendarDates { get; set; }
    }
}
