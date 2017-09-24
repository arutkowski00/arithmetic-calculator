namespace ArithmeticCalculator.Tokens
{
    public abstract class OperatorToken<T> : ValueToken<T>
    {
        public override bool IsNumber => false;
        public override bool IsOperator => true;

        protected OperatorToken(T value) : base(value)
        {
        }
    }
}