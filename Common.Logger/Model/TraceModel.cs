namespace Common.Logger.Model
{
    public class TraceModel : MongoBase
    {
        public string Application { get; set; }
        public string Route { get; set; }
    }
}
