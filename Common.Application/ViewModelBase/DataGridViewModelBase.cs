using System.Collections.Generic;

namespace Common.Application.ViewModelBase
{
    public abstract class DataGridViewModelBase
    {
        public List<DataGridHeader> Headers { get; set; }

    }

    public class DataGridHeader
    {
        public string Title { get; set; }
        public bool IsFilterable { get; set; }
        public bool IsOrderable { get; set; }
        public int Width { get; set; }
    }
}
