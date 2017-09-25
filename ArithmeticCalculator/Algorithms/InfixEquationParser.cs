using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Extensions;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public class InfixEquationParser : IEquationParser
    {
        private const char NegativeSign = '-';
        private static readonly char[] OpeningBrackets = {'[', '('};
        private static readonly char[] ClosingBrackets = {']', ')'};
        private static readonly char[] DigitSymbols = {'.', ','};

        private static readonly Dictionary<char, OperationType> OperationTypeMap = new Dictionary<char, OperationType>
        {
            {'+', OperationType.Add},
            {'-', OperationType.Subtract},
            {'*', OperationType.Multiply},
            {'/', OperationType.Divide},
            {'^', OperationType.Exponent},
        };

        private readonly Dictionary<SymbolType, Func<char, bool>> _symbolMap =
            new Dictionary<SymbolType, Func<char, bool>>
            {
                {SymbolType.OpeningBracket, OpeningBrackets.Contains},
                {SymbolType.ClosingBracket, ClosingBrackets.Contains},
                {SymbolType.Operator, OperationTypeMap.Keys.Contains},
                {SymbolType.Letter, char.IsLetter},
                {SymbolType.Digit, (c) => char.IsDigit(c) || DigitSymbols.Contains(c)},
            };

        public IEnumerable<IToken> Parse(string equation)
        {
            equation = equation?.RemoveWhitespace() ?? throw new ArgumentNullException(nameof(equation));

            if (equation.Length == 0)
                return new IToken[] { };

            var tokens = new List<IToken>();

            var symbolType = DetectSymbolType(equation[0]);
            var tokenBuilder = new StringBuilder(equation.Length / 2);
            var buildToken = false;

            for (var i = 0; i < equation.Length; i++)
            {
                var symbol = equation[i];
                tokenBuilder.Append(symbol);

                var nextSymbolType = i < equation.Length - 1 ? DetectSymbolType(equation[i + 1]) : SymbolType.Unknown;

                switch (symbolType)
                {
                    case SymbolType.OpeningBracket:
                        tokens.Add(new GroupToken(GroupTokenType.Opening));
                        break;
                    case SymbolType.ClosingBracket:
                        tokens.Add(new GroupToken(GroupTokenType.Closing));
                        break;
                    case SymbolType.Letter:
                        buildToken = true;
                        if (nextSymbolType != SymbolType.Letter)
                        {
                            var tokenString = tokenBuilder.ToString();
                            tokens.Add(new StringToken(tokenString));
                            buildToken = false;
                        }
                        break;
                    case SymbolType.Digit:
                        buildToken = true;
                        if (nextSymbolType != SymbolType.Digit)
                        {
                            var tokenString = tokenBuilder.ToString();

                            decimal number;
                            if (!decimal.TryParse(tokenString,
                                NumberStyles.Number,
                                CultureInfo.CurrentCulture,
                                out number))
                            {
                                throw new ParseException($"Invalid number format: {tokenString}",
                                    i + 1 - tokenBuilder.Length);
                            }
                            tokens.Add(new NumberToken(number));
                            buildToken = false;
                        }
                        break;
                    case SymbolType.Operator:
                        if ((tokens.Count == 0 || tokens.Last().IsOperator) &&
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
                                    tokens.Add(new NumberToken(-1));
                                    tokens.Add(new OperationToken(OperationType.Multiply));
                                    break;
                                default:
                                    throw new ParseException($"Invalid use of {NegativeSign}", i);
                            }
                        }
                        else
                        {
                            tokens.Add(new OperationToken(OperationTypeMap[symbol]));
                        }
                        break;
                    case SymbolType.Unknown:
                        throw new UnknownSymbolException(symbol, i);
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
    }
}