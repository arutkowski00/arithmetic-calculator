namespace ArithmeticCalculator.Tokens
{
    public interface IToken
    {
        int CharAt { get; }
        bool IsNumber { get; }
        bool IsOperator { get; }
    }
}