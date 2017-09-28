﻿namespace ArithmeticCalculator.Exceptions
{
    public class UnknownSymbolException : ParseException
    {
        public UnknownSymbolException(char token, int column)
            : base($"Unknown symbol: {token}", column)
        {
        }
    }
}
