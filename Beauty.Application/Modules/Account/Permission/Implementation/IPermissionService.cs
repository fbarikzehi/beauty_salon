using Beauty.Application.Modules.Account.Permission.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Account.Permission.Implementation
{
    public interface IPermissionService
    {
        Task<PermissionFindAllResponse> FindAll(PermissionFindAllRequest request);
    }
}