using Beauty.Application.Modules.Setting.Mapping;
using Beauty.Application.Modules.Setting.Messaging;
using Beauty.Persistence.Context;
using Beauty.Resource;
using Common.Application;
using System;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Setting.Implementation
{
    public class UnitService : ServiceBase<CoreDbContext>, IUnitService
    {
        public async Task<UnitFindAllResponse> FindAll(UnitFindAllRequest request)
        {
            var response = new UnitFindAllResponse();
            try
            {
                var units = DbContext.Units.ToFindAllViewModel();

                response.Data = units;
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
