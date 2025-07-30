using Beauty.Model.Customer;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Appointment
{
    [Table(name: "Appointments", Schema = "Appointment")]
    public class AppointmentModel : EntityBase<Guid>
    {
        public Guid CustomerProfileId { get; set; }
        [ForeignKey(nameof(CustomerProfileId))]
        public virtual CustomerProfileModel CustomerProfile { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string FollowingCode { get; set; }

        public virtual ICollection<AppointmentServiceModel> Services { get; set; }
        public virtual ICollection<AppointmentPaymentModel> Payments { get; set; }
        public virtual ICollection<AppointmentDiscountModel> Discounts { get; set; } = new List<AppointmentDiscountModel>();

        public bool IsCanceled { get; set; }
        public bool IsDone { get; set; }

        public void ReverseCanceled() => IsCanceled = !IsCanceled;
        public void ReverseDone() => IsDone = !IsDone;
    }
}
