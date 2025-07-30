using Beauty.Model.Customer;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Service
{
    [Table(name: "CustomerRatings", Schema = "Service")]
    public class ServiceCustomerRatingModel : ValueObjectBase<ServiceCustomerRatingModel, int>
    {
        public short ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual ServiceModel Service { get; set; }

        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual CustomerProfileModel Customer { get; set; }

        public float Rate { get; set; }
    }
}
