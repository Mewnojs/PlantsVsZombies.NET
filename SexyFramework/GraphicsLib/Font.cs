using System;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public abstract class Font
	{
		public Font()
		{
			this.mAscent = 0;
			this.mHeight = 0;
			this.mAscentPadding = 0;
			this.mLineSpacingOffset = 0;
		}

		public Font(Font theFont)
		{
			this.mAscent = theFont.mAscent;
			this.mHeight = theFont.mHeight;
			this.mAscentPadding = theFont.mAscentPadding;
			this.mLineSpacingOffset = theFont.mLineSpacingOffset;
		}

		public virtual void Dispose()
		{
		}

		public virtual ImageFont AsImageFont()
		{
			return null;
		}

		public virtual int GetAscent()
		{
			return this.mAscent;
		}

		public virtual int GetAscentPadding()
		{
			return this.mAscentPadding;
		}

		public virtual int GetDescent()
		{
			return this.mHeight - this.mAscent;
		}

		public virtual int GetHeight()
		{
			return this.mHeight;
		}

		public virtual int GetLineSpacingOffset()
		{
			return this.mLineSpacingOffset;
		}

		public virtual int GetLineSpacing()
		{
			return this.mHeight + this.mLineSpacingOffset;
		}

		public virtual int StringWidth(string theString)
		{
			return 0;
		}

		public virtual int CharWidth(char theChar)
		{
			return this.StringWidth(string.Concat(theChar));
		}

		public virtual int CharWidthKern(char theChar, char thePrevChar)
		{
			return this.CharWidth(theChar);
		}

		public virtual void DrawString(Graphics g, int theX, int theY, string theString, SexyColor theColor, Rect theClipRect)
		{
		}

		public abstract Font Duplicate();

		public int mAscent;

		public int mAscentPadding;

		public int mHeight;

		public int mLineSpacingOffset;
	}
}
