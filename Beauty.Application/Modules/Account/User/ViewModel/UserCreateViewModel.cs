using Beauty.Resource;
using System;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Account.User.ViewModel
{
    public class UserCreateViewModel
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Username { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Compare")]
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "ConfirmPassword")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string ConfirmPassword { get; set; }
        public bool SendAccountSms { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Role")]
        public short RoleId { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "FullName")]
        public string FullName { get; set; }
    }
}
