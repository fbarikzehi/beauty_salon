using Beauty.Application.Modules.Customer.ViewModel;
using Microsoft.AspNetCore.Http;
using Beauty.Model.Customer;
using Common.Security.Claim;
using Common.Crosscutting.Utility;
using System.Linq;
using System.Collections.Generic;
using Common.Crosscutting.Enum;
using Beauty.Application.Modules.Account.User.Mapping;

namespace Beauty.Application.Modules.Customer.Mapping
{
    public static class CustomerProfileMapper
    {
        public static CustomerProfileModel ToCreateModel(this CustomerModifyViewModel customer, IHttpContextAccessor httpContextAccessor, string memberCode)
        {
            return new CustomerProfileModel
            {
                Name = customer.Name,
                LastName = customer.LastName,
                Birthdate = customer.Birthdate.PersianDateStringToAllowNullDateTime(),
                Avatar = customer.Avatar,
                MemberCode = memberCode,
                UserId = customer.UserId,
                Contacts = customer.Contacts.ToCreateModel(),
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
        }
        public static CustomerProfileModel ToUpdateModel(this CustomerModifyViewModel customer, CustomerProfileModel model, IHttpContextAccessor httpContextAccessor)
        {
            model.Birthdate = customer.Birthdate.PersianDateStringToAllowNullDateTime();
            model.LastName = customer.LastName;
            model.Name = customer.Name;
            model.Avatar = customer.AvatarFile is null ? model.Avatar : customer.Avatar;
            foreach (var c in customer.Contacts.Where(c => !string.IsNullOrEmpty(c.Value)))
            {
                if (model.Contacts.Any(x => x.Type == c.Type))
                {
                    model.Contacts.FirstOrDefault(x => x.Type == c.Type).Value = c.Value;
                    model.Contacts.FirstOrDefault(x => x.Type == c.Type).IsActive = c.IsActive;
                }
                else
                    model.Contacts.Add(new CustomerContactModel { Type = c.Type, Value = c.Value });
            }
            return model;
        }

        public static List<CustomerSearchViewModel> ToFindAllBySearchViewModel(this IEnumerable<CustomerProfileModel> customers)
        {
            if (customers is null || customers.Count() == 0) return new List<CustomerSearchViewModel>();

            return customers.Select(customer => new CustomerSearchViewModel
            {
                Id = customer.Id,
                FullName = $"{customer.Name} {customer.LastName}",
                MemberCode = customer.MemberCode,
                Avatar = string.IsNullOrEmpty(customer.Avatar) ? "/images/avatar-placeholder.png" : customer.Avatar,
                Mobile = customer.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile)?.Value
            }).ToList();
        }

        public static List<CustomerDataTableViewModel> ToDataTableViewModel(this IQueryable<CustomerProfileModel> customerProfiles)
        {
            return customerProfiles.Select(customerProfile => new CustomerDataTableViewModel
            {
                Id = customerProfile.Id,
                FullName = $"{customerProfile.Name} {customerProfile.LastName}",
                Code = customerProfile.MemberCode,
                Username = customerProfile.User == null ? string.Empty : customerProfile.User.Username,
                Mobile = customerProfile.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile && x.IsActive) == null ? string.Empty :
                                               customerProfile.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile && x.IsActive).Value,
            }).ToList();
        }

        public static CustomerProfileViewModel ToFindByIdModel(this CustomerProfileModel m)
        {
            return new CustomerProfileViewModel
            {
                Id = m.Id,
                MemberCode = m.MemberCode,
                Avatar = m.Avatar,
                Name = m.Name,
                LastName = m.LastName,
                Birthdate = m.Birthdate.ToShortPersianDateNullable(""),
                Contacts = m.Contacts.ToFindViewModel(),
                User = m.User.ToFindViewModel(),
            };
        }

    }
}
