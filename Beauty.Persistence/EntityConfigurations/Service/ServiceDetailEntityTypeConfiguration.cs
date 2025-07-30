using Beauty.Model.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Service
{
    public class ServiceDetailEntityTypeConfiguration : IEntityTypeConfiguration<ServiceDetailModel>
    {
        public void Configure(EntityTypeBuilder<ServiceDetailModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
