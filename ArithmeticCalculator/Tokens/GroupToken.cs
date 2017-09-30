using System;

namespace ArithmeticCalculator.Tokens
{
    public class GroupToken : OperatorToken<GroupTokenType>
    {
        public override char Symbol => GetGroupTokenTypeSymbol(Value);
        public override GroupTokenType Value { get; }

        public GroupToken(GroupTokenType value, int charAt) : base(charAt)
        {
            Value = value;
        }

        private static char GetGroupTokenTypeSymbol(GroupTokenType groupTokenType)
        {
            switch (groupTokenType)
            {
                case GroupTokenType.Opening:
                    return '(';
                case GroupTokenType.Closing:
                    return ')';
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
