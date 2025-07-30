using Beauty.Model.Personnel;
using Beauty.Model.Service;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Appointment
{
    [Table(name: "AppointmentServices", Schema = "Appointment")]
    public class AppointmentServiceModel : ValueObjectBase<AppointmentServiceModel, int>
    {
        public Guid AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual AppointmentModel Appointment { get; set; }

        public short ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual ServiceModel Service { get; set; }
        public Guid? PersonnelProfileId { get; set; }
        [ForeignKey("PersonnelProfileId")]
        public virtual PersonnelProfileModel PersonnelProfile { get; set; }

        public DateTime? DoneDateTime { get; set; }
        public Guid? DonePersonnelProfileId { get; set; }
        [ForeignKey("DonePersonnelProfileId")]
        public virtual PersonnelProfileModel DonePersonnelProfile { get; set; }
        public bool IsDone { get; set; }
        public TimeSpan Time { get; set; }

        public virtual ICollection<AppointmentServiceDetailModel> AppointmentServiceDetails { get; set; } = new List<AppointmentServiceDetailModel>();

        public void ReverseDone() => IsDone = !IsDone;
    }
}
