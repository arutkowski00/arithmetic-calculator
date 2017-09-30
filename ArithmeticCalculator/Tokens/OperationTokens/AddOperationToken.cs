using ArithmeticCalculator.Algorithms;

namespace ArithmeticCalculator.Tokens.OperationTokens
{
    public class AddOperationToken : OperationToken
    {
        public override OperationAssociativity Associativity => OperationAssociativity.Left;
        public override int Precedence => 1;
        public override char Symbol => '+';

        public override double Calculate(double x, double y)
        {
            return x + y;
        }

        public AddOperationToken(int charAt)
            : base(charAt)
        {
        }
    }
}
