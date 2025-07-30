using Beauty.Application.Modules.Personnel.ViewModel;
using Beauty.Model.Personnel;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Personnel.Mapping
{
    public static class PersonnelContactMapper
    {
        public static List<PersonnelContactModel> ToCreateModel(this List<PersonnelContactViewModel> personnelContacts)
        {
            if (personnelContacts == null) return new List<PersonnelContactModel>();

            return personnelContacts.Select(x => new PersonnelContactModel
            {
                Id=x.Id,
                IsActive = x.IsActive,
                Value = x.Value,
                Type = x.Type,
            }).ToList();
        }

        public static List<PersonnelContactViewModel> ToFindViewModel(this ICollection<PersonnelContactModel> personnelContacts)
        {
            if (personnelContacts == null) return new List<PersonnelContactViewModel>();

            return personnelContacts.Select(x => new PersonnelContactViewModel
            {
                Id = x.Id,
                Value = x.Value,
                Type = x.Type,
                IsActive = x.IsActive
            }).ToList();
        }
    }
}
