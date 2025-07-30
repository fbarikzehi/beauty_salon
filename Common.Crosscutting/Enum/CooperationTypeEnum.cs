using System.ComponentModel.DataAnnotations;

namespace Common.Crosscutting.Enum
{
    public enum CooperationTypeEnum
    {
        [Display(Name = "حقوقی")]
        Salary,
        [Display(Name = "درصدی")]
        Percentage,
        [Display(Name = "حقوقی و درصدی")]
        SalaryPercentage
    }
}
