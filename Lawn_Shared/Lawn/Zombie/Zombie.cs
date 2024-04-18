using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public enum ZombieAttackType
    {
        Chew,
        DriveOver,
        Vault,
        Ladder
    }
    public/*internal*/ class Zombie : GameObject, IComparable
    {
        public static void PreallocateMemory()
        {
            for (int i = 0; i < 200; i++)
            {
                new Zombie().PrepareForReuse();
            }
        }

        public static Zombie GetNewZombie()
        {
            if (Zombie.unusedObjects.Count > 0)
            {
                Zombie zombie = Zombie.unusedObjects.Pop();
                zombie.Reset();
                return zombie;
            }
            return new Zombie();
        }

        static Zombie()
        {
            for (int i = 0; i < Zombie.aPicks.Length; i++)
            {
                Zombie.aPicks[i] = TodWeightedGridArray.GetNewTodWeightedGridArray();
            }
        }

        private Zombie()
        {
            Reset();
        }

        public override void PrepareForReuse()
        {
            Zombie.unusedObjects.Push(this);
        }

        protected override void Reset()
        {
            base.Reset();
            lastPlayedReanimName = string.Empty;
            lastPlayedReanimLoopType = ReanimLoopType.PlayOnce;
            lastPlayedReanimBlendTime = 0;
            lastPlayedReanimAnimRate = 0f;
            doLoot = true;
            doParticle = true;
            draggedByTangleKelp = false;
            cachedZombieRectUpToDate = false;
            mZombieType = ZombieType.Normal;
            mZombiePhase = ZombiePhase.ZombieNormal;
            mPosX = 0f;
            mPosY = 0f;
            mVelX = 0f;
            mAnimCounter = 0;
            mGroanCounter = 0;
            mAnimTicksPerFrame = 0;
            mAnimFrames = 0;
            mFrame = 0;
            mPrevFrame = 0;
            mVariant = false;
            mIsEating = false;
            mJustGotShotCounter = 0;
            mShieldJustGotShotCounter = 0;
            mShieldRecoilCounter = 0;
            mZombieAge = 0;
            mZombieHeight = ZombieHeight.ZombieNormal;
            mPhaseCounter = 0;
            mFromWave = 0;
            mDroppedLoot = false;
            mZombieFade = 0;
            mFlatTires = false;
            mUseLadderCol = 0;
            mTargetCol = 0;
            mAltitude = 0f;
            mHitUmbrella = false;
            mZombieRect = default(TRect);
            mZombieAttackRect = default(TRect);
            mChilledCounter = 0;
            mButteredCounter = 0;
            mIceTrapCounter = 0;
            mMindControlled = false;
            mBlowingAway = false;
            mHasHead = false;
            mHasArm = false;
            mHasObject = false;
            mInPool = false;
            mOnHighGround = false;
            mYuckyFace = false;
            mYuckyFaceCounter = 0;
            mHelmType = HelmType.None;
            mBodyHealth = 0;
            mBodyMaxHealth = 0;
            mHelmHealth = 0;
            mHelmMaxHealth = 0;
            mShieldType = ShieldType.None;
            mShieldHealth = 0;
            mShieldMaxHealth = 0;
            mFlyingHealth = 0;
            mFlyingMaxHealth = 0;
            mDead = false;
            mRelatedZombieID = null;
            for (int i = 0; i < mFollowerZombieID.Length; i++)
            {
                mFollowerZombieID[i] = null;
            }
            mLeaderZombieIDSaved = -1;
            mLeaderZombie = null;
            mPlayingSong = false;
            mParticleOffsetX = 0;
            mParticleOffsetY = 0;
            mSummonCounter = 0;
            mBodyReanimID = null;
            mScaleZombie = 0f;
            mVelZ = 0f;
            mOrginalAnimRate = 0f;
            mTargetPlantID = null;
            mBossMode = 0;
            mTargetRow = 0;
            mBossBungeeCounter = 0;
            mBossStompCounter = 0;
            mBossHeadCounter = 0;
            mBossFireBallReanimID = null;
            mSpecialHeadReanimID = null;
            mFireballRow = 0;
            mIsFireBall = false;
            mMoweredReanimID = null;
            mLastPortalX = 0;
            mHasGroundTrack = false;
            mSummonedDancers = false;
            mUsesClipping = false;
            mSurprised = false;
            mGroundTrackIndex = -1;
            mHasArm = true;
            mHasHead = true;
            mHasHelm = true;
            mHasShield = true;
        }

        int IComparable.CompareTo(object toCompare)
        {
            Zombie zombie = (Zombie)toCompare;
            return mX.CompareTo(zombie.mX);
        }

        public void ZombieInitialize(int theRow, ZombieType theType, bool theVariant, Zombie theParentZombie, int theFromWave)
        {
            Debug.ASSERT(theType >= ZombieType.Normal && theType < ZombieType.ZombieTypesCount);
            mRow = theRow;
            mFromWave = theFromWave;
            mPosX = 800f - Constants.InvertAndScale(20f) + RandomNumbers.NextNumber(Constants.Zombie_StartRandom_Offset);
            mPosX += Constants.Zombie_StartOffset;
            mPosY = GetPosYBasedOnRow(theRow);
            mWidth = 120;
            mHeight = 120;
            mVelX = 0f;
            mVelZ = 0f;
            mFrame = 0;
            mPrevFrame = 0;
            mZombieType = theType;
            mVariant = theVariant;
            mIsEating = false;
            mJustGotShotCounter = 0;
            mShieldJustGotShotCounter = 0;
            mShieldRecoilCounter = 0;
            mChilledCounter = 0;
            mIceTrapCounter = 0;
            mButteredCounter = 0;
            mMindControlled = false;
            mBlowingAway = false;
            mHasHead = true;
            mHasArm = true;
            mHasObject = false;
            mInPool = false;
            mOnHighGround = false;
            mHelmType = HelmType.None;
            mShieldType = ShieldType.None;
            mYuckyFace = false;
            mYuckyFaceCounter = 0;
            mAnimCounter = 0;
            mGroanCounter = TodCommon.RandRangeInt(400, 500);
            mAnimTicksPerFrame = 12;
            mAnimFrames = 12;
            mZombieAge = 0;
            mTargetCol = -1;
            mZombiePhase = ZombiePhase.ZombieNormal;
            mZombieHeight = ZombieHeight.ZombieNormal;
            mPhaseCounter = 0;
            mHitUmbrella = false;
            mDroppedLoot = false;
            mRelatedZombieID = null;
            mZombieRect = new TRect(36, 0, 42, 115);
            mZombieAttackRect = new TRect(50, 0, 20, 115);
            mPlayingSong = false;
            mZombieFade = -1;
            mFlatTires = false;
            mUseLadderCol = -1;
            mShieldHealth = 0;
            mHelmHealth = 0;
            mFlyingHealth = 0;
            mAttachmentID = null;
            mSummonCounter = 0;
            mBossStompCounter = -1;
            mBossBungeeCounter = -1;
            mBossHeadCounter = -1;
            mBodyReanimID = null;
            mScaleZombie = 1f;
            mAltitude = 0f;
            mOrginalAnimRate = 0f;
            mTargetPlantID = null;
            mBossMode = 0;
            mBossFireBallReanimID = null;
            mSpecialHeadReanimID = null;
            mTargetRow = -1;
            mFireballRow = -1;
            mIsFireBall = false;
            mMoweredReanimID = null;
            mLastPortalX = -1;
            mHasGroundTrack = false;
            mSummonedDancers = false;
            mSurprised = false;
            for (int i = 0; i < GameConstants.MAX_ZOMBIE_FOLLOWERS; i++)
            {
                mFollowerZombieID[i] = null;
            }
            if (mBoard != null && mBoard.IsFlagWave(mFromWave))
            {
                mPosX += 40f;
            }
            PickRandomSpeed();
            mBodyHealth = 270;
            RenderLayer aRenderLayer = RenderLayer.Zombie;
            int aRenderOffset = 4;
            ZombieDefinition aZombieDef = Zombie.GetZombieDefinition(mZombieType);
            if (aZombieDef.mReanimationType != ReanimationType.None)
            {
                LoadReanim(aZombieDef.mReanimationType);
            }
            switch (mZombieType)
            {
            case ZombieType.Normal:
                LoadPlainZombieReanim();
                break;
            case ZombieType.DuckyTube:
                LoadPlainZombieReanim();
                break;
            case ZombieType.TrafficCone:
                LoadPlainZombieReanim();
                ReanimShowPrefix("anim_cone", 0);
                ReanimShowPrefix("anim_hair", -1);
                mHelmType = HelmType.TrafficCone;
                mHelmHealth = 370;
                break;
            case ZombieType.Pail:
                LoadPlainZombieReanim();
                ReanimShowPrefix("anim_bucket", 0);
                ReanimShowPrefix("anim_hair", -1);
                mHelmType = HelmType.Pail;
                mHelmHealth = 1100;
                break;
            case ZombieType.Door:
                mShieldType = ShieldType.Door;
                mShieldHealth = 1100;
                mPosX += 60f;
                LoadPlainZombieReanim();
                AttachShield();
                break;
            case ZombieType.Yeti:
                mBodyHealth = 1350;
                mPhaseCounter = TodCommon.RandRangeInt(1500, 2000);
                mHasObject = true;
                mZombieAttackRect = new TRect(20, 0, 50, 115);
                mPosX += 60f;
                break;
            case ZombieType.Ladder:
                mBodyHealth = 500;
                mShieldType = ShieldType.Ladder;
                mShieldHealth = 500;
                mZombieAttackRect = new TRect(10, 0, 50, 115);
                if (IsOnBoard())
                {
                    mZombiePhase = ZombiePhase.LadderCarrying;
                    StartWalkAnim(0);
                }
                AttachShield();
                break;
            case ZombieType.Bungee:
            {
                mBodyHealth = 450;
                mAnimFrames = 4;
                mAltitude = GameConstants.BUNGEE_ZOMBIE_HEIGHT + TodCommon.RandRangeInt(0, 150);
                mVelX = 0f;
                if (IsOnBoard())
                {
                    PickBungeeZombieTarget(-1);
                    if (mDead)
                    {
                        return;
                    }
                    mZombiePhase = ZombiePhase.BungeeDiving;
                }
                else
                {
                    mZombiePhase = ZombiePhase.BungeeCutscene;
                    mPhaseCounter = TodCommon.RandRangeInt(0, 200);
                }
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_drop, ReanimLoopType.Loop, 0, 24f);
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                aBodyReanim.AssignRenderGroupToPrefix("Zombie_bungi_rightarm_lower2", GameConstants.RENDER_GROUP_ARMS);
                aBodyReanim.AssignRenderGroupToPrefix("Zombie_bungi_rightarm_hand2", GameConstants.RENDER_GROUP_ARMS);
                aBodyReanim.AssignRenderGroupToPrefix("Zombie_bungi_leftarm_lower2", GameConstants.RENDER_GROUP_ARMS);
                aBodyReanim.AssignRenderGroupToPrefix("Zombie_bungi_leftarm_hand2", GameConstants.RENDER_GROUP_ARMS);
                aBodyReanim.SetTruncateDisappearingFrames(string.Empty, false);
                aRenderLayer = RenderLayer.GraveStone;
                aRenderOffset = 7;
                mZombieRect = new TRect(-20, 22, 110, 94);
                mZombieAttackRect = new TRect(0, 0, 0, 0);
                mVariant = false;
                break;
            }

            case ZombieType.Football:
                mZombieRect = new TRect(50, 0, 57, 115);
                ReanimShowPrefix("anim_hair", -1);
                mHelmType = HelmType.Football;
                mHelmHealth = 1400;
                mAnimTicksPerFrame = 6;
                mVariant = false;
                break;
            case ZombieType.Digger:
            {
                mHelmType = HelmType.Digger;
                mHelmHealth = 100;
                mVariant = false;
                mHasObject = true;
                mZombieRect = new TRect(50, 0, 28, 115);
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                aBodyReanim.SetTruncateDisappearingFrames(string.Empty, false);
                if (!IsOnBoard())
                {
                    mZombiePhase = ZombiePhase.DiggerCutscene;
                }
                else
                {
                    mZombiePhase = ZombiePhase.DiggerTunneling;
                    AddAttachedParticle(60, 100, ParticleEffect.DiggerTunnel);
                    aRenderOffset = 7;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_dig, ReanimLoopType.LoopFullLastFrame, 0, 12f);
                    PickRandomSpeed();
                }

                break;
            }

            case ZombieType.Polevaulter:
                mBodyHealth = 500;
                mAnimTicksPerFrame = 6;
                mZombiePhase = ZombiePhase.PolevaulterPreVault;
                mHasObject = true;
                mVariant = false;
                mPosX = Constants.WIDE_BOARD_WIDTH + 70 + RandomNumbers.NextNumber(10);
                if (IsOnBoard())
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_run, ReanimLoopType.Loop, 0, 0f);
                    PickRandomSpeed();
                }
                if (mApp.IsWallnutBowlingLevel())
                {
                    mZombieAttackRect = new TRect(-229, 0, 270, 115);
                }
                else
                {
                    mZombieAttackRect = new TRect(-29, 0, 70, 115);
                }
                break;
            case ZombieType.DolphinRider:
                mBodyHealth = 500;
                mAnimTicksPerFrame = 6;
                mZombiePhase = ZombiePhase.DolphinWalking;
                mVariant = false;
                if (IsOnBoard())
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_walkdolphin, ReanimLoopType.Loop, 0, 0f);
                    PickRandomSpeed();
                }
                SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_dolphinrider_whitewater);
                SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_dolphinrider_dolphininwater);
                break;
            case ZombieType.Gargantuar:
            case ZombieType.RedeyeGargantuar:
            {
                mBodyHealth = 3000;
                mAnimFrames = 24;
                mAnimTicksPerFrame = 8;
                mWidth = 180;
                mHeight = 180;
                mPosX = Constants.WIDE_BOARD_WIDTH + 45 + RandomNumbers.NextNumber(10);
                mZombieRect = new TRect(-17, -38, 125, 154);
                mZombieAttackRect = new TRect(-30, -38, 89, 154);
                mVariant = false;
                aRenderOffset = 8;
                mHasObject = true;
                int num = RandomNumbers.NextNumber(100);
                int num2;
                if (!IsOnBoard() || mBoard.mLevel == 48)
                {
                    num2 = 0;
                }
                else if (num < 10)
                {
                    num2 = 2;
                }
                else if (num < 35)
                {
                    num2 = 1;
                }
                else
                {
                    num2 = 0;
                }
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                if (num2 == 2)
                {
                    aBodyReanim.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_gargantuar_telephonepole, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_ZOMBIE);
                }
                else if (num2 == 1)
                {
                    aBodyReanim.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_gargantuar_telephonepole, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_DUCKXING);
                }
                if (mZombieType == ZombieType.RedeyeGargantuar)
                {
                    aBodyReanim.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_HEAD_REDEYE);
                    mBodyHealth = 6000;
                }

                break;
            }

            case ZombieType.Zamboni:
                mBodyHealth = 1350;
                mAnimFrames = 2;
                mAnimTicksPerFrame = 8;
                mPosX = Constants.WIDE_BOARD_WIDTH + RandomNumbers.NextNumber(10) + 20;
                aRenderOffset = 8;
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_drive, ReanimLoopType.Loop, 0, 12f);
                mZombieRect = new TRect(0, -13, 153, 140);
                mZombieAttackRect = new TRect(10, -13, 133, 140);
                mVariant = false;
                break;
            case ZombieType.Catapult:
                mBodyHealth = 850;
                mPosX = Constants.WIDE_BOARD_WIDTH + 25 + RandomNumbers.NextNumber(10);
                mSummonCounter = 20;
                if (IsOnBoard())
                {
                    PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, 0, 5.5f);
                }
                else
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, 8f);
                }
                mZombieRect = new TRect(0, -13, 153, 140);
                mZombieAttackRect = new TRect(10, -13, 133, 140);
                mVariant = false;
                break;
            case ZombieType.Snorkel:
                mZombieRect = new TRect(12, 0, 62, 115);
                mZombieAttackRect = new TRect(-5, 0, 55, 115);
                SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_snorkle_whitewater);
                SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_snorkle_whitewater2);
                mVariant = false;
                mZombiePhase = ZombiePhase.SnorkelWalking;
                break;
            case ZombieType.JackInTheBox:
            {
                mBodyHealth = 500;
                mAnimTicksPerFrame = 6;
                int num3 = 450 + RandomNumbers.NextNumber(300);
                if (RandomNumbers.NextNumber(20) == 0)
                {
                    num3 /= 3;
                }
                mPhaseCounter = (int)(num3 / mVelX) * GameConstants.ZOMBIE_LIMP_SPEED_FACTOR;
                mZombieAttackRect = new TRect(20, 0, 50, 115);
                if (mApp.IsScaryPotterLevel())
                {
                    mPhaseCounter = 10;
                }
                if (IsOnBoard())
                {
                    mZombiePhase = ZombiePhase.JackInTheBoxRunning;
                }

                break;
            }

            case ZombieType.Bobsled:
            {
                aRenderOffset = 3;
                if (theParentZombie != null)
                {
                    int iFollower = 0;
                    while (iFollower < 3 && theParentZombie.mFollowerZombieID[iFollower] != null)
                    {
                        iFollower++;
                    }
                    Debug.ASSERT(iFollower < 3);
                    theParentZombie.mFollowerZombieID[iFollower] = mBoard.ZombieGetID(this);
                    mRelatedZombieID = mBoard.ZombieGetID(theParentZombie);
                    mPosX = theParentZombie.mPosX + (iFollower + 1) * 50;
                    if (iFollower == 0)
                    {
                        aRenderOffset = 1;
                        mAltitude = 9f;
                    }
                    else if (iFollower == 1)
                    {
                        aRenderOffset = 2;
                        mAltitude = -7f;
                    }
                    else
                    {
                        aRenderOffset = 0;
                        mAltitude = 9f;
                    }
                }
                else
                {
                    mPosX = Constants.WIDE_BOARD_WIDTH + 80;
                    mAltitude = -10f;
                    mHelmType = HelmType.Bobsled;
                    mHelmHealth = 300;
                    mZombieRect = new TRect(-50, 0, 275, 115);
                }
                mVelX = 0.6f;
                mZombiePhase = ZombiePhase.BobsledSliding;
                mPhaseCounter = 500;
                mVariant = false;
                if (theFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jump, ReanimLoopType.PlayOnceAndHold, 0, 20f);
                    mApp.ReanimationGet(mBodyReanimID)
                        .mAnimTime = 1f;
                    mAltitude = 18f;
                }
                else if (IsOnBoard())
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_push, ReanimLoopType.Loop, 0, 30f);
                }

                break;
            }

            case ZombieType.Flag:
            {
                mHasObject = true;
                LoadPlainZombieReanim();
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                Reanimation aFlagPoleReanim = mApp.AddReanimation(0f, 0f, 0, ReanimationType.ZombieFlagpole);
                aFlagPoleReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_zombie_flag, ReanimLoopType.Loop, 0, 15f);
                mSpecialHeadReanimID = mApp.ReanimationGetID(aFlagPoleReanim);
                ReanimatorTrackInstance aTrackInstance = aBodyReanim.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand);
                GlobalMembersAttachment.AttachReanim(ref aTrackInstance.mAttachmentID, aFlagPoleReanim, 0f, 0f);
                aBodyReanim.mFrameBasePose = 0;
                mPosX = Constants.WIDE_BOARD_WIDTH;
                break;
            }

            case ZombieType.Pogo:
            {
                mVariant = false;
                mZombiePhase = ZombiePhase.PogoBouncing;
                mPhaseCounter = RandomNumbers.NextNumber(GameConstants.POGO_BOUNCE_TIME) + 1;
                mHasObject = true;
                mBodyHealth = 500;
                mZombieAttackRect = new TRect(10, 0, 30, 115);
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_pogo, ReanimLoopType.PlayOnceAndHold, 0, 40f);
                mApp.ReanimationGet(mBodyReanimID)
                    .mAnimTime = 1f;
                break;
            }

            case ZombieType.Newspaper:
                mZombieAttackRect = new TRect(20, 0, 50, 115);
                mZombiePhase = ZombiePhase.NewspaperReading;
                mShieldType = ShieldType.Newspaper;
                mShieldHealth = 150;
                mVariant = false;
                AttachShield();
                break;
            case ZombieType.Balloon:
            {
                Reanimation reanimation8 = mApp.ReanimationGet(mBodyReanimID);
                reanimation8.SetTruncateDisappearingFrames(string.Empty, false);
                if (IsOnBoard())
                {
                    mZombiePhase = ZombiePhase.BalloonFlying;
                    mAltitude = 25f;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, reanimation8.mAnimRate);
                }
                else
                {
                    float animRate = TodCommon.RandRangeFloat(8f, 10f);
                    SetAnimRate(animRate);
                }
                Reanimation aBodyReanim = mApp.AddReanimation(0f, 0f, 0, aZombieDef.mReanimationType);
                aBodyReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_propeller);
                aBodyReanim.mLoopType = ReanimLoopType.LoopFullLastFrame;
                aBodyReanim.AttachToAnotherReanimation(ref reanimation8, GlobalMembersReanimIds.ReanimTrackId_hat);
                mFlyingHealth = 20;
                mZombieRect = new TRect(36, 30, 42, 115);
                mZombieAttackRect = new TRect(20, 30, 50, 115);
                mVariant = false;
                break;
            }

            case ZombieType.Dancer:
                if (!IsOnBoard())
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_moonwalk, ReanimLoopType.Loop, 0, 12f);
                }
                else
                {
                    mZombiePhase = ZombiePhase.DancerDancingIn;
                    mPhaseCounter = 200 + RandomNumbers.NextNumber(12);
                    mVelX = 0.5f;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_moonwalk, ReanimLoopType.Loop, 0, 24f);
                }
                mBodyHealth = 500;
                mVariant = false;
                break;
            case ZombieType.BackupDancer:
                if (!IsOnBoard())
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.Loop, 0, 12f);
                }
                ReanimShowPrefix("Zombie_disco_outerarm_upper_bone", -1);
                mZombiePhase = ZombiePhase.DancerDancingLeft;
                mVariant = false;
                break;
            case ZombieType.Imp:
                if (!IsOnBoard())
                {
                    PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, 0, 12f);
                }
                if (mApp.IsIZombieLevel())
                {
                    mBodyHealth = 70;
                }
                break;
            case ZombieType.Boss:
                mPosX = Constants.BOARD_EXTRA_ROOM;
                mPosY = 0f;
                mZombieRect = new TRect(700, 80, 90, 430);
                mZombieAttackRect = default;
                aRenderLayer = RenderLayer.Top;
                if (mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
                {
                    mBodyHealth = 40000;
                }
                else
                {
                    mBodyHealth = 60000;
                }
                if (IsOnBoard())
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_enter, ReanimLoopType.PlayOnceAndHold, 0, 12f);
                    mSummonCounter = 500;
                    mBossHeadCounter = 5000;
                    mZombiePhase = ZombiePhase.BossEnter;
                }
                else
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.Loop, 0, 12f);
                }
                BossSetupReanim();
                break;
            case ZombieType.PeaHead:
            {
                LoadPlainZombieReanim();
                ReanimShowPrefix("anim_hair", -1);
                ReanimShowPrefix("anim_head2", -1);
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                if (IsOnBoard())
                {
                    aBodyReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
                }
                ReanimatorTrackInstance aTrackInstance = aBodyReanim.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
                aTrackInstance.mImageOverride = AtlasResources.IMAGE_BLANK;
                Reanimation aPeaHeadReanim = mApp.AddReanimation(0f, 0f, 0, ReanimationType.Peashooter);
                aPeaHeadReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.Loop, 0, 15f);
                mSpecialHeadReanimID = mApp.ReanimationGetID(aPeaHeadReanim);
                AttachEffect aAttachEffect = GlobalMembersAttachment.AttachReanim(ref aTrackInstance.mAttachmentID, aPeaHeadReanim, 0f, 0f);
                aBodyReanim.mFrameBasePose = 0;
                TodCommon.TodScaleRotateTransformMatrix(ref aAttachEffect.mOffset.mMatrix, 65f * Constants.S, -8f * Constants.S, 0.2f, -1f, 1f);
                mPhaseCounter = 150;
                mVariant = false;
                break;
            }

            case ZombieType.WallnutHead:
            {
                LoadPlainZombieReanim();
                ReanimShowPrefix("anim_hair", -1);
                ReanimShowPrefix("anim_head", -1);
                ReanimShowPrefix("Zombie_tie", -1);
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                if (IsOnBoard())
                {
                    aBodyReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
                }
                ReanimatorTrackInstance aTrackInstance = aBodyReanim.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_body);
                Reanimation aWallnutHeadReanim = mApp.AddReanimation(0f, 0f, 0, ReanimationType.Wallnut);
                aWallnutHeadReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, 15f);
                mSpecialHeadReanimID = mApp.ReanimationGetID(aWallnutHeadReanim);
                AttachEffect aAttachEffect = GlobalMembersAttachment.AttachReanim(ref aTrackInstance.mAttachmentID, aWallnutHeadReanim, 0f, 0f);
                aBodyReanim.mFrameBasePose = 0;
                TodCommon.TodScaleRotateTransformMatrix(ref aAttachEffect.mOffset.mMatrix, 50f * Constants.S, 0f, 0.2f, -0.8f, 0.8f);
                mHelmType = HelmType.Wallnut;
                mHelmHealth = 1100;
                mVariant = false;
                break;
            }

            case ZombieType.TallnutHead:
            {
                LoadPlainZombieReanim();
                ReanimShowPrefix("anim_hair", -1);
                ReanimShowPrefix("anim_head", -1);
                ReanimShowPrefix("Zombie_tie", -1);
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                if (IsOnBoard())
                {
                    aBodyReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
                }
                ReanimatorTrackInstance aTrackInstance = aBodyReanim.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_body);
                Reanimation aTallnutHeadReanim = mApp.AddReanimation(0f, 0f, 0, ReanimationType.Tallnut);
                aTallnutHeadReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, 15f);
                mSpecialHeadReanimID = mApp.ReanimationGetID(aTallnutHeadReanim);
                AttachEffect aAttachEffect = GlobalMembersAttachment.AttachReanim(ref aTrackInstance.mAttachmentID, aTallnutHeadReanim, 0f, 0f);
                aBodyReanim.mFrameBasePose = 0;
                TodCommon.TodScaleRotateTransformMatrix(ref aAttachEffect.mOffset.mMatrix, 37f * Constants.S, 0f, 0.2f, -0.8f, 0.8f);
                mHelmType = HelmType.Tallnut;
                mHelmHealth = 2200;
                mVariant = false;
                mPosX += 30f;
                break;
            }

            case ZombieType.JalapenoHead:
            {
                LoadPlainZombieReanim();
                ReanimShowPrefix("anim_hair", -1);
                ReanimShowPrefix("anim_head", -1);
                ReanimShowPrefix("Zombie_tie", -1);
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                if (IsOnBoard())
                {
                    aBodyReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
                }
                ReanimatorTrackInstance aTrackInstance = aBodyReanim.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_body);
                Reanimation aJalapenoHeadReanim = mApp.AddReanimation(0f, 0f, 0, ReanimationType.Jalapeno);
                aJalapenoHeadReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, 15f);
                mSpecialHeadReanimID = mApp.ReanimationGetID(aJalapenoHeadReanim);
                AttachEffect aAttachEffect = GlobalMembersAttachment.AttachReanim(ref aTrackInstance.mAttachmentID, aJalapenoHeadReanim, 0f, 0f);
                aBodyReanim.mFrameBasePose = 0;
                TodCommon.TodScaleRotateTransformMatrix(ref aAttachEffect.mOffset.mMatrix, 55f * Constants.S, -5f * Constants.S, 0.2f, -1f, 1f);
                mVariant = false;
                mBodyHealth = 500;
                int num5 = 275 + RandomNumbers.NextNumber(175);
                mPhaseCounter = (int)(num5 / mVelX) * GameConstants.ZOMBIE_LIMP_SPEED_FACTOR;
                break;
            }

            case ZombieType.GatlingHead:
            {
                LoadPlainZombieReanim();
                ReanimShowPrefix("anim_hair", -1);
                ReanimShowPrefix("anim_head2", -1);
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                if (IsOnBoard())
                {
                    aBodyReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
                }
                ReanimatorTrackInstance aTrackInstance = aBodyReanim.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
                aTrackInstance.mImageOverride = AtlasResources.IMAGE_BLANK;
                Reanimation aGatlingHeadReanim = mApp.AddReanimation(0f, 0f, 0, ReanimationType.Gatlingpea);
                aGatlingHeadReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.Loop, 0, 15f);
                mSpecialHeadReanimID = mApp.ReanimationGetID(aGatlingHeadReanim);
                AttachEffect aAttachEffect = GlobalMembersAttachment.AttachReanim(ref aTrackInstance.mAttachmentID, aGatlingHeadReanim, 0f, 0f);
                aBodyReanim.mFrameBasePose = 0;
                TodCommon.TodScaleRotateTransformMatrix(ref aAttachEffect.mOffset.mMatrix, 65f * Constants.S, -18f * Constants.S, 0.2f, -1f, 1f);
                mPhaseCounter = 150;
                mVariant = false;
                break;
            }

            case ZombieType.SquashHead:
            {
                LoadPlainZombieReanim();
                ReanimShowPrefix("anim_hair", -1);
                ReanimShowPrefix("anim_head2", -1);
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                if (IsOnBoard())
                {
                    aBodyReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
                }
                ReanimatorTrackInstance aTrackInstance = aBodyReanim.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
                aTrackInstance.mImageOverride = AtlasResources.IMAGE_BLANK;
                Reanimation aSquashHeadReanim = mApp.AddReanimation(0f, 0f, 0, ReanimationType.Squash);
                aSquashHeadReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, 15f);
                mSpecialHeadReanimID = mApp.ReanimationGetID(aSquashHeadReanim);
                AttachEffect aAttachEffect = GlobalMembersAttachment.AttachReanim(ref aTrackInstance.mAttachmentID, aSquashHeadReanim, 0f, 0f);
                aBodyReanim.mFrameBasePose = 0;
                TodCommon.TodScaleRotateTransformMatrix(ref aAttachEffect.mOffset.mMatrix, 55f * Constants.S, -15f * Constants.S, 0.2f, -0.75f, 0.75f);
                mZombiePhase = ZombiePhase.SquashPreLaunch;
                mVariant = false;
                break;
            }
            }
            if (mApp.IsLittleTroubleLevel() && (IsOnBoard() || theFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE))
            {
                mScaleZombie = 0.5f;
                mBodyHealth /= 4;
                mHelmHealth /= 4;
                mShieldHealth /= 4;
                mFlyingHealth /= 4;
            }
            UpdateAnimSpeed();
            if (mVariant)
            {
                ReanimShowPrefix("anim_tongue", 0);
            }
            mBodyMaxHealth = mBodyHealth;
            mHelmMaxHealth = mHelmHealth;
            mShieldMaxHealth = mShieldHealth;
            mFlyingMaxHealth = mFlyingHealth;
            mDead = false;
            mX = (int)mPosX;
            mY = (int)mPosY;
            mRenderOrder = Board.MakeRenderOrder(aRenderLayer, mRow, aRenderOffset);
            if (mZombieHeight == ZombieHeight.Zombiquarium)
            {
                mBodyMaxHealth = 300;
            }
            if (IsOnBoard())
            {
                PlayZombieAppearSound();
                StartZombieSound();
            }
            UpdateReanim();
            if (mBodyReanimID != null && mBodyReanimID.TrackExists("zombie_butter"))
            {
                mBodyReanimID.AssignRenderGroupToTrack("zombie_butter", -1);
            }
        }

        public void Dispose()
        {
            GlobalMembersAttachment.AttachmentDie(ref mAttachmentID);
            StopZombieSound();
            PrepareForReuse();
        }

        public void Animate()//3update
        {
            mPrevFrame = mFrame;
            if (mZombiePhase == ZombiePhase.JackInTheBoxPopping || mZombiePhase == ZombiePhase.NewspaperMaddening || mZombiePhase == ZombiePhase.DiggerRising || mZombiePhase == ZombiePhase.DiggerTunnelingPauseWithoutAxe || mZombiePhase == ZombiePhase.DiggerRiseWithoutAxe || mZombiePhase == ZombiePhase.DiggerStunned)
            {
                return;
            }
            if (IsImmobilizied())
            {
                return;
            }
            //mAnimCounter += 3;
            mAnimCounter--;
            if (mYuckyFace)
            {
                //for (int i = 0; i < 3; i++)
                //{
                    //if (mYuckyFace)
                    //{
                        UpdateYuckyFace();
                    //}
                //}
            }
            if (mIsEating && mHasHead)
            {
                int num = 6;
                if (mChilledCounter > 0)
                {
                    num = 12;
                }
                if (mAnimCounter >= mAnimFrames * num)
                {
                    mAnimCounter = num;
                }
                mFrame = mAnimCounter / num;
                Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
                if (reanimation != null)
                {
                    float aLeftHandTime = 0.14f;
                    float aRightHandTime = 0.68f;
                    if (mZombieType == ZombieType.Polevaulter)
                    {
                        aLeftHandTime = 0.38f;
                        aRightHandTime = 0.8f;
                    }
                    else if (mZombieType == ZombieType.Newspaper || mZombieType == ZombieType.Ladder)
                    {
                        aLeftHandTime = 0.42f;
                        aRightHandTime = 0.42f;
                    }
                    else if (mZombieType == ZombieType.JackInTheBox)
                    {
                        aLeftHandTime = 0.53f;
                        aRightHandTime = 0.53f;
                    }
                    else if (mZombieType == ZombieType.Bobsled)
                    {
                        aLeftHandTime = 0.33f;
                        aRightHandTime = 0.83f;
                    }
                    else if (mZombieType == ZombieType.Imp)
                    {
                        aLeftHandTime = 0.33f;
                        aRightHandTime = 0.79f;
                    }
                    if (reanimation.ShouldTriggerTimedEvent(aLeftHandTime) || reanimation.ShouldTriggerTimedEvent(aRightHandTime))
                    {
                        AnimateChewSound();
                        AnimateChewEffect();
                        return;
                    }
                }
                else
                {
                    //if (mAnimCounter == 3 * num)
                    if (mAnimCounter == 4 * num)
                    {
                        AnimateChewSound();
                    }
                    //if (mAnimCounter == 6 * num && !mMindControlled)
                    if (mAnimCounter == 7 * num && !mMindControlled)
                    {
                        AnimateChewEffect();
                    }
                }
                return;
            }
            if (mAnimCounter >= mAnimFrames * mAnimTicksPerFrame)
            {
                mAnimCounter = 0;
            }
            mFrame = mAnimCounter / mAnimTicksPerFrame;
        }

        public void CheckIfPreyCaught()//3update
        {
            if (mZombieType == ZombieType.Bungee || mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar || mZombieType == ZombieType.Zamboni || mZombieType == ZombieType.Catapult || mZombieType == ZombieType.Boss)
            {
                return;
            }
            if (IsBouncingPogo() || IsBobsledTeamWithSled())
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.PolevaulterInVault || mZombiePhase == ZombiePhase.PolevaulterPreVault || mZombiePhase == ZombiePhase.NewspaperMaddening || mZombiePhase == ZombiePhase.DiggerRising || mZombiePhase == ZombiePhase.DiggerTunnelingPauseWithoutAxe || mZombiePhase == ZombiePhase.DiggerRiseWithoutAxe || mZombiePhase == ZombiePhase.DiggerStunned || mZombiePhase == ZombiePhase.RisingFromGrave || mZombiePhase == ZombiePhase.ImpGettingThrown || mZombiePhase == ZombiePhase.ImpLanding || mZombiePhase == ZombiePhase.DancerRising || mZombiePhase == ZombiePhase.DancerSnappingFingers || mZombiePhase == ZombiePhase.DancerSnappingFingersWithLight || mZombiePhase == ZombiePhase.DancerSnappingFingersHold || mZombiePhase == ZombiePhase.DolphinWalking || mZombiePhase == ZombiePhase.DolphinWalkingWithoutDolphin || mZombiePhase == ZombiePhase.DolphinIntoPool || mZombiePhase == ZombiePhase.DolphinRiding || mZombiePhase == ZombiePhase.DolphinInJump || mZombiePhase == ZombiePhase.SnorkelIntoPool || mZombiePhase == ZombiePhase.SnorkelWalking || mZombiePhase == ZombiePhase.LadderPlacing || mZombieHeight == ZombieHeight.GettingBungeeDropped || mZombieHeight == ZombieHeight.UpLadder || mZombieHeight == ZombieHeight.InToPool || mZombieHeight == ZombieHeight.OutOfPool || IsTangleKelpTarget() || mZombieHeight == ZombieHeight.Falling)
            {
                return;
            }
            if (!mHasHead)
            {
                return;
            }
            if (IsFlying())
            {
                return;
            }
            int num = GameConstants.TICKS_BETWEEN_EATS;
            if (mChilledCounter > 0)
            {
                num *= 2;
            }
            if (mZombieAge % num != 0)
            {
                return;
            }
            Zombie zombie = FindZombieTarget();
            if (zombie != null)
            {
                EatZombie(zombie);
                return;
            }
            if (!mMindControlled)
            {
                Plant plant = FindPlantTarget(ZombieAttackType.Chew);
                if (plant != null)
                {
                    EatPlant(plant);
                    return;
                }
            }
            if (mApp.IsIZombieLevel() && mBoard.mChallenge.IZombieEatBrain(this))
            {
                return;
            }
            if (mIsEating)
            {
                StopEating();
            }
        }

        public void EatZombie(Zombie theZombie)//3update
        {
            theZombie.TakeDamage(GameConstants.TICKS_BETWEEN_EATS, 9U, false);
            StartEating();
            if (theZombie.mBodyHealth <= 0)
            {
                mApp.PlaySample(Resources.SOUND_GULP);
            }
        }

        public void EatPlant(Plant thePlant)//3update
        {
            if (mZombiePhase == ZombiePhase.DancerDancingIn)
            {
                mPhaseCounter = 1;
                return;
            }
            if (mYuckyFace)
            {
                return;
            }
            if (mBoard.GetLadderAt(thePlant.mPlantCol, thePlant.mRow) != null && mZombieType != ZombieType.Digger)
            {
                StopEating();
                if (mZombieHeight == ZombieHeight.ZombieNormal && mUseLadderCol != thePlant.mPlantCol)
                {
                    mZombieHeight = ZombieHeight.UpLadder;
                    mUseLadderCol = thePlant.mPlantCol;
                }
                return;
            }
            StartEating();
            if ((thePlant.mSeedType == SeedType.Jalapeno || thePlant.mSeedType == SeedType.Cherrybomb || thePlant.mSeedType == SeedType.Doomshroom || thePlant.mSeedType == SeedType.Iceshroom || thePlant.mSeedType == SeedType.Hypnoshroom || thePlant.mState == PlantState.FlowerpotInvulnerable || thePlant.mState == PlantState.LilypadInvulnerable || thePlant.mState == PlantState.SquashLook || thePlant.mState == PlantState.SquashPreLaunch) && !thePlant.mIsAsleep)
            {
                return;
            }
            if (thePlant.mSeedType == SeedType.Potatomine && thePlant.mState != PlantState.Notready)
            {
                return;
            }
            bool flag = false;
            if (thePlant.mSeedType == SeedType.Blover)
            {
                flag = true;
            }
            if (thePlant.mSeedType == SeedType.Iceshroom && !thePlant.mIsAsleep)
            {
                flag = true;
            }
            if (flag)
            {
                thePlant.DoSpecial();
                return;
            }
            if (mChilledCounter > 0 && mZombieAge % 2 == 1)
            {
                return;
            }
            if (mApp.IsIZombieLevel() && thePlant.mSeedType == SeedType.Sunflower)
            {
                int num = thePlant.mPlantHealth / 40;
                int num2 = (thePlant.mPlantHealth - /*3 * */GameConstants.TICKS_BETWEEN_EATS) / 40;
                if (num2 < num)
                {
                    mBoard.AddCoin(thePlant.mX, thePlant.mY, CoinType.Sun, CoinMotion.FromPlant);
                }
            }
            thePlant.mPlantHealth -= /*3 * */GameConstants.TICKS_BETWEEN_EATS;
            thePlant.mRecentlyEatenCountdown = 50;
            if (mApp.IsIZombieLevel() && mJustGotShotCounter < -500 && (thePlant.mSeedType == SeedType.Wallnut || thePlant.mSeedType == SeedType.Tallnut || thePlant.mSeedType == SeedType.Pumpkinshell))
            {
                thePlant.mPlantHealth -= /*3 * */GameConstants.TICKS_BETWEEN_EATS;
            }
            if (thePlant.mPlantHealth <= 0)
            {
                mApp.PlaySample(Resources.SOUND_GULP);
                mBoard.mPlantsEaten++;
                thePlant.Die();
                mBoard.mChallenge.ZombieAtePlant(this, thePlant);
                if (mBoard.mLevel >= 2 && mBoard.mLevel <= 4 && mApp.IsFirstTimeAdventureMode() && thePlant.mPlantCol > 4 && mBoard.mPlants.Count < 15 && thePlant.mSeedType == SeedType.Peashooter)
                {
                    mBoard.DisplayAdvice("[ADVICE_PEASHOOTER_DIED]", MessageStyle.HintTallFast, AdviceType.PeashooterDied);
                }
            }
        }

        public override bool SaveToFile(Sexy.Buffer b)
        {
            base.SaveToFile(b);
            b.WriteLong((int)mZombieType);
            b.WriteBoolean(mVariant);
            b.WriteLong(mFromWave);
            b.WriteFloat(mAltitude);
            b.WriteLong(mAnimCounter);
            b.WriteLong(mAnimFrames);
            b.WriteLong(mAnimTicksPerFrame);
            b.WriteBoolean(mBlowingAway);
            b.WriteLong(mBodyHealth);
            b.WriteLong(mBodyMaxHealth);
            b.WriteLong(mBossBungeeCounter);
            b.WriteLong(mBossHeadCounter);
            b.WriteLong(mBossMode);
            b.WriteLong(mBossStompCounter);
            b.WriteLong(mButteredCounter);
            b.WriteLong(mChilledCounter);
            b.WriteBoolean(mDroppedLoot);
            b.WriteLong(mFireballRow);
            b.WriteBoolean(mFlatTires);
            b.WriteLong(mFlyingHealth);
            b.WriteLong(mFlyingMaxHealth);
            for (int i = 0; i < mFollowerZombieID.Length; i++)
            {
                GameObject.SaveId(mFollowerZombieID[i], b);
            }
            GameObject.SaveId(mLeaderZombie, b);
            b.WriteLong(mFrame);
            b.WriteLong(mGroanCounter);
            b.WriteBoolean(mHasGroundTrack);
            b.WriteBoolean(mHasObject);
            b.WriteLong(mHelmHealth);
            b.WriteLong(mHelmMaxHealth);
            b.WriteBoolean(mHitUmbrella);
            b.WriteLong(mIceTrapCounter);
            b.WriteBoolean(mInPool);
            b.WriteBoolean(mIsEating);
            b.WriteBoolean(mIsFireBall);
            b.WriteLong(mJustGotShotCounter);
            b.WriteLong(mLastPortalX);
            b.WriteBoolean(mMindControlled);
            b.WriteBoolean(mOnHighGround);
            b.WriteFloat(mOrginalAnimRate);
            b.WriteLong(mParticleOffsetX);
            b.WriteLong(mParticleOffsetY);
            b.WriteLong(mPhaseCounter);
            b.WriteBoolean(mPlayingSong);
            b.WriteFloat(mPosX);
            b.WriteFloat(mPosY);
            b.WriteLong(mPrevFrame);
            b.WriteFloat(mPrevTransX);
            b.WriteFloat(mPrevTransY);
            GameObject.SaveId(mRelatedZombieID, b);
            b.WriteFloat(mScaleZombie);
            b.WriteLong(mShieldHealth);
            b.WriteLong(mShieldJustGotShotCounter);
            b.WriteLong(mShieldMaxHealth);
            b.WriteLong(mShieldRecoilCounter);
            b.WriteLong(mSummonCounter);
            b.WriteBoolean(mSummonedDancers);
            b.WriteLong(mTargetCol);
            GameObject.SaveId(mTargetPlantID, b);
            b.WriteLong(mTargetRow);
            b.WriteLong(mUseLadderCol);
            b.WriteBoolean(mUsesClipping);
            b.WriteFloat(mVelX);
            b.WriteFloat(mVelZ);
            b.WriteBoolean(mYuckyFace);
            b.WriteLong(mYuckyFaceCounter);
            b.WriteLong(mZombieAge);
            b.WriteRect(mZombieAttackRect);
            b.WriteLong((int)mZombieHeight);
            b.WriteLong((int)mZombiePhase);
            b.WriteRect(mZombieRect);
            b.WriteString(lastPlayedReanimName);
            b.WriteFloat(lastPlayedReanimAnimRate);
            b.WriteByte(lastPlayedReanimBlendTime);
            b.WriteLong((int)lastPlayedReanimLoopType);
            b.WriteBoolean(mHasArm);
            b.WriteBoolean(mHasHead);
            b.WriteBoolean(mHasHelm);
            b.WriteBoolean(mHasShield);
            return true;
        }

        public override bool LoadFromFile(Sexy.Buffer b)
        {
            base.LoadFromFile(b);
            mZombieType = (ZombieType)b.ReadLong();
            mVariant = b.ReadBoolean();
            mFromWave = b.ReadLong();
            doLoot = false;
            doParticle = false;
            int aRow = mRow;
            ZombieInitialize(mRow, mZombieType, mVariant, null, mFromWave);
            mAltitude = b.ReadFloat();
            mAnimCounter = b.ReadLong();
            mAnimFrames = b.ReadLong();
            mAnimTicksPerFrame = b.ReadLong();
            mBlowingAway = b.ReadBoolean();
            mBodyHealth = b.ReadLong();
            mBodyMaxHealth = b.ReadLong();
            mBossBungeeCounter = b.ReadLong();
            mBossHeadCounter = b.ReadLong();
            mBossMode = b.ReadLong();
            mBossStompCounter = b.ReadLong();
            mButteredCounter = b.ReadLong();
            mChilledCounter = b.ReadLong();
            mDroppedLoot = b.ReadBoolean();
            mFireballRow = b.ReadLong();
            mFlatTires = b.ReadBoolean();
            mFlyingHealth = b.ReadLong();
            mFlyingMaxHealth = b.ReadLong();
            for (int i = 0; i < mFollowerZombieID.Length; i++)
            {
                mFollowerZombieIDSaved[i] = GameObject.LoadId(b);
            }
            mLeaderZombieIDSaved = GameObject.LoadId(b);
            mFrame = b.ReadLong();
            mGroanCounter = b.ReadLong();
            mHasGroundTrack = b.ReadBoolean();
            mHasObject = b.ReadBoolean();
            mHelmHealth = b.ReadLong();
            mHelmMaxHealth = b.ReadLong();
            mHitUmbrella = b.ReadBoolean();
            mIceTrapCounter = b.ReadLong();
            mInPool = b.ReadBoolean();
            mIsEating = b.ReadBoolean();
            mIsFireBall = b.ReadBoolean();
            mJustGotShotCounter = b.ReadLong();
            mLastPortalX = b.ReadLong();
            mMindControlled = b.ReadBoolean();
            mOnHighGround = b.ReadBoolean();
            mOrginalAnimRate = b.ReadFloat();
            mParticleOffsetX = b.ReadLong();
            mParticleOffsetY = b.ReadLong();
            mPhaseCounter = b.ReadLong();
            mPlayingSong = b.ReadBoolean();
            mPosX = b.ReadFloat();
            mPosY = b.ReadFloat();
            mPrevFrame = b.ReadLong();
            mPrevTransX = b.ReadFloat();
            mPrevTransY = b.ReadFloat();
            mRelatedZombieIDSaved = GameObject.LoadId(b);
            mScaleZombie = b.ReadFloat();
            mShieldHealth = b.ReadLong();
            mShieldJustGotShotCounter = b.ReadLong();
            mShieldMaxHealth = b.ReadLong();
            mShieldRecoilCounter = b.ReadLong();
            mSummonCounter = b.ReadLong();
            mSummonedDancers = b.ReadBoolean();
            mTargetCol = b.ReadLong();
            mTargetPlantIDSaved = GameObject.LoadId(b);
            mTargetRow = b.ReadLong();
            mUseLadderCol = b.ReadLong();
            mUsesClipping = b.ReadBoolean();
            mVelX = b.ReadFloat();
            mVelZ = b.ReadFloat();
            mYuckyFace = b.ReadBoolean();
            mYuckyFaceCounter = b.ReadLong();
            mZombieAge = b.ReadLong();
            mZombieAttackRect = b.ReadRect();
            mZombieHeight = (ZombieHeight)b.ReadLong();
            mZombiePhase = (ZombiePhase)b.ReadLong();
            mZombieRect = b.ReadRect();
            lastPlayedReanimName = b.ReadString();
            lastPlayedReanimAnimRate = b.ReadFloat();
            lastPlayedReanimBlendTime = b.ReadByte();
            lastPlayedReanimLoopType = (ReanimLoopType)b.ReadLong();
            mHasArm = b.ReadBoolean();
            mHasHead = b.ReadBoolean();
            mHasHelm = b.ReadBoolean();
            mHasShield = b.ReadBoolean();
            if (!mHasArm)
            {
                mHasArm = true;
                DropArm(16U);
            }
            if (!mHasHead)
            {
                mHasHead = true;
                DropHead(16U);
            }
            if (!mHasShield && mShieldType != ShieldType.None)
            {
                DropShield(16U);
            }
            if (!mHasHelm && mHelmType != HelmType.None)
            {
                DropHelm(16U);
            }
            if (!mHasObject && mZombieType == ZombieType.Digger)
            {
                DiggerLoseAxe();
            }
            if (mButteredCounter > 0 && !IsZombotany())
            {
                mBodyReanimID.AssignRenderGroupToTrack("zombie_butter", 0);
            }
            if (mZombieType == ZombieType.Bungee)
            {
                SetRow(aRow);
                mPosX = mBoard.GridToPixelX(mTargetCol, aRow);
                mPosY = GetPosYBasedOnRow(aRow);
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, aRow, 7);
            }
            return true;
        }

        public override void LoadingComplete()
        {
            justLoaded = true;
            base.LoadingComplete();
            for (int i = 0; i < mFollowerZombieID.Length; i++)
            {
                mFollowerZombieID[i] = (GameObject.GetObjectById(mFollowerZombieIDSaved[i]) as Zombie);
            }
            mLeaderZombie = (GameObject.GetObjectById(mLeaderZombieIDSaved) as Zombie);
            mTargetPlantID = (GameObject.GetObjectById(mTargetPlantIDSaved) as Plant);
            mRelatedZombieID = (GameObject.GetObjectById(mRelatedZombieIDSaved) as Zombie);
            if (mZombieType != ZombieType.Boss || lastPlayedReanimName != "anim_spawn_1")
            {
                PlayZombieReanim(ref lastPlayedReanimName, lastPlayedReanimLoopType, lastPlayedReanimBlendTime, lastPlayedReanimAnimRate);
            }
            int num = mJustGotShotCounter;
            TakeHelmDamage(0, 0U);
            if (IsFlying())
            {
                TakeFlyingDamage(0, 0U);
            }
            TakeShieldDamage(0, 0U);
            TakeBodyDamage(0, 0U, false);
            TakeDamage(0, 0U, false);
            UpdateDamageStates(0U);
            mJustGotShotCounter = num;
            if (mZombieType == ZombieType.Boss)
            {
                int bodyDamageIndex = GetBodyDamageIndex();
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (bodyDamageIndex == 1)
                {
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_head, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_HEAD_DAMAGE1);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_jaw, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_JAW_DAMAGE1);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerarm_hand, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_OUTERARM_HAND_DAMAGE1);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerarm_thumb2, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_OUTERARM_THUMB_DAMAGE1);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_innerleg_foot, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_FOOT_DAMAGE1);
                }
                else if (bodyDamageIndex == 2)
                {
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_head, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_HEAD_DAMAGE2);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_jaw, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_JAW_DAMAGE2);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerarm_hand, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_OUTERARM_HAND_DAMAGE2);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerarm_thumb2, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_OUTERARM_THUMB_DAMAGE2);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerleg_foot, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_FOOT_DAMAGE2);
                    ApplyBossSmokeParticles(true);
                }
            }
            Update();
            Update();
            doLoot = true;
            doParticle = true;
            justLoaded = false;
        }

        public void RemoveSurprise()
        {
            for (int i = 0; i < mAttachmentID.mEffectArray.Length; i++)
            {
                if (mAttachmentID.mEffectArray[i].mEffectType == EffectType.Reanim)
                {
                    Reanimation reanimation = (Reanimation)mAttachmentID.mEffectArray[i].mEffectID;
                    if (reanimation.mReanimationType == ReanimationType.ZombieSurprise && reanimation.mLoopCount == 1)
                    {
                        GlobalMembersAttachment.AttachmentDetach(ref mAttachmentID);
                        return;
                    }
                }
            }
        }

        public void Update()//3update
        {
            cachedZombieRectUpToDate = false;
            Debug.ASSERT(!mDead);
            //mZombieAge += 3;
            mZombieAge++;
            bool flag = mSurprised;
            if ((mApp.mGameScene != GameScenes.LevelIntro || mZombieType != ZombieType.Boss) && (!IsOnBoard() || !mBoard.mCutScene.ShouldRunUpsellBoard()) && mApp.mGameScene != GameScenes.Playing && IsOnBoard() && mFromWave != GameConstants.ZOMBIE_WAVE_WINNER)
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.ZombieBurned)
            {
                UpdateBurn();
            }
            else if (mZombiePhase == ZombiePhase.ZombieMowered)
            {
                UpdateMowered();
            }
            else if (mZombiePhase == ZombiePhase.ZombieDying)
            {
                UpdateDeath();
                UpdateZombieWalking();
            }
            else
            {
                if (mPhaseCounter > 0 && !IsImmobilizied())
                {
                    //mPhaseCounter -= 3;
                    mPhaseCounter--;
                }
                if (mApp.mGameScene == GameScenes.ZombiesWon)
                {
                    if (mBoard.mCutScene.ShowZombieWalking())
                    {
                        UpdateZombieChimney();
                        UpdateZombieWalkingIntoHouse();
                    }
                }
                else if (IsOnBoard())
                {
                    UpdatePlaying();
                }
                if (mZombieType == ZombieType.Bungee)
                {
                    UpdateZombieBungee();
                }
                if (mZombieType == ZombieType.Pogo)
                {
                    UpdateZombiePogo();
                }
                Animate();
            }
            //mJustGotShotCounter -= 3;
            mJustGotShotCounter--;
            if (mShieldJustGotShotCounter > 0)
            {
                //mShieldJustGotShotCounter -= 3;
                mShieldJustGotShotCounter--;
            }
            if (mShieldRecoilCounter > 0)
            {
                //mShieldRecoilCounter -= 3;
                mShieldRecoilCounter--;
            }
            if (mZombieFade > 0)
            {
                //mZombieFade -= 3;
                mZombieFade--;
                if (mZombieFade <= 0)
                {
                    DieNoLoot(true);
                }
            }
            mX = (int)mPosX;
            mY = (int)mPosY;
            GlobalMembersAttachment.AttachmentUpdateAndMove(ref mAttachmentID, mPosX, mPosY);
            UpdateReanim();
        }

        public void DieNoLoot(bool giveAchievements)
        {
            StopZombieSound();
            GlobalMembersAttachment.AttachmentDie(ref mAttachmentID);
            mApp.RemoveReanimation(ref mBodyReanimID);
            mApp.RemoveReanimation(ref mMoweredReanimID);
            mApp.RemoveReanimation(ref mSpecialHeadReanimID);
            mDead = true;
            TrySpawnLevelAward();
            if (mApp.mPlayerInfo != null && mFromWave != GameConstants.ZOMBIE_WAVE_UI && giveAchievements)
            {
                mApp.mPlayerInfo.mZombiesKilled += 1L;
            }
            if (mZombieType == ZombieType.Bobsled)
            {
                BobsledDie();
            }
            if (mZombieType == ZombieType.Bungee)
            {
                BungeeDie();
            }
            if (mZombieType == ZombieType.Boss)
            {
                BossDie();
            }
            if (giveAchievements && mZombieType == ZombieType.Gargantuar && mBoard != null)
            {
                mBoard.GrantAchievement(AchievementId.CrashoftheTitan, true);
            }
            if (mLeaderZombie != null && mLeaderZombie.mFollowerZombieID != null)
            {
                for (int i = 0; i < mLeaderZombie.mFollowerZombieID.Length; i++)
                {
                    if (mLeaderZombie.mFollowerZombieID[i] == this)
                    {
                        mLeaderZombie.mFollowerZombieID[i] = null;
                    }
                }
            }
        }

        public void DieWithLoot()
        {
            DieNoLoot(true);
            if (!doLoot)
            {
                return;
            }
            DropLoot();
        }

        public void Draw(Graphics g)
        {
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            if (mZombieHeight == ZombieHeight.GettingBungeeDropped)
            {
                return;
            }
            ZombieDrawPosition aDrawPos = default(ZombieDrawPosition);
            GetDrawPos(ref aDrawPos);
            if (mApp.mGameScene == GameScenes.ZombiesWon && !SetupDrawZombieWon(g))
            {
                return;
            }
            if (mIceTrapCounter > 0)
            {
                DrawIceTrap(g, ref aDrawPos, false);
            }
            if (mApp.mGameMode != GameMode.ChallengeInvisighoul || mFromWave == GameConstants.ZOMBIE_WAVE_UI)
            {
                if (mBodyReanimID != null)
                {
                    DrawReanim(g, ref aDrawPos, 0);
                }
                else
                {
                    DrawZombie(g, ref aDrawPos);
                }
            }
            if (mIceTrapCounter > 0)
            {
                DrawIceTrap(g, ref aDrawPos, true);
            }
            if (mButteredCounter > 0)
            {
                if (!mIsButterShowing)
                {
                    if (!IsZombotany())
                    {
                        mBodyReanimID.AssignRenderGroupToTrack("zombie_butter", 0);
                    }
                    mIsButterShowing = true;
                }
                if (IsZombotany() || mApp.mGameMode == GameMode.ChallengeInvisighoul)
                {
                    DrawButter(g, ref aDrawPos);
                }
            }
            else if (mIsButterShowing)
            {
                if (!IsZombotany())
                {
                    mBodyReanimID.AssignRenderGroupToTrack("zombie_butter", -1);
                }
                mIsButterShowing = false;
            }
            if (mAttachmentID != null)
            {
                Graphics theParticleGraphics = Graphics.GetNew(g);
                base.MakeParentGraphicsFrame(theParticleGraphics);
                theParticleGraphics.mTransY += (int)(aDrawPos.mBodyY * Constants.S);
                if (aDrawPos.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
                {
                    float aDrawHeight = 120f - aDrawPos.mClipHeight + 21f;
                    float anImageOffsetX = aDrawPos.mImageOffsetX;
                    float anOffsetY = aDrawPos.mImageOffsetY - 28f;
                    theParticleGraphics.ClipRect((int)((mX + anImageOffsetX - 400f) * Constants.S),
                                                 (int)((mY + anOffsetY) * Constants.S),
                                                 (int)(920f * Constants.S),
                                                 (int)(aDrawHeight * Constants.S));
                }
                GlobalMembersAttachment.AttachmentDraw(mAttachmentID, theParticleGraphics, false, true);
                theParticleGraphics.PrepareForReuse();
            }
            g.ClearClipRect();
        }

        public bool IsZombotany()
        {
            return mZombieType == ZombieType.SquashHead || mZombieType == ZombieType.WallnutHead || mZombieType == ZombieType.TallnutHead || mZombieType == ZombieType.PeaHead || mZombieType == ZombieType.JalapenoHead || mZombieType == ZombieType.GatlingHead;
        }

        public void DrawZombie(Graphics g, ref ZombieDrawPosition theDrawPos)
        {
            Image theImage = null;
            int theRow = 0;
            bool flag = false;
            ZombieType zombieType = mZombieType;
            switch (zombieType)
            {
                case ZombieType.Normal:
                case ZombieType.Flag:
                case ZombieType.TrafficCone:
                case ZombieType.Pail:
                case ZombieType.Newspaper:
                case ZombieType.Door:
                case ZombieType.Football:
                case ZombieType.DolphinRider:
                    break;
                case ZombieType.Polevaulter:
                case ZombieType.Dancer:
                case ZombieType.BackupDancer:
                case ZombieType.DuckyTube:
                case ZombieType.Snorkel:
                case ZombieType.Zamboni:
                case ZombieType.Bobsled:
                    goto IL_58;
                default:
                    if (zombieType != ZombieType.Ladder)
                    {
                        goto IL_58;
                    }
                    break;
            }
            flag = true;
            goto IL_5E;
            IL_58:
            Debug.ASSERT(false);
            IL_5E:
            if (flag)
            {
                DrawZombieWithParts(g, ref theDrawPos);
                return;
            }
            DrawZombiePart(g, theImage, mFrame, theRow, ref theDrawPos);
        }

        public void DrawZombieWithParts(Graphics g, ref ZombieDrawPosition theDrawPos)
        {
        }

        public void DrawZombiePart(Graphics g, Image theImage, int theFrame, int theRow, ref ZombieDrawPosition theDrawPos)
        {
            int celWidth = theImage.GetCelWidth();
            int celHeight = theImage.GetCelHeight();
            float num = theDrawPos.mImageOffsetX;
            float num2 = theDrawPos.mImageOffsetY + theDrawPos.mBodyY;
            if (mZombiePhase == ZombiePhase.PolevaulterInVault)
            {
                num += -120f;
                num2 += -120f;
            }
            if (mZombiePhase == ZombiePhase.DiggerTunneling)
            {
                num2 += 50f;
            }
            if (mZombieType == ZombieType.Zamboni)
            {
                num2 += -19f;
            }
            float num3 = celHeight;
            if (theDrawPos.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
            {
                num3 = TodCommon.ClampFloat(celHeight - theDrawPos.mClipHeight, 0f, celHeight);
            }
            int num4 = 255;
            if (mZombieFade >= 0)
            {
                num4 = TodCommon.ClampInt((int)(255 * mZombieFade / 30f), 0, 255);
                g.SetColorizeImages(true);
                g.SetColor(new SexyColor(255, 255, 255, num4));
            }
            bool flag = false;
            if (mZombiePhase == ZombiePhase.DancerDancingIn || mZombiePhase == ZombiePhase.DancerDancingLeft)
            {
                int dancerFrame = GetDancerFrame();
                if (!mIsEating && (dancerFrame == 12 || dancerFrame == 13 || dancerFrame == 14 || dancerFrame == 18 || dancerFrame == 19 || dancerFrame == 20))
                {
                    flag = true;
                    num -= 30f;
                }
            }
            if (flag)
            {
                num = -num;
            }
            TRect theSrcRect = new TRect(theFrame * celWidth, theRow * celHeight, celWidth, (int)num3);
            TRect theDestRect = new TRect((int)num, (int)num2, celWidth, (int)num3);
            if (mZombiePhase == ZombiePhase.ZombieBurned)
            {
                if (mMindControlled)
                {
                    flag = true;
                }
                g.SetColorizeImages(true);
                g.SetColor(SexyColor.Black);
                g.DrawImageMirror(theImage, theDestRect, theSrcRect, flag);
            }
            else if (mMindControlled)
            {
                flag = true;
                g.SetColorizeImages(true);
                SexyColor zombie_MINDCONTROLLED_COLOR = GameConstants.ZOMBIE_MINDCONTROLLED_COLOR;
                zombie_MINDCONTROLLED_COLOR.mAlpha = num4;
                g.SetColor(zombie_MINDCONTROLLED_COLOR);
                g.DrawImageMirror(theImage, theDestRect, theSrcRect, flag);
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                g.DrawImageMirror(theImage, theDestRect, theSrcRect, flag);
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            }
            else if (mChilledCounter > 0 || mIceTrapCounter > 0)
            {
                g.SetColorizeImages(true);
                g.SetColor(new SexyColor(75, 75, 255, num4));
                g.DrawImageMirror(theImage, theDestRect, theSrcRect, flag);
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                g.DrawImageMirror(theImage, theDestRect, theSrcRect, flag);
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            }
            else
            {
                g.DrawImageMirror(theImage, theDestRect, theSrcRect, flag);
            }
            if (mJustGotShotCounter > 0)
            {
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                g.SetColorizeImages(true);
                int num5 = mJustGotShotCounter * 10;
                g.SetColor(new SexyColor(num5, num5, num5, 255));
                g.DrawImageMirror(theImage, theDestRect, theSrcRect, flag);
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            }
            g.SetColorizeImages(false);
        }

        public void DrawBungeeCord(Graphics g, int theOffsetX, int theOffsetY)
        {
            int aCordCelHeight = (int)(AtlasResources.IMAGE_BUNGEECORD.GetCelHeight() * mScaleZombie);
            float aPosX = 0f;
            float aPosY = 0f;
            GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_bungi_body, ref aPosX, ref aPosY);
            bool aSetClip = false;
            if (IsOnBoard() && mApp.IsFinalBossLevel())
            {
                Zombie bossZombie = mBoard.GetBossZombie();
                int aClipAmount = 55;
                if (bossZombie.mZombiePhase == ZombiePhase.BossBungeesLeave)
                {
                    Reanimation reanimation = mApp.ReanimationGet(bossZombie.mBodyReanimID);
                    aClipAmount = (int)TodCommon.TodAnimateCurveFloatTime(0f, 0.2f, reanimation.mAnimTime, 55f, 0f, TodCurves.Linear);
                }
                if (mTargetCol > bossZombie.mTargetCol)
                {
                    g.SetClipRect(new TRect(-g.mTransX, (int)(aClipAmount * Constants.S) - g.mTransY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT));
                    aSetClip = true;
                }
            }
            //if (mZombiePhase == ZombiePhase.ZombieBurned)
            //{
                //g.SetColor(new Color(0, 0, 0, TodCommon.ClampInt((int)(255 * mZombieFade / 30f), 0, 255)));
                //g.SetColorizeImages(true); /* 删除此段以修复炸蹦极导致全场变黑问题 */
            //}
            for (float y = aPosY - aCordCelHeight; y > (float)(-aCordCelHeight); y -= aCordCelHeight)
            {
                float thePosX = theOffsetX + Constants.Zombie_Bungee_Offset.X - 4f / mScaleZombie;
                float thePosY = y - mPosY - Constants.Zombie_Bungee_Offset.Y;
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_BUNGEECORD, thePosX, thePosY, mScaleZombie, mScaleZombie);
            }
            if (mZombiePhase == ZombiePhase.ZombieBurned)
            {
                g.SetColor(g.mColor, false);
                g.SetColorizeImages(g.mColorizeImages);
            }
            if (aSetClip)
            {
                g.ClearClipRect();
            }
        }

        public void TakeDamage(int theDamage, uint theDamageFlags, bool isPyromaniac)
        {
            if (mZombiePhase == ZombiePhase.JackInTheBoxPopping)
            {
                return;
            }
            if (IsDeadOrDying())
            {
                return;
            }
            int num = theDamage;
            if (IsFlying())
            {
                num = TakeFlyingDamage(theDamage, theDamageFlags);
            }
            if (num > 0 && mShieldType != ShieldType.None && !TodCommon.TestBit(theDamageFlags, 0))
            {
                num = TakeShieldDamage(theDamage, theDamageFlags);
                if (TodCommon.TestBit(theDamageFlags, 1))
                {
                    num = theDamage;
                }
            }
            if (num > 0 && mHelmType != HelmType.None)
            {
                num = TakeHelmDamage(theDamage, theDamageFlags);
            }
            if (num > 0)
            {
                TakeBodyDamage(num, theDamageFlags, isPyromaniac);
            }
        }

        public void SetRow(int theRow)
        {
            Debug.ASSERT(theRow >= 0 && theRow < Constants.MAX_GRIDSIZEY);
            mRow = theRow;
            mRenderOrder = Board.MakeRenderOrder(RenderLayer.Zombie, mRow, 4);
        }

        public float GetPosYBasedOnRow(int theRow)
        {
            if (!IsOnBoard())
            {
                return 0f;
            }
            if (IsOnHighGround())
            {
                if (mAltitude < Constants.HIGH_GROUND_HEIGHT)
                {
                    mZombieHeight = ZombieHeight.UpToHighGround;
                }
                mOnHighGround = true;
            }
            float aPosY = mBoard.GetPosYBasedOnRow(mPosX + 40f, theRow) - 30f;
            switch (mZombieType)
            {
            case ZombieType.Balloon:
                aPosY -= 30f;
                break;
            case ZombieType.Pogo:
                aPosY -= 16f;
                break;
            }
            return aPosY;
        }

        public void ApplyChill(bool theIsIceTrap)
        {
            if (!CanBeChilled())
            {
                return;
            }
            if (mChilledCounter == 0)
            {
                mApp.PlayFoley(FoleyType.Frozen);
            }
            int num = 1000;
            if (theIsIceTrap)
            {
                num = 2000;
            }
            mChilledCounter = Math.Max(num, mChilledCounter);
            UpdateAnimSpeed();
        }

        public void UpdateZombieBungee()//3update
        {
            if (IsDeadOrDying())
            {
                return;
            }
            if (IsImmobilizied())
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.BungeeDiving || mZombiePhase == ZombiePhase.BungeeDivingScreaming)
            {
                float num = GameConstants.BUNGEE_ZOMBIE_HEIGHT - 404f;
                float aOldAltitude = mAltitude;
                //mAltitude -= 24f;
                mAltitude -= 8f;
                if (mAltitude <= num && aOldAltitude > num && mRelatedZombieID == null)
                {
                    mApp.PlayFoley(FoleyType.Grassstep);
                }
                BungeeLanding();
            }
            else if (mZombiePhase == ZombiePhase.BungeeAtBottom)
            {
                if (mPhaseCounter <= 0)
                {
                    BungeeStealTarget();
                    mZombiePhase = ZombiePhase.BungeeGrabbing;
                }
            }
            else if (mZombiePhase == ZombiePhase.BungeeGrabbing)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.mLoopCount > 0)
                {
                    BungeeLiftTarget();
                    mZombiePhase = ZombiePhase.BungeeRising;
                }
            }
            else if (mZombiePhase == ZombiePhase.BungeeHitOuchy)
            {
                if (mPhaseCounter <= 0)
                {
                    DieWithLoot();
                }
            }
            else if (mZombiePhase == ZombiePhase.BungeeRising)
            {
                //mAltitude += 24f;
                mAltitude += 8f;
                if (mAltitude >= 600f)
                {
                    DieNoLoot(false);
                }
            }
            else if (mZombiePhase == ZombiePhase.BungeeCutscene)
            {
                mAltitude = TodCommon.TodAnimateCurve(200, 0, mPhaseCounter, 40, 0, TodCurves.SinWave);
                if (mPhaseCounter <= 0)
                {
                    mPhaseCounter = 200;
                }
            }
            mX = (int)mPosX;
            mY = (int)mPosY;
        }

        public void BungeeLanding()
        {
            if (mZombiePhase == ZombiePhase.BungeeDiving && mAltitude < 1500f && !mApp.IsFinalBossLevel())
            {
                mApp.PlayFoley(FoleyType.BungeeScream);
                mZombiePhase = ZombiePhase.BungeeDivingScreaming;
            }
            if (mAltitude > 40f)
            {
                return;
            }
            Plant plant = mBoard.FindUmbrellaPlant(mTargetCol, mRow);
            if (plant != null)
            {
                mApp.PlaySample(Resources.SOUND_BOING);
                mApp.PlayFoley(FoleyType.Umbrella);
                plant.DoSpecial();
                mZombiePhase = ZombiePhase.BungeeRising;
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.Top, 0, 1);
                mHitUmbrella = true;
                return;
            }
            mBoard.GetTopPlantAt(mTargetCol, mRow, TopPlant.BungeeOrder);
            if (mAltitude > 0f)
            {
                return;
            }
            mAltitude = 0f;
            Zombie zombie = null;
            int num = mBoard.mZombies.IndexOf(mRelatedZombieID);
            if (num != -1)
            {
                zombie = mBoard.mZombies[num];
            }
            if (zombie != null)
            {
                zombie.mZombieHeight = ZombieHeight.ZombieNormal;
                zombie.StartWalkAnim(0);
                mRelatedZombieID = null;
                mZombiePhase = ZombiePhase.BungeeRising;
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_raise, ReanimLoopType.PlayOnceAndHold, 0, 36f);
                return;
            }
            mZombiePhase = ZombiePhase.BungeeAtBottom;
            mPhaseCounter = 300;
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 5, 24f);
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            reanimation.mAnimTime = 0.5f;
        }

        public bool EffectedByDamage(uint theDamageRangeFlags)
        {
            if (!TodCommon.TestBit(theDamageRangeFlags, 5) && IsDeadOrDying())
            {
                return false;
            }
            if (TodCommon.TestBit(theDamageRangeFlags, 7))
            {
                if (!mMindControlled)
                {
                    return false;
                }
            }
            else if (mMindControlled)
            {
                return false;
            }
            if (mZombieType == ZombieType.Bungee && mZombiePhase != ZombiePhase.BungeeAtBottom && mZombiePhase != ZombiePhase.BungeeGrabbing)
            {
                return false;
            }
            if (mZombieHeight == ZombieHeight.GettingBungeeDropped)
            {
                return false;
            }
            if (mZombieType == ZombieType.Boss)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (mZombiePhase == ZombiePhase.BossHeadEnter && reanimation.mAnimTime < 0.5f)
                {
                    return false;
                }
                if (mZombiePhase == ZombiePhase.BossHeadLeave && reanimation.mAnimTime > 0.5f)
                {
                    return false;
                }
                if (mZombiePhase != ZombiePhase.BossHeadIdleBeforeSpit && mZombiePhase != ZombiePhase.BossHeadIdleAfterSpit && mZombiePhase != ZombiePhase.BossHeadSpit)
                {
                    return false;
                }
            }
            if (mZombieType == ZombieType.Bobsled && GetBobsledPosition() > 0)
            {
                return false;
            }
            if (mZombiePhase == ZombiePhase.PolevaulterInVault || mZombiePhase == ZombiePhase.ImpGettingThrown || mZombiePhase == ZombiePhase.DiggerRising || mZombiePhase == ZombiePhase.DiggerTunnelingPauseWithoutAxe || mZombiePhase == ZombiePhase.DiggerRiseWithoutAxe || mZombiePhase == ZombiePhase.DolphinIntoPool || mZombiePhase == ZombiePhase.DolphinInJump || mZombiePhase == ZombiePhase.SnorkelIntoPool || mZombiePhase == ZombiePhase.BalloonPopping || mZombiePhase == ZombiePhase.RisingFromGrave || mZombiePhase == ZombiePhase.BobsledCrashing || mZombiePhase == ZombiePhase.DancerRising)
            {
                return TodCommon.TestBit(theDamageRangeFlags, 4);
            }
            if (mZombieType != ZombieType.Bobsled && mZombieType != ZombieType.Boss && GetZombieRect().mX > Constants.WIDE_BOARD_WIDTH)
            {
                return false;
            }
            bool flag = mZombieType == ZombieType.Snorkel && mInPool && !mIsEating;
            if (TodCommon.TestBit(theDamageRangeFlags, 2) && flag)
            {
                return true;
            }
            bool flag2 = mZombiePhase == ZombiePhase.DiggerTunneling;
            return (TodCommon.TestBit(theDamageRangeFlags, 6) && flag2) || (TodCommon.TestBit(theDamageRangeFlags, 1) && IsFlying()) || (TodCommon.TestBit(theDamageRangeFlags, 0) && !IsFlying() && !flag && !flag2);
        }

        public void PickRandomSpeed()
        {
            if (mZombiePhase == ZombiePhase.SnorkelWalkingInPool)
            {
                mVelX = 0.3f;
            }
            else if (mZombiePhase == ZombiePhase.DiggerWalking)
            {
                if (mApp.IsIZombieLevel())
                {
                    mVelX = 0.23f;
                }
                else
                {
                    mVelX = 0.12f;
                }
            }
            else if (mZombieType == ZombieType.Imp && mApp.IsIZombieLevel())
            {
                mVelX = 0.9f;
            }
            else if (mZombiePhase == ZombiePhase.YetiRunning)
            {
                mVelX = 0.8f;
            }
            else if (mZombieType == ZombieType.Yeti)
            {
                mVelX = 0.4f;
            }
            else if (mZombieType == ZombieType.Dancer || mZombieType == ZombieType.BackupDancer || mZombieType == ZombieType.Pogo || mZombieType == ZombieType.Flag)
            {
                mVelX = 0.45f;
            }
            else if (mZombiePhase == ZombiePhase.DiggerTunneling || mZombiePhase == ZombiePhase.PolevaulterPreVault || mZombieType == ZombieType.Football || mZombieType == ZombieType.Snorkel || mZombieType == ZombieType.JackInTheBox)
            {
                mVelX = TodCommon.RandRangeFloat(0.66f, 0.68f);
            }
            else if (mZombiePhase == ZombiePhase.LadderCarrying || mZombieType == ZombieType.SquashHead)
            {
                mVelX = TodCommon.RandRangeFloat(0.79f, 0.81f);
            }
            else if (mZombiePhase == ZombiePhase.NewspaperMad || mZombiePhase == ZombiePhase.DolphinWalking || mZombiePhase == ZombiePhase.DolphinWalkingWithoutDolphin)
            {
                mVelX = TodCommon.RandRangeFloat(0.89f, 0.91f);
            }
            else
            {
                mVelX = TodCommon.RandRangeFloat(0.23f, 0.37f);
                if (mVelX < 0.3)
                {
                    mAnimTicksPerFrame = 12;
                }
                else
                {
                    mAnimTicksPerFrame = 15;
                }
            }
            UpdateAnimSpeed();
        }

        public void UpdateZombiePolevaulter()//3update
        {
            if (mZombiePhase == ZombiePhase.PolevaulterPreVault && mHasHead && mZombieHeight == ZombieHeight.ZombieNormal)
            {
                Plant plant = FindPlantTarget(ZombieAttackType.Vault);
                if (plant != null)
                {
                    if (mBoard.GetLadderAt(plant.mPlantCol, plant.mRow) != null)
                    {
                        if (mBoard.GridToPixelX(plant.mPlantCol, plant.mRow) + 40 > mPosX && mZombieHeight == ZombieHeight.ZombieNormal && mUseLadderCol != plant.mPlantCol)
                        {
                            mZombieHeight = ZombieHeight.UpLadder;
                            mUseLadderCol = plant.mPlantCol;
                        }
                        return;
                    }
                    mZombiePhase = ZombiePhase.PolevaulterInVault;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jump, ReanimLoopType.PlayOnceAndHold, 20, 24f);
                    Reanimation aReanim = mApp.ReanimationGet(mBodyReanimID);
                    float aAnimDuration = aReanim.mFrameCount / aReanim.mAnimRate * 100f;
                    int aJumpDistance = mX - plant.mX - 80;
                    if (mApp.IsWallnutBowlingLevel())
                    {
                        aJumpDistance = 0;
                    }
                    mVelX = aJumpDistance / aAnimDuration;
                    mHasObject = false;
                }
                if (mApp.IsIZombieLevel() && mBoard.mChallenge.IZombieGetBrainTarget(this) != null)
                {
                    mZombiePhase = ZombiePhase.PolevaulterPostVault;
                    StartWalkAnim(0);
                    return;
                }
            }
            else if (mZombiePhase == ZombiePhase.PolevaulterInVault)
            {
                Reanimation aReanim2 = mApp.ReanimationGet(mBodyReanimID);
                bool flag = false;
                if (aReanim2.mAnimTime > 0.6f && aReanim2.mAnimTime <= 0.7f)
                {
                    Plant plant2 = FindPlantTarget(ZombieAttackType.Vault);
                    if (plant2 != null && plant2.mSeedType == SeedType.Tallnut)
                    {
                        mApp.PlayFoley(FoleyType.Bonk);
                        flag = true;
                        mApp.AddTodParticle(plant2.mX + 60, plant2.mY - 20, mRenderOrder + 1, ParticleEffect.TallNutBlock);
                        mPosX = plant2.mX;
                        mPosY -= 30f;
                        mZombieHeight = ZombieHeight.Falling;
                    }
                }
                if (aReanim2.mLoopCount > 0)
                {
                    flag = true;
                    mPosX -= 150f;
                }
                if (aReanim2.ShouldTriggerTimedEvent(0.2f))
                {
                    mApp.PlayFoley(FoleyType.Grassstep);
                }
                if (aReanim2.ShouldTriggerTimedEvent(0.4f))
                {
                    mApp.PlayFoley(FoleyType.Polevault);
                }
                if (flag)
                {
                    mX = (int)mPosX;
                    mZombiePhase = ZombiePhase.PolevaulterPostVault;
                    mZombieAttackRect = new TRect(50, 0, 20, 115);
                    StartWalkAnim(0);
                    return;
                }
                float aOldPosX = mPosX;
                mPosX -= 150f * aReanim2.mAnimTime;
                mPosY = GetPosYBasedOnRow(mRow);
                mPosX = aOldPosX;
            }
        }

        public void UpdateZombieDolphinRider()//3update
        {
            if (IsTangleKelpTarget())
            {
                return;
            }
            bool flag = IsWalkingBackwards();
            mUsesClipping = false;
            if (mZombiePhase == ZombiePhase.DolphinWalking && !flag)
            {
                if (mX > 800 && mX <= 820)
                {
                    mZombiePhase = ZombiePhase.DolphinIntoPool;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jumpinpool, ReanimLoopType.PlayOnceAndHold, 20, 16f);
                }
            }
            else if (mZombiePhase == ZombiePhase.DolphinIntoPool)
            {
                mUsesClipping = true;
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.ShouldTriggerTimedEvent(0.56f))
                {
                    Reanimation reanimation2 = mApp.AddReanimation(mX - 83, mY + 73, mRenderOrder + 1, ReanimationType.Splash);
                    reanimation2.OverrideScale(1.2f, 0.8f);
                    mApp.AddTodParticle(mX - 46, mY + 115, mRenderOrder + 1, ParticleEffect.PlantingPool);
                    mApp.PlayFoley(FoleyType.ZombieEnteringWater);
                }
                if (reanimation.mLoopCount > 0)
                {
                    mZombiePhase = ZombiePhase.DolphinRiding;
                    mInPool = true;
                    mPosX -= 70f;
                    mZombieAttackRect = new TRect(-29, 0, 70, 115);
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_ride, ReanimLoopType.LoopFullLastFrame, 0, 12f);
                }
            }
            else if (mZombiePhase == ZombiePhase.DolphinRiding)
            {
                if (mX <= 120)
                {
                    mZombieHeight = ZombieHeight.OutOfPool;
                    mZombiePhase = ZombiePhase.DolphinWalking;
                    mAltitude = -40f;
                    PoolSplash(false);
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_walkdolphin, ReanimLoopType.Loop, 0, 0f);
                    PickRandomSpeed();
                    return;
                }
                if (mHasHead && !IsTanglekelpTarget())
                {
                    Plant plant = FindPlantTarget(ZombieAttackType.Vault);
                    if (plant != null)
                    {
                        mApp.PlayFoley(FoleyType.DolphinBeforeJumping);
                        mApp.PlayFoley(FoleyType.PlantWater);
                        mZombiePhase = ZombiePhase.DolphinInJump;
                        mPhaseCounter = GameConstants.DOLPHIN_JUMP_TIME;
                        mVelX = 0.5f;
                        PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_dolphinjump, ReanimLoopType.PlayOnceAndHold, 0, 10f);
                    }
                }
            }
            else if (mZombiePhase == ZombiePhase.DolphinInJump)
            {
                Reanimation reanimation3 = mApp.ReanimationGet(mBodyReanimID);
                bool flag2 = false;
                mAltitude = TodCommon.TodAnimateCurveFloat(GameConstants.DOLPHIN_JUMP_TIME, 0, mPhaseCounter, 0f, 10f, TodCurves.Linear);
                if (reanimation3.ShouldTriggerTimedEvent(0.3f))
                {
                    Plant plant2 = FindPlantTarget(ZombieAttackType.Vault);
                    if (plant2 != null && plant2.mSeedType == SeedType.Tallnut)
                    {
                        mApp.PlayFoley(FoleyType.Bonk);
                        flag2 = true;
                        mApp.AddTodParticle(plant2.mX + 60, plant2.mY - 20, mRenderOrder + 1, ParticleEffect.TallNutBlock);
                        mPosX = plant2.mX + 25f;
                        mAltitude = 30f;
                        mZombieHeight = ZombieHeight.Falling;
                    }
                }
                else if (reanimation3.ShouldTriggerTimedEvent(0.49f))
                {
                    Reanimation reanimation4 = mApp.AddReanimation(mX - 63, mY + 73, mRenderOrder + 1, ReanimationType.Splash);
                    reanimation4.OverrideScale(1.2f, 0.8f);
                    mApp.AddTodParticle(mX - 26, mY + 115, mRenderOrder + 1, ParticleEffect.PlantingPool);
                    mApp.PlayFoley(FoleyType.ZombieEnteringWater);
                    mVelX = 0f;
                }
                else if (reanimation3.mLoopCount > 0)
                {
                    flag2 = true;
                    mPosX -= 94f;
                    mAltitude = 0f;
                }
                if (flag2)
                {
                    mZombiePhase = ZombiePhase.DolphinWalkingInPool;
                    mZombieAttackRect = new TRect(30, 0, 30, 115);
                    mZombieRect = new TRect(20, 0, 42, 115);
                    StartWalkAnim(0);
                }
            }
            else if (mZombiePhase == ZombiePhase.DolphinWalkingInPool)
            {
                if (mX <= 140 && !flag)
                {
                    mZombieHeight = ZombieHeight.OutOfPool;
                    mZombiePhase = ZombiePhase.DolphinWalkingWithoutDolphin;
                    mAltitude = -40f;
                    PoolSplash(false);
                    PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, 0, 0f);
                    PickRandomSpeed();
                }
                else if (mX > 770 && flag)
                {
                    mZombieHeight = ZombieHeight.OutOfPool;
                    mZombiePhase = ZombiePhase.DolphinWalkingWithoutDolphin;
                    mAltitude = -40f;
                    PoolSplash(false);
                    PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, 0, 0f);
                    PickRandomSpeed();
                }
            }
            if (mZombiePhase == ZombiePhase.DolphinWalkingWithoutDolphin)
            {
                mUsesClipping = (mAltitude < 0f);
            }
        }

        public void PickBungeeZombieTarget(int theColumn)
        {
            bool aAllowSunflowerTarget = true;
            if (CountBungeesTargetingSunFlowers() == mBoard.CountSunFlowers() - 1)
            {
                aAllowSunflowerTarget = false;
            }
            int aPickCount = 0;
            for (int i = 0; i < Zombie.aPicks.Length; i++)
            {
                Zombie.aPicks[i].Reset();
            }
            for (int j = 0; j < Constants.GRIDSIZEX; j++)
            {
                if (theColumn == -1 || theColumn == j)
                {
                    for (int k = 0; k < Constants.MAX_GRIDSIZEY; k++)
                    {
                        int aWeight = 1;
                        if (mBoard.GetGraveStoneAt(j, k) == null && mBoard.mGridSquareType[j, k] != GridSquareType.Dirt)
                        {
                            Plant topPlantAt = mBoard.GetTopPlantAt(j, k, TopPlant.BungeeOrder);
                            if (topPlantAt != null)
                            {
                                if ((!aAllowSunflowerTarget && topPlantAt.MakesSun())
                                    || topPlantAt.mSeedType == SeedType.Gravebuster
                                    || topPlantAt.mSeedType == SeedType.Cobcannon)
                                {
                                    continue;
                                }
                                aWeight = 10000;
                            }
                            if (!mBoard.BungeeIsTargetingCell(j, k))
                            {
                                Zombie.aPicks[aPickCount].mX = j;
                                Zombie.aPicks[aPickCount].mY = k;
                                Zombie.aPicks[aPickCount].mWeight = aWeight;
                                aPickCount++;
                            }
                        }
                    }
                }
            }
            if (aPickCount == 0)
            {
                DieNoLoot(false);
                return;
            }
            TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(Zombie.aPicks, aPickCount);
            mTargetCol = todWeightedGridArray.mX;
            SetRow(todWeightedGridArray.mY);
            mPosX = mBoard.GridToPixelX(mTargetCol, mRow);
            mPosY = GetPosYBasedOnRow(mRow);
        }

        public int CountBungeesTargetingSunFlowers()
        {
            int num = 0;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && !zombie.IsDeadOrDying() && zombie.mZombieType == ZombieType.Bungee && zombie.mTargetCol != -1)
                {
                    Plant topPlantAt = mBoard.GetTopPlantAt(zombie.mTargetCol, zombie.mRow, TopPlant.BungeeOrder);
                    if (topPlantAt != null && topPlantAt.MakesSun())
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public Plant FindPlantTarget(ZombieAttackType theAttackType)
        {
            TRect aAttackRect = GetZombieAttackRect();
            for (int i = 0; i < mBoard.mPlants.Count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead && mRow == aPlant.mRow)
                {
                    TRect aPlantRect = aPlant.GetPlantRect();
                    int rectOverlap = GameConstants.GetRectOverlap(aAttackRect, aPlantRect);
                    if (rectOverlap >= ((mZombieType == ZombieType.Digger) ? 5 : 20) && CanTargetPlant(aPlant, theAttackType))
                    {
                        return aPlant;
                    }
                }
            }
            return null;
        }

        public void CheckSquish(ZombieAttackType theAttackType)//3update
        {
            TRect aAttackRect = GetZombieAttackRect();
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead && mRow == aPlant.mRow)
                {
                    TRect aPlantRect = aPlant.GetPlantRect();
                    int rectOverlap = GameConstants.GetRectOverlap(aAttackRect, aPlantRect);
                    if (rectOverlap >= 20 && CanTargetPlant(aPlant, theAttackType) && !aPlant.IsSpiky())
                    {
                        SquishAllInSquare(aPlant.mPlantCol, aPlant.mRow, theAttackType);
                        break;
                    }
                }
            }
            if (mApp.IsIZombieLevel())
            {
                GridItem gridItem = mBoard.mChallenge.IZombieGetBrainTarget(this);
                if (gridItem != null)
                {
                    mBoard.mChallenge.IZombieSquishBrain(gridItem);
                }
            }
        }

        public void RiseFromGrave(int theCol, int theRow)
        {
            Debug.ASSERT(mZombiePhase == ZombiePhase.ZombieNormal);
            mPosX = mBoard.GridToPixelX(theCol, mRow) - 25;
            mPosY = GetPosYBasedOnRow(theRow);
            SetRow(theRow);
            mX = (int)mPosX;
            mY = (int)mPosY;
            mZombiePhase = ZombiePhase.RisingFromGrave;
            mPhaseCounter = 150;
            mAltitude = -200f;
            mUsesClipping = true;
            if (mBoard.StageHasPool())
            {
                mInPool = true;
                mPhaseCounter = 50;
                mAltitude = -150f;
                mZombieHeight = ZombieHeight.ZombieNormal;
                StartWalkAnim(0);
                ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_duckytube, false);
                ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_whitewater, false);
                ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, false);
                ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm3, false);
                Reanimation aReanim = mApp.ReanimationGet(mBodyReanimID);
                TodParticleSystem todParticleSystem = mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.ZombieSeaweed);
                OverrideParticleScale(todParticleSystem);
                if (mZombieType == ZombieType.TrafficCone && todParticleSystem != null)
                {
                    aReanim.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_cone, ref todParticleSystem, 37f * Constants.S, 20f * Constants.S);
                }
                else if (mZombieType == ZombieType.Pail && todParticleSystem != null)
                {
                    aReanim.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_bucket, ref todParticleSystem, 37f * Constants.S, 20f * Constants.S);
                }
                else if (todParticleSystem != null)
                {
                    aReanim.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head1, ref todParticleSystem, 30f * Constants.S, 20f * Constants.S);
                }
                PoolSplash(false);
                return;
            }
            int aParticleX = (int)mPosX + 60;
            int aParticleY = (int)mPosY + 110;
            if (IsOnHighGround())
            {
                aParticleY -= Constants.HIGH_GROUND_HEIGHT;
            }
            int aRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, theRow, 0);
            if (mApp.IsWhackAZombieLevel())
            {
                mApp.PlayFoley(FoleyType.DirtRise);
                mApp.AddTodParticle(aParticleX, aParticleY, aRenderOrder, ParticleEffect.WhackAZombieRise);
            }
            else
            {
                mApp.PlayFoley(FoleyType.GravestoneRumble);
                mApp.AddTodParticle(aParticleX, aParticleY, aRenderOrder, ParticleEffect.ZombieRise);
            }
            
        }

        public void UpdateZombieRiseFromGrave()//3update
        {
            if (mInPool)
            {
                mBodyReanimID.mClip = false;
                mAltitude = TodCommon.TodAnimateCurve(50, 0, mPhaseCounter, -150, -40, TodCurves.Linear) * mScaleZombie;
                mUsesClipping = true;
            }
            else
            {
                mBodyReanimID.mClip = true;
                mAltitude = TodCommon.TodAnimateCurve(50, 0, mPhaseCounter, -200, 0, TodCurves.Linear);
                mUsesClipping = (mAltitude < 0f);
            }
            if (mPhaseCounter <= 0)
            {
                mZombiePhase = ZombiePhase.ZombieNormal;
                if (IsOnHighGround())
                {
                    mAltitude = Constants.HIGH_GROUND_HEIGHT;
                }
                if (mInPool)
                {
                    ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_duckytube, true);
                    ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_whitewater, true);
                    ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, true);
                    ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm3, true);
                }
            }
        }

        public void UpdateDamageStates(uint theDamageFlags)
        {
            if (!CanLoseBodyParts())
            {
                return;
            }
            if (mHasArm && mBodyHealth < 2 * mBodyMaxHealth / 3 && mBodyHealth > 0)
            {
                DropArm(theDamageFlags);
            }
            if (mHasHead && mBodyHealth < mBodyMaxHealth / 3)
            {
                DropHead(theDamageFlags);
                DropLoot();
                StopZombieSound();
                if (mBoard.HasLevelAwardDropped())
                {
                    PlayDeathAnim(theDamageFlags);
                }
                if (mZombiePhase == ZombiePhase.SnorkelWalkingInPool)
                {
                    DieNoLoot(false);
                }
            }
        }

        public void UpdateZombiePool()//3update
        {
            if (mZombieHeight == ZombieHeight.OutOfPool)
            {
                //mAltitude += 3f;
                mAltitude++;
                if (mZombieType == ZombieType.Snorkel)
                {
                    //mAltitude += 3f;
                    mAltitude++;
                }
                if (mAltitude >= 0f)
                {
                    mAltitude = 0f;
                    mZombieHeight = ZombieHeight.ZombieNormal;
                    mInPool = false;
                    return;
                }
            }
            else if (mZombieHeight == ZombieHeight.InToPool)
            {
                //mAltitude -= 3f;
                mAltitude--;
                int aDepth = -40;
                aDepth *= (int)mScaleZombie;
                if (mAltitude <= aDepth)
                {
                    mAltitude = aDepth;
                    mZombieHeight = ZombieHeight.ZombieNormal;
                    StartWalkAnim(0);
                    return;
                }
            }
            else if (mZombieHeight == ZombieHeight.DraggedUnder)
            {
                //mAltitude -= 3f;
                mAltitude--;
            }
        }

        public void CheckForPool()//3update
        {
            if (!Zombie.ZombieTypeCanGoInPool(mZombieType))
            {
                return;
            }
            if (IsFlying())
            {
                return;
            }
            if (mZombieType == ZombieType.DolphinRider || mZombieType == ZombieType.Snorkel)
            {
                return;
            }
            if (mZombieHeight == ZombieHeight.InToPool || mZombieHeight == ZombieHeight.OutOfPool)
            {
                return;
            }

            if (mBoard.IsPoolSquare(mBoard.PixelToGridX(mX + 75, mY), mRow)
                && mBoard.IsPoolSquare(mBoard.PixelToGridX(mX + 45, mY), mRow)
                && mPosX < 800f)
            {
                if (!mInPool)
                {
                    if (mBoard.mIceTrapCounter > 0)
                    {
                        mIceTrapCounter = mBoard.mIceTrapCounter;
                        ApplyChill(true);
                        return;
                    }
                    mZombieHeight = ZombieHeight.InToPool;
                    mInPool = true;
                    PoolSplash(true);
                }
                mUsesClipping = true;
            }
            else if (mInPool)
            {
                mZombieHeight = ZombieHeight.OutOfPool;
                StartWalkAnim(0);
                PoolSplash(false);
            }
        }

        public void GetDrawPos(ref ZombieDrawPosition theDrawPos)
        {
            theDrawPos.mImageOffsetX = mPosX - mX;
            theDrawPos.mImageOffsetY = mPosY - mY;
            if (mIsEating)
            {
                theDrawPos.mHeadX = 47;
                theDrawPos.mHeadY = 4;
            }
            else
            {
                switch (mFrame)
                {
                case 0:
                    theDrawPos.mHeadX = 50;
                    theDrawPos.mHeadY = 2;
                    break;
                case 1:
                    theDrawPos.mHeadX = 49;
                    theDrawPos.mHeadY = 1;
                    break;
                case 2:
                    theDrawPos.mHeadX = 49;
                    theDrawPos.mHeadY = 2;
                    break;
                case 3:
                    theDrawPos.mHeadX = 48;
                    theDrawPos.mHeadY = 4;
                    break;
                case 4:
                    theDrawPos.mHeadX = 48;
                    theDrawPos.mHeadY = 5;
                    break;
                case 5:
                    theDrawPos.mHeadX = 48;
                    theDrawPos.mHeadY = 4;
                    break;
                case 6:
                    theDrawPos.mHeadX = 48;
                    theDrawPos.mHeadY = 2;
                    break;
                case 7:
                    theDrawPos.mHeadX = 49;
                    theDrawPos.mHeadY = 1;
                    break;
                case 8:
                    theDrawPos.mHeadX = 49;
                    theDrawPos.mHeadY = 2;
                    break;
                case 9:
                    theDrawPos.mHeadX = 50;
                    theDrawPos.mHeadY = 4;
                    break;
                case 10:
                    theDrawPos.mHeadX = 50;
                    theDrawPos.mHeadY = 5;
                    break;
                default:
                    theDrawPos.mHeadX = 50;
                    theDrawPos.mHeadY = 4;
                    break;
                }
            }
            theDrawPos.mArmY = theDrawPos.mHeadY / 2;
            switch (mZombieType)
            {
            case ZombieType.Football:
                theDrawPos.mImageOffsetY += -16f;
                break;
            case ZombieType.Yeti:
                theDrawPos.mImageOffsetY += -20f;
                break;
            case ZombieType.Catapult:
                theDrawPos.mImageOffsetX += -25f;
                theDrawPos.mImageOffsetY += -18f;
                break;
            case ZombieType.Pogo:
                theDrawPos.mImageOffsetY += 16f;
                break;
            case ZombieType.Balloon:
                theDrawPos.mImageOffsetY += 17f;
                break;
            case ZombieType.Polevaulter:
                theDrawPos.mImageOffsetX += -6f;
                theDrawPos.mImageOffsetY += -11f;
                break;
            case ZombieType.Zamboni:
                theDrawPos.mImageOffsetX += 68f;
                theDrawPos.mImageOffsetY += -23f;
                break;
            case ZombieType.Gargantuar:
            case ZombieType.RedeyeGargantuar:
                theDrawPos.mImageOffsetY += -8f;
                break;
            case ZombieType.Bobsled:
                theDrawPos.mImageOffsetY += -12f;
                break;
            case ZombieType.Dancer:
            case ZombieType.BackupDancer:
                theDrawPos.mImageOffsetY += 15f;
                break;
            }

            if (mZombiePhase == ZombiePhase.RisingFromGrave)
            {
                theDrawPos.mBodyY = -mAltitude;
                if (mInPool)
                {
                    theDrawPos.mClipHeight = theDrawPos.mBodyY;
                }
                else
                {
                    float num = Math.Min(mPhaseCounter, 40f);
                    theDrawPos.mClipHeight = theDrawPos.mBodyY + num;
                }
                if (IsOnHighGround())
                {
                    theDrawPos.mBodyY -= Constants.HIGH_GROUND_HEIGHT;
                    return;
                }
            }
            else
            {
                switch (mZombieType)
                {
                case ZombieType.DolphinRider:
                {
                    theDrawPos.mBodyY = -mAltitude;
                    theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
                    switch (mZombiePhase)
                    {
                    case ZombiePhase.DolphinIntoPool:
                    {
                        Reanimation reanimation = mBodyReanimID;
                        if (reanimation.mAnimTime >= 0.56f && reanimation.mAnimTime <= 0.65f)
                        {
                            theDrawPos.mClipHeight = 0f;
                            return;
                        }
                        if (reanimation.mAnimTime >= 0.75f)
                        {
                            theDrawPos.mClipHeight = -mAltitude - 10f;
                            return;
                        }
                        break;
                    }

                    case ZombiePhase.DolphinRiding:
                        theDrawPos.mImageOffsetX += 70f;
                        if (mZombieHeight == ZombieHeight.DraggedUnder)
                        {
                            theDrawPos.mClipHeight = -mAltitude - 15f;
                            return;
                        }
                        theDrawPos.mClipHeight = -mAltitude - 10f;
                        return;
                    case ZombiePhase.DolphinInJump:
                    {
                        theDrawPos.mImageOffsetX += 70f + mAltitude;
                        Reanimation reanimation2 = mBodyReanimID;
                        if (reanimation2.mAnimTime <= 0.06f)
                        {
                            theDrawPos.mClipHeight = -mAltitude - 10f;
                            return;
                        }
                        if (reanimation2.mAnimTime >= 0.5f && reanimation2.mAnimTime <= 0.76f)
                        {
                            theDrawPos.mClipHeight = -13f;
                            return;
                        }

                        break;
                    }

                    case ZombiePhase.DolphinWalkingInPool:
                    case ZombiePhase.ZombieDying:
                        theDrawPos.mImageOffsetY += 50f;
                        if (mZombiePhase == ZombiePhase.ZombieDying)
                        {
                            theDrawPos.mClipHeight = -mAltitude + 44f;
                            return;
                        }
                        if (mZombieHeight == ZombieHeight.DraggedUnder)
                        {
                            theDrawPos.mClipHeight = -mAltitude + 36f;
                            return;
                        }
                        break;
                    case ZombiePhase.DolphinWalking when mZombieHeight == ZombieHeight.OutOfPool:
                        theDrawPos.mClipHeight = -mAltitude;
                        return;
                    case ZombiePhase.DolphinWalkingWithoutDolphin when mZombieHeight == ZombieHeight.OutOfPool:
                        theDrawPos.mClipHeight = -mAltitude;
                        return;
                    }
                    break;
                }

                case ZombieType.Snorkel:
                {
                    theDrawPos.mBodyY = -mAltitude;
                    theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
                    if (mZombiePhase == ZombiePhase.SnorkelIntoPool)
                    {
                        Reanimation reanimation3 = mBodyReanimID;
                        if (reanimation3.mAnimTime >= 0.8f)
                        {
                            theDrawPos.mClipHeight = -10f;
                            return;
                        }
                    }
                    else if (mInPool)
                    {
                        theDrawPos.mClipHeight = -mAltitude - 5f;
                        theDrawPos.mClipHeight += 20f - 20f * mScaleZombie;
                        return;
                    }
                    break;
                }

                default:
                    if (mInPool)
                    {
                        theDrawPos.mBodyY = -mAltitude;
                        theDrawPos.mClipHeight = -mAltitude - 7f;
                        theDrawPos.mClipHeight += 10f - 10f * mScaleZombie;
                        if (mIsEating)
                        {
                            theDrawPos.mClipHeight += 7f;
                            return;
                        }
                    }
                    else
                    {
                        switch (mZombiePhase)
                        {
                        case ZombiePhase.DancerRising:
                            theDrawPos.mBodyY = -mAltitude;
                            theDrawPos.mClipHeight = -mAltitude;
                            if (IsOnHighGround())
                            {
                                theDrawPos.mBodyY -= Constants.HIGH_GROUND_HEIGHT;
                                return;
                            }
                            break;
                        case ZombiePhase.DiggerRising:
                        case ZombiePhase.DiggerRiseWithoutAxe:
                            theDrawPos.mBodyY = -mAltitude;
                            if (mPhaseCounter > 20)
                            {
                                theDrawPos.mClipHeight = -mAltitude;
                                return;
                            }
                            theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
                            return;
                        default:
                            if (mZombieType == ZombieType.Bungee)
                            {
                                theDrawPos.mBodyY = -mAltitude;
                                theDrawPos.mImageOffsetX += -18f;
                                if (IsOnHighGround())
                                {
                                    theDrawPos.mBodyY -= Constants.HIGH_GROUND_HEIGHT;
                                }
                                theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
                                return;
                            }
                            theDrawPos.mBodyY = -mAltitude;
                            theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        public void UpdateZombieHighGround()//3update
        {
            if (mZombieType == ZombieType.Pogo)
            {
                return;
            }
            if (mZombieHeight == ZombieHeight.UpToHighGround)
            {
                //mAltitude += 3f;
                mAltitude++;
                if (mAltitude >= Constants.HIGH_GROUND_HEIGHT)
                {
                    mAltitude = Constants.HIGH_GROUND_HEIGHT;
                    mZombieHeight = ZombieHeight.ZombieNormal;
                    return;
                }
            }
            else if (mZombieHeight == ZombieHeight.DownOffHighGround)
            {
                //mAltitude -= 3f;
                mAltitude--;
                if (mAltitude <= 0f)
                {
                    mAltitude = 0f;
                    mZombieHeight = ZombieHeight.ZombieNormal;
                    mOnHighGround = false;
                }
            }
        }

        public void CheckForHighGround()//3update
        {
            if (mZombieHeight != ZombieHeight.ZombieNormal)
            {
                return;
            }
            if (mZombieType == ZombieType.Bungee)
            {
                return;
            }
            bool flag = IsOnHighGround();
            if (!mOnHighGround && flag)
            {
                mZombieHeight = ZombieHeight.UpToHighGround;
                mOnHighGround = true;
                return;
            }
            if (mOnHighGround && !flag)
            {
                mZombieHeight = ZombieHeight.DownOffHighGround;
            }
        }

        public bool IsOnHighGround()
        {
            if (!IsOnBoard())
            {
                return false;
            }
            int num = mBoard.PixelToGridXKeepOnBoard(mX + 75, mY);
            return mBoard.mGridSquareType[num, mRow] == GridSquareType.HighGround;
        }

        public void DropLoot()
        {
            if (!IsOnBoard())
            {
                return;
            }
            AlmanacDialog.AlmanacPlayerDefeatedZombie(mZombieType);
            if (mZombieType == ZombieType.Yeti)
            {
                mBoard.mKilledYeti = true;
            }
            TrySpawnLevelAward();
            if (mDroppedLoot)
            {
                return;
            }
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            if (!mBoard.CanDropLoot())
            {
                return;
            }
            mDroppedLoot = true;
            ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(mZombieType);
            int zombieValue = zombieDefinition.mZombieValue;
            if (mApp.IsLittleTroubleLevel() && RandomNumbers.NextNumber(4) != 0)
            {
                return;
            }
            if (mApp.IsIZombieLevel())
            {
                return;
            }
            TRect zombieRect = GetZombieRect();
            int num = zombieRect.mX + zombieRect.mWidth / 2;
            int num2 = zombieRect.mY + zombieRect.mHeight / 4;
            if (mZombieType == ZombieType.Yeti)
            {
                mApp.PlayFoley(FoleyType.SpawnSun);
                mBoard.AddCoin(num - 20, num2, CoinType.Diamond, CoinMotion.Coin);
                mBoard.AddCoin(num - 30, num2, CoinType.Diamond, CoinMotion.Coin);
                mBoard.AddCoin(num - 40, num2, CoinType.Diamond, CoinMotion.Coin);
                mBoard.AddCoin(num - 50, num2, CoinType.Diamond, CoinMotion.Coin);
                return;
            }
            mBoard.DropLootPiece(num, num2, zombieValue);
        }

        public bool TrySpawnLevelAward()
        {
            if (!IsOnBoard())
            {
                return false;
            }
            if (mBoard.HasLevelAwardDropped())
            {
                return false;
            }
            if (mBoard.mLevelComplete)
            {
                return false;
            }
            if (mDroppedLoot)
            {
                return false;
            }
            if (mApp.IsFinalBossLevel())
            {
                if (mZombieType != ZombieType.Boss)
                {
                    return false;
                }
            }
            else if (mApp.IsScaryPotterLevel())
            {
                if (!mBoard.mChallenge.ScaryPotterIsCompleted())
                {
                    return false;
                }
            }
            else
            {
                if (mApp.IsContinuousChallenge())
                {
                    return false;
                }
                if (mBoard.mCurrentWave < mBoard.mNumWaves)
                {
                    return false;
                }
                if (mBoard.AreEnemyZombiesOnScreen())
                {
                    return false;
                }
            }
            if (mApp.IsWhackAZombieLevel() && mBoard.mZombieCountDown > 0)
            {
                return false;
            }
            mBoard.mLevelAwardSpawned = true;
            mApp.mBoardResult = BoardResult.Won;
            TRect aZombieRect = GetZombieRect();
            int aCenterX = aZombieRect.mX + aZombieRect.mWidth / 2;
            int aCenterY = aZombieRect.mY + aZombieRect.mHeight / 2;
            if (!mBoard.IsSurvivalStageWithRepick())
            {
                mBoard.RemoveAllZombies();
            }
            CoinType coinType;
            if (mApp.IsScaryPotterLevel() && !mBoard.IsFinalScaryPotterStage())
            {
                coinType = CoinType.None;
                int theGridX = mBoard.PixelToGridXKeepOnBoard((int)mPosX + 75, (int)mPosY);
                mBoard.mChallenge.PuzzlePhaseComplete(theGridX, mRow);
            }
            else if (mApp.IsAdventureMode() && mBoard.mLevel <= 50)
            {
                if (mBoard.mLevel == 9 || mBoard.mLevel == 19 || mBoard.mLevel == 29 || mBoard.mLevel == 39 || mBoard.mLevel == 49)
                {
                    coinType = CoinType.Note;
                }
                else if (mBoard.mLevel == 50)
                {
                    if (mApp.HasFinishedAdventure())
                    {
                        coinType = CoinType.AwardMoneyBag;
                    }
                    else
                    {
                        coinType = CoinType.AwardMoneyBag;
                    }
                }
                else if (mApp.HasFinishedAdventure())
                {
                    coinType = CoinType.AwardMoneyBag;
                }
                else if (mBoard.mLevel == 4)
                {
                    coinType = CoinType.Shovel;
                }
                else if (mBoard.mLevel == 14)
                {
                    coinType = CoinType.Almanac;
                }
                else if (mBoard.mLevel == 24)
                {
                    coinType = CoinType.Carkeys;
                }
                else if (mBoard.mLevel == 34)
                {
                    coinType = CoinType.Taco;
                }
                else if (mBoard.mLevel == 44)
                {
                    coinType = CoinType.WateringCan;
                }
                else
                {
                    coinType = CoinType.FinalSeedPacket;
                }
            }
            else if (mBoard.IsSurvivalStageWithRepick())
            {
                coinType = CoinType.None;
                mBoard.FadeOutLevel();
            }
            else if (mApp.IsQuickPlayMode())
            {
                coinType = CoinType.AwardMoneyBag;
            }
            else if (mBoard.IsLastStandStageWithRepick())
            {
                coinType = CoinType.None;
                mBoard.FadeOutLevel();
                mApp.PlayFoley(FoleyType.SpawnSun);
                for (int i = 0; i < 10; i++)
                {
                    mBoard.AddCoin(aCenterX + i * 5, aCenterY, CoinType.Sun, CoinMotion.Coin);
                }
            }
            else if (!mApp.IsAdventureMode())
            {
                if (mApp.HasBeatenChallenge(mApp.mGameMode))
                {
                    coinType = CoinType.AwardMoneyBag;
                }
                else
                {
                    coinType = CoinType.Trophy;
                }
            }
            else
            {
                coinType = CoinType.AwardMoneyBag;
            }
            CoinMotion theCoinMotion = CoinMotion.Coin;
            if (mZombieType == ZombieType.Boss)
            {
                theCoinMotion = CoinMotion.FromBoss;
            }
            if (coinType != CoinType.None)
            {
                mApp.PlayFoley(FoleyType.SpawnSun);
                mBoard.AddCoin(aCenterX, aCenterY, coinType, theCoinMotion);
            }
            mDroppedLoot = true;
            return true;
        }

        public void StartZombieSound()
        {
            if (mPlayingSong)
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.JackInTheBoxRunning && mHasHead)
            {
                mApp.PlayFoley(FoleyType.Jackinthebox);
                mPlayingSong = true;
                return;
            }
            if (mZombiePhase == ZombiePhase.DiggerTunneling)
            {
                mApp.PlayFoley(FoleyType.Digger);
                mPlayingSong = true;
            }
        }

        public void StopZombieSound()
        {
            if (mZombieType == ZombieType.Dancer || mZombieType == ZombieType.BackupDancer)
            {
                if (mApp.mBoard != null)
                {
                    bool flag = false;
                    int count = mApp.mBoard.mZombies.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Zombie zombie = mApp.mBoard.mZombies[i];
                        if (!zombie.mDead && zombie.mHasHead && !zombie.IsDeadOrDying() && zombie.IsOnBoard() && (zombie.mZombieType == ZombieType.Dancer || zombie.mZombieType == ZombieType.BackupDancer))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        mApp.mSoundSystem.StopFoley(FoleyType.Dancer);
                    }
                }
                else
                {
                    mApp.mSoundSystem.StopFoley(FoleyType.Dancer);
                }
            }
            if (!mPlayingSong)
            {
                return;
            }
            mPlayingSong = false;
            if (mZombieType == ZombieType.JackInTheBox)
            {
                mApp.mSoundSystem.StopFoley(FoleyType.Jackinthebox);
                return;
            }
            if (mZombieType == ZombieType.Digger)
            {
                mApp.mSoundSystem.StopFoley(FoleyType.Digger);
            }
        }

        public void UpdateZombieJackInTheBox()//3update
        {
            if (mZombiePhase == ZombiePhase.JackInTheBoxRunning)
            {
                if (mPhaseCounter <= 0 && mHasHead)
                {
                    mPhaseCounter = 110;
                    mZombiePhase = ZombiePhase.JackInTheBoxPopping;
                    StopZombieSound();
                    mApp.PlaySample(Resources.SOUND_BOING);
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_pop, ReanimLoopType.PlayOnceAndHold, 20, 28f);
                    return;
                }
            }
            else if (mZombiePhase == ZombiePhase.JackInTheBoxPopping)
            {
                if (mPhaseCounter == 80)
                {
                    mApp.PlayFoley(FoleyType.JackSurprise);
                }
                if (mPhaseCounter <= 0)
                {
                    mApp.PlayFoley(FoleyType.Explosion);
                    int aPosX = mX + mWidth / 2;
                    int aPosY = mY + mHeight / 2;
                    if (mMindControlled)
                    {
                        mBoard.KillAllZombiesInRadius(mRow, aPosX, aPosY, Constants.JackInTheBoxZombieRadius, 1, true, 0x7F);
                    }
                    else
                    {
                        mBoard.KillAllZombiesInRadius(mRow, aPosX, aPosY, Constants.JackInTheBoxZombieRadius, 1, true, 0xFF);
                        mBoard.KillAllPlantsInRadius(aPosX, aPosY, Constants.JackInTheBoxPlantRadius);
                    }
                    mApp.AddTodParticle(aPosX, aPosY, 400000, ParticleEffect.Jackexplode);
                    mBoard.ShakeBoard(4, -6);
                    DieNoLoot(false);
                    if (mApp.IsScaryPotterLevel())
                    {
                        mBoard.mChallenge.ScaryPotterJackExplode(aPosX, aPosY);
                    }
                }
            }
        }

        public void DrawZombieHead(Graphics g, ref ZombieDrawPosition theDrawPos, int theFrame)
        {
        }

        public void UpdateZombiePosition()//3update
        {
            if (mZombieType == ZombieType.Bungee || mZombieType == ZombieType.Boss || mZombiePhase == ZombiePhase.RisingFromGrave || mZombieHeight == ZombieHeight.Zombiquarium)
            {
                return;
            }
            UpdateZombieWalking();
            CheckForZombieStep();
            if (mBlowingAway)
            {
                mPosX += 30f;
                if (mX > 850)
                {
                    DieWithLoot();
                    return;
                }
            }
            if (mZombieHeight == ZombieHeight.ZombieNormal)
            {
                float aDesiredY = GetPosYBasedOnRow(mRow);
                if (mPosY < aDesiredY)
                {
                    mPosY += /*3f * */Math.Min(1f, aDesiredY - mPosY);
                    if (mPosY > aDesiredY)
                    {
                        mPosY = aDesiredY;
                        return;
                    }
                }
                else if (mPosY > aDesiredY)
                {
                    mPosY -= /*3f * */Math.Min(1f, mPosY - aDesiredY);
                    if (mPosY < aDesiredY)
                    {
                        mPosY = aDesiredY;
                    }
                }
            }
        }

        public TRect GetZombieRect()
        {
            if (cachedZombieRectUpToDate)
            {
                return cachedZombieRect;
            }
            cachedZombieRect = mZombieRect;
            if (IsWalkingBackwards())
            {
                cachedZombieRect.mX = mWidth - cachedZombieRect.mX - cachedZombieRect.mWidth;
            }
            ZombieDrawPosition aDrawPos = default(ZombieDrawPosition);
            GetDrawPos(ref aDrawPos);
            cachedZombieRect.Offset(mX, mY + (int)aDrawPos.mBodyY);
            if (aDrawPos.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
            {
                cachedZombieRect.mHeight = cachedZombieRect.mHeight - (int)aDrawPos.mClipHeight;
            }
            cachedZombieRectUpToDate = true;
            return cachedZombieRect;
        }

        public TRect GetZombieAttackRect()
        {
            TRect result = mZombieAttackRect;
            if (mZombiePhase == ZombiePhase.PolevaulterInVault || mZombiePhase == ZombiePhase.DolphinInJump)
            {
                result = new TRect(-40, 0, 100, 115);
            }
            if (IsWalkingBackwards())
            {
                result.mX = mWidth - result.mX - result.mWidth;
            }
            ZombieDrawPosition aDrawPos = default(ZombieDrawPosition);
            GetDrawPos(ref aDrawPos);
            result.Offset(mX, mY + (int)aDrawPos.mBodyY);
            if (aDrawPos.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
            {
                result.mHeight -= (int)aDrawPos.mClipHeight;
            }
            return result;
        }

        public void UpdateZombieWalking()//3update
        {
            if (ZombieNotWalking())
            {
                return;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation != null)
            {
                float aSpeed;
                if (IsBouncingPogo() || mZombiePhase == ZombiePhase.BalloonFlying || mZombiePhase == ZombiePhase.DolphinRiding || mZombiePhase == ZombiePhase.SnorkelWalkingInPool || mZombieType == ZombieType.Catapult)
                {
                    //aSpeed = mVelX * 3f;
                    aSpeed = mVelX;
                    if (IsMovingAtChilledSpeed())
                    {
                        aSpeed *= GameConstants.CHILLED_SPEED_FACTOR;
                    }
                }
                else if (mZombieType == ZombieType.Zamboni || mZombiePhase == ZombiePhase.DiggerTunneling || mZombiePhase == ZombiePhase.DolphinInJump || IsBobsledTeamWithSled() || mZombiePhase == ZombiePhase.PolevaulterInVault || mZombiePhase == ZombiePhase.SnorkelIntoPool)
                {
                    //aSpeed = mVelX * 3f;
                    aSpeed = mVelX;
                }
                else if (mHasGroundTrack)
                {
                    mGroundTrackIndex = reanimation.GetTrackIndex(Reanimation.ReanimTrackId__ground);
                    aSpeed = reanimation.GetTrackVelocity(mGroundTrackIndex) * mScaleZombie;
                }
                else
                {
                    //aSpeed = mVelX * 3f;
                    aSpeed = mVelX;
                    if (IsMovingAtChilledSpeed())
                    {
                        aSpeed *= GameConstants.CHILLED_SPEED_FACTOR;
                    }
                }
                if (mApp.mGameScene == GameScenes.ZombiesWon)
                {
                    ZombieType zombieType = mZombieType;
                    if (aSpeed > 0.3f)
                    {
                        aSpeed = 0.3f;
                    }
                }
                if (IsWalkingBackwards() || mZombiePhase == ZombiePhase.DancerDancingIn)
                {
                    mPosX += aSpeed;
                }
                else
                {
                    mPosX -= aSpeed;
                }
                if (mZombieType == ZombieType.Football && mFromWave != GameConstants.ZOMBIE_WAVE_WINNER)
                {
                    if (reanimation.ShouldTriggerTimedEvent(0.03f))
                    {
                        mApp.AddTodParticle(mX + 81, mY + 106, mRenderOrder - 1, ParticleEffect.DustFoot);
                    }
                    if (reanimation.ShouldTriggerTimedEvent(0.61f))
                    {
                        mApp.AddTodParticle(mX + 87, mY + 110, mRenderOrder - 1, ParticleEffect.DustFoot);
                    }
                }
                if (mZombiePhase == ZombiePhase.PolevaulterPreVault)
                {
                    if (reanimation.ShouldTriggerTimedEvent(0.16f))
                    {
                        mApp.AddTodParticle(mX + 81, mY + 106, mRenderOrder - 1, ParticleEffect.DustFoot);
                    }
                    if (reanimation.ShouldTriggerTimedEvent(0.67f))
                    {
                        mApp.AddTodParticle(mX + 87, mY + 110, mRenderOrder - 1, ParticleEffect.DustFoot);
                    }
                }
                return;
            }
            bool flag = false;
            if (mZombiePhase == ZombiePhase.PolevaulterInVault || mZombiePhase == ZombiePhase.DiggerTunneling || mZombieType == ZombieType.Dancer || mZombieType == ZombieType.BackupDancer || mZombieType == ZombieType.Bobsled || mZombieType == ZombieType.Pogo || mZombieType == ZombieType.DolphinRider || mZombieType == ZombieType.Balloon)
            {
                flag = true;
            }
            else if (mZombieType == ZombieType.Snorkel && mInPool)
            {
                flag = true;
            }
            else if (mFrame >= 0 && mFrame <= 2)
            {
                flag = true;
            }
            else if (mFrame >= 6 && mFrame <= 8)
            {
                flag = true;
            }
            if (flag)
            {
                //float num2 = mVelX * 3f;
                float num2 = mVelX;
                if (IsMovingAtChilledSpeed())
                {
                    num2 *= GameConstants.CHILLED_SPEED_FACTOR;
                }
                if (IsWalkingBackwards())
                {
                    mPosX += num2;
                    return;
                }
                mPosX -= num2;
            }
        }

        public void UpdateZombieWalkingIntoHouse()//3update
        {
            if (mZombieType == ZombieType.Bungee || mZombieType == ZombieType.Boss || mZombiePhase == ZombiePhase.RisingFromGrave || mZombieHeight == ZombieHeight.Zombiquarium)
            {
                return;
            }
            int num = 1;
            if (mZombieType == ZombieType.Normal || mZombieType == ZombieType.Pail || mZombieType == ZombieType.TrafficCone)
            {
                num = 2;
            }
            else if (mZombieType == ZombieType.Zamboni || mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
            {
                num = 4;
            }
            //num *= 3;
            while (num-- != 0)
            {
                UpdateZombieWalking();
                if (mZombieHeight == ZombieHeight.ZombieNormal)
                {
                    float num2 = GameConstants.ZOMBIE_WALK_IN_FRONT_DOOR_Y;
                    float num3 = 1f;
                    if (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
                    {
                        num2 += 30f;
                    }
                    else if (mZombiePhase == ZombiePhase.PolevaulterPreVault)
                    {
                        num2 += 35f;
                    }
                    else if (mZombieType == ZombieType.Zamboni)
                    {
                        num2 += 15f;
                    }
                    else if (mZombieType == ZombieType.Football)
                    {
                        num2 += 15f;
                        if (mRow == 0 || mRow == 1)
                        {
                            num3 = 2f;
                        }
                    }
                    if (!Zombie.WinningZombieReachedDesiredY && (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar))
                    {
                        Zombie.WinningZombieReachedDesiredY = true;
                        ReanimReenableClipping();
                    }
                    if (mPosY < num2)
                    {
                        mPosY += Math.Min(num3, num2 - mPosY);
                    }
                    else if (mPosY > num2)
                    {
                        mPosY -= Math.Min(num3, mPosY - num2);
                    }
                    else if (!Zombie.WinningZombieReachedDesiredY)
                    {
                        Zombie.WinningZombieReachedDesiredY = true;
                        ReanimReenableClipping();
                    }
                }
            }
        }

        public void UpdateZombieBobsled()//3update
        {
            if (mZombiePhase == ZombiePhase.BobsledCrashing)
            {
                if (mPhaseCounter <= 0)
                {
                    mZombiePhase = ZombiePhase.ZombieNormal;
                    if (GetBobsledPosition() == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Zombie zombie = mBoard.ZombieGet(mFollowerZombieID[i]);
                            zombie.mRelatedZombieID = null;
                            mFollowerZombieID[i] = null;
                            zombie.PickRandomSpeed();
                        }
                        PickRandomSpeed();
                    }
                }
                return;
            }
            if (mZombiePhase == ZombiePhase.BobsledSliding)
            {
                //if (mPhaseCounter >= 0 && mPhaseCounter < 3)
                if (mPhaseCounter == 0)
                {
                    mZombiePhase = ZombiePhase.BobsledBoarding;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jump, ReanimLoopType.PlayOnceAndHold, 0, 20f);
                }
            }
            else
            {
                if (mZombiePhase != ZombiePhase.BobsledBoarding)
                {
                    return;
                }
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                int theTimeAge = (int)(reanimation.mAnimTime * 50f);
                int bobsledPosition = GetBobsledPosition();
                if (bobsledPosition == 1 || bobsledPosition == 3)
                {
                    mAltitude = TodCommon.TodAnimateCurveFloat(0, 50, theTimeAge, 8f, 18f, TodCurves.Linear);
                }
                else
                {
                    mAltitude = TodCommon.TodAnimateCurveFloat(0, 50, theTimeAge, -9f, 18f, TodCurves.Linear);
                }
            }
            mBoard.mIceTimer[mRow] = Math.Max(500, mBoard.mIceTimer[mRow]);
            if (mPosX + 10f < mBoard.mIceMinX[mRow] && GetBobsledPosition() == 0)
            {
                TakeDamage(6, 8U, false);
            }
        }

        public void BobsledCrash()
        {
            mZombiePhase = ZombiePhase.BobsledCrashing;
            mPhaseCounter = GameConstants.BOBSLED_CRASH_TIME;
            mAltitude = 0f;
            mZombieRect = new TRect(36, 0, 42, 115);
            StartWalkAnim(0);
            Reanimation aFirstZombieReanim = mApp.ReanimationGet(mBodyReanimID);
            for (int i = 0; i < 3; i++)
            {
                Zombie aFolowerZombie = mBoard.ZombieGet(mFollowerZombieID[i]);
                aFolowerZombie.mZombiePhase = ZombiePhase.BobsledCrashing;
                aFolowerZombie.mPhaseCounter = GameConstants.BOBSLED_CRASH_TIME;
                aFolowerZombie.mPosY = GetPosYBasedOnRow(mRow);
                aFolowerZombie.mAltitude = 0f;
                aFolowerZombie.StartWalkAnim(0);
                Reanimation aFollowerZombieReanim = mApp.ReanimationGet(aFolowerZombie.mBodyReanimID);
                if (aFollowerZombieReanim != null)
                {
                    aFolowerZombie.mVelX = mVelX;
                    aFollowerZombieReanim.mAnimTime = TodCommon.RandRangeFloat(0f, 1f);
                    aFollowerZombieReanim.mAnimRate = aFirstZombieReanim.mAnimRate;
                }
            }
        }

        public Plant IsStandingOnSpikeweed()
        {
            if (mZombieType == ZombieType.Zamboni || mZombieType == ZombieType.Catapult)
            {
                return null;
            }
            TRect zombieRect = GetZombieRect();
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && mRow == plant.mRow && plant.IsSpiky() && !plant.NotOnGround() && (!mOnHighGround || plant.IsOnHighGround()))
                {
                    TRect plantAttackRect = plant.GetPlantAttackRect(PlantWeapon.Primary);
                    int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
                    if (rectOverlap > 0)
                    {
                        return plant;
                    }
                }
            }
            return null;
        }

        public void CheckForZombieStep()//3update
        {
            if (mZombieType == ZombieType.Zamboni || mZombieType == ZombieType.Catapult)
            {
                if (mFlatTires)
                {
                    return;
                }
                CheckSquish(ZombieAttackType.DriveOver);
            }
        }

        public void OverrideParticleColor(TodParticleSystem aParticle)
        {
            if (aParticle == null)
            {
                return;
            }
            if (mMindControlled)
            {
                aParticle.OverrideColor(null, GameConstants.ZOMBIE_MINDCONTROLLED_COLOR);
                aParticle.OverrideExtraAdditiveDraw(null, true);
            }
            else if (mChilledCounter > 0 || mIceTrapCounter > 0)
            {
                aParticle.OverrideColor(null, new SexyColor(75, 75, 255, 255));
                aParticle.OverrideExtraAdditiveDraw(null, true);
            }
        }

        public void OverrideParticleScale(TodParticleSystem aParticle)
        {
            aParticle?.OverrideScale(null, mScaleZombie);
        }

        public void PoolSplash(bool theInToPoolSound)
        {
            float aOffsetX = 23f;
            float aOffsetY = 78f;
            if (mZombiePhase == ZombiePhase.SnorkelWalkingInPool)
            {
                aOffsetX += -37f;
                aOffsetY += -8f;
            }
            int aOffsetY_v;
            if (mAltitude == 0f)
            {
                aOffsetY_v = (int)(aOffsetY * mScaleZombie);
            }
            else
            {
                aOffsetY_v = (int)aOffsetY;
            }
            mApp.AddReanimation(mX + (int)(aOffsetX * mScaleZombie), mY + aOffsetY_v, mRenderOrder + 1, ReanimationType.Splash)
                .OverrideScale(0.8f, 0.8f);
            mApp.AddTodParticle(mX + aOffsetX + 37f, mY + aOffsetY + 42f, mRenderOrder + 1, ParticleEffect.PlantingPool);
            if (theInToPoolSound)
            {
                mApp.PlayFoley(FoleyType.Zombiesplash);
            }
            else 
            { 
                mApp.PlayFoley(FoleyType.PlantWater); 
            }
            
        }

        public void UpdateZombieFlyer()//3update
        {
            if (mApp.mGameMode == GameMode.ChallengeHighGravity && mPosX < 720f + Constants.BOARD_EXTRA_ROOM)
            {
                mAltitude -= 0.1f;
                if (mAltitude < -35f)
                {
                    LandFlyer(0U);
                }
            }
            if (mZombiePhase == ZombiePhase.BalloonPopping)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.mLoopCount > 0)
                {
                    mZombiePhase = ZombiePhase.BalloonWalking;
                    StartWalkAnim(0);
                }
            }
            if (mApp.IsIZombieLevel() && mZombiePhase == ZombiePhase.BalloonFlying && mBoard.mChallenge.IZombieGetBrainTarget(this) != null)
            {
                LandFlyer(0U);
            }
        }

        public void UpdateZombiePogo()//3update
        {
            if (IsDeadOrDying())
            {
                return;
            }
            if (IsImmobilizied())
            {
                return;
            }
            if (!IsBouncingPogo())
            {
                return;
            }
            if (mZombieHeight == ZombieHeight.InToChimney)
            {
                return;
            }
            float aHeight = 40f;
            if (mZombiePhase >= ZombiePhase.PogoHighBounce1 && mZombiePhase <= ZombiePhase.PogoHighBounce6)
            {
                aHeight = 50f + 20f * (mZombiePhase - ZombiePhase.PogoHighBounce1);
            }
            else if (mZombiePhase == ZombiePhase.PogoForwardBounce2)
            {
                aHeight = 90f;
            }
            else if (mZombiePhase == ZombiePhase.PogoForwardBounce7)
            {
                aHeight = 170f;
            }
            float aDeflection = 9f;
            mAltitude = TodCommon.TodAnimateCurveFloat(GameConstants.POGO_BOUNCE_TIME, 0, mPhaseCounter, aDeflection, aHeight + aDeflection, TodCurves.BounceSlowMiddle);
            mFrame = TodCommon.ClampInt(3 - (int)mAltitude / 3, 0, 3);
            //if (mPhaseCounter >= 8 && mPhaseCounter < 11)
            if (mPhaseCounter == 8)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                reanimation.mAnimTime = 0f;
                reanimation.mLoopType = ReanimLoopType.PlayOnceAndHold;
            }
            //if (IsOnBoard() && mPhaseCounter >= 5 && mPhaseCounter < 8)
            if (IsOnBoard() && mPhaseCounter == 5)
            {
                mApp.PlayFoley(FoleyType.PogoZombie);
            }
            if (mZombieHeight == ZombieHeight.UpToHighGround)
            {
                mAltitude += Constants.HIGH_GROUND_HEIGHT;
                mZombieHeight = ZombieHeight.ZombieNormal;
            }
            else if (mZombieHeight == ZombieHeight.DownOffHighGround)
            {
                mOnHighGround = false;
                mZombieHeight = ZombieHeight.ZombieNormal;
            }
            else if (mOnHighGround)
            {
                mAltitude += Constants.HIGH_GROUND_HEIGHT;
            }
            Plant plant;
            if (mZombiePhase == ZombiePhase.PogoForwardBounce2 && mPhaseCounter >= 71 && mPhaseCounter < 74)
            {
                plant = FindPlantTarget(ZombieAttackType.Vault);
                if (plant != null && plant.mSeedType == SeedType.Tallnut)
                {
                    mApp.PlayFoley(FoleyType.Bonk);
                    mApp.AddTodParticle(plant.mX + 60, plant.mY - 20, mRenderOrder + 1, ParticleEffect.TallNutBlock);
                    mShieldType = ShieldType.None;
                    PogoBreak(0U);
                    return;
                }
            }
            if (mPhaseCounter > 0)
            {
                return;
            }
            plant = null;
            if (IsOnBoard())
            {
                plant = FindPlantTarget(ZombieAttackType.Vault);
            }
            if (plant == null)
            {
                mZombiePhase = ZombiePhase.PogoBouncing;
                PickRandomSpeed();
                mPhaseCounter = GameConstants.POGO_BOUNCE_TIME;
                return;
            }
            if (mZombiePhase == ZombiePhase.PogoHighBounce1)
            {
                mZombiePhase = ZombiePhase.PogoForwardBounce2;
                int num3 = mX - plant.mX + 60;
                mVelX = num3 / (float)GameConstants.POGO_BOUNCE_TIME;
                mPhaseCounter = GameConstants.POGO_BOUNCE_TIME;
                return;
            }
            mZombiePhase = ZombiePhase.PogoHighBounce1;
            mVelX = 0f;
            mPhaseCounter = GameConstants.POGO_BOUNCE_TIME;
        }

        public void UpdateZombieNewspaper()//3update
        {
            if (mZombiePhase == ZombiePhase.NewspaperMaddening)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.mLoopCount > 0)
                {
                    mZombiePhase = ZombiePhase.NewspaperMad;
                    if (mBoard.CountZombiesOnScreen() <= 10 && mHasHead)
                    {
                        mApp.PlayFoley(FoleyType.NewspaperRarrgh);
                    }
                    StartWalkAnim(20);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_MADHEAD);
                }
            }
        }

        public void LandFlyer(uint theDamageFlags)
        {
            if (!TodCommon.TestBit(theDamageFlags, 4) && mZombiePhase == ZombiePhase.BalloonFlying)
            {
                mApp.PlaySample(Resources.SOUND_BALLOON_POP);
                mZombiePhase = ZombiePhase.BalloonPopping;
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_pop, ReanimLoopType.PlayOnceAndHold, 20, 24f);
            }
            if (mBoard.mPlantRow[mRow] == PlantRowType.Pool)
            {
                DieWithLoot();
                return;
            }
            mZombieHeight = ZombieHeight.Falling;
        }

        public void UpdateZombieDigger()//3update
        {
            if (mZombiePhase == ZombiePhase.DiggerTunneling)
            {
                if (mPosX < 90f)
                {
                    mZombiePhase = ZombiePhase.DiggerRising;
                    mPhaseCounter = 130;
                    mAltitude = -120f;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_drill, ReanimLoopType.Loop, 0, 20f);
                    mApp.PlayFoley(FoleyType.DirtRise);
                    mApp.PlayFoley(FoleyType.Wakeup);
                    GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.DiggerTunnel, null);
                    StopZombieSound();
                    mApp.AddTodParticle(mPosX + 60f, mPosY + 118f, mRenderOrder + 1, ParticleEffect.DiggerRise);
                    Reanimation reanimation = mApp.AddReanimation(mPosX + 13f, mPosY + 97f, mRenderOrder + 1, ReanimationType.DiggerDirt);
                    reanimation.mAnimRate = 24f;
                }
            }
            else if (mZombiePhase == ZombiePhase.DiggerRising)
            {
                if (mPhaseCounter > 40)
                {
                    mAltitude = TodCommon.TodAnimateCurve(130, 40, mPhaseCounter, -120, 20, TodCurves.EaseOut);
                }
                else
                {
                    mAltitude = TodCommon.TodAnimateCurve(30, 0, mPhaseCounter, 20, 0, TodCurves.EaseIn);
                }
                //if (mPhaseCounter >= 30 && mPhaseCounter < 33)
                if (mPhaseCounter == 30)
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_landing, ReanimLoopType.PlayOnceAndHold, 0, 12f);
                }
                //if (mPhaseCounter >= 0 && mPhaseCounter < 3)
                if (mPhaseCounter == 0)
                {
                    mZombiePhase = ZombiePhase.DiggerStunned;
                    mAltitude = 0f;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_dizzy, ReanimLoopType.Loop, 10, 12f);
                }
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.LawnMower, mRow, 1);
            }
            else if (mZombiePhase == ZombiePhase.DiggerTunnelingPauseWithoutAxe)
            {
                //if (mPhaseCounter >= 150 && mPhaseCounter < 153)
                if (mPhaseCounter == 150)
                {
                    AddAttachedReanim(23, 93, ReanimationType.ZombieSurprise);
                }
                //if (mPhaseCounter >= 0 && mPhaseCounter < 3)
                if (mPhaseCounter == 0)
                {
                    mZombiePhase = ZombiePhase.DiggerRiseWithoutAxe;
                    mPhaseCounter = 130;
                    mAltitude = -120f;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_landing, ReanimLoopType.PlayOnceAndHold, 0, 0f);
                    mApp.PlayFoley(FoleyType.DirtRise);
                    mApp.AddTodParticle(mPosX + 60f, mPosY + 118f, mRenderOrder + 1, ParticleEffect.DiggerRise);
                    Reanimation reanimation2 = mApp.AddReanimation(mPosX + 13f, mPosY + 97f, mRenderOrder + 1, ReanimationType.DiggerDirt);
                    reanimation2.mAnimRate = 24f;
                }
            }
            else if (mZombiePhase == ZombiePhase.DiggerRiseWithoutAxe)
            {
                if (mPhaseCounter > 40)
                {
                    mAltitude = TodCommon.TodAnimateCurve(130, 40, mPhaseCounter, -120, 20, TodCurves.EaseOut);
                }
                else
                {
                    mAltitude = TodCommon.TodAnimateCurve(30, 0, mPhaseCounter, 20, 0, TodCurves.EaseIn);
                }
                //if (mPhaseCounter >= 30 && mPhaseCounter < 33)
                if (mPhaseCounter == 30)
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_landing, ReanimLoopType.PlayOnceAndHold, 20, 12f);
                }
                //if (mPhaseCounter >= 0 && mPhaseCounter < 3)
                if (mPhaseCounter == 0)
                {
                    mZombiePhase = ZombiePhase.DiggerWalkingWithoutAxe;
                    mAltitude = 0f;
                    StartWalkAnim(20);
                }
            }
            else if (mZombiePhase == ZombiePhase.DiggerStunned)
            {
                Reanimation reanimation3 = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation3.mLoopCount > 1)
                {
                    mZombiePhase = ZombiePhase.DiggerWalking;
                    StartWalkAnim(20);
                }
            }
            mUsesClipping = (mAltitude < 0f);
        }

        public bool IsWalkingBackwards()
        {
            if (mMindControlled)
            {
                return true;
            }
            if (mZombieHeight == ZombieHeight.Zombiquarium)
            {
                float num = mVelZ;
                if (num < 1.5707964f || num > 4.712389f)
                {
                    return true;
                }
            }
            if (mZombieType == ZombieType.Digger)
            {
                return mZombiePhase == ZombiePhase.DiggerRising || mZombiePhase == ZombiePhase.DiggerStunned || mZombiePhase == ZombiePhase.DiggerWalking || ((mZombiePhase == ZombiePhase.ZombieDying || mZombiePhase == ZombiePhase.ZombieBurned || mZombiePhase == ZombiePhase.ZombieMowered) && mHasObject);
            }
            return mZombieType == ZombieType.Yeti && !mHasObject;
        }

        public TodParticleSystem AddAttachedParticle(int thePosX, int thePosY, ParticleEffect theEffect)
        {
            if (mDead)
            {
                return null;
            }
            if (!doParticle)
            {
                return null;
            }
            if (GlobalMembersAttachment.IsFullOfAttachments(ref mAttachmentID))
            {
                return null;
            }
            TodParticleSystem todParticleSystem = mApp.AddTodParticle(mX + thePosX, mY + thePosY, 0, theEffect);
            if (todParticleSystem != null && !todParticleSystem.mDead)
            {
                GlobalMembersAttachment.AttachParticle(ref mAttachmentID, todParticleSystem, thePosX, thePosY);
            }
            return todParticleSystem;
        }

        public void PogoBreak(uint theDamageFlags)
        {
            if (!mHasObject)
            {
                return;
            }
            if (!TodCommon.TestBit(theDamageFlags, 4))
            {
                ZombieDrawPosition aDrawPos = default(ZombieDrawPosition);
                GetDrawPos(ref aDrawPos);
                int aRenderOrder = mRenderOrder + 1;
                float aPosX = 0f;
                float aPosY = 0f;
                GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_pogo_stick, ref aPosX, ref aPosY);
                TodParticleSystem aParticle = mApp.AddTodParticle(aPosX, aPosY + 30f, aRenderOrder, ParticleEffect.ZombiePogo);
                OverrideParticleScale(aParticle);
            }
            Debug.ASSERT(mZombiePhase != ZombiePhase.ZombieDying && mZombiePhase != ZombiePhase.ZombieBurned && !mDead);
            mZombieHeight = ZombieHeight.Falling;
            mZombiePhase = ZombiePhase.ZombieNormal;
            StartWalkAnim(0);
            mZombieRect = new TRect(36, 17, 42, 115);
            mZombieAttackRect = new TRect(20, 17, 50, 115);
            mShieldHealth = 0;
            mShieldType = ShieldType.None;
            mHasObject = false;
        }

        public void UpdateZombieFalling()//3update
        {
            //mAltitude -= 3f;
            mAltitude--;
            if (mZombiePhase == ZombiePhase.PolevaulterPreVault)
            {
                //mAltitude -= 3f;
                mAltitude--;
            }
            int num = 0;
            if (IsOnHighGround())
            {
                num = Constants.HIGH_GROUND_HEIGHT;
            }
            if (mAltitude <= num)
            {
                mAltitude = num;
                mZombieHeight = ZombieHeight.ZombieNormal;
            }
        }

        public void UpdateZombieDancer()//1update
        {
            if (mIsEating)
            {
                return;
            }
            if (mSummonCounter > 0)
            {
                mSummonCounter--;
                if (mSummonCounter <= 0)
                {
                    int dancerFrame = GetDancerFrame();
                    if (dancerFrame == 12 && mHasHead && mPosX < Constants.Zombie_Dancer_Dance_Limit_X)
                    {
                        mZombiePhase = ZombiePhase.DancerSnappingFingersWithLight;
                        PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_point, ReanimLoopType.PlayOnceAndHold, 20, 24f);
                    }
                    else
                    {
                        mSummonCounter = 1;
                    }
                }
            }
            switch (mZombiePhase)
            {
            case ZombiePhase.DancerDancingIn:
                if (!mHasHead)
                {
                    return;
                }
                if (mPhaseCounter <= 0)
                {
                    mZombiePhase = ZombiePhase.DancerSnappingFingers;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_point, ReanimLoopType.PlayOnceAndHold, 20, 24f);
                    PickRandomSpeed();
                }
                return;
            case ZombiePhase.DancerSnappingFingers:
            case ZombiePhase.DancerSnappingFingersWithLight:
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.mLoopCount > 0)
                {
                    if (mZombiePhase == ZombiePhase.DancerSnappingFingers && mBoard.CountZombiesOnScreen() <= 15)
                    {
                        mApp.PlayFoley(FoleyType.Dancer);
                    }
                    SummonBackupDancers();
                    mZombiePhase = ZombiePhase.DancerSnappingFingersHold;
                    mPhaseCounter = 200;
                }
                return;
            }

            case ZombiePhase.DancerSnappingFingersHold:
                if (mPhaseCounter > 0)
                {
                    return;
                }
                mZombiePhase = ZombiePhase.DancerDancingLeft;
                PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, 20, 0f);
                break;
            }
            ZombiePhase dancerPhase = GetDancerPhase();
                if (dancerPhase != mZombiePhase)
                {
                    if (dancerPhase == ZombiePhase.DancerDancingLeft)
                    {
                        mZombiePhase = dancerPhase;
                        PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, 10, 0f);
                    }
                    else if (dancerPhase == ZombiePhase.DancerWalkToRaise)
                    {
                        mZombiePhase = dancerPhase;
                        PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.Loop, 10, 18f);
                        Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
                        reanimation2.mAnimTime = 0.6f;
                    }
                    else if (dancerPhase == ZombiePhase.DancerRaiseLeft1 || dancerPhase == ZombiePhase.DancerRaiseRight1 || dancerPhase == ZombiePhase.DancerRaiseLeft2 || dancerPhase == ZombiePhase.DancerRaiseRight2)
                    {
                        mZombiePhase = dancerPhase;
                        PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.Loop, 10, 18f);
                    }
                }
                if (mHasHead && mSummonCounter == 0 && NeedsMoreBackupDancers())
                {
                    mSummonCounter = 100;
                }
                return;
        }

        public Zombie SummonBackupDancer(int theRow, int thePosX)
        {
            if (!mBoard.RowCanHaveZombieType(theRow, ZombieType.BackupDancer))
            {
                return null;
            }
            Zombie aZombie = mBoard.AddZombie(ZombieType.BackupDancer, mFromWave);
            if (aZombie == null)
            {
                return null;
            }
            aZombie.mPosX = thePosX;
            aZombie.mPosY = GetPosYBasedOnRow(theRow);
            aZombie.SetRow(theRow);
            aZombie.mX = (int)aZombie.mPosX;
            aZombie.mY = (int)aZombie.mPosY;
            aZombie.mZombiePhase = ZombiePhase.DancerRising;
            aZombie.mPhaseCounter = 150;
            aZombie.mAltitude = Constants.ZOMBIE_BACKUP_DANCER_RISE_HEIGHT;
            aZombie.mUsesClipping = true;
            aZombie.mRelatedZombieID = mBoard.ZombieGetID(this);
            aZombie.SetAnimRate(0f);
            aZombie.mMindControlled = mMindControlled;
            int aParticleX = (int)aZombie.mPosX + 60;
            int aParticleY = (int)aZombie.mPosY + 110;
            if (aZombie.IsOnHighGround())
            {
                aParticleY -= Constants.HIGH_GROUND_HEIGHT;
            }
            int aRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, theRow, 0);
            mApp.AddTodParticle(aParticleX, aParticleY, aRenderOrder, ParticleEffect.DancerRise);
            mApp.PlayFoley(FoleyType.GravestoneRumble);
            return mBoard.ZombieGetID(aZombie);
        }

        public void SummonBackupDancers()
        {
            if (!mHasHead)
            {
                return;
            }
            for (int i = 0; i < GameConstants.NUM_BACKUP_DANCERS; i++)
            {
                if (mBoard.ZombieTryToGet(mFollowerZombieID[i]) == null)
                {
                    int theRow = 0;
                    int thePosX = 0;
                    switch (i)
                    {
                        case 0:
                            theRow = mRow - 1;
                            thePosX = (int)mPosX;
                            break;
                        case 1:
                            theRow = mRow + 1;
                            thePosX = (int)mPosX;
                            break;
                        case 2:
                            if (mPosX < 130f)
                            {
                                goto IL_D6;
                            }
                            theRow = mRow;
                            thePosX = (int)mPosX - 100;
                            break;
                        case 3:
                            theRow = mRow;
                            thePosX = (int)mPosX + 100;
                            break;
                        default:
                            Debug.ASSERT(false);
                            break;
                    }
                    mFollowerZombieID[i] = SummonBackupDancer(theRow, thePosX);
                    if (mFollowerZombieID[i] != null)
                    {
                        mFollowerZombieID[i].mLeaderZombie = this;
                    }
                    mSummonedDancers = true;
                }
                IL_D6:;
            }
        }

        public int GetDancerFrame()
        {
            if (mFromWave == GameConstants.ZOMBIE_WAVE_UI)
            {
                return 0;
            }
            if (IsImmobilizied())
            {
                return 0;
            }
            int num = 20;
            int num2 = 23;
            if (mZombiePhase == ZombiePhase.DancerDancingIn)
            {
                num2 = 11;
                num = 10;
            }
            //return mApp.mAppCounter % (num * num2) / num;     
            return mBoard.mMainCounter % (num * num2) / num;    // 修复女仆秘籍问题
        }

        public void BungeeStealTarget()
        {
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_grab, ReanimLoopType.PlayOnceAndHold, 20, 24f);
            Plant topPlantAt = mBoard.GetTopPlantAt(mTargetCol, mRow, TopPlant.BungeeOrder);
            if (topPlantAt != null && !topPlantAt.NotOnGround() && topPlantAt.mSeedType != SeedType.Cobcannon)
            {
                Debug.ASSERT(topPlantAt.mSeedType != SeedType.Gravebuster);
                mTargetPlantID = topPlantAt;
                topPlantAt.mOnBungeeState = PlantOnBungeeState.GettingGrabbedByBungee;
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.Projectile, mRow, 0);
            }
        }

        public void UpdateYuckyFace()//1update
        {
            mYuckyFaceCounter++;
            if (mYuckyFaceCounter > GameConstants.YUCKI_SHORT_PAUSE_TIME && mYuckyFaceCounter < GameConstants.YUCKI_HOLD_TIME && !HasYuckyFaceImage())
            {
                StopEating();
                mYuckyFaceCounter = GameConstants.YUCKI_HOLD_TIME;
                if (mBoard.CountZombiesOnScreen() <= 5 && mHasHead)
                {
                    mApp.PlayFoley(FoleyType.Yuck);
                }
                else if (mBoard.CountZombiesOnScreen() <= 10 && mHasHead && RandomNumbers.NextNumber(2) == 0)
                {
                    mApp.PlayFoley(FoleyType.Yuck);
                }
            }
            if (mYuckyFaceCounter > GameConstants.YUCKI_WALK_TIME)
            {
                ShowYuckyFace(false);
                mYuckyFace = false;
                mYuckyFaceCounter = 0;
                if (mYuckySwitchRowsLate)
                {
                    mYuckySwitchRowsLate = false;
                    SetRow(mYuckyToRow);
                }
                return;
            }
            if (mYuckyFaceCounter == GameConstants.YUCKI_PAUSE_TIME)
            {
                StopEating();
                ShowYuckyFace(true);
                if (mBoard.CountZombiesOnScreen() <= 5 && mHasHead)
                {
                    mApp.PlayFoley(FoleyType.Yuck);
                }
                else if (mBoard.CountZombiesOnScreen() <= 10 && mHasHead && RandomNumbers.NextNumber(2) == 0)
                {
                    mApp.PlayFoley(FoleyType.Yuck);
                }
            }
            if (mYuckyFaceCounter == GameConstants.YUCKI_HOLD_TIME)
            {
                StartWalkAnim(20);
                bool aCanGoDown = true;
                bool aCanGoUp = true;
                bool aIsThisRowWaterFilled = mBoard.mPlantRow[mRow] == PlantRowType.Pool;
                if (!mBoard.RowCanHaveZombies(mRow - 1))
                {
                    aCanGoDown = false;
                }
                else if (mBoard.mPlantRow[mRow - 1] == PlantRowType.Pool && !aIsThisRowWaterFilled)
                {
                    aCanGoDown = false;
                }
                else if (mBoard.mPlantRow[mRow - 1] != PlantRowType.Pool && aIsThisRowWaterFilled)
                {
                    aCanGoDown = false;
                }
                if (!mBoard.RowCanHaveZombies(mRow + 1))
                {
                    aCanGoUp = false;
                }
                else if (mBoard.mPlantRow[mRow + 1] == PlantRowType.Pool && !aIsThisRowWaterFilled)
                {
                    aCanGoUp = false;
                }
                else if (mBoard.mPlantRow[mRow + 1] != PlantRowType.Pool && aIsThisRowWaterFilled)
                {
                    aCanGoUp = false;
                }
                if (aCanGoDown && !aCanGoUp)
                {
                    mBoard.ZombieSwitchRow(this, mRow - 1);
                    SetRow(mRow - 1);
                    return;
                }
                if (!aCanGoDown && aCanGoUp)
                {
                    mBoard.ZombieSwitchRow(this, mRow + 1);
                    mYuckyToRow = mRow + 1;
                    mYuckySwitchRowsLate = true;
                    mRow = mYuckyToRow;
                    return;
                }
                if (aCanGoDown && aCanGoUp)
                {
                    if (RandomNumbers.NextNumber(2) == 0)
                    {
                        mBoard.ZombieSwitchRow(this, mRow + 1);
                        mYuckyToRow = mRow + 1;
                        mYuckySwitchRowsLate = true;
                        mRow = mYuckyToRow;
                        return;
                    }
                    mBoard.ZombieSwitchRow(this, mRow - 1);
                    SetRow(mRow - 1);
                    return;
                }
                else
                {
                    Debug.ASSERT(false);
                }
            }
        }

        public void DrawIceTrap(Graphics g, ref ZombieDrawPosition theDrawPos, bool theFront)
        {
            if (mInPool)
            {
                return;
            }
            if (mZombieType == ZombieType.Boss)
            {
                return;
            }
            float aOffsetX = 46f;
            float aOffsetY = 92f + theDrawPos.mBodyY;
            float aScale = 1f;

            switch (mZombieType)
            {
                case ZombieType.Pogo:
                    aOffsetX -= 10f;
                    aOffsetY += 20f;
                    break;
                case ZombieType.Gargantuar:
                case ZombieType.RedeyeGargantuar:
                    aOffsetX -= 20f;
                    aOffsetY -= 7f;
                    aScale = 1.6f;
                    break;
                case ZombieType.Bungee:
                    aOffsetX -= 45f;
                    aOffsetY -= 23f;
                    aScale = 1.2f;
                    break;
                case ZombieType.Digger:
                    aOffsetX -= 27f;
                    break;
                case ZombieType.Catapult:
                    aOffsetX += 32f;
                    break;
                case ZombieType.Balloon:
                    aOffsetX -= 9f;
                    aOffsetY += 27f;
                    break;
            }
            if (theFront)
            {
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_ICETRAP, aOffsetX * Constants.S, aOffsetY * Constants.S, aScale, aScale);
                return;
            }
            TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_ICETRAP2, aOffsetX * Constants.S, aOffsetY * Constants.S, aScale, aScale);
        }

        public bool HitIceTrap()
        {
            bool flag = false;
            if (mChilledCounter > 0 || mIceTrapCounter != 0)
            {
                flag = true;
            }
            ApplyChill(true);
            if (!CanBeFrozen())
            {
                return false;
            }
            if (mInPool)
            {
                mIceTrapCounter = 300;
            }
            else if (flag)
            {
                mIceTrapCounter = TodCommon.RandRangeInt(300, 400);
            }
            else
            {
                mIceTrapCounter = TodCommon.RandRangeInt(400, 600);
            }
            StopZombieSound();
            if (mZombieType == ZombieType.Balloon)
            {
                BalloonPropellerHatSpin(false);
            }
            if (mZombiePhase == ZombiePhase.BossHeadSpit)
            {
                mBoard.RemoveParticleByType(ParticleEffect.ZombieBossFireball);
            }
            TakeDamage(20, 1U, false);
            UpdateAnimSpeed();
            return true;
        }

        public int GetHelmDamageIndex()
        {
            if (mHelmHealth < mHelmMaxHealth / 3)
            {
                return 2;
            }
            if (mHelmHealth < mHelmMaxHealth * 2 / 3)
            {
                return 1;
            }
            return 0;
        }

        public int GetShieldDamageIndex()
        {
            if (mShieldHealth < mShieldMaxHealth / 3)
            {
                return 2;
            }
            if (mShieldHealth < mShieldMaxHealth * 2 / 3)
            {
                return 1;
            }
            return 0;
        }

        public void DrawReanim(Graphics g, ref ZombieDrawPosition theDrawPos, int theBaseRenderGroup)
        {
            Reanimation aReanim = mApp.ReanimationTryToGet(mBodyReanimID);
            if (aReanim == null)
            {
                return;
            }
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            float anImageOffsetX = theDrawPos.mImageOffsetX;
            float anImageOffsetY = theDrawPos.mImageOffsetY + theDrawPos.mBodyY;
            switch (mZombieType)
            {
            case ZombieType.Normal:
                anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Normal * mScaleZombie);
                break;
            case ZombieType.Flag:
                anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Pail * mScaleZombie);
                break;
            case ZombieType.TrafficCone:
            case ZombieType.Pail:
                anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Pail * mScaleZombie);
                break;
            case ZombieType.Polevaulter:
                anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Default * mScaleZombie);
                break;
            case ZombieType.Snorkel:
            {
                if (mZombiePhase == ZombiePhase.ZombieDying || mZombiePhase == ZombiePhase.SnorkelWalking)
                {
                    if (mScaleZombie == 1f)
                    {
                        anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Snorkel_Dying * mScaleZombie);
                    }
                    else
                    {
                        anImageOffsetY -= Constants.Zombie_ClipOffset_Snorkel_Dying_Small;
                        mUsesClipping = true;
                    }
                }
                else if (mZombiePhase == ZombiePhase.SnorkelIntoPool && mScaleZombie != 1f)
                {
                    anImageOffsetY -= Constants.Zombie_ClipOffset_Snorkel_intoPool_Small;
                }
                else
                {
                    anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Snorkel * mScaleZombie);
                }
                if (draggedByTangleKelp)
                {
                    anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Snorkel_Grabbed * mScaleZombie);
                    mUsesClipping = true;
                    break;
                }
                break;
            }
            case ZombieType.DolphinRider:
                anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Dolphin_Into_Pool * mScaleZombie);
                break;
            case ZombieType.Digger:
                anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Digger * mScaleZombie);
                break;
            case ZombieType.PeaHead:
            case ZombieType.WallnutHead:
            case ZombieType.JalapenoHead:
            case ZombieType.GatlingHead:
            case ZombieType.TallnutHead:
                anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_PeaHead_InPool * mScaleZombie);
                break;
            default: anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Default * mScaleZombie); break;
            }
            if (mZombiePhase != ZombiePhase.RisingFromGrave)
            {
                if (mZombiePhase == ZombiePhase.SnorkelIntoPool)
                {
                    anImageOffsetY -= (int)(Constants.Zombie_ClipOffset_Snorkel_Into_Pool * mScaleZombie);
                }
            }
            else if (mScaleZombie == 1f)
            {
                anImageOffsetY += (int)(Constants.Zombie_ClipOffset_RisingFromGrave * mScaleZombie);
            }
            else
            {
                anImageOffsetY += (int)(Constants.Zombie_ClipOffset_RisingFromGrave_Small * mScaleZombie);
            }
            if (mZombieType == ZombieType.Normal && mZombiePhase == ZombiePhase.ZombieNormal && mInPool)
            {
                if (mScaleZombie == 1f)
                {
                    anImageOffsetY += Constants.Zombie_ClipOffset_Normal_In_Pool;
                }
                else
                {
                    anImageOffsetY += Constants.Zombie_ClipOffset_Normal_In_Pool_SMALL;
                }
            }
            if (mZombieType == ZombieType.TrafficCone && mZombiePhase == ZombiePhase.ZombieNormal && mInPool && mScaleZombie != 1f)
            {
                anImageOffsetY += Constants.Zombie_ClipOffset_TrafficCone_In_Pool_SMALL;
            }
            if (mZombieType == ZombieType.Normal && mZombiePhase == ZombiePhase.ZombieDying && mInPool)
            {
                if (mScaleZombie == 1f)
                {
                    anImageOffsetY += Constants.Zombie_ClipOffset_Normal_In_Pool;
                }
                else
                {
                    anImageOffsetY += Constants.Zombie_ClipOffset_Normal_In_Pool_SMALL;
                }
            }
            if (mZombieType == ZombieType.Flag && mInPool)
            {
                anImageOffsetY += Constants.Zombie_ClipOffset_Flag_In_Pool;
            }
            if (mScaleZombie != 1f && mAltitude != 0f && mZombiePhase != ZombiePhase.RisingFromGrave && mInPool)
            {
                anImageOffsetY -= mAltitude;
            }
            if (theDrawPos.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
            {
                float num2 = 120f - theDrawPos.mClipHeight + 71f;
                g.SetClipRect((int)((anImageOffsetX - 200f) * Constants.S), (int)(anImageOffsetY * Constants.S), (int)(520f * Constants.S), (int)(num2 * Constants.S));
            }
            if (mUsesClipping)
            {
                g.mClipRect.mX = g.mClipRect.mX + 1;
                g.HardwareClip();
            }
            int aFadeAlpha = 255;
            if (mZombieFade >= 0)
            {
                aFadeAlpha = TodCommon.ClampInt((int)(255 * mZombieFade / 30f), 0, 255);
            }
            SexyColor aColorOverride = new SexyColor(255, 255, 255, aFadeAlpha);
            SexyColor aExtraAdditiveColor = SexyColor.Black;
            bool aEnableExtraAdditiveDraw = false;
            if (mZombiePhase == ZombiePhase.ZombieBurned)
            {
                aColorOverride = new SexyColor(0, 0, 0, aFadeAlpha);
                aExtraAdditiveColor = SexyColor.Black;
                aEnableExtraAdditiveDraw = false;
            }
            else if (mZombieType == ZombieType.Boss && mZombiePhase != ZombiePhase.ZombieDying && mBodyHealth < mBodyMaxHealth / GameConstants.BOSS_FLASH_HEALTH_FRACTION)
            {
                int num4 = TodCommon.TodAnimateCurve(0, 39, mBoard.mMainCounter % 40, (int)Constants.InvertAndScale(155f), (int)Constants.InvertAndScale(255f), TodCurves.Bounce);
                if (mChilledCounter > 0 || mIceTrapCounter > 0)
                {
                    int num5 = TodCommon.TodAnimateCurve(0, 39, mBoard.mMainCounter % 40, 65, 75, TodCurves.Bounce);
                    aColorOverride = new SexyColor(num5, num5, num4, aFadeAlpha);
                }
                else
                {
                    aColorOverride = new SexyColor(num4, num4, num4, aFadeAlpha);
                }
                aExtraAdditiveColor = SexyColor.Black;
                aEnableExtraAdditiveDraw = false;
            }
            else if (mMindControlled)
            {
                aColorOverride = GameConstants.ZOMBIE_MINDCONTROLLED_COLOR;
                aColorOverride.mAlpha = aFadeAlpha;
                aExtraAdditiveColor = aColorOverride;
                aEnableExtraAdditiveDraw = true;
            }
            else if (mChilledCounter > 0 || mIceTrapCounter > 0)
            {
                aColorOverride = new SexyColor(75, 75, 255, aFadeAlpha);
                aExtraAdditiveColor = aColorOverride;
                aEnableExtraAdditiveDraw = true;
            }
            else if (mZombieHeight == ZombieHeight.Zombiquarium && mBodyHealth < 100)
            {
                aColorOverride = new SexyColor(100, 150, 25, aFadeAlpha);
                aExtraAdditiveColor = aColorOverride;
                aEnableExtraAdditiveDraw = true;
            }
            if (mJustGotShotCounter > 0 && !IsBobsledTeamWithSled() && !GlobalStaticVars.gLowFramerate)
            {
                int num6 = mJustGotShotCounter * 10;
                SexyColor theColor = new SexyColor(num6, num6, num6, 255);
                aExtraAdditiveColor = TodCommon.ColorAdd(theColor, aExtraAdditiveColor);
                aEnableExtraAdditiveDraw = true;
            }
            aReanim.mColorOverride = aColorOverride;
            aReanim.mExtraAdditiveColor = aExtraAdditiveColor;
            aReanim.mEnableExtraAdditiveDraw = aEnableExtraAdditiveDraw;
            switch (mZombieType)
            {
            case ZombieType.Bobsled:
                DrawBobsledReanim(g, ref theDrawPos, true);
                aReanim.DrawRenderGroup(g, theBaseRenderGroup);
                DrawBobsledReanim(g, ref theDrawPos, false);
                break;
            case ZombieType.Bungee:
                DrawBungeeReanim(g, ref theDrawPos);
                break;
            case ZombieType.Dancer:
                DrawDancerReanim(g, ref theDrawPos);
                break;
            default:
                aReanim.DrawRenderGroup(g, theBaseRenderGroup);
                break;
            }
            if (mShieldType != ShieldType.None)
            {
                if (mZombiePhase == ZombiePhase.ZombieBurned)
                {
                    aReanim.mColorOverride = new SexyColor(0, 0, 0, aFadeAlpha);
                    aReanim.mExtraAdditiveColor = SexyColor.Black;
                    aReanim.mEnableExtraAdditiveDraw = false;
                }
                else if (mShieldJustGotShotCounter > 0)
                {
                    aReanim.mColorOverride = new SexyColor(255, 255, 255, aFadeAlpha);
                    aReanim.mExtraAdditiveColor = SexyColor.White;
                    aReanim.mEnableExtraAdditiveDraw = true;
                }
                else
                {
                    aReanim.mColorOverride = new SexyColor(255, 255, 255, aFadeAlpha);
                    aReanim.mExtraAdditiveColor = SexyColor.Black;
                    aReanim.mEnableExtraAdditiveDraw = false;
                }
                float aShieldHitOffset = 0f;
                if (mShieldRecoilCounter > 0)
                {
                    aShieldHitOffset = TodCommon.TodAnimateCurveFloat(12, 0, mShieldRecoilCounter, 3f, 0f, TodCurves.Linear);
                }
                g.mTransX += (int)aShieldHitOffset;
                aReanim.DrawRenderGroup(g, GameConstants.RENDER_GROUP_SHIELD);
                g.mTransX -= (int)aShieldHitOffset;
            }
            if (mShieldType == ShieldType.Newspaper || mShieldType == ShieldType.Door || mShieldType == ShieldType.Ladder)
            {
                aReanim.mColorOverride = aColorOverride;
                aReanim.mExtraAdditiveColor = aExtraAdditiveColor;
                aReanim.mEnableExtraAdditiveDraw = aEnableExtraAdditiveDraw;
                aReanim.DrawRenderGroup(g, GameConstants.RENDER_GROUP_OVER_SHIELD);
            }
            g.ClearClipRect();
            if (mUsesClipping)
            {
                g.EndHardwareClip();
            }
        }

        public void UpdatePlaying()//3update
        {
            Debug.ASSERT(mBodyHealth > 0 || mZombiePhase == ZombiePhase.BobsledCrashing);
            //mGroanCounter -= 3;
            mGroanCounter--;
            int count = mBoard.mZombies.Count;
            if (mGroanCounter <= 0 && RandomNumbers.NextNumber(count) == 0 && mHasHead && mZombieType != ZombieType.Boss && !mBoard.HasLevelAwardDropped())
            {
                float aPitch = 0f;
                if (mApp.IsLittleTroubleLevel())
                {
                    aPitch = TodCommon.RandRangeFloat(40f, 50f);
                }
                if (mZombieType == ZombieType.Gargantuar)
                {
                    mApp.PlayFoley(FoleyType.Lowgroan);
                }
                else if (mVariant)
                {
                    mApp.PlayFoleyPitch(FoleyType.Brains, aPitch);
                }
                else
                {
                    mApp.PlayFoleyPitch(FoleyType.Groan, aPitch);
                }
                mGroanCounter = RandomNumbers.NextNumber(1000) + 500;
            }
            if (mIceTrapCounter > 0)
            {
                //mIceTrapCounter -= 3;
                mIceTrapCounter--;
                if (mIceTrapCounter <= 0)
                {
                    RemoveIceTrap();
                    AddAttachedParticle(75, 106, ParticleEffect.IceTrapRelease);
                }
            }
            if (mChilledCounter > 0)
            {
               // mChilledCounter -= 3;
                mChilledCounter--;
                if (mChilledCounter <= 0)
                {
                    UpdateAnimSpeed();
                }
            }
            if (mButteredCounter > 0)
            {
                //mButteredCounter -= 3;
                mButteredCounter--;
                if (mButteredCounter <= 0)
                {
                    RemoveButter();
                }
            }
            if (mZombiePhase == ZombiePhase.RisingFromGrave)
            {
                UpdateZombieRiseFromGrave();
                return;
            }
            mBodyReanimID.mClip = false;
            if (!IsImmobilizied())
            {
                UpdateActions();
                UpdateZombiePosition();
                CheckIfPreyCaught();
                CheckForPool();
                CheckForHighGround();
                CheckForBoardEdge();
            }
            if (mZombieType == ZombieType.Boss)
            {
                UpdateBoss();
            }
            if (!IsDeadOrDying() && mFromWave != GameConstants.ZOMBIE_WAVE_WINNER)
            {
                bool flag = !mHasHead;
                if ((mZombieType == ZombieType.Zamboni || mZombieType == ZombieType.Catapult) && mBodyHealth < 200)
                {
                    flag = true;
                }
                if (flag)
                {
                    int theDamage = 1;
                    if (mZombieType == ZombieType.Yeti)
                    {
                        theDamage = 10;
                    }
                    if (mBodyMaxHealth >= 500)
                    {
                        theDamage = 3;
                    }
                    if (RandomNumbers.NextNumber(5) == 0)
                    {
                        TakeDamage(theDamage, 9U, false);
                    }
                }
            }
        }

        public bool NeedsMoreBackupDancers()
        {
            for (int i = 0; i < GameConstants.NUM_BACKUP_DANCERS; i++)
            {
                if (mBoard.ZombieTryToGet(mFollowerZombieID[i]) == null && (i != 0 || mBoard.RowCanHaveZombieType(mRow - 1, ZombieType.BackupDancer)) && (i != 1 || mBoard.RowCanHaveZombieType(mRow + 1, ZombieType.BackupDancer)))
                {
                    return true;
                }
            }
            return false;
        }

        public void ConvertToNormalZombie()
        {
            StopZombieSound();
            mPosY = GetPosYBasedOnRow(mRow);
            mX = (int)mPosX;
            mY = (int)mPosY;
            mZombieType = ZombieType.Normal;
            mZombiePhase = ZombiePhase.ZombieNormal;
            mZombieAttackRect = new TRect(50, 0, 20, 115);
            mAnimFrames = 12;
            mAnimTicksPerFrame = 12;
            mPhaseCounter = 0;
            PickRandomSpeed();
        }

        public void StartEating()
        {
            if (mIsEating)
            {
                return;
            }
            mIsEating = true;
            if (mZombiePhase == ZombiePhase.DiggerTunneling)
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.LadderCarrying)
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_laddereat, ReanimLoopType.Loop, 20, 0f);
                return;
            }
            if (mZombiePhase == ZombiePhase.NewspaperMad)
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_eat_nopaper, ReanimLoopType.Loop, 20, 0f);
                return;
            }
            if (mZombieType != ZombieType.Snorkel)
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_eat, ReanimLoopType.Loop, 20, 0f);
            }
            if (mShieldType == ShieldType.Door)
            {
                ShowDoorArms(false);
            }
        }

        public void StopEating()
        {
            if (!mIsEating)
            {
                return;
            }
            mIsEating = false;
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (mZombiePhase == ZombiePhase.DiggerTunneling)
            {
                return;
            }
            if (reanimation != null && mZombieType != ZombieType.Snorkel)
            {
                StartWalkAnim(20);
            }
            if (mShieldType == ShieldType.Door)
            {
                ShowDoorArms(true);
            }
            UpdateAnimSpeed();
        }

        public void UpdateAnimSpeed()//3update
        {
            if (!IsOnBoard())
            {
                return;
            }
            Reanimation aReanim = mApp.ReanimationTryToGet(mBodyReanimID);
            if (aReanim == null)
            {
                return;
            }
            if (IsImmobilizied())
            {
                ApplyAnimRate(0f);
                return;
            }
            if (mYuckyFace && mYuckyFaceCounter < GameConstants.YUCKI_HOLD_TIME)
            {
                ApplyAnimRate(0f);
                return;
            }
            if (mZombiePhase == ZombiePhase.SnorkelUpToEat || mZombiePhase == ZombiePhase.SnorkelDownFromEat || IsDeadOrDying())
            {
                ApplyAnimRate(mOrginalAnimRate);
                return;
            }
            if (mIsEating)
            {
                if (mZombieType == ZombieType.Polevaulter || mZombieType == ZombieType.Balloon || mZombieType == ZombieType.Imp || mZombieType == ZombieType.Digger || mZombieType == ZombieType.JackInTheBox || mZombieType == ZombieType.Snorkel || mZombieType == ZombieType.Yeti)
                {
                    ApplyAnimRate(20f);
                    return;
                }
                ApplyAnimRate(36f);
                return;
            }
            else
            {
                if (ZombieNotWalking())
                {
                    ApplyAnimRate(mOrginalAnimRate);
                    return;
                }
                if (IsBobsledTeamWithSled() || mZombieType == ZombieType.Catapult || mZombiePhase == ZombiePhase.DolphinRiding || mZombiePhase == ZombiePhase.SnorkelWalkingInPool)
                {
                    ApplyAnimRate(mOrginalAnimRate);
                    return;
                }
                if (!mHasGroundTrack)
                {
                    return;
                }
                ReanimatorTrack aTrack = aReanim.mDefinition.mTracks[aReanim.FindTrackIndex(Reanimation.ReanimTrackId__ground)];
                float aDistance = aTrack.mTransforms[aReanim.mFrameStart + aReanim.mFrameCount - 1].mTransX
                             - aTrack.mTransforms[aReanim.mFrameStart].mTransX;
                if (aDistance < 1E-06f)
                {
                    return;
                }
                float num3 = aReanim.mFrameCount / aDistance;
                float theAnimRate = mVelX * num3 * 47f / mScaleZombie;
                ApplyAnimRate(theAnimRate);
                return;
            }
        }

        public void ReanimShowPrefix(string theTrackPrefix, int theRenderGroup)
        {
            mApp.ReanimationTryToGet(mBodyReanimID)?.AssignRenderGroupToPrefix(theTrackPrefix, theRenderGroup);
        }

        public void DetachPlantHead()
        {
            ReanimatorTrackInstance trackInstanceByName = mBodyReanimID.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
            GlobalMembersAttachment.AttachmentDetach(ref trackInstanceByName.mAttachmentID);
        }

        public void DetachFlag()
        {
            ReanimatorTrackInstance trackInstanceByName = mBodyReanimID.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand);
            GlobalMembersAttachment.AttachmentDetach(ref trackInstanceByName.mAttachmentID);
        }

        public void PlayDeathAnim(uint theDamageFlags)
        {
            if (mZombiePhase == ZombiePhase.ZombieDying || mZombiePhase == ZombiePhase.ZombieBurned || mZombiePhase == ZombiePhase.ZombieMowered)
            {
                return;
            }
            Reanimation aBodyReanim = mApp.ReanimationTryToGet(mBodyReanimID);
            if (aBodyReanim == null || !aBodyReanim.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_death))
            {
                DieNoLoot(true);
                return;
            }
            if (mZombieType == ZombieType.DolphinRider && mZombiePhase != ZombiePhase.DolphinWalkingInPool)
            {
                DieNoLoot(true);
                return;
            }
            if (mZombiePhase == ZombiePhase.SnorkelIntoPool || mZombiePhase == ZombiePhase.SnorkelWalking)
            {
                DieNoLoot(true);
                return;
            }
            if (mIceTrapCounter > 0)
            {
                AddAttachedParticle(75, 106, ParticleEffect.IceTrapRelease);
                mIceTrapCounter = 0;
            }
            if (mButteredCounter > 0)
            {
                mButteredCounter = 0;
            }
            if (mYuckyFace)
            {
                ShowYuckyFace(false);
                mYuckyFace = false;
                mYuckyFaceCounter = 0;
            }
            if (TodCommon.TestBit(theDamageFlags, 4) && mZombieType != ZombieType.Boss && mZombieType != ZombieType.Gargantuar && mZombieType != ZombieType.RedeyeGargantuar)
            {
                DieNoLoot(true);
                return;
            }
            if (mZombieType == ZombieType.Pogo)
            {
                mAltitude = 0f;
            }
            GlobalMembersAttachment.AttachmentReanimTypeDie(ref mAttachmentID, ReanimationType.ZombieSurprise);
            StopEating();
            if (mShieldType != ShieldType.None)
            {
                DropShield(1U);
            }
            if (mZombieType == ZombieType.SquashHead && !mHasHead)
            {
                DetachPlantHead();
                mApp.RemoveReanimation(ref mSpecialHeadReanimID);
                mSpecialHeadReanimID = null;
            }
            mZombiePhase = ZombiePhase.ZombieDying;
            mVelX = 0f;
            if (mZombieHeight == ZombieHeight.Zombiquarium)
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_aquarium_death, ReanimLoopType.PlayOnceAndHold, 20, 14f);
                return;
            }
            if (mZombieHeight == ZombieHeight.UpLadder)
            {
                mZombieHeight = ZombieHeight.Falling;
            }
            float aDeathAnimRate;
            if (mZombieType == ZombieType.Football)
            {
                aDeathAnimRate = 24f;
            }
            else if (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
            {
                aDeathAnimRate = 14f;
                mApp.PlayFoley(FoleyType.Gargantudeath);
            }
            else if (mZombieType == ZombieType.Snorkel)
            {
                aDeathAnimRate = 14f;
            }
            else if (mZombieType == ZombieType.Digger)
            {
                aDeathAnimRate = 18f;
            }
            else if (mZombieType == ZombieType.Yeti)
            {
                aDeathAnimRate = 14f;
            }
            else if (mZombieType == ZombieType.Boss)
            {
                aDeathAnimRate = 18f;
                BossDie();
                Reanimation reanimation2 = mApp.ReanimationGet(mSpecialHeadReanimID);
                reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_death, ReanimLoopType.PlayOnceAndHold, 20, aDeathAnimRate);
            }
            else
            {
                aDeathAnimRate = TodCommon.RandRangeFloat(24f, 30f);
            }
            string text = GlobalMembersReanimIds.ReanimTrackId_anim_death;
            int num = RandomNumbers.NextNumber(100);
            bool flag = mApp.HasFinishedAdventure() || mBoard.mLevel > 5;
            if (mInPool && aBodyReanim.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_waterdeath))
            {
                text = GlobalMembersReanimIds.ReanimTrackId_anim_waterdeath;
                ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_duckytube, false);
            }
            else if (num == 99 && aBodyReanim.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_superlongdeath) && flag && mChilledCounter == 0 && mBoard.CountZombiesOnScreen() <= 5)
            {
                aDeathAnimRate = 14f;
                text = GlobalMembersReanimIds.ReanimTrackId_anim_superlongdeath;
            }
            else if (num > 50 && aBodyReanim.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_death2))
            {
                text = GlobalMembersReanimIds.ReanimTrackId_anim_death2;
            }
            PlayZombieReanim(ref text, ReanimLoopType.PlayOnceAndHold, 20, aDeathAnimRate);
            ReanimShowPrefix("anim_tongue", -1);
        }

        public void UpdateDeath()//3update
        {
            Reanimation aReanim = mApp.ReanimationTryToGet(mBodyReanimID);
            if (aReanim == null)
            {
                DieNoLoot(true);
                return;
            }
            if (mZombieHeight == ZombieHeight.Falling)
            {
                UpdateZombieFalling();
            }
            if (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
            {
                if (aReanim.ShouldTriggerTimedEvent(0.89f))
                {
                    mBoard.ShakeBoard(0, 3);
                }
                else if (aReanim.ShouldTriggerTimedEvent(0.98f))
                {
                    mBoard.ShakeBoard(0, 1);
                }
            }
            float aFallTime = -1f;
            if (mInPool || mZombieType == ZombieType.Snorkel || mZombieType == ZombieType.Zamboni || mZombieType == ZombieType.DolphinRider || mZombieType == ZombieType.Bungee || mZombieType == ZombieType.Catapult || mZombieType == ZombieType.Imp || mZombieType == ZombieType.Boss)
            {
                aFallTime = -1f;
            }
            else if (mZombieType == ZombieType.Normal || mZombieType == ZombieType.Flag || mZombieType == ZombieType.TrafficCone || mZombieType == ZombieType.Pail || mZombieType == ZombieType.Door || mZombieType == ZombieType.PeaHead || mZombieType == ZombieType.WallnutHead || mZombieType == ZombieType.TallnutHead || mZombieType == ZombieType.JalapenoHead || mZombieType == ZombieType.GatlingHead || mZombieType == ZombieType.SquashHead || mZombieType == ZombieType.DuckyTube)
            {
                if (aReanim.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_superlongdeath))
                {
                    aFallTime = 0.788f;
                }
                else if (aReanim.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_death2))
                {
                    aFallTime = 0.71f;
                }
                else
                {
                    aFallTime = 0.77f;
                }
            }
            else if (mZombieType == ZombieType.Polevaulter)
            {
                aFallTime = 0.68f;
            }
            else if (mZombieType == ZombieType.Football)
            {
                aFallTime = 0.52f;
            }
            else if (mZombieType == ZombieType.Newspaper)
            {
                aFallTime = 0.63f;
            }
            else if (mZombieType == ZombieType.Dancer || mZombieType == ZombieType.BackupDancer)
            {
                aFallTime = 0.83f;
            }
            else if (mZombieType == ZombieType.Bobsled)
            {
                aFallTime = 0.81f;
            }
            else if (mZombieType == ZombieType.JackInTheBox)
            {
                aFallTime = 0.64f;
            }
            else if (mZombieType == ZombieType.Balloon)
            {
                aFallTime = 0.68f;
            }
            else if (mZombieType == ZombieType.Digger)
            {
                aFallTime = 0.85f;
            }
            else if (mZombieType == ZombieType.Pogo)
            {
                aFallTime = 0.84f;
            }
            else if (mZombieType == ZombieType.Yeti)
            {
                aFallTime = 0.68f;
            }
            else if (mZombieType == ZombieType.Ladder)
            {
                aFallTime = 0.62f;
            }
            else if (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
            {
                aFallTime = 0.86f;
            }
            if (aFallTime > 0f && aReanim.ShouldTriggerTimedEvent(aFallTime))
            {
                mApp.PlayFoley(FoleyType.ZombieFalling);
                if (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
                {
                    mApp.PlayFoley(FoleyType.Thump);
                    mApp.Vibrate();
                }
                if (mBoard.mDaisyMode)
                {
                    DoDaisies();
                }
            }
            if (mZombieType == ZombieType.Boss)
            {
                if (aReanim.ShouldTriggerTimedEvent(0.1f) || aReanim.ShouldTriggerTimedEvent(0.12f) || aReanim.ShouldTriggerTimedEvent(0.15f) || aReanim.ShouldTriggerTimedEvent(0.19f) || aReanim.ShouldTriggerTimedEvent(0.2f) || aReanim.ShouldTriggerTimedEvent(0.26f) || aReanim.ShouldTriggerTimedEvent(0.3f) || aReanim.ShouldTriggerTimedEvent(0.4f) || aReanim.ShouldTriggerTimedEvent(0.42f) || aReanim.ShouldTriggerTimedEvent(0.5f) || aReanim.ShouldTriggerTimedEvent(0.58f) || aReanim.ShouldTriggerTimedEvent(0.61f) || aReanim.ShouldTriggerTimedEvent(0.71f))
                {
                    float aExplosionPosX = TodCommon.RandRangeFloat(600f, 750f);
                    float aExplosionPosY = TodCommon.RandRangeFloat(50f, 300f);
                    mApp.AddTodParticle(aExplosionPosX, aExplosionPosY, 400000, ParticleEffect.BossExplosion);
                    mApp.PlayFoley(FoleyType.BossExplosionSmall);
                }
                Reanimation aReanim2 = mApp.ReanimationTryToGet(mSpecialHeadReanimID);
                if (aReanim.ShouldTriggerTimedEvent(0.93f))
                {
                    mBoard.ShakeBoard(1, 2);
                    mApp.PlayFoley(FoleyType.BossExplosionSmall);
                    mApp.PlayFoley(FoleyType.Thump);
                    mApp.Vibrate();
                }
                if (aReanim.ShouldTriggerTimedEvent(0.99f))
                {
                    aReanim2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_flag, ReanimLoopType.PlayOnceAndHold, 20, 30f);
                }
                if (aReanim2.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_flag) && aReanim2.mLoopCount > 0)
                {
                    aReanim2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_flag_loop, ReanimLoopType.Loop, 20, 17f);
                }
                if (aReanim.mLoopCount > 0)
                {
                    DropLoot();
                }
            }
            if (mZombieType == ZombieType.Zamboni && mPhaseCounter > 0)
            {
                //mPhaseCounter -= 3;
                mPhaseCounter--;
                if (mPhaseCounter <= 0)
                {
                    aReanim = mApp.ReanimationTryToGet(mBodyReanimID);
                    if (aReanim.IsTrackShowing(GlobalMembersReanimIds.ReanimTrackId_anim_wheelie2))
                    {
                        mApp.AddTodParticle(mPosX + 80f, mPosY + 60f, mRenderOrder + 1, ParticleEffect.ZamboniExplosion2);
                    }
                    else
                    {
                        mApp.AddTodParticle(mPosX + 80f, mPosY + 60f, mRenderOrder + 1, ParticleEffect.ZamboniExplosion);
                    }
                    DieWithLoot();
                    mApp.PlayFoley(FoleyType.Explosion);
                }
                return;
            }
            if (mZombieType == ZombieType.Catapult)
            {
                //mPhaseCounter -= 3;
                mPhaseCounter--;
                if (mPhaseCounter <= 0)
                {
                    mApp.AddTodParticle(mPosX + 80f, mPosY + 60f, mRenderOrder + 1, ParticleEffect.CatapultExplosion);
                    DieWithLoot();
                    mApp.PlayFoley(FoleyType.Explosion);
                }
                return;
            }
            if (mZombieFade == -1 && aReanim.mLoopCount > 0 && mZombieType != ZombieType.Boss)
            {
                if (mInPool)
                {
                    mZombieFade = 30;
                    return;
                }
                mZombieFade = 100;
            }
        }

        public void DrawShadow(Graphics g)
        {
            ZombieDrawPosition aDrawPos = default(ZombieDrawPosition);
            GetDrawPos(ref aDrawPos);
            if (mApp.mGameScene == GameScenes.ZombiesWon && !SetupDrawZombieWon(g))
            {
                return;
            }
            int aUseShadow2 = 0;
            float aShadowOffsetX = aDrawPos.mImageOffsetX;
            float aShadowOffsetY = aDrawPos.mImageOffsetY + aDrawPos.mBodyY;
            float aScale = mScaleZombie;
            aShadowOffsetX += mScaleZombie * 20f - 20f;
            if (IsOnBoard() && mBoard.StageIsNight())
            {
                aUseShadow2 = 1;
            }
            switch (mZombieType)
            {
            case ZombieType.Football:
                if (IsWalkingBackwards())
                {
                    aShadowOffsetX += -11f * mScaleZombie;
                }
                else
                {
                    aShadowOffsetX += 20f + 21f * mScaleZombie;
                }
                aShadowOffsetY += 16f;
                break;
            case ZombieType.Newspaper:
                if (IsWalkingBackwards())
                {
                    aShadowOffsetX += 5f;
                }
                else
                {
                    aShadowOffsetX += 29f;
                }
                break;
            case ZombieType.Polevaulter:
                if (IsWalkingBackwards())
                {
                    aShadowOffsetX += -5f;
                }
                else
                {
                    aShadowOffsetX += 36f;
                }
                aShadowOffsetY += 11f;
                break;
            case ZombieType.Bobsled:
                if (IsWalkingBackwards())
                {
                    aShadowOffsetX += 13f;
                }
                else
                {
                    aShadowOffsetX += 20f;
                }
                aShadowOffsetY += 13f;
                break;
            case ZombieType.Imp:
                aScale *= 0.6f;
                aShadowOffsetY += 7f;
                if (IsWalkingBackwards())
                {
                    aShadowOffsetX += 13f;
                }
                else
                {
                    aShadowOffsetX += 25f;
                }
                break;
            case ZombieType.Digger:
                aShadowOffsetY += 5f;
                if (IsWalkingBackwards())
                {
                    aShadowOffsetX += 14f;
                }
                else
                {
                    aShadowOffsetX += 17f;
                }
                break;
            case ZombieType.Snorkel:
                aShadowOffsetY += 5f;
                if (IsWalkingBackwards())
                {
                    aShadowOffsetX += -2f;
                }
                else
                {
                    aShadowOffsetX += 35f;
                }
                break;
            case ZombieType.DolphinRider:
                aShadowOffsetY += 11f;
                if (IsWalkingBackwards())
                {
                    aShadowOffsetX += 15f;
                }
                else
                {
                    aShadowOffsetX += 19f;
                }
                break;
            case ZombieType.Yeti:
                aShadowOffsetY += 20f;
                if (IsWalkingBackwards())
                {
                    aShadowOffsetX += 20f;
                }
                else
                {
                    aShadowOffsetX += 3f;
                }
                break;
            case ZombieType.Gargantuar:
            case ZombieType.RedeyeGargantuar:
                aScale *= 1.5f;
                aShadowOffsetX += 27f;
                aShadowOffsetY += 7f;
                break;
            default:
                if (mApp.ReanimationTryToGet(mBodyReanimID) != null)
                {
                    if (IsWalkingBackwards())
                    {
                        aShadowOffsetX += 11f;
                    }
                    else
                    {
                        aShadowOffsetX += 23f;
                    }
                }
                else if (IsWalkingBackwards())
                {
                    aShadowOffsetX += -2f;
                }
                else
                {
                    aShadowOffsetX += 35f;
                }

                break;
            }
            switch (mZombieType)
            {
            case ZombieType.Newspaper:
                aShadowOffsetY += 4f;
                break;
            case ZombieType.Balloon:
                aShadowOffsetY += 13f;
                break;
            case ZombieType.Bungee:
                aShadowOffsetX += -12f;
                aScale = TodCommon.TodAnimateCurveFloat(GameConstants.BUNGEE_ZOMBIE_HEIGHT - 1000, 100, (int)mAltitude, 0.1f, 1.5f, TodCurves.Linear);
                break;
            case ZombieType.Dancer:
            case ZombieType.BackupDancer:
                aShadowOffsetY -= 18f;
                break;
            }
            if (mZombieHeight == ZombieHeight.UpLadder || mZombieHeight == ZombieHeight.Falling || mZombiePhase == ZombiePhase.ImpGettingThrown || mZombieType == ZombieType.Bungee || IsBouncingPogo() || IsFlying())
            {
                aShadowOffsetY += mAltitude;
                if (mOnHighGround)
                {
                    aShadowOffsetY -= Constants.HIGH_GROUND_HEIGHT;
                }
            }
            if (mUsesClipping)
            {
                g.HardwareClip();
            }
            if (mInPool)
            {
                aShadowOffsetY += 67f;
                TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_WHITEWATER_SHADOW, aShadowOffsetX * Constants.S, aShadowOffsetY * Constants.S, aScale, aScale);
            }
            else
            {
                aShadowOffsetY += 92f;
                if (aUseShadow2 == 0)
                {
                    TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW, aShadowOffsetX * Constants.S, aShadowOffsetY * Constants.S, aScale, aScale);
                }
                else
                {
                    TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW2, aShadowOffsetX * Constants.S, aShadowOffsetY * Constants.S, aScale, aScale);
                }
            }
            if (mUsesClipping)
            {
                g.EndHardwareClip();
            }
            g.ClearClipRect();
        }

        public bool HasShadow()
        {
            return mZombiePhase != ZombiePhase.ZombieDying
                && mZombiePhase != ZombiePhase.DiggerRising
                && mZombiePhase != ZombiePhase.DiggerTunnelingPauseWithoutAxe
                && mZombiePhase != ZombiePhase.DiggerRiseWithoutAxe
                && mZombiePhase != ZombiePhase.DiggerTunneling
                && mZombiePhase != ZombiePhase.RisingFromGrave
                && mZombiePhase != ZombiePhase.DancerRising
                && mZombiePhase != ZombiePhase.BobsledBoarding
                && mZombiePhase != ZombiePhase.PolevaulterInVault
                && mZombiePhase != ZombiePhase.DolphinIntoPool
                && mZombiePhase != ZombiePhase.SnorkelIntoPool
                && mZombieType != ZombieType.Zamboni
                && mZombieType != ZombieType.Catapult
                && mZombieType != ZombieType.Boss
                && (mZombieType != ZombieType.Bungee || (IsOnBoard() && !mHitUmbrella))
                && mZombieHeight != ZombieHeight.DraggedUnder
                && mZombieHeight != ZombieHeight.InToChimney
                && mZombieHeight != ZombieHeight.GettingBungeeDropped
                && !mInPool
                && (mApp.mGameMode != GameMode.ChallengeInvisighoul || mFromWave == GameConstants.ZOMBIE_WAVE_UI);
        }

        public Reanimation LoadReanim(ReanimationType theReanimationType)
        {
            Reanimation reanimation = mApp.AddReanimation(0f, 0f, 0, theReanimationType);
            mBodyReanimID = mApp.ReanimationGetID(reanimation);
            reanimation.mLoopType = ReanimLoopType.Loop;
            reanimation.mIsAttachment = true;
            mHasGroundTrack = reanimation.TrackExists(Reanimation.ReanimTrackId__ground);
            if (!IsOnBoard())
            {
                int num = RandomNumbers.NextNumber(4);
                if (num > 0 && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle2))
                {
                    float theAnimRate = TodCommon.RandRangeFloat(12f, 24f);
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle2, ReanimLoopType.Loop, 0, theAnimRate);
                }
                else if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
                {
                    float theAnimRate2 = TodCommon.RandRangeFloat(12f, 18f);
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, theAnimRate2);
                }
                reanimation.mAnimTime = TodCommon.RandRangeFloat(0f, 0.99f);
            }
            else
            {
                StartWalkAnim(0);
            }
            return reanimation;
        }

        public int TakeFlyingDamage(int theDamage, uint theDamageFlags)
        {
            if (!TodCommon.TestBit(theDamageFlags, 3))
            {
                mJustGotShotCounter = 25;
            }
            int num = Math.Min(mFlyingHealth, theDamage);
            int result = theDamage - num;
            mFlyingHealth -= num;
            if (mFlyingHealth == 0)
            {
                LandFlyer(theDamageFlags);
            }
            return result;
        }

        public int TakeShieldDamage(int theDamage, uint theDamageFlags)
        {
            if (!TodCommon.TestBit(theDamageFlags, 3))
            {
                mShieldJustGotShotCounter = 25;
                if (mJustGotShotCounter < 0)
                {
                    mJustGotShotCounter = 0;
                }
            }
            if (!TodCommon.TestBit(theDamageFlags, 3) && !TodCommon.TestBit(theDamageFlags, 1))
            {
                mShieldRecoilCounter = 12;
                if (mShieldType == ShieldType.Door || mShieldType == ShieldType.Ladder)
                {
                    mApp.PlayFoley(FoleyType.ShieldHit);
                }
            }
            int shieldDamageIndex = GetShieldDamageIndex();
            int aDamageRemaining = theDamage - Math.Min(mShieldHealth, theDamage);
            mShieldHealth -= Math.Min(mShieldHealth, theDamage);
            if (mShieldHealth == 0)
            {
                DropShield(theDamageFlags);
                return aDamageRemaining;
            }
            int shieldDamageIndex2 = GetShieldDamageIndex();
            if (shieldDamageIndex != shieldDamageIndex2 || justLoaded)
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
                if (mShieldType == ShieldType.Door && shieldDamageIndex2 == 1)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_screendoor, AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR2);
                }
                else if (mShieldType == ShieldType.Door && shieldDamageIndex2 == 2)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_screendoor, AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR3);
                }
                else if (mShieldType == ShieldType.Newspaper && shieldDamageIndex2 == 1)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_paper_paper, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_PAPER2);
                }
                else if (mShieldType == ShieldType.Newspaper && shieldDamageIndex2 == 2)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_paper_paper, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_PAPER3);
                }
                else if (mShieldType == ShieldType.Ladder && shieldDamageIndex2 == 1)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_ladder_1, AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1_DAMAGE1);
                }
                else if (mShieldType == ShieldType.Ladder && shieldDamageIndex2 == 2)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_ladder_1, AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1_DAMAGE2);
                }
            }
            return aDamageRemaining;
        }

        public int TakeHelmDamage(int theDamage, uint theDamageFlags)
        {
            if (!TodCommon.TestBit(theDamageFlags, 3))
            {
                mJustGotShotCounter = 25;
            }
            int helmDamageIndex = GetHelmDamageIndex();
            int aDamageRemaining = theDamage - Math.Min(mHelmHealth, theDamage);
            mHelmHealth -= Math.Min(mHelmHealth, theDamage);
            if (TodCommon.TestBit(theDamageFlags, 2))
            {
                ApplyChill(false);
            }
            if (mHelmHealth == 0)
            {
                DropHelm(theDamageFlags);
                return aDamageRemaining;
            }
            int helmDamageIndex2 = GetHelmDamageIndex();
            if ((helmDamageIndex != helmDamageIndex2 && mBodyReanimID.mActive) || (justLoaded && mBodyReanimID.mActive))
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
                if (mHelmType == HelmType.TrafficCone && helmDamageIndex2 == 1 && reanimation != null)
                {
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_cone, AtlasResources.IMAGE_REANIM_ZOMBIE_CONE2);
                }
                else if (mHelmType == HelmType.TrafficCone && helmDamageIndex2 == 2 && reanimation != null)
                {
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_cone, AtlasResources.IMAGE_REANIM_ZOMBIE_CONE3);
                }
                else if (mHelmType == HelmType.Pail && helmDamageIndex2 == 1)
                {
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_bucket, AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET2);
                }
                else if (mHelmType == HelmType.Pail && helmDamageIndex2 == 2)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_bucket, AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET3);
                }
                else if (mHelmType == HelmType.Digger && helmDamageIndex2 == 1)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_digger_hardhat, AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_HARDHAT2);
                }
                else if (mHelmType == HelmType.Digger && helmDamageIndex2 == 2)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_digger_hardhat, AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_HARDHAT3);
                }
                else if (mHelmType == HelmType.Football && helmDamageIndex2 == 1)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_football_helmet, AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET2);
                }
                else if (mHelmType == HelmType.Football && helmDamageIndex2 == 2)
                {
                    Debug.ASSERT(reanimation != null);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_football_helmet, AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET3);
                }
                else if (mHelmType == HelmType.Wallnut && helmDamageIndex2 == 1)
                {
                    Reanimation reanimation2 = mApp.ReanimationGet(mSpecialHeadReanimID);
                    if (reanimation2 != null)
                    {
                        reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_face, AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED1);
                    }
                }
                else if (mHelmType == HelmType.Wallnut && helmDamageIndex2 == 2)
                {
                    Reanimation reanimation3 = mApp.ReanimationGet(mSpecialHeadReanimID);
                    if (reanimation3 != null)
                    {
                        reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_face, AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED2);
                    }
                }
                else if (mHelmType == HelmType.Tallnut && helmDamageIndex2 == 1)
                {
                    Reanimation reanimation4 = mApp.ReanimationGet(mSpecialHeadReanimID);
                    if (reanimation4 != null)
                    {
                        reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_idle, AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED1);
                    }
                }
                else if (mHelmType == HelmType.Tallnut && helmDamageIndex2 == 2)
                {
                    Reanimation reanimation5 = mApp.ReanimationGet(mSpecialHeadReanimID);
                    if (reanimation5 != null)
                    {
                        reanimation5.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_idle, AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED2);
                    }
                }
            }
            return aDamageRemaining;
        }

        public void TakeBodyDamage(int theDamage, uint theDamageFlags, bool isPyromaniac)
        {
            if (!TodCommon.TestBit(theDamageFlags, 3))
            {
                mJustGotShotCounter = 25;
            }
            if (TodCommon.TestBit(theDamageFlags, 2))
            {
                ApplyChill(false);
            }
            int num = mBodyHealth;
            int bodyDamageIndex = GetBodyDamageIndex();
            mBodyHealth -= theDamage;
            int bodyDamageIndex2 = GetBodyDamageIndex();
            if (mZombieType == ZombieType.Zamboni)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (!TodCommon.TestBit(theDamageFlags, 3))
                {
                    mApp.PlayFoley(FoleyType.ShieldHit);
                }
                if (TodCommon.TestBit(theDamageFlags, 5))
                {
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_1, AtlasResources.IMAGE_REANIM_ZOMBIE_ZAMBONI_1_DAMAGE2);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_2, AtlasResources.IMAGE_REANIM_ZOMBIE_ZAMBONI_2_DAMAGE2);
                    ZamboniDeath(theDamageFlags);
                }
                else if (mBodyHealth <= 0)
                {
                    ZamboniDeath(theDamageFlags);
                }
                else if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 1)
                {
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_1, AtlasResources.IMAGE_REANIM_ZOMBIE_ZAMBONI_1_DAMAGE1);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_2, AtlasResources.IMAGE_REANIM_ZOMBIE_ZAMBONI_2_DAMAGE1);
                }
                else if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 2)
                {
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_1, AtlasResources.IMAGE_REANIM_ZOMBIE_ZAMBONI_1_DAMAGE2);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_2, AtlasResources.IMAGE_REANIM_ZOMBIE_ZAMBONI_2_DAMAGE2);
                    AddAttachedParticle(27, 72, ParticleEffect.ZamboniSmoke);
                }
            }
            else if (mZombieType == ZombieType.Catapult)
            {
                Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
                if (TodCommon.TestBit(theDamageFlags, 5) || mBodyHealth <= 0)
                {
                    reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_siding, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_SIDING_DAMAGE);
                    CatapultDeath(theDamageFlags);
                }
                else if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 1)
                {
                    reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_siding, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_SIDING_DAMAGE);
                }
                else if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 2)
                {
                    AddAttachedParticle(47, 77, ParticleEffect.ZamboniSmoke);
                }
            }
            else if (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
            {
                Reanimation reanimation3 = mApp.ReanimationGet(mBodyReanimID);
                if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 1)
                {
                    reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_gargantua_body1, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_BODY1_2);
                    reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_gargantuar_outerarm_lower, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_OUTERARM_LOWER2);
                }
                else if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 2)
                {
                    reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_gargantua_body1, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_BODY1_3);
                    reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_gargantuar_outerleg_foot, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_FOOT2);
                    reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_gargantuar_outerarm_lower, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_OUTERARM_LOWER2);
                    if (mZombieType == ZombieType.RedeyeGargantuar)
                    {
                        reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_HEAD2_REDEYE);
                    }
                    else
                    {
                        reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_HEAD2);
                    }
                }
            }
            else if (mZombieType == ZombieType.Boss)
            {
                if (!TodCommon.TestBit(theDamageFlags, 3))
                {
                    mApp.PlayFoley(FoleyType.ShieldHit);
                }
                Reanimation reanimation4 = mApp.ReanimationGet(mBodyReanimID);
                if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 1)
                {
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_head, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_HEAD_DAMAGE1);
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_jaw, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_JAW_DAMAGE1);
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerarm_hand, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_OUTERARM_HAND_DAMAGE1);
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerarm_thumb2, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_OUTERARM_THUMB_DAMAGE1);
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_innerleg_foot, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_FOOT_DAMAGE1);
                }
                else if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 2)
                {
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_head, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_HEAD_DAMAGE2);
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_jaw, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_JAW_DAMAGE2);
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerarm_hand, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_OUTERARM_HAND_DAMAGE2);
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerarm_thumb2, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_OUTERARM_THUMB_DAMAGE2);
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_outerleg_foot, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_FOOT_DAMAGE2);
                    ApplyBossSmokeParticles(true);
                }
                if (num >= mBodyMaxHealth / GameConstants.BOSS_FLASH_HEALTH_FRACTION && mBodyHealth < mBodyMaxHealth / GameConstants.BOSS_FLASH_HEALTH_FRACTION)
                {
                    mApp.AddTodParticle(770f, 260f, 400000, ParticleEffect.BossExplosion);
                    mApp.PlayFoley(FoleyType.BossExplosionSmall);
                    ApplyBossSmokeParticles(true);
                }
                if (mBodyHealth <= 0)
                {
                    mBodyHealth = 1;
                }
            }
            else
            {
                UpdateDamageStates(theDamageFlags);
            }
            if (mBodyHealth <= 0)
            {
                mBodyHealth = 0;
                PlayDeathAnim(theDamageFlags);
                DropLoot();
                if (!isPyromaniac && mBoard.mPyromaniacKillZombiesOnly)
                {
                    mBoard.mPyromaniacKillZombiesOnly = false;
                }
            }
        }

        public void AttachShield()
        {
            string aTrackName = string.Empty;
            if (mShieldType == ShieldType.Door)
            {
                ShowDoorArms(true);
                ReanimShowPrefix("Zombie_outerarm_screendoor", GameConstants.RENDER_GROUP_OVER_SHIELD);
                aTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_screendoor;
            }
            else if (mShieldType == ShieldType.Newspaper)
            {
                ReanimShowPrefix("Zombie_paper_hands", GameConstants.RENDER_GROUP_OVER_SHIELD);
                aTrackName = GlobalMembersReanimIds.ReanimTrackId_zombie_paper_paper;
            }
            else if (mShieldType == ShieldType.Ladder)
            {
                ReanimShowPrefix("Zombie_outerarm", GameConstants.RENDER_GROUP_OVER_SHIELD);
                aTrackName = GlobalMembersReanimIds.ReanimTrackId_zombie_ladder_1;
            }
            else
            {
                Debug.ASSERT(false);
            }
            mApp.ReanimationGet(mBodyReanimID).AssignRenderGroupToTrack(aTrackName, GameConstants.RENDER_GROUP_SHIELD);
        }

        public void DetachShield()
        {
            if (mApp.ReanimationTryToGet(mBodyReanimID) == null)
            {
                mShieldType = ShieldType.None;
                mShieldHealth = 0;
                return;
            }
            if (mShieldType == ShieldType.Door)
            {
                ShowDoorArms(false);
            }
            else if (mShieldType == ShieldType.Newspaper)
            {
                ReanimShowPrefix("Zombie_paper_hands", 0);
            }
            else if (mShieldType == ShieldType.Ladder)
            {
                ReanimShowPrefix("Zombie_outerarm", 0);
                mZombiePhase = ZombiePhase.ZombieNormal;
                if (mIsEating)
                {
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_eat, ReanimLoopType.Loop, 20, 0f);
                }
                else
                {
                    StartWalkAnim(0);
                }
            }
            else
            {
                Debug.ASSERT(false);
            }
            mShieldType = ShieldType.None;
            mShieldHealth = 0;
            mHasShield = false;
        }

        public void UpdateReanim()//3update
        {
            Reanimation aBodyReanim = mApp.ReanimationTryToGet(mBodyReanimID);
            if (aBodyReanim == null || aBodyReanim.mDead)
            {
                return;
            }
            bool flag = false;
            if (mZombieType == ZombieType.Catapult)
            {
                flag = true;
                if (GetBodyDamageIndex() == 2 || mZombiePhase == ZombiePhase.ZombieDying)
                {
                    aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                    Image currentTrackImage = aBodyReanim.GetCurrentTrackImage(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_pole);
                    if (currentTrackImage == AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_POLE_WITHBALL && mSummonCounter != 0)
                    {
                        aBodyReanim.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_pole, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_POLE_DAMAGE_WITHBALL);
                    }
                    else
                    {
                        aBodyReanim.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_pole, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_POLE_DAMAGE);
                    }
                }
                else if (mSummonCounter == 0)
                {
                    aBodyReanim.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_pole, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_POLE);
                }
            }
            ZombieDrawPosition aDrawPos = default(ZombieDrawPosition);
            GetDrawPos(ref aDrawPos);
            float anOffsetX = aDrawPos.mImageOffsetX;
            float anOffsetY = aDrawPos.mImageOffsetY + aDrawPos.mBodyY - 28f;
            anOffsetX += 15f;
            anOffsetY += 20f;
            if ((mZombieType == ZombieType.Zamboni || mZombieType == ZombieType.Catapult) && mZombiePhase != ZombiePhase.ZombieBurned)
            {
                if (mZombiePhase == ZombiePhase.ZombieDying)
                {
                    float aShakeRange = TodCommon.TodAnimateCurveFloatTime(0.7f, 1f, aBodyReanim.mAnimTime, 0f, 1f, TodCurves.EaseOut);
                    anOffsetX += TodCommon.RandRangeFloat(-aShakeRange, aShakeRange);
                    anOffsetY += TodCommon.RandRangeFloat(-aShakeRange, aShakeRange);
                }
                else if (mBodyHealth < 200)
                {
                    anOffsetX += TodCommon.RandRangeFloat(-1f, 1f);
                    anOffsetY += TodCommon.RandRangeFloat(-1f, 1f);
                }
            }
            if (mZombieType == ZombieType.Football && mScaleZombie < 1f)
            {
                anOffsetY += 20f - mScaleZombie * 20f;
            }
            bool flag3 = false;
            if (IsWalkingBackwards())
            {
                flag3 = true;
            }
            if (mZombieType == ZombieType.Dancer || mZombieType == ZombieType.BackupDancer)
            {
                bool flag4 = false;
                if ((mZombiePhase == ZombiePhase.DancerDancingIn || mZombiePhase == ZombiePhase.DancerRaiseRight1 || mZombiePhase == ZombiePhase.DancerRaiseRight2) && !mIsEating)
                {
                    flag4 = true;
                }
                if (mMindControlled)
                {
                    flag4 = !flag4;
                }
                flag3 = flag4;
            }
            if (flag3)
            {
                anOffsetX += 90f * mScaleZombie;
            }
            Matrix aOverlayMatrix = Matrix.Identity;
            aOverlayMatrix.M11 = mScaleZombie;
            aOverlayMatrix.M22 = mScaleZombie;
            aOverlayMatrix.M41 = (anOffsetX + 30f - mScaleZombie * 30f) * Constants.S;
            aOverlayMatrix.M42 = (anOffsetY + 120f - mScaleZombie * 120f) * Constants.S;
            if (flag3)
            {
                aOverlayMatrix.M11 = -mScaleZombie;
            }
            if (aBodyReanim.mOverlayMatrix.mMatrix != aOverlayMatrix)
            {
                flag = true;
                aBodyReanim.mOverlayMatrix.mMatrix = aOverlayMatrix;
            }
            Reanimation reanimation2 = mApp.ReanimationTryToGet(mMoweredReanimID);
            if (reanimation2 != null)
            {
                reanimation2.Update();
                SexyTransform2D aTransform = default(SexyTransform2D);
                reanimation2.GetAttachmentOverlayMatrix(0, out aTransform);
                aTransform.mMatrix.M11 = aTransform.mMatrix.M11 * aBodyReanim.mOverlayMatrix.mMatrix.M11;
                aTransform.mMatrix.M21 = aTransform.mMatrix.M21 * aBodyReanim.mOverlayMatrix.mMatrix.M11;
                aTransform.mMatrix.M12 = aTransform.mMatrix.M12 * aBodyReanim.mOverlayMatrix.mMatrix.M22;
                aTransform.mMatrix.M22 = aTransform.mMatrix.M22 * aBodyReanim.mOverlayMatrix.mMatrix.M22;
                aTransform.mMatrix.M41 = aTransform.mMatrix.M41 * aBodyReanim.mOverlayMatrix.mMatrix.M11;
                aTransform.mMatrix.M42 = aTransform.mMatrix.M42 * aBodyReanim.mOverlayMatrix.mMatrix.M22;
                aTransform.mMatrix.M41 = aTransform.mMatrix.M41 + aBodyReanim.mOverlayMatrix.mMatrix.M22;
                aTransform.mMatrix.M42 = aTransform.mMatrix.M42 + aBodyReanim.mOverlayMatrix.mMatrix.M42;
                if (aBodyReanim.mOverlayMatrix != aTransform)
                {
                    flag = true;
                    aBodyReanim.mOverlayMatrix = aTransform;
                }
            }
            aBodyReanim.Update();
            if (flag)
            {
                aBodyReanim.PropogateColorToAttachments();
            }
        }

        public void GetTrackPosition(ref string theTrackName, ref float thePosX, ref float thePosY)
        {
            Reanimation reanimation_v = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation_v == null)
            {
                thePosX = mPosX;
                thePosY = mPosY;
                return;
            }
            int theTrackIndex = reanimation_v.FindTrackIndex(theTrackName);
            SexyTransform2D aSexyTransform2D = default(SexyTransform2D);
            reanimation_v.GetTrackTranslationMatrix(theTrackIndex, ref aSexyTransform2D);
            thePosX = aSexyTransform2D.mMatrix.M41 * Constants.IS + mPosX;
            thePosY = aSexyTransform2D.mMatrix.M42 * Constants.IS + mPosY;
        }

        public void LoadPlainZombieReanim()
        {
            mZombieAttackRect = new TRect(20, 0, 50, 115);
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            Zombie.SetupReanimLayers(reanimation, mZombieType);
            if (mBoard != null)
            {
                EnableMustache(mBoard.mMustacheMode);
                EnableFuture(mBoard.mFutureMode);
            }
            bool flag = false;
            if (mBoard != null && mBoard.mPlantRow[mRow] == PlantRowType.Pool)
            {
                flag = true;
            }
            else if (mZombieType == ZombieType.DuckyTube)
            {
                flag = true;
            }
            if (flag)
            {
                ReanimShowPrefix("zombie_duckytube", 0);
                ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_duckytube, true);
                ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, true);
                ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm3, true);
                SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_whitewater);
                SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_whitewater2);
            }
        }

        public void ShowDoorArms(bool theShow)
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation != null)
            {
                Zombie.SetupDoorArms(reanimation, theShow);
                if (!mHasArm)
                {
                    ReanimShowPrefix("Zombie_outerarm_lower", -1);
                    ReanimShowPrefix("Zombie_outerarm_hand", -1);
                }
            }
        }

        public void ReanimShowTrack(ref string theTrackName, int theRenderGroup)
        {
            mApp.ReanimationTryToGet(mBodyReanimID)?.AssignRenderGroupToTrack(theTrackName, theRenderGroup);
        }

        public void PlayZombieAppearSound()
        {
            if (mZombieType == ZombieType.DolphinRider)
            {
                mApp.PlayFoley(FoleyType.DolphinAppears);
                return;
            }
            if (mZombieType == ZombieType.Balloon)
            {
                mApp.PlayFoley(FoleyType.Ballooninflate);
                return;
            }
            if (mZombieType == ZombieType.Zamboni)
            {
                mApp.PlayFoley(FoleyType.Zamboni);
            }
        }

        public void StartMindControlled()
        {
            mApp.PlaySample(Resources.SOUND_MINDCONTROLLED);
            mMindControlled = true;
            mLastPortalX = -1;
            if (mZombieType == ZombieType.Dancer)
            {
                mBoard.GrantAchievement(AchievementId.DiscoIsUndead, true);
                for (int i = 0; i < GameConstants.NUM_BACKUP_DANCERS; i++)
                {
                    mFollowerZombieID[i] = null;
                }
                return;
            }
            if (mZombieType == ZombieType.BackupDancer)
            {
                Zombie zombie = mBoard.ZombieTryToGet(mRelatedZombieID);
                if (zombie != null)
                {
                    Zombie zombie2 = mBoard.ZombieGetID(this);
                    for (int j = 0; j < GameConstants.NUM_BACKUP_DANCERS; j++)
                    {
                        if (zombie.mFollowerZombieID[j] == zombie2)
                        {
                            zombie.mFollowerZombieID[j] = null;
                            break;
                        }
                    }
                }
                mRelatedZombieID = null;
                return;
            }
            Zombie zombie3 = mBoard.ZombieTryToGet(mRelatedZombieID);
            if (zombie3 != null)
            {
                zombie3.mRelatedZombieID = null;
                mRelatedZombieID = null;
            }
        }

        public bool IsFlying()
        {
            return mZombiePhase == ZombiePhase.BalloonFlying || mZombiePhase == ZombiePhase.BalloonPopping;
        }

        private void SetupReanimForLostHead()
        {
            ReanimShowPrefix("anim_head", -1);
            ReanimShowPrefix("anim_hair", -1);
            ReanimShowPrefix("anim_tongue", -1);
        }

        public void DropHead(uint theDamageFlags)
        {
            if (!CanLoseBodyParts())
            {
                return;
            }
            if (!mHasHead)
            {
                return;
            }
            if (mButteredCounter > 0)
            {
                mButteredCounter = 0;
                UpdateAnimSpeed();
            }
            mHasHead = false;
            SetupReanimForLostHead();
            if (TodCommon.TestBit(theDamageFlags, 4))
            {
                return;
            }
            if (mZombieType == ZombieType.PeaHead || mZombieType == ZombieType.WallnutHead || mZombieType == ZombieType.TallnutHead || mZombieType == ZombieType.JalapenoHead || mZombieType == ZombieType.GatlingHead || mZombieType == ZombieType.SquashHead)
            {
                DetachPlantHead();
                Reanimation reanimation = mApp.ReanimationGet(mSpecialHeadReanimID);
                if (reanimation != null)
                {
                    reanimation.ReanimationDie();
                }
                mSpecialHeadReanimID = null;
                return;
            }
            int aRenderOrder = mRenderOrder + 1;
            ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
            GetDrawPos(ref zombieDrawPosition);
            float aPosX = mPosX + zombieDrawPosition.mImageOffsetX + zombieDrawPosition.mHeadX + 11f;
            float aPosY = mPosY + zombieDrawPosition.mImageOffsetY + zombieDrawPosition.mHeadY + zombieDrawPosition.mBodyY + 21f;
            if (mBodyReanimID != null)
            {
                GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_head1, ref aPosX, ref aPosY);
            }
            ParticleEffect theEffect = ParticleEffect.ZombieHead;
            if (mZombiePhase == ZombiePhase.ZombieMowered)
            {
                theEffect = ParticleEffect.MoweredZombieHead;
                aPosX -= 40f;
                aPosY -= 50f;
            }
            else if (mInPool)
            {
                theEffect = ParticleEffect.ZombieHeadPool;
            }
            if (mZombieType == ZombieType.Dancer)
            {
                aRenderOrder = mRenderOrder - 1;
            }
            if (mZombieType == ZombieType.Newspaper)
            {
                theEffect = ParticleEffect.ZombieNewspaperHead;
            }
            else if (mZombieType == ZombieType.Pogo)
            {
                PogoBreak(theDamageFlags);
                theEffect = ParticleEffect.ZombiePogoHead;
            }
            else if (mZombieType == ZombieType.Balloon)
            {
                ReanimShowPrefix("anim_hat", -1);
                ReanimShowPrefix("hat", -1);
                theEffect = ParticleEffect.ZombieBalloonHead;
            }
            else if (mZombieType == ZombieType.Polevaulter)
            {
                DropPole();
            }
            else if (mZombieType == ZombieType.Flag)
            {
                DropFlag();
            }
            TodParticleSystem todParticleSystem = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, theEffect);
            OverrideParticleColor(todParticleSystem);
            OverrideParticleScale(todParticleSystem);
            if (todParticleSystem != null && mZombieType == ZombieType.Dancer)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_DISCO);
                todParticleSystem.OverrideScale(null, 1.2f);
                ReanimShowPrefix("Zombie_disco_glasses", -1);
                TodParticleSystem todParticleSystem2 = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, ParticleEffect.ZombieMustache);
                OverrideParticleColor(todParticleSystem2);
                OverrideParticleScale(todParticleSystem2);
                if (todParticleSystem2 != null)
                {
                    todParticleSystem2.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_DISCO_GLASSES);
                }
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.BackupDancer)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_DANCER_BACKUP_HEAD);
                todParticleSystem.OverrideScale(null, 1.2f);
                ReanimShowPrefix("Zombie_backup_stash", -1);
                todParticleSystem = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, ParticleEffect.ZombieMustache);
                OverrideParticleColor(todParticleSystem);
                OverrideParticleScale(todParticleSystem);
                if (todParticleSystem != null)
                {
                    todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_BACKUP_STASH);
                }
                ReanimShowPrefix("anim_head2", -1);
                todParticleSystem = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, ParticleEffect.ZombieMustache);
                OverrideParticleColor(todParticleSystem);
                OverrideParticleScale(todParticleSystem);
                if (todParticleSystem != null)
                {
                    todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_JAW);
                }
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.Bobsled)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEBOBSLEDHEAD);
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.Ladder)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIELADDERHEAD);
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.Imp)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEIMPHEAD);
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.Football)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEFOOTBALLHEAD);
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.Polevaulter)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEPOLEVAULTERHEAD);
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.Snorkel)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_SNORKLE_HEAD);
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.Digger)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEDIGGERHEAD);
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.DolphinRider)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEDOLPHINRIDERHEAD);
            }
            else if (todParticleSystem != null && mZombieType == ZombieType.Yeti)
            {
                todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEYETIHEAD);
            }
            Reanimation reanimation2 = mApp.ReanimationTryToGet(mBodyReanimID);
            if (mBoard.mMustacheMode && reanimation2.TrackExists(GlobalMembersReanimIds.ReanimTrackId_zombie_mustache))
            {
                ReanimShowPrefix("Zombie_mustache", -1);
                TodParticleSystem todParticleSystem3 = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, ParticleEffect.ZombieMustache);
                OverrideParticleColor(todParticleSystem3);
                OverrideParticleScale(todParticleSystem3);
                Image imageOverride = reanimation2.GetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_mustache);
                if (todParticleSystem3 != null && imageOverride != null)
                {
                    todParticleSystem3.OverrideImage(null, imageOverride);
                }
            }
            if (mBoard.mFutureMode)
            {
                Image imageOverride2 = reanimation2.GetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
                int num3 = -1;
                if (imageOverride2 != null && imageOverride2 == AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_SUNGLASSES1)
                {
                    num3 = 0;
                }
                else if (imageOverride2 != null && imageOverride2 == AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_SUNGLASSES2)
                {
                    num3 = 1;
                }
                else if (imageOverride2 != null && imageOverride2 == AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_SUNGLASSES3)
                {
                    num3 = 2;
                }
                else if (imageOverride2 != null && imageOverride2 == AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_SUNGLASSES4)
                {
                    num3 = 3;
                }
                if (num3 != -1)
                {
                    TodParticleSystem todParticleSystem4 = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, ParticleEffect.ZombieFutureGlasses);
                    OverrideParticleColor(todParticleSystem4);
                    OverrideParticleScale(todParticleSystem4);
                    if (todParticleSystem4 != null)
                    {
                        todParticleSystem4.OverrideFrame(null, num3);
                    }
                }
            }
            if (mBoard.mPinataMode && mZombiePhase != ZombiePhase.ZombieMowered)
            {
                TodParticleSystem aParticle = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, ParticleEffect.Pinata);
                OverrideParticleScale(aParticle);
            }
            mApp.PlayFoley(FoleyType.LimbsPop);
        }

        public bool CanTargetPlant(Plant thePlant, ZombieAttackType theAttackType)
        {
            if (mApp.IsWallnutBowlingLevel() && theAttackType != ZombieAttackType.Vault)
            {
                return false;
            }
            if (thePlant.NotOnGround())
            {
                return false;
            }
            if (thePlant.mSeedType == SeedType.Tanglekelp)
            {
                return false;
            }
            if (!mInPool && mBoard.IsPoolSquare(thePlant.mPlantCol, thePlant.mRow))
            {
                return false;
            }
            if (mZombiePhase == ZombiePhase.DiggerTunneling)
            {
                return thePlant.mSeedType == SeedType.Potatomine && thePlant.mState == PlantState.Notready;
            }
            if (thePlant.IsSpiky())
            {
                return mZombieType == ZombieType.Gargantuar
                    || mZombieType == ZombieType.RedeyeGargantuar
                    || mZombieType == ZombieType.Zamboni
                    || (mBoard.IsPoolSquare(thePlant.mPlantCol, thePlant.mRow) || mBoard.GetFlowerPotAt(thePlant.mPlantCol, thePlant.mRow) != null);
            }
            if (theAttackType == ZombieAttackType.DriveOver)
            {
                if (thePlant.mSeedType == SeedType.Cherrybomb
                    || thePlant.mSeedType == SeedType.Jalapeno
                    || thePlant.mSeedType == SeedType.Blover
                    || thePlant.mSeedType == SeedType.Squash)
                {
                    return false;
                }
                if (thePlant.mSeedType == SeedType.Doomshroom || thePlant.mSeedType == SeedType.Iceshroom)
                {
                    return thePlant.mIsAsleep;
                }
            }
            if (mZombiePhase == ZombiePhase.LadderCarrying || mZombiePhase == ZombiePhase.LadderPlacing)
            {
                bool flag = false;
                if (thePlant.mSeedType == SeedType.Wallnut
                    || thePlant.mSeedType == SeedType.Tallnut
                    || thePlant.mSeedType == SeedType.Pumpkinshell)
                {
                    flag = true;
                }
                if (mBoard.GetLadderAt(thePlant.mPlantCol, thePlant.mRow) != null)
                {
                    flag = false;
                }
                if (theAttackType == ZombieAttackType.Chew && flag)
                {
                    return false;
                }
                if (theAttackType == ZombieAttackType.Ladder && !flag)
                {
                    return false;
                }
            }
            if (theAttackType == ZombieAttackType.Chew)
            {
                Plant topPlantAt_v = mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, TopPlant.EatingOrder);
                if (topPlantAt_v != thePlant && topPlantAt_v != null && CanTargetPlant(topPlantAt_v, theAttackType))
                {
                    return false;
                }
            }
            if (theAttackType == ZombieAttackType.Vault)
            {
                Plant topPlantAt2_v = mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, TopPlant.OnlyNormalPosition);
                if (topPlantAt2_v != thePlant && topPlantAt2_v != null && CanTargetPlant(topPlantAt2_v, theAttackType))
                {
                    return false;
                }
            }
            return true;
        }

        public void UpdateZombieCatapult()//3update
        {
            if (mZombiePhase == ZombiePhase.ZombieNormal)
            {
                if (mPosX <= 650 + Constants.BOARD_EXTRA_ROOM && FindCatapultTarget() != null && mSummonCounter > 0)
                {
                    mZombiePhase = ZombiePhase.CatapultLaunching;
                    mPhaseCounter = 300;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_shoot, ReanimLoopType.PlayOnceAndHold, 0, 24f);
                    return;
                }
            }
            else if (mZombiePhase == ZombiePhase.CatapultLaunching)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.ShouldTriggerTimedEvent(0.545f))
                {
                    Plant thePlant = FindCatapultTarget();
                    ZombieCatapultFire(thePlant);
                }
                if (reanimation.mLoopCount > 0)
                {
                    mSummonCounter--;
                    if (mSummonCounter == 4)
                    {
                        ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_basketball, -1);
                    }
                    else if (mSummonCounter == 3)
                    {
                        ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_basketball2, -1);
                    }
                    else if (mSummonCounter == 2)
                    {
                        ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_basketball3, -1);
                    }
                    else if (mSummonCounter == 1)
                    {
                        ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_basketball4, -1);
                    }
                    if (mSummonCounter == 0)
                    {
                        PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, 20, 6f);
                        mZombiePhase = ZombiePhase.ZombieNormal;
                        return;
                    }
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 20, 12f);
                    mZombiePhase = ZombiePhase.CatapultReloading;
                    return;
                }
            }
            else if (mZombiePhase == ZombiePhase.CatapultReloading && mPhaseCounter == 0)
            {
                Plant plant = FindCatapultTarget();
                if (plant != null)
                {
                    mZombiePhase = ZombiePhase.CatapultLaunching;
                    mPhaseCounter = 300;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_shoot, ReanimLoopType.PlayOnceAndHold, 20, 24f);
                    return;
                }
                PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, 20, 6f);
                mZombiePhase = ZombiePhase.ZombieNormal;
            }
        }

        public Plant FindCatapultTarget()
        {
            Plant result_v = null;
            for (int i = 0; i < mBoard.mPlants.Count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead && mRow == aPlant.mRow && mX >= aPlant.mX + 100 && !aPlant.NotOnGround() && !aPlant.IsSpiky() && (result_v == null || aPlant.mPlantCol < result_v.mPlantCol))
                {
                    result_v = mBoard.GetTopPlantAt(aPlant.mPlantCol, aPlant.mRow, TopPlant.CatapultOrder);
                }
            }
            return result_v;
        }

        public void ZombieCatapultFire(Plant thePlant)
        {
            float aOriginX = mPosX + 113f;
            float aOriginY = mPosY - 44f;
            int aTargetX = thePlant?.mX ?? (int)mPosX - 300;
            int aTargetY = thePlant?.mY ?? 0;
            mApp.PlayFoley(FoleyType.Basketball);
            Projectile projectile_v = mBoard.AddProjectile((int)aOriginX, (int)aOriginY, mRenderOrder, mRow, ProjectileType.Basketball);
            float aRangeX = aOriginX - aTargetX - 20f;
            float aRangeY = aTargetY - aOriginY;
            if (aRangeX < 40f)
            {
                aRangeX = 40f;
            }
            projectile_v.mMotionType = ProjectileMotion.Lobbed;
            float k_v = 120f;
            projectile_v.mVelX = -aRangeX / k_v;
            projectile_v.mVelY = 0f;
            projectile_v.mVelZ = -7f + aRangeY / k_v;
            projectile_v.mAccZ = 0.115f;
        }

        public void UpdateClimbingLadder()//1update
        {
            float aDistOffGround = mAltitude;
            if (mOnHighGround)
            {
                aDistOffGround -= Constants.HIGH_GROUND_HEIGHT;
            }
            int theGridX = mBoard.PixelToGridXKeepOnBoard((int)(mX + 5 + aDistOffGround * 0.5f), mY);
            if (mBoard.GetLadderAt(theGridX, mRow) == null)
            {
                mZombieHeight = ZombieHeight.Falling;
                return;
            }
            mAltitude += 0.8f;
            if (mVelX < 0.5f)
            {
                mPosX -= 0.5f;
            }
            float aTargetHeight = 90f;
            if (mOnHighGround)
            {
                aTargetHeight += Constants.HIGH_GROUND_HEIGHT;
            }
            if (mAltitude >= aTargetHeight)
            {
                mZombieHeight = ZombieHeight.Falling;
            }
        }

        public void UpdateZombieGargantuar()//3update
        {
            Plant plant;
            if (mZombiePhase == ZombiePhase.GargantuarSmashing)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.ShouldTriggerTimedEvent(0.64f))
                {
                    plant = FindPlantTarget(ZombieAttackType.Chew);
                    if (plant != null && plant.mSeedType == SeedType.Spikerock)
                    {
                        TakeDamage(20, 32U, false);
                        plant.SpikeRockTakeDamage();
                        if (plant.mPlantHealth <= 0)
                        {
                            SquishAllInSquare(plant.mPlantCol, plant.mRow, ZombieAttackType.Chew);
                        }
                    }
                    else if (plant != null)
                    {
                        SquishAllInSquare(plant.mPlantCol, plant.mRow, ZombieAttackType.Chew);
                    }
                    if (mApp.IsScaryPotterLevel())
                    {
                        int theGridX = mBoard.PixelToGridX((int)mPosX, (int)mPosY);
                        GridItem scaryPotAt = mBoard.GetScaryPotAt(theGridX, mRow);
                        if (scaryPotAt != null)
                        {
                            mBoard.mChallenge.ScaryPotterOpenPot(scaryPotAt);
                        }
                    }
                    if (mApp.IsIZombieLevel())
                    {
                        GridItem gridItem = mBoard.mChallenge.IZombieGetBrainTarget(this);
                        if (gridItem != null)
                        {
                            mBoard.mChallenge.IZombieSquishBrain(gridItem);
                        }
                    }
                    mApp.PlayFoley(FoleyType.Thump);
                    mApp.Vibrate();
                    mBoard.ShakeBoard(0, 3);
                }
                if (reanimation.mLoopCount > 0)
                {
                    mZombiePhase = ZombiePhase.ZombieNormal;
                    StartWalkAnim(20);
                }
                return;
            }
            float aThrowingDistance = mPosX - 460f;
            if (mZombiePhase == ZombiePhase.GargantuarThrowing)
            {
                Reanimation aBodyReanim = mApp.ReanimationGet(mBodyReanimID);
                if (aBodyReanim.ShouldTriggerTimedEvent(0.74f))
                {
                    mHasObject = false;
                    ReanimShowPrefix("Zombie_imp", -1);
                    ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_gargantuar_whiterope, -1);
                    mApp.PlayFoley(FoleyType.Swing);
                    Zombie aZombieImp = mBoard.AddZombie(ZombieType.Imp, mFromWave);
                    if (aZombieImp == null)
                    {
                        return;
                    }
                    float aMinThrowDistance = 40f;
                    if (mBoard.StageHasRoof())
                    {
                        aThrowingDistance -= 180f;
                        aMinThrowDistance = -140f;
                    }
                    if (aThrowingDistance < aMinThrowDistance)
                    {
                        aThrowingDistance = aMinThrowDistance;
                    }
                    else if (aThrowingDistance > 140f)
                    {
                        aThrowingDistance -= TodCommon.RandRangeFloat(0f, 100f);
                    }
                    aZombieImp.mPosX = mPosX - 133f;
                    aZombieImp.mPosY = GetPosYBasedOnRow(mRow);
                    aZombieImp.SetRow(mRow);
                    aZombieImp.mVariant = false;
                    aZombieImp.mRenderOrder = mRenderOrder + 1;
                    aZombieImp.mZombiePhase = ZombiePhase.ImpGettingThrown;
                    aZombieImp.mAltitude = 88f;
                    aZombieImp.mVelX = 3f;
                    aZombieImp.mChilledCounter = mChilledCounter;
                    aZombieImp.mVelZ = 0.5f * (aThrowingDistance / aZombieImp.mVelX) * GameConstants.THOWN_ZOMBIE_GRAVITY;
                    aZombieImp.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_thrown, ReanimLoopType.PlayOnceAndHold, 0, 18f);
                    aZombieImp.UpdateReanim();
                    mApp.PlayFoley(FoleyType.Imp);
                }
                if (aBodyReanim.mLoopCount > 0)
                {
                    mZombiePhase = ZombiePhase.ZombieNormal;
                    StartWalkAnim(20);
                }
                return;
            }
            if (IsImmobilizied() || !mHasHead)
            {
                return;
            }
            if (mHasObject && mBodyHealth < mBodyMaxHealth / 2 && aThrowingDistance > 40f)
            {
                mZombiePhase = ZombiePhase.GargantuarThrowing;
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_throw, ReanimLoopType.PlayOnceAndHold, 20, 24f);
                return;
            }
            bool flag = false;
            plant = FindPlantTarget(ZombieAttackType.Chew);
            if (plant != null)
            {
                flag = true;
            }
            else if (mApp.IsScaryPotterLevel())
            {
                int theGridX2 = mBoard.PixelToGridX((int)mPosX, (int)mPosY);
                if (mBoard.GetScaryPotAt(theGridX2, mRow) != null)
                {
                    flag = true;
                }
            }
            else if (mApp.IsIZombieLevel() && mBoard.mChallenge.IZombieGetBrainTarget(this) != null)
            {
                flag = true;
            }
            if (flag)
            {
                mZombiePhase = ZombiePhase.GargantuarSmashing;
                mApp.PlayFoley(FoleyType.Lowgroan);
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_smash, ReanimLoopType.PlayOnceAndHold, 20, 16f);
            }
        }

        public int GetBodyDamageIndex()
        {
            if (mZombieType == ZombieType.Boss)
            {
                if (mBodyHealth < mBodyMaxHealth / 2)
                {
                    return 2;
                }
                if (mBodyHealth < mBodyMaxHealth * 4 / 5)
                {
                    return 1;
                }
                return 0;
            }
            else
            {
                if (mBodyHealth < mBodyMaxHealth / 3)
                {
                    return 2;
                }
                if (mBodyHealth < mBodyMaxHealth * 2 / 3)
                {
                    return 1;
                }
                return 0;
            }
        }

        public void ApplyBurn()
        {
            if (mDead || mZombiePhase == ZombiePhase.ZombieBurned)
            {
                return;
            }
            if (mBodyHealth >= 1800 || mZombieType == ZombieType.Boss)
            {
                TakeDamage(1800, 18U, true);
                return;
            }
            if (mZombieType == ZombieType.SquashHead && !mHasHead)
            {
                mApp.RemoveReanimation(ref mSpecialHeadReanimID);
                mSpecialHeadReanimID = null;
            }
            if (mIceTrapCounter > 0)
            {
                RemoveIceTrap();
            }
            if (mButteredCounter > 0)
            {
                mButteredCounter = 0;
            }
            GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.ZamboniSmoke, null);
            BungeeDropPlant();
            if (mZombiePhase == ZombiePhase.ZombieDying || mZombiePhase == ZombiePhase.PolevaulterInVault || mZombiePhase == ZombiePhase.ImpGettingThrown || mZombiePhase == ZombiePhase.RisingFromGrave || mZombiePhase == ZombiePhase.DancerRising || mZombiePhase == ZombiePhase.DolphinIntoPool || mZombiePhase == ZombiePhase.DolphinInJump || mZombiePhase == ZombiePhase.DolphinRiding || mZombiePhase == ZombiePhase.SnorkelIntoPool || mZombiePhase == ZombiePhase.DiggerTunneling || mZombiePhase == ZombiePhase.DiggerTunnelingPauseWithoutAxe || mZombiePhase == ZombiePhase.DiggerRising || mZombiePhase == ZombiePhase.DiggerRiseWithoutAxe || mZombiePhase == ZombiePhase.ZombieMowered || mInPool)
            {
                DieWithLoot();
            }
            else if (mZombieType == ZombieType.Bungee || mZombieType == ZombieType.Yeti || mZombieType == ZombieType.PeaHead || mZombieType == ZombieType.WallnutHead || mZombieType == ZombieType.JalapenoHead || mZombieType == ZombieType.GatlingHead || mZombieType == ZombieType.SquashHead || mZombieType == ZombieType.TallnutHead || IsBobsledTeamWithSled() || IsFlying() || !mHasHead)
            {
                SetAnimRate(0f);
                Reanimation reanimation = mApp.ReanimationTryToGet(mSpecialHeadReanimID);
                if (reanimation != null)
                {
                    reanimation.mAnimRate = 0f;
                    reanimation.mColorOverride = Color.Black;
                    reanimation.mEnableExtraAdditiveDraw = false;
                    reanimation.mEnableExtraOverlayDraw = false;
                }
                mZombiePhase = ZombiePhase.ZombieBurned;
                mPhaseCounter = 300;
                mJustGotShotCounter = 0;
                DropLoot();
                if (mZombieType == ZombieType.Bungee)
                {
                    mZombieFade = 50;
                }
                if (mZombieType == ZombieType.Balloon)
                {
                    Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
                    ReanimatorTrackInstance trackInstanceByName = reanimation2.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_hat);
                    Reanimation reanimation3 = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName.mAttachmentID);
                    if (reanimation3 != null)
                    {
                        reanimation3.mAnimRate = 0f;
                    }
                    mZombieFade = 50;
                }
            }
            else
            {
                ReanimationType theReanimationType = ReanimationType.ZombieCharred;
                float aCharredPosX = mPosX + 22f;
                float aCharredPosY = mPosY - 10f;
                if (mZombieType == ZombieType.Balloon)
                {
                    aCharredPosY += 31f;
                }
                if (mZombieType == ZombieType.Imp)
                {
                    aCharredPosX -= 6f;
                    theReanimationType = ReanimationType.ZombieCharredImp;
                }
                if (mZombieType == ZombieType.Digger)
                {
                    if (IsWalkingBackwards())
                    {
                        aCharredPosX += 14f;
                    }
                    theReanimationType = ReanimationType.ZombieCharredDigger;
                }
                if (mZombieType == ZombieType.Zamboni)
                {
                    aCharredPosX += 61f;
                    aCharredPosY += -16f;
                    theReanimationType = ReanimationType.ZombieCharredZamboni;
                }
                if (mZombieType == ZombieType.Catapult)
                {
                    aCharredPosX += -36f;
                    aCharredPosY += -20f;
                    theReanimationType = ReanimationType.ZombieCharredCatapult;
                }
                if (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
                {
                    aCharredPosX += -15f;
                    aCharredPosY += -10f;
                    theReanimationType = ReanimationType.ZombieCharredGargantuar;
                }
                Reanimation reanimation4 = mApp.AddReanimation(aCharredPosX, aCharredPosY, mRenderOrder, theReanimationType);
                reanimation4.mAnimRate *= TodCommon.RandRangeFloat(0.9f, 1.1f);
                if (mZombiePhase == ZombiePhase.DiggerWalkingWithoutAxe)
                {
                    reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_crumble_noaxe);
                }
                else if (mZombieType == ZombieType.Digger)
                {
                    reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_crumble);
                }
                else if ((mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar) && !mHasObject)
                {
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_impblink, AtlasResources.IMAGE_BLANK);
                    reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_imphead, AtlasResources.IMAGE_BLANK);
                }
                if (mScaleZombie != 1f)
                {
                    reanimation4.mOverlayMatrix.mMatrix.M11 = mScaleZombie;
                    reanimation4.mOverlayMatrix.mMatrix.M22 = mScaleZombie;
                    Reanimation reanimation5 = reanimation4;
                    reanimation5.mOverlayMatrix.mMatrix.M41 = reanimation5.mOverlayMatrix.mMatrix.M41 + (20f - mScaleZombie * 20f) * Constants.S;
                    Reanimation reanimation6 = reanimation4;
                    reanimation6.mOverlayMatrix.mMatrix.M42 = reanimation6.mOverlayMatrix.mMatrix.M42 + (120f - mScaleZombie * 120f) * Constants.S;
                    reanimation4.OverrideScale(mScaleZombie, mScaleZombie);
                }
                if (IsWalkingBackwards())
                {
                    reanimation4.OverrideScale(-mScaleZombie, mScaleZombie);
                    Reanimation reanimation7 = reanimation4;
                    reanimation7.mOverlayMatrix.mMatrix.M41 = reanimation7.mOverlayMatrix.mMatrix.M41 + 60f * mScaleZombie * Constants.S;
                }
                DieWithLoot();
            }
            if (mZombieType == ZombieType.Bobsled)
            {
                BobsledBurn();
            }
        }

        public void UpdateBurn()//3update
        {
            //mPhaseCounter -= 3;
            mPhaseCounter--;
            if (mPhaseCounter == 0)
            {
                DieWithLoot();
            }
        }

        public bool ZombieNotWalking()
        {
            if (mIsEating || IsImmobilizied())
            {
                return true;
            }
            if (mZombiePhase == ZombiePhase.JackInTheBoxPopping || mZombiePhase == ZombiePhase.NewspaperMaddening || mZombiePhase == ZombiePhase.GargantuarThrowing || mZombiePhase == ZombiePhase.GargantuarSmashing || mZombiePhase == ZombiePhase.CatapultLaunching || mZombiePhase == ZombiePhase.CatapultReloading || mZombiePhase == ZombiePhase.DiggerRising || mZombiePhase == ZombiePhase.DiggerTunnelingPauseWithoutAxe || mZombiePhase == ZombiePhase.DiggerRiseWithoutAxe || mZombiePhase == ZombiePhase.DiggerStunned || mZombiePhase == ZombiePhase.DancerSnappingFingers || mZombiePhase == ZombiePhase.DancerSnappingFingersWithLight || mZombiePhase == ZombiePhase.DancerSnappingFingersHold || mZombiePhase == ZombiePhase.DancerRising || mZombiePhase == ZombiePhase.ImpGettingThrown || mZombiePhase == ZombiePhase.ImpLanding || mZombiePhase == ZombiePhase.LadderPlacing || mZombieHeight == ZombieHeight.InToChimney || mZombieHeight == ZombieHeight.GettingBungeeDropped || mZombieHeight == ZombieHeight.Zombiquarium || mZombieType == ZombieType.Bungee || mZombieType == ZombieType.Boss)
            {
                return true;
            }
            if (mZombiePhase == ZombiePhase.DancerRaiseLeft1 || mZombiePhase == ZombiePhase.DancerWalkToRaise || mZombiePhase == ZombiePhase.DancerRaiseRight1 || mZombiePhase == ZombiePhase.DancerRaiseLeft2 || mZombiePhase == ZombiePhase.DancerRaiseRight2)
            {
                return true;
            }
            if (mZombieType == ZombieType.Dancer || mZombieType == ZombieType.BackupDancer)
            {
                Zombie zombie;
                if (mZombieType == ZombieType.Dancer)
                {
                    zombie = this;
                }
                else
                {
                    zombie = mBoard.ZombieTryToGet(mRelatedZombieID);
                }
                if (zombie == null)
                {
                    return false;
                }
                if (zombie.IsImmobilizied() || zombie.mIsEating)
                {
                    return true;
                }
                for (int i = 0; i < GameConstants.NUM_BACKUP_DANCERS; i++)
                {
                    Zombie zombie2 = mBoard.ZombieTryToGet(zombie.mFollowerZombieID[i]);
                    if (zombie2 != null && (zombie2.IsImmobilizied() || zombie2.mIsEating))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Zombie FindZombieTarget()
        {
            if (mZombiePhase == ZombiePhase.DiggerTunneling)
            {
                return null;
            }
            TRect aAttackRect = GetZombieAttackRect();
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie aZombie = mBoard.mZombies[i];
                if (!aZombie.mDead && mMindControlled != aZombie.mMindControlled && !aZombie.IsFlying() && aZombie.mZombiePhase != ZombiePhase.DiggerTunneling && aZombie.mZombiePhase != ZombiePhase.BungeeDiving && aZombie.mZombiePhase != ZombiePhase.BungeeDivingScreaming && aZombie.mZombiePhase != ZombiePhase.BungeeRising && aZombie.mZombieHeight != ZombieHeight.GettingBungeeDropped && !aZombie.IsDeadOrDying() && aZombie.mRow == mRow)
                {
                    TRect aZombieRect = aZombie.GetZombieRect();
                    int rectOverlap = GameConstants.GetRectOverlap(aAttackRect, aZombieRect);
                    if (rectOverlap >= 20 || (rectOverlap >= 0 && aZombie.mIsEating))
                    {
                        return aZombie;
                    }
                }
            }
            return null;
        }

        public void PlayZombieReanim(ref string theTrackName, ReanimLoopType theLoopType, byte theBlendTime, float theAnimRate)
        {
            lastPlayedReanimName = theTrackName;
            lastPlayedReanimLoopType = theLoopType;
            lastPlayedReanimBlendTime = theBlendTime;
            lastPlayedReanimAnimRate = theAnimRate;
            Reanimation aReanim = mApp.ReanimationTryToGet(mBodyReanimID);
            if (aReanim == null)
            {
                return;
            }
            aReanim.PlayReanim(theTrackName, theLoopType, theBlendTime, theAnimRate);
            if (theAnimRate != 0f)
            {
                mOrginalAnimRate = theAnimRate;
            }
            UpdateAnimSpeed();
        }

        public void UpdateZombieBackupDancer()//1update
        {
            if (mIsEating)
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.DancerRising)
            {
                mAltitude = TodCommon.TodAnimateCurve(150, 0, mPhaseCounter, Constants.ZOMBIE_BACKUP_DANCER_RISE_HEIGHT, 0, TodCurves.Linear);
                mUsesClipping = (mAltitude < 0f);
                if (mPhaseCounter != 0)
                {
                    return;
                }
                if (IsOnHighGround())
                {
                    mAltitude = Constants.HIGH_GROUND_HEIGHT;
                }
            }
            ZombiePhase aDancerPhase = GetDancerPhase();
            if (aDancerPhase == mZombiePhase)
            {
                return;
            }
            else if (aDancerPhase == ZombiePhase.DancerDancingLeft)
            {
                mZombiePhase = aDancerPhase;
                PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, 10, 0f);
                return;
            }
            else if (aDancerPhase == ZombiePhase.DancerWalkToRaise)
            {
                mZombiePhase = aDancerPhase;
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.Loop, 10, 18f);
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                reanimation.mAnimTime = 0.6f;
                return;
            }
            else if (aDancerPhase == ZombiePhase.DancerRaiseLeft1 || aDancerPhase == ZombiePhase.DancerRaiseRight1 || aDancerPhase == ZombiePhase.DancerRaiseLeft2 || aDancerPhase == ZombiePhase.DancerRaiseRight2)
            {
                mZombiePhase = aDancerPhase;
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.Loop, 10, 18f);
            }
        }

        public ZombiePhase GetDancerPhase()
        {
            int dancerFrame = GetDancerFrame();
            if (dancerFrame <= 11)
            {
                return ZombiePhase.DancerDancingLeft;
            }
            if (dancerFrame <= 12)
            {
                return ZombiePhase.DancerWalkToRaise;
            }
            if (dancerFrame <= 15)
            {
                return ZombiePhase.DancerRaiseRight1;
            }
            if (dancerFrame <= 18)
            {
                return ZombiePhase.DancerRaiseLeft1;
            }
            if (dancerFrame <= 21)
            {
                return ZombiePhase.DancerRaiseRight2;
            }
            return ZombiePhase.DancerRaiseLeft2;
        }

        public bool IsMovingAtChilledSpeed()
        {
            if (mChilledCounter > 0)
            {
                return true;
            }
            if (mZombieType == ZombieType.Dancer || mZombieType == ZombieType.BackupDancer)
            {
                Zombie zombie;
                if (mZombieType == ZombieType.Dancer)
                {
                    zombie = this;
                }
                else
                {
                    zombie = mBoard.ZombieTryToGet(mRelatedZombieID);
                }
                if (zombie == null)
                {
                    return false;
                }
                if (zombie.mChilledCounter > 0)
                {
                    return true;
                }
                for (int i = 0; i < GameConstants.NUM_BACKUP_DANCERS; i++)
                {
                    Zombie zombie2 = mBoard.ZombieTryToGet(zombie.mFollowerZombieID[i]);
                    if (zombie2 != null && zombie2.mChilledCounter > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void StartWalkAnim(byte theBlendTime)
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            PickRandomSpeed();
            if (mZombiePhase == ZombiePhase.LadderCarrying)
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_ladderwalk, ReanimLoopType.Loop, theBlendTime, 0f);
                return;
            }
            if (mZombiePhase == ZombiePhase.NewspaperMad)
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_walk_nopaper, ReanimLoopType.Loop, theBlendTime, 0f);
                return;
            }
            if (mInPool
                && mZombieHeight != ZombieHeight.InToPool
                && mZombieHeight != ZombieHeight.OutOfPool
                && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_swim))
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_swim, ReanimLoopType.Loop, theBlendTime, 0f);
                return;
            }
            if ((mZombieType == ZombieType.Normal
                || mZombieType == ZombieType.TrafficCone
                || mZombieType == ZombieType.Pail) && mBoard.mDanceMode)
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_dance, ReanimLoopType.Loop, theBlendTime, 0f);
                return;
            }
            int num = RandomNumbers.NextNumber(2);
            if (IsZombotany())
            {
                num = 0;
            }
            if (mZombieType == ZombieType.Flag)
            {
                num = 0;
            }
            if (num == 0 && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_walk2))
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_walk2, ReanimLoopType.Loop, theBlendTime, 0f);
                return;
            }
            if (reanimation.TrackExists(Reanimation.ReanimTrackId_anim_walk))
            {
                PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.Loop, theBlendTime, 0f);
            }
        }

        public Reanimation AddAttachedReanim(int thePosX, int thePosY, ReanimationType theReanimType)
        {
            if (mDead)
            {
                return null;
            }
            Reanimation reanimation_v = mApp.AddReanimation(mX + thePosX, mY + thePosY, 0, theReanimType);
            if (reanimation_v != null)
            {
                GlobalMembersAttachment.AttachReanim(ref mAttachmentID, reanimation_v, thePosX * Constants.S, thePosY * Constants.S);
            }
            return reanimation_v;
        }

        public void DragUnder()
        {
            mZombieHeight = ZombieHeight.DraggedUnder;
            StopEating();
            ReanimReenableClipping();
        }

        public static void SetupDoorArms(Reanimation aReanim, bool theShow)
        {
            int theRenderGroup_v = 0;
            int theRenderGroup2_v = -1;
            if (theShow)
            {
                theRenderGroup_v = -1;
                theRenderGroup2_v = 0;
            }
            aReanim.AssignRenderGroupToPrefix("Zombie_outerarm_hand", theRenderGroup_v);
            aReanim.AssignRenderGroupToPrefix("Zombie_outerarm_lower", theRenderGroup_v);
            aReanim.AssignRenderGroupToPrefix("Zombie_outerarm_upper", theRenderGroup_v);
            aReanim.AssignRenderGroupToPrefix("anim_innerarm", theRenderGroup_v);
            aReanim.AssignRenderGroupToPrefix("Zombie_outerarm_screendoor", theRenderGroup2_v);
            aReanim.AssignRenderGroupToPrefix("Zombie_innerarm_screendoor", theRenderGroup2_v);
            aReanim.AssignRenderGroupToPrefix("Zombie_innerarm_screendoor_hand", theRenderGroup2_v);
        }

        public static void SetupReanimLayers(Reanimation aReanim, ZombieType theZombieType)
        {
            aReanim.AssignRenderGroupToPrefix("anim_cone", -1);
            aReanim.AssignRenderGroupToPrefix("anim_bucket", -1);
            aReanim.AssignRenderGroupToPrefix("anim_screendoor", -1);
            aReanim.AssignRenderGroupToPrefix("Zombie_flaghand", -1);
            aReanim.AssignRenderGroupToPrefix("Zombie_duckytube", -1);
            aReanim.AssignRenderGroupToPrefix("anim_tongue", -1);
            aReanim.AssignRenderGroupToPrefix("Zombie_mustache", -1);
            Zombie.SetupDoorArms(aReanim, false);
            if (theZombieType == ZombieType.TrafficCone)
            {
                aReanim.AssignRenderGroupToPrefix("anim_cone", 0);
                aReanim.AssignRenderGroupToPrefix("anim_hair", -1);
                return;
            }
            if (theZombieType == ZombieType.Pail)
            {
                aReanim.AssignRenderGroupToPrefix("anim_bucket", 0);
                aReanim.AssignRenderGroupToPrefix("anim_hair", -1);
                return;
            }
            if (theZombieType == ZombieType.Door)
            {
                Zombie.SetupDoorArms(aReanim, true);
                return;
            }
            if (theZombieType == ZombieType.Newspaper)
            {
                aReanim.AssignRenderGroupToPrefix("Zombie_paper_paper", -1);
                return;
            }
            if (theZombieType == ZombieType.Flag)
            {
                aReanim.AssignRenderGroupToPrefix("anim_innerarm", -1);
                aReanim.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand, 0);
                aReanim.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm_screendoor, 0);
                return;
            }
            if (theZombieType == ZombieType.DuckyTube)
            {
                aReanim.AssignRenderGroupToPrefix("Zombie_duckytube", 0);
            }
        }

        public bool IsOnBoard()
        {
            if (mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE || mFromWave == GameConstants.ZOMBIE_WAVE_UI)
            {
                return false;
            }
            Debug.ASSERT(mBoard != null);
            return true;
        }

        public void DrawButter(Graphics g, ref ZombieDrawPosition theDrawPos)
        {
            float aOffsetX = mPosX + theDrawPos.mImageOffsetX + theDrawPos.mHeadX + 11f;
            float aOffsetY = mPosY + theDrawPos.mImageOffsetY + theDrawPos.mHeadY + theDrawPos.mBodyY + 21f;
            float aScale = 1f;
            if (mBodyReanimID != null)
            {
                GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_head1, ref aOffsetX, ref aOffsetY);
            }
            aOffsetX += -mPosX;
            aOffsetY += -mPosY;
            switch (mZombieType)
            {
            case ZombieType.SquashHead:
                aOffsetX += 6f;
                aOffsetY -= 9f;
                break;
            case ZombieType.WallnutHead:
                aOffsetX -= 10f;
                if (mInPool && mIsEating)
                {
                    aOffsetX -= 5f;
                    aOffsetY += 10f;
                }
                break;
            case ZombieType.TallnutHead:
                aOffsetX -= 30f;
                aOffsetY -= 30f;
                if (mInPool && mIsEating)
                {
                    aOffsetY += 10f;
                }
                break;
            case ZombieType.GatlingHead:
                aOffsetY -= 10f;
                break;
            }
            TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_REANIM_CORNPULT_BUTTER_SPLAT, aOffsetX * Constants.S + 0f, aOffsetY * Constants.S - 6f, aScale, aScale);
        }

        public bool IsImmobilizied()
        {
            return mIceTrapCounter > 0 || mButteredCounter > 0;
        }

        public void ApplyButter()
        {
            if (!mHasHead)
            {
                return;
            }
            if (!CanBeFrozen())
            {
                return;
            }
            if (mZombieType == ZombieType.Zamboni
                || mZombieType == ZombieType.Boss
                || IsTangleKelpTarget()
                || IsBobsledTeamWithSled()
                || IsFlying())
            {
                return;
            }
            mButteredCounter = 400;
            Zombie zombie = mBoard.ZombieTryToGet(mRelatedZombieID);
            if (zombie != null)
            {
                zombie.mRelatedZombieID = null;
                mRelatedZombieID = null;
            }
            switch (mZombieType)
            {
            case ZombieType.Pogo:
                mAltitude = 0f;
                if (mOnHighGround)
                {
                    mAltitude += Constants.HIGH_GROUND_HEIGHT;
                }
                break;
            case ZombieType.Balloon:
                BalloonPropellerHatSpin(false);
                break;
            case ZombieType.PeaHead:
            case ZombieType.WallnutHead:
            case ZombieType.TallnutHead:
            case ZombieType.JalapenoHead:
            case ZombieType.GatlingHead:
            case ZombieType.SquashHead:
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mSpecialHeadReanimID);
                if (reanimation != null)
                {
                    reanimation.mAnimRate = 0f;
                }
                break;
            }
            }
            UpdateAnimSpeed();
            StopZombieSound();
        }

        public float ZombieTargetLeadX(float theTime)
        {
            float num = mVelX;
            if (mChilledCounter > 0)
            {
                num *= GameConstants.CHILLED_SPEED_FACTOR;
            }
            if (IsWalkingBackwards())
            {
                num = -num;
            }
            if (ZombieNotWalking())
            {
                num = 0f;
            }
            float num2 = num * theTime;
            TRect zombieRect = GetZombieRect();
            int num3 = zombieRect.mX + zombieRect.mWidth / 2;
            return num3 - num2;
        }

        public void UpdateZombieImp()//1update
        {
            if (mZombiePhase == ZombiePhase.ImpGettingThrown)
            {
                mVelZ -= GameConstants.THOWN_ZOMBIE_GRAVITY;
                mAltitude += mVelZ;
                mPosX -= mVelX;
                float aDiffY = GetPosYBasedOnRow(mRow) - mPosY;
                mPosY += aDiffY;
                mAltitude += aDiffY;
                if (mAltitude <= 0f)
                {
                    mAltitude = 0f;
                    mZombiePhase = ZombiePhase.ImpLanding;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_land, ReanimLoopType.PlayOnceAndHold, 0, 24f);
                    return;
                }
            }
            else if (mZombiePhase == ZombiePhase.ImpLanding)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.mLoopCount > 0)
                {
                    mZombiePhase = ZombiePhase.ZombieNormal;
                    StartWalkAnim(0);
                }
            }
        }

        public void SquishAllInSquare(int theX, int theY, ZombieAttackType theAttackType)
        {
            for (int i = 0; i < mBoard.mPlants.Count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead
                    && theY == aPlant.mRow
                    && theX == aPlant.mPlantCol
                    && (theAttackType != ZombieAttackType.DriveOver || !aPlant.IsSpiky())
                    && aPlant.mSeedType != SeedType.Spikerock)
                {
                    mBoard.mPlantsEaten++;
                    aPlant.Squish();
                }
            }
        }

        public void RemoveIceTrap()
        {
            mIceTrapCounter = 0;
            if (mZombieType == ZombieType.Balloon)
            {
                BalloonPropellerHatSpin(true);
            }
            UpdateAnimSpeed();
            StartZombieSound();
        }

        public bool IsBouncingPogo()
        {
            return mZombiePhase >= ZombiePhase.PogoBouncing && mZombiePhase <= ZombiePhase.PogoForwardBounce7;
        }

        public int GetBobsledPosition()
        {
            if (mZombieType != ZombieType.Bobsled)
            {
                return -1;
            }
            if (mRelatedZombieID == null && mFollowerZombieID[0] == null)
            {
                return -1;
            }
            if (mRelatedZombieID == null)
            {
                return 0;
            }
            Zombie zombie = mBoard.ZombieGetID(this);
            Zombie zombie2 = mBoard.ZombieGet(mRelatedZombieID);
            for (int i = 0; i < 3; i++)
            {
                if (zombie2.mFollowerZombieID[i] == zombie)
                {
                    return i + 1;
                }
            }
            Debug.ASSERT(false);
            return -666;
        }

        public void DrawBobsledReanim(Graphics g, ref ZombieDrawPosition theDrawPos, bool theBeforeZombie)
        {
            int bobsledPosition = GetBobsledPosition();
            bool aDrawFront = false;
            bool aDrawBack = false;
            Zombie aZombieLeader;
            if (mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
            {
                aZombieLeader = this;
            }
            else
            {
                if (bobsledPosition == -1)
                {
                    return;
                }
                if (bobsledPosition == 0)
                {
                    aZombieLeader = this;
                }
                else
                {
                    aZombieLeader = mBoard.ZombieGet(mRelatedZombieID);
                }
            }
            if (mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
            {
                if (theBeforeZombie)
                {
                    aDrawBack = true;
                }
                else
                {
                    aDrawFront = true;
                }
            }
            else if (mZombiePhase == ZombiePhase.BobsledCrashing)
            {
                if (bobsledPosition == 0 && !theBeforeZombie)
                {
                    aDrawFront = true;
                    aDrawBack = true;
                }
            }
            else if (mZombiePhase == ZombiePhase.BobsledSliding || mZombiePhase == ZombiePhase.ZombieBurned)
            {
                if (bobsledPosition == 2 && theBeforeZombie)
                {
                    aDrawFront = true;
                    aDrawBack = true;
                }
            }
            else if (mZombiePhase == ZombiePhase.BobsledBoarding)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation.mAnimTime < 0.5f)
                {
                    if (bobsledPosition == 2 && theBeforeZombie)
                    {
                        aDrawFront = true;
                        aDrawBack = true;
                    }
                }
                else if (bobsledPosition == 0 && !theBeforeZombie)
                {
                    aDrawFront = true;
                }
                else if (bobsledPosition == 3 && theBeforeZombie)
                {
                    aDrawBack = true;
                }
            }
            int aAlpha = 255;
            float aOffsetX = theDrawPos.mImageOffsetX + aZombieLeader.mPosX - mPosX - 76f;
            float aOffsetY = 15f;
            int aBobsledDamageStatus;
            if (mZombiePhase == ZombiePhase.BobsledCrashing)
            {
                aBobsledDamageStatus = 3;
                aAlpha = TodCommon.TodAnimateCurve(30, 0, mPhaseCounter, 255, 0, TodCurves.Linear);
                aOffsetX += (GameConstants.BOBSLED_CRASH_TIME - mPhaseCounter) * mVelX / GameConstants.ZOMBIE_LIMP_SPEED_FACTOR;
                aOffsetX -= TodCommon.TodAnimateCurveFloat(GameConstants.BOBSLED_CRASH_TIME, 0, mPhaseCounter, 0f, 50f, TodCurves.EaseOut);
                aOffsetY += TodCommon.TodAnimateCurveFloat(GameConstants.BOBSLED_CRASH_TIME, 75, mPhaseCounter, 5f, 10f, TodCurves.Linear);
            }
            else
            {
                aBobsledDamageStatus = aZombieLeader.GetHelmDamageIndex();
            }
            if (aAlpha != 255)
            {
                g.SetColorizeImages(true);
                g.SetColor(new SexyColor(255, 255, 255, aAlpha));
            }
            Image aImage;
            if (aBobsledDamageStatus == 0)
            {
                aImage = AtlasResources.IMAGE_ZOMBIE_BOBSLED1;
            }
            else if (aBobsledDamageStatus == 1)
            {
                aImage = AtlasResources.IMAGE_ZOMBIE_BOBSLED2;
            }
            else if (aBobsledDamageStatus == 2)
            {
                aImage = AtlasResources.IMAGE_ZOMBIE_BOBSLED3;
            }
            else
            {
                aImage = AtlasResources.IMAGE_ZOMBIE_BOBSLED4;
            }
            if (mZombiePhase == ZombiePhase.ZombieBurned)
            {
                g.SetColorizeImages(true);
                g.SetColor(SexyColor.Black);
            }
            aOffsetX *= Constants.S;
            aOffsetY *= Constants.S;
            if (aDrawBack && aBobsledDamageStatus != 3)
            {
                g.DrawImageF(AtlasResources.IMAGE_ZOMBIE_BOBSLED_INSIDE, aOffsetX, aOffsetY);
            }
            if (aDrawFront)
            {
                g.DrawImageF(aImage, aOffsetX, aOffsetY);
            }
            if (aZombieLeader.mJustGotShotCounter > 0)
            {
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                g.SetColorizeImages(true);
                int num5 = aZombieLeader.mJustGotShotCounter * 10;
                g.SetColor(new SexyColor(num5, num5, num5, 255));
                if (aDrawBack && aBobsledDamageStatus != 3)
                {
                    g.DrawImageF(AtlasResources.IMAGE_ZOMBIE_BOBSLED_INSIDE, aOffsetX, aOffsetY);
                }
                if (aDrawFront)
                {
                    g.DrawImageF(aImage, aOffsetX, aOffsetY);
                }
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            }
            g.SetColorizeImages(false);
        }

        public void BobsledDie()
        {
            if (!IsBobsledTeamWithSled())
            {
                return;
            }
            if (!IsOnBoard())
            {
                return;
            }
            Zombie zombie;
            if (mRelatedZombieID == null)
            {
                zombie = this;
            }
            else
            {
                zombie = mBoard.ZombieGet(mRelatedZombieID);
            }
            if (!zombie.mDead)
            {
                zombie.DieNoLoot(true);
            }
            for (int i = 0; i < 3; i++)
            {
                Zombie zombie2 = mBoard.ZombieGet(zombie.mFollowerZombieID[i]);
                if (!zombie2.mDead)
                {
                    zombie2.DieNoLoot(true);
                }
            }
        }

        public void BobsledBurn()
        {
            if (!IsBobsledTeamWithSled())
            {
                return;
            }
            Zombie zombie;
            if (mRelatedZombieID == null)
            {
                zombie = this;
            }
            else
            {
                zombie = mBoard.ZombieGet(mRelatedZombieID);
            }
            zombie.ApplyBurn();
            for (int i = 0; i < 3; i++)
            {
                Zombie zombie2 = mBoard.ZombieGet(zombie.mFollowerZombieID[i]);
                zombie2.ApplyBurn();
            }
        }

        public bool IsBobsledTeamWithSled()
        {
            return GetBobsledPosition() != -1;
        }

        public bool CanBeFrozen()
        {
            return CanBeChilled() && mZombiePhase != ZombiePhase.PolevaulterInVault && mZombiePhase != ZombiePhase.DolphinIntoPool && mZombiePhase != ZombiePhase.DolphinInJump && mZombiePhase != ZombiePhase.SnorkelIntoPool && !IsFlying() && mZombiePhase != ZombiePhase.ImpGettingThrown && mZombiePhase != ZombiePhase.ImpLanding && mZombiePhase != ZombiePhase.BobsledCrashing && mZombiePhase != ZombiePhase.JackInTheBoxPopping && mZombiePhase != ZombiePhase.SquashRising && mZombiePhase != ZombiePhase.SquashFalling && mZombiePhase != ZombiePhase.SquashDoneFalling && !IsBouncingPogo() && (mZombieType != ZombieType.Bungee || mZombiePhase == ZombiePhase.BungeeAtBottom);
        }

        public bool CanBeChilled()
        {
            return mZombieType != ZombieType.Zamboni && !IsBobsledTeamWithSled() && !IsDeadOrDying() && mZombiePhase != ZombiePhase.DiggerTunneling && mZombiePhase != ZombiePhase.DiggerRising && mZombiePhase != ZombiePhase.DiggerTunnelingPauseWithoutAxe && mZombiePhase != ZombiePhase.DiggerRiseWithoutAxe && mZombiePhase != ZombiePhase.RisingFromGrave && mZombiePhase != ZombiePhase.DancerRising && !mMindControlled && (mZombieType != ZombieType.Boss || mZombiePhase == ZombiePhase.BossHeadIdleBeforeSpit || mZombiePhase == ZombiePhase.BossHeadIdleAfterSpit || mZombiePhase == ZombiePhase.BossHeadSpit);
        }

        public void UpdateZombieSnorkel()//3update
        {
            bool flag = IsWalkingBackwards();
            if (mZombiePhase == ZombiePhase.SnorkelWalking && !flag)
            {
                if (mX > 770 && mX <= 800)
                {
                    mZombiePhase = ZombiePhase.SnorkelIntoPool;
                    mVelX = 0.2f;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jumpinpool, ReanimLoopType.PlayOnceAndHold, 20, 16f);
                }
            }
            else if (mZombiePhase == ZombiePhase.SnorkelIntoPool)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                mAltitude = TodCommon.TodAnimateCurveFloat(0, 1000, (int)reanimation.mAnimTime * 1000, 0f, 10f, TodCurves.Linear);
                if (reanimation.ShouldTriggerTimedEvent(0.83f))
                {
                    Reanimation reanimation2 = mApp.AddReanimation(mX - 47, mY + 73, mRenderOrder + 1, ReanimationType.Splash);
                    reanimation2.OverrideScale(1.2f, 0.8f);
                    mApp.AddTodParticle(mX - 10, mY + 115, mRenderOrder + 1, ParticleEffect.PlantingPool);
                    mApp.PlayFoley(FoleyType.ZombieEnteringWater);
                }
                if (reanimation.mLoopCount > 0)
                {
                    mZombiePhase = ZombiePhase.SnorkelWalkingInPool;
                    mInPool = true;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_swim, ReanimLoopType.LoopFullLastFrame, 0, 12f);
                }
            }
            else if (mZombiePhase == ZombiePhase.SnorkelWalkingInPool)
            {
                if (!mHasHead)
                {
                    TakeDamage(1800, 9U, false);
                }
                else if (mX <= 140 && !flag)
                {
                    mZombieHeight = ZombieHeight.OutOfPool;
                    mZombiePhase = ZombiePhase.SnorkelWalking;
                    mAltitude = -90f;
                    mPosX -= 15f;
                    PoolSplash(false);
                    StartWalkAnim(0);
                }
                else if (mX > 730f && flag)
                {
                    mZombieHeight = ZombieHeight.OutOfPool;
                    mZombiePhase = ZombiePhase.SnorkelWalking;
                    mAltitude = -90f;
                    mPosX += 15f;
                    PoolSplash(false);
                    StartWalkAnim(0);
                }
                else if (mIsEating)
                {
                    mZombiePhase = ZombiePhase.SnorkelUpToEat;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_uptoeat, ReanimLoopType.PlayOnceAndHold, 0, 24f);
                }
            }
            else if (mZombiePhase == ZombiePhase.SnorkelUpToEat)
            {
                Reanimation reanimation3 = mApp.ReanimationGet(mBodyReanimID);
                if (!mIsEating)
                {
                    mZombiePhase = ZombiePhase.SnorkelDownFromEat;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_uptoeat, ReanimLoopType.PlayOnceAndHold, 0, -24f);
                }
                else if (reanimation3.mLoopCount > 0)
                {
                    mZombiePhase = ZombiePhase.SnorkelEatingInPool;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_eat, ReanimLoopType.Loop, 0, 0f);
                }
            }
            else if (mZombiePhase == ZombiePhase.SnorkelEatingInPool)
            {
                if (!mIsEating)
                {
                    mZombiePhase = ZombiePhase.SnorkelDownFromEat;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_uptoeat, ReanimLoopType.PlayOnceAndHold, 0, -24f);
                }
            }
            else if (mZombiePhase == ZombiePhase.SnorkelDownFromEat)
            {
                Reanimation reanimation4 = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation4.mLoopCount > 0)
                {
                    mZombiePhase = ZombiePhase.SnorkelWalkingInPool;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_swim, ReanimLoopType.LoopFullLastFrame, 0, 0f);
                    PickRandomSpeed();
                }
            }
            mUsesClipping = (mZombiePhase == ZombiePhase.SnorkelWalking || mZombiePhase == ZombiePhase.ZombieDying || mZombiePhase == ZombiePhase.SnorkelIntoPool);
        }

        public void ReanimIgnoreClipRect(string theTrackName, bool theIgnoreClipRect)
        {
            Reanimation reanimation_v = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation_v == null)
            {
                return;
            }
            for (int i = 0; i < reanimation_v.mDefinition.mTrackCount; i++)
            {
                if (reanimation_v.mDefinition.mTracks[i].mName == theTrackName)
                {
                    reanimation_v.mTrackInstances[i].mIgnoreClipRect = theIgnoreClipRect;
                }
            }
            mUsesClipping = !theIgnoreClipRect;
        }

        public void SetAnimRate(float theAnimRate)
        {
            mOrginalAnimRate = theAnimRate;
            ApplyAnimRate(theAnimRate);
        }

        public void ApplyAnimRate(float theAnimRate)
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            reanimation.mAnimRate = IsMovingAtChilledSpeed() ? theAnimRate * 0.5f : theAnimRate;
        }

        public bool IsDeadOrDying()
        {
            return mDead || mZombiePhase == ZombiePhase.ZombieDying || mZombiePhase == ZombiePhase.ZombieBurned || mZombiePhase == ZombiePhase.ZombieMowered;
        }

        public void DrawDancerReanim(Graphics g, ref ZombieDrawPosition theDrawPos)
        {
            SexyColor aSpotLightColor = default(SexyColor);
            if (mZombiePhase != ZombiePhase.DancerDancingIn && mZombiePhase != ZombiePhase.DancerSnappingFingers && mZombiePhase != ZombiePhase.ZombieNormal && mZombiePhase != ZombiePhase.ZombieDying && mApp.mGameScene != GameScenes.ZombiesWon)
            {
                switch (mZombieAge >= 700 ? mZombieAge / 100 * 7 % 5 : 0)
                {
                    case 0:
                        aSpotLightColor = new SexyColor(250, 250, 160);
                        break;
                    case 1:
                        aSpotLightColor = new SexyColor(114, 234, 170);
                        break;
                    case 2:
                        aSpotLightColor = new SexyColor(216, 126, 202);
                        break;
                    case 3:
                        aSpotLightColor = new SexyColor(90, 110, 140);
                        break;
                    case 4:
                        aSpotLightColor = new SexyColor(240, 90, 130);
                        break;
                }
                g.SetColorizeImages(true);
                g.SetColor(aSpotLightColor);
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_SPOTLIGHT2, Constants.Zombie_Dancer_Spotlight_Offset.X * Constants.S, Constants.Zombie_Dancer_Spotlight_Offset.Y * Constants.S, Constants.Zombie_Dancer_Spotlight_Scale, Constants.Zombie_Dancer_Spotlight_Scale);
                g.SetColorizeImages(false);
                mApp.ReanimationTryToGet(mBodyReanimID).DrawRenderGroup(g, 0);
                g.SetColorizeImages(true);
                g.SetColor(aSpotLightColor);
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_SPOTLIGHT, (Constants.Zombie_Dancer_Spotlight_Pos.X + (float)Constants.Zombie_Dancer_Spotlight_Offset.X) * Constants.S, (Constants.Zombie_Dancer_Spotlight_Pos.Y + Constants.Zombie_Dancer_Spotlight_Offset.Y) * Constants.S, Constants.Zombie_Dancer_Spotlight_Scale, Constants.Zombie_Dancer_Spotlight_Scale);
                g.SetColorizeImages(false);
            }
            else 
            {
                mApp.ReanimationTryToGet(mBodyReanimID).DrawRenderGroup(g, 0);
            }
        }

        public void DrawBungeeReanim(Graphics g, ref ZombieDrawPosition theDrawPos)
        {
            Reanimation aReanim = mApp.ReanimationTryToGet(mBodyReanimID);
            float anOffsetX = Constants.InvertAndScale(-22f);
            float anOffsetY = Constants.InvertAndScale(14f) + theDrawPos.mBodyY + theDrawPos.mImageOffsetY;
            DrawBungeeCord(g, (int)anOffsetX, (int)anOffsetY);
            aReanim.DrawRenderGroup(g, 0);
            Zombie aZombie = null;
            int iZombie = -1;
            if (mBoard != null)
            {
                iZombie = mBoard.mZombies.IndexOf(mRelatedZombieID);
            }
            if (iZombie != -1)
            {
                aZombie = mBoard.mZombies[iZombie];
            }
            if (aZombie != null)
            {
                Graphics aDroppedGraphics = Graphics.GetNew(g);
                aDroppedGraphics.mTransY += (int)(-mAltitude * Constants.S);
                aDroppedGraphics.mTransX += (int)((aZombie.mPosX - mPosX) * Constants.S);
                ZombieDrawPosition aDroppedDrawPos = default(ZombieDrawPosition);
                aZombie.GetDrawPos(ref aDroppedDrawPos);
                aZombie.DrawReanim(aDroppedGraphics, ref aDroppedDrawPos, 0);
                aDroppedGraphics.PrepareForReuse();
            }
            else
            {
                Plant aPlant = null;
                int iPlant = -1;
                if (mBoard != null)
                {
                    iPlant = mBoard.mPlants.IndexOf(mTargetPlantID);
                }
                if (iPlant != -1)
                {
                    aPlant = mBoard.mPlants[iPlant];
                }
                if (aPlant != null)
                {
                    Graphics aPlantGraphics = Graphics.GetNew(g);
                    aPlantGraphics.mTransY += (int)((30f - mAltitude) * Constants.S);
                    if (mZombiePhase == ZombiePhase.BungeeRising && (aPlant.mSeedType == SeedType.Spikeweed || aPlant.mSeedType == SeedType.Spikerock))
                    {
                        aPlantGraphics.mTransY -= 34;
                    }
                    if (aPlant.mPlantCol <= 4 && mBoard.StageHasRoof())
                    {
                        aPlantGraphics.mTransY += 10;
                    }
                    aPlant.Draw(aPlantGraphics);
                    aPlantGraphics.PrepareForReuse();
                }
            }
            aReanim.DrawRenderGroup(g, GameConstants.RENDER_GROUP_ARMS);
        }

        public void DrawBungeeTarget(Graphics g)
        {
            if (!IsOnBoard())
            {
                return;
            }
            if (mApp.IsFinalBossLevel())
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.BungeeHitOuchy)
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.BungeeRising)
            {
                return;
            }
            if (mRelatedZombieID != null)
            {
                return;
            }
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            ZombieDrawPosition aDrawPos = default(ZombieDrawPosition);
            GetDrawPos(ref aDrawPos);
            float aTargetX = mX + Constants.Zombie_Bungee_Target_Offset.X;
            float aTargetY = mY + Constants.Zombie_Bungee_Target_Offset.Y + aDrawPos.mBodyY + aDrawPos.mImageOffsetY;
            if (mZombiePhase == ZombiePhase.BungeeDiving || mZombiePhase == ZombiePhase.BungeeDivingScreaming)
            {
                aTargetX += TodCommon.TodAnimateCurveFloat(GameConstants.BUNGEE_ZOMBIE_HEIGHT, GameConstants.BUNGEE_ZOMBIE_HEIGHT - 400, (int)mAltitude, 30f, 0f, TodCurves.Linear);
                aTargetY += TodCommon.TodAnimateCurveFloat(GameConstants.BUNGEE_ZOMBIE_HEIGHT, GameConstants.BUNGEE_ZOMBIE_HEIGHT - 400, (int)mAltitude, -600f, 0f, TodCurves.Linear);
            }
            aTargetY += mAltitude;
            g.DrawImageF(AtlasResources.IMAGE_BUNGEETARGET, aTargetX * Constants.S, aTargetY * Constants.S);
        }

        public void BungeeDie()
        {
            BungeeDropPlant();
            Plant plant = null;
            int num = -1;
            if (mBoard != null)
            {
                num = mBoard.mPlants.IndexOf(mTargetPlantID);
            }
            if (num != -1)
            {
                plant = mBoard.mPlants[num];
            }
            if (plant != null)
            {
                mBoard.mPlantsEaten++;
                plant.Die();
            }
            Zombie zombie = null;
            if (mBoard != null)
            {
                zombie = mBoard.ZombieTryToGet(mRelatedZombieID);
            }
            if (zombie != null && !zombie.mDead)
            {
                zombie.DieNoLoot(true);
            }
        }

        public void ZamboniDeath(uint theDamageFlags)
        {
            if (!TodCommon.TestBit(theDamageFlags, 5))
            {
                mApp.AddTodParticle(mPosX + 80f, mPosY + 60f, mRenderOrder + 1, ParticleEffect.ZamboniExplosion);
                DieWithLoot();
                mApp.PlayFoley(FoleyType.Explosion);
                return;
            }
            mFlatTires = true;
            mApp.PlayFoley(FoleyType.TirePop);
            mZombiePhase = ZombiePhase.ZombieDying;
            mApp.AddTodParticle(mPosX + 29f, mPosY + 114f, mRenderOrder + 1, ParticleEffect.ZamboniTire);
            mVelX = 0f;
            if (RandomNumbers.NextNumber(4) == 0 && mPosX < 600f + Constants.BOARD_EXTRA_ROOM)
            {
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_wheelie2, ReanimLoopType.PlayOnceAndHold, 10, 10f);
                mPhaseCounter = 280;
                return;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            TodParticleSystem todParticleSystem = mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.ZamboniSmoke);
            if (todParticleSystem != null)
            {
                reanimation.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_1, ref todParticleSystem, 35f, 85f);
            }
            mPhaseCounter = 280;
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_wheelie1, ReanimLoopType.PlayOnceAndHold, 10, 12f);
        }

        public void CatapultDeath(uint theDamageFlags)
        {
            if (TodCommon.TestBit(theDamageFlags, 5))
            {
                mApp.PlayFoley(FoleyType.TirePop);
                mZombiePhase = ZombiePhase.ZombieDying;
                mApp.AddTodParticle(mPosX + 29f, mPosY + 114f, mRenderOrder + 1, ParticleEffect.ZamboniTire);
                mVelX = 0f;
                AddAttachedParticle(47, 77, ParticleEffect.ZamboniSmoke);
                mPhaseCounter = 280;
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_bounce, ReanimLoopType.PlayOnceAndHold, 10, 12f);
                return;
            }
            mApp.AddTodParticle(mPosX + 80f, mPosY + 60f, mRenderOrder + 1, ParticleEffect.CatapultExplosion);
            DieWithLoot();
            mApp.PlayFoley(FoleyType.Explosion);
        }

        public bool SetupDrawZombieWon(Graphics g)
        {
            if (mFromWave != GameConstants.ZOMBIE_WAVE_WINNER)
            {
                return true;
            }
            if (!mBoard.mCutScene.ShowZombieWalking())
            {
                return false;
            }
            switch (mBoard.mBackground)
            {
                case BackgroundType.Num1Day:
                case BackgroundType.Num2Night:
                    g.ClipRect((int)((-123 - mX) * Constants.S) + Constants.Zombie_GameOver_ClipOffset_1, (int)(-mY * Constants.S), 800, 600);
                    break;
                case BackgroundType.Num3Pool:
                case BackgroundType.Num4Fog:
                    g.ClipRect((int)((-172 - mX) * Constants.S) + Constants.Zombie_GameOver_ClipOffset_2, (int)(-mY * Constants.S), 800, 600);
                    break;
                case BackgroundType.Num5Roof:
                case BackgroundType.Num6Boss:
                    g.ClipRect((int)((-95 - mX) * Constants.S) + Constants.Zombie_GameOver_ClipOffset_3, (int)(-mY * Constants.S), 800, 600);
                    break;
            }
            return true;
        }

        public void WalkIntoHouse()
        {
            Zombie.WinningZombieReachedDesiredY = false;
            GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.ZamboniSmoke, null);
            mFromWave = GameConstants.ZOMBIE_WAVE_WINNER;
            if (mZombiePhase == ZombiePhase.PolevaulterPreVault)
            {
                mZombiePhase = ZombiePhase.PolevaulterPostVault;
                StartWalkAnim(0);
            }
            if (mBoard.mBackground == BackgroundType.Num1Day || mBoard.mBackground == BackgroundType.Num2Night || mBoard.mBackground == BackgroundType.Num3Pool || mBoard.mBackground == BackgroundType.Num4Fog || mBoard.mBackground == BackgroundType.Num5Roof || mBoard.mBackground == BackgroundType.Num6Boss)
            {
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.Zombie, 2, 100);
                for (int i = 0; i < mBoard.mLawnMowers.Count; i++)
                {
                    mBoard.mLawnMowers[i].PrepareForReuse();
                }
                mBoard.mLawnMowers.Clear();
                if (mZombiePhase == ZombiePhase.PolevaulterPreVault)
                {
                    mPosX += 35f;
                }
                if (mBoard.mBackground == BackgroundType.Num3Pool || mBoard.mBackground == BackgroundType.Num4Fog)
                {
                    ZombieType zombieType = mZombieType;
                }
            }
        }

        public void UpdateZamboni()//3update
        {
            if (mPosX > 400f && !mFlatTires)
            {
                mVelX = TodCommon.TodAnimateCurveFloat(700, 300, (int)mPosX, 0.25f, 0.05f, TodCurves.Linear);
            }
            else if (mFlatTires && mVelX > 0.0005f)
            {
                mVelX -= 0.0005f;
            }
            int num_v = (int)mPosX + 118;
            if (mBoard.StageHasRoof())
            {
                num_v = Math.Max(num_v, 500);
            }
            else
            {
                num_v = Math.Max(num_v, 25);
            }
            if (num_v < mBoard.mIceMinX[mRow])
            {
                mBoard.mIceMinX[mRow] = num_v;
            }
            if (num_v < 860)
            {
                mBoard.mIceTimer[mRow] = 3000;
                if (mApp.mGameMode == GameMode.ChallengeBobsledBonanza)
                {
                    mBoard.mIceTimer[mRow] = int.MaxValue;
                }
            }
        }

        public void UpdateZombieChimney()//3update
        {
        }

        public void UpdateLadder()//3update
        {
            if (mMindControlled || !mHasHead || IsDeadOrDying())
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.LadderCarrying && mZombieHeight == ZombieHeight.ZombieNormal)
            {
                Plant plant = FindPlantTarget(ZombieAttackType.Ladder);
                if (plant != null)
                {
                    StopEating();
                    mZombiePhase = ZombiePhase.LadderPlacing;
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_placeladder, ReanimLoopType.PlayOnceAndHold, 10, 24f);
                    return;
                }
            }
            else if (mZombiePhase == ZombiePhase.LadderPlacing)
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
                if (reanimation.mLoopCount > 0)
                {
                    Plant plant2 = FindPlantTarget(ZombieAttackType.Ladder);
                    if (plant2 != null)
                    {
                        mBoard.AddALadder(plant2.mPlantCol, plant2.mRow);
                        mApp.PlaySample(Resources.SOUND_LADDER_ZOMBIE);
                        mZombieHeight = ZombieHeight.UpLadder;
                        mUseLadderCol = plant2.mPlantCol;
                        DetachShield();
                        return;
                    }
                    mZombiePhase = ZombiePhase.LadderCarrying;
                    StartWalkAnim(0);
                }
            }
        }

        private void SetupReanimForLostArm(uint theDamageFlags)
        {
            switch (mZombieType)
            {
            case ZombieType.Football:
                ReanimShowPrefix("Zombie_football_leftarm_lower", -1);
                ReanimShowPrefix("Zombie_football_leftarm_hand", -1);
                break;
            case ZombieType.Newspaper:
                ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_paper_hands, -1);
                ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_paper_leftarm_lower, -1);
                break;
            case ZombieType.Polevaulter:
                ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_polevaulter_outerarm_lower, -1);
                ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, -1);
                break;
            case ZombieType.Dancer:
                ReanimShowPrefix("Zombie_disco_outerarm_lower", -1);
                ReanimShowPrefix("Zombie_disco_outerhand_point", -1);
                ReanimShowPrefix("Zombie_disco_outerhand", -1);
                ReanimShowPrefix("Zombie_disco_outerarm_upper", -1);
                break;
            case ZombieType.BackupDancer:
                ReanimShowPrefix("Zombie_disco_outerarm_lower", -1);
                ReanimShowPrefix("Zombie_disco_outerhand", -1);
                break;
            default:
                ReanimShowPrefix("Zombie_outerarm_lower", -1);
                ReanimShowPrefix("Zombie_outerarm_hand", -1);
                break;
            }
            ZombieDrawPosition aDrawPos = default(ZombieDrawPosition);
            GetDrawPos(ref aDrawPos);
            float aPosX = mPosX + aDrawPos.mImageOffsetX + 45f;
            float aPosY = mPosY + aDrawPos.mImageOffsetY + aDrawPos.mBodyY + 78f;
            if (IsWalkingBackwards())
            {
                aPosX += 36f;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation != null)
            {
                switch (mZombieType)
                {
                case ZombieType.Football:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_football_leftarm_hand, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_football_leftarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_LEFTARM_UPPER2);
                    break;
                case ZombieType.Newspaper:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_paper_leftarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_paper_leftarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_LEFTARM_UPPER2);
                    break;
                case ZombieType.Polevaulter:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_polevaulter_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_polevaulter_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_POLEVAULTER_OUTERARM_UPPER2);
                    break;
                case ZombieType.Balloon:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_BALLOON_OUTERARM_UPPER2);
                    break;
                case ZombieType.Imp:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_imp_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_IMP_ARM1_BONE);
                    break;
                case ZombieType.Digger:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_digger_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_OUTERARM_UPPER2);
                    break;
                case ZombieType.Bobsled:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_dolphinrider_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_BOBSLED_OUTERARM_UPPER2);
                    break;
                case ZombieType.JackInTheBox:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_outerarm_lower, AtlasResources.IMAGE_REANIM_ZOMBIE_JACKBOX_OUTERARM_LOWER2);
                    break;
                case ZombieType.Snorkel:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_snorkle_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_SNORKLE_OUTERARM_UPPER2);
                    break;
                case ZombieType.DolphinRider:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_dolphinrider_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_DOLPHINRIDER_OUTERARM_UPPER2);
                    break;
                case ZombieType.Pogo:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_OUTERARM_UPPER2);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_pogo_stickhands, AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_STICKHANDS2);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_pogo_stick, AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_STICKDAMAGE2);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_pogo_stick2, AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_STICK2DAMAGE2);
                    break;
                case ZombieType.Flag:
                {
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_OUTERARM_UPPER2);
                    Reanimation reanimation2 = mApp.ReanimationTryToGet(mSpecialHeadReanimID);
                    if (reanimation2 != null)
                    {
                        reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_flag, AtlasResources.IMAGE_REANIM_ZOMBIE_FLAG3);
                    }
                    break;
                }
                case ZombieType.Dancer:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_disco_outerhand, ref aPosX, ref aPosY);
                    ReanimShowPrefix("Zombie_disco_outerarm_upper_bone", 0);
                    break;
                case ZombieType.BackupDancer:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_disco_outerhand, ref aPosX, ref aPosY);
                    ReanimShowPrefix("Zombie_disco_outerarm_upper_bone", 0);
                    break;
                case ZombieType.Ladder:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_ladder_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_OUTERARM_UPPER2);
                    break;
                case ZombieType.Yeti:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_yeti_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_YETI_OUTERARM_UPPER2);
                    break;
                default:
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref aPosX, ref aPosY);
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_OUTERARM_UPPER2);
                    break;
                }
            }
            if (!mInPool && !TodCommon.TestBit(theDamageFlags, 4))
            {
                ParticleEffect theEffect = ParticleEffect.ZombieArm;
                if (mZombiePhase == ZombiePhase.ZombieMowered)
                {
                    theEffect = ParticleEffect.MoweredZombieArm;
                }
                if (mZombiePhase == ZombiePhase.ZombieMowered)
                {
                    aPosX -= 40f;
                    aPosY -= 50f;
                }
                TodParticleSystem aTodParticleSystem = mApp.AddTodParticle(aPosX, aPosY, mRenderOrder + 1, theEffect);
                OverrideParticleColor(aTodParticleSystem);
                OverrideParticleScale(aTodParticleSystem);
                if (aTodParticleSystem != null)
                {
                    switch (mZombieType)
                    {
                    case ZombieType.Football:
                        aTodParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_LEFTARM_HAND);
                        return;
                    case ZombieType.Newspaper:
                        aTodParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_LEFTARM_LOWER);
                        return;
                    case ZombieType.Dancer:
                        ReanimShowPrefix("Zombie_disco_outerarm_lower", -1);
                        ReanimShowPrefix("Zombie_disco_outerhand_point", -1);
                        ReanimShowPrefix("Zombie_disco_outerhand", -1);
                        ReanimShowPrefix("Zombie_disco_outerarm_upper", -1);
                        return;
                    case ZombieType.BackupDancer:
                        ReanimShowPrefix("Zombie_disco_outerarm_lower", -1);
                        ReanimShowPrefix("Zombie_disco_outerhand", -1);
                        return;
                    case ZombieType.Bobsled:
                        aTodParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_BOBSLED_OUTERARM_HAND);
                        return;
                    case ZombieType.Imp:
                        aTodParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_IMP_ARM2);
                        return;
                    case ZombieType.Yeti:
                        aTodParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_YETI_OUTERARM_HAND);
                        return;
                    case ZombieType.JackInTheBox:
                        aTodParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEJACKBOXARM);
                        return;
                    case ZombieType.Digger:
                        aTodParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEDIGGERARM);
                        return;
                    case ZombieType.Polevaulter:
                    case ZombieType.Balloon:
                    case ZombieType.DolphinRider:
                    case ZombieType.Pogo:
                    case ZombieType.Ladder:
                        aTodParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_OUTERARM_HAND);
                        break;
                    }
                }
            }
        }

        public void DropArm(uint theDamageFlags)
        {
            if (!CanLoseBodyParts())
            {
                return;
            }
            if (mShieldType == ShieldType.Door || mShieldType == ShieldType.Newspaper)
            {
                return;
            }
            if (mZombiePhase == ZombiePhase.SnorkelIntoPool || mZombiePhase == ZombiePhase.DolphinWalking || mZombiePhase == ZombiePhase.DolphinIntoPool || mZombiePhase == ZombiePhase.DolphinRiding || mZombiePhase == ZombiePhase.DolphinInJump || mZombiePhase == ZombiePhase.NewspaperReading)
            {
                return;
            }
            if (!mHasArm)
            {
                return;
            }
            mHasArm = false;
            SetupReanimForLostArm(theDamageFlags);
            mApp.PlayFoley(FoleyType.LimbsPop);
        }

        public bool CanLoseBodyParts()
        {
            return mZombieType != ZombieType.Zamboni && mZombieType != ZombieType.Bungee && mZombieType != ZombieType.Catapult && mZombieType != ZombieType.Gargantuar && mZombieType != ZombieType.RedeyeGargantuar && mZombieType != ZombieType.Boss && mZombieHeight != ZombieHeight.Zombiquarium && !IsFlying() && !IsBobsledTeamWithSled();
        }

        public void DropHelm(uint theDamageFlags)
        {
            if (mHelmType == HelmType.None)
            {
                return;
            }
            ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
            GetDrawPos(ref zombieDrawPosition);
            float theX = mPosX + zombieDrawPosition.mImageOffsetX + zombieDrawPosition.mHeadX + 14f;
            float theY = mPosY + zombieDrawPosition.mImageOffsetY + zombieDrawPosition.mHeadY + zombieDrawPosition.mBodyY + 18f;
            ParticleEffect particleEffect = ParticleEffect.None;
            if (mHelmType == HelmType.TrafficCone)
            {
                GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_cone, ref theX, ref theY);
                ReanimShowPrefix("anim_cone", -1);
                ReanimShowPrefix("anim_hair", 0);
                particleEffect = ParticleEffect.ZombieTrafficCone;
            }
            else if (mHelmType == HelmType.Pail)
            {
                GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_bucket, ref theX, ref theY);
                ReanimShowPrefix("anim_bucket", -1);
                ReanimShowPrefix("anim_hair", 0);
                particleEffect = ParticleEffect.ZombiePail;
            }
            else if (mHelmType == HelmType.Football)
            {
                GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_football_helmet, ref theX, ref theY);
                ReanimShowPrefix("zombie_football_helmet", -1);
                ReanimShowPrefix("anim_hair", 0);
                particleEffect = ParticleEffect.ZombieHelmet;
            }
            else if (mHelmType == HelmType.Digger)
            {
                GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_hardhat, ref theX, ref theY);
                ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_hardhat, -1);
                particleEffect = ParticleEffect.ZombieHeadlight;
            }
            else if (mHelmType == HelmType.Bobsled && !TodCommon.TestBit(theDamageFlags, 4))
            {
                BobsledCrash();
            }
            if (!TodCommon.TestBit(theDamageFlags, 4) && particleEffect != ParticleEffect.None)
            {
                TodParticleSystem aParticle = mApp.AddTodParticle(theX, theY, mRenderOrder + 1, particleEffect);
                OverrideParticleScale(aParticle);
            }
            mHasHelm = false;
            mHelmType = HelmType.None;
        }

        public void DropShield(uint theDamageFlags)
        {
            if (mShieldType == ShieldType.None)
            {
                return;
            }
            ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
            GetDrawPos(ref zombieDrawPosition);
            int aRenderOrder = mRenderOrder + 1;
            TodParticleSystem aParticle = null;
            switch (mShieldType)
            {
            case ShieldType.Door:
            {
                DetachShield();
                if (!TodCommon.TestBit(theDamageFlags, 4))
                {
                    float aPosX = 0f;
                    float aPosY = 0f;
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_screendoor, ref aPosX, ref aPosY);
                    aParticle = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, ParticleEffect.ZombieDoor);
                }
                break;
            }
            case ShieldType.Newspaper:
            {
                StopEating();
                if (mYuckyFace)
                {
                    ShowYuckyFace(false);
                    mYuckyFace = false;
                    mYuckyFaceCounter = 0;
                }
                mZombiePhase = ZombiePhase.NewspaperMaddening;
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_gasp, ReanimLoopType.PlayOnceAndHold, 10, 8f);
                DetachShield();
                if (!TodCommon.TestBit(theDamageFlags, 4))
                {
                    float aPosX = 0f;
                    float aPosY = 0f;
                    GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_paper_paper, ref aPosX, ref aPosY);
                    aParticle = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, ParticleEffect.ZombieNewspaper);
                }
                if (!TodCommon.TestBit(theDamageFlags, 4) && !TodCommon.TestBit(theDamageFlags, 0))
                {
                    mApp.PlayFoley(FoleyType.NewspaperRip);
                    AddAttachedReanim(-11, 0, ReanimationType.ZombieSurprise);
                    mSurprised = true;
                }
                break;
            }
            case ShieldType.Ladder:
            {
                DetachShield();
                if (!TodCommon.TestBit(theDamageFlags, 4))
                {
                    float aPosX = mPosX + 31f;
                    float aPosY = mPosY + 80f;
                    aParticle = mApp.AddTodParticle(aPosX, aPosY, aRenderOrder, ParticleEffect.ZombieLadder);
                }
                break;
            }
            }
            OverrideParticleScale(aParticle);
            mHasShield = false;
            mShieldType = ShieldType.None;
        }

        public void ReanimReenableClipping()
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            for (int i = 0; i < reanimation.mDefinition.mTrackCount; i++)
            {
                ReanimatorTrackInstance reanimatorTrackInstance = reanimation.mTrackInstances[i];
                reanimatorTrackInstance.mIgnoreClipRect = false;
            }
            mUsesClipping = true;
        }

        public void UpdateBoss()//3update
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (mApp.mGameScene == GameScenes.LevelIntro)
            {
                if (reanimation.ShouldTriggerTimedEvent(0.24f) || reanimation.ShouldTriggerTimedEvent(0.79f))
                {
                    mApp.PlayFoley(FoleyType.Thump);
                    mBoard.ShakeBoard(1, 4);
                    mApp.Vibrate();
                }
                return;
            }
            Reanimation reanimation2 = mApp.ReanimationGet(mSpecialHeadReanimID);
            UpdateBossFireball();
            if (mIceTrapCounter == 0)
            {
                if (mSummonCounter > 0)
                {
                    //mSummonCounter -= 3;
                    mSummonCounter--;
                }
                if (mBossBungeeCounter > 0)
                {
                    //mBossBungeeCounter -= 3;
                    mBossBungeeCounter--;
                }
                if (mBossStompCounter > 0)
                {
                    //mBossStompCounter -= 3;
                    mBossStompCounter--;
                }
                if (mBossHeadCounter > 0)
                {
                    //mBossHeadCounter -= 3;
                    mBossHeadCounter--;
                }
                if (mChilledCounter > 0)
                {
                    reanimation2.mAnimRate = 6f;
                }
                else if (reanimation2.mAnimRate == 0f)
                {
                    reanimation2.mAnimRate = 12f;
                }
            }
            else
            {
                reanimation2.mAnimRate = 0f;
            }
            if (mZombiePhase == ZombiePhase.BossEnter)
            {
                BossPlayIdle();
                return;
            }
            if (mZombiePhase != ZombiePhase.BossIdle)
            {
                if (mZombiePhase == ZombiePhase.BossSpawning)
                {
                    if (reanimation.ShouldTriggerTimedEvent(0.6f))
                    {
                        BossSpawnContact();
                    }
                    if (reanimation.mLoopCount > 0)
                    {
                        BossPlayIdle();
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossStomping)
                {
                    float theEventTime = 0.5f;
                    if (mTargetRow >= 2)
                    {
                        theEventTime = 0.55f;
                    }
                    if (reanimation.ShouldTriggerTimedEvent(theEventTime))
                    {
                        BossStompContact();
                    }
                    if (reanimation.mLoopCount > 0)
                    {
                        BossPlayIdle();
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossBungeesEnter)
                {
                    if (reanimation.ShouldTriggerTimedEvent(0.4f))
                    {
                        BossBungeeSpawn();
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossBungeesDrop)
                {
                    if (BossAreBungeesDone())
                    {
                        BossBungeeLeave();
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossBungeesLeave)
                {
                    if (reanimation.mLoopCount > 0)
                    {
                        BossPlayIdle();
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossDropRv)
                {
                    if (reanimation.ShouldTriggerTimedEvent(0.65f))
                    {
                        BossRVLanding();
                    }
                    if (reanimation.mLoopCount > 0)
                    {
                        BossPlayIdle();
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossHeadEnter)
                {
                    if (GetBodyDamageIndex() == 2 && reanimation.ShouldTriggerTimedEvent(0.37f))
                    {
                        ApplyBossSmokeParticles(true);
                    }
                    if (reanimation.ShouldTriggerTimedEvent(0.55f))
                    {
                        mApp.PlayFoley(FoleyType.Hydraulic);
                    }
                    if (reanimation.mLoopCount > 0)
                    {
                        mZombiePhase = ZombiePhase.BossHeadIdleBeforeSpit;
                        PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.Loop, 0, 12f);
                        mPhaseCounter = 500;
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossHeadIdleBeforeSpit)
                {
                    if (mBodyHealth == 1)
                    {
                        BossStartDeath();
                        return;
                    }
                    if (mPhaseCounter <= 0)
                    {
                        BossHeadSpit();
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossHeadSpit)
                {
                    if (reanimation.ShouldTriggerTimedEvent(0.37f))
                    {
                        BossHeadSpitEffect();
                    }
                    if (reanimation.ShouldTriggerTimedEvent(0.42f))
                    {
                        BossHeadSpitContact();
                    }
                    if (reanimation.mLoopCount > 0)
                    {
                        reanimation2 = mApp.ReanimationTryToGet(mSpecialHeadReanimID);
                        reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 20, 18f);
                        mZombiePhase = ZombiePhase.BossHeadIdleAfterSpit;
                        PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.Loop, 0, 12f);
                        mPhaseCounter = 300;
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossHeadIdleAfterSpit)
                {
                    if (mBodyHealth == 1)
                    {
                        BossStartDeath();
                        return;
                    }
                    if (mPhaseCounter <= 0)
                    {
                        mZombiePhase = ZombiePhase.BossHeadLeave;
                        PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_leave, ReanimLoopType.PlayOnceAndHold, 0, 12f);
                        return;
                    }
                }
                else if (mZombiePhase == ZombiePhase.BossHeadLeave)
                {
                    if (reanimation.ShouldTriggerTimedEvent(0.23f))
                    {
                        mChilledCounter = 0;
                        UpdateAnimSpeed();
                    }
                    if (reanimation.ShouldTriggerTimedEvent(0.48f) || reanimation.ShouldTriggerTimedEvent(0.8f))
                    {
                        mApp.PlayFoley(FoleyType.Thump);
                    }
                    if (reanimation.mLoopCount > 0)
                    {
                        ApplyBossSmokeParticles(false);
                        BossPlayIdle();
                        return;
                    }
                }
                else
                {
                    Debug.ASSERT(false);
                }
                return;
            }
            if (mBodyHealth == 1)
            {
                PlayDeathAnim(0U);
                return;
            }
            if (mPhaseCounter > 0)
            {
                return;
            }
            int bodyDamageIndex = GetBodyDamageIndex();
            if (bodyDamageIndex != mBossMode)
            {
                mBossMode = bodyDamageIndex;
                if (mBossMode == 1)
                {
                    BossBungeeAttack();
                    return;
                }
                BossRVAttack();
                return;
            }
            else
            {
                if (mBossStompCounter == 0)
                {
                    BossStompAttack();
                    return;
                }
                if (mBossBungeeCounter == 0)
                {
                    int ceiling;
                    if (mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
                    {
                        ceiling = 4;
                    }
                    else
                    {
                        ceiling = 2;
                    }
                    if (RandomNumbers.NextNumber(ceiling) == 0)
                    {
                        mBossBungeeCounter = TodCommon.RandRangeInt(4000, 5000);
                        BossRVAttack();
                        return;
                    }
                    BossBungeeAttack();
                    return;
                }
                else
                {
                    if (mBossHeadCounter <= 0)
                    {
                        BossHeadAttack();
                        return;
                    }
                    if (mSummonCounter <= 0)
                    {
                        BossSpawnAttack();
                        return;
                    }
                    mPhaseCounter = TodCommon.RandRangeInt(100, 200);
                    return;
                }
            }
        }

        public void BossPlayIdle()
        {
            mZombiePhase = ZombiePhase.BossIdle;
            mPhaseCounter = TodCommon.RandRangeInt(100, 200);
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, 6f);
        }

        public void BossRVLanding()
        {
            for (int i = 0; i < mBoard.mPlants.Count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead && aPlant.mRow >= mTargetRow && aPlant.mRow <= mTargetRow + 1 && aPlant.mPlantCol >= mTargetCol && aPlant.mPlantCol <= mTargetCol + 2)
                {
                    aPlant.Squish();
                }
            }
            mBoard.ShakeBoard(1, 2);
            mApp.PlaySample(Resources.SOUND_RVTHROW);
            mApp.Vibrate();
            mSummonCounter = 500;
            mBossHeadCounter = 5000;
            if (mBossMode >= 1)
            {
                mBossStompCounter = 4000;
            }
            if (mBossMode >= 2)
            {
                mBossBungeeCounter = 6500;
            }
        }

        public void BossStompContact()
        {
            for (int i = 0; i < mBoard.mPlants.Count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead && aPlant.mRow >= mTargetRow && aPlant.mRow <= mTargetRow + 1 && aPlant.mPlantCol >= 5)
                {
                    aPlant.Squish();
                }
            }
            mBoard.ShakeBoard(1, 4);
            mApp.PlayFoley(FoleyType.Thump);
            mApp.Vibrate();
        }

        public bool BossAreBungeesDone()
        {
            int num_v = 0;
            for (int i = 0; i < 3; i++)
            {
                Zombie aZombie_v = mBoard.ZombieTryToGet(mFollowerZombieID[i]);
                if (aZombie_v != null)
                {
                    if (aZombie_v.mZombiePhase == ZombiePhase.BungeeRising)
                    {
                        return true;
                    }
                    num_v++;
                }
            }
            return num_v == 0;
        }

        public void BossBungeeSpawn()
        {
            mZombiePhase = ZombiePhase.BossBungeesDrop;
            for (int i = 0; i < 3; i++)
            {
                Zombie zombie = mBoard.AddZombieInRow(ZombieType.Bungee, 0, 0);
                zombie.PickBungeeZombieTarget(mTargetCol + i);
                zombie.mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, zombie.mRow, 7);
                zombie.mAltitude = zombie.mPosY - 30f;
                mFollowerZombieID[i] = mBoard.ZombieGetID(zombie);
            }
        }

        public void BossSpawnAttack()
        {
            RemoveColdEffects();
            mZombiePhase = ZombiePhase.BossSpawning;
            if (mBossMode == 0)
            {
                mSummonCounter = TodCommon.RandRangeInt(450, 550);
            }
            else if (mBossMode == 1)
            {
                mSummonCounter = TodCommon.RandRangeInt(350, 450);
            }
            else if (mBossMode == 2)
            {
                mSummonCounter = TodCommon.RandRangeInt(150, 250);
            }
            mTargetRow = mBoard.PickRowForNewZombie(ZombieType.Normal);
            string text = string.Empty;
            switch (mTargetRow)
            {
                case 0:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_spawn_1;
                    break;
                case 1:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_spawn_2;
                    break;
                case 2:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_spawn_3;
                    break;
                case 3:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_spawn_4;
                    break;
                case 4:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_spawn_5;
                    break;
                default:
                    Debug.ASSERT(false);
                    break;
            }
            PlayZombieReanim(ref text, ReanimLoopType.PlayOnceAndHold, 20, 12f);
            mApp.PlayFoley(FoleyType.HydraulicShort);
        }

        public void BossBungeeAttack()
        {
            RemoveColdEffects();
            mZombiePhase = ZombiePhase.BossBungeesEnter;
            mBossBungeeCounter = TodCommon.RandRangeInt(4000, 5000);
            mTargetCol = TodCommon.RandRangeInt(0, 2);
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_bungee_1_enter, ReanimLoopType.PlayOnceAndHold, 20, 12f);
            mApp.PlayFoley(FoleyType.HydraulicShort);
            mApp.PlayFoley(FoleyType.BungeeScream);
        }

        public void BossRVAttack()
        {
            RemoveColdEffects();
            mZombiePhase = ZombiePhase.BossDropRv;
            mTargetRow = TodCommon.RandRangeInt(0, 3);
            mTargetCol = TodCommon.RandRangeInt(0, 2);
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_rv_1, ReanimLoopType.PlayOnceAndHold, 20, 16f);
            mApp.PlayFoley(FoleyType.HydraulicShort);
        }

        public void BossSpawnContact()
        {
            ZombieType theZombieType;
            if (mZombieAge < 3500)
            {
                theZombieType = ZombieType.Normal;
            }
            else if (mZombieAge < 8000)
            {
                theZombieType = ZombieType.TrafficCone;
            }
            else if (mZombieAge < 12500)
            {
                theZombieType = ZombieType.Pail;
            }
            else
            {
                int num = GameConstants.gBossZombieList.Length;
                if (mTargetRow == 0)
                {
                    Debug.ASSERT(GameConstants.gBossZombieList[num - 1] == ZombieType.Gargantuar);
                    num--;
                }
                theZombieType = GameConstants.gBossZombieList[RandomNumbers.NextNumber(num)];
            }
            Zombie zombie = mBoard.AddZombieInRow(theZombieType, mTargetRow, 0);
            zombie.mPosX = 600f + mPosX;
        }

        public void BossBungeeLeave()
        {
            mZombiePhase = ZombiePhase.BossBungeesLeave;
            for (int i = 0; i < 3; i++)
            {
                Zombie zombie = mBoard.ZombieTryToGet(mFollowerZombieID[i]);
                if (zombie != null && zombie.mButteredCounter > 0)
                {
                    zombie.DieWithLoot();
                }
            }
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_bungee_1_leave, ReanimLoopType.PlayOnceAndHold, 20, 18f);
        }

        public void BossStompAttack()
        {
            RemoveColdEffects();
            mZombiePhase = ZombiePhase.BossStomping;
            mBossStompCounter = TodCommon.RandRangeInt(5500, 6500);
            int num = 0;
            int[] aRowArray = new int[4];
            for (int i = 0; i < 4; i++)
            {
                if (BossCanStompRow(i))
                {
                    aRowArray[num] = i;
                    num++;
                }
            }
            if (num == 0)
            {
                return;
            }
            mTargetRow = TodCommon.TodPickFromArray(aRowArray, num);
            string text = string.Empty;
            switch (mTargetRow)
            {
                case 0:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_stomp_1;
                    break;
                case 1:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_stomp_2;
                    break;
                case 2:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_stomp_3;
                    break;
                case 3:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_stomp_4;
                    break;
                default:
                    Debug.ASSERT(false);
                    break;
            }
            PlayZombieReanim(ref text, ReanimLoopType.PlayOnceAndHold, 20, 12f);
            mApp.PlayFoley(FoleyType.HydraulicShort);
        }

        public bool BossCanStompRow(int theRow)
        {
            for (int i = 0; i < mBoard.mPlants.Count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead && !aPlant.NotOnGround() && aPlant.mRow >= theRow && aPlant.mRow <= theRow + 1 && aPlant.mPlantCol >= 5)
                {
                    return true;
                }
            }
            return false;
        }

        public void BossDie()
        {
            if (!IsOnBoard())
            {
                return;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mBossFireBallReanimID);
            if (reanimation != null)
            {
                reanimation.ReanimationDie();
                mBossFireBallReanimID = null;
                BossDestroyIceballInRow(mFireballRow);
                BossDestroyFireball();
            }
            mApp.mMusic.FadeOut(200);
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && zombie != this && !zombie.IsDeadOrDying())
                {
                    zombie.DieWithLoot();
                }
            }
            RemoveColdEffects();
        }

        public void BossHeadAttack()
        {
            mZombiePhase = ZombiePhase.BossHeadEnter;
            mBossHeadCounter = TodCommon.RandRangeInt(4000, 5000);
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_enter, ReanimLoopType.PlayOnceAndHold, 20, 12f);
            mApp.PlayFoley(FoleyType.HydraulicShort);
        }

        public void BossHeadSpitContact()
        {
            Debug.ASSERT(mApp.ReanimationTryToGet(mBossFireBallReanimID) == null);
            float aPosX = 550f + mPosX;
            float aPosY = mBoard.GetPosYBasedOnRow(aPosX, mFireballRow) + GameConstants.BOSS_BALL_OFFSET_Y;
            int aRenderOrder = mRenderOrder + 1;
            Reanimation reanimation;
            if (mIsFireBall)
            {
                aPosX -= 95f;
                reanimation = mApp.AddReanimation(aPosX, aPosY, aRenderOrder, ReanimationType.BossFireball);
                reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_form, ReanimLoopType.PlayOnceAndHold, 0, 16f);
                reanimation.mIsAttachment = true;
                reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_additive, GameConstants.RENDER_GROUP_BOSS_FIREBALL_ADDITIVE);
                reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_superglow, GameConstants.RENDER_GROUP_BOSS_FIREBALL_ADDITIVE);
            }
            else
            {
                aPosX -= 95f;
                reanimation = mApp.AddReanimation(aPosX, aPosY, aRenderOrder, ReanimationType.BossIceball);
                reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_form, ReanimLoopType.PlayOnceAndHold, 0, 16f);
                reanimation.mIsAttachment = true;
                reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_ice_highlight, GameConstants.RENDER_GROUP_BOSS_FIREBALL_ADDITIVE);
            }
            mBossFireBallReanimID = mApp.ReanimationGetID(reanimation);
            mApp.ReanimationTryToGet(mSpecialHeadReanimID).PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_laugh, ReanimLoopType.Loop, 20, 18f);
            mApp.PlayFoley(FoleyType.HydraulicShort);
        }

        public void BossHeadSpit()
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBossFireBallReanimID);
            if (reanimation != null)
            {
                reanimation.ReanimationDie();
                mBossFireBallReanimID = null;
            }
            mZombiePhase = ZombiePhase.BossHeadSpit;
            mFireballRow = TodCommon.RandRangeInt(0, 4);
            mIsFireBall = (TodCommon.RandRangeInt(0, 1) == 0);
            string text = string.Empty;
            switch (mFireballRow)
            {
                case 0:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_head_attack_1;
                    break;
                case 1:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_head_attack_2;
                    break;
                case 2:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_head_attack_3;
                    break;
                case 3:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_head_attack_4;
                    break;
                case 4:
                    text = GlobalMembersReanimIds.ReanimTrackId_anim_head_attack_5;
                    break;
                default:
                    Debug.ASSERT(false);
                    break;
            }
            PlayZombieReanim(ref text, ReanimLoopType.PlayOnceAndHold, 20, 12f);
            Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
            Image theImage = null;
            if (mIsFireBall)
            {
                reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_eyeglow_red, theImage);
                reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_mouthglow_red, theImage);
            }
            else
            {
                reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_eyeglow_red, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_EYEGLOW_BLUE);
                reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_mouthglow_red, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_MOUTHGLOW_BLUE);
            }
            Reanimation reanimation3 = mApp.ReanimationTryToGet(mSpecialHeadReanimID);
            reanimation3.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_drive, ReanimLoopType.Loop, 20, 36f);
        }

        public void UpdateBossFireball()//3update
        {
            Reanimation aBossFireballReanim = mApp.ReanimationTryToGet(mBossFireBallReanimID);
            if (aBossFireballReanim == null)
            {
                return;
            }
            float aSpeed = aBossFireballReanim.GetTrackVelocity(Reanimation.ReanimTrackId__ground);
            float aBallPosX = aBossFireballReanim.mOverlayMatrix.mMatrix.M41 * Constants.IS;
            aBallPosX -= aSpeed;
            aBossFireballReanim.mOverlayMatrix.mMatrix.M41 = aBallPosX * Constants.S;
            float aBallPosY = mBoard.GetPosYBasedOnRow(aBallPosX + 75f, mFireballRow) + GameConstants.BOSS_BALL_OFFSET_Y;
            aBossFireballReanim.mOverlayMatrix.mMatrix.M42 = aBallPosY * Constants.S;
            if (aBallPosX < -180f + Constants.BOARD_EXTRA_ROOM)
            {
                aBossFireballReanim.ReanimationDie();
                mBossFireBallReanimID = null;
            }
            int theX = mBoard.PixelToGridX((int)aBallPosX + 75, (int)aBallPosY);
            SquishAllInSquare(theX, mFireballRow, ZombieAttackType.DriveOver);
            foreach (LawnMower lawnMower in mBoard.mLawnMowers)
            {
                if (!lawnMower.mDead && lawnMower.mMowerState != LawnMowerState.Squished && lawnMower.mRow == mFireballRow && lawnMower.mPosX > aBallPosX && lawnMower.mPosX < aBallPosX + 50f)
                {
                    lawnMower.SquishMower();
                }
            }
            if (mIsFireBall)
            {
                if (aBossFireballReanim.mLoopType == ReanimLoopType.PlayOnceAndHold && aBossFireballReanim.mLoopCount > 0)
                {
                    aBossFireballReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_role, ReanimLoopType.Loop, 0, 2f);
                    aBossFireballReanim.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, mFireballRow, 0);
                }
                if (aBossFireballReanim.mLoopType == ReanimLoopType.Loop && RandomNumbers.NextNumber(10) == 0)
                {
                    float aPosX1 = aBallPosX + 100f + TodCommon.RandRangeFloat(0f, 20f);
                    float aPosY1 = mBoard.GetPosYBasedOnRow(aPosX1 - 40f, mFireballRow) + 90f + TodCommon.RandRangeFloat(-50f, 0f);
                    mApp.AddTodParticle(aPosX1,
                                        aPosY1,
                                        Board.MakeRenderOrder(RenderLayer.GraveStone, mFireballRow, 6),
                                        ParticleEffect.FireballTrail);
                }
            }
            else
            {
                if (aBossFireballReanim.mLoopType == ReanimLoopType.PlayOnceAndHold && aBossFireballReanim.mLoopCount > 0)
                {
                    aBossFireballReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_role, ReanimLoopType.Loop, 0, 2f);
                    aBossFireballReanim.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, mFireballRow, 0);
                }
                if (aBossFireballReanim.mLoopType == ReanimLoopType.Loop && RandomNumbers.NextNumber(10) == 0)
                {
                    float aPosX2 = aBallPosX + 100f + TodCommon.RandRangeFloat(0f, 20f);
                    float aPosY2 = mBoard.GetPosYBasedOnRow(aPosX2 - 40f, mFireballRow) + 90f + TodCommon.RandRangeFloat(-50f, 0f);
                    mApp.AddTodParticle(aPosX2,
                                        aPosY2,
                                        Board.MakeRenderOrder(RenderLayer.GraveStone, mFireballRow, 6),
                                        ParticleEffect.IceballTrail);
                }
            }
            aBossFireballReanim.Update();
        }

        public void BossDestroyFireball()
        {
            Reanimation aReanimation_v = mApp.ReanimationTryToGet(mBossFireBallReanimID);
            if (aReanimation_v == null)
            {
                return;
            }
            if (!mIsFireBall)
            {
                return;
            }
            float aPosX = aReanimation_v.mOverlayMatrix.mMatrix.M41 * Constants.IS + 80f;
            float aPosY = aReanimation_v.mOverlayMatrix.mMatrix.M42 * Constants.IS + 40f;
            for (int i = 0; i < 6; i++)
            {
                float aAngle = 1.5707964f + 6.2831855f * i / 6f;
                float theX = aPosX + 60f * (float)Math.Sin(aAngle);
                float theY = aPosY + 60f * (float)Math.Cos(aAngle);
                Reanimation aFireBallReanim = mApp.AddReanimation(theX, theY, 400000, ReanimationType.JalapenoFire);
                aFireBallReanim.mLoopType = ReanimLoopType.PlayOnceFullLastFrame;
                aFireBallReanim.mAnimTime = 0.2f;
                aFireBallReanim.mAnimRate = TodCommon.RandRangeFloat(20f, 25f);
            }
            aReanimation_v.ReanimationDie();
            mBossFireBallReanimID = null;
            mBoard.RemoveParticleByType(ParticleEffect.FireballTrail);
        }

        public void BossDestroyIceballInRow(int theRow)
        {
            if (theRow != mFireballRow)
            {
                return;
            }
            Reanimation aReanimation_v = mApp.ReanimationTryToGet(mBossFireBallReanimID);
            if (aReanimation_v == null)
            {
                return;
            }
            if (mIsFireBall)
            {
                return;
            }
            float theX = aReanimation_v.mOverlayMatrix.mMatrix.M41 * Constants.IS + 80f;
            float theY = aReanimation_v.mOverlayMatrix.mMatrix.M42 * Constants.IS + 80f;
            mApp.AddTodParticle(theX, theY, 400000, ParticleEffect.IceballDeath);
            aReanimation_v.ReanimationDie();
            mBossFireBallReanimID = null;
            mBoard.RemoveParticleByType(ParticleEffect.IceballTrail);
        }

        public void DiggerLoseAxe()
        {
            if (mZombiePhase == ZombiePhase.DiggerTunneling)
            {
                mZombiePhase = ZombiePhase.DiggerTunnelingPauseWithoutAxe;
                mPhaseCounter = 200;
                SetAnimRate(0f);
                UpdateAnimSpeed();
                GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.DiggerTunnel, null);
                StopZombieSound();
            }
            mHasObject = false;
            ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_pickaxe, -1);
            ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_dirt, -1);
        }

        public void BungeeDropZombie(Zombie theDroppedZombie, int theGridX, int theGridY)
        {
            mTargetCol = theGridX;
            SetRow(theGridY);
            mPosX = mBoard.GridToPixelX(mTargetCol, mRow);
            mPosY = GetPosYBasedOnRow(mRow);
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_raise, ReanimLoopType.PlayOnceAndHold, 0, 36f);
            mRelatedZombieID = mBoard.ZombieGetID(theDroppedZombie);
            theDroppedZombie.mPosX = mPosX - 15f;
            theDroppedZombie.SetRow(theGridY);
            theDroppedZombie.mPosY = GetPosYBasedOnRow(theGridY);
            theDroppedZombie.mZombieHeight = ZombieHeight.GettingBungeeDropped;
            theDroppedZombie.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, 0f);
            theDroppedZombie.mRenderOrder = mRenderOrder + 1;
        }

        private Image GetYuckyFaceImage()
        {
            if (mZombieType == ZombieType.Dancer)
            {
                return AtlasResources.IMAGE_REANIM_ZOMBIE_DISCO_HEAD_GROSSOUT;
            }
            if (mZombieType == ZombieType.BackupDancer)
            {
                return AtlasResources.IMAGE_REANIM_ZOMBIE_DANCER_HEAD_GROSSOUT;
            }
            if (mZombieType == ZombieType.Polevaulter)
            {
                return AtlasResources.IMAGE_REANIM_ZOMBIE_POLEVAULTER_HEAD_GROSSOUT;
            }
            return AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_GROSSOUT;
        }

        public void ShowYuckyFace(bool theShow)
        {
            Reanimation reanimation_v = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation_v == null)
            {
                return;
            }
            if (HasYuckyFaceImage())
            {
                if (theShow)
                {
                    reanimation_v.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, GetYuckyFaceImage());
                    reanimation_v.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head2, -1);
                    reanimation_v.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head_jaw, -1);
                    reanimation_v.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_tongue, -1);
                    return;
                }
                if (mHasHead)
                {
                    reanimation_v.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, null);
                    reanimation_v.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head2, 0);
                    reanimation_v.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head_jaw, 0);
                    if (mVariant)
                    {
                        reanimation_v.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_tongue, 0);
                    }
                }
            }
        }

        public void AnimateChewSound()
        {
            if (mZombiePhase == ZombiePhase.SnorkelUpToEat)
            {
                return;
            }
            Plant plant = FindPlantTarget(ZombieAttackType.Chew);
            if (plant != null)
            {
                if (plant.mSeedType == SeedType.Hypnoshroom && !plant.mIsAsleep)
                {
                    mApp.PlayFoley(FoleyType.Floop);
                    plant.Die();
                    StartMindControlled();
                    mApp.AddTodParticle(mPosX + 60f, mPosY + 40f, mRenderOrder + 1, ParticleEffect.MindControl);
                    TrySpawnLevelAward();
                    mVelX = 0.17f;
                    mAnimTicksPerFrame = 18;
                    UpdateAnimSpeed();
                    return;
                }
                if (plant.mSeedType == SeedType.Garlic)
                {
                    if (!mYuckyFace)
                    {
                        mYuckyFace = true;
                        mYuckyFaceCounter = 0;
                        UpdateAnimSpeed();
                        mApp.PlayFoley(FoleyType.Chomp);
                        return;
                    }
                }
                else
                {
                    if (plant.mSeedType == SeedType.Wallnut || plant.mSeedType == SeedType.Tallnut || plant.mSeedType == SeedType.Garlic || plant.mSeedType == SeedType.Pumpkinshell)
                    {
                        mApp.PlayFoley(FoleyType.ChompSoft);
                        return;
                    }
                    mApp.PlayFoley(FoleyType.Chomp);
                    return;
                }
            }
            else
            {
                if (mMindControlled)
                {
                    mApp.PlayFoley(FoleyType.ChompSoft);
                    return;
                }
                mApp.PlayFoley(FoleyType.Chomp);
            }
        }

        public void AnimateChewEffect()
        {
            if (mZombiePhase == ZombiePhase.SnorkelUpToEat)
            {
                return;
            }
            if (mApp.IsIZombieLevel())
            {
                GridItem gridItem = mBoard.mChallenge.IZombieGetBrainTarget(this);
                if (gridItem != null)
                {
                    gridItem.mTransparentCounter = Math.Max(gridItem.mTransparentCounter, 25);
                    return;
                }
            }
            Plant plant = FindPlantTarget(ZombieAttackType.Chew);
            if (plant == null)
            {
                return;
            }
            if (plant.mSeedType == SeedType.Wallnut || plant.mSeedType == SeedType.Tallnut)
            {
                int aRenderOrder = Board.MakeRenderOrder(RenderLayer.Projectile, mRow, 0);
                ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
                GetDrawPos(ref zombieDrawPosition);
                float num = mPosX + 37f;
                float num2 = mPosY + 40f + zombieDrawPosition.mBodyY;
                if (mZombieType == ZombieType.Snorkel || mZombieType == ZombieType.DolphinRider)
                {
                    num -= 7f;
                    num2 += 70f;
                }
                else if (IsWalkingBackwards())
                {
                    num += 47f;
                }
                else if (mZombieType == ZombieType.Balloon)
                {
                    num2 += 47f;
                }
                else if (mZombieType == ZombieType.Imp)
                {
                    num += 24f;
                    num2 += 40f;
                }
                mApp.AddTodParticle(num, num2, aRenderOrder, ParticleEffect.WallnutEatSmall);
            }
            plant.mEatenFlashCountdown = Math.Max(plant.mEatenFlashCountdown, 25);
        }

        public void UpdateActions()//3update
        {
            if (mZombieHeight == ZombieHeight.UpLadder)
            {
                //UpdateClimbingLadder();
                //UpdateClimbingLadder();
                UpdateClimbingLadder();
            }
            if (mZombieHeight == ZombieHeight.Zombiquarium)
            {
                UpdateZombiquarium();
            }
            if (mZombieHeight == ZombieHeight.OutOfPool || mZombieHeight == ZombieHeight.InToPool || mInPool)
            {
                UpdateZombiePool();
            }
            if (mZombieHeight == ZombieHeight.UpToHighGround || mZombieHeight == ZombieHeight.DownOffHighGround)
            {
                UpdateZombieHighGround();
            }
            if (mZombieHeight == ZombieHeight.Falling)
            {
                UpdateZombieFalling();
            }
            if (mZombieHeight == ZombieHeight.InToChimney)
            {
                UpdateZombieChimney();
            }
            if (mZombieType == ZombieType.Polevaulter)
            {
                UpdateZombiePolevaulter();
            }
            if (mZombieType == ZombieType.Catapult)
            {
                UpdateZombieCatapult();
            }
            if (mZombieType == ZombieType.DolphinRider)
            {
                UpdateZombieDolphinRider();
            }
            if (mZombieType == ZombieType.Snorkel)
            {
                UpdateZombieSnorkel();
            }
            if (mZombieType == ZombieType.Balloon)
            {
                UpdateZombieFlyer();
            }
            if (mZombieType == ZombieType.Newspaper)
            {
                UpdateZombieNewspaper();
            }
            if (mZombieType == ZombieType.Digger)
            {
                UpdateZombieDigger();
            }
            if (mZombieType == ZombieType.JackInTheBox)
            {
                UpdateZombieJackInTheBox();
            }
            if (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
            {
                UpdateZombieGargantuar();
            }
            if (mZombieType == ZombieType.Bobsled)
            {
                UpdateZombieBobsled();
            }
            if (mZombieType == ZombieType.Zamboni)
            {
                UpdateZamboni();
            }
            if (mZombieType == ZombieType.Ladder)
            {
                UpdateLadder();
            }
            if (mZombieType == ZombieType.Yeti)
            {
                UpdateYeti();
            }
            if (mZombieType == ZombieType.Dancer)
            {
                //UpdateZombieDancer();
                //UpdateZombieDancer();
                UpdateZombieDancer();
            }
            if (mZombieType == ZombieType.BackupDancer)
            {
                //UpdateZombieBackupDancer();
                //UpdateZombieBackupDancer();
                UpdateZombieBackupDancer();
            }
            if (mZombieType == ZombieType.Imp)
            {
                //UpdateZombieImp();
                //UpdateZombieImp();
                UpdateZombieImp();
            }
            if (mZombieType == ZombieType.PeaHead)
            {
                UpdateZombiePeaHead();
            }
            if (mZombieType == ZombieType.JalapenoHead)
            {
                UpdateZombieJalapenoHead();
            }
            if (mZombieType == ZombieType.GatlingHead)
            {
                UpdateZombieGatlingHead();
            }
            if (mZombieType == ZombieType.SquashHead)
            {
                UpdateZombieSquashHead();
            }
        }

        public void CheckForBoardEdge()//3update
        {
            if (mZombieType == ZombieType.Boss)
            {
                return;
            }
            if (IsWalkingBackwards() && mPosX > 900f)
            {
                DieNoLoot(false);
                return;
            }
            int board_EDGE = Constants.BOARD_EDGE;
            if (mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar)
            {
                board_EDGE = Constants.BOARD_EDGE;
            }
            else if (mZombieType == ZombieType.Polevaulter)
            {
                board_EDGE = Constants.BOARD_EDGE;
            }
            else if (mZombieType == ZombieType.Catapult || mZombieType == ZombieType.Football || mZombieType == ZombieType.Zamboni)
            {
                board_EDGE = Constants.BOARD_EDGE;
            }
            else if (mZombieType == ZombieType.Zamboni)
            {
                board_EDGE = Constants.BOARD_EDGE;
            }
            else if (mZombieType == ZombieType.BackupDancer || mZombieType == ZombieType.Dancer || mZombieType == ZombieType.Snorkel)
            {
                board_EDGE = Constants.BOARD_EDGE;
            }
            if (mX <= board_EDGE && mHasHead)
            {
                if (mApp.IsIZombieLevel())
                {
                    DieNoLoot(false);
                }
                else
                {
                    mBoard.ZombiesWon(this);
                }
            }
            if (mX <= board_EDGE + 70 && !mHasHead)
            {
                TakeDamage(1800, 9U, false);
            }
        }

        public void UpdateYeti()//3update
        {
            if (mMindControlled || !mHasHead || IsDeadOrDying())
            {
                return;
            }
            if (IsOnBoard() && mX < 800)
            {
                mBoard.GrantAchievement(AchievementId.Zombologist, true);
            }
            if (mZombiePhase == ZombiePhase.ZombieNormal && mPhaseCounter <= 0)
            {
                mZombiePhase = ZombiePhase.YetiRunning;
                mHasObject = false;
                PickRandomSpeed();
            }
        }

        public void DrawBossPart(Graphics g, BossPart theBossPart)
        {
            ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
            GetDrawPos(ref zombieDrawPosition);
            switch (theBossPart)
            {
                case BossPart.BackLeg:
                    DrawReanim(g, ref zombieDrawPosition, GameConstants.RENDER_GROUP_BOSS_BACK_LEG);
                    return;
                case BossPart.FrontLeg:
                    DrawReanim(g, ref zombieDrawPosition, GameConstants.RENDER_GROUP_BOSS_FRONT_LEG);
                    return;
                case BossPart.Main:
                    DrawReanim(g, ref zombieDrawPosition, 0);
                    return;
                case BossPart.BackArm:
                    DrawBossBackArm(g, ref zombieDrawPosition);
                    return;
                case BossPart.Fireball:
                    DrawBossFireBall(g, ref zombieDrawPosition);
                    return;
                default:
                    return;
            }
        }

        public void BossSetupReanim()
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            reanimation.AssignRenderGroupToPrefix("Boss_innerleg", GameConstants.RENDER_GROUP_BOSS_BACK_LEG);
            reanimation.AssignRenderGroupToPrefix("Boss_outerleg", GameConstants.RENDER_GROUP_BOSS_FRONT_LEG);
            reanimation.AssignRenderGroupToPrefix("Boss_body2", GameConstants.RENDER_GROUP_BOSS_FRONT_LEG);
            reanimation.AssignRenderGroupToPrefix("Boss_innerarm", GameConstants.RENDER_GROUP_BOSS_BACK_ARM);
            reanimation.AssignRenderGroupToPrefix("Boss_RV", GameConstants.RENDER_GROUP_BOSS_BACK_ARM);
            Reanimation reanimation2 = mApp.AddReanimation(0f, 0f, 0, ReanimationType.BossDriver);
            reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 0, 12f);
            mSpecialHeadReanimID = mApp.ReanimationGetID(reanimation2);
            ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_boss_head2);
            AttachEffect attachEffect = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName.mAttachmentID, reanimation2, 25f * Constants.S, -70f * Constants.S);
            reanimation.mFrameBasePose = 0;
            attachEffect.mDontDrawIfParentHidden = true;
            attachEffect.mOffset.Scale(1.2f, 1.2f);
        }

        public void MowDown()
        {
            if (mDead || mZombiePhase == ZombiePhase.ZombieMowered)
            {
                return;
            }
            if (mZombieType == ZombieType.Boss)
            {
                return;
            }
            if (mZombieType == ZombieType.Catapult)
            {
                mApp.AddTodParticle(mPosX + 80f, mPosY + 60f, mRenderOrder + 1, ParticleEffect.CatapultExplosion);
                mApp.PlayFoley(FoleyType.Explosion);
                DieWithLoot();
                if (!mBoard.AreEnemyZombiesOnScreen() && mBoard.mCurrentWave == mBoard.mNumWaves && (mApp.mGameMode != GameMode.ChallengeWhackAZombie || mApp.mBoard.mChallenge.mChallengeState == ChallengeState.Normal))
                {
                    mBoard.GrantAchievement(AchievementId.LastMownStanding, true);
                }
                return;
            }
            if (mZombieType == ZombieType.Zamboni)
            {
                mApp.AddTodParticle(mPosX + 80f, mPosY + 60f, mRenderOrder + 1, ParticleEffect.ZamboniExplosion);
                mApp.PlayFoley(FoleyType.Explosion);
                DieWithLoot();
                return;
            }
            if (mZombiePhase == ZombiePhase.ZombieDying || mZombiePhase == ZombiePhase.PolevaulterInVault || mZombiePhase == ZombiePhase.RisingFromGrave || mZombiePhase == ZombiePhase.DancerRising || mZombiePhase == ZombiePhase.SnorkelIntoPool || mZombiePhase == ZombiePhase.ZombieBurned || mZombieType == ZombieType.Gargantuar || mZombieType == ZombieType.RedeyeGargantuar || mZombieType == ZombieType.Bungee || mZombieType == ZombieType.Digger || mZombieType == ZombieType.Imp || mZombieType == ZombieType.Yeti || mZombieType == ZombieType.DolphinRider || IsBobsledTeamWithSled() || IsFlying() || mInPool)
            {
                Reanimation reanimation = mApp.AddReanimation(mPosX - 73f, mPosY - 56f, mRenderOrder + 2, ReanimationType.Puff);
                reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_puff);
                mApp.AddTodParticle(mPosX + 110f, mPosY + 0f, mRenderOrder + 1, ParticleEffect.MowerCloud);
                if (mBoard.mPlantRow[mRow] != PlantRowType.Pool)
                {
                    DropHead(0U);
                    DropArm(0U);
                    DropHelm(0U);
                    DropShield(0U);
                }
                DieWithLoot();
                return;
            }
            if (mIceTrapCounter > 0)
            {
                RemoveIceTrap();
            }
            if (mButteredCounter > 0)
            {
                mButteredCounter = 0;
            }
            DropShield(0U);
            DropHelm(0U);
            if (mZombieType == ZombieType.Flag)
            {
                DropFlag();
            }
            else if (mZombieType == ZombieType.Polevaulter)
            {
                DropPole();
            }
            else if (mZombieType == ZombieType.Newspaper || mZombieType == ZombieType.Balloon)
            {
                DropHead(0U);
            }
            else if (mZombieType == ZombieType.Pogo)
            {
                DropHead(0U);
                mAltitude = 0f;
            }
            Reanimation reanimation2 = mApp.AddReanimation(0f, 0f, mRenderOrder, ReanimationType.LawnMoweredZombie);
            reanimation2.mIsAttachment = false;
            reanimation2.mLoopType = ReanimLoopType.PlayOnceAndHold;
            reanimation2.mAnimRate = 8f;
            mMoweredReanimID = mApp.ReanimationGetID(reanimation2);
            mZombiePhase = ZombiePhase.ZombieMowered;
            DropLoot();
            mBoard.AreEnemyZombiesOnScreen();
        }

        public void UpdateMowered()//3update
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mMoweredReanimID);
            if (reanimation == null || reanimation.mLoopCount > 0)
            {
                DropHead(0U);
                DropArm(0U);
                DieWithLoot();
            }
        }

        public void DropFlag()
        {
            if (mZombieType != ZombieType.Flag || !mHasObject)
            {
                return;
            }
            DetachFlag();
            mApp.RemoveReanimation(ref mSpecialHeadReanimID);
            mSpecialHeadReanimID = null;
            ReanimShowPrefix("anim_innerarm", 0);
            ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand, -1);
            ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm_screendoor, -1);
            mHasObject = false;
            float aFlagPosX = 0f;
            float aFlagPosY = 0f;
            GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand, ref aFlagPosX, ref aFlagPosY);
            TodParticleSystem aParticle = mApp.AddTodParticle(aFlagPosX + 6f, aFlagPosY - 45f, mRenderOrder + 1, ParticleEffect.ZombieFlag);
            OverrideParticleColor(aParticle);
            OverrideParticleScale(aParticle);
        }

        public void DropPole()
        {
            if (mZombieType != ZombieType.Polevaulter)
            {
                return;
            }
            ReanimShowPrefix("Zombie_polevaulter_innerarm", -1);
            ReanimShowPrefix("Zombie_polevaulter_innerhand", -1);
            ReanimShowPrefix("Zombie_polevaulter_pole", -1);
        }

        public void DrawBossBackArm(Graphics g, ref ZombieDrawPosition theDrawPos)
        {
            float num = 0f;
            float num2 = 0f;
            if (mZombiePhase == ZombiePhase.BossDropRv)
            {
                num2 = (mTargetRow - 1) * 85f - mTargetCol * 20f;
                num = mTargetCol * 80f;
                num *= Constants.S;
                num2 *= Constants.S;
            }
            else if (mZombiePhase == ZombiePhase.BossBungeesEnter || mZombiePhase == ZombiePhase.BossBungeesDrop || mZombiePhase == ZombiePhase.BossBungeesLeave)
            {
                num = mTargetCol * 80f - 23f;
                num *= Constants.S;
            }
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            Reanimation reanimation2 = reanimation;
            reanimation2.mOverlayMatrix.mMatrix.M41 = reanimation2.mOverlayMatrix.mMatrix.M41 + num;
            Reanimation reanimation3 = reanimation;
            reanimation3.mOverlayMatrix.mMatrix.M42 = reanimation3.mOverlayMatrix.mMatrix.M42 + num2;
            DrawReanim(g, ref theDrawPos, GameConstants.RENDER_GROUP_BOSS_BACK_ARM);
            Reanimation reanimation4 = reanimation;
            reanimation4.mOverlayMatrix.mMatrix.M41 = reanimation4.mOverlayMatrix.mMatrix.M41 - num;
            Reanimation reanimation5 = reanimation;
            reanimation5.mOverlayMatrix.mMatrix.M42 = reanimation5.mOverlayMatrix.mMatrix.M42 - num2;
        }

        public static void PreloadZombieResources(ZombieType theZombieType)
        {
            ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
            if (zombieDefinition.mReanimationType != ReanimationType.None)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(zombieDefinition.mReanimationType, true);
            }
            if (theZombieType == ZombieType.Digger)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.DiggerDirt, true);
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.ZombieCharredDigger, true);
            }
            if (theZombieType == ZombieType.Boss)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.BossDriver, true);
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.BossFireball, true);
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.BossIceball, true);
                for (int i = 0; i < GameConstants.gBossZombieList.Length; i++)
                {
                    ZombieDefinition zombieDefinition2 = Zombie.GetZombieDefinition(GameConstants.gBossZombieList[i]);
                    ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(zombieDefinition2.mReanimationType, true);
                }
            }
            if (theZombieType == ZombieType.Dancer)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.BackupDancer, true);
            }
            if (theZombieType == ZombieType.Gargantuar || theZombieType == ZombieType.RedeyeGargantuar)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.Imp, true);
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.ZombieCharredImp, true);
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.ZombieCharredGargantuar, true);
            }
            if (theZombieType == ZombieType.Zamboni)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.Imp, true);
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.ZombieCharredZamboni, true);
            }
            if (theZombieType == ZombieType.Catapult)
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.ZombieCharredCatapult, true);
            }
            ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.Puff, true);
            ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.ZombieCharred, true);
            ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.LawnMoweredZombie, true);
        }

        public void BossStartDeath()
        {
            mZombiePhase = ZombiePhase.BossHeadLeave;
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_leave, ReanimLoopType.PlayOnceAndHold, 0, 24f);
            mApp.AddTodParticle(700f, 150f, 400000, ParticleEffect.BossExplosion);
            mApp.PlaySample(Resources.SOUND_BOSSEXPLOSION);
            mApp.PlayFoley(FoleyType.Gargantudeath);
            BossDie();
        }

        public void RemoveColdEffects()
        {
            if (mIceTrapCounter > 0)
            {
                RemoveIceTrap();
            }
            if (mChilledCounter > 0)
            {
                mChilledCounter = 0;
                UpdateAnimSpeed();
            }
        }

        public void BossHeadSpitEffect()
        {
            int aRenderOrder = mRenderOrder + 2;
            if (mIsFireBall)
            {
                Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
                int theTrackIndex = reanimation.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_boss_jaw);
                ReanimatorTransform aTransform;
                reanimation.GetCurrentTransform(theTrackIndex, out aTransform, false);
                float aFlamePosX = mPosX + aTransform.mTransX * Constants.IS + 100f;
                float aFlamePosY = mPosY + aTransform.mTransY * Constants.IS + 50f;
                mApp.AddTodParticle(aFlamePosX, aFlamePosY, aRenderOrder, ParticleEffect.ZombieBossFireball);
                aTransform.PrepareForReuse();
            }
            else
            {
                Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
                int theTrackIndex2 = reanimation2.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_boss_jaw);
                ReanimatorTransform reanimatorTransform2;
                reanimation2.GetCurrentTransform(theTrackIndex2, out reanimatorTransform2, false);
                float theX2 = mPosX + reanimatorTransform2.mTransX * Constants.IS + 100f;
                float theY2 = mPosY + reanimatorTransform2.mTransY * Constants.IS + 50f;
                TodParticleSystem todParticleSystem = mApp.AddTodParticle(theX2, theY2, aRenderOrder, ParticleEffect.ZombieBossFireball);
                if (todParticleSystem != null)
                {
                    todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIE_BOSS_ICEBALL_PARTICLES);
                }
                reanimatorTransform2.PrepareForReuse();
            }
            mApp.PlayFoley(FoleyType.Bossboulderattack);
        }

        public void DrawBossFireBall(Graphics g, ref ZombieDrawPosition theDrawPos)
        {
            base.MakeParentGraphicsFrame(g);
            Reanimation reanimation = mApp.ReanimationTryToGet(mBossFireBallReanimID);
            if (reanimation == null)
            {
                return;
            }
            reanimation.DrawRenderGroup(g, 0);
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
            reanimation.DrawRenderGroup(g, GameConstants.RENDER_GROUP_BOSS_FIREBALL_ADDITIVE);
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            reanimation.DrawRenderGroup(g, GameConstants.RENDER_GROUP_BOSS_FIREBALL_TOP);
        }

        public void UpdateZombiePeaHead()//3update
        {
            if (!mHasHead)
            {
                return;
            }
            //if (mPhaseCounter >= 36 && mPhaseCounter < 39)
            if (mPhaseCounter == 36)
            {
                mApp.ReanimationGet(mSpecialHeadReanimID)?.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.PlayOnceAndHold, 20, 35f);
                return;
            }
            else if (mPhaseCounter <= 0)
            {
                mApp.ReanimationGet(mSpecialHeadReanimID)?.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.PlayOnceAndHold, 20, 15f);
                mApp.PlayFoley(FoleyType.Throw);
                Reanimation reanimation_v = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation_v != null)
                {
                    reanimation_v.GetCurrentTransform(
                        reanimation_v.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_anim_head1),
                        out ReanimatorTransform aTransForm,
                        false);
                    float aOriginX = mPosX + aTransForm.mTransX * Constants.IS - 9f;
                    float aOriginY = mPosY + aTransForm.mTransY * Constants.IS + 6f - mAltitude;
                    if (mMindControlled)
                    {
                        aOriginX += 90f * mScaleZombie;
                        mBoard.AddProjectile((int)aOriginX, (int)aOriginY, mRenderOrder, mRow, ProjectileType.ZombiePeaMindControl);
                    }
                    else
                    {
                        mBoard.AddProjectile((int)aOriginX, (int)aOriginY, mRenderOrder, mRow, ProjectileType.ZombiePea)
                            .mMotionType = ProjectileMotion.Backwards;
                    }
                    mPhaseCounter = 150;
                    aTransForm.PrepareForReuse();
                }
            }
        }

        public void BurnRow(int theRow)
        {
            int theDamageRangeFlags = 127;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (zombie != this && !zombie.mDead)
                {
                    int num = zombie.mRow - mRow;
                    if (zombie.mZombieType == ZombieType.Boss)
                    {
                        num = 0;
                    }
                    if (num == 0 && zombie.EffectedByDamage((uint)theDamageRangeFlags))
                    {
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
        }

        public void UpdateZombieJalapenoHead()//3update
        {
            if (!mHasHead)
            {
                return;
            }
            if (mPhaseCounter <= 0)
            {
                mApp.PlayFoley(FoleyType.JalapenoIgnite);
                mApp.PlayFoley(FoleyType.Juicy);
                mBoard.DoFwoosh(mRow);
                mBoard.ShakeBoard(3, -4);
                if (mMindControlled)
                {
                    BurnRow(mRow);
                    return;
                }
                int count = mBoard.mPlants.Count;
                for (int i = 0; i < count; i++)
                {
                    Plant aPlant = mBoard.mPlants[i];
                    if (!aPlant.mDead)
                    {
                        TRect aPlantRect = aPlant.GetPlantRect();
                        if (mRow == aPlant.mRow && !aPlant.NotOnGround())
                        {
                            mBoard.mPlantsEaten++;
                            aPlant.Die();
                        }
                    }
                }
                DieNoLoot(false);
            }
        }

        public void ApplyBossSmokeParticles(bool theEnable)
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_boss_head);
            GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref trackInstanceByName.mAttachmentID, ParticleEffect.ZamboniSmoke, null);
            if (theEnable)
            {
                TodParticleSystem todParticleSystem = mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.ZamboniSmoke);
                TodParticleSystem todParticleSystem2 = mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.ZamboniSmoke);
                if (todParticleSystem != null)
                {
                    AttachEffect attachEffect = reanimation.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_boss_head, ref todParticleSystem, 60f, 30f);
                    attachEffect.mDontDrawIfParentHidden = true;
                    attachEffect.mDontPropogateColor = true;
                }
                if (todParticleSystem2 != null)
                {
                    AttachEffect attachEffect2 = reanimation.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_boss_head, ref todParticleSystem2, 100f, 58f);
                    attachEffect2.mDontDrawIfParentHidden = true;
                    attachEffect2.mDontPropogateColor = true;
                }
                if (mBodyHealth < mBodyMaxHealth / GameConstants.BOSS_FLASH_HEALTH_FRACTION)
                {
                    TodParticleSystem todParticleSystem3 = mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.ZamboniSmoke);
                    if (todParticleSystem3 != null)
                    {
                        AttachEffect attachEffect3 = reanimation.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_boss_head, ref todParticleSystem3, 80f, 27f);
                        attachEffect3.mDontDrawIfParentHidden = true;
                        attachEffect3.mDontPropogateColor = true;
                    }
                }
            }
        }

        public void UpdateZombiquarium()//3update
        {
            if (IsDeadOrDying())
            {
                return;
            }
            float num = mVelZ;
            float num2 = mVelX;
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            if (mZombiePhase == ZombiePhase.ZombiquariumBite)
            {
                if (reanimation.mLoopCount > 0)
                {
                    float theAnimRate = TodCommon.RandRangeFloat(8f, 10f);
                    PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_aquarium_swim, ReanimLoopType.Loop, 20, theAnimRate);
                    mZombiePhase = ZombiePhase.ZombiquariumDrift;
                    mPhaseCounter = 100;
                }
            }
            else if (!ZombiquariumFindClosestBrain() && mPhaseCounter == 0)
            {
                int num3 = RandomNumbers.NextNumber(7);
                if (num3 <= 4)
                {
                    mZombiePhase = ZombiePhase.ZombiquariumAccel;
                    num = TodCommon.RandRangeFloat(0f, 6.2831855f);
                    mPhaseCounter = TodCommon.RandRangeInt(300, 1000);
                    reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                }
                else if (num3 == 4)
                {
                    mZombiePhase = ZombiePhase.ZombiquariumDrift;
                    num = 4.712389f;
                    mPhaseCounter = TodCommon.RandRangeInt(300, 1000);
                    reanimation.mAnimRate = TodCommon.RandRangeFloat(8f, 10f);
                }
                else if (num3 == 5)
                {
                    mZombiePhase = ZombiePhase.ZombiquariumBackAndForth;
                    num = 0f;
                    mPhaseCounter = TodCommon.RandRangeInt(300, 1000);
                    reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                }
                else
                {
                    mZombiePhase = ZombiePhase.ZombiquariumBackAndForth;
                    num = 3.1415927f;
                    mPhaseCounter = TodCommon.RandRangeInt(300, 1000);
                    reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                }
            }
            float aVelX = (float)Math.Cos(num);
            float aVelY = (float)Math.Sin(num);
            bool flag = false;
            if (mPosX < 0f && aVelX < 0f)
            {
                flag = true;
            }
            else if (mPosX > 680f && aVelX > 0f)
            {
                flag = true;
            }
            else if (mPosY < 100f && aVelY < 0f)
            {
                flag = true;
            }
            else if (mPosY > 400f && aVelY > 0f)
            {
                flag = true;
            }
            float aMaxSpeed = 0.5f;
            if (flag)
            {
                aMaxSpeed = num2 * 0.3f;
                mPhaseCounter = Math.Min(100, mPhaseCounter);
            }
            else if (mZombiePhase == ZombiePhase.ZombiquariumAccel)
            {
                aMaxSpeed = 0.5f;
            }
            else if (mZombiePhase == ZombiePhase.ZombiquariumBackAndForth)
            {
                if (mPosX >= 200f || aVelX < 0f)
                {
                }
                if (mPosX <= 550f || aVelX > 0f)
                {
                }
                aMaxSpeed = 0.3f;
            }
            else if (mZombiePhase == ZombiePhase.ZombiquariumDrift || mZombiePhase == ZombiePhase.ZombiquariumBite)
            {
                aMaxSpeed = 0.05f;
            }
            num2 = Math.Min(aMaxSpeed, num2 + 0.01f);
            aVelX *= num2;
            aVelY *= num2;
            mPosX += aVelX;
            mPosY += aVelY;
            if (!mBoard.HasLevelAwardDropped())
            {
                if (mSummonCounter > 0)
                {
                    mSummonCounter--;
                    if (mSummonCounter == 0)
                    {
                        mApp.PlayFoley(FoleyType.SpawnSun);
                        mBoard.AddCoin(mX + 50, mY + 40, CoinType.Sun, CoinMotion.FromPlant);
                        mSummonCounter = TodCommon.RandRangeInt(1000, 1500);
                    }
                }
                if (mZombieAge % 100 == 0)
                {
                    TakeDamage(10, 8U, false);
                    if (IsDeadOrDying())
                    {
                        mApp.PlaySample(Resources.SOUND_ZOMBAQUARIUM_DIE);
                    }
                }
            }
        }

        public bool ZombiquariumFindClosestBrain()
        {
            if (mBoard.HasLevelAwardDropped())
            {
                return false;
            }
            if (mBodyHealth > 150)
            {
                return false;
            }
            GridItem aTargetGridItem = null;
            float aDiatanceClosest = 0f;
            float num2 = 15f;
            float num3 = 15f;
            float num4 = 50f;
            float num5 = 40f;
            int i = -1;
            GridItem aGridItem = null;
            while (mBoard.IterateGridItems(ref aGridItem, ref i))
            {
                if (aGridItem.mGridItemType == GridItemType.Brain && aGridItem.mGridItemCounter >= 15)
                {
                    float aDistance = TodCommon.Distance2D(aGridItem.mPosX + num2, aGridItem.mPosY + num3, mPosX + num4, mPosY + num5);
                    if (aTargetGridItem == null || aDistance < aDiatanceClosest)
                    {
                        aDiatanceClosest = aDistance;
                        aTargetGridItem = aGridItem;
                    }
                }
            }
            if (aTargetGridItem != null && aDiatanceClosest < 50f)
            {
                aTargetGridItem.GridItemDie();
                mApp.PlayFoley(FoleyType.Slurp);
                mBodyHealth = Math.Min(mBodyMaxHealth, mBodyHealth + 200);
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_aquarium_bite, ReanimLoopType.PlayOnceAndHold, 10, 24f);
                mZombiePhase = ZombiePhase.ZombiquariumBite;
                mPhaseCounter = 200;
                return false;
            }
            if (aTargetGridItem != null)
            {
                float num8 = aTargetGridItem.mPosX + num2 - (mPosX + num4);
                float num9 = aTargetGridItem.mPosY + num3 - (mPosY + num5);
                float num10 = mVelZ;
                num10 = (float)Math.Atan2(num9, num8);
                if (num10 < 0f)
                {
                    num10 += 6.2831855f;
                }
                mZombiePhase = ZombiePhase.ZombiquariumAccel;
                return true;
            }
            return false;
        }

        public void UpdateZombieGatlingHead()//3update
        {
            if (!mHasHead)
            {
                return;
            }
            //if (mPhaseCounter >= 99 && mPhaseCounter < 102)
            if (mPhaseCounter == 99)
            {
                mApp.ReanimationGet(mSpecialHeadReanimID).PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.PlayOnceAndHold, 20, 38f);
                return;
            }
            //if ((mPhaseCounter >= 18 && mPhaseCounter < 21) || (mPhaseCounter >= 36 && mPhaseCounter < 39) || (mPhaseCounter >= 51 && mPhaseCounter < 54) || (mPhaseCounter >= 69 && mPhaseCounter < 72))
            if (mPhaseCounter == 18 || mPhaseCounter == 36 || mPhaseCounter == 51 || mPhaseCounter == 69)
            {
                mApp.PlayFoley(FoleyType.Throw);
                Reanimation reanimation_v = mApp.ReanimationGet(mBodyReanimID);
                if (reanimation_v != null)
                {
                    int theTrackIndex = reanimation_v.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
                    ReanimatorTransform aTransForm;
                    reanimation_v.GetCurrentTransform(theTrackIndex, out aTransForm, false);
                    float aOriginX = mPosX + aTransForm.mTransX * Constants.IS - 9f;
                    float aOriginY = mPosY + aTransForm.mTransY * Constants.IS + 6f;
                    if (mMindControlled)
                    {
                        aOriginX += 90f * mScaleZombie;
                        mBoard.AddProjectile((int)aOriginX, (int)aOriginY, mRenderOrder, mRow, ProjectileType.ZombiePeaMindControl);
                    }
                    else
                    {
                        mBoard.AddProjectile((int)aOriginX, (int)aOriginY, mRenderOrder, mRow, ProjectileType.ZombiePea)
                            .mMotionType = ProjectileMotion.Backwards;
                    }
                    aTransForm.PrepareForReuse();
                    return;
                }
            }
            else if (mPhaseCounter <= 0)
            {
                mApp.ReanimationGet(mSpecialHeadReanimID).PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.PlayOnceAndHold, 20, 15f);
                mPhaseCounter = 150;
            }
        }

        public void UpdateZombieSquashHead()//3update
        {
            float aPosX = 6f;
            float aPosY = -21f;
            if (mHasHead && mIsEating && mZombiePhase == ZombiePhase.SquashPreLaunch)
            {
                StopEating();
                PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.Loop, 20, 12f);
                mHasHead = false;
                Reanimation mSpecialHeadReanim = mApp.ReanimationGet(mSpecialHeadReanimID);
                mSpecialHeadReanim.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpup, ReanimLoopType.PlayOnceAndHold, 20, 24f);
                mSpecialHeadReanim.mRenderOrder = mRenderOrder + 1;
                mSpecialHeadReanim.SetPosition((mPosX + aPosX) * Constants.S, (mPosY + aPosY) * Constants.S);
                Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
                ReanimatorTrackInstance trackInstanceByName = reanimation2.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
                GlobalMembersAttachment.AttachmentDetach(ref trackInstanceByName.mAttachmentID);
                mSpecialHeadReanim.mOverlayMatrix.mMatrix.M11 = 0.75f;
                mSpecialHeadReanim.mOverlayMatrix.mMatrix.M12 = 0f;
                mSpecialHeadReanim.mOverlayMatrix.mMatrix.M22 = 0.75f;
                mSpecialHeadReanim.mOverlayMatrix.mMatrix.M21 = 0f;
                mZombiePhase = ZombiePhase.SquashRising;
                mPhaseCounter = 95;
            }
            if (mZombiePhase == ZombiePhase.SquashRising)
            {
                int aX = mBoard.GridToPixelX(mBoard.PixelToGridXKeepOnBoard(mX, mY), mRow);
                int aDestX = TodCommon.TodAnimateCurve(50, 20, mPhaseCounter, 0, aX - (int)mPosX, TodCurves.EaseInOut);
                int aDestY = TodCommon.TodAnimateCurve(50, 20, mPhaseCounter, 0, -20, TodCurves.EaseInOut);
                Reanimation reanimation = mApp.ReanimationGet(mSpecialHeadReanimID);
                reanimation.SetPosition((mPosX + aPosX + aDestX) * Constants.S, (mPosY + aPosY + aDestY) * Constants.S);
                if (mPhaseCounter <= 0)
                {
                    reanimation = mApp.ReanimationGet(mSpecialHeadReanimID);
                    reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpdown, ReanimLoopType.PlayOnceAndHold, 0, 60f);
                    mZombiePhase = ZombiePhase.SquashFalling;
                    mPhaseCounter = 10;
                }
            }
            if (mZombiePhase == ZombiePhase.SquashFalling)
            {
                int aDestY = TodCommon.TodAnimateCurve(10, 0, mPhaseCounter, -20, 74, TodCurves.Linear);
                int aDestX = mBoard.GridToPixelX(mBoard.PixelToGridXKeepOnBoard(mX, mY), mRow);
                Reanimation reanimation = mApp.ReanimationGet(mSpecialHeadReanimID);
                reanimation.SetPosition((mPosX + aPosX + aDestX - mPosX) * Constants.S, (mPosY + aPosY + aDestY) * Constants.S);
                //if (mPhaseCounter >= 4 && mPhaseCounter < 7)
                if (mPhaseCounter == 4)
                {
                    SquishAllInSquare(mBoard.PixelToGridXKeepOnBoard(mX, mY), mRow, ZombieAttackType.Chew);
                }
                if (mPhaseCounter <= 0)
                {
                    mZombiePhase = ZombiePhase.SquashDoneFalling;
                    mPhaseCounter = 100;
                    mBoard.ShakeBoard(1, 4);
                    mApp.PlayFoley(FoleyType.Thump);
                }
            }
            if (mZombiePhase == ZombiePhase.SquashDoneFalling && mPhaseCounter <= 0)
            {
                Reanimation aSpecialHeadReanim = mApp.ReanimationGet(mSpecialHeadReanimID);
                aSpecialHeadReanim.ReanimationDie();
                mSpecialHeadReanimID = null;
                TakeDamage(1800, 9U, false);
            }
        }

        public bool IsTanglekelpTarget()
        {
            for (int i = 0; i < mBoard.mPlants.Count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead && aPlant.mState == PlantState.TanglekelpGrabbing && aPlant.mTargetZombieID == mBoard.ZombieGetID(this))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasYuckyFaceImage()
        {
            return !mBoard.mFutureMode && (mZombieType == ZombieType.Normal || mZombieType == ZombieType.TrafficCone || mZombieType == ZombieType.Pail || mZombieType == ZombieType.Flag || mZombieType == ZombieType.Door || mZombieType == ZombieType.DuckyTube || mZombieType == ZombieType.Dancer || mZombieType == ZombieType.BackupDancer || mZombieType == ZombieType.Newspaper || mZombieType == ZombieType.Polevaulter);
        }

        public bool IsSquashTarget(Plant exceptMe)
        {
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && plant != exceptMe && plant.mSeedType == SeedType.Squash && plant.mTargetZombieID == mBoard.ZombieGetID(this))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsTangleKelpTarget()
        {
            if (!mApp.mBoard.StageHasPool())
            {
                return false;
            }
            if (mZombieHeight == ZombieHeight.DraggedUnder)
            {
                return true;
            }
            for (int i = 0; i < mBoard.mPlants.Count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead && aPlant.mSeedType == SeedType.Tanglekelp && aPlant.mTargetZombieID == mBoard.ZombieGetID(this))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFireResistant()
        {
            return mZombieType == ZombieType.Catapult
                || mZombieType == ZombieType.Zamboni
                || (mShieldType == ShieldType.Door || mShieldType == ShieldType.Ladder);
        }

        public void EnableMustache(bool theEnableMustache)
        {
            if (mFromWave == GameConstants.ZOMBIE_WAVE_UI)
            {
                return;
            }
            if (!mHasHead)
            {
                return;
            }
            switch (mZombieType)
            {
                case ZombieType.PeaHead:
                case ZombieType.WallnutHead:
                case ZombieType.TallnutHead:
                case ZombieType.JalapenoHead:
                case ZombieType.GatlingHead:
                case ZombieType.SquashHead:
                    return;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            if (!reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_zombie_mustache))
            {
                return;
            }
            if (!theEnableMustache)
            {
                reanimation.AssignRenderGroupToPrefix("Zombie_mustache", -1);
                return;
            }
            reanimation.AssignRenderGroupToPrefix("Zombie_mustache", 0);
            int num = TodCommon.RandRangeInt(1, 3);
            if (num == 1)
            {
                Image theImage = null;
                reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_mustache, theImage);
                return;
            }
            if (num == 2)
            {
                reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_mustache, AtlasResources.IMAGE_REANIM_ZOMBIE_MUSTACHE2);
                return;
            }
            if (num == 3)
            {
                reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_mustache, AtlasResources.IMAGE_REANIM_ZOMBIE_MUSTACHE3);
            }
        }

        public void EnableFuture(bool theEnableFuture)
        {
            if (mFromWave == GameConstants.ZOMBIE_WAVE_UI)
            {
                return;
            }
            if (mZombieType == ZombieType.PeaHead || mZombieType == ZombieType.WallnutHead || mZombieType == ZombieType.JalapenoHead || mZombieType == ZombieType.GatlingHead || mZombieType == ZombieType.SquashHead || mZombieType == ZombieType.TallnutHead)
            {
                return;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            if (reanimation.mReanimationType != ReanimationType.Zombie)
            {
                return;
            }
            if (!theEnableFuture)
            {
                Image theImage = null;
                reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, theImage);
                return;
            }
            int num = RandomNumbers.NextNumber(8) % 4;
            Image theImage2 = null;
            switch (num)
            {
                case 0:
                    theImage2 = AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_SUNGLASSES1;
                    break;
                case 1:
                    theImage2 = AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_SUNGLASSES2;
                    break;
                case 2:
                    theImage2 = AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_SUNGLASSES3;
                    break;
                case 3:
                    theImage2 = AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_SUNGLASSES4;
                    break;
                default:
                    Debug.ASSERT(false);
                    break;
            }
            reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, theImage2);
        }

        public void BungeeDropPlant()
        {
            if (mZombiePhase != ZombiePhase.BungeeGrabbing) // 当蹦极没在抓取状态时，没有执行的必要
            {
                return;
            }
            Plant plant = mBoard.mPlants.Contains(mTargetPlantID) ? mTargetPlantID : null;   //原为 Plant plant = mBoard.mPlants[mBoard.mPlants.IndexOf(mTargetPlantID)] ，由于导致崩溃问题而修改逻辑
            if (plant == null)
            {
                return;
            }
            if (plant.mOnBungeeState == PlantOnBungeeState.GettingGrabbedByBungee)
            {
                plant.mOnBungeeState = PlantOnBungeeState.NotOnBungee;
            }
            else if (plant.mOnBungeeState == PlantOnBungeeState.RisingWithBungee) // 当植物已经被偷走，植物消失
            {
                plant.Die();
            }
            mTargetPlantID = null;
        }

        public void RemoveButter()
        {
            if (mZombieType == ZombieType.Balloon)
            {
                BalloonPropellerHatSpin(true);
            }
            if (mZombieType == ZombieType.PeaHead || mZombieType == ZombieType.WallnutHead || mZombieType == ZombieType.TallnutHead || mZombieType == ZombieType.JalapenoHead || mZombieType == ZombieType.GatlingHead || mZombieType == ZombieType.SquashHead)
            {
                Reanimation reanimation = mApp.ReanimationTryToGet(mSpecialHeadReanimID);
                if (reanimation != null)
                {
                    if (mZombieType == ZombieType.PeaHead && reanimation.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
                    {
                        reanimation.mAnimRate = 35f;
                    }
                    else if (mZombieType == ZombieType.GatlingHead && reanimation.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
                    {
                        reanimation.mAnimRate = 38f;
                    }
                    else
                    {
                        reanimation.mAnimRate = 15f;
                    }
                }
            }
            UpdateAnimSpeed();
            StartZombieSound();
        }

        public void BalloonPropellerHatSpin(bool theSpinning)
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_hat);
            Reanimation reanimation2 = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName.mAttachmentID);
            if (reanimation2 == null)
            {
                return;
            }
            if (theSpinning)
            {
                reanimation2.mAnimRate = reanimation2.mDefinition.mFPS;
                return;
            }
            reanimation2.mAnimRate = 0f;
        }

        public void DoDaisies()
        {
            if (IsWalkingBackwards())
            {
                return;
            }
            if (mBoard.mPlantRow[mRow] == PlantRowType.Pool)
            {
                return;
            }
            if (mBoard.StageHasRoof())
            {
                return;
            }
            float aOffsetX = 20f;
            float aOffsetY = 100f;
            switch (mZombieType)
            {
                case ZombieType.Bobsled:
                case ZombieType.Zamboni:
                case ZombieType.Catapult:
                    return;
                case ZombieType.Football:
                case ZombieType.Dancer:
                case ZombieType.BackupDancer:
                    aOffsetX += 160f;
                    break;
                case ZombieType.Pogo:
                    aOffsetY += 20f;
                    break;
                case ZombieType.Balloon:
                    aOffsetY += 30f;
                    aOffsetX += 110f;
                    break;
            }
            if (mBoard.StageHasGraveStones())
            {
                aOffsetY += 15f;
            }
            mApp.AddTodParticle(
                mX + Constants.InvertAndScale(aOffsetX), 
                mY + Constants.InvertAndScale(aOffsetY), 
                Board.MakeRenderOrder(RenderLayer.GraveStone, mRow, 5), 
                ParticleEffect.Daisy
            );
        }

        public void EnableDanceMode(bool theEnableDance)
        {
            if (mFromWave == GameConstants.ZOMBIE_WAVE_UI || mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
            {
                return;
            }
            if (ZombieNotWalking())
            {
                return;
            }
            if (IsDeadOrDying())
            {
                return;
            }
            if (mZombieType == ZombieType.Normal || mZombieType == ZombieType.TrafficCone || mZombieType == ZombieType.Pail)
            {
                StartWalkAnim(0);
            }
        }

        public void BungeeLiftTarget()
        {
            PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_raise, ReanimLoopType.PlayOnceAndHold, 0, 36f);
            if (mTargetPlantID == null)
            {
                return;
            }
            Plant plant = mTargetPlantID;
            if (plant == null)
            {
                return;
            }
            for (int i = 0; i < mBoard.mZombies.Count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (zombie.mZombieType == ZombieType.Bungee && zombie != this && zombie.mTargetPlantID == plant)
                {
                    zombie.mTargetPlantID = null;
                }
            }
            plant.mOnBungeeState = PlantOnBungeeState.RisingWithBungee;
            mApp.PlayFoley(FoleyType.Floop);
            Reanimation reanimation = mApp.ReanimationTryToGet(plant.mBodyReanimID);
            if (reanimation != null)
            {
                reanimation.mAnimRate = 0.1f;
            }
            if (plant.mSeedType == SeedType.Cattail && mBoard.GetTopPlantAt(mTargetCol, mRow, TopPlant.OnlyPumpkin) != null)
            {
                mBoard.NewPlant(mTargetCol, mRow, SeedType.Lilypad, SeedType.None);
            }
            if (mApp.IsIZombieLevel())
            {
                mBoard.mChallenge.IZombiePlantDropRemainingSun(plant);
            }
        }

        public void SetupWaterTrack(ref string theTrackName)
        {
            Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
            ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(theTrackName);
            trackInstanceByName.mIgnoreExtraAdditiveColor = true;
            trackInstanceByName.mIgnoreColorOverride = true;
            trackInstanceByName.mIgnoreClipRect = true;
        }

        public static ZombieDefinition GetZombieDefinition(ZombieType theZombieType)
        {
            Debug.ASSERT(theZombieType >= ZombieType.Normal && theZombieType < ZombieType.ZombieTypesCount);
            Debug.ASSERT(GameConstants.gZombieDefs[(int)theZombieType].mZombieType == theZombieType);
            return GameConstants.gZombieDefs[(int)theZombieType];
        }

        public static bool ZombieTypeCanGoInPool(ZombieType theZombieType)
        {
            return theZombieType == ZombieType.Normal || theZombieType == ZombieType.TrafficCone || theZombieType == ZombieType.Pail || theZombieType == ZombieType.Flag || theZombieType == ZombieType.Snorkel || theZombieType == ZombieType.DolphinRider || theZombieType == ZombieType.PeaHead || theZombieType == ZombieType.WallnutHead || theZombieType == ZombieType.JalapenoHead || theZombieType == ZombieType.GatlingHead || theZombieType == ZombieType.TallnutHead;
        }

        public static bool ZombieTypeCanGoOnHighGround(ZombieType theZombieType)
        {
            return theZombieType != ZombieType.Zamboni && theZombieType != ZombieType.Bobsled;
        }

        private const float FADE_SPEED = 30f;

        private const float FADE_TIME_LAND = 100f;

        private const float FADE_SPEED_BUNGEE = 50f;

        private const float FADE_SPEED_BALOON = 50f;

        public ZombieType mZombieType;

        public ZombiePhase mZombiePhase;

        public float mPosX;

        public float mPosY;

        public float mVelX;

        public int mAnimCounter;

        public int mGroanCounter;

        public int mAnimTicksPerFrame;

        public int mAnimFrames;

        public int mFrame;

        public int mPrevFrame;

        public bool mVariant;

        public bool mIsEating;

        public int mJustGotShotCounter;

        public int mShieldJustGotShotCounter;

        public int mShieldRecoilCounter;

        public int mZombieAge;

        public ZombieHeight mZombieHeight;

        public int mPhaseCounter;

        public int mFromWave;

        public bool mDroppedLoot;

        public int mZombieFade;

        public bool mFlatTires;

        public int mUseLadderCol;

        public int mTargetCol;

        public float mAltitude;

        public bool mHitUmbrella;

        public TRect mZombieRect = default(TRect);

        public TRect mZombieAttackRect = default(TRect);

        public int mChilledCounter;

        public int mButteredCounter;

        public int mIceTrapCounter;

        public bool mMindControlled;

        public bool mBlowingAway;

        public bool mHasHead;

        public bool mHasArm;

        public bool mHasObject;

        public bool mInPool;

        public bool mOnHighGround;

        public bool mYuckyFace;

        public int mYuckyFaceCounter;

        public HelmType mHelmType;

        public int mBodyHealth;

        public int mBodyMaxHealth;

        public int mHelmHealth;

        public int mHelmMaxHealth;

        public ShieldType mShieldType;

        public int mShieldHealth;

        public int mShieldMaxHealth;

        public int mFlyingHealth;

        public int mFlyingMaxHealth;

        public bool mDead;

        public Zombie mRelatedZombieID;

        private int mRelatedZombieIDSaved;

        public Zombie[] mFollowerZombieID = new Zombie[GameConstants.MAX_ZOMBIE_FOLLOWERS];

        private int[] mFollowerZombieIDSaved = new int[GameConstants.MAX_ZOMBIE_FOLLOWERS];

        public Zombie mLeaderZombie;

        private int mLeaderZombieIDSaved = -1;

        public bool mPlayingSong;

        public int mParticleOffsetX;

        public int mParticleOffsetY;

        public Attachment mAttachmentID;

        public int mSummonCounter;

        public Reanimation mBodyReanimID;

        public float mScaleZombie;

        public float mVelZ;

        public float mOrginalAnimRate;

        public Plant mTargetPlantID;

        private int mTargetPlantIDSaved;

        public int mBossMode;

        public int mTargetRow;

        public int mBossBungeeCounter;

        public int mBossStompCounter;

        public int mBossHeadCounter;

        public Reanimation mBossFireBallReanimID;

        public Reanimation mSpecialHeadReanimID;

        public int mFireballRow;

        public bool mIsFireBall;

        public Reanimation mMoweredReanimID;

        public int mLastPortalX;

        public bool mHasGroundTrack;

        public bool mSummonedDancers;

        public bool mSurprised;

        private int mGroundTrackIndex;

        public bool mUsesClipping;

        private bool doLoot = true;

        private bool doParticle = true;

        private static Stack<Zombie> unusedObjects = new Stack<Zombie>();

        private static TodWeightedGridArray[] aPicks = new TodWeightedGridArray[Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY];

        public bool mIsButterShowing;

        public bool cachedZombieRectUpToDate;

        private TRect cachedZombieRect;

        public int mYuckyToRow;

        public bool mYuckySwitchRowsLate;

        public bool draggedByTangleKelp;

        private bool justLoaded;

        private string lastPlayedReanimName;

        private ReanimLoopType lastPlayedReanimLoopType;

        private byte lastPlayedReanimBlendTime;

        private float lastPlayedReanimAnimRate;

        public static bool WinningZombieReachedDesiredY = false;

        public bool mHasHelm = true;

        public bool mHasShield = true;

        public enum ZombieRenderLayerOffset //Prefix: ZOMBIE_LAYER_OFFSET
        {
            Bobsled4,
            Bobsled3,
            Bobsled2,
            Bobsled1,
            Normal,
            DogWalker,
            Dog,
            Digger,
            Zamboni
        }

        public enum ZombieParts
        {
            Body,
            Head,
            HeadEating,
            Tongue,
            Arm,
            Hair,
            HeadYucky,
            ArmPickaxe,
            ArmPolevault,
            ArmLeash,
            ArmFlag,
            Pogo,
            Digger
        }
    }
}
