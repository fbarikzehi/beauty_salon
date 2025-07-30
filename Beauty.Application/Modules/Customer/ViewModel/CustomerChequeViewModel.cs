using Beauty.Resource;
using System;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Customer.ViewModel
{
    public class CustomerChequeViewModel
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Number")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Number { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Fee")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Fee { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Date")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Date { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Bank")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public short BankId { get; set; }
        public string BankName { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Customer")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public Guid CustomerProfileId { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Details")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Details { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "CreateDateTime")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string CreateDateTime { get; set; }
    }
}
