using System.Collections.Generic;

namespace Beauty.Application.ViewModel
{
    public class MenuViewModel
    {
        public short Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public List<SubMenuViewModel> SubMenus { get; set; }
        public bool Active { get; set; }
        public bool IsActive { get; set; }
    }
}
