namespace ArithmeticCalculator.Tokens
{
    public class OperationToken : OperatorToken<OperationType>
    {
        public OperationToken(OperationType value) : base(value)
        {
        }
    }

    public enum OperationType
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Exponent
    }
}