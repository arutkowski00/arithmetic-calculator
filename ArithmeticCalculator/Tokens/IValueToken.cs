namespace ArithmeticCalculator.Tokens
{
    public interface IValueToken<out T> : IToken
    {
        T Value { get; }
    }
}