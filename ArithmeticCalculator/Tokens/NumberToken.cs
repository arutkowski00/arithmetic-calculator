namespace ArithmeticCalculator.Tokens
{
    public class NumberToken : ValueToken<decimal>
    {
        public override bool IsNumber => true;
        public override bool IsOperator => false;

        public NumberToken(decimal value) : base(value)
        {
        }
    }
}