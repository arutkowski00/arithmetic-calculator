using System;

namespace ArithmeticCalculator.Utils
{
    public static class ConsoleEx
    {
        public static ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }
        
        public static string ReadLine()
        {
            return Console.ReadLine();
        }
        
        public static void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }
        
        public static void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public static void WriteLine(string format, ConsoleColor color, params object[] args)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            WriteLine(format, args);
            Console.ForegroundColor = currentColor;
        }
    }
}
