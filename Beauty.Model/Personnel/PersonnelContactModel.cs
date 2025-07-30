using Common.Crosscutting.Enum;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Personnel
{
    [Table(name: "Contacts", Schema = "Personnel")]
    public class PersonnelContactModel : ValueObjectBase<PersonnelContactModel, short>
    {
        public Guid PersonnelProfileId { get; set; }
        [ForeignKey("PersonnelProfileId")]
        public virtual PersonnelProfileModel PersonnelProfile { get; set; }
        public ContactTypeEnum Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; } = true;

        public void ReverseActive() => IsActive = !IsActive;
    }
}
