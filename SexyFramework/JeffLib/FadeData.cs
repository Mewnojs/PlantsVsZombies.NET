using System;

namespace JeffLib
{
	public class FadeData
	{
		public FadeData()
		{
			this.mFadeState = 0;
			this.mFadeOutTarget = (this.mFadeInTarget = 0);
			this.mFadeOutRate = (this.mFadeInRate = 0);
			this.mVal = 0;
			this.mFadeCount = 0;
			this.mStopWhenDone = true;
		}

		public FadeData(FadeData fd)
		{
			this.mFadeState = fd.mFadeState;
			this.mFadeOutTarget = fd.mFadeOutTarget;
			this.mFadeOutRate = fd.mFadeOutRate;
			this.mVal = fd.mVal;
			this.mFadeCount = fd.mFadeCount;
			this.mStopWhenDone = fd.mStopWhenDone;
		}

		public int mFadeState;

		public int mFadeOutRate;

		public int mFadeInRate;

		public int mFadeOutTarget;

		public int mFadeInTarget;

		public int mVal;

		public int mFadeCount;

		public bool mStopWhenDone;

		public enum FadeType
		{
			Fade_None,
			Fade_Out,
			Fade_In
		}
	}
}
