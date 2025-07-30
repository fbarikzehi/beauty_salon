using System.ComponentModel.DataAnnotations;

namespace Common.Application.ValidationAttributes
{
    public class PriceOnlyAttribute : ValidationAttribute
    {
        private string _price;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            _price = value?.ToString().Trim('_').Replace(",", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty).Replace(".", string.Empty).Trim('_');
            if (string.IsNullOrWhiteSpace(_price))
                return ValidationResult.Success;

            if (int.TryParse(_price, out int _))
                return ValidationResult.Success;

            return new ValidationResult(GetErrorMessage());
        }


        public string GetErrorMessage() => $"قیمت {_price} را فرمت درست وارد کنید";
    }
}
