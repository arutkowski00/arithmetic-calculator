using System.Collections.Generic;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public interface IReversePolishNotationCalculator
    {
        decimal Calculate(IEnumerable<IToken> reversePolishNotationTokens);
    }
}