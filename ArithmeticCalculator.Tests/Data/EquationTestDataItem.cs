using System.Collections.Generic;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Tests.Data
{
    public class EquationTestDataItem
    {
        public string Equation { get; set; }
        public IToken[] InfixTokens { get; set; }
        public IToken[] PostfixTokens { get; set; }
        public double Result { get; set; }
    }
}
