using System.Linq;
using ArithmeticCalculator.Exceptions;
using ArithmeticCalculator.Tokens;
using NUnit.Framework;

namespace ArithmeticCalculator.Test
{
    [TestFixture]
    public class EquationParserTests
    {
        private readonly EquationParser _equationParser = new EquationParser();
        
        [Test]
        public void ReturnTokensIfEquationIsValid()
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
            };
            
            var infixTokens = _equationParser.Parse(equation).ToArray();
            
            Assert.AreEqual(expectedTokens.Length, infixTokens.Length);
            
            for (var i = 0; i < expectedTokens.Length; i++)
            {
                var expectedToken = expectedTokens[i];
                var infixToken = infixTokens[i];
                    
                Assert.AreEqual(expectedToken, infixToken);
            }
        }

        [Test]
        public void ThrowExceptionIfEquationContainsInvalidChars()
        {
            const string equation = "2 £ 2";

            Assert.Throws<UnknownTokenException>(() =>
            {
                _equationParser.Parse(equation);
            });
        }
    }
}