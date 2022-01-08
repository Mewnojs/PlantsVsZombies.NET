using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class CursorObject : GameObject
    {
        public CursorObject()
        {
            mType = SeedType.SEED_NONE;
            mImitaterType = SeedType.SEED_NONE;
            mSeedBankIndex = -1;
            mX = 0;
            mY = 0;
            mCursorType = CursorType.CURSOR_TYPE_NORMAL;
            mCoinID = null;
            mDuplicatorPlantID = null;
            mCobCannonPlantID = null;
            mGlovePlantID = null;
            mReanimCursorID = null;
            mPosScaled = false;
            if (mApp.IsWhackAZombieLevel())
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_HAMMER, true);
                Reanimation reanimation = mApp.AddReanimation(-25f, 16f, 0, ReanimationType.REANIM_HAMMER);
                reanimation.mIsAttachment = true;
                reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_whack_zombie, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
                reanimation.mAnimTime = 1f;
                mReanimCursorID = mApp.ReanimationGetID(reanimation);
            }
            mWidth = 80;
            mHeight = 80;
        }

        public void Update()//3update
        {
            if (mApp.mGameScene != GameScenes.SCENE_PLAYING && !mBoard.mCutScene.IsInShovelTutorial())
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
            if (mCursorType != CursorType.CURSOR_TYPE_NORMAL)
            {
                int theX = (int)(mX * Constants.IS);
                int theY = (int)(mY * Constants.IS);
                int num;
                int num2;
                if (mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_BANK)
                {
                    if (mBoard.mIgnoreMouseUp)
                    {
                        return;
                    }
                    num = mBoard.PlantingPixelToGridX(theX, theY, mType);
                    num2 = mBoard.PlantingPixelToGridY(theX, theY, mType);
                    if (mBoard.CanPlantAt(num, num2, mType) != PlantingReason.PLANTING_OK)
                    {
                        return;
                    }
                }
                else
                {
                    num = mBoard.PixelToGridX(theX, theY);
                    num2 = mBoard.PixelToGridY(theX, theY);
                    if (mCursorType == CursorType.CURSOR_TYPE_SHOVEL && (mBoard.mIgnoreMouseUp || mBoard.ToolHitTest(mX, mY, false).mObjectType == GameObjectType.OBJECT_TYPE_NONE))
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
            switch (mCursorType)
            {
                case CursorType.CURSOR_TYPE_SHOVEL:
                    if (mBoard.mIgnoreMouseUp || mX * Constants.IS < Constants.LAWN_XMIN || mY * Constants.IS < Constants.LAWN_YMIN)
                    {
                        return;
                    }
                    int imgShovelWidth = AtlasResources.IMAGE_SHOVEL_HI_RES.mWidth;
                    int imgShovelHeight = AtlasResources.IMAGE_SHOVEL_HI_RES.mHeight;
                    g.SetColor(SexyColor.White);
                    int num = (int)TodCommon.TodAnimateCurveFloat(0, 39, mBoard.mEffectCounter % 40, 0f, imgShovelWidth / 2, TodCurves.CURVE_BOUNCE_SLOW_MIDDLE);
                    g.DrawImage(AtlasResources.IMAGE_SHOVEL_HI_RES, num, -imgShovelHeight - num, imgShovelWidth, imgShovelHeight);
                    return;
                case CursorType.CURSOR_TYPE_HAMMER:
                    Reanimation reanimation = mApp.ReanimationGet(mReanimCursorID);
                    reanimation.Draw(g);
                    return;
                case CursorType.CURSOR_TYPE_COBCANNON_TARGET:
                    if (mX * Constants.IS < Constants.LAWN_XMIN || mY * Constants.IS < Constants.LAWN_YMIN)
                    {
                        return;
                    }
                    int imgCobCannonTargetWidth = AtlasResources.IMAGE_COBCANNON_TARGET.mWidth;
                    int imgCobCannonTargetHeight = AtlasResources.IMAGE_COBCANNON_TARGET.mHeight;
                    g.SetColorizeImages(true);
                    g.SetColor(new SexyColor(255, 255, 255, 127));
                    g.DrawImage(AtlasResources.IMAGE_COBCANNON_TARGET, -imgCobCannonTargetWidth / 2, -imgCobCannonTargetHeight / 2, imgCobCannonTargetWidth, imgCobCannonTargetHeight);
                    g.SetColorizeImages(false);
                    return;
                case CursorType.CURSOR_TYPE_WATERING_CAN:
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
                case CursorType.CURSOR_TYPE_BUG_SPRAY:
                    DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_BUGSPRAY_BOTTLE);
                    return;
                case CursorType.CURSOR_TYPE_PHONOGRAPH:
                    DrawToolIconImage(g, AtlasResources.IMAGE_PHONOGRAPH);
                    return;
                case CursorType.CURSOR_TYPE_FERTILIZER:
                    DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_FERTILIZER_BAG1);
                    return;
                case CursorType.CURSOR_TYPE_GLOVE:
                    DrawToolIconImage(g, AtlasResources.IMAGE_ZEN_GARDENGLOVE);
                    return;
                case CursorType.CURSOR_TYPE_CHOCOLATE:
                    DrawToolIconImage(g, AtlasResources.IMAGE_CHOCOLATE);
                    return;
                case CursorType.CURSOR_TYPE_MONEY_SIGN:
                    DrawToolIconImage(g, AtlasResources.IMAGE_ZEN_MONEYSIGN);
                    return;
                case CursorType.CURSOR_TYPE_WHEEELBARROW:
                    Image wbimage = AtlasResources.IMAGE_ZEN_WHEELBARROW;
                    DrawToolIconImage(g, wbimage);
                    PottedPlant wbplant = mApp.mZenGarden.GetPottedPlantInWheelbarrow();
                    if (wbplant != null)
                    {
                        mApp.mZenGarden.DrawPottedPlant(g, -wbimage.mWidth / 2 + (float)(Constants.ZenGardenButton_WheelbarrowPlant_Offset.X), -wbimage.mHeight / 2 + (float)(Constants.ZenGardenButton_WheelbarrowPlant_Offset.Y), wbplant, 0.6f, true);
                    }
                    return;
                case CursorType.CURSOR_TYPE_PLANT_FROM_BANK:
                case CursorType.CURSOR_TYPE_PLANT_FROM_USABLE_COIN:
                    if (mBoard.mIgnoreMouseUp || mX * Constants.IS < Constants.LAWN_XMIN || mY * Constants.IS < Constants.LAWN_YMIN)
                    {
                        return;
                    }
                    if (mType == SeedType.SEED_NONE) 
                    {
                        return;
                    }
                    int xPos = (int)(20 * Constants.S);
                    int yPos = (int)(20 * Constants.S);
                    if (Challenge.IsZombieSeedType(mType))
                    {
                        xPos = (int)(-40 * Constants.S);
                        yPos = (int)(-90 * Constants.S);
                        ZombieType theZombieType = Challenge.IZombieSeedTypeToZombieType(mType);
                        GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedZombie(g, xPos, yPos, theZombieType);
                        return;
                    }
                    GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedPlant(g, xPos, yPos, mType, DrawVariation.VARIATION_NORMAL);
                    return;

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
