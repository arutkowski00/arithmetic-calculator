using System;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Exceptions
{
    public class UnknownTokenValueException : ParseException
    {
        public UnknownTokenValueException(IValueToken token)
            : base($"Unknown value `{token.Value}`", token.CharAt)
        {
        }
    }
}
