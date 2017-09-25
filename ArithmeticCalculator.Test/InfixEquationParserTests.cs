using System;
using System.Collections.Generic;
using System.Linq;
using ArithmeticCalculator.Algorithms;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Test.Data;
using ArithmeticCalculator.Test.Helpers;
using ArithmeticCalculator.Tokens;
using NUnit.Framework;

namespace ArithmeticCalculator.Test
{
    [TestFixture]
    public class InfixEquationParserTests
    {
        private readonly InfixEquationParser _infixEquationParser = new InfixEquationParser();

        [Test, TestCaseSource(typeof(EquationTestData), nameof(EquationTestData.EquationToInfixTestCases))]
        public void DoesReturnEquationAsInfixTokens(string equation, IList<IToken> expectedInfixTokens)
        {
            var infixTokens = _infixEquationParser.Parse(equation).ToList();

            AssertHelpers.AreListsEqual(expectedInfixTokens, infixTokens);
        }

        [Test]
        public void ThrowExceptionIfEquationContainsInvalidChars()
        {
            const string equation = "2 £ 2";

            Assert.Throws<UnknownSymbolException>(() => { _infixEquationParser.Parse(equation); });
        }

        [Test]
        public void ThrowExceptionIfEquationIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => { _infixEquationParser.Parse(null); });
        }

        [Test]
        public void ReturnEmptyEnumerableIfEquationIsEmpty()
        {
            Assert.IsEmpty(_infixEquationParser.Parse(""));
        }
    }
}