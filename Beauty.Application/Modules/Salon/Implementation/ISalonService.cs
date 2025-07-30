using Beauty.Application.Modules.Salon.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Salon.Implementation
{
    public interface ISalonService
    {
        Task<FindResponse> Find(FindRequest request);
        Task<UpdateResponse> Update(UpdateRequest request);
        Task<UpdateResponse> UpdateOpeningAndClosingTime(UpdateRequest request);
        Task<DeleteContactResponse> DeleteContact(DeleteContactRequest request);
        Task<UpdateWorkingDateTimesResponse> UpdateWorkingDateTimes(UpdateWorkingDateTimesRequest request);
    }
}
