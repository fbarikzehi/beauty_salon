using Common.Logger.Enum;
using System;

namespace Common.Logger.Repository
{
    public interface ILogRepository
    {
        void CreateModificationLog(object record, string tabelName, TypeCode recordType, ModificationTypeEnum type, string path);
        void CreateExceptionLog(string path, Exception exception);
        void CreateTraceLog();
    }
}
