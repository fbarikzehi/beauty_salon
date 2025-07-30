using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Application
{
    [Table("Themes", Schema = "Application")]
    public class ThemeModel : EntityBase<short>
    {
        public string MainColor { get; set; }
        public string HeaderColor { get; set; }
        public string HeaderBackgroundColor { get; set; }
        public string MenuColor { get; set; }
        public string MenuBackgroundColor { get; set; }
        public string SubMenuColor { get; set; }
        public string SubMenuBackgroundColor { get; set; }
        public string ThemeLayoutColor { get; set; }
        public string MenuType { get; set; }
        public string FloatingButtonPosition { get; set; }

    }
}
