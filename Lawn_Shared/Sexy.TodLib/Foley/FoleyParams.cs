using System;

namespace Sexy.TodLib
{
	public/*internal*/ class FoleyParams
	{
		public FoleyParams(FoleyType aFoleyType, float aPitchRange, int[] aIDs, uint aFoleyFlags)
		{
			mFoleyType = aFoleyType;
			mPitchRange = aPitchRange;
			mSfxID = aIDs;
			mFoleyFlags = aFoleyFlags;
		}

		public FoleyType mFoleyType;

		public float mPitchRange;

		public int[] mSfxID = new int[10];

		public uint mFoleyFlags;
	}
}
