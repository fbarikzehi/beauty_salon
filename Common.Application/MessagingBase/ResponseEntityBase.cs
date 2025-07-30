namespace Common.Application.MessagingBase
{
    public abstract class ResponseEntityBase<TEntity> : ResponseBase where TEntity : new()
    {
        protected ResponseEntityBase()
        {
            Entity = new TEntity();
        }

        public TEntity Entity { get; set; }
    }
}