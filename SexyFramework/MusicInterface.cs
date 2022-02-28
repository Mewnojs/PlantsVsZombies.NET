using System;
using Microsoft.Xna.Framework.Content;

namespace Sexy
{
	public class MusicInterface
	{
		public virtual void Dispose()
		{
		}

		public virtual bool LoadMusic(int theSongId, string theFileName, ContentManager content)
		{
			return false;
		}

		public virtual void PlayMusic(int theSongId, int theOffset, bool noLoop)
		{
			this.PlayMusic(theSongId, theOffset, noLoop, 0L);
		}

		public virtual void PlayMusic(int theSongId, int theOffset)
		{
			this.PlayMusic(theSongId, theOffset, false, 0L);
		}

		public virtual void PlayMusic(int theSongId)
		{
			this.PlayMusic(theSongId, 0, false, 0L);
		}

		public virtual void PlayMusic(int theSongId, int theOffset, bool inLoop, long theStartPos)
		{
		}

		public virtual void StopMusic(int theSongId)
		{
		}

		public virtual void PauseMusic(int theSongId)
		{
		}

		public virtual void ResumeMusic(int theSongId)
		{
		}

		public virtual void StopAllMusic()
		{
		}

		public virtual void UnloadMusic(int theSongId)
		{
		}

		public virtual void UnloadAllMusic()
		{
		}

		public virtual void PauseAllMusic()
		{
		}

		public virtual void ResumeAllMusic()
		{
		}

		public virtual void OnMediaPlayerStateChanged(object sender, EventArgs e)
		{
		}

		public virtual void FadeIn(int theSongId, int theOffset, double theSpeed)
		{
			this.FadeIn(theSongId, theOffset, theSpeed, false);
		}

		public virtual void FadeIn(int theSongId, int theOffset)
		{
			this.FadeIn(theSongId, theOffset, 0.002, false);
		}

		public virtual void FadeIn(int theSongId)
		{
			this.FadeIn(theSongId, -1, 0.002, false);
		}

		public virtual void FadeIn(int theSongId, int theOffset, double theSpeed, bool noLoop)
		{
		}

		public virtual void FadeOut(int theSongId, bool stopSong)
		{
			this.FadeOut(theSongId, stopSong, 0.004);
		}

		public virtual void FadeOut(int theSongId)
		{
			this.FadeOut(theSongId, true, 0.004);
		}

		public virtual void FadeOut(int theSongId, bool stopSong, double theSpeed)
		{
		}

		public virtual void FadeOutAll(bool stopSong)
		{
			this.FadeOutAll(stopSong, 0.004);
		}

		public virtual void FadeOutAll()
		{
			this.FadeOutAll(true, 0.004);
		}

		public virtual void FadeOutAll(bool stopSong, double theSpeed)
		{
		}

		public virtual void SetSongVolume(int theSongId, double theVolume)
		{
		}

		public virtual void SetSongMaxVolume(int theSongId, double theMaxVolume)
		{
		}

		public virtual bool IsPlaying(int theSongId)
		{
			return false;
		}

		public virtual void SetVolume(double theVolume)
		{
		}

		public virtual void SetMusicAmplify(int theSongId, double theAmp)
		{
		}

		public virtual void Update()
		{
		}

		public virtual void OnDeactived()
		{
		}

		public virtual void OnActived()
		{
		}

		public virtual void OnServiceDeactived()
		{
		}

		public virtual void OnServiceActived()
		{
		}

		public virtual void RegisterCallback(SongChangedEventHandle handle)
		{
		}

		public virtual bool isPlayingUserMusic()
		{
			return false;
		}

		public virtual void stopUserMusic()
		{
		}

		public virtual int GetMusicTempo(int theSongId)
		{
			return -1;
		}

		public virtual void SetMusicTempo(int theSongId, int theTempo)
		{
		}

		public virtual int GetMusicOrder(int theSongId)
		{
			return -1;
		}

		public virtual void SetMusicOrder(int theSongId, int theOrder)
		{
		}

		public virtual int GetMusicRow(int theSongId)
		{
			return -1;
		}

		public virtual void SetMusicRow(int theSongId, int theOrder)
		{
		}

		public virtual int GetMusicChannelVolume(int theSongId, int theChannelId)
		{
			return -1;
		}

		public virtual void SetMusicChannelVolume(int theSongId, int theChannelId, int theVolume)
		{
		}

		public virtual void OnMusicPosCallback(int theMusicId, int theOrder, int theRow)
		{
		}

		public const float MUSIC_FADE_SPEED = 0.005f;

		public const int MUSIC_TRACK_1 = 1;

		public const int MUSIC_TRACK_2 = 2;

		public MusicInterface.EMusicInterfaceState mCurState = MusicInterface.EMusicInterfaceState.State_None;

		public MusicInterface.EGameEvent mGameEvent = MusicInterface.EGameEvent.State_No;

		public MusicInterface.EServiceEvent mServiceEvent = MusicInterface.EServiceEvent.State_ServiceNo;

		protected float m_MusicVolume = 1f;

		public bool m_isUserMusicOn;

		public bool m_isInterruptByOtherSound;

		public enum EMusicInterfaceState
		{
			State_PlayingUserMusic,
			State_UserMusicStoppedInGame,
			State_UserMusicStopedOutGame,
			State_GameMusicStopedInGame,
			State_PlayingGameMusic,
			State_None
		}

		public enum EGameEvent
		{
			State_OnActived,
			State_OnDeactive,
			State_No
		}

		public enum EServiceEvent
		{
			State_OnServiceActived,
			State_OnServiceDeactive,
			State_ServiceNo
		}
	}
}
