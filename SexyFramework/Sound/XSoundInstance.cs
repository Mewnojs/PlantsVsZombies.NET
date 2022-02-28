using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Sexy.Sound
{
	public class XSoundInstance : SoundInstance
	{
		public static XSoundInstance GetNewXSoundInstance(int id, SoundEffectInstance instance)
		{
			if (XSoundInstance.unusedObjects.Count > 0)
			{
				XSoundInstance xsoundInstance = XSoundInstance.unusedObjects.Pop();
				xsoundInstance.Reset(id, instance);
				return xsoundInstance;
			}
			return new XSoundInstance(id, instance);
		}

		public XSoundInstance(int id, SoundEffectInstance instance)
		{
			this.Reset(id, instance);
		}

		public void Reset(int id, SoundEffectInstance instance)
		{
			this.m_SoundInstance = instance;
			this.m_SoundID = id;
			this.didPlay = false;
			this.mBaseVolume = 1f;
			this.mVolume = 1f;
			this.mBasePan = 0f;
			this.mPan = 0f;
			this.mPitch = 0f;
			this.mIsReleased = false;
		}

		public override void Release()
		{
			if (this.m_SoundInstance != null)
			{
				this.m_SoundInstance.Stop();
				this.m_SoundInstance.Dispose();
				this.m_SoundInstance = null;
			}
			this.mIsReleased = true;
		}

		public override void SetBaseVolume(double theBaseVolume)
		{
			this.mBaseVolume = (float)theBaseVolume;
		}

		public override void SetBasePan(int theBasePan)
		{
			this.mBasePan = (float)theBasePan / 100f;
		}

		public override void SetBaseRate(double theBaseRate)
		{
		}

		public override void AdjustPitch(double theNumSteps)
		{
			this.mPitch = (float)theNumSteps;
		}

		public override void SetVolume(double theVolume)
		{
			this.mVolume = (float)theVolume;
			if (this.m_SoundInstance != null)
			{
				this.m_SoundInstance.Volume = (float)theVolume;
			}
		}

		public override void SetMasterVolumeIdx(int theVolumeIdx)
		{
		}

		public override void SetPan(int thePosition)
		{
			this.mPan = (float)thePosition / 10000f;
		}

		public override bool Play(bool looping, bool autoRelease)
		{
			this.Stop();
			this.didPlay = true;
			if (this.m_SoundInstance.State == SoundState.Stopped)
			{
				this.m_SoundInstance.IsLooped = looping;
			}
			this.m_SoundInstance.Play();
			return true;
		}

		public override void Stop()
		{
			if (this.m_SoundInstance != null)
			{
				this.m_SoundInstance.Stop();
			}
		}

		public override void Pause()
		{
			if (this.m_SoundInstance != null)
			{
				this.m_SoundInstance.Pause();
			}
		}

		public override void Resume()
		{
			if (this.m_SoundInstance != null)
			{
				this.m_SoundInstance.Resume();
			}
		}

		public override bool IsPlaying()
		{
			return this.m_SoundInstance != null && this.m_SoundInstance.State == 0;
		}

		public override bool IsReleased()
		{
			return this.mIsReleased;
		}

		public override double GetVolume()
		{
			return (double)this.mVolume;
		}

		public override bool IsDormant()
		{
			return this.didPlay && this.m_SoundInstance.State == SoundState.Stopped;
		}

		public void PrepareForReuse()
		{
			XSoundInstance.unusedObjects.Push(this);
		}

		private SoundEffectInstance m_SoundInstance;

		private static Stack<XSoundInstance> unusedObjects = new Stack<XSoundInstance>(20);

		public int m_SoundID;

		public float mBaseVolume;

		public float mBasePan;

		private float mVolume;

		private float mPan;

		private float mPitch;

		private bool didPlay;

		private bool mIsReleased;
	}
}
