using Beauty.Model.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Beauty.Persistence.EntityConfigurations.Setting
{
    internal class UnitEntityTypeConfiguration : IEntityTypeConfiguration<UnitModel>
    {
        void IEntityTypeConfiguration<UnitModel>.Configure(EntityTypeBuilder<UnitModel> builder)
        {
            //builder.HasData(new List<UnitModel> {
            //new UnitModel
            //{
            //    Id = 1,
            //    Title="عدد"
            //},
            //});
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
