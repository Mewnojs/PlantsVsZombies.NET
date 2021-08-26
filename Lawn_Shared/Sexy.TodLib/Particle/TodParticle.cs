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

		public void SaveToFile(Sexy.Buffer b)
        {
			TodParticleHolder aHolder = mParticleEmitter.mParticleSystem.mParticleHolder;
			b.WriteLong(aHolder.mEmitters.FindIndex((TodParticleEmitter tpe) => tpe == mParticleEmitter));
			b.WriteLong(mParticleDuration);
			b.WriteLong(mParticleAge);
			b.WriteFloat(mParticleTimeValue);
			b.WriteFloat(mParticleLastTimeValue);
			b.WriteFloat(mAnimationTimeValue);
			b.WriteFloat(mVelocity.x);
			b.WriteFloat(mVelocity.y);
			b.WriteFloat(mPosition.x);
			b.WriteFloat(mPosition.y);
			b.WriteLong(mImageFrame);
			b.WriteFloat(mSpinPosition);
			b.WriteFloat(mSpinVelocity);
			if (mCrossFadeParticleID == null)
            {
				b.WriteLong(-1);
			}
			else
            {
				b.WriteLong(aHolder.mParticles.FindIndex((TodParticle tp) => tp == mCrossFadeParticleID));
			}
			
			b.WriteLong(mCrossFadeDuration);
			foreach (float val in mParticleInterp)
            {
				b.WriteFloat(val);
            }
			foreach (float val in mParticleFieldInterp)
            {
				b.WriteFloat(val);
            }
		}

		private int ltCrossFadeParticleID;

		public void LoadFromFile(Sexy.Buffer b)
        {
			TodParticleHolder aHolder = EffectSystem.gEffectSystem.mParticleHolder;
			mParticleEmitter = aHolder.mEmitters[b.ReadLong()];
			mParticleDuration = b.ReadLong();
			mParticleAge = b.ReadLong();
			mParticleTimeValue = b.ReadFloat();
			mParticleLastTimeValue = b.ReadFloat();
			mAnimationTimeValue = b.ReadFloat();
			mVelocity = new SexyVector2(b.ReadFloat(), b.ReadFloat());
			mPosition = new SexyVector2(b.ReadFloat(), b.ReadFloat());
			mImageFrame = b.ReadLong();
			mSpinPosition = b.ReadFloat();
			mSpinVelocity = b.ReadFloat();
			ltCrossFadeParticleID = b.ReadLong();
			mCrossFadeDuration = b.ReadLong();
			for (int i = 0; i < mParticleInterp.Length; i++)
            {
				mParticleInterp[i] = b.ReadFloat();
			}
			for (int i = 0; i < mParticleFieldInterp.GetLength(0); i++)
            {
				for (int j = 0; j < mParticleFieldInterp.GetLength(1); j++)
                {
					mParticleFieldInterp[i, j] = b.ReadFloat();
				}
            }
		}

		public void LoadingComplete()
        {
			TodParticleHolder aHolder = mParticleEmitter.mParticleSystem.mParticleHolder;
			if(ltCrossFadeParticleID == -1)
            {
				mCrossFadeParticleID = null;
			}
			else
			{
				mCrossFadeParticleID = aHolder.mParticles[ltCrossFadeParticleID];
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
