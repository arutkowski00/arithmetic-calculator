namespace ArithmeticCalculator.Tokens
{
    public interface IValueToken : IToken
    {
        object Value { get; }
    }
    
    public interface IValueToken<out T> : IValueToken
    {
        new T Value { get; }
    }
}