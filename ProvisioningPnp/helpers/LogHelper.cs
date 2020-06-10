using System;

namespace ProvisioningPnp.helpers
{
    public static class LogHelper
    {
        private static ConsoleColor defaultColor = ConsoleColor.Gray;
        public static void writeError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }
        public static void writeWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }
        public static void writeSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }
        public static void writeInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }
    }
}
