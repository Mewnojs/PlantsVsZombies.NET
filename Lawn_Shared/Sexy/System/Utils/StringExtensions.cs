using System;

namespace Sexy
{
	internal static class StringExtensions
	{
		public static int length(this string s)
		{
			if (s == null)
			{
				return 0;
			}
			return s.Length;
		}

		public static bool empty(this string s)
		{
			return string.IsNullOrEmpty(s);
		}

		public static bool StartsWithCharLimit(this string s, string contains, int endChar)
		{
			bool result = true;
			for (int i = 0; i < endChar; i++)
			{
				if (s.get_Chars(i) != contains.get_Chars(i))
				{
					result = false;
					break;
				}
			}
			return result;
		}
	}
}
