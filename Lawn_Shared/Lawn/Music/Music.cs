using System;
using Sexy;

namespace Lawn
{
	public/*internal*/ class Music
	{
		public Music()
		{
			mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			mMusicInterface = mApp.mMusicInterface;
			mPaused = false;
			mMusicDisabled = false;
			mCurMusicTune = MusicTune.None;
		}

		public void MusicTitleScreenInit()
		{
			LoadSong(MusicTune.TitleCrazyDaveMainTheme, "crazydave");
			PlayMusic(MusicTune.TitleCrazyDaveMainTheme);
		}

		public void MusicInit()
		{
			LoadSong(MusicTune.DayGrasswalk, "day");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.NightMoongrains, "night");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.PoolWaterygraves, "pool");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.FogRigormormist, "fog");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.RoofGrazetheroof, "roof");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.ChooseYourSeeds, "chooseyourseeds");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.FinalBossBrainiacManiac, "boss");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.PuzzleCerebrawl, "cerebrawl");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.MinigameLoonboon, "loonboon");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.Conveyer, "conveyor");
			mApp.mCompletedLoadingThreadTasks += 3500;
			LoadSong(MusicTune.ZenGarden, "zengarden");
			mApp.mCompletedLoadingThreadTasks += 3500;
		}

		public void StopAllMusic()
		{
			mMusicInterface.StopMusic(0f);
			mCurMusicTune = MusicTune.None;
			mPaused = false;
		}

		public void StartGameMusic()
		{
			Debug.ASSERT(mApp.mBoard != null);
			if (mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom)
			{
				MakeSureMusicIsPlaying(MusicTune.ZenGarden);
				return;
			}
			if (mApp.IsFinalBossLevel())
			{
				MakeSureMusicIsPlaying(MusicTune.FinalBossBrainiacManiac);
				return;
			}
			if (mApp.IsWallnutBowlingLevel() || mApp.IsWhackAZombieLevel() || mApp.IsLittleTroubleLevel() || mApp.IsBungeeBlitzLevel() || mApp.mGameMode == GameMode.ChallengeSpeed)
			{
				MakeSureMusicIsPlaying(MusicTune.MinigameLoonboon);
				return;
			}
			if ((mApp.IsAdventureMode() || mApp.IsQuickPlayMode()) && (mApp.mPlayerInfo.mLevel == 10 || mApp.mPlayerInfo.mLevel == 20 || mApp.mPlayerInfo.mLevel == 30))
			{
				MakeSureMusicIsPlaying(MusicTune.Conveyer);
				return;
			}
			if (mApp.mGameMode == GameMode.ChallengeColumn)
			{
				MakeSureMusicIsPlaying(MusicTune.Conveyer);
				return;
			}
			if (mApp.IsStormyNightLevel())
			{
				StopAllMusic();
				return;
			}
			if (mApp.IsScaryPotterLevel() || mApp.IsIZombieLevel())
			{
				MakeSureMusicIsPlaying(MusicTune.PuzzleCerebrawl);
				return;
			}
			if (mApp.mBoard.mBackground == BackgroundType.Num4Fog)
			{
				MakeSureMusicIsPlaying(MusicTune.FogRigormormist);
				return;
			}
			if (mApp.mBoard.StageIsNight())
			{
				MakeSureMusicIsPlaying(MusicTune.NightMoongrains);
				return;
			}
			if (mApp.mBoard.StageHasPool())
			{
				MakeSureMusicIsPlaying(MusicTune.PoolWaterygraves);
				return;
			}
			if (mApp.mBoard.StageHasRoof())
			{
				MakeSureMusicIsPlaying(MusicTune.RoofGrazetheroof);
				return;
			}
			MakeSureMusicIsPlaying(MusicTune.DayGrasswalk);
		}

		public void GameMusicPause(bool thePause)
		{
			if (thePause && !mPaused && mCurMusicTune != MusicTune.None)
			{
				if (mMusicInterface.IsPlaying((int)mCurMusicTune))
				{
					mMusicInterface.PauseMusic();
				}
				mPaused = true;
				return;
			}
			if (!thePause && mPaused)
			{
				if (mCurMusicTune != MusicTune.None)
				{
					if (mMusicInterface.IsPlaying((int)mCurMusicTune))
					{
						mMusicInterface.ResumeMusic();
					}
					else
					{
						PlayMusic(mCurMusicTune);
					}
				}
				mPaused = false;
			}
		}

		public void MakeSureMusicIsPlaying(MusicTune theMusicTune)
		{
			if (mCurMusicTune == theMusicTune)
			{
				return;
			}
			StopAllMusic();
			PlayMusic(theMusicTune);
		}

		public void FadeOut(int aFadeOutDuration)
		{
			if (mCurMusicTune != MusicTune.None)
			{
				mMusicInterface.StopMusic(aFadeOutDuration / 100f);
				mCurMusicTune = MusicTune.None;
				mPaused = false;
			}
		}

		public int GetNumLoadingTasks()
		{
			return 35000;
		}

		private void LoadSong(MusicTune theMusicTune, string theFileName)
		{
			if (!mMusicInterface.LoadMusic((int)theMusicTune, GlobalStaticVars.GetResourceDir() + "music/" + theFileName))
			{
				Console.Write("Failed to load music file {0}\n", theFileName);
			}
		}

		private void MusicDispose()
		{
			StopAllMusic();
			mMusicInterface.UnloadAllMusic();
		}

		private void PlayMusic(MusicTune theMusicTune)
		{
			if (mMusicDisabled)
			{
				return;
			}
			mCurMusicTune = theMusicTune;
			mMusicInterface.PlayMusic((int)theMusicTune, true);
		}

		public LawnApp mApp;

		public MusicInterface mMusicInterface;

		public MusicTune mCurMusicTune;

		private bool mPaused;

		private bool mMusicDisabled;
	}
}
