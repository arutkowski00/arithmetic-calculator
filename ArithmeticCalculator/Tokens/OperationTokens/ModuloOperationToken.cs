using ArithmeticCalculator.Algorithms;

namespace ArithmeticCalculator.Tokens.OperationTokens
{
    public class ModuloOperationToken : OperationToken
    {
        public override OperationAssociativity Associativity => OperationAssociativity.Left;
        public override int Precedence => 2;
        public override char Symbol => '%';

        public override double Calculate(double x, double y)
        {
            return x % y;
        }

        public ModuloOperationToken(int charAt)
            : base(charAt)
        {
        }
    }
}
