using Beauty.Model.Personnel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Personnel
{
    public class PersonnelWorkingTimeEntityTypeConfiguration : IEntityTypeConfiguration<PersonnelWorkingTimeModel>
    {
        public void Configure(EntityTypeBuilder<PersonnelWorkingTimeModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
