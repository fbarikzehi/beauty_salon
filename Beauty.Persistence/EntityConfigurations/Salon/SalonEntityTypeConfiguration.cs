using Beauty.Model.Salon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Beauty.Persistence.EntityConfigurations.Salon
{
    public class SalonEntityTypeConfiguration : IEntityTypeConfiguration<SalonModel>
    {
        public void Configure(EntityTypeBuilder<SalonModel> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            //builder.HasData(new List<SalonModel>
            //{
            //   new SalonModel { Id = Guid.NewGuid(),Logo="/images/dye.png"},
            //});
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
