using Common.Application.ViewModelBase;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beauty.Application.Modules.Service.ViewModel
{
    public class ServiceIndexViewModel : DataGridViewModelBase
    {
        public SelectList Lines { get; set; }
    }
}
