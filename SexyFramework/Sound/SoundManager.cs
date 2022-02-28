using System;

namespace Sexy.Sound
{
	public abstract class SoundManager
	{
		public abstract bool Initialized();

		public abstract bool LoadSound(uint theSfxID, string theFilename);

		public abstract int LoadSound(string theFilename);

		public virtual void Dispose()
		{
		}

		public virtual void ReleaseSound(int theSfxID)
		{
			throw new NotImplementedException();
		}

		public virtual double GetVolume(int theVolIdx)
		{
			return 0.0;
		}

		public abstract void SetVolume(double theVolume);

		public abstract void SetBaseVolume(uint mSoundId, double mVolume);

		public abstract void SetBasePan(uint theSfxID, int theBasePan);

		public abstract SoundInstance GetSoundInstance(int theSfxID);

		public abstract SoundInstance GetExistSoundInstance(int theSfxID);

		public abstract void ReleaseSounds();

		public abstract void ReleaseChannels();

		public abstract double GetMasterVolume();

		public abstract void SetMasterVolume(double theVolume);

		public abstract void Flush();

		public abstract void StopAllSounds();

		public abstract int GetFreeSoundId();

		public abstract int GetNumSounds();

		public abstract void Update();

		public float m_MasterVolume = 1f;
	}
}
