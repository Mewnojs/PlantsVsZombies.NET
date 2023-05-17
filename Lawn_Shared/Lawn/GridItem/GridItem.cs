using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class GridItem
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
            Reset();
        }

        private void Reset()
        {
            mGridItemState = GridItemState.Normal;
            for (int i = 0; i < mMotionTrailFrames.Length; i++)
            {
                mMotionTrailFrames[i] = new MotionTrailFrame();
            }
            mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
            mBoard = mApp.mBoard;
            mGridItemType = GridItemType.None;
            mGridX = 0;
            mGridY = 0;
            mGridItemCounter = 0;
            mRenderOrder = 0;
            mDead = false;
            mPosX = 0f;
            mPosY = 0f;
            mGoalX = 0f;
            mGoalY = 0f;
            mGridItemReanimID = null;
            mGridItemParticleID = null;
            mZombieType = ZombieType.Invalid;
            mSeedType = SeedType.None;
            mScaryPotType = ScaryPotType.None;
            mHighlighted = false;
            mTransparentCounter = 0;
            mSunCount = 0;
            mMotionTrailCount = 0;
        }

        public bool SaveToFile(Sexy.Buffer b)
        {
            try
            {
                b.WriteBoolean(mDead);
                b.WriteFloat(mGoalX);
                b.WriteFloat(mGoalY);
                b.WriteLong(mGridItemCounter);
                b.WriteLong((int)mGridItemState);
                b.WriteLong((int)mGridItemType);
                b.WriteLong(mGridX);
                b.WriteLong(mGridY);
                b.WriteBoolean(mHighlighted);
                b.WriteLong(mMotionTrailCount);
                for (int i = 0; i < mMotionTrailCount; i++)
                {
                    mMotionTrailFrames[i].SaveToFile(b);
                }
                b.WriteFloat(mPosX);
                b.WriteFloat(mPosY);
                b.WriteLong(mRenderOrder);
                b.WriteLong((int)mScaryPotType);
                b.WriteLong((int)mSeedType);
                b.WriteLong(mSunCount);
                b.WriteLong(mTransparentCounter);
                b.WriteLong((int)mZombieType);
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
                mDead = b.ReadBoolean();
                mGoalX = b.ReadFloat();
                mGoalY = b.ReadFloat();
                mGridItemCounter = b.ReadLong();
                mGridItemState = (GridItemState)b.ReadLong();
                mGridItemType = (GridItemType)b.ReadLong();
                mGridX = b.ReadLong();
                mGridY = b.ReadLong();
                mHighlighted = b.ReadBoolean();
                mMotionTrailCount = b.ReadLong();
                Array.Clear(mMotionTrailFrames, 0, mMotionTrailFrames.Length);
                for (int i = 0; i < mMotionTrailCount; i++)
                {
                    mMotionTrailFrames[i] = new MotionTrailFrame();
                    mMotionTrailFrames[i].LoadFromFile(b);
                }
                mPosX = b.ReadFloat();
                mPosY = b.ReadFloat();
                mRenderOrder = b.ReadLong();
                mScaryPotType = (ScaryPotType)b.ReadLong();
                mSeedType = (SeedType)b.ReadLong();
                mSunCount = b.ReadLong();
                mTransparentCounter = b.ReadLong();
                mZombieType = (ZombieType)b.ReadLong();
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
            mApp = GlobalStaticVars.gLawnApp;
            mBoard = mApp.mBoard;
            if (mGridItemType == GridItemType.Rake)
            {
                mGridItemReanimID = mBoard.CreateRakeReanim(mPosX, mPosY, mRenderOrder);
            }
            if (mGridItemType == GridItemType.PortalCircle || mGridItemType == GridItemType.PortalSquare)
            {
                OpenPortal();
            }
        }

        public void DrawLadder(Graphics g)
        {
            int num = mBoard.GridToPixelX(mGridX, mGridY);
            int num2 = mBoard.GridToPixelY(mGridX, mGridY);
            TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_REANIM_ZOMBIE_LADDER_5, (num + 25f) * Constants.S, (num2 - 4f) * Constants.S, 0.8f, 0.8f);
        }

        public void DrawCrater(Graphics g)
        {
            float num = mBoard.GridToPixelX(mGridX, mGridY) - 8f;
            float num2 = mBoard.GridToPixelY(mGridX, mGridY) + 40f;
            if (mGridItemCounter < 25)
            {
                int theAlpha = TodCommon.TodAnimateCurve(25, 0, mGridItemCounter, 255, 0, TodCurves.Linear);
                g.SetColor(new SexyColor(255, 255, 255, theAlpha));
                g.SetColorizeImages(true);
            }
            bool flag = mGridItemCounter < 9000;
            Image theImageStrip = AtlasResources.IMAGE_CRATER;
            int theCelCol = 0;
            if (mBoard.IsPoolSquare(mGridX, mGridY))
            {
                if (mBoard.StageIsNight())
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
                float num3 = mGridY * 3.1415927f + mGridX * 3.1415927f * 0.25f;
                float num4 = mBoard.mMainCounter * 3.1415927f * 2f / 200f;
                float num5 = (float)Math.Sin(num3 + num4) * 2f;
                num2 += num5;
            }
            else if (mBoard.StageHasRoof())
            {
                if (mGridX < 5)
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
            else if (mBoard.StageIsNight())
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
            if (mGridItemCounter <= 0)
            {
                return;
            }
            int theTimeAge = TodCommon.TodAnimateCurve(0, 100, mGridItemCounter, 1000, 0, TodCurves.EaseInOut);
            int num = mBoard.mGridCelLook[mGridX, mGridY];
            int num2 = mBoard.mGridCelOffset[mGridX, mGridY, 0];
            int num3 = mBoard.mGridCelOffset[mGridX, mGridY, 1];
            int num4 = (int)(AtlasResources.IMAGE_TOMBSTONES.GetCelWidth() * Constants.IS);
            int num5 = (int)(AtlasResources.IMAGE_TOMBSTONES.GetCelHeight() * Constants.IS);
            int num6 = num % 5;
            int num7;
            if (mGridY == 0)
            {
                num7 = 1;
            }
            else if (mGridItemState == GridItemState.GravestoneSpecial)
            {
                num7 = 0;
            }
            else
            {
                num7 = 2 + num % 2;
            }
            int num8 = TodCommon.TodAnimateCurve(0, 1000, theTimeAge, num5, 0, TodCurves.EaseInOut);
            int num9 = TodCommon.TodAnimateCurve(0, 50, theTimeAge, 0, 14, TodCurves.EaseInOut);
            int num10 = TodCommon.TodAnimateCurve(500, 1000, theTimeAge, num5, 0, TodCurves.EaseInOut);
            int num11 = 0;
            Plant topPlantAt = mBoard.GetTopPlantAt(mGridX, mGridY, TopPlant.OnlyNormalPosition);
            if (topPlantAt != null && topPlantAt.mState == PlantState.GravebusterEating)
            {
                num11 = TodCommon.TodAnimateCurve(400, 0, topPlantAt.mStateCountdown, 10, 40, TodCurves.Linear);
            }
            TRect theSrcRect = new TRect((int)(num4 * num6 * Constants.S), (int)((num5 * num7 + num11) * Constants.S), (int)(num4 * Constants.S), (int)((num8 - num9 - num11) * Constants.S));
            int num12 = mBoard.GridToPixelX(mGridX, mGridY) - 4 + num2;
            int num13 = mBoard.GridToPixelY(mGridX, mGridY) + num5 + num3;
            g.DrawImage(AtlasResources.IMAGE_TOMBSTONES, (int)(num12 * Constants.S), (int)((num13 - num8 + num11) * Constants.S + 0.5), theSrcRect);
            int num14 = (int)(num10 * Constants.S);
            if (num14 >= (int)Constants.InvertAndScale(34f))
            {
                TRect theSrcRect2 = new TRect(AtlasResources.IMAGE_TOMBSTONE_MOUNDS.GetCelWidth() * num6, AtlasResources.IMAGE_TOMBSTONE_MOUNDS.GetCelHeight() * num7, AtlasResources.IMAGE_TOMBSTONE_MOUNDS.GetCelWidth(), (int)(num10 * Constants.S) - (int)Constants.InvertAndScale(34f));
                g.DrawImage(AtlasResources.IMAGE_TOMBSTONE_MOUNDS, (int)(num12 * Constants.S), (int)((num13 - num10) * Constants.S) + (int)Constants.InvertAndScale(34f), theSrcRect2);
            }
        }

        public void GridItemDie()
        {
            mDead = true;
            Reanimation reanimation = mApp.ReanimationTryToGet(mGridItemReanimID);
            if (reanimation != null)
            {
                reanimation.ReanimationDie();
                mGridItemReanimID = null;
            }
            TodParticleSystem todParticleSystem = mApp.ParticleTryToGet(mGridItemParticleID);
            if (todParticleSystem != null)
            {
                todParticleSystem.ParticleSystemDie();
                mGridItemParticleID = null;
            }
        }

        public void AddGraveStoneParticles()
        {
            int num = mBoard.mGridCelOffset[mGridX, mGridY, 0];
            int num2 = mBoard.mGridCelOffset[mGridX, mGridY, 1];
            int num3 = mBoard.GridToPixelX(mGridX, mGridY) + 14 + num;
            int num4 = mBoard.GridToPixelY(mGridX, mGridY) + 78 + num2;
            mApp.AddTodParticle(num3, num4, mRenderOrder + 1, ParticleEffect.GraveStoneRise);
            mApp.PlayFoley(FoleyType.DirtRise);
        }

        public void DrawGridItem(Graphics g)
        {
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            switch (mGridItemType)
            {
            case GridItemType.Gravestone:
                DrawGraveStone(g);
                break;
            case GridItemType.Crater:
                DrawCrater(g);
                break;
            case GridItemType.Ladder:
                DrawLadder(g);
                break;
            case GridItemType.PortalCircle:
            case GridItemType.PortalSquare:
            case GridItemType.ZenTool:
            case GridItemType.Rake:
                break;
            case GridItemType.Brain:
                g.DrawImageF(AtlasResources.IMAGE_BRAIN, mPosX * Constants.S, mPosY * Constants.S);
                break;
            case GridItemType.ScaryPot:
                DrawScaryPot(g);
                break;
            case GridItemType.Squirrel:
                DrawSquirrel(g);
                break;
            case GridItemType.Stinky:
                DrawStinky(g);
                return;
            case GridItemType.IzombieBrain:
                DrawIZombieBrain(g);
                break;
            default:
                Debug.ASSERT(false);
                break;
            }
            Reanimation reanimation = mApp.ReanimationTryToGet(mGridItemReanimID);
            if (reanimation != null)
            {
                reanimation.Draw(g);
            }
            TodParticleSystem todParticleSystem = mApp.ParticleTryToGet(mGridItemParticleID);
            if (todParticleSystem != null)
            {
                todParticleSystem.Draw(g, true);
            }
        }

        public void DrawGridItemOverlay(Graphics g)
        {
            GridItemType gridItemType = mGridItemType;
            if (gridItemType != GridItemType.Stinky)
            {
                return;
            }
            if (mBoard.mCursorObject.mCursorType == CursorType.Chocolate && !mApp.mZenGarden.IsStinkyHighOnChocolate())
            {
                g.DrawImage(AtlasResources.IMAGE_PLANTSPEECHBUBBLE, Constants.ZenGarden_StinkySpeechBubble_Pos.X + Constants.S * mPosX, Constants.ZenGarden_StinkySpeechBubble_Pos.Y + Constants.S * mPosY);
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_CHOCOLATE, Constants.ZenGarden_Chocolate_Pos.X + mPosX * Constants.S, Constants.ZenGarden_Chocolate_Pos.Y + mPosY * Constants.S, 0.44f, 0.44f);
            }
        }

        public void OpenPortal()
        {
            float num = mGridX * 80f - 6f;
            float num2 = mBoard.GridToPixelY(0, mGridY) - 65f;
            Reanimation reanimation = mApp.ReanimationTryToGet(mGridItemReanimID);
            if (reanimation == null)
            {
                ReanimationType theReanimationType = ReanimationType.PortalCircle;
                if (mGridItemType == GridItemType.PortalSquare)
                {
                    num2 += 25f;
                    num += -4f;
                    theReanimationType = ReanimationType.PortalSquare;
                }
                reanimation = mApp.AddReanimation(num, num2, 0, theReanimationType);
                reanimation.mIsAttachment = true;
                mGridItemReanimID = mApp.ReanimationGetID(reanimation);
            }
            else
            {
                reanimation.SetPosition(num * Constants.S, num2 * Constants.S);
            }
            TodParticleSystem todParticleSystem = mApp.ParticleTryToGet(mGridItemParticleID);
            if (todParticleSystem != null)
            {
                todParticleSystem.ParticleSystemDie();
                mGridItemParticleID = null;
            }
            reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_appear, ReanimLoopType.PlayOnceAndHold, 0, 12f);
            mApp.PlayFoley(FoleyType.Portal);
        }

        public void Update(/*int updateCount*/)//1update
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mGridItemReanimID);
            if (reanimation != null/* && updateCount == 0*/)
            {
                reanimation.Update();
            }
            TodParticleSystem todParticleSystem = mApp.ParticleTryToGet(mGridItemParticleID);
            if (todParticleSystem != null)
            {
                todParticleSystem.Update();
            }
            if (mGridItemType == GridItemType.PortalCircle || mGridItemType == GridItemType.PortalSquare)
            {
                UpdatePortal();
            }
            if (mGridItemType == GridItemType.ScaryPot)
            {
                UpdateScaryPot();
            }
            if (mGridItemType == GridItemType.Rake)
            {
                UpdateRake();
            }
            if (mGridItemType == GridItemType.IzombieBrain)
            {
                UpdateBrain();
            }
        }

        public void ClosePortal()
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(mGridItemReanimID);
            if (reanimation != null)
            {
                reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_dissapear, ReanimLoopType.PlayOnceAndHold, 0, 12f);
            }
            TodParticleSystem todParticleSystem = mApp.ParticleTryToGet(mGridItemParticleID);
            if (todParticleSystem != null)
            {
                todParticleSystem.ParticleSystemDie();
                mGridItemParticleID = null;
            }
            mGridItemState = GridItemState.PortalClosed;
        }

        public void DrawScaryPot(Graphics g)
        {
            int num = mGridItemState - GridItemState.ScaryPotQuestion;
            Debug.ASSERT(num >= 0 && num < 3);
            int num2 = mBoard.GridToPixelX(mGridX, mGridY) - 5;
            int num3 = mBoard.GridToPixelY(mGridX, mGridY) - 15;
            if (mTransparentCounter > 0)
            {
                g.DrawImageCel(Resources.IMAGE_SCARY_POT, (int)(num2 * Constants.S), (int)(num3 * Constants.S), num, 0);
                Graphics @new = Graphics.GetNew(g);
                if (mScaryPotType == ScaryPotType.Seed)
                {
                    @new.mScaleX = 0.7f;
                    @new.mScaleY = 0.7f;
                    SeedPacket.DrawSmallSeedPacket(@new, (num2 + Constants.GridItem_ScaryPot_SeedPacket_Offset.X) * Constants.S, (num3 + Constants.GridItem_ScaryPot_SeedPacket_Offset.Y) * Constants.S, mSeedType, SeedType.None, 0f, 255, false, false, true, false);
                }
                else if (mScaryPotType == ScaryPotType.Zombie)
                {
                    @new.mScaleX = 0.4f;
                    @new.mScaleY = 0.4f;
                    float num4 = Constants.GridItem_ScaryPot_Zombie_Offset.X;
                    float num5 = Constants.GridItem_ScaryPot_Zombie_Offset.Y;
                    if (mZombieType == ZombieType.Football)
                    {
                        @new.mScaleX = 0.4f;
                        @new.mScaleY = 0.4f;
                        num4 = Constants.GridItem_ScaryPot_ZombieFootball_Offset.X;
                        num5 = Constants.GridItem_ScaryPot_ZombieFootball_Offset.Y;
                    }
                    if (mZombieType == ZombieType.Gargantuar)
                    {
                        @new.mScaleX = 0.3f;
                        @new.mScaleY = 0.3f;
                        num4 += Constants.GridItem_ScaryPot_ZombieGargantuar_Offset.X;
                        num5 += Constants.GridItem_ScaryPot_ZombieGargantuar_Offset.Y;
                    }
                    mApp.mReanimatorCache.DrawCachedZombie(@new, (num2 + num4) * Constants.S, (num3 + num5) * Constants.S, mZombieType);
                }
                else if (mScaryPotType == ScaryPotType.Sun)
                {
                    int num6 = mBoard.mChallenge.ScaryPotterCountSunInPot(this);
                    Reanimation newReanimation = Reanimation.GetNewReanimation();
                    newReanimation.ReanimationInitializeType(0f, 0f, ReanimationType.Sun);
                    newReanimation.OverrideScale(0.5f, 0.5f);
                    for (int i = 0; i < num6; i++)
                    {
                        float num7 = Constants.GridItem_ScaryPot_Sun_Offset.X;
                        float num8 = Constants.GridItem_ScaryPot_Sun_Offset.Y;
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
                        newReanimation.SetPosition((num2 + num7) * Constants.S, (num3 + num8) * Constants.S);
                        newReanimation.Draw(g);
                    }
                    newReanimation.PrepareForReuse();
                }
                @new.PrepareForReuse();
                int theAlpha = TodCommon.TodAnimateCurve(0, 50, mTransparentCounter, 255, 58, TodCurves.Linear);
                g.SetColorizeImages(true);
                g.SetColor(new SexyColor(255, 255, 255, theAlpha));
            }
            g.DrawImageCel(Resources.IMAGE_SCARY_POT, (int)(num2 * Constants.S), (int)(num3 * Constants.S), num, 1);
            if (mHighlighted)
            {
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                g.SetColorizeImages(true);
                if (mTransparentCounter == 0)
                {
                    g.SetColor(new SexyColor(255, 255, 255, 196));
                }
                g.DrawImageCel(Resources.IMAGE_SCARY_POT, (int)(num2 * Constants.S), (int)(num3 * Constants.S), num, 1);
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            }
            g.SetColorizeImages(false);
        }

        public void UpdateScaryPot()//3update
        {
            if (mApp.mTodCheatKeys && mApp.mWidgetManager.mKeyDown[16])
            {
                if (mTransparentCounter < 50)
                {
                    mTransparentCounter++;
                }
                return;
            }
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && plant.mSeedType == SeedType.Plantern && !plant.NotOnGround())
                {
                    int num = Math.Max(Math.Abs(plant.mPlantCol - mGridX), Math.Abs(plant.mRow - mGridY));
                    if (num <= 1)
                    {
                        if (mTransparentCounter < 50)
                        {
                            mTransparentCounter++;
                        }
                        return;
                    }
                }
            }
            if (mTransparentCounter > 0)
            {
                mTransparentCounter--;
            }
        }

        public void UpdatePortal()//3update
        {
            Reanimation reanimation = mApp.ReanimationGet(mGridItemReanimID);
            if (reanimation == null)
            {
                return;
            }
            if (mGridItemState == GridItemState.PortalClosed)
            {
                if (reanimation.mLoopCount > 0)
                {
                    GridItemDie();
                    return;
                }
            }
            else if (reanimation.mLoopType == ReanimLoopType.PlayOnceAndHold && reanimation.mLoopCount > 0)
            {
                reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_pulse, ReanimLoopType.Loop, 0, 12f);
                ParticleEffect theEffect = ParticleEffect.PortalCircle;
                float num = mGridX * 80f + 13f;
                float num2 = mBoard.GridToPixelY(0, mGridY) - 39f;
                if (mGridItemType == GridItemType.PortalSquare)
                {
                    theEffect = ParticleEffect.PortalSquare;
                    num += -8f;
                    num2 += 15f;
                }
                TodParticleSystem theParticle = mApp.AddTodParticle(num, num2, 0, theEffect);
                mGridItemParticleID = mApp.ParticleGetID(theParticle);
            }
        }

        public void DrawSquirrel(Graphics g)
        {
            int num = mBoard.GridToPixelX(mGridX, mGridY);
            int num2 = mBoard.GridToPixelY(mGridX, mGridY);
            if (mGridItemState == GridItemState.SquirrelPeeking)
            {
                num2 += TodCommon.TodAnimateCurve(50, 0, mGridItemCounter, 0, -40, TodCurves.BounceSlowMiddle);
                return;
            }
            if (mGridItemState == GridItemState.SquirrelRunningUp)
            {
                num2 += TodCommon.TodAnimateCurve(50, 0, mGridItemCounter, 100, 0, TodCurves.EaseIn);
                return;
            }
            if (mGridItemState == GridItemState.SquirrelRunningDown)
            {
                num2 += TodCommon.TodAnimateCurve(50, 0, mGridItemCounter, -100, 0, TodCurves.EaseIn);
                return;
            }
            if (mGridItemState == GridItemState.SquirrelRunningLeft)
            {
                num += TodCommon.TodAnimateCurve(50, 0, mGridItemCounter, 80, 0, TodCurves.EaseIn);
                return;
            }
            if (mGridItemState == GridItemState.SquirrelRunningRight)
            {
                num += TodCommon.TodAnimateCurve(50, 0, mGridItemCounter, -80, 0, TodCurves.EaseIn);
            }
        }

        public void UpdateRake()//3update
        {
            if (mGridItemState == GridItemState.RakeAttracting || mGridItemState == GridItemState.RakeWaiting)
            {
                Zombie zombie = RakeFindZombie();
                if (zombie != null)
                {
                    Reanimation reanimation = mApp.ReanimationGet(mGridItemReanimID);
                    reanimation.mAnimRate = 20f;
                    mGridItemCounter = 200;
                    mGridItemState = GridItemState.RakeTriggered;
                    mApp.PlayFoley(FoleyType.Swing);
                    return;
                }
            }
            else if (mGridItemState == GridItemState.RakeTriggered)
            {
                Reanimation reanimation2 = mApp.ReanimationGet(mGridItemReanimID);
                if (reanimation2 != null && reanimation2.ShouldTriggerTimedEvent(0.8f))
                {
                    Zombie zombie2 = RakeFindZombie();
                    if (zombie2 != null)
                    {
                        zombie2.TakeDamage(1800, 0U);
                        mApp.PlayFoley(FoleyType.Bonk);
                    }
                }
                //mGridItemCounter -= 3;
                mGridItemCounter--;
                if (mGridItemCounter <= 0)
                {
                    GridItemDie();
                }
            }
        }

        public Zombie RakeFindZombie()
        {
            TRect rect = new TRect((int)mPosX, (int)mPosY, 63, 80);
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && !zombie.IsDeadOrDying() && !zombie.IsBobsledTeamWithSled() && zombie.mRow - mGridY == 0 && zombie.EffectedByDamage(1U))
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
            if (mGridItemState == GridItemState.BrainSquished)
            {
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_BRAIN, mPosX * Constants.S, (mPosY + 20f) * Constants.S, 1f, 0.25f);
                return;
            }
            if (mBoard.mAdvice.mDuration > 0 && mBoard.mHelpIndex == AdviceType.IZombieEatAllBrains)
            {
                SexyColor flashingColor = TodCommon.GetFlashingColor(mBoard.mMainCounter, 75);
                g.SetColorizeImages(true);
                g.SetColor(flashingColor);
            }
            g.DrawImageF(AtlasResources.IMAGE_BRAIN, mPosX * Constants.S, mPosY * Constants.S);
            if (mTransparentCounter > 0)
            {
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                g.SetColorizeImages(true);
                int theAlpha = TodCommon.ClampInt(mTransparentCounter * 3, 0, 255);
                g.SetColor(new SexyColor(255, 255, 255, theAlpha));
                g.DrawImageF(AtlasResources.IMAGE_BRAIN, mPosX * Constants.S, mPosY * Constants.S);
                g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
                g.SetColorizeImages(false);
            }
            g.SetColorizeImages(false);
        }

        public void UpdateBrain()//3update
        {
            if (mGridItemState == GridItemState.BrainSquished)
            {
                //mGridItemCounter -= 3;
                mGridItemCounter--;
                if (mGridItemCounter <= 0)
                {
                    GridItemDie();
                }
            }
            if (mTransparentCounter > 0)
            {
                //mTransparentCounter -= 3;
                mTransparentCounter--;
            }
        }

        public void DrawStinky(Graphics g)
        {
            Reanimation reanimation = mApp.ReanimationGet(mGridItemReanimID);
            float animTime = reanimation.mAnimTime;
            Debug.ASSERT(mMotionTrailCount <= 12);
            for (int i = mMotionTrailCount - 1; i >= 0; i--)
            {
                if (i % 2 != 0)
                {
                    MotionTrailFrame motionTrailFrame = mMotionTrailFrames[i];
                    float num = motionTrailFrame.mPosX - mPosX;
                    float num2 = motionTrailFrame.mPosY - mPosY;
                    int theAlpha = TodCommon.TodAnimateCurve(0, 11, i, 64, 16, TodCurves.Linear);
                    g.SetColor(new SexyColor(255, 255, 255, theAlpha));
                    g.SetColorizeImages(true);
                    reanimation.mAnimTime = motionTrailFrame.mAnimTime;
                    float num3 = g.mTransX;
                    float num4 = g.mTransY;
                    g.mTransX += (int)(num * Constants.S);
                    g.mTransY += (int)(num2 * Constants.S);
                    reanimation.Draw(g);
                    g.SetColorizeImages(false);
                    g.mTransX = (int)num3;
                    g.mTransY = (int)num4;
                }
            }
            reanimation.mAnimTime = animTime;
            if (mGridItemType == GridItemType.Stinky && mHighlighted)
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
