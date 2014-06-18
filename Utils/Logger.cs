using System;

namespace PE_Explorer.Utils
{
    public enum ELogTypes : byte
    {
        DEBUG = 0,
        ERROR = 1,
        INFO = 2
    }
    public static class Logger
    {
        private static string[] msg = new string[] {"Debug","Error","Info"};
        public static void Log(ELogTypes logType,string format)
        {
            
            ConsoleColor c;
            switch(logType)
            {
                case ELogTypes.DEBUG:
                    c = ConsoleColor.Green;
                    break;
                case ELogTypes.ERROR:
                    c = ConsoleColor.Red;
                    break;
                case ELogTypes.INFO:
                    c = ConsoleColor.White;
                    break;
                default:
                    c = Console.ForegroundColor;
                    break;
            }

            Console.ForegroundColor = c;
            Console.Write(string.Format("<{0}>", msg[(int)logType]).PadRight(7,' '));
            Console.ResetColor();
            Console.WriteLine(" {0}",format);

        }
    }
}
