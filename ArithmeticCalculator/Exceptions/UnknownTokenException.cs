namespace ArithmeticCalculator.Exceptions
{
    public class UnknownTokenException : ParseException
    {
        public UnknownTokenException(char token, int column)
            : base($"Unknown token: {token}", column)
        {
        }
    }
}
