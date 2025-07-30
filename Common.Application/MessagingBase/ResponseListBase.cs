using System.Collections.Generic;

namespace Common.Application.MessagingBase
{
    public abstract class ResponseListBase<TEntity> : ResponseBase where TEntity : new()
    {
        public int Index { get; set; }
        public int Count { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public int Draw { get; set; }
        public List<TEntity> Data { get; set; } = new List<TEntity>();
    }
}