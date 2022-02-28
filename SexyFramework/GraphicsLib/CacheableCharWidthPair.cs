using System;

namespace Sexy.GraphicsLib
{
	public struct CacheableCharWidthPair
	{
		public CacheableCharWidthPair(char theChar, int theWidth)
		{
			this.charData = theChar;
			this.width = theWidth;
		}

		public char charData;

		public int width;
	}
}
