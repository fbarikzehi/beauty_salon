using Beauty.Model.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Appointment
{
    public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentModel>
    {
        public void Configure(EntityTypeBuilder<AppointmentModel> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
