using System;
using System.Collections.Generic;
using System.Linq;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public class PostfixCalculator : IPostfixCalculator
    {
        private delegate double CalculateFunctionCallback(double x);

        private readonly Dictionary<string, CalculateFunctionCallback> _stringFuncs =
            new Dictionary<string, CalculateFunctionCallback>
            {
                {"abs", Math.Abs},
                {"ceiling", Math.Ceiling},
                {"cos", Math.Cos},
                {"floor", Math.Floor},
                {"ln", x => Math.Log(x, Math.E)},
                {"round", Math.Round},
                {"sign", x => Math.Sign(x)},
                {"sin", Math.Sin},
                {"sqrt", Math.Sqrt}
            };

        private readonly Dictionary<string, double> _stringConsts =
            new Dictionary<string, double>
            {
                {"E", Math.E},
                {"PI", Math.PI}
            };

        public double Calculate(IEnumerable<IToken> reversePolishNotationTokens)
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

                    if (numbersStack.Count < 2)
                    {
                        throw new ParseException($"Invalid operator usage", token.CharAt);
                    }

                    // Order is important!
                    var b = numbersStack.Pop();
                    var a = numbersStack.Pop();

                    var result = operationToken.Calculate(a.Value, b.Value);
                    numbersStack.Push(new NumberToken(result, b.CharAt));
                }
                else if (token is StringToken)
                {
                    var stringToken = (StringToken) token;
                    var newCharAt = token.CharAt;
                    double result;

                    if (_stringFuncs.ContainsKey(stringToken.Value))
                    {
                        var calculateFunction = _stringFuncs[stringToken.Value];
                        var numberToken = numbersStack.Pop();
                        result = calculateFunction(numberToken.Value);
                        newCharAt = numberToken.CharAt;
                    }
                    else if (_stringConsts.ContainsKey(stringToken.Value))
                    {
                        result = _stringConsts[stringToken.Value];
                    }
                    else
                    {
                        throw new UnknownTokenValueException(stringToken);
                    }

                    numbersStack.Push(new NumberToken(result, newCharAt));
                }
                else
                {
                    throw new UnsupportedTokenException(nameof(PostfixCalculator), token);
                }
            }

            if (numbersStack.Count == 1)
                return numbersStack.Pop().Value;

            var exceptions = numbersStack
                .OrderBy(t => t.CharAt)
                .Select(t => new ParseException("Unexpected number", t.CharAt));

            throw new AggregateException(exceptions);
        }
    }
}