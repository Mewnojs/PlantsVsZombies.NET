using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
    public/*internal*/ class Font
    {
        public int FontId { get; private set; }

        public Vector2 scale
        {
            get
            {
                return new Vector2(mScaleX * FrameworkConstants.Font_Scale, mScaleY * FrameworkConstants.Font_Scale);
            }
        }

        public int mHeight
        {
            get
            {
                return (int)(height * mScaleY * FrameworkConstants.Font_Scale);
            }
            set
            {
                height = value;
            }
        }

        public void AddCharacterOffset(char c, Vector2 offset)
        {
            mCharOffsets.Add(c, offset);
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
            if (!Font.allCachedStringWidths.TryGetValue(theRect, out dictionary))
            {
                dictionary = new Dictionary<string, Font.CachedStringInfo>(10);
                Font.allCachedStringWidths.Add(theRect, dictionary);
            }
            Font.CachedStringInfo cachedStringInfo;
            if (!dictionary.TryGetValue(theLine, out cachedStringInfo))
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
                        int num5 = StringWidth(theLine.Substring(num2, i - num2));
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
                            num4 = StringWidth(theLine.Substring(num, i - num));
                            num = (num3 = i);
                        }
                    }
                }
                if (num4 + StringWidth(theLine.Substring(num3, theLine.Length - num3)) > theRect.mWidth && list[list.Count - 1].Length > 0)
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
                return mFonts.Count;
            }
        }

        public Font()
        {
            FontId = Font.nextId++;
            drawStringBuilder.Append("A");
            mAscent = 0;
            mHeight = 0;
            mAscentPadding = 0;
            mLineSpacingOffset = 0;
            mFonts = new List<SpriteFont>();
            mOffsets = new List<Vector2>();
        }

        public Font(Font theFont)
        {
            FontId = Font.nextId++;
            drawStringBuilder.Append("A");
            mAscent = theFont.mAscent;
            mHeight = theFont.mHeight;
            mAscentPadding = theFont.mAscentPadding;
            mLineSpacingOffset = theFont.mLineSpacingOffset;
            mFonts = new List<SpriteFont>();
            for (int i = 0; i < theFont.mFonts.Count; i++)
            {
                mFonts.Add(theFont.mFonts[i]);
            }
            mOffsets = new List<Vector2>();
            for (int j = 0; j < theFont.mOffsets.Count; j++)
            {
                mOffsets.Add(theFont.mOffsets[j]);
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
            return mFonts[i];
        }

        public void AddLayer(SpriteFont font)
        {
            AddLayer(font, Vector2.Zero);
        }

        public void AddLayer(SpriteFont font, Vector2 offset)
        {
            mFonts.Add(font);
            mOffsets.Add(offset);
            enabledLayers.Add(true);
        }

        public void EnableLayer(int layer, bool enabled)
        {
            enabledLayers[layer] = enabled;
        }

        public void Dispose()
        {
        }

        public int GetAscent()
        {
            return (int)(mAscent * scale.Y);
        }

        public int GetAscentPadding()
        {
            return mAscentPadding;
        }

        public int GetDescent()
        {
            int num = GetHeight();
            return num + mAscent;
        }

        public int GetHeight()
        {
            return StringHeight("1");
        }

        public int GetLineSpacingOffset()
        {
            return mLineSpacingOffset;
        }

        public int GetLineSpacing()
        {
            int num = 0;
            for (int i = 0; i < mFonts.Count; i++)
            {
                if (mFonts[i].LineSpacing > num)
                {
                    num = mFonts[i].LineSpacing;
                }
            }
            return (int)(num * scale.Y);
        }

        public int StringWidth(string theString)
        {
            int num;
            if (StringWidthCachingEnabled)
            {
                Dictionary<string, int> dictionary = null;
                if (StringWidthCachingEnabled && !cachedStringWidths.TryGetValue(mScaleX, out dictionary))
                {
                    dictionary = Font.GetNewStringDictionary();
                    cachedStringWidths.Add(mScaleX, dictionary);
                }
                if (!dictionary.TryGetValue(theString, out num))
                {
                    num = ComputeStringWidthForCache(theString);
                    dictionary.Add(theString, num);
                }
            }
            else
            {
                num = ComputeStringWidthForCache(theString);
            }
            return num;
        }

        private int ComputeStringWidthForCache(string theString)
        {
            float num = 0f;
            for (int i = 0; i < mFonts.Count; i++)
            {
                float num2 = mOffsets[i].X * mScaleX * FrameworkConstants.Font_Scale;
                for (int j = 0; j < theString.Length; j++)
                {
                    if (char.IsWhiteSpace(theString[j]))
                    {
                        num2 += StringWidth(SpaceChar);
                    }
                    else
                    {
                        drawStringBuilder[0] = theString[j];
                        num2 += StringWidth(drawStringBuilder) - characterOffsetMagic;
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
            return ComputeStringWidthForCache(theString);
        }

        private int ComputeStringWidthForCache(StringBuilder theString)
        {
            int num = int.MaxValue;
            for (int i = 0; i < mFonts.Count; i++)
            {
                int num2 = 0;
                if (theString.Length <= 1)
                {
                    if (char.IsWhiteSpace(theString[0]))
                    {
                        num2 += StringWidth(SpaceChar);
                    }
                    else if (mCharOffsets.ContainsKey(theString[0]))
                    {
                        num2 += (int)(mFonts[i].MeasureString(theString).X * mScaleX * FrameworkConstants.Font_Scale);
                    }
                }
                else
                {
                    for (int j = 0; j < theString.Length; j++)
                    {
                        drawStringBuilder[0] = theString[j];
                        num2 += StringWidth(drawStringBuilder);
                    }
                }
                if (num2 < num)
                {
                    num = num2;
                }
            }
            return num - characterOffsetMagic * theString.Length;
        }

        public int StringHeight(string theString)
        {
            int num = 0;
            for (int i = 0; i < mFonts.Count; i++)
            {
                for (int j = 0; j < theString.Length; j++)
                {
                    if (char.IsWhiteSpace(theString[j]))
                    {
                        drawStringBuilder[0] = SpaceChar[0];
                    }
                    else
                    {
                        drawStringBuilder[0] = theString[j];
                    }
                    int num2 = 0;
                    if (mCharOffsets.ContainsKey(drawStringBuilder[0]))
                    {
                        num2 = (int)mFonts[i].MeasureString(drawStringBuilder).Y;
                    }
                    if (num2 > num)
                    {
                        num = num2;
                    }
                }
            }
            return (int)(num * scale.Y);
        }

        public Vector2 MeasureString(string theString)
        {
            return new Vector2(StringWidth(theString), StringHeight(theString));
        }

        public int CharWidth(char theChar)
        {
            drawStringBuilder[0] = theChar;
            return StringWidth(drawStringBuilder);
        }

        public int CharWidthKern(char theChar, char thePrevChar)
        {
            return CharWidth(theChar);
        }

        public void DrawStringLayer(Graphics g, int theX, int theY, string theString, Color theColor, int layer)
        {
            if (!enabledLayers[layer])
            {
                return;
            }
            Vector2 value = new Vector2(theX, theY);
            Vector2 value2 = value + mOffsets[layer] * mScaleX * FrameworkConstants.Font_Scale;
            if (!string.IsNullOrEmpty(theString) && mCharOffsets.ContainsKey(theString[0]))
            {
                value2.X -= mCharOffsets[theString[0]].X * mScaleX * FrameworkConstants.Font_Scale;
            }
            for (int i = 0; i < theString.Length; i++)
            {
                if (char.IsWhiteSpace(theString[i]))
                {
                    value2.X += StringWidth(SpaceChar);
                }
                else
                {
                    drawStringBuilder[0] = theString[i];
                    if (mCharOffsets.ContainsKey(theString[i]))
                    {
                        Vector2 position = value2 + mCharOffsets[theString[i]] * mScaleX * FrameworkConstants.Font_Scale;
                        g.HardwareClip();
                        Graphics.spriteBatch.DrawString(mFonts[layer], drawStringBuilder, position, theColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f - layer / (float)mFonts.Count);
                        g.EndHardwareClip();
                        value2.X += StringWidth(drawStringBuilder) - characterOffsetMagic;
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
            for (int i = 0; i < mFonts.Count; i++)
            {
                DrawStringLayer(g, theX, theY, theString, theColor, i);
            }
        }

        public void DrawString(Graphics g, int theX, int theY, StringBuilder theString, Color theColor)
        {
            if (theString.Length == 0)
            {
                return;
            }
            g.EndDrawImageTransformed();
            Vector2 value = new Vector2(theX, theY);
            for (int i = 0; i < mFonts.Count; i++)
            {
                if (enabledLayers[i])
                {
                    Vector2 value2 = value + mOffsets[i] * mScaleX * FrameworkConstants.Font_Scale;
                    for (int j = 0; j < theString.Length; j++)
                    {
                        if (char.IsWhiteSpace(theString[j]))
                        {
                            value2.X += StringWidth(SpaceChar);
                        }
                        else
                        {
                            drawStringBuilder[0] = theString[j];
                            if (mCharOffsets.ContainsKey(theString[j]))
                            {
                                Vector2 position = value2 + mCharOffsets[theString[j]] * mScaleX * FrameworkConstants.Font_Scale;
                                Graphics.spriteBatch.DrawString(mFonts[i], drawStringBuilder, position, theColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f - i / (float)mFonts.Count);
                                value2.X += StringWidth(drawStringBuilder) - characterOffsetMagic;
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

        public/*internal*/ class CachedStringInfo
        {
            public string[] Strings { get; private set; }

            public Vector2[] StringDimensions { get; private set; }

            public CachedStringInfo(string[] substrings, Font fontUsed)
            {
                Strings = substrings;
                StringDimensions = new Vector2[Strings.Length];
                for (int i = 0; i < Strings.Length; i++)
                {
                    StringDimensions[i] = fontUsed.MeasureString(Strings[i]);
                    Vector2[] stringDimensions = StringDimensions;
                    int num = i;
                    stringDimensions[num].Y = stringDimensions[num].Y * fontUsed.scale.Y;
                }
            }

            public override int GetHashCode()
            {
                return Strings.GetHashCode();
            }
        }
    }
}
