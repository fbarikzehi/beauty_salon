using Common.Security.Claim;
using System;

namespace Common.Model
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; set; }
        public DateTime CreateDateTime { get; set; } = new DateTime();
        public Guid CreateUser { get; set; } 
        public bool IsDeleted { get; set; }

        public void ReverseDelete() => IsDeleted = !IsDeleted;

        public void SetCreateDateTime(DateTime dateTime)
        {
            CreateDateTime = dateTime;
        }

    }
}
