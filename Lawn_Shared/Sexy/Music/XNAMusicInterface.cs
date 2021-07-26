using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Sexy
{
	internal class XNAMusicInterface : MusicInterface
	{
		public XNAMusicInterface(SexyAppBase theApp)
		{
			MusicInterface.USER_MUSIC_PLAYING = !MediaPlayer.GameHasControl;
			this.mEnabled = false;
			this.mContent = theApp.mContentManager;
			this.mCurrentSong = -1;
			this.mFadeOut = false;
			for (int i = 0; i < XNAMusicInterfaceConstants.MAX_SONGS; i++)
			{
				this.mSongs[i] = null;
			}
		}

		public override void Dispose()
		{
			this.UnloadAllMusic();
			base.Dispose();
		}

		public override void Enable(bool enable)
		{
			this.mEnabled = enable;
		}

		public override bool LoadMusic(int theSongId, string theFileName)
		{
			this.mSongs[theSongId] = this.mContent.Load<Song>(theFileName);
			return true;
		}

		public override void PlayMusic(int theSongid, int offset, float fadeOutSeconds, float fadeinSeconds, bool loop)
		{
			try
			{
				if (!MusicInterface.USER_MUSIC_PLAYING)
				{
					if (theSongid != this.mCurrentSong || MediaPlayer.State != MediaState.Playing)
					{
						this.isStopped = false;
						if (theSongid >= 0)
						{
							this.mCurrentSong = theSongid;
							Song song = this.mSongs[theSongid];
							if (!(song == null))
							{
								MediaPlayer.Play(song);
								if (!this.mHasPlayed)
								{
									this.mHasPlayed = true;
									this.SetVolume(this.mFirstPlayVolume);
								}
								MediaPlayer.IsRepeating = loop;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public override void StopMusic(float fadeOutSeconds)
		{
			if (MusicInterface.USER_MUSIC_PLAYING)
			{
				return;
			}
			this.mCurrentSong = -1;
			try
			{
				MediaPlayer.Stop();
				this.isStopped = true;
			}
			catch (Exception)
			{
			}
		}

		public override void PauseMusic()
		{
			try
			{
				if (!MusicInterface.USER_MUSIC_PLAYING)
				{
					MediaPlayer.Pause();
				}
			}
			catch (Exception)
			{
			}
		}

		public override void ResumeMusic()
		{
			this.isStopped = false;
			if (MusicInterface.USER_MUSIC_PLAYING)
			{
				return;
			}
			try
			{
				if (!this.isStopped)
				{
					MediaPlayer.Resume();
				}
			}
			catch (Exception)
			{
			}
		}

		public override void UnloadMusic(int theSongId)
		{
			if (theSongId == this.mCurrentSong)
			{
				try
				{
					this.StopMusic();
				}
				catch (Exception)
				{
				}
			}
			if (this.mSongs[theSongId] != null)
			{
				this.mSongs[theSongId].Dispose();
				this.mSongs[theSongId] = null;
			}
		}

		public override void UnloadAllMusic()
		{
			try
			{
				this.StopMusic();
			}
			catch (Exception)
			{
			}
			for (int i = 0; i < XNAMusicInterfaceConstants.MAX_SONGS; i++)
			{
				this.UnloadMusic(i);
			}
		}

		public override bool IsPlaying(int theSongId)
		{
			bool flag = false;
			try
			{
				flag = (MediaPlayer.State == MediaState.Playing || MediaPlayer.State == MediaState.Paused);
			}
			catch (Exception)
			{
			}
			return this.mCurrentSong == theSongId && flag;
		}

		public override void SetVolume(float theVolume)
		{
			if (!this.mHasPlayed)
			{
				this.mFirstPlayVolume = theVolume;
				return;
			}
			try
			{
				this.maxVolume = (MediaPlayer.Volume = theVolume * 0.5f);
			}
			catch (Exception)
			{
			}
		}

		public override float GetVolume()
		{
			return this.maxVolume * 2f;
		}

		public override void Update()
		{
			try
			{
				if (this.mFadeOut)
				{
					this.StopMusic(0f);
				}
			}
			catch (Exception)
			{
			}
		}

		public override int GetFreeMusicId()
		{
			for (int i = 0; i < XNAMusicInterfaceConstants.MAX_SONGS; i++)
			{
				if (this.mSongs[i] == null)
				{
					return i;
				}
			}
			return -1;
		}

		private const float VOLUME_LIMIT = 0.5f;

		private Song[] mSongs = new Song[13];

		private bool mEnabled;

		private ContentManager mContent;

		private int mCurrentSong;

		private bool mFadeOut;

		private float maxVolume;

		private bool mHasPlayed;

		private float mFirstPlayVolume;
	}
}
