using Beauty.Model.Service;
using Common.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Line
{
    [Table("Lines", Schema = "Line")]
    public class LineModel : EntityBase<short>
    {
        public string Title { get; set; }
        public bool IsActive { get; set; } = true;

        public void ReverseActive() => IsActive = !IsActive;

        public virtual ICollection<ServiceModel> Services { get; set; }
        public virtual ICollection<LinePersonnelModel> LinePersonnels { get; set; }
    }
}
