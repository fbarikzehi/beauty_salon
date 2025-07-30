using Beauty.Application.Modules.Account.User.ViewModel;
using Beauty.Model.Account.User;
using Common.Crosscutting.Utility;
using Common.Security;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Account.User.Mapping
{
    public static class UserMapper
    {

        public static UserModel ToCreateModel(this UserCreateViewModel user, IHttpContextAccessor httpContextAccessor)
        {
            var passwordHasher = new PasswordHasher();
            return new UserModel
            {
                FullName = user.FullName,
                Username = user.Username,
                HashedPassword = passwordHasher.HashPassword(user.Password),
                Roles = new List<UserRoleModel> { new UserRoleModel { RoleId = user.RoleId } },
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
        }

        public static UserModel ToUpdateModel(this UserUpdateViewModel user, UserModel entity)
        {
            if (!string.IsNullOrEmpty(user.Username) && user.Username != entity.Username)
                entity.Username = user.Username;

            if (!string.IsNullOrEmpty(user.Password))
            {
                var passwordHasher = new PasswordHasher();
                entity.HashedPassword = passwordHasher.HashPassword(user.Password);
            }

            if (!string.IsNullOrEmpty(user.FullName))
                entity.FullName = user.FullName;

            if (user.RoleId != null)
                entity.Roles.FirstOrDefault().RoleId = (short)user.RoleId;

            return entity;
        }

        public static UserViewModel ToFindViewModel(this UserModel user)
        {
            if (user is null) return new UserViewModel();

            return new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                IsLocked = user.IsLocked,
                LastLogin = user.LastLogin.ToShortPersianDateTimeNullable(""),
                LockDateTime = user.LockDateTime.ToShortPersianDateTimeNullable(""),
                LockExpirationDatetime = user.LockExpirationDatetime.ToShortPersianDateTimeNullable(""),
                LockDescription = user.LockDescription,
            };
        }

        public static List<UserDatatableViewModel> ToFindAllViewModel(this IQueryable<UserModel> users)
        {
            return users.Select(x => new UserDatatableViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Username = x.Username,
                LockStatus = x.IsLocked ? "مسدود" : "ازاد",
                LastLogin = x.LastLogin.ToShortPersianDateTimeNullable(null),
                Role = x.Roles.FirstOrDefault().Role.PersianTitle,
                RoleId = x.Roles.FirstOrDefault().RoleId,
            }).ToList();
        }
    }
}
