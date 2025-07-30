using Beauty.Resource;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Personnel.ViewModel
{
    public class PersonnelProfileModifyViewModel
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
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "JobStart")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string JobStart { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "JobEnd")]
        public string JobEnd { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Address")]
        public string Address { get; set; }
        public string Avatar { get; set; } = "/images/avatar-placeholder.png";
        public IFormFile FileAvatar { get; set; }
        public List<PersonnelContactViewModel> Contacts { get; set; } = new List<PersonnelContactViewModel> { new PersonnelContactViewModel() };
        public string Code { get; set; }
    }

}
