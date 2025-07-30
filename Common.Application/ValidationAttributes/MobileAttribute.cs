using System.ComponentModel.DataAnnotations;

namespace Common.Application.ValidationAttributes
{
    public class MobileAttribute : ValidationAttribute
    {
 

        public MobileAttribute( )
        {
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            return new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage() => $"";
    }
}
