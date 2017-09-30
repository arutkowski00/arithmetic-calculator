namespace ArithmeticCalculator.Tokens.OperatorTokens
{
    public class GroupOpenOperatorToken : OperatorToken
    {
        public override char Symbol => '(';

        public GroupOpenOperatorToken(int charAt) : base(charAt)
        {
        }
    }
}
