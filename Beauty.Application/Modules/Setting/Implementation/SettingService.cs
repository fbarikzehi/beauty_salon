using Beauty.Application.Modules.Setting.Mapping;
using Beauty.Application.Modules.Setting.Messaging;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Setting.Implementation
{
    public class SettingService : ServiceBase<CoreDbContext>, ISettingService
    {
        public async Task<FindResponse> Find(FindRequest request)
        {
            var response = new FindResponse();
            try
            {
                response.Entity = DbContext.Settings.FirstOrDefault().ToFindViewModel();
                response.IsSuccess = true;
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
