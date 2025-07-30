using Beauty.Resource;
using Common.Application.ViewModelBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Account.User.ViewModel
{
    public class UserViewModel : ViewModelBase<Guid>
    {
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "FullName")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string FullName { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Username")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Username { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Password")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Password { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "ConfirmPassword")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
        public bool IsLocked { get; set; }
        public string LockDescription { get; set; }
        public string LockDateTime { get; set; }
        public string LockExpirationDatetime { get; set; }
        public string LastLogin { get; set; }
    }
}
