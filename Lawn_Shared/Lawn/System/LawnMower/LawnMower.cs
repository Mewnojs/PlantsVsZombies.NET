using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class LawnMower
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
			this.Reset();
			LawnMower.unusedObjects.Push(this);
		}

		private LawnMower()
		{
			this.Reset();
		}

		private void Reset()
		{
			this.mApp = null;
			this.mBoard = null;
			this.mPosX = 0f;
			this.mPosY = 0f;
			this.mRenderOrder = 0;
			this.mRow = 0;
			this.mAnimTicksPerFrame = 0;
			this.mReanimID = null;
			this.mChompCounter = 0;
			this.mRollingInCounter = 0;
			this.mSquishedCounter = 0;
			this.mMowerState = LawnMowerState.MOWER_ROLLING_IN;
			this.mDead = false;
			this.mVisible = false;
			this.mMowerType = LawnMowerType.LAWNMOWER_LAWN;
			this.mAltitude = 0f;
			this.mMowerHeight = MowerHeight.MOWER_HEIGHT_LAND;
			this.mLastPortalX = 0;
		}

		public void LawnMowerInitialize(int theRow)
		{
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mBoard = this.mApp.mBoard;
			this.mRow = theRow;
			this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_LAWN_MOWER, theRow, 0);
			this.mPosX = -160f + (float)Constants.BOARD_EXTRA_ROOM;
			this.mPosY = this.mBoard.GetPosYBasedOnRow(this.mPosX + 40f, theRow) + 23f;
			this.mDead = false;
			this.mMowerState = LawnMowerState.MOWER_READY;
			this.mVisible = true;
			this.mChompCounter = 0;
			this.mRollingInCounter = 0;
			this.mSquishedCounter = 0;
			this.mLastPortalX = -1;
			ReanimationType theReanimationType;
			if (this.mBoard.StageHasRoof())
			{
				this.mMowerType = LawnMowerType.LAWNMOWER_ROOF;
				theReanimationType = ReanimationType.REANIM_ROOF_CLEANER;
			}
			else if (this.mBoard.mPlantRow[this.mRow] == PlantRowType.PLANTROW_POOL && this.mApp.mPlayerInfo.mPurchases[22] != 0)
			{
				this.mMowerType = LawnMowerType.LAWNMOWER_POOL;
				theReanimationType = ReanimationType.REANIM_POOL_CLEANER;
			}
			else
			{
				this.mMowerType = LawnMowerType.LAWNMOWER_LAWN;
				theReanimationType = ReanimationType.REANIM_LAWNMOWER;
			}
			Reanimation reanimation = this.mApp.AddReanimation(0f, 18f, this.mRenderOrder, theReanimationType);
			reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
			reanimation.mAnimRate = 0f;
			reanimation.mIsAttachment = true;
			reanimation.OverrideScale(0.85f, 0.85f);
			this.mReanimID = this.mApp.ReanimationGetID(reanimation);
			if (this.mMowerType == LawnMowerType.LAWNMOWER_LAWN)
			{
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_normal);
			}
			else if (this.mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				reanimation.OverrideScale(0.8f, 0.8f);
				reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_land);
				string empty = string.Empty;
				reanimation.SetTruncateDisappearingFrames(empty, false);
			}
			if (this.mBoard.mSuperMowerMode)
			{
				this.EnableSuperMower(true);
			}
		}

		public void StartMower()
		{
			if (this.mMowerState == LawnMowerState.MOWER_TRIGGERED)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimID);
			if (this.mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				reanimation.mAnimRate = 35f;
				this.mApp.PlayFoley(FoleyType.FOLEY_POOL_CLEANER);
			}
			else
			{
				reanimation.mAnimRate = 70f;
				this.mApp.PlayFoley(FoleyType.FOLEY_LAWNMOWER);
			}
			this.mBoard.mWaveRowGotLawnMowered[this.mRow] = this.mBoard.mCurrentWave;
			this.mBoard.mTriggeredLawnMowers++;
			this.mMowerState = LawnMowerState.MOWER_TRIGGERED;
		}

		public void Update()
		{
			if (this.mMowerState == LawnMowerState.MOWER_SQUISHED)
			{
				this.mSquishedCounter -= 3;
				if (this.mSquishedCounter <= 0)
				{
					this.Die();
				}
				return;
			}
			if (this.mMowerState == LawnMowerState.MOWER_ROLLING_IN)
			{
				this.mRollingInCounter += 3;
				this.mPosX = TodCommon.TodAnimateCurveFloat(0, 100, this.mRollingInCounter, -160f, -21f, TodCurves.CURVE_EASE_IN_OUT) + (float)Constants.BOARD_EXTRA_ROOM;
				if (this.mRollingInCounter >= 100)
				{
					this.mMowerState = LawnMowerState.MOWER_READY;
				}
				return;
			}
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING && !this.mBoard.mCutScene.ShouldRunUpsellBoard())
			{
				return;
			}
			TRect lawnMowerAttackRect = this.GetLawnMowerAttackRect();
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && zombie.mZombieType != ZombieType.ZOMBIE_BOSS && zombie.mRow - this.mRow == 0 && zombie.mZombiePhase != ZombiePhase.PHASE_ZOMBIE_MOWERED && !zombie.IsTangleKelpTarget())
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
						if (rectOverlap > num && (this.mMowerState != LawnMowerState.MOWER_READY || (zombie.mZombieType != ZombieType.ZOMBIE_BUNGEE && zombie.mHasHead)))
						{
							this.MowZombie(zombie);
						}
					}
				}
			}
			if (this.mMowerState != LawnMowerState.MOWER_TRIGGERED && this.mMowerState != LawnMowerState.MOWER_SQUISHED)
			{
				return;
			}
			float num2 = 3.33f;
			if (this.mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				num2 = 2.5f;
			}
			if (this.mChompCounter > 0)
			{
				this.mChompCounter -= 3;
				num2 = TodCommon.TodAnimateCurveFloat(50, 0, this.mChompCounter, num2, 1f, TodCurves.CURVE_BOUNCE_SLOW_MIDDLE);
			}
			this.mPosX += 3f * num2;
			this.mPosY = this.mBoard.GetPosYBasedOnRow(this.mPosX + 40f, this.mRow) + 23f;
			if (this.mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				this.UpdatePool();
			}
			if (this.mMowerType == LawnMowerType.LAWNMOWER_LAWN && this.mBoard.mPlantRow[this.mRow] == PlantRowType.PLANTROW_POOL && this.mPosX > 50f)
			{
				Reanimation reanimation = this.mApp.AddReanimation(this.mPosX, this.mPosY + 25f, this.mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
				reanimation.OverrideScale(1.2f, 0.8f);
				this.mApp.AddTodParticle(this.mPosX + 50f, this.mPosY + 67f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
				this.mApp.PlaySample(Resources.SOUND_ZOMBIE_ENTERING_WATER);
				this.mApp.mSoundSystem.StopFoley(FoleyType.FOLEY_LAWNMOWER);
				this.Die();
			}
			if (this.mPosX > (float)Constants.WIDE_BOARD_WIDTH)
			{
				this.Die();
			}
			if (this.mReanimID != null)
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mReanimID);
				if (reanimation2 != null)
				{
					reanimation2.Update();
				}
			}
		}

		public void Draw(Graphics g)
		{
			if (!this.mVisible)
			{
				return;
			}
			float num = 14f;
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			if (this.mMowerHeight != MowerHeight.MOWER_HEIGHT_UP_TO_LAND && this.mMowerHeight != MowerHeight.MOWER_HEIGHT_DOWN_TO_POOL && this.mMowerHeight != MowerHeight.MOWER_HEIGHT_IN_POOL && this.mMowerState != LawnMowerState.MOWER_SQUISHED)
			{
				int num2 = 0;
				float theScaleX = 1f;
				float theScaleY = 1f;
				if (this.mBoard.StageIsNight())
				{
					num2 = 1;
				}
				float num3 = this.mPosX - 7f;
				float num4 = this.mPosY - this.mAltitude + 47f;
				if (this.mMowerType == LawnMowerType.LAWNMOWER_POOL)
				{
					num3 += -17f;
					num4 += -8f;
				}
				if (this.mMowerType == LawnMowerType.LAWNMOWER_ROOF)
				{
					num3 += -9f;
					num4 += -36f;
					theScaleY = 1.2f;
					if (this.mMowerState == LawnMowerState.MOWER_TRIGGERED)
					{
						num4 += 36f;
					}
				}
				if (num2 == 0)
				{
					int num5 = 2;
					int num6 = -2;
					TodCommon.TodDrawImageCelCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW, num3 * Constants.S + (float)num5 * Constants.IS, num4 * Constants.S + (float)num6 * Constants.IS, 0, theScaleX, theScaleY);
				}
				else
				{
					int num7 = 2;
					int num8 = -2;
					TodCommon.TodDrawImageCelCenterScaledF(g, AtlasResources.IMAGE_PLANTSHADOW2, num3 * Constants.S + (float)num7 * Constants.IS, num4 * Constants.S + (float)num8 * Constants.IS, 0, theScaleX, theScaleY);
				}
			}
			Graphics @new = Graphics.GetNew(g);
			@new.mTransX += (int)((this.mPosX + 6f) * Constants.S);
			@new.mTransY += (int)((this.mPosY - this.mAltitude - num) * Constants.S);
			if (this.mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				if (this.mMowerState == LawnMowerState.MOWER_TRIGGERED)
				{
					@new.mTransY -= (int)(7f * Constants.S);
					@new.mTransX -= (int)(10f * Constants.S);
				}
				else
				{
					@new.mTransY -= (int)(33f * Constants.S);
				}
				if (this.mMowerHeight == MowerHeight.MOWER_HEIGHT_UP_TO_LAND || this.mMowerHeight == MowerHeight.MOWER_HEIGHT_DOWN_TO_POOL)
				{
					@new.SetClipRect((int)(-50f * Constants.S), (int)(-50f * Constants.S), (int)(150f * Constants.S), (int)((132f + this.mAltitude) * Constants.S));
				}
			}
			else if (this.mMowerType == LawnMowerType.LAWNMOWER_ROOF)
			{
				if (this.mMowerState == LawnMowerState.MOWER_TRIGGERED)
				{
					@new.mTransY -= (int)(4f * Constants.S);
					@new.mTransX -= (int)(10f * Constants.S);
				}
				else
				{
					@new.mTransY -= (int)(40f * Constants.S);
				}
			}
			if (this.mMowerState == LawnMowerState.MOWER_TRIGGERED || this.mMowerState == LawnMowerState.MOWER_SQUISHED)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimID);
				reanimation.Draw(@new);
			}
			else
			{
				LawnMowerType lawnMowerType = this.mMowerType;
				if (this.mMowerType == LawnMowerType.LAWNMOWER_LAWN && this.mBoard.mSuperMowerMode)
				{
					lawnMowerType = LawnMowerType.LAWNMOWER_SUPER_MOWER;
				}
				@new.DrawImage(AtlasResources.GetImageInAtlasById((int)(10285 + lawnMowerType)), -20f * Constants.S, (float)((int)(19f * Constants.S)));
			}
			@new.PrepareForReuse();
		}

		public void Die()
		{
			this.mDead = true;
			this.mApp.RemoveReanimation(ref this.mReanimID);
			if (this.mBoard.mBonusLawnMowersRemaining > 0 && !this.mBoard.HasLevelAwardDropped())
			{
				LawnMower lawnMower = new LawnMower();
				lawnMower.LawnMowerInitialize(this.mRow);
				lawnMower.mMowerState = LawnMowerState.MOWER_ROLLING_IN;
				this.mBoard.mBonusLawnMowersRemaining--;
				this.mBoard.mLawnMowers.Add(lawnMower);
			}
		}

		public TRect GetLawnMowerAttackRect()
		{
			return new TRect((int)this.mPosX, (int)this.mPosY, 50, 80);
		}

		public void UpdatePool()
		{
			bool flag = false;
			if (this.mPosX > 150f && this.mPosX < 770f)
			{
				flag = true;
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimID);
			if (flag && this.mMowerHeight == MowerHeight.MOWER_HEIGHT_LAND)
			{
				Reanimation reanimation2 = this.mApp.AddReanimation(this.mPosX + 0f, this.mPosY + 25f, this.mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
				reanimation2.OverrideScale(1.2f, 0.8f);
				this.mApp.AddTodParticle(this.mPosX + 0f + 50f, this.mPosY + 0f + 42f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
				this.mApp.PlayFoley(FoleyType.FOLEY_ZOMBIESPLASH);
				this.mMowerHeight = MowerHeight.MOWER_HEIGHT_DOWN_TO_POOL;
			}
			else if (this.mMowerHeight == MowerHeight.MOWER_HEIGHT_DOWN_TO_POOL)
			{
				this.mAltitude -= 2f;
				if (this.mAltitude <= -28f)
				{
					this.mAltitude = 0f;
					this.mMowerHeight = MowerHeight.MOWER_HEIGHT_IN_POOL;
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_water, ReanimLoopType.REANIM_LOOP, 0, 0f);
				}
			}
			else if (this.mMowerHeight == MowerHeight.MOWER_HEIGHT_IN_POOL)
			{
				if (!flag)
				{
					this.mAltitude = -28f;
					this.mMowerHeight = MowerHeight.MOWER_HEIGHT_UP_TO_LAND;
					Reanimation reanimation3 = this.mApp.AddReanimation(this.mPosX + 0f, this.mPosY + 25f, this.mRenderOrder + 1, ReanimationType.REANIM_SPLASH);
					reanimation3.OverrideScale(1.2f, 0.8f);
					this.mApp.AddTodParticle(this.mPosX + 0f + 50f, this.mPosY + 0f + 42f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_PLANTING_POOL);
					this.mApp.PlayFoley(FoleyType.FOLEY_PLANT_WATER);
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_land, ReanimLoopType.REANIM_LOOP, 0, 0f);
				}
			}
			else if (this.mMowerHeight == MowerHeight.MOWER_HEIGHT_UP_TO_LAND)
			{
				this.mAltitude += 2f;
				if (this.mAltitude >= 0f)
				{
					this.mAltitude = 0f;
					this.mMowerHeight = MowerHeight.MOWER_HEIGHT_LAND;
				}
			}
			if (this.mMowerHeight == MowerHeight.MOWER_HEIGHT_IN_POOL && reanimation.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD && reanimation.mLoopCount > 0)
			{
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_water, ReanimLoopType.REANIM_LOOP, 10, 35f);
			}
		}

		public bool SaveToFile(Buffer b)
		{
			b.WriteLong(this.mRow);
			b.WriteFloat(this.mAltitude);
			b.WriteLong(this.mAnimTicksPerFrame);
			b.WriteLong(this.mChompCounter);
			b.WriteBoolean(this.mDead);
			b.WriteLong(this.mLastPortalX);
			b.WriteLong((int)this.mMowerHeight);
			b.WriteLong((int)this.mMowerState);
			b.WriteLong((int)this.mMowerType);
			b.WriteFloat(this.mPosX);
			b.WriteFloat(this.mPosY);
			b.WriteLong(this.mRenderOrder);
			b.WriteLong(this.mRollingInCounter);
			b.WriteLong(this.mSquishedCounter);
			b.WriteBoolean(this.mVisible);
			return true;
		}

		public bool LoadFromFile(Buffer b)
		{
			int theRow = b.ReadLong();
			this.LawnMowerInitialize(theRow);
			this.mRow = theRow;
			this.mAltitude = b.ReadFloat();
			this.mAnimTicksPerFrame = b.ReadLong();
			this.mChompCounter = b.ReadLong();
			this.mDead = b.ReadBoolean();
			this.mLastPortalX = b.ReadLong();
			this.mMowerHeight = (MowerHeight)b.ReadLong();
			this.mMowerState = (LawnMowerState)b.ReadLong();
			this.mMowerType = (LawnMowerType)b.ReadLong();
			this.mPosX = b.ReadFloat();
			this.mPosY = b.ReadFloat();
			this.mRenderOrder = b.ReadLong();
			this.mRollingInCounter = b.ReadLong();
			this.mSquishedCounter = b.ReadLong();
			this.mVisible = b.ReadBoolean();
			return true;
		}

		public void LoadingComplete()
		{
			this.mApp = GlobalStaticVars.gLawnApp;
			this.mBoard = this.mApp.mBoard;
		}

		public void MowZombie(Zombie theZombie)
		{
			if (this.mMowerState == LawnMowerState.MOWER_READY)
			{
				this.StartMower();
				this.mChompCounter = 25;
			}
			else if (this.mMowerState == LawnMowerState.MOWER_TRIGGERED)
			{
				this.mChompCounter = 50;
			}
			if (this.mMowerType == LawnMowerType.LAWNMOWER_POOL)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_SHOOP);
				if (this.mMowerHeight == MowerHeight.MOWER_HEIGHT_IN_POOL)
				{
					Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimID);
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_suck, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 35f);
				}
				else
				{
					Reanimation reanimation2 = this.mApp.ReanimationGet(this.mReanimID);
					reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_landsuck, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 10, 35f);
				}
				theZombie.DieWithLoot();
				return;
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
			theZombie.MowDown();
		}

		public void SquishMower()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimID);
			reanimation.OverrideScale(0.85f, 0.22f);
			reanimation.SetPosition(-11f * Constants.S, 65f * Constants.S);
			this.mMowerState = LawnMowerState.MOWER_SQUISHED;
			this.mSquishedCounter = 500;
			this.mApp.PlayFoley(FoleyType.FOLEY_SQUISH);
		}

		public void EnableSuperMower(bool theEnable)
		{
			if (this.mMowerType != LawnMowerType.LAWNMOWER_LAWN)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimID);
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
