using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Logger
{
    public class MongoBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
