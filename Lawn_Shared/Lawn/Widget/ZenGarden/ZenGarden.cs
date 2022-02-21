using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class ZenGarden : StoreListener
    {
        public ZenGarden()
        {
            mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
            mBoard = null;
            mGardenType = GardenType.Main;
            mIsTutorial = false;
        }

        static ZenGarden()
        {
            for (int i = 0; i < ZenGarden.gGreenhouseGridPlacement.Length; i++)
            {
                ZenGarden.gGreenhouseGridPlacement[i].mPixelX = (int)(gGreenhouseGridPlacement[i].mPixelX * Constants.ZenGardenGreenhouseMultiplierX) + Constants.ZenGardenGreenhouseOffset.X;
                ZenGarden.gGreenhouseGridPlacement[i].mPixelY = (int)(gGreenhouseGridPlacement[i].mPixelY * Constants.ZenGardenGreenhouseMultiplierY) + Constants.ZenGardenGreenhouseOffset.Y;
            }
            for (int j = 0; j < Constants.gMushroomGridPlacement.Length; j++)
            {
                Constants.gMushroomGridPlacement[j].mPixelX = Constants.gMushroomGridPlacement[j].mPixelX + Constants.ZenGardenMushroomGardenOffset.X;
                Constants.gMushroomGridPlacement[j].mPixelY = Constants.gMushroomGridPlacement[j].mPixelY + Constants.ZenGardenMushroomGardenOffset.Y;
            }
        }

        public void Dispose()
        {
            UnloadBackdrop();
        }

        public void UnloadBackdrop()
        {
            mApp.DelayLoadZenGardenBackground(string.Empty);
        }

        public void ZenGardenInitLevel(bool theJustSwitchingGardens)
        {
            mBoard = mApp.mBoard;
            for (int i = 0; i < mApp.mPlayerInfo.mNumPottedPlants; i++)
            {
                PottedPlant pottedPlant = PottedPlantFromIndex(i);
                if (pottedPlant.mWhichZenGarden == mGardenType)
                {
                    PlacePottedPlant(i);
                }
            }
            Challenge challenge = mBoard.mChallenge;
            challenge.mChallengeStateCounter = 3000;
            AddStinky();
            mApp.mMusic.StartGameMusic();
        }

        public float GetPottedPlantXOffset(SeedType theType, bool isFlipped)
        {
            return 0f;
        }

        public float GetPottedPlantYOffset(SeedType theType, bool isFlipped)
        {
            return 0f;
        }

        public void DrawPottedPlantIcon(Graphics g, float x, float y, PottedPlant thePottedPlant)
        {
            DrawPottedPlant(g, x, y, thePottedPlant, 0.7f, true);
        }

        public void DrawPottedPlant(Graphics g, float x, float y, PottedPlant thePottedPlant, float theScale, bool theDrawPot)
        {
            Graphics aPottedPlantG = Graphics.GetNew(g);
            aPottedPlantG.mScaleX = theScale;
            aPottedPlantG.mScaleY = theScale;
            DrawVariation aPlantVariation = DrawVariation.Normal;
            SeedType seedType = thePottedPlant.mSeedType;
            if (thePottedPlant.mPlantAge == PottedPlantAge.Sprout)
            {
                seedType = SeedType.Sprout;
                if (thePottedPlant.mSeedType != SeedType.Marigold)
                {
                    aPlantVariation = DrawVariation.SproutNoFlower;
                }
            }
            else if (seedType == SeedType.Tanglekelp && thePottedPlant.mWhichZenGarden == GardenType.Aquarium)
            {
                aPlantVariation = DrawVariation.Aquarium;
            }
            else if (seedType == SeedType.Seashroom && thePottedPlant.mWhichZenGarden == GardenType.Aquarium)
            {
                aPlantVariation = DrawVariation.Aquarium;
            }
            else if (seedType == SeedType.Sunshroom)
            {
                aPlantVariation = DrawVariation.Bigidle;
            }
            else
            {
                aPlantVariation = thePottedPlant.mDrawVariation;
            }
            PottedPlant.FacingDirection facing = thePottedPlant.mFacing;
            float aOffsetX = 0f;
            float aOffsetY = 0f;
            if (theDrawPot)
            {
                DrawVariation theDrawVariation2 = DrawVariation.ZenGarden;
                if (Plant.IsAquatic(seedType))
                {
                    theDrawVariation2 = DrawVariation.ZenGardenWater;
                }
                Plant.DrawSeedType(aPottedPlantG, SeedType.Flowerpot, SeedType.None, theDrawVariation2, x, y);
            }
            if (thePottedPlant.mFacing == PottedPlant.FacingDirection.Left)
            {
                aPottedPlantG.mScaleX = -theScale;
            }
            if (theDrawPot)
            {
                aOffsetY += Constants.InvertAndScale(POTTED_PLANT_DRAW_OFFSETS[(int)seedType].yCachedOffset) * aPottedPlantG.mScaleY;
                aOffsetX += Constants.InvertAndScale(POTTED_PLANT_DRAW_OFFSETS[(int)seedType].xCachedOffset) * aPottedPlantG.mScaleX;
            }
            Plant.DrawSeedType(aPottedPlantG, seedType, SeedType.None, aPlantVariation, x + aOffsetX, y + Constants.S * aOffsetY);
            aPottedPlantG.PrepareForReuse();
        }

        public bool IsZenGardenFull(bool theIncludeDroppedPresents)
        {
            int num = 0;
            if (mBoard != null && theIncludeDroppedPresents)
            {
                num += mBoard.CountCoinByType(CoinType.AwardPresent);
                num += mBoard.CountCoinByType(CoinType.PresentPlant);
            }
            int num2 = 0;
            for (int i = 0; i < mApp.mPlayerInfo.mNumPottedPlants; i++)
            {
                PottedPlant pottedPlant = PottedPlantFromIndex(i);
                if (pottedPlant.mWhichZenGarden == GardenType.Main)
                {
                    num2++;
                }
            }
            return num2 + num >= 32;
        }

        public void FindOpenZenGardenSpot(ref int theSpotX, ref int theSpotY)
        {
            TodWeightedGridArray[] aPicks = new TodWeightedGridArray[32];
            for (int i = 0; i < aPicks.Length; i++)
            {
                aPicks[i] = TodWeightedGridArray.GetNewTodWeightedGridArray();
            }
            int num = 0;
            for (int j = 0; j < 8; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (mApp.mCrazyDaveMessageIndex == -1 || (j >= 2 && k >= 1))
                    {
                        bool flag = false;
                        for (int l = 0; l < mApp.mPlayerInfo.mNumPottedPlants; l++)
                        {
                            PottedPlant pottedPlant = PottedPlantFromIndex(l);
                            if (pottedPlant.mWhichZenGarden == GardenType.Main && pottedPlant.mX == j && pottedPlant.mY == k)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            aPicks[num].mX = j;
                            aPicks[num].mY = k;
                            aPicks[num].mWeight = 1;
                            num++;
                        }
                    }
                }
            }
            TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(aPicks, num);
            theSpotX = todWeightedGridArray.mX;
            theSpotY = todWeightedGridArray.mY;
        }

        public void AddPottedPlant(PottedPlant thePottedPlant)
        {
            Debug.ASSERT(mApp.mPlayerInfo.mNumPottedPlants < GameConstants.MAX_POTTED_PLANTS);
            int numPottedPlants = mApp.mPlayerInfo.mNumPottedPlants;
            PottedPlant aPottedPlant = mApp.mPlayerInfo.mPottedPlant[numPottedPlants];
            aPottedPlant.mDrawVariation = thePottedPlant.mDrawVariation;
            aPottedPlant.mFacing = thePottedPlant.mFacing;
            aPottedPlant.mFeedingsPerGrow = thePottedPlant.mFeedingsPerGrow;
            aPottedPlant.mFutureAttribute = thePottedPlant.mFutureAttribute;
            aPottedPlant.mLastChocolateTime = thePottedPlant.mLastChocolateTime;
            aPottedPlant.mLastFertilizedTime = thePottedPlant.mLastFertilizedTime;
            aPottedPlant.mLastNeedFulfilledTime = thePottedPlant.mLastNeedFulfilledTime;
            aPottedPlant.mPlantAge = thePottedPlant.mPlantAge;
            aPottedPlant.mPlantNeed = thePottedPlant.mPlantNeed;
            aPottedPlant.mSeedType = thePottedPlant.mSeedType;
            aPottedPlant.mTimesFed = thePottedPlant.mTimesFed;
            aPottedPlant.mX = thePottedPlant.mX;
            aPottedPlant.mY = thePottedPlant.mY;
            aPottedPlant.mWhichZenGarden = GardenType.Main;
            aPottedPlant.mLastWateredTime = default(DateTime);
            FindOpenZenGardenSpot(ref aPottedPlant.mX, ref aPottedPlant.mY);
            mApp.mPlayerInfo.mNumPottedPlants++;
            if (mApp.mGameMode == GameMode.ChallengeZenGarden && mBoard != null && aPottedPlant.mWhichZenGarden == mGardenType)
            {
                Plant aPlant = PlacePottedPlant(numPottedPlants);
                if (mApp.GetDialog(Dialogs.DIALOG_STORE) == null)
                {
                    mBoard.DoPlantingEffects(aPottedPlant.mX, aPottedPlant.mY, aPlant, mGardenType == GardenType.Aquarium);
                }
            }
        }

        public void MouseDownWithTool(int x, int y, CursorType theCursorType)
        {
            if (theCursorType == CursorType.Wheeelbarrow && GetPottedPlantInWheelbarrow() != null)
            {
                MouseDownWithFullWheelBarrow(x, y);
                mBoard.ClearCursor();
                return;
            }
            if (theCursorType == CursorType.WateringCan || theCursorType == CursorType.Fertilizer || theCursorType == CursorType.BugSpray || theCursorType == CursorType.Phonograph || theCursorType == CursorType.Chocolate)
            {
                MouseDownWithFeedingTool(x, y, theCursorType);
                return;
            }
            HitResult hitResult = mBoard.ToolHitTest(x, y, true);
            Plant plant = null;
            if (hitResult.mObjectType == GameObjectType.Plant)
            {
                plant = (Plant)hitResult.mObject;
            }
            if (plant == null || plant.mPottedPlantIndex == -1)
            {
                mApp.PlayFoley(FoleyType.Drop);
                mBoard.ClearCursor();
                return;
            }
            if (theCursorType == CursorType.MoneySign)
            {
                MouseDownWithMoneySign(plant);
                return;
            }
            if (theCursorType == CursorType.Wheeelbarrow)
            {
                MouseDownWithEmptyWheelBarrow(plant);
                mBoard.ClearCursor();
                return;
            }
            if (theCursorType == CursorType.Glove)
            {
                mBoard.mCursorObject.mType = plant.mSeedType;
                mBoard.mCursorObject.mImitaterType = plant.mImitaterType;
                mBoard.mCursorObject.mCursorType = CursorType.PlantFromGlove;
                mBoard.mCursorObject.mGlovePlantID = mBoard.mPlants[mBoard.mPlants.IndexOf(plant)];
                plant.mGloveGrabbed = true;
                mBoard.mIgnoreMouseUp = true;
                mApp.PlaySample(Resources.SOUND_TAP);
            }
        }

        public void MovePlant(Plant thePlant, int theGridX, int theGridY)
        {
            if (mApp.mGameMode != GameMode.ChallengeZenGarden)
            {
                return;
            }
            int aPosX = mBoard.GridToPixelX(theGridX, theGridY);
            int aPosY = mBoard.GridToPixelY(theGridX, theGridY);
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                aPosY -= Constants.ZenGardenGreenhouseOffset.Y;
            }
            Debug.ASSERT(mBoard.GetTopPlantAt(theGridX, theGridY, TopPlant.Any) == null);
            bool aIsSleeping = thePlant.mIsAsleep;
            thePlant.SetSleeping(false);
            Plant aTopPlantAtGrid = mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, TopPlant.OnlyUnderPlant);
            if (aTopPlantAtGrid != null)
            {
                aTopPlantAtGrid.mX = aPosX;
                aTopPlantAtGrid.mY = aPosY;
                aTopPlantAtGrid.mPlantCol = theGridX;
                aTopPlantAtGrid.mRow = theGridY;
                aTopPlantAtGrid.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Plant, 0, aTopPlantAtGrid.mY);
            }
            float aDeltaX = aPosX - thePlant.mX;
            float aDeltaY = aPosY - thePlant.mY;
            thePlant.mX = aPosX;
            thePlant.mY = aPosY;
            thePlant.mPlantCol = theGridX;
            thePlant.mRow = theGridY;
            thePlant.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Plant, 0, thePlant.mY + 1);
            TodParticleSystem aTodParticleSystem = mApp.ParticleTryToGet(thePlant.mParticleID);
            if (aTodParticleSystem != null && aTodParticleSystem.mEmitterList.Count != 0)
            {
                TodParticleEmitter todParticleEmitter = aTodParticleSystem.mParticleHolder.mEmitters[0];
                aTodParticleSystem.SystemMove(todParticleEmitter.mSystemCenter.x + aDeltaX, todParticleEmitter.mSystemCenter.y + aDeltaY);
            }
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            pottedPlant.mX = theGridX;
            pottedPlant.mY = theGridY;
            if (thePlant.mState == PlantState.ZenGardenHappy)
            {
                RemoveHappyEffect(thePlant);
                AddHappyEffect(thePlant);
            }
            if (aTopPlantAtGrid != null)
            {
                mBoard.DoPlantingEffects(theGridX, theGridY, aTopPlantAtGrid, mGardenType == GardenType.Aquarium);
                return;
            }
            mBoard.DoPlantingEffects(theGridX, theGridY, thePlant, mGardenType == GardenType.Aquarium);
        }

        public void MouseDownWithMoneySign(Plant thePlant)
        {
            mBoard.ClearCursor();
            string aDialogHeader = TodStringFile.TodStringTranslate("[ZEN_SELL_HEADER]");
            string aDialogLines = TodStringFile.TodStringTranslate("[ZEN_SELL_LINES]");
            int aSellPrice = GetPlantSellPrice(thePlant);
            if (mApp.mCrazyDaveState == CrazyDaveState.Off)
            {
                mApp.CrazyDaveEnter();
            }
            PottedPlant aPottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            string aMessageText = mApp.GetCrazyDaveText(1700);
            aMessageText = TodCommon.TodReplaceString(aMessageText, "{SELL_PRICE}", Common.CommaSeperate(aSellPrice * 10));
            string aPlantName = string.Empty;
            if (thePlant.mSeedType == SeedType.Sprout && aPottedPlant.mSeedType == SeedType.Marigold)
            {
                aPlantName = TodStringFile.TodStringTranslate("[MARIGOLD_SPROUT]");
            }
            else
            {
                aPlantName = Plant.GetNameString(thePlant.mSeedType, thePlant.mImitaterType);
            }
            aMessageText = TodCommon.TodReplaceString(aMessageText, "{PLANT_TYPE}", aPlantName);
            mApp.CrazyDaveTalkMessage(aMessageText);
            Reanimation reanimation = mApp.ReanimationGet(mApp.mCrazyDaveReanimID);
            reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_blahblah, ReanimLoopType.PlayOnceAndHold, 20, 12f);
            LawnDialog lawnDialog = mApp.DoDialog(48, true, aDialogHeader, aDialogLines, "", 1);
            lawnDialog.mX += Constants.ZenGarden_SellDialog_Offset.X;
            lawnDialog.mY += Constants.ZenGarden_SellDialog_Offset.Y;
            mBoard.ShowCoinBank();
            mPlantForSale = thePlant;
        }

        public Plant PlacePottedPlant(int thePottedPlantIndex)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePottedPlantIndex);
            SeedType seedType = pottedPlant.mSeedType;
            if (pottedPlant.mPlantAge == PottedPlantAge.Sprout)
            {
                seedType = SeedType.Sprout;
            }
            bool flag = true;
            if (mGardenType == GardenType.Mushroom && !Plant.IsAquatic(seedType))
            {
                flag = false;
            }
            else if (mGardenType == GardenType.Aquarium)
            {
                flag = false;
            }
            if (flag)
            {
                Plant plant = mBoard.NewPlant(pottedPlant.mX, pottedPlant.mY, SeedType.Flowerpot, SeedType.None);
                plant.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Plant, 0, plant.mY);
                plant.mStateCountdown = 0;
                Reanimation reanimation = mApp.ReanimationGet(plant.mBodyReanimID);
                if (Plant.IsAquatic(seedType))
                {
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_waterplants);
                }
                else
                {
                    reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_zengarden);
                }
            }
            if (seedType < 0 || seedType >= SeedType.SeedTypeCount)
            {
                pottedPlant.mSeedType = SeedType.Kernelpult;
                seedType = SeedType.Kernelpult;
            }
            Plant plant2 = mBoard.NewPlant(pottedPlant.mX, pottedPlant.mY, seedType, SeedType.None);
            plant2.mPottedPlantIndex = thePottedPlantIndex;
            plant2.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Plant, 0, plant2.mY + 1);
            plant2.mStateCountdown = 0;
            Reanimation reanimation2 = mApp.ReanimationTryToGet(plant2.mBodyReanimID);
            if (reanimation2 != null)
            {
                if (seedType == SeedType.Sprout)
                {
                    if (pottedPlant.mSeedType != SeedType.Marigold)
                    {
                        reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle_noflower);
                    }
                }
                else if (seedType == SeedType.Tanglekelp && mGardenType == GardenType.Aquarium)
                {
                    reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle_aquarium);
                }
                else if (seedType == SeedType.Seashroom && mGardenType == GardenType.Aquarium)
                {
                    reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle_aquarium);
                }
                plant2.UpdateReanim();
                reanimation2.Update();
            }
            PlantSetLaunchCounter(plant2);
            UpdatePlantEffectState(plant2);
            return plant2;
        }

        public float PlantPottedDrawHeightOffset(SeedType theSeedType, float theScale, bool bInWheelBarrow)
        {
            return PlantPottedDrawHeightOffset(theSeedType, theScale, bInWheelBarrow, DrawVariation.Normal);
        }

        public float PlantPottedDrawHeightOffset(SeedType theSeedType, float theScale, bool bInWheelBarrow, DrawVariation theDrawVariation)
        {
            float aScaleOffsetFix = 0f;
            float aHeightOffset = 0f;
            if (theSeedType == SeedType.Gravebuster)
            {
                aHeightOffset += 50f;
                aScaleOffsetFix += 15f;
            }
            else if (theSeedType == SeedType.Puffshroom)
            {
                aHeightOffset += 10f;
                aScaleOffsetFix += 24f;
            }
            else if (theSeedType == SeedType.Sunshroom)
            {
                aHeightOffset += 10f;
                aScaleOffsetFix += 17f;
            }
            else if (theSeedType == SeedType.Scaredyshroom)
            {
                aHeightOffset += 5f;
                aScaleOffsetFix += 5f;
            }
            else if (theSeedType == SeedType.Tanglekelp)
            {
                aHeightOffset += -18f;
                aScaleOffsetFix += 20f;
            }
            else if (theSeedType == SeedType.Seashroom)
            {
                aHeightOffset += -20f;
                aScaleOffsetFix += 15f;
            }
            else if (theSeedType == SeedType.Lilypad)
            {
                aHeightOffset += -10f;
                aScaleOffsetFix += 30f;
            }
            else if (theSeedType == SeedType.Chomper)
            {
                aScaleOffsetFix += 0f;
            }
            else if (theSeedType == SeedType.Hypnoshroom)
            {
                aScaleOffsetFix += 10f;
            }
            else if (theSeedType == SeedType.Magnetshroom)
            {
                aScaleOffsetFix += 10f;
            }
            else if (theSeedType == SeedType.Peashooter || theSeedType == SeedType.Repeater || theSeedType == SeedType.Leftpeater || theSeedType == SeedType.Snowpea || theSeedType == SeedType.Threepeater || theSeedType == SeedType.Sunflower || theSeedType == SeedType.Marigold)
            {
                aScaleOffsetFix += 10f;
            }
            else if (theSeedType == SeedType.Starfruit)
            {
                aHeightOffset += 10f;
                aScaleOffsetFix += 24f;
            }
            else if (theSeedType == SeedType.Cabbagepult || theSeedType == SeedType.Melonpult)
            {
                aScaleOffsetFix += 10f;
                aHeightOffset += 3f;
            }
            else if (theSeedType == SeedType.Potatomine)
            {
                aScaleOffsetFix += 5f;
            }
            else if (theSeedType == SeedType.Torchwood)
            {
                aScaleOffsetFix += 3f;
            }
            else if (theSeedType == SeedType.Spikeweed)
            {
                aScaleOffsetFix += 10f;
                aHeightOffset -= 13f;
            }
            else if (theSeedType == SeedType.Blover)
            {
                aScaleOffsetFix += 10f;
            }
            else if (theSeedType == SeedType.Pumpkinshell)
            {
                aScaleOffsetFix += 20f;
            }
            else if (theSeedType == SeedType.Plantern)
            {
                aScaleOffsetFix += -1f;
            }
            if (bInWheelBarrow && theSeedType != SeedType.Flowerpot)
            {
                float num3 = POTTED_PLANT_DRAW_OFFSETS[(int)theSeedType].yWheelBarrowScale;
                aHeightOffset += num3 + num3 * (theScale - 0.5f) / 2f;
            }
            aScaleOffsetFix = Constants.InvertAndScale(aScaleOffsetFix);
            return aHeightOffset + (-aScaleOffsetFix + aScaleOffsetFix * theScale);
        }

        public int GetPlantSellPrice(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            if (pottedPlant.mSeedType == SeedType.Marigold)
            {
                if (pottedPlant.mPlantAge == PottedPlantAge.Sprout)
                {
                    return 150;
                }
                if (pottedPlant.mPlantAge == PottedPlantAge.Small)
                {
                    return 200;
                }
                if (pottedPlant.mPlantAge == PottedPlantAge.Medium)
                {
                    return 250;
                }
                if (pottedPlant.mPlantAge == PottedPlantAge.Full)
                {
                    return 300;
                }
                Debug.ASSERT(false);
            }
            if (pottedPlant.mPlantAge == PottedPlantAge.Sprout)
            {
                return 150;
            }
            if (pottedPlant.mPlantAge == PottedPlantAge.Small)
            {
                return 300;
            }
            if (pottedPlant.mPlantAge == PottedPlantAge.Medium)
            {
                return 500;
            }
            if (pottedPlant.mPlantAge != PottedPlantAge.Full)
            {
                Debug.ASSERT(false);
                return -666;
            }
            if (Plant.IsNocturnal(pottedPlant.mSeedType) || Plant.IsAquatic(pottedPlant.mSeedType))
            {
                return 1000;
            }
            return 800;
        }

        public void ZenGardenUpdate(/*int updateCount*/)//1update
        {
            if (mApp.GetDialog(4) != null)
            {
                return;
            }
            Challenge challenge = mBoard.mChallenge;
            if (mBoard.mCursorObject.mCursorType != CursorType.Normal)
            {
                challenge.mChallengeState = ChallengeState.Normal;
                challenge.mChallengeStateCounter = 3000;
            }
            else if (mApp.mBoard.mTutorialState == TutorialState.Off)
            {
                if (challenge.mChallengeStateCounter > 0)
                {
                    challenge.mChallengeStateCounter--;
                }
                if (challenge.mChallengeState == ChallengeState.Normal && challenge.mChallengeStateCounter == 0)
                {
                    challenge.mChallengeState = ChallengeState.ZenFading;
                    challenge.mChallengeStateCounter = 50;
                }
            }
            UpdatePlantNeeds();
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant aPlant = mBoard.mPlants[i];
                if (!aPlant.mDead && aPlant.mPottedPlantIndex != -1)
                {
                    PottedPlantUpdate(aPlant);
                }
            }
            int num = -1;
            GridItem aGridItem = null;
            while (mBoard.IterateGridItems(ref aGridItem, ref num))
            {
                if (aGridItem.mGridItemType == GridItemType.ZenTool)
                {
                    ZenToolUpdate(aGridItem);
                }
                if (aGridItem.mGridItemType == GridItemType.Stinky)
                {
                    StinkyUpdate(aGridItem);
                }
            }
            if (mBoard.mTutorialState == TutorialState.ZenGardenKeepWatering && CountPlantsNeedingFertilizer() > 0)
            {
                mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_VISIT_STORE]", MessageStyle.HintTallLong, AdviceType.None);
                mBoard.mTutorialState = TutorialState.ZenGardenVisitStore;
                mApp.mPlayerInfo.mZenTutorialMessage = 25;
                mBoard.mStoreButton.mDisabled = false;
                mBoard.mStoreButton.mBtnNoDraw = false;
            }
        }

        public void MouseDownWithFullWheelBarrow(int x, int y)
        {
            PottedPlant pottedPlantInWheelbarrow = GetPottedPlantInWheelbarrow();
            Debug.ASSERT(pottedPlantInWheelbarrow != null);
            if (mApp.mZenGarden.mGardenType == GardenType.Aquarium && !Plant.IsAquatic(pottedPlantInWheelbarrow.mSeedType))
            {
                mBoard.DisplayAdvice("[ZEN_ONLY_AQUATIC_PLANTS]", MessageStyle.HintTallFast, AdviceType.None);
                return;
            }
            int num = mBoard.PixelToGridX(x, y);
            int num2 = mBoard.PixelToGridY(x, y);
            if (num == -1 || num2 == -1)
            {
                return;
            }
            PlantingReason plantingReason = mBoard.CanPlantAt(num, num2, pottedPlantInWheelbarrow.mSeedType);
            if (plantingReason != PlantingReason.Ok)
            {
                return;
            }
            pottedPlantInWheelbarrow.mWhichZenGarden = mGardenType;
            pottedPlantInWheelbarrow.mX = num;
            pottedPlantInWheelbarrow.mY = num2;
            int thePottedPlantIndex = -1;
            for (int i = 0; i < mApp.mPlayerInfo.mPottedPlant.Length; i++)
            {
                if (pottedPlantInWheelbarrow == mApp.mPlayerInfo.mPottedPlant[i])
                {
                    thePottedPlantIndex = i;
                    break;
                }
            }
            Plant thePlant = PlacePottedPlant(thePottedPlantIndex);
            mBoard.DoPlantingEffects(pottedPlantInWheelbarrow.mX, pottedPlantInWheelbarrow.mY, thePlant, mGardenType == GardenType.Aquarium);
        }

        public void MouseDownWithEmptyWheelBarrow(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            RemovePottedPlant(thePlant);
            pottedPlant.mWhichZenGarden = GardenType.Wheelbarrow;
            pottedPlant.mX = 0;
            pottedPlant.mY = 0;
            mApp.PlayFoley(FoleyType.Plant);
        }

        public void GotoNextGarden()
        {
            LeaveGarden();
            mBoard.ClearAdvice(AdviceType.None);
            mBoard.mPlants.Clear();
            mBoard.mCoins.Clear();
            mApp.mEffectSystem.EffectSystemFreeAll();
            bool flag = false;
            if (mGardenType == GardenType.Main)
            {
                if (mApp.mPlayerInfo.mPurchases[18] != 0)
                {
                    mGardenType = GardenType.Mushroom;
                    mBoard.mBackground = BackgroundType.MushroomGarden;
                }
                else if (mApp.mPlayerInfo.mPurchases[25] != 0)
                {
                    mGardenType = GardenType.Aquarium;
                    mBoard.mBackground = BackgroundType.Zombiquarium;
                }
                else if (mApp.mPlayerInfo.mPurchases[27] != 0)
                {
                    flag = true;
                }
            }
            else if (mGardenType == GardenType.Mushroom)
            {
                if (mApp.mPlayerInfo.mPurchases[25] != 0)
                {
                    mGardenType = GardenType.Aquarium;
                    mBoard.mBackground = BackgroundType.Zombiquarium;
                }
                else if (mApp.mPlayerInfo.mPurchases[27] != 0)
                {
                    flag = true;
                }
                else
                {
                    mGardenType = GardenType.Main;
                    mBoard.mBackground = BackgroundType.Greenhouse;
                }
            }
            else if (mGardenType == GardenType.Aquarium)
            {
                mGardenType = GardenType.Main;
                mBoard.mBackground = BackgroundType.Greenhouse;
            }
            if (flag)
            {
                mApp.KillBoard();
                mApp.PreNewGame(GameMode.TreeOfWisdom, false);
                return;
            }
            if (mBoard.mBackground == BackgroundType.MushroomGarden)
            {
                mApp.DelayLoadZenGardenBackground("DelayLoad_MushroomGarden");
            }
            else if (mBoard.mBackground == BackgroundType.Greenhouse)
            {
                mApp.DelayLoadZenGardenBackground("DelayLoad_GreenHouseGarden");
            }
            else if (mBoard.mBackground == BackgroundType.Zombiquarium)
            {
                mApp.DelayLoadZenGardenBackground("DelayLoad_Zombiquarium");
            }
            else
            {
                Debug.ASSERT(false);
            }
            if ((mBoard.mBackground == BackgroundType.MushroomGarden || mBoard.mBackground == BackgroundType.Zombiquarium) && mApp.mPlayerInfo.mPurchases[19] == 0)
            {
                mBoard.DisplayAdvice("[ADVICE_NEED_WHEELBARROW]", MessageStyle.HintTallFast, AdviceType.NeedWheelbarrow);
            }
            ZenGardenInitLevel(true);
        }

        public PottedPlant GetPottedPlantInWheelbarrow()
        {
            for (int i = 0; i < mApp.mPlayerInfo.mNumPottedPlants; i++)
            {
                PottedPlant pottedPlant = PottedPlantFromIndex(i);
                if (pottedPlant.mWhichZenGarden == GardenType.Wheelbarrow)
                {
                    return pottedPlant;
                }
            }
            return null;
        }

        public void RemovePottedPlant(Plant thePlant)
        {
            thePlant.Die();
            Plant topPlantAt = mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, TopPlant.OnlyUnderPlant);
            if (topPlantAt != null)
            {
                topPlantAt.Die();
            }
        }

        public SpecialGridPlacement[] GetSpecialGridPlacements(ref int theCount)
        {
            if (mBoard.mBackground == BackgroundType.MushroomGarden)
            {
                theCount = Constants.gMushroomGridPlacement.Length;
                return Constants.gMushroomGridPlacement;
            }
            if (mBoard.mBackground == BackgroundType.Zombiquarium)
            {
                theCount = ZenGarden.gAquariumGridPlacement.Length;
                return ZenGarden.gAquariumGridPlacement;
            }
            if (mBoard.mBackground == BackgroundType.Greenhouse)
            {
                theCount = ZenGarden.gGreenhouseGridPlacement.Length;
                return ZenGarden.gGreenhouseGridPlacement;
            }
            Debug.ASSERT(false);
            return null;
        }

        public int PixelToGridX(int theX, int theY)
        {
            int aCount = 0;
            SpecialGridPlacement[] specialGridPlacements = GetSpecialGridPlacements(ref aCount);
            for (int i = 0; i < aCount; i++)
            {
                SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
                if (theX >= specialGridPlacement.mPixelX && theX <= specialGridPlacement.mPixelX + 80 && theY >= specialGridPlacement.mPixelY && theY <= specialGridPlacement.mPixelY + 85)
                {
                    return specialGridPlacement.mGridX;
                }
            }
            return -1;
        }

        public int PixelToGridY(int theX, int theY)
        {
            int aCount = 0;
            SpecialGridPlacement[] specialGridPlacements = GetSpecialGridPlacements(ref aCount);
            for (int i = 0; i < aCount; i++)
            {
                SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
                if (theX >= specialGridPlacement.mPixelX && theX <= specialGridPlacement.mPixelX + 80 && theY >= specialGridPlacement.mPixelY && theY <= specialGridPlacement.mPixelY + 85)
                {
                    return specialGridPlacement.mGridY;
                }
            }
            return -1;
        }

        public int GridToPixelX(int theGridX, int theGridY)
        {
            int aCount = 0;
            SpecialGridPlacement[] specialGridPlacements = GetSpecialGridPlacements(ref aCount);
            for (int i = 0; i < aCount; i++)
            {
                SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
                if (theGridX == specialGridPlacement.mGridX && theGridY == specialGridPlacement.mGridY)
                {
                    return specialGridPlacement.mPixelX;
                }
            }
            return -1;
        }

        public int GridToPixelY(int theGridX, int theGridY)
        {
            int aCount = 0;
            SpecialGridPlacement[] specialGridPlacements = GetSpecialGridPlacements(ref aCount);
            for (int i = 0; i < aCount; i++)
            {
                SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
                if (theGridX == specialGridPlacement.mGridX && theGridY == specialGridPlacement.mGridY)
                {
                    return specialGridPlacement.mPixelY;
                }
            }
            return -1;
        }

        public void DrawBackdrop(Graphics g)
        {
            if (mGardenType == GardenType.Aquarium && (mBoard.mCursorObject.mCursorType == CursorType.PlantFromWheelBarrow || mBoard.mCursorObject.mCursorType == CursorType.Wheeelbarrow || mBoard.mCursorObject.mCursorType == CursorType.PlantFromGlove))
            {
                int aCount = 0;
                SpecialGridPlacement[] specialGridPlacements = GetSpecialGridPlacements(ref aCount);
                for (int i = 0; i < aCount; i++)
                {
                    SpecialGridPlacement specialGridPlacement = specialGridPlacements[i];
                    if (mBoard.GetTopPlantAt(specialGridPlacement.mGridX, specialGridPlacement.mGridY, TopPlant.ZenToolOrder) == null)
                    {
                        TodCommon.TodDrawImageCelScaled(g, AtlasResources.IMAGE_PLANTSHADOW, (int)(Constants.S * (specialGridPlacement.mPixelX - Constants.ZenGarden_Aquarium_ShadowOffset.X)), (int)(Constants.S * (specialGridPlacement.mPixelY + Constants.ZenGarden_Aquarium_ShadowOffset.Y)), 0, 0, 1.7f, 1.7f);
                    }
                }
            }
        }

        public bool MouseDownZenGarden(int x, int y, int theClickCount, HitResult theHitResult)
        {
            Challenge challenge = mBoard.mChallenge;
            if (challenge.mChallengeState == ChallengeState.ZenFading)
            {
                challenge.mChallengeState = ChallengeState.Normal;
            }
            challenge.mChallengeStateCounter = 3000;
            if (theHitResult.mObjectType == GameObjectType.Stinky && mBoard.mCursorObject.mCursorType == CursorType.Normal)
            {
                WakeStinky();
            }
            else if (mBoard.mCursorObject.mCursorType == CursorType.Glove)
            {
                if (mBoard.CanUseGameObject(GameObjectType.Wheelbarrow))
                {
                    TRect zenButtonRect = mBoard.GetZenButtonRect(GameObjectType.Wheelbarrow);
                    PottedPlant pottedPlantInWheelbarrow = GetPottedPlantInWheelbarrow();
                    if (zenButtonRect.Contains(x, y) && pottedPlantInWheelbarrow != null)
                    {
                        mBoard.ClearCursor();
                        mBoard.mCursorObject.mType = pottedPlantInWheelbarrow.mSeedType;
                        mBoard.mCursorObject.mImitaterType = SeedType.None;
                        mBoard.mCursorObject.mCursorType = CursorType.PlantFromWheelBarrow;
                        return true;
                    }
                }
            }
            else if (mBoard.mCursorObject.mCursorType == CursorType.PlantFromGlove)
            {
                if (mBoard.CanUseGameObject(GameObjectType.Wheelbarrow))
                {
                    TRect zenButtonRect2 = mBoard.GetZenButtonRect(GameObjectType.Wheelbarrow);
                    Plant plant = mBoard.mPlants[mBoard.mPlants.IndexOf(mBoard.mCursorObject.mGlovePlantID)];
                    if (plant != null && zenButtonRect2.Contains(x, y) && GetPottedPlantInWheelbarrow() == null)
                    {
                        plant.mGloveGrabbed = false;
                        MouseDownWithEmptyWheelBarrow(plant);
                        mBoard.ClearCursor();
                        return true;
                    }
                }
            }
            else if (theHitResult.mObjectType != GameObjectType.None || mBoard.mCursorObject.mCursorType != CursorType.Normal || mGardenType == GardenType.Aquarium)
            {
            }
            if (mApp.mCrazyDaveMessageIndex != -1)
            {
                AdvanceCrazyDaveDialog();
                return true;
            }
            return false;
        }

        public void PlantFulfillNeed(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            pottedPlant.mLastNeedFulfilledTime = aNow;
            pottedPlant.mPlantNeed = PottedPlantNeed.None;
            pottedPlant.mTimesFed = 0;
            mApp.PlayFoley(FoleyType.Prize);
            mApp.PlayFoley(FoleyType.SpawnSun);
            mBoard.AddCoin(thePlant.mX + 40, thePlant.mY, CoinType.Gold, CoinMotion.Coin);
            if (Plant.IsNocturnal(thePlant.mSeedType) || Plant.IsAquatic(thePlant.mSeedType))
            {
                mBoard.AddCoin(thePlant.mX + 10, thePlant.mY, CoinType.Gold, CoinMotion.Coin);
                mBoard.AddCoin(thePlant.mX + 70, thePlant.mY, CoinType.Gold, CoinMotion.Coin);
            }
        }

        public void PlantWatered(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            pottedPlant.mTimesFed++;
            int num = TodCommon.RandRangeInt(0, 8);
            if (mBoard.mTutorialState == TutorialState.ZenGardenWaterPlant || mBoard.mTutorialState == TutorialState.ZenGardenKeepWatering)
            {
                num = 9;
            }
            pottedPlant.mLastWateredTime = aNow;
            pottedPlant.mLastWateredTime = pottedPlant.mLastWateredTime.Subtract(TimeSpan.FromSeconds(num));
            mApp.PlayFoley(FoleyType.SpawnSun);
            mBoard.AddCoin(thePlant.mX + 40, thePlant.mY, CoinType.Silver, CoinMotion.Coin);
            if (pottedPlant.mPlantAge == PottedPlantAge.Full && pottedPlant.mPlantNeed == PottedPlantNeed.None)
            {
                pottedPlant.mPlantNeed = (PottedPlantNeed)TodCommon.RandRangeInt(3, 4);
            }
            if (mBoard.mTutorialState == TutorialState.ZenGardenWaterPlant)
            {
                mBoard.mTutorialState = TutorialState.ZenGardenKeepWatering;
                mApp.mPlayerInfo.mZenTutorialMessage = 24;
                mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_KEEP_WATERING]", MessageStyle.ZenGardenLong, AdviceType.None);
            }
        }

        public PottedPlantNeed GetPlantsNeed(PottedPlant thePottedPlant)
        {
            if (thePottedPlant.mPlantAge != PottedPlantAge.Sprout
                && Plant.IsNocturnal(thePottedPlant.mSeedType)
                && thePottedPlant.mWhichZenGarden == GardenType.Main)
            {
                return PottedPlantNeed.None;
            }
            if (thePottedPlant.mWhichZenGarden == GardenType.Wheelbarrow)
            {
                return PottedPlantNeed.None;
            }
            TimeSpan timeSpan = aNow - thePottedPlant.mLastWateredTime;
            bool aTooLongSinceWatering = timeSpan.TotalSeconds > 15.0;
            bool flag3 = timeSpan.TotalSeconds < 3.0;
            if (WasPlantFertilizedInLastHour(thePottedPlant))
            {
                return PottedPlantNeed.None;
            }
            if (WasPlantNeedFulfilledToday(thePottedPlant))
            {
                return PottedPlantNeed.None;
            }
            if (Plant.IsAquatic(thePottedPlant.mSeedType) && thePottedPlant.mPlantAge != PottedPlantAge.Sprout)
            {
                if (thePottedPlant.mPlantAge == PottedPlantAge.Full)
                {
                    if (PlantShouldRefreshNeed(thePottedPlant))
                    {
                        return PottedPlantNeed.None;
                    }
                    return thePottedPlant.mPlantNeed;
                }
                else
                {
                    if (thePottedPlant.mWhichZenGarden != GardenType.Aquarium)
                    {
                        return PottedPlantNeed.None;
                    }
                    return PottedPlantNeed.Fertilizer;
                }
            }
            else
            {
                if (!aTooLongSinceWatering)
                {
                    return PottedPlantNeed.None;
                }
                if (thePottedPlant.mTimesFed < thePottedPlant.mFeedingsPerGrow)
                {
                    return PottedPlantNeed.Water;
                }
                if (flag3)
                {
                    return PottedPlantNeed.None;
                }
                if (thePottedPlant.mPlantAge != PottedPlantAge.Full)
                {
                    return PottedPlantNeed.Fertilizer;
                }
                if (PlantShouldRefreshNeed(thePottedPlant))
                {
                    return PottedPlantNeed.None;
                }
                if (thePottedPlant.mPlantNeed != PottedPlantNeed.None)
                {
                    return thePottedPlant.mPlantNeed;
                }
                return PottedPlantNeed.Water;
            }
        }

        public void MouseDownWithFeedingTool(int x, int y, CursorType theCursorType)
        {
            HitResult hitResult = mApp.mBoard.ToolHitTest(x, y, true);
            Plant plant = null;
            if (hitResult.mObjectType == GameObjectType.Plant)
            {
                plant = (Plant)hitResult.mObject;
            }
            bool flag = theCursorType == CursorType.WateringCan && mApp.mPlayerInfo.mPurchases[13] > 0;
            if ((plant == null || plant.mPottedPlantIndex == -1) && !flag && theCursorType != CursorType.Chocolate)
            {
                mBoard.ClearCursor();
                return;
            }
            if (theCursorType == CursorType.Chocolate)
            {
                Debug.ASSERT(mApp.mPlayerInfo.mPurchases[26] > 1000);
                GridItem stinky = GetStinky();
                if (!IsStinkyHighOnChocolate() && stinky != null)
                {
                    WakeStinky();
                    mApp.AddTodParticle(stinky.mPosX + 40f, stinky.mPosY + 40f, stinky.mRenderOrder + 1, ParticleEffect.PresentPickup);
                    mApp.mPlayerInfo.mLastStinkyChocolateTime = aNow;
                    mApp.mPlayerInfo.mPurchases[26]--;
                    mApp.PlayFoley(FoleyType.Wakeup);
                    mApp.PlaySample(Resources.SOUND_MINDCONTROLLED);
                }
                if (plant != null)
                {
                    mApp.mPlayerInfo.mPurchases[26]--;
                    FeedChocolateToPlant(plant);
                    mApp.PlayFoley(FoleyType.Wakeup);
                }
            }
            if (plant != null || flag)
            {
                GridItem newGridItem = GridItem.GetNewGridItem();
                newGridItem.mGridItemType = GridItemType.ZenTool;
                mBoard.mGridItems.Add(newGridItem);
                if (plant != null)
                {
                    newGridItem.mGridX = plant.mPlantCol;
                    newGridItem.mGridY = plant.mRow;
                    newGridItem.mPosX = plant.mX + 40;
                    newGridItem.mPosY = plant.mY + 40;
                }
                newGridItem.mRenderOrder = 800000;
                if (flag)
                {
                    newGridItem.mPosX = x;
                    newGridItem.mPosY = y;
                    Reanimation reanimation = mApp.AddReanimation(x + Constants.ZenGarden_GoldenWater_Pos.X, y + Constants.ZenGarden_GoldenWater_Pos.Y, 0, ReanimationType.ZengardenWateringcan, true);
                    reanimation.PlayReanim("anim_water_area", ReanimLoopType.PlayOnceAndHold, 0, 8f);
                    newGridItem.mGridItemReanimID = mApp.ReanimationGetID(reanimation);
                    newGridItem.mGridItemState = GridItemState.ZenToolGoldWateringCan;
                    mApp.PlayFoley(FoleyType.Watering);
                }
                else if (theCursorType == CursorType.WateringCan && mApp.mPlayerInfo.mPurchases[13] != 0)
                {
                    newGridItem.mPosX = x;
                    newGridItem.mPosY = y;
                    Reanimation reanimation2 = mApp.AddReanimation(x, y, 0, ReanimationType.ZengardenWateringcan);
                    reanimation2.PlayReanim("anim_water_area", ReanimLoopType.PlayOnceAndHold, 0, 8f);
                    newGridItem.mGridItemReanimID = mApp.ReanimationGetID(reanimation2);
                    newGridItem.mGridItemState = GridItemState.ZenToolGoldWateringCan;
                    mApp.PlayFoley(FoleyType.Watering);
                }
                else if (theCursorType == CursorType.WateringCan)
                {
                    Reanimation reanimation3 = mApp.AddReanimation(plant.mX + 32, plant.mY, 0, ReanimationType.ZengardenWateringcan);
                    reanimation3.PlayReanim("anim_water", ReanimLoopType.PlayOnceAndHold, 0, 0f);
                    newGridItem.mGridItemReanimID = mApp.ReanimationGetID(reanimation3);
                    newGridItem.mGridItemState = GridItemState.ZenToolWateringCan;
                    mApp.PlayFoley(FoleyType.Watering);
                }
                else if (theCursorType == CursorType.Fertilizer)
                {
                    Reanimation reanimation4 = mApp.AddReanimation(plant.mX, plant.mY, 0, ReanimationType.ZengardenFertilizer);
                    reanimation4.mLoopType = ReanimLoopType.PlayOnceAndHold;
                    newGridItem.mGridItemReanimID = mApp.ReanimationGetID(reanimation4);
                    newGridItem.mGridItemState = GridItemState.ZenToolFertilizer;
                    mApp.PlayFoley(FoleyType.Fertilizer);
                    Debug.ASSERT(mApp.mPlayerInfo.mPurchases[14] > 1000);
                    mApp.mPlayerInfo.mPurchases[14]--;
                }
                else if (theCursorType == CursorType.BugSpray)
                {
                    Reanimation reanimation5 = mApp.AddReanimation(plant.mX + 54, plant.mY, 0, ReanimationType.ZengardenBugspray);
                    reanimation5.mLoopType = ReanimLoopType.PlayOnceAndHold;
                    newGridItem.mGridItemReanimID = mApp.ReanimationGetID(reanimation5);
                    newGridItem.mGridItemState = GridItemState.ZenToolBugSpray;
                    mApp.PlayFoley(FoleyType.Bugspray);
                    Debug.ASSERT(mApp.mPlayerInfo.mPurchases[15] > 1000);
                    mApp.mPlayerInfo.mPurchases[15]--;
                }
                else if (theCursorType == CursorType.Phonograph)
                {
                    Reanimation reanimation6 = mApp.AddReanimation(plant.mX + 20, plant.mY + 34, 0, ReanimationType.ZengardenPhonograph);
                    reanimation6.mAnimRate = 20f;
                    reanimation6.mLoopType = ReanimLoopType.Loop;
                    newGridItem.mGridItemReanimID = mApp.ReanimationGetID(reanimation6);
                    newGridItem.mGridItemState = GridItemState.ZenToolPhonograph;
                    mApp.PlayFoley(FoleyType.Phonograph);
                }
            }
            mBoard.ClearCursor();
        }

        public void DrawPlantOverlay(Graphics g, Plant thePlant)
        {
            if (thePlant.mPottedPlantIndex == -1)
            {
                return;
            }
            PottedPlant thePottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            PottedPlantNeed plantsNeed = mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
            if (plantsNeed == PottedPlantNeed.None)
            {
                return;
            }
            g.DrawImage(AtlasResources.IMAGE_PLANTSPEECHBUBBLE, Constants.ZenGarden_PlantSpeechBubble_Pos.X, Constants.ZenGarden_PlantSpeechBubble_Pos.Y);
            switch (plantsNeed)
            {
            case PottedPlantNeed.Water:
                g.DrawImage(AtlasResources.IMAGE_WATERDROP, Constants.ZenGarden_WaterDrop_Pos.X, Constants.ZenGarden_WaterDrop_Pos.Y);
                return;
            case PottedPlantNeed.Fertilizer:
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_FERTILIZER_BAG3, Constants.ZenGarden_Fertiliser_Pos.X, Constants.ZenGarden_Fertiliser_Pos.Y, 0.5f, 0.5f);
                return;
            case PottedPlantNeed.Bugspray:
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_BUGSPRAY_BOTTLE, Constants.ZenGarden_BugSpray_Pos.X, Constants.ZenGarden_BugSpray_Pos.Y, 0.5f, 0.5f);
                return;
            case PottedPlantNeed.Phonograph:
                TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_PHONOGRAPH, Constants.ZenGarden_Phonograph_Pos.X, Constants.ZenGarden_Phonograph_Pos.Y, 0.5f, 0.5f);
                return;
            default:
                return;
            }
        }

        public PottedPlant PottedPlantFromIndex(int thePottedPlantIndex)
        {
            Debug.ASSERT(thePottedPlantIndex >= 0 && thePottedPlantIndex < mApp.mPlayerInfo.mNumPottedPlants);
            return mApp.mPlayerInfo.mPottedPlant[thePottedPlantIndex];
        }

        public bool WasPlantNeedFulfilledToday(PottedPlant thePottedPlant)
        {
            TimeSpan timeSpan = aNow - thePottedPlant.mLastNeedFulfilledTime;
            double totalSeconds = timeSpan.TotalSeconds;
            return timeSpan.TotalDays < 1.0;
        }

        public void PottedPlantUpdate(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            if (pottedPlant.mLastWateredTime > aNow || pottedPlant.mLastNeedFulfilledTime > aNow || pottedPlant.mLastFertilizedTime > aNow || pottedPlant.mLastChocolateTime > aNow)
            {
                ResetPlantTimers(pottedPlant);
            }
            if (thePlant.mIsAsleep)
            {
                return;
            }
            if (thePlant.mStateCountdown > 0)
            {
                thePlant.mStateCountdown--;
            }
            if (pottedPlant.mPlantAge == PottedPlantAge.Full && WasPlantNeedFulfilledToday(pottedPlant))
            {
                PlantUpdateProduction(thePlant);
            }
            UpdatePlantEffectState(thePlant);
        }

        public void AddHappyEffect(Plant thePlant)
        {
            Plant topPlantAt = mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, TopPlant.OnlyUnderPlant);
            if (topPlantAt == null)
            {
                thePlant.AddAttachedParticle(thePlant.mX + 40, thePlant.mY + 60, thePlant.mRenderOrder - 1, ParticleEffect.PottedZenGlow);
                return;
            }
            if (Plant.IsAquatic(thePlant.mSeedType))
            {
                topPlantAt.AddAttachedParticle(topPlantAt.mX + 40, topPlantAt.mY + 61, topPlantAt.mRenderOrder - 1, ParticleEffect.PottedWaterPlantGlow);
                return;
            }
            topPlantAt.AddAttachedParticle(topPlantAt.mX + 40, topPlantAt.mY + 63, topPlantAt.mRenderOrder - 1, ParticleEffect.PottedPlantGlow);
        }

        public void RemoveHappyEffect(Plant thePlant)
        {
            Plant topPlantAt = mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, TopPlant.OnlyUnderPlant);
            TodParticleSystem todParticleSystem;
            if (topPlantAt != null)
            {
                todParticleSystem = mApp.ParticleTryToGet(topPlantAt.mParticleID);
            }
            else
            {
                todParticleSystem = mApp.ParticleTryToGet(thePlant.mParticleID);
            }
            if (todParticleSystem != null)
            {
                todParticleSystem.ParticleSystemDie();
            }
        }

        public void PlantUpdateProduction(Plant thePlant)
        {
            thePlant.mLaunchCounter--;
            SetPlantAnimSpeed(thePlant);
            PottedPlant thePottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            if (PlantHighOnChocolate(thePottedPlant))
            {
                thePlant.mLaunchCounter--;
            }
            if (thePlant.mLaunchCounter <= 0)
            {
                PlantSetLaunchCounter(thePlant);
                mApp.PlayFoley(FoleyType.SpawnSun);
                int num = RandomNumbers.NextNumber(1000);
                int theTimeAge = PlantGetMinutesSinceHappy(thePlant);
                num += TodCommon.TodAnimateCurve(5, 30, theTimeAge, 0, 80, TodCurves.Linear);
                CoinType theCoinType = CoinType.Silver;
                if (num < 100)
                {
                    theCoinType = CoinType.Gold;
                }
                mBoard.AddCoin(thePlant.mX, thePlant.mY, theCoinType, CoinMotion.Coin);
            }
        }

        public bool CanDropPottedPlantLoot()
        {
            return mApp.HasFinishedAdventure() && !mApp.mZenGarden.IsZenGardenFull(true);
        }

        public void ShowTutorialArrowOnWateringCan()
        {
            TRect aZenButtonRect = mBoard.GetZenButtonRect(GameObjectType.WateringCan);
            mBoard.TutorialArrowShow(aZenButtonRect.mX + Constants.ZenGarden_TutorialArrow_Offset, (int)(aZenButtonRect.mY + Constants.S * 10f));
            mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_PICK_UP_WATER]", MessageStyle.ZenGardenLong, AdviceType.None);
            mBoard.mTutorialState = TutorialState.ZenGardenPickupWater;
            mApp.mPlayerInfo.mIsInZenTutorial = true;
            mApp.mPlayerInfo.mZenTutorialMessage = 22;
        }

        public bool PlantsNeedWater()
        {
            for (int i = 0; i < mApp.mPlayerInfo.mNumPottedPlants; i++)
            {
                PottedPlant thePottedPlant = PottedPlantFromIndex(i);
                PottedPlantNeed plantsNeed = mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
                if (plantsNeed == PottedPlantNeed.Water)
                {
                    return true;
                }
            }
            return false;
        }

        public void ZenGardenStart()
        {
        }

        public void UpdatePlantEffectState(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            PlantState thePlantState = thePlant.mState;
            PottedPlantNeed plantsNeed = GetPlantsNeed(pottedPlant);
            if (plantsNeed == PottedPlantNeed.Water)
            {
                thePlant.mState = PlantState.Notready;
            }
            else if (plantsNeed == PottedPlantNeed.None)
            {
                if (WasPlantNeedFulfilledToday(pottedPlant))
                {
                    thePlant.mState = PlantState.ZenGardenHappy;
                }
                else if (thePlant.mIsAsleep)
                {
                    thePlant.mState = PlantState.Notready;
                }
                else
                {
                    thePlant.mState = PlantState.ZenGardenWatered;
                }
            }
            else
            {
                thePlant.mState = PlantState.ZenGardenNeedy;
            }
            if (thePlantState == thePlant.mState)
            {
                return;
            }
            Plant topPlantAt = mBoard.GetTopPlantAt(thePlant.mPlantCol, thePlant.mRow, TopPlant.OnlyUnderPlant);
            if (topPlantAt != null && !Plant.IsAquatic(thePlant.mSeedType))
            {
                Reanimation reanimation = mApp.ReanimationGet(topPlantAt.mBodyReanimID);
                if (thePlant.mState == PlantState.ZenGardenWatered || thePlant.mState == PlantState.ZenGardenNeedy || thePlant.mState == PlantState.ZenGardenHappy)
                {
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_pot_top, AtlasResources.IMAGE_REANIM_POT_TOP_DARK);
                }
                else
                {
                    Image theImage = null;
                    reanimation.SetImageOverride(GlobalMembersReanimIds.ReanimTrackId_pot_top, theImage);
                }
            }
            if (thePlantState == PlantState.ZenGardenHappy)
            {
                RemoveHappyEffect(thePlant);
            }
            if (thePlant.mState == PlantState.ZenGardenHappy)
            {
                thePlant.SetSleeping(false);
                AddHappyEffect(thePlant);
                return;
            }
            if (Plant.IsNocturnal(thePlant.mSeedType) && !mBoard.StageIsNight())
            {
                thePlant.SetSleeping(true);
            }
        }

        public void ZenToolUpdate(GridItem theZenTool)
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(theZenTool.mGridItemReanimID);
            if (reanimation == null)
            {
                return;
            }
            int num = 1;
            if (theZenTool.mGridItemState == GridItemState.ZenToolPhonograph)
            {
                num = 2;
            }
            if (reanimation.mLoopCount >= num)
            {
                DoFeedingTool((int)theZenTool.mPosX, (int)theZenTool.mPosY, theZenTool.mGridItemState);
                theZenTool.GridItemDie();
            }
        }

        public void DoFeedingTool(int x, int y, GridItemState theToolType)
        {
            if (theToolType == GridItemState.ZenToolGoldWateringCan)
            {
                int count = mBoard.mPlants.Count;
                for (int i = 0; i < count; i++)
                {
                    Plant plant = mBoard.mPlants[i];
                    if (!plant.mDead && mBoard.IsPlantInGoldWateringCanRange(x, y, plant))
                    {
                        PottedPlant thePottedPlant = PottedPlantFromIndex(plant.mPottedPlantIndex);
                        PottedPlantNeed plantsNeed = GetPlantsNeed(thePottedPlant);
                        if (plantsNeed == PottedPlantNeed.Water)
                        {
                            PlantWatered(plant);
                        }
                    }
                }
                return;
            }
            int theGridX = PixelToGridX(x, y);
            int theGridY = PixelToGridY(x, y);
            Plant topPlantAt = mBoard.GetTopPlantAt(theGridX, theGridY, TopPlant.ZenToolOrder);
            if (topPlantAt != null)
            {
                PottedPlant thePottedPlant2 = PottedPlantFromIndex(topPlantAt.mPottedPlantIndex);
                PottedPlantNeed plantsNeed2 = GetPlantsNeed(thePottedPlant2);
                if (plantsNeed2 == PottedPlantNeed.Water && theToolType == GridItemState.ZenToolWateringCan)
                {
                    PlantWatered(topPlantAt);
                }
                else if (plantsNeed2 == PottedPlantNeed.Fertilizer && theToolType == GridItemState.ZenToolFertilizer)
                {
                    PlantFertilized(topPlantAt);
                }
                else if (plantsNeed2 == PottedPlantNeed.Bugspray && theToolType == GridItemState.ZenToolBugSpray)
                {
                    PlantFulfillNeed(topPlantAt);
                }
                else if (plantsNeed2 == PottedPlantNeed.Phonograph && theToolType == GridItemState.ZenToolPhonograph)
                {
                    PlantFulfillNeed(topPlantAt);
                }
                if (mBoard.mTutorialState == TutorialState.ZenGardenFertilizePlants && theToolType == GridItemState.ZenToolFertilizer)
                {
                    if (AllPlantsHaveBeenFertilized())
                    {
                        mApp.mBoard.mTutorialState = TutorialState.ZenGardenCompleted;
                        mApp.mPlayerInfo.mZenTutorialMessage = 27;
                        mApp.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_CONTINUE_ADVENTURE]", MessageStyle.HintTallFast, AdviceType.None);
                        mBoard.mMenuButton.mDisabled = false;
                        mBoard.mMenuButton.mBtnNoDraw = false;
                        return;
                    }
                    if (mApp.mPlayerInfo.mPurchases[14] == 1000)
                    {
                        mApp.mPlayerInfo.mPurchases[14] = 1005;
                        mApp.mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_NEED_MORE_FERTILIZER]", MessageStyle.HintTallFast, AdviceType.None);
                    }
                }
            }
        }

        public void AddStinky()
        {
            if (!HasPurchasedStinky())
            {
                return;
            }
            if (mGardenType != GardenType.Main)
            {
                return;
            }
            if (!mApp.mPlayerInfo.mHasSeenStinky)
            {
                mApp.mPlayerInfo.mHasSeenStinky = true;
                mApp.mPlayerInfo.mPurchases[20] = GetStinkyTime();
            }
            GridItem newGridItem = GridItem.GetNewGridItem();
            newGridItem.mGridItemType = GridItemType.Stinky;
            newGridItem.mPosX = mApp.mPlayerInfo.mStinkyPosX;
            newGridItem.mPosY = mApp.mPlayerInfo.mStinkyPosY;
            newGridItem.mGoalX = newGridItem.mPosX;
            newGridItem.mGoalY = newGridItem.mPosY;
            mBoard.mGridItems.Add(newGridItem);
            Reanimation reanimation = mApp.AddReanimation(newGridItem.mPosX * Constants.S, newGridItem.mPosY * Constants.S, 0, ReanimationType.Stinky);
            reanimation.OverrideScale(0.8f, 0.8f);
            newGridItem.mGridItemReanimID = mApp.ReanimationGetID(reanimation);
            if (mApp.mPlayerInfo.mStinkyPosX == 0)
            {
                StinkyPickGoal(newGridItem);
                newGridItem.mPosX = newGridItem.mGoalX;
                newGridItem.mPosY = newGridItem.mGoalY;
            }
            if (ShouldStinkyBeAwake())
            {
                reanimation.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.Loop, 0, 6f);
                newGridItem.mGridItemState = GridItemState.StinkyWalkingLeft;
            }
            else
            {
                newGridItem.mPosY = Constants.STINKY_SLEEP_POS_Y;
                StinkyFinishFallingAsleep(newGridItem, 0);
            }
            newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Plant, 0, (int)newGridItem.mPosY - 30);
            reanimation.SetPosition(newGridItem.mPosX * Constants.S, newGridItem.mPosY * Constants.S);
        }

        private int GetStinkyTime()
        {
            return (int)(TimeSpan.FromTicks(aNow.Ticks).TotalSeconds - STINKY_BASE_TIME);
        }

        public void StinkyUpdate(GridItem theStinky)
        {
            Reanimation aStinkyReanim = mApp.ReanimationGet(theStinky.mGridItemReanimID);
            if (mApp.mPlayerInfo.mLastStinkyChocolateTime > aNow || mApp.mPlayerInfo.mPurchases[20] > GetStinkyTime())
            {
                ResetStinkyTimers();
            }
            bool aStinkyHighOnChocolate = IsStinkyHighOnChocolate();
            UpdateStinkyMotionTrail(theStinky, aStinkyHighOnChocolate);
            if (theStinky.mGridItemState == GridItemState.StinkyFallingAsleep)
            {
                if (aStinkyReanim.mLoopCount > 0)
                {
                    StinkyFinishFallingAsleep(theStinky, 20);
                }
                return;
            }
            if (theStinky.mGridItemState == GridItemState.StinkySleeping)
            {
                ReanimatorTrackInstance trackInstanceByName = aStinkyReanim.GetTrackInstanceByName("shell");
                Reanimation reanimation2 = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName.mAttachmentID);
                Debug.ASSERT(reanimation2 != null);
                if (mBoard.mCursorObject.mCursorType == CursorType.Chocolate)
                {
                    reanimation2.AssignRenderGroupToPrefix("z", -1);
                }
                else
                {
                    reanimation2.AssignRenderGroupToPrefix("z", 0);
                }
                if (ShouldStinkyBeAwake())
                {
                    StinkyWakeUp(theStinky);
                }
                return;
            }
            if (theStinky.mGridItemState == GridItemState.StinkyWakingUp)
            {
                if (aStinkyReanim.mLoopCount > 0)
                {
                    theStinky.mGridItemState = GridItemState.StinkyWalkingLeft;
                    aStinkyReanim.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.Loop, 10, 6f);
                    StinkyPickGoal(theStinky);
                }
                return;
            }
            if (!ShouldStinkyBeAwake())
            {
                if (theStinky.mPosY < Constants.STINKY_SLEEP_POS_Y)
                {
                    if (theStinky.mGoalY != Constants.STINKY_SLEEP_POS_Y)
                    {
                        theStinky.mGoalY = Constants.STINKY_SLEEP_POS_Y + 10f;
                    }
                }
                else
                {
                    if (theStinky.mGridItemState == GridItemState.StinkyWalkingLeft)
                    {
                        StinkyStartFallingAsleep(theStinky);
                        return;
                    }
                    if (theStinky.mGridItemState == GridItemState.StinkyWalkingRight)
                    {
                        Reanimation reanimation3 = mApp.ReanimationGet(theStinky.mGridItemReanimID);
                        theStinky.mGridItemState = GridItemState.StinkyTurningLeft;
                        reanimation3.PlayReanim("turn", ReanimLoopType.PlayOnceAndHold, 10, 6f);
                        theStinky.mMotionTrailCount = 0;
                        theStinky.mGoalX = theStinky.mPosX;
                        theStinky.mGoalY = theStinky.mPosY;
                        return;
                    }
                }
            }
            if (theStinky.mGridItemCounter > 0)
            {
                theStinky.mGridItemCounter--;
            }
            SexyVector2 lhs = new SexyVector2(theStinky.mPosX, theStinky.mPosY);
            SexyVector2 rhs = default(SexyVector2);
            Coin coin = null;
            while (mBoard.IterateCoins(ref coin))
            {
                if (!coin.mIsBeingCollected)
                {
                    rhs.x = coin.mPosX;
                    rhs.y = coin.mPosY + 30f;
                    float num = (lhs - rhs).Magnitude();
                    if (num < 20f)
                    {
                        coin.PlayCollectSound();
                        coin.Collect();
                    }
                }
            }
            if (theStinky.mGridItemState == GridItemState.StinkyWalkingLeft || theStinky.mGridItemState == GridItemState.StinkyWalkingRight)
            {
                if (mBoard.mCursorObject.mCursorType == CursorType.Chocolate && !IsStinkyHighOnChocolate())
                {
                    if (!aStinkyReanim.IsAnimPlaying("anim_idle"))
                    {
                        aStinkyReanim.PlayReanim("anim_idle", ReanimLoopType.Loop, 10, 6f);
                    }
                }
                else if (!aStinkyReanim.IsAnimPlaying(Reanimation.ReanimTrackId_anim_crawl))
                {
                    aStinkyReanim.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.Loop, 10, 6f);
                }
            }
            float aDeltaX = theStinky.mPosX - theStinky.mGoalX;
            float aDeltaY = theStinky.mPosY - theStinky.mGoalY;
            float aSpeedY = 0.5f;
            float aSpeedX = aStinkyReanim.GetTrackVelocity(Reanimation.ReanimTrackId__ground) * 5f;
            if (aStinkyHighOnChocolate)
            {
                aSpeedY = 1f;
                aSpeedX = Math.Max(aSpeedX, 0.5f);
            }
            else if (mBoard.mCursorObject.mCursorType == CursorType.Chocolate)
            {
                aSpeedY = 0f;
                aSpeedX = 0f;
            }
            aSpeedY *= TodCommon.TodAnimateCurveFloatTime(20f, 5f, Math.Abs(aDeltaY), 1f, 0.2f, TodCurves.Linear);
            if (theStinky.mGridItemState == GridItemState.StinkyWalkingLeft)
            {
                theStinky.mPosX -= aSpeedX;
                if (theStinky.mPosX < theStinky.mGoalX)
                {
                    theStinky.mPosX = theStinky.mGoalX;
                }
            }
            else if (theStinky.mGridItemState == GridItemState.StinkyWalkingRight)
            {
                theStinky.mPosX += aSpeedX;
                if (theStinky.mPosX > theStinky.mGoalX)
                {
                    theStinky.mPosX = theStinky.mGoalX;
                }
            }
            if (theStinky.mGridItemState == GridItemState.StinkyWalkingLeft || theStinky.mGridItemState == GridItemState.StinkyWalkingRight)
            {
                if (Math.Abs(aDeltaY) < aSpeedY)
                {
                    theStinky.mPosY = theStinky.mGoalY;
                }
                else if (aDeltaY > 0f)
                {
                    theStinky.mPosY -= aSpeedY;
                }
                else
                {
                    theStinky.mPosY += aSpeedY;
                }
                if (Math.Abs(aDeltaX) < 5f && Math.Abs(aDeltaY) < 5f)
                {
                    StinkyPickGoal(theStinky);
                }
                else if (theStinky.mGridItemCounter == 0)
                {
                    StinkyPickGoal(theStinky);
                }
            }
            if (theStinky.mGridItemState == GridItemState.StinkyTurningLeft)
            {
                if (aStinkyReanim.mLoopCount > 0)
                {
                    theStinky.mGridItemState = GridItemState.StinkyWalkingLeft;
                    aStinkyReanim.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.Loop, 10, 6f);
                }
            }
            else if (theStinky.mGridItemState == GridItemState.StinkyTurningRight && aStinkyReanim.mLoopCount > 0)
            {
                theStinky.mGridItemState = GridItemState.StinkyWalkingRight;
                aStinkyReanim.PlayReanim(Reanimation.ReanimTrackId_anim_crawl, ReanimLoopType.Loop, 10, 6f);
            }
            StinkyAnimRateUpdate(theStinky);
            if (theStinky.mGridItemState == GridItemState.StinkyWalkingRight || theStinky.mGridItemState == GridItemState.StinkyTurningLeft)
            {
                aStinkyReanim.OverrideScale(-0.8f, 0.8f);
                aStinkyReanim.SetPosition((theStinky.mPosX + 69f) * Constants.S, theStinky.mPosY * Constants.S);
            }
            else
            {
                aStinkyReanim.OverrideScale(0.8f, 0.8f);
                aStinkyReanim.SetPosition(theStinky.mPosX * Constants.S, theStinky.mPosY * Constants.S);
            }
            theStinky.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Plant, 0, (int)theStinky.mPosY - 30);
        }

        public void OpenStore()
        {
            LeaveGarden();
            StoreScreen storeScreen = mApp.ShowStoreScreen(this);
            storeScreen.SetupBackButtonForZenGarden();
            if (mBoard.mTutorialState == TutorialState.ZenGardenVisitStore)
            {
                storeScreen.SetupForIntro(2600);
                mApp.mPlayerInfo.mPurchases[14] = 1005;
            }
            storeScreen.mBackButton.SetLabel("[STORE_BACK_TO_GAME]");
            storeScreen.mPage = StorePage.Zen1;
        }

        public GridItem GetStinky()
        {
            int num = -1;
            GridItem aGridItem = null;
            while (mBoard.IterateGridItems(ref aGridItem, ref num))
            {
                if (aGridItem.mGridItemType == GridItemType.Stinky)
                {
                    return aGridItem;
                }
            }
            return null;
        }

        public void StinkyPickGoal(GridItem theStinky)
        {
            float aDistFromGoal = TodCommon.Distance2D(theStinky.mGoalX, theStinky.mGoalY, theStinky.mPosX, theStinky.mPosY);
            Coin coin = null;
            float num2 = 0f;
            Coin aCoin = null;
            while (mBoard.IterateCoins(ref aCoin))
            {
                if (!aCoin.mIsBeingCollected && aCoin.mPosY == aCoin.mGroundY)
                {
                    float aCurDistToGoal = TodCommon.Distance2D(aCoin.mPosX, aCoin.mPosY + 30f, theStinky.mPosX, theStinky.mPosY);
                    if (aCoin.mType == CoinType.Gold)
                    {
                        aCurDistToGoal -= 40f;
                    }
                    else if (aCoin.mType == CoinType.Diamond)
                    {
                        aCurDistToGoal -= 80f;
                    }
                    float aCoinDistFromLastGoal = TodCommon.Distance2D(aCoin.mPosX, aCoin.mPosY + 30f, theStinky.mGoalX, theStinky.mGoalY);
                    if (aCoinDistFromLastGoal < 5f)
                    {
                        aCurDistToGoal -= 20f;
                    }
                    if (aCoinDistFromLastGoal < 5f)
                    {
                        aCurDistToGoal += TodCommon.TodAnimateCurve(3000, 6000, aCoin.mDisappearCounter, 0, -40, TodCurves.Linear);
                    }
                    if (coin == null || aCurDistToGoal < num2)
                    {
                        coin = aCoin;
                        num2 = aCurDistToGoal;
                    }
                }
            }
            if (coin != null)
            {
                theStinky.mGoalX = coin.mPosX;
                theStinky.mGoalY = coin.mPosY + 30f;
            }
            else
            {
                if (aDistFromGoal > 10f)
                {
                    return;
                }
                TodWeightedGridArray[] aPicks = new TodWeightedGridArray[Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY];
                for (int i = 0; i < aPicks.Length; i++)
                {
                    aPicks[i] = TodWeightedGridArray.GetNewTodWeightedGridArray();
                }
                int aPickCount = 0;
                int aCount = 0;
                SpecialGridPlacement[] specialGridPlacements = GetSpecialGridPlacements(ref aCount);
                Debug.ASSERT(aCount < Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY);
                for (int j = 0; j < aCount; j++)
                {
                    SpecialGridPlacement specialGridPlacement = specialGridPlacements[j];
                    Plant topPlantAt = mBoard.GetTopPlantAt(specialGridPlacement.mGridX, specialGridPlacement.mGridY, TopPlant.Any);
                    aPicks[aPickCount].mX = specialGridPlacement.mPixelX + 15;
                    aPicks[aPickCount].mY = specialGridPlacement.mPixelY + 80;
                    if (topPlantAt != null)
                    {
                        aPicks[aPickCount].mWeight = 2000;
                        aPicks[aPickCount].mWeight -= (int)Math.Abs(aPicks[aPickCount].mY - theStinky.mPosY);
                    }
                    else
                    {
                        aPicks[aPickCount].mWeight = 1;
                    }
                    aPickCount++;
                }
                TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(aPicks, aPickCount);
                theStinky.mGoalX = todWeightedGridArray.mX;
                theStinky.mGoalY = todWeightedGridArray.mY;
            }
            theStinky.mGridItemCounter = 100;
            if (theStinky.mGoalX < theStinky.mPosX && theStinky.mGridItemState == GridItemState.StinkyWalkingRight)
            {
                Reanimation reanimation = mApp.ReanimationGet(theStinky.mGridItemReanimID);
                theStinky.mGridItemState = GridItemState.StinkyTurningLeft;
                reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_turn, ReanimLoopType.PlayOnceAndHold, 10, 6f);
                theStinky.mMotionTrailCount = 0;
                return;
            }
            if (theStinky.mGoalX > theStinky.mPosX && theStinky.mGridItemState == GridItemState.StinkyWalkingLeft)
            {
                Reanimation reanimation2 = mApp.ReanimationGet(theStinky.mGridItemReanimID);
                theStinky.mGridItemState = GridItemState.StinkyTurningRight;
                reanimation2.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_turn, ReanimLoopType.PlayOnceAndHold, 10, 6f);
                theStinky.mMotionTrailCount = 0;
            }
        }

        public bool PlantShouldRefreshNeed(PottedPlant thePottedPlant)
        {
            TimeSpan timeSpan = aNow - thePottedPlant.mLastWateredTime;
            return timeSpan.TotalSeconds >= 3600.0 && timeSpan.TotalDays >= 1.0;
        }

        public void PlantFertilized(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            pottedPlant.mLastFertilizedTime = aNow;
            pottedPlant.mPlantNeed = PottedPlantNeed.None;
            pottedPlant.mTimesFed = 0;
            pottedPlant.mPlantAge++;
            if (pottedPlant.mPlantAge == PottedPlantAge.Small)
            {
                RemovePottedPlant(thePlant);
                PlacePottedPlant(thePlant.mPottedPlantIndex);
                mApp.PlaySample(Resources.SOUND_LOADINGBAR_FLOWER);
            }
            else
            {
                thePlant.mStateCountdown = 100;
                mApp.PlayFoley(FoleyType.Plantgrow);
            }
            mApp.PlayFoley(FoleyType.SpawnSun);
            if (pottedPlant.mPlantAge == PottedPlantAge.Small)
            {
                mBoard.AddCoin(thePlant.mX + 40, thePlant.mY, CoinType.Gold, CoinMotion.Coin);
                return;
            }
            if (pottedPlant.mPlantAge == PottedPlantAge.Medium)
            {
                mBoard.AddCoin(thePlant.mX + 30, thePlant.mY, CoinType.Gold, CoinMotion.Coin);
                mBoard.AddCoin(thePlant.mX + 50, thePlant.mY, CoinType.Gold, CoinMotion.Coin);
                return;
            }
            if (pottedPlant.mPlantAge == PottedPlantAge.Full)
            {
                if (pottedPlant.mSeedType == SeedType.Marigold)
                {
                    mBoard.AddCoin(thePlant.mX + 40, thePlant.mY, CoinType.Diamond, CoinMotion.Coin);
                    return;
                }
                mBoard.AddCoin(thePlant.mX + 10, thePlant.mY, CoinType.Diamond, CoinMotion.Coin);
                mBoard.AddCoin(thePlant.mX + 70, thePlant.mY, CoinType.Diamond, CoinMotion.Coin);
            }
        }

        public bool WasPlantFertilizedInLastHour(PottedPlant thePottedPlant)
        {
            return (aNow - thePottedPlant.mLastFertilizedTime).TotalSeconds < 3600.0;
        }

        public void SetupForZenTutorial()
        {
            mBoard.mMenuButton.SetLabel("[CONTINUE_BUTTON]");
            mBoard.mStoreButton.mDisabled = true;
            mBoard.mStoreButton.mBtnNoDraw = true;
            mBoard.mMenuButton.mDisabled = true;
            mBoard.mMenuButton.mBtnNoDraw = true;
            mIsTutorial = true;
            if (mApp.mPlayerInfo.mIsDaveTalkingZenTutorial)
            {
                mApp.CrazyDaveEnter();
                mApp.CrazyDaveTalkIndex(mApp.mPlayerInfo.mZenTutorialMessage);
                return;
            }
            if (!mApp.mPlayerInfo.mIsInZenTutorial)
            {
                mApp.mPlayerInfo.mIsDaveTalkingZenTutorial = true;
                mApp.mPlayerInfo.mZenTutorialMessage = 2050;
                mApp.CrazyDaveEnter();
                mApp.CrazyDaveTalkIndex(2050);
                return;
            }
            mBoard.mTutorialState = (TutorialState)mApp.mPlayerInfo.mZenTutorialMessage;
            if (mBoard.mTutorialState == TutorialState.ZenGardenPickupWater)
            {
                ShowTutorialArrowOnWateringCan();
                return;
            }
            if (mBoard.mTutorialState == TutorialState.ZenGardenKeepWatering)
            {
                mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_KEEP_WATERING]", MessageStyle.ZenGardenLong, AdviceType.None);
                return;
            }
            if (mBoard.mTutorialState == TutorialState.ZenGardenVisitStore)
            {
                mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_VISIT_STORE]", MessageStyle.HintTallLong, AdviceType.None);
                mBoard.mStoreButton.mDisabled = false;
                mBoard.mStoreButton.mBtnNoDraw = false;
                return;
            }
            if (mBoard.mTutorialState == TutorialState.ZenGardenFertilizePlants)
            {
                mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_FERTILIZE]", MessageStyle.ZenGardenLong, AdviceType.None);
                return;
            }
            if (mBoard.mTutorialState == TutorialState.ZenGardenCompleted)
            {
                mIsTutorial = false;
                mApp.mPlayerInfo.mZenGardenTutorialComplete = true;
                mApp.mPlayerInfo.mIsInZenTutorial = false;
            }
        }

        public bool HasPurchasedStinky()
        {
            return mApp.mPlayerInfo.mPurchases[20] != 0;
        }

        public int CountPlantsNeedingFertilizer()
        {
            int num = 0;
            for (int i = 0; i < mApp.mPlayerInfo.mNumPottedPlants; i++)
            {
                PottedPlant thePottedPlant = PottedPlantFromIndex(i);
                PottedPlantNeed plantsNeed = mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
                if (plantsNeed == PottedPlantNeed.Fertilizer)
                {
                    num++;
                }
            }
            return num;
        }

        public bool AllPlantsHaveBeenFertilized()
        {
            for (int i = 0; i < mApp.mPlayerInfo.mNumPottedPlants; i++)
            {
                PottedPlant pottedPlant = PottedPlantFromIndex(i);
                if (pottedPlant.mPlantAge == PottedPlantAge.Sprout)
                {
                    return false;
                }
            }
            return true;
        }

        public void WakeStinky()
        {
            mApp.mPlayerInfo.mPurchases[20] = GetStinkyTime();
            mApp.PlaySample(Resources.SOUND_TAP);
            mBoard.ClearAdvice(AdviceType.StinkySleeping);
            GlobalStaticVars.gLawnApp.mPlayerInfo.mHasWokenStinky = true;
        }

        public bool ShouldStinkyBeAwake()
        {
            if (IsStinkyHighOnChocolate())
            {
                return true;
            }
            int num = (int)(TimeSpan.FromTicks(aNow.Ticks).TotalSeconds - STINKY_BASE_TIME - mApp.mPlayerInfo.mPurchases[20]);
            int num2 = 180;
            return num <= num2;
        }

        public bool IsStinkySleeping()
        {
            GridItem stinky = GetStinky();
            return stinky != null && stinky.mGridItemState == GridItemState.StinkySleeping;
        }

        public SeedType PickRandomSeedType()
        {
            SeedType[] aSeedList = new SeedType[40];
            int num = 0;
            for (int i = 0; i < 40; i++)
            {
                SeedType seedType = (SeedType)i;
                if (seedType != SeedType.Marigold && seedType != SeedType.Flowerpot)
                {
                    aSeedList[num] = seedType;
                    num++;
                }
            }
            int num2 = RandomNumbers.NextNumber(num);
            SeedType seedType2 = aSeedList[num2];
            return aSeedList[num2];
        }

        public void StinkyWakeUp(GridItem theStinky)
        {
            Reanimation reanimation = mApp.ReanimationGet(theStinky.mGridItemReanimID);
            reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_out, ReanimLoopType.PlayOnceAndHold, 20, 6f);
            theStinky.mGridItemState = GridItemState.StinkyWakingUp;
            ReanimatorTrackInstance trackInstanceByName = reanimation.GetTrackInstanceByName(GlobalMembersReanimIds.ReanimTrackId_shell);
            Reanimation reanimation2 = GlobalMembersAttachment.FindReanimAttachment(trackInstanceByName.mAttachmentID);
            reanimation2.ReanimationDie();
            GlobalStaticVars.gLawnApp.mPlayerInfo.mHasWokenStinky = true;
        }

        public void StinkyStartFallingAsleep(GridItem theStinky)
        {
            Reanimation reanimation = mApp.ReanimationGet(theStinky.mGridItemReanimID);
            reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_in, ReanimLoopType.PlayOnceAndHold, 20, 6f);
            theStinky.mGridItemState = GridItemState.StinkyFallingAsleep;
        }

        public void StinkyFinishFallingAsleep(GridItem theStinky, byte theBlendTime)
        {
            Reanimation reanimation = mApp.ReanimationGet(theStinky.mGridItemReanimID);
            reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_out, ReanimLoopType.PlayOnceAndHold, theBlendTime, 0f);
            reanimation.mAnimRate = 0f;
            Reanimation reanimation2 = mApp.AddReanimation(0f, 0f, 0, ReanimationType.Sleeping);
            reanimation2.mLoopType = ReanimLoopType.Loop;
            reanimation2.mAnimRate = 3f;
            int num = reanimation.FindTrackIndex(GlobalMembersReanimIds.ReanimTrackId_shell);
            ReanimatorTrackInstance reanimatorTrackInstance = reanimation.mTrackInstances[num];
            GlobalMembersAttachment.AttachReanim(ref reanimatorTrackInstance.mAttachmentID, reanimation2, 34f * Constants.S, 39f * Constants.S);
            theStinky.mGridItemState = GridItemState.StinkySleeping;
            if (!GlobalStaticVars.gLawnApp.mPlayerInfo.mHasWokenStinky)
            {
                mApp.mBoard.DisplayAdvice("[ADVICE_STINKY_SLEEPING]", MessageStyle.HintLong, AdviceType.StinkySleeping);
            }
        }

        public void AdvanceCrazyDaveDialog()
        {
            if (mApp.mCrazyDaveMessageIndex == -1 || mApp.GetDialog(Dialogs.DIALOG_STORE) != null || mApp.GetDialog(Dialogs.DIALOG_ZEN_SELL) != null)
            {
                return;
            }
            if (mApp.mCrazyDaveMessageIndex == 2053 || mApp.mCrazyDaveMessageIndex == 2063)
            {
                mApp.mPlayerInfo.mIsDaveTalkingZenTutorial = false;
                ShowTutorialArrowOnWateringCan();
            }
            if (!mApp.AdvanceCrazyDaveText())
            {
                mApp.CrazyDaveLeave();
                return;
            }
            if (mApp.mCrazyDaveMessageIndex != 2054)
            {
                mApp.mPlayerInfo.mZenTutorialMessage = mApp.mCrazyDaveMessageIndex;
            }
            if ((mApp.mCrazyDaveMessageIndex == 2052 || mApp.mCrazyDaveMessageIndex == 2062) && mApp.mPlayerInfo.mNumPottedPlants == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    PottedPlant pottedPlant = new PottedPlant();
                    pottedPlant.InitializePottedPlant(SeedType.Marigold);
                    pottedPlant.mDrawVariation = (DrawVariation)TodCommon.RandRangeInt(2, 12);
                    pottedPlant.mFeedingsPerGrow = 3;
                    mApp.mZenGarden.AddPottedPlant(pottedPlant);
                }
            }
        }

        public void LeaveGarden()
        {
            int num = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num))
            {
                if (gridItem.mGridItemType == GridItemType.ZenTool)
                {
                    DoFeedingTool((int)gridItem.mPosX, (int)gridItem.mPosY, gridItem.mGridItemState);
                    gridItem.GridItemDie();
                }
                if (gridItem.mGridItemType == GridItemType.Stinky)
                {
                    mApp.mPlayerInfo.mStinkyPosX = (int)gridItem.mPosX;
                    mApp.mPlayerInfo.mStinkyPosY = (int)gridItem.mPosY;
                    gridItem.GridItemDie();
                }
            }
            Coin coin = null;
            while (mBoard.IterateCoins(ref coin))
            {
                if (coin.mIsBeingCollected)
                {
                    coin.ScoreCoin();
                }
                else
                {
                    coin.Die();
                }
            }
        }

        public bool CanDropChocolate()
        {
            return HasPurchasedStinky() && mApp.mPlayerInfo.mPurchases[26] - 1000 < 10;
        }

        public void FeedChocolateToPlant(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            pottedPlant.mLastChocolateTime = aNow;
            thePlant.mLaunchCounter = 200;
            mApp.AddTodParticle(thePlant.mX + 40f, thePlant.mY + 40f, thePlant.mRenderOrder + 1, ParticleEffect.PresentPickup);
        }

        public bool PlantHighOnChocolate(PottedPlant thePottedPlant)
        {
            return (aNow - thePottedPlant.mLastChocolateTime).TotalSeconds < 300.0;
        }

        public bool PlantCanHaveChocolate(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            return pottedPlant.mPlantAge == PottedPlantAge.Full && WasPlantNeedFulfilledToday(pottedPlant) && !PlantHighOnChocolate(pottedPlant);
        }

        public void SetPlantAnimSpeed(Plant thePlant)
        {
            Reanimation aBodyReanim = mApp.ReanimationGet(thePlant.mBodyReanimID);
            PottedPlant thePottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            bool flag = PlantHighOnChocolate(thePottedPlant);
            bool flag2 = aBodyReanim.mAnimRate >= 25f;
            if (flag2 == flag)
            {
                return;
            }
            float aTargetRate;
            if (thePlant.mSeedType == SeedType.Peashooter || thePlant.mSeedType == SeedType.Snowpea || thePlant.mSeedType == SeedType.Repeater || thePlant.mSeedType == SeedType.Leftpeater || thePlant.mSeedType == SeedType.Gatlingpea || thePlant.mSeedType == SeedType.Splitpea || thePlant.mSeedType == SeedType.Threepeater || thePlant.mSeedType == SeedType.Marigold)
            {
                aTargetRate = TodCommon.RandRangeFloat(15f, 20f);
            }
            else if (thePlant.mSeedType == SeedType.Potatomine)
            {
                aTargetRate = 12f;
            }
            else
            {
                aTargetRate = TodCommon.RandRangeFloat(10f, 15f);
            }
            if (flag)
            {
                aTargetRate *= 2f;
                aTargetRate = Math.Max(25f, aTargetRate);
            }
            aBodyReanim.mAnimRate = aTargetRate;
            Reanimation aHeadReanim = mApp.ReanimationTryToGet(thePlant.mHeadReanimID);
            Reanimation aHeadReanim2 = mApp.ReanimationTryToGet(thePlant.mHeadReanimID2);
            Reanimation aHeadReanim3 = mApp.ReanimationTryToGet(thePlant.mHeadReanimID3);
            if (aHeadReanim != null)
            {
                aHeadReanim.mAnimRate = aBodyReanim.mAnimRate;
                aHeadReanim.mAnimTime = aBodyReanim.mAnimTime;
            }
            if (aHeadReanim2 != null)
            {
                aHeadReanim2.mAnimRate = aBodyReanim.mAnimRate;
                aHeadReanim2.mAnimTime = aBodyReanim.mAnimTime;
            }
            if (aHeadReanim3 != null)
            {
                aHeadReanim3.mAnimRate = aBodyReanim.mAnimRate;
                aHeadReanim3.mAnimTime = aBodyReanim.mAnimTime;
            }
        }

        public void UpdateStinkyMotionTrail(GridItem theStinky, bool theStinkyHighOnChocolate)
        {
            Reanimation reanimation = mApp.ReanimationGet(theStinky.mGridItemReanimID);
            if (!theStinkyHighOnChocolate)
            {
                theStinky.mMotionTrailCount = 0;
                return;
            }
            if (theStinky.mGridItemState != GridItemState.StinkyWalkingRight && theStinky.mGridItemState != GridItemState.StinkyWalkingLeft)
            {
                theStinky.mMotionTrailCount = 0;
                return;
            }
            if (theStinky.mMotionTrailCount == 12)
            {
                theStinky.mMotionTrailCount--;
            }
            if (theStinky.mMotionTrailCount > 0)
            {
                Array.Copy(theStinky.mMotionTrailFrames, 1, theStinky.mMotionTrailFrames, 0, theStinky.mMotionTrailCount);
            }
            theStinky.mMotionTrailFrames[0].mPosX = theStinky.mPosX;
            theStinky.mMotionTrailFrames[0].mPosY = theStinky.mPosY;
            theStinky.mMotionTrailFrames[0].mAnimTime = reanimation.mAnimTime;
            theStinky.mMotionTrailCount++;
        }

        public void ResetPlantTimers(PottedPlant thePottedPlant)
        {
            thePottedPlant.mLastWateredTime = default(DateTime);
            thePottedPlant.mLastNeedFulfilledTime = default(DateTime);
            thePottedPlant.mLastFertilizedTime = default(DateTime);
            thePottedPlant.mLastChocolateTime = default(DateTime);
        }

        public void ResetStinkyTimers()
        {
            mApp.mPlayerInfo.mPurchases[20] = 2;
            mApp.mPlayerInfo.mLastStinkyChocolateTime = DateTime.MinValue;
        }

        public void UpdatePlantNeeds()
        {
            aNow = DateTime.UtcNow;
            if (mApp.mPlayerInfo == null)
            {
                return;
            }
            for (int i = 0; i < mApp.mPlayerInfo.mNumPottedPlants; i++)
            {
                PottedPlant thePottedPlant = PottedPlantFromIndex(i);
                RefreshPlantNeeds(thePottedPlant);
            }
        }

        public void RefreshPlantNeeds(PottedPlant thePottedPlant)
        {
            if (thePottedPlant.mPlantAge != PottedPlantAge.Full)
            {
                return;
            }
            if (!PlantShouldRefreshNeed(thePottedPlant))
            {
                return;
            }
            if (Plant.IsAquatic(thePottedPlant.mSeedType))
            {
                thePottedPlant.mLastWateredTime = aNow;
                thePottedPlant.mPlantNeed = (PottedPlantNeed)TodCommon.RandRangeInt(3, 4);
                return;
            }
            thePottedPlant.mTimesFed = 0;
            thePottedPlant.mPlantNeed = PottedPlantNeed.None;
        }

        public void PlantSetLaunchCounter(Plant thePlant)
        {
            int theTimeAge = PlantGetMinutesSinceHappy(thePlant);
            int theMax = TodCommon.TodAnimateCurve(5, 30, theTimeAge, 3000, 15000, TodCurves.Linear);
            thePlant.mLaunchCounter = TodCommon.RandRangeInt(1800, theMax);
        }

        public int PlantGetMinutesSinceHappy(Plant thePlant)
        {
            PottedPlant pottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            int result = (int)((aNow - pottedPlant.mLastNeedFulfilledTime).TotalSeconds / 60.0);
            if (PlantHighOnChocolate(pottedPlant))
            {
                result = 0;
            }
            return result;
        }

        public bool IsStinkyHighOnChocolate()
        {
            return (aNow - mApp.mPlayerInfo.mLastStinkyChocolateTime).TotalSeconds < 3600.0;
        }

        public void StinkyAnimRateUpdate(GridItem theStinky)
        {
            Reanimation reanimation = mApp.ReanimationGet(theStinky.mGridItemReanimID);
            if (IsStinkyHighOnChocolate())
            {
                if (theStinky.mGridItemState == GridItemState.StinkyWalkingLeft || theStinky.mGridItemState == GridItemState.StinkyWalkingRight)
                {
                    reanimation.mAnimRate = 12f;
                    return;
                }
                if (theStinky.mGridItemState == GridItemState.StinkyTurningRight || theStinky.mGridItemState == GridItemState.StinkyTurningLeft)
                {
                    reanimation.mAnimRate = 12f;
                    return;
                }
            }
            else
            {
                if (theStinky.mGridItemState == GridItemState.StinkyWalkingLeft || theStinky.mGridItemState == GridItemState.StinkyWalkingRight)
                {
                    reanimation.mAnimRate = 6f;
                    return;
                }
                if (theStinky.mGridItemState == GridItemState.StinkyTurningRight || theStinky.mGridItemState == GridItemState.StinkyTurningLeft)
                {
                    reanimation.mAnimRate = 6f;
                }
            }
        }

        public bool PlantCanBeWatered(Plant thePlant)
        {
            if (thePlant.mPottedPlantIndex == -1)
            {
                return false;
            }
            PottedPlant thePottedPlant = PottedPlantFromIndex(thePlant.mPottedPlantIndex);
            PottedPlantNeed plantsNeed = mApp.mZenGarden.GetPlantsNeed(thePottedPlant);
            return plantsNeed == PottedPlantNeed.Water;
        }

        public void MakeStinkySleeping()
        {
            if (!HasPurchasedStinky())
            {
                return;
            }
            if (!mApp.mPlayerInfo.mHasSeenStinky)
            {
                return;
            }
            ResetStinkyTimers();
        }

        public float ZenPlantOffsetX(PottedPlant thePottedPlant)
        {
            int num = 0;
            if (thePottedPlant.mFacing == PottedPlant.FacingDirection.Left && thePottedPlant.mSeedType == SeedType.Potatomine)
            {
                num -= 6;
            }
            return num;
        }

        public void DoPlantSale(bool wasSold)
        {
            mApp.CrazyDaveLeave();
            if (wasSold)
            {
                int plantSellPrice = GetPlantSellPrice(mPlantForSale);
                PottedPlantFromIndex(mPlantForSale.mPottedPlantIndex);
                mApp.mPlayerInfo.AddCoins(plantSellPrice);
                mBoard.mCoinsCollected += plantSellPrice;
                int num = mApp.mPlayerInfo.mNumPottedPlants - mPlantForSale.mPottedPlantIndex - 1;
                if (num > 0)
                {
                    for (int i = mPlantForSale.mPottedPlantIndex; i < mApp.mPlayerInfo.mPottedPlant.Length; i++)
                    {
                        if (i != mApp.mPlayerInfo.mPottedPlant.Length - 1)
                        {
                            mApp.mPlayerInfo.mPottedPlant[i] = mApp.mPlayerInfo.mPottedPlant[i + 1];
                        }
                    }
                    int count = mBoard.mPlants.Count;
                    for (int j = 0; j < count; j++)
                    {
                        Plant plant = mBoard.mPlants[j];
                        if (!plant.mDead && plant.mPottedPlantIndex > mPlantForSale.mPottedPlantIndex)
                        {
                            plant.mPottedPlantIndex--;
                        }
                    }
                }
                mApp.mPlayerInfo.mNumPottedPlants--;
                mApp.PlayFoley(FoleyType.UseShovel);
                RemovePottedPlant(mPlantForSale);
            }
        }

        public void BackFromStore()
        {
            StoreScreen storeScreen = (StoreScreen)mApp.GetDialog(4);
            bool goToTreeNow = storeScreen.mGoToTreeNow;
            mApp.KillDialog(Dialogs.DIALOG_STORE);
            if (goToTreeNow)
            {
                mApp.KillBoard();
                mApp.PreNewGame(GameMode.TreeOfWisdom, false);
                return;
            }
            mApp.mMusic.MakeSureMusicIsPlaying(MusicTune.ZenGarden);
            if (mBoard.mTutorialState == TutorialState.ZenGardenVisitStore)
            {
                mBoard.DisplayAdvice("[ADVICE_ZEN_GARDEN_FERTILIZE]", MessageStyle.ZenGardenLong, AdviceType.None);
                mBoard.mTutorialState = TutorialState.ZenGardenFertilizePlants;
                mApp.mPlayerInfo.mZenTutorialMessage = 26;
            }
            AddStinky();
        }

        private static ZenGarden.PottedPlantOffset[] POTTED_PLANT_DRAW_OFFSETS = new ZenGarden.PottedPlantOffset[]
        {
            new ZenGarden.PottedPlantOffset(30, 0, -35, -7, -16, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -40, -9, -14, 0),
            new ZenGarden.PottedPlantOffset(36, 0, -30, -25, -17, 0),
            new ZenGarden.PottedPlantOffset(34, -2, -35, -27, -17, 0),
            new ZenGarden.PottedPlantOffset(25, -5, -35, -13, -17, 0),
            new ZenGarden.PottedPlantOffset(30, -3, -40, -7, -17, -1),
            new ZenGarden.PottedPlantOffset(28, 4, -50, -16, -19, 3),
            new ZenGarden.PottedPlantOffset(30, -2, -37, -7, -17, 0),
            new ZenGarden.PottedPlantOffset(32, -4, -37, -25, -17, 0),
            new ZenGarden.PottedPlantOffset(35, 0, -37, -25, -17, 0),
            new ZenGarden.PottedPlantOffset(28, 0, -37, -20, -17, 1),
            new ZenGarden.PottedPlantOffset(70, 0, -37, 0, -14, 0),
            new ZenGarden.PottedPlantOffset(28, 0, -37, -25, -18, 0),
            new ZenGarden.PottedPlantOffset(34, 8, -43, -21, -20, -3),
            new ZenGarden.PottedPlantOffset(28, 0, -40, -21, -14, 0),
            new ZenGarden.PottedPlantOffset(28, 0, -40, -21, -18, 0),
            new ZenGarden.PottedPlantOffset(12, 0, -40, -36, -17, 0),
            new ZenGarden.PottedPlantOffset(28, 0, -40, -19, -17, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -40, -8, -17, 0),
            new ZenGarden.PottedPlantOffset(10, 0, -40, -28, -17, 0),
            new ZenGarden.PottedPlantOffset(28, 0, -40, -22, -17, 0),
            new ZenGarden.PottedPlantOffset(22, 0, -40, -30, -17, 0),
            new ZenGarden.PottedPlantOffset(27, 0, -40, -19, -17, 0),
            new ZenGarden.PottedPlantOffset(28, 0, -40, -22, -17, 0),
            new ZenGarden.PottedPlantOffset(5, 0, -40, -23, -11, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -40, -28, -17, 0),
            new ZenGarden.PottedPlantOffset(33, 5, -45, -28, -17, -1),
            new ZenGarden.PottedPlantOffset(35, 0, -40, -32, -22, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -40, -20, -17, -5),
            new ZenGarden.PottedPlantOffset(30, 0, -40, -30, -14, 0),
            new ZenGarden.PottedPlantOffset(15, 0, -40, -18, -14, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -40, -26, -14, -3),
            new ZenGarden.PottedPlantOffset(33, 0, -40, -27, -18, -8),
            new ZenGarden.PottedPlantOffset(0, 0, 0, 0, -14, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -37, -23, -14, -5),
            new ZenGarden.PottedPlantOffset(30, 10, -30, -10, -14, 0),
            new ZenGarden.PottedPlantOffset(32, 0, -40, -25, -14, 0),
            new ZenGarden.PottedPlantOffset(35, 0, -40, -30, -17, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -35, -8, -17, -2),
            new ZenGarden.PottedPlantOffset(33, 0, -40, -28, -17, -8),
            new ZenGarden.PottedPlantOffset(30, 0, -40, -22, -14, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -38, -22, -14, 0),
            new ZenGarden.PottedPlantOffset(35, 0, -40, -26, -14, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -40, -26, -14, 0),
            new ZenGarden.PottedPlantOffset(34, 0, -40, -26, -14, -5),
            new ZenGarden.PottedPlantOffset(30, 0, -40, -25, -14, -2),
            new ZenGarden.PottedPlantOffset(25, 0, -40, -35, -14, 0),
            new ZenGarden.PottedPlantOffset(0, 0, -40, 0, -14, 0),
            new ZenGarden.PottedPlantOffset(33, 0, -40, 0, -14, 0),
            new ZenGarden.PottedPlantOffset(20, 0, 0, 0, -14, 0),
            new ZenGarden.PottedPlantOffset(0, 0, 0, 0, -14, 0),
            new ZenGarden.PottedPlantOffset(30, 0, -38, -35, -14, 0),
            new ZenGarden.PottedPlantOffset(30, 0, 37, 0, -14, -5)
        };

        public static SpecialGridPlacement[] gGreenhouseGridPlacement = new SpecialGridPlacement[]
        {
            new SpecialGridPlacement(73, 73, 0, 0),
            new SpecialGridPlacement(155, 71, 1, 0),
            new SpecialGridPlacement(239, 68, 2, 0),
            new SpecialGridPlacement(321, 73, 3, 0),
            new SpecialGridPlacement(406, 71, 4, 0),
            new SpecialGridPlacement(484, 67, 5, 0),
            new SpecialGridPlacement(566, 70, 6, 0),
            new SpecialGridPlacement(648, 72, 7, 0),
            new SpecialGridPlacement(67, 168, 0, 1),
            new SpecialGridPlacement(150, 165, 1, 1),
            new SpecialGridPlacement(232, 170, 2, 1),
            new SpecialGridPlacement(314, 175, 3, 1),
            new SpecialGridPlacement(416, 173, 4, 1),
            new SpecialGridPlacement(497, 170, 5, 1),
            new SpecialGridPlacement(578, 164, 6, 1),
            new SpecialGridPlacement(660, 168, 7, 1),
            new SpecialGridPlacement(41, 268, 0, 2),
            new SpecialGridPlacement(130, 266, 1, 2),
            new SpecialGridPlacement(219, 260, 2, 2),
            new SpecialGridPlacement(310, 266, 3, 2),
            new SpecialGridPlacement(416, 267, 4, 2),
            new SpecialGridPlacement(504, 261, 5, 2),
            new SpecialGridPlacement(594, 265, 6, 2),
            new SpecialGridPlacement(684, 269, 7, 2),
            new SpecialGridPlacement(37, 371, 0, 3),
            new SpecialGridPlacement(124, 369, 1, 3),
            new SpecialGridPlacement(211, 368, 2, 3),
            new SpecialGridPlacement(302, 369, 3, 3),
            new SpecialGridPlacement(425, 375, 4, 3),
            new SpecialGridPlacement(512, 368, 5, 3),
            new SpecialGridPlacement(602, 365, 6, 3),
            new SpecialGridPlacement(691, 368, 7, 3)
        };

        public static SpecialGridPlacement[] gAquariumGridPlacement = new SpecialGridPlacement[]
        {
            new SpecialGridPlacement(113, 185, 0, 0),
            new SpecialGridPlacement(306, 120, 1, 0),
            new SpecialGridPlacement(356, 270, 2, 0),
            new SpecialGridPlacement(622, 120, 3, 0),
            new SpecialGridPlacement(669, 270, 4, 0),
            new SpecialGridPlacement(122, 355, 5, 0),
            new SpecialGridPlacement(365, 458, 6, 0),
            new SpecialGridPlacement(504, 417, 7, 0)
        };

        public LawnApp mApp;

        public Board mBoard;

        public GardenType mGardenType;

        public bool mIsTutorial;

        public readonly ulong STINKY_BASE_TIME = (ulong)TimeSpan.FromTicks(new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks).TotalSeconds;

        private DateTime aNow;

        public Plant mPlantForSale;

        private struct PottedPlantOffset
        {
            public PottedPlantOffset(int y, int xRight, int xLeft, int yWheelBarrowScale, int yCachedOffset, int xCachedOffset)
            {
                this.y = y;
                this.xRight = xRight;
                this.xLeft = xLeft;
                this.yWheelBarrowScale = yWheelBarrowScale;
                this.yCachedOffset = yCachedOffset;
                this.xCachedOffset = xCachedOffset;
            }

            public int y;

            public int xRight;

            public int xLeft;

            public int yWheelBarrowScale;

            public int yCachedOffset;

            public int xCachedOffset;
        }
    }
}
