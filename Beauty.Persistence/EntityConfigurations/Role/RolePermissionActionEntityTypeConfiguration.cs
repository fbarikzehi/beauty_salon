using Beauty.Model.Account.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Persistence.EntityConfigurations.Role
{
    internal class RolePermissionActionEntityTypeConfiguration : IEntityTypeConfiguration<RolePermissionActionModel>
    {
        void IEntityTypeConfiguration<RolePermissionActionModel>.Configure(EntityTypeBuilder<RolePermissionActionModel> builder)
        {
            //builder.HasOne(x => x.RolePermission)
            //           .WithMany(x => x.RolePermissionActions)
            //           .HasForeignKey(x => x.PermissionAction);
        }
    }
}
