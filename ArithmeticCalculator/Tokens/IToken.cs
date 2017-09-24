namespace ArithmeticCalculator.Tokens
{
    public interface IToken
    {
        bool IsNumber { get; }
        bool IsOperator { get; }
    }
}