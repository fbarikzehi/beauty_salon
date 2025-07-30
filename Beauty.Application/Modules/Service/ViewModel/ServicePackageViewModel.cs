using Beauty.Resource;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Service.ViewModel
{
    public class ServicePackageViewModel
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Title { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string ExtendTo { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "DiscountPrice")]
        public string DiscountPrice { get; set; } = "0";

        public List<ServicePackageItemViewModel> Items { get; set; }
    }
}
