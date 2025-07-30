using System.ComponentModel.DataAnnotations;

namespace Common.Crosscutting.Enum
{
    public enum PermissionActionTypeEnum
    {
        [Display(Name = "لیست")]
        List,
        [Display(Name = "ثبت")]
        Create,
        [Display(Name = "ویرایش")]
        Update,
        [Display(Name = "حذف")]
        Delete,
    }
}
