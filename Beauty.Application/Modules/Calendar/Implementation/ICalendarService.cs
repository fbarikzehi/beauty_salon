using Beauty.Application.Modules.Calendar.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Calendar.Implementation
{
    public interface ICalendarService
    {
        Task<CalendarFindByYearResponse> FindByYear(CalendarFindByYearRequest request);
    }
}
