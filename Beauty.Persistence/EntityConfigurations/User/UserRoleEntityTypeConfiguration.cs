using Beauty.Model.Account.User;
using Common.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Beauty.Persistence.EntityConfigurations.User
{
    internal class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRoleModel>
    {
        public IPasswordHasher PasswordHasher { get; set; } = new PasswordHasher();

        void IEntityTypeConfiguration<UserRoleModel>.Configure(EntityTypeBuilder<UserRoleModel> builder)
        {
            builder.HasAlternateKey(x => x.UserId);
            //builder.HasData(new List<UserRoleModel> {
            //     new UserRoleModel {Id=1, RoleId = 1, UserId = new Guid("3C92559B-7154-4220-8E67-949A54D4327F") },
            //     new UserRoleModel {Id=2, RoleId = 2, UserId = new Guid("AC3092EF-2A8F-439C-9354-47D6FAB98E3A") },
            //});
        }
    }
}
