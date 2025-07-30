using System.ComponentModel.DataAnnotations;

namespace Common.Application.ValidationAttributes
{
    public class RequiredGuidAttribute : ValidationAttribute
    {
        private bool _allowEmpty;

        public RequiredGuidAttribute(bool allowEmpty)
        {
            _allowEmpty = allowEmpty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage() => $"";
    }
}
