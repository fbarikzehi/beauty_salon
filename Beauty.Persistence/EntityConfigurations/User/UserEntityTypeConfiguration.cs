using Beauty.Model.Account.User;
using Common.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Beauty.Persistence.EntityConfigurations.User
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public IPasswordHasher PasswordHasher { get; set; } = new PasswordHasher();

        void IEntityTypeConfiguration<UserModel>.Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            //builder.HasData(new List<UserModel> {
            //new UserModel
            //{
            //    Id = new Guid("3C92559B-7154-4220-8E67-949A54D4327F"),
            //    HashedPassword = PasswordHasher.HashPassword("legend_4121"),
            //    Username = "admin",
            //},
            //new UserModel
            //{
            //    Id = new Guid("AC3092EF-2A8F-439C-9354-47D6FAB98E3A"),
            //    HashedPassword = PasswordHasher.HashPassword("legend_4121"),
            //    Username = "manager",
            //}
            //});
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
