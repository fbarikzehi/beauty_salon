using Beauty.Application.Modules.Calendar.Mapper;
using Beauty.Application.Modules.Calendar.Messaging;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Calendar.Implementation
{
    public class CalendarService : ServiceBase<CoreDbContext>, ICalendarService
    {
        public async Task<CalendarFindByYearResponse> FindByYear(CalendarFindByYearRequest request)
        {
            var response = new CalendarFindByYearResponse();
            try
            {
                var calendar = DbContext.Calendars.Include(c => c.CalendarDates).FirstOrDefault(c => c.Year == request.Year);
                if (calendar != null)
                    response.Entity = calendar.ToFindViewModel();

                response.IsSuccess = true;
                response.AlertType = ResponseAlertResource_en.Success;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionResource_fa.Exception;
                response.AlertType = ResponseAlertResource_en.Danger;
                return response;
            }
        }
    }
}
