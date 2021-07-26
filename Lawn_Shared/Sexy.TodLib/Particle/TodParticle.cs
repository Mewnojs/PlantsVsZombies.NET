using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	internal class TodParticle
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
			this.Reset();
			TodParticle.unusedObjects.Push(this);
		}

		private void Reset()
		{
			this.mParticleEmitter = null;
			this.mParticleDuration = 0;
			this.mParticleAge = 0;
			this.mParticleTimeValue = 0f;
			this.mParticleLastTimeValue = 0f;
			this.mAnimationTimeValue = 0f;
			this.mVelocity = default(SexyVector2);
			this.mPosition = default(SexyVector2);
			this.mImageFrame = 0;
			this.mSpinPosition = 0f;
			this.mSpinVelocity = 0f;
			this.mCrossFadeParticleID = null;
			this.mCrossFadeDuration = 0;
			for (int i = 0; i < this.mParticleInterp.Length; i++)
			{
				this.mParticleInterp[i] = 0f;
			}
			for (int j = 0; j < 5; j++)
			{
				for (int k = 0; k < 2; k++)
				{
					this.mParticleFieldInterp[j, k] = 0f;
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
