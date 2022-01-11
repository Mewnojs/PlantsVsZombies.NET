using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class LawnMower
	{
		public static LawnMower GetNewLawnMower()
		{
			if (LawnMower.unusedObjects.Count > 0)
			{
				return LawnMower.unusedObjects.Pop();
			}
			return new LawnMower();
		}

		public void PrepareForReuse()
		{
			Reset();
			LawnMower.unusedObjects.Push(this);
		}

		private LawnMower()
		{
			Reset();
		}

		private void Reset()
		{
			mApp = null;
			mBoard = null;
			mPosX = 0f;
			mPosY = 0f;
			mRenderOrder = 0;
			mRow = 0;
			mAnimTicksPerFrame = 0;
			mReanimID = null;
			mChompCounter = 0;
			mRollingInCounter = 0;
			mSquishedCounter = 0;
			mMowerState = LawnMowerState.MOWER_ROLLING_IN;
			mDead = false;
			mVisible = false;
			mMowerType = LawnMowerType.LAWNMOWER_LAWN;
			mAltitude = 0f;
			mMowerHeight = MowerHeight.MOWER_HEIGHT_LAND;
			mLastPortalX = 0;
		}

		public void LawnMowerInitialize(int theRow)
		{
			mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			mBoard = mApp.mBoard;
			mRow = theRow;
			mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_LAWN_MOWER, theRow, 0);
			mPosX = -160f + Constants.BOARD_EXTRA_ROOM;
			mPosY = mBoard.GetPosYBasedOnRow(mPosX + 40f, theRow) + 23f;
			mDead = false;
			mMowerState = LawnMowerState.MOWER_READY;
			mVisible = true;
			mChompCounter = 0;
			mRollingInCounter = 0;
			mSquishedCounter = 0;
			mLastPortalX = -1;
			ReanimationType theReanimationType;
			if (mBoard.StageHasRoof())
			{
				mMowerType = LawnMowerType.LAWNMOWER_ROOF;
				theReanimationType = ReanimationType.REANIM_ROOF_CLEANER;
			}
			else if (mBoard.mPlantRow[mRow] == PlantRowType.PLANTROW_POOL && mApp.mPlayerInfo.mPurchases[22] != 0)
			{
				mMowerType = LawnMowerType.LAWNMOWER_POOL;
				theReanimationType = ReanimationType.REANIM_POOL_CLEANER;
			}
			else
			{
				mMowerType = LawnMowerType.LAWNMOWER_LAWN;
				theReanimationType = ReanimationType.REANIM_LAWNMOWER;
			}
			Reanimation reanimation = mApp.AddReanimation(0f, 18f, mRenderOrder, theReanimationType);
			reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
			reanimation.mAnimRate = 0f;
			reanimation.mIsAttachment = true;
			reanimation.OverrideScale(0.85f, 0.85f);
			mReanimID = mApp.ReanimationGetID(reanimation);
			if (mMowerType == LawnMowerType.LAWNMOWER_LAWN)
			{
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_normal);
			}
			else if (mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				reanimation.OverrideScale(0.8f, 0.8f);
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_land);
				string empty = string.Empty;
				reanimation.SetTruncateDisappearingFrames(empty, false);
			}
			if (mBoard.mSuperMowerMode)
			{
				EnableSuperMower(true);
			}
		}

		public void StartMower()
		{
			if (mMowerState == LawnMowerState.MOWER_TRIGGERED)
			{
				return;
			}
			Reanimation reanimation = mApp.ReanimationGet(mReanimID);
			if (mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				reanimation.mAnimRate = 35f;
				mApp.PlayFoley(FoleyType.FOLEY_POOL_CLEANER);
			}
			else
			{
				reanimation.mAnimRate = 70f;
				mApp.PlayFoley(FoleyType.FOLEY_LAWNMOWER);
			}
			mBoard.mWaveRowGotLawnMowered[mRow] = mBoard.mCurrentWave;
			mBoard.mTriggeredLawnMowers++;
			mMowerState = LawnMowerState.MOWER_TRIGGERED;
		}

		public void Update()
		{
			if (mMowerState == LawnMowerState.MOWER_SQUISHED)
			{
				//mSquishedCounter -= 3;
				mSquishedCounter--;
				if (mSquishedCounter <= 0)
				{
					Die();
				}
				return;
			}
			if (mMowerState == LawnMowerState.MOWER_ROLLING_IN)
			{
				//mRollingInCounter += 3;
				mRollingInCounter++;
				mPosX = TodCommon.TodAnimateCurveFloat(0, 100, mRollingInCounter, -160f, -21f, TodCurves.CURVE_EASE_IN_OUT) + Constants.BOARD_EXTRA_ROOM;
				if (mRollingInCounter >= 100)
				{
					mMowerState = LawnMowerState.MOWER_READY;
				}
				return;
			}
			if (mApp.mGameScene != GameScenes.SCENE_PLAYING && !mBoard.mCutScene.ShouldRunUpsellBoard())
			{
				return;
			}
			TRect lawnMowerAttackRect = GetLawnMowerAttackRect();
			int count = mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = mBoard.mZombies[i];
				if (!zombie.mDead && zombie.mZombieType != ZombieType.ZOMBIE_BOSS && zombie.mRow - mRow == 0 && zombie.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_MOWERED && !zombie.IsTangleKelpTarget())
				{
					int theDamageRangeFlags = 127;
					if (zombie.EffectedByDamage((uint)theDamageRangeFlags))
					{
						TRect zombieRect = zombie.GetZombieRect();
						int rectOverlap = GameConstants.GetRectOverlap(lawnMowerAttackRect, zombieRect);
						int num = 0;
						if (zombie.mZombieType == ZombieType.ZOMBIE_BALLOON)
						{
							num = 20;
						}
						if (rectOverlap > num && (mMowerState != LawnMowerState.MOWER_READY || (zombie.mZombieType != ZombieType.ZOMBIE_BUNGEE && zombie.mHasHead)))
						{
							MowZombie(zombie);
						}
					}
				}
			}
			if (mMowerState != LawnMowerState.MOWER_TRIGGERED && mMowerState != LawnMowerState.MOWER_SQUISHED)
			{
				return;
			}
			float num2 = 3.33f;
			if (mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				num2 = 2.5f;
			}
			if (mChompCounter > 0)
			{
				//mChompCounter -= 3;
				mChompCounter--;
				num2 = TodCommon.TodAnimateCurveFloat(50, 0, mChompCounter, num2, 1f, TodCurves.CURVE_BOUNCE_SLOW_MIDDLE);
			}
			mPosX += /*3f * */num2;
			mPosY = mBoard.GetPosYBasedOnRow(mPosX + 40f, mRow) + 23f;
			if (mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				UpdatePool();
			}
			if (mMowerType == LawnMowerType.LAWNMOWER_LAWN && mBoard.mPlantRow[mRow] == PlantRowType.PLANTROW_POOL && mPosX > 50f)
			{
				Reanimation reanimation = mApp.AddReanimation(mPosX, mPosY + 25f, mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
				reanimation.OverrideScale(1.2f, 0.8f);
				mApp.AddTodParticle(mPosX + 50f, mPosY + 67f, mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
				mApp.PlaySample(Resources.SOUND_ZOMBIE_ENTERING_WATER);
				mApp.mSoundSystem.StopFoley(FoleyType.FOLEY_LAWNMOWER);
				Die();
			}
			if (mPosX > Constants.WIDE_BOARD_WIDTH)
			{
				Die();
			}
			if (mReanimID != null)
			{
				Reanimation reanimation2 = mApp.ReanimationGet(mReanimID);
				if (reanimation2 != null)
				{
					reanimation2.Update();
				}
			}
		}

		public void Draw(Graphics g)
		{
			if (!mVisible)
			{
				return;
			}
			float num = 14f;
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			if (mMowerHeight != MowerHeight.MOWER_HEIGHT_UP_TO_LAND && mMowerHeight != MowerHeight.MOWER_HEIGHT_DOWN_TO_POOL && mMowerHeight != MowerHeight.MOWER_HEIGHT_IN_POOL && mMowerState != LawnMowerState.MOWER_SQUISHED)
			{
				int num2 = 0;
				float theScaleX = 1f;
				float theScaleY = 1f;
				if (mBoard.StageIsNight())
				{
					num2 = 1;
				}
				float num3 = mPosX - 7f;
				float num4 = mPosY - mAltitude + 47f;
				if (mMowerType == LawnMowerType.LAWNMOWER_POOL)
				{
					num3 += -17f;
					num4 += -8f;
				}
				if (mMowerType == LawnMowerType.LAWNMOWER_ROOF)
				{
					num3 += -9f;
					num4 += -36f;
					theScaleY = 1.2f;
					if (mMowerState == LawnMowerState.MOWER_TRIGGERED)
					{
						num4 += 36f;
					}
				}
				if (num2 == 0)
				{
					int num5 = 2;
					int num6 = -2;
					TodCommon.TodDrawImageCelCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW, num3 * Constants.S + num5 * Constants.IS, num4 * Constants.S + num6 * Constants.IS, 0, theScaleX, theScaleY);
				}
				else
				{
					int num7 = 2;
					int num8 = -2;
					TodCommon.TodDrawImageCelCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW2, num3 * Constants.S + num7 * Constants.IS, num4 * Constants.S + num8 * Constants.IS, 0, theScaleX, theScaleY);
				}
			}
			Graphics @new = Graphics.GetNew(g);
			@new.mTransX += (int)((mPosX + 6f) * Constants.S);
			@new.mTransY += (int)((mPosY - mAltitude - num) * Constants.S);
			if (mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				if (mMowerState == LawnMowerState.MOWER_TRIGGERED)
				{
					@new.mTransY -= (int)(7f * Constants.S);
					@new.mTransX -= (int)(10f * Constants.S);
				}
				else
				{
					@new.mTransY -= (int)(33f * Constants.S);
				}
				if (mMowerHeight == MowerHeight.MOWER_HEIGHT_UP_TO_LAND || mMowerHeight == MowerHeight.MOWER_HEIGHT_DOWN_TO_POOL)
				{
					@new.SetClipRect((int)(-50f * Constants.S), (int)(-50f * Constants.S), (int)(150f * Constants.S), (int)((132f + mAltitude) * Constants.S));
				}
			}
			else if (mMowerType == LawnMowerType.LAWNMOWER_ROOF)
			{
				if (mMowerState == LawnMowerState.MOWER_TRIGGERED)
				{
					@new.mTransY -= (int)(4f * Constants.S);
					@new.mTransX -= (int)(10f * Constants.S);
				}
				else
				{
					@new.mTransY -= (int)(40f * Constants.S);
				}
			}
			if (mMowerState == LawnMowerState.MOWER_TRIGGERED || mMowerState == LawnMowerState.MOWER_SQUISHED)
			{
				Reanimation reanimation = mApp.ReanimationGet(mReanimID);
				reanimation.Draw(@new);
			}
			else
			{
				LawnMowerType lawnMowerType = mMowerType;
				if (mMowerType == LawnMowerType.LAWNMOWER_LAWN && mBoard.mSuperMowerMode)
				{
					lawnMowerType = LawnMowerType.LAWNMOWER_SUPER_MOWER;
				}
				GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedMower(@new, 0f * Constants.S, 19f * Constants.S, lawnMowerType);
			}
			@new.PrepareForReuse();
		}

		public void Die()
		{
			mDead = true;
			mApp.RemoveReanimation(ref mReanimID);
			if (mBoard.mBonusLawnMowersRemaining > 0 && !mBoard.HasLevelAwardDropped())
			{
				LawnMower lawnMower = new LawnMower();
				lawnMower.LawnMowerInitialize(mRow);
				lawnMower.mMowerState = LawnMowerState.MOWER_ROLLING_IN;
				mBoard.mBonusLawnMowersRemaining--;
				mBoard.mLawnMowers.Add(lawnMower);
			}
		}

		public TRect GetLawnMowerAttackRect()
		{
			return new TRect((int)mPosX, (int)mPosY, 50, 80);
		}

		public void UpdatePool()//3update
		{
			bool flag = false;
			if (mPosX > 150f && mPosX < 770f)
			{
				flag = true;
			}
			Reanimation reanimation = mApp.ReanimationGet(mReanimID);
			if (flag && mMowerHeight == MowerHeight.MOWER_HEIGHT_LAND)
			{
				Reanimation reanimation2 = mApp.AddReanimation(mPosX + 0f, mPosY + 25f, mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
				reanimation2.OverrideScale(1.2f, 0.8f);
				mApp.AddTodParticle(mPosX + 0f + 50f, mPosY + 0f + 42f, mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
				mApp.PlayFoley(FoleyType.FOLEY_ZOMBIESPLASH);
				mMowerHeight = MowerHeight.MOWER_HEIGHT_DOWN_TO_POOL;
			}
			else if (mMowerHeight == MowerHeight.MOWER_HEIGHT_DOWN_TO_POOL)
			{
				mAltitude -= 2f;
				if (mAltitude <= -28f)
				{
					mAltitude = 0f;
					mMowerHeight = MowerHeight.MOWER_HEIGHT_IN_POOL;
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_water, ReanimLoopType.REANIM_LOOP, 0, 0f);
				}
			}
			else if (mMowerHeight == MowerHeight.MOWER_HEIGHT_IN_POOL)
			{
				if (!flag)
				{
					mAltitude = -28f;
					mMowerHeight = MowerHeight.MOWER_HEIGHT_UP_TO_LAND;
					Reanimation reanimation3 = mApp.AddReanimation(mPosX + 0f, mPosY + 25f, mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
					reanimation3.OverrideScale(1.2f, 0.8f);
					mApp.AddTodParticle(mPosX + 0f + 50f, mPosY + 0f + 42f, mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
					mApp.PlayFoley(FoleyType.FOLEY_PLANT_WATER);
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_land, ReanimLoopType.REANIM_LOOP, 0, 0f);
				}
			}
			else if (mMowerHeight == MowerHeight.MOWER_HEIGHT_UP_TO_LAND)
			{
				mAltitude += 2f;
				if (mAltitude >= 0f)
				{
					mAltitude = 0f;
					mMowerHeight = MowerHeight.MOWER_HEIGHT_LAND;
				}
			}
			if (mMowerHeight == MowerHeight.MOWER_HEIGHT_IN_POOL && reanimation.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD && reanimation.mLoopCount > 0)
			{
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_water, ReanimLoopType.REANIM_LOOP, 10, 35f);
			}
		}

		public bool SaveToFile(Sexy.Buffer b)
		{
			b.WriteLong(mRow);
			b.WriteFloat(mAltitude);
			b.WriteLong(mAnimTicksPerFrame);
			b.WriteLong(mChompCounter);
			b.WriteBoolean(mDead);
			b.WriteLong(mLastPortalX);
			b.WriteLong((int)mMowerHeight);
			b.WriteLong((int)mMowerState);
			b.WriteLong((int)mMowerType);
			b.WriteFloat(mPosX);
			b.WriteFloat(mPosY);
			b.WriteLong(mRenderOrder);
			b.WriteLong(mRollingInCounter);
			b.WriteLong(mSquishedCounter);
			b.WriteBoolean(mVisible);
			return true;
		}

		public bool LoadFromFile(Sexy.Buffer b)
		{
			int theRow = b.ReadLong();
			LawnMowerInitialize(theRow);
			mRow = theRow;
			mAltitude = b.ReadFloat();
			mAnimTicksPerFrame = b.ReadLong();
			mChompCounter = b.ReadLong();
			mDead = b.ReadBoolean();
			mLastPortalX = b.ReadLong();
			mMowerHeight = (MowerHeight)b.ReadLong();
			mMowerState = (LawnMowerState)b.ReadLong();
			mMowerType = (LawnMowerType)b.ReadLong();
			mPosX = b.ReadFloat();
			mPosY = b.ReadFloat();
			mRenderOrder = b.ReadLong();
			mRollingInCounter = b.ReadLong();
			mSquishedCounter = b.ReadLong();
			mVisible = b.ReadBoolean();
			return true;
		}

		public void LoadingComplete()
		{
			mApp = GlobalStaticVars.gLawnApp;
			mBoard = mApp.mBoard;
		}

		public void MowZombie(Zombie theZombie)
		{
			if (mMowerState == LawnMowerState.MOWER_READY)
			{
				StartMower();
				mChompCounter = 25;
			}
			else if (mMowerState == LawnMowerState.MOWER_TRIGGERED)
			{
				mChompCounter = 50;
			}
			if (mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				mApp.PlayFoley(FoleyType.FOLEY_SHOOP);
				if (mMowerHeight == MowerHeight.MOWER_HEIGHT_IN_POOL)
				{
					Reanimation reanimation = mApp.ReanimationGet(mReanimID);
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_suck, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 35f);
				}
				else
				{
					Reanimation reanimation2 = mApp.ReanimationGet(mReanimID);
					reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_landsuck, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 35f);
				}
				theZombie.DieWithLoot();
				return;
			}
			mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
			theZombie.MowDown();
		}

		public void SquishMower()
		{
			Reanimation reanimation = mApp.ReanimationGet(mReanimID);
			reanimation.OverrideScale(0.85f, 0.22f);
			reanimation.SetPosition(-11f * Constants.S, 65f * Constants.S);
			mMowerState = LawnMowerState.MOWER_SQUISHED;
			mSquishedCounter = 500;
			mApp.PlayFoley(FoleyType.FOLEY_SQUISH);
		}

		public void EnableSuperMower(bool theEnable)
		{
			if (mMowerType != LawnMowerType.LAWNMOWER_LAWN)
			{
				return;
			}
			Reanimation reanimation = mApp.ReanimationGet(mReanimID);
			reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_tricked);
		}

		public LawnApp mApp;

		public Board mBoard;

		public float mPosX;

		public float mPosY;

		public int mRenderOrder;

		public int mRow;

		public int mAnimTicksPerFrame;

		public Reanimation mReanimID;

		public int mChompCounter;

		public int mRollingInCounter;

		public int mSquishedCounter;

		public LawnMowerState mMowerState;

		public bool mDead;

		public bool mVisible;

		public LawnMowerType mMowerType;

		public float mAltitude;

		public MowerHeight mMowerHeight;

		public int mLastPortalX;

		private static Stack<LawnMower> unusedObjects = new Stack<LawnMower>();
	}
}
