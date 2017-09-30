namespace ArithmeticCalculator.Tokens
{
    public abstract class BaseToken : IToken
    {
        public int CharAt { get; }
        
        protected BaseToken(int charAt)
        {
            CharAt = charAt;
        }

        protected bool Equals(BaseToken other)
        {
            return CharAt == other.CharAt;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseToken) obj);
        }

        public override int GetHashCode()
        {
            return CharAt;
        }
    }
}
