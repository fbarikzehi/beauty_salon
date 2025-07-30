using Beauty.Application.Modules.Account.Permission.Mapping;
using Beauty.Application.Modules.Account.Permission.Messaging;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Account.Permission.Implementation
{
    public class PermissionService : ServiceBase<CoreDbContext>, IPermissionService
    {
        public async Task<PermissionFindAllResponse> FindAll(PermissionFindAllRequest request)
        {
            var response = new PermissionFindAllResponse();
            try
            {
                var permissions = DbContext.Permissions.Include(x => x.SubPermission).Include(x => x.SubPermissions).Include(x => x.Actions).Where(x => x.SubPermissionId == null).AsQueryable().ToFindAllViewModel();

                response.Data = permissions;
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
