using System;
using System.Diagnostics;
using System.Reflection;

namespace Sexy
{
	public /*internal*/ static class Debug
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
#if DEBUG
            if (!value) 
			{
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                MethodBase m = sf.GetMethod();
                string tracing = ""; 
                for (int i = 2; i < st.FrameCount; i++) 
                {
                    var mm = st.GetFrame(i).GetMethod();
                    tracing += $"\n\tfrom {mm.DeclaringType}.{ mm.Name}";
                }
                Log(DebugType.Fatal, $"Assertion Failed at: \n{m.DeclaringType}.{m.Name}" +
                    tracing);
				throw new Exception();
			}
#endif
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
