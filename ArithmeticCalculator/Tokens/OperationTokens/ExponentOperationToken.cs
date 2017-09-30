using System;
using ArithmeticCalculator.Algorithms;

namespace ArithmeticCalculator.Tokens.OperationTokens
{
    public class ExponentOperationToken : OperationToken
    {
        public override OperationAssociativity Associativity => OperationAssociativity.Right;
        public override int Precedence => 3;
        public override char Symbol => '^';
        public override OperationType Value => OperationType.Exponent;

        public override double Calculate(double x, double y)
        {
            return Math.Pow(x, y);
        }

        public ExponentOperationToken(int charAt)
            : base(charAt)
        {
        }
    }
}
