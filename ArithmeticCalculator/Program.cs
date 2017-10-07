using System;
using ArithmeticCalculator.Exceptions;
using Console = ArithmeticCalculator.Utils.ConsoleEx;

namespace ArithmeticCalculator
{
    internal class Program
    {
        private const string ExitCommand = "exit";
        private readonly Calculator _calculator;

        public static void Main(string[] args)
        {
            var container = IocContainer.GetContainer();
            container.Verify();

            var program = container.GetInstance<Program>();
            program.RunCli();
        }

        public Program(Calculator calculator)
        {
            _calculator = calculator;    
        }

        public void RunCli()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("Welcome to Arithmetic Calculator!");
            Console.WriteLine("Press ^C or type 'exit' to quit\n");
            
            Console.ForegroundColor = ConsoleColor.White;
            
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input == ExitCommand)
                    return;

                try
                {
                    var result = _calculator.Calculate(input);
                    Console.WriteLine($"= {result}", ConsoleColor.Green);
                }
                catch (AggregateException ex)
                {
                    foreach (var innerEx in ex.InnerExceptions)
                    {
                        WriteException(innerEx);
                    }
                }
                catch (Exception ex)
                {
                    WriteException(ex);
                }
            }
        }

        private static void WriteException(Exception exception)
        {
            if (exception is ParseException)
            {
                WriteError(LogSeverity.Error, exception.Message);
            }
            else
            {
                WriteError(LogSeverity.Fatal, exception.ToString());
            }
        }

        private static void WriteError(LogSeverity severity, string format, params object[] args)
        {
            Console.WriteLine($"[{severity.ToString().ToUpper()}] {format}", ConsoleColor.Red, args);
        }

        private enum LogSeverity
        {
            Error,
            Fatal
        }
    }
}
