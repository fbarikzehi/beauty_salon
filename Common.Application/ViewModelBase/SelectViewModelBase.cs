namespace Common.Application.ViewModelBase
{
    public abstract class SelectViewModelBase<TValue>
    {
        public TValue Value { get; set; }
        public string Text { get; set; }
    }
}
