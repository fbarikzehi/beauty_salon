namespace Common.Logger.Model
{
    public class ExceptionModel : MongoBase
    {
        public string Path { get; set; }
        public string Exception { get; set; }
    }
}
