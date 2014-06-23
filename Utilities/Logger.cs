using System;

namespace Utilities
{
    public enum ELogTypes : byte
    {
        Debug = 0,
        Error = 1,
        Info = 2
    }
    public static class Logger
    {
        private static string[] msg = new string[] {"Debug","Error","Info"};
        public static void Log(ELogTypes logType,string format)
        {
            
            ConsoleColor c;
            switch(logType)
            {
                case ELogTypes.Debug:
                    c = ConsoleColor.Green;
                    break;
                case ELogTypes.Error:
                    c = ConsoleColor.Red;
                    break;
                case ELogTypes.Info:
                    c = ConsoleColor.White;
                    break;
                default:
                    c = Console.ForegroundColor;
                    break;
            }

            Console.ForegroundColor = c;
            Console.Write(string.Format("<{0}>", logType.ToString()).PadRight(7,' '));
            Console.ResetColor();
            Console.WriteLine(" {0}",format);

        }
    }
}
