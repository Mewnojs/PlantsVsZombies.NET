using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class SeedPacket : GameObject
    {
        public SeedPacket()
        {
            mPacketType = SeedType.None;
            mImitaterType = SeedType.None;
            mIndex = -1;
            mWidth = Constants.SMALL_SEEDPACKET_WIDTH;
            mHeight = Constants.SMALL_SEEDPACKET_HEIGHT;
            mRefreshCounter = 0;
            mRefreshTime = 0;
            mRefreshing = false;
            mActive = true;
            mOffsetY = 0;
            mSlotMachineCountDown = 0;
            mSlotMachiningNextSeed = SeedType.None;
            mSlotMachiningPosition = 0f;
            mTimesUsed = 0;
            mPosScaled = false;
        }

        public override bool SaveToFile(Sexy.Buffer b)
        {
            try
            {
                base.SaveToFile(b);
                b.WriteBoolean(mActive);
                b.WriteLong((int)mImitaterType);
                b.WriteLong(mIndex);
                b.WriteLong(mOffsetY);
                b.WriteLong((int)mPacketType);
                b.WriteLong(mRefreshCounter);
                b.WriteBoolean(mRefreshing);
                b.WriteLong(mRefreshTime);
                b.WriteLong(mSlotMachineCountDown);
                b.WriteLong((int)mSlotMachiningNextSeed);
                b.WriteFloat(mSlotMachiningPosition);
                b.WriteLong(mTimesUsed);
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
                mActive = b.ReadBoolean();
                mImitaterType = (SeedType)b.ReadLong();
                mIndex = b.ReadLong();
                mOffsetY = b.ReadLong();
                mPacketType = (SeedType)b.ReadLong();
                mRefreshCounter = b.ReadLong();
                mRefreshing = b.ReadBoolean();
                mRefreshTime = b.ReadLong();
                mSlotMachineCountDown = b.ReadLong();
                mSlotMachiningNextSeed = (SeedType)b.ReadLong();
                mSlotMachiningPosition = b.ReadFloat();
                mTimesUsed = b.ReadLong();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            return true;
        }

        public void Update()//1update
        {
            if (mApp.mGameScene != GameScenes.Playing)
            {
                return;
            }
            if (mPacketType == SeedType.None)
            {
                return;
            }
            if (mBoard.mMainCounter == 0)
            {
                FlashIfReady();
            }
            if (!mActive && mRefreshing)
            {
                mRefreshCounter++;
                if (mRefreshCounter > mRefreshTime)
                {
                    mRefreshCounter = 0;
                    mRefreshing = false;
                    Activate();
                    FlashIfReady();
                }
            }
            if (mSlotMachineCountDown > 0)
            {
                mSlotMachineCountDown--;
                float num = TodCommon.TodAnimateCurveFloat(400, 0, mSlotMachineCountDown, 6f, 2f, TodCurves.Linear);
                mSlotMachiningPosition += num * 0.01f;
                if (mSlotMachiningPosition >= 1f)
                {
                    mPacketType = mSlotMachiningNextSeed;
                    if (mSlotMachineCountDown >= 0 && mSlotMachineCountDown < 3)
                    {
                        mSlotMachiningPosition = 0f;
                        Activate();
                        return;
                    }
                    mSlotMachiningPosition -= 1f;
                    PickNextSlotMachineSeed();
                    return;
                }
                else if (mSlotMachineCountDown == 0)
                {
                    mSlotMachineCountDown = 3;
                }
            }
        }

        public void DrawBackground(Graphics g)
        {
            float num = 0f;
            int theGrayness = 255;
            if (mBoard.mCursorObject.mCursorType != CursorType.PlantFromBank || mBoard.mCursorObject.mSeedBankIndex != mIndex)
            {
                GetGraynessAndDarkness(ref theGrayness, ref num);
            }
            if (mApp.IsSlotMachineLevel())
            {
                TRect trect = mApp.mBoard.mChallenge.SlotMachineRect();
                int num2 = trect.mY + Constants.Challenge_SlotMachine_Y_Offset;
                float num3 = 0.9f;
                int challenge_SlotMachine_ClipHeight = Constants.Challenge_SlotMachine_ClipHeight;
                float num4 = AtlasResources.IMAGE_SEEDPACKETS.GetCelWidth() * num3;
                int num5 = num2 - Constants.Challenge_SlotMachine_Y_Pos + TodCommon.FloatRoundToInt(mSlotMachiningPosition * (float)(-challenge_SlotMachine_ClipHeight));
                Graphics @new = Graphics.GetNew(g);
                @new.mTransY = 0;
                @new.mTransX = mBoard.mX;
                int challenge_SlotMachine_Gap = Constants.Challenge_SlotMachine_Gap;
                int num6 = trect.mX + Constants.Challenge_SlotMachine_Offset + mIndex * TodCommon.FloatRoundToInt(challenge_SlotMachine_Gap + num4);
                int challenge_SlotMachine_Shadow_Offset = Constants.Challenge_SlotMachine_Shadow_Offset;
                @new.ClipRect(num6, num2, (int)num4, challenge_SlotMachine_ClipHeight);
                @new.HardwareClip();
                if (mSlotMachineCountDown > 0)
                {
                    DrawSeedPacketSlotMachine(@new, num6, num5, mPacketType, SeedType.None, theGrayness, num3);
                    DrawSeedPacketSlotMachine(@new, num6, num5 + AtlasResources.IMAGE_SEEDPACKETS.GetCelHeight() * num3 - challenge_SlotMachine_Shadow_Offset, mSlotMachiningNextSeed, SeedType.None, theGrayness, num3);
                }
                else
                {
                    DrawSeedPacketSlotMachine(@new, num6, num5, mPacketType, SeedType.None, theGrayness, num3);
                }
                @new.EndHardwareClip();
                @new.PrepareForReuse();
                return;
            }
            bool theDrawCostBackground = !mBoard.HasConveyorBeltSeedBank() && !mApp.IsSlotMachineLevel();
            SeedPacket.DrawSmallSeedPacket(g, 0f, mOffsetY, mPacketType, mImitaterType, 0f, theGrayness, false, true, true, theDrawCostBackground);
        }

        public void Draw(Graphics g)
        {
            if (mBoard.HasConveyorBeltSeedBank() || mApp.IsSlotMachineLevel())
            {
                return;
            }
            SeedPacket.DrawSmallSeedPacket(g, 0f, mOffsetY, mPacketType, mImitaterType, 0f, 255, true, true, false, false);
        }

        public void DrawOverlay(Graphics g)
        {
            float thePercentDark = 0f;
            int theGrayness = 255;
            bool flag = mBoard.mCursorObject.mCursorType == CursorType.PlantFromBank && mBoard.mCursorObject.mSeedBankIndex == mIndex;
            if (!flag)
            {
                GetGraynessAndDarkness(ref theGrayness, ref thePercentDark);
            }
            if (mSlotMachineCountDown > 0)
            {
                int num = TodCommon.FloatRoundToInt(mSlotMachiningPosition * (float)(-mHeight));
                Graphics @new = Graphics.GetNew(g);
                @new.ClipRect(0, 0, mWidth, mHeight);
                SeedPacket.DrawSmallSeedPacket(@new, 0f, num, mPacketType, SeedType.None, 0f, 128, false, false, false, false);
                SeedPacket.DrawSmallSeedPacket(@new, 0f, num + mHeight, mSlotMachiningNextSeed, SeedType.None, 0f, 128, false, false, false, false);
                @new.PrepareForReuse();
                return;
            }
            SeedPacket.DrawSmallSeedPacket(g, 0f, mOffsetY, mPacketType, mImitaterType, thePercentDark, theGrayness, false, true, false, false);
            if (flag)
            {
                g.DrawImage(AtlasResources.IMAGE_SELECTED_PACKET, Constants.SeedPacket_Selector_Pos.X, mOffsetY - Constants.SeedPacket_Selector_Pos.Y);
            }
        }

        internal static void DrawSeedType(Graphics g, float x, float y, SeedType theSeedType, SeedType theImitaterType)
        {
            Plant.DrawSeedType(g, theSeedType, theImitaterType, DrawVariation.Normal, x, y);
        }

        public static void DrawSmallSeedPacket(Graphics g, float x, float y, SeedType theSeedType, SeedType theImitaterType, float thePercentDark, int theGrayness, bool theDrawCost, bool theUseCurrentCost, bool theDrawBackground, bool theDrawCostBackground)
        {
            SeedType seedType = theSeedType;
            if (theSeedType == SeedType.Imitater && theImitaterType != SeedType.None)
            {
                seedType = theImitaterType;
            }
            if (seedType == SeedType.Leftpeater)
            {
                seedType = SeedType.Sprout;
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
                if (theSeedType == SeedType.SlotMachineSun)
                {
                    g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_SUN, x, y);
                }
                else if (theSeedType == SeedType.SlotMachineDiamond)
                {
                    g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_DIAMOND, x, y);
                }
                else if (theSeedType == SeedType.ZombiquariumSnorkel)
                {
                    g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_ZOMBIEQUARIUM, x, y);
                }
                else if (theSeedType == SeedType.ZombiquariumTrophy)
                {
                    g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_TROPHY, x, y);
                }
                else if (theSeedType == SeedType.BeghouledButtonShuffle)
                {
                    g.DrawImage(AtlasResources.IMAGE_SEEDPACKET_SHUFFLE, x, y);
                }
                else if (theSeedType == SeedType.BeghouledButtonCrater)
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
                    if (theSeedType == SeedType.Imitater)
                    {
                        g.SetColor(new SexyColor(0, 255, 0, 128));
                        g.FillRect((int)x, (int)y, Constants.SMALL_SEEDPACKET_WIDTH, Constants.SMALL_SEEDPACKET_HEIGHT);
                    }
                }
            }
            if (theDrawCostBackground)
            {
                Image theImage;
                if (theSeedType == SeedType.Imitater)
                {
                    theImage = AtlasResources.IMAGE_SEEDPACKETS_GRAY_TAB;
                }
                else if (theSeedType < SeedType.Gatlingpea)
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
                g.DrawImageF(theImage, x + x2, y + y2);
            }
            if (thePercentDark > 0f)
            {
                int theHeight = TodCommon.FloatRoundToInt(Constants.SMALL_SEEDPACKET_HEIGHT * thePercentDark) + 2;
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
                TodCommon.TodDrawString(g, theText, (int)x + x3, (int)y + y3, Resources.FONT_PICO129, SexyColor.Black, DrawStringJustification.Right);
            }
            g.SetColorizeImages(false);
        }

        private static void DrawIZombieSeedPacket(LawnApp theApp, Graphics g, float x, float y, SeedType aSeedType, float thePercentDark, int theGrayness, bool theDrawCost, bool theUseCurrentCost, bool theDrawBackground, bool theDrawCostBackground)
        {
            ZombieType izombieTypeFromSeed = SeedPacket.GetIZombieTypeFromSeed(aSeedType);
            if (izombieTypeFromSeed == ZombieType.Invalid)
            {
                return;
            }
            g.SetScale(0.75f, 0.5f, 0f, 0f);
            g.DrawImage(AtlasResources.IMAGE_ALMANAC_ZOMBIEWINDOW, x + Constants.IZombie_SeedOffset.X, y + Constants.IZombie_SeedOffset.Y);
            Graphics @new = Graphics.GetNew(g);
            @new.SetScale(1f);
            @new.ClipRect((int)x + Constants.IZombie_ClipOffset.X, (int)y + Constants.IZombie_ClipOffset.Y, Constants.IZombie_ClipOffset.Width, Constants.IZombie_ClipOffset.Height);
            @new.HardwareClip();
            theApp.mReanimatorCache.DrawCachedZombie(@new, x + Constants.ZombieOffsets[(int)izombieTypeFromSeed].X * g.mScaleX, y + Constants.ZombieOffsets[(int)izombieTypeFromSeed].Y * g.mScaleY, izombieTypeFromSeed);
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
            case SeedType.ZombieNormal:
                return ZombieType.Normal;
            case SeedType.ZombieTrafficCone:
                return ZombieType.TrafficCone;
            case SeedType.ZombiePolevaulter:
                return ZombieType.Polevaulter;
            case SeedType.ZombiePail:
                return ZombieType.Pail;
            case SeedType.ZombieLadder:
                return ZombieType.Ladder;
            case SeedType.ZombieDigger:
                return ZombieType.Digger;
            case SeedType.ZombieBungee:
                return ZombieType.Bungee;
            case SeedType.ZombieFootball:
                return ZombieType.Football;
            case SeedType.ZombieBalloon:
                return ZombieType.Balloon;
            case SeedType.ZombieScreenDoor:
                return ZombieType.Door;
            case SeedType.Zomboni:
                return ZombieType.Zamboni;
            case SeedType.ZombiePogo:
                return ZombieType.Pogo;
            case SeedType.ZombieDancer:
                return ZombieType.Dancer;
            case SeedType.ZombieGargantuar:
                return ZombieType.Gargantuar;
            case SeedType.ZombieImp:
                return ZombieType.Imp;
            default:
                return ZombieType.Invalid;
            }
        }

        private void DrawSeedPacketSlotMachine(Graphics g, float x, float y, SeedType theSeedType, SeedType theImitaterType, int theGrayness, float scale)
        {
            SeedType seedType = theSeedType;
            if (theSeedType == SeedType.Imitater && theImitaterType != SeedType.None)
            {
                seedType = theImitaterType;
            }
            if (seedType == SeedType.Leftpeater)
            {
                seedType = SeedType.Sprout;
            }
            if (theGrayness != 255)
            {
                g.SetColor(new SexyColor(theGrayness, theGrayness, theGrayness));
                g.SetColorizeImages(true);
            }
            if (theSeedType == SeedType.SlotMachineSun)
            {
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_SEEDPACKET_SUN, x, y, scale, scale);
            }
            else if (theSeedType == SeedType.SlotMachineDiamond)
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
            if (mBoard.mPaused || mApp.mGameScene != GameScenes.Playing)
            {
                return;
            }
            if (mPacketType == SeedType.None)
            {
                return;
            }
            if (mApp.IsSlotMachineLevel())
            {
                if (!mBoard.mAdvice.IsBeingDisplayed())
                {
                    mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_PULL]", MessageStyle.HintTallFast, AdviceType.None);
                }
                mBoard.mChallenge.mSlotMachineRollCount = Math.Min(mBoard.mChallenge.mSlotMachineRollCount, 2);
                return;
            }
            SeedType seedType = mPacketType;
            if (seedType == SeedType.Imitater && mImitaterType != SeedType.None)
            {
                seedType = mImitaterType;
            }
            if (!mApp.mEasyPlantingCheat)
            {
                if (!mActive)
                {
                    mApp.PlaySample(Resources.SOUND_BUZZER);
                    if (mApp.IsFirstTimeAdventureMode() && mBoard.mLevel == 1 && mBoard.mHelpDisplayed[0])
                    {
                        mBoard.DisplayAdvice("[ADVICE_SEED_REFRESH]", MessageStyle.TutorialLevel1, AdviceType.SeedRefresh);
                    }
                    return;
                }
                int currentPlantCost = mBoard.GetCurrentPlantCost(mPacketType, mImitaterType);
                if (!mBoard.CanTakeSunMoney(currentPlantCost) && !mBoard.HasConveyorBeltSeedBank())
                {
                    mApp.PlaySample(Resources.SOUND_BUZZER);
                    mBoard.mOutOfMoneyCounter = 70;
                    if (mApp.IsFirstTimeAdventureMode() && mBoard.mLevel == 1 && mBoard.mHelpDisplayed[0])
                    {
                        mBoard.DisplayAdvice("[ADVICE_CANT_AFFORD_PLANT]", MessageStyle.TutorialLevel1, AdviceType.CantAffordPlant);
                    }
                    return;
                }
                if (!mBoard.PlantingRequirementsMet(seedType))
                {
                    mApp.PlaySample(Resources.SOUND_BUZZER);
                    if (seedType == SeedType.Gatlingpea)
                    {
                        mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_REPEATER]", MessageStyle.HintLong, AdviceType.PlantNeedsRepeater);
                        return;
                    }
                    if (seedType == SeedType.Wintermelon)
                    {
                        mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_MELONPULT]", MessageStyle.HintLong, AdviceType.PlantNeedsMelonpult);
                        return;
                    }
                    if (seedType == SeedType.Twinsunflower)
                    {
                        mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_SUNFLOWER]", MessageStyle.HintLong, AdviceType.PlantNeedsSunflower);
                        return;
                    }
                    if (seedType == SeedType.Spikerock)
                    {
                        mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_SPIKEWEED]", MessageStyle.HintLong, AdviceType.PlantNeedsSpikeweed);
                        return;
                    }
                    if (seedType == SeedType.Cobcannon)
                    {
                        mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_KERNELPULT]", MessageStyle.HintLong, AdviceType.PlantNeedsKernelpult);
                        return;
                    }
                    if (seedType == SeedType.GoldMagnet)
                    {
                        mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_MAGNETSHROOM]", MessageStyle.HintLong, AdviceType.PlantNeedsMagnetshroom);
                        return;
                    }
                    if (seedType == SeedType.Gloomshroom)
                    {
                        mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_FUMESHROOM]", MessageStyle.HintLong, AdviceType.PlantNeedsFumeshroom);
                        return;
                    }
                    if (seedType == SeedType.Cattail)
                    {
                        mBoard.DisplayAdvice("[ADVICE_PLANT_NEEDS_LILYPAD]", MessageStyle.HintLong, AdviceType.PlantNeedsLilypad);
                        return;
                    }
                    Debug.ASSERT(false);
                    return;
                }
            }
            mBoard.ClearAdvice(AdviceType.CantAffordPlant);
            mBoard.ClearAdvice(AdviceType.PlantNeedsRepeater);
            mBoard.ClearAdvice(AdviceType.PlantNeedsMelonpult);
            mBoard.ClearAdvice(AdviceType.PlantNeedsSunflower);
            mBoard.ClearAdvice(AdviceType.PlantNeedsKernelpult);
            mBoard.ClearAdvice(AdviceType.PlantNeedsSpikeweed);
            mBoard.ClearAdvice(AdviceType.PlantNeedsMagnetshroom);
            mBoard.ClearAdvice(AdviceType.PlantNeedsFumeshroom);
            mBoard.ClearAdvice(AdviceType.PlantNeedsLilypad);
            if (mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                mBoard.mChallenge.BeghouledPacketClicked(this);
                return;
            }
            mBoard.mCursorObject.mType = mPacketType;
            mBoard.mCursorObject.mImitaterType = mImitaterType;
            mBoard.mCursorObject.mCursorType = CursorType.PlantFromBank;
            mBoard.mCursorObject.mSeedBankIndex = mIndex;
            mApp.PlaySample(Resources.SOUND_SEEDLIFT);
            if (mBoard.mTutorialState == TutorialState.Level1PickUpPeashooter)
            {
                mBoard.SetTutorialState(TutorialState.Level1PlantPeashooter);
            }
            else if (mBoard.mTutorialState == TutorialState.Level2PickUpSunflower)
            {
                if (mPacketType == SeedType.Sunflower)
                {
                    mBoard.SetTutorialState(TutorialState.Level2PlantSunflower);
                }
                else
                {
                    mBoard.SetTutorialState(TutorialState.Level2RefreshSunflower);
                }
            }
            else if (mBoard.mTutorialState == TutorialState.MoresunPickUpSunflower)
            {
                if (mPacketType == SeedType.Sunflower)
                {
                    mBoard.SetTutorialState(TutorialState.MoresunPlantSunflower);
                }
                else
                {
                    mBoard.SetTutorialState(TutorialState.MoresunRefreshSunflower);
                }
            }
            else if (mBoard.mTutorialState == TutorialState.WhackAZombiePickSeed || mBoard.mTutorialState == TutorialState.WhackAZombieBeforePickSeed)
            {
                mBoard.SetTutorialState(TutorialState.WhackAZombieCompleted);
            }
            Deactivate();
        }

        public bool MouseHitTest(int theX, int theY, HitResult theHitResult)
        {
            if (mSlotMachineCountDown > 0 || mPacketType == SeedType.None)
            {
                theHitResult.mObject = null;
                theHitResult.mObjectType = GameObjectType.None;
                return false;
            }
            if (theX >= mX && theX < mX + mWidth && theY >= mY + mOffsetY && theY < mY + mHeight + mOffsetY)
            {
                theHitResult.mObject = this;
                theHitResult.mObjectType = GameObjectType.Seedpacket;
                return true;
            }
            theHitResult.mObject = null;
            theHitResult.mObjectType = GameObjectType.None;
            return false;
        }

        public void Deactivate()
        {
            mActive = false;
            mRefreshCounter = 0;
            mRefreshTime = 0;
            mRefreshing = false;
        }

        public void Activate()
        {
            Debug.ASSERT(mPacketType != SeedType.None);
            mActive = true;
        }

        public void PickNextSlotMachineSeed()
        {
            int theTimeAge = mBoard.CountPlantByType(SeedType.Peashooter);
            for (int i = 0; i < aSeedWeightArray.Length; i++)
            {
                if (aSeedWeightArray[i] != null)
                {
                    aSeedWeightArray[i].PrepareForReuse();
                }
                aSeedWeightArray[i] = TodWeightedArray.GetNewTodWeightedArray();
            }
            int num = 0;
            for (int j = 0; j < 6; j++)
            {
                SeedType seedType = SLOT_SEED_TYPES[j];
                int num2 = 100;
                if (seedType == SeedType.Peashooter)
                {
                    num2 = TodCommon.TodAnimateCurve(0, 5, theTimeAge, 200, 100, TodCurves.Linear);
                }
                if (seedType == SeedType.SlotMachineDiamond)
                {
                    num2 = 30;
                }
                if (mIndex == 2 && seedType != SeedType.SlotMachineDiamond && (seedType == mBoard.mSeedBank.mSeedPackets[0].mSlotMachiningNextSeed || seedType == mBoard.mSeedBank.mSeedPackets[1].mSlotMachiningNextSeed))
                {
                    num2 += num2 / 2;
                }
                aSeedWeightArray[num].mItem = (int)seedType;
                aSeedWeightArray[num].mWeight = num2;
                num++;
            }
            mSlotMachiningNextSeed = (SeedType)TodCommon.TodPickFromWeightedArray(aSeedWeightArray, num);
        }

        public void SlotMachineStart()
        {
            mSlotMachineCountDown = 400;
            mSlotMachiningPosition = 0f;
            PickNextSlotMachineSeed();
        }

        public void WasPlanted()
        {
            Debug.ASSERT(mPacketType != SeedType.None);
            if (mBoard.HasConveyorBeltSeedBank())
            {
                mBoard.mSeedBank.RemoveSeed(mIndex);
                return;
            }
            if (mApp.IsSlotMachineLevel())
            {
                Deactivate();
                return;
            }
            if (mApp.mGameMode == GameMode.ChallengeLastStand && mBoard.mChallenge.mChallengeState != ChallengeState.LastStandOnslaught)
            {
                mTimesUsed++;
                mActive = true;
                FlashIfReady();
                return;
            }
            mTimesUsed++;
            mRefreshing = true;
            mRefreshTime = Plant.GetRefreshTime(mPacketType, mImitaterType);
        }

        public void FlashIfReady()
        {
            if (!CanPickUp())
            {
                return;
            }
            if (mApp.mEasyPlantingCheat)
            {
                return;
            }
            if (mBoard.mTutorialState == TutorialState.Level1RefreshPeashooter)
            {
                mBoard.SetTutorialState(TutorialState.Level1PickUpPeashooter);
                return;
            }
            if (mBoard.mTutorialState == TutorialState.Level2RefreshSunflower && mPacketType == SeedType.Sunflower)
            {
                mBoard.SetTutorialState(TutorialState.Level2PickUpSunflower);
                return;
            }
            if (mBoard.mTutorialState == TutorialState.MoresunRefreshSunflower && mPacketType == SeedType.Sunflower)
            {
                mBoard.SetTutorialState(TutorialState.MoresunPickUpSunflower);
            }
        }

        public bool CanPickUp()
        {
            if (mBoard.mPaused || mApp.mGameScene != GameScenes.Playing)
            {
                return false;
            }
            if (mPacketType == SeedType.None)
            {
                return false;
            }
            SeedType theSeedType = mPacketType;
            if (mPacketType == SeedType.Imitater && mImitaterType != SeedType.None)
            {
                theSeedType = mImitaterType;
            }
            if (mApp.IsSlotMachineLevel())
            {
                return false;
            }
            if (!mApp.mEasyPlantingCheat)
            {
                if (!mActive)
                {
                    return false;
                }
                int currentPlantCost = mBoard.GetCurrentPlantCost(mPacketType, mImitaterType);
                if (!mBoard.CanTakeSunMoney(currentPlantCost) && !mBoard.HasConveyorBeltSeedBank())
                {
                    return false;
                }
                if (!mBoard.PlantingRequirementsMet(theSeedType))
                {
                    return false;
                }
            }
            return true;
        }

        public void SetPacketType(SeedType theSeedType, SeedType theImitaterType)
        {
            mPacketType = theSeedType;
            mImitaterType = theImitaterType;
            mRefreshCounter = 0;
            mRefreshTime = 0;
            mRefreshing = false;
            mActive = true;
            SeedType theSeedtype = theSeedType;
            if (theSeedType == SeedType.Imitater && theImitaterType != SeedType.None)
            {
                theSeedtype = theImitaterType;
            }
            if (mApp.mGameMode != GameMode.ChallengeBeghouled && mApp.mGameMode != GameMode.ChallengeBeghouledTwist && mApp.mGameMode != GameMode.ChallengeLastStand && !mApp.IsIZombieLevel() && !mApp.IsScaryPotterLevel())
            {
                if (mApp.IsWhackAZombieLevel())
                {
                    return;
                }
                if (mApp.IsSurvivalMode() && mBoard.mChallenge.mSurvivalStage > 0)
                {
                    return;
                }
                if ((Plant.IsUpgrade(theSeedtype) && !((LawnApp)GlobalStaticVars.gSexyAppBase).IsSurvivalMode()) || Plant.GetRefreshTime(mPacketType, mImitaterType) == 5000)
                {
                    mRefreshTime = 3500;
                    mRefreshing = true;
                    mActive = false;
                    return;
                }
                if (Plant.IsUpgrade(theSeedtype) && ((LawnApp)GlobalStaticVars.gSexyAppBase).IsSurvivalMode())
                {
                    mRefreshTime = 8000;
                    mRefreshing = true;
                    mActive = false;
                    return;
                }
                if (Plant.GetRefreshTime(mPacketType, mImitaterType) == 3000)
                {
                    mRefreshTime = 2000;
                    mRefreshing = true;
                    mActive = false;
                }
            }
        }

        public void GetGraynessAndDarkness(ref int theGrayness, ref float thePercentDark)
        {
            float num = 0f;
            if (!mActive)
            {
                if (mRefreshTime == 0)
                {
                    num = 1f;
                }
                else
                {
                    num = (mRefreshTime - mRefreshCounter) / (float)mRefreshTime;
                }
            }
            SeedType theSeedType = mPacketType;
            if (mPacketType == SeedType.Imitater && mImitaterType != SeedType.None)
            {
                theSeedType = mImitaterType;
            }
            bool flag = true;
            if (mBoard.HasConveyorBeltSeedBank() || mApp.IsSlotMachineLevel())
            {
                flag = false;
            }
            int currentPlantCost = mBoard.GetCurrentPlantCost(mPacketType, mImitaterType);
            int num2 = 255;
            if (mApp.mGameMode == GameMode.ChallengeBeghouled && !mActive)
            {
                num2 = 64;
            }
            else if (mApp.mGameMode == GameMode.ChallengeBeghouledTwist && !mActive)
            {
                num2 = 64;
            }
            else if (mApp.mGameScene != GameScenes.Playing)
            {
                num2 = mBoard.mSeedBank.mCutSceneDarken;
                num = 0f;
            }
            else if (mBoard.mTutorialState == TutorialState.Level1PickUpPeashooter && mBoard.mTutorialTimer == -1 && mPacketType == SeedType.Peashooter)
            {
                num2 = TodCommon.GetFlashingColor(mBoard.mMainCounter, 75).mRed;
            }
            else if (mBoard.mTutorialState == TutorialState.Level2PickUpSunflower && mPacketType == SeedType.Sunflower)
            {
                num2 = TodCommon.GetFlashingColor(mBoard.mMainCounter, 75).mRed;
            }
            else if (mBoard.mTutorialState == TutorialState.MoresunPickUpSunflower && mPacketType == SeedType.Sunflower)
            {
                num2 = TodCommon.GetFlashingColor(mBoard.mMainCounter, 75).mRed;
            }
            else if (mBoard.mTutorialState == TutorialState.WhackAZombiePickSeed)
            {
                num2 = TodCommon.GetFlashingColor(mBoard.mMainCounter, 75).mRed;
            }
            else if (mApp.mEasyPlantingCheat)
            {
                num2 = 255;
                num = 0f;
            }
            else if (!mBoard.CanTakeSunMoney(currentPlantCost) && flag)
            {
                num2 = 128;
            }
            else if (num > 0f)
            {
                num2 = 128;
            }
            else if (!mBoard.PlantingRequirementsMet(theSeedType))
            {
                num2 = 128;
            }
            theGrayness = num2;
            thePercentDark = num;
        }

        public int CenterY()
        {
            return mY + mOffsetY + mHeight / 2;
        }

        public override string ToString()
        {
            return string.Format("X:{0}, Y:{1},Seed:{2}", mX, mY, mPacketType);
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
            SeedType.Sunflower,
            SeedType.Peashooter,
            SeedType.Snowpea,
            SeedType.Wallnut,
            SeedType.SlotMachineSun,
            SeedType.SlotMachineDiamond
        };
    }
}
