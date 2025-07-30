using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Setting
{
    [Table(name: "Units", Schema = "Setting")]
    public class UnitModel : EntityBase<int>
    {
        public string Title { get; set; }
    }
}
