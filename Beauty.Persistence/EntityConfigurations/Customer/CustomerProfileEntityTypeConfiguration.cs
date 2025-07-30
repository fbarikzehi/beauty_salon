using Beauty.Model.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Customer
{
    public class CustomerProfileEntityTypeConfiguration : IEntityTypeConfiguration<CustomerProfileModel>
    {
        public void Configure(EntityTypeBuilder<CustomerProfileModel> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
