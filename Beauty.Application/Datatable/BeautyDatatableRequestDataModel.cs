namespace Beauty.Application.Datatable
{
    public class BeautyDatatableRequestDataModel
    {
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public int Perpage { get; set; } = 10;
        public string Query { get; set; }
        public string SortBy { get; set; }
        public string SortDir { get; set; }
        public string[] SearchValues { get; set; } = new string[] { };
    }
}
