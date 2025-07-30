using Beauty.Application.Modules.Account.User.Mapping;
using Beauty.Application.Modules.Account.User.Messaging;
using Beauty.Model.Account.User;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Common.Crosscutting.Utility;
using Common.Security;
using Common.Security.Claim;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Account.User.Implementation
{
    public class UserService : ServiceBase<CoreDbContext>, IUserService
    {

        public IPasswordHasher PasswordHasher { get; set; } = new PasswordHasher();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var response = new LoginResponse();
            try
            {
                if (request.Entity.IsMobileRequest && string.IsNullOrWhiteSpace(request.Entity.DeviceId))
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.MobileDeviceNotRecognized;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }

                if (DbContext.Users.Any(x => x.Username.Equals(request.Entity.Username) && !x.IsDeleted))
                {
                    var user = await DbContext.Users.Include(x => x.Roles).ThenInclude(x => x.Role).ThenInclude(x => x.RolePermissions).FirstOrDefaultAsync(x => x.Username.Equals(request.Entity.Username));

                    string hashedPassword = PasswordHasher.HashPassword(request.Entity.Password);
                    var verificationResult = PasswordHasher.VerifyHashedPassword(user.HashedPassword, request.Entity.Password);
                    if (verificationResult == PasswordVerificationResultEnum.Failed)
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.WrongPassword;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                    if (user.IsLocked)
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.UserIsLocked;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                    if (!request.Entity.IsMobileRequest && user.Roles.FirstOrDefault()?.Role.RolePermissions.Count == 0)
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.RolePermissionsNotDefined;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                    if (request.Entity.IsMobileRequest && !user.Roles.Any(x => x.Role.Title.Equals(request.Entity.Role)))
                    {
                        response.IsSuccess = false;
                        response.Message = MessagingResource_fa.MobileNotUserInRole;
                        response.AlertType = ResponseAlertResource_en.Danger;
                        return response;
                    }
                    //if (request.Entity.IsMobileRequest && !string.IsNullOrWhiteSpace(user.DeviceId) && !user.DeviceId.Equals(request.Entity.DeviceId))
                    //{
                    //    response.IsSuccess = false;
                    //    response.Message = MessagingResource_fa.MaxDeviceReached;
                    //    response.AlertType = ResponseAlertResource_en.Danger;
                    //    return response;
                    //}
                    if (request.Entity.IsMobileRequest && string.IsNullOrWhiteSpace(user.DeviceId))
                    {
                        user.DeviceId = request.Entity.DeviceId;
                        user.DeviceType = request.Entity.DeviceType;
                        if (string.IsNullOrWhiteSpace(user.Token))
                        {
                            user.Token = RandomUtility.GetAlphaNumRandomString(30);
                        }
                    }
                    user.LastLogin = DateTime.Now;

                    var role = user.Roles.FirstOrDefault()?.Role;
                    if (request.Entity.IsMobileRequest)
                    {
                        if (role.Title.Equals("Personnel"))
                        {
                            var personnel = DbContext.Personnels.FirstOrDefault(x => x.UserId == user.Id);
                            if (personnel != null)
                            {
                                response.PersonnelId = personnel.Id;
                                if (string.IsNullOrEmpty(user.FullName))
                                    user.FullName = $"{personnel.Name} {personnel.LastName}";

                            }
                        }
                        else if (role.Title.Equals("Customer"))
                        {
                            var customer = DbContext.Customers.FirstOrDefault(x => x.UserId == user.Id);
                            if (customer != null)
                            { 
                                response.CustomerId = customer.Id;
                                if (string.IsNullOrEmpty(user.FullName))
                                    user.FullName = $"{customer.Name} {customer.LastName}";
                            }
                        }
                        response.FullName = user.FullName;
                        response.Token = user.Token;
                    }
                    else
                    {
                        response.Claims = new List<Claim>
                        {
                           new Claim(ClaimOptions.UserIdClaimType, user.Id.ToString()),
                           new Claim(ClaimOptions.UserFullNameClaimType, user.FullName),
                           new Claim(ClaimOptions.RoleIdClaimType, user.Roles.FirstOrDefault()?.Role.Id.ToString()),
                           new Claim(ClaimOptions.RoleTitleClaimType, user.Roles.FirstOrDefault()?.Role.Title),
                           new Claim(ClaimOptions.RolePersianTitleClaimType, user.Roles.FirstOrDefault()?.Role.PersianTitle),
                        };
                        response.RedirectUrl = user.Roles.FirstOrDefault()?.Role.HomeUrl;
                    }
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.LoginSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.WrongUsername;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.Error = ex;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<CreateResponse> Create(CreateRequest request)
        {
            var response = new CreateResponse();
            try
            {
                if (!DbContext.Users.Any(x => x.Username.Equals(request.Entity.Username)))
                {
                    var user = DbContext.Users.Add(request.Entity.ToCreateModel(_httpContextAccessor));
                    DbContext.SaveChanges();

                    response.Id = user.Entity.Id;
                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.UserCreateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.UserExist;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<UpdateResponse> Update(UpdateRequest request)
        {
            var response = new UpdateResponse();
            try
            {
                if (DbContext.Users.Any(x => x.Id == request.Entity.Id))
                {
                    var user = DbContext.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == request.Entity.Id);
                    user = request.Entity.ToUpdateModel(user);
                    DbContext.Entry(user).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.UserUpdateSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.UserNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<DeleteResponse> Delete(DeleteRequest request)
        {
            var response = new DeleteResponse();
            try
            {
                if (DbContext.Users.Any(x => x.Id == request.Id))
                {
                    var user = DbContext.Users.FirstOrDefault(x => x.Id == request.Id);
                    user.ReverseDelete();
                    DbContext.Entry(user).State = EntityState.Modified;
                    DbContext.SaveChanges();

                    response.IsSuccess = true;
                    response.Message = MessagingResource_fa.UserDeleteSucceed;
                    response.AlertType = ResponseAlertResource_en.Success;
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = MessagingResource_fa.UserNotFound;
                    response.AlertType = ResponseAlertResource_en.Danger;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }

        public async Task<UserFindAllByPagingResponse> FindAllByPaging(UserFindAllByPagingRequest request)
        {
            var response = new UserFindAllByPagingResponse();
            try
            {
                var dbset = DbContext.Users.Include(x => x.Roles).OrderByDescending(x => x.CreateDateTime).AsQueryable();
                Expression<Func<UserModel, bool>> predicate = null;


                if (request.Search?.Value.Length > 0 && !string.IsNullOrEmpty(request.Search?.Value))
                {
                    predicate = user => user.FullName.Contains(request.Search.Value) ||
                                           user.Username.Contains(request.Search.Value);
                }

                var entities = predicate is null ?
                          dbset.Skip(request.Start).Take(request.Length) :
                          dbset.AsExpandable().Where(predicate).Skip(request.Start).Take(request.Length);

                response.Data = entities.ToFindAllViewModel();
                response.Draw = request.Draw;
                response.RecordsTotal = dbset.Count();
                response.RecordsFiltered = dbset.Count();
                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }
    }
}
