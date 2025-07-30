using Beauty.Application.Modules.Setting.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Setting.Implementation
{
    public interface ISettingService
    {
        Task<FindResponse> Find(FindRequest request);
    }
}