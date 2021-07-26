using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class CursorObject : GameObject
	{
		public CursorObject()
		{
			this.mType = SeedType.SEED_NONE;
			this.mImitaterType = SeedType.SEED_NONE;
			this.mSeedBankIndex = -1;
			this.mX = 0;
			this.mY = 0;
			this.mCursorType = CursorType.CURSOR_TYPE_NORMAL;
			this.mCoinID = null;
			this.mDuplicatorPlantID = null;
			this.mCobCannonPlantID = null;
			this.mGlovePlantID = null;
			this.mReanimCursorID = null;
			this.mPosScaled = false;
			if (this.mApp.IsWhackAZombieLevel())
			{
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_HAMMER, true);
				Reanimation reanimation = this.mApp.AddReanimation(-25f, 16f, 0, ReanimationType.REANIM_HAMMER);
				reanimation.mIsAttachment = true;
				reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_whack_zombie, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
				reanimation.mAnimTime = 1f;
				this.mReanimCursorID = this.mApp.ReanimationGetID(reanimation);
			}
			this.mWidth = 80;
			this.mHeight = 80;
		}

		public void Update()
		{
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING && !this.mBoard.mCutScene.IsInShovelTutorial())
			{
				this.mVisible = false;
				return;
			}
			if (!this.mApp.mWidgetManager.mMouseIn)
			{
				this.mVisible = false;
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mReanimCursorID);
			if (reanimation != null)
			{
				reanimation.Update();
			}
			this.mVisible = true;
			this.mX = this.mBoard.mLastToolX;
			this.mY = this.mBoard.mLastToolY;
		}

		public override bool LoadFromFile(Buffer b)
		{
			base.LoadFromFile(b);
			this.mCursorType = (CursorType)b.ReadLong();
			this.mHammerDownCounter = b.ReadLong();
			this.mImitaterType = (SeedType)b.ReadLong();
			this.mSeedBankIndex = b.ReadLong();
			this.mType = (SeedType)b.ReadLong();
			return true;
		}

		public override bool SaveToFile(Buffer b)
		{
			base.SaveToFile(b);
			b.WriteLong((int)this.mCursorType);
			b.WriteLong(this.mHammerDownCounter);
			b.WriteLong((int)this.mImitaterType);
			b.WriteLong(this.mSeedBankIndex);
			b.WriteLong((int)this.mType);
			return true;
		}

		public void DrawGroundLayer(Graphics g)
		{
			if (this.mCursorType != CursorType.CURSOR_TYPE_NORMAL)
			{
				int theX = (int)((float)this.mX * Constants.IS);
				int theY = (int)((float)this.mY * Constants.IS);
				int num;
				int num2;
				if (this.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_BANK)
				{
					if (this.mBoard.mIgnoreMouseUp)
					{
						return;
					}
					num = this.mBoard.PlantingPixelToGridX(theX, theY, this.mType);
					num2 = this.mBoard.PlantingPixelToGridY(theX, theY, this.mType);
					if (this.mBoard.CanPlantAt(num, num2, this.mType) != PlantingReason.PLANTING_OK)
					{
						return;
					}
				}
				else
				{
					num = this.mBoard.PixelToGridX(theX, theY);
					num2 = this.mBoard.PixelToGridY(theX, theY);
					if (this.mCursorType == CursorType.CURSOR_TYPE_SHOVEL && (this.mBoard.mIgnoreMouseUp || this.mBoard.ToolHitTest(this.mX, this.mY, false).mObjectType == GameObjectType.OBJECT_TYPE_NONE))
					{
						return;
					}
				}
				if (num2 < 0 || num < 0 || this.mBoard.GridToPixelY(num, num2) < 0)
				{
					return;
				}
				Graphics @new = Graphics.GetNew();
				@new.mTransX = Constants.Board_Offset_AspectRatio_Correction;
				for (int i = 0; i < Constants.GRIDSIZEX; i++)
				{
					this.mBoard.DrawCelHighlight(@new, i, num2);
				}
				int num3 = this.mBoard.StageHas6Rows() ? 6 : 5;
				for (int j = 0; j < num3; j++)
				{
					if (j != num2)
					{
						this.mBoard.DrawCelHighlight(@new, num, j);
					}
				}
				@new.PrepareForReuse();
			}
		}

		public void DrawTopLayer(Graphics g)
		{
			if (this.mCursorType == CursorType.CURSOR_TYPE_SHOVEL)
			{
				if (this.mBoard.mIgnoreMouseUp || (float)this.mX * Constants.IS < (float)Constants.LAWN_XMIN || (float)this.mY * Constants.IS < (float)Constants.LAWN_YMIN)
				{
					return;
				}
				int mWidth = AtlasResources.IMAGE_SHOVEL_HI_RES.mWidth;
				int mHeight = AtlasResources.IMAGE_SHOVEL_HI_RES.mHeight;
				g.SetColor(SexyColor.White);
				int num = (int)TodCommon.TodAnimateCurveFloat(0, 39, this.mBoard.mEffectCounter % 40, 0f, (float)(mWidth / 2), TodCurves.CURVE_BOUNCE_SLOW_MIDDLE);
				g.DrawImage(AtlasResources.IMAGE_SHOVEL_HI_RES, num, -mHeight - num, mWidth, mHeight);
				return;
			}
			else
			{
				if (this.mCursorType == CursorType.CURSOR_TYPE_HAMMER)
				{
					Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimCursorID);
					reanimation.Draw(g);
					return;
				}
				if (this.mCursorType != CursorType.CURSOR_TYPE_COBCANNON_TARGET)
				{
					if (this.mCursorType == CursorType.CURSOR_TYPE_WATERING_CAN && this.mApp.mPlayerInfo.mPurchases[13] > 0)
					{
						TRect trect = new TRect(Constants.ZEN_XMIN, Constants.ZEN_YMIN, Constants.ZEN_XMAX - Constants.ZEN_XMIN, Constants.ZEN_YMAX - Constants.ZEN_YMIN);
						if (trect.Contains(this.mApp.mBoard.mLastToolX, this.mApp.mBoard.mLastToolY))
						{
							int mWidth2 = AtlasResources.IMAGE_ZEN_GOLDTOOLRETICLE.mWidth;
							int mHeight2 = AtlasResources.IMAGE_ZEN_GOLDTOOLRETICLE.mHeight;
							g.DrawImage(AtlasResources.IMAGE_ZEN_GOLDTOOLRETICLE, -mWidth2 / 2, -mHeight2 / 2, mWidth2, mHeight2);
						}
					}
					return;
				}
				if ((float)this.mX * Constants.IS < (float)Constants.LAWN_XMIN || (float)this.mY * Constants.IS < (float)Constants.LAWN_YMIN)
				{
					return;
				}
				int mWidth3 = AtlasResources.IMAGE_COBCANNON_TARGET.mWidth;
				int mHeight3 = AtlasResources.IMAGE_COBCANNON_TARGET.mHeight;
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, 127));
				g.DrawImage(AtlasResources.IMAGE_COBCANNON_TARGET, -mWidth3 / 2, -mHeight3 / 2, mWidth3, mHeight3);
				g.SetColorizeImages(false);
				return;
			}
		}

		public void Die()
		{
			this.mApp.RemoveReanimation(ref this.mReanimCursorID);
		}

		public int mSeedBankIndex;

		public SeedType mType;

		public SeedType mImitaterType;

		public CursorType mCursorType;

		public Coin mCoinID;

		public Plant mGlovePlantID;

		public Plant mDuplicatorPlantID;

		public Plant mCobCannonPlantID;

		public int mHammerDownCounter;

		public Reanimation mReanimCursorID;
	}
}
