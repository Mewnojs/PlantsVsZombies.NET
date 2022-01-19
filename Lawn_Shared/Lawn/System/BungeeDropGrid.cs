using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class BungeeDropGrid
    {
        public BungeeDropGrid()
        {
            for (int i = 0; i < mGridArray.Length; i++)
            {
                mGridArray[i] = TodWeightedGridArray.GetNewTodWeightedGridArray();
            }
        }

        public TodWeightedGridArray[] mGridArray = new TodWeightedGridArray[Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY];

        public int mGridArrayCount;
    }
}
