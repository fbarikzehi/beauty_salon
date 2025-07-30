using Beauty.Application.Modules.Account.Role.Mapping;
using Beauty.Application.Modules.Account.Role.Messaging;
using Beauty.Application.Modules.Account.Role.ViewModel;
using Beauty.Application.Modules.Line.Mapping;
using Beauty.Model.Account.Role;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Account.Role.Implementation
{
    public class RoleService : ServiceBase<CoreDbContext>, IRoleService
    {
        public async Task<FindAllResponse> FindAll(FindAllRequest request)
        {
            var response = new FindAllResponse();
            try
            {
                IQueryable<RoleModel> query = null;
                if (request.Type != null)
                    query = DbContext.Roles.Where(x => x.Type == request.Type && x.Title != "Admin");
                else
                    query = DbContext.Roles.Where(x => x.Title != "Admin");

                response.Data = query.AsQueryable().ToFindAllViewModel();
                response.IsSuccess = true;
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

        public async Task<FindByTitleResponse> FindByTitle(FindByTitleRequest request)
        {
            var response = new FindByTitleResponse();
            try
            {
                response.Entity = DbContext.Roles.FirstOrDefault(x => x.Title == request.Title).ToFindByTitleViewModel();
                response.IsSuccess = true;
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

        public async Task<RolePermissionFindAllResponse> RolePermissionFindAll(RolePermissionFindAllRequest request)
        {
            var response = new RolePermissionFindAllResponse();
            try
            {
                var rolePermissions = DbContext.Roles.Include(x => x.RolePermissions).ThenInclude(x => x.RolePermissionActions).FirstOrDefault(x => x.Id == request.Id)?.RolePermissions;

                var list = new List<RolePermissionViewModel>();
                foreach (var requestPermission in request.Permissions)
                {
                    if (rolePermissions.Any(x => x.PermissionId == requestPermission.Id))
                    {
                        var permission = new RolePermissionViewModel
                        {
                            Id = requestPermission.Id,
                            Icon = requestPermission.Icon,
                            Title = requestPermission.Title,
                            Url = requestPermission.Url,
                            Area = requestPermission.Area,
                            Controller = requestPermission.Controller,
                            Action = requestPermission.Action,
                            RolePermissionActions = new List<RolePermissionActionViewModel>(),
                            RoleSubPermissions = new List<RolePermissionViewModel>(),
                        };
                        foreach (var action in requestPermission.Actions)
                            permission.RolePermissionActions.Add(new RolePermissionActionViewModel { ActionTitle = EnumExtensions<PermissionActionTypeEnum>.GetPersianName(action.ActionType), ActionType = action.ActionType });

                        foreach (var sub in requestPermission.SubPermissions)
                        {
                            if (rolePermissions.Any(x => x.PermissionId == sub.Id) && !list.Any(x => x.Id == sub.Id))
                            {
                                var subPermission = new RolePermissionViewModel
                                {
                                    Icon = sub.Icon,
                                    Title = sub.Title,
                                    Url = sub.Url,
                                    Area = sub.Area,
                                    Controller = sub.Controller,
                                    Action = sub.Action,
                                    RolePermissionActions = new List<RolePermissionActionViewModel>(),
                                };

                                foreach (var action in sub.Actions)
                                    subPermission.RolePermissionActions.Add(new RolePermissionActionViewModel { ActionTitle = EnumExtensions<PermissionActionTypeEnum>.GetPersianName(action.ActionType), ActionType = action.ActionType });
                                permission.RoleSubPermissions.Add(subPermission);
                            }
                        }

                        list.Add(permission);
                    }
                }
                response.Data = list;
                response.IsSuccess = true;
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

        public async Task<RolePermissionUpdateFindAllResponse> RolePermissionUpdateFindAll(RolePermissionUpdateFindAllRequest request)
        {
            var response = new RolePermissionUpdateFindAllResponse();
            try
            {
                var rolePermissions = DbContext.Roles.Include(x => x.RolePermissions).ThenInclude(x => x.RolePermissionActions).FirstOrDefault(x => x.Id == request.Id).ToUpdateViewModel(request.Permissions);
                response.Data = rolePermissions;
                response.IsSuccess = true;
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

        public async Task<RolePermissionUpdateResponse> RolePermissionUpdate(RolePermissionUpdateRequest request)
        {
            var response = new RolePermissionUpdateResponse();
            try
            {
                var role = DbContext.Roles.Include(x => x.RolePermissions).ThenInclude(x => x.RolePermissionActions).FirstOrDefault(x => x.Id == request.RoleId);
                if (role != null)
                {
                    if (!request.SelectedAll)
                    {
                        var subPermissionId = DbContext.Permissions.FirstOrDefault(x => x.Id == request.PermissionId)?.SubPermissionId;
                        if (role.RolePermissions.Count > 0)
                        {
                            if (request.IsSelected)
                            {
                                var rolePermission = role.RolePermissions.FirstOrDefault(x => x.PermissionId == request.PermissionId);
                                if (rolePermission == null)
                                {
                                    if (subPermissionId != null && !role.RolePermissions.Any(x => x.PermissionId == subPermissionId))
                                    {
                                        role.RolePermissions.Add(new RolePermissionModel
                                        {
                                            PermissionId = (short)subPermissionId,
                                            RoleId = request.RoleId,
                                        });
                                    }

                                    role.RolePermissions.Add(new RolePermissionModel
                                    {
                                        PermissionId = request.PermissionId,
                                        RoleId = request.RoleId,
                                        RolePermissionActions = new List<RolePermissionActionModel> { new RolePermissionActionModel { PermissionActionId = request.PermissionActionId } }
                                    });
                                }
                                else
                                {
                                    if (subPermissionId != null && !role.RolePermissions.Any(x => x.PermissionId == subPermissionId))
                                    {
                                        role.RolePermissions.Add(new RolePermissionModel
                                        {
                                            PermissionId = (short)subPermissionId,
                                            RoleId = request.RoleId,
                                        });
                                    }

                                    rolePermission.RolePermissionActions.Add(new RolePermissionActionModel
                                    {
                                        PermissionActionId = request.PermissionActionId
                                    });
                                }
                            }
                            else
                            {
                                var rolePermission = role.RolePermissions.FirstOrDefault(x => x.PermissionId == request.PermissionId && x.RolePermissionActions.Any(s => s.PermissionActionId == request.PermissionActionId));
                                if (rolePermission != null)
                                {
                                    //if (subPermissionId != null && role.RolePermissions.Count(x => x.PermissionId == subPermissionId))
                                    //{
                                    //    var subPermission = role.RolePermissions.FirstOrDefault(x => x.PermissionId == subPermissionId);
                                    //    if (subPermission != null)
                                    //        DbContext.Entry(subPermission).State = EntityState.Deleted;
                                    //}
                                    DbContext.Entry(rolePermission).State = EntityState.Deleted;

                                }
                            }
                        }
                        else
                        {
                            if (request.IsSelected)
                            {
                                if (subPermissionId != null)
                                {
                                    role.RolePermissions.Add(new RolePermissionModel
                                    {
                                        PermissionId = (short)subPermissionId,
                                        RoleId = request.RoleId,
                                    });
                                }

                                role.RolePermissions.Add(new RolePermissionModel
                                {
                                    PermissionId = request.PermissionId,
                                    RoleId = request.RoleId,
                                    RolePermissionActions = new List<RolePermissionActionModel> { new RolePermissionActionModel { PermissionActionId = request.PermissionActionId } }
                                });
                            }
                        }

                    }
                    else
                    {
                        var permissions = DbContext.Permissions.Include(x => x.Actions).ToList();

                        foreach (var permission in permissions)
                        {
                            if (request.IsSelected)
                            {
                                if (!role.RolePermissions.Any(x => x.PermissionId == permission.Id))
                                {
                                    var rp = new RolePermissionModel
                                    {
                                        PermissionId = permission.Id,
                                        RoleId = request.RoleId,
                                        RolePermissionActions = new List<RolePermissionActionModel>()
                                    };
                                    foreach (var action in permission.Actions)
                                        rp.RolePermissionActions.Add(new RolePermissionActionModel { PermissionActionId = action.Id });

                                    role.RolePermissions.Add(rp);
                                }
                                else
                                {
                                    var rolePermission = role.RolePermissions.FirstOrDefault(x => x.PermissionId == permission.Id);
                                    foreach (var action in permission.Actions)
                                        rolePermission.RolePermissionActions.Add(new RolePermissionActionModel { PermissionActionId = action.Id });
                                }

                            }
                            else
                            {
                                if (role.RolePermissions.Count > 0)
                                {
                                    var rolePermission = role.RolePermissions.FirstOrDefault(x => x.PermissionId == permission.Id);
                                    if (rolePermission != null)
                                        DbContext.Entry(rolePermission).State = EntityState.Deleted;

                                }
                            }
                        }

                    }

                    DbContext.SaveChanges();
                    response.Message = MessagingResource_fa.RolePermissionUpdatedSucceed;
                    response.IsSuccess = true;
                    return response;
                }
                response.Message = MessagingResource_fa.RoleNotFound;
                response.IsSuccess = false;
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
