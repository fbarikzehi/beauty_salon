using Beauty.Application.Modules.Account.Role.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Account.Role.Implementation
{
    public interface IRoleService
    {
        Task<FindAllResponse> FindAll(FindAllRequest request);
        Task<FindByTitleResponse> FindByTitle(FindByTitleRequest request);
        Task<RolePermissionFindAllResponse> RolePermissionFindAll(RolePermissionFindAllRequest request);
        Task<RolePermissionUpdateFindAllResponse> RolePermissionUpdateFindAll(RolePermissionUpdateFindAllRequest request);
        Task<RolePermissionUpdateResponse> RolePermissionUpdate(RolePermissionUpdateRequest request);
    }
}