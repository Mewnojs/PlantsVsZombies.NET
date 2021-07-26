using System;

namespace Sexy
{
	public abstract class SoundInstance
	{
		public abstract void Release();

		public abstract void SetBaseVolume(double theBaseVolume);

		public abstract void SetBasePan(int theBasePan);

		public abstract void AdjustPitch(double theNumSteps);

		public abstract void SetVolume(double theVolume);

		public abstract void SetPan(int thePosition);

		public abstract bool Play(bool looping);

		public abstract bool Play(bool looping, bool autoRelease);

		public abstract void Stop();

		public abstract bool IsPlaying();

		public abstract bool IsDormant();

		public abstract double GetVolume();

		public abstract bool IsReleased();
	}
}
