using Beauty.Application.Modules.Account.User.ViewModel;
using Common.Crosscutting.Enum;
using System;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Personnel.ViewModel
{
    public class PersonnelProfileViewModel
    {
        public Guid Id { get; set; }
        public ushort Code { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string JobStart { get; set; }
        public string JobEnd { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; } = "/images/avatar-placeholder.png";
        public string Salary { get; set; }
        public string Income { get; set; }
        public CooperationTypeEnum CooperationType { get; set; }
        public string CooperationTitle { get; set; }
        public UserViewModel User { get; set; } = new UserViewModel();
        public List<PersonnelContactViewModel> Contacts { get; set; } = new List<PersonnelContactViewModel> { new PersonnelContactViewModel() };
        public List<PersonnelLineViewModel> Lines { get; set; }
    }
}
