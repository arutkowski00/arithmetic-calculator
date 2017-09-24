using System;
using System.Collections.Generic;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator
{
    public interface IReversePolishNotationCalculator
    {
        decimal Calculate(IEnumerable<IToken> reversePolishNotationTokens);
    }

    public class ReversePolishNotationCalculator : IReversePolishNotationCalculator
    {
        public delegate decimal CalculateOperationCallback(decimal x, decimal y);

        private readonly Dictionary<OperationType, CalculateOperationCallback> _operationTypeFuncs =
            new Dictionary<OperationType, CalculateOperationCallback>
            {
                {OperationType.Add, (x, y) => x + y},
                {OperationType.Subtract, (x, y) => x - y},
                {OperationType.Multiply, (x, y) => x * y},
                {OperationType.Divide, (x, y) => x / y},
                {OperationType.Exponent, (x, y) => (decimal) Math.Pow((double) x, (double) y)},
            };

        public decimal Calculate(IEnumerable<IToken> reversePolishNotationTokens)
        {
            var numbersStack = new Stack<NumberToken>();

            foreach (var token in reversePolishNotationTokens)
            {
                var numberToken = token as NumberToken;
                if (numberToken != null)
                {
                    numbersStack.Push(numberToken);
                    continue;
                }

                var operationToken = token as OperationToken;
                if (operationToken != null)
                {
                    var calculateOperation = _operationTypeFuncs[operationToken.Value];

                    // Order is important!
                    var b = numbersStack.Pop();
                    var a = numbersStack.Pop();

                    var result = calculateOperation(a.Value, b.Value);
                    numbersStack.Push(new NumberToken(result));
                }
                else
                {
                    throw new UnsupportedTokenTypeException(nameof(ReversePolishNotationCalculator), token);
                }
            }

            if (numbersStack.Count != 1)
            {
                throw new ArgumentException($"Invalid reverse polish notation: there are still {numbersStack.Count} numbers to calculate, but no operations left.\nStack: {numbersStack}", nameof(reversePolishNotationTokens));
            }

            return numbersStack.Pop().Value;
        }
    }
}