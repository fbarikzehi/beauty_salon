using Beauty.Application.Modules.Account.User.ViewModel;
using System;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Customer.ViewModel
{
    public class CustomerProfileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MemberCode { get; set; }
        public string Birthdate { get; set; }
        public string Avatar { get; set; }
        public UserViewModel User { get; set; } = new UserViewModel();

        public virtual List<CustomerContactViewModel> Contacts { get; set; } = new List<CustomerContactViewModel> { new CustomerContactViewModel() };
    }
}
