using System;

namespace Sexy
{
	public abstract class MusicInterface
	{
		public MusicInterface()
		{
		}

		public virtual void Dispose()
		{
		}

		public virtual void Enable(bool enable)
		{
		}

		public virtual bool LoadMusic(int theSongId, string theFileName)
		{
			return false;
		}

		public virtual void PlayMusic(int theSongId)
		{
			this.PlayMusic(theSongId, this.mDefaultFadeOutSeconds, this.mDefaultFadeInSeconds, true);
		}

		public virtual void PlayMusic(int theSongId, bool loop)
		{
			this.PlayMusic(theSongId, this.mDefaultFadeOutSeconds, this.mDefaultFadeInSeconds, loop);
		}

		public virtual void PlayMusic(int theSongId, float fadeInSeconds)
		{
			this.PlayMusic(theSongId, this.mDefaultFadeOutSeconds, fadeInSeconds, true);
		}

		public virtual void PlayMusic(int theSongId, float fadeInSeconds, bool loop)
		{
			this.PlayMusic(theSongId, this.mDefaultFadeOutSeconds, fadeInSeconds, loop);
		}

		public virtual void PlayMusic(int theSongId, float fadeOutSeconds, float fadeInSeconds)
		{
			this.PlayMusic(theSongId, fadeOutSeconds, fadeInSeconds, true);
		}

		public virtual void PlayMusic(int theSongId, float fadeOutSeconds, float fadeInSeconds, bool loop)
		{
			this.PlayMusic(theSongId, 0, fadeOutSeconds, fadeInSeconds, loop);
		}

		public virtual void PlayMusic(int theSongId, int offset)
		{
			this.PlayMusic(theSongId, offset, this.mDefaultFadeOutSeconds, this.mDefaultFadeInSeconds, true);
		}

		public virtual void PlayMusic(int theSongId, int offset, bool loop)
		{
			this.PlayMusic(theSongId, offset, this.mDefaultFadeOutSeconds, this.mDefaultFadeInSeconds, loop);
		}

		public virtual void PlayMusic(int theSongId, int offset, float fadeInSeconds)
		{
			this.PlayMusic(theSongId, offset, this.mDefaultFadeOutSeconds, fadeInSeconds, true);
		}

		public virtual void PlayMusic(int theSongId, int offset, float fadeInSeconds, bool loop)
		{
			this.PlayMusic(theSongId, offset, this.mDefaultFadeOutSeconds, fadeInSeconds, loop);
		}

		public virtual void PlayMusic(int theSongId, int offset, float fadeOutSeconds, float fadeInSeconds)
		{
			this.PlayMusic(theSongId, offset, fadeOutSeconds, fadeInSeconds, true);
		}

		public virtual void PlayMusic(int theSongid, int offset, float fadeOutSeconds, float fadeinSeconds, bool loop)
		{
		}

		public virtual void StopMusic()
		{
			this.StopMusic(this.mDefaultFadeOutSeconds);
		}

		public virtual void StopMusic(float fadeOutSeconds)
		{
		}

		public virtual void PauseMusic()
		{
		}

		public virtual void ResumeMusic()
		{
		}

		public virtual void UnloadMusic(int theSongId)
		{
		}

		public virtual void UnloadAllMusic()
		{
		}

		public virtual void SetDefaultFadeOut(float fadeOutSeconds)
		{
			this.mDefaultFadeOutSeconds = fadeOutSeconds;
		}

		public virtual void SetDefaultFadeIn(float fadeInSeconds)
		{
			this.mDefaultFadeInSeconds = fadeInSeconds;
		}

		public virtual bool IsPlaying(int theSongId)
		{
			return false;
		}

		public virtual void SetVolume(float theVolume)
		{
		}

		public abstract float GetVolume();

		public virtual void Update()
		{
		}

		public virtual int GetFreeMusicId()
		{
			return -1;
		}

		internal const bool kMusicLoop = true;

		internal const bool kMusicNoLoop = false;

		internal const float kMusicNoFade = 0f;

		public static bool USER_MUSIC_PLAYING;

		public bool isStopped;

		protected float mDefaultFadeInSeconds;

		protected float mDefaultFadeOutSeconds;
	}
}
