using System;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Exceptions
{
    public class UnsupportedTokenTypeException : Exception
    {
        public UnsupportedTokenTypeException(string parserName, IToken token)
            : base($"Token type `{token.GetType().Name}` is not supported in {parserName}")
        {
        }
    }
}