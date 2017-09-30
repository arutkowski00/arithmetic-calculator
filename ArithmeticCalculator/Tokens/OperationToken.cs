using ArithmeticCalculator.Algorithms;

namespace ArithmeticCalculator.Tokens
{
    public abstract class OperationToken : OperatorToken<OperationType>
    {
        public abstract OperationAssociativity Associativity { get; }
        public abstract int Precedence { get; }

        protected OperationToken(int charAt) : base(charAt)
        {
        }

        public abstract double Calculate(double x, double y);
    }
}
