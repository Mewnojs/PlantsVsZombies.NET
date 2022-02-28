using System;
using Microsoft.Xna.Framework.Content;

namespace Sexy
{
	public class ProxyMusicInterface : MusicInterface
	{
		public ProxyMusicInterface(MusicInterface theTargetInterface, bool deleteTarget)
		{
			this.mTargetInterface = theTargetInterface;
			this.mDeleteTarget = deleteTarget;
		}

		public override void Dispose()
		{
			if (this.mDeleteTarget && this.mTargetInterface != null)
			{
				this.mTargetInterface.Dispose();
			}
			base.Dispose();
		}

		public override bool LoadMusic(int theSongId, string theFileName, ContentManager content)
		{
			return this.mTargetInterface.LoadMusic(theSongId, theFileName, content);
		}

		public override void PlayMusic(int theSongId, int theOffset, bool noLoop)
		{
			this.PlayMusic(theSongId, theOffset, noLoop, 0L);
		}

		public override void PlayMusic(int theSongId, int theOffset)
		{
			this.PlayMusic(theSongId, theOffset, false, 0L);
		}

		public override void PlayMusic(int theSongId)
		{
			this.PlayMusic(theSongId, 0, false, 0L);
		}

		public override void PlayMusic(int theSongId, int theOffset, bool noLoop, long theStartPos)
		{
			this.mTargetInterface.PlayMusic(theSongId, theOffset, noLoop, theStartPos);
		}

		public override void StopMusic(int theSongId)
		{
			this.mTargetInterface.StopMusic(theSongId);
		}

		public override void PauseMusic(int theSongId)
		{
			this.mTargetInterface.PauseMusic(theSongId);
		}

		public override void ResumeMusic(int theSongId)
		{
			this.mTargetInterface.ResumeMusic(theSongId);
		}

		public override void StopAllMusic()
		{
			this.mTargetInterface.StopAllMusic();
		}

		public override void UnloadMusic(int theSongId)
		{
			this.mTargetInterface.UnloadMusic(theSongId);
		}

		public override void UnloadAllMusic()
		{
			this.mTargetInterface.UnloadAllMusic();
		}

		public override void PauseAllMusic()
		{
			this.mTargetInterface.PauseAllMusic();
		}

		public override void ResumeAllMusic()
		{
			this.mTargetInterface.ResumeAllMusic();
		}

		public override void FadeIn(int theSongId, int theOffset, double theSpeed)
		{
			this.FadeIn(theSongId, theOffset, theSpeed, false);
		}

		public override void FadeIn(int theSongId, int theOffset)
		{
			this.FadeIn(theSongId, theOffset, 0.002, false);
		}

		public override void FadeIn(int theSongId)
		{
			this.FadeIn(theSongId, -1, 0.002, false);
		}

		public override void FadeIn(int theSongId, int theOffset, double theSpeed, bool noLoop)
		{
			this.mTargetInterface.FadeIn(theSongId, theOffset, theSpeed, noLoop);
		}

		public override void FadeOut(int theSongId, bool stopSong)
		{
			this.FadeOut(theSongId, stopSong, 0.004);
		}

		public override void FadeOut(int theSongId)
		{
			this.FadeOut(theSongId, true, 0.004);
		}

		public override void FadeOut(int theSongId, bool stopSong, double theSpeed)
		{
			this.mTargetInterface.FadeOut(theSongId, stopSong, theSpeed);
		}

		public override void FadeOutAll(bool stopSong)
		{
			this.FadeOutAll(stopSong, 0.004);
		}

		public override void FadeOutAll()
		{
			this.FadeOutAll(true, 0.004);
		}

		public override void FadeOutAll(bool stopSong, double theSpeed)
		{
			this.mTargetInterface.FadeOutAll(stopSong, theSpeed);
		}

		public override void SetSongVolume(int theSongId, double theVolume)
		{
			this.mTargetInterface.SetSongVolume(theSongId, theVolume);
		}

		public override void SetSongMaxVolume(int theSongId, double theMaxVolume)
		{
			this.mTargetInterface.SetSongMaxVolume(theSongId, theMaxVolume);
		}

		public override bool IsPlaying(int theSongId)
		{
			return this.mTargetInterface.IsPlaying(theSongId);
		}

		public override void SetVolume(double theVolume)
		{
			this.mTargetInterface.SetVolume(theVolume);
		}

		public override void SetMusicAmplify(int theSongId, double theAmp)
		{
			this.mTargetInterface.SetMusicAmplify(theSongId, theAmp);
		}

		public override void Update()
		{
			this.mTargetInterface.Update();
		}

		private MusicInterface mTargetInterface;

		private bool mDeleteTarget;
	}
}
