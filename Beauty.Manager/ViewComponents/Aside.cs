using Beauty.Application.Modules.Menu.Implementation;
using Beauty.Application.Modules.Menu.Messaging;
using Beauty.Application.Modules.Salon.Implementation;
using Beauty.Application.Modules.Setting.Implementation;
using Beauty.Application.Modules.Setting.Messaging;
using Beauty.Application.ViewComponentViewModel;
using Common.Crosscutting.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Beauty.Manager.ViewComponents
{
    public class Aside : ViewComponent
    {
        private readonly IMenuService _menuService;
        private readonly ISettingService _settingService;
        private readonly ISalonService _salonService;

        public Aside(IMenuService menuService, ISettingService settingService, ISalonService salonService)
        {
            _menuService = menuService;
            _settingService = settingService;
            _salonService = salonService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = _settingService.Find(new FindRequest()).Result.Entity;
            var salon = _salonService.Find(new Application.Modules.Salon.Messaging.FindRequest()).Result.Entity;
            var viewModel = new AsideViewComponentViewModel
            {
                Menus = _menuService.FindAllActive(new FindAllActiveRequest
                {
                    ApplicationAreaType = ApplicationAreaTypeEnum.Manager
                }).Result.Data,
                SalonTitle = salon.Title,
                SalonLogo = salon.Logo,
                Version = setting.Version,
            };
            return View(viewModel);
        }
    }
}
