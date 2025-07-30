using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Appointment
{
    [Table(name: "AppointmentServiceDetails", Schema = "Appointment")]
    public class AppointmentServiceDetailModel : ValueObjectBase<AppointmentServiceDetailModel, int>
    {
        public int AppointmentServiceId { get; set; }
        [ForeignKey("AppointmentServiceId")]
        public virtual AppointmentServiceModel AppointmentService { get; set; }
        public short ServiceDetailId { get; set; }

        public float Count { get; set; }
        public float Price { get; set; }
        public float TotalPrice { get; set; }
    }
}
