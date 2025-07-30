using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Common.Persistance
{
    public class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class
    {
        void IEntityTypeConfiguration<T>.Configure(EntityTypeBuilder<T> builder)
        {
            throw new NotImplementedException();
        }
    }
}
