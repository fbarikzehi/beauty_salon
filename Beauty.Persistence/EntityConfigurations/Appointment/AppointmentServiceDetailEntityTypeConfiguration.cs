using Beauty.Model.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Appointment
{
    public class AppointmentServiceDetailEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentServiceDetailModel>
    {
        public void Configure(EntityTypeBuilder<AppointmentServiceDetailModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
