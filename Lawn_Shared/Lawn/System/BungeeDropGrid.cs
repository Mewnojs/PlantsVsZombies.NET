using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class BungeeDropGrid
	{
		public BungeeDropGrid()
		{
			for (int i = 0; i < this.mGridArray.Length; i++)
			{
				this.mGridArray[i] = TodWeightedGridArray.GetNewTodWeightedGridArray();
			}
		}

		public TodWeightedGridArray[] mGridArray = new TodWeightedGridArray[Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY];

		public int mGridArrayCount;
	}
}
