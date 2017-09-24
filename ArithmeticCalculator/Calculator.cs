using ArithmeticCalculator.Algorithms;

namespace ArithmeticCalculator
{
    public class Calculator
    {
        private readonly IPostfixBuilder _postfixBuilder;
        private readonly IPostfixCalculator _postfixCalculator;
        private readonly IEquationParser _equationParser;

        public Calculator(IPostfixBuilder postfixBuilder,
            IPostfixCalculator postfixCalculator, IEquationParser equationParser)
        {
            _postfixBuilder = postfixBuilder;
            _postfixCalculator = postfixCalculator;
            _equationParser = equationParser;
        }

        public decimal Calculate(string equation)
        {
            var parsedEquation = _equationParser.Parse(equation);
            // TODO: analyse parsed equation
            var rpNotation = _postfixBuilder.Build(parsedEquation);
            var result = _postfixCalculator.Calculate(rpNotation);

            return result;
        }
    }
}