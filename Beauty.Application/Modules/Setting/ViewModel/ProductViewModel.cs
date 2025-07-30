 using System;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Setting.ViewModel
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int UnitId { get; set; }
        public string Description { get; set; }
        public List<ProductImageViewModel> Images { get; set; }
    }
}
