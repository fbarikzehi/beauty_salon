namespace Common.Crosscutting.Utility
{
    public static class TypeUtility
    {
        public static string CommaFormatted { get; set; } = "N0";
        public static string HoursAndMinutes { get; set; } = @"hh\:mm";
        public static string HoursAndMinutesAndSeconds { get; set; } = @"hh\:mm\:ss";

        public static float ToFloatPrice(string price)
        {
            price = price?.ToString().Trim('_').Replace(",", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty).Replace(".", string.Empty).Trim('_');
            if (string.IsNullOrWhiteSpace(price) || !float.TryParse(price, out float _))
                return 0;

            return float.Parse(price);
        }
    }
}
