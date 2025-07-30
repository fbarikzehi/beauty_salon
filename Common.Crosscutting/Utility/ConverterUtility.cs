namespace Common.Crosscutting.Utility
{
    public static class ConverterUtility
    {
        public static float ToFloatPrice(this string price)
        {
            price = price?.ToString().Trim('_').Replace(",", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty).Replace(".", string.Empty).Trim('_');
            if (string.IsNullOrWhiteSpace(price))
                return 0;

            if (float.TryParse(price, out float _))
                return float.Parse(price);

            return 0;
        }
    }
}
