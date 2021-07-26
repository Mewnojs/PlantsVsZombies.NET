using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class Board : Widget, ButtonListener
	{
		public bool LoadFromFile(Sexy.Buffer b)
		{
			this.doAddGraveStones = false;
			this.mLevel = b.ReadLong();
			if (this.mApp.mGameMode == GameMode.GAMEMODE_ADVENTURE && this.mLevel != this.mApp.mPlayerInfo.mLevel)
			{
				throw new Exception("Board Level does not match player level.");
			}
			this.mApp.mGameScene = (GameScenes)b.ReadLong();
			Board.mPeashootersPlanted = b.ReadLong();
			this.mNomNomNomAchievementTracker = b.ReadBoolean();
			this.mNoFungusAmongUsAchievementTracker = b.ReadBoolean();
			this.mBackground = (BackgroundType)b.ReadLong();
			this.mBoardFadeOutCounter = b.ReadLong();
			this.mBoardRandSeed = b.ReadLong();
			this.mBonusLawnMowersRemaining = b.ReadLong();
			this.mCatapultPlantsUsed = b.ReadBoolean();
			this.mChallenge.LoadFromFile(b);
			this.mChocolateCollected = b.ReadLong();
			this.mCobCannonCursorDelayCounter = b.ReadLong();
			this.mCobCannonMouseX = b.ReadLong();
			this.mCobCannonMouseY = b.ReadLong();
			this.mCoinBankFadeCount = b.ReadLong();
			int num = b.ReadLong();
			this.mCoins.Clear();
			for (int i = 0; i < num; i++)
			{
				Coin coin = new Coin();
				coin.LoadFromFile(b);
				this.mCoins.Add(coin);
			}
			this.mCoinsCollected = b.ReadLong();
			this.mCollectedCoinStreak = b.ReadLong();
			this.mCurrentWave = b.ReadLong();
			this.mCursorObject.LoadFromFile(b);
			this.mDaisyMode = b.ReadBoolean();
			this.mDanceMode = b.ReadBoolean();
			this.mDiamondsCollected = b.ReadLong();
			this.mDoomsUsed = b.ReadLong();
			this.mDroppedFirstCoin = b.ReadBoolean();
			this.mEffectCounter = b.ReadLong();
			this.mEnableGraveStones = b.ReadBoolean();
			this.mFinalBossKilled = b.ReadBoolean();
			this.mFinalWaveSoundCounter = b.ReadLong();
			this.mFlagRaiseCounter = b.ReadLong();
			this.mFogBlownCountDown = b.ReadLong();
			this.mFogOffset = b.ReadFloat();
			this.mFwooshCountDown = b.ReadLong();
			this.mGameID = b.ReadLong();
			this.mGargantuarsKillsByCornCob = b.ReadLong();
			this.mGravesCleared = b.ReadLong();
			this.mGridCelFog = b.ReadLong2DArray();
			this.mGridCelLook = b.ReadLong2DArray();
			this.mGridCelOffset = b.ReadLong3DArray();
			int num2 = b.ReadLong();
			this.mGridItems.Clear();
			for (int j = 0; j < num2; j++)
			{
				GridItem newGridItem = GridItem.GetNewGridItem();
				newGridItem.LoadFromFile(b);
				this.mGridItems.Add(newGridItem);
			}
			int num3 = b.ReadLong();
			int num4 = b.ReadLong();
			for (int k = 0; k < num3; k++)
			{
				for (int l = 0; l < num4; l++)
				{
					this.mGridSquareType[k, l] = (GridSquareType)b.ReadLong();
				}
			}
			this.mHelpDisplayed = b.ReadBooleanArray();
			this.mHelpIndex = (AdviceType)b.ReadLong();
			this.mHugeWaveCountDown = b.ReadLong();
			this.mIceMinX = b.ReadLongArray();
			this.mIceTimer = b.ReadLongArray();
			this.mIceTrapCounter = b.ReadLong();
			this.mIgnoreMouseUp = b.ReadBoolean();
			this.mKilledYeti = b.ReadBoolean();
			this.mLastBungeeWave = b.ReadLong();
			this.mLastToolX = b.ReadLong();
			this.mLastToolY = b.ReadLong();
			this.mLastWMUpdateCount = b.ReadLong();
			int num5 = b.ReadLong();
			this.mLawnMowers.Clear();
			for (int m = 0; m < num5; m++)
			{
				LawnMower newLawnMower = LawnMower.GetNewLawnMower();
				newLawnMower.LoadFromFile(b);
				this.mLawnMowers.Add(newLawnMower);
			}
			this.mLevelAwardSpawned = b.ReadBoolean();
			this.mLevelComplete = b.ReadBoolean();
			this.mLevelFadeCount = b.ReadLong();
			this.mLevelStr = b.ReadString();
			this.mMainCounter = b.ReadLong();
			this.mMaxSunPlants = b.ReadLong();
			this.mMinFPS = b.ReadFloat();
			this.mMushroomAndCoffeeBeansOnly = b.ReadBoolean();
			this.mMushroomsUsed = b.ReadBoolean();
			this.mNextSurvivalStageCounter = b.ReadLong();
			this.mNumSunsFallen = b.ReadLong();
			this.mNumWaves = b.ReadLong();
			this.mNutsUsed = b.ReadBoolean();
			this.mOutOfMoneyCounter = b.ReadLong();
			this.mPeaShooterUsed = b.ReadBoolean();
			this.mPinataMode = b.ReadBoolean();
			this.mPlanternOrBloverUsed = b.ReadBoolean();
			this.PickBackground();
			int num6 = b.ReadLong();
			this.mPlants.Clear();
			for (int n = 0; n < num6; n++)
			{
				Plant newPlant = Plant.GetNewPlant();
				newPlant.LoadFromFile(b);
				if (this.mApp.IsIZombieLevel())
				{
					this.mChallenge.IZombieSetupPlant(newPlant);
				}
				this.mPlants.Add(newPlant);
			}
			this.mPlantsEaten = b.ReadLong();
			this.mPlantsShoveled = b.ReadLong();
			this.mPlayTimeActiveLevel = b.ReadLong();
			this.mPlayTimeInactiveLevel = b.ReadLong();
			this.mPottedPlantsCollected = b.ReadLong();
			this.mProgressMeterWidth = b.ReadLong();
			int num7 = b.ReadLong();
			this.mProjectiles.Clear();
			for (int num8 = 0; num8 < num7; num8++)
			{
				Projectile newProjectile = Projectile.GetNewProjectile();
				newProjectile.LoadFromFile(b);
				this.mProjectiles.Add(newProjectile);
			}
			this.mRiseFromGraveCounter = b.ReadLong();
			int num9 = b.ReadLong();
			for (int num10 = 0; num10 < num9; num10++)
			{
				if (this.mRowPickingArray[num10] == null)
				{
					this.mRowPickingArray[num10] = new TodSmoothArray();
				}
				this.mRowPickingArray[num10].LoadFromFile(b);
			}
			this.mScoreNextMowerCounter = b.ReadLong();
			this.mShowShovel = b.ReadBoolean();
			this.mSeedBank = new SeedBank();
			this.mSeedBank.LoadFromFile(b);
			this.mSodPosition = b.ReadLong();
			this.mSpecialGraveStoneX = b.ReadLong();
			this.mSpecialGraveStoneY = b.ReadLong();
			this.mSunCountDown = b.ReadLong();
			this.mSunMoney = b.ReadLong();
			this.mSuperMowerMode = b.ReadBoolean();
			this.mTimeStopCounter = b.ReadLong();
			this.mTotalSpawnedWaves = b.ReadLong();
			this.mTriggeredLawnMowers = b.ReadLong();
			this.mTutorialState = (TutorialState)b.ReadLong();
			this.mTutorialTimer = b.ReadLong();
			this.mWaveRowGotLawnMowered = b.ReadLongArray();
			this.mZombieAllowed = b.ReadBooleanArray();
			this.mZombieCountDown = b.ReadLong();
			this.mZombieCountDownStart = b.ReadLong();
			this.mZombieHealthToNextWave = b.ReadLong();
			this.mZombieHealthWaveStart = b.ReadLong();
			int num11 = b.ReadLong();
			this.mZombies.Clear();
			this.mZombiesRow1.Clear();
			this.mZombiesRow2.Clear();
			this.mZombiesRow3.Clear();
			this.mZombiesRow4.Clear();
			this.mZombiesRow5.Clear();
			this.mZombiesRow6.Clear();
			for (int num12 = 0; num12 < num11; num12++)
			{
				Zombie newZombie = Zombie.GetNewZombie();
				newZombie.LoadFromFile(b);
				this.AddToZombieList(newZombie);
			}
			num3 = b.ReadLong();
			num4 = b.ReadLong();
			for (int num13 = 0; num13 < num3; num13++)
			{
				for (int num14 = 0; num14 < num4; num14++)
				{
					this.mZombiesInWave[num13, num14] = (ZombieType)b.ReadLong();
				}
			}
			if (b.ReadLong() != 777)
			{
				throw new Exception("Check number mismatch while loading.");
			}
			foreach (Zombie zombie in this.mZombies)
			{
				zombie.LoadingComplete();
			}
			foreach (Plant plant in this.mPlants)
			{
				plant.LoadingComplete();
			}
			foreach (Projectile projectile in this.mProjectiles)
			{
				projectile.LoadingComplete();
			}
			foreach (LawnMower lawnMower in this.mLawnMowers)
			{
				lawnMower.LoadingComplete();
			}
			foreach (Coin coin2 in this.mCoins)
			{
				coin2.LoadingComplete();
			}
			foreach (GridItem gridItem in this.mGridItems)
			{
				gridItem.LoadingComplete();
			}
			this.doAddGraveStones = true;
			this.mApp.mSoundManager.StopAllSounds();
			return true;
		}

		static Board()
		{
			for (int i = 0; i < Board.aZombieWeightArray.Length; i++)
			{
				Board.aZombieWeightArray[i] = TodWeightedArray.GetNewTodWeightedArray();
			}
			for (int j = 0; j < Board.aPickArray.Length; j++)
			{
				Board.aPickArray[j] = TodWeightedArray.GetNewTodWeightedArray();
			}
			for (int k = 0; k < Board.aGridArray.Length; k++)
			{
				Board.aGridArray[k] = TodWeightedGridArray.GetNewTodWeightedGridArray();
			}
		}

		public Board() : this(GlobalStaticVars.gLawnApp)
		{
			this.SetFullRect();
		}

		private void SetFullRect()
		{
			this.FullRect = new TRect(-Constants.Board_Offset_AspectRatio_Correction, this.mY, this.mWidth, this.mHeight);
		}

		public override void Move(int theNewX, int theNewY)
		{
			base.Move(theNewX, theNewY);
			this.SetFullRect();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.SetFullRect();
		}

		public override void Resize(TRect theRect)
		{
			base.Resize(theRect);
			this.SetFullRect();
		}

		protected override void Reset()
		{
			base.Reset();
			this.mX = Constants.Board_Offset_AspectRatio_Correction;
			this.SetFullRect();
		}

		public Board(LawnApp theApp)
		{
			this.SetupRenderItems();
			this.mApp = theApp;
			this.mApp.mBoard = this;
			this.mZombies = new List<Zombie>(512);
			this.mPlants = new List<Plant>(512);
			this.mProjectiles = new List<Projectile>(512);
			this.mCoins = new List<Coin>(512);
			this.mLawnMowers = new List<LawnMower>(32);
			this.mGridItems = new List<GridItem>(128);
			this.mApp.mEffectSystem.EffectSystemFreeAll();
			this.mBoardRandSeed = this.mApp.mAppRandSeed;
			if (this.mApp.IsSurvivalMode())
			{
				this.mBoardRandSeed = RandomNumbers.NextNumber();
			}
			this.mCoinBankFadeCount = 0;
			this.mLevelFadeCount = 0;
			this.mLevel = 0;
			this.mCursorObject = new CursorObject();
			this.mCursorPreview = new CursorPreview();
			this.mSeedBank = new SeedBank();
			this.mCutScene = new CutScene();
			this.mSpecialGraveStoneX = -1;
			this.mSpecialGraveStoneY = -1;
			for (int i = 0; i < Constants.GRIDSIZEX; i++)
			{
				for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
				{
					this.mGridSquareType[i, j] = GridSquareType.GRIDSQUARE_GRASS;
					this.mGridCelLook[i, j] = RandomNumbers.NextNumber(20);
					this.mGridCelOffset[i, j, 0] = RandomNumbers.NextNumber(10) - 5;
					this.mGridCelOffset[i, j, 1] = RandomNumbers.NextNumber(10) - 5;
				}
				for (int k = 0; k < 7; k++)
				{
					this.mGridCelFog[i, k] = 0;
				}
			}
			this.mSunCountDown = 0;
			this.mShakeCounter = 0;
			this.mShakeAmountX = 0;
			this.mShakeAmountY = 0;
			this.mPaused = false;
			this.mLevelAwardSpawned = false;
			this.mFlagRaiseCounter = 0;
			this.mIceTrapCounter = 0;
			this.mLevelComplete = false;
			this.mBoardFadeOutCounter = -1;
			this.mNextSurvivalStageCounter = 0;
			this.mScoreNextMowerCounter = 0;
			this.mProgressMeterWidth = 0;
			this.mPoolSparklyParticleID = null;
			this.mFogBlownCountDown = 0;
			this.mFogOffset = 0f;
			this.mFwooshCountDown = 0;
			this.mTimeStopCounter = 0;
			this.mCobCannonCursorDelayCounter = 0;
			this.mCobCannonMouseX = 0;
			this.mCobCannonMouseY = 0;
			this.mDroppedFirstCoin = false;
			this.mBonusLawnMowersRemaining = 0;
			this.mEnableGraveStones = false;
			this.mHelpIndex = AdviceType.ADVICE_NONE;
			this.mEffectCounter = 0;
			this.mDrawCount = 0;
			this.mRiseFromGraveCounter = 0;
			this.mFinalWaveSoundCounter = 0;
			this.mKilledYeti = false;
			this.mTriggeredLawnMowers = 0;
			this.mPlayTimeActiveLevel = 0;
			this.mPlayTimeInactiveLevel = 0;
			this.mMaxSunPlants = 0;
			this.mStartDrawTime = 0;
			this.mIntervalDrawTime = 0;
			this.mIntervalDrawCountStart = 0;
			this.mPreloadTime = 0;
			this.mGameID = DateTime.Now.Millisecond;
			this.mGravesCleared = 0;
			this.mPlantsEaten = 0;
			this.mPlantsShoveled = 0;
			this.mCoinsCollected = 0;
			this.mDiamondsCollected = 0;
			this.mPottedPlantsCollected = 0;
			this.mChocolateCollected = 0;
			this.mCollectedCoinStreak = 0;
			this.mGargantuarsKillsByCornCob = 0;
			this.mMushroomsUsed = false;
			this.mDoomsUsed = 0;
			this.mPlanternOrBloverUsed = false;
			this.mNutsUsed = false;
			this.mLastToolX = (this.mLastToolY = -1);
			this.mMinFPS = 1000f;
			for (int l = 0; l < Constants.MAX_GRIDSIZEY; l++)
			{
				for (int m = 0; m < 12; m++)
				{
					this.mFwooshID[l, m] = null;
				}
			}
			this.mPrevMouseX = -1;
			this.mPrevMouseY = -1;
			this.mFinalBossKilled = false;
			this.mMustacheMode = this.mApp.mMustacheMode;
			this.mSuperMowerMode = this.mApp.mSuperMowerMode;
			this.mFutureMode = this.mApp.mFutureMode;
			this.mPinataMode = this.mApp.mPinataMode;
			this.mDanceMode = this.mApp.mDanceMode;
			this.mDaisyMode = this.mApp.mDaisyMode;
			this.mSukhbirMode = this.mApp.mSukhbirMode;
			this.mShowShovel = false;
			this.mAdvice = new MessageWidget(this.mApp);
			this.mBackground = BackgroundType.BACKGROUND_1_DAY;
			this.mMainCounter = 0;
			this.mTutorialState = TutorialState.TUTORIAL_OFF;
			this.mTutorialTimer = -1;
			this.mTutorialParticleID = null;
			this.mChallenge = new Challenge();
			this.mClip = false;
			this.mDebugTextMode = DebugTextMode.DEBUG_TEXT_NONE;
			this.mMenuButton = new GameButton(0, this);
			this.mMenuButton.mDrawStoneButton = true;
			this.mStoreButton = null;
			this.mIgnoreMouseUp = false;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				this.mMenuButton.SetLabel("[MAIN_MENU_BUTTON]");
				this.mMenuButton.Resize(Constants.UIMenuButtonPosition.X, Constants.UIMenuButtonPosition.Y, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
				this.mStoreButton = new GameButton(1, this);
				this.mStoreButton.mButtonImage = AtlasResources.IMAGE_ZENSHOPBUTTON;
				this.mStoreButton.mOverImage = AtlasResources.IMAGE_ZENSHOPBUTTON_HIGHLIGHT;
				this.mStoreButton.mDownImage = AtlasResources.IMAGE_ZENSHOPBUTTON_HIGHLIGHT;
				this.mStoreButton.mParentWidget = this;
				this.mStoreButton.Resize(Constants.ZenGardenStoreButtonX, Constants.ZenGardenStoreButtonY, AtlasResources.IMAGE_ZENSHOPBUTTON.mWidth, AtlasResources.IMAGE_ZENSHOPBUTTON.mHeight);
			}
			else
			{
				this.mMenuButton.SetLabel("[MENU_BUTTON]");
				this.mMenuButton.Resize(Constants.UIMenuButtonPosition.X, Constants.UIMenuButtonPosition.Y, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				this.mStoreButton = new GameButton(1, this);
				this.mStoreButton.mDrawStoneButton = true;
				this.mStoreButton.mBtnNoDraw = true;
				this.mStoreButton.mDisabled = true;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
			{
				this.mMenuButton.SetLabel("[MAIN_MENU_BUTTON]");
				this.mMenuButton.Resize(Constants.UIMenuButtonPosition.X, 2, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
				this.mStoreButton = new GameButton(1, this);
				this.mStoreButton.mDrawStoneButton = true;
				this.mStoreButton.mBtnNoDraw = true;
				this.mStoreButton.Resize(this.mStoreButton.mX, this.mStoreButton.mY, this.mStoreButton.mWidth + 200, this.mStoreButton.mHeight);
				this.mStoreButton.SetLabel("[GET_FULL_VERSION_BUTTON]");
			}
		}

		public override void Dispose()
		{
			this.mAdvice.Dispose();
			this.mMenuButton.Dispose();
			if (this.mStoreButton != null)
			{
				this.mStoreButton.Dispose();
			}
			this.mZombiesRow1.Clear();
			this.mZombiesRow2.Clear();
			this.mZombiesRow3.Clear();
			this.mZombiesRow4.Clear();
			this.mZombiesRow5.Clear();
			this.mZombiesRow6.Clear();
			for (int i = 0; i < this.mZombies.Count; i++)
			{
				this.mZombies[i].PrepareForReuse();
			}
			this.mZombies.Clear();
			for (int j = 0; j < this.mPlants.Count; j++)
			{
				this.mPlants[j].PrepareForReuse();
			}
			this.mPlants.Clear();
			for (int k = 0; k < this.mProjectiles.Count; k++)
			{
				this.mProjectiles[k].PrepareForReuse();
			}
			this.mProjectiles.Clear();
			this.mCoins.Clear();
			for (int l = 0; l < this.mLawnMowers.Count; l++)
			{
				this.mLawnMowers[l].PrepareForReuse();
			}
			this.mLawnMowers.Clear();
			for (int m = 0; m < this.mGridItems.Count; m++)
			{
				this.mGridItems[m].PrepareForReuse();
			}
			this.mGridItems.Clear();
			for (int n = 0; n < this.aRenderList.Length; n++)
			{
				this.aRenderList[n].PrepareForReuse();
			}
			this.mCutScene.Dispose();
			this.mApp.DelayLoadBackgroundResource(string.Empty);
			this.RemoveAllWidgets();
		}

		public void DisposeBoard()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mApp.mZenGarden.LeaveGarden();
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				this.mChallenge.TreeOfWisdomLeave();
			}
			this.mApp.mSoundSystem.StopFoley(FoleyType.FOLEY_RAIN);
			this.mApp.mZenGarden.mBoard = null;
			this.mApp.CrazyDaveDie();
			this.mApp.mEffectSystem.EffectSystemFreeAll();
		}

		public static void BoardInitForPlayer()
		{
			GameConstants.gShownMoreSunTutorial = false;
		}

		public int CountSunBeingCollected()
		{
			int num = 0;
			Coin coin = null;
			while (this.IterateCoins(ref coin))
			{
				if (coin.mIsBeingCollected && coin.IsSun())
				{
					num += coin.GetSunValue();
				}
			}
			return num;
		}

		public void DrawGameObjects(Graphics g)
		{
			int num = 0;
			bool flag = this.mChallenge.IsStormyNightPitchBlack();
			if (!flag)
			{
				int count = this.mPlants.Count;
				for (int i = 0; i < count; i++)
				{
					Plant plant = this.mPlants[i];
					if (!plant.mDead && plant.mOnBungeeState == PlantOnBungeeState.PLANT_NOT_ON_BUNGEE && this.mX + plant.mX >= -32)
					{
						GlobalMembersBoard.AddGameObjectRenderItemPlant(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_PLANT, plant);
						if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && plant.mPottedPlantIndex != -1)
						{
							RenderItem renderItem = this.aRenderList[num];
							renderItem.mRenderObjectType = RenderObjectType.RENDER_ITEM_PLANT_OVERLAY;
							renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, 0, plant.mY);
							renderItem.mGameObject = plant;
							renderItem.mPlant = plant;
							renderItem.id = (long)num;
							num++;
						}
						if ((plant.mSeedType == SeedType.SEED_MAGNETSHROOM || plant.mSeedType == SeedType.SEED_GOLD_MAGNET) && plant.DrawMagnetItemsOnTop())
						{
							RenderItem renderItem2 = this.aRenderList[num];
							renderItem2.mRenderObjectType = RenderObjectType.RENDER_ITEM_PLANT_MAGNET_ITEMS;
							renderItem2.mZPos = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_TOP, 0, -1);
							renderItem2.mGameObject = plant;
							renderItem2.mPlant = plant;
							renderItem2.id = (long)num;
							num++;
						}
					}
				}
			}
			Coin theGameObject = null;
			while (this.IterateCoins(ref theGameObject))
			{
				GlobalMembersBoard.AddGameObjectRenderItemCoin(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_COIN, theGameObject);
			}
			if (!flag)
			{
				int count2 = this.mZombies.Count;
				for (int j = 0; j < count2; j++)
				{
					Zombie zombie = this.mZombies[j];
					if (!zombie.mDead)
					{
						if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
						{
							this.AddBossRenderItem(this.aRenderList, ref num, zombie);
						}
						else
						{
							GlobalMembersBoard.AddGameObjectRenderItemZombie(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_ZOMBIE, zombie);
							if (zombie.HasShadow() && !GlobalStaticVars.gLowFramerate)
							{
								RenderItem renderItem3 = this.aRenderList[num];
								renderItem3.mRenderObjectType = RenderObjectType.RENDER_ITEM_ZOMBIE_SHADOW;
								renderItem3.mZPos = 200000 + 10000 * zombie.mRow + 3;
								renderItem3.mGameObject = zombie;
								renderItem3.mZombie = zombie;
								renderItem3.id = (long)num;
								num++;
							}
							if (zombie.mZombieType == ZombieType.ZOMBIE_BUNGEE)
							{
								RenderItem renderItem4 = this.aRenderList[num];
								renderItem4.mRenderObjectType = RenderObjectType.RENDER_ITEM_ZOMBIE_BUNGEE_TARGET;
								renderItem4.mZPos = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, zombie.mRow, 1);
								renderItem4.mGameObject = zombie;
								renderItem4.mZombie = zombie;
								renderItem4.id = (long)num;
								num++;
							}
						}
					}
				}
				int num2 = -1;
				Projectile projectile = null;
				while (this.IterateProjectiles(ref projectile, ref num2))
				{
					GlobalMembersBoard.AddGameObjectRenderItemProjectile(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_PROJECTILE, projectile);
					if (!GlobalStaticVars.gLowFramerate)
					{
						RenderItem renderItem5 = this.aRenderList[num];
						renderItem5.mRenderObjectType = RenderObjectType.RENDER_ITEM_PROJECTILE_SHADOW;
						renderItem5.mZPos = 305000 + 10000 * projectile.mRow + 3;
						renderItem5.mGameObject = projectile;
						renderItem5.mProjectile = projectile;
						renderItem5.id = (long)num;
						num++;
					}
				}
				LawnMower lawnMower = null;
				while (this.IterateLawnMowers(ref lawnMower))
				{
					RenderItem renderItem6 = this.aRenderList[num];
					renderItem6.mRenderObjectType = RenderObjectType.RENDER_ITEM_MOWER;
					renderItem6.mZPos = lawnMower.mRenderOrder;
					renderItem6.mMower = lawnMower;
					renderItem6.id = (long)num;
					num++;
				}
				int num3 = -1;
				TodParticleSystem todParticleSystem = null;
				while (this.IterateParticles(ref todParticleSystem, ref num3))
				{
					if (!todParticleSystem.mIsAttachment)
					{
						RenderItem renderItem7 = this.aRenderList[num];
						renderItem7.mRenderObjectType = RenderObjectType.RENDER_ITEM_PARTICLE;
						renderItem7.mZPos = todParticleSystem.mRenderOrder;
						renderItem7.mParticleSytem = todParticleSystem;
						renderItem7.id = (long)num;
						num++;
					}
				}
				int num4 = -1;
				Reanimation reanimation = null;
				while (this.IterateReanimations(ref reanimation, ref num4))
				{
					if (!reanimation.mIsAttachment)
					{
						RenderItem renderItem8 = this.aRenderList[num];
						renderItem8.mRenderObjectType = RenderObjectType.RENDER_ITEM_REANIMATION;
						renderItem8.mZPos = reanimation.mRenderOrder;
						renderItem8.mReanimation = reanimation;
						renderItem8.id = (long)num;
						num++;
					}
				}
				int num5 = -1;
				GridItem gridItem = null;
				while (this.IterateGridItems(ref gridItem, ref num5))
				{
					RenderItem renderItem9 = this.aRenderList[num];
					renderItem9.mRenderObjectType = RenderObjectType.RENDER_ITEM_GRID_ITEM;
					renderItem9.mZPos = gridItem.mRenderOrder;
					renderItem9.mGridItem = gridItem;
					renderItem9.id = (long)num;
					num++;
					if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && gridItem.mGridItemType == GridItemType.GRIDITEM_STINKY)
					{
						RenderItem renderItem10 = this.aRenderList[num];
						renderItem10.mRenderObjectType = RenderObjectType.RENDER_ITEM_GRID_ITEM_OVERLAY;
						renderItem10.mZPos = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, 0, (int)gridItem.mPosY - 30);
						renderItem10.mGridItem = gridItem;
						num++;
					}
				}
			}
			for (int k = 0; k < Constants.MAX_GRIDSIZEY; k++)
			{
				if (this.mIceTimer[k] > 0)
				{
					RenderItem renderItem11 = this.aRenderList[num];
					renderItem11.mRenderObjectType = RenderObjectType.RENDER_ITEM_ICE;
					renderItem11.mBoardGridY = k;
					renderItem11.mZPos = this.GetIceZPos(k);
					renderItem11.id = (long)num;
					num++;
				}
			}
			int thePosZ;
			if (this.mTimeStopCounter > 0)
			{
				thePosZ = 800000;
			}
			else if (this.mApp.mGameScene == GameScenes.SCENE_PLAYING || this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON || this.mCutScene.IsAfterSeedChooser() || this.mCutScene.IsInShovelTutorial() || this.mHelpIndex == AdviceType.ADVICE_CLICK_TO_CONTINUE)
			{
				thePosZ = 100001;
			}
			else
			{
				thePosZ = 800000;
			}
			GlobalMembersBoard.AddUIRenderItem(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_BACKDROP, 100000);
			GlobalMembersBoard.AddUIRenderItem(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_BOTTOM_UI, thePosZ);
			GlobalMembersBoard.AddUIRenderItem(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_COIN_BANK, 600000);
			GlobalMembersBoard.AddUIRenderItem(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_TOP_UI, 700000);
			GlobalMembersBoard.AddUIRenderItem(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_SCREEN_FADE, 900000);
			if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
			{
				int thePosZ2 = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, 3, 2);
				GlobalMembersBoard.AddUIRenderItem(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_DOOR_MASK, thePosZ2);
			}
			if (this.StageHasFog())
			{
				GlobalMembersBoard.AddUIRenderItem(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_FOG, 500000);
			}
			if (this.mApp.IsStormyNightLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
			{
				GlobalMembersBoard.AddUIRenderItem(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_STORM, 500003);
			}
			GlobalMembersBoard.AddGameObjectRenderItemCursorPreview(this.aRenderList, ref num, RenderObjectType.RENDER_ITEM_CURSOR_PREVIEW, this.mCursorPreview);
			if (Board.needToSortRenderList)
			{
				Array.Sort<RenderItem>(this.aRenderList, 0, num, null);
			}
			this.SortZombieRowLists();
			for (int l = 0; l < num; l++)
			{
				RenderItem renderItem12 = this.aRenderList[l];
				if (renderItem12.mRenderObjectType != (RenderObjectType)0)
				{
					RenderObjectType mRenderObjectType = renderItem12.mRenderObjectType;
					if (mRenderObjectType != RenderObjectType.RENDER_ITEM_ICE)
					{
						if (mRenderObjectType != RenderObjectType.RENDER_ITEM_BACKDROP)
						{
							if (mRenderObjectType == RenderObjectType.RENDER_ITEM_GRID_ITEM && renderItem12.mGridItem.mGridItemType == GridItemType.GRIDITEM_GRAVESTONE)
							{
								renderItem12.mGridItem.DrawGridItem(g);
							}
						}
						else if (!flag)
						{
							this.DrawBackdrop(g);
							this.DrawCursorOnBackground(g);
						}
					}
					else
					{
						this.DrawIce(g, renderItem12.mBoardGridY);
					}
				}
			}
			for (int m = 0; m < num; m++)
			{
				RenderItem renderItem13 = this.aRenderList[m];
				if (renderItem13.mRenderObjectType != (RenderObjectType)0)
				{
					switch (renderItem13.mRenderObjectType)
					{
					case RenderObjectType.RENDER_ITEM_COIN:
						if (renderItem13.mCoin.BeginDraw(g))
						{
							renderItem13.mCoin.Draw(g);
							renderItem13.mCoin.EndDraw(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_PROJECTILE:
						if (renderItem13.mProjectile.BeginDraw(g))
						{
							renderItem13.mProjectile.Draw(g);
							renderItem13.mProjectile.EndDraw(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_ZOMBIE:
						if (renderItem13.mZombie.BeginDraw(g))
						{
							renderItem13.mZombie.Draw(g);
							renderItem13.mZombie.EndDraw(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_ZOMBIE_SHADOW:
						if (renderItem13.mZombie.BeginDraw(g))
						{
							renderItem13.mZombie.DrawShadow(g);
							renderItem13.mZombie.EndDraw(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_ZOMBIE_BUNGEE_TARGET:
						renderItem13.mZombie.DrawBungeeTarget(g);
						break;
					case RenderObjectType.RENDER_ITEM_PLANT:
						if (renderItem13.mPlant.BeginDraw(g))
						{
							renderItem13.mPlant.Draw(g);
							renderItem13.mPlant.EndDraw(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_PLANT_OVERLAY:
						if (renderItem13.mPlant.BeginDraw(g))
						{
							this.mApp.mZenGarden.DrawPlantOverlay(g, renderItem13.mPlant);
							renderItem13.mPlant.EndDraw(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_PLANT_MAGNET_ITEMS:
						if (renderItem13.mPlant.BeginDraw(g))
						{
							renderItem13.mPlant.DrawMagnetItems(g);
							renderItem13.mPlant.EndDraw(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_CURSOR_PREVIEW:
						if (renderItem13.mCursorPreview.BeginDraw(g))
						{
							renderItem13.mCursorPreview.Draw(g);
							renderItem13.mCursorPreview.EndDraw(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_PARTICLE:
						renderItem13.mParticleSytem.Draw(g, true);
						break;
					case RenderObjectType.RENDER_ITEM_REANIMATION:
						renderItem13.mReanimation.Draw(g);
						break;
					case RenderObjectType.RENDER_ITEM_TOP_UI:
						this.DrawUITop(g);
						if (flag)
						{
							this.DrawCursorOnBackground(g);
						}
						this.DrawCursorOverlay(g);
						break;
					case RenderObjectType.RENDER_ITEM_FOG:
						this.DrawFog(g);
						break;
					case RenderObjectType.RENDER_ITEM_STORM:
						this.mChallenge.DrawWeather(g);
						break;
					case RenderObjectType.RENDER_ITEM_BOTTOM_UI:
						this.DrawUIBottom(g);
						break;
					case RenderObjectType.RENDER_ITEM_DOOR_MASK:
						this.DrawHouseDoorTop(g);
						break;
					case RenderObjectType.RENDER_ITEM_COIN_BANK:
						this.DrawUICoinBank(g);
						break;
					case RenderObjectType.RENDER_ITEM_PROJECTILE_SHADOW:
						if (renderItem13.mProjectile.BeginDraw(g))
						{
							renderItem13.mProjectile.DrawShadow(g);
							renderItem13.mProjectile.EndDraw(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_MOWER:
						renderItem13.mMower.Draw(g);
						break;
					case RenderObjectType.RENDER_ITEM_SCREEN_FADE:
						this.DrawFadeOut(g);
						break;
					case RenderObjectType.RENDER_ITEM_BOSS_PART:
					{
						Zombie bossZombie = this.GetBossZombie();
						if (bossZombie != null && bossZombie.BeginDraw(g))
						{
							bossZombie.DrawBossPart(g, renderItem13.mBossPart);
							bossZombie.EndDraw(g);
						}
						break;
					}
					case RenderObjectType.RENDER_ITEM_GRID_ITEM:
						if (renderItem13.mGridItem.mGridItemType != GridItemType.GRIDITEM_GRAVESTONE)
						{
							renderItem13.mGridItem.DrawGridItem(g);
						}
						break;
					case RenderObjectType.RENDER_ITEM_GRID_ITEM_OVERLAY:
						renderItem13.mGridItem.DrawGridItemOverlay(g);
						break;
					}
				}
			}
		}

		public void ClearCursor()
		{
			if (this.mAdvice.mDuration > 0 && (this.mHelpIndex == AdviceType.ADVICE_PLANT_GRAVEBUSTERS_ON_GRAVES || this.mHelpIndex == AdviceType.ADVICE_PLANT_LILYPAD_ON_WATER || this.mHelpIndex == AdviceType.ADVICE_PLANT_TANGLEKELP_ON_WATER || this.mHelpIndex == AdviceType.ADVICE_PLANT_SEASHROOM_ON_WATER || this.mHelpIndex == AdviceType.ADVICE_PLANT_POTATO_MINE_ON_LILY || this.mHelpIndex == AdviceType.ADVICE_PLANT_WRONG_ART_TYPE || this.mHelpIndex == AdviceType.ADVICE_PLANT_NEED_POT || this.mHelpIndex == AdviceType.ADVICE_PLANT_NOT_PASSED_LINE || this.mHelpIndex == AdviceType.ADVICE_PLANT_ONLY_ON_REPEATERS || this.mHelpIndex == AdviceType.ADVICE_PLANT_ONLY_ON_MELONPULT || this.mHelpIndex == AdviceType.ADVICE_PLANT_ONLY_ON_SUNFLOWER || this.mHelpIndex == AdviceType.ADVICE_PLANT_ONLY_ON_KERNELPULT))
			{
				this.ClearAdvice(this.mHelpIndex);
			}
			if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_USABLE_COIN && this.mCursorObject.mCoinID != null)
			{
				this.mCursorObject.mCoinID.mIsBeingCollected = false;
			}
			this.mCursorObject.mType = SeedType.SEED_NONE;
			this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_NORMAL;
			this.mCursorObject.mSeedBankIndex = -1;
			this.mCursorObject.mCoinID = null;
			this.mCursorObject.mDuplicatorPlantID = null;
			this.mCursorObject.mCobCannonPlantID = null;
			this.mCursorObject.mGlovePlantID = null;
			this.mChallenge.ClearCursor();
			if (this.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PLANT_PEASHOOTER)
			{
				this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_1_PICK_UP_PEASHOOTER);
				return;
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_LEVEL_2_PLANT_SUNFLOWER || this.mTutorialState == TutorialState.TUTORIAL_LEVEL_2_REFRESH_SUNFLOWER)
			{
				if (!this.mSeedBank.mSeedPackets[1].CanPickUp())
				{
					this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_2_REFRESH_SUNFLOWER);
					return;
				}
				this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_2_PICK_UP_SUNFLOWER);
				return;
			}
			else
			{
				if (this.mTutorialState != TutorialState.TUTORIAL_MORESUN_PLANT_SUNFLOWER && this.mTutorialState != TutorialState.TUTORIAL_MORESUN_REFRESH_SUNFLOWER)
				{
					if (this.mTutorialState == TutorialState.TUTORIAL_SHOVEL_DIG)
					{
						this.SetTutorialState(TutorialState.TUTORIAL_SHOVEL_PICKUP);
					}
					return;
				}
				if (!this.mSeedBank.mSeedPackets[1].CanPickUp())
				{
					this.SetTutorialState(TutorialState.TUTORIAL_MORESUN_REFRESH_SUNFLOWER);
					return;
				}
				this.SetTutorialState(TutorialState.TUTORIAL_MORESUN_PICK_UP_SUNFLOWER);
				return;
			}
		}

		public bool AreEnemyZombiesOnScreen()
		{
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && zombie.mHasHead && !zombie.IsDeadOrDying() && !zombie.mMindControlled)
				{
					return true;
				}
			}
			return false;
		}

		public LawnMower FindLawnMowerInRow(int theRow)
		{
			LawnMower lawnMower = null;
			while (this.IterateLawnMowers(ref lawnMower))
			{
				if (lawnMower.mRow == theRow)
				{
					return lawnMower;
				}
			}
			return null;
		}

		public void SaveGame(string theFilePath)
		{
			GlobalMembersSaveGame.LawnSaveGame(this, theFilePath);
		}

		public bool LoadGame(string theFilePath)
		{
			if (!GlobalMembersSaveGame.LawnLoadGame(this, theFilePath))
			{
				return false;
			}
			this.LoadBackgroundImages();
			this.ResetFPSStats();
			this.UpdateLayers();
			return true;
		}

		public void InitLevel()
		{
			this.mMainCounter = 0;
			this.mEnableGraveStones = false;
			this.mSodPosition = 0;
			this.mPrevBoardResult = this.mApp.mBoardResult;
			if (this.mApp.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mApp.mMusic.StopAllMusic();
			}
			this.mNomNomNomAchievementTracker = true;
			this.mNoFungusAmongUsAchievementTracker = true;
			this.mPeaShooterUsed = false;
			this.mCatapultPlantsUsed = false;
			this.mMushroomAndCoffeeBeansOnly = true;
			if (this.mApp.IsAdventureMode())
			{
				this.mLevel = this.mApp.mPlayerInfo.GetLevel();
			}
			else if (this.mApp.IsQuickPlayMode())
			{
				this.mLevel = this.mApp.mGameMode - GameMode.GAMEMODE_QUICKPLAY_1 + 1;
			}
			else
			{
				this.mLevel = 0;
			}
			this.mLevelStr = TodStringFile.TodStringTranslate("[LEVEL]") + " " + this.mApp.GetStageString(this.mLevel);
			this.PickBackground();
			this.mCurrentWave = 0;
			this.InitZombieWaves();
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST || this.mApp.IsScaryPotterLevel() || this.mApp.IsWhackAZombieLevel())
			{
				this.mSunMoney = 0;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				this.mSunMoney = 5000;
			}
			else if (this.mApp.IsIZombieLevel())
			{
				this.mSunMoney = 150;
			}
			else if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 1)
			{
				this.mSunMoney = 150;
			}
			else
			{
				this.mSunMoney = 50;
			}
			for (int i = 0; i < this.mRowPickingArray.Length; i++)
			{
				this.mRowPickingArray[i] = new TodSmoothArray();
			}
			for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
			{
				this.mWaveRowGotLawnMowered[j] = -100;
				this.mIceMinX[j] = Constants.Board_Ice_Start;
				this.mIceTimer[j] = 0;
				this.mIceParticleID[j] = ParticleSystemID.PARTICLESYSTEMID_NULL;
				this.mRowPickingArray[j].mItem = j;
			}
			this.mNumSunsFallen = 0;
			if (!this.StageIsNight())
			{
				this.mSunCountDown = TodCommon.RandRangeInt(425, 700);
			}
			for (int k = 0; k < 67; k++)
			{
				this.mHelpDisplayed[k] = false;
			}
			this.mSeedBank.mNumPackets = this.GetNumSeedsInBank();
			this.mSeedBank.UpdateHeight();
			for (int l = 0; l < 9; l++)
			{
				SeedPacket seedPacket = this.mSeedBank.mSeedPackets[l];
				seedPacket.mIndex = l;
				seedPacket.mY = this.GetSeedPacketPositionY(l);
				seedPacket.mX = 0;
				seedPacket.mPacketType = SeedType.SEED_NONE;
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 3);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_SUNFLOWER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_SNOWPEA, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 6);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_CHERRYBOMB, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_WALLNUT, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.SEED_REPEATER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.SEED_SNOWPEA, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[5].SetPacketType(SeedType.SEED_CHOMPER, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 3);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_NORMAL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_FOOTBALL, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 3);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_NORMAL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_SCREEN_DOOR, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 3);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_NORMAL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_DIGGER, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 3);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_NORMAL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_LADDER, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 4);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_NORMAL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_BUNGEE, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.SEED_ZOMBIE_BALLOON, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 4);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_NORMAL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_POLEVAULTER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.SEED_ZOMBIE_GARGANTUAR, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 4);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_NORMAL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_POLEVAULTER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.SEED_ZOMBIE_DANCER, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 6);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_IMP, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_TRAFFIC_CONE, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.SEED_ZOMBIE_BUNGEE, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.SEED_ZOMBIE_DIGGER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[5].SetPacketType(SeedType.SEED_ZOMBIE_LADDER, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_9)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 8);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_IMP, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_TRAFFIC_CONE, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_POLEVAULTER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.SEED_ZOMBIE_BUNGEE, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[5].SetPacketType(SeedType.SEED_ZOMBIE_DIGGER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[6].SetPacketType(SeedType.SEED_ZOMBIE_LADDER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[7].SetPacketType(SeedType.SEED_ZOMBIE_FOOTBALL, SeedType.SEED_NONE);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 9);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_ZOMBIE_IMP, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_ZOMBIE_TRAFFIC_CONE, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ZOMBIE_POLEVAULTER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.SEED_ZOMBIE_PAIL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.SEED_ZOMBIE_BUNGEE, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[5].SetPacketType(SeedType.SEED_ZOMBIE_DIGGER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[6].SetPacketType(SeedType.SEED_ZOMBIE_LADDER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[7].SetPacketType(SeedType.SEED_ZOMBIE_FOOTBALL, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[8].SetPacketType(SeedType.SEED_ZOMBIE_DANCER, SeedType.SEED_NONE);
			}
			else if (this.mApp.IsScaryPotterLevel())
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 1);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_CHERRYBOMB, SeedType.SEED_NONE);
			}
			else if (this.mApp.IsWhackAZombieLevel() && (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()))
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 3);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_POTATOMINE, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_GRAVEBUSTER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_CHERRYBOMB, SeedType.SEED_NONE);
			}
			else if (this.mApp.IsWhackAZombieLevel() && !this.mApp.IsAdventureMode() && !this.mApp.IsQuickPlayMode())
			{
				Debug.ASSERT(this.mSeedBank.mNumPackets == 3);
				this.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_POTATOMINE, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_GRAVEBUSTER, SeedType.SEED_NONE);
				this.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_ICESHROOM, SeedType.SEED_NONE);
			}
			else if (!this.ChooseSeedsOnCurrentLevel() && !this.HasConveyorBeltSeedBank())
			{
				this.mSeedBank.mNumPackets = this.GetNumSeedsInBank();
				for (int m = 0; m < this.mSeedBank.mNumPackets; m++)
				{
					SeedPacket seedPacket2 = this.mSeedBank.mSeedPackets[m];
					seedPacket2.SetPacketType((SeedType)m, SeedType.SEED_NONE);
				}
			}
			this.mWidgetManager.MarkAllDirty();
			this.mPaused = false;
			this.mOutOfMoneyCounter = 0;
			if (this.StageHasFog())
			{
				this.mFogOffset = 1065f - (float)this.LeftFogColumn() * 80f;
				this.mFogBlownCountDown = 200;
			}
			this.mChallenge.InitLevel();
			this.SetupRenderItems();
			Board.needToSortRenderList = true;
		}

		private void SetupRenderItems()
		{
			for (int i = 0; i < this.aRenderList.Length; i++)
			{
				if (this.aRenderList[i] != null)
				{
					this.aRenderList[i].PrepareForReuse();
				}
				this.aRenderList[i] = RenderItem.GetNewRenderItem();
			}
		}

		public void DisplayAdvice(string theAdvice, MessageStyle theMessageStyle, AdviceType theHelpIndex)
		{
			this.DisplayAdvice(theAdvice, theMessageStyle, theHelpIndex, null);
		}

		public void DisplayAdvice(string theAdvice, MessageStyle theMessageStyle, AdviceType theHelpIndex, Image theIcon)
		{
			if (theHelpIndex != AdviceType.ADVICE_NONE)
			{
				if (this.mHelpDisplayed[(int)theHelpIndex])
				{
					return;
				}
				this.mHelpDisplayed[(int)theHelpIndex] = true;
			}
			this.mAdvice.SetLabel(theAdvice, theMessageStyle, theIcon);
			this.mHelpIndex = theHelpIndex;
		}

		public void StartLevel()
		{
			this.mCoinBankFadeCount = 0;
			this.mLevelFadeCount = 1000;
			this.mApp.mLastLevelStats.Reset();
			this.mChallenge.StartLevel();
			if (this.mApp.IsSurvivalMode() && this.mChallenge.mSurvivalStage > 0)
			{
				string savedGameName = LawnCommon.GetSavedGameName(this.mApp.mGameMode, (int)this.mApp.mPlayerInfo.mId);
				this.mApp.EraseFile(savedGameName);
			}
			if (this.mApp.IsSurvivalMode() && this.mChallenge.mSurvivalStage > 0)
			{
				this.FreezeEffectsForCutscene(false);
				this.mApp.mSoundSystem.GamePause(false);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE)
			{
				return;
			}
			if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mApp.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM && this.mApp.mGameMode != GameMode.GAMEMODE_UPSELL && this.mApp.mGameMode != GameMode.GAMEMODE_INTRO && this.mApp.mGameMode != GameMode.GAMEMODE_INTRO)
			{
				if (this.mApp.IsFinalBossLevel())
				{
					return;
				}
				this.mApp.mMusic.StartGameMusic();
			}
		}

		public Plant AddPlant(int theGridX, int theGridY, SeedType theSeedType, SeedType theImitaterType)
		{
			Plant plant = this.NewPlant(theGridX, theGridY, theSeedType, theImitaterType);
			this.DoPlantingEffects(theGridX, theGridY, plant, false);
			this.mChallenge.PlantAdded(plant);
			int num = this.CountPlantByType(SeedType.SEED_SUNFLOWER) + this.CountPlantByType(SeedType.SEED_SUNSHROOM);
			if (num > this.mMaxSunPlants)
			{
				this.mMaxSunPlants = num;
			}
			SeedType seedType = theSeedType;
			if (seedType == SeedType.SEED_IMITATER)
			{
				seedType = theImitaterType;
			}
			if (seedType == SeedType.SEED_PEASHOOTER)
			{
				Board.mPeashootersPlanted++;
				if (Board.mPeashootersPlanted >= 10)
				{
					if (SexyAppBase.IsInTrialMode)
					{
						if (!this.mApp.mPlayerInfo.mHasSeenAchievementDialog)
						{
							this.mApp.mPlayerInfo.mHasSeenAchievementDialog = true;
							this.mApp.achievementToCheck = AchievementId.SoilYourPlants;
							this.mApp.DoLockedAchievementDialog(AchievementId.SoilYourPlants);
						}
					}
					else
					{
						this.GrantAchievement(AchievementId.SoilYourPlants);
					}
				}
			}
			if (seedType != SeedType.SEED_SUNFLOWER && seedType != SeedType.SEED_WALLNUT && seedType != SeedType.SEED_CHOMPER)
			{
				this.mNomNomNomAchievementTracker = false;
			}
			if (this.IsFungus(seedType))
			{
				this.mNoFungusAmongUsAchievementTracker = false;
			}
			if (seedType == SeedType.SEED_PEASHOOTER || seedType == SeedType.SEED_SNOWPEA || seedType == SeedType.SEED_REPEATER || seedType == SeedType.SEED_THREEPEATER || seedType == SeedType.SEED_SPLITPEA || seedType == SeedType.SEED_GATLINGPEA)
			{
				this.mPeaShooterUsed = true;
			}
			if (seedType == SeedType.SEED_CABBAGEPULT || seedType == SeedType.SEED_KERNELPULT || seedType == SeedType.SEED_MELONPULT || seedType == SeedType.SEED_WINTERMELON)
			{
				this.mCatapultPlantsUsed = true;
			}
			bool flag = this.IsFungus(seedType);
			if (seedType != SeedType.SEED_INSTANT_COFFEE && !flag)
			{
				this.mMushroomAndCoffeeBeansOnly = false;
			}
			if (flag)
			{
				this.mMushroomsUsed = true;
			}
			return plant;
		}

		public bool IsFungus(SeedType aCheckSeed)
		{
			return aCheckSeed == SeedType.SEED_PUFFSHROOM || aCheckSeed == SeedType.SEED_SUNSHROOM || aCheckSeed == SeedType.SEED_FUMESHROOM || aCheckSeed == SeedType.SEED_HYPNOSHROOM || aCheckSeed == SeedType.SEED_SCAREDYSHROOM || aCheckSeed == SeedType.SEED_ICESHROOM || aCheckSeed == SeedType.SEED_DOOMSHROOM || aCheckSeed == SeedType.SEED_MAGNETSHROOM || aCheckSeed == SeedType.SEED_SEASHROOM || aCheckSeed == SeedType.SEED_GLOOMSHROOM;
		}

		public Projectile AddProjectile(int theX, int theY, int aRenderOrder, int theRow, ProjectileType projectileType)
		{
			Projectile newProjectile = Projectile.GetNewProjectile();
			newProjectile.ProjectileInitialize(theX, theY, aRenderOrder, theRow, projectileType);
			this.mProjectiles.Add(newProjectile);
			return newProjectile;
		}

		public Coin AddCoin(int theX, int theY, CoinType theCoinType, CoinMotion theCoinMotion)
		{
			Coin coin = new Coin();
			coin.CoinInitialize(theX, theY, theCoinType, theCoinMotion);
			this.mCoins.Add(coin);
			if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 1)
			{
				this.DisplayAdvice("[ADVICE_CLICK_ON_SUN]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1_STAY, AdviceType.ADVICE_CLICK_ON_SUN);
			}
			return coin;
		}

		public void RefreshSeedPacketFromCursor()
		{
			if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_USABLE_COIN)
			{
				Coin coin = this.mCoins[this.mCoins.IndexOf(this.mCursorObject.mCoinID)];
				coin.DroppedUsableSeed();
			}
			else if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_BANK)
			{
				Debug.ASSERT(this.mCursorObject.mSeedBankIndex >= 0 && this.mCursorObject.mSeedBankIndex < this.mSeedBank.mNumPackets);
				SeedPacket seedPacket = this.mSeedBank.mSeedPackets[this.mCursorObject.mSeedBankIndex];
				seedPacket.Activate();
			}
			this.ClearCursor();
		}

		public void DeselectSeedPacket()
		{
			if (this.mCursorObject.mSeedBankIndex == -1)
			{
				return;
			}
			SeedPacket seedPacket = this.mSeedBank.mSeedPackets[this.mCursorObject.mSeedBankIndex];
			seedPacket.Activate();
			this.ClearCursor();
		}

		public ZombieType PickGraveRisingZombieType(int theZombiePoints)
		{
			for (int i = 0; i < Board.aZombieWeightArray.Length; i++)
			{
				Board.aZombieWeightArray[i].Reset();
			}
			int num = 0;
			Board.aZombieWeightArray[num].mItem = 0;
			Board.aZombieWeightArray[num].mWeight = Zombie.GetZombieDefinition(ZombieType.ZOMBIE_NORMAL).mPickWeight;
			num++;
			Board.aZombieWeightArray[num].mItem = 2;
			Board.aZombieWeightArray[num].mWeight = Zombie.GetZombieDefinition(ZombieType.ZOMBIE_TRAFFIC_CONE).mPickWeight;
			num++;
			if (!this.StageHasGraveStones())
			{
				Board.aZombieWeightArray[num].mItem = 4;
				Board.aZombieWeightArray[num].mWeight = Zombie.GetZombieDefinition(ZombieType.ZOMBIE_PAIL).mPickWeight;
				num++;
			}
			for (int j = 0; j < num; j++)
			{
				ZombieType zombieType = (ZombieType)Board.aZombieWeightArray[j].mItem;
				ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(zombieType);
				if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel < zombieDefinition.mStartingLevel)
				{
					Board.aZombieWeightArray[j].mWeight = 0;
				}
				else if (!this.mZombieAllowed[(int)zombieType] && zombieType != ZombieType.ZOMBIE_NORMAL)
				{
					Board.aZombieWeightArray[j].mWeight = 0;
				}
				else
				{
					Board.aZombieWeightArray[j].mWeight = zombieDefinition.mPickWeight;
				}
			}
			return (ZombieType)TodCommon.TodPickFromWeightedArray(Board.aZombieWeightArray, num);
		}

		public ZombieType PickZombieType(int theZombiePoints, int theWaveIndex, ZombiePicker theZombiePicker)
		{
			int num = 0;
			for (int i = 0; i < 33; i++)
			{
				Board.aZombieWeightArray[i].Reset();
				ZombieType zombieType = (ZombieType)i;
				ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(zombieType);
				if (this.mZombieAllowed[(int)zombieType])
				{
					if (zombieType == ZombieType.ZOMBIE_BUNGEE && this.mApp.IsSurvivalEndless(this.mApp.mGameMode))
					{
						if (!this.IsFlagWave(theWaveIndex))
						{
							goto IL_1E4;
						}
					}
					else if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_POGO_PARTY && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_AIR_RAID)
					{
						int num2 = zombieDefinition.mFirstAllowedWave;
						if (this.mApp.IsSurvivalEndless(this.mApp.mGameMode))
						{
							int survivalFlagsCompleted = this.GetSurvivalFlagsCompleted();
							int num3 = TodCommon.TodAnimateCurve(18, 50, survivalFlagsCompleted, 0, 15, TodCurves.CURVE_LINEAR);
							num2 = Math.Max(num2 - num3, 1);
						}
						if (theWaveIndex + 1 < num2 || theZombiePoints < zombieDefinition.mZombieValue)
						{
							goto IL_1E4;
						}
					}
					int mWeight = zombieDefinition.mPickWeight;
					if (this.mApp.IsSurvivalMode())
					{
						int survivalFlagsCompleted2 = this.GetSurvivalFlagsCompleted();
						if (zombieType == ZombieType.ZOMBIE_GARGANTUAR || zombieType == ZombieType.ZOMBIE_ZAMBONI)
						{
							int num4 = TodCommon.TodAnimateCurve(10, 50, survivalFlagsCompleted2, 2, 50, TodCurves.CURVE_LINEAR);
							if (theZombiePicker.mZombieTypeCount[(int)zombieType] >= num4)
							{
								goto IL_1E4;
							}
						}
						if (zombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
						{
							if (this.IsFlagWave(theWaveIndex))
							{
								int num5 = TodCommon.TodAnimateCurve(14, 100, survivalFlagsCompleted2, 1, 50, TodCurves.CURVE_LINEAR);
								if (theZombiePicker.mZombieTypeCount[(int)zombieType] >= num5)
								{
									goto IL_1E4;
								}
							}
							else
							{
								int num6 = TodCommon.TodAnimateCurve(10, 110, survivalFlagsCompleted2, 1, 50, TodCurves.CURVE_LINEAR);
								if (theZombiePicker.mAllWavesZombieTypeCount[(int)zombieType] >= num6)
								{
									goto IL_1E4;
								}
								mWeight = 1000;
							}
						}
						if (zombieType == ZombieType.ZOMBIE_NORMAL)
						{
							mWeight = TodCommon.TodAnimateCurve(10, 50, survivalFlagsCompleted2, zombieDefinition.mPickWeight, zombieDefinition.mPickWeight / 10, TodCurves.CURVE_LINEAR);
						}
						if (zombieType == ZombieType.ZOMBIE_TRAFFIC_CONE)
						{
							mWeight = TodCommon.TodAnimateCurve(10, 50, survivalFlagsCompleted2, zombieDefinition.mPickWeight, zombieDefinition.mPickWeight / 4, TodCurves.CURVE_LINEAR);
						}
					}
					Board.aZombieWeightArray[num].mItem = i;
					Board.aZombieWeightArray[num].mWeight = mWeight;
					num++;
				}
				IL_1E4:;
			}
			return (ZombieType)TodCommon.TodPickFromWeightedArray(Board.aZombieWeightArray, num);
		}

		public int PickRowForNewZombie(ZombieType theZombieType)
		{
			if (theZombieType == ZombieType.ZOMBIE_BOSS)
			{
				return 0;
			}
			GridItem rake = this.GetRake();
			if (rake != null && rake.mGridItemState == GridItemState.GRIDITEM_STATE_RAKE_ATTRACTING && this.RowCanHaveZombieType(rake.mGridY, theZombieType))
			{
				rake.mGridItemState = GridItemState.GRIDITEM_STATE_RAKE_WAITING;
				TodCommon.TodUpdateSmoothArrayPick(this.mRowPickingArray, Constants.MAX_GRIDSIZEY, rake.mGridY);
				return rake.mGridY;
			}
			for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
			{
				if (!this.RowCanHaveZombieType(i, theZombieType))
				{
					this.mRowPickingArray[i].mWeight = 0f;
				}
				else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
				{
					this.mRowPickingArray[i].mWeight = this.mChallenge.PortalCombatRowSpawnWeight(i);
				}
				else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL && this.mCurrentWave <= 3 && i == 5)
				{
					this.mRowPickingArray[i].mWeight = 0f;
				}
				else
				{
					int num = this.mCurrentWave - this.mWaveRowGotLawnMowered[i];
					if (this.mApp.IsContinuousChallenge() && this.mCurrentWave == this.mNumWaves - 1)
					{
						num = 100;
					}
					if (num <= 1)
					{
						this.mRowPickingArray[i].mWeight = 0.01f;
					}
					else if (num <= 2)
					{
						this.mRowPickingArray[i].mWeight = 0.5f;
					}
					else
					{
						this.mRowPickingArray[i].mWeight = 1f;
					}
				}
			}
			return TodCommon.TodPickFromSmoothArray(this.mRowPickingArray, Constants.MAX_GRIDSIZEY);
		}

		public Zombie AddZombie(ZombieType theZombieType, int theFromWave)
		{
			int theRow = this.PickRowForNewZombie(theZombieType);
			return this.AddZombieInRow(theZombieType, theRow, theFromWave);
		}

		public void SpawnZombieWave()
		{
			this.mChallenge.SpawnZombieWave();
			if (this.mApp.IsBungeeBlitzLevel())
			{
				BungeeDropGrid theBungeeDropGrid = new BungeeDropGrid();
				this.SetupBungeeDrop(theBungeeDropGrid);
				for (int i = 0; i < 50; i++)
				{
					ZombieType zombieType = this.mZombiesInWave[this.mCurrentWave, i];
					if (zombieType == ZombieType.ZOMBIE_INVALID)
					{
						break;
					}
					if (zombieType == ZombieType.ZOMBIE_BUNGEE || zombieType == ZombieType.ZOMBIE_ZAMBONI)
					{
						this.AddZombie(zombieType, this.mCurrentWave);
					}
					else
					{
						this.BungeeDropZombie(theBungeeDropGrid, zombieType);
					}
				}
			}
			else
			{
				Debug.ASSERT(this.mCurrentWave >= 0 && this.mCurrentWave < 100 && this.mCurrentWave < this.mNumWaves);
				for (int j = 0; j < 50; j++)
				{
					ZombieType zombieType2 = this.mZombiesInWave[this.mCurrentWave, j];
					if (zombieType2 == ZombieType.ZOMBIE_INVALID)
					{
						break;
					}
					if (zombieType2 == ZombieType.ZOMBIE_BOBSLED && !this.CanAddBobSled())
					{
						for (int k = 0; k < 4; k++)
						{
							this.AddZombie(ZombieType.ZOMBIE_NORMAL, this.mCurrentWave);
						}
					}
					else
					{
						this.AddZombie(zombieType2, this.mCurrentWave);
					}
				}
			}
			if (this.mCurrentWave == this.mNumWaves - 1 && !this.mApp.IsContinuousChallenge())
			{
				this.mRiseFromGraveCounter = 210;
			}
			if (this.IsFlagWave(this.mCurrentWave))
			{
				this.mFlagRaiseCounter = 100;
			}
			this.mCurrentWave++;
			this.mTotalSpawnedWaves++;
		}

		public void RemoveAllZombies()
		{
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && !zombie.IsDeadOrDying())
				{
					zombie.DieNoLoot(false);
				}
			}
		}

		public void RemoveCutsceneZombies()
		{
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && zombie.mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
				{
					zombie.DieNoLoot(false);
				}
			}
		}

		public void SpawnZombiesFromGraves()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS_2)
			{
				return;
			}
			if (this.StageHasRoof())
			{
				this.SpawnZombiesFromSky();
			}
			else if (this.StageHasPool())
			{
				this.SpawnZombiesFromPool();
				return;
			}
			int num = this.GetGraveStoneCount();
			int num2 = -1;
			GridItem gridItem = null;
			while (this.IterateGridItems(ref gridItem, ref num2))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_GRAVESTONE && gridItem.mGridItemCounter >= 100 && (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_GRAVE_DANGER || RandomNumbers.NextNumber(this.mNumWaves) <= this.mCurrentWave))
				{
					ZombieType theZombieType = this.PickGraveRisingZombieType(num);
					ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
					Zombie zombie = this.AddZombie(theZombieType, this.mCurrentWave);
					if (zombie == null)
					{
						return;
					}
					zombie.RiseFromGrave(gridItem.mGridX, gridItem.mGridY);
					num -= zombieDefinition.mZombieValue;
					num = Math.Max(1, num);
				}
			}
		}

		public PlantingReason CanPlantAt(int theGridX, int theGridY, SeedType theType)
		{
			if (theGridX < 0 || theGridX >= Constants.GRIDSIZEX || theGridY < 0 || theGridY >= Constants.MAX_GRIDSIZEY)
			{
				return PlantingReason.PLANTING_NOT_HERE;
			}
			PlantingReason plantingReason = this.mChallenge.CanPlantAt(theGridX, theGridY, theType);
			if (plantingReason != PlantingReason.PLANTING_OK || Challenge.IsZombieSeedType(theType))
			{
				return plantingReason;
			}
			PlantsOnLawn plantsOnLawn = default(PlantsOnLawn);
			this.GetPlantsOnLawn(theGridX, theGridY, ref plantsOnLawn);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				if (plantsOnLawn.mUnderPlant != null || plantsOnLawn.mNormalPlant != null || plantsOnLawn.mFlyingPlant != null || plantsOnLawn.mPumpkinPlant != null)
				{
					return PlantingReason.PLANTING_NOT_HERE;
				}
				if (this.mApp.mZenGarden.mGardenType == GardenType.GARDEN_AQUARIUM && !Plant.IsAquatic(theType))
				{
					return PlantingReason.PLANTING_NOT_ON_WATER;
				}
				return PlantingReason.PLANTING_OK;
			}
			else
			{
				bool flag = this.GetGraveStoneAt(theGridX, theGridY) != null;
				if (theType == SeedType.SEED_GRAVEBUSTER)
				{
					if (plantsOnLawn.mNormalPlant != null)
					{
						return PlantingReason.PLANTING_NOT_HERE;
					}
					if (flag)
					{
						return PlantingReason.PLANTING_OK;
					}
					return PlantingReason.PLANTING_ONLY_ON_GRAVES;
				}
				else if (theType == SeedType.SEED_INSTANT_COFFEE)
				{
					if (plantsOnLawn.mFlyingPlant != null)
					{
						return PlantingReason.PLANTING_NOT_HERE;
					}
					if (plantsOnLawn.mNormalPlant == null || !plantsOnLawn.mNormalPlant.mIsAsleep || plantsOnLawn.mNormalPlant.mWakeUpCounter > 0 || plantsOnLawn.mNormalPlant.mOnBungeeState == PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE)
					{
						return PlantingReason.PLANTING_NEEDS_SLEEPING;
					}
					return PlantingReason.PLANTING_OK;
				}
				else if (flag)
				{
					if (Plant.IsFlying(theType))
					{
						return PlantingReason.PLANTING_OK;
					}
					return PlantingReason.PLANTING_NOT_ON_GRAVE;
				}
				else
				{
					bool flag2 = plantsOnLawn.mUnderPlant != null && plantsOnLawn.mUnderPlant.mSeedType == SeedType.SEED_LILYPAD && plantsOnLawn.mUnderPlant.mOnBungeeState != PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE;
					bool flag3 = plantsOnLawn.mUnderPlant != null && plantsOnLawn.mUnderPlant.mSeedType == SeedType.SEED_FLOWERPOT && plantsOnLawn.mUnderPlant.mOnBungeeState != PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE;
					bool flag4 = this.GetCraterAt(theGridX, theGridY) != null;
					if (flag4)
					{
						if (Plant.IsFlying(theType))
						{
							return PlantingReason.PLANTING_OK;
						}
						return PlantingReason.PLANTING_NOT_ON_CRATER;
					}
					else
					{
						bool flag5 = this.GetScaryPotAt(theGridX, theGridY) != null;
						if (flag5)
						{
							return PlantingReason.PLANTING_NOT_HERE;
						}
						if (this.IsIceAt(theGridX, theGridY))
						{
							return PlantingReason.PLANTING_NOT_HERE;
						}
						if (this.mGridSquareType[theGridX, theGridY] == GridSquareType.GRIDSQUARE_DIRT || this.mGridSquareType[theGridX, theGridY] == GridSquareType.GRIDSQUARE_NONE)
						{
							return PlantingReason.PLANTING_NOT_HERE;
						}
						if (theType == SeedType.SEED_LILYPAD || theType == SeedType.SEED_TANGLEKELP || theType == SeedType.SEED_SEASHROOM)
						{
							if (!this.IsPoolSquare(theGridX, theGridY))
							{
								return PlantingReason.PLANTING_ONLY_IN_POOL;
							}
							if (plantsOnLawn.mNormalPlant != null || plantsOnLawn.mUnderPlant != null)
							{
								return PlantingReason.PLANTING_NOT_HERE;
							}
							return PlantingReason.PLANTING_OK;
						}
						else if (Plant.IsFlying(theType))
						{
							if (plantsOnLawn.mFlyingPlant != null)
							{
								return PlantingReason.PLANTING_NOT_HERE;
							}
							return PlantingReason.PLANTING_OK;
						}
						else
						{
							if ((theType == SeedType.SEED_SPIKEWEED || theType == SeedType.SEED_SPIKEROCK) && (this.mGridSquareType[theGridX, theGridY] == GridSquareType.GRIDSQUARE_POOL || this.StageHasRoof() || plantsOnLawn.mUnderPlant != null))
							{
								return PlantingReason.PLANTING_NEEDS_GROUND;
							}
							if (this.mGridSquareType[theGridX, theGridY] == GridSquareType.GRIDSQUARE_POOL && !flag2 && theType != SeedType.SEED_CATTAIL && (plantsOnLawn.mNormalPlant == null || plantsOnLawn.mNormalPlant.mSeedType != SeedType.SEED_CATTAIL || theType != SeedType.SEED_PUMPKINSHELL))
							{
								return PlantingReason.PLANTING_NOT_ON_WATER;
							}
							if (theType == SeedType.SEED_FLOWERPOT)
							{
								if (plantsOnLawn.mNormalPlant != null || plantsOnLawn.mUnderPlant != null || plantsOnLawn.mPumpkinPlant != null)
								{
									return PlantingReason.PLANTING_NOT_HERE;
								}
								return PlantingReason.PLANTING_OK;
							}
							else
							{
								if (this.StageHasRoof() && !flag3)
								{
									return PlantingReason.PLANTING_NEEDS_POT;
								}
								if (theType == SeedType.SEED_PUMPKINSHELL)
								{
									if (plantsOnLawn.mNormalPlant != null && plantsOnLawn.mNormalPlant.mSeedType == SeedType.SEED_COBCANNON)
									{
										return PlantingReason.PLANTING_NOT_HERE;
									}
									if (plantsOnLawn.mPumpkinPlant == null)
									{
										return PlantingReason.PLANTING_OK;
									}
									if (this.mApp.mPlayerInfo.mPurchases[29] != 0 && plantsOnLawn.mPumpkinPlant.mPlantHealth < plantsOnLawn.mPumpkinPlant.mPlantMaxHealth * 2 / 3 && theType == plantsOnLawn.mPumpkinPlant.mSeedType && plantsOnLawn.mPumpkinPlant.mOnBungeeState != PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE)
									{
										return PlantingReason.PLANTING_OK;
									}
									return PlantingReason.PLANTING_NOT_HERE;
								}
								else
								{
									if (flag2 && theType == SeedType.SEED_POTATOMINE)
									{
										return PlantingReason.PLANTING_ONLY_ON_GROUND;
									}
									if (plantsOnLawn.mUnderPlant != null && theType == SeedType.SEED_CATTAIL)
									{
										if (plantsOnLawn.mNormalPlant != null)
										{
											return PlantingReason.PLANTING_NOT_HERE;
										}
										if (plantsOnLawn.mUnderPlant.IsUpgradableTo(theType) && plantsOnLawn.mUnderPlant.mOnBungeeState != PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE)
										{
											return PlantingReason.PLANTING_OK;
										}
										if (Plant.IsUpgrade(theType))
										{
											return PlantingReason.PLANTING_NEEDS_UPGRADE;
										}
									}
									if (plantsOnLawn.mUnderPlant != null && plantsOnLawn.mUnderPlant.mSeedType == SeedType.SEED_IMITATER)
									{
										return PlantingReason.PLANTING_NOT_HERE;
									}
									if (plantsOnLawn.mNormalPlant == null)
									{
										if (!this.mApp.mEasyPlantingCheat)
										{
											if (Plant.IsUpgrade(theType))
											{
												return PlantingReason.PLANTING_NEEDS_UPGRADE;
											}
										}
										else
										{
											if (theType == SeedType.SEED_COBCANNON && !this.IsValidCobCannonSpot(theGridX, theGridY))
											{
												return PlantingReason.PLANTING_NEEDS_UPGRADE;
											}
											if (theType == SeedType.SEED_CATTAIL && !this.IsPoolSquare(theGridX, theGridY))
											{
												return PlantingReason.PLANTING_NOT_HERE;
											}
										}
										return PlantingReason.PLANTING_OK;
									}
									if (plantsOnLawn.mNormalPlant.IsUpgradableTo(theType) && plantsOnLawn.mNormalPlant.mOnBungeeState != PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE)
									{
										return PlantingReason.PLANTING_OK;
									}
									if (Plant.IsUpgrade(theType))
									{
										return PlantingReason.PLANTING_NEEDS_UPGRADE;
									}
									if ((theType == SeedType.SEED_WALLNUT || theType == SeedType.SEED_TALLNUT) && this.mApp.mPlayerInfo.mPurchases[29] != 0 && plantsOnLawn.mNormalPlant.mPlantHealth < plantsOnLawn.mNormalPlant.mPlantMaxHealth * 2 / 3 && theType == plantsOnLawn.mNormalPlant.mSeedType && plantsOnLawn.mNormalPlant.mOnBungeeState != PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE)
									{
										return PlantingReason.PLANTING_OK;
									}
									return PlantingReason.PLANTING_NOT_HERE;
								}
							}
						}
					}
				}
			}
		}

		public override void MouseMove(int x, int y)
		{
			base.MouseMove(x, y);
			this.mChallenge.MouseMove(x, y);
		}

		public override void MouseDrag(int x, int y)
		{
			base.MouseDrag(x, y);
			if (this.mIgnoreMouseUp && this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_USABLE_COIN)
			{
				this.mIgnoreMouseUp = false;
			}
			this.mLastToolX = x;
			this.mLastToolY = y;
			if (this.mIgnoreNextMouseUp)
			{
				TRect trect = new TRect(Constants.ZEN_XMIN, Constants.ZEN_YMIN, Constants.ZEN_XMAX - Constants.ZEN_XMIN, Constants.ZEN_YMAX - Constants.ZEN_YMIN);
				if (trect.Contains(this.mApp.mBoard.mLastToolX, this.mApp.mBoard.mLastToolY))
				{
					this.mIgnoreNextMouseUp = false;
				}
			}
			this.mChallenge.MouseMove(x, y);
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			base.MouseDown(x, y, theClickCount);
			this.mLastToolX = x;
			this.mLastToolY = y;
			this.mMenuButton.Update();
			if (this.mStoreButton != null)
			{
				this.mStoreButton.Update();
			}
			this.mIgnoreMouseUp = !this.CanInteractWithBoardButtons();
			if (this.mTimeStopCounter > 0)
			{
				return;
			}
			HitResult theHitResult;
			this.MouseHitTest(x, y, out theHitResult, false);
			if (this.mChallenge.MouseDown(x, y, theClickCount, theHitResult))
			{
				return;
			}
			if (this.mMenuButton.IsMouseOver() && this.CanInteractWithBoardButtons() && theClickCount > 0)
			{
				this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
			}
			else if (this.mStoreButton != null && this.mStoreButton.IsMouseOver() && this.CanInteractWithBoardButtons() && theClickCount > 0)
			{
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
				{
					this.mApp.PlaySample(Resources.SOUND_TAP);
				}
				else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND || this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
				{
					this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
				}
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO && this.mApp.mSeedChooserScreen != null)
			{
				this.mApp.mSeedChooserScreen.CancelLawnView();
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
			{
				this.mCutScene.ZombieWonClick();
				return;
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO)
			{
				this.mCutScene.MouseDown(x, y);
			}
			if (this.mApp.mTodCheatKeys && !this.mApp.IsScaryPotterLevel() && this.mNextSurvivalStageCounter > 0)
			{
				this.mNextSurvivalStageCounter = 2;
				for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
				{
					if (this.mIceTimer[i] > this.mNextSurvivalStageCounter)
					{
						this.mIceTimer[i] = this.mNextSurvivalStageCounter;
					}
				}
			}
			if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_COIN && theClickCount >= 0)
			{
				Coin coin = (Coin)theHitResult.mObject;
				coin.MouseDown(x, y, theClickCount);
				this.mIgnoreMouseUp = true;
				return;
			}
			if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_WATERING_CAN || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_FERTILIZER || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_BUG_SPRAY || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PHONOGRAPH || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_CHOCOLATE || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_GLOVE || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_MONEY_SIGN || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_WHEEELBARROW || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_TREE_FOOD)
			{
				this.MouseDownWithTool(x, y, theClickCount, this.mCursorObject.mCursorType, false);
				return;
			}
			if (this.IsPlantInCursor())
			{
				return;
			}
			if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_SEEDPACKET)
			{
				this.RefreshSeedPacketFromCursor();
				SeedPacket seedPacket = (SeedPacket)theHitResult.mObject;
				seedPacket.MouseDown(x, y, theClickCount);
				return;
			}
			if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_NEXT_GARDEN)
			{
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
				{
					this.mApp.mZenGarden.GotoNextGarden();
				}
				else if (this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
				{
					this.mChallenge.TreeOfWisdomNextGarden();
				}
				this.mApp.PlaySample(Resources.SOUND_TAP);
				return;
			}
			if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_SHOVEL || theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_WATERING_CAN || theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_FERTILIZER || theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_BUG_SPRAY || theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_PHONOGRAPH || theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_CHOCOLATE || theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_GLOVE || theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_MONEY_SIGN || theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_WHEELBARROW || theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_TREE_FOOD)
			{
				if (this.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_SHOVEL)
				{
					this.RefreshSeedPacketFromCursor();
				}
				this.PickUpTool(theHitResult.mObjectType);
				return;
			}
			if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
			{
				Plant plant = (Plant)theHitResult.mObject;
				plant.MouseDown(x, y, theClickCount);
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			base.MouseUp(x, y, theClickCount);
			if (this.mIgnoreMouseUp)
			{
				this.mLastToolX = (this.mLastToolY = -1);
				this.mLastToolX = 0;
				return;
			}
			if (this.mIgnoreNextMouseUp)
			{
				this.mIgnoreNextMouseUp = false;
				return;
			}
			if (this.mIgnoreNextMouseUpSeedPacket)
			{
				HitResult hitResult;
				this.MouseHitTest(x, y, out hitResult, false);
				this.mIgnoreNextMouseUpSeedPacket = false;
				if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_COIN)
				{
					Coin coin = (Coin)hitResult.mObject;
					if (coin.mType == CoinType.COIN_USABLE_SEED_PACKET)
					{
						return;
					}
				}
			}
			if (this.mMenuButton.IsMouseOver() && this.CanInteractWithBoardButtons() && theClickCount > 0)
			{
				this.RefreshSeedPacketFromCursor();
				if (this.mApp.GetDialog(Dialogs.DIALOG_GAME_OVER) != null || this.mApp.GetDialog(Dialogs.DIALOG_LEVEL_COMPLETE) != null)
				{
					return;
				}
				this.mMenuButton.mIsOver = false;
				this.mMenuButton.mIsDown = false;
				this.UpdateCursor();
				if (this.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_COMPLETED)
				{
					this.mApp.FinishZenGardenTutorial();
					return;
				}
				if (this.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_COMPLETED)
				{
					this.mApp.mBoardResult = BoardResult.BOARDRESULT_WON;
					this.mApp.KillBoard();
					this.mApp.PreNewGame(GameMode.GAMEMODE_ADVENTURE, false);
					return;
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM || this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
				{
					this.mApp.mPlayerInfo.mNeedsTrialLevelReset = true;
					this.mApp.mBoardResult = BoardResult.BOARDRESULT_QUIT;
					this.mApp.DoBackToMain();
					return;
				}
				this.mApp.PlaySample(Resources.SOUND_PAUSE);
				this.mApp.DoNewOptions(false);
				return;
			}
			else
			{
				if (((float)this.mLastToolX >= (float)Constants.LAWN_XMIN * Constants.S || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN) && this.IsPlantInCursor())
				{
					if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_GLOVE && this.mCursorObject.mGlovePlantID != null)
					{
						this.mCursorObject.mGlovePlantID.mGloveGrabbed = false;
					}
					this.MouseUpWithPlant(this.mLastToolX, this.mLastToolY, theClickCount);
					return;
				}
				if (((float)this.mLastToolX < (float)Constants.LAWN_XMIN * Constants.S || (float)this.mLastToolY >= (float)Constants.LAWN_YMIN * Constants.S) && this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_SHOVEL)
				{
					this.MouseDownWithTool(this.mLastToolX, this.mLastToolY, theClickCount, this.mCursorObject.mCursorType, false);
					return;
				}
				if ((float)this.mLastToolX >= (float)Constants.LAWN_XMIN * Constants.S && (float)this.mLastToolY >= (float)Constants.LAWN_YMIN * Constants.S && this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_COBCANNON_TARGET)
				{
					this.MouseDownCobcannonFire(this.mLastToolX, this.mLastToolY, theClickCount);
					this.mLastToolX = (this.mLastToolY = -1);
					return;
				}
				if ((this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_FERTILIZER || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_BUG_SPRAY || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PHONOGRAPH || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_CHOCOLATE || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_GLOVE || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_MONEY_SIGN || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_WHEEELBARROW || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_TREE_FOOD || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_WATERING_CAN) && (float)this.mLastToolY >= (float)Constants.ZEN_YMIN * Constants.S)
				{
					this.MouseDownWithTool(x, y, theClickCount, this.mCursorObject.mCursorType, false);
					return;
				}
				if (this.mChallenge.MouseUp(this.mLastToolX, this.mLastToolY) && theClickCount > 0)
				{
					return;
				}
				if (this.mStoreButton != null && this.mStoreButton.IsMouseOver() && this.CanInteractWithBoardButtons() && theClickCount > 0)
				{
					if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
					{
						this.ClearAdviceImmediately();
						this.mApp.mZenGarden.OpenStore();
					}
					if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
					{
						this.mChallenge.mChallengeState = ChallengeState.STATECHALLENGE_LAST_STAND_ONSLAUGHT;
						this.mStoreButton.mBtnNoDraw = true;
						this.mStoreButton.mDisabled = true;
						this.mZombieCountDown = 9;
						this.mZombieCountDownStart = this.mZombieCountDown;
					}
					else if (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
					{
						this.mApp.BuyGame();
						this.mApp.DoBackToMain();
					}
				}
				this.mLastToolX = (this.mLastToolY = -1);
				return;
			}
		}

		public override void KeyChar(SexyChar theChar)
		{
			char value_type = theChar.value_type;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				if (value_type == 'm')
				{
					if (this.mApp.mZenGarden.IsZenGardenFull(true))
					{
						return;
					}
					PottedPlant pottedPlant = new PottedPlant();
					pottedPlant.InitializePottedPlant(SeedType.SEED_MARIGOLD);
					pottedPlant.mPlantAge = PottedPlantAge.PLANTAGE_FULL;
					pottedPlant.mDrawVariation = (DrawVariation)TodCommon.RandRangeInt(2, 12);
					this.mApp.mZenGarden.AddPottedPlant(pottedPlant);
					return;
				}
				else
				{
					if (value_type == '+')
					{
						while (!this.mApp.mZenGarden.IsZenGardenFull(true))
						{
							SeedType theSeedType = this.mApp.mZenGarden.PickRandomSeedType();
							PottedPlant pottedPlant2 = new PottedPlant();
							pottedPlant2.InitializePottedPlant(theSeedType);
							pottedPlant2.mPlantAge = PottedPlantAge.PLANTAGE_FULL;
							this.mApp.mZenGarden.AddPottedPlant(pottedPlant2);
						}
						return;
					}
					if (value_type == '-')
					{
						this.mPlants.Clear();
						this.mApp.mPlayerInfo.mNumPottedPlants = 0;
						return;
					}
					if (value_type == 'a')
					{
						if (this.mApp.mZenGarden.IsZenGardenFull(true))
						{
							return;
						}
						SeedType theSeedType2 = this.mApp.mZenGarden.PickRandomSeedType();
						PottedPlant pottedPlant3 = new PottedPlant();
						pottedPlant3.InitializePottedPlant(theSeedType2);
						pottedPlant3.mPlantAge = PottedPlantAge.PLANTAGE_SMALL;
						this.mApp.mZenGarden.AddPottedPlant(pottedPlant3);
						return;
					}
					else
					{
						if (value_type == 'f')
						{
							int num = -1;
							Plant plant = null;
							while (this.IteratePlants(ref plant, ref num))
							{
								if (this.GetZenToolAt(plant.mPlantCol, plant.mRow) == null && plant.mPottedPlantIndex >= 0)
								{
									PottedPlant thePottedPlant = this.mApp.mZenGarden.PottedPlantFromIndex(plant.mPottedPlantIndex);
									PottedPlantNeed plantsNeed = this.mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
									if (plantsNeed == PottedPlantNeed.PLANTNEED_WATER)
									{
										plant.mHighlighted = true;
										this.mApp.mZenGarden.MouseDownWithFeedingTool(plant.mX, plant.mY, CursorType.CURSOR_TYPE_WATERING_CAN);
										return;
									}
									if (plantsNeed == PottedPlantNeed.PLANTNEED_FERTILIZER)
									{
										plant.mHighlighted = true;
										if (this.mApp.mPlayerInfo.mPurchases[14] <= 1000)
										{
											this.mApp.mPlayerInfo.mPurchases[14] = 1001;
										}
										this.mApp.mZenGarden.MouseDownWithFeedingTool(plant.mX, plant.mY, CursorType.CURSOR_TYPE_FERTILIZER);
										return;
									}
									if (plantsNeed == PottedPlantNeed.PLANTNEED_BUGSPRAY)
									{
										plant.mHighlighted = true;
										if (this.mApp.mPlayerInfo.mPurchases[15] <= 1000)
										{
											this.mApp.mPlayerInfo.mPurchases[15] = 1001;
										}
										this.mApp.mZenGarden.MouseDownWithFeedingTool(plant.mX, plant.mY, CursorType.CURSOR_TYPE_BUG_SPRAY);
										return;
									}
									if (plantsNeed == PottedPlantNeed.PLANTNEED_PHONOGRAPH)
									{
										plant.mHighlighted = true;
										this.mApp.mZenGarden.MouseDownWithFeedingTool(plant.mX, plant.mY, CursorType.CURSOR_TYPE_PHONOGRAPH);
										return;
									}
								}
							}
							return;
						}
						if (value_type == 'r')
						{
							int num2 = -1;
							Plant plant2 = null;
							while (this.IteratePlants(ref plant2, ref num2))
							{
								if (plant2.mPottedPlantIndex >= 0)
								{
									Debug.ASSERT(plant2.mPottedPlantIndex < this.mApp.mPlayerInfo.mNumPottedPlants);
									PottedPlant thePottedPlant2 = this.mApp.mPlayerInfo.mPottedPlant[plant2.mPottedPlantIndex];
									this.mApp.mZenGarden.ResetPlantTimers(thePottedPlant2);
								}
							}
							return;
						}
						if (value_type == 's')
						{
							if (this.mApp.mZenGarden.IsStinkySleeping())
							{
								this.mApp.mZenGarden.WakeStinky();
								return;
							}
							this.mApp.mZenGarden.ResetStinkyTimers();
							return;
						}
						else if (value_type == 'c')
						{
							if (this.mApp.mPlayerInfo.mPurchases[26] < 1000)
							{
								this.mApp.mPlayerInfo.mPurchases[26] = 1001;
								return;
							}
							this.mApp.mPlayerInfo.mPurchases[26]++;
							return;
						}
						else if (value_type == ']')
						{
							PottedPlant pottedPlantInWheelbarrow = this.mApp.mZenGarden.GetPottedPlantInWheelbarrow();
							if (pottedPlantInWheelbarrow != null)
							{
								pottedPlantInWheelbarrow.mSeedType++;
								if (pottedPlantInWheelbarrow.mSeedType == SeedType.SEED_GATLINGPEA)
								{
									pottedPlantInWheelbarrow.mSeedType = SeedType.SEED_PEASHOOTER;
								}
								if (pottedPlantInWheelbarrow.mSeedType == SeedType.SEED_FLOWERPOT)
								{
									pottedPlantInWheelbarrow.mSeedType++;
								}
							}
							return;
						}
					}
				}
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				if (value_type == 'f')
				{
					if (this.mApp.mPlayerInfo.mPurchases[28] <= 1000)
					{
						this.mApp.mPlayerInfo.mPurchases[28] = 1001;
					}
					this.mChallenge.TreeOfWisdomFertilize();
					return;
				}
				if (value_type == 'g')
				{
					this.mChallenge.TreeOfWisdomGrow();
					return;
				}
				if (value_type == 'b')
				{
					this.mChallenge.mChallengeStateCounter = 1;
					return;
				}
				if (value_type == '0')
				{
					int currentChallengeIndex = this.mApp.GetCurrentChallengeIndex();
					this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex] = 0;
					this.mChallenge.TreeOfWisdomGrow();
					return;
				}
				if (value_type == '1')
				{
					int currentChallengeIndex2 = this.mApp.GetCurrentChallengeIndex();
					this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex2] = 9;
					this.mChallenge.TreeOfWisdomGrow();
					return;
				}
				if (value_type == '2')
				{
					int currentChallengeIndex3 = this.mApp.GetCurrentChallengeIndex();
					this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex3] = 19;
					this.mChallenge.TreeOfWisdomGrow();
					return;
				}
				if (value_type == '3')
				{
					int currentChallengeIndex4 = this.mApp.GetCurrentChallengeIndex();
					this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex4] = 29;
					this.mChallenge.TreeOfWisdomGrow();
					return;
				}
				if (value_type == '4')
				{
					int currentChallengeIndex5 = this.mApp.GetCurrentChallengeIndex();
					this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex5] = 39;
					this.mChallenge.TreeOfWisdomGrow();
					return;
				}
				if (value_type == '5')
				{
					int currentChallengeIndex6 = this.mApp.GetCurrentChallengeIndex();
					this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex6] = 49;
					this.mChallenge.TreeOfWisdomGrow();
					return;
				}
				if (value_type == '6')
				{
					int currentChallengeIndex7 = this.mApp.GetCurrentChallengeIndex();
					this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex7] = 98;
					this.mChallenge.TreeOfWisdomGrow();
					return;
				}
				if (value_type == '7')
				{
					int currentChallengeIndex8 = this.mApp.GetCurrentChallengeIndex();
					this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex8] = 498;
					this.mChallenge.TreeOfWisdomGrow();
					return;
				}
				if (value_type == '8')
				{
					int currentChallengeIndex9 = this.mApp.GetCurrentChallengeIndex();
					this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex9] = 998;
					this.mChallenge.TreeOfWisdomGrow();
				}
				return;
			}
			else
			{
				Zombie bossZombie;
				if (value_type == '<')
				{
					this.mApp.DoNewOptions(false);
				}
				else if (value_type == '2')
				{
					this.AddZombie(ZombieType.ZOMBIE_DIGGER, GameConstants.ZOMBIE_WAVE_DEBUG);
				}
				else if (value_type == 'l')
				{
					this.mApp.DoCheatDialog();
				}
				else if (value_type == '#')
				{
					if (this.mApp.IsSurvivalMode())
					{
						if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO)
						{
							return;
						}
						this.mCurrentWave = this.mNumWaves;
						this.mChallenge.mSurvivalStage += 5;
						this.RemoveAllZombies();
						this.FadeOutLevel();
					}
					else
					{
						bossZombie = this.GetBossZombie();
						if (bossZombie != null)
						{
							bossZombie.ApplyBossSmokeParticles(true);
						}
					}
				}
				else if (value_type == '!')
				{
					this.mApp.mBoardResult = BoardResult.BOARDRESULT_CHEAT;
					if (this.IsLastStandStageWithRepick())
					{
						if (this.mNextSurvivalStageCounter == 0)
						{
							this.mCurrentWave = this.mNumWaves;
							this.RemoveAllZombies();
							this.FadeOutLevel();
						}
					}
					else if ((this.mApp.IsScaryPotterLevel() && !this.IsFinalScaryPotterStage()) || this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
					{
						if (this.mNextSurvivalStageCounter == 0)
						{
							this.RemoveAllZombies();
							this.FadeOutLevel();
						}
					}
					else if (this.mApp.IsSurvivalMode())
					{
						if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO)
						{
							return;
						}
						this.mCurrentWave = this.mNumWaves;
						if (!this.IsSurvivalStageWithRepick())
						{
							this.RemoveAllZombies();
						}
						this.FadeOutLevel();
					}
					else
					{
						this.RemoveAllZombies();
						this.FadeOutLevel();
						this.mBoardFadeOutCounter = 200;
					}
				}
				else if (value_type == '+')
				{
					this.mApp.mBoardResult = BoardResult.BOARDRESULT_CHEAT;
					if (this.IsLastStandStageWithRepick())
					{
						if (this.mNextSurvivalStageCounter == 0)
						{
							this.mCurrentWave = this.mNumWaves;
							this.RemoveAllZombies();
							this.FadeOutLevel();
						}
					}
					else if ((this.mApp.IsScaryPotterLevel() && !this.IsFinalScaryPotterStage()) || this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
					{
						if (this.mNextSurvivalStageCounter == 0)
						{
							this.RemoveAllZombies();
							this.FadeOutLevel();
						}
					}
					else if (this.mApp.IsSurvivalEndless(this.mApp.mGameMode))
					{
						if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO)
						{
							return;
						}
						this.mCurrentWave = this.mNumWaves;
						this.RemoveAllZombies();
						this.FadeOutLevel();
					}
					else if (this.mApp.IsSurvivalMode())
					{
						this.mChallenge.mSurvivalStage = 5;
						this.RemoveAllZombies();
						this.FadeOutLevel();
						this.mBoardFadeOutCounter = 200;
					}
					else
					{
						this.RemoveAllZombies();
						this.FadeOutLevel();
						this.mBoardFadeOutCounter = 200;
					}
				}
				else if (value_type == '8')
				{
					this.mApp.mEasyPlantingCheat = !this.mApp.mEasyPlantingCheat;
				}
				else if (value_type == '7')
				{
					this.mApp.ToggleSlowMo();
				}
				else if (value_type == '6')
				{
					this.mApp.ToggleFastMo();
				}
				else if (value_type == 'z')
				{
					this.ClearAdviceImmediately();
					this.DisplayAdviceAgain("[ADVICE_HUGE_WAVE]", MessageStyle.MESSAGE_STYLE_HUGE_WAVE, AdviceType.ADVICE_HUGE_WAVE);
					this.mHugeWaveCountDown = 750;
					this.mDebugTextMode++;
					if (this.mDebugTextMode > DebugTextMode.DEBUG_TEXT_COLLISION)
					{
						this.mDebugTextMode = DebugTextMode.DEBUG_TEXT_NONE;
					}
				}
				if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
				{
					return;
				}
				bossZombie = this.GetBossZombie();
				if (bossZombie != null && !bossZombie.IsDeadOrDying())
				{
					if (value_type == 'b')
					{
						bossZombie.mBossBungeeCounter = 0;
						return;
					}
					if (value_type == 'u')
					{
						bossZombie.mSummonCounter = 0;
						return;
					}
					if (value_type == 's')
					{
						bossZombie.mBossStompCounter = 0;
						return;
					}
					if (value_type == 'r')
					{
						bossZombie.BossRVAttack();
						return;
					}
					if (value_type == 'h')
					{
						bossZombie.mBossHeadCounter = 0;
						return;
					}
					if (value_type == 'd')
					{
						bossZombie.TakeDamage(10000, 0U);
						return;
					}
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS_2)
				{
					if (value_type == 'w')
					{
						this.AddZombie(ZombieType.ZOMBIE_WALLNUT_HEAD, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 't')
					{
						this.AddZombie(ZombieType.ZOMBIE_TALLNUT_HEAD, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 'j')
					{
						this.AddZombie(ZombieType.ZOMBIE_JALAPENO_HEAD, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 'g')
					{
						this.AddZombie(ZombieType.ZOMBIE_GATLING_HEAD, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 's')
					{
						this.AddZombie(ZombieType.ZOMBIE_SQUASH_HEAD, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
				}
				if (value_type == 'q' && this.mApp.IsSurvivalEndless(this.mApp.mGameMode))
				{
					this.mApp.mEasyPlantingCheat = true;
					for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
					{
						for (int j = 0; j < Constants.GRIDSIZEX; j++)
						{
							if (this.CanPlantAt(j, i, SeedType.SEED_LILYPAD) == PlantingReason.PLANTING_OK)
							{
								this.AddPlant(j, i, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
							}
							if (this.CanPlantAt(j, i, SeedType.SEED_PUMPKINSHELL) == PlantingReason.PLANTING_OK && (j <= 6 || this.IsPoolSquare(j, i)))
							{
								this.AddPlant(j, i, SeedType.SEED_PUMPKINSHELL, SeedType.SEED_NONE);
							}
							if (this.CanPlantAt(j, i, SeedType.SEED_GATLINGPEA) == PlantingReason.PLANTING_OK)
							{
								if (j < 5)
								{
									this.AddPlant(j, i, SeedType.SEED_GATLINGPEA, SeedType.SEED_NONE);
								}
								else if (j == 5)
								{
									this.AddPlant(j, i, SeedType.SEED_TORCHWOOD, SeedType.SEED_NONE);
								}
								else if (j == 6)
								{
									this.AddPlant(j, i, SeedType.SEED_SPLITPEA, SeedType.SEED_NONE);
								}
								else if (i == 2 || i == 3)
								{
									this.AddPlant(j, i, SeedType.SEED_GLOOMSHROOM, SeedType.SEED_NONE);
									if (this.CanPlantAt(j, i, SeedType.SEED_INSTANT_COFFEE) == PlantingReason.PLANTING_OK)
									{
										this.AddPlant(j, i, SeedType.SEED_INSTANT_COFFEE, SeedType.SEED_NONE);
									}
								}
							}
						}
					}
					return;
				}
				if (value_type == 'q' && this.mApp.IsIZombieLevel())
				{
					this.mApp.mEasyPlantingCheat = true;
					if (this.mApp.IsIZombieLevel())
					{
						for (int k = 0; k < 5; k++)
						{
							this.mChallenge.IZombiePlaceZombie(ZombieType.ZOMBIE_FOOTBALL, 6, k);
						}
						return;
					}
				}
				else if (value_type == 'q')
				{
					this.mApp.mEasyPlantingCheat = true;
					for (int l = 0; l < Constants.MAX_GRIDSIZEY; l++)
					{
						for (int m = 0; m < Constants.GRIDSIZEX; m++)
						{
							if (this.StageHasRoof() && this.CanPlantAt(m, l, SeedType.SEED_FLOWERPOT) == PlantingReason.PLANTING_OK)
							{
								this.AddPlant(m, l, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
							}
							if (this.CanPlantAt(m, l, SeedType.SEED_LILYPAD) == PlantingReason.PLANTING_OK)
							{
								this.AddPlant(m, l, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
							}
							if (this.CanPlantAt(m, l, SeedType.SEED_THREEPEATER) == PlantingReason.PLANTING_OK)
							{
								this.AddPlant(m, l, SeedType.SEED_THREEPEATER, SeedType.SEED_NONE);
							}
						}
					}
					if (!this.mChallenge.UpdateZombieSpawning())
					{
						int num3 = Math.Min(this.mNumWaves - this.mCurrentWave, 20);
						for (int n = 0; n < num3; n++)
						{
							this.SpawnZombieWave();
						}
					}
					if (this.mApp.IsScaryPotterLevel())
					{
						int num4 = -1;
						GridItem gridItem = null;
						while (this.IterateGridItems(ref gridItem, ref num4))
						{
							if (gridItem.mGridItemType == GridItemType.GRIDITEM_SCARY_POT)
							{
								this.mChallenge.ScaryPotterOpenPot(gridItem);
							}
						}
						return;
					}
				}
				else if (value_type == ']')
				{
					this.mApp.mEasyPlantingCheat = true;
					for (int num5 = 0; num5 < Constants.MAX_GRIDSIZEY; num5++)
					{
						for (int num6 = 0; num6 < Constants.GRIDSIZEX; num6++)
						{
							if (this.StageHasRoof() && this.CanPlantAt(num6, num5, SeedType.SEED_FLOWERPOT) == PlantingReason.PLANTING_OK)
							{
								this.AddPlant(num6, num5, SeedType.SEED_FLOWERPOT, SeedType.SEED_NONE);
							}
							if (this.CanPlantAt(num6, num5, SeedType.SEED_LILYPAD) == PlantingReason.PLANTING_OK)
							{
								this.AddPlant(num6, num5, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
							}
							if (this.CanPlantAt(num6, num5, SeedType.SEED_PEASHOOTER) == PlantingReason.PLANTING_OK)
							{
								this.AddPlant(num6, num5, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
							}
						}
					}
					if (!this.mChallenge.UpdateZombieSpawning())
					{
						int num7 = Math.Min(this.mNumWaves - this.mCurrentWave, 20);
						for (int num8 = 0; num8 < num7; num8++)
						{
							this.SpawnZombieWave();
						}
					}
					if (this.mApp.IsScaryPotterLevel())
					{
						int num9 = -1;
						GridItem gridItem2 = null;
						while (this.IterateGridItems(ref gridItem2, ref num9))
						{
							if (gridItem2.mGridItemType == GridItemType.GRIDITEM_SCARY_POT)
							{
								this.mChallenge.ScaryPotterOpenPot(gridItem2);
							}
						}
						return;
					}
				}
				else if (value_type == '[')
				{
					if (!this.mChallenge.UpdateZombieSpawning())
					{
						int num10 = 1;
						for (int num11 = 0; num11 < num10; num11++)
						{
							this.SpawnZombieWave();
						}
						return;
					}
				}
				else if (value_type == '?' || value_type == '/')
				{
					if (this.mHugeWaveCountDown > 0)
					{
						this.mHugeWaveCountDown = 1;
						return;
					}
					this.mZombieCountDown = 6;
					return;
				}
				else if (value_type == 'b')
				{
					if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
					{
						this.AddZombie(ZombieType.ZOMBIE_BUNGEE, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
				}
				else
				{
					if (value_type == 'O')
					{
						this.AddZombie(ZombieType.ZOMBIE_FOOTBALL, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 's')
					{
						this.AddZombie(ZombieType.ZOMBIE_DOOR, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 'L')
					{
						this.AddZombie(ZombieType.ZOMBIE_LADDER, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 'y')
					{
						this.AddZombie(ZombieType.ZOMBIE_YETI, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 'a')
					{
						this.AddZombie(ZombieType.ZOMBIE_FLAG, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 'w')
					{
						this.AddZombie(ZombieType.ZOMBIE_NEWSPAPER, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 'F')
					{
						this.AddZombie(ZombieType.ZOMBIE_BALLOON, GameConstants.ZOMBIE_WAVE_DEBUG);
						return;
					}
					if (value_type == 'n')
					{
						if (this.StageHasPool())
						{
							this.AddZombie(ZombieType.ZOMBIE_SNORKEL, GameConstants.ZOMBIE_WAVE_DEBUG);
							return;
						}
					}
					else
					{
						if (value_type == 'c')
						{
							this.AddZombie(ZombieType.ZOMBIE_TRAFFIC_CONE, GameConstants.ZOMBIE_WAVE_DEBUG);
							return;
						}
						if (value_type == 'm')
						{
							this.AddZombie(ZombieType.ZOMBIE_DANCER, GameConstants.ZOMBIE_WAVE_DEBUG);
							return;
						}
						if (value_type == '*')
						{
							Console.Write("CRASHING ON PURPOSE\n");
							Debug.ASSERT(false);
							return;
						}
						if (value_type == ' ')
						{
							this.mApp.DoPauseDialog();
							return;
						}
						if (value_type == 'h')
						{
							this.AddZombie(ZombieType.ZOMBIE_PAIL, GameConstants.ZOMBIE_WAVE_DEBUG);
							return;
						}
						if (value_type == 'H')
						{
							this.AddZombie(ZombieType.ZOMBIE_PAIL, GameConstants.ZOMBIE_WAVE_DEBUG);
							this.AddZombie(ZombieType.ZOMBIE_PAIL, GameConstants.ZOMBIE_WAVE_DEBUG);
							this.AddZombie(ZombieType.ZOMBIE_PAIL, GameConstants.ZOMBIE_WAVE_DEBUG);
							this.AddZombie(ZombieType.ZOMBIE_PAIL, GameConstants.ZOMBIE_WAVE_DEBUG);
							this.AddZombie(ZombieType.ZOMBIE_PAIL, GameConstants.ZOMBIE_WAVE_DEBUG);
							return;
						}
						if (value_type == 'D')
						{
							this.AddZombie(ZombieType.ZOMBIE_DIGGER, GameConstants.ZOMBIE_WAVE_DEBUG);
							return;
						}
						if (value_type == 'p')
						{
							this.AddZombie(ZombieType.ZOMBIE_POLEVAULTER, GameConstants.ZOMBIE_WAVE_DEBUG);
							return;
						}
						if (value_type == 'P')
						{
							this.AddZombie(ZombieType.ZOMBIE_POGO, GameConstants.ZOMBIE_WAVE_DEBUG);
							return;
						}
						if (value_type == 'R')
						{
							if (this.StageHasPool())
							{
								this.AddZombie(ZombieType.ZOMBIE_DOLPHIN_RIDER, GameConstants.ZOMBIE_WAVE_DEBUG);
								return;
							}
						}
						else
						{
							if (value_type == 'j')
							{
								this.AddZombie(ZombieType.ZOMBIE_JACK_IN_THE_BOX, GameConstants.ZOMBIE_WAVE_DEBUG);
								return;
							}
							if (value_type == 'g')
							{
								this.AddZombie(ZombieType.ZOMBIE_GARGANTUAR, GameConstants.ZOMBIE_WAVE_DEBUG);
								return;
							}
							if (value_type == 'G')
							{
								this.AddZombie(ZombieType.ZOMBIE_REDEYE_GARGANTUAR, GameConstants.ZOMBIE_WAVE_DEBUG);
								return;
							}
							if (value_type == 'i')
							{
								this.AddZombie(ZombieType.ZOMBIE_ZAMBONI, GameConstants.ZOMBIE_WAVE_DEBUG);
								return;
							}
							if (value_type == 'C')
							{
								this.AddZombie(ZombieType.ZOMBIE_CATAPULT, GameConstants.ZOMBIE_WAVE_DEBUG);
								return;
							}
							if (value_type == '1')
							{
								Plant topPlantAt = this.GetTopPlantAt(0, 0, PlantPriority.TOPPLANT_ANY);
								if (topPlantAt != null)
								{
									topPlantAt.Die();
									this.mChallenge.ZombieAtePlant(null, topPlantAt);
									return;
								}
							}
							else
							{
								if (value_type == 'B')
								{
									this.mFogBlownCountDown = 2200;
									return;
								}
								if (value_type == 't')
								{
									if (!this.CanAddBobSled())
									{
										int num12 = RandomNumbers.NextNumber(5);
										int num13 = 400;
										if (this.StageHasPool())
										{
											num12 = RandomNumbers.NextNumber(2);
										}
										else if (this.StageHasRoof())
										{
											num13 = 500;
										}
										this.mIceTimer[num12] = 3000;
										this.mIceMinX[num12] = num13;
									}
									this.AddZombie(ZombieType.ZOMBIE_BOBSLED, GameConstants.ZOMBIE_WAVE_DEBUG);
									return;
								}
								if (value_type == 'r')
								{
									this.SpawnZombiesFromGraves();
									return;
								}
								if (value_type == '0')
								{
									this.AddSunMoney(100);
									this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
									return;
								}
								if (value_type == '9')
								{
									this.AddSunMoney(999999);
									this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
									return;
								}
								if (value_type == '$')
								{
									this.mApp.mPlayerInfo.AddCoins(100);
									this.mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
									this.ShowCoinBank();
									return;
								}
								if (value_type == '-')
								{
									this.mSunMoney -= 100;
									if (this.mSunMoney < 0)
									{
										this.mSunMoney = 0;
										return;
									}
								}
								else
								{
									if (value_type == '%')
									{
										return;
									}
									if (value_type == 'M')
									{
										this.mLawnMowers.Clear();
									}
								}
							}
						}
					}
				}
				return;
			}
		}

		public override void KeyUp(KeyCode theKey)
		{
		}

		public override void KeyDown(KeyCode theKey)
		{
			this.DoTypingCheck(theKey);
			if ((ushort)theKey == 32 || (ushort)theKey == 13)
			{
				if (this.IsScaryPotterDaveTalking() && this.mApp.mCrazyDaveMessageIndex != -1)
				{
					this.mChallenge.AdvanceCrazyDaveDialog();
					return;
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
				{
					this.mApp.mZenGarden.AdvanceCrazyDaveDialog();
					return;
				}
			}
			if ((ushort)theKey == 32 && this.mApp.CanPauseNow())
			{
				this.mApp.PlaySample(Resources.SOUND_PAUSE);
				this.mApp.DoPauseDialog();
			}
			if (theKey == KeyCode.KEYCODE_ESCAPE)
			{
				if (this.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_NORMAL)
				{
					this.RefreshSeedPacketFromCursor();
					return;
				}
				if (this.CanInteractWithBoardButtons() && this.mApp.mGameScene != GameScenes.SCENE_ZOMBIES_WON)
				{
					this.mApp.DoNewOptions(false);
				}
			}
		}

		public override void Update()
		{
			base.Update();
			this.MarkDirty();
			this.mCutScene.Update(false);
			this.mCutScene.Update(false);
			this.mCutScene.Update(true);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mApp.mZenGarden.ZenGardenUpdate(0);
				this.mApp.mZenGarden.ZenGardenUpdate(1);
				this.mApp.mZenGarden.ZenGardenUpdate(2);
				this.mApp.UpdateCrazyDave();
			}
			if (this.IsScaryPotterDaveTalking())
			{
				this.mApp.UpdateCrazyDave();
			}
			if (this.mPaused)
			{
				this.mChallenge.Update();
				this.mChallenge.Update();
				this.mChallenge.Update();
				this.mCursorPreview.mVisible = false;
				this.mCursorObject.mVisible = false;
				return;
			}
			bool mDisabled = !this.CanInteractWithBoardButtons() || this.mIgnoreMouseUp;
			if (!this.mMenuButton.mBtnNoDraw)
			{
				this.mMenuButton.mDisabled = mDisabled;
			}
			this.mMenuButton.Update();
			if (this.mStoreButton != null)
			{
				this.mStoreButton.mDisabled = mDisabled;
				this.mStoreButton.Update();
			}
			this.mApp.mEffectSystem.Update();
			this.mAdvice.Update();
			this.UpdateTutorial();
			this.UpdateTutorial();
			this.UpdateTutorial();
			if (this.mCobCannonCursorDelayCounter > 0)
			{
				this.mCobCannonCursorDelayCounter -= 3;
			}
			if (this.mOutOfMoneyCounter > 0)
			{
				this.mOutOfMoneyCounter -= 3;
			}
			if (this.mShakeCounter > 0)
			{
				this.mShakeCounter -= 3;
				if (this.mShakeCounter == 0)
				{
					this.mX = Constants.Board_Offset_AspectRatio_Correction;
					this.mY = 0;
				}
				else
				{
					this.mX = TodCommon.TodAnimateCurve(12, 0, this.mShakeCounter, Constants.Board_Offset_AspectRatio_Correction, Constants.Board_Offset_AspectRatio_Correction - this.mShakeAmountX, TodCurves.CURVE_BOUNCE);
					this.mY = TodCommon.TodAnimateCurve(12, 0, this.mShakeCounter, 0, this.mShakeAmountY, TodCurves.CURVE_BOUNCE);
				}
			}
			if (this.mCoinBankFadeCount > 0 && this.mApp.GetDialog(Dialogs.DIALOG_PURCHASE_PACKET_SLOT) == null)
			{
				this.mCoinBankFadeCount -= 3;
			}
			if (this.mLevelFadeCount > 0)
			{
				this.mLevelFadeCount -= 3;
			}
			this.UpdateLayers();
			if (this.mTimeStopCounter > 0)
			{
				return;
			}
			this.mEffectCounter += 3;
			if (this.StageHasPool() && this.mPoolSparklyParticleID == null)
			{
				int aRenderOrder = 220000;
				TodParticleSystem theParticle = this.mApp.AddTodParticle((float)(450 + Constants.BOARD_EXTRA_ROOM), 295f, aRenderOrder, ParticleEffect.PARTICLE_POOL_SPARKLY);
				this.mPoolSparklyParticleID = this.mApp.ParticleGetID(theParticle);
			}
			this.UpdateGridItems();
			this.UpdateFwoosh();
			this.UpdateGame();
			this.UpdateFog();
			this.UpdateFog();
			this.UpdateFog();
			this.mChallenge.Update();
			this.mChallenge.Update();
			this.mChallenge.Update();
			this.UpdateLevelEndSequence();
			this.UpdateLevelEndSequence();
			this.UpdateLevelEndSequence();
			this.mPrevMouseX = this.mApp.mWidgetManager.mLastMouseX;
			this.mPrevMouseY = this.mApp.mWidgetManager.mLastMouseY;
		}

		public void UpdateLayers()
		{
			if (this.mWidgetManager != null)
			{
				this.mWidgetManager.MarkAllDirty();
			}
			for (LinkedListNode<Dialog> linkedListNode = this.mApp.mDialogList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				Dialog value = linkedListNode.Value;
				this.mWidgetManager.BringToFront(value);
				value.MarkDirty();
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mApp.GetDialog(Dialogs.DIALOG_STORE) != null || this.mApp.GetDialog(Dialogs.DIALOG_ALMANAC) != null)
			{
				return;
			}
			g.SetLinearBlend(true);
			if (this.mDrawCount == 0 || !this.mCutScene.mPreloaded)
			{
				this.ResetFPSStats();
			}
			else
			{
				int tickCount = Environment.TickCount;
				int num = this.mDrawCount - this.mIntervalDrawCountStart;
				int num2 = tickCount - this.mIntervalDrawTime;
				if (num2 > 10000)
				{
					float num3 = ((float)num * 1000f + 500f) / (float)num2;
					if (num3 < this.mMinFPS)
					{
						this.mMinFPS = num3;
					}
					this.mIntervalDrawCountStart = this.mDrawCount;
					this.mIntervalDrawTime = tickCount;
				}
			}
			this.mDrawCount++;
			this.DrawGameObjects(g);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
		}

		public void DrawBackdrop(Graphics g)
		{
			Image image = null;
			switch (this.mBackground)
			{
			case BackgroundType.BACKGROUND_1_DAY:
				image = Resources.IMAGE_BACKGROUND1;
				break;
			case BackgroundType.BACKGROUND_2_NIGHT:
				image = Resources.IMAGE_BACKGROUND2;
				break;
			case BackgroundType.BACKGROUND_3_POOL:
				image = Resources.IMAGE_BACKGROUND3;
				break;
			case BackgroundType.BACKGROUND_4_FOG:
				image = Resources.IMAGE_BACKGROUND4;
				break;
			case BackgroundType.BACKGROUND_5_ROOF:
				image = Resources.IMAGE_BACKGROUND5;
				break;
			case BackgroundType.BACKGROUND_6_BOSS:
				image = Resources.IMAGE_BACKGROUND6BOSS;
				break;
			case BackgroundType.BACKGROUND_MUSHROOM_GARDEN:
				image = Resources.IMAGE_BACKGROUND_MUSHROOMGARDEN;
				break;
			case BackgroundType.BACKGROUND_GREENHOUSE:
				image = Resources.IMAGE_BACKGROUND_GREENHOUSE;
				break;
			case BackgroundType.BACKGROUND_ZOMBIQUARIUM:
				image = Resources.IMAGE_AQUARIUM1;
				break;
			case BackgroundType.BACKGROUND_TREE_OF_WISDOM:
				image = null;
				break;
			default:
				Debug.ASSERT(false);
				break;
			}
			GameMode mGameMode = this.mApp.mGameMode;
			if (this.mLevel == 1 && this.mApp.IsFirstTimeAdventureMode())
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND1UNSODDED, -((float)Constants.BOARD_OFFSET * Constants.S), 0f);
				int theWidth = TodCommon.TodAnimateCurve(0, 950, this.mSodPosition, 0, AtlasResources.IMAGE_SOD1ROW.GetWidth(), TodCurves.CURVE_LINEAR);
				TRect theSrcRect = new TRect(0, 0, theWidth, AtlasResources.IMAGE_SOD1ROW.GetHeight());
				g.DrawImage(AtlasResources.IMAGE_SOD1ROW, (int)((float)(-(float)Constants.BOARD_OFFSET + 239) * Constants.S), (int)(265f * Constants.S), theSrcRect);
			}
			else if (((this.mLevel == 2 || this.mLevel == 3) && this.mApp.IsFirstTimeAdventureMode()) || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RESODDED)
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND1UNSODDED, (float)(-(float)Constants.BOARD_OFFSET) * Constants.S, 0f);
				g.DrawImage(AtlasResources.IMAGE_SOD1ROW, (float)(-(float)Constants.BOARD_OFFSET + 239) * Constants.S, 265f * Constants.S);
				int theWidth2 = TodCommon.TodAnimateCurve(0, 950, this.mSodPosition, 0, AtlasResources.IMAGE_SOD3ROW.GetWidth(), TodCurves.CURVE_LINEAR);
				TRect theSrcRect2 = new TRect(0, 0, theWidth2, AtlasResources.IMAGE_SOD3ROW.GetHeight());
				g.DrawImage(AtlasResources.IMAGE_SOD3ROW, (int)((float)(-(float)Constants.BOARD_OFFSET + 235) * Constants.S), (int)(149f * Constants.S), theSrcRect2);
			}
			else if (this.mLevel == 4 && this.mApp.IsFirstTimeAdventureMode())
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND1UNSODDED, (float)(-(float)Constants.BOARD_OFFSET) * Constants.S, 0f);
				g.DrawImage(AtlasResources.IMAGE_SOD3ROW, (float)(-(float)Constants.BOARD_OFFSET + 235) * Constants.S, 149f * Constants.S);
				int num = TodCommon.TodAnimateCurve(0, 950, this.mSodPosition, 0, 773, TodCurves.CURVE_LINEAR);
				TRect theSrcRect3 = new TRect((int)(232f * Constants.S), 0, (int)((float)num * Constants.S), Resources.IMAGE_BACKGROUND1.GetHeight());
				g.DrawImage(Resources.IMAGE_BACKGROUND1, (int)((float)(-(float)Constants.BOARD_OFFSET + 232) * Constants.S), 0, theSrcRect3);
			}
			else if (image != null)
			{
				if (image == Resources.IMAGE_BACKGROUND_MUSHROOMGARDEN || image == Resources.IMAGE_BACKGROUND_GREENHOUSE || image == Resources.IMAGE_AQUARIUM1)
				{
					g.DrawImage(image, -Constants.ZenGarden_Backdrop_X, 0);
				}
				else
				{
					g.DrawImage(image, (int)((float)(-(float)Constants.BOARD_OFFSET) * Constants.S), 0);
				}
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
			{
				this.DrawHouseDoorBottom(g);
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PLANT_PEASHOOTER)
			{
				Graphics @new = Graphics.GetNew(g);
				SexyColor flashingColor = TodCommon.GetFlashingColor(this.mMainCounter, 75);
				@new.SetColorizeImages(true);
				@new.SetColor(flashingColor);
				@new.DrawImage(AtlasResources.IMAGE_SOD1ROW, (float)(-(float)Constants.BOARD_OFFSET + 239) * Constants.S, 265f * Constants.S);
				@new.SetColorizeImages(false);
				@new.PrepareForReuse();
			}
			this.mChallenge.DrawBackdrop(g);
			if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO && this.StageHasGraveStones())
			{
				g.DrawImage(AtlasResources.IMAGE_NIGHT_GRAVE_GRAPHIC, Constants.InvertAndScale(640f), 40f * Constants.S);
			}
		}

		public void DrawCursorOnBackground(Graphics g)
		{
			if (this.mTimeStopCounter == 0 && (!this.mApp.IsWhackAZombieLevel() || this.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_HAMMER) && this.mIsDown && (float)this.mLastToolX >= (float)Constants.LAWN_XMIN * Constants.S && this.mCursorObject.BeginDraw(g))
			{
				this.mCursorObject.DrawGroundLayer(g);
				this.mCursorObject.EndDraw(g);
			}
		}

		public void DrawCursorOverlay(Graphics g)
		{
			if (this.mTimeStopCounter == 0 && this.mIsDown && ((float)this.mLastToolX >= (float)Constants.LAWN_XMIN * Constants.S || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN) && this.mCursorObject.BeginDraw(g))
			{
				this.mCursorObject.DrawTopLayer(g);
				this.mCursorObject.EndDraw(g);
			}
		}

		public virtual void ButtonMouseEnter(int theId)
		{
		}

		public virtual void ButtonMouseLeave(int theId)
		{
		}

		public virtual void ButtonPress(int theId)
		{
		}

		public void AddSunMoney(int theAmount)
		{
			this.mSunMoney += theAmount;
			if (this.mSunMoney > 9990)
			{
				this.mSunMoney = 9990;
			}
		}

		public bool TakeSunMoney(int theAmount)
		{
			if (theAmount <= this.mSunMoney + this.CountSunBeingCollected())
			{
				this.mSunMoney -= theAmount;
				return true;
			}
			this.mApp.PlaySample(Resources.SOUND_BUZZER);
			this.mOutOfMoneyCounter = 70;
			return false;
		}

		public bool CanTakeSunMoney(int theAmount)
		{
			return theAmount <= this.mSunMoney + this.CountSunBeingCollected();
		}

		public void Pause(bool thePause)
		{
			if (this.mPaused == thePause)
			{
				return;
			}
			if (thePause)
			{
				this.mPaused = true;
				if (this.mApp.mPlayerInfo.mCoins > 0)
				{
					this.ShowCoinBank();
				}
				this.mLevelFadeCount = 1000;
				if (this.mApp.mGameScene != GameScenes.SCENE_LEVEL_INTRO)
				{
					this.mApp.mSoundSystem.GamePause(true);
					this.mApp.mMusic.GameMusicPause(true);
					return;
				}
			}
			else
			{
				this.mPaused = false;
				this.mApp.mSoundSystem.GamePause(false);
				this.mApp.mMusic.GameMusicPause(false);
			}
		}

		public void TryToSaveGame()
		{
			this.ClearCursor();
			string savedGameName = LawnCommon.GetSavedGameName(this.mApp.mGameMode, (int)this.mApp.mPlayerInfo.mId);
			if (this.NeedSaveGame())
			{
				if (this.mBoardFadeOutCounter >= 0)
				{
					this.CompleteEndLevelSequenceForSaving();
					return;
				}
				Common.MkDir(GlobalStaticVars.GetDocumentsDir() + "userdata");
				this.mApp.mMusic.GameMusicPause(true);
				this.SaveGame(savedGameName);
				this.SurvivalSaveScore();
			}
		}

		public bool NeedSaveGame()
		{
			return this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ICE && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mApp.mGameMode != GameMode.GAMEMODE_UPSELL && this.mApp.mGameMode != GameMode.GAMEMODE_INTRO && this.mApp.mGameScene == GameScenes.SCENE_PLAYING;
		}

		public bool RowCanHaveZombies(int theRow)
		{
			return theRow >= 0 && theRow < Constants.MAX_GRIDSIZEY && ((this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RESODDED && theRow <= 4) || this.mPlantRow[theRow] != PlantRowType.PLANTROW_DIRT);
		}

		public void ProcessDeleteQueue()
		{
			for (int i = this.mPlants.Count - 1; i >= 0; i--)
			{
				if (this.mPlants[i].mDead)
				{
					this.mPlants[i].PrepareForReuse();
					this.mPlants.RemoveAt(i);
				}
			}
			for (int j = this.mZombies.Count - 1; j >= 0; j--)
			{
				if (this.mZombies[j].mDead)
				{
					if (this.mZombies[j].mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						for (int k = 0; k < 6; k++)
						{
							List<Zombie> zombiesInRow = this.GetZombiesInRow(k);
							zombiesInRow.Remove(this.mZombies[j]);
						}
					}
					else
					{
						List<Zombie> zombiesInRow2 = this.GetZombiesInRow(this.mZombies[j].mRow);
						zombiesInRow2.Remove(this.mZombies[j]);
					}
					this.mZombies[j].PrepareForReuse();
					this.mZombies.RemoveAt(j);
				}
			}
			for (int l = this.mProjectiles.Count - 1; l >= 0; l--)
			{
				if (this.mProjectiles[l].mDead)
				{
					this.mProjectiles[l].PrepareForReuse();
					this.mProjectiles.RemoveAt(l);
				}
			}
			for (int m = this.mCoins.Count - 1; m >= 0; m--)
			{
				if (this.mCoins[m].mDead)
				{
					this.mCoins.RemoveAt(m);
				}
			}
			for (int n = this.mLawnMowers.Count - 1; n >= 0; n--)
			{
				if (this.mLawnMowers[n].mDead)
				{
					this.mLawnMowers[n].PrepareForReuse();
					this.mLawnMowers.RemoveAt(n);
				}
			}
			for (int num = this.mLawnMowers.Count - 1; num >= 0; num--)
			{
				if (this.mLawnMowers[num].mDead)
				{
					this.mGridItems[num].PrepareForReuse();
					this.mGridItems.RemoveAt(num);
				}
			}
		}

		public bool ChooseSeedsOnCurrentLevel()
		{
			return !this.mApp.IsChallengeWithoutSeedBank() && !this.HasConveyorBeltSeedBank() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ICE && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mApp.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM && !this.mApp.IsIZombieLevel() && !this.mApp.IsSquirrelLevel() && !this.mApp.IsSlotMachineLevel() && ((!this.mApp.IsAdventureMode() && !this.mApp.IsQuickPlayMode()) || !this.mApp.IsFirstTimeAdventureMode() || this.mLevel > 7);
		}

		public int GetNumSeedsInBank()
		{
			if (this.mApp.IsScaryPotterLevel())
			{
				return 1;
			}
			if (this.mApp.IsWhackAZombieLevel())
			{
				return 3;
			}
			if (this.mApp.IsChallengeWithoutSeedBank())
			{
				return 0;
			}
			if (this.HasConveyorBeltSeedBank())
			{
				return 9;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE)
			{
				return 6;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				return 0;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM)
			{
				return 2;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4)
			{
				return 3;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7)
			{
				return 4;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8)
			{
				return 6;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_9)
			{
				return 8;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
			{
				return 9;
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				return 3;
			}
			int num = this.mApp.mPlayerInfo.mPurchases[21] + 6;
			int seedsAvailable = this.mApp.GetSeedsAvailable();
			if (seedsAvailable < num)
			{
				num = seedsAvailable;
			}
			return num;
		}

		public bool StageIsDayWithoutPool()
		{
			return this.mBackground == BackgroundType.BACKGROUND_1_DAY;
		}

		public bool StageIsNight()
		{
			return this.mBackground == BackgroundType.BACKGROUND_2_NIGHT || this.mBackground == BackgroundType.BACKGROUND_4_FOG || this.mBackground == BackgroundType.BACKGROUND_MUSHROOM_GARDEN || this.mBackground == BackgroundType.BACKGROUND_6_BOSS;
		}

		public bool StageHasPool()
		{
			return this.mBackground == BackgroundType.BACKGROUND_3_POOL || this.mBackground == BackgroundType.BACKGROUND_4_FOG;
		}

		public bool StageHas6Rows()
		{
			return this.mBackground == BackgroundType.BACKGROUND_3_POOL || this.mBackground == BackgroundType.BACKGROUND_4_FOG;
		}

		public bool StageHasFog()
		{
			return !this.mApp.IsStormyNightLevel() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL && this.mBackground == BackgroundType.BACKGROUND_4_FOG;
		}

		public bool StageHasGraveStones()
		{
			return !this.mApp.IsWallnutBowlingLevel() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_POGO_PARTY && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_LAST_STAND && !this.mApp.IsIZombieLevel() && !this.mApp.IsScaryPotterLevel() && this.mBackground == BackgroundType.BACKGROUND_2_NIGHT;
		}

		public int PixelToGridX(int theX, int theY)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && (this.mBackground == BackgroundType.BACKGROUND_MUSHROOM_GARDEN || this.mBackground == BackgroundType.BACKGROUND_ZOMBIQUARIUM || this.mBackground == BackgroundType.BACKGROUND_GREENHOUSE))
			{
				return this.mApp.mZenGarden.PixelToGridX(theX, theY);
			}
			if (theX < Constants.LAWN_XMIN)
			{
				return -1;
			}
			return TodCommon.ClampInt((theX - Constants.LAWN_XMIN) / 80, 0, Constants.GRIDSIZEX - 1);
		}

		public int PixelToGridY(int theX, int theY)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && (this.mBackground == BackgroundType.BACKGROUND_MUSHROOM_GARDEN || this.mBackground == BackgroundType.BACKGROUND_ZOMBIQUARIUM || this.mBackground == BackgroundType.BACKGROUND_GREENHOUSE))
			{
				return this.mApp.mZenGarden.PixelToGridY(theX, theY);
			}
			int num = this.PixelToGridX(theX, theY);
			if (num == -1 || theY < Constants.LAWN_YMIN)
			{
				return -1;
			}
			if (this.StageHasRoof())
			{
				int num2 = 0;
				if (num < 5)
				{
					num2 = (5 - num) * 20 - 20;
				}
				return TodCommon.ClampInt((theY - Constants.LAWN_YMIN - num2) / 85, 0, 4);
			}
			if (this.StageHas6Rows())
			{
				return TodCommon.ClampInt((theY - Constants.LAWN_YMIN) / 85, 0, 5);
			}
			return TodCommon.ClampInt((theY - Constants.LAWN_YMIN) / 100, 0, 4);
		}

		public int GridToPixelX(int theGridX, int theGridY)
		{
			Debug.ASSERT(theGridX >= 0 && theGridX < Constants.GRIDSIZEX);
			Debug.ASSERT(theGridY >= 0 && theGridY < Constants.MAX_GRIDSIZEY);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && (this.mBackground == BackgroundType.BACKGROUND_MUSHROOM_GARDEN || this.mBackground == BackgroundType.BACKGROUND_ZOMBIQUARIUM || this.mBackground == BackgroundType.BACKGROUND_GREENHOUSE))
			{
				return this.mApp.mZenGarden.GridToPixelX(theGridX, theGridY);
			}
			int num = 80;
			return theGridX * num + Constants.LAWN_XMIN;
		}

		public int GridToPixelY(int theGridX, int theGridY)
		{
			Debug.ASSERT(theGridX >= 0 && theGridX < Constants.GRIDSIZEX);
			Debug.ASSERT(theGridY >= 0 && theGridY < Constants.MAX_GRIDSIZEY);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && (this.mBackground == BackgroundType.BACKGROUND_MUSHROOM_GARDEN || this.mBackground == BackgroundType.BACKGROUND_ZOMBIQUARIUM || this.mBackground == BackgroundType.BACKGROUND_GREENHOUSE))
			{
				return this.mApp.mZenGarden.GridToPixelY(theGridX, theGridY);
			}
			int num2;
			if (this.StageHasRoof())
			{
				int num = 0;
				if (theGridX < 5)
				{
					num = (5 - theGridX) * 20;
				}
				num2 = theGridY * 85 + Constants.LAWN_YMIN + num - 10;
			}
			else if (this.StageHas6Rows())
			{
				num2 = theGridY * 85 + Constants.LAWN_YMIN;
			}
			else
			{
				num2 = theGridY * 100 + Constants.LAWN_YMIN;
			}
			if (theGridX != -1 && this.mGridSquareType[theGridX, theGridY] == GridSquareType.GRIDSQUARE_HIGH_GROUND)
			{
				num2 += -Constants.HIGH_GROUND_HEIGHT;
			}
			return num2;
		}

		public int PixelToGridXKeepOnBoard(int theX, int theY)
		{
			return Math.Max(this.PixelToGridX(theX, theY), 0);
		}

		public int PixelToGridYKeepOnBoard(int theX, int theY)
		{
			int theX2 = Math.Max(theX, Constants.LAWN_XMIN);
			return Math.Max(this.PixelToGridY(theX2, theY), 0);
		}

		public void UpdateGameObjects()
		{
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead)
				{
					plant.Update();
				}
			}
			count = this.mZombies.Count;
			for (int j = 0; j < count; j++)
			{
				Zombie zombie = this.mZombies[j];
				if (!zombie.mDead)
				{
					zombie.Update();
				}
			}
			count = this.mProjectiles.Count;
			for (int k = 0; k < count; k++)
			{
				Projectile projectile = this.mProjectiles[k];
				if (!projectile.mDead)
				{
					projectile.Update();
				}
			}
			Coin coin = null;
			while (this.IterateCoins(ref coin))
			{
				coin.Update();
			}
			LawnMower lawnMower = null;
			while (this.IterateLawnMowers(ref lawnMower))
			{
				lawnMower.Update();
			}
			this.mCursorPreview.Update();
			this.mCursorObject.Update();
			for (int l = 0; l < this.mSeedBank.mNumPackets; l++)
			{
				SeedPacket seedPacket = this.mSeedBank.mSeedPackets[l];
				seedPacket.Update();
				seedPacket.Update();
				seedPacket.Update();
			}
		}

		public bool MouseHitTest(int x, int y, out HitResult theHitResult, bool posScaled)
		{
			if (!posScaled)
			{
				x = (int)((float)x * Constants.IS);
				y = (int)((float)y * Constants.IS);
			}
			theHitResult = default(HitResult);
			if (this.mBoardFadeOutCounter >= 0)
			{
				theHitResult.mObject = null;
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_NONE;
				return false;
			}
			if (this.IsScaryPotterDaveTalking())
			{
				theHitResult.mObject = null;
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_NONE;
				return false;
			}
			if (this.mMenuButton.IsMouseOver() && this.CanInteractWithBoardButtons())
			{
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_MENU_BUTTON;
				return true;
			}
			if (this.mStoreButton != null && this.mStoreButton.IsMouseOver() && this.CanInteractWithBoardButtons())
			{
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_STORE_BUTTON;
				return true;
			}
			TRect shovelButtonRect = this.GetShovelButtonRect();
			x = (int)((float)x * Constants.S);
			y = (int)((float)y * Constants.S);
			if (this.mSeedBank.MouseHitTest(x, y, out theHitResult))
			{
				if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_BANK)
				{
					SeedPacket seedPacket = (SeedPacket)theHitResult.mObject;
					int mSeedBankIndex = this.mCursorObject.mSeedBankIndex;
					this.RefreshSeedPacketFromCursor();
					if (mSeedBankIndex == seedPacket.mIndex)
					{
						theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_NONE;
						return true;
					}
					return true;
				}
				else if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_SHOVEL || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_COBCANNON_TARGET || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_HAMMER)
				{
					return true;
				}
			}
			if (this.mShowShovel && shovelButtonRect.Contains(x, y) && this.CanInteractWithBoardButtons())
			{
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_SHOVEL;
				return true;
			}
			Coin coin = null;
			Coin coin2 = null;
			while (this.IterateCoins(ref coin2))
			{
				HitResult hitResult;
				if (coin2.MouseHitTest(x, y, out hitResult))
				{
					Coin coin3 = (Coin)hitResult.mObject;
					if (coin == null || coin3.mRenderOrder >= coin.mRenderOrder)
					{
						theHitResult = hitResult;
						coin = coin3;
					}
				}
			}
			if (coin != null)
			{
				return true;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				bool flag = false;
				if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_CHOCOLATE && !this.mApp.mZenGarden.IsStinkyHighOnChocolate())
				{
					flag = true;
				}
				else if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL && this.mApp.mZenGarden.IsStinkySleeping())
				{
					flag = true;
				}
				GridItem stinky = this.mApp.mZenGarden.GetStinky();
				if (flag && stinky != null)
				{
					TRect trect = new TRect((int)(Constants.S * (stinky.mPosX - 6f)), (int)(Constants.S * (stinky.mPosY - 10f)), (int)(Constants.S * 84f), (int)(Constants.S * 90f));
					if (trect.Contains(x, y))
					{
						theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_STINKY;
						return true;
					}
				}
			}
			x = (int)((float)x * Constants.IS);
			y = (int)((float)y * Constants.IS);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM && this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_TREE_FOOD && this.mChallenge.TreeOfWisdomHitTest(x, y, theHitResult))
			{
				return true;
			}
			x = (int)((float)x * Constants.S);
			y = (int)((float)y * Constants.S);
			if ((this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM) && this.CanInteractWithBoardButtons())
			{
				for (int i = 6; i <= 15; i++)
				{
					GameObjectType gameObjectType = (GameObjectType)i;
					if (this.CanUseGameObject(gameObjectType) && (gameObjectType != GameObjectType.OBJECT_TYPE_TREE_FOOD || this.mChallenge.TreeOfWisdomCanFeed()) && (gameObjectType != GameObjectType.OBJECT_TYPE_MONEY_SIGN || this.mApp.mPlayerInfo.mZenGardenTutorialComplete) && this.GetZenButtonRect(gameObjectType).Contains(x, y))
					{
						theHitResult.mObjectType = gameObjectType;
						return true;
					}
				}
			}
			if (this.mApp.IsSlotMachineLevel() && this.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_SHOVEL)
			{
				TRect trect2 = this.mChallenge.SlotMachineGetHandleRect();
				TRect trect3 = this.mChallenge.SlotMachineRect();
				int num = (int)(Constants.S * 50f);
				trect2.mX -= num;
				trect2.mWidth += num * 2;
				trect2.mY -= num;
				trect2.mHeight += num * 2;
				if ((trect2.Contains(x, y) || trect3.Contains(x, y)) && this.mChallenge.mChallengeState == ChallengeState.STATECHALLENGE_NORMAL && !this.HasLevelAwardDropped())
				{
					theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_SLOT_MACHINE_HANDLE;
					return true;
				}
			}
			x = (int)((float)x * Constants.IS);
			y = (int)((float)y * Constants.IS);
			if (this.MouseHitTestPlant(x, y, out theHitResult, true))
			{
				return true;
			}
			x = (int)((float)x * Constants.S);
			y = (int)((float)y * Constants.S);
			if (this.mApp.IsScaryPotterLevel() && this.mChallenge.mChallengeState != ChallengeState.STATECHALLENGE_SCARY_POTTER_MALLETING && this.mApp.mGameScene == GameScenes.SCENE_PLAYING && this.mApp.GetDialog(Dialogs.DIALOG_GAME_OVER) == null && this.mApp.GetDialog(Dialogs.DIALOG_CONTINUE) == null)
			{
				int theGridX = this.PixelToGridX((int)((float)x * Constants.IS), (int)((float)y * Constants.IS));
				int theGridY = this.PixelToGridY((int)((float)x * Constants.IS), (int)((float)y * Constants.IS));
				GridItem scaryPotAt = this.GetScaryPotAt(theGridX, theGridY);
				if (scaryPotAt != null)
				{
					theHitResult.mObject = scaryPotAt;
					theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_SCARY_POT;
					return true;
				}
			}
			if (this.mApp.IsSlotMachineLevel() && this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_SHOVEL)
			{
				TRect trect4 = this.mChallenge.SlotMachineGetHandleRect();
				TRect trect5 = this.mChallenge.SlotMachineRect();
				int num2 = (int)(Constants.S * 50f);
				trect4.mX -= num2;
				trect4.mWidth += num2 * 2;
				trect4.mY -= num2;
				trect4.mHeight += num2 * 2;
				if ((trect4.Contains(x, y) || trect5.Contains(x, y)) && this.mChallenge.mChallengeState == ChallengeState.STATECHALLENGE_NORMAL && !this.HasLevelAwardDropped())
				{
					theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_SLOT_MACHINE_HANDLE;
					return true;
				}
			}
			theHitResult.mObject = null;
			theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_NONE;
			return false;
		}

		public void MouseUpWithPlant(int x, int y, int theClickCount)
		{
			if (theClickCount < 0)
			{
				this.RefreshSeedPacketFromCursor();
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				return;
			}
			if (this.mApp.IsIZombieLevel())
			{
				this.mChallenge.IZombieMouseDownWithZombie(x, y, theClickCount);
				return;
			}
			SeedType seedTypeInCursor = this.GetSeedTypeInCursor();
			int num = this.PlantingPixelToGridX((int)((float)x * Constants.IS), (int)((float)y * Constants.IS), seedTypeInCursor);
			int num2 = this.PlantingPixelToGridY((int)((float)x * Constants.IS), (int)((float)y * Constants.IS), seedTypeInCursor);
			if (num < 0 || num >= Constants.GRIDSIZEX || num2 < 0 || num2 >= Constants.MAX_GRIDSIZEY)
			{
				this.RefreshSeedPacketFromCursor();
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				return;
			}
			PlantingReason plantingReason = this.CanPlantAt(num, num2, seedTypeInCursor);
			if (plantingReason != PlantingReason.PLANTING_OK)
			{
				if (plantingReason == PlantingReason.PLANTING_ONLY_ON_GRAVES)
				{
					this.DisplayAdvice("[ADVICE_GRAVEBUSTERS_ON_GRAVES]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_GRAVEBUSTERS_ON_GRAVES);
				}
				else if (seedTypeInCursor == SeedType.SEED_LILYPAD && plantingReason == PlantingReason.PLANTING_ONLY_IN_POOL)
				{
					this.DisplayAdvice("[ADVICE_LILYPAD_ON_WATER]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_LILYPAD_ON_WATER);
				}
				else if (seedTypeInCursor == SeedType.SEED_TANGLEKELP && plantingReason == PlantingReason.PLANTING_ONLY_IN_POOL)
				{
					this.DisplayAdvice("[ADVICE_TANGLEKELP_ON_WATER]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_TANGLEKELP_ON_WATER);
				}
				else if (seedTypeInCursor == SeedType.SEED_SEASHROOM && plantingReason == PlantingReason.PLANTING_ONLY_IN_POOL)
				{
					this.DisplayAdvice("[ADVICE_SEASHROOM_ON_WATER]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_SEASHROOM_ON_WATER);
				}
				else if (plantingReason == PlantingReason.PLANTING_ONLY_ON_GROUND)
				{
					this.DisplayAdvice("[ADVICE_POTATO_MINE_ON_LILY]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_POTATO_MINE_ON_LILY);
				}
				else if (plantingReason == PlantingReason.PLANTING_NOT_PASSED_LINE)
				{
					this.DisplayAdvice("[ADVICE_NOT_PASSED_LINE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_NOT_PASSED_LINE);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_UPGRADE && seedTypeInCursor == SeedType.SEED_GATLINGPEA)
				{
					this.DisplayAdvice("[ADVICE_ONLY_ON_REPEATERS]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_ONLY_ON_REPEATERS);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_UPGRADE && seedTypeInCursor == SeedType.SEED_WINTERMELON)
				{
					this.DisplayAdvice("[ADVICE_ONLY_ON_MELONPULT]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_ONLY_ON_MELONPULT);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_UPGRADE && seedTypeInCursor == SeedType.SEED_TWINSUNFLOWER)
				{
					this.DisplayAdvice("[ADVICE_ONLY_ON_SUNFLOWER]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_ONLY_ON_SUNFLOWER);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_UPGRADE && seedTypeInCursor == SeedType.SEED_SPIKEROCK)
				{
					this.DisplayAdvice("[ADVICE_ONLY_ON_SPIKEWEED]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_ONLY_ON_SPIKEWEED);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_UPGRADE && seedTypeInCursor == SeedType.SEED_COBCANNON)
				{
					this.DisplayAdvice("[ADVICE_ONLY_ON_KERNELPULT]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_ONLY_ON_KERNELPULT);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_UPGRADE && seedTypeInCursor == SeedType.SEED_GOLD_MAGNET)
				{
					this.DisplayAdvice("[ADVICE_ONLY_ON_MAGNETSHROOM]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_ONLY_ON_MAGNETSHROOM);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_UPGRADE && seedTypeInCursor == SeedType.SEED_GLOOMSHROOM)
				{
					this.DisplayAdvice("[ADVICE_ONLY_ON_FUMESHROOM]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_ONLY_ON_FUMESHROOM);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_UPGRADE && seedTypeInCursor == SeedType.SEED_CATTAIL)
				{
					this.DisplayAdvice("[ADVICE_ONLY_ON_LILYPAD]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_ONLY_ON_LILYPAD);
				}
				else if (plantingReason == PlantingReason.PLANTING_NOT_ON_ART)
				{
					SeedType artChallengeSeed = this.mChallenge.GetArtChallengeSeed(num, num2);
					string nameString = Plant.GetNameString(artChallengeSeed, SeedType.SEED_NONE);
					string theAdvice = TodCommon.TodReplaceString("[ADVICE_WRONG_ART_TYPE]", "{SEED}", nameString);
					this.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_WRONG_ART_TYPE);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_POT)
				{
					if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 41)
					{
						this.DisplayAdvice("[ADVICE_PLANT_NEED_POT1]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_NEED_POT);
					}
					else
					{
						this.DisplayAdvice("[ADVICE_PLANT_NEED_POT2]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_NEED_POT);
					}
				}
				else if (plantingReason == PlantingReason.PLANTING_NOT_ON_GRAVE)
				{
					this.DisplayAdvice("[ADVICE_PLANT_NOT_ON_GRAVE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_NOT_ON_GRAVE);
				}
				else if (plantingReason == PlantingReason.PLANTING_NOT_ON_CRATER)
				{
					if (this.IsPoolSquare(num, num2))
					{
						this.DisplayAdvice("[ADVICE_CANT_PLANT_THERE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_CANT_PLANT_THERE);
					}
					else
					{
						this.DisplayAdvice("[ADVICE_PLANT_NOT_ON_CRATER]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_NOT_ON_CRATER);
					}
				}
				else if (plantingReason == PlantingReason.PLANTING_NOT_ON_WATER)
				{
					if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mApp.mZenGarden.mGardenType == GardenType.GARDEN_AQUARIUM)
					{
						this.DisplayAdvice("[ZEN_ONLY_AQUATIC_PLANTS]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_NONE);
					}
					else if (seedTypeInCursor == SeedType.SEED_POTATOMINE)
					{
						this.DisplayAdvice("[ADVICE_CANT_PLANT_THERE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_CANT_PLANT_THERE);
					}
					else
					{
						this.DisplayAdvice("[ADVICE_PLANT_NOT_ON_WATER]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANT_NOT_ON_WATER);
					}
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_GROUND)
				{
					this.DisplayAdvice("[ADVICE_PLANTING_NEEDS_GROUND]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANTING_NEEDS_GROUND);
				}
				else if (plantingReason == PlantingReason.PLANTING_NEEDS_SLEEPING)
				{
					this.DisplayAdvice("[ADVICE_PLANTING_NEED_SLEEPING]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PLANTING_NEED_SLEEPING);
				}
				if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_GLOVE || this.mApp.IsWhackAZombieLevel())
				{
					this.RefreshSeedPacketFromCursor();
					this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				}
				return;
			}
			this.ClearAdvice(AdviceType.ADVICE_PLANTING_NEED_SLEEPING);
			this.ClearAdvice(AdviceType.ADVICE_CANT_PLANT_THERE);
			this.ClearAdvice(AdviceType.ADVICE_PLANTING_NEEDS_GROUND);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_NOT_ON_WATER);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_NOT_ON_CRATER);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_NOT_ON_GRAVE);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_NEED_POT);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_WRONG_ART_TYPE);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_ONLY_ON_LILYPAD);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_ONLY_ON_MAGNETSHROOM);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_ONLY_ON_FUMESHROOM);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_ONLY_ON_KERNELPULT);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_ONLY_ON_SUNFLOWER);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_ONLY_ON_SPIKEWEED);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_ONLY_ON_MELONPULT);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_ONLY_ON_REPEATERS);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_NOT_PASSED_LINE);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_GRAVEBUSTERS_ON_GRAVES);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_LILYPAD_ON_WATER);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_TANGLEKELP_ON_WATER);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_SEASHROOM_ON_WATER);
			this.ClearAdvice(AdviceType.ADVICE_PLANT_POTATO_MINE_ON_LILY);
			this.ClearAdvice(AdviceType.ADVICE_SURVIVE_FLAGS);
			if (!this.mApp.mEasyPlantingCheat && this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_BANK && !this.HasConveyorBeltSeedBank())
			{
				int currentPlantCost = this.GetCurrentPlantCost(this.mCursorObject.mType, this.mCursorObject.mImitaterType);
				if (!this.TakeSunMoney(currentPlantCost))
				{
					return;
				}
			}
			Plant topPlantAt = this.GetTopPlantAt(num, num2, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
			bool flag = false;
			int mWakeUpCounter = 0;
			if (topPlantAt != null && topPlantAt.IsUpgradableTo(seedTypeInCursor))
			{
				if (seedTypeInCursor == SeedType.SEED_GLOOMSHROOM)
				{
					if (!topPlantAt.mIsAsleep)
					{
						flag = true;
					}
					else
					{
						mWakeUpCounter = topPlantAt.mWakeUpCounter;
					}
				}
				topPlantAt.Die();
			}
			if ((seedTypeInCursor == SeedType.SEED_WALLNUT || seedTypeInCursor == SeedType.SEED_TALLNUT) && topPlantAt != null && topPlantAt.mSeedType == seedTypeInCursor)
			{
				topPlantAt.Die();
			}
			if (seedTypeInCursor == SeedType.SEED_PUMPKINSHELL)
			{
				Plant topPlantAt2 = this.GetTopPlantAt(num, num2, PlantPriority.TOPPLANT_ONLY_PUMPKIN);
				if (topPlantAt2 != null && topPlantAt2.mSeedType == seedTypeInCursor)
				{
					topPlantAt2.Die();
				}
			}
			if (seedTypeInCursor == SeedType.SEED_COBCANNON)
			{
				Plant topPlantAt3 = this.GetTopPlantAt(num + 1, num2, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
				if (topPlantAt3 != null)
				{
					topPlantAt3.Die();
				}
			}
			if (seedTypeInCursor == SeedType.SEED_CATTAIL)
			{
				PlantsOnLawn plantsOnLawn = default(PlantsOnLawn);
				this.GetPlantsOnLawn(num, num2, ref plantsOnLawn);
				if (plantsOnLawn.mUnderPlant != null)
				{
					plantsOnLawn.mUnderPlant.Die();
				}
				if (plantsOnLawn.mNormalPlant != null)
				{
					plantsOnLawn.mNormalPlant.Die();
				}
			}
			if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_GLOVE)
			{
				Plant mGlovePlantID = this.mCursorObject.mGlovePlantID;
				mGlovePlantID.mGloveGrabbed = false;
				this.mApp.mZenGarden.MovePlant(mGlovePlantID, num, num2);
			}
			else if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_WHEEL_BARROW)
			{
				this.mApp.mZenGarden.MouseDownWithFullWheelBarrow(x, y);
			}
			else if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_USABLE_COIN)
			{
				this.AddPlant(num, num2, this.mCursorObject.mType, this.mCursorObject.mImitaterType);
				Coin coin = this.mCoins[this.mCoins.IndexOf(this.mCursorObject.mCoinID)];
				this.mCursorObject.mCoinID = null;
				coin.Die();
			}
			else if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_BANK)
			{
				Plant plant = this.AddPlant(num, num2, this.mCursorObject.mType, this.mCursorObject.mImitaterType);
				if (flag)
				{
					plant.SetSleeping(false);
				}
				plant.mWakeUpCounter = mWakeUpCounter;
				Debug.ASSERT(this.mCursorObject.mSeedBankIndex >= 0 && this.mCursorObject.mSeedBankIndex < this.mSeedBank.mNumPackets);
				SeedPacket seedPacket = this.mSeedBank.mSeedPackets[this.mCursorObject.mSeedBankIndex];
				seedPacket.WasPlanted();
			}
			else
			{
				Debug.ASSERT(false);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
				{
					if (i != num2 && this.CanPlantAt(num, i, seedTypeInCursor) == PlantingReason.PLANTING_OK)
					{
						if (seedTypeInCursor == SeedType.SEED_WALLNUT || seedTypeInCursor == SeedType.SEED_TALLNUT)
						{
							Plant topPlantAt4 = this.GetTopPlantAt(num, i, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
							if (topPlantAt4 != null && topPlantAt4.mSeedType == seedTypeInCursor)
							{
								topPlantAt4.Die();
							}
						}
						if (seedTypeInCursor == SeedType.SEED_PUMPKINSHELL)
						{
							Plant topPlantAt5 = this.GetTopPlantAt(num, i, PlantPriority.TOPPLANT_ONLY_PUMPKIN);
							if (topPlantAt5 != null && topPlantAt5.mSeedType == seedTypeInCursor)
							{
								topPlantAt5.Die();
							}
						}
						this.AddPlant(num, i, this.mCursorObject.mType, this.mCursorObject.mImitaterType);
					}
				}
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PLANT_PEASHOOTER)
			{
				int count = this.mPlants.Count;
				if (count >= 2)
				{
					this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_1_COMPLETED);
				}
				else
				{
					this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_1_REFRESH_PEASHOOTER);
				}
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_LEVEL_2_PLANT_SUNFLOWER)
			{
				int num3 = this.CountSunFlowers();
				if (seedTypeInCursor == SeedType.SEED_SUNFLOWER && num3 == 2)
				{
					this.DisplayAdvice("[ADVICE_MORE_SUNFLOWERS]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL2, AdviceType.ADVICE_NONE);
				}
				if (num3 >= 3)
				{
					this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_2_COMPLETED);
				}
				else if (!this.mSeedBank.mSeedPackets[1].CanPickUp())
				{
					this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_2_REFRESH_SUNFLOWER);
				}
				else
				{
					this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_2_PICK_UP_SUNFLOWER);
				}
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_MORESUN_PLANT_SUNFLOWER)
			{
				int num4 = this.CountSunFlowers();
				if (num4 >= 3)
				{
					this.SetTutorialState(TutorialState.TUTORIAL_MORESUN_COMPLETED);
					this.DisplayAdvice("[ADVICE_PLANT_SUNFLOWER5]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LATER, AdviceType.ADVICE_PLANT_SUNFLOWER5);
					this.mTutorialTimer = -1;
				}
				else if (!this.mSeedBank.mSeedPackets[1].CanPickUp())
				{
					this.SetTutorialState(TutorialState.TUTORIAL_MORESUN_REFRESH_SUNFLOWER);
				}
				else
				{
					this.SetTutorialState(TutorialState.TUTORIAL_MORESUN_PICK_UP_SUNFLOWER);
				}
			}
			if (this.mApp.IsWallnutBowlingLevel())
			{
				this.mApp.PlaySample(Resources.SOUND_BOWLING);
			}
			this.ClearCursor();
		}

		public void MouseDownWithTool(int x, int y, int theClickCount, CursorType theCursorType, bool posScaled)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				if (posScaled)
				{
					x = (int)(Constants.S * (float)x);
					y = (int)(Constants.S * (float)y);
					posScaled = false;
				}
				if (y < Constants.ZEN_YMIN)
				{
					this.ClearCursor();
					return;
				}
			}
			if (!posScaled)
			{
				x = (int)((float)x * Constants.IS);
				y = (int)((float)y * Constants.IS);
			}
			if (theClickCount < 0)
			{
				this.ClearCursor();
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mApp.mZenGarden.MouseDownWithTool(x, y, theCursorType);
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				this.mChallenge.TreeOfWisdomTool(x, y);
				return;
			}
			HitResult hitResult = this.ToolHitTest(x, y, true);
			Plant plant = null;
			if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
			{
				plant = (Plant)hitResult.mObject;
			}
			if (plant != null)
			{
				if (theCursorType == CursorType.CURSOR_TYPE_SHOVEL)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_USE_SHOVEL);
					this.mPlantsShoveled++;
					plant.Die();
					if (plant.mSeedType == SeedType.SEED_CATTAIL && this.GetTopPlantAt(plant.mPlantCol, plant.mRow, PlantPriority.TOPPLANT_ONLY_PUMPKIN) != null)
					{
						this.NewPlant(plant.mPlantCol, plant.mRow, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
					}
					if (this.mTutorialState == TutorialState.TUTORIAL_SHOVEL_DIG || this.mTutorialState == TutorialState.TUTORIAL_SHOVEL_KEEP_DIGGING)
					{
						if (this.CountPlantByType(SeedType.SEED_PEASHOOTER) == 0)
						{
							this.SetTutorialState(TutorialState.TUTORIAL_SHOVEL_COMPLETED);
						}
						else
						{
							this.SetTutorialState(TutorialState.TUTORIAL_SHOVEL_KEEP_DIGGING);
						}
					}
				}
				this.ClearCursor();
				return;
			}
			HitResult hitResult2;
			this.MouseHitTest(x, y, out hitResult2, true);
			if (hitResult2.mObjectType == GameObjectType.OBJECT_TYPE_COIN)
			{
				return;
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
			this.ClearCursor();
		}

		public bool CanInteractWithBoardButtons()
		{
			return !this.mPaused && this.mApp.GetDialogCount() <= 0 && this.mBoardFadeOutCounter < 0 && this.mChallenge.mChallengeState != ChallengeState.STATECHALLENGE_ZEN_FADING && (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL || this.mApp.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_OFF);
		}

		public void DrawProgressMeter(Graphics g)
		{
			if (!this.HasProgressMeter())
			{
				return;
			}
			int num = Constants.UIProgressMeterPosition.X - Constants.Board_Offset_AspectRatio_Correction;
			int y = Constants.UIProgressMeterPosition.Y;
			g.DrawImageCel(AtlasResources.IMAGE_FLAGMETER, num, y, 0);
			int celWidth = AtlasResources.IMAGE_FLAGMETER.GetCelWidth();
			int celHeight = AtlasResources.IMAGE_FLAGMETER.GetCelHeight();
			int thePosX = num + celWidth / 2;
			int board_ProgressBarText_Pos = Constants.Board_ProgressBarText_Pos;
			int num2 = TodCommon.TodAnimateCurve(0, 150, this.mProgressMeterWidth, 0, Constants.UIProgressMeterBarEnd, TodCurves.CURVE_LINEAR);
			TRect theSrcRect = new TRect(celWidth - num2 - 7, celHeight, num2, celHeight);
			TRect theDestRect = new TRect(num + celWidth - num2 - 7, y, num2, celHeight);
			g.DrawImage(AtlasResources.IMAGE_FLAGMETER, theDestRect, theSrcRect);
			SexyColor theColor = new SexyColor(224, 187, 98);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				if (this.progressMeterStringValue != this.mChallenge.mChallengeScore)
				{
					this.progressMeterString = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_MATCHES]"), this.mChallenge.mChallengeScore, 75, TodStringFile.TodStringTranslate("[MATCHES]"));
					this.progressMeterStringValue = this.mChallenge.mChallengeScore;
				}
				TodCommon.TodDrawString(g, this.progressMeterString, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, DrawStringJustification.DS_ALIGN_CENTER);
			}
			else if (this.mApp.IsSquirrelLevel())
			{
				string theText = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_SQUIRRELS]"), this.mChallenge.mChallengeScore, 7, TodStringFile.TodStringTranslate("[SQUIRRELS]"));
				TodCommon.TodDrawString(g, theText, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, DrawStringJustification.DS_ALIGN_CENTER);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SLOT_MACHINE)
			{
				int num3 = TodCommon.ClampInt(this.mSunMoney, 0, 2000);
				if (this.progressMeterStringValue != num3)
				{
					this.progressMeterString = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_SUN_SLOT_MACHINE]"), num3, 2000, TodStringFile.TodStringTranslate("[SUN]"));
					this.progressMeterStringValue = num3;
				}
				TodCommon.TodDrawString(g, this.progressMeterString, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, celWidth - 10, DrawStringJustification.DS_ALIGN_CENTER);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM)
			{
				int num4 = TodCommon.ClampInt(this.mSunMoney, 0, 1000);
				string theText2 = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_SUN_ZOMBIQUARIUM]"), num4, 1000, TodStringFile.TodStringTranslate("[SUN]"));
				TodCommon.TodDrawString(g, theText2, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, DrawStringJustification.DS_ALIGN_CENTER);
			}
			else if (this.mApp.IsIZombieLevel())
			{
				if (this.progressMeterStringValue != this.mChallenge.mChallengeScore)
				{
					this.progressMeterString = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_BRAINS]"), this.mChallenge.mChallengeScore, 5, TodStringFile.TodStringTranslate("[BRAINS]"));
					this.progressMeterStringValue = this.mChallenge.mChallengeScore;
				}
				Resources.FONT_DWARVENTODCRAFT12.characterOffsetMagic = 2;
				TodCommon.TodDrawString(g, this.progressMeterString, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, DrawStringJustification.DS_ALIGN_CENTER);
				Resources.FONT_DWARVENTODCRAFT12.characterOffsetMagic = 0;
			}
			else if (this.ProgressMeterHasFlags())
			{
				int numWavesPerFlag = this.GetNumWavesPerFlag();
				for (int i = 1; i <= this.mNumWaves / numWavesPerFlag; i++)
				{
					int theTimeAge = i * numWavesPerFlag;
					int num5 = 0;
					int thePositionEnd = num + 6;
					int thePositionStart = num + celWidth - 10;
					int theX = TodCommon.TodAnimateCurve(0, this.mNumWaves, theTimeAge, thePositionStart, thePositionEnd, TodCurves.CURVE_LINEAR);
					g.DrawImageCel(AtlasResources.IMAGE_FLAGMETERPARTS, theX, y - 4, 1, 0);
					g.DrawImageCel(AtlasResources.IMAGE_FLAGMETERPARTS, theX, y - num5 - 3, 2, 0);
				}
			}
			if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && !this.mApp.IsSquirrelLevel() && !this.mApp.IsSlotMachineLevel() && !this.mApp.IsIZombieLevel() && !this.mApp.IsFinalBossLevel())
			{
				int num6 = TodCommon.TodAnimateCurve(0, 150, this.mProgressMeterWidth, 0, Constants.UIProgressMeterHeadEnd, TodCurves.CURVE_LINEAR);
				g.DrawImageCel(AtlasResources.IMAGE_FLAGMETERPARTS, num + celWidth - num6 - 20, y - 3, 0, 0);
			}
		}

		public Plant GetTopPlantAt(int theGridX, int theGridY, PlantPriority thePriority)
		{
			if (theGridX < 0 || theGridX >= Constants.GRIDSIZEX || theGridY < 0 || theGridY >= Constants.MAX_GRIDSIZEY)
			{
				return null;
			}
			if (this.mApp.IsWallnutBowlingLevel() && !this.mCutScene.IsInShovelTutorial())
			{
				return null;
			}
			PlantsOnLawn plantsOnLawn = default(PlantsOnLawn);
			this.GetPlantsOnLawn(theGridX, theGridY, ref plantsOnLawn);
			if (thePriority == PlantPriority.TOPPLANT_ONLY_FLYING)
			{
				return plantsOnLawn.mFlyingPlant;
			}
			if (thePriority == PlantPriority.TOPPLANT_ONLY_UNDER_PLANT)
			{
				return plantsOnLawn.mUnderPlant;
			}
			if (thePriority == PlantPriority.TOPPLANT_ONLY_PUMPKIN)
			{
				return plantsOnLawn.mPumpkinPlant;
			}
			if (thePriority == PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION)
			{
				return plantsOnLawn.mNormalPlant;
			}
			if (thePriority == PlantPriority.TOPPLANT_EATING_ORDER)
			{
				if (plantsOnLawn.mPumpkinPlant != null)
				{
					return plantsOnLawn.mPumpkinPlant;
				}
				if (plantsOnLawn.mNormalPlant != null)
				{
					return plantsOnLawn.mNormalPlant;
				}
				if (plantsOnLawn.mUnderPlant != null)
				{
					return plantsOnLawn.mUnderPlant;
				}
				return null;
			}
			else if (thePriority == PlantPriority.TOPPLANT_DIGGING_ORDER)
			{
				if (plantsOnLawn.mNormalPlant != null)
				{
					return plantsOnLawn.mNormalPlant;
				}
				if (plantsOnLawn.mUnderPlant != null)
				{
					return plantsOnLawn.mUnderPlant;
				}
				return null;
			}
			else if (thePriority == PlantPriority.TOPPLANT_BUNGEE_ORDER)
			{
				if (plantsOnLawn.mFlyingPlant != null)
				{
					return plantsOnLawn.mFlyingPlant;
				}
				if (plantsOnLawn.mNormalPlant != null)
				{
					return plantsOnLawn.mNormalPlant;
				}
				if (plantsOnLawn.mPumpkinPlant != null)
				{
					return plantsOnLawn.mPumpkinPlant;
				}
				if (plantsOnLawn.mUnderPlant != null)
				{
					return plantsOnLawn.mUnderPlant;
				}
				return null;
			}
			else if (thePriority == PlantPriority.TOPPLANT_CATAPULT_ORDER || thePriority == PlantPriority.TOPPLANT_ANY)
			{
				if (plantsOnLawn.mFlyingPlant != null)
				{
					return plantsOnLawn.mFlyingPlant;
				}
				if (plantsOnLawn.mNormalPlant != null)
				{
					return plantsOnLawn.mNormalPlant;
				}
				if (plantsOnLawn.mPumpkinPlant != null)
				{
					return plantsOnLawn.mPumpkinPlant;
				}
				if (plantsOnLawn.mUnderPlant != null)
				{
					return plantsOnLawn.mUnderPlant;
				}
				return null;
			}
			else
			{
				if (thePriority != PlantPriority.TOPPLANT_ZEN_TOOL_ORDER)
				{
					Debug.ASSERT(false);
					return null;
				}
				if (plantsOnLawn.mFlyingPlant != null)
				{
					return plantsOnLawn.mFlyingPlant;
				}
				if (plantsOnLawn.mPumpkinPlant != null)
				{
					return plantsOnLawn.mPumpkinPlant;
				}
				if (plantsOnLawn.mNormalPlant != null)
				{
					return plantsOnLawn.mNormalPlant;
				}
				if (plantsOnLawn.mUnderPlant != null)
				{
					return plantsOnLawn.mUnderPlant;
				}
				return null;
			}
		}

		public void GetPlantsOnLawn(int theGridX, int theGridY, ref PlantsOnLawn thePlantOnLawn)
		{
			thePlantOnLawn.mUnderPlant = null;
			thePlantOnLawn.mPumpkinPlant = null;
			thePlantOnLawn.mFlyingPlant = null;
			thePlantOnLawn.mNormalPlant = null;
			if (theGridX < 0 || theGridX >= Constants.GRIDSIZEX || theGridY < 0 || theGridY >= Constants.MAX_GRIDSIZEY)
			{
				return;
			}
			if (this.mApp.IsWallnutBowlingLevel() && !this.mCutScene.IsInShovelTutorial())
			{
				return;
			}
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead)
				{
					SeedType seedType = plant.mSeedType;
					if (plant.mSeedType == SeedType.SEED_IMITATER && plant.mImitaterType != SeedType.SEED_NONE)
					{
						seedType = plant.mImitaterType;
					}
					if (seedType == SeedType.SEED_COBCANNON)
					{
						if (plant.mPlantCol < theGridX - 1 || plant.mPlantCol > theGridX)
						{
							goto IL_19D;
						}
						if (plant.mRow != theGridY)
						{
							goto IL_19D;
						}
					}
					else if (plant.mPlantCol != theGridX || plant.mRow != theGridY)
					{
						goto IL_19D;
					}
					if (!plant.NotOnGround())
					{
						if (Plant.IsFlying(seedType))
						{
							Debug.ASSERT(thePlantOnLawn.mFlyingPlant == null);
							thePlantOnLawn.mFlyingPlant = plant;
						}
						else if (seedType == SeedType.SEED_FLOWERPOT)
						{
							Debug.ASSERT(thePlantOnLawn.mUnderPlant == null);
							thePlantOnLawn.mUnderPlant = plant;
						}
						else if (seedType == SeedType.SEED_LILYPAD)
						{
							if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
							{
								Debug.ASSERT(thePlantOnLawn.mNormalPlant == null);
								thePlantOnLawn.mNormalPlant = plant;
							}
							else
							{
								Debug.ASSERT(thePlantOnLawn.mUnderPlant == null);
								thePlantOnLawn.mUnderPlant = plant;
							}
						}
						else if (seedType == SeedType.SEED_PUMPKINSHELL)
						{
							Debug.ASSERT(thePlantOnLawn.mPumpkinPlant == null);
							thePlantOnLawn.mPumpkinPlant = plant;
						}
						else
						{
							Debug.ASSERT(thePlantOnLawn.mNormalPlant == null);
							thePlantOnLawn.mNormalPlant = plant;
						}
					}
				}
				IL_19D:;
			}
		}

		public int CountSunFlowers()
		{
			int num = 0;
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead && plant.MakesSun())
				{
					num++;
				}
			}
			return num;
		}

		public int GetSeedPacketPositionY(int theIndex)
		{
			int mNumPackets = this.mSeedBank.mNumPackets;
			int num = Constants.SMALL_SEEDPACKET_HEIGHT;
			if (mNumPackets <= 7)
			{
				num += (int)Constants.InvertAndScale(8f);
			}
			else if (mNumPackets == 8)
			{
				num += (int)Constants.InvertAndScale(5f);
			}
			return theIndex * num;
		}

		public void AddGraveStones(int theGridX, int theCount)
		{
			if (!this.doAddGraveStones)
			{
				return;
			}
			Debug.ASSERT(theCount <= Constants.MAX_GRIDSIZEY);
			int i = 0;
			while (i < theCount)
			{
				int theGridY = RandomNumbers.NextNumber(Constants.MAX_GRIDSIZEY);
				if (this.CanAddGraveStoneAt(theGridX, theGridY))
				{
					this.AddAGraveStone(theGridX, theGridY);
					i++;
				}
			}
		}

		public int GetGraveStoneCount()
		{
			int num = 0;
			int num2 = -1;
			GridItem gridItem = null;
			while (this.IterateGridItems(ref gridItem, ref num2))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_GRAVESTONE)
				{
					num++;
				}
			}
			return num;
		}

		public void ZombiesWon(Zombie aZombie)
		{
			if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
			{
				return;
			}
			this.ClearAdvice(AdviceType.ADVICE_NONE);
			this.ClearCursor();
			if (this.mNextSurvivalStageCounter > 0)
			{
				this.mNextSurvivalStageCounter = 0;
			}
			this.mApp.mBoardResult = BoardResult.BOARDRESULT_LOST;
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && zombie != aZombie)
				{
					if ((float)zombie.GetZombieRect().mX < -50f || zombie.mZombiePhase == ZombiePhase.PHASE_RISING_FROM_GRAVE || zombie.mZombiePhase == ZombiePhase.PHASE_DANCER_RISING)
					{
						zombie.DieNoLoot(false);
					}
					if ((zombie.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || zombie.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR) && zombie.IsDeadOrDying() && zombie.mPosX < 140f)
					{
						zombie.DieNoLoot(false);
					}
				}
			}
			string theMessage = string.Empty;
			bool flag = true;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM)
			{
				theMessage = "[ZOMBIQUARIUM_DEATH_MESSAGE]";
				flag = false;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				int survivalFlagsCompleted = this.GetSurvivalFlagsCompleted();
				string theStringToSubstitute = this.mApp.Pluralize(survivalFlagsCompleted, "[ONE_FLAG]", "[COUNT_FLAGS]");
				theMessage = TodCommon.TodReplaceString("[LAST_STAND_DEATH_MESSAGE]", "{FLAGS}", theStringToSubstitute);
				flag = false;
			}
			else if (this.mApp.IsEndlessScaryPotter(this.mApp.mGameMode) || this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
			{
				int mSurvivalStage = this.mChallenge.mSurvivalStage;
				if (this.mApp.IsEndlessScaryPotter(this.mApp.mGameMode))
				{
					this.mApp.mPlayerInfo.mVasebreakerScore = Math.Max(this.mApp.mPlayerInfo.mVasebreakerScore, (long)mSurvivalStage);
					LeaderBoardComm.RecordResult(LeaderboardGameMode.Vasebreaker, (int)this.mApp.mPlayerInfo.mVasebreakerScore);
				}
				else if (this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
				{
					this.mApp.mPlayerInfo.mIZombieScore = Math.Max(this.mApp.mPlayerInfo.mIZombieScore, (long)mSurvivalStage);
					LeaderBoardComm.RecordResult(LeaderboardGameMode.IZombie, (int)this.mApp.mPlayerInfo.mIZombieScore);
				}
				theMessage = TodCommon.TodReplaceNumberString("[ENDLESS_PUZZLE_DEATH_MESSAGE]", "{STREAK}", mSurvivalStage);
				flag = false;
			}
			else if (this.mApp.IsIZombieLevel())
			{
				theMessage = "[I_ZOMBIE_DEATH_MESSAGE]";
				flag = false;
			}
			if (flag)
			{
				this.mApp.mGameScene = GameScenes.SCENE_ZOMBIES_WON;
				aZombie.WalkIntoHouse();
				this.ClearAdvice(AdviceType.ADVICE_NONE);
				this.mCutScene.StartZombiesWon();
				this.FreezeEffectsForCutscene(true);
				this.TutorialArrowRemove();
				this.UpdateCursor();
				return;
			}
			GameOverDialog theDialog = new GameOverDialog(theMessage, true);
			this.mApp.AddDialog(17, theDialog);
			this.mApp.mMusic.StopAllMusic();
			this.StopAllZombieSounds();
			this.mApp.PlaySample(Resources.SOUND_LOSEMUSIC);
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIES_WON, true);
			int num = Constants.BOARD_EXTRA_ROOM / 2;
			Reanimation reanimation = this.mApp.AddReanimation((float)(-(float)Constants.BOARD_OFFSET + num + Constants.Board_Offset_AspectRatio_Correction), 0f, 900000, ReanimationType.REANIM_ZOMBIES_WON);
			reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
			ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(Reanimation.ReanimTrackId_fullscreen);
			trackInstanceByName.mTrackColor = SexyColor.Black;
			reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_screen);
		}

		public void DrawLevel(Graphics g)
		{
			if (this.mLevelFadeCount <= 0)
			{
				return;
			}
			if (this.mApp.IsAdventureMode())
			{
				if (this.levelStrVal != this.mLevel)
				{
					this.mLevelStr = TodStringFile.TodStringTranslate("[LEVEL]") + " " + this.mApp.GetStageString(this.mLevel);
					this.levelStrVal = this.mLevel;
				}
			}
			else if (this.mApp.IsSurvivalMode() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				this.mApp.GetCurrentChallengeIndex();
				int survivalFlagsCompleted = this.GetSurvivalFlagsCompleted();
				if (survivalFlagsCompleted > 0)
				{
					string theStringToSubstitute = this.mApp.Pluralize(survivalFlagsCompleted, "[ONE_FLAG]", "[COUNT_FLAGS]");
					string text;
					if (survivalFlagsCompleted == 1)
					{
						text = TodCommon.TodReplaceString("[FLAGS_COMPLETED]", "{FLAGS}", theStringToSubstitute);
					}
					else
					{
						text = TodCommon.TodReplaceString("[FLAGS_COMPLETED_PLURAL]", "{FLAGS}", theStringToSubstitute);
					}
					this.mLevelStr = TodStringFile.TodStringTranslate(this.mChallenge.mName) + " - " + text;
				}
				else
				{
					this.mLevelStr = this.mChallenge.mName;
				}
			}
			else if (this.mApp.IsEndlessScaryPotter(this.mApp.mGameMode) || this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
			{
				this.mApp.GetCurrentChallengeIndex();
				int num = this.mChallenge.mSurvivalStage;
				if (this.mNextSurvivalStageCounter > 0)
				{
					num++;
				}
				if (num > 0)
				{
					string text2 = TodCommon.TodReplaceNumberString("[ENDLESS_STREAK]", "{STREAK}", num);
					this.mLevelStr = TodStringFile.TodStringTranslate(this.mChallenge.mName) + " - " + text2;
				}
				else
				{
					this.mLevelStr = this.mChallenge.mName;
				}
			}
			else
			{
				this.mLevelStr = this.mChallenge.mName;
			}
			int thePosX = Constants.UILevelPosition.X - Constants.Board_Offset_AspectRatio_Correction;
			int num2 = Constants.UILevelPosition.Y;
			if (this.mChallenge.mChallengeState == ChallengeState.STATECHALLENGE_ZEN_FADING)
			{
				num2 += TodCommon.TodAnimateCurve(50, 0, this.mChallenge.mChallengeStateCounter, 0, 50, TodCurves.CURVE_EASE_IN_OUT);
			}
			int theAlpha = TodCommon.ClampInt(255 * this.mLevelFadeCount / 15, 0, 255);
			TodCommon.TodDrawString(g, this.mLevelStr, thePosX, num2, Resources.FONT_HOUSEOFTERROR16, new SexyColor(224, 187, 98, theAlpha), DrawStringJustification.DS_ALIGN_RIGHT);
		}

		public void DrawShovel(Graphics g)
		{
			if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mApp.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM && this.mShowShovel)
			{
				TRect shovelButtonRect = this.GetShovelButtonRect();
				g.DrawImage(AtlasResources.IMAGE_SHOVELBANK, shovelButtonRect.mX, shovelButtonRect.mY);
				if (this.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_SHOVEL)
				{
					if (this.mChallenge.mChallengeState == (ChallengeState)15)
					{
						SexyColor flashingColor = TodCommon.GetFlashingColor(this.mMainCounter, 75);
						g.SetColorizeImages(true);
						g.SetColor(flashingColor);
					}
					g.DrawImage(AtlasResources.IMAGE_TINY_SHOVEL, shovelButtonRect.mX + 2, shovelButtonRect.mY + 3);
					g.SetColorizeImages(false);
				}
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				this.DrawZenButtons(g);
			}
		}

		public void UpdateZombieSpawning()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL || this.mApp.mGameMode == GameMode.GAMEMODE_INTRO)
			{
				return;
			}
			if (this.mFinalWaveSoundCounter > 0)
			{
				this.mFinalWaveSoundCounter -= 3;
				if (this.mFinalWaveSoundCounter >= 0 && this.mFinalWaveSoundCounter < 3)
				{
					this.mApp.PlaySample(Resources.SOUND_FINALWAVE);
				}
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PICK_UP_PEASHOOTER || this.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PLANT_PEASHOOTER || this.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_REFRESH_PEASHOOTER || this.mTutorialState == TutorialState.TUTORIAL_SLOT_MACHINE_PULL)
			{
				return;
			}
			if (this.HasLevelAwardDropped())
			{
				return;
			}
			if (this.mRiseFromGraveCounter > 0)
			{
				this.mRiseFromGraveCounter -= 3;
				if (this.mRiseFromGraveCounter >= 0 && this.mRiseFromGraveCounter < 3)
				{
					this.SpawnZombiesFromGraves();
				}
			}
			if (this.mHugeWaveCountDown > 0)
			{
				this.mHugeWaveCountDown -= 3;
				if (this.mHugeWaveCountDown >= 0 && this.mHugeWaveCountDown < 3)
				{
					this.ClearAdvice(AdviceType.ADVICE_HUGE_WAVE);
					this.NextWaveComing();
					this.mZombieCountDown = 3;
				}
				else
				{
					if (this.mHugeWaveCountDown < 723 || this.mHugeWaveCountDown >= 726)
					{
						if (this.mApp.mMusic.mCurMusicTune == MusicTune.MUSIC_TUNE_DAY_GRASSWALK || this.mApp.mMusic.mCurMusicTune == MusicTune.MUSIC_TUNE_POOL_WATERYGRAVES || this.mApp.mMusic.mCurMusicTune == MusicTune.MUSIC_TUNE_FOG_RIGORMORMIST || this.mApp.mMusic.mCurMusicTune == MusicTune.MUSIC_TUNE_ROOF_GRAZETHEROOF)
						{
							if (this.mHugeWaveCountDown == 399)
							{
								return;
							}
						}
						else if (this.mApp.mMusic.mCurMusicTune == MusicTune.MUSIC_TUNE_NIGHT_MOONGRAINS)
						{
							int num = this.mHugeWaveCountDown;
						}
						return;
					}
					this.mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
				}
			}
			if (this.mChallenge.UpdateZombieSpawning())
			{
				return;
			}
			if (this.mCurrentWave == this.mNumWaves)
			{
				if (this.IsFinalSurvivalStage())
				{
					return;
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
				{
					return;
				}
				if (!this.mApp.IsSurvivalMode() && !this.mApp.IsContinuousChallenge())
				{
					return;
				}
			}
			this.mZombieCountDown -= 3;
			if (this.mCurrentWave == this.mNumWaves && this.mApp.IsSurvivalMode())
			{
				if (this.mZombieCountDown >= 0 && this.mZombieCountDown < 3)
				{
					this.FadeOutLevel();
				}
				return;
			}
			int num2 = this.mZombieCountDownStart - this.mZombieCountDown;
			if (this.mZombieCountDown > 5 && num2 > 400)
			{
				int num3 = this.TotalZombiesHealthInWave(this.mCurrentWave - 1);
				if (num3 <= this.mZombieHealthToNextWave && this.mZombieCountDown > 201)
				{
					this.mZombieCountDown = 201;
				}
			}
			if (this.mZombieCountDown >= 5 && this.mZombieCountDown < 8)
			{
				if (this.IsFlagWave(this.mCurrentWave))
				{
					this.ClearAdviceImmediately();
					this.DisplayAdviceAgain("[ADVICE_HUGE_WAVE]", MessageStyle.MESSAGE_STYLE_HUGE_WAVE, AdviceType.ADVICE_HUGE_WAVE);
					this.mHugeWaveCountDown = 750;
					return;
				}
				this.NextWaveComing();
			}
			if (this.mZombieCountDown < 0 || this.mZombieCountDown >= 3)
			{
				return;
			}
			this.SpawnZombieWave();
			this.mZombieHealthWaveStart = this.TotalZombiesHealthInWave(this.mCurrentWave - 1);
			bool flag = this.mApp.IsWallnutBowlingLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND;
			if (this.mCurrentWave == this.mNumWaves && this.mApp.IsSurvivalMode())
			{
				this.mZombieHealthToNextWave = 0;
				this.mZombieCountDown = 5499;
			}
			else if (this.IsFlagWave(this.mCurrentWave) && !flag)
			{
				this.mZombieHealthToNextWave = 0;
				this.mZombieCountDown = 4500;
			}
			else
			{
				this.mZombieHealthToNextWave = (int)(TodCommon.RandRangeFloat(0.5f, 0.65f) * (float)this.mZombieHealthWaveStart);
				if (this.mApp.IsLittleTroubleLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
				{
					this.mZombieCountDown = 750;
				}
				else
				{
					this.mZombieCountDown = 2500 + RandomNumbers.NextNumber(600);
				}
			}
			this.mZombieCountDownStart = this.mZombieCountDown;
		}

		public void UpdateSunSpawning()
		{
			if (this.StageIsNight())
			{
				return;
			}
			if (this.HasLevelAwardDropped())
			{
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE || this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL || this.mApp.mGameMode == GameMode.GAMEMODE_INTRO || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND || this.mApp.IsIZombieLevel() || this.mApp.IsScaryPotterLevel() || this.mApp.IsSquirrelLevel() || this.HasConveyorBeltSeedBank())
			{
				return;
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_SLOT_MACHINE_PULL)
			{
				return;
			}
			if ((this.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PICK_UP_PEASHOOTER || this.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PLANT_PEASHOOTER) && this.mPlants.Count == 0)
			{
				return;
			}
			this.mSunCountDown -= 3;
			if (this.mSunCountDown > 0)
			{
				return;
			}
			int theX = Constants.LAWN_XMIN + RandomNumbers.NextNumber(Constants.Board_SunCoinRange);
			this.mNumSunsFallen++;
			this.mSunCountDown = Math.Min(950, 425 + this.mNumSunsFallen * 10) + RandomNumbers.NextNumber(275);
			CoinType theCoinType = CoinType.COIN_SUN;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SUNNY_DAY)
			{
				theCoinType = CoinType.COIN_LARGESUN;
			}
			this.AddCoin(theX, 60, theCoinType, CoinMotion.COIN_MOTION_FROM_SKY);
		}

		public void ClearAdvice(AdviceType theHelpIndex)
		{
			if (theHelpIndex != AdviceType.ADVICE_NONE && theHelpIndex != this.mHelpIndex)
			{
				return;
			}
			this.mAdvice.ClearLabel();
			this.mHelpIndex = AdviceType.ADVICE_NONE;
		}

		public bool RowCanHaveZombieType(int theRow, ZombieType theZombieType)
		{
			if (!this.RowCanHaveZombies(theRow))
			{
				return false;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RESODDED && this.mPlantRow[theRow] == PlantRowType.PLANTROW_DIRT && this.mCurrentWave < 5)
			{
				return false;
			}
			if (this.mPlantRow[theRow] == PlantRowType.PLANTROW_POOL && !Zombie.ZombieTypeCanGoInPool(theZombieType))
			{
				return false;
			}
			if (this.mPlantRow[theRow] == PlantRowType.PLANTROW_HIGH_GROUND && !Zombie.ZombieTypeCanGoOnHighGround(theZombieType))
			{
				return false;
			}
			int num = this.mCurrentWave;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				num += this.mChallenge.mSurvivalStage * this.GetNumWavesPerSurvivalStage();
			}
			return (this.mPlantRow[theRow] != PlantRowType.PLANTROW_POOL || num >= 5 || Board.IsZombieTypePoolOnly(theZombieType)) && (this.mPlantRow[theRow] == PlantRowType.PLANTROW_POOL || !Board.IsZombieTypePoolOnly(theZombieType)) && (theZombieType != ZombieType.ZOMBIE_BOBSLED || this.mIceTimer[theRow] > 0) && (theRow != 0 || this.mApp.IsSurvivalEndless(this.mApp.mGameMode) || (theZombieType != ZombieType.ZOMBIE_GARGANTUAR && theZombieType != ZombieType.ZOMBIE_REDEYE_GARGANTUAR)) && (theZombieType != ZombieType.ZOMBIE_DANCER || this.StageHasPool() || (this.RowCanHaveZombies(theRow - 1) && this.RowCanHaveZombies(theRow + 1)));
		}

		public int NumberZombiesInWave(int theWaveIndex)
		{
			Debug.ASSERT(theWaveIndex >= 0 && theWaveIndex < 100 && theWaveIndex < this.mNumWaves);
			for (int i = 0; i < 50; i++)
			{
				ZombieType zombieType = this.mZombiesInWave[theWaveIndex, i];
				if (zombieType == ZombieType.ZOMBIE_INVALID)
				{
					return i;
				}
			}
			Debug.ASSERT(false);
			return 0;
		}

		public int TotalZombiesHealthInWave(int theWaveIndex)
		{
			int num = 0;
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && zombie.mFromWave == theWaveIndex && !zombie.mMindControlled && !zombie.IsDeadOrDying() && zombie.mZombieType != ZombieType.ZOMBIE_BUNGEE && zombie.mRelatedZombieID == null)
				{
					num += zombie.mBodyHealth;
					num += zombie.mHelmHealth;
					num += (int)((float)zombie.mShieldHealth * 0.2f);
					num += zombie.mFlyingHealth;
				}
			}
			return num;
		}

		public void DrawUICoinBank(Graphics g)
		{
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING && this.mApp.mCrazyDaveState == CrazyDaveState.CRAZY_DAVE_OFF)
			{
				return;
			}
			if (this.mCoinBankFadeCount <= 0)
			{
				return;
			}
			int num = Constants.UICoinBankPosition.X;
			int num2 = Constants.UICoinBankPosition.Y - AtlasResources.IMAGE_COINBANK.mHeight - 1;
			if (this.mApp.IsSlotMachineLevel())
			{
				num -= 50;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				num = 450 - this.mX;
			}
			else if (this.mApp.mCrazyDaveState != CrazyDaveState.CRAZY_DAVE_OFF)
			{
				num = 150 - this.mX;
			}
			g.SetColorizeImages(true);
			int theAlpha = TodCommon.ClampInt(255 * this.mCoinBankFadeCount / 15, 0, 255);
			g.SetColor(new SexyColor(255, 255, 255, theAlpha));
			g.DrawImage(AtlasResources.IMAGE_COINBANK, num, num2);
			g.SetColor(new SexyColor(180, 255, 90, theAlpha));
			g.SetFont(Resources.FONT_CONTINUUMBOLD14);
			string moneyString = LawnApp.GetMoneyString(this.mApp.mPlayerInfo.mCoins);
			int theX = num + Constants.StoreScreen_Coinbank_TextOffset.X - Resources.FONT_CONTINUUMBOLD14.StringWidth(moneyString);
			g.DrawString(moneyString, theX, num2 + Constants.StoreScreen_Coinbank_TextOffset.Y);
			g.SetColorizeImages(false);
		}

		public void ShowCoinBank()
		{
			this.mCoinBankFadeCount = 1000;
		}

		public void FadeOutLevel()
		{
			if (this.mApp.IsScaryPotterLevel())
			{
				if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_ENDLESS && this.mChallenge.mSurvivalStage >= 14)
				{
					this.GrantAchievement(AchievementId.ChinaShop);
				}
			}
			else if (this.mApp.IsIZombieLevel())
			{
				if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS && this.mChallenge.mSurvivalStage >= 9)
				{
					this.GrantAchievement(AchievementId.BetterOffDead);
				}
			}
			else if (!this.mApp.IsQuickPlayMode() && !this.HasConveyorBeltSeedBank() && !this.mApp.IsWhackAZombieLevel())
			{
				if (this.AwardCloseShave())
				{
					this.GrantAchievement(AchievementId.CloseShave);
				}
				if (this.mNomNomNomAchievementTracker)
				{
					this.GrantAchievement(AchievementId.NomNomNom);
				}
				if (this.StageIsNight() && this.mNoFungusAmongUsAchievementTracker)
				{
					this.GrantAchievement(AchievementId.NoFungusAmongUs);
				}
				if (this.StageHasPool() && !this.mPeaShooterUsed)
				{
					this.GrantAchievement(AchievementId.DontPeainthePool);
				}
				if (this.StageHasRoof() && !this.mCatapultPlantsUsed)
				{
					this.GrantAchievement(AchievementId.Grounded);
				}
				if (this.StageIsDayWithoutPool() && this.mMushroomAndCoffeeBeansOnly)
				{
					this.GrantAchievement(AchievementId.GoodMorning);
				}
			}
			if (this.mLevel >= 3)
			{
				this.mApp.mPlayerInfo.mHasFinishedTutorial = true;
			}
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				this.RefreshSeedPacketFromCursor();
				this.mApp.mLastLevelStats.mUnusedLawnMowers = 0;
				this.mLevelComplete = true;
				return;
			}
			bool flag = true;
			if (this.mApp.IsScaryPotterLevel() && !this.IsFinalScaryPotterStage())
			{
				flag = false;
			}
			else if (this.IsSurvivalStageWithRepick())
			{
				flag = false;
			}
			else if (this.IsLastStandStageWithRepick())
			{
				flag = false;
			}
			else if (this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
			{
				flag = false;
			}
			if (flag)
			{
				this.mApp.mMusic.StopAllMusic();
				if (this.mApp.IsAdventureMode() && this.mLevel == 50)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_FINALFANFARE);
				}
				else if (this.mApp.TrophiesNeedForGoldSunflower() == 1)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_FINALFANFARE);
				}
				else
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_WINMUSIC);
				}
			}
			if (this.mApp.IsEndlessScaryPotter(this.mApp.mGameMode))
			{
				this.mLevelAwardSpawned = true;
				this.mNextSurvivalStageCounter = 500;
				string theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_MORE_SCARY_POTS]", "{STREAK}", this.mChallenge.mSurvivalStage + 1);
				this.PuzzleSaveStreak();
				this.ClearAdvice(AdviceType.ADVICE_NONE);
				this.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_BIG_MIDDLE, AdviceType.ADVICE_NONE);
				return;
			}
			if (this.mApp.IsAdventureMode() && this.mApp.IsScaryPotterLevel() && !this.IsFinalScaryPotterStage())
			{
				this.mNextSurvivalStageCounter = 500;
				this.ClearAdvice(AdviceType.ADVICE_NONE);
				return;
			}
			if (this.mApp.IsScaryPotterLevel() && !this.IsFinalScaryPotterStage())
			{
				this.mLevelAwardSpawned = true;
				this.mNextSurvivalStageCounter = 500;
				string theAdvice2 = TodCommon.TodReplaceNumberString("[ADVICE_3_IN_A_ROW]", "{STREAK}", this.mChallenge.mSurvivalStage + 1);
				this.PuzzleSaveStreak();
				this.ClearAdvice(AdviceType.ADVICE_NONE);
				this.DisplayAdvice(theAdvice2, MessageStyle.MESSAGE_STYLE_BIG_MIDDLE, AdviceType.ADVICE_NONE);
				return;
			}
			if (this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
			{
				this.mNextSurvivalStageCounter = 500;
				string theAdvice3 = TodCommon.TodReplaceNumberString("[ADVICE_MORE_IZOMBIE]", "{STREAK}", this.mChallenge.mSurvivalStage + 1);
				this.PuzzleSaveStreak();
				this.ClearAdvice(AdviceType.ADVICE_NONE);
				this.DisplayAdvice(theAdvice3, MessageStyle.MESSAGE_STYLE_BIG_MIDDLE, AdviceType.ADVICE_NONE);
				return;
			}
			if (this.IsLastStandStageWithRepick())
			{
				this.mNextSurvivalStageCounter = 500;
				this.mChallenge.LastStandCompletedStage();
				return;
			}
			if (!this.IsSurvivalStageWithRepick())
			{
				this.RefreshSeedPacketFromCursor();
				this.mApp.mLastLevelStats.mUnusedLawnMowers = this.CountUntriggerLawnMowers();
				this.mBoardFadeOutCounter = 600;
				bool flag2 = this.mLevel == 9 || this.mLevel == 19 || this.mLevel == 29 || this.mLevel == 39 || this.mLevel == 49;
				if (flag2)
				{
					this.mBoardFadeOutCounter = 500;
				}
				if (this.CanDropLoot())
				{
					this.mScoreNextMowerCounter = 200;
				}
				Coin coin = null;
				while (this.IterateCoins(ref coin))
				{
					coin.TryAutoCollectAfterLevelAward();
				}
				return;
			}
			Debug.ASSERT(this.mApp.IsSurvivalMode());
			this.mNextSurvivalStageCounter = 500;
			this.DisplayAdvice("[ADVICE_MORE_ZOMBIES]", MessageStyle.MESSAGE_STYLE_BIG_MIDDLE, AdviceType.ADVICE_NONE);
			this.mApp.mMusic.FadeOut(500);
			this.mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
			for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
			{
				if (this.mIceTimer[i] > this.mNextSurvivalStageCounter)
				{
					this.mIceTimer[i] = this.mNextSurvivalStageCounter;
				}
			}
		}

		public bool AwardCloseShave()
		{
			return this.GetBottomLawnMower() == null && (this.mBackground != BackgroundType.BACKGROUND_5_ROOF || this.mApp.mPlayerInfo.mPurchases[23] != 0) && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST;
		}

		public void DrawFadeOut(Graphics g)
		{
			if (this.mBoardFadeOutCounter < 0)
			{
				return;
			}
			if (this.IsSurvivalStageWithRepick())
			{
				return;
			}
			int theAlpha = TodCommon.TodAnimateCurve(200, 0, this.mBoardFadeOutCounter, 0, 255, TodCurves.CURVE_LINEAR);
			if (this.mLevel == 9 || this.mLevel == 19 || this.mLevel == 29 || this.mLevel == 39 || this.mLevel == 49)
			{
				g.SetColor(new SexyColor(0, 0, 0, theAlpha, false));
			}
			else
			{
				g.SetColor(new SexyColor(255, 255, 255, theAlpha, false));
			}
			g.SetColorizeImages(true);
			g.FillRect(-Constants.Board_Offset_AspectRatio_Correction, 0, this.mWidth, this.mHeight);
		}

		public void DrawIce(Graphics g, int y)
		{
			int theY = (int)((float)(this.GridToPixelY(8, y) + 20) * Constants.S);
			int height = AtlasResources.IMAGE_ICE.GetHeight();
			int num = TodCommon.ClampInt((int)((float)(255 * this.mIceTimer[y]) / 10f), 0, 255);
			if (num < 255)
			{
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, num, true));
			}
			int num2 = (int)((float)this.mIceMinX[y] * Constants.S);
			int num3 = 8;
			int num4 = num2 + num3;
			int width = AtlasResources.IMAGE_ICE.GetWidth();
			int board_WIDTH = Constants.BOARD_WIDTH;
			int num5;
			for (int i = num4; i < board_WIDTH; i += num5)
			{
				if (i == num4)
				{
					num5 = (board_WIDTH - num4) % width;
					if (num5 == 0)
					{
						num5 = width;
					}
				}
				else
				{
					num5 = width;
				}
				TRect theSrcRect = new TRect(width - num5, 0, num5, height);
				TRect theDestRect = new TRect(i, theY, num5, height);
				g.DrawImage(AtlasResources.IMAGE_ICE, theDestRect, theSrcRect);
			}
			g.DrawImage(AtlasResources.IMAGE_ICE_CAP, num2, theY);
			g.SetColorizeImages(false);
		}

		public void DrawCelHighlight(Graphics g, int theCol, int theRow)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				return;
			}
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
			SexyColor aColor = default(SexyColor);
			aColor.mAlpha = 0;
			aColor.mRed = (aColor.mGreen = (aColor.mBlue = 50));
			g.SetColor(aColor);
			TPoint[] celPosition = this.GetCelPosition(theCol, theRow);
			celPosition[0].mX = (int)((float)celPosition[0].mX * Constants.S);
			celPosition[0].mY = (int)((float)celPosition[0].mY * Constants.S);
			celPosition[1].mX = (int)((float)celPosition[1].mX * Constants.S);
			celPosition[1].mY = (int)((float)celPosition[1].mY * Constants.S);
			celPosition[2].mX = (int)((float)celPosition[2].mX * Constants.S);
			celPosition[2].mY = (int)((float)celPosition[2].mY * Constants.S);
			celPosition[3].mX = (int)((float)celPosition[3].mX * Constants.S);
			celPosition[3].mY = (int)((float)celPosition[3].mY * Constants.S);
			g.SetColorizeImages(true);
			g.FillRect(new TRect(Board.mCelPoints[0].mX, Board.mCelPoints[0].mY, Board.mCelPoints[2].mX - Board.mCelPoints[0].mX, Board.mCelPoints[2].mY - Board.mCelPoints[0].mY));
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
		}

		public TPoint[] GetCelPosition(int theCol, int theRow)
		{
			int num = this.StageHas6Rows() ? 6 : 5;
			if (this.StageHasPool())
			{
				int[] board_Cel_Y_Values_Pool = Constants.Board_Cel_Y_Values_Pool;
				Board.mCelPoints[0].mX = this.GridToPixelX(theCol, theRow);
				Board.mCelPoints[0].mY = board_Cel_Y_Values_Pool[theRow];
				if (theCol == Constants.GRIDSIZEX - 1)
				{
					Board.mCelPoints[1].mX = 878;
				}
				else
				{
					Board.mCelPoints[1].mX = Board.mCelPoints[0].mX + 80;
				}
				Board.mCelPoints[1].mY = board_Cel_Y_Values_Pool[theRow];
				Board.mCelPoints[2].mX = Board.mCelPoints[1].mX;
				Board.mCelPoints[2].mY = board_Cel_Y_Values_Pool[theRow + 1];
				Board.mCelPoints[3].mX = Board.mCelPoints[0].mX;
				Board.mCelPoints[3].mY = board_Cel_Y_Values_Pool[theRow + 1];
			}
			else if (this.StageHasRoof())
			{
				int num2 = 20;
				Board.mCelPoints[0].mX = this.GridToPixelX(theCol, theRow);
				Board.mCelPoints[0].mY = this.GridToPixelY(theCol, theRow) + num2;
				int num3 = 80;
				Board.mCelPoints[1].mX = Board.mCelPoints[0].mX + num3;
				Board.mCelPoints[1].mY = Board.mCelPoints[0].mY;
				Board.mCelPoints[2].mX = Board.mCelPoints[1].mX;
				Board.mCelPoints[2].mY = ((theRow < num - 1) ? (this.GridToPixelY(theCol, theRow + 1) + num2) : (Board.mCelPoints[0].mY + num3));
				Board.mCelPoints[3].mX = Board.mCelPoints[0].mX;
				Board.mCelPoints[3].mY = Board.mCelPoints[2].mY;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				int[] board_Cel_Y_Values_ZenGarden = Constants.Board_Cel_Y_Values_ZenGarden;
				Board.mCelPoints[0].mX = this.GridToPixelX(theCol, theRow);
				Board.mCelPoints[0].mY = board_Cel_Y_Values_ZenGarden[theRow];
				if (theCol == Constants.GRIDSIZEX - 1)
				{
					Board.mCelPoints[1].mX = 878;
				}
				else
				{
					Board.mCelPoints[1].mX = Board.mCelPoints[0].mX + 80;
				}
				Board.mCelPoints[1].mY = board_Cel_Y_Values_ZenGarden[theRow];
				Board.mCelPoints[2].mX = Board.mCelPoints[1].mX;
				Board.mCelPoints[2].mY = board_Cel_Y_Values_ZenGarden[theRow + 1];
				Board.mCelPoints[3].mX = Board.mCelPoints[0].mX;
				Board.mCelPoints[3].mY = board_Cel_Y_Values_ZenGarden[theRow + 1];
			}
			else
			{
				int[] board_Cel_Y_Values_Normal = Constants.Board_Cel_Y_Values_Normal;
				Board.mCelPoints[0].mX = this.GridToPixelX(theCol, theRow);
				Board.mCelPoints[0].mY = board_Cel_Y_Values_Normal[theRow];
				if (theCol == Constants.GRIDSIZEX - 1)
				{
					Board.mCelPoints[1].mX = 878;
				}
				else
				{
					Board.mCelPoints[1].mX = Board.mCelPoints[0].mX + 80;
				}
				Board.mCelPoints[1].mY = board_Cel_Y_Values_Normal[theRow];
				Board.mCelPoints[2].mX = Board.mCelPoints[1].mX;
				Board.mCelPoints[2].mY = board_Cel_Y_Values_Normal[theRow + 1];
				Board.mCelPoints[3].mX = Board.mCelPoints[0].mX;
				Board.mCelPoints[3].mY = board_Cel_Y_Values_Normal[theRow + 1];
			}
			return Board.mCelPoints;
		}

		public bool IsIceAt(int theGridX, int theGridY)
		{
			Debug.ASSERT(theGridY >= 0 && theGridY < Constants.MAX_GRIDSIZEY);
			if (this.mIceTimer[theGridY] <= 0)
			{
				return false;
			}
			if (this.mIceMinX[theGridY] > 750 + Constants.BOARD_EXTRA_ROOM)
			{
				return false;
			}
			int num = this.PixelToGridXKeepOnBoard(this.mIceMinX[theGridY] + 12, 0);
			return theGridX >= num;
		}

		public Zombie ZombieGetID(Zombie theZombie)
		{
			if (this.mZombies.IndexOf(theZombie) != -1)
			{
				return theZombie;
			}
			return null;
		}

		public Zombie ZombieGet(Zombie theZombieID)
		{
			return this.mZombies[this.mZombies.IndexOf(theZombieID)];
		}

		public Zombie ZombieTryToGet(Zombie theZombieID)
		{
			int num = this.mZombies.IndexOf(theZombieID);
			if (num != -1)
			{
				return this.mZombies[num];
			}
			return null;
		}

		public static void ZombiePickerInitForWave(ZombiePicker theZombiePicker)
		{
			theZombiePicker.mZombieCount = 0;
			theZombiePicker.mZombiePoints = 0;
			for (int i = 0; i < 33; i++)
			{
				theZombiePicker.mZombieTypeCount[i] = 0;
			}
		}

		public static void ZombiePickerInit(ZombiePicker theZombiePicker)
		{
			Board.ZombiePickerInitForWave(theZombiePicker);
			for (int i = 0; i < 33; i++)
			{
				theZombiePicker.mAllWavesZombieTypeCount[i] = 0;
			}
		}

		public void UpdateIce()
		{
			for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
			{
				if (this.mIceTimer[i] > 0)
				{
					this.mIceTimer[i] -= 3;
					if (this.mIceTimer[i] < 0)
					{
						this.mIceTimer[i] = 0;
					}
					if (this.mIceTimer[i] >= 0 && this.mIceTimer[i] < 3)
					{
						this.mIceMinX[i] = Constants.Board_Ice_Start;
					}
				}
			}
		}

		public int GetIceZPos(int theRow)
		{
			return 200000 + 10000 * theRow + 2;
		}

		public bool CanAddBobSled()
		{
			for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
			{
				if (this.mIceTimer[i] > 0 && this.mIceMinX[i] < 700 + Constants.BOARD_EXTRA_ROOM)
				{
					return true;
				}
			}
			return false;
		}

		public void ShakeBoard(int theShakeAmountX, int theShakeAmountY)
		{
			this.mShakeCounter = 12;
			this.mShakeAmountX = theShakeAmountX;
			this.mShakeAmountY = theShakeAmountY;
		}

		public int CountUntriggerLawnMowers()
		{
			int num = 0;
			LawnMower lawnMower = null;
			while (this.IterateLawnMowers(ref lawnMower))
			{
				if (lawnMower.mMowerState != LawnMowerState.MOWER_TRIGGERED && lawnMower.mMowerState != LawnMowerState.MOWER_SQUISHED)
				{
					num++;
				}
			}
			return num;
		}

		public bool IterateZombies(ref Zombie theZombie, ref int index)
		{
			if (index == -1 || index >= this.mZombies.Count)
			{
				index = this.mZombies.IndexOf(theZombie);
			}
			while (++index < this.mZombies.Count)
			{
				theZombie = this.mZombies[index];
				if (!theZombie.mDead)
				{
					return true;
				}
			}
			return false;
		}

		public bool IteratePlants(ref Plant thePlant, ref int index)
		{
			if (index == -1 || index >= this.mPlants.Count)
			{
				index = this.mPlants.IndexOf(thePlant);
			}
			while (++index < this.mPlants.Count)
			{
				thePlant = this.mPlants[index];
				if (!thePlant.mDead)
				{
					return true;
				}
			}
			return false;
		}

		public bool IterateProjectiles(ref Projectile theProjectile, ref int index)
		{
			if (index == -1 || index >= this.mProjectiles.Count)
			{
				index = this.mProjectiles.IndexOf(theProjectile);
			}
			while (++index < this.mProjectiles.Count)
			{
				theProjectile = this.mProjectiles[index];
				if (!theProjectile.mDead)
				{
					return true;
				}
			}
			return false;
		}

		public bool IterateCoins(ref Coin theCoin)
		{
			int num = this.mCoins.IndexOf(theCoin);
			for (int i = num + 1; i < this.mCoins.Count; i++)
			{
				theCoin = this.mCoins[i];
				if (!theCoin.mDead)
				{
					return true;
				}
			}
			return false;
		}

		public bool IterateLawnMowers(ref LawnMower theLawnMower)
		{
			int num = this.mLawnMowers.IndexOf(theLawnMower);
			for (int i = num + 1; i < this.mLawnMowers.Count; i++)
			{
				theLawnMower = this.mLawnMowers[i];
				if (!theLawnMower.mDead)
				{
					return true;
				}
			}
			return false;
		}

		public bool IterateParticles(ref TodParticleSystem theParticle, ref int index)
		{
			if (index == -1 || index >= this.mApp.mEffectSystem.mParticleHolder.mParticleSystems.Count)
			{
				index = this.mApp.mEffectSystem.mParticleHolder.mParticleSystems.IndexOf(theParticle);
			}
			while (++index < this.mApp.mEffectSystem.mParticleHolder.mParticleSystems.Count)
			{
				theParticle = this.mApp.mEffectSystem.mParticleHolder.mParticleSystems[index];
				if (theParticle != null && !theParticle.mDead)
				{
					return true;
				}
			}
			return false;
		}

		public bool IterateReanimations(ref Reanimation theReanimation, ref int index)
		{
			if (index == -1 || index >= this.mApp.mEffectSystem.mReanimationHolder.mReanimations.Count)
			{
				index = this.mApp.mEffectSystem.mReanimationHolder.mReanimations.IndexOf(theReanimation);
			}
			while (++index < this.mApp.mEffectSystem.mReanimationHolder.mReanimations.Count)
			{
				theReanimation = this.mApp.mEffectSystem.mReanimationHolder.mReanimations[index];
				if (theReanimation != null && !theReanimation.mDead)
				{
					return true;
				}
			}
			return false;
		}

		public bool IterateGridItems(ref GridItem theGridItem, ref int index)
		{
			if (index == -1 || index >= this.mGridItems.Count)
			{
				index = this.mGridItems.IndexOf(theGridItem);
			}
			while (++index < this.mGridItems.Count)
			{
				theGridItem = this.mGridItems[index];
				if (!theGridItem.mDead)
				{
					return true;
				}
			}
			return false;
		}

		public void ZombieSwitchRow(Zombie aZombie, int aRow)
		{
			List<Zombie> zombiesInRow = this.GetZombiesInRow(aZombie.mRow);
			List<Zombie> zombiesInRow2 = this.GetZombiesInRow(aRow);
			zombiesInRow.Remove(aZombie);
			zombiesInRow2.Add(aZombie);
		}

		public void SortZombieRowLists()
		{
			this.mZombiesRow1.Sort();
			this.mZombiesRow2.Sort();
			this.mZombiesRow3.Sort();
			this.mZombiesRow4.Sort();
			this.mZombiesRow5.Sort();
			this.mZombiesRow6.Sort();
		}

		public List<Zombie> GetZombiesInRow(int aRow)
		{
			if (aRow == 0)
			{
				return this.mZombiesRow1;
			}
			if (aRow == 1)
			{
				return this.mZombiesRow2;
			}
			if (aRow == 2)
			{
				return this.mZombiesRow3;
			}
			if (aRow == 3)
			{
				return this.mZombiesRow4;
			}
			if (aRow == 4)
			{
				return this.mZombiesRow5;
			}
			return this.mZombiesRow6;
		}

		public void AddToZombieList(Zombie aZombie)
		{
			this.mZombies.Add(aZombie);
			if (aZombie.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				for (int i = 0; i < 6; i++)
				{
					List<Zombie> zombiesInRow = this.GetZombiesInRow(i);
					zombiesInRow.Add(aZombie);
				}
				return;
			}
			List<Zombie> zombiesInRow2 = this.GetZombiesInRow(aZombie.mRow);
			zombiesInRow2.Add(aZombie);
		}

		public void AddToZombieList(Zombie aZombie, int row)
		{
			this.mZombies.Add(aZombie);
			List<Zombie> zombiesInRow = this.GetZombiesInRow(row);
			zombiesInRow.Add(aZombie);
		}

		public Zombie AddZombieInRow(ZombieType theZombieType, int theRow, int theFromWave)
		{
			if (this.mZombies.Count >= this.mZombies.Capacity - 1)
			{
				return null;
			}
			bool theVariant = false;
			if (RandomNumbers.NextNumber(5) == 0)
			{
				theVariant = true;
			}
			Zombie newZombie = Zombie.GetNewZombie();
			newZombie.ZombieInitialize(theRow, theZombieType, theVariant, null, theFromWave);
			this.AddToZombieList(newZombie);
			if (theZombieType == ZombieType.ZOMBIE_BOBSLED && newZombie.IsOnBoard())
			{
				Zombie newZombie2 = Zombie.GetNewZombie();
				Zombie newZombie3 = Zombie.GetNewZombie();
				Zombie newZombie4 = Zombie.GetNewZombie();
				this.AddToZombieList(newZombie2, theRow);
				this.AddToZombieList(newZombie3, theRow);
				this.AddToZombieList(newZombie4, theRow);
				newZombie2.ZombieInitialize(theRow, ZombieType.ZOMBIE_BOBSLED, false, newZombie, theFromWave);
				newZombie3.ZombieInitialize(theRow, ZombieType.ZOMBIE_BOBSLED, false, newZombie, theFromWave);
				newZombie4.ZombieInitialize(theRow, ZombieType.ZOMBIE_BOBSLED, false, newZombie, theFromWave);
			}
			return newZombie;
		}

		public bool IsPoolSquare(int theGridX, int theGridY)
		{
			return theGridX >= 0 && theGridY >= 0 && this.mGridSquareType[theGridX, theGridY] == GridSquareType.GRIDSQUARE_POOL;
		}

		public void PickZombieWaves()
		{
			if ((this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()) && this.mApp.IsWhackAZombieLevel())
			{
				this.mNumWaves = 8;
			}
			else if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
			{
				int num = TodCommon.ClampInt(this.mLevel - 1, 0, 49);
				this.mNumWaves = GameConstants.gZombieWaves[num];
				if (!this.mApp.IsFirstTimeAdventureMode() && !this.mApp.IsMiniBossLevel())
				{
					if (this.mNumWaves < 10)
					{
						this.mNumWaves = 20;
					}
					else
					{
						this.mNumWaves += 10;
					}
				}
			}
			else if (this.mApp.IsSurvivalMode() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				this.mNumWaves = this.GetNumWavesPerSurvivalStage();
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM || this.mApp.IsSquirrelLevel())
			{
				this.mNumWaves = 0;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WHACK_A_ZOMBIE)
			{
				this.mNumWaves = 12;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_AIR_RAID || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_GRAVE_DANGER || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
			{
				this.mNumWaves = 20;
			}
			else if (this.mApp.IsStormyNightLevel() || this.mApp.IsLittleTroubleLevel() || this.mApp.IsBungeeBlitzLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN || this.mApp.IsShovelLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS_2 || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING_2 || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_POGO_PARTY)
			{
				this.mNumWaves = 30;
			}
			else
			{
				this.mNumWaves = 40;
			}
			ZombiePicker zombiePicker = new ZombiePicker();
			Board.ZombiePickerInit(zombiePicker);
			ZombieType introducedZombieType = this.GetIntroducedZombieType();
			Debug.ASSERT(this.mNumWaves <= 100);
			int i = 0;
			while (i < this.mNumWaves)
			{
				Board.ZombiePickerInitForWave(zombiePicker);
				this.mZombiesInWave[i, 0] = ZombieType.ZOMBIE_INVALID;
				bool flag = this.IsFlagWave(i);
				bool flag2 = i == this.mNumWaves - 1;
				if (!this.mApp.IsBungeeBlitzLevel() || !flag)
				{
					goto IL_2E4;
				}
				for (int j = 0; j < 5; j++)
				{
					this.PutZombieInWave(ZombieType.ZOMBIE_BUNGEE, i, zombiePicker);
				}
				if (flag2)
				{
					goto IL_2E4;
				}
				if ((this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()) && flag2)
				{
					this.PutInMissingZombies(i, zombiePicker);
				}
				IL_65F:
				i++;
				continue;
				IL_2E4:
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
				{
					zombiePicker.mZombiePoints = (this.mChallenge.mSurvivalStage * this.GetNumWavesPerSurvivalStage() + i + 10) * 2 / 5 + 1;
				}
				else if (this.mApp.IsSurvivalMode() && this.mChallenge.mSurvivalStage > 0)
				{
					zombiePicker.mZombiePoints = (this.mChallenge.mSurvivalStage * this.GetNumWavesPerSurvivalStage() + i) * 2 / 5 + 1;
				}
				else if (this.mApp.IsAdventureMode() && this.mApp.HasFinishedAdventure() && this.mLevel != 5)
				{
					zombiePicker.mZombiePoints = i * 2 / 5 + 1;
				}
				else
				{
					zombiePicker.mZombiePoints = i / 3 + 1;
				}
				if (flag)
				{
					int num2 = Math.Min(zombiePicker.mZombiePoints, 8);
					zombiePicker.mZombiePoints = (int)((float)zombiePicker.mZombiePoints * 2.5f);
					if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS_2)
					{
						for (int k = 0; k < num2; k++)
						{
							this.PutZombieInWave(ZombieType.ZOMBIE_NORMAL, i, zombiePicker);
						}
						this.PutZombieInWave(ZombieType.ZOMBIE_FLAG, i, zombiePicker);
					}
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
				{
					zombiePicker.mZombiePoints *= 6;
				}
				else if (this.mApp.IsLittleTroubleLevel() || this.mApp.IsWallnutBowlingLevel())
				{
					zombiePicker.mZombiePoints *= 4;
				}
				else if (this.mApp.IsMiniBossLevel())
				{
					zombiePicker.mZombiePoints *= 3;
				}
				else if (this.mApp.IsStormyNightLevel() && (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()))
				{
					zombiePicker.mZombiePoints *= 3;
				}
				else if (this.mApp.IsShovelLevel() || this.mApp.IsBungeeBlitzLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
				{
					zombiePicker.mZombiePoints *= 2;
				}
				if (introducedZombieType != ZombieType.ZOMBIE_INVALID && introducedZombieType != ZombieType.ZOMBIE_DUCKY_TUBE)
				{
					bool flag3 = false;
					if (introducedZombieType == ZombieType.ZOMBIE_DIGGER || introducedZombieType == ZombieType.ZOMBIE_BALLOON)
					{
						if (i + 1 == 7 || flag2)
						{
							flag3 = true;
						}
					}
					else if (introducedZombieType == ZombieType.ZOMBIE_YETI)
					{
						if (i == this.mNumWaves / 2 && !this.mApp.mKilledYetiAndRestarted && !this.mApp.IsQuickPlayMode())
						{
							flag3 = true;
						}
					}
					else if (i == this.mNumWaves / 2 || flag2)
					{
						flag3 = true;
					}
					if (flag3)
					{
						this.PutZombieInWave(introducedZombieType, i, zombiePicker);
					}
				}
				if (this.mLevel == 50 && flag2)
				{
					this.PutZombieInWave(ZombieType.ZOMBIE_GARGANTUAR, i, zombiePicker);
				}
				if ((this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()) && flag2)
				{
					this.PutInMissingZombies(i, zombiePicker);
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
				{
					if (i % 10 == 5)
					{
						for (int l = 0; l < 10; l++)
						{
							this.PutZombieInWave(ZombieType.ZOMBIE_LADDER, i, zombiePicker);
						}
					}
					if (i % 10 == 8)
					{
						for (int m = 0; m < 10; m++)
						{
							this.PutZombieInWave(ZombieType.ZOMBIE_JACK_IN_THE_BOX, i, zombiePicker);
						}
					}
					if (i == 19)
					{
						for (int n = 0; n < 3; n++)
						{
							this.PutZombieInWave(ZombieType.ZOMBIE_GARGANTUAR, i, zombiePicker);
						}
					}
					if (i == 29)
					{
						for (int num3 = 0; num3 < 5; num3++)
						{
							this.PutZombieInWave(ZombieType.ZOMBIE_GARGANTUAR, i, zombiePicker);
						}
					}
				}
				while (zombiePicker.mZombiePoints > 0 && zombiePicker.mZombieCount < 50)
				{
					ZombieType theZombieType = this.PickZombieType(zombiePicker.mZombiePoints, i, zombiePicker);
					this.PutZombieInWave(theZombieType, i, zombiePicker);
				}
				int mZombiePoints = zombiePicker.mZombiePoints;
				goto IL_65F;
			}
		}

		public void StopAllZombieSounds()
		{
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead)
				{
					zombie.StopZombieSound();
				}
			}
		}

		public bool HasLevelAwardDropped()
		{
			return this.mLevelAwardSpawned || (this.mNextSurvivalStageCounter > 0 || this.mBoardFadeOutCounter >= 0);
		}

		public void UpdateProgressMeter()
		{
			if (this.mApp.IsFinalBossLevel())
			{
				Zombie bossZombie = this.GetBossZombie();
				if (bossZombie != null && !bossZombie.IsDeadOrDying())
				{
					this.mProgressMeterWidth = 150 * (bossZombie.mBodyMaxHealth - bossZombie.mBodyHealth) / bossZombie.mBodyMaxHealth;
					return;
				}
				this.mProgressMeterWidth = 150;
				return;
			}
			else
			{
				if (this.mCurrentWave == 0)
				{
					return;
				}
				if (this.mFlagRaiseCounter > 0)
				{
					this.mFlagRaiseCounter -= 3;
				}
				int num = 150;
				int numWavesPerFlag = this.GetNumWavesPerFlag();
				if (this.ProgressMeterHasFlags())
				{
					int num2 = this.mNumWaves / numWavesPerFlag;
					num -= num2 * 12;
				}
				int num3 = num / (this.mNumWaves - 1);
				int num4 = (this.mCurrentWave - 1) * num / (this.mNumWaves - 1);
				int num5 = this.mCurrentWave * num / (this.mNumWaves - 1);
				if (this.ProgressMeterHasFlags())
				{
					int num6 = this.mCurrentWave / numWavesPerFlag;
					num4 += num6 * 12;
					num5 += num6 * 12;
				}
				float num7 = (float)(this.mZombieCountDownStart - this.mZombieCountDown) / (float)this.mZombieCountDownStart;
				if (this.mZombieHealthToNextWave != -1)
				{
					int num8 = this.TotalZombiesHealthInWave(this.mCurrentWave - 1);
					int num9 = Math.Max(this.mZombieHealthWaveStart - this.mZombieHealthToNextWave, 1);
					float num10 = (float)(num9 - num8 + this.mZombieHealthToNextWave) / (float)num9;
					if (num10 > num7)
					{
						num7 = num10;
					}
				}
				int num11 = num4 + TodCommon.FloatRoundToInt((float)(num5 - num4) * num7);
				num11 = TodCommon.ClampInt(num11, 1, 150);
				int num12 = num11 - this.mProgressMeterWidth;
				if (num12 > num3 && Board.IsInModRange(this.mMainCounter, 5))
				{
					this.mProgressMeterWidth++;
					return;
				}
				if (num12 > 0 && Board.IsInModRange(this.mMainCounter, 20))
				{
					this.mProgressMeterWidth++;
				}
				return;
			}
		}

		public static bool IsInModRange(int number, int mod)
		{
			return number % mod == 0 || (number - 1) % mod == 0 || (number + 1) % mod == 0;
		}

		public void DrawUIBottom(Graphics g)
		{
			if (this.mApp.mGameScene != GameScenes.SCENE_ZOMBIES_WON)
			{
				if (this.mSeedBank.BeginDraw(g))
				{
					this.mSeedBank.DrawSun(g);
					this.mSeedBank.EndDraw(g);
				}
				MessageStyle mMessageStyle = this.mAdvice.mMessageStyle;
			}
			this.DrawShovel(g);
			if (!this.StageHasFog())
			{
				this.DrawTopRightUI(g);
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_PLAYING)
			{
				this.DrawProgressMeter(g);
			}
		}

		public void DrawUITop(Graphics g)
		{
			if (this.mApp.mGameScene != GameScenes.SCENE_ZOMBIES_WON && this.mSeedBank.BeginDraw(g))
			{
				this.mSeedBank.Draw(g);
				this.mSeedBank.EndDraw(g);
			}
			if (this.StageHasFog())
			{
				this.DrawTopRightUI(g);
			}
			if (this.mTimeStopCounter > 0)
			{
				g.SetColor(new SexyColor(200, 200, 200, 210));
				g.FillRect(0, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				this.mChallenge.DrawSlotMachine(g);
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_PLAYING)
			{
				this.DrawLevel(g);
			}
			if (this.mStoreButton != null && this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				this.mStoreButton.Draw(g);
			}
			if ((this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL || this.mApp.mGameMode == GameMode.GAMEMODE_INTRO) && this.mCutScene.mUpsellHideBoard)
			{
				g.SetColor(new SexyColor(0, 0, 0));
				g.FillRect(-Constants.Board_Offset_AspectRatio_Correction, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL)
			{
				this.mCutScene.DrawUpsell(g);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_INTRO)
			{
				this.mCutScene.DrawIntro(g);
			}
			if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM || this.IsScaryPotterDaveTalking())
			{
				Graphics @new = Graphics.GetNew(g);
				@new.mTransX -= this.mX;
				@new.mTransY -= this.mY;
				this.mApp.DrawCrazyDave(@new);
				@new.PrepareForReuse();
			}
			this.mAdvice.Draw(g);
		}

		public Zombie ZombieHitTest(int theMouseX, int theMouseY)
		{
			Zombie zombie = null;
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie2 = this.mZombies[i];
				if (!zombie2.mDead && !zombie2.IsDeadOrDying() && (this.mApp.mGameScene != GameScenes.SCENE_LEVEL_INTRO || (zombie2.mZombieType != ZombieType.ZOMBIE_PEA_HEAD && zombie2.mZombieType != ZombieType.ZOMBIE_WALLNUT_HEAD && zombie2.mZombieType != ZombieType.ZOMBIE_TALLNUT_HEAD && zombie2.mZombieType != ZombieType.ZOMBIE_JALAPENO_HEAD && zombie2.mZombieType != ZombieType.ZOMBIE_GATLING_HEAD && zombie2.mZombieType != ZombieType.ZOMBIE_SQUASH_HEAD)) && zombie2.GetZombieRect().Contains(theMouseX, theMouseY) && (zombie == null || zombie2.mY > zombie.mY))
				{
					zombie = zombie2;
				}
			}
			return zombie;
		}

		public void KillAllPlantsInRadius(int theX, int theY, int theRadius)
		{
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead)
				{
					TRect plantRect = plant.GetPlantRect();
					if (GameConstants.GetCircleRectOverlap(theX, theY, theRadius, plantRect))
					{
						this.mPlantsEaten++;
						plant.Die();
					}
				}
			}
		}

		public Plant GetPumpkinAt(int theGridX, int theGridY)
		{
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead && plant.mPlantCol == theGridX && plant.mRow == theGridY && !plant.NotOnGround() && plant.mSeedType == SeedType.SEED_PUMPKINSHELL)
				{
					return plant;
				}
			}
			return null;
		}

		public Plant GetFlowerPotAt(int theGridX, int theGridY)
		{
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead && plant.mPlantCol == theGridX && plant.mRow == theGridY && !plant.NotOnGround() && plant.mSeedType == SeedType.SEED_FLOWERPOT)
				{
					return plant;
				}
			}
			return null;
		}

		public static bool CanZombieSpawnOnLevel(ZombieType theZombieType, int theLevel)
		{
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
			if (theZombieType == ZombieType.ZOMBIE_YETI)
			{
				return GlobalStaticVars.gLawnApp.CanSpawnYetis();
			}
			if (theLevel < zombieDefinition.mStartingLevel)
			{
				return false;
			}
			if (zombieDefinition.mPickWeight == 0)
			{
				return false;
			}
			Debug.ASSERT(GameConstants.gZombieAllowedLevels[(int)theZombieType].mZombieType == theZombieType);
			int num = TodCommon.ClampInt(theLevel - 1, 0, 49);
			return GameConstants.gZombieAllowedLevels[(int)theZombieType].mAllowedOnLevel[num] != 0;
		}

		public bool IsZombieWaveDistributionOk()
		{
			if (!this.mApp.IsAdventureMode() && !this.mApp.IsQuickPlayMode())
			{
				return true;
			}
			int[] array = new int[33];
			for (int i = 0; i < 33; i++)
			{
				array[i] = 0;
			}
			for (int j = 0; j < this.mNumWaves; j++)
			{
				for (int k = 0; k < 50; k++)
				{
					ZombieType zombieType = this.mZombiesInWave[j, k];
					if (zombieType == ZombieType.ZOMBIE_INVALID)
					{
						break;
					}
					Debug.ASSERT(zombieType >= ZombieType.ZOMBIE_NORMAL && zombieType < ZombieType.NUM_ZOMBIE_TYPES);
					array[(int)zombieType]++;
				}
			}
			for (int l = 0; l < 33; l++)
			{
				if (l != 19 && Board.CanZombieSpawnOnLevel((ZombieType)l, this.mLevel) && array[l] == 0)
				{
					return false;
				}
			}
			return true;
		}

		public void PickBackground()
		{
			switch (this.mApp.mGameMode)
			{
			case GameMode.GAMEMODE_ADVENTURE:
			case GameMode.GAMEMODE_QUICKPLAY_1:
			case GameMode.GAMEMODE_QUICKPLAY_2:
			case GameMode.GAMEMODE_QUICKPLAY_3:
			case GameMode.GAMEMODE_QUICKPLAY_4:
			case GameMode.GAMEMODE_QUICKPLAY_5:
			case GameMode.GAMEMODE_QUICKPLAY_6:
			case GameMode.GAMEMODE_QUICKPLAY_7:
			case GameMode.GAMEMODE_QUICKPLAY_8:
			case GameMode.GAMEMODE_QUICKPLAY_9:
			case GameMode.GAMEMODE_QUICKPLAY_10:
			case GameMode.GAMEMODE_QUICKPLAY_11:
			case GameMode.GAMEMODE_QUICKPLAY_12:
			case GameMode.GAMEMODE_QUICKPLAY_13:
			case GameMode.GAMEMODE_QUICKPLAY_14:
			case GameMode.GAMEMODE_QUICKPLAY_15:
			case GameMode.GAMEMODE_QUICKPLAY_16:
			case GameMode.GAMEMODE_QUICKPLAY_17:
			case GameMode.GAMEMODE_QUICKPLAY_18:
			case GameMode.GAMEMODE_QUICKPLAY_19:
			case GameMode.GAMEMODE_QUICKPLAY_20:
			case GameMode.GAMEMODE_QUICKPLAY_21:
			case GameMode.GAMEMODE_QUICKPLAY_22:
			case GameMode.GAMEMODE_QUICKPLAY_23:
			case GameMode.GAMEMODE_QUICKPLAY_24:
			case GameMode.GAMEMODE_QUICKPLAY_25:
			case GameMode.GAMEMODE_QUICKPLAY_26:
			case GameMode.GAMEMODE_QUICKPLAY_27:
			case GameMode.GAMEMODE_QUICKPLAY_28:
			case GameMode.GAMEMODE_QUICKPLAY_29:
			case GameMode.GAMEMODE_QUICKPLAY_30:
			case GameMode.GAMEMODE_QUICKPLAY_31:
			case GameMode.GAMEMODE_QUICKPLAY_32:
			case GameMode.GAMEMODE_QUICKPLAY_33:
			case GameMode.GAMEMODE_QUICKPLAY_34:
			case GameMode.GAMEMODE_QUICKPLAY_35:
			case GameMode.GAMEMODE_QUICKPLAY_36:
			case GameMode.GAMEMODE_QUICKPLAY_37:
			case GameMode.GAMEMODE_QUICKPLAY_38:
			case GameMode.GAMEMODE_QUICKPLAY_39:
			case GameMode.GAMEMODE_QUICKPLAY_40:
			case GameMode.GAMEMODE_QUICKPLAY_41:
			case GameMode.GAMEMODE_QUICKPLAY_42:
			case GameMode.GAMEMODE_QUICKPLAY_43:
			case GameMode.GAMEMODE_QUICKPLAY_44:
			case GameMode.GAMEMODE_QUICKPLAY_45:
			case GameMode.GAMEMODE_QUICKPLAY_46:
			case GameMode.GAMEMODE_QUICKPLAY_47:
			case GameMode.GAMEMODE_QUICKPLAY_48:
			case GameMode.GAMEMODE_QUICKPLAY_49:
			case GameMode.GAMEMODE_QUICKPLAY_50:
				if (this.mLevel <= 10)
				{
					this.mBackground = BackgroundType.BACKGROUND_1_DAY;
				}
				else if (this.mLevel <= 20)
				{
					this.mBackground = BackgroundType.BACKGROUND_2_NIGHT;
				}
				else if (this.mLevel <= 30)
				{
					this.mBackground = BackgroundType.BACKGROUND_3_POOL;
				}
				else if (this.mApp.IsScaryPotterLevel())
				{
					this.mBackground = BackgroundType.BACKGROUND_2_NIGHT;
				}
				else if (this.mLevel <= 40)
				{
					this.mBackground = BackgroundType.BACKGROUND_4_FOG;
				}
				else if (this.mLevel <= 49)
				{
					this.mBackground = BackgroundType.BACKGROUND_5_ROOF;
				}
				else if (this.mLevel == 50)
				{
					this.mBackground = BackgroundType.BACKGROUND_6_BOSS;
				}
				else
				{
					this.mBackground = BackgroundType.BACKGROUND_1_DAY;
				}
				break;
			case GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1:
			case GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_1:
			case GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_1:
			case GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS:
			case GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING:
			case GameMode.GAMEMODE_CHALLENGE_SLOT_MACHINE:
			case GameMode.GAMEMODE_CHALLENGE_SEEING_STARS:
			case GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING_2:
			case GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1:
			case GameMode.GAMEMODE_CHALLENGE_SUNNY_DAY:
			case GameMode.GAMEMODE_CHALLENGE_RESODDED:
			case GameMode.GAMEMODE_CHALLENGE_BIG_TIME:
			case GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_2:
			case GameMode.GAMEMODE_CHALLENGE_ICE:
			case GameMode.GAMEMODE_CHALLENGE_SHOVEL:
			case GameMode.GAMEMODE_CHALLENGE_SQUIRREL:
				this.mBackground = BackgroundType.BACKGROUND_1_DAY;
				break;
			case GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_2:
			case GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_2:
			case GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_2:
			case GameMode.GAMEMODE_CHALLENGE_BEGHOULED:
			case GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST:
			case GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT:
			case GameMode.GAMEMODE_CHALLENGE_WHACK_A_ZOMBIE:
			case GameMode.GAMEMODE_CHALLENGE_GRAVE_DANGER:
			case GameMode.GAMEMODE_SCARY_POTTER_1:
			case GameMode.GAMEMODE_SCARY_POTTER_2:
			case GameMode.GAMEMODE_SCARY_POTTER_3:
			case GameMode.GAMEMODE_SCARY_POTTER_4:
			case GameMode.GAMEMODE_SCARY_POTTER_5:
			case GameMode.GAMEMODE_SCARY_POTTER_6:
			case GameMode.GAMEMODE_SCARY_POTTER_7:
			case GameMode.GAMEMODE_SCARY_POTTER_8:
			case GameMode.GAMEMODE_SCARY_POTTER_9:
			case GameMode.GAMEMODE_SCARY_POTTER_ENDLESS:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_9:
			case GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS:
				this.mBackground = BackgroundType.BACKGROUND_2_NIGHT;
				break;
			case GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_3:
			case GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_3:
			case GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_3:
			case GameMode.GAMEMODE_CHALLENGE_LITTLE_TROUBLE:
			case GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA:
			case GameMode.GAMEMODE_CHALLENGE_SPEED:
			case GameMode.GAMEMODE_CHALLENGE_LAST_STAND:
			case GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS_2:
			case GameMode.GAMEMODE_UPSELL:
			case GameMode.GAMEMODE_INTRO:
				this.mBackground = BackgroundType.BACKGROUND_3_POOL;
				break;
			case GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_4:
			case GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_4:
			case GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_4:
			case GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS:
			case GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL:
			case GameMode.GAMEMODE_CHALLENGE_AIR_RAID:
			case GameMode.GAMEMODE_CHALLENGE_STORMY_NIGHT:
				this.mBackground = BackgroundType.BACKGROUND_4_FOG;
				break;
			case GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_5:
			case GameMode.GAMEMODE_SURVIVAL_HARD_STAGE_5:
			case GameMode.GAMEMODE_SURVIVAL_ENDLESS_STAGE_5:
			case GameMode.GAMEMODE_CHALLENGE_COLUMN:
			case GameMode.GAMEMODE_CHALLENGE_POGO_PARTY:
			case GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY:
			case GameMode.GAMEMODE_CHALLENGE_BUNGEE_BLITZ:
				this.mBackground = BackgroundType.BACKGROUND_5_ROOF;
				break;
			case GameMode.GAMEMODE_CHALLENGE_FINAL_BOSS:
				this.mBackground = BackgroundType.BACKGROUND_6_BOSS;
				break;
			case GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN:
				this.mBackground = BackgroundType.BACKGROUND_GREENHOUSE;
				break;
			case GameMode.GAMEMODE_TREE_OF_WISDOM:
				this.mBackground = BackgroundType.BACKGROUND_TREE_OF_WISDOM;
				break;
			case GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM:
				this.mBackground = BackgroundType.BACKGROUND_ZOMBIQUARIUM;
				break;
			default:
				Debug.ASSERT(false);
				break;
			}
			this.LoadBackgroundImages();
			if (this.mBackground == BackgroundType.BACKGROUND_1_DAY || this.mBackground == BackgroundType.BACKGROUND_GREENHOUSE || this.mBackground == BackgroundType.BACKGROUND_TREE_OF_WISDOM)
			{
				this.mPlantRow[0] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[1] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[2] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[3] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[4] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[5] = PlantRowType.PLANTROW_DIRT;
				if (this.mApp.IsAdventureMode() && this.mApp.IsFirstTimeAdventureMode())
				{
					if (this.mLevel == 1)
					{
						this.mPlantRow[0] = PlantRowType.PLANTROW_DIRT;
						this.mPlantRow[1] = PlantRowType.PLANTROW_DIRT;
						this.mPlantRow[3] = PlantRowType.PLANTROW_DIRT;
						this.mPlantRow[4] = PlantRowType.PLANTROW_DIRT;
					}
					else if (this.mLevel == 2 || this.mLevel == 3)
					{
						this.mPlantRow[0] = PlantRowType.PLANTROW_DIRT;
						this.mPlantRow[4] = PlantRowType.PLANTROW_DIRT;
					}
				}
				else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RESODDED)
				{
					this.mPlantRow[0] = PlantRowType.PLANTROW_DIRT;
					this.mPlantRow[4] = PlantRowType.PLANTROW_DIRT;
				}
			}
			else if (this.mBackground == BackgroundType.BACKGROUND_2_NIGHT)
			{
				this.mPlantRow[0] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[1] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[2] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[3] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[4] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[5] = PlantRowType.PLANTROW_DIRT;
			}
			else if (this.mBackground == BackgroundType.BACKGROUND_3_POOL || this.mBackground == BackgroundType.BACKGROUND_ZOMBIQUARIUM)
			{
				this.mPlantRow[0] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[1] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[2] = PlantRowType.PLANTROW_POOL;
				this.mPlantRow[3] = PlantRowType.PLANTROW_POOL;
				this.mPlantRow[4] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[5] = PlantRowType.PLANTROW_NORMAL;
			}
			else if (this.mBackground == BackgroundType.BACKGROUND_4_FOG)
			{
				this.mPlantRow[0] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[1] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[2] = PlantRowType.PLANTROW_POOL;
				this.mPlantRow[3] = PlantRowType.PLANTROW_POOL;
				this.mPlantRow[4] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[5] = PlantRowType.PLANTROW_NORMAL;
			}
			else if (this.mBackground == BackgroundType.BACKGROUND_5_ROOF || this.mBackground == BackgroundType.BACKGROUND_6_BOSS)
			{
				this.mPlantRow[0] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[1] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[2] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[3] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[4] = PlantRowType.PLANTROW_NORMAL;
				this.mPlantRow[5] = PlantRowType.PLANTROW_DIRT;
			}
			else
			{
				Debug.ASSERT(false);
			}
			for (int i = 0; i < Constants.GRIDSIZEX; i++)
			{
				for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
				{
					if (this.mPlantRow[j] == PlantRowType.PLANTROW_DIRT)
					{
						this.mGridSquareType[i, j] = GridSquareType.GRIDSQUARE_DIRT;
					}
					else if (this.mPlantRow[j] == PlantRowType.PLANTROW_POOL && i >= 0 && i <= 8)
					{
						this.mGridSquareType[i, j] = GridSquareType.GRIDSQUARE_POOL;
					}
					else if (this.mPlantRow[j] == PlantRowType.PLANTROW_HIGH_GROUND && i >= 4 && i <= 8)
					{
						this.mGridSquareType[i, j] = GridSquareType.GRIDSQUARE_HIGH_GROUND;
					}
				}
			}
			int levelRandSeed = this.GetLevelRandSeed();
			RandomNumbers.Seed(levelRandSeed);
			if (this.StageHasGraveStones())
			{
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_GRAVE_DANGER)
				{
					this.AddGraveStones(6, TodCommon.RandRangeInt(1, 2));
					this.AddGraveStones(7, TodCommon.RandRangeInt(1, 3));
					this.AddGraveStones(8, TodCommon.RandRangeInt(2, 3));
				}
				else if (this.mApp.IsWhackAZombieLevel())
				{
					this.mChallenge.WhackAZombiePlaceGraves(9);
				}
				else if (this.mBackground == BackgroundType.BACKGROUND_2_NIGHT)
				{
					if (this.mApp.IsSurvivalNormal(this.mApp.mGameMode))
					{
						this.AddGraveStones(5, 1);
						this.AddGraveStones(6, 1);
						this.AddGraveStones(7, 1);
						this.AddGraveStones(8, 2);
					}
					else if (!this.mApp.IsAdventureMode() && !this.mApp.IsQuickPlayMode())
					{
						this.AddGraveStones(4, 1);
						this.AddGraveStones(5, 1);
						this.AddGraveStones(6, 2);
						this.AddGraveStones(7, 2);
						this.AddGraveStones(8, 3);
					}
					else if (this.mLevel == 11 || this.mLevel == 12 || this.mLevel == 13)
					{
						this.AddGraveStones(6, 1);
						this.AddGraveStones(7, 1);
						this.AddGraveStones(8, 2);
					}
					else if (this.mLevel == 14 || this.mLevel == 16 || this.mLevel == 18)
					{
						this.AddGraveStones(5, 1);
						this.AddGraveStones(6, 1);
						this.AddGraveStones(7, 2);
						this.AddGraveStones(8, 3);
					}
					else if (this.mLevel == 17 || this.mLevel == 19)
					{
						this.AddGraveStones(4, 1);
						this.AddGraveStones(5, 2);
						this.AddGraveStones(6, 2);
						this.AddGraveStones(7, 3);
						this.AddGraveStones(8, 3);
					}
					else if (this.mLevel >= 20)
					{
						this.AddGraveStones(3, 1);
						this.AddGraveStones(4, 2);
						this.AddGraveStones(5, 2);
						this.AddGraveStones(6, 2);
						this.AddGraveStones(7, 3);
						this.AddGraveStones(8, 3);
					}
					else
					{
						Debug.ASSERT(false);
					}
				}
			}
			this.PickSpecialGraveStone();
		}

		public void InitZombieWaves()
		{
			Debug.ASSERT(true);
			if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
			{
				this.InitZombieWavesForLevel(this.mLevel);
			}
			else
			{
				this.mChallenge.InitZombieWaves();
			}
			this.PickZombieWaves();
			Debug.ASSERT(this.IsZombieWaveDistributionOk());
			this.mCurrentWave = 0;
			this.mTotalSpawnedWaves = 0;
			this.mApp.mKilledYetiAndRestarted = false;
			if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 2)
			{
				this.mZombieCountDown = 4998;
			}
			else if (this.mApp.IsSurvivalMode() && this.mChallenge.mSurvivalStage > 0)
			{
				this.mZombieCountDown = 600;
			}
			else
			{
				this.mZombieCountDown = 1800;
			}
			this.mZombieCountDownStart = this.mZombieCountDown;
			this.mZombieHealthToNextWave = -1;
			this.mZombieHealthWaveStart = 0;
			this.mLastBungeeWave = 0;
			this.mProgressMeterWidth = 0;
			this.mHugeWaveCountDown = 0;
			this.mLevelAwardSpawned = false;
		}

		public void InitSurvivalStage()
		{
			this.RefreshSeedPacketFromCursor();
			this.mApp.mSoundSystem.GamePause(true);
			this.FreezeEffectsForCutscene(true);
			this.mLevelComplete = false;
			this.InitZombieWaves();
			this.mApp.mGameScene = GameScenes.SCENE_LEVEL_INTRO;
			this.mApp.ShowSeedChooserScreen();
			this.mCutScene.StartLevelIntro();
			this.mSeedBank.UpdateHeight();
			for (int i = 0; i < 9; i++)
			{
				SeedPacket seedPacket = this.mSeedBank.mSeedPackets[i];
				seedPacket.mY = this.GetSeedPacketPositionY(i);
				seedPacket.mPacketType = SeedType.SEED_NONE;
			}
			if (this.StageHasFog())
			{
				this.mFogBlownCountDown = 2000;
			}
			for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
			{
				this.mWaveRowGotLawnMowered[j] = -100;
			}
		}

		public static int MakeRenderOrder(RenderLayer theRenderLayer, int theRow, int theLayerOffset)
		{
			return (int)(theRenderLayer + theLayerOffset + 10000 * theRow);
		}

		public void UpdateGame()
		{
			this.UpdateGameObjects();
			if (this.StageHasFog() && this.mFogBlownCountDown > 0)
			{
				float num = 1065f - (float)this.LeftFogColumn() * 80f + (float)Constants.BOARD_EXTRA_ROOM;
				if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO)
				{
					this.mFogOffset = TodCommon.TodAnimateCurveFloat(200, 0, this.mFogBlownCountDown, num, 0f, TodCurves.CURVE_EASE_OUT);
				}
				else if (this.mFogBlownCountDown < 2000)
				{
					this.mFogOffset = TodCommon.TodAnimateCurveFloat(2000, 0, this.mFogBlownCountDown, num, 0f, TodCurves.CURVE_EASE_OUT);
				}
				else if (this.mFogOffset < num)
				{
					this.mFogOffset = TodCommon.TodAnimateCurveFloat(-5, (int)num, (int)(this.mFogOffset * 1.1f), 0f, num, TodCurves.CURVE_LINEAR);
				}
			}
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING && !this.mCutScene.ShouldRunUpsellBoard())
			{
				return;
			}
			this.mMainCounter += 3;
			this.UpdateSunSpawning();
			this.UpdateZombieSpawning();
			this.UpdateIce();
			if (this.mIceTrapCounter > 0)
			{
				this.mIceTrapCounter -= 3;
				if (this.mIceTrapCounter >= 0 && this.mIceTrapCounter < 3)
				{
					TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mPoolSparklyParticleID);
					if (todParticleSystem != null)
					{
						todParticleSystem.mDontUpdate = false;
					}
				}
			}
			if (this.mFogBlownCountDown > 0)
			{
				this.mFogBlownCountDown -= 3;
			}
			if (this.mMainCounter == 3)
			{
				if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 1)
				{
					this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_1_PICK_UP_PEASHOOTER);
				}
				else if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 2)
				{
					this.SetTutorialState(TutorialState.TUTORIAL_LEVEL_2_PICK_UP_SUNFLOWER);
					this.DisplayAdvice("[ADVICE_PLANT_SUNFLOWER1]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL2, AdviceType.ADVICE_NONE);
					this.mTutorialTimer = 500;
				}
			}
			this.UpdateProgressMeter();
		}

		public void InitZombieWavesForLevel(int aForLevel)
		{
			if (this.mApp.IsWhackAZombieLevel())
			{
				this.mChallenge.InitZombieWaves();
				return;
			}
			if (this.mApp.IsWallnutBowlingLevel() && !this.mApp.IsFirstTimeAdventureMode())
			{
				this.mChallenge.InitZombieWaves();
				return;
			}
			for (int i = 0; i < 33; i++)
			{
				this.mZombieAllowed[i] = Board.CanZombieSpawnOnLevel((ZombieType)i, aForLevel);
			}
		}

		public uint SeedNotRecommendedForLevel(SeedType theSeedType)
		{
			uint result = 0U;
			if (Plant.IsNocturnal(theSeedType) && !this.StageIsNight())
			{
				TodCommon.SetBit(ref result, 0, 1);
			}
			if (theSeedType == SeedType.SEED_INSTANT_COFFEE && this.StageIsNight())
			{
				TodCommon.SetBit(ref result, 7, 1);
			}
			if (theSeedType == SeedType.SEED_GRAVEBUSTER && !this.StageHasGraveStones())
			{
				TodCommon.SetBit(ref result, 2, 1);
			}
			if (theSeedType == SeedType.SEED_PLANTERN && !this.StageHasFog())
			{
				TodCommon.SetBit(ref result, 3, 1);
			}
			if (theSeedType == SeedType.SEED_FLOWERPOT && !this.StageHasRoof())
			{
				TodCommon.SetBit(ref result, 4, 1);
			}
			if (this.StageHasRoof() && (theSeedType == SeedType.SEED_SPIKEWEED || theSeedType == SeedType.SEED_SPIKEROCK))
			{
				TodCommon.SetBit(ref result, 5, 1);
			}
			if (!this.StageHasPool() && (theSeedType == SeedType.SEED_LILYPAD || theSeedType == SeedType.SEED_TANGLEKELP || theSeedType == SeedType.SEED_SEASHROOM || theSeedType == SeedType.SEED_CATTAIL))
			{
				TodCommon.SetBit(ref result, 1, 1);
			}
			return result;
		}

		public void DrawTopRightUI(Graphics g)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				if (this.mChallenge.mChallengeState == ChallengeState.STATECHALLENGE_ZEN_FADING)
				{
					this.mMenuButton.mY = TodCommon.TodAnimateCurve(50, 0, this.mChallenge.mChallengeStateCounter, 2, -50, TodCurves.CURVE_EASE_IN_OUT);
					this.mStoreButton.mX = TodCommon.TodAnimateCurve(50, 0, this.mChallenge.mChallengeStateCounter, Constants.ZenGardenStoreButtonX, 800, TodCurves.CURVE_EASE_IN_OUT);
				}
				else
				{
					this.mMenuButton.mY = 2;
					this.mStoreButton.mX = Constants.ZenGardenStoreButtonX;
				}
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_COMPLETED)
			{
				SexyColor flashingColor = TodCommon.GetFlashingColor(this.mMainCounter, 75);
				g.SetColorizeImages(true);
				g.SetColor(flashingColor);
			}
			this.mMenuButton.Draw(g);
			g.SetColorizeImages(false);
			if (this.mStoreButton != null && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				if (this.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_VISIT_STORE)
				{
					SexyColor flashingColor2 = TodCommon.GetFlashingColor(this.mMainCounter, 75);
					g.SetColorizeImages(true);
					g.SetColor(flashingColor2);
				}
				this.mStoreButton.Draw(g);
				g.SetColorizeImages(false);
			}
		}

		public void DrawFog(Graphics g)
		{
			Image image_FOG = Resources.IMAGE_FOG;
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			for (int i = 0; i < Constants.GRIDSIZEX; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					int num = this.mGridCelFog[i, j];
					if (num != 0)
					{
						int num2 = this.mGridCelLook[i, j % Constants.MAX_GRIDSIZEY];
						int theCelCol = num2 % 8;
						float num3 = (float)(i * 80) + this.mFogOffset - 15f;
						float num4 = (float)(j * 85) + 20f;
						int num5 = (int)(255f - (float)num2 * 1.5f);
						int num6 = 255 - num2;
						float num7 = (float)this.mMainCounter * 3.1415927f * 2f / 900f;
						float num8 = (float)this.mMainCounter * 3.1415927f * 2f / 500f;
						float num9 = 3f * (float)i * 3.1415927f * 2f / (float)Constants.GRIDSIZEX;
						float num10 = 3f * (float)j * 3.1415927f * 2f / 7f;
						float num11 = (float)(13.0 + 4.0 * Math.Sin((double)(num10 + num7)) + 8.0 * Math.Sin((double)(num9 + num8)));
						num5 -= (int)(num11 * 1.5f);
						num6 -= (int)num11;
						g.SetColorizeImages(true);
						g.SetColor(new SexyColor(num5, num5, num6, num));
						g.DrawImageCel(image_FOG, (int)(num3 * Constants.S), (int)(num4 * Constants.S), theCelCol, 0);
						if (i == Constants.GRIDSIZEX - 1)
						{
							int num12 = 120;
							g.DrawImageCel(image_FOG, (int)((num3 + (float)num12) * Constants.S), (int)(num4 * Constants.S), theCelCol, 0);
						}
						g.SetColorizeImages(false);
					}
				}
			}
		}

		public void UpdateFog()
		{
			if (!this.StageHasFog())
			{
				return;
			}
			int num = 3;
			if (this.mFogBlownCountDown > 0 && this.mFogBlownCountDown < 2000)
			{
				num = 1;
			}
			else if (this.mFogBlownCountDown > 0)
			{
				num = 20;
			}
			int num2 = this.LeftFogColumn();
			for (int i = num2; i < Constants.GRIDSIZEX; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					int num3 = 255;
					if (i == num2)
					{
						num3 = 200;
					}
					this.mGridCelFog[i, j] = Math.Min(this.mGridCelFog[i, j] + num, num3);
				}
			}
			int count = this.mPlants.Count;
			for (int k = 0; k < count; k++)
			{
				Plant plant = this.mPlants[k];
				if (!plant.mDead && !plant.NotOnGround())
				{
					if (plant.mSeedType == SeedType.SEED_PLANTERN)
					{
						this.ClearFogAroundPlant(plant, 4);
					}
					else if (plant.mSeedType == SeedType.SEED_TORCHWOOD)
					{
						this.ClearFogAroundPlant(plant, 1);
					}
				}
			}
		}

		public int LeftFogColumn()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_AIR_RAID)
			{
				return 6;
			}
			if (!this.mApp.IsAdventureMode() && !this.mApp.IsQuickPlayMode())
			{
				return 5;
			}
			if (this.mLevel == 31)
			{
				return 6;
			}
			if (this.mLevel >= 32 && this.mLevel <= 36)
			{
				return 5;
			}
			if (this.mLevel >= 37 && this.mLevel <= 40)
			{
				return 4;
			}
			Debug.ASSERT(false);
			return -666;
		}

		public static bool IsZombieTypePoolOnly(ZombieType theZombieType)
		{
			return theZombieType == ZombieType.ZOMBIE_SNORKEL || theZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER;
		}

		public void DropLootPiece(int thePosX, int thePosY, int theDropFactor)
		{
			if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 22 && this.mCurrentWave > 5 && !this.mApp.mPlayerInfo.mHasUnlockedMinigames && this.CountCoinByType(CoinType.COIN_PRESENT_MINIGAMES) == 0)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
				this.AddCoin(thePosX - 40, thePosY, CoinType.COIN_PRESENT_MINIGAMES, CoinMotion.COIN_MOTION_COIN);
				return;
			}
			if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 36 && this.mCurrentWave > 5 && !this.mApp.mPlayerInfo.mHasUnlockedPuzzleMode && this.CountCoinByType(CoinType.COIN_PRESENT_PUZZLE_MODE) == 0)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
				this.AddCoin(thePosX - 40, thePosY, CoinType.COIN_PRESENT_PUZZLE_MODE, CoinMotion.COIN_MOTION_COIN);
				return;
			}
			int num = RandomNumbers.NextNumber(30000);
			if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 11 && !this.mDroppedFirstCoin && this.mCurrentWave > 5)
			{
				num = 1000;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				num *= 5;
			}
			if (this.mApp.IsWhackAZombieLevel())
			{
				int num2 = 2500;
				int num3;
				if (this.mSunMoney > 500)
				{
					num3 = num2 + 300;
				}
				else if (this.mSunMoney > 350)
				{
					num3 = num2 + 600;
				}
				else if (this.mSunMoney > 200)
				{
					num3 = num2 + 1200;
				}
				else
				{
					num3 = num2 + 2500;
				}
				if (num >= num2 * theDropFactor && num < num3 * theDropFactor)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
					this.AddCoin(thePosX - 20, thePosY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
					this.AddCoin(thePosX - 40, thePosY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
					this.AddCoin(thePosX - 60, thePosY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
					return;
				}
			}
			if (this.mTotalSpawnedWaves > 70)
			{
				return;
			}
			int num4;
			if (!this.mApp.mZenGarden.CanDropPottedPlantLoot())
			{
				num4 = 0;
			}
			else if ((this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()) && !this.mApp.IsFirstTimeAdventureMode())
			{
				num4 = 24;
			}
			else if (this.mApp.IsSurvivalEndless(this.mApp.mGameMode))
			{
				num4 = 3;
			}
			else
			{
				num4 = 12;
			}
			int num5 = num4;
			if (!this.mApp.mZenGarden.CanDropChocolate())
			{
				num5 = num5;
			}
			else if ((this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()) && !this.mApp.IsFirstTimeAdventureMode())
			{
				num5 += 72;
			}
			else if (this.mApp.IsSurvivalEndless(this.mApp.mGameMode))
			{
				num5 += 9;
			}
			else
			{
				num5 += 36;
			}
			int num6 = 14 + num5;
			int num7 = 250 + num5;
			int num8 = 2500 + num5;
			CoinType coinType;
			if (num < num4 * theDropFactor)
			{
				coinType = CoinType.COIN_PRESENT_PLANT;
			}
			else if (num < num5 * theDropFactor)
			{
				coinType = CoinType.COIN_CHOCOLATE;
			}
			else if (num < num6 * theDropFactor)
			{
				coinType = CoinType.COIN_DIAMOND;
			}
			else if (num < num7 * theDropFactor)
			{
				coinType = CoinType.COIN_GOLD;
			}
			else
			{
				if (num >= num8 * theDropFactor)
				{
					return;
				}
				coinType = CoinType.COIN_SILVER;
			}
			if (coinType == CoinType.COIN_DIAMOND && this.mApp.mPlayerInfo.mPurchases[21] < 1)
			{
				coinType = CoinType.COIN_GOLD;
			}
			if (this.mApp.IsWallnutBowlingLevel() && (coinType == CoinType.COIN_SILVER || coinType == CoinType.COIN_GOLD || coinType == CoinType.COIN_DIAMOND))
			{
				return;
			}
			if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 11)
			{
				int num9 = Coin.GetCoinValue(CoinType.COIN_GOLD) * this.mLawnMowers.Count;
				int itemCost = StoreScreen.GetItemCost(StoreItem.STORE_ITEM_PACKET_UPGRADE);
				int num10 = this.mApp.mPlayerInfo.mCoins + this.CountCoinsBeingCollected();
				if (Coin.GetCoinValue(coinType) + num10 + num9 >= itemCost)
				{
					return;
				}
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
			this.AddCoin(thePosX - 40, thePosY, coinType, CoinMotion.COIN_MOTION_COIN);
			this.mDroppedFirstCoin = true;
		}

		public void UpdateLevelEndSequence()
		{
			if (this.mNextSurvivalStageCounter > 0)
			{
				if (!this.IsScaryPotterDaveTalking())
				{
					this.mNextSurvivalStageCounter--;
					if (this.mApp.IsAdventureMode() && this.mApp.IsScaryPotterLevel() && this.mNextSurvivalStageCounter == 300)
					{
						this.mApp.CrazyDaveEnter();
						if (this.mChallenge.mSurvivalStage == 0)
						{
							this.mApp.CrazyDaveTalkIndex(2700);
						}
						else
						{
							this.mApp.CrazyDaveTalkIndex(2800);
						}
						this.mChallenge.PuzzleNextStageClear();
						this.mNextSurvivalStageCounter = 100;
					}
				}
				if (this.mNextSurvivalStageCounter == 1 && this.mApp.IsSurvivalMode() && this.mApp.IsSurvivalMode())
				{
					this.TryToSaveGame();
				}
				if (this.mNextSurvivalStageCounter == 0)
				{
					if (this.mApp.IsScaryPotterLevel() && this.mApp.IsAdventureMode())
					{
						return;
					}
					if (this.mApp.IsScaryPotterLevel() && !this.IsFinalScaryPotterStage())
					{
						this.mChallenge.PuzzleNextStageClear();
						this.mChallenge.ScaryPotterPopulate();
						return;
					}
					if (this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
					{
						this.mChallenge.PuzzleNextStageClear();
						this.mChallenge.IZombieInitLevel();
						return;
					}
					if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
					{
						this.ClearAdvice(AdviceType.ADVICE_NONE);
						return;
					}
					this.mLevelComplete = true;
					this.RemoveZombiesForRepick();
					return;
				}
			}
			if (this.mBoardFadeOutCounter < 0)
			{
				return;
			}
			this.mBoardFadeOutCounter--;
			if (this.mBoardFadeOutCounter == 0)
			{
				this.mLevelComplete = true;
				return;
			}
			if (this.mBoardFadeOutCounter == 300)
			{
				bool flag = this.IsSurvivalStageWithRepick();
				bool flag2 = this.mLevel == 9 || this.mLevel == 19 || this.mLevel == 29 || this.mLevel == 39 || this.mLevel == 49;
				if (!flag && !flag2)
				{
					this.mApp.PlaySample(Resources.SOUND_LIGHTFILL);
				}
			}
			if (this.mScoreNextMowerCounter > 0)
			{
				this.mScoreNextMowerCounter--;
				if (this.mScoreNextMowerCounter != 0)
				{
					return;
				}
			}
			if (!this.CanDropLoot())
			{
				return;
			}
			if (this.IsSurvivalStageWithRepick())
			{
				return;
			}
			this.mScoreNextMowerCounter = 40;
			LawnMower bottomLawnMower = this.GetBottomLawnMower();
			if (bottomLawnMower == null)
			{
				return;
			}
			CoinType theCoinType = CoinType.COIN_GOLD;
			this.AddCoin((int)(bottomLawnMower.mPosX + (float)Constants.LawnMower_Coin_Offset.X), (int)(bottomLawnMower.mPosY + (float)Constants.LawnMower_Coin_Offset.Y), theCoinType, CoinMotion.COIN_MOTION_LAWNMOWER_COIN);
			SoundInstance soundInstance = this.mApp.mSoundManager.GetSoundInstance((uint)Resources.SOUND_POINTS);
			if (soundInstance != null)
			{
				soundInstance.Play(false, true);
				float num = TodCommon.ClampFloat(6f - (float)this.CountUntriggerLawnMowers(), 0f, 6f);
				soundInstance.AdjustPitch((double)num);
			}
			else
			{
				Debug.OutputDebug<string>("FAILED TO PLAY SOUND INSTANCE");
			}
			bottomLawnMower.Die();
		}

		public LawnMower GetBottomLawnMower()
		{
			LawnMower lawnMower = null;
			LawnMower lawnMower2 = null;
			while (this.IterateLawnMowers(ref lawnMower2))
			{
				if (lawnMower2.mMowerState != LawnMowerState.MOWER_TRIGGERED && lawnMower2.mMowerState != LawnMowerState.MOWER_SQUISHED && (lawnMower == null || lawnMower.mRow < lawnMower2.mRow))
				{
					lawnMower = lawnMower2;
				}
			}
			return lawnMower;
		}

		public bool CanDropLoot()
		{
			return !this.mCutScene.ShouldRunUpsellBoard() && (!this.mApp.IsFirstTimeAdventureMode() || this.mLevel >= 11);
		}

		public ZombieType GetIntroducedZombieType()
		{
			if ((!this.mApp.IsAdventureMode() && !this.mApp.IsQuickPlayMode()) || this.mLevel == 1)
			{
				return ZombieType.ZOMBIE_INVALID;
			}
			for (int i = 0; i < 33; i++)
			{
				ZombieType zombieType = (ZombieType)i;
				ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(zombieType);
				if ((zombieType != ZombieType.ZOMBIE_YETI || this.mApp.CanSpawnYetis()) && zombieDefinition.mStartingLevel == this.mLevel)
				{
					return zombieType;
				}
			}
			return ZombieType.ZOMBIE_INVALID;
		}

		public void PickSpecialGraveStone()
		{
			int num = 0;
			int num2 = -1;
			GridItem gridItem = null;
			while (this.IterateGridItems(ref gridItem, ref num2))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_GRAVESTONE)
				{
					Debug.ASSERT(num < Board.MAX_GRAVE_STONES);
					Board.aPicks[num] = gridItem;
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			GridItem gridItem2 = Board.aPicks[RandomNumbers.NextNumber(num)];
			gridItem2.mGridItemState = GridItemState.GRIDITEM_STATE_GRAVESTONE_SPECIAL;
		}

		public float GetPosYBasedOnRow(float thePosX, int theRow)
		{
			if (this.StageHasRoof())
			{
				float num = 0f;
				int num2 = 440 + Constants.BOARD_EXTRA_ROOM;
				if (thePosX < (float)num2)
				{
					num = ((float)num2 - thePosX) * 0.25f;
				}
				return (float)this.GridToPixelY(8, theRow) + num;
			}
			return (float)this.GridToPixelY(0, theRow);
		}

		public void NextWaveComing()
		{
			if (this.mCurrentWave + 1 == this.mNumWaves)
			{
				bool flag = true;
				if (this.IsSurvivalStageWithRepick())
				{
					flag = false;
				}
				else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
				{
					flag = false;
				}
				else if (this.mApp.IsContinuousChallenge())
				{
					flag = false;
				}
				if (flag)
				{
					this.mApp.AddReanimation(0f + (float)Constants.BOARD_EXTRA_ROOM, 30f, 800000, ReanimationType.REANIM_FINAL_WAVE);
					this.mFinalWaveSoundCounter = 60;
				}
			}
			if (this.mCurrentWave == 0)
			{
				this.mApp.PlaySample(Resources.SOUND_AWOOGA);
				return;
			}
			if (this.mApp.IsWhackAZombieLevel())
			{
				if (this.mCurrentWave == this.mNumWaves - 1)
				{
					this.mApp.PlaySample(Resources.SOUND_SIREN);
					return;
				}
			}
			else if (this.IsFlagWave(this.mCurrentWave))
			{
				this.mApp.PlaySample(Resources.SOUND_SIREN);
			}
		}

		public bool BungeeIsTargetingCell(int theCol, int theRow)
		{
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && !zombie.IsDeadOrDying() && zombie.mZombieType == ZombieType.ZOMBIE_BUNGEE && zombie.mRow == theRow && zombie.mTargetCol == theCol)
				{
					return true;
				}
			}
			return false;
		}

		public int PlantingPixelToGridX(int theX, int theY, SeedType theSeedType)
		{
			int theY2 = theY;
			this.OffsetYForPlanting(ref theY2, theSeedType);
			return this.PixelToGridX(theX, theY2);
		}

		public int PlantingPixelToGridY(int theX, int theY, SeedType theSeedType)
		{
			int num = theY;
			this.OffsetYForPlanting(ref num, theSeedType);
			if (theSeedType == SeedType.SEED_INSTANT_COFFEE)
			{
				int theGridX = this.PixelToGridX(theX, num);
				int num2 = this.PixelToGridY(theX, num);
				Plant topPlantAt = this.GetTopPlantAt(theGridX, num2, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
				if (topPlantAt != null && topPlantAt.mIsAsleep)
				{
					return num2;
				}
				num2 = this.PixelToGridY(theX, num + 30);
				Plant topPlantAt2 = this.GetTopPlantAt(theGridX, num2, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
				if (topPlantAt2 != null && topPlantAt2.mIsAsleep)
				{
					return num2;
				}
				num2 = this.PixelToGridY(theX, num - 50);
				Plant topPlantAt3 = this.GetTopPlantAt(theGridX, num2, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
				if (topPlantAt3 != null && topPlantAt3.mIsAsleep)
				{
					return num2;
				}
			}
			return this.PixelToGridY(theX, num);
		}

		public Plant FindUmbrellaPlant(int theGridX, int theGridY)
		{
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead && plant.mSeedType == SeedType.SEED_UMBRELLA && !plant.NotOnGround() && theGridX >= plant.mPlantCol - 1 && theGridX <= plant.mPlantCol + 1 && theGridY >= plant.mRow - 1 && theGridY <= plant.mRow + 1)
				{
					return plant;
				}
			}
			return null;
		}

		public void SetTutorialState(TutorialState theTutorialState)
		{
			if (theTutorialState == TutorialState.TUTORIAL_LEVEL_1_PICK_UP_PEASHOOTER)
			{
				if (this.mPlants.Count == 0)
				{
					float num = (float)(this.mSeedBank.mX + this.mSeedBank.mSeedPackets[0].mX + Constants.SMALL_SEEDPACKET_WIDTH / 2) - Constants.InvertAndScale(13f);
					float num2 = 0f;
					this.TutorialArrowShow((int)num, (int)num2);
					this.DisplayAdvice("[ADVICE_CLICK_SEED_PACKET]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1_STAY, AdviceType.ADVICE_NONE);
				}
				else
				{
					this.DisplayAdvice("[ADVICE_ENOUGH_SUN]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1_STAY, AdviceType.ADVICE_NONE);
					this.mTutorialTimer = 400;
				}
			}
			else if (theTutorialState == TutorialState.TUTORIAL_LEVEL_1_PLANT_PEASHOOTER)
			{
				this.mTutorialTimer = -1;
				this.TutorialArrowRemove();
				if (this.mPlants.Count == 0)
				{
					this.DisplayAdvice("[ADVICE_CLICK_ON_GRASS]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1_STAY, AdviceType.ADVICE_NONE);
				}
				else
				{
					this.ClearAdvice(AdviceType.ADVICE_NONE);
				}
			}
			else if (theTutorialState == TutorialState.TUTORIAL_LEVEL_1_REFRESH_PEASHOOTER)
			{
				this.DisplayAdvice("[ADVICE_PLANTED_PEASHOOTER]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1_STAY, AdviceType.ADVICE_NONE);
				this.mSunCountDown = 400;
			}
			else if (theTutorialState == TutorialState.TUTORIAL_LEVEL_1_COMPLETED)
			{
				this.DisplayAdvice("[ADVICE_ZOMBIE_ONSLAUGHT]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1, AdviceType.ADVICE_NONE);
				this.mZombieCountDown = 99;
				this.mZombieCountDownStart = this.mZombieCountDown;
			}
			else if (theTutorialState == TutorialState.TUTORIAL_LEVEL_2_PICK_UP_SUNFLOWER || theTutorialState == TutorialState.TUTORIAL_MORESUN_PICK_UP_SUNFLOWER)
			{
				float num3 = (float)(this.mSeedBank.mX + this.mSeedBank.mSeedPackets[1].mX) + Constants.InvertAndScale(13f);
				float num4 = (float)(this.mSeedBank.mY + this.mSeedBank.mSeedPackets[1].mY);
				this.TutorialArrowShow((int)num3, (int)num4);
			}
			else if (theTutorialState == TutorialState.TUTORIAL_LEVEL_2_PLANT_SUNFLOWER || theTutorialState == TutorialState.TUTORIAL_LEVEL_2_REFRESH_SUNFLOWER || theTutorialState == TutorialState.TUTORIAL_MORESUN_PLANT_SUNFLOWER || theTutorialState == TutorialState.TUTORIAL_MORESUN_REFRESH_SUNFLOWER)
			{
				this.TutorialArrowRemove();
			}
			else if (theTutorialState == TutorialState.TUTORIAL_LEVEL_2_COMPLETED)
			{
				if (this.mCurrentWave == 0)
				{
					this.mZombieCountDown = 999;
					this.mZombieCountDownStart = this.mZombieCountDown;
				}
			}
			else if (theTutorialState == TutorialState.TUTORIAL_SLOT_MACHINE_PULL)
			{
				this.DisplayAdvice("[ADVICE_SLOT_MACHINE_PULL]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_SLOT_MACHINE_PULL);
			}
			else if (theTutorialState == TutorialState.TUTORIAL_SLOT_MACHINE_COMPLETED)
			{
				this.ClearAdvice(AdviceType.ADVICE_SLOT_MACHINE_PULL);
			}
			else if (theTutorialState == TutorialState.TUTORIAL_SHOVEL_PICKUP)
			{
				this.DisplayAdvice("[ADVICE_CLICK_SHOVEL]", MessageStyle.MESSAGE_STYLE_HINT_STAY, AdviceType.ADVICE_NONE);
				int x = this.GetShovelButtonRect().mX + (int)Constants.InvertAndScale(16f) + Constants.Board_Offset_AspectRatio_Correction;
				int y = 0;
				this.TutorialArrowShow(x, y);
			}
			else if (theTutorialState == TutorialState.TUTORIAL_SHOVEL_DIG)
			{
				this.DisplayAdvice("[ADVICE_CLICK_PLANT]", MessageStyle.MESSAGE_STYLE_HINT_STAY, AdviceType.ADVICE_NONE);
				this.TutorialArrowRemove();
			}
			else if (theTutorialState == TutorialState.TUTORIAL_SHOVEL_KEEP_DIGGING)
			{
				this.DisplayAdvice("[ADVICE_KEEP_DIGGING]", MessageStyle.MESSAGE_STYLE_HINT_STAY, AdviceType.ADVICE_NONE);
			}
			else if (theTutorialState == TutorialState.TUTORIAL_SHOVEL_COMPLETED)
			{
				this.ClearAdvice(AdviceType.ADVICE_NONE);
				this.mCutScene.mCutsceneTime = 1500;
				this.mCutScene.mCrazyDaveDialogStart = 2410;
			}
			this.mTutorialState = theTutorialState;
		}

		public void DoFwoosh(int theRow)
		{
			for (int i = 0; i < 12; i++)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mFwooshID[theRow, i]);
				if (reanimation != null)
				{
					reanimation.ReanimationDie();
				}
				float num = 750f * (float)i / 11f + 10f + (float)Constants.BOARD_EXTRA_ROOM;
				float theY = this.GetPosYBasedOnRow(num + 10f, theRow) - 10f;
				int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, theRow, 1);
				Reanimation reanimation2 = this.mApp.AddReanimation(num, theY, aRenderOrder, ReanimationType.REANIM_JALAPENO_FIRE);
				reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_flame);
				reanimation2.mLoopType = ReanimLoopType.REANIM_LOOP_FULL_LAST_FRAME;
				reanimation2.mAnimRate *= TodCommon.RandRangeFloat(0.7f, 1.3f);
				float num2 = TodCommon.RandRangeFloat(0.9f, 1.1f);
				float num3 = 1f;
				if (RandomNumbers.NextNumber(2) == 0)
				{
					num3 = -1f;
				}
				reanimation2.OverrideScale(num2 * num3, 1f);
				this.mFwooshID[theRow, i] = this.mApp.ReanimationGetID(reanimation2);
			}
			this.mFwooshCountDown = 100;
		}

		public void UpdateFwoosh()
		{
			if (this.mFwooshCountDown == 0)
			{
				return;
			}
			this.mFwooshCountDown -= 3;
			int num = TodCommon.TodAnimateCurve(50, 0, this.mFwooshCountDown, 12, 0, TodCurves.CURVE_LINEAR);
			for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
			{
				for (int j = 0; j < 12 - num; j++)
				{
					Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mFwooshID[i, j]);
					if (reanimation != null)
					{
						reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_done);
						reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME;
						reanimation.mAnimRate = 15f;
					}
					this.mFwooshID[i, j] = null;
				}
			}
		}

		public Plant SpecialPlantHitTest(int x, int y)
		{
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead)
				{
					if (plant.mSeedType == SeedType.SEED_PUMPKINSHELL)
					{
						float num = 25f;
						if (this.GetTopPlantAt(plant.mPlantCol, plant.mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION) == null)
						{
							num = 0f;
						}
						float num2 = TodCommon.Distance2D((float)x, (float)y, (float)plant.mX + 40f, (float)plant.mY + 40f);
						if (num2 >= num && num2 <= 50f && (float)y > (float)plant.mY + 25f)
						{
							return plant;
						}
					}
					if (Plant.IsFlying(plant.mSeedType))
					{
						float num3 = TodCommon.Distance2D((float)x, (float)y, (float)plant.mX + 40f, (float)plant.mY);
						if (num3 < 15f)
						{
							return plant;
						}
					}
				}
			}
			return null;
		}

		public HitResult ToolHitTest(int x, int y, bool posScaled)
		{
			HitResult result;
			this.MouseHitTest(x, y, out result, posScaled);
			if (result.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
			{
				Plant plant = (Plant)result.mObject;
				if (plant.mSeedType == SeedType.SEED_GRAVEBUSTER && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
				{
					result.mObjectType = GameObjectType.OBJECT_TYPE_NONE;
					result.mObject = null;
				}
			}
			return result;
		}

		public bool CanAddGraveStoneAt(int theGridX, int theGridY)
		{
			if (this.mGridSquareType[theGridX, theGridY] != GridSquareType.GRIDSQUARE_GRASS && this.mGridSquareType[theGridX, theGridY] != GridSquareType.GRIDSQUARE_HIGH_GROUND)
			{
				return false;
			}
			int num = -1;
			GridItem gridItem = null;
			while (this.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridX == theGridX && gridItem.mGridY == theGridY && (gridItem.mGridItemType == GridItemType.GRIDITEM_GRAVESTONE || gridItem.mGridItemType == GridItemType.GRIDITEM_CRATER || gridItem.mGridItemType == GridItemType.GRIDITEM_LADDER))
				{
					return false;
				}
			}
			return true;
		}

		public void UpdateGridItems()
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.IterateGridItems(ref gridItem, ref num))
			{
				if (this.mEnableGraveStones && gridItem.mGridItemType == GridItemType.GRIDITEM_GRAVESTONE && gridItem.mGridItemCounter < 100)
				{
					gridItem.mGridItemCounter += 3;
				}
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_CRATER && this.mApp.mGameScene == GameScenes.SCENE_PLAYING)
				{
					if (gridItem.mGridItemCounter > 0)
					{
						gridItem.mGridItemCounter -= 3;
					}
					if (gridItem.mGridItemCounter <= 0)
					{
						gridItem.GridItemDie();
					}
				}
				gridItem.Update(0);
				gridItem.Update(1);
				gridItem.Update(2);
			}
		}

		public GridItem AddAGraveStone(int theGridX, int theGridY)
		{
			if (!this.doAddGraveStones)
			{
				return null;
			}
			GridItem newGridItem = GridItem.GetNewGridItem();
			newGridItem.mGridItemType = GridItemType.GRIDITEM_GRAVESTONE;
			newGridItem.mGridItemCounter = -RandomNumbers.NextNumber(50);
			newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, theGridY, 3);
			newGridItem.mGridX = theGridX;
			newGridItem.mGridY = theGridY;
			this.mGridItems.Add(newGridItem);
			return newGridItem;
		}

		public int GetSurvivalFlagsCompleted()
		{
			int numWavesPerFlag = this.GetNumWavesPerFlag();
			int num = this.mChallenge.mSurvivalStage * this.GetNumWavesPerSurvivalStage() / numWavesPerFlag;
			if (this.IsFlagWave(this.mCurrentWave - 1) && this.mBoardFadeOutCounter < 0 && this.mNextSurvivalStageCounter == 0)
			{
				return (this.mCurrentWave - 1) / numWavesPerFlag + num;
			}
			return this.mCurrentWave / numWavesPerFlag + num;
		}

		public bool HasProgressMeter()
		{
			return this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST || this.mApp.IsFinalBossLevel() || this.mApp.IsSlotMachineLevel() || this.mApp.IsSquirrelLevel() || this.mApp.IsIZombieLevel() || (this.mProgressMeterWidth != 0 && !this.mApp.IsContinuousChallenge() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mApp.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM && !this.mApp.IsScaryPotterLevel());
		}

		public void UpdateCursor()
		{
			bool flag = false;
			bool flag2 = false;
			int num = this.mApp.mWidgetManager.mLastMouseX - this.mX;
			int num2 = this.mApp.mWidgetManager.mLastMouseY - this.mY;
			if (this.mApp.mSeedChooserScreen != null && this.mApp.mSeedChooserScreen.Contains(num + this.mX, num2 + this.mY))
			{
				return;
			}
			if (this.mApp.GetDialogCount() > 0)
			{
				return;
			}
			if (this.mPaused || this.mBoardFadeOutCounter >= 0 || this.mTimeStopCounter > 0 || this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
			{
				flag = false;
			}
			else
			{
				HitResult hitResult;
				this.MouseHitTest(num, num2, out hitResult, false);
				if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_MENU_BUTTON || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_STORE_BUTTON || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_SHOVEL || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_WATERING_CAN || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_FERTILIZER || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_BUG_SPRAY || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_PHONOGRAPH || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_CHOCOLATE || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_GLOVE || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_MONEY_SIGN || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_NEXT_GARDEN || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_WHEELBARROW || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_SLOT_MACHINE_HANDLE || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_TREE_FOOD)
				{
					flag = true;
				}
				else if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_SEEDPACKET)
				{
					SeedPacket seedPacket = (SeedPacket)hitResult.mObject;
					if (seedPacket.CanPickUp())
					{
						flag = true;
					}
				}
				else if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_SCARY_POT && this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL)
				{
					flag = true;
				}
				else if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_STINKY)
				{
					flag = true;
				}
				else if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_TREE_OF_WISDOM)
				{
					flag = true;
				}
				else if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_COIN || hitResult.mObjectType == GameObjectType.OBJECT_TYPE_PROJECTILE)
				{
					flag = true;
				}
				else if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
				{
					if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED && !this.HasLevelAwardDropped())
					{
						flag2 = true;
					}
					if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && !this.HasLevelAwardDropped())
					{
						flag2 = true;
					}
					Plant plant = (Plant)hitResult.mObject;
					if (plant.mState == PlantState.STATE_COBCANNON_READY && this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL)
					{
						flag = true;
					}
				}
				else if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_HAMMER)
				{
				}
				if (this.mChallenge.mBeghouledMouseCapture)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				return;
			}
			if (flag)
			{
				return;
			}
		}

		public void UpdateTutorial()
		{
			if (this.mTutorialTimer > 0)
			{
				this.mTutorialTimer--;
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PICK_UP_PEASHOOTER && this.mTutorialTimer == 0)
			{
				this.DisplayAdvice("[ADVICE_CLICK_PEASHOOTER]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1_STAY, AdviceType.ADVICE_NONE);
				float num = (float)(this.mSeedBank.mX + this.mSeedBank.mSeedPackets[0].mX);
				float num2 = 0f;
				this.TutorialArrowShow((int)(num + (float)(Constants.SMALL_SEEDPACKET_WIDTH / 2) - Constants.InvertAndScale(13f)), (int)num2);
				this.mTutorialTimer = -1;
			}
			if (this.mTutorialState == TutorialState.TUTORIAL_LEVEL_2_PICK_UP_SUNFLOWER || this.mTutorialState == TutorialState.TUTORIAL_LEVEL_2_PLANT_SUNFLOWER || this.mTutorialState == TutorialState.TUTORIAL_LEVEL_2_REFRESH_SUNFLOWER)
			{
				if (this.mTutorialTimer == 0)
				{
					this.DisplayAdvice("[ADVICE_PLANT_SUNFLOWER2]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL2, AdviceType.ADVICE_NONE);
					this.mTutorialTimer = -1;
				}
				else if (this.mZombieCountDown == 750 && this.mCurrentWave == 0)
				{
					this.DisplayAdvice("[ADVICE_PLANT_SUNFLOWER3]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL2, AdviceType.ADVICE_NONE);
				}
			}
			if ((this.mTutorialState == TutorialState.TUTORIAL_MORESUN_PICK_UP_SUNFLOWER || this.mTutorialState == TutorialState.TUTORIAL_MORESUN_PLANT_SUNFLOWER || this.mTutorialState == TutorialState.TUTORIAL_MORESUN_REFRESH_SUNFLOWER) && this.mTutorialTimer == 0)
			{
				this.DisplayAdvice("[ADVICE_PLANT_SUNFLOWER5]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LATER, AdviceType.ADVICE_PLANT_SUNFLOWER5);
				this.mTutorialTimer = -1;
			}
			if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel >= 3 && this.mLevel != 5 && this.mLevel <= 7 && this.mTutorialState == TutorialState.TUTORIAL_OFF && this.mCurrentWave >= 5 && !Board.gShownMoreSunTutorial && this.mSeedBank.mSeedPackets[1].CanPickUp() && this.CountPlantByType(SeedType.SEED_SUNFLOWER) < 3)
			{
				Debug.ASSERT(!this.ChooseSeedsOnCurrentLevel());
				this.DisplayAdvice("[ADVICE_PLANT_SUNFLOWER4]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LATER_STAY, AdviceType.ADVICE_NONE);
				GameConstants.gShownMoreSunTutorial = true;
				this.SetTutorialState(TutorialState.TUTORIAL_MORESUN_PICK_UP_SUNFLOWER);
				this.mTutorialTimer = 500;
			}
		}

		public SeedType GetSeedTypeInCursor()
		{
			if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_WHEEELBARROW && this.mApp.mZenGarden.GetPottedPlantInWheelbarrow() != null)
			{
				return this.mApp.mZenGarden.GetPottedPlantInWheelbarrow().mSeedType;
			}
			if (!this.IsPlantInCursor())
			{
				return SeedType.SEED_NONE;
			}
			if (this.mCursorObject.mType == SeedType.SEED_IMITATER)
			{
				return this.mCursorObject.mImitaterType;
			}
			return this.mCursorObject.mType;
		}

		public int CountPlantByType(SeedType theSeedType)
		{
			int num = 0;
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead && plant.mSeedType == theSeedType)
				{
					num++;
				}
			}
			return num;
		}

		public bool PlantingRequirementsMet(SeedType theSeedType)
		{
			return (theSeedType != SeedType.SEED_GATLINGPEA || this.CountPlantByType(SeedType.SEED_REPEATER) != 0) && (theSeedType != SeedType.SEED_WINTERMELON || this.CountPlantByType(SeedType.SEED_MELONPULT) != 0) && (theSeedType != SeedType.SEED_TWINSUNFLOWER || this.CountPlantByType(SeedType.SEED_SUNFLOWER) != 0) && (theSeedType != SeedType.SEED_SPIKEROCK || this.CountPlantByType(SeedType.SEED_SPIKEWEED) != 0) && (theSeedType != SeedType.SEED_COBCANNON || this.HasValidCobCannonSpot()) && (theSeedType != SeedType.SEED_GOLD_MAGNET || this.CountPlantByType(SeedType.SEED_MAGNETSHROOM) != 0) && (theSeedType != SeedType.SEED_GLOOMSHROOM || this.CountPlantByType(SeedType.SEED_FUMESHROOM) != 0) && (theSeedType != SeedType.SEED_CATTAIL || this.CountEmptyPotsOrLilies(SeedType.SEED_LILYPAD) != 0);
		}

		public bool HasValidCobCannonSpot()
		{
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead && plant.mSeedType == SeedType.SEED_KERNELPULT && this.IsValidCobCannonSpot(plant.mPlantCol, plant.mRow))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsValidCobCannonSpot(int theGridX, int theGridY)
		{
			if (!this.IsValidCobCannonSpotHelper(theGridX, theGridY) || !this.IsValidCobCannonSpotHelper(theGridX + 1, theGridY))
			{
				return false;
			}
			bool flag = this.GetFlowerPotAt(theGridX, theGridY) != null;
			bool flag2 = this.GetFlowerPotAt(theGridX + 1, theGridY) != null;
			return flag == flag2;
		}

		public bool IsValidCobCannonSpotHelper(int theGridX, int theGridY)
		{
			PlantsOnLawn plantsOnLawn = default(PlantsOnLawn);
			this.GetPlantsOnLawn(theGridX, theGridY, ref plantsOnLawn);
			if (plantsOnLawn.mPumpkinPlant != null)
			{
				return false;
			}
			if (!this.mApp.mEasyPlantingCheat)
			{
				return plantsOnLawn.mNormalPlant != null && plantsOnLawn.mNormalPlant.mSeedType == SeedType.SEED_KERNELPULT;
			}
			return (plantsOnLawn.mNormalPlant != null && plantsOnLawn.mNormalPlant.mSeedType == SeedType.SEED_KERNELPULT) || this.CanPlantAt(theGridX, theGridY, SeedType.SEED_KERNELPULT) == PlantingReason.PLANTING_OK;
		}

		public void MouseDownCobcannonFire(int x, int y, int theClickCount)
		{
			float num = TodCommon.Distance2D((float)x, (float)y, (float)this.mCobCannonMouseX, (float)this.mCobCannonMouseY);
			x = (int)((float)x * Constants.IS);
			y = (int)((float)y * Constants.IS);
			if (theClickCount < 0)
			{
				this.ClearCursor();
				return;
			}
			if (y < Constants.LAWN_YMIN)
			{
				this.ClearCursor();
				return;
			}
			if (this.mCobCannonCursorDelayCounter > 0 && num < 50f)
			{
				return;
			}
			if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_DUPLICATOR)
			{
				this.ClearCursor();
				return;
			}
			Plant plant = null;
			if (this.mCursorObject.mCobCannonPlantID != null && this.mPlants.IndexOf(this.mCursorObject.mCobCannonPlantID) >= 0)
			{
				plant = this.mCursorObject.mCobCannonPlantID;
			}
			if (plant == null)
			{
				this.ClearCursor();
				return;
			}
			plant.CobCannonFire(x, y);
			this.ClearCursor();
		}

		public int KillAllZombiesInRadius(int theRow, int theX, int theY, int theRadius, int theRowRange, bool theBurn, int theDamageRangeFlags)
		{
			int num = 0;
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && zombie.EffectedByDamage((uint)theDamageRangeFlags))
				{
					TRect zombieRect = zombie.GetZombieRect();
					int num2 = zombie.mRow - theRow;
					if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						num2 = 0;
					}
					if (num2 <= theRowRange && num2 >= -theRowRange && GameConstants.GetCircleRectOverlap(theX, theY, theRadius, zombieRect))
					{
						bool flag = zombie.IsDeadOrDying();
						if (theBurn)
						{
							zombie.ApplyBurn();
						}
						else
						{
							zombie.TakeDamage(1800, 18U);
						}
						if (!flag && zombie.IsDeadOrDying())
						{
							num++;
						}
					}
				}
			}
			int num3 = this.PixelToGridXKeepOnBoard(theX, theY);
			int num4 = this.PixelToGridYKeepOnBoard(theX, theY);
			int num5 = -1;
			GridItem gridItem = null;
			while (this.IterateGridItems(ref gridItem, ref num5))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_LADDER)
				{
					int num6 = gridItem.mGridX - num3;
					int num7 = gridItem.mGridY - num4;
					if (num6 <= theRowRange && num6 >= -theRowRange && num7 <= theRowRange && num7 >= -theRowRange)
					{
						gridItem.GridItemDie();
					}
				}
			}
			return num;
		}

		public bool IsFlagWave(int theWaveNumber)
		{
			if (this.mApp.IsFirstTimeAdventureMode() && this.mLevel == 1)
			{
				return false;
			}
			int numWavesPerFlag = this.GetNumWavesPerFlag();
			return theWaveNumber % numWavesPerFlag == numWavesPerFlag - 1;
		}

		public void DrawHouseDoorTop(Graphics g)
		{
			if (this.mBackground == BackgroundType.BACKGROUND_1_DAY)
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND1_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_1.X, Constants.Board_GameOver_Exterior_Overlay_1.Y);
				return;
			}
			if (this.mBackground == BackgroundType.BACKGROUND_2_NIGHT)
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND2_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_2.X, Constants.Board_GameOver_Exterior_Overlay_2.Y);
				return;
			}
			if (this.mBackground == BackgroundType.BACKGROUND_3_POOL)
			{
				if (Zombie.WinningZombieReachedDesiredY)
				{
					g.DrawImage(Resources.IMAGE_BACKGROUND3_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_3.X, Constants.Board_GameOver_Exterior_Overlay_3.Y);
					return;
				}
			}
			else
			{
				if (this.mBackground == BackgroundType.BACKGROUND_4_FOG)
				{
					g.DrawImage(Resources.IMAGE_BACKGROUND4_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_4.X, Constants.Board_GameOver_Exterior_Overlay_4.Y);
					return;
				}
				if (this.mBackground == BackgroundType.BACKGROUND_5_ROOF)
				{
					if (Zombie.WinningZombieReachedDesiredY)
					{
						g.DrawImage(Resources.IMAGE_BACKGROUND5_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_5.X, Constants.Board_GameOver_Exterior_Overlay_5.Y);
						return;
					}
				}
				else if (this.mBackground == BackgroundType.BACKGROUND_6_BOSS && Zombie.WinningZombieReachedDesiredY)
				{
					g.DrawImage(Resources.IMAGE_BACKGROUND6_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_6.X, Constants.Board_GameOver_Exterior_Overlay_6.Y);
				}
			}
		}

		public void DrawHouseDoorBottom(Graphics g)
		{
			if (this.mBackground == BackgroundType.BACKGROUND_1_DAY)
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND1_GAMEOVER_INTERIOR_OVERLAY, Constants.Board_GameOver_Interior_Overlay_1.X, Constants.Board_GameOver_Interior_Overlay_1.Y);
				return;
			}
			if (this.mBackground == BackgroundType.BACKGROUND_2_NIGHT)
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND2_GAMEOVER_INTERIOR_OVERLAY, Constants.Board_GameOver_Interior_Overlay_2.X, Constants.Board_GameOver_Interior_Overlay_2.Y);
				return;
			}
			if (this.mBackground == BackgroundType.BACKGROUND_3_POOL)
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND3_GAMEOVER_INTERIOR_OVERLAY, Constants.Board_GameOver_Interior_Overlay_3.X, Constants.Board_GameOver_Interior_Overlay_3.Y);
				return;
			}
			if (this.mBackground == BackgroundType.BACKGROUND_4_FOG)
			{
				g.DrawImage(Resources.IMAGE_BACKGROUND4_GAMEOVER_INTERIOR_OVERLAY, Constants.Board_GameOver_Interior_Overlay_4.X, Constants.Board_GameOver_Interior_Overlay_4.Y);
			}
		}

		public Zombie GetBossZombie()
		{
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
				{
					return zombie;
				}
			}
			return null;
		}

		public bool HasConveyorBeltSeedBank()
		{
			return this.mApp.IsFinalBossLevel() || this.mApp.IsMiniBossLevel() || this.mApp.IsShovelLevel() || this.mApp.IsWallnutBowlingLevel() || this.mApp.IsLittleTroubleLevel() || this.mApp.IsStormyNightLevel() || this.mApp.IsBungeeBlitzLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL;
		}

		public bool StageHasRoof()
		{
			return this.mBackground == BackgroundType.BACKGROUND_5_ROOF || this.mBackground == BackgroundType.BACKGROUND_6_BOSS;
		}

		public void SpawnZombiesFromPool()
		{
			if (this.mIceTrapCounter > 0)
			{
				return;
			}
			int num;
			int num2;
			if (this.mLevel == 21 || this.mLevel == 22 || this.mLevel == 31 || this.mLevel == 32)
			{
				num = 2;
				num2 = 3;
			}
			else if (this.mLevel == 23 || this.mLevel == 24 || this.mLevel == 25 || this.mLevel == 33 || this.mLevel == 34 || this.mLevel == 35)
			{
				num = 3;
				num2 = 5;
			}
			else
			{
				num = 3;
				num2 = 7;
			}
			int num3 = 0;
			for (int i = 0; i < Board.aGridArray.Length; i++)
			{
				Board.aGridArray[i].Reset();
			}
			for (int j = 5; j < Constants.GRIDSIZEX; j++)
			{
				for (int k = 2; k <= 3; k++)
				{
					Board.aGridArray[num3].mX = j;
					Board.aGridArray[num3].mY = k;
					Board.aGridArray[num3].mWeight = 10000;
					num3++;
					Debug.ASSERT(num3 <= 10);
				}
			}
			if (num > num3)
			{
				num = num3;
			}
			if (num3 == 0)
			{
				return;
			}
			for (int l = 0; l < num; l++)
			{
				TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(Board.aGridArray, num3);
				todWeightedGridArray.mWeight = 0;
				ZombieType theZombieType = this.PickGraveRisingZombieType(num2);
				ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
				Zombie zombie = this.AddZombieInRow(theZombieType, todWeightedGridArray.mY, this.mCurrentWave);
				if (zombie == null)
				{
					return;
				}
				zombie.RiseFromGrave(todWeightedGridArray.mX, todWeightedGridArray.mY);
				zombie.mUsesClipping = true;
				num2 -= zombieDefinition.mZombieValue;
				num2 = Math.Max(1, num2);
			}
		}

		public void SpawnZombiesFromSky()
		{
			if (this.mIceTrapCounter > 0)
			{
				return;
			}
			int num;
			int num2;
			if (this.mLevel == 41 || this.mLevel == 42)
			{
				num = 2;
				num2 = 3;
			}
			else if (this.mLevel == 43 || this.mLevel == 44 || this.mLevel == 45)
			{
				num = 3;
				num2 = 5;
			}
			else
			{
				num = 3;
				num2 = 7;
			}
			BungeeDropGrid bungeeDropGrid = new BungeeDropGrid();
			this.SetupBungeeDrop(bungeeDropGrid);
			if (num > bungeeDropGrid.mGridArrayCount)
			{
				num = bungeeDropGrid.mGridArrayCount;
			}
			if (bungeeDropGrid.mGridArrayCount == 0)
			{
				return;
			}
			for (int i = 0; i < num; i++)
			{
				ZombieType theZombieType = this.PickGraveRisingZombieType(num2);
				this.BungeeDropZombie(bungeeDropGrid, theZombieType);
				ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
				num2 -= zombieDefinition.mZombieValue;
				num2 = Math.Max(1, num2);
			}
		}

		public void PickUpTool(GameObjectType theObjectType)
		{
			if (this.mPaused)
			{
				return;
			}
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING && !this.mCutScene.IsInShovelTutorial())
			{
				return;
			}
			if (theObjectType == GameObjectType.OBJECT_TYPE_SHOVEL)
			{
				if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_SHOVEL)
				{
					this.ClearCursor();
					return;
				}
				if (this.mTutorialState == TutorialState.TUTORIAL_SHOVEL_PICKUP)
				{
					this.SetTutorialState(TutorialState.TUTORIAL_SHOVEL_DIG);
				}
				this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_SHOVEL;
				this.mApp.PlayFoley(FoleyType.FOLEY_SHOVEL);
			}
			else if (theObjectType == GameObjectType.OBJECT_TYPE_WATERING_CAN)
			{
				if (this.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_PICKUP_WATER)
				{
					this.mTutorialState = TutorialState.TUTORIAL_ZEN_GARDEN_WATER_PLANT;
					this.mApp.mPlayerInfo.mZenTutorialMessage = 23;
					this.DisplayAdvice("[ADVICE_ZEN_GARDEN_WATER_PLANT]", MessageStyle.MESSAGE_STYLE_ZEN_GARDEN_LONG, AdviceType.ADVICE_NONE);
					this.TutorialArrowRemove();
				}
				this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_WATERING_CAN;
				if (this.mApp.mPlayerInfo.mPurchases[13] > 0)
				{
					this.mIgnoreNextMouseUp = true;
				}
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
			}
			else if (theObjectType == GameObjectType.OBJECT_TYPE_FERTILIZER)
			{
				if (this.mApp.mPlayerInfo.mPurchases[14] > 1000)
				{
					this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_FERTILIZER;
					this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				}
				else
				{
					this.mApp.PlaySample(Resources.SOUND_BUZZER);
				}
			}
			else if (theObjectType == GameObjectType.OBJECT_TYPE_BUG_SPRAY)
			{
				if (this.mApp.mPlayerInfo.mPurchases[15] > 1000)
				{
					this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_BUG_SPRAY;
					this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				}
				else
				{
					this.mApp.PlaySample(Resources.SOUND_BUZZER);
				}
			}
			else if (theObjectType == GameObjectType.OBJECT_TYPE_PHONOGRAPH)
			{
				this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_PHONOGRAPH;
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
			}
			else if (theObjectType == GameObjectType.OBJECT_TYPE_CHOCOLATE)
			{
				if (this.mApp.mPlayerInfo.mPurchases[26] > 1000)
				{
					this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_CHOCOLATE;
					this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				}
				else
				{
					this.mApp.PlaySample(Resources.SOUND_BUZZER);
				}
			}
			else if (theObjectType == GameObjectType.OBJECT_TYPE_GLOVE)
			{
				this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_GLOVE;
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
			}
			else if (theObjectType == GameObjectType.OBJECT_TYPE_MONEY_SIGN)
			{
				this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_MONEY_SIGN;
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
			}
			else if (theObjectType == GameObjectType.OBJECT_TYPE_WHEELBARROW)
			{
				this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_WHEEELBARROW;
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
			}
			else if (theObjectType == GameObjectType.OBJECT_TYPE_TREE_FOOD)
			{
				if (this.mChallenge.TreeOfWisdomCanFeed())
				{
					if (this.mApp.mPlayerInfo.mPurchases[28] > 1000)
					{
						this.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_TREE_FOOD;
						this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
					}
					else
					{
						this.mApp.PlaySample(Resources.SOUND_BUZZER);
					}
				}
			}
			else
			{
				Debug.ASSERT(false);
			}
			this.mCursorObject.mType = SeedType.SEED_NONE;
		}

		public void TutorialArrowShow(int x, int y)
		{
			this.TutorialArrowRemove();
			TodParticleSystem theParticle = this.mApp.AddTodParticle((float)(x - Constants.Board_Offset_AspectRatio_Correction) * Constants.IS, (float)y * Constants.IS, 800000, ParticleEffect.PARTICLE_SEED_PACKET_PICK);
			this.mTutorialParticleID = this.mApp.ParticleGetID(theParticle);
		}

		public void TutorialArrowRemove()
		{
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mTutorialParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.ParticleSystemDie();
			}
			this.mTutorialParticleID = null;
		}

		public int CountCoinsBeingCollected()
		{
			int num = 0;
			Coin coin = null;
			while (this.IterateCoins(ref coin))
			{
				if (coin.mIsBeingCollected && coin.IsMoney())
				{
					num += Coin.GetCoinValue(coin.mType);
				}
			}
			return num;
		}

		public void BungeeDropZombie(BungeeDropGrid theBungeeDropGrid, ZombieType theZombieType)
		{
			TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(theBungeeDropGrid.mGridArray, theBungeeDropGrid.mGridArrayCount);
			todWeightedGridArray.mWeight = 1;
			Zombie zombie = this.AddZombie(ZombieType.ZOMBIE_BUNGEE, this.mCurrentWave);
			Zombie zombie2 = this.AddZombie(theZombieType, this.mCurrentWave);
			Debug.ASSERT(zombie != null && zombie2 != null);
			zombie.BungeeDropZombie(zombie2, todWeightedGridArray.mX, todWeightedGridArray.mY);
		}

		public void SetupBungeeDrop(BungeeDropGrid theBungeeDropGrid)
		{
			theBungeeDropGrid.mGridArrayCount = 0;
			for (int i = 4; i < Constants.GRIDSIZEX; i++)
			{
				for (int j = 0; j <= 4; j++)
				{
					int mGridArrayCount = theBungeeDropGrid.mGridArrayCount;
					theBungeeDropGrid.mGridArray[mGridArrayCount].mX = i;
					theBungeeDropGrid.mGridArray[mGridArrayCount].mY = j;
					theBungeeDropGrid.mGridArray[mGridArrayCount].mWeight = 10000;
					theBungeeDropGrid.mGridArrayCount++;
					Debug.ASSERT(theBungeeDropGrid.mGridArrayCount <= theBungeeDropGrid.mGridArray.Length);
				}
			}
		}

		public void PutZombieInWave(ZombieType theZombieType, int theWaveNumber, ZombiePicker theZombiePicker)
		{
			Debug.ASSERT(theWaveNumber < 100 && theZombiePicker.mZombieCount < 50);
			this.mZombiesInWave[theWaveNumber, theZombiePicker.mZombieCount] = theZombieType;
			theZombiePicker.mZombieCount++;
			if (theZombiePicker.mZombieCount < 50)
			{
				this.mZombiesInWave[theWaveNumber, theZombiePicker.mZombieCount] = ZombieType.ZOMBIE_INVALID;
			}
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
			theZombiePicker.mZombiePoints -= zombieDefinition.mZombieValue;
			theZombiePicker.mZombieTypeCount[(int)theZombieType]++;
			theZombiePicker.mAllWavesZombieTypeCount[(int)theZombieType]++;
		}

		public void PutInMissingZombies(int theWaveNumber, ZombiePicker theZombiePicker)
		{
			for (int i = 0; i < 33; i++)
			{
				ZombieType zombieType = (ZombieType)i;
				if (theZombiePicker.mZombieTypeCount[(int)zombieType] <= 0 && zombieType != ZombieType.ZOMBIE_YETI && Board.CanZombieSpawnOnLevel(zombieType, this.mLevel))
				{
					this.PutZombieInWave(zombieType, theWaveNumber, theZombiePicker);
				}
			}
		}

		public TRect GetShovelButtonRect()
		{
			TRect result = new TRect(Constants.UIShovelButtonPosition.X - Constants.Board_Offset_AspectRatio_Correction, Constants.UIShovelButtonPosition.Y, AtlasResources.IMAGE_SHOVELBANK.mWidth, AtlasResources.IMAGE_SHOVELBANK.mHeight);
			if (this.mApp.IsSquirrelLevel())
			{
				result.mX = 600;
			}
			return result;
		}

		public TRect GetZenShovelButtonRect()
		{
			TRect result = new TRect(Constants.ZenGardenTopButtonStart, Constants.UIShovelButtonPosition.Y, AtlasResources.IMAGE_SHOVELBANK_ZEN.mWidth, AtlasResources.IMAGE_SHOVELBANK_ZEN.mHeight);
			if (this.mApp.IsSlotMachineLevel() || this.mApp.IsSquirrelLevel())
			{
				result.mX = 600;
			}
			return result;
		}

		public TRect GetZenButtonRect(GameObjectType theObjectType)
		{
			TRect zenShovelButtonRect = this.GetZenShovelButtonRect();
			if (theObjectType == GameObjectType.OBJECT_TYPE_NEXT_GARDEN)
			{
				zenShovelButtonRect.mX = Constants.ZenGarden_NextGarden_Pos.X;
				return zenShovelButtonRect;
			}
			bool flag = true;
			for (int i = 6; i <= 15; i++)
			{
				GameObjectType gameObjectType = (GameObjectType)i;
				if (gameObjectType != GameObjectType.OBJECT_TYPE_TREE_FOOD && !this.CanUseGameObject(gameObjectType))
				{
					flag = false;
				}
			}
			if (flag)
			{
				zenShovelButtonRect.mX = Constants.ZenGardenTopButtonStart;
			}
			for (int j = 6; j < (int)theObjectType; j++)
			{
				GameObjectType theGameObject = (GameObjectType)j;
				if (this.CanUseGameObject(theGameObject))
				{
					zenShovelButtonRect.mX += AtlasResources.IMAGE_SHOVELBANK_ZEN.GetWidth();
				}
			}
			return zenShovelButtonRect;
		}

		public Plant NewPlant(int theGridX, int theGridY, SeedType theSeedType, SeedType theImitaterType)
		{
			Plant newPlant = Plant.GetNewPlant();
			newPlant.mIsOnBoard = true;
			newPlant.PlantInitialize(theGridX, theGridY, theSeedType, theImitaterType);
			this.mPlants.Add(newPlant);
			return newPlant;
		}

		public void DoPlantingEffects(int theGridX, int theGridY, Plant thePlant, bool forAquarium)
		{
			int num = this.GridToPixelX(theGridX, theGridY) + 41;
			int num2 = this.GridToPixelY(theGridX, theGridY) + 74;
			if (thePlant != null)
			{
				if (thePlant.mSeedType == SeedType.SEED_LILYPAD)
				{
					num2 += 15;
				}
				else if (thePlant.mSeedType == SeedType.SEED_FLOWERPOT)
				{
					num2 += 30;
				}
			}
			if (Plant.IsFlying(thePlant.mSeedType))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_PLANT);
				return;
			}
			if (forAquarium || this.IsPoolSquare(theGridX, theGridY))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_PLANT_WATER);
				if (forAquarium)
				{
					num2 -= 30;
				}
				this.mApp.AddTodParticle((float)num, (float)num2, 400000, ParticleEffect.PARTICLE_PLANTING_POOL);
				return;
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_PLANT);
			this.mApp.AddTodParticle((float)num, (float)num2, 400000, ParticleEffect.PARTICLE_PLANTING);
		}

		public bool IsFinalSurvivalStage()
		{
			return false;
		}

		public void SurvivalSaveScore()
		{
			if (!this.mApp.IsSurvivalMode())
			{
				return;
			}
			int survivalFlagsCompleted = this.GetSurvivalFlagsCompleted();
			int currentChallengeIndex = this.mApp.GetCurrentChallengeIndex();
			if (survivalFlagsCompleted > this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex])
			{
				this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex] = survivalFlagsCompleted;
				this.mApp.WriteCurrentUserConfig();
			}
		}

		public int CountZombiesOnScreen()
		{
			int num = 0;
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && zombie.mHasHead && !zombie.IsDeadOrDying() && !zombie.mMindControlled && zombie.IsOnBoard())
				{
					num++;
				}
			}
			return num;
		}

		public int GetNumWavesPerSurvivalStage()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				return 10;
			}
			if (this.mApp.IsSurvivalNormal(this.mApp.mGameMode))
			{
				return 10;
			}
			if (this.mApp.IsSurvivalHard(this.mApp.mGameMode))
			{
				return 20;
			}
			if (this.mApp.IsSurvivalEndless(this.mApp.mGameMode))
			{
				return 20;
			}
			Debug.ASSERT(false);
			return -666;
		}

		public int GetLevelRandSeed()
		{
			int num = 101;
			int num2 = this.mBoardRandSeed + (int)this.mApp.mPlayerInfo.mId;
			if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
			{
				num2 += this.mLevel + this.mApp.mPlayerInfo.mFinishedAdventure * num;
			}
			else if (this.mApp.IsScaryPotterLevel() || this.mApp.IsIZombieLevel())
			{
				RandomNumbers.Seed();
				num2 = RandomNumbers.NextNumber();
			}
			else
			{
				num2 = (int)(num2 + (this.mApp.mGameMode + this.mChallenge.mSurvivalStage * num));
			}
			return num2;
		}

		public void AddBossRenderItem(RenderItem[] theRenderList, ref int theCurRenderItem, Zombie theBossZombie)
		{
			Debug.ASSERT(theCurRenderItem < 2048);
			int theRow = 1;
			int theRow2 = 3;
			int theRow3 = 4;
			if (theBossZombie.IsDeadOrDying())
			{
				theRow3 = 1;
			}
			else if (theBossZombie.mZombiePhase == ZombiePhase.PHASE_BOSS_STOMPING)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(theBossZombie.mBodyReanimID);
				if (reanimation.mAnimTime > 0.25f && reanimation.mAnimTime < 0.75f)
				{
					if (theBossZombie.mTargetRow == 1)
					{
						theRow = 2;
					}
					else if (theBossZombie.mTargetRow == 3)
					{
						theRow2 = 4;
					}
				}
			}
			RenderItem renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = RenderObjectType.RENDER_ITEM_BOSS_PART;
			renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_BOSS, theRow, 2);
			renderItem.mBossPart = BossPart.BOSS_PART_BACK_LEG;
			theCurRenderItem++;
			renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = RenderObjectType.RENDER_ITEM_BOSS_PART;
			renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_BOSS, theRow2, 2);
			renderItem.mBossPart = BossPart.BOSS_PART_FRONT_LEG;
			theCurRenderItem++;
			renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = RenderObjectType.RENDER_ITEM_BOSS_PART;
			renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_BOSS, 4, 2);
			renderItem.mBossPart = BossPart.BOSS_PART_MAIN;
			theCurRenderItem++;
			renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = RenderObjectType.RENDER_ITEM_BOSS_PART;
			renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_BOSS, theRow3, 3);
			renderItem.mBossPart = BossPart.BOSS_PART_BACK_ARM;
			theCurRenderItem++;
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(theBossZombie.mBossFireBallReanimID);
			if (reanimation2 != null)
			{
				renderItem = theRenderList[theCurRenderItem];
				renderItem.mRenderObjectType = RenderObjectType.RENDER_ITEM_BOSS_PART;
				renderItem.mZPos = reanimation2.mRenderOrder;
				renderItem.mBossPart = BossPart.BOSS_PART_FIREBALL;
				theCurRenderItem++;
			}
		}

		public GridItem GetCraterAt(int theGridX, int theGridY)
		{
			return this.GetGridItemAt(GridItemType.GRIDITEM_CRATER, theGridX, theGridY);
		}

		public GridItem GetGraveStoneAt(int theGridX, int theGridY)
		{
			return this.GetGridItemAt(GridItemType.GRIDITEM_GRAVESTONE, theGridX, theGridY);
		}

		public GridItem GetLadderAt(int theGridX, int theGridY)
		{
			return this.GetGridItemAt(GridItemType.GRIDITEM_LADDER, theGridX, theGridY);
		}

		public GridItem AddALadder(int theGridX, int theGridY)
		{
			GridItem newGridItem = GridItem.GetNewGridItem();
			newGridItem.mGridItemType = GridItemType.GRIDITEM_LADDER;
			newGridItem.mRenderOrder = 302000 + 10000 * theGridY + 800;
			newGridItem.mGridX = theGridX;
			newGridItem.mGridY = theGridY;
			this.mGridItems.Add(newGridItem);
			return newGridItem;
		}

		public GridItem AddACrater(int theGridX, int theGridY)
		{
			GridItem newGridItem = GridItem.GetNewGridItem();
			newGridItem.mGridItemType = GridItemType.GRIDITEM_CRATER;
			newGridItem.mRenderOrder = 200000 + 10000 * theGridY + 1;
			newGridItem.mGridX = theGridX;
			newGridItem.mGridY = theGridY;
			this.mGridItems.Add(newGridItem);
			return newGridItem;
		}

		public void InitLawnMowers()
		{
			for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
			{
				bool flag = (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RESODDED && i <= 4) || ((this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode()) && this.mLevel == 35) || (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mApp.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_LAST_STAND && !this.mApp.IsScaryPotterLevel() && !this.mApp.IsSquirrelLevel() && !this.mApp.IsIZombieLevel() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM && (!this.StageHasRoof() || this.mApp.mPlayerInfo.mPurchases[23] != 0) && this.mPlantRow[i] != PlantRowType.PLANTROW_DIRT);
				if (flag)
				{
					LawnMower newLawnMower = LawnMower.GetNewLawnMower();
					newLawnMower.LawnMowerInitialize(i);
					newLawnMower.mVisible = false;
					this.mLawnMowers.Add(newLawnMower);
				}
			}
		}

		public bool IsPlantInCursor()
		{
			return this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_BANK || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_USABLE_COIN || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_GLOVE || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_DUPLICATOR || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_WHEEL_BARROW;
		}

		public void ClearFogAroundPlant(Plant thePlant, int theSize)
		{
			int num = 6;
			if (this.mFogBlownCountDown > 0 && this.mFogBlownCountDown < 2000)
			{
				num = 2;
			}
			else if (this.mFogBlownCountDown > 0)
			{
				num = 40;
			}
			int num2 = this.LeftFogColumn();
			for (int i = thePlant.mPlantCol - theSize; i <= thePlant.mPlantCol + theSize; i++)
			{
				int num3 = i + -(((int)this.mFogOffset + 50) / 100);
				for (int j = thePlant.mRow - theSize; j <= thePlant.mRow + theSize; j++)
				{
					if (num3 >= num2 && num3 < Constants.GRIDSIZEX && j >= 0 && j < 7)
					{
						int num4 = Math.Abs(i - thePlant.mPlantCol);
						int num5 = Math.Abs(j - thePlant.mRow);
						if (theSize == 4)
						{
							if (num4 > 3 || num5 > 2)
							{
								goto IL_D3;
							}
							if (num4 + num5 == 5)
							{
								goto IL_D3;
							}
						}
						else if (num4 + num5 > theSize)
						{
							goto IL_D3;
						}
						this.mGridCelFog[num3, j] = Math.Max(this.mGridCelFog[num3, j] - num, 0);
					}
					IL_D3:;
				}
			}
		}

		public void RemoveParticleByType(ParticleEffect theEffectType)
		{
			int num = -1;
			TodParticleSystem todParticleSystem = null;
			while (this.IterateParticles(ref todParticleSystem, ref num))
			{
				if (todParticleSystem.mEffectType == theEffectType)
				{
					todParticleSystem.ParticleSystemDie();
				}
			}
		}

		public GridItem GetScaryPotAt(int theGridX, int theGridY)
		{
			return this.GetGridItemAt(GridItemType.GRIDITEM_SCARY_POT, theGridX, theGridY);
		}

		public void PuzzleSaveStreak()
		{
			if (!this.mApp.IsEndlessScaryPotter(this.mApp.mGameMode) && !this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
			{
				return;
			}
			int num = this.mChallenge.mSurvivalStage + 1;
			int currentChallengeIndex = this.mApp.GetCurrentChallengeIndex();
			if (num > this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex])
			{
				this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex] = num;
				this.mApp.WriteCurrentUserConfig();
			}
		}

		public void ClearAdviceImmediately()
		{
			this.ClearAdvice(AdviceType.ADVICE_NONE);
			this.mAdvice.mDuration = 0;
		}

		public bool IsFinalScaryPotterStage()
		{
			if (!this.mApp.IsScaryPotterLevel())
			{
				return false;
			}
			if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
			{
				return this.mChallenge.mSurvivalStage == 2;
			}
			return !this.mApp.IsEndlessScaryPotter(this.mApp.mGameMode);
		}

		public void DisplayAdviceAgain(string theAdvice, MessageStyle theMessageStyle, AdviceType theHelpIndex)
		{
			if (theHelpIndex != AdviceType.ADVICE_NONE && this.mHelpDisplayed[(int)theHelpIndex])
			{
				this.mHelpDisplayed[(int)theHelpIndex] = false;
			}
			this.DisplayAdvice(theAdvice, theMessageStyle, theHelpIndex);
		}

		public GridItem GetSquirrelAt(int theGridX, int theGridY)
		{
			return this.GetGridItemAt(GridItemType.GRIDITEM_SQUIRREL, theGridX, theGridY);
		}

		public GridItem GetZenToolAt(int theGridX, int theGridY)
		{
			return this.GetGridItemAt(GridItemType.GRIDITEM_ZEN_TOOL, theGridX, theGridY);
		}

		public bool IsPlantInGoldWateringCanRange(int theMouseX, int theMouseY, Plant thePlant)
		{
			int num = theMouseX + Constants.ZenGarden_GoldenWater_Size.X;
			int num2 = theMouseX + Constants.ZenGarden_GoldenWater_Size.Width;
			int num3 = theMouseY + Constants.ZenGarden_GoldenWater_Size.Y;
			int num4 = theMouseY + Constants.ZenGarden_GoldenWater_Size.Height;
			return this.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, PlantPriority.TOPPLANT_ZEN_TOOL_ORDER) == thePlant && (thePlant.mX + 40 >= num && thePlant.mX + 40 < num2 && thePlant.mY + 40 >= num3 && thePlant.mY + 40 < num4);
		}

		public bool StageHasZombieWalkInFromRight()
		{
			return !this.mApp.IsWhackAZombieLevel() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ICE && !this.mApp.IsFinalBossLevel() && !this.mApp.IsIZombieLevel() && !this.mApp.IsSquirrelLevel() && !this.mApp.IsScaryPotterLevel() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mApp.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM;
		}

		public void PlaceRake()
		{
			if (this.mApp.mPlayerInfo.mPurchases[24] == 0)
			{
				return;
			}
			int num;
			if (this.mApp.IsScaryPotterLevel())
			{
				num = 7;
				int num2 = -1;
				GridItem gridItem = null;
				while (this.IterateGridItems(ref gridItem, ref num2))
				{
					if (gridItem.mGridItemType == GridItemType.GRIDITEM_SCARY_POT && gridItem.mGridX <= num && gridItem.mGridX > 0)
					{
						num = gridItem.mGridX - 1;
					}
				}
			}
			else
			{
				if (!this.StageHasZombieWalkInFromRight() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA)
				{
					return;
				}
				num = 7;
			}
			int num3 = 0;
			for (int i = 0; i < Board.aPickArray.Length; i++)
			{
				Board.aPickArray[i].Reset();
			}
			for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
			{
				if (j != 5 && this.mPlantRow[j] == PlantRowType.PLANTROW_NORMAL)
				{
					Board.aPickArray[num3].mWeight = 1;
					Board.aPickArray[num3].mItem = j;
					num3++;
				}
			}
			if (num3 == 0)
			{
				return;
			}
			int mGridY = (int)TodCommon.TodPickFromWeightedArray(Board.aPickArray, num3);
			this.mApp.mPlayerInfo.mPurchases[24]--;
			GridItem newGridItem = GridItem.GetNewGridItem();
			newGridItem.mGridItemType = GridItemType.GRIDITEM_RAKE;
			newGridItem.mGridX = num;
			newGridItem.mGridY = mGridY;
			newGridItem.mPosX = (float)this.GridToPixelX(newGridItem.mGridX, newGridItem.mGridY);
			newGridItem.mPosY = (float)this.GridToPixelY(newGridItem.mGridX, newGridItem.mGridY);
			newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, newGridItem.mGridY, 9);
			this.mGridItems.Add(newGridItem);
			Reanimation theReanimation = this.CreateRakeReanim(newGridItem.mPosX, newGridItem.mPosY, 0);
			newGridItem.mGridItemReanimID = this.mApp.ReanimationGetID(theReanimation);
			newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_RAKE_ATTRACTING;
		}

		public Reanimation CreateRakeReanim(float rakeX, float rakeY, int renderOrder)
		{
			Reanimation reanimation = this.mApp.AddReanimation(rakeX + 20f, rakeY, renderOrder, ReanimationType.REANIM_RAKE);
			reanimation.mAnimRate = 0f;
			reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
			reanimation.mIsAttachment = true;
			return reanimation;
		}

		public GridItem GetRake()
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_RAKE)
				{
					return gridItem;
				}
			}
			return null;
		}

		public bool IsScaryPotterDaveTalking()
		{
			return this.mApp.IsScaryPotterLevel() && this.mNextSurvivalStageCounter > 0 && this.mApp.mCrazyDaveState != CrazyDaveState.CRAZY_DAVE_OFF;
		}

		public Zombie GetWinningZombie()
		{
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && zombie.mFromWave == GameConstants.ZOMBIE_WAVE_WINNER)
				{
					return zombie;
				}
			}
			return null;
		}

		public void ResetFPSStats()
		{
			int tickCount = Environment.TickCount;
			this.mStartDrawTime = tickCount;
			this.mIntervalDrawTime = tickCount;
			this.mDrawCount = 1;
			this.mIntervalDrawCountStart = 1;
		}

		public int CountEmptyPotsOrLilies(SeedType theSeedType)
		{
			int num = 0;
			int count = this.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mPlants[i];
				if (!plant.mDead && plant.mSeedType == theSeedType && this.GetTopPlantAt(plant.mPlantCol, plant.mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION) == null)
				{
					num++;
				}
			}
			return num;
		}

		public GridItem GetGridItemAt(GridItemType theGridItemType, int theGridX, int theGridY)
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridX == theGridX && gridItem.mGridY == theGridY && gridItem.mGridItemType == theGridItemType)
				{
					return gridItem;
				}
			}
			return null;
		}

		public bool ProgressMeterHasFlags()
		{
			return (!this.mApp.IsFirstTimeAdventureMode() || this.mLevel != 1) && !this.mApp.IsWhackAZombieLevel() && !this.mApp.IsFinalBossLevel() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && !this.mApp.IsSlotMachineLevel() && !this.mApp.IsSquirrelLevel() && !this.mApp.IsIZombieLevel();
		}

		public bool IsLastStandFinalStage()
		{
			return this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && this.mChallenge.mSurvivalStage == 4;
		}

		public int GetNumWavesPerFlag()
		{
			if (this.mApp.IsFirstTimeAdventureMode() && this.mNumWaves < 10)
			{
				return this.mNumWaves;
			}
			return 10;
		}

		public int GetCurrentPlantCost(SeedType theSeedType, SeedType theImitaterType)
		{
			int num = Plant.GetCost(theSeedType, theImitaterType);
			if (this.PlantUsesAcceleratedPricing(theSeedType))
			{
				int num2 = this.CountPlantByType(theSeedType);
				num += num2 * 50;
			}
			return num;
		}

		public bool PlantUsesAcceleratedPricing(SeedType theSeedType)
		{
			return Plant.IsUpgrade(theSeedType) && this.mApp.IsSurvivalEndless(this.mApp.mGameMode);
		}

		public void FreezeEffectsForCutscene(bool theFreeze)
		{
			int num = -1;
			TodParticleSystem todParticleSystem = null;
			while (this.IterateParticles(ref todParticleSystem, ref num))
			{
				if (todParticleSystem.mEffectType == ParticleEffect.PARTICLE_GRAVE_BUSTER)
				{
					todParticleSystem.mDontUpdate = theFreeze;
				}
				if (todParticleSystem.mEffectType == ParticleEffect.PARTICLE_POOL_SPARKLY && this.mIceTrapCounter == 0)
				{
					todParticleSystem.mDontUpdate = theFreeze;
				}
			}
			int num2 = -1;
			Reanimation reanimation = null;
			while (this.IterateReanimations(ref reanimation, ref num2))
			{
				if (reanimation.mReanimationType == ReanimationType.REANIM_SLEEPING)
				{
					if (theFreeze)
					{
						reanimation.mAnimRate = 0f;
					}
					else
					{
						reanimation.mAnimRate = TodCommon.RandRangeFloat(6f, 8f);
					}
				}
			}
		}

		public void LoadBackgroundImages()
		{
			if (this.mBackground == BackgroundType.BACKGROUND_1_DAY)
			{
				this.mApp.DelayLoadBackgroundResource("DelayLoad_Background1");
				if (this.mLevel <= 4 && this.mApp.IsFirstTimeAdventureMode())
				{
					TodCommon.TodLoadResources("DelayLoad_BackgroundUnsodded");
					return;
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RESODDED)
				{
					TodCommon.TodLoadResources("DelayLoad_BackgroundUnsodded");
					return;
				}
			}
			else
			{
				if (this.mBackground == BackgroundType.BACKGROUND_2_NIGHT)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background2");
					return;
				}
				if (this.mBackground == BackgroundType.BACKGROUND_3_POOL)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background3");
					return;
				}
				if (this.mBackground == BackgroundType.BACKGROUND_4_FOG)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background4");
					return;
				}
				if (this.mBackground == BackgroundType.BACKGROUND_5_ROOF)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background5");
					return;
				}
				if (this.mBackground == BackgroundType.BACKGROUND_6_BOSS)
				{
					this.mApp.DelayLoadBackgroundResource("DelayLoad_Background6");
					return;
				}
				if (this.mBackground == BackgroundType.BACKGROUND_GREENHOUSE)
				{
					this.mApp.DelayLoadZenGardenBackground("DelayLoad_GreenHouseGarden");
					return;
				}
				if (this.mBackground == BackgroundType.BACKGROUND_TREE_OF_WISDOM)
				{
					return;
				}
				if (this.mBackground == BackgroundType.BACKGROUND_ZOMBIQUARIUM)
				{
					this.mApp.DelayLoadZenGardenBackground("DelayLoad_Zombiquarium");
					this.mApp.DelayLoadZenGardenBackground("DelayLoad_GreenHouseOverlay");
					return;
				}
				if (this.mBackground == BackgroundType.BACKGROUND_MUSHROOM_GARDEN)
				{
					this.mApp.DelayLoadZenGardenBackground("DelayLoad_MushroomGarden");
					return;
				}
				Debug.ASSERT(false);
			}
		}

		public bool CanUseGameObject(GameObjectType theGameObject)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				return theGameObject == GameObjectType.OBJECT_TYPE_TREE_FOOD || theGameObject == GameObjectType.OBJECT_TYPE_NEXT_GARDEN;
			}
			if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				return false;
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_WATERING_CAN)
			{
				return true;
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_NEXT_GARDEN)
			{
				return this.mApp.mPlayerInfo.mPurchases[18] != 0 || this.mApp.mPlayerInfo.mPurchases[25] != 0 || this.mApp.mPlayerInfo.mPurchases[27] != 0;
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_FERTILIZER)
			{
				return this.mApp.mPlayerInfo.mPurchases[14] > 0;
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_BUG_SPRAY)
			{
				return this.mApp.mPlayerInfo.mPurchases[15] > 0;
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_PHONOGRAPH)
			{
				return this.mApp.mPlayerInfo.mPurchases[16] > 0;
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_CHOCOLATE)
			{
				return this.mApp.mPlayerInfo.mPurchases[26] > 0;
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_WHEELBARROW)
			{
				return this.mApp.mPlayerInfo.mPurchases[19] > 0;
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_GLOVE)
			{
				return this.mApp.mPlayerInfo.mPurchases[17] > 0;
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_MONEY_SIGN)
			{
				return this.mApp.HasFinishedAdventure();
			}
			if (theGameObject == GameObjectType.OBJECT_TYPE_TREE_FOOD)
			{
				return false;
			}
			Debug.ASSERT(false);
			return false;
		}

		public void SetMustacheMode(bool theEnableMustache)
		{
			this.mApp.PlayFoley(FoleyType.FOLEY_POLEVAULT);
			this.mMustacheMode = theEnableMustache;
			this.mApp.mMustacheMode = theEnableMustache;
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead)
				{
					zombie.EnableMustache(theEnableMustache);
				}
			}
		}

		public int CountCoinByType(CoinType theCoinType)
		{
			int num = 0;
			Coin coin = null;
			while (this.IterateCoins(ref coin))
			{
				if (coin.mType == theCoinType)
				{
					num++;
				}
			}
			return num;
		}

		public void SetSuperMowerMode(bool theEnableSuperMower)
		{
			this.mApp.PlayFoley(FoleyType.FOLEY_ZAMBONI);
			this.mSuperMowerMode = theEnableSuperMower;
			this.mApp.mSuperMowerMode = theEnableSuperMower;
			LawnMower lawnMower = null;
			while (this.IterateLawnMowers(ref lawnMower))
			{
				lawnMower.EnableSuperMower(theEnableSuperMower);
			}
		}

		public void DrawZenWheelBarrowButton(Graphics g, int theOffsetY)
		{
			TRect zenButtonRect = this.GetZenButtonRect(GameObjectType.OBJECT_TYPE_WHEELBARROW);
			PottedPlant pottedPlantInWheelbarrow = this.mApp.mZenGarden.GetPottedPlantInWheelbarrow();
			if (pottedPlantInWheelbarrow != null && this.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_PLANT_FROM_WHEEL_BARROW)
			{
				if (this.mChallenge.mChallengeState == ChallengeState.STATECHALLENGE_ZEN_FADING)
				{
					g.DrawImage(AtlasResources.IMAGE_ZEN_WHEELBARROW, zenButtonRect.mX + Constants.ZenGardenButton_Wheelbarrow_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Wheelbarrow_Offset.Y + theOffsetY);
				}
				else
				{
					g.DrawImage(AtlasResources.IMAGE_ZEN_WHEELBARROW, zenButtonRect.mX + Constants.ZenGardenButton_Wheelbarrow_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Wheelbarrow_Offset.Y + theOffsetY);
				}
				if (pottedPlantInWheelbarrow.mPlantAge != PottedPlantAge.PLANTAGE_SMALL)
				{
					PottedPlantAge mPlantAge = pottedPlantInWheelbarrow.mPlantAge;
				}
				this.mApp.mZenGarden.DrawPottedPlant(g, (float)(zenButtonRect.mX + Constants.ZenGardenButton_WheelbarrowPlant_Offset.X), (float)(zenButtonRect.mY + Constants.ZenGardenButton_WheelbarrowPlant_Offset.Y + theOffsetY), pottedPlantInWheelbarrow, 0.6f, true);
				return;
			}
			g.DrawImage(AtlasResources.IMAGE_ZEN_WHEELBARROW, zenButtonRect.mX + Constants.ZenGardenButton_Wheelbarrow_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Wheelbarrow_Offset.Y + theOffsetY);
		}

		public void DrawZenButtons(Graphics g)
		{
			int num = 0;
			if (this.mChallenge.mChallengeState == ChallengeState.STATECHALLENGE_ZEN_FADING)
			{
				num = TodCommon.TodAnimateCurve(50, 0, this.mChallenge.mChallengeStateCounter, 0, -72, TodCurves.CURVE_EASE_IN_OUT);
			}
			for (int i = 6; i <= 15; i++)
			{
				GameObjectType gameObjectType = (GameObjectType)i;
				if (this.CanUseGameObject(gameObjectType))
				{
					TRect zenButtonRect = this.GetZenButtonRect(gameObjectType);
					if (gameObjectType != GameObjectType.OBJECT_TYPE_NEXT_GARDEN && (this.mApp.mPlayerInfo.mZenGardenTutorialComplete || gameObjectType != GameObjectType.OBJECT_TYPE_MONEY_SIGN))
					{
						g.DrawImage(AtlasResources.IMAGE_SHOVELBANK_ZEN, zenButtonRect.mX, zenButtonRect.mY + num);
						if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_WATERING_CAN + i - 6)
						{
							goto IL_63E;
						}
					}
					if (gameObjectType == GameObjectType.OBJECT_TYPE_WATERING_CAN)
					{
						if (this.mApp.mPlayerInfo.mPurchases[13] != 0)
						{
							g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1_GOLD, zenButtonRect.mX + Constants.ZenGardenButton_GoldenWateringCan_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_GoldenWateringCan_Offset.Y + num);
						}
						else
						{
							g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1, zenButtonRect.mX + Constants.ZenGardenButton_NormalWateringCan_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_NormalWateringCan_Offset.Y + num);
						}
					}
					else if (gameObjectType == GameObjectType.OBJECT_TYPE_FERTILIZER)
					{
						int num2 = this.mApp.mPlayerInfo.mPurchases[14] - 1000;
						if (num2 == 0)
						{
							g.SetColorizeImages(true);
							g.SetColor(new SexyColor(96, 96, 96));
						}
						else if (this.mTutorialState == TutorialState.TUTORIAL_ZEN_GARDEN_FERTILIZE_PLANTS)
						{
							SexyColor flashingColor = TodCommon.GetFlashingColor(this.mMainCounter, 75);
							g.SetColorizeImages(true);
							g.SetColor(flashingColor);
						}
						g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_FERTILIZER_BAG2, zenButtonRect.mX + Constants.ZenGardenButton_Fertiliser_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Fertiliser_Offset.Y + num);
						g.SetColorizeImages(false);
						string text;
						if (!Board.cachedChargesStringsFertilizer.TryGetValue(num2, ref text))
						{
							text = "x" + num2.ToString();
							Board.cachedChargesStringsFertilizer.Add(num2, text);
						}
						TodCommon.TodDrawString(g, text, zenButtonRect.mX + Constants.ZenGardenButtonCounterOffset.X, zenButtonRect.mY + Constants.ZenGardenButtonCounterOffset.Y + num, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.DS_ALIGN_RIGHT, 0.6f);
					}
					else if (gameObjectType == GameObjectType.OBJECT_TYPE_BUG_SPRAY)
					{
						int num3 = this.mApp.mPlayerInfo.mPurchases[15] - 1000;
						if (num3 == 0)
						{
							g.SetColorizeImages(true);
							g.SetColor(new SexyColor(128, 128, 128));
						}
						g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_BUGSPRAY_BOTTLE, zenButtonRect.mX + Constants.ZenGardenButton_BugSpray_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_BugSpray_Offset.Y + num);
						g.SetColorizeImages(false);
						string text2;
						if (!Board.cachedChargesStringsBugSpray.TryGetValue(num3, ref text2))
						{
							text2 = Common.StrFormat_(TodStringFile.TodStringTranslate("[BUG_SPRAY_MULTIPLIED_X]"), num3);
							Board.cachedChargesStringsBugSpray.Add(num3, text2);
						}
						TodCommon.TodDrawString(g, text2, zenButtonRect.mX + Constants.ZenGardenButtonCounterOffset.X, zenButtonRect.mY + Constants.ZenGardenButtonCounterOffset.Y + num, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.DS_ALIGN_RIGHT, 0.6f);
					}
					else if (gameObjectType == GameObjectType.OBJECT_TYPE_PHONOGRAPH)
					{
						g.DrawImage(AtlasResources.IMAGE_PHONOGRAPH, zenButtonRect.mX + Constants.ZenGardenButton_Phonograph_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Phonograph_Offset.Y + num);
					}
					else if (gameObjectType == GameObjectType.OBJECT_TYPE_CHOCOLATE)
					{
						int num4 = this.mApp.mPlayerInfo.mPurchases[26] - 1000;
						if (num4 == 0)
						{
							g.SetColorizeImages(true);
							g.SetColor(new SexyColor(128, 128, 128));
						}
						g.DrawImage(AtlasResources.IMAGE_CHOCOLATE, zenButtonRect.mX + Constants.ZenGardenButton_Chocolate_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Chocolate_Offset.Y + num);
						g.SetColorizeImages(false);
						string text3;
						if (!Board.cachedChargesStringsChocolate.TryGetValue(num4, ref text3))
						{
							text3 = Common.StrFormat_(TodStringFile.TodStringTranslate("[CHOCOLATE_MULTIPLIED_X]"), num4);
							Board.cachedChargesStringsChocolate.Add(num4, text3);
						}
						TodCommon.TodDrawString(g, text3, zenButtonRect.mX + Constants.ZenGardenButtonCounterOffset.X, zenButtonRect.mY + Constants.ZenGardenButtonCounterOffset.Y + num, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.DS_ALIGN_RIGHT, 0.6f);
					}
					else if (gameObjectType == GameObjectType.OBJECT_TYPE_GLOVE && this.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_PLANT_FROM_GLOVE && this.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_PLANT_FROM_WHEEL_BARROW)
					{
						g.DrawImage(AtlasResources.IMAGE_ZEN_GARDENGLOVE, zenButtonRect.mX + Constants.ZenGardenButton_Glove_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Glove_Offset.Y + num);
					}
					else if (gameObjectType == GameObjectType.OBJECT_TYPE_MONEY_SIGN && this.mApp.mPlayerInfo.mZenGardenTutorialComplete)
					{
						g.DrawImage(AtlasResources.IMAGE_ZEN_MONEYSIGN, zenButtonRect.mX + Constants.ZenGardenButton_MoneySign_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_MoneySign_Offset.Y + num);
					}
					else if (gameObjectType == GameObjectType.OBJECT_TYPE_WHEELBARROW)
					{
						this.DrawZenWheelBarrowButton(g, num);
					}
					else if (gameObjectType == GameObjectType.OBJECT_TYPE_NEXT_GARDEN)
					{
						if (!this.mMenuButton.mBtnNoDraw)
						{
							g.DrawImage(AtlasResources.IMAGE_ZEN_NEXT_GARDEN, zenButtonRect.mX + Constants.ZenGardenButton_NextGarden_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_NextGarden_Offset.Y + num);
						}
					}
					else if (gameObjectType == GameObjectType.OBJECT_TYPE_TREE_FOOD)
					{
						int num5 = this.mApp.mPlayerInfo.mPurchases[28] - 1000;
						if (num5 <= 0)
						{
							g.SetColorizeImages(true);
							g.SetColor(new SexyColor(96, 96, 96));
							num5 = 0;
						}
						if (!this.mChallenge.TreeOfWisdomCanFeed())
						{
							g.SetColorizeImages(true);
							g.SetColor(new SexyColor(96, 96, 96));
						}
						g.SetColorizeImages(false);
						string theText = Common.StrFormat_(TodStringFile.TodStringTranslate("[TREE_FOOD_MULTIPLIED_X]"), num5);
						TodCommon.TodDrawString(g, theText, zenButtonRect.mX + Constants.ZenGardenButtonCounterOffset.X, zenButtonRect.mY + Constants.ZenGardenButtonCounterOffset.Y + num, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.DS_ALIGN_RIGHT, 0.6f);
					}
				}
				IL_63E:;
			}
		}

		public void OffsetYForPlanting(ref int theY, SeedType theSeedType)
		{
			if (Plant.IsFlying(theSeedType) || theSeedType == SeedType.SEED_GRAVEBUSTER)
			{
				theY += 15;
			}
			if (theSeedType == SeedType.SEED_SPIKEWEED || theSeedType == SeedType.SEED_SPIKEROCK)
			{
				theY -= 15;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mBackground == BackgroundType.BACKGROUND_GREENHOUSE)
			{
				theY -= 25;
			}
		}

		public void SetFutureMode(bool theEnableFuture)
		{
			this.mApp.PlaySample(Resources.SOUND_BOING);
			this.mFutureMode = theEnableFuture;
			this.mApp.mFutureMode = theEnableFuture;
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead)
				{
					zombie.EnableFuture(theEnableFuture);
				}
			}
		}

		public void SetPinataMode(bool theEnablePinata)
		{
			this.mApp.PlayFoley(FoleyType.FOLEY_JUICY);
			this.mPinataMode = theEnablePinata;
			this.mApp.mPinataMode = theEnablePinata;
		}

		public void SetDanceMode(bool theEnableDance)
		{
			this.mApp.PlayFoley(FoleyType.FOLEY_DANCER);
			this.mDanceMode = theEnableDance;
			this.mApp.mDanceMode = theEnableDance;
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead)
				{
					zombie.EnableDanceMode(theEnableDance);
				}
			}
		}

		public void SetDaisyMode(bool theEnableDaisy)
		{
			this.mApp.PlaySample(Resources.SOUND_LOADINGBAR_FLOWER);
			this.mDaisyMode = theEnableDaisy;
			this.mApp.mDaisyMode = theEnableDaisy;
		}

		public void SetSukhbirMode(bool theEnableSukhbir)
		{
			this.mSukhbirMode = theEnableSukhbir;
			this.mApp.mSukhbirMode = theEnableSukhbir;
		}

		public bool MouseHitTestPlant(int x, int y, out HitResult theHitResult, bool posScaled)
		{
			theHitResult = default(HitResult);
			if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_COBCANNON_TARGET || this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_HAMMER)
			{
				return false;
			}
			if (!posScaled)
			{
				x = (int)((float)x * Constants.IS);
				y = (int)((float)y * Constants.IS);
			}
			Plant plant = this.SpecialPlantHitTest(x, y);
			if (plant != null)
			{
				theHitResult.mObject = plant;
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_PLANT;
				return true;
			}
			int theGridX = this.PixelToGridX(x, y);
			int theGridY = this.PixelToGridY(x, y);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				plant = this.GetTopPlantAt(theGridX, theGridY, PlantPriority.TOPPLANT_ZEN_TOOL_ORDER);
				if (this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_WATERING_CAN && (plant == null || !this.mApp.mZenGarden.PlantCanBeWatered(plant)))
				{
					int theGridX2 = this.PixelToGridX(x - 30, y - 20);
					int theGridY2 = this.PixelToGridY(x - 30, y - 20);
					Plant topPlantAt = this.GetTopPlantAt(theGridX2, theGridY2, PlantPriority.TOPPLANT_ZEN_TOOL_ORDER);
					if (topPlantAt != null && this.mApp.mZenGarden.PlantCanBeWatered(topPlantAt))
					{
						plant = topPlantAt;
					}
				}
			}
			else
			{
				plant = this.GetTopPlantAt(theGridX, theGridY, PlantPriority.TOPPLANT_DIGGING_ORDER);
				if (plant != null && (plant.mSeedType == SeedType.SEED_LILYPAD || plant.mSeedType == SeedType.SEED_FLOWERPOT) && this.GetTopPlantAt(theGridX, theGridY, PlantPriority.TOPPLANT_ONLY_PUMPKIN) != null)
				{
					plant = null;
				}
			}
			if (plant != null && this.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_CHOCOLATE && !this.mApp.mZenGarden.PlantCanHaveChocolate(plant))
			{
				theHitResult.mObject = null;
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_NONE;
				return false;
			}
			if (plant != null)
			{
				theHitResult.mObject = plant;
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_PLANT;
				return true;
			}
			return false;
		}

		public void DoTypingCheck(KeyCode theKey)
		{
			if (this.mApp.mKonamiCheck.Check(theKey))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				return;
			}
			if (this.mApp.mMustacheCheck.Check(theKey) || this.mApp.mMoustacheCheck.Check(theKey))
			{
				this.SetMustacheMode(!this.mMustacheMode);
				return;
			}
			if (this.mApp.mSuperMowerCheck.Check(theKey) || this.mApp.mSuperMowerCheck2.Check(theKey))
			{
				this.SetSuperMowerMode(!this.mSuperMowerMode);
				return;
			}
			if (this.mApp.mFutureCheck.Check(theKey))
			{
				this.SetFutureMode(!this.mFutureMode);
				return;
			}
			if (this.mApp.mPinataCheck.Check(theKey))
			{
				if (this.mApp.CanDoPinataMode())
				{
					this.SetPinataMode(!this.mPinataMode);
					return;
				}
				if (this.mApp.mGameScene == GameScenes.SCENE_PLAYING)
				{
					this.DisplayAdvice("[CANT_USE_CODE]", MessageStyle.MESSAGE_STYLE_BIG_MIDDLE_FAST, AdviceType.ADVICE_NONE);
				}
				this.mApp.PlaySample(Resources.SOUND_BUZZER);
				return;
			}
			else if (this.mApp.mDanceCheck.Check(theKey))
			{
				if (this.mApp.CanDoDanceMode())
				{
					this.SetDanceMode(!this.mDanceMode);
					return;
				}
				if (this.mApp.mGameScene == GameScenes.SCENE_PLAYING)
				{
					this.DisplayAdvice("[CANT_USE_CODE]", MessageStyle.MESSAGE_STYLE_BIG_MIDDLE_FAST, AdviceType.ADVICE_NONE);
				}
				this.mApp.PlaySample(Resources.SOUND_BUZZER);
				return;
			}
			else
			{
				if (!this.mApp.mDaisyCheck.Check(theKey))
				{
					if (this.mApp.mSukhbirCheck.Check(theKey))
					{
						this.SetSukhbirMode(!this.mSukhbirMode);
					}
					return;
				}
				if (this.mApp.CanDoDaisyMode())
				{
					this.SetDaisyMode(!this.mDaisyMode);
					return;
				}
				if (this.mApp.mGameScene == GameScenes.SCENE_PLAYING)
				{
					this.DisplayAdvice("[CANT_USE_CODE]", MessageStyle.MESSAGE_STYLE_BIG_MIDDLE_FAST, AdviceType.ADVICE_NONE);
				}
				this.mApp.PlaySample(Resources.SOUND_BUZZER);
				return;
			}
		}

		public void CompleteEndLevelSequenceForSaving()
		{
			if (this.CanDropLoot())
			{
				LawnMower lawnMower = null;
				while (this.IterateLawnMowers(ref lawnMower))
				{
					if (lawnMower.mMowerState != LawnMowerState.MOWER_TRIGGERED && lawnMower.mMowerState != LawnMowerState.MOWER_SQUISHED)
					{
						int coinValue = Coin.GetCoinValue(CoinType.COIN_GOLD);
						this.mApp.mPlayerInfo.AddCoins(coinValue);
						this.mCoinsCollected += coinValue;
					}
				}
			}
			Coin coin = null;
			while (this.IterateCoins(ref coin))
			{
				if (coin.mIsBeingCollected)
				{
					coin.ScoreCoin();
				}
				else
				{
					coin.Die();
				}
			}
			this.CheckForPostGameAchievements();
			this.mApp.UpdatePlayerProfileForFinishingLevel();
		}

		public void RemoveZombiesForRepick()
		{
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && !zombie.IsDeadOrDying() && zombie.mMindControlled && zombie.mPosX > 720f)
				{
					zombie.DieNoLoot(false);
				}
			}
		}

		public bool IsSurvivalStageWithRepick()
		{
			return this.mApp.IsSurvivalMode() && !this.IsFinalSurvivalStage();
		}

		public bool IsLastStandStageWithRepick()
		{
			return this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && !this.IsLastStandFinalStage();
		}

		public bool GrantAchievement(AchievementId theAchievement)
		{
			return this.GrantAchievement(theAchievement, true);
		}

		public bool GrantAchievement(AchievementId theAchievement, bool show)
		{
			return ReportAchievement.GiveAchievement(theAchievement);
		}

		public bool CheckForPostGameAchievements()
		{
			if (this.mApp.IsWhackAZombieLevel() || this.mApp.IsScaryPotterLevel() || this.mApp.IsWallnutBowlingLevel())
			{
				return false;
			}
			bool flag = false;
			if (this.mApp.IsAdventureMode() && this.mLevel == 50 && this.mApp.mPlayerInfo.mFinishedAdventure < 1)
			{
				flag = (this.GrantAchievement(AchievementId.ACHIEVEMENT_HOME_SECURITY, false) || flag);
			}
			return flag;
		}

		public int GetLiveGargantuarCount()
		{
			int num = 0;
			int count = this.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mZombies[i];
				if (!zombie.mDead && zombie.mHasHead && !zombie.IsDeadOrDying() && zombie.IsOnBoard() && (zombie.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || zombie.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR))
				{
					num++;
				}
			}
			return num;
		}

		public void ButtonMouseMove(int id, int x, int y)
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

		public void ButtonDepress(int id)
		{
		}

		public override bool BackButtonPress()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mApp.mBoardResult = BoardResult.BOARDRESULT_QUIT;
				this.TryToSaveGame();
				this.mApp.DoBackToMain();
				return true;
			}
			if (!this.CanInteractWithBoardButtons())
			{
				return true;
			}
			this.RefreshSeedPacketFromCursor();
			this.mApp.DoNewOptions(false);
			return true;
		}

		public bool SaveToFile(Sexy.Buffer b)
		{
			LeaderBoardComm.RecordResult(LeaderboardGameMode.Adventure, (int)this.mApp.mPlayerInfo.mZombiesKilled);
			try
			{
				b.WriteLong(this.mLevel);
				b.WriteLong((int)this.mApp.mGameScene);
				b.WriteLong(Board.mPeashootersPlanted);
				b.WriteBoolean(this.mNomNomNomAchievementTracker);
				b.WriteBoolean(this.mNoFungusAmongUsAchievementTracker);
				b.WriteLong((int)this.mBackground);
				b.WriteLong(this.mBoardFadeOutCounter);
				b.WriteLong(this.mBoardRandSeed);
				b.WriteLong(this.mBonusLawnMowersRemaining);
				b.WriteBoolean(this.mCatapultPlantsUsed);
				this.mChallenge.SaveToFile(b);
				b.WriteLong(this.mChocolateCollected);
				b.WriteLong(this.mCobCannonCursorDelayCounter);
				b.WriteLong(this.mCobCannonMouseX);
				b.WriteLong(this.mCobCannonMouseY);
				b.WriteLong(this.mCoinBankFadeCount);
				b.WriteLong(this.mCoins.Count);
				for (int i = 0; i < this.mCoins.Count; i++)
				{
					this.mCoins[i].SaveToFile(b);
				}
				b.WriteLong(this.mCoinsCollected);
				b.WriteLong(this.mCollectedCoinStreak);
				b.WriteLong(this.mCurrentWave);
				this.mCursorObject.SaveToFile(b);
				b.WriteBoolean(this.mDaisyMode);
				b.WriteBoolean(this.mDanceMode);
				b.WriteLong(this.mDiamondsCollected);
				b.WriteLong(this.mDoomsUsed);
				b.WriteBoolean(this.mDroppedFirstCoin);
				b.WriteLong(this.mEffectCounter);
				b.WriteBoolean(this.mEnableGraveStones);
				b.WriteBoolean(this.mFinalBossKilled);
				b.WriteLong(this.mFinalWaveSoundCounter);
				b.WriteLong(this.mFlagRaiseCounter);
				b.WriteLong(this.mFogBlownCountDown);
				b.WriteFloat(this.mFogOffset);
				b.WriteLong(this.mFwooshCountDown);
				b.WriteLong(this.mGameID);
				b.WriteLong(this.mGargantuarsKillsByCornCob);
				b.WriteLong(this.mGravesCleared);
				b.WriteLong2DArray(this.mGridCelFog);
				b.WriteLong2DArray(this.mGridCelLook);
				b.WriteLong3DArray(this.mGridCelOffset);
				b.WriteLong(this.mGridItems.Count);
				for (int j = 0; j < this.mGridItems.Count; j++)
				{
					this.mGridItems[j].SaveToFile(b);
				}
				int length = this.mGridSquareType.GetLength(0);
				int length2 = this.mGridSquareType.GetLength(1);
				b.WriteLong(length);
				b.WriteLong(length2);
				for (int k = 0; k < length; k++)
				{
					for (int l = 0; l < length2; l++)
					{
						b.WriteLong((int)this.mGridSquareType[k, l]);
					}
				}
				b.WriteBooleanArray(this.mHelpDisplayed);
				b.WriteLong((int)this.mHelpIndex);
				b.WriteLong(this.mHugeWaveCountDown);
				b.WriteLongArray(this.mIceMinX);
				b.WriteLongArray(this.mIceTimer);
				b.WriteLong(this.mIceTrapCounter);
				b.WriteBoolean(this.mIgnoreMouseUp);
				b.WriteBoolean(this.mKilledYeti);
				b.WriteLong(this.mLastBungeeWave);
				b.WriteLong(this.mLastToolX);
				b.WriteLong(this.mLastToolY);
				b.WriteLong(this.mLastWMUpdateCount);
				b.WriteLong(this.mLawnMowers.Count);
				for (int m = 0; m < this.mLawnMowers.Count; m++)
				{
					this.mLawnMowers[m].SaveToFile(b);
				}
				b.WriteBoolean(this.mLevelAwardSpawned);
				b.WriteBoolean(this.mLevelComplete);
				b.WriteLong(this.mLevelFadeCount);
				b.WriteString(this.mLevelStr);
				b.WriteLong(this.mMainCounter);
				b.WriteLong(this.mMaxSunPlants);
				b.WriteFloat(this.mMinFPS);
				b.WriteBoolean(this.mMushroomAndCoffeeBeansOnly);
				b.WriteBoolean(this.mMushroomsUsed);
				b.WriteLong(this.mNextSurvivalStageCounter);
				b.WriteLong(this.mNumSunsFallen);
				b.WriteLong(this.mNumWaves);
				b.WriteBoolean(this.mNutsUsed);
				b.WriteLong(this.mOutOfMoneyCounter);
				b.WriteBoolean(this.mPeaShooterUsed);
				b.WriteBoolean(this.mPinataMode);
				b.WriteBoolean(this.mPlanternOrBloverUsed);
				b.WriteLong(this.mPlants.Count);
				for (int n = 0; n < this.mPlants.Count; n++)
				{
					this.mPlants[n].SaveToFile(b);
				}
				b.WriteLong(this.mPlantsEaten);
				b.WriteLong(this.mPlantsShoveled);
				b.WriteLong(this.mPlayTimeActiveLevel);
				b.WriteLong(this.mPlayTimeInactiveLevel);
				b.WriteLong(this.mPottedPlantsCollected);
				b.WriteLong(this.mProgressMeterWidth);
				b.WriteLong(this.mProjectiles.Count);
				for (int num = 0; num < this.mProjectiles.Count; num++)
				{
					this.mProjectiles[num].SaveToFile(b);
				}
				b.WriteLong(this.mRiseFromGraveCounter);
				b.WriteLong(this.mRowPickingArray.Length);
				for (int num2 = 0; num2 < this.mRowPickingArray.Length; num2++)
				{
					this.mRowPickingArray[num2].SaveToFile(b);
				}
				b.WriteLong(this.mScoreNextMowerCounter);
				b.WriteBoolean(this.mShowShovel);
				this.mSeedBank.SaveToFile(b);
				b.WriteLong(this.mSodPosition);
				b.WriteLong(this.mSpecialGraveStoneX);
				b.WriteLong(this.mSpecialGraveStoneY);
				b.WriteLong(this.mSunCountDown);
				b.WriteLong(this.mSunMoney);
				b.WriteBoolean(this.mSuperMowerMode);
				b.WriteLong(this.mTimeStopCounter);
				b.WriteLong(this.mTotalSpawnedWaves);
				b.WriteLong(this.mTriggeredLawnMowers);
				b.WriteLong((int)this.mTutorialState);
				b.WriteLong(this.mTutorialTimer);
				b.WriteLongArray(this.mWaveRowGotLawnMowered);
				b.WriteBooleanArray(this.mZombieAllowed);
				b.WriteLong(this.mZombieCountDown);
				b.WriteLong(this.mZombieCountDownStart);
				b.WriteLong(this.mZombieHealthToNextWave);
				b.WriteLong(this.mZombieHealthWaveStart);
				b.WriteLong(this.mZombies.Count);
				for (int num3 = 0; num3 < this.mZombies.Count; num3++)
				{
					this.mZombies[num3].SaveToFile(b);
				}
				length = this.mZombiesInWave.GetLength(0);
				length2 = this.mZombiesInWave.GetLength(1);
				b.WriteLong(length);
				b.WriteLong(length2);
				for (int num4 = 0; num4 < length; num4++)
				{
					for (int num5 = 0; num5 < length2; num5++)
					{
						b.WriteLong((int)this.mZombiesInWave[num4, num5]);
					}
				}
				b.WriteLong(777);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return false;
			}
			return true;
		}

		public const int TUTORIAL_LEVEL_LIMIT = 3;

		public const int TRIALMODE_LEVEL_LIMIT = 7;

		private const int MAX_POOL_GRID_SIZE = 10;

		public const int FIRST_MINIGAME_UNLOCK_LEVEL = 22;

		public const int PUZZLE_UNLOCK_LEVEL = 36;

		private const int SAVE_CHECK_NUMBER = 777;

		private RenderItem[] aRenderList = new RenderItem[2048];

		public LawnApp mApp;

		public List<Zombie> mZombies = new List<Zombie>();

		public List<Plant> mPlants = new List<Plant>();

		public List<Projectile> mProjectiles = new List<Projectile>();

		public List<Coin> mCoins = new List<Coin>();

		public List<LawnMower> mLawnMowers = new List<LawnMower>();

		public List<GridItem> mGridItems = new List<GridItem>();

		public CursorObject mCursorObject;

		public CursorPreview mCursorPreview;

		public MessageWidget mAdvice;

		public SeedBank mSeedBank;

		public GameButton mMenuButton;

		public GameButton mStoreButton;

		public bool mIgnoreMouseUp;

		public bool mIgnoreNextMouseUp;

		public bool mIgnoreNextMouseUpSeedPacket;

		public int mLastToolX;

		public int mLastToolY;

		public CutScene mCutScene;

		public Challenge mChallenge;

		public static bool gShownMoreSunTutorial = false;

		private static bool needToSortRenderList = true;

		public List<Zombie> mZombiesRow1 = new List<Zombie>();

		public List<Zombie> mZombiesRow2 = new List<Zombie>();

		public List<Zombie> mZombiesRow3 = new List<Zombie>();

		public List<Zombie> mZombiesRow4 = new List<Zombie>();

		public List<Zombie> mZombiesRow5 = new List<Zombie>();

		public List<Zombie> mZombiesRow6 = new List<Zombie>();

		public bool mPaused;

		public GridSquareType[,] mGridSquareType = new GridSquareType[Constants.GRIDSIZEX, Constants.MAX_GRIDSIZEY];

		public int[,] mGridCelLook = new int[Constants.GRIDSIZEX, Constants.MAX_GRIDSIZEY];

		public int[,,] mGridCelOffset = new int[Constants.GRIDSIZEX, Constants.MAX_GRIDSIZEY, 2];

		public int[,] mGridCelFog = new int[Constants.GRIDSIZEX, 7];

		public bool mEnableGraveStones;

		public int mSpecialGraveStoneX;

		public int mSpecialGraveStoneY;

		public float mFogOffset;

		public int mFogBlownCountDown;

		public PlantRowType[] mPlantRow = new PlantRowType[Constants.MAX_GRIDSIZEY];

		public int[] mWaveRowGotLawnMowered = new int[Constants.MAX_GRIDSIZEY];

		public int mBonusLawnMowersRemaining;

		public int[] mIceMinX = new int[Constants.MAX_GRIDSIZEY];

		public int[] mIceTimer = new int[Constants.MAX_GRIDSIZEY];

		public ParticleSystemID[] mIceParticleID = new ParticleSystemID[Constants.MAX_GRIDSIZEY];

		public TodSmoothArray[] mRowPickingArray = new TodSmoothArray[Constants.MAX_GRIDSIZEY];

		public ZombieType[,] mZombiesInWave = new ZombieType[100, 50];

		public bool[] mZombieAllowed = new bool[100];

		public int mSunCountDown;

		public int mNumSunsFallen;

		public int mShakeCounter;

		public int mShakeAmountX;

		public int mShakeAmountY;

		public BackgroundType mBackground;

		public int mLevel;

		public int mSodPosition;

		public int mPrevMouseX;

		public int mPrevMouseY;

		public int mSunMoney;

		public int mNumWaves;

		public int mMainCounter;

		public int mEffectCounter;

		public int mDrawCount;

		public int mRiseFromGraveCounter;

		public int mOutOfMoneyCounter;

		public int mCurrentWave;

		public int mTotalSpawnedWaves;

		public TutorialState mTutorialState;

		public TodParticleSystem mTutorialParticleID;

		public int mTutorialTimer;

		public int mLastBungeeWave;

		public int mZombieHealthToNextWave;

		public int mZombieHealthWaveStart;

		public int mZombieCountDown;

		public int mZombieCountDownStart;

		public int mHugeWaveCountDown;

		public bool[] mHelpDisplayed = new bool[67];

		public AdviceType mHelpIndex;

		public bool mFinalBossKilled;

		public bool mShowShovel;

		public int mCoinBankFadeCount;

		public int mLevelFadeCount;

		public DebugTextMode mDebugTextMode;

		public bool mLevelComplete;

		public int mBoardFadeOutCounter;

		public int mNextSurvivalStageCounter;

		public int mScoreNextMowerCounter;

		public bool mLevelAwardSpawned;

		public int mProgressMeterWidth;

		public int mFlagRaiseCounter;

		public int mIceTrapCounter;

		public int mBoardRandSeed;

		public TodParticleSystem mPoolSparklyParticleID;

		public Reanimation[,] mFwooshID = new Reanimation[Constants.MAX_GRIDSIZEY, 12];

		public int mFwooshCountDown;

		public int mTimeStopCounter;

		public bool mDroppedFirstCoin;

		public int mFinalWaveSoundCounter;

		public int mCobCannonCursorDelayCounter;

		public int mCobCannonMouseX;

		public int mCobCannonMouseY;

		public bool mKilledYeti;

		public bool mMustacheMode;

		public bool mSuperMowerMode;

		public bool mFutureMode;

		public bool mPinataMode;

		public bool mDanceMode;

		public bool mDaisyMode;

		public bool mSukhbirMode;

		public BoardResult mPrevBoardResult;

		public int mTriggeredLawnMowers;

		public int mPlayTimeActiveLevel;

		public int mPlayTimeInactiveLevel;

		public int mMaxSunPlants;

		public int mStartDrawTime;

		public int mIntervalDrawTime;

		public int mIntervalDrawCountStart;

		public float mMinFPS;

		public int mPreloadTime;

		public int mGameID;

		public int mGravesCleared;

		public int mPlantsEaten;

		public int mPlantsShoveled;

		public int mCoinsCollected;

		public int mDiamondsCollected;

		public int mPottedPlantsCollected;

		public int mChocolateCollected;

		public bool mPeaShooterUsed;

		public bool mCatapultPlantsUsed;

		public int mCollectedCoinStreak;

		public int mGargantuarsKillsByCornCob;

		public bool mMushroomAndCoffeeBeansOnly;

		public bool mMushroomsUsed;

		public int mDoomsUsed;

		public bool mPlanternOrBloverUsed;

		public bool mNutsUsed;

		public bool mNomNomNomAchievementTracker;

		public bool mNoFungusAmongUsAchievementTracker;

		private static int mPeashootersPlanted;

		private static TodWeightedArray[] aZombieWeightArray = new TodWeightedArray[33];

		private static TodWeightedArray[] aPickArray = new TodWeightedArray[Constants.MAX_GRIDSIZEY];

		private static TodWeightedGridArray[] aGridArray = new TodWeightedGridArray[10];

		private static int MAX_GRAVE_STONES = Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY;

		private static GridItem[] aPicks = new GridItem[Board.MAX_GRAVE_STONES];

		private string progressMeterString;

		private int progressMeterStringValue = int.MinValue;

		private bool doAddGraveStones = true;

		public string mLevelStr;

		private int levelStrVal = -1;

		private static TPoint[] mCelPoints = new TPoint[4];

		private static Dictionary<int, string> cachedChargesStringsFertilizer = new Dictionary<int, string>();

		private static Dictionary<int, string> cachedChargesStringsBugSpray = new Dictionary<int, string>();

		private static Dictionary<int, string> cachedChargesStringsChocolate = new Dictionary<int, string>();
	}
}
