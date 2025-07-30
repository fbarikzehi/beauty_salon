using Beauty.Model.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Appointment
{
    public class AppointmentServiceEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentServiceModel>
    {
        public void Configure(EntityTypeBuilder<AppointmentServiceModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.HasOne(p => p.PersonnelProfile)
                   .WithMany(a => a.AppointmentServices)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
