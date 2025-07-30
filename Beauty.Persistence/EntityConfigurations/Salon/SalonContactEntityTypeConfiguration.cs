using Beauty.Model.Salon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Salon
{
    public class SalonContactEntityTypeConfiguration : IEntityTypeConfiguration<SalonContactModel>
    {
        public void Configure(EntityTypeBuilder<SalonContactModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
