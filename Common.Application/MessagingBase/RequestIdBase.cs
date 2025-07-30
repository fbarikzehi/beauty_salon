namespace Common.Application.MessagingBase
{
    public abstract class RequestIdBase<TId> : RequestBase
    {
        public TId Id { get; set; }
    }
}