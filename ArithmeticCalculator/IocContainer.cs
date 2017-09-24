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
            container.Register<IEquationParser, EquationParser>();
            container.Register<IReversePolishNotationBuilder, ReversePolishNotationBuilder>();
            container.Register<IReversePolishNotationCalculator, ReversePolishNotationCalculator>();
            
            return container;
        }
    }
}