using MongoDB.Bson.Serialization.Attributes;

namespace Common.Logger.Model
{
    public class DataModificationModel : MongoBase
    {
        public string TabelName { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        [BsonElement("Record")]
        public string Record { get; set; }
        public string RecordType { get; set; }
    }
}
