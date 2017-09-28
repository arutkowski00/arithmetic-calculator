using System;

namespace ArithmeticCalculator.Exceptions
{
    public class ParseException : Exception
    {
        public ParseException(string message, int column)
            : base($"(char {column}) {message}")
        {
        }
    }
}
