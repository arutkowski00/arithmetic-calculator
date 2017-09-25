namespace ArithmeticCalculator.Tokens
{
    public class GroupToken : OperatorToken<GroupTokenType>
    {
        public GroupToken(GroupTokenType value, int charAt) : base(value, charAt)
        {
        }
    }
}
