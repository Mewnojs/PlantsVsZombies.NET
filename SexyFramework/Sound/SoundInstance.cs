using System;

namespace Sexy.Sound
{
	public abstract class SoundInstance
	{
		public SoundInstance()
		{
		}

		public abstract void Release();

		public abstract void SetBaseVolume(double theBaseVolume);

		public abstract void SetBasePan(int theBasePan);

		public abstract void SetBaseRate(double theBaseRate);

		public abstract void AdjustPitch(double theNumSteps);

		public abstract void SetVolume(double theVolume);

		public abstract void SetMasterVolumeIdx(int theVolumeIdx);

		public abstract void SetPan(int thePosition);

		public abstract bool Play(bool looping, bool autoRelease);

		public abstract void Stop();

		public abstract bool IsPlaying();

		public abstract bool IsReleased();

		public abstract double GetVolume();

		public abstract bool IsDormant();

		public abstract void Pause();

		public abstract void Resume();
	}
}
