using System;

namespace Lawn
{
	public/*internal*/ class SpecialGridPlacement
	{
		public SpecialGridPlacement(int aPixelX, int aPixelY, int aGridX, int aGridY)
		{
			this.mPixelX = aPixelX;
			this.mPixelY = aPixelY;
			this.mGridX = aGridX;
			this.mGridY = aGridY;
		}

		public int mPixelX;

		public int mPixelY;

		public int mGridX;

		public int mGridY;
	}
}
