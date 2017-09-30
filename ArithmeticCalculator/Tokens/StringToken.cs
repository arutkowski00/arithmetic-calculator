namespace ArithmeticCalculator.Tokens
{
    public class StringToken : ValueToken<string>
    {
        public override bool IsNumber => false;
        public override bool IsOperator => false;
        public override string Value { get; }

        public StringToken(string value, int charAt) : base(charAt)
        {
            Value = value;
        }
    }
}
