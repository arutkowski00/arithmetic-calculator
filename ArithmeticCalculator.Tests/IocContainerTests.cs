using NUnit.Framework;

namespace ArithmeticCalculator.Tests
{
    [TestFixture]
    public class IocContainerTests
    {
        [Test]
        public void VerifyContainerConfiguration()
        {
            var container = IocContainer.GetContainer();
            Assert.DoesNotThrow(() => container.Verify());
        }
    }
}
