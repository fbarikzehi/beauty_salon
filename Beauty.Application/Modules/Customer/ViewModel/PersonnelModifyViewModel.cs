using Beauty.Application.Modules.Account.User.ViewModel;
using Beauty.Resource;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Customer.ViewModel
{
    public class CustomerModifyViewModel
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "LastName")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string LastName { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Birthdate")]
        public string Birthdate { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "AvatarFile")]
        public IFormFile AvatarFile { get; set; }
        public string MemberCode { get; set; }
        public string Avatar { get; set; }
        public UserViewModel User { get; set; } = new UserViewModel();
        public virtual List<CustomerContactViewModel> Contacts { get; set; } = new List<CustomerContactViewModel> { new CustomerContactViewModel() };
        public Guid? UserId { get; set; }
    }
}
