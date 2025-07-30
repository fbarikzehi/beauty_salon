using Beauty.Resource;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Salon.ViewModel
{
    public class SalonWorkingDateTimeViewModel
    {
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Date")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Date { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "FromTime")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string FromTime { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "ToTime")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string ToTime { get; set; }
    }
}
