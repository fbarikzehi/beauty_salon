using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Application.Setting
{
    [Table("Setting", Schema = "Application")]
    public class SettingModel : EntityBase<short>
    {
        public string Version { get; set; }
    }
}
