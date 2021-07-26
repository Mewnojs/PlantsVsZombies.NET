using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	internal class Font
	{
		public int FontId { get; private set; }

		public Vector2 scale
		{
			get
			{
				return new Vector2(this.mScaleX * FrameworkConstants.Font_Scale, this.mScaleY * FrameworkConstants.Font_Scale);
			}
		}

		public int mHeight
		{
			get
			{
				return (int)((float)this.height * this.mScaleY * FrameworkConstants.Font_Scale);
			}
			set
			{
				this.height = value;
			}
		}

		public void AddCharacterOffset(char c, Vector2 offset)
		{
			this.mCharOffsets.Add(c, offset);
		}

		static Font()
		{
			for (int i = 0; i < 40; i++)
			{
				Font.charDictionaries.Push(new Dictionary<char, int>(100));
			}
			for (int j = 0; j < 40; j++)
			{
				Font.stringDictionaries.Push(new Dictionary<string, int>(100));
			}
		}

		private static Dictionary<char, int> GetNewCharDictionary()
		{
			if (Font.charDictionaries.Count > 0)
			{
				return Font.charDictionaries.Pop();
			}
			return new Dictionary<char, int>(100);
		}

		private static Dictionary<string, int> GetNewStringDictionary()
		{
			if (Font.stringDictionaries.Count > 0)
			{
				return Font.stringDictionaries.Pop();
			}
			return new Dictionary<string, int>(100);
		}

		public Font.CachedStringInfo GetWordWrappedSubStrings(string theLine, TRect theRect)
		{
			Dictionary<string, Font.CachedStringInfo> dictionary;
			if (!Font.allCachedStringWidths.TryGetValue(theRect, ref dictionary))
			{
				dictionary = new Dictionary<string, Font.CachedStringInfo>(10);
				Font.allCachedStringWidths.Add(theRect, dictionary);
			}
			Font.CachedStringInfo cachedStringInfo;
			if (!dictionary.TryGetValue(theLine, ref cachedStringInfo))
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				List<StringBuilder> list = new List<StringBuilder>();
				list.Add(new StringBuilder());
				for (int i = 0; i < theLine.Length; i++)
				{
					if (char.IsWhiteSpace(theLine[i]))
					{
						int num5 = this.StringWidth(theLine.Substring(num2, i - num2));
						if (num5 < theRect.mWidth)
						{
							list[list.Count - 1].Append(theLine, num, i - num);
							num = (num3 = i);
							num4 = num5;
						}
						else
						{
							list.Add(new StringBuilder());
							list[list.Count - 1].Append(theLine, num, i - num);
							num2 = num;
							num4 = this.StringWidth(theLine.Substring(num, i - num));
							num = (num3 = i);
						}
					}
				}
				if (num4 + this.StringWidth(theLine.Substring(num3, theLine.Length - num3)) > theRect.mWidth && list[list.Count - 1].Length > 0)
				{
					list.Add(new StringBuilder());
				}
				list[list.Count - 1].Append(theLine, num3, theLine.Length - num3);
				string[] array = new string[list.Count];
				for (int j = 0; j < list.Count; j++)
				{
					array[j] = list[j].ToString().Trim();
				}
				cachedStringInfo = new Font.CachedStringInfo(array, this);
				dictionary.Add(theLine, cachedStringInfo);
			}
			return cachedStringInfo;
		}

		public int LayerCount
		{
			get
			{
				return this.mFonts.Count;
			}
		}

		public Font()
		{
			this.FontId = Font.nextId++;
			this.drawStringBuilder.Append("A");
			this.mAscent = 0;
			this.mHeight = 0;
			this.mAscentPadding = 0;
			this.mLineSpacingOffset = 0;
			this.mFonts = new List<SpriteFont>();
			this.mOffsets = new List<Vector2>();
		}

		public Font(Font theFont)
		{
			this.FontId = Font.nextId++;
			this.drawStringBuilder.Append("A");
			this.mAscent = theFont.mAscent;
			this.mHeight = theFont.mHeight;
			this.mAscentPadding = theFont.mAscentPadding;
			this.mLineSpacingOffset = theFont.mLineSpacingOffset;
			this.mFonts = new List<SpriteFont>();
			for (int i = 0; i < theFont.mFonts.Count; i++)
			{
				this.mFonts.Add(theFont.mFonts[i]);
			}
			this.mOffsets = new List<Vector2>();
			for (int j = 0; j < theFont.mOffsets.Count; j++)
			{
				this.mOffsets.Add(theFont.mOffsets[j]);
			}
		}

		public static int FontComparer(Font a, Font b)
		{
			if (a == null && b == null)
			{
				return 0;
			}
			if (a == null)
			{
				return 1;
			}
			if (b == null)
			{
				return -1;
			}
			return a.FontId.CompareTo(b.FontId);
		}

		public SpriteFont GetFontLayer(int i)
		{
			return this.mFonts[i];
		}

		public void AddLayer(SpriteFont font)
		{
			this.AddLayer(font, Vector2.Zero);
		}

		public void AddLayer(SpriteFont font, Vector2 offset)
		{
			this.mFonts.Add(font);
			this.mOffsets.Add(offset);
			this.enabledLayers.Add(true);
		}

		public void EnableLayer(int layer, bool enabled)
		{
			this.enabledLayers[layer] = enabled;
		}

		public void Dispose()
		{
		}

		public int GetAscent()
		{
			return (int)((float)this.mAscent * this.scale.Y);
		}

		public int GetAscentPadding()
		{
			return this.mAscentPadding;
		}

		public int GetDescent()
		{
			int num = this.GetHeight();
			return num + this.mAscent;
		}

		public int GetHeight()
		{
			return this.StringHeight("1");
		}

		public int GetLineSpacingOffset()
		{
			return this.mLineSpacingOffset;
		}

		public int GetLineSpacing()
		{
			int num = 0;
			for (int i = 0; i < this.mFonts.Count; i++)
			{
				if (this.mFonts[i].LineSpacing > num)
				{
					num = this.mFonts[i].LineSpacing;
				}
			}
			return (int)((float)num * this.scale.Y);
		}

		public int StringWidth(string theString)
		{
			int num;
			if (this.StringWidthCachingEnabled)
			{
				Dictionary<string, int> dictionary = null;
				if (this.StringWidthCachingEnabled && !this.cachedStringWidths.TryGetValue(this.mScaleX, ref dictionary))
				{
					dictionary = Font.GetNewStringDictionary();
					this.cachedStringWidths.Add(this.mScaleX, dictionary);
				}
				if (!dictionary.TryGetValue(theString, ref num))
				{
					num = this.ComputeStringWidthForCache(theString);
					dictionary.Add(theString, num);
				}
			}
			else
			{
				num = this.ComputeStringWidthForCache(theString);
			}
			return num;
		}

		private int ComputeStringWidthForCache(string theString)
		{
			float num = 0f;
			for (int i = 0; i < this.mFonts.Count; i++)
			{
				float num2 = this.mOffsets[i].X * this.mScaleX * FrameworkConstants.Font_Scale;
				for (int j = 0; j < theString.Length; j++)
				{
					if (char.IsWhiteSpace(theString[j]))
					{
						num2 += (float)this.StringWidth(this.SpaceChar);
					}
					else
					{
						this.drawStringBuilder[0] = theString[j];
						num2 += (float)(this.StringWidth(this.drawStringBuilder) - this.characterOffsetMagic);
					}
				}
				if (num2 > num)
				{
					num = num2;
				}
			}
			return (int)num;
		}

		public int StringWidth(StringBuilder theString)
		{
			int result = 0;
			if (theString.Length == 0)
			{
				return result;
			}
			return this.ComputeStringWidthForCache(theString);
		}

		private int ComputeStringWidthForCache(StringBuilder theString)
		{
			int num = int.MaxValue;
			for (int i = 0; i < this.mFonts.Count; i++)
			{
				int num2 = 0;
				if (theString.Length <= 1)
				{
					if (char.IsWhiteSpace(theString[0]))
					{
						num2 += this.StringWidth(this.SpaceChar);
					}
					else if (this.mCharOffsets.ContainsKey(theString[0]))
					{
						num2 += (int)(this.mFonts[i].MeasureString(theString).X * this.mScaleX * FrameworkConstants.Font_Scale);
					}
				}
				else
				{
					for (int j = 0; j < theString.Length; j++)
					{
						this.drawStringBuilder[0] = theString[j];
						num2 += this.StringWidth(this.drawStringBuilder);
					}
				}
				if (num2 < num)
				{
					num = num2;
				}
			}
			return num - this.characterOffsetMagic * theString.Length;
		}

		public int StringHeight(string theString)
		{
			int num = 0;
			for (int i = 0; i < this.mFonts.Count; i++)
			{
				for (int j = 0; j < theString.Length; j++)
				{
					if (char.IsWhiteSpace(theString[j]))
					{
						this.drawStringBuilder[0] = this.SpaceChar[0];
					}
					else
					{
						this.drawStringBuilder[0] = theString[j];
					}
					int num2 = 0;
					if (this.mCharOffsets.ContainsKey(this.drawStringBuilder[0]))
					{
						num2 = (int)this.mFonts[i].MeasureString(this.drawStringBuilder).Y;
					}
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return (int)((float)num * this.scale.Y);
		}

		public Vector2 MeasureString(string theString)
		{
			return new Vector2((float)this.StringWidth(theString), (float)this.StringHeight(theString));
		}

		public int CharWidth(char theChar)
		{
			this.drawStringBuilder[0] = theChar;
			return this.StringWidth(this.drawStringBuilder);
		}

		public int CharWidthKern(char theChar, char thePrevChar)
		{
			return this.CharWidth(theChar);
		}

		public void DrawStringLayer(Graphics g, int theX, int theY, string theString, Color theColor, int layer)
		{
			if (!this.enabledLayers[layer])
			{
				return;
			}
			Vector2 value = new Vector2((float)theX, (float)theY);
			Vector2 value2 = value + this.mOffsets[layer] * this.mScaleX * FrameworkConstants.Font_Scale;
			if (!string.IsNullOrEmpty(theString) && this.mCharOffsets.ContainsKey(theString[0]))
			{
				value2.X -= this.mCharOffsets[theString[0]].X * this.mScaleX * FrameworkConstants.Font_Scale;
			}
			for (int i = 0; i < theString.Length; i++)
			{
				if (char.IsWhiteSpace(theString[i]))
				{
					value2.X += (float)this.StringWidth(this.SpaceChar);
				}
				else
				{
					this.drawStringBuilder[0] = theString[i];
					if (this.mCharOffsets.ContainsKey(theString[i]))
					{
						Vector2 position = value2 + this.mCharOffsets[theString[i]] * this.mScaleX * FrameworkConstants.Font_Scale;
						Graphics.spriteBatch.DrawString(this.mFonts[layer], this.drawStringBuilder, position, theColor, 0f, Vector2.Zero, this.scale, SpriteEffects.None, 1f - (float)layer / (float)this.mFonts.Count);
						value2.X += (float)(this.StringWidth(this.drawStringBuilder) - this.characterOffsetMagic);
					}
				}
			}
		}

		public void DrawString(Graphics g, int theX, int theY, string theString, Color theColor)
		{
			if (string.IsNullOrEmpty(theString))
			{
				return;
			}
			g.EndDrawImageTransformed();
			for (int i = 0; i < this.mFonts.Count; i++)
			{
				this.DrawStringLayer(g, theX, theY, theString, theColor, i);
			}
		}

		public void DrawString(Graphics g, int theX, int theY, StringBuilder theString, Color theColor)
		{
			if (theString.Length == 0)
			{
				return;
			}
			g.EndDrawImageTransformed();
			Vector2 value = new Vector2((float)theX, (float)theY);
			for (int i = 0; i < this.mFonts.Count; i++)
			{
				if (this.enabledLayers[i])
				{
					Vector2 value2 = value + this.mOffsets[i] * this.mScaleX * FrameworkConstants.Font_Scale;
					for (int j = 0; j < theString.Length; j++)
					{
						if (char.IsWhiteSpace(theString[j]))
						{
							value2.X += (float)this.StringWidth(this.SpaceChar);
						}
						else
						{
							this.drawStringBuilder[0] = theString[j];
							if (this.mCharOffsets.ContainsKey(theString[j]))
							{
								Vector2 position = value2 + this.mCharOffsets[theString[j]] * this.mScaleX * FrameworkConstants.Font_Scale;
								Graphics.spriteBatch.DrawString(this.mFonts[i], this.drawStringBuilder, position, theColor, 0f, Vector2.Zero, this.scale, SpriteEffects.None, 1f - (float)i / (float)this.mFonts.Count);
								value2.X += (float)(this.StringWidth(this.drawStringBuilder) - this.characterOffsetMagic);
							}
						}
					}
				}
			}
		}

		internal Font Duplicate()
		{
			return this;
		}

		public int mAscent;

		public int mAscentPadding;

		public int characterOffsetMagic;

		private int height;

		public float mScaleX = 1f;

		public float mScaleY = 1f;

		private static int nextId;

		private List<bool> enabledLayers = new List<bool>();

		public int mLineSpacingOffset;

		private List<SpriteFont> mFonts;

		private List<Vector2> mOffsets;

		private Dictionary<char, Vector2> mCharOffsets = new Dictionary<char, Vector2>();

		public string SpaceChar = "k";

		private StringBuilder drawStringBuilder = new StringBuilder();

		public bool StringWidthCachingEnabled = true;

		private static Dictionary<TRect, Dictionary<string, Font.CachedStringInfo>> allCachedStringWidths = new Dictionary<TRect, Dictionary<string, Font.CachedStringInfo>>(20);

		private static Stack<Dictionary<char, int>> charDictionaries = new Stack<Dictionary<char, int>>(20);

		private static Stack<Dictionary<string, int>> stringDictionaries = new Stack<Dictionary<string, int>>(20);

		private Dictionary<float, Dictionary<string, int>> cachedStringWidths = new Dictionary<float, Dictionary<string, int>>(10);

		private Dictionary<float, Dictionary<char, int>> cachedStringBuilderWidths = new Dictionary<float, Dictionary<char, int>>(5);

		internal class CachedStringInfo
		{
			public string[] Strings { get; private set; }

			public Vector2[] StringDimensions { get; private set; }

			public CachedStringInfo(string[] substrings, Font fontUsed)
			{
				this.Strings = substrings;
				this.StringDimensions = new Vector2[this.Strings.Length];
				for (int i = 0; i < this.Strings.Length; i++)
				{
					this.StringDimensions[i] = fontUsed.MeasureString(this.Strings[i]);
					Vector2[] stringDimensions = this.StringDimensions;
					int num = i;
					stringDimensions[num].Y = stringDimensions[num].Y * fontUsed.scale.Y;
				}
			}

			public override int GetHashCode()
			{
				return this.Strings.GetHashCode();
			}
		}
	}
}
