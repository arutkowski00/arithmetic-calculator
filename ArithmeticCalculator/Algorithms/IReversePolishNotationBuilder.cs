using System.Collections.Generic;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public interface IReversePolishNotationBuilder
    {
        IEnumerable<IToken> Build(IEnumerable<IToken> infixNotationTokens);
    }
}