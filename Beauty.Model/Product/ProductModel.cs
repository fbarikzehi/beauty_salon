using Beauty.Model.Setting;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Product
{
    [Table(name: "Products", Schema = "Product")]
    public class ProductModel : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int UnitId { get; set; }
        [ForeignKey("UnitId")]
        public virtual UnitModel Unit { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductImageModel> Images { get; set; }
    }
}
