using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Service
{
    [Table(name: "ServicePackages", Schema = "Service")]
    public class ServicePackageModel : EntityBase<int>
    {
        public string Title { get; set; }
        public DateTime?  From { get; set; }
        public DateTime? To { get; set; }
        public DateTime? ExtendTo { get; set; }
        public float DiscountPrice { get; set; }

        public virtual ICollection<ServicePackageItemModel> Items { get; set; }
    }
}
