using Beauty.Resource;
using System;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Salon.ViewModel
{
    public class SalonUpdateOpeningAndClosingViewModel
    {
        public Guid SalonId { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "OpeningTime")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string OpeningTime { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "ClosingTime")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string ClosingTime { get; set; }
    }
}
