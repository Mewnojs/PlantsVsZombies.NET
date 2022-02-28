using System;
using Microsoft.Xna.Framework;

namespace Sexy.GraphicsLib
{
	public class PIParticleDef
	{
		public PIParticleDef()
		{
			this.mValues = new PIValue[28];
			this.mRefPointOfs = default(Vector2);
			for (int i = 0; i < 28; i++)
			{
				this.mValues[i] = new PIValue();
			}
		}

		public PIEmitter mParent;

		public string mName;

		public int mTextureIdx;

		public PIValue[] mValues;

		public Vector2 mRefPointOfs;

		public bool mLockAspect;

		public bool mIntense;

		public bool mSingleParticle;

		public bool mPreserveColor;

		public bool mAttachToEmitter;

		public int mAnimSpeed;

		public bool mAnimStartOnRandomFrame;

		public float mAttachVal;

		public bool mFlipHorz;

		public bool mFlipVert;

		public int mRepeatColor;

		public int mRepeatAlpha;

		public bool mRandomGradientColor;

		public bool mUseNextColorKey;

		public bool mGetColorFromLayer;

		public bool mUpdateColorFromLayer;

		public bool mGetTransparencyFromLayer;

		public bool mUpdateTransparencyFromLayer;

		public int mNumberOfEachColor;

		public bool mLinkTransparencyToColor;

		public bool mUseKeyColorsOnly;

		public bool mUseEmitterAngleAndRange;

		public bool mAngleAlignToMotion;

		public bool mAngleKeepAlignedToMotion;

		public bool mAngleRandomAlign;

		public int mAngleAlignOffset;

		public int mAngleValue;

		public int mAngleRange;

		public int mAngleOffset;

		public PIInterpolator mColor = new PIInterpolator();

		public PIInterpolator mAlpha = new PIInterpolator();

		public enum PIParticleDefValue
		{
			VALUE_LIFE,
			VALUE_NUMBER,
			VALUE_SIZE_X,
			VALUE_VELOCITY,
			VALUE_WEIGHT,
			VALUE_SPIN,
			VALUE_MOTION_RAND,
			VALUE_BOUNCE,
			VALUE_LIFE_VARIATION,
			VALUE_NUMBER_VARIATION,
			VALUE_SIZE_X_VARIATION,
			VALUE_VELOCITY_VARIATION,
			VALUE_WEIGHT_VARIATION,
			VALUE_SPIN_VARIATION,
			VALUE_MOTION_RAND_VARIATION,
			VALUE_BOUNCE_VARIATION,
			VALUE_SIZE_X_OVER_LIFE,
			VALUE_VELOCITY_OVER_LIFE,
			VALUE_WEIGHT_OVER_LIFE,
			VALUE_SPIN_OVER_LIFE,
			VALUE_MOTION_RAND_OVER_LIFE,
			VALUE_BOUNCE_OVER_LIFE,
			VALUE_VISIBILITY,
			VALUE_EMISSION_ANGLE,
			VALUE_EMISSION_RANGE,
			VALUE_SIZE_Y,
			VALUE_SIZE_Y_VARIATION,
			VALUE_SIZE_Y_OVER_LIFE,
			NUM_VALUES
		}
	}
}
