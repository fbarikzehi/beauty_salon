using Beauty.Application.Modules.Customer.ViewModel;
using Beauty.Model.Customer;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Customer.Mapping
{
    public static class CustomerProfileContactMapper
    {
        public static List<CustomerContactModel> ToCreateModel(this List<CustomerContactViewModel> customerContacts)
        {
            if (customerContacts == null) return new List<CustomerContactModel>();

            return customerContacts.Select(x => new CustomerContactModel
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Value = x.Value,
                Type = x.Type,
            }).ToList();
        }

        public static List<CustomerContactViewModel> ToFindViewModel(this ICollection<CustomerContactModel> personnelContacts)
        {
            if (personnelContacts == null) return new List<CustomerContactViewModel>();

            return personnelContacts.Select(x => new CustomerContactViewModel
            {
                Id = x.Id,
                Value = x.Value,
                Type = x.Type,
                IsActive = x.IsActive
            }).ToList();
        }
    }
}
