namespace ArithmeticCalculator.Exceptions
{
    public class UnknownSymbolException : ParseException
    {
        public UnknownSymbolException(char symbol, int column)
            : base($"Unknown symbol: {symbol}", column)
        {
        }
    }
}
