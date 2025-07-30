using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Appointment
{
    [Table(name: "Discounts", Schema = "Appointment")]
    public class AppointmentDiscountModel : ValueObjectBase<AppointmentDiscountModel, int>
    {
        public Guid AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual AppointmentModel Appointment { get; set; }

        public float Amount { get; set; }
        public DateTime CreateDateTime { get; private set; } = new DateTime();

        public void SetCreateDateTime(DateTime dateTime)
        {
            CreateDateTime = dateTime;
        }
    }
}
