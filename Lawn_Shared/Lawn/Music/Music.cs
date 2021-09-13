using System;
using Sexy;

namespace Lawn
{
	public/*internal*/ class Music
	{
		public Music()
		{
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mMusicInterface = this.mApp.mMusicInterface;
			this.mPaused = false;
			this.mMusicDisabled = false;
			this.mCurMusicTune = MusicTune.MUSIC_TUNE_NONE;
		}

		public void MusicTitleScreenInit()
		{
			this.LoadSong(MusicTune.MUSIC_TUNE_TITLE_CRAZY_DAVE_MAIN_THEME, "crazydave");
			this.PlayMusic(MusicTune.MUSIC_TUNE_TITLE_CRAZY_DAVE_MAIN_THEME);
		}

		public void MusicInit()
		{
			this.LoadSong(MusicTune.MUSIC_TUNE_DAY_GRASSWALK, "day");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_NIGHT_MOONGRAINS, "night");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_POOL_WATERYGRAVES, "pool");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_FOG_RIGORMORMIST, "fog");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_ROOF_GRAZETHEROOF, "roof");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_CHOOSE_YOUR_SEEDS, "chooseyourseeds");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_FINAL_BOSS_BRAINIAC_MANIAC, "boss");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_PUZZLE_CEREBRAWL, "cerebrawl");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_MINIGAME_LOONBOON, "loonboon");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_CONVEYER, "conveyor");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
			this.LoadSong(MusicTune.MUSIC_TUNE_ZEN_GARDEN, "zengarden");
			this.mApp.mCompletedLoadingThreadTasks += 3500;
		}

		public void StopAllMusic()
		{
			this.mMusicInterface.StopMusic(0f);
			this.mCurMusicTune = MusicTune.MUSIC_TUNE_NONE;
			this.mPaused = false;
		}

		public void StartGameMusic()
		{
			Debug.ASSERT(this.mApp.mBoard != null);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_ZEN_GARDEN);
				return;
			}
			if (this.mApp.IsFinalBossLevel())
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_FINAL_BOSS_BRAINIAC_MANIAC);
				return;
			}
			if (this.mApp.IsWallnutBowlingLevel() || this.mApp.IsWhackAZombieLevel() || this.mApp.IsLittleTroubleLevel() || this.mApp.IsBungeeBlitzLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SPEED)
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_MINIGAME_LOONBOON);
				return;
			}
			if ((this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()) && (this.mApp.mPlayerInfo.mLevel == 10 || this.mApp.mPlayerInfo.mLevel == 20 || this.mApp.mPlayerInfo.mLevel == 30))
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_CONVEYER);
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_CONVEYER);
				return;
			}
			if (this.mApp.IsStormyNightLevel())
			{
				this.StopAllMusic();
				return;
			}
			if (this.mApp.IsScaryPotterLevel() || this.mApp.IsIZombieLevel())
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_PUZZLE_CEREBRAWL);
				return;
			}
			if (this.mApp.mBoard.mBackground == BackgroundType.BACKGROUND_4_FOG)
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_FOG_RIGORMORMIST);
				return;
			}
			if (this.mApp.mBoard.StageIsNight())
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_NIGHT_MOONGRAINS);
				return;
			}
			if (this.mApp.mBoard.StageHasPool())
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_POOL_WATERYGRAVES);
				return;
			}
			if (this.mApp.mBoard.StageHasRoof())
			{
				this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_ROOF_GRAZETHEROOF);
				return;
			}
			this.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_DAY_GRASSWALK);
		}

		public void GameMusicPause(bool thePause)
		{
			if (thePause && !this.mPaused && this.mCurMusicTune != MusicTune.MUSIC_TUNE_NONE)
			{
				if (this.mMusicInterface.IsPlaying((int)this.mCurMusicTune))
				{
					this.mMusicInterface.PauseMusic();
				}
				this.mPaused = true;
				return;
			}
			if (!thePause && this.mPaused)
			{
				if (this.mCurMusicTune != MusicTune.MUSIC_TUNE_NONE)
				{
					if (this.mMusicInterface.IsPlaying((int)this.mCurMusicTune))
					{
						this.mMusicInterface.ResumeMusic();
					}
					else
					{
						this.PlayMusic(this.mCurMusicTune);
					}
				}
				this.mPaused = false;
			}
		}

		public void MakeSureMusicIsPlaying(MusicTune theMusicTune)
		{
			if (this.mCurMusicTune == theMusicTune)
			{
				return;
			}
			this.StopAllMusic();
			this.PlayMusic(theMusicTune);
		}

		public void FadeOut(int aFadeOutDuration)
		{
			if (this.mCurMusicTune != MusicTune.MUSIC_TUNE_NONE)
			{
				this.mMusicInterface.StopMusic((float)aFadeOutDuration / 100f);
				this.mCurMusicTune = MusicTune.MUSIC_TUNE_NONE;
				this.mPaused = false;
			}
		}

		public int GetNumLoadingTasks()
		{
			return 35000;
		}

		private void LoadSong(MusicTune theMusicTune, string theFileName)
		{
			if (!this.mMusicInterface.LoadMusic((int)theMusicTune, GlobalStaticVars.GetResourceDir() + "music/" + theFileName))
			{
				Console.Write("Failed to load music file {0}\n", theFileName);
			}
		}

		private void MusicDispose()
		{
			this.StopAllMusic();
			this.mMusicInterface.UnloadAllMusic();
		}

		private void PlayMusic(MusicTune theMusicTune)
		{
			if (this.mMusicDisabled)
			{
				return;
			}
			this.mCurMusicTune = theMusicTune;
			this.mMusicInterface.PlayMusic((int)theMusicTune, true);
		}

		public LawnApp mApp;

		public MusicInterface mMusicInterface;

		public MusicTune mCurMusicTune;

		private bool mPaused;

		private bool mMusicDisabled;
	}
}
