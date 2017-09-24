using System.Collections.Generic;
using NUnit.Framework;

namespace ArithmeticCalculator.Test
{
    public static class AssertHelpers
    {
        public static void AreListsEqual<T>(IList<T> expected, IList<T> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            
            for (var i = 0; i < expected.Count; i++)
            {
                var expectedObj = expected[i];
                var actualObj = actual[i];
                    
                Assert.AreEqual(expectedObj, actualObj);
            }
        }
    }
}