using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class ChallengeScreen : Widget, ButtonListener
	{
		public ChallengeScreen(LawnApp theApp, ChallengePage thePage)
		{
			mApp = theApp;
			mClip = false;
			mCheatEnableChallenges = false;
			mPageIndex = thePage;
			mUnlockState = UnlockingState.UNLOCKING_OFF;
			mUnlockChallengeIndex = -1;
			mUnlockStateCounter = 0;
			mLockShakeX = 0f;
			mLockShakeY = 0f;
			mBackButton = GameButton.MakeNewButton(100, this, "[BACK_TO_MENU]", null, AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2, AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2_GLOW, AtlasResources.IMAGE_SEEDCHOOSER_BUTTON2_GLOW);
			mBackButton.Resize(18, 366, 111, 26);
			mBackButton.mTextDownOffsetX = 1;
			mBackButton.mTextDownOffsetY = 1;
			mBackButton.mColors[0] = new SexyColor(42, 42, 90);
			mBackButton.mColors[1] = new SexyColor(42, 42, 90);
			mChallengeScreenWidget = new ChallengeScreenWidget(mApp);
			mChallengeScreenWidget.Resize(base.M(0), base.M1(0), base.M2(800), base.M3(415));
			mScrollWidget = new ScrollWidget();
			mScrollWidget.AddWidget(mChallengeScreenWidget);
			mScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			mScrollWidget.Resize(base.M(0), base.M1(65), base.M2(800), base.M3(415));
			AddWidget(mScrollWidget);
			for (int i = 0; i < 4; i++)
			{
				mPageButton[i] = new ButtonWidget(300 + i, this);
				mPageButton[i].mDoFinger = true;
				if (i == 2)
				{
					mPageButton[i].mLabel = TodStringFile.TodStringTranslate("Limbo Page");
				}
				else
				{
					mPageButton[i].mLabel = TodCommon.TodReplaceNumberString("[PAGE_X]", "{PAGE}", i);
				}
				mPageButton[i].mButtonImage = AtlasResources.IMAGE_BLANK;
				mPageButton[i].mOverImage = AtlasResources.IMAGE_BLANK;
				mPageButton[i].mDownImage = AtlasResources.IMAGE_BLANK;
				mPageButton[i].SetFont(Resources.FONT_BRIANNETOD12);
				mPageButton[i].mColors[0] = new SexyColor(255, 240, 0);
				mPageButton[i].mColors[1] = new SexyColor(220, 220, 0);
				mPageButton[i].Resize(base.S(200 + i * 100), base.S(540), base.S(100), base.S(75));
				if (!ShowPageButtons() || i == 0 || i == 3)
				{
					mPageButton[i].mVisible = false;
				}
			}
			for (int j = 0; j < ChallengeScreen.gChallengeDefs.Length; j++)
			{
				ChallengeDefinition challengeDefinition = ChallengeScreen.gChallengeDefs[j];
				mChallengeButton[j] = new ButtonWidget(200 + j, this);
				mChallengeButton[j].mDoFinger = true;
				mChallengeButton[j].mFrameNoDraw = true;
				if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_CHALLENGE || challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_LIMBO || challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_PUZZLE)
				{
					mChallengeButton[j].Resize(base.M(75) + challengeDefinition.mCol * base.M1(150), base.M2(15) + challengeDefinition.mRow * base.M3(95), 50, 60);
				}
				else
				{
					mChallengeButton[j].Resize(base.S(38 + challengeDefinition.mCol * 155), base.S(125 + challengeDefinition.mRow * 145), base.S(104), base.S(115));
				}
				if (MoreTrophiesNeeded(j) != 0)
				{
					mChallengeButton[j].mDoFinger = false;
					mChallengeButton[j].mDisabled = true;
				}
			}
			UpdateButtons();
			if (mApp.mGameMode != GameMode.GAMEMODE_UPSELL || mApp.mGameScene != GameScenes.SCENE_LEVEL_INTRO)
			{
				mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_CHOOSE_YOUR_SEEDS);
			}
			if (mPageIndex == ChallengePage.CHALLENGE_PAGE_SURVIVAL && mApp.mPlayerInfo.mHasNewSurvival)
			{
				SetUnlockChallengeIndex(mPageIndex, false);
				mApp.mPlayerInfo.mHasNewSurvival = false;
				return;
			}
			if (mPageIndex == ChallengePage.CHALLENGE_PAGE_CHALLENGE && mApp.mPlayerInfo.mHasNewMiniGame)
			{
				SetUnlockChallengeIndex(mPageIndex, false);
				mApp.mPlayerInfo.mHasNewMiniGame = false;
				return;
			}
			if (mPageIndex == ChallengePage.CHALLENGE_PAGE_PUZZLE && mApp.mPlayerInfo.mHasNewVasebreaker)
			{
				SetUnlockChallengeIndex(mPageIndex, false);
				mApp.mPlayerInfo.mHasNewVasebreaker = false;
				return;
			}
			if (mPageIndex == ChallengePage.CHALLENGE_PAGE_PUZZLE && mApp.mPlayerInfo.mHasNewIZombie)
			{
				SetUnlockChallengeIndex(mPageIndex, true);
				mApp.mPlayerInfo.mHasNewIZombie = false;
			}
		}

		public override bool BackButtonPress()
		{
			mApp.KillChallengeScreen();
			mApp.DoBackToMain();
			return true;
		}

		public void SetUnlockChallengeIndex(ChallengePage thePage, bool aIsIZombie)
		{
			mUnlockState = UnlockingState.UNLOCKING_SHAKING;
			mUnlockStateCounter = 100;
			mUnlockChallengeIndex = 0;
			for (int i = 0; i < ChallengeScreen.gChallengeDefs.Length; i++)
			{
				ChallengeDefinition challengeDefinition = ChallengeScreen.gChallengeDefs[i];
				if (challengeDefinition.mPage == thePage && (thePage != ChallengePage.CHALLENGE_PAGE_PUZZLE || ((aIsIZombie || IsScaryPotterLevel(challengeDefinition.mChallengeMode)) && (!aIsIZombie || IsIZombieLevel(challengeDefinition.mChallengeMode)))))
				{
					int num = AccomplishmentsNeeded(i);
					if (num <= 0)
					{
						mUnlockChallengeIndex = i;
					}
				}
			}
		}

		public int MoreTrophiesNeeded(int aChallengeIndex)
		{
			ChallengeDefinition challengeDefinition = ChallengeScreen.gChallengeDefs[aChallengeIndex];
			if (mApp.mGameMode == GameMode.GAMEMODE_UPSELL && mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO)
			{
				if (challengeDefinition.mChallengeMode == GameMode.GAMEMODE_CHALLENGE_FINAL_BOSS)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (mApp.IsTrialStageLocked())
				{
					if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_PUZZLE && challengeDefinition.mChallengeMode >= GameMode.GAMEMODE_SCARY_POTTER_4)
					{
						if (challengeDefinition.mChallengeMode == GameMode.GAMEMODE_SCARY_POTTER_4)
						{
							return 1;
						}
						return 2;
					}
					else if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_CHALLENGE && challengeDefinition.mChallengeMode >= GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
					{
						if (challengeDefinition.mChallengeMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
						{
							return 1;
						}
						return 2;
					}
					else if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_SURVIVAL && challengeDefinition.mChallengeMode >= GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_4)
					{
						if (challengeDefinition.mChallengeMode == GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_4)
						{
							return 1;
						}
						return 2;
					}
				}
				if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_PUZZLE && IsScaryPotterLevel(challengeDefinition.mChallengeMode))
				{
					int num = 0;
					for (int i = 0; i < ChallengeScreen.gChallengeDefs.Length; i++)
					{
						ChallengeDefinition challengeDefinition2 = ChallengeScreen.gChallengeDefs[i];
						if (IsScaryPotterLevel(challengeDefinition2.mChallengeMode) && mApp.HasBeatenChallenge(challengeDefinition2.mChallengeMode))
						{
							num++;
						}
					}
					if (challengeDefinition.mChallengeMode < GameMode.GAMEMODE_SCARY_POTTER_4 || mApp.HasFinishedAdventure() || num < 3)
					{
						int num2 = challengeDefinition.mChallengeMode - GameMode.GAMEMODE_SCARY_POTTER_1;
						return TodCommon.ClampInt(num2 - num, 0, 9);
					}
					if (challengeDefinition.mChallengeMode == GameMode.GAMEMODE_SCARY_POTTER_4)
					{
						return 1;
					}
					return 2;
				}
				else if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_PUZZLE && IsIZombieLevel(challengeDefinition.mChallengeMode))
				{
					int num3 = 0;
					for (int j = 0; j < ChallengeScreen.gChallengeDefs.Length; j++)
					{
						ChallengeDefinition challengeDefinition3 = ChallengeScreen.gChallengeDefs[j];
						if (IsIZombieLevel(challengeDefinition3.mChallengeMode) && mApp.HasBeatenChallenge(challengeDefinition3.mChallengeMode))
						{
							num3++;
						}
					}
					if (challengeDefinition.mChallengeMode < GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4 || mApp.HasFinishedAdventure() || num3 < 3)
					{
						int num4 = challengeDefinition.mChallengeMode - GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1;
						return TodCommon.ClampInt(num4 - num3, 0, 9);
					}
					if (challengeDefinition.mChallengeMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4)
					{
						return 1;
					}
					return 2;
				}
				else
				{
					int num5 = challengeDefinition.mRow * 5 + challengeDefinition.mCol;
					if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_CHALLENGE && !mApp.HasFinishedAdventure())
					{
						if (num5 < 3)
						{
							return 0;
						}
						if (num5 == 3)
						{
							return 1;
						}
						return 2;
					}
					else if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_SURVIVAL && !mApp.HasFinishedAdventure())
					{
						if (num5 < 3)
						{
							return 0;
						}
						if (num5 == 3)
						{
							return 1;
						}
						return 2;
					}
					else
					{
						int numTrophies = mApp.GetNumTrophies(challengeDefinition.mPage);
						int num6 = 0;
						if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_LIMBO)
						{
							return 0;
						}
						if (mApp.IsSurvivalEndless(challengeDefinition.mChallengeMode))
						{
							return 10 - numTrophies;
						}
						if (challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_SURVIVAL || challengeDefinition.mPage == ChallengePage.CHALLENGE_PAGE_CHALLENGE)
						{
							num6 = 3;
						}
						int num7 = numTrophies + num6;
						if (num5 >= num7)
						{
							return num5 - num7 + 1;
						}
						return 0;
					}
				}
			}
		}

		public bool ShowPageButtons()
		{
			return mApp.mTodCheatKeys && mPageIndex != ChallengePage.CHALLENGE_PAGE_SURVIVAL && mPageIndex != ChallengePage.CHALLENGE_PAGE_PUZZLE;
		}

		public void UpdateButtons()
		{
			for (int i = 0; i < ChallengeScreen.gChallengeDefs.Length; i++)
			{
				ChallengeDefinition challengeDefinition = ChallengeScreen.gChallengeDefs[i];
				if (challengeDefinition.mPage != mPageIndex)
				{
					mChallengeButton[i].mVisible = false;
				}
				else
				{
					mChallengeButton[i].mVisible = true;
				}
			}
			for (int j = 0; j < 4; j++)
			{
				if (j == (int)mPageIndex)
				{
					mPageButton[j].mColors[0] = new SexyColor(64, 64, 64);
					mPageButton[j].mDisabled = true;
				}
				else
				{
					mPageButton[j].mColors[0] = new SexyColor(255, 240, 0);
					mPageButton[j].mDisabled = false;
				}
			}
		}

		public bool ChallengeModeRecordsTime(GameMode theGameMode)
		{
			return theGameMode >= GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1 && theGameMode <= GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_5;
		}

		public int AccomplishmentsNeeded(int aChallengeIndex)
		{
			int num = MoreTrophiesNeeded(aChallengeIndex);
			ChallengeDefinition challengeDefinition = ChallengeScreen.gChallengeDefs[aChallengeIndex];
			if (mApp.IsSurvivalEndless(challengeDefinition.mChallengeMode) && num <= 3 && mApp.GetNumTrophies(ChallengePage.CHALLENGE_PAGE_SURVIVAL) < 10 && mApp.HasFinishedAdventure() && !mApp.IsTrialStageLocked())
			{
				num = 1;
			}
			if (mCheatEnableChallenges)
			{
				num = 0;
			}
			return num;
		}

		public override void Draw(Graphics g)
		{
			g.SetLinearBlend(true);
			LawnCommon.DrawImageBox(g, new TRect(0, 0, mWidth, mHeight), AtlasResources.IMAGE_ALMANAC_ROUNDED_OUTLINE);
			if (mPageIndex == ChallengePage.CHALLENGE_PAGE_SURVIVAL)
			{
				TodCommon.TodDrawString(g, "[PICK_AREA]", 400, 22, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(220, 220, 220), DrawStringJustification.DS_ALIGN_CENTER);
			}
			else if (mPageIndex == ChallengePage.CHALLENGE_PAGE_PUZZLE)
			{
				TodCommon.TodDrawString(g, "[SCARY_POTTER]", 400, 22, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(220, 220, 220), DrawStringJustification.DS_ALIGN_CENTER);
			}
			else
			{
				TodCommon.TodDrawString(g, "[PICK_CHALLENGE]", 400, 22, Resources.FONT_DWARVENTODCRAFT15, new SexyColor(220, 220, 220), DrawStringJustification.DS_ALIGN_CENTER);
			}
			int num = 0;
			int numTrophies = mApp.GetNumTrophies(mPageIndex);
			if (mPageIndex == ChallengePage.CHALLENGE_PAGE_SURVIVAL)
			{
				num = 10;
			}
			else if (mPageIndex == ChallengePage.CHALLENGE_PAGE_CHALLENGE)
			{
				num = 20;
			}
			else if (mPageIndex == ChallengePage.CHALLENGE_PAGE_PUZZLE)
			{
				num = 18;
			}
			if (num > 0)
			{
				if (trophyStrings[(int)mPageIndex] == null)
				{
					trophyStrings[(int)mPageIndex] = LawnApp.ToString(numTrophies) + "/" + LawnApp.ToString(num);
				}
				string theText = trophyStrings[(int)mPageIndex];
				TodCommon.TodDrawString(g, theText, 600, 39, Resources.FONT_DWARVENTODCRAFT12, new SexyColor(255, 240, 0), DrawStringJustification.DS_ALIGN_CENTER);
			}
			TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_TROPHY, 580f, 15f, 0.5f, 0.5f);
		}

		public override void Update()
		{
			base.Update();
			UpdateToolTip();
			if (mUnlockStateCounter > 0)
			{
				mUnlockStateCounter--;
			}
			if (mUnlockState == UnlockingState.UNLOCKING_SHAKING)
			{
				if (mUnlockStateCounter == 0)
				{
					mApp.PlayFoley(FoleyType.FOLEY_PAPER);
					mUnlockState = UnlockingState.UNLOCKING_FADING;
					mUnlockStateCounter = 50;
					mLockShakeX = 0f;
					mLockShakeY = 0f;
				}
				else
				{
					mLockShakeX = TodCommon.RandRangeFloat(-2f, 2f);
					mLockShakeY = TodCommon.RandRangeFloat(-2f, 2f);
				}
			}
			else if (mUnlockState == UnlockingState.UNLOCKING_FADING && mUnlockStateCounter == 0)
			{
				mUnlockState = UnlockingState.UNLOCKING_OFF;
				mUnlockStateCounter = 0;
				mUnlockChallengeIndex = -1;
			}
			MarkDirty();
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			AddWidget(mBackButton);
			for (int i = 0; i < ChallengeScreen.gChallengeDefs.Length; i++)
			{
				mChallengeScreenWidget.AddWidget(mChallengeButton[i]);
			}
			for (int j = 0; j < 4; j++)
			{
				AddWidget(mPageButton[j]);
			}
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			RemoveWidget(mBackButton);
			for (int i = 0; i < 122; i++)
			{
				RemoveWidget(mChallengeButton[i]);
			}
			for (int j = 0; j < 4; j++)
			{
				RemoveWidget(mPageButton[j]);
			}
		}

		public void ButtonPress(int theId)
		{
			mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
		}

		public void ButtonDepress(int theId)
		{
			if (theId == 100)
			{
				mApp.KillChallengeScreen();
				mApp.DoBackToMain();
			}
			int num = theId - 200;
			if (num >= 0 && num < 122)
			{
				GameMode theGameMode = num + GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1;
				mApp.KillChallengeScreen();
				mApp.PreNewGame(theGameMode, true);
			}
			int num2 = theId - 300;
			if (num2 >= 0 && num2 < 4)
			{
				mPageIndex = (ChallengePage)num2;
				UpdateButtons();
			}
		}

		public bool IsScaryPotterLevel(GameMode theGameMode)
		{
			return theGameMode >= GameMode.GAMEMODE_SCARY_POTTER_1 && theGameMode <= GameMode.GAMEMODE_SCARY_POTTER_ENDLESS;
		}

		public bool IsIZombieLevel(GameMode theGameMode)
		{
			return theGameMode >= GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 && theGameMode <= GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS;
		}

		public void UpdateToolTip()
		{
		}

		public void ButtonMouseMove(int id, int x, int y)
		{
		}

		public void ButtonMouseLeave(int id)
		{
		}

		public void ButtonMouseEnter(int id)
		{
		}

		public void ButtonMouseTick(int id)
		{
		}

		public void ButtonPress(int id, int id2)
		{
		}

		public void ButtonDownTick(int id)
		{
		}

		private const int NUM_TROPHIES_SURVIVAL = 10;

		private const int NUM_TROPHIES_CHALLENGE = 20;

		private const int NUM_TROPHIES_PUZZLE = 18;

		public static ChallengeDefinition[] gChallengeDefs = new ChallengeDefinition[]
		{
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1, 0, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 0, 0, "[SURVIVAL_DAY_NORMAL]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_2, 1, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 0, 1, "[SURVIVAL_NIGHT_NORMAL]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_3, 2, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 0, 2, "[SURVIVAL_POOL_NORMAL]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_4, 3, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 0, 3, "[SURVIVAL_FOG_NORMAL]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_5, 4, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 0, 4, "[SURVIVAL_ROOF_NORMAL]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_1, 5, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 1, 0, "[SURVIVAL_DAY_HARD]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_2, 6, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 1, 1, "[SURVIVAL_NIGHT_HARD]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_3, 7, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 1, 2, "[SURVIVAL_POOL_HARD]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_4, 8, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 1, 3, "[SURVIVAL_FOG_HARD]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_5, 9, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 1, 4, "[SURVIVAL_ROOF_HARD]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_1, 10, ChallengePage.CHALLENGE_PAGE_LIMBO, 3, 0, "[SURVIVAL_DAY_ENDLESS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_2, 10, ChallengePage.CHALLENGE_PAGE_LIMBO, 3, 1, "[SURVIVAL_NIGHT_ENDLESS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_3, 10, ChallengePage.CHALLENGE_PAGE_SURVIVAL, 2, 2, "[SURVIVAL_POOL_ENDLESS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_4, 10, ChallengePage.CHALLENGE_PAGE_LIMBO, 3, 2, "[SURVIVAL_FOG_ENDLESS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_5, 10, ChallengePage.CHALLENGE_PAGE_LIMBO, 3, 3, "[SURVIVAL_ROOF_ENDLESS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS, 0, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 0, 0, "[WAR_AND_PEAS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING, 6, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 0, 1, "[WALL_NUT_BOWLING]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_SLOT_MACHINE, 2, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 0, 2, "[SLOT_MACHINE]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS, 3, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 0, 3, "[ITS_RAINING_SEEDS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_BEGHOULED, 1, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 0, 4, "[BEGHOULED]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL, 8, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 1, 0, "[INVISIGHOUL]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_SEEING_STARS, 5, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 1, 1, "[SEEING_STARS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST, 20, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 1, 3, "[BEGHOULED_TWIST]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_LITTLE_TROUBLE, 12, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 1, 4, "[LITTLE_TROUBLE]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT, 15, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 2, 0, "[PORTAL_COMBAT]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_COLUMN, 4, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 2, 1, "[COLUMN_AS_YOU_SEE_EM]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA, 17, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 2, 2, "[BOBSLED_BONANZA]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_SPEED, 18, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 2, 3, "[ZOMBIES_ON_SPEED]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_WHACK_A_ZOMBIE, 16, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 2, 4, "[WHACK_A_ZOMBIE]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_LAST_STAND, 21, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 3, 0, "[LAST_STAND]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS_2, 0, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 3, 1, "[WAR_AND_PEAS_2]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING_2, 6, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 3, 2, "[WALL_NUT_BOWLING_EXTREME]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_POGO_PARTY, 14, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 3, 3, "[POGO_PARTY]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_FINAL_BOSS, 19, ChallengePage.CHALLENGE_PAGE_CHALLENGE, 3, 4, "[FINAL_BOSS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1, 0, ChallengePage.CHALLENGE_PAGE_LIMBO, 0, 0, "[ART_CHALLENGE_WALL_NUT]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_SUNNY_DAY, 1, ChallengePage.CHALLENGE_PAGE_LIMBO, 0, 1, "[SUNNY_DAY]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_RESODDED, 2, ChallengePage.CHALLENGE_PAGE_LIMBO, 0, 2, "[UNSODDED]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_BIG_TIME, 3, ChallengePage.CHALLENGE_PAGE_LIMBO, 0, 3, "[BIG_TIME]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_2, 4, ChallengePage.CHALLENGE_PAGE_LIMBO, 0, 4, "[ART_CHALLENGE_SUNFLOWER]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_AIR_RAID, 5, ChallengePage.CHALLENGE_PAGE_LIMBO, 1, 0, "[AIR_RAID]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_ICE, 6, ChallengePage.CHALLENGE_PAGE_LIMBO, 1, 1, "[ICE_LEVEL]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN, 7, ChallengePage.CHALLENGE_PAGE_LIMBO, 1, 2, "[ZEN_GARDEN]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY, 8, ChallengePage.CHALLENGE_PAGE_LIMBO, 1, 3, "[HIGH_GRAVITY]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_GRAVE_DANGER, 11, ChallengePage.CHALLENGE_PAGE_LIMBO, 1, 4, "[GRAVE_DANGER]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_SHOVEL, 10, ChallengePage.CHALLENGE_PAGE_LIMBO, 2, 0, "[CAN_YOU_DIG_IT]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_STORMY_NIGHT, 13, ChallengePage.CHALLENGE_PAGE_LIMBO, 2, 1, "[DARK_STORMY_NIGHT]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_BUNGEE_BLITZ, 9, ChallengePage.CHALLENGE_PAGE_LIMBO, 2, 2, "[BUNGEE_BLITZ]"),
			new ChallengeDefinition(GameMode.GAMEMODE_CHALLENGE_SQUIRREL, 10, ChallengePage.CHALLENGE_PAGE_LIMBO, 2, 3, "Squirrel"),
			new ChallengeDefinition(GameMode.GAMEMODE_TREE_OF_WISDOM, 10, ChallengePage.CHALLENGE_PAGE_LIMBO, 2, 4, "Tree of Wisdom"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_1, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 0, 0, "[SCARY_POTTER_1]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_2, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 0, 1, "[SCARY_POTTER_2]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_3, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 0, 2, "[SCARY_POTTER_3]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_4, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 0, 3, "[SCARY_POTTER_4]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_5, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 0, 4, "[SCARY_POTTER_5]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_6, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 1, 0, "[SCARY_POTTER_6]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_7, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 1, 1, "[SCARY_POTTER_7]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_8, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 1, 2, "[SCARY_POTTER_8]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_9, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 1, 3, "[SCARY_POTTER_9]"),
			new ChallengeDefinition(GameMode.GAMEMODE_SCARY_POTTER_ENDLESS, 10, ChallengePage.CHALLENGE_PAGE_PUZZLE, 1, 4, "[SCARY_POTTER_ENDLESS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 2, 0, "[I_ZOMBIE_1]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 2, 1, "[I_ZOMBIE_2]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 2, 2, "[I_ZOMBIE_3]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 2, 3, "[I_ZOMBIE_4]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 2, 4, "[I_ZOMBIE_5]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 3, 0, "[I_ZOMBIE_6]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 3, 1, "[I_ZOMBIE_7]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 3, 2, "[I_ZOMBIE_8]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_9, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 3, 3, "[I_ZOMBIE_9]"),
			new ChallengeDefinition(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS, 11, ChallengePage.CHALLENGE_PAGE_PUZZLE, 3, 4, "[I_ZOMBIE_ENDLESS]"),
			new ChallengeDefinition(GameMode.GAMEMODE_UPSELL, 10, ChallengePage.CHALLENGE_PAGE_LIMBO, 3, 4, "Upsell"),
			new ChallengeDefinition(GameMode.GAMEMODE_INTRO, 10, ChallengePage.CHALLENGE_PAGE_LIMBO, 2, 3, "Intro")
		};

		public NewLawnButton mBackButton;

		public ButtonWidget[] mPageButton = new ButtonWidget[4];

		public ButtonWidget[] mChallengeButton = new ButtonWidget[122];

		public LawnApp mApp;

		public ChallengePage mPageIndex;

		public bool mCheatEnableChallenges;

		public UnlockingState mUnlockState;

		public int mUnlockStateCounter;

		public int mUnlockChallengeIndex;

		public float mLockShakeX;

		public float mLockShakeY;

		public ChallengeScreenWidget mChallengeScreenWidget;

		public ScrollWidget mScrollWidget;

		private string[] trophyStrings = new string[4];
	}
}
