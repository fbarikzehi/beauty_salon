using System.Collections.Generic;

namespace Common.Application.MessagingBase
{
    public abstract class ResponseDataTableBase<TEntity> : ResponseBase where TEntity : new()
    {
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public int sEcho { get; set; }
        public string sColumns { get; set; }
        public IEnumerable<TEntity> aaData { get; set; } = new List<TEntity>();
    }
}
