using System.Collections.Generic;

namespace ArithmeticCalculator.Tokens
{
    public abstract class ValueToken<T> : BaseToken, IValueToken<T>
    {
        object IValueToken.Value => Value;
        public T Value { get; }

        protected ValueToken(T value, int charAt)
            : base(charAt)
        {
            Value = value;
        }

        protected bool Equals(ValueToken<T> other)
        {
            return base.Equals(other) && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ValueToken<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
            }
        }
    }
}