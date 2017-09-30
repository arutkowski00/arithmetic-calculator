using ArithmeticCalculator.Algorithms;

namespace ArithmeticCalculator.Tokens.OperationTokens
{
    public class SubtractOperationToken : OperationToken
    {
        public override OperationAssociativity Associativity => OperationAssociativity.Left;
        public override int Precedence => 1;
        public override char Symbol => '-';
        public override OperationType Value => OperationType.Subtract;

        public override double Calculate(double x, double y)
        {
            return x - y;
        }

        public SubtractOperationToken(int charAt)
            : base(charAt)
        {
        }
    }
}
