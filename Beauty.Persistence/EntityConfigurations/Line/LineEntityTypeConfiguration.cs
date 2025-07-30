using Beauty.Model.Line;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Line
{
    public class LineEntityTypeConfiguration : IEntityTypeConfiguration<LineModel>
    {
        public void Configure(EntityTypeBuilder<LineModel> builder)
        {
            //builder.HasData(new LineModel { Id = 1, Title = "نامشخص" });
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
