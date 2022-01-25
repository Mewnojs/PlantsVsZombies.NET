using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class CursorObject : GameObject
    {
        public CursorObject()
        {
            mType = SeedType.None;
            mImitaterType = SeedType.None;
            mSeedBankIndex = -1;
            mX = 0;
            mY = 0;
            mCursorType = CursorType.Normal;
            mCoinID = null;
            mDuplicatorPlantID = null;
            mCobCannonPlantID = null;
            mGlovePlantID = null;
            mReanimCursorID = null;
            mPosScaled = false;
            if (mApp.IsWhackAZombieLevel())
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.Hammer, true);
                Reanimation reanimation = mApp.AddReanimation(-25f, 16f, 0, ReanimationType.Hammer);
                reanimation.mIsAttachment = true;
                reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_whack_zombie, ReanimLoopType.PlayOnceAndHold, 0, 24f);
                reanimation.mAnimTime = 1f;
                mReanimCursorID = mApp.ReanimationGetID(reanimation);
            }
            mWidth = 80;
            mHeight = 80;
        }

        public void Update()
        {
            if (mApp.mGameScene != GameScenes.Playing && !mBoard.mCutScene.IsInShovelTutorial())
            {
                mVisible = false;
                return;
            }
            if (!mApp.mWidgetManager.mMouseIn)
            {
                mVisible = false;
                return;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mReanimCursorID);
            if (reanimation != null)
            {
                reanimation.Update();
            }
            mVisible = true;
            mX = mBoard.mLastToolX;
            mY = mBoard.mLastToolY;
        }

        public override bool LoadFromFile(Sexy.Buffer b)
        {
            base.LoadFromFile(b);
            mCursorType = (CursorType)b.ReadLong();
            mHammerDownCounter = b.ReadLong();
            mImitaterType = (SeedType)b.ReadLong();
            mSeedBankIndex = b.ReadLong();
            mType = (SeedType)b.ReadLong();
            return true;
        }

        public override bool SaveToFile(Sexy.Buffer b)
        {
            base.SaveToFile(b);
            b.WriteLong((int)mCursorType);
            b.WriteLong(mHammerDownCounter);
            b.WriteLong((int)mImitaterType);
            b.WriteLong(mSeedBankIndex);
            b.WriteLong((int)mType);
            return true;
        }

        public void DrawGroundLayer(Graphics g)
        {
            if (mCursorType != CursorType.Normal)
            {
                int theX = (int)(mX * Constants.IS);
                int theY = (int)(mY * Constants.IS);
                int num;
                int num2;
                if (mCursorType == CursorType.PlantFromBank)
                {
                    if (mBoard.mIgnoreMouseUp)
                    {
                        return;
                    }
                    num = mBoard.PlantingPixelToGridX(theX, theY, mType);
                    num2 = mBoard.PlantingPixelToGridY(theX, theY, mType);
                    if (mBoard.CanPlantAt(num, num2, mType) != PlantingReason.Ok)
                    {
                        return;
                    }
                }
                else
                {
                    num = mBoard.PixelToGridX(theX, theY);
                    num2 = mBoard.PixelToGridY(theX, theY);
                    if (mCursorType == CursorType.Shovel && (mBoard.mIgnoreMouseUp || mBoard.ToolHitTest(mX, mY, false).mObjectType == GameObjectType.None))
                    {
                        return;
                    }
                }
                if (num2 < 0 || num < 0 || mBoard.GridToPixelY(num, num2) < 0)
                {
                    return;
                }
                Graphics @new = Graphics.GetNew();
                @new.mTransX = Constants.Board_Offset_AspectRatio_Correction;
                for (int i = 0; i < Constants.GRIDSIZEX; i++)
                {
                    mBoard.DrawCelHighlight(@new, i, num2);
                }
                int num3 = mBoard.StageHas6Rows() ? 6 : 5;
                for (int j = 0; j < num3; j++)
                {
                    if (j != num2)
                    {
                        mBoard.DrawCelHighlight(@new, num, j);
                    }
                }
                @new.PrepareForReuse();
            }
        }

        public void DrawToolIconImage(Graphics g, Image image)
        {
            g.DrawImage(image, -image.mWidth / 2, -image.mHeight / 2, image.mWidth, image.mHeight);
        }

        public void DrawTopLayer(Graphics g)
        {
            bool flag = mBoard.HasConveyorBeltSeedBank();
            if (flag)
            {
                g.SetClipRect(new TRect(-mX - Constants.Board_Offset_AspectRatio_Correction + (int)(Constants.New.SeedBank_Width), -mY, short.MaxValue, short.MaxValue));
                g.HardwareClip();
            }
            switch (mCursorType)
            {
            case CursorType.Shovel:
                if (mBoard.mIgnoreMouseUp /*|| mX * Constants.IS < Constants.LAWN_XMIN || mY * Constants.IS < Constants.LAWN_YMIN*/)
                {
                    break;
                }
                int imgShovelWidth = AtlasResources.IMAGE_SHOVEL_HI_RES.mWidth;
                int imgShovelHeight = AtlasResources.IMAGE_SHOVEL_HI_RES.mHeight;
                g.SetColor(SexyColor.White);
                int num = (int)TodCommon.TodAnimateCurveFloat(0, 39, mBoard.mEffectCounter % 40, 0f, imgShovelWidth / 2, TodCurves.BounceSlowMiddle);
                g.DrawImage(AtlasResources.IMAGE_SHOVEL_HI_RES, num, -imgShovelHeight - num, imgShovelWidth, imgShovelHeight);
                break;
            case CursorType.Hammer:
                Reanimation reanimation = mApp.ReanimationGet(mReanimCursorID);
                reanimation.Draw(g);
                break;
            case CursorType.CobcannonTarget:
                if (/*mX * Constants.IS < Constants.LAWN_XMIN ||mY * Constants.IS < Constants.LAWN_YMIN*/false)
                {
                    break;
                }
                int imgCobCannonTargetWidth = AtlasResources.IMAGE_COBCANNON_TARGET.mWidth;
                int imgCobCannonTargetHeight = AtlasResources.IMAGE_COBCANNON_TARGET.mHeight;
                g.SetColorizeImages(true);
                g.SetColor(new SexyColor(255, 255, 255, 127));
                g.DrawImage(AtlasResources.IMAGE_COBCANNON_TARGET, -imgCobCannonTargetWidth / 2, -imgCobCannonTargetHeight / 2, imgCobCannonTargetWidth, imgCobCannonTargetHeight);
                g.SetColorizeImages(false);
                break;
            case CursorType.WateringCan:
                if (mApp.mPlayerInfo.mPurchases[13] > 0)
                {
                    TRect trect = new TRect(Constants.ZEN_XMIN, Constants.ZEN_YMIN, Constants.ZEN_XMAX - Constants.ZEN_XMIN, Constants.ZEN_YMAX - Constants.ZEN_YMIN);
                    if (trect.Contains(mApp.mBoard.mLastToolX, mApp.mBoard.mLastToolY))
                    {
                        int imgGoldToolReticleWidth = AtlasResources.IMAGE_ZEN_GOLDTOOLRETICLE.mWidth;
                        int imgGoldToolReticleHeight = AtlasResources.IMAGE_ZEN_GOLDTOOLRETICLE.mHeight;
                        g.DrawImage(AtlasResources.IMAGE_ZEN_GOLDTOOLRETICLE, -imgGoldToolReticleWidth / 2, -imgGoldToolReticleHeight / 2, imgGoldToolReticleWidth, imgGoldToolReticleHeight);
                    }
                    DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1_GOLD);
                }
                else
                {
                    DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1);
                }
                break;
            case CursorType.BugSpray:
                DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_BUGSPRAY_BOTTLE);
                break;
            case CursorType.Phonograph:
                DrawToolIconImage(g, AtlasResources.IMAGE_PHONOGRAPH);
                break;
            case CursorType.Fertilizer:
                DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_FERTILIZER_BAG1);
                break;
            case CursorType.Glove:
                DrawToolIconImage(g, AtlasResources.IMAGE_ZEN_GARDENGLOVE);
                break;
            case CursorType.Chocolate:
                DrawToolIconImage(g, AtlasResources.IMAGE_CHOCOLATE);
                break;
            case CursorType.MoneySign:
                DrawToolIconImage(g, AtlasResources.IMAGE_ZEN_MONEYSIGN);
                break;
            case CursorType.Wheeelbarrow:
                Image wbimage = AtlasResources.IMAGE_ZEN_WHEELBARROW;
                DrawToolIconImage(g, wbimage);
                PottedPlant wbplant = mApp.mZenGarden.GetPottedPlantInWheelbarrow();
                if (wbplant != null)
                {
                    mApp.mZenGarden.DrawPottedPlant(g, -wbimage.mWidth / 2 + (float)(Constants.ZenGardenButton_WheelbarrowPlant_Offset.X), -wbimage.mHeight / 2 + (float)(Constants.ZenGardenButton_WheelbarrowPlant_Offset.Y), wbplant, 0.6f, true);
                }
                break;
            case CursorType.PlantFromBank:
            case CursorType.PlantFromUsableCoin:
                if (mBoard.mIgnoreMouseUp /*|| mX * Constants.IS < Constants.LAWN_XMIN || mY * Constants.IS < Constants.LAWN_YMIN*/)
                {
                    break;
                }
                if (mType == SeedType.None)
                {
                    break;
                }
                if (!flag && mX + Constants.Board_Offset_AspectRatio_Correction < Constants.SMALL_SEEDPACKET_WIDTH) 
                {
                    break;
                }
                float yPos = -Constants.New.Board_GridCellSizeY_6Rows / 2f * Constants.S;
                float xPos = -Constants.New.Board_GridCellSizeX / 2f * Constants.S;
                if (Challenge.IsZombieSeedType(mType))
                {
                    xPos = (int)(-40 * Constants.S);
                    yPos = (int)(-90 * Constants.S);
                    ZombieType theZombieType = Challenge.IZombieSeedTypeToZombieType(mType);
                    GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedZombie(g, xPos, yPos, theZombieType);
                    break;
                }
                GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedPlant(g, xPos, yPos, mType, DrawVariation.Normal);
                break;
            }
            if (flag)
            {
                g.EndHardwareClip();
            }
        }

        public void Die()
        {
            mApp.RemoveReanimation(ref mReanimCursorID);
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
