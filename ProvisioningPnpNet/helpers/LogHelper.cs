using ProvisioningPnpNet.constants;
using System;

namespace ProvisioningPnpNet.helpers
{
    public static class LogHelper
    {
        private static ConsoleColor defaultColor = ConsoleColor.Gray;
        public static void writeError(string message)
        {
            writeLog(ConsoleColor.Red, typeLog.space, message);
        }
        public static void writeWarning(string message)
        {
            writeLog(ConsoleColor.Yellow, typeLog.space, message);
        }
        public static void writeSuccess(string message)
        {
            writeLog(ConsoleColor.Green, typeLog.space, message);
        }
        public static void writeInfo(string message)
        {
            writeLog(ConsoleColor.Blue, typeLog.info, message);
        }
        public static void writeConsole(string message)
        {
            writeLog(ConsoleColor.DarkGreen, typeLog.normal, message);
        }
        public static void writeBasic(string message)
        {
            writeLog(defaultColor, typeLog.basic, message);
        }



        private static void writeLog(ConsoleColor color, typeLog type, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(string.Concat(generateTime(type), message));
            Console.ForegroundColor = defaultColor;
        }
        private static string generateTime(typeLog type)
        {
            string date = DateTime.UtcNow.ToString("HH:mm:ss");

            switch (type) {
                case typeLog.normal: date = ("-> "); break;
                case typeLog.basic: date = ("(" + date + ") - "); break;
                case typeLog.space: date = (" -> " + date + " - "); break;
                case typeLog.info: date = ("  --> " + date + " - "); break;
            }

            return date;
        }
    }
}
