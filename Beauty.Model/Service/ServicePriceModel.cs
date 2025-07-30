using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Service
{
    [Table(name: "Prices", Schema = "Service")]
    public class ServicePriceModel : ValueObjectBase<ServicePriceModel, short>
    {
        public short ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual ServiceModel Service { get; set; }

        public float Price { get; set; }
        public DateTime FromDateTime { get; set; } = DateTime.Now;
        public DateTime? ToDateTime { get; set; }
    }
}
