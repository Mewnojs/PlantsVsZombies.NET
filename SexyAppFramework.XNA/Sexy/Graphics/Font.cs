using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
    public interface Font
    {
        public int FontId { get; }

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

        //public void AddCharacterOffset(char c, Vector2 offset);

        static Font()
        {
        }

        public Font.CachedStringInfo GetWordWrappedSubStrings(string theLine, TRect theRect);

        public int LayerCount { get; }

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

        //public SpriteFont GetFontLayer(int i);

        public void AddLayer(dynamic font)
        {
            AddLayer(font, Vector2.Zero);
        }

        public void AddLayer(dynamic font, Vector2 offset);

        public void EnableLayer(int layer, bool enabled);

        public void Dispose();

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

        public int GetLineSpacing();

        public int StringWidth(string theString);

        //private int ComputeStringWidthForCache(string theString)

        public int StringWidth(StringBuilder theString);

        public int StringHeight(string theString);

        public Vector2 MeasureString(string theString)
        {
            return new Vector2(StringWidth(theString), StringHeight(theString));
        }

        public int CharWidth(char theChar);

        public int CharWidthKern(char theChar, char thePrevChar)
        {
            return CharWidth(theChar);
        }

        public void DrawStringLayer(Graphics g, int theX, int theY, string theString, Color theColor, int layer);

        public void DrawString(Graphics g, int theX, int theY, string theString, Color theColor);

        public void DrawString(Graphics g, int theX, int theY, StringBuilder theString, Color theColor);

        public/*internal*/ Font Duplicate()
        {
            return this;
        }

        public int mAscent { get; set; }

        public int mAscentPadding { get; set; }

        public int characterOffsetMagic { get; set; }

        protected int height { get; set; }

        public float mScaleX { get; set; }

        public float mScaleY { get; set; }

        protected static int nextId;

        //private List<bool> enabledLayers = new List<bool>();

        public int mLineSpacingOffset { get; }

        public string SpaceChar { get; set; }

        //private StringBuilder drawStringBuilder = new StringBuilder();

        public bool StringWidthCachingEnabled { get; set; }

        //private static Dictionary<TRect, Dictionary<string, IFont.CachedStringInfo>> allCachedStringWidths = new Dictionary<TRect, Dictionary<string, IFont.CachedStringInfo>>(20);

        //private static Stack<Dictionary<char, int>> charDictionaries = new Stack<Dictionary<char, int>>(20);

        //private static Stack<Dictionary<string, int>> stringDictionaries = new Stack<Dictionary<string, int>>(20);

        //private Dictionary<float, Dictionary<string, int>> cachedStringWidths = new Dictionary<float, Dictionary<string, int>>(10);

        //private Dictionary<float, Dictionary<char, int>> cachedStringBuilderWidths = new Dictionary<float, Dictionary<char, int>>(5);

        public class CachedStringInfo
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
