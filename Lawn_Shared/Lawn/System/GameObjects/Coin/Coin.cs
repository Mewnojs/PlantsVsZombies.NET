using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class Coin : GameObject
	{
		internal static void LoadSeedPacketImage(SeedType theSeedType)
		{
			Coin.LoadedSeedType = theSeedType;
		}

		public static void CoinFreeTextures()
		{
		}

		public void CoinInitialize(int theX, int theY, CoinType theCoinType, CoinMotion theCoinMotion)
		{
			this.mDead = false;
			this.mWidth = (int)Constants.InvertAndScale(23f);
			this.mHeight = (int)Constants.InvertAndScale(23f);
			this.mType = theCoinType;
			this.mPosX = (float)theX;
			this.mPosY = (float)theY;
			this.mDisappearCounter = 0;
			this.mIsBeingCollected = false;
			this.mFadeCount = 0;
			this.mCoinMotion = theCoinMotion;
			this.mCoinAge = 0;
			this.mAttachmentID = null;
			this.mCollectionDistance = 0f;
			this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_COIN_BANK, 0, 1);
			this.mScale = 1f;
			this.mUsableSeedType = SeedType.SEED_NONE;
			this.mNeedsBouncyArrow = false;
			this.mHasBouncyArrow = false;
			this.mHitGround = false;
			this.mTimesDropped = 0;
			this.mPottedPlantSpec.InitializePottedPlant(SeedType.SEED_NONE);
			int num = Constants.LAWN_XMIN + (int)Constants.InvertAndScale(40f);
			if (num + this.mWidth < Constants.LAWN_XMIN && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mPosX = (float)(num + this.mWidth);
			}
			if (this.IsSun())
			{
				float num2 = (float)this.mWidth * Constants.IS * 0.5f;
				float num3 = (float)this.mHeight * Constants.IS * 0.5f;
				Reanimation reanimation = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_SUN);
				reanimation.SetPosition((this.mPosX + num2) * Constants.S, (this.mPosY + num3) * Constants.S);
				reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation.mAnimRate = 6f;
				GlobalMembersAttachment.AttachReanim(ref this.mAttachmentID, reanimation, num2, num3);
			}
			else if (this.mType == CoinType.COIN_SILVER)
			{
				this.mPosX -= 10f;
				this.mPosY -= 8f;
				float num4 = 9f;
				float num5 = 9f;
				Reanimation reanimation2 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_COIN_SILVER);
				reanimation2.SetPosition((this.mPosX + num4) * Constants.S, (this.mPosY + num5) * Constants.S);
				reanimation2.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation2.mAnimTime = TodCommon.RandRangeFloat(0f, 0.99f);
				reanimation2.mAnimRate *= TodCommon.RandRangeFloat(0.6f, 1f);
				GlobalMembersAttachment.AttachReanim(ref this.mAttachmentID, reanimation2, num4, num5);
			}
			else if (this.mType == CoinType.COIN_GOLD)
			{
				this.mPosX -= 10f;
				this.mPosY -= 8f;
				float num6 = 9f;
				float num7 = 9f;
				Reanimation reanimation3 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_COIN_GOLD);
				reanimation3.SetPosition((this.mPosX + num6) * Constants.S, (this.mPosY + num7) * Constants.S);
				reanimation3.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation3.mAnimTime = TodCommon.RandRangeFloat(0f, 0.99f);
				reanimation3.mAnimRate *= TodCommon.RandRangeFloat(0.6f, 1f);
				GlobalMembersAttachment.AttachReanim(ref this.mAttachmentID, reanimation3, num6, num7);
			}
			else if (this.mType == CoinType.COIN_DIAMOND)
			{
				this.mPosX -= 15f;
				this.mPosY -= 15f;
				float num8 = -3f;
				float num9 = 4f;
				Reanimation reanimation4 = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_DIAMOND);
				reanimation4.SetPosition((this.mPosX + num8) * Constants.S, (this.mPosY + num9) * Constants.S);
				reanimation4.mLoopType = ReanimLoopType.REANIM_LOOP;
				reanimation4.mAnimTime = TodCommon.RandRangeFloat(0f, 0.99f);
				reanimation4.mAnimRate = TodCommon.RandRangeFloat(50f, 80f);
				GlobalMembersAttachment.AttachReanim(ref this.mAttachmentID, reanimation4, num8, num9);
			}
			if (this.mApp.IsStormyNightLevel())
			{
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			if (this.mType == CoinType.COIN_FINAL_SEED_PACKET)
			{
				this.mWidth = AtlasResources.IMAGE_SEEDPACKETS.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_SEEDPACKETS.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
				Coin.LoadSeedPacketImage(this.GetFinalSeedPacketType());
			}
			else if (this.mType == CoinType.COIN_TROPHY)
			{
				this.mWidth = AtlasResources.IMAGE_TROPHY.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_TROPHY.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.mType == CoinType.COIN_SHOVEL)
			{
				this.mWidth = AtlasResources.IMAGE_SHOVEL.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_SHOVEL.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.mType == CoinType.COIN_CARKEYS)
			{
				this.mWidth = AtlasResources.IMAGE_CARKEYS.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_CARKEYS.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.mType == CoinType.COIN_ALMANAC)
			{
				this.mWidth = AtlasResources.IMAGE_ALMANAC.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_ALMANAC.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.mType == CoinType.COIN_WATERING_CAN)
			{
				this.mWidth = AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.mType == CoinType.COIN_TACO)
			{
				this.mWidth = AtlasResources.IMAGE_TACO.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_TACO.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.mType == CoinType.COIN_BACON)
			{
				this.mWidth = AtlasResources.IMAGE_BACON.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_BACON.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.mType == CoinType.COIN_NOTE)
			{
				this.mWidth = Resources.IMAGE_ZOMBIE_NOTE_SMALL.GetCelWidth();
				this.mHeight = Resources.IMAGE_ZOMBIE_NOTE_SMALL.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.mType == CoinType.COIN_USABLE_SEED_PACKET)
			{
				this.mWidth = AtlasResources.IMAGE_SEEDPACKETS.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_SEEDPACKETS.GetCelHeight();
				this.mRenderOrder = 500002;
			}
			else if (this.mType == CoinType.COIN_PRESENT_PLANT)
			{
				this.mWidth = AtlasResources.IMAGE_PRESENT.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_PRESENT.GetCelHeight();
				if (this.mApp.IsSurvivalEndless(this.mApp.mGameMode) || this.mApp.IsEndlessIZombie(this.mApp.mGameMode) || this.mApp.IsEndlessScaryPotter(this.mApp.mGameMode))
				{
					SeedType theSeedType = this.mApp.mZenGarden.PickRandomSeedType();
					this.mPottedPlantSpec.InitializePottedPlant(theSeedType);
				}
				else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_1_DAY)
				{
					int[] array = new int[]
					{
						0,
						1,
						2,
						3,
						7,
						4,
						5,
						6
					};
					SeedType theSeedType2 = (SeedType)TodCommon.TodPickFromArray(array, array.Length);
					this.mPottedPlantSpec.InitializePottedPlant(theSeedType2);
				}
				else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_2_NIGHT)
				{
					int[] array2 = new int[]
					{
						8,
						9,
						10,
						11,
						12,
						13,
						14,
						15
					};
					SeedType theSeedType3 = (SeedType)TodCommon.TodPickFromArray(array2, array2.Length);
					this.mPottedPlantSpec.InitializePottedPlant(theSeedType3);
				}
				else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_3_POOL)
				{
					int[] array3 = new int[]
					{
						16,
						17,
						18,
						19,
						20,
						21,
						22,
						23
					};
					SeedType theSeedType4 = (SeedType)TodCommon.TodPickFromArray(array3, array3.Length);
					this.mPottedPlantSpec.InitializePottedPlant(theSeedType4);
				}
				else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_4_FOG)
				{
					int[] array4 = new int[]
					{
						24,
						25,
						26,
						27,
						28,
						29,
						30,
						31
					};
					SeedType theSeedType5 = (SeedType)TodCommon.TodPickFromArray(array4, array4.Length);
					this.mPottedPlantSpec.InitializePottedPlant(theSeedType5);
				}
				else if (this.mBoard.mBackground == BackgroundType.BACKGROUND_5_ROOF)
				{
					int[] array5 = new int[]
					{
						32,
						34,
						35,
						36,
						37,
						39
					};
					SeedType theSeedType6 = (SeedType)TodCommon.TodPickFromArray(array5, array5.Length);
					this.mPottedPlantSpec.InitializePottedPlant(theSeedType6);
				}
				else
				{
					SeedType theSeedType7 = this.mApp.mZenGarden.PickRandomSeedType();
					this.mPottedPlantSpec.InitializePottedPlant(theSeedType7);
				}
			}
			else if (this.mType == CoinType.COIN_AWARD_MONEY_BAG || this.mType == CoinType.COIN_AWARD_BAG_DIAMOND)
			{
				this.mWidth = AtlasResources.IMAGE_MONEYBAG_HI_RES.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_MONEYBAG_HI_RES.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.mType == CoinType.COIN_CHOCOLATE || this.mType == CoinType.COIN_AWARD_CHOCOLATE)
			{
				this.mWidth = AtlasResources.IMAGE_CHOCOLATE.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_CHOCOLATE.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			else if (this.IsPresentWithAdvice())
			{
				this.mWidth = AtlasResources.IMAGE_PRESENT.GetCelWidth();
				this.mHeight = AtlasResources.IMAGE_PRESENT.GetCelHeight();
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 0);
			}
			this.mWidth = (int)((float)this.mWidth * Constants.IS);
			this.mHeight = (int)((float)this.mHeight * Constants.IS);
			switch (this.mCoinMotion)
			{
			case CoinMotion.COIN_MOTION_FROM_SKY:
				this.mVelY = 0.67f;
				this.mVelX = 0f;
				this.mGroundY = RandomNumbers.NextNumber(250) + 300;
				break;
			case CoinMotion.COIN_MOTION_FROM_SKY_SLOW:
				this.mVelY = 0.33f;
				this.mVelX = 0f;
				this.mGroundY = RandomNumbers.NextNumber(250) + 300;
				break;
			case CoinMotion.COIN_MOTION_FROM_PLANT:
				this.mVelY = -1.7f - TodCommon.RandRangeFloat(0f, 1.7f);
				this.mVelX = -0.4f + TodCommon.RandRangeFloat(0f, 0.8f);
				this.mGroundY = (int)this.mPosY + 15 + RandomNumbers.NextNumber(20);
				this.mScale = 0.4f;
				break;
			case CoinMotion.COIN_MOTION_COIN:
				this.mVelY = -3f - TodCommon.RandRangeFloat(0f, 2f);
				this.mVelX = -0.5f + TodCommon.RandRangeFloat(0f, 1f);
				this.mGroundY = (int)this.mPosY + 45 + RandomNumbers.NextNumber(20);
				if (this.mGroundY > Constants.BOARD_HEIGHT - 60 && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
				{
					this.mGroundY = Constants.BOARD_HEIGHT - 60;
				}
				if (this.mGroundY < 80 && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
				{
					this.mGroundY = 80;
				}
				if (this.mType == CoinType.COIN_FINAL_SEED_PACKET || this.mType == CoinType.COIN_USABLE_SEED_PACKET || this.mType == CoinType.COIN_TROPHY || this.mType == CoinType.COIN_SHOVEL || this.mType == CoinType.COIN_CARKEYS || this.mType == CoinType.COIN_ALMANAC || this.mType == CoinType.COIN_TACO || this.mType == CoinType.COIN_BACON || this.mType == CoinType.COIN_WATERING_CAN || this.mType == CoinType.COIN_NOTE)
				{
					this.mGroundY -= 30;
				}
				break;
			case CoinMotion.COIN_MOTION_LAWNMOWER_COIN:
				this.mVelY = 0f;
				this.mVelX = 0f;
				this.mGroundY = 600;
				this.Collect();
				break;
			case CoinMotion.COIN_MOTION_FROM_PRESENT:
				this.mVelY = 0f;
				this.mVelX = 0f;
				this.mGroundY = 600;
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 1);
				break;
			case CoinMotion.COIN_MOTION_FROM_BOSS:
				this.mVelY = -5f;
				this.mVelX = -3f;
				this.mPosX = 750f;
				this.mPosY = 245f;
				this.mGroundY = (int)this.mPosY + 40;
				break;
			default:
				Debug.ASSERT(false);
				break;
			}
			if (this.mCoinMotion != CoinMotion.COIN_MOTION_LAWNMOWER_COIN && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN && this.mPosX - (float)this.mWidth < (float)Constants.LAWN_XMIN)
			{
				this.mPosX = (float)(Constants.LAWN_XMIN + this.mWidth);
			}
			this.mScale *= this.GetSunScale();
			if (this.CoinGetsBouncyArrow())
			{
				this.mNeedsBouncyArrow = true;
			}
			if (this.mCoinMotion != CoinMotion.COIN_MOTION_FROM_PRESENT)
			{
				this.PlayLaunchSound();
			}
			if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				Coin.CheckRange_X(ref this.mPosX, this.mWidth, theCoinMotion);
			}
			this.Update();
		}

		public void Dispose()
		{
			GlobalMembersAttachment.AttachmentDie(ref this.mAttachmentID);
		}

		public void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mBoard == null || this.mBoard.mPaused || this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				return;
			}
			if (this.mDead)
			{
				return;
			}
			if (theClickCount >= 0 && !this.mIsBeingCollected)
			{
				this.PlayCollectSound();
				this.Collect();
				if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 1)
				{
					this.mBoard.DisplayAdvice("[ADVICE_CLICKED_ON_SUN]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1_STAY, AdviceType.ADVICE_CLICKED_ON_SUN);
				}
			}
		}

		public bool MouseHitTest(int theX, int theY, out HitResult theHitResult)
		{
			theHitResult = default(HitResult);
			theX = (int)((float)theX * Constants.IS);
			theY = (int)((float)theY * Constants.IS);
			int num = 0;
			if (this.mType == CoinType.COIN_AWARD_PRESENT || this.IsPresentWithAdvice() || this.mType == CoinType.COIN_PRESENT_PLANT)
			{
				num = -60;
			}
			int num2 = 0;
			int num3 = 0;
			if (this.mApp.IsWhackAZombieLevel())
			{
				num3 = 30;
				num2 = 15;
			}
			if (this.IsMoney() || this.mType == CoinType.COIN_AWARD_MONEY_BAG || this.mType == CoinType.COIN_AWARD_BAG_DIAMOND)
			{
				num2 = 40;
			}
			if (this.mType == CoinType.COIN_SUN || this.mType == CoinType.COIN_SMALLSUN)
			{
				num2 = 50;
			}
			bool flag = !this.mDead && (!this.mIsBeingCollected || (this.mType == CoinType.COIN_USABLE_SEED_PACKET && this.mBoard.mIgnoreNextMouseUpSeedPacket)) && (this.mType != CoinType.COIN_USABLE_SEED_PACKET || this.mBoard == null || this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL || this.mApp.IsWhackAZombieLevel() || this.mBoard.mIgnoreNextMouseUpSeedPacket) && ((float)theX >= this.mPosX - (float)num2 && (float)theX < this.mPosX + (float)this.mWidth + (float)num2 && (float)theY >= this.mPosY + (float)num - (float)num2 && (float)theY < this.mPosY + (float)this.mHeight + (float)num + (float)num2 + (float)num3);
			if (flag)
			{
				theHitResult.mObject = this;
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_COIN;
				return true;
			}
			theHitResult.mObject = null;
			theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_NONE;
			return false;
		}

		public void Die()
		{
			Debug.ASSERT(this.mBoard == null || this.mBoard.mCursorObject.mCoinID != this.mBoard.mCoins[this.mBoard.mCoins.IndexOf(this)]);
			this.mDead = true;
			GlobalMembersAttachment.AttachmentDie(ref this.mAttachmentID);
		}

		public void StartFade()
		{
			this.mFadeCount = 15;
		}

		public void Update()
		{
			this.mCoinAge += 3;
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING && this.mApp.mGameScene != GameScenes.SCENE_AWARD && this.mBoard != null && !this.mBoard.mCutScene.ShouldRunUpsellBoard())
			{
				return;
			}
			if (this.mFadeCount < 0 || this.mFadeCount >= 3)
			{
				this.UpdateFade();
			}
			else if (!this.mIsBeingCollected)
			{
				this.UpdateFall();
			}
			else
			{
				this.UpdateCollected();
			}
			if (this.mAttachmentID != null)
			{
				float num = 0f;
				float num2 = 0f;
				if (this.mType == CoinType.COIN_DIAMOND)
				{
					float num3 = Constants.InvertAndScale(18f);
					float num4 = Constants.InvertAndScale(13f);
					num = num3 - num3 * this.mScale;
					num2 = num4 - num4 * this.mScale;
				}
				GlobalMembersAttachment.AttachmentUpdateAndMove(ref this.mAttachmentID, this.mPosX + num, this.mPosY + num2);
				GlobalMembersAttachment.AttachmentOverrideColor(this.mAttachmentID, this.GetColor());
				GlobalMembersAttachment.AttachmentOverrideScale(this.mAttachmentID, this.mScale);
				if ((!this.mHitGround || this.mIsBeingCollected) && (this.mType == CoinType.COIN_SILVER || this.mType == CoinType.COIN_GOLD))
				{
					GlobalMembersAttachment.AttachmentOverrideColor(this.mAttachmentID, new SexyColor(0, 0, 0, 0));
				}
			}
		}

		public void Draw(Graphics g)
		{
			g.SetColor(this.GetColor());
			if (this.mType == CoinType.COIN_DIAMOND)
			{
				g.SetColorizeImages(true);
				g.DrawImage(AtlasResources.IMAGE_AWARDPICKUPGLOW, (this.mPosX - 56f) * Constants.S, (this.mPosY - 66f) * Constants.S);
				g.SetColorizeImages(false);
			}
			if (this.mType == CoinType.COIN_PRESENT_PLANT)
			{
				g.SetColorizeImages(true);
				g.DrawImage(AtlasResources.IMAGE_AWARDPICKUPGLOW, (this.mPosX - 50f) * Constants.S, (this.mPosY - 70f) * Constants.S);
				g.SetColorizeImages(false);
			}
			if (this.mType == CoinType.COIN_AWARD_PRESENT && this.mIsBeingCollected)
			{
				g.SetColorizeImages(true);
				g.DrawImage(AtlasResources.IMAGE_AWARDPICKUPGLOW, (this.mPosX - 50f) * Constants.S, (this.mPosY - 64f) * Constants.S);
				g.SetColorizeImages(false);
			}
			if (this.mType == CoinType.COIN_CHOCOLATE || this.mType == CoinType.COIN_AWARD_CHOCOLATE)
			{
				g.SetColorizeImages(true);
				g.DrawImage(AtlasResources.IMAGE_AWARDPICKUPGLOW, (this.mPosX - 56f) * Constants.S, (this.mPosY - 50f) * Constants.S);
				g.SetColorizeImages(false);
			}
			if (this.mAttachmentID != null)
			{
				Graphics @new = Graphics.GetNew(g);
				base.MakeParentGraphicsFrame(@new);
				GlobalMembersAttachment.AttachmentDraw(this.mAttachmentID, @new, false, true);
				@new.PrepareForReuse();
			}
			if ((this.mType == CoinType.COIN_SILVER || this.mType == CoinType.COIN_GOLD) && this.mHitGround && !this.mIsBeingCollected)
			{
				return;
			}
			if (this.mType == CoinType.COIN_DIAMOND)
			{
				return;
			}
			if (this.IsLevelAward() && !this.mIsBeingCollected)
			{
				SexyColor flashingColor = TodCommon.GetFlashingColor(this.mCoinAge, 75);
				g.SetColor(flashingColor);
			}
			if (this.mType == CoinType.COIN_SILVER || this.mType == CoinType.COIN_GOLD)
			{
				g.SetColorizeImages(true);
				TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_REANIM_COINGLOW, (this.mPosX - (float)Constants.Coin_Glow_Offset.X) * Constants.S, (this.mPosY - (float)Constants.Coin_Glow_Offset.Y) * Constants.S, this.mScale, this.mScale);
				g.SetColorizeImages(false);
			}
			Image theImageStrip = null;
			int theCelCol = 0;
			float num = this.mScale;
			float num2 = 0f;
			float num3 = 0f;
			if (this.mType == CoinType.COIN_SILVER)
			{
				theImageStrip = AtlasResources.IMAGE_REANIM_COIN_SILVER_DOLLAR;
				num2 = 8f;
				num3 = 10f;
			}
			else if (this.mType == CoinType.COIN_GOLD)
			{
				theImageStrip = AtlasResources.IMAGE_REANIM_COIN_GOLD_DOLLAR;
				num2 = 8f;
				num3 = 10f;
			}
			else
			{
				if (this.mType == CoinType.COIN_SUN || this.mType == CoinType.COIN_SMALLSUN || this.mType == CoinType.COIN_LARGESUN)
				{
					return;
				}
				if (this.mType == CoinType.COIN_FINAL_SEED_PACKET)
				{
					TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_SEEDPACKETS, (float)((int)(this.mPosX * Constants.S)), (float)((int)(this.mPosY * Constants.S)), this.mScale, this.mScale, (int)Coin.LoadedSeedType, true);
					return;
				}
				if (this.mType == CoinType.COIN_PRESENT_PLANT || this.mType == CoinType.COIN_AWARD_PRESENT)
				{
					if (this.mIsBeingCollected)
					{
						this.mApp.mZenGarden.DrawPottedPlantIcon(g, (this.mPosX + 35f) * Constants.S, (this.mPosY + 15f) * Constants.S, this.mPottedPlantSpec);
						return;
					}
					theImageStrip = AtlasResources.IMAGE_PRESENT;
					num3 = -60f;
				}
				else if (this.IsPresentWithAdvice())
				{
					num3 = -60f;
					if (this.mIsBeingCollected)
					{
						num2 = -10f;
						num3 -= -10f;
						theImageStrip = AtlasResources.IMAGE_PRESENTOPEN;
					}
					else
					{
						theImageStrip = AtlasResources.IMAGE_PRESENT;
					}
				}
				else if (this.mType == CoinType.COIN_AWARD_MONEY_BAG || this.mType == CoinType.COIN_AWARD_BAG_DIAMOND)
				{
					if (this.mIsBeingCollected && this.mApp.IsQuickPlayMode())
					{
						return;
					}
					theImageStrip = AtlasResources.IMAGE_MONEYBAG_HI_RES;
					if (this.mScale == 1f)
					{
						num2 -= (float)(this.mWidth / 4 + 10);
						num3 -= (float)(this.mHeight / 4);
					}
					num *= 0.5f;
				}
				else if (this.mType == CoinType.COIN_CHOCOLATE || this.mType == CoinType.COIN_AWARD_CHOCOLATE)
				{
					theImageStrip = AtlasResources.IMAGE_CHOCOLATE;
				}
				else if (this.mType == CoinType.COIN_TROPHY)
				{
					theImageStrip = AtlasResources.IMAGE_TROPHY_HI_RES;
					num2 -= (float)(this.mWidth / 2);
					num3 -= (float)(this.mHeight / 2);
					num *= 0.5f;
				}
				else if (this.mType == CoinType.COIN_SHOVEL)
				{
					theImageStrip = AtlasResources.IMAGE_SHOVEL_HI_RES;
					num2 -= (float)Constants.Coin_Shovel_Offset.X;
					num3 -= (float)Constants.Coin_Shovel_Offset.Y;
					num *= 0.5f;
				}
				else if (this.mType == CoinType.COIN_CARKEYS)
				{
					theImageStrip = AtlasResources.IMAGE_CARKEYS;
				}
				else if (this.mType == CoinType.COIN_ALMANAC)
				{
					theImageStrip = AtlasResources.IMAGE_ALMANAC;
				}
				else if (this.mType == CoinType.COIN_TACO)
				{
					theImageStrip = AtlasResources.IMAGE_TACO;
				}
				else if (this.mType == CoinType.COIN_BACON)
				{
					theImageStrip = AtlasResources.IMAGE_BACON;
				}
				else if (this.mType == CoinType.COIN_WATERING_CAN)
				{
					num2 -= (float)Constants.Coin_Shovel_Offset.X;
					num3 -= (float)Constants.Coin_Shovel_Offset.Y;
					theImageStrip = AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1;
				}
				else if (this.mType == CoinType.COIN_NOTE)
				{
					theImageStrip = Resources.IMAGE_ZOMBIE_NOTE_SMALL;
				}
				else
				{
					if (this.mType == CoinType.COIN_USABLE_SEED_PACKET)
					{
						int theGrayness = 255;
						if (this.mIsBeingCollected)
						{
							theGrayness = 128;
						}
						else
						{
							int disappearTime = this.GetDisappearTime();
							if (this.mDisappearCounter > disappearTime - 300 && this.mDisappearCounter % 60 < 30)
							{
								theGrayness = 192;
							}
						}
						g.SetColorizeImages(true);
						if (g.mDrawMode == Graphics.DrawMode.DRAWMODE_ADDITIVE)
						{
							int num4 = 0;
							num4++;
						}
						SeedPacket.DrawSmallSeedPacket(g, (float)((int)(this.mPosX * Constants.S)), (float)((int)(this.mPosY * Constants.S)), this.mUsableSeedType, SeedType.SEED_NONE, 0f, theGrayness, false, false, true, false);
						g.SetColorizeImages(false);
						return;
					}
					Debug.ASSERT(false);
				}
			}
			g.SetColorizeImages(true);
			TodCommon.TodDrawImageCelCenterScaledF(g, theImageStrip, (this.mPosX + num2) * Constants.S, (this.mPosY + num3) * Constants.S, theCelCol, num, num);
			g.SetColorizeImages(false);
		}

		public void Collect()
		{
			if (this.mDead)
			{
				return;
			}
			this.mCollectX = this.mPosX;
			this.mCollectY = this.mPosY;
			this.mIsBeingCollected = true;
			bool flag = false;
			if ((this.mApp.IsEndlessIZombie(this.mApp.mGameMode) || this.mApp.IsEndlessScaryPotter(this.mApp.mGameMode)) && this.IsLevelAward())
			{
				flag = true;
			}
			if (this.mType == CoinType.COIN_AWARD_PRESENT || this.mType == CoinType.COIN_PRESENT_PLANT)
			{
				Debug.ASSERT(this.mBoard != null);
				if (this.mApp.mZenGarden.IsZenGardenFull(false))
				{
					this.mBoard.DisplayAdvice("[DIALOG_ZEN_GARDEN_FULL]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
				}
				else
				{
					this.mBoard.mPottedPlantsCollected++;
					this.mBoard.DisplayAdvice("[ADVICE_FOUND_PLANT]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
					this.mApp.AddTodParticle(this.mPosX + Constants.InvertAndScale(30f), this.mPosY + Constants.InvertAndScale(30f), this.mRenderOrder + 1, ParticleEffect.PARTICLE_PRESENT_PICKUP);
					this.mApp.mZenGarden.AddPottedPlant(this.mPottedPlantSpec);
				}
				this.mDisappearCounter = 0;
				this.mFadeCount = 0;
				if (flag)
				{
					GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_AWARD_PICKUP_ARROW, null);
					this.mBoard.FadeOutLevel();
				}
				return;
			}
			if (this.mType == CoinType.COIN_PRESENT_MINIGAMES)
			{
				this.mApp.AddTodParticle(this.mPosX + 30f, this.mPosY + 30f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_PRESENT_PICKUP);
				this.mDisappearCounter = 0;
				this.mFadeCount = 0;
				GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_AWARD_PICKUP_ARROW, null);
				this.mApp.mPlayerInfo.UnlockFirstMiniGames();
				return;
			}
			if (this.mType == CoinType.COIN_PRESENT_PUZZLE_MODE)
			{
				this.mApp.AddTodParticle(this.mPosX + 30f, this.mPosY + 30f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_PRESENT_PICKUP);
				this.mDisappearCounter = 0;
				this.mFadeCount = 0;
				GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_AWARD_PICKUP_ARROW, null);
				this.mApp.mPlayerInfo.UnlockPuzzleMode();
				return;
			}
			if (this.mType == CoinType.COIN_CHOCOLATE || this.mType == CoinType.COIN_AWARD_CHOCOLATE)
			{
				this.mBoard.mChocolateCollected++;
				this.mApp.AddTodParticle(this.mPosX + 30f, this.mPosY + 30f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_PRESENT_PICKUP);
				if (this.mApp.mPlayerInfo.mPurchases[26] < 1000)
				{
					this.mBoard.DisplayAdvice("[ADVICE_FOUND_CHOCOLATE]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_NONE);
					this.mApp.mPlayerInfo.mPurchases[26] = 1001;
				}
				else
				{
					this.mApp.mPlayerInfo.mPurchases[26]++;
				}
				this.mDisappearCounter = 0;
				this.StartFade();
				if (flag)
				{
					GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_AWARD_PICKUP_ARROW, null);
					this.mBoard.FadeOutLevel();
				}
				return;
			}
			if (this.IsLevelAward())
			{
				if (this.mApp.IsQuickPlayMode() && this.mType == CoinType.COIN_AWARD_MONEY_BAG)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_COIN);
					this.FanOutCoins(CoinType.COIN_GOLD, 5);
					this.StartFade();
				}
				else if (flag)
				{
					if (this.mType == CoinType.COIN_AWARD_BAG_DIAMOND)
					{
						this.mApp.PlaySample(Resources.SOUND_DIAMOND);
						this.FanOutCoins(CoinType.COIN_DIAMOND, 1);
						this.StartFade();
					}
					else if (this.mType == CoinType.COIN_AWARD_MONEY_BAG)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_COIN);
						this.FanOutCoins(CoinType.COIN_GOLD, 5);
						this.StartFade();
					}
				}
				else if (this.mApp.IsScaryPotterLevel())
				{
					if (this.mType == CoinType.COIN_TROPHY)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_COIN);
						this.FanOutCoins(CoinType.COIN_GOLD, 5);
					}
					else if (this.mType == CoinType.COIN_AWARD_MONEY_BAG)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_COIN);
						this.FanOutCoins(CoinType.COIN_GOLD, 2);
					}
				}
				else if (this.mApp.IsAdventureMode() && this.mBoard.mLevel == 50)
				{
					this.FanOutCoins(CoinType.COIN_DIAMOND, 3);
				}
				else if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 4)
				{
					this.mApp.PlaySample(Resources.SOUND_SHOVEL);
				}
				else if (this.mApp.IsFirstTimeAdventureMode() && (this.mBoard.mLevel == 24 || this.mBoard.mLevel == 34 || this.mBoard.mLevel == 44))
				{
					this.mApp.PlaySample(Resources.SOUND_TAP2);
				}
				else if (this.mType == CoinType.COIN_TROPHY)
				{
					this.mApp.PlaySample(Resources.SOUND_DIAMOND);
					this.FanOutCoins(CoinType.COIN_DIAMOND, 1);
				}
				else if (this.mType == CoinType.COIN_AWARD_MONEY_BAG)
				{
					int theNumCoins = 5;
					this.mApp.PlayFoley(FoleyType.FOLEY_COIN);
					this.FanOutCoins(CoinType.COIN_GOLD, theNumCoins);
				}
				else
				{
					this.mApp.PlaySample(Resources.SOUND_SEEDLIFT);
					this.mApp.PlaySample(Resources.SOUND_TAP2);
				}
				this.mApp.AddTodParticle(this.mPosX + 30f, this.mPosY + 30f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_STARBURST);
				Debug.ASSERT(this.mBoard != null);
				this.mBoard.FadeOutLevel();
				GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_SEED_PACKET, null);
				GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_AWARD_PICKUP_ARROW, null);
				GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_COIN_PICKUP_ARROW, null);
				if (this.mType == CoinType.COIN_NOTE)
				{
					this.mApp.AddTodParticle(this.mPosX + Constants.InvertAndScale(30f), this.mPosY + Constants.InvertAndScale(30f), this.mRenderOrder + 1, ParticleEffect.PARTICLE_PRESENT_PICKUP);
					this.StartFade();
				}
				else if (!flag && this.mApp.Is3DAccelerated() && !this.mApp.IsQuickPlayMode())
				{
					float num = (float)(this.mWidth / 2);
					float num2 = (float)(this.mHeight / 2);
					TodParticleSystem theParticleSystem = this.mApp.AddTodParticle(this.mPosX + num, this.mPosY + num2, this.mRenderOrder - 1, ParticleEffect.PARTICLE_SEED_PACKET_PICKUP);
					GlobalMembersAttachment.AttachParticle(ref this.mAttachmentID, theParticleSystem, num, num2);
				}
				this.mDisappearCounter = 0;
				return;
			}
			if (this.mType == CoinType.COIN_USABLE_SEED_PACKET)
			{
				Debug.ASSERT(this.mBoard != null);
				this.mBoard.mCursorObject.mType = this.mUsableSeedType;
				this.mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_PLANT_FROM_USABLE_COIN;
				this.mBoard.mCursorObject.mCoinID = this.mBoard.mCoins[this.mBoard.mCoins.IndexOf(this)];
				this.mGroundY = (int)this.mPosY;
				this.mFadeCount = 0;
				this.mBoard.mIgnoreNextMouseUpSeedPacket = true;
				return;
			}
			if (this.IsMoney() && this.mBoard != null)
			{
				this.mBoard.ShowCoinBank();
			}
			this.mFadeCount = 0;
			if (this.IsSun() && this.mBoard != null && !this.mBoard.HasConveyorBeltSeedBank())
			{
				for (int i = 0; i < this.mBoard.mSeedBank.mNumPackets; i++)
				{
					SeedPacket seedPacket = this.mBoard.mSeedBank.mSeedPackets[i];
					int currentPlantCost = this.mBoard.GetCurrentPlantCost(seedPacket.mPacketType, seedPacket.mImitaterType);
					int num3 = this.mBoard.mSunMoney + this.mBoard.CountSunBeingCollected() - currentPlantCost;
					if (num3 >= 0 && num3 < this.GetSunValue())
					{
						seedPacket.FlashIfReady();
					}
				}
				if (this.mBoard.StageHasFog())
				{
					this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_ABOVE_UI, 0, 2);
				}
			}
			GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref this.mAttachmentID, ParticleEffect.PARTICLE_COIN_PICKUP_ARROW, null);
			if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard != null && this.mBoard.mLevel == 11 && (this.mType == CoinType.COIN_GOLD || this.mType == CoinType.COIN_SILVER))
			{
				this.mBoard.DisplayAdvice("[ADVICE_CLICKED_ON_COIN]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_CLICKED_ON_COIN);
			}
		}

		public int GetSunValue()
		{
			if (this.mType == CoinType.COIN_SUN)
			{
				return 25;
			}
			if (this.mType == CoinType.COIN_SMALLSUN)
			{
				return 15;
			}
			if (this.mType == CoinType.COIN_LARGESUN)
			{
				return 50;
			}
			return 0;
		}

		public static int GetCoinValue(CoinType theType)
		{
			if (theType == CoinType.COIN_SILVER)
			{
				return 1;
			}
			if (theType == CoinType.COIN_GOLD)
			{
				return 5;
			}
			if (theType == CoinType.COIN_DIAMOND)
			{
				return 100;
			}
			return 0;
		}

		private void UpdateFade()
		{
			if (!this.mApp.IsEndlessIZombie(this.mApp.mGameMode) && !this.mApp.IsEndlessScaryPotter(this.mApp.mGameMode) && this.mType != CoinType.COIN_NOTE && this.IsLevelAward())
			{
				return;
			}
			this.mFadeCount -= 3;
			if (this.mFadeCount >= 0 && this.mFadeCount < 3)
			{
				if (this.mType == CoinType.COIN_SILVER || this.mType == CoinType.COIN_GOLD)
				{
					this.mBoard.mCollectedCoinStreak = 0;
				}
				this.Die();
			}
		}

		private static void CheckRange_X(ref float mPosX, int mWidth, CoinMotion mCoinMotion)
		{
			if (mPosX > (float)(800 - mWidth) && mCoinMotion != CoinMotion.COIN_MOTION_FROM_BOSS)
			{
				mPosX = (float)(800 - mWidth);
				return;
			}
			if (mPosX < 0f)
			{
				mPosX = 0f;
			}
		}

		private void UpdateFall()
		{
			if (this.IsPresentWithAdvice())
			{
				this.mDisappearCounter = 0;
				if (this.mCoinAge > 500)
				{
					this.Collect();
				}
			}
			if (this.mCoinMotion == CoinMotion.COIN_MOTION_FROM_PRESENT)
			{
				this.mPosX += 3f * this.mVelX;
				this.mPosY += 3f * this.mVelY;
				this.mVelX *= 0.857f;
				this.mVelY *= 0.857f;
				if (this.mCoinAge >= 80)
				{
					this.Collect();
				}
			}
			else if (this.mPosY + this.mVelY < (float)this.mGroundY)
			{
				this.mPosY += 3f * this.mVelY;
				if (this.mCoinMotion == CoinMotion.COIN_MOTION_FROM_PLANT)
				{
					this.mVelY += 3f * Constants.InvertAndScale(0.09f);
				}
				else if (this.mCoinMotion == CoinMotion.COIN_MOTION_COIN || this.mCoinMotion == CoinMotion.COIN_MOTION_FROM_BOSS)
				{
					this.mVelY += 3f * Constants.InvertAndScale(0.15f);
				}
				this.mPosX += 3f * this.mVelX;
				if (this.mPosX > (float)(800 - this.mWidth) && this.mCoinMotion != CoinMotion.COIN_MOTION_FROM_BOSS)
				{
					this.mPosX = (float)(800 - this.mWidth);
					this.mVelX = Constants.InvertAndScale(-0.4f - TodCommon.RandRangeFloat(0f, 0.4f));
				}
				else if (this.mPosX < 0f)
				{
					this.mPosX = 0f;
					this.mVelX = Constants.InvertAndScale(0.4f + TodCommon.RandRangeFloat(0f, 0.4f));
				}
			}
			else
			{
				if (this.mNeedsBouncyArrow && !this.mHasBouncyArrow)
				{
					float num = (float)(this.mWidth / 2);
					float num2 = (float)(this.mHeight / 2) - Constants.InvertAndScale(60f);
					if (this.mType == CoinType.COIN_TROPHY)
					{
						num += Constants.InvertAndScale(2f);
					}
					else if (this.mType == CoinType.COIN_AWARD_MONEY_BAG || this.mType == CoinType.COIN_AWARD_BAG_DIAMOND)
					{
						num += (float)Constants.Coin_MoneyBag_Offset.X;
						num2 += (float)Constants.Coin_MoneyBag_Offset.Y;
					}
					else if (this.mType == CoinType.COIN_AWARD_PRESENT || this.IsPresentWithAdvice())
					{
						num2 += Constants.InvertAndScale(-20f);
					}
					else if (this.IsMoney())
					{
						num += (float)Constants.Coin_Silver_Award_Offset.X;
						num2 += (float)Constants.Coin_Silver_Award_Offset.Y;
					}
					else if (this.mType == CoinType.COIN_NOTE)
					{
						num += (float)Constants.Coin_Note_Offset.X;
						num2 += (float)Constants.Coin_Note_Offset.Y;
					}
					else if (this.mType == CoinType.COIN_ALMANAC)
					{
						num += (float)Constants.Coin_Almanac_Offset.X;
						num2 += (float)Constants.Coin_Almanac_Offset.Y;
					}
					else if (this.mType == CoinType.COIN_SHOVEL)
					{
						num += (float)Constants.Coin_Shovel_Offset.X;
						num2 += (float)Constants.Coin_Shovel_Offset.Y;
					}
					else if (this.mType == CoinType.COIN_CARKEYS)
					{
						num += (float)Constants.Coin_CarKeys_Offset.X;
						num2 += (float)Constants.Coin_CarKeys_Offset.Y;
					}
					else if (this.mType == CoinType.COIN_TACO)
					{
						num += (float)Constants.Coin_Taco_Offset.X;
						num2 += (float)Constants.Coin_Taco_Offset.Y;
					}
					else if (this.mType == CoinType.COIN_BACON)
					{
						num += (float)Constants.Coin_Bacon_Offset.X;
						num2 += (float)Constants.Coin_Bacon_Offset.Y;
					}
					ParticleEffect theEffect;
					if (this.mType == CoinType.COIN_FINAL_SEED_PACKET)
					{
						theEffect = ParticleEffect.PARTICLE_SEED_PACKET;
					}
					else if (this.IsMoney())
					{
						theEffect = ParticleEffect.PARTICLE_COIN_PICKUP_ARROW;
					}
					else
					{
						theEffect = ParticleEffect.PARTICLE_AWARD_PICKUP_ARROW;
					}
					TodParticleSystem theParticleSystem = this.mApp.AddTodParticle(this.mPosX + num, this.mPosY + num2, 0, theEffect);
					GlobalMembersAttachment.AttachParticle(ref this.mAttachmentID, theParticleSystem, num, num2);
					this.mHasBouncyArrow = true;
				}
				if (!this.mHitGround)
				{
					this.mHitGround = true;
					this.PlayGroundSound();
				}
				this.mPosY = (float)this.mGroundY;
				this.mPosX = (float)TodCommon.FloatRoundToInt(this.mPosX);
				if ((this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_LAST_STAND || this.mBoard == null || this.mBoard.mChallenge.mChallengeState == ChallengeState.STATECHALLENGE_LAST_STAND_ONSLAUGHT) && !this.IsLevelAward() && !this.IsPresentWithAdvice())
				{
					this.mDisappearCounter += 3;
					int disappearTime = this.GetDisappearTime();
					if (this.mDisappearCounter >= disappearTime)
					{
						this.StartFade();
					}
				}
			}
			if (this.mCoinMotion == CoinMotion.COIN_MOTION_FROM_PLANT)
			{
				float sunScale = this.GetSunScale();
				if (this.mScale < sunScale)
				{
					this.mScale += 0.06f;
					return;
				}
				this.mScale = sunScale;
			}
		}

		public override bool SaveToFile(Sexy.Buffer b)
		{
			try
			{
				base.SaveToFile(b);
				b.WriteLong(this.mCoinAge);
				b.WriteLong((int)this.mCoinMotion);
				b.WriteFloat(this.mCollectionDistance);
				b.WriteFloat(this.mCollectX);
				b.WriteFloat(this.mCollectY);
				b.WriteBoolean(this.mDead);
				b.WriteLong(this.mDisappearCounter);
				b.WriteLong(this.mFadeCount);
				b.WriteLong(this.mGroundY);
				b.WriteBoolean(this.mHitGround);
				b.WriteBoolean(this.mIsBeingCollected);
				b.WriteBoolean(this.mNeedsBouncyArrow);
				b.WriteFloat(this.mPosX);
				b.WriteFloat(this.mPosY);
				b.WriteBoolean(this.mPottedPlantSpec != null);
				if (this.mPottedPlantSpec != null)
				{
					this.mPottedPlantSpec.Save(b);
				}
				b.WriteLong(this.mRow);
				b.WriteFloat(this.mScale);
				b.WriteLong(this.mTimesDropped);
				b.WriteLong((int)this.mType);
				b.WriteLong((int)this.mUsableSeedType);
				b.WriteFloat(this.mVelX);
				b.WriteFloat(this.mVelY);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return false;
			}
			return true;
		}

		public override bool LoadFromFile(Sexy.Buffer b)
		{
			try
			{
				base.LoadFromFile(b);
				this.mCoinAge = b.ReadLong();
				this.mCoinMotion = (CoinMotion)b.ReadLong();
				this.mCollectionDistance = b.ReadFloat();
				this.mCollectX = b.ReadFloat();
				this.mCollectY = b.ReadFloat();
				this.mDead = b.ReadBoolean();
				this.mDisappearCounter = b.ReadLong();
				this.mFadeCount = b.ReadLong();
				this.mGroundY = b.ReadLong();
				this.mHitGround = b.ReadBoolean();
				this.mIsBeingCollected = b.ReadBoolean();
				this.mNeedsBouncyArrow = b.ReadBoolean();
				this.mPosX = b.ReadFloat();
				this.mPosY = b.ReadFloat();
				bool flag = b.ReadBoolean();
				if (flag)
				{
					this.mPottedPlantSpec = new PottedPlant();
					this.mPottedPlantSpec.Load(b);
				}
				else
				{
					this.mPottedPlantSpec = null;
				}
				this.mRow = b.ReadLong();
				this.mScale = b.ReadFloat();
				this.mTimesDropped = b.ReadLong();
				this.mType = (CoinType)b.ReadLong();
				this.mUsableSeedType = (SeedType)b.ReadLong();
				this.mVelX = b.ReadFloat();
				this.mVelY = b.ReadFloat();
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return false;
			}
			return true;
		}

		public void ScoreCoin()
		{
			this.Die();
			if (this.IsSun())
			{
				int sunValue = this.GetSunValue();
				this.mBoard.AddSunMoney(sunValue);
			}
			else if (this.IsMoney())
			{
				int coinValue = Coin.GetCoinValue(this.mType);
				this.mApp.mPlayerInfo.AddCoins(coinValue);
				if (this.mBoard != null)
				{
					this.mBoard.mCoinsCollected += coinValue;
				}
			}
			if (this.mType == CoinType.COIN_DIAMOND && this.mBoard != null)
			{
				this.mBoard.mDiamondsCollected++;
			}
		}

		public void UpdateCollected()
		{
			int num;
			int num2;
			if (this.IsSun())
			{
				num = Constants.Board_SunCoin_CollectTarget.X;
				num2 = Constants.Board_SunCoin_CollectTarget.Y;
			}
			else if (this.IsMoney())
			{
				num = 130 - Constants.Board_Offset_AspectRatio_Correction;
				num2 = 550;
				if (this.mApp.GetDialog(4) != null)
				{
					num = 662;
					num2 = 546;
				}
				else if (this.mApp.mCrazyDaveState != CrazyDaveState.CRAZY_DAVE_OFF || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
				{
					num = Constants.ZenGarden_MoneyTarget_X;
				}
			}
			else if (this.IsPresentWithAdvice())
			{
				num = 35;
				num2 = 487;
			}
			else
			{
				if (this.mType == CoinType.COIN_AWARD_PRESENT || this.mType == CoinType.COIN_PRESENT_PLANT)
				{
					this.mDisappearCounter += 3;
					if (this.mDisappearCounter >= 200)
					{
						this.StartFade();
					}
					return;
				}
				if (!this.IsLevelAward())
				{
					if (this.mType == CoinType.COIN_USABLE_SEED_PACKET)
					{
						this.mDisappearCounter += 3;
					}
					return;
				}
				num = Constants.Coin_AwardSeedpacket_Pos.X - this.mWidth / 2;
				num2 = Constants.Coin_AwardSeedpacket_Pos.Y - this.mHeight / 2;
				this.mDisappearCounter += 3;
			}
			if (this.IsLevelAward())
			{
				this.mScale = TodCommon.TodAnimateCurveFloat(0, 400, this.mDisappearCounter, 1.01f, 2f, TodCurves.CURVE_EASE_IN_OUT);
				this.mPosX = TodCommon.TodAnimateCurveFloat(0, 350, this.mDisappearCounter, this.mCollectX, (float)num, TodCurves.CURVE_EASE_OUT);
				this.mPosY = TodCommon.TodAnimateCurveFloat(0, 350, this.mDisappearCounter, this.mCollectY, (float)num2, TodCurves.CURVE_EASE_OUT);
				return;
			}
			float num3 = Math.Abs(this.mPosX - (float)num);
			float num4 = Math.Abs(this.mPosY - (float)num2);
			if (this.mPosX > (float)num)
			{
				this.mPosX -= num3 / 7f;
			}
			else if (this.mPosX < (float)num)
			{
				this.mPosX += num3 / 7f;
			}
			if (this.mPosY > (float)num2)
			{
				this.mPosY -= num4 / 7f;
			}
			else if (this.mPosY < (float)num2)
			{
				this.mPosY += num4 / 7f;
			}
			this.mCollectionDistance = (float)Math.Sqrt((double)(num4 * num4 + num3 * num3));
			if (this.IsPresentWithAdvice())
			{
				if (this.mCollectionDistance < Constants.InvertAndScale(15f))
				{
					if (!this.mBoard.mHelpDisplayed[64])
					{
						if (this.mType == CoinType.COIN_PRESENT_MINIGAMES)
						{
							this.mBoard.DisplayAdvice("[UNLOCKED_MINIGAMES]", MessageStyle.MESSAGE_STYLE_HINT_TALL_UNLOCKMESSAGE, AdviceType.ADVICE_UNLOCKED_MODE);
							return;
						}
						if (this.mType == CoinType.COIN_PRESENT_PUZZLE_MODE)
						{
							this.mBoard.DisplayAdvice("[UNLOCKED_PUZZLE_MODE]", MessageStyle.MESSAGE_STYLE_HINT_TALL_UNLOCKMESSAGE, AdviceType.ADVICE_UNLOCKED_MODE);
							return;
						}
					}
					else if (this.mBoard.mHelpIndex != AdviceType.ADVICE_UNLOCKED_MODE || !this.mBoard.mAdvice.IsBeingDisplayed())
					{
						this.Die();
					}
				}
				return;
			}
			float num5 = 8f;
			if (this.IsMoney())
			{
				num5 = 12f;
			}
			if (this.mCollectionDistance < num5 && !this.mScored)
			{
				this.ScoreCoin();
				this.mScored = true;
			}
			this.mScale = TodCommon.ClampFloat(this.mCollectionDistance * 0.05f, 0.5f, 1f);
			this.mScale *= this.GetSunScale();
		}

		public SexyColor GetColor()
		{
			if ((this.IsSun() || this.IsMoney()) && this.mIsBeingCollected)
			{
				float num = TodCommon.ClampFloat(this.mCollectionDistance * 0.035f, 0.35f, 1f);
				return new SexyColor(255, 255, 255, (int)(255f * num), false);
			}
			if (this.mFadeCount > 0)
			{
				int theAlpha = TodCommon.TodAnimateCurve(15, 0, this.mFadeCount, 255, 0, TodCurves.CURVE_LINEAR);
				return new SexyColor(255, 255, 255, theAlpha, false);
			}
			return SexyColor.White;
		}

		public bool IsMoney()
		{
			return this.mType == CoinType.COIN_SILVER || this.mType == CoinType.COIN_GOLD || this.mType == CoinType.COIN_DIAMOND;
		}

		public bool IsSun()
		{
			return this.mType == CoinType.COIN_SUN || this.mType == CoinType.COIN_SMALLSUN || this.mType == CoinType.COIN_LARGESUN;
		}

		public float GetSunScale()
		{
			if (this.mType == CoinType.COIN_SMALLSUN)
			{
				return 0.5f;
			}
			if (this.mType == CoinType.COIN_LARGESUN)
			{
				return 2f;
			}
			return 1f;
		}

		public SeedType GetFinalSeedPacketType()
		{
			if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard != null && this.mBoard.mLevel <= 50)
			{
				return this.mApp.GetAwardSeedForLevel(this.mBoard.mLevel);
			}
			return SeedType.SEED_NONE;
		}

		public bool IsLevelAward()
		{
			return this.mType == CoinType.COIN_FINAL_SEED_PACKET || this.mType == CoinType.COIN_TROPHY || this.mType == CoinType.COIN_SHOVEL || this.mType == CoinType.COIN_CARKEYS || this.mType == CoinType.COIN_ALMANAC || this.mType == CoinType.COIN_TACO || this.mType == CoinType.COIN_BACON || this.mType == CoinType.COIN_NOTE || this.mType == CoinType.COIN_AWARD_MONEY_BAG || this.mType == CoinType.COIN_AWARD_BAG_DIAMOND || this.mType == CoinType.COIN_AWARD_PRESENT || this.mType == CoinType.COIN_WATERING_CAN || this.mType == CoinType.COIN_AWARD_CHOCOLATE;
		}

		public bool CoinGetsBouncyArrow()
		{
			return this.IsLevelAward() || ((this.mType == CoinType.COIN_SILVER || this.mType == CoinType.COIN_GOLD) && this.mApp.IsFirstTimeAdventureMode() && this.mBoard != null && this.mBoard.mLevel == 11 && !this.mBoard.mDroppedFirstCoin) || this.IsPresentWithAdvice();
		}

		public void FanOutCoins(CoinType theCoinType, int theNumCoins)
		{
			Debug.ASSERT(this.mBoard != null);
			for (int i = 0; i < theNumCoins; i++)
			{
				float num = 1.5707964f + 3.1415927f * (float)(i + 1) / (float)(theNumCoins + 1);
				float num2 = this.mPosX + 20f;
				float num3 = this.mPosY;
				Coin coin = this.mBoard.AddCoin((int)num2, (int)num3, theCoinType, CoinMotion.COIN_MOTION_FROM_PRESENT);
				coin.mVelX = 5f * (float)Math.Sin((double)num);
				coin.mVelY = 5f * (float)Math.Cos((double)num);
			}
		}

		public int GetDisappearTime()
		{
			int result = 750;
			if (this.mType == CoinType.COIN_DIAMOND || this.mType == CoinType.COIN_CHOCOLATE || this.mHasBouncyArrow || this.mType == CoinType.COIN_PRESENT_PLANT)
			{
				result = 1500;
			}
			if ((this.mApp.IsScaryPotterLevel() || this.mApp.IsSlotMachineLevel()) && this.mType == CoinType.COIN_USABLE_SEED_PACKET)
			{
				result = 1500;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				result = 6000;
			}
			return result;
		}

		public void DroppedUsableSeed()
		{
			this.mIsBeingCollected = false;
			if (this.mTimesDropped == 0)
			{
				this.mDisappearCounter = Math.Min(this.mDisappearCounter, 1200);
			}
			this.mTimesDropped++;
		}

		public void PlayCollectSound()
		{
			if (this.mType == CoinType.COIN_USABLE_SEED_PACKET)
			{
				this.mApp.PlaySample(Resources.SOUND_SEEDLIFT);
				return;
			}
			if (this.mType == CoinType.COIN_SILVER || this.mType == CoinType.COIN_GOLD)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_COIN);
				return;
			}
			if (this.mType == CoinType.COIN_DIAMOND)
			{
				this.mApp.PlaySample(Resources.SOUND_DIAMOND);
				return;
			}
			if (this.IsSun())
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_SUN);
				return;
			}
			if (this.mType == CoinType.COIN_CHOCOLATE || this.IsPresentWithAdvice() || this.mType == CoinType.COIN_AWARD_PRESENT || this.mType == CoinType.COIN_AWARD_CHOCOLATE || this.mType == CoinType.COIN_PRESENT_PLANT)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_PRIZE);
				return;
			}
			if (this.IsSun())
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_SUN);
			}
		}

		public void TryAutoCollectAfterLevelAward()
		{
			bool flag = false;
			if (this.IsMoney() && this.mCoinMotion != CoinMotion.COIN_MOTION_FROM_PRESENT)
			{
				flag = true;
			}
			else if (this.IsSun())
			{
				flag = true;
			}
			else if (this.mType == CoinType.COIN_CHOCOLATE || this.IsPresentWithAdvice() || this.mType == CoinType.COIN_PRESENT_PLANT)
			{
				flag = true;
			}
			if (flag)
			{
				this.PlayCollectSound();
				this.Collect();
			}
		}

		public bool IsPresentWithAdvice()
		{
			return this.mType == CoinType.COIN_PRESENT_MINIGAMES || this.mType == CoinType.COIN_PRESENT_PUZZLE_MODE;
		}

		public void PlayGroundSound()
		{
			if (this.mType == CoinType.COIN_GOLD)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_MONEYFALLS);
				return;
			}
			if (this.mType != CoinType.COIN_PRESENT_PLANT && this.mType != CoinType.COIN_DIAMOND && this.mType != CoinType.COIN_CHOCOLATE && this.mType != CoinType.COIN_AWARD_CHOCOLATE && this.mType != CoinType.COIN_AWARD_PRESENT)
			{
				if (this.IsPresentWithAdvice())
				{
					return;
				}
				this.IsLevelAward();
			}
		}

		public void PlayLaunchSound()
		{
			if (this.mType == CoinType.COIN_PRESENT_PLANT || this.mType == CoinType.COIN_DIAMOND || this.mType == CoinType.COIN_CHOCOLATE || this.mType == CoinType.COIN_AWARD_CHOCOLATE || this.mType == CoinType.COIN_AWARD_PRESENT || this.IsPresentWithAdvice())
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_CHIME);
			}
		}

		public void Loaded()
		{
			if (this.mType == CoinType.COIN_FINAL_SEED_PACKET)
			{
				Coin.LoadSeedPacketImage(this.GetFinalSeedPacketType());
			}
		}

		public override void LoadingComplete()
		{
			base.LoadingComplete();
			this.Loaded();
			Coin coin = new Coin();
			coin.CoinInitialize(this.mX, this.mY, this.mType, this.mCoinMotion);
			this.mAttachmentID = coin.mAttachmentID;
		}

		public static SeedType LoadedSeedType;

		public float mPosX;

		public float mPosY;

		public float mVelX;

		public float mVelY;

		public float mScale;

		public bool mDead;

		public int mFadeCount;

		public float mCollectX;

		public float mCollectY;

		public int mGroundY;

		public int mCoinAge;

		public bool mIsBeingCollected;

		public int mDisappearCounter;

		public CoinType mType;

		public CoinMotion mCoinMotion;

		private Attachment mAttachmentID;

		public float mCollectionDistance;

		public SeedType mUsableSeedType;

		public PottedPlant mPottedPlantSpec = new PottedPlant();

		public bool mNeedsBouncyArrow;

		public bool mHasBouncyArrow;

		public bool mHitGround;

		public int mTimesDropped;

		public bool mScored;
	}
}
