using System;

namespace Beauty.Application.Modules.Setting.ViewModel
{
    public class ProductDataTableViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public int ImageCount { get; set; }
    }
}
