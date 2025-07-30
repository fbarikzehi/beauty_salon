using Beauty.Model.Setting;
using Beauty.Model.Sms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Beauty.Persistence.EntityConfigurations.Sms
{
    internal class SmsMessageEntityTypeConfiguration : IEntityTypeConfiguration<SmsMessageModel>
    {
        void IEntityTypeConfiguration<SmsMessageModel>.Configure(EntityTypeBuilder<SmsMessageModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
