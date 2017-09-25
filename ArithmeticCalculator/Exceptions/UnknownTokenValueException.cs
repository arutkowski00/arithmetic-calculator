using System;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Exceptions
{
    public class UnknownTokenValueException : Exception
    {
        public UnknownTokenValueException(IValueToken token)
            : base($"Unknown {token.GetType().Name} value `{token.Value}`")
        {
        }
    }
}