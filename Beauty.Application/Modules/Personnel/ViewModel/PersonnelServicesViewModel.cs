using Beauty.Application.Modules.Line.ViewModel;
using System;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Personnel.ViewModel
{
    public class PersonnelServicesViewModel
    {
        public List<PersonnelServiceItemsViewModel> Services { get; set; }
        public Guid PersonnelId { get; set; }
        public short ServiceId { get; set; }
        public List<LineViewModel> Lines { get; set; }
    }
}
