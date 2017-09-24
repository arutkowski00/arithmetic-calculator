using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Extensions;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator
{
    public interface IEquationParser
    {
        IEnumerable<IToken> Parse(string equation);
    }

    public class EquationParser : IEquationParser
    {
        private static readonly char[] OpeningBrackets = {'[', '('};
        private static readonly char[] ClosingBrackets = {']', ')'};
        private static readonly char[] DecimalSeparators = {'.', ','};

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
                {SymbolType.Digit, (c) => char.IsDigit(c) || DecimalSeparators.Contains(c)},
            };

        public IEnumerable<IToken> Parse(string equation)
        {
            equation = equation.RemoveWhitespace();

            var tokens = new List<IToken>();

            var symbolType = DetectSymbolType(equation[0]);
            var tokenBuilder = new StringBuilder(equation.Length / 2);

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
                        break;
                    case SymbolType.Digit:
                        if (nextSymbolType != symbolType)
                        {
                            var tokenString = tokenBuilder.ToString();

                            decimal number;
                            if (!decimal.TryParse(tokenString, NumberStyles.Any, CultureInfo.InvariantCulture,
                                out number))
                            {
                                throw new ParseException($"Invalid number format: {tokenString}",
                                    i + 1 - tokenBuilder.Length);
                            }
                            tokens.Add(new NumberToken(number));
                        }
                        break;
                    case SymbolType.Operator:
                        tokens.Add(new OperationToken(OperationTypeMap[symbol]));
                        break;
                    case SymbolType.Unknown:
                        throw new UnknownTokenException(symbol, i);
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (nextSymbolType != symbolType)
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