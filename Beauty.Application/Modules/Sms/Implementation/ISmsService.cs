using Beauty.Application.Modules.Sms.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Sms.Implementation
{
    public interface ISmsService
    {
        Task<SmsUpdateResponse> Update(SmsUpdateRequest request);
        Task<SmsCreateResponse> Create(SmsCreateRequest request);
        Task<SmsFindByIdResponse> FindById(SmsFindByIdRequest request);
        Task<SmsFindAllResponse> FindAll(SmsFindAllRequest request);
    }
}