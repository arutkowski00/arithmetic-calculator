using System.Collections.Generic;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public class ReversePolishNotationBuilder : IReversePolishNotationBuilder
    {
        private readonly Dictionary<OperationType, int> _operationPrecedences = new Dictionary<OperationType, int>
        {
            {OperationType.Add, 1},
            {OperationType.Subtract, 1},
            {OperationType.Multiply, 2},
            {OperationType.Divide, 2},
            {OperationType.Exponent, 3},
        };

        public IEnumerable<IToken> Build(IEnumerable<IToken> infixNotationTokens)
        {
            var outputQueue = new Queue<IToken>();
            var operatorStack = new Stack<IToken>();

            foreach (var infixToken in infixNotationTokens)
            {
                if (infixToken is NumberToken)
                {
                    outputQueue.Enqueue(infixToken);
                }
                else if (infixToken is OperationToken)
                {
                    var operationToken = (OperationToken) infixToken;

                    while (operatorStack.Count > 0 &&
                           !ReferenceEquals(operationToken, GetOperatorWithGreaterPrecedence(operationToken, operatorStack.Peek())))
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }

                    operatorStack.Push(operationToken);
                }
                else if (infixToken is GroupToken)
                {
                    var groupToken = (GroupToken) infixToken;
                    if (groupToken.Value == GroupTokenType.Opening)
                    {
                        operatorStack.Push(groupToken);
                    }
                    else
                    {
                        while (operatorStack.Count > 0)
                        {
                            var tokenFromStack = operatorStack.Pop() as GroupToken;

                            if (tokenFromStack != null &&
                                tokenFromStack.Value == GroupTokenType.Opening)
                                break;

                            outputQueue.Enqueue(tokenFromStack);
                        }
                    }
                }
            }

            while (operatorStack.Count > 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            return outputQueue;
        }

        private IToken GetOperatorWithGreaterPrecedence(IToken a, IToken b)
        {
            if (!(b is OperationToken)) return a;
            if (!(a is OperationToken)) return b;

            var aOperation = ((OperationToken) a).Value;
            var bOperation = ((OperationToken) b).Value;

            return _operationPrecedences[aOperation] > _operationPrecedences[bOperation]
                ? a
                : b;
        }
    }
}