using ArithmeticCalculator.Algorithms;
using SimpleInjector;

namespace ArithmeticCalculator
{
    public class IocContainer
    {
        public static Container GetContainer()
        {
            var container = new Container();

            container.Register<Calculator>();
            container.Register<IEquationParser, InfixEquationParser>();
            container.Register<IPostfixBuilder, PostfixBuilder>();
            container.Register<IPostfixCalculator, PostfixCalculator>();
            
            return container;
        }
    }
}