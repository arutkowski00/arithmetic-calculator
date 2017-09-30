namespace ArithmeticCalculator.Tokens.OperatorTokens
{
    public class GroupCloseOperatorToken : OperatorToken
    {
        public override char Symbol => ')';

        public GroupCloseOperatorToken(int charAt) : base(charAt)
        {
        }
    }
}
