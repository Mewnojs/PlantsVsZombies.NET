using System;

namespace Sexy.Misc
{
	public static class SexyLocale
	{
		public static void SetSeperators(string theGrouping, string theSeperator)
		{
			SexyLocale.gGrouping = theGrouping;
			SexyLocale.gThousandSep = theSeperator;
		}

		public static void SetLocale(string theLocale)
		{
		}

		public static string StringToUpper(string theString)
		{
			return theString.ToUpper();
		}

		public static string StringToLower(string theString)
		{
			return theString.ToLower();
		}

		public static bool isalnum(char theChar)
		{
			return char.IsNumber(theChar);
		}

		public static string CommaSeparate(int theValue)
		{
			if (theValue < 0)
			{
				return "-" + SexyLocale.UCommaSeparate((uint)(-(uint)theValue));
			}
			return SexyLocale.UCommaSeparate((uint)theValue);
		}

		public static string UCommaSeparate(uint theValue)
		{
			char[] array = new char[64];
			if (theValue == 0U)
			{
				return "0";
			}
			string text = SexyLocale.gGrouping;
			int num = 64;
			int num2 = 0;
			if (text[num2] != SexyLocale.CHAR_MAX && text[num2] > '\0')
			{
				char c = SexyLocale.gThousandSep[0];
				int num3 = 0;
				while (theValue != 0U)
				{
					array[--num] = (char)(48U + theValue % 10U);
					theValue /= 10U;
					if (theValue != 0U && ++num3 == (int)text[num2])
					{
						array[--num] = c;
						num3 = 0;
						if (text[num2 + 1] > '\0')
						{
							num2++;
						}
					}
				}
			}
			else
			{
				while (theValue != 0U)
				{
					array[--num] = (char)(48U + theValue % 10U);
					theValue /= 10U;
				}
			}
			return new string(array, num, 64 - num);
		}

		public static string CommaSeparate64(long theValue)
		{
			if (theValue < 0L)
			{
				return "-" + SexyLocale.UCommaSeparate64((ulong)(-theValue));
			}
			return SexyLocale.UCommaSeparate64((ulong)theValue);
		}

		public static string UCommaSeparate64(ulong theValue)
		{
			return "";
		}

		public static string gGrouping = "\\3";

		public static string gThousandSep = ",";

		private static char CHAR_MAX = '\u007f';
	}
}
