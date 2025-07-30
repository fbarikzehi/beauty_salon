using Beauty.Model.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Service
{
    public class ServicePackageEntityTypeConfiguration : IEntityTypeConfiguration<ServicePackageModel>
    {
        public void Configure(EntityTypeBuilder<ServicePackageModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
