using System;

namespace Sexy
{
	internal static class Debug
	{
		public static void Log(Action<string> logger, Type msgtype, string msg) 
		{
			ConsoleColor fgColor = Console.ForegroundColor;
			ConsoleColor bgColor = Console.BackgroundColor;
			Action colorReset = Console.ResetColor;
			switch (msgtype) 
			{
				case Type.Debug:
					break;
				case Type.Info:
					fgColor = ConsoleColor.Cyan;
					break;
				case Type.Warn:
					fgColor = ConsoleColor.Yellow;
					break;
				case Type.Error:
					fgColor = ConsoleColor.Red;
					break;
				case Type.Fatal:
					fgColor = ConsoleColor.White;
					bgColor = ConsoleColor.DarkRed;
					break;
			}
			Console.ForegroundColor = fgColor;
			Console.BackgroundColor = bgColor;
			logger($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msgtype}] {msg}");
			colorReset();
		}

		public static void OutputDebug(string format, params object[] t) 
		{
#if DEBUG
			Log(Console.WriteLine, Type.Debug, string.Format(format, t));
#endif
		}

		public static void OutputDebug<T>(T t)
		{
#if DEBUG
			Log(Console.WriteLine, Type.Debug, $"{t}");
#endif
		}

		public static void ASSERT(bool value)
		{
		}

		public enum Type 
		{
			Debug,
			Info,
			Warn,
			Error,
			Fatal
		}
	}
}
