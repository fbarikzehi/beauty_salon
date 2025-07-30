using Common.Logger.Enum;
using Common.Logger.Extensions;
using Common.Logger.Model;
using MongoDB.Driver;
using System;

namespace Common.Logger.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<DataModificationModel> _logDataModification;
        private readonly IMongoCollection<ExceptionModel> _logException;
        private readonly IMongoCollection<TraceModel> _trace;

        public LogRepository(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _logDataModification = database.GetCollection<DataModificationModel>(settings.DataModificationsCollectionName);
            _logException = database.GetCollection<ExceptionModel>(settings.ExceptionsCollectionName);
            _trace = database.GetCollection<TraceModel>(settings.TraceCollectionName);
        }

        public void CreateModificationLog(object record, string tabelName, TypeCode recordType, ModificationTypeEnum type, string path)
        {
            _logDataModification.InsertOne(new DataModificationModel
            {
                Record = Newtonsoft.Json.JsonConvert.SerializeObject(record),
                TabelName = tabelName,
                RecordType = recordType.ToString(),
                Type = EnumExtensions<ModificationTypeEnum>.GetPersianName(type),
                Path = path
            });
        }

        public void CreateExceptionLog(string path, Exception exception)
        {
            _logException.InsertOne(new ExceptionModel { Path = path, Exception = exception.ToString() });
        }

        public void CreateTraceLog()
        {
            throw new NotImplementedException();
        }
    }
}
