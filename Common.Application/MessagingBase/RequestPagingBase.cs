namespace Common.Application.MessagingBase
{
    public abstract class RequestPagingBase : RequestBase
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public string[] SearchValues { get; set; } = new string[] { };
    }
}