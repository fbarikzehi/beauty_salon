using Beauty.Model.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Setting
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        void IEntityTypeConfiguration<ProductModel>.Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
