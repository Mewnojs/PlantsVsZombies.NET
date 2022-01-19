using System;

namespace Lawn
{
    public/*internal*/ class SpecialGridPlacement
    {
        public SpecialGridPlacement(int aPixelX, int aPixelY, int aGridX, int aGridY)
        {
            mPixelX = aPixelX;
            mPixelY = aPixelY;
            mGridX = aGridX;
            mGridY = aGridY;
        }

        public int mPixelX;

        public int mPixelY;

        public int mGridX;

        public int mGridY;
    }
}
