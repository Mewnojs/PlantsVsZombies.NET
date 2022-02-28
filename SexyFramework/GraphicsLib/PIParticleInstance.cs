using System;
using Microsoft.Xna.Framework;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class PIParticleInstance : IDisposable
	{
		public PIParticleInstance()
		{
			this.mPrev = null;
			this.mNext = null;
			this.mTransformScaleFactor = 1f;
			this.mImgIdx = 0;
			this.mBkgColor = uint.MaxValue;
			this.mSrcSizeXMult = 1f;
			this.mSrcSizeYMult = 1f;
			this.mParentFreeEmitter = null;
			this.mHasDrawn = false;
			PIParticleInstance.mCount++;
		}

		public void Reset()
		{
			this.mPos.X = 0f;
			this.mPos.Y = 0f;
			this.mPrev = null;
			this.mNext = null;
			this.mParticleDef = null;
			this.mEmitterSrc = null;
			this.mParentFreeEmitter = null;
			this.mTransformScaleFactor = 1f;
			this.mImgIdx = 0;
			this.mBkgColor = uint.MaxValue;
			this.mSrcSizeXMult = 1f;
			this.mSrcSizeYMult = 1f;
			this.mParentFreeEmitter = null;
			this.mHasDrawn = false;
			this.mTicks = 0f;
			this.mLife = 0f;
			this.mLifePct = 0f;
		}

		public void Init()
		{
		}

		public void Release()
		{
		}

		public virtual void Dispose()
		{
			PIParticleInstance.mCount--;
		}

		public PIParticleInstance mPrev;

		public PIParticleInstance mNext;

		public PIParticleDef mParticleDef;

		public PIEmitter mEmitterSrc;

		public int mNum;

		public PIFreeEmitterInstance mParentFreeEmitter;

		public Vector2 mPos = default(Vector2);

		public Vector2 mOrigPos = default(Vector2);

		public Vector2 mEmittedPos = default(Vector2);

		public Vector2 mLastEmitterPos = default(Vector2);

		public Vector2 mVel = default(Vector2);

		public float mImgAngle;

		public float[] mVariationValues = new float[9];

		public float mZoom;

		public float mSrcSizeXMult;

		public float mSrcSizeYMult;

		public float mGradientRand;

		public float mOrigEmitterAng;

		public int mAnimFrameRand;

		public SexyTransform2D mTransform = new SexyTransform2D(false);

		public float mTransformScaleFactor;

		public int mImgIdx;

		public float mThicknessHitVariation;

		public float mTicks;

		public float mLife;

		public float mLifePct;

		public bool mHasDrawn;

		public uint mBkgColor;

		public static int mCount;

		public enum PIParticleVariation
		{
			VARIATION_LIFE,
			VARIATION_SIZE_X,
			VARIATION_SIZE_Y,
			VARIATION_VELOCITY,
			VARIATION_WEIGHT,
			VARIATION_SPIN,
			VARIATION_MOTION_RAND,
			VARIATION_BOUNCE,
			VARIATION_ZOOM,
			NUM_VARIATIONS
		}
	}
}
