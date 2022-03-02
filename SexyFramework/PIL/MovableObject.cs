using System;
using System.Collections.Generic;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class MovableObject : IDisposable
	{
		protected LifetimeSettings GetInterpLifetimeSettings()
		{
			if (this.mLifetimeKeyFrames.size<LifetimeSettingKeyFrame>() == 0)
			{
				return new LifetimeSettings();
			}
			if (this.mKeyFrameIndex == this.mLifetimeKeyFrames.size<LifetimeSettingKeyFrame>() || this.mKeyFrameIndex + 1 == this.mLifetimeKeyFrames.size<LifetimeSettingKeyFrame>())
			{
				return this.mLifetimeKeyFrames[this.mKeyFrameIndex].second;
			}
			LifetimeSettings second = this.mLifetimeKeyFrames[this.mKeyFrameIndex + 1].second;
			LifetimeSettings second2 = this.mLifetimeKeyFrames[this.mKeyFrameIndex].second;
			float num = (float)(this.mUpdateCount - this.mLifetimeKeyFrames[this.mKeyFrameIndex].first) / (float)(this.mLifetimeKeyFrames[this.mKeyFrameIndex + 1].first - this.mLifetimeKeyFrames[this.mKeyFrameIndex].first);
			this.mInterpLifetimeSettings.Reset();
			this.mInterpLifetimeSettings.mMotionRandMult += (second.mMotionRandMult - second2.mMotionRandMult) * num;
			this.mInterpLifetimeSettings.mSizeXMult += (second.mSizeXMult - second2.mSizeXMult) * num;
			this.mInterpLifetimeSettings.mSizeYMult += (second.mSizeYMult - second2.mSizeYMult) * num;
			this.mInterpLifetimeSettings.mSpinMult += (second.mSpinMult - second2.mSpinMult) * num;
			this.mInterpLifetimeSettings.mVelocityMult += (second.mVelocityMult - second2.mVelocityMult) * num;
			this.mInterpLifetimeSettings.mWeightMult += (second.mWeightMult - second2.mWeightMult) * num;
			this.mInterpLifetimeSettings.mZoomMult += (second.mZoomMult - second2.mZoomMult) * num;
			this.mInterpLifetimeSettings.mNumberMult += (second.mNumberMult - second2.mNumberMult) * num;
			return this.mInterpLifetimeSettings;
		}

		public MovableObject()
		{
			this.Reset();
		}

		public MovableObject(MovableObject rhs)
			: this()
		{
			this.CopyFrom(rhs);
		}

		public virtual void Dispose()
		{
			for (int i = 0; i < this.mLifetimeKeyFrames.size<LifetimeSettingKeyFrame>(); i++)
			{
				this.mLifetimeKeyFrames[i].second = null;
			}
		}

		public void CopyFrom(MovableObject rhs)
		{
			if (this == rhs)
			{
				return;
			}
			this.mLife = rhs.mLife;
			this.mVX = rhs.mVX;
			this.mVY = rhs.mVY;
			this.mMotionRand = rhs.mMotionRand;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mWeight = rhs.mWeight;
			this.mAngle = rhs.mAngle;
			this.mSpin = rhs.mSpin;
			this.mBounce = rhs.mBounce;
			this.mUpdateCount = 0;
			this.mKeyFrameIndex = 0;
			this.mOriginalWeight = rhs.mOriginalWeight;
			this.mOriginalBounce = rhs.mOriginalBounce;
			this.mMotionRandAccum = 0f;
			this.mAX = 0f;
			this.mAY = 0f;
			this.mInitialized = rhs.mInitialized;
			for (int i = 0; i < this.mLifetimeKeyFrames.Count; i++)
			{
				this.mLifetimeKeyFrames[i].second = null;
			}
			this.mLifetimeKeyFrames.Clear();
			for (int j = 0; j < rhs.mLifetimeKeyFrames.size<LifetimeSettingKeyFrame>(); j++)
			{
				this.AddLifetimeKeyFrame((this.mLife == 0) ? 0f : ((float)rhs.mLifetimeKeyFrames[j].first / (float)this.mLife), new LifetimeSettings(rhs.mLifetimeKeyFrames[j].second));
			}
		}

		public virtual void Serialize(SexyBuffer b)
		{
			b.WriteLong(this.mKeyFrameIndex);
			b.WriteFloat(this.mOriginalWeight);
			b.WriteFloat(this.mOriginalBounce);
			b.WriteFloat(this.mMotionRandAccum);
			b.WriteFloat(this.mAX);
			b.WriteFloat(this.mAY);
			b.WriteFloat(this.mX);
			b.WriteFloat(this.mY);
			b.WriteBoolean(this.mInitialized);
			b.WriteLong(this.mUpdateCount);
			b.WriteLong(this.mLife);
			b.WriteFloat(this.mVX);
			b.WriteFloat(this.mVY);
			b.WriteFloat(this.mMotionRand);
			b.WriteFloat(this.mWeight);
			b.WriteFloat(this.mAngle);
			b.WriteFloat(this.mSpin);
			b.WriteFloat(this.mBounce);
			this.mCurrentLifetimeSettings.Serialize(b);
			b.WriteLong(this.mLifetimeKeyFrames.Count);
			for (int i = 0; i < this.mLifetimeKeyFrames.Count; i++)
			{
				b.WriteLong(this.mLifetimeKeyFrames[i].first);
				this.mLifetimeKeyFrames[i].second.Serialize(b);
			}
			b.WriteLong(this.mDeflectorCollMap.Count);
			foreach (KeyValuePair<Deflector, DeflectorCollInfo> keyValuePair in this.mDeflectorCollMap)
			{
				b.WriteLong(keyValuePair.Key.mSerialIndex);
				b.WriteLong(keyValuePair.Value.mLastCollFrame);
				b.WriteBoolean(keyValuePair.Value.mIgnoresDeflector);
			}
		}

		public virtual void Deserialize(SexyBuffer b, Dictionary<int, Deflector> deflector_ptr_map)
		{
			this.mKeyFrameIndex = (int)b.ReadLong();
			this.mOriginalWeight = b.ReadFloat();
			this.mOriginalBounce = b.ReadFloat();
			this.mMotionRandAccum = b.ReadFloat();
			this.mAX = b.ReadFloat();
			this.mAY = b.ReadFloat();
			this.mX = b.ReadFloat();
			this.mY = b.ReadFloat();
			this.mInitialized = b.ReadBoolean();
			this.mUpdateCount = (int)b.ReadLong();
			this.mLife = (int)b.ReadLong();
			this.mVX = b.ReadFloat();
			this.mVY = b.ReadFloat();
			this.mMotionRand = b.ReadFloat();
			this.mWeight = b.ReadFloat();
			this.mAngle = b.ReadFloat();
			this.mSpin = b.ReadFloat();
			this.mBounce = b.ReadFloat();
			this.mCurrentLifetimeSettings.Deserialize(b);
			int num = (int)b.ReadLong();
			this.mLifetimeKeyFrames.Clear();
			for (int i = 0; i < num; i++)
			{
				int f = (int)b.ReadLong();
				LifetimeSettings lifetimeSettings = new LifetimeSettings();
				lifetimeSettings.Deserialize(b);
				this.mLifetimeKeyFrames.Add(new LifetimeSettingKeyFrame(f, lifetimeSettings));
			}
			this.mDeflectorCollMap.Clear();
			num = (int)b.ReadLong();
			for (int j = 0; j < num; j++)
			{
				int num2 = (int)b.ReadLong();
				int f2 = (int)b.ReadLong();
				bool b2 = b.ReadBoolean();
				if (deflector_ptr_map.ContainsKey(num2))
				{
					Deflector deflector = deflector_ptr_map[num2];
					this.mDeflectorCollMap.Add(deflector, new DeflectorCollInfo(f2, b2));
				}
			}
		}

		public virtual void Launch(float angle, float velocity)
		{
			this.mVX = (float)Math.Cos((double)angle) * velocity;
			this.mVY = -(float)Math.Sin((double)angle) * velocity;
		}

		public virtual LifetimeSettings AddLifetimeKeyFrame(float pct, LifetimeSettings s, float second_frame_pct, bool make_new)
		{
			LifetimeSettingKeyFrame lifetimeSettingKeyFrame = new LifetimeSettingKeyFrame();
			lifetimeSettingKeyFrame.first = (int)(pct * (float)this.mLife);
			lifetimeSettingKeyFrame.second = s;
			lifetimeSettingKeyFrame.second.mPct = pct;
			this.mLifetimeKeyFrames.Add(lifetimeSettingKeyFrame);
			this.mLifetimeKeyFrames.Sort(new LifeFrameSort());
			if (second_frame_pct >= 0f)
			{
				this.AddLifetimeKeyFrame(second_frame_pct, new LifetimeSettings(s));
				if (make_new)
				{
					return new LifetimeSettings(s);
				}
			}
			return null;
		}

		public virtual LifetimeSettings AddLifetimeKeyFrame(float pct, LifetimeSettings s, float second_frame_pct)
		{
			return this.AddLifetimeKeyFrame(pct, s, second_frame_pct, false);
		}

		public virtual LifetimeSettings AddLifetimeKeyFrame(float pct, LifetimeSettings s)
		{
			return this.AddLifetimeKeyFrame(pct, s, -1f, false);
		}

		public virtual void ClearLifetimeFrames()
		{
			this.mLifetimeKeyFrames.Clear();
			this.AddLifetimeKeyFrame(0f, new LifetimeSettings(this.mCurrentLifetimeSettings));
		}

		public virtual void Reset()
		{
			this.mLife = -1;
			this.mUpdateCount = 0;
			this.mMotionRand = 0f;
			this.mWeight = 0f;
			this.mAngle = 0f;
			this.mSpin = 0f;
			this.mX = 0f;
			this.mY = 0f;
			this.mKeyFrameIndex = 0;
			this.mOriginalWeight = 1f;
			this.mOriginalBounce = 0f;
			this.mInitialized = false;
			this.mMotionRandAccum = 0f;
			this.mAX = 0f;
			this.mAY = 0f;
			this.mVX = 0f;
			this.mVY = 0f;
			for (int i = 0; i < this.mLifetimeKeyFrames.size<LifetimeSettingKeyFrame>(); i++)
			{
				this.mLifetimeKeyFrames[i] = null;
			}
			this.mLifetimeKeyFrames.Clear();
			this.mCurrentLifetimeSettings = new LifetimeSettings();
			this.mDeflectorCollMap.Clear();
		}

		public virtual void Update()
		{
			if (!this.mInitialized)
			{
				this.mInitialized = true;
				this.mOriginalWeight = this.mWeight;
				this.mOriginalBounce = this.mBounce;
			}
			this.mUpdateCount++;
			if (this.Dead())
			{
				return;
			}
			if (this.mLifetimeKeyFrames.size<LifetimeSettingKeyFrame>() > 0 && this.mKeyFrameIndex + 1 < this.mLifetimeKeyFrames.size<LifetimeSettingKeyFrame>() && this.mUpdateCount >= this.mLifetimeKeyFrames[this.mKeyFrameIndex + 1].first)
			{
				this.mKeyFrameIndex++;
			}
			this.mCurrentLifetimeSettings = this.GetInterpLifetimeSettings();
			this.mBounce = this.mOriginalBounce * this.mCurrentLifetimeSettings.mBounceMult;
			this.mWeight = this.mOriginalWeight * this.mCurrentLifetimeSettings.mWeightMult;
			this.mVY += this.mWeight;
			this.mVX += this.mAX;
			this.mVY += this.mAY;
			this.mX += this.mVX * this.mCurrentLifetimeSettings.mVelocityMult + this.mMotionRandAccum;
			this.mY += this.mVY * this.mCurrentLifetimeSettings.mVelocityMult + this.mMotionRandAccum;
			if (this.mMotionRand > 0f)
			{
				this.mMotionRandAccum += (-this.mMotionRand / 2f + Common.FloatRange(0f, this.mMotionRand)) * this.mCurrentLifetimeSettings.mMotionRandMult / 10f;
			}
			this.mAngle += this.mSpin * this.mCurrentLifetimeSettings.mSpinMult;
			this.mAX = (this.mAY = 0f);
		}

		public virtual bool Dead()
		{
			return this.mLife >= 0 && this.mUpdateCount >= this.mLife;
		}

		public virtual void ApplyAcceleration(float ax, float ay)
		{
			this.mAX += ax;
			this.mAY += ay;
		}

		public virtual float GetX()
		{
			return this.mX;
		}

		public virtual float GetY()
		{
			return this.mY;
		}

		public virtual void SetX(float x)
		{
			this.mX = x;
		}

		public virtual void SetY(float y)
		{
			this.mY = y;
		}

		public virtual void SetXY(float x, float y)
		{
			this.SetX(x);
			this.SetY(y);
		}

		public virtual bool CanInteract()
		{
			return true;
		}

		protected List<LifetimeSettingKeyFrame> mLifetimeKeyFrames = new List<LifetimeSettingKeyFrame>();

		protected LifetimeSettings mCurrentLifetimeSettings = new LifetimeSettings();

		protected int mKeyFrameIndex;

		protected float mOriginalWeight;

		protected float mOriginalBounce;

		protected float mMotionRandAccum;

		protected float mAX;

		protected float mAY;

		protected float mX;

		protected float mY;

		protected bool mInitialized;

		public Dictionary<Deflector, DeflectorCollInfo> mDeflectorCollMap = new Dictionary<Deflector, DeflectorCollInfo>();

		public int mUpdateCount;

		public int mLife;

		public float mVX;

		public float mVY;

		public float mMotionRand;

		public float mWeight;

		public float mAngle;

		public float mSpin;

		public float mBounce;

		protected LifetimeSettings mInterpLifetimeSettings = new LifetimeSettings();
	}
}
