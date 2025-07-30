using Beauty.Model.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Appointment
{
    public class AppointmentPaymentEntityTypeConfiguration : IEntityTypeConfiguration<AppointmentPaymentModel>
    {
        public void Configure(EntityTypeBuilder<AppointmentPaymentModel> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
