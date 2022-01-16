using System;

namespace Sexy
{
	internal static class Debug
	{
		public static void Log(DebugType msgtype, string msg) 
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
			Logger($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msgtype}] {msg}");
			colorReset();
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
				Log(DebugType.Fatal, "Assertion Failed");
				throw new ApplicationException();
			}
		}

		public static Action<string> Logger = Console.WriteLine;

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
