using Beauty.Model.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Appointment
{
    public class AppointmentDiscountEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentDiscountModel>
    {
        public void Configure(EntityTypeBuilder<AppointmentDiscountModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
