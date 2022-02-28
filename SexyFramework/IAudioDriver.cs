using System;
using Sexy.Sound;

namespace Sexy
{
	public abstract class IAudioDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract bool InitAudioDriver();

		public abstract SoundManager CreateSoundManager();

		public abstract MusicInterface CreateMusicInterface();
	}
}
