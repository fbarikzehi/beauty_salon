using System.Collections.Generic;

namespace Common.Application.MessagingBase
{
    public abstract class RequestListBase<T>
    {
        public List<T> Entities { get; set; }
    }
}
