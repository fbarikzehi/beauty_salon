using System.Collections.Generic;

namespace Beauty.Application.Datatable
{
    public class BeautyDatatableResponse<TModel>
    {
        public BeautyDatatableMeta Meta { get; set; }
        public List<TModel> Data { get; set; }
    }
}
