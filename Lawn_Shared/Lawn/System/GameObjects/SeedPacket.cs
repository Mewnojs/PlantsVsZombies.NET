using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class SeedPacket : GameObject
	{
		public SeedPacket()
		{
			this.mPacketType = SeedType.SEED_NONE;
			this.mImitaterType = SeedType.SEED_NONE;
			this.mIndex = -1;
			this.mWidth = Constants.SMALL_SEEDPACKET_WIDTH;
			this.mHeight = Constants.SMALL_SEEDPACKET_HEIGHT;
			this.mRefreshCounter = 0;
			this.mRefreshTime = 0;
			this.mRefreshing = false;
			this.mActive = true;
			this.mOffsetY = 0;
			this.mSlotMachineCountDown = 0;
			this.mSlotMachiningNextSeed = SeedType.SEED_NONE;
			this.mSlotMachiningPosition = 0f;
			this.mTimesUsed = 0;
			this.mPosScaled = false;
		}

		public override bool SaveToFile(Sexy.Buffer b)
		{
			try
			{
				base.SaveToFile(b);
				b.WriteBoolean(this.mActive);
				b.WriteLong((int)this.mImitaterType);
				b.WriteLong(this.mIndex);
				b.WriteLong(this.mOffsetY);
				b.WriteLong((int)this.mPacketType);
				b.WriteLong(this.mRefreshCounter);
				b.WriteBoolean(this.mRefreshing);
				b.WriteLong(this.mRefreshTime);
				b.WriteLong(this.mSlotMachineCountDown);
				b.WriteLong((int)this.mSlotMachiningNextSeed);
				b.WriteFloat(this.mSlotMachiningPosition);
				b.WriteLong(this.mTimesUsed);
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
				this.mActive = b.ReadBoolean();
				this.mImitaterType = (SeedType)b.ReadLong();
				this.mIndex = b.ReadLong();
				this.mOffsetY = b.ReadLong();
				this.mPacketType = (SeedType)b.ReadLong();
				this.mRefreshCounter = b.ReadLong();
				this.mRefreshing = b.ReadBoolean();
				this.mRefreshTime = b.ReadLong();
				this.mSlotMachineCountDown = b.ReadLong();
				this.mSlotMachiningNextSeed = (SeedType)b.ReadLong();
				this.mSlotMachiningPosition = b.ReadFloat();
				this.mTimesUsed = b.ReadLong();
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				return false;
			}
			return true;
		}

		public void Update()
		{
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				return;
			}
			if (this.mPacketType == SeedType.SEED_NONE)
			{
				return;
			}
			if (this.mBoard.mMainCounter == 0)
			{
				this.FlashIfReady();
			}
			if (!this.mActive && this.mRefreshing)
			{
				this.mRefreshCounter++;
				if (this.mRefreshCounter > this.mRefreshTime)
				{
					this.mRefreshCounter = 0;
					this.mRefreshing = false;
					this.Activate();
					this.FlashIfReady();
				}
			}
			if (this.mSlotMachineCountDown > 0)
			{
				this.mSlotMachineCountDown--;
				float num = TodCommon.TodAnimateCurveFloat(400, 0, this.mSlotMachineCountDown, 6f, 2f, TodCurves.CURVE_LINEAR);
				this.mSlotMachiningPosition += num * 0.01f;
				if (this.mSlotMachiningPosition >= 1f)
				{
					this.mPacketType = this.mSlotMachiningNextSeed;
					if (this.mSlotMachineCountDown >= 0 && this.mSlotMachineCountDown < 3)
					{
						this.mSlotMachiningPosition = 0f;
						this.Activate();
						return;
					}
					this.mSlotMachiningPosition -= 1f;
					this.PickNextSlotMachineSeed();
					return;
				}
				else if (this.mSlotMachineCountDown == 0)
				{
					this.mSlotMachineCountDown = 3;
				}
			}
		}

		public void DrawBackground(Graphics g)
		{
			float num = 0f;
			int theGrayness = 255;
			if (this.mBoard.mCursorObject.mCursorType != CursorType.CURSOR_TYPE_PLANT_FROM_BANK || this.mBoard.mCursorObject.mSeedBankIndex != this.mIndex)
			{
				this.GetGraynessAndDarkness(ref theGrayness, ref num);
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				TRect trect = this.mApp.mBoard.mChallenge.SlotMachineRect();
				int num2 = trect.mY + Constants.Challenge_SlotMachine_Y_Offset;
				float num3 = 0.9f;
				int challenge_SlotMachine_ClipHeight = Constants.Challenge_SlotMachine_ClipHeight;
				float num4 = (float)AtlasResources.IMAGE_SEEDPACKETS.GetCelWidth() * num3;
				int num5 = num2 - Constants.Challenge_SlotMachine_Y_Pos + TodCommon.FloatRoundToInt(this.mSlotMachiningPosition * (float)(-(float)challenge_SlotMachine_ClipHeight));
				Graphics @new = Graphics.GetNew(g);
				@new.mTransY = 0;
				@new.mTransX = this.mBoard.mX;
				int challenge_SlotMachine_Gap = Constants.Challenge_SlotMachine_Gap;
				int num6 = trect.mX + Constants.Challenge_SlotMachine_Offset + this.mIndex * TodCommon.FloatRoundToInt((float)challenge_SlotMachine_Gap + num4);
				int challenge_SlotMachine_Shadow_Offset = Constants.Challenge_SlotMachine_Shadow_Offset;
				@new.ClipRect(num6, num2, (int)num4, challenge_SlotMachine_ClipHeight);
				@new.HardwareClip();
				if (this.mSlotMachineCountDown > 0)
				{
					this.DrawSeedPacketSlotMachine(@new, (float)num6, (float)num5, this.mPacketType, SeedType.SEED_NONE, theGrayness, num3);
					this.DrawSeedPacketSlotMachine(@new, (float)num6, (float)num5 + (float)AtlasResources.IMAGE_SEEDPACKETS.GetCelHeight() * num3 - (float)challenge_SlotMachine_Shadow_Offset, this.mSlotMachiningNextSeed, SeedType.SEED_NONE, theGrayness, num3);
				}
				else
				{
					this.DrawSeedPacketSlotMachine(@new, (float)num6, (float)num5, this.mPacketType, SeedType.SEED_NONE, theGrayness, num3);
				}
				@new.EndHardwareClip();
				@new.PrepareForReuse();
				return;
			}
			bool theDrawCostBackground = !this.mBoard.HasConveyorBeltSeedBank() && !this.mApp.IsSlotMachineLevel();
			SeedPacket.DrawSmallSeedPacket(g, 0f, (float)this.mOffsetY, this.mPacketType, this.mImitaterType, 0f, theGrayness, false, true, true, theDrawCostBackground);
		}

		public void Draw(Graphics g)
		{
			if (this.mBoard.HasConveyorBeltSeedBank() || this.mApp.IsSlotMachineLevel())
			{
				return;
			}
			SeedPacket.DrawSmallSeedPacket(g, 0f, (float)this.mOffsetY, this.mPacketType, this.mImitaterType, 0f, 255, true, true, false, false);
		}

		public void DrawOverlay(Graphics g)
		{
			float thePercentDark = 0f;
			int theGrayness = 255;
			bool flag = this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_BANK && this.mBoard.mCursorObject.mSeedBankIndex == this.mIndex;
			if (!flag)
			{
				this.GetGraynessAndDarkness(ref theGrayness, ref thePercentDark);
			}
			if (this.mSlotMachineCountDown > 0)
			{
				int num = TodCommon.FloatRoundToInt(this.mSlotMachiningPosition * (float)(-(float)this.mHeight));
				Graphics @new = Graphics.GetNew(g);
				@new.ClipRect(0, 0, this.mWidth, this.mHeight);
				SeedPacket.DrawSmallSeedPacket(@new, 0f, (float)num, this.mPacketType, SeedType.SEED_NONE, 0f, 128, false, false, false, false);
				SeedPacket.DrawSmallSeedPacket(@new, 0f, (float)(num + this.mHeight), this.mSlotMachiningNextSeed, SeedType.SEED_NONE, 0f, 128, false, false, false, false);
				@new.PrepareForReuse();
				return;
			}
			SeedPacket.DrawSmallSeedPacket(g, 0f, (float)this.mOffsetY, this.mPacketType, this.mImitaterType, thePercentDark, theGrayness, false, true, false, false);
			if (flag)
			{
				g.DrawImage(AtlasResources.IMAGE_SELECTED_PACKET, Constants.SeedPacket_Selector_Pos.X, this.mOffsetY - Constants.SeedPacket_Selector_Pos.Y);
			}
		}

		internal static void DrawSeedType(Graphics g, float x, float y, SeedType theSeedType, SeedType theImitaterType)
		{
			Plant.DrawSeedType(g, theSeedType, theImitaterType, DrawVariation.VARIATION_NORMAL, x, y);
		}

		public static void DrawSmallSeedPacket(Graphics g, float x, float y, SeedType theSeedType, SeedType theImitaterType, float thePercentDark, int theGrayness, bool theDrawCost, bool theUseCurrentCost, bool theDrawBackground, bool theDrawCostBackground)
		{
			SeedType seedType = theSeedType;
			if (theSeedType == SeedType.SEED_IMITATER && theImitaterType != SeedType.SEED_NONE)
			{
				seedType = theImitaterType;
			}
			if (seedType == SeedType.SEED_LEFTPEATER)
			{
				seedType = SeedType.SEED_SPROUT;
			}
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.SetColor(new SexyColor(255, 255, 255, 255));
			if (theGrayness != 255)
			{
				g.SetColor(new SexyColor(theGrayness, theGrayness, theGrayness));
				g.SetColorizeImages(true);
			}
			else if (thePercentDark > 0f)
			{
				g.SetColor(new SexyColor(128, 128, 128, 255));
				g.SetColorizeImages(true);
			}
			if (theDrawBackground)
			{
				if (theSeedType == SeedType.SEED_SLOT_MACHINE_SUN)
				{
					g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_SUN, x, y);
				}
				else if (theSeedType == SeedType.SEED_SLOT_MACHINE_DIAMOND)
				{
					g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_DIAMOND, x, y);
				}
				else if (theSeedType == SeedType.SEED_ZOMBIQUARIUM_SNORKEL)
				{
					g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_ZOMBIEQUARIUM, x, y);
				}
				else if (theSeedType == SeedType.SEED_ZOMBIQUARIUM_TROPHY)
				{
					g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_TROPHY, x, y);
				}
				else if (theSeedType == SeedType.SEED_BEGHOULED_BUTTON_SHUFFLE)
				{
					g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_SHUFFLE, x, y);
				}
				else if (theSeedType == SeedType.SEED_BEGHOULED_BUTTON_CRATER)
				{
					g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_CRATER, x, y);
				}
				else if (GlobalStaticVars.gLawnApp.IsIZombieLevel())
				{
					SeedPacket.DrawIZombieSeedPacket(GlobalStaticVars.gLawnApp, g, x, y, seedType, thePercentDark, theGrayness, theDrawCost, theUseCurrentCost, theDrawBackground, theDrawCostBackground);
				}
				else
				{
					g.DrawImageCel(AtlasResources.IMAGE_SEEDPACKETS, (int)x, (int)y, (int)seedType);
					if (theSeedType == SeedType.SEED_IMITATER)
					{
						g.SetColor(new SexyColor(0, 255, 0, 128));
						g.FillRect((int)x, (int)y, Constants.SMALL_SEEDPACKET_WIDTH, Constants.SMALL_SEEDPACKET_HEIGHT);
					}
				}
			}
			if (theDrawCostBackground)
			{
				Image theImage;
				if (theSeedType == SeedType.SEED_IMITATER)
				{
					theImage = AtlasResources.IMAGE_SEEDPACKETS_GRAY_TAB;
				}
				else if (theSeedType < SeedType.SEED_GATLINGPEA)
				{
					theImage = AtlasResources.IMAGE_SEEDPACKETS_GREEN_TAB;
				}
				else
				{
					theImage = AtlasResources.IMAGE_SEEDPACKETS_PURPLE_TAB;
				}
				int x2;
				int y2;
				if (GlobalStaticVars.gLawnApp.IsIZombieLevel())
				{
					x2 = Constants.SeedPacket_Cost_IZombie.X;
					y2 = Constants.SeedPacket_Cost_IZombie.Y;
				}
				else
				{
					x2 = Constants.SeedPacket_Cost.X;
					y2 = Constants.SeedPacket_Cost.Y;
				}
				g.DrawImageF(theImage, x + (float)x2, y + (float)y2);
			}
			if (thePercentDark > 0f)
			{
				int theHeight = TodCommon.FloatRoundToInt((float)Constants.SMALL_SEEDPACKET_HEIGHT * thePercentDark) + 2;
				g.SetColor(new SexyColor(0, 0, 0, 150));
				g.FillRect((int)x, (int)y, Constants.SMALL_SEEDPACKET_WIDTH, theHeight);
			}
			if (theDrawCost)
			{
				string theText = string.Empty;
				if (((LawnApp)GlobalStaticVars.gSexyAppBase).mBoard != null && ((LawnApp)GlobalStaticVars.gSexyAppBase).mBoard.PlantUsesAcceleratedPricing(seedType))
				{
					if (theUseCurrentCost)
					{
						theText = ((LawnApp)GlobalStaticVars.gSexyAppBase).mBoard.GetCurrentPlantCost(theSeedType, theImitaterType).ToString();
					}
					else
					{
						int cost = Plant.GetCost(theSeedType, theImitaterType);
						theText = Common.StrFormat_(TodStringFile.TodStringTranslate("[SEED_PACKET_COST_PLUS]"), cost);
					}
				}
				else
				{
					int cost2 = Plant.GetCost(theSeedType, theImitaterType);
					theText = LawnApp.ToString(cost2);
				}
				int x3;
				int y3;
				if (GlobalStaticVars.gLawnApp.IsIZombieLevel())
				{
					x3 = Constants.SeedPacket_CostText_IZombie_Pos.X;
					y3 = Constants.SeedPacket_CostText_IZombie_Pos.Y;
				}
				else
				{
					x3 = Constants.SeedPacket_CostText_Pos.X;
					y3 = Constants.SeedPacket_CostText_Pos.Y;
				}
				TodCommon.TodDrawString(g, theText, (int)x + x3, (int)y + y3, Resources.FONT_PICO129, SexyColor.Black, DrawStringJustification.DS_ALIGN_RIGHT);
			}
			g.SetColorizeImages(false);
		}

		private static void DrawIZombieSeedPacket(LawnApp theApp, Graphics g, float x, float y, SeedType aSeedType, float thePercentDark, int theGrayness, bool theDrawCost, bool theUseCurrentCost, bool theDrawBackground, bool theDrawCostBackground)
		{
			ZombieType izombieTypeFromSeed = SeedPacket.GetIZombieTypeFromSeed(aSeedType);
			if (izombieTypeFromSeed == ZombieType.ZOMBIE_INVALID)
			{
				return;
			}
			g.SetScale(0.75f, 0.5f, 0f, 0f);
			g.DrawImage(AtlasResources.IMAGE_ALMANAC_ZOMBIEWINDOW, x + (float)Constants.IZombie_SeedOffset.X, y + (float)Constants.IZombie_SeedOffset.Y);
			Graphics @new = Graphics.GetNew(g);
			@new.SetScale(1f);
			@new.ClipRect((int)x + Constants.IZombie_ClipOffset.X, (int)y + Constants.IZombie_ClipOffset.Y, Constants.IZombie_ClipOffset.Width, Constants.IZombie_ClipOffset.Height);
			@new.HardwareClip();
			theApp.mReanimatorCache.DrawCachedZombie(@new, x + (float)Constants.ZombieOffsets[(int)izombieTypeFromSeed].X * g.mScaleX, y + (float)Constants.ZombieOffsets[(int)izombieTypeFromSeed].Y * g.mScaleY, izombieTypeFromSeed);
			@new.SetColorizeImages(false);
			@new.EndHardwareClip();
			@new.PrepareForReuse();
			g.DrawImage(AtlasResources.IMAGE_ALMANAC_ZOMBIEWINDOW2, x, y);
			g.SetScale(1f, 1f, 0f, 0f);
		}

		private static ZombieType GetIZombieTypeFromSeed(SeedType theSeedType)
		{
			switch (theSeedType)
			{
			case SeedType.SEED_ZOMBIE_NORMAL:
				return ZombieType.ZOMBIE_NORMAL;
			case SeedType.SEED_ZOMBIE_TRAFFIC_CONE:
				return ZombieType.ZOMBIE_TRAFFIC_CONE;
			case SeedType.SEED_ZOMBIE_POLEVAULTER:
				return ZombieType.ZOMBIE_POLEVAULTER;
			case SeedType.SEED_ZOMBIE_PAIL:
				return ZombieType.ZOMBIE_PAIL;
			case SeedType.SEED_ZOMBIE_LADDER:
				return ZombieType.ZOMBIE_LADDER;
			case SeedType.SEED_ZOMBIE_DIGGER:
				return ZombieType.ZOMBIE_DIGGER;
			case SeedType.SEED_ZOMBIE_BUNGEE:
				return ZombieType.ZOMBIE_BUNGEE;
			case SeedType.SEED_ZOMBIE_FOOTBALL:
				return ZombieType.ZOMBIE_FOOTBALL;
			case SeedType.SEED_ZOMBIE_BALLOON:
				return ZombieType.ZOMBIE_BALLOON;
			case SeedType.SEED_ZOMBIE_SCREEN_DOOR:
				return ZombieType.ZOMBIE_DOOR;
			case SeedType.SEED_ZOMBONI:
				return ZombieType.ZOMBIE_ZAMBONI;
			case SeedType.SEED_ZOMBIE_POGO:
				return ZombieType.ZOMBIE_POGO;
			case SeedType.SEED_ZOMBIE_DANCER:
				return ZombieType.ZOMBIE_DANCER;
			case SeedType.SEED_ZOMBIE_GARGANTUAR:
				return ZombieType.ZOMBIE_GARGANTUAR;
			case SeedType.SEED_ZOMBIE_IMP:
				return ZombieType.ZOMBIE_IMP;
			default:
				return ZombieType.ZOMBIE_INVALID;
			}
		}

		private void DrawSeedPacketSlotMachine(Graphics g, float x, float y, SeedType theSeedType, SeedType theImitaterType, int theGrayness, float scale)
		{
			SeedType seedType = theSeedType;
			if (theSeedType == SeedType.SEED_IMITATER && theImitaterType != SeedType.SEED_NONE)
			{
				seedType = theImitaterType;
			}
			if (seedType == SeedType.SEED_LEFTPEATER)
			{
				seedType = SeedType.SEED_SPROUT;
			}
			if (theGrayness != 255)
			{
				g.SetColor(new SexyColor(theGrayness, theGrayness, theGrayness));
				g.SetColorizeImages(true);
			}
			if (theSeedType == SeedType.SEED_SLOT_MACHINE_SUN)
			{
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_SEEDPACKET_SUN, x, y, scale, scale);
			}
			else if (theSeedType == SeedType.SEED_SLOT_MACHINE_DIAMOND)
			{
				TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_SEEDPACKET_DIAMOND, x, y, scale, scale);
			}
			else
			{
				int theCelCol = (int)seedType % AtlasResources.IMAGE_SEEDPACKETS.mNumCols;
				int theCelRow = (int)seedType / AtlasResources.IMAGE_SEEDPACKETS.mNumCols;
				TodCommon.TodDrawImageCelScaledF(g, AtlasResources.IMAGE_SEEDPACKETS, x, y, theCelCol, theCelRow, scale, scale);
			}
			g.SetColorizeImages(false);
		}

		public void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mBoard.mPaused || this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				return;
			}
			if (this.mPacketType == SeedType.SEED_NONE)
			{
				return;
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				if (!this.mBoard.mAdvice.IsBeingDisplayed())
				{
					this.mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_PULL]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_NONE);
				}
				this.mBoard.mChallenge.mSlotMachineRollCount = Math.Min(this.mBoard.mChallenge.mSlotMachineRollCount, 2);
				return;
			}
			SeedType seedType = this.mPacketType;
			if (seedType == SeedType.SEED_IMITATER && this.mImitaterType != SeedType.SEED_NONE)
			{
				seedType = this.mImitaterType;
			}
			if (!this.mApp.mEasyPlantingCheat)
			{
				if (!this.mActive)
				{
					this.mApp.PlaySample(Resources.SOUND_BUZZER);
					if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 1 && this.mBoard.mHelpDisplayed[0])
					{
						this.mBoard.DisplayAdvice("[ADVICE_SEED_REFRESH]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1, AdviceType.ADVICE_SEED_REFRESH);
					}
					return;
				}
				int currentPlantCost = this.mBoard.GetCurrentPlantCost(this.mPacketType, this.mImitaterType);
				if (!this.mBoard.CanTakeSunMoney(currentPlantCost) && !this.mBoard.HasConveyorBeltSeedBank())
				{
					this.mApp.PlaySample(Resources.SOUND_BUZZER);
					this.mBoard.mOutOfMoneyCounter = 70;
					if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 1 && this.mBoard.mHelpDisplayed[0])
					{
						this.mBoard.DisplayAdvice("[ADVICE_CANT_AFFORD_PLANT]", MessageStyle.MESSAGE_STYLE_TUTORIAL_LEVEL1, AdviceType.ADVICE_CANT_AFFORD_PLANT);
					}
					return;
				}
				if (!this.mBoard.PlantingRequirementsMet(seedType))
				{
					this.mApp.PlaySample(Resources.SOUND_BUZZER);
					if (seedType == SeedType.SEED_GATLINGPEA)
					{
						this.mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_REPEATER]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_PLANT_NEEDS_REPEATER);
						return;
					}
					if (seedType == SeedType.SEED_WINTERMELON)
					{
						this.mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_MELONPULT]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_PLANT_NEEDS_MELONPULT);
						return;
					}
					if (seedType == SeedType.SEED_TWINSUNFLOWER)
					{
						this.mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_SUNFLOWER]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_PLANT_NEEDS_SUNFLOWER);
						return;
					}
					if (seedType == SeedType.SEED_SPIKEROCK)
					{
						this.mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_SPIKEWEED]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_PLANT_NEEDS_SPIKEWEED);
						return;
					}
					if (seedType == SeedType.SEED_COBCANNON)
					{
						this.mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_KERNELPULT]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_PLANT_NEEDS_KERNELPULT);
						return;
					}
					if (seedType == SeedType.SEED_GOLD_MAGNET)
					{
						this.mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_MAGNETSHROOM]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_PLANT_NEEDS_MAGNETSHROOM);
						return;
					}
					if (seedType == SeedType.SEED_GLOOMSHROOM)
					{
						this.mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_FUMESHROOM]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_PLANT_NEEDS_FUMESHROOM);
						return;
					}
					if (seedType == SeedType.SEED_CATTAIL)
					{
						this.mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_LILYPAD]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_PLANT_NEEDS_LILYPAD);
						return;
					}
					Debug.ASSERT(false);
					return;
				}
			}
			this.mBoard.ClearAdvice(AdviceType.ADVICE_CANT_AFFORD_PLANT);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_PLANT_NEEDS_REPEATER);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_PLANT_NEEDS_MELONPULT);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_PLANT_NEEDS_SUNFLOWER);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_PLANT_NEEDS_KERNELPULT);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_PLANT_NEEDS_SPIKEWEED);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_PLANT_NEEDS_MAGNETSHROOM);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_PLANT_NEEDS_FUMESHROOM);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_PLANT_NEEDS_LILYPAD);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				this.mBoard.mChallenge.BeghouledPacketClicked(this);
				return;
			}
			this.mBoard.mCursorObject.mType = this.mPacketType;
			this.mBoard.mCursorObject.mImitaterType = this.mImitaterType;
			this.mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_PLANT_FROM_BANK;
			this.mBoard.mCursorObject.mSeedBankIndex = this.mIndex;
			this.mApp.PlaySample(Resources.SOUND_SEEDLIFT);
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PICK_UP_PEASHOOTER)
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_LEVEL_1_PLANT_PEASHOOTER);
			}
			else if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_LEVEL_2_PICK_UP_SUNFLOWER)
			{
				if (this.mPacketType == SeedType.SEED_SUNFLOWER)
				{
					this.mBoard.SetTutorialState(TutorialState.TUTORIAL_LEVEL_2_PLANT_SUNFLOWER);
				}
				else
				{
					this.mBoard.SetTutorialState(TutorialState.TUTORIAL_LEVEL_2_REFRESH_SUNFLOWER);
				}
			}
			else if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_MORESUN_PICK_UP_SUNFLOWER)
			{
				if (this.mPacketType == SeedType.SEED_SUNFLOWER)
				{
					this.mBoard.SetTutorialState(TutorialState.TUTORIAL_MORESUN_PLANT_SUNFLOWER);
				}
				else
				{
					this.mBoard.SetTutorialState(TutorialState.TUTORIAL_MORESUN_REFRESH_SUNFLOWER);
				}
			}
			else if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_WHACK_A_ZOMBIE_PICK_SEED || this.mBoard.mTutorialState == TutorialState.TUTORIAL_WHACK_A_ZOMBIE_BEFORE_PICK_SEED)
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_WHACK_A_ZOMBIE_COMPLETED);
			}
			this.Deactivate();
		}

		public bool MouseHitTest(int theX, int theY, HitResult theHitResult)
		{
			if (this.mSlotMachineCountDown > 0 || this.mPacketType == SeedType.SEED_NONE)
			{
				theHitResult.mObject = null;
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_NONE;
				return false;
			}
			if (theX >= this.mX && theX < this.mX + this.mWidth && theY >= this.mY + this.mOffsetY && theY < this.mY + this.mHeight + this.mOffsetY)
			{
				theHitResult.mObject = this;
				theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_SEEDPACKET;
				return true;
			}
			theHitResult.mObject = null;
			theHitResult.mObjectType = GameObjectType.OBJECT_TYPE_NONE;
			return false;
		}

		public void Deactivate()
		{
			this.mActive = false;
			this.mRefreshCounter = 0;
			this.mRefreshTime = 0;
			this.mRefreshing = false;
		}

		public void Activate()
		{
			Debug.ASSERT(this.mPacketType != SeedType.SEED_NONE);
			this.mActive = true;
		}

		public void PickNextSlotMachineSeed()
		{
			int theTimeAge = this.mBoard.CountPlantByType(SeedType.SEED_PEASHOOTER);
			for (int i = 0; i < this.aSeedWeightArray.Length; i++)
			{
				if (this.aSeedWeightArray[i] != null)
				{
					this.aSeedWeightArray[i].PrepareForReuse();
				}
				this.aSeedWeightArray[i] = TodWeightedArray.GetNewTodWeightedArray();
			}
			int num = 0;
			for (int j = 0; j < 6; j++)
			{
				SeedType seedType = this.SLOT_SEED_TYPES[j];
				int num2 = 100;
				if (seedType == SeedType.SEED_PEASHOOTER)
				{
					num2 = TodCommon.TodAnimateCurve(0, 5, theTimeAge, 200, 100, TodCurves.CURVE_LINEAR);
				}
				if (seedType == SeedType.SEED_SLOT_MACHINE_DIAMOND)
				{
					num2 = 30;
				}
				if (this.mIndex == 2 && seedType != SeedType.SEED_SLOT_MACHINE_DIAMOND && (seedType == this.mBoard.mSeedBank.mSeedPackets[0].mSlotMachiningNextSeed || seedType == this.mBoard.mSeedBank.mSeedPackets[1].mSlotMachiningNextSeed))
				{
					num2 += num2 / 2;
				}
				this.aSeedWeightArray[num].mItem = (int)seedType;
				this.aSeedWeightArray[num].mWeight = num2;
				num++;
			}
			this.mSlotMachiningNextSeed = (SeedType)TodCommon.TodPickFromWeightedArray(this.aSeedWeightArray, num);
		}

		public void SlotMachineStart()
		{
			this.mSlotMachineCountDown = 400;
			this.mSlotMachiningPosition = 0f;
			this.PickNextSlotMachineSeed();
		}

		public void WasPlanted()
		{
			Debug.ASSERT(this.mPacketType != SeedType.SEED_NONE);
			if (this.mBoard.HasConveyorBeltSeedBank())
			{
				this.mBoard.mSeedBank.RemoveSeed(this.mIndex);
				return;
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				this.Deactivate();
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && this.mBoard.mChallenge.mChallengeState != ChallengeState.STATECHALLENGE_LAST_STAND_ONSLAUGHT)
			{
				this.mTimesUsed++;
				this.mActive = true;
				this.FlashIfReady();
				return;
			}
			this.mTimesUsed++;
			this.mRefreshing = true;
			this.mRefreshTime = Plant.GetRefreshTime(this.mPacketType, this.mImitaterType);
		}

		public void FlashIfReady()
		{
			if (!this.CanPickUp())
			{
				return;
			}
			if (this.mApp.mEasyPlantingCheat)
			{
				return;
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_REFRESH_PEASHOOTER)
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_LEVEL_1_PICK_UP_PEASHOOTER);
				return;
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_LEVEL_2_REFRESH_SUNFLOWER && this.mPacketType == SeedType.SEED_SUNFLOWER)
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_LEVEL_2_PICK_UP_SUNFLOWER);
				return;
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_MORESUN_REFRESH_SUNFLOWER && this.mPacketType == SeedType.SEED_SUNFLOWER)
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_MORESUN_PICK_UP_SUNFLOWER);
			}
		}

		public bool CanPickUp()
		{
			if (this.mBoard.mPaused || this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				return false;
			}
			if (this.mPacketType == SeedType.SEED_NONE)
			{
				return false;
			}
			SeedType theSeedType = this.mPacketType;
			if (this.mPacketType == SeedType.SEED_IMITATER && this.mImitaterType != SeedType.SEED_NONE)
			{
				theSeedType = this.mImitaterType;
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				return false;
			}
			if (!this.mApp.mEasyPlantingCheat)
			{
				if (!this.mActive)
				{
					return false;
				}
				int currentPlantCost = this.mBoard.GetCurrentPlantCost(this.mPacketType, this.mImitaterType);
				if (!this.mBoard.CanTakeSunMoney(currentPlantCost) && !this.mBoard.HasConveyorBeltSeedBank())
				{
					return false;
				}
				if (!this.mBoard.PlantingRequirementsMet(theSeedType))
				{
					return false;
				}
			}
			return true;
		}

		public void SetPacketType(SeedType theSeedType, SeedType theImitaterType)
		{
			this.mPacketType = theSeedType;
			this.mImitaterType = theImitaterType;
			this.mRefreshCounter = 0;
			this.mRefreshTime = 0;
			this.mRefreshing = false;
			this.mActive = true;
			SeedType theSeedtype = theSeedType;
			if (theSeedType == SeedType.SEED_IMITATER && theImitaterType != SeedType.SEED_NONE)
			{
				theSeedtype = theImitaterType;
			}
			if (this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && this.mApp.mGameMode != GameMode.GAMEMODE_CHALLENGE_LAST_STAND && !this.mApp.IsIZombieLevel() && !this.mApp.IsScaryPotterLevel())
			{
				if (this.mApp.IsWhackAZombieLevel())
				{
					return;
				}
				if (this.mApp.IsSurvivalMode() && this.mBoard.mChallenge.mSurvivalStage > 0)
				{
					return;
				}
				if ((Plant.IsUpgrade(theSeedtype) && !((LawnApp)GlobalStaticVars.gSexyAppBase).IsSurvivalMode()) || Plant.GetRefreshTime(this.mPacketType, this.mImitaterType) == 5000)
				{
					this.mRefreshTime = 3500;
					this.mRefreshing = true;
					this.mActive = false;
					return;
				}
				if (Plant.IsUpgrade(theSeedtype) && ((LawnApp)GlobalStaticVars.gSexyAppBase).IsSurvivalMode())
				{
					this.mRefreshTime = 8000;
					this.mRefreshing = true;
					this.mActive = false;
					return;
				}
				if (Plant.GetRefreshTime(this.mPacketType, this.mImitaterType) == 3000)
				{
					this.mRefreshTime = 2000;
					this.mRefreshing = true;
					this.mActive = false;
				}
			}
		}

		public void GetGraynessAndDarkness(ref int theGrayness, ref float thePercentDark)
		{
			float num = 0f;
			if (!this.mActive)
			{
				if (this.mRefreshTime == 0)
				{
					num = 1f;
				}
				else
				{
					num = (float)(this.mRefreshTime - this.mRefreshCounter) / (float)this.mRefreshTime;
				}
			}
			SeedType theSeedType = this.mPacketType;
			if (this.mPacketType == SeedType.SEED_IMITATER && this.mImitaterType != SeedType.SEED_NONE)
			{
				theSeedType = this.mImitaterType;
			}
			bool flag = true;
			if (this.mBoard.HasConveyorBeltSeedBank() || this.mApp.IsSlotMachineLevel())
			{
				flag = false;
			}
			int currentPlantCost = this.mBoard.GetCurrentPlantCost(this.mPacketType, this.mImitaterType);
			int num2 = 255;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED && !this.mActive)
			{
				num2 = 64;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && !this.mActive)
			{
				num2 = 64;
			}
			else if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				num2 = this.mBoard.mSeedBank.mCutSceneDarken;
				num = 0f;
			}
			else if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_LEVEL_1_PICK_UP_PEASHOOTER && this.mBoard.mTutorialTimer == -1 && this.mPacketType == SeedType.SEED_PEASHOOTER)
			{
				num2 = TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 75).mRed;
			}
			else if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_LEVEL_2_PICK_UP_SUNFLOWER && this.mPacketType == SeedType.SEED_SUNFLOWER)
			{
				num2 = TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 75).mRed;
			}
			else if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_MORESUN_PICK_UP_SUNFLOWER && this.mPacketType == SeedType.SEED_SUNFLOWER)
			{
				num2 = TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 75).mRed;
			}
			else if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_WHACK_A_ZOMBIE_PICK_SEED)
			{
				num2 = TodCommon.GetFlashingColor(this.mBoard.mMainCounter, 75).mRed;
			}
			else if (this.mApp.mEasyPlantingCheat)
			{
				num2 = 255;
				num = 0f;
			}
			else if (!this.mBoard.CanTakeSunMoney(currentPlantCost) && flag)
			{
				num2 = 128;
			}
			else if (num > 0f)
			{
				num2 = 128;
			}
			else if (!this.mBoard.PlantingRequirementsMet(theSeedType))
			{
				num2 = 128;
			}
			theGrayness = num2;
			thePercentDark = num;
		}

		public int CenterY()
		{
			return this.mY + this.mOffsetY + this.mHeight / 2;
		}

		public override string ToString()
		{
			return string.Format("X:{0}, Y:{1},Seed:{2}", this.mX, this.mY, this.mPacketType);
		}

		public int mRefreshCounter;

		public int mRefreshTime;

		public int mIndex;

		public int mOffsetY;

		public SeedType mPacketType;

		public SeedType mImitaterType;

		public int mSlotMachineCountDown;

		public SeedType mSlotMachiningNextSeed;

		public float mSlotMachiningPosition;

		public bool mActive;

		public bool mRefreshing;

		public int mTimesUsed;

		private TodWeightedArray[] aSeedWeightArray = new TodWeightedArray[53];

		private SeedType[] SLOT_SEED_TYPES = new SeedType[]
		{
			SeedType.SEED_SUNFLOWER,
			SeedType.SEED_PEASHOOTER,
			SeedType.SEED_SNOWPEA,
			SeedType.SEED_WALLNUT,
			SeedType.SEED_SLOT_MACHINE_SUN,
			SeedType.SEED_SLOT_MACHINE_DIAMOND
		};
	}
}
