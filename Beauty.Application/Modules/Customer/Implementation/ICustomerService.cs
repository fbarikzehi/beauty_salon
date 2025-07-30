using Beauty.Application.Modules.Customer.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Customer.Implementation
{
    public interface ICustomerService
    {
        Task<CreateResponse> Create(CreateRequest request);
        Task<UpdateResponse> Update(UpdateRequest request);
        Task<FindByIdResponse> FindById(FindByIdRequest request);
        Task<FindAllBySearchResponse> FindAllBySearch(FindAllBySearchRequest request);
        Task<FindAllByPagingResponse> FindAllByPaging(FindAllByPagingRequest request);
        Task<DeleteByIdResponse> DeleteById(DeleteByIdRequest request);
        Task<DeleteContactResponse> DeleteContact(DeleteContactRequest request);
        Task<CustomerChequeCreateResponse> ChequeCreate(CustomerChequeCreateRequest request);
        Task<CustomerChequeFindAllByCreateDateResponse> ChequeFindAllByCreateDate(CustomerChequeFindAllByCreateDateRequest request);
    }
}