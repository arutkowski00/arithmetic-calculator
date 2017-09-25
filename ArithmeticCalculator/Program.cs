using System;
using ArithmeticCalculator.Exceptions;

namespace ArithmeticCalculator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var container = IocContainer.GetContainer();
            container.Verify();

            var calculator = container.GetInstance<Calculator>();

            const string exitCommand = "exit";
            
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input == exitCommand)
                    break;

                try
                {
                    var result = calculator.Calculate(input);
                    Console.WriteLine("= {0}", result);
                }
                catch (ParseException ex)
                {
                    WriteError("[ERROR] {0}", ex.Message);
                }
                catch (Exception ex)
                {
                    WriteError("[FATAL] {0}", ex.ToString());
                }
            }
        }

        static void WriteError(string format, params object[] args)
        {
            var fgColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(format, args);
            Console.ForegroundColor = fgColor;
        }
    }
}