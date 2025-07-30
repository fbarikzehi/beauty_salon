using Beauty.Model.Account.User;
using Beauty.Model.Appointment;
using Beauty.Model.Line;
using Common.Crosscutting.Enum;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Personnel
{
    [Table(name: "Profiles", Schema = "Personnel")]
    public class PersonnelProfileModel : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public ushort Code { get; set; }
        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }
        [Column(TypeName = "date")]
        public DateTime JobStart { get; set; }
        [Column(TypeName = "date")]
        public DateTime? JobEnd { get; set; }
        public CooperationTypeEnum CooperationType { get; set; }
        public float Salary { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
        public virtual ICollection<PersonnelContactModel> Contacts { get; set; }
        public virtual ICollection<PersonnelServiceModel> Services { get; set; }
        public virtual ICollection<PersonnelWorkingTimeModel> WorkingTimes { get; set; }
        public virtual ICollection<PersonnelAttendanceModel> Attendances { get; set; }
        public virtual ICollection<AppointmentServiceModel> AppointmentServices { get; set; }
        public virtual ICollection<LinePersonnelModel> LinePersonnels { get; set; }
    }
}
