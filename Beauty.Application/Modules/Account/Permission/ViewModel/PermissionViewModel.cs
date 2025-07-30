using System.Collections.Generic;

namespace Beauty.Application.Modules.Account.Permission.ViewModel
{
    public class PermissionViewModel
    {
        public short Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public List<PermissionViewModel> SubPermissions { get; set; }
        public List<PermissionActionViewModel> Actions { get; set; }
    }
}
