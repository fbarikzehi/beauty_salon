using Beauty.Application.Modules.Personnel.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Personnel.Implementation
{
    public interface IPersonnelService
    {
        Task<CreateResponse> Create(CreateRequest request);
        Task<UpdateResponse> Update(UpdateRequest request);
        Task<FindByIdModifyResponse> FindByIdModify(FindByIdModifyRequest request);
        Task<FindByIdResponse> FindById(FindByIdRequest request);
        Task<DeleteContactResponse> DeleteContact(DeleteContactRequest request);
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request);
        Task<FindAllUpdateServicesResponse> FindAllServices(FindAllUpdateServicesRequest request);
        Task<UpdateServicesResponse> UpdateServices(UpdateServicesRequest request);
        Task<UpdateServicesResponse> AddService(UpdateServicesRequest request);
        Task<UpdateServicesResponse> DeleteService(UpdateServicesRequest request);
        Task<UpdateLineResponse> UpdateLine(UpdateLineRequest request);
        Task<FindAllPercentageServicesResponse> FindAllPercentageServices(FindAllPercentageServicesRequest request);
        Task<UpdateFinancialResponse> UpdateFinancial(UpdateFinancialRequest request);
        Task<FindAllByPagingResponse> FindAllByPaging(FindAllByPagingRequest request);
        Task<FindCountAllResponse> FindCountAll(FindCountAllRequest request);
        Task<FindAllSelectResponse> FindAllSelect(FindAllSelectRequest request);
        Task<DeleteByIdResponse> DeleteById(DeleteByIdRequest request);
        Task<DeleteRangeByIdResponse> DeleteRangeById(DeleteRangeByIdRequest request);
        Task<FindAllUpdateWorkingTimeResponse> FindAllUpdateWorkingTime(FindAllUpdateWorkingTimeRequest request);
        Task<UpdateWorkingTimeResponse> UpdateWorkingTime(UpdateWorkingTimeRequest request);
        Task<UpdateRangeWorkingTimeResponse> UpdateRangeWorkingTime(UpdateRangeWorkingTimeRequest request);

    }
}