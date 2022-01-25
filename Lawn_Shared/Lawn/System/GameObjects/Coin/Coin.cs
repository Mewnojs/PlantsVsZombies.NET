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
            mDead = false;
            mWidth = (int)Constants.InvertAndScale(23f);
            mHeight = (int)Constants.InvertAndScale(23f);
            mType = theCoinType;
            mPosX = theX;
            mPosY = theY;
            mDisappearCounter = 0;
            mIsBeingCollected = false;
            mFadeCount = 0;
            mCoinMotion = theCoinMotion;
            mCoinAge = 0;
            mAttachmentID = null;
            mCollectionDistance = 0f;
            mRenderOrder = Board.MakeRenderOrder(RenderLayer.CoinBank, 0, 1);
            mScale = 1f;
            mUsableSeedType = SeedType.None;
            mNeedsBouncyArrow = false;
            mHasBouncyArrow = false;
            mHitGround = false;
            mTimesDropped = 0;
            mPottedPlantSpec.InitializePottedPlant(SeedType.None);
            int num = Constants.LAWN_XMIN + (int)Constants.InvertAndScale(40f);
            if (num + mWidth < Constants.LAWN_XMIN && mApp.mGameMode != GameMode.ChallengeZenGarden)
            {
                mPosX = num + mWidth;
            }
            if (IsSun())
            {
                float num2 = mWidth * Constants.IS * 0.5f;
                float num3 = mHeight * Constants.IS * 0.5f;
                Reanimation reanimation = mApp.AddReanimation(0f, 0f, 0, ReanimationType.Sun);
                reanimation.SetPosition((mPosX + num2) * Constants.S, (mPosY + num3) * Constants.S);
                reanimation.mLoopType = ReanimLoopType.Loop;
                reanimation.mAnimRate = 6f;
                GlobalMembersAttachment.AttachReanim(ref mAttachmentID, reanimation, num2, num3);
            }
            else if (mType == CoinType.Silver)
            {
                mPosX -= 10f;
                mPosY -= 8f;
                float num4 = 9f;
                float num5 = 9f;
                Reanimation reanimation2 = mApp.AddReanimation(0f, 0f, 0, ReanimationType.CoinSilver);
                reanimation2.SetPosition((mPosX + num4) * Constants.S, (mPosY + num5) * Constants.S);
                reanimation2.mLoopType = ReanimLoopType.Loop;
                reanimation2.mAnimTime = TodCommon.RandRangeFloat(0f, 0.99f);
                reanimation2.mAnimRate *= TodCommon.RandRangeFloat(0.6f, 1f);
                GlobalMembersAttachment.AttachReanim(ref mAttachmentID, reanimation2, num4, num5);
            }
            else if (mType == CoinType.Gold)
            {
                mPosX -= 10f;
                mPosY -= 8f;
                float num6 = 9f;
                float num7 = 9f;
                Reanimation reanimation3 = mApp.AddReanimation(0f, 0f, 0, ReanimationType.CoinGold);
                reanimation3.SetPosition((mPosX + num6) * Constants.S, (mPosY + num7) * Constants.S);
                reanimation3.mLoopType = ReanimLoopType.Loop;
                reanimation3.mAnimTime = TodCommon.RandRangeFloat(0f, 0.99f);
                reanimation3.mAnimRate *= TodCommon.RandRangeFloat(0.6f, 1f);
                GlobalMembersAttachment.AttachReanim(ref mAttachmentID, reanimation3, num6, num7);
            }
            else if (mType == CoinType.Diamond)
            {
                mPosX -= 15f;
                mPosY -= 15f;
                float num8 = -3f;
                float num9 = 4f;
                Reanimation reanimation4 = mApp.AddReanimation(0f, 0f, 0, ReanimationType.Diamond);
                reanimation4.SetPosition((mPosX + num8) * Constants.S, (mPosY + num9) * Constants.S);
                reanimation4.mLoopType = ReanimLoopType.Loop;
                reanimation4.mAnimTime = TodCommon.RandRangeFloat(0f, 0.99f);
                reanimation4.mAnimRate = TodCommon.RandRangeFloat(50f, 80f);
                GlobalMembersAttachment.AttachReanim(ref mAttachmentID, reanimation4, num8, num9);
            }
            if (mApp.IsStormyNightLevel())
            {
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            if (mType == CoinType.FinalSeedPacket)
            {
                mWidth = AtlasResources.IMAGE_SEEDPACKETS.GetCelWidth();
                mHeight = AtlasResources.IMAGE_SEEDPACKETS.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
                Coin.LoadSeedPacketImage(GetFinalSeedPacketType());
            }
            else if (mType == CoinType.Trophy)
            {
                mWidth = AtlasResources.IMAGE_TROPHY.GetCelWidth();
                mHeight = AtlasResources.IMAGE_TROPHY.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (mType == CoinType.Shovel)
            {
                mWidth = AtlasResources.IMAGE_SHOVEL.GetCelWidth();
                mHeight = AtlasResources.IMAGE_SHOVEL.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (mType == CoinType.Carkeys)
            {
                mWidth = AtlasResources.IMAGE_CARKEYS.GetCelWidth();
                mHeight = AtlasResources.IMAGE_CARKEYS.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (mType == CoinType.Almanac)
            {
                mWidth = AtlasResources.IMAGE_ALMANAC.GetCelWidth();
                mHeight = AtlasResources.IMAGE_ALMANAC.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (mType == CoinType.WateringCan)
            {
                mWidth = AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1.GetCelWidth();
                mHeight = AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (mType == CoinType.Taco)
            {
                mWidth = AtlasResources.IMAGE_TACO.GetCelWidth();
                mHeight = AtlasResources.IMAGE_TACO.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (mType == CoinType.Bacon)
            {
                mWidth = AtlasResources.IMAGE_BACON.GetCelWidth();
                mHeight = AtlasResources.IMAGE_BACON.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (mType == CoinType.Note)
            {
                mWidth = Resources.IMAGE_ZOMBIE_NOTE_SMALL.GetCelWidth();
                mHeight = Resources.IMAGE_ZOMBIE_NOTE_SMALL.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (mType == CoinType.UsableSeedPacket)
            {
                mWidth = AtlasResources.IMAGE_SEEDPACKETS.GetCelWidth();
                mHeight = AtlasResources.IMAGE_SEEDPACKETS.GetCelHeight();
                mRenderOrder = 500002;
            }
            else if (mType == CoinType.PresentPlant)
            {
                mWidth = AtlasResources.IMAGE_PRESENT.GetCelWidth();
                mHeight = AtlasResources.IMAGE_PRESENT.GetCelHeight();
                if (mApp.IsSurvivalEndless(mApp.mGameMode) || mApp.IsEndlessIZombie(mApp.mGameMode) || mApp.IsEndlessScaryPotter(mApp.mGameMode))
                {
                    SeedType theSeedType = mApp.mZenGarden.PickRandomSeedType();
                    mPottedPlantSpec.InitializePottedPlant(theSeedType);
                }
                else if (mBoard.mBackground == BackgroundType.Num1Day)
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
                    mPottedPlantSpec.InitializePottedPlant(theSeedType2);
                }
                else if (mBoard.mBackground == BackgroundType.Num2Night)
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
                    mPottedPlantSpec.InitializePottedPlant(theSeedType3);
                }
                else if (mBoard.mBackground == BackgroundType.Num3Pool)
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
                    mPottedPlantSpec.InitializePottedPlant(theSeedType4);
                }
                else if (mBoard.mBackground == BackgroundType.Num4Fog)
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
                    mPottedPlantSpec.InitializePottedPlant(theSeedType5);
                }
                else if (mBoard.mBackground == BackgroundType.Num5Roof)
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
                    mPottedPlantSpec.InitializePottedPlant(theSeedType6);
                }
                else
                {
                    SeedType theSeedType7 = mApp.mZenGarden.PickRandomSeedType();
                    mPottedPlantSpec.InitializePottedPlant(theSeedType7);
                }
            }
            else if (mType == CoinType.AwardMoneyBag || mType == CoinType.AwardBagDiamond)
            {
                mWidth = AtlasResources.IMAGE_MONEYBAG_HI_RES.GetCelWidth();
                mHeight = AtlasResources.IMAGE_MONEYBAG_HI_RES.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (mType == CoinType.Chocolate || mType == CoinType.AwardChocolate)
            {
                mWidth = AtlasResources.IMAGE_CHOCOLATE.GetCelWidth();
                mHeight = AtlasResources.IMAGE_CHOCOLATE.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            else if (IsPresentWithAdvice())
            {
                mWidth = AtlasResources.IMAGE_PRESENT.GetCelWidth();
                mHeight = AtlasResources.IMAGE_PRESENT.GetCelHeight();
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 0);
            }
            mWidth = (int)(mWidth * Constants.IS);
            mHeight = (int)(mHeight * Constants.IS);
            switch (mCoinMotion)
            {
            case CoinMotion.FromSky:
                mVelY = 0.67f;
                mVelX = 0f;
                mGroundY = RandomNumbers.NextNumber(250) + 300;
                break;
            case CoinMotion.FromSkySlow:
                mVelY = 0.33f;
                mVelX = 0f;
                mGroundY = RandomNumbers.NextNumber(250) + 300;
                break;
            case CoinMotion.FromPlant:
                mVelY = -1.7f - TodCommon.RandRangeFloat(0f, 1.7f);
                mVelX = -0.4f + TodCommon.RandRangeFloat(0f, 0.8f);
                mGroundY = (int)mPosY + 15 + RandomNumbers.NextNumber(20);
                mScale = 0.4f;
                break;
            case CoinMotion.Coin:
                mVelY = -3f - TodCommon.RandRangeFloat(0f, 2f);
                mVelX = -0.5f + TodCommon.RandRangeFloat(0f, 1f);
                mGroundY = (int)mPosY + 45 + RandomNumbers.NextNumber(20);
                if (mGroundY > Constants.BOARD_HEIGHT - 60 && mApp.mGameMode != GameMode.ChallengeZenGarden)
                {
                    mGroundY = Constants.BOARD_HEIGHT - 60;
                }
                if (mGroundY < 80 && mApp.mGameMode != GameMode.ChallengeZenGarden)
                {
                    mGroundY = 80;
                }
                if (mType == CoinType.FinalSeedPacket || mType == CoinType.UsableSeedPacket || mType == CoinType.Trophy || mType == CoinType.Shovel || mType == CoinType.Carkeys || mType == CoinType.Almanac || mType == CoinType.Taco || mType == CoinType.Bacon || mType == CoinType.WateringCan || mType == CoinType.Note)
                {
                    mGroundY -= 30;
                }
                break;
            case CoinMotion.LawnmowerCoin:
                mVelY = 0f;
                mVelX = 0f;
                mGroundY = 600;
                Collect();
                break;
            case CoinMotion.FromPresent:
                mVelY = 0f;
                mVelX = 0f;
                mGroundY = 600;
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 1);
                break;
            case CoinMotion.FromBoss:
                mVelY = -5f;
                mVelX = -3f;
                mPosX = 750f;
                mPosY = 245f;
                mGroundY = (int)mPosY + 40;
                break;
            default:
                Debug.ASSERT(false);
                break;
            }
            if (mCoinMotion != CoinMotion.LawnmowerCoin && mApp.mGameMode != GameMode.ChallengeZenGarden && mPosX - mWidth < Constants.LAWN_XMIN)
            {
                mPosX = Constants.LAWN_XMIN + mWidth;
            }
            mScale *= GetSunScale();
            if (CoinGetsBouncyArrow())
            {
                mNeedsBouncyArrow = true;
            }
            if (mCoinMotion != CoinMotion.FromPresent)
            {
                PlayLaunchSound();
            }
            if (mApp.mGameMode != GameMode.ChallengeZenGarden)
            {
                Coin.CheckRange_X(ref mPosX, mWidth, theCoinMotion);
            }
            Update();
        }

        public void Dispose()
        {
            GlobalMembersAttachment.AttachmentDie(ref mAttachmentID);
        }

        public void MouseDown(int x, int y, int theClickCount)
        {
            if (mBoard == null || mBoard.mPaused || mApp.mGameScene != GameScenes.Playing)
            {
                return;
            }
            if (mDead)
            {
                return;
            }
            if (theClickCount >= 0 && mType == CoinType.UsableSeedPacket) 
            {
                if (mBoard.mCursorObject.mCoinID == this)
                {
                    mBoard.RefreshSeedPacketFromCursor();
                    mApp.PlayFoley(FoleyType.Drop);
                    return;
                }
            }
            if (theClickCount >= 0 && !mIsBeingCollected)
            {
                PlayCollectSound();
                Collect();
                if (mApp.IsFirstTimeAdventureMode() && mBoard.mLevel == 1)
                {
                    mBoard.DisplayAdvice("[ADVICE_CLICKED_ON_SUN]", MessageStyle.TutorialLevel1Stay, AdviceType.ClickedOnSun);
                }
            }
        }

        public bool MouseHitTest(int theX, int theY, out HitResult theHitResult)
        {
            theHitResult = default(HitResult);
            theX = (int)(theX * Constants.IS);
            theY = (int)(theY * Constants.IS);
            int num = 0;
            if (mType == CoinType.AwardPresent || IsPresentWithAdvice() || mType == CoinType.PresentPlant)
            {
                num = -60;
            }
            int num2 = 0;
            int num3 = 0;
            if (mApp.IsWhackAZombieLevel())
            {
                num3 = 30;
                num2 = 15;
            }
            if (IsMoney() || mType == CoinType.AwardMoneyBag || mType == CoinType.AwardBagDiamond)
            {
                num2 = 40;
            }
            if (mType == CoinType.Sun || mType == CoinType.Smallsun)
            {
                num2 = 50;
            }
            bool flag = !mDead 
                && (!mIsBeingCollected || (mType == CoinType.UsableSeedPacket && mBoard.mIgnoreNextMouseUpSeedPacket)) 
                && (mType != CoinType.UsableSeedPacket
                    || mBoard == null
                    || mBoard.mCursorObject.mCursorType == CursorType.Normal
                    || mApp.IsWhackAZombieLevel()
                    || mBoard.mIgnoreNextMouseUpSeedPacket) 
                && (theX >= mPosX - num2
                    && theX < mPosX + mWidth + num2
                    && theY >= mPosY + num - num2
                    && theY < mPosY + mHeight + num + num2 + num3);
            if (flag)
            {
                theHitResult.mObject = this;
                theHitResult.mObjectType = GameObjectType.Coin;
                return true;
            }
            theHitResult.mObject = null;
            theHitResult.mObjectType = GameObjectType.None;
            return false;
        }

        public void Die()
        {
            Debug.ASSERT(mBoard == null || mBoard.mCursorObject.mCoinID != mBoard.mCoins[mBoard.mCoins.IndexOf(this)]);
            mDead = true;
            GlobalMembersAttachment.AttachmentDie(ref mAttachmentID);
        }

        public void StartFade()
        {
            mFadeCount = 15;
        }

        public void Update()
        {
            mCoinAge += 3;
            if (mApp.mGameScene != GameScenes.Playing && mApp.mGameScene != GameScenes.Award && mBoard != null && !mBoard.mCutScene.ShouldRunUpsellBoard())
            {
                return;
            }
            if (mFadeCount < 0 || mFadeCount >= 3)
            {
                UpdateFade();
            }
            else if (!mIsBeingCollected)
            {
                UpdateFall();
            }
            else
            {
                UpdateCollected();
            }
            if (mAttachmentID != null)
            {
                float num = 0f;
                float num2 = 0f;
                if (mType == CoinType.Diamond)
                {
                    float num3 = Constants.InvertAndScale(18f);
                    float num4 = Constants.InvertAndScale(13f);
                    num = num3 - num3 * mScale;
                    num2 = num4 - num4 * mScale;
                }
                GlobalMembersAttachment.AttachmentUpdateAndMove(ref mAttachmentID, mPosX + num, mPosY + num2);
                GlobalMembersAttachment.AttachmentOverrideColor(mAttachmentID, GetColor());
                GlobalMembersAttachment.AttachmentOverrideScale(mAttachmentID, mScale);
                if ((!mHitGround || mIsBeingCollected) && (mType == CoinType.Silver || mType == CoinType.Gold))
                {
                    GlobalMembersAttachment.AttachmentOverrideColor(mAttachmentID, new SexyColor(0, 0, 0, 0));
                }
            }
        }

        public void Draw(Graphics g)
        {
            g.SetColor(GetColor());
            if (mType == CoinType.Diamond)
            {
                g.SetColorizeImages(true);
                g.DrawImage(AtlasResources.IMAGE_AWARDPICKUPGLOW, (mPosX - 56f) * Constants.S, (mPosY - 66f) * Constants.S);
                g.SetColorizeImages(false);
            }
            if (mType == CoinType.PresentPlant)
            {
                g.SetColorizeImages(true);
                g.DrawImage(AtlasResources.IMAGE_AWARDPICKUPGLOW, (mPosX - 50f) * Constants.S, (mPosY - 70f) * Constants.S);
                g.SetColorizeImages(false);
            }
            if (mType == CoinType.AwardPresent && mIsBeingCollected)
            {
                g.SetColorizeImages(true);
                g.DrawImage(AtlasResources.IMAGE_AWARDPICKUPGLOW, (mPosX - 50f) * Constants.S, (mPosY - 64f) * Constants.S);
                g.SetColorizeImages(false);
            }
            if (mType == CoinType.Chocolate || mType == CoinType.AwardChocolate)
            {
                g.SetColorizeImages(true);
                g.DrawImage(AtlasResources.IMAGE_AWARDPICKUPGLOW, (mPosX - 56f) * Constants.S, (mPosY - 50f) * Constants.S);
                g.SetColorizeImages(false);
            }
            if (mAttachmentID != null)
            {
                Graphics @new = Graphics.GetNew(g);
                base.MakeParentGraphicsFrame(@new);
                GlobalMembersAttachment.AttachmentDraw(mAttachmentID, @new, false, true);
                @new.PrepareForReuse();
            }
            if ((mType == CoinType.Silver || mType == CoinType.Gold) && mHitGround && !mIsBeingCollected)
            {
                return;
            }
            if (mType == CoinType.Diamond)
            {
                return;
            }
            if (IsLevelAward() && !mIsBeingCollected)
            {
                SexyColor flashingColor = TodCommon.GetFlashingColor(mCoinAge, 75);
                g.SetColor(flashingColor);
            }
            if (mType == CoinType.Silver || mType == CoinType.Gold)
            {
                g.SetColorizeImages(true);
                TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_REANIM_COINGLOW, (mPosX - Constants.Coin_Glow_Offset.X) * Constants.S, (mPosY - Constants.Coin_Glow_Offset.Y) * Constants.S, mScale, mScale);
                g.SetColorizeImages(false);
            }
            Image theImageStrip = null;
            int theCelCol = 0;
            float num = mScale;
            float num2 = 0f;
            float num3 = 0f;
            if (mType == CoinType.Silver)
            {
                theImageStrip = AtlasResources.IMAGE_REANIM_COIN_SILVER_DOLLAR;
                num2 = 8f;
                num3 = 10f;
            }
            else if (mType == CoinType.Gold)
            {
                theImageStrip = AtlasResources.IMAGE_REANIM_COIN_GOLD_DOLLAR;
                num2 = 8f;
                num3 = 10f;
            }
            else
            {
                if (mType == CoinType.Sun || mType == CoinType.Smallsun || mType == CoinType.Largesun)
                {
                    return;
                }
                if (mType == CoinType.FinalSeedPacket)
                {
                    TodCommon.TodDrawImageCenterScaledF(g, AtlasResources.IMAGE_SEEDPACKETS, (int)(mPosX * Constants.S), (int)(mPosY * Constants.S), mScale, mScale, (int)Coin.LoadedSeedType, true);
                    return;
                }
                if (mType == CoinType.PresentPlant || mType == CoinType.AwardPresent)
                {
                    if (mIsBeingCollected)
                    {
                        mApp.mZenGarden.DrawPottedPlantIcon(g, (mPosX + 35f) * Constants.S, (mPosY + 15f) * Constants.S, mPottedPlantSpec);
                        return;
                    }
                    theImageStrip = AtlasResources.IMAGE_PRESENT;
                    num3 = -60f;
                }
                else if (IsPresentWithAdvice())
                {
                    num3 = -60f;
                    if (mIsBeingCollected)
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
                else if (mType == CoinType.AwardMoneyBag || mType == CoinType.AwardBagDiamond)
                {
                    if (mIsBeingCollected && mApp.IsQuickPlayMode())
                    {
                        return;
                    }
                    theImageStrip = AtlasResources.IMAGE_MONEYBAG_HI_RES;
                    if (mScale == 1f)
                    {
                        num2 -= mWidth / 4 + 10;
                        num3 -= mHeight / 4;
                    }
                    num *= 0.5f;
                }
                else if (mType == CoinType.Chocolate || mType == CoinType.AwardChocolate)
                {
                    theImageStrip = AtlasResources.IMAGE_CHOCOLATE;
                }
                else if (mType == CoinType.Trophy)
                {
                    theImageStrip = AtlasResources.IMAGE_TROPHY_HI_RES;
                    num2 -= mWidth / 2;
                    num3 -= mHeight / 2;
                    num *= 0.5f;
                }
                else if (mType == CoinType.Shovel)
                {
                    theImageStrip = AtlasResources.IMAGE_SHOVEL_HI_RES;
                    num2 -= Constants.Coin_Shovel_Offset.X;
                    num3 -= Constants.Coin_Shovel_Offset.Y;
                    num *= 0.5f;
                }
                else if (mType == CoinType.Carkeys)
                {
                    theImageStrip = AtlasResources.IMAGE_CARKEYS;
                }
                else if (mType == CoinType.Almanac)
                {
                    theImageStrip = AtlasResources.IMAGE_ALMANAC;
                }
                else if (mType == CoinType.Taco)
                {
                    theImageStrip = AtlasResources.IMAGE_TACO;
                }
                else if (mType == CoinType.Bacon)
                {
                    theImageStrip = AtlasResources.IMAGE_BACON;
                }
                else if (mType == CoinType.WateringCan)
                {
                    num2 -= Constants.Coin_Shovel_Offset.X;
                    num3 -= Constants.Coin_Shovel_Offset.Y;
                    theImageStrip = AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1;
                }
                else if (mType == CoinType.Note)
                {
                    theImageStrip = Resources.IMAGE_ZOMBIE_NOTE_SMALL;
                }
                else
                {
                    if (mType == CoinType.UsableSeedPacket)
                    {
                        int theGrayness = 255;
                        if (mIsBeingCollected)
                        {
                            //theGrayness = 128;
                        }
                        else
                        {
                            int disappearTime = GetDisappearTime();
                            if (mDisappearCounter > disappearTime - 300 && mDisappearCounter % 60 < 30)
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
                        SeedPacket.DrawSmallSeedPacket(g, (int)(mPosX * Constants.S), (int)(mPosY * Constants.S), mUsableSeedType, SeedType.None, 0f, theGrayness, false, false, true, false);
                        if (mIsBeingCollected)
                        {
                            g.DrawImage(AtlasResources.IMAGE_SELECTED_PACKET, (int)(mPosX * Constants.S + Constants.SeedPacket_Selector_Pos.X), (int)(mPosY * Constants.S - Constants.SeedPacket_Selector_Pos.Y));
                        }
                        g.SetColorizeImages(false);
                        return;
                    }
                    Debug.ASSERT(false);
                }
            }
            g.SetColorizeImages(true);
            TodCommon.TodDrawImageCelCenterScaledF(g, theImageStrip, (mPosX + num2) * Constants.S, (mPosY + num3) * Constants.S, theCelCol, num, num);
            g.SetColorizeImages(false);
        }

        public void Collect()
        {
            if (mDead)
            {
                return;
            }
            mCollectX = mPosX;
            mCollectY = mPosY;
            mIsBeingCollected = true;
            bool flag = false;
            if ((mApp.IsEndlessIZombie(mApp.mGameMode) || mApp.IsEndlessScaryPotter(mApp.mGameMode)) && IsLevelAward())
            {
                flag = true;
            }
            if (mType == CoinType.AwardPresent || mType == CoinType.PresentPlant)
            {
                Debug.ASSERT(mBoard != null);
                if (mApp.mZenGarden.IsZenGardenFull(false))
                {
                    mBoard.DisplayAdvice("[DIALOG_ZEN_GARDEN_FULL]", MessageStyle.HintFast, AdviceType.None);
                }
                else
                {
                    mBoard.mPottedPlantsCollected++;
                    mBoard.DisplayAdvice("[ADVICE_FOUND_PLANT]", MessageStyle.HintFast, AdviceType.None);
                    mApp.AddTodParticle(mPosX + Constants.InvertAndScale(30f), mPosY + Constants.InvertAndScale(30f), mRenderOrder + 1, ParticleEffect.PresentPickup);
                    mApp.mZenGarden.AddPottedPlant(mPottedPlantSpec);
                }
                mDisappearCounter = 0;
                mFadeCount = 0;
                if (flag)
                {
                    GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.AwardPickupArrow, null);
                    mBoard.FadeOutLevel();
                }
                return;
            }
            if (mType == CoinType.PresentMinigames)
            {
                mApp.AddTodParticle(mPosX + 30f, mPosY + 30f, mRenderOrder + 1, ParticleEffect.PresentPickup);
                mDisappearCounter = 0;
                mFadeCount = 0;
                GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.AwardPickupArrow, null);
                mApp.mPlayerInfo.UnlockFirstMiniGames();
                return;
            }
            if (mType == CoinType.PresentPuzzleMode)
            {
                mApp.AddTodParticle(mPosX + 30f, mPosY + 30f, mRenderOrder + 1, ParticleEffect.PresentPickup);
                mDisappearCounter = 0;
                mFadeCount = 0;
                GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.AwardPickupArrow, null);
                mApp.mPlayerInfo.UnlockPuzzleMode();
                return;
            }
            if (mType == CoinType.Chocolate || mType == CoinType.AwardChocolate)
            {
                mBoard.mChocolateCollected++;
                mApp.AddTodParticle(mPosX + 30f, mPosY + 30f, mRenderOrder + 1, ParticleEffect.PresentPickup);
                if (mApp.mPlayerInfo.mPurchases[26] < 1000)
                {
                    mBoard.DisplayAdvice("[ADVICE_FOUND_CHOCOLATE]", MessageStyle.HintTallFast, AdviceType.None);
                    mApp.mPlayerInfo.mPurchases[26] = 1001;
                }
                else
                {
                    mApp.mPlayerInfo.mPurchases[26]++;
                }
                mDisappearCounter = 0;
                StartFade();
                if (flag)
                {
                    GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.AwardPickupArrow, null);
                    mBoard.FadeOutLevel();
                }
                return;
            }
            if (IsLevelAward())
            {
                if (mApp.IsQuickPlayMode() && mType == CoinType.AwardMoneyBag)
                {
                    mApp.PlayFoley(FoleyType.Coin);
                    FanOutCoins(CoinType.Gold, 5);
                    StartFade();
                }
                else if (flag)
                {
                    if (mType == CoinType.AwardBagDiamond)
                    {
                        mApp.PlaySample(Resources.SOUND_DIAMOND);
                        FanOutCoins(CoinType.Diamond, 1);
                        StartFade();
                    }
                    else if (mType == CoinType.AwardMoneyBag)
                    {
                        mApp.PlayFoley(FoleyType.Coin);
                        FanOutCoins(CoinType.Gold, 5);
                        StartFade();
                    }
                }
                else if (mApp.IsScaryPotterLevel())
                {
                    if (mType == CoinType.Trophy)
                    {
                        mApp.PlayFoley(FoleyType.Coin);
                        FanOutCoins(CoinType.Gold, 5);
                    }
                    else if (mType == CoinType.AwardMoneyBag)
                    {
                        mApp.PlayFoley(FoleyType.Coin);
                        FanOutCoins(CoinType.Gold, 2);
                    }
                }
                else if (mApp.IsAdventureMode() && mBoard.mLevel == 50)
                {
                    FanOutCoins(CoinType.Diamond, 3);
                }
                else if (mApp.IsFirstTimeAdventureMode() && mBoard.mLevel == 4)
                {
                    mApp.PlaySample(Resources.SOUND_SHOVEL);
                }
                else if (mApp.IsFirstTimeAdventureMode() && (mBoard.mLevel == 24 || mBoard.mLevel == 34 || mBoard.mLevel == 44))
                {
                    mApp.PlaySample(Resources.SOUND_TAP2);
                }
                else if (mType == CoinType.Trophy)
                {
                    mApp.PlaySample(Resources.SOUND_DIAMOND);
                    FanOutCoins(CoinType.Diamond, 1);
                }
                else if (mType == CoinType.AwardMoneyBag)
                {
                    int theNumCoins = 5;
                    mApp.PlayFoley(FoleyType.Coin);
                    FanOutCoins(CoinType.Gold, theNumCoins);
                }
                else
                {
                    mApp.PlaySample(Resources.SOUND_SEEDLIFT);
                    mApp.PlaySample(Resources.SOUND_TAP2);
                }
                mApp.AddTodParticle(mPosX + 30f, mPosY + 30f, mRenderOrder + 1, ParticleEffect.Starburst);
                Debug.ASSERT(mBoard != null);
                mBoard.FadeOutLevel();
                GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.SeedPacket, null);
                GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.AwardPickupArrow, null);
                GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.CoinPickupArrow, null);
                if (mType == CoinType.Note)
                {
                    mApp.AddTodParticle(mPosX + Constants.InvertAndScale(30f), mPosY + Constants.InvertAndScale(30f), mRenderOrder + 1, ParticleEffect.PresentPickup);
                    StartFade();
                }
                else if (!flag && mApp.Is3DAccelerated() && !mApp.IsQuickPlayMode())
                {
                    float num = mWidth / 2;
                    float num2 = mHeight / 2;
                    TodParticleSystem theParticleSystem = mApp.AddTodParticle(mPosX + num, mPosY + num2, mRenderOrder - 1, ParticleEffect.SeedPacketPickup);
                    GlobalMembersAttachment.AttachParticle(ref mAttachmentID, theParticleSystem, num, num2);
                }
                mDisappearCounter = 0;
                return;
            }
            if (mType == CoinType.UsableSeedPacket)
            {
                Debug.ASSERT(mBoard != null);
                //
                mBoard.ClearCursor();
                //
                mBoard.mCursorObject.mType = mUsableSeedType;
                mBoard.mCursorObject.mCursorType = CursorType.PlantFromUsableCoin;
                mBoard.mCursorObject.mCoinID = mBoard.mCoins[mBoard.mCoins.IndexOf(this)];
                mGroundY = (int)mPosY;
                mFadeCount = 0;
                mBoard.mIgnoreNextMouseUpSeedPacket = true;
                return;
            }
            if (IsMoney() && mBoard != null)
            {
                mBoard.ShowCoinBank();
            }
            mFadeCount = 0;
            if (IsSun() && mBoard != null && !mBoard.HasConveyorBeltSeedBank())
            {
                for (int i = 0; i < mBoard.mSeedBank.mNumPackets; i++)
                {
                    SeedPacket seedPacket = mBoard.mSeedBank.mSeedPackets[i];
                    int currentPlantCost = mBoard.GetCurrentPlantCost(seedPacket.mPacketType, seedPacket.mImitaterType);
                    int num3 = mBoard.mSunMoney + mBoard.CountSunBeingCollected() - currentPlantCost;
                    if (num3 >= 0 && num3 < GetSunValue())
                    {
                        seedPacket.FlashIfReady();
                    }
                }
                if (mBoard.StageHasFog())
                {
                    mRenderOrder = Board.MakeRenderOrder(RenderLayer.AboveUI, 0, 2);
                }
            }
            GlobalMembersAttachment.AttachmentDetachCrossFadeParticleType(ref mAttachmentID, ParticleEffect.CoinPickupArrow, null);
            if (mApp.IsFirstTimeAdventureMode() && mBoard != null && mBoard.mLevel == 11 && (mType == CoinType.Gold || mType == CoinType.Silver))
            {
                mBoard.DisplayAdvice("[ADVICE_CLICKED_ON_COIN]", MessageStyle.HintFast, AdviceType.ClickedOnCoin);
            }
        }

        public int GetSunValue()
        {
            if (mType == CoinType.Sun)
            {
                return 25;
            }
            if (mType == CoinType.Smallsun)
            {
                return 15;
            }
            if (mType == CoinType.Largesun)
            {
                return 50;
            }
            return 0;
        }

        public static int GetCoinValue(CoinType theType)
        {
            if (theType == CoinType.Silver)
            {
                return 1;
            }
            if (theType == CoinType.Gold)
            {
                return 5;
            }
            if (theType == CoinType.Diamond)
            {
                return 100;
            }
            return 0;
        }

        private void UpdateFade()
        {
            if (!mApp.IsEndlessIZombie(mApp.mGameMode) && !mApp.IsEndlessScaryPotter(mApp.mGameMode) && mType != CoinType.Note && IsLevelAward())
            {
                return;
            }
            mFadeCount -= 1;
            if (mFadeCount >= 0 && mFadeCount < 3)
            {
                if (mType == CoinType.Silver || mType == CoinType.Gold)
                {
                    mBoard.mCollectedCoinStreak = 0;
                }
                Die();
            }
        }

        private static void CheckRange_X(ref float thePosX, int theWidth, CoinMotion theCoinMotion)
        {
            if (thePosX > 800 - theWidth && theCoinMotion != CoinMotion.FromBoss)
            {
                thePosX = 800 - theWidth;
                return;
            }
            if (thePosX < 0f)
            {
                thePosX = 0f;
            }
        }

        private void UpdateFall()
        {
            if (IsPresentWithAdvice())
            {
                mDisappearCounter = 0;
                if (mCoinAge > 500)
                {
                    Collect();
                }
            }
            if (mCoinMotion == CoinMotion.FromPresent)
            {
                mPosX += 3f * mVelX;
                mPosY += 3f * mVelY;
                mVelX *= 0.857f;
                mVelY *= 0.857f;
                if (mCoinAge >= 80)
                {
                    Collect();
                }
            }
            else if (mPosY + mVelY < mGroundY)
            {
                mPosY += 3f * mVelY;
                if (mCoinMotion == CoinMotion.FromPlant)
                {
                    mVelY += 3f * Constants.InvertAndScale(0.09f);
                }
                else if (mCoinMotion == CoinMotion.Coin || mCoinMotion == CoinMotion.FromBoss)
                {
                    mVelY += 3f * Constants.InvertAndScale(0.15f);
                }
                mPosX += 3f * mVelX;
                if (mPosX > 800 - mWidth && mCoinMotion != CoinMotion.FromBoss)
                {
                    mPosX = 800 - mWidth;
                    mVelX = Constants.InvertAndScale(-0.4f - TodCommon.RandRangeFloat(0f, 0.4f));
                }
                else if (mPosX < 0f)
                {
                    mPosX = 0f;
                    mVelX = Constants.InvertAndScale(0.4f + TodCommon.RandRangeFloat(0f, 0.4f));
                }
            }
            else
            {
                if (mNeedsBouncyArrow && !mHasBouncyArrow)
                {
                    float num = mWidth / 2;
                    float num2 = mHeight / 2 - Constants.InvertAndScale(60f);
                    if (mType == CoinType.Trophy)
                    {
                        num += Constants.InvertAndScale(2f);
                    }
                    else if (mType == CoinType.AwardMoneyBag || mType == CoinType.AwardBagDiamond)
                    {
                        num += Constants.Coin_MoneyBag_Offset.X;
                        num2 += Constants.Coin_MoneyBag_Offset.Y;
                    }
                    else if (mType == CoinType.AwardPresent || IsPresentWithAdvice())
                    {
                        num2 += Constants.InvertAndScale(-20f);
                    }
                    else if (IsMoney())
                    {
                        num += Constants.Coin_Silver_Award_Offset.X;
                        num2 += Constants.Coin_Silver_Award_Offset.Y;
                    }
                    else if (mType == CoinType.Note)
                    {
                        num += Constants.Coin_Note_Offset.X;
                        num2 += Constants.Coin_Note_Offset.Y;
                    }
                    else if (mType == CoinType.Almanac)
                    {
                        num += Constants.Coin_Almanac_Offset.X;
                        num2 += Constants.Coin_Almanac_Offset.Y;
                    }
                    else if (mType == CoinType.Shovel)
                    {
                        num += Constants.Coin_Shovel_Offset.X;
                        num2 += Constants.Coin_Shovel_Offset.Y;
                    }
                    else if (mType == CoinType.Carkeys)
                    {
                        num += Constants.Coin_CarKeys_Offset.X;
                        num2 += Constants.Coin_CarKeys_Offset.Y;
                    }
                    else if (mType == CoinType.Taco)
                    {
                        num += Constants.Coin_Taco_Offset.X;
                        num2 += Constants.Coin_Taco_Offset.Y;
                    }
                    else if (mType == CoinType.Bacon)
                    {
                        num += Constants.Coin_Bacon_Offset.X;
                        num2 += Constants.Coin_Bacon_Offset.Y;
                    }
                    ParticleEffect theEffect;
                    if (mType == CoinType.FinalSeedPacket)
                    {
                        theEffect = ParticleEffect.SeedPacket;
                    }
                    else if (IsMoney())
                    {
                        theEffect = ParticleEffect.CoinPickupArrow;
                    }
                    else
                    {
                        theEffect = ParticleEffect.AwardPickupArrow;
                    }
                    TodParticleSystem theParticleSystem = mApp.AddTodParticle(mPosX + num, mPosY + num2, 0, theEffect);
                    GlobalMembersAttachment.AttachParticle(ref mAttachmentID, theParticleSystem, num, num2);
                    mHasBouncyArrow = true;
                }
                if (!mHitGround)
                {
                    mHitGround = true;
                    PlayGroundSound();
                }
                mPosY = mGroundY;
                mPosX = TodCommon.FloatRoundToInt(mPosX);
                if ((mApp.mGameMode != GameMode.ChallengeLastStand || mBoard == null || mBoard.mChallenge.mChallengeState == ChallengeState.LastStandOnslaught) && !IsLevelAward() && !IsPresentWithAdvice())
                {
                    mDisappearCounter += 3;
                    int disappearTime = GetDisappearTime();
                    if (mDisappearCounter >= disappearTime)
                    {
                        StartFade();
                    }
                }
            }
            if (mCoinMotion == CoinMotion.FromPlant)
            {
                float sunScale = GetSunScale();
                if (mScale < sunScale)
                {
                    mScale += 0.06f;
                    return;
                }
                mScale = sunScale;
            }
        }

        public override bool SaveToFile(Sexy.Buffer b)
        {
            try
            {
                base.SaveToFile(b);
                b.WriteLong(mCoinAge);
                b.WriteLong((int)mCoinMotion);
                b.WriteFloat(mCollectionDistance);
                b.WriteFloat(mCollectX);
                b.WriteFloat(mCollectY);
                b.WriteBoolean(mDead);
                b.WriteLong(mDisappearCounter);
                b.WriteLong(mFadeCount);
                b.WriteLong(mGroundY);
                b.WriteBoolean(mHitGround);
                b.WriteBoolean(mIsBeingCollected);
                b.WriteBoolean(mNeedsBouncyArrow);
                b.WriteFloat(mPosX);
                b.WriteFloat(mPosY);
                b.WriteBoolean(mPottedPlantSpec != null);
                if (mPottedPlantSpec != null)
                {
                    mPottedPlantSpec.Save(b);
                }
                b.WriteLong(mRow);
                b.WriteFloat(mScale);
                b.WriteLong(mTimesDropped);
                b.WriteLong((int)mType);
                b.WriteLong((int)mUsableSeedType);
                b.WriteFloat(mVelX);
                b.WriteFloat(mVelY);
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
                mCoinAge = b.ReadLong();
                mCoinMotion = (CoinMotion)b.ReadLong();
                mCollectionDistance = b.ReadFloat();
                mCollectX = b.ReadFloat();
                mCollectY = b.ReadFloat();
                mDead = b.ReadBoolean();
                mDisappearCounter = b.ReadLong();
                mFadeCount = b.ReadLong();
                mGroundY = b.ReadLong();
                mHitGround = b.ReadBoolean();
                mIsBeingCollected = b.ReadBoolean();
                mNeedsBouncyArrow = b.ReadBoolean();
                mPosX = b.ReadFloat();
                mPosY = b.ReadFloat();
                bool flag = b.ReadBoolean();
                if (flag)
                {
                    mPottedPlantSpec = new PottedPlant();
                    mPottedPlantSpec.Load(b);
                }
                else
                {
                    mPottedPlantSpec = null;
                }
                mRow = b.ReadLong();
                mScale = b.ReadFloat();
                mTimesDropped = b.ReadLong();
                mType = (CoinType)b.ReadLong();
                mUsableSeedType = (SeedType)b.ReadLong();
                mVelX = b.ReadFloat();
                mVelY = b.ReadFloat();
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
            Die();
            if (IsSun())
            {
                int sunValue = GetSunValue();
                mBoard.AddSunMoney(sunValue);
            }
            else if (IsMoney())
            {
                int coinValue = Coin.GetCoinValue(mType);
                mApp.mPlayerInfo.AddCoins(coinValue);
                if (mBoard != null)
                {
                    mBoard.mCoinsCollected += coinValue;
                }
            }
            if (mType == CoinType.Diamond && mBoard != null)
            {
                mBoard.mDiamondsCollected++;
            }
        }

        public void UpdateCollected()
        {
            int num;
            int num2;
            if (IsSun())
            {
                num = Constants.Board_SunCoin_CollectTarget.X;
                num2 = Constants.Board_SunCoin_CollectTarget.Y;
            }
            else if (IsMoney())
            {
                num = 130 - Constants.Board_Offset_AspectRatio_Correction;
                num2 = 550;
                if (mApp.GetDialog(4) != null)
                {
                    num = 662;
                    num2 = 546;
                }
                else if (mApp.mCrazyDaveState != CrazyDaveState.Off || mApp.mGameMode == GameMode.ChallengeZenGarden)
                {
                    num = Constants.ZenGarden_MoneyTarget_X;
                }
            }
            else if (IsPresentWithAdvice())
            {
                num = 35;
                num2 = 487;
            }
            else
            {
                if (mType == CoinType.AwardPresent || mType == CoinType.PresentPlant)
                {
                    mDisappearCounter += 3;
                    if (mDisappearCounter >= 200)
                    {
                        StartFade();
                    }
                    return;
                }
                if (!IsLevelAward())
                {
                    if (mType == CoinType.UsableSeedPacket)
                    {
                        mDisappearCounter += 3;
                    }
                    return;
                }
                num = Constants.Coin_AwardSeedpacket_Pos.X - mWidth / 2;
                num2 = Constants.Coin_AwardSeedpacket_Pos.Y - mHeight / 2;
                mDisappearCounter += 3;
            }
            if (IsLevelAward())
            {
                mScale = TodCommon.TodAnimateCurveFloat(0, 400, mDisappearCounter, 1.01f, 2f, TodCurves.EaseInOut);
                mPosX = TodCommon.TodAnimateCurveFloat(0, 350, mDisappearCounter, mCollectX, num, TodCurves.EaseOut);
                mPosY = TodCommon.TodAnimateCurveFloat(0, 350, mDisappearCounter, mCollectY, num2, TodCurves.EaseOut);
                return;
            }
            float num3 = Math.Abs(mPosX - num);
            float num4 = Math.Abs(mPosY - num2);
            if (mPosX > num)
            {
                mPosX -= num3 / 7f;
            }
            else if (mPosX < num)
            {
                mPosX += num3 / 7f;
            }
            if (mPosY > num2)
            {
                mPosY -= num4 / 7f;
            }
            else if (mPosY < num2)
            {
                mPosY += num4 / 7f;
            }
            mCollectionDistance = (float)Math.Sqrt(num4 * num4 + num3 * num3);
            if (IsPresentWithAdvice())
            {
                if (mCollectionDistance < Constants.InvertAndScale(15f))
                {
                    if (!mBoard.mHelpDisplayed[64])
                    {
                        if (mType == CoinType.PresentMinigames)
                        {
                            mBoard.DisplayAdvice("[UNLOCKED_MINIGAMES]", MessageStyle.HintTallUnlockmessage, AdviceType.UnlockedMode);
                            return;
                        }
                        if (mType == CoinType.PresentPuzzleMode)
                        {
                            mBoard.DisplayAdvice("[UNLOCKED_PUZZLE_MODE]", MessageStyle.HintTallUnlockmessage, AdviceType.UnlockedMode);
                            return;
                        }
                    }
                    else if (mBoard.mHelpIndex != AdviceType.UnlockedMode || !mBoard.mAdvice.IsBeingDisplayed())
                    {
                        Die();
                    }
                }
                return;
            }
            float num5 = 8f;
            if (IsMoney())
            {
                num5 = 12f;
            }
            if (mCollectionDistance < num5 && !mScored)
            {
                ScoreCoin();
                mScored = true;
            }
            mScale = TodCommon.ClampFloat(mCollectionDistance * 0.05f, 0.5f, 1f);
            mScale *= GetSunScale();
        }

        public SexyColor GetColor()
        {
            if ((IsSun() || IsMoney()) && mIsBeingCollected)
            {
                float num = TodCommon.ClampFloat(mCollectionDistance * 0.035f, 0.35f, 1f);
                return new SexyColor(255, 255, 255, (int)(255f * num), false);
            }
            if (mFadeCount > 0)
            {
                int theAlpha = TodCommon.TodAnimateCurve(15, 0, mFadeCount, 255, 0, TodCurves.Linear);
                return new SexyColor(255, 255, 255, theAlpha, false);
            }
            return SexyColor.White;
        }

        public bool IsMoney()
        {
            return mType == CoinType.Silver || mType == CoinType.Gold || mType == CoinType.Diamond;
        }

        public bool IsSun()
        {
            return mType == CoinType.Sun || mType == CoinType.Smallsun || mType == CoinType.Largesun;
        }

        public float GetSunScale()
        {
            if (mType == CoinType.Smallsun)
            {
                return 0.5f;
            }
            if (mType == CoinType.Largesun)
            {
                return 2f;
            }
            return 1f;
        }

        public SeedType GetFinalSeedPacketType()
        {
            if (mApp.IsFirstTimeAdventureMode() && mBoard != null && mBoard.mLevel <= 50)
            {
                return mApp.GetAwardSeedForLevel(mBoard.mLevel);
            }
            return SeedType.None;
        }

        public bool IsLevelAward()
        {
            return mType == CoinType.FinalSeedPacket || mType == CoinType.Trophy || mType == CoinType.Shovel || mType == CoinType.Carkeys || mType == CoinType.Almanac || mType == CoinType.Taco || mType == CoinType.Bacon || mType == CoinType.Note || mType == CoinType.AwardMoneyBag || mType == CoinType.AwardBagDiamond || mType == CoinType.AwardPresent || mType == CoinType.WateringCan || mType == CoinType.AwardChocolate;
        }

        public bool CoinGetsBouncyArrow()
        {
            return IsLevelAward() || ((mType == CoinType.Silver || mType == CoinType.Gold) && mApp.IsFirstTimeAdventureMode() && mBoard != null && mBoard.mLevel == 11 && !mBoard.mDroppedFirstCoin) || IsPresentWithAdvice();
        }

        public void FanOutCoins(CoinType theCoinType, int theNumCoins)
        {
            Debug.ASSERT(mBoard != null);
            for (int i = 0; i < theNumCoins; i++)
            {
                float num = 1.5707964f + 3.1415927f * (i + 1) / (theNumCoins + 1);
                float num2 = mPosX + 20f;
                float num3 = mPosY;
                Coin coin = mBoard.AddCoin((int)num2, (int)num3, theCoinType, CoinMotion.FromPresent);
                coin.mVelX = 5f * (float)Math.Sin(num);
                coin.mVelY = 5f * (float)Math.Cos(num);
            }
        }

        public int GetDisappearTime()
        {
            int result = 750;
            if (mType == CoinType.Diamond || mType == CoinType.Chocolate || mHasBouncyArrow || mType == CoinType.PresentPlant)
            {
                result = 1500;
            }
            if ((mApp.IsScaryPotterLevel() || mApp.IsSlotMachineLevel()) && mType == CoinType.UsableSeedPacket)
            {
                result = 1500;
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                result = 6000;
            }
            return result;
        }

        public void DroppedUsableSeed()
        {
            mIsBeingCollected = false;
            if (mTimesDropped == 0)
            {
                mDisappearCounter = Math.Min(mDisappearCounter, 1200);
            }
            mTimesDropped++;
        }

        public void PlayCollectSound()
        {
            if (mType == CoinType.UsableSeedPacket)
            {
                mApp.PlaySample(Resources.SOUND_SEEDLIFT);
                return;
            }
            if (mType == CoinType.Silver || mType == CoinType.Gold)
            {
                mApp.PlayFoley(FoleyType.Coin);
                return;
            }
            if (mType == CoinType.Diamond)
            {
                mApp.PlaySample(Resources.SOUND_DIAMOND);
                return;
            }
            if (IsSun())
            {
                mApp.PlayFoley(FoleyType.Sun);
                return;
            }
            if (mType == CoinType.Chocolate || IsPresentWithAdvice() || mType == CoinType.AwardPresent || mType == CoinType.AwardChocolate || mType == CoinType.PresentPlant)
            {
                mApp.PlayFoley(FoleyType.Prize);
                return;
            }
            if (IsSun())
            {
                mApp.PlayFoley(FoleyType.Sun);
            }
        }

        public void TryAutoCollectAfterLevelAward()
        {
            bool flag = false;
            if (IsMoney() && mCoinMotion != CoinMotion.FromPresent)
            {
                flag = true;
            }
            else if (IsSun())
            {
                flag = true;
            }
            else if (mType == CoinType.Chocolate || IsPresentWithAdvice() || mType == CoinType.PresentPlant)
            {
                flag = true;
            }
            if (flag)
            {
                PlayCollectSound();
                Collect();
            }
        }

        public bool IsPresentWithAdvice()
        {
            return mType == CoinType.PresentMinigames || mType == CoinType.PresentPuzzleMode;
        }

        public void PlayGroundSound()
        {
            if (mType == CoinType.Gold)
            {
                mApp.PlayFoley(FoleyType.Moneyfalls);
                return;
            }
            if (mType != CoinType.PresentPlant && mType != CoinType.Diamond && mType != CoinType.Chocolate && mType != CoinType.AwardChocolate && mType != CoinType.AwardPresent)
            {
                if (IsPresentWithAdvice())
                {
                    return;
                }
                IsLevelAward();
            }
        }

        public void PlayLaunchSound()
        {
            if (mType == CoinType.PresentPlant || mType == CoinType.Diamond || mType == CoinType.Chocolate || mType == CoinType.AwardChocolate || mType == CoinType.AwardPresent || IsPresentWithAdvice())
            {
                mApp.PlayFoley(FoleyType.Chime);
            }
        }

        public void Loaded()
        {
            if (mType == CoinType.FinalSeedPacket)
            {
                Coin.LoadSeedPacketImage(GetFinalSeedPacketType());
            }
        }

        public override void LoadingComplete()
        {
            base.LoadingComplete();
            Loaded();
            Coin coin = new Coin();
            coin.CoinInitialize(mX, mY, mType, mCoinMotion);
            mAttachmentID = coin.mAttachmentID;
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
