namespace Common.Logger
{
    public interface IMongoDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ExceptionsCollectionName { get; set; }
        string DataModificationsCollectionName { get; set; }
        string TraceCollectionName { get; set; }
    }
}
