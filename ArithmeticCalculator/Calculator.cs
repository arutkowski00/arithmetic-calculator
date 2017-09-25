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

        public double Calculate(string equation)
        {
            var infixTokens = _equationParser.Parse(equation);
            var postfixTokens = _postfixBuilder.Build(infixTokens);
            var result = _postfixCalculator.Calculate(postfixTokens);

            return result;
        }
    }
}