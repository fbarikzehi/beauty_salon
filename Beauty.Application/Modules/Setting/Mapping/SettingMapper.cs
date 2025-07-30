using Beauty.Application.Modules.Setting.ViewModel;
using Beauty.Model.Application.Setting;

namespace Beauty.Application.Modules.Setting.Mapping
{
    public static class SettingMapper
    {
        public static SettingViewModel ToFindViewModel(this SettingModel model)
        {
            return new SettingViewModel
            {
                Version = model.Version,
            };
        }
    }
}
