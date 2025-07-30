using Beauty.Model.Account.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Beauty.Persistence.EntityConfigurations.Role
{
    internal class RoleEntityTypeConfiguration : IEntityTypeConfiguration<RoleModel>
    {
        void IEntityTypeConfiguration<RoleModel>.Configure(EntityTypeBuilder<RoleModel> builder)
        {
            //builder.HasData(new List<RoleModel>
            //{
            //    new RoleModel { Id = 1, PersianTitle = "مدیر سیستم", Title = "Admin", HomeUrl = "/Home/Index" },
            //    new RoleModel { Id = 2, PersianTitle = "مدیریت", Title = "Manager", HomeUrl = "/Home/Index" },
            //    new RoleModel { Id = 3, PersianTitle = "پرسنل", Title = "Personnel", HomeUrl = "/Home/Index" },
            //    new RoleModel { Id = 4, PersianTitle = "مشتری", Title = "Customer", HomeUrl = "/Home/Index"},
            //    new RoleModel { Id = 5, PersianTitle = "کاربر", Title = "NoneUser", HomeUrl = "/Home/Index" },
            //});
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
