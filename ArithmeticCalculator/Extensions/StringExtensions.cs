using System;

namespace ArithmeticCalculator.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveWhitespace(this string str) {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
