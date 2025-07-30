using System.ComponentModel.DataAnnotations;

namespace Common.Crosscutting.Enum
{
    public enum AttendanceTypeEnum
    {
        [Display(Name = "ورود")]
        Entrance,
        [Display(Name = "خروج")]
        Exit
    }
}
