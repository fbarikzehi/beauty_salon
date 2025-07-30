using Beauty.Application.Modules.Salon.ViewModel;
using Beauty.Model.Salon;
using Common.Crosscutting.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Salon.Mapping
{
    public static class SalonContactMapper
    {
        public static List<SalonContactModel> ToCreateModel(this List<SalonContactViewModel> salonContacts)
        {
            if (salonContacts == null) return new List<SalonContactModel>();

            return salonContacts.Select(x => new SalonContactModel
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Value = x.Value,
                Type = x.Type,
            }).ToList();
        }

        public static List<SalonContactViewModel> ToFindViewModel(this ICollection<SalonContactModel> salonContacts)
        {
            if (salonContacts == null) return new List<SalonContactViewModel>();

            return salonContacts.Select(x => new SalonContactViewModel
            {
                Id = x.Id,
                Value = x.Value,
                Type = x.Type,
                IsActive = x.IsActive
            }).ToList();
        }
    }
}
