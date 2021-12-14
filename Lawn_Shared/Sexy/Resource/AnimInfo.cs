using System;
using System.Collections.Generic;

namespace Sexy
{
	public/*internal*/ class AnimInfo
	{
		public AnimInfo()
		{
			mAnimType = AnimType.AnimType_None;
			mFrameDelay = 1;
			mNumCels = 1;
			mTotalAnimTime = 0;
		}

		public AnimInfo(AnimInfo anotherAnim)
		{
			mAnimType = anotherAnim.mAnimType;
			mFrameDelay = anotherAnim.mFrameDelay;
			mNumCels = anotherAnim.mNumCels;
			mTotalAnimTime = anotherAnim.mTotalAnimTime;
			mPerFrameDelay = anotherAnim.mPerFrameDelay;
			mFrameMap = anotherAnim.mFrameMap;
		}

		public void Dispose()
		{
		}

		public void SetPerFrameDelay(int theFrame, int theTime)
		{
			if (mPerFrameDelay.Count <= theFrame)
			{
				mPerFrameDelay.Capacity = theFrame + 1;
			}
			mPerFrameDelay[theFrame] = theTime;
		}

		public void Compute(int theNumCels)
		{
			Compute(theNumCels, 0, 0);
		}

		public void Compute(int theNumCels, int theBeginFrameTime)
		{
			Compute(theNumCels, theBeginFrameTime, 0);
		}

		public void Compute(int theNumCels, int theBeginFrameTime, int theEndFrameTime)
		{
			mNumCels = theNumCels;
			if (mNumCels <= 0)
			{
				mNumCels = 1;
			}
			if (mFrameDelay <= 0)
			{
				mFrameDelay = 1;
			}
			if (mAnimType == AnimType.AnimType_PingPong && mNumCels > 1)
			{
				mFrameMap.Capacity = theNumCels * 2 - 2;
				int num = 0;
				for (int i = 0; i < theNumCels; i++)
				{
					mFrameMap[num++] = i;
				}
				for (int i = theNumCels - 2; i >= 1; i--)
				{
					mFrameMap[num++] = i;
				}
			}
			if (mFrameMap.Count != 0)
			{
				mNumCels = mFrameMap.Count;
			}
			if (theBeginFrameTime > 0)
			{
				SetPerFrameDelay(0, theBeginFrameTime);
			}
			if (theEndFrameTime > 0)
			{
				SetPerFrameDelay(mNumCels - 1, theEndFrameTime);
			}
			if (mPerFrameDelay.Count != 0)
			{
				mTotalAnimTime = 0;
				mPerFrameDelay.Capacity = mNumCels;
				for (int i = 0; i < mNumCels; i++)
				{
					if (mPerFrameDelay[i] <= 0)
					{
						mPerFrameDelay[i] = mFrameDelay;
					}
					mTotalAnimTime += mPerFrameDelay[i];
				}
			}
			else
			{
				mTotalAnimTime = mFrameDelay * mNumCels;
			}
			if (mFrameMap.Count != 0)
			{
				mFrameMap.Capacity = mNumCels;
			}
		}

		public int GetPerFrameCel(int theTime)
		{
			for (int i = 0; i < mNumCels; i++)
			{
				theTime -= mPerFrameDelay[i];
				if (theTime < 0)
				{
					return i;
				}
			}
			return mNumCels - 1;
		}

		public int GetCel(int theTime)
		{
			if (mAnimType == AnimType.AnimType_Once && theTime >= mTotalAnimTime)
			{
				if (mFrameMap.Count != 0)
				{
					return mFrameMap[mFrameMap.Count - 1];
				}
				return mNumCels - 1;
			}
			else
			{
				theTime %= mTotalAnimTime;
				int num;
				if (mPerFrameDelay.Count != 0)
				{
					num = GetPerFrameCel(theTime);
				}
				else
				{
					num = theTime / mFrameDelay % mNumCels;
				}
				if (mFrameMap.Count == 0)
				{
					return num;
				}
				return mFrameMap[num];
			}
		}

		public AnimType mAnimType;

		public int mFrameDelay;

		public int mNumCels;

		public List<int> mPerFrameDelay = new List<int>();

		public List<int> mFrameMap = new List<int>();

		public int mTotalAnimTime;
	}
}
