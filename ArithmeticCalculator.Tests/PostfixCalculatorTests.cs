using System;
using System.Collections.Generic;
using ArithmeticCalculator.Algorithms;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Tests.Data;
using ArithmeticCalculator.Tokens;
using NUnit.Framework;

namespace ArithmeticCalculator.Tests
{
    [TestFixture]
    public class PostfixCalculatorTests
    {
        private readonly PostfixCalculator _postfixCalculator = new PostfixCalculator();

        [Test, TestCaseSource(typeof(EquationTestData), nameof(EquationTestData.PostfixToResultTestCases))]
        public void DoesBuildPostfixTokensFromInfixTokens(IList<IToken> postfixTokens, double expectedResult)
        {
            var result = _postfixCalculator.Calculate(postfixTokens);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ThrowExceptionIfNumberTokenUsageIsInvalid()
        {
            // Equation: 2 2 + 2
            var tokens = new IToken[]
            {
                new NumberToken(2, 1),
                new NumberToken(2, 3),
                new OperationToken(OperationType.Add, 5),
                new NumberToken(2, 7)
            };
            
            Assert.Throws<AggregateException>(() => _postfixCalculator.Calculate(tokens));
        }

        [Test]
        public void ThrowExceptionIfOperationTokenUsageIsInvalid()
        {
            // Equation: 2 * / 2
            var tokens = new IToken[]
            {
                new NumberToken(2, 1),
                new OperationToken(OperationType.Multiply, 3),
                new OperationToken(OperationType.Divide, 5),
                new NumberToken(2, 7)
            };
            
            Assert.Throws<ParseException>(() => _postfixCalculator.Calculate(tokens));
        }
    }
}