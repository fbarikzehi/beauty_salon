using Beauty.Resource;
using Common.Crosscutting.Enum;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Personnel.ViewModel
{
    public class PersonnelContactViewModel
    {
        public short Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Type")]
        public ContactTypeEnum Type { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Value")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Value { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
