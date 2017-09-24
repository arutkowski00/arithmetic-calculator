using System.Collections.Generic;
using ArithmeticCalculator.Tokens;

namespace ArithmeticCalculator.Test.Data
{
    public class EquationTestDataItem
    {
        public string Equation { get; set; }
        public IToken[] InfixTokens { get; set; }
        public IToken[] PostfixTokens { get; set; }
        public decimal Result { get; set; }
    }
}