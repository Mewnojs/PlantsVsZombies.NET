using System;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class GameSelector : Widget, ButtonListener, StoreListener, AlmanacListener, MiniGamesWidgetListener, QuickPlayWidgetListener
	{
		public GameSelector(LawnApp theApp)
		{
			this.mApp = theApp;
			this.mLevel = 1;
			this.mLoading = false;
			this.mHasTrophy = false;
			this.mDoNewGameAfterStore = false;
			this.mInUserDialog = false;
			this.mAdventureButton = GameButton.MakeNewButton(100, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ADVENTURE_BUTTON, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ADVENTURE_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ADVENTURE_HIGHLIGHT);
			this.mAdventureButton.Resize(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_AdventureButton_X, 10, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ADVENTURE_BUTTON.mWidth, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ADVENTURE_BUTTON.mHeight);
			this.mAdventureButton.mClip = false;
			this.mFadeInCounter = 0;
			this.mMoreWaysToPlayButton = GameButton.MakeNewButton(101, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS_HIGHLIGHT);
			this.mMoreWaysToPlayButton.Resize(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_MiniGameButton_X, Constants.GameSelector_MiniGameButton_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS.mWidth, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS.mHeight);
			this.mMoreWaysToPlayButton.mClip = false;
			this.mLeaderboardButton = GameButton.MakeNewButton(120, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_HIGHLIGHT);
			this.mLeaderboardButton.Resize(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_LeaderboardButton_X, Constants.GameSelector_LeaderboardButton_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD.mWidth, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD.mHeight);
			this.mLeaderboardButton.mClip = false;
			this.mZenGardenButton = GameButton.MakeNewButton(124, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ZENGARDEN, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ZENGARDEN_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ZENGARDEN_HIGHLIGHT);
			this.mZenGardenButton.Resize(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_ZenGardenButton_X, Constants.GameSelector_ZenGardenButton_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ZENGARDEN.mWidth, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ZENGARDEN.mHeight);
			this.mZenGardenButton.mClip = false;
			this.mOptionsButton = GameButton.MakeNewButton(109, this, "", null, AtlasResources.IMAGE_SELECTORSCREEN_OPTIONS1, AtlasResources.IMAGE_SELECTORSCREEN_OPTIONS2, AtlasResources.IMAGE_SELECTORSCREEN_OPTIONS2);
			this.mOptionsButton.Resize(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_OptionsButton_X, Constants.GameSelector_OptionsButton_Y, AtlasResources.IMAGE_SELECTORSCREEN_OPTIONS1.mWidth, AtlasResources.IMAGE_SELECTORSCREEN_OPTIONS1.mHeight + 25);
			this.mUserDialogButton = GameButton.MakeNewButton(113, this, string.Empty, null, AtlasResources.IMAGE_BLANK, AtlasResources.IMAGE_BLANK, AtlasResources.IMAGE_BLANK);
			this.mUserDialogButton.Resize(Constants.MAIN_MENU_ORIGIN_X, (int)Constants.InvertAndScale(100f), (int)Constants.InvertAndScale(210f), (int)Constants.InvertAndScale(30f));
			this.mStoreButton = GameButton.MakeNewButton(112, this, "", null, AtlasResources.IMAGE_SELECTORSCREEN_STORE, AtlasResources.IMAGE_SELECTORSCREEN_STOREHIGHLIGHT, AtlasResources.IMAGE_SELECTORSCREEN_STOREHIGHLIGHT);
			this.mStoreButton.Resize(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_StoreButton_X, Constants.GameSelector_StoreButton_Y, AtlasResources.IMAGE_SELECTORSCREEN_STORE.mWidth, AtlasResources.IMAGE_SELECTORSCREEN_STORE.mHeight);
			this.mAlmanacButton = GameButton.MakeNewButton(114, this, "", null, AtlasResources.IMAGE_SELECTORSCREEN_ALMANAC, AtlasResources.IMAGE_SELECTORSCREEN_ALMANACHIGHLIGHT, AtlasResources.IMAGE_SELECTORSCREEN_ALMANACHIGHLIGHT);
			this.mAlmanacButton.Resize(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_AlmanacButton_X, Constants.GameSelector_AlmanacButton_Y, AtlasResources.IMAGE_SELECTORSCREEN_ALMANAC.mWidth, AtlasResources.IMAGE_SELECTORSCREEN_ALMANAC.mHeight);
			this.mMoreGamesButton = GameButton.MakeNewButton(115, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_BUTTON, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_HIGHLIGHT);
			this.mMoreGamesButton.Resize(Constants.MAIN_MENU_ORIGIN_X + (int)Constants.InvertAndScale(10f), (int)Constants.InvertAndScale(170f), AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_BUTTON.mWidth, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_BUTTON.mHeight);
			this.mMoreGamesButton.mTranslateX = 0;
			this.mMoreGamesButton.mTranslateY = 0;
			this.mMoreGamesBackButton = GameButton.MakeNewButton(116, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_BACK_BUTTON, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_BACK_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_BACK_HIGHLIGHT);
			this.mMoreGamesBackButton.Resize(Constants.MAIN_MENU_ORIGIN_X + (int)Constants.InvertAndScale(10f), (int)Constants.InvertAndScale(170f), AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_BACK_BUTTON.mWidth, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREGAMES_BACK_BUTTON.mHeight);
			this.mMoreGamesBackButton.mVisible = false;
			this.mMoreGamesBackButton.mDisabled = true;
			this.mMoreGamesBackButton.mTranslateX = 0;
			this.mMoreGamesBackButton.mTranslateY = 0;
			this.mAchievementsButton = GameButton.MakeNewButton(117, this, "", null, AtlasResources.IMAGE_BLANK, AtlasResources.IMAGE_BLANK, AtlasResources.IMAGE_BLANK);
			this.mAchievementsButton.Resize(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_AchievementsStatue_X, Constants.GameSelector_AchievementsStatue_Y, Resources.IMAGE_ACHIEVEMENT_GNOME.mWidth, Resources.IMAGE_ACHIEVEMENT_GNOME.mHeight);
			this.mMiniGamesButton = GameButton.MakeNewButton(121, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MINIGAMES, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MINIGAMES_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MINIGAMES_HIGHLIGHT);
			this.mVaseBreakerButton = GameButton.MakeNewButton(122, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_VASEBREAKER, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_VASEBREAKER_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_VASEBREAKER_HIGHLIGHT);
			this.mIZombieButton = GameButton.MakeNewButton(123, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_IZOMBIE, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_IZOMBIE_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_IZOMBIE_HIGHLIGHT);
			this.mMoreWaysBackButton = GameButton.MakeNewButton(108, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS_BACK, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS_BACK_HIGHLIGHT, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS_BACK_HIGHLIGHT);
			this.mMiniGamesButton.Resize(Constants.QUICKPLAY_ORIGIN_X + Constants.GameSelector_MoreWaysToPlay_MiniGames_X, Constants.GameSelector_MoreWaysToPlay_MiniGames_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MINIGAMES.mWidth + 2, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MINIGAMES.mHeight + 2);
			this.mVaseBreakerButton.Resize(Constants.QUICKPLAY_ORIGIN_X + Constants.GameSelector_MoreWaysToPlay_VaseBreaker_X, Constants.GameSelector_MoreWaysToPlay_VaseBreaker_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_VASEBREAKER.mWidth + 2, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_VASEBREAKER.mHeight + 2);
			this.mIZombieButton.Resize(Constants.QUICKPLAY_ORIGIN_X + Constants.GameSelector_MoreWaysToPlay_IZombie_X, Constants.GameSelector_MoreWaysToPlay_IZombie_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_IZOMBIE.mWidth + 2, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_IZOMBIE.mHeight + 2);
			this.mMoreWaysBackButton.Resize(Constants.QUICKPLAY_ORIGIN_X + Constants.GameSelector_MoreWaysToPlay_Back_X, Constants.GameSelector_MoreWaysToPlay_Back_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS_BACK.mWidth + 2, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS_BACK.mHeight + 2);
			this.mMiniGamesButton.mTranslateX = -1;
			this.mMoreWaysBackButton.mTranslateX = (this.mMoreWaysBackButton.mTranslateY = 0);
			this.mSelectedQuickplayButtonId = 121;
			this.ToggleGameButton(this.mSelectedQuickplayButtonId);
			this.mMoreGamesListWidget = new MoreGamesListWidget(this.mApp);

			//this.mUserButton = /*GameButton.MakeButton(29, this, "[GAMESELECTOR_SWITCH_USER]");*/new NewLawnButton(null,29,this);
			//this.mUserButton.mDoFinger = true;
			//this.mUserButton.mLabel = "[GAMESELECTOR_SWITCH_USER]";
			//this.mUserButton.mFont = Resources.FONT_HOUSEOFTERROR16;
			////this.mUserButton.mButtonImage = AtlasResources.IMAGE_STORE_MAINMENUBUTTON;
			////this.mUserButton.mDownImage = AtlasResources.IMAGE_STORE_MAINMENUBUTTONDOWN;
			//this.mUserButton.mColors[0] = new SexyColor(255, 240, 0);
			//this.mUserButton.mColors[1] = new SexyColor(200, 200, 255);
			//mUserButton.Resize(Constants.MAIN_MENU_ORIGIN_X + (int)Constants.InvertAndScale(20f), (int)Constants.InvertAndScale(90f), (int)Constants.InvertAndScale(120f), (int)Constants.InvertAndScale(30f));
			float limboBtnOffset = 190;
			this.mChallengePageSurvivalButton = GameButton.MakeButton(126/*(int)GameSelectorButtons.GameSelector_ChallengePageSurvival*/, this, "[GAMESELECTOR_SURVIVAL]");
			mChallengePageSurvivalButton.Resize(Constants.QUICKPLAY_ORIGIN_X + (int)Constants.InvertAndScale(20f), (int)Constants.InvertAndScale(limboBtnOffset), (int)Constants.InvertAndScale(100f), (int)Constants.InvertAndScale(30f));
			this.mChallengePageLimboButton = GameButton.MakeButton(127/*(int)GameSelectorButtons.GameSelector_ChallengePageLimbo*/, this, "[GAMESELECTOR_LIMBO]");
			mChallengePageLimboButton.Resize(Constants.QUICKPLAY_ORIGIN_X + (int)Constants.InvertAndScale(20f), (int)Constants.InvertAndScale(limboBtnOffset + 30f), (int)Constants.InvertAndScale(100f), (int)Constants.InvertAndScale(30f));


			this.LoadGames();
			this.mMoreGamesListWidget.Resize(0, 0, this.mMoreGamesListWidget.mWidth, this.mMoreGamesListWidget.GetPreferredHeight() + (int)Constants.InvertAndScale(20f));
			this.mMoreGamesScrollWidget = new ScrollWidget();
			this.mMoreGamesScrollWidget.Resize(0, 0, this.mMoreGamesListWidget.mWidth, Constants.BOARD_HEIGHT);
			this.mMoreGamesScrollWidget.AddWidget(this.mMoreGamesListWidget);
			this.mMoreGamesScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mAchievementsWidget = new AchievementsWidget(this.mApp);
			this.mAchievementsScrollWidget = new ScrollWidget();
			this.mAchievementsScrollWidget.EnableBounce(false);
			this.mAchievementsScrollWidget.Resize(Constants.ACHIEVEMENTS_ORIGIN_X, Constants.BOARD_HEIGHT, this.mAchievementsWidget.mWidth, Constants.BOARD_HEIGHT);
			this.mAchievementsScrollWidget.AddWidget(this.mAchievementsWidget);
			this.mQuickplayWidget = new QuickPlayWidget(this.mApp, this);
			this.mQuickplayScrollWidget = new ScrollWidget();
			this.mQuickplayScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mQuickplayWidget.SizeToFit();
			this.mQuickplayScrollWidget.Resize(Constants.QUICKPLAY_ORIGIN_X + 30, 0, Constants.BOARD_WIDTH, this.mQuickplayWidget.mHeight);
			this.mQuickplayScrollWidget.AddWidget(this.mQuickplayWidget);
			this.mQuickplayScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mMiniGamesWidget = new MiniGamesWidget(this.mApp, this);
			this.mMiniGamesScrollWidget = new ScrollWidget();
			this.mMiniGamesScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mMiniGamesWidget.SizeToFit();
			this.mMiniGamesScrollWidget.Resize(Constants.QUICKPLAY_ORIGIN_X + 30, 10, Constants.BOARD_WIDTH, this.mMiniGamesWidget.mHeight);
			this.mMiniGamesScrollWidget.AddWidget(this.mMiniGamesWidget);
			this.mMiniGamesScrollWidget.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mRetractingQuickplay = false;
			this.mQuickplaySlideCounter = 0;
			this.mNeedToPlayRollIn = false;
			this.AddWidget(this.mAdventureButton);
			this.AddWidget(this.mMoreWaysToPlayButton);
			this.AddWidget(this.mOptionsButton);
			this.AddWidget(this.mStoreButton);
			this.AddWidget(this.mAlmanacButton);
			this.AddWidget(this.mZenGardenButton);
			this.AddWidget(this.mAchievementsButton);

			if (mApp.mPlayerInfo != null && mApp.mPlayerInfo.mHasUsedCheatKeys)
			{
				this.AddWidget(this.mChallengePageSurvivalButton);
				this.AddWidget(this.mChallengePageLimboButton);
			}

			if (this.mApp.mPlayerInfo != null && this.mApp.mPlayerInfo.mHasUnlockedPuzzleMode)
			{
				this.AddWidget(this.mVaseBreakerButton);
				this.AddWidget(this.mIZombieButton);
			}
			this.AddWidget(this.mMiniGamesButton);
			this.AddWidget(this.mMoreWaysBackButton);
			this.AddWidget(this.mAchievementsScrollWidget);
			this.AddWidget(this.mLeaderboardButton);
			this.AddWidget(this.mMiniGamesScrollWidget);
			this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_TITLE_CRAZY_DAVE_MAIN_THEME);
			this.mQuickplayLocked = false;
			this.mUnlockSelectorCheat = false;
			this.mTrophyParticleID = null;
			this.mShowStartButton = false;
			float theX = 0f;
			float theY = 0f;
			Reanimation theReanimation = this.mApp.AddReanimation(theX, theY, 400000, ReanimationType.REANIM_WOODSIGN, false);
			this.mWoodSignReanimID = this.mApp.ReanimationGetID(theReanimation);
			this.SetupUnlockFullGameReanim();
			//if (SexyAppBase.IsInTrialMode)
			//{
				this.AddWidget(this.mUserDialogButton);
			//}
			this.mLeafCounter = 50;
			this.mSignState = SelectorSignState.SIGN_UP;
			this.SyncProfile(false);
			this.LowerSign();
			this.mSlideCounter = 0;
			this.mStartX = (this.mStartY = (this.mDestX = (this.mDestY = 0)));
		}

		private void SetupUnlockFullGameReanim()
		{
			//if (SexyAppBase.IsInTrialMode)
			//{
				this.mWoodSignReanimID.AssignRenderGroupToPrefix("short rope1", -1);
				this.mWoodSignReanimID.AssignRenderGroupToPrefix("short rope2", -1);
				return;
			//}
			//this.mWoodSignReanimID.AssignRenderGroupToPrefix("long rope1", -1);
			//this.mWoodSignReanimID.AssignRenderGroupToPrefix("long rope2", -1);
			//this.mWoodSignReanimID.AssignRenderGroupToPrefix("click here", -1);
			//this.mWoodSignReanimID.AssignRenderGroupToPrefix("broken", -1);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public void SyncProfile(bool theShowLoading)
		{
			if (theShowLoading)
			{
				this.mLoading = true;
				this.mApp.PreloadForUser();
				this.mLoading = false;
			}
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mTrophyParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.ParticleSystemDie();
				this.mTrophyParticleID = null;
			}
			this.mLevel = 1;
			if (this.mApp.mPlayerInfo != null)
			{
				this.mLevel = this.mApp.mPlayerInfo.mLevel;
			}
			this.mShowStartButton = true;
			this.mQuickplayLocked = true;
			if (this.mApp.mPlayerInfo != null && !this.mApp.IsIceDemo())
			{
				if (this.mLevel >= 2)
				{
					this.mShowStartButton = false;
				}
				if (this.mApp.mPlayerInfo.mHasUnlockedMinigames)
				{
					this.mQuickplayLocked = false;
				}
			}
			if (this.mApp.HasFinishedAdventure() && !this.mApp.IsTrialStageLocked())
			{
				this.mHasTrophy = true;
			}
			else
			{
				this.mHasTrophy = false;
			}
			if (this.mHasTrophy)
			{
				this.AddTrophySparkle();
			}
			this.SyncButtons();
			AlmanacDialog.AlmanacInitForPlayer();
			Board.BoardInitForPlayer();
		}

		public override void Draw(Graphics g)
		{
			if (this.mApp.GetDialog(Dialogs.DIALOG_STORE) != null || this.mApp.GetDialog(Dialogs.DIALOG_ALMANAC) != null)
			{
				return;
			}
			this.DrawMoreGamesArea(g);
			this.DrawMainMenuArea(g);
			this.DrawQuickplayArea(g);
			base.DeferOverlay();
		}

		public override void DrawOverlay(Graphics g)
		{
			if (this.mApp.GetDialog(Dialogs.DIALOG_STORE) != null || this.mApp.GetDialog(Dialogs.DIALOG_ALMANAC) != null)
			{
				return;
			}
			g.SetLinearBlend(true);
			g.DrawImage(AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEAVES, (float)Constants.MAIN_MENU_ORIGIN_X, Constants.InvertAndScale(287f));
			if (this.mApp.mPlayerInfo == null)
			{
				return;
			}
			if (!this.mApp.IsIceDemo() && !this.mShowStartButton)
			{
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				int num5 = TodCommon.ClampInt((this.mLevel - 1) / 10 + 1, 1, 6);
				int num6 = this.mLevel - (num5 - 1) * 10;
				if (num5 == 1)
				{
					num2 += 1f;
				}
				if (num5 == 4)
				{
					num -= 1f;
				}
				if (num6 == 3)
				{
					num3 -= 1f;
				}
				if (this.mAdventureButton.mIsDown)
				{
					num += (float)Constants.GameSelector_LevelNumber_ButtonDown_Offset;
					num3 += (float)Constants.GameSelector_LevelNumber_ButtonDown_Offset;
					num2 += (float)Constants.GameSelector_LevelNumber_ButtonDown_Offset;
					num4 += (float)Constants.GameSelector_LevelNumber_ButtonDown_Offset;
				}
				g.SetColorizeImages(true);
				g.SetColor(this.mAdventureButton.mColors[5]);
				TodCommon.TodDrawImageCelF(g, AtlasResources.IMAGE_SELECTORSCREEN_LEVELNUMBERS, (float)(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_LevelNumber_1_Pos.X) + num, (float)Constants.GameSelector_LevelNumber_1_Pos.Y + num2, num5, 0);
				if (num6 < 10)
				{
					TodCommon.TodDrawImageCelF(g, AtlasResources.IMAGE_SELECTORSCREEN_LEVELNUMBERS, (float)(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_LevelNumber_2_Pos.X) + num3, (float)Constants.GameSelector_LevelNumber_2_Pos.Y + num4, num6, 0);
				}
				else if (num6 == 10)
				{
					TodCommon.TodDrawImageCelF(g, AtlasResources.IMAGE_SELECTORSCREEN_LEVELNUMBERS, (float)(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_LevelNumber_2_Pos.X) + num3, (float)Constants.GameSelector_LevelNumber_2_Pos.Y + num4, 1, 0);
					TodCommon.TodDrawImageCelF(g, AtlasResources.IMAGE_SELECTORSCREEN_LEVELNUMBERS, (float)(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_LevelNumber_3_Pos.X) + num3, (float)Constants.GameSelector_LevelNumber_3_Pos.Y + num4, 0, 0);
				}
				g.SetColor(new SexyColor(255, 255, 255));
				g.FillRect(Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_LevelNumber_Bar.X + (int)num, Constants.GameSelector_LevelNumber_Bar.Y + (int)num2, Constants.GameSelector_LevelNumber_Bar.Width, Constants.GameSelector_LevelNumber_Bar.Height);
				g.SetColorizeImages(false);
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mWoodSignReanimID);
			g.Translate(Constants.MAIN_MENU_ORIGIN_X, 0);
			reanimation.Draw(g);
			g.Translate(-Constants.MAIN_MENU_ORIGIN_X, 0);
			if (this.mApp.mPlayerInfo != null && !string.IsNullOrEmpty(this.mApp.mPlayerInfo.mName) && (this.mSignState == SelectorSignState.SIGN_DOWN || this.mSignState == SelectorSignState.SIGN_MOVING_DOWN))
			{
				string welcomeString = this.GetWelcomeString();
				int theTrackIndex = reanimation.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_welcome);
				SexyTransform2D sexyTransform2D = default(SexyTransform2D);
				reanimation.GetAttachmentOverlayMatrix(theTrackIndex, out sexyTransform2D);
				float num7 = (float)Resources.FONT_BRIANNETOD16.StringWidth(welcomeString);
				SexyTransform2D sexyTransform2D2 = default(SexyTransform2D);
				sexyTransform2D2.Translate((float)(Constants.GameSelector_PlayerName_Pos.X - (int)(num7 * 0.5f) + Constants.MAIN_MENU_ORIGIN_X + this.mX), (float)(Constants.GameSelector_PlayerName_Pos.Y + this.mY));
				sexyTransform2D2.Translate((float)(-(float)g.mTransX), (float)(-(float)g.mTransY));
				TodCommon.TodDrawStringMatrix(g, Resources.FONT_BRIANNETOD16, sexyTransform2D.mMatrix * sexyTransform2D2.mMatrix, welcomeString, new SexyColor(255, 245, 200));
			}
			if (this.mApp.mPlayerInfo != null)
			{
				bool flag_noUse = this.mApp.mPlayerInfo.mLastSeenMoreGames < this.mApp.mProfileMgr.mLastMoreGamesUpdate;
			}
			g.SetColor(new SexyColor(115, 101, 66));
			g.SetColorizeImages(true);
			int num8 = this.mMoreGamesListWidget.mY + this.mMoreGamesListWidget.mHeight;
			int num9 = -this.mMoreGamesListWidget.mY + this.mMoreGamesScrollWidget.mHeight - 52;
			if (this.mMoreGamesListWidget.mY < -48)
			{
				int num10 = Constants.MORE_GAMES_PLANK_HEIGHT + Constants.MORE_GAMES_ITEM_GAP;
				int num11 = (num9 + num10 / 2) / num10 * num10;
				int theTimeAge = Math.Abs(num11 - num9);
				int theAlpha = TodCommon.TodAnimateCurve(0, 35, theTimeAge, 0, 220, TodCurves.CURVE_LINEAR);
				g.SetColor(new SexyColor(115, 101, 66, theAlpha));
			}
			if (num8 > 340)
			{
				int num12 = Constants.MORE_GAMES_PLANK_HEIGHT + Constants.MORE_GAMES_ITEM_GAP;
				int num13 = (num9 + num12 / 2) / num12 * num12;
				int theTimeAge2 = Math.Abs(num13 - num9);
				int theAlpha = TodCommon.TodAnimateCurve(0, 35, theTimeAge2, 0, 220, TodCurves.CURVE_LINEAR);
				g.SetColor(new SexyColor(115, 101, 66, theAlpha));
			}
			g.SetColorizeImages(false);
			g.Translate(-(Constants.QUICKPLAY_ORIGIN_X + this.mX), 0);
		}

		private string GetWelcomeString()
		{
			if (GameSelector.cachedWelcomeString == null || GameSelector.cachedWelcomeStringName != this.mApp.mPlayerInfo.mName)
			{
				GameSelector.cachedWelcomeString = Common.StrFormat_(TodStringFile.TodStringTranslate("[WELCOME_USER_NAME]"), this.mApp.mPlayerInfo.mName);
				GameSelector.cachedWelcomeStringName = this.mApp.mPlayerInfo.mName;
			}
			return GameSelector.cachedWelcomeString;
		}

		public void DrawMainMenuArea(Graphics g)
		{
			g.SetLinearBlend(true);
			g.DrawImage(Resources.IMAGE_SELECTORSCREEN_MAIN_BACKGROUND, Constants.MAIN_MENU_ORIGIN_X, 0);
			int num = (this.mAchievementsButton.mIsDown && this.mAchievementsButton.mIsOver) ? 1 : 0;
			g.DrawImage(Resources.IMAGE_ACHIEVEMENT_GNOME, Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_AchievementsStatue_X + num, Constants.GameSelector_AchievementsStatue_Y + num);
			if (this.mAchievementsButton.mIsDown)
			{
				g.DrawImage(AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ACHIEVEMENTS_HIGHLIGHT, Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_AchievementsButton_X + num, Constants.GameSelector_AchievementsButton_Y + num);
			}
			else
			{
				g.DrawImage(AtlasResources.IMAGE_REANIM_SELECTORSCREEN_ACHIEVEMENTS_BUTTON, Constants.MAIN_MENU_ORIGIN_X + Constants.GameSelector_AchievementsButton_X + num, Constants.GameSelector_AchievementsButton_Y + num);
			}
			g.SetColor(SexyColor.White);
			g.SetFont(Resources.FONT_PICO129);
		}

		public void DrawQuickplayArea(Graphics g)
		{
			g.DrawImage(Resources.IMAGE_SELECTORSCREEN_QUICKPLAY_BACKGROUND, Constants.QUICKPLAY_ORIGIN_X, 0);
			g.DrawImage(AtlasResources.IMAGE_TROPHY, Constants.QUICKPLAY_ORIGIN_X + Constants.BOARD_WIDTH - 155, Constants.BOARD_HEIGHT - 50);
			int num = this.mApp.GetNumTrophies(ChallengePage.CHALLENGE_PAGE_CHALLENGE) + this.mApp.GetNumTrophies(ChallengePage.CHALLENGE_PAGE_PUZZLE);
			int num2 = 37;
			if (this.cachedTrophyStringCount != num)
			{
				this.cachedTrophyString = num.ToString() + "/" + num2.ToString();
				this.cachedTrophyStringCount = num;
			}
			g.SetFont(Resources.FONT_DWARVENTODCRAFT18);
			g.SetColor(new Color(224, 187, 98));
			g.DrawString(this.cachedTrophyString, Constants.QUICKPLAY_ORIGIN_X + Constants.BOARD_WIDTH - 90, Constants.BOARD_HEIGHT - 50);
		}

		public void DrawMoreGamesArea(Graphics g)
		{
		}

		public void RaiseSign()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mWoodSignReanimID);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lift, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 10f);
			this.mSignState = SelectorSignState.SIGN_MOVING_UP;
			this.mNeedToPlayRollIn = false;
		}

		public void LowerSign()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mWoodSignReanimID);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_drop, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 10f);
			this.mSignState = SelectorSignState.SIGN_MOVING_DOWN;
			this.mNeedToPlayRollIn = true;
			this.woodSignY = -30f;
		}

		public override bool BackButtonPress()
		{
			switch (this.state)
			{
			case GameSelector.GameSelectorScreenState.Main:
				this.mApp.WantsToExit = true;
				break;
			case GameSelector.GameSelectorScreenState.MoreGames:
				this.ButtonPress(this.mMoreGamesBackButton.mId);
				this.ButtonDepress(this.mMoreGamesBackButton.mId);
				break;
			case GameSelector.GameSelectorScreenState.Achievements:
				this.mAchievementsScrollWidget.ScrollToMin(true);
				this.wantToExitFromAchievementsViaBackButton = true;
				this.mAchievementsScrollWidget.mSpringOverride = 0.6f;
				break;
			case GameSelector.GameSelectorScreenState.QuickPlay:
				this.ButtonPress(this.mMoreWaysBackButton.mId);
				this.ButtonDepress(this.mMoreWaysBackButton.mId);
				break;
			}
			return true;
		}

		public override void Update()
		{
			base.Update();
			if (this.wantToExitFromAchievementsViaBackButton && this.mAchievementsScrollWidget.GetScrollOffset().y > -5f)
			{
				this.mAchievementsScrollWidget.SetScrollOffset(0f, 0f);
				this.mAchievementsScrollWidget.mSpringOverride = 0f;
				this.ButtonPress(118);
				this.ButtonDepress(118);
				this.wantToExitFromAchievementsViaBackButton = false;
			}
			if (this.mApp.GetDialog(Dialogs.DIALOG_STORE) != null || this.mApp.GetDialog(Dialogs.DIALOG_ALMANAC) != null)
			{
				return;
			}
			this.MarkDirty();
			if (this.mSlideCounter > 0)
			{
				int theNewX = TodCommon.TodAnimateCurve(75, 0, this.mSlideCounter, this.mStartX, this.mDestX, TodCurves.CURVE_EASE_IN_OUT);
				int theNewY = TodCommon.TodAnimateCurve(75, 0, this.mSlideCounter, this.mStartY, this.mDestY, TodCurves.CURVE_EASE_IN_OUT);
				this.Move(theNewX, theNewY);
				this.mSlideCounter -= 3;
				if (this.mSlideCounter >= 0 && this.mSlideCounter < 3)
				{
					if (this.mX == -Constants.MAIN_MENU_ORIGIN_X && this.mY == 0 && this.mSignState == SelectorSignState.SIGN_UP)
					{
						this.LowerSign();
					}
				}
				else if (this.mSignState == SelectorSignState.SIGN_DOWN && this.mStartX == -Constants.MAIN_MENU_ORIGIN_X && this.mDestX == 0)
				{
					this.RaiseSign();
				}
			}
			if (this.mQuickplaySlideCounter > 0)
			{
				int num = 150;
				int theNewY2;
				if (this.mRetractingQuickplay)
				{
					theNewY2 = TodCommon.TodAnimateCurve(30, 0, this.mQuickplaySlideCounter, 10, -this.mMiniGamesScrollWidget.mHeight - num, TodCurves.CURVE_EASE_IN_OUT);
				}
				else
				{
					theNewY2 = TodCommon.TodAnimateCurve(30, 0, this.mQuickplaySlideCounter, -this.mMiniGamesScrollWidget.mHeight - num, 10, TodCurves.CURVE_EASE_IN_OUT);
				}
				this.mQuickplayScrollWidget.Move(this.mQuickplayScrollWidget.mX, theNewY2);
				this.mMiniGamesScrollWidget.Move(this.mMiniGamesScrollWidget.mX, theNewY2);
				this.mQuickplaySlideCounter -= 3;
				if (this.mQuickplaySlideCounter >= 0 && this.mQuickplaySlideCounter < 3 && this.mRetractingQuickplay && (this.mSlideCounter == 0 || this.mDestX == -Constants.QUICKPLAY_ORIGIN_X))
				{
					this.PopulateQuickPlayWidget();
					this.SlideOutQuickPlayWidget();
				}
			}
			if (this.mApp.mZenGarden != null)
			{
				this.mApp.mZenGarden.UpdatePlantNeeds();
			}
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mTrophyParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.Update();
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mWoodSignReanimID);
			if (this.mSignState == SelectorSignState.SIGN_DOWN)
			{
				if (reanimation.mLoopCount > 0)
				{
					if (this.mApp.mPlayerInfo == null)
					{
						this.mInUserDialog = true;
						this.mApp.DoCreateUserDialog(true);
						this.RaiseSign();
					}
					if (this.mHasTrophy)
					{
						this.AddTrophySparkle();
					}
					if (this.mApp.mPlayerInfo != null && this.mApp.mPlayerInfo.mNeedsMessageOnGameSelector)
					{
						this.mApp.mPlayerInfo.mNeedsMessageOnGameSelector = false;
						this.mApp.WriteCurrentUserConfig();
						this.mApp.LawnMessageBox(49, "[ADVENTURE_COMPLETE_HEADER]", "[ADVENTURE_COMPLETE_BODY]", "[DIALOG_BUTTON_OK]", "", 3, null);
					}
				}
			}
			else if (this.mSignState == SelectorSignState.SIGN_MOVING_UP)
			{
				if (reanimation.mLoopCount > 0)
				{
					this.mSignState = SelectorSignState.SIGN_UP;
				}
			}
			else if (this.mSignState == SelectorSignState.SIGN_MOVING_DOWN && reanimation.mLoopCount > 0)
			{
				this.mSignState = SelectorSignState.SIGN_DOWN;
			}
			if (this.mInUserDialog && this.mApp.GetDialog(30) == null)
			{
				this.mInUserDialog = false;
				this.LowerSign();
			}
			if (this.mNeedToPlayRollIn)
			{
				this.mApp.PlaySample(Resources.SOUND_ROLL_IN);
				this.mNeedToPlayRollIn = false;
			}
			if (this.woodSignY != 0f)
			{
				this.woodSignY += 1f;
				if (this.woodSignY > 0f)
				{
					this.woodSignY = 0f;
				}
				reanimation.SetPosition(reanimation.mOverlayMatrix.mMatrix.Translation.X, this.woodSignY);
			}
			reanimation.Update();
			if (this.mFadeInCounter > 0)
			{
				this.mFadeInCounter -= 3;
			}
		}

		public virtual void ButtonMouseEnter(int theId)
		{
			if (this.mQuickplayLocked && theId == 101)
			{
				return;
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_BLEEP);
		}

		public virtual void ButtonPress(int theId, int theClickCount)
		{
			if (this.mSlideCounter != 0)
			{
				return;
			}
			if (theId == 100 || theId == 101)
			{
				this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
				return;
			}
			this.mApp.PlaySample(Resources.SOUND_TAP);
		}

		public virtual void ButtonDepress(int theId)
		{
			if (this.mApp.mPlayerInfo == null)
			{
				return;
			}
			if (this.mSlideCounter != 0)
			{
				return;
			}
			if (theId == 101 && this.mQuickplayLocked)
			{
				this.mApp.LawnMessageBox(49, "[MODE_LOCKED]", "[QUICKPLAY_LOCKED_MESSAGE]", "[DIALOG_BUTTON_OK]", "", 3, null);
				return;
			}
			this.state = GameSelector.GameSelectorScreenState.Main;
			switch (theId)
			{
			case 100:
				this.mApp.KillGameSelector();
				this.ClickedAdventure();
				return;
			case 101:
				this.mQuickplayWidget.Clear();
				this.mQuickplayWidget.SizeToFit();
				this.SlideTo(-Constants.QUICKPLAY_ORIGIN_X - 30, 0);
				this.SlideOutQuickPlayWidget();
				this.state = GameSelector.GameSelectorScreenState.QuickPlay;
				return;
			case 102:
			case 103:
			case 104:
			case 105:
			case 106:
			case 107:
				if (theId != this.mSelectedQuickplayButtonId)
				{
					this.ToggleGameButton(this.mSelectedQuickplayButtonId);
					this.ToggleGameButton(theId);
					this.mSelectedQuickplayButtonId = theId;
					this.RetractQuickPlayWidget();
					return;
				}
				break;
			case 108:
				this.RetractQuickPlayWidget();
				this.SlideTo(-Constants.MAIN_MENU_ORIGIN_X, 0);
				this.state = GameSelector.GameSelectorScreenState.Main;
				return;
			case 109:
				this.mApp.DoNewOptions(true);
				return;
			case 110:
				this.mApp.KillGameSelector();
				this.mApp.ShowAwardScreen(AwardType.AWARD_HELP_ZOMBIE_NOTE, false);
				return;
			case 111:
				this.mApp.ConfirmQuit();
				return;
			case 112:
				this.mDoNewGameAfterStore = false;
				this.mApp.ShowStoreScreen(this);
				return;
			case 113:
				this.mApp.BuyGame();
				return;
			case 114:
				this.mApp.DoAlmanacDialog(SeedType.SEED_NONE, ZombieType.ZOMBIE_INVALID, this);
				return;
			case 115:
				this.mMoreGamesButton.mVisible = false;
				this.mMoreGamesButton.mDisabled = true;
				this.mMoreGamesBackButton.mVisible = true;
				this.mMoreGamesBackButton.mDisabled = false;
				this.mMoreGamesScrollWidget.ScrollToMin(false);
				if (this.mApp.mPlayerInfo != null)
				{
					this.mApp.mPlayerInfo.mLastSeenMoreGames = DateTime.UtcNow;
					this.mApp.WriteCurrentUserConfig();
				}
				this.SlideTo(0, 0);
				this.state = GameSelector.GameSelectorScreenState.MoreGames;
				return;
			case 116:
				this.mMoreGamesBackButton.mVisible = false;
				this.mMoreGamesBackButton.mDisabled = true;
				this.mMoreGamesButton.mVisible = true;
				this.mMoreGamesButton.mDisabled = false;
				this.SlideTo(-Constants.MAIN_MENU_ORIGIN_X, 0);
				this.state = GameSelector.GameSelectorScreenState.Main;
				return;
			case 117:
				this.mAchievementsScrollWidget.ScrollToMin(false);
				this.SlideTo(-Constants.ACHIEVEMENTS_ORIGIN_X, -Constants.BOARD_HEIGHT);
				this.mAchievementsScrollWidget.SetDisabled(false);
				this.state = GameSelector.GameSelectorScreenState.Achievements;
				return;
			case 118:
				if (this.mAchievementsScrollWidget.GetScrollOffset().y != 0f)
				{
					this.mAchievementsScrollWidget.ScrollToMin(true);
					this.wantToExitFromAchievementsViaBackButton = true;
					return;
				}
				this.SlideTo(-Constants.MAIN_MENU_ORIGIN_X, 0);
				this.state = GameSelector.GameSelectorScreenState.Main;
				this.mAchievementsScrollWidget.SetDisabled(true);
				return;
			case 119:
				break;
			case 120:
				this.mApp.KillGameSelector();
				this.mApp.ShowLeaderboardScreen();
				return;
			case 121:
			case 122:
			case 123:
				if (theId != this.mSelectedQuickplayButtonId)
				{
					this.ToggleGameButton(this.mSelectedQuickplayButtonId);
					this.ToggleGameButton(theId);
					this.mSelectedQuickplayButtonId = theId;
					this.RetractQuickPlayWidget();
				}
				this.state = GameSelector.GameSelectorScreenState.MoreGames;
				break;
			case 124:
				this.mApp.KillGameSelector();
				this.mApp.PreNewGame(GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN, false);
				return;
			default:
				return;
			}
		}

		public override void KeyDown(KeyCode theKey)
		{
			if (this.mApp.mKonamiCheck.Check(theKey))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				return;
			}
			if (this.mApp.mMustacheCheck.Check(theKey) || this.mApp.mMoustacheCheck.Check(theKey))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_POLEVAULT);
				this.mApp.mMustacheMode = !this.mApp.mMustacheMode;
				return;
			}
			if (this.mApp.mSuperMowerCheck.Check(theKey) || this.mApp.mSuperMowerCheck2.Check(theKey))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_ZAMBONI);
				this.mApp.mSuperMowerMode = !this.mApp.mSuperMowerMode;
				return;
			}
			if (this.mApp.mFutureCheck.Check(theKey))
			{
				this.mApp.PlaySample(Resources.SOUND_BOING);
				this.mApp.mFutureMode = !this.mApp.mFutureMode;
				return;
			}
			if (this.mApp.mPinataCheck.Check(theKey))
			{
				if (this.mApp.CanDoPinataMode())
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_JUICY);
					this.mApp.mPinataMode = !this.mApp.mPinataMode;
					return;
				}
				this.mApp.PlaySample(Resources.SOUND_BUZZER);
				return;
			}
			else
			{
				if (!this.mApp.mDanceCheck.Check(theKey))
				{
					if (this.mApp.mDaisyCheck.Check(theKey))
					{
						if (this.mApp.CanDoDaisyMode())
						{
							this.mApp.PlaySample(Resources.SOUND_LOADINGBAR_FLOWER);
							this.mApp.mDaisyMode = !this.mApp.mDaisyMode;
							return;
						}
						this.mApp.PlaySample(Resources.SOUND_BUZZER);
					}
					return;
				}
				if (this.mApp.CanDoDanceMode())
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_DANCER);
					this.mApp.mDanceMode = !this.mApp.mDanceMode;
					return;
				}
				this.mApp.PlaySample(Resources.SOUND_BUZZER);
				return;
			}
		}

		public override void KeyChar(SexyChar theChar)
		{
			if (theChar.value_type == 'u' && this.mApp.mPlayerInfo != null)
			{
				this.mApp.mPlayerInfo.mFinishedAdventure = 2;
				this.mApp.mPlayerInfo.AddCoins(50000);
				this.mApp.mPlayerInfo.mHasUsedCheatKeys = true;
				this.mApp.mPlayerInfo.mHasUnlockedMinigames = true;
				this.mApp.mPlayerInfo.mHasUnlockedPuzzleMode = true;
				this.mApp.mPlayerInfo.mHasUnlockedSurvivalMode = true;
				for (int i = 0; i < 200; i++)
				{
					GameMode gameMode = i + GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1;
					if (gameMode != GameMode.GAMEMODE_SCARY_POTTER_ENDLESS && gameMode != GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS && gameMode != GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_3)
					{
						this.mApp.mPlayerInfo.mChallengeRecords[i] = 20;
					}
				}
				for (int j = 0; j < 80; j++)
				{
					this.mApp.mPlayerInfo.mPurchases[j] = 1;
				}
				this.mApp.mPlayerInfo.mPurchases[7] = 0;
				this.SyncProfile(true);
				string savedGameName = LawnCommon.GetSavedGameName(GameMode.GAMEMODE_ADVENTURE, (int)this.mApp.mPlayerInfo.mId);
				this.mApp.EraseFile(savedGameName);
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (y > Constants.BOARD_HEIGHT)
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
				this.ButtonDepress(118);
			}
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			this.mX = -Constants.MAIN_MENU_ORIGIN_X;
		}

		public void SyncButtons()
		{
			bool flag = this.mApp.CanShowAlmanac() || this.mUnlockSelectorCheat;
			bool flag2 = this.mApp.CanShowStore() || this.mUnlockSelectorCheat;
			bool flag3 = this.mApp.CanShowZenGarden() || this.mUnlockSelectorCheat;
			this.mAlmanacButton.mDisabled = !flag;
			this.mAlmanacButton.mVisible = flag;
			this.mStoreButton.mDisabled = !flag2;
			this.mStoreButton.mVisible = flag2;
			this.mZenGardenButton.mDisabled = !flag3;
			this.mZenGardenButton.mVisible = flag3;
			this.SetupUnlockFullGameReanim();
			if (this.mQuickplayLocked)
			{
				this.mMoreWaysToPlayButton.mOverImage = AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS;
				this.mMoreWaysToPlayButton.mDownImage = AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS;
				this.mMoreWaysToPlayButton.SetColor(5, new SexyColor(128, 128, 128));
				return;
			}
			this.mMoreWaysToPlayButton.mOverImage = AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS_HIGHLIGHT;
			this.mMoreWaysToPlayButton.mDownImage = AtlasResources.IMAGE_REANIM_SELECTORSCREEN_MOREWAYS_HIGHLIGHT;
			this.mMoreWaysToPlayButton.SetColor(5, SexyColor.White);
		}

		public void AddTrophySparkle()
		{
		}

		public void ClickedAdventure()
		{
			this.mApp.mMusic.StopAllMusic();
			this.mAdventureButton.SetDisabled(true);
			this.mMoreWaysToPlayButton.SetDisabled(true);
			this.mZenGardenButton.SetDisabled(true);
			this.mOptionsButton.SetDisabled(true);
			this.mUserDialogButton.SetDisabled(true);
			this.mStoreButton.SetDisabled(true);
			this.mAlmanacButton.SetDisabled(true);
			this.mMoreGamesButton.SetDisabled(true);
			this.mAchievementsButton.SetDisabled(true);
			this.mApp.KillGameSelector();
			if (this.mApp.IsIceDemo())
			{
				this.mApp.PreNewGame(GameMode.GAMEMODE_CHALLENGE_ICE, false);
				return;
			}
			if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 1 && !this.mApp.SaveFileExists())
			{
				this.mApp.PreNewGame(GameMode.GAMEMODE_INTRO, false);
				return;
			}
			if (this.mApp.mPlayerInfo.mNeedsMagicTacoReward && this.mLevel == 35)
			{
				StoreScreen storeScreen = this.mApp.ShowStoreScreen(this);
				storeScreen.SetupForIntro(601);
				this.mDoNewGameAfterStore = true;
				return;
			}
			if (this.ShouldDoZenTuturialBeforeAdventure())
			{
				this.mApp.PreNewGame(GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN, false);
				this.mApp.mZenGarden.SetupForZenTutorial();
				return;
			}
			this.mApp.PreNewGame(GameMode.GAMEMODE_ADVENTURE, true);
		}

		public bool ShouldDoZenTuturialBeforeAdventure()
		{
			return !this.mApp.HasFinishedAdventure() && this.mApp.mPlayerInfo.mLevel == 45 && this.mApp.mPlayerInfo.mNumPottedPlants == 0;
		}

		public void BackFromStore()
		{
			StoreScreen storeScreen = (StoreScreen)this.mApp.GetDialog(Dialogs.DIALOG_STORE);
			this.mApp.KillDialog(4);
			if (this.mDoNewGameAfterStore)
			{
				this.mApp.PreNewGame(GameMode.GAMEMODE_ADVENTURE, false);
				return;
			}
			this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_TITLE_CRAZY_DAVE_MAIN_THEME);
		}

		public void QuickPlayStageSelected(int theLevel)
		{
			this.mApp.PlaySample(Resources.SOUND_TAP);
			this.mApp.KillGameSelector();
			GameMode theGameMode = theLevel - 1 + GameMode.GAMEMODE_QUICKPLAY_1;
			this.mApp.PreNewGame(theGameMode, true);
		}

		public void MiniGamesStageSelected(int theLevel)
		{
			this.mApp.PlaySample(Resources.SOUND_TAP);
			GameMode theGameMode = theLevel + GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1;
			this.mApp.KillGameSelector();
			this.mApp.PreNewGame(theGameMode, true);
		}

		public void BackFromAlmanac()
		{
			this.mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.MUSIC_TUNE_TITLE_CRAZY_DAVE_MAIN_THEME);
		}

		public void LoadGames()
		{
		}

		public void SlideTo(int theX, int theY)
		{
			this.mSlideCounter = 75;
			this.mDestX = theX;
			this.mDestY = theY;
			this.mStartX = this.mX;
			this.mStartY = this.mY;
		}

		public void MoveToQuickplay(bool theDoFadeIn, GameSelectorButtons theButton)
		{
			this.state = GameSelector.GameSelectorScreenState.QuickPlay;
			if (theDoFadeIn)
			{
				this.mFadeInCounter = 180;
			}
			this.mQuickplayWidget.Clear();
			this.mQuickplayWidget.SizeToFit();
			this.Move(-Constants.QUICKPLAY_ORIGIN_X - 30, 0);
			this.RetractQuickPlayWidget();
			this.mQuickplaySlideCounter = 0;
			if (theButton != (GameSelectorButtons)this.mSelectedQuickplayButtonId)
			{
				this.ToggleGameButton(this.mSelectedQuickplayButtonId);
				this.ToggleGameButton((int)theButton);
				this.mSelectedQuickplayButtonId = (int)theButton;
				this.PopulateQuickPlayWidget();
				this.mMiniGamesScrollWidget.ScrollToMin(false);
				this.mMiniGamesScrollWidget.Move(this.mMiniGamesScrollWidget.mX, 10);
			}
		}

		public void SlideOutQuickPlayWidget()
		{
			this.mQuickplayScrollWidget.ScrollToMin(false);
			this.mMiniGamesScrollWidget.ScrollToMin(false);
			this.mRetractingQuickplay = false;
			this.mQuickplaySlideCounter = 30;
		}

		public void RetractQuickPlayWidget()
		{
			this.mQuickplayScrollWidget.ScrollToMin(false);
			this.mRetractingQuickplay = true;
			this.mQuickplaySlideCounter = 30;
		}

		public void PopulateQuickPlayWidget()
		{
			MiniGameMode mode = MiniGameMode.MINI_GAME_MODE_GAMES;
			switch (this.mSelectedQuickplayButtonId)
			{
			case 121:
				mode = MiniGameMode.MINI_GAME_MODE_GAMES;
				break;
			case 122:
				mode = MiniGameMode.MINI_GAME_MODE_VASEBREAKER;
				break;
			case 123:
				mode = MiniGameMode.MINI_GAME_MODE_I_ZOMBIE;
				break;
			}
			this.mMiniGamesWidget.SwitchMode(mode);
		}

		public void ToggleGameButton(int theId)
		{
			DialogButton dialogButton;
			switch (theId)
			{
			case 121:
				dialogButton = this.mMiniGamesButton;
				break;
			case 122:
				dialogButton = this.mVaseBreakerButton;
				break;
			case 123:
				dialogButton = this.mIZombieButton;
				break;
			default:
				return;
			}
			Image mButtonImage = dialogButton.mButtonImage;
			dialogButton.mButtonImage = dialogButton.mDownImage;
			dialogButton.mDownImage = (dialogButton.mOverImage = mButtonImage);
		}

		public void ButtonMouseMove(int id, int x, int y)
		{
		}

		public void ButtonMouseLeave(int id)
		{
		}

		public void ButtonMouseTick(int id)
		{
		}

		public void ButtonDownTick(int id)
		{
		}

		public void ButtonPress(int id)
		{
		}

		private GameSelector.GameSelectorScreenState state;

		public LawnApp mApp;

		public NewLawnButton mAdventureButton;

		public NewLawnButton mMoreWaysToPlayButton;

		public NewLawnButton mZenGardenButton;

		public NewLawnButton mOptionsButton;

		public DialogButton mStoreButton;

		public DialogButton mAlmanacButton;

		public DialogButton mUserDialogButton;

		public DialogButton mAchievementsButton;

		public DialogButton mMoreGamesButton;

		public DialogButton mMoreGamesBackButton;

		public DialogButton mLeaderboardButton;

		public DialogButton mMiniGamesButton;

		public DialogButton mVaseBreakerButton;

		public DialogButton mIZombieButton;

		public DialogButton mMoreWaysBackButton;

		public DialogButton mChallengePageSurvivalButton;
		public DialogButton mChallengePageLimboButton;

		public int mSelectedQuickplayButtonId;

		public MoreGamesListWidget mMoreGamesListWidget;
        
        public ScrollWidget mMoreGamesScrollWidget;

		public string mLexText = string.Empty;

		public AchievementsWidget mAchievementsWidget;

		public ScrollWidget mAchievementsScrollWidget;

		public QuickPlayWidget mQuickplayWidget;

		public ScrollWidget mQuickplayScrollWidget;

		public MiniGamesWidget mMiniGamesWidget;

		public ScrollWidget mMiniGamesScrollWidget;

		public bool mQuickplayLocked;

		public bool mShowStartButton;

		public TodParticleSystem mTrophyParticleID;

		public Reanimation mWoodSignReanimID;

		public Reanimation[] mCloudReanimID = new Reanimation[6];

		public int[] mCloudCounter = new int[6];

		public Reanimation[] mFlowerReanimID = new Reanimation[3];

		public int mLeafCounter;

		public SelectorSignState mSignState;

		public bool mInUserDialog;

		public int mLevel;

		public bool mLoading;

		public bool mHasTrophy;

		public bool mUnlockSelectorCheat;

		public int mSlideCounter;

		public int mStartX;

		public int mStartY;

		public int mDestX;

		public int mDestY;

		public bool mNeedToPlayRollIn;

		public int mFadeInCounter;

		private static string cachedWelcomeString;

		private static string cachedWelcomeStringName;

		private string cachedTrophyString = string.Empty;

		private int cachedTrophyStringCount = -1;

		private bool wantToExitFromAchievementsViaBackButton;

		private float woodSignY;

		public bool mDoNewGameAfterStore;

		public int mQuickplaySlideCounter;

		public bool mRetractingQuickplay;
        

		private enum GameSelectorScreenState
		{
			Main,
			MoreGames,
			Achievements,
			QuickPlay
		}
	}
}
