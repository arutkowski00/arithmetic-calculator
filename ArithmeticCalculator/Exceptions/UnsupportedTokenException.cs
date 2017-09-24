using System;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Exceptions
{
    public class UnsupportedTokenException : Exception
    {
        public UnsupportedTokenException(string parserName, IToken token)
            : base($"Token type `{token.GetType().Name}` is not supported in {parserName}")
        {
        }
    }
}