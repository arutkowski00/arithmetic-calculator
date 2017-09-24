namespace ArithmeticCalculator.Tokens
{
    public class GroupToken : OperatorToken<GroupTokenType>
    {
        public GroupToken(GroupTokenType value) : base(value)
        {
        }
    }

    public enum GroupTokenType
    {
        Opening,
        Closing
    }
}