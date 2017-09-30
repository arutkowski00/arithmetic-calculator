using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Tokens;
using ArithmeticCalculator.Tokens.OperationTokens;
using ArithmeticCalculator.Tokens.OperatorTokens;

namespace ArithmeticCalculator.Algorithms
{
    public class InfixEquationParser : IEquationParser
    {
        private const char NegativeSign = '-';
        private static readonly char[] OpeningBrackets = {'[', '('};
        private static readonly char[] ClosingBrackets = {']', ')'};
        private static readonly char[] DigitSymbols = {'.', ','};

        private delegate OperationToken OperationTokenFactoryDelegate(int charAt);

        private static readonly Dictionary<char, OperationTokenFactoryDelegate> OperationTokenMap
            = new Dictionary<char, OperationTokenFactoryDelegate>
            {
                {'+', charAt => new AddOperationToken(charAt)},
                {'-', charAt => new SubtractOperationToken(charAt)},
                {'*', charAt => new MultiplyOperationToken(charAt)},
                {'/', charAt => new DivideOperationToken(charAt)},
                {'%', charAt => new ModuloOperationToken(charAt)},
                {'^', charAt => new ExponentOperationToken(charAt)}
            };

        private readonly Dictionary<SymbolType, Func<char, bool>> _symbolMap =
            new Dictionary<SymbolType, Func<char, bool>>
            {
                {SymbolType.OpeningBracket, OpeningBrackets.Contains},
                {SymbolType.ClosingBracket, ClosingBrackets.Contains},
                {SymbolType.Operator, OperationTokenMap.Keys.Contains},
                {SymbolType.Letter, char.IsLetter},
                {SymbolType.Digit, c => char.IsDigit(c) || DigitSymbols.Contains(c)},
                {SymbolType.Whitespace, char.IsWhiteSpace}
            };

        public IEnumerable<IToken> Parse(string equation)
        {
            if (equation == null) throw new ArgumentNullException(nameof(equation));

            if (equation.Length == 0)
                return new IToken[] { };

            var tokens = new List<IToken>();

            var symbolType = DetectSymbolType(equation[0]);
            var tokenBuilder = new StringBuilder(equation.Length / 2);
            var buildToken = false;

            for (var i = 0; i < equation.Length; i++)
            {
                var symbol = equation[i];
                var symbolStartAt = i + 1 - tokenBuilder.Length;
                tokenBuilder.Append(symbol);

                var nextSymbolType = i < equation.Length - 1 ? DetectSymbolType(equation[i + 1]) : SymbolType.Unknown;

                switch (symbolType)
                {
                    case SymbolType.OpeningBracket:
                        tokens.Add(new GroupOpenOperatorToken(symbolStartAt));
                        break;
                    case SymbolType.ClosingBracket:
                        tokens.Add(new GroupCloseOperatorToken(symbolStartAt));
                        break;
                    case SymbolType.Letter:
                        buildToken = true;
                        if (nextSymbolType != SymbolType.Letter)
                        {
                            var tokenString = tokenBuilder.ToString();
                            tokens.Add(new StringToken(tokenString, symbolStartAt));
                            buildToken = false;
                        }
                        break;
                    case SymbolType.Digit:
                        buildToken = true;
                        if (nextSymbolType != SymbolType.Digit)
                        {
                            var tokenString = tokenBuilder.ToString();

                            double number;
                            if (!double.TryParse(tokenString,
                                NumberStyles.Number,
                                CultureInfo.InvariantCulture,
                                out number))
                            {
                                throw new ParseException($"Invalid number format: {tokenString}",
                                    symbolStartAt);
                            }
                            tokens.Add(new NumberToken(number, symbolStartAt));
                            buildToken = false;
                        }
                        break;
                    case SymbolType.Operator:
                        if ((tokens.Count == 0 || tokens.Last() is OperatorToken) &&
                            symbol == NegativeSign)
                        {
                            switch (nextSymbolType)
                            {
                                case SymbolType.Digit:
                                    // treat '-' between operation and digit as number sign
                                    buildToken = true;
                                    break;
                                case SymbolType.OpeningBracket:
                                case SymbolType.Letter:
                                    // treat '-' between operation and opening bracket or letter as (-1 * x)
                                    tokens.Add(new NumberToken(-1, symbolStartAt));
                                    tokens.Add(new MultiplyOperationToken(symbolStartAt));
                                    break;
                                default:
                                    throw new ParseException($"Invalid use of {NegativeSign}", symbolStartAt);
                            }
                        }
                        else
                        {
                            tokens.Add(OperationTokenMap[symbol](symbolStartAt));
                        }
                        break;
                    case SymbolType.Whitespace:
                        break;
                    case SymbolType.Unknown:
                        throw new UnknownSymbolException(symbol, symbolStartAt);
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (!buildToken && tokenBuilder.Length > 0)
                {
                    tokenBuilder.Clear();
                }

                symbolType = nextSymbolType;
            }

            return tokens;
        }

        private SymbolType DetectSymbolType(char c)
        {
            return (from tokenMapKeyValue in _symbolMap
                    where tokenMapKeyValue.Value(c)
                    select tokenMapKeyValue.Key)
                .DefaultIfEmpty(SymbolType.Unknown)
                .FirstOrDefault();
        }

        public enum SymbolType
        {
            Unknown,
            OpeningBracket,
            ClosingBracket,
            Letter,
            Digit,
            Operator,
            Whitespace
        }
    }
}
