using System;

namespace Sexy
{
	internal static class Debug
	{
		public static void OutputDebug(string format, params object[] t) 
		{
#if DEBUG
			Console.WriteLine("[DEBUG]" + string.Format(format, t));
#endif
		}

		public static void OutputDebug<T>(T t)
		{
#if DEBUG
			Console.WriteLine("[DEBUG]"+ t);
#endif
		}

		public static void OutputDebug<T1, T2>(T1 t1, T2 t2)
		{
#if DEBUG
			Console.WriteLine("[DEBUG]" + t1 + ": " + t2);
#endif
		}

		public static void ASSERT(bool value)
		{
		}
	}
}
