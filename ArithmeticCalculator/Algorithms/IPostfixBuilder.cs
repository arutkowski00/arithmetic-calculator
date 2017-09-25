using System.Collections.Generic;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public interface IPostfixBuilder
    {
        IEnumerable<IToken> Build(IEnumerable<IToken> infixNotationTokens);
    }
}
