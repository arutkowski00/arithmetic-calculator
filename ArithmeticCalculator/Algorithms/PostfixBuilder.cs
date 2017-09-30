using System.Collections.Generic;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Algorithms
{
    public class PostfixBuilder : IPostfixBuilder
    {
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
                else if (infixToken is StringToken)
                {
                    operatorStack.Push(infixToken);
                }
                else if (infixToken is OperationToken)
                {
                    var operationToken = (OperationToken) infixToken;

                    while (operatorStack.Count > 0 &&
                           !CanPushOperationTokenToStack(operationToken, operatorStack))
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
                        var openingFound = false;
                        while (operatorStack.Count > 0)
                        {
                            var tokenFromStack = operatorStack.Pop();
                            var groupTokenFromStack = tokenFromStack as GroupToken;

                            if (groupTokenFromStack != null &&
                                groupTokenFromStack.Value == GroupTokenType.Opening)
                            {
                                openingFound = true;
                                break;
                            }

                            outputQueue.Enqueue(tokenFromStack);
                        }

                        if (!openingFound)
                        {
                            throw new ParseException("Missing opening bracket", groupToken.CharAt); 
                        }
                    }
                }
                else
                {
                    throw new UnsupportedTokenException(nameof(PostfixBuilder), infixToken);
                }
            }

            while (operatorStack.Count > 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            return outputQueue;
        }

        private bool CanPushOperationTokenToStack(OperationToken operationToken, Stack<IToken> operatorStack)
        {
            var precedence = GetOperationPrecedence(operationToken, operatorStack.Peek());
            var canPush = precedence > 0 ||
                          precedence == 0 && operationToken.Associativity == OperationAssociativity.Right;
            return canPush;
        }

        /// <summary>
        /// Returns:
        /// <list type="bullet">
        /// <item>
        /// <term>positive number</term>
        /// <description>if <paramref name="operation"/> has greater precedence than <paramref name="compareTo"/></description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>if <paramref name="operation"/> has the same precedence as <paramref name="compareTo"/></description>
        /// </item>
        /// <item>
        /// <term>negative number</term>
        /// <description>if <paramref name="operation"/> has smaller precedence than <paramref name="compareTo"/></description>
        /// </item>
        /// </list>
        /// </summary>
        /// <returns></returns>
        private int GetOperationPrecedence(OperationToken operation, IToken compareTo)
        {
            if (compareTo is StringToken) return -1;
            
            var compareToOperation = compareTo as OperationToken;
            if (compareToOperation == null) return 1;

            return operation.Precedence - compareToOperation.Precedence;
        }
    }

    public enum OperationAssociativity
    {
        Left,
        Right
    }
}
