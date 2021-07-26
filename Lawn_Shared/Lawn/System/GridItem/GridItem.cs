using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class GridItem
	{
		public static GridItem GetNewGridItem()
		{
			if (GridItem.unusedObjects.Count > 0)
			{
				GridItem gridItem = GridItem.unusedObjects.Pop();
				gridItem.Reset();
				return gridItem;
			}
			return new GridItem();
		}

		public void PrepareForReuse()
		{
			GridItem.unusedObjects.Push(this);
		}

		private GridItem()
		{
			this.Reset();
		}

		private void Reset()
		{
			this.mGridItemState = GridItemState.GRIDITEM_STATE_NORMAL;
			for (int i = 0; i < this.mMotionTrailFrames.Length; i++)
			{
				this.mMotionTrailFrames[i] = new MotionTrailFrame();
			}
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mBoard = this.mApp.mBoard;
			this.mGridItemType = GridItemType.GRIDITEM_NONE;
			this.mGridX = 0;
			this.mGridY = 0;
			this.mGridItemCounter = 0;
			this.mRenderOrder = 0;
			this.mDead = false;
			this.mPosX = 0f;
			this.mPosY = 0f;
			this.mGoalX = 0f;
			this.mGoalY = 0f;
			this.mGridItemReanimID = null;
			this.mGridItemParticleID = null;
			this.mZombieType = ZombieType.ZOMBIE_INVALID;
			this.mSeedType = SeedType.SEED_NONE;
			this.mScaryPotType = ScaryPotType.SCARYPOT_NONE;
			this.mHighlighted = false;
			this.mTransparentCounter = 0;
			this.mSunCount = 0;
			this.mMotionTrailCount = 0;
		}

		public bool SaveToFile(Sexy.Buffer b)
		{
			try
			{
				b.WriteBoolean(this.mDead);
				b.WriteFloat(this.mGoalX);
				b.WriteFloat(this.mGoalY);
				b.WriteLong(this.mGridItemCounter);
				b.WriteLong((int)this.mGridItemState);
				b.WriteLong((int)this.mGridItemType);
				b.WriteLong(this.mGridX);
				b.WriteLong(this.mGridY);
				b.WriteBoolean(this.mHighlighted);
				b.WriteLong(this.mMotionTrailCount);
				for (int i = 0; i < this.mMotionTrailCount; i++)
				{
					this.mMotionTrailFrames[i].SaveToFile(b);
				}
				b.WriteFloat(this.mPosX);
				b.WriteFloat(this.mPosY);
				b.WriteLong(this.mRenderOrder);
				b.WriteLong((int)this.mScaryPotType);
				b.WriteLong((int)this.mSeedType);
				b.WriteLong(this.mSunCount);
				b.WriteLong(this.mTransparentCounter);
				b.WriteLong((int)this.mZombieType);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return false;
			}
			return true;
		}

		public bool LoadFromFile(Sexy.Buffer b)
		{
			try
			{
				this.mDead = b.ReadBoolean();
				this.mGoalX = b.ReadFloat();
				this.mGoalY = b.ReadFloat();
				this.mGridItemCounter = b.ReadLong();
				this.mGridItemState = (GridItemState)b.ReadLong();
				this.mGridItemType = (GridItemType)b.ReadLong();
				this.mGridX = b.ReadLong();
				this.mGridY = b.ReadLong();
				this.mHighlighted = b.ReadBoolean();
				this.mMotionTrailCount = b.ReadLong();
				Array.Clear(this.mMotionTrailFrames, 0, this.mMotionTrailFrames.Length);
				for (int i = 0; i < this.mMotionTrailCount; i++)
				{
					this.mMotionTrailFrames[i] = new MotionTrailFrame();
					this.mMotionTrailFrames[i].LoadFromFile(b);
				}
				this.mPosX = b.ReadFloat();
				this.mPosY = b.ReadFloat();
				this.mRenderOrder = b.ReadLong();
				this.mScaryPotType = (ScaryPotType)b.ReadLong();
				this.mSeedType = (SeedType)b.ReadLong();
				this.mSunCount = b.ReadLong();
				this.mTransparentCounter = b.ReadLong();
				this.mZombieType = (ZombieType)b.ReadLong();
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return false;
			}
			return true;
		}

		public void LoadingComplete()
		{
			this.mApp = GlobalStaticVars.gLawnApp;
			this.mBoard = this.mApp.mBoard;
			if (this.mGridItemType == GridItemType.GRIDITEM_RAKE)
			{
				this.mGridItemReanimID = this.mBoard.CreateRakeReanim(this.mPosX, this.mPosY, this.mRenderOrder);
			}
			if (this.mGridItemType == GridItemType.GRIDITEM_PORTAL_CIRCLE || this.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE)
			{
				this.OpenPortal();
			}
		}

		public void DrawLadder(Graphics g)
		{
			int num = this.mBoard.GridToPixelX(this.mGridX, this.mGridY);
			int num2 = this.mBoard.GridToPixelY(this.mGridX, this.mGridY);
			TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_5, ((float)num + 25f) * Constants.S, ((float)num2 - 4f) * Constants.S, 0.8f, 0.8f);
		}

		public void DrawCrater(Graphics g)
		{
			float num = (float)this.mBoard.GridToPixelX(this.mGridX, this.mGridY) - 8f;
			float num2 = (float)this.mBoard.GridToPixelY(this.mGridX, this.mGridY) + 40f;
			if (this.mGridItemCounter < 25)
			{
				int theAlpha = TodCommon.TodAnimateCurve(25, 0, this.mGridItemCounter, 255, 0, TodCurves.CURVE_LINEAR);
				g.SetColor(new SexyColor(255, 255, 255, theAlpha));
				g.SetColorizeImages(true);
			}
			bool flag = this.mGridItemCounter < 9000;
			Image theImageStrip = AtlasResources.IMAGE_CRATER;
			int theCelCol = 0;
			if (this.mBoard.IsPoolSquare(this.mGridX, this.mGridY))
			{
				if (this.mBoard.StageIsNight())
				{
					theImageStrip = AtlasResources.IMAGE_CRATER_WATER_NIGHT;
				}
				else
				{
					theImageStrip = AtlasResources.IMAGE_CRATER_WATER_DAY;
				}
				if (flag)
				{
					theCelCol = 1;
				}
				float num3 = (float)this.mGridY * 3.1415927f + (float)this.mGridX * 3.1415927f * 0.25f;
				float num4 = (float)this.mBoard.mMainCounter * 3.1415927f * 2f / 200f;
				float num5 = (float)Math.Sin((double)(num3 + num4)) * 2f;
				num2 += num5;
			}
			else if (this.mBoard.StageHasRoof())
			{
				if (this.mGridX < 5)
				{
					theImageStrip = AtlasResources.IMAGE_CRATER_ROOF_LEFT;
					num += 16f;
					num2 += -16f;
				}
				else
				{
					theImageStrip = AtlasResources.IMAGE_CRATER_ROOF_CENTER;
					num += 18f;
					num2 += -9f;
				}
				if (flag)
				{
					theCelCol = 1;
				}
			}
			else if (this.mBoard.StageIsNight())
			{
				theCelCol = 1;
				if (flag)
				{
					theImageStrip = AtlasResources.IMAGE_CRATER_FADING;
				}
			}
			else if (flag)
			{
				theImageStrip = AtlasResources.IMAGE_CRATER_FADING;
			}
			num = TodCommon.PixelAligned(num);
			num2 = TodCommon.PixelAligned(num2);
			TodCommon.TodDrawImageCelF(g, theImageStrip, num * Constants.S, num2 * Constants.S, theCelCol, 0);
			g.SetColorizeImages(false);
		}

		public void DrawGraveStone(Graphics g)
		{
			if (this.mGridItemCounter <= 0)
			{
				return;
			}
			int theTimeAge = TodCommon.TodAnimateCurve(0, 100, this.mGridItemCounter, 1000, 0, TodCurves.CURVE_EASE_IN_OUT);
			int num = this.mBoard.mGridCelLook[this.mGridX, this.mGridY];
			int num2 = this.mBoard.mGridCelOffset[this.mGridX, this.mGridY, 0];
			int num3 = this.mBoard.mGridCelOffset[this.mGridX, this.mGridY, 1];
			int num4 = (int)((float)AtlasResources.IMAGE_TOMBSTONES.GetCelWidth() * Constants.IS);
			int num5 = (int)((float)AtlasResources.IMAGE_TOMBSTONES.GetCelHeight() * Constants.IS);
			int num6 = num % 5;
			int num7;
			if (this.mGridY == 0)
			{
				num7 = 1;
			}
			else if (this.mGridItemState == GridItemState.GRIDITEM_STATE_GRAVESTONE_SPECIAL)
			{
				num7 = 0;
			}
			else
			{
				num7 = 2 + num % 2;
			}
			int num8 = TodCommon.TodAnimateCurve(0, 1000, theTimeAge, num5, 0, TodCurves.CURVE_EASE_IN_OUT);
			int num9 = TodCommon.TodAnimateCurve(0, 50, theTimeAge, 0, 14, TodCurves.CURVE_EASE_IN_OUT);
			int num10 = TodCommon.TodAnimateCurve(500, 1000, theTimeAge, num5, 0, TodCurves.CURVE_EASE_IN_OUT);
			int num11 = 0;
			Plant topPlantAt = this.mBoard.GetTopPlantAt(this.mGridX, this.mGridY, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
			if (topPlantAt != null && topPlantAt.mState == PlantState.STATE_GRAVEBUSTER_EATING)
			{
				num11 = TodCommon.TodAnimateCurve(400, 0, topPlantAt.mStateCountdown, 10, 40, TodCurves.CURVE_LINEAR);
			}
			TRect theSrcRect = new TRect((int)((float)(num4 * num6) * Constants.S), (int)((float)(num5 * num7 + num11) * Constants.S), (int)((float)num4 * Constants.S), (int)((float)(num8 - num9 - num11) * Constants.S));
			int num12 = this.mBoard.GridToPixelX(this.mGridX, this.mGridY) - 4 + num2;
			int num13 = this.mBoard.GridToPixelY(this.mGridX, this.mGridY) + num5 + num3;
			g.DrawImage(AtlasResources.IMAGE_TOMBSTONES, (int)((float)num12 * Constants.S), (int)((double)((float)(num13 - num8 + num11) * Constants.S) + 0.5), theSrcRect);
			int num14 = (int)((float)num10 * Constants.S);
			if (num14 >= (int)Constants.InvertAndScale(34f))
			{
				TRect theSrcRect2 = new TRect(AtlasResources.IMAGE_TOMBSTONE_MOUNDS.GetCelWidth() * num6, AtlasResources.IMAGE_TOMBSTONE_MOUNDS.GetCelHeight() * num7, AtlasResources.IMAGE_TOMBSTONE_MOUNDS.GetCelWidth(), (int)((float)num10 * Constants.S) - (int)Constants.InvertAndScale(34f));
				g.DrawImage(AtlasResources.IMAGE_TOMBSTONE_MOUNDS, (int)((float)num12 * Constants.S), (int)((float)(num13 - num10) * Constants.S) + (int)Constants.InvertAndScale(34f), theSrcRect2);
			}
		}

		public void GridItemDie()
		{
			this.mDead = true;
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mGridItemReanimID);
			if (reanimation != null)
			{
				reanimation.ReanimationDie();
				this.mGridItemReanimID = null;
			}
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mGridItemParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.ParticleSystemDie();
				this.mGridItemParticleID = null;
			}
		}

		public void AddGraveStoneParticles()
		{
			int num = this.mBoard.mGridCelOffset[this.mGridX, this.mGridY, 0];
			int num2 = this.mBoard.mGridCelOffset[this.mGridX, this.mGridY, 1];
			int num3 = this.mBoard.GridToPixelX(this.mGridX, this.mGridY) + 14 + num;
			int num4 = this.mBoard.GridToPixelY(this.mGridX, this.mGridY) + 78 + num2;
			this.mApp.AddTodParticle((float)num3, (float)num4, this.mRenderOrder + 1, ParticleEffect.PARTICLE_GRAVE_STONE_RISE);
			this.mApp.PlayFoley(FoleyType.FOLEY_DIRT_RISE);
		}

		public void DrawGridItem(Graphics g)
		{
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			switch (this.mGridItemType)
			{
			case GridItemType.GRIDITEM_GRAVESTONE:
				this.DrawGraveStone(g);
				break;
			case GridItemType.GRIDITEM_CRATER:
				this.DrawCrater(g);
				break;
			case GridItemType.GRIDITEM_LADDER:
				this.DrawLadder(g);
				break;
			case GridItemType.GRIDITEM_PORTAL_CIRCLE:
			case GridItemType.GRIDITEM_PORTAL_SQUARE:
			case GridItemType.GRIDITEM_ZEN_TOOL:
			case GridItemType.GRIDITEM_RAKE:
				break;
			case GridItemType.GRIDITEM_BRAIN:
				g.DrawImageF(AtlasResources.IMAGE_BRAIN, this.mPosX * Constants.S, this.mPosY * Constants.S);
				break;
			case GridItemType.GRIDITEM_SCARY_POT:
				this.DrawScaryPot(g);
				break;
			case GridItemType.GRIDITEM_SQUIRREL:
				this.DrawSquirrel(g);
				break;
			case GridItemType.GRIDITEM_STINKY:
				this.DrawStinky(g);
				return;
			case GridItemType.GRIDITEM_IZOMBIE_BRAIN:
				this.DrawIZombieBrain(g);
				break;
			default:
				Debug.ASSERT(false);
				break;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mGridItemReanimID);
			if (reanimation != null)
			{
				reanimation.Draw(g);
			}
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mGridItemParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.Draw(g, true);
			}
		}

		public void DrawGridItemOverlay(Graphics g)
		{
			GridItemType gridItemType = this.mGridItemType;
			if (gridItemType != GridItemType.GRIDITEM_STINKY)
			{
				return;
			}
			if (this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_CHOCOLATE && !this.mApp.mZenGarden.IsStinkyHighOnChocolate())
			{
				g.DrawImage(AtlasResources.IMAGE_PLANTSPEECHBUBBLE, (float)Constants.ZenGarden_StinkySpeechBubble_Pos.X + Constants.S * this.mPosX, (float)Constants.ZenGarden_StinkySpeechBubble_Pos.Y + Constants.S * this.mPosY);
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_CHOCOLATE, (float)Constants.ZenGarden_Chocolate_Pos.X + this.mPosX * Constants.S, (float)Constants.ZenGarden_Chocolate_Pos.Y + this.mPosY * Constants.S, 0.44f, 0.44f);
			}
		}

		public void OpenPortal()
		{
			float num = (float)this.mGridX * 80f - 6f;
			float num2 = (float)this.mBoard.GridToPixelY(0, this.mGridY) - 65f;
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mGridItemReanimID);
			if (reanimation == null)
			{
				ReanimationType theReanimationType = ReanimationType.REANIM_PORTAL_CIRCLE;
				if (this.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE)
				{
					num2 += 25f;
					num += -4f;
					theReanimationType = ReanimationType.REANIM_PORTAL_SQUARE;
				}
				reanimation = this.mApp.AddReanimation(num, num2, 0, theReanimationType);
				reanimation.mIsAttachment = true;
				this.mGridItemReanimID = this.mApp.ReanimationGetID(reanimation);
			}
			else
			{
				reanimation.SetPosition(num * Constants.S, num2 * Constants.S);
			}
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mGridItemParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.ParticleSystemDie();
				this.mGridItemParticleID = null;
			}
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_appear, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 12f);
			this.mApp.PlayFoley(FoleyType.FOLEY_PORTAL);
		}

		public void Update(int updateCount)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mGridItemReanimID);
			if (reanimation != null && updateCount == 0)
			{
				reanimation.Update();
			}
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mGridItemParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.Update();
			}
			if (this.mGridItemType == GridItemType.GRIDITEM_PORTAL_CIRCLE || this.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE)
			{
				this.UpdatePortal();
			}
			if (this.mGridItemType == GridItemType.GRIDITEM_SCARY_POT)
			{
				this.UpdateScaryPot();
			}
			if (this.mGridItemType == GridItemType.GRIDITEM_RAKE)
			{
				this.UpdateRake();
			}
			if (this.mGridItemType == GridItemType.GRIDITEM_IZOMBIE_BRAIN)
			{
				this.UpdateBrain();
			}
		}

		public void ClosePortal()
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mGridItemReanimID);
			if (reanimation != null)
			{
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_dissapear, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 12f);
			}
			TodParticleSystem todParticleSystem = this.mApp.ParticleTryToGet(this.mGridItemParticleID);
			if (todParticleSystem != null)
			{
				todParticleSystem.ParticleSystemDie();
				this.mGridItemParticleID = null;
			}
			this.mGridItemState = GridItemState.GRIDITEM_STATE_PORTAL_CLOSED;
		}

		public void DrawScaryPot(Graphics g)
		{
			int num = this.mGridItemState - GridItemState.GRIDITEM_STATE_SCARY_POT_QUESTION;
			Debug.ASSERT(num >= 0 && num < 3);
			int num2 = this.mBoard.GridToPixelX(this.mGridX, this.mGridY) - 5;
			int num3 = this.mBoard.GridToPixelY(this.mGridX, this.mGridY) - 15;
			if (this.mTransparentCounter > 0)
			{
				g.DrawImageCel(Resources.IMAGE_SCARY_POT, (int)((float)num2 * Constants.S), (int)((float)num3 * Constants.S), num, 0);
				Graphics @new = Graphics.GetNew(g);
				if (this.mScaryPotType == ScaryPotType.SCARYPOT_SEED)
				{
					@new.mScaleX = 0.7f;
					@new.mScaleY = 0.7f;
					SeedPacket.DrawSmallSeedPacket(@new, (float)(num2 + Constants.GridItem_ScaryPot_SeedPacket_Offset.X) * Constants.S, (float)(num3 + Constants.GridItem_ScaryPot_SeedPacket_Offset.Y) * Constants.S, this.mSeedType, SeedType.SEED_NONE, 0f, 255, false, false, true, false);
				}
				else if (this.mScaryPotType == ScaryPotType.SCARYPOT_ZOMBIE)
				{
					@new.mScaleX = 0.4f;
					@new.mScaleY = 0.4f;
					float num4 = (float)Constants.GridItem_ScaryPot_Zombie_Offset.X;
					float num5 = (float)Constants.GridItem_ScaryPot_Zombie_Offset.Y;
					if (this.mZombieType == ZombieType.ZOMBIE_FOOTBALL)
					{
						@new.mScaleX = 0.4f;
						@new.mScaleY = 0.4f;
						num4 = (float)Constants.GridItem_ScaryPot_ZombieFootball_Offset.X;
						num5 = (float)Constants.GridItem_ScaryPot_ZombieFootball_Offset.Y;
					}
					if (this.mZombieType == ZombieType.ZOMBIE_GARGANTUAR)
					{
						@new.mScaleX = 0.3f;
						@new.mScaleY = 0.3f;
						num4 += (float)Constants.GridItem_ScaryPot_ZombieGargantuar_Offset.X;
						num5 += (float)Constants.GridItem_ScaryPot_ZombieGargantuar_Offset.Y;
					}
					this.mApp.mReanimatorCache.DrawCachedZombie(@new, ((float)num2 + num4) * Constants.S, ((float)num3 + num5) * Constants.S, this.mZombieType);
				}
				else if (this.mScaryPotType == ScaryPotType.SCARYPOT_SUN)
				{
					int num6 = this.mBoard.mChallenge.ScaryPotterCountSunInPot(this);
					Reanimation newReanimation = Reanimation.GetNewReanimation();
					newReanimation.ReanimationInitializeType(0f, 0f, ReanimationType.REANIM_SUN);
					newReanimation.OverrideScale(0.5f, 0.5f);
					for (int i = 0; i < num6; i++)
					{
						float num7 = (float)Constants.GridItem_ScaryPot_Sun_Offset.X;
						float num8 = (float)Constants.GridItem_ScaryPot_Sun_Offset.Y;
						switch (i)
						{
						case 1:
							num7 += 3f;
							num8 += -20f;
							break;
						case 2:
							num7 += -6f;
							num8 += -10f;
							break;
						case 3:
							num7 += 6f;
							num8 += -5f;
							break;
						case 4:
							num7 += 5f;
							num8 += -15f;
							break;
						}
						newReanimation.SetPosition(((float)num2 + num7) * Constants.S, ((float)num3 + num8) * Constants.S);
						newReanimation.Draw(g);
					}
					newReanimation.PrepareForReuse();
				}
				@new.PrepareForReuse();
				int theAlpha = TodCommon.TodAnimateCurve(0, 50, this.mTransparentCounter, 255, 58, TodCurves.CURVE_LINEAR);
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, theAlpha));
			}
			g.DrawImageCel(Resources.IMAGE_SCARY_POT, (int)((float)num2 * Constants.S), (int)((float)num3 * Constants.S), num, 1);
			if (this.mHighlighted)
			{
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				g.SetColorizeImages(true);
				if (this.mTransparentCounter == 0)
				{
					g.SetColor(new SexyColor(255, 255, 255, 196));
				}
				g.DrawImageCel(Resources.IMAGE_SCARY_POT, (int)((float)num2 * Constants.S), (int)((float)num3 * Constants.S), num, 1);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			}
			g.SetColorizeImages(false);
		}

		public void UpdateScaryPot()
		{
			if (this.mApp.mTodCheatKeys && this.mApp.mWidgetManager.mKeyDown[16])
			{
				if (this.mTransparentCounter < 50)
				{
					this.mTransparentCounter++;
				}
				return;
			}
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant.mSeedType == SeedType.SEED_PLANTERN && !plant.NotOnGround())
				{
					int num = Math.Max(Math.Abs(plant.mPlantCol - this.mGridX), Math.Abs(plant.mRow - this.mGridY));
					if (num <= 1)
					{
						if (this.mTransparentCounter < 50)
						{
							this.mTransparentCounter++;
						}
						return;
					}
				}
			}
			if (this.mTransparentCounter > 0)
			{
				this.mTransparentCounter--;
			}
		}

		public void UpdatePortal()
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mGridItemReanimID);
			if (reanimation == null)
			{
				return;
			}
			if (this.mGridItemState == GridItemState.GRIDITEM_STATE_PORTAL_CLOSED)
			{
				if (reanimation.mLoopCount > 0)
				{
					this.GridItemDie();
					return;
				}
			}
			else if (reanimation.mLoopType == ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD && reanimation.mLoopCount > 0)
			{
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_pulse, ReanimLoopType.REANIM_LOOP, 0, 12f);
				ParticleEffect theEffect = ParticleEffect.PARTICLE_PORTAL_CIRCLE;
				float num = (float)this.mGridX * 80f + 13f;
				float num2 = (float)this.mBoard.GridToPixelY(0, this.mGridY) - 39f;
				if (this.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE)
				{
					theEffect = ParticleEffect.PARTICLE_PORTAL_SQUARE;
					num += -8f;
					num2 += 15f;
				}
				TodParticleSystem theParticle = this.mApp.AddTodParticle(num, num2, 0, theEffect);
				this.mGridItemParticleID = this.mApp.ParticleGetID(theParticle);
			}
		}

		public void DrawSquirrel(Graphics g)
		{
			int num = this.mBoard.GridToPixelX(this.mGridX, this.mGridY);
			int num2 = this.mBoard.GridToPixelY(this.mGridX, this.mGridY);
			if (this.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_PEEKING)
			{
				num2 += TodCommon.TodAnimateCurve(50, 0, this.mGridItemCounter, 0, -40, TodCurves.CURVE_BOUNCE_SLOW_MIDDLE);
				return;
			}
			if (this.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_UP)
			{
				num2 += TodCommon.TodAnimateCurve(50, 0, this.mGridItemCounter, 100, 0, TodCurves.CURVE_EASE_IN);
				return;
			}
			if (this.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_DOWN)
			{
				num2 += TodCommon.TodAnimateCurve(50, 0, this.mGridItemCounter, -100, 0, TodCurves.CURVE_EASE_IN);
				return;
			}
			if (this.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_LEFT)
			{
				num += TodCommon.TodAnimateCurve(50, 0, this.mGridItemCounter, 80, 0, TodCurves.CURVE_EASE_IN);
				return;
			}
			if (this.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_RIGHT)
			{
				num += TodCommon.TodAnimateCurve(50, 0, this.mGridItemCounter, -80, 0, TodCurves.CURVE_EASE_IN);
			}
		}

		public void UpdateRake()
		{
			if (this.mGridItemState == GridItemState.GRIDITEM_STATE_RAKE_ATTRACTING || this.mGridItemState == GridItemState.GRIDITEM_STATE_RAKE_WAITING)
			{
				Zombie zombie = this.RakeFindZombie();
				if (zombie != null)
				{
					Reanimation reanimation = this.mApp.ReanimationGet(this.mGridItemReanimID);
					reanimation.mAnimRate = 20f;
					this.mGridItemCounter = 200;
					this.mGridItemState = GridItemState.GRIDITEM_STATE_RAKE_TRIGGERED;
					this.mApp.PlayFoley(FoleyType.FOLEY_SWING);
					return;
				}
			}
			else if (this.mGridItemState == GridItemState.GRIDITEM_STATE_RAKE_TRIGGERED)
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mGridItemReanimID);
				if (reanimation2 != null && reanimation2.ShouldTriggerTimedEvent(0.8f))
				{
					Zombie zombie2 = this.RakeFindZombie();
					if (zombie2 != null)
					{
						zombie2.TakeDamage(1800, 0U);
						this.mApp.PlayFoley(FoleyType.FOLEY_BONK);
					}
				}
				this.mGridItemCounter -= 3;
				if (this.mGridItemCounter <= 0)
				{
					this.GridItemDie();
				}
			}
		}

		public Zombie RakeFindZombie()
		{
			TRect rect = new TRect((int)this.mPosX, (int)this.mPosY, 63, 80);
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && !zombie.IsDeadOrDying() && !zombie.IsBobsledTeamWithSled() && zombie.mRow - this.mGridY == 0 && zombie.EffectedByDamage(1U))
				{
					TRect zombieRect = zombie.GetZombieRect();
					int rectOverlap = GameConstants.GetRectOverlap(rect, zombieRect);
					if (rectOverlap >= 0)
					{
						return zombie;
					}
				}
			}
			return null;
		}

		public void DrawIZombieBrain(Graphics g)
		{
			if (this.mGridItemState == GridItemState.GRIDITEM_STATE_BRAIN_SQUISHED)
			{
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_BRAIN, this.mPosX * Constants.S, (this.mPosY + 20f) * Constants.S, 1f, 0.25f);
				return;
			}
			if (this.mBoard.mAdvice.mDuration > 0 && this.mBoard.mHelpIndex == AdviceType.ADVICE_I_ZOMBIE_EAT_ALL_BRAINS)
			{
				SexyColor flashingColor = TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 75);
				g.SetColorizeImages(true);
				g.SetColor(flashingColor);
			}
			g.DrawImageF(AtlasResources.IMAGE_BRAIN, this.mPosX * Constants.S, this.mPosY * Constants.S);
			if (this.mTransparentCounter > 0)
			{
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				g.SetColorizeImages(true);
				int theAlpha = TodCommon.ClampInt(this.mTransparentCounter * 3, 0, 255);
				g.SetColor(new SexyColor(255, 255, 255, theAlpha));
				g.DrawImageF(AtlasResources.IMAGE_BRAIN, this.mPosX * Constants.S, this.mPosY * Constants.S);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				g.SetColorizeImages(false);
			}
			g.SetColorizeImages(false);
		}

		public void UpdateBrain()
		{
			if (this.mGridItemState == GridItemState.GRIDITEM_STATE_BRAIN_SQUISHED)
			{
				this.mGridItemCounter -= 3;
				if (this.mGridItemCounter <= 0)
				{
					this.GridItemDie();
				}
			}
			if (this.mTransparentCounter > 0)
			{
				this.mTransparentCounter -= 3;
			}
		}

		public void DrawStinky(Graphics g)
		{
			Reanimation reanimation = this.mApp.ReanimationGet(this.mGridItemReanimID);
			float mAnimTime = reanimation.mAnimTime;
			Debug.ASSERT(this.mMotionTrailCount <= 12);
			for (int i = this.mMotionTrailCount - 1; i >= 0; i--)
			{
				if (i % 2 != 0)
				{
					MotionTrailFrame motionTrailFrame = this.mMotionTrailFrames[i];
					float num = motionTrailFrame.mPosX - this.mPosX;
					float num2 = motionTrailFrame.mPosY - this.mPosY;
					int theAlpha = TodCommon.TodAnimateCurve(0, 11, i, 64, 16, TodCurves.CURVE_LINEAR);
					g.SetColor(new SexyColor(255, 255, 255, theAlpha));
					g.SetColorizeImages(true);
					reanimation.mAnimTime = motionTrailFrame.mAnimTime;
					float num3 = (float)g.mTransX;
					float num4 = (float)g.mTransY;
					g.mTransX += (int)(num * Constants.S);
					g.mTransY += (int)(num2 * Constants.S);
					reanimation.Draw(g);
					g.SetColorizeImages(false);
					g.mTransX = (int)num3;
					g.mTransY = (int)num4;
				}
			}
			reanimation.mAnimTime = mAnimTime;
			if (this.mGridItemType == GridItemType.GRIDITEM_STINKY && this.mHighlighted)
			{
				reanimation.mEnableExtraAdditiveDraw = true;
				reanimation.mExtraAdditiveColor = new SexyColor(255, 255, 255, 196);
			}
			reanimation.Draw(g);
			reanimation.mEnableExtraAdditiveDraw = false;
		}

		public LawnApp mApp;

		public Board mBoard;

		public GridItemType mGridItemType;

		public GridItemState mGridItemState;

		public int mGridX;

		public int mGridY;

		public int mGridItemCounter;

		public int mRenderOrder;

		public bool mDead;

		public float mPosX;

		public float mPosY;

		public float mGoalX;

		public float mGoalY;

		public Reanimation mGridItemReanimID;

		public TodParticleSystem mGridItemParticleID;

		public ZombieType mZombieType;

		public SeedType mSeedType;

		public ScaryPotType mScaryPotType;

		public bool mHighlighted;

		public int mTransparentCounter;

		public int mSunCount;

		public MotionTrailFrame[] mMotionTrailFrames = new MotionTrailFrame[12];

		public int mMotionTrailCount;

		private static Stack<GridItem> unusedObjects = new Stack<GridItem>();
	}
}
