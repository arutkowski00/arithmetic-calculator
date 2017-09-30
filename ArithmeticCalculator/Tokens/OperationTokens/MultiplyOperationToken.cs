using ArithmeticCalculator.Algorithms;

namespace ArithmeticCalculator.Tokens.OperationTokens
{
    public class MultiplyOperationToken : OperationToken
    {
        public override OperationAssociativity Associativity => OperationAssociativity.Left;
        public override int Precedence => 2;
        public override char Symbol => '*';
        public override OperationType Value => OperationType.Multiply;

        public override double Calculate(double x, double y)
        {
            return x * y;
        }

        public MultiplyOperationToken(int charAt)
            : base(charAt)
        {
        }
    }
}
