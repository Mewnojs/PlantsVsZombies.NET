using System;

namespace Sexy.TodLib
{
	public/*internal*/ class FoleyParams
	{
		public FoleyParams(FoleyType aFoleyType, float aPitchRange, int[] aIDs, uint aFoleyFlags)
		{
			this.mFoleyType = aFoleyType;
			this.mPitchRange = aPitchRange;
			this.mSfxID = aIDs;
			this.mFoleyFlags = aFoleyFlags;
		}

		public FoleyType mFoleyType;

		public float mPitchRange;

		public int[] mSfxID = new int[10];

		public uint mFoleyFlags;
	}
}
