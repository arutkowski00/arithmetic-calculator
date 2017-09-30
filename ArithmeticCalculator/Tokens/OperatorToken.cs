namespace ArithmeticCalculator.Tokens
{
    public abstract class OperatorToken<T> : ValueToken<T>
    {
        public override bool IsNumber => false;
        public override bool IsOperator => true;
        public abstract char Symbol { get; }

        protected OperatorToken(int charAt) : base(charAt)
        {
        }
    }
}
