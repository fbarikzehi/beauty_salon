using Beauty.Model.Personnel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Beauty.Persistence.EntityConfigurations.Personnel
{
    public class PersonnelProfileEntityTypeConfiguration : IEntityTypeConfiguration<PersonnelProfileModel>
    {
        public void Configure(EntityTypeBuilder<PersonnelProfileModel> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
