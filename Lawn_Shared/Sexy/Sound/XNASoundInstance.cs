using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Sexy
{
	public/*internal*/ class XNASoundInstance : SoundInstance
	{
		public uint SoundId
		{
			get
			{
				return this.mId;
			}
		}

		public static void PreallocateMemory()
		{
			for (int i = 0; i < 50; i++)
			{
				new XNASoundInstance().PrepareForReuse();
			}
		}

		public static XNASoundInstance GetNewXNASoundInstance(uint id, SoundEffectInstance instance)
		{
			if (XNASoundInstance.unusedObjects.Count > 0)
			{
				XNASoundInstance xnasoundInstance = XNASoundInstance.unusedObjects.Pop();
				xnasoundInstance.Reset(id, instance);
				return xnasoundInstance;
			}
			Console.Write("newsound");
			return new XNASoundInstance(id, instance);
		}

		private XNASoundInstance()
		{
			this.Reset(0U, null);
		}

		private XNASoundInstance(uint id, SoundEffectInstance instance)
		{
			this.Reset(id, instance);
		}

		public void Reset(uint id, SoundEffectInstance instance)
		{
			this.mBaseVolume = 1f;
			this.mVolume = 1f;
			this.mBasePan = 0f;
			this.mPan = 0f;
			this.mPitch = 0f;
			this.mIsReleased = false;
			this.mId = id;
			this.didPlay = false;
			this.mSound = instance;
		}

		public void PrepareForReuse()
		{
			XNASoundInstance.unusedObjects.Push(this);
		}

		public override void Release()
		{
			if (this.mSound != null)
			{
				this.mSound.Dispose();
				this.mSound = null;
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

		public override void AdjustPitch(double theNumSteps)
		{
			this.mPitch = (float)theNumSteps;
		}

		public override void SetVolume(double theVolume)
		{
			this.mVolume = (float)theVolume;
		}

		public override void SetPan(int thePosition)
		{
			this.mPan = (float)thePosition / 10000f;
		}

		public override bool Play(bool looping)
		{
			this.didPlay = true;
			if (this.mSound.State == SoundState.Stopped)
			{
				this.mSound.IsLooped = looping;
			}
			this.mSound.Volume = this.mBaseVolume * this.mVolume;
			this.mSound.Pan = Math.Min(Math.Max(-1f, this.mBasePan + this.mPan), 1f);
			this.mSound.Pitch = this.mPitch / 10f;
			this.mSound.Play();
			return true;
		}

		public override bool Play(bool looping, bool autoRelease)
		{
			return this.Play(looping);
		}

		public override void Stop()
		{
			if (this.mSound != null)
			{
				this.mSound.Stop();
			}
		}

		public override bool IsPlaying()
		{
			return this.mSound != null && this.mSound.State == SoundState.Playing;
		}

		public override bool IsDormant()
		{
			return this.didPlay && this.mSound.State == SoundState.Stopped;
		}

		public override double GetVolume()
		{
			return (double)this.mVolume;
		}

		public override bool IsReleased()
		{
			return this.mIsReleased;
		}

		private float mBaseVolume;

		private float mBasePan;

		private float mVolume;

		private float mPan;

		private float mPitch;

		private bool didPlay;

		private bool mIsReleased;

		private uint mId;

		private SoundEffectInstance mSound;

		private static Stack<XNASoundInstance> unusedObjects = new Stack<XNASoundInstance>(50);
	}
}
