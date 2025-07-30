using Beauty.Model.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Financial
{
    public class FinancialYearEntityTypeConfiguration : IEntityTypeConfiguration<FinancialYearModel>
    {
        public void Configure(EntityTypeBuilder<FinancialYearModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
