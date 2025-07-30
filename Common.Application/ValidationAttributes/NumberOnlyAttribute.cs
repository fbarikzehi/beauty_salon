using System.ComponentModel.DataAnnotations;

namespace Common.Application.ValidationAttributes
{
    public class NumberOnlyAttribute : ValidationAttribute
    {
        private string _number;
        private bool _allowEmpty;

        public NumberOnlyAttribute(bool allowEmpty)
        {
            _allowEmpty = allowEmpty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            _number = value?.ToString();
            if (_allowEmpty && string.IsNullOrEmpty(_number))
                return ValidationResult.Success;

            if (long.TryParse(_number, out long _))
                return ValidationResult.Success;

            return new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage() => $"مقدار {_number} باید عددی باشد";
    }
}
