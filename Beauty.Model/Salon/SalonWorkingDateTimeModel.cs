using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Salon
{
    [Table(name: "WorkingDateTimes", Schema = "Salon")]
    public class SalonWorkingDateTimeModel : ValueObjectBase<SalonWorkingDateTimeModel, long>
    {
        public Guid SalonId { get; set; }
        [ForeignKey("SalonId")]
        public virtual SalonModel Salon { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}
