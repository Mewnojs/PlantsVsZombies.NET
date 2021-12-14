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
			lastPlayedBodyReanim_Type = ReanimLoopType.REANIM_PLAY_ONCE;
			lastPlayedBodyReanim_BlendTime = 0;
			lastPlayedBodyReanim_AnimRate = 0f;
			mSeedType = SeedType.SEED_PEASHOOTER;
			mPlantCol = 0;
			mAnimCounter = 0;
			mFrame = 0;
			mFrameLength = 0;
			mNumFrames = 0;
			mState = PlantState.STATE_NOTREADY;
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
			mOnBungeeState = PlantOnBungeeState.PLANT_NOT_ON_BUNGEE;
			mImitaterType = SeedType.SEED_PEASHOOTER;
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
				if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
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
			mState = PlantState.STATE_NOTREADY;
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
			mOnBungeeState = PlantOnBungeeState.PLANT_NOT_ON_BUNGEE;
			mPottedPlantIndex = -1;
			PlantDefinition plantDefinition = Plant.GetPlantDefinition(theSeedType);
			mLaunchRate = plantDefinition.mLaunchRate;
			mSubclass = (int)plantDefinition.mSubClass;
			mRenderOrder = CalcRenderOrder();
			Reanimation reanimation = null;
			string empty = string.Empty;
			if (plantDefinition.mReanimationType != ReanimationType.REANIM_NONE)
			{
				float theY = Plant.PlantDrawHeightOffset(mBoard, this, mSeedType, mPlantCol, mRow);
				reanimation = mApp.AddReanimation(0f, theY, mRenderOrder + 1, plantDefinition.mReanimationType);
				reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
				if (reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_idle))
				{
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
				}
				if (mApp.IsWallnutBowlingLevel() && reanimation.TrackExists(Reanimation.ReanimTrackId__ground))
				{
					reanimation.SetFramesForLayer(Reanimation.ReanimTrackId__ground);
					if (mSeedType == SeedType.SEED_WALLNUT || mSeedType == SeedType.SEED_EXPLODE_O_NUT)
					{
						reanimation.mAnimRate = TodCommon.RandRangeFloat(12f, 18f);
					}
					else if (mSeedType == SeedType.SEED_GIANT_WALLNUT)
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
			if (theSeedType == SeedType.SEED_BLOVER)
			{
				mDoSpecialCountdown = 50;
				if (IsInPlay())
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
					Reanimation reanimation2 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
					reanimation2.mLoopType = ReanimLoopType.REANIM_LOOP;
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
			else if (theSeedType == SeedType.SEED_SPLITPEA)
			{
				Debug.ASSERT(reanimation != null);
				reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
				Reanimation reanimation3 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation3.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation3.mAnimRate = reanimation.mAnimRate;
				reanimation3.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
				reanimation3.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
				mHeadReanimID = mApp.ReanimationGetID(reanimation3);
				Reanimation reanimation4 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation4.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation4.mAnimRate = reanimation.mAnimRate;
				reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_idle);
				reanimation4.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
				mHeadReanimID2 = mApp.ReanimationGetID(reanimation4);
			}
			else if (theSeedType == SeedType.SEED_THREEPEATER)
			{
				Debug.ASSERT(reanimation != null);
				reanimation.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
				Reanimation reanimation5 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation5.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation5.mAnimRate = reanimation.mAnimRate;
				reanimation5.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle1);
				reanimation5.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_head1);
				mHeadReanimID = mApp.ReanimationGetID(reanimation5);
				Reanimation reanimation6 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation6.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation6.mAnimRate = reanimation.mAnimRate;
				reanimation6.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle2);
				reanimation6.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_head2);
				mHeadReanimID2 = mApp.ReanimationGetID(reanimation6);
				Reanimation reanimation7 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
				reanimation7.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation7.mAnimRate = reanimation.mAnimRate;
				reanimation7.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle3);
				reanimation7.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_head3);
				mHeadReanimID3 = mApp.ReanimationGetID(reanimation7);
			}
			else if (theSeedType == SeedType.SEED_WALLNUT)
			{
				mPlantHealth = 4000;
				mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
			}
			else if (theSeedType == SeedType.SEED_EXPLODE_O_NUT)
			{
				mPlantHealth = 4000;
				mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
				reanimation.mColorOverride = new SexyColor(255, 64, 64);
			}
			else if (theSeedType == SeedType.SEED_GIANT_WALLNUT)
			{
				mPlantHealth = 4000;
				mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
			}
			else if (theSeedType == SeedType.SEED_TALLNUT)
			{
				mPlantHealth = 8000;
				mHeight = 80;
				mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
			}
			else if (theSeedType == SeedType.SEED_GARLIC)
			{
				Debug.ASSERT(reanimation != null);
				mPlantHealth = 400;
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
				if (IsInPlay())
				{
					mDoSpecialCountdown = 100;
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_explode);
					reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					mApp.PlayFoley(FoleyType.FOLEY_REVERSE_EXPLOSION);
				}
			}
			else if (theSeedType == SeedType.SEED_IMITATER)
			{
				Debug.ASSERT(reanimation != null);
				reanimation.mAnimRate = TodCommon.RandRangeFloat(25f, 30f);
				mStateCountdown = 200;
			}
			else if (theSeedType == SeedType.SEED_JALAPENO)
			{
				Debug.ASSERT(reanimation != null);
				if (IsInPlay())
				{
					mDoSpecialCountdown = 100;
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_explode);
					reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					mApp.PlayFoley(FoleyType.FOLEY_REVERSE_EXPLOSION);
				}
			}
			else if (theSeedType == SeedType.SEED_POTATOMINE)
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
					mState = PlantState.STATE_POTATO_ARMED;
				}
			}
			else if (theSeedType == SeedType.SEED_GRAVEBUSTER)
			{
				Debug.ASSERT(reanimation != null);
				if (IsInPlay())
				{
					mY += 8;
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_land);
					reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
					mState = PlantState.STATE_GRAVEBUSTER_LANDING;
					mApp.PlayFoley(FoleyType.FOLEY_GRAVEBUSTERCHOMP);
				}
			}
			else if (theSeedType == SeedType.SEED_SUNSHROOM)
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
				mState = PlantState.STATE_SUNSHROOM_SMALL;
				mStateCountdown = 12000;
			}
			else if (theSeedType == SeedType.SEED_PUFFSHROOM || theSeedType == SeedType.SEED_SEASHROOM)
			{
				if (IsInPlay())
				{
					mX += RandomNumbers.NextNumber(10) - 5;
					mY += RandomNumbers.NextNumber(6) - 3;
				}
			}
			else if (theSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				mPlantHealth = 4000;
				mWidth = 120;
				Debug.ASSERT(reanimation != null);
				reanimation.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_pumpkin_back, 1);
			}
			else if (theSeedType == SeedType.SEED_CHOMPER)
			{
				mState = PlantState.STATE_READY;
			}
			else if (theSeedType == SeedType.SEED_PLANTERN)
			{
				mStateCountdown = 50;
				if (!IsOnBoard() || mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
				{
					AddAttachedParticle(mX + 40, mY + 40, 500001, ParticleEffect.PARTICLE_LANTERN_SHINE);
				}
				if (IsInPlay())
				{
					mApp.PlaySample(Resources.SOUND_PLANTERN);
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
					mState = PlantState.STATE_CACTUS_LOW;
				}
				else if (theSeedType == SeedType.SEED_INSTANT_COFFEE)
				{
					mDoSpecialCountdown = 100;
				}
				else if (theSeedType == SeedType.SEED_SCAREDYSHROOM)
				{
					mState = PlantState.STATE_READY;
				}
				else if (theSeedType == SeedType.SEED_COBCANNON)
				{
					if (IsInPlay())
					{
						mState = PlantState.STATE_COBCANNON_ARMING;
						mStateCountdown = 500;
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
					mPlantHealth = 450;
					Debug.ASSERT(reanimation != null);
				}
				else if (theSeedType != SeedType.SEED_SPROUT)
				{
					if (theSeedType == SeedType.SEED_FLOWERPOT)
					{
						if (IsInPlay())
						{
							mState = PlantState.STATE_FLOWERPOT_INVULNERABLE;
							mStateCountdown = 100;
						}
					}
					else if (theSeedType == SeedType.SEED_LILYPAD)
					{
						if (IsInPlay())
						{
							mState = PlantState.STATE_LILYPAD_INVULNERABLE;
							mStateCountdown = 100;
						}
					}
					else if (theSeedType == SeedType.SEED_TANGLEKELP)
					{
						Debug.ASSERT(reanimation != null);
						reanimation.SetTruncateDisappearingFrames(empty, false);
					}
				}
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BIG_TIME && (mSeedType == SeedType.SEED_WALLNUT || mSeedType == SeedType.SEED_SUNFLOWER || mSeedType == SeedType.SEED_MARIGOLD))
			{
				mPlantHealth *= 2;
			}
			mPlantMaxHealth = mPlantHealth;
			if (mSeedType != SeedType.SEED_FLOWERPOT && IsOnBoard())
			{
				Plant flowerPotAt = mBoard.GetFlowerPotAt(mPlantCol, mRow);
				if (flowerPotAt != null)
				{
					Reanimation reanimation8 = mApp.ReanimationGet(flowerPotAt.mBodyReanimID);
					reanimation8.mAnimRate = 0f;
					mInFlowerPot = true;
				}
			}
			if (theImitaterType == SeedType.SEED_IMITATER)
			{
				FilterEffectType aFilterEffect = FilterEffectType.FILTER_EFFECT_WASHED_OUT;
				if (mSeedType == SeedType.SEED_HYPNOSHROOM || mSeedType == SeedType.SEED_SQUASH || mSeedType == SeedType.SEED_POTATOMINE || mSeedType == SeedType.SEED_GARLIC || mSeedType == SeedType.SEED_LILYPAD)
				{
					aFilterEffect = FilterEffectType.FILTER_EFFECT_LESS_WASHED_OUT;
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

		public void Update()
		{
			if ((!IsOnBoard() || mApp.mGameScene != GameScenes.SCENE_LEVEL_INTRO || !mApp.IsWallnutBowlingLevel()) && (!IsOnBoard() || mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN) && (!IsOnBoard() || !mBoard.mCutScene.ShouldRunUpsellBoard()) && IsOnBoard() && mApp.mGameScene != GameScenes.SCENE_PLAYING)
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

		public void Animate()
		{
			if ((mSeedType == SeedType.SEED_CHERRYBOMB || mSeedType == SeedType.SEED_JALAPENO) && mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				mShakeOffsetX = TodCommon.RandRangeFloat(0f, 2f) - 1f;
				mShakeOffsetY = TodCommon.RandRangeFloat(0f, 2f) - 1f;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && mPottedPlantIndex != -1)
			{
				UpdateNeedsFood();
			}
			if (mRecentlyEatenCountdown > 0)
			{
				mRecentlyEatenCountdown -= 3;
			}
			if (mEatenFlashCountdown > 0)
			{
				mEatenFlashCountdown -= 3;
			}
			if (mBeghouledFlashCountdown > 0)
			{
				mBeghouledFlashCountdown -= 3;
			}
			if (mSquished)
			{
				mFrame = 0;
				return;
			}
			if (mSeedType == SeedType.SEED_WALLNUT || mSeedType == SeedType.SEED_TALLNUT)
			{
				AnimateNuts();
			}
			else if (mSeedType == SeedType.SEED_GARLIC)
			{
				AnimateGarlic();
			}
			else if (mSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				AnimatePumpkin();
			}
			UpdateBlink();
			if (mAnimPing)
			{
				int num = mFrameLength * mNumFrames - 1;
				if (mAnimCounter < num)
				{
					mAnimCounter += 3;
				}
				else
				{
					mAnimPing = false;
					mAnimCounter -= mFrameLength;
				}
			}
			else if (mAnimCounter > 0)
			{
				mAnimCounter -= 3;
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
				if (mSeedType == SeedType.SEED_FLOWERPOT)
				{
					num2 -= 15f;
				}
				if (mSeedType == SeedType.SEED_INSTANT_COFFEE)
				{
					num2 -= 20f;
				}
				g.SetScale(1f, 0.5f, 0f, 0f);
				Image imageInAtlasById = AtlasResources.GetImageInAtlasById((int)(10300 + mSeedType));
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(255f * Math.Min(1f, (float)mDisappearCountdown / 100f))));
				Plant.DrawSeedType(g, mSeedType, mImitaterType, DrawVariation.VARIATION_NORMAL, num * Constants.S + (float)(imageInAtlasById.GetCelWidth() / 2) + (float)Constants.Plant_Squished_Offset.X, num2 * Constants.S + (float)imageInAtlasById.GetCelHeight() + (float)Constants.Plant_Squished_Offset.Y);
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
					Plant plant2 = mBoard.GetTopPlantAt(mPlantCol, mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
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
					if (plant2 == null && mSeedType == SeedType.SEED_PUMPKINSHELL)
					{
						flag = true;
					}
				}
				else if (mSeedType == SeedType.SEED_PUMPKINSHELL)
				{
					flag = true;
					plant = this;
				}
			}
			else if (mSeedType == SeedType.SEED_PUMPKINSHELL)
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
				float num4 = (float)(num3 + mRow * 97 + mPlantCol * 61) * 0.03f;
				float num5 = (float)Math.Sin((double)num4) * 2f;
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
				SeedType seedType = SeedType.SEED_NONE;
				if (mBoard != null)
				{
					seedType = mBoard.GetSeedTypeInCursor();
				}
				if (IsPartOfUpgradableTo(seedType) && mBoard.CanPlantAt(mPlantCol, mRow, seedType) == PlantingReason.PLANTING_OK)
				{
					g.SetColorizeImages(true);
					g.SetColor(TodCommon.GetFlashingColor(mBoard.mMainCounter, 90));
				}
				else if (seedType == SeedType.SEED_COBCANNON && mBoard.CanPlantAt(mPlantCol - 1, mRow, seedType) == PlantingReason.PLANTING_OK)
				{
					g.SetColorizeImages(true);
					g.SetColor(TodCommon.GetFlashingColor(mBoard.mMainCounter, 90));
				}
				else if (mBoard != null && mBoard.mTutorialState == TutorialState.TUTORIAL_SHOVEL_DIG)
				{
					g.SetColorizeImages(true);
					g.SetColor(TodCommon.GetFlashingColor(mBoard.mMainCounter, 90));
				}
				if (image != null)
				{
					TodCommon.TodDrawImageCelF(g, image, num, num2, theCelCol, theCelRow);
				}
				if (mSeedType == SeedType.SEED_SPROUT)
				{
					if (mGloveGrabbed)
					{
						g.SetColorizeImages(true);
						g.SetColor(new Color(150, 255, 150, 255));
					}
					TodCommon.TodDrawImageCelF(g, AtlasResources.IMAGE_CACHED_MARIGOLD, (float)Constants.ZenGarden_Marigold_Sprout_Offset.X, (float)Constants.ZenGarden_Marigold_Sprout_Offset.Y, 0, 0);
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
			if (mSeedType == SeedType.SEED_MAGNETSHROOM && !DrawMagnetItemsOnTop())
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
			if (mState == PlantState.STATE_COBCANNON_READY && mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL)
			{
				mBoard.RefreshSeedPacketFromCursor();
				mBoard.mCursorObject.mType = SeedType.SEED_NONE;
				mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_COBCANNON_TARGET;
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
			int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			if (mSeedType == SeedType.SEED_BLOVER)
			{
				if (mState == PlantState.STATE_DOINGSPECIAL)
				{
					return;
				}
				mState = PlantState.STATE_DOINGSPECIAL;
				BlowAwayFliers(mX, mRow);
				return;
			}
			else
			{
				if (mSeedType == SeedType.SEED_CHERRYBOMB)
				{
					mApp.PlayFoley(FoleyType.FOLEY_CHERRYBOMB);
					mApp.PlayFoley(FoleyType.FOLEY_JUICY);
					int num3 = mBoard.KillAllZombiesInRadius(mRow, num, num2, 115, 1, true, damageRangeFlags);
					if (num3 >= 10 && !mApp.IsLittleTroubleLevel())
					{
						mBoard.GrantAchievement(AchievementId.ACHIEVEMENT_EXPLODONATOR, true);
					}
					mApp.AddTodParticle((float)num, (float)num2, 400000, ParticleEffect.PARTICLE_POWIE);
					mApp.Vibrate();
					mBoard.ShakeBoard(3, -4);
					Die();
					return;
				}
				if (mSeedType == SeedType.SEED_DOOMSHROOM)
				{
					mApp.PlaySample(Resources.SOUND_DOOMSHROOM);
					mBoard.KillAllZombiesInRadius(mRow, num, num2, 250, 3, true, damageRangeFlags);
					KillAllPlantsNearDoom();
					mApp.AddTodParticle((float)num, (float)num2, 400000, ParticleEffect.PARTICLE_DOOM);
					GridItem gridItem = mBoard.AddACrater(mPlantCol, mRow);
					gridItem.mGridItemCounter = 18000;
					mBoard.ShakeBoard(3, -4);
					mApp.Vibrate();
					Die();
					mBoard.mDoomsUsed++;
					return;
				}
				if (mSeedType == SeedType.SEED_JALAPENO)
				{
					mApp.PlayFoley(FoleyType.FOLEY_JALAPENO_IGNITE);
					mApp.PlayFoley(FoleyType.FOLEY_JUICY);
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
				if (mSeedType == SeedType.SEED_UMBRELLA)
				{
					if (mState == PlantState.STATE_UMBRELLA_TRIGGERED || mState == PlantState.STATE_UMBRELLA_REFLECTING)
					{
						return;
					}
					mState = PlantState.STATE_UMBRELLA_TRIGGERED;
					mStateCountdown = 5;
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_block, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 22f);
					return;
				}
				else
				{
					if (mSeedType == SeedType.SEED_ICESHROOM)
					{
						mApp.PlayFoley(FoleyType.FOLEY_FROZEN);
						IceZombies();
						mApp.AddTodParticle((float)num, (float)num2, 400000, ParticleEffect.PARTICLE_ICE_TRAP);
						Die();
						return;
					}
					if (mSeedType == SeedType.SEED_POTATOMINE)
					{
						num = mX + mWidth / 2 - 20;
						num2 = mY + mHeight / 2;
						mApp.PlaySample(Resources.SOUND_POTATO_MINE);
						mBoard.KillAllZombiesInRadius(mRow, num, num2, 60, 0, false, damageRangeFlags);
						int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, mRow, 0);
						mApp.AddTodParticle((float)num + 20f, (float)num2, aRenderOrder, ParticleEffect.PARTICLE_POTATO_MINE);
						mBoard.ShakeBoard(3, -4);
						mApp.Vibrate();
						Die();
						return;
					}
					if (mSeedType == SeedType.SEED_INSTANT_COFFEE)
					{
						Plant topPlantAt = mBoard.GetTopPlantAt(mPlantCol, mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
						if (topPlantAt != null && topPlantAt.mIsAsleep)
						{
							topPlantAt.mWakeUpCounter = 100;
						}
						mState = PlantState.STATE_DOINGSPECIAL;
						PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_crumble, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 22f);
						mApp.PlayFoley(FoleyType.FOLEY_COFFEE);
					}
					return;
				}
			}
		}

		public void Fire(Zombie theTargetZombie, int theRow, PlantWeapon thePlantWeapon)
		{
			if (mSeedType == SeedType.SEED_FUMESHROOM)
			{
				DoRowAreaDamage(20, 2U);
				mApp.PlayFoley(FoleyType.FOLEY_FUME);
				return;
			}
			if (mSeedType == SeedType.SEED_GLOOMSHROOM)
			{
				DoRowAreaDamage(20, 2U);
				return;
			}
			if (mSeedType == SeedType.SEED_STARFRUIT)
			{
				StarFruitFire();
				return;
			}
			ProjectileType projectileType = ProjectileType.NUM_PROJECTILES;
			SeedType seedType = mSeedType;
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
			if (mSeedType == SeedType.SEED_KERNELPULT && thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
			{
				projectileType = ProjectileType.PROJECTILE_BUTTER;
			}
			mApp.PlayFoley(FoleyType.FOLEY_THROW);
			if (mSeedType == SeedType.SEED_SNOWPEA || mSeedType == SeedType.SEED_WINTERMELON)
			{
				mApp.PlayFoley(FoleyType.FOLEY_SNOW_PEA_SPARKLES);
			}
			else if (mSeedType == SeedType.SEED_PUFFSHROOM || mSeedType == SeedType.SEED_SCAREDYSHROOM || mSeedType == SeedType.SEED_SEASHROOM)
			{
				mApp.PlayFoley(FoleyType.FOLEY_PUFF);
			}
			int num;
			int num2;
			if (mSeedType == SeedType.SEED_PUFFSHROOM)
			{
				num = mX + 40;
				num2 = mY + 40;
			}
			else if (mSeedType == SeedType.SEED_SEASHROOM)
			{
				num = mX + 45;
				num2 = mY + 63;
			}
			else if (mSeedType == SeedType.SEED_CABBAGEPULT)
			{
				num = mX + 5;
				num2 = mY - 12;
			}
			else if (mSeedType == SeedType.SEED_MELONPULT || mSeedType == SeedType.SEED_WINTERMELON)
			{
				num = mX + 25;
				num2 = mY - 46;
			}
			else if (mSeedType == SeedType.SEED_CATTAIL)
			{
				num = mX + 20;
				num2 = mY - 3;
			}
			else if (mSeedType == SeedType.SEED_KERNELPULT && thePlantWeapon == PlantWeapon.WEAPON_PRIMARY)
			{
				num = mX + 19;
				num2 = mY - 37;
			}
			else if (mSeedType == SeedType.SEED_KERNELPULT && thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
			{
				num = mX + 12;
				num2 = mY - 56;
			}
			else if (mSeedType == SeedType.SEED_PEASHOOTER || mSeedType == SeedType.SEED_SNOWPEA || mSeedType == SeedType.SEED_REPEATER)
			{
				int num3 = 0;
				int num4 = 0;
				GetPeaHeadOffset(ref num3, ref num4);
				num = mX + num3 + 24;
				num2 = mY + num4 + -33;
			}
			else if (mSeedType == SeedType.SEED_LEFTPEATER)
			{
				int num5 = 0;
				int num6 = 0;
				GetPeaHeadOffset(ref num5, ref num6);
				num = mX - num5 + 27;
				num2 = mY + num6 - 33;
			}
			else if (mSeedType == SeedType.SEED_GATLINGPEA)
			{
				int num7 = 0;
				int num8 = 0;
				GetPeaHeadOffset(ref num7, ref num8);
				num = mX + num7 + 34;
				num2 = mY + num8 + -33;
			}
			else if (mSeedType == SeedType.SEED_SPLITPEA)
			{
				int num9 = 0;
				int num10 = 0;
				GetPeaHeadOffset(ref num9, ref num10);
				num2 = mY + num10 + -33;
				if (thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
				{
					num = mX + num9 - 64;
				}
				else
				{
					num = mX + num9 + 24;
				}
			}
			else if (mSeedType == SeedType.SEED_THREEPEATER)
			{
				num2 = mY + 10;
				num = mX + 45;
			}
			else if (mSeedType == SeedType.SEED_SCAREDYSHROOM)
			{
				num = mX + 29;
				num2 = mY + 21;
			}
			else if (mSeedType == SeedType.SEED_CACTUS)
			{
				if (thePlantWeapon == PlantWeapon.WEAPON_PRIMARY)
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
			else if (mSeedType == SeedType.SEED_COBCANNON)
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
			if (mSeedType == SeedType.SEED_SNOWPEA)
			{
				int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_LAWN_MOWER, mRow, 1);
				mApp.AddTodParticle((float)(num + 8), (float)(num2 + 13), aRenderOrder, ParticleEffect.PARTICLE_SNOWPEA_PUFF);
			}
			Projectile projectile = mBoard.AddProjectile(num, num2, mRenderOrder + -1, theRow, projectileType);
			projectile.mDamageRangeFlags = GetDamageRangeFlags(thePlantWeapon);
			if (mSeedType == SeedType.SEED_CABBAGEPULT || mSeedType == SeedType.SEED_KERNELPULT || mSeedType == SeedType.SEED_MELONPULT || mSeedType == SeedType.SEED_WINTERMELON)
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
						int num14 = mBoard.GridToPixelY(8, mRow);
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
			if (mSeedType == SeedType.SEED_THREEPEATER)
			{
				if (theRow < mRow)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_THREEPEATER;
					projectile.mVelY = -3f;
					projectile.mShadowY += 80f;
					return;
				}
				if (theRow > mRow)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_THREEPEATER;
					projectile.mVelY = 3f;
					projectile.mShadowY += -80f;
					return;
				}
			}
			else
			{
				if (mSeedType == SeedType.SEED_PUFFSHROOM || mSeedType == SeedType.SEED_SEASHROOM)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_PUFF;
					return;
				}
				if (mSeedType == SeedType.SEED_SPLITPEA && thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_BACKWARDS;
					return;
				}
				if (mSeedType == SeedType.SEED_LEFTPEATER)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_BACKWARDS;
					return;
				}
				if (mSeedType == SeedType.SEED_CATTAIL)
				{
					projectile.mMotionType = ProjectileMotion.MOTION_HOMING;
					projectile.mVelX = 2f;
					projectile.mTargetZombieID = mBoard.ZombieGetID(theTargetZombie);
					return;
				}
				if (mSeedType == SeedType.SEED_COBCANNON)
				{
					projectile.mDamageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
					projectile.mMotionType = ProjectileMotion.MOTION_LOBBED;
					projectile.mVelX = 0.001f;
					projectile.mVelY = 0f;
					projectile.mAccZ = 0f;
					projectile.mVelZ = -8f;
					projectile.mCobTargetX = (float)(mTargetX - 40);
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
					if (theZombieItem.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						rowDeviation = 0;
					}
					if ((theZombieItem.mHasHead && !theZombieItem.IsTangleKelpTarget()) || (mSeedType != SeedType.SEED_POTATOMINE && mSeedType != SeedType.SEED_CHOMPER && mSeedType != SeedType.SEED_TANGLEKELP))
					{
						bool isPortalCheckNeeded = false;
						if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT && (mSeedType == SeedType.SEED_PEASHOOTER || mSeedType == SeedType.SEED_CACTUS || mSeedType == SeedType.SEED_REPEATER))
						{
							isPortalCheckNeeded = true;
						}
						if (mSeedType != SeedType.SEED_CATTAIL)
						{
							if (mSeedType == SeedType.SEED_GLOOMSHROOM)
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
							if (mSeedType == SeedType.SEED_CATTAIL)
							{
								num3 = Constants.Board_Offset_AspectRatio_Correction;
							}
							if (mSeedType == SeedType.SEED_CHOMPER)
							{
								if (theZombieItem.mZombiePhase == ZombiePhase.PHASE_DIGGER_WALKING)
								{
									plantAttackRect.mX += 20;
									plantAttackRect.mWidth -= 20;
								}
								if (theZombieItem.mZombiePhase == ZombiePhase.PHASE_POGO_BOUNCING || (theZombieItem.mZombieType == ZombieType.ZOMBIE_BUNGEE && theZombieItem.mTargetCol == mPlantCol))
								{
									continue;
								}
								if (theZombieItem.mIsEating || mState == PlantState.STATE_CHOMPER_BITING)
								{
									num3 = 60;
								}
							}
							if (mSeedType == SeedType.SEED_POTATOMINE)
							{
								if ((theZombieItem.mZombieType == ZombieType.ZOMBIE_POGO && theZombieItem.mHasObject) || theZombieItem.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_IN_VAULT || theZombieItem.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT)
								{
									continue;
								}
								if (theZombieItem.mZombieType == ZombieType.ZOMBIE_POLEVAULTER)
								{
									plantAttackRect.mX += 40;
									plantAttackRect.mWidth -= 40; //原版造成土豆雷不爆炸Bug的机制
								}
								if (theZombieItem.mZombieType == ZombieType.ZOMBIE_BUNGEE && theZombieItem.mTargetCol != mPlantCol)
								{
									continue;
								}
								if (theZombieItem.mIsEating)
								{
									num3 = 30;
								}
							}
							if ((mSeedType != SeedType.SEED_EXPLODE_O_NUT || theZombieItem.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_IN_VAULT) && (mSeedType != SeedType.SEED_TANGLEKELP || theZombieItem.mInPool))
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
								if (mSeedType == SeedType.SEED_CATTAIL)
								{
									distance = -(int)TodCommon.Distance2D((float)mX + 40f, (float)mY + 40f, (float)(zombieRect.mX + zombieRect.mWidth / 2), (float)(zombieRect.mY + zombieRect.mHeight / 2));
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
			if (IsOnBoard() && mSeedType == SeedType.SEED_TANGLEKELP)
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
				Plant topPlantAt = mBoard.GetTopPlantAt(mPlantCol, mRow, PlantPriority.TOPPLANT_BUNGEE_ORDER);
				Plant flowerPotAt = mBoard.GetFlowerPotAt(mPlantCol, mRow);
				if (flowerPotAt != null && topPlantAt == flowerPotAt)
				{
					Reanimation reanimation = mApp.ReanimationGet(flowerPotAt.mBodyReanimID);
					reanimation.mAnimRate = TodCommon.RandRangeFloat(10f, 15f);
				}
			}
		}

		public void UpdateProductionPlant()
		{
			if (!IsInPlay())
			{
				return;
			}
			if (mApp.IsIZombieLevel() || mApp.mGameMode == GameMode.GAMEMODE_UPSELL || mApp.mGameMode == GameMode.GAMEMODE_INTRO)
			{
				return;
			}
			if (mBoard.HasLevelAwardDropped())
			{
				return;
			}
			if (mSeedType == SeedType.SEED_MARIGOLD && mBoard.mCurrentWave == mBoard.mNumWaves)
			{
				if (mState != PlantState.STATE_MARIGOLD_ENDING)
				{
					mState = PlantState.STATE_MARIGOLD_ENDING;
					mStateCountdown = 6000;
				}
				else if (mStateCountdown <= 0)
				{
					return;
				}
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && mBoard.mChallenge.mChallengeState != ChallengeState.STATECHALLENGE_LAST_STAND_ONSLAUGHT)
			{
				return;
			}
			mLaunchCounter -= 3;
			if (mLaunchCounter <= 100)
			{
				int num = TodCommon.TodAnimateCurve(100, 0, mLaunchCounter, 0, 100, TodCurves.CURVE_LINEAR);
				mEatenFlashCountdown = Math.Max(mEatenFlashCountdown, num);
			}
			if (mLaunchCounter <= 0)
			{
				mLaunchCounter = TodCommon.RandRangeInt(mLaunchRate - 150, mLaunchRate);
				mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
				if (mSeedType == SeedType.SEED_SUNSHROOM)
				{
					if (mState == PlantState.STATE_SUNSHROOM_SMALL)
					{
						mBoard.AddCoin(mX, mY, CoinType.COIN_SMALLSUN, CoinMotion.COIN_MOTION_FROM_PLANT);
					}
					else
					{
						mBoard.AddCoin(mX, mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
					}
				}
				else if (mSeedType == SeedType.SEED_SUNFLOWER)
				{
					mBoard.AddCoin(mX, mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
				}
				else if (mSeedType == SeedType.SEED_TWINSUNFLOWER)
				{
					mBoard.AddCoin(mX, mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
					mBoard.AddCoin(mX, mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
				}
				else if (mSeedType == SeedType.SEED_MARIGOLD)
				{
					int num2 = RandomNumbers.NextNumber(100);
					CoinType theCoinType = CoinType.COIN_SILVER;
					if (num2 < 10)
					{
						theCoinType = CoinType.COIN_GOLD;
					}
					mBoard.AddCoin(mX, mY, theCoinType, CoinMotion.COIN_MOTION_COIN);
				}
				if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BIG_TIME)
				{
					if (mSeedType == SeedType.SEED_SUNFLOWER)
					{
						mBoard.AddCoin(mX, mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
						return;
					}
					if (mSeedType == SeedType.SEED_MARIGOLD)
					{
						mBoard.AddCoin(mX, mY, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
					}
				}
			}
		}

		public void UpdateShooter()
		{
			mLaunchCounter--;
			if (mLaunchCounter <= 0)
			{
				mLaunchCounter = mLaunchRate - RandomNumbers.NextNumber(15);
				if (mSeedType == SeedType.SEED_THREEPEATER)
				{
					LaunchThreepeater();
				}
				else if (mSeedType == SeedType.SEED_STARFRUIT)
				{
					LaunchStarFruit();
				}
				else if (mSeedType == SeedType.SEED_SPLITPEA)
				{
					FindTargetAndFire(mRow, PlantWeapon.WEAPON_SECONDARY);
					Reanimation reanimation = mApp.ReanimationGet(mHeadReanimID);
					Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
					reanimation.StartBlend(20);
					reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
					reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
					reanimation.mAnimRate = reanimation2.mAnimRate;
					reanimation.mAnimTime = reanimation2.mAnimTime;
				}
				else if (mSeedType == SeedType.SEED_CACTUS)
				{
					if (mState == PlantState.STATE_CACTUS_HIGH)
					{
						FindTargetAndFire(mRow, PlantWeapon.WEAPON_PRIMARY);
					}
					else if (mState == PlantState.STATE_CACTUS_LOW)
					{
						FindTargetAndFire(mRow, PlantWeapon.WEAPON_SECONDARY);
					}
				}
				else
				{
					FindTargetAndFire(mRow, PlantWeapon.WEAPON_PRIMARY);
				}
			}
			if (mLaunchCounter == 50 && mSeedType == SeedType.SEED_CATTAIL)
			{
				FindTargetAndFire(mRow, PlantWeapon.WEAPON_PRIMARY);
			}
			if (mLaunchCounter == 25)
			{
				if (mSeedType == SeedType.SEED_REPEATER || mSeedType == SeedType.SEED_LEFTPEATER)
				{
					FindTargetAndFire(mRow, PlantWeapon.WEAPON_PRIMARY);
					return;
				}
				if (mSeedType == SeedType.SEED_SPLITPEA)
				{
					FindTargetAndFire(mRow, PlantWeapon.WEAPON_PRIMARY);
					FindTargetAndFire(mRow, PlantWeapon.WEAPON_SECONDARY);
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
			if (mSeedType == SeedType.SEED_SPLITPEA && thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
			{
				Reanimation reanimation3 = mApp.ReanimationGet(mHeadReanimID2);
				reanimation3.StartBlend(20);
				reanimation3.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
				reanimation3.mAnimRate = 35f;
				reanimation3.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_shooting);
				mShootingCounter = 26;
			}
			else if (reanimation2 != null && reanimation2.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
			{
				reanimation2.StartBlend(20);
				reanimation2.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
				reanimation2.mAnimRate = 35f;
				reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting);
				mShootingCounter = 33;
				if (mSeedType == SeedType.SEED_REPEATER || mSeedType == SeedType.SEED_SPLITPEA || mSeedType == SeedType.SEED_LEFTPEATER)
				{
					reanimation2.mAnimRate = 45f;
					mShootingCounter = 26;
				}
				else if (mSeedType == SeedType.SEED_GATLINGPEA)
				{
					reanimation2.mAnimRate = 38f;
					mShootingCounter = 100;
				}
			}
			else if (mState == PlantState.STATE_CACTUS_HIGH)
			{
				PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shootinghigh, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 35f);
				mShootingCounter = 23;
			}
			else if (mSeedType == SeedType.SEED_GLOOMSHROOM)
			{
				PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 14f);
				mShootingCounter = 200;
			}
			else if (mSeedType == SeedType.SEED_CATTAIL)
			{
				PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 30f);
				mShootingCounter = 50;
			}
			else if (reanimation != null && reanimation.TrackExists(GlobalMembersReanimIds.ReanimTrackId_anim_shooting))
			{
				PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 35f);
				if (mSeedType == SeedType.SEED_FUMESHROOM)
				{
					mShootingCounter = 50;
				}
				else if (mSeedType == SeedType.SEED_PUFFSHROOM)
				{
					mShootingCounter = 29;
				}
				else if (mSeedType == SeedType.SEED_SCAREDYSHROOM)
				{
					mShootingCounter = 25;
				}
				else if (mSeedType == SeedType.SEED_CABBAGEPULT)
				{
					mShootingCounter = 32;
				}
				else if (mSeedType == SeedType.SEED_MELONPULT || mSeedType == SeedType.SEED_WINTERMELON)
				{
					mShootingCounter = 36;
				}
				else if (mSeedType == SeedType.SEED_KERNELPULT)
				{
					if (RandomNumbers.NextNumber(4) == 0)
					{
						reanimation = mApp.ReanimationGet(mBodyReanimID);
						reanimation.AssignRenderGroupToPrefix("Cornpult_butter", 0);
						reanimation.AssignRenderGroupToPrefix("Cornpult_kernal", -1);
						mState = PlantState.STATE_KERNELPULT_BUTTER;
					}
					mShootingCounter = 30;
				}
				else if (mSeedType == SeedType.SEED_CACTUS)
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
			if (FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY) != null)
			{
				flag = true;
			}
			else if (mBoard.RowCanHaveZombies(theRow) && FindTargetZombie(theRow, PlantWeapon.WEAPON_PRIMARY) != null)
			{
				flag = true;
			}
			else if (mBoard.RowCanHaveZombies(theRow2) && FindTargetZombie(theRow2, PlantWeapon.WEAPON_PRIMARY) != null)
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
				reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
				reanimation.mAnimRate = 20f;
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting1);
			}
			reanimation2.StartBlend(10);
			reanimation2.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
			reanimation2.mAnimRate = 20f;
			reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_shooting2);
			if (mBoard.RowCanHaveZombies(theRow))
			{
				reanimation3.StartBlend(10);
				reanimation3.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
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
			if (!IsInPlay())
			{
				return;
			}
			if (mState == PlantState.STATE_DOINGSPECIAL || mSquished)
			{
				mDisappearCountdown -= 3;
				if (mDisappearCountdown < 0)
				{
					Die();
					return;
				}
			}
			if (mWakeUpCounter > 0)
			{
				mWakeUpCounter -= 3;
				if (mWakeUpCounter >= 60 && mWakeUpCounter < 63)
				{
					mApp.PlayFoley(FoleyType.FOLEY_WAKEUP);
				}
				if (mWakeUpCounter >= 0 && mWakeUpCounter < 3)
				{
					SetSleeping(false);
				}
			}
			if (mIsAsleep || mSquished || mOnBungeeState != PlantOnBungeeState.PLANT_NOT_ON_BUNGEE)
			{
				return;
			}
			UpdateShooting();
			if (mStateCountdown > 0)
			{
				mStateCountdown -= 3;
			}
			if (mApp.IsWallnutBowlingLevel())
			{
				UpdateBowling();
				UpdateBowling();
				UpdateBowling();
				return;
			}
			if (mSeedType == SeedType.SEED_SQUASH)
			{
				UpdateSquash();
			}
			else if (mSeedType == SeedType.SEED_DOOMSHROOM)
			{
				UpdateDoomShroom();
			}
			else if (mSeedType == SeedType.SEED_ICESHROOM)
			{
				UpdateIceShroom();
			}
			else if (mSeedType == SeedType.SEED_CHOMPER)
			{
				UpdateChomper();
			}
			else if (mSeedType == SeedType.SEED_BLOVER)
			{
				UpdateBlover();
			}
			else if (mSeedType == SeedType.SEED_FLOWERPOT)
			{
				UpdateFlowerPot();
			}
			else if (mSeedType == SeedType.SEED_LILYPAD)
			{
				UpdateLilypad();
			}
			else if (mSeedType == SeedType.SEED_IMITATER)
			{
				UpdateImitater();
			}
			else if (mSeedType == SeedType.SEED_INSTANT_COFFEE)
			{
				UpdateCoffeeBean();
			}
			else if (mSeedType == SeedType.SEED_UMBRELLA)
			{
				UpdateUmbrella();
			}
			else if (mSeedType == SeedType.SEED_COBCANNON)
			{
				UpdateCobCannon();
			}
			else if (mSeedType == SeedType.SEED_CACTUS)
			{
				UpdateCactus();
			}
			else if (mSeedType == SeedType.SEED_MAGNETSHROOM)
			{
				UpdateMagnetShroom();
			}
			else if (mSeedType == SeedType.SEED_GOLD_MAGNET)
			{
				UpdateGoldMagnetShroom();
			}
			else if (mSeedType == SeedType.SEED_SUNSHROOM)
			{
				UpdateSunShroom();
			}
			else if (MakesSun() || mSeedType == SeedType.SEED_MARIGOLD)
			{
				UpdateProductionPlant();
			}
			else if (mSeedType == SeedType.SEED_GRAVEBUSTER)
			{
				UpdateGraveBuster();
			}
			else if (mSeedType == SeedType.SEED_TORCHWOOD)
			{
				UpdateTorchwood();
			}
			else if (mSeedType == SeedType.SEED_POTATOMINE)
			{
				UpdatePotato();
			}
			else if (mSeedType == SeedType.SEED_SPIKEWEED || mSeedType == SeedType.SEED_SPIKEROCK)
			{
				UpdateSpikeweed();
			}
			else if (mSeedType == SeedType.SEED_TANGLEKELP)
			{
				UpdateTanglekelp();
			}
			else if (mSeedType == SeedType.SEED_SCAREDYSHROOM)
			{
				UpdateScaredyShroom();
			}
			if (mSubclass == 1)
			{
				UpdateShooter();
				UpdateShooter();
				UpdateShooter();
			}
			if (mDoSpecialCountdown > 0)
			{
				mDoSpecialCountdown -= 3;
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
				if (mSeedType == SeedType.SEED_CHERRYBOMB || mSeedType == SeedType.SEED_JALAPENO || mSeedType == SeedType.SEED_DOOMSHROOM || mSeedType == SeedType.SEED_ICESHROOM)
				{
					DoSpecial();
					return;
				}
				if (mSeedType == SeedType.SEED_POTATOMINE && mState != PlantState.STATE_NOTREADY)
				{
					DoSpecial();
					return;
				}
			}
			if (mSeedType == SeedType.SEED_SQUASH && mState != PlantState.STATE_NOTREADY)
			{
				return;
			}
			mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, mRow, 8);
			if (mSeedType == SeedType.SEED_FLOWERPOT)
			{
				mRenderOrder--;
			}
			mSquished = true;
			mDisappearCountdown = 500;
			mApp.PlayFoley(FoleyType.FOLEY_SQUISH);
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
			int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
			int count = mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = mBoard.mZombies[i];
				if (!zombie.mDead)
				{
					int num = zombie.mRow - mRow;
					if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						num = 0;
					}
					if (mSeedType == SeedType.SEED_GLOOMSHROOM)
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
							if ((zombie.mZombieType == ZombieType.ZOMBIE_ZAMBONI || zombie.mZombieType == ZombieType.ZOMBIE_CATAPULT) && TodCommon.TestBit(theDamageFlags, 5))
							{
								theDamage2 = 1800;
								if (mSeedType == SeedType.SEED_SPIKEROCK)
								{
									SpikeRockTakeDamage();
								}
								else
								{
									Die();
								}
							}
							zombie.TakeDamage(theDamage2, theDamageFlags);
							mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
						}
					}
				}
				IL_108:;
			}
		}

		public int GetDamageRangeFlags(PlantWeapon thePlantWeapon)
		{
			if (mSeedType == SeedType.SEED_CACTUS)
			{
				if (thePlantWeapon == PlantWeapon.WEAPON_SECONDARY)
				{
					return 1;
				}
				return 2;
			}
			else
			{
				if (mSeedType == SeedType.SEED_CHERRYBOMB || mSeedType == SeedType.SEED_JALAPENO || mSeedType == SeedType.SEED_COBCANNON || mSeedType == SeedType.SEED_DOOMSHROOM)
				{
					return 127;
				}
				if (mSeedType == SeedType.SEED_MELONPULT || mSeedType == SeedType.SEED_CABBAGEPULT || mSeedType == SeedType.SEED_KERNELPULT || mSeedType == SeedType.SEED_WINTERMELON)
				{
					return 13;
				}
				if (mSeedType == SeedType.SEED_POTATOMINE)
				{
					return 77;
				}
				if (mSeedType == SeedType.SEED_SQUASH)
				{
					return 13;
				}
				if (mSeedType == SeedType.SEED_PUFFSHROOM || mSeedType == SeedType.SEED_SEASHROOM || mSeedType == SeedType.SEED_FUMESHROOM || mSeedType == SeedType.SEED_GLOOMSHROOM || mSeedType == SeedType.SEED_CHOMPER)
				{
					return 9;
				}
				if (mSeedType == SeedType.SEED_CATTAIL)
				{
					return 11;
				}
				if (mSeedType == SeedType.SEED_TANGLEKELP)
				{
					return 5;
				}
				if (mSeedType == SeedType.SEED_GIANT_WALLNUT)
				{
					return 17;
				}
				return 1;
			}
		}

		public TRect GetPlantRect()
		{
			TRect result = default(TRect);
			if (mSeedType == SeedType.SEED_TALLNUT)
			{
				result = new TRect(mX + 10, mY, mWidth, mHeight);
			}
			else if (mSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				result = new TRect(mX, mY, mWidth - 20, mHeight);
			}
			else if (mSeedType == SeedType.SEED_COBCANNON)
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
			else if (thePlantWeapon == PlantWeapon.WEAPON_SECONDARY && mSeedType == SeedType.SEED_SPLITPEA)
			{
				result = new TRect(0, mY, mX + 16, mHeight);
			}
			else
			{
				SeedType seedType = mSeedType;
				if (seedType <= SeedType.SEED_SEASHROOM)
				{
					switch (seedType)
					{
					case SeedType.SEED_POTATOMINE:
						result = new TRect(mX, mY, mWidth - 25, mHeight);
						return result;
					case SeedType.SEED_SNOWPEA:
					case SeedType.SEED_REPEATER:
					case SeedType.SEED_SUNSHROOM:
						goto IL_27E;
					case SeedType.SEED_CHOMPER:
						result = new TRect(mX + 80, mY, 40, mHeight);
						return result;
					case SeedType.SEED_PUFFSHROOM:
						break;
					case SeedType.SEED_FUMESHROOM:
						result = new TRect(mX + 60, mY, 340, mHeight);
						return result;
					default:
						switch (seedType)
						{
						case SeedType.SEED_SQUASH:
							result = new TRect(mX + 20, mY, mWidth - 35, mHeight);
							return result;
						case SeedType.SEED_THREEPEATER:
						case SeedType.SEED_JALAPENO:
						case SeedType.SEED_TALLNUT:
							goto IL_27E;
						case SeedType.SEED_TANGLEKELP:
							result = new TRect(mX, mY, mWidth, mHeight);
							return result;
						case SeedType.SEED_SPIKEWEED:
							goto IL_15B;
						case SeedType.SEED_TORCHWOOD:
							result = new TRect(mX + 50, mY, 30, mHeight);
							return result;
						case SeedType.SEED_SEASHROOM:
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
				case SeedType.SEED_GLOOMSHROOM:
					result = new TRect(mX - 80, mY - 80, 240, 240);
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
			int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
			int num = 0;
			Zombie zombie = null;
			int count = mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie2 = mBoard.mZombies[i];
				if (!zombie2.mDead)
				{
					int num2 = zombie2.mRow - mRow;
					if (zombie2.mZombieType == ZombieType.ZOMBIE_BOSS)
					{
						num2 = 0;
					}
					if (num2 == 0 && zombie2.mHasHead && !zombie2.IsTangleKelpTarget() && zombie2.EffectedByDamage((uint)damageRangeFlags) && !zombie2.IsSquashTarget(this))
					{
						TRect zombieRect = zombie2.GetZombieRect();
						if ((zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT && zombieRect.mX < mX + 20) || (zombie2.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT && zombie2.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_IN_VAULT && zombie2.mZombiePhase != ZombiePhase.PHASE_SNORKEL_INTO_POOL && zombie2.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_INTO_POOL && zombie2.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_RIDING && zombie2.mZombiePhase != ZombiePhase.PHASE_DOLPHIN_IN_JUMP && !zombie2.IsBobsledTeamWithSled()))
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
								if (zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_POST_VAULT || zombie2.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT || zombie2.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING_IN_POOL || zombie2.mZombieType == ZombieType.ZOMBIE_IMP || zombie2.mZombieType == ZombieType.ZOMBIE_FOOTBALL || mApp.IsScaryPotterLevel())
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

		public void UpdateSquash()
		{
			Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
			Debug.ASSERT(reanimation != null);
			if (mState == PlantState.STATE_NOTREADY)
			{
				Zombie zombie = FindSquashTarget();
				if (zombie == null)
				{
					return;
				}
				mTargetZombieID = mBoard.ZombieGetID(zombie);
				mTargetX = (int)zombie.ZombieTargetLeadX(0f) - mWidth / 2;
				mState = PlantState.STATE_SQUASH_LOOK;
				mStateCountdown = 80;
				if (mTargetX < mX)
				{
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lookleft, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 24f);
				}
				else
				{
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lookright, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 24f);
				}
				mApp.PlayFoley(FoleyType.FOLEY_SQUASH_HMM);
				return;
			}
			else
			{
				if (mState == PlantState.STATE_SQUASH_LOOK)
				{
					if (mStateCountdown <= 0)
					{
						PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpup, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
						mState = PlantState.STATE_SQUASH_PRE_LAUNCH;
						mStateCountdown = 30;
					}
					return;
				}
				if (mState == PlantState.STATE_SQUASH_PRE_LAUNCH)
				{
					if (mStateCountdown <= 0)
					{
						Zombie zombie2 = FindSquashTarget();
						if (zombie2 != null)
						{
							mTargetX = (int)zombie2.ZombieTargetLeadX(30f) - mWidth / 2;
						}
						mState = PlantState.STATE_SQUASH_RISING;
						mStateCountdown = 50;
						mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, mRow, 0);
					}
					return;
				}
				int theGridX = mBoard.PixelToGridXKeepOnBoard(mTargetX, mY);
				int thePositionEnd = mTargetX;
				int num = mBoard.GridToPixelY(theGridX, mRow) + 8;
				if (mState == PlantState.STATE_SQUASH_RISING)
				{
					int thePositionStart = mBoard.GridToPixelX(mPlantCol, mStartRow);
					int thePositionStart2 = mBoard.GridToPixelY(mPlantCol, mStartRow);
					mX = TodCommon.TodAnimateCurve(50, 20, mStateCountdown, thePositionStart, thePositionEnd, TodCurves.CURVE_EASE_IN_OUT);
					mY = TodCommon.TodAnimateCurve(50, 20, mStateCountdown, thePositionStart2, num - 120, TodCurves.CURVE_EASE_IN_OUT);
					if (mStateCountdown <= 0)
					{
						PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_jumpdown, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 60f);
						mState = PlantState.STATE_SQUASH_FALLING;
						mStateCountdown = 10;
						return;
					}
				}
				else if (mState == PlantState.STATE_SQUASH_FALLING)
				{
					mY = TodCommon.TodAnimateCurve(10, 0, mStateCountdown, num - 120, num, TodCurves.CURVE_LINEAR);
					if (mStateCountdown == 4)
					{
						DoSquashDamage();
					}
					if (mStateCountdown <= 0)
					{
						if (mBoard.IsPoolSquare(theGridX, mRow))
						{
							mApp.AddReanimation((float)(mX - 11), (float)(mY + 20), mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
							mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
							mApp.PlaySample(Resources.SOUND_ZOMBIESPLASH);
							Die();
							return;
						}
						mState = PlantState.STATE_SQUASH_DONE_FALLING;
						mStateCountdown = 100;
						mBoard.ShakeBoard(1, 4);
						mApp.PlayFoley(FoleyType.FOLEY_THUMP);
						float num2 = 80f;
						if (mBoard.StageHasRoof())
						{
							num2 -= 11f;
						}
						mApp.AddTodParticle((float)(mX + 40), (float)mY + num2, mRenderOrder + 4, ParticleEffect.PARTICLE_DUST_SQUASH);
						return;
					}
				}
				else if (mState == PlantState.STATE_SQUASH_DONE_FALLING && mStateCountdown <= 0)
				{
					Die();
				}
				return;
			}
		}

		public bool NotOnGround()
		{
			return (mSeedType == SeedType.SEED_SQUASH && (mState == PlantState.STATE_SQUASH_RISING || mState == PlantState.STATE_SQUASH_FALLING || mState == PlantState.STATE_SQUASH_DONE_FALLING)) || mSquished || mOnBungeeState == PlantOnBungeeState.PLANT_RISING_WITH_BUNGEE || mDead;
		}

		public void DoSquashDamage()
		{
			int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
			int num = 0;
			int count = mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = mBoard.mZombies[i];
				if (!zombie.mDead)
				{
					int num2 = zombie.mRow - mRow;
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
			int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			int count = mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = mBoard.mZombies[i];
				if (!zombie.mDead)
				{
					int num = zombie.mRow - mRow;
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
			while (mBoard.IterateGridItems(ref gridItem, ref num2))
			{
				if (gridItem.mGridY == theRow && gridItem.mGridItemType == GridItemType.GRIDITEM_LADDER)
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
					if (zombie.IsFlying() && zombie.mZombiePhase != ZombiePhase.PHASE_BALLOON_POPPING)
					{
						zombie.mBlowingAway = true;
					}
				}
			}
			mApp.PlaySample(Resources.SOUND_BLOVER);
			mBoard.mFogBlownCountDown = 4000;
		}

		public void UpdateGraveBuster()
		{
			Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
			if (mState == PlantState.STATE_GRAVEBUSTER_LANDING)
			{
				if (reanimation.mLoopCount > 0)
				{
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 10, 12f);
					mStateCountdown = 400;
					mState = PlantState.STATE_GRAVEBUSTER_EATING;
					AddAttachedParticle(mX + 40, mY + 40, mRenderOrder + 4, ParticleEffect.PARTICLE_GRAVE_BUSTER);
					return;
				}
			}
			else if (mState == PlantState.STATE_GRAVEBUSTER_EATING && mStateCountdown <= 0)
			{
				GridItem graveStoneAt = mBoard.GetGraveStoneAt(mPlantCol, mRow);
				if (graveStoneAt != null)
				{
					graveStoneAt.GridItemDie();
					mBoard.mGravesCleared++;
				}
				mApp.AddTodParticle((float)(mX + 40), (float)(mY + 40), mRenderOrder + 4, ParticleEffect.PARTICLE_GRAVE_BUSTER_DIE);
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
			TodParticleSystem todParticleSystem2 = mApp.AddTodParticle((float)thePosX, (float)thePosY, theRenderPostition, theEffect);
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
			return mSeedType == SeedType.SEED_SUNFLOWER || mSeedType == SeedType.SEED_TWINSUNFLOWER || mSeedType == SeedType.SEED_SUNSHROOM;
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
			return mBoard != null && mBoard.mGridSquareType[mPlantCol, mRow] == GridSquareType.GRIDSQUARE_HIGH_GROUND;
		}

		public void UpdateTorchwood()
		{
			TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
			int num = -1;
			Projectile projectile = null;
			while (mBoard.IterateProjectiles(ref projectile, ref num))
			{
				if (projectile.mRow == mRow && (projectile.mProjectileType == ProjectileType.PROJECTILE_PEA || projectile.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA))
				{
					TRect projectileRect = projectile.GetProjectileRect();
					int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, projectileRect);
					if (rectOverlap >= 1)
					{
						if (projectile.mProjectileType == ProjectileType.PROJECTILE_PEA)
						{
							projectile.ConvertToFireball(mPlantCol);
						}
						else if (projectile.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
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
			PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shoot, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 28f);
			mShootingCounter = 40;
		}

		public bool FindStarFruitTarget()
		{
			if (mRecentlyEatenCountdown > 0)
			{
				return true;
			}
			int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
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
						if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS && mPlantCol >= 5)
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

		public void UpdateChomper()
		{
			Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
			if (mState == PlantState.STATE_READY)
			{
				Zombie zombie = FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_bite, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 24f);
					mState = PlantState.STATE_CHOMPER_BITING;
					mStateCountdown = 70;
					return;
				}
			}
			else if (mState == PlantState.STATE_CHOMPER_BITING)
			{
				if (mStateCountdown <= 0)
				{
					mApp.PlayFoley(FoleyType.FOLEY_BIGCHOMP);
					Zombie zombie2 = FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY);
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
						mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
						zombie2.TakeDamage(40, 0U);
						mState = PlantState.STATE_CHOMPER_BITING_MISSED;
						return;
					}
					if (flag2)
					{
						mState = PlantState.STATE_CHOMPER_BITING_MISSED;
						return;
					}
					zombie2.DieWithLoot();
					mState = PlantState.STATE_CHOMPER_BITING_GOT_ONE;
					return;
				}
			}
			else if (mState == PlantState.STATE_CHOMPER_BITING_GOT_ONE)
			{
				if (reanimation.mLoopCount > 0)
				{
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_chew, ReanimLoopType.REANIM_LOOP, 0, 15f);
					if (mApp.IsIZombieLevel())
					{
						reanimation.mAnimRate = 0f;
					}
					mState = PlantState.STATE_CHOMPER_DIGESTING;
					mStateCountdown = 4000;
					return;
				}
			}
			else if (mState == PlantState.STATE_CHOMPER_DIGESTING)
			{
				if (mStateCountdown <= 0)
				{
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_swallow, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
					mState = PlantState.STATE_CHOMPER_SWALLOWING;
					return;
				}
			}
			else if ((mState == PlantState.STATE_CHOMPER_SWALLOWING || mState == PlantState.STATE_CHOMPER_BITING_MISSED) && reanimation.mLoopCount > 0)
			{
				PlayIdleAnim(reanimation.mDefinition.mFPS);
				mState = PlantState.STATE_READY;
			}
		}

		public void DoBlink()
		{
			mBlinkCountdown = 400 + RandomNumbers.NextNumber(400);
			if (NotOnGround() || mShootingCounter != 0)
			{
				return;
			}
			if (mSeedType == SeedType.SEED_POTATOMINE && mState != PlantState.STATE_POTATO_ARMED)
			{
				return;
			}
			if (mState == PlantState.STATE_CACTUS_RISING || mState == PlantState.STATE_CACTUS_HIGH || mState == PlantState.STATE_CACTUS_LOWERING || mState == PlantState.STATE_MAGNETSHROOM_SUCKING || mState == PlantState.STATE_MAGNETSHROOM_CHARGING)
			{
				return;
			}
			EndBlink();
			Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
			if (reanimation == null || reanimation.mDead)
			{
				return;
			}
			if (mSeedType == SeedType.SEED_TALLNUT && reanimation.GetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_idle) == AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED2)
			{
				return;
			}
			if (mSeedType == SeedType.SEED_GARLIC && reanimation.GetImageOverride(GlobalMembersReanimIds.ReanimTrackId_anim_face) == AtlasResources.IMAGE_REANIM_GARLIC_BODY3)
			{
				return;
			}
			if (mSeedType == SeedType.SEED_WALLNUT || mSeedType == SeedType.SEED_TALLNUT || mSeedType == SeedType.SEED_EXPLODE_O_NUT || mSeedType == SeedType.SEED_GIANT_WALLNUT)
			{
				mBlinkCountdown = 1000 + RandomNumbers.NextNumber(1000);
			}
			Reanimation reanimation2 = AttachBlinkAnim(reanimation);
			if (reanimation2 != null)
			{
				mBlinkReanimID = mApp.ReanimationGetID(reanimation2);
			}
			reanimation.AssignRenderGroupToPrefix("anim_eye", -1);
		}

		public void UpdateBlink()
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
				mBlinkCountdown -= 3;
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

		public void UpdateMagnetShroom()
		{
			for (int i = 0; i < 5; i++)
			{
				MagnetItem magnetItem = mMagnetItems[i];
				if (magnetItem.mItemType != MagnetItemType.MAGNET_ITEM_NONE)
				{
					SexyVector2 sexyVector = new SexyVector2((float)mX + magnetItem.mDestOffsetX - magnetItem.mPosX, (float)mY + magnetItem.mDestOffsetY - magnetItem.mPosY);
					float num = sexyVector.Magnitude();
					if (num >= 20f)
					{
						magnetItem.mPosX += sexyVector.x * 0.05f;
						magnetItem.mPosY += sexyVector.y * 0.05f;
					}
				}
			}
			if (mState == PlantState.STATE_MAGNETSHROOM_CHARGING)
			{
				if (mStateCountdown <= 0)
				{
					mState = PlantState.STATE_READY;
					float theAnimRate = TodCommon.RandRangeFloat(10f, 15f);
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 30, theAnimRate);
					if (mApp.IsIZombieLevel())
					{
						Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
						reanimation.mAnimRate = 0f;
					}
					MagnetItem magnetItem2 = mMagnetItems[0];
					magnetItem2.mItemType = MagnetItemType.MAGNET_ITEM_NONE;
				}
				return;
			}
			if (mState == PlantState.STATE_MAGNETSHROOM_SUCKING)
			{
				Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_nonactive_idle2, ReanimLoopType.REANIM_LOOP, 20, 2f);
					if (mApp.IsIZombieLevel())
					{
						reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
						reanimation.mAnimRate = 0f;
					}
					mState = PlantState.STATE_MAGNETSHROOM_CHARGING;
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
						if (GameConstants.GetCircleRectOverlap(mX, mY + 20, theRadius, zombieRect))
						{
							float num4 = TodCommon.Distance2D((float)mX, (float)mY, (float)zombieRect.mX, (float)zombieRect.mY);
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
				MagnetShroomAttactItem(zombie);
				return;
			}
			GridItem gridItem = null;
			float num5 = 0f;
			int num6 = -1;
			GridItem gridItem2 = null;
			while (mBoard.IterateGridItems(ref gridItem2, ref num6))
			{
				if (gridItem2.mGridItemType == GridItemType.GRIDITEM_LADDER)
				{
					int num7 = gridItem2.mGridX - mPlantCol;
					int num8 = gridItem2.mGridY - mRow;
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
				mState = PlantState.STATE_MAGNETSHROOM_SUCKING;
				mStateCountdown = 1500;
				PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
				mApp.PlayFoley(FoleyType.FOLEY_MAGNETSHROOM);
				gridItem.GridItemDie();
				MagnetItem freeMagnetItem = GetFreeMagnetItem();
				freeMagnetItem.mPosX = (float)(mBoard.GridToPixelX(gridItem.mGridX, gridItem.mGridY) + 40);
				freeMagnetItem.mPosY = (float)mBoard.GridToPixelY(gridItem.mGridX, gridItem.mGridY);
				freeMagnetItem.mDestOffsetX = TodCommon.RandRangeFloat(-10f, 10f) + 10f;
				freeMagnetItem.mDestOffsetY = TodCommon.RandRangeFloat(-10f, 10f);
				freeMagnetItem.mItemType = MagnetItemType.MAGNET_ITEM_LADDER_PLACED;
			}
		}

		public MagnetItem GetFreeMagnetItem()
		{
			if (mSeedType == SeedType.SEED_GOLD_MAGNET)
			{
				for (int i = 0; i < 5; i++)
				{
					if (mMagnetItems[i].mItemType == MagnetItemType.MAGNET_ITEM_NONE)
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
			for (int i = 0; i < 5; i++)
			{
				MagnetItem magnetItem = mMagnetItems[i];
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
						g.DrawImageCel(theImageStrip, (int)((magnetItem.mPosX - (float)mX + num) * Constants.S), (int)((magnetItem.mPosY - (float)mY + num2) * Constants.S), theCelCol, theCelRow);
					}
					else
					{
						TodCommon.TodDrawImageCelScaledF(g, theImageStrip, (magnetItem.mPosX - (float)mX + num) * Constants.S, (magnetItem.mPosY - (float)mY + num2) * Constants.S, theCelCol, 0, num3, num3);
					}
				}
			}
		}

		public void UpdateDoomShroom()
		{
			if (mIsAsleep || mState == PlantState.STATE_DOINGSPECIAL)
			{
				return;
			}
			mState = PlantState.STATE_DOINGSPECIAL;
			mDoSpecialCountdown = 100;
			Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
			Debug.ASSERT(reanimation != null);
			reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_explode);
			reanimation.mAnimRate = 23f;
			reanimation.mLoopType = ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD;
			reanimation.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_doomshroom_head1, 1f);
			reanimation.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_doomshroom_head2, 2f);
			reanimation.SetShakeOverride(GlobalMembersReanimIds.ReanimTrackId_doomshroom_head3, 2f);
			mApp.PlayFoley(FoleyType.FOLEY_REVERSE_EXPLOSION);
		}

		public void UpdateIceShroom()
		{
			if (mIsAsleep || mState == PlantState.STATE_DOINGSPECIAL)
			{
				return;
			}
			mState = PlantState.STATE_DOINGSPECIAL;
			mDoSpecialCountdown = 100;
		}

		public void UpdatePotato()
		{
			Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
			if (mState == PlantState.STATE_NOTREADY)
			{
				if (mStateCountdown <= 0)
				{
					int num = mX + mWidth / 2;
					int num2 = mY + mHeight / 2;
					mApp.AddTodParticle((float)num, (float)num2, mRenderOrder, ParticleEffect.PARTICLE_POTATO_MINE_RISE);
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_rise, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 18f);
					mState = PlantState.STATE_POTATO_RISING;
					mApp.PlayFoley(FoleyType.FOLEY_DIRT_RISE);
					return;
				}
			}
			else if (mState == PlantState.STATE_POTATO_RISING)
			{
				if (reanimation.mLoopCount > 0)
				{
					float num3 = TodCommon.RandRangeFloat(12f, 15f);
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_armed, ReanimLoopType.REANIM_LOOP, 0, num3);
					PlantDefinition plantDefinition = Plant.GetPlantDefinition(mSeedType);
					Reanimation reanimation2 = mApp.AddReanimation(0f, 0f, mRenderOrder + 2, plantDefinition.mReanimationType);
					reanimation2.mLoopType = ReanimLoopType.REANIM_LOOP;
					reanimation2.mAnimRate = num3 - 2f;
					reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_glow);
					reanimation2.mFrameCount = 10;
					reanimation2.ShowOnlyTrack(GlobalMembersReanimIds.ReanimTrackId_anim_glow);
					reanimation2.SetTruncateDisappearingFrames(GlobalMembersReanimIds.ReanimTrackId_anim_glow, false);
					mLightReanimID = mApp.ReanimationGetID(reanimation2);
					reanimation2.AttachToAnotherReanimation(ref reanimation, GlobalMembersReanimIds.ReanimTrackId_anim_light);
					mState = PlantState.STATE_POTATO_ARMED;
					mBlinkCountdown = 400 + RandomNumbers.NextNumber(400);
					return;
				}
			}
			else if (mState == PlantState.STATE_POTATO_ARMED)
			{
				Zombie zombie = FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					DoSpecial();
					return;
				}
				Reanimation reanimation3 = mApp.ReanimationTryToGet(mLightReanimID);
				if (reanimation3 != null)
				{
					int theTimeAge = DistanceToClosestZombie();
					reanimation3.mFrameCount = (short)TodCommon.TodAnimateCurve(200, 50, theTimeAge, 10, 3, TodCurves.CURVE_LINEAR);
				}
			}
		}

		public int CalcRenderOrder()
		{
			PLANT_ORDER plant_ORDER = PLANT_ORDER.PLANT_ORDER_NORMAL;
			RenderLayer theRenderLayer = RenderLayer.RENDER_LAYER_PLANT;
			int num = 0;
			SeedType seedType = mSeedType;
			if (mSeedType == SeedType.SEED_IMITATER && mImitaterType != SeedType.SEED_NONE)
			{
				seedType = mImitaterType;
			}
			if (mApp.IsWallnutBowlingLevel())
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
			else if (seedType == SeedType.SEED_LILYPAD && mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				plant_ORDER = PLANT_ORDER.PLANT_ORDER_LILYPAD;
			}
			if (seedType == SeedType.SEED_COBCANNON)
			{
				num = 0;
			}
			return Board.MakeRenderOrder(theRenderLayer, mRow, (int)plant_ORDER * 5 - mX + num);
		}

		public void AnimateNuts()
		{
			Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			Image image;
			Image image2;
			string theTrackName;
			if (mSeedType == SeedType.SEED_WALLNUT)
			{
				image = AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED1;
				image2 = AtlasResources.IMAGE_REANIM_WALLNUT_CRACKED2;
				theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_face;
			}
			else
			{
				if (mSeedType != SeedType.SEED_TALLNUT)
				{
					return;
				}
				image = AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED1;
				image2 = AtlasResources.IMAGE_REANIM_TALLNUT_CRACKED2;
				theTrackName = GlobalMembersReanimIds.ReanimTrackId_anim_idle;
			}
			int num = mX + 40;
			int num2 = mY + 10;
			if (mSeedType == SeedType.SEED_TALLNUT)
			{
				num2 -= 32;
			}
			Image imageOverride = reanimation.GetImageOverride(theTrackName);
			if (mPlantHealth < mPlantMaxHealth / 3)
			{
				if (imageOverride != image2)
				{
					reanimation.SetImageOverride(theTrackName, image2);
					mApp.AddTodParticle((float)num, (float)num2, mRenderOrder + 4, ParticleEffect.PARTICLE_WALLNUT_EAT_LARGE);
				}
			}
			else if (mPlantHealth < mPlantMaxHealth * 2 / 3)
			{
				if (imageOverride != image)
				{
					reanimation.SetImageOverride(theTrackName, image);
					mApp.AddTodParticle((float)num, (float)num2, mRenderOrder + 4, ParticleEffect.PARTICLE_WALLNUT_EAT_LARGE);
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
				if (reanimation.mAnimRate < 1f && mOnBungeeState != PlantOnBungeeState.PLANT_RISING_WITH_BUNGEE)
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
				float num = (float)mX + 50f;
				float num2 = (float)mY + 40f;
				if (mSeedType == SeedType.SEED_FUMESHROOM)
				{
					num += 12f;
				}
				else if (mSeedType == SeedType.SEED_SCAREDYSHROOM)
				{
					num2 -= 20f;
				}
				else if (mSeedType == SeedType.SEED_GLOOMSHROOM)
				{
					num2 -= 12f;
				}
				Reanimation reanimation = mApp.AddReanimation(num, num2, mRenderOrder + 2, ReanimationType.REANIM_SLEEPING);
				reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
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
				if (!IsInPlay() && mSeedType == SeedType.SEED_SUNSHROOM)
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
			if (!IsInPlay() && mSeedType == SeedType.SEED_SUNSHROOM)
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

		public void UpdateShooting()
		{
			if (NotOnGround())
			{
				return;
			}
			if (mShootingCounter == 0)
			{
				return;
			}
			mShootingCounter -= 3;
			if (mSeedType == SeedType.SEED_FUMESHROOM && mShootingCounter >= 15 && mShootingCounter < 18)
			{
				int theRenderPostition = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, mRow, 0);
				AddAttachedParticle(mX + 85, mY + 31, theRenderPostition, ParticleEffect.PARTICLE_FUMECLOUD);
			}
			Reanimation reanimation4;
			Reanimation reanimation6;
			if (mSeedType == SeedType.SEED_GLOOMSHROOM)
			{
				if ((mShootingCounter >= 136 && mShootingCounter < 139) || (mShootingCounter >= 108 && mShootingCounter < 111) || (mShootingCounter >= 80 && mShootingCounter < 83) || (mShootingCounter >= 52 && mShootingCounter < 55))
				{
					int theRenderPostition2 = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, mRow, 0);
					AddAttachedParticle(mX + 40, mY + 40, theRenderPostition2, ParticleEffect.PARTICLE_GLOOMCLOUD);
				}
				if ((mShootingCounter >= 126 && mShootingCounter < 129) || (mShootingCounter >= 98 && mShootingCounter < 101) || (mShootingCounter >= 70 && mShootingCounter < 73) || (mShootingCounter >= 42 && mShootingCounter < 45))
				{
					Fire(null, mRow, PlantWeapon.WEAPON_PRIMARY);
				}
			}
			else if (mSeedType == SeedType.SEED_GATLINGPEA)
			{
				if ((mShootingCounter >= 18 && mShootingCounter < 21) || (mShootingCounter >= 35 && mShootingCounter < 38) || (mShootingCounter >= 51 && mShootingCounter < 54) || (mShootingCounter >= 68 && mShootingCounter < 71))
				{
					Fire(null, mRow, PlantWeapon.WEAPON_PRIMARY);
				}
			}
			else if (mSeedType == SeedType.SEED_CATTAIL)
			{
				if (mShootingCounter >= 19 && mShootingCounter < 22)
				{
					Zombie zombie = FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY);
					if (zombie != null)
					{
						Fire(zombie, mRow, PlantWeapon.WEAPON_PRIMARY);
					}
				}
			}
			else if (mShootingCounter >= 1 && mShootingCounter < 4)
			{
				if (mSeedType == SeedType.SEED_THREEPEATER)
				{
					int theRow = mRow - 1;
					int theRow2 = mRow + 1;
					Reanimation reanimation = mApp.ReanimationTryToGet(mHeadReanimID);
					Reanimation reanimation2 = mApp.ReanimationTryToGet(mHeadReanimID2);
					Reanimation reanimation3 = mApp.ReanimationTryToGet(mHeadReanimID3);
					if (reanimation.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						Fire(null, theRow2, PlantWeapon.WEAPON_PRIMARY);
					}
					if (reanimation2.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						Fire(null, mRow, PlantWeapon.WEAPON_PRIMARY);
					}
					if (reanimation3.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						Fire(null, theRow, PlantWeapon.WEAPON_PRIMARY);
						return;
					}
				}
				else if (mSeedType == SeedType.SEED_SPLITPEA)
				{
					reanimation4 = mApp.ReanimationTryToGet(mHeadReanimID);
					Reanimation reanimation5 = mApp.ReanimationTryToGet(mHeadReanimID2);
					if (reanimation4.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						Fire(null, mRow, PlantWeapon.WEAPON_PRIMARY);
					}
					if (reanimation5.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD)
					{
						Fire(null, mRow, PlantWeapon.WEAPON_SECONDARY);
						return;
					}
				}
				else
				{
					if (mState == PlantState.STATE_CACTUS_LOW)
					{
						Fire(null, mRow, PlantWeapon.WEAPON_SECONDARY);
						return;
					}
					if (mSeedType == SeedType.SEED_CABBAGEPULT || mSeedType == SeedType.SEED_KERNELPULT || mSeedType == SeedType.SEED_MELONPULT || mSeedType == SeedType.SEED_WINTERMELON)
					{
						PlantWeapon thePlantWeapon = PlantWeapon.WEAPON_PRIMARY;
						if (mState == PlantState.STATE_KERNELPULT_BUTTER)
						{
							reanimation6 = mApp.ReanimationGet(mBodyReanimID);
							reanimation6.AssignRenderGroupToPrefix("Cornpult_butter", -1);
							reanimation6.AssignRenderGroupToPrefix("Cornpult_kernal", 0);
							mState = PlantState.STATE_NOTREADY;
							thePlantWeapon = PlantWeapon.WEAPON_SECONDARY;
						}
						Zombie theTargetZombie = FindTargetZombie(mRow, thePlantWeapon);
						Fire(theTargetZombie, mRow, thePlantWeapon);
						return;
					}
					Fire(null, mRow, PlantWeapon.WEAPON_PRIMARY);
				}
				return;
			}
			if (mShootingCounter > 0)
			{
				return;
			}
			reanimation6 = mApp.ReanimationTryToGet(mBodyReanimID);
			reanimation4 = mApp.ReanimationTryToGet(mHeadReanimID);
			if (mSeedType == SeedType.SEED_THREEPEATER)
			{
				Reanimation reanimation7 = reanimation4;
				Reanimation reanimation8 = mApp.ReanimationTryToGet(mHeadReanimID2);
				Reanimation reanimation9 = mApp.ReanimationTryToGet(mHeadReanimID3);
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
				if (mSeedType == SeedType.SEED_SPLITPEA)
				{
					Reanimation reanimation10 = mApp.ReanimationGet(mHeadReanimID2);
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
				if (mState == PlantState.STATE_CACTUS_HIGH)
				{
					if (reanimation6.mLoopCount > 0)
					{
						PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idlehigh, ReanimLoopType.REANIM_LOOP, 20, 0f);
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
						reanimation4.mLoopType = ReanimLoopType.REANIM_LOOP;
						reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
						reanimation4.mAnimRate = reanimation6.mAnimRate;
						reanimation4.mAnimTime = reanimation6.mAnimTime;
						return;
					}
				}
				else if (mSeedType == SeedType.SEED_COBCANNON)
				{
					if (reanimation6.mLoopCount > 0)
					{
						mState = PlantState.STATE_COBCANNON_ARMING;
						mStateCountdown = 3000;
						PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_unarmed_idle, ReanimLoopType.REANIM_LOOP, 20, reanimation6.mDefinition.mFPS);
						return;
					}
				}
				else if (reanimation6 != null && reanimation6.mLoopCount > 0)
				{
					PlayIdleAnim(reanimation6.mDefinition.mFPS);
					return;
				}
			}
			mShootingCounter = 3;
		}

		public void DrawShadow(Graphics g, float theOffsetX, float theOffsetY)
		{
			if (mSeedType == SeedType.SEED_LILYPAD || mSeedType == SeedType.SEED_STARFRUIT || mSeedType == SeedType.SEED_TANGLEKELP || mSeedType == SeedType.SEED_SEASHROOM || mSeedType == SeedType.SEED_COBCANNON || mSeedType == SeedType.SEED_SPIKEWEED || mSeedType == SeedType.SEED_SPIKEROCK || mSeedType == SeedType.SEED_GRAVEBUSTER || mSeedType == SeedType.SEED_CATTAIL || mOnBungeeState == PlantOnBungeeState.PLANT_RISING_WITH_BUNGEE)
			{
				return;
			}
			if (IsOnBoard() && mBoard.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && mBoard.mApp.mZenGarden.mGardenType == GardenType.GARDEN_MAIN)
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
			if (mSeedType == SeedType.SEED_SQUASH)
			{
				if (mBoard != null)
				{
					num3 += (float)(mBoard.GridToPixelY(mPlantCol, mRow) - mY);
				}
				num3 += 5f;
			}
			else if (mSeedType == SeedType.SEED_PUFFSHROOM || mSeedType == SeedType.SEED_SEASHROOM)
			{
				num4 = 0.5f;
				num3 -= 9f;
			}
			else if (mSeedType == SeedType.SEED_SUNSHROOM)
			{
				num3 += -9f;
				if (mState == PlantState.STATE_SUNSHROOM_SMALL)
				{
					num4 = 0.5f;
				}
				else if (mState == PlantState.STATE_SUNSHROOM_GROWING)
				{
					Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
					num4 = 0.5f + 0.5f * reanimation.mAnimTime;
				}
				else
				{
					num4 = 1f;
				}
			}
			else if (mSeedType == SeedType.SEED_UMBRELLA)
			{
				num4 = 0.5f;
				num2 -= 4f;
				num3 += 1f;
			}
			else if (mSeedType == SeedType.SEED_FUMESHROOM || mSeedType == SeedType.SEED_GLOOMSHROOM)
			{
				num4 = 1.3f;
				num3 -= 4f;
			}
			else if (mSeedType == SeedType.SEED_CABBAGEPULT || mSeedType == SeedType.SEED_MELONPULT || mSeedType == SeedType.SEED_WINTERMELON)
			{
				num3 -= 4f;
			}
			else if (mSeedType == SeedType.SEED_KERNELPULT)
			{
				num2 += 3f;
				num3 -= 4f;
			}
			else if (mSeedType == SeedType.SEED_SCAREDYSHROOM)
			{
				num2 += -6f;
				num3 += 4f;
			}
			else if (mSeedType == SeedType.SEED_CHOMPER)
			{
				num2 += -18f;
				num3 += 6f;
			}
			else if (mSeedType == SeedType.SEED_FLOWERPOT)
			{
				num2 += -1f;
				num3 += -5f;
			}
			else if (mSeedType == SeedType.SEED_TALLNUT)
			{
				num3 += 3f;
				num4 = 1.3f;
			}
			else if (mSeedType == SeedType.SEED_PUMPKINSHELL)
			{
				num3 += -5f;
				num4 = 1.4f;
			}
			else if (mSeedType == SeedType.SEED_CACTUS)
			{
				num2 += -5f;
				num3 += -1f;
			}
			else if (mSeedType == SeedType.SEED_PLANTERN)
			{
				num3 += 6f;
			}
			else if (mSeedType == SeedType.SEED_INSTANT_COFFEE)
			{
				num3 += 20f;
			}
			else if (mSeedType == SeedType.SEED_GIANT_WALLNUT)
			{
				num2 -= 30f;
				num3 += 5f;
				num4 = 1.7f;
			}
			if (Plant.IsFlying(mSeedType))
			{
				num3 += 10f;
				if (mBoard != null && (mBoard.GetTopPlantAt(mPlantCol, mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION) != null || mBoard.GetTopPlantAt(mPlantCol, mRow, PlantPriority.TOPPLANT_ONLY_PUMPKIN) != null))
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
					if (zombie.mZombieType == ZombieType.ZOMBIE_BOSS)
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
			if (mState == PlantState.STATE_READY)
			{
				if (flag)
				{
					mState = PlantState.STATE_SCAREDYSHROOM_LOWERING;
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_scared, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 10f);
				}
			}
			else if (mState == PlantState.STATE_SCAREDYSHROOM_LOWERING)
			{
				if (reanimation.mLoopCount > 0)
				{
					mState = PlantState.STATE_SCAREDYSHROOM_SCARED;
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_scaredidle, ReanimLoopType.REANIM_LOOP, 10, 0f);
				}
			}
			else if (mState == PlantState.STATE_SCAREDYSHROOM_SCARED)
			{
				if (!flag)
				{
					mState = PlantState.STATE_SCAREDYSHROOM_RAISING;
					float theAnimRate = TodCommon.RandRangeFloat(7f, 12f);
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_grow, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, theAnimRate);
				}
			}
			else if (mState == PlantState.STATE_SCAREDYSHROOM_RAISING && reanimation.mLoopCount > 0)
			{
				mState = PlantState.STATE_READY;
				float theRate = TodCommon.RandRangeFloat(10f, 15f);
				PlayIdleAnim(theRate);
			}
			if (mState != PlantState.STATE_READY)
			{
				mLaunchCounter = mLaunchRate;
			}
		}

		public int DistanceToClosestZombie()
		{
			int damageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
			TRect plantAttackRect = GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
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

		public void UpdateSpikeweed()
		{
			Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
			if (mState == PlantState.STATE_SPIKEWEED_ATTACKING)
			{
				if (mStateCountdown <= 0)
				{
					mState = PlantState.STATE_NOTREADY;
				}
				else if (mSeedType == SeedType.SEED_SPIKEROCK)
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
				Zombie zombie = FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					SpikeweedAttack();
				}
			}
		}

		public void MagnetShroomAttactItem(Zombie theZombie)
		{
			mState = PlantState.STATE_MAGNETSHROOM_SUCKING;
			mStateCountdown = 1500;
			PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			mApp.PlayFoley(FoleyType.FOLEY_MAGNETSHROOM);
			MagnetItem freeMagnetItem = GetFreeMagnetItem();
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
			Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
			if (mState == PlantState.STATE_SUNSHROOM_SMALL)
			{
				if (mStateCountdown <= 0)
				{
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_grow, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 12f);
					mState = PlantState.STATE_SUNSHROOM_GROWING;
					mApp.PlayFoley(FoleyType.FOLEY_PLANTGROW);
				}
				UpdateProductionPlant();
				return;
			}
			if (mState == PlantState.STATE_SUNSHROOM_GROWING)
			{
				if (reanimation.mLoopCount > 0)
				{
					float theAnimRate = TodCommon.RandRangeFloat(12f, 15f);
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_bigidle, ReanimLoopType.REANIM_LOOP, 10, theAnimRate);
					mState = PlantState.STATE_SUNSHROOM_BIG;
					return;
				}
			}
			else
			{
				UpdateProductionPlant();
			}
		}

		public void UpdateBowling()
		{
			Reanimation reanimation = mApp.ReanimationTryToGet(mBodyReanimID);
			if (reanimation != null && reanimation.TrackExists(Reanimation.ReanimTrackId__ground))
			{
				float num = reanimation.GetTrackVelocity(Reanimation.ReanimTrackId__ground) / 4f;
				if (mSeedType == SeedType.SEED_GIANT_WALLNUT)
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
			if (mState == PlantState.STATE_BOWLING_UP)
			{
				mY -= num2;
			}
			else if (mState == PlantState.STATE_BOWLING_DOWN)
			{
				mY += num2;
			}
			int num3 = mBoard.GridToPixelY(0, mRow) - mY;
			if (num3 > 2 || num3 < -2)
			{
				return;
			}
			PlantState plantState = mState;
			if (plantState == PlantState.STATE_BOWLING_UP && mRow <= 0)
			{
				plantState = PlantState.STATE_BOWLING_DOWN;
			}
			else if (plantState == PlantState.STATE_BOWLING_DOWN && mRow >= 4)
			{
				plantState = PlantState.STATE_BOWLING_UP;
			}
			Zombie zombie = FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY);
			if (zombie != null)
			{
				int num4 = mX + mWidth / 2;
				int num5 = mY + mHeight / 2;
				if (mSeedType == SeedType.SEED_EXPLODE_O_NUT)
				{
					mApp.PlayFoley(FoleyType.FOLEY_CHERRYBOMB);
					mApp.PlaySample(Resources.SOUND_BOWLINGIMPACT2);
					int theDamageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY) | 32;
					mBoard.KillAllZombiesInRadius(mRow, num4, num5, 90, 1, true, theDamageRangeFlags);
					mApp.AddTodParticle((float)num4, (float)num5, 400000, ParticleEffect.PARTICLE_POWIE);
					mBoard.ShakeBoard(3, -4);
					Die();
					return;
				}
				mApp.PlayFoley(FoleyType.FOLEY_BOWLINGIMPACT);
				mBoard.ShakeBoard(1, -2);
				if (mSeedType == SeedType.SEED_GIANT_WALLNUT)
				{
					zombie.TakeDamage(1800, 0U);
				}
				else if (zombie.mShieldType == ShieldType.SHIELDTYPE_DOOR && mState != PlantState.STATE_NOTREADY)
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
						mApp.PlayFoley(FoleyType.FOLEY_SHIELD_HIT);
					}
					else if (zombie.mHelmType == HelmType.HELMTYPE_TRAFFIC_CONE)
					{
						mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
					}
					zombie.TakeHelmDamage(900, 0U);
				}
				else
				{
					zombie.TakeDamage(1800, 0U);
				}
				if ((!mApp.IsFirstTimeAdventureMode() || mBoard.mLevel > 10) && mSeedType == SeedType.SEED_WALLNUT)
				{
					mLaunchCounter++;
					if (mLaunchCounter == 2)
					{
						mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
						mBoard.AddCoin(num4, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
					}
					else if (mLaunchCounter == 3)
					{
						mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
						mBoard.AddCoin(num4 - 5, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
						mBoard.AddCoin(num4 + 5, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
					}
					else if (mLaunchCounter == 4)
					{
						mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
						mBoard.AddCoin(num4 - 10, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
						mBoard.AddCoin(num4, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
						mBoard.AddCoin(num4 + 10, num5, CoinType.COIN_SILVER, CoinMotion.COIN_MOTION_COIN);
					}
					else if (mLaunchCounter >= 5)
					{
						mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
						mBoard.AddCoin(num4, num5, CoinType.COIN_GOLD, CoinMotion.COIN_MOTION_COIN);
						mBoard.GrantAchievement(AchievementId.ACHIEVEMENT_ROLL_SOME_HEADS, true);
					}
				}
				if (mSeedType != SeedType.SEED_GIANT_WALLNUT)
				{
					if (mRow == 4 || mState == PlantState.STATE_BOWLING_DOWN)
					{
						plantState = PlantState.STATE_BOWLING_UP;
					}
					else if (mRow == 0 || mState == PlantState.STATE_BOWLING_UP)
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
				mState = PlantState.STATE_BOWLING_UP;
				mRow--;
				mRenderOrder = CalcRenderOrder();
				return;
			}
			if (plantState == PlantState.STATE_BOWLING_DOWN)
			{
				mState = PlantState.STATE_BOWLING_DOWN;
				mRenderOrder = CalcRenderOrder();
				mRow++;
			}
		}

		public void AnimatePumpkin()
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

		public void UpdateBlover()
		{
			Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
			if (reanimation.mLoopCount > 0 && reanimation.mLoopType != ReanimLoopType.REANIM_LOOP)
			{
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_loop);
				reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
			}
			if (mState != PlantState.STATE_DOINGSPECIAL && mDoSpecialCountdown == 0)
			{
				DoSpecial();
			}
		}

		public void UpdateCactus()
		{
			if (mShootingCounter > 0)
			{
				return;
			}
			Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
			if (mState == PlantState.STATE_CACTUS_RISING)
			{
				if (reanimation.mLoopCount > 0)
				{
					mState = PlantState.STATE_CACTUS_HIGH;
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idlehigh, ReanimLoopType.REANIM_LOOP, 20, 0f);
					if (mApp.IsIZombieLevel())
					{
						reanimation.mAnimRate = 0f;
					}
					mLaunchCounter = 1;
					return;
				}
			}
			else if (mState == PlantState.STATE_CACTUS_HIGH)
			{
				if (FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY) == null)
				{
					mState = PlantState.STATE_CACTUS_LOWERING;
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_lower, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, reanimation.mDefinition.mFPS);
					return;
				}
			}
			else if (mState == PlantState.STATE_CACTUS_LOWERING)
			{
				if (reanimation.mLoopCount > 0)
				{
					mState = PlantState.STATE_CACTUS_LOW;
					PlayIdleAnim(0f);
					return;
				}
			}
			else
			{
				Zombie zombie = FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					mState = PlantState.STATE_CACTUS_RISING;
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_rise, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, reanimation.mDefinition.mFPS);
					mApp.PlayFoley(FoleyType.FOLEY_PLANTGROW);
				}
			}
		}

		public void StarFruitFire()
		{
			mApp.PlayFoley(FoleyType.FOLEY_THROW);
			for (int i = 0; i < 5; i++)
			{
				int theX = mX + 25;
				int theY = mY + 25;
				Projectile projectile = mBoard.AddProjectile(theX, theY, mRenderOrder + -1, mRow, ProjectileType.PROJECTILE_STAR);
				projectile.mDamageRangeFlags = GetDamageRangeFlags(PlantWeapon.WEAPON_PRIMARY);
				projectile.mMotionType = ProjectileMotion.MOTION_STAR;
				float velX = (float)Math.Cos((double)TodCommon.DegToRad(30f)) * 3.33f;
				float velY = (float)Math.Sin((double)TodCommon.DegToRad(30f)) * 3.33f;
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

		public void UpdateTanglekelp()
		{
			if (mState != PlantState.STATE_TANGLEKELP_GRABBING)
			{
				Zombie zombie = FindTargetZombie(mRow, PlantWeapon.WEAPON_PRIMARY);
				if (zombie != null)
				{
					mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
					mState = PlantState.STATE_TANGLEKELP_GRABBING;
					mStateCountdown = 99;
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
					int aRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, mRow, 0);
					Reanimation reanimation2 = mApp.AddReanimation((float)(mX - 23), (float)(mY + 7), aRenderOrder, ReanimationType.REANIM_SPLASH);
					reanimation2.OverrideScale(1.3f, 1.3f);
					mApp.AddTodParticle((float)(mX + 31), (float)(mY + 64), aRenderOrder, ParticleEffect.PARTICLE_PLANTING_POOL);
					mApp.PlayFoley(FoleyType.FOLEY_ZOMBIE_ENTERING_WATER);
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
			if (mSeedType == SeedType.SEED_WALLNUT || mSeedType == SeedType.SEED_TALLNUT || mSeedType == SeedType.SEED_EXPLODE_O_NUT || mSeedType == SeedType.SEED_GIANT_WALLNUT)
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
			else if (mSeedType == SeedType.SEED_THREEPEATER)
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
			else if (mSeedType == SeedType.SEED_SPLITPEA)
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
			else if (mSeedType == SeedType.SEED_TWINSUNFLOWER)
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
			else if (mSeedType == SeedType.SEED_PEASHOOTER || mSeedType == SeedType.SEED_SNOWPEA || mSeedType == SeedType.SEED_REPEATER || mSeedType == SeedType.SEED_LEFTPEATER || mSeedType == SeedType.SEED_GATLINGPEA)
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
			if (IsPartOfUpgradableTo(seedTypeInCursor) && mBoard.CanPlantAt(mPlantCol, mRow, seedTypeInCursor) == PlantingReason.PLANTING_OK)
			{
				SexyColor flashingColor = TodCommon.GetFlashingColor(mBoard.mMainCounter, 90);
				if (flashingColor != reanimation.mColorOverride)
				{
					flag = true;
					reanimation.mColorOverride = flashingColor;
				}
			}
			else if (seedTypeInCursor == SeedType.SEED_COBCANNON && mSeedType == SeedType.SEED_KERNELPULT && mBoard.CanPlantAt(mPlantCol - 1, mRow, seedTypeInCursor) == PlantingReason.PLANTING_OK)
			{
				SexyColor flashingColor2 = TodCommon.GetFlashingColor(mBoard.mMainCounter, 90);
				if (flashingColor2 != reanimation.mColorOverride)
				{
					flag = true;
					reanimation.mColorOverride = flashingColor2;
				}
			}
			else if (mSeedType == SeedType.SEED_EXPLODE_O_NUT)
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
				if (mImitaterType == SeedType.SEED_IMITATER)
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
				int theAlpha = TodCommon.TodAnimateCurve(50, 0, mBeghouledFlashCountdown % 50, 0, 128, TodCurves.CURVE_BOUNCE);
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
				if (mImitaterType == SeedType.SEED_IMITATER)
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
				int theAlpha2 = TodCommon.TodAnimateCurve(50, 0, mBeghouledFlashCountdown % 50, 0, 128, TodCurves.CURVE_BOUNCE);
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
			if (mSeedType != SeedType.SEED_SUNFLOWER && mSeedType != SeedType.SEED_TWINSUNFLOWER && mSeedType != SeedType.SEED_SUNSHROOM && reanimation.mEnableExtraAdditiveDraw)
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
			if (aUpdatedType == SeedType.SEED_GATLINGPEA && mSeedType == SeedType.SEED_REPEATER)
			{
				return true;
			}
			if (aUpdatedType == SeedType.SEED_WINTERMELON && mSeedType == SeedType.SEED_MELONPULT)
			{
				return true;
			}
			if (aUpdatedType == SeedType.SEED_TWINSUNFLOWER && mSeedType == SeedType.SEED_SUNFLOWER)
			{
				return true;
			}
			if (aUpdatedType == SeedType.SEED_SPIKEROCK && mSeedType == SeedType.SEED_SPIKEWEED)
			{
				return true;
			}
			if (aUpdatedType == SeedType.SEED_COBCANNON && mSeedType == SeedType.SEED_KERNELPULT)
			{
				if (mBoard.IsValidCobCannonSpot(mPlantCol, mRow))
				{
					return true;
				}
			}
			else
			{
				if (aUpdatedType == SeedType.SEED_GOLD_MAGNET && mSeedType == SeedType.SEED_MAGNETSHROOM)
				{
					return true;
				}
				if (aUpdatedType == SeedType.SEED_GLOOMSHROOM && mSeedType == SeedType.SEED_FUMESHROOM)
				{
					return true;
				}
				if (aUpdatedType == SeedType.SEED_CATTAIL && mSeedType == SeedType.SEED_LILYPAD)
				{
					Plant topPlantAt = mBoard.GetTopPlantAt(mPlantCol, mRow, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
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
			if (aUpdatedType == SeedType.SEED_COBCANNON && mSeedType == SeedType.SEED_KERNELPULT)
			{
				return mBoard.IsValidCobCannonSpot(mPlantCol, mRow) || mBoard.IsValidCobCannonSpot(mPlantCol - 1, mRow);
			}
			return IsUpgradableTo(aUpdatedType);
		}

		public void UpdateCobCannon()
		{
			if (mState == PlantState.STATE_COBCANNON_ARMING)
			{
				if (mStateCountdown <= 0)
				{
					mState = PlantState.STATE_COBCANNON_LOADING;
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_charge, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
					return;
				}
			}
			else if (mState == PlantState.STATE_COBCANNON_LOADING)
			{
				Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
				if (reanimation.ShouldTriggerTimedEvent(0.5f))
				{
					mApp.PlayFoley(FoleyType.FOLEY_SHOOP);
				}
				if (reanimation.mLoopCount > 0)
				{
					mState = PlantState.STATE_COBCANNON_READY;
					PlayIdleAnim(12f);
					return;
				}
			}
			else
			{
				if (mState == PlantState.STATE_COBCANNON_READY)
				{
					Reanimation reanimation2 = mApp.ReanimationGet(mBodyReanimID);
					ReanimatorTrackInstance trackInstanceByName = reanimation2.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_cobcannon_cob);
					trackInstanceByName.mTrackColor = TodCommon.GetFlashingColor(mBoard.mMainCounter, 75);
					return;
				}
				if (mState == PlantState.STATE_COBCANNON_FIRING)
				{
					Reanimation reanimation3 = mApp.ReanimationGet(mBodyReanimID);
					if (reanimation3.ShouldTriggerTimedEvent(0.48f))
					{
						mApp.PlayFoley(FoleyType.FOLEY_COBLAUNCH);
					}
				}
			}
		}

		public void CobCannonFire(int theTargetX, int theTargetY)
		{
			Debug.ASSERT(mState == PlantState.STATE_COBCANNON_READY);
			mState = PlantState.STATE_COBCANNON_FIRING;
			mShootingCounter = 184;
			PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_shooting, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
			mTargetX = theTargetX - 47;
			mTargetY = theTargetY;
			Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
			ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_cobcannon_cob);
			trackInstanceByName.mTrackColor = SexyColor.White;
		}

		public void UpdateGoldMagnetShroom()
		{
			Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
			bool flag = false;
			for (int i = 0; i < 5; i++)
			{
				MagnetItem magnetItem = mMagnetItems[i];
				if (magnetItem.mItemType != MagnetItemType.MAGNET_ITEM_NONE)
				{
					SexyVector2 sexyVector = new SexyVector2((float)mX + magnetItem.mDestOffsetX - magnetItem.mPosX, (float)mY + magnetItem.mDestOffsetY - magnetItem.mPosY);
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
						mApp.mPlayerInfo.AddCoins(coinValue);
						mBoard.mCoinsCollected += coinValue;
						mApp.PlayFoley(FoleyType.FOLEY_COIN);
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
			if (mState == PlantState.STATE_MAGNETSHROOM_CHARGING)
			{
				if (mStateCountdown <= 0)
				{
					mState = PlantState.STATE_READY;
				}
				return;
			}
			if (mState == PlantState.STATE_MAGNETSHROOM_SUCKING)
			{
				if (reanimation.ShouldTriggerTimedEvent(0.4f))
				{
					mApp.PlayFoley(FoleyType.FOLEY_MAGNETSHROOM);
					GoldMagnetFindTargets();
				}
				if (reanimation.mLoopCount > 0 && !flag)
				{
					PlayIdleAnim(14f);
					mState = PlantState.STATE_MAGNETSHROOM_CHARGING;
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
				mState = PlantState.STATE_MAGNETSHROOM_SUCKING;
				PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_attract, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 12f);
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

		public void UpdateCoffeeBean()
		{
			if (mState == PlantState.STATE_DOINGSPECIAL)
			{
				Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					Die();
				}
			}
		}

		public void UpdateUmbrella()
		{
			if (mState == PlantState.STATE_UMBRELLA_TRIGGERED)
			{
				if (mStateCountdown <= 0)
				{
					mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, mRow + 1, 0);
					mState = PlantState.STATE_UMBRELLA_REFLECTING;
					return;
				}
			}
			else if (mState == PlantState.STATE_UMBRELLA_REFLECTING)
			{
				Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
				if (reanimation.mLoopCount > 0)
				{
					PlayIdleAnim(0f);
					mState = PlantState.STATE_NOTREADY;
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

		public void AnimateGarlic()
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
				if ((coin2.mType == CoinType.COIN_SILVER || coin2.mType == CoinType.COIN_GOLD || coin2.mType == CoinType.COIN_DIAMOND) && coin2.mCoinMotion != CoinMotion.COIN_MOTION_FROM_PRESENT && !coin2.mIsBeingCollected && coin2.mCoinAge >= 50)
				{
					float num2 = TodCommon.Distance2D((float)mX + 40f, (float)mY + 40f, coin2.mPosX + (float)(coin2.mWidth / 2), coin2.mPosY + (float)(coin2.mHeight / 2));
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
			if (mState != PlantState.STATE_SPIKEWEED_ATTACKING)
			{
				PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_attack, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 20, 18f);
				mApp.PlaySample(Resources.SOUND_THROW);
				mState = PlantState.STATE_SPIKEWEED_ATTACKING;
				mStateCountdown = 99;
			}
		}

		public void ImitaterMorph()
		{
			Die();
			Plant plant = mBoard.AddPlant(mPlantCol, mRow, mImitaterType, SeedType.SEED_IMITATER);
			FilterEffectType aFilterEffect = FilterEffectType.FILTER_EFFECT_WASHED_OUT;
			if (mImitaterType == SeedType.SEED_HYPNOSHROOM || mImitaterType == SeedType.SEED_SQUASH || mImitaterType == SeedType.SEED_POTATOMINE || mImitaterType == SeedType.SEED_GARLIC || mImitaterType == SeedType.SEED_LILYPAD)
			{
				aFilterEffect = FilterEffectType.FILTER_EFFECT_LESS_WASHED_OUT;
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

		public void UpdateImitater()
		{
			if (mState != PlantState.STATE_IMITATER_MORPHING)
			{
				if (mStateCountdown <= 0)
				{
					mState = PlantState.STATE_IMITATER_MORPHING;
					PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_explode, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 26f);
					return;
				}
			}
			else
			{
				Reanimation reanimation = mApp.ReanimationGet(mBodyReanimID);
				if (reanimation.ShouldTriggerTimedEvent(0.8f))
				{
					mApp.AddTodParticle((float)(mX + 40), (float)(mY + 40), 400000, ParticleEffect.PARTICLE_IMITATER_MORPH);
				}
				if (reanimation.mLoopCount > 0)
				{
					ImitaterMorph();
				}
			}
		}

		public void UpdateReanim()
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
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BIG_TIME && (mSeedType == SeedType.SEED_WALLNUT || mSeedType == SeedType.SEED_SUNFLOWER || mSeedType == SeedType.SEED_MARIGOLD))
			{
				num3 = 1.5f;
				num4 = 1.5f;
				num += -20f;
				num2 += -40f;
			}
			if (mSeedType == SeedType.SEED_GIANT_WALLNUT)
			{
				num3 = 2f;
				num4 = 2f;
				num += -76f;
				num2 += -64f;
			}
			if (mSeedType == SeedType.SEED_INSTANT_COFFEE)
			{
				num3 = 0.8f;
				num4 = 0.8f;
				num += 12f;
				num2 += 10f;
			}
			if (mSeedType == SeedType.SEED_POTATOMINE)
			{
				num3 = 0.8f;
				num4 = 0.8f;
				num += 12f;
				num2 += 12f;
			}
			if (mState == PlantState.STATE_GRAVEBUSTER_EATING)
			{
				num2 += TodCommon.TodAnimateCurveFloat(400, 0, mStateCountdown, 0f, 30f, TodCurves.CURVE_LINEAR);
			}
			if (mWakeUpCounter > 0)
			{
				float num5 = TodCommon.TodAnimateCurveFloat(70, 0, mWakeUpCounter, 1f, 0.8f, TodCurves.CURVE_EASE_SIN_WAVE);
				num4 *= num5;
				num2 += 80f - num5 * 80f;
			}
			reanimation.Update();
			if (mSeedType == SeedType.SEED_LEFTPEATER)
			{
				num += 80f * num3;
				num3 *= -1f;
			}
			if (mPottedPlantIndex != -1)
			{
				PottedPlant pottedPlant = mApp.mPlayerInfo.mPottedPlant[mPottedPlantIndex];
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
				float num9 = TodCommon.TodAnimateCurveFloat(100, 0, mStateCountdown, num6, thePositionEnd, TodCurves.CURVE_LINEAR);
				float num10 = TodCommon.TodAnimateCurveFloat(100, 0, mStateCountdown, num7, thePositionEnd2, TodCurves.CURVE_LINEAR);
				float num11 = TodCommon.TodAnimateCurveFloat(100, 0, mStateCountdown, num8, thePositionEnd3, TodCurves.CURVE_LINEAR);
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
				mApp.PlayFoley(FoleyType.FOLEY_SQUISH);
				Die();
			}
		}

		public bool IsSpiky()
		{
			return mSeedType == SeedType.SEED_SPIKEWEED || mSeedType == SeedType.SEED_SPIKEROCK;
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
			return IsOnBoard() && mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && mApp.mGameMode != GameMode.GAMEMODE_TREE_OF_WISDOM;
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
			PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_idle, ReanimLoopType.REANIM_LOOP, 20, theRate);
			if (mApp.IsIZombieLevel())
			{
				reanimation.mAnimRate = 0f;
			}
		}

		public void UpdateFlowerPot()
		{
			if (mState == PlantState.STATE_FLOWERPOT_INVULNERABLE && mStateCountdown <= 0)
			{
				mState = PlantState.STATE_NOTREADY;
			}
		}

		public void UpdateLilypad()
		{
			if (mState == PlantState.STATE_LILYPAD_INVULNERABLE && mStateCountdown <= 0)
			{
				mState = PlantState.STATE_NOTREADY;
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
			int count = mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = mBoard.mPlants[i];
				if (!plant.mDead && !plant.NotOnGround() && plant.mSeedType == SeedType.SEED_GOLD_MAGNET && plant.mState == PlantState.STATE_MAGNETSHROOM_SUCKING)
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
			if (mSeedType == SeedType.SEED_GOLD_MAGNET)
			{
				for (int i = 0; i < 5; i++)
				{
					MagnetItem magnetItem = mMagnetItems[i];
					if (magnetItem.mItemType != MagnetItemType.MAGNET_ITEM_NONE)
					{
						return true;
					}
				}
				return false;
			}
			if (mSeedType == SeedType.SEED_MAGNETSHROOM)
			{
				for (int j = 0; j < 5; j++)
				{
					MagnetItem magnetItem2 = mMagnetItems[j];
					if (magnetItem2.mItemType != MagnetItemType.MAGNET_ITEM_NONE)
					{
						SexyVector2 sexyVector = new SexyVector2((float)mX + magnetItem2.mDestOffsetX - magnetItem2.mPosX, (float)mY + magnetItem2.mDestOffsetY - magnetItem2.mPosY);
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
			if (mSeedType == SeedType.SEED_TWINSUNFLOWER)
			{
				mBoard.CountPlantByType(mSeedType);
			}
			if (mSeedType == SeedType.SEED_COBCANNON)
			{
				mBoard.CountPlantByType(mSeedType);
			}
			if (mSeedType >= SeedType.SEED_PEASHOOTER && mSeedType < SeedType.SEED_EXPLODE_O_NUT)
			{
				mApp.mPlayerInfo.mPlantTypesUsed[(int)mSeedType] = true;
				int num = 0;
				while (num < mApp.mPlayerInfo.mPlantTypesUsed.Length && mApp.mPlayerInfo.mPlantTypesUsed[num])
				{
					num++;
				}
			}
			if (mBoard.StageHasFog() && (mSeedType == SeedType.SEED_PLANTERN || mSeedType == SeedType.SEED_BLOVER))
			{
				mBoard.mPlanternOrBloverUsed = true;
			}
			if (mBoard.StageIsNight() && (mSeedType == SeedType.SEED_WALLNUT || mSeedType == SeedType.SEED_TALLNUT))
			{
				mBoard.mNutsUsed = true;
			}
			if (mSeedType == SeedType.SEED_WINTERMELON)
			{
				uint num2 = 0U;
				int count = mBoard.mPlants.Count;
				for (int i = 0; i < count; i++)
				{
					Plant plant = mBoard.mPlants[i];
					if (!plant.mDead && plant.mSeedType == SeedType.SEED_WINTERMELON)
					{
						TodCommon.SetBit(ref num2, plant.mRow, 1);
					}
				}
				mBoard.StageHas6Rows();
			}
		}

		public static PlantDefinition GetPlantDefinition(SeedType theSeedtype)
		{
			Debug.ASSERT(theSeedtype >= SeedType.SEED_PEASHOOTER && theSeedtype < SeedType.NUM_SEED_TYPES);
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
			b.WriteFloat(mBodyReanimID.mAnimTime);
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
