using Beauty.Application.Modules.Account.User.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Account.User.Implementation
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<CreateResponse> Create(CreateRequest request);
        Task<UpdateResponse> Update(UpdateRequest request);
        Task<DeleteResponse> Delete(DeleteRequest request);
        Task<UserFindAllByPagingResponse> FindAllByPaging(UserFindAllByPagingRequest request);

    }
}