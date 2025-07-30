using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Appointment
{
    [Table(name: "Payment", Schema = "Appointment")]
    public class AppointmentPaymentModel : EntityBase<long>
    {
        public Guid AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual AppointmentModel Appointment { get; set; }

        public float Amount { get; set; }
    }
}
