using Beauty.Model.Personnel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Personnel
{
    public class PersonnelServiceEntityTypeConfiguration : IEntityTypeConfiguration<PersonnelServiceModel>
    {
        public void Configure(EntityTypeBuilder<PersonnelServiceModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
