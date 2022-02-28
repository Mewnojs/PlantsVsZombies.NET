using System;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class CharData
	{
		public CharData()
		{
			this.mKerningFirst = 0;
			this.mKerningCount = 0;
			this.mWidth = 0;
			this.mOrder = 0;
		}

		public ushort mChar;

		public Rect mImageRect = default(Rect);

		public SexyPoint mOffset = new SexyPoint();

		public ushort mKerningFirst;

		public ushort mKerningCount;

		public int mWidth;

		public int mOrder;

		public int mHashEntryIndex;
	}
}
