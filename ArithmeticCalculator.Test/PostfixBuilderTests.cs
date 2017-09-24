using System.Collections.Generic;
using System.Linq;
using ArithmeticCalculator.Algorithms;
using ArithmeticCalculator.Test.Data;
using ArithmeticCalculator.Test.Helpers;
using ArithmeticCalculator.Tokens;
using NUnit.Framework;

namespace ArithmeticCalculator.Test
{
    [TestFixture]
    public class PostfixBuilderTests
    {
        private readonly PostfixBuilder _postfixBuilder = new PostfixBuilder();

        [Test, TestCaseSource(typeof(EquationTestData), nameof(EquationTestData.InfixToPostfixTestCases))]
        public void DoesBuildPostfixTokensFromInfixTokens(IList<IToken> infixTokens,
            IList<IToken> expectedPostfixTokens)
        {
            var postfixTokens = _postfixBuilder.Build(infixTokens).ToList();

            AssertHelpers.AreListsEqual(expectedPostfixTokens, postfixTokens);
        }
    }
}