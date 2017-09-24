namespace ArithmeticCalculator.Tokens
{
    public class OperationToken : OperatorToken<OperationType>
    {
        public OperationToken(OperationType value) : base(value)
        {
        }
    }
}