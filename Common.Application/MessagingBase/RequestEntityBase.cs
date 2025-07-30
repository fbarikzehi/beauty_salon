namespace Common.Application.MessagingBase
{
    public abstract class RequestEntityBase<TEntity> : RequestBase where TEntity : new()
    {
        protected RequestEntityBase()
        {
            Entity = new TEntity();
        }

        public TEntity Entity { get; set; } = new TEntity();
    }
}