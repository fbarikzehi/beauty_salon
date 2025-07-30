using Beauty.Resource;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Setting.ViewModel
{
    public class ProductModifyViewModel
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Code")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Code { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Unit")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public int UnitId { get; set; }
        public SelectList Units { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Description")]
        public string Description { get; set; }
        public List<ProductImageViewModel> Images { get; set; }
    }
}
