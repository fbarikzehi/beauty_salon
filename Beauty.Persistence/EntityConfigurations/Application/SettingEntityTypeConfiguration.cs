using Beauty.Model.Application.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Beauty.Persistence.EntityConfigurations.Application
{
    internal class SettingEntityTypeConfiguration : IEntityTypeConfiguration<SettingModel>
    {
        void IEntityTypeConfiguration<SettingModel>.Configure(EntityTypeBuilder<SettingModel> builder)
        {
            builder.HasData(new List<SettingModel>
            {
               new SettingModel { Id = 1,Version="دمو 0.1"},
            });

            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
