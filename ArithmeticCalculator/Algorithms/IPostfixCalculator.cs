using System.Collections.Generic;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public interface IPostfixCalculator
    {
        double Calculate(IEnumerable<IToken> reversePolishNotationTokens);
    }
}
