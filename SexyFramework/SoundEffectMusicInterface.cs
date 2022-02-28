using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Sexy
{
	public class SoundEffectMusicInterface : MusicInterface
	{
		public event SongChangedEventHandle mHandle;

		public void SongChanged(object sender, EventArgs e)
		{
		}

		public SoundEffectMusicInterface()
		{
			this.m_isUserMusicOn = !MediaPlayer.GameHasControl;
			MediaPlayer.MediaStateChanged += new EventHandler<EventArgs>(this.OnMediaPlayerStateChanged);
		}

		public override bool isPlayingUserMusic()
		{
			return !MediaPlayer.GameHasControl;
		}

		public override void stopUserMusic()
		{
			this.m_PauseByFunction = true;
			MediaPlayer.Pause();
			if (this.m_CurrSong != null)
			{
				this.m_CurrSong.play();
				MediaPlayer.IsRepeating = !this.m_CurrSong.mStopOnFade;
				MediaPlayer.Volume = Common.CaculatePowValume(this.m_MusicVolume);
			}
		}

		public override bool LoadMusic(int theSongId, string theFileName, ContentManager content)
		{
			Song s = null;
			try
			{
				s = content.Load<Song>(theFileName);
			}
			catch (Exception)
			{
				s = null;
				return false;
			}
			if (this.m_SoundDict.ContainsKey(theSongId))
			{
				this.m_SoundDict[theSongId].load(s);
			}
			else
			{
				this.m_SoundDict.Add(theSongId, new SoundEffectWrapper(s));
			}
			return true;
		}

		public override void UnloadAllMusic()
		{
			this.m_SoundDict.Clear();
		}

		public override void PlayMusic(int theSongId, int theOffset, bool noLoop, long theStartPos)
		{
			this.StopAllMusic();
			if (this.m_SoundDict.ContainsKey(theSongId))
			{
				this.m_CurrSongID = theSongId;
				this.m_CurrSong = this.m_SoundDict[theSongId];
				this.m_SoundDict[theSongId].mStopOnFade = noLoop;
				this.m_SoundDict[theSongId].mVolume = (this.m_SoundDict[theSongId].mVolumeCap = (double)this.m_MusicVolume);
				this.m_SoundDict[theSongId].mVolumeAdd = 0.0;
				if (MediaPlayer.GameHasControl)
				{
					this.m_SoundDict[theSongId].play();
					MediaPlayer.IsRepeating = !noLoop;
					MediaPlayer.Volume = Common.CaculatePowValume(this.m_MusicVolume);
				}
			}
		}

		public override void SetVolume(double theVolume)
		{
			this.m_MusicVolume = (float)theVolume;
			if (this.m_isUserMusicOn)
			{
				return;
			}
			MediaPlayer.Volume = Common.CaculatePowValume(this.m_MusicVolume);
		}

		public override void StopAllMusic()
		{
			if (this.m_isUserMusicOn)
			{
				return;
			}
			MediaPlayer.Stop();
			foreach (SoundEffectWrapper soundEffectWrapper in this.m_SoundDict.Values)
			{
				soundEffectWrapper.mVolumeAdd = 0.0;
				soundEffectWrapper.mVolume = 0.0;
				soundEffectWrapper.m_isPlaying = false;
			}
			this.m_CurrSong = null;
			this.m_CurrSongID = -1;
		}

		public override void PauseAllMusic()
		{
			if (this.m_isUserMusicOn || MediaPlayer.State == MediaState.Paused)
			{
				return;
			}
			if (this.m_CurrSong != null)
			{
				this.m_PauseByFunction = true;
				MediaPlayer.Pause();
			}
		}

		public override void ResumeAllMusic()
		{
			if (this.m_isUserMusicOn)
			{
				return;
			}
			if (this.m_PauseByFunction && this.m_CurrSong != null)
			{
				this.m_PauseByFunction = false;
				MediaQueue queue = MediaPlayer.Queue;
				Song activeSong = queue.ActiveSong;
				if (this.m_CurrSong.m_Song.Name != activeSong.Name)
				{
					this.m_CurrSong.play();
					MediaPlayer.IsRepeating = !this.m_CurrSong.mStopOnFade;
					MediaPlayer.Volume = Common.CaculatePowValume(this.m_MusicVolume);
					SongChangedEventArgs songChangedEventArgs = new SongChangedEventArgs();
					songChangedEventArgs.songID = this.m_CurrSongID;
					songChangedEventArgs.loop = !this.m_CurrSong.mStopOnFade;
					if (this.mHandle != null)
					{
						this.mHandle(this, songChangedEventArgs);
						return;
					}
				}
				else
				{
					MediaPlayer.Resume();
				}
			}
		}

		public override void FadeIn(int theSongId, int theOffset, double theSpeed, bool noLoop)
		{
			this.PlayMusic(theSongId, theOffset, noLoop);
		}

		public override void FadeOut(int theSongId, bool stopSong, double theSpeed)
		{
			this.StopAllMusic();
		}

		public override void FadeOutAll(bool stopSong, double theSpeed)
		{
			if (this.m_isUserMusicOn)
			{
				return;
			}
			this.StopAllMusic();
		}

		public override bool IsPlaying(int theSongId)
		{
			return !this.m_isUserMusicOn && this.m_SoundDict.ContainsKey(theSongId) && this.m_SoundDict[theSongId].isPlaying();
		}

		public override void Update()
		{
			if (this.mGameEvent == MusicInterface.EGameEvent.State_OnDeactive || this.mServiceEvent == MusicInterface.EServiceEvent.State_OnServiceDeactive)
			{
				return;
			}
			if ((this.mCurState == MusicInterface.EMusicInterfaceState.State_GameMusicStopedInGame || this.mCurState == MusicInterface.EMusicInterfaceState.State_UserMusicStoppedInGame) && this.m_onDeactive && !this.m_onServiceDeactive && this.mUpdateCount > 1)
			{
				this.mCurState = MusicInterface.EMusicInterfaceState.State_None;
				this.m_onDeactive = false;
				MediaPlayer.Resume();
				this.m_isUserMusicOn = !MediaPlayer.GameHasControl;
			}
			else if (this.mCurState == MusicInterface.EMusicInterfaceState.State_UserMusicStoppedInGame && !this.m_onDeactive && !this.m_onServiceDeactive && this.mUpdateCount > 100)
			{
				this.mCurState = MusicInterface.EMusicInterfaceState.State_None;
				this.mUpdateCount = 0;
				if (this.m_CurrSong == null || this.m_CurrSong.mStopOnFade)
				{
					return;
				}
				MediaQueue queue = MediaPlayer.Queue;
				Song activeSong = queue.ActiveSong;
				if (this.m_CurrSong.m_Song.Name != activeSong.Name)
				{
					this.m_CurrSong.play();
					MediaPlayer.IsRepeating = !this.m_CurrSong.mStopOnFade;
					MediaPlayer.Volume = Common.CaculatePowValume(this.m_MusicVolume);
					SongChangedEventArgs songChangedEventArgs = new SongChangedEventArgs();
					songChangedEventArgs.songID = this.m_CurrSongID;
					songChangedEventArgs.loop = !this.m_CurrSong.mStopOnFade;
					if (this.mHandle != null)
					{
						this.mHandle(this, songChangedEventArgs);
					}
				}
				else
				{
					MediaPlayer.Resume();
				}
				this.m_isUserMusicOn = !MediaPlayer.GameHasControl;
			}
			else if (this.mCurState == MusicInterface.EMusicInterfaceState.State_GameMusicStopedInGame && !this.m_onDeactive && !this.m_onServiceDeactive && this.mUpdateCount > 1)
			{
				this.mCurState = MusicInterface.EMusicInterfaceState.State_None;
				this.mUpdateCount = 0;
				if (this.m_CurrSong == null || this.m_CurrSong.mStopOnFade)
				{
					return;
				}
				MediaQueue queue2 = MediaPlayer.Queue;
				Song activeSong2 = queue2.ActiveSong;
				if (this.m_CurrSong.m_Song.Name != activeSong2.Name)
				{
					this.m_CurrSong.play();
					MediaPlayer.IsRepeating = !this.m_CurrSong.mStopOnFade;
					MediaPlayer.Volume = Common.CaculatePowValume(this.m_MusicVolume);
					SongChangedEventArgs songChangedEventArgs2 = new SongChangedEventArgs();
					songChangedEventArgs2.songID = this.m_CurrSongID;
					songChangedEventArgs2.loop = !this.m_CurrSong.mStopOnFade;
					if (this.mHandle != null)
					{
						this.mHandle(this, songChangedEventArgs2);
					}
				}
				else
				{
					MediaPlayer.Resume();
				}
				this.m_isUserMusicOn = !MediaPlayer.GameHasControl;
			}
			else if (this.mCurState == MusicInterface.EMusicInterfaceState.State_UserMusicStopedOutGame && this.m_isUserMusicOn && MediaPlayer.State != MediaState.Playing && this.m_onDeactive && this.m_onServiceDeactive && this.mUpdateCount > 1)
			{
				this.m_onDeactive = false;
				this.m_onServiceDeactive = false;
				this.mCurState = MusicInterface.EMusicInterfaceState.State_None;
				if (this.m_CurrSong == null || this.m_CurrSong.mStopOnFade)
				{
					return;
				}
				MediaQueue queue3 = MediaPlayer.Queue;
				Song activeSong3 = queue3.ActiveSong;
				if (this.m_CurrSong.m_Song.Name != activeSong3.Name)
				{
					this.m_CurrSong.play();
					MediaPlayer.IsRepeating = !this.m_CurrSong.mStopOnFade;
					MediaPlayer.Volume = Common.CaculatePowValume(this.m_MusicVolume);
					SongChangedEventArgs songChangedEventArgs3 = new SongChangedEventArgs();
					songChangedEventArgs3.songID = this.m_CurrSongID;
					songChangedEventArgs3.loop = !this.m_CurrSong.mStopOnFade;
					if (this.mHandle != null)
					{
						this.mHandle(this, songChangedEventArgs3);
					}
				}
				else
				{
					MediaPlayer.Resume();
				}
				this.m_isUserMusicOn = !MediaPlayer.GameHasControl;
			}
			else if (this.mCurState == MusicInterface.EMusicInterfaceState.State_None)
			{
				this.m_onServiceDeactive = false;
				this.m_onDeactive = false;
				this.mUpdateCount = 0;
			}
			else
			{
				this.mUpdateCount++;
			}
			bool isUserMusicOn = this.m_isUserMusicOn;
		}

		public override void OnDeactived()
		{
			this.m_onDeactive = true;
			this.mGameEvent = MusicInterface.EGameEvent.State_OnDeactive;
		}

		public override void OnActived()
		{
			if (!MediaPlayer.GameHasControl)
			{
				this.m_isUserMusicOn = true;
				this.mCurState = MusicInterface.EMusicInterfaceState.State_None;
				this.m_onDeactive = false;
			}
			this.mActiveCount = 1;
			this.mGameEvent = MusicInterface.EGameEvent.State_OnActived;
		}

		public override void OnServiceDeactived()
		{
			this.m_onServiceDeactive = true;
			this.m_onServiceActive = false;
			this.mServiceEvent = MusicInterface.EServiceEvent.State_OnServiceDeactive;
		}

		public override void OnServiceActived()
		{
			this.m_onServiceActive = true;
			this.mServiceEvent = MusicInterface.EServiceEvent.State_OnServiceActived;
			if (this.mActiveCount == 1 && this.m_isUserMusicOn && MediaPlayer.State != MediaState.Playing)
			{
				this.mCurState = MusicInterface.EMusicInterfaceState.State_UserMusicStopedOutGame;
			}
			this.mActiveCount = 0;
		}

		public override void RegisterCallback(SongChangedEventHandle handle)
		{
			this.mHandle += handle;
		}

		public override void OnMediaPlayerStateChanged(object sender, EventArgs e)
		{
			MediaState state = MediaPlayer.State;
			if (this.m_isUserMusicOn && state == MediaState.Paused)
			{
				this.mCurState = MusicInterface.EMusicInterfaceState.State_UserMusicStoppedInGame;
				this.mUpdateCount = 0;
			}
			else if (state == MediaState.Paused)
			{
				this.mCurState = MusicInterface.EMusicInterfaceState.State_GameMusicStopedInGame;
				this.mUpdateCount = 0;
			}
			else
			{
				this.mCurState = MusicInterface.EMusicInterfaceState.State_None;
			}
			this.m_isUserMusicOn = !MediaPlayer.GameHasControl;
		}

		protected Dictionary<int, SoundEffectWrapper> m_SoundDict = new Dictionary<int, SoundEffectWrapper>();

		private SoundEffectWrapper m_CurrSong;

		private int m_CurrSongID = -1;

		private bool m_PauseByFunction;

		protected bool m_onDeactive;

		protected bool m_onServiceDeactive;

		protected bool m_onServiceActive;

		public int mUpdateCount;

		public int mActiveCount;
	}
}
