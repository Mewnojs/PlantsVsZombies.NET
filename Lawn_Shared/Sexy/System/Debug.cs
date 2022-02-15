using System;
using System.Diagnostics;
using System.Reflection;

namespace Sexy
{
    public /*internal*/ static class Debug
    {
        public static void Log(object msg)
        {
            Log(DebugType.Info, msg);
        }

        public static void Log(DebugType msgtype, object msg)
        {
            msg = msg.ToString();
            Logger($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msgtype}] {msg}", msgtype);
        }

        public static void OutputDebug(string format, params object[] t)
        {
#if DEBUG
            Log(DebugType.Debug, string.Format(format, t));
#endif
        }

        public static void OutputDebug<T>(T t)
        {
#if DEBUG
            Log(DebugType.Debug, $"{t}");
#endif
        }

        public static void ASSERT(bool value)
        {
            if (!value) 
            {
                Log(DebugType.Error, $"Assertion Failed\n{new StackTrace()}");
#if DEBUG
                throw new Exception();
#endif
            }
        }

            private static void LoggerConsole(string s, DebugType msgtype) 
        {
            ConsoleColor fgColor = Console.ForegroundColor;
            ConsoleColor bgColor = Console.BackgroundColor;
            Action colorReset = Console.ResetColor;
            switch (msgtype)
            {
            case DebugType.Debug:
                break;
            case DebugType.Info:
                fgColor = ConsoleColor.Cyan;
                break;
            case DebugType.Warn:
                fgColor = ConsoleColor.Yellow;
                break;
            case DebugType.Error:
                fgColor = ConsoleColor.Red;
                break;
            case DebugType.Fatal:
                fgColor = ConsoleColor.White;
                bgColor = ConsoleColor.DarkRed;
                break;
            }
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            Console.WriteLine(s);
            colorReset();
        }

        public static Action<string, DebugType> Logger = LoggerConsole;
    }

    public enum DebugType
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}
