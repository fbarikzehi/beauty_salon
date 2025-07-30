using Common.Crosscutting.Enum;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Salon
{
    [Table(name: "Contacts", Schema = "Salon")]
    public class SalonContactModel : ValueObjectBase<SalonContactModel, int>
    {
        public Guid SalonId { get; set; }
        [ForeignKey("SalonId")]
        public virtual SalonModel Salon { get; set; }
        public ContactTypeEnum Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; } = true;

        public void ReverseActive() => IsActive = !IsActive;
    }
}
