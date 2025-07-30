using Beauty.Model.Customer;
using Beauty.Resource;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Customer.ViewModel
{
    public class CustomerProfileCreateViewModel
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "LastName")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string LastName { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Birthdate")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Birthdate { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "AvatarFile")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public IFormFile AvatarFile { get; set; }
        public string AvatarUrl { get; set; }
        public Guid? UserId { get; set; }
        public List<CustomerContactCreateViewModel> Contacts { get; set; }
    }
}
