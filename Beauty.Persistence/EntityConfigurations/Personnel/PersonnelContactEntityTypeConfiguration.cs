using Beauty.Model.Personnel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Personnel
{
    public class PersonnelContactEntityTypeConfiguration : IEntityTypeConfiguration<PersonnelContactModel>
    {
        public void Configure(EntityTypeBuilder<PersonnelContactModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
