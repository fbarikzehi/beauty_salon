using Beauty.Model.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Service
{
    public class ServicePackageItemEntityTypeConfiguration : IEntityTypeConfiguration<ServicePackageItemModel>
    {
        public void Configure(EntityTypeBuilder<ServicePackageItemModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
