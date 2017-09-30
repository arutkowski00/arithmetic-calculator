namespace ArithmeticCalculator.Tokens
{
    public class NumberToken : ValueToken<double>
    {
        public override double Value { get; }

        public NumberToken(double value, int charAt) : base(charAt)
        {
            Value = value;
        }
    }
}
