using Beauty.Resource;
using Common.Crosscutting.Enum;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Account.User.ViewModel
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false,ErrorMessageResourceType = typeof(DataAnnotationResource_fa),ErrorMessageResourceName = "Required")]
        [MinLength(6,ErrorMessageResourceType = typeof(DataAnnotationResource_fa),ErrorMessageResourceName = "MinLength")]
        [Display(Name = "Username", ResourceType = typeof(DisplayResource_fa))]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessageResourceType = typeof(DataAnnotationResource_fa),ErrorMessageResourceName = "Required")]
        [StringLength(100,ErrorMessageResourceType = typeof(DataAnnotationResource_fa),ErrorMessageResourceName = "StringLength",MinimumLength = 6)]
        [Display(Name = "Password", ResourceType = typeof(DisplayResource_fa))]
        public string Password { get; set; }
        [Display(Name = "RememberMe", ResourceType = typeof(DisplayResource_fa))]
        public bool RememberMe { get; set; }
        public bool IsMobileRequest { get; set; }
        public string DeviceId { get; set; }
        public DeviceEnumType DeviceType { get; set; }
        public string Role { get; set; }
    }
}
