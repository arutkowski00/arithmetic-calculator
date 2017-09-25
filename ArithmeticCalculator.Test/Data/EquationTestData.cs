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
                new NumberToken(2, 1),
                new OperationToken(OperationType.Add, 3),
                new GroupToken(GroupTokenType.Opening, 5),
                new NumberToken(4, 6),
                new OperationToken(OperationType.Divide, 8),
                new NumberToken(8, 10),
                new GroupToken(GroupTokenType.Closing, 11),
                new OperationToken(OperationType.Multiply, 13),
                new NumberToken(3, 15),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(2, 1),
                new NumberToken(4, 6),
                new NumberToken(8, 10),
                new OperationToken(OperationType.Divide, 8),
                new NumberToken(3, 15),
                new OperationToken(OperationType.Multiply, 13),
                new OperationToken(OperationType.Add, 3),
            },
            Result = 3.5
        };

        public static readonly EquationTestDataItem Equation2 = new EquationTestDataItem
        {
            Equation = "-2 + -3 * -(-4 / 8)",
            InfixTokens = new IToken[]
            {
                new NumberToken(-2, 1),
                new OperationToken(OperationType.Add, 4),
                new NumberToken(-3, 6),
                new OperationToken(OperationType.Multiply, 9),
                new NumberToken(-1, 11),
                new OperationToken(OperationType.Multiply, 11),
                new GroupToken(GroupTokenType.Opening, 12),
                new NumberToken(-4, 13),
                new OperationToken(OperationType.Divide, 16),
                new NumberToken(8, 18),
                new GroupToken(GroupTokenType.Closing, 19),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(-2, 1),
                new NumberToken(-3, 6),
                new NumberToken(-1, 11),
                new OperationToken(OperationType.Multiply, 9),
                new NumberToken(-4, 13),
                new NumberToken(8, 18),
                new OperationToken(OperationType.Divide, 16),
                new OperationToken(OperationType.Multiply, 11),
                new OperationToken(OperationType.Add, 4),
            },
            Result = -3.5
        };

        public static readonly EquationTestDataItem Equation3 = new EquationTestDataItem
        {
            Equation = "4.25 / (-10 * [-5.5 + 25.5])",
            InfixTokens = new IToken[]
            {
                new NumberToken(4.25, 1),
                new OperationToken(OperationType.Divide, 6),
                new GroupToken(GroupTokenType.Opening, 8),
                new NumberToken(-10, 9),
                new OperationToken(OperationType.Multiply, 13),
                new GroupToken(GroupTokenType.Opening, 15),
                new NumberToken(-5.5, 16),
                new OperationToken(OperationType.Add, 21),
                new NumberToken(25.5, 23),
                new GroupToken(GroupTokenType.Closing, 27),
                new GroupToken(GroupTokenType.Closing, 28),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(4.25, 1),
                new NumberToken(-10, 9),
                new NumberToken(-5.5, 16),
                new NumberToken(25.5, 23),
                new OperationToken(OperationType.Add, 21),
                new OperationToken(OperationType.Multiply, 13),
                new OperationToken(OperationType.Divide, 6),
            },
            Result = -0.02125
        };

        public static readonly EquationTestDataItem Equation4 = new EquationTestDataItem
        {
            Equation = "-2 ^ 3 ^ 2",
            InfixTokens = new IToken[]
            {
                new NumberToken(-2, 1),
                new OperationToken(OperationType.Exponent, 4),
                new NumberToken(3, 6),
                new OperationToken(OperationType.Exponent, 8),
                new NumberToken(2, 10),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(-2, 1),
                new NumberToken(3, 6),
                new NumberToken(2, 10),
                new OperationToken(OperationType.Exponent, 8),
                new OperationToken(OperationType.Exponent, 4),
            },
            Result = -512
        };
        
        public static readonly EquationTestDataItem Equation5 = new EquationTestDataItem
        {
            Equation = "1 - 2 + abs(-3) + 4 - 5 - 6 + 7",
            InfixTokens = new IToken[]
            {
                new NumberToken(1, 1),
                new OperationToken(OperationType.Subtract, 3),
                new NumberToken(2, 5),
                new OperationToken(OperationType.Add, 7),
                new StringToken("abs", 9),
                new GroupToken(GroupTokenType.Opening, 12),
                new NumberToken(-3, 13),
                new GroupToken(GroupTokenType.Closing, 15),
                new OperationToken(OperationType.Add, 17),
                new NumberToken(4, 19),
                new OperationToken(OperationType.Subtract, 21),
                new NumberToken(5, 23),
                new OperationToken(OperationType.Subtract, 25),
                new NumberToken(6, 27),
                new OperationToken(OperationType.Add, 29),
                new NumberToken(7, 31),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(1, 1),
                new NumberToken(2, 5),
                new OperationToken(OperationType.Subtract, 3),
                new NumberToken(-3, 13),
                new StringToken("abs", 9),
                new OperationToken(OperationType.Add, 7),
                new NumberToken(4, 19),
                new OperationToken(OperationType.Add, 17),
                new NumberToken(5, 23),
                new OperationToken(OperationType.Subtract, 21),
                new NumberToken(6, 27),
                new OperationToken(OperationType.Subtract, 25),
                new NumberToken(7, 31),
                new OperationToken(OperationType.Add, 29),
            },
            Result = 2
        };
        
        public static readonly EquationTestDataItem Equation6 = new EquationTestDataItem
        {
            Equation = "sin(PI / 6) + ln(E)",
            InfixTokens = new IToken[]
            {
                new StringToken("sin", 1),
                new GroupToken(GroupTokenType.Opening, 4),
                new StringToken("PI", 5),
                new OperationToken(OperationType.Divide, 8),
                new NumberToken(6, 10),
                new GroupToken(GroupTokenType.Closing, 11),
                new OperationToken(OperationType.Add, 13),
                new StringToken("ln", 15),
                new GroupToken(GroupTokenType.Opening, 17),
                new StringToken("E", 18),
                new GroupToken(GroupTokenType.Closing, 19),
                
            },
            PostfixTokens = new IToken[]
            {
                new StringToken("PI", 5),
                new NumberToken(6, 10),
                new OperationToken(OperationType.Divide, 8),
                new StringToken("sin", 1),
                new StringToken("E", 18),
                new StringToken("ln", 15),
                new OperationToken(OperationType.Add, 13),
            },
            Result = 1.5
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