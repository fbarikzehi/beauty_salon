using Beauty.Application.Modules.Setting.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Setting.Implementation
{
    public interface IProductService
    {
        Task<ProductCreateResponse> Create(ProductCreateRequest request);
        Task<ProductUpdateResponse> Update(ProductUpdateRequest request);
        Task<ProductDeleteByIdResponse> Delete(ProductDeleteByIdRequest request);
        Task<ProductFindByIdResponse> FindById(ProductFindByIdRequest request);
        Task<ProductFindAllByPagingResponse> FindAllByPaging(ProductFindAllByPagingRequest request);
        Task<ProductImageDeleteResponse> DeleteImage(ProductImageDeleteRequest request);
    }
}