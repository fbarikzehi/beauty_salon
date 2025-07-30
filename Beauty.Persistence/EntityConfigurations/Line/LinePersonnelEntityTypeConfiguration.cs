using Beauty.Model.Line;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Line
{
    public class LinePersonnelEntityTypeConfiguration : IEntityTypeConfiguration<LinePersonnelModel>
    {
        public void Configure(EntityTypeBuilder<LinePersonnelModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
