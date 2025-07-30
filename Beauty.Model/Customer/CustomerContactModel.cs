using Common.Crosscutting.Enum;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Customer
{
    [Table(name: "Contacts", Schema = "Customer")]
    public class CustomerContactModel : ValueObjectBase<CustomerContactModel, short>
    {
        public Guid CustomerProfileId { get; set; }
        [ForeignKey("CustomerProfileId")]
        public virtual CustomerProfileModel CustomerProfile { get; set; }
        public ContactTypeEnum Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; } = true;

        public void ReverseActive() => IsActive = !IsActive;
    }
}
