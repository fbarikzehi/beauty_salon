using Beauty.Resource;
using Common.Application.ViewModelBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Personnel.ViewModel
{
    public class PersonnelAccountViewModel
    {
        public Guid UserId { get; set; }    
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Username")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        //[MinLength(6,ErrorMessageResourceType = typeof(DataAnnotationResource_fa),ErrorMessageResourceName = "MinLength")]
        public string Username { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Password")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        //[StringLength(100,ErrorMessageResourceType = typeof(DataAnnotationResource_fa),ErrorMessageResourceName = "StringLength",MinimumLength = 6)]
        public string Password { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "ConfirmPassword")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        //[StringLength(100,ErrorMessageResourceType = typeof(DataAnnotationResource_fa),ErrorMessageResourceName = "StringLength",MinimumLength = 6)]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Compare")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
        public bool IsLocked { get; set; }
        public string LockDescription { get; set; }
        public string LockDateTime { get; set; }
        public string LockExpirationDatetime { get; set; }
        public string LastLogin { get; set; }
        public Guid PersonnelId { get; set; }
        public short RoleId { get; set; }
    }
}
