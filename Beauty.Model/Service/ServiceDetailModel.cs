using Beauty.Model.Appointment;
using Common.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Service
{
    [Table(name: "Details", Schema = "Service")]
    public class ServiceDetailModel : ValueObjectBase<ServiceDetailModel, short>
    {
        public short ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual ServiceModel Service { get; set; }

        public string Title { get; set; }
        public float Price { get; set; }
        public virtual ICollection<AppointmentServiceDetailModel> AppointmentServiceDetails { get; set; }
    }
}
