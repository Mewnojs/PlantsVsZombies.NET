using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class Zombie : GameObject, IComparable
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
			this.Reset();
		}

		public override void PrepareForReuse()
		{
			Zombie.unusedObjects.Push(this);
		}

		protected override void Reset()
		{
			base.Reset();
			this.lastPlayedReanimName = string.Empty;
			this.lastPlayedReanimLoopType = ReanimLoopType.REANIM_PLAY_ONCE;
			this.lastPlayedReanimBlendTime = 0;
			this.lastPlayedReanimAnimRate = 0f;
			this.doLoot = true;
			this.doParticle = true;
			this.draggedByTangleKelp = false;
			this.cachedZombieRectUpToDate = false;
			this.mZombieType = ZombieType.ZOMBIE_NORMAL;
			this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
			this.mPosX = 0f;
			this.mPosY = 0f;
			this.mVelX = 0f;
			this.mAnimCounter = 0;
			this.mGroanCounter = 0;
			this.mAnimTicksPerFrame = 0;
			this.mAnimFrames = 0;
			this.mFrame = 0;
			this.mPrevFrame = 0;
			this.mVariant = false;
			this.mIsEating = false;
			this.mJustGotShotCounter = 0;
			this.mShieldJustGotShotCounter = 0;
			this.mShieldRecoilCounter = 0;
			this.mZombieAge = 0;
			this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
			this.mPhaseCounter = 0;
			this.mFromWave = 0;
			this.mDroppedLoot = false;
			this.mZombieFade = 0;
			this.mFlatTires = false;
			this.mUseLadderCol = 0;
			this.mTargetCol = 0;
			this.mAltitude = 0f;
			this.mHitUmbrella = false;
			this.mZombieRect = default(TRect);
			this.mZombieAttackRect = default(TRect);
			this.mChilledCounter = 0;
			this.mButteredCounter = 0;
			this.mIceTrapCounter = 0;
			this.mMindControlled = false;
			this.mBlowingAway = false;
			this.mHasHead = false;
			this.mHasArm = false;
			this.mHasObject = false;
			this.mInPool = false;
			this.mOnHighGround = false;
			this.mYuckyFace = false;
			this.mYuckyFaceCounter = 0;
			this.mHelmType = HelmType.HELMTYPE_NONE;
			this.mBodyHealth = 0;
			this.mBodyMaxHealth = 0;
			this.mHelmHealth = 0;
			this.mHelmMaxHealth = 0;
			this.mShieldType = ShieldType.SHIELDTYPE_NONE;
			this.mShieldHealth = 0;
			this.mShieldMaxHealth = 0;
			this.mFlyingHealth = 0;
			this.mFlyingMaxHealth = 0;
			this.mDead = false;
			this.mRelatedZombieID = null;
			for (int i = 0; i < this.mFollowerZombieID.Length; i++)
			{
				this.mFollowerZombieID[i] = null;
			}
			this.mLeaderZombieIDSaved = -1;
			this.mLeaderZombie = null;
			this.mPlayingSong = false;
			this.mParticleOffsetX = 0;
			this.mParticleOffsetY = 0;
			this.mSummonCounter = 0;
			this.mBodyReanimID = null;
			this.mScaleZombie = 0f;
			this.mVelZ = 0f;
			this.mOrginalAnimRate = 0f;
			this.mTargetPlantID = null;
			this.mBossMode = 0;
			this.mTargetRow = 0;
			this.mBossBungeeCounter = 0;
			this.mBossStompCounter = 0;
			this.mBossHeadCounter = 0;
			this.mBossFireBallReanimID = null;
			this.mSpecialHeadReanimID = null;
			this.mFireballRow = 0;
			this.mIsFireBall = false;
			this.mMoweredReanimID = null;
			this.mLastPortalX = 0;
			this.mHasGroundTrack = false;
			this.mSummonedDancers = false;
			this.mUsesClipping = false;
			this.mSurprised = false;
			this.mGroundTrackIndex = -1;
			this.mHasArm = true;
			this.mHasHead = true;
			this.mHasHelm = true;
			this.mHasShield = true;
		}

		int IComparable.CompareTo(object toCompare)
		{
			Zombie zombie = (Zombie)toCompare;
			return this.mX.CompareTo(zombie.mX);
		}

		public void ZombieInitialize(int theRow, ZombieType theType, bool theVariant, Zombie theParentZombie, int theFromWave)
		{
			Debug.ASSERT(theType >= ZombieType.ZOMBIE_NORMAL && theType < ZombieType.NUM_ZOMBIE_TYPES);
			this.mRow = theRow;
			this.mFromWave = theFromWave;
			this.mPosX = 800f - Constants.InvertAndScale(20f) + RandomNumbers.NextNumber(Constants.Zombie_StartRandom_Offset);
			this.mPosX += Constants.Zombie_StartOffset;
			this.mPosY = this.GetPosYBasedOnRow(theRow);
			this.mWidth = 120;
			this.mHeight = 120;
			this.mVelX = 0f;
			this.mVelZ = 0f;
			this.mFrame = 0;
			this.mPrevFrame = 0;
			this.mZombieType = theType;
			this.mVariant = theVariant;
			this.mIsEating = false;
			this.mJustGotShotCounter = 0;
			this.mShieldJustGotShotCounter = 0;
			this.mShieldRecoilCounter = 0;
			this.mChilledCounter = 0;
			this.mIceTrapCounter = 0;
			this.mButteredCounter = 0;
			this.mMindControlled = false;
			this.mBlowingAway = false;
			this.mHasHead = true;
			this.mHasArm = true;
			this.mHasObject = false;
			this.mInPool = false;
			this.mOnHighGround = false;
			this.mHelmType = HelmType.HELMTYPE_NONE;
			this.mShieldType = ShieldType.SHIELDTYPE_NONE;
			this.mYuckyFace = false;
			this.mYuckyFaceCounter = 0;
			this.mAnimCounter = 0;
			this.mGroanCounter = TodCommon.RandRangeInt(400, 500);
			this.mAnimTicksPerFrame = 12;
			this.mAnimFrames = 12;
			this.mZombieAge = 0;
			this.mTargetCol = -1;
			this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
			this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
			this.mPhaseCounter = 0;
			this.mHitUmbrella = false;
			this.mDroppedLoot = false;
			this.mRelatedZombieID = null;
			this.mZombieRect = new TRect(36, 0, 42, 115);
			this.mZombieAttackRect = new TRect(50, 0, 20, 115);
			this.mPlayingSong = false;
			this.mZombieFade = -1;
			this.mFlatTires = false;
			this.mUseLadderCol = -1;
			this.mShieldHealth = 0;
			this.mHelmHealth = 0;
			this.mFlyingHealth = 0;
			this.mAttachmentID = null;
			this.mSummonCounter = 0;
			this.mBossStompCounter = -1;
			this.mBossBungeeCounter = -1;
			this.mBossHeadCounter = -1;
			this.mBodyReanimID = null;
			this.mScaleZombie = 1f;
			this.mAltitude = 0f;
			this.mOrginalAnimRate = 0f;
			this.mTargetPlantID = null;
			this.mBossMode = 0;
			this.mBossFireBallReanimID = null;
			this.mSpecialHeadReanimID = null;
			this.mTargetRow = -1;
			this.mFireballRow = -1;
			this.mIsFireBall = false;
			this.mMoweredReanimID = null;
			this.mLastPortalX = -1;
			this.mHasGroundTrack = false;
			this.mSummonedDancers = false;
			this.mSurprised = false;
			for (int i = 0; i < GameConstants.MAX_ZOMBIE_FOLLOWERS; i++)
			{
				this.mFollowerZombieID[i] = null;
			}
			if (this.mBoard != null && this.mBoard.IsFlagWave(this.mFromWave))
			{
				this.mPosX += 40f;
			}
			this.PickRandomSpeed();
			this.mBodyHealth = 270;
			RenderLayer theRenderLayer = RenderLayer.RENDER_LAYER_ZOMBIE;
			int theLayerOffset = 4;
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(this.mZombieType);
			if (zombieDefinition.mReanimationType != ReanimationType.REANIM_NONE)
			{
				this.LoadReanim(zombieDefinition.mReanimationType);
			}
			if (theType == ZombieType.ZOMBIE_NORMAL)
			{
				this.LoadPlainZombieReanim();
			}
			else if (theType == ZombieType.ZOMBIE_DUCKY_TUBE)
			{
				this.LoadPlainZombieReanim();
			}
			else if (theType == ZombieType.ZOMBIE_TRAFFIC_CONE)
			{
				this.LoadPlainZombieReanim();
				this.ReanimShowPrefix("anim_cone", 0);
				this.ReanimShowPrefix("anim_hair", -1);
				this.mHelmType = HelmType.HELMTYPE_TRAFFIC_CONE;
				this.mHelmHealth = 370;
			}
			else if (theType == ZombieType.ZOMBIE_PAIL)
			{
				this.LoadPlainZombieReanim();
				this.ReanimShowPrefix("anim_bucket", 0);
				this.ReanimShowPrefix("anim_hair", -1);
				this.mHelmType = HelmType.HELMTYPE_PAIL;
				this.mHelmHealth = 1100;
			}
			else if (theType == ZombieType.ZOMBIE_DOOR)
			{
				this.mShieldType = ShieldType.SHIELDTYPE_DOOR;
				this.mShieldHealth = 1100;
				this.mPosX += 60f;
				this.LoadPlainZombieReanim();
				this.AttachShield();
			}
			else if (theType == ZombieType.ZOMBIE_YETI)
			{
				this.mBodyHealth = 1350;
				this.mPhaseCounter = TodCommon.RandRangeInt(1500, 2000);
				this.mHasObject = true;
				this.mZombieAttackRect = new TRect(20, 0, 50, 115);
				this.mPosX += 60f;
			}
			else if (theType == ZombieType.ZOMBIE_LADDER)
			{
				this.mBodyHealth = 500;
				this.mShieldType = ShieldType.SHIELDTYPE_LADDER;
				this.mShieldHealth = 500;
				this.mZombieAttackRect = new TRect(10, 0, 50, 115);
				if (this.IsOnBoard())
				{
					this.mZombiePhase = ZombiePhase.PHASE_LADDER_CARRYING;
					this.StartWalkAnim(0);
				}
				this.AttachShield();
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				this.mBodyHealth = 450;
				this.mAnimFrames = 4;
				this.mAltitude = (float)(GameConstants.BUNGEE_ZOMBIE_HEIGHT + TodCommon.RandRangeInt(0, 150));
				this.mVelX = 0f;
				if (this.IsOnBoard())
				{
					this.PickBungeeZombieTarget(-1);
					if (this.mDead)
					{
						return;
					}
					this.mZombiePhase = ZombiePhase.PHASE_BUNGEE_DIVING;
				}
				else
				{
					this.mZombiePhase = ZombiePhase.PHASE_BUNGEE_CUTSCENE;
					this.mPhaseCounter = TodCommon.RandRangeInt(0, 200);
				}
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_drop, ReanimLoopType.REANIM_LOOP, 0, 24f);
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				reanimation.AssignRenderGroupToPrefix("Zombie_bungi_rightarm_lower2", GameConstants.RENDER_GROUP_ARMS);
				reanimation.AssignRenderGroupToPrefix("Zombie_bungi_rightarm_hand2", GameConstants.RENDER_GROUP_ARMS);
				reanimation.AssignRenderGroupToPrefix("Zombie_bungi_leftarm_lower2", GameConstants.RENDER_GROUP_ARMS);
				reanimation.AssignRenderGroupToPrefix("Zombie_bungi_leftarm_hand2", GameConstants.RENDER_GROUP_ARMS);
				reanimation.SetTruncateDisappearingFrames(string.Empty, false);
				theRenderLayer = RenderLayer.RENDER_LAYER_GRAVE_STONE;
				theLayerOffset = 7;
				this.mZombieRect = new TRect(-20, 22, 110, 94);
				this.mZombieAttackRect = new TRect(0, 0, 0, 0);
				this.mVariant = false;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
			{
				this.mZombieRect = new TRect(50, 0, 57, 115);
				this.ReanimShowPrefix("anim_hair", -1);
				this.mHelmType = HelmType.HELMTYPE_FOOTBALL;
				this.mHelmHealth = 1400;
				this.mAnimTicksPerFrame = 6;
				this.mVariant = false;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				this.mHelmType = HelmType.HELMTYPE_DIGGER;
				this.mHelmHealth = 100;
				this.mVariant = false;
				this.mHasObject = true;
				this.mZombieRect = new TRect(50, 0, 28, 115);
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
				reanimation2.SetTruncateDisappearingFrames(string.Empty, false);
				if (!this.IsOnBoard())
				{
					this.mZombiePhase = ZombiePhase.PHASE_DIGGER_CUTSCENE;
				}
				else
				{
					this.mZombiePhase = ZombiePhase.PHASE_DIGGER_TUNNELING;
					this.AddAttachedParticle(60, 100, ParticleEffect.PARTICLE_DIGGER_TUNNEL);
					theLayerOffset = 7;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_dig, ReanimLoopType.REANIM_LOOP_FULL_LAST_FRAME, 0, 12f);
					this.PickRandomSpeed();
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				this.mBodyHealth = 500;
				this.mAnimTicksPerFrame = 6;
				this.mZombiePhase = ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT;
				this.mHasObject = true;
				this.mVariant = false;
				this.mPosX = (float)(Constants.WIDE_BOARD_WIDTH + 70 + RandomNumbers.NextNumber(10));
				if (this.IsOnBoard())
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_run, ReanimLoopType.REANIM_LOOP, 0, 0f);
					this.PickRandomSpeed();
				}
				if (this.mApp.IsWallnutBowlingLevel())
				{
					this.mZombieAttackRect = new TRect(-229, 0, 270, 115);
				}
				else
				{
					this.mZombieAttackRect = new TRect(-29, 0, 70, 115);
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER)
			{
				this.mBodyHealth = 500;
				this.mAnimTicksPerFrame = 6;
				this.mZombiePhase = ZombiePhase.PHASE_DOLPHIN_WALKING;
				this.mVariant = false;
				if (this.IsOnBoard())
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_walkdolphin, ReanimLoopType.REANIM_LOOP, 0, 0f);
					this.PickRandomSpeed();
				}
				this.SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_dolphinrider_whitewater);
				this.SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_dolphinrider_dolphininwater);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				this.mBodyHealth = 3000;
				this.mAnimFrames = 24;
				this.mAnimTicksPerFrame = 8;
				this.mWidth = 180;
				this.mHeight = 180;
				this.mPosX = (float)(Constants.WIDE_BOARD_WIDTH + 45 + RandomNumbers.NextNumber(10));
				this.mZombieRect = new TRect(-17, -38, 125, 154);
				this.mZombieAttackRect = new TRect(-30, -38, 89, 154);
				this.mVariant = false;
				theLayerOffset = 8;
				this.mHasObject = true;
				int num = RandomNumbers.NextNumber(100);
				int num2;
				if (!this.IsOnBoard() || this.mBoard.mLevel == 48)
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
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (num2 == 2)
				{
					reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_gargantuar_telephonepole, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_ZOMBIE);
				}
				else if (num2 == 1)
				{
					reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_gargantuar_telephonepole, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_DUCKXING);
				}
				if (this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
				{
					reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_HEAD_REDEYE);
					this.mBodyHealth = 6000;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				this.mBodyHealth = 1350;
				this.mAnimFrames = 2;
				this.mAnimTicksPerFrame = 8;
				this.mPosX = (float)(Constants.WIDE_BOARD_WIDTH + RandomNumbers.NextNumber(10) + 20);
				theLayerOffset = 8;
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_drive, ReanimLoopType.REANIM_LOOP, 0, 12f);
				this.mZombieRect = new TRect(0, -13, 153, 140);
				this.mZombieAttackRect = new TRect(10, -13, 133, 140);
				this.mVariant = false;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				this.mBodyHealth = 850;
				this.mPosX = (float)(Constants.WIDE_BOARD_WIDTH + 25 + RandomNumbers.NextNumber(10));
				this.mSummonCounter = 20;
				if (this.IsOnBoard())
				{
					this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, 0, 5.5f);
				}
				else
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 8f);
				}
				this.mZombieRect = new TRect(0, -13, 153, 140);
				this.mZombieAttackRect = new TRect(10, -13, 133, 140);
				this.mVariant = false;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
			{
				this.mZombieRect = new TRect(12, 0, 62, 115);
				this.mZombieAttackRect = new TRect(-5, 0, 55, 115);
				this.SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_snorkle_whitewater);
				this.SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_snorkle_whitewater2);
				this.mVariant = false;
				this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_WALKING;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_JACK_IN_THE_BOX)
			{
				this.mBodyHealth = 500;
				this.mAnimTicksPerFrame = 6;
				int num3 = 450 + RandomNumbers.NextNumber(300);
				if (RandomNumbers.NextNumber(20) == 0)
				{
					num3 /= 3;
				}
				this.mPhaseCounter = (int)((float)num3 / this.mVelX) * GameConstants.ZOMBIE_LIMP_SPEED_FACTOR;
				this.mZombieAttackRect = new TRect(20, 0, 50, 115);
				if (this.mApp.IsScaryPotterLevel())
				{
					this.mPhaseCounter = 10;
				}
				if (this.IsOnBoard())
				{
					this.mZombiePhase = ZombiePhase.PHASE_JACK_IN_THE_BOX_RUNNING;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				theLayerOffset = 3;
				if (theParentZombie != null)
				{
					int num4 = 0;
					while (num4 < 3 && theParentZombie.mFollowerZombieID[num4] != null)
					{
						num4++;
					}
					Debug.ASSERT(num4 < 3);
					theParentZombie.mFollowerZombieID[num4] = this.mBoard.ZombieGetID(this);
					this.mRelatedZombieID = this.mBoard.ZombieGetID(theParentZombie);
					this.mPosX = theParentZombie.mPosX + (float)((num4 + 1) * 50);
					if (num4 == 0)
					{
						theLayerOffset = 1;
						this.mAltitude = 9f;
					}
					else if (num4 == 1)
					{
						theLayerOffset = 2;
						this.mAltitude = -7f;
					}
					else
					{
						theLayerOffset = 0;
						this.mAltitude = 9f;
					}
				}
				else
				{
					this.mPosX = (float)(Constants.WIDE_BOARD_WIDTH + 80);
					this.mAltitude = -10f;
					this.mHelmType = HelmType.HELMTYPE_BOBSLED;
					this.mHelmHealth = 300;
					this.mZombieRect = new TRect(-50, 0, 275, 115);
				}
				this.mVelX = 0.6f;
				this.mZombiePhase = ZombiePhase.PHASE_BOBSLED_SLIDING;
				this.mPhaseCounter = 500;
				this.mVariant = false;
				if (theFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jump, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 20f);
					Reanimation reanimation4 = this.mApp.ReanimationGet(this.mBodyReanimID);
					reanimation4.mAnimTime = 1f;
					this.mAltitude = 18f;
				}
				else if (this.IsOnBoard())
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_push, ReanimLoopType.REANIM_LOOP, 0, 30f);
				}
			}
			else if (theType == ZombieType.ZOMBIE_FLAG)
			{
				this.mHasObject = true;
				this.LoadPlainZombieReanim();
				Reanimation reanimation5 = this.mApp.ReanimationGet(this.mBodyReanimID);
				Reanimation reanimation6 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_ZOMBIE_FLAGPOLE);
				reanimation6.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_zombie_flag, ReanimLoopType.REANIM_LOOP, 0, 15f);
				this.mSpecialHeadReanimID = this.mApp.ReanimationGetID(reanimation6);
				ReanimatorTrackInstance trackInstanceByName = reanimation5.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand);
				GlobalMembersAttachment.AttachReanim(ref trackInstanceByName.mAttachmentID, reanimation6, 0f, 0f);
				reanimation5.mFrameBasePose = 0;
				this.mPosX = (float)Constants.WIDE_BOARD_WIDTH;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				this.mVariant = false;
				this.mZombiePhase = ZombiePhase.PHASE_POGO_BOUNCING;
				this.mPhaseCounter = RandomNumbers.NextNumber(GameConstants.POGO_BOUNCE_TIME) + 1;
				this.mHasObject = true;
				this.mBodyHealth = 500;
				this.mZombieAttackRect = new TRect(10, 0, 30, 115);
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_pogo, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 40f);
				Reanimation reanimation7 = this.mApp.ReanimationGet(this.mBodyReanimID);
				reanimation7.mAnimTime = 1f;
			}
			else if (theType == ZombieType.ZOMBIE_NEWSPAPER)
			{
				this.mZombieAttackRect = new TRect(20, 0, 50, 115);
				this.mZombiePhase = ZombiePhase.PHASE_NEWSPAPER_READING;
				this.mShieldType = ShieldType.SHIELDTYPE_NEWSPAPER;
				this.mShieldHealth = 150;
				this.mVariant = false;
				this.AttachShield();
			}
			else if (theType == ZombieType.ZOMBIE_BALLOON)
			{
				Reanimation reanimation8 = this.mApp.ReanimationGet(this.mBodyReanimID);
				reanimation8.SetTruncateDisappearingFrames(string.Empty, false);
				if (this.IsOnBoard())
				{
					this.mZombiePhase = ZombiePhase.PHASE_BALLOON_FLYING;
					this.mAltitude = 25f;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, reanimation8.mAnimRate);
				}
				else
				{
					float animRate = TodCommon.RandRangeFloat(8f, 10f);
					this.SetAnimRate(animRate);
				}
				Reanimation reanimation9 = this.mApp.AddReanimation(0f, 0f, 0, zombieDefinition.mReanimationType);
				reanimation9.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_propeller);
				reanimation9.mLoopType = ReanimLoopType.REANIM_LOOP_FULL_LAST_FRAME;
				reanimation9.AttachToAnotherReanimation(ref reanimation8, GlobalMembersReanimIds.ReanimTrackId_hat);
				this.mFlyingHealth = 20;
				this.mZombieRect = new TRect(36, 30, 42, 115);
				this.mZombieAttackRect = new TRect(20, 30, 50, 115);
				this.mVariant = false;
			}
			else if (theType == ZombieType.ZOMBIE_DANCER)
			{
				if (!this.IsOnBoard())
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_moonwalk, ReanimLoopType.REANIM_LOOP, 0, 12f);
				}
				else
				{
					this.mZombiePhase = ZombiePhase.PHASE_DANCER_DANCING_IN;
					this.mPhaseCounter = 200 + RandomNumbers.NextNumber(12);
					this.mVelX = 0.5f;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_moonwalk, ReanimLoopType.REANIM_LOOP, 0, 24f);
				}
				this.mBodyHealth = 500;
				this.mVariant = false;
			}
			else if (theType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				if (!this.IsOnBoard())
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.REANIM_LOOP, 0, 12f);
				}
				this.ReanimShowPrefix("Zombie_disco_outerarm_upper_bone", -1);
				this.mZombiePhase = ZombiePhase.PHASE_DANCER_DANCING_LEFT;
				this.mVariant = false;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_IMP)
			{
				if (!this.IsOnBoard())
				{
					this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, 0, 12f);
				}
				if (this.mApp.IsIZombieLevel())
				{
					this.mBodyHealth = 70;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				this.mPosX = (float)Constants.BOARD_EXTRA_ROOM;
				this.mPosY = 0f;
				this.mZombieRect = new TRect(700, 80, 90, 430);
				this.mZombieAttackRect = default(TRect);
				theRenderLayer = RenderLayer.RENDER_LAYER_TOP;
				if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
				{
					this.mBodyHealth = 40000;
				}
				else
				{
					this.mBodyHealth = 60000;
				}
				if (this.IsOnBoard())
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_enter, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 12f);
					this.mSummonCounter = 500;
					this.mBossHeadCounter = 5000;
					this.mZombiePhase = ZombiePhase.PHASE_BOSS_ENTER;
				}
				else
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.REANIM_LOOP, 0, 12f);
				}
				this.BossSetupReanim();
			}
			else if (theType == ZombieType.ZOMBIE_PEA_HEAD)
			{
				this.LoadPlainZombieReanim();
				this.ReanimShowPrefix("anim_hair", -1);
				this.ReanimShowPrefix("anim_head2", -1);
				Reanimation reanimation10 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (this.IsOnBoard())
				{
					reanimation10.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
				}
				ReanimatorTrackInstance trackInstanceByName2 = reanimation10.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
				trackInstanceByName2.mImageOverride = AtlasResources.IMAGE_BLANK;
				Reanimation reanimation11 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_PEASHOOTER);
				reanimation11.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.REANIM_LOOP, 0, 15f);
				this.mSpecialHeadReanimID = this.mApp.ReanimationGetID(reanimation11);
				AttachEffect attachEffect = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName2.mAttachmentID, reanimation11, 0f, 0f);
				reanimation10.mFrameBasePose = 0;
				TodCommon.TodScaleRotateTransformMatrix(ref attachEffect.mOffset.mMatrix, 65f * Constants.S, -8f * Constants.S, 0.2f, -1f, 1f);
				this.mPhaseCounter = 150;
				this.mVariant = false;
			}
			else if (theType == ZombieType.ZOMBIE_WALLNUT_HEAD)
			{
				this.LoadPlainZombieReanim();
				this.ReanimShowPrefix("anim_hair", -1);
				this.ReanimShowPrefix("anim_head", -1);
				this.ReanimShowPrefix("Zombie_tie", -1);
				Reanimation reanimation12 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (this.IsOnBoard())
				{
					reanimation12.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
				}
				ReanimatorTrackInstance trackInstanceByName3 = reanimation12.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_body);
				Reanimation reanimation13 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_WALLNUT);
				reanimation13.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 15f);
				this.mSpecialHeadReanimID = this.mApp.ReanimationGetID(reanimation13);
				AttachEffect attachEffect2 = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName3.mAttachmentID, reanimation13, 0f, 0f);
				reanimation12.mFrameBasePose = 0;
				TodCommon.TodScaleRotateTransformMatrix(ref attachEffect2.mOffset.mMatrix, 50f * Constants.S, 0f, 0.2f, -0.8f, 0.8f);
				this.mHelmType = HelmType.HELMTYPE_WALLNUT;
				this.mHelmHealth = 1100;
				this.mVariant = false;
			}
			else if (theType == ZombieType.ZOMBIE_TALLNUT_HEAD)
			{
				this.LoadPlainZombieReanim();
				this.ReanimShowPrefix("anim_hair", -1);
				this.ReanimShowPrefix("anim_head", -1);
				this.ReanimShowPrefix("Zombie_tie", -1);
				Reanimation reanimation14 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (this.IsOnBoard())
				{
					reanimation14.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
				}
				ReanimatorTrackInstance trackInstanceByName4 = reanimation14.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_body);
				Reanimation reanimation15 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_TALLNUT);
				reanimation15.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 15f);
				this.mSpecialHeadReanimID = this.mApp.ReanimationGetID(reanimation15);
				AttachEffect attachEffect3 = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName4.mAttachmentID, reanimation15, 0f, 0f);
				reanimation14.mFrameBasePose = 0;
				TodCommon.TodScaleRotateTransformMatrix(ref attachEffect3.mOffset.mMatrix, 37f * Constants.S, 0f, 0.2f, -0.8f, 0.8f);
				this.mHelmType = HelmType.HELMTYPE_TALLNUT;
				this.mHelmHealth = 2200;
				this.mVariant = false;
				this.mPosX += 30f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD)
			{
				this.LoadPlainZombieReanim();
				this.ReanimShowPrefix("anim_hair", -1);
				this.ReanimShowPrefix("anim_head", -1);
				this.ReanimShowPrefix("Zombie_tie", -1);
				Reanimation reanimation16 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (this.IsOnBoard())
				{
					reanimation16.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
				}
				ReanimatorTrackInstance trackInstanceByName5 = reanimation16.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_body);
				Reanimation reanimation17 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_JALAPENO);
				reanimation17.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 15f);
				this.mSpecialHeadReanimID = this.mApp.ReanimationGetID(reanimation17);
				AttachEffect attachEffect4 = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName5.mAttachmentID, reanimation17, 0f, 0f);
				reanimation16.mFrameBasePose = 0;
				TodCommon.TodScaleRotateTransformMatrix(ref attachEffect4.mOffset.mMatrix, 55f * Constants.S, -5f * Constants.S, 0.2f, -1f, 1f);
				this.mVariant = false;
				this.mBodyHealth = 500;
				int num5 = 275 + RandomNumbers.NextNumber(175);
				this.mPhaseCounter = (int)((float)num5 / this.mVelX) * GameConstants.ZOMBIE_LIMP_SPEED_FACTOR;
			}
			else if (theType == ZombieType.ZOMBIE_GATLING_HEAD)
			{
				this.LoadPlainZombieReanim();
				this.ReanimShowPrefix("anim_hair", -1);
				this.ReanimShowPrefix("anim_head2", -1);
				Reanimation reanimation18 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (this.IsOnBoard())
				{
					reanimation18.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
				}
				ReanimatorTrackInstance trackInstanceByName6 = reanimation18.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
				trackInstanceByName6.mImageOverride = AtlasResources.IMAGE_BLANK;
				Reanimation reanimation19 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_GATLINGPEA);
				reanimation19.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.REANIM_LOOP, 0, 15f);
				this.mSpecialHeadReanimID = this.mApp.ReanimationGetID(reanimation19);
				AttachEffect attachEffect5 = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName6.mAttachmentID, reanimation19, 0f, 0f);
				reanimation18.mFrameBasePose = 0;
				TodCommon.TodScaleRotateTransformMatrix(ref attachEffect5.mOffset.mMatrix, 65f * Constants.S, -18f * Constants.S, 0.2f, -1f, 1f);
				this.mPhaseCounter = 150;
				this.mVariant = false;
			}
			else if (theType == ZombieType.ZOMBIE_SQUASH_HEAD)
			{
				this.LoadPlainZombieReanim();
				this.ReanimShowPrefix("anim_hair", -1);
				this.ReanimShowPrefix("anim_head2", -1);
				Reanimation reanimation20 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (this.IsOnBoard())
				{
					reanimation20.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_walk2);
				}
				ReanimatorTrackInstance trackInstanceByName7 = reanimation20.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
				trackInstanceByName7.mImageOverride = AtlasResources.IMAGE_BLANK;
				Reanimation reanimation21 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_SQUASH);
				reanimation21.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 15f);
				this.mSpecialHeadReanimID = this.mApp.ReanimationGetID(reanimation21);
				AttachEffect attachEffect6 = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName7.mAttachmentID, reanimation21, 0f, 0f);
				reanimation20.mFrameBasePose = 0;
				TodCommon.TodScaleRotateTransformMatrix(ref attachEffect6.mOffset.mMatrix, 55f * Constants.S, -15f * Constants.S, 0.2f, -0.75f, 0.75f);
				this.mZombiePhase = ZombiePhase.PHASE_SQUASH_PRE_LAUNCH;
				this.mVariant = false;
			}
			if (this.mApp.IsLittleTroubleLevel() && (this.IsOnBoard() || theFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE))
			{
				this.mScaleZombie = 0.5f;
				this.mBodyHealth /= 4;
				this.mHelmHealth /= 4;
				this.mShieldHealth /= 4;
				this.mFlyingHealth /= 4;
			}
			this.UpdateAnimSpeed();
			if (this.mVariant)
			{
				this.ReanimShowPrefix("anim_tongue", 0);
			}
			this.mBodyMaxHealth = this.mBodyHealth;
			this.mHelmMaxHealth = this.mHelmHealth;
			this.mShieldMaxHealth = this.mShieldHealth;
			this.mFlyingMaxHealth = this.mFlyingHealth;
			this.mDead = false;
			this.mX = (int)this.mPosX;
			this.mY = (int)this.mPosY;
			this.mRenderOrder = Board.MakeRenderOrder(theRenderLayer, this.mRow, theLayerOffset);
			if (this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIQUARIUM)
			{
				this.mBodyMaxHealth = 300;
			}
			if (this.IsOnBoard())
			{
				this.PlayZombieAppearSound();
				this.StartZombieSound();
			}
			this.UpdateReanim();
			if (this.mBodyReanimID != null && this.mBodyReanimID.TrackExists("zombie_butter"))
			{
				this.mBodyReanimID.AssignRenderGroupToTrack("zombie_butter", -1);
			}
		}

		public void Dispose()
		{
			GlobalMembersAttachment.AttachmentDie(ref this.mAttachmentID);
			this.StopZombieSound();
			this.PrepareForReuse();
		}

		public void Animate()
		{
			this.mPrevFrame = this.mFrame;
			if (this.mZombiePhase == ZombiePhase.PHASE_JACK_IN_THE_BOX_POPPING || this.mZombiePhase == ZombiePhase.PHASE_NEWSPAPER_MADDENING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING_PAUSE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_STUNNED)
			{
				return;
			}
			if (this.IsImmobilizied())
			{
				return;
			}
			this.mAnimCounter += 3;
			if (this.mYuckyFace)
			{
				for (int i = 0; i < 3; i++)
				{
					if (this.mYuckyFace)
					{
						this.UpdateYuckyFace();
					}
				}
			}
			if (this.mIsEating && this.mHasHead)
			{
				int num = 6;
				if (this.mChilledCounter > 0)
				{
					num = 12;
				}
				if (this.mAnimCounter >= this.mAnimFrames * num)
				{
					this.mAnimCounter = num;
				}
				this.mFrame = this.mAnimCounter / num;
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
				if (reanimation != null)
				{
					float theEventTime = 0.14f;
					float theEventTime2 = 0.68f;
					if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
					{
						theEventTime = 0.38f;
						theEventTime2 = 0.8f;
					}
					else if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER || this.mZombieType == ZombieType.ZOMBIE_LADDER)
					{
						theEventTime = 0.42f;
						theEventTime2 = 0.42f;
					}
					else if (this.mZombieType == ZombieType.ZOMBIE_JACK_IN_THE_BOX)
					{
						theEventTime = 0.53f;
						theEventTime2 = 0.53f;
					}
					else if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
					{
						theEventTime = 0.33f;
						theEventTime2 = 0.83f;
					}
					else if (this.mZombieType == ZombieType.ZOMBIE_IMP)
					{
						theEventTime = 0.33f;
						theEventTime2 = 0.79f;
					}
					if (reanimation.ShouldTriggerTimedEvent(theEventTime) || reanimation.ShouldTriggerTimedEvent(theEventTime2))
					{
						this.AnimateChewSound();
						this.AnimateChewEffect();
						return;
					}
				}
				else
				{
					if (this.mAnimCounter == 3 * num)
					{
						this.AnimateChewSound();
					}
					if (this.mAnimCounter == 6 * num && !this.mMindControlled)
					{
						this.AnimateChewEffect();
					}
				}
				return;
			}
			if (this.mAnimCounter >= this.mAnimFrames * this.mAnimTicksPerFrame)
			{
				this.mAnimCounter = 0;
			}
			this.mFrame = this.mAnimCounter / this.mAnimTicksPerFrame;
		}

		public void CheckIfPreyCaught()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE || this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombieType == ZombieType.ZOMBIE_CATAPULT || this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				return;
			}
			if (this.IsBouncingPogo() || this.IsBobsledTeamWithSled())
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT || this.mZombiePhase == ZombiePhase.PHASE_NEWSPAPER_MADDENING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING_PAUSE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_STUNNED || this.mZombiePhase == ZombiePhase.PHASE_RISING_FROM_GRAVE || this.mZombiePhase == ZombiePhase.PHASE_IMP_GETTING_THROWN || this.mZombiePhase == ZombiePhase.PHASE_IMP_LANDING || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RISING || this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS || this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS_WITH_LIGHT || this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS_HOLD || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING_WITHOUT_DOLPHIN || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_RIDING || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_IN_JUMP || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING || this.mZombiePhase == ZombiePhase.PHASE_LADDER_PLACING || this.mZombieHeight == ZombieHeight.HEIGHT_GETTING_BUNGEE_DROPPED || this.mZombieHeight == ZombieHeight.HEIGHT_UP_LADDER || this.mZombieHeight == ZombieHeight.HEIGHT_IN_TO_POOL || this.mZombieHeight == ZombieHeight.HEIGHT_OUT_OF_POOL || this.IsTangleKelpTarget() || this.mZombieHeight == ZombieHeight.HEIGHT_FALLING)
			{
				return;
			}
			if (!this.mHasHead)
			{
				return;
			}
			if (this.IsFlying())
			{
				return;
			}
			int num = GameConstants.TICKS_BETWEEN_EATS;
			if (this.mChilledCounter > 0)
			{
				num *= 6;
			}
			if (this.mZombieAge % num != 0)
			{
				return;
			}
			Zombie zombie = this.FindZombieTarget();
			if (zombie != null)
			{
				this.EatZombie(zombie);
				return;
			}
			if (!this.mMindControlled)
			{
				Plant plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_CHEW);
				if (plant != null)
				{
					this.EatPlant(plant);
					return;
				}
			}
			if (this.mApp.IsIZombieLevel() && this.mBoard.mChallenge.IZombieEatBrain(this))
			{
				return;
			}
			if (this.mIsEating)
			{
				this.StopEating();
			}
		}

		public void EatZombie(Zombie theZombie)
		{
			theZombie.TakeDamage(GameConstants.TICKS_BETWEEN_EATS, 9U);
			this.StartEating();
			if (theZombie.mBodyHealth <= 0)
			{
				this.mApp.PlaySample(Resources.SOUND_GULP);
			}
		}

		public void EatPlant(Plant thePlant)
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_DANCING_IN)
			{
				this.mPhaseCounter = 1;
				return;
			}
			if (this.mYuckyFace)
			{
				return;
			}
			if (this.mBoard.GetLadderAt(thePlant.mPlantCol, thePlant.mRow) != null && this.mZombieType != ZombieType.ZOMBIE_DIGGER)
			{
				this.StopEating();
				if (this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIE_NORMAL && this.mUseLadderCol != thePlant.mPlantCol)
				{
					this.mZombieHeight = ZombieHeight.HEIGHT_UP_LADDER;
					this.mUseLadderCol = thePlant.mPlantCol;
				}
				return;
			}
			this.StartEating();
			if ((thePlant.mSeedType == SeedType.SEED_JALAPENO || thePlant.mSeedType == SeedType.SEED_CHERRYBOMB || thePlant.mSeedType == SeedType.SEED_DOOMSHROOM || thePlant.mSeedType == SeedType.SEED_ICESHROOM || thePlant.mSeedType == SeedType.SEED_HYPNOSHROOM || thePlant.mState == PlantState.STATE_FLOWERPOT_INVULNERABLE || thePlant.mState == PlantState.STATE_LILYPAD_INVULNERABLE || thePlant.mState == PlantState.STATE_SQUASH_LOOK || thePlant.mState == PlantState.STATE_SQUASH_PRE_LAUNCH) && !thePlant.mIsAsleep)
			{
				if (this.mZombieType == ZombieType.ZOMBIE_DANCER && thePlant.mSeedType == SeedType.SEED_HYPNOSHROOM)
				{
					this.mBoard.GrantAchievement(AchievementId.DiscoisUndead);
				}
				return;
			}
			if (thePlant.mSeedType == SeedType.SEED_POTATOMINE && thePlant.mState != PlantState.STATE_NOTREADY)
			{
				return;
			}
			bool flag = false;
			if (thePlant.mSeedType == SeedType.SEED_BLOVER)
			{
				flag = true;
			}
			if (thePlant.mSeedType == SeedType.SEED_ICESHROOM && !thePlant.mIsAsleep)
			{
				flag = true;
			}
			if (flag)
			{
				thePlant.DoSpecial();
				return;
			}
			if (this.mChilledCounter > 0 && this.mZombieAge % 2 == 1)
			{
				return;
			}
			if (this.mApp.IsIZombieLevel() && thePlant.mSeedType == SeedType.SEED_SUNFLOWER)
			{
				int num = thePlant.mPlantHealth / 40;
				int num2 = (thePlant.mPlantHealth - 3 * GameConstants.TICKS_BETWEEN_EATS) / 40;
				if (num2 < num)
				{
					this.mBoard.AddCoin(thePlant.mX, thePlant.mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
				}
			}
			thePlant.mPlantHealth -= 3 * GameConstants.TICKS_BETWEEN_EATS;
			thePlant.mRecentlyEatenCountdown = 50;
			if (this.mApp.IsIZombieLevel() && this.mJustGotShotCounter < -500 && (thePlant.mSeedType == SeedType.SEED_WALLNUT || thePlant.mSeedType == SeedType.SEED_TALLNUT || thePlant.mSeedType == SeedType.SEED_PUMPKINSHELL))
			{
				thePlant.mPlantHealth -= 3 * GameConstants.TICKS_BETWEEN_EATS;
			}
			if (thePlant.mPlantHealth <= 0)
			{
				this.mApp.PlaySample(Resources.SOUND_GULP);
				this.mBoard.mPlantsEaten++;
				thePlant.Die();
				this.mBoard.mChallenge.ZombieAtePlant(this, thePlant);
				if (this.mBoard.mLevel >= 2 && this.mBoard.mLevel <= 4 && this.mApp.IsFirstTimeAdventureMode() && thePlant.mPlantCol > 4 && this.mBoard.mPlants.Count < 15 && thePlant.mSeedType == SeedType.SEED_PEASHOOTER)
				{
					this.mBoard.DisplayAdvice("[ADVICE_PEASHOOTER_DIED]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_PEASHOOTER_DIED);
				}
			}
		}

		public override bool SaveToFile(Buffer b)
		{
			base.SaveToFile(b);
			b.WriteLong((int)this.mZombieType);
			b.WriteBoolean(this.mVariant);
			b.WriteLong(this.mFromWave);
			b.WriteFloat(this.mAltitude);
			b.WriteLong(this.mAnimCounter);
			b.WriteLong(this.mAnimFrames);
			b.WriteLong(this.mAnimTicksPerFrame);
			b.WriteBoolean(this.mBlowingAway);
			b.WriteLong(this.mBodyHealth);
			b.WriteLong(this.mBodyMaxHealth);
			b.WriteLong(this.mBossBungeeCounter);
			b.WriteLong(this.mBossHeadCounter);
			b.WriteLong(this.mBossMode);
			b.WriteLong(this.mBossStompCounter);
			b.WriteLong(this.mButteredCounter);
			b.WriteLong(this.mChilledCounter);
			b.WriteBoolean(this.mDroppedLoot);
			b.WriteLong(this.mFireballRow);
			b.WriteBoolean(this.mFlatTires);
			b.WriteLong(this.mFlyingHealth);
			b.WriteLong(this.mFlyingMaxHealth);
			for (int i = 0; i < this.mFollowerZombieID.Length; i++)
			{
				GameObject.SaveId(this.mFollowerZombieID[i], b);
			}
			GameObject.SaveId(this.mLeaderZombie, b);
			b.WriteLong(this.mFrame);
			b.WriteLong(this.mGroanCounter);
			b.WriteBoolean(this.mHasGroundTrack);
			b.WriteBoolean(this.mHasObject);
			b.WriteLong(this.mHelmHealth);
			b.WriteLong(this.mHelmMaxHealth);
			b.WriteBoolean(this.mHitUmbrella);
			b.WriteLong(this.mIceTrapCounter);
			b.WriteBoolean(this.mInPool);
			b.WriteBoolean(this.mIsEating);
			b.WriteBoolean(this.mIsFireBall);
			b.WriteLong(this.mJustGotShotCounter);
			b.WriteLong(this.mLastPortalX);
			b.WriteBoolean(this.mMindControlled);
			b.WriteBoolean(this.mOnHighGround);
			b.WriteFloat(this.mOrginalAnimRate);
			b.WriteLong(this.mParticleOffsetX);
			b.WriteLong(this.mParticleOffsetY);
			b.WriteLong(this.mPhaseCounter);
			b.WriteBoolean(this.mPlayingSong);
			b.WriteFloat(this.mPosX);
			b.WriteFloat(this.mPosY);
			b.WriteLong(this.mPrevFrame);
			b.WriteFloat(this.mPrevTransX);
			b.WriteFloat(this.mPrevTransY);
			GameObject.SaveId(this.mRelatedZombieID, b);
			b.WriteFloat(this.mScaleZombie);
			b.WriteLong(this.mShieldHealth);
			b.WriteLong(this.mShieldJustGotShotCounter);
			b.WriteLong(this.mShieldMaxHealth);
			b.WriteLong(this.mShieldRecoilCounter);
			b.WriteLong(this.mSummonCounter);
			b.WriteBoolean(this.mSummonedDancers);
			b.WriteLong(this.mTargetCol);
			GameObject.SaveId(this.mTargetPlantID, b);
			b.WriteLong(this.mTargetRow);
			b.WriteLong(this.mUseLadderCol);
			b.WriteBoolean(this.mUsesClipping);
			b.WriteFloat(this.mVelX);
			b.WriteFloat(this.mVelZ);
			b.WriteBoolean(this.mYuckyFace);
			b.WriteLong(this.mYuckyFaceCounter);
			b.WriteLong(this.mZombieAge);
			b.WriteRect(this.mZombieAttackRect);
			b.WriteLong((int)this.mZombieHeight);
			b.WriteLong((int)this.mZombiePhase);
			b.WriteRect(this.mZombieRect);
			b.WriteString(this.lastPlayedReanimName);
			b.WriteFloat(this.lastPlayedReanimAnimRate);
			b.WriteByte(this.lastPlayedReanimBlendTime);
			b.WriteLong((int)this.lastPlayedReanimLoopType);
			b.WriteBoolean(this.mHasArm);
			b.WriteBoolean(this.mHasHead);
			b.WriteBoolean(this.mHasHelm);
			b.WriteBoolean(this.mHasShield);
			return true;
		}

		public override bool LoadFromFile(Buffer b)
		{
			base.LoadFromFile(b);
			this.mZombieType = (ZombieType)b.ReadLong();
			this.mVariant = b.ReadBoolean();
			this.mFromWave = b.ReadLong();
			this.doLoot = false;
			this.doParticle = false;
			int mRow = this.mRow;
			this.ZombieInitialize(this.mRow, this.mZombieType, this.mVariant, null, this.mFromWave);
			this.mAltitude = b.ReadFloat();
			this.mAnimCounter = b.ReadLong();
			this.mAnimFrames = b.ReadLong();
			this.mAnimTicksPerFrame = b.ReadLong();
			this.mBlowingAway = b.ReadBoolean();
			this.mBodyHealth = b.ReadLong();
			this.mBodyMaxHealth = b.ReadLong();
			this.mBossBungeeCounter = b.ReadLong();
			this.mBossHeadCounter = b.ReadLong();
			this.mBossMode = b.ReadLong();
			this.mBossStompCounter = b.ReadLong();
			this.mButteredCounter = b.ReadLong();
			this.mChilledCounter = b.ReadLong();
			this.mDroppedLoot = b.ReadBoolean();
			this.mFireballRow = b.ReadLong();
			this.mFlatTires = b.ReadBoolean();
			this.mFlyingHealth = b.ReadLong();
			this.mFlyingMaxHealth = b.ReadLong();
			for (int i = 0; i < this.mFollowerZombieID.Length; i++)
			{
				this.mFollowerZombieIDSaved[i] = GameObject.LoadId(b);
			}
			this.mLeaderZombieIDSaved = GameObject.LoadId(b);
			this.mFrame = b.ReadLong();
			this.mGroanCounter = b.ReadLong();
			this.mHasGroundTrack = b.ReadBoolean();
			this.mHasObject = b.ReadBoolean();
			this.mHelmHealth = b.ReadLong();
			this.mHelmMaxHealth = b.ReadLong();
			this.mHitUmbrella = b.ReadBoolean();
			this.mIceTrapCounter = b.ReadLong();
			this.mInPool = b.ReadBoolean();
			this.mIsEating = b.ReadBoolean();
			this.mIsFireBall = b.ReadBoolean();
			this.mJustGotShotCounter = b.ReadLong();
			this.mLastPortalX = b.ReadLong();
			this.mMindControlled = b.ReadBoolean();
			this.mOnHighGround = b.ReadBoolean();
			this.mOrginalAnimRate = b.ReadFloat();
			this.mParticleOffsetX = b.ReadLong();
			this.mParticleOffsetY = b.ReadLong();
			this.mPhaseCounter = b.ReadLong();
			this.mPlayingSong = b.ReadBoolean();
			this.mPosX = b.ReadFloat();
			this.mPosY = b.ReadFloat();
			this.mPrevFrame = b.ReadLong();
			this.mPrevTransX = b.ReadFloat();
			this.mPrevTransY = b.ReadFloat();
			this.mRelatedZombieIDSaved = GameObject.LoadId(b);
			this.mScaleZombie = b.ReadFloat();
			this.mShieldHealth = b.ReadLong();
			this.mShieldJustGotShotCounter = b.ReadLong();
			this.mShieldMaxHealth = b.ReadLong();
			this.mShieldRecoilCounter = b.ReadLong();
			this.mSummonCounter = b.ReadLong();
			this.mSummonedDancers = b.ReadBoolean();
			this.mTargetCol = b.ReadLong();
			this.mTargetPlantIDSaved = GameObject.LoadId(b);
			this.mTargetRow = b.ReadLong();
			this.mUseLadderCol = b.ReadLong();
			this.mUsesClipping = b.ReadBoolean();
			this.mVelX = b.ReadFloat();
			this.mVelZ = b.ReadFloat();
			this.mYuckyFace = b.ReadBoolean();
			this.mYuckyFaceCounter = b.ReadLong();
			this.mZombieAge = b.ReadLong();
			this.mZombieAttackRect = b.ReadRect();
			this.mZombieHeight = (ZombieHeight)b.ReadLong();
			this.mZombiePhase = (ZombiePhase)b.ReadLong();
			this.mZombieRect = b.ReadRect();
			this.lastPlayedReanimName = b.ReadString();
			this.lastPlayedReanimAnimRate = b.ReadFloat();
			this.lastPlayedReanimBlendTime = b.ReadByte();
			this.lastPlayedReanimLoopType = (ReanimLoopType)b.ReadLong();
			this.mHasArm = b.ReadBoolean();
			this.mHasHead = b.ReadBoolean();
			this.mHasHelm = b.ReadBoolean();
			this.mHasShield = b.ReadBoolean();
			if (!this.mHasArm)
			{
				this.mHasArm = true;
				this.DropArm(16U);
			}
			if (!this.mHasHead)
			{
				this.mHasHead = true;
				this.DropHead(16U);
			}
			if (!this.mHasShield && this.mShieldType != ShieldType.SHIELDTYPE_NONE)
			{
				this.DropShield(16U);
			}
			if (!this.mHasHelm && this.mHelmType != HelmType.HELMTYPE_NONE)
			{
				this.DropHelm(16U);
			}
			if (!this.mHasObject && this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				this.DiggerLoseAxe();
			}
			if (this.mButteredCounter > 0 && !this.IsZombotany())
			{
				this.mBodyReanimID.AssignRenderGroupToTrack("zombie_butter", 0);
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				this.SetRow(mRow);
				this.mPosX = (float)this.mBoard.GridToPixelX(this.mTargetCol, mRow);
				this.mPosY = this.GetPosYBasedOnRow(mRow);
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, mRow, 7);
			}
			return true;
		}

		public override void LoadingComplete()
		{
			this.justLoaded = true;
			base.LoadingComplete();
			for (int i = 0; i < this.mFollowerZombieID.Length; i++)
			{
				this.mFollowerZombieID[i] = (GameObject.GetObjectById(this.mFollowerZombieIDSaved[i]) as Zombie);
			}
			this.mLeaderZombie = (GameObject.GetObjectById(this.mLeaderZombieIDSaved) as Zombie);
			this.mTargetPlantID = (GameObject.GetObjectById(this.mTargetPlantIDSaved) as Plant);
			this.mRelatedZombieID = (GameObject.GetObjectById(this.mRelatedZombieIDSaved) as Zombie);
			if (this.mZombieType != ZombieType.ZOMBIE_BOSS || this.lastPlayedReanimName != "anim_spawn_1")
			{
				this.PlayZombieReanim(ref this.lastPlayedReanimName, this.lastPlayedReanimLoopType, this.lastPlayedReanimBlendTime, this.lastPlayedReanimAnimRate);
			}
			int num = this.mJustGotShotCounter;
			this.TakeHelmDamage(0, 0U);
			if (this.IsFlying())
			{
				this.TakeFlyingDamage(0, 0U);
			}
			this.TakeShieldDamage(0, 0U);
			this.TakeBodyDamage(0, 0U);
			this.TakeDamage(0, 0U);
			this.UpdateDamageStates(0U);
			this.mJustGotShotCounter = num;
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				int bodyDamageIndex = this.GetBodyDamageIndex();
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
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
					this.ApplyBossSmokeParticles(true);
				}
			}
			this.Update();
			this.Update();
			this.doLoot = true;
			this.doParticle = true;
			this.justLoaded = false;
		}

		public void RemoveSurprise()
		{
			for (int i = 0; i < this.mAttachmentID.mEffectArray.Length; i++)
			{
				if (this.mAttachmentID.mEffectArray[i].mEffectType == EffectType.EFFECT_REANIM)
				{
					Reanimation reanimation = (Reanimation)this.mAttachmentID.mEffectArray[i].mEffectID;
					if (reanimation.mReanimationType == ReanimationType.REANIM_ZOMBIE_SURPRISE && reanimation.mLoopCount == 1)
					{
						GlobalMembersAttachment.AttachmentDetach(ref this.mAttachmentID);
						return;
					}
				}
			}
		}

		public void Update()
		{
			this.cachedZombieRectUpToDate = false;
			Debug.ASSERT(!this.mDead);
			this.mZombieAge += 3;
			bool flag = this.mSurprised;
			if ((this.mApp.mGameScene != GameScenes.SCENE_LEVEL_INTRO || this.mZombieType != ZombieType.ZOMBIE_BOSS) && (!this.IsOnBoard() || !this.mBoard.mCutScene.ShouldRunUpsellBoard()) && this.mApp.mGameScene != GameScenes.SCENE_PLAYING && this.IsOnBoard() && this.mFromWave != GameConstants.ZOMBIE_WAVE_WINNER)
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED)
			{
				this.UpdateBurn();
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_MOWERED)
			{
				this.UpdateMowered();
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING)
			{
				this.UpdateDeath();
				this.UpdateZombieWalking();
			}
			else
			{
				if (this.mPhaseCounter > 0 && !this.IsImmobilizied())
				{
					this.mPhaseCounter -= 3;
				}
				if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
				{
					if (this.mBoard.mCutScene.ShowZombieWalking())
					{
						this.UpdateZombieChimney();
						this.UpdateZombieWalkingIntoHouse();
					}
				}
				else if (this.IsOnBoard())
				{
					this.UpdatePlaying();
				}
				if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
				{
					this.UpdateZombieBungee();
				}
				if (this.mZombieType == ZombieType.ZOMBIE_POGO)
				{
					this.UpdateZombiePogo();
				}
				this.Animate();
			}
			this.mJustGotShotCounter -= 3;
			if (this.mShieldJustGotShotCounter > 0)
			{
				this.mShieldJustGotShotCounter -= 3;
			}
			if (this.mShieldRecoilCounter > 0)
			{
				this.mShieldRecoilCounter -= 3;
			}
			if (this.mZombieFade > 0)
			{
				this.mZombieFade -= 3;
				if (this.mZombieFade <= 0)
				{
					this.DieNoLoot(true);
				}
			}
			this.mX = (int)this.mPosX;
			this.mY = (int)this.mPosY;
			GlobalMembersAttachment.AttachmentUpdateAndMove(ref this.mAttachmentID, this.mPosX, this.mPosY);
			this.UpdateReanim();
		}

		public void DieNoLoot(bool giveAchievements)
		{
			this.StopZombieSound();
			GlobalMembersAttachment.AttachmentDie(ref this.mAttachmentID);
			this.mApp.RemoveReanimation(ref this.mBodyReanimID);
			this.mApp.RemoveReanimation(ref this.mMoweredReanimID);
			this.mApp.RemoveReanimation(ref this.mSpecialHeadReanimID);
			this.mDead = true;
			this.TrySpawnLevelAward();
			if (this.mApp.mPlayerInfo != null && this.mFromWave != GameConstants.ZOMBIE_WAVE_UI && giveAchievements)
			{
				this.mApp.mPlayerInfo.mZombiesKilled += 1L;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				this.BobsledDie();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				this.BungeeDie();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				this.BossDie();
			}
			if (giveAchievements && this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR && this.mBoard != null)
			{
				this.mBoard.GrantAchievement(AchievementId.CrashoftheTitan);
			}
			if (this.mLeaderZombie != null && this.mLeaderZombie.mFollowerZombieID != null)
			{
				for (int i = 0; i < this.mLeaderZombie.mFollowerZombieID.Length; i++)
				{
					if (this.mLeaderZombie.mFollowerZombieID[i] == this)
					{
						this.mLeaderZombie.mFollowerZombieID[i] = null;
					}
				}
			}
		}

		public void DieWithLoot()
		{
			this.DieNoLoot(true);
			if (!this.doLoot)
			{
				return;
			}
			this.DropLoot();
		}

		public void Draw(Graphics g)
		{
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			if (this.mZombieHeight == ZombieHeight.HEIGHT_GETTING_BUNGEE_DROPPED)
			{
				return;
			}
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON && !this.SetupDrawZombieWon(g))
			{
				return;
			}
			if (this.mIceTrapCounter > 0)
			{
				this.DrawIceTrap(g, ref zombieDrawPosition, false);
			}
			if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL || this.mFromWave == GameConstants.ZOMBIE_WAVE_UI)
			{
				if (this.mBodyReanimID != null)
				{
					this.DrawReanim(g, ref zombieDrawPosition, 0);
				}
				else
				{
					this.DrawZombie(g, ref zombieDrawPosition);
				}
			}
			if (this.mIceTrapCounter > 0)
			{
				this.DrawIceTrap(g, ref zombieDrawPosition, true);
			}
			if (this.mButteredCounter > 0)
			{
				if (!this.mIsButterShowing)
				{
					if (!this.IsZombotany())
					{
						this.mBodyReanimID.AssignRenderGroupToTrack("zombie_butter", 0);
					}
					this.mIsButterShowing = true;
				}
				if (this.IsZombotany() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
				{
					this.DrawButter(g, ref zombieDrawPosition);
				}
			}
			else if (this.mIsButterShowing)
			{
				if (!this.IsZombotany())
				{
					this.mBodyReanimID.AssignRenderGroupToTrack("zombie_butter", -1);
				}
				this.mIsButterShowing = false;
			}
			if (this.mAttachmentID != null)
			{
				Graphics @new = Graphics.GetNew(g);
				base.MakeParentGraphicsFrame(@new);
				@new.mTransY += (int)(zombieDrawPosition.mBodyY * Constants.S);
				if (zombieDrawPosition.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
				{
					float num = 120f - zombieDrawPosition.mClipHeight + 21f;
					float mImageOffsetX = zombieDrawPosition.mImageOffsetX;
					float num2 = zombieDrawPosition.mImageOffsetY - 28f;
					@new.ClipRect((int)(((float)this.mX + mImageOffsetX - 400f) * Constants.S), (int)(((float)this.mY + num2) * Constants.S), (int)(920f * Constants.S), (int)(num * Constants.S));
				}
				GlobalMembersAttachment.AttachmentDraw(this.mAttachmentID, @new, false, true);
				@new.PrepareForReuse();
			}
			g.ClearClipRect();
		}

		public bool IsZombotany()
		{
			return this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD || this.mZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD || this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD || this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD;
		}

		public void DrawZombie(Graphics g, ref ZombieDrawPosition theDrawPos)
		{
			Image theImage = null;
			int theRow = 0;
			bool flag = false;
			ZombieType zombieType = this.mZombieType;
			switch (zombieType)
			{
			case ZombieType.ZOMBIE_NORMAL:
			case ZombieType.ZOMBIE_FLAG:
			case ZombieType.ZOMBIE_TRAFFIC_CONE:
			case ZombieType.ZOMBIE_PAIL:
			case ZombieType.ZOMBIE_NEWSPAPER:
			case ZombieType.ZOMBIE_DOOR:
			case ZombieType.ZOMBIE_FOOTBALL:
			case ZombieType.ZOMBIE_DOLPHIN_RIDER:
				break;
			case ZombieType.ZOMBIE_POLEVAULTER:
			case ZombieType.ZOMBIE_DANCER:
			case ZombieType.ZOMBIE_BACKUP_DANCER:
			case ZombieType.ZOMBIE_DUCKY_TUBE:
			case ZombieType.ZOMBIE_SNORKEL:
			case ZombieType.ZOMBIE_ZAMBONI:
			case ZombieType.ZOMBIE_BOBSLED:
				goto IL_58;
			default:
				if (zombieType != ZombieType.ZOMBIE_LADDER)
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
				this.DrawZombieWithParts(g, ref theDrawPos);
				return;
			}
			this.DrawZombiePart(g, theImage, this.mFrame, theRow, ref theDrawPos);
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
			if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT)
			{
				num += -120f;
				num2 += -120f;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING)
			{
				num2 += 50f;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				num2 += -19f;
			}
			float num3 = (float)celHeight;
			if (theDrawPos.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
			{
				num3 = TodCommon.ClampFloat((float)celHeight - theDrawPos.mClipHeight, 0f, (float)celHeight);
			}
			int num4 = 255;
			if (this.mZombieFade >= 0)
			{
				num4 = TodCommon.ClampInt((int)((float)(255 * this.mZombieFade) / 30f), 0, 255);
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, num4));
			}
			bool flag = false;
			if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_DANCING_IN || this.mZombiePhase == ZombiePhase.PHASE_DANCER_DANCING_LEFT)
			{
				int dancerFrame = this.GetDancerFrame();
				if (!this.mIsEating && (dancerFrame == 12 || dancerFrame == 13 || dancerFrame == 14 || dancerFrame == 18 || dancerFrame == 19 || dancerFrame == 20))
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
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED)
			{
				if (this.mMindControlled)
				{
					flag = true;
				}
				g.SetColorizeImages(true);
				g.SetColor(SexyColor.Black);
				g.DrawImageMirror(theImage, theDestRect, theSrcRect, flag);
			}
			else if (this.mMindControlled)
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
			else if (this.mChilledCounter > 0 || this.mIceTrapCounter > 0)
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
			if (this.mJustGotShotCounter > 0)
			{
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				g.SetColorizeImages(true);
				int num5 = this.mJustGotShotCounter * 10;
				g.SetColor(new SexyColor(num5, num5, num5, 255));
				g.DrawImageMirror(theImage, theDestRect, theSrcRect, flag);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			}
			g.SetColorizeImages(false);
		}

		public void DrawBungeeCord(Graphics g, int theOffsetX, int theOffsetY)
		{
			int num = (int)((float)AtlasResources.IMAGE_BUNGEECORD.GetCelHeight() * this.mScaleZombie);
			float num2 = 0f;
			float num3 = 0f;
			this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_bungi_body, ref num2, ref num3);
			bool flag = false;
			if (this.IsOnBoard() && this.mApp.IsFinalBossLevel())
			{
				Zombie bossZombie = this.mBoard.GetBossZombie();
				int num4 = 55;
				if (bossZombie.mZombiePhase == ZombiePhase.PHASE_BOSS_BUNGEES_LEAVE)
				{
					Reanimation reanimation = this.mApp.ReanimationGet(bossZombie.mBodyReanimID);
					num4 = (int)TodCommon.TodAnimateCurveFloatTime(0f, 0.2f, reanimation.mAnimTime, 55f, 0f, TodCurves.CURVE_LINEAR);
				}
				if (this.mTargetCol > bossZombie.mTargetCol)
				{
					g.SetClipRect(new TRect(-g.mTransX, (int)((float)num4 * Constants.S) - g.mTransY, Constants.BOARD_WIDTH, Constants.BOARD_HEIGHT));
					flag = true;
				}
			}
			bool mColorizeImages = g.mColorizeImages;
			Color mColor = g.mColor;
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED)
			{
				int a = TodCommon.ClampInt((int)((float)(255 * this.mZombieFade) / 30f), 0, 255);
				g.SetColor(new Color(0, 0, 0, a));
				g.SetColorizeImages(true);
			}
			for (float num5 = num3 - (float)num; num5 > (float)(-(float)num); num5 -= (float)num)
			{
				float thePosX = (float)(theOffsetX + Constants.Zombie_Bungee_Offset.X) - 4f / this.mScaleZombie;
				float thePosY = num5 - this.mPosY - (float)Constants.Zombie_Bungee_Offset.Y;
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_BUNGEECORD, thePosX, thePosY, this.mScaleZombie, this.mScaleZombie);
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED)
			{
				g.SetColor(mColor, false);
				g.SetColorizeImages(mColorizeImages);
			}
			if (flag)
			{
				g.ClearClipRect();
			}
		}

		public void TakeDamage(int theDamage, uint theDamageFlags)
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_JACK_IN_THE_BOX_POPPING)
			{
				return;
			}
			if (this.IsDeadOrDying())
			{
				return;
			}
			int num = theDamage;
			if (this.IsFlying())
			{
				num = this.TakeFlyingDamage(theDamage, theDamageFlags);
			}
			if (num > 0 && this.mShieldType != ShieldType.SHIELDTYPE_NONE && !TodCommon.TestBit(theDamageFlags, 0))
			{
				num = this.TakeShieldDamage(theDamage, theDamageFlags);
				if (TodCommon.TestBit(theDamageFlags, 1))
				{
					num = theDamage;
				}
			}
			if (num > 0 && this.mHelmType != HelmType.HELMTYPE_NONE)
			{
				num = this.TakeHelmDamage(theDamage, theDamageFlags);
			}
			if (num > 0)
			{
				this.TakeBodyDamage(num, theDamageFlags);
			}
		}

		public void SetRow(int theRow)
		{
			Debug.ASSERT(theRow >= 0 && theRow < Constants.MAX_GRIDSIZEY);
			this.mRow = theRow;
			this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ZOMBIE, this.mRow, 4);
		}

		public float GetPosYBasedOnRow(int theRow)
		{
			if (!this.IsOnBoard())
			{
				return 0f;
			}
			if (this.IsOnHighGround())
			{
				if (this.mAltitude < (float)Constants.HIGH_GROUND_HEIGHT)
				{
					this.mZombieHeight = ZombieHeight.HEIGHT_UP_TO_HIGH_GROUND;
				}
				this.mOnHighGround = true;
			}
			float num = this.mBoard.GetPosYBasedOnRow(this.mPosX + 40f, theRow) - 30f;
			if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				num -= 30f;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				num -= 16f;
			}
			return num;
		}

		public void ApplyChill(bool theIsIceTrap)
		{
			if (!this.CanBeChilled())
			{
				return;
			}
			if (this.mChilledCounter == 0)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_FROZEN);
			}
			int num = 1000;
			if (theIsIceTrap)
			{
				num = 2000;
			}
			this.mChilledCounter = Math.Max(num, this.mChilledCounter);
			this.UpdateAnimSpeed();
		}

		public void UpdateZombieBungee()
		{
			if (this.IsDeadOrDying())
			{
				return;
			}
			if (this.IsImmobilizied())
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_DIVING || this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_DIVING_SCREAMING)
			{
				float num = (float)GameConstants.BUNGEE_ZOMBIE_HEIGHT - 404f;
				float num2 = this.mAltitude;
				this.mAltitude -= 24f;
				if (this.mAltitude <= num && num2 > num && this.mRelatedZombieID == null)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_GRASSSTEP);
				}
				this.BungeeLanding();
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_AT_BOTTOM)
			{
				if (this.mPhaseCounter <= 0)
				{
					this.BungeeStealTarget();
					this.mZombiePhase = ZombiePhase.PHASE_BUNGEE_GRABBING;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_GRABBING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					this.BungeeLiftTarget();
					this.mZombiePhase = ZombiePhase.PHASE_BUNGEE_RISING;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_HIT_OUCHY)
			{
				if (this.mPhaseCounter <= 0)
				{
					this.DieWithLoot();
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_RISING)
			{
				this.mAltitude += 24f;
				if (this.mAltitude >= 600f)
				{
					this.DieNoLoot(false);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_CUTSCENE)
			{
				this.mAltitude = (float)TodCommon.TodAnimateCurve(200, 0, this.mPhaseCounter, 40, 0, TodCurves.CURVE_SIN_WAVE);
				if (this.mPhaseCounter <= 0)
				{
					this.mPhaseCounter = 200;
				}
			}
			this.mX = (int)this.mPosX;
			this.mY = (int)this.mPosY;
		}

		public void BungeeLanding()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_DIVING && this.mAltitude < 1500f && !this.mApp.IsFinalBossLevel())
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_BUNGEE_SCREAM);
				this.mZombiePhase = ZombiePhase.PHASE_BUNGEE_DIVING_SCREAMING;
			}
			if (this.mAltitude > 40f)
			{
				return;
			}
			Plant plant = this.mBoard.FindUmbrellaPlant(this.mTargetCol, this.mRow);
			if (plant != null)
			{
				this.mApp.PlaySample(Resources.SOUND_BOING);
				this.mApp.PlayFoley(FoleyType.FOLEY_UMBRELLA);
				plant.DoSpecial();
				this.mZombiePhase = ZombiePhase.PHASE_BUNGEE_RISING;
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_TOP, 0, 1);
				this.mHitUmbrella = true;
				return;
			}
			this.mBoard.GetTopPlantAt(this.mTargetCol, this.mRow, PlantPriority.TOPPLANT_BUNGEE_ORDER);
			if (this.mAltitude > 0f)
			{
				return;
			}
			this.mAltitude = 0f;
			Zombie zombie = null;
			int num = this.mBoard.mZombies.IndexOf(this.mRelatedZombieID);
			if (num != -1)
			{
				zombie = this.mBoard.mZombies[num];
			}
			if (zombie != null)
			{
				zombie.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
				zombie.StartWalkAnim(0);
				this.mRelatedZombieID = null;
				this.mZombiePhase = ZombiePhase.PHASE_BUNGEE_RISING;
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_raise, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 36f);
				return;
			}
			this.mZombiePhase = ZombiePhase.PHASE_BUNGEE_AT_BOTTOM;
			this.mPhaseCounter = 300;
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 5, 24f);
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			reanimation.mAnimTime = 0.5f;
		}

		public bool EffectedByDamage(uint theDamageRangeFlags)
		{
			if (!TodCommon.TestBit(theDamageRangeFlags, 5) && this.IsDeadOrDying())
			{
				return false;
			}
			if (TodCommon.TestBit(theDamageRangeFlags, 7))
			{
				if (!this.mMindControlled)
				{
					return false;
				}
			}
			else if (this.mMindControlled)
			{
				return false;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE && this.mZombiePhase != ZombiePhase.PHASE_BUNGEE_AT_BOTTOM && this.mZombiePhase != ZombiePhase.PHASE_BUNGEE_GRABBING)
			{
				return false;
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_GETTING_BUNGEE_DROPPED)
			{
				return false;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_ENTER && reanimation.mAnimTime < 0.5f)
				{
					return false;
				}
				if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_LEAVE && reanimation.mAnimTime > 0.5f)
				{
					return false;
				}
				if (this.mZombiePhase != ZombiePhase.PHASE_BOSS_HEAD_IDLE_BEFORE_SPIT && this.mZombiePhase != ZombiePhase.PHASE_BOSS_HEAD_IDLE_AFTER_SPIT && this.mZombiePhase != ZombiePhase.PHASE_BOSS_HEAD_SPIT)
				{
					return false;
				}
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED && this.GetBobsledPosition() > 0)
			{
				return false;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || this.mZombiePhase == ZombiePhase.PHASE_IMP_GETTING_THROWN || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING_PAUSE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_IN_JUMP || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_BALLOON_POPPING || this.mZombiePhase == ZombiePhase.PHASE_RISING_FROM_GRAVE || this.mZombiePhase == ZombiePhase.PHASE_BOBSLED_CRASHING || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RISING)
			{
				return TodCommon.TestBit(theDamageRangeFlags, 4);
			}
			if (this.mZombieType != ZombieType.ZOMBIE_BOBSLED && this.mZombieType != ZombieType.ZOMBIE_BOSS && this.GetZombieRect().mX > Constants.WIDE_BOARD_WIDTH)
			{
				return false;
			}
			bool flag = this.mZombieType == ZombieType.ZOMBIE_SNORKEL && this.mInPool && !this.mIsEating;
			if (TodCommon.TestBit(theDamageRangeFlags, 2) && flag)
			{
				return true;
			}
			bool flag2 = this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING;
			return (TodCommon.TestBit(theDamageRangeFlags, 6) && flag2) || (TodCommon.TestBit(theDamageRangeFlags, 1) && this.IsFlying()) || (TodCommon.TestBit(theDamageRangeFlags, 0) && !this.IsFlying() && !flag && !flag2);
		}

		public void PickRandomSpeed()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL)
			{
				this.mVelX = 0.3f;
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_WALKING)
			{
				if (this.mApp.IsIZombieLevel())
				{
					this.mVelX = 0.23f;
				}
				else
				{
					this.mVelX = 0.12f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_IMP && this.mApp.IsIZombieLevel())
			{
				this.mVelX = 0.9f;
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_YETI_RUNNING)
			{
				this.mVelX = 0.8f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_YETI)
			{
				this.mVelX = 0.4f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER || this.mZombieType == ZombieType.ZOMBIE_POGO || this.mZombieType == ZombieType.ZOMBIE_FLAG)
			{
				this.mVelX = 0.45f;
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING || this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT || this.mZombieType == ZombieType.ZOMBIE_FOOTBALL || this.mZombieType == ZombieType.ZOMBIE_SNORKEL || this.mZombieType == ZombieType.ZOMBIE_JACK_IN_THE_BOX)
			{
				this.mVelX = TodCommon.RandRangeFloat(0.66f, 0.68f);
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_LADDER_CARRYING || this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD)
			{
				this.mVelX = TodCommon.RandRangeFloat(0.79f, 0.81f);
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_NEWSPAPER_MAD || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING_WITHOUT_DOLPHIN)
			{
				this.mVelX = TodCommon.RandRangeFloat(0.89f, 0.91f);
			}
			else
			{
				this.mVelX = TodCommon.RandRangeFloat(0.23f, 0.37f);
				if ((double)this.mVelX < 0.3)
				{
					this.mAnimTicksPerFrame = 12;
				}
				else
				{
					this.mAnimTicksPerFrame = 15;
				}
			}
			this.UpdateAnimSpeed();
		}

		public void UpdateZombiePolevaulter()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT && this.mHasHead && this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIE_NORMAL)
			{
				Plant plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_VAULT);
				if (plant != null)
				{
					if (this.mBoard.GetLadderAt(plant.mPlantCol, plant.mRow) != null)
					{
						if ((float)(this.mBoard.GridToPixelX(plant.mPlantCol, plant.mRow) + 40) > this.mPosX && this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIE_NORMAL && this.mUseLadderCol != plant.mPlantCol)
						{
							this.mZombieHeight = ZombieHeight.HEIGHT_UP_LADDER;
							this.mUseLadderCol = plant.mPlantCol;
						}
						return;
					}
					this.mZombiePhase = ZombiePhase.PHASE_POLEVAULTER_IN_VAULT;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jump, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
					Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
					float num = (float)reanimation.mFrameCount / reanimation.mAnimRate * 100f;
					int num2 = this.mX - plant.mX - 80;
					if (this.mApp.IsWallnutBowlingLevel())
					{
						num2 = 0;
					}
					this.mVelX = (float)num2 / num;
					this.mHasObject = false;
				}
				if (this.mApp.IsIZombieLevel() && this.mBoard.mChallenge.IZombieGetBrainTarget(this) != null)
				{
					this.mZombiePhase = ZombiePhase.PHASE_POLEVAULTER_POST_VAULT;
					this.StartWalkAnim(0);
					return;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT)
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
				bool flag = false;
				if (reanimation2.mAnimTime > 0.6f && reanimation2.mAnimTime <= 0.7f)
				{
					Plant plant2 = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_VAULT);
					if (plant2 != null && plant2.mSeedType == SeedType.SEED_TALLNUT)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_BONK);
						flag = true;
						this.mApp.AddTodParticle((float)(plant2.mX + 60), (float)(plant2.mY - 20), this.mRenderOrder + 1, ParticleEffect.PARTICLE_TALL_NUT_BLOCK);
						this.mPosX = (float)plant2.mX;
						this.mPosY -= 30f;
						this.mZombieHeight = ZombieHeight.HEIGHT_FALLING;
					}
				}
				if (reanimation2.mLoopCount > 0)
				{
					flag = true;
					this.mPosX -= 150f;
				}
				if (reanimation2.ShouldTriggerTimedEvent(0.2f))
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_GRASSSTEP);
				}
				if (reanimation2.ShouldTriggerTimedEvent(0.4f))
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_POLEVAULT);
				}
				if (flag)
				{
					this.mX = (int)this.mPosX;
					this.mZombiePhase = ZombiePhase.PHASE_POLEVAULTER_POST_VAULT;
					this.mZombieAttackRect = new TRect(50, 0, 20, 115);
					this.StartWalkAnim(0);
					return;
				}
				float num3 = this.mPosX;
				this.mPosX -= 150f * reanimation2.mAnimTime;
				this.mPosY = this.GetPosYBasedOnRow(this.mRow);
				this.mPosX = num3;
			}
		}

		public void UpdateZombieDolphinRider()
		{
			if (this.IsTangleKelpTarget())
			{
				return;
			}
			bool flag = this.IsWalkingBackwards();
			this.mUsesClipping = false;
			if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING && !flag)
			{
				if (this.mX > 800 && this.mX <= 820)
				{
					this.mZombiePhase = ZombiePhase.PHASE_DOLPHIN_INTO_POOL;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jumpinpool, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 16f);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_INTO_POOL)
			{
				this.mUsesClipping = true;
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.ShouldTriggerTimedEvent(0.56f))
				{
					Reanimation reanimation2 = this.mApp.AddReanimation((float)(this.mX - 83), (float)(this.mY + 73), this.mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
					reanimation2.OverrideScale(1.2f, 0.8f);
					this.mApp.AddTodParticle((float)(this.mX - 46), (float)(this.mY + 115), this.mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
					this.mApp.PlayFoley(FoleyType.FOLEY_ZOMBIE_ENTERING_WATER);
				}
				if (reanimation.mLoopCount > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_DOLPHIN_RIDING;
					this.mInPool = true;
					this.mPosX -= 70f;
					this.mZombieAttackRect = new TRect(-29, 0, 70, 115);
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_ride, ReanimLoopType.REANIM_LOOP_FULL_LAST_FRAME, 0, 12f);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_RIDING)
			{
				if (this.mX <= 120)
				{
					this.mZombieHeight = ZombieHeight.HEIGHT_OUT_OF_POOL;
					this.mZombiePhase = ZombiePhase.PHASE_DOLPHIN_WALKING;
					this.mAltitude = -40f;
					this.PoolSplash(false);
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_walkdolphin, ReanimLoopType.REANIM_LOOP, 0, 0f);
					this.PickRandomSpeed();
					return;
				}
				if (this.mHasHead && !this.IsTanglekelpTarget())
				{
					Plant plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_VAULT);
					if (plant != null)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_DOLPHIN_BEFORE_JUMPING);
						this.mApp.PlayFoley(FoleyType.FOLEY_PLANT_WATER);
						this.mZombiePhase = ZombiePhase.PHASE_DOLPHIN_IN_JUMP;
						this.mPhaseCounter = GameConstants.DOLPHIN_JUMP_TIME;
						this.mVelX = 0.5f;
						this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_dolphinjump, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 10f);
					}
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_IN_JUMP)
			{
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mBodyReanimID);
				bool flag2 = false;
				this.mAltitude = TodCommon.TodAnimateCurveFloat(GameConstants.DOLPHIN_JUMP_TIME, 0, this.mPhaseCounter, 0f, 10f, TodCurves.CURVE_LINEAR);
				if (reanimation3.ShouldTriggerTimedEvent(0.3f))
				{
					Plant plant2 = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_VAULT);
					if (plant2 != null && plant2.mSeedType == SeedType.SEED_TALLNUT)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_BONK);
						flag2 = true;
						this.mApp.AddTodParticle((float)(plant2.mX + 60), (float)(plant2.mY - 20), this.mRenderOrder + 1, ParticleEffect.PARTICLE_TALL_NUT_BLOCK);
						this.mPosX = (float)plant2.mX + 25f;
						this.mAltitude = 30f;
						this.mZombieHeight = ZombieHeight.HEIGHT_FALLING;
					}
				}
				else if (reanimation3.ShouldTriggerTimedEvent(0.49f))
				{
					Reanimation reanimation4 = this.mApp.AddReanimation((float)(this.mX - 63), (float)(this.mY + 73), this.mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
					reanimation4.OverrideScale(1.2f, 0.8f);
					this.mApp.AddTodParticle((float)(this.mX - 26), (float)(this.mY + 115), this.mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
					this.mApp.PlayFoley(FoleyType.FOLEY_ZOMBIE_ENTERING_WATER);
					this.mVelX = 0f;
				}
				else if (reanimation3.mLoopCount > 0)
				{
					flag2 = true;
					this.mPosX -= 94f;
					this.mAltitude = 0f;
				}
				if (flag2)
				{
					this.mZombiePhase = ZombiePhase.PHASE_DOLPHIN_WALKING_IN_POOL;
					this.mZombieAttackRect = new TRect(30, 0, 30, 115);
					this.mZombieRect = new TRect(20, 0, 42, 115);
					this.StartWalkAnim(0);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING_IN_POOL)
			{
				if (this.mX <= 140 && !flag)
				{
					this.mZombieHeight = ZombieHeight.HEIGHT_OUT_OF_POOL;
					this.mZombiePhase = ZombiePhase.PHASE_DOLPHIN_WALKING_WITHOUT_DOLPHIN;
					this.mAltitude = -40f;
					this.PoolSplash(false);
					this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, 0, 0f);
					this.PickRandomSpeed();
				}
				else if (this.mX > 770 && flag)
				{
					this.mZombieHeight = ZombieHeight.HEIGHT_OUT_OF_POOL;
					this.mZombiePhase = ZombiePhase.PHASE_DOLPHIN_WALKING_WITHOUT_DOLPHIN;
					this.mAltitude = -40f;
					this.PoolSplash(false);
					this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, 0, 0f);
					this.PickRandomSpeed();
				}
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING_WITHOUT_DOLPHIN)
			{
				this.mUsesClipping = (this.mAltitude < 0f);
			}
		}

		public void PickBungeeZombieTarget(int theColumn)
		{
			int num = this.CountBungeesTargetingSunFlowers();
			int num2 = this.mBoard.CountSunFlowers();
			bool flag = true;
			if (num == num2 - 1)
			{
				flag = false;
			}
			int num3 = 0;
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
						int mWeight = 1;
						if (this.mBoard.GetGraveStoneAt(j, k) == null && this.mBoard.mGridSquareType[j, k] != GridSquareType.GRIDSQUARE_DIRT)
						{
							Plant topPlantAt = this.mBoard.GetTopPlantAt(j, k, PlantPriority.TOPPLANT_BUNGEE_ORDER);
							if (topPlantAt != null)
							{
								if ((!flag && topPlantAt.MakesSun()) || topPlantAt.mSeedType == SeedType.SEED_GRAVEBUSTER || topPlantAt.mSeedType == SeedType.SEED_COBCANNON)
								{
									goto IL_10A;
								}
								mWeight = 10000;
							}
							if (!this.mBoard.BungeeIsTargetingCell(j, k))
							{
								Zombie.aPicks[num3].mX = j;
								Zombie.aPicks[num3].mY = k;
								Zombie.aPicks[num3].mWeight = mWeight;
								num3++;
							}
						}
						IL_10A:;
					}
				}
			}
			if (num3 == 0)
			{
				this.DieNoLoot(false);
				return;
			}
			TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(Zombie.aPicks, num3);
			this.mTargetCol = todWeightedGridArray.mX;
			this.SetRow(todWeightedGridArray.mY);
			this.mPosX = (float)this.mBoard.GridToPixelX(this.mTargetCol, this.mRow);
			this.mPosY = this.GetPosYBasedOnRow(this.mRow);
		}

		public int CountBungeesTargetingSunFlowers()
		{
			int num = 0;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && !zombie.IsDeadOrDying() && zombie.mZombieType == ZombieType.ZOMBIE_BUNGEE && zombie.mTargetCol != -1)
				{
					Plant topPlantAt = this.mBoard.GetTopPlantAt(zombie.mTargetCol, zombie.mRow, PlantPriority.TOPPLANT_BUNGEE_ORDER);
					if (topPlantAt != null && topPlantAt.MakesSun())
					{
						num++;
					}
				}
			}
			return num;
		}

		public Plant FindPlantTarget(Zombie.ZombieAttackType theAttackType)
		{
			TRect zombieAttackRect = this.GetZombieAttackRect();
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && this.mRow == plant.mRow)
				{
					TRect plantRect = plant.GetPlantRect();
					int rectOverlap = GameConstants.GetRectOverlap(zombieAttackRect, plantRect);
					int num = (this.mZombieType == ZombieType.ZOMBIE_DIGGER) ? 5 : 20;
					if (rectOverlap >= num && this.CanTargetPlant(plant, theAttackType))
					{
						return plant;
					}
				}
			}
			return null;
		}

		public void CheckSquish(Zombie.ZombieAttackType theAttackType)
		{
			TRect zombieAttackRect = this.GetZombieAttackRect();
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && this.mRow == plant.mRow)
				{
					TRect plantRect = plant.GetPlantRect();
					int rectOverlap = GameConstants.GetRectOverlap(zombieAttackRect, plantRect);
					if (rectOverlap >= 20 && this.CanTargetPlant(plant, theAttackType) && !plant.IsSpiky())
					{
						this.SquishAllInSquare(plant.mPlantCol, plant.mRow, theAttackType);
						break;
					}
				}
			}
			if (this.mApp.IsIZombieLevel())
			{
				GridItem gridItem = this.mBoard.mChallenge.IZombieGetBrainTarget(this);
				if (gridItem != null)
				{
					this.mBoard.mChallenge.IZombieSquishBrain(gridItem);
				}
			}
		}

		public void RiseFromGrave(int theCol, int theRow)
		{
			Debug.ASSERT(this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_NORMAL);
			this.mPosX = (float)(this.mBoard.GridToPixelX(theCol, this.mRow) - 25);
			this.mPosY = this.GetPosYBasedOnRow(theRow);
			this.SetRow(theRow);
			this.mX = (int)this.mPosX;
			this.mY = (int)this.mPosY;
			this.mZombiePhase = ZombiePhase.PHASE_RISING_FROM_GRAVE;
			this.mPhaseCounter = 150;
			this.mAltitude = -200f;
			this.mUsesClipping = true;
			if (this.mBoard.StageHasPool())
			{
				this.mInPool = true;
				this.mPhaseCounter = 50;
				this.mAltitude = -150f;
				this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
				this.StartWalkAnim(0);
				this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_duckytube, false);
				this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_whitewater, false);
				this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, false);
				this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm3, false);
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				TodParticleSystem todParticleSystem = this.mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.PARTICLE_ZOMBIE_SEAWEED);
				this.OverrideParticleScale(todParticleSystem);
				if (this.mZombieType == ZombieType.ZOMBIE_TRAFFIC_CONE && todParticleSystem != null)
				{
					reanimation.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_cone, ref todParticleSystem, 37f * Constants.S, 20f * Constants.S);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_PAIL && todParticleSystem != null)
				{
					reanimation.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_bucket, ref todParticleSystem, 37f * Constants.S, 20f * Constants.S);
				}
				else if (todParticleSystem != null)
				{
					reanimation.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head1, ref todParticleSystem, 30f * Constants.S, 20f * Constants.S);
				}
				this.PoolSplash(false);
				return;
			}
			int num = (int)this.mPosX + 60;
			int num2 = (int)this.mPosY + 110;
			if (this.IsOnHighGround())
			{
				num2 -= Constants.HIGH_GROUND_HEIGHT;
			}
			int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, theRow, 0);
			ParticleEffect theEffect = ParticleEffect.PARTICLE_ZOMBIE_RISE;
			if (this.mApp.IsWhackAZombieLevel())
			{
				theEffect = ParticleEffect.PARTICLE_WHACK_A_ZOMBIE_RISE;
				this.mApp.PlayFoley(FoleyType.FOLEY_DIRT_RISE);
			}
			else
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_GRAVESTONE_RUMBLE);
			}
			this.mApp.AddTodParticle((float)num, (float)num2, aRenderOrder, theEffect);
		}

		public void UpdateZombieRiseFromGrave()
		{
			if (this.mInPool)
			{
				this.mBodyReanimID.mClip = false;
				this.mAltitude = (float)TodCommon.TodAnimateCurve(50, 0, this.mPhaseCounter, -150, -40, TodCurves.CURVE_LINEAR) * this.mScaleZombie;
				this.mUsesClipping = true;
			}
			else
			{
				this.mBodyReanimID.mClip = true;
				this.mAltitude = (float)TodCommon.TodAnimateCurve(50, 0, this.mPhaseCounter, -200, 0, TodCurves.CURVE_LINEAR);
				this.mUsesClipping = (this.mAltitude < 0f);
			}
			if (this.mPhaseCounter <= 0)
			{
				this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
				if (this.IsOnHighGround())
				{
					this.mAltitude = (float)Constants.HIGH_GROUND_HEIGHT;
				}
				if (this.mInPool)
				{
					this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_duckytube, true);
					this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_whitewater, true);
					this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, true);
					this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm3, true);
				}
			}
		}

		public void UpdateDamageStates(uint theDamageFlags)
		{
			if (!this.CanLoseBodyParts())
			{
				return;
			}
			if (this.mHasArm && this.mBodyHealth < 2 * this.mBodyMaxHealth / 3 && this.mBodyHealth > 0)
			{
				this.DropArm(theDamageFlags);
			}
			if (this.mHasHead && this.mBodyHealth < this.mBodyMaxHealth / 3)
			{
				this.DropHead(theDamageFlags);
				this.DropLoot();
				this.StopZombieSound();
				if (this.mBoard.HasLevelAwardDropped())
				{
					this.PlayDeathAnim(theDamageFlags);
				}
				if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL)
				{
					this.DieNoLoot(false);
				}
			}
		}

		public void UpdateZombiePool()
		{
			if (this.mZombieHeight == ZombieHeight.HEIGHT_OUT_OF_POOL)
			{
				this.mAltitude += 3f;
				if (this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
				{
					this.mAltitude += 3f;
				}
				if (this.mAltitude >= 0f)
				{
					this.mAltitude = 0f;
					this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
					this.mInPool = false;
					return;
				}
			}
			else if (this.mZombieHeight == ZombieHeight.HEIGHT_IN_TO_POOL)
			{
				this.mAltitude -= 3f;
				int num = -40;
				num *= (int)this.mScaleZombie;
				if (this.mAltitude <= (float)num)
				{
					this.mAltitude = (float)num;
					this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
					this.StartWalkAnim(0);
					return;
				}
			}
			else if (this.mZombieHeight == ZombieHeight.HEIGHT_DRAGGED_UNDER)
			{
				this.mAltitude -= 3f;
			}
		}

		public void CheckForPool()
		{
			if (!Zombie.ZombieTypeCanGoInPool(this.mZombieType))
			{
				return;
			}
			if (this.IsFlying())
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER || this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
			{
				return;
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_IN_TO_POOL || this.mZombieHeight == ZombieHeight.HEIGHT_OUT_OF_POOL)
			{
				return;
			}
			int theGridX = this.mBoard.PixelToGridX(this.mX + 75, this.mY);
			int theGridX2 = this.mBoard.PixelToGridX(this.mX + 45, this.mY);
			bool flag = false;
			if (this.mBoard.IsPoolSquare(theGridX, this.mRow) && this.mBoard.IsPoolSquare(theGridX2, this.mRow) && this.mPosX < 800f)
			{
				flag = true;
			}
			if (!this.mInPool && flag)
			{
				if (this.mBoard.mIceTrapCounter > 0)
				{
					this.mIceTrapCounter = this.mBoard.mIceTrapCounter;
					this.ApplyChill(true);
					return;
				}
				this.mZombieHeight = ZombieHeight.HEIGHT_IN_TO_POOL;
				this.mInPool = true;
				this.PoolSplash(true);
			}
			else if (this.mInPool && !flag)
			{
				this.mZombieHeight = ZombieHeight.HEIGHT_OUT_OF_POOL;
				this.StartWalkAnim(0);
				this.PoolSplash(false);
			}
			if (flag)
			{
				this.mUsesClipping = true;
			}
		}

		public void GetDrawPos(ref ZombieDrawPosition theDrawPos)
		{
			theDrawPos.mImageOffsetX = this.mPosX - (float)this.mX;
			theDrawPos.mImageOffsetY = this.mPosY - (float)this.mY;
			if (this.mIsEating)
			{
				theDrawPos.mHeadX = 47;
				theDrawPos.mHeadY = 4;
			}
			else if (this.mFrame == 0)
			{
				theDrawPos.mHeadX = 50;
				theDrawPos.mHeadY = 2;
			}
			else if (this.mFrame == 1)
			{
				theDrawPos.mHeadX = 49;
				theDrawPos.mHeadY = 1;
			}
			else if (this.mFrame == 2)
			{
				theDrawPos.mHeadX = 49;
				theDrawPos.mHeadY = 2;
			}
			else if (this.mFrame == 3)
			{
				theDrawPos.mHeadX = 48;
				theDrawPos.mHeadY = 4;
			}
			else if (this.mFrame == 4)
			{
				theDrawPos.mHeadX = 48;
				theDrawPos.mHeadY = 5;
			}
			else if (this.mFrame == 5)
			{
				theDrawPos.mHeadX = 48;
				theDrawPos.mHeadY = 4;
			}
			else if (this.mFrame == 6)
			{
				theDrawPos.mHeadX = 48;
				theDrawPos.mHeadY = 2;
			}
			else if (this.mFrame == 7)
			{
				theDrawPos.mHeadX = 49;
				theDrawPos.mHeadY = 1;
			}
			else if (this.mFrame == 8)
			{
				theDrawPos.mHeadX = 49;
				theDrawPos.mHeadY = 2;
			}
			else if (this.mFrame == 9)
			{
				theDrawPos.mHeadX = 50;
				theDrawPos.mHeadY = 4;
			}
			else if (this.mFrame == 10)
			{
				theDrawPos.mHeadX = 50;
				theDrawPos.mHeadY = 5;
			}
			else
			{
				theDrawPos.mHeadX = 50;
				theDrawPos.mHeadY = 4;
			}
			theDrawPos.mArmY = theDrawPos.mHeadY / 2;
			if (this.mZombieType != ZombieType.ZOMBIE_DOLPHIN_RIDER)
			{
				if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
				{
					theDrawPos.mImageOffsetY += -16f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_YETI)
				{
					theDrawPos.mImageOffsetY += -20f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
				{
					theDrawPos.mImageOffsetX += -25f;
					theDrawPos.mImageOffsetY += -18f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_POGO)
				{
					theDrawPos.mImageOffsetY += 16f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
				{
					theDrawPos.mImageOffsetY += 17f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
				{
					theDrawPos.mImageOffsetX += -6f;
					theDrawPos.mImageOffsetY += -11f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
				{
					theDrawPos.mImageOffsetX += 68f;
					theDrawPos.mImageOffsetY += -23f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
				{
					theDrawPos.mImageOffsetY += -8f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
				{
					theDrawPos.mImageOffsetY += -12f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
				{
					theDrawPos.mImageOffsetY += 15f;
				}
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_RISING_FROM_GRAVE)
			{
				theDrawPos.mBodyY = -this.mAltitude;
				if (this.mInPool)
				{
					theDrawPos.mClipHeight = theDrawPos.mBodyY;
				}
				else
				{
					float num = Math.Min((float)this.mPhaseCounter, 40f);
					theDrawPos.mClipHeight = theDrawPos.mBodyY + num;
				}
				if (this.IsOnHighGround())
				{
					theDrawPos.mBodyY -= (float)Constants.HIGH_GROUND_HEIGHT;
					return;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER)
			{
				theDrawPos.mBodyY = -this.mAltitude;
				theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
				if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_INTO_POOL)
				{
					Reanimation reanimation = this.mBodyReanimID;
					if (reanimation.mAnimTime >= 0.56f && reanimation.mAnimTime <= 0.65f)
					{
						theDrawPos.mClipHeight = 0f;
						return;
					}
					if (reanimation.mAnimTime >= 0.75f)
					{
						theDrawPos.mClipHeight = -this.mAltitude - 10f;
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_RIDING)
				{
					theDrawPos.mImageOffsetX += 70f;
					if (this.mZombieHeight == ZombieHeight.HEIGHT_DRAGGED_UNDER)
					{
						theDrawPos.mClipHeight = -this.mAltitude - 15f;
						return;
					}
					theDrawPos.mClipHeight = -this.mAltitude - 10f;
					return;
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_IN_JUMP)
				{
					theDrawPos.mImageOffsetX += 70f + this.mAltitude;
					Reanimation reanimation2 = this.mBodyReanimID;
					if (reanimation2.mAnimTime <= 0.06f)
					{
						theDrawPos.mClipHeight = -this.mAltitude - 10f;
						return;
					}
					if (reanimation2.mAnimTime >= 0.5f && reanimation2.mAnimTime <= 0.76f)
					{
						theDrawPos.mClipHeight = -13f;
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING_IN_POOL || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING)
				{
					theDrawPos.mImageOffsetY += 50f;
					if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING)
					{
						theDrawPos.mClipHeight = -this.mAltitude + 44f;
						return;
					}
					if (this.mZombieHeight == ZombieHeight.HEIGHT_DRAGGED_UNDER)
					{
						theDrawPos.mClipHeight = -this.mAltitude + 36f;
						return;
					}
				}
				else
				{
					if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING && this.mZombieHeight == ZombieHeight.HEIGHT_OUT_OF_POOL)
					{
						theDrawPos.mClipHeight = -this.mAltitude;
						return;
					}
					if (this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING_WITHOUT_DOLPHIN && this.mZombieHeight == ZombieHeight.HEIGHT_OUT_OF_POOL)
					{
						theDrawPos.mClipHeight = -this.mAltitude;
						return;
					}
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
			{
				theDrawPos.mBodyY = -this.mAltitude;
				theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
				if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL)
				{
					Reanimation reanimation3 = this.mBodyReanimID;
					if (reanimation3.mAnimTime >= 0.8f)
					{
						theDrawPos.mClipHeight = -10f;
						return;
					}
				}
				else if (this.mInPool)
				{
					theDrawPos.mClipHeight = -this.mAltitude - 5f;
					theDrawPos.mClipHeight += 20f - 20f * this.mScaleZombie;
					return;
				}
			}
			else if (this.mInPool)
			{
				theDrawPos.mBodyY = -this.mAltitude;
				theDrawPos.mClipHeight = -this.mAltitude - 7f;
				theDrawPos.mClipHeight += 10f - 10f * this.mScaleZombie;
				if (this.mIsEating)
				{
					theDrawPos.mClipHeight += 7f;
					return;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_RISING)
			{
				theDrawPos.mBodyY = -this.mAltitude;
				theDrawPos.mClipHeight = -this.mAltitude;
				if (this.IsOnHighGround())
				{
					theDrawPos.mBodyY -= (float)Constants.HIGH_GROUND_HEIGHT;
					return;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE)
			{
				theDrawPos.mBodyY = -this.mAltitude;
				if (this.mPhaseCounter > 20)
				{
					theDrawPos.mClipHeight = -this.mAltitude;
					return;
				}
				theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
				return;
			}
			else
			{
				if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
				{
					theDrawPos.mBodyY = -this.mAltitude;
					theDrawPos.mImageOffsetX += -18f;
					if (this.IsOnHighGround())
					{
						theDrawPos.mBodyY -= (float)Constants.HIGH_GROUND_HEIGHT;
					}
					theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
					return;
				}
				theDrawPos.mBodyY = -this.mAltitude;
				theDrawPos.mClipHeight = GameConstants.CLIP_HEIGHT_OFF;
			}
		}

		public void UpdateZombieHighGround()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				return;
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_UP_TO_HIGH_GROUND)
			{
				this.mAltitude += 3f;
				if (this.mAltitude >= (float)Constants.HIGH_GROUND_HEIGHT)
				{
					this.mAltitude = (float)Constants.HIGH_GROUND_HEIGHT;
					this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
					return;
				}
			}
			else if (this.mZombieHeight == ZombieHeight.HEIGHT_DOWN_OFF_HIGH_GROUND)
			{
				this.mAltitude -= 3f;
				if (this.mAltitude <= 0f)
				{
					this.mAltitude = 0f;
					this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
					this.mOnHighGround = false;
				}
			}
		}

		public void CheckForHighGround()
		{
			if (this.mZombieHeight != ZombieHeight.HEIGHT_ZOMBIE_NORMAL)
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				return;
			}
			bool flag = this.IsOnHighGround();
			if (!this.mOnHighGround && flag)
			{
				this.mZombieHeight = ZombieHeight.HEIGHT_UP_TO_HIGH_GROUND;
				this.mOnHighGround = true;
				return;
			}
			if (this.mOnHighGround && !flag)
			{
				this.mZombieHeight = ZombieHeight.HEIGHT_DOWN_OFF_HIGH_GROUND;
			}
		}

		public bool IsOnHighGround()
		{
			if (!this.IsOnBoard())
			{
				return false;
			}
			int num = this.mBoard.PixelToGridXKeepOnBoard(this.mX + 75, this.mY);
			return this.mBoard.mGridSquareType[num, this.mRow] == GridSquareType.GRIDSQUARE_HIGH_GROUND;
		}

		public void DropLoot()
		{
			if (!this.IsOnBoard())
			{
				return;
			}
			AlmanacDialog.AlmanacPlayerDefeatedZombie(this.mZombieType);
			if (this.mZombieType == ZombieType.ZOMBIE_YETI)
			{
				this.mBoard.mKilledYeti = true;
			}
			this.TrySpawnLevelAward();
			if (this.mDroppedLoot)
			{
				return;
			}
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			if (!this.mBoard.CanDropLoot())
			{
				return;
			}
			this.mDroppedLoot = true;
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(this.mZombieType);
			int mZombieValue = zombieDefinition.mZombieValue;
			if (this.mApp.IsLittleTroubleLevel() && RandomNumbers.NextNumber(4) != 0)
			{
				return;
			}
			if (this.mApp.IsIZombieLevel())
			{
				return;
			}
			TRect zombieRect = this.GetZombieRect();
			int num = zombieRect.mX + zombieRect.mWidth / 2;
			int num2 = zombieRect.mY + zombieRect.mHeight / 4;
			if (this.mZombieType == ZombieType.ZOMBIE_YETI)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
				this.mBoard.AddCoin(num - 20, num2, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
				this.mBoard.AddCoin(num - 30, num2, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
				this.mBoard.AddCoin(num - 40, num2, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
				this.mBoard.AddCoin(num - 50, num2, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
				return;
			}
			this.mBoard.DropLootPiece(num, num2, mZombieValue);
		}

		public bool TrySpawnLevelAward()
		{
			if (!this.IsOnBoard())
			{
				return false;
			}
			if (this.mBoard.HasLevelAwardDropped())
			{
				return false;
			}
			if (this.mBoard.mLevelComplete)
			{
				return false;
			}
			if (this.mDroppedLoot)
			{
				return false;
			}
			if (this.mApp.IsFinalBossLevel())
			{
				if (this.mZombieType != ZombieType.ZOMBIE_BOSS)
				{
					return false;
				}
			}
			else if (this.mApp.IsScaryPotterLevel())
			{
				if (!this.mBoard.mChallenge.ScaryPotterIsCompleted())
				{
					return false;
				}
			}
			else
			{
				if (this.mApp.IsContinuousChallenge())
				{
					return false;
				}
				if (this.mBoard.mCurrentWave < this.mBoard.mNumWaves)
				{
					return false;
				}
				if (this.mBoard.AreEnemyZombiesOnScreen())
				{
					return false;
				}
			}
			if (this.mApp.IsWhackAZombieLevel() && this.mBoard.mZombieCountDown > 0)
			{
				return false;
			}
			this.mBoard.mLevelAwardSpawned = true;
			this.mApp.mBoardResult = BoardResult.BOARDRESULT_WON;
			TRect zombieRect = this.GetZombieRect();
			int num = zombieRect.mX + zombieRect.mWidth / 2;
			int theY = zombieRect.mY + zombieRect.mHeight / 2;
			if (!this.mBoard.IsSurvivalStageWithRepick())
			{
				this.mBoard.RemoveAllZombies();
			}
			CoinType coinType;
			if (this.mApp.IsScaryPotterLevel() && !this.mBoard.IsFinalScaryPotterStage())
			{
				coinType = CoinType.COIN_NONE;
				int theGridX = this.mBoard.PixelToGridXKeepOnBoard((int)this.mPosX + 75, (int)this.mPosY);
				this.mBoard.mChallenge.PuzzlePhaseComplete(theGridX, this.mRow);
			}
			else if (this.mApp.IsAdventureMode() && this.mBoard.mLevel <= 50)
			{
				if (this.mBoard.mLevel == 9 || this.mBoard.mLevel == 19 || this.mBoard.mLevel == 29 || this.mBoard.mLevel == 39 || this.mBoard.mLevel == 49)
				{
					coinType = CoinType.COIN_NOTE;
				}
				else if (this.mBoard.mLevel == 50)
				{
					if (this.mApp.HasFinishedAdventure())
					{
						coinType = CoinType.COIN_AWARD_MONEY_BAG;
					}
					else
					{
						coinType = CoinType.COIN_AWARD_MONEY_BAG;
					}
				}
				else if (this.mApp.HasFinishedAdventure())
				{
					coinType = CoinType.COIN_AWARD_MONEY_BAG;
				}
				else if (this.mBoard.mLevel == 4)
				{
					coinType = CoinType.COIN_SHOVEL;
				}
				else if (this.mBoard.mLevel == 14)
				{
					coinType = CoinType.COIN_ALMANAC;
				}
				else if (this.mBoard.mLevel == 24)
				{
					coinType = CoinType.COIN_CARKEYS;
				}
				else if (this.mBoard.mLevel == 34)
				{
					coinType = CoinType.COIN_TACO;
				}
				else if (this.mBoard.mLevel == 44)
				{
					coinType = CoinType.COIN_WATERING_CAN;
				}
				else
				{
					coinType = CoinType.COIN_FINAL_SEED_PACKET;
				}
			}
			else if (this.mBoard.IsSurvivalStageWithRepick())
			{
				coinType = CoinType.COIN_NONE;
				this.mBoard.FadeOutLevel();
			}
			else if (this.mApp.IsQuickPlayMode())
			{
				coinType = CoinType.COIN_AWARD_MONEY_BAG;
			}
			else if (this.mBoard.IsLastStandStageWithRepick())
			{
				coinType = CoinType.COIN_NONE;
				this.mBoard.FadeOutLevel();
				this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
				for (int i = 0; i < 10; i++)
				{
					this.mBoard.AddCoin(num + i * 5, theY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
				}
			}
			else if (!this.mApp.IsAdventureMode())
			{
				if (this.mApp.HasBeatenChallenge(this.mApp.mGameMode))
				{
					coinType = CoinType.COIN_AWARD_MONEY_BAG;
				}
				else
				{
					coinType = CoinType.COIN_TROPHY;
				}
			}
			else
			{
				coinType = CoinType.COIN_AWARD_MONEY_BAG;
			}
			CoinMotion theCoinMotion = CoinMotion.COIN_MOTION_COIN;
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				theCoinMotion = CoinMotion.COIN_MOTION_FROM_BOSS;
			}
			if (coinType != CoinType.COIN_NONE)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
				this.mBoard.AddCoin(num, theY, coinType, theCoinMotion);
			}
			this.mDroppedLoot = true;
			return true;
		}

		public void StartZombieSound()
		{
			if (this.mPlayingSong)
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_JACK_IN_THE_BOX_RUNNING && this.mHasHead)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_JACKINTHEBOX);
				this.mPlayingSong = true;
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_DIGGER);
				this.mPlayingSong = true;
			}
		}

		public void StopZombieSound()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				if (this.mApp.mBoard != null)
				{
					bool flag = false;
					int count = this.mApp.mBoard.mZombies.Count;
					for (int i = 0; i < count; i++)
					{
						Zombie zombie = this.mApp.mBoard.mZombies[i];
						if (!zombie.mDead && zombie.mHasHead && !zombie.IsDeadOrDying() && zombie.IsOnBoard() && (zombie.mZombieType == ZombieType.ZOMBIE_DANCER || zombie.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.mApp.mSoundSystem.StopFoley(FoleyType.FOLEY_DANCER);
					}
				}
				else
				{
					this.mApp.mSoundSystem.StopFoley(FoleyType.FOLEY_DANCER);
				}
			}
			if (!this.mPlayingSong)
			{
				return;
			}
			this.mPlayingSong = false;
			if (this.mZombieType == ZombieType.ZOMBIE_JACK_IN_THE_BOX)
			{
				this.mApp.mSoundSystem.StopFoley(FoleyType.FOLEY_JACKINTHEBOX);
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				this.mApp.mSoundSystem.StopFoley(FoleyType.FOLEY_DIGGER);
			}
		}

		public void UpdateZombieJackInTheBox()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_JACK_IN_THE_BOX_RUNNING)
			{
				if (this.mPhaseCounter <= 0 && this.mHasHead)
				{
					this.mPhaseCounter = 110;
					this.mZombiePhase = ZombiePhase.PHASE_JACK_IN_THE_BOX_POPPING;
					this.StopZombieSound();
					this.mApp.PlaySample(Resources.SOUND_BOING);
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_pop, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 28f);
					return;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_JACK_IN_THE_BOX_POPPING)
			{
				if (this.mPhaseCounter == 80)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_JACK_SURPRISE);
				}
				if (this.mPhaseCounter <= 0)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_EXPLOSION);
					int num = this.mX + this.mWidth / 2;
					int num2 = this.mY + this.mHeight / 2;
					int num3 = 127;
					if (this.mMindControlled)
					{
						this.mBoard.KillAllZombiesInRadius(this.mRow, num, num2, Constants.JackInTheBoxZombieRadius, 1, true, num3);
					}
					else
					{
						num3 |= 128;
						this.mBoard.KillAllZombiesInRadius(this.mRow, num, num2, Constants.JackInTheBoxZombieRadius, 1, true, num3);
						this.mBoard.KillAllPlantsInRadius(num, num2, Constants.JackInTheBoxPlantRadius);
					}
					this.mApp.AddTodParticle((float)num, (float)num2, 400000, ParticleEffect.PARTICLE_JACKEXPLODE);
					this.mBoard.ShakeBoard(4, -6);
					this.DieNoLoot(false);
					if (this.mApp.IsScaryPotterLevel())
					{
						this.mBoard.mChallenge.ScaryPotterJackExplode(num, num2);
					}
				}
			}
		}

		public void DrawZombieHead(Graphics g, ref ZombieDrawPosition theDrawPos, int theFrame)
		{
		}

		public void UpdateZombiePosition()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE || this.mZombieType == ZombieType.ZOMBIE_BOSS || this.mZombiePhase == ZombiePhase.PHASE_RISING_FROM_GRAVE || this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIQUARIUM)
			{
				return;
			}
			this.UpdateZombieWalking();
			this.CheckForZombieStep();
			if (this.mBlowingAway)
			{
				this.mPosX += 30f;
				if (this.mX > 850)
				{
					this.DieWithLoot();
					return;
				}
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIE_NORMAL)
			{
				float posYBasedOnRow = this.GetPosYBasedOnRow(this.mRow);
				if (this.mPosY < posYBasedOnRow)
				{
					this.mPosY += 3f * Math.Min(1f, posYBasedOnRow - this.mPosY);
					if (this.mPosY > posYBasedOnRow)
					{
						this.mPosY = posYBasedOnRow;
						return;
					}
				}
				else if (this.mPosY > posYBasedOnRow)
				{
					this.mPosY -= 3f * Math.Min(1f, this.mPosY - posYBasedOnRow);
					if (this.mPosY < posYBasedOnRow)
					{
						this.mPosY = posYBasedOnRow;
					}
				}
			}
		}

		public TRect GetZombieRect()
		{
			if (this.cachedZombieRectUpToDate)
			{
				return this.cachedZombieRect;
			}
			this.cachedZombieRect = this.mZombieRect;
			if (this.IsWalkingBackwards())
			{
				this.cachedZombieRect.mX = this.mWidth - this.cachedZombieRect.mX - this.cachedZombieRect.mWidth;
			}
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			this.cachedZombieRect.Offset(this.mX, this.mY + (int)zombieDrawPosition.mBodyY);
			if (zombieDrawPosition.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
			{
				this.cachedZombieRect.mHeight = this.cachedZombieRect.mHeight - (int)zombieDrawPosition.mClipHeight;
			}
			this.cachedZombieRectUpToDate = true;
			return this.cachedZombieRect;
		}

		public TRect GetZombieAttackRect()
		{
			TRect result = this.mZombieAttackRect;
			if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_IN_JUMP)
			{
				result = new TRect(-40, 0, 100, 115);
			}
			if (this.IsWalkingBackwards())
			{
				result.mX = this.mWidth - result.mX - result.mWidth;
			}
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			result.Offset(this.mX, this.mY + (int)zombieDrawPosition.mBodyY);
			if (zombieDrawPosition.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
			{
				result.mHeight -= (int)zombieDrawPosition.mClipHeight;
			}
			return result;
		}

		public void UpdateZombieWalking()
		{
			if (this.ZombieNotWalking())
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation != null)
			{
				float num;
				if (this.IsBouncingPogo() || this.mZombiePhase == ZombiePhase.PHASE_BALLOON_FLYING || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_RIDING || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL || this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
				{
					num = this.mVelX * 3f;
					if (this.IsMovingAtChilledSpeed())
					{
						num *= GameConstants.CHILLED_SPEED_FACTOR;
					}
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_IN_JUMP || this.IsBobsledTeamWithSled() || this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL)
				{
					num = this.mVelX * 3f;
				}
				else if (this.mHasGroundTrack)
				{
					this.mGroundTrackIndex = reanimation.GetTrackIndex(Reanimation.ReanimTrackId__ground);
					num = reanimation.GetTrackVelocity(this.mGroundTrackIndex) * this.mScaleZombie;
				}
				else
				{
					num = this.mVelX * 3f;
					if (this.IsMovingAtChilledSpeed())
					{
						num *= GameConstants.CHILLED_SPEED_FACTOR;
					}
				}
				if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
				{
					ZombieType zombieType = this.mZombieType;
					if (num > 0.3f)
					{
						num = 0.3f;
					}
				}
				if (this.IsWalkingBackwards() || this.mZombiePhase == ZombiePhase.PHASE_DANCER_DANCING_IN)
				{
					this.mPosX += num;
				}
				else
				{
					this.mPosX -= num;
				}
				if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL && this.mFromWave != GameConstants.ZOMBIE_WAVE_WINNER)
				{
					if (reanimation.ShouldTriggerTimedEvent(0.03f))
					{
						this.mApp.AddTodParticle((float)(this.mX + 81), (float)(this.mY + 106), this.mRenderOrder - 1, ParticleEffect.PARTICLE_DUST_FOOT);
					}
					if (reanimation.ShouldTriggerTimedEvent(0.61f))
					{
						this.mApp.AddTodParticle((float)(this.mX + 87), (float)(this.mY + 110), this.mRenderOrder - 1, ParticleEffect.PARTICLE_DUST_FOOT);
					}
				}
				if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT)
				{
					if (reanimation.ShouldTriggerTimedEvent(0.16f))
					{
						this.mApp.AddTodParticle((float)(this.mX + 81), (float)(this.mY + 106), this.mRenderOrder - 1, ParticleEffect.PARTICLE_DUST_FOOT);
					}
					if (reanimation.ShouldTriggerTimedEvent(0.67f))
					{
						this.mApp.AddTodParticle((float)(this.mX + 87), (float)(this.mY + 110), this.mRenderOrder - 1, ParticleEffect.PARTICLE_DUST_FOOT);
					}
				}
				return;
			}
			bool flag = false;
			if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING || this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER || this.mZombieType == ZombieType.ZOMBIE_BOBSLED || this.mZombieType == ZombieType.ZOMBIE_POGO || this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER || this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				flag = true;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_SNORKEL && this.mInPool)
			{
				flag = true;
			}
			else if (this.mFrame >= 0 && this.mFrame <= 2)
			{
				flag = true;
			}
			else if (this.mFrame >= 6 && this.mFrame <= 8)
			{
				flag = true;
			}
			if (flag)
			{
				float num2 = this.mVelX * 3f;
				if (this.IsMovingAtChilledSpeed())
				{
					num2 *= GameConstants.CHILLED_SPEED_FACTOR;
				}
				if (this.IsWalkingBackwards())
				{
					this.mPosX += num2;
					return;
				}
				this.mPosX -= num2;
			}
		}

		public void UpdateZombieWalkingIntoHouse()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE || this.mZombieType == ZombieType.ZOMBIE_BOSS || this.mZombiePhase == ZombiePhase.PHASE_RISING_FROM_GRAVE || this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIQUARIUM)
			{
				return;
			}
			int num = 1;
			if (this.mZombieType == ZombieType.ZOMBIE_NORMAL || this.mZombieType == ZombieType.ZOMBIE_PAIL || this.mZombieType == ZombieType.ZOMBIE_TRAFFIC_CONE)
			{
				num = 2;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				num = 4;
			}
			num *= 3;
			while (num-- != 0)
			{
				this.UpdateZombieWalking();
				if (this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIE_NORMAL)
				{
					float num2 = GameConstants.ZOMBIE_WALK_IN_FRONT_DOOR_Y;
					float num3 = 1f;
					if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
					{
						num2 += 30f;
					}
					else if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT)
					{
						num2 += 35f;
					}
					else if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
					{
						num2 += 15f;
					}
					else if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
					{
						num2 += 15f;
						if (this.mRow == 0 || this.mRow == 1)
						{
							num3 = 2f;
						}
					}
					if (!Zombie.WinningZombieReachedDesiredY && (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR))
					{
						Zombie.WinningZombieReachedDesiredY = true;
						this.ReanimReenableClipping();
					}
					if (this.mPosY < num2)
					{
						this.mPosY += Math.Min(num3, num2 - this.mPosY);
					}
					else if (this.mPosY > num2)
					{
						this.mPosY -= Math.Min(num3, this.mPosY - num2);
					}
					else if (!Zombie.WinningZombieReachedDesiredY)
					{
						Zombie.WinningZombieReachedDesiredY = true;
						this.ReanimReenableClipping();
					}
				}
			}
		}

		public void UpdateZombieBobsled()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_BOBSLED_CRASHING)
			{
				if (this.mPhaseCounter <= 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
					if (this.GetBobsledPosition() == 0)
					{
						for (int i = 0; i < 3; i++)
						{
							Zombie zombie = this.mBoard.ZombieGet(this.mFollowerZombieID[i]);
							zombie.mRelatedZombieID = null;
							this.mFollowerZombieID[i] = null;
							zombie.PickRandomSpeed();
						}
						this.PickRandomSpeed();
					}
				}
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_BOBSLED_SLIDING)
			{
				if (this.mPhaseCounter >= 0 && this.mPhaseCounter < 3)
				{
					this.mZombiePhase = ZombiePhase.PHASE_BOBSLED_BOARDING;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jump, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 20f);
				}
			}
			else
			{
				if (this.mZombiePhase != ZombiePhase.PHASE_BOBSLED_BOARDING)
				{
					return;
				}
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				int theTimeAge = (int)(reanimation.mAnimTime * 50f);
				int bobsledPosition = this.GetBobsledPosition();
				if (bobsledPosition == 1 || bobsledPosition == 3)
				{
					this.mAltitude = TodCommon.TodAnimateCurveFloat(0, 50, theTimeAge, 8f, 18f, TodCurves.CURVE_LINEAR);
				}
				else
				{
					this.mAltitude = TodCommon.TodAnimateCurveFloat(0, 50, theTimeAge, -9f, 18f, TodCurves.CURVE_LINEAR);
				}
			}
			this.mBoard.mIceTimer[this.mRow] = Math.Max(500, this.mBoard.mIceTimer[this.mRow]);
			if (this.mPosX + 10f < (float)this.mBoard.mIceMinX[this.mRow] && this.GetBobsledPosition() == 0)
			{
				this.TakeDamage(6, 8U);
			}
		}

		public void BobsledCrash()
		{
			this.mZombiePhase = ZombiePhase.PHASE_BOBSLED_CRASHING;
			this.mPhaseCounter = GameConstants.BOBSLED_CRASH_TIME;
			this.mAltitude = 0f;
			this.mZombieRect = new TRect(36, 0, 42, 115);
			this.StartWalkAnim(0);
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			for (int i = 0; i < 3; i++)
			{
				Zombie zombie = this.mBoard.ZombieGet(this.mFollowerZombieID[i]);
				zombie.mZombiePhase = ZombiePhase.PHASE_BOBSLED_CRASHING;
				zombie.mPhaseCounter = GameConstants.BOBSLED_CRASH_TIME;
				zombie.mPosY = this.GetPosYBasedOnRow(this.mRow);
				zombie.mAltitude = 0f;
				zombie.StartWalkAnim(0);
				Reanimation reanimation2 = this.mApp.ReanimationGet(zombie.mBodyReanimID);
				if (reanimation2 != null)
				{
					zombie.mVelX = this.mVelX;
					reanimation2.mAnimTime = TodCommon.RandRangeFloat(0f, 1f);
					reanimation2.mAnimRate = reanimation.mAnimRate;
				}
			}
		}

		public Plant IsStandingOnSpikeweed()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				return null;
			}
			TRect zombieRect = this.GetZombieRect();
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && this.mRow == plant.mRow && plant.IsSpiky() && !plant.NotOnGround() && (!this.mOnHighGround || plant.IsOnHighGround()))
				{
					TRect plantAttackRect = plant.GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
					int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
					if (rectOverlap > 0)
					{
						return plant;
					}
				}
			}
			return null;
		}

		public void CheckForZombieStep()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				if (this.mFlatTires)
				{
					return;
				}
				this.CheckSquish(Zombie.ZombieAttackType.ATTACKTYPE_DRIVE_OVER);
			}
		}

		public void OverrideParticleColor(TodParticleSystem aParticle)
		{
			if (aParticle == null)
			{
				return;
			}
			if (this.mMindControlled)
			{
				aParticle.OverrideColor(null, GameConstants.ZOMBIE_MINDCONTROLLED_COLOR);
				aParticle.OverrideExtraAdditiveDraw(null, true);
				return;
			}
			if (this.mChilledCounter > 0 || this.mIceTrapCounter > 0)
			{
				aParticle.OverrideColor(null, new SexyColor(75, 75, 255, 255));
				aParticle.OverrideExtraAdditiveDraw(null, true);
			}
		}

		public void OverrideParticleScale(TodParticleSystem aParticle)
		{
			if (aParticle == null)
			{
				return;
			}
			aParticle.OverrideScale(null, this.mScaleZombie);
		}

		public void PoolSplash(bool theInToPoolSound)
		{
			float num = 23f;
			float num2 = 78f;
			if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL)
			{
				num += -37f;
				num2 += -8f;
			}
			int num3;
			if (this.mAltitude == 0f)
			{
				num3 = (int)(num2 * this.mScaleZombie);
			}
			else
			{
				num3 = (int)num2;
			}
			Reanimation reanimation = this.mApp.AddReanimation((float)(this.mX + (int)(num * this.mScaleZombie)), (float)(this.mY + num3), this.mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
			reanimation.OverrideScale(0.8f, 0.8f);
			this.mApp.AddTodParticle((float)this.mX + num + 37f, (float)this.mY + num2 + 42f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
			if (theInToPoolSound)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_ZOMBIESPLASH);
				return;
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_PLANT_WATER);
		}

		public void UpdateZombieFlyer()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY && this.mPosX < 720f + (float)Constants.BOARD_EXTRA_ROOM)
			{
				this.mAltitude -= 0.1f;
				if (this.mAltitude < -35f)
				{
					this.LandFlyer(0U);
				}
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_BALLOON_POPPING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_BALLOON_WALKING;
					this.StartWalkAnim(0);
				}
			}
			if (this.mApp.IsIZombieLevel() && this.mZombiePhase == ZombiePhase.PHASE_BALLOON_FLYING && this.mBoard.mChallenge.IZombieGetBrainTarget(this) != null)
			{
				this.LandFlyer(0U);
			}
		}

		public void UpdateZombiePogo()
		{
			if (this.IsDeadOrDying())
			{
				return;
			}
			if (this.IsImmobilizied())
			{
				return;
			}
			if (!this.IsBouncingPogo())
			{
				return;
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_IN_TO_CHIMNEY)
			{
				return;
			}
			float num = 40f;
			if (this.mZombiePhase >= ZombiePhase.PHASE_POGO_HIGH_BOUNCE_1 && this.mZombiePhase <= ZombiePhase.PHASE_POGO_HIGH_BOUNCE_6)
			{
				num = 50f + 20f * (float)(this.mZombiePhase - ZombiePhase.PHASE_POGO_HIGH_BOUNCE_1);
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_POGO_FORWARD_BOUNCE_2)
			{
				num = 90f;
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_POGO_FORWARD_BOUNCE_7)
			{
				num = 170f;
			}
			float num2 = 9f;
			this.mAltitude = TodCommon.TodAnimateCurveFloat(GameConstants.POGO_BOUNCE_TIME, 0, this.mPhaseCounter, num2, num + num2, TodCurves.CURVE_BOUNCE_SLOW_MIDDLE);
			this.mFrame = TodCommon.ClampInt(3 - (int)this.mAltitude / 3, 0, 3);
			if (this.mPhaseCounter >= 8 && this.mPhaseCounter < 11)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				reanimation.mAnimTime = 0f;
				reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
			}
			if (this.IsOnBoard() && this.mPhaseCounter >= 5 && this.mPhaseCounter < 8)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_POGO_ZOMBIE);
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_UP_TO_HIGH_GROUND)
			{
				this.mAltitude += (float)Constants.HIGH_GROUND_HEIGHT;
				this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
			}
			else if (this.mZombieHeight == ZombieHeight.HEIGHT_DOWN_OFF_HIGH_GROUND)
			{
				this.mOnHighGround = false;
				this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
			}
			else if (this.mOnHighGround)
			{
				this.mAltitude += (float)Constants.HIGH_GROUND_HEIGHT;
			}
			Plant plant;
			if (this.mZombiePhase == ZombiePhase.PHASE_POGO_FORWARD_BOUNCE_2 && this.mPhaseCounter >= 71 && this.mPhaseCounter < 74)
			{
				plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_VAULT);
				if (plant != null && plant.mSeedType == SeedType.SEED_TALLNUT)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_BONK);
					this.mApp.AddTodParticle((float)(plant.mX + 60), (float)(plant.mY - 20), this.mRenderOrder + 1, ParticleEffect.PARTICLE_TALL_NUT_BLOCK);
					this.mShieldType = ShieldType.SHIELDTYPE_NONE;
					this.PogoBreak(0U);
					return;
				}
			}
			if (this.mPhaseCounter > 0)
			{
				return;
			}
			plant = null;
			if (this.IsOnBoard())
			{
				plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_VAULT);
			}
			if (plant == null)
			{
				this.mZombiePhase = ZombiePhase.PHASE_POGO_BOUNCING;
				this.PickRandomSpeed();
				this.mPhaseCounter = GameConstants.POGO_BOUNCE_TIME;
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_POGO_HIGH_BOUNCE_1)
			{
				this.mZombiePhase = ZombiePhase.PHASE_POGO_FORWARD_BOUNCE_2;
				int num3 = this.mX - plant.mX + 60;
				this.mVelX = (float)num3 / (float)GameConstants.POGO_BOUNCE_TIME;
				this.mPhaseCounter = GameConstants.POGO_BOUNCE_TIME;
				return;
			}
			this.mZombiePhase = ZombiePhase.PHASE_POGO_HIGH_BOUNCE_1;
			this.mVelX = 0f;
			this.mPhaseCounter = GameConstants.POGO_BOUNCE_TIME;
		}

		public void UpdateZombieNewspaper()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_NEWSPAPER_MADDENING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_NEWSPAPER_MAD;
					if (this.mBoard.CountZombiesOnScreen() <= 10 && this.mHasHead)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_NEWSPAPER_RARRGH);
					}
					this.StartWalkAnim(20);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_MADHEAD);
				}
			}
		}

		public void LandFlyer(uint theDamageFlags)
		{
			if (!TodCommon.TestBit(theDamageFlags, 4) && this.mZombiePhase == ZombiePhase.PHASE_BALLOON_FLYING)
			{
				this.mApp.PlaySample(Resources.SOUND_BALLOON_POP);
				this.mZombiePhase = ZombiePhase.PHASE_BALLOON_POPPING;
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_pop, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
			}
			if (this.mBoard.mPlantRow[this.mRow] == PlantRowType.PLANTROW_POOL)
			{
				this.DieWithLoot();
				return;
			}
			this.mZombieHeight = ZombieHeight.HEIGHT_FALLING;
		}

		public void UpdateZombieDigger()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING)
			{
				if (this.mPosX < 90f)
				{
					this.mZombiePhase = ZombiePhase.PHASE_DIGGER_RISING;
					this.mPhaseCounter = 130;
					this.mAltitude = -120f;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_drill, ReanimLoopType.REANIM_LOOP, 0, 20f);
					this.mApp.PlayFoley(FoleyType.FOLEY_DIRT_RISE);
					this.mApp.PlayFoley(FoleyType.FOLEY_WAKEUP);
					GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_DIGGER_TUNNEL, null);
					this.StopZombieSound();
					this.mApp.AddTodParticle(this.mPosX + 60f, this.mPosY + 118f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_DIGGER_RISE);
					Reanimation reanimation = this.mApp.AddReanimation(this.mPosX + 13f, this.mPosY + 97f, this.mRenderOrder + 1, ReanimationType.REANIM_DIGGER_DIRT);
					reanimation.mAnimRate = 24f;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISING)
			{
				if (this.mPhaseCounter > 40)
				{
					this.mAltitude = (float)TodCommon.TodAnimateCurve(130, 40, this.mPhaseCounter, -120, 20, TodCurves.CURVE_EASE_OUT);
				}
				else
				{
					this.mAltitude = (float)TodCommon.TodAnimateCurve(30, 0, this.mPhaseCounter, 20, 0, TodCurves.CURVE_EASE_IN);
				}
				if (this.mPhaseCounter >= 30 && this.mPhaseCounter < 33)
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_landing, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 12f);
				}
				if (this.mPhaseCounter >= 0 && this.mPhaseCounter < 3)
				{
					this.mZombiePhase = ZombiePhase.PHASE_DIGGER_STUNNED;
					this.mAltitude = 0f;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_dizzy, ReanimLoopType.REANIM_LOOP, 10, 12f);
				}
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_LAWN_MOWER, this.mRow, 1);
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING_PAUSE_WITHOUT_AXE)
			{
				if (this.mPhaseCounter >= 150 && this.mPhaseCounter < 153)
				{
					this.AddAttachedReanim(23, 93, ReanimationType.REANIM_ZOMBIE_SURPRISE);
				}
				if (this.mPhaseCounter >= 0 && this.mPhaseCounter < 3)
				{
					this.mZombiePhase = ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE;
					this.mPhaseCounter = 130;
					this.mAltitude = -120f;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_landing, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 0f);
					this.mApp.PlayFoley(FoleyType.FOLEY_DIRT_RISE);
					this.mApp.AddTodParticle(this.mPosX + 60f, this.mPosY + 118f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_DIGGER_RISE);
					Reanimation reanimation2 = this.mApp.AddReanimation(this.mPosX + 13f, this.mPosY + 97f, this.mRenderOrder + 1, ReanimationType.REANIM_DIGGER_DIRT);
					reanimation2.mAnimRate = 24f;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE)
			{
				if (this.mPhaseCounter > 40)
				{
					this.mAltitude = (float)TodCommon.TodAnimateCurve(130, 40, this.mPhaseCounter, -120, 20, TodCurves.CURVE_EASE_OUT);
				}
				else
				{
					this.mAltitude = (float)TodCommon.TodAnimateCurve(30, 0, this.mPhaseCounter, 20, 0, TodCurves.CURVE_EASE_IN);
				}
				if (this.mPhaseCounter >= 30 && this.mPhaseCounter < 33)
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_landing, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
				}
				if (this.mPhaseCounter >= 0 && this.mPhaseCounter < 3)
				{
					this.mZombiePhase = ZombiePhase.PHASE_DIGGER_WALKING_WITHOUT_AXE;
					this.mAltitude = 0f;
					this.StartWalkAnim(20);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_STUNNED)
			{
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation3.mLoopCount > 1)
				{
					this.mZombiePhase = ZombiePhase.PHASE_DIGGER_WALKING;
					this.StartWalkAnim(20);
				}
			}
			this.mUsesClipping = (this.mAltitude < 0f);
		}

		public bool IsWalkingBackwards()
		{
			if (this.mMindControlled)
			{
				return true;
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIQUARIUM)
			{
				float num = this.mVelZ;
				if (num < 1.5707964f || num > 4.712389f)
				{
					return true;
				}
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				return this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_STUNNED || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_WALKING || ((this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_MOWERED) && this.mHasObject);
			}
			return this.mZombieType == ZombieType.ZOMBIE_YETI && !this.mHasObject;
		}

		public TodParticleSystem AddAttachedParticle(int thePosX, int thePosY, ParticleEffect theEffect)
		{
			if (this.mDead)
			{
				return null;
			}
			if (!this.doParticle)
			{
				return null;
			}
			if (GlobalMembersAttachment.IsFullOfAttachments(ref this.mAttachmentID))
			{
				return null;
			}
			TodParticleSystem todParticleSystem = this.mApp.AddTodParticle((float)(this.mX + thePosX), (float)(this.mY + thePosY), 0, theEffect);
			if (todParticleSystem != null && !todParticleSystem.mDead)
			{
				GlobalMembersAttachment.AttachParticle(ref this.mAttachmentID, todParticleSystem, (float)thePosX, (float)thePosY);
			}
			return todParticleSystem;
		}

		public void PogoBreak(uint theDamageFlags)
		{
			if (!this.mHasObject)
			{
				return;
			}
			if (!TodCommon.TestBit(theDamageFlags, 4))
			{
				ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
				this.GetDrawPos(ref zombieDrawPosition);
				int aRenderOrder = this.mRenderOrder + 1;
				float theX = 0f;
				float num = 0f;
				this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_pogo_stick, ref theX, ref num);
				TodParticleSystem aParticle = this.mApp.AddTodParticle(theX, num + 30f, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_POGO);
				this.OverrideParticleScale(aParticle);
			}
			Debug.ASSERT(this.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_DYING && this.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_BURNED && !this.mDead);
			this.mZombieHeight = ZombieHeight.HEIGHT_FALLING;
			this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
			this.StartWalkAnim(0);
			this.mZombieRect = new TRect(36, 17, 42, 115);
			this.mZombieAttackRect = new TRect(20, 17, 50, 115);
			this.mShieldHealth = 0;
			this.mShieldType = ShieldType.SHIELDTYPE_NONE;
			this.mHasObject = false;
		}

		public void UpdateZombieFalling()
		{
			this.mAltitude -= 3f;
			if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT)
			{
				this.mAltitude -= 3f;
			}
			int num = 0;
			if (this.IsOnHighGround())
			{
				num = Constants.HIGH_GROUND_HEIGHT;
			}
			if (this.mAltitude <= (float)num)
			{
				this.mAltitude = (float)num;
				this.mZombieHeight = ZombieHeight.HEIGHT_ZOMBIE_NORMAL;
			}
		}

		public void UpdateZombieDancer()
		{
			if (this.mIsEating)
			{
				return;
			}
			if (this.mSummonCounter > 0)
			{
				this.mSummonCounter--;
				if (this.mSummonCounter <= 0)
				{
					int dancerFrame = this.GetDancerFrame();
					if (dancerFrame == 12 && this.mHasHead && this.mPosX < (float)Constants.Zombie_Dancer_Dance_Limit_X)
					{
						this.mZombiePhase = ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS_WITH_LIGHT;
						this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_point, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
					}
					else
					{
						this.mSummonCounter = 1;
					}
				}
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_DANCING_IN)
			{
				if (!this.mHasHead)
				{
					return;
				}
				if (this.mPhaseCounter <= 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_point, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
					this.PickRandomSpeed();
				}
				return;
			}
			else
			{
				if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS || this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS_WITH_LIGHT)
				{
					Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
					if (reanimation.mLoopCount > 0)
					{
						if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS && this.mBoard.CountZombiesOnScreen() <= 15)
						{
							this.mApp.PlayFoley(FoleyType.FOLEY_DANCER);
						}
						this.SummonBackupDancers();
						this.mZombiePhase = ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS_HOLD;
						this.mPhaseCounter = 200;
					}
					return;
				}
				if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS_HOLD)
				{
					if (this.mPhaseCounter > 0)
					{
						return;
					}
					this.mZombiePhase = ZombiePhase.PHASE_DANCER_DANCING_LEFT;
					this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, 20, 0f);
				}
				ZombiePhase dancerPhase = this.GetDancerPhase();
				if (dancerPhase != this.mZombiePhase)
				{
					if (dancerPhase == ZombiePhase.PHASE_DANCER_DANCING_LEFT)
					{
						this.mZombiePhase = dancerPhase;
						this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, 10, 0f);
					}
					else if (dancerPhase == ZombiePhase.PHASE_DANCER_WALK_TO_RAISE)
					{
						this.mZombiePhase = dancerPhase;
						this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.REANIM_LOOP, 10, 18f);
						Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
						reanimation2.mAnimTime = 0.6f;
					}
					else if (dancerPhase == ZombiePhase.PHASE_DANCER_RAISE_LEFT_1 || dancerPhase == ZombiePhase.PHASE_DANCER_RAISE_RIGHT_1 || dancerPhase == ZombiePhase.PHASE_DANCER_RAISE_LEFT_2 || dancerPhase == ZombiePhase.PHASE_DANCER_RAISE_RIGHT_2)
					{
						this.mZombiePhase = dancerPhase;
						this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.REANIM_LOOP, 10, 18f);
					}
				}
				if (this.mHasHead && this.mSummonCounter == 0 && this.NeedsMoreBackupDancers())
				{
					this.mSummonCounter = 100;
				}
				return;
			}
		}

		public Zombie SummonBackupDancer(int theRow, int thePosX)
		{
			if (!this.mBoard.RowCanHaveZombieType(theRow, ZombieType.ZOMBIE_BACKUP_DANCER))
			{
				return null;
			}
			Zombie zombie = this.mBoard.AddZombie(ZombieType.ZOMBIE_BACKUP_DANCER, this.mFromWave);
			if (zombie == null)
			{
				return null;
			}
			zombie.mPosX = (float)thePosX;
			zombie.mPosY = this.GetPosYBasedOnRow(theRow);
			zombie.SetRow(theRow);
			zombie.mX = (int)zombie.mPosX;
			zombie.mY = (int)zombie.mPosY;
			zombie.mZombiePhase = ZombiePhase.PHASE_DANCER_RISING;
			zombie.mPhaseCounter = 150;
			zombie.mAltitude = (float)Constants.ZOMBIE_BACKUP_DANCER_RISE_HEIGHT;
			zombie.mUsesClipping = true;
			zombie.mRelatedZombieID = this.mBoard.ZombieGetID(this);
			zombie.SetAnimRate(0f);
			zombie.mMindControlled = this.mMindControlled;
			int num = (int)zombie.mPosX + 60;
			int num2 = (int)zombie.mPosY + 110;
			if (zombie.IsOnHighGround())
			{
				num2 -= Constants.HIGH_GROUND_HEIGHT;
			}
			int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, theRow, 0);
			this.mApp.AddTodParticle((float)num, (float)num2, aRenderOrder, ParticleEffect.PARTICLE_DANCER_RISE);
			this.mApp.PlayFoley(FoleyType.FOLEY_GRAVESTONE_RUMBLE);
			return this.mBoard.ZombieGetID(zombie);
		}

		public void SummonBackupDancers()
		{
			if (!this.mHasHead)
			{
				return;
			}
			for (int i = 0; i < GameConstants.NUM_BACKUP_DANCERS; i++)
			{
				if (this.mBoard.ZombieTryToGet(this.mFollowerZombieID[i]) == null)
				{
					int theRow = 0;
					int thePosX = 0;
					switch (i)
					{
					case 0:
						theRow = this.mRow - 1;
						thePosX = (int)this.mPosX;
						break;
					case 1:
						theRow = this.mRow + 1;
						thePosX = (int)this.mPosX;
						break;
					case 2:
						if (this.mPosX < 130f)
						{
							goto IL_D6;
						}
						theRow = this.mRow;
						thePosX = (int)this.mPosX - 100;
						break;
					case 3:
						theRow = this.mRow;
						thePosX = (int)this.mPosX + 100;
						break;
					default:
						Debug.ASSERT(false);
						break;
					}
					this.mFollowerZombieID[i] = this.SummonBackupDancer(theRow, thePosX);
					if (this.mFollowerZombieID[i] != null)
					{
						this.mFollowerZombieID[i].mLeaderZombie = this;
					}
					this.mSummonedDancers = true;
				}
				IL_D6:;
			}
		}

		public int GetDancerFrame()
		{
			if (this.mFromWave == GameConstants.ZOMBIE_WAVE_UI)
			{
				return 0;
			}
			if (this.IsImmobilizied())
			{
				return 0;
			}
			int num = 20;
			int num2 = 23;
			if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_DANCING_IN)
			{
				num2 = 11;
				num = 10;
			}
			return this.mApp.mAppCounter * 3 % (num * num2) / num;
		}

		public void BungeeStealTarget()
		{
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_grab, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
			Plant topPlantAt = this.mBoard.GetTopPlantAt(this.mTargetCol, this.mRow, PlantPriority.TOPPLANT_BUNGEE_ORDER);
			if (topPlantAt != null && !topPlantAt.NotOnGround() && topPlantAt.mSeedType != SeedType.SEED_COBCANNON)
			{
				Debug.ASSERT(topPlantAt.mSeedType != SeedType.SEED_GRAVEBUSTER);
				this.mTargetPlantID = topPlantAt;
				topPlantAt.mOnBungeeState = PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE;
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, this.mRow, 0);
			}
		}

		public void UpdateYuckyFace()
		{
			this.mYuckyFaceCounter++;
			if (this.mYuckyFaceCounter > GameConstants.YUCKI_SHORT_PAUSE_TIME && this.mYuckyFaceCounter < GameConstants.YUCKI_HOLD_TIME && !this.HasYuckyFaceImage())
			{
				this.StopEating();
				this.mYuckyFaceCounter = GameConstants.YUCKI_HOLD_TIME;
				if (this.mBoard.CountZombiesOnScreen() <= 5 && this.mHasHead)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_YUCK);
				}
				else if (this.mBoard.CountZombiesOnScreen() <= 10 && this.mHasHead && RandomNumbers.NextNumber(2) == 0)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_YUCK);
				}
			}
			if (this.mYuckyFaceCounter > GameConstants.YUCKI_WALK_TIME)
			{
				this.ShowYuckyFace(false);
				this.mYuckyFace = false;
				this.mYuckyFaceCounter = 0;
				if (this.mYuckySwitchRowsLate)
				{
					this.mYuckySwitchRowsLate = false;
					this.SetRow(this.mYuckyToRow);
				}
				return;
			}
			if (this.mYuckyFaceCounter == GameConstants.YUCKI_PAUSE_TIME)
			{
				this.StopEating();
				this.ShowYuckyFace(true);
				if (this.mBoard.CountZombiesOnScreen() <= 5 && this.mHasHead)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_YUCK);
				}
				else if (this.mBoard.CountZombiesOnScreen() <= 10 && this.mHasHead && RandomNumbers.NextNumber(2) == 0)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_YUCK);
				}
			}
			if (this.mYuckyFaceCounter == GameConstants.YUCKI_HOLD_TIME)
			{
				this.StartWalkAnim(20);
				bool flag = true;
				bool flag2 = true;
				bool flag3 = this.mBoard.mPlantRow[this.mRow] == PlantRowType.PLANTROW_POOL;
				if (!this.mBoard.RowCanHaveZombies(this.mRow - 1))
				{
					flag = false;
				}
				else if (this.mBoard.mPlantRow[this.mRow - 1] == PlantRowType.PLANTROW_POOL && !flag3)
				{
					flag = false;
				}
				else if (this.mBoard.mPlantRow[this.mRow - 1] != PlantRowType.PLANTROW_POOL && flag3)
				{
					flag = false;
				}
				if (!this.mBoard.RowCanHaveZombies(this.mRow + 1))
				{
					flag2 = false;
				}
				else if (this.mBoard.mPlantRow[this.mRow + 1] == PlantRowType.PLANTROW_POOL && !flag3)
				{
					flag2 = false;
				}
				else if (this.mBoard.mPlantRow[this.mRow + 1] != PlantRowType.PLANTROW_POOL && flag3)
				{
					flag2 = false;
				}
				if (flag && !flag2)
				{
					this.mBoard.ZombieSwitchRow(this, this.mRow - 1);
					this.SetRow(this.mRow - 1);
					return;
				}
				if (!flag && flag2)
				{
					this.mBoard.ZombieSwitchRow(this, this.mRow + 1);
					this.mYuckyToRow = this.mRow + 1;
					this.mYuckySwitchRowsLate = true;
					this.mRow = this.mYuckyToRow;
					return;
				}
				if (flag && flag2)
				{
					if (RandomNumbers.NextNumber(2) == 0)
					{
						this.mBoard.ZombieSwitchRow(this, this.mRow + 1);
						this.mYuckyToRow = this.mRow + 1;
						this.mYuckySwitchRowsLate = true;
						this.mRow = this.mYuckyToRow;
						return;
					}
					this.mBoard.ZombieSwitchRow(this, this.mRow - 1);
					this.SetRow(this.mRow - 1);
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
			if (this.mInPool)
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				return;
			}
			float num = 46f;
			float num2 = 92f + theDrawPos.mBodyY;
			float num3 = 1f;
			if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				num -= 10f;
				num2 += 20f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				num -= 20f;
				num2 -= 7f;
				num3 = 1.6f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				num -= 45f;
				num2 -= 23f;
				num3 = 1.2f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				num -= 27f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				num += 32f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				num -= 9f;
				num2 += 27f;
			}
			if (theFront)
			{
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_ICETRAP, num * Constants.S, num2 * Constants.S, num3, num3);
				return;
			}
			TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_ICETRAP2, num * Constants.S, num2 * Constants.S, num3, num3);
		}

		public bool HitIceTrap()
		{
			bool flag = false;
			if (this.mChilledCounter > 0 || this.mIceTrapCounter != 0)
			{
				flag = true;
			}
			this.ApplyChill(true);
			if (!this.CanBeFrozen())
			{
				return false;
			}
			if (this.mInPool)
			{
				this.mIceTrapCounter = 300;
			}
			else if (flag)
			{
				this.mIceTrapCounter = TodCommon.RandRangeInt(300, 400);
			}
			else
			{
				this.mIceTrapCounter = TodCommon.RandRangeInt(400, 600);
			}
			this.StopZombieSound();
			if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				this.BalloonPropellerHatSpin(false);
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_SPIT)
			{
				this.mBoard.RemoveParticleByType(ParticleEffect.PARTICLE_ZOMBIE_BOSS_FIREBALL);
			}
			this.TakeDamage(20, 1U);
			this.UpdateAnimSpeed();
			return true;
		}

		public int GetHelmDamageIndex()
		{
			if (this.mHelmHealth < this.mHelmMaxHealth / 3)
			{
				return 2;
			}
			if (this.mHelmHealth < this.mHelmMaxHealth * 2 / 3)
			{
				return 1;
			}
			return 0;
		}

		public int GetShieldDamageIndex()
		{
			if (this.mShieldHealth < this.mShieldMaxHealth / 3)
			{
				return 2;
			}
			if (this.mShieldHealth < this.mShieldMaxHealth * 2 / 3)
			{
				return 1;
			}
			return 0;
		}

		public void DrawReanim(Graphics g, ref ZombieDrawPosition theDrawPos, int theBaseRenderGroup)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			float mImageOffsetX = theDrawPos.mImageOffsetX;
			float num = theDrawPos.mImageOffsetY + theDrawPos.mBodyY;
			ZombieType zombieType = this.mZombieType;
			if (zombieType <= ZombieType.ZOMBIE_SNORKEL)
			{
				switch (zombieType)
				{
				case ZombieType.ZOMBIE_NORMAL:
					num -= (float)((int)((float)Constants.Zombie_ClipOffset_Normal * this.mScaleZombie));
					goto IL_1CC;
				case ZombieType.ZOMBIE_FLAG:
					num -= (float)((int)((float)Constants.Zombie_ClipOffset_Pail * this.mScaleZombie));
					goto IL_1CC;
				case ZombieType.ZOMBIE_TRAFFIC_CONE:
				case ZombieType.ZOMBIE_PAIL:
					num -= (float)((int)((float)Constants.Zombie_ClipOffset_Pail * this.mScaleZombie));
					goto IL_1CC;
				case ZombieType.ZOMBIE_POLEVAULTER:
					break;
				default:
					if (zombieType == ZombieType.ZOMBIE_SNORKEL)
					{
						if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING)
						{
							if (this.mScaleZombie == 1f)
							{
								num -= (float)((int)((float)Constants.Zombie_ClipOffset_Snorkel_Dying * this.mScaleZombie));
							}
							else
							{
								num -= (float)Constants.Zombie_ClipOffset_Snorkel_Dying_Small;
								this.mUsesClipping = true;
							}
						}
						else if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL && this.mScaleZombie != 1f)
						{
							num -= (float)Constants.Zombie_ClipOffset_Snorkel_intoPool_Small;
						}
						else
						{
							num -= (float)((int)((float)Constants.Zombie_ClipOffset_Snorkel * this.mScaleZombie));
						}
						if (this.draggedByTangleKelp)
						{
							num -= (float)((int)((float)Constants.Zombie_ClipOffset_Snorkel_Grabbed * this.mScaleZombie));
							this.mUsesClipping = true;
							goto IL_1CC;
						}
						goto IL_1CC;
					}
					break;
				}
			}
			else
			{
				if (zombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER)
				{
					num -= (float)((int)((float)Constants.Zombie_ClipOffset_Dolphin_Into_Pool * this.mScaleZombie));
					goto IL_1CC;
				}
				if (zombieType == ZombieType.ZOMBIE_DIGGER)
				{
					num -= (float)((int)((float)Constants.Zombie_ClipOffset_Digger * this.mScaleZombie));
					goto IL_1CC;
				}
				switch (zombieType)
				{
				case ZombieType.ZOMBIE_PEA_HEAD:
				case ZombieType.ZOMBIE_WALLNUT_HEAD:
				case ZombieType.ZOMBIE_JALAPENO_HEAD:
				case ZombieType.ZOMBIE_GATLING_HEAD:
				case ZombieType.ZOMBIE_TALLNUT_HEAD:
					num -= (float)((int)((float)Constants.Zombie_ClipOffset_PeaHead_InPool * this.mScaleZombie));
					goto IL_1CC;
				}
			}
			num -= (float)((int)((float)Constants.Zombie_ClipOffset_Default * this.mScaleZombie));
			IL_1CC:
			ZombiePhase zombiePhase = this.mZombiePhase;
			if (zombiePhase != ZombiePhase.PHASE_RISING_FROM_GRAVE)
			{
				if (zombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL)
				{
					num -= (float)((int)((float)Constants.Zombie_ClipOffset_Snorkel_Into_Pool * this.mScaleZombie));
				}
			}
			else if (this.mScaleZombie == 1f)
			{
				num += (float)((int)((float)Constants.Zombie_ClipOffset_RisingFromGrave * this.mScaleZombie));
			}
			else
			{
				num += (float)((int)((float)Constants.Zombie_ClipOffset_RisingFromGrave_Small * this.mScaleZombie));
			}
			if (this.mZombieType == ZombieType.ZOMBIE_NORMAL && this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_NORMAL && this.mInPool)
			{
				if (this.mScaleZombie == 1f)
				{
					num += (float)Constants.Zombie_ClipOffset_Normal_In_Pool;
				}
				else
				{
					num += (float)Constants.Zombie_ClipOffset_Normal_In_Pool_SMALL;
				}
			}
			if (this.mZombieType == ZombieType.ZOMBIE_TRAFFIC_CONE && this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_NORMAL && this.mInPool && this.mScaleZombie != 1f)
			{
				num += (float)Constants.Zombie_ClipOffset_TrafficCone_In_Pool_SMALL;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_NORMAL && this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING && this.mInPool)
			{
				if (this.mScaleZombie == 1f)
				{
					num += (float)Constants.Zombie_ClipOffset_Normal_In_Pool;
				}
				else
				{
					num += (float)Constants.Zombie_ClipOffset_Normal_In_Pool_SMALL;
				}
			}
			if (this.mZombieType == ZombieType.ZOMBIE_FLAG && this.mInPool)
			{
				num += (float)Constants.Zombie_ClipOffset_Flag_In_Pool;
			}
			if (this.mScaleZombie != 1f && this.mAltitude != 0f && this.mZombiePhase != ZombiePhase.PHASE_RISING_FROM_GRAVE && this.mInPool)
			{
				num -= this.mAltitude;
			}
			if (theDrawPos.mClipHeight > GameConstants.CLIP_HEIGHT_LIMIT)
			{
				float num2 = 120f - theDrawPos.mClipHeight + 71f;
				g.SetClipRect((int)((mImageOffsetX - 200f) * Constants.S), (int)(num * Constants.S), (int)(520f * Constants.S), (int)(num2 * Constants.S));
			}
			if (this.mUsesClipping)
			{
				g.mClipRect.mX = g.mClipRect.mX + 1;
				g.HardwareClip();
			}
			int num3 = 255;
			if (this.mZombieFade >= 0)
			{
				num3 = TodCommon.ClampInt((int)((float)(255 * this.mZombieFade) / 30f), 0, 255);
			}
			SexyColor zombie_MINDCONTROLLED_COLOR = new SexyColor(255, 255, 255, num3);
			SexyColor sexyColor = SexyColor.Black;
			bool mEnableExtraAdditiveDraw = false;
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED)
			{
				zombie_MINDCONTROLLED_COLOR = new SexyColor(0, 0, 0, num3);
				sexyColor = SexyColor.Black;
				mEnableExtraAdditiveDraw = false;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BOSS && this.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_DYING && this.mBodyHealth < this.mBodyMaxHealth / GameConstants.BOSS_FLASH_HEALTH_FRACTION)
			{
				int num4 = TodCommon.TodAnimateCurve(0, 39, this.mBoard.mMainCounter % 40, (int)Constants.InvertAndScale(155f), (int)Constants.InvertAndScale(255f), TodCurves.CURVE_BOUNCE);
				if (this.mChilledCounter > 0 || this.mIceTrapCounter > 0)
				{
					int num5 = TodCommon.TodAnimateCurve(0, 39, this.mBoard.mMainCounter % 40, 65, 75, TodCurves.CURVE_BOUNCE);
					zombie_MINDCONTROLLED_COLOR = new SexyColor(num5, num5, num4, num3);
				}
				else
				{
					zombie_MINDCONTROLLED_COLOR = new SexyColor(num4, num4, num4, num3);
				}
				sexyColor = SexyColor.Black;
				mEnableExtraAdditiveDraw = false;
			}
			else if (this.mMindControlled)
			{
				zombie_MINDCONTROLLED_COLOR = GameConstants.ZOMBIE_MINDCONTROLLED_COLOR;
				zombie_MINDCONTROLLED_COLOR.mAlpha = num3;
				sexyColor = zombie_MINDCONTROLLED_COLOR;
				mEnableExtraAdditiveDraw = true;
			}
			else if (this.mChilledCounter > 0 || this.mIceTrapCounter > 0)
			{
				zombie_MINDCONTROLLED_COLOR = new SexyColor(75, 75, 255, num3);
				sexyColor = zombie_MINDCONTROLLED_COLOR;
				mEnableExtraAdditiveDraw = true;
			}
			else if (this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIQUARIUM && this.mBodyHealth < 100)
			{
				zombie_MINDCONTROLLED_COLOR = new SexyColor(100, 150, 25, num3);
				sexyColor = zombie_MINDCONTROLLED_COLOR;
				mEnableExtraAdditiveDraw = true;
			}
			if (this.mJustGotShotCounter > 0 && !this.IsBobsledTeamWithSled() && !GlobalStaticVars.gLowFramerate)
			{
				int num6 = this.mJustGotShotCounter * 10;
				SexyColor theColor = new SexyColor(num6, num6, num6, 255);
				sexyColor = TodCommon.ColorAdd(theColor, sexyColor);
				mEnableExtraAdditiveDraw = true;
			}
			reanimation.mColorOverride = zombie_MINDCONTROLLED_COLOR;
			reanimation.mExtraAdditiveColor = sexyColor;
			reanimation.mEnableExtraAdditiveDraw = mEnableExtraAdditiveDraw;
			if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				this.DrawBobsledReanim(g, ref theDrawPos, true);
				reanimation.DrawRenderGroup(g, theBaseRenderGroup);
				this.DrawBobsledReanim(g, ref theDrawPos, false);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				this.DrawBungeeReanim(g, ref theDrawPos);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
			{
				this.DrawDancerReanim(g, ref theDrawPos);
			}
			else
			{
				reanimation.DrawRenderGroup(g, theBaseRenderGroup);
			}
			if (this.mShieldType != ShieldType.SHIELDTYPE_NONE)
			{
				if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED)
				{
					reanimation.mColorOverride = new SexyColor(0, 0, 0, num3);
					reanimation.mExtraAdditiveColor = SexyColor.Black;
					reanimation.mEnableExtraAdditiveDraw = false;
				}
				else if (this.mShieldJustGotShotCounter > 0)
				{
					reanimation.mColorOverride = new SexyColor(255, 255, 255, num3);
					reanimation.mExtraAdditiveColor = SexyColor.White;
					reanimation.mEnableExtraAdditiveDraw = true;
				}
				else
				{
					reanimation.mColorOverride = new SexyColor(255, 255, 255, num3);
					reanimation.mExtraAdditiveColor = SexyColor.Black;
					reanimation.mEnableExtraAdditiveDraw = false;
				}
				float num7 = 0f;
				if (this.mShieldRecoilCounter > 0)
				{
					num7 = TodCommon.TodAnimateCurveFloat(12, 0, this.mShieldRecoilCounter, 3f, 0f, TodCurves.CURVE_LINEAR);
				}
				g.mTransX += (int)num7;
				reanimation.DrawRenderGroup(g, GameConstants.RENDER_GROUP_SHIELD);
				g.mTransX -= (int)num7;
			}
			if (this.mShieldType == ShieldType.SHIELDTYPE_NEWSPAPER || this.mShieldType == ShieldType.SHIELDTYPE_DOOR || this.mShieldType == ShieldType.SHIELDTYPE_LADDER)
			{
				reanimation.mColorOverride = zombie_MINDCONTROLLED_COLOR;
				reanimation.mExtraAdditiveColor = sexyColor;
				reanimation.mEnableExtraAdditiveDraw = mEnableExtraAdditiveDraw;
				reanimation.DrawRenderGroup(g, GameConstants.RENDER_GROUP_OVER_SHIELD);
			}
			g.ClearClipRect();
			if (this.mUsesClipping)
			{
				g.EndHardwareClip();
			}
		}

		public void UpdatePlaying()
		{
			Debug.ASSERT(this.mBodyHealth > 0 || this.mZombiePhase == ZombiePhase.PHASE_BOBSLED_CRASHING);
			this.mGroanCounter -= 3;
			int count = this.mBoard.mZombies.Count;
			if (this.mGroanCounter <= 0 && RandomNumbers.NextNumber(count) == 0 && this.mHasHead && this.mZombieType != ZombieType.ZOMBIE_BOSS && !this.mBoard.HasLevelAwardDropped())
			{
				float aPitch = 0f;
				if (this.mApp.IsLittleTroubleLevel())
				{
					aPitch = TodCommon.RandRangeFloat(40f, 50f);
				}
				if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_LOWGROAN);
				}
				else if (this.mVariant)
				{
					this.mApp.PlayFoleyPitch(FoleyType.FOLEY_BRAINS, aPitch);
				}
				else
				{
					this.mApp.PlayFoleyPitch(FoleyType.FOLEY_GROAN, aPitch);
				}
				this.mGroanCounter = RandomNumbers.NextNumber(1000) + 500;
			}
			if (this.mIceTrapCounter > 0)
			{
				this.mIceTrapCounter -= 3;
				if (this.mIceTrapCounter <= 0)
				{
					this.RemoveIceTrap();
					this.AddAttachedParticle(75, 106, ParticleEffect.PARTICLE_ICE_TRAP_RELEASE);
				}
			}
			if (this.mChilledCounter > 0)
			{
				this.mChilledCounter -= 3;
				if (this.mChilledCounter <= 0)
				{
					this.UpdateAnimSpeed();
				}
			}
			if (this.mButteredCounter > 0)
			{
				this.mButteredCounter -= 3;
				if (this.mButteredCounter <= 0)
				{
					this.RemoveButter();
				}
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_RISING_FROM_GRAVE)
			{
				this.UpdateZombieRiseFromGrave();
				return;
			}
			this.mBodyReanimID.mClip = false;
			if (!this.IsImmobilizied())
			{
				this.UpdateActions();
				this.UpdateZombiePosition();
				this.CheckIfPreyCaught();
				this.CheckForPool();
				this.CheckForHighGround();
				this.CheckForBoardEdge();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				this.UpdateBoss();
			}
			if (!this.IsDeadOrDying() && this.mFromWave != GameConstants.ZOMBIE_WAVE_WINNER)
			{
				bool flag = !this.mHasHead;
				if ((this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombieType == ZombieType.ZOMBIE_CATAPULT) && this.mBodyHealth < 200)
				{
					flag = true;
				}
				if (flag)
				{
					int theDamage = 1;
					if (this.mZombieType == ZombieType.ZOMBIE_YETI)
					{
						theDamage = 10;
					}
					if (this.mBodyMaxHealth >= 500)
					{
						theDamage = 3;
					}
					if (RandomNumbers.NextNumber(5) == 0)
					{
						this.TakeDamage(theDamage, 9U);
					}
				}
			}
		}

		public bool NeedsMoreBackupDancers()
		{
			for (int i = 0; i < GameConstants.NUM_BACKUP_DANCERS; i++)
			{
				if (this.mBoard.ZombieTryToGet(this.mFollowerZombieID[i]) == null && (i != 0 || this.mBoard.RowCanHaveZombieType(this.mRow - 1, ZombieType.ZOMBIE_BACKUP_DANCER)) && (i != 1 || this.mBoard.RowCanHaveZombieType(this.mRow + 1, ZombieType.ZOMBIE_BACKUP_DANCER)))
				{
					return true;
				}
			}
			return false;
		}

		public void ConvertToNormalZombie()
		{
			this.StopZombieSound();
			this.mPosY = this.GetPosYBasedOnRow(this.mRow);
			this.mX = (int)this.mPosX;
			this.mY = (int)this.mPosY;
			this.mZombieType = ZombieType.ZOMBIE_NORMAL;
			this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
			this.mZombieAttackRect = new TRect(50, 0, 20, 115);
			this.mAnimFrames = 12;
			this.mAnimTicksPerFrame = 12;
			this.mPhaseCounter = 0;
			this.PickRandomSpeed();
		}

		public void StartEating()
		{
			if (this.mIsEating)
			{
				return;
			}
			this.mIsEating = true;
			if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING)
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_LADDER_CARRYING)
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_laddereat, ReanimLoopType.REANIM_LOOP, 20, 0f);
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_NEWSPAPER_MAD)
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_eat_nopaper, ReanimLoopType.REANIM_LOOP, 20, 0f);
				return;
			}
			if (this.mZombieType != ZombieType.ZOMBIE_SNORKEL)
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_eat, ReanimLoopType.REANIM_LOOP, 20, 0f);
			}
			if (this.mShieldType == ShieldType.SHIELDTYPE_DOOR)
			{
				this.ShowDoorArms(false);
			}
		}

		public void StopEating()
		{
			if (!this.mIsEating)
			{
				return;
			}
			this.mIsEating = false;
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING)
			{
				return;
			}
			if (reanimation != null && this.mZombieType != ZombieType.ZOMBIE_SNORKEL)
			{
				this.StartWalkAnim(20);
			}
			if (this.mShieldType == ShieldType.SHIELDTYPE_DOOR)
			{
				this.ShowDoorArms(true);
			}
			this.UpdateAnimSpeed();
		}

		public void UpdateAnimSpeed()
		{
			if (!this.IsOnBoard())
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			if (this.IsImmobilizied())
			{
				this.ApplyAnimRate(0f);
				return;
			}
			if (this.mYuckyFace && this.mYuckyFaceCounter < GameConstants.YUCKI_HOLD_TIME)
			{
				this.ApplyAnimRate(0f);
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_UP_TO_EAT || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_DOWN_FROM_EAT || this.IsDeadOrDying())
			{
				this.ApplyAnimRate(this.mOrginalAnimRate);
				return;
			}
			if (this.mIsEating)
			{
				if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER || this.mZombieType == ZombieType.ZOMBIE_BALLOON || this.mZombieType == ZombieType.ZOMBIE_IMP || this.mZombieType == ZombieType.ZOMBIE_DIGGER || this.mZombieType == ZombieType.ZOMBIE_JACK_IN_THE_BOX || this.mZombieType == ZombieType.ZOMBIE_SNORKEL || this.mZombieType == ZombieType.ZOMBIE_YETI)
				{
					this.ApplyAnimRate(20f);
					return;
				}
				this.ApplyAnimRate(36f);
				return;
			}
			else
			{
				if (this.ZombieNotWalking())
				{
					this.ApplyAnimRate(this.mOrginalAnimRate);
					return;
				}
				if (this.IsBobsledTeamWithSled() || this.mZombieType == ZombieType.ZOMBIE_CATAPULT || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_RIDING || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL)
				{
					this.ApplyAnimRate(this.mOrginalAnimRate);
					return;
				}
				if (!this.mHasGroundTrack)
				{
					return;
				}
				int num = reanimation.FindTrackIndex(Reanimation.ReanimTrackId__ground);
				ReanimatorTrack reanimatorTrack = reanimation.mDefinition.mTracks[num];
				ReanimatorTransform reanimatorTransform = reanimatorTrack.mTransforms[(int)reanimation.mFrameStart];
				ReanimatorTransform reanimatorTransform2 = reanimatorTrack.mTransforms[(int)(reanimation.mFrameStart + reanimation.mFrameCount - 1)];
				float num2 = reanimatorTransform2.mTransX - reanimatorTransform.mTransX;
				if (num2 < 1E-06f)
				{
					return;
				}
				float num3 = (float)reanimation.mFrameCount / num2;
				float theAnimRate = this.mVelX * num3 * 47f / this.mScaleZombie;
				this.ApplyAnimRate(theAnimRate);
				return;
			}
		}

		public void ReanimShowPrefix(string theTrackPrefix, int theRenderGroup)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			reanimation.AssignRenderGroupToPrefix(theTrackPrefix, theRenderGroup);
		}

		public void DetachPlantHead()
		{
			ReanimatorTrackInstance trackInstanceByName = this.mBodyReanimID.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
			GlobalMembersAttachment.AttachmentDetach(ref trackInstanceByName.mAttachmentID);
		}

		public void DetachFlag()
		{
			ReanimatorTrackInstance trackInstanceByName = this.mBodyReanimID.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand);
			GlobalMembersAttachment.AttachmentDetach(ref trackInstanceByName.mAttachmentID);
		}

		public void PlayDeathAnim(uint theDamageFlags)
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_MOWERED)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null || !reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_death))
			{
				this.DieNoLoot(true);
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER && this.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_WALKING_IN_POOL)
			{
				this.DieNoLoot(true);
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING)
			{
				this.DieNoLoot(true);
				return;
			}
			if (this.mIceTrapCounter > 0)
			{
				this.AddAttachedParticle(75, 106, ParticleEffect.PARTICLE_ICE_TRAP_RELEASE);
				this.mIceTrapCounter = 0;
			}
			if (this.mButteredCounter > 0)
			{
				this.mButteredCounter = 0;
			}
			if (this.mYuckyFace)
			{
				this.ShowYuckyFace(false);
				this.mYuckyFace = false;
				this.mYuckyFaceCounter = 0;
			}
			if (TodCommon.TestBit(theDamageFlags, 4) && this.mZombieType != ZombieType.ZOMBIE_BOSS && this.mZombieType != ZombieType.ZOMBIE_GARGANTUAR && this.mZombieType != ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				this.DieNoLoot(true);
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				this.mAltitude = 0f;
			}
			GlobalMembersAttachment.AttachmentReanimTypeDie(ref this.mAttachmentID, ReanimationType.REANIM_ZOMBIE_SURPRISE);
			this.StopEating();
			if (this.mShieldType != ShieldType.SHIELDTYPE_NONE)
			{
				this.DropShield(1U);
			}
			if (this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD && !this.mHasHead)
			{
				this.DetachPlantHead();
				this.mApp.RemoveReanimation(ref this.mSpecialHeadReanimID);
				this.mSpecialHeadReanimID = null;
			}
			this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_DYING;
			this.mVelX = 0f;
			if (this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIQUARIUM)
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_aquarium_death, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 14f);
				return;
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_UP_LADDER)
			{
				this.mZombieHeight = ZombieHeight.HEIGHT_FALLING;
			}
			float theAnimRate;
			if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
			{
				theAnimRate = 24f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				theAnimRate = 14f;
				this.mApp.PlayFoley(FoleyType.FOLEY_GARGANTUDEATH);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
			{
				theAnimRate = 14f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				theAnimRate = 18f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_YETI)
			{
				theAnimRate = 14f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				theAnimRate = 18f;
				this.BossDie();
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_death, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, theAnimRate);
			}
			else
			{
				theAnimRate = TodCommon.RandRangeFloat(24f, 30f);
			}
			string text = GlobalMembersReanimIds.ReanimTrackId_anim_death;
			int num = RandomNumbers.NextNumber(100);
			bool flag = this.mApp.HasFinishedAdventure() || this.mBoard.mLevel > 5;
			if (this.mInPool && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_waterdeath))
			{
				text = GlobalMembersReanimIds.ReanimTrackId_anim_waterdeath;
				this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_duckytube, false);
			}
			else if (num == 99 && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_superlongdeath) && flag && this.mChilledCounter == 0 && this.mBoard.CountZombiesOnScreen() <= 5)
			{
				theAnimRate = 14f;
				text = GlobalMembersReanimIds.ReanimTrackId_anim_superlongdeath;
			}
			else if (num > 50 && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_death2))
			{
				text = GlobalMembersReanimIds.ReanimTrackId_anim_death2;
			}
			this.PlayZombieReanim(ref text, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, theAnimRate);
			this.ReanimShowPrefix("anim_tongue", -1);
		}

		public void UpdateDeath()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				this.DieNoLoot(true);
				return;
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_FALLING)
			{
				this.UpdateZombieFalling();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				if (reanimation.ShouldTriggerTimedEvent(0.89f))
				{
					this.mBoard.ShakeBoard(0, 3);
				}
				else if (reanimation.ShouldTriggerTimedEvent(0.98f))
				{
					this.mBoard.ShakeBoard(0, 1);
				}
			}
			float num = -1f;
			if (this.mInPool || this.mZombieType == ZombieType.ZOMBIE_SNORKEL || this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER || this.mZombieType == ZombieType.ZOMBIE_BUNGEE || this.mZombieType == ZombieType.ZOMBIE_CATAPULT || this.mZombieType == ZombieType.ZOMBIE_IMP || this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				num = -1f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_NORMAL || this.mZombieType == ZombieType.ZOMBIE_FLAG || this.mZombieType == ZombieType.ZOMBIE_TRAFFIC_CONE || this.mZombieType == ZombieType.ZOMBIE_PAIL || this.mZombieType == ZombieType.ZOMBIE_DOOR || this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD || this.mZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD || this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD || this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD || this.mZombieType == ZombieType.ZOMBIE_DUCKY_TUBE)
			{
				if (reanimation.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_superlongdeath))
				{
					num = 0.788f;
				}
				else if (reanimation.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_death2))
				{
					num = 0.71f;
				}
				else
				{
					num = 0.77f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				num = 0.68f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
			{
				num = 0.52f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER)
			{
				num = 0.63f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				num = 0.83f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				num = 0.81f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_JACK_IN_THE_BOX)
			{
				num = 0.64f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				num = 0.68f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				num = 0.85f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				num = 0.84f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_YETI)
			{
				num = 0.68f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_LADDER)
			{
				num = 0.62f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				num = 0.86f;
			}
			if (num > 0f && reanimation.ShouldTriggerTimedEvent(num))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_ZOMBIE_FALLING);
				if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_THUMP);
					this.mApp.Vibrate();
				}
				if (this.mBoard.mDaisyMode)
				{
					this.DoDaisies();
				}
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				if (reanimation.ShouldTriggerTimedEvent(0.1f) || reanimation.ShouldTriggerTimedEvent(0.12f) || reanimation.ShouldTriggerTimedEvent(0.15f) || reanimation.ShouldTriggerTimedEvent(0.19f) || reanimation.ShouldTriggerTimedEvent(0.2f) || reanimation.ShouldTriggerTimedEvent(0.26f) || reanimation.ShouldTriggerTimedEvent(0.3f) || reanimation.ShouldTriggerTimedEvent(0.4f) || reanimation.ShouldTriggerTimedEvent(0.42f) || reanimation.ShouldTriggerTimedEvent(0.5f) || reanimation.ShouldTriggerTimedEvent(0.58f) || reanimation.ShouldTriggerTimedEvent(0.61f) || reanimation.ShouldTriggerTimedEvent(0.71f))
				{
					float theX = TodCommon.RandRangeFloat(600f, 750f);
					float theY = TodCommon.RandRangeFloat(50f, 300f);
					this.mApp.AddTodParticle(theX, theY, 400000, ParticleEffect.PARTICLE_BOSS_EXPLOSION);
					this.mApp.PlayFoley(FoleyType.FOLEY_BOSS_EXPLOSION_SMALL);
				}
				Reanimation reanimation2 = this.mApp.ReanimationTryToGet(this.mSpecialHeadReanimID);
				if (reanimation.ShouldTriggerTimedEvent(0.93f))
				{
					this.mBoard.ShakeBoard(1, 2);
					this.mApp.PlayFoley(FoleyType.FOLEY_BOSS_EXPLOSION_SMALL);
					this.mApp.PlayFoley(FoleyType.FOLEY_THUMP);
					this.mApp.Vibrate();
				}
				if (reanimation.ShouldTriggerTimedEvent(0.99f))
				{
					reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_flag, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 30f);
				}
				if (reanimation2.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_flag) && reanimation2.mLoopCount > 0)
				{
					reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_flag_loop, ReanimLoopType.REANIM_LOOP, 20, 17f);
				}
				if (reanimation.mLoopCount > 0)
				{
					this.DropLoot();
				}
			}
			if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI && this.mPhaseCounter > 0)
			{
				this.mPhaseCounter -= 3;
				if (this.mPhaseCounter <= 0)
				{
					reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
					if (reanimation.IsTrackShowing(GlobalMembersReanimIds.ReanimTrackId_anim_wheelie2))
					{
						this.mApp.AddTodParticle(this.mPosX + 80f, this.mPosY + 60f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_ZAMBONI_EXPLOSION2);
					}
					else
					{
						this.mApp.AddTodParticle(this.mPosX + 80f, this.mPosY + 60f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_ZAMBONI_EXPLOSION);
					}
					this.DieWithLoot();
					this.mApp.PlayFoley(FoleyType.FOLEY_EXPLOSION);
				}
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				this.mPhaseCounter -= 3;
				if (this.mPhaseCounter <= 0)
				{
					this.mApp.AddTodParticle(this.mPosX + 80f, this.mPosY + 60f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_CATAPULT_EXPLOSION);
					this.DieWithLoot();
					this.mApp.PlayFoley(FoleyType.FOLEY_EXPLOSION);
				}
				return;
			}
			if (this.mZombieFade == -1 && reanimation.mLoopCount > 0 && this.mZombieType != ZombieType.ZOMBIE_BOSS)
			{
				if (this.mInPool)
				{
					this.mZombieFade = 30;
					return;
				}
				this.mZombieFade = 100;
			}
		}

		public void DrawShadow(Graphics g)
		{
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON && !this.SetupDrawZombieWon(g))
			{
				return;
			}
			int num = 0;
			float num2 = zombieDrawPosition.mImageOffsetX;
			float num3 = zombieDrawPosition.mImageOffsetY + zombieDrawPosition.mBodyY;
			float num4 = this.mScaleZombie;
			num2 += this.mScaleZombie * 20f - 20f;
			if (this.IsOnBoard() && this.mBoard.StageIsNight())
			{
				num = 1;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
			{
				if (this.IsWalkingBackwards())
				{
					num2 += -11f * this.mScaleZombie;
				}
				else
				{
					num2 += 20f + 21f * this.mScaleZombie;
				}
				num3 += 16f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER)
			{
				if (this.IsWalkingBackwards())
				{
					num2 += 5f;
				}
				else
				{
					num2 += 29f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				if (this.IsWalkingBackwards())
				{
					num2 += -5f;
				}
				else
				{
					num2 += 36f;
				}
				num3 += 11f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				if (this.IsWalkingBackwards())
				{
					num2 += 13f;
				}
				else
				{
					num2 += 20f;
				}
				num3 += 13f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_IMP)
			{
				num4 *= 0.6f;
				num3 += 7f;
				if (this.IsWalkingBackwards())
				{
					num2 += 13f;
				}
				else
				{
					num2 += 25f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				num3 += 5f;
				if (this.IsWalkingBackwards())
				{
					num2 += 14f;
				}
				else
				{
					num2 += 17f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
			{
				num3 += 5f;
				if (this.IsWalkingBackwards())
				{
					num2 += -2f;
				}
				else
				{
					num2 += 35f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER)
			{
				num3 += 11f;
				if (this.IsWalkingBackwards())
				{
					num2 += 15f;
				}
				else
				{
					num2 += 19f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_YETI)
			{
				num3 += 20f;
				if (this.IsWalkingBackwards())
				{
					num2 += 20f;
				}
				else
				{
					num2 += 3f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				num4 *= 1.5f;
				num2 += 27f;
				num3 += 7f;
			}
			else if (this.mApp.ReanimationTryToGet(this.mBodyReanimID) != null)
			{
				if (this.IsWalkingBackwards())
				{
					num2 += 11f;
				}
				else
				{
					num2 += 23f;
				}
			}
			else if (this.IsWalkingBackwards())
			{
				num2 += -2f;
			}
			else
			{
				num2 += 35f;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER)
			{
				num3 += 4f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				num3 += 13f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				num2 += -12f;
				num4 = TodCommon.TodAnimateCurveFloat(GameConstants.BUNGEE_ZOMBIE_HEIGHT - 1000, 100, (int)this.mAltitude, 0.1f, 1.5f, TodCurves.CURVE_LINEAR);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				num3 -= 18f;
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_UP_LADDER || this.mZombieHeight == ZombieHeight.HEIGHT_FALLING || this.mZombiePhase == ZombiePhase.PHASE_IMP_GETTING_THROWN || this.mZombieType == ZombieType.ZOMBIE_BUNGEE || this.IsBouncingPogo() || this.IsFlying())
			{
				num3 += this.mAltitude;
				if (this.mOnHighGround)
				{
					num3 -= (float)Constants.HIGH_GROUND_HEIGHT;
				}
			}
			if (this.mUsesClipping)
			{
				g.HardwareClip();
			}
			if (this.mInPool)
			{
				num3 += 67f;
				TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_WHITEWATER_SHADOW, num2 * Constants.S, num3 * Constants.S, num4, num4);
			}
			else
			{
				num3 += 92f;
				if (num == 0)
				{
					TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW, num2 * Constants.S, num3 * Constants.S, num4, num4);
				}
				else
				{
					TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW2, num2 * Constants.S, num3 * Constants.S, num4, num4);
				}
			}
			if (this.mUsesClipping)
			{
				g.EndHardwareClip();
			}
			g.ClearClipRect();
		}

		public bool HasShadow()
		{
			return this.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_DYING && this.mZombiePhase != ZombiePhase.PHASE_DIGGER_RISING && this.mZombiePhase != ZombiePhase.PHASE_DIGGER_TUNNELING_PAUSE_WITHOUT_AXE && this.mZombiePhase != ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE && this.mZombiePhase != ZombiePhase.PHASE_DIGGER_TUNNELING && this.mZombiePhase != ZombiePhase.PHASE_RISING_FROM_GRAVE && this.mZombiePhase != ZombiePhase.PHASE_DANCER_RISING && this.mZombiePhase != ZombiePhase.PHASE_BOBSLED_BOARDING && this.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_IN_VAULT && this.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_INTO_POOL && this.mZombiePhase != ZombiePhase.PHASE_SNORKEL_INTO_POOL && this.mZombieType != ZombieType.ZOMBIE_ZAMBONI && this.mZombieType != ZombieType.ZOMBIE_CATAPULT && this.mZombieType != ZombieType.ZOMBIE_BOSS && (this.mZombieType != ZombieType.ZOMBIE_BUNGEE || (this.IsOnBoard() && !this.mHitUmbrella)) && this.mZombieHeight != ZombieHeight.HEIGHT_DRAGGED_UNDER && this.mZombieHeight != ZombieHeight.HEIGHT_IN_TO_CHIMNEY && this.mZombieHeight != ZombieHeight.HEIGHT_GETTING_BUNGEE_DROPPED && !this.mInPool && (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL || this.mFromWave == GameConstants.ZOMBIE_WAVE_UI);
		}

		public Reanimation LoadReanim(ReanimationType theReanimationType)
		{
			Reanimation reanimation = this.mApp.AddReanimation(0f, 0f, 0, theReanimationType);
			this.mBodyReanimID = this.mApp.ReanimationGetID(reanimation);
			reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
			reanimation.mIsAttachment = true;
			this.mHasGroundTrack = reanimation.TrackExists(Reanimation.ReanimTrackId__ground);
			if (!this.IsOnBoard())
			{
				int num = RandomNumbers.NextNumber(4);
				if (num > 0 && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle2))
				{
					float theAnimRate = TodCommon.RandRangeFloat(12f, 24f);
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle2, ReanimLoopType.REANIM_LOOP, 0, theAnimRate);
				}
				else if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
				{
					float theAnimRate2 = TodCommon.RandRangeFloat(12f, 18f);
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, theAnimRate2);
				}
				reanimation.mAnimTime = TodCommon.RandRangeFloat(0f, 0.99f);
			}
			else
			{
				this.StartWalkAnim(0);
			}
			return reanimation;
		}

		public int TakeFlyingDamage(int theDamage, uint theDamageFlags)
		{
			if (!TodCommon.TestBit(theDamageFlags, 3))
			{
				this.mJustGotShotCounter = 25;
			}
			int num = Math.Min(this.mFlyingHealth, theDamage);
			int result = theDamage - num;
			this.mFlyingHealth -= num;
			if (this.mFlyingHealth == 0)
			{
				this.LandFlyer(theDamageFlags);
			}
			return result;
		}

		public int TakeShieldDamage(int theDamage, uint theDamageFlags)
		{
			if (!TodCommon.TestBit(theDamageFlags, 3))
			{
				this.mShieldJustGotShotCounter = 25;
				if (this.mJustGotShotCounter < 0)
				{
					this.mJustGotShotCounter = 0;
				}
			}
			if (!TodCommon.TestBit(theDamageFlags, 3) && !TodCommon.TestBit(theDamageFlags, 1))
			{
				this.mShieldRecoilCounter = 12;
				if (this.mShieldType == ShieldType.SHIELDTYPE_DOOR || this.mShieldType == ShieldType.SHIELDTYPE_LADDER)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_SHIELD_HIT);
				}
			}
			int shieldDamageIndex = this.GetShieldDamageIndex();
			int num = Math.Min(this.mShieldHealth, theDamage);
			int result = theDamage - num;
			this.mShieldHealth -= num;
			if (this.mShieldHealth == 0)
			{
				this.DropShield(theDamageFlags);
				return result;
			}
			int shieldDamageIndex2 = this.GetShieldDamageIndex();
			if (shieldDamageIndex != shieldDamageIndex2 || this.justLoaded)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
				if (this.mShieldType == ShieldType.SHIELDTYPE_DOOR && shieldDamageIndex2 == 1)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_screendoor, AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR2);
				}
				else if (this.mShieldType == ShieldType.SHIELDTYPE_DOOR && shieldDamageIndex2 == 2)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_screendoor, AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR3);
				}
				else if (this.mShieldType == ShieldType.SHIELDTYPE_NEWSPAPER && shieldDamageIndex2 == 1)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_paper_paper, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_PAPER2);
				}
				else if (this.mShieldType == ShieldType.SHIELDTYPE_NEWSPAPER && shieldDamageIndex2 == 2)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_paper_paper, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_PAPER3);
				}
				else if (this.mShieldType == ShieldType.SHIELDTYPE_LADDER && shieldDamageIndex2 == 1)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_ladder_1, AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1_DAMAGE1);
				}
				else if (this.mShieldType == ShieldType.SHIELDTYPE_LADDER && shieldDamageIndex2 == 2)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_ladder_1, AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1_DAMAGE2);
				}
			}
			return result;
		}

		public int TakeHelmDamage(int theDamage, uint theDamageFlags)
		{
			if (!TodCommon.TestBit(theDamageFlags, 3))
			{
				this.mJustGotShotCounter = 25;
			}
			int helmDamageIndex = this.GetHelmDamageIndex();
			int num = Math.Min(this.mHelmHealth, theDamage);
			int result = theDamage - num;
			this.mHelmHealth -= num;
			if (TodCommon.TestBit(theDamageFlags, 2))
			{
				this.ApplyChill(false);
			}
			if (this.mHelmHealth == 0)
			{
				this.DropHelm(theDamageFlags);
				return result;
			}
			int helmDamageIndex2 = this.GetHelmDamageIndex();
			if ((helmDamageIndex != helmDamageIndex2 && this.mBodyReanimID.mActive) || (this.justLoaded && this.mBodyReanimID.mActive))
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
				if (this.mHelmType == HelmType.HELMTYPE_TRAFFIC_CONE && helmDamageIndex2 == 1 && reanimation != null)
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_cone, AtlasResources.IMAGE_REANIM_ZOMBIE_CONE2);
				}
				else if (this.mHelmType == HelmType.HELMTYPE_TRAFFIC_CONE && helmDamageIndex2 == 2 && reanimation != null)
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_cone, AtlasResources.IMAGE_REANIM_ZOMBIE_CONE3);
				}
				else if (this.mHelmType == HelmType.HELMTYPE_PAIL && helmDamageIndex2 == 1)
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_bucket, AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET2);
				}
				else if (this.mHelmType == HelmType.HELMTYPE_PAIL && helmDamageIndex2 == 2)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_bucket, AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET3);
				}
				else if (this.mHelmType == HelmType.HELMTYPE_DIGGER && helmDamageIndex2 == 1)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_digger_hardhat, AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_HARDHAT2);
				}
				else if (this.mHelmType == HelmType.HELMTYPE_DIGGER && helmDamageIndex2 == 2)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_digger_hardhat, AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_HARDHAT3);
				}
				else if (this.mHelmType == HelmType.HELMTYPE_FOOTBALL && helmDamageIndex2 == 1)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_football_helmet, AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET2);
				}
				else if (this.mHelmType == HelmType.HELMTYPE_FOOTBALL && helmDamageIndex2 == 2)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_football_helmet, AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET3);
				}
				else if (this.mHelmType == HelmType.HELMTYPE_WALLNUT && helmDamageIndex2 == 1)
				{
					Reanimation reanimation2 = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
					if (reanimation2 != null)
					{
						reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_face, AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED1);
					}
				}
				else if (this.mHelmType == HelmType.HELMTYPE_WALLNUT && helmDamageIndex2 == 2)
				{
					Reanimation reanimation3 = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
					if (reanimation3 != null)
					{
						reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_face, AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED2);
					}
				}
				else if (this.mHelmType == HelmType.HELMTYPE_TALLNUT && helmDamageIndex2 == 1)
				{
					Reanimation reanimation4 = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
					if (reanimation4 != null)
					{
						reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_idle, AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED1);
					}
				}
				else if (this.mHelmType == HelmType.HELMTYPE_TALLNUT && helmDamageIndex2 == 2)
				{
					Reanimation reanimation5 = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
					if (reanimation5 != null)
					{
						reanimation5.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_idle, AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED2);
					}
				}
			}
			return result;
		}

		public void TakeBodyDamage(int theDamage, uint theDamageFlags)
		{
			if (!TodCommon.TestBit(theDamageFlags, 3))
			{
				this.mJustGotShotCounter = 25;
			}
			if (TodCommon.TestBit(theDamageFlags, 2))
			{
				this.ApplyChill(false);
			}
			int num = this.mBodyHealth;
			int bodyDamageIndex = this.GetBodyDamageIndex();
			this.mBodyHealth -= theDamage;
			int bodyDamageIndex2 = this.GetBodyDamageIndex();
			if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (!TodCommon.TestBit(theDamageFlags, 3))
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_SHIELD_HIT);
				}
				if (TodCommon.TestBit(theDamageFlags, 5))
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_1, AtlasResources.IMAGE_REANIM_ZOMBIE_ZAMBONI_1_DAMAGE2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_2, AtlasResources.IMAGE_REANIM_ZOMBIE_ZAMBONI_2_DAMAGE2);
					this.ZamboniDeath(theDamageFlags);
				}
				else if (this.mBodyHealth <= 0)
				{
					this.ZamboniDeath(theDamageFlags);
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
					this.AddAttachedParticle(27, 72, ParticleEffect.PARTICLE_ZAMBONI_SMOKE);
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (TodCommon.TestBit(theDamageFlags, 5) || this.mBodyHealth <= 0)
				{
					reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_siding, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_SIDING_DAMAGE);
					this.CatapultDeath(theDamageFlags);
				}
				else if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 1)
				{
					reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_siding, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_SIDING_DAMAGE);
				}
				else if (bodyDamageIndex != bodyDamageIndex2 && bodyDamageIndex2 == 2)
				{
					this.AddAttachedParticle(47, 77, ParticleEffect.PARTICLE_ZAMBONI_SMOKE);
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mBodyReanimID);
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
					if (this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
					{
						reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_HEAD2_REDEYE);
					}
					else
					{
						reanimation3.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, AtlasResources.IMAGE_REANIM_ZOMBIE_GARGANTUAR_HEAD2);
					}
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				if (!TodCommon.TestBit(theDamageFlags, 3))
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_SHIELD_HIT);
				}
				Reanimation reanimation4 = this.mApp.ReanimationGet(this.mBodyReanimID);
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
					this.ApplyBossSmokeParticles(true);
				}
				if (num >= this.mBodyMaxHealth / GameConstants.BOSS_FLASH_HEALTH_FRACTION && this.mBodyHealth < this.mBodyMaxHealth / GameConstants.BOSS_FLASH_HEALTH_FRACTION)
				{
					this.mApp.AddTodParticle(770f, 260f, 400000, ParticleEffect.PARTICLE_BOSS_EXPLOSION);
					this.mApp.PlayFoley(FoleyType.FOLEY_BOSS_EXPLOSION_SMALL);
					this.ApplyBossSmokeParticles(true);
				}
				if (this.mBodyHealth <= 0)
				{
					this.mBodyHealth = 1;
				}
			}
			else
			{
				this.UpdateDamageStates(theDamageFlags);
			}
			if (this.mBodyHealth <= 0)
			{
				this.mBodyHealth = 0;
				this.PlayDeathAnim(theDamageFlags);
				this.DropLoot();
			}
		}

		public void AttachShield()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			string theTrackName = string.Empty;
			if (this.mShieldType == ShieldType.SHIELDTYPE_DOOR)
			{
				this.ShowDoorArms(true);
				this.ReanimShowPrefix("Zombie_outerarm_screendoor", GameConstants.RENDER_GROUP_OVER_SHIELD);
				theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_screendoor;
			}
			else if (this.mShieldType == ShieldType.SHIELDTYPE_NEWSPAPER)
			{
				this.ReanimShowPrefix("Zombie_paper_hands", GameConstants.RENDER_GROUP_OVER_SHIELD);
				theTrackName = GlobalMembersReanimIds.ReanimTrackId_zombie_paper_paper;
			}
			else if (this.mShieldType == ShieldType.SHIELDTYPE_LADDER)
			{
				this.ReanimShowPrefix("Zombie_outerarm", GameConstants.RENDER_GROUP_OVER_SHIELD);
				theTrackName = GlobalMembersReanimIds.ReanimTrackId_zombie_ladder_1;
			}
			else
			{
				Debug.ASSERT(false);
			}
			reanimation.AssignRenderGroupToTrack(theTrackName, GameConstants.RENDER_GROUP_SHIELD);
		}

		public void DetachShield()
		{
			if (this.mApp.ReanimationTryToGet(this.mBodyReanimID) == null)
			{
				this.mShieldType = ShieldType.SHIELDTYPE_NONE;
				this.mShieldHealth = 0;
				return;
			}
			if (this.mShieldType == ShieldType.SHIELDTYPE_DOOR)
			{
				this.ShowDoorArms(false);
			}
			else if (this.mShieldType == ShieldType.SHIELDTYPE_NEWSPAPER)
			{
				this.ReanimShowPrefix("Zombie_paper_hands", 0);
			}
			else if (this.mShieldType == ShieldType.SHIELDTYPE_LADDER)
			{
				this.ReanimShowPrefix("Zombie_outerarm", 0);
				this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
				if (this.mIsEating)
				{
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_eat, ReanimLoopType.REANIM_LOOP, 20, 0f);
				}
				else
				{
					this.StartWalkAnim(0);
				}
			}
			else
			{
				Debug.ASSERT(false);
			}
			this.mShieldType = ShieldType.SHIELDTYPE_NONE;
			this.mShieldHealth = 0;
			this.mHasShield = false;
		}

		public void UpdateReanim()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null || reanimation.mDead)
			{
				return;
			}
			bool flag = false;
			if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				flag = true;
				int bodyDamageIndex = this.GetBodyDamageIndex();
				bool flag2 = this.mSummonCounter == 0;
				if (bodyDamageIndex == 2 || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING)
				{
					reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
					Image currentTrackImage = reanimation.GetCurrentTrackImage(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_pole);
					if (currentTrackImage == AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_POLE_WITHBALL && !flag2)
					{
						reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_pole, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_POLE_DAMAGE_WITHBALL);
					}
					else
					{
						reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_pole, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_POLE_DAMAGE);
					}
				}
				else if (flag2)
				{
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_pole, AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_POLE);
				}
			}
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			float num = zombieDrawPosition.mImageOffsetX;
			float num2 = zombieDrawPosition.mImageOffsetY + zombieDrawPosition.mBodyY - 28f;
			num += 15f;
			num2 += 20f;
			if ((this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombieType == ZombieType.ZOMBIE_CATAPULT) && this.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_BURNED)
			{
				if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING)
				{
					float num3 = TodCommon.TodAnimateCurveFloatTime(0.7f, 1f, reanimation.mAnimTime, 0f, 1f, TodCurves.CURVE_EASE_OUT);
					num += TodCommon.RandRangeFloat(-num3, num3);
					num2 += TodCommon.RandRangeFloat(-num3, num3);
				}
				else if (this.mBodyHealth < 200)
				{
					num += TodCommon.RandRangeFloat(-1f, 1f);
					num2 += TodCommon.RandRangeFloat(-1f, 1f);
				}
			}
			if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL && this.mScaleZombie < 1f)
			{
				num2 += 20f - this.mScaleZombie * 20f;
			}
			bool flag3 = false;
			if (this.IsWalkingBackwards())
			{
				flag3 = true;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				bool flag4 = false;
				if ((this.mZombiePhase == ZombiePhase.PHASE_DANCER_DANCING_IN || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RAISE_RIGHT_1 || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RAISE_RIGHT_2) && !this.mIsEating)
				{
					flag4 = true;
				}
				if (this.mMindControlled)
				{
					flag4 = !flag4;
				}
				flag3 = flag4;
			}
			if (flag3)
			{
				num += 90f * this.mScaleZombie;
			}
			Matrix identity = Matrix.Identity;
			identity.M11 = this.mScaleZombie;
			identity.M22 = this.mScaleZombie;
			identity.M41 = (num + 30f - this.mScaleZombie * 30f) * Constants.S;
			identity.M42 = (num2 + 120f - this.mScaleZombie * 120f) * Constants.S;
			if (flag3)
			{
				identity.M11 = -this.mScaleZombie;
			}
			if (reanimation.mOverlayMatrix.mMatrix != identity)
			{
				flag = true;
				reanimation.mOverlayMatrix.mMatrix = identity;
			}
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(this.mMoweredReanimID);
			if (reanimation2 != null)
			{
				reanimation2.Update();
				SexyTransform2D sexyTransform2D = default(SexyTransform2D);
				reanimation2.GetAttachmentOverlayMatrix(0, out sexyTransform2D);
				sexyTransform2D.mMatrix.M11 = sexyTransform2D.mMatrix.M11 * reanimation.mOverlayMatrix.mMatrix.M11;
				sexyTransform2D.mMatrix.M21 = sexyTransform2D.mMatrix.M21 * reanimation.mOverlayMatrix.mMatrix.M11;
				sexyTransform2D.mMatrix.M12 = sexyTransform2D.mMatrix.M12 * reanimation.mOverlayMatrix.mMatrix.M22;
				sexyTransform2D.mMatrix.M22 = sexyTransform2D.mMatrix.M22 * reanimation.mOverlayMatrix.mMatrix.M22;
				sexyTransform2D.mMatrix.M41 = sexyTransform2D.mMatrix.M41 * reanimation.mOverlayMatrix.mMatrix.M11;
				sexyTransform2D.mMatrix.M42 = sexyTransform2D.mMatrix.M42 * reanimation.mOverlayMatrix.mMatrix.M22;
				sexyTransform2D.mMatrix.M41 = sexyTransform2D.mMatrix.M41 + reanimation.mOverlayMatrix.mMatrix.M22;
				sexyTransform2D.mMatrix.M42 = sexyTransform2D.mMatrix.M42 + reanimation.mOverlayMatrix.mMatrix.M42;
				if (reanimation.mOverlayMatrix != sexyTransform2D)
				{
					flag = true;
					reanimation.mOverlayMatrix = sexyTransform2D;
				}
			}
			reanimation.Update();
			if (flag)
			{
				reanimation.PropogateColorToAttachments();
			}
		}

		public void GetTrackPosition(ref string theTrackName, ref float thePosX, ref float thePosY)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				thePosX = this.mPosX;
				thePosY = this.mPosY;
				return;
			}
			int theTrackIndex = reanimation.FindTrackIndex(theTrackName);
			SexyTransform2D sexyTransform2D = default(SexyTransform2D);
			reanimation.GetTrackTranslationMatrix(theTrackIndex, ref sexyTransform2D);
			thePosX = sexyTransform2D.mMatrix.M41 * Constants.IS + this.mPosX;
			thePosY = sexyTransform2D.mMatrix.M42 * Constants.IS + this.mPosY;
		}

		public void LoadPlainZombieReanim()
		{
			this.mZombieAttackRect = new TRect(20, 0, 50, 115);
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			Zombie.SetupReanimLayers(reanimation, this.mZombieType);
			if (this.mBoard != null)
			{
				this.EnableMustache(this.mBoard.mMustacheMode);
				this.EnableFuture(this.mBoard.mFutureMode);
			}
			bool flag = false;
			if (this.mBoard != null && this.mBoard.mPlantRow[this.mRow] == PlantRowType.PLANTROW_POOL)
			{
				flag = true;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DUCKY_TUBE)
			{
				flag = true;
			}
			if (flag)
			{
				this.ReanimShowPrefix("zombie_duckytube", 0);
				this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_duckytube, true);
				this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, true);
				this.ReanimIgnoreClipRect(GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm3, true);
				this.SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_whitewater);
				this.SetupWaterTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_whitewater2);
			}
		}

		public void ShowDoorArms(bool theShow)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation != null)
			{
				Zombie.SetupDoorArms(reanimation, theShow);
				if (!this.mHasArm)
				{
					this.ReanimShowPrefix("Zombie_outerarm_lower", -1);
					this.ReanimShowPrefix("Zombie_outerarm_hand", -1);
				}
			}
		}

		public void ReanimShowTrack(ref string theTrackName, int theRenderGroup)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			reanimation.AssignRenderGroupToTrack(theTrackName, theRenderGroup);
		}

		public void PlayZombieAppearSound()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_DOLPHIN_APPEARS);
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_BALLOONINFLATE);
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_ZAMBONI);
			}
		}

		public void StartMindControlled()
		{
			this.mApp.PlaySample(Resources.SOUND_MINDCONTROLLED);
			this.mMindControlled = true;
			this.mLastPortalX = -1;
			if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
			{
				for (int i = 0; i < GameConstants.NUM_BACKUP_DANCERS; i++)
				{
					this.mFollowerZombieID[i] = null;
				}
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				Zombie zombie = this.mBoard.ZombieTryToGet(this.mRelatedZombieID);
				if (zombie != null)
				{
					Zombie zombie2 = this.mBoard.ZombieGetID(this);
					for (int j = 0; j < GameConstants.NUM_BACKUP_DANCERS; j++)
					{
						if (zombie.mFollowerZombieID[j] == zombie2)
						{
							zombie.mFollowerZombieID[j] = null;
							break;
						}
					}
				}
				this.mRelatedZombieID = null;
				return;
			}
			Zombie zombie3 = this.mBoard.ZombieTryToGet(this.mRelatedZombieID);
			if (zombie3 != null)
			{
				zombie3.mRelatedZombieID = null;
				this.mRelatedZombieID = null;
			}
		}

		public bool IsFlying()
		{
			return this.mZombiePhase == ZombiePhase.PHASE_BALLOON_FLYING || this.mZombiePhase == ZombiePhase.PHASE_BALLOON_POPPING;
		}

		private void SetupReanimForLostHead()
		{
			this.ReanimShowPrefix("anim_head", -1);
			this.ReanimShowPrefix("anim_hair", -1);
			this.ReanimShowPrefix("anim_tongue", -1);
		}

		public void DropHead(uint theDamageFlags)
		{
			if (!this.CanLoseBodyParts())
			{
				return;
			}
			if (!this.mHasHead)
			{
				return;
			}
			if (this.mButteredCounter > 0)
			{
				this.mButteredCounter = 0;
				this.UpdateAnimSpeed();
			}
			this.mHasHead = false;
			this.SetupReanimForLostHead();
			if (TodCommon.TestBit(theDamageFlags, 4))
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD || this.mZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD || this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD || this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD)
			{
				this.DetachPlantHead();
				Reanimation reanimation = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				if (reanimation != null)
				{
					reanimation.ReanimationDie();
				}
				this.mSpecialHeadReanimID = null;
				return;
			}
			int aRenderOrder = this.mRenderOrder + 1;
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			float num = this.mPosX + zombieDrawPosition.mImageOffsetX + (float)zombieDrawPosition.mHeadX + 11f;
			float num2 = this.mPosY + zombieDrawPosition.mImageOffsetY + (float)zombieDrawPosition.mHeadY + zombieDrawPosition.mBodyY + 21f;
			if (this.mBodyReanimID != null)
			{
				this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_head1, ref num, ref num2);
			}
			ParticleEffect theEffect = ParticleEffect.PARTICLE_ZOMBIE_HEAD;
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_MOWERED)
			{
				theEffect = ParticleEffect.PARTICLE_MOWERED_ZOMBIE_HEAD;
				num -= 40f;
				num2 -= 50f;
			}
			else if (this.mInPool)
			{
				theEffect = ParticleEffect.PARTICLE_ZOMBIE_HEAD_POOL;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
			{
				aRenderOrder = this.mRenderOrder - 1;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER)
			{
				theEffect = ParticleEffect.PARTICLE_ZOMBIE_NEWSPAPER_HEAD;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				this.PogoBreak(theDamageFlags);
				theEffect = ParticleEffect.PARTICLE_ZOMBIE_POGO_HEAD;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				this.ReanimShowPrefix("anim_hat", -1);
				this.ReanimShowPrefix("hat", -1);
				theEffect = ParticleEffect.PARTICLE_ZOMBIE_BALLOON_HEAD;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				this.DropPole();
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_FLAG)
			{
				this.DropFlag();
			}
			TodParticleSystem todParticleSystem = this.mApp.AddTodParticle(num, num2, aRenderOrder, theEffect);
			this.OverrideParticleColor(todParticleSystem);
			this.OverrideParticleScale(todParticleSystem);
			if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_DANCER)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_DISCO);
				todParticleSystem.OverrideScale(null, 1.2f);
				this.ReanimShowPrefix("Zombie_disco_glasses", -1);
				TodParticleSystem todParticleSystem2 = this.mApp.AddTodParticle(num, num2, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_MUSTACHE);
				this.OverrideParticleColor(todParticleSystem2);
				this.OverrideParticleScale(todParticleSystem2);
				if (todParticleSystem2 != null)
				{
					todParticleSystem2.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_DISCO_GLASSES);
				}
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_DANCER_BACKUP_HEAD);
				todParticleSystem.OverrideScale(null, 1.2f);
				this.ReanimShowPrefix("Zombie_backup_stash", -1);
				todParticleSystem = this.mApp.AddTodParticle(num, num2, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_MUSTACHE);
				this.OverrideParticleColor(todParticleSystem);
				this.OverrideParticleScale(todParticleSystem);
				if (todParticleSystem != null)
				{
					todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_BACKUP_STASH);
				}
				this.ReanimShowPrefix("anim_head2", -1);
				todParticleSystem = this.mApp.AddTodParticle(num, num2, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_MUSTACHE);
				this.OverrideParticleColor(todParticleSystem);
				this.OverrideParticleScale(todParticleSystem);
				if (todParticleSystem != null)
				{
					todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_JAW);
				}
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEBOBSLEDHEAD);
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_LADDER)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIELADDERHEAD);
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_IMP)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEIMPHEAD);
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEFOOTBALLHEAD);
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEPOLEVAULTERHEAD);
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_SNORKLE_HEAD);
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEDIGGERHEAD);
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEDOLPHINRIDERHEAD);
			}
			else if (todParticleSystem != null && this.mZombieType == ZombieType.ZOMBIE_YETI)
			{
				todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEYETIHEAD);
			}
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (this.mBoard.mMustacheMode && reanimation2.TrackExists(GlobalMembersReanimIds.ReanimTrackId_zombie_mustache))
			{
				this.ReanimShowPrefix("Zombie_mustache", -1);
				TodParticleSystem todParticleSystem3 = this.mApp.AddTodParticle(num, num2, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_MUSTACHE);
				this.OverrideParticleColor(todParticleSystem3);
				this.OverrideParticleScale(todParticleSystem3);
				Image imageOverride = reanimation2.GetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_mustache);
				if (todParticleSystem3 != null && imageOverride != null)
				{
					todParticleSystem3.OverrideImage(null, imageOverride);
				}
			}
			if (this.mBoard.mFutureMode)
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
					TodParticleSystem todParticleSystem4 = this.mApp.AddTodParticle(num, num2, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_FUTURE_GLASSES);
					this.OverrideParticleColor(todParticleSystem4);
					this.OverrideParticleScale(todParticleSystem4);
					if (todParticleSystem4 != null)
					{
						todParticleSystem4.OverrideFrame(null, num3);
					}
				}
			}
			if (this.mBoard.mPinataMode && this.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_MOWERED)
			{
				TodParticleSystem aParticle = this.mApp.AddTodParticle(num, num2, aRenderOrder, ParticleEffect.PARTICLE_PINATA);
				this.OverrideParticleScale(aParticle);
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_LIMBS_POP);
		}

		public bool CanTargetPlant(Plant thePlant, Zombie.ZombieAttackType theAttackType)
		{
			if (this.mApp.IsWallnutBowlingLevel() && theAttackType != Zombie.ZombieAttackType.ATTACKTYPE_VAULT)
			{
				return false;
			}
			if (thePlant.NotOnGround())
			{
				return false;
			}
			if (thePlant.mSeedType == SeedType.SEED_TANGLEKELP)
			{
				return false;
			}
			if (!this.mInPool && this.mBoard.IsPoolSquare(thePlant.mPlantCol, thePlant.mRow))
			{
				return false;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING)
			{
				return thePlant.mSeedType == SeedType.SEED_POTATOMINE && thePlant.mState == PlantState.STATE_NOTREADY;
			}
			if (thePlant.IsSpiky())
			{
				return this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || (this.mBoard.IsPoolSquare(thePlant.mPlantCol, thePlant.mRow) || this.mBoard.GetFlowerPotAt(thePlant.mPlantCol, thePlant.mRow) != null);
			}
			if (theAttackType == Zombie.ZombieAttackType.ATTACKTYPE_DRIVE_OVER)
			{
				if (thePlant.mSeedType == SeedType.SEED_CHERRYBOMB || thePlant.mSeedType == SeedType.SEED_JALAPENO || thePlant.mSeedType == SeedType.SEED_BLOVER || thePlant.mSeedType == SeedType.SEED_SQUASH)
				{
					return false;
				}
				if (thePlant.mSeedType == SeedType.SEED_DOOMSHROOM || thePlant.mSeedType == SeedType.SEED_ICESHROOM)
				{
					return thePlant.mIsAsleep;
				}
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_LADDER_CARRYING || this.mZombiePhase == ZombiePhase.PHASE_LADDER_PLACING)
			{
				bool flag = false;
				if (thePlant.mSeedType == SeedType.SEED_WALLNUT || thePlant.mSeedType == SeedType.SEED_TALLNUT || thePlant.mSeedType == SeedType.SEED_PUMPKINSHELL)
				{
					flag = true;
				}
				if (this.mBoard.GetLadderAt(thePlant.mPlantCol, thePlant.mRow) != null)
				{
					flag = false;
				}
				if (theAttackType == Zombie.ZombieAttackType.ATTACKTYPE_CHEW && flag)
				{
					return false;
				}
				if (theAttackType == Zombie.ZombieAttackType.ATTACKTYPE_LADDER && !flag)
				{
					return false;
				}
			}
			if (theAttackType == Zombie.ZombieAttackType.ATTACKTYPE_CHEW)
			{
				Plant topPlantAt = this.mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, PlantPriority.TOPPLANT_EATING_ORDER);
				if (topPlantAt != thePlant && topPlantAt != null && this.CanTargetPlant(topPlantAt, theAttackType))
				{
					return false;
				}
			}
			if (theAttackType == Zombie.ZombieAttackType.ATTACKTYPE_VAULT)
			{
				Plant topPlantAt2 = this.mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
				if (topPlantAt2 != thePlant && topPlantAt2 != null && this.CanTargetPlant(topPlantAt2, theAttackType))
				{
					return false;
				}
			}
			return true;
		}

		public void UpdateZombieCatapult()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_NORMAL)
			{
				if (this.mPosX <= (float)(650 + Constants.BOARD_EXTRA_ROOM) && this.FindCatapultTarget() != null && this.mSummonCounter > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_CATAPULT_LAUNCHING;
					this.mPhaseCounter = 300;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_shoot, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
					return;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_CATAPULT_LAUNCHING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.ShouldTriggerTimedEvent(0.545f))
				{
					Plant thePlant = this.FindCatapultTarget();
					this.ZombieCatapultFire(thePlant);
				}
				if (reanimation.mLoopCount > 0)
				{
					this.mSummonCounter--;
					if (this.mSummonCounter == 4)
					{
						this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_basketball, -1);
					}
					else if (this.mSummonCounter == 3)
					{
						this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_basketball2, -1);
					}
					else if (this.mSummonCounter == 2)
					{
						this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_basketball3, -1);
					}
					else if (this.mSummonCounter == 1)
					{
						this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_catapult_basketball4, -1);
					}
					if (this.mSummonCounter == 0)
					{
						this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, 20, 6f);
						this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
						return;
					}
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 20, 12f);
					this.mZombiePhase = ZombiePhase.PHASE_CATAPULT_RELOADING;
					return;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_CATAPULT_RELOADING && this.mPhaseCounter == 0)
			{
				Plant plant = this.FindCatapultTarget();
				if (plant != null)
				{
					this.mZombiePhase = ZombiePhase.PHASE_CATAPULT_LAUNCHING;
					this.mPhaseCounter = 300;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_shoot, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
					return;
				}
				this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, 20, 6f);
				this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
			}
		}

		public Plant FindCatapultTarget()
		{
			Plant plant = null;
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant2 = this.mBoard.mPlants[i];
				if (!plant2.mDead && this.mRow == plant2.mRow && this.mX >= plant2.mX + 100 && !plant2.NotOnGround() && !plant2.IsSpiky() && (plant == null || plant2.mPlantCol < plant.mPlantCol))
				{
					plant = this.mBoard.GetTopPlantAt(plant2.mPlantCol, plant2.mRow, PlantPriority.TOPPLANT_CATAPULT_ORDER);
				}
			}
			return plant;
		}

		public void ZombieCatapultFire(Plant thePlant)
		{
			float num = this.mPosX + 113f;
			float num2 = this.mPosY - 44f;
			int num3;
			int num4;
			if (thePlant != null)
			{
				num3 = thePlant.mX;
				num4 = thePlant.mY;
			}
			else
			{
				num3 = (int)this.mPosX - 300;
				num4 = 0;
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_BASKETBALL);
			Projectile projectile = this.mBoard.AddProjectile((int)num, (int)num2, this.mRenderOrder, this.mRow, ProjectileType.PROJECTILE_BASKETBALL);
			float num5 = num - (float)num3 - 20f;
			float num6 = (float)num4 - num2;
			if (num5 < 40f)
			{
				num5 = 40f;
			}
			projectile.mMotionType = ProjectileMotion.MOTION_LOBBED;
			float num7 = 120f;
			projectile.mVelX = -num5 / num7;
			projectile.mVelY = 0f;
			projectile.mVelZ = -7f + num6 / num7;
			projectile.mAccZ = 0.115f;
		}

		public void UpdateClimbingLadder()
		{
			float num = this.mAltitude;
			if (this.mOnHighGround)
			{
				num -= (float)Constants.HIGH_GROUND_HEIGHT;
			}
			int theGridX = this.mBoard.PixelToGridXKeepOnBoard((int)((float)(this.mX + 5) + num * 0.5f), this.mY);
			if (this.mBoard.GetLadderAt(theGridX, this.mRow) == null)
			{
				this.mZombieHeight = ZombieHeight.HEIGHT_FALLING;
				return;
			}
			this.mAltitude += 0.8f;
			if (this.mVelX < 0.5f)
			{
				this.mPosX -= 0.5f;
			}
			float num2 = 90f;
			if (this.mOnHighGround)
			{
				num2 += (float)Constants.HIGH_GROUND_HEIGHT;
			}
			if (this.mAltitude >= num2)
			{
				this.mZombieHeight = ZombieHeight.HEIGHT_FALLING;
			}
		}

		public void UpdateZombieGargantuar()
		{
			Plant plant;
			if (this.mZombiePhase == ZombiePhase.PHASE_GARGANTUAR_SMASHING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.ShouldTriggerTimedEvent(0.64f))
				{
					plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_CHEW);
					if (plant != null && plant.mSeedType == SeedType.SEED_SPIKEROCK)
					{
						this.TakeDamage(20, 32U);
						plant.SpikeRockTakeDamage();
						if (plant.mPlantHealth <= 0)
						{
							this.SquishAllInSquare(plant.mPlantCol, plant.mRow, Zombie.ZombieAttackType.ATTACKTYPE_CHEW);
						}
					}
					else if (plant != null)
					{
						this.SquishAllInSquare(plant.mPlantCol, plant.mRow, Zombie.ZombieAttackType.ATTACKTYPE_CHEW);
					}
					if (this.mApp.IsScaryPotterLevel())
					{
						int theGridX = this.mBoard.PixelToGridX((int)this.mPosX, (int)this.mPosY);
						GridItem scaryPotAt = this.mBoard.GetScaryPotAt(theGridX, this.mRow);
						if (scaryPotAt != null)
						{
							this.mBoard.mChallenge.ScaryPotterOpenPot(scaryPotAt);
						}
					}
					if (this.mApp.IsIZombieLevel())
					{
						GridItem gridItem = this.mBoard.mChallenge.IZombieGetBrainTarget(this);
						if (gridItem != null)
						{
							this.mBoard.mChallenge.IZombieSquishBrain(gridItem);
						}
					}
					this.mApp.PlayFoley(FoleyType.FOLEY_THUMP);
					this.mApp.Vibrate();
					this.mBoard.ShakeBoard(0, 3);
				}
				if (reanimation.mLoopCount > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
					this.StartWalkAnim(20);
				}
				return;
			}
			float num = this.mPosX - 460f;
			if (this.mZombiePhase == ZombiePhase.PHASE_GARGANTUAR_THROWING)
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation2.ShouldTriggerTimedEvent(0.74f))
				{
					this.mHasObject = false;
					this.ReanimShowPrefix("Zombie_imp", -1);
					this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_gargantuar_whiterope, -1);
					this.mApp.PlayFoley(FoleyType.FOLEY_SWING);
					Zombie zombie = this.mBoard.AddZombie(ZombieType.ZOMBIE_IMP, this.mFromWave);
					if (zombie == null)
					{
						return;
					}
					float num2 = 40f;
					if (this.mBoard.StageHasRoof())
					{
						num -= 180f;
						num2 = -140f;
					}
					if (num < num2)
					{
						num = num2;
					}
					else if (num > 140f)
					{
						num -= TodCommon.RandRangeFloat(0f, 100f);
					}
					zombie.mPosX = this.mPosX - 133f;
					zombie.mPosY = this.GetPosYBasedOnRow(this.mRow);
					zombie.SetRow(this.mRow);
					zombie.mVariant = false;
					zombie.mRenderOrder = this.mRenderOrder + 1;
					zombie.mZombiePhase = ZombiePhase.PHASE_IMP_GETTING_THROWN;
					zombie.mAltitude = 88f;
					zombie.mVelX = 3f;
					zombie.mChilledCounter = this.mChilledCounter;
					float num3 = num / zombie.mVelX;
					zombie.mVelZ = 0.5f * num3 * GameConstants.THOWN_ZOMBIE_GRAVITY;
					zombie.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_thrown, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 18f);
					zombie.UpdateReanim();
					this.mApp.PlayFoley(FoleyType.FOLEY_IMP);
				}
				if (reanimation2.mLoopCount > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
					this.StartWalkAnim(20);
				}
				return;
			}
			if (this.IsImmobilizied() || !this.mHasHead)
			{
				return;
			}
			if (this.mHasObject && this.mBodyHealth < this.mBodyMaxHealth / 2 && num > 40f)
			{
				this.mZombiePhase = ZombiePhase.PHASE_GARGANTUAR_THROWING;
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_throw, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
				return;
			}
			bool flag = false;
			plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_CHEW);
			if (plant != null)
			{
				flag = true;
			}
			else if (this.mApp.IsScaryPotterLevel())
			{
				int theGridX2 = this.mBoard.PixelToGridX((int)this.mPosX, (int)this.mPosY);
				if (this.mBoard.GetScaryPotAt(theGridX2, this.mRow) != null)
				{
					flag = true;
				}
			}
			else if (this.mApp.IsIZombieLevel() && this.mBoard.mChallenge.IZombieGetBrainTarget(this) != null)
			{
				flag = true;
			}
			if (flag)
			{
				this.mZombiePhase = ZombiePhase.PHASE_GARGANTUAR_SMASHING;
				this.mApp.PlayFoley(FoleyType.FOLEY_LOWGROAN);
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_smash, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 16f);
			}
		}

		public int GetBodyDamageIndex()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				if (this.mBodyHealth < this.mBodyMaxHealth / 2)
				{
					return 2;
				}
				if (this.mBodyHealth < this.mBodyMaxHealth * 4 / 5)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (this.mBodyHealth < this.mBodyMaxHealth / 3)
				{
					return 2;
				}
				if (this.mBodyHealth < this.mBodyMaxHealth * 2 / 3)
				{
					return 1;
				}
				return 0;
			}
		}

		public void ApplyBurn()
		{
			if (this.mDead || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED)
			{
				return;
			}
			if (this.mBodyHealth >= 1800 || this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				this.TakeDamage(1800, 18U);
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD && !this.mHasHead)
			{
				this.mApp.RemoveReanimation(ref this.mSpecialHeadReanimID);
				this.mSpecialHeadReanimID = null;
			}
			if (this.mIceTrapCounter > 0)
			{
				this.RemoveIceTrap();
			}
			if (this.mButteredCounter > 0)
			{
				this.mButteredCounter = 0;
			}
			GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_ZAMBONI_SMOKE, null);
			this.BungeeDropPlant();
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING || this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || this.mZombiePhase == ZombiePhase.PHASE_IMP_GETTING_THROWN || this.mZombiePhase == ZombiePhase.PHASE_RISING_FROM_GRAVE || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RISING || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_IN_JUMP || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_RIDING || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING_PAUSE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_MOWERED || this.mInPool)
			{
				this.DieWithLoot();
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE || this.mZombieType == ZombieType.ZOMBIE_YETI || this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD || this.mZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD || this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD || this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD || this.mZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD || this.IsBobsledTeamWithSled() || this.IsFlying() || !this.mHasHead)
			{
				this.SetAnimRate(0f);
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mSpecialHeadReanimID);
				if (reanimation != null)
				{
					reanimation.mAnimRate = 0f;
					reanimation.mColorOverride = Color.Black;
					reanimation.mEnableExtraAdditiveDraw = false;
					reanimation.mEnableExtraOverlayDraw = false;
				}
				this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_BURNED;
				this.mPhaseCounter = 300;
				this.mJustGotShotCounter = 0;
				this.DropLoot();
				if (this.mZombieType == ZombieType.ZOMBIE_BUNGEE)
				{
					this.mZombieFade = 50;
				}
				if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
				{
					Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
					ReanimatorTrackInstance trackInstanceByName = reanimation2.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_hat);
					Reanimation reanimation3 = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName.mAttachmentID);
					if (reanimation3 != null)
					{
						reanimation3.mAnimRate = 0f;
					}
					this.mZombieFade = 50;
				}
			}
			else
			{
				ReanimationType theReanimationType = ReanimationType.REANIM_ZOMBIE_CHARRED;
				float num = this.mPosX + 22f;
				float num2 = this.mPosY - 10f;
				if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
				{
					num2 += 31f;
				}
				if (this.mZombieType == ZombieType.ZOMBIE_IMP)
				{
					num -= 6f;
					theReanimationType = ReanimationType.REANIM_ZOMBIE_CHARRED_IMP;
				}
				if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
				{
					if (this.IsWalkingBackwards())
					{
						num += 14f;
					}
					theReanimationType = ReanimationType.REANIM_ZOMBIE_CHARRED_DIGGER;
				}
				if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
				{
					num += 61f;
					num2 += -16f;
					theReanimationType = ReanimationType.REANIM_ZOMBIE_CHARRED_ZAMBONI;
				}
				if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
				{
					num += -36f;
					num2 += -20f;
					theReanimationType = ReanimationType.REANIM_ZOMBIE_CHARRED_CATAPULT;
				}
				if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
				{
					num += -15f;
					num2 += -10f;
					theReanimationType = ReanimationType.REANIM_ZOMBIE_CHARRED_GARGANTUAR;
				}
				Reanimation reanimation4 = this.mApp.AddReanimation(num, num2, this.mRenderOrder, theReanimationType);
				reanimation4.mAnimRate *= TodCommon.RandRangeFloat(0.9f, 1.1f);
				if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_WALKING_WITHOUT_AXE)
				{
					reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_crumble_noaxe);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
				{
					reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_crumble);
				}
				else if ((this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR) && !this.mHasObject)
				{
					reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_impblink, AtlasResources.IMAGE_BLANK);
					reanimation4.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_imphead, AtlasResources.IMAGE_BLANK);
				}
				if (this.mScaleZombie != 1f)
				{
					reanimation4.mOverlayMatrix.mMatrix.M11 = this.mScaleZombie;
					reanimation4.mOverlayMatrix.mMatrix.M22 = this.mScaleZombie;
					Reanimation reanimation5 = reanimation4;
					reanimation5.mOverlayMatrix.mMatrix.M41 = reanimation5.mOverlayMatrix.mMatrix.M41 + (20f - this.mScaleZombie * 20f) * Constants.S;
					Reanimation reanimation6 = reanimation4;
					reanimation6.mOverlayMatrix.mMatrix.M42 = reanimation6.mOverlayMatrix.mMatrix.M42 + (120f - this.mScaleZombie * 120f) * Constants.S;
					reanimation4.OverrideScale(this.mScaleZombie, this.mScaleZombie);
				}
				if (this.IsWalkingBackwards())
				{
					reanimation4.OverrideScale(-this.mScaleZombie, this.mScaleZombie);
					Reanimation reanimation7 = reanimation4;
					reanimation7.mOverlayMatrix.mMatrix.M41 = reanimation7.mOverlayMatrix.mMatrix.M41 + 60f * this.mScaleZombie * Constants.S;
				}
				this.DieWithLoot();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				this.BobsledBurn();
			}
		}

		public void UpdateBurn()
		{
			this.mPhaseCounter -= 3;
			if (this.mPhaseCounter == 0)
			{
				this.DieWithLoot();
			}
		}

		public bool ZombieNotWalking()
		{
			if (this.mIsEating || this.IsImmobilizied())
			{
				return true;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_JACK_IN_THE_BOX_POPPING || this.mZombiePhase == ZombiePhase.PHASE_NEWSPAPER_MADDENING || this.mZombiePhase == ZombiePhase.PHASE_GARGANTUAR_THROWING || this.mZombiePhase == ZombiePhase.PHASE_GARGANTUAR_SMASHING || this.mZombiePhase == ZombiePhase.PHASE_CATAPULT_LAUNCHING || this.mZombiePhase == ZombiePhase.PHASE_CATAPULT_RELOADING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISING || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING_PAUSE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE || this.mZombiePhase == ZombiePhase.PHASE_DIGGER_STUNNED || this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS || this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS_WITH_LIGHT || this.mZombiePhase == ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS_HOLD || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RISING || this.mZombiePhase == ZombiePhase.PHASE_IMP_GETTING_THROWN || this.mZombiePhase == ZombiePhase.PHASE_IMP_LANDING || this.mZombiePhase == ZombiePhase.PHASE_LADDER_PLACING || this.mZombieHeight == ZombieHeight.HEIGHT_IN_TO_CHIMNEY || this.mZombieHeight == ZombieHeight.HEIGHT_GETTING_BUNGEE_DROPPED || this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIQUARIUM || this.mZombieType == ZombieType.ZOMBIE_BUNGEE || this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				return true;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_RAISE_LEFT_1 || this.mZombiePhase == ZombiePhase.PHASE_DANCER_WALK_TO_RAISE || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RAISE_RIGHT_1 || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RAISE_LEFT_2 || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RAISE_RIGHT_2)
			{
				return true;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				Zombie zombie;
				if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
				{
					zombie = this;
				}
				else
				{
					zombie = this.mBoard.ZombieTryToGet(this.mRelatedZombieID);
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
					Zombie zombie2 = this.mBoard.ZombieTryToGet(zombie.mFollowerZombieID[i]);
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
			if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING)
			{
				return null;
			}
			TRect zombieAttackRect = this.GetZombieAttackRect();
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && this.mMindControlled != zombie.mMindControlled && !zombie.IsFlying() && zombie.mZombiePhase != ZombiePhase.PHASE_DIGGER_TUNNELING && zombie.mZombiePhase != ZombiePhase.PHASE_BUNGEE_DIVING && zombie.mZombiePhase != ZombiePhase.PHASE_BUNGEE_DIVING_SCREAMING && zombie.mZombiePhase != ZombiePhase.PHASE_BUNGEE_RISING && zombie.mZombieHeight != ZombieHeight.HEIGHT_GETTING_BUNGEE_DROPPED && !zombie.IsDeadOrDying() && zombie.mRow == this.mRow)
				{
					TRect zombieRect = zombie.GetZombieRect();
					int rectOverlap = GameConstants.GetRectOverlap(zombieAttackRect, zombieRect);
					if (rectOverlap >= 20 || (rectOverlap >= 0 && zombie.mIsEating))
					{
						return zombie;
					}
				}
			}
			return null;
		}

		public void PlayZombieReanim(ref string theTrackName, ReanimLoopType theLoopType, byte theBlendTime, float theAnimRate)
		{
			this.lastPlayedReanimName = theTrackName;
			this.lastPlayedReanimLoopType = theLoopType;
			this.lastPlayedReanimBlendTime = theBlendTime;
			this.lastPlayedReanimAnimRate = theAnimRate;
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			reanimation.PlayReanim(theTrackName, theLoopType, theBlendTime, theAnimRate);
			if (theAnimRate != 0f)
			{
				this.mOrginalAnimRate = theAnimRate;
			}
			this.UpdateAnimSpeed();
		}

		public void UpdateZombieBackupDancer()
		{
			if (this.mIsEating)
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_DANCER_RISING)
			{
				this.mAltitude = (float)TodCommon.TodAnimateCurve(150, 0, this.mPhaseCounter, Constants.ZOMBIE_BACKUP_DANCER_RISE_HEIGHT, 0, TodCurves.CURVE_LINEAR);
				this.mUsesClipping = (this.mAltitude < 0f);
				if (this.mPhaseCounter != 0)
				{
					return;
				}
				if (this.IsOnHighGround())
				{
					this.mAltitude = (float)Constants.HIGH_GROUND_HEIGHT;
				}
			}
			ZombiePhase dancerPhase = this.GetDancerPhase();
			if (dancerPhase != this.mZombiePhase)
			{
				if (dancerPhase == ZombiePhase.PHASE_DANCER_DANCING_LEFT)
				{
					this.mZombiePhase = dancerPhase;
					this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, 10, 0f);
					return;
				}
				if (dancerPhase == ZombiePhase.PHASE_DANCER_WALK_TO_RAISE)
				{
					this.mZombiePhase = dancerPhase;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.REANIM_LOOP, 10, 18f);
					Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
					reanimation.mAnimTime = 0.6f;
					return;
				}
				if (dancerPhase == ZombiePhase.PHASE_DANCER_RAISE_LEFT_1 || dancerPhase == ZombiePhase.PHASE_DANCER_RAISE_RIGHT_1 || dancerPhase == ZombiePhase.PHASE_DANCER_RAISE_LEFT_2 || dancerPhase == ZombiePhase.PHASE_DANCER_RAISE_RIGHT_2)
				{
					this.mZombiePhase = dancerPhase;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_armraise, ReanimLoopType.REANIM_LOOP, 10, 18f);
				}
			}
		}

		public ZombiePhase GetDancerPhase()
		{
			int dancerFrame = this.GetDancerFrame();
			if (dancerFrame <= 11)
			{
				return ZombiePhase.PHASE_DANCER_DANCING_LEFT;
			}
			if (dancerFrame <= 12)
			{
				return ZombiePhase.PHASE_DANCER_WALK_TO_RAISE;
			}
			if (dancerFrame <= 15)
			{
				return ZombiePhase.PHASE_DANCER_RAISE_RIGHT_1;
			}
			if (dancerFrame <= 18)
			{
				return ZombiePhase.PHASE_DANCER_RAISE_LEFT_1;
			}
			if (dancerFrame <= 21)
			{
				return ZombiePhase.PHASE_DANCER_RAISE_RIGHT_2;
			}
			return ZombiePhase.PHASE_DANCER_RAISE_LEFT_2;
		}

		public bool IsMovingAtChilledSpeed()
		{
			if (this.mChilledCounter > 0)
			{
				return true;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				Zombie zombie;
				if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
				{
					zombie = this;
				}
				else
				{
					zombie = this.mBoard.ZombieTryToGet(this.mRelatedZombieID);
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
					Zombie zombie2 = this.mBoard.ZombieTryToGet(zombie.mFollowerZombieID[i]);
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
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			this.PickRandomSpeed();
			if (this.mZombiePhase == ZombiePhase.PHASE_LADDER_CARRYING)
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_ladderwalk, ReanimLoopType.REANIM_LOOP, theBlendTime, 0f);
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_NEWSPAPER_MAD)
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_walk_nopaper, ReanimLoopType.REANIM_LOOP, theBlendTime, 0f);
				return;
			}
			if (this.mInPool && this.mZombieHeight != ZombieHeight.HEIGHT_IN_TO_POOL && this.mZombieHeight != ZombieHeight.HEIGHT_OUT_OF_POOL && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_swim))
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_swim, ReanimLoopType.REANIM_LOOP, theBlendTime, 0f);
				return;
			}
			if ((this.mZombieType == ZombieType.ZOMBIE_NORMAL || this.mZombieType == ZombieType.ZOMBIE_TRAFFIC_CONE || this.mZombieType == ZombieType.ZOMBIE_PAIL) && this.mBoard.mDanceMode)
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_dance, ReanimLoopType.REANIM_LOOP, theBlendTime, 0f);
				return;
			}
			int num = RandomNumbers.NextNumber(2);
			if (this.IsZombotany())
			{
				num = 0;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_FLAG)
			{
				num = 0;
			}
			if (num == 0 && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_walk2))
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_walk2, ReanimLoopType.REANIM_LOOP, theBlendTime, 0f);
				return;
			}
			if (reanimation.TrackExists(Reanimation.ReanimTrackId_anim_walk))
			{
				this.PlayZombieReanim(ref Reanimation.ReanimTrackId_anim_walk, ReanimLoopType.REANIM_LOOP, theBlendTime, 0f);
			}
		}

		public Reanimation AddAttachedReanim(int thePosX, int thePosY, ReanimationType theReanimType)
		{
			if (this.mDead)
			{
				return null;
			}
			Reanimation reanimation = this.mApp.AddReanimation((float)(this.mX + thePosX), (float)(this.mY + thePosY), 0, theReanimType);
			if (reanimation != null)
			{
				GlobalMembersAttachment.AttachReanim(ref this.mAttachmentID, reanimation, (float)thePosX * Constants.S, (float)thePosY * Constants.S);
			}
			return reanimation;
		}

		public void DragUnder()
		{
			this.mZombieHeight = ZombieHeight.HEIGHT_DRAGGED_UNDER;
			this.StopEating();
			this.ReanimReenableClipping();
		}

		public static void SetupDoorArms(Reanimation aReanim, bool theShow)
		{
			int theRenderGroup = 0;
			int theRenderGroup2 = -1;
			if (theShow)
			{
				theRenderGroup = -1;
				theRenderGroup2 = 0;
			}
			aReanim.AssignRenderGroupToPrefix("Zombie_outerarm_hand", theRenderGroup);
			aReanim.AssignRenderGroupToPrefix("Zombie_outerarm_lower", theRenderGroup);
			aReanim.AssignRenderGroupToPrefix("Zombie_outerarm_upper", theRenderGroup);
			aReanim.AssignRenderGroupToPrefix("anim_innerarm", theRenderGroup);
			aReanim.AssignRenderGroupToPrefix("Zombie_outerarm_screendoor", theRenderGroup2);
			aReanim.AssignRenderGroupToPrefix("Zombie_innerarm_screendoor", theRenderGroup2);
			aReanim.AssignRenderGroupToPrefix("Zombie_innerarm_screendoor_hand", theRenderGroup2);
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
			if (theZombieType == ZombieType.ZOMBIE_TRAFFIC_CONE)
			{
				aReanim.AssignRenderGroupToPrefix("anim_cone", 0);
				aReanim.AssignRenderGroupToPrefix("anim_hair", -1);
				return;
			}
			if (theZombieType == ZombieType.ZOMBIE_PAIL)
			{
				aReanim.AssignRenderGroupToPrefix("anim_bucket", 0);
				aReanim.AssignRenderGroupToPrefix("anim_hair", -1);
				return;
			}
			if (theZombieType == ZombieType.ZOMBIE_DOOR)
			{
				Zombie.SetupDoorArms(aReanim, true);
				return;
			}
			if (theZombieType == ZombieType.ZOMBIE_NEWSPAPER)
			{
				aReanim.AssignRenderGroupToPrefix("Zombie_paper_paper", -1);
				return;
			}
			if (theZombieType == ZombieType.ZOMBIE_FLAG)
			{
				aReanim.AssignRenderGroupToPrefix("anim_innerarm", -1);
				aReanim.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand, 0);
				aReanim.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm_screendoor, 0);
				return;
			}
			if (theZombieType == ZombieType.ZOMBIE_DUCKY_TUBE)
			{
				aReanim.AssignRenderGroupToPrefix("Zombie_duckytube", 0);
			}
		}

		public bool IsOnBoard()
		{
			if (this.mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE || this.mFromWave == GameConstants.ZOMBIE_WAVE_UI)
			{
				return false;
			}
			Debug.ASSERT(this.mBoard != null);
			return true;
		}

		public void DrawButter(Graphics g, ref ZombieDrawPosition theDrawPos)
		{
			float num = this.mPosX + theDrawPos.mImageOffsetX + (float)theDrawPos.mHeadX + 11f;
			float num2 = this.mPosY + theDrawPos.mImageOffsetY + (float)theDrawPos.mHeadY + theDrawPos.mBodyY + 21f;
			float num3 = 1f;
			if (this.mBodyReanimID != null)
			{
				this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_head1, ref num, ref num2);
			}
			num += -this.mPosX;
			num2 += -this.mPosY;
			if (this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD)
			{
				num += 6f;
				num2 -= 9f;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD)
			{
				num -= 10f;
				if (this.mInPool && this.mIsEating)
				{
					num -= 5f;
					num2 += 10f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD)
			{
				num -= 30f;
				num2 -= 30f;
				if (this.mInPool && this.mIsEating)
				{
					num2 += 10f;
				}
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD)
			{
				num2 -= 10f;
			}
			TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_REANIM_CORNPULT_BUTTER_SPLAT, num * Constants.S + 0f, num2 * Constants.S - 6f, num3, num3);
		}

		public bool IsImmobilizied()
		{
			return this.mIceTrapCounter > 0 || this.mButteredCounter > 0;
		}

		public void ApplyButter()
		{
			if (!this.mHasHead)
			{
				return;
			}
			if (!this.CanBeFrozen())
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombieType == ZombieType.ZOMBIE_BOSS || this.IsTangleKelpTarget() || this.IsBobsledTeamWithSled() || this.IsFlying())
			{
				return;
			}
			this.mButteredCounter = 400;
			Zombie zombie = this.mBoard.ZombieTryToGet(this.mRelatedZombieID);
			if (zombie != null)
			{
				zombie.mRelatedZombieID = null;
				this.mRelatedZombieID = null;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				this.mAltitude = 0f;
				if (this.mOnHighGround)
				{
					this.mAltitude += (float)Constants.HIGH_GROUND_HEIGHT;
				}
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				this.BalloonPropellerHatSpin(false);
			}
			if (this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD || this.mZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD || this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD || this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mSpecialHeadReanimID);
				if (reanimation != null)
				{
					reanimation.mAnimRate = 0f;
				}
			}
			this.UpdateAnimSpeed();
			this.StopZombieSound();
		}

		public float ZombieTargetLeadX(float theTime)
		{
			float num = this.mVelX;
			if (this.mChilledCounter > 0)
			{
				num *= GameConstants.CHILLED_SPEED_FACTOR;
			}
			if (this.IsWalkingBackwards())
			{
				num = -num;
			}
			if (this.ZombieNotWalking())
			{
				num = 0f;
			}
			float num2 = num * theTime;
			TRect zombieRect = this.GetZombieRect();
			int num3 = zombieRect.mX + zombieRect.mWidth / 2;
			return (float)num3 - num2;
		}

		public void UpdateZombieImp()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_IMP_GETTING_THROWN)
			{
				this.mVelZ -= GameConstants.THOWN_ZOMBIE_GRAVITY;
				this.mAltitude += this.mVelZ;
				this.mPosX -= this.mVelX;
				float num = this.GetPosYBasedOnRow(this.mRow) - this.mPosY;
				this.mPosY += num;
				this.mAltitude += num;
				if (this.mAltitude <= 0f)
				{
					this.mAltitude = 0f;
					this.mZombiePhase = ZombiePhase.PHASE_IMP_LANDING;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_land, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
					return;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_IMP_LANDING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
					this.StartWalkAnim(0);
				}
			}
		}

		public void SquishAllInSquare(int theX, int theY, Zombie.ZombieAttackType theAttackType)
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && theY == plant.mRow && theX == plant.mPlantCol && (theAttackType != Zombie.ZombieAttackType.ATTACKTYPE_DRIVE_OVER || !plant.IsSpiky()) && plant.mSeedType != SeedType.SEED_SPIKEROCK)
				{
					this.mBoard.mPlantsEaten++;
					plant.Squish();
				}
			}
		}

		public void RemoveIceTrap()
		{
			this.mIceTrapCounter = 0;
			if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				this.BalloonPropellerHatSpin(true);
			}
			this.UpdateAnimSpeed();
			this.StartZombieSound();
		}

		public bool IsBouncingPogo()
		{
			return this.mZombiePhase >= ZombiePhase.PHASE_POGO_BOUNCING && this.mZombiePhase <= ZombiePhase.PHASE_POGO_FORWARD_BOUNCE_7;
		}

		public int GetBobsledPosition()
		{
			if (this.mZombieType != ZombieType.ZOMBIE_BOBSLED)
			{
				return -1;
			}
			if (this.mRelatedZombieID == null && this.mFollowerZombieID[0] == null)
			{
				return -1;
			}
			if (this.mRelatedZombieID == null)
			{
				return 0;
			}
			Zombie zombie = this.mBoard.ZombieGetID(this);
			Zombie zombie2 = this.mBoard.ZombieGet(this.mRelatedZombieID);
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
			int bobsledPosition = this.GetBobsledPosition();
			bool flag = false;
			bool flag2 = false;
			Zombie zombie;
			if (this.mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
			{
				zombie = this;
			}
			else
			{
				if (bobsledPosition == -1)
				{
					return;
				}
				if (bobsledPosition == 0)
				{
					zombie = this;
				}
				else
				{
					zombie = this.mBoard.ZombieGet(this.mRelatedZombieID);
				}
			}
			if (this.mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
			{
				if (theBeforeZombie)
				{
					flag2 = true;
				}
				else
				{
					flag = true;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_BOBSLED_CRASHING)
			{
				if (bobsledPosition == 0 && !theBeforeZombie)
				{
					flag = true;
					flag2 = true;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_BOBSLED_SLIDING || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED)
			{
				if (bobsledPosition == 2 && theBeforeZombie)
				{
					flag = true;
					flag2 = true;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_BOBSLED_BOARDING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.mAnimTime < 0.5f)
				{
					if (bobsledPosition == 2 && theBeforeZombie)
					{
						flag = true;
						flag2 = true;
					}
				}
				else if (bobsledPosition == 0 && !theBeforeZombie)
				{
					flag = true;
				}
				else if (bobsledPosition == 3 && theBeforeZombie)
				{
					flag2 = true;
				}
			}
			int num = 255;
			float num2 = theDrawPos.mImageOffsetX + zombie.mPosX - this.mPosX - 76f;
			float num3 = 15f;
			int num4;
			if (this.mZombiePhase == ZombiePhase.PHASE_BOBSLED_CRASHING)
			{
				num4 = 3;
				num = TodCommon.TodAnimateCurve(30, 0, this.mPhaseCounter, 255, 0, TodCurves.CURVE_LINEAR);
				num2 += (float)(GameConstants.BOBSLED_CRASH_TIME - this.mPhaseCounter) * this.mVelX / (float)GameConstants.ZOMBIE_LIMP_SPEED_FACTOR;
				num2 -= TodCommon.TodAnimateCurveFloat(GameConstants.BOBSLED_CRASH_TIME, 0, this.mPhaseCounter, 0f, 50f, TodCurves.CURVE_EASE_OUT);
				num3 += TodCommon.TodAnimateCurveFloat(GameConstants.BOBSLED_CRASH_TIME, 75, this.mPhaseCounter, 5f, 10f, TodCurves.CURVE_LINEAR);
			}
			else
			{
				num4 = zombie.GetHelmDamageIndex();
			}
			if (num != 255)
			{
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, num));
			}
			Image theImage;
			if (num4 == 0)
			{
				theImage = AtlasResources.IMAGE_ZOMBIE_BOBSLED1;
			}
			else if (num4 == 1)
			{
				theImage = AtlasResources.IMAGE_ZOMBIE_BOBSLED2;
			}
			else if (num4 == 2)
			{
				theImage = AtlasResources.IMAGE_ZOMBIE_BOBSLED3;
			}
			else
			{
				theImage = AtlasResources.IMAGE_ZOMBIE_BOBSLED4;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED)
			{
				g.SetColorizeImages(true);
				g.SetColor(SexyColor.Black);
			}
			num2 *= Constants.S;
			num3 *= Constants.S;
			if (flag2 && num4 != 3)
			{
				g.DrawImageF(AtlasResources.IMAGE_ZOMBIE_BOBSLED_INSIDE, num2, num3);
			}
			if (flag)
			{
				g.DrawImageF(theImage, num2, num3);
			}
			if (zombie.mJustGotShotCounter > 0)
			{
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				g.SetColorizeImages(true);
				int num5 = zombie.mJustGotShotCounter * 10;
				g.SetColor(new SexyColor(num5, num5, num5, 255));
				if (flag2 && num4 != 3)
				{
					g.DrawImageF(AtlasResources.IMAGE_ZOMBIE_BOBSLED_INSIDE, num2, num3);
				}
				if (flag)
				{
					g.DrawImageF(theImage, num2, num3);
				}
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			}
			g.SetColorizeImages(false);
		}

		public void BobsledDie()
		{
			if (!this.IsBobsledTeamWithSled())
			{
				return;
			}
			if (!this.IsOnBoard())
			{
				return;
			}
			Zombie zombie;
			if (this.mRelatedZombieID == null)
			{
				zombie = this;
			}
			else
			{
				zombie = this.mBoard.ZombieGet(this.mRelatedZombieID);
			}
			if (!zombie.mDead)
			{
				zombie.DieNoLoot(true);
			}
			for (int i = 0; i < 3; i++)
			{
				Zombie zombie2 = this.mBoard.ZombieGet(zombie.mFollowerZombieID[i]);
				if (!zombie2.mDead)
				{
					zombie2.DieNoLoot(true);
				}
			}
		}

		public void BobsledBurn()
		{
			if (!this.IsBobsledTeamWithSled())
			{
				return;
			}
			Zombie zombie;
			if (this.mRelatedZombieID == null)
			{
				zombie = this;
			}
			else
			{
				zombie = this.mBoard.ZombieGet(this.mRelatedZombieID);
			}
			zombie.ApplyBurn();
			for (int i = 0; i < 3; i++)
			{
				Zombie zombie2 = this.mBoard.ZombieGet(zombie.mFollowerZombieID[i]);
				zombie2.ApplyBurn();
			}
		}

		public bool IsBobsledTeamWithSled()
		{
			return this.GetBobsledPosition() != -1;
		}

		public bool CanBeFrozen()
		{
			return this.CanBeChilled() && this.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_IN_VAULT && this.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_INTO_POOL && this.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_IN_JUMP && this.mZombiePhase != ZombiePhase.PHASE_SNORKEL_INTO_POOL && !this.IsFlying() && this.mZombiePhase != ZombiePhase.PHASE_IMP_GETTING_THROWN && this.mZombiePhase != ZombiePhase.PHASE_IMP_LANDING && this.mZombiePhase != ZombiePhase.PHASE_BOBSLED_CRASHING && this.mZombiePhase != ZombiePhase.PHASE_JACK_IN_THE_BOX_POPPING && this.mZombiePhase != ZombiePhase.PHASE_SQUASH_RISING && this.mZombiePhase != ZombiePhase.PHASE_SQUASH_FALLING && this.mZombiePhase != ZombiePhase.PHASE_SQUASH_DONE_FALLING && !this.IsBouncingPogo() && (this.mZombieType != ZombieType.ZOMBIE_BUNGEE || this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_AT_BOTTOM);
		}

		public bool CanBeChilled()
		{
			return this.mZombieType != ZombieType.ZOMBIE_ZAMBONI && !this.IsBobsledTeamWithSled() && !this.IsDeadOrDying() && this.mZombiePhase != ZombiePhase.PHASE_DIGGER_TUNNELING && this.mZombiePhase != ZombiePhase.PHASE_DIGGER_RISING && this.mZombiePhase != ZombiePhase.PHASE_DIGGER_TUNNELING_PAUSE_WITHOUT_AXE && this.mZombiePhase != ZombiePhase.PHASE_DIGGER_RISE_WITHOUT_AXE && this.mZombiePhase != ZombiePhase.PHASE_RISING_FROM_GRAVE && this.mZombiePhase != ZombiePhase.PHASE_DANCER_RISING && !this.mMindControlled && (this.mZombieType != ZombieType.ZOMBIE_BOSS || this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_IDLE_BEFORE_SPIT || this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_IDLE_AFTER_SPIT || this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_SPIT);
		}

		public void UpdateZombieSnorkel()
		{
			bool flag = this.IsWalkingBackwards();
			if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING && !flag)
			{
				if (this.mX > 770 && this.mX <= 800)
				{
					this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_INTO_POOL;
					this.mVelX = 0.2f;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_jumpinpool, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 16f);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				this.mAltitude = TodCommon.TodAnimateCurveFloat(0, 1000, (int)reanimation.mAnimTime * 1000, 0f, 10f, TodCurves.CURVE_LINEAR);
				if (reanimation.ShouldTriggerTimedEvent(0.83f))
				{
					Reanimation reanimation2 = this.mApp.AddReanimation((float)(this.mX - 47), (float)(this.mY + 73), this.mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
					reanimation2.OverrideScale(1.2f, 0.8f);
					this.mApp.AddTodParticle((float)(this.mX - 10), (float)(this.mY + 115), this.mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
					this.mApp.PlayFoley(FoleyType.FOLEY_ZOMBIE_ENTERING_WATER);
				}
				if (reanimation.mLoopCount > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL;
					this.mInPool = true;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_swim, ReanimLoopType.REANIM_LOOP_FULL_LAST_FRAME, 0, 12f);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL)
			{
				if (!this.mHasHead)
				{
					this.TakeDamage(1800, 9U);
				}
				else if (this.mX <= 140 && !flag)
				{
					this.mZombieHeight = ZombieHeight.HEIGHT_OUT_OF_POOL;
					this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_WALKING;
					this.mAltitude = -90f;
					this.mPosX -= 15f;
					this.PoolSplash(false);
					this.StartWalkAnim(0);
				}
				else if ((float)this.mX > 730f && flag)
				{
					this.mZombieHeight = ZombieHeight.HEIGHT_OUT_OF_POOL;
					this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_WALKING;
					this.mAltitude = -90f;
					this.mPosX += 15f;
					this.PoolSplash(false);
					this.StartWalkAnim(0);
				}
				else if (this.mIsEating)
				{
					this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_UP_TO_EAT;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_uptoeat, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_UP_TO_EAT)
			{
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (!this.mIsEating)
				{
					this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_DOWN_FROM_EAT;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_uptoeat, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, -24f);
				}
				else if (reanimation3.mLoopCount > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_EATING_IN_POOL;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_eat, ReanimLoopType.REANIM_LOOP, 0, 0f);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_EATING_IN_POOL)
			{
				if (!this.mIsEating)
				{
					this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_DOWN_FROM_EAT;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_uptoeat, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, -24f);
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_DOWN_FROM_EAT)
			{
				Reanimation reanimation4 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation4.mLoopCount > 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_swim, ReanimLoopType.REANIM_LOOP_FULL_LAST_FRAME, 0, 0f);
					this.PickRandomSpeed();
				}
			}
			this.mUsesClipping = (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL);
		}

		public void ReanimIgnoreClipRect(string theTrackName, bool theIgnoreClipRect)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			for (int i = 0; i < (int)reanimation.mDefinition.mTrackCount; i++)
			{
				ReanimatorTrack reanimatorTrack = reanimation.mDefinition.mTracks[i];
				ReanimatorTrackInstance reanimatorTrackInstance = reanimation.mTrackInstances[i];
				if (reanimatorTrack.mName == theTrackName)
				{
					reanimatorTrackInstance.mIgnoreClipRect = theIgnoreClipRect;
				}
			}
			this.mUsesClipping = !theIgnoreClipRect;
		}

		public void SetAnimRate(float theAnimRate)
		{
			this.mOrginalAnimRate = theAnimRate;
			this.ApplyAnimRate(theAnimRate);
		}

		public void ApplyAnimRate(float theAnimRate)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			reanimation.mAnimRate = theAnimRate;
			if (this.IsMovingAtChilledSpeed())
			{
				reanimation.mAnimRate *= 0.5f;
			}
		}

		public bool IsDeadOrDying()
		{
			return this.mDead || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_MOWERED;
		}

		public void DrawDancerReanim(Graphics g, ref ZombieDrawPosition theDrawPos)
		{
			bool flag = false;
			SexyColor aColor = default(SexyColor);
			float zombie_Dancer_Spotlight_Scale = Constants.Zombie_Dancer_Spotlight_Scale;
			float num = (float)Constants.Zombie_Dancer_Spotlight_Offset.X;
			if (this.mZombiePhase != ZombiePhase.PHASE_DANCER_DANCING_IN && this.mZombiePhase != ZombiePhase.PHASE_DANCER_SNAPPING_FINGERS && this.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_NORMAL && this.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_DYING && this.mApp.mGameScene != GameScenes.SCENE_ZOMBIES_WON)
			{
				flag = true;
				int num2 = this.mZombieAge / 100 * 7 % 5;
				if (this.mZombieAge < 700)
				{
					num2 = 0;
				}
				switch (num2)
				{
				case 0:
					aColor = new SexyColor(250, 250, 160);
					break;
				case 1:
					aColor = new SexyColor(114, 234, 170);
					break;
				case 2:
					aColor = new SexyColor(216, 126, 202);
					break;
				case 3:
					aColor = new SexyColor(90, 110, 140);
					break;
				case 4:
					aColor = new SexyColor(240, 90, 130);
					break;
				}
				g.SetColorizeImages(true);
				g.SetColor(aColor);
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_SPOTLIGHT2, num * Constants.S, (float)Constants.Zombie_Dancer_Spotlight_Offset.Y * Constants.S, zombie_Dancer_Spotlight_Scale, zombie_Dancer_Spotlight_Scale);
				g.SetColorizeImages(false);
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			reanimation.DrawRenderGroup(g, 0);
			if (flag)
			{
				g.SetColorizeImages(true);
				g.SetColor(aColor);
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_SPOTLIGHT, ((float)Constants.Zombie_Dancer_Spotlight_Pos.X + num) * Constants.S, (float)(Constants.Zombie_Dancer_Spotlight_Pos.Y + Constants.Zombie_Dancer_Spotlight_Offset.Y) * Constants.S, zombie_Dancer_Spotlight_Scale, zombie_Dancer_Spotlight_Scale);
				g.SetColorizeImages(false);
			}
		}

		public void DrawBungeeReanim(Graphics g, ref ZombieDrawPosition theDrawPos)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			float num = Constants.InvertAndScale(-22f);
			float num2 = Constants.InvertAndScale(14f) + theDrawPos.mBodyY + theDrawPos.mImageOffsetY;
			this.DrawBungeeCord(g, (int)num, (int)num2);
			reanimation.DrawRenderGroup(g, 0);
			Zombie zombie = null;
			int num3 = -1;
			if (this.mBoard != null)
			{
				num3 = this.mBoard.mZombies.IndexOf(this.mRelatedZombieID);
			}
			if (num3 != -1)
			{
				zombie = this.mBoard.mZombies[num3];
			}
			if (zombie != null)
			{
				Graphics @new = Graphics.GetNew(g);
				@new.mTransY += (int)(-this.mAltitude * Constants.S);
				@new.mTransX += (int)((zombie.mPosX - this.mPosX) * Constants.S);
				ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
				zombie.GetDrawPos(ref zombieDrawPosition);
				zombie.DrawReanim(@new, ref zombieDrawPosition, 0);
				@new.PrepareForReuse();
			}
			else
			{
				Plant plant = null;
				int num4 = -1;
				if (this.mBoard != null)
				{
					num4 = this.mBoard.mPlants.IndexOf(this.mTargetPlantID);
				}
				if (num4 != -1)
				{
					plant = this.mBoard.mPlants[num4];
				}
				if (plant != null)
				{
					Graphics new2 = Graphics.GetNew(g);
					new2.mTransY += (int)((30f - this.mAltitude) * Constants.S);
					if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_RISING && (plant.mSeedType == SeedType.SEED_SPIKEWEED || plant.mSeedType == SeedType.SEED_SPIKEROCK))
					{
						new2.mTransY -= 34;
					}
					if (plant.mPlantCol <= 4 && this.mBoard.StageHasRoof())
					{
						new2.mTransY += 10;
					}
					plant.Draw(new2);
					new2.PrepareForReuse();
				}
			}
			reanimation.DrawRenderGroup(g, GameConstants.RENDER_GROUP_ARMS);
		}

		public void DrawBungeeTarget(Graphics g)
		{
			if (!this.IsOnBoard())
			{
				return;
			}
			if (this.mApp.IsFinalBossLevel())
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_HIT_OUCHY)
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_RISING)
			{
				return;
			}
			if (this.mRelatedZombieID != null)
			{
				return;
			}
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			float num = (float)(this.mX + Constants.Zombie_Bungee_Target_Offset.X);
			float num2 = (float)(this.mY + Constants.Zombie_Bungee_Target_Offset.Y) + zombieDrawPosition.mBodyY + zombieDrawPosition.mImageOffsetY;
			if (this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_DIVING || this.mZombiePhase == ZombiePhase.PHASE_BUNGEE_DIVING_SCREAMING)
			{
				num += TodCommon.TodAnimateCurveFloat(GameConstants.BUNGEE_ZOMBIE_HEIGHT, GameConstants.BUNGEE_ZOMBIE_HEIGHT - 400, (int)this.mAltitude, 30f, 0f, TodCurves.CURVE_LINEAR);
				num2 += TodCommon.TodAnimateCurveFloat(GameConstants.BUNGEE_ZOMBIE_HEIGHT, GameConstants.BUNGEE_ZOMBIE_HEIGHT - 400, (int)this.mAltitude, -600f, 0f, TodCurves.CURVE_LINEAR);
			}
			num2 += this.mAltitude;
			g.DrawImageF(AtlasResources.IMAGE_BUNGEETARGET, num * Constants.S, num2 * Constants.S);
		}

		public void BungeeDie()
		{
			this.BungeeDropPlant();
			Plant plant = null;
			int num = -1;
			if (this.mBoard != null)
			{
				num = this.mBoard.mPlants.IndexOf(this.mTargetPlantID);
			}
			if (num != -1)
			{
				plant = this.mBoard.mPlants[num];
			}
			if (plant != null)
			{
				this.mBoard.mPlantsEaten++;
				plant.Die();
			}
			Zombie zombie = null;
			if (this.mBoard != null)
			{
				zombie = this.mBoard.ZombieTryToGet(this.mRelatedZombieID);
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
				this.mApp.AddTodParticle(this.mPosX + 80f, this.mPosY + 60f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_ZAMBONI_EXPLOSION);
				this.DieWithLoot();
				this.mApp.PlayFoley(FoleyType.FOLEY_EXPLOSION);
				return;
			}
			this.mFlatTires = true;
			this.mApp.PlayFoley(FoleyType.FOLEY_TIRE_POP);
			this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_DYING;
			this.mApp.AddTodParticle(this.mPosX + 29f, this.mPosY + 114f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_ZAMBONI_TIRE);
			this.mVelX = 0f;
			if (RandomNumbers.NextNumber(4) == 0 && this.mPosX < 600f + (float)Constants.BOARD_EXTRA_ROOM)
			{
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_wheelie2, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 10f);
				this.mPhaseCounter = 280;
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			TodParticleSystem todParticleSystem = this.mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.PARTICLE_ZAMBONI_SMOKE);
			if (todParticleSystem != null)
			{
				reanimation.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_zombie_zamboni_1, ref todParticleSystem, 35f, 85f);
			}
			this.mPhaseCounter = 280;
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_wheelie1, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 12f);
		}

		public void CatapultDeath(uint theDamageFlags)
		{
			if (TodCommon.TestBit(theDamageFlags, 5))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_TIRE_POP);
				this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_DYING;
				this.mApp.AddTodParticle(this.mPosX + 29f, this.mPosY + 114f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_ZAMBONI_TIRE);
				this.mVelX = 0f;
				this.AddAttachedParticle(47, 77, ParticleEffect.PARTICLE_ZAMBONI_SMOKE);
				this.mPhaseCounter = 280;
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_bounce, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 12f);
				return;
			}
			this.mApp.AddTodParticle(this.mPosX + 80f, this.mPosY + 60f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_CATAPULT_EXPLOSION);
			this.DieWithLoot();
			this.mApp.PlayFoley(FoleyType.FOLEY_EXPLOSION);
		}

		public bool SetupDrawZombieWon(Graphics g)
		{
			if (this.mFromWave != GameConstants.ZOMBIE_WAVE_WINNER)
			{
				return true;
			}
			if (!this.mBoard.mCutScene.ShowZombieWalking())
			{
				return false;
			}
			if (this.mBoard.mBackground == BackgroundType.BACKGROUND_1_DAY || this.mBoard.mBackground == BackgroundType.BACKGROUND_2_NIGHT)
			{
				g.ClipRect((int)((float)(-123 - this.mX) * Constants.S) + Constants.Zombie_GameOver_ClipOffset_1, (int)((float)(-(float)this.mY) * Constants.S), 800, 600);
			}
			else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_3_POOL || this.mBoard.mBackground == BackgroundType.BACKGROUND_4_FOG)
			{
				g.ClipRect((int)((float)(-172 - this.mX) * Constants.S) + Constants.Zombie_GameOver_ClipOffset_2, (int)((float)(-(float)this.mY) * Constants.S), 800, 600);
			}
			else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_5_ROOF || this.mBoard.mBackground == BackgroundType.BACKGROUND_6_BOSS)
			{
				g.ClipRect((int)((float)(-95 - this.mX) * Constants.S) + Constants.Zombie_GameOver_ClipOffset_3, (int)((float)(-(float)this.mY) * Constants.S), 800, 600);
			}
			return true;
		}

		public void WalkIntoHouse()
		{
			Zombie.WinningZombieReachedDesiredY = false;
			GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_ZAMBONI_SMOKE, null);
			this.mFromWave = GameConstants.ZOMBIE_WAVE_WINNER;
			if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT)
			{
				this.mZombiePhase = ZombiePhase.PHASE_POLEVAULTER_POST_VAULT;
				this.StartWalkAnim(0);
			}
			if (this.mBoard.mBackground == BackgroundType.BACKGROUND_1_DAY || this.mBoard.mBackground == BackgroundType.BACKGROUND_2_NIGHT || this.mBoard.mBackground == BackgroundType.BACKGROUND_3_POOL || this.mBoard.mBackground == BackgroundType.BACKGROUND_4_FOG || this.mBoard.mBackground == BackgroundType.BACKGROUND_5_ROOF || this.mBoard.mBackground == BackgroundType.BACKGROUND_6_BOSS)
			{
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ZOMBIE, 2, 100);
				for (int i = 0; i < this.mBoard.mLawnMowers.Count; i++)
				{
					this.mBoard.mLawnMowers[i].PrepareForReuse();
				}
				this.mBoard.mLawnMowers.Clear();
				if (this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT)
				{
					this.mPosX += 35f;
				}
				if (this.mBoard.mBackground == BackgroundType.BACKGROUND_3_POOL || this.mBoard.mBackground == BackgroundType.BACKGROUND_4_FOG)
				{
					ZombieType zombieType = this.mZombieType;
				}
			}
		}

		public void UpdateZamboni()
		{
			if (this.mPosX > 400f && !this.mFlatTires)
			{
				this.mVelX = TodCommon.TodAnimateCurveFloat(700, 300, (int)this.mPosX, 0.25f, 0.05f, TodCurves.CURVE_LINEAR);
			}
			else if (this.mFlatTires && this.mVelX > 0.0005f)
			{
				this.mVelX -= 0.0005f;
			}
			int num = (int)this.mPosX + 118;
			if (this.mBoard.StageHasRoof())
			{
				num = Math.Max(num, 500);
			}
			else
			{
				num = Math.Max(num, 25);
			}
			if (num < this.mBoard.mIceMinX[this.mRow])
			{
				this.mBoard.mIceMinX[this.mRow] = num;
			}
			if (num < 860)
			{
				this.mBoard.mIceTimer[this.mRow] = 3000;
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA)
				{
					this.mBoard.mIceTimer[this.mRow] = int.MaxValue;
				}
			}
		}

		public void UpdateZombieChimney()
		{
		}

		public void UpdateLadder()
		{
			if (this.mMindControlled || !this.mHasHead || this.IsDeadOrDying())
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_LADDER_CARRYING && this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIE_NORMAL)
			{
				Plant plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_LADDER);
				if (plant != null)
				{
					this.StopEating();
					this.mZombiePhase = ZombiePhase.PHASE_LADDER_PLACING;
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_placeladder, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 24f);
					return;
				}
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_LADDER_PLACING)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					Plant plant2 = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_LADDER);
					if (plant2 != null)
					{
						this.mBoard.AddALadder(plant2.mPlantCol, plant2.mRow);
						this.mApp.PlaySample(Resources.SOUND_LADDER_ZOMBIE);
						this.mZombieHeight = ZombieHeight.HEIGHT_UP_LADDER;
						this.mUseLadderCol = plant2.mPlantCol;
						this.DetachShield();
						return;
					}
					this.mZombiePhase = ZombiePhase.PHASE_LADDER_CARRYING;
					this.StartWalkAnim(0);
				}
			}
		}

		private void SetupReanimForLostArm(uint theDamageFlags)
		{
			if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
			{
				this.ReanimShowPrefix("Zombie_football_leftarm_lower", -1);
				this.ReanimShowPrefix("Zombie_football_leftarm_hand", -1);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER)
			{
				this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_paper_hands, -1);
				this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_paper_leftarm_lower, -1);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_polevaulter_outerarm_lower, -1);
				this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, -1);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
			{
				this.ReanimShowPrefix("Zombie_disco_outerarm_lower", -1);
				this.ReanimShowPrefix("Zombie_disco_outerhand_point", -1);
				this.ReanimShowPrefix("Zombie_disco_outerhand", -1);
				this.ReanimShowPrefix("Zombie_disco_outerarm_upper", -1);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				this.ReanimShowPrefix("Zombie_disco_outerarm_lower", -1);
				this.ReanimShowPrefix("Zombie_disco_outerhand", -1);
			}
			else
			{
				this.ReanimShowPrefix("Zombie_outerarm_lower", -1);
				this.ReanimShowPrefix("Zombie_outerarm_hand", -1);
			}
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			float num = this.mPosX + zombieDrawPosition.mImageOffsetX + 45f;
			float num2 = this.mPosY + zombieDrawPosition.mImageOffsetY + zombieDrawPosition.mBodyY + 78f;
			if (this.IsWalkingBackwards())
			{
				num += 36f;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation != null)
			{
				if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_football_leftarm_hand, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_football_leftarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_LEFTARM_UPPER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_paper_leftarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_paper_leftarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_LEFTARM_UPPER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_polevaulter_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_polevaulter_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_POLEVAULTER_OUTERARM_UPPER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_BALLOON_OUTERARM_UPPER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_IMP)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_imp_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_IMP_ARM1_BONE);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_digger_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_OUTERARM_UPPER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_dolphinrider_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_BOBSLED_OUTERARM_UPPER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_JACK_IN_THE_BOX)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_outerarm_lower, AtlasResources.IMAGE_REANIM_ZOMBIE_JACKBOX_OUTERARM_LOWER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_snorkle_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_SNORKLE_OUTERARM_UPPER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_dolphinrider_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_DOLPHINRIDER_OUTERARM_UPPER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_POGO)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_OUTERARM_UPPER2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_pogo_stickhands, AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_STICKHANDS2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_pogo_stick, AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_STICKDAMAGE2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_pogo_stick2, AtlasResources.IMAGE_REANIM_ZOMBIE_POGO_STICK2DAMAGE2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_FLAG)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_OUTERARM_UPPER2);
					Reanimation reanimation2 = this.mApp.ReanimationTryToGet(this.mSpecialHeadReanimID);
					if (reanimation2 != null)
					{
						reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_flag, AtlasResources.IMAGE_REANIM_ZOMBIE_FLAG3);
					}
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_disco_outerhand, ref num, ref num2);
					this.ReanimShowPrefix("Zombie_disco_outerarm_upper_bone", 0);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_disco_outerhand, ref num, ref num2);
					this.ReanimShowPrefix("Zombie_disco_outerarm_upper_bone", 0);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_LADDER)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_ladder_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_OUTERARM_UPPER2);
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_YETI)
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_hand, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_yeti_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_YETI_OUTERARM_UPPER2);
				}
				else
				{
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_lower, ref num, ref num2);
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_zombie_outerarm_upper, AtlasResources.IMAGE_REANIM_ZOMBIE_OUTERARM_UPPER2);
				}
			}
			if (!this.mInPool && !TodCommon.TestBit(theDamageFlags, 4))
			{
				ParticleEffect theEffect = ParticleEffect.PARTICLE_ZOMBIE_ARM;
				if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_MOWERED)
				{
					theEffect = ParticleEffect.PARTICLE_MOWERED_ZOMBIE_ARM;
				}
				if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_MOWERED)
				{
					num -= 40f;
					num2 -= 50f;
				}
				TodParticleSystem todParticleSystem = this.mApp.AddTodParticle(num, num2, this.mRenderOrder + 1, theEffect);
				this.OverrideParticleColor(todParticleSystem);
				this.OverrideParticleScale(todParticleSystem);
				if (todParticleSystem != null)
				{
					if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
					{
						todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_LEFTARM_HAND);
						return;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER)
					{
						todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_PAPER_LEFTARM_LOWER);
						return;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
					{
						this.ReanimShowPrefix("Zombie_disco_outerarm_lower", -1);
						this.ReanimShowPrefix("Zombie_disco_outerhand_point", -1);
						this.ReanimShowPrefix("Zombie_disco_outerhand", -1);
						this.ReanimShowPrefix("Zombie_disco_outerarm_upper", -1);
						return;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
					{
						this.ReanimShowPrefix("Zombie_disco_outerarm_lower", -1);
						this.ReanimShowPrefix("Zombie_disco_outerhand", -1);
						return;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
					{
						todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_BOBSLED_OUTERARM_HAND);
						return;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_IMP)
					{
						todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_IMP_ARM2);
						return;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_YETI)
					{
						todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_YETI_OUTERARM_HAND);
						return;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_JACK_IN_THE_BOX)
					{
						todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEJACKBOXARM);
						return;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
					{
						todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIEDIGGERARM);
						return;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER || this.mZombieType == ZombieType.ZOMBIE_BALLOON || this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER || this.mZombieType == ZombieType.ZOMBIE_POGO || this.mZombieType == ZombieType.ZOMBIE_LADDER)
					{
						todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_REANIM_ZOMBIE_OUTERARM_HAND);
					}
				}
			}
		}

		public void DropArm(uint theDamageFlags)
		{
			if (!this.CanLoseBodyParts())
			{
				return;
			}
			if (this.mShieldType == ShieldType.SHIELDTYPE_DOOR || this.mShieldType == ShieldType.SHIELDTYPE_NEWSPAPER)
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_RIDING || this.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_IN_JUMP || this.mZombiePhase == ZombiePhase.PHASE_NEWSPAPER_READING)
			{
				return;
			}
			if (!this.mHasArm)
			{
				return;
			}
			this.mHasArm = false;
			this.SetupReanimForLostArm(theDamageFlags);
			this.mApp.PlayFoley(FoleyType.FOLEY_LIMBS_POP);
		}

		public bool CanLoseBodyParts()
		{
			return this.mZombieType != ZombieType.ZOMBIE_ZAMBONI && this.mZombieType != ZombieType.ZOMBIE_BUNGEE && this.mZombieType != ZombieType.ZOMBIE_CATAPULT && this.mZombieType != ZombieType.ZOMBIE_GARGANTUAR && this.mZombieType != ZombieType.ZOMBIE_REDEYE_GARGANTUAR && this.mZombieType != ZombieType.ZOMBIE_BOSS && this.mZombieHeight != ZombieHeight.HEIGHT_ZOMBIQUARIUM && !this.IsFlying() && !this.IsBobsledTeamWithSled();
		}

		public void DropHelm(uint theDamageFlags)
		{
			if (this.mHelmType == HelmType.HELMTYPE_NONE)
			{
				return;
			}
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			float theX = this.mPosX + zombieDrawPosition.mImageOffsetX + (float)zombieDrawPosition.mHeadX + 14f;
			float theY = this.mPosY + zombieDrawPosition.mImageOffsetY + (float)zombieDrawPosition.mHeadY + zombieDrawPosition.mBodyY + 18f;
			ParticleEffect particleEffect = ParticleEffect.PARTICLE_NONE;
			if (this.mHelmType == HelmType.HELMTYPE_TRAFFIC_CONE)
			{
				this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_cone, ref theX, ref theY);
				this.ReanimShowPrefix("anim_cone", -1);
				this.ReanimShowPrefix("anim_hair", 0);
				particleEffect = ParticleEffect.PARTICLE_ZOMBIE_TRAFFIC_CONE;
			}
			else if (this.mHelmType == HelmType.HELMTYPE_PAIL)
			{
				this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_bucket, ref theX, ref theY);
				this.ReanimShowPrefix("anim_bucket", -1);
				this.ReanimShowPrefix("anim_hair", 0);
				particleEffect = ParticleEffect.PARTICLE_ZOMBIE_PAIL;
			}
			else if (this.mHelmType == HelmType.HELMTYPE_FOOTBALL)
			{
				this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_football_helmet, ref theX, ref theY);
				this.ReanimShowPrefix("zombie_football_helmet", -1);
				this.ReanimShowPrefix("anim_hair", 0);
				particleEffect = ParticleEffect.PARTICLE_ZOMBIE_HELMET;
			}
			else if (this.mHelmType == HelmType.HELMTYPE_DIGGER)
			{
				this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_hardhat, ref theX, ref theY);
				this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_hardhat, -1);
				particleEffect = ParticleEffect.PARTICLE_ZOMBIE_HEADLIGHT;
			}
			else if (this.mHelmType == HelmType.HELMTYPE_BOBSLED && !TodCommon.TestBit(theDamageFlags, 4))
			{
				this.BobsledCrash();
			}
			if (!TodCommon.TestBit(theDamageFlags, 4) && particleEffect != ParticleEffect.PARTICLE_NONE)
			{
				TodParticleSystem aParticle = this.mApp.AddTodParticle(theX, theY, this.mRenderOrder + 1, particleEffect);
				this.OverrideParticleScale(aParticle);
			}
			this.mHasHelm = false;
			this.mHelmType = HelmType.HELMTYPE_NONE;
		}

		public void DropShield(uint theDamageFlags)
		{
			if (this.mShieldType == ShieldType.SHIELDTYPE_NONE)
			{
				return;
			}
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			int aRenderOrder = this.mRenderOrder + 1;
			TodParticleSystem aParticle = null;
			if (this.mShieldType == ShieldType.SHIELDTYPE_DOOR)
			{
				this.DetachShield();
				if (!TodCommon.TestBit(theDamageFlags, 4))
				{
					float theX = 0f;
					float theY = 0f;
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_screendoor, ref theX, ref theY);
					aParticle = this.mApp.AddTodParticle(theX, theY, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_DOOR);
				}
			}
			else if (this.mShieldType == ShieldType.SHIELDTYPE_NEWSPAPER)
			{
				this.StopEating();
				if (this.mYuckyFace)
				{
					this.ShowYuckyFace(false);
					this.mYuckyFace = false;
					this.mYuckyFaceCounter = 0;
				}
				this.mZombiePhase = ZombiePhase.PHASE_NEWSPAPER_MADDENING;
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_gasp, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 8f);
				this.DetachShield();
				if (!TodCommon.TestBit(theDamageFlags, 4))
				{
					float theX2 = 0f;
					float theY2 = 0f;
					this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_paper_paper, ref theX2, ref theY2);
					aParticle = this.mApp.AddTodParticle(theX2, theY2, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_NEWSPAPER);
				}
				if (!TodCommon.TestBit(theDamageFlags, 4) && !TodCommon.TestBit(theDamageFlags, 0))
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_NEWSPAPER_RIP);
					this.AddAttachedReanim(-11, 0, ReanimationType.REANIM_ZOMBIE_SURPRISE);
					this.mSurprised = true;
				}
			}
			else if (this.mShieldType == ShieldType.SHIELDTYPE_LADDER)
			{
				this.DetachShield();
				if (!TodCommon.TestBit(theDamageFlags, 4))
				{
					float theX3 = this.mPosX + 31f;
					float theY3 = this.mPosY + 80f;
					aParticle = this.mApp.AddTodParticle(theX3, theY3, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_LADDER);
				}
			}
			this.OverrideParticleScale(aParticle);
			this.mHasShield = false;
			this.mShieldType = ShieldType.SHIELDTYPE_NONE;
		}

		public void ReanimReenableClipping()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			for (int i = 0; i < (int)reanimation.mDefinition.mTrackCount; i++)
			{
				ReanimatorTrackInstance reanimatorTrackInstance = reanimation.mTrackInstances[i];
				reanimatorTrackInstance.mIgnoreClipRect = false;
			}
			this.mUsesClipping = true;
		}

		public void UpdateBoss()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (this.mApp.mGameScene == GameScenes.SCENE_LEVEL_INTRO)
			{
				if (reanimation.ShouldTriggerTimedEvent(0.24f) || reanimation.ShouldTriggerTimedEvent(0.79f))
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_THUMP);
					this.mBoard.ShakeBoard(1, 4);
					this.mApp.Vibrate();
				}
				return;
			}
			Reanimation reanimation2 = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
			this.UpdateBossFireball();
			if (this.mIceTrapCounter == 0)
			{
				if (this.mSummonCounter > 0)
				{
					this.mSummonCounter -= 3;
				}
				if (this.mBossBungeeCounter > 0)
				{
					this.mBossBungeeCounter -= 3;
				}
				if (this.mBossStompCounter > 0)
				{
					this.mBossStompCounter -= 3;
				}
				if (this.mBossHeadCounter > 0)
				{
					this.mBossHeadCounter -= 3;
				}
				if (this.mChilledCounter > 0)
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
			if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_ENTER)
			{
				this.BossPlayIdle();
				return;
			}
			if (this.mZombiePhase != ZombiePhase.PHASE_BOSS_IDLE)
			{
				if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_SPAWNING)
				{
					if (reanimation.ShouldTriggerTimedEvent(0.6f))
					{
						this.BossSpawnContact();
					}
					if (reanimation.mLoopCount > 0)
					{
						this.BossPlayIdle();
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_STOMPING)
				{
					float theEventTime = 0.5f;
					if (this.mTargetRow >= 2)
					{
						theEventTime = 0.55f;
					}
					if (reanimation.ShouldTriggerTimedEvent(theEventTime))
					{
						this.BossStompContact();
					}
					if (reanimation.mLoopCount > 0)
					{
						this.BossPlayIdle();
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_BUNGEES_ENTER)
				{
					if (reanimation.ShouldTriggerTimedEvent(0.4f))
					{
						this.BossBungeeSpawn();
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_BUNGEES_DROP)
				{
					if (this.BossAreBungeesDone())
					{
						this.BossBungeeLeave();
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_BUNGEES_LEAVE)
				{
					if (reanimation.mLoopCount > 0)
					{
						this.BossPlayIdle();
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_DROP_RV)
				{
					if (reanimation.ShouldTriggerTimedEvent(0.65f))
					{
						this.BossRVLanding();
					}
					if (reanimation.mLoopCount > 0)
					{
						this.BossPlayIdle();
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_ENTER)
				{
					if (this.GetBodyDamageIndex() == 2 && reanimation.ShouldTriggerTimedEvent(0.37f))
					{
						this.ApplyBossSmokeParticles(true);
					}
					if (reanimation.ShouldTriggerTimedEvent(0.55f))
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_HYDRAULIC);
					}
					if (reanimation.mLoopCount > 0)
					{
						this.mZombiePhase = ZombiePhase.PHASE_BOSS_HEAD_IDLE_BEFORE_SPIT;
						this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.REANIM_LOOP, 0, 12f);
						this.mPhaseCounter = 500;
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_IDLE_BEFORE_SPIT)
				{
					if (this.mBodyHealth == 1)
					{
						this.BossStartDeath();
						return;
					}
					if (this.mPhaseCounter <= 0)
					{
						this.BossHeadSpit();
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_SPIT)
				{
					if (reanimation.ShouldTriggerTimedEvent(0.37f))
					{
						this.BossHeadSpitEffect();
					}
					if (reanimation.ShouldTriggerTimedEvent(0.42f))
					{
						this.BossHeadSpitContact();
					}
					if (reanimation.mLoopCount > 0)
					{
						reanimation2 = this.mApp.ReanimationTryToGet(this.mSpecialHeadReanimID);
						reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 20, 18f);
						this.mZombiePhase = ZombiePhase.PHASE_BOSS_HEAD_IDLE_AFTER_SPIT;
						this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.REANIM_LOOP, 0, 12f);
						this.mPhaseCounter = 300;
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_IDLE_AFTER_SPIT)
				{
					if (this.mBodyHealth == 1)
					{
						this.BossStartDeath();
						return;
					}
					if (this.mPhaseCounter <= 0)
					{
						this.mZombiePhase = ZombiePhase.PHASE_BOSS_HEAD_LEAVE;
						this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_leave, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 12f);
						return;
					}
				}
				else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_HEAD_LEAVE)
				{
					if (reanimation.ShouldTriggerTimedEvent(0.23f))
					{
						this.mChilledCounter = 0;
						this.UpdateAnimSpeed();
					}
					if (reanimation.ShouldTriggerTimedEvent(0.48f) || reanimation.ShouldTriggerTimedEvent(0.8f))
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_THUMP);
					}
					if (reanimation.mLoopCount > 0)
					{
						this.ApplyBossSmokeParticles(false);
						this.BossPlayIdle();
						return;
					}
				}
				else
				{
					Debug.ASSERT(false);
				}
				return;
			}
			if (this.mBodyHealth == 1)
			{
				this.PlayDeathAnim(0U);
				return;
			}
			if (this.mPhaseCounter > 0)
			{
				return;
			}
			int bodyDamageIndex = this.GetBodyDamageIndex();
			if (bodyDamageIndex != this.mBossMode)
			{
				this.mBossMode = bodyDamageIndex;
				if (this.mBossMode == 1)
				{
					this.BossBungeeAttack();
					return;
				}
				this.BossRVAttack();
				return;
			}
			else
			{
				if (this.mBossStompCounter == 0)
				{
					this.BossStompAttack();
					return;
				}
				if (this.mBossBungeeCounter == 0)
				{
					int ceiling;
					if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
					{
						ceiling = 4;
					}
					else
					{
						ceiling = 2;
					}
					if (RandomNumbers.NextNumber(ceiling) == 0)
					{
						this.mBossBungeeCounter = TodCommon.RandRangeInt(4000, 5000);
						this.BossRVAttack();
						return;
					}
					this.BossBungeeAttack();
					return;
				}
				else
				{
					if (this.mBossHeadCounter <= 0)
					{
						this.BossHeadAttack();
						return;
					}
					if (this.mSummonCounter <= 0)
					{
						this.BossSpawnAttack();
						return;
					}
					this.mPhaseCounter = TodCommon.RandRangeInt(100, 200);
					return;
				}
			}
		}

		public void BossPlayIdle()
		{
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_IDLE;
			this.mPhaseCounter = TodCommon.RandRangeInt(100, 200);
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 6f);
		}

		public void BossRVLanding()
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant.mRow >= this.mTargetRow && plant.mRow <= this.mTargetRow + 1 && plant.mPlantCol >= this.mTargetCol && plant.mPlantCol <= this.mTargetCol + 2)
				{
					plant.Squish();
				}
			}
			this.mBoard.ShakeBoard(1, 2);
			this.mApp.PlaySample(Resources.SOUND_RVTHROW);
			this.mApp.Vibrate();
			this.mSummonCounter = 500;
			this.mBossHeadCounter = 5000;
			if (this.mBossMode >= 1)
			{
				this.mBossStompCounter = 4000;
			}
			if (this.mBossMode >= 2)
			{
				this.mBossBungeeCounter = 6500;
			}
		}

		public void BossStompContact()
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant.mRow >= this.mTargetRow && plant.mRow <= this.mTargetRow + 1 && plant.mPlantCol >= 5)
				{
					plant.Squish();
				}
			}
			this.mBoard.ShakeBoard(1, 4);
			this.mApp.PlayFoley(FoleyType.FOLEY_THUMP);
			this.mApp.Vibrate();
		}

		public bool BossAreBungeesDone()
		{
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				Zombie zombie = this.mBoard.ZombieTryToGet(this.mFollowerZombieID[i]);
				if (zombie != null)
				{
					if (zombie.mZombiePhase == ZombiePhase.PHASE_BUNGEE_RISING)
					{
						return true;
					}
					num++;
				}
			}
			return num == 0;
		}

		public void BossBungeeSpawn()
		{
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_BUNGEES_DROP;
			for (int i = 0; i < 3; i++)
			{
				Zombie zombie = this.mBoard.AddZombieInRow(ZombieType.ZOMBIE_BUNGEE, 0, 0);
				zombie.PickBungeeZombieTarget(this.mTargetCol + i);
				zombie.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, zombie.mRow, 7);
				zombie.mAltitude = zombie.mPosY - 30f;
				this.mFollowerZombieID[i] = this.mBoard.ZombieGetID(zombie);
			}
		}

		public void BossSpawnAttack()
		{
			this.RemoveColdEffects();
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_SPAWNING;
			if (this.mBossMode == 0)
			{
				this.mSummonCounter = TodCommon.RandRangeInt(450, 550);
			}
			else if (this.mBossMode == 1)
			{
				this.mSummonCounter = TodCommon.RandRangeInt(350, 450);
			}
			else if (this.mBossMode == 2)
			{
				this.mSummonCounter = TodCommon.RandRangeInt(150, 250);
			}
			this.mTargetRow = this.mBoard.PickRowForNewZombie(ZombieType.ZOMBIE_NORMAL);
			string text = string.Empty;
			switch (this.mTargetRow)
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
			this.PlayZombieReanim(ref text, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			this.mApp.PlayFoley(FoleyType.FOLEY_HYDRAULIC_SHORT);
		}

		public void BossBungeeAttack()
		{
			this.RemoveColdEffects();
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_BUNGEES_ENTER;
			this.mBossBungeeCounter = TodCommon.RandRangeInt(4000, 5000);
			this.mTargetCol = TodCommon.RandRangeInt(0, 2);
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_bungee_1_enter, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			this.mApp.PlayFoley(FoleyType.FOLEY_HYDRAULIC_SHORT);
			this.mApp.PlayFoley(FoleyType.FOLEY_BUNGEE_SCREAM);
		}

		public void BossRVAttack()
		{
			this.RemoveColdEffects();
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_DROP_RV;
			this.mTargetRow = TodCommon.RandRangeInt(0, 3);
			this.mTargetCol = TodCommon.RandRangeInt(0, 2);
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_rv_1, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 16f);
			this.mApp.PlayFoley(FoleyType.FOLEY_HYDRAULIC_SHORT);
		}

		public void BossSpawnContact()
		{
			ZombieType theZombieType;
			if (this.mZombieAge < 3500)
			{
				theZombieType = ZombieType.ZOMBIE_NORMAL;
			}
			else if (this.mZombieAge < 8000)
			{
				theZombieType = ZombieType.ZOMBIE_TRAFFIC_CONE;
			}
			else if (this.mZombieAge < 12500)
			{
				theZombieType = ZombieType.ZOMBIE_PAIL;
			}
			else
			{
				int num = GameConstants.gBossZombieList.Length;
				if (this.mTargetRow == 0)
				{
					Debug.ASSERT(GameConstants.gBossZombieList[num - 1] == ZombieType.ZOMBIE_GARGANTUAR);
					num--;
				}
				theZombieType = GameConstants.gBossZombieList[RandomNumbers.NextNumber(num)];
			}
			Zombie zombie = this.mBoard.AddZombieInRow(theZombieType, this.mTargetRow, 0);
			zombie.mPosX = 600f + this.mPosX;
		}

		public void BossBungeeLeave()
		{
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_BUNGEES_LEAVE;
			for (int i = 0; i < 3; i++)
			{
				Zombie zombie = this.mBoard.ZombieTryToGet(this.mFollowerZombieID[i]);
				if (zombie != null && zombie.mButteredCounter > 0)
				{
					zombie.DieWithLoot();
				}
			}
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_bungee_1_leave, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 18f);
		}

		public void BossStompAttack()
		{
			this.RemoveColdEffects();
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_STOMPING;
			this.mBossStompCounter = TodCommon.RandRangeInt(5500, 6500);
			int num = 0;
			int[] array = new int[4];
			for (int i = 0; i < 4; i++)
			{
				if (this.BossCanStompRow(i))
				{
					array[num] = i;
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			this.mTargetRow = TodCommon.TodPickFromArray(array, num);
			string text = string.Empty;
			switch (this.mTargetRow)
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
			this.PlayZombieReanim(ref text, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			this.mApp.PlayFoley(FoleyType.FOLEY_HYDRAULIC_SHORT);
		}

		public bool BossCanStompRow(int theRow)
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && !plant.NotOnGround() && plant.mRow >= theRow && plant.mRow <= theRow + 1 && plant.mPlantCol >= 5)
				{
					return true;
				}
			}
			return false;
		}

		public void BossDie()
		{
			if (!this.IsOnBoard())
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBossFireBallReanimID);
			if (reanimation != null)
			{
				reanimation.ReanimationDie();
				this.mBossFireBallReanimID = null;
				this.BossDestroyIceballInRow(this.mFireballRow);
				this.BossDestroyFireball();
			}
			this.mApp.mMusic.FadeOut(200);
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && zombie != this && !zombie.IsDeadOrDying())
				{
					zombie.DieWithLoot();
				}
			}
			this.RemoveColdEffects();
		}

		public void BossHeadAttack()
		{
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_HEAD_ENTER;
			this.mBossHeadCounter = TodCommon.RandRangeInt(4000, 5000);
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_enter, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			this.mApp.PlayFoley(FoleyType.FOLEY_HYDRAULIC_SHORT);
		}

		public void BossHeadSpitContact()
		{
			Debug.ASSERT(this.mApp.ReanimationTryToGet(this.mBossFireBallReanimID) == null);
			float num = 550f + this.mPosX;
			float theY = this.mBoard.GetPosYBasedOnRow(num, this.mFireballRow) + GameConstants.BOSS_BALL_OFFSET_Y;
			int aRenderOrder = this.mRenderOrder + 1;
			Reanimation reanimation;
			if (this.mIsFireBall)
			{
				num -= 95f;
				reanimation = this.mApp.AddReanimation(num, theY, aRenderOrder, ReanimationType.REANIM_BOSS_FIREBALL);
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_form, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 16f);
				reanimation.mIsAttachment = true;
				reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_additive, GameConstants.RENDER_GROUP_BOSS_FIREBALL_ADDITIVE);
				reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_superglow, GameConstants.RENDER_GROUP_BOSS_FIREBALL_ADDITIVE);
			}
			else
			{
				num -= 95f;
				reanimation = this.mApp.AddReanimation(num, theY, aRenderOrder, ReanimationType.REANIM_BOSS_ICEBALL);
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_form, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 16f);
				reanimation.mIsAttachment = true;
				reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_ice_highlight, GameConstants.RENDER_GROUP_BOSS_FIREBALL_ADDITIVE);
			}
			this.mBossFireBallReanimID = this.mApp.ReanimationGetID(reanimation);
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(this.mSpecialHeadReanimID);
			reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_laugh, ReanimLoopType.REANIM_LOOP, 20, 18f);
			this.mApp.PlayFoley(FoleyType.FOLEY_HYDRAULIC_SHORT);
		}

		public void BossHeadSpit()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBossFireBallReanimID);
			if (reanimation != null)
			{
				reanimation.ReanimationDie();
				this.mBossFireBallReanimID = null;
			}
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_HEAD_SPIT;
			this.mFireballRow = TodCommon.RandRangeInt(0, 4);
			this.mIsFireBall = (TodCommon.RandRangeInt(0, 1) == 0);
			string text = string.Empty;
			switch (this.mFireballRow)
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
			this.PlayZombieReanim(ref text, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
			Image theImage = null;
			if (this.mIsFireBall)
			{
				reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_eyeglow_red, theImage);
				reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_mouthglow_red, theImage);
			}
			else
			{
				reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_eyeglow_red, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_EYEGLOW_BLUE);
				reanimation2.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_boss_mouthglow_red, AtlasResources.IMAGE_REANIM_ZOMBIE_BOSS_MOUTHGLOW_BLUE);
			}
			Reanimation reanimation3 = this.mApp.ReanimationTryToGet(this.mSpecialHeadReanimID);
			reanimation3.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_drive, ReanimLoopType.REANIM_LOOP, 20, 36f);
		}

		public void UpdateBossFireball()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBossFireBallReanimID);
			if (reanimation == null)
			{
				return;
			}
			float trackVelocity = reanimation.GetTrackVelocity(Reanimation.ReanimTrackId__ground);
			float num = reanimation.mOverlayMatrix.mMatrix.M41 * Constants.IS;
			num -= trackVelocity;
			reanimation.mOverlayMatrix.mMatrix.M41 = num * Constants.S;
			float num2 = this.mBoard.GetPosYBasedOnRow(num + 75f, this.mFireballRow) + GameConstants.BOSS_BALL_OFFSET_Y;
			reanimation.mOverlayMatrix.mMatrix.M42 = num2 * Constants.S;
			if (num < -180f + (float)Constants.BOARD_EXTRA_ROOM)
			{
				reanimation.ReanimationDie();
				this.mBossFireBallReanimID = null;
			}
			int theX = this.mBoard.PixelToGridX((int)num + 75, (int)num2);
			this.SquishAllInSquare(theX, this.mFireballRow, Zombie.ZombieAttackType.ATTACKTYPE_DRIVE_OVER);
			foreach (LawnMower lawnMower in this.mBoard.mLawnMowers)
			{
				if (!lawnMower.mDead && lawnMower.mMowerState != LawnMowerState.MOWER_SQUISHED && lawnMower.mRow == this.mFireballRow && lawnMower.mPosX > num && lawnMower.mPosX < num + 50f)
				{
					lawnMower.SquishMower();
				}
			}
			if (this.mIsFireBall)
			{
				if (reanimation.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD && reanimation.mLoopCount > 0)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_role, ReanimLoopType.REANIM_LOOP, 0, 2f);
					reanimation.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, this.mFireballRow, 0);
				}
				if (reanimation.mLoopType == ReanimLoopType.REANIM_LOOP && RandomNumbers.NextNumber(10) == 0)
				{
					float num3 = num + 100f + TodCommon.RandRangeFloat(0f, 20f);
					float theY = this.mBoard.GetPosYBasedOnRow(num3 - 40f, this.mFireballRow) + 90f + TodCommon.RandRangeFloat(-50f, 0f);
					int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, this.mFireballRow, 6);
					this.mApp.AddTodParticle(num3, theY, aRenderOrder, ParticleEffect.PARTICLE_FIREBALL_TRAIL);
				}
			}
			else
			{
				if (reanimation.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD && reanimation.mLoopCount > 0)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_role, ReanimLoopType.REANIM_LOOP, 0, 2f);
					reanimation.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, this.mFireballRow, 0);
				}
				if (reanimation.mLoopType == ReanimLoopType.REANIM_LOOP && RandomNumbers.NextNumber(10) == 0)
				{
					float num4 = num + 100f + TodCommon.RandRangeFloat(0f, 20f);
					float theY2 = this.mBoard.GetPosYBasedOnRow(num4 - 40f, this.mFireballRow) + 90f + TodCommon.RandRangeFloat(-50f, 0f);
					int aRenderOrder2 = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, this.mFireballRow, 6);
					this.mApp.AddTodParticle(num4, theY2, aRenderOrder2, ParticleEffect.PARTICLE_ICEBALL_TRAIL);
				}
			}
			reanimation.Update();
		}

		public void BossDestroyFireball()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBossFireBallReanimID);
			if (reanimation == null)
			{
				return;
			}
			if (!this.mIsFireBall)
			{
				return;
			}
			float num = reanimation.mOverlayMatrix.mMatrix.M41 * Constants.IS + 80f;
			float num2 = reanimation.mOverlayMatrix.mMatrix.M42 * Constants.IS + 40f;
			for (int i = 0; i < 6; i++)
			{
				float num3 = 1.5707964f + 6.2831855f * (float)i / 6f;
				float theX = num + 60f * (float)Math.Sin((double)num3);
				float theY = num2 + 60f * (float)Math.Cos((double)num3);
				Reanimation reanimation2 = this.mApp.AddReanimation(theX, theY, 400000, ReanimationType.REANIM_JALAPENO_FIRE);
				reanimation2.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME;
				reanimation2.mAnimTime = 0.2f;
				reanimation2.mAnimRate = TodCommon.RandRangeFloat(20f, 25f);
			}
			reanimation.ReanimationDie();
			this.mBossFireBallReanimID = null;
			this.mBoard.RemoveParticleByType(ParticleEffect.PARTICLE_FIREBALL_TRAIL);
		}

		public void BossDestroyIceballInRow(int theRow)
		{
			if (theRow != this.mFireballRow)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBossFireBallReanimID);
			if (reanimation == null)
			{
				return;
			}
			if (this.mIsFireBall)
			{
				return;
			}
			float theX = reanimation.mOverlayMatrix.mMatrix.M41 * Constants.IS + 80f;
			float theY = reanimation.mOverlayMatrix.mMatrix.M42 * Constants.IS + 80f;
			this.mApp.AddTodParticle(theX, theY, 400000, ParticleEffect.PARTICLE_ICEBALL_DEATH);
			reanimation.ReanimationDie();
			this.mBossFireBallReanimID = null;
			this.mBoard.RemoveParticleByType(ParticleEffect.PARTICLE_ICEBALL_TRAIL);
		}

		public void DiggerLoseAxe()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING)
			{
				this.mZombiePhase = ZombiePhase.PHASE_DIGGER_TUNNELING_PAUSE_WITHOUT_AXE;
				this.mPhaseCounter = 200;
				this.SetAnimRate(0f);
				this.UpdateAnimSpeed();
				GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_DIGGER_TUNNEL, null);
				this.StopZombieSound();
			}
			this.mHasObject = false;
			this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_pickaxe, -1);
			this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_dirt, -1);
		}

		public void BungeeDropZombie(Zombie theDroppedZombie, int theGridX, int theGridY)
		{
			this.mTargetCol = theGridX;
			this.SetRow(theGridY);
			this.mPosX = (float)this.mBoard.GridToPixelX(this.mTargetCol, this.mRow);
			this.mPosY = this.GetPosYBasedOnRow(this.mRow);
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_raise, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 36f);
			this.mRelatedZombieID = this.mBoard.ZombieGetID(theDroppedZombie);
			theDroppedZombie.mPosX = this.mPosX - 15f;
			theDroppedZombie.SetRow(theGridY);
			theDroppedZombie.mPosY = this.GetPosYBasedOnRow(theGridY);
			theDroppedZombie.mZombieHeight = ZombieHeight.HEIGHT_GETTING_BUNGEE_DROPPED;
			theDroppedZombie.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 0f);
			theDroppedZombie.mRenderOrder = this.mRenderOrder + 1;
		}

		private Image GetYuckyFaceImage()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
			{
				return AtlasResources.IMAGE_REANIM_ZOMBIE_DISCO_HEAD_GROSSOUT;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				return AtlasResources.IMAGE_REANIM_ZOMBIE_DANCER_HEAD_GROSSOUT;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				return AtlasResources.IMAGE_REANIM_ZOMBIE_POLEVAULTER_HEAD_GROSSOUT;
			}
			return AtlasResources.IMAGE_REANIM_ZOMBIE_HEAD_GROSSOUT;
		}

		public void ShowYuckyFace(bool theShow)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			if (this.HasYuckyFaceImage())
			{
				if (theShow)
				{
					Image yuckyFaceImage = this.GetYuckyFaceImage();
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, yuckyFaceImage);
					reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head2, -1);
					reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head_jaw, -1);
					reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_tongue, -1);
					return;
				}
				if (this.mHasHead)
				{
					Image theImage = null;
					reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_head1, theImage);
					reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head2, 0);
					reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_head_jaw, 0);
					if (this.mVariant)
					{
						reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_tongue, 0);
					}
				}
			}
		}

		public void AnimateChewSound()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_UP_TO_EAT)
			{
				return;
			}
			Plant plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_CHEW);
			if (plant != null)
			{
				if (plant.mSeedType == SeedType.SEED_HYPNOSHROOM && !plant.mIsAsleep)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
					plant.Die();
					this.StartMindControlled();
					this.mApp.AddTodParticle(this.mPosX + 60f, this.mPosY + 40f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_MIND_CONTROL);
					this.TrySpawnLevelAward();
					this.mVelX = 0.17f;
					this.mAnimTicksPerFrame = 18;
					this.UpdateAnimSpeed();
					return;
				}
				if (plant.mSeedType == SeedType.SEED_GARLIC)
				{
					if (!this.mYuckyFace)
					{
						this.mYuckyFace = true;
						this.mYuckyFaceCounter = 0;
						this.UpdateAnimSpeed();
						this.mApp.PlayFoley(FoleyType.FOLEY_CHOMP);
						return;
					}
				}
				else
				{
					if (plant.mSeedType == SeedType.SEED_WALLNUT || plant.mSeedType == SeedType.SEED_TALLNUT || plant.mSeedType == SeedType.SEED_GARLIC || plant.mSeedType == SeedType.SEED_PUMPKINSHELL)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_CHOMP_SOFT);
						return;
					}
					this.mApp.PlayFoley(FoleyType.FOLEY_CHOMP);
					return;
				}
			}
			else
			{
				if (this.mMindControlled)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_CHOMP_SOFT);
					return;
				}
				this.mApp.PlayFoley(FoleyType.FOLEY_CHOMP);
			}
		}

		public void AnimateChewEffect()
		{
			if (this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_UP_TO_EAT)
			{
				return;
			}
			if (this.mApp.IsIZombieLevel())
			{
				GridItem gridItem = this.mBoard.mChallenge.IZombieGetBrainTarget(this);
				if (gridItem != null)
				{
					gridItem.mTransparentCounter = Math.Max(gridItem.mTransparentCounter, 25);
					return;
				}
			}
			Plant plant = this.FindPlantTarget(Zombie.ZombieAttackType.ATTACKTYPE_CHEW);
			if (plant == null)
			{
				return;
			}
			if (plant.mSeedType == SeedType.SEED_WALLNUT || plant.mSeedType == SeedType.SEED_TALLNUT)
			{
				int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, this.mRow, 0);
				ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
				this.GetDrawPos(ref zombieDrawPosition);
				float num = this.mPosX + 37f;
				float num2 = this.mPosY + 40f + zombieDrawPosition.mBodyY;
				if (this.mZombieType == ZombieType.ZOMBIE_SNORKEL || this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER)
				{
					num -= 7f;
					num2 += 70f;
				}
				else if (this.IsWalkingBackwards())
				{
					num += 47f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
				{
					num2 += 47f;
				}
				else if (this.mZombieType == ZombieType.ZOMBIE_IMP)
				{
					num += 24f;
					num2 += 40f;
				}
				this.mApp.AddTodParticle(num, num2, aRenderOrder, ParticleEffect.PARTICLE_WALLNUT_EAT_SMALL);
			}
			plant.mEatenFlashCountdown = Math.Max(plant.mEatenFlashCountdown, 25);
		}

		public void UpdateActions()
		{
			if (this.mZombieHeight == ZombieHeight.HEIGHT_UP_LADDER)
			{
				this.UpdateClimbingLadder();
				this.UpdateClimbingLadder();
				this.UpdateClimbingLadder();
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIQUARIUM)
			{
				this.UpdateZombiquarium();
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_OUT_OF_POOL || this.mZombieHeight == ZombieHeight.HEIGHT_IN_TO_POOL || this.mInPool)
			{
				this.UpdateZombiePool();
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_UP_TO_HIGH_GROUND || this.mZombieHeight == ZombieHeight.HEIGHT_DOWN_OFF_HIGH_GROUND)
			{
				this.UpdateZombieHighGround();
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_FALLING)
			{
				this.UpdateZombieFalling();
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_IN_TO_CHIMNEY)
			{
				this.UpdateZombieChimney();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				this.UpdateZombiePolevaulter();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				this.UpdateZombieCatapult();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER)
			{
				this.UpdateZombieDolphinRider();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
			{
				this.UpdateZombieSnorkel();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				this.UpdateZombieFlyer();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER)
			{
				this.UpdateZombieNewspaper();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				this.UpdateZombieDigger();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_JACK_IN_THE_BOX)
			{
				this.UpdateZombieJackInTheBox();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				this.UpdateZombieGargantuar();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED)
			{
				this.UpdateZombieBobsled();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				this.UpdateZamboni();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_LADDER)
			{
				this.UpdateLadder();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_YETI)
			{
				this.UpdateYeti();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_DANCER)
			{
				this.UpdateZombieDancer();
				this.UpdateZombieDancer();
				this.UpdateZombieDancer();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				this.UpdateZombieBackupDancer();
				this.UpdateZombieBackupDancer();
				this.UpdateZombieBackupDancer();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_IMP)
			{
				this.UpdateZombieImp();
				this.UpdateZombieImp();
				this.UpdateZombieImp();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD)
			{
				this.UpdateZombiePeaHead();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD)
			{
				this.UpdateZombieJalapenoHead();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD)
			{
				this.UpdateZombieGatlingHead();
			}
			if (this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD)
			{
				this.UpdateZombieSquashHead();
			}
		}

		public void CheckForBoardEdge()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				return;
			}
			if (this.IsWalkingBackwards() && this.mPosX > 900f)
			{
				this.DieNoLoot(false);
				return;
			}
			int board_EDGE = Constants.BOARD_EDGE;
			if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				board_EDGE = Constants.BOARD_EDGE;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				board_EDGE = Constants.BOARD_EDGE;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT || this.mZombieType == ZombieType.ZOMBIE_FOOTBALL || this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				board_EDGE = Constants.BOARD_EDGE;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				board_EDGE = Constants.BOARD_EDGE;
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER || this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_SNORKEL)
			{
				board_EDGE = Constants.BOARD_EDGE;
			}
			if (this.mX <= board_EDGE && this.mHasHead)
			{
				if (this.mApp.IsIZombieLevel())
				{
					this.DieNoLoot(false);
				}
				else
				{
					this.mBoard.ZombiesWon(this);
				}
			}
			if (this.mX <= board_EDGE + 70 && !this.mHasHead)
			{
				this.TakeDamage(1800, 9U);
			}
		}

		public void UpdateYeti()
		{
			if (this.mMindControlled || !this.mHasHead || this.IsDeadOrDying())
			{
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_NORMAL && this.mPhaseCounter <= 0)
			{
				this.mZombiePhase = ZombiePhase.PHASE_YETI_RUNNING;
				this.mHasObject = false;
				this.PickRandomSpeed();
			}
		}

		public void DrawBossPart(Graphics g, BossPart theBossPart)
		{
			ZombieDrawPosition zombieDrawPosition = default(ZombieDrawPosition);
			this.GetDrawPos(ref zombieDrawPosition);
			switch (theBossPart)
			{
			case BossPart.BOSS_PART_BACK_LEG:
				this.DrawReanim(g, ref zombieDrawPosition, GameConstants.RENDER_GROUP_BOSS_BACK_LEG);
				return;
			case BossPart.BOSS_PART_FRONT_LEG:
				this.DrawReanim(g, ref zombieDrawPosition, GameConstants.RENDER_GROUP_BOSS_FRONT_LEG);
				return;
			case BossPart.BOSS_PART_MAIN:
				this.DrawReanim(g, ref zombieDrawPosition, 0);
				return;
			case BossPart.BOSS_PART_BACK_ARM:
				this.DrawBossBackArm(g, ref zombieDrawPosition);
				return;
			case BossPart.BOSS_PART_FIREBALL:
				this.DrawBossFireBall(g, ref zombieDrawPosition);
				return;
			default:
				return;
			}
		}

		public void BossSetupReanim()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			reanimation.AssignRenderGroupToPrefix("Boss_innerleg", GameConstants.RENDER_GROUP_BOSS_BACK_LEG);
			reanimation.AssignRenderGroupToPrefix("Boss_outerleg", GameConstants.RENDER_GROUP_BOSS_FRONT_LEG);
			reanimation.AssignRenderGroupToPrefix("Boss_body2", GameConstants.RENDER_GROUP_BOSS_FRONT_LEG);
			reanimation.AssignRenderGroupToPrefix("Boss_innerarm", GameConstants.RENDER_GROUP_BOSS_BACK_ARM);
			reanimation.AssignRenderGroupToPrefix("Boss_RV", GameConstants.RENDER_GROUP_BOSS_BACK_ARM);
			Reanimation reanimation2 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_BOSS_DRIVER);
			reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 0, 12f);
			this.mSpecialHeadReanimID = this.mApp.ReanimationGetID(reanimation2);
			ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_boss_head2);
			AttachEffect attachEffect = GlobalMembersAttachment.AttachReanim(ref trackInstanceByName.mAttachmentID, reanimation2, 25f * Constants.S, -70f * Constants.S);
			reanimation.mFrameBasePose = 0;
			attachEffect.mDontDrawIfParentHidden = true;
			attachEffect.mOffset.Scale(1.2f, 1.2f);
		}

		public void MowDown()
		{
			if (this.mDead || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_MOWERED)
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				this.mApp.AddTodParticle(this.mPosX + 80f, this.mPosY + 60f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_CATAPULT_EXPLOSION);
				this.mApp.PlayFoley(FoleyType.FOLEY_EXPLOSION);
				this.DieWithLoot();
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				this.mApp.AddTodParticle(this.mPosX + 80f, this.mPosY + 60f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_ZAMBONI_EXPLOSION);
				this.mApp.PlayFoley(FoleyType.FOLEY_EXPLOSION);
				this.DieWithLoot();
				return;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_DYING || this.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || this.mZombiePhase == ZombiePhase.PHASE_RISING_FROM_GRAVE || this.mZombiePhase == ZombiePhase.PHASE_DANCER_RISING || this.mZombiePhase == ZombiePhase.PHASE_SNORKEL_INTO_POOL || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIE_BURNED || this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR || this.mZombieType == ZombieType.ZOMBIE_BUNGEE || this.mZombieType == ZombieType.ZOMBIE_DIGGER || this.mZombieType == ZombieType.ZOMBIE_IMP || this.mZombieType == ZombieType.ZOMBIE_YETI || this.mZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER || this.IsBobsledTeamWithSled() || this.IsFlying() || this.mInPool)
			{
				Reanimation reanimation = this.mApp.AddReanimation(this.mPosX - 73f, this.mPosY - 56f, this.mRenderOrder + 2, ReanimationType.REANIM_PUFF);
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_puff);
				this.mApp.AddTodParticle(this.mPosX + 110f, this.mPosY + 0f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_MOWER_CLOUD);
				if (this.mBoard.mPlantRow[this.mRow] != PlantRowType.PLANTROW_POOL)
				{
					this.DropHead(0U);
					this.DropArm(0U);
					this.DropHelm(0U);
					this.DropShield(0U);
				}
				this.DieWithLoot();
				return;
			}
			if (this.mIceTrapCounter > 0)
			{
				this.RemoveIceTrap();
			}
			if (this.mButteredCounter > 0)
			{
				this.mButteredCounter = 0;
			}
			this.DropShield(0U);
			this.DropHelm(0U);
			if (this.mZombieType == ZombieType.ZOMBIE_FLAG)
			{
				this.DropFlag();
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
			{
				this.DropPole();
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER || this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				this.DropHead(0U);
			}
			else if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				this.DropHead(0U);
				this.mAltitude = 0f;
			}
			Reanimation reanimation2 = this.mApp.AddReanimation(0f, 0f, this.mRenderOrder, ReanimationType.REANIM_LAWN_MOWERED_ZOMBIE);
			reanimation2.mIsAttachment = false;
			reanimation2.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
			reanimation2.mAnimRate = 8f;
			this.mMoweredReanimID = this.mApp.ReanimationGetID(reanimation2);
			this.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_MOWERED;
			this.DropLoot();
			this.mBoard.AreEnemyZombiesOnScreen();
		}

		public void UpdateMowered()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mMoweredReanimID);
			if (reanimation == null || reanimation.mLoopCount > 0)
			{
				this.DropHead(0U);
				this.DropArm(0U);
				this.DieWithLoot();
			}
		}

		public void DropFlag()
		{
			if (this.mZombieType != ZombieType.ZOMBIE_FLAG || !this.mHasObject)
			{
				return;
			}
			this.DetachFlag();
			this.mApp.RemoveReanimation(ref this.mSpecialHeadReanimID);
			this.mSpecialHeadReanimID = null;
			this.ReanimShowPrefix("anim_innerarm", 0);
			this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand, -1);
			this.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_innerarm_screendoor, -1);
			this.mHasObject = false;
			float num = 0f;
			float num2 = 0f;
			this.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_flaghand, ref num, ref num2);
			TodParticleSystem aParticle = this.mApp.AddTodParticle(num + 6f, num2 - 45f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_ZOMBIE_FLAG);
			this.OverrideParticleColor(aParticle);
			this.OverrideParticleScale(aParticle);
		}

		public void DropPole()
		{
			if (this.mZombieType != ZombieType.ZOMBIE_POLEVAULTER)
			{
				return;
			}
			this.ReanimShowPrefix("Zombie_polevaulter_innerarm", -1);
			this.ReanimShowPrefix("Zombie_polevaulter_innerhand", -1);
			this.ReanimShowPrefix("Zombie_polevaulter_pole", -1);
		}

		public void DrawBossBackArm(Graphics g, ref ZombieDrawPosition theDrawPos)
		{
			float num = 0f;
			float num2 = 0f;
			if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_DROP_RV)
			{
				num2 = (float)(this.mTargetRow - 1) * 85f - (float)this.mTargetCol * 20f;
				num = (float)this.mTargetCol * 80f;
				num *= Constants.S;
				num2 *= Constants.S;
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_BOSS_BUNGEES_ENTER || this.mZombiePhase == ZombiePhase.PHASE_BOSS_BUNGEES_DROP || this.mZombiePhase == ZombiePhase.PHASE_BOSS_BUNGEES_LEAVE)
			{
				num = (float)this.mTargetCol * 80f - 23f;
				num *= Constants.S;
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			Reanimation reanimation2 = reanimation;
			reanimation2.mOverlayMatrix.mMatrix.M41 = reanimation2.mOverlayMatrix.mMatrix.M41 + num;
			Reanimation reanimation3 = reanimation;
			reanimation3.mOverlayMatrix.mMatrix.M42 = reanimation3.mOverlayMatrix.mMatrix.M42 + num2;
			this.DrawReanim(g, ref theDrawPos, GameConstants.RENDER_GROUP_BOSS_BACK_ARM);
			Reanimation reanimation4 = reanimation;
			reanimation4.mOverlayMatrix.mMatrix.M41 = reanimation4.mOverlayMatrix.mMatrix.M41 - num;
			Reanimation reanimation5 = reanimation;
			reanimation5.mOverlayMatrix.mMatrix.M42 = reanimation5.mOverlayMatrix.mMatrix.M42 - num2;
		}

		public static void PreloadZombieResources(ZombieType theZombieType)
		{
			ZombieDefinition zombieDefinition = Zombie.GetZombieDefinition(theZombieType);
			if (zombieDefinition.mReanimationType != ReanimationType.REANIM_NONE)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(zombieDefinition.mReanimationType, true);
			}
			if (theZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_DIGGER_DIRT, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIE_CHARRED_DIGGER, true);
			}
			if (theZombieType == ZombieType.ZOMBIE_BOSS)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_BOSS_DRIVER, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_BOSS_FIREBALL, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_BOSS_ICEBALL, true);
				for (int i = 0; i < GameConstants.gBossZombieList.Length; i++)
				{
					ZombieDefinition zombieDefinition2 = Zombie.GetZombieDefinition(GameConstants.gBossZombieList[i]);
					ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(zombieDefinition2.mReanimationType, true);
				}
			}
			if (theZombieType == ZombieType.ZOMBIE_DANCER)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_BACKUP_DANCER, true);
			}
			if (theZombieType == ZombieType.ZOMBIE_GARGANTUAR || theZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_IMP, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIE_CHARRED_IMP, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIE_CHARRED_GARGANTUAR, true);
			}
			if (theZombieType == ZombieType.ZOMBIE_ZAMBONI)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_IMP, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIE_CHARRED_ZAMBONI, true);
			}
			if (theZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIE_CHARRED_CATAPULT, true);
			}
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_PUFF, true);
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIE_CHARRED, true);
			ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_LAWN_MOWERED_ZOMBIE, true);
		}

		public void BossStartDeath()
		{
			this.mZombiePhase = ZombiePhase.PHASE_BOSS_HEAD_LEAVE;
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_head_leave, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
			this.mApp.AddTodParticle(700f, 150f, 400000, ParticleEffect.PARTICLE_BOSS_EXPLOSION);
			this.mApp.PlaySample(Resources.SOUND_BOSSEXPLOSION);
			this.mApp.PlayFoley(FoleyType.FOLEY_GARGANTUDEATH);
			this.BossDie();
		}

		public void RemoveColdEffects()
		{
			if (this.mIceTrapCounter > 0)
			{
				this.RemoveIceTrap();
			}
			if (this.mChilledCounter > 0)
			{
				this.mChilledCounter = 0;
				this.UpdateAnimSpeed();
			}
		}

		public void BossHeadSpitEffect()
		{
			int aRenderOrder = this.mRenderOrder + 2;
			if (this.mIsFireBall)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				int theTrackIndex = reanimation.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_boss_jaw);
				ReanimatorTransform reanimatorTransform;
				reanimation.GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
				float theX = this.mPosX + reanimatorTransform.mTransX * Constants.IS + 100f;
				float theY = this.mPosY + reanimatorTransform.mTransY * Constants.IS + 50f;
				this.mApp.AddTodParticle(theX, theY, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_BOSS_FIREBALL);
				reanimatorTransform.PrepareForReuse();
			}
			else
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
				int theTrackIndex2 = reanimation2.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_boss_jaw);
				ReanimatorTransform reanimatorTransform2;
				reanimation2.GetCurrentTransform(theTrackIndex2, out reanimatorTransform2, false);
				float theX2 = this.mPosX + reanimatorTransform2.mTransX * Constants.IS + 100f;
				float theY2 = this.mPosY + reanimatorTransform2.mTransY * Constants.IS + 50f;
				TodParticleSystem todParticleSystem = this.mApp.AddTodParticle(theX2, theY2, aRenderOrder, ParticleEffect.PARTICLE_ZOMBIE_BOSS_FIREBALL);
				if (todParticleSystem != null)
				{
					todParticleSystem.OverrideImage(null, AtlasResources.IMAGE_ZOMBIE_BOSS_ICEBALL_PARTICLES);
				}
				reanimatorTransform2.PrepareForReuse();
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_BOSSBOULDERATTACK);
		}

		public void DrawBossFireBall(Graphics g, ref ZombieDrawPosition theDrawPos)
		{
			base.MakeParentGraphicsFrame(g);
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBossFireBallReanimID);
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

		public void UpdateZombiePeaHead()
		{
			if (!this.mHasHead)
			{
				return;
			}
			if (this.mPhaseCounter >= 36 && this.mPhaseCounter < 39)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				if (reanimation != null)
				{
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 35f);
					return;
				}
			}
			else if (this.mPhaseCounter <= 0)
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				if (reanimation2 != null)
				{
					reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 15f);
				}
				this.mApp.PlayFoley(FoleyType.FOLEY_THROW);
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation3 != null)
				{
					int theTrackIndex = reanimation3.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
					ReanimatorTransform reanimatorTransform;
					reanimation3.GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
					float num = this.mPosX + reanimatorTransform.mTransX * Constants.IS - 9f;
					float num2 = this.mPosY + reanimatorTransform.mTransY * Constants.IS + 6f - this.mAltitude;
					ProjectileType projectileType = ProjectileType.PROJECTILE_ZOMBIE_PEA;
					if (this.mMindControlled)
					{
						num += 90f * this.mScaleZombie;
						projectileType = ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL;
					}
					Projectile projectile = this.mBoard.AddProjectile((int)num, (int)num2, this.mRenderOrder, this.mRow, projectileType);
					if (!this.mMindControlled)
					{
						projectile.mMotionType = ProjectileMotion.MOTION_BACKWARDS;
					}
					this.mPhaseCounter = 150;
					reanimatorTransform.PrepareForReuse();
				}
			}
		}

		public void BurnRow(int theRow)
		{
			int theDamageRangeFlags = 127;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (zombie != this && !zombie.mDead)
				{
					int num = zombie.mRow - this.mRow;
					if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
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
			while (this.mBoard.IterateGridItems(ref gridItem, ref num2))
			{
				if (gridItem.mGridY == theRow && gridItem.mGridItemType == GridItemType.GRIDITEM_LADDER)
				{
					gridItem.GridItemDie();
				}
			}
		}

		public void UpdateZombieJalapenoHead()
		{
			if (!this.mHasHead)
			{
				return;
			}
			if (this.mPhaseCounter <= 0)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_JALAPENO_IGNITE);
				this.mApp.PlayFoley(FoleyType.FOLEY_JUICY);
				this.mBoard.DoFwoosh(this.mRow);
				this.mBoard.ShakeBoard(3, -4);
				if (this.mMindControlled)
				{
					this.BurnRow(this.mRow);
					return;
				}
				int count = this.mBoard.mPlants.Count;
				for (int i = 0; i < count; i++)
				{
					Plant plant = this.mBoard.mPlants[i];
					if (!plant.mDead)
					{
						plant.GetPlantRect();
						if (this.mRow == plant.mRow && !plant.NotOnGround())
						{
							this.mBoard.mPlantsEaten++;
							plant.Die();
						}
					}
				}
				this.DieNoLoot(false);
			}
		}

		public void ApplyBossSmokeParticles(bool theEnable)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_boss_head);
			GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref trackInstanceByName.mAttachmentID, ParticleEffect.PARTICLE_ZAMBONI_SMOKE, null);
			if (theEnable)
			{
				TodParticleSystem todParticleSystem = this.mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.PARTICLE_ZAMBONI_SMOKE);
				TodParticleSystem todParticleSystem2 = this.mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.PARTICLE_ZAMBONI_SMOKE);
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
				if (this.mBodyHealth < this.mBodyMaxHealth / GameConstants.BOSS_FLASH_HEALTH_FRACTION)
				{
					TodParticleSystem todParticleSystem3 = this.mApp.AddTodParticle(0f, 0f, 0, ParticleEffect.PARTICLE_ZAMBONI_SMOKE);
					if (todParticleSystem3 != null)
					{
						AttachEffect attachEffect3 = reanimation.AttachParticleToTrack(GlobalMembersReanimIds.ReanimTrackId_boss_head, ref todParticleSystem3, 80f, 27f);
						attachEffect3.mDontDrawIfParentHidden = true;
						attachEffect3.mDontPropogateColor = true;
					}
				}
			}
		}

		public void UpdateZombiquarium()
		{
			if (this.IsDeadOrDying())
			{
				return;
			}
			float num = this.mVelZ;
			float num2 = this.mVelX;
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIQUARIUM_BITE)
			{
				if (reanimation.mLoopCount > 0)
				{
					float theAnimRate = TodCommon.RandRangeFloat(8f, 10f);
					this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_aquarium_swim, ReanimLoopType.REANIM_LOOP, 20, theAnimRate);
					this.mZombiePhase = ZombiePhase.PHASE_ZOMBIQUARIUM_DRIFT;
					this.mPhaseCounter = 100;
				}
			}
			else if (!this.ZombiquariumFindClosestBrain() && this.mPhaseCounter == 0)
			{
				int num3 = RandomNumbers.NextNumber(7);
				if (num3 <= 4)
				{
					this.mZombiePhase = ZombiePhase.PHASE_ZOMBIQUARIUM_ACCEL;
					num = TodCommon.RandRangeFloat(0f, 6.2831855f);
					this.mPhaseCounter = TodCommon.RandRangeInt(300, 1000);
					reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
				}
				else if (num3 == 4)
				{
					this.mZombiePhase = ZombiePhase.PHASE_ZOMBIQUARIUM_DRIFT;
					num = 4.712389f;
					this.mPhaseCounter = TodCommon.RandRangeInt(300, 1000);
					reanimation.mAnimRate = TodCommon.RandRangeFloat(8f, 10f);
				}
				else if (num3 == 5)
				{
					this.mZombiePhase = ZombiePhase.PHASE_ZOMBIQUARIUM_BACK_AND_FORTH;
					num = 0f;
					this.mPhaseCounter = TodCommon.RandRangeInt(300, 1000);
					reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
				}
				else
				{
					this.mZombiePhase = ZombiePhase.PHASE_ZOMBIQUARIUM_BACK_AND_FORTH;
					num = 3.1415927f;
					this.mPhaseCounter = TodCommon.RandRangeInt(300, 1000);
					reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
				}
			}
			float num4 = (float)Math.Cos((double)num);
			float num5 = (float)Math.Sin((double)num);
			bool flag = false;
			if (this.mPosX < 0f && num4 < 0f)
			{
				flag = true;
			}
			else if (this.mPosX > 680f && num4 > 0f)
			{
				flag = true;
			}
			else if (this.mPosY < 100f && num5 < 0f)
			{
				flag = true;
			}
			else if (this.mPosY > 400f && num5 > 0f)
			{
				flag = true;
			}
			float num6 = 0.5f;
			if (flag)
			{
				num6 = num2 * 0.3f;
				this.mPhaseCounter = Math.Min(100, this.mPhaseCounter);
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIQUARIUM_ACCEL)
			{
				num6 = 0.5f;
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIQUARIUM_BACK_AND_FORTH)
			{
				if (this.mPosX >= 200f || num4 < 0f)
				{
				}
				if (this.mPosX <= 550f || num4 > 0f)
				{
				}
				num6 = 0.3f;
			}
			else if (this.mZombiePhase == ZombiePhase.PHASE_ZOMBIQUARIUM_DRIFT || this.mZombiePhase == ZombiePhase.PHASE_ZOMBIQUARIUM_BITE)
			{
				num6 = 0.05f;
			}
			num2 = Math.Min(num6, num2 + 0.01f);
			num4 *= num2;
			num5 *= num2;
			this.mPosX += num4;
			this.mPosY += num5;
			if (!this.mBoard.HasLevelAwardDropped())
			{
				if (this.mSummonCounter > 0)
				{
					this.mSummonCounter--;
					if (this.mSummonCounter == 0)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
						this.mBoard.AddCoin(this.mX + 50, this.mY + 40, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
						this.mSummonCounter = TodCommon.RandRangeInt(1000, 1500);
					}
				}
				if (this.mZombieAge % 100 == 0)
				{
					this.TakeDamage(10, 8U);
					if (this.IsDeadOrDying())
					{
						this.mApp.PlaySample(Resources.SOUND_ZOMBAQUARIUM_DIE);
					}
				}
			}
		}

		public bool ZombiquariumFindClosestBrain()
		{
			if (this.mBoard.HasLevelAwardDropped())
			{
				return false;
			}
			if (this.mBodyHealth > 150)
			{
				return false;
			}
			GridItem gridItem = null;
			float num = 0f;
			float num2 = 15f;
			float num3 = 15f;
			float num4 = 50f;
			float num5 = 40f;
			int num6 = -1;
			GridItem gridItem2 = null;
			while (this.mBoard.IterateGridItems(ref gridItem2, ref num6))
			{
				if (gridItem2.mGridItemType == GridItemType.GRIDITEM_BRAIN && gridItem2.mGridItemCounter >= 15)
				{
					float num7 = TodCommon.Distance2D(gridItem2.mPosX + num2, gridItem2.mPosY + num3, this.mPosX + num4, this.mPosY + num5);
					if (gridItem == null || num7 < num)
					{
						num = num7;
						gridItem = gridItem2;
					}
				}
			}
			if (gridItem != null && num < 50f)
			{
				gridItem.GridItemDie();
				this.mApp.PlayFoley(FoleyType.FOLEY_SLURP);
				this.mBodyHealth = Math.Min(this.mBodyMaxHealth, this.mBodyHealth + 200);
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_aquarium_bite, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 24f);
				this.mZombiePhase = ZombiePhase.PHASE_ZOMBIQUARIUM_BITE;
				this.mPhaseCounter = 200;
				return false;
			}
			if (gridItem != null)
			{
				float num8 = gridItem.mPosX + num2 - (this.mPosX + num4);
				float num9 = gridItem.mPosY + num3 - (this.mPosY + num5);
				float num10 = this.mVelZ;
				num10 = (float)Math.Atan2((double)num9, (double)num8);
				if (num10 < 0f)
				{
					num10 += 6.2831855f;
				}
				this.mZombiePhase = ZombiePhase.PHASE_ZOMBIQUARIUM_ACCEL;
				return true;
			}
			return false;
		}

		public void UpdateZombieGatlingHead()
		{
			if (!this.mHasHead)
			{
				return;
			}
			if (this.mPhaseCounter >= 99 && this.mPhaseCounter < 102)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 38f);
				return;
			}
			if ((this.mPhaseCounter >= 18 && this.mPhaseCounter < 21) || (this.mPhaseCounter >= 36 && this.mPhaseCounter < 39) || (this.mPhaseCounter >= 51 && this.mPhaseCounter < 54) || (this.mPhaseCounter >= 69 && this.mPhaseCounter < 72))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_THROW);
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation2 != null)
				{
					int theTrackIndex = reanimation2.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
					ReanimatorTransform reanimatorTransform;
					reanimation2.GetCurrentTransform(theTrackIndex, out reanimatorTransform, false);
					float num = this.mPosX + reanimatorTransform.mTransX * Constants.IS - 9f;
					float num2 = this.mPosY + reanimatorTransform.mTransY * Constants.IS + 6f;
					ProjectileType projectileType = ProjectileType.PROJECTILE_ZOMBIE_PEA;
					if (this.mMindControlled)
					{
						num += 90f * this.mScaleZombie;
						projectileType = ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL;
					}
					Projectile projectile = this.mBoard.AddProjectile((int)num, (int)num2, this.mRenderOrder, this.mRow, projectileType);
					if (!this.mMindControlled)
					{
						projectile.mMotionType = ProjectileMotion.MOTION_BACKWARDS;
					}
					reanimatorTransform.PrepareForReuse();
					return;
				}
			}
			else if (this.mPhaseCounter <= 0)
			{
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				reanimation3.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 15f);
				this.mPhaseCounter = 150;
			}
		}

		public void UpdateZombieSquashHead()
		{
			float num = 6f;
			float num2 = -21f;
			if (this.mHasHead && this.mIsEating && this.mZombiePhase == ZombiePhase.PHASE_SQUASH_PRE_LAUNCH)
			{
				this.StopEating();
				this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 20, 12f);
				this.mHasHead = false;
				Reanimation reanimation = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpup, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
				reanimation.mRenderOrder = this.mRenderOrder + 1;
				reanimation.SetPosition((this.mPosX + num) * Constants.S, (this.mPosY + num2) * Constants.S);
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
				ReanimatorTrackInstance trackInstanceByName = reanimation2.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_anim_head1);
				GlobalMembersAttachment.AttachmentDetach(ref trackInstanceByName.mAttachmentID);
				reanimation.mOverlayMatrix.mMatrix.M11 = 0.75f;
				reanimation.mOverlayMatrix.mMatrix.M12 = 0f;
				reanimation.mOverlayMatrix.mMatrix.M22 = 0.75f;
				reanimation.mOverlayMatrix.mMatrix.M21 = 0f;
				this.mZombiePhase = ZombiePhase.PHASE_SQUASH_RISING;
				this.mPhaseCounter = 95;
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_SQUASH_RISING)
			{
				int num3 = this.mBoard.PixelToGridXKeepOnBoard(this.mX, this.mY);
				int num4 = this.mBoard.GridToPixelX(num3, this.mRow);
				int num5 = TodCommon.TodAnimateCurve(50, 20, this.mPhaseCounter, 0, num4 - (int)this.mPosX, TodCurves.CURVE_EASE_IN_OUT);
				int num6 = TodCommon.TodAnimateCurve(50, 20, this.mPhaseCounter, 0, -20, TodCurves.CURVE_EASE_IN_OUT);
				Reanimation reanimation = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				reanimation.SetPosition((this.mPosX + num + (float)num5) * Constants.S, (this.mPosY + num2 + (float)num6) * Constants.S);
				if (this.mPhaseCounter <= 0)
				{
					reanimation = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpdown, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 60f);
					this.mZombiePhase = ZombiePhase.PHASE_SQUASH_FALLING;
					this.mPhaseCounter = 10;
				}
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_SQUASH_FALLING)
			{
				int num7 = TodCommon.TodAnimateCurve(10, 0, this.mPhaseCounter, -20, 74, TodCurves.CURVE_LINEAR);
				int num3 = this.mBoard.PixelToGridXKeepOnBoard(this.mX, this.mY);
				int num8 = this.mBoard.GridToPixelX(num3, this.mRow);
				Reanimation reanimation = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				reanimation.SetPosition((this.mPosX + num + (float)num8 - this.mPosX) * Constants.S, (this.mPosY + num2 + (float)num7) * Constants.S);
				if (this.mPhaseCounter >= 4 && this.mPhaseCounter < 7)
				{
					num3 = this.mBoard.PixelToGridXKeepOnBoard(this.mX, this.mY);
					this.SquishAllInSquare(num3, this.mRow, Zombie.ZombieAttackType.ATTACKTYPE_CHEW);
				}
				if (this.mPhaseCounter <= 0)
				{
					this.mZombiePhase = ZombiePhase.PHASE_SQUASH_DONE_FALLING;
					this.mPhaseCounter = 100;
					this.mBoard.ShakeBoard(1, 4);
					this.mApp.PlayFoley(FoleyType.FOLEY_THUMP);
				}
			}
			if (this.mZombiePhase == ZombiePhase.PHASE_SQUASH_DONE_FALLING && this.mPhaseCounter <= 0)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mSpecialHeadReanimID);
				reanimation.ReanimationDie();
				this.mSpecialHeadReanimID = null;
				this.TakeDamage(1800, 9U);
			}
		}

		public bool IsTanglekelpTarget()
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant.mState == PlantState.STATE_TANGLEKELP_GRABBING && plant.mTargetZombieID == this.mBoard.ZombieGetID(this))
				{
					return true;
				}
			}
			return false;
		}

		public bool HasYuckyFaceImage()
		{
			return !this.mBoard.mFutureMode && (this.mZombieType == ZombieType.ZOMBIE_NORMAL || this.mZombieType == ZombieType.ZOMBIE_TRAFFIC_CONE || this.mZombieType == ZombieType.ZOMBIE_PAIL || this.mZombieType == ZombieType.ZOMBIE_FLAG || this.mZombieType == ZombieType.ZOMBIE_DOOR || this.mZombieType == ZombieType.ZOMBIE_DUCKY_TUBE || this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER || this.mZombieType == ZombieType.ZOMBIE_NEWSPAPER || this.mZombieType == ZombieType.ZOMBIE_POLEVAULTER);
		}

		public bool IsSquashTarget(Plant exceptMe)
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant != exceptMe && plant.mSeedType == SeedType.SEED_SQUASH && plant.mTargetZombieID == this.mBoard.ZombieGetID(this))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsTangleKelpTarget()
		{
			if (!this.mApp.mBoard.StageHasPool())
			{
				return false;
			}
			if (this.mZombieHeight == ZombieHeight.HEIGHT_DRAGGED_UNDER)
			{
				return true;
			}
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant.mSeedType == SeedType.SEED_TANGLEKELP && plant.mTargetZombieID == this.mBoard.ZombieGetID(this))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsFireResistant()
		{
			return this.mZombieType == ZombieType.ZOMBIE_CATAPULT || this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || (this.mShieldType == ShieldType.SHIELDTYPE_DOOR || this.mShieldType == ShieldType.SHIELDTYPE_LADDER);
		}

		public void EnableMustache(bool theEnableMustache)
		{
			if (this.mFromWave == GameConstants.ZOMBIE_WAVE_UI)
			{
				return;
			}
			if (!this.mHasHead)
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD || this.mZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD || this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD || this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
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
			if (this.mFromWave == GameConstants.ZOMBIE_WAVE_UI)
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD || this.mZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD || this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD || this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD || this.mZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			if (reanimation.mReanimationType != ReanimationType.REANIM_ZOMBIE)
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
			if (this.mZombiePhase != ZombiePhase.PHASE_BUNGEE_GRABBING)
			{
				return;
			}
			Plant plant = this.mBoard.mPlants[this.mBoard.mPlants.IndexOf(this.mTargetPlantID)];
			if (plant == null)
			{
				return;
			}
			if (plant.mOnBungeeState == PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE)
			{
				plant.mOnBungeeState = PlantOnBungeeState.PLANT_NOT_ON_BUNGEE;
			}
			else if (plant.mOnBungeeState == PlantOnBungeeState.PLANT_RISING_WITH_BUNGEE)
			{
				plant.Die();
			}
			this.mTargetPlantID = null;
		}

		public void RemoveButter()
		{
			if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				this.BalloonPropellerHatSpin(true);
			}
			if (this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD || this.mZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD || this.mZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD || this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD || this.mZombieType == ZombieType.ZOMBIE_SQUASH_HEAD)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mSpecialHeadReanimID);
				if (reanimation != null)
				{
					if (this.mZombieType == ZombieType.ZOMBIE_PEA_HEAD && reanimation.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
					{
						reanimation.mAnimRate = 35f;
					}
					else if (this.mZombieType == ZombieType.ZOMBIE_GATLING_HEAD && reanimation.IsAnimPlaying(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
					{
						reanimation.mAnimRate = 38f;
					}
					else
					{
						reanimation.mAnimRate = 15f;
					}
				}
			}
			this.UpdateAnimSpeed();
			this.StartZombieSound();
		}

		public void BalloonPropellerHatSpin(bool theSpinning)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
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
			if (this.IsWalkingBackwards())
			{
				return;
			}
			if (this.mBoard.mPlantRow[this.mRow] == PlantRowType.PLANTROW_POOL)
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BOBSLED || this.mZombieType == ZombieType.ZOMBIE_ZAMBONI || this.mZombieType == ZombieType.ZOMBIE_CATAPULT)
			{
				return;
			}
			if (this.mBoard.StageHasRoof())
			{
				return;
			}
			float num = 20f;
			float num2 = 100f;
			if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL || this.mZombieType == ZombieType.ZOMBIE_DANCER || this.mZombieType == ZombieType.ZOMBIE_BACKUP_DANCER)
			{
				num += 160f;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_POGO)
			{
				num2 += 20f;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				num2 += 30f;
				num += 110f;
			}
			if (this.mBoard.StageHasGraveStones())
			{
				num2 += 15f;
			}
			int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, this.mRow, 5);
			this.mApp.AddTodParticle((float)this.mX + Constants.InvertAndScale(num), (float)this.mY + Constants.InvertAndScale(num2), aRenderOrder, ParticleEffect.PARTICLE_DAISY);
		}

		public void EnableDanceMode(bool theEnableDance)
		{
			if (this.mFromWave == GameConstants.ZOMBIE_WAVE_UI || this.mFromWave == GameConstants.ZOMBIE_WAVE_CUTSCENE)
			{
				return;
			}
			if (this.ZombieNotWalking())
			{
				return;
			}
			if (this.IsDeadOrDying())
			{
				return;
			}
			if (this.mZombieType == ZombieType.ZOMBIE_NORMAL || this.mZombieType == ZombieType.ZOMBIE_TRAFFIC_CONE || this.mZombieType == ZombieType.ZOMBIE_PAIL)
			{
				this.StartWalkAnim(0);
			}
		}

		public void BungeeLiftTarget()
		{
			this.PlayZombieReanim(ref GlobalMembersReanimIds.ReanimTrackId_anim_raise, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 36f);
			if (this.mTargetPlantID == null)
			{
				return;
			}
			Plant plant = this.mTargetPlantID;
			if (plant == null)
			{
				return;
			}
			for (int i = 0; i < this.mBoard.mZombies.Count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (zombie.mZombieType == ZombieType.ZOMBIE_BUNGEE && zombie != this && zombie.mTargetPlantID == plant)
				{
					zombie.mTargetPlantID = null;
				}
			}
			plant.mOnBungeeState = PlantOnBungeeState.PLANT_RISING_WITH_BUNGEE;
			this.mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
			Reanimation reanimation = this.mApp.ReanimationTryToGet(plant.mBodyReanimID);
			if (reanimation != null)
			{
				reanimation.mAnimRate = 0.1f;
			}
			if (plant.mSeedType == SeedType.SEED_CATTAIL && this.mBoard.GetTopPlantAt(this.mTargetCol, this.mRow, PlantPriority.TOPPLANT_ONLY_PUMPKIN) != null)
			{
				this.mBoard.NewPlant(this.mTargetCol, this.mRow, SeedType.SEED_LILYPAD, SeedType.SEED_NONE);
			}
			if (this.mApp.IsIZombieLevel())
			{
				this.mBoard.mChallenge.IZombiePlantDropRemainingSun(plant);
			}
		}

		public void SetupWaterTrack(ref string theTrackName)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(theTrackName);
			trackInstanceByName.mIgnoreExtraAdditiveColor = true;
			trackInstanceByName.mIgnoreColorOverride = true;
			trackInstanceByName.mIgnoreClipRect = true;
		}

		public static ZombieDefinition GetZombieDefinition(ZombieType theZombieType)
		{
			Debug.ASSERT(theZombieType >= ZombieType.ZOMBIE_NORMAL && theZombieType < ZombieType.NUM_ZOMBIE_TYPES);
			Debug.ASSERT(GameConstants.gZombieDefs[(int)theZombieType].mZombieType == theZombieType);
			return GameConstants.gZombieDefs[(int)theZombieType];
		}

		public static bool ZombieTypeCanGoInPool(ZombieType theZombieType)
		{
			return theZombieType == ZombieType.ZOMBIE_NORMAL || theZombieType == ZombieType.ZOMBIE_TRAFFIC_CONE || theZombieType == ZombieType.ZOMBIE_PAIL || theZombieType == ZombieType.ZOMBIE_FLAG || theZombieType == ZombieType.ZOMBIE_SNORKEL || theZombieType == ZombieType.ZOMBIE_DOLPHIN_RIDER || theZombieType == ZombieType.ZOMBIE_PEA_HEAD || theZombieType == ZombieType.ZOMBIE_WALLNUT_HEAD || theZombieType == ZombieType.ZOMBIE_JALAPENO_HEAD || theZombieType == ZombieType.ZOMBIE_GATLING_HEAD || theZombieType == ZombieType.ZOMBIE_TALLNUT_HEAD;
		}

		public static bool ZombieTypeCanGoOnHighGround(ZombieType theZombieType)
		{
			return theZombieType != ZombieType.ZOMBIE_ZAMBONI && theZombieType != ZombieType.ZOMBIE_BOBSLED;
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

		public enum ZombieAttackType
		{
			ATTACKTYPE_CHEW,
			ATTACKTYPE_DRIVE_OVER,
			ATTACKTYPE_VAULT,
			ATTACKTYPE_LADDER
		}

		public enum ZombieRenderLayerOffset
		{
			ZOMBIE_LAYER_OFFSET_BOBSLED_4,
			ZOMBIE_LAYER_OFFSET_BOBSLED_3,
			ZOMBIE_LAYER_OFFSET_BOBSLED_2,
			ZOMBIE_LAYER_OFFSET_BOBSLED_1,
			ZOMBIE_LAYER_OFFSET_NORMAL,
			ZOMBIE_LAYER_OFFSET_DOG_WALKER,
			ZOMBIE_LAYER_OFFSET_DOG,
			ZOMBIE_LAYER_OFFSET_DIGGER,
			ZOMBIE_LAYER_OFFSET_ZAMBONI
		}

		public enum ZombieParts
		{
			PART_BODY,
			PART_HEAD,
			PART_HEAD_EATING,
			PART_TONGUE,
			PART_ARM,
			PART_HAIR,
			PART_HEAD_YUCKY,
			PART_ARM_PICKAXE,
			PART_ARM_POLEVAULT,
			PART_ARM_LEASH,
			PART_ARM_FLAG,
			PART_POGO,
			PART_DIGGER
		}
	}
}
