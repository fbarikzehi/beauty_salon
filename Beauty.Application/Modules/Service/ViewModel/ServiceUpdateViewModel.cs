using Beauty.Resource;
using Common.Application.ValidationAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Service.ViewModel
{
    public class ServiceUpdateViewModel
    {
        public short Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Title { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "DurationMinutes")]
        [NumberOnly(true)]
        public string DurationMinutes { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Price")]
        [PriceOnly]
        public string Price { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Score")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        [NumberOnly(true)]
        public string Score { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "TakeItFreeCount")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        [NumberOnly(true)]
        public string TakeItFreeCount { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Prepayment")]
        [PriceOnly]
        public string Prepayment { get; set; }

        [Display(ResourceType = typeof(DisplayResource_fa), Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(DisplayResource_fa), Name = "CurrentPrice")]
        public string CurrentPrice { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Line")]
        public short LineId { get; set; }

        public List<ServiceDetailViewModel> Details { get; set; } = new List<ServiceDetailViewModel>();
    }
}
