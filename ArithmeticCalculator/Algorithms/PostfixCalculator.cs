using System;
using System.Collections.Generic;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public class PostfixCalculator : IPostfixCalculator
    {
        public delegate decimal CalculateOperationCallback(decimal x, decimal y);

        public delegate decimal CalculateFunctionCallback(decimal x);

        private readonly Dictionary<OperationType, CalculateOperationCallback> _operationTypeFuncs =
            new Dictionary<OperationType, CalculateOperationCallback>
            {
                {OperationType.Add, (x, y) => x + y},
                {OperationType.Subtract, (x, y) => x - y},
                {OperationType.Multiply, (x, y) => x * y},
                {OperationType.Divide, (x, y) => x / y},
                {OperationType.Exponent, (x, y) => (decimal) Math.Pow((double) x, (double) y)},
            };

        private readonly Dictionary<string, CalculateFunctionCallback> _stringFuncs =
            new Dictionary<string, CalculateFunctionCallback>
            {
                {"abs", Math.Abs},
                {"ceiling", Math.Ceiling},
                {"cos", (x) => (decimal) Math.Cos((double) x)},
                {"floor", Math.Floor},
                {"ln", (x) => (decimal) Math.Log((double) x, Math.E)},
                {"round", Math.Round},
                {"sign", (x) => (decimal) Math.Sign((double) x)},
                {"sin", (x) => (decimal) Math.Sin((double) x)},
                {"sqrt", (x) => (decimal) Math.Sqrt((double) x)}
            };

        private readonly Dictionary<string, decimal> _stringConsts =
            new Dictionary<string, decimal>
            {
                {"E", (decimal) Math.E},
                {"PI", (decimal) Math.PI},
            };

        public decimal Calculate(IEnumerable<IToken> reversePolishNotationTokens)
        {
            var numbersStack = new Stack<NumberToken>();

            foreach (var token in reversePolishNotationTokens)
            {
                // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
                if (token is NumberToken)
                {
                    numbersStack.Push((NumberToken) token);
                }
                else if (token is OperationToken)
                {
                    var operationToken = (OperationToken) token;
                    var calculateOperation = _operationTypeFuncs[operationToken.Value];

                    // Order is important!
                    var b = numbersStack.Pop();
                    var a = numbersStack.Pop();

                    var result = calculateOperation(a.Value, b.Value);
                    numbersStack.Push(new NumberToken(result));
                }
                else if (token is StringToken)
                {
                    var stringToken = (StringToken) token;
                    decimal result;

                    if (_stringFuncs.ContainsKey(stringToken.Value))
                    {
                        var calculateFunction = _stringFuncs[stringToken.Value];
                        var numberToken = numbersStack.Pop();
                        result = calculateFunction(numberToken.Value);
                    }
                    else if (_stringConsts.ContainsKey(stringToken.Value))
                    {
                        result = _stringConsts[stringToken.Value];
                    }
                    else
                    {
                        throw new UnknownTokenValueException(stringToken);
                    }

                    numbersStack.Push(new NumberToken(result));
                }
                else
                {
                    throw new UnsupportedTokenException(nameof(PostfixCalculator), token);
                }
            }

            if (numbersStack.Count != 1)
            {
                throw new ArgumentException(
                    $"Invalid reverse polish notation: there are still {numbersStack.Count} numbers to calculate, but no operations left.\nStack: {numbersStack}",
                    nameof(reversePolishNotationTokens));
            }

            return numbersStack.Pop().Value;
        }
    }
}