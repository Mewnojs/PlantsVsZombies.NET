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
				return mId;
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
			Reset(0U, null);
		}

		private XNASoundInstance(uint id, SoundEffectInstance instance)
		{
			Reset(id, instance);
		}

		public void Reset(uint id, SoundEffectInstance instance)
		{
			mBaseVolume = 1f;
			mVolume = 1f;
			mBasePan = 0f;
			mPan = 0f;
			mPitch = 0f;
			mIsReleased = false;
			mId = id;
			didPlay = false;
			mSound = instance;
		}

		public void PrepareForReuse()
		{
			XNASoundInstance.unusedObjects.Push(this);
		}

		public override void Release()
		{
			if (mSound != null)
			{
				mSound.Dispose();
				mSound = null;
			}
			mIsReleased = true;
		}

		public override void SetBaseVolume(double theBaseVolume)
		{
			mBaseVolume = (float)theBaseVolume;
		}

		public override void SetBasePan(int theBasePan)
		{
			mBasePan = (float)theBasePan / 100f;
		}

		public override void AdjustPitch(double theNumSteps)
		{
			mPitch = (float)theNumSteps;
		}

		public override void SetVolume(double theVolume)
		{
			mVolume = (float)theVolume;
		}

		public override void SetPan(int thePosition)
		{
			mPan = (float)thePosition / 10000f;
		}

		public override bool Play(bool looping)
		{
			didPlay = true;
			if (mSound.State == SoundState.Stopped)
			{
				mSound.IsLooped = looping;
			}
			mSound.Volume = mBaseVolume * mVolume;
			mSound.Pan = Math.Min(Math.Max(-1f, mBasePan + mPan), 1f);
			mSound.Pitch = mPitch / 10f;
			mSound.Play();
			return true;
		}

		public override bool Play(bool looping, bool autoRelease)
		{
			return Play(looping);
		}

		public override void Stop()
		{
			if (mSound != null)
			{
				mSound.Stop();
			}
		}

		public override bool IsPlaying()
		{
			return mSound != null && mSound.State == SoundState.Playing;
		}

		public override bool IsDormant()
		{
			return didPlay && mSound.State == SoundState.Stopped;
		}

		public override double GetVolume()
		{
			return (double)mVolume;
		}

		public override bool IsReleased()
		{
			return mIsReleased;
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
