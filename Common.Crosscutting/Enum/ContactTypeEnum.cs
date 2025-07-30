using System.ComponentModel.DataAnnotations;

namespace Common.Crosscutting.Enum
{
    public enum ContactTypeEnum
    {
        [Display(Name = "موبایل")]
        Mobile,
        [Display(Name = "تلفن")]
        Phone,
        [Display(Name = "اینستاگرام")]
        Instagram,
        [Display(Name = "تلگرام")]
        Telegram,
        [Display(Name = "واتساب")]
        Whatsapp,
        [Display(Name = "ایمیل")]
        Email,
    }
}
