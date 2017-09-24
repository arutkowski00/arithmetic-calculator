using System;

namespace ArithmeticCalculator.Exceptions
{
    public class ParseException : Exception
    {
        public ParseException(string message, int column)
            : base($"[Char at {column}] {message}")
        {
        }
    }
}
