using System.Collections.Generic;
using System.Linq;
using ArithmeticCalculator.Algorithms;
using ArithmeticCalculator.Tests.Data;
using ArithmeticCalculator.Tests.Helpers;
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
    }
}
