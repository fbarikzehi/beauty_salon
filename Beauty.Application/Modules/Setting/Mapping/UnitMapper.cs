using Beauty.Application.Modules.Setting.ViewModel;
using Beauty.Model.Setting;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Setting.Mapping
{
    public static class UnitMapper
    {
        public static List<UnitViewModel> ToFindAllViewModel(this IQueryable<UnitModel> model)
        {
            return model.Select(x => new UnitViewModel
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();
        }
    }
}
