using System;
using System.Collections.Generic;
using System.Linq;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace JeffLib
{
	public static class Common
	{
		public static bool _ATLIMIT(float cc, float mc, float d)
		{
			return (cc >= mc && d > 0f) || (cc <= mc && d < 0f) || d == 0f;
		}

		public static bool DoneMoving(float coord, float vel, float target)
		{
			return (vel >= 0f && coord >= target) || (vel <= 0f && coord <= target);
		}

		public static int GetAlphaFromUpdateCount(int update_count, int modifier)
		{
			int num = update_count % modifier;
			if (num < modifier / 2)
			{
				return num * 2;
			}
			return (modifier - num) * 2;
		}

		public static void StringDimensions(string str, Font f, out int widest, out int height)
		{
			Common.StringDimensions(str, f, out widest, out height, true);
		}

		public static void StringDimensions(string str, Font f, out int widest, out int height, bool real_newline)
		{
			widest = 0;
			height = 0;
			List<string> list = new List<string>();
			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				if (real_newline && str[i] == '\n')
				{
					list.Add(str.Substring(num, i - num));
					num = i + 1;
				}
				else if (!real_newline && str[i] == '\\' && i + 1 < str.Length && str[i + 1] == 'n')
				{
					list.Add(str.Substring(num, i - num));
					num = i + 2;
				}
			}
			if (num < str.Length)
			{
				list.Add(str.Substring(num));
			}
			if (Enumerable.Count<string>(list) == 0)
			{
				list.Add(str);
			}
			height = f.GetHeight() * Enumerable.Count<string>(list);
			for (int j = 0; j < Enumerable.Count<string>(list); j++)
			{
				int num2 = f.StringWidth(list[j]);
				if (num2 > widest)
				{
					widest = num2;
				}
			}
		}

		public static SexyVector2 RotatePoint(float pAngle, SexyVector2 pVector, SexyVector2 pCenter)
		{
			float num = pVector.x - pCenter.x;
			float num2 = pVector.y - pCenter.y;
			float theX = (float)((double)pCenter.x + (double)num * Math.Cos((double)pAngle) + (double)num2 * Math.Sin((double)pAngle));
			float theY = (float)((double)pCenter.y + (double)num2 * Math.Cos((double)pAngle) - (double)num * Math.Sin((double)pAngle));
			return new SexyVector2(theX, theY);
		}

		public static SexyVector2 RotatePoint(float pAngle, SexyVector2 pVector)
		{
			return Common.RotatePoint(pAngle, pVector, new SexyVector2(0f, 0f));
		}

		public static void RotatePoint(float pAngle, ref float x, ref float y, float cx, float cy)
		{
			SexyVector2 sexyVector = Common.RotatePoint(pAngle, new SexyVector2(x, y), new SexyVector2(cx, cy));
			x = sexyVector.x;
			y = sexyVector.y;
		}

		public static string UpdateToTimeStr(int u)
		{
			return Common.UpdateToTimeStr(u, false, 2);
		}

		public static string UpdateToTimeStr(int u, bool use_hour_field)
		{
			return Common.UpdateToTimeStr(u, use_hour_field, 2);
		}

		public static string UpdateToTimeStr(int u, bool use_hour_field, int min_hour_digits)
		{
			bool flag = u < 0;
			if (flag)
			{
				u *= -1;
			}
			int num = u / 100 % 60;
			int num2 = u / 6000;
			if (!use_hour_field)
			{
				return string.Format((num < 10) ? "{0}{1}:0{2}" : "{0}{1}:{2}", flag ? "-" : "", num2, num);
			}
			int num3 = num2 / 60;
			num2 %= 60;
			string text = string.Format("{0}{1}", flag ? "-" : "", num3);
			int length = text.Length;
			for (int i = length; i < min_hour_digits; i++)
			{
				text = "0" + text;
			}
			return text + string.Format((num2 < 10) ? ":0{0}" : ":{0}", num2) + string.Format((num < 10) ? ":0{0}" : ":{0}", num);
		}

		public static uint StrToHex(string str)
		{
			uint num = 0U;
			int length = str.Length;
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				uint num2 = 0U;
				if (c >= '0' && c <= '9')
				{
					num2 = (uint)(c - '0');
				}
				else if (c == 'a' || c == 'A')
				{
					num2 = 10U;
				}
				else if (c == 'b' || c == 'B')
				{
					num2 = 11U;
				}
				else if (c == 'c' || c == 'C')
				{
					num2 = 12U;
				}
				else if (c == 'd' || c == 'D')
				{
					num2 = 13U;
				}
				else if (c == 'e' || c == 'E')
				{
					num2 = 14U;
				}
				else if (c == 'f' || c == 'F')
				{
					num2 = 15U;
				}
				num += num2 << (length - i - 1) * 4;
			}
			return num;
		}

		public static int StrFindNoCase(string str, string cmp)
		{
			return str.ToLower().IndexOf(cmp.ToLower());
		}

		public static bool RightClick(int c)
		{
			return c == Common.RIGHT_BUTTON || c == Common.DOUBLE_RIGHT_BUTTON;
		}

		public static string PathToResName(string path, string start_dir, string start_dir_replace_string)
		{
			string text = "";
			int num = path.IndexOf(start_dir);
			if (num == -1)
			{
				return text;
			}
			text = start_dir_replace_string;
			for (int i = num + start_dir.Length; i < path.Length; i++)
			{
				if (path[i] == '/' || path[i] == '\\')
				{
					text += "_";
				}
				else
				{
					text += path[i];
				}
			}
			if (text[text.Length - 1] == '_')
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text.ToUpper();
		}

		public static string StripFileExtension(string fname)
		{
			int num = fname.LastIndexOf('.');
			if (num == -1)
			{
				return fname;
			}
			return fname.Substring(0, num);
		}

		public static string TruncateStr(string str, Font f, int width)
		{
			int num = f.StringWidth(str);
			if (num <= width)
			{
				return str;
			}
			int num2 = f.StringWidth("...");
			int num3 = num2;
			string text = "";
			for (int i = 0; i < str.Length; i++)
			{
				string text2 = str.Substring(i, 1);
				int num4 = f.StringWidth(text2);
				if (num3 + num4 >= width)
				{
					break;
				}
				num3 += num4;
				text += text2;
			}
			return text + "...";
		}

		public static int RIGHT_BUTTON = -1;

		public static int LEFT_BUTTON = 1;

		public static int DOUBLE_LEFT_BUTTON = 2;

		public static int DOUBLE_RIGHT_BUTTON = -2;

		public static int MIDDLE_BUTTON = 3;

		public static ulong MTRAND_MAX = ulong.MaxValue;
	}
}
