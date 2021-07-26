using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class Plant : GameObject
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
			this.lastPlayedBodyReanim_Name = string.Empty;
			this.lastPlayedBodyReanim_Type = ReanimLoopType.REANIM_PLAY_ONCE;
			this.lastPlayedBodyReanim_BlendTime = 0;
			this.lastPlayedBodyReanim_AnimRate = 0f;
			this.mSeedType = SeedType.SEED_PEASHOOTER;
			this.mPlantCol = 0;
			this.mAnimCounter = 0;
			this.mFrame = 0;
			this.mFrameLength = 0;
			this.mNumFrames = 0;
			this.mState = PlantState.STATE_NOTREADY;
			this.mPlantHealth = 0;
			this.mPlantMaxHealth = 0;
			this.mSubclass = 0;
			this.mDisappearCountdown = 0;
			this.mDoSpecialCountdown = 0;
			this.mStateCountdown = 0;
			this.mLaunchCounter = 0;
			this.mLaunchRate = 0;
			this.mPlantRect = default(TRect);
			this.mPlantAttackRect = default(TRect);
			this.mTargetX = 0;
			this.mTargetY = 0;
			this.mStartRow = 0;
			this.mParticleID = null;
			this.mShootingCounter = 0;
			this.mBodyReanimID = null;
			this.mHeadReanimID = null;
			this.mHeadReanimID2 = null;
			this.mHeadReanimID3 = null;
			this.mBlinkReanimID = null;
			this.mLightReanimID = null;
			this.mSleepingReanimID = null;
			this.mBlinkCountdown = 0;
			this.mRecentlyEatenCountdown = 0;
			this.mEatenFlashCountdown = 0;
			this.mBeghouledFlashCountdown = 0;
			this.mShakeOffsetX = 0f;
			this.mShakeOffsetY = 0f;
			for (int i = 0; i < this.mMagnetItems.Length; i++)
			{
				this.mMagnetItems[i] = null;
			}
			this.mTargetZombieID = null;
			this.mWakeUpCounter = 0;
			this.mOnBungeeState = PlantOnBungeeState.PLANT_NOT_ON_BUNGEE;
			this.mImitaterType = SeedType.SEED_PEASHOOTER;
			this.mPottedPlantIndex = 0;
			this.mAnimPing = false;
			this.mDead = false;
			this.mSquished = false;
			this.mIsAsleep = false;
			this.mIsOnBoard = false;
			this.mHighlighted = false;
			this.mInFlowerPot = false;
		}

		private Plant()
		{
			this.Reset();
		}

		public void PlantInitialize(int theGridX, int theGridY, SeedType theSeedType, SeedType theImitaterType)
		{
			for (int i = 0; i < this.mMagnetItems.Length; i++)
			{
				if (this.mMagnetItems[i] == null)
				{
					this.mMagnetItems[i] = new MagnetItem();
				}
				else
				{
					this.mMagnetItems[i].Reset();
				}
			}
			this.mPlantCol = theGridX;
			this.mRow = theGridY;
			if (this.mBoard != null)
			{
				this.mX = this.mBoard.GridToPixelX(theGridX, theGridY);
				this.mY = this.mBoard.GridToPixelY(theGridX, theGridY);
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
				{
					this.mY -= Constants.ZenGardenGreenhouseOffset.Y;
				}
			}
			this.mAnimCounter = 0;
			this.mAnimPing = true;
			this.mFrame = 0;
			this.mShootingCounter = 0;
			this.mFrameLength = TodCommon.RandRangeInt(12, 18);
			this.mNumFrames = 5;
			this.mState = PlantState.STATE_NOTREADY;
			this.mDead = false;
			this.mSquished = false;
			this.mSeedType = theSeedType;
			this.mImitaterType = theImitaterType;
			this.mPlantHealth = 300;
			this.mDoSpecialCountdown = 0;
			this.mDisappearCountdown = 200;
			this.mTargetX = -1;
			this.mTargetY = -1;
			this.mStateCountdown = 0;
			this.mStartRow = this.mRow;
			this.mParticleID = null;
			this.mBodyReanimID = null;
			this.mHeadReanimID = null;
			this.mHeadReanimID2 = null;
			this.mHeadReanimID3 = null;
			this.mBlinkReanimID = null;
			this.mLightReanimID = null;
			this.mSleepingReanimID = null;
			this.mBlinkCountdown = 0;
			this.mRecentlyEatenCountdown = 0;
			this.mEatenFlashCountdown = 0;
			this.mBeghouledFlashCountdown = 0;
			this.mWidth = 80;
			this.mHeight = 80;
			this.mShakeOffsetX = 0f;
			this.mShakeOffsetY = 0f;
			this.mIsAsleep = false;
			this.mWakeUpCounter = 0;
			this.mOnBungeeState = PlantOnBungeeState.PLANT_NOT_ON_BUNGEE;
			this.mPottedPlantIndex = -1;
			PlantDefinition plantDefinition = Plant.GetPlantDefinition(theSeedType);
			this.mLaunchRate = plantDefinition.mLaunchRate;
			this.mSubclass = (int)plantDefinition.mSubClass;
			this.mRenderOrder = this.CalcRenderOrder();
			Reanimation reanimation = null;
			string empty = string.Empty;
			if (plantDefinition.mReanimationType != ReanimationType.REANIM_NONE)
			{
				float theY = Plant.PlantDrawHeightOffset(this.mBoard, this, this.mSeedType, this.mPlantCol, this.mRow);
				reanimation = this.mApp.AddReanimation(0f, theY, this.mRenderOrder + 1, plantDefinition.mReanimationType);
				reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
				if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
				{
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
				}
				if (this.mApp.IsWallnutBowlingLevel() && reanimation.TrackExists(Reanimation.ReanimTrackId__ground))
				{
					reanimation.SetFramesForLayer(Reanimation.ReanimTrackId__ground);
					if (this.mSeedType == SeedType.SEED_WALLNUT || this.mSeedType == SeedType.SEED_EXPLODE_O_NUT)
					{
						reanimation.mAnimRate = TodCommon.RandRangeFloat(12f, 18f);
					}
					else if (this.mSeedType == SeedType.SEED_GIANT_WALLNUT)
					{
						reanimation.mAnimRate = TodCommon.RandRangeFloat(6f, 10f);
					}
				}
				reanimation.mIsAttachment = true;
				this.mBodyReanimID = this.mApp.ReanimationGetID(reanimation);
				this.mBlinkCountdown = 400 + RandomNumbers.NextNumber(400);
			}
			if (Plant.IsNocturnal(this.mSeedType) && this.mBoard != null && !this.mBoard.StageIsNight())
			{
				this.SetSleeping(true);
			}
			if (this.mLaunchRate > 0)
			{
				if (this.MakesSun())
				{
					this.mLaunchCounter = TodCommon.RandRangeInt(300, this.mLaunchRate / 2);
				}
				else
				{
					this.mLaunchCounter = TodCommon.RandRangeInt(0, this.mLaunchRate);
				}
			}
			else
			{
				this.mLaunchCounter = 0;
			}
			if (theSeedType == SeedType.SEED_BLOVER)
			{
				this.mDoSpecialCountdown = 50;
				if (this.IsInPlay())
				{
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_blow);
					reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					reanimation.mAnimRate = 20f;
				}
				else
				{
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
					reanimation.mAnimRate = 10f;
				}
			}
			else if (theSeedType == SeedType.SEED_PEASHOOTER || theSeedType == SeedType.SEED_SNOWPEA || theSeedType == SeedType.SEED_REPEATER || theSeedType == SeedType.SEED_LEFTPEATER || theSeedType == SeedType.SEED_GATLINGPEA)
			{
				if (reanimation != null)
				{
					reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
					Reanimation reanimation2 = this.mApp.AddReanimation(0f, 0f, this.mRenderOrder + 2, plantDefinition.mReanimationType);
					reanimation2.mLoopType = ReanimLoopType.REANIM_LOOP;
					reanimation2.mAnimRate = reanimation.mAnimRate;
					reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
					this.mHeadReanimID = this.mApp.ReanimationGetID(reanimation2);
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
			else if (theSeedType == SeedType.SEED_SPLITPEA)
			{
				Debug.ASSERT(reanimation != null);
				reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
				Reanimation reanimation3 = this.mApp.AddReanimation(0f, 0f, this.mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation3.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation3.mAnimRate = reanimation.mAnimRate;
				reanimation3.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
				reanimation3.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
				this.mHeadReanimID = this.mApp.ReanimationGetID(reanimation3);
				Reanimation reanimation4 = this.mApp.AddReanimation(0f, 0f, this.mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation4.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation4.mAnimRate = reanimation.mAnimRate;
				reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_idle);
				reanimation4.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
				this.mHeadReanimID2 = this.mApp.ReanimationGetID(reanimation4);
			}
			else if (theSeedType == SeedType.SEED_THREEPEATER)
			{
				Debug.ASSERT(reanimation != null);
				reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
				Reanimation reanimation5 = this.mApp.AddReanimation(0f, 0f, this.mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation5.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation5.mAnimRate = reanimation.mAnimRate;
				reanimation5.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle1);
				reanimation5.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_head1);
				this.mHeadReanimID = this.mApp.ReanimationGetID(reanimation5);
				Reanimation reanimation6 = this.mApp.AddReanimation(0f, 0f, this.mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation6.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation6.mAnimRate = reanimation.mAnimRate;
				reanimation6.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle2);
				reanimation6.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_head2);
				this.mHeadReanimID2 = this.mApp.ReanimationGetID(reanimation6);
				Reanimation reanimation7 = this.mApp.AddReanimation(0f, 0f, this.mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation7.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation7.mAnimRate = reanimation.mAnimRate;
				reanimation7.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle3);
				reanimation7.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_head3);
				this.mHeadReanimID3 = this.mApp.ReanimationGetID(reanimation7);
			}
			else if (theSeedType == SeedType.SEED_WALLNUT)
			{
				this.mPlantHealth = 4000;
				this.mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
			}
			else if (theSeedType == SeedType.SEED_EXPLODE_O_NUT)
			{
				this.mPlantHealth = 4000;
				this.mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
				reanimation.mColorOverride = new SexyColor(255, 64, 64);
			}
			else if (theSeedType == SeedType.SEED_GIANT_WALLNUT)
			{
				this.mPlantHealth = 4000;
				this.mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
			}
			else if (theSeedType == SeedType.SEED_TALLNUT)
			{
				this.mPlantHealth = 8000;
				this.mHeight = 80;
				this.mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
			}
			else if (theSeedType == SeedType.SEED_GARLIC)
			{
				Debug.ASSERT(reanimation != null);
				this.mPlantHealth = 400;
				reanimation.SetTruncateDisappearingFrames(empty, false);
			}
			else if (theSeedType == SeedType.SEED_GOLD_MAGNET)
			{
				Debug.ASSERT(reanimation != null);
				reanimation.SetTruncateDisappearingFrames(empty, false);
			}
			else if (theSeedType == SeedType.SEED_CHERRYBOMB)
			{
				Debug.ASSERT(reanimation != null);
				if (this.IsInPlay())
				{
					this.mDoSpecialCountdown = 100;
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_explode);
					reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					this.mApp.PlayFoley(FoleyType.FOLEY_REVERSE_EXPLOSION);
				}
			}
			else if (theSeedType == SeedType.SEED_IMITATER)
			{
				Debug.ASSERT(reanimation != null);
				reanimation.mAnimRate = TodCommon.RandRangeFloat(25f, 30f);
				this.mStateCountdown = 200;
			}
			else if (theSeedType == SeedType.SEED_JALAPENO)
			{
				Debug.ASSERT(reanimation != null);
				if (this.IsInPlay())
				{
					this.mDoSpecialCountdown = 100;
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_explode);
					reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					this.mApp.PlayFoley(FoleyType.FOLEY_REVERSE_EXPLOSION);
				}
			}
			else if (theSeedType == SeedType.SEED_POTATOMINE)
			{
				Debug.ASSERT(reanimation != null);
				reanimation.mAnimRate = 12f;
				if (this.IsInPlay())
				{
					reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_glow, -1);
					this.mStateCountdown = 1500;
				}
				else
				{
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_armed);
					this.mState = PlantState.STATE_POTATO_ARMED;
				}
			}
			else if (theSeedType == SeedType.SEED_GRAVEBUSTER)
			{
				Debug.ASSERT(reanimation != null);
				if (this.IsInPlay())
				{
					this.mY += 8;
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_land);
					reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					this.mState = PlantState.STATE_GRAVEBUSTER_LANDING;
					this.mApp.PlayFoley(FoleyType.FOLEY_GRAVEBUSTERCHOMP);
				}
			}
			else if (theSeedType == SeedType.SEED_SUNSHROOM)
			{
				Debug.ASSERT(reanimation != null);
				reanimation.mFrameBasePose = 6;
				if (this.IsInPlay())
				{
					this.mX += RandomNumbers.NextNumber(10) - 5;
					this.mY += RandomNumbers.NextNumber(10) - 5;
				}
				else if (this.mIsAsleep)
				{
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_bigsleep);
				}
				else
				{
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_bigidle);
				}
				this.mState = PlantState.STATE_SUNSHROOM_SMALL;
				this.mStateCountdown = 12000;
			}
			else if (theSeedType == SeedType.SEED_PUFFSHROOM || theSeedType == SeedType.SEED_SEASHROOM)
			{
				if (this.IsInPlay())
				{
					this.mX += RandomNumbers.NextNumber(10) - 5;
					this.mY += RandomNumbers.NextNumber(6) - 3;
				}
			}
			else if (theSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				this.mPlantHealth = 4000;
				this.mWidth = 120;
				Debug.ASSERT(reanimation != null);
				reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_pumpkin_back, 1);
			}
			else if (theSeedType == SeedType.SEED_CHOMPER)
			{
				this.mState = PlantState.STATE_READY;
			}
			else if (theSeedType == SeedType.SEED_PLANTERN)
			{
				this.mStateCountdown = 50;
				if (!this.IsOnBoard() || this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
				{
					this.AddAttachedParticle(this.mX + 40, this.mY + 40, 500001, ParticleEffect.PARTICLE_LANTERN_SHINE);
				}
				if (this.IsInPlay())
				{
					this.mApp.PlaySample(Resources.SOUND_PLANTERN);
				}
			}
			else if (theSeedType != SeedType.SEED_TORCHWOOD)
			{
				if (theSeedType == SeedType.SEED_MARIGOLD)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
				}
				else if (theSeedType == SeedType.SEED_CACTUS)
				{
					this.mState = PlantState.STATE_CACTUS_LOW;
				}
				else if (theSeedType == SeedType.SEED_INSTANT_COFFEE)
				{
					this.mDoSpecialCountdown = 100;
				}
				else if (theSeedType == SeedType.SEED_SCAREDYSHROOM)
				{
					this.mState = PlantState.STATE_READY;
				}
				else if (theSeedType == SeedType.SEED_COBCANNON)
				{
					if (this.IsInPlay())
					{
						this.mState = PlantState.STATE_COBCANNON_ARMING;
						this.mStateCountdown = 500;
						Debug.ASSERT(reanimation != null);
						reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_unarmed_idle);
					}
				}
				else if (theSeedType == SeedType.SEED_KERNELPULT)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.AssignRenderGroupToPrefix("Cornpult_butter", -1);
				}
				else if (theSeedType == SeedType.SEED_MAGNETSHROOM)
				{
					Debug.ASSERT(reanimation != null);
					reanimation.SetTruncateDisappearingFrames(empty, false);
				}
				else if (theSeedType == SeedType.SEED_SPIKEROCK)
				{
					this.mPlantHealth = 450;
					Debug.ASSERT(reanimation != null);
				}
				else if (theSeedType != SeedType.SEED_SPROUT)
				{
					if (theSeedType == SeedType.SEED_FLOWERPOT)
					{
						if (this.IsInPlay())
						{
							this.mState = PlantState.STATE_FLOWERPOT_INVULNERABLE;
							this.mStateCountdown = 100;
						}
					}
					else if (theSeedType == SeedType.SEED_LILYPAD)
					{
						if (this.IsInPlay())
						{
							this.mState = PlantState.STATE_LILYPAD_INVULNERABLE;
							this.mStateCountdown = 100;
						}
					}
					else if (theSeedType == SeedType.SEED_TANGLEKELP)
					{
						Debug.ASSERT(reanimation != null);
						reanimation.SetTruncateDisappearingFrames(empty, false);
					}
				}
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BIG_TIME && (this.mSeedType == SeedType.SEED_WALLNUT || this.mSeedType == SeedType.SEED_SUNFLOWER || this.mSeedType == SeedType.SEED_MARIGOLD))
			{
				this.mPlantHealth *= 2;
			}
			this.mPlantMaxHealth = this.mPlantHealth;
			if (this.mSeedType != SeedType.SEED_FLOWERPOT && this.IsOnBoard())
			{
				Plant flowerPotAt = this.mBoard.GetFlowerPotAt(this.mPlantCol, this.mRow);
				if (flowerPotAt != null)
				{
					Reanimation reanimation8 = this.mApp.ReanimationGet(flowerPotAt.mBodyReanimID);
					reanimation8.mAnimRate = 0f;
					this.mInFlowerPot = true;
				}
			}
			this.checkForPlantAchievements();
			this.UpdateReanim();
		}

		public void Update()
		{
			if ((!this.IsOnBoard() || this.mApp.mGameScene != GameScenes.SCENE_LEVEL_INTRO || !this.mApp.IsWallnutBowlingLevel()) && (!this.IsOnBoard() || this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN) && (!this.IsOnBoard() || !this.mBoard.mCutScene.ShouldRunUpsellBoard()) && this.IsOnBoard() && this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				return;
			}
			this.UpdateAbilities();
			this.Animate();
			if (this.mPlantHealth < 0)
			{
				this.Die();
			}
			this.UpdateReanim();
		}

		public void Animate()
		{
			if ((this.mSeedType == SeedType.SEED_CHERRYBOMB || this.mSeedType == SeedType.SEED_JALAPENO) && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mShakeOffsetX = TodCommon.RandRangeFloat(0f, 2f) - 1f;
				this.mShakeOffsetY = TodCommon.RandRangeFloat(0f, 2f) - 1f;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mPottedPlantIndex != -1)
			{
				this.UpdateNeedsFood();
			}
			if (this.mRecentlyEatenCountdown > 0)
			{
				this.mRecentlyEatenCountdown -= 3;
			}
			if (this.mEatenFlashCountdown > 0)
			{
				this.mEatenFlashCountdown -= 3;
			}
			if (this.mBeghouledFlashCountdown > 0)
			{
				this.mBeghouledFlashCountdown -= 3;
			}
			if (this.mSquished)
			{
				this.mFrame = 0;
				return;
			}
			if (this.mSeedType == SeedType.SEED_WALLNUT || this.mSeedType == SeedType.SEED_TALLNUT)
			{
				this.AnimateNuts();
			}
			else if (this.mSeedType == SeedType.SEED_GARLIC)
			{
				this.AnimateGarlic();
			}
			else if (this.mSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				this.AnimatePumpkin();
			}
			this.UpdateBlink();
			if (this.mAnimPing)
			{
				int num = this.mFrameLength * this.mNumFrames - 1;
				if (this.mAnimCounter < num)
				{
					this.mAnimCounter += 3;
				}
				else
				{
					this.mAnimPing = false;
					this.mAnimCounter -= this.mFrameLength;
				}
			}
			else if (this.mAnimCounter > 0)
			{
				this.mAnimCounter -= 3;
			}
			else
			{
				this.mAnimPing = true;
				this.mAnimCounter += this.mFrameLength;
			}
			this.mFrame = this.mAnimCounter / this.mFrameLength;
		}

		public void Draw(Graphics g)
		{
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			int theCelRow = 0;
			float num = 0f;
			float num2 = Plant.PlantDrawHeightOffset(this.mBoard, this, this.mSeedType, this.mPlantCol, this.mRow);
			if (Plant.IsFlying(this.mSeedType) && this.mSquished)
			{
				num2 += 30f;
			}
			int theCelCol = this.mFrame;
			Image image = Plant.GetImage(this.mSeedType);
			if (this.mSquished)
			{
				if (this.mSeedType == SeedType.SEED_FLOWERPOT)
				{
					num2 -= 15f;
				}
				if (this.mSeedType == SeedType.SEED_INSTANT_COFFEE)
				{
					num2 -= 20f;
				}
				g.SetScale(1f, 0.5f, 0f, 0f);
				Image imageInAtlasById = AtlasResources.GetImageInAtlasById((int)(10300 + this.mSeedType));
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(255f * Math.Min(1f, (float)this.mDisappearCountdown / 100f))));
				Plant.DrawSeedType(g, this.mSeedType, this.mImitaterType, DrawVariation.VARIATION_NORMAL, num * Constants.S + (float)(imageInAtlasById.GetCelWidth() / 2) + (float)Constants.Plant_Squished_Offset.X, num2 * Constants.S + (float)imageInAtlasById.GetCelHeight() + (float)Constants.Plant_Squished_Offset.Y);
				g.SetScale(1f, 1f, 0f, 0f);
				g.SetColorizeImages(false);
				return;
			}
			bool flag = false;
			Plant plant = null;
			if (this.IsOnBoard())
			{
				plant = this.mBoard.GetPumpkinAt(this.mPlantCol, this.mRow);
				if (plant != null)
				{
					Plant plant2 = this.mBoard.GetTopPlantAt(this.mPlantCol, this.mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
					if (plant2 != null && plant2.mRenderOrder > plant.mRenderOrder)
					{
						plant2 = null;
					}
					if (plant2 != null && plant2.mOnBungeeState == PlantOnBungeeState.PLANT_GETTING_GRABBED_BY_BUNGEE)
					{
						plant2 = null;
					}
					if (plant2 == this)
					{
						flag = true;
					}
					if (plant2 == null && this.mSeedType == SeedType.SEED_PUMPKINSHELL)
					{
						flag = true;
					}
				}
				else if (this.mSeedType == SeedType.SEED_PUMPKINSHELL)
				{
					flag = true;
					plant = this;
				}
			}
			else if (this.mSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				flag = true;
				plant = this;
			}
			if (!GlobalStaticVars.gLowFramerate)
			{
				this.DrawShadow(g, num, num2);
			}
			if (Plant.IsFlying(this.mSeedType))
			{
				int num3;
				if (this.IsOnBoard())
				{
					num3 = this.mBoard.mMainCounter;
				}
				else
				{
					num3 = this.mApp.mAppCounter;
				}
				float num4 = (float)(num3 + this.mRow * 97 + this.mPlantCol * 61) * 0.03f;
				float num5 = (float)Math.Sin((double)num4) * 2f;
				num2 += num5;
			}
			if (flag && plant.mBodyReanimID.mActive)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(plant.mBodyReanimID);
				Graphics @new = Graphics.GetNew(g);
				@new.mTransX += plant.mX - this.mX;
				@new.mTransY += plant.mY - this.mY;
				reanimation.DrawRenderGroup(@new, 1);
				@new.PrepareForReuse();
			}
			num += this.mShakeOffsetX;
			num2 += this.mShakeOffsetY;
			if (this.IsInPlay() && this.mApp.IsIZombieLevel())
			{
				this.mBoard.mChallenge.IZombieDrawPlant(g, this);
			}
			else if (this.mBodyReanimID != null)
			{
				Reanimation reanimation2 = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
				if (reanimation2 != null)
				{
					if (this.mGloveGrabbed)
					{
						g.SetColorizeImages(true);
						g.SetColor(new Color(150, 255, 150, 255));
					}
					reanimation2.DrawRenderGroup(g, 0);
					if (this.mGloveGrabbed)
					{
						g.SetColorizeImages(false);
					}
				}
			}
			else
			{
				SeedType seedType = SeedType.SEED_NONE;
				if (this.mBoard != null)
				{
					seedType = this.mBoard.GetSeedTypeInCursor();
				}
				if (this.IsPartOfUpgradableTo(seedType) && this.mBoard.CanPlantAt(this.mPlantCol, this.mRow, seedType) == PlantingReason.PLANTING_OK)
				{
					g.SetColorizeImages(true);
					g.SetColor(TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 90));
				}
				else if (seedType == SeedType.SEED_COBCANNON && this.mBoard.CanPlantAt(this.mPlantCol - 1, this.mRow, seedType) == PlantingReason.PLANTING_OK)
				{
					g.SetColorizeImages(true);
					g.SetColor(TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 90));
				}
				else if (this.mBoard != null && this.mBoard.mTutorialState == TutorialState.TUTORIAL_SHOVEL_DIG)
				{
					g.SetColorizeImages(true);
					g.SetColor(TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 90));
				}
				if (image != null)
				{
					TodCommon.TodDrawImageCelF(g, image, num, num2, theCelCol, theCelRow);
				}
				if (this.mSeedType == SeedType.SEED_SPROUT)
				{
					if (this.mGloveGrabbed)
					{
						g.SetColorizeImages(true);
						g.SetColor(new Color(150, 255, 150, 255));
					}
					TodCommon.TodDrawImageCelF(g, AtlasResources.IMAGE_CACHED_MARIGOLD, (float)Constants.ZenGarden_Marigold_Sprout_Offset.X, (float)Constants.ZenGarden_Marigold_Sprout_Offset.Y, 0, 0);
					if (this.mGloveGrabbed)
					{
						g.SetColorizeImages(false);
					}
				}
				g.SetColorizeImages(false);
				if (this.mHighlighted)
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
				else if (this.mEatenFlashCountdown > 0)
				{
					g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
					g.SetColorizeImages(true);
					int theAlpha = TodCommon.ClampInt(this.mEatenFlashCountdown * 3, 0, 255);
					g.SetColor(new SexyColor(255, 255, 255, theAlpha));
					if (image != null)
					{
						TodCommon.TodDrawImageCelF(g, image, num, num2, theCelCol, theCelRow);
					}
					g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
					g.SetColorizeImages(false);
				}
			}
			if (this.mSeedType == SeedType.SEED_MAGNETSHROOM && !this.DrawMagnetItemsOnTop())
			{
				this.DrawMagnetItems(g);
			}
		}

		public void MouseDown(int x, int y, int theClickCount)
		{
			if (theClickCount < 0)
			{
				return;
			}
			if (this.mState == PlantState.STATE_COBCANNON_READY && this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL)
			{
				this.mBoard.RefreshSeedPacketFromCursor();
				this.mBoard.mCursorObject.mType = SeedType.SEED_NONE;
				this.mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_COBCANNON_TARGET;
				this.mBoard.mCursorObject.mSeedBankIndex = -1;
				this.mBoard.mCursorObject.mCoinID = null;
				this.mBoard.mCursorObject.mCobCannonPlantID = this.mBoard.mPlants[this.mBoard.mPlants.IndexOf(this)];
				this.mBoard.mCobCannonCursorDelayCounter = 30;
				this.mBoard.mCobCannonMouseX = x;
				this.mBoard.mCobCannonMouseY = y;
			}
		}

		public void DoSpecial()
		{
			int num = this.mX + this.mWidth / 2;
			int num2 = this.mY + this.mHeight / 2;
			int damageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			if (this.mSeedType == SeedType.SEED_BLOVER)
			{
				if (this.mState == PlantState.STATE_DOINGSPECIAL)
				{
					return;
				}
				this.mState = PlantState.STATE_DOINGSPECIAL;
				this.BlowAwayFliers(this.mX, this.mRow);
				return;
			}
			else
			{
				if (this.mSeedType == SeedType.SEED_CHERRYBOMB)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_CHERRYBOMB);
					this.mApp.PlayFoley(FoleyType.FOLEY_JUICY);
					int num3 = this.mBoard.KillAllZombiesInRadius(this.mRow, num, num2, 115, 1, true, damageRangeFlags);
					if (num3 >= 10 && !this.mApp.IsLittleTroubleLevel())
					{
						this.mBoard.GrantAchievement(AchievementId.ACHIEVEMENT_EXPLODONATOR, true);
					}
					this.mApp.AddTodParticle((float)num, (float)num2, 400000, ParticleEffect.PARTICLE_POWIE);
					this.mApp.Vibrate();
					this.mBoard.ShakeBoard(3, -4);
					this.Die();
					return;
				}
				if (this.mSeedType == SeedType.SEED_DOOMSHROOM)
				{
					this.mApp.PlaySample(Resources.SOUND_DOOMSHROOM);
					this.mBoard.KillAllZombiesInRadius(this.mRow, num, num2, 250, 3, true, damageRangeFlags);
					this.KillAllPlantsNearDoom();
					this.mApp.AddTodParticle((float)num, (float)num2, 400000, ParticleEffect.PARTICLE_DOOM);
					GridItem gridItem = this.mBoard.AddACrater(this.mPlantCol, this.mRow);
					gridItem.mGridItemCounter = 18000;
					this.mBoard.ShakeBoard(3, -4);
					this.mApp.Vibrate();
					this.Die();
					this.mBoard.mDoomsUsed++;
					return;
				}
				if (this.mSeedType == SeedType.SEED_JALAPENO)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_JALAPENO_IGNITE);
					this.mApp.PlayFoley(FoleyType.FOLEY_JUICY);
					this.mBoard.DoFwoosh(this.mRow);
					this.mBoard.ShakeBoard(3, -4);
					this.mApp.Vibrate();
					this.BurnRow(this.mRow);
					if (this.mBoard.mIceTimer[this.mRow] > 0)
					{
						this.mBoard.mIceTimer[this.mRow] = 20;
					}
					this.Die();
					return;
				}
				if (this.mSeedType == SeedType.SEED_UMBRELLA)
				{
					if (this.mState == PlantState.STATE_UMBRELLA_TRIGGERED || this.mState == PlantState.STATE_UMBRELLA_REFLECTING)
					{
						return;
					}
					this.mState = PlantState.STATE_UMBRELLA_TRIGGERED;
					this.mStateCountdown = 5;
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_block, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 22f);
					return;
				}
				else
				{
					if (this.mSeedType == SeedType.SEED_ICESHROOM)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_FROZEN);
						this.IceZombies();
						this.mApp.AddTodParticle((float)num, (float)num2, 400000, ParticleEffect.PARTICLE_ICE_TRAP);
						this.Die();
						return;
					}
					if (this.mSeedType == SeedType.SEED_POTATOMINE)
					{
						num = this.mX + this.mWidth / 2 - 20;
						num2 = this.mY + this.mHeight / 2;
						this.mApp.PlaySample(Resources.SOUND_POTATO_MINE);
						this.mBoard.KillAllZombiesInRadius(this.mRow, num, num2, 60, 0, false, damageRangeFlags);
						int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, this.mRow, 0);
						this.mApp.AddTodParticle((float)num + 20f, (float)num2, aRenderOrder, ParticleEffect.PARTICLE_POTATO_MINE);
						this.mBoard.ShakeBoard(3, -4);
						this.mApp.Vibrate();
						this.Die();
						return;
					}
					if (this.mSeedType == SeedType.SEED_INSTANT_COFFEE)
					{
						Plant topPlantAt = this.mBoard.GetTopPlantAt(this.mPlantCol, this.mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
						if (topPlantAt != null && topPlantAt.mIsAsleep)
						{
							topPlantAt.mWakeUpCounter = 100;
						}
						this.mState = PlantState.STATE_DOINGSPECIAL;
						this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_crumble, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 22f);
						this.mApp.PlayFoley(FoleyType.FOLEY_COFFEE);
					}
					return;
				}
			}
		}

		public void Fire(Zombie theTargetZombie, int theRow, PlantWeapon thePlantWeapon)
		{
			if (this.mSeedType == SeedType.SEED_FUMESHROOM)
			{
				this.DoRowAreaDamage(20, 2U);
				this.mApp.PlayFoley(FoleyType.FOLEY_FUME);
				return;
			}
			if (this.mSeedType == SeedType.SEED_GLOOMSHROOM)
			{
				this.DoRowAreaDamage(20, 2U);
				return;
			}
			if (this.mSeedType == SeedType.SEED_STARFRUIT)
			{
				this.StarFruitFire();
				return;
			}
			ProjectileType projectileType = ProjectileType.NUM_PROJECTILES;
			SeedType seedType = this.mSeedType;
			if (seedType <= SeedType.SEED_THREEPEATER)
			{
				if (seedType <= SeedType.SEED_PUFFSHROOM)
				{
					if (seedType == SeedType.SEED_PEASHOOTER)
					{
						projectileType = ProjectileType.PROJECTILE_PEA;
						goto IL_157;
					}
					switch (seedType)
					{
					case SeedType.SEED_SNOWPEA:
						projectileType = ProjectileType.PROJECTILE_SNOWPEA;
						goto IL_157;
					case SeedType.SEED_REPEATER:
						projectileType = ProjectileType.PROJECTILE_PEA;
						goto IL_157;
					case SeedType.SEED_PUFFSHROOM:
						projectileType = ProjectileType.PROJECTILE_PUFF;
						goto IL_157;
					}
				}
				else
				{
					if (seedType == SeedType.SEED_SCAREDYSHROOM)
					{
						projectileType = ProjectileType.PROJECTILE_PUFF;
						goto IL_157;
					}
					if (seedType == SeedType.SEED_THREEPEATER)
					{
						projectileType = ProjectileType.PROJECTILE_PEA;
						goto IL_157;
					}
				}
			}
			else if (seedType <= SeedType.SEED_KERNELPULT)
			{
				switch (seedType)
				{
				case SeedType.SEED_SEASHROOM:
					projectileType = ProjectileType.PROJECTILE_PUFF;
					goto IL_157;
				case SeedType.SEED_PLANTERN:
				case SeedType.SEED_BLOVER:
					break;
				case SeedType.SEED_CACTUS:
					projectileType = ProjectileType.PROJECTILE_SPIKE;
					goto IL_157;
				case SeedType.SEED_SPLITPEA:
					projectileType = ProjectileType.PROJECTILE_PEA;
					goto IL_157;
				default:
					switch (seedType)
					{
					case SeedType.SEED_CABBAGEPULT:
						projectileType = ProjectileType.PROJECTILE_CABBAGE;
						goto IL_157;
					case SeedType.SEED_KERNELPULT:
						projectileType = ProjectileType.PROJECTILE_KERNEL;
						goto IL_157;
					}
					break;
				}
			}
			else
			{
				switch (seedType)
				{
				case SeedType.SEED_MELONPULT:
					projectileType = ProjectileType.PROJECTILE_MELON;
					goto IL_157;
				case SeedType.SEED_GATLINGPEA:
					projectileType = ProjectileType.PROJECTILE_PEA;
					goto IL_157;
				case SeedType.SEED_TWINSUNFLOWER:
				case SeedType.SEED_GLOOMSHROOM:
				case SeedType.SEED_GOLD_MAGNET:
				case SeedType.SEED_SPIKEROCK:
					break;
				case SeedType.SEED_CATTAIL:
					projectileType = ProjectileType.PROJECTILE_SPIKE;
					goto IL_157;
				case SeedType.SEED_WINTERMELON:
					projectileType = ProjectileType.PROJECTILE_WINTERMELON;
					goto IL_157;
				case SeedType.SEED_COBCANNON:
					projectileType = ProjectileType.PROJECTILE_COBBIG;
					goto IL_157;
				default:
					if (seedType == SeedType.SEED_LEFTPEATER)
					{
						projectileType = ProjectileType.PROJECTILE_PEA;
						goto IL_157;
					}
					break;
				}
			}
			Debug.ASSERT(false);
			IL_157:
			if (this.mSeedType == SeedType.SEED_KERNELPULT && thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
			{
				projectileType = ProjectileType.PROJECTILE_BUTTER;
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_THROW);
			if (this.mSeedType == SeedType.SEED_SNOWPEA || this.mSeedType == SeedType.SEED_WINTERMELON)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_SNOW_PEA_SPARKLES);
			}
			else if (this.mSeedType == SeedType.SEED_PUFFSHROOM || this.mSeedType == SeedType.SEED_SCAREDYSHROOM || this.mSeedType == SeedType.SEED_SEASHROOM)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_PUFF);
			}
			int num;
			int num2;
			if (this.mSeedType == SeedType.SEED_PUFFSHROOM)
			{
				num = this.mX + 40;
				num2 = this.mY + 40;
			}
			else if (this.mSeedType == SeedType.SEED_SEASHROOM)
			{
				num = this.mX + 45;
				num2 = this.mY + 63;
			}
			else if (this.mSeedType == SeedType.SEED_CABBAGEPULT)
			{
				num = this.mX + 5;
				num2 = this.mY - 12;
			}
			else if (this.mSeedType == SeedType.SEED_MELONPULT || this.mSeedType == SeedType.SEED_WINTERMELON)
			{
				num = this.mX + 25;
				num2 = this.mY - 46;
			}
			else if (this.mSeedType == SeedType.SEED_CATTAIL)
			{
				num = this.mX + 20;
				num2 = this.mY - 3;
			}
			else if (this.mSeedType == SeedType.SEED_KERNELPULT && thePlantWeapon == PlantWeapon.WEAPON_PRIMARY)
			{
				num = this.mX + 19;
				num2 = this.mY - 37;
			}
			else if (this.mSeedType == SeedType.SEED_KERNELPULT && thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
			{
				num = this.mX + 12;
				num2 = this.mY - 56;
			}
			else if (this.mSeedType == SeedType.SEED_PEASHOOTER || this.mSeedType == SeedType.SEED_SNOWPEA || this.mSeedType == SeedType.SEED_REPEATER)
			{
				int num3 = 0;
				int num4 = 0;
				this.GetPeaHeadOffset(ref num3, ref num4);
				num = this.mX + num3 + 24;
				num2 = this.mY + num4 + -33;
			}
			else if (this.mSeedType == SeedType.SEED_LEFTPEATER)
			{
				int num5 = 0;
				int num6 = 0;
				this.GetPeaHeadOffset(ref num5, ref num6);
				num = this.mX - num5 + 27;
				num2 = this.mY + num6 - 33;
			}
			else if (this.mSeedType == SeedType.SEED_GATLINGPEA)
			{
				int num7 = 0;
				int num8 = 0;
				this.GetPeaHeadOffset(ref num7, ref num8);
				num = this.mX + num7 + 34;
				num2 = this.mY + num8 + -33;
			}
			else if (this.mSeedType == SeedType.SEED_SPLITPEA)
			{
				int num9 = 0;
				int num10 = 0;
				this.GetPeaHeadOffset(ref num9, ref num10);
				num2 = this.mY + num10 + -33;
				if (thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
				{
					num = this.mX + num9 - 64;
				}
				else
				{
					num = this.mX + num9 + 24;
				}
			}
			else if (this.mSeedType == SeedType.SEED_THREEPEATER)
			{
				num2 = this.mY + 10;
				num = this.mX + 45;
			}
			else if (this.mSeedType == SeedType.SEED_SCAREDYSHROOM)
			{
				num = this.mX + 29;
				num2 = this.mY + 21;
			}
			else if (this.mSeedType == SeedType.SEED_CACTUS)
			{
				if (thePlantWeapon == PlantWeapon.WEAPON_PRIMARY)
				{
					num = this.mX + 93;
					num2 = this.mY - 50;
				}
				else
				{
					num = this.mX + 70;
					num2 = this.mY + 23;
				}
			}
			else if (this.mSeedType == SeedType.SEED_COBCANNON)
			{
				num = this.mX + Constants.Plant_CobCannon_Projectile_Offset.X;
				num2 = this.mY + Constants.Plant_CobCannon_Projectile_Offset.Y;
			}
			else
			{
				num = this.mX + 10;
				num2 = this.mY + 5;
			}
			Plant flowerPotAt = this.mBoard.GetFlowerPotAt(this.mPlantCol, this.mRow);
			if (flowerPotAt != null)
			{
				num2 -= 5;
			}
			if (this.mSeedType == SeedType.SEED_SNOWPEA)
			{
				int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_LAWN_MOWER, this.mRow, 1);
				this.mApp.AddTodParticle((float)(num + 8), (float)(num2 + 13), aRenderOrder, ParticleEffect.PARTICLE_SNOWPEA_PUFF);
			}
			Projectile projectile = this.mBoard.AddProjectile(num, num2, this.mRenderOrder + -1, theRow, projectileType);
			projectile.mDamageRangeFlags = this.GetDamageRangeFlags(thePlantWeapon);
			if (this.mSeedType == SeedType.SEED_CABBAGEPULT || this.mSeedType == SeedType.SEED_KERNELPULT || this.mSeedType == SeedType.SEED_MELONPULT || this.mSeedType == SeedType.SEED_WINTERMELON)
			{
				float num12;
				float num13;
				if (theTargetZombie != null)
				{
					TRect zombieRect = theTargetZombie.GetZombieRect();
					float num11 = theTargetZombie.ZombieTargetLeadX(50f);
					num12 = num11 - (float)num - 30f;
					num13 = (float)(zombieRect.mY - num2);
					if (theTargetZombie.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_RIDING)
					{
						num12 -= 60f;
					}
					if (theTargetZombie.mZombieType == ZombieType.ZOMBIE_POGO && theTargetZombie.mHasObject)
					{
						num12 -= 60f;
					}
					if (theTargetZombie.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL)
					{
						num12 -= 40f;
					}
					if (theTargetZombie.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						int num14 = this.mBoard.GridToPixelY(8, this.mRow);
						num13 = (float)(num14 - num2);
					}
				}
				else
				{
					num12 = 700f - (float)num;
					num13 = 0f;
				}
				if (num12 < 40f)
				{
					num12 = 40f;
				}
				projectile.mMotionType = ProjectileMotion.MOTION_LOBBED;
				float num15 = 120f;
				projectile.mVelX = num12 / num15;
				projectile.mVelY = 0f;
				projectile.mVelZ = -7f + num13 / num15;
				projectile.mAccZ = 0.115f;
				return;
			}
			if (this.mSeedType == SeedType.SEED_THREEPEATER)
			{
				if (theRow < this.mRow)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_THREEPEATER;
					projectile.mVelY = -3f;
					projectile.mShadowY += 80f;
					return;
				}
				if (theRow > this.mRow)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_THREEPEATER;
					projectile.mVelY = 3f;
					projectile.mShadowY += -80f;
					return;
				}
			}
			else
			{
				if (this.mSeedType == SeedType.SEED_PUFFSHROOM || this.mSeedType == SeedType.SEED_SEASHROOM)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_PUFF;
					return;
				}
				if (this.mSeedType == SeedType.SEED_SPLITPEA && thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_BACKWARDS;
					return;
				}
				if (this.mSeedType == SeedType.SEED_LEFTPEATER)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_BACKWARDS;
					return;
				}
				if (this.mSeedType == SeedType.SEED_CATTAIL)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_HOMING;
					projectile.mVelX = 2f;
					projectile.mTargetZombieID = this.mBoard.ZombieGetID(theTargetZombie);
					return;
				}
				if (this.mSeedType == SeedType.SEED_COBCANNON)
				{
					projectile.mDamageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
					projectile.mMotionType = ProjectileMotion.MOTION_LOBBED;
					projectile.mVelX = 0.001f;
					projectile.mVelY = 0f;
					projectile.mAccZ = 0f;
					projectile.mVelZ = -8f;
					projectile.mCobTargetX = (float)(this.mTargetX - 40);
					projectile.mCobTargetRow = this.mBoard.PixelToGridYKeepOnBoard(this.mTargetX, this.mTargetY);
				}
			}
		}

		public Zombie FindTargetZombie(int theRow, PlantWeapon thePlantWeapon)
		{
			int damageRangeFlags = this.GetDamageRangeFlags(thePlantWeapon);
			TRect plantAttackRect = this.GetPlantAttackRect(thePlantWeapon);
			int num = 0;
			Zombie zombie = null;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie2 = this.mBoard.mZombies[i];
				if (!zombie2.mDead)
				{
					int num2 = zombie2.mRow - theRow;
					if (zombie2.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						num2 = 0;
					}
					if ((zombie2.mHasHead && !zombie2.IsTangleKelpTarget()) || (this.mSeedType != SeedType.SEED_POTATOMINE && this.mSeedType != SeedType.SEED_CHOMPER && this.mSeedType != SeedType.SEED_TANGLEKELP))
					{
						bool flag = false;
						if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT && (this.mSeedType == SeedType.SEED_PEASHOOTER || this.mSeedType == SeedType.SEED_CACTUS || this.mSeedType == SeedType.SEED_REPEATER))
						{
							flag = true;
						}
						if (this.mSeedType != SeedType.SEED_CATTAIL)
						{
							if (this.mSeedType == SeedType.SEED_GLOOMSHROOM)
							{
								if (num2 < -1)
								{
									goto IL_30B;
								}
								if (num2 > 1)
								{
									goto IL_30B;
								}
							}
							else if (flag)
							{
								if (!this.mBoard.mChallenge.CanTargetZombieWithPortals(this, zombie2))
								{
									goto IL_30B;
								}
							}
							else if (num2 != 0)
							{
								goto IL_30B;
							}
						}
						if (zombie2.EffectedByDamage((uint)damageRangeFlags))
						{
							int num3 = 0;
							if (this.mSeedType == SeedType.SEED_CATTAIL)
							{
								num3 = Constants.Board_Offset_AspectRatio_Correction;
							}
							if (this.mSeedType == SeedType.SEED_CHOMPER)
							{
								if (zombie2.mZombiePhase == ZombiePhase.PHASE_DIGGER_WALKING)
								{
									plantAttackRect.mX += 20;
									plantAttackRect.mWidth -= 20;
								}
								if (zombie2.mZombiePhase == ZombiePhase.PHASE_POGO_BOUNCING || (zombie2.mZombieType == ZombieType.ZOMBIE_BUNGEE && zombie2.mTargetCol == this.mPlantCol))
								{
									goto IL_30B;
								}
								if (zombie2.mIsEating || this.mState == PlantState.STATE_CHOMPER_BITING)
								{
									num3 = 60;
								}
							}
							if (this.mSeedType == SeedType.SEED_POTATOMINE)
							{
								if ((zombie2.mZombieType == ZombieType.ZOMBIE_POGO && zombie2.mHasObject) || zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT)
								{
									goto IL_30B;
								}
								if (zombie2.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
								{
									plantAttackRect.mX += 40;
									plantAttackRect.mWidth -= 40;
								}
								if (zombie2.mZombieType == ZombieType.ZOMBIE_BUNGEE && zombie2.mTargetCol != this.mPlantCol)
								{
									goto IL_30B;
								}
								if (zombie2.mIsEating)
								{
									num3 = 30;
								}
							}
							if ((this.mSeedType != SeedType.SEED_EXPLODE_O_NUT || zombie2.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_IN_VAULT) && (this.mSeedType != SeedType.SEED_TANGLEKELP || zombie2.mInPool))
							{
								TRect zombieRect = zombie2.GetZombieRect();
								if (!flag)
								{
									int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
									if (rectOverlap < -num3)
									{
										goto IL_30B;
									}
								}
								int num4 = -zombieRect.mX;
								if (this.mSeedType == SeedType.SEED_CATTAIL)
								{
									num4 = -(int)TodCommon.Distance2D((float)this.mX + 40f, (float)this.mY + 40f, (float)(zombieRect.mX + zombieRect.mWidth / 2), (float)(zombieRect.mY + zombieRect.mHeight / 2));
									if (zombie2.IsFlying())
									{
										num4 += 10000;
									}
								}
								if (zombie == null || num4 > num)
								{
									num = num4;
									zombie = zombie2;
								}
							}
						}
					}
				}
				IL_30B:;
			}
			return zombie;
		}

		public void Die()
		{
			if (this.IsOnBoard() && this.mSeedType == SeedType.SEED_TANGLEKELP)
			{
				Zombie zombie = this.mBoard.ZombieTryToGet(this.mTargetZombieID);
				if (zombie != null)
				{
					zombie.DieWithLoot();
				}
			}
			this.mDead = true;
			this.RemoveEffects();
			if (!Plant.IsFlying(this.mSeedType) && this.IsOnBoard())
			{
				GridItem ladderAt = this.mBoard.GetLadderAt(this.mPlantCol, this.mRow);
				if (ladderAt != null)
				{
					ladderAt.GridItemDie();
				}
			}
			if (this.IsOnBoard())
			{
				Plant topPlantAt = this.mBoard.GetTopPlantAt(this.mPlantCol, this.mRow, PlantPriority.TOPPLANT_BUNGEE_ORDER);
				Plant flowerPotAt = this.mBoard.GetFlowerPotAt(this.mPlantCol, this.mRow);
				if (flowerPotAt != null && topPlantAt == flowerPotAt)
				{
					Reanimation reanimation = this.mApp.ReanimationGet(flowerPotAt.mBodyReanimID);
					reanimation.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
				}
			}
		}

		public void UpdateProductionPlant()
		{
			if (!this.IsInPlay())
			{
				return;
			}
			if (this.mApp.IsIZombieLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_UPSELL || this.mApp.mGameMode == GameMode.GAMEMODE_INTRO)
			{
				return;
			}
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			if (this.mSeedType == SeedType.SEED_MARIGOLD && this.mBoard.mCurrentWave == this.mBoard.mNumWaves)
			{
				if (this.mState != PlantState.STATE_MARIGOLD_ENDING)
				{
					this.mState = PlantState.STATE_MARIGOLD_ENDING;
					this.mStateCountdown = 6000;
				}
				else if (this.mStateCountdown <= 0)
				{
					return;
				}
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && this.mBoard.mChallenge.mChallengeState != ChallengeState.STATECHALLENGE_LAST_STAND_ONSLAUGHT)
			{
				return;
			}
			this.mLaunchCounter -= 3;
			if (this.mLaunchCounter <= 100)
			{
				int num = TodCommon.TodAnimateCurve(100, 0, this.mLaunchCounter, 0, 100, TodCurves.CURVE_LINEAR);
				this.mEatenFlashCountdown = Math.Max(this.mEatenFlashCountdown, num);
			}
			if (this.mLaunchCounter <= 0)
			{
				this.mLaunchCounter = TodCommon.RandRangeInt(this.mLaunchRate - 150, this.mLaunchRate);
				this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
				if (this.mSeedType == SeedType.SEED_SUNSHROOM)
				{
					if (this.mState == PlantState.STATE_SUNSHROOM_SMALL)
					{
						this.mBoard.AddCoin(this.mX, this.mY, CoinType.COIN_SMALLSUN, CoinMotion.COIN_MOTION_FROM_PLANT);
					}
					else
					{
						this.mBoard.AddCoin(this.mX, this.mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
					}
				}
				else if (this.mSeedType == SeedType.SEED_SUNFLOWER)
				{
					this.mBoard.AddCoin(this.mX, this.mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
				}
				else if (this.mSeedType == SeedType.SEED_TWINSUNFLOWER)
				{
					this.mBoard.AddCoin(this.mX, this.mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
					this.mBoard.AddCoin(this.mX, this.mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
				}
				else if (this.mSeedType == SeedType.SEED_MARIGOLD)
				{
					int num2 = RandomNumbers.NextNumber(100);
					CoinType theCoinType = CoinType.COIN_SILVER;
					if (num2 < 10)
					{
						theCoinType = CoinType.COIN_GOLD;
					}
					this.mBoard.AddCoin(this.mX, this.mY, theCoinType, CoinMotion.COIN_MOTION_COIN);
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BIG_TIME)
				{
					if (this.mSeedType == SeedType.SEED_SUNFLOWER)
					{
						this.mBoard.AddCoin(this.mX, this.mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
						return;
					}
					if (this.mSeedType == SeedType.SEED_MARIGOLD)
					{
						this.mBoard.AddCoin(this.mX, this.mY, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
					}
				}
			}
		}

		public void UpdateShooter()
		{
			this.mLaunchCounter--;
			if (this.mLaunchCounter <= 0)
			{
				this.mLaunchCounter = this.mLaunchRate - RandomNumbers.NextNumber(15);
				if (this.mSeedType == SeedType.SEED_THREEPEATER)
				{
					this.LaunchThreepeater();
				}
				else if (this.mSeedType == SeedType.SEED_STARFRUIT)
				{
					this.LaunchStarFruit();
				}
				else if (this.mSeedType == SeedType.SEED_SPLITPEA)
				{
					this.FindTargetAndFire(this.mRow, PlantWeapon.WEAPON_SECONDARY);
					Reanimation reanimation = this.mApp.ReanimationGet(this.mHeadReanimID);
					Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
					reanimation.StartBlend(20);
					reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
					reanimation.mAnimRate = reanimation2.mAnimRate;
					reanimation.mAnimTime = reanimation2.mAnimTime;
				}
				else if (this.mSeedType == SeedType.SEED_CACTUS)
				{
					if (this.mState == PlantState.STATE_CACTUS_HIGH)
					{
						this.FindTargetAndFire(this.mRow, PlantWeapon.WEAPON_PRIMARY);
					}
					else if (this.mState == PlantState.STATE_CACTUS_LOW)
					{
						this.FindTargetAndFire(this.mRow, PlantWeapon.WEAPON_SECONDARY);
					}
				}
				else
				{
					this.FindTargetAndFire(this.mRow, PlantWeapon.WEAPON_PRIMARY);
				}
			}
			if (this.mLaunchCounter == 50 && this.mSeedType == SeedType.SEED_CATTAIL)
			{
				this.FindTargetAndFire(this.mRow, PlantWeapon.WEAPON_PRIMARY);
			}
			if (this.mLaunchCounter == 25)
			{
				if (this.mSeedType == SeedType.SEED_REPEATER || this.mSeedType == SeedType.SEED_LEFTPEATER)
				{
					this.FindTargetAndFire(this.mRow, PlantWeapon.WEAPON_PRIMARY);
					return;
				}
				if (this.mSeedType == SeedType.SEED_SPLITPEA)
				{
					this.FindTargetAndFire(this.mRow, PlantWeapon.WEAPON_PRIMARY);
					this.FindTargetAndFire(this.mRow, PlantWeapon.WEAPON_SECONDARY);
				}
			}
		}

		public bool FindTargetAndFire(int theRow, PlantWeapon thePlantWeapon)
		{
			Zombie zombie = this.FindTargetZombie(theRow, thePlantWeapon);
			if (zombie == null)
			{
				return false;
			}
			this.EndBlink();
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(this.mHeadReanimID);
			if (this.mSeedType == SeedType.SEED_SPLITPEA && thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
			{
				Reanimation reanimation3 = this.mApp.ReanimationGet(this.mHeadReanimID2);
				reanimation3.StartBlend(20);
				reanimation3.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
				reanimation3.mAnimRate = 35f;
				reanimation3.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_shooting);
				this.mShootingCounter = 26;
			}
			else if (reanimation2 != null && reanimation2.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
			{
				reanimation2.StartBlend(20);
				reanimation2.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
				reanimation2.mAnimRate = 35f;
				reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting);
				this.mShootingCounter = 33;
				if (this.mSeedType == SeedType.SEED_REPEATER || this.mSeedType == SeedType.SEED_SPLITPEA || this.mSeedType == SeedType.SEED_LEFTPEATER)
				{
					reanimation2.mAnimRate = 45f;
					this.mShootingCounter = 26;
				}
				else if (this.mSeedType == SeedType.SEED_GATLINGPEA)
				{
					reanimation2.mAnimRate = 38f;
					this.mShootingCounter = 100;
				}
			}
			else if (this.mState == PlantState.STATE_CACTUS_HIGH)
			{
				this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shootinghigh, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 35f);
				this.mShootingCounter = 23;
			}
			else if (this.mSeedType == SeedType.SEED_GLOOMSHROOM)
			{
				this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 14f);
				this.mShootingCounter = 200;
			}
			else if (this.mSeedType == SeedType.SEED_CATTAIL)
			{
				this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 30f);
				this.mShootingCounter = 50;
			}
			else if (reanimation != null && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
			{
				this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 35f);
				if (this.mSeedType == SeedType.SEED_FUMESHROOM)
				{
					this.mShootingCounter = 50;
				}
				else if (this.mSeedType == SeedType.SEED_PUFFSHROOM)
				{
					this.mShootingCounter = 29;
				}
				else if (this.mSeedType == SeedType.SEED_SCAREDYSHROOM)
				{
					this.mShootingCounter = 25;
				}
				else if (this.mSeedType == SeedType.SEED_CABBAGEPULT)
				{
					this.mShootingCounter = 32;
				}
				else if (this.mSeedType == SeedType.SEED_MELONPULT || this.mSeedType == SeedType.SEED_WINTERMELON)
				{
					this.mShootingCounter = 36;
				}
				else if (this.mSeedType == SeedType.SEED_KERNELPULT)
				{
					if (RandomNumbers.NextNumber(4) == 0)
					{
						reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
						reanimation.AssignRenderGroupToPrefix("Cornpult_butter", 0);
						reanimation.AssignRenderGroupToPrefix("Cornpult_kernal", -1);
						this.mState = PlantState.STATE_KERNELPULT_BUTTER;
					}
					this.mShootingCounter = 30;
				}
				else if (this.mSeedType == SeedType.SEED_CACTUS)
				{
					this.mShootingCounter = 35;
				}
				else
				{
					this.mShootingCounter = 29;
				}
			}
			else
			{
				this.Fire(zombie, theRow, thePlantWeapon);
			}
			return true;
		}

		public void LaunchThreepeater()
		{
			int theRow = this.mRow - 1;
			int theRow2 = this.mRow + 1;
			bool flag = false;
			if (this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY) != null)
			{
				flag = true;
			}
			else if (this.mBoard.RowCanHaveZombies(theRow) && this.FindTargetZombie(theRow, PlantWeapon.WEAPON_PRIMARY) != null)
			{
				flag = true;
			}
			else if (this.mBoard.RowCanHaveZombies(theRow2) && this.FindTargetZombie(theRow2, PlantWeapon.WEAPON_PRIMARY) != null)
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mHeadReanimID);
			Reanimation reanimation2 = this.mApp.ReanimationGet(this.mHeadReanimID2);
			Reanimation reanimation3 = this.mApp.ReanimationGet(this.mHeadReanimID3);
			if (this.mBoard.RowCanHaveZombies(theRow2))
			{
				reanimation.StartBlend(10);
				reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
				reanimation.mAnimRate = 20f;
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting1);
			}
			reanimation2.StartBlend(10);
			reanimation2.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
			reanimation2.mAnimRate = 20f;
			reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting2);
			if (this.mBoard.RowCanHaveZombies(theRow))
			{
				reanimation3.StartBlend(10);
				reanimation3.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
				reanimation3.mAnimRate = 20f;
				reanimation3.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting3);
			}
			this.mShootingCounter = 35;
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
			if (GlobalStaticVars.gLawnApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || GlobalStaticVars.gLawnApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				if (theSeedType == SeedType.SEED_REPEATER)
				{
					return 1000;
				}
				if (theSeedType == SeedType.SEED_FUMESHROOM)
				{
					return 500;
				}
				if (theSeedType == SeedType.SEED_TALLNUT)
				{
					return 250;
				}
				if (theSeedType == SeedType.SEED_BEGHOULED_BUTTON_SHUFFLE)
				{
					return 100;
				}
				if (theSeedType == SeedType.SEED_BEGHOULED_BUTTON_CRATER)
				{
					return 200;
				}
			}
			switch (theSeedType)
			{
			case SeedType.SEED_SLOT_MACHINE_SUN:
				return 0;
			case SeedType.SEED_SLOT_MACHINE_DIAMOND:
				return 0;
			case SeedType.SEED_ZOMBIQUARIUM_SNORKEL:
				return 100;
			case SeedType.SEED_ZOMBIQUARIUM_TROPHY:
				return 1000;
			case SeedType.SEED_ZOMBIE_NORMAL:
				return 50;
			case SeedType.SEED_ZOMBIE_TRAFFIC_CONE:
				return 75;
			case SeedType.SEED_ZOMBIE_POLEVAULTER:
				return 75;
			case SeedType.SEED_ZOMBIE_PAIL:
				return 125;
			case SeedType.SEED_ZOMBIE_LADDER:
				return 150;
			case SeedType.SEED_ZOMBIE_DIGGER:
				return 125;
			case SeedType.SEED_ZOMBIE_BUNGEE:
				return 125;
			case SeedType.SEED_ZOMBIE_FOOTBALL:
				return 175;
			case SeedType.SEED_ZOMBIE_BALLOON:
				return 150;
			case SeedType.SEED_ZOMBIE_SCREEN_DOOR:
				return 100;
			case SeedType.SEED_ZOMBONI:
				return 175;
			case SeedType.SEED_ZOMBIE_POGO:
				return 200;
			case SeedType.SEED_ZOMBIE_DANCER:
				return 350;
			case SeedType.SEED_ZOMBIE_GARGANTUAR:
				return 300;
			case SeedType.SEED_ZOMBIE_IMP:
				return 50;
			default:
			{
				if (theSeedType == SeedType.SEED_IMITATER && theImitaterType != SeedType.SEED_NONE)
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
			if (theSeedtype == SeedType.SEED_IMITATER && theImitaterType != SeedType.SEED_NONE)
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
			if (theSeedType == SeedType.SEED_IMITATER && theImitaterType != SeedType.SEED_NONE)
			{
				PlantDefinition plantDefinition = Plant.GetPlantDefinition(theImitaterType);
				return plantDefinition.mRefreshTime;
			}
			PlantDefinition plantDefinition2 = Plant.GetPlantDefinition(theSeedType);
			return plantDefinition2.mRefreshTime;
		}

		public static bool IsNocturnal(SeedType theSeedtype)
		{
			return theSeedtype == SeedType.SEED_PUFFSHROOM || theSeedtype == SeedType.SEED_SEASHROOM || theSeedtype == SeedType.SEED_SUNSHROOM || theSeedtype == SeedType.SEED_FUMESHROOM || theSeedtype == SeedType.SEED_HYPNOSHROOM || theSeedtype == SeedType.SEED_DOOMSHROOM || theSeedtype == SeedType.SEED_ICESHROOM || theSeedtype == SeedType.SEED_MAGNETSHROOM || theSeedtype == SeedType.SEED_SCAREDYSHROOM || theSeedtype == SeedType.SEED_GLOOMSHROOM;
		}

		public static bool IsAquatic(SeedType theSeedType)
		{
			return theSeedType == SeedType.SEED_LILYPAD || theSeedType == SeedType.SEED_TANGLEKELP || theSeedType == SeedType.SEED_SEASHROOM || theSeedType == SeedType.SEED_CATTAIL;
		}

		public static bool IsFlying(SeedType theSeedtype)
		{
			return theSeedtype == SeedType.SEED_INSTANT_COFFEE;
		}

		public static bool IsUpgrade(SeedType theSeedtype)
		{
			return theSeedtype == SeedType.SEED_GATLINGPEA || theSeedtype == SeedType.SEED_WINTERMELON || theSeedtype == SeedType.SEED_TWINSUNFLOWER || theSeedtype == SeedType.SEED_SPIKEROCK || theSeedtype == SeedType.SEED_COBCANNON || theSeedtype == SeedType.SEED_GOLD_MAGNET || theSeedtype == SeedType.SEED_GLOOMSHROOM || theSeedtype == SeedType.SEED_CATTAIL;
		}

		public void UpdateAbilities()
		{
			if (!this.IsInPlay())
			{
				return;
			}
			if (this.mState == PlantState.STATE_DOINGSPECIAL || this.mSquished)
			{
				this.mDisappearCountdown -= 3;
				if (this.mDisappearCountdown < 0)
				{
					this.Die();
					return;
				}
			}
			if (this.mWakeUpCounter > 0)
			{
				this.mWakeUpCounter -= 3;
				if (this.mWakeUpCounter >= 60 && this.mWakeUpCounter < 63)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_WAKEUP);
				}
				if (this.mWakeUpCounter >= 0 && this.mWakeUpCounter < 3)
				{
					this.SetSleeping(false);
				}
			}
			if (this.mIsAsleep || this.mSquished || this.mOnBungeeState != PlantOnBungeeState.PLANT_NOT_ON_BUNGEE)
			{
				return;
			}
			this.UpdateShooting();
			if (this.mStateCountdown > 0)
			{
				this.mStateCountdown -= 3;
			}
			if (this.mApp.IsWallnutBowlingLevel())
			{
				this.UpdateBowling();
				this.UpdateBowling();
				this.UpdateBowling();
				return;
			}
			if (this.mSeedType == SeedType.SEED_SQUASH)
			{
				this.UpdateSquash();
			}
			else if (this.mSeedType == SeedType.SEED_DOOMSHROOM)
			{
				this.UpdateDoomShroom();
			}
			else if (this.mSeedType == SeedType.SEED_ICESHROOM)
			{
				this.UpdateIceShroom();
			}
			else if (this.mSeedType == SeedType.SEED_CHOMPER)
			{
				this.UpdateChomper();
			}
			else if (this.mSeedType == SeedType.SEED_BLOVER)
			{
				this.UpdateBlover();
			}
			else if (this.mSeedType == SeedType.SEED_FLOWERPOT)
			{
				this.UpdateFlowerPot();
			}
			else if (this.mSeedType == SeedType.SEED_LILYPAD)
			{
				this.UpdateLilypad();
			}
			else if (this.mSeedType == SeedType.SEED_IMITATER)
			{
				this.UpdateImitater();
			}
			else if (this.mSeedType == SeedType.SEED_INSTANT_COFFEE)
			{
				this.UpdateCoffeeBean();
			}
			else if (this.mSeedType == SeedType.SEED_UMBRELLA)
			{
				this.UpdateUmbrella();
			}
			else if (this.mSeedType == SeedType.SEED_COBCANNON)
			{
				this.UpdateCobCannon();
			}
			else if (this.mSeedType == SeedType.SEED_CACTUS)
			{
				this.UpdateCactus();
			}
			else if (this.mSeedType == SeedType.SEED_MAGNETSHROOM)
			{
				this.UpdateMagnetShroom();
			}
			else if (this.mSeedType == SeedType.SEED_GOLD_MAGNET)
			{
				this.UpdateGoldMagnetShroom();
			}
			else if (this.mSeedType == SeedType.SEED_SUNSHROOM)
			{
				this.UpdateSunShroom();
			}
			else if (this.MakesSun() || this.mSeedType == SeedType.SEED_MARIGOLD)
			{
				this.UpdateProductionPlant();
			}
			else if (this.mSeedType == SeedType.SEED_GRAVEBUSTER)
			{
				this.UpdateGraveBuster();
			}
			else if (this.mSeedType == SeedType.SEED_TORCHWOOD)
			{
				this.UpdateTorchwood();
			}
			else if (this.mSeedType == SeedType.SEED_POTATOMINE)
			{
				this.UpdatePotato();
			}
			else if (this.mSeedType == SeedType.SEED_SPIKEWEED || this.mSeedType == SeedType.SEED_SPIKEROCK)
			{
				this.UpdateSpikeweed();
			}
			else if (this.mSeedType == SeedType.SEED_TANGLEKELP)
			{
				this.UpdateTanglekelp();
			}
			else if (this.mSeedType == SeedType.SEED_SCAREDYSHROOM)
			{
				this.UpdateScaredyShroom();
			}
			if (this.mSubclass == 1)
			{
				this.UpdateShooter();
				this.UpdateShooter();
				this.UpdateShooter();
			}
			if (this.mDoSpecialCountdown > 0)
			{
				this.mDoSpecialCountdown -= 3;
				if (this.mDoSpecialCountdown <= 0)
				{
					this.DoSpecial();
				}
			}
		}

		public void Squish()
		{
			if (this.NotOnGround())
			{
				return;
			}
			if (!this.mIsAsleep)
			{
				if (this.mSeedType == SeedType.SEED_CHERRYBOMB || this.mSeedType == SeedType.SEED_JALAPENO || this.mSeedType == SeedType.SEED_DOOMSHROOM || this.mSeedType == SeedType.SEED_ICESHROOM)
				{
					this.DoSpecial();
					return;
				}
				if (this.mSeedType == SeedType.SEED_POTATOMINE && this.mState != PlantState.STATE_NOTREADY)
				{
					this.DoSpecial();
					return;
				}
			}
			if (this.mSeedType == SeedType.SEED_SQUASH && this.mState != PlantState.STATE_NOTREADY)
			{
				return;
			}
			this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, this.mRow, 8);
			if (this.mSeedType == SeedType.SEED_FLOWERPOT)
			{
				this.mRenderOrder--;
			}
			this.mSquished = true;
			this.mDisappearCountdown = 500;
			this.mApp.PlayFoley(FoleyType.FOLEY_SQUISH);
			this.RemoveEffects();
			GridItem ladderAt = this.mBoard.GetLadderAt(this.mPlantCol, this.mRow);
			if (ladderAt != null)
			{
				ladderAt.GridItemDie();
			}
			if (this.mApp.IsIZombieLevel())
			{
				this.mBoard.mChallenge.IZombiePlantDropRemainingSun(this);
			}
		}

		public void DoRowAreaDamage(int theDamage, uint theDamageFlags)
		{
			int damageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			TRect plantAttackRect = this.GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead)
				{
					int num = zombie.mRow - this.mRow;
					if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						num = 0;
					}
					if (this.mSeedType == SeedType.SEED_GLOOMSHROOM)
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
					if (zombie.mOnHighGround == this.IsOnHighGround() && zombie.EffectedByDamage((uint)damageRangeFlags))
					{
						TRect zombieRect = zombie.GetZombieRect();
						int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
						if (rectOverlap > 0)
						{
							int theDamage2 = theDamage;
							if ((zombie.mZombieType == ZombieType.ZOMBIE_ZAMBONI || zombie.mZombieType == ZombieType.ZOMBIE_CATAPULT) && TodCommon.TestBit(theDamageFlags, 5))
							{
								theDamage2 = 1800;
								if (this.mSeedType == SeedType.SEED_SPIKEROCK)
								{
									this.SpikeRockTakeDamage();
								}
								else
								{
									this.Die();
								}
							}
							zombie.TakeDamage(theDamage2, theDamageFlags);
							this.mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
						}
					}
				}
				IL_108:;
			}
		}

		public int GetDamageRangeFlags(PlantWeapon thePlantWeapon)
		{
			if (this.mSeedType == SeedType.SEED_CACTUS)
			{
				if (thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
				{
					return 1;
				}
				return 2;
			}
			else
			{
				if (this.mSeedType == SeedType.SEED_CHERRYBOMB || this.mSeedType == SeedType.SEED_JALAPENO || this.mSeedType == SeedType.SEED_COBCANNON || this.mSeedType == SeedType.SEED_DOOMSHROOM)
				{
					return 127;
				}
				if (this.mSeedType == SeedType.SEED_MELONPULT || this.mSeedType == SeedType.SEED_CABBAGEPULT || this.mSeedType == SeedType.SEED_KERNELPULT || this.mSeedType == SeedType.SEED_WINTERMELON)
				{
					return 13;
				}
				if (this.mSeedType == SeedType.SEED_POTATOMINE)
				{
					return 77;
				}
				if (this.mSeedType == SeedType.SEED_SQUASH)
				{
					return 13;
				}
				if (this.mSeedType == SeedType.SEED_PUFFSHROOM || this.mSeedType == SeedType.SEED_SEASHROOM || this.mSeedType == SeedType.SEED_FUMESHROOM || this.mSeedType == SeedType.SEED_GLOOMSHROOM || this.mSeedType == SeedType.SEED_CHOMPER)
				{
					return 9;
				}
				if (this.mSeedType == SeedType.SEED_CATTAIL)
				{
					return 11;
				}
				if (this.mSeedType == SeedType.SEED_TANGLEKELP)
				{
					return 5;
				}
				if (this.mSeedType == SeedType.SEED_GIANT_WALLNUT)
				{
					return 17;
				}
				return 1;
			}
		}

		public TRect GetPlantRect()
		{
			TRect result = default(TRect);
			if (this.mSeedType == SeedType.SEED_TALLNUT)
			{
				result = new TRect(this.mX + 10, this.mY, this.mWidth, this.mHeight);
			}
			else if (this.mSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				result = new TRect(this.mX, this.mY, this.mWidth - 20, this.mHeight);
			}
			else if (this.mSeedType == SeedType.SEED_COBCANNON)
			{
				result = new TRect(this.mX, this.mY, 140, 80);
			}
			else
			{
				result = new TRect(this.mX + 10, this.mY, this.mWidth - 20, this.mHeight);
			}
			return result;
		}

		public TRect GetPlantAttackRect(PlantWeapon thePlantWeapon)
		{
			TRect result = default(TRect);
			if (this.mApp.IsWallnutBowlingLevel())
			{
				result = new TRect(this.mX, this.mY, this.mWidth - 20, this.mHeight);
			}
			else if (thePlantWeapon == PlantWeapon.WEAPON_SECONDARY && this.mSeedType == SeedType.SEED_SPLITPEA)
			{
				result = new TRect(0, this.mY, this.mX + 16, this.mHeight);
			}
			else
			{
				SeedType seedType = this.mSeedType;
				if (seedType <= SeedType.SEED_SEASHROOM)
				{
					switch (seedType)
					{
					case SeedType.SEED_POTATOMINE:
						result = new TRect(this.mX, this.mY, this.mWidth - 25, this.mHeight);
						return result;
					case SeedType.SEED_SNOWPEA:
					case SeedType.SEED_REPEATER:
					case SeedType.SEED_SUNSHROOM:
						goto IL_27E;
					case SeedType.SEED_CHOMPER:
						result = new TRect(this.mX + 80, this.mY, 40, this.mHeight);
						return result;
					case SeedType.SEED_PUFFSHROOM:
						break;
					case SeedType.SEED_FUMESHROOM:
						result = new TRect(this.mX + 60, this.mY, 340, this.mHeight);
						return result;
					default:
						switch (seedType)
						{
						case SeedType.SEED_SQUASH:
							result = new TRect(this.mX + 20, this.mY, this.mWidth - 35, this.mHeight);
							return result;
						case SeedType.SEED_THREEPEATER:
						case SeedType.SEED_JALAPENO:
						case SeedType.SEED_TALLNUT:
							goto IL_27E;
						case SeedType.SEED_TANGLEKELP:
							result = new TRect(this.mX, this.mY, this.mWidth, this.mHeight);
							return result;
						case SeedType.SEED_SPIKEWEED:
							goto IL_15B;
						case SeedType.SEED_TORCHWOOD:
							result = new TRect(this.mX + 50, this.mY, 30, this.mHeight);
							return result;
						case SeedType.SEED_SEASHROOM:
							break;
						default:
							goto IL_27E;
						}
						break;
					}
					result = new TRect(this.mX + 60, this.mY, 230, this.mHeight);
					return result;
				}
				switch (seedType)
				{
				case SeedType.SEED_GLOOMSHROOM:
					result = new TRect(this.mX - 80, this.mY - 80, 240, 240);
					return result;
				case SeedType.SEED_CATTAIL:
					result = new TRect(-800, -600, 1600, 1200);
					return result;
				case SeedType.SEED_WINTERMELON:
				case SeedType.SEED_GOLD_MAGNET:
					goto IL_27E;
				case SeedType.SEED_SPIKEROCK:
					break;
				default:
					if (seedType == SeedType.SEED_LEFTPEATER)
					{
						result = new TRect(0, this.mY, this.mX, this.mHeight);
						return result;
					}
					goto IL_27E;
				}
				IL_15B:
				result = new TRect(this.mX + 20, this.mY, this.mWidth - 50, this.mHeight);
				return result;
				IL_27E:
				result = new TRect(this.mX + 60, this.mY, 800, this.mHeight);
			}
			return result;
		}

		public Zombie FindSquashTarget()
		{
			int damageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			TRect plantAttackRect = this.GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
			int num = 0;
			Zombie zombie = null;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie2 = this.mBoard.mZombies[i];
				if (!zombie2.mDead)
				{
					int num2 = zombie2.mRow - this.mRow;
					if (zombie2.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						num2 = 0;
					}
					if (num2 == 0 && zombie2.mHasHead && !zombie2.IsTangleKelpTarget() && zombie2.EffectedByDamage((uint)damageRangeFlags) && !zombie2.IsSquashTarget(this))
					{
						TRect zombieRect = zombie2.GetZombieRect();
						if ((zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT && zombieRect.mX < this.mX + 20) || (zombie2.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT && zombie2.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_IN_VAULT && zombie2.mZombiePhase != ZombiePhase.PHASE_SNORKEL_INTO_POOL && zombie2.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_INTO_POOL && zombie2.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_RIDING && zombie2.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_IN_JUMP && !zombie2.IsBobsledTeamWithSled()))
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
								if (zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_POST_VAULT || zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT || zombie2.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING_IN_POOL || zombie2.mZombieType == ZombieType.ZOMBIE_IMP || zombie2.mZombieType == ZombieType.ZOMBIE_FOOTBALL || this.mApp.IsScaryPotterLevel())
								{
									num5 = plantAttackRect.mX - 60;
								}
								if (zombie2.IsWalkingBackwards() || zombieRect.mX + zombieRect.mWidth >= num5)
								{
									if (this.mBoard.ZombieGetID(zombie2) == this.mTargetZombieID)
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

		public void UpdateSquash()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			Debug.ASSERT(reanimation != null);
			if (this.mState == PlantState.STATE_NOTREADY)
			{
				Zombie zombie = this.FindSquashTarget();
				if (zombie == null)
				{
					return;
				}
				this.mTargetZombieID = this.mBoard.ZombieGetID(zombie);
				this.mTargetX = (int)zombie.ZombieTargetLeadX(0f) - this.mWidth / 2;
				this.mState = PlantState.STATE_SQUASH_LOOK;
				this.mStateCountdown = 80;
				if (this.mTargetX < this.mX)
				{
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lookleft, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 24f);
				}
				else
				{
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lookright, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 24f);
				}
				this.mApp.PlayFoley(FoleyType.FOLEY_SQUASH_HMM);
				return;
			}
			else
			{
				if (this.mState == PlantState.STATE_SQUASH_LOOK)
				{
					if (this.mStateCountdown <= 0)
					{
						this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpup, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
						this.mState = PlantState.STATE_SQUASH_PRE_LAUNCH;
						this.mStateCountdown = 30;
					}
					return;
				}
				if (this.mState == PlantState.STATE_SQUASH_PRE_LAUNCH)
				{
					if (this.mStateCountdown <= 0)
					{
						Zombie zombie2 = this.FindSquashTarget();
						if (zombie2 != null)
						{
							this.mTargetX = (int)zombie2.ZombieTargetLeadX(30f) - this.mWidth / 2;
						}
						this.mState = PlantState.STATE_SQUASH_RISING;
						this.mStateCountdown = 50;
						this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, this.mRow, 0);
					}
					return;
				}
				int theGridX = this.mBoard.PixelToGridXKeepOnBoard(this.mTargetX, this.mY);
				int thePositionEnd = this.mTargetX;
				int num = this.mBoard.GridToPixelY(theGridX, this.mRow) + 8;
				if (this.mState == PlantState.STATE_SQUASH_RISING)
				{
					int thePositionStart = this.mBoard.GridToPixelX(this.mPlantCol, this.mStartRow);
					int thePositionStart2 = this.mBoard.GridToPixelY(this.mPlantCol, this.mStartRow);
					this.mX = TodCommon.TodAnimateCurve(50, 20, this.mStateCountdown, thePositionStart, thePositionEnd, TodCurves.CURVE_EASE_IN_OUT);
					this.mY = TodCommon.TodAnimateCurve(50, 20, this.mStateCountdown, thePositionStart2, num - 120, TodCurves.CURVE_EASE_IN_OUT);
					if (this.mStateCountdown <= 0)
					{
						this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpdown, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 60f);
						this.mState = PlantState.STATE_SQUASH_FALLING;
						this.mStateCountdown = 10;
						return;
					}
				}
				else if (this.mState == PlantState.STATE_SQUASH_FALLING)
				{
					this.mY = TodCommon.TodAnimateCurve(10, 0, this.mStateCountdown, num - 120, num, TodCurves.CURVE_LINEAR);
					if (this.mStateCountdown == 4)
					{
						this.DoSquashDamage();
					}
					if (this.mStateCountdown <= 0)
					{
						if (this.mBoard.IsPoolSquare(theGridX, this.mRow))
						{
							this.mApp.AddReanimation((float)(this.mX - 11), (float)(this.mY + 20), this.mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
							this.mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
							this.mApp.PlaySample(Resources.SOUND_ZOMBIESPLASH);
							this.Die();
							return;
						}
						this.mState = PlantState.STATE_SQUASH_DONE_FALLING;
						this.mStateCountdown = 100;
						this.mBoard.ShakeBoard(1, 4);
						this.mApp.PlayFoley(FoleyType.FOLEY_THUMP);
						float num2 = 80f;
						if (this.mBoard.StageHasRoof())
						{
							num2 -= 11f;
						}
						this.mApp.AddTodParticle((float)(this.mX + 40), (float)this.mY + num2, this.mRenderOrder + 4, ParticleEffect.PARTICLE_DUST_SQUASH);
						return;
					}
				}
				else if (this.mState == PlantState.STATE_SQUASH_DONE_FALLING && this.mStateCountdown <= 0)
				{
					this.Die();
				}
				return;
			}
		}

		public bool NotOnGround()
		{
			return (this.mSeedType == SeedType.SEED_SQUASH && (this.mState == PlantState.STATE_SQUASH_RISING || this.mState == PlantState.STATE_SQUASH_FALLING || this.mState == PlantState.STATE_SQUASH_DONE_FALLING)) || this.mSquished || this.mOnBungeeState == PlantOnBungeeState.PLANT_RISING_WITH_BUNGEE || this.mDead;
		}

		public void DoSquashDamage()
		{
			int damageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			TRect plantAttackRect = this.GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
			int num = 0;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead)
				{
					int num2 = zombie.mRow - this.mRow;
					if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						num2 = 0;
					}
					if (num2 == 0 && zombie.EffectedByDamage((uint)damageRangeFlags))
					{
						TRect zombieRect = zombie.GetZombieRect();
						int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, zombieRect);
						int num3 = 0;
						if (zombie.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
						{
							num3 = -20;
						}
						if (rectOverlap > num3)
						{
							zombie.TakeDamage(1800, 18U);
							num++;
						}
					}
				}
			}
		}

		public void BurnRow(int theRow)
		{
			int damageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead)
				{
					int num = zombie.mRow - this.mRow;
					if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						num = 0;
					}
					if (num == 0 && zombie.EffectedByDamage((uint)damageRangeFlags))
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
			Zombie bossZombie = this.mBoard.GetBossZombie();
			if (bossZombie != null)
			{
				bossZombie.BossDestroyIceballInRow(theRow);
			}
		}

		public void IceZombies()
		{
			int num = 0;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && zombie.HitIceTrap())
				{
					num++;
				}
			}
			this.mBoard.mIceTrapCounter = 300;
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mBoard.mPoolSparklyParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.mDontUpdate = true;
			}
			Zombie bossZombie = this.mBoard.GetBossZombie();
			if (bossZombie != null)
			{
				bossZombie.BossDestroyFireball();
			}
		}

		public void BlowAwayFliers(int theX, int theRow)
		{
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && !zombie.IsDeadOrDying())
				{
					zombie.GetZombieRect();
					if (zombie.IsFlying() && zombie.mZombiePhase != ZombiePhase.PHASE_BALLOON_POPPING)
					{
						zombie.mBlowingAway = true;
					}
				}
			}
			this.mApp.PlaySample(Resources.SOUND_BLOVER);
			this.mBoard.mFogBlownCountDown = 4000;
		}

		public void UpdateGraveBuster()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			if (this.mState == PlantState.STATE_GRAVEBUSTER_LANDING)
			{
				if (reanimation.mLoopCount > 0)
				{
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 10, 12f);
					this.mStateCountdown = 400;
					this.mState = PlantState.STATE_GRAVEBUSTER_EATING;
					this.AddAttachedParticle(this.mX + 40, this.mY + 40, this.mRenderOrder + 4, ParticleEffect.PARTICLE_GRAVE_BUSTER);
					return;
				}
			}
			else if (this.mState == PlantState.STATE_GRAVEBUSTER_EATING && this.mStateCountdown <= 0)
			{
				GridItem graveStoneAt = this.mBoard.GetGraveStoneAt(this.mPlantCol, this.mRow);
				if (graveStoneAt != null)
				{
					graveStoneAt.GridItemDie();
					this.mBoard.mGravesCleared++;
				}
				this.mApp.AddTodParticle((float)(this.mX + 40), (float)(this.mY + 40), this.mRenderOrder + 4, ParticleEffect.PARTICLE_GRAVE_BUSTER_DIE);
				this.Die();
				this.mBoard.DropLootPiece(this.mX + 40, this.mY, 12);
			}
		}

		public TodParticleSystem AddAttachedParticle(int thePosX, int thePosY, int theRenderPostition, ParticleEffect theEffect)
		{
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.ParticleSystemDie();
			}
			TodParticleSystem todParticleSystem2 = this.mApp.AddTodParticle((float)thePosX, (float)thePosY, theRenderPostition, theEffect);
			if (todParticleSystem2 != null)
			{
				this.mParticleID = this.mApp.ParticleGetID(todParticleSystem2);
			}
			return todParticleSystem2;
		}

		public void GetPeaHeadOffset(ref int theOffsetX, ref int theOffsetY)
		{
			if (!this.mBodyReanimID.mActive)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
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
			return this.mSeedType == SeedType.SEED_SUNFLOWER || this.mSeedType == SeedType.SEED_TWINSUNFLOWER || this.mSeedType == SeedType.SEED_SUNSHROOM;
		}

		public static void DrawSeedType(Graphics g, SeedType theSeedType, SeedType theImitaterType, DrawVariation theDrawVariation, float thePosX, float thePosY)
		{
			SeedType theSeedType2 = theSeedType;
			if (theSeedType == SeedType.SEED_IMITATER && theImitaterType != SeedType.SEED_NONE)
			{
				theSeedType2 = theImitaterType;
			}
			if (Challenge.IsZombieSeedType(theSeedType2))
			{
				ZombieType theZombieType = Challenge.IZombieSeedTypeToZombieType(theSeedType2);
				GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedZombie(g, thePosX, thePosY, theZombieType);
				return;
			}
			GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedPlant(g, thePosX, thePosY, theSeedType2, DrawVariation.VARIATION_NORMAL);
		}

		public void KillAllPlantsNearDoom()
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant.mRow == this.mRow && plant.mPlantCol - this.mPlantCol == 0)
				{
					plant.Die();
				}
			}
		}

		public bool IsOnHighGround()
		{
			return this.mBoard != null && this.mBoard.mGridSquareType[this.mPlantCol, this.mRow] == GridSquareType.GRIDSQUARE_HIGH_GROUND;
		}

		public void UpdateTorchwood()
		{
			TRect plantAttackRect = this.GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
			int num = -1;
			Projectile projectile = null;
			while (this.mBoard.IterateProjectiles(ref projectile, ref num))
			{
				if (projectile.mRow == this.mRow && (projectile.mProjectileType == ProjectileType.PROJECTILE_PEA || projectile.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA))
				{
					TRect projectileRect = projectile.GetProjectileRect();
					int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, projectileRect);
					if (rectOverlap >= 1)
					{
						if (projectile.mProjectileType == ProjectileType.PROJECTILE_PEA)
						{
							projectile.ConvertToFireball(this.mPlantCol);
						}
						else if (projectile.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
						{
							projectile.ConvertToPea(this.mPlantCol);
						}
					}
				}
			}
		}

		public void LaunchStarFruit()
		{
			if (!this.FindStarFruitTarget())
			{
				return;
			}
			this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shoot, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 28f);
			this.mShootingCounter = 40;
		}

		public bool FindStarFruitTarget()
		{
			if (this.mRecentlyEatenCountdown > 0)
			{
				return true;
			}
			int damageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			int num = this.mX + 40;
			int num2 = this.mY + 40;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead)
				{
					TRect zombieRect = zombie.GetZombieRect();
					if (zombie.EffectedByDamage((uint)damageRangeFlags))
					{
						if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS && this.mPlantCol >= 5)
						{
							return true;
						}
						if (zombie.mRow == this.mRow)
						{
							if (zombieRect.mX + zombieRect.mWidth < num)
							{
								return true;
							}
						}
						else
						{
							if (zombie.mZombieType == ZombieType.ZOMBIE_DIGGER)
							{
								zombieRect.mWidth += 10;
							}
							float theTime = TodCommon.Distance2D((float)num, (float)num2, (float)(zombieRect.mX + zombieRect.mWidth / 2), (float)(zombieRect.mY + zombieRect.mHeight / 2)) / 3.33f;
							int num3 = (int)(zombie.ZombieTargetLeadX(theTime) - (float)(zombieRect.mWidth / 2));
							if (num3 + zombieRect.mWidth > num && num3 < num)
							{
								return true;
							}
							int num4 = num3 + zombieRect.mWidth / 2;
							int num5 = zombieRect.mY + zombieRect.mHeight / 2;
							float num6 = TodCommon.RadToDeg((float)Math.Atan2((double)((float)(num5 - num2)), (double)((float)(num4 - num))));
							if (Math.Abs(zombie.mRow - this.mRow) < 2)
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

		public void UpdateChomper()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (this.mState == PlantState.STATE_READY)
			{
				Zombie zombie = this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_bite, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
					this.mState = PlantState.STATE_CHOMPER_BITING;
					this.mStateCountdown = 70;
					return;
				}
			}
			else if (this.mState == PlantState.STATE_CHOMPER_BITING)
			{
				if (this.mStateCountdown <= 0)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_BIGCHOMP);
					Zombie zombie2 = this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY);
					bool flag = false;
					if (zombie2 != null && (zombie2.mZombieType == ZombieType.ZOMBIE_GARGANTUAR || zombie2.mZombieType == ZombieType.ZOMBIE_REDEYE_GARGANTUAR || zombie2.mZombieType == ZombieType.ZOMBIE_BOSS))
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
						else if (zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT)
						{
							flag2 = true;
						}
					}
					if (flag)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
						zombie2.TakeDamage(40, 0U);
						this.mState = PlantState.STATE_CHOMPER_BITING_MISSED;
						return;
					}
					if (flag2)
					{
						this.mState = PlantState.STATE_CHOMPER_BITING_MISSED;
						return;
					}
					zombie2.DieWithLoot();
					this.mState = PlantState.STATE_CHOMPER_BITING_GOT_ONE;
					return;
				}
			}
			else if (this.mState == PlantState.STATE_CHOMPER_BITING_GOT_ONE)
			{
				if (reanimation.mLoopCount > 0)
				{
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_chew, ReanimLoopType.REANIM_LOOP, 0, 15f);
					if (this.mApp.IsIZombieLevel())
					{
						reanimation.mAnimRate = 0f;
					}
					this.mState = PlantState.STATE_CHOMPER_DIGESTING;
					this.mStateCountdown = 4000;
					return;
				}
			}
			else if (this.mState == PlantState.STATE_CHOMPER_DIGESTING)
			{
				if (this.mStateCountdown <= 0)
				{
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_swallow, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
					this.mState = PlantState.STATE_CHOMPER_SWALLOWING;
					return;
				}
			}
			else if ((this.mState == PlantState.STATE_CHOMPER_SWALLOWING || this.mState == PlantState.STATE_CHOMPER_BITING_MISSED) && reanimation.mLoopCount > 0)
			{
				this.PlayIdleAnim(reanimation.mDefinition.mFPS);
				this.mState = PlantState.STATE_READY;
			}
		}

		public void DoBlink()
		{
			this.mBlinkCountdown = 400 + RandomNumbers.NextNumber(400);
			if (this.NotOnGround() || this.mShootingCounter != 0)
			{
				return;
			}
			if (this.mSeedType == SeedType.SEED_POTATOMINE && this.mState != PlantState.STATE_POTATO_ARMED)
			{
				return;
			}
			if (this.mState == PlantState.STATE_CACTUS_RISING || this.mState == PlantState.STATE_CACTUS_HIGH || this.mState == PlantState.STATE_CACTUS_LOWERING || this.mState == PlantState.STATE_MAGNETSHROOM_SUCKING || this.mState == PlantState.STATE_MAGNETSHROOM_CHARGING)
			{
				return;
			}
			this.EndBlink();
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null || reanimation.mDead)
			{
				return;
			}
			if (this.mSeedType == SeedType.SEED_TALLNUT && reanimation.GetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_idle) == AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED2)
			{
				return;
			}
			if (this.mSeedType == SeedType.SEED_GARLIC && reanimation.GetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_face) == AtlasResources.IMAGE_REANIM_GARLIC_BODY3)
			{
				return;
			}
			if (this.mSeedType == SeedType.SEED_WALLNUT || this.mSeedType == SeedType.SEED_TALLNUT || this.mSeedType == SeedType.SEED_EXPLODE_O_NUT || this.mSeedType == SeedType.SEED_GIANT_WALLNUT)
			{
				this.mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
			}
			Reanimation reanimation2 = this.AttachBlinkAnim(reanimation);
			if (reanimation2 != null)
			{
				this.mBlinkReanimID = this.mApp.ReanimationGetID(reanimation2);
			}
			reanimation.AssignRenderGroupToPrefix("anim_eye", -1);
		}

		public void UpdateBlink()
		{
			if (this.mBlinkReanimID != null)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBlinkReanimID);
				if (reanimation == null || reanimation.mLoopCount > 0)
				{
					this.EndBlink();
				}
			}
			if (this.mIsAsleep)
			{
				return;
			}
			if (this.mBlinkCountdown > 0)
			{
				this.mBlinkCountdown -= 3;
				if (this.mBlinkCountdown == 0)
				{
					this.DoBlink();
				}
			}
		}

		public void PlayBodyReanim(string theTrackName, ReanimLoopType theLoopType, byte theBlendTime, float theAnimRate)
		{
			this.lastPlayedBodyReanim_Name = theTrackName;
			this.lastPlayedBodyReanim_Type = theLoopType;
			this.lastPlayedBodyReanim_BlendTime = theBlendTime;
			this.lastPlayedBodyReanim_AnimRate = theAnimRate;
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
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

		public void UpdateMagnetShroom()
		{
			for (int i = 0; i < 5; i++)
			{
				MagnetItem magnetItem = this.mMagnetItems[i];
				if (magnetItem.mItemType != MagnetItemType.MAGNET_ITEM_NONE)
				{
					SexyVector2 sexyVector = new SexyVector2((float)this.mX + magnetItem.mDestOffsetX - magnetItem.mPosX, (float)this.mY + magnetItem.mDestOffsetY - magnetItem.mPosY);
					float num = sexyVector.Magnitude();
					if (num >= 20f)
					{
						magnetItem.mPosX += sexyVector.x * 0.05f;
						magnetItem.mPosY += sexyVector.y * 0.05f;
					}
				}
			}
			if (this.mState == PlantState.STATE_MAGNETSHROOM_CHARGING)
			{
				if (this.mStateCountdown <= 0)
				{
					this.mState = PlantState.STATE_READY;
					float theAnimRate = TodCommon.RandRangeFloat(10f, 15f);
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 30, theAnimRate);
					if (this.mApp.IsIZombieLevel())
					{
						Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
						reanimation.mAnimRate = 0f;
					}
					MagnetItem magnetItem2 = this.mMagnetItems[0];
					magnetItem2.mItemType = MagnetItemType.MAGNET_ITEM_NONE;
				}
				return;
			}
			if (this.mState == PlantState.STATE_MAGNETSHROOM_SUCKING)
			{
				Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_nonactive_idle2, ReanimLoopType.REANIM_LOOP, 20, 2f);
					if (this.mApp.IsIZombieLevel())
					{
						reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
						reanimation.mAnimRate = 0f;
					}
					this.mState = PlantState.STATE_MAGNETSHROOM_CHARGING;
				}
				return;
			}
			Zombie zombie = null;
			float num2 = 0f;
			int count = this.mBoard.mZombies.Count;
			for (int j = 0; j < count; j++)
			{
				Zombie zombie2 = this.mBoard.mZombies[j];
				if (!zombie2.mDead)
				{
					TRect zombieRect = zombie2.GetZombieRect();
					int num3 = zombie2.mRow - this.mRow;
					if (!zombie2.mMindControlled && zombie2.mHasHead && zombie2.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIE_NORMAL && zombie2.mZombiePhase != ZombiePhase.PHASE_RISING_FROM_GRAVE && !zombie2.IsDeadOrDying() && zombieRect.mX <= Constants.WIDE_BOARD_WIDTH && num3 <= 2 && num3 >= -2)
					{
						if (zombie2.mZombiePhase == ZombiePhase.PHASE_DIGGER_TUNNELING || zombie2.mZombiePhase == ZombiePhase.PHASE_DIGGER_STUNNED || zombie2.mZombiePhase == ZombiePhase.PHASE_DIGGER_WALKING || zombie2.mZombieType == ZombieType.ZOMBIE_POGO)
						{
							if (!zombie2.mHasObject)
							{
								goto IL_322;
							}
						}
						else if (zombie2.mHelmType != HelmType.HELMTYPE_PAIL && zombie2.mHelmType != HelmType.HELMTYPE_FOOTBALL && zombie2.mShieldType != ShieldType.SHIELDTYPE_DOOR && zombie2.mShieldType != ShieldType.SHIELDTYPE_LADDER && zombie2.mZombiePhase != ZombiePhase.PHASE_JACK_IN_THE_BOX_RUNNING)
						{
							goto IL_322;
						}
						int theRadius = 270;
						if (zombie2.mIsEating)
						{
							theRadius = 320;
						}
						if (GameConstants.GetCircleRectOverlap(this.mX, this.mY + 20, theRadius, zombieRect))
						{
							float num4 = TodCommon.Distance2D((float)this.mX, (float)this.mY, (float)zombieRect.mX, (float)zombieRect.mY);
							num4 += (float)Math.Abs(num3) * 80f;
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
				this.MagnetShroomAttactItem(zombie);
				return;
			}
			GridItem gridItem = null;
			float num5 = 0f;
			int num6 = -1;
			GridItem gridItem2 = null;
			while (this.mBoard.IterateGridItems(ref gridItem2, ref num6))
			{
				if (gridItem2.mGridItemType == GridItemType.GRIDITEM_LADDER)
				{
					int num7 = gridItem2.mGridX - this.mPlantCol;
					int num8 = gridItem2.mGridY - this.mRow;
					int num9 = Math.Max(Math.Abs(num7), Math.Abs(num8));
					if (num9 <= 2)
					{
						float num10 = (float)num9;
						num10 += (float)Math.Abs(num8) * 0.05f;
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
				this.mState = PlantState.STATE_MAGNETSHROOM_SUCKING;
				this.mStateCountdown = 1500;
				this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
				this.mApp.PlayFoley(FoleyType.FOLEY_MAGNETSHROOM);
				gridItem.GridItemDie();
				MagnetItem freeMagnetItem = this.GetFreeMagnetItem();
				freeMagnetItem.mPosX = (float)(this.mBoard.GridToPixelX(gridItem.mGridX, gridItem.mGridY) + 40);
				freeMagnetItem.mPosY = (float)this.mBoard.GridToPixelY(gridItem.mGridX, gridItem.mGridY);
				freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 10f;
				freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f);
				freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_LADDER_PLACED;
			}
		}

		public MagnetItem GetFreeMagnetItem()
		{
			if (this.mSeedType == SeedType.SEED_GOLD_MAGNET)
			{
				for (int i = 0; i < 5; i++)
				{
					if (this.mMagnetItems[i].mItemType == MagnetItemType.MAGNET_ITEM_NONE)
					{
						return this.mMagnetItems[i];
					}
				}
				return null;
			}
			return this.mMagnetItems[0];
		}

		public void DrawMagnetItems(Graphics g)
		{
			float num = 0f;
			float num2 = Plant.PlantDrawHeightOffset(this.mBoard, this, this.mSeedType, this.mPlantCol, this.mRow);
			for (int i = 0; i < 5; i++)
			{
				MagnetItem magnetItem = this.mMagnetItems[i];
				if (magnetItem.mItemType != MagnetItemType.MAGNET_ITEM_NONE)
				{
					int theCelCol = 0;
					int theCelRow = 0;
					Image theImageStrip = null;
					float num3 = 1f;
					if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_PAIL_1)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET1;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_PAIL_2)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET2;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_PAIL_3)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET3;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_FOOTBALL_HELMET_1)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_FOOTBALL_HELMET_2)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET2;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_FOOTBALL_HELMET_3)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_FOOTBALL_HELMET3;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_DOOR_1)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR1;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_DOOR_2)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR2;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_DOOR_3)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR3;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType >= MagnetItemType.MAGNET_ITEM_POGO_1 && magnetItem.mItemType <= MagnetItemType.MAGNET_ITEM_POGO_3)
					{
						theCelCol = magnetItem.mItemType - MagnetItemType.MAGNET_ITEM_POGO_1;
						theImageStrip = AtlasResources.IMAGE_ZOMBIEPOGO;
						num3 = 0.8f;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_LADDER_1)
					{
						num3 = 0.8f;
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_LADDER_2)
					{
						num3 = 0.8f;
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1_DAMAGE1;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_LADDER_3)
					{
						num3 = 0.8f;
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_1_DAMAGE2;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_LADDER_PLACED)
					{
						num3 = 0.8f;
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_5;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_JACK_IN_THE_BOX)
					{
						num3 = 0.8f;
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_JACKBOX_BOX;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_PICK_AXE)
					{
						num3 = 0.8f;
						theImageStrip = AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_PICKAXE;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_SILVER_COIN)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_COIN_SILVER_DOLLAR;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_GOLD_COIN)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_COIN_GOLD_DOLLAR;
					}
					else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_DIAMOND)
					{
						theImageStrip = AtlasResources.IMAGE_REANIM_DIAMOND;
					}
					else
					{
						Debug.ASSERT(false);
					}
					if (num3 == 1f)
					{
						g.DrawImageCel(theImageStrip, (int)((magnetItem.mPosX - (float)this.mX + num) * Constants.S), (int)((magnetItem.mPosY - (float)this.mY + num2) * Constants.S), theCelCol, theCelRow);
					}
					else
					{
						TodCommon.TodDrawImageCelScaledF(g, theImageStrip, (magnetItem.mPosX - (float)this.mX + num) * Constants.S, (magnetItem.mPosY - (float)this.mY + num2) * Constants.S, theCelCol, 0, num3, num3);
					}
				}
			}
		}

		public void UpdateDoomShroom()
		{
			if (this.mIsAsleep || this.mState == PlantState.STATE_DOINGSPECIAL)
			{
				return;
			}
			this.mState = PlantState.STATE_DOINGSPECIAL;
			this.mDoSpecialCountdown = 100;
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			Debug.ASSERT(reanimation != null);
			reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_explode);
			reanimation.mAnimRate = 23f;
			reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
			reanimation.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_doomshroom_head1, 1f);
			reanimation.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_doomshroom_head2, 2f);
			reanimation.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_doomshroom_head3, 2f);
			this.mApp.PlayFoley(FoleyType.FOLEY_REVERSE_EXPLOSION);
		}

		public void UpdateIceShroom()
		{
			if (this.mIsAsleep || this.mState == PlantState.STATE_DOINGSPECIAL)
			{
				return;
			}
			this.mState = PlantState.STATE_DOINGSPECIAL;
			this.mDoSpecialCountdown = 100;
		}

		public void UpdatePotato()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			if (this.mState == PlantState.STATE_NOTREADY)
			{
				if (this.mStateCountdown <= 0)
				{
					int num = this.mX + this.mWidth / 2;
					int num2 = this.mY + this.mHeight / 2;
					this.mApp.AddTodParticle((float)num, (float)num2, this.mRenderOrder, ParticleEffect.PARTICLE_POTATO_MINE_RISE);
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_rise, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 18f);
					this.mState = PlantState.STATE_POTATO_RISING;
					this.mApp.PlayFoley(FoleyType.FOLEY_DIRT_RISE);
					return;
				}
			}
			else if (this.mState == PlantState.STATE_POTATO_RISING)
			{
				if (reanimation.mLoopCount > 0)
				{
					float num3 = TodCommon.RandRangeFloat(12f, 15f);
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_armed, ReanimLoopType.REANIM_LOOP, 0, num3);
					PlantDefinition plantDefinition = Plant.GetPlantDefinition(this.mSeedType);
					Reanimation reanimation2 = this.mApp.AddReanimation(0f, 0f, this.mRenderOrder + 2, plantDefinition.mReanimationType);
					reanimation2.mLoopType = ReanimLoopType.REANIM_LOOP;
					reanimation2.mAnimRate = num3 - 2f;
					reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_glow);
					reanimation2.mFrameCount = 10;
					reanimation2.ShowOnlyTrack(GlobalMembersReanimIds.ReanimTrackId_anim_glow);
					reanimation2.SetTruncateDisappearingFrames(GlobalMembersReanimIds.ReanimTrackId_anim_glow, false);
					this.mLightReanimID = this.mApp.ReanimationGetID(reanimation2);
					reanimation2.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_light);
					this.mState = PlantState.STATE_POTATO_ARMED;
					this.mBlinkCountdown = 400 + RandomNumbers.NextNumber(400);
					return;
				}
			}
			else if (this.mState == PlantState.STATE_POTATO_ARMED)
			{
				Zombie zombie = this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					this.DoSpecial();
					return;
				}
				Reanimation reanimation3 = this.mApp.ReanimationTryToGet(this.mLightReanimID);
				if (reanimation3 != null)
				{
					int theTimeAge = this.DistanceToClosestZombie();
					reanimation3.mFrameCount = (short)TodCommon.TodAnimateCurve(200, 50, theTimeAge, 10, 3, TodCurves.CURVE_LINEAR);
				}
			}
		}

		public int CalcRenderOrder()
		{
			PLANT_ORDER plant_ORDER = PLANT_ORDER.PLANT_ORDER_NORMAL;
			RenderLayer theRenderLayer = RenderLayer.RENDER_LAYER_PLANT;
			int num = 0;
			SeedType seedType = this.mSeedType;
			if (this.mSeedType == SeedType.SEED_IMITATER && this.mImitaterType != SeedType.SEED_NONE)
			{
				seedType = this.mImitaterType;
			}
			if (this.mApp.IsWallnutBowlingLevel())
			{
				theRenderLayer = RenderLayer.RENDER_LAYER_PROJECTILE;
			}
			else if (seedType == SeedType.SEED_PUMPKINSHELL)
			{
				plant_ORDER = PLANT_ORDER.PLANT_ORDER_PUMPKIN;
			}
			else if (Plant.IsFlying(seedType))
			{
				plant_ORDER = PLANT_ORDER.PLANT_ORDER_FLYER;
			}
			else if (seedType == SeedType.SEED_FLOWERPOT)
			{
				plant_ORDER = PLANT_ORDER.PLANT_ORDER_LILYPAD;
			}
			else if (seedType == SeedType.SEED_LILYPAD && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				plant_ORDER = PLANT_ORDER.PLANT_ORDER_LILYPAD;
			}
			if (seedType == SeedType.SEED_COBCANNON)
			{
				num = 0;
			}
			return Board.MakeRenderOrder(theRenderLayer, this.mRow, plant_ORDER * (PLANT_ORDER)5 - (PLANT_ORDER)this.mX + num);
		}

		public void AnimateNuts()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			Image image;
			Image image2;
			string theTrackName;
			if (this.mSeedType == SeedType.SEED_WALLNUT)
			{
				image = AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED1;
				image2 = AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED2;
				theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face;
			}
			else
			{
				if (this.mSeedType != SeedType.SEED_TALLNUT)
				{
					return;
				}
				image = AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED1;
				image2 = AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED2;
				theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_idle;
			}
			int num = this.mX + 40;
			int num2 = this.mY + 10;
			if (this.mSeedType == SeedType.SEED_TALLNUT)
			{
				num2 -= 32;
			}
			Image imageOverride = reanimation.GetImageOverride(theTrackName);
			if (this.mPlantHealth < this.mPlantMaxHealth / 3)
			{
				if (imageOverride != image2)
				{
					reanimation.SetImageOverride(theTrackName, image2);
					this.mApp.AddTodParticle((float)num, (float)num2, this.mRenderOrder + 4, ParticleEffect.PARTICLE_WALLNUT_EAT_LARGE);
				}
			}
			else if (this.mPlantHealth < this.mPlantMaxHealth * 2 / 3)
			{
				if (imageOverride != image)
				{
					reanimation.SetImageOverride(theTrackName, image);
					this.mApp.AddTodParticle((float)num, (float)num2, this.mRenderOrder + 4, ParticleEffect.PARTICLE_WALLNUT_EAT_LARGE);
				}
			}
			else
			{
				Image theImage = null;
				reanimation.SetImageOverride(theTrackName, theImage);
			}
			if (this.IsInPlay() && !this.mApp.IsIZombieLevel())
			{
				if (this.mRecentlyEatenCountdown > 0)
				{
					reanimation.mAnimRate = 0.1f;
					return;
				}
				if (reanimation.mAnimRate < 1f && this.mOnBungeeState != PlantOnBungeeState.PLANT_RISING_WITH_BUNGEE)
				{
					reanimation.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
				}
			}
		}

		public void SetSleeping(bool theIsAsleep)
		{
			if (this.mIsAsleep == theIsAsleep)
			{
				return;
			}
			if (this.NotOnGround())
			{
				return;
			}
			this.mIsAsleep = theIsAsleep;
			if (theIsAsleep)
			{
				float num = (float)this.mX + 50f;
				float num2 = (float)this.mY + 40f;
				if (this.mSeedType == SeedType.SEED_FUMESHROOM)
				{
					num += 12f;
				}
				else if (this.mSeedType == SeedType.SEED_SCAREDYSHROOM)
				{
					num2 -= 20f;
				}
				else if (this.mSeedType == SeedType.SEED_GLOOMSHROOM)
				{
					num2 -= 12f;
				}
				Reanimation reanimation = this.mApp.AddReanimation(num, num2, this.mRenderOrder + 2, ReanimationType.REANIM_SLEEPING);
				reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation.mAnimRate = TodCommon.RandRangeFloat(6f, 8f);
				reanimation.mAnimTime = TodCommon.RandRangeFloat(0f, 0.9f);
				this.mSleepingReanimID = this.mApp.ReanimationGetID(reanimation);
			}
			else
			{
				this.mApp.RemoveReanimation(ref this.mSleepingReanimID);
				this.mSleepingReanimID = null;
			}
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation2 == null)
			{
				return;
			}
			if (theIsAsleep)
			{
				if (!this.IsInPlay() && this.mSeedType == SeedType.SEED_SUNSHROOM)
				{
					reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_bigsleep);
				}
				else if (reanimation2.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_sleep))
				{
					float mAnimTime = reanimation2.mAnimTime;
					reanimation2.StartBlend(20);
					reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_sleep);
					reanimation2.mAnimTime = mAnimTime;
				}
				else
				{
					reanimation2.mAnimRate = 1f;
				}
				this.EndBlink();
				return;
			}
			if (!this.IsInPlay() && this.mSeedType == SeedType.SEED_SUNSHROOM)
			{
				reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_bigidle);
			}
			else if (reanimation2.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
			{
				float mAnimTime2 = reanimation2.mAnimTime;
				reanimation2.StartBlend(20);
				reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
				reanimation2.mAnimTime = mAnimTime2;
			}
			if (reanimation2.mAnimRate < 2f && this.IsInPlay())
			{
				reanimation2.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
			}
		}

		public void UpdateShooting()
		{
			if (this.NotOnGround())
			{
				return;
			}
			if (this.mShootingCounter == 0)
			{
				return;
			}
			this.mShootingCounter -= 3;
			if (this.mSeedType == SeedType.SEED_FUMESHROOM && this.mShootingCounter >= 15 && this.mShootingCounter < 18)
			{
				int theRenderPostition = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, this.mRow, 0);
				this.AddAttachedParticle(this.mX + 85, this.mY + 31, theRenderPostition, ParticleEffect.PARTICLE_FUMECLOUD);
			}
			Reanimation reanimation4;
			Reanimation reanimation6;
			if (this.mSeedType == SeedType.SEED_GLOOMSHROOM)
			{
				if ((this.mShootingCounter >= 136 && this.mShootingCounter < 139) || (this.mShootingCounter >= 108 && this.mShootingCounter < 111) || (this.mShootingCounter >= 80 && this.mShootingCounter < 83) || (this.mShootingCounter >= 52 && this.mShootingCounter < 55))
				{
					int theRenderPostition2 = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, this.mRow, 0);
					this.AddAttachedParticle(this.mX + 40, this.mY + 40, theRenderPostition2, ParticleEffect.PARTICLE_GLOOMCLOUD);
				}
				if ((this.mShootingCounter >= 126 && this.mShootingCounter < 129) || (this.mShootingCounter >= 98 && this.mShootingCounter < 101) || (this.mShootingCounter >= 70 && this.mShootingCounter < 73) || (this.mShootingCounter >= 42 && this.mShootingCounter < 45))
				{
					this.Fire(null, this.mRow, PlantWeapon.WEAPON_PRIMARY);
				}
			}
			else if (this.mSeedType == SeedType.SEED_GATLINGPEA)
			{
				if ((this.mShootingCounter >= 18 && this.mShootingCounter < 21) || (this.mShootingCounter >= 35 && this.mShootingCounter < 38) || (this.mShootingCounter >= 51 && this.mShootingCounter < 54) || (this.mShootingCounter >= 68 && this.mShootingCounter < 71))
				{
					this.Fire(null, this.mRow, PlantWeapon.WEAPON_PRIMARY);
				}
			}
			else if (this.mSeedType == SeedType.SEED_CATTAIL)
			{
				if (this.mShootingCounter >= 19 && this.mShootingCounter < 22)
				{
					Zombie zombie = this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY);
					if (zombie != null)
					{
						this.Fire(zombie, this.mRow, PlantWeapon.WEAPON_PRIMARY);
					}
				}
			}
			else if (this.mShootingCounter >= 1 && this.mShootingCounter < 4)
			{
				if (this.mSeedType == SeedType.SEED_THREEPEATER)
				{
					int theRow = this.mRow - 1;
					int theRow2 = this.mRow + 1;
					Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mHeadReanimID);
					Reanimation reanimation2 = this.mApp.ReanimationTryToGet(this.mHeadReanimID2);
					Reanimation reanimation3 = this.mApp.ReanimationTryToGet(this.mHeadReanimID3);
					if (reanimation.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						this.Fire(null, theRow2, PlantWeapon.WEAPON_PRIMARY);
					}
					if (reanimation2.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						this.Fire(null, this.mRow, PlantWeapon.WEAPON_PRIMARY);
					}
					if (reanimation3.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						this.Fire(null, theRow, PlantWeapon.WEAPON_PRIMARY);
						return;
					}
				}
				else if (this.mSeedType == SeedType.SEED_SPLITPEA)
				{
					reanimation4 = this.mApp.ReanimationTryToGet(this.mHeadReanimID);
					Reanimation reanimation5 = this.mApp.ReanimationTryToGet(this.mHeadReanimID2);
					if (reanimation4.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						this.Fire(null, this.mRow, PlantWeapon.WEAPON_PRIMARY);
					}
					if (reanimation5.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						this.Fire(null, this.mRow, PlantWeapon.WEAPON_SECONDARY);
						return;
					}
				}
				else
				{
					if (this.mState == PlantState.STATE_CACTUS_LOW)
					{
						this.Fire(null, this.mRow, PlantWeapon.WEAPON_SECONDARY);
						return;
					}
					if (this.mSeedType == SeedType.SEED_CABBAGEPULT || this.mSeedType == SeedType.SEED_KERNELPULT || this.mSeedType == SeedType.SEED_MELONPULT || this.mSeedType == SeedType.SEED_WINTERMELON)
					{
						PlantWeapon thePlantWeapon = PlantWeapon.WEAPON_PRIMARY;
						if (this.mState == PlantState.STATE_KERNELPULT_BUTTER)
						{
							reanimation6 = this.mApp.ReanimationGet(this.mBodyReanimID);
							reanimation6.AssignRenderGroupToPrefix("Cornpult_butter", -1);
							reanimation6.AssignRenderGroupToPrefix("Cornpult_kernal", 0);
							this.mState = PlantState.STATE_NOTREADY;
							thePlantWeapon = PlantWeapon.WEAPON_SECONDARY;
						}
						Zombie theTargetZombie = this.FindTargetZombie(this.mRow, thePlantWeapon);
						this.Fire(theTargetZombie, this.mRow, thePlantWeapon);
						return;
					}
					this.Fire(null, this.mRow, PlantWeapon.WEAPON_PRIMARY);
				}
				return;
			}
			if (this.mShootingCounter > 0)
			{
				return;
			}
			reanimation6 = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			reanimation4 = this.mApp.ReanimationTryToGet(this.mHeadReanimID);
			if (this.mSeedType == SeedType.SEED_THREEPEATER)
			{
				Reanimation reanimation7 = reanimation4;
				Reanimation reanimation8 = this.mApp.ReanimationTryToGet(this.mHeadReanimID2);
				Reanimation reanimation9 = this.mApp.ReanimationTryToGet(this.mHeadReanimID3);
				if (reanimation8.mLoopCount > 0)
				{
					if (reanimation7.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						reanimation7.StartBlend(20);
						reanimation7.mLoopType = ReanimLoopType.REANIM_LOOP;
						reanimation7.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle1);
						reanimation7.mAnimRate = reanimation6.mAnimRate;
						reanimation7.mAnimTime = reanimation6.mAnimTime;
					}
					reanimation8.StartBlend(20);
					reanimation8.mLoopType = ReanimLoopType.REANIM_LOOP;
					reanimation8.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle2);
					reanimation8.mAnimRate = reanimation6.mAnimRate;
					reanimation8.mAnimTime = reanimation6.mAnimTime;
					if (reanimation9.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						reanimation9.StartBlend(20);
						reanimation9.mLoopType = ReanimLoopType.REANIM_LOOP;
						reanimation9.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle3);
						reanimation9.mAnimRate = reanimation6.mAnimRate;
						reanimation9.mAnimTime = reanimation6.mAnimTime;
					}
					return;
				}
			}
			else
			{
				if (this.mSeedType == SeedType.SEED_SPLITPEA)
				{
					Reanimation reanimation10 = this.mApp.ReanimationGet(this.mHeadReanimID2);
					if (reanimation4.mLoopCount > 0)
					{
						reanimation4.StartBlend(20);
						reanimation4.mLoopType = ReanimLoopType.REANIM_LOOP;
						reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
						reanimation4.mAnimRate = reanimation6.mAnimRate;
						reanimation4.mAnimTime = reanimation6.mAnimTime;
					}
					if (reanimation10.mLoopCount > 0)
					{
						reanimation10.StartBlend(20);
						reanimation10.mLoopType = ReanimLoopType.REANIM_LOOP;
						reanimation10.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_idle);
						reanimation10.mAnimRate = reanimation6.mAnimRate;
						reanimation10.mAnimTime = reanimation6.mAnimTime;
					}
					return;
				}
				if (this.mState == PlantState.STATE_CACTUS_HIGH)
				{
					if (reanimation6.mLoopCount > 0)
					{
						this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idlehigh, ReanimLoopType.REANIM_LOOP, 20, 0f);
						reanimation6.mAnimRate = reanimation6.mDefinition.mFPS;
						if (this.mApp.IsIZombieLevel())
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
						reanimation4.mLoopType = ReanimLoopType.REANIM_LOOP;
						reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
						reanimation4.mAnimRate = reanimation6.mAnimRate;
						reanimation4.mAnimTime = reanimation6.mAnimTime;
						return;
					}
				}
				else if (this.mSeedType == SeedType.SEED_COBCANNON)
				{
					if (reanimation6.mLoopCount > 0)
					{
						this.mState = PlantState.STATE_COBCANNON_ARMING;
						this.mStateCountdown = 3000;
						this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_unarmed_idle, ReanimLoopType.REANIM_LOOP, 20, reanimation6.mDefinition.mFPS);
						return;
					}
				}
				else if (reanimation6 != null && reanimation6.mLoopCount > 0)
				{
					this.PlayIdleAnim(reanimation6.mDefinition.mFPS);
					return;
				}
			}
			this.mShootingCounter = 3;
		}

		public void DrawShadow(Graphics g, float theOffsetX, float theOffsetY)
		{
			if (this.mSeedType == SeedType.SEED_LILYPAD || this.mSeedType == SeedType.SEED_STARFRUIT || this.mSeedType == SeedType.SEED_TANGLEKELP || this.mSeedType == SeedType.SEED_SEASHROOM || this.mSeedType == SeedType.SEED_COBCANNON || this.mSeedType == SeedType.SEED_SPIKEWEED || this.mSeedType == SeedType.SEED_SPIKEROCK || this.mSeedType == SeedType.SEED_GRAVEBUSTER || this.mSeedType == SeedType.SEED_CATTAIL || this.mOnBungeeState == PlantOnBungeeState.PLANT_RISING_WITH_BUNGEE)
			{
				return;
			}
			if (this.IsOnBoard() && this.mBoard.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mBoard.mApp.mZenGarden.mGardenType == GardenType.GARDEN_MAIN)
			{
				return;
			}
			int num = 0;
			float num2 = -3f;
			float num3 = 51f;
			float num4 = 1f;
			if (this.mBoard != null && this.mBoard.StageIsNight())
			{
				num = 1;
			}
			if (this.mSeedType == SeedType.SEED_SQUASH)
			{
				if (this.mBoard != null)
				{
					num3 += (float)(this.mBoard.GridToPixelY(this.mPlantCol, this.mRow) - this.mY);
				}
				num3 += 5f;
			}
			else if (this.mSeedType == SeedType.SEED_PUFFSHROOM || this.mSeedType == SeedType.SEED_SEASHROOM)
			{
				num4 = 0.5f;
				num3 -= 9f;
			}
			else if (this.mSeedType == SeedType.SEED_SUNSHROOM)
			{
				num3 += -9f;
				if (this.mState == PlantState.STATE_SUNSHROOM_SMALL)
				{
					num4 = 0.5f;
				}
				else if (this.mState == PlantState.STATE_SUNSHROOM_GROWING)
				{
					Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
					num4 = 0.5f + 0.5f * reanimation.mAnimTime;
				}
				else
				{
					num4 = 1f;
				}
			}
			else if (this.mSeedType == SeedType.SEED_UMBRELLA)
			{
				num4 = 0.5f;
				num2 -= 4f;
				num3 += 1f;
			}
			else if (this.mSeedType == SeedType.SEED_FUMESHROOM || this.mSeedType == SeedType.SEED_GLOOMSHROOM)
			{
				num4 = 1.3f;
				num3 -= 4f;
			}
			else if (this.mSeedType == SeedType.SEED_CABBAGEPULT || this.mSeedType == SeedType.SEED_MELONPULT || this.mSeedType == SeedType.SEED_WINTERMELON)
			{
				num3 -= 4f;
			}
			else if (this.mSeedType == SeedType.SEED_KERNELPULT)
			{
				num2 += 3f;
				num3 -= 4f;
			}
			else if (this.mSeedType == SeedType.SEED_SCAREDYSHROOM)
			{
				num2 += -6f;
				num3 += 4f;
			}
			else if (this.mSeedType == SeedType.SEED_CHOMPER)
			{
				num2 += -18f;
				num3 += 6f;
			}
			else if (this.mSeedType == SeedType.SEED_FLOWERPOT)
			{
				num2 += -1f;
				num3 += -5f;
			}
			else if (this.mSeedType == SeedType.SEED_TALLNUT)
			{
				num3 += 3f;
				num4 = 1.3f;
			}
			else if (this.mSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				num3 += -5f;
				num4 = 1.4f;
			}
			else if (this.mSeedType == SeedType.SEED_CACTUS)
			{
				num2 += -5f;
				num3 += -1f;
			}
			else if (this.mSeedType == SeedType.SEED_PLANTERN)
			{
				num3 += 6f;
			}
			else if (this.mSeedType == SeedType.SEED_INSTANT_COFFEE)
			{
				num3 += 20f;
			}
			else if (this.mSeedType == SeedType.SEED_GIANT_WALLNUT)
			{
				num2 -= 30f;
				num3 += 5f;
				num4 = 1.7f;
			}
			if (Plant.IsFlying(this.mSeedType))
			{
				num3 += 10f;
				if (this.mBoard != null && (this.mBoard.GetTopPlantAt(this.mPlantCol, this.mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION) != null || this.mBoard.GetTopPlantAt(this.mPlantCol, this.mRow, PlantPriority.TOPPLANT_ONLY_PUMPKIN) != null))
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

		public void UpdateScaredyShroom()
		{
			if (this.mShootingCounter > 0)
			{
				return;
			}
			bool flag = false;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead)
				{
					TRect zombieRect = zombie.GetZombieRect();
					int num = zombie.mRow - this.mRow;
					if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						num = 0;
					}
					if (!zombie.mMindControlled && !zombie.IsDeadOrDying() && num <= 1 && num >= -1 && GameConstants.GetCircleRectOverlap(this.mX, this.mY + 20, 120, zombieRect))
					{
						flag = true;
						break;
					}
				}
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			if (this.mState == PlantState.STATE_READY)
			{
				if (flag)
				{
					this.mState = PlantState.STATE_SCAREDYSHROOM_LOWERING;
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_scared, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 10f);
				}
			}
			else if (this.mState == PlantState.STATE_SCAREDYSHROOM_LOWERING)
			{
				if (reanimation.mLoopCount > 0)
				{
					this.mState = PlantState.STATE_SCAREDYSHROOM_SCARED;
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_scaredidle, ReanimLoopType.REANIM_LOOP, 10, 0f);
				}
			}
			else if (this.mState == PlantState.STATE_SCAREDYSHROOM_SCARED)
			{
				if (!flag)
				{
					this.mState = PlantState.STATE_SCAREDYSHROOM_RAISING;
					float theAnimRate = TodCommon.RandRangeFloat(7f, 12f);
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_grow, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, theAnimRate);
				}
			}
			else if (this.mState == PlantState.STATE_SCAREDYSHROOM_RAISING && reanimation.mLoopCount > 0)
			{
				this.mState = PlantState.STATE_READY;
				float theRate = TodCommon.RandRangeFloat(10f, 15f);
				this.PlayIdleAnim(theRate);
			}
			if (this.mState != PlantState.STATE_READY)
			{
				this.mLaunchCounter = this.mLaunchRate;
			}
		}

		public int DistanceToClosestZombie()
		{
			int damageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			TRect plantAttackRect = this.GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
			int num = 1000;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && zombie.mRow == this.mRow && zombie.EffectedByDamage((uint)damageRangeFlags))
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

		public void UpdateSpikeweed()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			if (this.mState == PlantState.STATE_SPIKEWEED_ATTACKING)
			{
				if (this.mStateCountdown <= 0)
				{
					this.mState = PlantState.STATE_NOTREADY;
				}
				else if (this.mSeedType == SeedType.SEED_SPIKEROCK)
				{
					if (this.mStateCountdown == 69 || this.mStateCountdown == 33)
					{
						this.DoRowAreaDamage(20, 33U);
					}
				}
				else if (this.mStateCountdown == 75)
				{
					this.DoRowAreaDamage(20, 33U);
				}
				if (reanimation.mLoopCount > 0)
				{
					float theRate = TodCommon.RandRangeFloat(12f, 15f);
					this.PlayIdleAnim(theRate);
					return;
				}
			}
			else
			{
				Zombie zombie = this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					this.SpikeweedAttack();
				}
			}
		}

		public void MagnetShroomAttactItem(Zombie theZombie)
		{
			this.mState = PlantState.STATE_MAGNETSHROOM_SUCKING;
			this.mStateCountdown = 1500;
			this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			this.mApp.PlayFoley(FoleyType.FOLEY_MAGNETSHROOM);
			MagnetItem freeMagnetItem = this.GetFreeMagnetItem();
			if (theZombie.mHelmType == HelmType.HELMTYPE_PAIL)
			{
				int helmDamageIndex = theZombie.GetHelmDamageIndex();
				theZombie.mHelmHealth = 0;
				theZombie.mHelmType = HelmType.HELMTYPE_NONE;
				theZombie.mHasHelm = false;
				theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_bucket, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
				theZombie.ReanimShowPrefix("anim_bucket", -1);
				theZombie.ReanimShowPrefix("anim_hair", 0);
				freeMagnetItem.mPosX -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET1.mWidth / 2);
				freeMagnetItem.mPosY -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_BUCKET1.mHeight / 2);
				freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 25f;
				freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f) + 20f;
				freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_PAIL_1 + helmDamageIndex;
			}
			else if (theZombie.mHelmType == HelmType.HELMTYPE_FOOTBALL)
			{
				int helmDamageIndex2 = theZombie.GetHelmDamageIndex();
				theZombie.mHelmHealth = 0;
				theZombie.mHelmType = HelmType.HELMTYPE_NONE;
				theZombie.mHasHelm = false;
				theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_football_helmet, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
				theZombie.ReanimShowPrefix("zombie_football_helmet", -1);
				theZombie.ReanimShowPrefix("anim_hair", 0);
				freeMagnetItem.mPosX = theZombie.mPosX + 37f;
				freeMagnetItem.mPosY = theZombie.mPosY - 60f;
				freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 20f;
				freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f) + 20f;
				freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_FOOTBALL_HELMET_1 + helmDamageIndex2;
			}
			else if (theZombie.mShieldType == ShieldType.SHIELDTYPE_DOOR)
			{
				int shieldDamageIndex = theZombie.GetShieldDamageIndex();
				theZombie.DetachShield();
				theZombie.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
				if (!theZombie.mIsEating)
				{
					Debug.ASSERT(theZombie.mZombieHeight == ZombieHeight.HEIGHT_ZOMBIE_NORMAL);
					theZombie.StartWalkAnim(0);
				}
				theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_anim_screendoor, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
				freeMagnetItem.mPosX -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR1.mWidth / 2);
				freeMagnetItem.mPosY -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_SCREENDOOR1.mHeight / 2);
				freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 30f;
				freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f);
				freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_DOOR_1 + shieldDamageIndex;
			}
			else if (theZombie.mShieldType == ShieldType.SHIELDTYPE_LADDER)
			{
				int shieldDamageIndex2 = theZombie.GetShieldDamageIndex();
				theZombie.DetachShield();
				freeMagnetItem.mPosX = theZombie.mPosX + 31f;
				freeMagnetItem.mPosY = theZombie.mPosY + 20f;
				freeMagnetItem.mPosX -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_5.GetCelWidth() / 2);
				freeMagnetItem.mPosY -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_5.GetCelHeight() / 2);
				freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 30f;
				freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f);
				freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_LADDER_1 + shieldDamageIndex2;
			}
			else if (theZombie.mZombieType == ZombieType.ZOMBIE_POGO)
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
					freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_POGO_1;
				}
				else
				{
					freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_POGO_3;
				}
			}
			else if (theZombie.mZombiePhase == ZombiePhase.PHASE_JACK_IN_THE_BOX_RUNNING)
			{
				theZombie.StopZombieSound();
				theZombie.PickRandomSpeed();
				theZombie.mZombiePhase = ZombiePhase.PHASE_ZOMBIE_NORMAL;
				theZombie.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_box, -1);
				theZombie.ReanimShowTrack(ref GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_handle, -1);
				theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_jackbox_box, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
				freeMagnetItem.mPosX -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_JACKBOX_BOX.mWidth / 2);
				freeMagnetItem.mPosY -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_JACKBOX_BOX.mHeight / 2);
				freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 20f;
				freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f) + 15f;
				freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_JACK_IN_THE_BOX;
			}
			else if (theZombie.mZombieType == ZombieType.ZOMBIE_DIGGER)
			{
				theZombie.DiggerLoseAxe();
				theZombie.GetTrackPosition(ref GlobalMembersReanimIds.ReanimTrackId_zombie_digger_pickaxe, ref freeMagnetItem.mPosX, ref freeMagnetItem.mPosY);
				freeMagnetItem.mPosX -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_PICKAXE.mWidth / 2);
				freeMagnetItem.mPosY -= (float)(AtlasResources.IMAGE_REANIM_ZOMBIE_DIGGER_PICKAXE.mHeight / 2);
				freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 45f;
				freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f) + 15f;
				freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_PICK_AXE;
			}
			freeMagnetItem.mDestOffsetX *= Constants.S;
			freeMagnetItem.mDestOffsetY *= Constants.S;
		}

		public void UpdateSunShroom()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			if (this.mState == PlantState.STATE_SUNSHROOM_SMALL)
			{
				if (this.mStateCountdown <= 0)
				{
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_grow, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 12f);
					this.mState = PlantState.STATE_SUNSHROOM_GROWING;
					this.mApp.PlayFoley(FoleyType.FOLEY_PLANTGROW);
				}
				this.UpdateProductionPlant();
				return;
			}
			if (this.mState == PlantState.STATE_SUNSHROOM_GROWING)
			{
				if (reanimation.mLoopCount > 0)
				{
					float theAnimRate = TodCommon.RandRangeFloat(12f, 15f);
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_bigidle, ReanimLoopType.REANIM_LOOP, 10, theAnimRate);
					this.mState = PlantState.STATE_SUNSHROOM_BIG;
					return;
				}
			}
			else
			{
				this.UpdateProductionPlant();
			}
		}

		public void UpdateBowling()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation != null && reanimation.TrackExists(Reanimation.ReanimTrackId__ground))
			{
				float num = reanimation.GetTrackVelocity(Reanimation.ReanimTrackId__ground) / 4f;
				if (this.mSeedType == SeedType.SEED_GIANT_WALLNUT)
				{
					num *= 2f;
				}
				this.mX -= (int)num;
				if (this.mX > 900)
				{
					this.Die();
				}
			}
			int num2 = 3;
			if (this.mState == PlantState.STATE_BOWLING_UP)
			{
				this.mY -= num2;
			}
			else if (this.mState == PlantState.STATE_BOWLING_DOWN)
			{
				this.mY += num2;
			}
			int num3 = this.mBoard.GridToPixelY(0, this.mRow) - this.mY;
			if (num3 > 2 || num3 < -2)
			{
				return;
			}
			PlantState plantState = this.mState;
			if (plantState == PlantState.STATE_BOWLING_UP && this.mRow <= 0)
			{
				plantState = PlantState.STATE_BOWLING_DOWN;
			}
			else if (plantState == PlantState.STATE_BOWLING_DOWN && this.mRow >= 4)
			{
				plantState = PlantState.STATE_BOWLING_UP;
			}
			Zombie zombie = this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY);
			if (zombie != null)
			{
				int num4 = this.mX + this.mWidth / 2;
				int num5 = this.mY + this.mHeight / 2;
				if (this.mSeedType == SeedType.SEED_EXPLODE_O_NUT)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_CHERRYBOMB);
					this.mApp.PlaySample(Resources.SOUND_BOWLINGIMPACT2);
					int theDamageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY) | 32;
					this.mBoard.KillAllZombiesInRadius(this.mRow, num4, num5, 90, 1, true, theDamageRangeFlags);
					this.mApp.AddTodParticle((float)num4, (float)num5, 400000, ParticleEffect.PARTICLE_POWIE);
					this.mBoard.ShakeBoard(3, -4);
					this.Die();
					return;
				}
				this.mApp.PlayFoley(FoleyType.FOLEY_BOWLINGIMPACT);
				this.mBoard.ShakeBoard(1, -2);
				if (this.mSeedType == SeedType.SEED_GIANT_WALLNUT)
				{
					zombie.TakeDamage(1800, 0U);
				}
				else if (zombie.mShieldType == ShieldType.SHIELDTYPE_DOOR && this.mState != PlantState.STATE_NOTREADY)
				{
					zombie.TakeDamage(1800, 0U);
				}
				else if (zombie.mShieldType != ShieldType.SHIELDTYPE_NONE)
				{
					zombie.TakeShieldDamage(400, 0U);
				}
				else if (zombie.mHelmType != HelmType.HELMTYPE_NONE)
				{
					if (zombie.mHelmType == HelmType.HELMTYPE_PAIL)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_SHIELD_HIT);
					}
					else if (zombie.mHelmType == HelmType.HELMTYPE_TRAFFIC_CONE)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
					}
					zombie.TakeHelmDamage(900, 0U);
				}
				else
				{
					zombie.TakeDamage(1800, 0U);
				}
				if ((!this.mApp.IsFirstTimeAdventureMode() || this.mBoard.mLevel > 10) && this.mSeedType == SeedType.SEED_WALLNUT)
				{
					this.mLaunchCounter++;
					if (this.mLaunchCounter == 2)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
						this.mBoard.AddCoin(num4, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
					}
					else if (this.mLaunchCounter == 3)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
						this.mBoard.AddCoin(num4 - 5, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
						this.mBoard.AddCoin(num4 + 5, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
					}
					else if (this.mLaunchCounter == 4)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
						this.mBoard.AddCoin(num4 - 10, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
						this.mBoard.AddCoin(num4, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
						this.mBoard.AddCoin(num4 + 10, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
					}
					else if (this.mLaunchCounter >= 5)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
						this.mBoard.AddCoin(num4, num5, CoinType.COIN_GOLD, CoinMotion.COIN_MOTION_COIN);
						this.mBoard.GrantAchievement(AchievementId.ACHIEVEMENT_ROLL_SOME_HEADS, true);
					}
				}
				if (this.mSeedType != SeedType.SEED_GIANT_WALLNUT)
				{
					if (this.mRow == 4 || this.mState == PlantState.STATE_BOWLING_DOWN)
					{
						plantState = PlantState.STATE_BOWLING_UP;
					}
					else if (this.mRow == 0 || this.mState == PlantState.STATE_BOWLING_UP)
					{
						plantState = PlantState.STATE_BOWLING_DOWN;
					}
					else if (RandomNumbers.NextNumber(2) == 0)
					{
						plantState = PlantState.STATE_BOWLING_DOWN;
					}
					else
					{
						plantState = PlantState.STATE_BOWLING_UP;
					}
				}
			}
			if (plantState == PlantState.STATE_BOWLING_UP)
			{
				this.mState = PlantState.STATE_BOWLING_UP;
				this.mRow--;
				this.mRenderOrder = this.CalcRenderOrder();
				return;
			}
			if (plantState == PlantState.STATE_BOWLING_DOWN)
			{
				this.mState = PlantState.STATE_BOWLING_DOWN;
				this.mRenderOrder = this.CalcRenderOrder();
				this.mRow++;
			}
		}

		public void AnimatePumpkin()
		{
			if (!this.mBodyReanimID.mActive)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			string reanimTrackId_pumpkin_front = GlobalMembersReanimIds.ReanimTrackId_pumpkin_front;
			Image imageOverride = reanimation.GetImageOverride(reanimTrackId_pumpkin_front);
			if (this.mPlantHealth < this.mPlantMaxHealth / 3)
			{
				if (imageOverride != AtlasResources.IMAGE_REANIM_PUMPKIN_DAMAGE3)
				{
					reanimation.SetImageOverride(reanimTrackId_pumpkin_front, AtlasResources.IMAGE_REANIM_PUMPKIN_DAMAGE3);
					return;
				}
			}
			else if (this.mPlantHealth < this.mPlantMaxHealth * 2 / 3)
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

		public void UpdateBlover()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			if (reanimation.mLoopCount > 0 && reanimation.mLoopType != ReanimLoopType.REANIM_LOOP)
			{
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_loop);
				reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
			}
			if (this.mState != PlantState.STATE_DOINGSPECIAL && this.mDoSpecialCountdown == 0)
			{
				this.DoSpecial();
			}
		}

		public void UpdateCactus()
		{
			if (this.mShootingCounter > 0)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			if (this.mState == PlantState.STATE_CACTUS_RISING)
			{
				if (reanimation.mLoopCount > 0)
				{
					this.mState = PlantState.STATE_CACTUS_HIGH;
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idlehigh, ReanimLoopType.REANIM_LOOP, 20, 0f);
					if (this.mApp.IsIZombieLevel())
					{
						reanimation.mAnimRate = 0f;
					}
					this.mLaunchCounter = 1;
					return;
				}
			}
			else if (this.mState == PlantState.STATE_CACTUS_HIGH)
			{
				if (this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY) == null)
				{
					this.mState = PlantState.STATE_CACTUS_LOWERING;
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lower, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, reanimation.mDefinition.mFPS);
					return;
				}
			}
			else if (this.mState == PlantState.STATE_CACTUS_LOWERING)
			{
				if (reanimation.mLoopCount > 0)
				{
					this.mState = PlantState.STATE_CACTUS_LOW;
					this.PlayIdleAnim(0f);
					return;
				}
			}
			else
			{
				Zombie zombie = this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					this.mState = PlantState.STATE_CACTUS_RISING;
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_rise, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, reanimation.mDefinition.mFPS);
					this.mApp.PlayFoley(FoleyType.FOLEY_PLANTGROW);
				}
			}
		}

		public void StarFruitFire()
		{
			this.mApp.PlayFoley(FoleyType.FOLEY_THROW);
			for (int i = 0; i < 5; i++)
			{
				int theX = this.mX + 25;
				int theY = this.mY + 25;
				Projectile projectile = this.mBoard.AddProjectile(theX, theY, this.mRenderOrder + -1, this.mRow, ProjectileType.PROJECTILE_STAR);
				projectile.mDamageRangeFlags = this.GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
				projectile.mMotionType = ProjectileMotion.MOTION_STAR;
				float mVelX = (float)Math.Cos((double)TodCommon.DegToRad(30f)) * 3.33f;
				float num = (float)Math.Sin((double)TodCommon.DegToRad(30f)) * 3.33f;
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
					projectile.mVelX = mVelX;
					projectile.mVelY = num;
					break;
				case 4:
					projectile.mVelX = mVelX;
					projectile.mVelY = -num;
					break;
				default:
					Debug.ASSERT(false);
					break;
				}
			}
		}

		public void UpdateTanglekelp()
		{
			if (this.mState != PlantState.STATE_TANGLEKELP_GRABBING)
			{
				Zombie zombie = this.FindTargetZombie(this.mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
					this.mState = PlantState.STATE_TANGLEKELP_GRABBING;
					this.mStateCountdown = 99;
					zombie.PoolSplash(false);
					float num = -13f;
					float num2 = 15f;
					if (zombie.mZombieType == ZombieType.ZOMBIE_SNORKEL)
					{
						num = -43f;
						num2 = 55f;
					}
					if (zombie.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_RIDING)
					{
						num = -20f;
						num2 = 37f;
					}
					Reanimation reanimation = zombie.AddAttachedReanim((int)num, (int)num2, ReanimationType.REANIM_TANGLEKELP);
					zombie.mAttachmentID.mUsesClipping = true;
					if (reanimation != null)
					{
						reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_grab);
						reanimation.mAnimRate = 24f;
						reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					}
					this.mTargetZombieID = this.mBoard.ZombieGetID(zombie);
					this.mTargetZombieID.mUsesClipping = true;
					this.mTargetZombieID.draggedByTangleKelp = true;
					return;
				}
			}
			else
			{
				if (this.mStateCountdown == 51)
				{
					Zombie zombie2 = this.mBoard.ZombieTryToGet(this.mTargetZombieID);
					if (zombie2 != null)
					{
						zombie2.DragUnder();
						zombie2.PoolSplash(false);
					}
				}
				if (this.mStateCountdown == 21)
				{
					int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, this.mRow, 0);
					Reanimation reanimation2 = this.mApp.AddReanimation((float)(this.mX - 23), (float)(this.mY + 7), aRenderOrder, ReanimationType.REANIM_SPLASH);
					reanimation2.OverrideScale(1.3f, 1.3f);
					this.mApp.AddTodParticle((float)(this.mX + 31), (float)(this.mY + 64), aRenderOrder, ParticleEffect.PARTICLE_PLANTING_POOL);
					this.mApp.PlayFoley(FoleyType.FOLEY_ZOMBIE_ENTERING_WATER);
				}
				if (this.mStateCountdown <= 0)
				{
					this.Die();
					Zombie zombie3 = this.mBoard.ZombieTryToGet(this.mTargetZombieID);
					if (zombie3 != null)
					{
						zombie3.DieWithLoot();
					}
				}
			}
		}

		public Reanimation AttachBlinkAnim(Reanimation theReanimBody)
		{
			PlantDefinition plantDefinition = Plant.GetPlantDefinition(this.mSeedType);
			LawnApp lawnApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			Reanimation reanimation = theReanimBody;
			string text = GlobalMembersReanimIds.ReanimTrackId_anim_blink;
			string theTrackName = Reanimation.ReanimTrackIdEmpty;
			if (this.mSeedType == SeedType.SEED_WALLNUT || this.mSeedType == SeedType.SEED_TALLNUT || this.mSeedType == SeedType.SEED_EXPLODE_O_NUT || this.mSeedType == SeedType.SEED_GIANT_WALLNUT)
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
			else if (this.mSeedType == SeedType.SEED_THREEPEATER)
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
			else if (this.mSeedType == SeedType.SEED_SPLITPEA)
			{
				if (RandomNumbers.NextNumber(2) == 0)
				{
					text = GlobalMembersReanimIds.ReanimTrackId_anim_blink;
					theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face;
					reanimation = this.mApp.ReanimationTryToGet(this.mHeadReanimID);
				}
				else
				{
					text = GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_blink;
					theTrackName = GlobalMembersReanimIds.ReanimTrackId_splitpea_head;
					reanimation = this.mApp.ReanimationTryToGet(this.mHeadReanimID2);
				}
			}
			else if (this.mSeedType == SeedType.SEED_TWINSUNFLOWER)
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
			else if (this.mSeedType == SeedType.SEED_PEASHOOTER || this.mSeedType == SeedType.SEED_SNOWPEA || this.mSeedType == SeedType.SEED_REPEATER || this.mSeedType == SeedType.SEED_LEFTPEATER || this.mSeedType == SeedType.SEED_GATLINGPEA)
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
			reanimation2.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_FULL_LAST_FRAME_AND_HOLD;
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

		public void UpdateReanimColor()
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
			SeedType seedTypeInCursor = this.mBoard.GetSeedTypeInCursor();
			bool flag = false;
			if (this.IsPartOfUpgradableTo(seedTypeInCursor) && this.mBoard.CanPlantAt(this.mPlantCol, this.mRow, seedTypeInCursor) == PlantingReason.PLANTING_OK)
			{
				SexyColor flashingColor = TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 90);
				if (flashingColor != reanimation.mColorOverride)
				{
					flag = true;
					reanimation.mColorOverride = flashingColor;
				}
			}
			else if (seedTypeInCursor == SeedType.SEED_COBCANNON && this.mSeedType == SeedType.SEED_KERNELPULT && this.mBoard.CanPlantAt(this.mPlantCol - 1, this.mRow, seedTypeInCursor) == PlantingReason.PLANTING_OK)
			{
				SexyColor flashingColor2 = TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 90);
				if (flashingColor2 != reanimation.mColorOverride)
				{
					flag = true;
					reanimation.mColorOverride = flashingColor2;
				}
			}
			else if (this.mSeedType == SeedType.SEED_EXPLODE_O_NUT)
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
			if (this.mHighlighted)
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
				if (this.mImitaterType == SeedType.SEED_IMITATER)
				{
					sexyColor2 = new SexyColor(255, 255, 255, 92);
					if (sexyColor2 != reanimation.mExtraAdditiveColor)
					{
						flag = true;
						reanimation.mExtraAdditiveColor = sexyColor2;
					}
				}
			}
			else if (this.mBeghouledFlashCountdown > 0)
			{
				int theAlpha = TodCommon.TodAnimateCurve(50, 0, this.mBeghouledFlashCountdown % 50, 0, 128, TodCurves.CURVE_BOUNCE);
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
			else if (this.mEatenFlashCountdown > 0)
			{
				int maxNum = 255;
				if (this.mImitaterType == SeedType.SEED_IMITATER)
				{
					maxNum = 128;
				}
				int num = TodCommon.ClampInt(this.mEatenFlashCountdown * 3, 0, maxNum);
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
			if (this.mBeghouledFlashCountdown > 0)
			{
				int theAlpha2 = TodCommon.TodAnimateCurve(50, 0, this.mBeghouledFlashCountdown % 50, 0, 128, TodCurves.CURVE_BOUNCE);
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
			if (this.mSeedType != SeedType.SEED_SUNFLOWER && this.mSeedType != SeedType.SEED_TWINSUNFLOWER && this.mSeedType != SeedType.SEED_SUNSHROOM && reanimation.mEnableExtraAdditiveDraw)
			{
				flag = true;
				reanimation.mEnableExtraAdditiveDraw = false;
			}
			if (flag)
			{
				reanimation.PropogateColorToAttachments();
			}
		}

		public bool IsUpgradableTo(SeedType aUpdatedType)
		{
			if (aUpdatedType == SeedType.SEED_GATLINGPEA && this.mSeedType == SeedType.SEED_REPEATER)
			{
				return true;
			}
			if (aUpdatedType == SeedType.SEED_WINTERMELON && this.mSeedType == SeedType.SEED_MELONPULT)
			{
				return true;
			}
			if (aUpdatedType == SeedType.SEED_TWINSUNFLOWER && this.mSeedType == SeedType.SEED_SUNFLOWER)
			{
				return true;
			}
			if (aUpdatedType == SeedType.SEED_SPIKEROCK && this.mSeedType == SeedType.SEED_SPIKEWEED)
			{
				return true;
			}
			if (aUpdatedType == SeedType.SEED_COBCANNON && this.mSeedType == SeedType.SEED_KERNELPULT)
			{
				if (this.mBoard.IsValidCobCannonSpot(this.mPlantCol, this.mRow))
				{
					return true;
				}
			}
			else
			{
				if (aUpdatedType == SeedType.SEED_GOLD_MAGNET && this.mSeedType == SeedType.SEED_MAGNETSHROOM)
				{
					return true;
				}
				if (aUpdatedType == SeedType.SEED_GLOOMSHROOM && this.mSeedType == SeedType.SEED_FUMESHROOM)
				{
					return true;
				}
				if (aUpdatedType == SeedType.SEED_CATTAIL && this.mSeedType == SeedType.SEED_LILYPAD)
				{
					Plant topPlantAt = this.mBoard.GetTopPlantAt(this.mPlantCol, this.mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
					if (topPlantAt == null || topPlantAt.mSeedType != SeedType.SEED_CATTAIL)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool IsPartOfUpgradableTo(SeedType aUpdatedType)
		{
			if (aUpdatedType == SeedType.SEED_COBCANNON && this.mSeedType == SeedType.SEED_KERNELPULT)
			{
				return this.mBoard.IsValidCobCannonSpot(this.mPlantCol, this.mRow) || this.mBoard.IsValidCobCannonSpot(this.mPlantCol - 1, this.mRow);
			}
			return this.IsUpgradableTo(aUpdatedType);
		}

		public void UpdateCobCannon()
		{
			if (this.mState == PlantState.STATE_COBCANNON_ARMING)
			{
				if (this.mStateCountdown <= 0)
				{
					this.mState = PlantState.STATE_COBCANNON_LOADING;
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_charge, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
					return;
				}
			}
			else if (this.mState == PlantState.STATE_COBCANNON_LOADING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.ShouldTriggerTimedEvent(0.5f))
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_SHOOP);
				}
				if (reanimation.mLoopCount > 0)
				{
					this.mState = PlantState.STATE_COBCANNON_READY;
					this.PlayIdleAnim(12f);
					return;
				}
			}
			else
			{
				if (this.mState == PlantState.STATE_COBCANNON_READY)
				{
					Reanimation reanimation2 = this.mApp.ReanimationGet(this.mBodyReanimID);
					ReanimatorTrackInstance trackInstanceByName = reanimation2.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_cobcannon_cob);
					trackInstanceByName.mTrackColor = TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 75);
					return;
				}
				if (this.mState == PlantState.STATE_COBCANNON_FIRING)
				{
					Reanimation reanimation3 = this.mApp.ReanimationGet(this.mBodyReanimID);
					if (reanimation3.ShouldTriggerTimedEvent(0.48f))
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_COBLAUNCH);
					}
				}
			}
		}

		public void CobCannonFire(int theTargetX, int theTargetY)
		{
			Debug.ASSERT(this.mState == PlantState.STATE_COBCANNON_READY);
			this.mState = PlantState.STATE_COBCANNON_FIRING;
			this.mShootingCounter = 184;
			this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			this.mTargetX = theTargetX - 47;
			this.mTargetY = theTargetY;
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_cobcannon_cob);
			trackInstanceByName.mTrackColor = SexyColor.White;
		}

		public void UpdateGoldMagnetShroom()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			bool flag = false;
			for (int i = 0; i < 5; i++)
			{
				MagnetItem magnetItem = this.mMagnetItems[i];
				if (magnetItem.mItemType != MagnetItemType.MAGNET_ITEM_NONE)
				{
					SexyVector2 sexyVector = new SexyVector2((float)this.mX + magnetItem.mDestOffsetX - magnetItem.mPosX, (float)this.mY + magnetItem.mDestOffsetY - magnetItem.mPosY);
					float num = sexyVector.Magnitude();
					if (num < 20f)
					{
						CoinType theType = CoinType.COIN_ALMANAC;
						if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_SILVER_COIN)
						{
							theType = CoinType.COIN_SILVER;
						}
						else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_GOLD_COIN)
						{
							theType = CoinType.COIN_GOLD;
						}
						else if (magnetItem.mItemType == MagnetItemType.MAGNET_ITEM_DIAMOND)
						{
							theType = CoinType.COIN_DIAMOND;
						}
						else
						{
							Debug.ASSERT(false);
						}
						int coinValue = Coin.GetCoinValue(theType);
						this.mApp.mPlayerInfo.AddCoins(coinValue);
						this.mBoard.mCoinsCollected += coinValue;
						this.mApp.PlayFoley(FoleyType.FOLEY_COIN);
						magnetItem.mItemType = MagnetItemType.MAGNET_ITEM_NONE;
					}
					else
					{
						float num2 = TodCommon.TodAnimateCurveFloatTime(300f, 0f, num, 0.02f, 0.05f, TodCurves.CURVE_LINEAR);
						magnetItem.mPosX += sexyVector.x * num2;
						magnetItem.mPosY += sexyVector.y * num2;
						flag = true;
					}
				}
			}
			if (this.mState == PlantState.STATE_MAGNETSHROOM_CHARGING)
			{
				if (this.mStateCountdown <= 0)
				{
					this.mState = PlantState.STATE_READY;
				}
				return;
			}
			if (this.mState == PlantState.STATE_MAGNETSHROOM_SUCKING)
			{
				if (reanimation.ShouldTriggerTimedEvent(0.4f))
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_MAGNETSHROOM);
					this.GoldMagnetFindTargets();
				}
				if (reanimation.mLoopCount > 0 && !flag)
				{
					this.PlayIdleAnim(14f);
					this.mState = PlantState.STATE_MAGNETSHROOM_CHARGING;
					this.mStateCountdown = TodCommon.RandRangeInt(200, 300);
				}
				return;
			}
			if (this.IsAGoldMagnetAboutToSuck())
			{
				return;
			}
			if (RandomNumbers.NextNumber(50) != 0)
			{
				return;
			}
			Coin coin = this.FindGoldMagnetTarget();
			if (coin != null)
			{
				this.mBoard.ShowCoinBank();
				this.mState = PlantState.STATE_MAGNETSHROOM_SUCKING;
				this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_attract, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			}
		}

		public bool IsOnBoard()
		{
			if (!this.mIsOnBoard)
			{
				return false;
			}
			Debug.ASSERT(this.mBoard != null);
			return true;
		}

		public void RemoveEffects()
		{
			this.mApp.RemoveParticle(this.mParticleID);
			this.mParticleID = null;
			this.mApp.RemoveReanimation(ref this.mBodyReanimID);
			this.mApp.RemoveReanimation(ref this.mHeadReanimID);
			this.mApp.RemoveReanimation(ref this.mHeadReanimID2);
			this.mApp.RemoveReanimation(ref this.mHeadReanimID3);
			this.mApp.RemoveReanimation(ref this.mLightReanimID);
			this.mApp.RemoveReanimation(ref this.mBlinkReanimID);
			this.mApp.RemoveReanimation(ref this.mSleepingReanimID);
		}

		public void UpdateCoffeeBean()
		{
			if (this.mState == PlantState.STATE_DOINGSPECIAL)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					this.Die();
				}
			}
		}

		public void UpdateUmbrella()
		{
			if (this.mState == PlantState.STATE_UMBRELLA_TRIGGERED)
			{
				if (this.mStateCountdown <= 0)
				{
					this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, this.mRow + 1, 0);
					this.mState = PlantState.STATE_UMBRELLA_REFLECTING;
					return;
				}
			}
			else if (this.mState == PlantState.STATE_UMBRELLA_REFLECTING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					this.PlayIdleAnim(0f);
					this.mState = PlantState.STATE_NOTREADY;
					this.mRenderOrder = this.CalcRenderOrder();
				}
			}
		}

		public void EndBlink()
		{
			if (this.mBlinkReanimID == null)
			{
				return;
			}
			this.mApp.RemoveReanimation(ref this.mBlinkReanimID);
			this.mBlinkReanimID = null;
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation != null)
			{
				reanimation.AssignRenderGroupToPrefix("anim_eye", 0);
			}
		}

		public void AnimateGarlic()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			string reanimTrackId_anim_face = GlobalMembersReanimIds.ReanimTrackId_anim_face;
			Image imageOverride = reanimation.GetImageOverride(reanimTrackId_anim_face);
			if (this.mPlantHealth < this.mPlantMaxHealth / 3)
			{
				if (imageOverride != AtlasResources.IMAGE_REANIM_GARLIC_BODY3)
				{
					reanimation.SetImageOverride(reanimTrackId_anim_face, AtlasResources.IMAGE_REANIM_GARLIC_BODY3);
					reanimation.AssignRenderGroupToPrefix("Garlic_stem", -1);
					return;
				}
			}
			else if (this.mPlantHealth < this.mPlantMaxHealth * 2 / 3)
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
			while (this.mBoard.IterateCoins(ref coin2))
			{
				if ((coin2.mType == CoinType.COIN_SILVER || coin2.mType == CoinType.COIN_GOLD || coin2.mType == CoinType.COIN_DIAMOND) && coin2.mCoinMotion != CoinMotion.COIN_MOTION_FROM_PRESENT && !coin2.mIsBeingCollected && coin2.mCoinAge >= 50)
				{
					float num2 = TodCommon.Distance2D((float)this.mX + 40f, (float)this.mY + 40f, coin2.mPosX + (float)(coin2.mWidth / 2), coin2.mPosY + (float)(coin2.mHeight / 2));
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
			Debug.ASSERT(this.IsSpiky());
			if (this.mState != PlantState.STATE_SPIKEWEED_ATTACKING)
			{
				this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_attack, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 18f);
				this.mApp.PlaySample(Resources.SOUND_THROW);
				this.mState = PlantState.STATE_SPIKEWEED_ATTACKING;
				this.mStateCountdown = 99;
			}
		}

		public void ImitaterMorph()
		{
			this.Die();
			Plant plant = this.mBoard.AddPlant(this.mPlantCol, this.mRow, this.mImitaterType, SeedType.SEED_IMITATER);
			FilterEffectType mFilterEffect = FilterEffectType.FILTER_EFFECT_WASHED_OUT;
			if (this.mImitaterType == SeedType.SEED_HYPNOSHROOM || this.mImitaterType == SeedType.SEED_SQUASH || this.mImitaterType == SeedType.SEED_POTATOMINE || this.mImitaterType == SeedType.SEED_GARLIC || this.mImitaterType == SeedType.SEED_LILYPAD)
			{
				mFilterEffect = FilterEffectType.FILTER_EFFECT_LESS_WASHED_OUT;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(plant.mBodyReanimID);
			if (reanimation != null)
			{
				reanimation.mFilterEffect = mFilterEffect;
			}
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(plant.mHeadReanimID);
			if (reanimation2 != null)
			{
				reanimation2.mFilterEffect = mFilterEffect;
			}
			Reanimation reanimation3 = this.mApp.ReanimationTryToGet(plant.mHeadReanimID2);
			if (reanimation3 != null)
			{
				reanimation3.mFilterEffect = mFilterEffect;
			}
			Reanimation reanimation4 = this.mApp.ReanimationTryToGet(plant.mHeadReanimID3);
			if (reanimation4 != null)
			{
				reanimation4.mFilterEffect = mFilterEffect;
			}
		}

		public void UpdateImitater()
		{
			if (this.mState != PlantState.STATE_IMITATER_MORPHING)
			{
				if (this.mStateCountdown <= 0)
				{
					this.mState = PlantState.STATE_IMITATER_MORPHING;
					this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_explode, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 26f);
					return;
				}
			}
			else
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
				if (reanimation.ShouldTriggerTimedEvent(0.8f))
				{
					this.mApp.AddTodParticle((float)(this.mX + 40), (float)(this.mY + 40), 400000, ParticleEffect.PARTICLE_IMITATER_MORPH);
				}
				if (reanimation.mLoopCount > 0)
				{
					this.ImitaterMorph();
				}
			}
		}

		public void UpdateReanim()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			this.UpdateReanimColor();
			float num = this.mShakeOffsetX;
			float num2 = this.mShakeOffsetY + Plant.PlantDrawHeightOffset(this.mBoard, this, this.mSeedType, this.mPlantCol, this.mRow);
			float num3 = 1f;
			float num4 = 1f;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BIG_TIME && (this.mSeedType == SeedType.SEED_WALLNUT || this.mSeedType == SeedType.SEED_SUNFLOWER || this.mSeedType == SeedType.SEED_MARIGOLD))
			{
				num3 = 1.5f;
				num4 = 1.5f;
				num += -20f;
				num2 += -40f;
			}
			if (this.mSeedType == SeedType.SEED_GIANT_WALLNUT)
			{
				num3 = 2f;
				num4 = 2f;
				num += -76f;
				num2 += -64f;
			}
			if (this.mSeedType == SeedType.SEED_INSTANT_COFFEE)
			{
				num3 = 0.8f;
				num4 = 0.8f;
				num += 12f;
				num2 += 10f;
			}
			if (this.mSeedType == SeedType.SEED_POTATOMINE)
			{
				num3 = 0.8f;
				num4 = 0.8f;
				num += 12f;
				num2 += 12f;
			}
			if (this.mState == PlantState.STATE_GRAVEBUSTER_EATING)
			{
				num2 += TodCommon.TodAnimateCurveFloat(400, 0, this.mStateCountdown, 0f, 30f, TodCurves.CURVE_LINEAR);
			}
			if (this.mWakeUpCounter > 0)
			{
				float num5 = TodCommon.TodAnimateCurveFloat(70, 0, this.mWakeUpCounter, 1f, 0.8f, TodCurves.CURVE_EASE_SIN_WAVE);
				num4 *= num5;
				num2 += 80f - num5 * 80f;
			}
			reanimation.Update();
			if (this.mSeedType == SeedType.SEED_LEFTPEATER)
			{
				num += 80f * num3;
				num3 *= -1f;
			}
			if (this.mPottedPlantIndex != -1)
			{
				PottedPlant pottedPlant = this.mApp.mPlayerInfo.mPottedPlant[this.mPottedPlantIndex];
				if (pottedPlant.mFacing == PottedPlant.FacingDirection.FACING_LEFT)
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
				if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_SMALL)
				{
					num6 = 20f;
					thePositionEnd = num6;
					num7 = 40f;
					thePositionEnd2 = num7;
					num8 = 0.5f;
					thePositionEnd3 = num8;
				}
				else if (pottedPlant.mPlantAge == PottedPlantAge.PLANTAGE_MEDIUM)
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
				float num9 = TodCommon.TodAnimateCurveFloat(100, 0, this.mStateCountdown, num6, thePositionEnd, TodCurves.CURVE_LINEAR);
				float num10 = TodCommon.TodAnimateCurveFloat(100, 0, this.mStateCountdown, num7, thePositionEnd2, TodCurves.CURVE_LINEAR);
				float num11 = TodCommon.TodAnimateCurveFloat(100, 0, this.mStateCountdown, num8, thePositionEnd3, TodCurves.CURVE_LINEAR);
				num += num9 * num3;
				num2 += num10 * num4;
				num3 *= num11;
				num4 *= num11;
				num += this.mApp.mZenGarden.ZenPlantOffsetX(pottedPlant);
				num2 += this.mApp.mZenGarden.PlantPottedDrawHeightOffset(this.mSeedType, num4, false);
			}
			reanimation.SetPosition(num * Constants.S, num2 * Constants.S);
			reanimation.OverrideScale(num3, num4);
		}

		public void SpikeRockTakeDamage()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBodyReanimID);
			this.SpikeweedAttack();
			this.mPlantHealth -= 50;
			if (this.mPlantHealth <= 300)
			{
				reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_bigspike3, -1);
			}
			if (this.mPlantHealth <= 150)
			{
				reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_bigspike2, -1);
			}
			if (this.mPlantHealth <= 0)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_SQUISH);
				this.Die();
			}
		}

		public bool IsSpiky()
		{
			return this.mSeedType == SeedType.SEED_SPIKEWEED || this.mSeedType == SeedType.SEED_SPIKEROCK;
		}

		public static void PreloadPlantResources(SeedType theSeedType)
		{
			PlantDefinition plantDefinition = Plant.GetPlantDefinition(theSeedType);
			if (plantDefinition.mReanimationType != ReanimationType.REANIM_NONE)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(plantDefinition.mReanimationType, true);
			}
			if (theSeedType == SeedType.SEED_CHERRYBOMB)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_ZOMBIE_CHARRED, true);
			}
			if (theSeedType == SeedType.SEED_JALAPENO)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_JALAPENO_FIRE, true);
			}
			if (theSeedType == SeedType.SEED_TORCHWOOD)
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_FIRE_PEA, true);
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_JALAPENO_FIRE, true);
			}
			if (Plant.IsNocturnal(theSeedType))
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_SLEEPING, true);
			}
		}

		public bool IsInPlay()
		{
			return this.IsOnBoard() && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mApp.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM;
		}

		public void UpdateNeedsFood()
		{
		}

		public void PlayIdleAnim(float theRate)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			this.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 20, theRate);
			if (this.mApp.IsIZombieLevel())
			{
				reanimation.mAnimRate = 0f;
			}
		}

		public void UpdateFlowerPot()
		{
			if (this.mState == PlantState.STATE_FLOWERPOT_INVULNERABLE && this.mStateCountdown <= 0)
			{
				this.mState = PlantState.STATE_NOTREADY;
			}
		}

		public void UpdateLilypad()
		{
			if (this.mState == PlantState.STATE_LILYPAD_INVULNERABLE && this.mStateCountdown <= 0)
			{
				this.mState = PlantState.STATE_NOTREADY;
			}
		}

		public void GoldMagnetFindTargets()
		{
			for (;;)
			{
				MagnetItem freeMagnetItem = this.GetFreeMagnetItem();
				if (freeMagnetItem == null)
				{
					break;
				}
				Coin coin = this.FindGoldMagnetTarget();
				if (coin == null)
				{
					return;
				}
				freeMagnetItem.mPosX = coin.mPosX + 15f;
				freeMagnetItem.mPosY = coin.mPosY + 15f;
				freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(20f, 40f);
				freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-20f, 0f) + 20f;
				if (coin.mType == CoinType.COIN_SILVER)
				{
					freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_SILVER_COIN;
				}
				else if (coin.mType == CoinType.COIN_GOLD)
				{
					freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_GOLD_COIN;
				}
				else if (coin.mType == CoinType.COIN_DIAMOND)
				{
					freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_DIAMOND;
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
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && !plant.NotOnGround() && plant.mSeedType == SeedType.SEED_GOLD_MAGNET && plant.mState == PlantState.STATE_MAGNETSHROOM_SUCKING)
				{
					Reanimation reanimation = this.mApp.ReanimationGet(plant.mBodyReanimID);
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
			if (this.mSeedType == SeedType.SEED_GOLD_MAGNET)
			{
				for (int i = 0; i < 5; i++)
				{
					MagnetItem magnetItem = this.mMagnetItems[i];
					if (magnetItem.mItemType != MagnetItemType.MAGNET_ITEM_NONE)
					{
						return true;
					}
				}
				return false;
			}
			if (this.mSeedType == SeedType.SEED_MAGNETSHROOM)
			{
				for (int j = 0; j < 5; j++)
				{
					MagnetItem magnetItem2 = this.mMagnetItems[j];
					if (magnetItem2.mItemType != MagnetItemType.MAGNET_ITEM_NONE)
					{
						SexyVector2 sexyVector = new SexyVector2((float)this.mX + magnetItem2.mDestOffsetX - magnetItem2.mPosX, (float)this.mY + magnetItem2.mDestOffsetY - magnetItem2.mPosY);
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
			if (this.mBoard == null || !this.IsOnBoard())
			{
				return;
			}
			if (this.mSeedType == SeedType.SEED_TWINSUNFLOWER)
			{
				this.mBoard.CountPlantByType(this.mSeedType);
			}
			if (this.mSeedType == SeedType.SEED_COBCANNON)
			{
				this.mBoard.CountPlantByType(this.mSeedType);
			}
			if (this.mSeedType >= SeedType.SEED_PEASHOOTER && this.mSeedType < SeedType.SEED_EXPLODE_O_NUT)
			{
				this.mApp.mPlayerInfo.mPlantTypesUsed[(int)this.mSeedType] = true;
				int num = 0;
				while (num < this.mApp.mPlayerInfo.mPlantTypesUsed.Length && this.mApp.mPlayerInfo.mPlantTypesUsed[num])
				{
					num++;
				}
			}
			if (this.mBoard.StageHasFog() && (this.mSeedType == SeedType.SEED_PLANTERN || this.mSeedType == SeedType.SEED_BLOVER))
			{
				this.mBoard.mPlanternOrBloverUsed = true;
			}
			if (this.mBoard.StageIsNight() && (this.mSeedType == SeedType.SEED_WALLNUT || this.mSeedType == SeedType.SEED_TALLNUT))
			{
				this.mBoard.mNutsUsed = true;
			}
			if (this.mSeedType == SeedType.SEED_WINTERMELON)
			{
				uint num2 = 0U;
				int count = this.mBoard.mPlants.Count;
				for (int i = 0; i < count; i++)
				{
					Plant plant = this.mBoard.mPlants[i];
					if (!plant.mDead && plant.mSeedType == SeedType.SEED_WINTERMELON)
					{
						TodCommon.SetBit(ref num2, plant.mRow, 1);
					}
				}
				this.mBoard.StageHas6Rows();
			}
		}

		public static PlantDefinition GetPlantDefinition(SeedType theSeedtype)
		{
			Debug.ASSERT(theSeedtype >= SeedType.SEED_PEASHOOTER && theSeedtype < SeedType.NUM_SEED_TYPES);
			Debug.ASSERT(GameConstants.gPlantDefs[(int)theSeedtype].mSeedType == theSeedtype);
			return GameConstants.gPlantDefs[(int)theSeedtype];
		}

		public override bool SaveToFile(Buffer b)
		{
			base.SaveToFile(b);
			b.WriteString(this.lastPlayedBodyReanim_Name);
			b.WriteFloat(this.lastPlayedBodyReanim_AnimRate);
			b.WriteByte(this.lastPlayedBodyReanim_BlendTime);
			b.WriteLong((int)this.lastPlayedBodyReanim_Type);
			b.WriteLong((int)this.mSeedType);
			b.WriteLong((int)this.mImitaterType);
			b.WriteBoolean(this.mIsOnBoard);
			b.WriteLong(this.mAnimCounter);
			b.WriteBoolean(this.mAnimPing);
			b.WriteLong(this.mBeghouledFlashCountdown);
			b.WriteLong(this.mBlinkCountdown);
			b.WriteBoolean(this.mDead);
			b.WriteLong(this.mDisappearCountdown);
			b.WriteLong(this.mDoSpecialCountdown);
			b.WriteLong(this.mEatenFlashCountdown);
			b.WriteLong(this.mFrame);
			b.WriteLong(this.mFrameLength);
			b.WriteBoolean(this.mHighlighted);
			b.WriteBoolean(this.mInFlowerPot);
			b.WriteBoolean(this.mIsAsleep);
			b.WriteLong(this.mLaunchCounter);
			b.WriteLong(this.mLaunchRate);
			for (int i = 0; i < this.mMagnetItems.Length; i++)
			{
				b.WriteBoolean(this.mMagnetItems[i] != null);
				if (this.mMagnetItems[i] != null)
				{
					this.mMagnetItems[i].SaveToFile(b);
				}
			}
			b.WriteLong(this.mNumFrames);
			b.WriteLong((int)this.mOnBungeeState);
			b.WriteRect(this.mPlantAttackRect);
			b.WriteLong(this.mPlantCol);
			b.WriteLong(this.mPlantHealth);
			b.WriteLong(this.mPlantMaxHealth);
			b.WriteRect(this.mPlantRect);
			b.WriteLong(this.mPottedPlantIndex);
			b.WriteLong(this.mRecentlyEatenCountdown);
			b.WriteFloat(this.mShakeOffsetX);
			b.WriteFloat(this.mShakeOffsetY);
			b.WriteLong(this.mShootingCounter);
			b.WriteBoolean(this.mSquished);
			b.WriteLong(this.mStartRow);
			b.WriteLong((int)this.mState);
			b.WriteLong(this.mStateCountdown);
			b.WriteLong(this.mSubclass);
			b.WriteLong(this.mTargetX);
			b.WriteLong(this.mTargetY);
			GameObject.SaveId(this.mTargetZombieID, b);
			b.WriteLong(this.mWakeUpCounter);
			b.WriteFloat(this.mBodyReanimID.mAnimTime);
			return true;
		}

		public override bool LoadFromFile(Buffer b)
		{
			base.LoadFromFile(b);
			this.lastPlayedBodyReanim_Name = b.ReadString();
			this.lastPlayedBodyReanim_AnimRate = b.ReadFloat();
			this.lastPlayedBodyReanim_BlendTime = b.ReadByte();
			this.lastPlayedBodyReanim_Type = (ReanimLoopType)b.ReadLong();
			this.mSeedType = (SeedType)b.ReadLong();
			this.mImitaterType = (SeedType)b.ReadLong();
			this.mIsOnBoard = b.ReadBoolean();
			this.mBoard = GlobalStaticVars.gLawnApp.mBoard;
			int mX = this.mX;
			int mY = this.mY;
			this.PlantInitialize(this.mBoard.PixelToGridX(this.mX, this.mY), this.mRow, this.mSeedType, this.mImitaterType);
			this.mX = mX;
			this.mY = mY;
			this.mAnimCounter = b.ReadLong();
			this.mAnimPing = b.ReadBoolean();
			this.mBeghouledFlashCountdown = b.ReadLong();
			this.mBlinkCountdown = b.ReadLong();
			this.mDead = b.ReadBoolean();
			this.mDisappearCountdown = b.ReadLong();
			this.mDoSpecialCountdown = b.ReadLong();
			this.mEatenFlashCountdown = b.ReadLong();
			this.mFrame = b.ReadLong();
			this.mFrameLength = b.ReadLong();
			this.mHighlighted = b.ReadBoolean();
			this.mInFlowerPot = b.ReadBoolean();
			this.mIsAsleep = b.ReadBoolean();
			this.mLaunchCounter = b.ReadLong();
			this.mLaunchRate = b.ReadLong();
			for (int i = 0; i < this.mMagnetItems.Length; i++)
			{
				bool flag = b.ReadBoolean();
				if (flag)
				{
					this.mMagnetItems[i] = new MagnetItem();
					this.mMagnetItems[i].LoadFromFile(b);
				}
				else
				{
					this.mMagnetItems[i] = null;
				}
			}
			this.mNumFrames = b.ReadLong();
			this.mOnBungeeState = (PlantOnBungeeState)b.ReadLong();
			this.mPlantAttackRect = b.ReadRect();
			this.mPlantCol = b.ReadLong();
			this.mPlantHealth = b.ReadLong();
			this.mPlantMaxHealth = b.ReadLong();
			this.mPlantRect = b.ReadRect();
			this.mPottedPlantIndex = b.ReadLong();
			this.mRecentlyEatenCountdown = b.ReadLong();
			this.mShakeOffsetX = b.ReadFloat();
			this.mShakeOffsetY = b.ReadFloat();
			this.mShootingCounter = b.ReadLong();
			this.mSquished = b.ReadBoolean();
			this.mStartRow = b.ReadLong();
			this.mState = (PlantState)b.ReadLong();
			this.mStateCountdown = b.ReadLong();
			this.mSubclass = b.ReadLong();
			this.mTargetX = b.ReadLong();
			this.mTargetY = b.ReadLong();
			this.mTargetZombieIDSaved = GameObject.LoadId(b);
			this.mWakeUpCounter = b.ReadLong();
			this.mBodyReanimID.mAnimTime = b.ReadFloat();
			return true;
		}

		public override void LoadingComplete()
		{
			base.LoadingComplete();
			this.mTargetZombieID = (GameObject.GetObjectById(this.mTargetZombieIDSaved) as Zombie);
			if (!this.mIsAsleep && this.mSleepingReanimID != null)
			{
				this.mIsAsleep = true;
				this.SetSleeping(false);
			}
			this.UpdateReanim();
			this.UpdateReanim();
			float mAnimTime = this.mBodyReanimID.mAnimTime;
			if (!string.IsNullOrEmpty(this.lastPlayedBodyReanim_Name))
			{
				this.PlayBodyReanim(this.lastPlayedBodyReanim_Name, this.lastPlayedBodyReanim_Type, 0, this.lastPlayedBodyReanim_AnimRate);
			}
			this.mBodyReanimID.mAnimTime = mAnimTime;
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
				if (theSeedType == SeedType.SEED_LILYPAD || theSeedType == SeedType.SEED_TANGLEKELP || theSeedType == SeedType.SEED_SEASHROOM || theSeedType == SeedType.SEED_CATTAIL)
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
				float num3 = (float)theRow * 3.1415927f + (float)theCol * 3.1415927f * 0.25f;
				float num4 = (float)num2 * 3.1415927f * 2f / 200f;
				float num5 = (float)Math.Sin((double)(num3 + num4)) * 2f;
				num += num5;
			}
			if (theBoard != null && (thePlant == null || !thePlant.mSquished) && thePlant.mInFlowerPot)
			{
				Plant flowerPotAt = theBoard.GetFlowerPotAt(theCol, theRow);
				if (flowerPotAt != null && !flowerPotAt.mSquished && theSeedType != SeedType.SEED_FLOWERPOT)
				{
					num += Plant.PlantFlowerPotHeightOffset(theSeedType, 1f);
				}
			}
			if (theSeedType == SeedType.SEED_FLOWERPOT)
			{
				num += 26f;
			}
			else if (theSeedType == SeedType.SEED_LILYPAD)
			{
				num += 25f;
			}
			else if (theSeedType == SeedType.SEED_STARFRUIT)
			{
				num += 10f;
			}
			else if (theSeedType == SeedType.SEED_TANGLEKELP)
			{
				num += 24f;
			}
			else if (theSeedType == SeedType.SEED_SEASHROOM)
			{
				num += 28f;
			}
			else if (theSeedType == SeedType.SEED_INSTANT_COFFEE)
			{
				num -= 20f;
			}
			else if (Plant.IsFlying(theSeedType))
			{
				num -= 30f;
			}
			else if (theSeedType != SeedType.SEED_CACTUS)
			{
				if (theSeedType == SeedType.SEED_PUMPKINSHELL)
				{
					num += 15f;
				}
				else if (theSeedType == SeedType.SEED_PUFFSHROOM)
				{
					num += 5f;
				}
				else if (theSeedType == SeedType.SEED_SCAREDYSHROOM)
				{
					num -= 14f;
				}
				else if (theSeedType == SeedType.SEED_GRAVEBUSTER)
				{
					num -= 40f;
				}
				else if (theSeedType == SeedType.SEED_SPIKEWEED || theSeedType == SeedType.SEED_SPIKEROCK)
				{
					int num6 = 4;
					if (theBoard != null && theBoard.StageHas6Rows())
					{
						num6 = 5;
					}
					if (theSeedType == SeedType.SEED_SPIKEROCK)
					{
						num += 6f;
					}
					if (theBoard != null && theBoard.GetFlowerPotAt(theCol, theRow) != null && GlobalStaticVars.gLawnApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
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
			if (theSeedType == SeedType.SEED_CHOMPER || theSeedType == SeedType.SEED_PLANTERN)
			{
				num -= 5f;
			}
			else if (theSeedType == SeedType.SEED_SCAREDYSHROOM)
			{
				num += 5f;
				num2 += -8f;
			}
			else if (theSeedType == SeedType.SEED_SUNSHROOM || theSeedType == SeedType.SEED_PUFFSHROOM)
			{
				num2 += -4f;
			}
			else if (theSeedType == SeedType.SEED_HYPNOSHROOM)
			{
				num2 += -8f;
			}
			else if (theSeedType == SeedType.SEED_MAGNETSHROOM)
			{
				num2 += -8f;
			}
			else if (theSeedType == SeedType.SEED_PEASHOOTER || theSeedType == SeedType.SEED_REPEATER || theSeedType == SeedType.SEED_LEFTPEATER || theSeedType == SeedType.SEED_SNOWPEA || theSeedType == SeedType.SEED_THREEPEATER || theSeedType == SeedType.SEED_SUNFLOWER || theSeedType == SeedType.SEED_MARIGOLD)
			{
				num2 += -8f;
			}
			else if (theSeedType == SeedType.SEED_CABBAGEPULT || theSeedType == SeedType.SEED_MELONPULT)
			{
				num2 += -8f;
			}
			else if (theSeedType == SeedType.SEED_TANGLEKELP)
			{
				num2 += -8f;
			}
			else if (theSeedType == SeedType.SEED_BLOVER)
			{
				num2 += -8f;
			}
			else if (theSeedType == SeedType.SEED_SPIKEWEED)
			{
				num2 += -8f;
			}
			else if (theSeedType == SeedType.SEED_SEASHROOM)
			{
				num2 += -4f;
			}
			else if (theSeedType == SeedType.SEED_POTATOMINE)
			{
				num2 += -4f;
			}
			else if (theSeedType == SeedType.SEED_LILYPAD)
			{
				num2 += -16f;
			}
			else if (theSeedType == SeedType.SEED_INSTANT_COFFEE)
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

		public MagnetItem[] mMagnetItems = new MagnetItem[5];

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
