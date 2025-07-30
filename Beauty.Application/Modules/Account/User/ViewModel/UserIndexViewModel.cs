using Common.Application.ViewModelBase;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beauty.Application.Modules.Account.User.ViewModel
{
    public class UserIndexViewModel : DataGridViewModelBase
    {
        public SelectList Roles { get; set; }
    }
}
