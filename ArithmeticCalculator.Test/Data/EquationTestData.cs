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

        public static readonly EquationTestDataItem Equation3 = new EquationTestDataItem
        {
            Equation = "4.25 / (-10 * [-5.5 + 25.5])",
            InfixTokens = new IToken[]
            {
                new NumberToken(4.25m),
                new OperationToken(OperationType.Divide),
                new GroupToken(GroupTokenType.Opening),
                new NumberToken(-10),
                new OperationToken(OperationType.Multiply),
                new GroupToken(GroupTokenType.Opening),
                new NumberToken(-5.5m),
                new OperationToken(OperationType.Add),
                new NumberToken(25.5m),
                new GroupToken(GroupTokenType.Closing),
                new GroupToken(GroupTokenType.Closing),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(4.25m),
                new NumberToken(-10),
                new NumberToken(-5.5m),
                new NumberToken(25.5m),
                new OperationToken(OperationType.Add),
                new OperationToken(OperationType.Multiply),
                new OperationToken(OperationType.Divide),
            },
            Result = -0.02125m
        };

        public static readonly EquationTestDataItem Equation4 = new EquationTestDataItem
        {
            Equation = "-2 ^ 3 ^ 2",
            InfixTokens = new IToken[]
            {
                new NumberToken(-2),
                new OperationToken(OperationType.Exponent),
                new NumberToken(3),
                new OperationToken(OperationType.Exponent),
                new NumberToken(2),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(-2),
                new NumberToken(3),
                new NumberToken(2),
                new OperationToken(OperationType.Exponent),
                new OperationToken(OperationType.Exponent),
            },
            Result = -512
        };
        
        public static readonly EquationTestDataItem Equation5 = new EquationTestDataItem
        {
            Equation = "1 - 2 + abs(-3) + 4 - 5 - 6 + 7",
            InfixTokens = new IToken[]
            {
                new NumberToken(1),
                new OperationToken(OperationType.Subtract),
                new NumberToken(2),
                new OperationToken(OperationType.Add),
                new StringToken("abs"),
                new GroupToken(GroupTokenType.Opening),
                new NumberToken(-3),
                new GroupToken(GroupTokenType.Closing),
                new OperationToken(OperationType.Add),
                new NumberToken(4),
                new OperationToken(OperationType.Subtract),
                new NumberToken(5),
                new OperationToken(OperationType.Subtract),
                new NumberToken(6),
                new OperationToken(OperationType.Add),
                new NumberToken(7),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(1),
                new NumberToken(2),
                new OperationToken(OperationType.Subtract),
                new NumberToken(-3),
                new StringToken("abs"),
                new OperationToken(OperationType.Add),
                new NumberToken(4),
                new OperationToken(OperationType.Add),
                new NumberToken(5),
                new OperationToken(OperationType.Subtract),
                new NumberToken(6),
                new OperationToken(OperationType.Subtract),
                new NumberToken(7),
                new OperationToken(OperationType.Add),
            },
            Result = 2
        };
        
        public static readonly EquationTestDataItem Equation6 = new EquationTestDataItem
        {
            Equation = "sin(PI / 6) + ln(E)",
            InfixTokens = new IToken[]
            {
                new StringToken("sin"),
                new GroupToken(GroupTokenType.Opening),
                new StringToken("PI"),
                new OperationToken(OperationType.Divide),
                new NumberToken(6),
                new GroupToken(GroupTokenType.Closing),
                new OperationToken(OperationType.Add),
                new StringToken("ln"),
                new GroupToken(GroupTokenType.Opening),
                new StringToken("E"),
                new GroupToken(GroupTokenType.Closing),
                
            },
            PostfixTokens = new IToken[]
            {
                new StringToken("PI"),
                new NumberToken(6),
                new OperationToken(OperationType.Divide),
                new StringToken("sin"),
                new StringToken("E"),
                new StringToken("ln"),
                new OperationToken(OperationType.Add),
            },
            Result = 1.5m
        };

        public static readonly EquationTestDataItem[] EquationTestDataItems =
        {
            Equation1,
            Equation2,
            Equation3,
            Equation4,
            Equation5,
            Equation6,
        };

        public static IEnumerable EquationToInfixTestCases
        {
            get
            {
                return EquationTestDataItems.Select(x =>
                    new TestCaseData(x.Equation, x.InfixTokens)
                        .SetName(x.Equation));
            }
        }

        public static IEnumerable InfixToPostfixTestCases
        {
            get
            {
                return EquationTestDataItems.Select(x =>
                    new TestCaseData(x.InfixTokens, x.PostfixTokens)
                        .SetName(x.Equation));
            }
        }

        public static IEnumerable PostfixToResultTestCases
        {
            get
            {
                return EquationTestDataItems.Select(x =>
                    new TestCaseData(x.PostfixTokens, x.Result)
                        .SetName(x.Equation));
            }
        }
    }
}