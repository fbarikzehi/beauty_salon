using Beauty.Application.Modules.Service.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Service.Implementation
{
    public interface IServiceService
    {
        Task<CreateResponse> Create(CreateRequest request);
        Task<DeleteResponse> Delete(DeleteRequest request);
        Task<UpdateActiveResponse> UpdateActive(UpdateActiveRequest request);
        Task<FindAllByPageResponse> FindAllByPage(FindAllByPageRequest request);
        Task<FindAllResponse> FindAll(FindAllRequest request);
        Task<FindByIdResponse> FindById(FindByIdRequest request);
        Task<UpdateResponse> Update(UpdateRequest request);
        Task<FindAllBySearchResponse> FindAllBySearch(FindAllBySearchRequest request);
        Task<FindAllBySearchResponse> SearchAllByName(FindAllBySearchRequest request);

        Task<ServicePackageCreateResponse> CreateServicePackage(ServicePackageCreateRequest request);
        Task<ServicePackageUpdateResponse> UpdateServicePackage(ServicePackageUpdateRequest request);
        Task<ServicePackageDeleteResponse> ServicePackageDelete(ServicePackageDeleteRequest request);
        Task<ServicePackageDeleteServiceResponse> ServicePackageDeleteService(ServicePackageDeleteServiceRequest request);
        Task<ServicePackageFindByIdResponse> ServicePackageFindById(ServicePackageFindByIdRequest request);
        Task<ServicePackageFindByPagingResponse> ServicePackageFindByPaging(ServicePackageFindByPagingRequest request);

    }
}