using System;
using System.Linq;
using ArithmeticCalculator.Algorithms;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Test.Helpers;
using ArithmeticCalculator.Tokens;
using NUnit.Framework;

namespace ArithmeticCalculator.Test
{
    [TestFixture]
    public class EquationParserTests
    {
        private readonly InfixEquationParser _infixEquationParser = new InfixEquationParser();
        
        [Test]
        public void DoesParseEquation()
        {
            const string equation = "2 + (4 / 8) * 3";
            var expectedTokens = new IToken[]
            {
                new NumberToken(2),
                new OperationToken(OperationType.Add),
                new GroupToken(GroupTokenType.Opening),
                new NumberToken(4),
                new OperationToken(OperationType.Divide),
                new NumberToken(8),
                new GroupToken(GroupTokenType.Closing), 
                new OperationToken(OperationType.Multiply),
                new NumberToken(3)
            }.ToList();
            
            var infixTokens = _infixEquationParser.Parse(equation).ToList();
            
            AssertHelpers.AreListsEqual(expectedTokens, infixTokens);
        }

        [Test]
        public void DoesParseEquationWithNegativeNumbers()
        {
            const string equation = "-2 + -3 * -(-4 / 8)";
            var expectedTokens = new IToken[]
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
                new GroupToken(GroupTokenType.Closing)
            }.ToList();
            
            var infixTokens = _infixEquationParser.Parse(equation).ToList();
            
            AssertHelpers.AreListsEqual(expectedTokens, infixTokens);
        }

        [Test]
        public void ThrowExceptionIfEquationContainsInvalidChars()
        {
            const string equation = "2 £ 2";

            Assert.Throws<UnknownTokenException>(() =>
            {
                _infixEquationParser.Parse(equation);
            });
        }
        
        [Test]
        public void ThrowExceptionIfEquationIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _infixEquationParser.Parse(null);
            });
        }

        [Test]
        public void ReturnEmptyEnumerableIfEquationIsEmpty()
        {
            Assert.IsEmpty(_infixEquationParser.Parse(""));
        }
    }
}