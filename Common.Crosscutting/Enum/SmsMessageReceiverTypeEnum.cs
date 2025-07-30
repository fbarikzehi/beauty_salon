using System.ComponentModel.DataAnnotations;

namespace Common.Crosscutting.Enum
{
    public enum SmsMessageReceiverTypeEnum
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "پرسنل")]
        Personnel,
        [Display(Name = "مشتریان")]
        Customer
    }
}
