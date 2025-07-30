using System;
using System.Linq;

namespace Common.Crosscutting.Utility
{
    public static class RandomUtility
    {
        public static string GetAlphaRandomString(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GetAlphaNumRandomString(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetAlphaNumRandomString(int alphaLength, int digitLength)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digits = "0123456789";

            string alpha = new string(Enumerable.Repeat(chars, alphaLength).Select(a => a[random.Next(a.Length)]).ToArray());
            string digit = new string(Enumerable.Repeat(digits, digitLength).Select(d => d[random.Next(d.Length)]).ToArray());
            return $"{alpha}{digit}";
        }

        public static string GetAlphaNumRandomString(string prefix, int digitLength, string postfix)
        {
            Random random = new Random();
            string digits = "0123456789";

            string digit = new string(Enumerable.Repeat(digits, digitLength).Select(d => d[random.Next(d.Length)]).ToArray());
            return $"{prefix}-{digit}{postfix}";
        }

        public static long GetRandomInt(int maxValue)
        {
            Random random = new Random();
            return random.Next(0, maxValue);
        }

        public static ushort GetRandomUshort(ushort maxValue)
        {
            Random random = new Random();
            return (ushort)random.Next(0, maxValue);
        }

        public static string GetRandomShortString(short minValue, short maxValue, int subString)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue).ToString().Substring(0, subString);
        }
    }
}
