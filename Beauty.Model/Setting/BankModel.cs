using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Setting
{
    [Table(name: "Banks", Schema = "Setting")]
    public class BankModel : EntityBase<short>
    {
        public string Title { get; set; }
    }
}
