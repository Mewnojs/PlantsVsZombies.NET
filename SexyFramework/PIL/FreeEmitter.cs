using System;
using System.Collections.Generic;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class FreeEmitter
	{
		public FreeEmitter()
		{
			this.mSettingsTimeLine.mCurrentSettings = new FreeEmitterSettings();
			this.mVarianceTimeLine.mCurrentSettings = new FreeEmitterVariance();
		}

		public FreeEmitter(FreeEmitter rhs)
			: this()
		{
			this.CopyFrom(rhs);
		}

		public virtual void Dispose()
		{
			if (this.mEmitter != null)
			{
				this.mEmitter.Dispose();
			}
			this.mEmitter = null;
			this.mLifePctSettings.Clear();
		}

		public void CopyFrom(FreeEmitter rhs)
		{
			if (rhs == null)
			{
				return;
			}
			this.mSettingsTimeLine = rhs.mSettingsTimeLine;
			this.mVarianceTimeLine = rhs.mVarianceTimeLine;
			this.mAspectLocked = rhs.mAspectLocked;
			if (this.mEmitter == null)
			{
				this.mEmitter = new Emitter(rhs.mEmitter);
			}
			else
			{
				this.mEmitter.CopyFrom(rhs.mEmitter);
			}
			for (int i = 0; i < this.mLifePctSettings.size<LifetimeSettingPct>(); i++)
			{
				this.mLifePctSettings[i].second = null;
			}
			this.mLifePctSettings.Clear();
			for (int j = 0; j < rhs.mLifePctSettings.size<LifetimeSettingPct>(); j++)
			{
				this.AddLifetimeKeyFrame(rhs.mLifePctSettings[j].first, new LifetimeSettings(rhs.mLifePctSettings[j].second));
			}
		}

		public void GetCreationParams(int frame, out int emitter_life, out float emit_frame, out FreeEmitterSettings settings, out FreeEmitterVariance variance)
		{
			this.mSettingsTimeLine.Update(frame);
			this.mVarianceTimeLine.Update(frame);
			if (this.mSettingsTimeLine.mKeyFrames.size<KeyFrame>() == 0)
			{
				settings = new FreeEmitterSettings();
				this.mSettingsTimeLine.mCurrentSettings = settings;
			}
			else
			{
				settings = (FreeEmitterSettings)this.mSettingsTimeLine.mCurrentSettings;
			}
			if (this.mVarianceTimeLine.mKeyFrames.size<KeyFrame>() == 0)
			{
				variance = new FreeEmitterVariance();
				this.mVarianceTimeLine.mCurrentSettings = variance;
			}
			else
			{
				variance = (FreeEmitterVariance)this.mVarianceTimeLine.mCurrentSettings;
			}
			emitter_life = this.GetRandomizedLife();
			emit_frame = 100f / (float)settings.mNumber;
		}

		public int GetRandomizedLife()
		{
			FreeEmitterSettings freeEmitterSettings = this.mSettingsTimeLine.mCurrentSettings as FreeEmitterSettings;
			FreeEmitterVariance freeEmitterVariance = this.mVarianceTimeLine.mCurrentSettings as FreeEmitterVariance;
			int num = freeEmitterSettings.mLife - (int)Common.SAFE_RAND((float)(freeEmitterVariance.mLifeVar / 2)) + (int)Common.SAFE_RAND((float)(freeEmitterVariance.mLifeVar / 2));
			return 10 * num;
		}

		public LifetimeSettings AddLifetimeKeyFrame(float pct, LifetimeSettings s, float second_frame_pct, bool make_new)
		{
			LifetimeSettingPct lifetimeSettingPct = new LifetimeSettingPct(pct, s);
			lifetimeSettingPct.second.mPct = pct;
			this.mLifePctSettings.Add(lifetimeSettingPct);
			this.mLifePctSettings.Sort(new LifePctSort());
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

		public LifetimeSettings AddLifetimeKeyFrame(float pct, LifetimeSettings s, float second_frame_pct)
		{
			return this.AddLifetimeKeyFrame(pct, s, second_frame_pct, false);
		}

		public LifetimeSettings AddLifetimeKeyFrame(float pct, LifetimeSettings s)
		{
			return this.AddLifetimeKeyFrame(pct, s, -1f, false);
		}

		public FreeEmitterSettings AddSettingsKeyFrame(int frame, FreeEmitterSettings s, int second_frame_time, bool make_new)
		{
			this.mSettingsTimeLine.AddKeyFrame(frame, s);
			if (second_frame_time != -1)
			{
				this.mSettingsTimeLine.AddKeyFrame(second_frame_time, new FreeEmitterSettings(s));
				if (make_new)
				{
					return new FreeEmitterSettings(s);
				}
			}
			return null;
		}

		public FreeEmitterSettings AddSettingsKeyFrame(int frame, FreeEmitterSettings s, int second_frame_time)
		{
			return this.AddSettingsKeyFrame(frame, s, second_frame_time, false);
		}

		public FreeEmitterSettings AddSettingsKeyFrame(int frame, FreeEmitterSettings s)
		{
			return this.AddSettingsKeyFrame(frame, s, -1, false);
		}

		public FreeEmitterVariance AddVarianceKeyFrame(int frame, FreeEmitterVariance v, int second_frame_time, bool make_new)
		{
			this.mVarianceTimeLine.AddKeyFrame(frame, v);
			if (second_frame_time != -1)
			{
				this.mVarianceTimeLine.AddKeyFrame(second_frame_time, new FreeEmitterVariance(v));
				if (make_new)
				{
					return new FreeEmitterVariance(v);
				}
			}
			return null;
		}

		public FreeEmitterVariance AddVarianceKeyFrame(int frame, FreeEmitterVariance v, int second_frame_time)
		{
			return this.AddVarianceKeyFrame(frame, v, second_frame_time, false);
		}

		public FreeEmitterVariance AddVarianceKeyFrame(int frame, FreeEmitterVariance v)
		{
			return this.AddVarianceKeyFrame(frame, v, -1, false);
		}

		public void LoopSettingsTimeLine(bool l)
		{
			this.mSettingsTimeLine.mLoop = l;
		}

		public void LoopVarianceTimeLine(bool l)
		{
			this.mVarianceTimeLine.mLoop = l;
		}

		public void Serialize(SexyBuffer b, GlobalMembers.GetIdByImageFunc f)
		{
			this.mSettingsTimeLine.Serialize(b);
			this.mVarianceTimeLine.Serialize(b);
			b.WriteBoolean(this.mAspectLocked);
			b.WriteLong(this.mSerialIndex);
			b.WriteLong(this.mLifePctSettings.Count);
			for (int i = 0; i < this.mLifePctSettings.Count; i++)
			{
				b.WriteFloat(this.mLifePctSettings[i].first);
				this.mLifePctSettings[i].second.Serialize(b);
			}
			this.mEmitter.Serialize(b, f);
		}

		public void Deserialize(SexyBuffer b, GlobalMembers.GetImageByIdFunc f)
		{
			this.mSettingsTimeLine.Deserialize(b, new GlobalMembers.KFDInstantiateFunc(FreeEmitterSettings.Instantiate));
			this.mVarianceTimeLine.Deserialize(b, new GlobalMembers.KFDInstantiateFunc(FreeEmitterVariance.Instantiate));
			this.mAspectLocked = b.ReadBoolean();
			this.mSerialIndex = (int)b.ReadLong();
			int num = (int)b.ReadLong();
			for (int i = 0; i < num; i++)
			{
				float f2 = b.ReadFloat();
				LifetimeSettings lifetimeSettings = new LifetimeSettings();
				lifetimeSettings.Deserialize(b);
				this.mLifePctSettings.Add(new LifetimeSettingPct(f2, lifetimeSettings));
			}
			Dictionary<int, Deflector> deflector_ptr_map = new Dictionary<int, Deflector>();
			Dictionary<int, FreeEmitter> fe_ptr_map = new Dictionary<int, FreeEmitter>();
			this.mEmitter.Deserialize(b, deflector_ptr_map, fe_ptr_map, f);
		}

		public TimeLine mSettingsTimeLine = new TimeLine();

		public TimeLine mVarianceTimeLine = new TimeLine();

		public List<LifetimeSettingPct> mLifePctSettings = new List<LifetimeSettingPct>();

		public Emitter mEmitter;

		public bool mAspectLocked = true;

		public int mSerialIndex = -1;
	}
}
