using System.Collections.Generic;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public interface IEquationParser
    {
        IEnumerable<IToken> Parse(string equation);
    }
}