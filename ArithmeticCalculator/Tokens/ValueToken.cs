using System.Collections.Generic;

namespace ArithmeticCalculator.Tokens
{
    public abstract class ValueToken<T> : IValueToken<T>
    {
        public abstract bool IsNumber { get; }
        public abstract bool IsOperator { get; }
        public T Value { get; }
        
        protected ValueToken(T value)
        {
            Value = value;
        }

        protected bool Equals(ValueToken<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
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
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }
}