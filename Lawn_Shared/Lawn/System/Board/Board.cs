using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class Board : Widget, ButtonListener
    {
        public bool LoadFromFile(Sexy.Buffer b)
        {
            this.doAddGraveStones = false;
            this.mLevel = b.ReadLong();
            if (this.mApp.mGameMode == GameMode.Adventure && this.mLevel != this.mApp.mPlayerInfo.mLevel)
            {
                throw new Exception("Board Level does not match player level.");
            }
            mApp.mGameScene = (GameScenes)b.ReadLong();
            Board.mPeashootersPlanted = b.ReadLong();
            mNomNomNomAchievementTracker = b.ReadBoolean();
            mNoFungusAmongUsAchievementTracker = b.ReadBoolean();
            mBackground = (BackgroundType)b.ReadLong();
            mBoardFadeOutCounter = b.ReadLong();
            mBoardRandSeed = b.ReadLong();
            mBonusLawnMowersRemaining = b.ReadLong();
            mCatapultPlantsUsed = b.ReadBoolean();
            mChallenge.LoadFromFile(b);
            mChocolateCollected = b.ReadLong();
            mCobCannonCursorDelayCounter = b.ReadLong();
            mCobCannonMouseX = b.ReadLong();
            mCobCannonMouseY = b.ReadLong();
            mCoinBankFadeCount = b.ReadLong();
            int num = b.ReadLong();
            mCoins.Clear();
            for (int i = 0; i < num; i++)
            {
                Coin coin = new Coin();
                coin.LoadFromFile(b);
                mCoins.Add(coin);
            }
            mCoinsCollected = b.ReadLong();
            mCollectedCoinStreak = b.ReadLong();
            mCurrentWave = b.ReadLong();
            mCursorObject.LoadFromFile(b);
            mDaisyMode = b.ReadBoolean();
            mDanceMode = b.ReadBoolean();
            mDiamondsCollected = b.ReadLong();
            mDoomsUsed = b.ReadLong();
            mDroppedFirstCoin = b.ReadBoolean();
            mEffectCounter = b.ReadLong();
            mEnableGraveStones = b.ReadBoolean();
            mFinalBossKilled = b.ReadBoolean();
            mFinalWaveSoundCounter = b.ReadLong();
            mFlagRaiseCounter = b.ReadLong();
            mFogBlownCountDown = b.ReadLong();
            mFogOffset = b.ReadFloat();
            mFwooshCountDown = b.ReadLong();
            mGameID = b.ReadLong();
            mGargantuarsKillsByCornCob = b.ReadLong();
            mGravesCleared = b.ReadLong();
            mGridCelFog = b.ReadLong2DArray();
            mGridCelLook = b.ReadLong2DArray();
            mGridCelOffset = b.ReadLong3DArray();
            int num2 = b.ReadLong();
            mGridItems.Clear();
            for (int j = 0; j < num2; j++)
            {
                GridItem newGridItem = GridItem.GetNewGridItem();
                newGridItem.LoadFromFile(b);
                mGridItems.Add(newGridItem);
            }
            int num3 = b.ReadLong();
            int num4 = b.ReadLong();
            for (int k = 0; k < num3; k++)
            {
                for (int l = 0; l < num4; l++)
                {
                    mGridSquareType[k, l] = (GridSquareType)b.ReadLong();
                }
            }
            mHelpDisplayed = b.ReadBooleanArray();
            mHelpIndex = (AdviceType)b.ReadLong();
            mHugeWaveCountDown = b.ReadLong();
            mIceMinX = b.ReadLongArray();
            mIceTimer = b.ReadLongArray();
            mIceTrapCounter = b.ReadLong();
            mIgnoreMouseUp = b.ReadBoolean();
            mKilledYeti = b.ReadBoolean();
            mLastBungeeWave = b.ReadLong();
            mLastToolX = b.ReadLong();
            mLastToolY = b.ReadLong();
            mLastWMUpdateCount = b.ReadLong();
            int num5 = b.ReadLong();
            mLawnMowers.Clear();
            for (int m = 0; m < num5; m++)
            {
                LawnMower newLawnMower = LawnMower.GetNewLawnMower();
                newLawnMower.LoadFromFile(b);
                mLawnMowers.Add(newLawnMower);
            }
            mLevelAwardSpawned = b.ReadBoolean();
            mLevelComplete = b.ReadBoolean();
            mLevelFadeCount = b.ReadLong();
            mLevelStr = b.ReadString();
            mMainCounter = b.ReadLong();
            mMaxSunPlants = b.ReadLong();
            mMinFPS = b.ReadFloat();
            mMushroomAndCoffeeBeansOnly = b.ReadBoolean();
            mMushroomsUsed = b.ReadBoolean();
            mNextSurvivalStageCounter = b.ReadLong();
            mNumSunsFallen = b.ReadLong();
            mNumWaves = b.ReadLong();
            mNutsUsed = b.ReadBoolean();
            mOutOfMoneyCounter = b.ReadLong();
            mPeaShooterUsed = b.ReadBoolean();
            mPinataMode = b.ReadBoolean();
            mPlanternOrBloverUsed = b.ReadBoolean();
            PickBackground();
            int num6 = b.ReadLong();
            mPlants.Clear();
            for (int n = 0; n < num6; n++)
            {
                Plant newPlant = Plant.GetNewPlant();
                newPlant.LoadFromFile(b);
                if (mApp.IsIZombieLevel())
                {
                    mChallenge.IZombieSetupPlant(newPlant);
                }
                mPlants.Add(newPlant);
            }
            mPlantsEaten = b.ReadLong();
            mPlantsShoveled = b.ReadLong();
            mPlayTimeActiveLevel = b.ReadLong();
            mPlayTimeInactiveLevel = b.ReadLong();
            mPottedPlantsCollected = b.ReadLong();
            mProgressMeterWidth = b.ReadLong();
            int num7 = b.ReadLong();
            mProjectiles.Clear();
            for (int num8 = 0; num8 < num7; num8++)
            {
                Projectile newProjectile = Projectile.GetNewProjectile();
                newProjectile.LoadFromFile(b);
                mProjectiles.Add(newProjectile);
            }
            mRiseFromGraveCounter = b.ReadLong();
            int num9 = b.ReadLong();
            for (int num10 = 0; num10 < num9; num10++)
            {
                if (mRowPickingArray[num10] == null)
                {
                    mRowPickingArray[num10] = new TodSmoothArray();
                }
                mRowPickingArray[num10].LoadFromFile(b);
            }
            mScoreNextMowerCounter = b.ReadLong();
            mShowShovel = b.ReadBoolean();
            mSeedBank = new SeedBank();
            mSeedBank.LoadFromFile(b);
            mSodPosition = b.ReadLong();
            mSpecialGraveStoneX = b.ReadLong();
            mSpecialGraveStoneY = b.ReadLong();
            mSunCountDown = b.ReadLong();
            mSunMoney = b.ReadLong();
            mSuperMowerMode = b.ReadBoolean();
            mTimeStopCounter = b.ReadLong();
            mTotalSpawnedWaves = b.ReadLong();
            mTriggeredLawnMowers = b.ReadLong();
            mTutorialState = (TutorialState)b.ReadLong();
            mTutorialTimer = b.ReadLong();
            mWaveRowGotLawnMowered = b.ReadLongArray();
            mZombieAllowed = b.ReadBooleanArray();
            mZombieCountDown = b.ReadLong();
            mZombieCountDownStart = b.ReadLong();
            mZombieHealthToNextWave = b.ReadLong();
            mZombieHealthWaveStart = b.ReadLong();
            int num11 = b.ReadLong();
            mZombies.Clear();
            mZombiesRow1.Clear();
            mZombiesRow2.Clear();
            mZombiesRow3.Clear();
            mZombiesRow4.Clear();
            mZombiesRow5.Clear();
            mZombiesRow6.Clear();
            for (int num12 = 0; num12 < num11; num12++)
            {
                Zombie newZombie = Zombie.GetNewZombie();
                newZombie.LoadFromFile(b);
                AddToZombieList(newZombie);
            }
            num3 = b.ReadLong();
            num4 = b.ReadLong();
            for (int num13 = 0; num13 < num3; num13++)
            {
                for (int num14 = 0; num14 < num4; num14++)
                {
                    mZombiesInWave[num13, num14] = (ZombieType)b.ReadLong();
                }
            }
            if (b.ReadLong() != 777)
            {
                throw new Exception("Check number mismatch while loading.");
            }
            foreach (Zombie zombie in mZombies)
            {
                zombie.LoadingComplete();
            }
            foreach (Plant plant in mPlants)
            {
                plant.LoadingComplete();
            }
            foreach (Projectile projectile in mProjectiles)
            {
                projectile.LoadingComplete();
            }
            foreach (LawnMower lawnMower in mLawnMowers)
            {
                lawnMower.LoadingComplete();
            }
            foreach (Coin coin2 in mCoins)
            {
                coin2.LoadingComplete();
            }
            foreach (GridItem gridItem in mGridItems)
            {
                gridItem.LoadingComplete();
            }
            doAddGraveStones = true;
            mApp.mSoundManager.StopAllSounds();
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
            SetFullRect();
        }

        private void SetFullRect()
        {
            FullRect = new TRect(-Constants.Board_Offset_AspectRatio_Correction, mY, mWidth, mHeight);
        }

        public override void Move(int theNewX, int theNewY)
        {
            base.Move(theNewX, theNewY);
            SetFullRect();
        }

        public override void Resize(int theX, int theY, int theWidth, int theHeight)
        {
            base.Resize(theX, theY, theWidth, theHeight);
            SetFullRect();
        }

        public override void Resize(TRect theRect)
        {
            base.Resize(theRect);
            SetFullRect();
        }

        protected override void Reset()
        {
            base.Reset();
            mX = Constants.Board_Offset_AspectRatio_Correction;
            SetFullRect();
        }

        public Board(LawnApp theApp)
        {
            SetupRenderItems();
            mApp = theApp;
            mApp.mBoard = this;
            mZombies = new List<Zombie>(512);
            mPlants = new List<Plant>(512);
            mProjectiles = new List<Projectile>(512);
            mCoins = new List<Coin>(512);
            mLawnMowers = new List<LawnMower>(32);
            mGridItems = new List<GridItem>(128);
            mApp.mEffectSystem.EffectSystemFreeAll();
            mBoardRandSeed = mApp.mAppRandSeed;
            if (mApp.IsSurvivalMode())
            {
                mBoardRandSeed = RandomNumbers.NextNumber();
            }
            mCoinBankFadeCount = 0;
            mLevelFadeCount = 0;
            mLevel = 0;
            mCursorObject = new CursorObject();
            mCursorPreview = new CursorPreview();
            mSeedBank = new SeedBank();
            mCutScene = new CutScene();
            mSpecialGraveStoneX = -1;
            mSpecialGraveStoneY = -1;
            for (int i = 0; i < Constants.GRIDSIZEX; i++)
            {
                for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
                {
                    mGridSquareType[i, j] = GridSquareType.Grass;
                    mGridCelLook[i, j] = RandomNumbers.NextNumber(20);
                    mGridCelOffset[i, j, 0] = RandomNumbers.NextNumber(10) - 5;
                    mGridCelOffset[i, j, 1] = RandomNumbers.NextNumber(10) - 5;
                }
                for (int k = 0; k < 7; k++)
                {
                    mGridCelFog[i, k] = 0;
                }
            }
            mSunCountDown = 0;
            mShakeCounter = 0;
            mShakeAmountX = 0;
            mShakeAmountY = 0;
            mPaused = false;
            mLevelAwardSpawned = false;
            mFlagRaiseCounter = 0;
            mIceTrapCounter = 0;
            mLevelComplete = false;
            mBoardFadeOutCounter = -1;
            mNextSurvivalStageCounter = 0;
            mScoreNextMowerCounter = 0;
            mProgressMeterWidth = 0;
            mPoolSparklyParticleID = null;
            mFogBlownCountDown = 0;
            mFogOffset = 0f;
            mFwooshCountDown = 0;
            mTimeStopCounter = 0;
            mCobCannonCursorDelayCounter = 0;
            mCobCannonMouseX = 0;
            mCobCannonMouseY = 0;
            mDroppedFirstCoin = false;
            mBonusLawnMowersRemaining = 0;
            mEnableGraveStones = false;
            mHelpIndex = AdviceType.None;
            mEffectCounter = 0;
            mDrawCount = 0;
            mRiseFromGraveCounter = 0;
            mFinalWaveSoundCounter = 0;
            mKilledYeti = false;
            mTriggeredLawnMowers = 0;
            mPlayTimeActiveLevel = 0;
            mPlayTimeInactiveLevel = 0;
            mMaxSunPlants = 0;
            mStartDrawTime = 0;
            mIntervalDrawTime = 0;
            mIntervalDrawCountStart = 0;
            mPreloadTime = 0;
            mGameID = DateTime.Now.Millisecond;
            mGravesCleared = 0;
            mPlantsEaten = 0;
            mPlantsShoveled = 0;
            mCoinsCollected = 0;
            mDiamondsCollected = 0;
            mPottedPlantsCollected = 0;
            mChocolateCollected = 0;
            mCollectedCoinStreak = 0;
            mGargantuarsKillsByCornCob = 0;
            mMushroomsUsed = false;
            mDoomsUsed = 0;
            mPlanternOrBloverUsed = false;
            mNutsUsed = false;
            mLastToolX = (mLastToolY = short.MinValue);
            mMinFPS = 1000f;
            for (int l = 0; l < Constants.MAX_GRIDSIZEY; l++)
            {
                for (int m = 0; m < 12; m++)
                {
                    mFwooshID[l, m] = null;
                }
            }
            mPrevMouseX = -1;
            mPrevMouseY = -1;
            mFinalBossKilled = false;
            mMustacheMode = mApp.mMustacheMode;
            mSuperMowerMode = mApp.mSuperMowerMode;
            mFutureMode = mApp.mFutureMode;
            mPinataMode = mApp.mPinataMode;
            mDanceMode = mApp.mDanceMode;
            mDaisyMode = mApp.mDaisyMode;
            mSukhbirMode = mApp.mSukhbirMode;
            mShowShovel = false;
            mAdvice = new MessageWidget(mApp);
            mBackground = BackgroundType.Num1Day;
            mMainCounter = 0;
            mTutorialState = TutorialState.Off;
            mTutorialTimer = -1;
            mTutorialParticleID = null;
            mChallenge = new Challenge();
            mClip = false;
            mDebugTextMode = DebugTextMode.None;
            mMenuButton = new GameButton(0, this);
            mMenuButton.mDrawStoneButton = true;
            mStoreButton = null;
            mIgnoreMouseUp = false;
            if (mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom)
            {
                mMenuButton.SetLabel("[MAIN_MENU_BUTTON]");
                mMenuButton.Resize(Constants.UIMenuButtonPosition.X, Constants.UIMenuButtonPosition.Y, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
                mStoreButton = new GameButton(1, this);
                mStoreButton.mButtonImage = AtlasResources.IMAGE_ZENSHOPBUTTON;
                mStoreButton.mOverImage = AtlasResources.IMAGE_ZENSHOPBUTTON_HIGHLIGHT;
                mStoreButton.mDownImage = AtlasResources.IMAGE_ZENSHOPBUTTON_HIGHLIGHT;
                mStoreButton.mParentWidget = this;
                mStoreButton.Resize(Constants.ZenGardenStoreButtonX, Constants.ZenGardenStoreButtonY, AtlasResources.IMAGE_ZENSHOPBUTTON.mWidth, AtlasResources.IMAGE_ZENSHOPBUTTON.mHeight);
            }
            else
            {
                mMenuButton.SetLabel("[MENU_BUTTON]");
                mMenuButton.Resize(Constants.UIMenuButtonPosition.X, Constants.UIMenuButtonPosition.Y, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
            }
            if (mApp.mGameMode == GameMode.ChallengeLastStand)
            {
                mStoreButton = new GameButton(1, this);
                mStoreButton.mDrawStoneButton = true;
                mStoreButton.mBtnNoDraw = true;
                mStoreButton.mDisabled = true;
            }
            if (mApp.mGameMode == GameMode.Upsell)
            {
                mMenuButton.SetLabel("[MAIN_MENU_BUTTON]");
                mMenuButton.Resize(Constants.UIMenuButtonPosition.X, 2, Constants.UIMenuButtonWidth, AtlasResources.IMAGE_BUTTON_LEFT.mHeight);
                mStoreButton = new GameButton(1, this);
                mStoreButton.mDrawStoneButton = true;
                mStoreButton.mBtnNoDraw = true;
                mStoreButton.Resize(mStoreButton.mX, mStoreButton.mY, mStoreButton.mWidth + 200, mStoreButton.mHeight);
                mStoreButton.SetLabel("[GET_FULL_VERSION_BUTTON]");
            }
        }

        public override void Dispose()
        {
            mAdvice.Dispose();
            mMenuButton.Dispose();
            if (mStoreButton != null)
            {
                mStoreButton.Dispose();
            }
            mZombiesRow1.Clear();
            mZombiesRow2.Clear();
            mZombiesRow3.Clear();
            mZombiesRow4.Clear();
            mZombiesRow5.Clear();
            mZombiesRow6.Clear();
            for (int i = 0; i < mZombies.Count; i++)
            {
                mZombies[i].PrepareForReuse();
            }
            mZombies.Clear();
            for (int j = 0; j < mPlants.Count; j++)
            {
                mPlants[j].PrepareForReuse();
            }
            mPlants.Clear();
            for (int k = 0; k < mProjectiles.Count; k++)
            {
                mProjectiles[k].PrepareForReuse();
            }
            mProjectiles.Clear();
            mCoins.Clear();
            for (int l = 0; l < mLawnMowers.Count; l++)
            {
                mLawnMowers[l].PrepareForReuse();
            }
            mLawnMowers.Clear();
            for (int m = 0; m < mGridItems.Count; m++)
            {
                mGridItems[m].PrepareForReuse();
            }
            mGridItems.Clear();
            for (int n = 0; n < aRenderList.Length; n++)
            {
                aRenderList[n].PrepareForReuse();
            }
            mCutScene.Dispose();
            mApp.DelayLoadBackgroundResource(string.Empty);
            RemoveAllWidgets();
        }

        public void DisposeBoard()
        {
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                mApp.mZenGarden.LeaveGarden();
            }
            if (mApp.mGameMode == GameMode.TreeOfWisdom)
            {
                mChallenge.TreeOfWisdomLeave();
            }
            mApp.mSoundSystem.StopFoley(FoleyType.Rain);
            mApp.mZenGarden.mBoard = null;
            mApp.CrazyDaveDie();
            mApp.mEffectSystem.EffectSystemFreeAll();
        }

        public static void BoardInitForPlayer()
        {
            GameConstants.gShownMoreSunTutorial = false;
        }

        public int CountSunBeingCollected()
        {
            int num = 0;
            Coin coin = null;
            while (IterateCoins(ref coin))
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
            bool flag = mChallenge.IsStormyNightPitchBlack();
            if (!flag)
            {
                int count = mPlants.Count;
                for (int i = 0; i < count; i++)
                {
                    Plant plant = mPlants[i];
                    if (!plant.mDead && plant.mOnBungeeState == PlantOnBungeeState.NotOnBungee && mX + plant.mX >= -32)
                    {
                        GlobalMembersBoard.AddGameObjectRenderItemPlant(aRenderList, ref num, RenderObjectType.Plant, plant);
                        if (mApp.mGameMode == GameMode.ChallengeZenGarden && plant.mPottedPlantIndex != -1)
                        {
                            RenderItem renderItem = aRenderList[num];
                            renderItem.mRenderObjectType = RenderObjectType.PlantOverlay;
                            renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.Particle, 0, plant.mY);
                            renderItem.mGameObject = plant;
                            renderItem.mPlant = plant;
                            renderItem.id = num;
                            num++;
                        }
                        if ((plant.mSeedType == SeedType.Magnetshroom || plant.mSeedType == SeedType.GoldMagnet) && plant.DrawMagnetItemsOnTop())
                        {
                            RenderItem renderItem2 = aRenderList[num];
                            renderItem2.mRenderObjectType = RenderObjectType.PlantMagnetItems;
                            renderItem2.mZPos = Board.MakeRenderOrder(RenderLayer.Top, 0, -1);
                            renderItem2.mGameObject = plant;
                            renderItem2.mPlant = plant;
                            renderItem2.id = num;
                            num++;
                        }
                    }
                }
            }
            Coin theGameObject = null;
            while (IterateCoins(ref theGameObject))
            {
                GlobalMembersBoard.AddGameObjectRenderItemCoin(aRenderList, ref num, RenderObjectType.Coin, theGameObject);
            }
            if (!flag)
            {
                int count2 = mZombies.Count;
                for (int j = 0; j < count2; j++)
                {
                    Zombie zombie = mZombies[j];
                    if (!zombie.mDead)
                    {
                        if (zombie.mZombieType == ZombieType.Boss)
                        {
                            AddBossRenderItem(aRenderList, ref num, zombie);
                        }
                        else
                        {
                            GlobalMembersBoard.AddGameObjectRenderItemZombie(aRenderList, ref num, RenderObjectType.Zombie, zombie);
                            if (zombie.HasShadow() && !GlobalStaticVars.gLowFramerate)
                            {
                                RenderItem renderItem3 = aRenderList[num];
                                renderItem3.mRenderObjectType = RenderObjectType.ZombieShadow;
                                renderItem3.mZPos = 200000 + 10000 * zombie.mRow + 3;
                                renderItem3.mGameObject = zombie;
                                renderItem3.mZombie = zombie;
                                renderItem3.id = num;
                                num++;
                            }
                            if (zombie.mZombieType == ZombieType.Bungee)
                            {
                                RenderItem renderItem4 = aRenderList[num];
                                renderItem4.mRenderObjectType = RenderObjectType.ZombieBungeeTarget;
                                renderItem4.mZPos = Board.MakeRenderOrder(RenderLayer.Projectile, zombie.mRow, 1);
                                renderItem4.mGameObject = zombie;
                                renderItem4.mZombie = zombie;
                                renderItem4.id = num;
                                num++;
                            }
                        }
                    }
                }
                int num2 = -1;
                Projectile projectile = null;
                while (IterateProjectiles(ref projectile, ref num2))
                {
                    GlobalMembersBoard.AddGameObjectRenderItemProjectile(aRenderList, ref num, RenderObjectType.Projectile, projectile);
                    if (!GlobalStaticVars.gLowFramerate)
                    {
                        RenderItem renderItem5 = aRenderList[num];
                        renderItem5.mRenderObjectType = RenderObjectType.ProjectileShadow;
                        renderItem5.mZPos = 305000 + 10000 * projectile.mRow + 3;
                        renderItem5.mGameObject = projectile;
                        renderItem5.mProjectile = projectile;
                        renderItem5.id = num;
                        num++;
                    }
                }
                LawnMower lawnMower = null;
                while (IterateLawnMowers(ref lawnMower))
                {
                    RenderItem renderItem6 = aRenderList[num];
                    renderItem6.mRenderObjectType = RenderObjectType.Mower;
                    renderItem6.mZPos = lawnMower.mRenderOrder;
                    renderItem6.mMower = lawnMower;
                    renderItem6.id = num;
                    num++;
                }
                int num3 = -1;
                TodParticleSystem todParticleSystem = null;
                while (IterateParticles(ref todParticleSystem, ref num3))
                {
                    if (!todParticleSystem.mIsAttachment)
                    {
                        RenderItem renderItem7 = aRenderList[num];
                        renderItem7.mRenderObjectType = RenderObjectType.Particle;
                        renderItem7.mZPos = todParticleSystem.mRenderOrder;
                        renderItem7.mParticleSytem = todParticleSystem;
                        renderItem7.id = num;
                        num++;
                    }
                }
                int num4 = -1;
                Reanimation reanimation = null;
                while (IterateReanimations(ref reanimation, ref num4))
                {
                    if (!reanimation.mIsAttachment)
                    {
                        RenderItem renderItem8 = aRenderList[num];
                        renderItem8.mRenderObjectType = RenderObjectType.Reanimation;
                        renderItem8.mZPos = reanimation.mRenderOrder;
                        renderItem8.mReanimation = reanimation;
                        renderItem8.id = num;
                        num++;
                    }
                }
                int num5 = -1;
                GridItem gridItem = null;
                while (IterateGridItems(ref gridItem, ref num5))
                {
                    RenderItem renderItem9 = aRenderList[num];
                    renderItem9.mRenderObjectType = RenderObjectType.GridItem;
                    renderItem9.mZPos = gridItem.mRenderOrder;
                    renderItem9.mGridItem = gridItem;
                    renderItem9.id = num;
                    num++;
                    if (mApp.mGameMode == GameMode.ChallengeZenGarden && gridItem.mGridItemType == GridItemType.Stinky)
                    {
                        RenderItem renderItem10 = aRenderList[num];
                        renderItem10.mRenderObjectType = RenderObjectType.GridItemOverlay;
                        renderItem10.mZPos = Board.MakeRenderOrder(RenderLayer.Particle, 0, (int)gridItem.mPosY - 30);
                        renderItem10.mGridItem = gridItem;
                        num++;
                    }
                }
            }
            for (int k = 0; k < Constants.MAX_GRIDSIZEY; k++)
            {
                if (mIceTimer[k] > 0)
                {
                    RenderItem renderItem11 = aRenderList[num];
                    renderItem11.mRenderObjectType = RenderObjectType.Ice;
                    renderItem11.mBoardGridY = k;
                    renderItem11.mZPos = GetIceZPos(k);
                    renderItem11.id = num;
                    num++;
                }
            }
            int thePosZ;
            if (mTimeStopCounter > 0)
            {
                thePosZ = 800000;
            }
            else if (mApp.mGameScene == GameScenes.Playing || mApp.mGameScene == GameScenes.ZombiesWon || mCutScene.IsAfterSeedChooser() || mCutScene.IsInShovelTutorial() || mHelpIndex == AdviceType.ClickToContinue)
            {
                thePosZ = 100001;
            }
            else
            {
                thePosZ = 800000;
            }
            GlobalMembersBoard.AddUIRenderItem(aRenderList, ref num, RenderObjectType.Backdrop, 100000);
            GlobalMembersBoard.AddUIRenderItem(aRenderList, ref num, RenderObjectType.BottomUi, thePosZ);
            GlobalMembersBoard.AddUIRenderItem(aRenderList, ref num, RenderObjectType.CoinBank, 600000);
            GlobalMembersBoard.AddUIRenderItem(aRenderList, ref num, RenderObjectType.TopUi, 700000);
            GlobalMembersBoard.AddUIRenderItem(aRenderList, ref num, RenderObjectType.ScreenFade, 900000);
            if (mApp.mGameScene == GameScenes.ZombiesWon)
            {
                int thePosZ2 = Board.MakeRenderOrder(RenderLayer.GraveStone, 3, 2);
                GlobalMembersBoard.AddUIRenderItem(aRenderList, ref num, RenderObjectType.DoorMask, thePosZ2);
            }
            if (StageHasFog())
            {
                GlobalMembersBoard.AddUIRenderItem(aRenderList, ref num, RenderObjectType.Fog, 500000);
            }
            if (mApp.IsStormyNightLevel() || mApp.mGameMode == GameMode.ChallengeRainingSeeds)
            {
                GlobalMembersBoard.AddUIRenderItem(aRenderList, ref num, RenderObjectType.Storm, 500003);
            }
            GlobalMembersBoard.AddGameObjectRenderItemCursorPreview(aRenderList, ref num, RenderObjectType.CursorPreview, mCursorPreview);
            if (Board.needToSortRenderList)
            {
                Array.Sort<RenderItem>(aRenderList, 0, num, null);
            }
            SortZombieRowLists();
            for (int l = 0; l < num; l++)
            {
                RenderItem renderItem12 = aRenderList[l];
                if (renderItem12.mRenderObjectType != 0)
                {
                    RenderObjectType aRenderObjectType = renderItem12.mRenderObjectType;
                    if (aRenderObjectType != RenderObjectType.Ice)
                    {
                        if (aRenderObjectType != RenderObjectType.Backdrop)
                        {
                            if (aRenderObjectType == RenderObjectType.GridItem && renderItem12.mGridItem.mGridItemType == GridItemType.Gravestone)
                            {
                                renderItem12.mGridItem.DrawGridItem(g);
                            }
                        }
                        else if (!flag)
                        {
                            DrawBackdrop(g);
                            DrawCursorOnBackground(g);
                        }
                    }
                    else
                    {
                        DrawIce(g, renderItem12.mBoardGridY);
                    }
                }
            }
            for (int m = 0; m < num; m++)
            {
                RenderItem renderItem13 = aRenderList[m];
                if (renderItem13.mRenderObjectType != 0)
                {
                    switch (renderItem13.mRenderObjectType)
                    {
                    case RenderObjectType.Coin:
                        if (renderItem13.mCoin.BeginDraw(g))
                        {
                            renderItem13.mCoin.Draw(g);
                            renderItem13.mCoin.EndDraw(g);
                        }
                        break;
                    case RenderObjectType.Projectile:
                        if (renderItem13.mProjectile.BeginDraw(g))
                        {
                            renderItem13.mProjectile.Draw(g);
                            renderItem13.mProjectile.EndDraw(g);
                        }
                        break;
                    case RenderObjectType.Zombie:
                        if (renderItem13.mZombie.BeginDraw(g))
                        {
                            renderItem13.mZombie.Draw(g);
                            renderItem13.mZombie.EndDraw(g);
                        }
                        break;
                    case RenderObjectType.ZombieShadow:
                        if (renderItem13.mZombie.BeginDraw(g))
                        {
                            renderItem13.mZombie.DrawShadow(g);
                            renderItem13.mZombie.EndDraw(g);
                        }
                        break;
                    case RenderObjectType.ZombieBungeeTarget:
                        renderItem13.mZombie.DrawBungeeTarget(g);
                        break;
                    case RenderObjectType.Plant:
                        if (renderItem13.mPlant.BeginDraw(g))
                        {
                            renderItem13.mPlant.Draw(g);
                            renderItem13.mPlant.EndDraw(g);
                        }
                        break;
                    case RenderObjectType.PlantOverlay:
                        if (renderItem13.mPlant.BeginDraw(g))
                        {
                            mApp.mZenGarden.DrawPlantOverlay(g, renderItem13.mPlant);
                            renderItem13.mPlant.EndDraw(g);
                        }
                        break;
                    case RenderObjectType.PlantMagnetItems:
                        if (renderItem13.mPlant.BeginDraw(g))
                        {
                            renderItem13.mPlant.DrawMagnetItems(g);
                            renderItem13.mPlant.EndDraw(g);
                        }
                        break;
                    case RenderObjectType.CursorPreview:
                        if (renderItem13.mCursorPreview.BeginDraw(g))
                        {
                            renderItem13.mCursorPreview.Draw(g);
                            renderItem13.mCursorPreview.EndDraw(g);
                        }
                        break;
                    case RenderObjectType.Particle:
                        renderItem13.mParticleSytem.Draw(g, true);
                        break;
                    case RenderObjectType.Reanimation:
                        renderItem13.mReanimation.Draw(g);
                        break;
                    case RenderObjectType.TopUi:
                        DrawUITop(g);
                        if (flag)
                        {
                            DrawCursorOnBackground(g);
                        }
                        DrawCursorOverlay(g);
                        break;
                    case RenderObjectType.Fog:
                        DrawFog(g);
                        break;
                    case RenderObjectType.Storm:
                        mChallenge.DrawWeather(g);
                        break;
                    case RenderObjectType.BottomUi:
                        DrawUIBottom(g);
                        break;
                    case RenderObjectType.DoorMask:
                        DrawHouseDoorTop(g);
                        break;
                    case RenderObjectType.CoinBank:
                        DrawUICoinBank(g);
                        break;
                    case RenderObjectType.ProjectileShadow:
                        if (renderItem13.mProjectile.BeginDraw(g))
                        {
                            renderItem13.mProjectile.DrawShadow(g);
                            renderItem13.mProjectile.EndDraw(g);
                        }
                        break;
                    case RenderObjectType.Mower:
                        renderItem13.mMower.Draw(g);
                        break;
                    case RenderObjectType.ScreenFade:
                        DrawFadeOut(g);
                        break;
                    case RenderObjectType.BossPart:
                    {
                        Zombie bossZombie = GetBossZombie();
                        if (bossZombie != null && bossZombie.BeginDraw(g))
                        {
                            bossZombie.DrawBossPart(g, renderItem13.mBossPart);
                            bossZombie.EndDraw(g);
                        }
                        break;
                    }
                    case RenderObjectType.GridItem:
                        if (renderItem13.mGridItem.mGridItemType != GridItemType.Gravestone)
                        {
                            renderItem13.mGridItem.DrawGridItem(g);
                        }
                        break;
                    case RenderObjectType.GridItemOverlay:
                        renderItem13.mGridItem.DrawGridItemOverlay(g);
                        break;
                    }
                }
            }
        }

        public void ClearCursor()
        {
            if (mAdvice.mDuration > 0 && (mHelpIndex == AdviceType.PlantGravebustersOnGraves || mHelpIndex == AdviceType.PlantLilypadOnWater || mHelpIndex == AdviceType.PlantTanglekelpOnWater || mHelpIndex == AdviceType.PlantSeashroomOnWater || mHelpIndex == AdviceType.PlantPotatoMineOnLily || mHelpIndex == AdviceType.PlantWrongArtType || mHelpIndex == AdviceType.PlantNeedPot || mHelpIndex == AdviceType.PlantNotPassedLine || mHelpIndex == AdviceType.PlantOnlyOnRepeaters || mHelpIndex == AdviceType.PlantOnlyOnMelonpult || mHelpIndex == AdviceType.PlantOnlyOnSunflower || mHelpIndex == AdviceType.PlantOnlyOnKernelpult))
            {
                ClearAdvice(mHelpIndex);
            }
            if (mCursorObject.mCursorType == CursorType.PlantFromUsableCoin && mCursorObject.mCoinID != null)
            {
                mCursorObject.mCoinID.mIsBeingCollected = false;
            }
            mCursorObject.mType = SeedType.None;
            mCursorObject.mCursorType = CursorType.Normal;
            mCursorObject.mSeedBankIndex = -1;
            mCursorObject.mCoinID = null;
            mCursorObject.mDuplicatorPlantID = null;
            mCursorObject.mCobCannonPlantID = null;
            mCursorObject.mGlovePlantID = null;
            mChallenge.ClearCursor();
            if (mTutorialState == TutorialState.Level1PlantPeashooter)
            {
                SetTutorialState(TutorialState.Level1PickUpPeashooter);
                return;
            }
            if (mTutorialState == TutorialState.Level2PlantSunflower || mTutorialState == TutorialState.Level2RefreshSunflower)
            {
                if (!mSeedBank.mSeedPackets[1].CanPickUp())
                {
                    SetTutorialState(TutorialState.Level2RefreshSunflower);
                    return;
                }
                SetTutorialState(TutorialState.Level2PickUpSunflower);
                return;
            }
            else
            {
                if (mTutorialState != TutorialState.MoresunPlantSunflower && mTutorialState != TutorialState.MoresunRefreshSunflower)
                {
                    if (mTutorialState == TutorialState.ShovelDig)
                    {
                        SetTutorialState(TutorialState.ShovelPickup);
                    }
                    return;
                }
                if (!mSeedBank.mSeedPackets[1].CanPickUp())
                {
                    SetTutorialState(TutorialState.MoresunRefreshSunflower);
                    return;
                }
                SetTutorialState(TutorialState.MoresunPickUpSunflower);
                return;
            }
        }

        public bool AreEnemyZombiesOnScreen()
        {
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
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
            while (IterateLawnMowers(ref lawnMower))
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
            LoadBackgroundImages();
            ResetFPSStats();
            UpdateLayers();
            return true;
        }

        public void InitLevel()
        {
            mMainCounter = 0;
            mEnableGraveStones = false;
            mSodPosition = 0;
            mPrevBoardResult = mApp.mBoardResult;
            if (mApp.mGameMode != GameMode.TreeOfWisdom && mApp.mGameMode != GameMode.ChallengeZenGarden)
            {
                mApp.mMusic.StopAllMusic();
            }
            mNomNomNomAchievementTracker = true;
            mNoFungusAmongUsAchievementTracker = true;
            mPeaShooterUsed = false;
            mCatapultPlantsUsed = false;
            mMushroomAndCoffeeBeansOnly = true;
            if (mApp.IsAdventureMode())
            {
                mLevel = mApp.mPlayerInfo.GetLevel();
            }
            else if (mApp.IsQuickPlayMode())
            {
                mLevel = mApp.mGameMode - GameMode.Quickplay1 + 1;
            }
            else
            {
                mLevel = 0;
            }
            mLevelStr = TodStringFile.TodStringTranslate("[LEVEL]") + " " + mApp.GetStageString(mLevel);
            PickBackground();
            mCurrentWave = 0;
            InitZombieWaves();
            if (mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist || mApp.IsScaryPotterLevel() || mApp.IsWhackAZombieLevel())
            {
                mSunMoney = 0;
            }
            else if (mApp.mGameMode == GameMode.ChallengeLastStand)
            {
                mSunMoney = 5000;
            }
            else if (mApp.IsIZombieLevel())
            {
                mSunMoney = 150;
            }
            else if (mApp.IsFirstTimeAdventureMode() && mLevel == 1)
            {
                mSunMoney = 150;
            }
            else
            {
                mSunMoney = 50;
            }
            for (int i = 0; i < mRowPickingArray.Length; i++)
            {
                mRowPickingArray[i] = new TodSmoothArray();
            }
            for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
            {
                mWaveRowGotLawnMowered[j] = -100;
                mIceMinX[j] = Constants.Board_Ice_Start;
                mIceTimer[j] = 0;
                mIceParticleID[j] = ParticleSystemID.Null;
                mRowPickingArray[j].mItem = j;
            }
            mNumSunsFallen = 0;
            if (!StageIsNight())
            {
                mSunCountDown = TodCommon.RandRangeInt(425, 700);
            }
            for (int k = 0; k < 67; k++)
            {
                mHelpDisplayed[k] = false;
            }
            mSeedBank.mNumPackets = GetNumSeedsInBank();
            mSeedBank.UpdateHeight();
            for (int l = 0; l < 9; l++)
            {
                SeedPacket seedPacket = mSeedBank.mSeedPackets[l];
                seedPacket.mIndex = l;
                seedPacket.mY = GetSeedPacketPositionY(l);
                seedPacket.mX = 0;
                seedPacket.mPacketType = SeedType.None;
            }
            if (mApp.IsSlotMachineLevel())
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 3);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.Sunflower, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.Peashooter, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.Snowpea, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.ChallengeIce)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 6);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.Peashooter, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.Cherrybomb, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.Wallnut, SeedType.None);
                mSeedBank.mSeedPackets[3].SetPacketType(SeedType.Repeater, SeedType.None);
                mSeedBank.mSeedPackets[4].SetPacketType(SeedType.Snowpea, SeedType.None);
                mSeedBank.mSeedPackets[5].SetPacketType(SeedType.Chomper, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie1)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 3);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieNormal, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombiePail, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombieFootball, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie2)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 3);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieNormal, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombieScreenDoor, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombiePail, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie3)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 3);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieNormal, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombiePail, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombieDigger, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie4)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 3);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieNormal, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombiePail, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombieLadder, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie5)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 4);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieNormal, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombiePail, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombieBungee, SeedType.None);
                mSeedBank.mSeedPackets[3].SetPacketType(SeedType.ZombieBalloon, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie6)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 4);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieNormal, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombiePolevaulter, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombiePail, SeedType.None);
                mSeedBank.mSeedPackets[3].SetPacketType(SeedType.ZombieGargantuar, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie7)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 4);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieNormal, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombiePolevaulter, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombiePail, SeedType.None);
                mSeedBank.mSeedPackets[3].SetPacketType(SeedType.ZombieDancer, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie8)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 6);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieImp, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombieTrafficCone, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombiePail, SeedType.None);
                mSeedBank.mSeedPackets[3].SetPacketType(SeedType.ZombieBungee, SeedType.None);
                mSeedBank.mSeedPackets[4].SetPacketType(SeedType.ZombieDigger, SeedType.None);
                mSeedBank.mSeedPackets[5].SetPacketType(SeedType.ZombieLadder, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie9)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 8);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieImp, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombieTrafficCone, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombiePolevaulter, SeedType.None);
                mSeedBank.mSeedPackets[3].SetPacketType(SeedType.ZombiePail, SeedType.None);
                mSeedBank.mSeedPackets[4].SetPacketType(SeedType.ZombieBungee, SeedType.None);
                mSeedBank.mSeedPackets[5].SetPacketType(SeedType.ZombieDigger, SeedType.None);
                mSeedBank.mSeedPackets[6].SetPacketType(SeedType.ZombieLadder, SeedType.None);
                mSeedBank.mSeedPackets[7].SetPacketType(SeedType.ZombieFootball, SeedType.None);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombieEndless)
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 9);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.ZombieImp, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.ZombieTrafficCone, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.ZombiePolevaulter, SeedType.None);
                mSeedBank.mSeedPackets[3].SetPacketType(SeedType.ZombiePail, SeedType.None);
                mSeedBank.mSeedPackets[4].SetPacketType(SeedType.ZombieBungee, SeedType.None);
                mSeedBank.mSeedPackets[5].SetPacketType(SeedType.ZombieDigger, SeedType.None);
                mSeedBank.mSeedPackets[6].SetPacketType(SeedType.ZombieLadder, SeedType.None);
                mSeedBank.mSeedPackets[7].SetPacketType(SeedType.ZombieFootball, SeedType.None);
                mSeedBank.mSeedPackets[8].SetPacketType(SeedType.ZombieDancer, SeedType.None);
            }
            else if (mApp.IsScaryPotterLevel())
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 1);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.Cherrybomb, SeedType.None);
            }
            else if (mApp.IsWhackAZombieLevel() && (mApp.IsAdventureMode() || mApp.IsQuickPlayMode()))
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 3);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.Potatomine, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.Gravebuster, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.Cherrybomb, SeedType.None);
            }
            else if (mApp.IsWhackAZombieLevel() && !mApp.IsAdventureMode() && !mApp.IsQuickPlayMode())
            {
                Debug.ASSERT(mSeedBank.mNumPackets == 3);
                mSeedBank.mSeedPackets[0].SetPacketType(SeedType.Potatomine, SeedType.None);
                mSeedBank.mSeedPackets[1].SetPacketType(SeedType.Gravebuster, SeedType.None);
                mSeedBank.mSeedPackets[2].SetPacketType(SeedType.Iceshroom, SeedType.None);
            }
            else if (!ChooseSeedsOnCurrentLevel() && !HasConveyorBeltSeedBank())
            {
                mSeedBank.mNumPackets = GetNumSeedsInBank();
                for (int m = 0; m < mSeedBank.mNumPackets; m++)
                {
                    SeedPacket seedPacket2 = mSeedBank.mSeedPackets[m];
                    seedPacket2.SetPacketType((SeedType)m, SeedType.None);
                }
            }
            mWidgetManager.MarkAllDirty();
            mPaused = false;
            mOutOfMoneyCounter = 0;
            if (StageHasFog())
            {
                mFogOffset = 1065f - LeftFogColumn() * 80f;
                mFogBlownCountDown = 200;
            }
            mChallenge.InitLevel();
            SetupRenderItems();
            Board.needToSortRenderList = true;
        }

        private void SetupRenderItems()
        {
            for (int i = 0; i < aRenderList.Length; i++)
            {
                if (aRenderList[i] != null)
                {
                    aRenderList[i].PrepareForReuse();
                }
                aRenderList[i] = RenderItem.GetNewRenderItem();
            }
        }

        public void DisplayAdvice(string theAdvice, MessageStyle theMessageStyle, AdviceType theHelpIndex)
        {
            DisplayAdvice(theAdvice, theMessageStyle, theHelpIndex, null);
        }

        public void DisplayAdvice(string theAdvice, MessageStyle theMessageStyle, AdviceType theHelpIndex, Image theIcon)
        {
            if (theHelpIndex != AdviceType.None)
            {
                if (mHelpDisplayed[(int)theHelpIndex])
                {
                    return;
                }
                mHelpDisplayed[(int)theHelpIndex] = true;
            }
            mAdvice.SetLabel(theAdvice, theMessageStyle, theIcon);
            mHelpIndex = theHelpIndex;
        }

        public void StartLevel()
        {
            mCoinBankFadeCount = 0;
            mLevelFadeCount = 1000;
            mApp.mLastLevelStats.Reset();
            mChallenge.StartLevel();
            if (mApp.IsSurvivalMode() && mChallenge.mSurvivalStage > 0)
            {
                string savedGameName = LawnCommon.GetSavedGameName(mApp.mGameMode, (int)mApp.mPlayerInfo.mId);
                mApp.EraseFile(savedGameName);
            }
            if (mApp.IsSurvivalMode() && mChallenge.mSurvivalStage > 0)
            {
                FreezeEffectsForCutscene(false);
                mApp.mSoundSystem.GamePause(false);
            }
            if (mApp.mGameMode == GameMode.ChallengeIce)
            {
                return;
            }
            if (mApp.mGameMode != GameMode.ChallengeZenGarden && mApp.mGameMode != GameMode.TreeOfWisdom && mApp.mGameMode != GameMode.Upsell && mApp.mGameMode != GameMode.Intro && mApp.mGameMode != GameMode.Intro)
            {
                if (mApp.IsFinalBossLevel())
                {
                    return;
                }
                mApp.mMusic.StartGameMusic();
            }
        }

        public Plant AddPlant(int theGridX, int theGridY, SeedType theSeedType, SeedType theImitaterType)
        {
            Plant plant = NewPlant(theGridX, theGridY, theSeedType, theImitaterType);
            DoPlantingEffects(theGridX, theGridY, plant, false);
            mChallenge.PlantAdded(plant);
            int num = CountPlantByType(SeedType.Sunflower) + CountPlantByType(SeedType.Sunshroom);
            if (num > mMaxSunPlants)
            {
                mMaxSunPlants = num;
            }
            SeedType seedType = theSeedType;
            if (seedType == SeedType.Imitater)
            {
                seedType = theImitaterType;
            }
            if (seedType == SeedType.Peashooter)
            {
                Board.mPeashootersPlanted++;
                if (Board.mPeashootersPlanted >= 10)
                {
                    if (SexyAppBase.IsInTrialMode)
                    {
                        if (!mApp.mPlayerInfo.mHasSeenAchievementDialog)
                        {
                            mApp.mPlayerInfo.mHasSeenAchievementDialog = true;
                            mApp.achievementToCheck = AchievementId.SoilYourPlants;
                            mApp.DoLockedAchievementDialog(AchievementId.SoilYourPlants);
                        }
                    }
                    else
                    {
                        GrantAchievement(AchievementId.SoilYourPlants);
                    }
                }
            }
            if (seedType != SeedType.Sunflower && seedType != SeedType.Wallnut && seedType != SeedType.Chomper)
            {
                mNomNomNomAchievementTracker = false;
            }
            if (IsFungus(seedType))
            {
                mNoFungusAmongUsAchievementTracker = false;
            }
            if (seedType == SeedType.Peashooter || seedType == SeedType.Snowpea || seedType == SeedType.Repeater || seedType == SeedType.Threepeater || seedType == SeedType.Splitpea || seedType == SeedType.Gatlingpea)
            {
                mPeaShooterUsed = true;
            }
            if (seedType == SeedType.Cabbagepult || seedType == SeedType.Kernelpult || seedType == SeedType.Melonpult || seedType == SeedType.Wintermelon)
            {
                mCatapultPlantsUsed = true;
            }
            bool flag = IsFungus(seedType);
            if (seedType != SeedType.InstantCoffee && !flag)
            {
                mMushroomAndCoffeeBeansOnly = false;
            }
            if (flag)
            {
                mMushroomsUsed = true;
            }
            return plant;
        }

        public bool IsFungus(SeedType aCheckSeed)
        {
            return aCheckSeed == SeedType.Puffshroom || aCheckSeed == SeedType.Sunshroom || aCheckSeed == SeedType.Fumeshroom || aCheckSeed == SeedType.Hypnoshroom || aCheckSeed == SeedType.Scaredyshroom || aCheckSeed == SeedType.Iceshroom || aCheckSeed == SeedType.Doomshroom || aCheckSeed == SeedType.Magnetshroom || aCheckSeed == SeedType.Seashroom || aCheckSeed == SeedType.Gloomshroom;
        }

        public Projectile AddProjectile(int theX, int theY, int aRenderOrder, int theRow, ProjectileType projectileType)
        {
            Projectile newProjectile = Projectile.GetNewProjectile();
            newProjectile.ProjectileInitialize(theX, theY, aRenderOrder, theRow, projectileType);
            mProjectiles.Add(newProjectile);
            return newProjectile;
        }

        public Coin AddCoin(int theX, int theY, CoinType theCoinType, CoinMotion theCoinMotion)
        {
            Coin coin = new Coin();
            coin.CoinInitialize(theX, theY, theCoinType, theCoinMotion);
            mCoins.Add(coin);
            if (mApp.IsFirstTimeAdventureMode() && mLevel == 1)
            {
                DisplayAdvice("[ADVICE_CLICK_ON_SUN]", MessageStyle.TutorialLevel1Stay, AdviceType.ClickOnSun);
            }
            return coin;
        }

        public void RefreshSeedPacketFromCursor()
        {
            if (mCursorObject.mCursorType == CursorType.PlantFromUsableCoin)
            {
                Coin coin = mCoins[mCoins.IndexOf(mCursorObject.mCoinID)];
                coin.DroppedUsableSeed();
            }
            else if (mCursorObject.mCursorType == CursorType.PlantFromBank)
            {
                Debug.ASSERT(mCursorObject.mSeedBankIndex >= 0 && mCursorObject.mSeedBankIndex < mSeedBank.mNumPackets);
                SeedPacket seedPacket = mSeedBank.mSeedPackets[mCursorObject.mSeedBankIndex];
                seedPacket.Activate();
            }
            ClearCursor();
        }

        public void DeselectSeedPacket()
        {
            if (mCursorObject.mSeedBankIndex == -1)
            {
                return;
            }
            SeedPacket seedPacket = mSeedBank.mSeedPackets[mCursorObject.mSeedBankIndex];
            seedPacket.Activate();
            ClearCursor();
        }

        public ZombieType PickGraveRisingZombieType(int theZombiePoints)
        {
            for (int i = 0; i < Board.aZombieWeightArray.Length; i++)
            {
                Board.aZombieWeightArray[i].Reset();
            }
            int num = 0;
            Board.aZombieWeightArray[num].mItem = 0;
            Board.aZombieWeightArray[num].mWeight = Zombie.GetZombieDefinition(ZombieType.Normal).mPickWeight;
            num++;
            Board.aZombieWeightArray[num].mItem = 2;
            Board.aZombieWeightArray[num].mWeight = Zombie.GetZombieDefinition(ZombieType.TrafficCone).mPickWeight;
            num++;
            if (!StageHasGraveStones())
            {
                Board.aZombieWeightArray[num].mItem = 4;
                Board.aZombieWeightArray[num].mWeight = Zombie.GetZombieDefinition(ZombieType.Pail).mPickWeight;
                num++;
            }
            for (int j = 0; j < num; j++)
            {
                ZombieType zombieType = (ZombieType)Board.aZombieWeightArray[j].mItem;
                ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(zombieType);
                if (mApp.IsFirstTimeAdventureMode() && mLevel < zombieDefinition.mStartingLevel)
                {
                    Board.aZombieWeightArray[j].mWeight = 0;
                }
                else if (!mZombieAllowed[(int)zombieType] && zombieType != ZombieType.Normal)
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
                if (mZombieAllowed[(int)zombieType])
                {
                    if (zombieType == ZombieType.Bungee && mApp.IsSurvivalEndless(mApp.mGameMode))
                    {
                        if (!IsFlagWave(theWaveIndex))
                        {
                            goto IL_1E4;
                        }
                    }
                    else if (mApp.mGameMode != GameMode.ChallengePogoParty && mApp.mGameMode != GameMode.ChallengeBobsledBonanza && mApp.mGameMode != GameMode.ChallengeAirRaid)
                    {
                        int num2 = zombieDefinition.mFirstAllowedWave;
                        if (mApp.IsSurvivalEndless(mApp.mGameMode))
                        {
                            int survivalFlagsCompleted = GetSurvivalFlagsCompleted();
                            int num3 = TodCommon.TodAnimateCurve(18, 50, survivalFlagsCompleted, 0, 15, TodCurves.Linear);
                            num2 = Math.Max(num2 - num3, 1);
                        }
                        if (theWaveIndex + 1 < num2 || theZombiePoints < zombieDefinition.mZombieValue)
                        {
                            goto IL_1E4;
                        }
                    }
                    int aPickWeight = zombieDefinition.mPickWeight;
                    if (mApp.IsSurvivalMode())
                    {
                        int survivalFlagsCompleted2 = GetSurvivalFlagsCompleted();
                        if (zombieType == ZombieType.Gargantuar || zombieType == ZombieType.Zamboni)
                        {
                            int num4 = TodCommon.TodAnimateCurve(10, 50, survivalFlagsCompleted2, 2, 50, TodCurves.Linear);
                            if (theZombiePicker.mZombieTypeCount[(int)zombieType] >= num4)
                            {
                                goto IL_1E4;
                            }
                        }
                        if (zombieType == ZombieType.RedeyeGargantuar)
                        {
                            if (IsFlagWave(theWaveIndex))
                            {
                                int num5 = TodCommon.TodAnimateCurve(14, 100, survivalFlagsCompleted2, 1, 50, TodCurves.Linear);
                                if (theZombiePicker.mZombieTypeCount[(int)zombieType] >= num5)
                                {
                                    goto IL_1E4;
                                }
                            }
                            else
                            {
                                int num6 = TodCommon.TodAnimateCurve(10, 110, survivalFlagsCompleted2, 1, 50, TodCurves.Linear);
                                if (theZombiePicker.mAllWavesZombieTypeCount[(int)zombieType] >= num6)
                                {
                                    goto IL_1E4;
                                }
                                aPickWeight = 1000;
                            }
                        }
                        if (zombieType == ZombieType.Normal)
                        {
                            aPickWeight = TodCommon.TodAnimateCurve(10, 50, survivalFlagsCompleted2, zombieDefinition.mPickWeight, zombieDefinition.mPickWeight / 10, TodCurves.Linear);
                        }
                        if (zombieType == ZombieType.TrafficCone)
                        {
                            aPickWeight = TodCommon.TodAnimateCurve(10, 50, survivalFlagsCompleted2, zombieDefinition.mPickWeight, zombieDefinition.mPickWeight / 4, TodCurves.Linear);
                        }
                    }
                    Board.aZombieWeightArray[num].mItem = i;
                    Board.aZombieWeightArray[num].mWeight = aPickWeight;
                    num++;
                }
                IL_1E4:;
            }
            return (ZombieType)TodCommon.TodPickFromWeightedArray(Board.aZombieWeightArray, num);
        }

        public int PickRowForNewZombie(ZombieType theZombieType)
        {
            if (theZombieType == ZombieType.Boss)
            {
                return 0;
            }
            GridItem rake = GetRake();
            if (rake != null && rake.mGridItemState == GridItemState.RakeAttracting && RowCanHaveZombieType(rake.mGridY, theZombieType))
            {
                rake.mGridItemState = GridItemState.RakeWaiting;
                TodCommon.TodUpdateSmoothArrayPick(mRowPickingArray, Constants.MAX_GRIDSIZEY, rake.mGridY);
                return rake.mGridY;
            }
            for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
            {
                if (!RowCanHaveZombieType(i, theZombieType))
                {
                    mRowPickingArray[i].mWeight = 0f;
                }
                else if (mApp.mGameMode == GameMode.ChallengePortalCombat)
                {
                    mRowPickingArray[i].mWeight = mChallenge.PortalCombatRowSpawnWeight(i);
                }
                else if (mApp.mGameMode == GameMode.ChallengeInvisighoul && mCurrentWave <= 3 && i == 5)
                {
                    mRowPickingArray[i].mWeight = 0f;
                }
                else
                {
                    int num = mCurrentWave - mWaveRowGotLawnMowered[i];
                    if (mApp.IsContinuousChallenge() && mCurrentWave == mNumWaves - 1)
                    {
                        num = 100;
                    }
                    if (num <= 1)
                    {
                        mRowPickingArray[i].mWeight = 0.01f;
                    }
                    else if (num <= 2)
                    {
                        mRowPickingArray[i].mWeight = 0.5f;
                    }
                    else
                    {
                        mRowPickingArray[i].mWeight = 1f;
                    }
                }
            }
            return TodCommon.TodPickFromSmoothArray(mRowPickingArray, Constants.MAX_GRIDSIZEY);
        }

        public Zombie AddZombie(ZombieType theZombieType, int theFromWave)
        {
            int theRow = PickRowForNewZombie(theZombieType);
            return AddZombieInRow(theZombieType, theRow, theFromWave);
        }

        public void SpawnZombieWave()
        {
            mChallenge.SpawnZombieWave();
            if (mApp.IsBungeeBlitzLevel())
            {
                BungeeDropGrid theBungeeDropGrid = new BungeeDropGrid();
                SetupBungeeDrop(theBungeeDropGrid);
                for (int i = 0; i < 50; i++)
                {
                    ZombieType zombieType = mZombiesInWave[mCurrentWave, i];
                    if (zombieType == ZombieType.Invalid)
                    {
                        break;
                    }
                    if (zombieType == ZombieType.Bungee || zombieType == ZombieType.Zamboni)
                    {
                        AddZombie(zombieType, mCurrentWave);
                    }
                    else
                    {
                        BungeeDropZombie(theBungeeDropGrid, zombieType);
                    }
                }
            }
            else
            {
                Debug.ASSERT(mCurrentWave >= 0 && mCurrentWave < 100 && mCurrentWave < mNumWaves);
                for (int j = 0; j < 50; j++)
                {
                    ZombieType zombieType2 = mZombiesInWave[mCurrentWave, j];
                    if (zombieType2 == ZombieType.Invalid)
                    {
                        break;
                    }
                    if (zombieType2 == ZombieType.Bobsled && !CanAddBobSled())
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            AddZombie(ZombieType.Normal, mCurrentWave);
                        }
                    }
                    else
                    {
                        AddZombie(zombieType2, mCurrentWave);
                    }
                }
            }
            if (mCurrentWave == mNumWaves - 1 && !mApp.IsContinuousChallenge())
            {
                mRiseFromGraveCounter = 210;
            }
            if (IsFlagWave(mCurrentWave))
            {
                mFlagRaiseCounter = 100;
            }
            mCurrentWave++;
            mTotalSpawnedWaves++;
        }

        public void RemoveAllZombies()
        {
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && !zombie.IsDeadOrDying())
                {
                    zombie.DieNoLoot(false);
                }
            }
        }

        public void RemoveCutsceneZombies()
        {
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && zombie.mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
                {
                    zombie.DieNoLoot(false);
                }
            }
        }

        public void SpawnZombiesFromGraves()
        {
            if (mApp.mGameMode == GameMode.ChallengeWarAndPeas || mApp.mGameMode == GameMode.ChallengeWarAndPeas2)
            {
                return;
            }
            if (StageHasRoof())
            {
                SpawnZombiesFromSky();
            }
            else if (StageHasPool())
            {
                SpawnZombiesFromPool();
                return;
            }
            int num = GetGraveStoneCount();
            int num2 = -1;
            GridItem gridItem = null;
            while (IterateGridItems(ref gridItem, ref num2))
            {
                if (gridItem.mGridItemType == GridItemType.Gravestone && gridItem.mGridItemCounter >= 100 && (mApp.mGameMode != GameMode.ChallengeGraveDanger || RandomNumbers.NextNumber(mNumWaves) <= mCurrentWave))
                {
                    ZombieType theZombieType = PickGraveRisingZombieType(num);
                    ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
                    Zombie zombie = AddZombie(theZombieType, mCurrentWave);
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
                return PlantingReason.NotHere;
            }
            PlantingReason plantingReason = mChallenge.CanPlantAt(theGridX, theGridY, theType);
            if (plantingReason != PlantingReason.Ok || Challenge.IsZombieSeedType(theType))
            {
                return plantingReason;
            }
            PlantsOnLawn plantsOnLawn = default(PlantsOnLawn);
            GetPlantsOnLawn(theGridX, theGridY, ref plantsOnLawn);
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                if (plantsOnLawn.mUnderPlant != null || plantsOnLawn.mNormalPlant != null || plantsOnLawn.mFlyingPlant != null || plantsOnLawn.mPumpkinPlant != null)
                {
                    return PlantingReason.NotHere;
                }
                if (mApp.mZenGarden.mGardenType == GardenType.Aquarium && !Plant.IsAquatic(theType))
                {
                    return PlantingReason.NotOnWater;
                }
                return PlantingReason.Ok;
            }
            else
            {
                bool flag = GetGraveStoneAt(theGridX, theGridY) != null;
                if (theType == SeedType.Gravebuster)
                {
                    if (plantsOnLawn.mNormalPlant != null)
                    {
                        return PlantingReason.NotHere;
                    }
                    if (flag)
                    {
                        return PlantingReason.Ok;
                    }
                    return PlantingReason.OnlyOnGraves;
                }
                else if (theType == SeedType.InstantCoffee)
                {
                    if (plantsOnLawn.mFlyingPlant != null)
                    {
                        return PlantingReason.NotHere;
                    }
                    if (plantsOnLawn.mNormalPlant == null || !plantsOnLawn.mNormalPlant.mIsAsleep || plantsOnLawn.mNormalPlant.mWakeUpCounter > 0 || plantsOnLawn.mNormalPlant.mOnBungeeState == PlantOnBungeeState.GettingGrabbedByBungee)
                    {
                        return PlantingReason.NeedsSleeping;
                    }
                    return PlantingReason.Ok;
                }
                else if (flag)
                {
                    if (Plant.IsFlying(theType))
                    {
                        return PlantingReason.Ok;
                    }
                    return PlantingReason.NotOnGrave;
                }
                else
                {
                    bool flag2 = plantsOnLawn.mUnderPlant != null && plantsOnLawn.mUnderPlant.mSeedType == SeedType.Lilypad && plantsOnLawn.mUnderPlant.mOnBungeeState != PlantOnBungeeState.GettingGrabbedByBungee;
                    bool flag3 = plantsOnLawn.mUnderPlant != null && plantsOnLawn.mUnderPlant.mSeedType == SeedType.Flowerpot && plantsOnLawn.mUnderPlant.mOnBungeeState != PlantOnBungeeState.GettingGrabbedByBungee;
                    bool flag4 = GetCraterAt(theGridX, theGridY) != null;
                    if (flag4)
                    {
                        if (Plant.IsFlying(theType))
                        {
                            return PlantingReason.Ok;
                        }
                        return PlantingReason.NotOnCrater;
                    }
                    else
                    {
                        bool flag5 = GetScaryPotAt(theGridX, theGridY) != null;
                        if (flag5)
                        {
                            return PlantingReason.NotHere;
                        }
                        if (IsIceAt(theGridX, theGridY))
                        {
                            return PlantingReason.NotHere;
                        }
                        if (mGridSquareType[theGridX, theGridY] == GridSquareType.Dirt || mGridSquareType[theGridX, theGridY] == GridSquareType.None)
                        {
                            return PlantingReason.NotHere;
                        }
                        if (theType == SeedType.Lilypad || theType == SeedType.Tanglekelp || theType == SeedType.Seashroom)
                        {
                            if (!IsPoolSquare(theGridX, theGridY))
                            {
                                return PlantingReason.OnlyInPool;
                            }
                            if (plantsOnLawn.mNormalPlant != null || plantsOnLawn.mUnderPlant != null)
                            {
                                return PlantingReason.NotHere;
                            }
                            return PlantingReason.Ok;
                        }
                        else if (Plant.IsFlying(theType))
                        {
                            if (plantsOnLawn.mFlyingPlant != null)
                            {
                                return PlantingReason.NotHere;
                            }
                            return PlantingReason.Ok;
                        }
                        else
                        {
                            if ((theType == SeedType.Spikeweed || theType == SeedType.Spikerock) && (mGridSquareType[theGridX, theGridY] == GridSquareType.Pool || StageHasRoof() || plantsOnLawn.mUnderPlant != null))
                            {
                                return PlantingReason.NeedsGround;
                            }
                            if (mGridSquareType[theGridX, theGridY] == GridSquareType.Pool && !flag2 && theType != SeedType.Cattail && (plantsOnLawn.mNormalPlant == null || plantsOnLawn.mNormalPlant.mSeedType != SeedType.Cattail || theType != SeedType.Pumpkinshell))
                            {
                                return PlantingReason.NotOnWater;
                            }
                            if (theType == SeedType.Flowerpot)
                            {
                                if (plantsOnLawn.mNormalPlant != null || plantsOnLawn.mUnderPlant != null || plantsOnLawn.mPumpkinPlant != null)
                                {
                                    return PlantingReason.NotHere;
                                }
                                return PlantingReason.Ok;
                            }
                            else
                            {
                                if (StageHasRoof() && !flag3)
                                {
                                    return PlantingReason.NeedsPot;
                                }
                                if (theType == SeedType.Pumpkinshell)
                                {
                                    if (plantsOnLawn.mNormalPlant != null && plantsOnLawn.mNormalPlant.mSeedType == SeedType.Cobcannon)
                                    {
                                        return PlantingReason.NotHere;
                                    }
                                    if (plantsOnLawn.mPumpkinPlant == null)
                                    {
                                        return PlantingReason.Ok;
                                    }
                                    if (mApp.mPlayerInfo.mPurchases[29] != 0 && plantsOnLawn.mPumpkinPlant.mPlantHealth < plantsOnLawn.mPumpkinPlant.mPlantMaxHealth * 2 / 3 && theType == plantsOnLawn.mPumpkinPlant.mSeedType && plantsOnLawn.mPumpkinPlant.mOnBungeeState != PlantOnBungeeState.GettingGrabbedByBungee)
                                    {
                                        return PlantingReason.Ok;
                                    }
                                    return PlantingReason.NotHere;
                                }
                                else
                                {
                                    if (flag2 && theType == SeedType.Potatomine)
                                    {
                                        return PlantingReason.OnlyOnGround;
                                    }
                                    if (plantsOnLawn.mUnderPlant != null && theType == SeedType.Cattail)
                                    {
                                        if (plantsOnLawn.mNormalPlant != null)
                                        {
                                            return PlantingReason.NotHere;
                                        }
                                        if (plantsOnLawn.mUnderPlant.IsUpgradableTo(theType) && plantsOnLawn.mUnderPlant.mOnBungeeState != PlantOnBungeeState.GettingGrabbedByBungee)
                                        {
                                            return PlantingReason.Ok;
                                        }
                                        if (Plant.IsUpgrade(theType))
                                        {
                                            return PlantingReason.NeedsUpgrade;
                                        }
                                    }
                                    if (plantsOnLawn.mUnderPlant != null && plantsOnLawn.mUnderPlant.mSeedType == SeedType.Imitater)
                                    {
                                        return PlantingReason.NotHere;
                                    }
                                    if (plantsOnLawn.mNormalPlant == null)
                                    {
                                        if (!mApp.mEasyPlantingCheat)
                                        {
                                            if (Plant.IsUpgrade(theType))
                                            {
                                                return PlantingReason.NeedsUpgrade;
                                            }
                                        }
                                        else
                                        {
                                            if (theType == SeedType.Cobcannon && !IsValidCobCannonSpot(theGridX, theGridY))
                                            {
                                                return PlantingReason.NeedsUpgrade;
                                            }
                                            if (theType == SeedType.Cattail && !IsPoolSquare(theGridX, theGridY))
                                            {
                                                return PlantingReason.NotHere;
                                            }
                                        }
                                        return PlantingReason.Ok;
                                    }
                                    if (plantsOnLawn.mNormalPlant.IsUpgradableTo(theType) && plantsOnLawn.mNormalPlant.mOnBungeeState != PlantOnBungeeState.GettingGrabbedByBungee)
                                    {
                                        return PlantingReason.Ok;
                                    }
                                    if (Plant.IsUpgrade(theType))
                                    {
                                        return PlantingReason.NeedsUpgrade;
                                    }
                                    if ((theType == SeedType.Wallnut || theType == SeedType.Tallnut) && mApp.mPlayerInfo.mPurchases[29] != 0 && plantsOnLawn.mNormalPlant.mPlantHealth < plantsOnLawn.mNormalPlant.mPlantMaxHealth * 2 / 3 && theType == plantsOnLawn.mNormalPlant.mSeedType && plantsOnLawn.mNormalPlant.mOnBungeeState != PlantOnBungeeState.GettingGrabbedByBungee)
                                    {
                                        return PlantingReason.Ok;
                                    }
                                    return PlantingReason.NotHere;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void MouseMove(int x, int y)
        {
            //base.MouseMove(x, y);
            MouseDrag(x, y);
            mChallenge.MouseMove(x, y);
        }

        public override void MouseDrag(int x, int y)
        {
            base.MouseDrag(x, y);
            if (mIgnoreMouseUp && mCursorObject.mCursorType == CursorType.PlantFromUsableCoin)
            {
                mIgnoreMouseUp = false;
            }
            mLastToolX = x;
            mLastToolY = y;
            if (mIgnoreNextMouseUp)
            {
                TRect trect = new TRect(Constants.ZEN_XMIN, Constants.ZEN_YMIN, Constants.ZEN_XMAX - Constants.ZEN_XMIN, Constants.ZEN_YMAX - Constants.ZEN_YMIN);
                if (trect.Contains(mApp.mBoard.mLastToolX, mApp.mBoard.mLastToolY))
                {
                    mIgnoreNextMouseUp = false;
                }
            }
            mChallenge.MouseMove(x, y);
        }

        public override void MouseDown(int x, int y, int theClickCount)
        {
            base.MouseDown(x, y, theClickCount);
            mLastToolX = x;
            mLastToolY = y;
            mMenuButton.Update();
            if (mStoreButton != null)
            {
                mStoreButton.Update();
            }
            mIgnoreMouseUp = !CanInteractWithBoardButtons();
            if (mTimeStopCounter > 0)
            {
                return;
            }
            HitResult theHitResult;
            MouseHitTest(x, y, out theHitResult, false);
            if (mChallenge.MouseDown(x, y, theClickCount, theHitResult))
            {
                return;
            }
            if (mMenuButton.IsMouseOver() && CanInteractWithBoardButtons() && theClickCount > 0)
            {
                mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
            }
            else if (mStoreButton != null && mStoreButton.IsMouseOver() && CanInteractWithBoardButtons() && theClickCount > 0)
            {
                if (mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom)
                {
                    mApp.PlaySample(Resources.SOUND_TAP);
                }
                else if (mApp.mGameMode == GameMode.ChallengeLastStand || mApp.mGameMode == GameMode.Upsell)
                {
                    mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
                }
            }
            if (mApp.mGameScene == GameScenes.LevelIntro && mApp.mSeedChooserScreen != null)
            {
                mApp.mSeedChooserScreen.CancelLawnView();
            }
            if (mApp.mGameScene == GameScenes.ZombiesWon)
            {
                mCutScene.ZombieWonClick();
                return;
            }
            if (mApp.mGameScene == GameScenes.LevelIntro)
            {
                mCutScene.MouseDown(x, y);
            }
            if (mApp.mTodCheatKeys && !mApp.IsScaryPotterLevel() && mNextSurvivalStageCounter > 0)
            {
                mNextSurvivalStageCounter = 2;
                for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
                {
                    if (mIceTimer[i] > mNextSurvivalStageCounter)
                    {
                        mIceTimer[i] = mNextSurvivalStageCounter;
                    }
                }
            }
            if (theHitResult.mObjectType == GameObjectType.Coin && theClickCount >= 0)
            {
                Coin coin = (Coin)theHitResult.mObject;
                coin.MouseDown(x, y, theClickCount);
                mIgnoreMouseUp = true;
                return;
            }
            if (mCursorObject.mCursorType == CursorType.WateringCan || mCursorObject.mCursorType == CursorType.Fertilizer || mCursorObject.mCursorType == CursorType.BugSpray || mCursorObject.mCursorType == CursorType.Phonograph || mCursorObject.mCursorType == CursorType.Chocolate || mCursorObject.mCursorType == CursorType.Glove || mCursorObject.mCursorType == CursorType.MoneySign || mCursorObject.mCursorType == CursorType.Wheeelbarrow || mCursorObject.mCursorType == CursorType.TreeFood)
            {
                MouseDownWithTool(x, y, theClickCount, mCursorObject.mCursorType, false);
                return;
            }
            if (IsPlantInCursor())
            {
                MouseDownWithPlant(x, y, theClickCount);
                return;
            }
            if (theHitResult.mObjectType == GameObjectType.Seedpacket)
            {
                RefreshSeedPacketFromCursor();
                SeedPacket seedPacket = (SeedPacket)theHitResult.mObject;
                seedPacket.MouseDown(x, y, theClickCount);
                return;
            }
            if (theHitResult.mObjectType == GameObjectType.NextGarden)
            {
                if (mApp.mGameMode == GameMode.ChallengeZenGarden)
                {
                    mApp.mZenGarden.GotoNextGarden();
                }
                else if (mApp.mGameMode == GameMode.TreeOfWisdom)
                {
                    mChallenge.TreeOfWisdomNextGarden();
                }
                mApp.PlaySample(Resources.SOUND_TAP);
                return;
            }
            if (theHitResult.mObjectType == GameObjectType.Shovel || theHitResult.mObjectType == GameObjectType.WateringCan || theHitResult.mObjectType == GameObjectType.Fertilizer || theHitResult.mObjectType == GameObjectType.BugSpray || theHitResult.mObjectType == GameObjectType.Phonograph || theHitResult.mObjectType == GameObjectType.Chocolate || theHitResult.mObjectType == GameObjectType.Glove || theHitResult.mObjectType == GameObjectType.MoneySign || theHitResult.mObjectType == GameObjectType.Wheelbarrow || theHitResult.mObjectType == GameObjectType.TreeFood)
            {
                if (mCursorObject.mCursorType != CursorType.Shovel)
                {
                    RefreshSeedPacketFromCursor();
                }
                PickUpTool(theHitResult.mObjectType);
                return;
            }
            if (theHitResult.mObjectType == GameObjectType.Plant)
            {
                Plant plant = (Plant)theHitResult.mObject;
                plant.MouseDown(x, y, theClickCount);
            }
        }

        public override void MouseUp(int x, int y, int theClickCount)
        {
            base.MouseUp(x, y, theClickCount);
            if (mIgnoreMouseUp)
            {
                mLastToolX = (mLastToolY = short.MinValue);
                mLastToolX = 0;
                return;
            }
            if (mIgnoreNextMouseUp)
            {
                mIgnoreNextMouseUp = false;
                return;
            }
            if (mIgnoreNextMouseUpSeedPacket)
            {
                HitResult hitResult;
                MouseHitTest(x, y, out hitResult, false);
                //mIgnoreNextMouseUpSeedPacket = false;
                if (hitResult.mObjectType == GameObjectType.Coin)
                {
                    Coin coin = (Coin)hitResult.mObject;
                    if (coin.mType == CoinType.UsableSeedPacket)
                    {
                        mLastToolX = (mLastToolY = short.MinValue);
                        return;
                    }
                }
            }
            if (mMenuButton.IsMouseOver() && CanInteractWithBoardButtons() && theClickCount > 0)
            {
                RefreshSeedPacketFromCursor();
                if (mApp.GetDialog(Dialogs.DIALOG_GAME_OVER) != null || mApp.GetDialog(Dialogs.DIALOG_LEVEL_COMPLETE) != null)
                {
                    return;
                }
                mMenuButton.mIsOver = false;
                mMenuButton.mIsDown = false;
                UpdateCursor();
                if (mTutorialState == TutorialState.ZenGardenCompleted)
                {
                    mApp.FinishZenGardenTutorial();
                    return;
                }
                if (mTutorialState == TutorialState.ZenGardenCompleted)
                {
                    mApp.mBoardResult = BoardResult.Won;
                    mApp.KillBoard();
                    mApp.PreNewGame(GameMode.Adventure, false);
                    return;
                }
                if (mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom || mApp.mGameMode == GameMode.Upsell)
                {
                    mApp.mPlayerInfo.mNeedsTrialLevelReset = true;
                    mApp.mBoardResult = BoardResult.Quit;
                    mApp.DoBackToMain();
                    return;
                }
                mApp.PlaySample(Resources.SOUND_PAUSE);
                mApp.DoNewOptions(false);
                return;
            }
            else
            {
                if ((mLastToolX >= Constants.LAWN_XMIN * Constants.S || mApp.mGameMode == GameMode.ChallengeZenGarden) && IsPlantInCursor())
                {
                    if (mCursorObject.mCursorType == CursorType.PlantFromGlove && mCursorObject.mGlovePlantID != null)
                    {
                        mCursorObject.mGlovePlantID.mGloveGrabbed = false;
                    }
                    MouseUpWithPlant(mLastToolX, mLastToolY, theClickCount);
                    return;
                }
                if ((mLastToolX < Constants.LAWN_XMIN * Constants.S || mLastToolY >= Constants.LAWN_YMIN * Constants.S) && mCursorObject.mCursorType == CursorType.Shovel)
                {
                    MouseDownWithTool(mLastToolX, mLastToolY, theClickCount, mCursorObject.mCursorType, false);
                    return;
                }
                if (mLastToolX >= Constants.LAWN_XMIN * Constants.S && mLastToolY >= Constants.LAWN_YMIN * Constants.S && mCursorObject.mCursorType == CursorType.CobcannonTarget)
                {
                    MouseDownCobcannonFire(mLastToolX, mLastToolY, theClickCount);
                    mLastToolX = (mLastToolY = short.MinValue);
                    return;
                }
                if ((mCursorObject.mCursorType == CursorType.Fertilizer || mCursorObject.mCursorType == CursorType.BugSpray || mCursorObject.mCursorType == CursorType.Phonograph || mCursorObject.mCursorType == CursorType.Chocolate || mCursorObject.mCursorType == CursorType.Glove || mCursorObject.mCursorType == CursorType.MoneySign || mCursorObject.mCursorType == CursorType.Wheeelbarrow || mCursorObject.mCursorType == CursorType.TreeFood || mCursorObject.mCursorType == CursorType.WateringCan) && mLastToolY >= Constants.ZEN_YMIN * Constants.S)
                {
                    MouseDownWithTool(x, y, theClickCount, mCursorObject.mCursorType, false);
                    return;
                }
                if (mChallenge.MouseUp(mLastToolX, mLastToolY) && theClickCount > 0)
                {
                    return;
                }
                if (mStoreButton != null && mStoreButton.IsMouseOver() && CanInteractWithBoardButtons() && theClickCount > 0)
                {
                    if (mApp.mGameMode == GameMode.ChallengeZenGarden)
                    {
                        ClearAdviceImmediately();
                        mApp.mZenGarden.OpenStore();
                    }
                    if (mApp.mGameMode == GameMode.ChallengeLastStand)
                    {
                        mChallenge.mChallengeState = ChallengeState.LastStandOnslaught;
                        mStoreButton.mBtnNoDraw = true;
                        mStoreButton.mDisabled = true;
                        mZombieCountDown = 9;
                        mZombieCountDownStart = mZombieCountDown;
                    }
                    else if (mApp.mGameMode == GameMode.Upsell)
                    {
                        mApp.BuyGame();
                        mApp.DoBackToMain();
                    }
                }
                mLastToolX = (mLastToolY = short.MinValue);
                return;
            }
        }

        public override void KeyChar(SexyChar theChar)
        {
            if (!mApp.mDebugKeysEnabled)
            {
                switch ((char)theChar) 
                {
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    mSeedBank.mSeedPackets[theChar-'1'].MouseDown(0, 0, 1);
                    break;
                }
            }
            else
            {
                char value_type = theChar.value_type;
                if (mApp.mGameMode == GameMode.ChallengeZenGarden)
                {
                    if (value_type == 'm')
                    {
                        if (mApp.mZenGarden.IsZenGardenFull(true))
                        {
                            return;
                        }
                        PottedPlant pottedPlant = new PottedPlant();
                        pottedPlant.InitializePottedPlant(SeedType.Marigold);
                        pottedPlant.mPlantAge = PottedPlantAge.Full;
                        pottedPlant.mDrawVariation = (DrawVariation)TodCommon.RandRangeInt(2, 12);
                        mApp.mZenGarden.AddPottedPlant(pottedPlant);
                        return;
                    }
                    else
                    {
                        if (value_type == '+')
                        {
                            while (!mApp.mZenGarden.IsZenGardenFull(true))
                            {
                                SeedType theSeedType = mApp.mZenGarden.PickRandomSeedType();
                                PottedPlant pottedPlant2 = new PottedPlant();
                                pottedPlant2.InitializePottedPlant(theSeedType);
                                pottedPlant2.mPlantAge = PottedPlantAge.Full;
                                mApp.mZenGarden.AddPottedPlant(pottedPlant2);
                            }
                            return;
                        }
                        if (value_type == '-')
                        {
                            mPlants.Clear();
                            mApp.mPlayerInfo.mNumPottedPlants = 0;
                            return;
                        }
                        if (value_type == 'a')
                        {
                            if (mApp.mZenGarden.IsZenGardenFull(true))
                            {
                                return;
                            }
                            SeedType theSeedType2 = mApp.mZenGarden.PickRandomSeedType();
                            PottedPlant pottedPlant3 = new PottedPlant();
                            pottedPlant3.InitializePottedPlant(theSeedType2);
                            pottedPlant3.mPlantAge = PottedPlantAge.Small;
                            mApp.mZenGarden.AddPottedPlant(pottedPlant3);
                            return;
                        }
                        else
                        {
                            if (value_type == 'f')
                            {
                                int num = -1;
                                Plant plant = null;
                                while (IteratePlants(ref plant, ref num))
                                {
                                    if (GetZenToolAt(plant.mPlantCol, plant.mRow) == null && plant.mPottedPlantIndex >= 0)
                                    {
                                        PottedPlant thePottedPlant = mApp.mZenGarden.PottedPlantFromIndex(plant.mPottedPlantIndex);
                                        PottedPlantNeed plantsNeed = mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
                                        if (plantsNeed == PottedPlantNeed.Water)
                                        {
                                            plant.mHighlighted = true;
                                            mApp.mZenGarden.MouseDownWithFeedingTool(plant.mX, plant.mY, CursorType.WateringCan);
                                            return;
                                        }
                                        if (plantsNeed == PottedPlantNeed.Fertilizer)
                                        {
                                            plant.mHighlighted = true;
                                            if (mApp.mPlayerInfo.mPurchases[14] <= 1000)
                                            {
                                                mApp.mPlayerInfo.mPurchases[14] = 1001;
                                            }
                                            mApp.mZenGarden.MouseDownWithFeedingTool(plant.mX, plant.mY, CursorType.Fertilizer);
                                            return;
                                        }
                                        if (plantsNeed == PottedPlantNeed.Bugspray)
                                        {
                                            plant.mHighlighted = true;
                                            if (mApp.mPlayerInfo.mPurchases[15] <= 1000)
                                            {
                                                mApp.mPlayerInfo.mPurchases[15] = 1001;
                                            }
                                            mApp.mZenGarden.MouseDownWithFeedingTool(plant.mX, plant.mY, CursorType.BugSpray);
                                            return;
                                        }
                                        if (plantsNeed == PottedPlantNeed.Phonograph)
                                        {
                                            plant.mHighlighted = true;
                                            mApp.mZenGarden.MouseDownWithFeedingTool(plant.mX, plant.mY, CursorType.Phonograph);
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
                                while (IteratePlants(ref plant2, ref num2))
                                {
                                    if (plant2.mPottedPlantIndex >= 0)
                                    {
                                        Debug.ASSERT(plant2.mPottedPlantIndex < mApp.mPlayerInfo.mNumPottedPlants);
                                        PottedPlant thePottedPlant2 = mApp.mPlayerInfo.mPottedPlant[plant2.mPottedPlantIndex];
                                        mApp.mZenGarden.ResetPlantTimers(thePottedPlant2);
                                    }
                                }
                                return;
                            }
                            if (value_type == 's')
                            {
                                if (mApp.mZenGarden.IsStinkySleeping())
                                {
                                    mApp.mZenGarden.WakeStinky();
                                    return;
                                }
                                mApp.mZenGarden.ResetStinkyTimers();
                                return;
                            }
                            else if (value_type == 'c')
                            {
                                if (mApp.mPlayerInfo.mPurchases[26] < 1000)
                                {
                                    mApp.mPlayerInfo.mPurchases[26] = 1001;
                                    return;
                                }
                                mApp.mPlayerInfo.mPurchases[26]++;
                                return;
                            }
                            else if (value_type == ']')
                            {
                                PottedPlant pottedPlantInWheelbarrow = mApp.mZenGarden.GetPottedPlantInWheelbarrow();
                                if (pottedPlantInWheelbarrow != null)
                                {
                                    pottedPlantInWheelbarrow.mSeedType++;
                                    if (pottedPlantInWheelbarrow.mSeedType == SeedType.Gatlingpea)
                                    {
                                        pottedPlantInWheelbarrow.mSeedType = SeedType.Peashooter;
                                    }
                                    if (pottedPlantInWheelbarrow.mSeedType == SeedType.Flowerpot)
                                    {
                                        pottedPlantInWheelbarrow.mSeedType++;
                                    }
                                }
                                return;
                            }
                        }
                    }
                }
                if (mApp.mGameMode == GameMode.TreeOfWisdom)
                {
                    if (value_type == 'f')
                    {
                        if (mApp.mPlayerInfo.mPurchases[28] <= 1000)
                        {
                            mApp.mPlayerInfo.mPurchases[28] = 1001;
                        }
                        mChallenge.TreeOfWisdomFertilize();
                        return;
                    }
                    if (value_type == 'g')
                    {
                        mChallenge.TreeOfWisdomGrow();
                        return;
                    }
                    if (value_type == 'b')
                    {
                        mChallenge.mChallengeStateCounter = 1;
                        return;
                    }
                    if (value_type == '0')
                    {
                        int currentChallengeIndex = mApp.GetCurrentChallengeIndex();
                        mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex] = 0;
                        mChallenge.TreeOfWisdomGrow();
                        return;
                    }
                    if (value_type == '1')
                    {
                        int currentChallengeIndex2 = mApp.GetCurrentChallengeIndex();
                        mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex2] = 9;
                        mChallenge.TreeOfWisdomGrow();
                        return;
                    }
                    if (value_type == '2')
                    {
                        int currentChallengeIndex3 = mApp.GetCurrentChallengeIndex();
                        mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex3] = 19;
                        mChallenge.TreeOfWisdomGrow();
                        return;
                    }
                    if (value_type == '3')
                    {
                        int currentChallengeIndex4 = mApp.GetCurrentChallengeIndex();
                        mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex4] = 29;
                        mChallenge.TreeOfWisdomGrow();
                        return;
                    }
                    if (value_type == '4')
                    {
                        int currentChallengeIndex5 = mApp.GetCurrentChallengeIndex();
                        mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex5] = 39;
                        mChallenge.TreeOfWisdomGrow();
                        return;
                    }
                    if (value_type == '5')
                    {
                        int currentChallengeIndex6 = mApp.GetCurrentChallengeIndex();
                        mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex6] = 49;
                        mChallenge.TreeOfWisdomGrow();
                        return;
                    }
                    if (value_type == '6')
                    {
                        int currentChallengeIndex7 = mApp.GetCurrentChallengeIndex();
                        mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex7] = 98;
                        mChallenge.TreeOfWisdomGrow();
                        return;
                    }
                    if (value_type == '7')
                    {
                        int currentChallengeIndex8 = mApp.GetCurrentChallengeIndex();
                        mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex8] = 498;
                        mChallenge.TreeOfWisdomGrow();
                        return;
                    }
                    if (value_type == '8')
                    {
                        int currentChallengeIndex9 = mApp.GetCurrentChallengeIndex();
                        mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex9] = 998;
                        mChallenge.TreeOfWisdomGrow();
                    }
                    return;
                }
                else
                {
                    Zombie bossZombie;
                    if (value_type == '<')
                    {
                        mApp.DoNewOptions(false);
                    }
                    else if (value_type == '2')
                    {
                        AddZombie(ZombieType.Digger, GameConstants.ZOMBIE_WAVE_DEBUG);
                    }
                    else if (value_type == 'l')
                    {
                        mApp.DoCheatDialog();
                    }
                    else if (value_type == '#')
                    {
                        if (mApp.IsSurvivalMode())
                        {
                            if (mApp.mGameScene == GameScenes.LevelIntro)
                            {
                                return;
                            }
                            mCurrentWave = mNumWaves;
                            mChallenge.mSurvivalStage += 5;
                            RemoveAllZombies();
                            FadeOutLevel();
                        }
                        else
                        {
                            bossZombie = GetBossZombie();
                            if (bossZombie != null)
                            {
                                bossZombie.ApplyBossSmokeParticles(true);
                            }
                        }
                    }
                    else if (value_type == '!')
                    {
                        mApp.mBoardResult = BoardResult.Cheat;
                        if (IsLastStandStageWithRepick())
                        {
                            if (mNextSurvivalStageCounter == 0)
                            {
                                mCurrentWave = mNumWaves;
                                RemoveAllZombies();
                                FadeOutLevel();
                            }
                        }
                        else if ((mApp.IsScaryPotterLevel() && !IsFinalScaryPotterStage()) || mApp.IsEndlessIZombie(mApp.mGameMode))
                        {
                            if (mNextSurvivalStageCounter == 0)
                            {
                                RemoveAllZombies();
                                FadeOutLevel();
                            }
                        }
                        else if (mApp.IsSurvivalMode())
                        {
                            if (mApp.mGameScene == GameScenes.LevelIntro)
                            {
                                return;
                            }
                            mCurrentWave = mNumWaves;
                            if (!IsSurvivalStageWithRepick())
                            {
                                RemoveAllZombies();
                            }
                            FadeOutLevel();
                        }
                        else
                        {
                            RemoveAllZombies();
                            FadeOutLevel();
                            mBoardFadeOutCounter = 200;
                        }
                    }
                    else if (value_type == '+')
                    {
                        mApp.mBoardResult = BoardResult.Cheat;
                        if (IsLastStandStageWithRepick())
                        {
                            if (mNextSurvivalStageCounter == 0)
                            {
                                mCurrentWave = mNumWaves;
                                RemoveAllZombies();
                                FadeOutLevel();
                            }
                        }
                        else if ((mApp.IsScaryPotterLevel() && !IsFinalScaryPotterStage()) || mApp.IsEndlessIZombie(mApp.mGameMode))
                        {
                            if (mNextSurvivalStageCounter == 0)
                            {
                                RemoveAllZombies();
                                FadeOutLevel();
                            }
                        }
                        else if (mApp.IsSurvivalEndless(mApp.mGameMode))
                        {
                            if (mApp.mGameScene == GameScenes.LevelIntro)
                            {
                                return;
                            }
                            mCurrentWave = mNumWaves;
                            RemoveAllZombies();
                            FadeOutLevel();
                        }
                        else if (mApp.IsSurvivalMode())
                        {
                            mChallenge.mSurvivalStage = 5;
                            RemoveAllZombies();
                            FadeOutLevel();
                            mBoardFadeOutCounter = 200;
                        }
                        else
                        {
                            RemoveAllZombies();
                            FadeOutLevel();
                            mBoardFadeOutCounter = 200;
                        }
                    }
                    else if (value_type == '8')
                    {
                        mApp.mEasyPlantingCheat = !mApp.mEasyPlantingCheat;
                    }
                    else if (value_type == '7')
                    {
                        mApp.ToggleSlowMo();
                    }
                    else if (value_type == '6')
                    {
                        mApp.ToggleFastMo();
                    }
                    else if (value_type == 'z')
                    {
                        ClearAdviceImmediately();
                        DisplayAdviceAgain("[ADVICE_HUGE_WAVE]", MessageStyle.HugeWave, AdviceType.HugeWave);
                        mHugeWaveCountDown = 750;
                        mDebugTextMode++;
                        if (mDebugTextMode > DebugTextMode.Collision)
                        {
                            mDebugTextMode = DebugTextMode.None;
                        }
                    }
                    if (mApp.mGameScene != GameScenes.Playing)
                    {
                        return;
                    }
                    bossZombie = GetBossZombie();
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
                    if (mApp.mGameMode == GameMode.ChallengeWarAndPeas || mApp.mGameMode == GameMode.ChallengeWarAndPeas2)
                    {
                        if (value_type == 'w')
                        {
                            AddZombie(ZombieType.WallnutHead, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 't')
                        {
                            AddZombie(ZombieType.TallnutHead, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 'j')
                        {
                            AddZombie(ZombieType.JalapenoHead, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 'g')
                        {
                            AddZombie(ZombieType.GatlingHead, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 's')
                        {
                            AddZombie(ZombieType.SquashHead, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                    }
                    if (value_type == 'q' && mApp.IsSurvivalEndless(mApp.mGameMode))
                    {
                        mApp.mEasyPlantingCheat = true;
                        for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
                        {
                            for (int j = 0; j < Constants.GRIDSIZEX; j++)
                            {
                                if (CanPlantAt(j, i, SeedType.Lilypad) == PlantingReason.Ok)
                                {
                                    AddPlant(j, i, SeedType.Lilypad, SeedType.None);
                                }
                                if (CanPlantAt(j, i, SeedType.Pumpkinshell) == PlantingReason.Ok && (j <= 6 || IsPoolSquare(j, i)))
                                {
                                    AddPlant(j, i, SeedType.Pumpkinshell, SeedType.None);
                                }
                                if (CanPlantAt(j, i, SeedType.Gatlingpea) == PlantingReason.Ok)
                                {
                                    if (j < 5)
                                    {
                                        AddPlant(j, i, SeedType.Gatlingpea, SeedType.None);
                                    }
                                    else if (j == 5)
                                    {
                                        AddPlant(j, i, SeedType.Torchwood, SeedType.None);
                                    }
                                    else if (j == 6)
                                    {
                                        AddPlant(j, i, SeedType.Splitpea, SeedType.None);
                                    }
                                    else if (i == 2 || i == 3)
                                    {
                                        AddPlant(j, i, SeedType.Gloomshroom, SeedType.None);
                                        if (CanPlantAt(j, i, SeedType.InstantCoffee) == PlantingReason.Ok)
                                        {
                                            AddPlant(j, i, SeedType.InstantCoffee, SeedType.None);
                                        }
                                    }
                                }
                            }
                        }
                        return;
                    }
                    if (value_type == 'q' && mApp.IsIZombieLevel())
                    {
                        mApp.mEasyPlantingCheat = true;
                        if (mApp.IsIZombieLevel())
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                mChallenge.IZombiePlaceZombie(ZombieType.Football, 6, k);
                            }
                            return;
                        }
                    }
                    else if (value_type == 'q')
                    {
                        mApp.mEasyPlantingCheat = true;
                        for (int l = 0; l < Constants.MAX_GRIDSIZEY; l++)
                        {
                            for (int m = 0; m < Constants.GRIDSIZEX; m++)
                            {
                                if (StageHasRoof() && CanPlantAt(m, l, SeedType.Flowerpot) == PlantingReason.Ok)
                                {
                                    AddPlant(m, l, SeedType.Flowerpot, SeedType.None);
                                }
                                if (CanPlantAt(m, l, SeedType.Lilypad) == PlantingReason.Ok)
                                {
                                    AddPlant(m, l, SeedType.Lilypad, SeedType.None);
                                }
                                if (CanPlantAt(m, l, SeedType.Threepeater) == PlantingReason.Ok)
                                {
                                    AddPlant(m, l, SeedType.Threepeater, SeedType.None);
                                }
                            }
                        }
                        if (!mChallenge.UpdateZombieSpawning())
                        {
                            int num3 = Math.Min(mNumWaves - mCurrentWave, 20);
                            for (int n = 0; n < num3; n++)
                            {
                                SpawnZombieWave();
                            }
                        }
                        if (mApp.IsScaryPotterLevel())
                        {
                            int num4 = -1;
                            GridItem gridItem = null;
                            while (IterateGridItems(ref gridItem, ref num4))
                            {
                                if (gridItem.mGridItemType == GridItemType.ScaryPot)
                                {
                                    mChallenge.ScaryPotterOpenPot(gridItem);
                                }
                            }
                            return;
                        }
                    }
                    else if (value_type == ']')
                    {
                        mApp.mEasyPlantingCheat = true;
                        for (int num5 = 0; num5 < Constants.MAX_GRIDSIZEY; num5++)
                        {
                            for (int num6 = 0; num6 < Constants.GRIDSIZEX; num6++)
                            {
                                if (StageHasRoof() && CanPlantAt(num6, num5, SeedType.Flowerpot) == PlantingReason.Ok)
                                {
                                    AddPlant(num6, num5, SeedType.Flowerpot, SeedType.None);
                                }
                                if (CanPlantAt(num6, num5, SeedType.Lilypad) == PlantingReason.Ok)
                                {
                                    AddPlant(num6, num5, SeedType.Lilypad, SeedType.None);
                                }
                                if (CanPlantAt(num6, num5, SeedType.Peashooter) == PlantingReason.Ok)
                                {
                                    AddPlant(num6, num5, SeedType.Peashooter, SeedType.None);
                                }
                            }
                        }
                        if (!mChallenge.UpdateZombieSpawning())
                        {
                            int num7 = Math.Min(mNumWaves - mCurrentWave, 20);
                            for (int num8 = 0; num8 < num7; num8++)
                            {
                                SpawnZombieWave();
                            }
                        }
                        if (mApp.IsScaryPotterLevel())
                        {
                            int num9 = -1;
                            GridItem gridItem2 = null;
                            while (IterateGridItems(ref gridItem2, ref num9))
                            {
                                if (gridItem2.mGridItemType == GridItemType.ScaryPot)
                                {
                                    mChallenge.ScaryPotterOpenPot(gridItem2);
                                }
                            }
                            return;
                        }
                    }
                    else if (value_type == '[')
                    {
                        if (!mChallenge.UpdateZombieSpawning())
                        {
                            int num10 = 1;
                            for (int num11 = 0; num11 < num10; num11++)
                            {
                                SpawnZombieWave();
                            }
                            return;
                        }
                    }
                    else if (value_type == '?' || value_type == '/')
                    {
                        if (mHugeWaveCountDown > 0)
                        {
                            mHugeWaveCountDown = 1;
                            return;
                        }
                        mZombieCountDown = 6;
                        return;
                    }
                    else if (value_type == 'b')
                    {
                        if (mApp.mGameMode != GameMode.ChallengeBeghouled && mApp.mGameMode != GameMode.ChallengeBeghouledTwist)
                        {
                            AddZombie(ZombieType.Bungee, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                    }
                    else
                    {
                        if (value_type == 'O')
                        {
                            AddZombie(ZombieType.Football, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 's')
                        {
                            AddZombie(ZombieType.Door, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 'L')
                        {
                            AddZombie(ZombieType.Ladder, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 'y')
                        {
                            AddZombie(ZombieType.Yeti, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 'a')
                        {
                            AddZombie(ZombieType.Flag, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 'w')
                        {
                            AddZombie(ZombieType.Newspaper, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 'F')
                        {
                            AddZombie(ZombieType.Balloon, GameConstants.ZOMBIE_WAVE_DEBUG);
                            return;
                        }
                        if (value_type == 'n')
                        {
                            if (StageHasPool())
                            {
                                AddZombie(ZombieType.Snorkel, GameConstants.ZOMBIE_WAVE_DEBUG);
                                return;
                            }
                        }
                        else
                        {
                            if (value_type == 'c')
                            {
                                AddZombie(ZombieType.TrafficCone, GameConstants.ZOMBIE_WAVE_DEBUG);
                                return;
                            }
                            if (value_type == 'm')
                            {
                                AddZombie(ZombieType.Dancer, GameConstants.ZOMBIE_WAVE_DEBUG);
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
                                mApp.DoPauseDialog();
                                return;
                            }
                            if (value_type == 'h')
                            {
                                AddZombie(ZombieType.Pail, GameConstants.ZOMBIE_WAVE_DEBUG);
                                return;
                            }
                            if (value_type == 'H')
                            {
                                AddZombie(ZombieType.Pail, GameConstants.ZOMBIE_WAVE_DEBUG);
                                AddZombie(ZombieType.Pail, GameConstants.ZOMBIE_WAVE_DEBUG);
                                AddZombie(ZombieType.Pail, GameConstants.ZOMBIE_WAVE_DEBUG);
                                AddZombie(ZombieType.Pail, GameConstants.ZOMBIE_WAVE_DEBUG);
                                AddZombie(ZombieType.Pail, GameConstants.ZOMBIE_WAVE_DEBUG);
                                return;
                            }
                            if (value_type == 'D')
                            {
                                AddZombie(ZombieType.Digger, GameConstants.ZOMBIE_WAVE_DEBUG);
                                return;
                            }
                            if (value_type == 'p')
                            {
                                AddZombie(ZombieType.Polevaulter, GameConstants.ZOMBIE_WAVE_DEBUG);
                                return;
                            }
                            if (value_type == 'P')
                            {
                                AddZombie(ZombieType.Pogo, GameConstants.ZOMBIE_WAVE_DEBUG);
                                return;
                            }
                            if (value_type == 'R')
                            {
                                if (StageHasPool())
                                {
                                    AddZombie(ZombieType.DolphinRider, GameConstants.ZOMBIE_WAVE_DEBUG);
                                    return;
                                }
                            }
                            else
                            {
                                if (value_type == 'j')
                                {
                                    AddZombie(ZombieType.JackInTheBox, GameConstants.ZOMBIE_WAVE_DEBUG);
                                    return;
                                }
                                if (value_type == 'g')
                                {
                                    AddZombie(ZombieType.Gargantuar, GameConstants.ZOMBIE_WAVE_DEBUG);
                                    return;
                                }
                                if (value_type == 'G')
                                {
                                    AddZombie(ZombieType.RedeyeGargantuar, GameConstants.ZOMBIE_WAVE_DEBUG);
                                    return;
                                }
                                if (value_type == 'i')
                                {
                                    AddZombie(ZombieType.Zamboni, GameConstants.ZOMBIE_WAVE_DEBUG);
                                    return;
                                }
                                if (value_type == 'C')
                                {
                                    AddZombie(ZombieType.Catapult, GameConstants.ZOMBIE_WAVE_DEBUG);
                                    return;
                                }
                                if (value_type == '1')
                                {
                                    Plant topPlantAt = GetTopPlantAt(0, 0, TopPlant.Any);
                                    if (topPlantAt != null)
                                    {
                                        topPlantAt.Die();
                                        mChallenge.ZombieAtePlant(null, topPlantAt);
                                        return;
                                    }
                                }
                                else
                                {
                                    if (value_type == 'B')
                                    {
                                        mFogBlownCountDown = 2200;
                                        return;
                                    }
                                    if (value_type == 't')
                                    {
                                        if (!CanAddBobSled())
                                        {
                                            int num12 = RandomNumbers.NextNumber(5);
                                            int num13 = 400;
                                            if (StageHasPool())
                                            {
                                                num12 = RandomNumbers.NextNumber(2);
                                            }
                                            else if (StageHasRoof())
                                            {
                                                num13 = 500;
                                            }
                                            mIceTimer[num12] = 3000;
                                            mIceMinX[num12] = num13;
                                        }
                                        AddZombie(ZombieType.Bobsled, GameConstants.ZOMBIE_WAVE_DEBUG);
                                        return;
                                    }
                                    if (value_type == 'r')
                                    {
                                        SpawnZombiesFromGraves();
                                        return;
                                    }
                                    if (value_type == '0')
                                    {
                                        AddSunMoney(100);
                                        mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
                                        return;
                                    }
                                    if (value_type == '9')
                                    {
                                        AddSunMoney(999999);
                                        mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
                                        return;
                                    }
                                    if (value_type == '$')
                                    {
                                        mApp.mPlayerInfo.AddCoins(100);
                                        mApp.PlaySample(Resources.SOUND_BUTTONCLICK);
                                        ShowCoinBank();
                                        return;
                                    }
                                    if (value_type == '-')
                                    {
                                        mSunMoney -= 100;
                                        if (mSunMoney < 0)
                                        {
                                            mSunMoney = 0;
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
                                            mLawnMowers.Clear();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return;
                }
            }
        }

        public override void KeyUp(KeyCode theKey)
        {
        }

        public override void KeyDown(KeyCode theKey)
        {
            DoTypingCheck(theKey);
            if ((ushort)theKey == 32 || (ushort)theKey == 13)
            {
                if (IsScaryPotterDaveTalking() && mApp.mCrazyDaveMessageIndex != -1)
                {
                    mChallenge.AdvanceCrazyDaveDialog();
                    return;
                }
                if (mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom)
                {
                    mApp.mZenGarden.AdvanceCrazyDaveDialog();
                    return;
                }
            }
            if ((ushort)theKey == 32 && mApp.CanPauseNow())
            {
                mApp.PlaySample(Resources.SOUND_PAUSE);
                mApp.DoPauseDialog();
            }
            if (theKey == KeyCode.Escape)
            {
                if (mCursorObject.mCursorType != CursorType.Normal)
                {
                    RefreshSeedPacketFromCursor();
                    return;
                }
                if (CanInteractWithBoardButtons() && mApp.mGameScene != GameScenes.ZombiesWon)
                {
                    mApp.DoNewOptions(false);
                }
            }
        }

        public override void Update()
        {
            base.Update();
            MarkDirty();
            mCutScene.Update(false);
            mCutScene.Update(false);
            mCutScene.Update(true);
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                mApp.mZenGarden.ZenGardenUpdate(0);
                mApp.mZenGarden.ZenGardenUpdate(1);
                mApp.mZenGarden.ZenGardenUpdate(2);
                mApp.UpdateCrazyDave();
            }
            if (IsScaryPotterDaveTalking())
            {
                mApp.UpdateCrazyDave();
            }
            if (mPaused)
            {
                mChallenge.Update();
                mChallenge.Update();
                mChallenge.Update();
                mCursorPreview.mVisible = false;
                mCursorObject.mVisible = false;
                return;
            }
            bool aDisabled = !CanInteractWithBoardButtons() || mIgnoreMouseUp;
            if (!mMenuButton.mBtnNoDraw)
            {
                mMenuButton.mDisabled = aDisabled;
            }
            mMenuButton.Update();
            if (mStoreButton != null)
            {
                mStoreButton.mDisabled = aDisabled;
                mStoreButton.Update();
            }
            mApp.mEffectSystem.Update();
            mAdvice.Update();
            UpdateTutorial();
            UpdateTutorial();
            UpdateTutorial();
            if (mCobCannonCursorDelayCounter > 0)
            {
                mCobCannonCursorDelayCounter -= 3;
            }
            if (mOutOfMoneyCounter > 0)
            {
                mOutOfMoneyCounter -= 3;
            }
            if (mShakeCounter > 0)
            {
                mShakeCounter -= 3;
                if (mShakeCounter == 0)
                {
                    mX = Constants.Board_Offset_AspectRatio_Correction;
                    mY = 0;
                }
                else
                {
                    mX = TodCommon.TodAnimateCurve(12, 0, mShakeCounter, Constants.Board_Offset_AspectRatio_Correction, Constants.Board_Offset_AspectRatio_Correction - mShakeAmountX, TodCurves.Bounce);
                    mY = TodCommon.TodAnimateCurve(12, 0, mShakeCounter, 0, mShakeAmountY, TodCurves.Bounce);
                }
            }
            if (mCoinBankFadeCount > 0 && mApp.GetDialog(Dialogs.DIALOG_PURCHASE_PACKET_SLOT) == null)
            {
                mCoinBankFadeCount -= 3;
            }
            if (mLevelFadeCount > 0)
            {
                mLevelFadeCount -= 3;
            }
            UpdateLayers();
            if (mTimeStopCounter > 0)
            {
                return;
            }
            mEffectCounter += 3;
            if (StageHasPool() && mPoolSparklyParticleID == null)
            {
                int aRenderOrder = 220000;
                TodParticleSystem theParticle = mApp.AddTodParticle(450 + Constants.BOARD_EXTRA_ROOM, 295f, aRenderOrder, ParticleEffect.PoolSparkly);
                mPoolSparklyParticleID = mApp.ParticleGetID(theParticle);
            }
            UpdateGridItems();
            UpdateFwoosh();
            UpdateGame();
            UpdateFog();
            UpdateFog();
            UpdateFog();
            mChallenge.Update();
            mChallenge.Update();
            mChallenge.Update();
            UpdateLevelEndSequence();
            UpdateLevelEndSequence();
            UpdateLevelEndSequence();
            mPrevMouseX = mApp.mWidgetManager.mLastMouseX;
            mPrevMouseY = mApp.mWidgetManager.mLastMouseY;
        }

        public void UpdateLayers()
        {
            if (mWidgetManager != null)
            {
                mWidgetManager.MarkAllDirty();
            }
            for (LinkedListNode<Dialog> linkedListNode = mApp.mDialogList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
            {
                Dialog value = linkedListNode.Value;
                mWidgetManager.BringToFront(value);
                value.MarkDirty();
            }
        }

        public override void Draw(Graphics g)
        {
            if (mApp.GetDialog(Dialogs.DIALOG_STORE) != null || mApp.GetDialog(Dialogs.DIALOG_ALMANAC) != null)
            {
                return;
            }
            g.SetLinearBlend(true);
            if (mDrawCount == 0 || !mCutScene.mPreloaded)
            {
                ResetFPSStats();
            }
            else
            {
                int tickCount = Environment.TickCount;
                int num = mDrawCount - mIntervalDrawCountStart;
                int num2 = tickCount - mIntervalDrawTime;
                if (num2 > 10000)
                {
                    float num3 = (num * 1000f + 500f) / num2;
                    if (num3 < mMinFPS)
                    {
                        mMinFPS = num3;
                    }
                    mIntervalDrawCountStart = mDrawCount;
                    mIntervalDrawTime = tickCount;
                }
            }
            mDrawCount++;
            DrawGameObjects(g);
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
        }

        public void DrawBackdrop(Graphics g)
        {
            Image image = null;
            switch (mBackground)
            {
            case BackgroundType.Num1Day:
                image = Resources.IMAGE_BACKGROUND1;
                break;
            case BackgroundType.Num2Night:
                image = Resources.IMAGE_BACKGROUND2;
                break;
            case BackgroundType.Num3Pool:
                image = Resources.IMAGE_BACKGROUND3;
                break;
            case BackgroundType.Num4Fog:
                image = Resources.IMAGE_BACKGROUND4;
                break;
            case BackgroundType.Num5Roof:
                image = Resources.IMAGE_BACKGROUND5;
                break;
            case BackgroundType.Num6Boss:
                image = Resources.IMAGE_BACKGROUND6BOSS;
                break;
            case BackgroundType.MushroomGarden:
                image = Resources.IMAGE_BACKGROUND_MUSHROOMGARDEN;
                break;
            case BackgroundType.Greenhouse:
                image = Resources.IMAGE_BACKGROUND_GREENHOUSE;
                break;
            case BackgroundType.Zombiquarium:
                image = Resources.IMAGE_AQUARIUM1;
                break;
            case BackgroundType.TreeOfWisdom:
                image = null;
                break;
            default:
                Debug.ASSERT(false);
                break;
            }
            GameMode aGameMode = mApp.mGameMode;
            if (mLevel == 1 && mApp.IsFirstTimeAdventureMode())
            {
                g.DrawImage(Resources.IMAGE_BACKGROUND1UNSODDED, -(Constants.BOARD_OFFSET * Constants.S), 0f);
                int theWidth = TodCommon.TodAnimateCurve(0, 950, mSodPosition, 0, AtlasResources.IMAGE_SOD1ROW.GetWidth(), TodCurves.Linear);
                TRect theSrcRect = new TRect(0, 0, theWidth, AtlasResources.IMAGE_SOD1ROW.GetHeight());
                g.DrawImage(AtlasResources.IMAGE_SOD1ROW, (int)((float)(-(float)Constants.BOARD_OFFSET + 239) * Constants.S), (int)(265f * Constants.S), theSrcRect);
            }
            else if (((mLevel == 2 || mLevel == 3) && mApp.IsFirstTimeAdventureMode()) || mApp.mGameMode == GameMode.ChallengeResodded)
            {
                g.DrawImage(Resources.IMAGE_BACKGROUND1UNSODDED, (float)(-Constants.BOARD_OFFSET) * Constants.S, 0f);
                g.DrawImage(AtlasResources.IMAGE_SOD1ROW, (float)(-(float)Constants.BOARD_OFFSET + 239) * Constants.S, 265f * Constants.S);
                int theWidth2 = TodCommon.TodAnimateCurve(0, 950, mSodPosition, 0, AtlasResources.IMAGE_SOD3ROW.GetWidth(), TodCurves.Linear);
                TRect theSrcRect2 = new TRect(0, 0, theWidth2, AtlasResources.IMAGE_SOD3ROW.GetHeight());
                g.DrawImage(AtlasResources.IMAGE_SOD3ROW, (int)((float)(-(float)Constants.BOARD_OFFSET + 235) * Constants.S), (int)(149f * Constants.S), theSrcRect2);
            }
            else if (mLevel == 4 && mApp.IsFirstTimeAdventureMode())
            {
                g.DrawImage(Resources.IMAGE_BACKGROUND1UNSODDED, (float)(-Constants.BOARD_OFFSET) * Constants.S, 0f);
                g.DrawImage(AtlasResources.IMAGE_SOD3ROW, (float)(-(float)Constants.BOARD_OFFSET + 235) * Constants.S, 149f * Constants.S);
                int num = TodCommon.TodAnimateCurve(0, 950, mSodPosition, 0, 773, TodCurves.Linear);
                TRect theSrcRect3 = new TRect((int)(232f * Constants.S), 0, (int)(num * Constants.S), Resources.IMAGE_BACKGROUND1.GetHeight());
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
                    g.DrawImage(image, (int)((float)(-Constants.BOARD_OFFSET) * Constants.S), 0);
                }
            }
            if (mApp.mGameScene == GameScenes.ZombiesWon)
            {
                DrawHouseDoorBottom(g);
            }
            if (mTutorialState == TutorialState.Level1PlantPeashooter)
            {
                Graphics @new = Graphics.GetNew(g);
                SexyColor flashingColor = TodCommon.GetFlashingColor(mMainCounter, 75);
                @new.SetColorizeImages(true);
                @new.SetColor(flashingColor);
                @new.DrawImage(AtlasResources.IMAGE_SOD1ROW, (float)(-(float)Constants.BOARD_OFFSET + 239) * Constants.S, 265f * Constants.S);
                @new.SetColorizeImages(false);
                @new.PrepareForReuse();
            }
            mChallenge.DrawBackdrop(g);
            if (mApp.mGameScene == GameScenes.LevelIntro && StageHasGraveStones())
            {
                g.DrawImage(AtlasResources.IMAGE_NIGHT_GRAVE_GRAPHIC, Constants.InvertAndScale(640f), 40f * Constants.S);
            }
        }

        public void DrawCursorOnBackground(Graphics g)
        {
            if (mTimeStopCounter == 0 && (!mApp.IsWhackAZombieLevel() || mCursorObject.mCursorType != CursorType.Hammer) /*&& mIsDown */&& mLastToolX >= Constants.LAWN_XMIN * Constants.S && mCursorObject.BeginDraw(g))
            {
                mCursorObject.DrawGroundLayer(g);
                mCursorObject.EndDraw(g);
            }
        }

        public void DrawCursorOverlay(Graphics g)
        {
            if (mTimeStopCounter == 0 /*&& mIsDown && (mLastToolX >= Constants.LAWN_XMIN * Constants.S || mApp.mGameMode == GameMode.ChallengeZenGarden)*/ && mCursorObject.BeginDraw(g))
            {
                mCursorObject.DrawTopLayer(g);
                mCursorObject.EndDraw(g);
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
            mSunMoney += theAmount;
            if (mSunMoney > 9990)
            {
                mSunMoney = 9990;
            }
        }

        public bool TakeSunMoney(int theAmount)
        {
            if (theAmount <= mSunMoney + CountSunBeingCollected())
            {
                mSunMoney -= theAmount;
                return true;
            }
            mApp.PlaySample(Resources.SOUND_BUZZER);
            mOutOfMoneyCounter = 70;
            return false;
        }

        public bool CanTakeSunMoney(int theAmount)
        {
            return theAmount <= mSunMoney + CountSunBeingCollected();
        }

        public void Pause(bool thePause)
        {
            if (mPaused == thePause)
            {
                return;
            }
            if (thePause)
            {
                mPaused = true;
                if (mApp.mPlayerInfo.mCoins > 0)
                {
                    ShowCoinBank();
                }
                mLevelFadeCount = 1000;
                if (mApp.mGameScene != GameScenes.LevelIntro)
                {
                    mApp.mSoundSystem.GamePause(true);
                    mApp.mMusic.GameMusicPause(true);
                    return;
                }
            }
            else
            {
                mPaused = false;
                mApp.mSoundSystem.GamePause(false);
                mApp.mMusic.GameMusicPause(false);
            }
        }

        public void TryToSaveGame()
        {
            ClearCursor();
            string savedGameName = LawnCommon.GetSavedGameName(mApp.mGameMode, (int)mApp.mPlayerInfo.mId);
            if (NeedSaveGame())
            {
                if (mBoardFadeOutCounter >= 0)
                {
                    CompleteEndLevelSequenceForSaving();
                    return;
                }
                Common.MkDir(GlobalStaticVars.GetDocumentsDir() + "userdata");
                mApp.mMusic.GameMusicPause(true);
                SaveGame(savedGameName);
                SurvivalSaveScore();
            }
        }

        public bool NeedSaveGame()
        {
            return mApp.mGameMode != GameMode.ChallengeIce && mApp.mGameMode != GameMode.ChallengeZenGarden && mApp.mGameMode != GameMode.Upsell && mApp.mGameMode != GameMode.Intro && mApp.mGameScene == GameScenes.Playing;
        }

        public bool RowCanHaveZombies(int theRow)
        {
            return theRow >= 0 && theRow < Constants.MAX_GRIDSIZEY && ((mApp.mGameMode == GameMode.ChallengeResodded && theRow <= 4) || mPlantRow[theRow] != PlantRowType.Dirt);
        }

        public void ProcessDeleteQueue()
        {
            for (int i = mPlants.Count - 1; i >= 0; i--)
            {
                if (mPlants[i].mDead)
                {
                    mPlants[i].PrepareForReuse();
                    mPlants.RemoveAt(i);
                }
            }
            for (int j = mZombies.Count - 1; j >= 0; j--)
            {
                if (mZombies[j].mDead)
                {
                    if (mZombies[j].mZombieType == ZombieType.Boss)
                    {
                        for (int k = 0; k < 6; k++)
                        {
                            List<Zombie> zombiesInRow = GetZombiesInRow(k);
                            zombiesInRow.Remove(mZombies[j]);
                        }
                    }
                    else
                    {
                        List<Zombie> zombiesInRow2 = GetZombiesInRow(mZombies[j].mRow);
                        zombiesInRow2.Remove(mZombies[j]);
                    }
                    mZombies[j].PrepareForReuse();
                    mZombies.RemoveAt(j);
                }
            }
            for (int l = mProjectiles.Count - 1; l >= 0; l--)
            {
                if (mProjectiles[l].mDead)
                {
                    mProjectiles[l].PrepareForReuse();
                    mProjectiles.RemoveAt(l);
                }
            }
            for (int m = mCoins.Count - 1; m >= 0; m--)
            {
                if (mCoins[m].mDead)
                {
                    mCoins.RemoveAt(m);
                }
            }
            for (int n = mLawnMowers.Count - 1; n >= 0; n--)
            {
                if (mLawnMowers[n].mDead)
                {
                    mLawnMowers[n].PrepareForReuse();
                    mLawnMowers.RemoveAt(n);
                }
            }
            for (int num = mLawnMowers.Count - 1; num >= 0; num--)
            {
                if (mLawnMowers[num].mDead)
                {
                    mGridItems[num].PrepareForReuse();
                    mGridItems.RemoveAt(num);
                }
            }
        }

        public bool ChooseSeedsOnCurrentLevel()
        {
            return !mApp.IsChallengeWithoutSeedBank() && !HasConveyorBeltSeedBank() && mApp.mGameMode != GameMode.ChallengeIce && mApp.mGameMode != GameMode.ChallengeZenGarden && mApp.mGameMode != GameMode.TreeOfWisdom && mApp.mGameMode != GameMode.ChallengeBeghouled && mApp.mGameMode != GameMode.ChallengeBeghouledTwist && mApp.mGameMode != GameMode.ChallengeZombiquarium && !mApp.IsIZombieLevel() && !mApp.IsSquirrelLevel() && !mApp.IsSlotMachineLevel() && ((!mApp.IsAdventureMode() && !mApp.IsQuickPlayMode()) || !mApp.IsFirstTimeAdventureMode() || mLevel > 7);
        }

        public int GetNumSeedsInBank()
        {
            if (mApp.IsScaryPotterLevel())
            {
                return 1;
            }
            if (mApp.IsWhackAZombieLevel())
            {
                return 3;
            }
            if (mApp.IsChallengeWithoutSeedBank())
            {
                return 0;
            }
            if (HasConveyorBeltSeedBank())
            {
                return 9;
            }
            if (mApp.mGameMode == GameMode.ChallengeIce)
            {
                return 6;
            }
            if (mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                return 0;
            }
            if (mApp.mGameMode == GameMode.ChallengeZombiquarium)
            {
                return 2;
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombie1 || mApp.mGameMode == GameMode.PuzzleIZombie2 || mApp.mGameMode == GameMode.PuzzleIZombie3 || mApp.mGameMode == GameMode.PuzzleIZombie4)
            {
                return 3;
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombie5 || mApp.mGameMode == GameMode.PuzzleIZombie6 || mApp.mGameMode == GameMode.PuzzleIZombie7)
            {
                return 4;
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombie8)
            {
                return 6;
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombie9)
            {
                return 8;
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombieEndless)
            {
                return 9;
            }
            if (mApp.IsSlotMachineLevel())
            {
                return 3;
            }
            int num = mApp.mPlayerInfo.mPurchases[21] + 6;
            int seedsAvailable = mApp.GetSeedsAvailable();
            if (seedsAvailable < num)
            {
                num = seedsAvailable;
            }
            return num;
        }

        public bool StageIsDayWithoutPool()
        {
            return mBackground == BackgroundType.Num1Day;
        }

        public bool StageIsNight()
        {
            return mBackground == BackgroundType.Num2Night || mBackground == BackgroundType.Num4Fog || mBackground == BackgroundType.MushroomGarden || mBackground == BackgroundType.Num6Boss;
        }

        public bool StageHasPool()
        {
            return mBackground == BackgroundType.Num3Pool || mBackground == BackgroundType.Num4Fog;
        }

        public bool StageHas6Rows()
        {
            return mBackground == BackgroundType.Num3Pool || mBackground == BackgroundType.Num4Fog;
        }

        public bool StageHasFog()
        {
            return !mApp.IsStormyNightLevel() && mApp.mGameMode != GameMode.ChallengeInvisighoul && mBackground == BackgroundType.Num4Fog;
        }

        public bool StageHasGraveStones()
        {
            return !mApp.IsWallnutBowlingLevel() && mApp.mGameMode != GameMode.ChallengePogoParty && mApp.mGameMode != GameMode.ChallengeBeghouled && mApp.mGameMode != GameMode.ChallengeBeghouledTwist && mApp.mGameMode != GameMode.ChallengePortalCombat && mApp.mGameMode != GameMode.ChallengeLastStand && !mApp.IsIZombieLevel() && !mApp.IsScaryPotterLevel() && mBackground == BackgroundType.Num2Night;
        }

        public int PixelToGridX(int theX, int theY)
        {
            if (mApp.mGameMode == GameMode.ChallengeZenGarden && (mBackground == BackgroundType.MushroomGarden || mBackground == BackgroundType.Zombiquarium || mBackground == BackgroundType.Greenhouse))
            {
                return mApp.mZenGarden.PixelToGridX(theX, theY);
            }
            if (theX < Constants.LAWN_XMIN)
            {
                return -1;
            }
            return TodCommon.ClampInt((theX - Constants.LAWN_XMIN) / 80, 0, Constants.GRIDSIZEX - 1);
        }

        public int PixelToGridY(int theX, int theY)
        {
            if (mApp.mGameMode == GameMode.ChallengeZenGarden && (mBackground == BackgroundType.MushroomGarden || mBackground == BackgroundType.Zombiquarium || mBackground == BackgroundType.Greenhouse))
            {
                return mApp.mZenGarden.PixelToGridY(theX, theY);
            }
            int num = PixelToGridX(theX, theY);
            if (num == -1 || theY < Constants.LAWN_YMIN)
            {
                return -1;
            }
            if (StageHasRoof())
            {
                int num2 = 0;
                if (num < 5)
                {
                    num2 = (5 - num) * 20 - 20;
                }
                return TodCommon.ClampInt((theY - Constants.LAWN_YMIN - num2) / 85, 0, 4);
            }
            if (StageHas6Rows())
            {
                return TodCommon.ClampInt((theY - Constants.LAWN_YMIN) / 85, 0, 5);
            }
            return TodCommon.ClampInt((theY - Constants.LAWN_YMIN) / 100, 0, 4);
        }

        public int GridToPixelX(int theGridX, int theGridY)
        {
            Debug.ASSERT(theGridX >= 0 && theGridX < Constants.GRIDSIZEX);
            Debug.ASSERT(theGridY >= 0 && theGridY < Constants.MAX_GRIDSIZEY);
            if (mApp.mGameMode == GameMode.ChallengeZenGarden && (mBackground == BackgroundType.MushroomGarden || mBackground == BackgroundType.Zombiquarium || mBackground == BackgroundType.Greenhouse))
            {
                return mApp.mZenGarden.GridToPixelX(theGridX, theGridY);
            }
            return theGridX * Constants.New.Board_GridCellSizeX + Constants.LAWN_XMIN;
        }

        public int GridToPixelY(int theGridX, int theGridY)
        {
            Debug.ASSERT(theGridX >= 0 && theGridX < Constants.GRIDSIZEX);
            Debug.ASSERT(theGridY >= 0 && theGridY < Constants.MAX_GRIDSIZEY);
            if (mApp.mGameMode == GameMode.ChallengeZenGarden && (mBackground == BackgroundType.MushroomGarden || mBackground == BackgroundType.Zombiquarium || mBackground == BackgroundType.Greenhouse))
            {
                return mApp.mZenGarden.GridToPixelY(theGridX, theGridY);
            }
            int num2;
            if (StageHasRoof())
            {
                int num = 0;
                if (theGridX < 5)
                {
                    num = (5 - theGridX) * 20;
                }
                num2 = theGridY * Constants.New.Board_GridCellSizeY_6Rows + Constants.LAWN_YMIN + num - 10;
            }
            else if (StageHas6Rows())
            {
                num2 = theGridY * Constants.New.Board_GridCellSizeY_6Rows + Constants.LAWN_YMIN;
            }
            else
            {
                num2 = theGridY * Constants.New.Board_GridCellSizeY_5Rows + Constants.LAWN_YMIN;
            }
            if (theGridX != -1 && mGridSquareType[theGridX, theGridY] == GridSquareType.HighGround)
            {
                num2 += -Constants.HIGH_GROUND_HEIGHT;
            }
            return num2;
        }

        public int PixelToGridXKeepOnBoard(int theX, int theY)
        {
            return Math.Max(PixelToGridX(theX, theY), 0);
        }

        public int PixelToGridYKeepOnBoard(int theX, int theY)
        {
            int theX2 = Math.Max(theX, Constants.LAWN_XMIN);
            return Math.Max(PixelToGridY(theX2, theY), 0);
        }

        public void UpdateGameObjects()
        {
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead)
                {
                    plant.Update();
                }
            }
            count = mZombies.Count;
            for (int j = 0; j < count; j++)
            {
                Zombie zombie = mZombies[j];
                if (!zombie.mDead)
                {
                    zombie.Update();
                }
            }
            count = mProjectiles.Count;
            for (int k = 0; k < count; k++)
            {
                Projectile projectile = mProjectiles[k];
                if (!projectile.mDead)
                {
                    projectile.Update();
                }
            }
            Coin coin = null;
            while (IterateCoins(ref coin))
            {
                coin.Update();
            }
            LawnMower lawnMower = null;
            while (IterateLawnMowers(ref lawnMower))
            {
                lawnMower.Update();
            }
            mCursorPreview.Update();
            mCursorObject.Update();
            for (int l = 0; l < mSeedBank.mNumPackets; l++)
            {
                SeedPacket seedPacket = mSeedBank.mSeedPackets[l];
                seedPacket.Update();
                seedPacket.Update();
                seedPacket.Update();
            }
        }

        public bool MouseHitTest(int x, int y, out HitResult theHitResult, bool posScaled)
        {
            if (!posScaled)
            {
                x = (int)(x * Constants.IS);
                y = (int)(y * Constants.IS);
            }
            theHitResult = default(HitResult);
            if (mBoardFadeOutCounter >= 0)
            {
                theHitResult.mObject = null;
                theHitResult.mObjectType = GameObjectType.None;
                return false;
            }
            if (IsScaryPotterDaveTalking())
            {
                theHitResult.mObject = null;
                theHitResult.mObjectType = GameObjectType.None;
                return false;
            }
            if (mMenuButton.IsMouseOver() && CanInteractWithBoardButtons())
            {
                theHitResult.mObjectType = GameObjectType.MenuButton;
                return true;
            }
            if (mStoreButton != null && mStoreButton.IsMouseOver() && CanInteractWithBoardButtons())
            {
                theHitResult.mObjectType = GameObjectType.StoreButton;
                return true;
            }
            TRect shovelButtonRect = GetShovelButtonRect();
            x = (int)(x * Constants.S);
            y = (int)(y * Constants.S);
            if (mSeedBank.MouseHitTest(x, y, out theHitResult))
            {
                if (mCursorObject.mCursorType == CursorType.PlantFromBank)
                {
                    SeedPacket seedPacket = (SeedPacket)theHitResult.mObject;
                    int aSeedBankIndex = mCursorObject.mSeedBankIndex;
                    RefreshSeedPacketFromCursor();
                    if (aSeedBankIndex == seedPacket.mIndex)
                    {
                        theHitResult.mObjectType = GameObjectType.None;
                        return true;
                    }
                    return true;
                }
                else if (mCursorObject.mCursorType == CursorType.Normal || mCursorObject.mCursorType == CursorType.Shovel || mCursorObject.mCursorType == CursorType.CobcannonTarget || mCursorObject.mCursorType == CursorType.Hammer)
                {
                    return true;
                }
            }
            if (mShowShovel && shovelButtonRect.Contains(x, y) && CanInteractWithBoardButtons())
            {
                theHitResult.mObjectType = GameObjectType.Shovel;
                return true;
            }
            Coin coin = null;
            Coin coin2 = null;
            while (IterateCoins(ref coin2))
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
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                bool flag = false;
                if (mCursorObject.mCursorType == CursorType.Chocolate && !mApp.mZenGarden.IsStinkyHighOnChocolate())
                {
                    flag = true;
                }
                else if (mCursorObject.mCursorType == CursorType.Normal && mApp.mZenGarden.IsStinkySleeping())
                {
                    flag = true;
                }
                GridItem stinky = mApp.mZenGarden.GetStinky();
                if (flag && stinky != null)
                {
                    TRect trect = new TRect((int)(Constants.S * (stinky.mPosX - 6f)), (int)(Constants.S * (stinky.mPosY - 10f)), (int)(Constants.S * 84f), (int)(Constants.S * 90f));
                    if (trect.Contains(x, y))
                    {
                        theHitResult.mObjectType = GameObjectType.Stinky;
                        return true;
                    }
                }
            }
            x = (int)(x * Constants.IS);
            y = (int)(y * Constants.IS);
            if (mApp.mGameMode == GameMode.TreeOfWisdom && mCursorObject.mCursorType == CursorType.TreeFood && mChallenge.TreeOfWisdomHitTest(x, y, theHitResult))
            {
                return true;
            }
            x = (int)(x * Constants.S);
            y = (int)(y * Constants.S);
            if ((mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom) && CanInteractWithBoardButtons())
            {
                for (int i = 6; i <= 15; i++)
                {
                    GameObjectType gameObjectType = (GameObjectType)i;
                    if (CanUseGameObject(gameObjectType) && (gameObjectType != GameObjectType.TreeFood || mChallenge.TreeOfWisdomCanFeed()) && (gameObjectType != GameObjectType.MoneySign || mApp.mPlayerInfo.mZenGardenTutorialComplete) && GetZenButtonRect(gameObjectType).Contains(x, y))
                    {
                        theHitResult.mObjectType = gameObjectType;
                        return true;
                    }
                }
            }
            if (mApp.IsSlotMachineLevel() && mCursorObject.mCursorType != CursorType.Shovel)
            {
                TRect trect2 = mChallenge.SlotMachineGetHandleRect();
                TRect trect3 = mChallenge.SlotMachineRect();
                int num = (int)(Constants.S * 50f);
                trect2.mX -= num;
                trect2.mWidth += num * 2;
                trect2.mY -= num;
                trect2.mHeight += num * 2;
                if ((trect2.Contains(x, y) || trect3.Contains(x, y)) && mChallenge.mChallengeState == ChallengeState.Normal && !HasLevelAwardDropped())
                {
                    theHitResult.mObjectType = GameObjectType.SlotMachineHandle;
                    return true;
                }
            }
            x = (int)(x * Constants.IS);
            y = (int)(y * Constants.IS);
            if (MouseHitTestPlant(x, y, out theHitResult, true))
            {
                return true;
            }
            x = (int)(x * Constants.S);
            y = (int)(y * Constants.S);
            if (mApp.IsScaryPotterLevel() && mChallenge.mChallengeState != ChallengeState.ScaryPotterMalleting && mApp.mGameScene == GameScenes.Playing && mApp.GetDialog(Dialogs.DIALOG_GAME_OVER) == null && mApp.GetDialog(Dialogs.DIALOG_CONTINUE) == null)
            {
                int theGridX = PixelToGridX((int)(x * Constants.IS), (int)(y * Constants.IS));
                int theGridY = PixelToGridY((int)(x * Constants.IS), (int)(y * Constants.IS));
                GridItem scaryPotAt = GetScaryPotAt(theGridX, theGridY);
                if (scaryPotAt != null)
                {
                    theHitResult.mObject = scaryPotAt;
                    theHitResult.mObjectType = GameObjectType.ScaryPot;
                    return true;
                }
            }
            if (mApp.IsSlotMachineLevel() && mCursorObject.mCursorType == CursorType.Shovel)
            {
                TRect trect4 = mChallenge.SlotMachineGetHandleRect();
                TRect trect5 = mChallenge.SlotMachineRect();
                int num2 = (int)(Constants.S * 50f);
                trect4.mX -= num2;
                trect4.mWidth += num2 * 2;
                trect4.mY -= num2;
                trect4.mHeight += num2 * 2;
                if ((trect4.Contains(x, y) || trect5.Contains(x, y)) && mChallenge.mChallengeState == ChallengeState.Normal && !HasLevelAwardDropped())
                {
                    theHitResult.mObjectType = GameObjectType.SlotMachineHandle;
                    return true;
                }
            }
            theHitResult.mObject = null;
            theHitResult.mObjectType = GameObjectType.None;
            return false;
        }

        public void MouseDownWithPlant(int x, int y, int theClickCount) 
        {
            if (theClickCount < 0)
            {
                RefreshSeedPacketFromCursor();
                mApp.PlayFoley(FoleyType.Drop);
                return;
            }
        }

        public void MouseUpWithPlant(int x, int y, int theClickCount)
        {
            if (mApp.IsIZombieLevel())
            {
                mChallenge.IZombieMouseDownWithZombie(x, y, theClickCount);
                return;
            }
            SeedType seedTypeInCursor = GetSeedTypeInCursor();
            int num = PlantingPixelToGridX((int)(x * Constants.IS), (int)(y * Constants.IS), seedTypeInCursor);
            int num2 = PlantingPixelToGridY((int)(x * Constants.IS), (int)(y * Constants.IS), seedTypeInCursor);
            if (num < 0 || num >= Constants.GRIDSIZEX || num2 < 0 || num2 >= Constants.MAX_GRIDSIZEY)
            {
                RefreshSeedPacketFromCursor();
                mApp.PlayFoley(FoleyType.Drop);
                return;
            }
            PlantingReason plantingReason = CanPlantAt(num, num2, seedTypeInCursor);
            if (plantingReason != PlantingReason.Ok)
            {
                if (plantingReason == PlantingReason.OnlyOnGraves)
                {
                    DisplayAdvice("[ADVICE_GRAVEBUSTERS_ON_GRAVES]", MessageStyle.HintFast, AdviceType.PlantGravebustersOnGraves);
                }
                else if (seedTypeInCursor == SeedType.Lilypad && plantingReason == PlantingReason.OnlyInPool)
                {
                    DisplayAdvice("[ADVICE_LILYPAD_ON_WATER]", MessageStyle.HintFast, AdviceType.PlantLilypadOnWater);
                }
                else if (seedTypeInCursor == SeedType.Tanglekelp && plantingReason == PlantingReason.OnlyInPool)
                {
                    DisplayAdvice("[ADVICE_TANGLEKELP_ON_WATER]", MessageStyle.HintFast, AdviceType.PlantTanglekelpOnWater);
                }
                else if (seedTypeInCursor == SeedType.Seashroom && plantingReason == PlantingReason.OnlyInPool)
                {
                    DisplayAdvice("[ADVICE_SEASHROOM_ON_WATER]", MessageStyle.HintFast, AdviceType.PlantSeashroomOnWater);
                }
                else if (plantingReason == PlantingReason.OnlyOnGround)
                {
                    DisplayAdvice("[ADVICE_POTATO_MINE_ON_LILY]", MessageStyle.HintFast, AdviceType.PlantPotatoMineOnLily);
                }
                else if (plantingReason == PlantingReason.NotPassedLine)
                {
                    DisplayAdvice("[ADVICE_NOT_PASSED_LINE]", MessageStyle.HintFast, AdviceType.PlantNotPassedLine);
                }
                else if (plantingReason == PlantingReason.NeedsUpgrade && seedTypeInCursor == SeedType.Gatlingpea)
                {
                    DisplayAdvice("[ADVICE_ONLY_ON_REPEATERS]", MessageStyle.HintFast, AdviceType.PlantOnlyOnRepeaters);
                }
                else if (plantingReason == PlantingReason.NeedsUpgrade && seedTypeInCursor == SeedType.Wintermelon)
                {
                    DisplayAdvice("[ADVICE_ONLY_ON_MELONPULT]", MessageStyle.HintFast, AdviceType.PlantOnlyOnMelonpult);
                }
                else if (plantingReason == PlantingReason.NeedsUpgrade && seedTypeInCursor == SeedType.Twinsunflower)
                {
                    DisplayAdvice("[ADVICE_ONLY_ON_SUNFLOWER]", MessageStyle.HintFast, AdviceType.PlantOnlyOnSunflower);
                }
                else if (plantingReason == PlantingReason.NeedsUpgrade && seedTypeInCursor == SeedType.Spikerock)
                {
                    DisplayAdvice("[ADVICE_ONLY_ON_SPIKEWEED]", MessageStyle.HintFast, AdviceType.PlantOnlyOnSpikeweed);
                }
                else if (plantingReason == PlantingReason.NeedsUpgrade && seedTypeInCursor == SeedType.Cobcannon)
                {
                    DisplayAdvice("[ADVICE_ONLY_ON_KERNELPULT]", MessageStyle.HintFast, AdviceType.PlantOnlyOnKernelpult);
                }
                else if (plantingReason == PlantingReason.NeedsUpgrade && seedTypeInCursor == SeedType.GoldMagnet)
                {
                    DisplayAdvice("[ADVICE_ONLY_ON_MAGNETSHROOM]", MessageStyle.HintFast, AdviceType.PlantOnlyOnMagnetshroom);
                }
                else if (plantingReason == PlantingReason.NeedsUpgrade && seedTypeInCursor == SeedType.Gloomshroom)
                {
                    DisplayAdvice("[ADVICE_ONLY_ON_FUMESHROOM]", MessageStyle.HintFast, AdviceType.PlantOnlyOnFumeshroom);
                }
                else if (plantingReason == PlantingReason.NeedsUpgrade && seedTypeInCursor == SeedType.Cattail)
                {
                    DisplayAdvice("[ADVICE_ONLY_ON_LILYPAD]", MessageStyle.HintFast, AdviceType.PlantOnlyOnLilypad);
                }
                else if (plantingReason == PlantingReason.NotOnArt)
                {
                    SeedType artChallengeSeed = mChallenge.GetArtChallengeSeed(num, num2);
                    string nameString = Plant.GetNameString(artChallengeSeed, SeedType.None);
                    string theAdvice = TodCommon.TodReplaceString("[ADVICE_WRONG_ART_TYPE]", "{SEED}", nameString);
                    DisplayAdvice(theAdvice, MessageStyle.HintFast, AdviceType.PlantWrongArtType);
                }
                else if (plantingReason == PlantingReason.NeedsPot)
                {
                    if (mApp.IsFirstTimeAdventureMode() && mLevel == 41)
                    {
                        DisplayAdvice("[ADVICE_PLANT_NEED_POT1]", MessageStyle.HintFast, AdviceType.PlantNeedPot);
                    }
                    else
                    {
                        DisplayAdvice("[ADVICE_PLANT_NEED_POT2]", MessageStyle.HintFast, AdviceType.PlantNeedPot);
                    }
                }
                else if (plantingReason == PlantingReason.NotOnGrave)
                {
                    DisplayAdvice("[ADVICE_PLANT_NOT_ON_GRAVE]", MessageStyle.HintFast, AdviceType.PlantNotOnGrave);
                }
                else if (plantingReason == PlantingReason.NotOnCrater)
                {
                    if (IsPoolSquare(num, num2))
                    {
                        DisplayAdvice("[ADVICE_CANT_PLANT_THERE]", MessageStyle.HintFast, AdviceType.CantPlantThere);
                    }
                    else
                    {
                        DisplayAdvice("[ADVICE_PLANT_NOT_ON_CRATER]", MessageStyle.HintFast, AdviceType.PlantNotOnCrater);
                    }
                }
                else if (plantingReason == PlantingReason.NotOnWater)
                {
                    if (mApp.mGameMode == GameMode.ChallengeZenGarden && mApp.mZenGarden.mGardenType == GardenType.Aquarium)
                    {
                        DisplayAdvice("[ZEN_ONLY_AQUATIC_PLANTS]", MessageStyle.HintTallFast, AdviceType.None);
                    }
                    else if (seedTypeInCursor == SeedType.Potatomine)
                    {
                        DisplayAdvice("[ADVICE_CANT_PLANT_THERE]", MessageStyle.HintFast, AdviceType.CantPlantThere);
                    }
                    else
                    {
                        DisplayAdvice("[ADVICE_PLANT_NOT_ON_WATER]", MessageStyle.HintFast, AdviceType.PlantNotOnWater);
                    }
                }
                else if (plantingReason == PlantingReason.NeedsGround)
                {
                    DisplayAdvice("[ADVICE_PLANTING_NEEDS_GROUND]", MessageStyle.HintFast, AdviceType.PlantingNeedsGround);
                }
                else if (plantingReason == PlantingReason.NeedsSleeping)
                {
                    DisplayAdvice("[ADVICE_PLANTING_NEED_SLEEPING]", MessageStyle.HintFast, AdviceType.PlantingNeedSleeping);
                }
                if (mCursorObject.mCursorType == CursorType.PlantFromGlove || mApp.IsWhackAZombieLevel())
                {
                    RefreshSeedPacketFromCursor();
                    mApp.PlayFoley(FoleyType.Drop);
                }
                return;
            }
            ClearAdvice(AdviceType.PlantingNeedSleeping);
            ClearAdvice(AdviceType.CantPlantThere);
            ClearAdvice(AdviceType.PlantingNeedsGround);
            ClearAdvice(AdviceType.PlantNotOnWater);
            ClearAdvice(AdviceType.PlantNotOnCrater);
            ClearAdvice(AdviceType.PlantNotOnGrave);
            ClearAdvice(AdviceType.PlantNeedPot);
            ClearAdvice(AdviceType.PlantWrongArtType);
            ClearAdvice(AdviceType.PlantOnlyOnLilypad);
            ClearAdvice(AdviceType.PlantOnlyOnMagnetshroom);
            ClearAdvice(AdviceType.PlantOnlyOnFumeshroom);
            ClearAdvice(AdviceType.PlantOnlyOnKernelpult);
            ClearAdvice(AdviceType.PlantOnlyOnSunflower);
            ClearAdvice(AdviceType.PlantOnlyOnSpikeweed);
            ClearAdvice(AdviceType.PlantOnlyOnMelonpult);
            ClearAdvice(AdviceType.PlantOnlyOnRepeaters);
            ClearAdvice(AdviceType.PlantNotPassedLine);
            ClearAdvice(AdviceType.PlantGravebustersOnGraves);
            ClearAdvice(AdviceType.PlantLilypadOnWater);
            ClearAdvice(AdviceType.PlantTanglekelpOnWater);
            ClearAdvice(AdviceType.PlantSeashroomOnWater);
            ClearAdvice(AdviceType.PlantPotatoMineOnLily);
            ClearAdvice(AdviceType.SurviveFlags);
            if (!mApp.mEasyPlantingCheat && mCursorObject.mCursorType == CursorType.PlantFromBank && !HasConveyorBeltSeedBank())
            {
                int currentPlantCost = GetCurrentPlantCost(mCursorObject.mType, mCursorObject.mImitaterType);
                if (!TakeSunMoney(currentPlantCost))
                {
                    return;
                }
            }
            Plant topPlantAt = GetTopPlantAt(num, num2, TopPlant.OnlyNormalPosition);
            bool flag = false;
            int aWakeUpCounter = 0;
            if (topPlantAt != null && topPlantAt.IsUpgradableTo(seedTypeInCursor))
            {
                if (seedTypeInCursor == SeedType.Gloomshroom)
                {
                    if (!topPlantAt.mIsAsleep)
                    {
                        flag = true;
                    }
                    else
                    {
                        aWakeUpCounter = topPlantAt.mWakeUpCounter;
                    }
                }
                topPlantAt.Die();
            }
            if ((seedTypeInCursor == SeedType.Wallnut || seedTypeInCursor == SeedType.Tallnut) && topPlantAt != null && topPlantAt.mSeedType == seedTypeInCursor)
            {
                topPlantAt.Die();
            }
            if (seedTypeInCursor == SeedType.Pumpkinshell)
            {
                Plant topPlantAt2 = GetTopPlantAt(num, num2, TopPlant.OnlyPumpkin);
                if (topPlantAt2 != null && topPlantAt2.mSeedType == seedTypeInCursor)
                {
                    topPlantAt2.Die();
                }
            }
            if (seedTypeInCursor == SeedType.Cobcannon)
            {
                Plant topPlantAt3 = GetTopPlantAt(num + 1, num2, TopPlant.OnlyNormalPosition);
                if (topPlantAt3 != null)
                {
                    topPlantAt3.Die();
                }
            }
            if (seedTypeInCursor == SeedType.Cattail)
            {
                PlantsOnLawn plantsOnLawn = default(PlantsOnLawn);
                GetPlantsOnLawn(num, num2, ref plantsOnLawn);
                if (plantsOnLawn.mUnderPlant != null)
                {
                    plantsOnLawn.mUnderPlant.Die();
                }
                if (plantsOnLawn.mNormalPlant != null)
                {
                    plantsOnLawn.mNormalPlant.Die();
                }
            }
            if (mCursorObject.mCursorType == CursorType.PlantFromGlove)
            {
                Plant aGlovePlantID = mCursorObject.mGlovePlantID;
                aGlovePlantID.mGloveGrabbed = false;
                mApp.mZenGarden.MovePlant(aGlovePlantID, num, num2);
            }
            else if (mCursorObject.mCursorType == CursorType.PlantFromWheelBarrow)
            {
                mApp.mZenGarden.MouseDownWithFullWheelBarrow(x, y);
            }
            else if (mCursorObject.mCursorType == CursorType.PlantFromUsableCoin)
            {
                AddPlant(num, num2, mCursorObject.mType, mCursorObject.mImitaterType);
                Coin coin = mCoins[mCoins.IndexOf(mCursorObject.mCoinID)];
                mCursorObject.mCoinID = null;
                coin.Die();
            }
            else if (mCursorObject.mCursorType == CursorType.PlantFromBank)
            {
                Plant plant = AddPlant(num, num2, mCursorObject.mType, mCursorObject.mImitaterType);
                if (flag)
                {
                    plant.SetSleeping(false);
                }
                plant.mWakeUpCounter = aWakeUpCounter;
                Debug.ASSERT(mCursorObject.mSeedBankIndex >= 0 && mCursorObject.mSeedBankIndex < mSeedBank.mNumPackets);
                SeedPacket seedPacket = mSeedBank.mSeedPackets[mCursorObject.mSeedBankIndex];
                seedPacket.WasPlanted();
            }
            else
            {
                Debug.ASSERT(false);
            }
            if (mApp.mGameMode == GameMode.ChallengeColumn)
            {
                for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
                {
                    if (i != num2 && CanPlantAt(num, i, seedTypeInCursor) == PlantingReason.Ok)
                    {
                        if (seedTypeInCursor == SeedType.Wallnut || seedTypeInCursor == SeedType.Tallnut)
                        {
                            Plant topPlantAt4 = GetTopPlantAt(num, i, TopPlant.OnlyNormalPosition);
                            if (topPlantAt4 != null && topPlantAt4.mSeedType == seedTypeInCursor)
                            {
                                topPlantAt4.Die();
                            }
                        }
                        if (seedTypeInCursor == SeedType.Pumpkinshell)
                        {
                            Plant topPlantAt5 = GetTopPlantAt(num, i, TopPlant.OnlyPumpkin);
                            if (topPlantAt5 != null && topPlantAt5.mSeedType == seedTypeInCursor)
                            {
                                topPlantAt5.Die();
                            }
                        }
                        AddPlant(num, i, mCursorObject.mType, mCursorObject.mImitaterType);
                    }
                }
            }
            if (mTutorialState == TutorialState.Level1PlantPeashooter)
            {
                int count = mPlants.Count;
                if (count >= 2)
                {
                    SetTutorialState(TutorialState.Level1Completed);
                }
                else
                {
                    SetTutorialState(TutorialState.Level1RefreshPeashooter);
                }
            }
            if (mTutorialState == TutorialState.Level2PlantSunflower)
            {
                int num3 = CountSunFlowers();
                if (seedTypeInCursor == SeedType.Sunflower && num3 == 2)
                {
                    DisplayAdvice("[ADVICE_MORE_SUNFLOWERS]", MessageStyle.TutorialLevel2, AdviceType.None);
                }
                if (num3 >= 3)
                {
                    SetTutorialState(TutorialState.Level2Completed);
                }
                else if (!mSeedBank.mSeedPackets[1].CanPickUp())
                {
                    SetTutorialState(TutorialState.Level2RefreshSunflower);
                }
                else
                {
                    SetTutorialState(TutorialState.Level2PickUpSunflower);
                }
            }
            if (mTutorialState == TutorialState.MoresunPlantSunflower)
            {
                int num4 = CountSunFlowers();
                if (num4 >= 3)
                {
                    SetTutorialState(TutorialState.MoresunCompleted);
                    DisplayAdvice("[ADVICE_PLANT_SUNFLOWER5]", MessageStyle.TutorialLater, AdviceType.PlantSunflower5);
                    mTutorialTimer = -1;
                }
                else if (!mSeedBank.mSeedPackets[1].CanPickUp())
                {
                    SetTutorialState(TutorialState.MoresunRefreshSunflower);
                }
                else
                {
                    SetTutorialState(TutorialState.MoresunPickUpSunflower);
                }
            }
            if (mApp.IsWallnutBowlingLevel())
            {
                mApp.PlaySample(Resources.SOUND_BOWLING);
            }
            ClearCursor();
        }

        public void MouseDownWithTool(int x, int y, int theClickCount, CursorType theCursorType, bool posScaled)
        {
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                if (posScaled)
                {
                    x = (int)(Constants.S * x);
                    y = (int)(Constants.S * y);
                    posScaled = false;
                }
                if (y < Constants.ZEN_YMIN)
                {
                    ClearCursor();
                    return;
                }
            }
            if (!posScaled)
            {
                x = (int)(x * Constants.IS);
                y = (int)(y * Constants.IS);
            }
            if (theClickCount < 0)
            {
                ClearCursor();
                mApp.PlayFoley(FoleyType.Drop);
                return;
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                mApp.mZenGarden.MouseDownWithTool(x, y, theCursorType);
                return;
            }
            if (mApp.mGameMode == GameMode.TreeOfWisdom)
            {
                mChallenge.TreeOfWisdomTool(x, y);
                return;
            }
            HitResult hitResult = ToolHitTest(x, y, true);
            Plant plant = null;
            if (hitResult.mObjectType == GameObjectType.Plant)
            {
                plant = (Plant)hitResult.mObject;
            }
            if (plant != null)
            {
                if (theCursorType == CursorType.Shovel)
                {
                    mApp.PlayFoley(FoleyType.UseShovel);
                    mPlantsShoveled++;
                    plant.Die();
                    if (plant.mSeedType == SeedType.Cattail && GetTopPlantAt(plant.mPlantCol, plant.mRow, TopPlant.OnlyPumpkin) != null)
                    {
                        NewPlant(plant.mPlantCol, plant.mRow, SeedType.Lilypad, SeedType.None);
                    }
                    if (mTutorialState == TutorialState.ShovelDig || mTutorialState == TutorialState.ShovelKeepDigging)
                    {
                        if (CountPlantByType(SeedType.Peashooter) == 0)
                        {
                            SetTutorialState(TutorialState.ShovelCompleted);
                        }
                        else
                        {
                            SetTutorialState(TutorialState.ShovelKeepDigging);
                        }
                    }
                }
                ClearCursor();
                return;
            }
            HitResult hitResult2;
            MouseHitTest(x, y, out hitResult2, true);
            if (hitResult2.mObjectType == GameObjectType.Coin)
            {
                return;
            }
            mApp.PlayFoley(FoleyType.Drop);
            ClearCursor();
        }

        public bool CanInteractWithBoardButtons()
        {
            return !mPaused && mApp.GetDialogCount() <= 0 && mBoardFadeOutCounter < 0 && mChallenge.mChallengeState != ChallengeState.ZenFading && (mApp.mGameMode == GameMode.Upsell || mApp.mCrazyDaveState == CrazyDaveState.Off);
        }

        public void DrawProgressMeter(Graphics g)
        {
            if (!HasProgressMeter())
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
            int num2 = TodCommon.TodAnimateCurve(0, 150, mProgressMeterWidth, 0, Constants.UIProgressMeterBarEnd, TodCurves.Linear);
            TRect theSrcRect = new TRect(celWidth - num2 - 7, celHeight, num2, celHeight);
            TRect theDestRect = new TRect(num + celWidth - num2 - 7, y, num2, celHeight);
            g.DrawImage(AtlasResources.IMAGE_FLAGMETER, theDestRect, theSrcRect);
            SexyColor theColor = new SexyColor(224, 187, 98);
            if (mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                if (progressMeterStringValue != mChallenge.mChallengeScore)
                {
                    progressMeterString = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_MATCHES]"), mChallenge.mChallengeScore, 75, TodStringFile.TodStringTranslate("[MATCHES]"));
                    progressMeterStringValue = mChallenge.mChallengeScore;
                }
                TodCommon.TodDrawString(g, progressMeterString, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, DrawStringJustification.Center);
            }
            else if (mApp.IsSquirrelLevel())
            {
                string theText = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_SQUIRRELS]"), mChallenge.mChallengeScore, 7, TodStringFile.TodStringTranslate("[SQUIRRELS]"));
                TodCommon.TodDrawString(g, theText, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, DrawStringJustification.Center);
            }
            else if (mApp.mGameMode == GameMode.ChallengeSlotMachine)
            {
                int num3 = TodCommon.ClampInt(mSunMoney, 0, 2000);
                if (progressMeterStringValue != num3)
                {
                    progressMeterString = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_SUN_SLOT_MACHINE]"), num3, 2000, TodStringFile.TodStringTranslate("[SUN]"));
                    progressMeterStringValue = num3;
                }
                TodCommon.TodDrawString(g, progressMeterString, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, celWidth - 10, DrawStringJustification.Center);
            }
            else if (mApp.mGameMode == GameMode.ChallengeZombiquarium)
            {
                int num4 = TodCommon.ClampInt(mSunMoney, 0, 1000);
                string theText2 = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_SUN_ZOMBIQUARIUM]"), num4, 1000, TodStringFile.TodStringTranslate("[SUN]"));
                TodCommon.TodDrawString(g, theText2, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, DrawStringJustification.Center);
            }
            else if (mApp.IsIZombieLevel())
            {
                if (progressMeterStringValue != mChallenge.mChallengeScore)
                {
                    progressMeterString = Common.StrFormat_(TodStringFile.TodStringTranslate("[PROGRESS_METER_BRAINS]"), mChallenge.mChallengeScore, 5, TodStringFile.TodStringTranslate("[BRAINS]"));
                    progressMeterStringValue = mChallenge.mChallengeScore;
                }
                Resources.FONT_DWARVENTODCRAFT12.characterOffsetMagic = 2;
                TodCommon.TodDrawString(g, progressMeterString, thePosX, board_ProgressBarText_Pos, Resources.FONT_DWARVENTODCRAFT12, theColor, DrawStringJustification.Center);
                Resources.FONT_DWARVENTODCRAFT12.characterOffsetMagic = 0;
            }
            else if (ProgressMeterHasFlags())
            {
                int numWavesPerFlag = GetNumWavesPerFlag();
                for (int i = 1; i <= mNumWaves / numWavesPerFlag; i++)
                {
                    int theTimeAge = i * numWavesPerFlag;
                    int num5 = 0;
                    int thePositionEnd = num + 6;
                    int thePositionStart = num + celWidth - 10;
                    int theX = TodCommon.TodAnimateCurve(0, mNumWaves, theTimeAge, thePositionStart, thePositionEnd, TodCurves.Linear);
                    g.DrawImageCel(AtlasResources.IMAGE_FLAGMETERPARTS, theX, y - 4, 1, 0);
                    g.DrawImageCel(AtlasResources.IMAGE_FLAGMETERPARTS, theX, y - num5 - 3, 2, 0);
                }
            }
            if (mApp.mGameMode != GameMode.ChallengeBeghouled && mApp.mGameMode != GameMode.ChallengeBeghouledTwist && !mApp.IsSquirrelLevel() && !mApp.IsSlotMachineLevel() && !mApp.IsIZombieLevel() && !mApp.IsFinalBossLevel())
            {
                int num6 = TodCommon.TodAnimateCurve(0, 150, mProgressMeterWidth, 0, Constants.UIProgressMeterHeadEnd, TodCurves.Linear);
                g.DrawImageCel(AtlasResources.IMAGE_FLAGMETERPARTS, num + celWidth - num6 - 20, y - 3, 0, 0);
            }
        }

        public Plant GetTopPlantAt(int theGridX, int theGridY, TopPlant thePriority)
        {
            if (theGridX < 0 || theGridX >= Constants.GRIDSIZEX || theGridY < 0 || theGridY >= Constants.MAX_GRIDSIZEY)
            {
                return null;
            }
            if (mApp.IsWallnutBowlingLevel() && !mCutScene.IsInShovelTutorial())
            {
                return null;
            }
            PlantsOnLawn plantsOnLawn = default(PlantsOnLawn);
            GetPlantsOnLawn(theGridX, theGridY, ref plantsOnLawn);
            if (thePriority == TopPlant.OnlyFlying)
            {
                return plantsOnLawn.mFlyingPlant;
            }
            if (thePriority == TopPlant.OnlyUnderPlant)
            {
                return plantsOnLawn.mUnderPlant;
            }
            if (thePriority == TopPlant.OnlyPumpkin)
            {
                return plantsOnLawn.mPumpkinPlant;
            }
            if (thePriority == TopPlant.OnlyNormalPosition)
            {
                return plantsOnLawn.mNormalPlant;
            }
            if (thePriority == TopPlant.EatingOrder)
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
            else if (thePriority == TopPlant.DiggingOrder)
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
            else if (thePriority == TopPlant.BungeeOrder)
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
            else if (thePriority == TopPlant.CatapultOrder || thePriority == TopPlant.Any)
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
                if (thePriority != TopPlant.ZenToolOrder)
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
            if (mApp.IsWallnutBowlingLevel() && !mCutScene.IsInShovelTutorial())
            {
                return;
            }
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead)
                {
                    SeedType seedType = plant.mSeedType;
                    if (plant.mSeedType == SeedType.Imitater && plant.mImitaterType != SeedType.None)
                    {
                        seedType = plant.mImitaterType;
                    }
                    if (seedType == SeedType.Cobcannon)
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
                        else if (seedType == SeedType.Flowerpot)
                        {
                            Debug.ASSERT(thePlantOnLawn.mUnderPlant == null);
                            thePlantOnLawn.mUnderPlant = plant;
                        }
                        else if (seedType == SeedType.Lilypad)
                        {
                            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
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
                        else if (seedType == SeedType.Pumpkinshell)
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
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead && plant.MakesSun())
                {
                    num++;
                }
            }
            return num;
        }

        public int GetSeedPacketPositionY(int theIndex)
        {
            int aNumPackets = mSeedBank.mNumPackets;
            int num = Constants.SMALL_SEEDPACKET_HEIGHT;
            if (aNumPackets <= 7)
            {
                num += (int)Constants.InvertAndScale(8f);
            }
            else if (aNumPackets == 8)
            {
                num += (int)Constants.InvertAndScale(5f);
            }
            return theIndex * num;
        }

        public void AddGraveStones(int theGridX, int theCount)
        {
            if (!doAddGraveStones)
            {
                return;
            }
            Debug.ASSERT(theCount <= Constants.MAX_GRIDSIZEY);
            int i = 0;
            while (i < theCount)
            {
                int theGridY = RandomNumbers.NextNumber(Constants.MAX_GRIDSIZEY);
                if (CanAddGraveStoneAt(theGridX, theGridY))
                {
                    AddAGraveStone(theGridX, theGridY);
                    i++;
                }
            }
        }

        public int GetGraveStoneCount()
        {
            int num = 0;
            int num2 = -1;
            GridItem gridItem = null;
            while (IterateGridItems(ref gridItem, ref num2))
            {
                if (gridItem.mGridItemType == GridItemType.Gravestone)
                {
                    num++;
                }
            }
            return num;
        }

        public void ZombiesWon(Zombie aZombie)
        {
            if (mApp.mGameScene == GameScenes.ZombiesWon)
            {
                return;
            }
            ClearAdvice(AdviceType.None);
            ClearCursor();
            if (mNextSurvivalStageCounter > 0)
            {
                mNextSurvivalStageCounter = 0;
            }
            mApp.mBoardResult = BoardResult.Lost;
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && zombie != aZombie)
                {
                    if (zombie.GetZombieRect().mX < -50f || zombie.mZombiePhase == ZombiePhase.RisingFromGrave || zombie.mZombiePhase == ZombiePhase.DancerRising)
                    {
                        zombie.DieNoLoot(false);
                    }
                    if ((zombie.mZombieType == ZombieType.Gargantuar || zombie.mZombieType == ZombieType.RedeyeGargantuar) && zombie.IsDeadOrDying() && zombie.mPosX < 140f)
                    {
                        zombie.DieNoLoot(false);
                    }
                }
            }
            string theMessage = string.Empty;
            bool flag = true;
            if (mApp.mGameMode == GameMode.ChallengeZombiquarium)
            {
                theMessage = "[ZOMBIQUARIUM_DEATH_MESSAGE]";
                flag = false;
            }
            else if (mApp.mGameMode == GameMode.ChallengeLastStand)
            {
                int survivalFlagsCompleted = GetSurvivalFlagsCompleted();
                string theStringToSubstitute = mApp.Pluralize(survivalFlagsCompleted, "[ONE_FLAG]", "[COUNT_FLAGS]");
                theMessage = TodCommon.TodReplaceString("[LAST_STAND_DEATH_MESSAGE]", "{FLAGS}", theStringToSubstitute);
                flag = false;
            }
            else if (mApp.IsEndlessScaryPotter(mApp.mGameMode) || mApp.IsEndlessIZombie(mApp.mGameMode))
            {
                int aSurvivalStage = mChallenge.mSurvivalStage;
                if (mApp.IsEndlessScaryPotter(mApp.mGameMode))
                {
                    mApp.mPlayerInfo.mVasebreakerScore = Math.Max(mApp.mPlayerInfo.mVasebreakerScore, aSurvivalStage);
                    LeaderBoardComm.RecordResult(LeaderboardGameMode.Vasebreaker, (int)mApp.mPlayerInfo.mVasebreakerScore);
                }
                else if (mApp.IsEndlessIZombie(mApp.mGameMode))
                {
                    mApp.mPlayerInfo.mIZombieScore = Math.Max(mApp.mPlayerInfo.mIZombieScore, aSurvivalStage);
                    LeaderBoardComm.RecordResult(LeaderboardGameMode.IZombie, (int)mApp.mPlayerInfo.mIZombieScore);
                }
                theMessage = TodCommon.TodReplaceNumberString("[ENDLESS_PUZZLE_DEATH_MESSAGE]", "{STREAK}", aSurvivalStage);
                flag = false;
            }
            else if (mApp.IsIZombieLevel())
            {
                theMessage = "[I_ZOMBIE_DEATH_MESSAGE]";
                flag = false;
            }
            if (flag)
            {
                mApp.mGameScene = GameScenes.ZombiesWon;
                aZombie.WalkIntoHouse();
                ClearAdvice(AdviceType.None);
                mCutScene.StartZombiesWon();
                FreezeEffectsForCutscene(true);
                TutorialArrowRemove();
                UpdateCursor();
                return;
            }
            GameOverDialog theDialog = new GameOverDialog(theMessage, true);
            mApp.AddDialog(17, theDialog);
            mApp.mMusic.StopAllMusic();
            StopAllZombieSounds();
            mApp.PlaySample(Resources.SOUND_LOSEMUSIC);
            ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.ZombiesWon, true);
            int num = Constants.BOARD_EXTRA_ROOM / 2;
            Reanimation reanimation = mApp.AddReanimation((float)(-(float)Constants.BOARD_OFFSET + num + Constants.Board_Offset_AspectRatio_Correction), 0f, 900000, ReanimationType.ZombiesWon);
            reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
            ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(Reanimation.ReanimTrackId_fullscreen);
            trackInstanceByName.mTrackColor = SexyColor.Black;
            reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_screen);
        }

        public void DrawLevel(Graphics g)
        {
            if (mLevelFadeCount <= 0)
            {
                return;
            }
            if (mApp.IsAdventureMode())
            {
                if (levelStrVal != mLevel)
                {
                    mLevelStr = TodStringFile.TodStringTranslate("[LEVEL]") + " " + mApp.GetStageString(mLevel);
                    levelStrVal = mLevel;
                }
            }
            else if (mApp.IsSurvivalMode() || mApp.mGameMode == GameMode.ChallengeLastStand)
            {
                mApp.GetCurrentChallengeIndex();
                int survivalFlagsCompleted = GetSurvivalFlagsCompleted();
                if (survivalFlagsCompleted > 0)
                {
                    string theStringToSubstitute = mApp.Pluralize(survivalFlagsCompleted, "[ONE_FLAG]", "[COUNT_FLAGS]");
                    string text;
                    if (survivalFlagsCompleted == 1)
                    {
                        text = TodCommon.TodReplaceString("[FLAGS_COMPLETED]", "{FLAGS}", theStringToSubstitute);
                    }
                    else
                    {
                        text = TodCommon.TodReplaceString("[FLAGS_COMPLETED_PLURAL]", "{FLAGS}", theStringToSubstitute);
                    }
                    mLevelStr = TodStringFile.TodStringTranslate(mChallenge.mName) + " - " + text;
                }
                else
                {
                    mLevelStr = mChallenge.mName;
                }
            }
            else if (mApp.IsEndlessScaryPotter(mApp.mGameMode) || mApp.IsEndlessIZombie(mApp.mGameMode))
            {
                mApp.GetCurrentChallengeIndex();
                int num = mChallenge.mSurvivalStage;
                if (mNextSurvivalStageCounter > 0)
                {
                    num++;
                }
                if (num > 0)
                {
                    string text2 = TodCommon.TodReplaceNumberString("[ENDLESS_STREAK]", "{STREAK}", num);
                    mLevelStr = TodStringFile.TodStringTranslate(mChallenge.mName) + " - " + text2;
                }
                else
                {
                    mLevelStr = mChallenge.mName;
                }
            }
            else
            {
                mLevelStr = mChallenge.mName;
            }
            int thePosX = Constants.UILevelPosition.X - Constants.Board_Offset_AspectRatio_Correction;
            int num2 = Constants.UILevelPosition.Y;
            if (mChallenge.mChallengeState == ChallengeState.ZenFading)
            {
                num2 += TodCommon.TodAnimateCurve(50, 0, mChallenge.mChallengeStateCounter, 0, 50, TodCurves.EaseInOut);
            }
            int theAlpha = TodCommon.ClampInt(255 * mLevelFadeCount / 15, 0, 255);
            TodCommon.TodDrawString(g, mLevelStr, thePosX, num2, Resources.FONT_HOUSEOFTERROR16, new SexyColor(224, 187, 98, theAlpha), DrawStringJustification.Right);
        }

        public void DrawShovel(Graphics g)
        {
            if (mApp.mGameMode != GameMode.ChallengeZenGarden && mApp.mGameMode != GameMode.TreeOfWisdom && mShowShovel)
            {
                TRect shovelButtonRect = GetShovelButtonRect();
                g.DrawImage(AtlasResources.IMAGE_SHOVELBANK, shovelButtonRect.mX, shovelButtonRect.mY);
                if (mCursorObject.mCursorType != CursorType.Shovel)
                {
                    if (mChallenge.mChallengeState == (ChallengeState)15)
                    {
                        SexyColor flashingColor = TodCommon.GetFlashingColor(mMainCounter, 75);
                        g.SetColorizeImages(true);
                        g.SetColor(flashingColor);
                    }
                    g.DrawImage(AtlasResources.IMAGE_TINY_SHOVEL, shovelButtonRect.mX + 2, shovelButtonRect.mY + 3);
                    g.SetColorizeImages(false);
                }
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom)
            {
                DrawZenButtons(g);
            }
        }

        public void UpdateZombieSpawning()
        {
            if (mApp.mGameMode == GameMode.Upsell || mApp.mGameMode == GameMode.Intro)
            {
                return;
            }
            if (mFinalWaveSoundCounter > 0)
            {
                mFinalWaveSoundCounter -= 3;
                if (mFinalWaveSoundCounter >= 0 && mFinalWaveSoundCounter < 3)
                {
                    mApp.PlaySample(Resources.SOUND_FINALWAVE);
                }
            }
            if (mTutorialState == TutorialState.Level1PickUpPeashooter || mTutorialState == TutorialState.Level1PlantPeashooter || mTutorialState == TutorialState.Level1RefreshPeashooter || mTutorialState == TutorialState.SlotMachinePull)
            {
                return;
            }
            if (HasLevelAwardDropped())
            {
                return;
            }
            if (mRiseFromGraveCounter > 0)
            {
                mRiseFromGraveCounter -= 3;
                if (mRiseFromGraveCounter >= 0 && mRiseFromGraveCounter < 3)
                {
                    SpawnZombiesFromGraves();
                }
            }
            if (mHugeWaveCountDown > 0)
            {
                mHugeWaveCountDown -= 3;
                if (mHugeWaveCountDown >= 0 && mHugeWaveCountDown < 3)
                {
                    ClearAdvice(AdviceType.HugeWave);
                    NextWaveComing();
                    mZombieCountDown = 3;
                }
                else
                {
                    if (mHugeWaveCountDown < 723 || mHugeWaveCountDown >= 726)
                    {
                        if (mApp.mMusic.mCurMusicTune == MusicTune.DayGrasswalk || mApp.mMusic.mCurMusicTune == MusicTune.PoolWaterygraves || mApp.mMusic.mCurMusicTune == MusicTune.FogRigormormist || mApp.mMusic.mCurMusicTune == MusicTune.RoofGrazetheroof)
                        {
                            if (mHugeWaveCountDown == 399)
                            {
                                return;
                            }
                        }
                        else if (mApp.mMusic.mCurMusicTune == MusicTune.NightMoongrains)
                        {
                            int num = mHugeWaveCountDown;
                        }
                        return;
                    }
                    mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
                }
            }
            if (mChallenge.UpdateZombieSpawning())
            {
                return;
            }
            if (mCurrentWave == mNumWaves)
            {
                if (IsFinalSurvivalStage())
                {
                    return;
                }
                if (mApp.mGameMode == GameMode.ChallengeLastStand)
                {
                    return;
                }
                if (!mApp.IsSurvivalMode() && !mApp.IsContinuousChallenge())
                {
                    return;
                }
            }
            mZombieCountDown -= 3;
            if (mCurrentWave == mNumWaves && mApp.IsSurvivalMode())
            {
                if (mZombieCountDown >= 0 && mZombieCountDown < 3)
                {
                    FadeOutLevel();
                }
                return;
            }
            int num2 = mZombieCountDownStart - mZombieCountDown;
            if (mZombieCountDown > 5 && num2 > 400)
            {
                int num3 = TotalZombiesHealthInWave(mCurrentWave - 1);
                if (num3 <= mZombieHealthToNextWave && mZombieCountDown > 201)
                {
                    mZombieCountDown = 201;
                }
            }
            if (mZombieCountDown >= 5 && mZombieCountDown < 8)
            {
                if (IsFlagWave(mCurrentWave))
                {
                    ClearAdviceImmediately();
                    DisplayAdviceAgain("[ADVICE_HUGE_WAVE]", MessageStyle.HugeWave, AdviceType.HugeWave);
                    mHugeWaveCountDown = 750;
                    return;
                }
                NextWaveComing();
            }
            if (mZombieCountDown < 0 || mZombieCountDown >= 3)
            {
                return;
            }
            SpawnZombieWave();
            mZombieHealthWaveStart = TotalZombiesHealthInWave(mCurrentWave - 1);
            bool flag = mApp.IsWallnutBowlingLevel() || mApp.mGameMode == GameMode.ChallengeLastStand;
            if (mCurrentWave == mNumWaves && mApp.IsSurvivalMode())
            {
                mZombieHealthToNextWave = 0;
                mZombieCountDown = 5499;
            }
            else if (IsFlagWave(mCurrentWave) && !flag)
            {
                mZombieHealthToNextWave = 0;
                mZombieCountDown = 4500;
            }
            else
            {
                mZombieHealthToNextWave = (int)(TodCommon.RandRangeFloat(0.5f, 0.65f) * mZombieHealthWaveStart);
                if (mApp.IsLittleTroubleLevel() || mApp.mGameMode == GameMode.ChallengeColumn || mApp.mGameMode == GameMode.ChallengeLastStand)
                {
                    mZombieCountDown = 750;
                }
                else
                {
                    mZombieCountDown = 2500 + RandomNumbers.NextNumber(600);
                }
            }
            mZombieCountDownStart = mZombieCountDown;
        }

        public void UpdateSunSpawning()
        {
            if (StageIsNight())
            {
                return;
            }
            if (HasLevelAwardDropped())
            {
                return;
            }
            if (mApp.mGameMode == GameMode.ChallengeRainingSeeds || mApp.mGameMode == GameMode.ChallengeIce || mApp.mGameMode == GameMode.Upsell || mApp.mGameMode == GameMode.Intro || mApp.mGameMode == GameMode.ChallengeZombiquarium || mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom || mApp.mGameMode == GameMode.ChallengeLastStand || mApp.IsIZombieLevel() || mApp.IsScaryPotterLevel() || mApp.IsSquirrelLevel() || HasConveyorBeltSeedBank())
            {
                return;
            }
            if (mTutorialState == TutorialState.SlotMachinePull)
            {
                return;
            }
            if ((mTutorialState == TutorialState.Level1PickUpPeashooter || mTutorialState == TutorialState.Level1PlantPeashooter) && mPlants.Count == 0)
            {
                return;
            }
            mSunCountDown -= 3;
            if (mSunCountDown > 0)
            {
                return;
            }
            int theX = Constants.LAWN_XMIN + RandomNumbers.NextNumber(Constants.Board_SunCoinRange);
            mNumSunsFallen++;
            mSunCountDown = Math.Min(950, 425 + mNumSunsFallen * 10) + RandomNumbers.NextNumber(275);
            CoinType theCoinType = CoinType.Sun;
            if (mApp.mGameMode == GameMode.ChallengeSunnyDay)
            {
                theCoinType = CoinType.Largesun;
            }
            AddCoin(theX, 60, theCoinType, CoinMotion.FromSky);
        }

        public void ClearAdvice(AdviceType theHelpIndex)
        {
            if (theHelpIndex != AdviceType.None && theHelpIndex != mHelpIndex)
            {
                return;
            }
            mAdvice.ClearLabel();
            mHelpIndex = AdviceType.None;
        }

        public bool RowCanHaveZombieType(int theRow, ZombieType theZombieType)
        {
            if (!RowCanHaveZombies(theRow))
            {
                return false;
            }
            if (mApp.mGameMode == GameMode.ChallengeResodded && mPlantRow[theRow] == PlantRowType.Dirt && mCurrentWave < 5)
            {
                return false;
            }
            if (mPlantRow[theRow] == PlantRowType.Pool && !Zombie.ZombieTypeCanGoInPool(theZombieType))
            {
                return false;
            }
            if (mPlantRow[theRow] == PlantRowType.HighGround && !Zombie.ZombieTypeCanGoOnHighGround(theZombieType))
            {
                return false;
            }
            int num = mCurrentWave;
            if (mApp.mGameMode == GameMode.ChallengeLastStand)
            {
                num += mChallenge.mSurvivalStage * GetNumWavesPerSurvivalStage();
            }
            return (mPlantRow[theRow] != PlantRowType.Pool || num >= 5 || Board.IsZombieTypePoolOnly(theZombieType)) && (mPlantRow[theRow] == PlantRowType.Pool || !Board.IsZombieTypePoolOnly(theZombieType)) && (theZombieType != ZombieType.Bobsled || mIceTimer[theRow] > 0) && (theRow != 0 || mApp.IsSurvivalEndless(mApp.mGameMode) || (theZombieType != ZombieType.Gargantuar && theZombieType != ZombieType.RedeyeGargantuar)) && (theZombieType != ZombieType.Dancer || StageHasPool() || (RowCanHaveZombies(theRow - 1) && RowCanHaveZombies(theRow + 1)));
        }

        public int NumberZombiesInWave(int theWaveIndex)
        {
            Debug.ASSERT(theWaveIndex >= 0 && theWaveIndex < 100 && theWaveIndex < mNumWaves);
            for (int i = 0; i < 50; i++)
            {
                ZombieType zombieType = mZombiesInWave[theWaveIndex, i];
                if (zombieType == ZombieType.Invalid)
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
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && zombie.mFromWave == theWaveIndex && !zombie.mMindControlled && !zombie.IsDeadOrDying() && zombie.mZombieType != ZombieType.Bungee && zombie.mRelatedZombieID == null)
                {
                    num += zombie.mBodyHealth;
                    num += zombie.mHelmHealth;
                    num += (int)(zombie.mShieldHealth * 0.2f);
                    num += zombie.mFlyingHealth;
                }
            }
            return num;
        }

        public void DrawUICoinBank(Graphics g)
        {
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            if (mApp.mGameScene != GameScenes.Playing && mApp.mCrazyDaveState == CrazyDaveState.Off)
            {
                return;
            }
            if (mCoinBankFadeCount <= 0)
            {
                return;
            }
            int num = Constants.UICoinBankPosition.X;
            int num2 = Constants.UICoinBankPosition.Y - AtlasResources.IMAGE_COINBANK.mHeight - 1;
            if (mApp.IsSlotMachineLevel())
            {
                num -= 50;
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                num = 450 - mX;
            }
            else if (mApp.mCrazyDaveState != CrazyDaveState.Off)
            {
                num = 150 - mX;
            }
            g.SetColorizeImages(true);
            int theAlpha = TodCommon.ClampInt(255 * mCoinBankFadeCount / 15, 0, 255);
            g.SetColor(new SexyColor(255, 255, 255, theAlpha));
            g.DrawImage(AtlasResources.IMAGE_COINBANK, num, num2);
            g.SetColor(new SexyColor(180, 255, 90, theAlpha));
            g.SetFont(Resources.FONT_CONTINUUMBOLD14);
            string moneyString = LawnApp.GetMoneyString(mApp.mPlayerInfo.mCoins);
            int theX = num + Constants.StoreScreen_Coinbank_TextOffset.X - Resources.FONT_CONTINUUMBOLD14.StringWidth(moneyString);
            g.DrawString(moneyString, theX, num2 + Constants.StoreScreen_Coinbank_TextOffset.Y);
            g.SetColorizeImages(false);
        }

        public void ShowCoinBank()
        {
            mCoinBankFadeCount = 1000;
        }

        public void FadeOutLevel()
        {
            if (mApp.IsScaryPotterLevel())
            {
                if (mApp.mGameMode == GameMode.ScaryPotterEndless && mChallenge.mSurvivalStage >= 14)
                {
                    GrantAchievement(AchievementId.ChinaShop);
                }
            }
            else if (mApp.IsIZombieLevel())
            {
                if (mApp.mGameMode == GameMode.PuzzleIZombieEndless && mChallenge.mSurvivalStage >= 9)
                {
                    GrantAchievement(AchievementId.BetterOffDead);
                }
            }
            else if (!mApp.IsQuickPlayMode() && !HasConveyorBeltSeedBank() && !mApp.IsWhackAZombieLevel())
            {
                if (AwardCloseShave())
                {
                    GrantAchievement(AchievementId.CloseShave);
                }
                if (mNomNomNomAchievementTracker)
                {
                    GrantAchievement(AchievementId.NomNomNom);
                }
                if (StageIsNight() && mNoFungusAmongUsAchievementTracker)
                {
                    GrantAchievement(AchievementId.NoFungusAmongUs);
                }
                if (StageHasPool() && !mPeaShooterUsed)
                {
                    GrantAchievement(AchievementId.DontPeainthePool);
                }
                if (StageHasRoof() && !mCatapultPlantsUsed)
                {
                    GrantAchievement(AchievementId.Grounded);
                }
                if (StageIsDayWithoutPool() && mMushroomAndCoffeeBeansOnly)
                {
                    GrantAchievement(AchievementId.GoodMorning);
                }
            }
            if (mLevel >= 3)
            {
                mApp.mPlayerInfo.mHasFinishedTutorial = true;
            }
            if (mApp.mGameScene != GameScenes.Playing)
            {
                RefreshSeedPacketFromCursor();
                mApp.mLastLevelStats.mUnusedLawnMowers = 0;
                mLevelComplete = true;
                return;
            }
            bool flag = true;
            if (mApp.IsScaryPotterLevel() && !IsFinalScaryPotterStage())
            {
                flag = false;
            }
            else if (IsSurvivalStageWithRepick())
            {
                flag = false;
            }
            else if (IsLastStandStageWithRepick())
            {
                flag = false;
            }
            else if (mApp.IsEndlessIZombie(mApp.mGameMode))
            {
                flag = false;
            }
            if (flag)
            {
                mApp.mMusic.StopAllMusic();
                if (mApp.IsAdventureMode() && mLevel == 50)
                {
                    mApp.PlayFoley(FoleyType.Finalfanfare);
                }
                else if (mApp.TrophiesNeedForGoldSunflower() == 1)
                {
                    mApp.PlayFoley(FoleyType.Finalfanfare);
                }
                else
                {
                    mApp.PlayFoley(FoleyType.Winmusic);
                }
            }
            if (mApp.IsEndlessScaryPotter(mApp.mGameMode))
            {
                mLevelAwardSpawned = true;
                mNextSurvivalStageCounter = 500;
                string theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_MORE_SCARY_POTS]", "{STREAK}", mChallenge.mSurvivalStage + 1);
                PuzzleSaveStreak();
                ClearAdvice(AdviceType.None);
                DisplayAdvice(theAdvice, MessageStyle.BigMiddle, AdviceType.None);
                return;
            }
            if (mApp.IsAdventureMode() && mApp.IsScaryPotterLevel() && !IsFinalScaryPotterStage())
            {
                mNextSurvivalStageCounter = 500;
                ClearAdvice(AdviceType.None);
                return;
            }
            if (mApp.IsScaryPotterLevel() && !IsFinalScaryPotterStage())
            {
                mLevelAwardSpawned = true;
                mNextSurvivalStageCounter = 500;
                string theAdvice2 = TodCommon.TodReplaceNumberString("[ADVICE_3_IN_A_ROW]", "{STREAK}", mChallenge.mSurvivalStage + 1);
                PuzzleSaveStreak();
                ClearAdvice(AdviceType.None);
                DisplayAdvice(theAdvice2, MessageStyle.BigMiddle, AdviceType.None);
                return;
            }
            if (mApp.IsEndlessIZombie(mApp.mGameMode))
            {
                mNextSurvivalStageCounter = 500;
                string theAdvice3 = TodCommon.TodReplaceNumberString("[ADVICE_MORE_IZOMBIE]", "{STREAK}", mChallenge.mSurvivalStage + 1);
                PuzzleSaveStreak();
                ClearAdvice(AdviceType.None);
                DisplayAdvice(theAdvice3, MessageStyle.BigMiddle, AdviceType.None);
                return;
            }
            if (IsLastStandStageWithRepick())
            {
                mNextSurvivalStageCounter = 500;
                mChallenge.LastStandCompletedStage();
                return;
            }
            if (!IsSurvivalStageWithRepick())
            {
                RefreshSeedPacketFromCursor();
                mApp.mLastLevelStats.mUnusedLawnMowers = CountUntriggerLawnMowers();
                mBoardFadeOutCounter = 600;
                bool flag2 = mLevel == 9 || mLevel == 19 || mLevel == 29 || mLevel == 39 || mLevel == 49;
                if (flag2)
                {
                    mBoardFadeOutCounter = 500;
                }
                if (CanDropLoot())
                {
                    mScoreNextMowerCounter = 200;
                }
                Coin coin = null;
                while (IterateCoins(ref coin))
                {
                    coin.TryAutoCollectAfterLevelAward();
                }
                return;
            }
            Debug.ASSERT(mApp.IsSurvivalMode());
            mNextSurvivalStageCounter = 500;
            DisplayAdvice("[ADVICE_MORE_ZOMBIES]", MessageStyle.BigMiddle, AdviceType.None);
            mApp.mMusic.FadeOut(500);
            mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
            for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
            {
                if (mIceTimer[i] > mNextSurvivalStageCounter)
                {
                    mIceTimer[i] = mNextSurvivalStageCounter;
                }
            }
        }

        public bool AwardCloseShave()
        {
            return GetBottomLawnMower() == null && (mBackground != BackgroundType.Num5Roof || mApp.mPlayerInfo.mPurchases[23] != 0) && mApp.mGameMode != GameMode.ChallengeBeghouled && mApp.mGameMode != GameMode.ChallengeBeghouledTwist;
        }

        public void DrawFadeOut(Graphics g)
        {
            if (mBoardFadeOutCounter < 0)
            {
                return;
            }
            if (IsSurvivalStageWithRepick())
            {
                return;
            }
            int theAlpha = TodCommon.TodAnimateCurve(200, 0, mBoardFadeOutCounter, 0, 255, TodCurves.Linear);
            if (mLevel == 9 || mLevel == 19 || mLevel == 29 || mLevel == 39 || mLevel == 49)
            {
                g.SetColor(new SexyColor(0, 0, 0, theAlpha, false));
            }
            else
            {
                g.SetColor(new SexyColor(255, 255, 255, theAlpha, false));
            }
            g.SetColorizeImages(true);
            g.FillRect(-Constants.Board_Offset_AspectRatio_Correction, 0, mWidth, mHeight);
        }

        public void DrawIce(Graphics g, int y)
        {
            int theY = (int)((GridToPixelY(8, y) + 20) * Constants.S);
            int height = AtlasResources.IMAGE_ICE.GetHeight();
            int num = TodCommon.ClampInt((int)(255 * mIceTimer[y] / 10f), 0, 255);
            if (num < 255)
            {
                g.SetColorizeImages(true);
                g.SetColor(new SexyColor(255, 255, 255, num, true));
            }
            int num2 = (int)(mIceMinX[y] * Constants.S);
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
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                return;
            }
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
            SexyColor aColor = default(SexyColor);
            aColor.mAlpha = 0;
            aColor.mRed = (aColor.mGreen = (aColor.mBlue = 50));
            g.SetColor(aColor);
            TPoint[] celPosition = GetCelPosition(theCol, theRow);
            celPosition[0].mX = (int)(celPosition[0].mX * Constants.S);
            celPosition[0].mY = (int)(celPosition[0].mY * Constants.S);
            celPosition[1].mX = (int)(celPosition[1].mX * Constants.S);
            celPosition[1].mY = (int)(celPosition[1].mY * Constants.S);
            celPosition[2].mX = (int)(celPosition[2].mX * Constants.S);
            celPosition[2].mY = (int)(celPosition[2].mY * Constants.S);
            celPosition[3].mX = (int)(celPosition[3].mX * Constants.S);
            celPosition[3].mY = (int)(celPosition[3].mY * Constants.S);
            g.SetColorizeImages(true);
            g.FillRect(new TRect(Board.mCelPoints[0].mX, Board.mCelPoints[0].mY, Board.mCelPoints[2].mX - Board.mCelPoints[0].mX, Board.mCelPoints[2].mY - Board.mCelPoints[0].mY));
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
        }

        public TPoint[] GetCelPosition(int theCol, int theRow)
        {
            int num = StageHas6Rows() ? 6 : 5;
            if (StageHasPool())
            {
                int[] board_Cel_Y_Values_Pool = Constants.Board_Cel_Y_Values_Pool;
                Board.mCelPoints[0].mX = GridToPixelX(theCol, theRow);
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
            else if (StageHasRoof())
            {
                int num2 = 20;
                Board.mCelPoints[0].mX = GridToPixelX(theCol, theRow);
                Board.mCelPoints[0].mY = GridToPixelY(theCol, theRow) + num2;
                int num3 = 80;
                Board.mCelPoints[1].mX = Board.mCelPoints[0].mX + num3;
                Board.mCelPoints[1].mY = Board.mCelPoints[0].mY;
                Board.mCelPoints[2].mX = Board.mCelPoints[1].mX;
                Board.mCelPoints[2].mY = ((theRow < num - 1) ? (GridToPixelY(theCol, theRow + 1) + num2) : (Board.mCelPoints[0].mY + num3));
                Board.mCelPoints[3].mX = Board.mCelPoints[0].mX;
                Board.mCelPoints[3].mY = Board.mCelPoints[2].mY;
            }
            else if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                int[] board_Cel_Y_Values_ZenGarden = Constants.Board_Cel_Y_Values_ZenGarden;
                Board.mCelPoints[0].mX = GridToPixelX(theCol, theRow);
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
                Board.mCelPoints[0].mX = GridToPixelX(theCol, theRow);
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
            if (mIceTimer[theGridY] <= 0)
            {
                return false;
            }
            if (mIceMinX[theGridY] > 750 + Constants.BOARD_EXTRA_ROOM)
            {
                return false;
            }
            int num = PixelToGridXKeepOnBoard(mIceMinX[theGridY] + 12, 0);
            return theGridX >= num;
        }

        public Zombie ZombieGetID(Zombie theZombie)
        {
            if (mZombies.IndexOf(theZombie) != -1)
            {
                return theZombie;
            }
            return null;
        }

        public Zombie ZombieGet(Zombie theZombieID)
        {
            return mZombies[mZombies.IndexOf(theZombieID)];
        }

        public Zombie ZombieTryToGet(Zombie theZombieID)
        {
            int num = mZombies.IndexOf(theZombieID);
            if (num != -1)
            {
                return mZombies[num];
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
                if (mIceTimer[i] > 0)
                {
                    mIceTimer[i] -= 3;
                    if (mIceTimer[i] < 0)
                    {
                        mIceTimer[i] = 0;
                    }
                    if (mIceTimer[i] >= 0 && mIceTimer[i] < 3)
                    {
                        mIceMinX[i] = Constants.Board_Ice_Start;
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
                if (mIceTimer[i] > 0 && mIceMinX[i] < 700 + Constants.BOARD_EXTRA_ROOM)
                {
                    return true;
                }
            }
            return false;
        }

        public void ShakeBoard(int theShakeAmountX, int theShakeAmountY)
        {
            mShakeCounter = 12;
            mShakeAmountX = theShakeAmountX;
            mShakeAmountY = theShakeAmountY;
        }

        public int CountUntriggerLawnMowers()
        {
            int num = 0;
            LawnMower lawnMower = null;
            while (IterateLawnMowers(ref lawnMower))
            {
                if (lawnMower.mMowerState != LawnMowerState.Triggered && lawnMower.mMowerState != LawnMowerState.Squished)
                {
                    num++;
                }
            }
            return num;
        }

        public bool IterateZombies(ref Zombie theZombie, ref int index)
        {
            if (index == -1 || index >= mZombies.Count)
            {
                index = mZombies.IndexOf(theZombie);
            }
            while (++index < mZombies.Count)
            {
                theZombie = mZombies[index];
                if (!theZombie.mDead)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IteratePlants(ref Plant thePlant, ref int index)
        {
            if (index == -1 || index >= mPlants.Count)
            {
                index = mPlants.IndexOf(thePlant);
            }
            while (++index < mPlants.Count)
            {
                thePlant = mPlants[index];
                if (!thePlant.mDead)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IterateProjectiles(ref Projectile theProjectile, ref int index)
        {
            if (index == -1 || index >= mProjectiles.Count)
            {
                index = mProjectiles.IndexOf(theProjectile);
            }
            while (++index < mProjectiles.Count)
            {
                theProjectile = mProjectiles[index];
                if (!theProjectile.mDead)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IterateCoins(ref Coin theCoin)
        {
            int num = mCoins.IndexOf(theCoin);
            for (int i = num + 1; i < mCoins.Count; i++)
            {
                theCoin = mCoins[i];
                if (!theCoin.mDead)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IterateLawnMowers(ref LawnMower theLawnMower)
        {
            int num = mLawnMowers.IndexOf(theLawnMower);
            for (int i = num + 1; i < mLawnMowers.Count; i++)
            {
                theLawnMower = mLawnMowers[i];
                if (!theLawnMower.mDead)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IterateParticles(ref TodParticleSystem theParticle, ref int index)
        {
            if (index == -1 || index >= mApp.mEffectSystem.mParticleHolder.mParticleSystems.Count)
            {
                index = mApp.mEffectSystem.mParticleHolder.mParticleSystems.IndexOf(theParticle);
            }
            while (++index < mApp.mEffectSystem.mParticleHolder.mParticleSystems.Count)
            {
                theParticle = mApp.mEffectSystem.mParticleHolder.mParticleSystems[index];
                if (theParticle != null && !theParticle.mDead)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IterateReanimations(ref Reanimation theReanimation, ref int index)
        {
            if (index == -1 || index >= mApp.mEffectSystem.mReanimationHolder.mReanimations.Count)
            {
                index = mApp.mEffectSystem.mReanimationHolder.mReanimations.IndexOf(theReanimation);
            }
            while (++index < mApp.mEffectSystem.mReanimationHolder.mReanimations.Count)
            {
                theReanimation = mApp.mEffectSystem.mReanimationHolder.mReanimations[index];
                if (theReanimation != null && !theReanimation.mDead)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IterateGridItems(ref GridItem theGridItem, ref int index)
        {
            if (index == -1 || index >= mGridItems.Count)
            {
                index = mGridItems.IndexOf(theGridItem);
            }
            while (++index < mGridItems.Count)
            {
                theGridItem = mGridItems[index];
                if (!theGridItem.mDead)
                {
                    return true;
                }
            }
            return false;
        }

        public void ZombieSwitchRow(Zombie aZombie, int aRow)
        {
            List<Zombie> zombiesInRow = GetZombiesInRow(aZombie.mRow);
            List<Zombie> zombiesInRow2 = GetZombiesInRow(aRow);
            zombiesInRow.Remove(aZombie);
            zombiesInRow2.Add(aZombie);
        }

        public void SortZombieRowLists()
        {
            mZombiesRow1.Sort();
            mZombiesRow2.Sort();
            mZombiesRow3.Sort();
            mZombiesRow4.Sort();
            mZombiesRow5.Sort();
            mZombiesRow6.Sort();
        }

        public List<Zombie> GetZombiesInRow(int aRow)
        {
            if (aRow == 0)
            {
                return mZombiesRow1;
            }
            if (aRow == 1)
            {
                return mZombiesRow2;
            }
            if (aRow == 2)
            {
                return mZombiesRow3;
            }
            if (aRow == 3)
            {
                return mZombiesRow4;
            }
            if (aRow == 4)
            {
                return mZombiesRow5;
            }
            return mZombiesRow6;
        }

        public void AddToZombieList(Zombie aZombie)
        {
            mZombies.Add(aZombie);
            if (aZombie.mZombieType == ZombieType.Boss)
            {
                for (int i = 0; i < 6; i++)
                {
                    List<Zombie> zombiesInRow = GetZombiesInRow(i);
                    zombiesInRow.Add(aZombie);
                }
                return;
            }
            List<Zombie> zombiesInRow2 = GetZombiesInRow(aZombie.mRow);
            zombiesInRow2.Add(aZombie);
        }

        public void AddToZombieList(Zombie aZombie, int row)
        {
            mZombies.Add(aZombie);
            List<Zombie> zombiesInRow = GetZombiesInRow(row);
            zombiesInRow.Add(aZombie);
        }

        public Zombie AddZombieInRow(ZombieType theZombieType, int theRow, int theFromWave)
        {
            if (mZombies.Count >= mZombies.Capacity - 1)
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
            AddToZombieList(newZombie);
            if (theZombieType == ZombieType.Bobsled && newZombie.IsOnBoard())
            {
                Zombie newZombie2 = Zombie.GetNewZombie();
                Zombie newZombie3 = Zombie.GetNewZombie();
                Zombie newZombie4 = Zombie.GetNewZombie();
                AddToZombieList(newZombie2, theRow);
                AddToZombieList(newZombie3, theRow);
                AddToZombieList(newZombie4, theRow);
                newZombie2.ZombieInitialize(theRow, ZombieType.Bobsled, false, newZombie, theFromWave);
                newZombie3.ZombieInitialize(theRow, ZombieType.Bobsled, false, newZombie, theFromWave);
                newZombie4.ZombieInitialize(theRow, ZombieType.Bobsled, false, newZombie, theFromWave);
            }
            return newZombie;
        }

        public bool IsPoolSquare(int theGridX, int theGridY)
        {
            return theGridX >= 0 && theGridY >= 0 && mGridSquareType[theGridX, theGridY] == GridSquareType.Pool;
        }

        public void PickZombieWaves()
        {
            if ((mApp.IsAdventureMode() || mApp.IsQuickPlayMode()) && mApp.IsWhackAZombieLevel())
            {
                mNumWaves = 8;
            }
            else if (mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
            {
                int num = TodCommon.ClampInt(mLevel - 1, 0, 49);
                mNumWaves = GameConstants.gZombieWaves[num];
                if (!mApp.IsFirstTimeAdventureMode() && !mApp.IsMiniBossLevel())
                {
                    if (mNumWaves < 10)
                    {
                        mNumWaves = 20;
                    }
                    else
                    {
                        mNumWaves += 10;
                    }
                }
            }
            else if (mApp.IsSurvivalMode() || mApp.mGameMode == GameMode.ChallengeLastStand)
            {
                mNumWaves = GetNumWavesPerSurvivalStage();
            }
            else if (mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom || mApp.IsSquirrelLevel())
            {
                mNumWaves = 0;
            }
            else if (mApp.mGameMode == GameMode.ChallengeWhackAZombie)
            {
                mNumWaves = 12;
            }
            else if (mApp.mGameMode == GameMode.ChallengeWallnutBowling || mApp.mGameMode == GameMode.ChallengeAirRaid || mApp.mGameMode == GameMode.ChallengeGraveDanger || mApp.mGameMode == GameMode.ChallengeHighGravity || mApp.mGameMode == GameMode.ChallengePortalCombat || mApp.mGameMode == GameMode.ChallengeWarAndPeas || mApp.mGameMode == GameMode.ChallengeInvisighoul)
            {
                mNumWaves = 20;
            }
            else if (mApp.IsStormyNightLevel() || mApp.IsLittleTroubleLevel() || mApp.IsBungeeBlitzLevel() || mApp.mGameMode == GameMode.ChallengeColumn || mApp.IsShovelLevel() || mApp.mGameMode == GameMode.ChallengeWarAndPeas2 || mApp.mGameMode == GameMode.ChallengeWallnutBowling2 || mApp.mGameMode == GameMode.ChallengePogoParty)
            {
                mNumWaves = 30;
            }
            else
            {
                mNumWaves = 40;
            }
            ZombiePicker zombiePicker = new ZombiePicker();
            Board.ZombiePickerInit(zombiePicker);
            ZombieType introducedZombieType = GetIntroducedZombieType();
            Debug.ASSERT(mNumWaves <= 100);
            
            for (int i = 0; i < mNumWaves; i++)
            {
                Board.ZombiePickerInitForWave(zombiePicker);
                mZombiesInWave[i, 0] = ZombieType.Invalid;
                bool isFlagWave = IsFlagWave(i);
                bool isBeforeLastWave = i == mNumWaves - 1;
                if (mApp.IsBungeeBlitzLevel() && isFlagWave)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        PutZombieInWave(ZombieType.Bungee, i, zombiePicker);
                    }
                    if (!isBeforeLastWave)
                    {
                        if ((mApp.IsAdventureMode() || mApp.IsQuickPlayMode()) && isBeforeLastWave)
                        {
                            PutInMissingZombies(i, zombiePicker);
                        }
                        continue;
                    }
                }
                if (mApp.mGameMode == GameMode.ChallengeLastStand)
                {
                    zombiePicker.mZombiePoints = (mChallenge.mSurvivalStage * GetNumWavesPerSurvivalStage() + i + 10) * 2 / 5 + 1;
                }
                else if (mApp.IsSurvivalMode() && mChallenge.mSurvivalStage > 0)
                {
                    zombiePicker.mZombiePoints = (mChallenge.mSurvivalStage * GetNumWavesPerSurvivalStage() + i) * 2 / 5 + 1;
                }
                else if (mApp.IsAdventureMode() && mApp.HasFinishedAdventure() && mLevel != 5)
                {
                    zombiePicker.mZombiePoints = i * 2 / 5 + 1;
                }
                else
                {
                    zombiePicker.mZombiePoints = i / 3 + 1;
                }
                if (isFlagWave)
                {
                    int num2 = Math.Min(zombiePicker.mZombiePoints, 8);
                    zombiePicker.mZombiePoints = (int)(zombiePicker.mZombiePoints * 2.5f);
                    if (mApp.mGameMode != GameMode.ChallengeWarAndPeas && mApp.mGameMode != GameMode.ChallengeWarAndPeas2)
                    {
                        for (int k = 0; k < num2; k++)
                        {
                            PutZombieInWave(ZombieType.Normal, i, zombiePicker);
                        }
                        PutZombieInWave(ZombieType.Flag, i, zombiePicker);
                    }
                }
                if (mApp.mGameMode == GameMode.ChallengeColumn)
                {
                    zombiePicker.mZombiePoints *= 6;
                }
                else if (mApp.IsLittleTroubleLevel() || mApp.IsWallnutBowlingLevel())
                {
                    zombiePicker.mZombiePoints *= 4;
                }
                else if (mApp.IsMiniBossLevel())
                {
                    zombiePicker.mZombiePoints *= 3;
                }
                else if (mApp.IsStormyNightLevel() && (mApp.IsAdventureMode() || mApp.IsQuickPlayMode()))
                {
                    zombiePicker.mZombiePoints *= 3;
                }
                else if (mApp.IsShovelLevel() || mApp.IsBungeeBlitzLevel() || mApp.mGameMode == GameMode.ChallengePortalCombat || mApp.mGameMode == GameMode.ChallengeInvisighoul)
                {
                    zombiePicker.mZombiePoints *= 2;
                }
                if (introducedZombieType != ZombieType.Invalid && introducedZombieType != ZombieType.DuckyTube)
                {
                    bool flag3 = false;
                    if (introducedZombieType == ZombieType.Digger || introducedZombieType == ZombieType.Balloon)
                    {
                        if (i + 1 == 7 || isBeforeLastWave)
                        {
                            flag3 = true;
                        }
                    }
                    else if (introducedZombieType == ZombieType.Yeti)
                    {
                        if (i == mNumWaves / 2 && !mApp.mKilledYetiAndRestarted && !mApp.IsQuickPlayMode())
                        {
                            flag3 = true;
                        }
                    }
                    else if (i == mNumWaves / 2 || isBeforeLastWave)
                    {
                        flag3 = true;
                    }
                    if (flag3)
                    {
                        PutZombieInWave(introducedZombieType, i, zombiePicker);
                    }
                }
                if (mLevel == 50 && isBeforeLastWave)
                {
                    PutZombieInWave(ZombieType.Gargantuar, i, zombiePicker);
                }
                if ((mApp.IsAdventureMode() || mApp.IsQuickPlayMode()) && isBeforeLastWave)
                {
                    PutInMissingZombies(i, zombiePicker);
                }
                if (mApp.mGameMode == GameMode.ChallengeColumn)
                {
                    if (i % 10 == 5)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            PutZombieInWave(ZombieType.Ladder, i, zombiePicker);
                        }
                    }
                    if (i % 10 == 8)
                    {
                        for (int m = 0; m < 10; m++)
                        {
                            PutZombieInWave(ZombieType.JackInTheBox, i, zombiePicker);
                        }
                    }
                    if (i == 19)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            PutZombieInWave(ZombieType.Gargantuar, i, zombiePicker);
                        }
                    }
                    if (i == 29)
                    {
                        for (int num3 = 0; num3 < 5; num3++)
                        {
                            PutZombieInWave(ZombieType.Gargantuar, i, zombiePicker);
                        }
                    }
                }
                while (zombiePicker.mZombiePoints > 0 && zombiePicker.mZombieCount < 50)
                {
                    ZombieType theZombieType = PickZombieType(zombiePicker.mZombiePoints, i, zombiePicker);
                    PutZombieInWave(theZombieType, i, zombiePicker);
                }
                int aZombiePoints = zombiePicker.mZombiePoints;
                continue;
            }
        }

        public void StopAllZombieSounds()
        {
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead)
                {
                    zombie.StopZombieSound();
                }
            }
        }

        public bool HasLevelAwardDropped()
        {
            return mLevelAwardSpawned || (mNextSurvivalStageCounter > 0 || mBoardFadeOutCounter >= 0);
        }

        public void UpdateProgressMeter()
        {
            if (mApp.IsFinalBossLevel())
            {
                Zombie bossZombie = GetBossZombie();
                if (bossZombie != null && !bossZombie.IsDeadOrDying())
                {
                    mProgressMeterWidth = 150 * (bossZombie.mBodyMaxHealth - bossZombie.mBodyHealth) / bossZombie.mBodyMaxHealth;
                    return;
                }
                mProgressMeterWidth = 150;
                return;
            }
            else
            {
                if (mCurrentWave == 0)
                {
                    return;
                }
                if (mFlagRaiseCounter > 0)
                {
                    mFlagRaiseCounter -= 3;
                }
                int num = 150;
                int numWavesPerFlag = GetNumWavesPerFlag();
                if (ProgressMeterHasFlags())
                {
                    int num2 = mNumWaves / numWavesPerFlag;
                    num -= num2 * 12;
                }
                int num3 = num / (mNumWaves - 1);
                int num4 = (mCurrentWave - 1) * num / (mNumWaves - 1);
                int num5 = mCurrentWave * num / (mNumWaves - 1);
                if (ProgressMeterHasFlags())
                {
                    int num6 = mCurrentWave / numWavesPerFlag;
                    num4 += num6 * 12;
                    num5 += num6 * 12;
                }
                float num7 = (mZombieCountDownStart - mZombieCountDown) / (float)mZombieCountDownStart;
                if (mZombieHealthToNextWave != -1)
                {
                    int num8 = TotalZombiesHealthInWave(mCurrentWave - 1);
                    int num9 = Math.Max(mZombieHealthWaveStart - mZombieHealthToNextWave, 1);
                    float num10 = (num9 - num8 + mZombieHealthToNextWave) / (float)num9;
                    if (num10 > num7)
                    {
                        num7 = num10;
                    }
                }
                int num11 = num4 + TodCommon.FloatRoundToInt((num5 - num4) * num7);
                num11 = TodCommon.ClampInt(num11, 1, 150);
                int num12 = num11 - mProgressMeterWidth;
                if (num12 > num3 && Board.IsInModRange(mMainCounter, 5))
                {
                    mProgressMeterWidth++;
                    return;
                }
                if (num12 > 0 && Board.IsInModRange(mMainCounter, 20))
                {
                    mProgressMeterWidth++;
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
            if (mApp.mGameScene != GameScenes.ZombiesWon)
            {
                if (mSeedBank.BeginDraw(g))
                {
                    mSeedBank.DrawSun(g);
                    mSeedBank.EndDraw(g);
                }
                MessageStyle aMessageStyle = mAdvice.mMessageStyle;
            }
            DrawShovel(g);
            if (!StageHasFog())
            {
                DrawTopRightUI(g);
            }
            if (mApp.mGameScene == GameScenes.Playing)
            {
                DrawProgressMeter(g);
            }
        }

        public void DrawUITop(Graphics g)
        {
            if (mApp.mGameScene != GameScenes.ZombiesWon && mSeedBank.BeginDraw(g))
            {
                mSeedBank.Draw(g);
                mSeedBank.EndDraw(g);
            }
            if (StageHasFog())
            {
                DrawTopRightUI(g);
            }
            if (mTimeStopCounter > 0)
            {
                g.SetColor(new SexyColor(200, 200, 200, 210));
                g.FillRect(0, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
            }
            if (mApp.IsSlotMachineLevel())
            {
                mChallenge.DrawSlotMachine(g);
            }
            if (mApp.mGameScene == GameScenes.Playing)
            {
                DrawLevel(g);
            }
            if (mStoreButton != null && mApp.mGameMode == GameMode.ChallengeLastStand)
            {
                mStoreButton.Draw(g);
            }
            if ((mApp.mGameMode == GameMode.Upsell || mApp.mGameMode == GameMode.Intro) && mCutScene.mUpsellHideBoard)
            {
                g.SetColor(new SexyColor(0, 0, 0));
                g.FillRect(-Constants.Board_Offset_AspectRatio_Correction, 0, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT);
            }
            if (mApp.mGameMode == GameMode.Upsell)
            {
                mCutScene.DrawUpsell(g);
            }
            if (mApp.mGameMode == GameMode.Intro)
            {
                mCutScene.DrawIntro(g);
            }
            if (mApp.mGameScene == GameScenes.LevelIntro || mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom || IsScaryPotterDaveTalking())
            {
                Graphics @new = Graphics.GetNew(g);
                @new.mTransX -= mX;
                @new.mTransY -= mY;
                mApp.DrawCrazyDave(@new);
                @new.PrepareForReuse();
            }
            mAdvice.Draw(g);
        }

        public Zombie ZombieHitTest(int theMouseX, int theMouseY)
        {
            Zombie zombie = null;
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie2 = mZombies[i];
                if (!zombie2.mDead && !zombie2.IsDeadOrDying() && (mApp.mGameScene != GameScenes.LevelIntro || (zombie2.mZombieType != ZombieType.PeaHead && zombie2.mZombieType != ZombieType.WallnutHead && zombie2.mZombieType != ZombieType.TallnutHead && zombie2.mZombieType != ZombieType.JalapenoHead && zombie2.mZombieType != ZombieType.GatlingHead && zombie2.mZombieType != ZombieType.SquashHead)) && zombie2.GetZombieRect().Contains(theMouseX, theMouseY) && (zombie == null || zombie2.mY > zombie.mY))
                {
                    zombie = zombie2;
                }
            }
            return zombie;
        }

        public void KillAllPlantsInRadius(int theX, int theY, int theRadius)
        {
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead)
                {
                    TRect plantRect = plant.GetPlantRect();
                    if (GameConstants.GetCircleRectOverlap(theX, theY, theRadius, plantRect))
                    {
                        mPlantsEaten++;
                        plant.Die();
                    }
                }
            }
        }

        public Plant GetPumpkinAt(int theGridX, int theGridY)
        {
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead && plant.mPlantCol == theGridX && plant.mRow == theGridY && !plant.NotOnGround() && plant.mSeedType == SeedType.Pumpkinshell)
                {
                    return plant;
                }
            }
            return null;
        }

        public Plant GetFlowerPotAt(int theGridX, int theGridY)
        {
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead && plant.mPlantCol == theGridX && plant.mRow == theGridY && !plant.NotOnGround() && plant.mSeedType == SeedType.Flowerpot)
                {
                    return plant;
                }
            }
            return null;
        }

        public static bool CanZombieSpawnOnLevel(ZombieType theZombieType, int theLevel)
        {
            ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
            if (theZombieType == ZombieType.Yeti)
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
            if (!mApp.IsAdventureMode() && !mApp.IsQuickPlayMode())
            {
                return true;
            }
            int[] array = new int[33];
            for (int i = 0; i < 33; i++)
            {
                array[i] = 0;
            }
            for (int j = 0; j < mNumWaves; j++)
            {
                for (int k = 0; k < 50; k++)
                {
                    ZombieType zombieType = mZombiesInWave[j, k];
                    if (zombieType == ZombieType.Invalid)
                    {
                        break;
                    }
                    Debug.ASSERT(zombieType >= ZombieType.Normal && zombieType < ZombieType.ZombieTypesCount);
                    array[(int)zombieType]++;
                }
            }
            for (int l = 0; l < 33; l++)
            {
                if (l != 19 && Board.CanZombieSpawnOnLevel((ZombieType)l, mLevel) && array[l] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void PickBackground()
        {
            switch (mApp.mGameMode)
            {
            case GameMode.Adventure:
            case GameMode.Quickplay1:
            case GameMode.Quickplay2:
            case GameMode.Quickplay3:
            case GameMode.Quickplay4:
            case GameMode.Quickplay5:
            case GameMode.Quickplay6:
            case GameMode.Quickplay7:
            case GameMode.Quickplay8:
            case GameMode.Quickplay9:
            case GameMode.Quickplay10:
            case GameMode.Quickplay11:
            case GameMode.Quickplay12:
            case GameMode.Quickplay13:
            case GameMode.Quickplay14:
            case GameMode.Quickplay15:
            case GameMode.Quickplay16:
            case GameMode.Quickplay17:
            case GameMode.Quickplay18:
            case GameMode.Quickplay19:
            case GameMode.Quickplay20:
            case GameMode.Quickplay21:
            case GameMode.Quickplay22:
            case GameMode.Quickplay23:
            case GameMode.Quickplay24:
            case GameMode.Quickplay25:
            case GameMode.Quickplay26:
            case GameMode.Quickplay27:
            case GameMode.Quickplay28:
            case GameMode.Quickplay29:
            case GameMode.Quickplay30:
            case GameMode.Quickplay31:
            case GameMode.Quickplay32:
            case GameMode.Quickplay33:
            case GameMode.Quickplay34:
            case GameMode.Quickplay35:
            case GameMode.Quickplay36:
            case GameMode.Quickplay37:
            case GameMode.Quickplay38:
            case GameMode.Quickplay39:
            case GameMode.Quickplay40:
            case GameMode.Quickplay41:
            case GameMode.Quickplay42:
            case GameMode.Quickplay43:
            case GameMode.Quickplay44:
            case GameMode.Quickplay45:
            case GameMode.Quickplay46:
            case GameMode.Quickplay47:
            case GameMode.Quickplay48:
            case GameMode.Quickplay49:
            case GameMode.Quickplay50:
                if (mLevel <= 10)
                {
                    mBackground = BackgroundType.Num1Day;
                }
                else if (mLevel <= 20)
                {
                    mBackground = BackgroundType.Num2Night;
                }
                else if (mLevel <= 30)
                {
                    mBackground = BackgroundType.Num3Pool;
                }
                else if (mApp.IsScaryPotterLevel())
                {
                    mBackground = BackgroundType.Num2Night;
                }
                else if (mLevel <= 40)
                {
                    mBackground = BackgroundType.Num4Fog;
                }
                else if (mLevel <= 49)
                {
                    mBackground = BackgroundType.Num5Roof;
                }
                else if (mLevel == 50)
                {
                    mBackground = BackgroundType.Num6Boss;
                }
                else
                {
                    mBackground = BackgroundType.Num1Day;
                }
                break;
            case GameMode.SurvivalNormalStage1:
            case GameMode.SurvivalHardStage1:
            case GameMode.SurvivalEndlessStage1:
            case GameMode.ChallengeWarAndPeas:
            case GameMode.ChallengeWallnutBowling:
            case GameMode.ChallengeSlotMachine:
            case GameMode.ChallengeSeeingStars:
            case GameMode.ChallengeWallnutBowling2:
            case GameMode.ChallengeArtChallenge1:
            case GameMode.ChallengeSunnyDay:
            case GameMode.ChallengeResodded:
            case GameMode.ChallengeBigTime:
            case GameMode.ChallengeArtChallenge2:
            case GameMode.ChallengeIce:
            case GameMode.ChallengeShovel:
            case GameMode.ChallengeSquirrel:
                mBackground = BackgroundType.Num1Day;
                break;
            case GameMode.SurvivalNormalStage2:
            case GameMode.SurvivalHardStage2:
            case GameMode.SurvivalEndlessStage2:
            case GameMode.ChallengeBeghouled:
            case GameMode.ChallengeBeghouledTwist:
            case GameMode.ChallengePortalCombat:
            case GameMode.ChallengeWhackAZombie:
            case GameMode.ChallengeGraveDanger:
            case GameMode.ScaryPotter1:
            case GameMode.ScaryPotter2:
            case GameMode.ScaryPotter3:
            case GameMode.ScaryPotter4:
            case GameMode.ScaryPotter5:
            case GameMode.ScaryPotter6:
            case GameMode.ScaryPotter7:
            case GameMode.ScaryPotter8:
            case GameMode.ScaryPotter9:
            case GameMode.ScaryPotterEndless:
            case GameMode.PuzzleIZombie1:
            case GameMode.PuzzleIZombie2:
            case GameMode.PuzzleIZombie3:
            case GameMode.PuzzleIZombie4:
            case GameMode.PuzzleIZombie5:
            case GameMode.PuzzleIZombie6:
            case GameMode.PuzzleIZombie7:
            case GameMode.PuzzleIZombie8:
            case GameMode.PuzzleIZombie9:
            case GameMode.PuzzleIZombieEndless:
                mBackground = BackgroundType.Num2Night;
                break;
            case GameMode.SurvivalNormalStage3:
            case GameMode.SurvivalHardStage3:
            case GameMode.SurvivalEndlessStage3:
            case GameMode.ChallengeLittleTrouble:
            case GameMode.ChallengeBobsledBonanza:
            case GameMode.ChallengeSpeed:
            case GameMode.ChallengeLastStand:
            case GameMode.ChallengeWarAndPeas2:
            case GameMode.Upsell:
            case GameMode.Intro:
                mBackground = BackgroundType.Num3Pool;
                break;
            case GameMode.SurvivalNormalStage4:
            case GameMode.SurvivalHardStage4:
            case GameMode.SurvivalEndlessStage4:
            case GameMode.ChallengeRainingSeeds:
            case GameMode.ChallengeInvisighoul:
            case GameMode.ChallengeAirRaid:
            case GameMode.ChallengeStormyNight:
                mBackground = BackgroundType.Num4Fog;
                break;
            case GameMode.SurvivalNormalStage5:
            case GameMode.SurvivalHardStage5:
            case GameMode.SurvivalEndlessStage5:
            case GameMode.ChallengeColumn:
            case GameMode.ChallengePogoParty:
            case GameMode.ChallengeHighGravity:
            case GameMode.ChallengeBungeeBlitz:
                mBackground = BackgroundType.Num5Roof;
                break;
            case GameMode.ChallengeFinalBoss:
                mBackground = BackgroundType.Num6Boss;
                break;
            case GameMode.ChallengeZenGarden:
                mBackground = BackgroundType.Greenhouse;
                break;
            case GameMode.TreeOfWisdom:
                mBackground = BackgroundType.TreeOfWisdom;
                break;
            case GameMode.ChallengeZombiquarium:
                mBackground = BackgroundType.Zombiquarium;
                break;
            default:
                Debug.ASSERT(false);
                break;
            }
            LoadBackgroundImages();
            if (mBackground == BackgroundType.Num1Day || mBackground == BackgroundType.Greenhouse || mBackground == BackgroundType.TreeOfWisdom)
            {
                mPlantRow[0] = PlantRowType.Normal;
                mPlantRow[1] = PlantRowType.Normal;
                mPlantRow[2] = PlantRowType.Normal;
                mPlantRow[3] = PlantRowType.Normal;
                mPlantRow[4] = PlantRowType.Normal;
                mPlantRow[5] = PlantRowType.Dirt;
                if (mApp.IsAdventureMode() && mApp.IsFirstTimeAdventureMode())
                {
                    if (mLevel == 1)
                    {
                        mPlantRow[0] = PlantRowType.Dirt;
                        mPlantRow[1] = PlantRowType.Dirt;
                        mPlantRow[3] = PlantRowType.Dirt;
                        mPlantRow[4] = PlantRowType.Dirt;
                    }
                    else if (mLevel == 2 || mLevel == 3)
                    {
                        mPlantRow[0] = PlantRowType.Dirt;
                        mPlantRow[4] = PlantRowType.Dirt;
                    }
                }
                else if (mApp.mGameMode == GameMode.ChallengeResodded)
                {
                    mPlantRow[0] = PlantRowType.Dirt;
                    mPlantRow[4] = PlantRowType.Dirt;
                }
            }
            else if (mBackground == BackgroundType.Num2Night)
            {
                mPlantRow[0] = PlantRowType.Normal;
                mPlantRow[1] = PlantRowType.Normal;
                mPlantRow[2] = PlantRowType.Normal;
                mPlantRow[3] = PlantRowType.Normal;
                mPlantRow[4] = PlantRowType.Normal;
                mPlantRow[5] = PlantRowType.Dirt;
            }
            else if (mBackground == BackgroundType.Num3Pool || mBackground == BackgroundType.Zombiquarium)
            {
                mPlantRow[0] = PlantRowType.Normal;
                mPlantRow[1] = PlantRowType.Normal;
                mPlantRow[2] = PlantRowType.Pool;
                mPlantRow[3] = PlantRowType.Pool;
                mPlantRow[4] = PlantRowType.Normal;
                mPlantRow[5] = PlantRowType.Normal;
            }
            else if (mBackground == BackgroundType.Num4Fog)
            {
                mPlantRow[0] = PlantRowType.Normal;
                mPlantRow[1] = PlantRowType.Normal;
                mPlantRow[2] = PlantRowType.Pool;
                mPlantRow[3] = PlantRowType.Pool;
                mPlantRow[4] = PlantRowType.Normal;
                mPlantRow[5] = PlantRowType.Normal;
            }
            else if (mBackground == BackgroundType.Num5Roof || mBackground == BackgroundType.Num6Boss)
            {
                mPlantRow[0] = PlantRowType.Normal;
                mPlantRow[1] = PlantRowType.Normal;
                mPlantRow[2] = PlantRowType.Normal;
                mPlantRow[3] = PlantRowType.Normal;
                mPlantRow[4] = PlantRowType.Normal;
                mPlantRow[5] = PlantRowType.Dirt;
            }
            else
            {
                Debug.ASSERT(false);
            }
            for (int i = 0; i < Constants.GRIDSIZEX; i++)
            {
                for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
                {
                    if (mPlantRow[j] == PlantRowType.Dirt)
                    {
                        mGridSquareType[i, j] = GridSquareType.Dirt;
                    }
                    else if (mPlantRow[j] == PlantRowType.Pool && i >= 0 && i <= 8)
                    {
                        mGridSquareType[i, j] = GridSquareType.Pool;
                    }
                    else if (mPlantRow[j] == PlantRowType.HighGround && i >= 4 && i <= 8)
                    {
                        mGridSquareType[i, j] = GridSquareType.HighGround;
                    }
                }
            }
            int levelRandSeed = GetLevelRandSeed();
            RandomNumbers.Seed(levelRandSeed);
            if (StageHasGraveStones())
            {
                if (mApp.mGameMode == GameMode.ChallengeGraveDanger)
                {
                    AddGraveStones(6, TodCommon.RandRangeInt(1, 2));
                    AddGraveStones(7, TodCommon.RandRangeInt(1, 3));
                    AddGraveStones(8, TodCommon.RandRangeInt(2, 3));
                }
                else if (mApp.IsWhackAZombieLevel())
                {
                    mChallenge.WhackAZombiePlaceGraves(9);
                }
                else if (mBackground == BackgroundType.Num2Night)
                {
                    if (mApp.IsSurvivalNormal(mApp.mGameMode))
                    {
                        AddGraveStones(5, 1);
                        AddGraveStones(6, 1);
                        AddGraveStones(7, 1);
                        AddGraveStones(8, 2);
                    }
                    else if (!mApp.IsAdventureMode() && !mApp.IsQuickPlayMode())
                    {
                        AddGraveStones(4, 1);
                        AddGraveStones(5, 1);
                        AddGraveStones(6, 2);
                        AddGraveStones(7, 2);
                        AddGraveStones(8, 3);
                    }
                    else if (mLevel == 11 || mLevel == 12 || mLevel == 13)
                    {
                        AddGraveStones(6, 1);
                        AddGraveStones(7, 1);
                        AddGraveStones(8, 2);
                    }
                    else if (mLevel == 14 || mLevel == 16 || mLevel == 18)
                    {
                        AddGraveStones(5, 1);
                        AddGraveStones(6, 1);
                        AddGraveStones(7, 2);
                        AddGraveStones(8, 3);
                    }
                    else if (mLevel == 17 || mLevel == 19)
                    {
                        AddGraveStones(4, 1);
                        AddGraveStones(5, 2);
                        AddGraveStones(6, 2);
                        AddGraveStones(7, 3);
                        AddGraveStones(8, 3);
                    }
                    else if (mLevel >= 20)
                    {
                        AddGraveStones(3, 1);
                        AddGraveStones(4, 2);
                        AddGraveStones(5, 2);
                        AddGraveStones(6, 2);
                        AddGraveStones(7, 3);
                        AddGraveStones(8, 3);
                    }
                    else
                    {
                        Debug.ASSERT(false);
                    }
                }
            }
            PickSpecialGraveStone();
        }

        public void InitZombieWaves()
        {
            Debug.ASSERT(true);
            if (mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
            {
                InitZombieWavesForLevel(mLevel);
            }
            else
            {
                mChallenge.InitZombieWaves();
            }
            PickZombieWaves();
            Debug.ASSERT(IsZombieWaveDistributionOk());
            mCurrentWave = 0;
            mTotalSpawnedWaves = 0;
            mApp.mKilledYetiAndRestarted = false;
            if (mApp.IsFirstTimeAdventureMode() && mLevel == 2)
            {
                mZombieCountDown = 4998;
            }
            else if (mApp.IsSurvivalMode() && mChallenge.mSurvivalStage > 0)
            {
                mZombieCountDown = 600;
            }
            else
            {
                mZombieCountDown = 1800;
            }
            mZombieCountDownStart = mZombieCountDown;
            mZombieHealthToNextWave = -1;
            mZombieHealthWaveStart = 0;
            mLastBungeeWave = 0;
            mProgressMeterWidth = 0;
            mHugeWaveCountDown = 0;
            mLevelAwardSpawned = false;
        }

        public void InitSurvivalStage()
        {
            RefreshSeedPacketFromCursor();
            mApp.mSoundSystem.GamePause(true);
            FreezeEffectsForCutscene(true);
            mLevelComplete = false;
            InitZombieWaves();
            mApp.mGameScene = GameScenes.LevelIntro;
            mApp.ShowSeedChooserScreen();
            mCutScene.StartLevelIntro();
            mSeedBank.UpdateHeight();
            for (int i = 0; i < 9; i++)
            {
                SeedPacket seedPacket = mSeedBank.mSeedPackets[i];
                seedPacket.mY = GetSeedPacketPositionY(i);
                seedPacket.mPacketType = SeedType.None;
            }
            if (StageHasFog())
            {
                mFogBlownCountDown = 2000;
            }
            for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
            {
                mWaveRowGotLawnMowered[j] = -100;
            }
        }

        public static int MakeRenderOrder(RenderLayer theRenderLayer, int theRow, int theLayerOffset)
        {
            return (int)(theRenderLayer + theLayerOffset + 10000 * theRow);
        }

        public void UpdateGame()
        {
            UpdateGameObjects();
            if (StageHasFog() && mFogBlownCountDown > 0)
            {
                float num = 1065f - LeftFogColumn() * 80f + Constants.BOARD_EXTRA_ROOM;
                if (mApp.mGameScene == GameScenes.LevelIntro)
                {
                    mFogOffset = TodCommon.TodAnimateCurveFloat(200, 0, mFogBlownCountDown, num, 0f, TodCurves.EaseOut);
                }
                else if (mFogBlownCountDown < 2000)
                {
                    mFogOffset = TodCommon.TodAnimateCurveFloat(2000, 0, mFogBlownCountDown, num, 0f, TodCurves.EaseOut);
                }
                else if (mFogOffset < num)
                {
                    mFogOffset = TodCommon.TodAnimateCurveFloat(-5, (int)num, (int)(mFogOffset * 1.1f), 0f, num, TodCurves.Linear);
                }
            }
            if (mApp.mGameScene != GameScenes.Playing && !mCutScene.ShouldRunUpsellBoard())
            {
                return;
            }
            mMainCounter += 3;
            UpdateSunSpawning();
            UpdateZombieSpawning();
            UpdateIce();
            if (mIceTrapCounter > 0)
            {
                mIceTrapCounter -= 3;
                if (mIceTrapCounter >= 0 && mIceTrapCounter < 3)
                {
                    TodParticleSystem todParticleSystem = mApp.ParticleTryToGet(mPoolSparklyParticleID);
                    if (todParticleSystem != null)
                    {
                        todParticleSystem.mDontUpdate = false;
                    }
                }
            }
            if (mFogBlownCountDown > 0)
            {
                mFogBlownCountDown -= 3;
            }
            if (mMainCounter == 3)
            {
                if (mApp.IsFirstTimeAdventureMode() && mLevel == 1)
                {
                    SetTutorialState(TutorialState.Level1PickUpPeashooter);
                }
                else if (mApp.IsFirstTimeAdventureMode() && mLevel == 2)
                {
                    SetTutorialState(TutorialState.Level2PickUpSunflower);
                    DisplayAdvice("[ADVICE_PLANT_SUNFLOWER1]", MessageStyle.TutorialLevel2, AdviceType.None);
                    mTutorialTimer = 500;
                }
            }
            UpdateProgressMeter();
        }

        public void InitZombieWavesForLevel(int aForLevel)
        {
            if (mApp.IsWhackAZombieLevel())
            {
                mChallenge.InitZombieWaves();
                return;
            }
            if (mApp.IsWallnutBowlingLevel() && !mApp.IsFirstTimeAdventureMode())
            {
                mChallenge.InitZombieWaves();
                return;
            }
            for (int i = 0; i < 33; i++)
            {
                mZombieAllowed[i] = Board.CanZombieSpawnOnLevel((ZombieType)i, aForLevel);
            }
        }

        public uint SeedNotRecommendedForLevel(SeedType theSeedType)
        {
            uint result = 0U;
            if (Plant.IsNocturnal(theSeedType) && !StageIsNight())
            {
                TodCommon.SetBit(ref result, 0, 1);
            }
            if (theSeedType == SeedType.InstantCoffee && StageIsNight())
            {
                TodCommon.SetBit(ref result, 7, 1);
            }
            if (theSeedType == SeedType.Gravebuster && !StageHasGraveStones())
            {
                TodCommon.SetBit(ref result, 2, 1);
            }
            if (theSeedType == SeedType.Plantern && !StageHasFog())
            {
                TodCommon.SetBit(ref result, 3, 1);
            }
            if (theSeedType == SeedType.Flowerpot && !StageHasRoof())
            {
                TodCommon.SetBit(ref result, 4, 1);
            }
            if (StageHasRoof() && (theSeedType == SeedType.Spikeweed || theSeedType == SeedType.Spikerock))
            {
                TodCommon.SetBit(ref result, 5, 1);
            }
            if (!StageHasPool() && (theSeedType == SeedType.Lilypad || theSeedType == SeedType.Tanglekelp || theSeedType == SeedType.Seashroom || theSeedType == SeedType.Cattail))
            {
                TodCommon.SetBit(ref result, 1, 1);
            }
            return result;
        }

        public void DrawTopRightUI(Graphics g)
        {
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                if (mChallenge.mChallengeState == ChallengeState.ZenFading)
                {
                    mMenuButton.mY = TodCommon.TodAnimateCurve(50, 0, mChallenge.mChallengeStateCounter, 2, -50, TodCurves.EaseInOut);
                    mStoreButton.mX = TodCommon.TodAnimateCurve(50, 0, mChallenge.mChallengeStateCounter, Constants.ZenGardenStoreButtonX, 800, TodCurves.EaseInOut);
                }
                else
                {
                    mMenuButton.mY = 2;
                    mStoreButton.mX = Constants.ZenGardenStoreButtonX;
                }
            }
            if (mTutorialState == TutorialState.ZenGardenCompleted)
            {
                SexyColor flashingColor = TodCommon.GetFlashingColor(mMainCounter, 75);
                g.SetColorizeImages(true);
                g.SetColor(flashingColor);
            }
            mMenuButton.Draw(g);
            g.SetColorizeImages(false);
            if (mStoreButton != null && mApp.mGameMode != GameMode.ChallengeLastStand)
            {
                if (mTutorialState == TutorialState.ZenGardenVisitStore)
                {
                    SexyColor flashingColor2 = TodCommon.GetFlashingColor(mMainCounter, 75);
                    g.SetColorizeImages(true);
                    g.SetColor(flashingColor2);
                }
                mStoreButton.Draw(g);
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
                    int num = mGridCelFog[i, j];
                    if (num != 0)
                    {
                        int num2 = mGridCelLook[i, j % Constants.MAX_GRIDSIZEY];
                        int theCelCol = num2 % 8;
                        float num3 = i * 80 + mFogOffset - 15f;
                        float num4 = j * 85 + 20f;
                        int num5 = (int)(255f - num2 * 1.5f);
                        int num6 = 255 - num2;
                        float num7 = mMainCounter * 3.1415927f * 2f / 900f;
                        float num8 = mMainCounter * 3.1415927f * 2f / 500f;
                        float num9 = 3f * i * 3.1415927f * 2f / Constants.GRIDSIZEX;
                        float num10 = 3f * j * 3.1415927f * 2f / 7f;
                        float num11 = (float)(13.0 + 4.0 * Math.Sin(num10 + num7) + 8.0 * Math.Sin(num9 + num8));
                        num5 -= (int)(num11 * 1.5f);
                        num6 -= (int)num11;
                        g.SetColorizeImages(true);
                        g.SetColor(new SexyColor(num5, num5, num6, num));
                        g.DrawImageCel(image_FOG, (int)(num3 * Constants.S), (int)(num4 * Constants.S), theCelCol, 0);
                        if (i == Constants.GRIDSIZEX - 1)
                        {
                            int num12 = 120;
                            g.DrawImageCel(image_FOG, (int)((num3 + num12) * Constants.S), (int)(num4 * Constants.S), theCelCol, 0);
                        }
                        g.SetColorizeImages(false);
                    }
                }
            }
        }

        public void UpdateFog()
        {
            if (!StageHasFog())
            {
                return;
            }
            int num = 3;
            if (mFogBlownCountDown > 0 && mFogBlownCountDown < 2000)
            {
                num = 1;
            }
            else if (mFogBlownCountDown > 0)
            {
                num = 20;
            }
            int num2 = LeftFogColumn();
            for (int i = num2; i < Constants.GRIDSIZEX; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    int num3 = 255;
                    if (i == num2)
                    {
                        num3 = 200;
                    }
                    mGridCelFog[i, j] = Math.Min(mGridCelFog[i, j] + num, num3);
                }
            }
            int count = mPlants.Count;
            for (int k = 0; k < count; k++)
            {
                Plant plant = mPlants[k];
                if (!plant.mDead && !plant.NotOnGround())
                {
                    if (plant.mSeedType == SeedType.Plantern)
                    {
                        ClearFogAroundPlant(plant, 4);
                    }
                    else if (plant.mSeedType == SeedType.Torchwood)
                    {
                        ClearFogAroundPlant(plant, 1);
                    }
                }
            }
        }

        public int LeftFogColumn()
        {
            if (mApp.mGameMode == GameMode.ChallengeAirRaid)
            {
                return 6;
            }
            if (!mApp.IsAdventureMode() && !mApp.IsQuickPlayMode())
            {
                return 5;
            }
            if (mLevel == 31)
            {
                return 6;
            }
            if (mLevel >= 32 && mLevel <= 36)
            {
                return 5;
            }
            if (mLevel >= 37 && mLevel <= 40)
            {
                return 4;
            }
            Debug.ASSERT(false);
            return -666;
        }

        public static bool IsZombieTypePoolOnly(ZombieType theZombieType)
        {
            return theZombieType == ZombieType.Snorkel || theZombieType == ZombieType.DolphinRider;
        }

        public void DropLootPiece(int thePosX, int thePosY, int theDropFactor)
        {
            if (mApp.IsFirstTimeAdventureMode() && mLevel == 22 && mCurrentWave > 5 && !mApp.mPlayerInfo.mHasUnlockedMinigames && CountCoinByType(CoinType.PresentMinigames) == 0)
            {
                mApp.PlayFoley(FoleyType.ArtChallenge);
                AddCoin(thePosX - 40, thePosY, CoinType.PresentMinigames, CoinMotion.Coin);
                return;
            }
            if (mApp.IsFirstTimeAdventureMode() && mLevel == 36 && mCurrentWave > 5 && !mApp.mPlayerInfo.mHasUnlockedPuzzleMode && CountCoinByType(CoinType.PresentPuzzleMode) == 0)
            {
                mApp.PlayFoley(FoleyType.ArtChallenge);
                AddCoin(thePosX - 40, thePosY, CoinType.PresentPuzzleMode, CoinMotion.Coin);
                return;
            }
            int num = RandomNumbers.NextNumber(30000);
            if (mApp.IsFirstTimeAdventureMode() && mLevel == 11 && !mDroppedFirstCoin && mCurrentWave > 5)
            {
                num = 1000;
            }
            if (mApp.mGameMode == GameMode.ChallengeColumn)
            {
                num *= 5;
            }
            if (mApp.IsWhackAZombieLevel())
            {
                int num2 = 2500;
                int num3;
                if (mSunMoney > 500)
                {
                    num3 = num2 + 300;
                }
                else if (mSunMoney > 350)
                {
                    num3 = num2 + 600;
                }
                else if (mSunMoney > 200)
                {
                    num3 = num2 + 1200;
                }
                else
                {
                    num3 = num2 + 2500;
                }
                if (num >= num2 * theDropFactor && num < num3 * theDropFactor)
                {
                    mApp.PlayFoley(FoleyType.SpawnSun);
                    AddCoin(thePosX - 20, thePosY, CoinType.Sun, CoinMotion.Coin);
                    AddCoin(thePosX - 40, thePosY, CoinType.Sun, CoinMotion.Coin);
                    AddCoin(thePosX - 60, thePosY, CoinType.Sun, CoinMotion.Coin);
                    return;
                }
            }
            if (mTotalSpawnedWaves > 70)
            {
                return;
            }
            int num4;
            if (!mApp.mZenGarden.CanDropPottedPlantLoot())
            {
                num4 = 0;
            }
            else if ((mApp.IsAdventureMode() || mApp.IsQuickPlayMode()) && !mApp.IsFirstTimeAdventureMode())
            {
                num4 = 24;
            }
            else if (mApp.IsSurvivalEndless(mApp.mGameMode))
            {
                num4 = 3;
            }
            else
            {
                num4 = 12;
            }
            int num5 = num4;
            if (!mApp.mZenGarden.CanDropChocolate())
            {
                //num5 = num5;
            }
            else if ((mApp.IsAdventureMode() || mApp.IsQuickPlayMode()) && !mApp.IsFirstTimeAdventureMode())
            {
                num5 += 72;
            }
            else if (mApp.IsSurvivalEndless(mApp.mGameMode))
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
                coinType = CoinType.PresentPlant;
            }
            else if (num < num5 * theDropFactor)
            {
                coinType = CoinType.Chocolate;
            }
            else if (num < num6 * theDropFactor)
            {
                coinType = CoinType.Diamond;
            }
            else if (num < num7 * theDropFactor)
            {
                coinType = CoinType.Gold;
            }
            else
            {
                if (num >= num8 * theDropFactor)
                {
                    return;
                }
                coinType = CoinType.Silver;
            }
            if (coinType == CoinType.Diamond && mApp.mPlayerInfo.mPurchases[21] < 1)
            {
                coinType = CoinType.Gold;
            }
            if (mApp.IsWallnutBowlingLevel() && (coinType == CoinType.Silver || coinType == CoinType.Gold || coinType == CoinType.Diamond))
            {
                return;
            }
            if (mApp.IsFirstTimeAdventureMode() && mLevel == 11)
            {
                int num9 = Coin.GetCoinValue(CoinType.Gold) * mLawnMowers.Count;
                int itemCost = StoreScreen.GetItemCost(StoreItem.STORE_ITEM_PACKET_UPGRADE);
                int num10 = mApp.mPlayerInfo.mCoins + CountCoinsBeingCollected();
                if (Coin.GetCoinValue(coinType) + num10 + num9 >= itemCost)
                {
                    return;
                }
            }
            mApp.PlayFoley(FoleyType.SpawnSun);
            AddCoin(thePosX - 40, thePosY, coinType, CoinMotion.Coin);
            mDroppedFirstCoin = true;
        }

        public void UpdateLevelEndSequence()
        {
            if (mNextSurvivalStageCounter > 0)
            {
                if (!IsScaryPotterDaveTalking())
                {
                    mNextSurvivalStageCounter--;
                    if (mApp.IsAdventureMode() && mApp.IsScaryPotterLevel() && mNextSurvivalStageCounter == 300)
                    {
                        mApp.CrazyDaveEnter();
                        if (mChallenge.mSurvivalStage == 0)
                        {
                            mApp.CrazyDaveTalkIndex(2700);
                        }
                        else
                        {
                            mApp.CrazyDaveTalkIndex(2800);
                        }
                        mChallenge.PuzzleNextStageClear();
                        mNextSurvivalStageCounter = 100;
                    }
                }
                if (mNextSurvivalStageCounter == 1 && mApp.IsSurvivalMode() && mApp.IsSurvivalMode())
                {
                    TryToSaveGame();
                }
                if (mNextSurvivalStageCounter == 0)
                {
                    if (mApp.IsScaryPotterLevel() && mApp.IsAdventureMode())
                    {
                        return;
                    }
                    if (mApp.IsScaryPotterLevel() && !IsFinalScaryPotterStage())
                    {
                        mChallenge.PuzzleNextStageClear();
                        mChallenge.ScaryPotterPopulate();
                        return;
                    }
                    if (mApp.IsEndlessIZombie(mApp.mGameMode))
                    {
                        mChallenge.PuzzleNextStageClear();
                        mChallenge.IZombieInitLevel();
                        return;
                    }
                    if (mApp.mGameMode == GameMode.ChallengeLastStand)
                    {
                        ClearAdvice(AdviceType.None);
                        return;
                    }
                    mLevelComplete = true;
                    RemoveZombiesForRepick();
                    return;
                }
            }
            if (mBoardFadeOutCounter < 0)
            {
                return;
            }
            mBoardFadeOutCounter--;
            if (mBoardFadeOutCounter == 0)
            {
                mLevelComplete = true;
                return;
            }
            if (mBoardFadeOutCounter == 300)
            {
                bool flag = IsSurvivalStageWithRepick();
                bool flag2 = mLevel == 9 || mLevel == 19 || mLevel == 29 || mLevel == 39 || mLevel == 49;
                if (!flag && !flag2)
                {
                    mApp.PlaySample(Resources.SOUND_LIGHTFILL);
                }
            }
            if (mScoreNextMowerCounter > 0)
            {
                mScoreNextMowerCounter--;
                if (mScoreNextMowerCounter != 0)
                {
                    return;
                }
            }
            if (!CanDropLoot())
            {
                return;
            }
            if (IsSurvivalStageWithRepick())
            {
                return;
            }
            mScoreNextMowerCounter = 40;
            LawnMower bottomLawnMower = GetBottomLawnMower();
            if (bottomLawnMower == null)
            {
                return;
            }
            CoinType theCoinType = CoinType.Gold;
            AddCoin((int)(bottomLawnMower.mPosX + Constants.LawnMower_Coin_Offset.X), (int)(bottomLawnMower.mPosY + Constants.LawnMower_Coin_Offset.Y), theCoinType, CoinMotion.LawnmowerCoin);
            SoundInstance soundInstance = mApp.mSoundManager.GetSoundInstance((uint)Resources.SOUND_POINTS);
            if (soundInstance != null)
            {
                soundInstance.Play(false, true);
                float num = TodCommon.ClampFloat(6f - CountUntriggerLawnMowers(), 0f, 6f);
                soundInstance.AdjustPitch(num);
            }
            else
            {
                Debug.Log(DebugType.Error, "FAILED TO PLAY SOUND INSTANCE");
            }
            bottomLawnMower.Die();
        }

        public LawnMower GetBottomLawnMower()
        {
            LawnMower lawnMower = null;
            LawnMower lawnMower2 = null;
            while (IterateLawnMowers(ref lawnMower2))
            {
                if (lawnMower2.mMowerState != LawnMowerState.Triggered && lawnMower2.mMowerState != LawnMowerState.Squished && (lawnMower == null || lawnMower.mRow < lawnMower2.mRow))
                {
                    lawnMower = lawnMower2;
                }
            }
            return lawnMower;
        }

        public bool CanDropLoot()
        {
            return !mCutScene.ShouldRunUpsellBoard() && (!mApp.IsFirstTimeAdventureMode() || mLevel >= 11);
        }

        public ZombieType GetIntroducedZombieType()
        {
            if ((!mApp.IsAdventureMode() && !mApp.IsQuickPlayMode()) || mLevel == 1)
            {
                return ZombieType.Invalid;
            }
            for (int i = 0; i < 33; i++)
            {
                ZombieType zombieType = (ZombieType)i;
                ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(zombieType);
                if ((zombieType != ZombieType.Yeti || mApp.CanSpawnYetis()) && zombieDefinition.mStartingLevel == mLevel)
                {
                    return zombieType;
                }
            }
            return ZombieType.Invalid;
        }

        public void PickSpecialGraveStone()
        {
            int num = 0;
            int num2 = -1;
            GridItem gridItem = null;
            while (IterateGridItems(ref gridItem, ref num2))
            {
                if (gridItem.mGridItemType == GridItemType.Gravestone)
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
            gridItem2.mGridItemState = GridItemState.GravestoneSpecial;
        }

        public float GetPosYBasedOnRow(float thePosX, int theRow)
        {
            if (StageHasRoof())
            {
                float num = 0f;
                int num2 = 440 + Constants.BOARD_EXTRA_ROOM;
                if (thePosX < num2)
                {
                    num = (num2 - thePosX) * 0.25f;
                }
                return GridToPixelY(8, theRow) + num;
            }
            return GridToPixelY(0, theRow);
        }

        public void NextWaveComing()
        {
            if (mCurrentWave + 1 == mNumWaves)
            {
                bool flag = true;
                if (IsSurvivalStageWithRepick())
                {
                    flag = false;
                }
                else if (mApp.mGameMode == GameMode.ChallengeLastStand)
                {
                    flag = false;
                }
                else if (mApp.IsContinuousChallenge())
                {
                    flag = false;
                }
                if (flag)
                {
                    mApp.AddReanimation(0f + Constants.BOARD_EXTRA_ROOM, 30f, 800000, ReanimationType.FinalWave);
                    mFinalWaveSoundCounter = 60;
                }
            }
            if (mCurrentWave == 0)
            {
                mApp.PlaySample(Resources.SOUND_AWOOGA);
                return;
            }
            if (mApp.IsWhackAZombieLevel())
            {
                if (mCurrentWave == mNumWaves - 1)
                {
                    mApp.PlaySample(Resources.SOUND_SIREN);
                    return;
                }
            }
            else if (IsFlagWave(mCurrentWave))
            {
                mApp.PlaySample(Resources.SOUND_SIREN);
            }
        }

        public bool BungeeIsTargetingCell(int theCol, int theRow)
        {
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && !zombie.IsDeadOrDying() && zombie.mZombieType == ZombieType.Bungee && zombie.mRow == theRow && zombie.mTargetCol == theCol)
                {
                    return true;
                }
            }
            return false;
        }

        public int PlantingPixelToGridX(int theX, int theY, SeedType theSeedType)
        {
            int theY2 = theY;
            OffsetYForPlanting(ref theY2, theSeedType);
            return PixelToGridX(theX, theY2);
        }

        public int PlantingPixelToGridY(int theX, int theY, SeedType theSeedType)
        {
            int num = theY;
            OffsetYForPlanting(ref num, theSeedType);
            if (theSeedType == SeedType.InstantCoffee)
            {
                int theGridX = PixelToGridX(theX, num);
                int num2 = PixelToGridY(theX, num);
                Plant topPlantAt = GetTopPlantAt(theGridX, num2, TopPlant.OnlyNormalPosition);
                if (topPlantAt != null && topPlantAt.mIsAsleep)
                {
                    return num2;
                }
                num2 = PixelToGridY(theX, num + 30);
                Plant topPlantAt2 = GetTopPlantAt(theGridX, num2, TopPlant.OnlyNormalPosition);
                if (topPlantAt2 != null && topPlantAt2.mIsAsleep)
                {
                    return num2;
                }
                num2 = PixelToGridY(theX, num - 50);
                Plant topPlantAt3 = GetTopPlantAt(theGridX, num2, TopPlant.OnlyNormalPosition);
                if (topPlantAt3 != null && topPlantAt3.mIsAsleep)
                {
                    return num2;
                }
            }
            return PixelToGridY(theX, num);
        }

        public Plant FindUmbrellaPlant(int theGridX, int theGridY)
        {
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead && plant.mSeedType == SeedType.Umbrella && !plant.NotOnGround() && theGridX >= plant.mPlantCol - 1 && theGridX <= plant.mPlantCol + 1 && theGridY >= plant.mRow - 1 && theGridY <= plant.mRow + 1)
                {
                    return plant;
                }
            }
            return null;
        }

        public void SetTutorialState(TutorialState theTutorialState)
        {
            if (theTutorialState == TutorialState.Level1PickUpPeashooter)
            {
                if (mPlants.Count == 0)
                {
                    float num = mSeedBank.mX + mSeedBank.mSeedPackets[0].mX + Constants.SMALL_SEEDPACKET_WIDTH / 2 - Constants.InvertAndScale(13f);
                    float num2 = 0f;
                    TutorialArrowShow((int)num, (int)num2);
                    DisplayAdvice("[ADVICE_CLICK_SEED_PACKET]", MessageStyle.TutorialLevel1Stay, AdviceType.None);
                }
                else
                {
                    DisplayAdvice("[ADVICE_ENOUGH_SUN]", MessageStyle.TutorialLevel1Stay, AdviceType.None);
                    mTutorialTimer = 400;
                }
            }
            else if (theTutorialState == TutorialState.Level1PlantPeashooter)
            {
                mTutorialTimer = -1;
                TutorialArrowRemove();
                if (mPlants.Count == 0)
                {
                    DisplayAdvice("[ADVICE_CLICK_ON_GRASS]", MessageStyle.TutorialLevel1Stay, AdviceType.None);
                }
                else
                {
                    ClearAdvice(AdviceType.None);
                }
            }
            else if (theTutorialState == TutorialState.Level1RefreshPeashooter)
            {
                DisplayAdvice("[ADVICE_PLANTED_PEASHOOTER]", MessageStyle.TutorialLevel1Stay, AdviceType.None);
                mSunCountDown = 400;
            }
            else if (theTutorialState == TutorialState.Level1Completed)
            {
                DisplayAdvice("[ADVICE_ZOMBIE_ONSLAUGHT]", MessageStyle.TutorialLevel1, AdviceType.None);
                mZombieCountDown = 99;
                mZombieCountDownStart = mZombieCountDown;
            }
            else if (theTutorialState == TutorialState.Level2PickUpSunflower || theTutorialState == TutorialState.MoresunPickUpSunflower)
            {
                float num3 = mSeedBank.mX + mSeedBank.mSeedPackets[1].mX + Constants.InvertAndScale(13f);
                float num4 = mSeedBank.mY + mSeedBank.mSeedPackets[1].mY;
                TutorialArrowShow((int)num3, (int)num4);
            }
            else if (theTutorialState == TutorialState.Level2PlantSunflower || theTutorialState == TutorialState.Level2RefreshSunflower || theTutorialState == TutorialState.MoresunPlantSunflower || theTutorialState == TutorialState.MoresunRefreshSunflower)
            {
                TutorialArrowRemove();
            }
            else if (theTutorialState == TutorialState.Level2Completed)
            {
                if (mCurrentWave == 0)
                {
                    mZombieCountDown = 999;
                    mZombieCountDownStart = mZombieCountDown;
                }
            }
            else if (theTutorialState == TutorialState.SlotMachinePull)
            {
                DisplayAdvice("[ADVICE_SLOT_MACHINE_PULL]", MessageStyle.SlotMachine, AdviceType.SlotMachinePull);
            }
            else if (theTutorialState == TutorialState.SlotMachineCompleted)
            {
                ClearAdvice(AdviceType.SlotMachinePull);
            }
            else if (theTutorialState == TutorialState.ShovelPickup)
            {
                DisplayAdvice("[ADVICE_CLICK_SHOVEL]", MessageStyle.HintStay, AdviceType.None);
                int x = GetShovelButtonRect().mX + (int)Constants.InvertAndScale(16f) + Constants.Board_Offset_AspectRatio_Correction;
                int y = 0;
                TutorialArrowShow(x, y);
            }
            else if (theTutorialState == TutorialState.ShovelDig)
            {
                DisplayAdvice("[ADVICE_CLICK_PLANT]", MessageStyle.HintStay, AdviceType.None);
                TutorialArrowRemove();
            }
            else if (theTutorialState == TutorialState.ShovelKeepDigging)
            {
                DisplayAdvice("[ADVICE_KEEP_DIGGING]", MessageStyle.HintStay, AdviceType.None);
            }
            else if (theTutorialState == TutorialState.ShovelCompleted)
            {
                ClearAdvice(AdviceType.None);
                mCutScene.mCutsceneTime = 1500;
                mCutScene.mCrazyDaveDialogStart = 2410;
            }
            mTutorialState = theTutorialState;
        }

        public void DoFwoosh(int theRow)
        {
            for (int i = 0; i < 12; i++)
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mFwooshID[theRow, i]);
                if (reanimation != null)
                {
                    reanimation.ReanimationDie();
                }
                float num = 750f * i / 11f + 10f + Constants.BOARD_EXTRA_ROOM;
                float theY = GetPosYBasedOnRow(num + 10f, theRow) - 10f;
                int aRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, theRow, 1);
                Reanimation reanimation2 = mApp.AddReanimation(num, theY, aRenderOrder, ReanimationType.JalapenoFire);
                reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_flame);
                reanimation2.mLoopType = ReanimLoopType.LoopFullLastFrame;
                reanimation2.mAnimRate *= TodCommon.RandRangeFloat(0.7f, 1.3f);
                float num2 = TodCommon.RandRangeFloat(0.9f, 1.1f);
                float num3 = 1f;
                if (RandomNumbers.NextNumber(2) == 0)
                {
                    num3 = -1f;
                }
                reanimation2.OverrideScale(num2 * num3, 1f);
                mFwooshID[theRow, i] = mApp.ReanimationGetID(reanimation2);
            }
            mFwooshCountDown = 100;
        }

        public void UpdateFwoosh()
        {
            if (mFwooshCountDown == 0)
            {
                return;
            }
            mFwooshCountDown -= 3;
            int num = TodCommon.TodAnimateCurve(50, 0, mFwooshCountDown, 12, 0, TodCurves.Linear);
            for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
            {
                for (int j = 0; j < 12 - num; j++)
                {
                    Reanimation reanimation = mApp.ReanimationTryToGet(mFwooshID[i, j]);
                    if (reanimation != null)
                    {
                        reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_done);
                        reanimation.mLoopType = ReanimLoopType.PlayOnceFullLastFrame;
                        reanimation.mAnimRate = 15f;
                    }
                    mFwooshID[i, j] = null;
                }
            }
        }

        public Plant SpecialPlantHitTest(int x, int y)
        {
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead)
                {
                    if (plant.mSeedType == SeedType.Pumpkinshell)
                    {
                        float num = 25f;
                        if (GetTopPlantAt(plant.mPlantCol, plant.mRow, TopPlant.OnlyNormalPosition) == null)
                        {
                            num = 0f;
                        }
                        float num2 = TodCommon.Distance2D(x, y, plant.mX + 40f, plant.mY + 40f);
                        if (num2 >= num && num2 <= 50f && y > plant.mY + 25f)
                        {
                            return plant;
                        }
                    }
                    if (Plant.IsFlying(plant.mSeedType))
                    {
                        float num3 = TodCommon.Distance2D(x, y, plant.mX + 40f, plant.mY);
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
            MouseHitTest(x, y, out result, posScaled);
            if (result.mObjectType == GameObjectType.Plant)
            {
                Plant plant = (Plant)result.mObject;
                if (plant.mSeedType == SeedType.Gravebuster && mApp.mGameMode != GameMode.ChallengeZenGarden)
                {
                    result.mObjectType = GameObjectType.None;
                    result.mObject = null;
                }
            }
            return result;
        }

        public bool CanAddGraveStoneAt(int theGridX, int theGridY)
        {
            if (mGridSquareType[theGridX, theGridY] != GridSquareType.Grass && mGridSquareType[theGridX, theGridY] != GridSquareType.HighGround)
            {
                return false;
            }
            int num = -1;
            GridItem gridItem = null;
            while (IterateGridItems(ref gridItem, ref num))
            {
                if (gridItem.mGridX == theGridX && gridItem.mGridY == theGridY && (gridItem.mGridItemType == GridItemType.Gravestone || gridItem.mGridItemType == GridItemType.Crater || gridItem.mGridItemType == GridItemType.Ladder))
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
            while (IterateGridItems(ref gridItem, ref num))
            {
                if (mEnableGraveStones && gridItem.mGridItemType == GridItemType.Gravestone && gridItem.mGridItemCounter < 100)
                {
                    gridItem.mGridItemCounter += 3;
                }
                if (gridItem.mGridItemType == GridItemType.Crater && mApp.mGameScene == GameScenes.Playing)
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
            if (!doAddGraveStones)
            {
                return null;
            }
            GridItem newGridItem = GridItem.GetNewGridItem();
            newGridItem.mGridItemType = GridItemType.Gravestone;
            newGridItem.mGridItemCounter = -RandomNumbers.NextNumber(50);
            newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, theGridY, 3);
            newGridItem.mGridX = theGridX;
            newGridItem.mGridY = theGridY;
            mGridItems.Add(newGridItem);
            return newGridItem;
        }

        public int GetSurvivalFlagsCompleted()
        {
            int numWavesPerFlag = GetNumWavesPerFlag();
            int num = mChallenge.mSurvivalStage * GetNumWavesPerSurvivalStage() / numWavesPerFlag;
            if (IsFlagWave(mCurrentWave - 1) && mBoardFadeOutCounter < 0 && mNextSurvivalStageCounter == 0)
            {
                return (mCurrentWave - 1) / numWavesPerFlag + num;
            }
            return mCurrentWave / numWavesPerFlag + num;
        }

        public bool HasProgressMeter()
        {
            return mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist || mApp.IsFinalBossLevel() || mApp.IsSlotMachineLevel() || mApp.IsSquirrelLevel() || mApp.IsIZombieLevel() || (mProgressMeterWidth != 0 && !mApp.IsContinuousChallenge() && mApp.mGameMode != GameMode.ChallengeZenGarden && mApp.mGameMode != GameMode.TreeOfWisdom && !mApp.IsScaryPotterLevel());
        }

        public void UpdateCursor()
        {
            bool flag = false;
            bool flag2 = false;
            int num = mApp.mWidgetManager.mLastMouseX - mX;
            int num2 = mApp.mWidgetManager.mLastMouseY - mY;
            if (mApp.mSeedChooserScreen != null && mApp.mSeedChooserScreen.Contains(num + mX, num2 + mY))
            {
                return;
            }
            if (mApp.GetDialogCount() > 0)
            {
                return;
            }
            if (mPaused || mBoardFadeOutCounter >= 0 || mTimeStopCounter > 0 || mApp.mGameScene == GameScenes.ZombiesWon)
            {
                flag = false;
            }
            else
            {
                HitResult hitResult;
                MouseHitTest(num, num2, out hitResult, false);
                if (hitResult.mObjectType == GameObjectType.MenuButton || hitResult.mObjectType == GameObjectType.StoreButton || hitResult.mObjectType == GameObjectType.Shovel || hitResult.mObjectType == GameObjectType.WateringCan || hitResult.mObjectType == GameObjectType.Fertilizer || hitResult.mObjectType == GameObjectType.BugSpray || hitResult.mObjectType == GameObjectType.Phonograph || hitResult.mObjectType == GameObjectType.Chocolate || hitResult.mObjectType == GameObjectType.Glove || hitResult.mObjectType == GameObjectType.MoneySign || hitResult.mObjectType == GameObjectType.NextGarden || hitResult.mObjectType == GameObjectType.Wheelbarrow || hitResult.mObjectType == GameObjectType.SlotMachineHandle || hitResult.mObjectType == GameObjectType.TreeFood)
                {
                    flag = true;
                }
                else if (hitResult.mObjectType == GameObjectType.Seedpacket)
                {
                    SeedPacket seedPacket = (SeedPacket)hitResult.mObject;
                    if (seedPacket.CanPickUp())
                    {
                        flag = true;
                    }
                }
                else if (hitResult.mObjectType == GameObjectType.ScaryPot && mCursorObject.mCursorType == CursorType.Normal)
                {
                    flag = true;
                }
                else if (hitResult.mObjectType == GameObjectType.Stinky)
                {
                    flag = true;
                }
                else if (hitResult.mObjectType == GameObjectType.TreeOfWisdom)
                {
                    flag = true;
                }
                else if (hitResult.mObjectType == GameObjectType.Coin || hitResult.mObjectType == GameObjectType.Projectile)
                {
                    flag = true;
                }
                else if (hitResult.mObjectType == GameObjectType.Plant)
                {
                    if (mApp.mGameMode == GameMode.ChallengeBeghouled && !HasLevelAwardDropped())
                    {
                        flag2 = true;
                    }
                    if (mApp.mGameMode == GameMode.ChallengeBeghouledTwist && !HasLevelAwardDropped())
                    {
                        flag2 = true;
                    }
                    Plant plant = (Plant)hitResult.mObject;
                    if (plant.mState == PlantState.CobcannonReady && mCursorObject.mCursorType == CursorType.Normal)
                    {
                        flag = true;
                    }
                }
                else if (mCursorObject.mCursorType == CursorType.Hammer)
                {
                }
                if (mChallenge.mBeghouledMouseCapture)
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
            if (mTutorialTimer > 0)
            {
                mTutorialTimer--;
            }
            if (mTutorialState == TutorialState.Level1PickUpPeashooter && mTutorialTimer == 0)
            {
                DisplayAdvice("[ADVICE_CLICK_PEASHOOTER]", MessageStyle.TutorialLevel1Stay, AdviceType.None);
                float num = mSeedBank.mX + mSeedBank.mSeedPackets[0].mX;
                float num2 = 0f;
                TutorialArrowShow((int)(num + Constants.SMALL_SEEDPACKET_WIDTH / 2 - Constants.InvertAndScale(13f)), (int)num2);
                mTutorialTimer = -1;
            }
            if (mTutorialState == TutorialState.Level2PickUpSunflower || mTutorialState == TutorialState.Level2PlantSunflower || mTutorialState == TutorialState.Level2RefreshSunflower)
            {
                if (mTutorialTimer == 0)
                {
                    DisplayAdvice("[ADVICE_PLANT_SUNFLOWER2]", MessageStyle.TutorialLevel2, AdviceType.None);
                    mTutorialTimer = -1;
                }
                else if (mZombieCountDown == 750 && mCurrentWave == 0)
                {
                    DisplayAdvice("[ADVICE_PLANT_SUNFLOWER3]", MessageStyle.TutorialLevel2, AdviceType.None);
                }
            }
            if ((mTutorialState == TutorialState.MoresunPickUpSunflower || mTutorialState == TutorialState.MoresunPlantSunflower || mTutorialState == TutorialState.MoresunRefreshSunflower) && mTutorialTimer == 0)
            {
                DisplayAdvice("[ADVICE_PLANT_SUNFLOWER5]", MessageStyle.TutorialLater, AdviceType.PlantSunflower5);
                mTutorialTimer = -1;
            }
            if (mApp.IsFirstTimeAdventureMode() && mLevel >= 3 && mLevel != 5 && mLevel <= 7 && mTutorialState == TutorialState.Off && mCurrentWave >= 5 && !Board.gShownMoreSunTutorial && mSeedBank.mSeedPackets[1].CanPickUp() && CountPlantByType(SeedType.Sunflower) < 3)
            {
                Debug.ASSERT(!ChooseSeedsOnCurrentLevel());
                DisplayAdvice("[ADVICE_PLANT_SUNFLOWER4]", MessageStyle.TutorialLaterStay, AdviceType.None);
                GameConstants.gShownMoreSunTutorial = true;
                SetTutorialState(TutorialState.MoresunPickUpSunflower);
                mTutorialTimer = 500;
            }
        }

        public SeedType GetSeedTypeInCursor()
        {
            if (mCursorObject.mCursorType == CursorType.Wheeelbarrow && mApp.mZenGarden.GetPottedPlantInWheelbarrow() != null)
            {
                return mApp.mZenGarden.GetPottedPlantInWheelbarrow().mSeedType;
            }
            if (!IsPlantInCursor())
            {
                return SeedType.None;
            }
            if (mCursorObject.mType == SeedType.Imitater)
            {
                return mCursorObject.mImitaterType;
            }
            return mCursorObject.mType;
        }

        public int CountPlantByType(SeedType theSeedType)
        {
            int num = 0;
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead && plant.mSeedType == theSeedType)
                {
                    num++;
                }
            }
            return num;
        }

        public bool PlantingRequirementsMet(SeedType theSeedType)
        {
            return (theSeedType != SeedType.Gatlingpea || CountPlantByType(SeedType.Repeater) != 0) && (theSeedType != SeedType.Wintermelon || CountPlantByType(SeedType.Melonpult) != 0) && (theSeedType != SeedType.Twinsunflower || CountPlantByType(SeedType.Sunflower) != 0) && (theSeedType != SeedType.Spikerock || CountPlantByType(SeedType.Spikeweed) != 0) && (theSeedType != SeedType.Cobcannon || HasValidCobCannonSpot()) && (theSeedType != SeedType.GoldMagnet || CountPlantByType(SeedType.Magnetshroom) != 0) && (theSeedType != SeedType.Gloomshroom || CountPlantByType(SeedType.Fumeshroom) != 0) && (theSeedType != SeedType.Cattail || CountEmptyPotsOrLilies(SeedType.Lilypad) != 0);
        }

        public bool HasValidCobCannonSpot()
        {
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead && plant.mSeedType == SeedType.Kernelpult && IsValidCobCannonSpot(plant.mPlantCol, plant.mRow))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsValidCobCannonSpot(int theGridX, int theGridY)
        {
            if (!IsValidCobCannonSpotHelper(theGridX, theGridY) || !IsValidCobCannonSpotHelper(theGridX + 1, theGridY))
            {
                return false;
            }
            bool flag = GetFlowerPotAt(theGridX, theGridY) != null;
            bool flag2 = GetFlowerPotAt(theGridX + 1, theGridY) != null;
            return flag == flag2;
        }

        public bool IsValidCobCannonSpotHelper(int theGridX, int theGridY)
        {
            PlantsOnLawn plantsOnLawn = default(PlantsOnLawn);
            GetPlantsOnLawn(theGridX, theGridY, ref plantsOnLawn);
            if (plantsOnLawn.mPumpkinPlant != null)
            {
                return false;
            }
            if (!mApp.mEasyPlantingCheat)
            {
                return plantsOnLawn.mNormalPlant != null && plantsOnLawn.mNormalPlant.mSeedType == SeedType.Kernelpult;
            }
            return (plantsOnLawn.mNormalPlant != null && plantsOnLawn.mNormalPlant.mSeedType == SeedType.Kernelpult) || CanPlantAt(theGridX, theGridY, SeedType.Kernelpult) == PlantingReason.Ok;
        }

        public void MouseDownCobcannonFire(int x, int y, int theClickCount)
        {
            float num = TodCommon.Distance2D(x, y, mCobCannonMouseX, mCobCannonMouseY);
            x = (int)(x * Constants.IS);
            y = (int)(y * Constants.IS);
            if (theClickCount < 0)
            {
                ClearCursor();
                return;
            }
            if (y < Constants.LAWN_YMIN)
            {
                ClearCursor();
                return;
            }
            if (mCobCannonCursorDelayCounter > 0 && num < 50f)
            {
                return;
            }
            if (mCursorObject.mCursorType == CursorType.PlantFromDuplicator)
            {
                ClearCursor();
                return;
            }
            Plant plant = null;
            if (mCursorObject.mCobCannonPlantID != null && mPlants.IndexOf(mCursorObject.mCobCannonPlantID) >= 0)
            {
                plant = mCursorObject.mCobCannonPlantID;
            }
            if (plant == null)
            {
                ClearCursor();
                return;
            }
            plant.CobCannonFire(x, y);
            ClearCursor();
        }

        public int KillAllZombiesInRadius(int theRow, int theX, int theY, int theRadius, int theRowRange, bool theBurn, int theDamageRangeFlags)
        {
            int num = 0;
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && zombie.EffectedByDamage((uint)theDamageRangeFlags))
                {
                    TRect zombieRect = zombie.GetZombieRect();
                    int num2 = zombie.mRow - theRow;
                    if (zombie.mZombieType == ZombieType.Boss)
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
            int num3 = PixelToGridXKeepOnBoard(theX, theY);
            int num4 = PixelToGridYKeepOnBoard(theX, theY);
            int num5 = -1;
            GridItem gridItem = null;
            while (IterateGridItems(ref gridItem, ref num5))
            {
                if (gridItem.mGridItemType == GridItemType.Ladder)
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
            if (mApp.IsFirstTimeAdventureMode() && mLevel == 1)
            {
                return false;
            }
            int numWavesPerFlag = GetNumWavesPerFlag();
            return theWaveNumber % numWavesPerFlag == numWavesPerFlag - 1;
        }

        public void DrawHouseDoorTop(Graphics g)
        {
            if (mBackground == BackgroundType.Num1Day)
            {
                g.DrawImage(Resources.IMAGE_BACKGROUND1_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_1.X, Constants.Board_GameOver_Exterior_Overlay_1.Y);
                return;
            }
            if (mBackground == BackgroundType.Num2Night)
            {
                g.DrawImage(Resources.IMAGE_BACKGROUND2_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_2.X, Constants.Board_GameOver_Exterior_Overlay_2.Y);
                return;
            }
            if (mBackground == BackgroundType.Num3Pool)
            {
                if (Zombie.WinningZombieReachedDesiredY)
                {
                    g.DrawImage(Resources.IMAGE_BACKGROUND3_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_3.X, Constants.Board_GameOver_Exterior_Overlay_3.Y);
                    return;
                }
            }
            else
            {
                if (mBackground == BackgroundType.Num4Fog)
                {
                    g.DrawImage(Resources.IMAGE_BACKGROUND4_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_4.X, Constants.Board_GameOver_Exterior_Overlay_4.Y);
                    return;
                }
                if (mBackground == BackgroundType.Num5Roof)
                {
                    if (Zombie.WinningZombieReachedDesiredY)
                    {
                        g.DrawImage(Resources.IMAGE_BACKGROUND5_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_5.X, Constants.Board_GameOver_Exterior_Overlay_5.Y);
                        return;
                    }
                }
                else if (mBackground == BackgroundType.Num6Boss && Zombie.WinningZombieReachedDesiredY)
                {
                    g.DrawImage(Resources.IMAGE_BACKGROUND6_GAMEOVER_MASK, Constants.Board_GameOver_Exterior_Overlay_6.X, Constants.Board_GameOver_Exterior_Overlay_6.Y);
                }
            }
        }

        public void DrawHouseDoorBottom(Graphics g)
        {
            if (mBackground == BackgroundType.Num1Day)
            {
                g.DrawImage(Resources.IMAGE_BACKGROUND1_GAMEOVER_INTERIOR_OVERLAY, Constants.Board_GameOver_Interior_Overlay_1.X, Constants.Board_GameOver_Interior_Overlay_1.Y);
                return;
            }
            if (mBackground == BackgroundType.Num2Night)
            {
                g.DrawImage(Resources.IMAGE_BACKGROUND2_GAMEOVER_INTERIOR_OVERLAY, Constants.Board_GameOver_Interior_Overlay_2.X, Constants.Board_GameOver_Interior_Overlay_2.Y);
                return;
            }
            if (mBackground == BackgroundType.Num3Pool)
            {
                g.DrawImage(Resources.IMAGE_BACKGROUND3_GAMEOVER_INTERIOR_OVERLAY, Constants.Board_GameOver_Interior_Overlay_3.X, Constants.Board_GameOver_Interior_Overlay_3.Y);
                return;
            }
            if (mBackground == BackgroundType.Num4Fog)
            {
                g.DrawImage(Resources.IMAGE_BACKGROUND4_GAMEOVER_INTERIOR_OVERLAY, Constants.Board_GameOver_Interior_Overlay_4.X, Constants.Board_GameOver_Interior_Overlay_4.Y);
            }
        }

        public Zombie GetBossZombie()
        {
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && zombie.mZombieType == ZombieType.Boss)
                {
                    return zombie;
                }
            }
            return null;
        }

        public bool HasConveyorBeltSeedBank()
        {
            return mApp.IsFinalBossLevel() || mApp.IsMiniBossLevel() || mApp.IsShovelLevel() || mApp.IsWallnutBowlingLevel() || mApp.IsLittleTroubleLevel() || mApp.IsStormyNightLevel() || mApp.IsBungeeBlitzLevel() || mApp.mGameMode == GameMode.ChallengePortalCombat || mApp.mGameMode == GameMode.ChallengeColumn || mApp.mGameMode == GameMode.ChallengeInvisighoul;
        }

        public bool StageHasRoof()
        {
            return mBackground == BackgroundType.Num5Roof || mBackground == BackgroundType.Num6Boss;
        }

        public void SpawnZombiesFromPool()
        {
            if (mIceTrapCounter > 0)
            {
                return;
            }
            int num;
            int num2;
            if (mLevel == 21 || mLevel == 22 || mLevel == 31 || mLevel == 32)
            {
                num = 2;
                num2 = 3;
            }
            else if (mLevel == 23 || mLevel == 24 || mLevel == 25 || mLevel == 33 || mLevel == 34 || mLevel == 35)
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
                ZombieType theZombieType = PickGraveRisingZombieType(num2);
                ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
                Zombie zombie = AddZombieInRow(theZombieType, todWeightedGridArray.mY, mCurrentWave);
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
            if (mIceTrapCounter > 0)
            {
                return;
            }
            int num;
            int num2;
            if (mLevel == 41 || mLevel == 42)
            {
                num = 2;
                num2 = 3;
            }
            else if (mLevel == 43 || mLevel == 44 || mLevel == 45)
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
            SetupBungeeDrop(bungeeDropGrid);
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
                ZombieType theZombieType = PickGraveRisingZombieType(num2);
                BungeeDropZombie(bungeeDropGrid, theZombieType);
                ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
                num2 -= zombieDefinition.mZombieValue;
                num2 = Math.Max(1, num2);
            }
        }

        public void PickUpTool(GameObjectType theObjectType)
        {
            if (mPaused)
            {
                return;
            }
            if (mApp.mGameScene != GameScenes.Playing && !mCutScene.IsInShovelTutorial())
            {
                return;
            }
            if (theObjectType == GameObjectType.Shovel)
            {
                if (mCursorObject.mCursorType == CursorType.Shovel)
                {
                    ClearCursor();
                    return;
                }
                if (mTutorialState == TutorialState.ShovelPickup)
                {
                    SetTutorialState(TutorialState.ShovelDig);
                }
                mCursorObject.mCursorType = CursorType.Shovel;
                mApp.PlayFoley(FoleyType.Shovel);
            }
            else if (theObjectType == GameObjectType.WateringCan)
            {
                if (mTutorialState == TutorialState.ZenGardenPickupWater)
                {
                    mTutorialState = TutorialState.ZenGardenWaterPlant;
                    mApp.mPlayerInfo.mZenTutorialMessage = 23;
                    DisplayAdvice("[ADVICE_ZEN_GARDEN_WATER_PLANT]", MessageStyle.ZenGardenLong, AdviceType.None);
                    TutorialArrowRemove();
                }
                mCursorObject.mCursorType = CursorType.WateringCan;
                if (mApp.mPlayerInfo.mPurchases[13] > 0)
                {
                    mIgnoreNextMouseUp = true;
                }
                mApp.PlayFoley(FoleyType.Drop);
            }
            else if (theObjectType == GameObjectType.Fertilizer)
            {
                if (mApp.mPlayerInfo.mPurchases[14] > 1000)
                {
                    mCursorObject.mCursorType = CursorType.Fertilizer;
                    mApp.PlayFoley(FoleyType.Drop);
                }
                else
                {
                    mApp.PlaySample(Resources.SOUND_BUZZER);
                }
            }
            else if (theObjectType == GameObjectType.BugSpray)
            {
                if (mApp.mPlayerInfo.mPurchases[15] > 1000)
                {
                    mCursorObject.mCursorType = CursorType.BugSpray;
                    mApp.PlayFoley(FoleyType.Drop);
                }
                else
                {
                    mApp.PlaySample(Resources.SOUND_BUZZER);
                }
            }
            else if (theObjectType == GameObjectType.Phonograph)
            {
                mCursorObject.mCursorType = CursorType.Phonograph;
                mApp.PlayFoley(FoleyType.Drop);
            }
            else if (theObjectType == GameObjectType.Chocolate)
            {
                if (mApp.mPlayerInfo.mPurchases[26] > 1000)
                {
                    mCursorObject.mCursorType = CursorType.Chocolate;
                    mApp.PlayFoley(FoleyType.Drop);
                }
                else
                {
                    mApp.PlaySample(Resources.SOUND_BUZZER);
                }
            }
            else if (theObjectType == GameObjectType.Glove)
            {
                mCursorObject.mCursorType = CursorType.Glove;
                mApp.PlayFoley(FoleyType.Drop);
            }
            else if (theObjectType == GameObjectType.MoneySign)
            {
                mCursorObject.mCursorType = CursorType.MoneySign;
                mApp.PlayFoley(FoleyType.Drop);
            }
            else if (theObjectType == GameObjectType.Wheelbarrow)
            {
                mCursorObject.mCursorType = CursorType.Wheeelbarrow;
                mApp.PlayFoley(FoleyType.Drop);
            }
            else if (theObjectType == GameObjectType.TreeFood)
            {
                if (mChallenge.TreeOfWisdomCanFeed())
                {
                    if (mApp.mPlayerInfo.mPurchases[28] > 1000)
                    {
                        mCursorObject.mCursorType = CursorType.TreeFood;
                        mApp.PlayFoley(FoleyType.Drop);
                    }
                    else
                    {
                        mApp.PlaySample(Resources.SOUND_BUZZER);
                    }
                }
            }
            else
            {
                Debug.ASSERT(false);
            }
            mCursorObject.mType = SeedType.None;
        }

        public void TutorialArrowShow(int x, int y)
        {
            TutorialArrowRemove();
            TodParticleSystem theParticle = mApp.AddTodParticle((x - Constants.Board_Offset_AspectRatio_Correction) * Constants.IS, y * Constants.IS, 800000, ParticleEffect.SeedPacketPick);
            mTutorialParticleID = mApp.ParticleGetID(theParticle);
        }

        public void TutorialArrowRemove()
        {
            TodParticleSystem todParticleSystem = mApp.ParticleTryToGet(mTutorialParticleID);
            if (todParticleSystem != null)
            {
                todParticleSystem.ParticleSystemDie();
            }
            mTutorialParticleID = null;
        }

        public int CountCoinsBeingCollected()
        {
            int num = 0;
            Coin coin = null;
            while (IterateCoins(ref coin))
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
            Zombie zombie = AddZombie(ZombieType.Bungee, mCurrentWave);
            Zombie zombie2 = AddZombie(theZombieType, mCurrentWave);
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
                    int aGridArrayCount = theBungeeDropGrid.mGridArrayCount;
                    theBungeeDropGrid.mGridArray[aGridArrayCount].mX = i;
                    theBungeeDropGrid.mGridArray[aGridArrayCount].mY = j;
                    theBungeeDropGrid.mGridArray[aGridArrayCount].mWeight = 10000;
                    theBungeeDropGrid.mGridArrayCount++;
                    Debug.ASSERT(theBungeeDropGrid.mGridArrayCount <= theBungeeDropGrid.mGridArray.Length);
                }
            }
        }

        public void PutZombieInWave(ZombieType theZombieType, int theWaveNumber, ZombiePicker theZombiePicker)
        {
            Debug.ASSERT(theWaveNumber < 100 && theZombiePicker.mZombieCount < 50);
            mZombiesInWave[theWaveNumber, theZombiePicker.mZombieCount] = theZombieType;
            theZombiePicker.mZombieCount++;
            if (theZombiePicker.mZombieCount < 50)
            {
                mZombiesInWave[theWaveNumber, theZombiePicker.mZombieCount] = ZombieType.Invalid;
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
                if (theZombiePicker.mZombieTypeCount[(int)zombieType] <= 0 && zombieType != ZombieType.Yeti && Board.CanZombieSpawnOnLevel(zombieType, mLevel))
                {
                    PutZombieInWave(zombieType, theWaveNumber, theZombiePicker);
                }
            }
        }

        public TRect GetShovelButtonRect()
        {
            TRect result = new TRect(Constants.UIShovelButtonPosition.X - Constants.Board_Offset_AspectRatio_Correction, Constants.UIShovelButtonPosition.Y, AtlasResources.IMAGE_SHOVELBANK.mWidth, AtlasResources.IMAGE_SHOVELBANK.mHeight);
            if (mApp.IsSquirrelLevel())
            {
                result.mX = 600;
            }
            return result;
        }

        public TRect GetZenShovelButtonRect()
        {
            TRect result = new TRect(Constants.ZenGardenTopButtonStart, Constants.UIShovelButtonPosition.Y, AtlasResources.IMAGE_SHOVELBANK_ZEN.mWidth, AtlasResources.IMAGE_SHOVELBANK_ZEN.mHeight);
            if (mApp.IsSlotMachineLevel() || mApp.IsSquirrelLevel())
            {
                result.mX = 600;
            }
            return result;
        }

        public TRect GetZenButtonRect(GameObjectType theObjectType)
        {
            TRect zenShovelButtonRect = GetZenShovelButtonRect();
            if (theObjectType == GameObjectType.NextGarden)
            {
                zenShovelButtonRect.mX = Constants.ZenGarden_NextGarden_Pos.X;
                return zenShovelButtonRect;
            }
            bool flag = true;
            for (int i = 6; i <= 15; i++)
            {
                GameObjectType gameObjectType = (GameObjectType)i;
                if (gameObjectType != GameObjectType.TreeFood && !CanUseGameObject(gameObjectType))
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
                if (CanUseGameObject(theGameObject))
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
            mPlants.Add(newPlant);
            return newPlant;
        }

        public void DoPlantingEffects(int theGridX, int theGridY, Plant thePlant, bool forAquarium)
        {
            int num = GridToPixelX(theGridX, theGridY) + 41;
            int num2 = GridToPixelY(theGridX, theGridY) + 74;
            if (thePlant != null)
            {
                if (thePlant.mSeedType == SeedType.Lilypad)
                {
                    num2 += 15;
                }
                else if (thePlant.mSeedType == SeedType.Flowerpot)
                {
                    num2 += 30;
                }
            }
            if (Plant.IsFlying(thePlant.mSeedType))
            {
                mApp.PlayFoley(FoleyType.Plant);
                return;
            }
            if (forAquarium || IsPoolSquare(theGridX, theGridY))
            {
                mApp.PlayFoley(FoleyType.PlantWater);
                if (forAquarium)
                {
                    num2 -= 30;
                }
                mApp.AddTodParticle(num, num2, 400000, ParticleEffect.PlantingPool);
                return;
            }
            mApp.PlayFoley(FoleyType.Plant);
            mApp.AddTodParticle(num, num2, 400000, ParticleEffect.Planting);
        }

        public bool IsFinalSurvivalStage()
        {
            return false;
        }

        public void SurvivalSaveScore()
        {
            if (!mApp.IsSurvivalMode())
            {
                return;
            }
            int survivalFlagsCompleted = GetSurvivalFlagsCompleted();
            int currentChallengeIndex = mApp.GetCurrentChallengeIndex();
            if (survivalFlagsCompleted > mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex])
            {
                mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex] = survivalFlagsCompleted;
                mApp.WriteCurrentUserConfig();
            }
        }

        public int CountZombiesOnScreen()
        {
            int num = 0;
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && zombie.mHasHead && !zombie.IsDeadOrDying() && !zombie.mMindControlled && zombie.IsOnBoard())
                {
                    num++;
                }
            }
            return num;
        }

        public int GetNumWavesPerSurvivalStage()
        {
            if (mApp.mGameMode == GameMode.ChallengeLastStand)
            {
                return 10;
            }
            if (mApp.IsSurvivalNormal(mApp.mGameMode))
            {
                return 10;
            }
            if (mApp.IsSurvivalHard(mApp.mGameMode))
            {
                return 20;
            }
            if (mApp.IsSurvivalEndless(mApp.mGameMode))
            {
                return 20;
            }
            Debug.ASSERT(false);
            return -666;
        }

        public int GetLevelRandSeed()
        {
            int num = 101;
            int num2 = mBoardRandSeed + (int)mApp.mPlayerInfo.mId;
            if (mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
            {
                num2 += mLevel + mApp.mPlayerInfo.mFinishedAdventure * num;
            }
            else if (mApp.IsScaryPotterLevel() || mApp.IsIZombieLevel())
            {
                RandomNumbers.Seed();
                num2 = RandomNumbers.NextNumber();
            }
            else
            {
                num2 = (int)(num2 + (mApp.mGameMode + mChallenge.mSurvivalStage * num));
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
            else if (theBossZombie.mZombiePhase == ZombiePhase.BossStomping)
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(theBossZombie.mBodyReanimID);
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
            renderItem.mRenderObjectType = RenderObjectType.BossPart;
            renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.Boss, theRow, 2);
            renderItem.mBossPart = BossPart.BackLeg;
            theCurRenderItem++;
            renderItem = theRenderList[theCurRenderItem];
            renderItem.mRenderObjectType = RenderObjectType.BossPart;
            renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.Boss, theRow2, 2);
            renderItem.mBossPart = BossPart.FrontLeg;
            theCurRenderItem++;
            renderItem = theRenderList[theCurRenderItem];
            renderItem.mRenderObjectType = RenderObjectType.BossPart;
            renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.Boss, 4, 2);
            renderItem.mBossPart = BossPart.Main;
            theCurRenderItem++;
            renderItem = theRenderList[theCurRenderItem];
            renderItem.mRenderObjectType = RenderObjectType.BossPart;
            renderItem.mZPos = Board.MakeRenderOrder(RenderLayer.Boss, theRow3, 3);
            renderItem.mBossPart = BossPart.BackArm;
            theCurRenderItem++;
            Reanimation reanimation2 = mApp.ReanimationTryToGet(theBossZombie.mBossFireBallReanimID);
            if (reanimation2 != null)
            {
                renderItem = theRenderList[theCurRenderItem];
                renderItem.mRenderObjectType = RenderObjectType.BossPart;
                renderItem.mZPos = reanimation2.mRenderOrder;
                renderItem.mBossPart = BossPart.Fireball;
                theCurRenderItem++;
            }
        }

        public GridItem GetCraterAt(int theGridX, int theGridY)
        {
            return GetGridItemAt(GridItemType.Crater, theGridX, theGridY);
        }

        public GridItem GetGraveStoneAt(int theGridX, int theGridY)
        {
            return GetGridItemAt(GridItemType.Gravestone, theGridX, theGridY);
        }

        public GridItem GetLadderAt(int theGridX, int theGridY)
        {
            return GetGridItemAt(GridItemType.Ladder, theGridX, theGridY);
        }

        public GridItem AddALadder(int theGridX, int theGridY)
        {
            GridItem newGridItem = GridItem.GetNewGridItem();
            newGridItem.mGridItemType = GridItemType.Ladder;
            newGridItem.mRenderOrder = 302000 + 10000 * theGridY + 800;
            newGridItem.mGridX = theGridX;
            newGridItem.mGridY = theGridY;
            mGridItems.Add(newGridItem);
            return newGridItem;
        }

        public GridItem AddACrater(int theGridX, int theGridY)
        {
            GridItem newGridItem = GridItem.GetNewGridItem();
            newGridItem.mGridItemType = GridItemType.Crater;
            newGridItem.mRenderOrder = 200000 + 10000 * theGridY + 1;
            newGridItem.mGridX = theGridX;
            newGridItem.mGridY = theGridY;
            mGridItems.Add(newGridItem);
            return newGridItem;
        }

        public void InitLawnMowers()
        {
            for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
            {
                bool flag = (mApp.mGameMode == GameMode.ChallengeResodded && i <= 4) || ((mApp.IsAdventureMode() || mApp.IsQuickPlayMode()) && mLevel == 35) || (mApp.mGameMode != GameMode.ChallengeBeghouled && mApp.mGameMode != GameMode.ChallengeBeghouledTwist && mApp.mGameMode != GameMode.ChallengeZenGarden && mApp.mGameMode != GameMode.TreeOfWisdom && mApp.mGameMode != GameMode.ChallengeLastStand && !mApp.IsScaryPotterLevel() && !mApp.IsSquirrelLevel() && !mApp.IsIZombieLevel() && mApp.mGameMode != GameMode.ChallengeZombiquarium && (!StageHasRoof() || mApp.mPlayerInfo.mPurchases[23] != 0) && mPlantRow[i] != PlantRowType.Dirt);
                if (flag)
                {
                    LawnMower newLawnMower = LawnMower.GetNewLawnMower();
                    newLawnMower.LawnMowerInitialize(i);
                    newLawnMower.mVisible = false;
                    mLawnMowers.Add(newLawnMower);
                }
            }
        }

        public bool IsPlantInCursor()
        {
            return mCursorObject.mCursorType == CursorType.PlantFromBank || mCursorObject.mCursorType == CursorType.PlantFromUsableCoin || mCursorObject.mCursorType == CursorType.PlantFromGlove || mCursorObject.mCursorType == CursorType.PlantFromDuplicator || mCursorObject.mCursorType == CursorType.PlantFromWheelBarrow;
        }

        public void ClearFogAroundPlant(Plant thePlant, int theSize)
        {
            int num = 6;
            if (mFogBlownCountDown > 0 && mFogBlownCountDown < 2000)
            {
                num = 2;
            }
            else if (mFogBlownCountDown > 0)
            {
                num = 40;
            }
            int num2 = LeftFogColumn();
            for (int i = thePlant.mPlantCol - theSize; i <= thePlant.mPlantCol + theSize; i++)
            {
                int num3 = i + -(((int)mFogOffset + 50) / 100);
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
                        mGridCelFog[num3, j] = Math.Max(mGridCelFog[num3, j] - num, 0);
                    }
                    IL_D3:;
                }
            }
        }

        public void RemoveParticleByType(ParticleEffect theEffectType)
        {
            int num = -1;
            TodParticleSystem todParticleSystem = null;
            while (IterateParticles(ref todParticleSystem, ref num))
            {
                if (todParticleSystem.mEffectType == theEffectType)
                {
                    todParticleSystem.ParticleSystemDie();
                }
            }
        }

        public GridItem GetScaryPotAt(int theGridX, int theGridY)
        {
            return GetGridItemAt(GridItemType.ScaryPot, theGridX, theGridY);
        }

        public void PuzzleSaveStreak()
        {
            if (!mApp.IsEndlessScaryPotter(mApp.mGameMode) && !mApp.IsEndlessIZombie(mApp.mGameMode))
            {
                return;
            }
            int num = mChallenge.mSurvivalStage + 1;
            int currentChallengeIndex = mApp.GetCurrentChallengeIndex();
            if (num > mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex])
            {
                mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex] = num;
                mApp.WriteCurrentUserConfig();
            }
        }

        public void ClearAdviceImmediately()
        {
            ClearAdvice(AdviceType.None);
            mAdvice.mDuration = 0;
        }

        public bool IsFinalScaryPotterStage()
        {
            if (!mApp.IsScaryPotterLevel())
            {
                return false;
            }
            if (mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
            {
                return mChallenge.mSurvivalStage == 2;
            }
            return !mApp.IsEndlessScaryPotter(mApp.mGameMode);
        }

        public void DisplayAdviceAgain(string theAdvice, MessageStyle theMessageStyle, AdviceType theHelpIndex)
        {
            if (theHelpIndex != AdviceType.None && mHelpDisplayed[(int)theHelpIndex])
            {
                mHelpDisplayed[(int)theHelpIndex] = false;
            }
            DisplayAdvice(theAdvice, theMessageStyle, theHelpIndex);
        }

        public GridItem GetSquirrelAt(int theGridX, int theGridY)
        {
            return GetGridItemAt(GridItemType.Squirrel, theGridX, theGridY);
        }

        public GridItem GetZenToolAt(int theGridX, int theGridY)
        {
            return GetGridItemAt(GridItemType.ZenTool, theGridX, theGridY);
        }

        public bool IsPlantInGoldWateringCanRange(int theMouseX, int theMouseY, Plant thePlant)
        {
            int num = theMouseX + Constants.ZenGarden_GoldenWater_Size.X;
            int num2 = theMouseX + Constants.ZenGarden_GoldenWater_Size.Width;
            int num3 = theMouseY + Constants.ZenGarden_GoldenWater_Size.Y;
            int num4 = theMouseY + Constants.ZenGarden_GoldenWater_Size.Height;
            return GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, TopPlant.ZenToolOrder) == thePlant && (thePlant.mX + 40 >= num && thePlant.mX + 40 < num2 && thePlant.mY + 40 >= num3 && thePlant.mY + 40 < num4);
        }

        public bool StageHasZombieWalkInFromRight()
        {
            return !mApp.IsWhackAZombieLevel() && mApp.mGameMode != GameMode.ChallengeIce && !mApp.IsFinalBossLevel() && !mApp.IsIZombieLevel() && !mApp.IsSquirrelLevel() && !mApp.IsScaryPotterLevel() && mApp.mGameMode != GameMode.ChallengeZenGarden && mApp.mGameMode != GameMode.TreeOfWisdom && mApp.mGameMode != GameMode.ChallengeZombiquarium;
        }

        public void PlaceRake()
        {
            if (mApp.mPlayerInfo.mPurchases[24] == 0)
            {
                return;
            }
            int num;
            if (mApp.IsScaryPotterLevel())
            {
                num = 7;
                int num2 = -1;
                GridItem gridItem = null;
                while (IterateGridItems(ref gridItem, ref num2))
                {
                    if (gridItem.mGridItemType == GridItemType.ScaryPot && gridItem.mGridX <= num && gridItem.mGridX > 0)
                    {
                        num = gridItem.mGridX - 1;
                    }
                }
            }
            else
            {
                if (!StageHasZombieWalkInFromRight() || mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist || mApp.mGameMode == GameMode.ChallengeBobsledBonanza)
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
                if (j != 5 && mPlantRow[j] == PlantRowType.Normal)
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
            int aGridY = (int)TodCommon.TodPickFromWeightedArray(Board.aPickArray, num3);
            mApp.mPlayerInfo.mPurchases[24]--;
            GridItem newGridItem = GridItem.GetNewGridItem();
            newGridItem.mGridItemType = GridItemType.Rake;
            newGridItem.mGridX = num;
            newGridItem.mGridY = aGridY;
            newGridItem.mPosX = GridToPixelX(newGridItem.mGridX, newGridItem.mGridY);
            newGridItem.mPosY = GridToPixelY(newGridItem.mGridX, newGridItem.mGridY);
            newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, newGridItem.mGridY, 9);
            mGridItems.Add(newGridItem);
            Reanimation theReanimation = CreateRakeReanim(newGridItem.mPosX, newGridItem.mPosY, 0);
            newGridItem.mGridItemReanimID = mApp.ReanimationGetID(theReanimation);
            newGridItem.mGridItemState = GridItemState.RakeAttracting;
        }

        public Reanimation CreateRakeReanim(float rakeX, float rakeY, int renderOrder)
        {
            Reanimation reanimation = mApp.AddReanimation(rakeX + 20f, rakeY, renderOrder, ReanimationType.Rake);
            reanimation.mAnimRate = 0f;
            reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
            reanimation.mIsAttachment = true;
            return reanimation;
        }

        public GridItem GetRake()
        {
            int num = -1;
            GridItem gridItem = null;
            while (IterateGridItems(ref gridItem, ref num))
            {
                if (gridItem.mGridItemType == GridItemType.Rake)
                {
                    return gridItem;
                }
            }
            return null;
        }

        public bool IsScaryPotterDaveTalking()
        {
            return mApp.IsScaryPotterLevel() && mNextSurvivalStageCounter > 0 && mApp.mCrazyDaveState != CrazyDaveState.Off;
        }

        public Zombie GetWinningZombie()
        {
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
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
            mStartDrawTime = tickCount;
            mIntervalDrawTime = tickCount;
            mDrawCount = 1;
            mIntervalDrawCountStart = 1;
        }

        public int CountEmptyPotsOrLilies(SeedType theSeedType)
        {
            int num = 0;
            int count = mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mPlants[i];
                if (!plant.mDead && plant.mSeedType == theSeedType && GetTopPlantAt(plant.mPlantCol, plant.mRow, TopPlant.OnlyNormalPosition) == null)
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
            while (IterateGridItems(ref gridItem, ref num))
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
            return (!mApp.IsFirstTimeAdventureMode() || mLevel != 1) && !mApp.IsWhackAZombieLevel() && !mApp.IsFinalBossLevel() && mApp.mGameMode != GameMode.ChallengeBeghouled && mApp.mGameMode != GameMode.ChallengeBeghouledTwist && !mApp.IsSlotMachineLevel() && !mApp.IsSquirrelLevel() && !mApp.IsIZombieLevel();
        }

        public bool IsLastStandFinalStage()
        {
            return mApp.mGameMode == GameMode.ChallengeLastStand && mChallenge.mSurvivalStage == 4;
        }

        public int GetNumWavesPerFlag()
        {
            if (mApp.IsFirstTimeAdventureMode() && mNumWaves < 10)
            {
                return mNumWaves;
            }
            return 10;
        }

        public int GetCurrentPlantCost(SeedType theSeedType, SeedType theImitaterType)
        {
            int num = Plant.GetCost(theSeedType, theImitaterType);
            if (PlantUsesAcceleratedPricing(theSeedType))
            {
                int num2 = CountPlantByType(theSeedType);
                num += num2 * 50;
            }
            return num;
        }

        public bool PlantUsesAcceleratedPricing(SeedType theSeedType)
        {
            return Plant.IsUpgrade(theSeedType) && mApp.IsSurvivalEndless(mApp.mGameMode);
        }

        public void FreezeEffectsForCutscene(bool theFreeze)
        {
            int num = -1;
            TodParticleSystem todParticleSystem = null;
            while (IterateParticles(ref todParticleSystem, ref num))
            {
                if (todParticleSystem.mEffectType == ParticleEffect.GraveBuster)
                {
                    todParticleSystem.mDontUpdate = theFreeze;
                }
                if (todParticleSystem.mEffectType == ParticleEffect.PoolSparkly && mIceTrapCounter == 0)
                {
                    todParticleSystem.mDontUpdate = theFreeze;
                }
            }
            int num2 = -1;
            Reanimation reanimation = null;
            while (IterateReanimations(ref reanimation, ref num2))
            {
                if (reanimation.mReanimationType == ReanimationType.Sleeping)
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
            if (mBackground == BackgroundType.Num1Day)
            {
                mApp.DelayLoadBackgroundResource("DelayLoad_Background1");
                if (mLevel <= 4 && mApp.IsFirstTimeAdventureMode())
                {
                    TodCommon.TodLoadResources("DelayLoad_BackgroundUnsodded");
                    return;
                }
                if (mApp.mGameMode == GameMode.ChallengeResodded)
                {
                    TodCommon.TodLoadResources("DelayLoad_BackgroundUnsodded");
                    return;
                }
            }
            else
            {
                if (mBackground == BackgroundType.Num2Night)
                {
                    mApp.DelayLoadBackgroundResource("DelayLoad_Background2");
                    return;
                }
                if (mBackground == BackgroundType.Num3Pool)
                {
                    mApp.DelayLoadBackgroundResource("DelayLoad_Background3");
                    return;
                }
                if (mBackground == BackgroundType.Num4Fog)
                {
                    mApp.DelayLoadBackgroundResource("DelayLoad_Background4");
                    return;
                }
                if (mBackground == BackgroundType.Num5Roof)
                {
                    mApp.DelayLoadBackgroundResource("DelayLoad_Background5");
                    return;
                }
                if (mBackground == BackgroundType.Num6Boss)
                {
                    mApp.DelayLoadBackgroundResource("DelayLoad_Background6");
                    return;
                }
                if (mBackground == BackgroundType.Greenhouse)
                {
                    mApp.DelayLoadZenGardenBackground("DelayLoad_GreenHouseGarden");
                    return;
                }
                if (mBackground == BackgroundType.TreeOfWisdom)
                {
                    return;
                }
                if (mBackground == BackgroundType.Zombiquarium)
                {
                    mApp.DelayLoadZenGardenBackground("DelayLoad_Zombiquarium");
                    mApp.DelayLoadZenGardenBackground("DelayLoad_GreenHouseOverlay");
                    return;
                }
                if (mBackground == BackgroundType.MushroomGarden)
                {
                    mApp.DelayLoadZenGardenBackground("DelayLoad_MushroomGarden");
                    return;
                }
                Debug.ASSERT(false);
            }
        }

        public bool CanUseGameObject(GameObjectType theGameObject)
        {
            if (mApp.mGameMode == GameMode.TreeOfWisdom)
            {
                return theGameObject == GameObjectType.TreeFood || theGameObject == GameObjectType.NextGarden;
            }
            if (mApp.mGameMode != GameMode.ChallengeZenGarden)
            {
                return false;
            }
            if (theGameObject == GameObjectType.WateringCan)
            {
                return true;
            }
            if (theGameObject == GameObjectType.NextGarden)
            {
                return mApp.mPlayerInfo.mPurchases[18] != 0 || mApp.mPlayerInfo.mPurchases[25] != 0 || mApp.mPlayerInfo.mPurchases[27] != 0;
            }
            if (theGameObject == GameObjectType.Fertilizer)
            {
                return mApp.mPlayerInfo.mPurchases[14] > 0;
            }
            if (theGameObject == GameObjectType.BugSpray)
            {
                return mApp.mPlayerInfo.mPurchases[15] > 0;
            }
            if (theGameObject == GameObjectType.Phonograph)
            {
                return mApp.mPlayerInfo.mPurchases[16] > 0;
            }
            if (theGameObject == GameObjectType.Chocolate)
            {
                return mApp.mPlayerInfo.mPurchases[26] > 0;
            }
            if (theGameObject == GameObjectType.Wheelbarrow)
            {
                return mApp.mPlayerInfo.mPurchases[19] > 0;
            }
            if (theGameObject == GameObjectType.Glove)
            {
                return mApp.mPlayerInfo.mPurchases[17] > 0;
            }
            if (theGameObject == GameObjectType.MoneySign)
            {
                return mApp.HasFinishedAdventure();
            }
            if (theGameObject == GameObjectType.TreeFood)
            {
                return false;
            }
            Debug.ASSERT(false);
            return false;
        }

        public void SetMustacheMode(bool theEnableMustache)
        {
            mApp.PlayFoley(FoleyType.Polevault);
            mMustacheMode = theEnableMustache;
            mApp.mMustacheMode = theEnableMustache;
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
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
            while (IterateCoins(ref coin))
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
            mApp.PlayFoley(FoleyType.Zamboni);
            mSuperMowerMode = theEnableSuperMower;
            mApp.mSuperMowerMode = theEnableSuperMower;
            LawnMower lawnMower = null;
            while (IterateLawnMowers(ref lawnMower))
            {
                lawnMower.EnableSuperMower(theEnableSuperMower);
            }
        }

        public void DrawZenWheelBarrowButton(Graphics g, int theOffsetY)
        {
            TRect zenButtonRect = GetZenButtonRect(GameObjectType.Wheelbarrow);
            PottedPlant pottedPlantInWheelbarrow = mApp.mZenGarden.GetPottedPlantInWheelbarrow();
            if (pottedPlantInWheelbarrow != null && mCursorObject.mCursorType != CursorType.PlantFromWheelBarrow)
            {
                if (mChallenge.mChallengeState == ChallengeState.ZenFading)
                {
                    g.DrawImage(AtlasResources.IMAGE_ZEN_WHEELBARROW, zenButtonRect.mX + Constants.ZenGardenButton_Wheelbarrow_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Wheelbarrow_Offset.Y + theOffsetY);
                }
                else
                {
                    g.DrawImage(AtlasResources.IMAGE_ZEN_WHEELBARROW, zenButtonRect.mX + Constants.ZenGardenButton_Wheelbarrow_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Wheelbarrow_Offset.Y + theOffsetY);
                }
                if (pottedPlantInWheelbarrow.mPlantAge != PottedPlantAge.Small)
                {
                    PottedPlantAge aPlantAge = pottedPlantInWheelbarrow.mPlantAge;
                }
                mApp.mZenGarden.DrawPottedPlant(g, zenButtonRect.mX + Constants.ZenGardenButton_WheelbarrowPlant_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_WheelbarrowPlant_Offset.Y + theOffsetY, pottedPlantInWheelbarrow, 0.6f, true);
                return;
            }
            g.DrawImage(AtlasResources.IMAGE_ZEN_WHEELBARROW, zenButtonRect.mX + Constants.ZenGardenButton_Wheelbarrow_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Wheelbarrow_Offset.Y + theOffsetY);
        }

        public void DrawZenButtons(Graphics g)
        {
            int num = 0;
            if (mChallenge.mChallengeState == ChallengeState.ZenFading)
            {
                num = TodCommon.TodAnimateCurve(50, 0, mChallenge.mChallengeStateCounter, 0, -72, TodCurves.EaseInOut);
            }
            for (int i = 6; i <= 15; i++)
            {
                GameObjectType gameObjectType = (GameObjectType)i;
                if (CanUseGameObject(gameObjectType))
                {
                    TRect zenButtonRect = GetZenButtonRect(gameObjectType);
                    if (gameObjectType != GameObjectType.NextGarden && (mApp.mPlayerInfo.mZenGardenTutorialComplete || gameObjectType != GameObjectType.MoneySign))
                    {
                        g.DrawImage(AtlasResources.IMAGE_SHOVELBANK_ZEN, zenButtonRect.mX, zenButtonRect.mY + num);
                        if (mCursorObject.mCursorType == CursorType.WateringCan + i - 6)
                        {
                            goto IL_63E;
                        }
                    }
                    if (gameObjectType == GameObjectType.WateringCan)
                    {
                        if (mApp.mPlayerInfo.mPurchases[13] != 0)
                        {
                            g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1_GOLD, zenButtonRect.mX + Constants.ZenGardenButton_GoldenWateringCan_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_GoldenWateringCan_Offset.Y + num);
                        }
                        else
                        {
                            g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1, zenButtonRect.mX + Constants.ZenGardenButton_NormalWateringCan_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_NormalWateringCan_Offset.Y + num);
                        }
                    }
                    else if (gameObjectType == GameObjectType.Fertilizer)
                    {
                        int num2 = mApp.mPlayerInfo.mPurchases[14] - 1000;
                        if (num2 == 0)
                        {
                            g.SetColorizeImages(true);
                            g.SetColor(new SexyColor(96, 96, 96));
                        }
                        else if (mTutorialState == TutorialState.ZenGardenFertilizePlants)
                        {
                            SexyColor flashingColor = TodCommon.GetFlashingColor(mMainCounter, 75);
                            g.SetColorizeImages(true);
                            g.SetColor(flashingColor);
                        }
                        g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_FERTILIZER_BAG2, zenButtonRect.mX + Constants.ZenGardenButton_Fertiliser_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Fertiliser_Offset.Y + num);
                        g.SetColorizeImages(false);
                        string text;
                        if (!Board.cachedChargesStringsFertilizer.TryGetValue(num2, out text))
                        {
                            text = "x" + num2.ToString();
                            Board.cachedChargesStringsFertilizer.Add(num2, text);
                        }
                        TodCommon.TodDrawString(g, text, zenButtonRect.mX + Constants.ZenGardenButtonCounterOffset.X, zenButtonRect.mY + Constants.ZenGardenButtonCounterOffset.Y + num, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.Right, 0.6f);
                    }
                    else if (gameObjectType == GameObjectType.BugSpray)
                    {
                        int num3 = mApp.mPlayerInfo.mPurchases[15] - 1000;
                        if (num3 == 0)
                        {
                            g.SetColorizeImages(true);
                            g.SetColor(new SexyColor(128, 128, 128));
                        }
                        g.DrawImage(AtlasResources.IMAGE_REANIM_ZENGARDEN_BUGSPRAY_BOTTLE, zenButtonRect.mX + Constants.ZenGardenButton_BugSpray_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_BugSpray_Offset.Y + num);
                        g.SetColorizeImages(false);
                        string text2;
                        if (!Board.cachedChargesStringsBugSpray.TryGetValue(num3, out text2))
                        {
                            text2 = Common.StrFormat_(TodStringFile.TodStringTranslate("[BUG_SPRAY_MULTIPLIED_X]"), num3);
                            Board.cachedChargesStringsBugSpray.Add(num3, text2);
                        }
                        TodCommon.TodDrawString(g, text2, zenButtonRect.mX + Constants.ZenGardenButtonCounterOffset.X, zenButtonRect.mY + Constants.ZenGardenButtonCounterOffset.Y + num, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.Right, 0.6f);
                    }
                    else if (gameObjectType == GameObjectType.Phonograph)
                    {
                        g.DrawImage(AtlasResources.IMAGE_PHONOGRAPH, zenButtonRect.mX + Constants.ZenGardenButton_Phonograph_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Phonograph_Offset.Y + num);
                    }
                    else if (gameObjectType == GameObjectType.Chocolate)
                    {
                        int num4 = mApp.mPlayerInfo.mPurchases[26] - 1000;
                        if (num4 == 0)
                        {
                            g.SetColorizeImages(true);
                            g.SetColor(new SexyColor(128, 128, 128));
                        }
                        g.DrawImage(AtlasResources.IMAGE_CHOCOLATE, zenButtonRect.mX + Constants.ZenGardenButton_Chocolate_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Chocolate_Offset.Y + num);
                        g.SetColorizeImages(false);
                        string text3;
                        if (!Board.cachedChargesStringsChocolate.TryGetValue(num4, out text3))
                        {
                            text3 = Common.StrFormat_(TodStringFile.TodStringTranslate("[CHOCOLATE_MULTIPLIED_X]"), num4);
                            Board.cachedChargesStringsChocolate.Add(num4, text3);
                        }
                        TodCommon.TodDrawString(g, text3, zenButtonRect.mX + Constants.ZenGardenButtonCounterOffset.X, zenButtonRect.mY + Constants.ZenGardenButtonCounterOffset.Y + num, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.Right, 0.6f);
                    }
                    else if (gameObjectType == GameObjectType.Glove && mCursorObject.mCursorType != CursorType.PlantFromGlove && mCursorObject.mCursorType != CursorType.PlantFromWheelBarrow)
                    {
                        g.DrawImage(AtlasResources.IMAGE_ZEN_GARDENGLOVE, zenButtonRect.mX + Constants.ZenGardenButton_Glove_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_Glove_Offset.Y + num);
                    }
                    else if (gameObjectType == GameObjectType.MoneySign && mApp.mPlayerInfo.mZenGardenTutorialComplete)
                    {
                        g.DrawImage(AtlasResources.IMAGE_ZEN_MONEYSIGN, zenButtonRect.mX + Constants.ZenGardenButton_MoneySign_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_MoneySign_Offset.Y + num);
                    }
                    else if (gameObjectType == GameObjectType.Wheelbarrow)
                    {
                        DrawZenWheelBarrowButton(g, num);
                    }
                    else if (gameObjectType == GameObjectType.NextGarden)
                    {
                        if (!mMenuButton.mBtnNoDraw)
                        {
                            g.DrawImage(AtlasResources.IMAGE_ZEN_NEXT_GARDEN, zenButtonRect.mX + Constants.ZenGardenButton_NextGarden_Offset.X, zenButtonRect.mY + Constants.ZenGardenButton_NextGarden_Offset.Y + num);
                        }
                    }
                    else if (gameObjectType == GameObjectType.TreeFood)
                    {
                        int num5 = mApp.mPlayerInfo.mPurchases[28] - 1000;
                        if (num5 <= 0)
                        {
                            g.SetColorizeImages(true);
                            g.SetColor(new SexyColor(96, 96, 96));
                            num5 = 0;
                        }
                        if (!mChallenge.TreeOfWisdomCanFeed())
                        {
                            g.SetColorizeImages(true);
                            g.SetColor(new SexyColor(96, 96, 96));
                        }
                        g.SetColorizeImages(false);
                        string theText = Common.StrFormat_(TodStringFile.TodStringTranslate("[TREE_FOOD_MULTIPLIED_X]"), num5);
                        TodCommon.TodDrawString(g, theText, zenButtonRect.mX + Constants.ZenGardenButtonCounterOffset.X, zenButtonRect.mY + Constants.ZenGardenButtonCounterOffset.Y + num, Resources.FONT_HOUSEOFTERROR16, SexyColor.White, DrawStringJustification.Right, 0.6f);
                    }
                }
                IL_63E:;
            }
        }

        public void OffsetYForPlanting(ref int theY, SeedType theSeedType)
        {
            if (Plant.IsFlying(theSeedType) || theSeedType == SeedType.Gravebuster)
            {
                theY += 15;
            }
            if (theSeedType == SeedType.Spikeweed || theSeedType == SeedType.Spikerock)
            {
                theY -= 15;
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden && mBackground == BackgroundType.Greenhouse)
            {
                theY -= 25;
            }
        }

        public void SetFutureMode(bool theEnableFuture)
        {
            mApp.PlaySample(Resources.SOUND_BOING);
            mFutureMode = theEnableFuture;
            mApp.mFutureMode = theEnableFuture;
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead)
                {
                    zombie.EnableFuture(theEnableFuture);
                }
            }
        }

        public void SetPinataMode(bool theEnablePinata)
        {
            mApp.PlayFoley(FoleyType.Juicy);
            mPinataMode = theEnablePinata;
            mApp.mPinataMode = theEnablePinata;
        }

        public void SetDanceMode(bool theEnableDance)
        {
            mApp.PlayFoley(FoleyType.Dancer);
            mDanceMode = theEnableDance;
            mApp.mDanceMode = theEnableDance;
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead)
                {
                    zombie.EnableDanceMode(theEnableDance);
                }
            }
        }

        public void SetDaisyMode(bool theEnableDaisy)
        {
            mApp.PlaySample(Resources.SOUND_LOADINGBAR_FLOWER);
            mDaisyMode = theEnableDaisy;
            mApp.mDaisyMode = theEnableDaisy;
        }

        public void SetSukhbirMode(bool theEnableSukhbir)
        {
            mSukhbirMode = theEnableSukhbir;
            mApp.mSukhbirMode = theEnableSukhbir;
        }

        public bool MouseHitTestPlant(int x, int y, out HitResult theHitResult, bool posScaled)
        {
            theHitResult = default(HitResult);
            if (mCursorObject.mCursorType == CursorType.CobcannonTarget || mCursorObject.mCursorType == CursorType.Hammer)
            {
                return false;
            }
            if (!posScaled)
            {
                x = (int)(x * Constants.IS);
                y = (int)(y * Constants.IS);
            }
            Plant plant = SpecialPlantHitTest(x, y);
            if (plant != null)
            {
                theHitResult.mObject = plant;
                theHitResult.mObjectType = GameObjectType.Plant;
                return true;
            }
            int theGridX = PixelToGridX(x, y);
            int theGridY = PixelToGridY(x, y);
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                plant = GetTopPlantAt(theGridX, theGridY, TopPlant.ZenToolOrder);
                if (mCursorObject.mCursorType == CursorType.WateringCan && (plant == null || !mApp.mZenGarden.PlantCanBeWatered(plant)))
                {
                    int theGridX2 = PixelToGridX(x - 30, y - 20);
                    int theGridY2 = PixelToGridY(x - 30, y - 20);
                    Plant topPlantAt = GetTopPlantAt(theGridX2, theGridY2, TopPlant.ZenToolOrder);
                    if (topPlantAt != null && mApp.mZenGarden.PlantCanBeWatered(topPlantAt))
                    {
                        plant = topPlantAt;
                    }
                }
            }
            else
            {
                plant = GetTopPlantAt(theGridX, theGridY, TopPlant.DiggingOrder);
                if (plant != null && (plant.mSeedType == SeedType.Lilypad || plant.mSeedType == SeedType.Flowerpot) && GetTopPlantAt(theGridX, theGridY, TopPlant.OnlyPumpkin) != null)
                {
                    plant = null;
                }
            }
            if (plant != null && mCursorObject.mCursorType == CursorType.Chocolate && !mApp.mZenGarden.PlantCanHaveChocolate(plant))
            {
                theHitResult.mObject = null;
                theHitResult.mObjectType = GameObjectType.None;
                return false;
            }
            if (plant != null)
            {
                theHitResult.mObject = plant;
                theHitResult.mObjectType = GameObjectType.Plant;
                return true;
            }
            return false;
        }

        public void DoTypingCheck(KeyCode theKey)
        {
            if (mApp.mKonamiCheck.Check(theKey))
            {
                mApp.PlayFoley(FoleyType.Drop);
                return;
            }
            if (mApp.mMustacheCheck.Check(theKey) || mApp.mMoustacheCheck.Check(theKey))
            {
                SetMustacheMode(!mMustacheMode);
                return;
            }
            if (mApp.mSuperMowerCheck.Check(theKey) || mApp.mSuperMowerCheck2.Check(theKey))
            {
                SetSuperMowerMode(!mSuperMowerMode);
                return;
            }
            if (mApp.mFutureCheck.Check(theKey))
            {
                SetFutureMode(!mFutureMode);
                return;
            }
            if (mApp.mPinataCheck.Check(theKey))
            {
                if (mApp.CanDoPinataMode())
                {
                    SetPinataMode(!mPinataMode);
                    return;
                }
                if (mApp.mGameScene == GameScenes.Playing)
                {
                    DisplayAdvice("[CANT_USE_CODE]", MessageStyle.BigMiddleFast, AdviceType.None);
                }
                mApp.PlaySample(Resources.SOUND_BUZZER);
                return;
            }
            else if (mApp.mDanceCheck.Check(theKey))
            {
                if (mApp.CanDoDanceMode())
                {
                    SetDanceMode(!mDanceMode);
                    return;
                }
                if (mApp.mGameScene == GameScenes.Playing)
                {
                    DisplayAdvice("[CANT_USE_CODE]", MessageStyle.BigMiddleFast, AdviceType.None);
                }
                mApp.PlaySample(Resources.SOUND_BUZZER);
                return;
            }
            else
            {
                if (!mApp.mDaisyCheck.Check(theKey))
                {
                    if (mApp.mSukhbirCheck.Check(theKey))
                    {
                        SetSukhbirMode(!mSukhbirMode);
                    }
                    return;
                }
                if (mApp.CanDoDaisyMode())
                {
                    SetDaisyMode(!mDaisyMode);
                    return;
                }
                if (mApp.mGameScene == GameScenes.Playing)
                {
                    DisplayAdvice("[CANT_USE_CODE]", MessageStyle.BigMiddleFast, AdviceType.None);
                }
                mApp.PlaySample(Resources.SOUND_BUZZER);
                return;
            }
        }

        public void CompleteEndLevelSequenceForSaving()
        {
            if (CanDropLoot())
            {
                LawnMower lawnMower = null;
                while (IterateLawnMowers(ref lawnMower))
                {
                    if (lawnMower.mMowerState != LawnMowerState.Triggered && lawnMower.mMowerState != LawnMowerState.Squished)
                    {
                        int coinValue = Coin.GetCoinValue(CoinType.Gold);
                        mApp.mPlayerInfo.AddCoins(coinValue);
                        mCoinsCollected += coinValue;
                    }
                }
            }
            Coin coin = null;
            while (IterateCoins(ref coin))
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
            CheckForPostGameAchievements();
            mApp.UpdatePlayerProfileForFinishingLevel();
        }

        public void RemoveZombiesForRepick()
        {
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && !zombie.IsDeadOrDying() && zombie.mMindControlled && zombie.mPosX > 720f)
                {
                    zombie.DieNoLoot(false);
                }
            }
        }

        public bool IsSurvivalStageWithRepick()
        {
            return mApp.IsSurvivalMode() && !IsFinalSurvivalStage();
        }

        public bool IsLastStandStageWithRepick()
        {
            return mApp.mGameMode == GameMode.ChallengeLastStand && !IsLastStandFinalStage();
        }

        public bool GrantAchievement(AchievementId theAchievement)
        {
            return GrantAchievement(theAchievement, true);
        }

        public bool GrantAchievement(AchievementId theAchievement, bool show)
        {
            return ReportAchievement.GiveAchievement(theAchievement);
        }

        public bool CheckForPostGameAchievements()
        {
            if (mApp.IsWhackAZombieLevel() || mApp.IsScaryPotterLevel() || mApp.IsWallnutBowlingLevel())
            {
                return false;
            }
            bool flag = false;
            if (mApp.IsAdventureMode() && mLevel == 50 && mApp.mPlayerInfo.mFinishedAdventure < 1)
            {
                flag = (GrantAchievement(AchievementId.ACHIEVEMENT_HOME_SECURITY, false) || flag);
            }
            return flag;
        }

        public int GetLiveGargantuarCount()
        {
            int num = 0;
            int count = mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mZombies[i];
                if (!zombie.mDead && zombie.mHasHead && !zombie.IsDeadOrDying() && zombie.IsOnBoard() && (zombie.mZombieType == ZombieType.Gargantuar || zombie.mZombieType == ZombieType.RedeyeGargantuar))
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
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                mApp.mBoardResult = BoardResult.Quit;
                TryToSaveGame();
                mApp.DoBackToMain();
                return true;
            }
            if (!CanInteractWithBoardButtons())
            {
                return true;
            }
            RefreshSeedPacketFromCursor();
            mApp.DoNewOptions(false);
            return true;
        }

        public bool SaveToFile(Sexy.Buffer b)
        {
            LeaderBoardComm.RecordResult(LeaderboardGameMode.Adventure, (int)mApp.mPlayerInfo.mZombiesKilled);
            try
            {
                b.WriteLong(this.mLevel);
                b.WriteLong((int)this.mApp.mGameScene);
                b.WriteLong(Board.mPeashootersPlanted);
                b.WriteBoolean(mNomNomNomAchievementTracker);
                b.WriteBoolean(mNoFungusAmongUsAchievementTracker);
                b.WriteLong((int)mBackground);
                b.WriteLong(mBoardFadeOutCounter);
                b.WriteLong(mBoardRandSeed);
                b.WriteLong(mBonusLawnMowersRemaining);
                b.WriteBoolean(mCatapultPlantsUsed);
                mChallenge.SaveToFile(b);
                b.WriteLong(mChocolateCollected);
                b.WriteLong(mCobCannonCursorDelayCounter);
                b.WriteLong(mCobCannonMouseX);
                b.WriteLong(mCobCannonMouseY);
                b.WriteLong(mCoinBankFadeCount);
                b.WriteLong(mCoins.Count);
                for (int i = 0; i < mCoins.Count; i++)
                {
                    mCoins[i].SaveToFile(b);
                }
                b.WriteLong(mCoinsCollected);
                b.WriteLong(mCollectedCoinStreak);
                b.WriteLong(mCurrentWave);
                mCursorObject.SaveToFile(b);
                b.WriteBoolean(mDaisyMode);
                b.WriteBoolean(mDanceMode);
                b.WriteLong(mDiamondsCollected);
                b.WriteLong(mDoomsUsed);
                b.WriteBoolean(mDroppedFirstCoin);
                b.WriteLong(mEffectCounter);
                b.WriteBoolean(mEnableGraveStones);
                b.WriteBoolean(mFinalBossKilled);
                b.WriteLong(mFinalWaveSoundCounter);
                b.WriteLong(mFlagRaiseCounter);
                b.WriteLong(mFogBlownCountDown);
                b.WriteFloat(mFogOffset);
                b.WriteLong(mFwooshCountDown);
                b.WriteLong(mGameID);
                b.WriteLong(mGargantuarsKillsByCornCob);
                b.WriteLong(mGravesCleared);
                b.WriteLong2DArray(mGridCelFog);
                b.WriteLong2DArray(mGridCelLook);
                b.WriteLong3DArray(mGridCelOffset);
                b.WriteLong(mGridItems.Count);
                for (int j = 0; j < mGridItems.Count; j++)
                {
                    mGridItems[j].SaveToFile(b);
                }
                int length = mGridSquareType.GetLength(0);
                int length2 = mGridSquareType.GetLength(1);
                b.WriteLong(length);
                b.WriteLong(length2);
                for (int k = 0; k < length; k++)
                {
                    for (int l = 0; l < length2; l++)
                    {
                        b.WriteLong((int)mGridSquareType[k, l]);
                    }
                }
                b.WriteBooleanArray(mHelpDisplayed);
                b.WriteLong((int)mHelpIndex);
                b.WriteLong(mHugeWaveCountDown);
                b.WriteLongArray(mIceMinX);
                b.WriteLongArray(mIceTimer);
                b.WriteLong(mIceTrapCounter);
                b.WriteBoolean(mIgnoreMouseUp);
                b.WriteBoolean(mKilledYeti);
                b.WriteLong(mLastBungeeWave);
                b.WriteLong(mLastToolX);
                b.WriteLong(mLastToolY);
                b.WriteLong(mLastWMUpdateCount);
                b.WriteLong(mLawnMowers.Count);
                for (int m = 0; m < mLawnMowers.Count; m++)
                {
                    mLawnMowers[m].SaveToFile(b);
                }
                b.WriteBoolean(mLevelAwardSpawned);
                b.WriteBoolean(mLevelComplete);
                b.WriteLong(mLevelFadeCount);
                b.WriteString(mLevelStr);
                b.WriteLong(mMainCounter);
                b.WriteLong(mMaxSunPlants);
                b.WriteFloat(mMinFPS);
                b.WriteBoolean(mMushroomAndCoffeeBeansOnly);
                b.WriteBoolean(mMushroomsUsed);
                b.WriteLong(mNextSurvivalStageCounter);
                b.WriteLong(mNumSunsFallen);
                b.WriteLong(mNumWaves);
                b.WriteBoolean(mNutsUsed);
                b.WriteLong(mOutOfMoneyCounter);
                b.WriteBoolean(mPeaShooterUsed);
                b.WriteBoolean(mPinataMode);
                b.WriteBoolean(mPlanternOrBloverUsed);
                b.WriteLong(mPlants.Count);
                for (int n = 0; n < mPlants.Count; n++)
                {
                    mPlants[n].SaveToFile(b);
                }
                b.WriteLong(mPlantsEaten);
                b.WriteLong(mPlantsShoveled);
                b.WriteLong(mPlayTimeActiveLevel);
                b.WriteLong(mPlayTimeInactiveLevel);
                b.WriteLong(mPottedPlantsCollected);
                b.WriteLong(mProgressMeterWidth);
                b.WriteLong(mProjectiles.Count);
                for (int num = 0; num < mProjectiles.Count; num++)
                {
                    mProjectiles[num].SaveToFile(b);
                }
                b.WriteLong(mRiseFromGraveCounter);
                b.WriteLong(mRowPickingArray.Length);
                for (int num2 = 0; num2 < mRowPickingArray.Length; num2++)
                {
                    mRowPickingArray[num2].SaveToFile(b);
                }
                b.WriteLong(mScoreNextMowerCounter);
                b.WriteBoolean(mShowShovel);
                mSeedBank.SaveToFile(b);
                b.WriteLong(mSodPosition);
                b.WriteLong(mSpecialGraveStoneX);
                b.WriteLong(mSpecialGraveStoneY);
                b.WriteLong(mSunCountDown);
                b.WriteLong(mSunMoney);
                b.WriteBoolean(mSuperMowerMode);
                b.WriteLong(mTimeStopCounter);
                b.WriteLong(mTotalSpawnedWaves);
                b.WriteLong(mTriggeredLawnMowers);
                b.WriteLong((int)mTutorialState);
                b.WriteLong(mTutorialTimer);
                b.WriteLongArray(mWaveRowGotLawnMowered);
                b.WriteBooleanArray(mZombieAllowed);
                b.WriteLong(mZombieCountDown);
                b.WriteLong(mZombieCountDownStart);
                b.WriteLong(mZombieHealthToNextWave);
                b.WriteLong(mZombieHealthWaveStart);
                b.WriteLong(mZombies.Count);
                for (int num3 = 0; num3 < mZombies.Count; num3++)
                {
                    mZombies[num3].SaveToFile(b);
                }
                length = mZombiesInWave.GetLength(0);
                length2 = mZombiesInWave.GetLength(1);
                b.WriteLong(length);
                b.WriteLong(length2);
                for (int num4 = 0; num4 < length; num4++)
                {
                    for (int num5 = 0; num5 < length2; num5++)
                    {
                        b.WriteLong((int)mZombiesInWave[num4, num5]);
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

        public const int LevelLimit = 3;

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

        public static TodWeightedArray[] aZombieWeightArray = new TodWeightedArray[33];

        public static TodWeightedArray[] aPickArray = new TodWeightedArray[Constants.MAX_GRIDSIZEY];

        public static TodWeightedGridArray[] aGridArray = new TodWeightedGridArray[10];

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
