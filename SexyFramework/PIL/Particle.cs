using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class Particle : MovableObject
	{
		public override void Reset()
		{
		}

		public override void Deserialize(SexyBuffer b, Dictionary<int, Deflector> deflector_ptr_map)
		{
		}

		public Particle()
		{
			this.mColorKeyManager = new ColorKeyManager();
			this.mAlphaKeyManager = new ColorKeyManager();
		}

		public Particle(float spawn_angle, float velocity)
		{
			this.Reset(spawn_angle, velocity);
			this.mColorKeyManager = new ColorKeyManager();
			this.mAlphaKeyManager = new ColorKeyManager();
		}

		public override void Dispose()
		{
			this.mColorKeyManager = null;
			this.mAlphaKeyManager = null;
		}

		private void DoDraw(Graphics g, Transform t, float scale)
		{
			if (g.Is3D())
			{
				g.DrawImageTransformF(this.mImage, t, this.mImage.GetCelRect(this.mImageCel), (this.mX + (float)this.mParentType.mXOff * this.mCurXSize + (float)this.mRefXOff * this.mCurXSize) * scale, (this.mY + (float)this.mParentType.mYOff * this.mCurYSize + (float)this.mRefYOff * this.mCurYSize) * scale);
				return;
			}
			g.DrawImageTransform(this.mImage, t, this.mImage.GetCelRect(this.mImageCel), (this.mX + (float)this.mParentType.mXOff * this.mCurXSize + (float)this.mRefXOff * this.mCurXSize) * scale, (this.mY + (float)this.mParentType.mYOff * this.mCurYSize + (float)this.mRefYOff * this.mCurYSize) * scale);
		}

		private void DoDraw(Graphics g, Transform t)
		{
			this.DoDraw(g, t, 1f);
		}

		public virtual void Reset(float spawn_angle, float velocity)
		{
			base.Reset();
			this.mForceDeletion = false;
			this.mForceFadeoutRate = 0f;
			this.mImage = null;
			this.mCurXSize = 1f;
			this.mCurYSize = 1f;
			this.mLockSizeAspect = true;
			this.mOriginalXSize = 1f;
			this.mOriginalYSize = 1f;
			this.mParentType = null;
			this.mAdditive = (this.mAdditiveWithNormal = false);
			this.mFlipX = (this.mFlipY = false);
			this.mRefXOff = (this.mRefYOff = 0);
			this.mLife = -1;
			this.mAlignAngleToMotion = false;
			this.mMotionAngleOffset = 0f;
			this.mHasBeenVisible = false;
			this.mLastFrameWasVisible = true;
			this.Launch(spawn_angle, velocity);
		}

		public override void Update()
		{
			if (!this.mInitialized)
			{
				this.mOriginalXSize = this.mCurXSize;
				this.mOriginalYSize = this.mCurYSize;
			}
			base.Update();
			if (this.Dead())
			{
				return;
			}
			if (this.mImage != null && this.mImageRate > 0 && this.mUpdateCount % this.mImageRate == 0)
			{
				this.mImageCel = (this.mImageCel + 1) % (this.mImage.mNumCols * this.mImage.mNumRows);
			}
			if (this.mAlignAngleToMotion)
			{
				float num = Common.AngleBetweenPoints(this.mVX, -this.mVY, 0f, 0f);
				this.mAngle = num - this.mMotionAngleOffset;
			}
			this.mColorKeyManager.Update(this.mX, this.mY);
			if (this.mForceFadeoutRate <= 0f)
			{
				this.mAlphaKeyManager.Update(this.mX, this.mY);
			}
			else
			{
				if (this.mForcedAlpha < 0f)
				{
					this.mForcedAlpha = (float)this.mAlphaKeyManager.GetColor().mAlpha;
				}
				this.mForcedAlpha -= this.mForceFadeoutRate;
				if (this.mForcedAlpha <= 0f)
				{
					this.mForceDeletion = true;
				}
			}
			LifetimeSettings interpLifetimeSettings = base.GetInterpLifetimeSettings();
			this.mCurXSize = this.mOriginalXSize * interpLifetimeSettings.mSizeXMult;
			this.mCurYSize = this.mOriginalYSize * interpLifetimeSettings.mSizeYMult;
		}

		public virtual void Draw(Graphics g, float alpha_pct, SexyColor tint_color, float tint_strength, float scale)
		{
			if (this.mImage == null || alpha_pct == 0f || this.mCurXSize == 0f || (!this.mLockSizeAspect && this.mCurYSize == 0f))
			{
				this.mLastFrameWasVisible = false;
				return;
			}
			this.mGlobalTransform.Reset();
			float num = (this.mLockSizeAspect ? this.mCurXSize : this.mCurYSize);
			if (!Common._eq(this.mCurXSize * scale, 1f, 1E-06f) || (!Common._eq(this.mCurYSize * scale, 1f, 1E-06f) && !this.mLockSizeAspect) || this.mFlipX || this.mFlipY)
			{
				this.mGlobalTransform.Scale(this.mFlipX ? (-this.mCurXSize) : this.mCurXSize, num * (this.mFlipY ? (-1f) : 1f));
			}
			if (this.mAngle != 0f)
			{
				this.mGlobalTransform.Translate((float)this.mRefXOff * this.mCurXSize, (float)this.mRefYOff * num);
				this.mGlobalTransform.RotateRad(-this.mAngle);
				this.mGlobalTransform.Translate((float)(-(float)this.mRefXOff) * this.mCurXSize, (float)(-(float)this.mRefYOff) * num);
			}
			SexyColor color = this.mColorKeyManager.GetColor();
			color.mRed -= (int)((float)(color.mRed - tint_color.mRed) * tint_strength);
			color.mGreen -= (int)((float)(color.mGreen - tint_color.mGreen) * tint_strength);
			color.mBlue -= (int)((float)(color.mBlue - tint_color.mBlue) * tint_strength);
			if (this.mForcedAlpha > 0f)
			{
				color.mAlpha = (int)this.mForcedAlpha;
			}
			else
			{
				color.mAlpha = this.mAlphaKeyManager.GetColor().mAlpha;
			}
			if (!Common._eq(alpha_pct, 1f, 1E-06f) || color != SexyColor.White)
			{
				g.SetColorizeImages(true);
				color.mAlpha = (int)((float)color.mAlpha * alpha_pct);
				g.SetColor(color);
			}
			if (color.mAlpha > 0)
			{
				this.mHasBeenVisible = true;
			}
			this.mLastFrameWasVisible = color.mAlpha != 0;
			if (this.mAdditive)
			{
				if (this.mAdditiveWithNormal)
				{
					this.DoDraw(g, this.mGlobalTransform, scale);
				}
				g.SetDrawMode(1);
			}
			this.DoDraw(g, this.mGlobalTransform, scale);
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		public virtual void Draw(Graphics g, float alpha_pct, SexyColor tint_color, float tint_strength)
		{
			this.Draw(g, alpha_pct, tint_color, tint_strength, 1f);
		}

		public override bool Dead()
		{
			return this.mForceDeletion || base.Dead();
		}

		public virtual float GetWidth()
		{
			return (float)this.mImage.GetWidth() * this.mCurXSize;
		}

		public virtual float GetHeight()
		{
			if (this.mLockSizeAspect)
			{
				return (float)this.mImage.GetHeight() * this.mCurXSize;
			}
			return (float)this.mImage.GetHeight() * this.mCurYSize;
		}

		public virtual Rect GetRect()
		{
			float width = this.GetWidth();
			float height = this.GetHeight();
			this.mRect.mX = (int)(this.mX - width / 2f);
			this.mRect.mY = (int)(this.mY - height / 2f);
			this.mRect.mWidth = (int)width;
			this.mRect.mHeight = (int)height;
			return this.mRect;
		}

		public override void Serialize(SexyBuffer b)
		{
			base.Serialize(b);
			b.WriteFloat(this.mOriginalXSize);
			b.WriteFloat(this.mOriginalYSize);
			b.WriteLong((long)this.mImageCel);
			b.WriteLong((long)this.mParentType.mSerialIndex);
			this.mColorKeyManager.Serialize(b);
			this.mAlphaKeyManager.Serialize(b);
			b.WriteLong((long)this.mImageRate);
			b.WriteLong((long)this.mRefXOff);
			b.WriteLong((long)this.mRefYOff);
			b.WriteBoolean(this.mAdditive);
			b.WriteBoolean(this.mAdditiveWithNormal);
			b.WriteBoolean(this.mLockSizeAspect);
			b.WriteBoolean(this.mFlipX);
			b.WriteBoolean(this.mFlipY);
			b.WriteBoolean(this.mAlignAngleToMotion);
			b.WriteFloat(this.mMotionAngleOffset);
			b.WriteFloat(this.mCurXSize);
			b.WriteFloat(this.mCurYSize);
			b.WriteBoolean(this.mHasBeenVisible);
			b.WriteBoolean(this.mLastFrameWasVisible);
		}

		public void Deserialize(SexyBuffer b, Dictionary<int, Deflector> deflector_ptr_map, Dictionary<int, ParticleType> ptype_ptr_map)
		{
			base.Deserialize(b, deflector_ptr_map);
			this.mOriginalXSize = b.ReadFloat();
			this.mOriginalYSize = b.ReadFloat();
			this.mImageCel = (int)b.ReadLong();
			int num = (int)b.ReadLong();
			if (ptype_ptr_map.ContainsKey(num))
			{
				this.mParentType = ptype_ptr_map[num];
			}
			this.mImage = this.mParentType.mImage;
			this.mColorKeyManager.Deserialize(b);
			this.mAlphaKeyManager.Deserialize(b);
			this.mImageRate = (int)b.ReadLong();
			this.mRefXOff = (int)b.ReadLong();
			this.mRefYOff = (int)b.ReadLong();
			this.mAdditive = b.ReadBoolean();
			this.mAdditiveWithNormal = b.ReadBoolean();
			this.mLockSizeAspect = b.ReadBoolean();
			this.mFlipX = b.ReadBoolean();
			this.mFlipY = b.ReadBoolean();
			this.mAlignAngleToMotion = b.ReadBoolean();
			this.mMotionAngleOffset = b.ReadFloat();
			this.mCurXSize = b.ReadFloat();
			this.mCurYSize = b.ReadFloat();
			this.mHasBeenVisible = b.ReadBoolean();
			this.mLastFrameWasVisible = b.ReadBoolean();
		}

		public int mPoolIndex = -1;

		public float mForcedAlpha = -100f;

		public float mOriginalXSize;

		public float mOriginalYSize;

		public string mParentName = "";

		public Image mImage;

		public ParticleType mParentType;

		public ColorKeyManager mColorKeyManager;

		public ColorKeyManager mAlphaKeyManager;

		public int mImageCel;

		public int mImageRate = 1;

		public int mRefXOff;

		public int mRefYOff;

		public Rect mRect = default(Rect);

		public int mWidth;

		public int mHeight;

		public bool mHasBeenVisible;

		public bool mLastFrameWasVisible = true;

		public bool mAdditive;

		public bool mAdditiveWithNormal;

		public bool mLockSizeAspect;

		public bool mFlipX;

		public bool mFlipY;

		public bool mAlignAngleToMotion;

		public bool mForceDeletion;

		public float mForceFadeoutRate;

		public float mMotionAngleOffset;

		public float mCurXSize;

		public float mCurYSize;

		protected Transform mGlobalTransform = new Transform();
	}
}
