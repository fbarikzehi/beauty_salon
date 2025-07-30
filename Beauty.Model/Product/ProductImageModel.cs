using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Product
{
    [Table(name: "ProductImages", Schema = "Product")]
    public class ProductImageModel : ValueObjectBase<ProductImageModel, int>
    {
        public string ServerPath { get; set; }
        public int Order { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }
    }
}
