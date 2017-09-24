using System.Collections;
using System.Linq;
using ArithmeticCalculator.Tokens;
using NUnit.Framework;

namespace ArithmeticCalculator.Test.Data
{
    public static class EquationTestData
    {
        public static readonly EquationTestDataItem Equation1 = new EquationTestDataItem
        {
            Equation = "2 + (4 / 8) * 3",
            InfixTokens = new IToken[]
            {
                new NumberToken(2),
                new OperationToken(OperationType.Add),
                new GroupToken(GroupTokenType.Opening),
                new NumberToken(4),
                new OperationToken(OperationType.Divide),
                new NumberToken(8),
                new GroupToken(GroupTokenType.Closing),
                new OperationToken(OperationType.Multiply),
                new NumberToken(3),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(2),
                new NumberToken(4),
                new NumberToken(8),
                new OperationToken(OperationType.Divide),
                new NumberToken(3),
                new OperationToken(OperationType.Multiply),
                new OperationToken(OperationType.Add),
            },
            Result = 3.5m
        };

        public static readonly EquationTestDataItem Equation2 = new EquationTestDataItem
        {
            Equation = "-2 + -3 * -(-4 / 8)",
            InfixTokens = new IToken[]
            {
                new NumberToken(-2),
                new OperationToken(OperationType.Add),
                new NumberToken(-3),
                new OperationToken(OperationType.Multiply),
                new NumberToken(-1),
                new OperationToken(OperationType.Multiply),
                new GroupToken(GroupTokenType.Opening),
                new NumberToken(-4),
                new OperationToken(OperationType.Divide),
                new NumberToken(8),
                new GroupToken(GroupTokenType.Closing),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(-2),
                new NumberToken(-3),
                new NumberToken(-1),
                new OperationToken(OperationType.Multiply),
                new NumberToken(-4),
                new NumberToken(8),
                new OperationToken(OperationType.Divide),
                new OperationToken(OperationType.Multiply),
                new OperationToken(OperationType.Add),
            },
            Result = -3.5m
        };
        
        public static readonly EquationTestDataItem Equation2 = new EquationTestDataItem
        {
            Equation = "",
            InfixTokens = new IToken[]
            {
                new NumberToken(-2),
                new OperationToken(OperationType.Add),
                new NumberToken(-3),
                new OperationToken(OperationType.Multiply),
                new NumberToken(-1),
                new OperationToken(OperationType.Multiply),
                new GroupToken(GroupTokenType.Opening),
                new NumberToken(-4),
                new OperationToken(OperationType.Divide),
                new NumberToken(8),
                new GroupToken(GroupTokenType.Closing),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(-2),
                new NumberToken(-3),
                new NumberToken(-1),
                new OperationToken(OperationType.Multiply),
                new NumberToken(-4),
                new NumberToken(8),
                new OperationToken(OperationType.Divide),
                new OperationToken(OperationType.Multiply),
                new OperationToken(OperationType.Add),
            },
            Result = -3.5m
        };

        public static readonly EquationTestDataItem[] EquationTestDataItems =
        {
            Equation1,
            Equation2
        };
        
        public static IEnumerable EquationToInfixTestCases
        {
            get
            {
                return EquationTestDataItems.Select(x =>
                    new TestCaseData(x.Equation, x.InfixTokens));
            }
        }
        
        public static IEnumerable InfixToPostfixTestCases
        {
            get
            {
                return EquationTestDataItems.Select(x =>
                    new TestCaseData(x.InfixTokens, x.PostfixTokens));
            }
        }

        public static IEnumerable PostfixToResultTestCases
        {
            get
            {
                return EquationTestDataItems.Select(x =>
                    new TestCaseData(x.PostfixTokens, x.Result));

            }
        }
    }
}