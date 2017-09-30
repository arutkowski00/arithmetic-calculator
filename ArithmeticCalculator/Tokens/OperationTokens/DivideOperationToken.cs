using ArithmeticCalculator.Algorithms;

namespace ArithmeticCalculator.Tokens.OperationTokens
{
    public class DivideOperationToken : OperationToken
    {
        public override OperationAssociativity Associativity => OperationAssociativity.Left;
        public override int Precedence => 2;
        public override char Symbol => '/';
        public override OperationType Value => OperationType.Divide;

        public override double Calculate(double x, double y)
        {
            return x / y;
        }

        public DivideOperationToken(int charAt)
            : base(charAt)
        {
        }
    }
}
