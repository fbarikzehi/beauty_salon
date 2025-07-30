namespace Common.Logger
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ExceptionsCollectionName { get; set; }
        public string DataModificationsCollectionName { get; set; }
        public string TraceCollectionName { get; set; }
    }
}
