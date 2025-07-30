using System;

namespace Common.Application.MessagingBase
{
    public abstract class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string AlertType { get; set; }
        public Exception Error { get; set; }

    }
}