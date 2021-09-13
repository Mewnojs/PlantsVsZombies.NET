using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class CutScene
	{
		static CutScene()
		{
			for (int i = 0; i < CutScene.aPicks.Length; i++)
			{
				CutScene.aPicks[i] = TodWeightedGridArray.GetNewTodWeightedGridArray();
			}
		}

		public CutScene()
		{
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mBoard = this.mApp.mBoard;
			this.mCutsceneTime = 0;
			this.mSodTime = 0;
			this.mFogTime = 0;
			this.mBossTime = 0;
			this.mCrazyDaveTime = 0;
			this.mGraveStoneTime = 0;
			this.mReadySetPlantTime = 0;
			this.mLawnMowerTime = 0;
			this.mCrazyDaveDialogStart = -1;
			this.mSeedChoosing = false;
			this.mZombiesWonReanimID = null;
			this.mPreloaded = false;
			this.mPlacedZombies = false;
			this.mPlacedLawnItems = false;
			this.mCrazyDaveCountDown = 0;
			this.mCrazyDaveLastTalkIndex = -1;
			this.mUpsellHideBoard = false;
			this.mUpsellChallengeScreen = null;
			this.mPreUpdatingBoard = false;
		}

		public void Dispose()
		{
			this.mApp.mMuteSoundsForCutscene = false;
		}

		public void StartLevelIntro()
		{
			this.mCutsceneTime = 0;
			this.mBoard.mSeedBank.Move(-this.mBoard.mSeedBank.mWidth, 0);
			this.mBoard.mMenuButton.mBtnNoDraw = true;
			this.mApp.mSeedChooserScreen.mMouseVisible = false;
			this.mApp.mSeedChooserScreen.Move(0, Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET);
			this.mApp.mSeedChooserScreen.mMenuButton.mBtnNoDraw = true;
			this.mBoard.mShowShovel = false;
			this.mBoard.mSeedBank.mCutSceneDarken = 255;
			this.mPlacedZombies = false;
			this.mPreloaded = false;
			this.mPlacedLawnItems = false;
			this.mApp.mWidgetManager.SetFocus(this.mBoard);
			bool flag = false;
			if (!this.mApp.IsFirstTimeAdventureMode())
			{
				flag = false;
			}
			else if (this.mBoard.mLevel == 1 || this.mBoard.mLevel == 2 || this.mBoard.mLevel == 4)
			{
				flag = true;
			}
			if (flag)
			{
				this.mSodTime = CutScene.TimeRollSodEnd - CutScene.TimeRollSodStart;
				this.mBoard.mSodPosition = 0;
			}
			else
			{
				this.mSodTime = 0;
				this.mBoard.mSodPosition = 1000;
			}
			this.mGraveStoneTime = 0;
			this.mBoard.mEnableGraveStones = false;
			if (this.mBoard.StageHasGraveStones())
			{
				if ((this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()) && this.mApp.IsWhackAZombieLevel())
				{
					this.mGraveStoneTime = 0;
				}
				else if (!this.IsSurvivalRepick())
				{
					this.mGraveStoneTime = CutScene.TimeGraveStoneEnd - CutScene.TimeGraveStoneStart;
				}
			}
			if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel <= 2)
			{
				this.mReadySetPlantTime = 0;
			}
			else if (this.mApp.IsShovelLevel() || this.mApp.IsSquirrelLevel() || this.mApp.IsWallnutBowlingLevel() || this.mApp.IsIZombieLevel() || this.mApp.IsWhackAZombieLevel() || this.mApp.IsScaryPotterLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				this.mReadySetPlantTime = 0;
			}
			else
			{
				this.mReadySetPlantTime = CutScene.TimeReadySetPlantEnd - CutScene.TimeReadySetPlantStart;
			}
			this.mLawnMowerTime = 0;
			if (!this.IsSurvivalRepick())
			{
				this.mLawnMowerTime = 550;
			}
			bool flag2 = false;
			if (this.mBoard.mPrevBoardResult == BoardResult.BOARDRESULT_RESTART || this.mBoard.mPrevBoardResult == BoardResult.BOARDRESULT_LOST)
			{
				flag2 = true;
			}
			if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 11)
			{
				this.mCrazyDaveDialogStart = 201;
			}
			else if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 12)
			{
				this.mCrazyDaveDialogStart = 1401;
			}
			else if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel >= 13 && this.mBoard.mLevel <= 24 && this.mBoard.mLevel != 15 && this.mBoard.mLevel != 20 && this.mBoard.mLevel != 21 && this.CanGetPacketUpgrade())
			{
				this.mCrazyDaveDialogStart = 1501;
			}
			else if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel >= 16 && this.mBoard.mLevel <= 24 && this.mBoard.mLevel != 20 && this.mBoard.mLevel != 21 && this.CanGetSecondPacketUpgrade())
			{
				this.mCrazyDaveDialogStart = 1551;
			}
			else if (this.mApp.IsWallnutBowlingLevel() && this.mApp.IsAdventureMode())
			{
				if (this.mApp.IsFirstTimeAdventureMode())
				{
					this.mCrazyDaveDialogStart = 2400;
				}
				else
				{
					this.mCrazyDaveDialogStart = 2411;
					this.mBoard.mChallenge.mShowBowlingLine = true;
				}
				this.mBoard.mShowShovel = true;
			}
			else if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 21)
			{
				this.mCrazyDaveDialogStart = 501;
			}
			else if (this.mApp.IsWhackAZombieLevel() && this.mApp.IsAdventureMode())
			{
				this.mCrazyDaveDialogStart = 401;
			}
			else if (this.mApp.IsLittleTroubleLevel() && this.mApp.IsAdventureMode())
			{
				this.mCrazyDaveDialogStart = 701;
			}
			else if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 31)
			{
				this.mCrazyDaveDialogStart = 801;
			}
			else if (this.mApp.IsScaryPotterLevel() && this.mApp.IsAdventureMode())
			{
				this.mCrazyDaveDialogStart = 2500;
			}
			else if (this.mApp.IsStormyNightLevel() && this.mApp.IsAdventureMode())
			{
				this.mCrazyDaveDialogStart = 1101;
			}
			else if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 41)
			{
				this.mCrazyDaveDialogStart = 1201;
			}
			else if (this.mApp.IsBungeeBlitzLevel() && this.mApp.IsAdventureMode())
			{
				this.mCrazyDaveDialogStart = 1304;
			}
			else if (!this.mApp.IsFirstTimeAdventureMode() && !this.mApp.IsQuickPlayMode() && this.mBoard.mLevel == 1)
			{
				this.mCrazyDaveDialogStart = 1601;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1)
			{
				this.mCrazyDaveDialogStart = 2200;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
			{
				this.mCrazyDaveDialogStart = 3300;
				this.mUpsellHideBoard = true;
				this.mBoard.mMenuButton.mBtnNoDraw = false;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_1 && !this.mApp.HasBeatenChallenge(GameMode.GAMEMODE_SCARY_POTTER_1))
			{
				this.mCrazyDaveDialogStart = 3000;
			}
			else if (this.mApp.IsFinalBossLevel() && this.mApp.IsAdventureMode() && !flag2)
			{
				this.mCrazyDaveDialogStart = 2300;
			}
			if (this.mCrazyDaveDialogStart != -1)
			{
				this.mCrazyDaveTime = CutScene.TimeEarlyDaveLeaveEnd - CutScene.TimePanRightStart;
				if (this.mApp.IsFinalBossLevel() && this.mApp.IsAdventureMode())
				{
					this.mCrazyDaveTime += 4000;
				}
			}
			if (this.mBoard.StageHasFog())
			{
				this.mFogTime = CutScene.TimeFogRollIn + 2000 - CutScene.TimeReadySetPlantStart - this.mLawnMowerTime - this.mSodTime;
			}
			else
			{
				this.mFogTime = 0;
			}
			if (this.mApp.IsFinalBossLevel())
			{
				this.mBossTime = 4000;
			}
			else
			{
				this.mBossTime = 0;
			}
			if (this.IsScrolledLeftAtStart())
			{
				this.mBoard.Move((int)((float)Constants.BOARD_OFFSET * Constants.S), 0);
			}
			if (this.IsNonScrollingCutscene() && this.mCrazyDaveTime == 0)
			{
				this.CancelIntro();
				return;
			}
			if (this.mApp.IsFinalBossLevel() || this.mApp.IsScaryPotterLevel() || this.mApp.IsWallnutBowlingLevel())
			{
				this.PreloadResources();
				this.PlaceLawnItems();
			}
			string text = string.Empty;
			if (this.mCrazyDaveTime <= 0 && this.mApp.mGameMode != GameMode.GAMEMODE_INTRO && (this.mApp.mGameMode == GameMode.GAMEMODE_ADVENTURE || this.mApp.IsQuickPlayMode()))
			{
				if (this.mBoard.mBackground == BackgroundType.BACKGROUND_1_DAY || this.mBoard.mBackground == BackgroundType.BACKGROUND_2_NIGHT)
				{
					text = TodStringFile.TodStringTranslate("[PLAYERS_HOUSE]");
				}
				else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_3_POOL || this.mBoard.mBackground == BackgroundType.BACKGROUND_4_FOG)
				{
					text = TodStringFile.TodStringTranslate("[PLAYERS_BACKYARD]");
				}
				else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_5_ROOF || this.mBoard.mBackground == BackgroundType.BACKGROUND_6_BOSS)
				{
					text = TodStringFile.TodStringTranslate("[PLAYERS_ROOF]");
				}
				else
				{
					Debug.ASSERT(false);
				}
			}
			text = TodCommon.TodReplaceString(text, "{PLAYER}", this.mApp.mPlayerInfo.mName);
			if (!text.empty())
			{
				this.mBoard.DisplayAdvice(text, MessageStyle.MESSAGE_STYLE_HOUSE_NAME, AdviceType.ADVICE_NONE);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
			{
				this.mApp.mMusic.StopAllMusic();
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_INTRO)
			{
				this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_POOL_WATERYGRAVES);
				return;
			}
			if (this.mCrazyDaveTime > 0)
			{
				this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_TITLE_CRAZY_DAVE_MAIN_THEME);
				return;
			}
			if (this.mApp.IsFinalBossLevel())
			{
				this.mApp.mMusic.StopAllMusic();
				return;
			}
			this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_CHOOSE_YOUR_SEEDS);
		}

		public void CancelIntro()
		{
			this.PreloadResources();
			this.PlaceStreetZombies();
			if (this.mCutsceneTime < CutScene.TimePanRightEnd + this.mCrazyDaveTime)
			{
				this.mCutsceneTime = CutScene.TimeSeedChoserSlideOnEnd + this.mCrazyDaveTime - 20;
				if (!this.IsNonScrollingCutscene())
				{
					this.mBoard.Move(Constants.BOARD_OFFSET - Constants.BACKGROUND_IMAGE_WIDTH + Constants.WIDE_BOARD_WIDTH, 0);
				}
				if (this.mBoard.mAdvice.mMessageStyle == MessageStyle.MESSAGE_STYLE_HOUSE_NAME)
				{
					this.mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
				}
				if (this.mCrazyDaveDialogStart != -1)
				{
					if (this.mApp.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_OFF)
					{
						this.mApp.CrazyDaveEnter();
					}
					this.mApp.mCrazyDaveMessageIndex = this.mCrazyDaveDialogStart;
				}
				while (this.mApp.mCrazyDaveMessageIndex != -1)
				{
					this.AdvanceCrazyDaveDialog(true);
				}
				if (this.mBoard.mLevel == 5)
				{
					int count = this.mBoard.mPlants.Count;
					for (int i = 0; i < count; i++)
					{
						Plant plant = this.mBoard.mPlants[i];
						if (!plant.mDead)
						{
							plant.Die();
						}
					}
					this.mBoard.mChallenge.mShowBowlingLine = true;
				}
			}
			this.mApp.CrazyDaveDie();
			if (this.mCutsceneTime > CutScene.TimePanLeftStart + this.mCrazyDaveTime || !this.mBoard.ChooseSeedsOnCurrentLevel())
			{
				this.mCutsceneTime = CutScene.TimeIntroEnd + this.mLawnMowerTime + this.mSodTime + this.mGraveStoneTime + this.mCrazyDaveTime + this.mFogTime + this.mBossTime + this.mReadySetPlantTime - 20;
				this.PlaceLawnItems();
				if (this.mApp.IsStormyNightLevel())
				{
					this.mBoard.mChallenge.mChallengeStateCounter = 0;
				}
				if (this.mApp.IsFinalBossLevel())
				{
					this.mBoard.mChallenge.PlayBossEnter();
				}
				if (!this.mApp.IsChallengeWithoutSeedBank())
				{
					this.mBoard.mSeedBank.Move(0, 0);
				}
				this.mBoard.mEnableGraveStones = true;
				this.ShowShovel();
				if (this.mApp.IsFinalBossLevel())
				{
					this.mApp.mMusic.StartGameMusic();
				}
				if (this.mBoard.mFogBlownCountDown > 0)
				{
					this.mBoard.mFogBlownCountDown = 0;
					this.mBoard.mFogOffset = 0f;
				}
				if (this.mBoard.mTutorialState != TutorialState.TUTORIAL_ZEN_GARDEN_PICKUP_WATER)
				{
					this.mBoard.mMenuButton.mBtnNoDraw = false;
				}
				this.mApp.mSoundSystem.StopFoley(FoleyType.FOLEY_DIGGER);
			}
		}

		public void Update(bool updateDave)
		{
			if (this.mPreUpdatingBoard)
			{
				return;
			}
			if (this.IsShowingCrazyDave() && this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO && (!this.mBoard.mPaused || this.mApp.mGameMode != GameMode.GAMEMODE_UPSELL) && updateDave)
			{
				this.mApp.UpdateCrazyDave();
			}
			if (this.mBoard.mPaused)
			{
				return;
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
			{
				this.mCutsceneTime += 10;
				this.UpdateZombiesWon();
				return;
			}
			if (this.mApp.mGameScene != GameScenes.SCENE_LEVEL_INTRO)
			{
				return;
			}
			if (this.mBoard.mDrawCount == 0)
			{
				return;
			}
			if (!this.mPreloaded)
			{
				this.PreloadResources();
			}
			if (!this.mPlacedZombies)
			{
				this.PlaceStreetZombies();
			}
			if (this.IsNonScrollingCutscene() || !this.mBoard.ChooseSeedsOnCurrentLevel())
			{
				this.PlaceLawnItems();
			}
			bool flag = false;
			if (this.mSeedChoosing)
			{
				flag = true;
			}
			else if (this.mApp.mCrazyDaveMessageIndex != -1)
			{
				flag = true;
			}
			else if (this.IsInShovelTutorial())
			{
				flag = true;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
			{
				this.UpdateUpsell();
				if (this.mApp.mCrazyDaveState != CrazyDaveState.CRAZY_DAVE_OFF && this.mApp.mCrazyDaveState != CrazyDaveState.CRAZY_DAVE_ENTERING)
				{
					flag = true;
				}
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_INTRO)
			{
				this.mCutsceneTime += 10;
				this.UpdateIntro();
				return;
			}
			if (!flag)
			{
				this.mCutsceneTime += 10;
				if (this.mCutsceneTime == CutScene.TimeSeedChoserSlideOnEnd + this.mCrazyDaveTime && this.mBoard.ChooseSeedsOnCurrentLevel())
				{
					this.StartSeedChooser();
				}
			}
			int num = CutScene.TimeIntroEnd + this.mLawnMowerTime + this.mSodTime + this.mGraveStoneTime + this.mCrazyDaveTime + this.mFogTime + this.mBossTime + this.mReadySetPlantTime;
			if (this.mCutsceneTime >= num)
			{
				this.mBoard.RemoveCutsceneZombies();
				if (this.mBoard.mTutorialState != TutorialState.TUTORIAL_ZEN_GARDEN_PICKUP_WATER && this.mBoard.mTutorialState != TutorialState.TUTORIAL_ZEN_GARDEN_KEEP_WATERING && this.mBoard.mTutorialState != TutorialState.TUTORIAL_ZEN_GARDEN_FERTILIZE_PLANTS && this.mBoard.mTutorialState != TutorialState.TUTORIAL_ZEN_GARDEN_VISIT_STORE && this.mBoard.mTutorialState != TutorialState.TUTORIAL_ZEN_GARDEN_WATER_PLANT && this.mBoard.mTutorialState != TutorialState.TUTORIAL_ZEN_GARDEN_COMPLETED)
				{
					this.mBoard.mMenuButton.mBtnNoDraw = false;
				}
				this.ShowShovel();
				this.mApp.StartPlaying();
				return;
			}
			this.AnimateBoard();
		}

		public void AnimateBoard()
		{
			int timeEarlyDaveEnterStart = CutScene.TimeEarlyDaveEnterStart;
			int timeEarlyDaveEnterEnd = CutScene.TimeEarlyDaveEnterEnd;
			int timeEarlyDaveLeaveEnd = CutScene.TimeEarlyDaveLeaveEnd;
			int num = CutScene.TimePanRightStart + this.mCrazyDaveTime;
			int num2 = CutScene.TimePanRightEnd + this.mCrazyDaveTime;
			int num3 = CutScene.TimePanLeftStart + this.mCrazyDaveTime;
			int num4 = CutScene.TimePanLeftEnd + this.mCrazyDaveTime;
			if (this.mCrazyDaveTime > 0)
			{
				if (this.mCutsceneTime == timeEarlyDaveEnterStart)
				{
					this.mApp.CrazyDaveEnter();
					if (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
					{
						Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mApp.mCrazyDaveReanimID);
						reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_enterup, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 12f);
						reanimation.SetPosition(100f * Constants.S, 70f * Constants.S);
					}
				}
				if (this.mCutsceneTime == timeEarlyDaveEnterEnd && this.mCrazyDaveDialogStart != -1)
				{
					this.mApp.CrazyDaveTalkIndex(this.mCrazyDaveDialogStart);
					this.mCrazyDaveDialogStart = -1;
				}
				if (this.mCutsceneTime == timeEarlyDaveLeaveEnd && this.IsNonScrollingCutscene())
				{
					this.mCutsceneTime = num4;
				}
			}
			int num5 = Constants.BOARD_OFFSET;
			if (!this.IsScrolledLeftAtStart())
			{
				num5 = (int)(Constants.IS * (float)Constants.Board_Offset_AspectRatio_Correction);
			}
			if (this.mCutsceneTime <= num)
			{
				this.mBoard.Move((int)((float)num5 * Constants.S), 0);
			}
			if (this.mCutsceneTime > num && this.mCutsceneTime <= num2)
			{
				int thePositionStart = -num5;
				int thePositionEnd = -Constants.BOARD_OFFSET + Constants.BACKGROUND_IMAGE_WIDTH - Constants.WIDE_BOARD_WIDTH + Constants.Board_Cutscene_ExtraScroll;
				int num6 = this.CalcPosition(num, num2, thePositionStart, thePositionEnd);
				this.mBoard.Move(-(int)((float)num6 * Constants.S), 0);
			}
			if (this.mBoard.ChooseSeedsOnCurrentLevel())
			{
				int num7 = CutScene.TimeSeedChoserSlideOnStart + this.mCrazyDaveTime;
				int num8 = CutScene.TimeSeedChoserSlideOnEnd + this.mCrazyDaveTime;
				if (this.mCutsceneTime > num7 && this.mCutsceneTime <= num8)
				{
					int seed_CHOOSER_OFFSETSCREEN_OFFSET = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET;
					int thePositionEnd2 = 0;
					int theNewY = this.CalcPosition(num7, num8, seed_CHOOSER_OFFSETSCREEN_OFFSET, thePositionEnd2);
					this.mApp.mSeedChooserScreen.Move(0, theNewY);
					int mY = this.CalcPosition(num7, num8, (int)Constants.InvertAndScale(-50f), Constants.UIMenuButtonPosition.Y);
					this.mApp.mSeedChooserScreen.mMenuButton.mY = mY;
					this.mApp.mSeedChooserScreen.mMenuButton.mBtnNoDraw = false;
				}
				int num9 = CutScene.TimeSeedChoserSlideOffStart + this.mCrazyDaveTime;
				int num10 = CutScene.TimeSeedChoserSlideOffEnd + this.mCrazyDaveTime;
				if (this.mCutsceneTime > num9 && this.mCutsceneTime <= num10)
				{
					int thePositionStart2 = 0;
					int seed_CHOOSER_OFFSETSCREEN_OFFSET2 = Constants.SEED_CHOOSER_OFFSETSCREEN_OFFSET;
					int theNewY2 = this.CalcPosition(num9, num10, thePositionStart2, seed_CHOOSER_OFFSETSCREEN_OFFSET2);
					this.mApp.mSeedChooserScreen.Move(0, theNewY2);
					this.mApp.mSeedChooserScreen.mMenuButton.mDisabled = true;
				}
			}
			if (this.mCutsceneTime > num3)
			{
				int thePositionStart3 = Constants.BACKGROUND_IMAGE_WIDTH - Constants.WIDE_BOARD_WIDTH - Constants.BOARD_OFFSET + Constants.Board_Cutscene_ExtraScroll + (int)(Constants.IS * (float)Constants.Board_Offset_AspectRatio_Correction);
				int thePositionEnd3 = 0;
				int num11 = this.CalcPosition(num3, num4, thePositionStart3, thePositionEnd3);
				this.mBoard.Move(-(int)((float)num11 * Constants.S) + Constants.Board_Offset_AspectRatio_Correction, 0);
			}
			int num12 = 0;
			if (!this.mBoard.ChooseSeedsOnCurrentLevel())
			{
				num12 = CutScene.TimePanLeftEnd - CutScene.TimeSeedChoserSlideOnStart + this.mSodTime + this.mGraveStoneTime + this.mFogTime + this.mBossTime;
			}
			int num13 = CutScene.TimeSeedBankOnStart + this.mCrazyDaveTime + num12;
			int num14 = CutScene.TimeSeedBankOnEnd + this.mCrazyDaveTime + num12;
			if (!this.mApp.IsChallengeWithoutSeedBank() && this.mCutsceneTime > num13 && this.mCutsceneTime <= num14)
			{
				int x = this.CalcPosition(num13, num14, -this.mBoard.mSeedBank.mWidth, 0);
				this.mBoard.mSeedBank.Move(x, 0);
			}
			int num15 = CutScene.TimeSeedBankRightStart + this.mCrazyDaveTime;
			int theTimeEnd = CutScene.TimeSeedBankRightEnd + this.mCrazyDaveTime;
			if (this.mCutsceneTime > num15)
			{
				this.mBoard.mSeedBank.mCutSceneDarken = TodCommon.TodAnimateCurve(num15, theTimeEnd, this.mCutsceneTime, 255, 128, TodCurves.CURVE_EASE_OUT);
			}
			if (this.mSodTime > 0)
			{
				int num16 = CutScene.TimeRollSodStart + this.mCrazyDaveTime;
				int num17 = CutScene.TimeRollSodEnd + this.mCrazyDaveTime;
				int mSodPosition = TodCommon.TodAnimateCurve(num16, num17, this.mCutsceneTime, 0, 1000, TodCurves.CURVE_LINEAR);
				this.mBoard.mSodPosition = mSodPosition;
				if (this.mCutsceneTime == num16)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_DIGGER);
					if (this.mBoard.mLevel == 1)
					{
						this.mApp.AddReanimation((float)Constants.BOARD_EDGE, 0f, 400000, ReanimationType.REANIM_SODROLL, false);
						this.mApp.AddTodParticle((float)(Constants.CutScene_ExtraRoom_1_Particle_Pos.X + Constants.BOARD_EXTRA_ROOM), (float)Constants.CutScene_ExtraRoom_1_Particle_Pos.Y, 400001, ParticleEffect.PARTICLE_SOD_ROLL);
					}
					else if (this.mBoard.mLevel == 2)
					{
						this.mApp.AddReanimation((float)Constants.BOARD_EDGE - 10f, (float)Constants.CutScene_SodRoll_1_Pos * Constants.S, 400000, ReanimationType.REANIM_SODROLL, false);
						this.mApp.AddReanimation((float)Constants.BOARD_EDGE - 10f, (float)Constants.CutScene_SodRoll_2_Pos * Constants.S, 400000, ReanimationType.REANIM_SODROLL, false);
						this.mApp.AddTodParticle((float)(Constants.CutScene_ExtraRoom_2_Particle_Pos.X + Constants.BOARD_EXTRA_ROOM), (float)Constants.CutScene_ExtraRoom_2_Particle_Pos.Y, 400001, ParticleEffect.PARTICLE_SOD_ROLL);
						this.mApp.AddTodParticle((float)(Constants.CutScene_ExtraRoom_3_Particle_Pos.X + Constants.BOARD_EXTRA_ROOM), (float)Constants.CutScene_ExtraRoom_3_Particle_Pos.Y, 400001, ParticleEffect.PARTICLE_SOD_ROLL);
					}
					else if (this.mBoard.mLevel == 4)
					{
						this.mApp.AddReanimation((float)(Constants.CutScene_SodRoll_3_Pos.X + Constants.BOARD_EDGE) + 10f, (float)Constants.CutScene_SodRoll_3_Pos.Y * Constants.S, 400000, ReanimationType.REANIM_SODROLL, false);
						this.mApp.AddReanimation((float)(Constants.CutScene_SodRoll_4_Pos.X + Constants.BOARD_EDGE) + 10f, (float)Constants.CutScene_SodRoll_4_Pos.Y * Constants.S, 400000, ReanimationType.REANIM_SODROLL, false);
						this.mApp.AddTodParticle((float)(Constants.CutScene_ExtraRoom_4_Particle_Pos.X + Constants.BOARD_EXTRA_ROOM), (float)Constants.CutScene_ExtraRoom_4_Particle_Pos.Y, 400001, ParticleEffect.PARTICLE_SOD_ROLL);
						this.mApp.AddTodParticle((float)(Constants.CutScene_ExtraRoom_5_Particle_Pos.X + Constants.BOARD_EXTRA_ROOM), (float)Constants.CutScene_ExtraRoom_5_Particle_Pos.Y, 400001, ParticleEffect.PARTICLE_SOD_ROLL);
					}
				}
				if (this.mCutsceneTime == num17)
				{
					this.mApp.mSoundSystem.StopFoley(FoleyType.FOLEY_DIGGER);
				}
			}
			if (this.mGraveStoneTime > 0)
			{
				int num18 = CutScene.TimeGraveStoneStart + this.mSodTime + this.mCrazyDaveTime;
				if (this.mCutsceneTime == num18)
				{
					this.mBoard.mEnableGraveStones = true;
					this.AddGraveStoneParticles();
				}
			}
			if (this.mCutsceneTime == num3)
			{
				this.PlaceLawnItems();
			}
			if (!this.IsSurvivalRepick())
			{
				for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
				{
					int num19 = CutScene.TimeLawnMowerStart[i] + this.mSodTime + this.mGraveStoneTime + this.mCrazyDaveTime;
					int theTimeEnd2 = num19 + CutScene.TimeLawnMowerDuration;
					if (this.mCutsceneTime > num19)
					{
						LawnMower lawnMower = this.mBoard.FindLawnMowerInRow(i);
						if (lawnMower != null)
						{
							lawnMower.mVisible = true;
							int num20 = this.CalcPosition(num19, theTimeEnd2, -80, -21) + Constants.BOARD_EXTRA_ROOM;
							lawnMower.mPosX = (float)num20;
						}
					}
				}
			}
			int num21 = CutScene.TimeFogRollIn + this.mSodTime + this.mGraveStoneTime + this.mCrazyDaveTime;
			if (this.mBoard.mFogBlownCountDown > 0 && this.mCutsceneTime > num21)
			{
				if (this.mBoard.mFogBlownCountDown > 200)
				{
					this.mBoard.mFogBlownCountDown = 200;
				}
				this.mBoard.mFogBlownCountDown--;
			}
			if (this.mApp.IsStormyNightLevel())
			{
				if (this.mCutsceneTime == num2 - 1000)
				{
					this.mBoard.mChallenge.mChallengeState = ChallengeState.STATECHALLENGE_STORM_FLASH_2;
					this.mBoard.mChallenge.mChallengeStateCounter = 310;
				}
				else if (this.mCutsceneTime == num4)
				{
					this.mBoard.mChallenge.mChallengeState = ChallengeState.STATECHALLENGE_STORM_FLASH_2;
					this.mBoard.mChallenge.mChallengeStateCounter = 310;
				}
			}
			int num22 = CutScene.TimeReadySetPlantStart + this.mLawnMowerTime + this.mCrazyDaveTime;
			if (this.mBossTime > 0 && this.mCutsceneTime == num22)
			{
				this.mBoard.mChallenge.PlayBossEnter();
			}
			if (this.mApp.IsFinalBossLevel() && this.mCutsceneTime == num13)
			{
				this.mApp.mMusic.StartGameMusic();
			}
			int num23 = CutScene.TimeReadySetPlantStart + this.mLawnMowerTime + this.mSodTime + this.mGraveStoneTime + this.mCrazyDaveTime + this.mFogTime + this.mBossTime;
			if (this.mReadySetPlantTime > 0 && this.mCutsceneTime == num23)
			{
				int x2 = Constants.CutScene_ReadySetPlant_Pos.X;
				int y = Constants.CutScene_ReadySetPlant_Pos.Y;
				this.mApp.AddReanimation((float)x2 * Constants.IS, (float)y * Constants.IS, 900000, ReanimationType.REANIM_READYSETPLANT);
				this.mApp.PlaySample(Resources.SOUND_READYSETPLANT);
				this.mApp.IsFinalBossLevel();
			}
			if (this.mReadySetPlantTime == 0 && this.mCutsceneTime == num23 - 2000)
			{
				this.mApp.IsFinalBossLevel();
			}
		}

		public void StartSeedChooser()
		{
			this.mApp.mSeedChooserScreen.mMouseVisible = true;
			this.mSeedChoosing = true;
			this.mApp.mWidgetManager.SetFocus(this.mApp.mSeedChooserScreen);
		}

		public void EndSeedChooser()
		{
			this.mApp.mSeedChooserScreen.mMouseVisible = false;
			this.mSeedChoosing = false;
			this.mCutsceneTime = CutScene.TimeSeedChoserSlideOnEnd + this.mCrazyDaveTime + 10;
			this.mApp.mWidgetManager.SetFocus(this.mBoard);
		}

		public int CalcPosition(int theTimeStart, int theTimeEnd, int thePositionStart, int thePositionEnd)
		{
			return TodCommon.TodAnimateCurve(theTimeStart, theTimeEnd, this.mCutsceneTime, thePositionStart, thePositionEnd, TodCurves.CURVE_EASE_IN_OUT);
		}

		public void PlaceStreetZombies()
		{
			if (this.mPlacedZombies)
			{
				return;
			}
			this.mPlacedZombies = true;
			if (this.mApp.IsFinalBossLevel())
			{
				return;
			}
			int num = 0;
			int[] array = new int[33];
			int num2 = 0;
			for (int i = 0; i < 33; i++)
			{
				array[i] = 0;
			}
			Debug.ASSERT(this.mBoard.mNumWaves <= 100);
			for (int j = 0; j < this.mBoard.mNumWaves; j++)
			{
				for (int k = 0; k < 50; k++)
				{
					ZombieType zombieType = this.mBoard.mZombiesInWave[j, k];
					if (zombieType == ZombieType.ZOMBIE_INVALID)
					{
						break;
					}
					num2 += Zombie.GetZombieDefinition(zombieType).mZombieValue;
					if (zombieType != ZombieType.ZOMBIE_FLAG && (zombieType != ZombieType.ZOMBIE_YETI || (!this.mApp.IsQuickPlayMode() && this.mApp.IsStormyNightLevel())) && (zombieType != ZombieType.ZOMBIE_BOBSLED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA))
					{
						Debug.ASSERT(zombieType >= ZombieType.ZOMBIE_NORMAL && zombieType < ZombieType.NUM_ZOMBIE_TYPES);
						array[(int)zombieType]++;
						num++;
						if (zombieType == ZombieType.ZOMBIE_BUNGEE || zombieType == ZombieType.ZOMBIE_BOBSLED)
						{
							array[(int)zombieType] = 1;
						}
					}
				}
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				for (int l = 0; l < 33; l++)
				{
					if (l != 19 && this.mBoard.mZombieAllowed[l])
					{
						array[l] = Math.Max(array[l], 1);
					}
				}
			}
			if (this.mBoard.StageHasPool())
			{
				array[10] = 1;
			}
			bool[,] array2 = new bool[5, 5];
			for (int m = 0; m < 5; m++)
			{
				for (int n = 0; n < 5; n++)
				{
					array2[m, n] = false;
				}
			}
			int num3 = 10;
			if (this.mApp.IsLittleTroubleLevel())
			{
				num3 = 15;
			}
			else if (this.mApp.IsStormyNightLevel() && (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()))
			{
				num3 = 18;
			}
			else if (this.mApp.IsMiniBossLevel())
			{
				num3 = 18;
			}
			Debug.ASSERT(num3 <= 18);
			for (int num4 = 0; num4 < 33; num4++)
			{
				if (array[num4] != 0 && (this.Is2x2Zombie((ZombieType)num4) || num4 == 12))
				{
					this.FindAndPlaceZombie((ZombieType)num4, array2);
				}
			}
			for (int num5 = 0; num5 < 33; num5++)
			{
				if (array[num5] != 0 && !this.Is2x2Zombie((ZombieType)num5) && num5 != 12)
				{
					int num6 = array[num5] * num3 / num;
					num6 = TodCommon.ClampInt(num6, 1, array[num5]);
					for (int num7 = 0; num7 < num6; num7++)
					{
						this.FindAndPlaceZombie((ZombieType)num5, array2);
					}
				}
			}
		}

		public void AddGraveStoneParticles()
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_GRAVESTONE)
				{
					gridItem.AddGraveStoneParticles();
				}
			}
		}

		public void PlaceAZombie(ZombieType theZombieType, int theGridX, int theGridY)
		{
			bool flag = false;
			if (theZombieType == ZombieType.ZOMBIE_DUCKY_TUBE && this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS_2)
			{
				theZombieType = ZombieType.ZOMBIE_PEA_HEAD;
				flag = true;
			}
			Zombie zombie = this.mBoard.AddZombieInRow(theZombieType, 0, GameConstants.ZOMBIE_WAVE_CUTSCENE);
			Debug.ASSERT(zombie != null);
			zombie.mPosX = (float)(830 + 56 * theGridX + 110);
			zombie.mPosY = (float)(70 + 90 * theGridY);
			if (theGridX % 2 == 1)
			{
				zombie.mPosY += 30f;
			}
			if (flag)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(zombie.mBodyReanimID);
				reanimation.AssignRenderGroupToPrefix("Zombie_duckytube", 0);
			}
			if (this.mBoard.StageHasRoof())
			{
				zombie.mPosY -= (float)(7 * (5 - theGridX) - 2 * (5 - theGridY) + 5);
				zombie.mPosX -= 5f;
			}
			if (theZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				zombie.mPosY -= 10f;
				zombie.mPosX -= 30f;
			}
			else if (this.mApp.IsLittleTroubleLevel())
			{
				zombie.mPosY += (float)(RandomNumbers.NextNumber(50) - 25);
				zombie.mPosX += (float)(RandomNumbers.NextNumber(50) - 25);
			}
			else if (this.Is2x2Zombie(theZombieType))
			{
				zombie.mPosX += (float)(-20 + RandomNumbers.NextNumber(15));
			}
			else if (theGridY == 4 && (this.mApp.CanShowAlmanac() || this.mApp.CanShowStore()))
			{
				zombie.mPosX += (float)RandomNumbers.NextNumber(15);
			}
			else
			{
				zombie.mPosY += (float)RandomNumbers.NextNumber(15);
				zombie.mPosX += (float)RandomNumbers.NextNumber(15);
			}
			int num = theGridY * 2 + theGridX % 2;
			zombie.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_LAWN, 0, num * 2);
			if (theZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				zombie.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GROUND, 0, 0);
				zombie.mPosX = 950f + (float)theGridX * 50f;
				zombie.mPosY = 50f;
				zombie.mRow = 0;
			}
			if (theZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				zombie.mPosX = 1105f;
				zombie.mPosY = 480f;
				zombie.mRow = 0;
				zombie.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_LAWN, 0, 1000);
			}
		}

		public bool CanZombieGoInGridSpot(ZombieType theZombieType, int theGridX, int theGridY, bool[,] theZombieGrid)
		{
			if (theZombieGrid[theGridX, theGridY])
			{
				return false;
			}
			if (this.Is2x2Zombie(theZombieType))
			{
				if (theGridX == 0 || theGridY == 0)
				{
					return false;
				}
				if (theZombieGrid[theGridX - 1, theGridY] || theZombieGrid[theGridX, theGridY - 1] || theZombieGrid[theGridX - 1, theGridY - 1])
				{
					return false;
				}
			}
			if (theGridX == 4 && theGridY == 0)
			{
				return false;
			}
			if (theGridX != 4 && theZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				return false;
			}
			if (theGridX == 0 && this.mBoard.StageHasPool())
			{
				return false;
			}
			if (this.mBoard.StageHasRoof() && theGridX == 0 && theGridY == 0)
			{
				return false;
			}
			if (theGridX == 4 && this.mBoard.StageHasFog() && theZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				return false;
			}
			if (theZombieType == ZombieType.ZOMBIE_GARGANTUAR || theZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR || theZombieType == ZombieType.ZOMBIE_ZAMBONI || theZombieType == ZombieType.ZOMBIE_BOBSLED || theZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				if (theGridX == 0)
				{
					return false;
				}
				if (theGridX == 1 && this.mBoard.StageHasPool())
				{
					return false;
				}
				if (theGridX == 1 && theGridY == 0)
				{
					return false;
				}
			}
			return true;
		}

		public bool IsSurvivalRepick()
		{
			return this.mApp.IsSurvivalMode() && this.mBoard.mChallenge.mSurvivalStage > 0 && this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO;
		}

		public bool IsAfterSeedChooser()
		{
			return this.mCutsceneTime > CutScene.TimeSeedChoserSlideOffStart + this.mCrazyDaveTime;
		}

		public void AddFlowerPots()
		{
			int num = 0;
			if (this.mBoard.mLevel == 41)
			{
				num = 5;
			}
			else if (this.mBoard.mLevel == 42)
			{
				num = 4;
			}
			else if (this.mBoard.mLevel >= 43 && this.mBoard.mLevel <= 50)
			{
				num = 3;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				num = 8;
			}
			else if (this.mBoard.StageHasRoof())
			{
				num = 3;
			}
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
				{
					if (this.mBoard.CanPlantAt(i, j, SeedType.SEED_FLOWERPOT) == PlantingReason.PLANTING_OK)
					{
						Plant newPlant = Plant.GetNewPlant();
						newPlant.mIsOnBoard = true;
						newPlant.PlantInitialize(i, j, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
						this.mBoard.mPlants.Add(newPlant);
					}
				}
			}
		}

		public void UpdateZombiesWon()
		{
			if (this.mCutsceneTime > CutScene.LostTimePanRightStart && this.mCutsceneTime <= CutScene.LostTimePanRightEnd)
			{
				int num = this.CalcPosition(CutScene.TimePanRightStart, CutScene.TimePanRightEnd, (int)((float)Constants.Board_Offset_AspectRatio_Correction * Constants.IS), Constants.BOARD_OFFSET);
				this.mBoard.Move((int)((float)num * Constants.S), 0);
			}
			if (this.mCutsceneTime == CutScene.LostTimeBrainGraphicStart - 400 || this.mCutsceneTime == CutScene.LostTimeBrainGraphicStart - 900)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_CHOMP);
			}
			if (this.mCutsceneTime == CutScene.LostTimeBrainGraphicStart)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIES_WON, true);
				int num2 = Constants.BOARD_EXTRA_ROOM / 2;
				Reanimation reanimation = this.mApp.AddReanimation((float)(-(float)Constants.BOARD_OFFSET + num2 + Constants.Board_Offset_AspectRatio_Correction), 0f, 900000, ReanimationType.REANIM_ZOMBIES_WON);
				reanimation.mAnimRate = 12f;
				reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
				ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(Reanimation.ReanimTrackId_fullscreen);
				trackInstanceByName.mTrackColor = SexyColor.Black;
				this.mZombiesWonReanimID = this.mApp.ReanimationGetID(reanimation);
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_zombieswon);
				this.mApp.PlayFoley(FoleyType.FOLEY_SCREAM);
			}
			if (this.mCutsceneTime == CutScene.LostTimeBrainGraphicShake)
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mZombiesWonReanimID);
				reanimation2.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_zombieswon, 1f);
			}
			if (this.mCutsceneTime == CutScene.LostTimeBrainGraphicCancelShake)
			{
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mZombiesWonReanimID);
				reanimation3.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_zombieswon, 0f);
			}
			if (this.mCutsceneTime == CutScene.LostTimeBrainGraphicEnd)
			{
				Reanimation reanimation4 = this.mApp.ReanimationGet(this.mZombiesWonReanimID);
				reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_screen);
			}
			if (this.mCutsceneTime == CutScene.LostTimeEnd)
			{
				if (this.mApp.IsSurvivalMode())
				{
					int survivalFlagsCompleted = this.mBoard.GetSurvivalFlagsCompleted();
					string theStringToSubstitute = this.mApp.Pluralize(survivalFlagsCompleted, "[ONE_FLAG]", "[COUNT_FLAGS]");
					string theMessage = TodCommon.TodReplaceString("[SURVIVAL_DEATH_MESSAGE]", "{FLAGS}", theStringToSubstitute);
					GameOverDialog theDialog = new GameOverDialog(theMessage, true);
					this.mApp.AddDialog(17, theDialog);
					return;
				}
				GameOverDialog theDialog2 = new GameOverDialog("", false);
				this.mApp.AddDialog(17, theDialog2);
			}
		}

		public void StartZombiesWon()
		{
			this.mCutsceneTime = 0;
			this.mBoard.mMenuButton.mBtnNoDraw = true;
			this.mBoard.mShowShovel = false;
			this.mApp.mMusic.StopAllMusic();
			this.mBoard.StopAllZombieSounds();
			this.mApp.PlaySample(Resources.SOUND_LOSEMUSIC);
		}

		public bool ShowZombieWalking()
		{
			return true;
		}

		public bool IsCutSceneOver()
		{
			Debug.ASSERT(this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON);
			return this.mCutsceneTime >= CutScene.LostTimeEnd;
		}

		public void ZombieWonClick()
		{
			if (this.IsCutSceneOver() || this.mApp.mTodCheatKeys)
			{
				this.mApp.EndLevel();
			}
		}

		public void MouseDown(int x, int y)
		{
			if (this.mApp.mTodCheatKeys && this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
			{
				if (this.mCrazyDaveCountDown > 1)
				{
					this.mCrazyDaveCountDown = 1;
					return;
				}
			}
			else
			{
				if (this.IsShowingCrazyDave())
				{
					this.AdvanceCrazyDaveDialog(false);
					return;
				}
				if (this.mApp.mTodCheatKeys)
				{
					this.CancelIntro();
				}
			}
		}

		public void AdvanceCrazyDaveDialog(bool theJustSkipping)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
			{
				return;
			}
			if (this.mApp.mCrazyDaveMessageIndex == -1)
			{
				return;
			}
			if (this.mApp.mCrazyDaveMessageIndex == 2406 && !theJustSkipping)
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_SHOVEL_PICKUP);
				this.mApp.CrazyDaveLeave();
				return;
			}
			if (!this.mApp.AdvanceCrazyDaveText())
			{
				this.mApp.CrazyDaveLeave();
				if (this.mApp.IsFinalBossLevel() && this.mApp.IsAdventureMode())
				{
					Reanimation reanimation = this.mApp.ReanimationGet(this.mApp.mCrazyDaveReanimID);
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_grab, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 18f);
					this.mApp.mMusic.FadeOut(50);
					if (!theJustSkipping)
					{
						this.mApp.PlaySample(Resources.SOUND_BUNGEE_SCREAM);
						return;
					}
				}
				else
				{
					if (this.mBoard.ChooseSeedsOnCurrentLevel())
					{
						this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_CHOOSE_YOUR_SEEDS);
						return;
					}
					if (this.IsNonScrollingCutscene())
					{
						this.mApp.mMusic.FadeOut(50);
					}
				}
				return;
			}
			if (this.mApp.mCrazyDaveMessageIndex == 107 || this.mApp.mCrazyDaveMessageIndex == 2407)
			{
				this.mBoard.mChallenge.ShovelAddWallnuts();
			}
			if (this.mApp.mCrazyDaveMessageIndex == 405 || this.mApp.mCrazyDaveMessageIndex == 2411)
			{
				this.mBoard.mChallenge.mShowBowlingLine = true;
			}
			bool flag = this.mApp.mCrazyDaveMessageIndex == 1503 || this.mApp.mCrazyDaveMessageIndex == 1553;
			if (flag && !theJustSkipping)
			{
				int itemCost = StoreScreen.GetItemCost(StoreItem.STORE_ITEM_PACKET_UPGRADE);
				int num = this.mApp.mPlayerInfo.mPurchases[21] + 6;
				string theDialogLines = TodCommon.TodReplaceNumberString("[UPGRADE_DIALOG_BODY]", "{SLOTS}", num + 1);
				string moneyString = LawnApp.GetMoneyString(itemCost);
				LawnDialog lawnDialog = this.mApp.DoDialog(51, true, moneyString, theDialogLines, string.Empty, 1);
				lawnDialog.Resize((int)Constants.InvertAndScale(300f), (int)Constants.InvertAndScale(100f), (int)Constants.InvertAndScale(370f), (int)Constants.InvertAndScale(200f));
				this.mBoard.mCoinBankFadeCount = 100;
				return;
			}
			if (this.mApp.mCrazyDaveMessageIndex == 406)
			{
				this.mBoard.mEnableGraveStones = true;
				this.AddGraveStoneParticles();
			}
		}

		public void ShowShovel()
		{
			if (this.mApp.IsWhackAZombieLevel() || this.mApp.IsWallnutBowlingLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST || this.mApp.IsIZombieLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				return;
			}
			if (!this.mApp.IsFirstTimeAdventureMode() || this.mBoard.mLevel > 4)
			{
				this.mBoard.mShowShovel = true;
			}
		}

		public bool CanGetPacketUpgrade()
		{
			int itemCost = StoreScreen.GetItemCost(StoreItem.STORE_ITEM_PACKET_UPGRADE);
			return this.mApp.mPlayerInfo.mPurchases[21] == 0 && this.mApp.mPlayerInfo.mCoins >= itemCost && this.mApp.mPlayerInfo.mDidntPurchasePacketUpgrade < 2;
		}

		public void FindPlaceForStreetZombies(ZombieType theZombieType, bool[,] theZombieGrid, ref int thePosX, ref int thePosY)
		{
			if (theZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				thePosX = 0;
				thePosY = 0;
				return;
			}
			for (int i = 0; i < CutScene.aPicks.Length; i++)
			{
				CutScene.aPicks[i].Reset();
			}
			int num = 0;
			for (int j = 0; j < 5; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					if (this.CanZombieGoInGridSpot(theZombieType, j, k, theZombieGrid))
					{
						CutScene.aPicks[num].mX = j;
						CutScene.aPicks[num].mY = k;
						CutScene.aPicks[num].mWeight = 1;
						num++;
					}
				}
			}
			if (num == 0)
			{
				thePosX = 2;
				thePosY = 2;
				return;
			}
			TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(CutScene.aPicks, num);
			thePosX = todWeightedGridArray.mX;
			thePosY = todWeightedGridArray.mY;
		}

		public void FindAndPlaceZombie(ZombieType theZombieType, bool[,] theZombieGrid)
		{
			int num = 0;
			int num2 = 0;
			this.FindPlaceForStreetZombies(theZombieType, theZombieGrid, ref num, ref num2);
			if (theZombieType != ZombieType.ZOMBIE_BUNGEE)
			{
				theZombieGrid[num, num2] = true;
			}
			if (this.Is2x2Zombie(theZombieType))
			{
				Debug.ASSERT(num > 0 && num2 > 0);
				theZombieGrid[num - 1, num2] = true;
				theZombieGrid[num, num2 - 1] = true;
				theZombieGrid[num - 1, num2 - 1] = true;
			}
			this.PlaceAZombie(theZombieType, num, num2);
			if (theZombieType == ZombieType.ZOMBIE_BUNGEE && this.mApp.IsBungeeBlitzLevel())
			{
				this.PlaceAZombie(ZombieType.ZOMBIE_BUNGEE, 1, num2);
				this.PlaceAZombie(ZombieType.ZOMBIE_BUNGEE, 2, num2);
			}
		}

		public bool Is2x2Zombie(ZombieType theZombieType)
		{
			return theZombieType == ZombieType.ZOMBIE_GARGANTUAR || theZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR;
		}

		public void PreloadResources()
		{
			if (this.mPreloaded)
			{
				return;
			}
			this.mPreloaded = true;
			PerfTimer perfTimer = default(PerfTimer);
			perfTimer.Start();
			for (int i = 0; i < this.mBoard.mNumWaves; i++)
			{
				for (int j = 0; j < 50; j++)
				{
					ZombieType zombieType = this.mBoard.mZombiesInWave[i, j];
					if (zombieType == ZombieType.ZOMBIE_INVALID)
					{
						break;
					}
					Zombie.PreloadZombieResources(zombieType);
				}
			}
			for (int k = 0; k < 53; k++)
			{
				SeedType theSeedType = (SeedType)k;
				if (this.mApp.HasSeedType(theSeedType))
				{
					Plant.PreloadPlantResources(theSeedType);
				}
			}
			if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel <= 50)
			{
				SeedType awardSeedForLevel = this.mApp.GetAwardSeedForLevel(this.mBoard.mLevel);
				Plant.PreloadPlantResources(awardSeedForLevel);
			}
			if (this.mCrazyDaveDialogStart != -1)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_CRAZY_DAVE, true);
			}
			if (this.mApp.mPlayerInfo.mPurchases[24] != 0)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_RAKE, true);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				Plant.PreloadPlantResources(SeedType.SEED_SPROUT);
				Plant.PreloadPlantResources(SeedType.SEED_MARIGOLD);
			}
			if (this.mBoard.StageHasRoof())
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ROOF_CLEANER, true);
			}
			else
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_LAWNMOWER, true);
			}
			if (this.mBoard.StageHasPool())
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_SPLASH, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_POOL_CLEANER, true);
			}
			if (this.mBoard.CanDropLoot())
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_COIN_SILVER, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_COIN_GOLD, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_DIAMOND, true);
			}
			if (this.mSodTime > 0)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_SODROLL, true);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_PORTAL_CIRCLE, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_PORTAL_SQUARE, true);
			}
			if (this.mApp.IsWhackAZombieLevel() || this.mApp.IsScaryPotterLevel())
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_HAMMER, true);
			}
			if (this.mApp.IsStormyNightLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_RAIN_CIRCLE, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_RAIN_SPLASH, true);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_INTRO)
			{
				this.mApp.DelayLoadBackgroundResource("DelayLoad_Background3");
				Zombie.PreloadZombieResources(ZombieType.ZOMBIE_NORMAL);
				Zombie.PreloadZombieResources(ZombieType.ZOMBIE_TRAFFIC_CONE);
				Zombie.PreloadZombieResources(ZombieType.ZOMBIE_PAIL);
				Zombie.PreloadZombieResources(ZombieType.ZOMBIE_ZAMBONI);
				Plant.PreloadPlantResources(SeedType.SEED_SUNFLOWER);
				Plant.PreloadPlantResources(SeedType.SEED_PEASHOOTER);
				Plant.PreloadPlantResources(SeedType.SEED_SQUASH);
				Plant.PreloadPlantResources(SeedType.SEED_THREEPEATER);
				Plant.PreloadPlantResources(SeedType.SEED_LILYPAD);
				Plant.PreloadPlantResources(SeedType.SEED_TORCHWOOD);
				Plant.PreloadPlantResources(SeedType.SEED_SPIKEWEED);
				Plant.PreloadPlantResources(SeedType.SEED_TANGLEKELP);
			}
			this.PlaceStreetZombies();
			this.mBoard.mPreloadTime = Math.Max((int)perfTimer.GetDuration(), 0);
		}

		public bool IsBeforePreloading()
		{
			return this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO && !this.mPreloaded;
		}

		public bool IsShowingCrazyDave()
		{
			return this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO && (this.mCrazyDaveTime > 0 && this.mCutsceneTime < CutScene.TimePanRightEnd + this.mCrazyDaveTime);
		}

		public bool IsNonScrollingCutscene()
		{
			return this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE || this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL || this.mApp.IsScaryPotterLevel() || this.mApp.IsIZombieLevel() || this.mApp.IsWhackAZombieLevel() || this.mApp.IsShovelLevel() || this.mApp.IsSquirrelLevel() || this.mApp.IsWallnutBowlingLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM;
		}

		public bool IsScrolledLeftAtStart()
		{
			return (this.mBoard.mChallenge.mSurvivalStage <= 0 || !this.mApp.IsSurvivalMode()) && !this.mApp.IsShovelLevel() && !this.mApp.IsSquirrelLevel() && !this.mApp.IsWallnutBowlingLevel() && !this.IsNonScrollingCutscene();
		}

		public bool IsInShovelTutorial()
		{
			return this.mBoard.mTutorialState == TutorialState.TUTORIAL_SHOVEL_PICKUP || this.mBoard.mTutorialState == TutorialState.TUTORIAL_SHOVEL_DIG || this.mBoard.mTutorialState == TutorialState.TUTORIAL_SHOVEL_KEEP_DIGGING;
		}

		public void PlaceLawnItems()
		{
			if (this.mPlacedLawnItems)
			{
				return;
			}
			this.mPlacedLawnItems = true;
			if (!this.IsSurvivalRepick())
			{
				this.mBoard.InitLawnMowers();
				this.AddFlowerPots();
			}
			if (!this.IsSurvivalRepick())
			{
				this.mBoard.PlaceRake();
			}
		}

		public bool CanGetSecondPacketUpgrade()
		{
			int itemCost = StoreScreen.GetItemCost(StoreItem.STORE_ITEM_PACKET_UPGRADE);
			return this.mApp.mPlayerInfo.mPurchases[21] == 1 && this.mApp.mPlayerInfo.mCoins >= itemCost && this.mApp.mPlayerInfo.mDidntPurchasePacketUpgrade < 2;
		}

		public int ParseTalkTimeFromMessage()
		{
			string crazyDaveText = this.mApp.GetCrazyDaveText(this.mCrazyDaveLastTalkIndex);
			int num = crazyDaveText.IndexOf("{TIME_");
			if (num != -1)
			{
				int num2 = crazyDaveText.IndexOf("}", num);
				string text = crazyDaveText.Substring(num + 6, num2 - num - 6);
				this.mCrazyDaveCountDown = int.Parse(text);
				return this.mCrazyDaveCountDown;
			}
			return 100;
		}

		public int ParseDelayTimeFromMessage()
		{
			string crazyDaveText = this.mApp.GetCrazyDaveText(this.mCrazyDaveLastTalkIndex);
			int num = crazyDaveText.IndexOf("{DELAY_");
			if (num != -1)
			{
				int num2 = crazyDaveText.IndexOf("}", num);
				string text = crazyDaveText.Substring(num + 7, num2 - num - 7);
				this.mCrazyDaveCountDown = int.Parse(text);
				return this.mCrazyDaveCountDown;
			}
			return 100;
		}

		public void UpdateUpsell()
		{
			if (!this.mBoard.mMenuButton.mIsOver)
			{
				bool mIsOver = this.mBoard.mStoreButton.mIsOver;
			}
			if (this.mApp.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_OFF || this.mApp.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_ENTERING)
			{
				return;
			}
			if (this.mCrazyDaveLastTalkIndex == -1)
			{
				this.mApp.CrazyDaveTalkIndex(this.mCrazyDaveDialogStart);
				this.mCrazyDaveLastTalkIndex = this.mCrazyDaveDialogStart;
				this.mCrazyDaveCountDown = this.ParseTalkTimeFromMessage();
				return;
			}
			if (this.mCrazyDaveCountDown > 0)
			{
				this.mCrazyDaveCountDown--;
			}
			if (this.mCrazyDaveLastTalkIndex == 3317)
			{
				if (this.mCrazyDaveCountDown == 0)
				{
					this.mBoard.mStoreButton.Resize(450, 420, 260, 46);
					this.mBoard.mMenuButton.mBtnNoDraw = false;
					this.mBoard.mStoreButton.mBtnNoDraw = false;
				}
				return;
			}
			if (this.mCrazyDaveLastTalkIndex == 3311 && this.mCrazyDaveCountDown == 90)
			{
				this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_MINIGAME_LOONBOON);
			}
			if (this.mCrazyDaveCountDown != 0)
			{
				return;
			}
			if (this.mApp.mCrazyDaveMessageIndex != -1)
			{
				this.mCrazyDaveCountDown = this.ParseDelayTimeFromMessage();
				this.mApp.CrazyDaveStopTalking();
				return;
			}
			int theMessageIndex = this.mCrazyDaveLastTalkIndex + 1;
			this.mApp.CrazyDaveTalkIndex(theMessageIndex);
			this.mCrazyDaveLastTalkIndex = theMessageIndex;
			this.mCrazyDaveCountDown = this.ParseTalkTimeFromMessage();
			if (this.mCrazyDaveLastTalkIndex == 3305)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mApp.mCrazyDaveReanimID);
				Reanimation reanimation2 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_SQUASH);
				reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 15f);
				ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_dave_handinghand);
				AttachEffect attachEffect = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName.mAttachmentID, reanimation2, Constants.S * 92f, Constants.S * 387f);
				attachEffect.mOffset.mMatrix.M11 = 1.2f;
				attachEffect.mOffset.mMatrix.M22 = 1.2f;
				reanimation.Update();
			}
			if (this.mCrazyDaveLastTalkIndex == 3306)
			{
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mApp.mCrazyDaveReanimID);
				Reanimation reanimation4 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_THREEPEATER);
				reanimation4.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 15f);
				Reanimation reanimation5 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_THREEPEATER);
				reanimation5.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation5.mAnimRate = reanimation4.mAnimRate;
				reanimation5.SetFramesForLayer("anim_head_idle1");
				reanimation5.AttachToAnotherReanimation(ref reanimation4, "anim_head1");
				Reanimation reanimation6 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_THREEPEATER);
				reanimation6.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation6.mAnimRate = reanimation4.mAnimRate;
				reanimation6.SetFramesForLayer("anim_head_idle2");
				reanimation6.AttachToAnotherReanimation(ref reanimation4, "anim_head2");
				Reanimation reanimation7 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_THREEPEATER);
				reanimation7.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation7.mAnimRate = reanimation4.mAnimRate;
				reanimation7.SetFramesForLayer("anim_head_idle3");
				reanimation7.AttachToAnotherReanimation(ref reanimation4, "anim_head3");
				ReanimatorTrackInstance trackInstanceByName2 = reanimation3.GetTrackInstanceByName("Dave_body1");
				AttachEffect attachEffect2 = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName2.mAttachmentID, reanimation4, 0f, 0f);
				TodCommon.TodScaleRotateTransformMatrix(ref attachEffect2.mOffset.mMatrix, -50f, 230f, 0.5f, 1.2f, 1.2f);
				reanimation3.Update();
				reanimation4.Update();
			}
			if (this.mCrazyDaveLastTalkIndex == 3307)
			{
				Reanimation reanimation8 = this.mApp.ReanimationGet(this.mApp.mCrazyDaveReanimID);
				Reanimation reanimation9 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_MAGNETSHROOM);
				reanimation9.PlayReanim("anim_idle", ReanimLoopType.REANIM_LOOP, 0, 15f);
				TodCommon.TodScaleRotateTransformMatrix(ref reanimation9.mOverlayMatrix.mMatrix, 0f, 0f, 0.3f, 1f, 1f);
				ReanimatorTrackInstance trackInstanceByName3 = reanimation8.GetTrackInstanceByName("Dave_pot");
				AttachEffect attachEffect3 = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName3.mAttachmentID, reanimation9, Constants.S * 25f, Constants.S * 49f);
				attachEffect3.mOffset.mMatrix.M11 = 1.2f;
				attachEffect3.mOffset.mMatrix.M22 = 1.2f;
				reanimation8.Update();
			}
			if (this.mCrazyDaveLastTalkIndex == 3309)
			{
				Reanimation reanimation10 = this.mApp.ReanimationGet(this.mApp.mCrazyDaveReanimID);
				Reanimation reanimation11 = reanimation10.FindSubReanim(ReanimationType.REANIM_THREEPEATER);
				reanimation11.ReanimationDie();
				Reanimation reanimation12 = reanimation10.FindSubReanim(ReanimationType.REANIM_MAGNETSHROOM);
				reanimation12.ReanimationDie();
			}
			if (this.mCrazyDaveLastTalkIndex == 3312)
			{
				this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_MINIGAME_LOONBOON);
				this.LoadUpsellBoardPool();
				this.mApp.PlaySample(Resources.SOUND_FINALWAVE);
				this.mUpsellHideBoard = false;
			}
			if (this.mCrazyDaveLastTalkIndex == 3313)
			{
				this.LoadUpsellBoardFog();
				this.mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
				this.mUpsellHideBoard = false;
			}
			if (this.mCrazyDaveLastTalkIndex == 3314)
			{
				this.LoadUpsellChallengeScreen();
				this.mApp.PlaySample(Resources.SOUND_FINALWAVE);
				this.mUpsellHideBoard = false;
			}
			if (this.mCrazyDaveLastTalkIndex == 3315)
			{
				this.ClearUpsellBoard();
				this.mApp.PlaySample(Resources.SOUND_FINALWAVE);
				this.mUpsellHideBoard = true;
				this.mApp.AddTodParticle((float)Constants.CutScene_Upsell_TerraCotta_Arrow.X, (float)Constants.CutScene_Upsell_TerraCotta_Arrow.Y, 900000, ParticleEffect.PARTICLE_UPSELL_ARROW);
			}
			if (this.mCrazyDaveLastTalkIndex == 3316)
			{
				this.LoadUpsellBoardRoof();
				this.mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
				this.mUpsellHideBoard = false;
			}
			if (this.mCrazyDaveLastTalkIndex == 3317)
			{
				this.ClearUpsellBoard();
				this.mBoard.mMenuButton.mBtnNoDraw = true;
				this.mUpsellHideBoard = true;
			}
		}

		public void ClearUpsellBoard()
		{
			for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
			{
				this.mBoard.mIceTimer[i] = 0;
				this.mBoard.mIceMinX[i] = Constants.BOARD_WIDTH;
			}
			this.mBoard.mZombiesRow1.Clear();
			this.mBoard.mZombiesRow2.Clear();
			this.mBoard.mZombiesRow3.Clear();
			this.mBoard.mZombiesRow4.Clear();
			this.mBoard.mZombiesRow5.Clear();
			this.mBoard.mZombiesRow6.Clear();
			for (int j = 0; j < this.mBoard.mZombies.Count; j++)
			{
				this.mBoard.mZombies[j].PrepareForReuse();
			}
			this.mBoard.mZombies.Clear();
			for (int k = 0; k < this.mBoard.mPlants.Count; k++)
			{
				this.mBoard.mPlants[k].PrepareForReuse();
			}
			this.mBoard.mPlants.Clear();
			for (int l = 0; l < this.mBoard.mCoins.Count; l++)
			{
				this.mBoard.mCoins[l].PrepareForReuse();
			}
			this.mBoard.mCoins.Clear();
			for (int m = 0; m < this.mBoard.mProjectiles.Count; m++)
			{
				this.mBoard.mProjectiles[m].PrepareForReuse();
			}
			this.mBoard.mProjectiles.Clear();
			for (int n = 0; n < this.mBoard.mGridItems.Count; n++)
			{
				this.mBoard.mGridItems[n].PrepareForReuse();
			}
			this.mBoard.mGridItems.Clear();
			for (int num = 0; num < this.mBoard.mLawnMowers.Count; num++)
			{
				this.mBoard.mLawnMowers[num].PrepareForReuse();
			}
			this.mBoard.mLawnMowers.Clear();
			int num2 = -1;
			TodParticleSystem todParticleSystem = null;
			while (this.mBoard.IterateParticles(ref todParticleSystem, ref num2))
			{
				todParticleSystem.ParticleSystemDie();
			}
			int num3 = -1;
			Reanimation reanimation = null;
			while (this.mBoard.IterateReanimations(ref reanimation, ref num3))
			{
				if (reanimation.mReanimationType != ReanimationType.REANIM_CRAZY_DAVE)
				{
					reanimation.ReanimationDie();
				}
			}
			this.mBoard.mPoolSparklyParticleID = null;
			if (this.mUpsellChallengeScreen != null)
			{
				this.mUpsellChallengeScreen.Dispose();
				this.mUpsellChallengeScreen = null;
			}
		}

		public void AddUpsellZombie(ZombieType theZombieType, int thePixelX, int theGridY)
		{
			Zombie zombie = this.mBoard.AddZombieInRow(theZombieType, theGridY, 0);
			zombie.mPosX = (float)thePixelX;
			zombie.mPosY = zombie.GetPosYBasedOnRow(theGridY);
			zombie.SetRow(theGridY);
			zombie.mX = (int)zombie.mPosX;
			zombie.mY = (int)zombie.mPosY;
			if (this.mBoard.StageHasPool() && (theGridY == 2 || theGridY == 3))
			{
				zombie.mUsesClipping = true;
			}
		}

		public void LoadIntroBoard()
		{
			this.ClearUpsellBoard();
			this.mApp.mMuteSoundsForCutscene = true;
			this.mBoard.NewPlant(0, 1, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 2, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 2, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 3, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 3, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 4, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 0, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 1, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 2, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 2, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 4, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 5, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 0, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 1, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 3, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 3, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 4, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 5, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 0, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 4, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 2, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 2, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 1, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 4, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 5, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(6, 0, SeedType.SEED_SPIKEWEED, SeedType.SEED_NONE);
			this.mBoard.NewPlant(6, 4, SeedType.SEED_SPIKEWEED, SeedType.SEED_NONE);
			this.mBoard.NewPlant(7, 1, SeedType.SEED_SPIKEWEED, SeedType.SEED_NONE);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 460, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_FOOTBALL, 680, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, 730, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 810, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, 670, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 740, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 880, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 500, 2);
			this.AddUpsellZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, 680, 2);
			this.AddUpsellZombie(ZombieType.ZOMBIE_PAIL, 604, 3);
			this.AddUpsellZombie(ZombieType.ZOMBIE_SNORKEL, 880, 3);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 600, 4);
			this.AddUpsellZombie(ZombieType.ZOMBIE_PAIL, 690, 4);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 780, 4);
			this.AddUpsellZombie(ZombieType.ZOMBIE_CATAPULT, 730, 5);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 590, 5);
			this.mPreUpdatingBoard = true;
			for (int i = 0; i < 100; i++)
			{
				this.mBoard.Update();
			}
			this.mPreUpdatingBoard = false;
		}

		public void LoadUpsellBoardPool()
		{
			this.ClearUpsellBoard();
			this.mApp.mMuteSoundsForCutscene = true;
			this.mBoard.NewPlant(0, 1, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 2, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 2, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 3, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 3, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 4, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 0, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 1, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 2, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 2, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 4, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 5, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 0, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 1, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 3, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 3, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 4, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 5, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 4, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 0, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 2, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 2, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 1, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 4, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 5, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(6, 0, SeedType.SEED_SPIKEWEED, SeedType.SEED_NONE);
			this.mBoard.NewPlant(6, 3, SeedType.SEED_TANGLEKELP, SeedType.SEED_NONE);
			this.mBoard.NewPlant(6, 4, SeedType.SEED_SPIKEWEED, SeedType.SEED_NONE);
			this.mBoard.NewPlant(6, 5, SeedType.SEED_SQUASH, SeedType.SEED_NONE);
			this.mBoard.NewPlant(7, 1, SeedType.SEED_SPIKEWEED, SeedType.SEED_NONE);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 460, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_ZAMBONI, 680, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, 670, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 740, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 500, 2);
			this.AddUpsellZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, 680, 2);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 604, 3);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 690, 4);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 740, 4);
			this.AddUpsellZombie(ZombieType.ZOMBIE_PAIL, 730, 5);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 590, 5);
			this.mPreUpdatingBoard = true;
			for (int i = 0; i < 100; i++)
			{
				this.mBoard.Update();
			}
			this.mPreUpdatingBoard = false;
			this.mApp.mMuteSoundsForCutscene = false;
		}

		public void LoadUpsellBoardFog()
		{
			this.ClearUpsellBoard();
			this.mApp.mMuteSoundsForCutscene = true;
			this.mBoard.mBackground = BackgroundType.BACKGROUND_4_FOG;
			this.mBoard.LoadBackgroundImages();
			this.mBoard.NewPlant(0, 1, SeedType.SEED_SUNSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 4, SeedType.SEED_SUNSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 0, SeedType.SEED_SUNSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 1, SeedType.SEED_SUNSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 2, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 2, SeedType.SEED_CACTUS, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 4, SeedType.SEED_SUNSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 5, SeedType.SEED_SUNSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 0, SeedType.SEED_CACTUS, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 4, SeedType.SEED_CACTUS, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 5, SeedType.SEED_FUMESHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 1, SeedType.SEED_FUMESHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 2, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 3, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 3, SeedType.SEED_CACTUS, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 5, SeedType.SEED_PUFFSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 0, SeedType.SEED_PUFFSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 1, SeedType.SEED_MAGNETSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 2, SeedType.SEED_SEASHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 5, SeedType.SEED_PUFFSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 1, SeedType.SEED_PUFFSHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 2, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 2, SeedType.SEED_PLANTERN, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 3, SeedType.SEED_SEASHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(6, 2, SeedType.SEED_SEASHROOM, SeedType.SEED_NONE);
			this.mBoard.NewPlant(6, 3, SeedType.SEED_SEASHROOM, SeedType.SEED_NONE);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 460, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 680, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_BALLOON, 780, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, 670, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_BALLOON, 640, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_PAIL, 640, 2);
			this.AddUpsellZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, 780, 3);
			this.AddUpsellZombie(ZombieType.ZOMBIE_BALLOON, 704, 4);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 690, 4);
			this.AddUpsellZombie(ZombieType.ZOMBIE_PAIL, 590, 5);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 740, 5);
			this.mPreUpdatingBoard = true;
			for (int i = 0; i < 100; i++)
			{
				this.mBoard.Update();
			}
			this.mPreUpdatingBoard = false;
			this.mApp.mMuteSoundsForCutscene = false;
		}

		public void LoadUpsellChallengeScreen()
		{
			this.ClearUpsellBoard();
		}

		public void LoadUpsellBoardRoof()
		{
			this.ClearUpsellBoard();
			this.mApp.mMuteSoundsForCutscene = true;
			this.mBoard.mBackground = BackgroundType.BACKGROUND_5_ROOF;
			this.mBoard.LoadBackgroundImages();
			this.mBoard.mPlantRow[0] = PlantRowType.PLANTROW_NORMAL;
			this.mBoard.mPlantRow[1] = PlantRowType.PLANTROW_NORMAL;
			this.mBoard.mPlantRow[2] = PlantRowType.PLANTROW_NORMAL;
			this.mBoard.mPlantRow[3] = PlantRowType.PLANTROW_NORMAL;
			this.mBoard.mPlantRow[4] = PlantRowType.PLANTROW_NORMAL;
			this.mBoard.mPlantRow[5] = PlantRowType.PLANTROW_DIRT;
			for (int i = 0; i < Constants.GRIDSIZEX; i++)
			{
				for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
				{
					if (this.mBoard.mPlantRow[j] == PlantRowType.PLANTROW_DIRT)
					{
						this.mBoard.mGridSquareType[i, j] = GridSquareType.GRIDSQUARE_DIRT;
					}
					else
					{
						this.mBoard.mGridSquareType[i, j] = GridSquareType.GRIDSQUARE_GRASS;
					}
				}
			}
			this.mBoard.NewPlant(0, 0, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 0, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 1, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 1, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 2, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 2, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 3, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 3, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 4, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(0, 4, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 0, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 0, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 1, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 1, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 2, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 2, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 3, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 3, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 4, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(1, 4, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 0, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 0, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 1, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 1, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 2, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 2, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 3, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 3, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 4, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(2, 4, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 1, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 1, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 2, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 2, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 3, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 3, SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 4, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(3, 4, SeedType.SEED_CABBAGEPULT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 0, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 0, SeedType.SEED_CHOMPER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 1, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 1, SeedType.SEED_CHOMPER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 2, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 2, SeedType.SEED_REPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(4, 3, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 2, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 2, SeedType.SEED_WALLNUT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 3, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 3, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 4, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
			this.mBoard.NewPlant(5, 4, SeedType.SEED_WALLNUT, SeedType.SEED_NONE);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 460, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 680, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_CATAPULT, 780, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, 670, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 580, 0);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 540, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_PAIL, 500, 1);
			this.AddUpsellZombie(ZombieType.ZOMBIE_PAIL, 640, 2);
			this.AddUpsellZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, 780, 3);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 380, 3);
			this.AddUpsellZombie(ZombieType.ZOMBIE_CATAPULT, 704, 4);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 690, 4);
			this.AddUpsellZombie(ZombieType.ZOMBIE_NORMAL, 590, 4);
			this.mPreUpdatingBoard = true;
			for (int k = 0; k < 100; k++)
			{
				this.mBoard.Update();
			}
			this.mPreUpdatingBoard = false;
			this.mApp.mMuteSoundsForCutscene = false;
		}

		public bool ShouldRunUpsellBoard()
		{
			return (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL || this.mApp.mGameMode == GameMode.GAMEMODE_INTRO) && !this.mUpsellHideBoard;
		}

		public void DrawUpsell(Graphics g)
		{
			if (this.mCrazyDaveLastTalkIndex == 3315)
			{
				Reanimation newReanimation = Reanimation.GetNewReanimation();
				newReanimation.ReanimationInitializeType((float)Constants.CutScene_Upsell_TerraCotta_Pot.X, (float)Constants.CutScene_Upsell_TerraCotta_Pot.Y, ReanimationType.REANIM_FLOWER_POT);
				newReanimation.SetFramesForLayer("anim_zengarden");
				newReanimation.OverrideScale(1.3f, 1.3f);
				newReanimation.Draw(g);
				this.mBoard.mMenuButton.Draw(g);
				newReanimation.PrepareForReuse();
			}
			if (this.mUpsellChallengeScreen != null)
			{
				this.mUpsellChallengeScreen.Draw(g);
				this.mBoard.mMenuButton.Draw(g);
			}
		}

		public void DrawIntro(Graphics g)
		{
			if (this.mCutsceneTime <= CutScene.TimeIntro_PanRightStart)
			{
				g.SetColorizeImages(true);
				g.SetColor(SexyColor.Black);
				g.FillRect(-this.mBoard.mX, -this.mBoard.mY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
				g.SetColorizeImages(false);
			}
			int num = CutScene.TimeIntro_PanRightStart - 1000;
			if (this.mCutsceneTime > CutScene.TimeIntro_PresentsFadeIn && this.mCutsceneTime <= num)
			{
				int theAlpha;
				if (this.mCutsceneTime < num - 600)
				{
					theAlpha = TodCommon.TodAnimateCurve(CutScene.TimeIntro_PresentsFadeIn, CutScene.TimeIntro_PresentsFadeIn + 300, this.mCutsceneTime, 0, Constants.CutScene_LogoEndPos, TodCurves.CURVE_LINEAR);
				}
				else
				{
					theAlpha = TodCommon.TodAnimateCurve(num - 600, num - 300, this.mCutsceneTime, 255, 0, TodCurves.CURVE_LINEAR);
				}
				SexyColor theColor = new SexyColor(255, 255, 255, theAlpha);
				TodCommon.TodDrawString(g, "[INTRO_PRESENTS]", Constants.BOARD_WIDTH / 2 - this.mBoard.mX, (int)(310f * Constants.S) - this.mBoard.mY - 40, Resources.FONT_BRIANNETOD16, theColor, DrawStringJustification.DS_ALIGN_CENTER);
			}
			if (this.mCutsceneTime > CutScene.TimeIntro_LogoStart && this.mCutsceneTime <= CutScene.TimeIntro_PanRightEnd)
			{
				float num2 = TodCommon.TodAnimateCurveFloat(CutScene.TimeIntro_LogoStart, CutScene.TimeIntro_LogoEnd, this.mCutsceneTime, 5f, 1f, TodCurves.CURVE_EASE_OUT);
				TRect theRect = new TRect(Constants.BOARD_WIDTH / 2 - this.mBoard.mX - (int)((float)Constants.BOARD_WIDTH * 0.5f * num2), Constants.BOARD_HEIGHT / 2 - this.mBoard.mY - (int)(75f * num2), (int)((float)Constants.BOARD_WIDTH * num2), (int)((float)Constants.CutScene_LogoBackRect_Height * num2));
				g.SetColor(new SexyColor(0, 0, 0, 128));
				g.SetColorizeImages(true);
				g.FillRect(theRect);
				g.SetColorizeImages(false);
				TodCommon.TodDrawImageScaledF(g, Resources.IMAGE_PVZ_LOGO, (float)(Constants.BOARD_WIDTH / 2 - this.mBoard.mX) - (float)Resources.IMAGE_PVZ_LOGO.mWidth * 0.5f * num2, (float)(Constants.BOARD_HEIGHT / 2 - this.mBoard.mY) - (float)Resources.IMAGE_PVZ_LOGO.mHeight * 0.5f * num2, num2, num2);
			}
			if (this.mCutsceneTime > CutScene.TimeIntro_FadeOut && this.mCutsceneTime <= CutScene.TimeIntro_FadeOutEnd)
			{
				int theAlpha2 = TodCommon.TodAnimateCurve(CutScene.TimeIntro_FadeOut, CutScene.TimeIntro_FadeOutEnd, this.mCutsceneTime, 0, 255, TodCurves.CURVE_LINEAR);
				g.SetColor(new SexyColor(0, 0, 0, theAlpha2));
				g.SetColorizeImages(true);
				g.FillRect(-this.mBoard.mX, -this.mBoard.mY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
			}
			if (this.mCutsceneTime > CutScene.TimeIntro_FadeOutEnd)
			{
				g.SetColor(SexyColor.Black);
				g.SetColorizeImages(true);
				g.FillRect(-this.mBoard.mX, -this.mBoard.mY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
			}
		}

		public void UpdateIntro()
		{
			int num = TodCommon.TodAnimateCurve(CutScene.TimeIntro_PanRightStart, CutScene.TimeIntro_PanRightEnd, this.mCutsceneTime, -100, 100, TodCurves.CURVE_LINEAR);
			this.mBoard.Move((int)((float)(-(float)num) * Constants.S), 0);
			if (this.mCutsceneTime == 10)
			{
				this.LoadIntroBoard();
			}
			if (this.mCutsceneTime == CutScene.TimeIntro_FadeOut)
			{
				this.mApp.mMusic.FadeOut(250);
			}
			if (this.mCutsceneTime == CutScene.TimeIntro_LogoEnd)
			{
				this.mApp.AddTodParticle((float)Constants.CutScene_LogoEnd_Particle_Pos.X, (float)Constants.CutScene_LogoEnd_Particle_Pos.Y, 400000, ParticleEffect.PARTICLE_SCREEN_FLASH);
				this.mApp.mMuteSoundsForCutscene = false;
				this.mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
				this.mApp.mMuteSoundsForCutscene = true;
			}
			if (this.mCutsceneTime == CutScene.TimeIntro_FadeOut - 200)
			{
				this.mApp.mMuteSoundsForCutscene = false;
				this.mApp.PlaySample(Resources.SOUND_SIREN);
				this.mApp.mMuteSoundsForCutscene = true;
			}
			if (this.mCutsceneTime == CutScene.TimeIntro_End)
			{
				this.mApp.PreNewGame(GameMode.GAMEMODE_ADVENTURE, false);
			}
		}

		internal static int TimePanRightStart = 1500;

		internal static int TimePanRightEnd = CutScene.TimePanRightStart + 2000;

		internal static int TimeEarlyDaveEnterStart = CutScene.TimePanRightStart + 500;

		internal static int TimeEarlyDaveEnterEnd = CutScene.TimeEarlyDaveEnterStart + 750;

		internal static int TimeEarlyDaveLeaveStart = CutScene.TimeEarlyDaveEnterEnd + 500;

		internal static int TimeEarlyDaveLeaveEnd = CutScene.TimeEarlyDaveLeaveStart + 750;

		internal static int TimeSeedChoserSlideOnStart = CutScene.TimePanRightEnd + 500;

		internal static int TimeSeedChoserSlideOnEnd = CutScene.TimeSeedChoserSlideOnStart + 250;

		internal static int TimeSeedChoserSlideOffStart = CutScene.TimeSeedChoserSlideOnEnd + 250;

		internal static int TimeSeedChoserSlideOffEnd = CutScene.TimeSeedChoserSlideOffStart + 250;

		internal static int TimeSeedBankOnStart = CutScene.TimeSeedChoserSlideOnStart;

		internal static int TimeSeedBankOnEnd = CutScene.TimeSeedChoserSlideOnEnd;

		internal static int TimePanLeftStart = CutScene.TimeSeedChoserSlideOffStart;

		internal static int TimePanLeftEnd = CutScene.TimePanLeftStart + 1500;

		internal static int TimeSeedBankRightStart = CutScene.TimeSeedChoserSlideOffEnd;

		internal static int TimeSeedBankRightEnd = CutScene.TimePanLeftEnd;

		internal static int TimeRollSodStart = CutScene.TimePanLeftEnd;

		internal static int TimeRollSodEnd = CutScene.TimeRollSodStart + 2000;

		internal static int TimeGraveStoneStart = CutScene.TimePanLeftEnd;

		internal static int TimeGraveStoneEnd = CutScene.TimeRollSodStart + 1000;

		internal static int[] TimeLawnMowerStart = new int[]
		{
			CutScene.TimePanLeftEnd + 300,
			CutScene.TimePanLeftEnd + 250,
			CutScene.TimePanLeftEnd + 200,
			CutScene.TimePanLeftEnd + 150,
			CutScene.TimePanLeftEnd + 100,
			CutScene.TimePanLeftEnd + 50
		};

		internal static int TimeLawnMowerDuration = 250;

		internal static int TimeReadySetPlantStart = CutScene.TimePanLeftEnd;

		internal static int TimeReadySetPlantEnd = CutScene.TimeReadySetPlantStart + 1830;

		internal static int TimeFogRollIn = CutScene.TimePanLeftEnd - 50;

		internal static int TimeCrazyDaveEnterStart = CutScene.TimePanLeftEnd + 500;

		internal static int TimeCrazyDaveEnterEnd = CutScene.TimeCrazyDaveEnterStart + 750;

		internal static int TimeCrazyDaveLeaveStart = CutScene.TimeCrazyDaveEnterEnd + 500;

		internal static int TimeCrazyDaveLeaveEnd = CutScene.TimeCrazyDaveLeaveStart + 750;

		internal static int TimeIntroEnd = CutScene.TimeReadySetPlantStart;

		internal static int LostTimePanRightStart = 1500;

		internal static int LostTimePanRightEnd = CutScene.TimePanRightStart + 2000;

		internal static int LostTimeBrainGraphicStart = CutScene.LostTimePanRightEnd + 3500;

		internal static int LostTimeBrainGraphicShake = CutScene.LostTimeBrainGraphicStart + 1000;

		internal static int LostTimeBrainGraphicCancelShake = CutScene.LostTimeBrainGraphicShake + 1000;

		internal static int LostTimeBrainGraphicEnd = CutScene.LostTimeBrainGraphicCancelShake + 3000;

		internal static int LostTimeEnd = CutScene.LostTimeBrainGraphicEnd;

		internal static int TimeIntro_PresentsFadeIn = 1000;

		internal static int TimeIntro_LogoStart = CutScene.TimeIntro_PresentsFadeIn + 4500;

		internal static int TimeIntro_LogoEnd = CutScene.TimeIntro_LogoStart + 400;

		internal static int TimeIntro_PanRightStart = CutScene.TimeIntro_PresentsFadeIn + 4890;

		internal static int TimeIntro_PanRightEnd = CutScene.TimeIntro_PanRightStart + 6000;

		internal static int TimeIntro_FadeOut = CutScene.TimeIntro_PanRightStart + 5000;

		internal static int TimeIntro_FadeOutEnd = CutScene.TimeIntro_FadeOut + 1000;

		internal static int TimeIntro_End = CutScene.TimeIntro_FadeOutEnd + 2000;

		public LawnApp mApp;

		public Board mBoard;

		public int mCutsceneTime;

		public int mSodTime;

		public int mGraveStoneTime;

		public int mReadySetPlantTime;

		public int mFogTime;

		public int mBossTime;

		public int mCrazyDaveTime;

		public int mLawnMowerTime;

		public int mCrazyDaveDialogStart;

		public bool mSeedChoosing;

		public Reanimation mZombiesWonReanimID;

		public bool mPreloaded;

		public bool mPlacedZombies;

		public bool mPlacedLawnItems;

		public int mCrazyDaveCountDown;

		public int mCrazyDaveLastTalkIndex;

		public bool mUpsellHideBoard;

		private ChallengeScreen mUpsellChallengeScreen;

		public bool mPreUpdatingBoard;

		private static TodWeightedGridArray[] aPicks = new TodWeightedGridArray[25];
	}
}
