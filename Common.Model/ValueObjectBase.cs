namespace Common.Model
{
    public abstract class ValueObjectBase<T, TId> where T : ValueObjectBase<T, TId>
    {
        public TId Id { get; set; }

        public bool IsDeleted { get; set; }

        public void ReverseDelete() => IsDeleted = !IsDeleted;
    }
}
