using Beauty.Model.Setting;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Customer
{

    [Table(name: "Cheques", Schema = "Customer")]
    public class CustomerChequeModel : ValueObjectBase<CustomerChequeModel, int>
    {
        public string Number { get; set; }
        public float Fee { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public short BankId { get; set; }
        [ForeignKey("BankId")]
        public virtual BankModel Bank { get; set; }
        public Guid CustomerProfileId { get; set; }
        [ForeignKey("CustomerProfileId")]
        public virtual CustomerProfileModel CustomerProfile { get; set; }
        public string Details { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
