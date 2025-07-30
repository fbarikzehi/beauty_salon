using Microsoft.AspNetCore.Http;

namespace Beauty.Application.Datatable
{
    public static class BeautyDatatableRequest
    {
        public static BeautyDatatableRequestDataModel GetDatatableParameters(this HttpContext context)
        {
            var data= new BeautyDatatableRequestDataModel
            {
                CurrentPage = int.TryParse(context.Request.Form["pagination[page]"].ToString(), out int _) ? int.Parse(context.Request.Form["pagination[page]"].ToString()) : 0,
                Pages = int.TryParse(context.Request.Form["pagination[pages]"].ToString(), out int _) ? int.Parse(context.Request.Form["pagination[pages]"].ToString()) : 0,
                Perpage = int.TryParse(context.Request.Form["pagination[perpage]"].ToString(), out int _) ? int.Parse(context.Request.Form["pagination[perpage]"].ToString()) : 0,
                Query = context.Request.Form["query"].ToString(),
                SortBy = context.Request.Form["sort[field]"].ToString(),
                SortDir = context.Request.Form["sort[sort]"].ToString(),
            };

            return data;
        }
    }
}
