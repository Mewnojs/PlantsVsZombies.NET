using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Sexy.TodLib
{
	internal static class TodStringFile
	{
		public static void TodStringListSetColors(TodStringListFormat[] theFormats, int theCount)
		{
			TodStringFile.gTodStringFormats = theFormats;
			TodStringFile.gTodStringFormatCount = theCount;
		}

		public static void TodStringListLoad(string theFileName)
		{
			if (!TodStringFile.TodStringListReadFile(theFileName))
			{
				Common.StrFormat_("Failed to load string list file '{0}'", theFileName);
			}
			TodStringFile.StringsLoaded = true;
		}

		public static string TodStringTranslate(string theString)
		{
			if (theString.Length >= 3 && theString[0] == '[')
			{
				string text;
				if (!TodStringFile.todStringTranslateCache.TryGetValue(theString, out text))
				{
					text = theString.Substring(1, theString.Length - 2);
					TodStringFile.todStringTranslateCache.Add(theString, text);
				}
				return TodStringFile.TodStringListFind(text);
			}
			return theString;
		}

		public static bool TodStringListExists(string theString)
		{
			int num = theString.length();
			if (num < 3 || theString[0] != '[')
			{
				return false;
			}
			string text = theString.Substring(1, num - 2);
			return TodStringFile.gStringProperties.ContainsKey(text);
		}

		public static int TodDrawStringWrapped(Graphics g, string theText, TRect theRect, Font theFont, SexyColor theColor, DrawStringJustification theJustification)
		{
			return TodStringFile.TodDrawStringWrapped(g, theText, theRect, theFont, theColor, theJustification, '\n');
		}

		public static int TodDrawStringWrapped(Graphics g, string theText, TRect theRect, Font theFont, SexyColor theColor, DrawStringJustification theJustification, char lineBreakChar)
		{
			return TodStringFile.TodDrawStringWrapped(g, theText, theRect, theFont, theColor, theJustification, false, lineBreakChar);
		}

		public static int TodDrawStringWrappedHeight(Graphics g, string theText, TRect theRect, Font theFont, SexyColor theColor, DrawStringJustification theJustification)
		{
			return TodStringFile.TodDrawStringWrappedHeight(g, theText, theRect, theFont, theColor, theJustification, false);
		}

		public static int TodDrawStringWrappedHeight(Graphics g, string theText, TRect theRect, Font theFont, SexyColor theColor, DrawStringJustification theJustification, bool widowSupression)
		{
			string theText2 = TodStringFile.TodStringTranslate(theText);
			TRect theRect2 = theRect;
			if (theJustification == DrawStringJustification.LeftVerticalMiddle || theJustification == DrawStringJustification.RightVerticalMiddle || theJustification == DrawStringJustification.CenterVerticalMiddle)
			{
				int num = TodStringFile.TodDrawStringWrappedHelper(g, theText2, theRect2, theFont, theColor, theJustification, false, widowSupression);
				theRect2.mY += (theRect.mHeight - num) / 2;
			}
			return TodStringFile.TodDrawStringWrappedHelper(g, theText2, theRect2, theFont, theColor, theJustification, false, widowSupression);
		}

		public static int TodDrawStringWrapped(Graphics g, string theText, TRect theRect, Font theFont, SexyColor theColor, DrawStringJustification theJustification, bool widowSupression)
		{
			return TodStringFile.TodDrawStringWrapped(g, theText, theRect, theFont, theColor, theJustification, widowSupression, '\n');
		}

		public static int TodDrawStringWrapped(Graphics g, string theText, TRect theRect, Font theFont, SexyColor theColor, DrawStringJustification theJustification, bool widowSupression, char lineBreakChar)
		{
			string theText2 = TodStringFile.TodStringTranslate(theText);
			TRect theRect2 = theRect;
			if (theJustification == DrawStringJustification.LeftVerticalMiddle || theJustification == DrawStringJustification.RightVerticalMiddle || theJustification == DrawStringJustification.CenterVerticalMiddle)
			{
				int num = TodStringFile.TodDrawStringWrappedHelper(g, theText2, theRect2, theFont, theColor, theJustification, false, widowSupression);
				theRect2.mY += (theRect.mHeight - num) / 2;
			}
			return TodStringFile.TodDrawStringWrappedHelper(g, theText2, theRect2, theFont, theColor, theJustification, true, widowSupression, lineBreakChar);
		}

		public static bool TodStringListReadName(string thePtr, ref string theName, ref int startIndex)
		{
			int num = thePtr.IndexOf('[', startIndex);
			if (num < 0)
			{
				if (TodStringFile.strspn(thePtr, "\n\r\t".ToCharArray(), startIndex) != -1)
				{
					Debug.OutputDebug<string>("Failed to find string name");
					return false;
				}
				theName = string.Empty;
				return true;
			}
			else
			{
				int num2 = num + 1;
				int num3 = thePtr.IndexOf(']', startIndex);
				if (num3 < 0)
				{
					Debug.OutputDebug<string>("Failed to find ']'");
					return false;
				}
				int num4 = num3 - num2;
				theName = thePtr.Substring(num2, num4);
				theName = theName.Trim();
				if (theName.Length == 0)
				{
					Debug.OutputDebug<string>("Name too short");
					return false;
				}
				startIndex = num3 + 1;
				return true;
			}
		}

		public static void TodStringRemoveReturnChars(ref string theValue)
		{
			theValue = theValue.Replace("\r", "");
		}

		public static bool TodStringListReadValue(string thePtr, ref string theValue, ref int startIndex)
		{
			int num = thePtr.IndexOf('[', startIndex);
			if (num < 0)
			{
				theValue = thePtr.Substring(startIndex);
			}
			else
			{
				theValue = thePtr.Substring(startIndex, num - startIndex);
			}
			if (theValue.IndexOf("/c/") != -1)
			{
				theValue = theValue.Replace("/c/", "©");
			}
			theValue = theValue.Trim();
			TodStringFile.TodStringRemoveReturnChars(ref theValue);
			int num2 = 0;
			int num3;
			do
			{
				num3 = theValue.IndexOf("%");
				if (num3 >= 0)
				{
					if (!char.IsWhiteSpace(theValue[num3 + 1]))
					{
						theValue = theValue.Remove(num3, 2);
						theValue = theValue.Insert(num3, "{" + num2.ToString() + "}");
						num2++;
					}
					else
					{
						num3 = -1;
					}
				}
			}
			while (num3 != -1);
			return true;
		}

		public static bool TodStringListReadItems(string theFileText)
		{
			int num = 0;
			for (;;)
			{
				string empty = string.Empty;
				if (!TodStringFile.TodStringListReadName(theFileText, ref empty, ref num))
				{
					break;
				}
				if (empty.Length == 0)
				{
					return true;
				}
				string empty2 = string.Empty;
				if (!TodStringFile.TodStringListReadValue(theFileText, ref empty2, ref num))
				{
					return false;
				}
				string text = empty.ToUpper();
				TodStringFile.gStringProperties[text] = empty2;
				GlobalStaticVars.gSexyAppBase.SetString(text, empty2);
			}
			return false;
		}

		public static bool TodStringListReadFile(string theFileName)
		{
			try
			{
				using (Stream stream = TitleContainer.OpenStream(theFileName))
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						string theFileText = streamReader.ReadToEnd();
						if (!TodStringFile.TodStringListReadItems(theFileText))
						{
							return false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.OutputDebug<string>("The file: " + theFileName + " could not be read");
				Debug.OutputDebug<string>(ex.Message);
				return false;
			}
			return true;
		}

		public static string TodStringListFind(string theName)
		{
			if (TodStringFile.gStringProperties.ContainsKey(theName))
			{
				return TodStringFile.gStringProperties[theName];
			}
			return Common.StrFormat_("<Missing {0}>", theName);
		}

		internal static bool CharIsSpaceInFormat(char theChar, TodStringListFormat theCurrentFormat)
		{
			return theChar == ' ' || (TodCommon.TestBit(theCurrentFormat.mFormatFlags, 0) && theChar == '\n');
		}

		internal static void TodWriteStringSetFormat(string theFormat, ref TodStringListFormat theCurrentFormat)
		{
			for (int i = 0; i < TodStringFile.gTodStringFormatCount; i++)
			{
				TodStringListFormat todStringListFormat = TodStringFile.gTodStringFormats[i];
				int length = todStringListFormat.mFormatName.Length;
				if (string.Compare(theFormat, todStringListFormat.mFormatName) == 0)
				{
					if (todStringListFormat.mNewFont != null)
					{
						theCurrentFormat.mNewFont = todStringListFormat.mNewFont;
					}
					theCurrentFormat.mNewColor = todStringListFormat.mNewColor;
					theCurrentFormat.mLineSpacingOffset = todStringListFormat.mLineSpacingOffset;
					theCurrentFormat.mFormatFlags = todStringListFormat.mFormatFlags;
					return;
				}
			}
		}

		internal static int TodWriteString(Graphics g, string theString, int theX, int theY, TodStringListFormat theCurrentFormat, int theWidth, DrawStringJustification theJustification, bool drawString, int theOffset, int theLength)
		{
			Font newFont = theCurrentFormat.mNewFont;
			if (drawString)
			{
				switch (theJustification)
				{
				case DrawStringJustification.Right:
				case DrawStringJustification.RightVerticalMiddle:
					theX += theWidth - TodStringFile.TodWriteString(g, theString, theX, theY, theCurrentFormat, theWidth, DrawStringJustification.Left, false, theOffset, theLength);
					break;
				case DrawStringJustification.Center:
				case DrawStringJustification.CenterVerticalMiddle:
					theX += (theWidth - TodStringFile.TodWriteString(g, theString, theX, theY, theCurrentFormat, theWidth, DrawStringJustification.Left, false, theOffset, theLength)) / 2;
					break;
				}
			}
			if (theLength < 0 || theOffset + theLength > theString.length())
			{
				theLength = theString.length();
			}
			else
			{
				theLength = theOffset + theLength;
			}
			TodStringFile.TodWriteStringBuilder.Remove(0, TodStringFile.TodWriteStringBuilder.Length);
			int num = 0;
			bool flag = false;
			int num2 = 0;
			for (int i = theOffset; i < theLength; i++)
			{
				if (theString[i] == '{')
				{
					int num3 = num2 + i;
					int num4 = num3 + 1;
					int num5 = theString.IndexOf('}', num4);
					if (num5 >= 0)
					{
						i += num5 - num3;
						if (drawString)
						{
							newFont.DrawString(g, g.mTransX + theX + num, g.mTransY + theY, TodStringFile.TodWriteStringBuilder, theCurrentFormat.mNewColor);
						}
						num += newFont.StringWidth(TodStringFile.TodWriteStringBuilder);
						TodStringFile.TodWriteStringBuilder.Remove(0, TodStringFile.TodWriteStringBuilder.Length);
						string theFormat = theString.Substring(num4, num5 - (num3 + 1));
						TodStringFile.TodWriteStringSetFormat(theFormat, ref theCurrentFormat);
						newFont = theCurrentFormat.mNewFont;
					}
				}
				else
				{
					if (TodCommon.TestBit(theCurrentFormat.mFormatFlags, 0))
					{
						if (TodStringFile.CharIsSpaceInFormat(theString[i], theCurrentFormat))
						{
							if (!flag)
							{
								TodStringFile.TodWriteStringBuilder.Append(" ");
								flag = true;
								goto IL_1AA;
							}
							goto IL_1AA;
						}
						else
						{
							flag = false;
						}
					}
					TodStringFile.TodWriteStringBuilder.Append(theString[i]);
				}
				IL_1AA:;
			}
			if (drawString)
			{
				newFont.DrawString(g, g.mTransX + theX + num, g.mTransY + theY, TodStringFile.TodWriteStringBuilder, theCurrentFormat.mNewColor);
			}
			return num + newFont.StringWidth(TodStringFile.TodWriteStringBuilder);
		}

		internal static int TodWriteWordWrappedHelper(Graphics g, string theString, int theX, int theY, TodStringListFormat theCurrentFormat, int theWidth, DrawStringJustification theJustification, bool drawString, int theOffset, int theLength, int theMaxChars)
		{
			if (theOffset + theLength > theMaxChars)
			{
				theLength = theMaxChars - theOffset;
				if (theLength <= 0)
				{
					return -1;
				}
			}
			return TodStringFile.TodWriteString(g, theString, theX, theY, theCurrentFormat, theWidth, theJustification, drawString, theOffset, theLength);
		}

		internal static void GetWidowRange(string theText, ref int theStartPos, ref int theEndPos)
		{
			int num = theText.length() - 1;
			int num2 = 0;
			theStartPos = (theEndPos = -1);
			while (num >= num2 && theText[num] != ' ')
			{
				num--;
			}
			if (num < num2)
			{
				return;
			}
			theEndPos = num - num2;
			while (num >= num2 && theText[num] == ' ')
			{
				num--;
			}
			theStartPos = num - num2 + 1;
		}

		internal static int TodDrawStringWrappedHelper(Graphics g, string theText, TRect theRect, Font theFont, SexyColor theColor, DrawStringJustification theJustification, bool drawString, bool widowSuppression)
		{
			return TodStringFile.TodDrawStringWrappedHelper(g, theText, theRect, theFont, theColor, theJustification, drawString, widowSuppression, '\n');
		}

		internal static int TodDrawStringWrappedHelper(Graphics g, string theText, TRect theRect, Font theFont, SexyColor theColor, DrawStringJustification theJustification, bool drawString, bool widowSuppression, char lineBreakChar)
		{
			int theMaxChars = theText.length();
			TodStringFile.aCurrentFormat.Reset();
			TodStringFile.aCurrentFormat.mFormatName = string.Empty;
			TodStringFile.aCurrentFormat.mNewFont = theFont;
			TodStringFile.aCurrentFormat.mNewColor.CopyFrom(theColor);
			TodStringFile.aCurrentFormat.mLineSpacingOffset = 0;
			TodStringFile.aCurrentFormat.mFormatFlags = 0U;
			Font newFont = TodStringFile.aCurrentFormat.mNewFont;
			int num = 0;
			int num2 = newFont.GetLineSpacing() + TodStringFile.aCurrentFormat.mLineSpacingOffset;
			string empty = string.Empty;
			int i = 0;
			int num3 = 0;
			int num4 = 0;
			char c = ' ';
			char thePrevChar = ' ';
			int num5 = -1;
			int num6 = 0;
			int num7 = 0;
			int num8 = -1;
			int num9 = -1;
			if (widowSuppression)
			{
				TodStringFile.GetWidowRange(theText, ref num8, ref num9);
			}
			while (i < theText.length())
			{
				c = theText[i];
				if (c == '{')
				{
					int num10 = i;
					int num11 = theText.IndexOf('}', num10 + 1);
					string theFormat = theText.Substring(num10 + 1, num11 - (num10 + 1));
					if (num11 != -1)
					{
						i += num11 - num10 + 1;
						int num12 = newFont.GetAscent() - newFont.GetAscentPadding();
						SexyColor newColor = TodStringFile.aCurrentFormat.mNewColor;
						TodStringFile.TodWriteStringSetFormat(theFormat, ref TodStringFile.aCurrentFormat);
						TodStringFile.aCurrentFormat.mNewColor = newColor;
						if (TodStringFile.aCurrentFormat.mNewColor.mAlpha == 0)
						{
							int num13 = 0;
							num13++;
						}
						int num14 = newFont.GetAscent() - newFont.GetAscentPadding();
						num2 = newFont.GetLineSpacing() + TodStringFile.aCurrentFormat.mLineSpacingOffset;
						num += num14 - num12;
						continue;
					}
				}
				else if (TodStringFile.CharIsSpaceInFormat(c, TodStringFile.aCurrentFormat))
				{
					if (!widowSuppression || i < num8 || i > num9)
					{
						num5 = i;
					}
					c = ' ';
				}
				else if (c == '\n' || c == lineBreakChar)
				{
					num4 = theRect.mWidth + 1;
					num5 = i;
					i++;
				}
				num4 += newFont.CharWidthKern(c, thePrevChar);
				thePrevChar = c;
				if (num4 > theRect.mWidth)
				{
					int num15;
					if (num5 != -1)
					{
						TodStringFile.TodWriteWordWrappedHelper(g, theText, theRect.mX + num7, theRect.mY + num, TodStringFile.aCurrentFormat, theRect.mWidth, theJustification, drawString, num3, num5 - num3, theMaxChars);
						num15 = num4 + num7;
						if (num15 < 0)
						{
							break;
						}
						i = num5 + 1;
						if (c != '\n')
						{
							while (i < theText.length() && TodStringFile.CharIsSpaceInFormat(theText[i], TodStringFile.aCurrentFormat))
							{
								i++;
							}
						}
					}
					else
					{
						if (i < num3 + 1)
						{
							i++;
						}
						num15 = TodStringFile.TodWriteWordWrappedHelper(g, theText, theRect.mX + num7, theRect.mY + num, TodStringFile.aCurrentFormat, theRect.mWidth, theJustification, drawString, num3, i - num3, theMaxChars);
						if (num15 < 0)
						{
							break;
						}
					}
					if (num15 > num6)
					{
						num6 = num15;
					}
					num3 = i;
					num5 = -1;
					num4 = 0;
					num7 = 0;
					num += num2;
				}
				else
				{
					i++;
				}
			}
			if (num3 < theText.length())
			{
				int num16 = TodStringFile.TodWriteWordWrappedHelper(g, theText, theRect.mX + num7, theRect.mY + num, TodStringFile.aCurrentFormat, theRect.mWidth, theJustification, drawString, num3, theText.length() - num3, theMaxChars);
				if (num16 >= 0)
				{
					if (num16 > num6)
					{
					}
					num += num2;
				}
			}
			else if (c == '\n')
			{
				num += num2;
			}
			return num - newFont.GetDescent();
		}

		private static int strspn(string InputString, char[] Mask, int start)
		{
			int num = 0;
			bool flag = false;
			for (int i = start; i < InputString.Length; i++)
			{
				if (!Enumerable.Contains<char>(Mask, InputString[i]))
				{
					flag = true;
					break;
				}
				num++;
			}
			if (!flag)
			{
				return -1;
			}
			return num;
		}

		public static TodStringListFormat[] gTodDefaultStringFormats = new TodStringListFormat[]
		{
			new TodStringListFormat("NORMAL", null, new SexyColor(40, 50, 90), 0, 0U),
			new TodStringListFormat("KEYWORD", null, new SexyColor(143, 67, 27), 0, 0U)
		};

		public static TodStringListFormat[] gTodStringFormats = TodStringFile.gTodDefaultStringFormats;

		public static int gTodStringFormatCount = TodStringFile.gTodDefaultStringFormats.Length;

		internal static Dictionary<string, string> gStringProperties = new Dictionary<string, string>();

		public static bool StringsLoaded = false;

		private static Dictionary<string, string> todStringTranslateCache = new Dictionary<string, string>(100);

		private static StringBuilder TodWriteStringBuilder = new StringBuilder();

		private static TodStringListFormat aCurrentFormat = new TodStringListFormat();
	}
}
