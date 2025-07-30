using Beauty.Model.Personnel;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Line
{
    [Table(name: "LinePersonnels", Schema = "Line")]
    public class LinePersonnelModel : ValueObjectBase<LinePersonnelModel, int>
    {
        public Guid PersonnelProfileId { get; set; }
        [ForeignKey("PersonnelProfileId")]
        public virtual PersonnelProfileModel PersonnelProfile { get; set; }

        public short LineId { get; set; }
        [ForeignKey("LineId")]
        public virtual LineModel Line { get; set; }
    }
}
