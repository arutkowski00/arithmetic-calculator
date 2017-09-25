namespace ArithmeticCalculator.Tokens
{
    public class OperationToken : OperatorToken<OperationType>
    {
        public OperationToken(OperationType value, int charAt) : base(value, charAt)
        {
        }
    }
}