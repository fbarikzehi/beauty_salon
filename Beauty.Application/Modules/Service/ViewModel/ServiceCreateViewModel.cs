using Beauty.Resource;
using Common.Application.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Service.ViewModel
{
    public class ServiceCreateViewModel
    {
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Title { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "DurationMinutes")]
        [NumberOnly(true)]
        public string DurationMinutes { get; set; } = "0";
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Price")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        [PriceOnly]
        public string Price { get; set; } = "0";
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Score")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        [NumberOnly(true)]
        public string Score { get; set; } = "0";
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "TakeItFreeCount")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        [NumberOnly(true)]
        public string TakeItFreeCount { get; set; } = "0";
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Prepayment")]
        [PriceOnly]
        public string Prepayment { get; set; } = "0";
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Line")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public short LineId { get; set; }

        public List<ServiceDetailViewModel> Details { get; set; } = new List<ServiceDetailViewModel>();
    }
}
