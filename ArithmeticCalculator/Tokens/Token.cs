namespace ArithmeticCalculator.Tokens
{
    public abstract class Token : IToken
    {
        public int CharAt { get; }
        
        protected Token(int charAt)
        {
            CharAt = charAt;
        }

        protected bool Equals(Token other)
        {
            return CharAt == other.CharAt;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Token) obj);
        }

        public override int GetHashCode()
        {
            return CharAt;
        }
    }
}
