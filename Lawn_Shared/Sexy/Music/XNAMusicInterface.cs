using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Sexy
{
	public class XNAMusicInterfaceConstants
	{
		public static int MAX_SONGS = 13;
	}

	internal class XNAMusicInterface : MusicInterface
	{
		public XNAMusicInterface(SexyAppBase theApp)
		{
			MusicInterface.USER_MUSIC_PLAYING = !MediaPlayer.GameHasControl;
			mEnabled = false;
			mContent = theApp.mContentManager;
			mCurrentSong = -1;
			mFadeOut = false;
			for (int i = 0; i < XNAMusicInterfaceConstants.MAX_SONGS; i++)
			{
				mSongs[i] = null;
			}
		}

		public override void Dispose()
		{
			UnloadAllMusic();
			base.Dispose();
		}

		public override void Enable(bool enable)
		{
			mEnabled = enable;
		}

		public override bool LoadMusic(int theSongId, string theFileName)
		{
			mSongs[theSongId] = mContent.Load<Song>(theFileName);
			return true;
		}

		public override void PlayMusic(int theSongid, int offset, float fadeOutSeconds, float fadeinSeconds, bool loop)
		{
			try
			{
				if (!MusicInterface.USER_MUSIC_PLAYING)
				{
					if (theSongid != mCurrentSong || MediaPlayer.State != MediaState.Playing)
					{
						isStopped = false;
						if (theSongid >= 0)
						{
							mCurrentSong = theSongid;
							Song song = mSongs[theSongid];
							if (!(song == null))
							{
								MediaPlayer.Play(song);
								if (!mHasPlayed)
								{
									mHasPlayed = true;
									SetVolume(mFirstPlayVolume);
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
			mCurrentSong = -1;
			try
			{
				MediaPlayer.Stop();
				isStopped = true;
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
			isStopped = false;
			if (MusicInterface.USER_MUSIC_PLAYING)
			{
				return;
			}
			try
			{
				if (!isStopped)
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
			if (theSongId == mCurrentSong)
			{
				try
				{
					StopMusic();
				}
				catch (Exception)
				{
				}
			}
			if (mSongs[theSongId] != null)
			{
				mSongs[theSongId].Dispose();
				mSongs[theSongId] = null;
			}
		}

		public override void UnloadAllMusic()
		{
			try
			{
				StopMusic();
			}
			catch (Exception)
			{
			}
			for (int i = 0; i < XNAMusicInterfaceConstants.MAX_SONGS; i++)
			{
				UnloadMusic(i);
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
			return mCurrentSong == theSongId && flag;
		}

		public override void SetVolume(float theVolume)
		{
			if (!mHasPlayed)
			{
				mFirstPlayVolume = theVolume;
				return;
			}
			try
			{
				maxVolume = (MediaPlayer.Volume = theVolume * 0.5f);
			}
			catch (Exception)
			{
			}
		}

		public override float GetVolume()
		{
			return maxVolume * 2f;
		}

		public override void Update()
		{
			try
			{
				if (mFadeOut)
				{
					StopMusic(0f);
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
				if (mSongs[i] == null)
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
