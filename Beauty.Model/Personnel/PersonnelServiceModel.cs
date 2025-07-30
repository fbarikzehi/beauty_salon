using Beauty.Model.Service;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Personnel
{
    [Table(name: "Services", Schema = "Personnel")]
    public class PersonnelServiceModel : ValueObjectBase<PersonnelServiceModel, int>
    {
        public short ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual ServiceModel Service { get; set; }
        public Guid PersonnelProfileId { get; set; }
        [ForeignKey("PersonnelProfileId")]
        public virtual PersonnelProfileModel PersonnelProfile { get; set; }
        public float? Percentage { get; set; }

    }
}
