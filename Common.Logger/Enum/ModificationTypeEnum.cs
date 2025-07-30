using System.ComponentModel.DataAnnotations;

namespace Common.Logger.Enum
{
    public enum ModificationTypeEnum
    {
        [Display(Name = "ایجاد")]
        Create,
        [Display(Name = "ویرایش")]
        Update,
        [Display(Name = "حذف")]
        Delete,
        [Display(Name = "مشاهده")]
        Read,
        [Display(Name = "ویرایش فعال/غیر فعال")]
        UpdateActive
    }
}
