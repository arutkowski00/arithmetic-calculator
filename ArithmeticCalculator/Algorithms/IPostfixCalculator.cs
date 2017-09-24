using System.Collections.Generic;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public interface IPostfixCalculator
    {
        decimal Calculate(IEnumerable<IToken> reversePolishNotationTokens);
    }
}