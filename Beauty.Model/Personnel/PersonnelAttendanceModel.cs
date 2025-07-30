using Common.Crosscutting.Enum;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Personnel
{
    [Table(name: "Attendances", Schema = "Personnel")]
    public class PersonnelAttendanceModel : ValueObjectBase<PersonnelAttendanceModel, long>
    {
        public Guid PersonnelProfileId { get; set; }
        [ForeignKey("PersonnelProfileId")]
        public virtual PersonnelProfileModel PersonnelProfile { get; set; }
        [Column(TypeName ="date")]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public AttendanceTypeEnum Type { get; set; }
    }
}
