namespace ArithmeticCalculator.Tokens
{
    public abstract class OperatorToken : Token
    {
        public abstract char Symbol { get; }

        protected OperatorToken(int charAt) : base(charAt)
        {
        }
    }
}
