namespace ArithmeticCalculator.Tokens
{
    public abstract class OperatorToken : BaseToken
    {
        public abstract char Symbol { get; }

        protected OperatorToken(int charAt) : base(charAt)
        {
        }
    }
}
