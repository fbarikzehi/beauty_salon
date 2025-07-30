using System.ComponentModel.DataAnnotations;

namespace Common.Crosscutting.Enum
{
    public enum HolidayTypeEnum
    {
        [Display(Name = "تعطیلی نیست")]
        None,
        [Display(Name = "قمری")]
        Lunar,
        [Display(Name = "شمسی")]
        Solar,
        [Display(Name = "میلادی")]
        Gregorian
    }
}
