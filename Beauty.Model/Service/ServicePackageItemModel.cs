using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Service
{
    [Table(name: "ServicePackageItems", Schema = "Service")]
    public class ServicePackageItemModel : ValueObjectBase<ServicePackageItemModel, int>
    {
        public short ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual ServiceModel Service { get; set; }

        public int ServicePackageId { get; set; }
        [ForeignKey("ServicePackageId")]
        public virtual ServicePackageModel ServicePackage { get; set; }

        public float AfterDiscountPrice { get; set; }
    }
}