using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	public/*internal*/ class TodParticle
	{
		public static void PreallocateMemory()
		{
			for (int i = 0; i < 1500; i++)
			{
				new TodParticle().PrepareForReuse();
			}
		}

		public static TodParticle GetNewTodParticle()
		{
			TodParticle result;
			if (TodParticle.unusedObjects.Count > 0)
			{
				result = TodParticle.unusedObjects.Pop();
			}
			else
			{
				result = new TodParticle();
			}
			return result;
		}

		private TodParticle()
		{
		}

		public void PrepareForReuse()
		{
			Reset();
			TodParticle.unusedObjects.Push(this);
		}

		private void Reset()
		{
			mParticleEmitter = null;
			mParticleDuration = 0;
			mParticleAge = 0;
			mParticleTimeValue = 0f;
			mParticleLastTimeValue = 0f;
			mAnimationTimeValue = 0f;
			mVelocity = default(SexyVector2);
			mPosition = default(SexyVector2);
			mImageFrame = 0;
			mSpinPosition = 0f;
			mSpinVelocity = 0f;
			mCrossFadeParticleID = null;
			mCrossFadeDuration = 0;
			for (int i = 0; i < mParticleInterp.Length; i++)
			{
				mParticleInterp[i] = 0f;
			}
			for (int j = 0; j < 5; j++)
			{
				for (int k = 0; k < 2; k++)
				{
					mParticleFieldInterp[j, k] = 0f;
				}
			}
		}

		public TodParticleEmitter mParticleEmitter;

		public int mParticleDuration;

		public int mParticleAge;

		public float mParticleTimeValue;

		public float mParticleLastTimeValue;

		public float mAnimationTimeValue;

		public SexyVector2 mVelocity = default(SexyVector2);

		public SexyVector2 mPosition = default(SexyVector2);

		public int mImageFrame;

		public float mSpinPosition;

		public float mSpinVelocity;

		public TodParticle mCrossFadeParticleID;

		public int mCrossFadeDuration;

		public float[] mParticleInterp = new float[16];

		public float[,] mParticleFieldInterp = new float[5, 2];

		private static Stack<TodParticle> unusedObjects = new Stack<TodParticle>(1000);
	}
}
