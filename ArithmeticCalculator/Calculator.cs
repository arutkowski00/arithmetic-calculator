using ArithmeticCalculator.Algorithms;

namespace ArithmeticCalculator
{
    public class Calculator
    {
        private readonly IReversePolishNotationBuilder _reversePolishNotationBuilder;
        private readonly IReversePolishNotationCalculator _reversePolishNotationCalculator;
        private readonly IEquationParser _equationParser;

        public Calculator(IReversePolishNotationBuilder reversePolishNotationBuilder,
            IReversePolishNotationCalculator reversePolishNotationCalculator, IEquationParser equationParser)
        {
            _reversePolishNotationBuilder = reversePolishNotationBuilder;
            _reversePolishNotationCalculator = reversePolishNotationCalculator;
            _equationParser = equationParser;
        }

        public decimal Calculate(string equation)
        {
            var parsedEquation = _equationParser.Parse(equation);
            // TODO: analyse parsed equation
            var rpNotation = _reversePolishNotationBuilder.Build(parsedEquation);
            var result = _reversePolishNotationCalculator.Calculate(rpNotation);

            return result;
        }
    }
}