using System;

namespace Sexy.TodLib
{
	public/*internal*/ class FoleyTypeData
	{
		public FoleyTypeData()
		{
			this.mLastVariationPlayed = -1;
			for (int i = 0; i < 8; i++)
			{
				this.mFoleyInstances[i] = new FoleyInstance();
			}
		}

		public FoleyInstance[] mFoleyInstances = new FoleyInstance[8];

		public int mLastVariationPlayed;
	}
}
