using System;

namespace ArithmeticCalculator.Tokens
{
    public class GroupToken : OperatorToken
    {
        public override char Symbol => GetGroupTokenTypeSymbol(GroupTokenType);
        public GroupTokenType GroupTokenType { get; }

        public GroupToken(GroupTokenType groupTokenType, int charAt) : base(charAt)
        {
            GroupTokenType = groupTokenType;
        }

        protected bool Equals(GroupToken other)
        {
            return base.Equals(other) && GroupTokenType == other.GroupTokenType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GroupToken) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (int) GroupTokenType;
            }
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
