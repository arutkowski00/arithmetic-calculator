﻿using System.Collections.Generic;
using ArithmeticCalculator.Algorithms;
using ArithmeticCalculator.Tokens;
using ArithmeticCalculator.Tokens.OperationTokens;
using Moq;
using NUnit.Framework;

namespace ArithmeticCalculator.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private readonly Mock<IPostfixBuilder> _reversePolishNotationBuilder =
            new Mock<IPostfixBuilder>();

        private readonly Mock<IPostfixCalculator> _reversePolishNotationCalculator =
            new Mock<IPostfixCalculator>();

        private readonly Mock<IEquationParser> _equationParser =
            new Mock<IEquationParser>();

        private Calculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new Calculator(
                _reversePolishNotationBuilder.Object,
                _reversePolishNotationCalculator.Object,
                _equationParser.Object);
        }

        [Test]
        public void ReturnResultIfGivenValidEquation()
        {
            const string equation = "2 + 2";
            const int result = 4;

            var infixTokens = new IToken[]
                {new NumberToken(2, 1), new AddOperationToken(3), new NumberToken(2, 5)};
            var reversePolishTokens = new IToken[]
                {new NumberToken(2, 1), new NumberToken(2, 5), new AddOperationToken(3)};
            
            _equationParser.Setup(x => x.Parse(It.IsAny<string>()))
                .Returns(infixTokens);
            _reversePolishNotationBuilder.Setup(x => x.Build(It.IsAny<IEnumerable<IToken>>()))
                .Returns(reversePolishTokens);
            _reversePolishNotationCalculator.Setup(x => x.Calculate(It.IsAny<IEnumerable<IToken>>()))
                .Returns(result);
            
            var actualResult = _calculator.Calculate(equation);
            
            Assert.AreEqual(result, actualResult);
            _equationParser.Verify(x => x.Parse(equation), Times.Once);
            _reversePolishNotationBuilder.Verify(x => x.Build(infixTokens), Times.Once);
            _reversePolishNotationCalculator.Verify(x => x.Calculate(reversePolishTokens), Times.Once);
        }
    }
}
