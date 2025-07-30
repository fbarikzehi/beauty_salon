using Beauty.Application.Modules.Setting.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Setting.Implementation
{
    public interface IUnitService
    {
        Task<UnitFindAllResponse> FindAll(UnitFindAllRequest request);
    }
}