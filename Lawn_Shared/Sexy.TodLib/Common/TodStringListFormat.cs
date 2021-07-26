using System;

namespace Sexy.TodLib
{
	internal class TodStringListFormat
	{
		public TodStringListFormat()
		{
			this.Reset();
		}

		public void Reset()
		{
			this.mFormatName = null;
			this.mNewFont = null;
			this.mNewColor = default(SexyColor);
			this.mBaseColor = default(SexyColor);
			this.mLineSpacingOffset = 0;
			this.mFormatFlags = 0U;
		}

		public TodStringListFormat(string aFormatName, Font aNewFont, SexyColor aNewColor, int aLineSpacingOffset, uint aFormatFlags)
		{
			this.mFormatName = aFormatName;
			this.mNewFont = aNewFont;
			this.mNewColor = aNewColor;
			this.mBaseColor = aNewColor;
			this.mLineSpacingOffset = aLineSpacingOffset;
			this.mFormatFlags = aFormatFlags;
		}

		public SexyColor mBaseColor = default(SexyColor);

		public string mFormatName;

		public Font mNewFont;

		public SexyColor mNewColor = default(SexyColor);

		public int mLineSpacingOffset;

		public uint mFormatFlags;
	}
}
