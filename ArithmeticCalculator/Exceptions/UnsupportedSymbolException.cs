using System;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Exceptions
{
    public class UnsupportedSymbolException : Exception
    {
        public UnsupportedSymbolException(string parserName, SymbolType symbolType)
            : base($"Token type `{symbolType}` is not supported in {parserName}")
        {
        }
    }
}