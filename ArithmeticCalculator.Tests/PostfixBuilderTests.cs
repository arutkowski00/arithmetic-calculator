using System.Collections.Generic;
using System.Linq;
using ArithmeticCalculator.Algorithms;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Tests.Data;
using ArithmeticCalculator.Tests.Helpers;
using ArithmeticCalculator.Tokens;
using NUnit.Framework;

namespace ArithmeticCalculator.Tests
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

        [Test]
        public void ThrowExceptionIfTokensEnumerableHasUnsupportedToken()
        {
            var tokens = new IToken[]
            {
                new UnsupportedToken()
            };
            Assert.Throws<UnsupportedTokenException>(() => _postfixBuilder.Build(tokens));
        }
        
        private class UnsupportedToken : IToken
        {
            public int CharAt => 0;
            public bool IsNumber => false;
            public bool IsOperator => false;
        }
    }
}
