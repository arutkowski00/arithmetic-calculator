namespace ArithmeticCalculator.Tokens
{
    public class StringToken : ValueToken<string>
    {
        public override bool IsNumber => false;
        public override bool IsOperator => false;

        public StringToken(string value) : base(value)
        {
        }
    }
}