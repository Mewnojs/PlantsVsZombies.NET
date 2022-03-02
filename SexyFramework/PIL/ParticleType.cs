using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class ParticleType
	{
		public ParticleType()
		{
			this.mColorKeyManager = new ColorKeyManager();
			this.mAlphaKeyManager = new ColorKeyManager();
			this.mSettingsTimeLine.mCurrentSettings = new ParticleSettings();
			this.mVarTimeLine.mCurrentSettings = new ParticleVariance();
		}

		public ParticleType(ParticleType rhs)
			: this()
		{
			this.CopyFrom(rhs);
		}

		public virtual void Dispose()
		{
			this.mColorKeyManager = null;
			this.mAlphaKeyManager = null;
		}

		public void CopyFrom(ParticleType rhs)
		{
			if (this == rhs || rhs == null)
			{
				return;
			}
			this.mSettingsTimeLine = rhs.mSettingsTimeLine;
			this.mVarTimeLine = rhs.mVarTimeLine;
			this.mSameColorKeyCount = rhs.mSameColorKeyCount;
			this.mLastColorKeyIndex = rhs.mLastColorKeyIndex;
			this.mImageSetByPINLoader = rhs.mImageSetByPINLoader;
			this.mInitAngleStep = rhs.mInitAngleStep;
			this.mLastSpawnAngle = rhs.mLastSpawnAngle;
			this.mLifePctSettings.Clear();
			for (int i = 0; i < rhs.mLifePctSettings.size<LifetimeSettingPct>(); i++)
			{
				this.AddSettingAtLifePct(rhs.mLifePctSettings[i].first, new LifetimeSettings(rhs.mLifePctSettings[i].second));
			}
			this.mImageRate = rhs.mImageRate;
			if (this.mImageSetByPINLoader)
			{
				this.mImage = GlobalMembers.gSexyAppBase.CopyImage(rhs.mImage);
			}
			else
			{
				this.mImage = rhs.mImage;
			}
			this.mImageName = rhs.mImageName;
			this.mName = rhs.mName;
			this.mColorKeyManager = rhs.mColorKeyManager;
			this.mAlphaKeyManager = rhs.mAlphaKeyManager;
			this.mXOff = rhs.mXOff;
			this.mYOff = rhs.mYOff;
			this.mRandomStartCel = rhs.mRandomStartCel;
			this.mLockSizeAspect = rhs.mLockSizeAspect;
			this.mAdditive = rhs.mAdditive;
			this.mAdditiveWithNormal = rhs.mAdditiveWithNormal;
			this.mFlipX = rhs.mFlipX;
			this.mFlipY = rhs.mFlipY;
			this.mLoopTimeline = rhs.mLoopTimeline;
			this.mAlignAngleToMotion = rhs.mAlignAngleToMotion;
			this.mSingle = rhs.mSingle;
			this.mMotionAngleOffset = rhs.mMotionAngleOffset;
			this.mAngleRange = rhs.mAngleRange;
			this.mInitAngle = rhs.mInitAngle;
			this.mEmitterAttachPct = rhs.mEmitterAttachPct;
			this.mNumSameColorKeyInRow = rhs.mNumSameColorKeyInRow;
			this.mNumCreated = rhs.mNumCreated;
			this.mRefXOff = rhs.mRefXOff;
			this.mRefYOff = rhs.mRefYOff;
		}

		public void ResetForReuse()
		{
			this.mNumCreated = 0;
			this.mSameColorKeyCount = 0;
			this.mLastColorKeyIndex = 0;
			this.mLastSpawnAngle = float.MaxValue;
		}

		public void Serialize(SexyBuffer b, GlobalMembers.GetIdByImageFunc f)
		{
			this.mSettingsTimeLine.Serialize(b);
			this.mVarTimeLine.Serialize(b);
			b.WriteLong(this.mSameColorKeyCount);
			b.WriteLong(this.mLastColorKeyIndex);
			b.WriteBoolean(this.mImageSetByPINLoader);
			b.WriteLong(this.mLifePctSettings.Count);
			for (int i = 0; i < this.mLifePctSettings.Count; i++)
			{
				b.WriteFloat(this.mLifePctSettings[i].first);
				this.mLifePctSettings[i].second.Serialize(b);
			}
			b.WriteBoolean(this.mImage != null);
			if (this.mImage != null)
			{
				b.WriteLong(f(this.mImage));
			}
			b.WriteStringWithEncoding(this.mImageName);
			b.WriteStringWithEncoding(this.mName);
			this.mColorKeyManager.Serialize(b);
			this.mAlphaKeyManager.Serialize(b);
			b.WriteBoolean(this.mLockSizeAspect);
			b.WriteBoolean(this.mAdditive);
			b.WriteBoolean(this.mAdditiveWithNormal);
			b.WriteBoolean(this.mFlipX);
			b.WriteBoolean(this.mFlipY);
			b.WriteBoolean(this.mLoopTimeline);
			b.WriteBoolean(this.mAlignAngleToMotion);
			b.WriteBoolean(this.mSingle);
			b.WriteFloat(this.mMotionAngleOffset);
			b.WriteFloat(this.mInitAngle);
			b.WriteFloat(this.mAngleRange);
			b.WriteFloat(this.mEmitterAttachPct);
			b.WriteLong(this.mNumSameColorKeyInRow);
			b.WriteLong(this.mNumCreated);
			b.WriteLong(this.mRefXOff);
			b.WriteLong(this.mRefYOff);
			b.WriteLong(this.mImageRate);
			b.WriteLong(this.mSerialIndex);
			b.WriteFloat(this.mInitAngleStep);
			b.WriteFloat(this.mLastSpawnAngle);
			b.WriteBoolean(this.mRandomStartCel);
			b.WriteLong(this.mXOff);
			b.WriteLong(this.mYOff);
		}

		public void Deserialize(SexyBuffer b, GlobalMembers.GetImageByIdFunc f)
		{
			this.mSettingsTimeLine.Deserialize(b, new GlobalMembers.KFDInstantiateFunc(ParticleSettings.Instantiate));
			this.mVarTimeLine.Deserialize(b, new GlobalMembers.KFDInstantiateFunc(ParticleVariance.Instantiate));
			this.mSameColorKeyCount = (int)b.ReadLong();
			this.mLastColorKeyIndex = (int)b.ReadLong();
			this.mImageSetByPINLoader = b.ReadBoolean();
			int num = (int)b.ReadLong();
			for (int i = 0; i < num; i++)
			{
				float f2 = b.ReadFloat();
				LifetimeSettings lifetimeSettings = new LifetimeSettings();
				lifetimeSettings.Deserialize(b);
				this.mLifePctSettings.Add(new LifetimeSettingPct(f2, lifetimeSettings));
			}
			this.mImage = null;
			if (b.ReadBoolean())
			{
				this.mImage = f((int)b.ReadLong());
			}
			this.mImageName = b.ReadStringWithEncoding();
			this.mName = b.ReadStringWithEncoding();
			this.mColorKeyManager.Deserialize(b);
			this.mAlphaKeyManager.Deserialize(b);
			this.mLockSizeAspect = b.ReadBoolean();
			this.mAdditive = b.ReadBoolean();
			this.mAdditiveWithNormal = b.ReadBoolean();
			this.mFlipX = b.ReadBoolean();
			this.mFlipY = b.ReadBoolean();
			this.mLoopTimeline = b.ReadBoolean();
			this.mAlignAngleToMotion = b.ReadBoolean();
			this.mSingle = b.ReadBoolean();
			this.mMotionAngleOffset = b.ReadFloat();
			this.mInitAngle = b.ReadFloat();
			this.mAngleRange = b.ReadFloat();
			this.mEmitterAttachPct = b.ReadFloat();
			this.mNumSameColorKeyInRow = (int)b.ReadLong();
			this.mNumCreated = (int)b.ReadLong();
			this.mRefXOff = (int)b.ReadLong();
			this.mRefYOff = (int)b.ReadLong();
			this.mImageRate = (int)b.ReadLong();
			this.mSerialIndex = (int)b.ReadLong();
			this.mInitAngleStep = b.ReadFloat();
			this.mLastSpawnAngle = b.ReadFloat();
			this.mRandomStartCel = b.ReadBoolean();
			this.mXOff = (int)b.ReadLong();
			this.mYOff = (int)b.ReadLong();
		}

		public void GetCreationParameters(int current_frame, out int life_frames, out float emit_frame, out ParticleSettings kfdata, out ParticleVariance vardata)
		{
			this.mSettingsTimeLine.Update(current_frame);
			this.mVarTimeLine.Update(current_frame);
			if (this.mSettingsTimeLine.mKeyFrames.size<KeyFrame>() == 0)
			{
				kfdata = new ParticleSettings();
				this.mSettingsTimeLine.mCurrentSettings = kfdata;
			}
			else
			{
				kfdata = (ParticleSettings)this.mSettingsTimeLine.mCurrentSettings;
			}
			if (this.mVarTimeLine.mKeyFrames.size<KeyFrame>() == 0)
			{
				vardata = new ParticleVariance();
				this.mVarTimeLine.mCurrentSettings = vardata;
			}
			else
			{
				vardata = (ParticleVariance)this.mVarTimeLine.mCurrentSettings;
			}
			if (kfdata.mLife == -1)
			{
				life_frames = -1;
			}
			else
			{
				int mLife = kfdata.mLife;
				Common.SAFE_RAND((float)(vardata.mLifeVar / 2));
				Common.SAFE_RAND((float)(vardata.mLifeVar / 2));
				life_frames = this.GetRandomizedLife();
				if (life_frames < 0)
				{
					life_frames = 0;
				}
			}
			float num = (float)(kfdata.mNumber + (int)Common.SAFE_RAND((float)vardata.mNumberVar));
			if (num == 0f)
			{
				emit_frame = float.MaxValue;
				return;
			}
			emit_frame = 100f / num;
		}

		public int GetRandomizedLife()
		{
			ParticleSettings particleSettings = this.mSettingsTimeLine.mCurrentSettings as ParticleSettings;
			ParticleVariance particleVariance = this.mVarTimeLine.mCurrentSettings as ParticleVariance;
			if (particleSettings.mLife == -1)
			{
				return -1;
			}
			int num;
			if (!this.mUseNewLifeRandomization)
			{
				num = particleSettings.mLife - (int)Common.SAFE_RAND((float)(particleVariance.mLifeVar / 2)) + (int)Common.SAFE_RAND((float)(particleVariance.mLifeVar / 2));
			}
			else
			{
				num = particleSettings.mLife + (int)Common.SAFE_RAND((float)particleVariance.mLifeVar);
			}
			if (num < 0)
			{
				num = 0;
			}
			return 10 * num;
		}

		public ParticleSettings AddSettingsKeyFrame(int frame_time, ParticleSettings @params, int second_frame_time, bool make_new)
		{
			this.mSettingsTimeLine.AddKeyFrame(frame_time, @params);
			if (second_frame_time != -1)
			{
				ParticleSettings k = new ParticleSettings(@params);
				this.mSettingsTimeLine.AddKeyFrame(second_frame_time, k);
				if (make_new)
				{
					return new ParticleSettings(@params);
				}
			}
			return null;
		}

		public ParticleSettings AddSettingsKeyFrame(int frame_time, ParticleSettings @params, int second_frame_time)
		{
			return this.AddSettingsKeyFrame(frame_time, @params, second_frame_time, false);
		}

		public ParticleSettings AddSettingsKeyFrame(int frame_time, ParticleSettings @params)
		{
			return this.AddSettingsKeyFrame(frame_time, @params, -1, false);
		}

		public ParticleVariance AddVarianceKeyFrame(int frame_time, ParticleVariance @params, int second_frame_time, bool make_new)
		{
			this.mVarTimeLine.AddKeyFrame(frame_time, @params);
			if (second_frame_time != -1)
			{
				ParticleVariance k = new ParticleVariance(@params);
				this.mSettingsTimeLine.AddKeyFrame(second_frame_time, k);
				if (make_new)
				{
					return new ParticleVariance(@params);
				}
			}
			return null;
		}

		public ParticleVariance AddVarianceKeyFrame(int frame_time, ParticleVariance @params, int second_frame_time)
		{
			return this.AddVarianceKeyFrame(frame_time, @params, second_frame_time, false);
		}

		public ParticleVariance AddVarianceKeyFrame(int frame_time, ParticleVariance @params)
		{
			return this.AddVarianceKeyFrame(frame_time, @params, -1, false);
		}

		public ParticleSettings GetSettingsKeyFrame(int frame_time)
		{
			for (int i = 0; i < this.mSettingsTimeLine.mKeyFrames.size<KeyFrame>(); i++)
			{
				if (this.mSettingsTimeLine.mKeyFrames[i].first == frame_time)
				{
					return (ParticleSettings)this.mSettingsTimeLine.mKeyFrames[i].second;
				}
			}
			return null;
		}

		public ParticleVariance GetVarianceKeyFrame(int frame_time)
		{
			for (int i = 0; i < this.mVarTimeLine.mKeyFrames.size<KeyFrame>(); i++)
			{
				if (this.mVarTimeLine.mKeyFrames[i].first == frame_time)
				{
					return (ParticleVariance)this.mVarTimeLine.mKeyFrames[i].second;
				}
			}
			return null;
		}

		public bool EndOfSettingsTimeLine(int frame)
		{
			return this.mSettingsTimeLine.mKeyFrames.size<KeyFrame>() == 0 || frame >= this.mSettingsTimeLine.mKeyFrames.back<KeyFrame>().first;
		}

		public bool EndOfVarianceTimeLine(int frame)
		{
			return this.mVarTimeLine.mKeyFrames.size<KeyFrame>() == 0 || frame >= this.mVarTimeLine.mKeyFrames.back<KeyFrame>().first;
		}

		public float GetSpawnAngle()
		{
			if (this.mInitAngleStep == 0f)
			{
				return this.mInitAngle - Common.FloatRange(0f, this.mAngleRange / 2f) + Common.FloatRange(0f, this.mAngleRange / 2f);
			}
			if (Common._eq(this.mLastSpawnAngle, 3.4028235E+38f))
			{
				this.mLastSpawnAngle = this.mInitAngle - Common.FloatRange(0f, this.mAngleRange / 2f) + Common.FloatRange(0f, this.mAngleRange / 2f);
			}
			else
			{
				this.mLastSpawnAngle += this.mInitAngleStep;
				if (Common._geq(this.mLastSpawnAngle, this.mInitAngle + this.mAngleRange / 2f))
				{
					this.mLastSpawnAngle -= this.mAngleRange;
				}
			}
			return this.mLastSpawnAngle;
		}

		public void LoopTimeLine(bool l)
		{
			this.mSettingsTimeLine.mLoop = l;
		}

		public void LoopVarTimeLine(bool l)
		{
			this.mVarTimeLine.mLoop = l;
		}

		public LifetimeSettings AddSettingAtLifePct(float pct, LifetimeSettings s, float second_frame_pct, bool make_new)
		{
			this.mLifePctSettings.Add(new LifetimeSettingPct(pct, s));
			this.mLifePctSettings.Sort(new LifePctSort());
			if (second_frame_pct >= 0f)
			{
				this.AddSettingAtLifePct(second_frame_pct, new LifetimeSettings(s), -1f, false);
				if (make_new)
				{
					return new LifetimeSettings(s);
				}
			}
			return null;
		}

		public LifetimeSettings AddSettingAtLifePct(float pct, LifetimeSettings s, float second_frame_pct)
		{
			return this.AddSettingAtLifePct(pct, s, second_frame_pct, false);
		}

		public LifetimeSettings AddSettingAtLifePct(float pct, LifetimeSettings s)
		{
			return this.AddSettingAtLifePct(pct, s, -1f, false);
		}

		public int GetSettingsTimeLineSize()
		{
			return this.mSettingsTimeLine.mKeyFrames.size<KeyFrame>();
		}

		public int GetVarTimeLineSize()
		{
			return this.mVarTimeLine.mKeyFrames.size<KeyFrame>();
		}

		public SexyColor GetNextKeyColor()
		{
			if (this.mNumSameColorKeyInRow <= 0)
			{
				return SexyColor.White;
			}
			if (++this.mSameColorKeyCount >= this.mNumSameColorKeyInRow)
			{
				this.mSameColorKeyCount = 0;
				this.mLastColorKeyIndex = (this.mLastColorKeyIndex + 1) % this.mColorKeyManager.GetNumKeys();
			}
			return this.mColorKeyManager.GetColorByIndex(this.mLastColorKeyIndex);
		}

		public TimeLine mSettingsTimeLine = new TimeLine();

		public TimeLine mVarTimeLine = new TimeLine();

		public int mSameColorKeyCount;

		public int mLastColorKeyIndex;

		public float mLastSpawnAngle = float.MaxValue;

		public bool mImageSetByPINLoader;

		public List<LifetimeSettingPct> mLifePctSettings = new List<LifetimeSettingPct>();

		public Image mImage;

		public string mImageName = "";

		public string mName = "";

		public ColorKeyManager mColorKeyManager;

		public ColorKeyManager mAlphaKeyManager;

		public bool mUseNewLifeRandomization;

		public bool mLockSizeAspect = true;

		public bool mAdditive;

		public bool mAdditiveWithNormal;

		public bool mFlipX;

		public bool mFlipY;

		public bool mLoopTimeline;

		public bool mAlignAngleToMotion;

		public bool mSingle;

		public float mMotionAngleOffset;

		public float mInitAngle;

		public float mAngleRange;

		public float mInitAngleStep;

		public float mEmitterAttachPct;

		public int mNumSameColorKeyInRow;

		public int mNumCreated;

		public int mRefXOff;

		public int mRefYOff;

		public int mImageRate = 1;

		public int mXOff;

		public int mYOff;

		public bool mRandomStartCel;

		public int mSerialIndex = -1;
	}
}
