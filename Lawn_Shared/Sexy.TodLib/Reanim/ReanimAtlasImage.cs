using System;

namespace Sexy.TodLib
{
	public/*internal*/ class ReanimAtlasImage
	{
		public ReanimAtlasImage()
		{
			this.mX = 0;
			this.mY = 0;
			this.mWidth = 0;
			this.mHeight = 0;
			this.mOriginalImage = null;
		}

		public int mX;

		public int mY;

		public int mWidth;

		public int mHeight;

		public Image mOriginalImage;
	}
}
