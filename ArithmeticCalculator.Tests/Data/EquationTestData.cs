﻿using System.Collections;
using System.Linq;
using ArithmeticCalculator.Tokens;
using ArithmeticCalculator.Tokens.OperationTokens;
using ArithmeticCalculator.Tokens.OperatorTokens;
using NUnit.Framework;

namespace ArithmeticCalculator.Tests.Data
{
    public static class EquationTestData
    {
        public static readonly EquationTestDataItem Equation1 = new EquationTestDataItem
        {
            Equation = "2 + (4 / 8) * 3",
            InfixTokens = new IToken[]
            {
                new NumberToken(2, 1),
                new AddOperationToken(3),
                new GroupOpenOperatorToken(5),
                new NumberToken(4, 6),
                new DivideOperationToken(8),
                new NumberToken(8, 10),
                new GroupCloseOperatorToken(11),
                new MultiplyOperationToken(13),
                new NumberToken(3, 15),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(2, 1),
                new NumberToken(4, 6),
                new NumberToken(8, 10),
                new DivideOperationToken(8),
                new NumberToken(3, 15),
                new MultiplyOperationToken(13),
                new AddOperationToken(3),
            },
            Result = 3.5
        };

        public static readonly EquationTestDataItem Equation2 = new EquationTestDataItem
        {
            Equation = "-2 + -3 * -(-4 / 8)",
            InfixTokens = new IToken[]
            {
                new NumberToken(-2, 1),
                new AddOperationToken(4),
                new NumberToken(-3, 6),
                new MultiplyOperationToken(9),
                new NumberToken(-1, 11),
                new MultiplyOperationToken(11),
                new GroupOpenOperatorToken(12),
                new NumberToken(-4, 13),
                new DivideOperationToken(16),
                new NumberToken(8, 18),
                new GroupCloseOperatorToken(19),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(-2, 1),
                new NumberToken(-3, 6),
                new NumberToken(-1, 11),
                new MultiplyOperationToken(9),
                new NumberToken(-4, 13),
                new NumberToken(8, 18),
                new DivideOperationToken(16),
                new MultiplyOperationToken(11),
                new AddOperationToken(4),
            },
            Result = -3.5
        };

        public static readonly EquationTestDataItem Equation3 = new EquationTestDataItem
        {
            Equation = "4.25 / (-10 * [-5.5 + 25.5])",
            InfixTokens = new IToken[]
            {
                new NumberToken(4.25, 1),
                new DivideOperationToken(6),
                new GroupOpenOperatorToken(8),
                new NumberToken(-10, 9),
                new MultiplyOperationToken(13),
                new GroupOpenOperatorToken(15),
                new NumberToken(-5.5, 16),
                new AddOperationToken(21),
                new NumberToken(25.5, 23),
                new GroupCloseOperatorToken(27),
                new GroupCloseOperatorToken(28),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(4.25, 1),
                new NumberToken(-10, 9),
                new NumberToken(-5.5, 16),
                new NumberToken(25.5, 23),
                new AddOperationToken(21),
                new MultiplyOperationToken(13),
                new DivideOperationToken(6),
            },
            Result = -0.02125
        };

        public static readonly EquationTestDataItem Equation4 = new EquationTestDataItem
        {
            Equation = "-2 ^ 3 ^ 2",
            InfixTokens = new IToken[]
            {
                new NumberToken(-2, 1),
                new ExponentOperationToken(4),
                new NumberToken(3, 6),
                new ExponentOperationToken(8),
                new NumberToken(2, 10),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(-2, 1),
                new NumberToken(3, 6),
                new NumberToken(2, 10),
                new ExponentOperationToken(8),
                new ExponentOperationToken(4),
            },
            Result = -512
        };
        
        public static readonly EquationTestDataItem Equation5 = new EquationTestDataItem
        {
            Equation = "1 - 2 + abs(-3) + 4 - 5 - 6 + 7",
            InfixTokens = new IToken[]
            {
                new NumberToken(1, 1),
                new SubtractOperationToken(3),
                new NumberToken(2, 5),
                new AddOperationToken(7),
                new StringToken("abs", 9),
                new GroupOpenOperatorToken(12),
                new NumberToken(-3, 13),
                new GroupCloseOperatorToken(15),
                new AddOperationToken(17),
                new NumberToken(4, 19),
                new SubtractOperationToken(21),
                new NumberToken(5, 23),
                new SubtractOperationToken(25),
                new NumberToken(6, 27),
                new AddOperationToken(29),
                new NumberToken(7, 31),
            },
            PostfixTokens = new IToken[]
            {
                new NumberToken(1, 1),
                new NumberToken(2, 5),
                new SubtractOperationToken(3),
                new NumberToken(-3, 13),
                new StringToken("abs", 9),
                new AddOperationToken(7),
                new NumberToken(4, 19),
                new AddOperationToken(17),
                new NumberToken(5, 23),
                new SubtractOperationToken(21),
                new NumberToken(6, 27),
                new SubtractOperationToken(25),
                new NumberToken(7, 31),
                new AddOperationToken(29),
            },
            Result = 2
        };
        
        public static readonly EquationTestDataItem Equation6 = new EquationTestDataItem
        {
            Equation = "sin(PI / 6) + ln(E)",
            InfixTokens = new IToken[]
            {
                new StringToken("sin", 1),
                new GroupOpenOperatorToken(4),
                new StringToken("PI", 5),
                new DivideOperationToken(8),
                new NumberToken(6, 10),
                new GroupCloseOperatorToken(11),
                new AddOperationToken(13),
                new StringToken("ln", 15),
                new GroupOpenOperatorToken(17),
                new StringToken("E", 18),
                new GroupCloseOperatorToken(19),
                
            },
            PostfixTokens = new IToken[]
            {
                new StringToken("PI", 5),
                new NumberToken(6, 10),
                new DivideOperationToken(8),
                new StringToken("sin", 1),
                new StringToken("E", 18),
                new StringToken("ln", 15),
                new AddOperationToken(13),
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
