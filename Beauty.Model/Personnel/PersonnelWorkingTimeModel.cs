using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Personnel
{
    [Table(name: "WorkingTime", Schema = "Personnel")]
    public class PersonnelWorkingTimeModel : ValueObjectBase<PersonnelWorkingTimeModel, long>
    {
        public Guid PersonnelProfileId { get; set; }
        [ForeignKey("PersonnelProfileId")]
        public virtual PersonnelProfileModel PersonnelProfile { get; set; }
        [Column(TypeName ="date")]
        public DateTime Date { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}
