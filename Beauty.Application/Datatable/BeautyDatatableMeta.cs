namespace Beauty.Application.Datatable
{
    public class BeautyDatatableMeta
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        //asc
        public string Sort { get; set; }
        //email
        public string Field { get; set; }
    }
}
