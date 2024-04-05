using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class Plant : GameObject
    {
        public static void PreallocateMemory()
        {
            for (int i = 0; i < 100; i++)
            {
                new Plant().PrepareForReuse();
            }
        }

        public static Plant GetNewPlant()
        {
            if (Plant.unusedObjects.Count > 0)
            {
                Plant plant = Plant.unusedObjects.Pop();
                plant.Reset();
                return plant;
            }
            return new Plant();
        }

        public override void PrepareForReuse()
        {
            Plant.unusedObjects.Push(this);
        }

        protected override void Reset()
        {
            base.Reset();
            lastPlayedBodyReanim_Name = string.Empty;
            lastPlayedBodyReanim_Type = ReanimLoopType.PlayOnce;
            lastPlayedBodyReanim_BlendTime = 0;
            lastPlayedBodyReanim_AnimRate = 0f;
            mSeedType = SeedType.Peashooter;
            mPlantCol = 0;
            mAnimCounter = 0;
            mFrame = 0;
            mFrameLength = 0;
            mNumFrames = 0;
            mState = PlantState.Notready;
            mPlantHealth = 0;
            mPlantMaxHealth = 0;
            mSubclass = 0;
            mDisappearCountdown = 0;
            mDoSpecialCountdown = 0;
            mStateCountdown = 0;
            mLaunchCounter = 0;
            mLaunchRate = 0;
            mPlantRect = default(TRect);
            mPlantAttackRect = default(TRect);
            mTargetX = 0;
            mTargetY = 0;
            mStartRow = 0;
            mParticleID = null;
            mShootingCounter = 0;
            mBodyReanimID = null;
            mHeadReanimID = null;
            mHeadReanimID2 = null;
            mHeadReanimID3 = null;
            mBlinkReanimID = null;
            mLightReanimID = null;
            mSleepingReanimID = null;
            mBlinkCountdown = 0;
            mRecentlyEatenCountdown = 0;
            mEatenFlashCountdown = 0;
            mBeghouledFlashCountdown = 0;
            mShakeOffsetX = 0f;
            mShakeOffsetY = 0f;
            for (int i = 0; i < mMagnetItems.Length; i++)
            {
                mMagnetItems[i] = null;
            }
            mTargetZombieID = null;
            mWakeUpCounter = 0;
            mOnBungeeState = PlantOnBungeeState.NotOnBungee;
            mImitaterType = SeedType.Peashooter;
            mPottedPlantIndex = 0;
            mAnimPing = false;
            mDead = false;
            mSquished = false;
            mIsAsleep = false;
            mIsOnBoard = false;
            mHighlighted = false;
            mInFlowerPot = false;
        }

        private Plant()
        {
            Reset();
        }

        public void PlantInitialize(int theGridX, int theGridY, SeedType theSeedType, SeedType theImitaterType)
        {
            for (int i = 0; i < mMagnetItems.Length; i++)
            {
                if (mMagnetItems[i] == null)
                {
                    mMagnetItems[i] = new MagnetItem();
                }
                else
                {
                    mMagnetItems[i].Reset();
                }
            }
            mPlantCol = theGridX;
            mRow = theGridY;
            if (mBoard != null)
            {
                mX = mBoard.GridToPixelX(theGridX, theGridY);
                mY = mBoard.GridToPixelY(theGridX, theGridY);
                if (mApp.mGameMode == GameMode.ChallengeZenGarden)
                {
                    mY -= Constants.ZenGardenGreenhouseOffset.Y;
                }
            }
            mAnimCounter = 0;
            mAnimPing = true;
            mFrame = 0;
            mShootingCounter = 0;
            mFrameLength = TodCommon.RandRangeInt(12, 18);
            mNumFrames = 5;
            mState = PlantState.Notready;
            mDead = false;
            mSquished = false;
            mSeedType = theSeedType;
            mImitaterType = theImitaterType;
            mPlantHealth = 300;
            mDoSpecialCountdown = 0;
            mDisappearCountdown = 200;
            mTargetX = -1;
            mTargetY = -1;
            mStateCountdown = 0;
            mStartRow = mRow;
            mParticleID = null;
            mBodyReanimID = null;
            mHeadReanimID = null;
            mHeadReanimID2 = null;
            mHeadReanimID3 = null;
            mBlinkReanimID = null;
            mLightReanimID = null;
            mSleepingReanimID = null;
            mBlinkCountdown = 0;
            mRecentlyEatenCountdown = 0;
            mEatenFlashCountdown = 0;
            mBeghouledFlashCountdown = 0;
            mWidth = 80;
            mHeight = 80;
            mShakeOffsetX = 0f;
            mShakeOffsetY = 0f;
            mIsAsleep = false;
            mWakeUpCounter = 0;
            mOnBungeeState = PlantOnBungeeState.NotOnBungee;
            mPottedPlantIndex = -1;
            PlantDefinition plantDefinition = Plant.GetPlantDefinition(theSeedType);
            mLaunchRate = plantDefinition.mLaunchRate;
            mSubclass = (int)plantDefinition.mSubClass;
            mRenderOrder = CalcRenderOrder();
            Reanimation reanimation = null;
            string empty = string.Empty;
            if (plantDefinition.mReanimationType != ReanimationType.None)
            {
                float theY = Plant.PlantDrawHeightOffset(mBoard, this, mSeedType, mPlantCol, mRow);
                reanimation = mApp.AddReanimation(0f, theY, mRenderOrder + 1, plantDefinition.mReanimationType);
                reanimation.mLoopType = ReanimLoopType.Loop;
                reanimation.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
                if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
                {
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                }
                if (mApp.IsWallnutBowlingLevel() && reanimation.TrackExists(Reanimation.ReanimTrackId__ground))
                {
                    reanimation.SetFramesForLayer(Reanimation.ReanimTrackId__ground);
                    if (mSeedType == SeedType.Wallnut || mSeedType == SeedType.ExplodeONut)
                    {
                        reanimation.mAnimRate = TodCommon.RandRangeFloat(12f, 18f);
                    }
                    else if (mSeedType == SeedType.GiantWallnut)
                    {
                        reanimation.mAnimRate = TodCommon.RandRangeFloat(6f, 10f);
                    }
                }
                reanimation.mIsAttachment = true;
                mBodyReanimID = mApp.ReanimationGetID(reanimation);
                mBlinkCountdown = 400 + RandomNumbers.NextNumber(400);
            }
            if (Plant.IsNocturnal(mSeedType) && mBoard != null && !mBoard.StageIsNight())
            {
                SetSleeping(true);
            }
            if (mLaunchRate > 0)
            {
                if (MakesSun())
                {
                    mLaunchCounter = TodCommon.RandRangeInt(300, mLaunchRate / 2);
                }
                else
                {
                    mLaunchCounter = TodCommon.RandRangeInt(0, mLaunchRate);
                }
            }
            else
            {
                mLaunchCounter = 0;
            }
            if (theSeedType == SeedType.Blover)
            {
                mDoSpecialCountdown = 50;
                if (IsInPlay())
                {
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_blow);
                    reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
                    reanimation.mAnimRate = 20f;
                }
                else
                {
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                    reanimation.mAnimRate = 10f;
                }
            }
            else if (theSeedType == SeedType.Peashooter || theSeedType == SeedType.Snowpea || theSeedType == SeedType.Repeater || theSeedType == SeedType.Leftpeater || theSeedType == SeedType.Gatlingpea)
            {
                if (reanimation != null)
                {
                    reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                    Reanimation reanimation2 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
                    reanimation2.mLoopType = ReanimLoopType.Loop;
                    reanimation2.mAnimRate = reanimation.mAnimRate;
                    reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
                    mHeadReanimID = mApp.ReanimationGetID(reanimation2);
                    if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_stem))
                    {
                        reanimation2.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_stem);
                    }
                    else if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
                    {
                        reanimation2.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                    }
                }
            }
            else if (theSeedType == SeedType.Splitpea)
            {
                Debug.ASSERT(reanimation != null);
                reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                Reanimation reanimation3 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
                reanimation3.mLoopType = ReanimLoopType.Loop;
                reanimation3.mAnimRate = reanimation.mAnimRate;
                reanimation3.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
                reanimation3.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                mHeadReanimID = mApp.ReanimationGetID(reanimation3);
                Reanimation reanimation4 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
                reanimation4.mLoopType = ReanimLoopType.Loop;
                reanimation4.mAnimRate = reanimation.mAnimRate;
                reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_idle);
                reanimation4.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                mHeadReanimID2 = mApp.ReanimationGetID(reanimation4);
            }
            else if (theSeedType == SeedType.Threepeater)
            {
                Debug.ASSERT(reanimation != null);
                reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                Reanimation reanimation5 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
                reanimation5.mLoopType = ReanimLoopType.Loop;
                reanimation5.mAnimRate = reanimation.mAnimRate;
                reanimation5.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle1);
                reanimation5.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_head1);
                mHeadReanimID = mApp.ReanimationGetID(reanimation5);
                Reanimation reanimation6 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
                reanimation6.mLoopType = ReanimLoopType.Loop;
                reanimation6.mAnimRate = reanimation.mAnimRate;
                reanimation6.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle2);
                reanimation6.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_head2);
                mHeadReanimID2 = mApp.ReanimationGetID(reanimation6);
                Reanimation reanimation7 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
                reanimation7.mLoopType = ReanimLoopType.Loop;
                reanimation7.mAnimRate = reanimation.mAnimRate;
                reanimation7.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle3);
                reanimation7.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_head3);
                mHeadReanimID3 = mApp.ReanimationGetID(reanimation7);
            }
            else if (theSeedType == SeedType.Wallnut)
            {
                mPlantHealth = 4000;
                mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
            }
            else if (theSeedType == SeedType.ExplodeONut)
            {
                mPlantHealth = 4000;
                mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
                reanimation.mColorOverride = new SexyColor(255, 64, 64);
            }
            else if (theSeedType == SeedType.GiantWallnut)
            {
                mPlantHealth = 4000;
                mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
            }
            else if (theSeedType == SeedType.Tallnut)
            {
                mPlantHealth = 8000;
                mHeight = 80;
                mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
            }
            else if (theSeedType == SeedType.Garlic)
            {
                Debug.ASSERT(reanimation != null);
                mPlantHealth = 400;
                reanimation.SetTruncateDisappearingFrames(empty, false);
            }
            else if (theSeedType == SeedType.GoldMagnet)
            {
                Debug.ASSERT(reanimation != null);
                reanimation.SetTruncateDisappearingFrames(empty, false);
            }
            else if (theSeedType == SeedType.Cherrybomb)
            {
                Debug.ASSERT(reanimation != null);
                if (IsInPlay())
                {
                    mDoSpecialCountdown = 100;
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_explode);
                    reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
                    mApp.PlayFoley(FoleyType.ReverseExplosion);
                }
            }
            else if (theSeedType == SeedType.Imitater)
            {
                Debug.ASSERT(reanimation != null);
                reanimation.mAnimRate = TodCommon.RandRangeFloat(25f, 30f);
                mStateCountdown = 200;
            }
            else if (theSeedType == SeedType.Jalapeno)
            {
                Debug.ASSERT(reanimation != null);
                if (IsInPlay())
                {
                    mDoSpecialCountdown = 100;
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_explode);
                    reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
                    mApp.PlayFoley(FoleyType.ReverseExplosion);
                }
            }
            else if (theSeedType == SeedType.Potatomine)
            {
                Debug.ASSERT(reanimation != null);
                reanimation.mAnimRate = 12f;
                if (IsInPlay())
                {
                    reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_glow, -1);
                    mStateCountdown = 1500;
                }
                else
                {
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_armed);
                    mState = PlantState.PotatoArmed;
                }
            }
            else if (theSeedType == SeedType.Gravebuster)
            {
                Debug.ASSERT(reanimation != null);
                if (IsInPlay())
                {
                    mY += 8;
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_land);
                    reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
                    mState = PlantState.GravebusterLanding;
                    mApp.PlayFoley(FoleyType.Gravebusterchomp);
                }
            }
            else if (theSeedType == SeedType.Sunshroom)
            {
                Debug.ASSERT(reanimation != null);
                reanimation.mFrameBasePose = 6;
                if (IsInPlay())
                {
                    mX += RandomNumbers.NextNumber(10) - 5;
                    mY += RandomNumbers.NextNumber(10) - 5;
                }
                else if (mIsAsleep)
                {
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_bigsleep);
                }
                else
                {
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_bigidle);
                }
                mState = PlantState.SunshroomSmall;
                mStateCountdown = 12000;
            }
            else if (theSeedType == SeedType.Puffshroom || theSeedType == SeedType.Seashroom)
            {
                if (IsInPlay())
                {
                    mX += RandomNumbers.NextNumber(10) - 5;
                    mY += RandomNumbers.NextNumber(6) - 3;
                }
            }
            else if (theSeedType == SeedType.Pumpkinshell)
            {
                mPlantHealth = 4000;
                mWidth = 120;
                Debug.ASSERT(reanimation != null);
                reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_pumpkin_back, 1);
            }
            else if (theSeedType == SeedType.Chomper)
            {
                mState = PlantState.Ready;
            }
            else if (theSeedType == SeedType.Plantern)
            {
                mStateCountdown = 50;
                if (!IsOnBoard() || mApp.mGameMode != GameMode.ChallengeZenGarden)
                {
                    AddAttachedParticle(mX + 40, mY + 40, 500001, ParticleEffect.LanternShine);
                }
                if (IsInPlay())
                {
                    mApp.PlaySample(Resources.SOUND_PLANTERN);
                }
            }
            else if (theSeedType != SeedType.Torchwood)
            {
                if (theSeedType == SeedType.Marigold)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                }
                else if (theSeedType == SeedType.Cactus)
                {
                    mState = PlantState.CactusLow;
                }
                else if (theSeedType == SeedType.InstantCoffee)
                {
                    mDoSpecialCountdown = 100;
                }
                else if (theSeedType == SeedType.Scaredyshroom)
                {
                    mState = PlantState.Ready;
                }
                else if (theSeedType == SeedType.Cobcannon)
                {
                    if (IsInPlay())
                    {
                        mState = PlantState.CobcannonArming;
                        mStateCountdown = 500;
                        Debug.ASSERT(reanimation != null);
                        reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_unarmed_idle);
                    }
                }
                else if (theSeedType == SeedType.Kernelpult)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.AssignRenderGroupToPrefix("Cornpult_butter", -1);
                }
                else if (theSeedType == SeedType.Magnetshroom)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetTruncateDisappearingFrames(empty, false);
                }
                else if (theSeedType == SeedType.Spikerock)
                {
                    mPlantHealth = 450;
                    Debug.ASSERT(reanimation != null);
                }
                else if (theSeedType != SeedType.Sprout)
                {
                    if (theSeedType == SeedType.Flowerpot)
                    {
                        if (IsInPlay())
                        {
                            mState = PlantState.FlowerpotInvulnerable;
                            mStateCountdown = 100;
                        }
                    }
                    else if (theSeedType == SeedType.Lilypad)
                    {
                        if (IsInPlay())
                        {
                            mState = PlantState.LilypadInvulnerable;
                            mStateCountdown = 100;
                        }
                    }
                    else if (theSeedType == SeedType.Tanglekelp)
                    {
                        Debug.ASSERT(reanimation != null);
                        reanimation.SetTruncateDisappearingFrames(empty, false);
                    }
                }
            }
            if (mApp.mGameMode == GameMode.ChallengeBigTime && (mSeedType == SeedType.Wallnut || mSeedType == SeedType.Sunflower || mSeedType == SeedType.Marigold))
            {
                mPlantHealth *= 2;
            }
            mPlantMaxHealth = mPlantHealth;
            if (mSeedType != SeedType.Flowerpot && IsOnBoard())
            {
                Plant flowerPotAt = mBoard.GetFlowerPotAt(mPlantCol, mRow);
                if (flowerPotAt != null)
                {
                    Reanimation reanimation8 = mApp.ReanimationGet(flowerPotAt.mBodyReanimID);
                    reanimation8.mAnimRate = 0f;
                    mInFlowerPot = true;
                }
            }
            if (theImitaterType == SeedType.Imitater)
            {
                FilterEffectType aFilterEffect = FilterEffectType.WashedOut;
                if (mSeedType == SeedType.Hypnoshroom || mSeedType == SeedType.Squash || mSeedType == SeedType.Potatomine || mSeedType == SeedType.Garlic || mSeedType == SeedType.Lilypad)
                {
                    aFilterEffect = FilterEffectType.LessWashedOut;
                }
                Reanimation reanimation1 = mApp.ReanimationTryToGet(mBodyReanimID);
                if (reanimation != null)
                {
                    reanimation.mFilterEffect = aFilterEffect;
                }
                Reanimation reanimation2 = mApp.ReanimationTryToGet(mHeadReanimID);
                if (reanimation2 != null)
                {
                    reanimation2.mFilterEffect = aFilterEffect;
                }
                Reanimation reanimation3 = mApp.ReanimationTryToGet(mHeadReanimID2);
                if (reanimation3 != null)
                {
                    reanimation3.mFilterEffect = aFilterEffect;
                }
                Reanimation reanimation4 = mApp.ReanimationTryToGet(mHeadReanimID3);
                if (reanimation4 != null)
                {
                    reanimation4.mFilterEffect = aFilterEffect;
                }
            }
            checkForPlantAchievements();
            UpdateReanim();
        }

        public void Update()//3update
        {
            if ((!IsOnBoard() || mApp.mGameScene != GameScenes.LevelIntro || !mApp.IsWallnutBowlingLevel()) && (!IsOnBoard() || mApp.mGameMode != GameMode.ChallengeZenGarden) && (!IsOnBoard() || !mBoard.mCutScene.ShouldRunUpsellBoard()) && IsOnBoard() && mApp.mGameScene != GameScenes.Playing)
            {
                return;
            }
            UpdateAbilities();
            Animate();
            if (mPlantHealth < 0)
            {
                Die();
            }
            UpdateReanim();
        }

        public void Animate()//3update
        {
            if ((mSeedType == SeedType.Cherrybomb || mSeedType == SeedType.Jalapeno) && mApp.mGameMode != GameMode.ChallengeZenGarden)
            {
                mShakeOffsetX = TodCommon.RandRangeFloat(0f, 2f) - 1f;
                mShakeOffsetY = TodCommon.RandRangeFloat(0f, 2f) - 1f;
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden && mPottedPlantIndex != -1)
            {
                UpdateNeedsFood();
            }
            if (mRecentlyEatenCountdown > 0)
            {
                //mRecentlyEatenCountdown -= 3;
                mRecentlyEatenCountdown--;
            }
            if (mEatenFlashCountdown > 0)
            {
                //mEatenFlashCountdown -= 3;
                mEatenFlashCountdown--;
            }
            if (mBeghouledFlashCountdown > 0)
            {
                //mBeghouledFlashCountdown -= 3;
                mBeghouledFlashCountdown--;
            }
            if (mSquished)
            {
                mFrame = 0;
                return;
            }
            if (mSeedType == SeedType.Wallnut || mSeedType == SeedType.Tallnut)
            {
                AnimateNuts();
            }
            else if (mSeedType == SeedType.Garlic)
            {
                AnimateGarlic();
            }
            else if (mSeedType == SeedType.Pumpkinshell)
            {
                AnimatePumpkin();
            }
            UpdateBlink();
            if (mAnimPing)
            {
                int num = mFrameLength * mNumFrames - 1;
                if (mAnimCounter < num)
                {
                    //mAnimCounter += 3;
                    mAnimCounter++;
                }
                else
                {
                    mAnimPing = false;
                    mAnimCounter -= mFrameLength;
                }
            }
            else if (mAnimCounter > 0)
            {
                //mAnimCounter -= 3;
                mAnimCounter--;
            }
            else
            {
                mAnimPing = true;
                mAnimCounter += mFrameLength;
            }
            mFrame = mAnimCounter / mFrameLength;
        }

        public void Draw(Graphics g)
        {
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            int theCelRow = 0;
            float num = 0f;
            float num2 = Plant.PlantDrawHeightOffset(mBoard, this, mSeedType, mPlantCol, mRow);
            if (Plant.IsFlying(mSeedType) && mSquished)
            {
                num2 += 30f;
            }
            int theCelCol = mFrame;
            Image image = Plant.GetImage(mSeedType);
            if (mSquished)
            {
                if (mSeedType == SeedType.Flowerpot)
                {
                    num2 -= 15f;
                }
                if (mSeedType == SeedType.InstantCoffee)
                {
                    num2 -= 20f;
                }
                float ratioSquished = 0.5f;
                g.SetScale(1f, ratioSquished, 0f, 0f);
                //Image imageInAtlasById = AtlasResources.GetImageInAtlasById((int)(10300 + mSeedType));
                g.SetColorizeImages(true);
                g.SetColor(new Color(255, 255, 255, (int)(255f * Math.Min(1f, mDisappearCountdown / 100f))));
                Plant.DrawSeedType(g, mSeedType, mImitaterType, DrawVariation.Normal, num * Constants.S/* + imageInAtlasById.GetCelWidth() / 2 */+ Constants.Plant_Squished_Offset.X, num2 * Constants.S + (float)Constants.New.Board_GridCellSizeY_6Rows * (1-ratioSquished) * Constants.S/*+ imageInAtlasById.GetCelHeight()*/ + Constants.Plant_Squished_Offset.Y);
                g.SetScale(1f, 1f, 0f, 0f);
                g.SetColorizeImages(false);
                return;
            }
            bool flag = false;
            Plant plant = null;
            if (IsOnBoard())
            {
                plant = mBoard.GetPumpkinAt(mPlantCol, mRow);
                if (plant != null)
                {
                    Plant plant2 = mBoard.GetTopPlantAt(mPlantCol, mRow, TopPlant.OnlyNormalPosition);
                    if (plant2 != null && plant2.mRenderOrder > plant.mRenderOrder)
                    {
                        plant2 = null;
                    }
                    if (plant2 != null && plant2.mOnBungeeState == PlantOnBungeeState.GettingGrabbedByBungee)
                    {
                        plant2 = null;
                    }
                    if (plant2 == this)
                    {
                        flag = true;
                    }
                    if (plant2 == null && mSeedType == SeedType.Pumpkinshell)
                    {
                        flag = true;
                    }
                }
                else if (mSeedType == SeedType.Pumpkinshell)
                {
                    flag = true;
                    plant = this;
                }
            }
            else if (mSeedType == SeedType.Pumpkinshell)
            {
                flag = true;
                plant = this;
            }
            if (!GlobalStaticVars.gLowFramerate)
            {
                DrawShadow(g, num, num2);
            }
            if (Plant.IsFlying(mSeedType))
            {
                int num3;
                if (IsOnBoard())
                {
                    num3 = mBoard.mMainCounter;
                }
                else
                {
                    num3 = mApp.mAppCounter;
                }
                float num4 = (num3 + mRow * 97 + mPlantCol * 61) * 0.03f;
                float num5 = (float)Math.Sin(num4) * 2f;
                num2 += num5;
            }
            if (flag && plant.mBodyReanimID.mActive)
            {
                Reanimation reanimation = mApp.ReanimationGet(plant.mBodyReanimID);
                Graphics @new = Graphics.GetNew(g);
                @new.mTransX += plant.mX - mX;
                @new.mTransY += plant.mY - mY;
                reanimation.DrawRenderGroup(@new, 1);
                @new.PrepareForReuse();
            }
            num += mShakeOffsetX;
            num2 += mShakeOffsetY;
            if (IsInPlay() && mApp.IsIZombieLevel())
            {
                mBoard.mChallenge.IZombieDrawPlant(g, this);
            }
            else if (mBodyReanimID != null)
            {
                Reanimation reanimation2 = mApp.ReanimationTryToGet(mBodyReanimID);
                if (reanimation2 != null)
                {
                    if (mGloveGrabbed)
                    {
                        g.SetColorizeImages(true);
                        g.SetColor(new Color(150, 255, 150, 255));
                    }
                    reanimation2.DrawRenderGroup(g, 0);
                    if (mGloveGrabbed)
                    {
                        g.SetColorizeImages(false);
                    }
                }
            }
            else
            {
                SeedType seedType = SeedType.None;
                if (mBoard != null)
                {
                    seedType = mBoard.GetSeedTypeInCursor();
                }
                if (IsPartOfUpgradableTo(seedType) && mBoard.CanPlantAt(mPlantCol, mRow, seedType) == PlantingReason.Ok)
                {
                    g.SetColorizeImages(true);
                    g.SetColor(TodCommon.GetFlashingColor(mBoard.mMainCounter, 90));
                }
                else if (seedType == SeedType.Cobcannon && mBoard.CanPlantAt(mPlantCol - 1, mRow, seedType) == PlantingReason.Ok)
                {
                    g.SetColorizeImages(true);
                    g.SetColor(TodCommon.GetFlashingColor(mBoard.mMainCounter, 90));
                }
                else if (mBoard != null && mBoard.mTutorialState == TutorialState.ShovelDig)
                {
                    g.SetColorizeImages(true);
                    g.SetColor(TodCommon.GetFlashingColor(mBoard.mMainCounter, 90));
                }
                if (image != null)
                {
                    TodCommon.TodDrawImageCelF(g, image, num, num2, theCelCol, theCelRow);
                }
                if (mSeedType == SeedType.Sprout)
                {
                    if (mGloveGrabbed)
                    {
                        g.SetColorizeImages(true);
                        g.SetColor(new Color(150, 255, 150, 255));
                    }
                    TodCommon.TodDrawImageCelF(g, AtlasResources.IMAGE_CACHED_MARIGOLD, Constants.ZenGarden_Marigold_Sprout_Offset.X, Constants.ZenGarden_Marigold_Sprout_Offset.Y, 0, 0);
                    if (mGloveGrabbed)
                    {
                        g.SetColorizeImages(false);
                    }
                }
                g.SetColorizeImages(false);
                if (mHighlighted)
                {
                    g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                    g.SetColorizeImages(true);
                    g.SetColor(new SexyColor(255, 255, 255, 196));
                    if (image != null)
                    {
                        TodCommon.TodDrawImageCelF(g, image, num, num2, theCelCol, theCelRow);
                    }
                    g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
                    g.SetColorizeImages(false);
                }
                else if (mEatenFlashCountdown > 0)
                {
                    g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                    g.SetColorizeImages(true);
                    int theAlpha = TodCommon.ClampInt(mEatenFlashCountdown * 3, 0, 255);
                    g.SetColor(new SexyColor(255, 255, 255, theAlpha));
                    if (image != null)
                    {
                        TodCommon.TodDrawImageCelF(g, image, num, num2, theCelCol, theCelRow);
                    }
                    g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
                    g.SetColorizeImages(false);
                }
            }
            if (mSeedType == SeedType.Magnetshroom && !DrawMagnetItemsOnTop())
            {
                DrawMagnetItems(g);
            }
        }

        public void MouseDown(int x, int y, int theClickCount)
        {
            if (theClickCount < 0)
            {
                return;
            }
            if (mState == PlantState.CobcannonReady && mBoard.mCursorObject.mCursorType == CursorType.Normal)
            {
                mBoard.RefreshSeedPacketFromCursor();
                mBoard.mCursorObject.mType = SeedType.None;
                mBoard.mCursorObject.mCursorType = CursorType.CobcannonTarget;
                mBoard.mCursorObject.mSeedBankIndex = -1;
                mBoard.mCursorObject.mCoinID = null;
                mBoard.mCursorObject.mCobCannonPlantID = mBoard.mPlants[mBoard.mPlants.IndexOf(this)];
                mBoard.mCobCannonCursorDelayCounter = 30;
                mBoard.mCobCannonMouseX = x;
                mBoard.mCobCannonMouseY = y;
            }
        }

        public void DoSpecial()
        {
            int num = mX + mWidth / 2;
            int num2 = mY + mHeight / 2;
            int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary);
            if (mSeedType == SeedType.Blover)
            {
                if (mState == PlantState.Doingspecial)
                {
                    return;
                }
                mState = PlantState.Doingspecial;
                BlowAwayFliers(mX, mRow);
                return;
            }
            else
            {
                if (mSeedType == SeedType.Cherrybomb)
                {
                    mApp.PlayFoley(FoleyType.Cherrybomb);
                    mApp.PlayFoley(FoleyType.Juicy);
                    int num3 = mBoard.KillAllZombiesInRadius(mRow, num, num2, 115, 1, true, damageRangeFlags);
                    if (num3 >= 10 && !mApp.IsLittleTroubleLevel())
                    {
                        mBoard.GrantAchievement(AchievementId.Explodonator, true);
                    }
                    mApp.AddTodParticle(num, num2, 400000, ParticleEffect.Powie);
                    mApp.Vibrate();
                    mBoard.ShakeBoard(3, -4);
                    Die();
                    return;
                }
                if (mSeedType == SeedType.Doomshroom)
                {
                    mApp.PlaySample(Resources.SOUND_DOOMSHROOM);
                    mBoard.KillAllZombiesInRadius(mRow, num, num2, 250, 3, true, damageRangeFlags);
                    KillAllPlantsNearDoom();
                    mApp.AddTodParticle(num, num2, 400000, ParticleEffect.Doom);
                    GridItem gridItem = mBoard.AddACrater(mPlantCol, mRow);
                    gridItem.mGridItemCounter = GameConstants.CRATER_TIME;
                    mBoard.ShakeBoard(3, -4);
                    mApp.Vibrate();
                    Die();
                    mBoard.mDoomsUsed++;
                    return;
                }
                if (mSeedType == SeedType.Jalapeno)
                {
                    mApp.PlayFoley(FoleyType.JalapenoIgnite);
                    mApp.PlayFoley(FoleyType.Juicy);
                    mBoard.DoFwoosh(mRow);
                    mBoard.ShakeBoard(3, -4);
                    mApp.Vibrate();
                    BurnRow(mRow);
                    if (mBoard.mIceTimer[mRow] > 0)
                    {
                        mBoard.mIceTimer[mRow] = 20;
                    }
                    Die();
                    return;
                }
                if (mSeedType == SeedType.Umbrella)
                {
                    if (mState == PlantState.UmbrellaTriggered || mState == PlantState.UmbrellaReflecting)
                    {
                        return;
                    }
                    mState = PlantState.UmbrellaTriggered;
                    mStateCountdown = 5;
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_block, ReanimLoopType.PlayOnceAndHold, 0, 22f);
                    return;
                }
                else
                {
                    if (mSeedType == SeedType.Iceshroom)
                    {
                        mApp.PlayFoley(FoleyType.Frozen);
                        IceZombies();
                        mApp.AddTodParticle(num, num2, 400000, ParticleEffect.IceTrap);
                        Die();
                        return;
                    }
                    if (mSeedType == SeedType.Potatomine)
                    {
                        num = mX + mWidth / 2 - 20;
                        num2 = mY + mHeight / 2;
                        mApp.PlaySample(Resources.SOUND_POTATO_MINE);
                        mBoard.KillAllZombiesInRadius(mRow, num, num2, 60, 0, false, damageRangeFlags);
                        int aRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, mRow, 0);
                        mApp.AddTodParticle(num + 20f, num2, aRenderOrder, ParticleEffect.PotatoMine);
                        mBoard.ShakeBoard(3, -4);
                        mApp.Vibrate();
                        Die();
                        if (!mApp.IsIZombieLevel())
                        {
                            mBoard.GrantAchievement(AchievementId.Spudow, true);
                        }
                        return;
                    }
                    if (mSeedType == SeedType.InstantCoffee)
                    {
                        Plant topPlantAt = mBoard.GetTopPlantAt(mPlantCol, mRow, TopPlant.OnlyNormalPosition);
                        if (topPlantAt != null && topPlantAt.mIsAsleep)
                        {
                            topPlantAt.mWakeUpCounter = GameConstants.WAKE_UP_TIME;
                        }
                        mState = PlantState.Doingspecial;
                        PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_crumble, ReanimLoopType.PlayOnceAndHold, 20, 22f);
                        mApp.PlayFoley(FoleyType.Coffee);
                    }
                    return;
                }
            }
        }

        public void Fire(Zombie theTargetZombie, int theRow, PlantWeapon thePlantWeapon)
        {
            if (mSeedType == SeedType.Fumeshroom)
            {
                DoRowAreaDamage(20, 2U);
                mApp.PlayFoley(FoleyType.Fume);
                return;
            }
            if (mSeedType == SeedType.Gloomshroom)
            {
                DoRowAreaDamage(20, 2U);
                return;
            }
            if (mSeedType == SeedType.Starfruit)
            {
                StarFruitFire();
                return;
            }
            ProjectileType projectileType = ProjectileType.ProjectilesCount;
            SeedType seedType = mSeedType;
            if (seedType <= SeedType.Threepeater)
            {
                if (seedType <= SeedType.Puffshroom)
                {
                    if (seedType == SeedType.Peashooter)
                    {
                        projectileType = ProjectileType.Pea;
                        goto IL_157;
                    }
                    switch (seedType)
                    {
                    case SeedType.Snowpea:
                        projectileType = ProjectileType.Snowpea;
                        goto IL_157;
                    case SeedType.Repeater:
                        projectileType = ProjectileType.Pea;
                        goto IL_157;
                    case SeedType.Puffshroom:
                        projectileType = ProjectileType.Puff;
                        goto IL_157;
                    }
                }
                else
                {
                    if (seedType == SeedType.Scaredyshroom)
                    {
                        projectileType = ProjectileType.Puff;
                        goto IL_157;
                    }
                    if (seedType == SeedType.Threepeater)
                    {
                        projectileType = ProjectileType.Pea;
                        goto IL_157;
                    }
                }
            }
            else if (seedType <= SeedType.Kernelpult)
            {
                switch (seedType)
                {
                case SeedType.Seashroom:
                    projectileType = ProjectileType.Puff;
                    goto IL_157;
                case SeedType.Plantern:
                case SeedType.Blover:
                    break;
                case SeedType.Cactus:
                    projectileType = ProjectileType.Spike;
                    goto IL_157;
                case SeedType.Splitpea:
                    projectileType = ProjectileType.Pea;
                    goto IL_157;
                default:
                    switch (seedType)
                    {
                    case SeedType.Cabbagepult:
                        projectileType = ProjectileType.Cabbage;
                        goto IL_157;
                    case SeedType.Kernelpult:
                        projectileType = ProjectileType.Kernel;
                        goto IL_157;
                    }
                    break;
                }
            }
            else
            {
                switch (seedType)
                {
                case SeedType.Melonpult:
                    projectileType = ProjectileType.Melon;
                    goto IL_157;
                case SeedType.Gatlingpea:
                    projectileType = ProjectileType.Pea;
                    goto IL_157;
                case SeedType.Twinsunflower:
                case SeedType.Gloomshroom:
                case SeedType.GoldMagnet:
                case SeedType.Spikerock:
                    break;
                case SeedType.Cattail:
                    projectileType = ProjectileType.Spike;
                    goto IL_157;
                case SeedType.Wintermelon:
                    projectileType = ProjectileType.Wintermelon;
                    goto IL_157;
                case SeedType.Cobcannon:
                    projectileType = ProjectileType.Cobbig;
                    goto IL_157;
                default:
                    if (seedType == SeedType.Leftpeater)
                    {
                        projectileType = ProjectileType.Pea;
                        goto IL_157;
                    }
                    break;
                }
            }
            Debug.ASSERT(false);
            IL_157:
            if (mSeedType == SeedType.Kernelpult && thePlantWeapon == PlantWeapon.Secondary)
            {
                projectileType = ProjectileType.Butter;
            }
            mApp.PlayFoley(FoleyType.Throw);
            if (mSeedType == SeedType.Snowpea || mSeedType == SeedType.Wintermelon)
            {
                mApp.PlayFoley(FoleyType.SnowPeaSparkles);
            }
            else if (mSeedType == SeedType.Puffshroom || mSeedType == SeedType.Scaredyshroom || mSeedType == SeedType.Seashroom)
            {
                mApp.PlayFoley(FoleyType.Puff);
            }
            int num;
            int num2;
            if (mSeedType == SeedType.Puffshroom)
            {
                num = mX + 40;
                num2 = mY + 40;
            }
            else if (mSeedType == SeedType.Seashroom)
            {
                num = mX + 45;
                num2 = mY + 63;
            }
            else if (mSeedType == SeedType.Cabbagepult)
            {
                num = mX + 5;
                num2 = mY - 12;
            }
            else if (mSeedType == SeedType.Melonpult || mSeedType == SeedType.Wintermelon)
            {
                num = mX + 25;
                num2 = mY - 46;
            }
            else if (mSeedType == SeedType.Cattail)
            {
                num = mX + 20;
                num2 = mY - 3;
            }
            else if (mSeedType == SeedType.Kernelpult && thePlantWeapon == PlantWeapon.Primary)
            {
                num = mX + 19;
                num2 = mY - 37;
            }
            else if (mSeedType == SeedType.Kernelpult && thePlantWeapon == PlantWeapon.Secondary)
            {
                num = mX + 12;
                num2 = mY - 56;
            }
            else if (mSeedType == SeedType.Peashooter || mSeedType == SeedType.Snowpea || mSeedType == SeedType.Repeater)
            {
                int num3 = 0;
                int num4 = 0;
                GetPeaHeadOffset(ref num3, ref num4);
                num = mX + num3 + 24;
                num2 = mY + num4 + -33;
            }
            else if (mSeedType == SeedType.Leftpeater)
            {
                int num5 = 0;
                int num6 = 0;
                GetPeaHeadOffset(ref num5, ref num6);
                num = mX - num5 + 27;
                num2 = mY + num6 - 33;
            }
            else if (mSeedType == SeedType.Gatlingpea)
            {
                int num7 = 0;
                int num8 = 0;
                GetPeaHeadOffset(ref num7, ref num8);
                num = mX + num7 + 34;
                num2 = mY + num8 + -33;
            }
            else if (mSeedType == SeedType.Splitpea)
            {
                int num9 = 0;
                int num10 = 0;
                GetPeaHeadOffset(ref num9, ref num10);
                num2 = mY + num10 + -33;
                if (thePlantWeapon == PlantWeapon.Secondary)
                {
                    num = mX + num9 - 64;
                }
                else
                {
                    num = mX + num9 + 24;
                }
            }
            else if (mSeedType == SeedType.Threepeater)
            {
                num2 = mY + 10;
                num = mX + 45;
            }
            else if (mSeedType == SeedType.Scaredyshroom)
            {
                num = mX + 29;
                num2 = mY + 21;
            }
            else if (mSeedType == SeedType.Cactus)
            {
                if (thePlantWeapon == PlantWeapon.Primary)
                {
                    num = mX + 93;
                    num2 = mY - 50;
                }
                else
                {
                    num = mX + 70;
                    num2 = mY + 23;
                }
            }
            else if (mSeedType == SeedType.Cobcannon)
            {
                num = mX + Constants.Plant_CobCannon_Projectile_Offset.X;
                num2 = mY + Constants.Plant_CobCannon_Projectile_Offset.Y;
            }
            else
            {
                num = mX + 10;
                num2 = mY + 5;
            }
            Plant flowerPotAt = mBoard.GetFlowerPotAt(mPlantCol, mRow);
            if (flowerPotAt != null)
            {
                num2 -= 5;
            }
            if (mSeedType == SeedType.Snowpea)
            {
                int aRenderOrder = Board.MakeRenderOrder(RenderLayer.LawnMower, mRow, 1);
                mApp.AddTodParticle(num + 8, num2 + 13, aRenderOrder, ParticleEffect.SnowpeaPuff);
            }
            Projectile projectile = mBoard.AddProjectile(num, num2, mRenderOrder + -1, theRow, projectileType);
            projectile.mDamageRangeFlags = GetDamageRangeFlags(thePlantWeapon);
            if (mSeedType == SeedType.Cabbagepult || mSeedType == SeedType.Kernelpult || mSeedType == SeedType.Melonpult || mSeedType == SeedType.Wintermelon)
            {
                float num12;
                float num13;
                if (theTargetZombie != null)
                {
                    TRect zombieRect = theTargetZombie.GetZombieRect();
                    float num11 = theTargetZombie.ZombieTargetLeadX(50f);
                    num12 = num11 - num - 30f;
                    num13 = zombieRect.mY - num2;
                    if (theTargetZombie.mZombiePhase == ZombiePhase.DolphinRiding)
                    {
                        num12 -= 60f;
                    }
                    if (theTargetZombie.mZombieType == ZombieType.Pogo && theTargetZombie.mHasObject)
                    {
                        num12 -= 60f;
                    }
                    if (theTargetZombie.mZombiePhase == ZombiePhase.SnorkelWalkingInPool)
                    {
                        num12 -= 40f;
                    }
                    if (theTargetZombie.mZombieType == ZombieType.Boss)
                    {
                        int num14 = mBoard.GridToPixelY(8, mRow);
                        num13 = num14 - num2;
                    }
                }
                else
                {
                    num12 = 700f - num;
                    num13 = 0f;
                }
                if (num12 < 40f)
                {
                    num12 = 40f;
                }
                projectile.mMotionType = ProjectileMotion.Lobbed;
                float num15 = 120f;
                projectile.mVelX = num12 / num15;
                projectile.mVelY = 0f;
                projectile.mVelZ = -7f + num13 / num15;
                projectile.mAccZ = 0.115f;
                return;
            }
            if (mSeedType == SeedType.Threepeater)
            {
                if (theRow < mRow)
                {
                    projectile.mMotionType = ProjectileMotion.Threepeater;
                    projectile.mVelY = -3f;
                    projectile.mShadowY += 80f;
                    return;
                }
                if (theRow > mRow)
                {
                    projectile.mMotionType = ProjectileMotion.Threepeater;
                    projectile.mVelY = 3f;
                    projectile.mShadowY += -80f;
                    return;
                }
            }
            else
            {
                if (mSeedType == SeedType.Puffshroom || mSeedType == SeedType.Seashroom)
                {
                    projectile.mMotionType = ProjectileMotion.Puff;
                    return;
                }
                if (mSeedType == SeedType.Splitpea && thePlantWeapon == PlantWeapon.Secondary)
                {
                    projectile.mMotionType = ProjectileMotion.Backwards;
                    return;
                }
                if (mSeedType == SeedType.Leftpeater)
                {
                    projectile.mMotionType = ProjectileMotion.Backwards;
                    return;
                }
                if (mSeedType == SeedType.Cattail)
                {
                    projectile.mMotionType = ProjectileMotion.Homing;
                    projectile.mVelX = 2f;
                    projectile.mTargetZombieID = mBoard.ZombieGetID(theTargetZombie);
                    return;
                }
                if (mSeedType == SeedType.Cobcannon)
                {
                    projectile.mDamageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary);
                    projectile.mMotionType = ProjectileMotion.Lobbed;
                    projectile.mVelX = 0.001f;
                    projectile.mVelY = 0f;
                    projectile.mAccZ = 0f;
                    projectile.mVelZ = -8f;
                    projectile.mCobTargetX = mTargetX - 40;
                    projectile.mCobTargetRow = mBoard.PixelToGridYKeepOnBoard(mTargetX, mTargetY);
                }
            }
        }

        public Zombie FindTargetZombie(int theRow, PlantWeapon thePlantWeapon)
        {
            int damageRangeFlags = GetDamageRangeFlags(thePlantWeapon);
            TRect plantAttackRect = GetPlantAttackRect(thePlantWeapon);
            int distanceMax = 0;
            Zombie result = null;
            for (int i = 0; i < mBoard.mZombies.Count; i++)
            {
                Zombie theZombieItem = mBoard.mZombies[i];
                if (!theZombieItem.mDead)
                {
                    int rowDeviation = theZombieItem.mRow - theRow;
                    if (theZombieItem.mZombieType == ZombieType.Boss)
                    {
                        rowDeviation = 0;
                    }
                    if ((theZombieItem.mHasHead && !theZombieItem.IsTangleKelpTarget()) || (mSeedType != SeedType.Potatomine && mSeedType != SeedType.Chomper && mSeedType != SeedType.Tanglekelp))
                    {
                        bool isPortalCheckNeeded = false;
                        if (mApp.mGameMode == GameMode.ChallengePortalCombat && (mSeedType == SeedType.Peashooter || mSeedType == SeedType.Cactus || mSeedType == SeedType.Repeater))
                        {
                            isPortalCheckNeeded = true;
                        }
                        if (mSeedType != SeedType.Cattail)
                        {
                            if (mSeedType == SeedType.Gloomshroom)
                            {
                                if (rowDeviation < -1)
                                {
                                    continue;
                                }
                                if (rowDeviation > 1)
                                {
                                    continue;
                                }
                            }
                            else if (isPortalCheckNeeded)
                            {
                                if (!mBoard.mChallenge.CanTargetZombieWithPortals(this, theZombieItem))
                                {
                                    continue;
                                }
                            }
                            else if (rowDeviation != 0)
                            {
                                continue;
                            }
                        }
                        if (theZombieItem.EffectedByDamage((uint)damageRangeFlags))
                        {
                            int num3 = 0;
                            if (mSeedType == SeedType.Cattail)
                            {
                                num3 = Constants.Board_Offset_AspectRatio_Correction;
                            }
                            if (mSeedType == SeedType.Chomper)
                            {
                                if (theZombieItem.mZombiePhase == ZombiePhase.DiggerWalking)
                                {
                                    plantAttackRect.mX += 20;
                                    plantAttackRect.mWidth -= 20;
                                }
                                if (theZombieItem.mZombiePhase == ZombiePhase.PogoBouncing || (theZombieItem.mZombieType == ZombieType.Bungee && theZombieItem.mTargetCol == mPlantCol))
                                {
                                    continue;
                                }
                                if (theZombieItem.mIsEating || mState == PlantState.ChomperBiting)
                                {
                                    num3 = 60;
                                }
                            }
                            if (mSeedType == SeedType.Potatomine)
                            {
                                if ((theZombieItem.mZombieType == ZombieType.Pogo && theZombieItem.mHasObject) || theZombieItem.mZombiePhase == ZombiePhase.PolevaulterInVault || theZombieItem.mZombiePhase == ZombiePhase.PolevaulterPreVault)
                                {
                                    continue;
                                }
                                if (theZombieItem.mZombieType == ZombieType.Polevaulter)
                                {
                                    plantAttackRect.mX += 40;
                                    plantAttackRect.mWidth -= 40; //原版造成土豆雷不爆炸Bug的机制
                                }
                                if (theZombieItem.mZombieType == ZombieType.Bungee && theZombieItem.mTargetCol != mPlantCol)
                                {
                                    continue;
                                }
                                if (theZombieItem.mIsEating)
                                {
                                    num3 = 30;
                                }
                            }
                            if ((mSeedType != SeedType.ExplodeONut || theZombieItem.mZombiePhase != ZombiePhase.PolevaulterInVault) && (mSeedType != SeedType.Tanglekelp || theZombieItem.mInPool))
                            {
                                TRect zombieRect = theZombieItem.GetZombieRect();
                                if (!isPortalCheckNeeded)
                                {
                                    int theXOverlap = GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
                                    if (theXOverlap < -num3)
                                    {
                                        continue;
                                    }
                                }
                                int distance = -zombieRect.mX;
                                if (mSeedType == SeedType.Cattail)
                                {
                                    distance = -(int)TodCommon.Distance2D(mX + 40f, mY + 40f, zombieRect.mX + zombieRect.mWidth / 2, zombieRect.mY + zombieRect.mHeight / 2);
                                    if (theZombieItem.IsFlying())
                                    {
                                        distance += 10000;	// prority for balloon
                                    }
                                }
                                if (result == null || distance > distanceMax)
                                {
                                    distanceMax = distance;
                                    result = theZombieItem;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public void Die()
        {
            if (IsOnBoard() && mSeedType == SeedType.Tanglekelp)
            {
                Zombie zombie = mBoard.ZombieTryToGet(mTargetZombieID);
                if (zombie != null)
                {
                    zombie.DieWithLoot();
                }
            }
            mDead = true;
            RemoveEffects();
            if (!Plant.IsFlying(mSeedType) && IsOnBoard())
            {
                GridItem ladderAt = mBoard.GetLadderAt(mPlantCol, mRow);
                if (ladderAt != null)
                {
                    ladderAt.GridItemDie();
                }
            }
            if (IsOnBoard())
            {
                Plant topPlantAt = mBoard.GetTopPlantAt(mPlantCol, mRow, TopPlant.BungeeOrder);
                Plant flowerPotAt = mBoard.GetFlowerPotAt(mPlantCol, mRow);
                if (flowerPotAt != null && topPlantAt == flowerPotAt)
                {
                    Reanimation reanimation = mApp.ReanimationGet(flowerPotAt.mBodyReanimID);
                    reanimation.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
                }
            }
        }

        public void UpdateProductionPlant()//3update
        {
            if (!IsInPlay())
            {
                return;
            }
            if (mApp.IsIZombieLevel() || mApp.mGameMode == GameMode.Upsell || mApp.mGameMode == GameMode.Intro)
            {
                return;
            }
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            if (mSeedType == SeedType.Marigold && mBoard.mCurrentWave == mBoard.mNumWaves)
            {
                if (mState != PlantState.MarigoldEnding)
                {
                    mState = PlantState.MarigoldEnding;
                    mStateCountdown = 6000;
                }
                else if (mStateCountdown <= 0)
                {
                    return;
                }
            }
            if (mApp.mGameMode == GameMode.ChallengeLastStand && mBoard.mChallenge.mChallengeState != ChallengeState.LastStandOnslaught)
            {
                return;
            }
            //mLaunchCounter -= 3;
            mLaunchCounter--;
            if (mLaunchCounter <= 100)
            {
                int num = TodCommon.TodAnimateCurve(100, 0, mLaunchCounter, 0, 100, TodCurves.Linear);
                mEatenFlashCountdown = Math.Max(mEatenFlashCountdown, num);
            }
            if (mLaunchCounter <= 0)
            {
                mLaunchCounter = TodCommon.RandRangeInt(mLaunchRate - 150, mLaunchRate);
                mApp.PlayFoley(FoleyType.SpawnSun);
                if (mSeedType == SeedType.Sunshroom)
                {
                    if (mState == PlantState.SunshroomSmall)
                    {
                        mBoard.AddCoin(mX, mY, CoinType.Smallsun, CoinMotion.FromPlant);
                    }
                    else
                    {
                        mBoard.AddCoin(mX, mY, CoinType.Sun, CoinMotion.FromPlant);
                    }
                }
                else if (mSeedType == SeedType.Sunflower)
                {
                    mBoard.AddCoin(mX, mY, CoinType.Sun, CoinMotion.FromPlant);
                }
                else if (mSeedType == SeedType.Twinsunflower)
                {
                    mBoard.AddCoin(mX, mY, CoinType.Sun, CoinMotion.FromPlant);
                    mBoard.AddCoin(mX, mY, CoinType.Sun, CoinMotion.FromPlant);
                }
                else if (mSeedType == SeedType.Marigold)
                {
                    int num2 = RandomNumbers.NextNumber(100);
                    CoinType theCoinType = CoinType.Silver;
                    if (num2 < 10)
                    {
                        theCoinType = CoinType.Gold;
                    }
                    mBoard.AddCoin(mX, mY, theCoinType, CoinMotion.Coin);
                }
                if (mApp.mGameMode == GameMode.ChallengeBigTime)
                {
                    if (mSeedType == SeedType.Sunflower)
                    {
                        mBoard.AddCoin(mX, mY, CoinType.Sun, CoinMotion.FromPlant);
                        return;
                    }
                    if (mSeedType == SeedType.Marigold)
                    {
                        mBoard.AddCoin(mX, mY, CoinType.Silver, CoinMotion.Coin);
                    }
                }
            }
        }

        public void UpdateShooter()//1update
        {
            mLaunchCounter--;
            if (mLaunchCounter <= 0)
            {
                mLaunchCounter = mLaunchRate - RandomNumbers.NextNumber(15);
                if (mSeedType == SeedType.Threepeater)
                {
                    LaunchThreepeater();
                }
                else if (mSeedType == SeedType.Starfruit)
                {
                    LaunchStarFruit();
                }
                else if (mSeedType == SeedType.Splitpea)
                {
                    FindTargetAndFire(mRow, PlantWeapon.Secondary);
                    Reanimation reanimation = mApp.ReanimationGet(mHeadReanimID);
                    Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
                    reanimation.StartBlend(20);
                    reanimation.mLoopType = ReanimLoopType.Loop;
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
                    reanimation.mAnimRate = reanimation2.mAnimRate;
                    reanimation.mAnimTime = reanimation2.mAnimTime;
                }
                else if (mSeedType == SeedType.Cactus)
                {
                    if (mState == PlantState.CactusHigh)
                    {
                        FindTargetAndFire(mRow, PlantWeapon.Primary);
                    }
                    else if (mState == PlantState.CactusLow)
                    {
                        FindTargetAndFire(mRow, PlantWeapon.Secondary);
                    }
                }
                else
                {
                    FindTargetAndFire(mRow, PlantWeapon.Primary);
                }
            }
            if (mLaunchCounter == 50 && mSeedType == SeedType.Cattail)
            {
                FindTargetAndFire(mRow, PlantWeapon.Primary);
            }
            if (mLaunchCounter == 25)
            {
                if (mSeedType == SeedType.Repeater || mSeedType == SeedType.Leftpeater)
                {
                    FindTargetAndFire(mRow, PlantWeapon.Primary);
                    return;
                }
                if (mSeedType == SeedType.Splitpea)
                {
                    FindTargetAndFire(mRow, PlantWeapon.Primary);
                    FindTargetAndFire(mRow, PlantWeapon.Secondary);
                }
            }
        }

        public bool FindTargetAndFire(int theRow, PlantWeapon thePlantWeapon)
        {
            Zombie zombie = FindTargetZombie(theRow, thePlantWeapon);
            if (zombie == null)
            {
                return false;
            }
            EndBlink();
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            Reanimation reanimation2 = mApp.ReanimationTryToGet(mHeadReanimID);
            if (mSeedType == SeedType.Splitpea && thePlantWeapon == PlantWeapon.Secondary)
            {
                Reanimation reanimation3 = mApp.ReanimationGet(mHeadReanimID2);
                reanimation3.StartBlend(20);
                reanimation3.mLoopType = ReanimLoopType.PlayOnceAndHold;
                reanimation3.mAnimRate = 35f;
                reanimation3.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_shooting);
                mShootingCounter = 26;
            }
            else if (reanimation2 != null && reanimation2.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
            {
                reanimation2.StartBlend(20);
                reanimation2.mLoopType = ReanimLoopType.PlayOnceAndHold;
                reanimation2.mAnimRate = 35f;
                reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting);
                mShootingCounter = 33;
                if (mSeedType == SeedType.Repeater || mSeedType == SeedType.Splitpea || mSeedType == SeedType.Leftpeater)
                {
                    reanimation2.mAnimRate = 45f;
                    mShootingCounter = 26;
                }
                else if (mSeedType == SeedType.Gatlingpea)
                {
                    reanimation2.mAnimRate = 38f;
                    mShootingCounter = 100;
                }
            }
            else if (mState == PlantState.CactusHigh)
            {
                PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shootinghigh, ReanimLoopType.PlayOnceAndHold, 20, 35f);
                mShootingCounter = 23;
            }
            else if (mSeedType == SeedType.Gloomshroom)
            {
                PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.PlayOnceAndHold, 20, 14f);
                mShootingCounter = 200;
            }
            else if (mSeedType == SeedType.Cattail)
            {
                PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.PlayOnceAndHold, 10, 30f);
                mShootingCounter = 50;
            }
            else if (reanimation != null && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
            {
                PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.PlayOnceAndHold, 20, 35f);
                if (mSeedType == SeedType.Fumeshroom)
                {
                    mShootingCounter = 50;
                }
                else if (mSeedType == SeedType.Puffshroom)
                {
                    mShootingCounter = 29;
                }
                else if (mSeedType == SeedType.Scaredyshroom)
                {
                    mShootingCounter = 25;
                }
                else if (mSeedType == SeedType.Cabbagepult)
                {
                    mShootingCounter = 32;
                }
                else if (mSeedType == SeedType.Melonpult || mSeedType == SeedType.Wintermelon)
                {
                    mShootingCounter = 36;
                }
                else if (mSeedType == SeedType.Kernelpult)
                {
                    if (RandomNumbers.NextNumber(4) == 0)
                    {
                        reanimation = mApp.ReanimationGet(mBodyReanimID);
                        reanimation.AssignRenderGroupToPrefix("Cornpult_butter", 0);
                        reanimation.AssignRenderGroupToPrefix("Cornpult_kernal", -1);
                        mState = PlantState.KernelpultButter;
                    }
                    mShootingCounter = 30;
                }
                else if (mSeedType == SeedType.Cactus)
                {
                    mShootingCounter = 35;
                }
                else
                {
                    mShootingCounter = 29;
                }
            }
            else
            {
                Fire(zombie, theRow, thePlantWeapon);
            }
            return true;
        }

        public void LaunchThreepeater()
        {
            int theRow = mRow - 1;
            int theRow2 = mRow + 1;
            bool flag = false;
            if (FindTargetZombie(mRow, PlantWeapon.Primary) != null)
            {
                flag = true;
            }
            else if (mBoard.RowCanHaveZombies(theRow) && FindTargetZombie(theRow, PlantWeapon.Primary) != null)
            {
                flag = true;
            }
            else if (mBoard.RowCanHaveZombies(theRow2) && FindTargetZombie(theRow2, PlantWeapon.Primary) != null)
            {
                flag = true;
            }
            if (!flag)
            {
                return;
            }
            Reanimation reanimation = mApp.ReanimationGet(mHeadReanimID);
            Reanimation reanimation2 = mApp.ReanimationGet(mHeadReanimID2);
            Reanimation reanimation3 = mApp.ReanimationGet(mHeadReanimID3);
            if (mBoard.RowCanHaveZombies(theRow2))
            {
                reanimation.StartBlend(10);
                reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
                reanimation.mAnimRate = 20f;
                reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting1);
            }
            reanimation2.StartBlend(10);
            reanimation2.mLoopType = ReanimLoopType.PlayOnceAndHold;
            reanimation2.mAnimRate = 20f;
            reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting2);
            if (mBoard.RowCanHaveZombies(theRow))
            {
                reanimation3.StartBlend(10);
                reanimation3.mLoopType = ReanimLoopType.PlayOnceAndHold;
                reanimation3.mAnimRate = 20f;
                reanimation3.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting3);
            }
            mShootingCounter = 35;
        }

        public static Image GetImage(SeedType theSeedtype)
        {
            PlantDefinition plantDefinition = Plant.GetPlantDefinition(theSeedtype);
            if (plantDefinition.mPlantImage == null)
            {
                return null;
            }
            return plantDefinition.mPlantImage[0];
        }

        public static int GetCost(SeedType theSeedType, SeedType theImitaterType)
        {
            if (GlobalStaticVars.gLawnApp.mGameMode == GameMode.ChallengeBeghouled || GlobalStaticVars.gLawnApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                if (theSeedType == SeedType.Repeater)
                {
                    return 1000;
                }
                if (theSeedType == SeedType.Fumeshroom)
                {
                    return 500;
                }
                if (theSeedType == SeedType.Tallnut)
                {
                    return 250;
                }
                if (theSeedType == SeedType.BeghouledButtonShuffle)
                {
                    return 100;
                }
                if (theSeedType == SeedType.BeghouledButtonCrater)
                {
                    return 200;
                }
            }
            switch (theSeedType)
            {
            case SeedType.SlotMachineSun:
                return 0;
            case SeedType.SlotMachineDiamond:
                return 0;
            case SeedType.ZombiquariumSnorkel:
                return 100;
            case SeedType.ZombiquariumTrophy:
                return 1000;
            case SeedType.ZombieNormal:
                return 50;
            case SeedType.ZombieTrafficCone:
                return 75;
            case SeedType.ZombiePolevaulter:
                return 75;
            case SeedType.ZombiePail:
                return 125;
            case SeedType.ZombieLadder:
                return 150;
            case SeedType.ZombieDigger:
                return 125;
            case SeedType.ZombieBungee:
                return 125;
            case SeedType.ZombieFootball:
                return 175;
            case SeedType.ZombieBalloon:
                return 150;
            case SeedType.ZombieScreenDoor:
                return 100;
            case SeedType.Zomboni:
                return 175;
            case SeedType.ZombiePogo:
                return 200;
            case SeedType.ZombieDancer:
                return 350;
            case SeedType.ZombieGargantuar:
                return 300;
            case SeedType.ZombieImp:
                return 50;
            default:
            {
                if (theSeedType == SeedType.Imitater && theImitaterType != SeedType.None)
                {
                    PlantDefinition plantDefinition = Plant.GetPlantDefinition(theImitaterType);
                    return plantDefinition.mSeedCost;
                }
                PlantDefinition plantDefinition2 = Plant.GetPlantDefinition(theSeedType);
                return plantDefinition2.mSeedCost;
            }
            }
        }

        public static string GetNameString(SeedType theSeedtype, SeedType theImitaterType)
        {
            PlantDefinition plantDefinition = Plant.GetPlantDefinition(theSeedtype);
            string theString = Common.StrFormat_("[{0}]", plantDefinition.mPlantName);
            string result = TodStringFile.TodStringTranslate(theString);
            if (theSeedtype == SeedType.Imitater && theImitaterType != SeedType.None)
            {
                PlantDefinition plantDefinition2 = Plant.GetPlantDefinition(theImitaterType);
                string theString2 = Common.StrFormat_("[{0}]", plantDefinition2.mPlantName);
                string theStringToSubstitute = TodStringFile.TodStringTranslate(theString2);
                return TodCommon.TodReplaceString(TodStringFile.TodStringTranslate("[IMITATED_PLANT]"), "{PLANT}", theStringToSubstitute);
            }
            return result;
        }

        public static string GetToolTip(SeedType theSeedType)
        {
            PlantDefinition plantDefinition = Plant.GetPlantDefinition(theSeedType);
            string theString = "[" + plantDefinition.mPlantName + "_TOOLTIP]";
            return TodStringFile.TodStringTranslate(theString);
        }

        public static int GetRefreshTime(SeedType theSeedType, SeedType theImitaterType)
        {
            if (Challenge.IsZombieSeedType(theSeedType))
            {
                return 0;
            }
            if (theSeedType == SeedType.Imitater && theImitaterType != SeedType.None)
            {
                PlantDefinition plantDefinition = Plant.GetPlantDefinition(theImitaterType);
                return plantDefinition.mRefreshTime;
            }
            PlantDefinition plantDefinition2 = Plant.GetPlantDefinition(theSeedType);
            return plantDefinition2.mRefreshTime;
        }

        public static bool IsNocturnal(SeedType theSeedtype)
        {
            return theSeedtype == SeedType.Puffshroom || theSeedtype == SeedType.Seashroom || theSeedtype == SeedType.Sunshroom || theSeedtype == SeedType.Fumeshroom || theSeedtype == SeedType.Hypnoshroom || theSeedtype == SeedType.Doomshroom || theSeedtype == SeedType.Iceshroom || theSeedtype == SeedType.Magnetshroom || theSeedtype == SeedType.Scaredyshroom || theSeedtype == SeedType.Gloomshroom;
        }

        public static bool IsAquatic(SeedType theSeedType)
        {
            return theSeedType == SeedType.Lilypad || theSeedType == SeedType.Tanglekelp || theSeedType == SeedType.Seashroom || theSeedType == SeedType.Cattail;
        }

        public static bool IsFlying(SeedType theSeedtype)
        {
            return theSeedtype == SeedType.InstantCoffee;
        }

        public static bool IsUpgrade(SeedType theSeedtype)
        {
            return theSeedtype == SeedType.Gatlingpea || theSeedtype == SeedType.Wintermelon || theSeedtype == SeedType.Twinsunflower || theSeedtype == SeedType.Spikerock || theSeedtype == SeedType.Cobcannon || theSeedtype == SeedType.GoldMagnet || theSeedtype == SeedType.Gloomshroom || theSeedtype == SeedType.Cattail;
        }

        public void UpdateAbilities()//3update
        {
            if (!IsInPlay())
            {
                return;
            }
            if (mState == PlantState.Doingspecial || mSquished)
            {
                //mDisappearCountdown -= 3;
                mDisappearCountdown--;
                if (mDisappearCountdown < 0)
                {
                    Die();
                    return;
                }
            }
            if (mWakeUpCounter > 0)
            {
                //mWakeUpCounter -= 3;
                mWakeUpCounter--;
                //if (mWakeUpCounter >= 60 && mWakeUpCounter < 63)
                if (mWakeUpCounter == 60)
                {
                    mApp.PlayFoley(FoleyType.Wakeup);
                }
                //if (mWakeUpCounter >= 0 && mWakeUpCounter < 3)
                if (mWakeUpCounter == 0) 
                {
                    SetSleeping(false);
                }
            }
            if (mIsAsleep || mSquished || mOnBungeeState != PlantOnBungeeState.NotOnBungee)
            {
                return;
            }
            UpdateShooting();
            if (mStateCountdown > 0)
            {
                //mStateCountdown -= 3;
                mStateCountdown--;
            }
            if (mApp.IsWallnutBowlingLevel())
            {
                //UpdateBowling();
                //UpdateBowling();
                UpdateBowling();
                return;
            }
            if (mSeedType == SeedType.Squash)
            {
                UpdateSquash();
            }
            else if (mSeedType == SeedType.Doomshroom)
            {
                UpdateDoomShroom();
            }
            else if (mSeedType == SeedType.Iceshroom)
            {
                UpdateIceShroom();
            }
            else if (mSeedType == SeedType.Chomper)
            {
                UpdateChomper();
            }
            else if (mSeedType == SeedType.Blover)
            {
                UpdateBlover();
            }
            else if (mSeedType == SeedType.Flowerpot)
            {
                UpdateFlowerPot();
            }
            else if (mSeedType == SeedType.Lilypad)
            {
                UpdateLilypad();
            }
            else if (mSeedType == SeedType.Imitater)
            {
                UpdateImitater();
            }
            else if (mSeedType == SeedType.InstantCoffee)
            {
                UpdateCoffeeBean();
            }
            else if (mSeedType == SeedType.Umbrella)
            {
                UpdateUmbrella();
            }
            else if (mSeedType == SeedType.Cobcannon)
            {
                UpdateCobCannon();
            }
            else if (mSeedType == SeedType.Cactus)
            {
                UpdateCactus();
            }
            else if (mSeedType == SeedType.Magnetshroom)
            {
                UpdateMagnetShroom();
            }
            else if (mSeedType == SeedType.GoldMagnet)
            {
                UpdateGoldMagnetShroom();
            }
            else if (mSeedType == SeedType.Sunshroom)
            {
                UpdateSunShroom();
            }
            else if (MakesSun() || mSeedType == SeedType.Marigold)
            {
                UpdateProductionPlant();
            }
            else if (mSeedType == SeedType.Gravebuster)
            {
                UpdateGraveBuster();
            }
            else if (mSeedType == SeedType.Torchwood)
            {
                UpdateTorchwood();
            }
            else if (mSeedType == SeedType.Potatomine)
            {
                UpdatePotato();
            }
            else if (mSeedType == SeedType.Spikeweed || mSeedType == SeedType.Spikerock)
            {
                UpdateSpikeweed();
            }
            else if (mSeedType == SeedType.Tanglekelp)
            {
                UpdateTanglekelp();
            }
            else if (mSeedType == SeedType.Scaredyshroom)
            {
                UpdateScaredyShroom();
            }
            if (mSubclass == 1)
            {
                //UpdateShooter();
                //UpdateShooter();
                UpdateShooter();
            }
            if (mDoSpecialCountdown > 0)
            {
                //mDoSpecialCountdown -= 3;
                mDoSpecialCountdown--;
                if (mDoSpecialCountdown <= 0)
                {
                    DoSpecial();
                }
            }
        }

        public void Squish()
        {
            if (NotOnGround())
            {
                return;
            }
            if (!mIsAsleep)
            {
                if (mSeedType == SeedType.Cherrybomb || mSeedType == SeedType.Jalapeno || mSeedType == SeedType.Doomshroom || mSeedType == SeedType.Iceshroom)
                {
                    DoSpecial();
                    return;
                }
                if (mSeedType == SeedType.Potatomine && mState != PlantState.Notready)
                {
                    DoSpecial();
                    return;
                }
            }
            if (mSeedType == SeedType.Squash && mState != PlantState.Notready)
            {
                return;
            }
            mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, mRow, 8);
            if (mSeedType == SeedType.Flowerpot)
            {
                mRenderOrder--;
            }
            mSquished = true;
            mDisappearCountdown = 500;
            mApp.PlayFoley(FoleyType.Squish);
            RemoveEffects();
            GridItem ladderAt = mBoard.GetLadderAt(mPlantCol, mRow);
            if (ladderAt != null)
            {
                ladderAt.GridItemDie();
            }
            if (mApp.IsIZombieLevel())
            {
                mBoard.mChallenge.IZombiePlantDropRemainingSun(this);
            }
        }

        public void DoRowAreaDamage(int theDamage, uint theDamageFlags)
        {
            int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary);
            TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.Primary);
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead)
                {
                    int num = zombie.mRow - mRow;
                    if (zombie.mZombieType == ZombieType.Boss)
                    {
                        num = 0;
                    }
                    if (mSeedType == SeedType.Gloomshroom)
                    {
                        if (num < -1)
                        {
                            goto IL_108;
                        }
                        if (num > 1)
                        {
                            goto IL_108;
                        }
                    }
                    else if (num != 0)
                    {
                        goto IL_108;
                    }
                    if (zombie.mOnHighGround == IsOnHighGround() && zombie.EffectedByDamage((uint)damageRangeFlags))
                    {
                        TRect zombieRect = zombie.GetZombieRect();
                        int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
                        if (rectOverlap > 0)
                        {
                            int theDamage2 = theDamage;
                            if ((zombie.mZombieType == ZombieType.Zamboni || zombie.mZombieType == ZombieType.Catapult) && TodCommon.TestBit(theDamageFlags, 5))
                            {
                                theDamage2 = 1800;
                                if (mSeedType == SeedType.Spikerock)
                                {
                                    SpikeRockTakeDamage();
                                }
                                else
                                {
                                    Die();
                                }
                            }
                            zombie.TakeDamage(theDamage2, theDamageFlags, false);
                            mApp.PlayFoley(FoleyType.Splat);
                        }
                    }
                }
                IL_108:;
            }
        }

        public int GetDamageRangeFlags(PlantWeapon thePlantWeapon)
        {
            if (mSeedType == SeedType.Cactus)
            {
                if (thePlantWeapon == PlantWeapon.Secondary)
                {
                    return 1;
                }
                return 2;
            }
            else
            {
                if (mSeedType == SeedType.Cherrybomb || mSeedType == SeedType.Jalapeno || mSeedType == SeedType.Cobcannon || mSeedType == SeedType.Doomshroom)
                {
                    return 127;
                }
                if (mSeedType == SeedType.Melonpult || mSeedType == SeedType.Cabbagepult || mSeedType == SeedType.Kernelpult || mSeedType == SeedType.Wintermelon)
                {
                    return 13;
                }
                if (mSeedType == SeedType.Potatomine)
                {
                    return 77;
                }
                if (mSeedType == SeedType.Squash)
                {
                    return 13;
                }
                if (mSeedType == SeedType.Puffshroom || mSeedType == SeedType.Seashroom || mSeedType == SeedType.Fumeshroom || mSeedType == SeedType.Gloomshroom || mSeedType == SeedType.Chomper)
                {
                    return 9;
                }
                if (mSeedType == SeedType.Cattail)
                {
                    return 11;
                }
                if (mSeedType == SeedType.Tanglekelp)
                {
                    return 5;
                }
                if (mSeedType == SeedType.GiantWallnut)
                {
                    return 17;
                }
                return 1;
            }
        }

        public TRect GetPlantRect()
        {
            TRect result = default(TRect);
            if (mSeedType == SeedType.Tallnut)
            {
                result = new TRect(mX + 10, mY, mWidth, mHeight);
            }
            else if (mSeedType == SeedType.Pumpkinshell)
            {
                result = new TRect(mX, mY, mWidth - 20, mHeight);
            }
            else if (mSeedType == SeedType.Cobcannon)
            {
                result = new TRect(mX, mY, 140, 80);
            }
            else
            {
                result = new TRect(mX + 10, mY, mWidth - 20, mHeight);
            }
            return result;
        }

        public TRect GetPlantAttackRect(PlantWeapon thePlantWeapon)
        {
            TRect result = default(TRect);
            if (mApp.IsWallnutBowlingLevel())
            {
                result = new TRect(mX, mY, mWidth - 20, mHeight);
            }
            else if (thePlantWeapon == PlantWeapon.Secondary && mSeedType == SeedType.Splitpea)
            {
                result = new TRect(0, mY, mX + 16, mHeight);
            }
            else
            {
                SeedType seedType = mSeedType;
                if (seedType <= SeedType.Seashroom)
                {
                    switch (seedType)
                    {
                    case SeedType.Potatomine:
                        result = new TRect(mX, mY, mWidth - 25, mHeight);
                        return result;
                    case SeedType.Snowpea:
                    case SeedType.Repeater:
                    case SeedType.Sunshroom:
                        goto IL_27E;
                    case SeedType.Chomper:
                        result = new TRect(mX + 80, mY, 40, mHeight);
                        return result;
                    case SeedType.Puffshroom:
                        break;
                    case SeedType.Fumeshroom:
                        result = new TRect(mX + 60, mY, 340, mHeight);
                        return result;
                    default:
                        switch (seedType)
                        {
                        case SeedType.Squash:
                            result = new TRect(mX + 20, mY, mWidth - 35, mHeight);
                            return result;
                        case SeedType.Threepeater:
                        case SeedType.Jalapeno:
                        case SeedType.Tallnut:
                            goto IL_27E;
                        case SeedType.Tanglekelp:
                            result = new TRect(mX, mY, mWidth, mHeight);
                            return result;
                        case SeedType.Spikeweed:
                            goto IL_15B;
                        case SeedType.Torchwood:
                            result = new TRect(mX + 50, mY, 30, mHeight);
                            return result;
                        case SeedType.Seashroom:
                            break;
                        default:
                            goto IL_27E;
                        }
                        break;
                    }
                    result = new TRect(mX + 60, mY, 230, mHeight);
                    return result;
                }
                switch (seedType)
                {
                case SeedType.Gloomshroom:
                    result = new TRect(mX - 80, mY - 80, 240, 240);
                    return result;
                case SeedType.Cattail:
                    result = new TRect(-800, -600, 1600, 1200);
                    return result;
                case SeedType.Wintermelon:
                case SeedType.GoldMagnet:
                    goto IL_27E;
                case SeedType.Spikerock:
                    break;
                default:
                    if (seedType == SeedType.Leftpeater)
                    {
                        result = new TRect(0, mY, mX, mHeight);
                        return result;
                    }
                    goto IL_27E;
                }
                IL_15B:
                result = new TRect(mX + 20, mY, mWidth - 50, mHeight);
                return result;
                IL_27E:
                result = new TRect(mX + 60, mY, 800, mHeight);
            }
            return result;
        }

        public Zombie FindSquashTarget()
        {
            int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary);
            TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.Primary);
            int num = 0;
            Zombie zombie = null;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie2 = mBoard.mZombies[i];
                if (!zombie2.mDead)
                {
                    int num2 = zombie2.mRow - mRow;
                    if (zombie2.mZombieType == ZombieType.Boss)
                    {
                        num2 = 0;
                    }
                    if (num2 == 0 && zombie2.mHasHead && !zombie2.IsTangleKelpTarget() && zombie2.EffectedByDamage((uint)damageRangeFlags) && !zombie2.IsSquashTarget(this))
                    {
                        TRect zombieRect = zombie2.GetZombieRect();
                        if ((zombie2.mZombiePhase == ZombiePhase.PolevaulterPreVault && zombieRect.mX < mX + 20) || (zombie2.mZombiePhase != ZombiePhase.PolevaulterPreVault && zombie2.mZombiePhase != ZombiePhase.PolevaulterInVault && zombie2.mZombiePhase != ZombiePhase.SnorkelIntoPool && zombie2.mZombiePhase != ZombiePhase.DolphinIntoPool && zombie2.mZombiePhase != ZombiePhase.DolphinRiding && zombie2.mZombiePhase != ZombiePhase.DolphinInJump && !zombie2.IsBobsledTeamWithSled()))
                        {
                            int num3 = 70;
                            if (zombie2.mIsEating)
                            {
                                num3 = 110;
                            }
                            int num4 = -GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
                            if (num4 <= num3)
                            {
                                int num5 = plantAttackRect.mX;
                                if (zombie2.mZombiePhase == ZombiePhase.PolevaulterPostVault || zombie2.mZombiePhase == ZombiePhase.PolevaulterPreVault || zombie2.mZombiePhase == ZombiePhase.DolphinWalkingInPool || zombie2.mZombieType == ZombieType.Imp || zombie2.mZombieType == ZombieType.Football || mApp.IsScaryPotterLevel())
                                {
                                    num5 = plantAttackRect.mX - 60;
                                }
                                if (zombie2.IsWalkingBackwards() || zombieRect.mX + zombieRect.mWidth >= num5)
                                {
                                    if (mBoard.ZombieGetID(zombie2) == mTargetZombieID)
                                    {
                                        return zombie2;
                                    }
                                    if (zombie == null || num4 < num)
                                    {
                                        zombie = zombie2;
                                        num = num4;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return zombie;
        }

        public void UpdateSquash()//3update
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            Debug.ASSERT(reanimation != null);
            if (mState == PlantState.Notready)
            {
                Zombie zombie = FindSquashTarget();
                if (zombie == null)
                {
                    return;
                }
                mTargetZombieID = mBoard.ZombieGetID(zombie);
                mTargetX = (int)zombie.ZombieTargetLeadX(0f) - mWidth / 2;
                mState = PlantState.SquashLook;
                mStateCountdown = 80;
                if (mTargetX < mX)
                {
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lookleft, ReanimLoopType.PlayOnceAndHold, 10, 24f);
                }
                else
                {
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lookright, ReanimLoopType.PlayOnceAndHold, 10, 24f);
                }
                mApp.PlayFoley(FoleyType.SquashHmm);
                return;
            }
            else
            {
                if (mState == PlantState.SquashLook)
                {
                    if (mStateCountdown <= 0)
                    {
                        PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpup, ReanimLoopType.PlayOnceAndHold, 20, 24f);
                        mState = PlantState.SquashPreLaunch;
                        mStateCountdown = 30;
                    }
                    return;
                }
                if (mState == PlantState.SquashPreLaunch)
                {
                    if (mStateCountdown <= 0)
                    {
                        Zombie zombie2 = FindSquashTarget();
                        if (zombie2 != null)
                        {
                            mTargetX = (int)zombie2.ZombieTargetLeadX(30f) - mWidth / 2;
                        }
                        mState = PlantState.SquashRising;
                        mStateCountdown = 50;
                        mRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, mRow, 0);
                    }
                    return;
                }
                int theGridX = mBoard.PixelToGridXKeepOnBoard(mTargetX, mY);
                int thePositionEnd = mTargetX;
                int num = mBoard.GridToPixelY(theGridX, mRow) + 8;
                if (mState == PlantState.SquashRising)
                {
                    int thePositionStart = mBoard.GridToPixelX(mPlantCol, mStartRow);
                    int thePositionStart2 = mBoard.GridToPixelY(mPlantCol, mStartRow);
                    mX = TodCommon.TodAnimateCurve(50, 20, mStateCountdown, thePositionStart, thePositionEnd, TodCurves.EaseInOut);
                    mY = TodCommon.TodAnimateCurve(50, 20, mStateCountdown, thePositionStart2, num - 120, TodCurves.EaseInOut);
                    if (mStateCountdown <= 0)
                    {
                        PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpdown, ReanimLoopType.PlayOnceAndHold, 0, 60f);
                        mState = PlantState.SquashFalling;
                        mStateCountdown = 10;
                        return;
                    }
                }
                else if (mState == PlantState.SquashFalling)
                {
                    mY = TodCommon.TodAnimateCurve(10, 0, mStateCountdown, num - 120, num, TodCurves.Linear);
                    if (mStateCountdown == 4)
                    {
                        DoSquashDamage();
                    }
                    if (mStateCountdown <= 0)
                    {
                        if (mBoard.IsPoolSquare(theGridX, mRow))
                        {
                            mApp.AddReanimation(mX - 11, mY + 20, mRenderOrder + 1, ReanimationType.Splash);
                            mApp.PlayFoley(FoleyType.Splat);
                            mApp.PlaySample(Resources.SOUND_ZOMBIESPLASH);
                            Die();
                            return;
                        }
                        mState = PlantState.SquashDoneFalling;
                        mStateCountdown = 100;
                        mBoard.ShakeBoard(1, 4);
                        mApp.PlayFoley(FoleyType.Thump);
                        float num2 = 80f;
                        if (mBoard.StageHasRoof())
                        {
                            num2 -= 11f;
                        }
                        mApp.AddTodParticle(mX + 40, mY + num2, mRenderOrder + 4, ParticleEffect.DustSquash);
                        return;
                    }
                }
                else if (mState == PlantState.SquashDoneFalling && mStateCountdown <= 0)
                {
                    Die();
                }
                return;
            }
        }

        public bool NotOnGround()
        {
            return (mSeedType == SeedType.Squash && (mState == PlantState.SquashRising || mState == PlantState.SquashFalling || mState == PlantState.SquashDoneFalling)) || mSquished || mOnBungeeState == PlantOnBungeeState.RisingWithBungee || mDead;
        }

        public void DoSquashDamage()
        {
            int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary);
            TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.Primary);
            int num = 0;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead)
                {
                    int num2 = zombie.mRow - mRow;
                    if (zombie.mZombieType == ZombieType.Boss)
                    {
                        num2 = 0;
                    }
                    if (num2 == 0 && zombie.EffectedByDamage((uint)damageRangeFlags))
                    {
                        TRect zombieRect = zombie.GetZombieRect();
                        int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
                        int num3 = 0;
                        if (zombie.mZombieType == ZombieType.Football)
                        {
                            num3 = -20;
                        }
                        if (rectOverlap > num3)
                        {
                            zombie.TakeDamage(1800, 18U, false);
                            num++;
                        }
                    }
                }
            }
            if (num >= 5 && !mApp.IsIZombieLevel())
            {
                mBoard.GrantAchievement(AchievementId.MonsterMash, true);
            }
        }

        public void BurnRow(int theRow)
        {
            int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary);
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead)
                {
                    int num = zombie.mRow - mRow;
                    if (zombie.mZombieType == ZombieType.Boss)
                    {
                        num = 0;
                    }
                    if (num == 0 && zombie.EffectedByDamage((uint)damageRangeFlags))
                    {
                        if (zombie.mZombieType == ZombieType.Bobsled && zombie.IsBobsledTeamWithSled() && ++mBoard.mBobsledKilled >= 3)
                        {
                            mBoard.GrantAchievement(AchievementId.ChillOut, true);
                        }
                        zombie.RemoveColdEffects();
                        zombie.ApplyBurn();
                    }
                }
            }
            int num2 = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num2))
            {
                if (gridItem.mGridY == theRow && gridItem.mGridItemType == GridItemType.Ladder)
                {
                    gridItem.GridItemDie();
                }
            }
            Zombie bossZombie = mBoard.GetBossZombie();
            if (bossZombie != null)
            {
                bossZombie.BossDestroyIceballInRow(theRow);
            }
        }

        public void IceZombies()
        {
            int num = 0;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && zombie.HitIceTrap())
                {
                    num++;
                }
            }
            if (num >= 20 && !mApp.IsLittleTroubleLevel())
            {
                mBoard.GrantAchievement(AchievementId.Num20BelowZero, true);
            }
            mBoard.mIceTrapCounter = 300;
            TodParticleSystem todParticleSystem = mApp.ParticleTryToGet(mBoard.mPoolSparklyParticleID);
            if (todParticleSystem != null)
            {
                todParticleSystem.mDontUpdate = true;
            }
            Zombie bossZombie = mBoard.GetBossZombie();
            if (bossZombie != null)
            {
                bossZombie.BossDestroyFireball();
            }
        }

        public void BlowAwayFliers(int theX, int theRow)
        {
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && !zombie.IsDeadOrDying())
                {
                    zombie.GetZombieRect();
                    if (zombie.IsFlying() && zombie.mZombiePhase != ZombiePhase.BalloonPopping)
                    {
                        zombie.mBlowingAway = true;
                    }
                }
            }
            mApp.PlaySample(Resources.SOUND_BLOVER);
            mBoard.mFogBlownCountDown = 4000;
        }

        public void UpdateGraveBuster()//3update
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            if (mState == PlantState.GravebusterLanding)
            {
                if (reanimation.mLoopCount > 0)
                {
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 10, 12f);
                    mStateCountdown = GameConstants.GRAVE_BUSTER_EAT_TIME;
                    mState = PlantState.GravebusterEating;
                    AddAttachedParticle(mX + 40, mY + 40, mRenderOrder + 4, ParticleEffect.GraveBuster);
                    return;
                }
            }
            else if (mState == PlantState.GravebusterEating && mStateCountdown <= 0)
            {
                GridItem graveStoneAt = mBoard.GetGraveStoneAt(mPlantCol, mRow);
                if (graveStoneAt != null)
                {
                    graveStoneAt.GridItemDie();
                    mBoard.mGravesCleared++;
                }
                mApp.AddTodParticle(mX + 40, mY + 40, mRenderOrder + 4, ParticleEffect.GraveBusterDie);
                Die();
                mBoard.DropLootPiece(mX + 40, mY, 12);
            }
        }

        public TodParticleSystem AddAttachedParticle(int thePosX, int thePosY, int theRenderPostition, ParticleEffect theEffect)
        {
            TodParticleSystem todParticleSystem = mApp.ParticleTryToGet(mParticleID);
            if (todParticleSystem != null)
            {
                todParticleSystem.ParticleSystemDie();
            }
            TodParticleSystem todParticleSystem2 = mApp.AddTodParticle(thePosX, thePosY, theRenderPostition, theEffect);
            if (todParticleSystem2 != null)
            {
                mParticleID = mApp.ParticleGetID(todParticleSystem2);
            }
            return todParticleSystem2;
        }

        public void GetPeaHeadOffset(ref int theOffsetX, ref int theOffsetY)
        {
            if (!mBodyReanimID.mActive)
            {
                return;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            int theTrackIndex = 0;
            if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_stem))
            {
                theTrackIndex = reanimation.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_anim_stem);
            }
            else if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
            {
                theTrackIndex = reanimation.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
            }
            ReanimatorTransform reanimatorTransform;
            reanimation.GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
            theOffsetX = (int)(reanimatorTransform.mTransX * Constants.IS);
            theOffsetY = (int)(reanimatorTransform.mTransY * Constants.IS);
            reanimatorTransform.PrepareForReuse();
        }

        public bool MakesSun()
        {
            return mSeedType == SeedType.Sunflower || mSeedType == SeedType.Twinsunflower || mSeedType == SeedType.Sunshroom;
        }

        public static void DrawSeedType(Graphics g, SeedType theSeedType, SeedType theImitaterType, DrawVariation theDrawVariation, float thePosX, float thePosY)
        {
            SeedType theSeedType2 = theSeedType;
            if (theSeedType == SeedType.Imitater && theImitaterType != SeedType.None)
            {
                theSeedType2 = theImitaterType;
            }
            if (Challenge.IsZombieSeedType(theSeedType2))
            {
                ZombieType theZombieType = Challenge.IZombieSeedTypeToZombieType(theSeedType2);
                GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedZombie(g, thePosX, thePosY, theZombieType);
                return;
            }
            if (theSeedType2 == SeedType.Sprout) 
            {
                Image image = AtlasResources.IMAGE_CACHED_MARIGOLD;
                int num = (int)(thePosX - (float)(image.mWidth));// * g.mScaleX);
                int num2 = (int)(thePosY - (float)image.mHeight);// * g.mScaleY);
                TodCommon.TodDrawImageScaledF(g, image, (float)num, (float)num2, g.mScaleX, g.mScaleY);
                return;
            }
            GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedPlant(g, thePosX, thePosY, theSeedType2, DrawVariation.Normal);
        }

        public void KillAllPlantsNearDoom()
        {
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && plant.mRow == mRow && plant.mPlantCol - mPlantCol == 0)
                {
                    plant.Die();
                }
            }
        }

        public bool IsOnHighGround()
        {
            return mBoard != null && mBoard.mGridSquareType[mPlantCol, mRow] == GridSquareType.HighGround;
        }

        public void UpdateTorchwood()//3update
        {
            TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.Primary);
            int num = -1;
            Projectile projectile = null;
            while (mBoard.IterateProjectiles(ref projectile, ref num))
            {
                if (projectile.mRow == mRow && (projectile.mProjectileType == ProjectileType.Pea || projectile.mProjectileType == ProjectileType.Snowpea))
                {
                    TRect projectileRect = projectile.GetProjectileRect();
                    int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, projectileRect);
                    if (rectOverlap >= 1)
                    {
                        if (projectile.mProjectileType == ProjectileType.Pea)
                        {
                            projectile.ConvertToFireball(mPlantCol);
                        }
                        else if (projectile.mProjectileType == ProjectileType.Snowpea)
                        {
                            projectile.ConvertToPea(mPlantCol);
                        }
                    }
                }
            }
        }

        public void LaunchStarFruit()
        {
            if (!FindStarFruitTarget())
            {
                return;
            }
            PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shoot, ReanimLoopType.PlayOnceAndHold, 20, 28f);
            mShootingCounter = 40;
        }

        public bool FindStarFruitTarget()
        {
            if (mRecentlyEatenCountdown > 0)
            {
                return true;
            }
            int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary);
            int num = mX + 40;
            int num2 = mY + 40;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead)
                {
                    TRect zombieRect = zombie.GetZombieRect();
                    if (zombie.EffectedByDamage((uint)damageRangeFlags))
                    {
                        if (zombie.mZombieType == ZombieType.Boss && mPlantCol >= 5)
                        {
                            return true;
                        }
                        if (zombie.mRow == mRow)
                        {
                            if (zombieRect.mX + zombieRect.mWidth < num)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (zombie.mZombieType == ZombieType.Digger)
                            {
                                zombieRect.mWidth += 10;
                            }
                            float theTime = TodCommon.Distance2D(num, num2, zombieRect.mX + zombieRect.mWidth / 2, zombieRect.mY + zombieRect.mHeight / 2) / 3.33f;
                            int num3 = (int)(zombie.ZombieTargetLeadX(theTime) - zombieRect.mWidth / 2);
                            if (num3 + zombieRect.mWidth > num && num3 < num)
                            {
                                return true;
                            }
                            int num4 = num3 + zombieRect.mWidth / 2;
                            int num5 = zombieRect.mY + zombieRect.mHeight / 2;
                            float num6 = TodCommon.RadToDeg((float)Math.Atan2((float)(num5 - num2), (float)(num4 - num)));
                            if (Math.Abs(zombie.mRow - mRow) < 2)
                            {
                                if (num6 > 20f && num6 < 40f)
                                {
                                    return true;
                                }
                                if (num6 < -25f && num6 > -45f)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (num6 > 25f && num6 < 35f)
                                {
                                    return true;
                                }
                                if (num6 < -28f && num6 > -38f)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void UpdateChomper()//3update
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (mState == PlantState.Ready)
            {
                Zombie zombie = FindTargetZombie(mRow, PlantWeapon.Primary);
                if (zombie != null)
                {
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_bite, ReanimLoopType.PlayOnceAndHold, 20, 24f);
                    mState = PlantState.ChomperBiting;
                    mStateCountdown = 70;
                    return;
                }
            }
            else if (mState == PlantState.ChomperBiting)
            {
                if (mStateCountdown <= 0)
                {
                    mApp.PlayFoley(FoleyType.Bigchomp);
                    Zombie zombie2 = FindTargetZombie(mRow, PlantWeapon.Primary);
                    bool flag = false;
                    if (zombie2 != null && (zombie2.mZombieType == ZombieType.Gargantuar || zombie2.mZombieType == ZombieType.RedeyeGargantuar || zombie2.mZombieType == ZombieType.Boss))
                    {
                        flag = true;
                    }
                    bool flag2 = false;
                    if (zombie2 == null)
                    {
                        flag2 = true;
                    }
                    else if (!zombie2.IsImmobilizied())
                    {
                        if (zombie2.IsBouncingPogo())
                        {
                            flag2 = true;
                        }
                        else if (zombie2.mZombiePhase == ZombiePhase.PolevaulterInVault || zombie2.mZombiePhase == ZombiePhase.PolevaulterPreVault)
                        {
                            flag2 = true;
                        }
                    }
                    if (flag)
                    {
                        mApp.PlayFoley(FoleyType.Splat);
                        zombie2.TakeDamage(40, 0U, false);
                        mState = PlantState.ChomperBitingMissed;
                        return;
                    }
                    if (flag2)
                    {
                        mState = PlantState.ChomperBitingMissed;
                        return;
                    }
                    zombie2.DieWithLoot();
                    mState = PlantState.ChomperBitingGotOne;
                    return;
                }
            }
            else if (mState == PlantState.ChomperBitingGotOne)
            {
                if (reanimation.mLoopCount > 0)
                {
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_chew, ReanimLoopType.Loop, 0, 15f);
                    if (mApp.IsIZombieLevel())
                    {
                        reanimation.mAnimRate = 0f;
                    }
                    mState = PlantState.ChomperDigesting;
                    mStateCountdown = 4000;
                    return;
                }
            }
            else if (mState == PlantState.ChomperDigesting)
            {
                if (mStateCountdown <= 0)
                {
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_swallow, ReanimLoopType.PlayOnceAndHold, 20, 12f);
                    mState = PlantState.ChomperSwallowing;
                    return;
                }
            }
            else if ((mState == PlantState.ChomperSwallowing || mState == PlantState.ChomperBitingMissed) && reanimation.mLoopCount > 0)
            {
                PlayIdleAnim(reanimation.mDefinition.mFPS);
                mState = PlantState.Ready;
            }
        }

        public void DoBlink()
        {
            mBlinkCountdown = GameConstants.BLINK_RATE + RandomNumbers.NextNumber(GameConstants.BLINK_RATE);
            if (NotOnGround() || mShootingCounter != 0)
            {
                return;
            }
            if (mSeedType == SeedType.Potatomine && mState != PlantState.PotatoArmed)
            {
                return;
            }
            if (mState == PlantState.CactusRising || mState == PlantState.CactusHigh || mState == PlantState.CactusLowering || mState == PlantState.MagnetshroomSucking || mState == PlantState.MagnetshroomCharging)
            {
                return;
            }
            EndBlink();
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null || reanimation.mDead)
            {
                return;
            }
            if (mSeedType == SeedType.Tallnut && reanimation.GetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_idle) == AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED2)
            {
                return;
            }
            if (mSeedType == SeedType.Garlic && reanimation.GetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_face) == AtlasResources.IMAGE_REANIM_GARLIC_BODY3)
            {
                return;
            }
            if (mSeedType == SeedType.Wallnut || mSeedType == SeedType.Tallnut || mSeedType == SeedType.ExplodeONut || mSeedType == SeedType.GiantWallnut)
            {
                mBlinkCountdown = GameConstants.BLINK_RATE_WALLNUT + RandomNumbers.NextNumber(GameConstants.BLINK_RATE_WALLNUT);
            }
            Reanimation reanimation2 = AttachBlinkAnim(reanimation);
            if (reanimation2 != null)
            {
                mBlinkReanimID = mApp.ReanimationGetID(reanimation2);
            }
            reanimation.AssignRenderGroupToPrefix("anim_eye", -1);
        }

        public void UpdateBlink()//3update
        {
            if (mBlinkReanimID != null)
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mBlinkReanimID);
                if (reanimation == null || reanimation.mLoopCount > 0)
                {
                    EndBlink();
                }
            }
            if (mIsAsleep)
            {
                return;
            }
            if (mBlinkCountdown > 0)
            {
                //mBlinkCountdown -= 3;
                mBlinkCountdown--;
                if (mBlinkCountdown == 0)
                {
                    DoBlink();
                }
            }
        }

        public void PlayBodyReanim(string theTrackName, ReanimLoopType theLoopType, byte theBlendTime, float theAnimRate)
        {
            lastPlayedBodyReanim_Name = theTrackName;
            lastPlayedBodyReanim_Type = theLoopType;
            lastPlayedBodyReanim_BlendTime = theBlendTime;
            lastPlayedBodyReanim_AnimRate = theAnimRate;
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            if (theBlendTime > 0)
            {
                reanimation.StartBlend(theBlendTime);
            }
            if (theAnimRate > 0f)
            {
                reanimation.mAnimRate = theAnimRate;
            }
            reanimation.mLoopType = theLoopType;
            reanimation.mLoopCount = 0;
            reanimation.SetFramesForLayer(theTrackName);
        }

        public void UpdateMagnetShroom()//3update
        {
            for (int i = 0; i < GameConstants.MAX_MAGNET_ITEMS; i++)
            {
                MagnetItem magnetItem = mMagnetItems[i];
                if (magnetItem.mItemType != MagnetItemType.None)
                {
                    SexyVector2 sexyVector = new SexyVector2(mX + magnetItem.mDestOffsetX - magnetItem.mPosX, mY + magnetItem.mDestOffsetY - magnetItem.mPosY);
                    float num = sexyVector.Magnitude();
                    if (num >= 20f)
                    {
                        magnetItem.mPosX += sexyVector.x * 0.05f;
                        magnetItem.mPosY += sexyVector.y * 0.05f;
                    }
                }
            }
            if (mState == PlantState.MagnetshroomCharging)
            {
                if (mStateCountdown <= 0)
                {
                    mState = PlantState.Ready;
                    float theAnimRate = TodCommon.RandRangeFloat(10f, 15f);
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 30, theAnimRate);
                    if (mApp.IsIZombieLevel())
                    {
                        Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
                        reanimation.mAnimRate = 0f;
                    }
                    MagnetItem magnetItem2 = mMagnetItems[0];
                    magnetItem2.mItemType = MagnetItemType.None;
                }
                return;
            }
            if (mState == PlantState.MagnetshroomSucking)
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
                if (reanimation.mLoopCount > 0)
                {
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_nonactive_idle2, ReanimLoopType.Loop, 20, 2f);
                    if (mApp.IsIZombieLevel())
                    {
                        reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
                        reanimation.mAnimRate = 0f;
                    }
                    mState = PlantState.MagnetshroomCharging;
                }
                return;
            }
            Zombie zombie = null;
            float num2 = 0f;
            int count = mBoard.mZombies.Count;
            for (int j = 0; j < count; j++)
            {
                Zombie zombie2 = mBoard.mZombies[j];
                if (!zombie2.mDead)
                {
                    TRect zombieRect = zombie2.GetZombieRect();
                    int num3 = zombie2.mRow - mRow;
                    if (!zombie2.mMindControlled && zombie2.mHasHead && zombie2.mZombieHeight == ZombieHeight.ZombieNormal && zombie2.mZombiePhase != ZombiePhase.RisingFromGrave && !zombie2.IsDeadOrDying() && zombieRect.mX <= Constants.WIDE_BOARD_WIDTH && num3 <= 2 && num3 >= -2)
                    {
                        if (zombie2.mZombiePhase == ZombiePhase.DiggerTunneling || zombie2.mZombiePhase == ZombiePhase.DiggerStunned || zombie2.mZombiePhase == ZombiePhase.DiggerWalking || zombie2.mZombieType == ZombieType.Pogo)
                        {
                            if (!zombie2.mHasObject)
                            {
                                goto IL_322;
                            }
                        }
                        else if (zombie2.mHelmType != HelmType.Pail && zombie2.mHelmType != HelmType.Football && zombie2.mShieldType != ShieldType.Door && zombie2.mShieldType != ShieldType.Ladder && zombie2.mZombiePhase != ZombiePhase.JackInTheBoxRunning)
                        {
                            goto IL_322;
                        }
                        int theRadius = 270;
                        if (zombie2.mIsEating)
                        {
                            theRadius = 320;
                        }
                        if (GameConstants.GetCircleRectOverlap(mX, mY + 20, theRadius, zombieRect))
                        {
                            float num4 = TodCommon.Distance2D(mX, mY, zombieRect.mX, zombieRect.mY);
                            num4 += Math.Abs(num3) * 80f;
                            if (zombie == null || num4 < num2)
                            {
                                zombie = zombie2;
                                num2 = num4;
                            }
                        }
                    }
                }
                IL_322:;
            }
            if (zombie != null)
            {
                MagnetShroomAttactItem(zombie);
                return;
            }
            GridItem gridItem = null;
            float num5 = 0f;
            int num6 = -1;
            GridItem gridItem2 = null;
            while (mBoard.IterateGridItems(ref gridItem2, ref num6))
            {
                if (gridItem2.mGridItemType == GridItemType.Ladder)
                {
                    int num7 = gridItem2.mGridX - mPlantCol;
                    int num8 = gridItem2.mGridY - mRow;
                    int num9 = Math.Max(Math.Abs(num7), Math.Abs(num8));
                    if (num9 <= 2)
                    {
                        float num10 = num9;
                        num10 += Math.Abs(num8) * 0.05f;
                        if (gridItem == null || num10 < num5)
                        {
                            gridItem = gridItem2;
                            num5 = num10;
                        }
                    }
                }
            }
            if (gridItem != null)
            {
                mState = PlantState.MagnetshroomSucking;
                mStateCountdown = 1500;
                PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.PlayOnceAndHold, 20, 12f);
                mApp.PlayFoley(FoleyType.Magnetshroom);
                gridItem.GridItemDie();
                MagnetItem freeMagnetItem = GetFreeMagnetItem();
                freeMagnetItem.mPosX = mBoard.GridToPixelX(gridItem.mGridX, gridItem.mGridY) + 40;
                freeMagnetItem.mPosY = mBoard.GridToPixelY(gridItem.mGridX, gridItem.mGridY);
                freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 10f;
                freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f);
                freeMagnetItem.mItemType = MagnetItemType.LadderPlaced;
            }
        }

        public MagnetItem GetFreeMagnetItem()
        {
            if (mSeedType == SeedType.GoldMagnet)
            {
                for (int i = 0; i < GameConstants.MAX_MAGNET_ITEMS; i++)
                {
                    if (mMagnetItems[i].mItemType == MagnetItemType.None)
                    {
                        return mMagnetItems[i];
                    }
                }
                return null;
            }
            return mMagnetItems[0];
        }

        public void DrawMagnetItems(Graphics g)
        {
            float num = 0f;
            float num2 = Plant.PlantDrawHeightOffset(mBoard, this, mSeedType, mPlantCol, mRow);
            for (int i = 0; i < GameConstants.MAX_MAGNET_ITEMS; i++)
            {
                MagnetItem magnetItem = mMagnetItems[i];
                if (magnetItem.mItemType != MagnetItemType.None)
                {
                    int theCelCol = 0;
                    int theCelRow = 0;
                    Image theImageStrip = null;
                    float num3 = 1f;
                    if (magnetItem.mItemType == MagnetItemType.Pail1)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET1;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.Pail2)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET2;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.Pail3)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET3;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.FootballHelmet1)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.FootballHelmet2)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET2;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.FootballHelmet3)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET3;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.Door1)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR1;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.Door2)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR2;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.Door3)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR3;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType >= MagnetItemType.Pogo1 && magnetItem.mItemType <= MagnetItemType.Pogo3)
                    {
                        theCelCol = magnetItem.mItemType - MagnetItemType.Pogo1;
                        theImageStrip = AtlasResources.IMAGE_ZOMBIEPOGO;
                        num3 = 0.8f;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.Ladder1)
                    {
                        num3 = 0.8f;
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.Ladder2)
                    {
                        num3 = 0.8f;
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1_DAMAGE1;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.Ladder3)
                    {
                        num3 = 0.8f;
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1_DAMAGE2;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.LadderPlaced)
                    {
                        num3 = 0.8f;
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_5;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.JackInTheBox)
                    {
                        num3 = 0.8f;
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_JACKBOX_BOX;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.PickAxe)
                    {
                        num3 = 0.8f;
                        theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_PICKAXE;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.SilverCoin)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_COIN_SILVER_DOLLAR;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.GoldCoin)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_COIN_GOLD_DOLLAR;
                    }
                    else if (magnetItem.mItemType == MagnetItemType.Diamond)
                    {
                        theImageStrip = AtlasResources.IMAGE_REANIM_DIAMOND;
                    }
                    else
                    {
                        Debug.ASSERT(false);
                    }
                    if (num3 == 1f)
                    {
                        g.DrawImageCel(theImageStrip, (int)((magnetItem.mPosX - mX + num) * Constants.S), (int)((magnetItem.mPosY - mY + num2) * Constants.S), theCelCol, theCelRow);
                    }
                    else
                    {
                        TodCommon.TodDrawImageCelScaledF(g, theImageStrip, (magnetItem.mPosX - mX + num) * Constants.S, (magnetItem.mPosY - mY + num2) * Constants.S, theCelCol, 0, num3, num3);
                    }
                }
            }
        }

        public void UpdateDoomShroom()//3update
        {
            if (mIsAsleep || mState == PlantState.Doingspecial)
            {
                return;
            }
            mState = PlantState.Doingspecial;
            mDoSpecialCountdown = 100;
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            Debug.ASSERT(reanimation != null);
            reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_explode);
            reanimation.mAnimRate = 23f;
            reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
            reanimation.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_doomshroom_head1, 1f);
            reanimation.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_doomshroom_head2, 2f);
            reanimation.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_doomshroom_head3, 2f);
            mApp.PlayFoley(FoleyType.ReverseExplosion);
        }

        public void UpdateIceShroom()//3update
        {
            if (mIsAsleep || mState == PlantState.Doingspecial)
            {
                return;
            }
            mState = PlantState.Doingspecial;
            mDoSpecialCountdown = 100;
        }

        public void UpdatePotato()//3update
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            if (mState == PlantState.Notready)
            {
                if (mStateCountdown <= 0)
                {
                    int num = mX + mWidth / 2;
                    int num2 = mY + mHeight / 2;
                    mApp.AddTodParticle(num, num2, mRenderOrder, ParticleEffect.PotatoMineRise);
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_rise, ReanimLoopType.PlayOnceAndHold, 20, 18f);
                    mState = PlantState.PotatoRising;
                    mApp.PlayFoley(FoleyType.DirtRise);
                    return;
                }
            }
            else if (mState == PlantState.PotatoRising)
            {
                if (reanimation.mLoopCount > 0)
                {
                    float num3 = TodCommon.RandRangeFloat(12f, 15f);
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_armed, ReanimLoopType.Loop, 0, num3);
                    PlantDefinition plantDefinition = Plant.GetPlantDefinition(mSeedType);
                    Reanimation reanimation2 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
                    reanimation2.mLoopType = ReanimLoopType.Loop;
                    reanimation2.mAnimRate = num3 - 2f;
                    reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_glow);
                    reanimation2.mFrameCount = 10;
                    reanimation2.ShowOnlyTrack(GlobalMembersReanimIds.ReanimTrackId_anim_glow);
                    reanimation2.SetTruncateDisappearingFrames(GlobalMembersReanimIds.ReanimTrackId_anim_glow, false);
                    mLightReanimID = mApp.ReanimationGetID(reanimation2);
                    reanimation2.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_light);
                    mState = PlantState.PotatoArmed;
                    mBlinkCountdown = 400 + RandomNumbers.NextNumber(400);
                    return;
                }
            }
            else if (mState == PlantState.PotatoArmed)
            {
                Zombie zombie = FindTargetZombie(mRow, PlantWeapon.Primary);
                if (zombie != null)
                {
                    DoSpecial();
                    return;
                }
                Reanimation reanimation3 = mApp.ReanimationTryToGet(mLightReanimID);
                if (reanimation3 != null)
                {
                    int theTimeAge = DistanceToClosestZombie();
                    reanimation3.mFrameCount = (short)TodCommon.TodAnimateCurve(200, 50, theTimeAge, 10, 3, TodCurves.Linear);
                }
            }
        }

        public int CalcRenderOrder()
        {
            PlantOrder plant_ORDER = PlantOrder.Normal;
            RenderLayer theRenderLayer = RenderLayer.Plant;
            int num = 0;
            SeedType seedType = mSeedType;
            if (mSeedType == SeedType.Imitater && mImitaterType != SeedType.None)
            {
                seedType = mImitaterType;
            }
            if (mApp.IsWallnutBowlingLevel())
            {
                theRenderLayer = RenderLayer.Projectile;
            }
            else if (seedType == SeedType.Pumpkinshell)
            {
                plant_ORDER = PlantOrder.Pumpkin;
            }
            else if (Plant.IsFlying(seedType))
            {
                plant_ORDER = PlantOrder.Flyer;
            }
            else if (seedType == SeedType.Flowerpot)
            {
                plant_ORDER = PlantOrder.Lilypad;
            }
            else if (seedType == SeedType.Lilypad && mApp.mGameMode != GameMode.ChallengeZenGarden)
            {
                plant_ORDER = PlantOrder.Lilypad;
            }
            if (seedType == SeedType.Cobcannon)
            {
                num = 0;
            }
            return Board.MakeRenderOrder(theRenderLayer, mRow, (int)plant_ORDER * 5 - mX + num);
        }

        public void AnimateNuts()//3update
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            Image image;
            Image image2;
            string theTrackName;
            if (mSeedType == SeedType.Wallnut)
            {
                image = AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED1;
                image2 = AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED2;
                theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face;
            }
            else
            {
                if (mSeedType != SeedType.Tallnut)
                {
                    return;
                }
                image = AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED1;
                image2 = AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED2;
                theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_idle;
            }
            int num = mX + 40;
            int num2 = mY + 10;
            if (mSeedType == SeedType.Tallnut)
            {
                num2 -= 32;
            }
            Image imageOverride = reanimation.GetImageOverride(theTrackName);
            if (mPlantHealth < mPlantMaxHealth / 3)
            {
                if (imageOverride != image2)
                {
                    reanimation.SetImageOverride(theTrackName, image2);
                    mApp.AddTodParticle(num, num2, mRenderOrder + 4, ParticleEffect.WallnutEatLarge);
                }
            }
            else if (mPlantHealth < mPlantMaxHealth * 2 / 3)
            {
                if (imageOverride != image)
                {
                    reanimation.SetImageOverride(theTrackName, image);
                    mApp.AddTodParticle(num, num2, mRenderOrder + 4, ParticleEffect.WallnutEatLarge);
                }
            }
            else
            {
                Image theImage = null;
                reanimation.SetImageOverride(theTrackName, theImage);
            }
            if (IsInPlay() && !mApp.IsIZombieLevel())
            {
                if (mRecentlyEatenCountdown > 0)
                {
                    reanimation.mAnimRate = 0.1f;
                    return;
                }
                if (reanimation.mAnimRate < 1f && mOnBungeeState != PlantOnBungeeState.RisingWithBungee)
                {
                    reanimation.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
                }
            }
        }

        public void SetSleeping(bool theIsAsleep)
        {
            if (mIsAsleep == theIsAsleep)
            {
                return;
            }
            if (NotOnGround())
            {
                return;
            }
            mIsAsleep = theIsAsleep;
            if (theIsAsleep)
            {
                float num = mX + 50f;
                float num2 = mY + 40f;
                if (mSeedType == SeedType.Fumeshroom)
                {
                    num += 12f;
                }
                else if (mSeedType == SeedType.Scaredyshroom)
                {
                    num2 -= 20f;
                }
                else if (mSeedType == SeedType.Gloomshroom)
                {
                    num2 -= 12f;
                }
                Reanimation reanimation = mApp.AddReanimation(num, num2, mRenderOrder + 2, ReanimationType.Sleeping);
                reanimation.mLoopType = ReanimLoopType.Loop;
                reanimation.mAnimRate = TodCommon.RandRangeFloat(6f, 8f);
                reanimation.mAnimTime = TodCommon.RandRangeFloat(0f, 0.9f);
                mSleepingReanimID = mApp.ReanimationGetID(reanimation);
            }
            else
            {
                mApp.RemoveReanimation(ref mSleepingReanimID);
                mSleepingReanimID = null;
            }
            Reanimation reanimation2 = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation2 == null)
            {
                return;
            }
            if (theIsAsleep)
            {
                if (!IsInPlay() && mSeedType == SeedType.Sunshroom)
                {
                    reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_bigsleep);
                }
                else if (reanimation2.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_sleep))
                {
                    float anAnimTime = reanimation2.mAnimTime;
                    reanimation2.StartBlend(20);
                    reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_sleep);
                    reanimation2.mAnimTime = anAnimTime;
                }
                else
                {
                    reanimation2.mAnimRate = 1f;
                }
                EndBlink();
                return;
            }
            if (!IsInPlay() && mSeedType == SeedType.Sunshroom)
            {
                reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_bigidle);
            }
            else if (reanimation2.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
            {
                float anAnimTime2 = reanimation2.mAnimTime;
                reanimation2.StartBlend(20);
                reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                reanimation2.mAnimTime = anAnimTime2;
            }
            if (reanimation2.mAnimRate < 2f && IsInPlay())
            {
                reanimation2.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
            }
        }

        public void UpdateShooting()//3update
        {
            if (NotOnGround())
            {
                return;
            }
            if (mShootingCounter == 0)
            {
                return;
            }
            //mShootingCounter -= 3;
            mShootingCounter--;
            //if (mSeedType == SeedType.Fumeshroom && mShootingCounter >= 15 && mShootingCounter < 18)
            if (mSeedType == SeedType.Fumeshroom && mShootingCounter == 15)
            {
                int theRenderPostition = Board.MakeRenderOrder(RenderLayer.Particle, mRow, 0);
                AddAttachedParticle(mX + 85, mY + 31, theRenderPostition, ParticleEffect.Fumecloud);
            }
            Reanimation reanimation4;
            Reanimation reanimation6;
            if (mSeedType == SeedType.Gloomshroom)
            {
                //if ((mShootingCounter >= 136 && mShootingCounter < 139) || (mShootingCounter >= 108 && mShootingCounter < 111) || (mShootingCounter >= 80 && mShootingCounter < 83) || (mShootingCounter >= 52 && mShootingCounter < 55))
                if (mShootingCounter == 136 || mShootingCounter == 108 || mShootingCounter == 80 || mShootingCounter == 52)
                {
                    int theRenderPostition2 = Board.MakeRenderOrder(RenderLayer.Particle, mRow, 0);
                    AddAttachedParticle(mX + 40, mY + 40, theRenderPostition2, ParticleEffect.Gloomcloud);
                }
                //if ((mShootingCounter >= 126 && mShootingCounter < 129) || (mShootingCounter >= 98 && mShootingCounter < 101) || (mShootingCounter >= 70 && mShootingCounter < 73) || (mShootingCounter >= 42 && mShootingCounter < 45))
                if (mShootingCounter == 126 || mShootingCounter == 98 || mShootingCounter == 70 || mShootingCounter == 42)
                {
                    Fire(null, mRow, PlantWeapon.Primary);
                }
            }
            else if (mSeedType == SeedType.Gatlingpea)
            {
                //if ((mShootingCounter >= 18 && mShootingCounter < 21) || (mShootingCounter >= 35 && mShootingCounter < 38) || (mShootingCounter >= 51 && mShootingCounter < 54) || (mShootingCounter >= 68 && mShootingCounter < 71))
                if (mShootingCounter == 18 || mShootingCounter == 35 || mShootingCounter == 51 || mShootingCounter == 68)
                {
                    Fire(null, mRow, PlantWeapon.Primary);
                }
            }
            else if (mSeedType == SeedType.Cattail)
            {
                //if (mShootingCounter >= 19 && mShootingCounter < 22)
                if (mShootingCounter == 19)
                {
                    Zombie zombie = FindTargetZombie(mRow, PlantWeapon.Primary);
                    if (zombie != null)
                    {
                        Fire(zombie, mRow, PlantWeapon.Primary);
                    }
                }
            }
            //else if (mShootingCounter >= 1 && mShootingCounter < 4)
            else if (mShootingCounter == 1)
            {
                if (mSeedType == SeedType.Threepeater)
                {
                    int theRow = mRow - 1;
                    int theRow2 = mRow + 1;
                    Reanimation reanimation = mApp.ReanimationTryToGet(mHeadReanimID);
                    Reanimation reanimation2 = mApp.ReanimationTryToGet(mHeadReanimID2);
                    Reanimation reanimation3 = mApp.ReanimationTryToGet(mHeadReanimID3);
                    if (reanimation.mLoopType == ReanimLoopType.PlayOnceAndHold)
                    {
                        Fire(null, theRow2, PlantWeapon.Primary);
                    }
                    if (reanimation2.mLoopType == ReanimLoopType.PlayOnceAndHold)
                    {
                        Fire(null, mRow, PlantWeapon.Primary);
                    }
                    if (reanimation3.mLoopType == ReanimLoopType.PlayOnceAndHold)
                    {
                        Fire(null, theRow, PlantWeapon.Primary);
                        return;
                    }
                }
                else if (mSeedType == SeedType.Splitpea)
                {
                    reanimation4 = mApp.ReanimationTryToGet(mHeadReanimID);
                    Reanimation reanimation5 = mApp.ReanimationTryToGet(mHeadReanimID2);
                    if (reanimation4.mLoopType == ReanimLoopType.PlayOnceAndHold)
                    {
                        Fire(null, mRow, PlantWeapon.Primary);
                    }
                    if (reanimation5.mLoopType == ReanimLoopType.PlayOnceAndHold)
                    {
                        Fire(null, mRow, PlantWeapon.Secondary);
                        return;
                    }
                }
                else
                {
                    if (mState == PlantState.CactusLow)
                    {
                        Fire(null, mRow, PlantWeapon.Secondary);
                        return;
                    }
                    if (mSeedType == SeedType.Cabbagepult || mSeedType == SeedType.Kernelpult || mSeedType == SeedType.Melonpult || mSeedType == SeedType.Wintermelon)
                    {
                        PlantWeapon thePlantWeapon = PlantWeapon.Primary;
                        if (mState == PlantState.KernelpultButter)
                        {
                            reanimation6 = mApp.ReanimationGet(mBodyReanimID);
                            reanimation6.AssignRenderGroupToPrefix("Cornpult_butter", -1);
                            reanimation6.AssignRenderGroupToPrefix("Cornpult_kernal", 0);
                            mState = PlantState.Notready;
                            thePlantWeapon = PlantWeapon.Secondary;
                        }
                        Zombie theTargetZombie = FindTargetZombie(mRow, thePlantWeapon);
                        Fire(theTargetZombie, mRow, thePlantWeapon);
                        return;
                    }
                    Fire(null, mRow, PlantWeapon.Primary);
                }
                return;
            }
            if (mShootingCounter > 0)
            {
                return;
            }
            reanimation6 = mApp.ReanimationTryToGet(mBodyReanimID);
            reanimation4 = mApp.ReanimationTryToGet(mHeadReanimID);
            if (mSeedType == SeedType.Threepeater)
            {
                Reanimation reanimation7 = reanimation4;
                Reanimation reanimation8 = mApp.ReanimationTryToGet(mHeadReanimID2);
                Reanimation reanimation9 = mApp.ReanimationTryToGet(mHeadReanimID3);
                if (reanimation8.mLoopCount > 0)
                {
                    if (reanimation7.mLoopType == ReanimLoopType.PlayOnceAndHold)
                    {
                        reanimation7.StartBlend(20);
                        reanimation7.mLoopType = ReanimLoopType.Loop;
                        reanimation7.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle1);
                        reanimation7.mAnimRate = reanimation6.mAnimRate;
                        reanimation7.mAnimTime = reanimation6.mAnimTime;
                    }
                    reanimation8.StartBlend(20);
                    reanimation8.mLoopType = ReanimLoopType.Loop;
                    reanimation8.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle2);
                    reanimation8.mAnimRate = reanimation6.mAnimRate;
                    reanimation8.mAnimTime = reanimation6.mAnimTime;
                    if (reanimation9.mLoopType == ReanimLoopType.PlayOnceAndHold)
                    {
                        reanimation9.StartBlend(20);
                        reanimation9.mLoopType = ReanimLoopType.Loop;
                        reanimation9.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle3);
                        reanimation9.mAnimRate = reanimation6.mAnimRate;
                        reanimation9.mAnimTime = reanimation6.mAnimTime;
                    }
                    return;
                }
            }
            else
            {
                if (mSeedType == SeedType.Splitpea)
                {
                    Reanimation reanimation10 = mApp.ReanimationGet(mHeadReanimID2);
                    if (reanimation4.mLoopCount > 0)
                    {
                        reanimation4.StartBlend(20);
                        reanimation4.mLoopType = ReanimLoopType.Loop;
                        reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
                        reanimation4.mAnimRate = reanimation6.mAnimRate;
                        reanimation4.mAnimTime = reanimation6.mAnimTime;
                    }
                    if (reanimation10.mLoopCount > 0)
                    {
                        reanimation10.StartBlend(20);
                        reanimation10.mLoopType = ReanimLoopType.Loop;
                        reanimation10.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_idle);
                        reanimation10.mAnimRate = reanimation6.mAnimRate;
                        reanimation10.mAnimTime = reanimation6.mAnimTime;
                    }
                    return;
                }
                if (mState == PlantState.CactusHigh)
                {
                    if (reanimation6.mLoopCount > 0)
                    {
                        PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idlehigh, ReanimLoopType.Loop, 20, 0f);
                        reanimation6.mAnimRate = reanimation6.mDefinition.mFPS;
                        if (mApp.IsIZombieLevel())
                        {
                            reanimation6.mAnimRate = 0f;
                        }
                        return;
                    }
                }
                else if (reanimation4 != null)
                {
                    if (reanimation4.mLoopCount > 0)
                    {
                        reanimation4.StartBlend(20);
                        reanimation4.mLoopType = ReanimLoopType.Loop;
                        reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
                        reanimation4.mAnimRate = reanimation6.mAnimRate;
                        reanimation4.mAnimTime = reanimation6.mAnimTime;
                        return;
                    }
                }
                else if (mSeedType == SeedType.Cobcannon)
                {
                    if (reanimation6.mLoopCount > 0)
                    {
                        mState = PlantState.CobcannonArming;
                        mStateCountdown = 3000;
                        PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_unarmed_idle, ReanimLoopType.Loop, 20, reanimation6.mDefinition.mFPS);
                        return;
                    }
                }
                else if (reanimation6 != null && reanimation6.mLoopCount > 0)
                {
                    PlayIdleAnim(reanimation6.mDefinition.mFPS);
                    return;
                }
            }
            mShootingCounter = 1;//3;
        }

        public void DrawShadow(Graphics g, float theOffsetX, float theOffsetY)
        {
            if (mSeedType == SeedType.Lilypad || mSeedType == SeedType.Starfruit || mSeedType == SeedType.Tanglekelp || mSeedType == SeedType.Seashroom || mSeedType == SeedType.Cobcannon || mSeedType == SeedType.Spikeweed || mSeedType == SeedType.Spikerock || mSeedType == SeedType.Gravebuster || mSeedType == SeedType.Cattail || mOnBungeeState == PlantOnBungeeState.RisingWithBungee)
            {
                return;
            }
            if (IsOnBoard() && mBoard.mApp.mGameMode == GameMode.ChallengeZenGarden && mBoard.mApp.mZenGarden.mGardenType == GardenType.Main)
            {
                return;
            }
            int num = 0;
            float num2 = -3f;
            float num3 = 51f;
            float num4 = 1f;
            if (mBoard != null && mBoard.StageIsNight())
            {
                num = 1;
            }
            if (mSeedType == SeedType.Squash)
            {
                if (mBoard != null)
                {
                    num3 += mBoard.GridToPixelY(mPlantCol, mRow) - mY;
                }
                num3 += 5f;
            }
            else if (mSeedType == SeedType.Puffshroom || mSeedType == SeedType.Seashroom)
            {
                num4 = 0.5f;
                num3 -= 9f;
            }
            else if (mSeedType == SeedType.Sunshroom)
            {
                num3 += -9f;
                if (mState == PlantState.SunshroomSmall)
                {
                    num4 = 0.5f;
                }
                else if (mState == PlantState.SunshroomGrowing)
                {
                    Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                    num4 = 0.5f + 0.5f * reanimation.mAnimTime;
                }
                else
                {
                    num4 = 1f;
                }
            }
            else if (mSeedType == SeedType.Umbrella)
            {
                num4 = 0.5f;
                num2 -= 4f;
                num3 += 1f;
            }
            else if (mSeedType == SeedType.Fumeshroom || mSeedType == SeedType.Gloomshroom)
            {
                num4 = 1.3f;
                num3 -= 4f;
            }
            else if (mSeedType == SeedType.Cabbagepult || mSeedType == SeedType.Melonpult || mSeedType == SeedType.Wintermelon)
            {
                num3 -= 4f;
            }
            else if (mSeedType == SeedType.Kernelpult)
            {
                num2 += 3f;
                num3 -= 4f;
            }
            else if (mSeedType == SeedType.Scaredyshroom)
            {
                num2 += -6f;
                num3 += 4f;
            }
            else if (mSeedType == SeedType.Chomper)
            {
                num2 += -18f;
                num3 += 6f;
            }
            else if (mSeedType == SeedType.Flowerpot)
            {
                num2 += -1f;
                num3 += -5f;
            }
            else if (mSeedType == SeedType.Tallnut)
            {
                num3 += 3f;
                num4 = 1.3f;
            }
            else if (mSeedType == SeedType.Pumpkinshell)
            {
                num3 += -5f;
                num4 = 1.4f;
            }
            else if (mSeedType == SeedType.Cactus)
            {
                num2 += -5f;
                num3 += -1f;
            }
            else if (mSeedType == SeedType.Plantern)
            {
                num3 += 6f;
            }
            else if (mSeedType == SeedType.InstantCoffee)
            {
                num3 += 20f;
            }
            else if (mSeedType == SeedType.GiantWallnut)
            {
                num2 -= 30f;
                num3 += 5f;
                num4 = 1.7f;
            }
            if (Plant.IsFlying(mSeedType))
            {
                num3 += 10f;
                if (mBoard != null && (mBoard.GetTopPlantAt(mPlantCol, mRow, TopPlant.OnlyNormalPosition) != null || mBoard.GetTopPlantAt(mPlantCol, mRow, TopPlant.OnlyPumpkin) != null))
                {
                    return;
                }
            }
            if (num == 0)
            {
                TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW, (theOffsetX + num2) * Constants.S, (theOffsetY + num3) * Constants.S, num4, num4);
                return;
            }
            TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW2, (theOffsetX + num2) * Constants.S, (theOffsetY + num3) * Constants.S, num4, num4);
        }

        public void UpdateScaredyShroom()//3update
        {
            if (mShootingCounter > 0)
            {
                return;
            }
            bool flag = false;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead)
                {
                    TRect zombieRect = zombie.GetZombieRect();
                    int num = zombie.mRow - mRow;
                    if (zombie.mZombieType == ZombieType.Boss)
                    {
                        num = 0;
                    }
                    if (!zombie.mMindControlled && !zombie.IsDeadOrDying() && num <= 1 && num >= -1 && GameConstants.GetCircleRectOverlap(mX, mY + 20, 120, zombieRect))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            if (mState == PlantState.Ready)
            {
                if (flag)
                {
                    mState = PlantState.ScaredyshroomLowering;
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_scared, ReanimLoopType.PlayOnceAndHold, 10, 10f);
                }
            }
            else if (mState == PlantState.ScaredyshroomLowering)
            {
                if (reanimation.mLoopCount > 0)
                {
                    mState = PlantState.ScaredyshroomScared;
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_scaredidle, ReanimLoopType.Loop, 10, 0f);
                }
            }
            else if (mState == PlantState.ScaredyshroomScared)
            {
                if (!flag)
                {
                    mState = PlantState.ScaredyshroomRaising;
                    float theAnimRate = TodCommon.RandRangeFloat(7f, 12f);
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_grow, ReanimLoopType.PlayOnceAndHold, 10, theAnimRate);
                }
            }
            else if (mState == PlantState.ScaredyshroomRaising && reanimation.mLoopCount > 0)
            {
                mState = PlantState.Ready;
                float theRate = TodCommon.RandRangeFloat(10f, 15f);
                PlayIdleAnim(theRate);
            }
            if (mState != PlantState.Ready)
            {
                mLaunchCounter = mLaunchRate;
            }
        }

        public int DistanceToClosestZombie()
        {
            int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary);
            TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.Primary);
            int num = 1000;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && zombie.mRow == mRow && zombie.EffectedByDamage((uint)damageRangeFlags))
                {
                    TRect zombieRect = zombie.GetZombieRect();
                    int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
                    if (num > -rectOverlap)
                    {
                        num = Math.Max(-rectOverlap, 0);
                    }
                }
            }
            return num;
        }

        public void UpdateSpikeweed()//3update
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            if (mState == PlantState.SpikeweedAttacking)
            {
                if (mStateCountdown <= 0)
                {
                    mState = PlantState.Notready;
                }
                else if (mSeedType == SeedType.Spikerock)
                {
                    if (mStateCountdown == 69 || mStateCountdown == 33)
                    {
                        DoRowAreaDamage(20, 33U);
                    }
                }
                else if (mStateCountdown == 75)
                {
                    DoRowAreaDamage(20, 33U);
                }
                if (reanimation.mLoopCount > 0)
                {
                    float theRate = TodCommon.RandRangeFloat(12f, 15f);
                    PlayIdleAnim(theRate);
                    return;
                }
            }
            else
            {
                Zombie zombie = FindTargetZombie(mRow, PlantWeapon.Primary);
                if (zombie != null)
                {
                    SpikeweedAttack();
                }
            }
        }

        public void MagnetShroomAttactItem(Zombie theZombie)
        {
            mState = PlantState.MagnetshroomSucking;
            mStateCountdown = 1500;
            PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.PlayOnceAndHold, 20, 12f);
            mApp.PlayFoley(FoleyType.Magnetshroom);
            MagnetItem freeMagnetItem = GetFreeMagnetItem();
            if (theZombie.mHelmType == HelmType.Pail)
            {
                int helmDamageIndex = theZombie.GetHelmDamageIndex();
                theZombie.mHelmHealth = 0;
                theZombie.mHelmType = HelmType.None;
                theZombie.mHasHelm = false;
                theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_bucket, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
                theZombie.ReanimShowPrefix("anim_bucket", -1);
                theZombie.ReanimShowPrefix("anim_hair", 0);
                freeMagnetItem.mPosX -= AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET1.mWidth / 2;
                freeMagnetItem.mPosY -= AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET1.mHeight / 2;
                freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 25f;
                freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f) + 20f;
                freeMagnetItem.mItemType = MagnetItemType.Pail1 + helmDamageIndex;
            }
            else if (theZombie.mHelmType == HelmType.Football)
            {
                int helmDamageIndex2 = theZombie.GetHelmDamageIndex();
                theZombie.mHelmHealth = 0;
                theZombie.mHelmType = HelmType.None;
                theZombie.mHasHelm = false;
                theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_football_helmet, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
                theZombie.ReanimShowPrefix("zombie_football_helmet", -1);
                theZombie.ReanimShowPrefix("anim_hair", 0);
                freeMagnetItem.mPosX = theZombie.mPosX + 37f;
                freeMagnetItem.mPosY = theZombie.mPosY - 60f;
                freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 20f;
                freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f) + 20f;
                freeMagnetItem.mItemType = MagnetItemType.FootballHelmet1 + helmDamageIndex2;
            }
            else if (theZombie.mShieldType == ShieldType.Door)
            {
                int shieldDamageIndex = theZombie.GetShieldDamageIndex();
                theZombie.DetachShield();
                theZombie.mZombiePhase = ZombiePhase.ZombieNormal;
                if (!theZombie.mIsEating)
                {
                    Debug.ASSERT(theZombie.mZombieHeight == ZombieHeight.ZombieNormal);
                    theZombie.StartWalkAnim(0);
                }
                theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_screendoor, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
                freeMagnetItem.mPosX -= AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR1.mWidth / 2;
                freeMagnetItem.mPosY -= AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR1.mHeight / 2;
                freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 30f;
                freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f);
                freeMagnetItem.mItemType = MagnetItemType.Door1 + shieldDamageIndex;
            }
            else if (theZombie.mShieldType == ShieldType.Ladder)
            {
                int shieldDamageIndex2 = theZombie.GetShieldDamageIndex();
                theZombie.DetachShield();
                freeMagnetItem.mPosX = theZombie.mPosX + 31f;
                freeMagnetItem.mPosY = theZombie.mPosY + 20f;
                freeMagnetItem.mPosX -= AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_5.GetCelWidth() / 2;
                freeMagnetItem.mPosY -= AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_5.GetCelHeight() / 2;
                freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 30f;
                freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f);
                freeMagnetItem.mItemType = MagnetItemType.Ladder1 + shieldDamageIndex2;
            }
            else if (theZombie.mZombieType == ZombieType.Pogo)
            {
                theZombie.PogoBreak(16U);
                ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
                theZombie.GetDrawPos(ref zombieDrawPosition);
                theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_pogo_stick, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
                freeMagnetItem.mPosX += (float)(-(float)AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_STICKDAMAGE1.mWidth / 2) + 40f;
                freeMagnetItem.mPosY += (float)(-(float)AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_STICKDAMAGE1.mHeight / 2) + 84f;
                freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 44f;
                freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f) + 10f;
                if (theZombie.mHasArm)
                {
                    freeMagnetItem.mItemType = MagnetItemType.Pogo1;
                }
                else
                {
                    freeMagnetItem.mItemType = MagnetItemType.Pogo3;
                }
            }
            else if (theZombie.mZombiePhase == ZombiePhase.JackInTheBoxRunning)
            {
                theZombie.StopZombieSound();
                theZombie.PickRandomSpeed();
                theZombie.mZombiePhase = ZombiePhase.ZombieNormal;
                theZombie.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_box, -1);
                theZombie.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_handle, -1);
                theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_box, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
                freeMagnetItem.mPosX -= AtlasResources.IMAGE_REANIM_ZOMBIE_JACKBOX_BOX.mWidth / 2;
                freeMagnetItem.mPosY -= AtlasResources.IMAGE_REANIM_ZOMBIE_JACKBOX_BOX.mHeight / 2;
                freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 20f;
                freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f) + 15f;
                freeMagnetItem.mItemType = MagnetItemType.JackInTheBox;
            }
            else if (theZombie.mZombieType == ZombieType.Digger)
            {
                theZombie.DiggerLoseAxe();
                theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_pickaxe, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
                freeMagnetItem.mPosX -= AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_PICKAXE.mWidth / 2;
                freeMagnetItem.mPosY -= AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_PICKAXE.mHeight / 2;
                freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 45f;
                freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f) + 15f;
                freeMagnetItem.mItemType = MagnetItemType.PickAxe;
            }
            freeMagnetItem.mDestOffsetX *= Constants.S;
            freeMagnetItem.mDestOffsetY *= Constants.S;
        }

        public void UpdateSunShroom()//3update
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            if (mState == PlantState.SunshroomSmall)
            {
                if (mStateCountdown <= 0)
                {
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_grow, ReanimLoopType.PlayOnceAndHold, 10, 12f);
                    mState = PlantState.SunshroomGrowing;
                    mApp.PlayFoley(FoleyType.Plantgrow);
                }
                UpdateProductionPlant();
                return;
            }
            if (mState == PlantState.SunshroomGrowing)
            {
                if (reanimation.mLoopCount > 0)
                {
                    float theAnimRate = TodCommon.RandRangeFloat(12f, 15f);
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_bigidle, ReanimLoopType.Loop, 10, theAnimRate);
                    mState = PlantState.SunshroomBig;
                    return;
                }
            }
            else
            {
                UpdateProductionPlant();
            }
        }

        public void UpdateBowling()//1update
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation != null && reanimation.TrackExists(Reanimation.ReanimTrackId__ground))
            {
                //float num = reanimation.GetTrackVelocity(Reanimation.ReanimTrackId__ground) / 4f;
                float num = reanimation.GetTrackVelocity(Reanimation.ReanimTrackId__ground) * Constants.S;
                if (mSeedType == SeedType.GiantWallnut)
                {
                    num *= 2f;
                }
                mX -= (int)num;
                if (mX > 900)
                {
                    Die();
                }
            }
            int num2 = 3;
            if (mState == PlantState.BowlingUp)
            {
                mY -= num2;
            }
            else if (mState == PlantState.BowlingDown)
            {
                mY += num2;
            }
            int num3 = mBoard.GridToPixelY(0, mRow) - mY;
            if (num3 > 2 || num3 < -2)
            {
                return;
            }
            PlantState plantState = mState;
            if (plantState == PlantState.BowlingUp && mRow <= 0)
            {
                plantState = PlantState.BowlingDown;
            }
            else if (plantState == PlantState.BowlingDown && mRow >= 4)
            {
                plantState = PlantState.BowlingUp;
            }
            Zombie zombie = FindTargetZombie(mRow, PlantWeapon.Primary);
            if (zombie != null)
            {
                int num4 = mX + mWidth / 2;
                int num5 = mY + mHeight / 2;
                if (mSeedType == SeedType.ExplodeONut)
                {
                    mApp.PlayFoley(FoleyType.Cherrybomb);
                    mApp.PlaySample(Resources.SOUND_BOWLINGIMPACT2);
                    int theDamageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary) | 32;
                    mBoard.KillAllZombiesInRadius(mRow, num4, num5, 90, 1, true, theDamageRangeFlags);
                    mApp.AddTodParticle(num4, num5, 400000, ParticleEffect.Powie);
                    mBoard.ShakeBoard(3, -4);
                    Die();
                    return;
                }
                mApp.PlayFoley(FoleyType.Bowlingimpact);
                mBoard.ShakeBoard(1, -2);
                if (mSeedType == SeedType.GiantWallnut)
                {
                    zombie.TakeDamage(1800, 0U, true);
                }
                else if (zombie.mShieldType == ShieldType.Door && mState != PlantState.Notready)
                {
                    zombie.TakeDamage(1800, 0U, false);
                }
                else if (zombie.mShieldType != ShieldType.None)
                {
                    zombie.TakeShieldDamage(400, 0U);
                }
                else if (zombie.mHelmType != HelmType.None)
                {
                    if (zombie.mHelmType == HelmType.Pail)
                    {
                        mApp.PlayFoley(FoleyType.ShieldHit);
                    }
                    else if (zombie.mHelmType == HelmType.TrafficCone)
                    {
                        mApp.PlayFoley(FoleyType.PlasticHit);
                    }
                    zombie.TakeHelmDamage(900, 0U);
                }
                else
                {
                    zombie.TakeDamage(1800, 0U, false);
                }
                if ((!mApp.IsFirstTimeAdventureMode() || mBoard.mLevel > 10) && mSeedType == SeedType.Wallnut)
                {
                    mLaunchCounter++;
                    if (mLaunchCounter == 2)
                    {
                        mApp.PlayFoley(FoleyType.SpawnSun);
                        mBoard.AddCoin(num4, num5, CoinType.Silver, CoinMotion.Coin);
                    }
                    else if (mLaunchCounter == 3)
                    {
                        mApp.PlayFoley(FoleyType.SpawnSun);
                        mBoard.AddCoin(num4 - 5, num5, CoinType.Silver, CoinMotion.Coin);
                        mBoard.AddCoin(num4 + 5, num5, CoinType.Silver, CoinMotion.Coin);
                    }
                    else if (mLaunchCounter == 4)
                    {
                        mApp.PlayFoley(FoleyType.SpawnSun);
                        mBoard.AddCoin(num4 - 10, num5, CoinType.Silver, CoinMotion.Coin);
                        mBoard.AddCoin(num4, num5, CoinType.Silver, CoinMotion.Coin);
                        mBoard.AddCoin(num4 + 10, num5, CoinType.Silver, CoinMotion.Coin);
                    }
                    else if (mLaunchCounter >= 5)
                    {
                        mApp.PlayFoley(FoleyType.SpawnSun);
                        mBoard.AddCoin(num4, num5, CoinType.Gold, CoinMotion.Coin);
                        mBoard.GrantAchievement(AchievementId.RollSomeHeads, true);
                    }
                }
                if (mSeedType != SeedType.GiantWallnut)
                {
                    if (mRow == 4 || mState == PlantState.BowlingDown)
                    {
                        plantState = PlantState.BowlingUp;
                    }
                    else if (mRow == 0 || mState == PlantState.BowlingUp)
                    {
                        plantState = PlantState.BowlingDown;
                    }
                    else if (RandomNumbers.NextNumber(2) == 0)
                    {
                        plantState = PlantState.BowlingDown;
                    }
                    else
                    {
                        plantState = PlantState.BowlingUp;
                    }
                }
            }
            if (plantState == PlantState.BowlingUp)
            {
                mState = PlantState.BowlingUp;
                mRow--;
                mRenderOrder = CalcRenderOrder();
                return;
            }
            if (plantState == PlantState.BowlingDown)
            {
                mState = PlantState.BowlingDown;
                mRenderOrder = CalcRenderOrder();
                mRow++;
            }
        }

        public void AnimatePumpkin()//3update
        {
            if (!mBodyReanimID.mActive)
            {
                return;
            }
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            string reanimTrackId_pumpkin_front = GlobalMembersReanimIds.ReanimTrackId_pumpkin_front;
            Image imageOverride = reanimation.GetImageOverride(reanimTrackId_pumpkin_front);
            if (mPlantHealth < mPlantMaxHealth / 3)
            {
                if (imageOverride != AtlasResources.IMAGE_REANIM_PUMPKIN_DAMAGE3)
                {
                    reanimation.SetImageOverride(reanimTrackId_pumpkin_front, AtlasResources.IMAGE_REANIM_PUMPKIN_DAMAGE3);
                    return;
                }
            }
            else if (mPlantHealth < mPlantMaxHealth * 2 / 3)
            {
                if (imageOverride != AtlasResources.IMAGE_REANIM_PUMPKIN_DAMAGE1)
                {
                    reanimation.SetImageOverride(reanimTrackId_pumpkin_front, AtlasResources.IMAGE_REANIM_PUMPKIN_DAMAGE1);
                    return;
                }
            }
            else
            {
                Image theImage = null;
                reanimation.SetImageOverride(reanimTrackId_pumpkin_front, theImage);
            }
        }

        public void UpdateBlover()//3update
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            if (reanimation.mLoopCount > 0 && reanimation.mLoopType != ReanimLoopType.Loop)
            {
                reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_loop);
                reanimation.mLoopType = ReanimLoopType.Loop;
            }
            if (mState != PlantState.Doingspecial && mDoSpecialCountdown == 0)
            {
                DoSpecial();
            }
        }

        public void UpdateCactus()//3update
        {
            if (mShootingCounter > 0)
            {
                return;
            }
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            if (mState == PlantState.CactusRising)
            {
                if (reanimation.mLoopCount > 0)
                {
                    mState = PlantState.CactusHigh;
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idlehigh, ReanimLoopType.Loop, 20, 0f);
                    if (mApp.IsIZombieLevel())
                    {
                        reanimation.mAnimRate = 0f;
                    }
                    mLaunchCounter = 1;
                    return;
                }
            }
            else if (mState == PlantState.CactusHigh)
            {
                if (FindTargetZombie(mRow, PlantWeapon.Primary) == null)
                {
                    mState = PlantState.CactusLowering;
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lower, ReanimLoopType.PlayOnceAndHold, 20, reanimation.mDefinition.mFPS);
                    return;
                }
            }
            else if (mState == PlantState.CactusLowering)
            {
                if (reanimation.mLoopCount > 0)
                {
                    mState = PlantState.CactusLow;
                    PlayIdleAnim(0f);
                    return;
                }
            }
            else
            {
                Zombie zombie = FindTargetZombie(mRow, PlantWeapon.Primary);
                if (zombie != null)
                {
                    mState = PlantState.CactusRising;
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_rise, ReanimLoopType.PlayOnceAndHold, 20, reanimation.mDefinition.mFPS);
                    mApp.PlayFoley(FoleyType.Plantgrow);
                }
            }
        }

        public void StarFruitFire()
        {
            mApp.PlayFoley(FoleyType.Throw);
            for (int i = 0; i < 5; i++)
            {
                int theX = mX + 25;
                int theY = mY + 25;
                Projectile projectile = mBoard.AddProjectile(theX, theY, mRenderOrder + -1, mRow, ProjectileType.Star);
                projectile.mDamageRangeFlags = GetDamageRangeFlags(PlantWeapon.Primary);
                projectile.mMotionType = ProjectileMotion.Star;
                float velX = (float)Math.Cos(TodCommon.DegToRad(30f)) * 3.33f;
                float velY = (float)Math.Sin(TodCommon.DegToRad(30f)) * 3.33f;
                switch (i)
                {
                case 0:
                    projectile.mVelX = -3.33f;
                    projectile.mVelY = 0f;
                    break;
                case 1:
                    projectile.mVelX = 0f;
                    projectile.mVelY = 3.33f;
                    break;
                case 2:
                    projectile.mVelX = 0f;
                    projectile.mVelY = -3.33f;
                    break;
                case 3:
                    projectile.mVelX = velX;
                    projectile.mVelY = velY;
                    break;
                case 4:
                    projectile.mVelX = velX;
                    projectile.mVelY = -velY;
                    break;
                default:
                    Debug.ASSERT(false);
                    break;
                }
            }
        }

        public void UpdateTanglekelp()//3update
        {
            if (mState != PlantState.TanglekelpGrabbing)
            {
                Zombie zombie = FindTargetZombie(mRow, PlantWeapon.Primary);
                if (zombie != null)
                {
                    mApp.PlayFoley(FoleyType.Floop);
                    mState = PlantState.TanglekelpGrabbing;
                    mStateCountdown = 99;
                    zombie.PoolSplash(false);
                    float num = -13f;
                    float num2 = 15f;
                    if (zombie.mZombieType == ZombieType.Snorkel)
                    {
                        num = -43f;
                        num2 = 55f;
                    }
                    if (zombie.mZombiePhase == ZombiePhase.DolphinRiding)
                    {
                        num = -20f;
                        num2 = 37f;
                    }
                    Reanimation reanimation = zombie.AddAttachedReanim((int)num, (int)num2, ReanimationType.Tanglekelp);
                    zombie.mAttachmentID.mUsesClipping = true;
                    if (reanimation != null)
                    {
                        reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_grab);
                        reanimation.mAnimRate = 24f;
                        reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
                    }
                    mTargetZombieID = mBoard.ZombieGetID(zombie);
                    mTargetZombieID.mUsesClipping = true;
                    mTargetZombieID.draggedByTangleKelp = true;
                    return;
                }
            }
            else
            {
                if (mStateCountdown == 51)
                {
                    Zombie zombie2 = mBoard.ZombieTryToGet(mTargetZombieID);
                    if (zombie2 != null)
                    {
                        zombie2.DragUnder();
                        zombie2.PoolSplash(false);
                    }
                }
                if (mStateCountdown == 21)
                {
                    int aRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, mRow, 0);
                    Reanimation reanimation2 = mApp.AddReanimation(mX - 23, mY + 7, aRenderOrder, ReanimationType.Splash);
                    reanimation2.OverrideScale(1.3f, 1.3f);
                    mApp.AddTodParticle(mX + 31, mY + 64, aRenderOrder, ParticleEffect.PlantingPool);
                    mApp.PlayFoley(FoleyType.ZombieEnteringWater);
                }
                if (mStateCountdown <= 0)
                {
                    Die();
                    Zombie zombie3 = mBoard.ZombieTryToGet(mTargetZombieID);
                    if (zombie3 != null)
                    {
                        zombie3.DieWithLoot();
                    }
                }
            }
        }

        public Reanimation AttachBlinkAnim(Reanimation theReanimBody)
        {
            PlantDefinition plantDefinition = Plant.GetPlantDefinition(mSeedType);
            LawnApp lawnApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
            Reanimation reanimation = theReanimBody;
            string text = GlobalMembersReanimIds.ReanimTrackId_anim_blink;
            string theTrackName = Reanimation.ReanimTrackIdEmpty;
            if (mSeedType == SeedType.Wallnut || mSeedType == SeedType.Tallnut || mSeedType == SeedType.ExplodeONut || mSeedType == SeedType.GiantWallnut)
            {
                int num = RandomNumbers.NextNumber(10);
                if (num < 1 && theReanimBody.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_blink_twitch))
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_blink_twitch;
                }
                else if (num < 7)
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_blink_twice;
                }
                else
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_blink_thrice;
                }
            }
            else if (mSeedType == SeedType.Threepeater)
            {
                int num2 = RandomNumbers.NextNumber(3);
                if (num2 == 0)
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_blink1;
                    theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face1;
                    ReanimatorTrackInstance trackInstanceByName = theReanimBody.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
                    reanimation = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName.mAttachmentID);
                }
                else if (num2 == 1)
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_blink2;
                    theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face2;
                    ReanimatorTrackInstance trackInstanceByName2 = theReanimBody.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head2);
                    reanimation = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName2.mAttachmentID);
                }
                else
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_blink3;
                    theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face3;
                    ReanimatorTrackInstance trackInstanceByName3 = theReanimBody.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head3);
                    reanimation = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName3.mAttachmentID);
                }
            }
            else if (mSeedType == SeedType.Splitpea)
            {
                if (RandomNumbers.NextNumber(2) == 0)
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_blink;
                    theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face;
                    reanimation = mApp.ReanimationTryToGet(mHeadReanimID);
                }
                else
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_blink;
                    theTrackName = GlobalMembersReanimIds.ReanimTrackId_splitpea_head;
                    reanimation = mApp.ReanimationTryToGet(mHeadReanimID2);
                }
            }
            else if (mSeedType == SeedType.Twinsunflower)
            {
                if (RandomNumbers.NextNumber(2) == 0)
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_blink;
                    theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face;
                }
                else
                {
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_blink2;
                    theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face2;
                }
            }
            else if (mSeedType == SeedType.Peashooter || mSeedType == SeedType.Snowpea || mSeedType == SeedType.Repeater || mSeedType == SeedType.Leftpeater || mSeedType == SeedType.Gatlingpea)
            {
                if (theReanimBody.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_stem))
                {
                    ReanimatorTrackInstance trackInstanceByName4 = theReanimBody.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_stem);
                    reanimation = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName4.mAttachmentID);
                }
                else if (theReanimBody.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
                {
                    ReanimatorTrackInstance trackInstanceByName5 = theReanimBody.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                    reanimation = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName5.mAttachmentID);
                }
            }
            if (reanimation == null)
            {
                return null;
            }
            if (!theReanimBody.TrackExists(text))
            {
                return null;
            }
            Reanimation reanimation2 = lawnApp.mEffectSystem.mReanimationHolder.AllocReanimation(0f, 0f, 0, plantDefinition.mReanimationType);
            reanimation2.SetFramesForLayer(text);
            reanimation2.mLoopType = ReanimLoopType.PlayOnceFullLastFrameAndHold;
            reanimation2.mAnimRate = 15f;
            reanimation2.mColorOverride.CopyFrom(theReanimBody.mColorOverride);
            if (reanimation.TrackExists(theTrackName))
            {
                reanimation2.AttachToAnotherReanimation(ref reanimation, theTrackName);
            }
            else if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_face))
            {
                reanimation2.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_face);
            }
            else if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
            {
                reanimation2.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
            }
            reanimation2.mFilterEffect = theReanimBody.mFilterEffect;
            return reanimation2;
        }

        public void UpdateReanimColor()//3update
        {
            if (!IsOnBoard())
            {
                return;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            SeedType seedTypeInCursor = mBoard.GetSeedTypeInCursor();
            bool flag = false;
            if (IsPartOfUpgradableTo(seedTypeInCursor) && mBoard.CanPlantAt(mPlantCol, mRow, seedTypeInCursor) == PlantingReason.Ok)
            {
                SexyColor flashingColor = TodCommon.GetFlashingColor(mBoard.mMainCounter, 90);
                if (flashingColor != reanimation.mColorOverride)
                {
                    flag = true;
                    reanimation.mColorOverride = flashingColor;
                }
            }
            else if (seedTypeInCursor == SeedType.Cobcannon && mSeedType == SeedType.Kernelpult && mBoard.CanPlantAt(mPlantCol - 1, mRow, seedTypeInCursor) == PlantingReason.Ok)
            {
                SexyColor flashingColor2 = TodCommon.GetFlashingColor(mBoard.mMainCounter, 90);
                if (flashingColor2 != reanimation.mColorOverride)
                {
                    flag = true;
                    reanimation.mColorOverride = flashingColor2;
                }
            }
            else if (mSeedType == SeedType.ExplodeONut)
            {
                SexyColor sexyColor = new SexyColor(255, 64, 64);
                if (sexyColor != reanimation.mColorOverride)
                {
                    flag = true;
                    reanimation.mColorOverride = sexyColor;
                }
            }
            else
            {
                SexyColor white = SexyColor.White;
                if (white != reanimation.mColorOverride)
                {
                    flag = true;
                    reanimation.mColorOverride = white;
                }
            }
            if (mHighlighted)
            {
                SexyColor sexyColor2 = new SexyColor(255, 255, 255, 196);
                if (sexyColor2 != reanimation.mExtraAdditiveColor)
                {
                    flag = true;
                    reanimation.mExtraAdditiveColor = sexyColor2;
                }
                if (!reanimation.mEnableExtraAdditiveDraw)
                {
                    flag = true;
                    reanimation.mEnableExtraAdditiveDraw = true;
                }
                if (mImitaterType == SeedType.Imitater)
                {
                    sexyColor2 = new SexyColor(255, 255, 255, 92);
                    if (sexyColor2 != reanimation.mExtraAdditiveColor)
                    {
                        flag = true;
                        reanimation.mExtraAdditiveColor = sexyColor2;
                    }
                }
            }
            else if (mBeghouledFlashCountdown > 0)
            {
                int theAlpha = TodCommon.TodAnimateCurve(50, 0, mBeghouledFlashCountdown % 50, 0, 128, TodCurves.Bounce);
                SexyColor sexyColor3 = new SexyColor(255, 255, 255, theAlpha);
                if (sexyColor3 != reanimation.mExtraAdditiveColor)
                {
                    flag = true;
                    reanimation.mExtraAdditiveColor = sexyColor3;
                }
                if (!reanimation.mEnableExtraAdditiveDraw)
                {
                    flag = true;
                    reanimation.mEnableExtraAdditiveDraw = true;
                }
            }
            else if (mEatenFlashCountdown > 0)
            {
                int maxNum = 255;
                if (mImitaterType == SeedType.Imitater)
                {
                    maxNum = 128;
                }
                int num = TodCommon.ClampInt(mEatenFlashCountdown * 3, 0, maxNum);
                SexyColor sexyColor4 = new SexyColor(num, num, num);
                if (sexyColor4 != reanimation.mExtraAdditiveColor)
                {
                    flag = true;
                    reanimation.mExtraAdditiveColor = sexyColor4;
                }
                if (!reanimation.mEnableExtraAdditiveDraw)
                {
                    flag = true;
                    reanimation.mEnableExtraAdditiveDraw = true;
                }
            }
            else if (reanimation.mEnableExtraAdditiveDraw)
            {
                flag = true;
                reanimation.mEnableExtraAdditiveDraw = false;
            }
            if (mBeghouledFlashCountdown > 0)
            {
                int theAlpha2 = TodCommon.TodAnimateCurve(50, 0, mBeghouledFlashCountdown % 50, 0, 128, TodCurves.Bounce);
                SexyColor sexyColor5 = new SexyColor(255, 255, 255, theAlpha2);
                if (sexyColor5 != reanimation.mExtraOverlayColor)
                {
                    flag = true;
                    reanimation.mExtraOverlayColor = sexyColor5;
                }
            }
            else if (reanimation.mEnableExtraOverlayDraw)
            {
                flag = true;
                reanimation.mEnableExtraOverlayDraw = false;
            }
            /*if (mSeedType != SeedType.SEED_SUNFLOWER && mSeedType != SeedType.SEED_TWINSUNFLOWER && mSeedType != SeedType.SEED_SUNSHROOM && reanimation.mEnableExtraAdditiveDraw)
            {
                flag = true;
                reanimation.mEnableExtraAdditiveDraw = false;
            }*/
            if (flag)
            {
                reanimation.PropogateColorToAttachments();
            }
        }

        public bool IsUpgradableTo(SeedType aUpdatedType)
        {
            if (aUpdatedType == SeedType.Gatlingpea && mSeedType == SeedType.Repeater)
            {
                return true;
            }
            if (aUpdatedType == SeedType.Wintermelon && mSeedType == SeedType.Melonpult)
            {
                return true;
            }
            if (aUpdatedType == SeedType.Twinsunflower && mSeedType == SeedType.Sunflower)
            {
                return true;
            }
            if (aUpdatedType == SeedType.Spikerock && mSeedType == SeedType.Spikeweed)
            {
                return true;
            }
            if (aUpdatedType == SeedType.Cobcannon && mSeedType == SeedType.Kernelpult)
            {
                if (mBoard.IsValidCobCannonSpot(mPlantCol, mRow))
                {
                    return true;
                }
            }
            else
            {
                if (aUpdatedType == SeedType.GoldMagnet && mSeedType == SeedType.Magnetshroom)
                {
                    return true;
                }
                if (aUpdatedType == SeedType.Gloomshroom && mSeedType == SeedType.Fumeshroom)
                {
                    return true;
                }
                if (aUpdatedType == SeedType.Cattail && mSeedType == SeedType.Lilypad)
                {
                    Plant topPlantAt = mBoard.GetTopPlantAt(mPlantCol, mRow, TopPlant.OnlyNormalPosition);
                    if (topPlantAt == null || topPlantAt.mSeedType != SeedType.Cattail)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsPartOfUpgradableTo(SeedType aUpdatedType)
        {
            if (aUpdatedType == SeedType.Cobcannon && mSeedType == SeedType.Kernelpult)
            {
                return mBoard.IsValidCobCannonSpot(mPlantCol, mRow) || mBoard.IsValidCobCannonSpot(mPlantCol - 1, mRow);
            }
            return IsUpgradableTo(aUpdatedType);
        }

        public void UpdateCobCannon()//3update
        {
            if (mState == PlantState.CobcannonArming)
            {
                if (mStateCountdown <= 0)
                {
                    mState = PlantState.CobcannonLoading;
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_charge, ReanimLoopType.PlayOnceAndHold, 20, 12f);
                    return;
                }
            }
            else if (mState == PlantState.CobcannonLoading)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.ShouldTriggerTimedEvent(0.5f))
                {
                    mApp.PlayFoley(FoleyType.Shoop);
                }
                if (reanimation.mLoopCount > 0)
                {
                    mState = PlantState.CobcannonReady;
                    PlayIdleAnim(12f);
                    return;
                }
            }
            else
            {
                if (mState == PlantState.CobcannonReady)
                {
                    Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
                    ReanimatorTrackInstance trackInstanceByName = reanimation2.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_cobcannon_cob);
                    trackInstanceByName.mTrackColor = TodCommon.GetFlashingColor(mBoard.mMainCounter, 75);
                    return;
                }
                if (mState == PlantState.CobcannonFiring)
                {
                    Reanimation reanimation3 = mApp.ReanimationGet(mBodyReanimID);
                    if (reanimation3.ShouldTriggerTimedEvent(0.48f))
                    {
                        mApp.PlayFoley(FoleyType.Coblaunch);
                    }
                }
            }
        }

        public void CobCannonFire(int theTargetX, int theTargetY)
        {
            Debug.ASSERT(mState == PlantState.CobcannonReady);
            mState = PlantState.CobcannonFiring;
            mShootingCounter = 184;
            PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.PlayOnceAndHold, 20, 12f);
            mTargetX = theTargetX - 47;
            mTargetY = theTargetY;
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_cobcannon_cob);
            trackInstanceByName.mTrackColor = SexyColor.White;
        }

        public void UpdateGoldMagnetShroom()//3update
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            bool flag = false;
            for (int i = 0; i < GameConstants.MAX_MAGNET_ITEMS; i++)
            {
                MagnetItem magnetItem = mMagnetItems[i];
                if (magnetItem.mItemType != MagnetItemType.None)
                {
                    SexyVector2 sexyVector = new SexyVector2(mX + magnetItem.mDestOffsetX - magnetItem.mPosX, mY + magnetItem.mDestOffsetY - magnetItem.mPosY);
                    float num = sexyVector.Magnitude();
                    if (num < 20f)
                    {
                        CoinType theType = CoinType.Almanac;
                        if (magnetItem.mItemType == MagnetItemType.SilverCoin)
                        {
                            theType = CoinType.Silver;
                        }
                        else if (magnetItem.mItemType == MagnetItemType.GoldCoin)
                        {
                            theType = CoinType.Gold;
                        }
                        else if (magnetItem.mItemType == MagnetItemType.Diamond)
                        {
                            theType = CoinType.Diamond;
                        }
                        else
                        {
                            Debug.ASSERT(false);
                        }
                        int coinValue = Coin.GetCoinValue(theType);
                        mApp.mPlayerInfo.AddCoins(coinValue);
                        mBoard.mCoinsCollected += coinValue;
                        mApp.PlayFoley(FoleyType.Coin);
                        magnetItem.mItemType = MagnetItemType.None;
                    }
                    else
                    {
                        float num2 = TodCommon.TodAnimateCurveFloatTime(300f, 0f, num, 0.02f, 0.05f, TodCurves.Linear);
                        magnetItem.mPosX += sexyVector.x * num2;
                        magnetItem.mPosY += sexyVector.y * num2;
                        flag = true;
                    }
                }
            }
            if (mState == PlantState.MagnetshroomCharging)
            {
                if (mStateCountdown <= 0)
                {
                    mState = PlantState.Ready;
                }
                return;
            }
            if (mState == PlantState.MagnetshroomSucking)
            {
                if (reanimation.ShouldTriggerTimedEvent(0.4f))
                {
                    mApp.PlayFoley(FoleyType.Magnetshroom);
                    GoldMagnetFindTargets();
                }
                if (reanimation.mLoopCount > 0 && !flag)
                {
                    PlayIdleAnim(14f);
                    mState = PlantState.MagnetshroomCharging;
                    mStateCountdown = TodCommon.RandRangeInt(200, 300);
                }
                return;
            }
            if (IsAGoldMagnetAboutToSuck())
            {
                return;
            }
            if (RandomNumbers.NextNumber(50) != 0)
            {
                return;
            }
            Coin coin = FindGoldMagnetTarget();
            if (coin != null)
            {
                mBoard.ShowCoinBank();
                mState = PlantState.MagnetshroomSucking;
                PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_attract, ReanimLoopType.PlayOnceAndHold, 20, 12f);
            }
        }

        public bool IsOnBoard()
        {
            if (!mIsOnBoard)
            {
                return false;
            }
            Debug.ASSERT(mBoard != null);
            return true;
        }

        public void RemoveEffects()
        {
            mApp.RemoveParticle(mParticleID);
            mParticleID = null;
            mApp.RemoveReanimation(ref mBodyReanimID);
            mApp.RemoveReanimation(ref mHeadReanimID);
            mApp.RemoveReanimation(ref mHeadReanimID2);
            mApp.RemoveReanimation(ref mHeadReanimID3);
            mApp.RemoveReanimation(ref mLightReanimID);
            mApp.RemoveReanimation(ref mBlinkReanimID);
            mApp.RemoveReanimation(ref mSleepingReanimID);
        }

        public void UpdateCoffeeBean()//3update
        {
            if (mState == PlantState.Doingspecial)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.mLoopCount > 0)
                {
                    Die();
                }
            }
        }

        public void UpdateUmbrella()//3update
        {
            if (mState == PlantState.UmbrellaTriggered)
            {
                if (mStateCountdown <= 0)
                {
                    mRenderOrder = Board.MakeRenderOrder(RenderLayer.Projectile, mRow + 1, 0);
                    mState = PlantState.UmbrellaReflecting;
                    return;
                }
            }
            else if (mState == PlantState.UmbrellaReflecting)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.mLoopCount > 0)
                {
                    PlayIdleAnim(0f);
                    mState = PlantState.Notready;
                    mRenderOrder = CalcRenderOrder();
                }
            }
        }

        public void EndBlink()
        {
            if (mBlinkReanimID == null)
            {
                return;
            }
            mApp.RemoveReanimation(ref mBlinkReanimID);
            mBlinkReanimID = null;
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation != null)
            {
                reanimation.AssignRenderGroupToPrefix("anim_eye", 0);
            }
        }

        public void AnimateGarlic()//3update
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            string reanimTrackId_anim_face = GlobalMembersReanimIds.ReanimTrackId_anim_face;
            Image imageOverride = reanimation.GetImageOverride(reanimTrackId_anim_face);
            if (mPlantHealth < mPlantMaxHealth / 3)
            {
                if (imageOverride != AtlasResources.IMAGE_REANIM_GARLIC_BODY3)
                {
                    reanimation.SetImageOverride(reanimTrackId_anim_face, AtlasResources.IMAGE_REANIM_GARLIC_BODY3);
                    reanimation.AssignRenderGroupToPrefix("Garlic_stem", -1);
                    return;
                }
            }
            else if (mPlantHealth < mPlantMaxHealth * 2 / 3)
            {
                if (imageOverride != AtlasResources.IMAGE_REANIM_GARLIC_BODY2)
                {
                    reanimation.SetImageOverride(reanimTrackId_anim_face, AtlasResources.IMAGE_REANIM_GARLIC_BODY2);
                    return;
                }
            }
            else
            {
                Image theImage = null;
                reanimation.SetImageOverride(reanimTrackId_anim_face, theImage);
            }
        }

        public Coin FindGoldMagnetTarget()
        {
            Coin coin = null;
            float num = 0f;
            Coin coin2 = null;
            while (mBoard.IterateCoins(ref coin2))
            {
                if ((coin2.mType == CoinType.Silver || coin2.mType == CoinType.Gold || coin2.mType == CoinType.Diamond) && coin2.mCoinMotion != CoinMotion.FromPresent && !coin2.mIsBeingCollected && coin2.mCoinAge >= 50)
                {
                    float num2 = TodCommon.Distance2D(mX + 40f, mY + 40f, coin2.mPosX + coin2.mWidth / 2, coin2.mPosY + coin2.mHeight / 2);
                    if (coin == null || num2 < num)
                    {
                        coin = coin2;
                        num = num2;
                    }
                }
            }
            return coin;
        }

        public void SpikeweedAttack()
        {
            Debug.ASSERT(IsSpiky());
            if (mState != PlantState.SpikeweedAttacking)
            {
                PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_attack, ReanimLoopType.PlayOnceAndHold, 20, 18f);
                mApp.PlaySample(Resources.SOUND_THROW);
                mState = PlantState.SpikeweedAttacking;
                mStateCountdown = 99;
            }
        }

        public void ImitaterMorph()
        {
            Die();
            Plant plant = mBoard.AddPlant(mPlantCol, mRow, mImitaterType, SeedType.Imitater);
            FilterEffectType aFilterEffect = FilterEffectType.WashedOut;
            if (mImitaterType == SeedType.Hypnoshroom || mImitaterType == SeedType.Squash || mImitaterType == SeedType.Potatomine || mImitaterType == SeedType.Garlic || mImitaterType == SeedType.Lilypad)
            {
                aFilterEffect = FilterEffectType.LessWashedOut;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(plant.mBodyReanimID);
            if (reanimation != null)
            {
                reanimation.mFilterEffect = aFilterEffect;
            }
            Reanimation reanimation2 = mApp.ReanimationTryToGet(plant.mHeadReanimID);
            if (reanimation2 != null)
            {
                reanimation2.mFilterEffect = aFilterEffect;
            }
            Reanimation reanimation3 = mApp.ReanimationTryToGet(plant.mHeadReanimID2);
            if (reanimation3 != null)
            {
                reanimation3.mFilterEffect = aFilterEffect;
            }
            Reanimation reanimation4 = mApp.ReanimationTryToGet(plant.mHeadReanimID3);
            if (reanimation4 != null)
            {
                reanimation4.mFilterEffect = aFilterEffect;
            }
        }

        public void UpdateImitater()//3update
        {
            if (mState != PlantState.ImitaterMorphing)
            {
                if (mStateCountdown <= 0)
                {
                    mState = PlantState.ImitaterMorphing;
                    PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_explode, ReanimLoopType.PlayOnceAndHold, 0, 26f);
                    return;
                }
            }
            else
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.ShouldTriggerTimedEvent(0.8f))
                {
                    mApp.AddTodParticle(mX + 40, mY + 40, 400000, ParticleEffect.ImitaterMorph);
                }
                if (reanimation.mLoopCount > 0)
                {
                    ImitaterMorph();
                }
            }
        }

        public void UpdateReanim()//3update
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            UpdateReanimColor();
            float num = mShakeOffsetX;
            float num2 = mShakeOffsetY + Plant.PlantDrawHeightOffset(mBoard, this, mSeedType, mPlantCol, mRow);
            float num3 = 1f;
            float num4 = 1f;
            if (mApp.mGameMode == GameMode.ChallengeBigTime && (mSeedType == SeedType.Wallnut || mSeedType == SeedType.Sunflower || mSeedType == SeedType.Marigold))
            {
                num3 = 1.5f;
                num4 = 1.5f;
                num += -20f;
                num2 += -40f;
            }
            if (mSeedType == SeedType.GiantWallnut)
            {
                num3 = 2f;
                num4 = 2f;
                num += -76f;
                num2 += -64f;
            }
            if (mSeedType == SeedType.InstantCoffee)
            {
                num3 = 0.8f;
                num4 = 0.8f;
                num += 12f;
                num2 += 10f;
            }
            if (mSeedType == SeedType.Potatomine)
            {
                num3 = 0.8f;
                num4 = 0.8f;
                num += 12f;
                num2 += 12f;
            }
            if (mState == PlantState.GravebusterEating)
            {
                num2 += TodCommon.TodAnimateCurveFloat(400, 0, mStateCountdown, 0f, 30f, TodCurves.Linear);
            }
            if (mWakeUpCounter > 0)
            {
                float num5 = TodCommon.TodAnimateCurveFloat(70, 0, mWakeUpCounter, 1f, 0.8f, TodCurves.EaseSinWave);
                num4 *= num5;
                num2 += 80f - num5 * 80f;
            }
            reanimation.Update();
            if (mSeedType == SeedType.Leftpeater)
            {
                num += 80f * num3;
                num3 *= -1f;
            }
            if (mPottedPlantIndex != -1)
            {
                PottedPlant pottedPlant = mApp.mPlayerInfo.mPottedPlant[mPottedPlantIndex];
                if (pottedPlant.mFacing == PottedPlant.FacingDirection.Left)
                {
                    num += 80f * num3;
                    num3 *= -1f;
                }
                float num6;
                float thePositionEnd;
                float num7;
                float thePositionEnd2;
                float num8;
                float thePositionEnd3;
                if (pottedPlant.mPlantAge == PottedPlantAge.Small)
                {
                    num6 = 20f;
                    thePositionEnd = num6;
                    num7 = 40f;
                    thePositionEnd2 = num7;
                    num8 = 0.5f;
                    thePositionEnd3 = num8;
                }
                else if (pottedPlant.mPlantAge == PottedPlantAge.Medium)
                {
                    num6 = 20f;
                    thePositionEnd = 10f;
                    num7 = 40f;
                    thePositionEnd2 = 20f;
                    num8 = 0.5f;
                    thePositionEnd3 = 0.75f;
                }
                else
                {
                    num6 = 10f;
                    thePositionEnd = 0f;
                    num7 = 20f;
                    thePositionEnd2 = 0f;
                    num8 = 0.75f;
                    thePositionEnd3 = 1f;
                }
                float num9 = TodCommon.TodAnimateCurveFloat(100, 0, mStateCountdown, num6, thePositionEnd, TodCurves.Linear);
                float num10 = TodCommon.TodAnimateCurveFloat(100, 0, mStateCountdown, num7, thePositionEnd2, TodCurves.Linear);
                float num11 = TodCommon.TodAnimateCurveFloat(100, 0, mStateCountdown, num8, thePositionEnd3, TodCurves.Linear);
                num += num9 * num3;
                num2 += num10 * num4;
                num3 *= num11;
                num4 *= num11;
                num += mApp.mZenGarden.ZenPlantOffsetX(pottedPlant);
                num2 += mApp.mZenGarden.PlantPottedDrawHeightOffset(mSeedType, num4, false);
            }
            reanimation.SetPosition(num * Constants.S, num2 * Constants.S);
            reanimation.OverrideScale(num3, num4);
        }

        public void SpikeRockTakeDamage()
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            SpikeweedAttack();
            mPlantHealth -= 50;
            if (mPlantHealth <= 300)
            {
                reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_bigspike3, -1);
            }
            if (mPlantHealth <= 150)
            {
                reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_bigspike2, -1);
            }
            if (mPlantHealth <= 0)
            {
                mApp.PlayFoley(FoleyType.Squish);
                Die();
            }
        }

        public bool IsSpiky()
        {
            return mSeedType == SeedType.Spikeweed || mSeedType == SeedType.Spikerock;
        }

        public static void PreloadPlantResources(SeedType theSeedType)
        {
            PlantDefinition plantDefinition = Plant.GetPlantDefinition(theSeedType);
            if (plantDefinition.mReanimationType != ReanimationType.None)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(plantDefinition.mReanimationType, true);
            }
            if (theSeedType == SeedType.Cherrybomb)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.ZombieCharred, true);
            }
            if (theSeedType == SeedType.Jalapeno)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.JalapenoFire, true);
            }
            if (theSeedType == SeedType.Torchwood)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.FirePea, true);
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.JalapenoFire, true);
            }
            if (Plant.IsNocturnal(theSeedType))
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.Sleeping, true);
            }
        }

        public bool IsInPlay()
        {
            return IsOnBoard() && mApp.mGameMode != GameMode.ChallengeZenGarden && mApp.mGameMode != GameMode.TreeOfWisdom;
        }

        public void UpdateNeedsFood()
        {
        }

        public void PlayIdleAnim(float theRate)
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 20, theRate);
            if (mApp.IsIZombieLevel())
            {
                reanimation.mAnimRate = 0f;
            }
        }

        public void UpdateFlowerPot()//3update
        {
            if (mState == PlantState.FlowerpotInvulnerable && mStateCountdown <= 0)
            {
                mState = PlantState.Notready;
            }
        }

        public void UpdateLilypad()//3update
        {
            if (mState == PlantState.LilypadInvulnerable && mStateCountdown <= 0)
            {
                mState = PlantState.Notready;
            }
        }

        public void GoldMagnetFindTargets()
        {
            for (;;)
            {
                MagnetItem freeMagnetItem = GetFreeMagnetItem();
                if (freeMagnetItem == null)
                {
                    break;
                }
                Coin coin = FindGoldMagnetTarget();
                if (coin == null)
                {
                    return;
                }
                freeMagnetItem.mPosX = coin.mPosX + 15f;
                freeMagnetItem.mPosY = coin.mPosY + 15f;
                freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(20f, 40f);
                freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-20f, 0f) + 20f;
                if (coin.mType == CoinType.Silver)
                {
                    freeMagnetItem.mItemType = MagnetItemType.SilverCoin;
                }
                else if (coin.mType == CoinType.Gold)
                {
                    freeMagnetItem.mItemType = MagnetItemType.GoldCoin;
                }
                else if (coin.mType == CoinType.Diamond)
                {
                    freeMagnetItem.mItemType = MagnetItemType.Diamond;
                }
                else
                {
                    Debug.ASSERT(false);
                }
                coin.Die();
            }
        }

        public bool IsAGoldMagnetAboutToSuck()
        {
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && !plant.NotOnGround() && plant.mSeedType == SeedType.GoldMagnet && plant.mState == PlantState.MagnetshroomSucking)
                {
                    Reanimation reanimation = mApp.ReanimationGet(plant.mBodyReanimID);
                    if (reanimation.mAnimTime < 0.5f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DrawMagnetItemsOnTop()
        {
            if (mSeedType == SeedType.GoldMagnet)
            {
                for (int i = 0; i < GameConstants.MAX_MAGNET_ITEMS; i++)
                {
                    MagnetItem magnetItem = mMagnetItems[i];
                    if (magnetItem.mItemType != MagnetItemType.None)
                    {
                        return true;
                    }
                }
                return false;
            }
            if (mSeedType == SeedType.Magnetshroom)
            {
                for (int j = 0; j < GameConstants.MAX_MAGNET_ITEMS; j++)
                {
                    MagnetItem magnetItem2 = mMagnetItems[j];
                    if (magnetItem2.mItemType != MagnetItemType.None)
                    {
                        SexyVector2 sexyVector = new SexyVector2(mX + magnetItem2.mDestOffsetX - magnetItem2.mPosX, mY + magnetItem2.mDestOffsetY - magnetItem2.mPosY);
                        float num = sexyVector.Magnitude();
                        if (num > 20f)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
        }

        public void checkForPlantAchievements()
        {
            if (mBoard == null || !IsOnBoard())
            {
                return;
            }
            if (mSeedType == SeedType.Twinsunflower)
            {
                int count = mBoard.CountPlantByType(mSeedType);
                if (count >= 10)
                {
                    mBoard.GrantAchievement(AchievementId.FlowerPower, true);
                }
            }
            if (mSeedType == SeedType.Cobcannon)
            {
                int count = mBoard.CountPlantByType(mSeedType);
                if (count >= 5)
                {
                    mBoard.GrantAchievement(AchievementId.Defcorn5, true);
                }
            }
            if (mSeedType >= SeedType.Peashooter && mSeedType < SeedType.ExplodeONut)
            {
                mApp.mPlayerInfo.mPlantTypesUsed[(int)mSeedType] = true;
                int num = 0;
                while (num < mApp.mPlayerInfo.mPlantTypesUsed.Length && mApp.mPlayerInfo.mPlantTypesUsed[num])
                {
                    num++;
                }
            }
            if (mBoard.StageHasFog() && (mSeedType == SeedType.Plantern || mSeedType == SeedType.Blover))
            {
                mBoard.mPlanternOrBloverUsed = true;
            }
            if (mSeedType == SeedType.Wallnut || mSeedType == SeedType.Tallnut || mSeedType == SeedType.Pumpkinshell)
            {
                mBoard.mNutsUsed = true;
            }
            if (mSeedType == SeedType.Wintermelon)
            {
                uint num2 = 0U;
                int count = mBoard.mPlants.Count;
                for (int i = 0; i < count; i++)
                {
                    Plant plant = mBoard.mPlants[i];
                    if (!plant.mDead && plant.mSeedType == SeedType.Wintermelon)
                    {
                        TodCommon.SetBit(ref num2, plant.mRow, 1);
                    }
                }
                if (mBoard.StageHas6Rows())
                {
                    if (num2 == 63)
                    {
                        mBoard.GrantAchievement(AchievementId.MelonYLane, true);
                    }
                }
                else
                {
                    if (num2 == 31)
                    {
                        mBoard.GrantAchievement(AchievementId.MelonYLane, true);
                    }
                }
            }
            if (mBoard.StageHasPool()
                 && (mSeedType == SeedType.Lilypad
                 || mSeedType == SeedType.Tanglekelp
                 || mSeedType == SeedType.Seashroom
                 || mSeedType == SeedType.Cattail))
            {
                mBoard.mPoolPlantsUsed = true;
            }
            if (mSeedType == SeedType.Jalapeno)
            {
                mBoard.mJalapenoUsed = true;
            }
            if (mSeedType == SeedType.Tallnut)
            {
                mBoard.mTallNutUsed = true;
            }
            if (mSeedType == SeedType.Magnetshroom)
            {
                mBoard.mMagnetShroomUsed = true;
            }
        }

        public static PlantDefinition GetPlantDefinition(SeedType theSeedtype)
        {
            Debug.ASSERT(theSeedtype >= SeedType.Peashooter && theSeedtype < SeedType.SeedTypeCount);
            Debug.ASSERT(GameConstants.gPlantDefs[(int)theSeedtype].mSeedType == theSeedtype);
            return GameConstants.gPlantDefs[(int)theSeedtype];
        }

        public override bool SaveToFile(Sexy.Buffer b)
        {
            base.SaveToFile(b);
            b.WriteString(lastPlayedBodyReanim_Name);
            b.WriteFloat(lastPlayedBodyReanim_AnimRate);
            b.WriteByte(lastPlayedBodyReanim_BlendTime);
            b.WriteLong((int)lastPlayedBodyReanim_Type);
            b.WriteLong((int)mSeedType);
            b.WriteLong((int)mImitaterType);
            b.WriteBoolean(mIsOnBoard);
            b.WriteLong(mAnimCounter);
            b.WriteBoolean(mAnimPing);
            b.WriteLong(mBeghouledFlashCountdown);
            b.WriteLong(mBlinkCountdown);
            b.WriteBoolean(mDead);
            b.WriteLong(mDisappearCountdown);
            b.WriteLong(mDoSpecialCountdown);
            b.WriteLong(mEatenFlashCountdown);
            b.WriteLong(mFrame);
            b.WriteLong(mFrameLength);
            b.WriteBoolean(mHighlighted);
            b.WriteBoolean(mInFlowerPot);
            b.WriteBoolean(mIsAsleep);
            b.WriteLong(mLaunchCounter);
            b.WriteLong(mLaunchRate);
            for (int i = 0; i < mMagnetItems.Length; i++)
            {
                b.WriteBoolean(mMagnetItems[i] != null);
                if (mMagnetItems[i] != null)
                {
                    mMagnetItems[i].SaveToFile(b);
                }
            }
            b.WriteLong(mNumFrames);
            b.WriteLong((int)mOnBungeeState);
            b.WriteRect(mPlantAttackRect);
            b.WriteLong(mPlantCol);
            b.WriteLong(mPlantHealth);
            b.WriteLong(mPlantMaxHealth);
            b.WriteRect(mPlantRect);
            b.WriteLong(mPottedPlantIndex);
            b.WriteLong(mRecentlyEatenCountdown);
            b.WriteFloat(mShakeOffsetX);
            b.WriteFloat(mShakeOffsetY);
            b.WriteLong(mShootingCounter);
            b.WriteBoolean(mSquished);
            b.WriteLong(mStartRow);
            b.WriteLong((int)mState);
            b.WriteLong(mStateCountdown);
            b.WriteLong(mSubclass);
            b.WriteLong(mTargetX);
            b.WriteLong(mTargetY);
            GameObject.SaveId(mTargetZombieID, b);
            b.WriteLong(mWakeUpCounter);
            b.WriteFloat(mBodyReanimID?.mAnimTime ?? 0f);
            return true;
        }

        public override bool LoadFromFile(Sexy.Buffer b)
        {
            base.LoadFromFile(b);
            lastPlayedBodyReanim_Name = b.ReadString();
            lastPlayedBodyReanim_AnimRate = b.ReadFloat();
            lastPlayedBodyReanim_BlendTime = b.ReadByte();
            lastPlayedBodyReanim_Type = (ReanimLoopType)b.ReadLong();
            mSeedType = (SeedType)b.ReadLong();
            mImitaterType = (SeedType)b.ReadLong();
            mIsOnBoard = b.ReadBoolean();
            mBoard = GlobalStaticVars.gLawnApp.mBoard;
            int x = mX;
            int y = mY;
            PlantInitialize(mBoard.PixelToGridX(mX, mY), mRow, mSeedType, mImitaterType);
            mX = x;
            mY = y;
            mAnimCounter = b.ReadLong();
            mAnimPing = b.ReadBoolean();
            mBeghouledFlashCountdown = b.ReadLong();
            mBlinkCountdown = b.ReadLong();
            mDead = b.ReadBoolean();
            mDisappearCountdown = b.ReadLong();
            mDoSpecialCountdown = b.ReadLong();
            mEatenFlashCountdown = b.ReadLong();
            mFrame = b.ReadLong();
            mFrameLength = b.ReadLong();
            mHighlighted = b.ReadBoolean();
            mInFlowerPot = b.ReadBoolean();
            mIsAsleep = b.ReadBoolean();
            mLaunchCounter = b.ReadLong();
            mLaunchRate = b.ReadLong();
            for (int i = 0; i < mMagnetItems.Length; i++)
            {
                bool flag = b.ReadBoolean();
                if (flag)
                {
                    mMagnetItems[i] = new MagnetItem();
                    mMagnetItems[i].LoadFromFile(b);
                }
                else
                {
                    mMagnetItems[i] = null;
                }
            }
            mNumFrames = b.ReadLong();
            mOnBungeeState = (PlantOnBungeeState)b.ReadLong();
            mPlantAttackRect = b.ReadRect();
            mPlantCol = b.ReadLong();
            mPlantHealth = b.ReadLong();
            mPlantMaxHealth = b.ReadLong();
            mPlantRect = b.ReadRect();
            mPottedPlantIndex = b.ReadLong();
            mRecentlyEatenCountdown = b.ReadLong();
            mShakeOffsetX = b.ReadFloat();
            mShakeOffsetY = b.ReadFloat();
            mShootingCounter = b.ReadLong();
            mSquished = b.ReadBoolean();
            mStartRow = b.ReadLong();
            mState = (PlantState)b.ReadLong();
            mStateCountdown = b.ReadLong();
            mSubclass = b.ReadLong();
            mTargetX = b.ReadLong();
            mTargetY = b.ReadLong();
            mTargetZombieIDSaved = GameObject.LoadId(b);
            mWakeUpCounter = b.ReadLong();
            mBodyReanimID.mAnimTime = b.ReadFloat();
            return true;
        }

        public override void LoadingComplete()
        {
            base.LoadingComplete();
            mTargetZombieID = (GameObject.GetObjectById(mTargetZombieIDSaved) as Zombie);
            if (!mIsAsleep && mSleepingReanimID != null)
            {
                mIsAsleep = true;
                SetSleeping(false);
            }
            UpdateReanim();
            UpdateReanim();
            float anAnimTime = mBodyReanimID.mAnimTime;
            if (!string.IsNullOrEmpty(lastPlayedBodyReanim_Name))
            {
                PlayBodyReanim(lastPlayedBodyReanim_Name, lastPlayedBodyReanim_Type, 0, lastPlayedBodyReanim_AnimRate);
            }
            mBodyReanimID.mAnimTime = anAnimTime;
        }

        public static float PlantDrawHeightOffset(Board theBoard, Plant thePlant, SeedType theSeedType, int theCol, int theRow)
        {
            float num = 0f;
            bool flag = false;
            if (Plant.IsFlying(theSeedType))
            {
                flag = false;
            }
            else if (theBoard == null)
            {
                if (theSeedType == SeedType.Lilypad || theSeedType == SeedType.Tanglekelp || theSeedType == SeedType.Seashroom || theSeedType == SeedType.Cattail)
                {
                    flag = true;
                }
            }
            else if (theBoard.IsPoolSquare(theCol, theRow))
            {
                flag = true;
            }
            if (flag)
            {
                int num2;
                if (theBoard != null)
                {
                    num2 = theBoard.mMainCounter;
                }
                else
                {
                    num2 = GlobalStaticVars.gLawnApp.mAppCounter;
                }
                float num3 = theRow * 3.1415927f + theCol * 3.1415927f * 0.25f;
                float num4 = num2 * 3.1415927f * 2f / 200f;
                float num5 = (float)Math.Sin(num3 + num4) * 2f;
                num += num5;
            }
            if (theBoard != null && (thePlant == null || !thePlant.mSquished) && (thePlant?.mInFlowerPot ?? false))
            {
                Plant flowerPotAt = theBoard.GetFlowerPotAt(theCol, theRow);
                if (flowerPotAt != null && !flowerPotAt.mSquished && theSeedType != SeedType.Flowerpot)
                {
                    num += Plant.PlantFlowerPotHeightOffset(theSeedType, 1f);
                }
            }
            if (theSeedType == SeedType.Flowerpot)
            {
                num += 26f;
            }
            else if (theSeedType == SeedType.Lilypad)
            {
                num += 25f;
            }
            else if (theSeedType == SeedType.Starfruit)
            {
                num += 10f;
            }
            else if (theSeedType == SeedType.Tanglekelp)
            {
                num += 24f;
            }
            else if (theSeedType == SeedType.Seashroom)
            {
                num += 28f;
            }
            else if (theSeedType == SeedType.InstantCoffee)
            {
                num -= 20f;
            }
            else if (Plant.IsFlying(theSeedType))
            {
                num -= 30f;
            }
            else if (theSeedType != SeedType.Cactus)
            {
                if (theSeedType == SeedType.Pumpkinshell)
                {
                    num += 15f;
                }
                else if (theSeedType == SeedType.Puffshroom)
                {
                    num += 5f;
                }
                else if (theSeedType == SeedType.Scaredyshroom)
                {
                    num -= 14f;
                }
                else if (theSeedType == SeedType.Gravebuster)
                {
                    num -= 40f;
                }
                else if (theSeedType == SeedType.Spikeweed || theSeedType == SeedType.Spikerock)
                {
                    int num6 = 4;
                    if (theBoard != null && theBoard.StageHas6Rows())
                    {
                        num6 = 5;
                    }
                    if (theSeedType == SeedType.Spikerock)
                    {
                        num += 6f;
                    }
                    if (theBoard != null && theBoard.GetFlowerPotAt(theCol, theRow) != null && GlobalStaticVars.gLawnApp.mGameMode != GameMode.ChallengeZenGarden)
                    {
                        num += 5f;
                    }
                    else if (theBoard != null && theBoard.StageHasRoof())
                    {
                        num += 15f;
                    }
                    else if (theBoard != null && theBoard.IsPoolSquare(theCol, theRow))
                    {
                        num += 0f;
                    }
                    else if (theRow == num6 && theCol >= 7 && theBoard.StageHas6Rows())
                    {
                        num += 6f;
                    }
                    else if (theRow == num6)
                    {
                        num += 6f;
                    }
                    else
                    {
                        num += 15f;
                    }
                }
            }
            if (theBoard != null && theBoard.StageHasRoof())
            {
                num -= 12f;
            }
            return num;
        }

        public static float PlantFlowerPotHeightOffset(SeedType theSeedType, float theFlowerPotScale)
        {
            float num = -5f * theFlowerPotScale;
            float num2 = 0f;
            if (theSeedType == SeedType.Chomper || theSeedType == SeedType.Plantern)
            {
                num -= 5f;
            }
            else if (theSeedType == SeedType.Scaredyshroom)
            {
                num += 5f;
                num2 += -8f;
            }
            else if (theSeedType == SeedType.Sunshroom || theSeedType == SeedType.Puffshroom)
            {
                num2 += -4f;
            }
            else if (theSeedType == SeedType.Hypnoshroom)
            {
                num2 += -8f;
            }
            else if (theSeedType == SeedType.Magnetshroom)
            {
                num2 += -8f;
            }
            else if (theSeedType == SeedType.Peashooter || theSeedType == SeedType.Repeater || theSeedType == SeedType.Leftpeater || theSeedType == SeedType.Snowpea || theSeedType == SeedType.Threepeater || theSeedType == SeedType.Sunflower || theSeedType == SeedType.Marigold)
            {
                num2 += -8f;
            }
            else if (theSeedType == SeedType.Cabbagepult || theSeedType == SeedType.Melonpult)
            {
                num2 += -8f;
            }
            else if (theSeedType == SeedType.Tanglekelp)
            {
                num2 += -8f;
            }
            else if (theSeedType == SeedType.Blover)
            {
                num2 += -8f;
            }
            else if (theSeedType == SeedType.Spikeweed)
            {
                num2 += -8f;
            }
            else if (theSeedType == SeedType.Seashroom)
            {
                num2 += -4f;
            }
            else if (theSeedType == SeedType.Potatomine)
            {
                num2 += -4f;
            }
            else if (theSeedType == SeedType.Lilypad)
            {
                num2 += -16f;
            }
            else if (theSeedType == SeedType.InstantCoffee)
            {
                num2 += -20f;
            }
            num2 = Constants.InvertAndScale(num2);
            return num + (-num2 + num2 * theFlowerPotScale);
        }

        public SeedType mSeedType;

        public int mPlantCol;

        public int mAnimCounter;

        public int mFrame;

        public int mFrameLength;

        public int mNumFrames;

        public PlantState mState;

        public int mPlantHealth;

        public int mPlantMaxHealth;

        public int mSubclass;

        public int mDisappearCountdown;

        public int mDoSpecialCountdown;

        public int mStateCountdown;

        public int mLaunchCounter;

        public int mLaunchRate;

        public TRect mPlantRect = default(TRect);

        public TRect mPlantAttackRect = default(TRect);

        public int mTargetX;

        public int mTargetY;

        public int mStartRow;

        public TodParticleSystem mParticleID;

        public int mShootingCounter;

        public Reanimation mBodyReanimID;

        public Reanimation mHeadReanimID;

        public Reanimation mHeadReanimID2;

        public Reanimation mHeadReanimID3;

        public Reanimation mBlinkReanimID;

        public Reanimation mLightReanimID;

        public Reanimation mSleepingReanimID;

        public int mBlinkCountdown;

        public int mRecentlyEatenCountdown;

        public int mEatenFlashCountdown;

        public int mBeghouledFlashCountdown;

        public float mShakeOffsetX;

        public float mShakeOffsetY;

        public MagnetItem[] mMagnetItems = new MagnetItem[GameConstants.MAX_MAGNET_ITEMS];

        public Zombie mTargetZombieID;

        private int mTargetZombieIDSaved;

        public int mWakeUpCounter;

        public PlantOnBungeeState mOnBungeeState;

        public SeedType mImitaterType;

        public int mPottedPlantIndex;

        public bool mAnimPing;

        public bool mDead;

        public bool mSquished;

        public bool mIsAsleep;

        public bool mIsOnBoard;

        public bool mHighlighted;

        public bool mInFlowerPot;

        private static Stack<Plant> unusedObjects = new Stack<Plant>();

        public bool mGloveGrabbed;

        private string lastPlayedBodyReanim_Name = string.Empty;

        private ReanimLoopType lastPlayedBodyReanim_Type;

        private byte lastPlayedBodyReanim_BlendTime;

        private float lastPlayedBodyReanim_AnimRate;
    }
}
