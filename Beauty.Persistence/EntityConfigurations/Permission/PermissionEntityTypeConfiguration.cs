using Beauty.Model.Account.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Permission
{
    public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<PermissionModel>
    {
        public void Configure(EntityTypeBuilder<PermissionModel> builder)
        {
            builder.HasOne(x => x.SubPermission)
                       .WithMany(x => x.SubPermissions)
                       .HasForeignKey(x => x.SubPermissionId);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
