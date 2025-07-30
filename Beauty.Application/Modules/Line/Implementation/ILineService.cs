using Beauty.Application.Modules.Line.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Line.Implementation
{
    public interface ILineService
    {
        Task<CreateResponse> Create(CreateRequest request);
        Task<UpdateResponse> Update(UpdateRequest request);
        Task<DeleteResponse> Delete(DeleteRequest request);
        Task<FindAllResponse> FindAll(FindAllRequest request);
    }
}