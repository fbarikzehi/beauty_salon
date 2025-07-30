using Beauty.Model.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Service
{
    public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<ServiceModel>
    {
        public void Configure(EntityTypeBuilder<ServiceModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
