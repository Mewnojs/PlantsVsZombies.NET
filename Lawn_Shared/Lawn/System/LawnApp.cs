using System;
using System.Collections.Generic;
using System.Threading;
//using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class LawnApp : SexyAppBase
	{
		public PlayerInfo mPlayerInfo
		{
			get
			{
				return this._playerInfo;
			}
			set
			{
				this._playerInfo = value;
				if (this.mPlayerInfo != null)
				{
					this.SetMusicVolume(this.mPlayerInfo.mMusicVolume);
					this.SetSfxVolume(this.mPlayerInfo.mSoundVolume);
				}
			}
		}

		public static void CenterDialog(Dialog theDialog, int theWidth, int theHeight)
		{
			theDialog.Resize((Constants.BOARD_WIDTH - theWidth) / 2, (Constants.BOARD_HEIGHT - theHeight) / 2, theWidth, theHeight);
		}

		public LawnApp(Main m) : base(m)
		{
			this.mBoard = null;
			this.mGameSelector = null;
			this.mSeedChooserScreen = null;
			this.mAwardScreen = null;
			this.mCreditScreen = null;
			this.mTitleScreen = null;
			this.mSoundSystem = null;
			this.mKonamiCheck = null;
			this.mMustacheCheck = null;
			this.mMoustacheCheck = null;
			this.mSuperMowerCheck = null;
			this.mSuperMowerCheck2 = null;
			this.mFutureCheck = null;
			this.mPinataCheck = null;
			this.mDanceCheck = null;
			this.mDaisyCheck = null;
			this.mSukhbirCheck = null;
			this.mMustacheMode = false;
			this.mSuperMowerMode = false;
			this.mFutureMode = false;
			this.mPinataMode = false;
			this.mDanceMode = false;
			this.mDaisyMode = false;
			this.mSukhbirMode = false;
			this.mGameScene = GameScenes.SCENE_LOADING;
			this.mZenGarden = null;
			this.mEffectSystem = null;
			this.mReanimatorCache = null;
			this.mCloseRequest = false;
			this.mWidth = Constants.BOARD_WIDTH;
			this.mHeight = Constants.BOARD_HEIGHT;
			this.mAppCounter = 0;
			this.mAppRandSeed = DateTime.UtcNow.Millisecond;
			this.mTrialType = TrialType.TRIAL_NONE;
			this.mDebugTrialLocked = false;
			this.mMuteSoundsForCutscene = false;
			base.mMusicVolume = 0.85;
			this.mSfxVolume = 0.85;
			this.mAutoStartLoadingThread = false;
			this.mProdName = "PlantsVsZombies";
			string mTitle = "Plants vs. Zombies";
			this.mTitle = mTitle;
			this.mPlayerInfo = null;
			this.mLastLevelStats = new LevelStats();
			this.mFirstTimeGameSelector = true;
			this.mGameMode = GameMode.GAMEMODE_ADVENTURE;
			this.mEasyPlantingCheat = false;
			this.mLoadingZombiesThreadCompleted = true;
			this.mGamesPlayed = 0;
			this.mMaxExecutions = 0;
			this.mMaxPlays = 0;
			this.mMaxTime = 0;
			this.mCompletedLoadingThreadTasks = 0;
			this.mProfileMgr = new ProfileMgr();
			this.mRegisterResourcesLoaded = false;
			this.mTodCheatKeys = false;
			this.mCrazyDaveReanimID = null;
			this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_OFF;
			this.mCrazyDaveBlinkCounter = 0;
			this.mCrazyDaveBlinkReanimID = null;
			this.mCrazyDaveMessageIndex = -1;
			this.mLawnMessageBoxListener = null;
			ReportAchievement.AchievementsChanged += this.ReportAchievement_AchievementsChanged;
		}

		private void ReportAchievement_AchievementsChanged()
		{
			if (this.mPlayerInfo != null)
			{
				this.mPlayerInfo.UpdateAchievementInfo();
			}
		}

		public override void Dispose()
		{
			if (this.mBoard != null)
			{
				this.WriteCurrentUserConfig();
			}
			if (this.mBoard != null)
			{
				this.mBoardResult = BoardResult.BOARDRESULT_QUIT_APP;
				this.mBoard.TryToSaveGame();
				this.mWidgetManager.RemoveWidget(this.mBoard);
				this.mBoard.Dispose();
				this.mBoard = null;
			}
			if (this.mTitleScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mTitleScreen);
				this.mTitleScreen.Dispose();
			}
			if (this.mGameSelector != null)
			{
				this.mWidgetManager.RemoveWidget(this.mGameSelector);
				this.mGameSelector.Dispose();
			}
			if (this.mSeedChooserScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mSeedChooserScreen);
				this.mSeedChooserScreen.Dispose();
			}
			if (this.mAwardScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mAwardScreen);
				this.mAwardScreen.Dispose();
			}
			if (this.mCreditScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mCreditScreen);
				this.mCreditScreen.Dispose();
			}
			this.mProfileMgr.Dispose();
			this.mResourceManager.DeleteResources("");
		}

		public bool KillNewOptionsDialog()
		{
			if ((NewOptionsDialog)base.GetDialog(Dialogs.DIALOG_NEWOPTIONS) == null)
			{
				return false;
			}
			this.KillDialog(2);
			return true;
		}

		public bool KillLeaderboardDialog()
		{
			if ((LeaderboardDialog)base.GetDialog(Dialogs.DIALOG_LEADERBOARD) == null)
			{
				return false;
			}
			this.KillDialog(59);
			this.mLeaderboardScreen.SetGrayed(false);
			return true;
		}

		public override void GotFocus()
		{
			if (this.mSoundManager != null)
			{
				this.mSoundManager.Enable(true);
			}
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.Enable(this.mMusicEnabled);
			}
			if (this.mCreditScreen != null)
			{
				this.mCreditScreen.AppGotFocus();
			}
			if (this.checkGiveAchievements && !SexyAppBase.IsInTrialMode)
			{
				this.checkGiveAchievements = false;
				ReportAchievement.GiveAchievement(this.achievementToCheck);
			}
			base.GotFocus();
		}

		public override void LostFocus()
		{
			base.LostFocus();
			if (this.mSoundManager != null)
			{
				this.mSoundManager.StopAllSounds();
			}
			if (this.mBoard != null && this.mBoard.mBoardFadeOutCounter > 0)
			{
				this.mBoard.mBoardFadeOutCounter = 3;
			}
			if (!this.mTodCheatKeys && this.CanPauseNow())
			{
				if (this.mBoard != null)
				{
					this.mBoard.RefreshSeedPacketFromCursor();
				}
				this.DoPauseDialog();
			}
		}

		public override void AppEnteredBackground()
		{
			this.WriteRestoreInfo();
			if (this.mBoard != null)
			{
				this.mBoard.TryToSaveGame();
			}
			this.WriteCurrentUserConfig();
		}

		public override void InitHook()
		{
		}

		public override void WriteToRegistry()
		{
			if (this.mPlayerInfo != null)
			{
				base.RegistryWriteString("CurUser", this.mPlayerInfo.mName);
				this.mPlayerInfo.SaveDetails();
			}
			base.WriteToRegistry();
		}

		public override void LoadingThreadProc()
		{
			GameConstants.Init();
			if (!TodCommon.TodLoadResources("LoaderBar") || !TodCommon.TodLoadResources("LoaderBarFont"))
			{
				return;
			}
			Resources.ExtractLoaderBarFontResources(this.mResourceManager);
			Resources.ExtractLoaderBarResources(this.mResourceManager);
			AtlasResources.mAtlasResources.UnpackLoadingAtlasImages();
			Resources.LinkUpResArray();
			ReanimationParams[] array = new ReanimationParams[]
			{
				new ReanimationParams(ReanimationType.REANIM_LOADBAR_SPROUT, "reanim/loadbar_sprout", 1),
				new ReanimationParams(ReanimationType.REANIM_LOADBAR_ZOMBIEHEAD, "reanim/loadbar_zombiehead", 1)
			};
			ReanimatorXnaHelpers.ReanimatorLoadDefinitions(ref array, array.Length);
			TodStringFile.TodStringListLoad("Content/"+"LawnStrings_" + Constants.LanguageSubDir + ".txt");
			this.mTitleScreen.mLoaderScreenIsLoaded = true;
			this.mNumLoadingThreadTasks += this.mResourceManager.GetNumResources("LoadingFonts") * 54;
			this.mNumLoadingThreadTasks += this.mResourceManager.GetNumResources("LoadingImages") * 9;
			this.mNumLoadingThreadTasks += this.mResourceManager.GetNumResources("LoadingSounds") * 54;
			this.mNumLoadingThreadTasks += 612;
			this.mNumLoadingThreadTasks += 8092;
			this.mNumLoadingThreadTasks += 360500;
			this.mNumLoadingThreadTasks += this.GetNumPreloadingTasks();
			this.mNumLoadingThreadTasks += this.mMusic.GetNumLoadingTasks();
			if (!Main.LOW_MEMORY_DEVICE)
			{
				this.DelayLoadGamePlayResources(true);
				this.DelayLoadLeaderboardResource(true);
				this.DelayLoadCachedResources(true);
				this.DelayLoadZenGardenResources(true);
			}
			this.DelayLoadMainMenuResource(true);
			this.mResourceManager.LoadAllResources();
			Resources.ExtractResources(this.mResourceManager, AtlasResources.mAtlasResources);
			AtlasResources.mAtlasResources.ExtractResources();
			ReanimatorXnaHelpers.ReanimatorLoadDefinitions(ref GameConstants.gLawnReanimationArray, 119);
			TodStringFile.TodStringListSetColors(GameConstants.gLawnStringFormats, GameConstants.gLawnStringFormatCount);
			if (this.mLoadingFailed || this.mShutdown || this.mCloseRequest)
			{
				return;
			}
			this.mMusic.MusicInit();
			this.mZenGarden = new ZenGarden();
			this.mReanimatorCache = new ReanimatorCache();
			this.mReanimatorCache.ReanimatorCacheInitialize();
			TodFoley.TodFoleyInitialize(null, 103);
			GlobalMembersTrail.TrailLoadDefinitions(GameConstants.gLawnTrailArray, 1);
			TodParticleGlobal.TodParticleLoadDefinitions(ref GameConstants.gLawnParticleArray, 102);
			this.PreloadForUser();
			if (!this.mLoadingFailed && !this.mShutdown)
			{
				bool flag = this.mCloseRequest;
			}
			// init filter effects
			for (int i = 0; i < (int)FilterEffectType.NUM_FILTER_EFFECTS; i++)
			{
				FilterEffect.FilterEffectInitTexture(AtlasResources.IMAGE_REANIM_IMITATER_BLINK1.Texture, (FilterEffectType)i);
			}
		}

		public virtual void LoadingCompleted()
		{
			this.mWidgetManager.RemoveWidget(this.mTitleScreen);
			base.SafeDeleteWidget(this.mTitleScreen);
			this.mTitleScreen = null;
			this.mResourceManager.DeleteImage("IMAGE_TITLESCREEN");
			if (this.mRestoreLocation == RestoreLocation.RESTORE_BOARD && this.RestoreGame())
			{
				return;
			}
			this.ShowGameSelector();
			if (Main.LOW_MEMORY_DEVICE)
			{
				this.mResourceManager.UnloadInitResources();
			}
		}

		public static void PreallocateMemory()
		{
			GC.Collect();
			ReanimatorTransform.PreallocateMemory();
			Reanimation.PreallocateMemory();
			ReanimatorTrackInstance.PreallocateMemory();
			Attachment.PreallocateMemory();
			Plant.PreallocateMemory();
			Zombie.PreallocateMemory();
			Projectile.PreallocateMemory();
			XNASoundInstance.PreallocateMemory();
			TodParticle.PreallocateMemory();
			RenderItem.PreallocateMemory();
			TodParticleEmitter.PreallocateMemory();
			SexyAppBase.XnaGame.CompensateForSlowUpdate();
		}

		public override void LoadingThreadCompleted()
		{
			PropertiesParser propertiesParser = new PropertiesParser(this);
			if (propertiesParser.ParsePropertiesFile("properties/content.xml"))
			{
				string @string = base.GetString("ContentUpdateSitePrefix");
				if (!string.IsNullOrEmpty(@string))
				{
					Debug.OutputDebug<string>(Common.StrFormat_("Content Update: URL={0}\n", @string));
				}
				else
				{
					Debug.OutputDebug<string>("Content Update: Failed to find property 'ContentUpdateSitePrefix'.\n");
				}
			}
			else
			{
				Debug.OutputDebug<string>(Common.StrFormat_("Content Update: Failed to parse properties file: {0}\n", propertiesParser.GetErrorText()));
			}
			
			
			// Cached GameObjects
			for (SeedType i = 0; i < SeedType.NUM_SEED_TYPES; i++) 
			{
				if (i == SeedType.SEED_SPROUT) continue;
				mReanimatorCache.MakeCachedPlantFrame(i, DrawVariation.VARIATION_NORMAL);
			}
			for (LawnMowerType i = 0; i < LawnMowerType.NUM_MOWER_TYPES ; i++)
			{
				mReanimatorCache.MakeCachedMowerFrame(i);
			}
			for (ZombieType i = 0; i < ZombieType.NUM_CACHED_ZOMBIE_TYPES; i++)
			{
				if (i == ZombieType.NUM_ZOMBIE_TYPES) continue;
				mReanimatorCache.MakeCachedZombieFrame(i);
			}
			for (DrawVariation j = DrawVariation.VARIATION_MARIGOLD_WHITE; j <= DrawVariation.VARIATION_MARIGOLD_LIGHT_GREEN; j++) 
			{
				mReanimatorCache.MakeCachedPlantFrame(SeedType.SEED_MARIGOLD, j);
			}
			//mReanimatorCache.MakeCachedPlantFrame(SeedType.SEED_MARIGOLD, DrawVariation.VARIATION_MARIGOLD_WHITE);

			GC.Collect();
			SexyAppBase.XnaGame.CompensateForSlowUpdate();
		}

		public virtual bool DebugKeyDown(int theKey)
		{
			return false;
		}

		public virtual void HandleCmdLineParam(string theParamName, string theParamValue)
		{
		}

		public override void PlaySample(int theSoundNum)
		{
			if (!this.mMuteSoundsForCutscene)
			{
				base.PlaySample(theSoundNum);
			}
		}

		public void ConfirmQuit()
		{
			string theDialogLines = TodStringFile.TodStringTranslate("[QUIT_MESSAGE]");
			string theDialogHeader = TodStringFile.TodStringTranslate("[QUIT_HEADER]");
			LawnDialog lawnDialog = this.DoDialog(13, true, theDialogHeader, theDialogLines, "", 2);
			lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[QUIT_BUTTON]");
			LawnApp.CenterDialog(lawnDialog, lawnDialog.mWidth, lawnDialog.mHeight);
		}

		public void ConfirmCheckForUpdates()
		{
		}

		public void CheckForUpdates()
		{
		}

		public void DoUserDialog()
		{
			this.KillDialog(29);
			UserDialog userDialog = new UserDialog(this);
			LawnApp.CenterDialog(userDialog, userDialog.mWidth, userDialog.mHeight);
			base.AddDialog(29, userDialog);
			this.mWidgetManager.SetFocus(userDialog);
		}

		public void FinishUserDialog(bool isYes)
		{
			UserDialog userDialog = (UserDialog)base.GetDialog(Dialogs.DIALOG_USERDIALOG);
			if (userDialog == null)
			{
				return;
			}
			if (isYes)
			{
				PlayerInfo profile = this.mProfileMgr.GetProfile(userDialog.GetSelName());
				if (profile != null)
				{
					this.mPlayerInfo = profile;
					this.mWidgetManager.MarkAllDirty();
					if (this.mGameSelector != null)
					{
						this.mGameSelector.SyncProfile(true);
					}
				}
			}
			this.KillDialog(29);
		}

		public void DoCreateUserDialog(bool isOnlyUser)
		{
			this.FinishCreateUserDialog(true);
		}

		public void DoCheatDialog()
		{
			this.KillDialog(35);
			CheatDialog cheatDialog = new CheatDialog(this);
			LawnApp.CenterDialog(cheatDialog, cheatDialog.mWidth, cheatDialog.mHeight);
			base.AddDialog(35, cheatDialog);
		}

		public void FinishCheatDialog(bool isYes)
		{
			CheatDialog cheatDialog = (CheatDialog)base.GetDialog(Dialogs.DIALOG_CHEAT);
			if (cheatDialog == null)
			{
				return;
			}
			if (isYes && !cheatDialog.ApplyCheat())
			{
				return;
			}
			this.KillDialog(35);
			if (isYes)
			{
				this.mMusic.StopAllMusic();
				this.mBoardResult = BoardResult.BOARDRESULT_CHEAT;
				this.PreNewGame(this.mGameMode, false);
			}
		}

		public void FinishCreateUserDialog(bool isYes)
		{
			string gamertag = "Player";//Gamer.SignedInGamers[PlayerIndex.One].Gamertag;
			string theDialogLines = "[ENTER_NEW_USER]";
			if (isYes && gamertag.empty() && this.mPlayerInfo != null)
			{
				this.KillDialog(30);
				return;
			}
			if (this.mPlayerInfo == null && (!isYes || gamertag.empty()))
			{
				this.DoDialog(33, true, "[ENTER_YOUR_NAME]", theDialogLines, "[DIALOG_BUTTON_OK]", 3);
				return;
			}
			if (!isYes)
			{
				this.KillDialog(30);
				return;
			}
			PlayerInfo playerInfo = this.mProfileMgr.AddProfile(gamertag);
			if (playerInfo == null)
			{
				this.DoDialog(33, true, "[NAME_CONFLICT]", "[ENTER_UNIQUE_PLAYER_NAME]", "[DIALOG_BUTTON_OK]", 3);
				return;
			}
			this.mProfileMgr.Save();
			this.mPlayerInfo = playerInfo;
			this.KillDialog(29);
			this.KillDialog(30);
			this.mWidgetManager.MarkAllDirty();
			if (this.mGameSelector != null)
			{
				this.mGameSelector.SyncProfile(true);
			}
		}

		public void DoConfirmDeleteUserDialog(string theName)
		{
			this.KillDialog(31);
			this.DoDialog(31, true, "[ARE_YOU_SURE]", Common.StrFormat_(TodStringFile.TodStringTranslate("[DELETE_USER_WARNING]"), theName), "", 1);
		}

		public void FinishConfirmDeleteUserDialog(bool isYes)
		{
			this.KillDialog(31);
			UserDialog userDialog = (UserDialog)base.GetDialog(Dialogs.DIALOG_USERDIALOG);
			if (userDialog == null)
			{
				return;
			}
			this.mWidgetManager.SetFocus(userDialog);
			if (!isYes)
			{
				return;
			}
			string text = (this.mPlayerInfo != null) ? this.mPlayerInfo.mName : "";
			string selName = userDialog.GetSelName();
			if (selName == text)
			{
				this.mPlayerInfo = null;
			}
			this.mProfileMgr.DeleteProfile(selName);
			userDialog.FinishDeleteUser();
			if (this.mPlayerInfo == null)
			{
				this.mPlayerInfo = this.mProfileMgr.GetProfile(userDialog.GetSelName());
				if (this.mPlayerInfo == null)
				{
					this.mPlayerInfo = this.mProfileMgr.GetAnyProfile();
				}
			}
			this.mProfileMgr.Save();
			if (this.mPlayerInfo == null)
			{
				this.DoCreateUserDialog(true);
			}
			this.mWidgetManager.MarkAllDirty();
			if (this.mGameSelector != null)
			{
				this.mGameSelector.SyncProfile(true);
			}
		}

		public void DoRenameUserDialog(string theName)
		{
			this.KillDialog(32);
			NewUserDialog newUserDialog = new NewUserDialog(this, true, true);
			newUserDialog.Move(this.mWidth / 2 - newUserDialog.mWidth / 2, (int)Constants.InvertAndScale(20f));
			newUserDialog.SetName(theName);
			base.AddDialog(32, newUserDialog);
		}

		public void FinishRenameUserDialog(bool isYes)
		{
			UserDialog userDialog = (UserDialog)base.GetDialog(29);
			if (!isYes)
			{
				this.KillDialog(32);
				this.mWidgetManager.SetFocus(userDialog);
				return;
			}
			NewUserDialog newUserDialog = (NewUserDialog)base.GetDialog(32);
			if (userDialog == null || newUserDialog == null)
			{
				return;
			}
			string selName = userDialog.GetSelName();
			string name = newUserDialog.GetName();
			if (string.IsNullOrEmpty(name))
			{
				this.KillDialog(32);
				this.mWidgetManager.SetFocus(userDialog);
				return;
			}
			bool flag = this.mProfileMgr.GetProfile(selName) == this.mPlayerInfo;
			if (!this.mProfileMgr.RenameProfile(selName, name))
			{
				this.DoDialog(34, true, "[NAME_CONFLICT]", "[ENTER_UNIQUE_PLAYER_NAME]", "[DIALOG_BUTTON_OK]", 3);
				return;
			}
			this.mProfileMgr.Save();
			if (flag)
			{
				this.mPlayerInfo = this.mProfileMgr.GetProfile(name);
			}
			userDialog.FinishRenameUser(name);
			this.mWidgetManager.MarkAllDirty();
			this.KillDialog(32);
			this.mWidgetManager.SetFocus(userDialog);
		}

		public void FinishNameError(int theId)
		{
			this.KillDialog(theId);
			NewUserDialog newUserDialog = (NewUserDialog)base.GetDialog(Dialogs.DIALOG_CREATEUSER);
			if (newUserDialog != null)
			{
				this.mWidgetManager.SetFocus(newUserDialog.mNameEditWidget);
			}
		}

		public void FinishRestartConfirmDialog()
		{
			this.mKilledYetiAndRestarted = this.mBoard.mKilledYeti;
			this.KillDialog(37);
			this.KillDialog(39);
			this.KillBoard();
			this.PreNewGame(this.mGameMode, false);
		}

		public void FinishInGameRestartConfirmDialog(bool isYes)
		{
			this.KillDialog(23);
			if (isYes)
			{
				this.mMusic.StopAllMusic();
				this.mSoundSystem.CancelPausedFoley();
				this.KillNewOptionsDialog();
				this.mBoardResult = BoardResult.BOARDRESULT_RESTART;
				if (this.mBoard != null)
				{
					this.mKilledYetiAndRestarted = this.mBoard.mKilledYeti;
				}
				this.PreNewGame(this.mGameMode, false);
			}
		}

		public void FinishAboutDialog(bool isYes)
		{
			this.KillDialog(52);
		}

		public void FinishRestartWarningDialog(bool isYes)
		{
			this.KillDialog(53);
		}

		public void FinishPacketSlotPurchaseDialog(bool isYes)
		{
			this.KillDialog(51);
			if (isYes)
			{
				int itemCost = StoreScreen.GetItemCost(StoreItem.STORE_ITEM_PACKET_UPGRADE);
				this.mPlayerInfo.AddCoins(-itemCost);
				this.mPlayerInfo.mPurchases[21]++;
				this.WriteCurrentUserConfig();
				this.mBoard.mSeedBank.UpdateHeight();
				if (this.mCrazyDaveMessageIndex == 1503)
				{
					this.CrazyDaveTalkIndex(1510);
					return;
				}
				if (this.mCrazyDaveMessageIndex == 1553)
				{
					this.CrazyDaveTalkIndex(1560);
					return;
				}
			}
			else
			{
				this.mPlayerInfo.mDidntPurchasePacketUpgrade++;
				if (this.mCrazyDaveMessageIndex == 1503)
				{
					this.CrazyDaveTalkIndex(1520);
					return;
				}
				if (this.mCrazyDaveMessageIndex == 1553)
				{
					this.CrazyDaveTalkIndex(1570);
				}
			}
		}

		public void FinishTimesUpDialog()
		{
			this.KillDialog(42);
		}

		public void FinishPlantSale(bool isYes)
		{
			this.mZenGarden.DoPlantSale(isYes);
			this.KillDialog(48);
		}

		public void FinishLawnDialogMessageBox(bool isYes)
		{
			if (this.mLawnMessageBoxListener != null)
			{
				this.mLawnMessageBoxListener.LawnMessageBoxDone(isYes ? 1000 : 1001);
				this.mLawnMessageBoxListener = null;
				this.mWidgetManager.SetFocus(this.mOldFocus);
				this.mOldFocus = null;
			}
		}

		public void KillBoard()
		{
			this.FinishModelessDialogs();
			this.KillSeedChooserScreen();
			if (this.mBoard != null)
			{
				this.mBoard.DisposeBoard();
				this.mWidgetManager.RemoveWidget(this.mBoard);
				base.SafeDeleteWidget(this.mBoard);
				this.mBoard = null;
			}
		}

		public void MakeNewBoard()
		{
			this.KillBoard();
			this.mBoard = new Board(this);
			this.mBoard.Resize(Constants.Board_Offset_AspectRatio_Correction, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mBoard);
			this.mWidgetManager.BringToBack(this.mBoard);
			this.mWidgetManager.SetFocus(this.mBoard);
			GC.Collect();
			SexyAppBase.XnaGame.CompensateForSlowUpdate();
		}

		public void StartPlaying()
		{
			this.KillSeedChooserScreen();
			this.mBoard.StartLevel();
			this.mGameScene = GameScenes.SCENE_PLAYING;
		}

		public bool TryLoadGame()
		{
			string savedGameName = LawnCommon.GetSavedGameName(this.mGameMode, (int)this.mPlayerInfo.mId);
			this.mMusic.StopAllMusic();
			if (base.FileExists(savedGameName))
			{
				this.MakeNewBoard();
				if (this.mBoard.LoadGame(savedGameName))
				{
					this.mFirstTimeGameSelector = false;
					this.DoContinueDialog();
					return true;
				}
				this.KillBoard();
			}
			return false;
		}

		internal override void NewGame()
		{
			this.mFirstTimeGameSelector = false;
			this.MakeNewBoard();
			this.mBoard.InitLevel();
			this.mBoardResult = BoardResult.BOARDRESULT_NONE;
			this.mGameScene = GameScenes.SCENE_LEVEL_INTRO;
			this.ShowSeedChooserScreen();
			this.mBoard.mCutScene.StartLevelIntro();
		}

		public bool RestoreGame()
		{
			string savedGameName = LawnCommon.GetSavedGameName(this.mRestoreGameMode, (int)this.mPlayerInfo.mId);
			this.mMusic.StopAllMusic();
			if (base.FileExists(savedGameName))
			{
				this.mGameMode = this.mRestoreGameMode;
				this.MakeNewBoard();
				if (this.mBoard.LoadGame(savedGameName))
				{
					if (this.mBoard.mNextSurvivalStageCounter != 1)
					{
						string savedGameName2 = LawnCommon.GetSavedGameName(this.mRestoreGameMode, (int)this.mPlayerInfo.mId);
						base.EraseFile(savedGameName2);
					}
					this.mBoard.Pause(false);
					this.DoPauseDialog();
					return true;
				}
				this.KillBoard();
			}
			return false;
		}

		public void RestartLoopingSounds()
		{
			if (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS || this.IsStormyNightLevel())
			{
				this.PlayFoley(FoleyType.FOLEY_RAIN);
			}
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && zombie.mPlayingSong)
				{
					zombie.mPlayingSong = false;
					zombie.StartZombieSound();
				}
			}
		}

		public void PreNewGame(GameMode theGameMode, bool theLookForSavedGame)
		{
			this.PreNewGame(theGameMode, theLookForSavedGame, true);
		}

		public void PreNewGame(GameMode theGameMode, bool theLookForSavedGame, bool checkForTutorialCompletion)
		{
			if (this.NeedRegister())
			{
				this.ShowGameSelector();
				return;
			}
			this.DelayLoadMainMenuResource(false);
			if (theGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.DelayLoadZenGardenResources(true);
			}
			else
			{
				this.DelayLoadZenGardenResources(false);
			}
			GC.Collect();
			GC.WaitForPendingFinalizers();
			this.DelayLoadGamePlayResources(true);
			if (SexyAppBase.IsInTrialMode && this.mPlayerInfo.mLevel >= 7 && theGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				if (this.mPlayerInfo.mNeedsTrialLevelReset)
				{
					this.mPlayerInfo.SetLevel(1);
					this.mPlayerInfo.mNeedsTrialLevelReset = false;
				}
				else
				{
					theGameMode = GameMode.GAMEMODE_UPSELL;
				}
			}
			if (SexyAppBase.IsInTrialMode && checkForTutorialCompletion && theGameMode == GameMode.GAMEMODE_ADVENTURE && this.mPlayerInfo.mLevel <= 3 && this.mPlayerInfo.mHasFinishedTutorial && this.mPlayerInfo.mFinishedAdventure == 0)
			{
				LawnDialog theDialog = this.DoDialog(58, true, string.Empty, "[SKIP_TUTORIAL_MESSAGE]", string.Empty, 1);
				LawnApp.CenterDialog(theDialog, (int)Constants.InvertAndScale(400f), (int)Constants.InvertAndScale(200f));
				return;
			}
			this.mGameMode = theGameMode;
			if (theLookForSavedGame && this.TryLoadGame())
			{
				return;
			}
			string savedGameName = LawnCommon.GetSavedGameName(this.mGameMode, (int)this.mPlayerInfo.mId);
			base.EraseFile(savedGameName);
			this.NewGame();
			if (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && !this.mPlayerInfo.mZenGardenTutorialComplete)
			{
				this.mZenGarden.SetupForZenTutorial();
			}
		}

		public void ShowGameSelectorWithOptions()
		{
			this.ShowGameSelector();
			this.DoNewOptions(true);
		}

		public void ShowGameSelector()
		{
			this.KillBoard();
			this.UpdateRegisterInfo();
			this.DelayLoadGamePlayResources(false);
			this.DelayLoadMainMenuResource(true);
			if (this.mGameSelector != null)
			{
				this.mWidgetManager.RemoveWidget(this.mGameSelector);
				base.SafeDeleteWidget(this.mGameSelector);
			}
			this.mGameScene = GameScenes.SCENE_MENU;
			this.mGameSelector = new GameSelector(this);
			this.mGameSelector.Resize(0, 0, Constants.GameSelector_Width, Constants.GameSelector_Height);
			this.mWidgetManager.AddWidget(this.mGameSelector);
			this.mWidgetManager.BringToBack(this.mGameSelector);
			this.mWidgetManager.SetFocus(this.mGameSelector);
			if (this.NeedRegister())
			{
				this.DoNeedRegisterDialog();
			}
		}

		public void KillGameSelector()
		{
			if (this.mGameSelector != null)
			{
				this.mWidgetManager.RemoveWidget(this.mGameSelector);
				base.SafeDeleteWidget(this.mGameSelector);
				this.mGameSelector = null;
			}
		}

		public void ShowGameSelectorQuickPlay(bool theDoFadeIn, GameSelectorButtons theButton)
		{
			this.ShowGameSelector();
			this.mGameSelector.mNeedToPlayRollIn = false;
			this.mGameSelector.MoveToQuickplay(theDoFadeIn, theButton);
		}

		public void ShowGameSelectorQuickPlay(bool theDoFadeIn)
		{
			this.ShowGameSelectorQuickPlay(theDoFadeIn, GameSelectorButtons.GameSelector_MiniGames);
		}

		protected override void ShowUpdateMessage()
		{
			SexyAppBase.UseLiveServers = false;
			if (Guide.IsVisible)
			{
				return;
			}
			Guide.BeginShowMessageBox(TodStringFile.TodStringTranslate("[UPDATE]"), TodStringFile.TodStringTranslate("[UPDATE_REQUIRED]"), new string[]
			{
				TodStringFile.TodStringTranslate("[BUTTON_YES]"),
				TodStringFile.TodStringTranslate("[BUTTON_NO]")
			}, 0, MessageBoxIcon.None, new AsyncCallback(this.GameUpdateMessageClosed), null);
			this.wantToShowUpdateMessage = false;
		}

		protected override bool ShowRunWhenLockedMessage()
		{
			if (Guide.IsVisible)
			{
				return false;
			}
			if (!TodStringFile.StringsLoaded)
			{
				return false;
			}
			Guide.BeginShowMessageBox(TodStringFile.TodStringTranslate("[ALLOW_RUN_WHEN_LOCKED_HEADING]"), TodStringFile.TodStringTranslate("[ALLOW_RUN_WHEN_LOCKED]"), new string[]
			{
				TodStringFile.TodStringTranslate("[BUTTON_YES]"),
				TodStringFile.TodStringTranslate("[BUTTON_NO]")
			}, 0, MessageBoxIcon.None, new AsyncCallback(this.RunWhenLockedMessageClosed), null);
			return true;
		}

		private void RunWhenLockedMessageClosed(IAsyncResult result)
		{
			Main.RunWhenLocked = (Guide.EndShowMessageBox(result) == 0);
			if (this.mPlayerInfo != null)
			{
				this.mPlayerInfo.mRunWhileLocked = Main.RunWhenLocked;
			}
		}

		private void GameUpdateMessageClosed(IAsyncResult result)
		{
			bool flag = Guide.EndShowMessageBox(result) == 0;
			if (!flag)
			{
				SexyAppBase.UseLiveServers = false;
				return;
			}
			if (Main.IsInTrialMode)
			{
				Guide.ShowMarketplace(PlayerIndex.One);
				return;
			}
			/*new MarketplaceDetailTask
			{
				ContentType = 1
			}.Show();*/
		}

		public void ShowAwardScreen(AwardType theAwardType, bool theShowAchievements)
		{
			this.mGameScene = GameScenes.SCENE_AWARD;
			this.mAwardScreen = new AwardScreen(this, theAwardType, theShowAchievements);
			this.mAwardScreen.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mAwardScreen);
			this.mWidgetManager.BringToBack(this.mAwardScreen);
			this.mWidgetManager.SetFocus(this.mAwardScreen);
		}

		public void KillAwardScreen()
		{
			if (this.mAwardScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mAwardScreen);
				base.SafeDeleteWidget(this.mAwardScreen);
				this.mAwardScreen = null;
			}
		}

		public void ShowSeedChooserScreen()
		{
			Debug.ASSERT(this.mSeedChooserScreen == null);
			this.mSeedChooserScreen = new SeedChooserScreen();
			this.mSeedChooserScreen.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mSeedChooserScreen);
			this.mWidgetManager.BringToBack(this.mSeedChooserScreen);
		}

		public void KillSeedChooserScreen()
		{
			if (this.mSeedChooserScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mSeedChooserScreen);
				base.SafeDeleteWidget(this.mSeedChooserScreen);
				this.mSeedChooserScreen = null;
			}
		}

		public void DoBackToMain()
		{
			this.DoBackToMain(true);
		}

		public void DoBackToMain(bool stopMusic)
		{
			if (stopMusic)
			{
				this.mMusic.StopAllMusic();
			}
			this.mSoundSystem.CancelPausedFoley();
			this.mSoundManager.StopAllSounds();
			this.WriteCurrentUserConfig();
			this.KillNewOptionsDialog();
			this.KillBoard();
			this.ShowGameSelector();
			this.mZenGarden.UnloadBackdrop();
		}

		public void DoConfirmBackToMain()
		{
			LawnDialog lawnDialog = this.DoDialog(22, true, "[LEAVE_GAME_HEADER]", "[LEAVE_GAME]", "", 1);
			lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[LEAVE_BUTTON]");
			lawnDialog.mLawnNoButton.mLabel = TodStringFile.TodStringTranslate("[DIALOG_BUTTON_CANCEL]");
			lawnDialog.CalcSize(0, 0);
		}

		public void DoNewOptions(bool theFromGameSelector)
		{
			this.FinishModelessDialogs();
			NewOptionsDialog newOptionsDialog = new NewOptionsDialog(this, theFromGameSelector);
			int theWidth = (int)Constants.InvertAndScale(420f);
			int preferredHeight = newOptionsDialog.GetPreferredHeight(theWidth);
			LawnApp.CenterDialog(newOptionsDialog, theWidth, preferredHeight);
			base.AddDialog(2, newOptionsDialog);
			this.mWidgetManager.SetFocus(newOptionsDialog);
		}

		public void ShowLeaderboardDialog(LeaderBoardType aType)
		{
			this.FinishModelessDialogs();
			LeaderboardDialog leaderboardDialog = new LeaderboardDialog(this, aType);
			int theWidth = (int)Constants.InvertAndScale(420f);
			int preferredHeight = leaderboardDialog.GetPreferredHeight(theWidth);
			LawnApp.CenterDialog(leaderboardDialog, theWidth, preferredHeight);
			base.AddDialog(59, leaderboardDialog);
			this.mLeaderboardScreen.SetGrayed(true);
			this.mWidgetManager.SetFocus(leaderboardDialog);
		}

		public void DoRegister()
		{
		}

		public void DoRegisterError()
		{
			this.DoDialog(9, true, "[INVALID_CODE]", "[INVALID_CODE_MESSAGE]", "[DIALOG_BUTTON_OK]", 3);
		}

		public bool CanDoRegisterDialog()
		{
			return true;
		}

		public bool WriteCurrentUserConfig()
		{
			if (this.mPlayerInfo != null)
			{
				this.mPlayerInfo.SaveDetails();
			}
			return true;
		}

		public void WriteRestoreInfo()
		{
			RestoreLocation theValue = RestoreLocation.RESTORE_TITLESCREEN;
			if (this.mGameSelector != null)
			{
				theValue = RestoreLocation.RESTORE_MAINMENU;
			}
			else if (this.mBoard != null)
			{
				theValue = RestoreLocation.RESTORE_BOARD;
				base.RegistryWriteInteger("RestoreGameMode", (int)this.mGameMode);
			}
			base.RegistryWriteInteger("RestoreLocation", (int)theValue);
		}

		public void ReadRestoreInfo()
		{
			this.mRestoreLocation = RestoreLocation.RESTORE_MAINMENU;
			this.mRestoreLocation = (RestoreLocation)base.RegistryReadInteger("RestoreLocation");
			if (this.mRestoreLocation == RestoreLocation.RESTORE_BOARD)
			{
				this.mRestoreGameMode = (GameMode)base.RegistryReadInteger("RestoreGameMode");
			}
		}

		public void DoLockedAchievementDialog(AchievementId theId)
		{
			string fmt = TodStringFile.TodStringTranslate("[UNLOCK_TO_EARN]");
			string theDialogLines = Common.StrFormat_(fmt, Achievements.GetAchievementItem(theId).Name);
			LawnDialog lawnDialog = this.DoDialog(55, true, "[ACHIEVEMENT_UNLOCKED]", theDialogLines, "", 2);
			lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[CONTINUE_BUTTON]");
			lawnDialog.mLawnNoButton.mLabel = TodStringFile.TodStringTranslate("[GET_FULL_VERSION_BUTTON]");
			lawnDialog.CalcSize(200, 10);
			LawnApp.CenterDialog(lawnDialog, lawnDialog.mWidth, lawnDialog.mHeight);
		}

		public void DoNeedRegisterDialog()
		{
			LawnDialog lawnDialog = this.DoDialog(5, true, "[REGISTER_HEADER]", "[REGISTER]", "", 2);
			lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate("[REGISTER_BUTTON]");
			lawnDialog.mLawnNoButton.mLabel = TodStringFile.TodStringTranslate("[QUIT_BUTTON]");
		}

		public void DoContinueDialog()
		{
			ContinueDialog continueDialog = new ContinueDialog(this);
			LawnApp.CenterDialog(continueDialog, continueDialog.mWidth, continueDialog.mHeight);
			base.AddDialog(37, continueDialog);
		}

		public void DoPauseDialog()
		{
			this.mBoard.Pause(true);
			this.FinishModelessDialogs();
			LawnDialog lawnDialog = this.DoDialog(19, true, "[GAME_PAUSED]", "", "[RESUME_GAME]", 3);
			int num = Math.Max(Resources.FONT_DWARVENTODCRAFT15.StringWidth(TodStringFile.TodStringTranslate("[GAME_PAUSED]")), Resources.FONT_DWARVENTODCRAFT15.StringWidth(TodStringFile.TodStringTranslate("[RESUME_GAME]")));
			if ((float)num < Constants.InvertAndScale(125f))
			{
				num = (int)Constants.InvertAndScale(125f);
			}
			int num2 = AtlasResources.IMAGE_DIALOG_TOPLEFT.mWidth + num + AtlasResources.IMAGE_DIALOG_TOPRIGHT.mWidth;
			lawnDialog.mReanimation.AddReanimation((float)(num2 / 2) - Constants.InvertAndScale(85f), Constants.InvertAndScale(30f), ReanimationType.REANIM_ZOMBIE_NEWSPAPER);
			lawnDialog.mSpaceAfterHeader = (int)Constants.InvertAndScale(65f);
			lawnDialog.CalcSize((int)Constants.InvertAndScale(20f), (int)Constants.InvertAndScale(10f), num);
			LawnApp.CenterDialog(lawnDialog, lawnDialog.mWidth, lawnDialog.mHeight);
			if (this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_HAMMER)
			{
				this.EnforceCursor();
			}
		}

		public void FinishModelessDialogs()
		{
		}

		public LawnDialog DoDialog(int theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
		{
			TodStringFile.TodStringTranslate(theDialogHeader);
			TodStringFile.TodStringTranslate(theDialogLines);
			TodStringFile.TodStringTranslate(theDialogFooter);
			LawnDialog lawnDialog = new LawnDialog(this, null, theDialogId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode);
			base.DoDialog(lawnDialog, theDialogId);
			if (this.mWidgetManager.mFocusWidget == null)
			{
				this.mWidgetManager.mFocusWidget = lawnDialog;
			}
			return lawnDialog;
		}

		public LawnDialog DoDialogDelay(int theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
		{
			LawnDialog lawnDialog = new LawnDialog(this, null, theDialogId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode);
			base.DoDialog(lawnDialog, theDialogId);
			lawnDialog.SetButtonDelay(30);
			return lawnDialog;
		}

		public override void Shutdown()
		{
			if (!this.mLoadingThreadCompleted)
			{
				this.mLoadingFailed = true;
				return;
			}
			if (!this.mShutdown)
			{
				for (int i = 0; i < 60; i++)
				{
					this.KillDialog(i);
				}
				if (this.mBoard != null)
				{
					this.mBoardResult = BoardResult.BOARDRESULT_QUIT_APP;
					this.mWidgetManager.mDownButtons = 0;
					this.mBoard.TryToSaveGame();
					this.KillBoard();
					this.WriteCurrentUserConfig();
				}
				this.mProfileMgr.Save();
				base.ProcessSafeDeleteList();
				if (this.mZenGarden != null)
				{
					this.mZenGarden = null;
				}
				if (this.mEffectSystem != null)
				{
					this.mEffectSystem.EffectSystemDispose();
					this.mEffectSystem.Dispose();
					this.mEffectSystem = null;
				}
				if (this.mReanimatorCache != null)
				{
					this.mReanimatorCache.ReanimatorCacheDispose();
					this.mReanimatorCache = null;
				}
				TodParticleGlobal.TodParticleFreeDefinitions();
				ReanimatorXnaHelpers.ReanimatorFreeDefinitions();
				Coin.CoinFreeTextures();
				GlobalMembersTrail.TrailFreeDefinitions();
				this.UpdateRegisterInfo();
				base.Shutdown();
			}
		}

		public override void Init()
		{
			bool flag = this.mTodCheatKeys;
			this.mSessionID = (int)DateTime.UtcNow.Ticks;
			this.mPlayTimeActiveSession = 0;
			this.mPlayTimeInactiveSession = 0;
			this.mBoardResult = BoardResult.BOARDRESULT_NONE;
			this.mKilledYetiAndRestarted = false;
			base.Init();
			this.ReadRestoreInfo();
			if (!this.mResourceManager.ParseResourcesFile("Content/resources.xml"))
			{
				this.ShowResourceError(true);
				return;
			}
			if (Constants.Language != Constants.LanguageIndex.de)
			{
				if (!TodCommon.TodLoadResources("Init"))
				{
					return;
				}
			}
			else if (!TodCommon.TodLoadResources("InitRegistered"))
			{
				return;
			}
			Resources.ExtractInitResources(this.mResourceManager);
			PerfTimer perfTimer = default(PerfTimer);
			perfTimer.Start();
			this.mProfileMgr.Load();
			string empty = string.Empty;
			if (this.mPlayerInfo == null && this.RegistryReadString("CurUser", empty) != null)
			{
				this.mPlayerInfo = this.mProfileMgr.GetProfile(empty);
			}
			try
			{
				if (MediaPlayer.GameHasControl)
				{
					MediaPlayer.Play(this.mContentManager.Load<Song>(GlobalStaticVars.GetResourceDir() + "music/crazydave"));
				}
			}
			catch (Exception)
			{
			}
			if (this.mPlayerInfo == null)
			{
				PlayerInfo anyProfile = this.mProfileMgr.GetAnyProfile();
				if (!MediaPlayer.GameHasControl)
				{
					anyProfile.mMusicVolume = (double)MediaPlayer.Volume;
				}
				this.mPlayerInfo = anyProfile;
			}
			this.mMaxExecutions = base.GetInteger("MaxExecutions", 0);
			this.mMaxPlays = base.GetInteger("MaxPlays", 0);
			this.mMaxTime = base.GetInteger("MaxTime", 60);
			this.mTitleScreen = new TitleScreen(this);
			this.mTitleScreen.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mTitleScreen);
			this.mWidgetManager.SetFocus(this.mTitleScreen);
			perfTimer.Start();
			this.mMusic = new Music();
			this.mSoundSystem = new TodFoley();
			this.mEffectSystem = new EffectSystem();
			this.mEffectSystem.EffectSystemInitialize();
			FilterEffect.FilterEffectInitForApp();
			this.mKonamiCheck = new TypingCheck();
			this.mKonamiCheck.AddKeyCode(KeyCode.KEYCODE_UP);
			this.mKonamiCheck.AddKeyCode(KeyCode.KEYCODE_UP);
			this.mKonamiCheck.AddKeyCode(KeyCode.KEYCODE_DOWN);
			this.mKonamiCheck.AddKeyCode(KeyCode.KEYCODE_DOWN);
			this.mKonamiCheck.AddKeyCode(KeyCode.KEYCODE_LEFT);
			this.mKonamiCheck.AddKeyCode(KeyCode.KEYCODE_RIGHT);
			this.mKonamiCheck.AddKeyCode(KeyCode.KEYCODE_LEFT);
			this.mKonamiCheck.AddKeyCode(KeyCode.KEYCODE_RIGHT);
			this.mKonamiCheck.AddChar('b');
			this.mKonamiCheck.AddChar('a');
			this.mMustacheCheck = new TypingCheck("mustache");
			this.mMoustacheCheck = new TypingCheck("moustache");
			this.mSuperMowerCheck = new TypingCheck("trickedout");
			this.mSuperMowerCheck2 = new TypingCheck("tricked out");
			this.mFutureCheck = new TypingCheck("future");
			this.mPinataCheck = new TypingCheck("pinata");
			this.mDanceCheck = new TypingCheck("dance");
			this.mDaisyCheck = new TypingCheck("daisies");
			this.mSukhbirCheck = new TypingCheck("sukhbir");
			perfTimer.Start();
			perfTimer.Start();
		}

		public override void Start()
		{
			if (this.mLoadingFailed)
			{
				return;
			}
			base.Start();
		}

		public string RegistryReadString(string key, string value)
		{
			return null;
		}

		public virtual Dialog NewDialog(int theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
		{
			LawnDialog lawnDialog = new LawnDialog(this, null, theDialogId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode);
			if (lawnDialog.mWidth < 380)
			{
				lawnDialog.mWidth = 380;
			}
			if (lawnDialog.mHeight > 320)
			{
				lawnDialog.mHeight = 320;
			}
			LawnApp.CenterDialog(lawnDialog, lawnDialog.mWidth, lawnDialog.mHeight);
			return lawnDialog;
		}

		public override bool KillDialog(int theDialogId)
		{
			if (base.KillDialog(theDialogId))
			{
				if (this.mDialogMap.Count == 0)
				{
					if (this.mBoard != null)
					{
						this.mWidgetManager.SetFocus(this.mBoard);
					}
					else if (this.mGameSelector != null)
					{
						this.mWidgetManager.SetFocus(this.mGameSelector);
					}
				}
				if (this.mBoard != null && !this.NeedPauseGame())
				{
					this.mBoard.Pause(false);
				}
				return true;
			}
			return false;
		}

		public override void ModalOpen()
		{
			if (this.mBoard != null && this.NeedPauseGame())
			{
				this.mBoard.Pause(true);
			}
		}

		public override void ModalClose()
		{
		}

		public void PreDisplayHook()
		{
			this.PreDisplayHook();
		}

		public bool ChangeDirHook(string theIntendedPath)
		{
			return false;
		}

		public bool NeedRegister()
		{
			return false;
		}

		public void UpdateRegisterInfo()
		{
		}

		public override void ButtonPress(int theId)
		{
		}

		public override void ButtonDepress(int theId)
		{
			if (theId % 10000 >= 2000 && theId % 10000 < 3000)
			{
				int num = theId - 2000;
				switch (num)
				{
				case 0:
					this.KillDialog(0);
					this.ShowGameSelector();
					return;
				case 1:
				case 3:
				case 4:
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 14:
				case 17:
				case 18:
				case 24:
				case 25:
				case 26:
				case 27:
				case 36:
				case 37:
				case 38:
				case 40:
				case 41:
				case 43:
				case 44:
				case 45:
				case 50:
				case 54:
				case 56:
				case 57:
					break;
				case 2:
					this.KillNewOptionsDialog();
					if (this.mBoard != null)
					{
						this.RestartLoopingSounds();
						return;
					}
					return;
				case 5:
					this.DoRegister();
					return;
				case 6:
					return;
				case 7:
					this.KillDialog(7);
					this.CheckForUpdates();
					return;
				case 13:
					this.KillDialog(13);
					this.CloseRequestAsync();
					return;
				case 15:
					this.KillDialog(15);
					this.DoRegister();
					return;
				case 16:
					this.KillDialog(16);
					return;
				case 19:
					this.KillDialog(19);
					return;
				case 20:
					this.KillDialog(20);
					this.mBoard.AddSunMoney(100);
					return;
				case 21:
					this.KillDialog(21);
					return;
				case 22:
					this.KillDialog(22);
					this.mBoardResult = BoardResult.BOARDRESULT_QUIT;
					this.mBoard.TryToSaveGame();
					this.DoBackToMain();
					return;
				case 23:
					this.FinishInGameRestartConfirmDialog(true);
					return;
				case 28:
				case 49:
					this.KillDialog(theId - 2000);
					this.FinishLawnDialogMessageBox(true);
					return;
				case 29:
					this.FinishUserDialog(true);
					return;
				case 30:
					this.FinishCreateUserDialog(true);
					return;
				case 31:
					this.FinishConfirmDeleteUserDialog(true);
					return;
				case 32:
					this.FinishRenameUserDialog(true);
					return;
				case 33:
					this.FinishNameError(theId - 2000);
					return;
				case 34:
					this.FinishNameError(theId - 2000);
					return;
				case 35:
					this.FinishCheatDialog(true);
					return;
				case 39:
					this.FinishRestartConfirmDialog();
					return;
				case 42:
					this.FinishTimesUpDialog();
					return;
				case 46:
					((StoreScreen)base.GetDialog(4)).PurchasePendingItem();
					return;
				case 47:
					((StoreScreen)base.GetDialog(4)).FinishTreeOfWisdomDialog(true);
					return;
				case 48:
					this.FinishPlantSale(true);
					return;
				case 51:
					this.FinishPacketSlotPurchaseDialog(true);
					return;
				case 52:
					this.FinishAboutDialog(true);
					return;
				case 53:
					this.FinishRestartWarningDialog(true);
					return;
				case 55:
					this.KillDialog(55);
					return;
				case 58:
					this.KillDialog(theId - 2000);
					this.PreNewGame(GameMode.GAMEMODE_ADVENTURE, true, false);
					this.mPlayerInfo.mHasFinishedTutorial = false;
					return;
				default:
					if (num == 20008)
					{
						this.KillDialog(20008);
						this.KillDialog(8);
						return;
					}
					break;
				}
				this.KillDialog(theId - 2000);
				return;
			}
			if (theId % 10000 >= 3000 && theId % 10000 < 4000)
			{
				int num2 = theId - 3000;
				if (num2 <= 42)
				{
					switch (num2)
					{
					case 5:
						this.KillDialog(5);
						this.Shutdown();
						return;
					case 6:
						this.KillDialog(6);
						return;
					default:
						switch (num2)
						{
						case 23:
							this.FinishInGameRestartConfirmDialog(false);
							return;
						case 24:
						case 25:
						case 26:
						case 27:
						case 33:
						case 34:
							goto IL_47F;
						case 28:
							break;
						case 29:
							this.FinishUserDialog(false);
							return;
						case 30:
							this.FinishCreateUserDialog(false);
							return;
						case 31:
							this.FinishConfirmDeleteUserDialog(false);
							return;
						case 32:
							this.FinishRenameUserDialog(false);
							return;
						case 35:
							this.FinishCheatDialog(false);
							return;
						default:
							if (num2 != 42)
							{
								goto IL_47F;
							}
							this.FinishTimesUpDialog();
							return;
						}
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 47:
						((StoreScreen)base.GetDialog(4)).FinishTreeOfWisdomDialog(false);
						return;
					case 48:
						this.FinishPlantSale(false);
						return;
					case 49:
						break;
					case 50:
					case 52:
					case 53:
					case 54:
						goto IL_47F;
					case 51:
						this.FinishPacketSlotPurchaseDialog(false);
						return;
					case 55:
						this.checkGiveAchievements = true;
						this.BuyGame();
						return;
					default:
						if (num2 == 58)
						{
							this.KillDialog(theId - 3000);
							this.mPlayerInfo.SetLevel(4);
							this.PreNewGame(GameMode.GAMEMODE_ADVENTURE, false, false);
							return;
						}
						if (num2 != 10008)
						{
							goto IL_47F;
						}
						this.KillDialog(10008);
						this.KillDialog(8);
						return;
					}
				}
				this.KillDialog(theId - 3000);
				this.FinishLawnDialogMessageBox(false);
				return;
				IL_47F:
				this.KillDialog(theId - 3000);
			}
		}

		public override void LeftTrialMode()
		{
			base.LeftTrialMode();
			if (this.mGameSelector != null)
			{
				this.mGameSelector.SyncButtons();
			}
		}

		public override void UpdateFrames()
		{
			if (this.wantToShowUpdateMessage)
			{
				this.ShowUpdateMessage();
			}
			if (LoadingScreen.IsLoading)
			{
				LoadingScreen.gLoadingScreen.Update();
				return;
			}
			this.UpdatePlayTimeStats();
			int num = 1;
			if (GlobalStaticVars.gSlowMo)
			{
				GlobalStaticVars.gSlowMoCounter++;
				if (GlobalStaticVars.gSlowMoCounter >= 4)
				{
					GlobalStaticVars.gSlowMoCounter = 0;
				}
				else
				{
					num = 0;
				}
			}
			else if (GlobalStaticVars.gFastMo)
			{
				num = 20;
			}
			for (int i = 0; i < num; i++)
			{
				this.mAppCounter++;
				if (this.mBoard != null)
				{
					this.mBoard.ProcessDeleteQueue();
				}
				base.UpdateFrames();
				if (this.mLoadingThreadCompleted && this.mEffectSystem != null)
				{
					this.mEffectSystem.ProcessDeleteQueue();
				}
				this.CheckForGameEnd();
			}
		}

		public bool IsAdventureMode()
		{
			return this.mGameMode == GameMode.GAMEMODE_ADVENTURE;
		}

		public bool IsQuickPlayMode()
		{
			return this.mGameMode >= GameMode.GAMEMODE_QUICKPLAY_1 && this.mGameMode <= GameMode.GAMEMODE_QUICKPLAY_50;
		}

		public bool IsSurvivalMode()
		{
			return this.mGameMode >= GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1 && this.mGameMode <= GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_5;
		}

		public bool IsContinuousChallenge()
		{
			return this.IsArtChallenge() || this.IsSlotMachineLevel() || this.IsFinalBossLevel() || this.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mGameMode == GameMode.GAMEMODE_UPSELL || this.mGameMode == GameMode.GAMEMODE_INTRO || this.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST;
		}

		public bool IsArtChallenge()
		{
			return this.mBoard != null && (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1 || this.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_2 || this.mGameMode == GameMode.GAMEMODE_CHALLENGE_SEEING_STARS);
		}

		public bool NeedPauseGame()
		{
			if (this.mDialogList.Count == 0)
			{
				return false;
			}
			int num = 0;
			if (this.mDialogList.Count == 1 && this.mDialogList.First.Value.mId != 0)
			{
				num = this.mDialogList.First.Value.mId;
			}
			return num != 28 && num != 51 && num != 50 && (this.mBoard == null || this.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN) && (this.mBoard == null || this.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM);
		}

		public void ShowResourceError()
		{
			this.ShowResourceError(false);
		}

		public override void ShowResourceError(bool doExit)
		{
			base.ShowResourceError(doExit);
		}

		public void ToggleSlowMo()
		{
			GlobalStaticVars.gSlowMoCounter = 0;
			GlobalStaticVars.gSlowMo = !GlobalStaticVars.gSlowMo;
			GlobalStaticVars.gFastMo = false;
		}

		public void ToggleFastMo()
		{
			GlobalStaticVars.gFastMo = !GlobalStaticVars.gFastMo;
			GlobalStaticVars.gSlowMo = false;
		}

		public void PlayFoley(FoleyType theFoleyType)
		{
			if (!this.mMuteSoundsForCutscene)
			{
				this.mSoundSystem.PlayFoley(theFoleyType);
			}
		}

		public void PlayFoleyPitch(FoleyType theFoleyType, float aPitch)
		{
			if (!this.mMuteSoundsForCutscene)
			{
				this.mSoundSystem.PlayFoleyPitch(theFoleyType, aPitch);
			}
		}

		public void FastLoad(GameMode theGameMode)
		{
			if (this.mShutdown)
			{
				return;
			}
			this.mWidgetManager.RemoveWidget(this.mTitleScreen);
			base.SafeDeleteWidget(this.mTitleScreen);
			this.mTitleScreen = null;
			this.PreNewGame(theGameMode, false);
		}

		public string GetStageString(int theLevel)
		{
			string text;
			if (!this.cachedStageStrings.TryGetValue(theLevel, out text))
			{
				int num = TodCommon.ClampInt((theLevel - 1) / 10 + 1, 1, 6);
				int num2 = theLevel - (num - 1) * 10;
				text = Common.StrFormat_(TodStringFile.TodStringTranslate("[STAGE_STRING]"), num, num2);
				this.cachedStageStrings.Add(theLevel, text);
			}
			return text;
		}

		public void KillChallengeScreen()
		{
			if (this.mChallengeScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mChallengeScreen);
				base.SafeDeleteWidget(this.mChallengeScreen);
				this.mChallengeScreen = null;
			}
		}

		public void ShowChallengeScreen(ChallengePage thePage)
		{
			this.mGameScene = GameScenes.SCENE_CHALLENGE;
			this.mChallengeScreen = new ChallengeScreen(this, thePage);
			this.mChallengeScreen.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mChallengeScreen);
			this.mWidgetManager.BringToBack(this.mChallengeScreen);
			this.mWidgetManager.SetFocus(this.mChallengeScreen);
		}

		public void ShowLeaderboardScreen()
		{
			this.mGameScene = GameScenes.SCENE_LEADERBOARD;
			this.mLeaderboardScreen = new LeaderboardScreen(this);
			this.mLeaderboardScreen.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mLeaderboardScreen);
			this.mWidgetManager.BringToBack(this.mLeaderboardScreen);
			this.mWidgetManager.SetFocus(this.mLeaderboardScreen);
		}

		public void KillLeaderboardScreen()
		{
			if (this.mLeaderboardScreen != null)
			{
				this.mLeaderboardScreen.UnloadResources();
				this.mWidgetManager.RemoveWidget(this.mLeaderboardScreen);
				base.SafeDeleteWidget(this.mLeaderboardScreen);
				this.mLeaderboardScreen = null;
			}
		}

		public void CheckForGameEnd()
		{
			if (this.mBoard == null || !this.mBoard.mLevelComplete)
			{
				return;
			}
			bool flag = this.mBoard.CheckForPostGameAchievements();
			flag = false;
			this.UpdatePlayerProfileForFinishingLevel();
			if (this.IsAdventureMode())
			{
				int mLevel = this.mBoard.mLevel;
				this.KillBoard();
				if (this.IsFirstTimeAdventureMode() && mLevel < 50)
				{
					this.ShowAwardScreen(AwardType.AWARD_FOR_LEVEL, flag);
					return;
				}
				if (mLevel == 50)
				{
					if (this.mPlayerInfo.mFinishedAdventure != 1)
					{
						this.ShowAwardScreen(AwardType.AWARD_FOR_LEVEL, flag);
						return;
					}
					this.ShowAwardScreen(AwardType.AWARD_PRE_CREDITS_ZOMBIE_NOTE, flag);
					return;
				}
				else
				{
					if (mLevel == 9 || mLevel == 19 || mLevel == 29 || mLevel == 39 || mLevel == 49)
					{
						this.ShowAwardScreen(AwardType.AWARD_FOR_LEVEL, flag);
						return;
					}
					if (flag)
					{
						this.ShowAwardScreen(AwardType.AWARD_ACHIEVEMENT_ONLY, true);
						return;
					}
					this.PreNewGame(this.mGameMode, false);
					return;
				}
			}
			else if (this.IsQuickPlayMode())
			{
				this.KillBoard();
				if (flag)
				{
					this.ShowAwardScreen(AwardType.AWARD_ACHIEVEMENT_ONLY, flag);
					return;
				}
				this.ShowGameSelectorQuickPlay(false);
				return;
			}
			else if (this.IsSurvivalMode())
			{
				if (this.mBoard.IsFinalSurvivalStage())
				{
					this.KillBoard();
					this.ShowGameSelectorQuickPlay(false);
					return;
				}
				this.mBoard.mChallenge.mSurvivalStage++;
				this.KillSeedChooserScreen();
				this.mBoard.InitSurvivalStage();
				return;
			}
			else
			{
				if (!this.IsPuzzleMode())
				{
					this.KillBoard();
					this.ShowGameSelectorQuickPlay(false);
					return;
				}
				bool flag2 = this.IsIZombieLevel();
				this.KillBoard();
				if (flag2)
				{
					this.ShowGameSelectorQuickPlay(false, GameSelectorButtons.GameSelector_IZombie);
					return;
				}
				this.ShowGameSelectorQuickPlay(false, GameSelectorButtons.GameSelector_Vasebreaker);
				return;
			}
		}

		public virtual void CloseRequestAsync()
		{
			this.mCloseRequest = true;
		}

		public bool IsChallengeWithoutSeedBank()
		{
			return this.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS || this.mGameMode == GameMode.GAMEMODE_UPSELL || this.mGameMode == GameMode.GAMEMODE_INTRO || this.IsWhackAZombieLevel() || this.IsSquirrelLevel() || this.IsScaryPotterLevel() || this.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM;
		}

		public AlmanacDialog DoAlmanacDialog(SeedType theSeedType, ZombieType theZombieType, AlmanacListener theListener)
		{
			default(PerfTimer).Start();
			this.FinishModelessDialogs();
			AlmanacDialog almanacDialog = new AlmanacDialog(this, theListener);
			almanacDialog.Resize(0, 0, Constants.BackBufferSize.Y, Constants.BackBufferSize.X);
			base.AddDialog(3, almanacDialog);
			this.mWidgetManager.SetFocus(almanacDialog);
			if (theSeedType != SeedType.SEED_NONE)
			{
				almanacDialog.ShowPlant(theSeedType);
			}
			else if (theZombieType != ZombieType.ZOMBIE_INVALID)
			{
				almanacDialog.ShowZombie(theZombieType);
			}
			return almanacDialog;
		}

		public bool KillAlmanacDialog()
		{
			if ((AlmanacDialog)base.GetDialog(Dialogs.DIALOG_ALMANAC) == null)
			{
				return false;
			}
			this.KillDialog(3);
			return true;
		}

		public int GetSeedsAvailable()
		{
			int level = this.mPlayerInfo.GetLevel();
			if (this.HasFinishedAdventure() || level > 50)
			{
				return 49;
			}
			int awardSeedForLevel = (int)this.GetAwardSeedForLevel(level);
			return Math.Min(49, awardSeedForLevel);
		}

		public Reanimation AddReanimation(float theX, float theY, int aRenderOrder, ReanimationType theReanimationType)
		{
			return this.AddReanimation(theX, theY, aRenderOrder, theReanimationType, true);
		}

		public Reanimation AddReanimation(float theX, float theY, int aRenderOrder, ReanimationType theReanimationType, bool theDoScalePos)
		{
			if (theDoScalePos)
			{
				theX *= Constants.S;
				theY *= Constants.S;
			}
			return this.mEffectSystem.mReanimationHolder.AllocReanimation(theX, theY, aRenderOrder, theReanimationType);
		}

		public TodParticleSystem AddTodParticle(float theX, float theY, int aRenderOrder, ParticleEffect theEffect)
		{
			return this.mEffectSystem.mParticleHolder.AllocParticleSystem(theX, theY, aRenderOrder, theEffect);
		}

		public TodParticleSystem ParticleGetID(TodParticleSystem theParticle)
		{
			return theParticle;
		}

		public TodParticleSystem ParticleGet(TodParticleSystem theParticleID)
		{
			return theParticleID;
		}

		public TodParticleSystem ParticleTryToGet(TodParticleSystem theParticleID)
		{
			if (theParticleID == null || !theParticleID.mActive)
			{
				return null;
			}
			return theParticleID;
		}

		public Reanimation ReanimationGetID(Reanimation theReanimation)
		{
			if (theReanimation == null || theReanimation.mDead)
			{
				return null;
			}
			return theReanimation;
		}

		public Reanimation ReanimationGet(Reanimation theReanimID)
		{
			if (theReanimID == null || !theReanimID.mActive)
			{
				return null;
			}
			return theReanimID;
		}

		public Reanimation ReanimationTryToGet(Reanimation theReanimID)
		{
			if (theReanimID == null || !theReanimID.mActive)
			{
				return null;
			}
			return theReanimID;
		}

		public void RemoveReanimation(ref Reanimation theReanimationID)
		{
			Reanimation reanimation = this.ReanimationTryToGet(theReanimationID);
			if (reanimation != null)
			{
				reanimation.ReanimationDie();
			}
			theReanimationID = null;
		}

		public void RemoveParticle(TodParticleSystem theParticleID)
		{
			TodParticleSystem todParticleSystem = this.ParticleTryToGet(theParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.ParticleSystemDie();
			}
		}

		public StoreScreen ShowStoreScreen(StoreListener theListener)
		{
			this.DelayLoadGamePlayResources(false);
			this.DelayLoadCachedResources(true);
			this.DelayLoadMainMenuResource(true);
			this.DelayLoadZenGardenResources(true);
			this.FinishModelessDialogs();
			Debug.ASSERT(base.GetDialog(Dialogs.DIALOG_STORE) == null);
			StoreScreen storeScreen = new StoreScreen(this, theListener);
			base.AddDialog(4, storeScreen);
			this.mWidgetManager.SetFocus(storeScreen);
			return storeScreen;
		}

		public UpsellScreen ShowUpsellScreen()
		{
			this.FinishModelessDialogs();
			UpsellScreen upsellScreen = new UpsellScreen(this);
			base.AddDialog(54, upsellScreen);
			this.mWidgetManager.SetFocus(upsellScreen);
			return upsellScreen;
		}

		public void KillStoreScreen()
		{
			if ((AlmanacDialog)base.GetDialog(Dialogs.DIALOG_STORE) == null)
			{
				return;
			}
			this.KillDialog(4);
		}

		public bool HasSeedType(SeedType theSeedType)
		{
			if (theSeedType == SeedType.SEED_GATLINGPEA)
			{
				return this.mPlayerInfo.mPurchases[0] > 0;
			}
			if (this.IsTrialStageLocked() && theSeedType >= SeedType.SEED_JALAPENO)
			{
				return false;
			}
			if (theSeedType == SeedType.SEED_TWINSUNFLOWER)
			{
				return this.mPlayerInfo.mPurchases[1] > 0;
			}
			if (theSeedType == SeedType.SEED_GLOOMSHROOM)
			{
				return this.mPlayerInfo.mPurchases[2] > 0;
			}
			if (theSeedType == SeedType.SEED_CATTAIL)
			{
				return this.mPlayerInfo.mPurchases[3] > 0;
			}
			if (theSeedType == SeedType.SEED_WINTERMELON)
			{
				return this.mPlayerInfo.mPurchases[4] > 0;
			}
			if (theSeedType == SeedType.SEED_GOLD_MAGNET)
			{
				return this.mPlayerInfo.mPurchases[5] > 0;
			}
			if (theSeedType == SeedType.SEED_SPIKEROCK)
			{
				return this.mPlayerInfo.mPurchases[6] > 0;
			}
			if (theSeedType == SeedType.SEED_COBCANNON)
			{
				return this.mPlayerInfo.mPurchases[7] > 0;
			}
			if (theSeedType == SeedType.SEED_IMITATER)
			{
				return this.mPlayerInfo.mPurchases[8] > 0;
			}
			return theSeedType < (SeedType)this.GetSeedsAvailable();
		}

		public void EndLevel()
		{
			this.KillBoard();
			if (this.IsAdventureMode())
			{
				this.NewGame();
			}
		}

		public bool IsIceDemo()
		{
			return false;
		}

		public bool IsShovelLevel()
		{
			return this.mBoard != null && this.mGameMode == GameMode.GAMEMODE_CHALLENGE_SHOVEL;
		}

		public bool IsWallnutBowlingLevel()
		{
			return this.mBoard != null && (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING || this.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING_2 || ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 5) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_5));
		}

		public bool IsMiniBossLevel()
		{
			return this.mBoard != null && ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 10) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_10 || ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 20) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_20) || ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 30) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_30));
		}

		public bool IsSlotMachineLevel()
		{
			return this.mBoard != null && this.mGameMode == GameMode.GAMEMODE_CHALLENGE_SLOT_MACHINE;
		}

		public bool IsLittleTroubleLevel()
		{
			return this.mBoard != null && (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_LITTLE_TROUBLE || ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 25) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_25));
		}

		public bool IsStormyNightLevel()
		{
			return this.mBoard != null && (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_STORMY_NIGHT || ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 40) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_40));
		}

		public bool IsFinalBossLevel()
		{
			return this.mBoard != null && (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_FINAL_BOSS || ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 50) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_50));
		}

		public bool IsBungeeBlitzLevel()
		{
			return this.mBoard != null && (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_BUNGEE_BLITZ || ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 45) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_45));
		}

		public SeedType GetAwardSeedForLevel(int theLevel)
		{
			int num = (theLevel - 1) / 10 + 1;
			int num2 = (theLevel - 1) % 10 + 1;
			int num3 = (num - 1) * 8 + num2;
			if (num2 >= 10)
			{
				num3 -= 2;
			}
			else if (num2 >= 5)
			{
				num3--;
			}
			if (num3 > 40)
			{
				num3 = 40;
			}
			return (SeedType)num3;
		}

		public string GetCrazyDaveText(int theMessageIndex)
		{
			string theText = Common.StrFormat_("[CRAZY_DAVE_{0}]", theMessageIndex);
			theText = TodCommon.TodReplaceString(theText, "{PLAYER_NAME}", this.mPlayerInfo.mName);
			theText = TodCommon.TodReplaceString(theText, "{MONEY}", LawnApp.GetMoneyString(this.mPlayerInfo.mCoins));
			int itemCost = StoreScreen.GetItemCost(StoreItem.STORE_ITEM_PACKET_UPGRADE);
			return TodCommon.TodReplaceString(theText, "{UPGRADE_COST}", LawnApp.GetMoneyString(itemCost));
		}

		public bool CanShowAlmanac()
		{
			return !this.IsIceDemo() && this.mPlayerInfo != null && (this.HasFinishedAdventure() || this.mPlayerInfo.mLevel >= 15);
		}

		public bool IsNight()
		{
			return (this.mBoard != null && this.mBoard.StageIsNight()) || (!this.IsIceDemo() && this.mPlayerInfo != null && ((this.mPlayerInfo.mLevel >= 11 && this.mPlayerInfo.mLevel <= 20) || (this.mPlayerInfo.mLevel >= 31 && this.mPlayerInfo.mLevel <= 40) || this.mPlayerInfo.mLevel == 50));
		}

		public bool CanShowStore()
		{
			return !this.IsIceDemo() && this.mPlayerInfo != null && (this.HasFinishedAdventure() || this.mPlayerInfo.mHasSeenUpsell || this.mPlayerInfo.mLevel >= 25);
		}

		public bool HasBeatenChallenge(GameMode theGameMode)
		{
			if (this.mPlayerInfo == null)
			{
				return false;
			}
			int num = theGameMode - GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1;
			Debug.ASSERT(num >= 0 && num < 122);
			if (this.IsSurvivalNormal(theGameMode))
			{
				return this.mPlayerInfo.mChallengeRecords[num] >= 5;
			}
			if (this.IsSurvivalHard(theGameMode))
			{
				return this.mPlayerInfo.mChallengeRecords[num] >= 10;
			}
			return !this.IsSurvivalEndless(theGameMode) && !this.IsEndlessScaryPotter(theGameMode) && !this.IsEndlessIZombie(theGameMode) && this.mPlayerInfo.mChallengeRecords[num] > 0;
		}

		public PottedPlant GetPottedPlantByIndex(int thePottedPlantIndex)
		{
			Debug.ASSERT(thePottedPlantIndex >= 0 && thePottedPlantIndex < this.mPlayerInfo.mNumPottedPlants);
			return this.mPlayerInfo.mPottedPlant[thePottedPlantIndex];
		}

		public bool IsSurvivalNormal(GameMode theGameMode)
		{
			return theGameMode >= GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1 && theGameMode <= GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_5;
		}

		public bool IsSurvivalHard(GameMode theGameMode)
		{
			return theGameMode >= GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_1 && theGameMode <= GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_5;
		}

		public bool IsSurvivalEndless(GameMode theGameMode)
		{
			return theGameMode >= GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_1 && theGameMode <= GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_5;
		}

		public bool HasFinishedAdventure()
		{
			return this.mPlayerInfo != null && this.mPlayerInfo.mFinishedAdventure > 0;
		}

		public bool IsFirstTimeAdventureMode()
		{
			return this.IsAdventureMode() && !this.HasFinishedAdventure();
		}

		public bool CanSpawnYetis()
		{
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(ZombieType.ZOMBIE_YETI);
			return this.HasFinishedAdventure() && (this.mPlayerInfo.mFinishedAdventure >= 2 || this.mPlayerInfo.mLevel >= zombieDefinition.mStartingLevel);
		}

		public void CrazyDaveEnter()
		{
			Debug.ASSERT(this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_OFF);
			Debug.ASSERT(this.ReanimationTryToGet(this.mCrazyDaveReanimID) == null);
			Reanimation reanimation = this.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_CRAZY_DAVE);
			reanimation.mIsAttachment = true;
			reanimation.SetBasePoseFromAnim(GlobalMembersReanimIds.ReanimTrackId_anim_idle_handing);
			this.mCrazyDaveReanimID = this.ReanimationGetID(reanimation);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_enter, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
			this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_ENTERING;
			this.mCrazyDaveMessageIndex = -1;
			this.mCrazyDaveMessageText = string.Empty;
			this.mCrazyDaveBlinkCounter = TodCommon.RandRangeInt(400, 800);
			if (this.mGameScene == GameScenes.SCENE_LEVEL_INTRO && this.IsStormyNightLevel())
			{
				reanimation.mColorOverride = new SexyColor(64, 64, 64);
			}
		}

		public void FinishZenGardenTutorial()
		{
			this.mZenGarden.mIsTutorial = false;
			this.mPlayerInfo.mZenGardenTutorialComplete = true;
			this.mPlayerInfo.mIsInZenTutorial = false;
			this.mBoardResult = BoardResult.BOARDRESULT_WON;
			this.KillBoard();
			this.PreNewGame(GameMode.GAMEMODE_ADVENTURE, false);
		}

		public void UpdateCrazyDave()
		{
			Reanimation reanimation = this.ReanimationTryToGet(this.mCrazyDaveReanimID);
			if (reanimation == null)
			{
				return;
			}
			if (this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_ENTERING || this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_TALKING)
			{
				if (reanimation.mLoopCount > 0)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 20, 12f);
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_IDLING;
				}
			}
			else if (this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_TALKING)
			{
				if (reanimation.mLoopCount > 0)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle_handing, ReanimLoopType.REANIM_LOOP, 20, 12f);
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_HANDING_IDLING;
				}
			}
			else if (this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_LEAVING && reanimation.mLoopCount > 0)
			{
				this.CrazyDaveDie();
			}
			if (this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_IDLING || this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_IDLING)
			{
				if (this.mCrazyDaveMessageText.IndexOf("{MOUTH_BIG_SMILE}") != -1)
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_dave_mouths, AtlasResources.IMAGE_REANIM_CRAZYDAVE_MOUTH1);
					this.mCrazyDaveMessageText = this.mCrazyDaveMessageText.Replace("{MOUTH_BIG_SMILE}", "");
				}
				else if (this.mCrazyDaveMessageText.IndexOf("{MOUTH_SMALL_SMILE}") != -1)
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_dave_mouths, AtlasResources.IMAGE_REANIM_CRAZYDAVE_MOUTH5);
					this.mCrazyDaveMessageText = this.mCrazyDaveMessageText.Replace("{MOUTH_SMALL_SMILE}", "");
				}
				else if (this.mCrazyDaveMessageText.IndexOf("{MOUTH_BIG_OH}") != -1)
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_dave_mouths, AtlasResources.IMAGE_REANIM_CRAZYDAVE_MOUTH4);
					this.mCrazyDaveMessageText = this.mCrazyDaveMessageText.Replace("{MOUTH_BIG_OH}", "");
				}
				else if (this.mCrazyDaveMessageText.IndexOf("{MOUTH_SMALL_OH}") != -1)
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_dave_mouths, AtlasResources.IMAGE_REANIM_CRAZYDAVE_MOUTH6);
					this.mCrazyDaveMessageText = this.mCrazyDaveMessageText.Replace("{MOUTH_SMALL_OH}", "");
				}
			}
			Reanimation reanimation2;
			if (this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_IDLING || this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_TALKING || this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_TALKING || this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_IDLING)
			{
				this.mCrazyDaveBlinkCounter--;
				if (this.mCrazyDaveBlinkCounter <= 0)
				{
					this.mCrazyDaveBlinkCounter = TodCommon.RandRangeInt(400, 800);
					reanimation2 = this.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_CRAZY_DAVE);
					reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_blink);
					reanimation2.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME_AND_HOLD;
					reanimation2.mAnimRate = 15f;
					reanimation2.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_dave_head);
					reanimation2.mColorOverride = reanimation.mColorOverride;
					reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_dave_eye, -1);
					this.mCrazyDaveBlinkReanimID = this.ReanimationGetID(reanimation2);
				}
			}
			reanimation2 = this.ReanimationTryToGet(this.mCrazyDaveBlinkReanimID);
			if (reanimation2 != null && reanimation2.mLoopCount > 0)
			{
				reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_dave_eye, 0);
				this.RemoveReanimation(ref this.mCrazyDaveBlinkReanimID);
				this.mCrazyDaveBlinkReanimID = null;
			}
			reanimation.Update();
		}

		public void CrazyDaveTalkIndex(int theMessageIndex)
		{
			this.mCrazyDaveMessageIndex = theMessageIndex;
			string crazyDaveText = this.GetCrazyDaveText(theMessageIndex);
			this.CrazyDaveTalkMessage(crazyDaveText);
		}

		public void CrazyDaveTalkMessage(string theMessage)
		{
			Reanimation reanimation = this.ReanimationGet(this.mCrazyDaveReanimID);
			bool flag = false;
			if (theMessage.IndexOf("{HANDING}") != -1)
			{
				flag = true;
				theMessage = theMessage.Replace("{HANDING}", string.Empty);
			}
			if ((this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_TALKING || this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_IDLING) && !flag)
			{
				this.CrazyDaveDoneHanding();
			}
			bool flag2 = true;
			if (theMessage.IndexOf("{NO_SOUND}") != -1)
			{
				flag2 = false;
				theMessage = theMessage.Replace("{NO_SOUND}", "");
			}
			else
			{
				this.CrazyDaveStopSound();
			}
			int num = 0;
			bool flag3 = false;
			for (int i = 0; i < theMessage.length(); i++)
			{
				if (theMessage[i] == '{')
				{
					flag3 = true;
				}
				else if (theMessage[i] == '}')
				{
					flag3 = false;
				}
				else if (!flag3)
				{
					num++;
				}
			}
			Image theImage = null;
			reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_dave_mouths, theImage);
			if (this.mCrazyDaveState != CrazyDaveState.CRAZY_DAVE_TALKING || flag2)
			{
				if (flag)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_talk_handing, ReanimLoopType.REANIM_LOOP, 50, 12f);
					if (flag2 && theMessage.IndexOf("{SHORT_SOUND}") != -1)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVESHORT);
						theMessage = theMessage.Replace("{SHORT_SOUND}", "");
					}
					else if (flag2 && theMessage.IndexOf("{SCREAM}") != -1)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVESCREAM);
						theMessage = theMessage.Replace("{SCREAM}", "");
					}
					else if (flag2)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVELONG);
					}
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_HANDING_TALKING;
				}
				else if (theMessage.IndexOf("{SHAKE}") != -1)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_crazy, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 50, 12f);
					if (flag2)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVECRAZY);
					}
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_TALKING;
					theMessage = theMessage.Replace("{SHAKE}", "");
				}
				else if (theMessage.IndexOf("{SCREAM}") != -1)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_smalltalk, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 50, 12f);
					if (flag2)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVESCREAM);
					}
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_TALKING;
					theMessage = theMessage.Replace("{SCREAM}", "");
				}
				else if (theMessage.IndexOf("{SCREAM2}") != -1)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_mediumtalk, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 50, 12f);
					if (flag2)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVESCREAM2);
					}
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_TALKING;
					theMessage = theMessage.Replace("{SCREAM2}", "");
				}
				else if (theMessage.IndexOf("{SHOW_WALLNUT}") != -1)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_talk_handing, ReanimLoopType.REANIM_LOOP, 50, 12f);
					Reanimation reanimation2 = this.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_WALLNUT);
					reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 12f);
					ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_dave_handinghand);
					AttachEffect attachEffect = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName.mAttachmentID, reanimation2, 100f * Constants.S, 393f * Constants.S);
					attachEffect.mOffset.mMatrix.M11 = 1.2f;
					attachEffect.mOffset.mMatrix.M22 = 1.2f;
					reanimation.Update();
					if (flag2)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVESCREAM2);
					}
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_HANDING_TALKING;
					theMessage = theMessage.Replace("{SHOW_WALLNUT}", "");
				}
				else if (theMessage.IndexOf("{SHOW_HAMMER}") != -1)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_talk_handing, ReanimLoopType.REANIM_LOOP, 50, 12f);
					Reanimation reanimation3 = this.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_HAMMER);
					reanimation3.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_whack_zombie, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
					reanimation3.mAnimTime = 1f;
					ReanimatorTrackInstance trackInstanceByName2 = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_dave_handinghand);
					AttachEffect attachEffect2 = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName2.mAttachmentID, reanimation3, 62f * Constants.S, 445f * Constants.S);
					attachEffect2.mOffset.mMatrix.M11 = 1.5f;
					attachEffect2.mOffset.mMatrix.M22 = 1.5f;
					reanimation.Update();
					if (flag2)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVELONG);
					}
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_HANDING_TALKING;
					theMessage = theMessage.Replace("{SHOW_HAMMER}", "");
				}
				else if (num < 23)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_smalltalk, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 50, 12f);
					if (flag2)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVESHORT);
					}
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_TALKING;
				}
				else if (num < 52)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_mediumtalk, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 50, 12f);
					if (flag2)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVELONG);
					}
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_TALKING;
				}
				else
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_blahblah, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 50, 12f);
					if (flag2)
					{
						this.PlayFoley(FoleyType.FOLEY_CRAZYDAVEEXTRALONG);
					}
					this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_TALKING;
				}
			}
			this.mCrazyDaveMessageText = theMessage;
		}

		public void CrazyDaveLeave()
		{
			Reanimation reanimation = this.ReanimationTryToGet(this.mCrazyDaveReanimID);
			if (reanimation == null)
			{
				return;
			}
			if (this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_TALKING || this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_IDLING)
			{
				this.CrazyDaveDoneHanding();
			}
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_leave, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
			Image theImage = null;
			reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_dave_mouths, theImage);
			this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_LEAVING;
			this.mCrazyDaveMessageIndex = -1;
			this.mCrazyDaveMessageText = string.Empty;
			this.CrazyDaveStopSound();
		}

		public void DrawCrazyDave(Graphics g)
		{
			this.DrawCrazyDave(g, false);
		}

		public void DrawCrazyDave(Graphics g, bool theUseSmallFont)
		{
			Reanimation reanimation = this.ReanimationTryToGet(this.mCrazyDaveReanimID);
			if (reanimation == null)
			{
				return;
			}
			int theWidth = Constants.RetardedDave_Bubble_Size;
			if (!this.mCrazyDaveMessageText.empty())
			{
				int num = (int)Constants.InvertAndScale(285f);
				int num2 = (int)Constants.InvertAndScale(80f);
				Image image_STORE_SPEECHBUBBLE = AtlasResources.IMAGE_STORE_SPEECHBUBBLE;
				if (base.GetDialog(Dialogs.DIALOG_STORE) != null)
				{
					num += Constants.RetardedDave_Bubble_Offset_Shop.X;
					num2 += Constants.RetardedDave_Bubble_Offset_Shop.Y;
					theWidth = (int)Constants.InvertAndScale(150f);
					int num3 = (int)Constants.InvertAndScale(105f);
					g.DrawImage(image_STORE_SPEECHBUBBLE, num, num2, new TRect(0, 0, (int)Constants.InvertAndScale(64f), image_STORE_SPEECHBUBBLE.mHeight));
					g.DrawImage(image_STORE_SPEECHBUBBLE, num + (int)Constants.InvertAndScale(64f), num2, new TRect(image_STORE_SPEECHBUBBLE.mWidth - num3, 0, num3, image_STORE_SPEECHBUBBLE.mHeight));
				}
				else if (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
				{
					num += Constants.ZenGarden_RetardedDaveBubble_Pos.X;
					num2 += Constants.ZenGarden_RetardedDaveBubble_Pos.Y;
					g.DrawImage(image_STORE_SPEECHBUBBLE, num, num2);
				}
				else
				{
					num += Constants.RetardedDave_Bubble_Offset_Board.X;
					num2 += Constants.RetardedDave_Bubble_Offset_Board.Y;
					g.DrawImage(image_STORE_SPEECHBUBBLE, num, num2);
				}
				g.DrawImage(AtlasResources.IMAGE_STORE_SPEECHBUBBLE_TIP, num + (int)Constants.InvertAndScale(30f), num2 - Constants.RetardedDave_Bubble_Tip_Offset + image_STORE_SPEECHBUBBLE.mHeight);
				string text = this.mCrazyDaveMessageText;
				TRect theRect = new TRect(num + Constants.RetardedDave_Bubble_Rect.mX, num2 + Constants.RetardedDave_Bubble_Rect.mY, theWidth, Constants.RetardedDave_Bubble_Rect.mHeight);
				int mX = theRect.mX;
				if (text.IndexOf("{SHAKE}") != -1)
				{
					text = TodCommon.TodReplaceString(text, "{SHAKE}", "");
					theRect.mX += RandomNumbers.NextNumber() % 2;
					theRect.mY += RandomNumbers.NextNumber() % 2;
				}
				bool flag = true;
				if (this.mGameMode == GameMode.GAMEMODE_UPSELL)
				{
					flag = false;
				}
				else if (text.IndexOf("{NO_CLICK}") != -1)
				{
					string text2;
					if (!LawnApp.noClickStringCache.TryGetValue(text, out text2))
					{
						text2 = TodCommon.TodReplaceString(text, "{NO_CLICK}", string.Empty);
						LawnApp.noClickStringCache.Add(text, text2);
					}
					text = text2;
					flag = false;
				}
				g.SetColor(SexyColor.Black);
				g.SetFont(theUseSmallFont ? Resources.FONT_BRIANNETOD12 : Resources.FONT_BRIANNETOD16);
				TodStringFile.TodDrawStringWrapped(g, text, theRect, theUseSmallFont ? Resources.FONT_BRIANNETOD12 : Resources.FONT_BRIANNETOD16, SexyColor.Black, DrawStringJustification.DS_ALIGN_CENTER_VERTICAL_MIDDLE);
				if (flag)
				{
					TodCommon.TodDrawString(g, "[TAP_TO_CONTINUE]", mX + theRect.mWidth / 2, num2 + Constants.RetardedDave_Bubble_TapToContinue_Y, Resources.FONT_PICO129, SexyColor.Black, DrawStringJustification.DS_ALIGN_CENTER);
				}
			}
			reanimation.Draw(g);
		}

		public void CrazyDaveDie()
		{
			Reanimation reanimation = this.ReanimationTryToGet(this.mCrazyDaveReanimID);
			if (reanimation == null)
			{
				return;
			}
			reanimation.ReanimationDie();
			this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_OFF;
			this.mCrazyDaveReanimID = null;
			this.mCrazyDaveMessageIndex = -1;
			this.mCrazyDaveMessageText = string.Empty;
			this.CrazyDaveStopSound();
		}

		public void DoUpsellScreen()
		{
		}

		public void CrazyDaveStopTalking()
		{
			bool flag = true;
			if (this.mGameMode == GameMode.GAMEMODE_UPSELL)
			{
				flag = false;
			}
			if (flag && this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_TALKING)
			{
				this.CrazyDaveDoneHanding();
			}
			Image theImage = null;
			Reanimation reanimation = this.ReanimationGet(this.mCrazyDaveReanimID);
			reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_dave_mouths, theImage);
			if (this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_TALKING && !flag)
			{
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle_handing, ReanimLoopType.REANIM_LOOP, 20, 12f);
				this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_HANDING_IDLING;
			}
			else if (this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_TALKING || this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_HANDING_TALKING)
			{
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 20, 12f);
				this.mCrazyDaveState = CrazyDaveState.CRAZY_DAVE_IDLING;
			}
			this.mCrazyDaveMessageIndex = -1;
			this.mCrazyDaveMessageText = string.Empty;
			this.CrazyDaveStopSound();
		}

		public void PreloadForUser()
		{
			int num = this.mCompletedLoadingThreadTasks + this.GetNumPreloadingTasks();
			if (this.mTitleScreen != null && this.mTitleScreen.mQuickLoadKey != KeyCode.KEYCODE_UNKNOWN)
			{
				this.mCompletedLoadingThreadTasks = num;
				return;
			}
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_PUFF, true);
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_LAWN_MOWERED_ZOMBIE, true);
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_READYSETPLANT, true);
			this.mCompletedLoadingThreadTasks += 68;
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_FINAL_WAVE, true);
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_SUN, true);
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_TEXT_FADE_ON, true);
			this.mCompletedLoadingThreadTasks += 68;
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIE, true);
			this.mCompletedLoadingThreadTasks += 68;
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIE_NEWSPAPER, true);
			this.mCompletedLoadingThreadTasks += 68;
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_SELECTOR_SCREEN, true);
			this.mCompletedLoadingThreadTasks += 340;
			this.mCompletedLoadingThreadTasks += 68;
			if (this.mPlayerInfo != null)
			{
				for (int i = 0; i < 53; i++)
				{
					SeedType theSeedType = (SeedType)i;
					if (this.HasSeedType(theSeedType) || this.HasFinishedAdventure())
					{
						Plant.PreloadPlantResources(theSeedType);
						if (this.mCompletedLoadingThreadTasks < num)
						{
							this.mCompletedLoadingThreadTasks += 68;
						}
						if (this.mTitleScreen != null && this.mTitleScreen.mQuickLoadKey != KeyCode.KEYCODE_UNKNOWN)
						{
							this.mCompletedLoadingThreadTasks = num;
							return;
						}
						if (this.mShutdown || this.mCloseRequest)
						{
							return;
						}
					}
				}
				int j = 0;
				while (j < 33)
				{
					ZombieType zombieType = (ZombieType)j;
					if (this.HasFinishedAdventure())
					{
						goto IL_175;
					}
					ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(zombieType);
					if (this.mPlayerInfo.mLevel >= zombieDefinition.mStartingLevel)
					{
						goto IL_175;
					}
					IL_1E0:
					j++;
					continue;
					IL_175:
					if (zombieType == ZombieType.ZOMBIE_BOSS || zombieType == ZombieType.ZOMBIE_CATAPULT || zombieType == ZombieType.ZOMBIE_GARGANTUAR || zombieType == ZombieType.ZOMBIE_DIGGER || zombieType == ZombieType.ZOMBIE_ZAMBONI)
					{
						goto IL_1E0;
					}
					Zombie.PreloadZombieResources(zombieType);
					if (this.mCompletedLoadingThreadTasks < num)
					{
						this.mCompletedLoadingThreadTasks += 68;
					}
					if (this.mTitleScreen != null && this.mTitleScreen.mQuickLoadKey != KeyCode.KEYCODE_UNKNOWN)
					{
						this.mCompletedLoadingThreadTasks = num;
						return;
					}
					if (this.mShutdown || this.mCloseRequest)
					{
						return;
					}
					goto IL_1E0;
				}
			}
			if (this.mCompletedLoadingThreadTasks != num)
			{
				this.mCompletedLoadingThreadTasks = num;
			}
		}

		public void PreloadLoadingThreadReanimations()
		{
		}

		public void PreloadReanimation(ReanimationType theReanimType)
		{
		}

		public int GetNumPreloadingTasks()
		{
			int num = 10;
			if (this.mPlayerInfo != null)
			{
				for (int i = 0; i < 53; i++)
				{
					SeedType theSeedType = (SeedType)i;
					if (this.HasSeedType(theSeedType) || this.HasFinishedAdventure())
					{
						num++;
					}
				}
				int j = 0;
				while (j < 33)
				{
					ZombieType zombieType = (ZombieType)j;
					if (this.HasFinishedAdventure())
					{
						goto IL_5B;
					}
					ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(zombieType);
					if (this.mPlayerInfo.mLevel >= zombieDefinition.mStartingLevel)
					{
						goto IL_5B;
					}
					IL_7D:
					j++;
					continue;
					IL_5B:
					if (zombieType != ZombieType.ZOMBIE_BOSS && zombieType != ZombieType.ZOMBIE_CATAPULT && zombieType != ZombieType.ZOMBIE_GARGANTUAR && zombieType != ZombieType.ZOMBIE_DIGGER && zombieType != ZombieType.ZOMBIE_ZAMBONI)
					{
						num++;
						goto IL_7D;
					}
					goto IL_7D;
				}
			}
			return num * 68;
		}

		public void LawnMessageBox(int theDialogId, string theHeaderName, string theLinesName, string theButton1Name, string theButton2Name, int theButtonMode, LawnMessageBoxListener theListener)
		{
			this.mOldFocus = this.mWidgetManager.mFocusWidget;
			this.mLawnMessageBoxListener = theListener;
			LawnDialog lawnDialog = this.DoDialog(theDialogId, true, theHeaderName, theLinesName, theButton1Name, theButtonMode);
			if (lawnDialog.mLawnYesButton != null)
			{
				lawnDialog.mLawnYesButton.mLabel = TodStringFile.TodStringTranslate(theButton1Name);
			}
			if (lawnDialog.mLawnNoButton != null)
			{
				lawnDialog.mLawnNoButton.mLabel = TodStringFile.TodStringTranslate(theButton2Name);
			}
			lawnDialog.CalcSize(0, 0, (int)Constants.InvertAndScale(400f));
			this.mWidgetManager.SetFocus(lawnDialog);
		}

		public virtual void EnforceCursor()
		{
		}

		public void ShowCreditScreen()
		{
			this.mCreditScreen = new CreditScreen(this);
			this.mCreditScreen.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mCreditScreen);
			this.mWidgetManager.BringToBack(this.mCreditScreen);
			this.mWidgetManager.SetFocus(this.mCreditScreen);
		}

		public void KillCreditScreen()
		{
			if (this.mCreditScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mCreditScreen);
				base.SafeDeleteWidget(this.mCreditScreen);
				this.mCreditScreen = null;
			}
		}

		public string Pluralize(int theCount, string theSingular, string thePlural)
		{
			if (theCount == 1)
			{
				return TodCommon.TodReplaceNumberString(theSingular, "{COUNT}", theCount);
			}
			return TodCommon.TodReplaceNumberString(thePlural, "{COUNT}", theCount);
		}

		public int GetNumTrophies(ChallengePage thePage)
		{
			int num = 0;
			for (int i = 1; i < 69; i++)
			{
				ChallengeDefinition challengeDefinition = ChallengeScreen.gChallengeDefs[i - 1];
				if (thePage == challengeDefinition.mPage && this.HasBeatenChallenge(challengeDefinition.mChallengeMode))
				{
					num++;
				}
			}
			return num;
		}

		public bool EarnedGoldTrophy()
		{
			return this.HasFinishedAdventure() && this.TrophiesNeedForGoldSunflower() <= 0;
		}

		public bool IsRegistered()
		{
			return false;
		}

		public bool IsTrialStageLocked()
		{
			return false;
		}

		public bool IsDRMConnected()
		{
			return false;
		}

		public bool IsScaryPotterLevel()
		{
			return (this.mGameMode >= GameMode.GAMEMODE_SCARY_POTTER_1 && this.mGameMode <= GameMode.GAMEMODE_SCARY_POTTER_ENDLESS) || ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 35) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_35);
		}

		public bool IsEndlessScaryPotter(GameMode theGameMode)
		{
			return theGameMode == GameMode.GAMEMODE_SCARY_POTTER_ENDLESS;
		}

		public bool IsSquirrelLevel()
		{
			return this.mBoard != null && this.mGameMode == GameMode.GAMEMODE_CHALLENGE_SQUIRREL;
		}

		public bool IsIZombieLevel()
		{
			return this.mBoard != null && (this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_9 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS);
		}

		public bool CanShowZenGarden()
		{
			return this.mPlayerInfo != null && !this.IsTrialStageLocked() && (this.HasFinishedAdventure() || this.mPlayerInfo.mLevel >= 45);
		}

		public static string GetMoneyString(int theAmount)
		{
			int theValue = theAmount * 10;
			string text;
			if (!LawnApp.moneyStrings.TryGetValue(theAmount, out text))
			{
				text = TodCommon.TodReplaceString(TodStringFile.TodStringTranslate("[CURRENCY_STRING]"), "{CURRENCY_SYMBOL}", TodStringFile.TodStringTranslate("[CURRENCY_SYMBOL]"));
				text = TodCommon.TodReplaceString(text, "{AMOUNT}", Common.CommaSeperate(theValue));
				LawnApp.moneyStrings.Add(theAmount, text);
			}
			return text;
		}

		public static string ToString(int i)
		{
			string text;
			if (!LawnApp.cachedIntToString.TryGetValue(i, out text))
			{
				text = i.ToString();
				LawnApp.cachedIntToString.Add(i, text);
			}
			return text;
		}

		public bool AdvanceCrazyDaveText()
		{
			int num = this.mCrazyDaveMessageIndex + 1;
			string theString = Common.StrFormat_("[CRAZY_DAVE_{0}]", num);
			if (!TodStringFile.TodStringListExists(theString))
			{
				return false;
			}
			this.CrazyDaveTalkIndex(num);
			return true;
		}

		public bool IsWhackAZombieLevel()
		{
			return this.mBoard != null && (this.mGameMode == GameMode.GAMEMODE_CHALLENGE_WHACK_A_ZOMBIE || ((this.IsAdventureMode() && this.mPlayerInfo.mLevel == 15) || this.mGameMode == GameMode.GAMEMODE_QUICKPLAY_15));
		}

		public void UpdatePlayTimeStats()
		{
		}

		public bool CanPauseNow()
		{
			return this.mBoard != null && (this.mSeedChooserScreen == null || !this.mSeedChooserScreen.mMouseVisible) && this.mBoard.mBoardFadeOutCounter < 0 && this.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_OFF && this.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM && base.GetDialogCount() <= 0;
		}

		public bool IsPuzzleMode()
		{
			return (this.mGameMode >= GameMode.GAMEMODE_SCARY_POTTER_1 && this.mGameMode <= GameMode.GAMEMODE_SCARY_POTTER_ENDLESS) || (this.mGameMode >= GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 && this.mGameMode <= GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS);
		}

		public bool IsChallengeMode()
		{
			return !this.IsAdventureMode() && !this.IsQuickPlayMode() && !this.IsPuzzleMode() && !this.IsSurvivalMode();
		}

		public bool IsEndlessIZombie(GameMode theGameMode)
		{
			return theGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS;
		}

		public void CrazyDaveDoneHanding()
		{
			Reanimation reanimation = this.ReanimationGet(this.mCrazyDaveReanimID);
			ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_dave_handinghand);
			GlobalMembersAttachment.AttachmentDie(ref trackInstanceByName.mAttachmentID);
		}

		public int TrophiesNeedForGoldSunflower()
		{
			return 0;
		}

		public int GetCurrentChallengeIndex()
		{
			return this.mGameMode - GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1;
		}

		public void LoadGroup(string theGroupName, int theGroupAveMsToLoad)
		{
			PerfTimer perfTimer = default(PerfTimer);
			perfTimer.Start();
			this.mResourceManager.StartLoadResources(theGroupName);
			while (!this.mShutdown && !this.mCloseRequest && !this.mLoadingFailed && TodCommon.TodLoadNextResource())
			{
				this.mCompletedLoadingThreadTasks += theGroupAveMsToLoad;
			}
			if (this.mShutdown || this.mCloseRequest)
			{
				return;
			}
			if (this.mResourceManager.HadError())
			{
				this.ShowResourceError();
				this.mLoadingFailed = true;
				return;
			}
			if (!Resources.ExtractResourcesByName(this.mResourceManager, theGroupName))
			{
				this.ShowResourceError();
				this.mLoadingFailed = true;
				return;
			}
			int theTotalGroupWeigth = this.mResourceManager.GetNumResources(theGroupName) * theGroupAveMsToLoad;
			int theGroupTime = Math.Max((int)perfTimer.GetDuration(), 0);
			this.TraceLoadGroup(theGroupName, theGroupTime, theTotalGroupWeigth, theGroupAveMsToLoad);
		}

		public void TraceLoadGroup(string theGroupName, int theGroupTime, int theTotalGroupWeigth, int theTaskWeight)
		{
		}

		public void DelayLoadBackgroundResource(string theGroupName)
		{
			if (this.mLastBackgroundResGroupLoaded != theGroupName)
			{
				if (!string.IsNullOrEmpty(this.mLastBackgroundResGroupLoaded))
				{
					this.mResourceManager.UnloadBackground(this.mLastBackgroundResGroupLoaded);
				}
				if (!string.IsNullOrEmpty(theGroupName))
				{
					TodCommon.TodLoadResources(theGroupName);
				}
				this.mLastBackgroundResGroupLoaded = theGroupName;
			}
		}

		private static void LoadResourceInThread(string resourceGroup)
		{
			if (Main.LOW_MEMORY_DEVICE)
			{
				Thread thread = new Thread(new ParameterizedThreadStart(LawnApp.Load));
				LoadingScreen.IsLoading = true;
				thread.IsBackground = true;
				thread.Start(resourceGroup);
				return;
			}
			LawnApp.Load(resourceGroup);
		}

		private static void Load(object resourceGroup)
		{
			string theGroup = resourceGroup as string;
			TodCommon.TodLoadResources(theGroup);
		}

		public void DelayLoadLeaderboardResource(bool doLoad)
		{
			if (!Main.LOW_MEMORY_DEVICE && this.leaderboardLoaded)
			{
				return;
			}
			if (doLoad)
			{
				this.DelayLoadMainMenuResource(false);
				this.DelayLoadGamePlayResources(false);
			}
			if (doLoad && !this.leaderboardLoaded)
			{
				LawnApp.LoadResourceInThread("DelayLoad_Leaderboard");
			}
			else if (this.leaderboardLoaded && !doLoad)
			{
				this.mResourceManager.UnloadBackground("DelayLoad_Leaderboard");
			}
			this.DelayLoadPileResource(doLoad);
			if (!doLoad)
			{
				this.DelayLoadMainMenuResource(true);
			}
			this.leaderboardLoaded = doLoad;
		}

		public void DelayLoadGamePlayResources(bool doLoad)
		{
			if (!Main.LOW_MEMORY_DEVICE && this.gamePlayLoaded)
			{
				return;
			}
			if (doLoad && !this.gamePlayLoaded)
			{
				GC.Collect();
				TodCommon.TodLoadResources("DelayLoad_GamePlay");
				AtlasResources.mAtlasResources.UnpackPlantsZombiesAtlasImages();
				AtlasResources.mAtlasResources.UnpackParticlesAtlasImages();
				if (Main.LOW_MEMORY_DEVICE)
				{
					ResourceManager.mReanimContentManager.Unload();
					ReanimatorXnaHelpers.ReanimatorLoadDefinitions(ref GameConstants.gLawnReanimationArray, 119);
					ResourceManager.mParticleContentManager.Unload();
					TodParticleGlobal.TodParticleLoadDefinitions(ref GameConstants.gLawnParticleArray, 102);
				}
			}
			else if (this.gamePlayLoaded && !doLoad)
			{
				this.mResourceManager.UnloadBackground("DelayLoad_GamePlay");
			}
			this.gamePlayLoaded = doLoad;
		}

		private void DelayLoadCachedResources(bool doLoad)
		{
			if (!Main.LOW_MEMORY_DEVICE && this.cachedLoaded)
			{
				return;
			}
			if (doLoad && !this.cachedLoaded)
			{
				GC.Collect();
				TodCommon.TodLoadResources("DelayLoad_Cached");
				AtlasResources.mAtlasResources.UnpackCachedAtlasImages();
			}
			else if (this.cachedLoaded && !doLoad)
			{
				this.mResourceManager.UnloadBackground("DelayLoad_Cached");
			}
			this.cachedLoaded = doLoad;
		}

		public void DelayLoadZenGardenResources(bool doLoad)
		{
			if (!Main.LOW_MEMORY_DEVICE && this.zenGardenLoaded)
			{
				return;
			}
			if (doLoad && !this.zenGardenLoaded)
			{
				GC.Collect();
				TodCommon.TodLoadResources("DelayLoad_ZenGarden");
				AtlasResources.mAtlasResources.UnpackZengardenAtlasImages();
				if (Main.LOW_MEMORY_DEVICE)
				{
					ResourceManager.mReanimContentManager.Unload();
					ReanimatorXnaHelpers.ReanimatorLoadDefinitions(ref GameConstants.gLawnReanimationArray, 119);
					ResourceManager.mParticleContentManager.Unload();
					TodParticleGlobal.TodParticleLoadDefinitions(ref GameConstants.gLawnParticleArray, 102);
				}
			}
			else if (this.zenGardenLoaded && !doLoad)
			{
				this.mResourceManager.UnloadBackground("DelayLoad_ZenGarden");
			}
			this.zenGardenLoaded = doLoad;
		}

		public void DelayLoadMainMenuResource(bool doLoad)
		{
			if (!Main.LOW_MEMORY_DEVICE && this.mainMenuLoaded)
			{
				return;
			}
			if (doLoad && !this.mainMenuLoaded)
			{
				TodCommon.TodLoadResources("DelayLoad_MainMenu");
				AtlasResources.mAtlasResources.UnpackGoodiesAtlasImages();
				AtlasResources.mAtlasResources.UnpackQuickplayAtlasImages();
				AtlasResources.mAtlasResources.UnpackMiniGamesAtlasImages();
			}
			else if (this.mainMenuLoaded && !doLoad)
			{
				this.mResourceManager.UnloadBackground("DelayLoad_MainMenu");
			}
			this.mainMenuLoaded = doLoad;
		}

		public void DelayLoadPileResource(bool doLoad)
		{
			if (!Main.LOW_MEMORY_DEVICE && this.pileLoaded)
			{
				return;
			}
			if (doLoad && !this.pileLoaded)
			{
				TodCommon.TodLoadResources("DelayLoad_Pile");
				AtlasResources.mAtlasResources.UnpackPileAtlasImages();
			}
			else if (this.pileLoaded && !doLoad)
			{
				this.mResourceManager.UnloadBackground("DelayLoad_Pile");
			}
			this.pileLoaded = doLoad;
		}

		public void DelayLoadZombieNoteResource(string theGroupName)
		{
			if (this.mLastZombieNoteResGroupLoaded != theGroupName)
			{
				if (!string.IsNullOrEmpty(this.mLastZombieNoteResGroupLoaded))
				{
					this.mResourceManager.UnloadBackground(this.mLastZombieNoteResGroupLoaded);
				}
				TodCommon.TodLoadResources(theGroupName);
				this.mLastZombieNoteResGroupLoaded = theGroupName;
				Resources.ExtractResourcesByName(this.mResourceManager, theGroupName);
			}
		}

		public void DelayLoadZenGardenBackground(string theGroupName)
		{
			if (this.mLastZenGardenResourceLoaded != theGroupName)
			{
				if (!string.IsNullOrEmpty(this.mLastZenGardenResourceLoaded))
				{
					this.mResourceManager.UnloadBackground(this.mLastZenGardenResourceLoaded);
				}
				TodCommon.TodLoadResources(theGroupName);
				this.mLastZenGardenResourceLoaded = theGroupName;
				Resources.ExtractResourcesByName(this.mResourceManager, theGroupName);
			}
		}

		public void DelayLoadZombieNotePaperResource(string theGroupName)
		{
			if (this.mLastPaperGroupLoaded != theGroupName)
			{
				if (!string.IsNullOrEmpty(this.mLastPaperGroupLoaded))
				{
					this.mResourceManager.UnloadBackground(this.mLastPaperGroupLoaded);
				}
				TodCommon.TodLoadResources(theGroupName);
				this.mLastPaperGroupLoaded = theGroupName;
				Resources.ExtractResourcesByName(this.mResourceManager, theGroupName);
			}
		}

		public void DelayLoadUpsellResource(string theGroupName)
		{
			if (this.mLastStoreResGroupLoaded != theGroupName)
			{
				if (!string.IsNullOrEmpty(this.mLastUpsellResGroupLoaded))
				{
					this.mResourceManager.UnloadBackground(this.mLastUpsellResGroupLoaded);
				}
				TodCommon.TodLoadResources(theGroupName);
				this.mLastUpsellResGroupLoaded = theGroupName;
				Resources.ExtractResourcesByName(this.mResourceManager, theGroupName);
			}
		}

		public void DelayLoadStoreResource(string theGroupName)
		{
			if (this.mLastStoreResGroupLoaded != theGroupName)
			{
				if (!string.IsNullOrEmpty(this.mLastStoreResGroupLoaded))
				{
					this.mResourceManager.UnloadBackground(this.mLastStoreResGroupLoaded);
				}
				TodCommon.TodLoadResources(theGroupName);
				this.mLastStoreResGroupLoaded = theGroupName;
			}
		}

		public void CrazyDaveStopSound()
		{
			this.mSoundSystem.StopFoley(FoleyType.FOLEY_CRAZYDAVESHORT);
			this.mSoundSystem.StopFoley(FoleyType.FOLEY_CRAZYDAVELONG);
			this.mSoundSystem.StopFoley(FoleyType.FOLEY_CRAZYDAVEEXTRALONG);
			this.mSoundSystem.StopFoley(FoleyType.FOLEY_CRAZYDAVECRAZY);
		}

		public bool UpdatePlayerProfileForFinishingLevel()
		{
			bool flag = false;
			if (this.IsAdventureMode())
			{
				int mLevel = this.mBoard.mLevel;
				if (mLevel == 50)
				{
					if (this.mPlayerInfo.mIZombieUnlocked == 3 && this.HasBeatenChallenge(GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3))
					{
						this.mPlayerInfo.mIZombieUnlocked++;
					}
					if (this.mPlayerInfo.mVasebreakerUnlocked == 3 && this.HasBeatenChallenge(GameMode.GAMEMODE_SCARY_POTTER_3))
					{
						this.mPlayerInfo.mVasebreakerUnlocked++;
					}
					this.mPlayerInfo.SetLevel(1);
					this.mPlayerInfo.mFinishedAdventure++;
					if (this.mPlayerInfo.mFinishedAdventure == 1)
					{
						this.mPlayerInfo.mNeedsMessageOnGameSelector = true;
						this.mPlayerInfo.mMiniGamesUnlockable = 19;
						int num = 0;
						if (this.HasBeatenChallenge(GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS))
						{
							num++;
						}
						if (this.HasBeatenChallenge(GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING))
						{
							num++;
						}
						if (this.HasBeatenChallenge(GameMode.GAMEMODE_CHALLENGE_SLOT_MACHINE))
						{
							num++;
						}
						this.mPlayerInfo.mMiniGamesUnlocked += num;
					}
				}
				else
				{
					this.mPlayerInfo.SetLevel(mLevel + 1);
				}
				if (!this.HasFinishedAdventure() && mLevel == 34)
				{
					this.mPlayerInfo.mNeedsMagicTacoReward = true;
				}
				if (!this.HasFinishedAdventure() && mLevel == 44)
				{
					this.mPlayerInfo.mNeedsMagicBaconReward = true;
				}
				if ((mLevel >= 22 || this.mPlayerInfo.mFinishedAdventure > 0) && !this.mPlayerInfo.mHasUnlockedMinigames)
				{
					this.mPlayerInfo.UnlockFirstMiniGames();
				}
				if ((mLevel >= 36 || this.mPlayerInfo.mFinishedAdventure > 0) && !this.mPlayerInfo.mHasUnlockedPuzzleMode)
				{
					this.mPlayerInfo.UnlockPuzzleMode();
				}
			}
			else if (!this.IsQuickPlayMode())
			{
				if (this.IsSurvivalMode())
				{
					if (this.mBoard.IsFinalSurvivalStage())
					{
						flag = !this.HasBeatenChallenge(this.mGameMode);
						this.mBoard.SurvivalSaveScore();
						if (flag && this.HasFinishedAdventure())
						{
							int numTrophies = this.GetNumTrophies(ChallengePage.CHALLENGE_PAGE_SURVIVAL);
							if (numTrophies != 8 && numTrophies != 9)
							{
								this.mPlayerInfo.mHasNewSurvival = true;
							}
						}
					}
				}
				else if (this.IsPuzzleMode())
				{
					flag = !this.HasBeatenChallenge(this.mGameMode);
					int currentChallengeIndex = this.GetCurrentChallengeIndex();
					this.mPlayerInfo.mChallengeRecords[currentChallengeIndex]++;
					if (!this.HasFinishedAdventure() && (this.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_3 || this.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3))
					{
						flag = false;
					}
					if (flag)
					{
						if (this.IsScaryPotterLevel())
						{
							this.mPlayerInfo.mHasNewVasebreaker = true;
							this.mPlayerInfo.mVasebreakerUnlocked++;
							if (this.mPlayerInfo.mFinishedAdventure == 0 && this.mPlayerInfo.mVasebreakerUnlocked > 3)
							{
								this.mPlayerInfo.mVasebreakerUnlocked = 3;
							}
							if (this.mPlayerInfo.mVasebreakerUnlocked > 10)
							{
								this.mPlayerInfo.mVasebreakerUnlocked = 10;
							}
						}
						else
						{
							this.mPlayerInfo.mHasNewIZombie = true;
							this.mPlayerInfo.mIZombieUnlocked++;
							if (this.mPlayerInfo.mFinishedAdventure == 0 && this.mPlayerInfo.mIZombieUnlocked > 3)
							{
								this.mPlayerInfo.mIZombieUnlocked = 3;
							}
							if (this.mPlayerInfo.mIZombieUnlocked > 10)
							{
								this.mPlayerInfo.mIZombieUnlocked = 10;
							}
						}
					}
				}
				else
				{
					flag = !this.HasBeatenChallenge(this.mGameMode);
					int currentChallengeIndex2 = this.GetCurrentChallengeIndex();
					this.mPlayerInfo.mChallengeRecords[currentChallengeIndex2]++;
					if (this.mPlayerInfo.mMiniGamesUnlocked < this.mPlayerInfo.mMiniGamesUnlockable)
					{
						this.mPlayerInfo.mMiniGamesUnlocked++;
					}
					if (flag && this.HasFinishedAdventure())
					{
						int numTrophies2 = this.GetNumTrophies(ChallengePage.CHALLENGE_PAGE_CHALLENGE);
						if (numTrophies2 <= 17)
						{
							this.mPlayerInfo.mHasNewMiniGame = true;
						}
					}
				}
			}
			int numTrophies3 = this.GetNumTrophies(ChallengePage.CHALLENGE_PAGE_CHALLENGE);
			if (numTrophies3 == 19)
			{
				ReportAchievement.GiveAchievement(AchievementId.BeyondTheGrave);
			}
			this.WriteCurrentUserConfig();
			return flag;
		}

		public bool CanDoDaisyMode()
		{
			return false;
		}

		public bool CanDoPinataMode()
		{
			return false;
		}

		public bool CanDoDanceMode()
		{
			return false;
		}

		public override void SetSfxVolume(double theVolume)
		{
			base.SetSfxVolume(theVolume);
			this.mPlayerInfo.mSoundVolume = theVolume;
		}

		public override void SetMusicVolume(double theVolume)
		{
			base.SetMusicVolume(theVolume);
			if (this.mPlayerInfo != null)
			{
				this.mPlayerInfo.mMusicVolume = theVolume;
			}
		}

		public bool SaveFileExists()
		{
			string savedGameName = LawnCommon.GetSavedGameName(GameMode.GAMEMODE_ADVENTURE, (int)this.mPlayerInfo.mId);
			return base.FileExists(savedGameName);
		}

		public void Vibrate()
		{
			if (this.mPlayerInfo == null || !this.mPlayerInfo.mDoVibration)
			{
				return;
			}
			base.DoVibration();
		}

		public override void MoviePlayerContentPreloadDidFinish(bool succeeded)
		{
			if (this.mCreditScreen != null)
			{
				this.mCreditScreen.VideoLoaded(succeeded);
			}
		}

		public override void MoviePlayerPlaybackDidFinish()
		{
			if (this.mCreditScreen != null)
			{
				this.mCreditScreen.VideoFinished();
			}
		}

		public int GetAchievementIcon(AchievementId theAchievement)
		{
			return GameConstants.AchievementInfo[(int)theAchievement].mImageId;
		}

		public string GetAchievementName(AchievementId theAchievement)
		{
			return TodStringFile.TodStringTranslate(GameConstants.AchievementInfo[(int)theAchievement].mName);
		}

		public string GetAchievementDescription(AchievementId theAchievement)
		{
			return TodStringFile.TodStringTranslate(GameConstants.AchievementInfo[(int)theAchievement].mDesc);
		}

		public override bool ShouldAutorotateToInterfaceOrientation(UI_ORIENTATION theOrientation)
		{
			return base.ShouldAutorotateToInterfaceOrientation(theOrientation) && (theOrientation == UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_LEFT || theOrientation == UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_RIGHT);
		}

		private const string PLACEHOLDER_PLAYER = "{PLAYER_NAME}";

		private const string PLACEHOLDER_MONEY = "{MONEY}";

		private const string PLACEHOLDER_UPGRADECOST = "{UPGRADE_COST}";

		private const string PLACEHOLDER_CRAZYDAVE_0 = "[CRAZY_DAVE_{0}]";

		public static string AppVersionNumber = "1.4";

		public Board mBoard;

		public TitleScreen mTitleScreen;

		public GameSelector mGameSelector;

		public SeedChooserScreen mSeedChooserScreen;

		public AwardScreen mAwardScreen;

		public CreditScreen mCreditScreen;

		public TodFoley mSoundSystem;

		public LinkedList<ButtonWidget> mControlButtonList = new LinkedList<ButtonWidget>();

		public LinkedList<Image> mCreatedImageList = new LinkedList<Image>();

		public string mReferId;

		public string mRegisterLink;

		public string mMod;

		public bool mRegisterResourcesLoaded;

		public bool mTodCheatKeys;

		public GameMode mGameMode;

		public GameScenes mGameScene;

		public bool mLoadingZombiesThreadCompleted;

		public bool mFirstTimeGameSelector;

		public int mGamesPlayed;

		public int mMaxExecutions;

		public int mMaxPlays;

		public int mMaxTime;

		public bool mEasyPlantingCheat;

		public ZenGarden mZenGarden;

		public EffectSystem mEffectSystem;

		public ReanimatorCache mReanimatorCache;

		public ProfileMgr mProfileMgr;

		private PlayerInfo _playerInfo;

		public LevelStats mLastLevelStats;

		public bool mCloseRequest;

		public int mAppCounter;

		public Music mMusic;

		public Reanimation mCrazyDaveReanimID;

		public CrazyDaveState mCrazyDaveState;

		public int mCrazyDaveBlinkCounter;

		public Reanimation mCrazyDaveBlinkReanimID;

		public int mCrazyDaveMessageIndex;

		public string mCrazyDaveMessageText = string.Empty;

		public int mAppRandSeed;

		public int mSessionID;

		public int mPlayTimeActiveSession;

		public int mPlayTimeInactiveSession;

		public BoardResult mBoardResult;

		public bool mKilledYetiAndRestarted;

		public TypingCheck mKonamiCheck;

		public TypingCheck mMustacheCheck;

		public TypingCheck mMoustacheCheck;

		public TypingCheck mSuperMowerCheck;

		public TypingCheck mSuperMowerCheck2;

		public TypingCheck mFutureCheck;

		public TypingCheck mPinataCheck;

		public TypingCheck mDanceCheck;

		public TypingCheck mDaisyCheck;

		public TypingCheck mSukhbirCheck;

		public bool mMustacheMode;

		public bool mSuperMowerMode;

		public bool mFutureMode;

		public bool mPinataMode;

		public bool mDanceMode;

		public bool mDaisyMode;

		public bool mSukhbirMode;

		public TrialType mTrialType;

		public bool mDebugTrialLocked;

		public bool mMuteSoundsForCutscene;

		private string mLastBackgroundResGroupLoaded;

		private string mLastZombieNoteResGroupLoaded;

		private string mLastStoreResGroupLoaded;

		private string mLastPaperGroupLoaded;

		private string mLastZenGardenResourceLoaded;

		private string mLastUpsellResGroupLoaded;

		public GameMode mRestoreGameMode;

		public RestoreLocation mRestoreLocation;

		public bool checkGiveAchievements;

		public AchievementId achievementToCheck = AchievementId.MAX_ACHIEVEMENTS;

		private Dictionary<int, string> cachedStageStrings = new Dictionary<int, string>();

		public ChallengeScreen mChallengeScreen;

		public LeaderboardScreen mLeaderboardScreen;

		private static Dictionary<string, string> noClickStringCache = new Dictionary<string, string>(10);

		public LawnMessageBoxListener mLawnMessageBoxListener;

		public Widget mOldFocus;

		private static Dictionary<int, string> moneyStrings = new Dictionary<int, string>();

		private static Dictionary<int, string> cachedIntToString = new Dictionary<int, string>();

		private bool leaderboardLoaded;

		private bool gamePlayLoaded;

		private bool cachedLoaded;

		private bool zenGardenLoaded;

		private bool mainMenuLoaded;

		private bool pileLoaded;

		private class TableTmp
		{
			public TableTmp(int aNormal, int aAdditive)
			{
				this.mNormalImageId = aNormal;
				this.mAdditiveImageId = aAdditive;
			}

			public int mNormalImageId;

			public int mAdditiveImageId;
		}
	}
}
