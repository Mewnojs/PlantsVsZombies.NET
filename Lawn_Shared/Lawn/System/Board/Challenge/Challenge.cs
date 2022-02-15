using System;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class Challenge : StoreListener
    {
        static Challenge()
        {
            SeedType[,] array = new SeedType[6, 9];
            array[0, 0] = SeedType.None;
            array[0, 1] = SeedType.None;
            array[0, 2] = SeedType.None;
            array[0, 3] = SeedType.None;
            array[0, 4] = SeedType.Wallnut;
            array[0, 5] = SeedType.Wallnut;
            array[0, 6] = SeedType.Wallnut;
            array[0, 7] = SeedType.None;
            array[0, 8] = SeedType.None;
            array[1, 0] = SeedType.None;
            array[1, 1] = SeedType.None;
            array[1, 2] = SeedType.None;
            array[1, 3] = SeedType.Wallnut;
            array[1, 4] = SeedType.None;
            array[1, 5] = SeedType.None;
            array[1, 6] = SeedType.None;
            array[1, 7] = SeedType.Wallnut;
            array[1, 8] = SeedType.None;
            array[2, 0] = SeedType.None;
            array[2, 1] = SeedType.None;
            array[2, 2] = SeedType.None;
            array[2, 3] = SeedType.Wallnut;
            array[2, 4] = SeedType.None;
            array[2, 5] = SeedType.None;
            array[2, 6] = SeedType.None;
            array[2, 7] = SeedType.Wallnut;
            array[2, 8] = SeedType.None;
            array[3, 0] = SeedType.None;
            array[3, 1] = SeedType.None;
            array[3, 2] = SeedType.None;
            array[3, 3] = SeedType.Wallnut;
            array[3, 4] = SeedType.None;
            array[3, 5] = SeedType.None;
            array[3, 6] = SeedType.None;
            array[3, 7] = SeedType.Wallnut;
            array[3, 8] = SeedType.None;
            array[4, 0] = SeedType.None;
            array[4, 1] = SeedType.None;
            array[4, 2] = SeedType.None;
            array[4, 3] = SeedType.None;
            array[4, 4] = SeedType.Wallnut;
            array[4, 5] = SeedType.Wallnut;
            array[4, 6] = SeedType.Wallnut;
            array[4, 7] = SeedType.None;
            array[4, 8] = SeedType.None;
            array[5, 0] = SeedType.None;
            array[5, 1] = SeedType.None;
            array[5, 2] = SeedType.None;
            array[5, 3] = SeedType.None;
            array[5, 4] = SeedType.None;
            array[5, 5] = SeedType.None;
            array[5, 6] = SeedType.None;
            array[5, 7] = SeedType.None;
            array[5, 8] = SeedType.None;
            Challenge.gArtChallengeWallnut = array;
            SeedType[,] array2 = new SeedType[6, 9];
            array2[0, 0] = SeedType.None;
            array2[0, 1] = SeedType.None;
            array2[0, 2] = SeedType.Starfruit;
            array2[0, 3] = SeedType.Starfruit;
            array2[0, 4] = SeedType.Starfruit;
            array2[0, 5] = SeedType.None;
            array2[0, 6] = SeedType.None;
            array2[0, 7] = SeedType.None;
            array2[0, 8] = SeedType.None;
            array2[1, 0] = SeedType.None;
            array2[1, 1] = SeedType.Starfruit;
            array2[1, 2] = SeedType.Wallnut;
            array2[1, 3] = SeedType.Wallnut;
            array2[1, 4] = SeedType.Wallnut;
            array2[1, 5] = SeedType.Starfruit;
            array2[1, 6] = SeedType.None;
            array2[1, 7] = SeedType.None;
            array2[1, 8] = SeedType.None;
            array2[2, 0] = SeedType.None;
            array2[2, 1] = SeedType.None;
            array2[2, 2] = SeedType.Starfruit;
            array2[2, 3] = SeedType.Starfruit;
            array2[2, 4] = SeedType.Starfruit;
            array2[2, 5] = SeedType.None;
            array2[2, 6] = SeedType.None;
            array2[2, 7] = SeedType.None;
            array2[2, 8] = SeedType.None;
            array2[3, 0] = SeedType.None;
            array2[3, 1] = SeedType.None;
            array2[3, 2] = SeedType.None;
            array2[3, 3] = SeedType.Umbrella;
            array2[3, 4] = SeedType.None;
            array2[3, 5] = SeedType.None;
            array2[3, 6] = SeedType.None;
            array2[3, 7] = SeedType.None;
            array2[3, 8] = SeedType.None;
            array2[4, 0] = SeedType.None;
            array2[4, 1] = SeedType.None;
            array2[4, 2] = SeedType.Umbrella;
            array2[4, 3] = SeedType.Umbrella;
            array2[4, 4] = SeedType.Umbrella;
            array2[4, 5] = SeedType.None;
            array2[4, 6] = SeedType.None;
            array2[4, 7] = SeedType.None;
            array2[4, 8] = SeedType.None;
            array2[5, 0] = SeedType.None;
            array2[5, 1] = SeedType.None;
            array2[5, 2] = SeedType.None;
            array2[5, 3] = SeedType.None;
            array2[5, 4] = SeedType.None;
            array2[5, 5] = SeedType.None;
            array2[5, 6] = SeedType.None;
            array2[5, 7] = SeedType.None;
            array2[5, 8] = SeedType.None;
            Challenge.gArtChallengeSunFlower = array2;
            SeedType[,] array3 = new SeedType[6, 9];
            array3[0, 0] = SeedType.None;
            array3[0, 1] = SeedType.None;
            array3[0, 2] = SeedType.None;
            array3[0, 3] = SeedType.Starfruit;
            array3[0, 4] = SeedType.None;
            array3[0, 5] = SeedType.None;
            array3[0, 6] = SeedType.None;
            array3[0, 7] = SeedType.None;
            array3[0, 8] = SeedType.None;
            array3[1, 0] = SeedType.None;
            array3[1, 1] = SeedType.None;
            array3[1, 2] = SeedType.None;
            array3[1, 3] = SeedType.Starfruit;
            array3[1, 4] = SeedType.Starfruit;
            array3[1, 5] = SeedType.None;
            array3[1, 6] = SeedType.None;
            array3[1, 7] = SeedType.None;
            array3[1, 8] = SeedType.None;
            array3[2, 0] = SeedType.None;
            array3[2, 1] = SeedType.Starfruit;
            array3[2, 2] = SeedType.Starfruit;
            array3[2, 3] = SeedType.Starfruit;
            array3[2, 4] = SeedType.Starfruit;
            array3[2, 5] = SeedType.Starfruit;
            array3[2, 6] = SeedType.Starfruit;
            array3[2, 7] = SeedType.None;
            array3[2, 8] = SeedType.None;
            array3[3, 0] = SeedType.None;
            array3[3, 1] = SeedType.None;
            array3[3, 2] = SeedType.None;
            array3[3, 3] = SeedType.Starfruit;
            array3[3, 4] = SeedType.Starfruit;
            array3[3, 5] = SeedType.Starfruit;
            array3[3, 6] = SeedType.None;
            array3[3, 7] = SeedType.None;
            array3[3, 8] = SeedType.None;
            array3[4, 0] = SeedType.None;
            array3[4, 1] = SeedType.None;
            array3[4, 2] = SeedType.None;
            array3[4, 3] = SeedType.Starfruit;
            array3[4, 4] = SeedType.None;
            array3[4, 5] = SeedType.None;
            array3[4, 6] = SeedType.Starfruit;
            array3[4, 7] = SeedType.None;
            array3[4, 8] = SeedType.None;
            array3[5, 0] = SeedType.None;
            array3[5, 1] = SeedType.None;
            array3[5, 2] = SeedType.None;
            array3[5, 3] = SeedType.None;
            array3[5, 4] = SeedType.None;
            array3[5, 5] = SeedType.None;
            array3[5, 6] = SeedType.None;
            array3[5, 7] = SeedType.None;
            array3[5, 8] = SeedType.None;
            Challenge.gArtChallengeStarfruit = array3;
            Challenge.MAX_PICKS = (Constants.GRIDSIZEX + 1) * Constants.MAX_GRIDSIZEY;
            Challenge.aGridPickItemArray = new GridItem[Challenge.MAX_PICKS];
            Challenge.aGridPicks = new TodWeightedArray[Challenge.MAX_PICKS];
            Challenge.aSeedPickArray = new TodWeightedArray[20];
            Challenge.aPotArray = new TodWeightedArray[54];
            Challenge.aGridArray = new TodWeightedGridArray[54];
            Challenge.aPicks = new TodWeightedGridArray[Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY];
            for (int i = 0; i < Challenge.aGridPickItemArray.Length; i++)
            {
                Challenge.aGridPickItemArray[i] = GridItem.GetNewGridItem();
            }
            for (int j = 0; j < Challenge.aGridPicks.Length; j++)
            {
                Challenge.aGridPicks[j] = TodWeightedArray.GetNewTodWeightedArray();
            }
            for (int k = 0; k < Challenge.aSeedPickArray.Length; k++)
            {
                Challenge.aSeedPickArray[k] = TodWeightedArray.GetNewTodWeightedArray();
            }
            for (int l = 0; l < Challenge.aPotArray.Length; l++)
            {
                Challenge.aPotArray[l] = TodWeightedArray.GetNewTodWeightedArray();
            }
            for (int m = 0; m < Challenge.aGridArray.Length; m++)
            {
                Challenge.aGridArray[m] = TodWeightedGridArray.GetNewTodWeightedGridArray();
            }
            for (int n = 0; n < Challenge.aPicks.Length; n++)
            {
                Challenge.aPicks[n] = TodWeightedGridArray.GetNewTodWeightedGridArray();
            }
        }

        public Challenge()
        {
            Init();
        }

        private void Init()
        {
            mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
            mBoard = mApp.mBoard;
            mBeghouledMouseCapture = false;
            mBeghouledMouseDownX = 0;
            mBeghouledMouseDownY = 0;
            mChallengeStateCounter = 0;
            mConveyorBeltCounter = 0;
            mChallengeScore = 0;
            mChallengeState = ChallengeState.Normal;
            mShowBowlingLine = false;
            mLastConveyorSeedType = SeedType.None;
            mSurvivalStage = 0;
            mSlotMachineRollCount = 0;
            mReanimChallenge = null;
            mChallengeGridX = 0;
            mChallengeGridY = 0;
            mScaryPotterPots = 0;
            mBeghouledMatchesThisMove = 0;
            mRainCounter = 0;
            mTreeOfWisdomTalkIndex = 0;
            for (int i = 0; i < 6; i++)
            {
                mReanimCloud[i] = null;
            }
            for (int j = 0; j < Constants.GRIDSIZEX; j++)
            {
                for (int k = 0; k < Constants.MAX_GRIDSIZEY; k++)
                {
                    mBeghouledEated[j, k] = false;
                }
            }
            for (int l = 0; l < 3; l++)
            {
                mBeghouledPurcasedUpgrade[l] = false;
            }
            if (mApp.IsSlotMachineLevel())
            {
                TRect trect = SlotMachineGetHandleRect();
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.SlotMachineHandle, true);
                Reanimation reanimation = mApp.AddReanimation(trect.mX - 243f, trect.mY + 50f, 0, ReanimationType.SlotMachineHandle);
                reanimation.mIsAttachment = true;
                reanimation.mAnimRate = 0f;
                mReanimChallenge = mApp.ReanimationGetID(reanimation);
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                mName = TodStringFile.TodStringTranslate("[ZEN_GARDEN]");
                return;
            }
            if (mApp.mGameMode == GameMode.TreeOfWisdom)
            {
                mName = TodStringFile.TodStringTranslate("[TREE_OF_WISDOM]");
                return;
            }
            if (mApp.mGameMode != GameMode.Intro)
            {
                if (mApp.mGameMode == GameMode.Adventure)
                {
                    return;
                }
                mName = ChallengeScreen.gChallengeDefs[mApp.mGameMode - GameMode.SurvivalNormalStage1].mChallengeName;
            }
        }

        public bool SaveToFile(Sexy.Buffer b)
        {
            b.WriteBoolean2DArray(mBeghouledEated);
            b.WriteLong(mBeghouledMatchesThisMove);
            b.WriteBoolean(mBeghouledMouseCapture);
            b.WriteLong(mBeghouledMouseDownX);
            b.WriteLong(mBeghouledMouseDownY);
            b.WriteBooleanArray(mBeghouledPurcasedUpgrade);
            b.WriteLong(mChallengeGridX);
            b.WriteLong(mChallengeGridY);
            b.WriteLong(mChallengeScore);
            b.WriteLong((int)mChallengeState);
            b.WriteLong(mChallengeStateCounter);
            b.WriteLongArray(mCloudCounter);
            b.WriteLong(mConveyorBeltCounter);
            b.WriteLong((int)mLastConveyorSeedType);
            b.WriteLong(mRainCounter);
            b.WriteLong(mScaryPotterPots);
            b.WriteBoolean(mShowBowlingLine);
            b.WriteLong(mSlotMachineRollCount);
            b.WriteLong(mSurvivalStage);
            b.WriteLong(mTreeOfWisdomTalkIndex);
            return true;
        }

        public bool LoadFromFile(Sexy.Buffer b)
        {
            mApp = GlobalStaticVars.gLawnApp;
            mBoard = mApp.mBoard;
            Init();
            mBeghouledEated = b.ReadBoolean2DArray();
            mBeghouledMatchesThisMove = b.ReadLong();
            mBeghouledMouseCapture = b.ReadBoolean();
            mBeghouledMouseDownX = b.ReadLong();
            mBeghouledMouseDownY = b.ReadLong();
            mBeghouledPurcasedUpgrade = b.ReadBooleanArray();
            mChallengeGridX = b.ReadLong();
            mChallengeGridY = b.ReadLong();
            mChallengeScore = b.ReadLong();
            mChallengeState = (ChallengeState)b.ReadLong();
            mChallengeStateCounter = b.ReadLong();
            mCloudCounter = b.ReadLongArray();
            mConveyorBeltCounter = b.ReadLong();
            mLastConveyorSeedType = (SeedType)b.ReadLong();
            mRainCounter = b.ReadLong();
            mScaryPotterPots = b.ReadLong();
            mShowBowlingLine = b.ReadBoolean();
            mSlotMachineRollCount = b.ReadLong();
            mSurvivalStage = b.ReadLong();
            mTreeOfWisdomTalkIndex = b.ReadLong();
            return true;
        }

        public void StartLevel()
        {
            if (mApp.IsWhackAZombieLevel())
            {
                mBoard.mCursorObject.mCursorType = CursorType.Hammer;
                mBoard.mZombieCountDown = 200;
                mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
            }
            if (mApp.IsStormyNightLevel())
            {
                mChallengeState = ChallengeState.StormFlash1;
                mChallengeStateCounter = 400;
            }
            if (mApp.mGameMode == GameMode.ChallengeBobsledBonanza)
            {
                for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
                {
                    if (mBoard.mPlantRow[i] != PlantRowType.Pool)
                    {
                        mBoard.mIceMinX[i] = 400;
                        mBoard.mIceTimer[i] = int.MaxValue;
                    }
                }
            }
            if (mApp.IsWallnutBowlingLevel())
            {
                mBoard.mZombieCountDown = 200;
                mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
                mBoard.mSeedBank.AddSeed(SeedType.Wallnut);
                mConveyorBeltCounter = 400;
            }
            if (mApp.IsWallnutBowlingLevel())
            {
                mShowBowlingLine = true;
            }
            if (mApp.mGameMode == GameMode.ChallengeShovel || mApp.mGameMode == GameMode.ChallengeSquirrel)
            {
                ShovelAddWallnuts();
            }
            if (mApp.IsScaryPotterLevel())
            {
                ScaryPotterStart();
            }
            if (mApp.IsLittleTroubleLevel() || mApp.IsStormyNightLevel() || mApp.IsBungeeBlitzLevel() || mApp.mGameMode == GameMode.ChallengeInvisighoul)
            {
                mBoard.mZombieCountDown = 200;
                mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
                mConveyorBeltCounter = 200;
            }
            if (mApp.IsSurvivalMode() && mSurvivalStage == 0)
            {
                string theAdvice = string.Empty;
                if (mApp.IsSurvivalNormal(mApp.mGameMode))
                {
                    theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_SURVIVE_FLAGS]", "{FLAGS}", 5);
                }
                else if (mApp.IsSurvivalHard(mApp.mGameMode))
                {
                    theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_SURVIVE_FLAGS]", "{FLAGS}", 10);
                }
                else
                {
                    theAdvice = "[ADVICE_SURVIVE_ENDLESS]";
                }
                mBoard.DisplayAdvice(theAdvice, MessageStyle.HintFast, AdviceType.SurviveFlags);
            }
            if (mApp.mGameMode == GameMode.ChallengeLastStand && mSurvivalStage == 0)
            {
                string theAdvice2 = TodCommon.TodReplaceNumberString("[ADVICE_SURVIVE_FLAGS]", "{FLAGS}", 5);
                mBoard.DisplayAdvice(theAdvice2, MessageStyle.BigMiddleFast, AdviceType.SurviveFlags);
            }
            if (mApp.mGameMode == GameMode.ChallengeArtChallenge1)
            {
                mBoard.DisplayAdvice("[ADVICE_FILL_IN_WALLNUTS]", MessageStyle.HintFast, AdviceType.None);
            }
            if (mApp.mGameMode == GameMode.ChallengeArtChallenge2)
            {
                mBoard.DisplayAdvice("[ADVICE_FILL_IN_SPACES]", MessageStyle.HintFast, AdviceType.None);
            }
            if (mApp.mGameMode == GameMode.ChallengeSeeingStars)
            {
                mBoard.DisplayAdvice("[ADVICE_FILL_IN_STARFRUIT]", MessageStyle.HintFast, AdviceType.None);
            }
            if (mApp.IsSlotMachineLevel())
            {
                mBoard.SetTutorialState(TutorialState.SlotMachinePull);
            }
            if (mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                mBoard.mZombieCountDown = 200;
                mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
                mChallengeStateCounter = 1500;
                BeghouledMakeStartBoard();
                BeghouledUpdateCraters();
                if (mApp.mGameMode == GameMode.ChallengeBeghouled)
                {
                    mBoard.DisplayAdvice("[ADVICE_BEGHOULED_DRAG_TO_MATCH_3]", MessageStyle.HintFast, AdviceType.None);
                }
                else if (mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
                {
                    mBoard.DisplayAdvice("[ADVICE_BEGHOULED_TWIST_TO_MATCH_3]", MessageStyle.HintFast, AdviceType.None);
                }
            }
            if (mApp.IsFirstTimeAdventureMode())
            {
                mApp.IsSquirrelLevel();
            }
            if (mApp.IsMiniBossLevel())
            {
                mBoard.mZombieCountDown = 100;
                mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
                mConveyorBeltCounter = 200;
            }
            if (!mApp.IsFinalBossLevel() && !mApp.IsWhackAZombieLevel())
            {
                GameMode aGameMode = mApp.mGameMode;
            }
            if (mApp.mGameMode == GameMode.ChallengePortalCombat)
            {
                PortalStart();
            }
            if (mApp.mGameMode == GameMode.ChallengeColumn)
            {
                mBoard.mCurrentWave = 9;
                mBoard.mZombieCountDown = 2400;
            }
            if (mApp.mGameMode == GameMode.ChallengeAirRaid || mApp.mGameMode == GameMode.ChallengeBobsledBonanza)
            {
                mBoard.mZombieCountDown = 4500;
            }
            if (mApp.mGameMode == GameMode.ChallengePogoParty)
            {
                mBoard.mZombieCountDown = 5500;
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                mApp.mZenGarden.ZenGardenStart();
            }
            if (mApp.IsIZombieLevel())
            {
                IZombieStart();
            }
            if (mApp.IsSquirrelLevel())
            {
                SquirrelStart();
            }
        }

        public void BeghouledPopulateBoard()
        {
            BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
            LoadBeghouledBoardState(newBeghouledBoardState);
            bool theAllowMatches = false;
            if (BeghouledBoardHasMatch(newBeghouledBoardState))
            {
                theAllowMatches = true;
            }
            BeghouledBoardState newBeghouledBoardState2 = BeghouledBoardState.GetNewBeghouledBoardState();
            for (int i = 0; i < 2; i++)
            {
                LoadBeghouledBoardState(newBeghouledBoardState2);
                BeghouledFillHoles(newBeghouledBoardState2, theAllowMatches);
                if (BeghouledCheckForPossibleMoves(newBeghouledBoardState2))
                {
                    break;
                }
            }
            BeghouledCreatePlants(newBeghouledBoardState, newBeghouledBoardState2);
            newBeghouledBoardState2.PrepareForReuse();
            newBeghouledBoardState.PrepareForReuse();
        }

        public void LoadBeghouledBoardState(BeghouledBoardState theBoardState)
        {
            for (int i = 0; i < Constants.GRIDSIZEX; i++)
            {
                for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
                {
                    theBoardState.mSeedType[i, j] = SeedType.None;
                }
            }
            int count = mBoard.mPlants.Count;
            for (int k = 0; k < count; k++)
            {
                Plant plant = mBoard.mPlants[k];
                if (!plant.mDead)
                {
                    theBoardState.mSeedType[plant.mPlantCol, plant.mRow] = plant.mSeedType;
                }
            }
        }

        public SeedType BeghouledPickSeed(int theGridX, int theGridY, BeghouledBoardState theBoardState, bool theAllowMatches)
        {
            Debug.ASSERT(theBoardState.mSeedType[theGridX, theGridY] == SeedType.None);
            int[] array = new int[6];
            int num = 0;
            for (int i = 0; i < 6; i++)
            {
                SeedType seedType = SeedType.Peashooter;
                switch (i)
                {
                case 0:
                    seedType = SeedType.Puffshroom;
                    break;
                case 1:
                    seedType = SeedType.Starfruit;
                    break;
                case 2:
                    seedType = SeedType.Magnetshroom;
                    break;
                case 3:
                    seedType = SeedType.Snowpea;
                    break;
                case 4:
                    seedType = SeedType.Wallnut;
                    break;
                case 5:
                    seedType = SeedType.Peashooter;
                    break;
                default:
                    Debug.ASSERT(false);
                    break;
                }
                if (mBeghouledPurcasedUpgrade[0] && seedType == SeedType.Peashooter)
                {
                    seedType = SeedType.Repeater;
                }
                else if (mBeghouledPurcasedUpgrade[1] && seedType == SeedType.Puffshroom)
                {
                    seedType = SeedType.Fumeshroom;
                }
                else if (mBeghouledPurcasedUpgrade[2] && seedType == SeedType.Wallnut)
                {
                    seedType = SeedType.Tallnut;
                }
                theBoardState.mSeedType[theGridX, theGridY] = seedType;
                if (theAllowMatches || !BeghouledBoardHasMatch(theBoardState))
                {
                    array[num] = (int)seedType;
                    num++;
                }
            }
            theBoardState.mSeedType[theGridX, theGridY] = SeedType.None;
            return (SeedType)TodCommon.TodPickFromArray(array, num);
        }

        public bool BeghouledBoardHasMatch(BeghouledBoardState theBoardState)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (BeghouledHorizontalMatchLength(j, i, theBoardState) >= 3)
                    {
                        return true;
                    }
                    if (BeghouledVerticalMatchLength(j, i, theBoardState) >= 3)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public SeedType BeghouledGetPlantAt(int x, int y, BeghouledBoardState theBoardState)
        {
            if (x < 0 || x >= Constants.GRIDSIZEX || y < 0 || y >= Constants.MAX_GRIDSIZEY)
            {
                return SeedType.None;
            }
            return theBoardState.mSeedType[x, y];
        }

        public int BeghouledVerticalMatchLength(int x, int y, BeghouledBoardState theBoardState)
        {
            SeedType seedType = BeghouledGetPlantAt(x, y, theBoardState);
            if (seedType == SeedType.None)
            {
                return 0;
            }
            if (BeghouledGetPlantAt(x, y - 1, theBoardState) == seedType)
            {
                return 0;
            }
            int num = 1;
            while (BeghouledGetPlantAt(x, y + num, theBoardState) == seedType)
            {
                num++;
            }
            return num;
        }

        public int BeghouledHorizontalMatchLength(int x, int y, BeghouledBoardState theBoardState)
        {
            SeedType seedType = BeghouledGetPlantAt(x, y, theBoardState);
            if (seedType == SeedType.None)
            {
                return 0;
            }
            if (BeghouledGetPlantAt(x - 1, y, theBoardState) == seedType)
            {
                return 0;
            }
            int num = 1;
            while (BeghouledGetPlantAt(x + num, y, theBoardState) == seedType)
            {
                num++;
            }
            return num;
        }

        public void BeghouledDragStart(int x, int y)
        {
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            mBeghouledMouseCapture = true;
            mBeghouledMouseDownX = (int)(x * Constants.IS);
            mBeghouledMouseDownY = (int)(y * Constants.IS);
        }

        public void BeghouledDragUpdate(int x, int y)
        {
            x = (int)(x * Constants.IS);
            y = (int)(y * Constants.IS);
            int num = x - mBeghouledMouseDownX;
            int num2 = y - mBeghouledMouseDownY;
            if (Math.Abs(num) < 10 && Math.Abs(num2) < 10)
            {
                return;
            }
            mBoard.ClearAdvice(AdviceType.BeghouledDragToMatch3);
            mBeghouledMouseCapture = false;
            int num3 = mBoard.PixelToGridX(mBeghouledMouseDownX, mBeghouledMouseDownY);
            int num4 = mBoard.PixelToGridY(mBeghouledMouseDownX, mBeghouledMouseDownY);
            int num5;
            int num6;
            if (Math.Abs(num) > Math.Abs(num2))
            {
                if (num > 0)
                {
                    num5 = num3 + 1;
                    num6 = num4;
                }
                else
                {
                    num5 = num3 - 1;
                    num6 = num4;
                }
            }
            else if (num2 > 0)
            {
                num5 = num3;
                num6 = num4 + 1;
            }
            else
            {
                num5 = num3;
                num6 = num4 - 1;
            }
            BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
            LoadBeghouledBoardState(newBeghouledBoardState);
            Plant topPlantAt = mBoard.GetTopPlantAt(num3, num4, TopPlant.Any);
            if (!BeghouledIsValidMove(num3, num4, num5, num6, newBeghouledBoardState))
            {
                BeghouledDragCancel();
                if (topPlantAt != null)
                {
                    if (num5 > num3)
                    {
                        topPlantAt.mX += 30;
                    }
                    else if (num5 < num3)
                    {
                        topPlantAt.mX -= 30;
                    }
                    else if (num6 > num4)
                    {
                        topPlantAt.mY += 30;
                    }
                    else if (num6 < num4)
                    {
                        topPlantAt.mY -= 30;
                    }
                    mApp.PlayFoley(FoleyType.Floop);
                }
                newBeghouledBoardState.PrepareForReuse();
                return;
            }
            Plant topPlantAt2 = mBoard.GetTopPlantAt(num5, num6, TopPlant.Any);
            if (topPlantAt != null)
            {
                topPlantAt.mPlantCol = num5;
                topPlantAt.mRow = num6;
                topPlantAt.mRenderOrder = topPlantAt.CalcRenderOrder();
            }
            if (topPlantAt2 != null)
            {
                topPlantAt2.mPlantCol = num3;
                topPlantAt2.mRow = num4;
                topPlantAt2.mRenderOrder = topPlantAt2.CalcRenderOrder();
            }
            BeghouledStartFalling(ChallengeState.BeghouledMoving);
            newBeghouledBoardState.PrepareForReuse();
        }

        public void BeghouledDragCancel()
        {
            mBeghouledMouseCapture = false;
        }

        public bool MouseMove(int x, int y)
        {
            if (mApp.mGameMode == GameMode.ChallengeBeghouled && !mBoard.HasLevelAwardDropped())
            {
                if (mBeghouledMouseCapture)
                {
                    BeghouledDragUpdate(x, y);
                    return true;
                }
                HitResult hitResult;
                mBoard.MouseHitTest(x, y, out hitResult, false);
                if (hitResult.mObjectType == GameObjectType.Plant)
                {
                    return true;
                }
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                if (mChallengeState == ChallengeState.ZenFading)
                {
                    mChallengeState = ChallengeState.Normal;
                }
                mChallengeStateCounter = 3000;
            }
            return false;
        }

        public bool MouseDown(int x, int y, int theClickCount, HitResult theHitResult)
        {
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                return mApp.mZenGarden.MouseDownZenGarden(x, y, theClickCount, theHitResult);
            }
            if (mApp.mGameScene != GameScenes.Playing)
            {
                return false;
            }
            if (mBoard.IsScaryPotterDaveTalking() && mApp.mCrazyDaveMessageIndex != -1)
            {
                AdvanceCrazyDaveDialog();
                return true;
            }
            if (theHitResult.mObjectType == GameObjectType.Coin && theClickCount >= 0)
            {
                return false;
            }
            if (mApp.mGameMode == GameMode.ChallengeBeghouled)
            {
                if (mChallengeState != ChallengeState.Normal)
                {
                    return false;
                }
                if (theHitResult.mObjectType == GameObjectType.Plant)
                {
                    BeghouledDragStart(x, y);
                    return true;
                }
            }
            if (mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                if (mChallengeState != ChallengeState.Normal)
                {
                    return false;
                }
                BeghouledTwistMouseDown(x, y);
            }
            if (mApp.IsSlotMachineLevel() && theHitResult.mObjectType == GameObjectType.SlotMachineHandle && mBoard.mCursorObject.mCursorType == CursorType.Normal && mChallengeState == ChallengeState.Normal)
            {
                if (mBoard.TakeSunMoney(25))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        mBoard.mSeedBank.mSeedPackets[i].SlotMachineStart();
                    }
                    Reanimation reanimation = mApp.ReanimationGet(mReanimChallenge);
                    reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_pull, ReanimLoopType.PlayOnceAndHold, 0, 36f);
                    mChallengeState = ChallengeState.SlotMachineRolling;
                    mBoard.SetTutorialState(TutorialState.SlotMachineCompleted);
                    mBoard.ClearAdvice(AdviceType.None);
                    mSlotMachineRollCount++;
                    mApp.PlaySample(Resources.SOUND_SLOT_MACHINE);
                }
                return true;
            }
            if (mApp.IsWhackAZombieLevel() && theHitResult.mObjectType == GameObjectType.None && mBoard.mCursorObject.mCursorType == CursorType.Hammer && theClickCount >= 0)
            {
                MouseDownWhackAZombie(x, y);
                return true;
            }
            if (mApp.IsScaryPotterLevel() && theHitResult.mObjectType == GameObjectType.ScaryPot)
            {
                ScaryPotterMalletPot((GridItem)theHitResult.mObject);
                return true;
            }
            return false;
        }

        public bool MouseUp(int x, int y)
        {
            if (mApp.mGameMode == GameMode.ChallengeBeghouled)
            {
                if (mBeghouledMouseCapture && !mBoard.mAdvice.IsBeingDisplayed() && mChallengeScore == 0)
                {
                    mBoard.DisplayAdvice("[ADVICE_BEGHOULED_DRAG_TO_MATCH_3]", MessageStyle.HintFast, AdviceType.BeghouledDragToMatch3);
                }
                BeghouledDragCancel();
            }
            return false;
        }

        public void ClearCursor()
        {
            if (mApp.mGameMode == GameMode.ChallengeBeghouled)
            {
                BeghouledDragCancel();
            }
            if (mApp.IsWhackAZombieLevel() && !mBoard.HasLevelAwardDropped())
            {
                mBoard.mCursorObject.mCursorType = CursorType.Hammer;
            }
        }

        public void BeghouledRemoveHorizontalMatch(int x, int y, BeghouledBoardState theBoardState)
        {
            SeedType seedType = BeghouledGetPlantAt(x, y, theBoardState);
            do
            {
                Plant topPlantAt = mBoard.GetTopPlantAt(x, y, TopPlant.Any);
                if (topPlantAt != null)
                {
                    topPlantAt.Die();
                }
                x++;
            }
            while (BeghouledGetPlantAt(x, y, theBoardState) == seedType);
        }

        public void BeghouledRemoveVerticalMatch(int x, int y, BeghouledBoardState theBoardState)
        {
            SeedType seedType = BeghouledGetPlantAt(x, y, theBoardState);
            do
            {
                Plant topPlantAt = mBoard.GetTopPlantAt(x, y, TopPlant.Any);
                if (topPlantAt != null)
                {
                    topPlantAt.Die();
                }
                y++;
            }
            while (BeghouledGetPlantAt(x, y, theBoardState) == seedType);
        }

        public void BeghouledRemoveMatches(BeghouledBoardState theBoardState)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int num = BeghouledHorizontalMatchLength(j, i, theBoardState);
                    if (num >= 3)
                    {
                        BeghouledRemoveHorizontalMatch(j, i, theBoardState);
                        BeghouledScore(j, i, num, true);
                    }
                    int num2 = BeghouledVerticalMatchLength(j, i, theBoardState);
                    if (num2 >= 3)
                    {
                        BeghouledRemoveVerticalMatch(j, i, theBoardState);
                        BeghouledScore(j, i, num2, false);
                    }
                }
            }
        }

		public void Update()//1update
		{
			if (mApp.IsStormyNightLevel())
			{
				UpdateStormyNight();
			}
			if (mBoard.mPaused)
			{
				if (mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
				{
					mChallengeGridX = -1;
					mChallengeGridY = -1;
				}
				return;
			}
			if (mApp.mGameMode == GameMode.ChallengeRainingSeeds || mApp.IsStormyNightLevel())
			{
				UpdateRain();
			}
			if (mApp.mGameScene != GameScenes.Playing)
			{
				return;
			}
			if (mBoard.HasConveyorBeltSeedBank())
			{
				UpdateConveyorBelt();
			}
			if (mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
			{
				UpdateBeghouled();
			}
			if (mApp.IsScaryPotterLevel())
			{
				ScaryPotterUpdate();
			}
			if ((mApp.IsScaryPotterLevel() || mApp.IsWhackAZombieLevel()) && mBoard.mSeedBank.mX < 0)
			{
				int num = mBoard.mSunMoney + mBoard.CountSunBeingCollected();
				if (num > 0 || mBoard.mSeedBank.mX > -mBoard.mSeedBank.mWidth)
				{
					mBoard.mSeedBank.mX += 2;
					if (mBoard.mSeedBank.mX > 0)
					{
						mBoard.mSeedBank.mX = 0;
					}
				}
			}
			if (mApp.IsWhackAZombieLevel())
			{
				WhackAZombieUpdate();
			}
			if (mApp.IsIZombieLevel())
			{
				IZombieUpdate();
			}
			if (mApp.IsSlotMachineLevel())
			{
				UpdateSlotMachine();
			}
			if (mApp.mGameMode == GameMode.ChallengeSpeed && speedBoardCounter++ % 3 == 0)
			{
				mBoard.UpdateGame();
			}
			if (mApp.mGameMode == GameMode.ChallengeRainingSeeds)
			{
				UpdateRainingSeeds();
			}
			if (mApp.mGameMode == GameMode.ChallengePortalCombat)
			{
				UpdatePortalCombat();
			}
			if (mApp.IsSquirrelLevel())
			{
				SquirrelUpdate();
			}
			if (mApp.mGameMode == GameMode.ChallengeIce && mBoard.mMainCounter == 3000)
			{
				mApp.PlayFoley(FoleyType.Floop);
				mApp.PlaySample(Resources.SOUND_LOSEMUSIC);
			}
			if (mApp.mGameMode == GameMode.ChallengeLastStand)
			{
				LastStandUpate();
			}
			Reanimation reanimation = mApp.ReanimationTryToGet(mReanimChallenge);
			if (reanimation != null && reanimation.mIsAttachment)
			{
				reanimation.Update();
			}
		}

        public void UpdateBeghouled()
        {
            mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 75, mChallengeScore, 0, 150, TodCurves.Linear);
            bool flag = false;
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && UpdateBeghouledPlant(plant))
                {
                    flag = true;
                }
            }
            if (mBoard.mSeedBank.mNumPackets > 4 && !mBoard.mAdvice.IsBeingDisplayed() && !mBoard.mHelpDisplayed[22])
            {
                int currentPlantCost = mBoard.GetCurrentPlantCost(SeedType.BeghouledButtonCrater, SeedType.None);
                if (mBoard.CanTakeSunMoney(currentPlantCost) && BeghouledCanClearCrater() && !mBoard.HasLevelAwardDropped())
                {
                    mBoard.DisplayAdvice("[ADVICE_BEGHOULED_USE_CRATER_2]", MessageStyle.HintFast, AdviceType.BeghouledUseCrater2);
                }
            }
            if (mApp.mGameMode == GameMode.ChallengeBeghouledTwist && mChallengeState == ChallengeState.Normal)
            {
                if (BeghouledTwistSquareFromMouse(mApp.mWidgetManager.mLastMouseX, mApp.mWidgetManager.mLastMouseY, ref mChallengeGridX, ref mChallengeGridY))
                {
                    BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
                    LoadBeghouledBoardState(newBeghouledBoardState);
                    if (!BeghouledTwistValidMove(mChallengeGridX, mChallengeGridY, newBeghouledBoardState))
                    {
                        mChallengeGridX = -1;
                        mChallengeGridY = -1;
                    }
                    newBeghouledBoardState.PrepareForReuse();
                }
            }
            else
            {
                mChallengeGridX = -1;
                mChallengeGridY = -1;
            }
            if (!flag && (mChallengeState == ChallengeState.BeghouledFalling || mChallengeState == ChallengeState.BeghouledMoving))
            {
                mChallengeState = ChallengeState.Normal;
                mChallengeStateCounter = 1500;
                BeghouledBoardState newBeghouledBoardState2 = BeghouledBoardState.GetNewBeghouledBoardState();
                LoadBeghouledBoardState(newBeghouledBoardState2);
                BeghouledRemoveMatches(newBeghouledBoardState2);
                LoadBeghouledBoardState(newBeghouledBoardState2);
                BeghouledMakePlantsFall(newBeghouledBoardState2);
                BeghouledPopulateBoard();
                if (mChallengeState == ChallengeState.BeghouledFalling)
                {
                    newBeghouledBoardState2.PrepareForReuse();
                    return;
                }
                mChallengeStateCounter = 1500;
                mBeghouledMatchesThisMove = 0;
                BeghouledCheckStuckState();
                newBeghouledBoardState2.PrepareForReuse();
            }
            if (mChallengeStateCounter == 0)
            {
                return;
            }
            mChallengeStateCounter--;
            if (mChallengeStateCounter > 0)
            {
                return;
            }
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            if (mChallengeState == ChallengeState.Normal)
            {
                BeghouledFlashAMatch();
                mChallengeStateCounter = 1500;
                return;
            }
            if (mChallengeState == ChallengeState.BeghouledNoMatches)
            {
                mApp.AddTodParticle(400f, 300f, 400000, ParticleEffect.ScreenFlash);
                BeghouledShuffle();
            }
        }

        public bool UpdateBeghouledPlant(Plant thePlant)
        {
            bool flag = false;
            int num = mBoard.GridToPixelX(thePlant.mPlantCol, thePlant.mRow) - thePlant.mX;
            int num2 = mBoard.GridToPixelY(thePlant.mPlantCol, thePlant.mRow) - thePlant.mY;
            int num3;
            if (mChallengeState == ChallengeState.BeghouledMoving)
            {
                num3 = 3;
            }
            else
            {
                num3 = TodCommon.TodAnimateCurve(90, 30, mChallengeStateCounter, 1, 20, TodCurves.EaseIn);
            }
            int num4 = 0;
            int num5 = 0;
            if (num > 0)
            {
                num4 = Math.Min(num3, num);
                thePlant.mX += num4;
                flag = true;
            }
            else if (num < 0)
            {
                num4 = -Math.Min(num3, -num);
                thePlant.mX += num4;
                flag = true;
            }
            if (num2 > 0)
            {
                num5 = Math.Min(num3, num2);
                thePlant.mY += num5;
                flag = true;
            }
            else if (num2 < 0)
            {
                num5 = -Math.Min(num3, -num2);
                thePlant.mY += num5;
                flag = true;
            }
            if (flag && (thePlant.mState == PlantState.MagnetshroomCharging || thePlant.mState == PlantState.MagnetshroomSucking))
            {
                MagnetItem magnetItem = thePlant.mMagnetItems[0];
                magnetItem.mPosX += num4;
                magnetItem.mPosY += num5;
            }
            return flag;
        }

        public void BeghouledFallIntoSquare(int x, int y, BeghouledBoardState theBoardState)
        {
            if (mBeghouledEated[x, y])
            {
                return;
            }
            for (int i = y - 1; i >= 0; i--)
            {
                Plant topPlantAt = mBoard.GetTopPlantAt(x, i, TopPlant.Any);
                if (topPlantAt != null)
                {
                    topPlantAt.mRow = y;
                    topPlantAt.mRenderOrder = topPlantAt.CalcRenderOrder();
                    theBoardState.mSeedType[x, y] = topPlantAt.mSeedType;
                    theBoardState.mSeedType[x, i] = SeedType.None;
                    BeghouledStartFalling(ChallengeState.BeghouledFalling);
                    return;
                }
            }
        }

        public void BeghouledMakePlantsFall(BeghouledBoardState theBoardState)
        {
            for (int i = 4; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    SeedType seedType = BeghouledGetPlantAt(j, i, theBoardState);
                    if (seedType == SeedType.None)
                    {
                        BeghouledFallIntoSquare(j, i, theBoardState);
                    }
                }
            }
        }

        public void ZombieAtePlant(Zombie theZombie, Plant thePlant)
        {
            if (mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                mBeghouledEated[thePlant.mPlantCol, thePlant.mRow] = true;
                if (mBoard.mSeedBank.mNumPackets == 4)
                {
                    mBoard.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.BeghouledButtonCrater, SeedType.None);
                    mBoard.mSeedBank.mNumPackets = 5;
                    mBoard.DisplayAdvice("[ADVICE_BEGHOULED_USE_CRATER_1]", MessageStyle.HintFast, AdviceType.BeghouledUseCrater1);
                }
                BeghouledCheckStuckState();
                BeghouledUpdateCraters();
            }
        }

        public void DrawBackdrop(Graphics g)
        {
            if (mApp.IsArtChallenge())
            {
                DrawArtChallenge(g);
            }
            if (mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                DrawBeghouled(g);
            }
            if (mApp.IsWallnutBowlingLevel() && mShowBowlingLine)
            {
                g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(200f), 77f * Constants.S);
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombie1 || mApp.mGameMode == GameMode.PuzzleIZombie2 || mApp.mGameMode == GameMode.PuzzleIZombie3 || mApp.mGameMode == GameMode.PuzzleIZombie4 || mApp.mGameMode == GameMode.PuzzleIZombie5)
            {
                g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(244f), 73f * Constants.S);
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombie6 || mApp.mGameMode == GameMode.PuzzleIZombie7 || mApp.mGameMode == GameMode.PuzzleIZombie8 || mApp.mGameMode == GameMode.PuzzleIZombieEndless)
            {
                g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(284f), 73f * Constants.S);
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombie9)
            {
                g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(330f), 73f * Constants.S);
            }
            if (mApp.mGameMode == GameMode.ChallengeIce)
            {
                int aMainCounter = mBoard.mMainCounter;
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                mApp.mZenGarden.DrawBackdrop(g);
            }
        }

        public void DrawArtChallenge(Graphics g)
        {
            g.SetColorizeImages(true);
            g.SetColor(new SexyColor(255, 255, 255, 150));
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SeedType artChallengeSeed = GetArtChallengeSeed(j, i);
                    if (artChallengeSeed != SeedType.None && mBoard.GetTopPlantAt(j, i, TopPlant.OnlyNormalPosition) == null)
                    {
                        TPoint[] celPosition = mBoard.GetCelPosition(j, i);
                        celPosition[0].mX = (int)(celPosition[0].mX * Constants.S);
                        celPosition[0].mY = (int)(celPosition[0].mY * Constants.S);
                        celPosition[1].mX = (int)(celPosition[1].mX * Constants.S);
                        celPosition[1].mY = (int)(celPosition[1].mY * Constants.S);
                        celPosition[2].mX = (int)(celPosition[2].mX * Constants.S);
                        celPosition[2].mY = (int)(celPosition[2].mY * Constants.S);
                        celPosition[3].mX = (int)(celPosition[3].mX * Constants.S);
                        celPosition[3].mY = (int)(celPosition[3].mY * Constants.S);
                        float thePosX = celPosition[0].x; //+ (celPosition[2].x - celPosition[0].x) / 2;
                        float thePosY = celPosition[0].y; //- Constants.Challenge_SeeingStars_StarfruitPreview_Offset_Y;
                        Plant.DrawSeedType(g, artChallengeSeed, SeedType.None, DrawVariation.Normal, thePosX, thePosY);
                    }
                }
            }
            g.SetColorizeImages(false);
            GameMode aGameMode = mApp.mGameMode;
        }

        public void CheckForCompleteArtChallenge(int theGridX, int theGridY)
        {
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SeedType artChallengeSeed = GetArtChallengeSeed(j, i);
                    if (artChallengeSeed != SeedType.None)
                    {
                        Plant topPlantAt = mBoard.GetTopPlantAt(j, i, TopPlant.OnlyNormalPosition);
                        if (topPlantAt == null || topPlantAt.mSeedType != artChallengeSeed)
                        {
                            return;
                        }
                    }
                }
            }
            SpawnLevelAward(theGridX, theGridY);
        }

        public SeedType GetArtChallengeSeed(int theGridX, int theGridY)
        {
            if (theGridY >= 6)
            {
                return SeedType.None;
            }
            Debug.ASSERT(theGridX >= 0 && theGridX < 9 && theGridY >= 0);
            if (mApp.mGameMode == GameMode.ChallengeArtChallenge1)
            {
                return Challenge.gArtChallengeWallnut[theGridY, theGridX];
            }
            if (mApp.mGameMode == GameMode.ChallengeArtChallenge2)
            {
                return Challenge.gArtChallengeSunFlower[theGridY, theGridX];
            }
            if (mApp.mGameMode == GameMode.ChallengeSeeingStars)
            {
                return Challenge.gArtChallengeStarfruit[theGridY, theGridX];
            }
            return SeedType.None;
        }

        public void PlantAdded(Plant thePlant)
        {
            if (mApp.IsArtChallenge())
            {
                SeedType artChallengeSeed = GetArtChallengeSeed(thePlant.mPlantCol, thePlant.mRow);
                if (artChallengeSeed != SeedType.None && artChallengeSeed == thePlant.mSeedType)
                {
                    mApp.PlayFoley(FoleyType.ArtChallenge);
                    mApp.AddTodParticle(thePlant.mX + 40, thePlant.mY + 40, 400000, ParticleEffect.PresentPickup);
                    CheckForCompleteArtChallenge(thePlant.mPlantCol, thePlant.mRow);
                }
            }
        }

        public PlantingReason CanPlantAt(int theGridX, int theGridY, SeedType theType)
        {
            if (mApp.IsWallnutBowlingLevel())
            {
                if (theGridX > 2)
                {
                    return PlantingReason.NotPassedLine;
                }
            }
            else if (mApp.IsIZombieLevel())
            {
                int num = 6;
                if (mApp.mGameMode == GameMode.PuzzleIZombie1 || mApp.mGameMode == GameMode.PuzzleIZombie2 || mApp.mGameMode == GameMode.PuzzleIZombie3 || mApp.mGameMode == GameMode.PuzzleIZombie4 || mApp.mGameMode == GameMode.PuzzleIZombie5)
                {
                    num = 4;
                }
                if (mApp.mGameMode == GameMode.PuzzleIZombie6 || mApp.mGameMode == GameMode.PuzzleIZombie7 || mApp.mGameMode == GameMode.PuzzleIZombie8 || mApp.mGameMode == GameMode.PuzzleIZombieEndless)
                {
                    num = 5;
                }
                if (theType == SeedType.ZombieBungee)
                {
                    if (theGridX < num)
                    {
                        return PlantingReason.Ok;
                    }
                    return PlantingReason.NotHere;
                }
                else if (Challenge.IsZombieSeedType(theType))
                {
                    if (theGridX >= num)
                    {
                        return PlantingReason.Ok;
                    }
                    return PlantingReason.NotHere;
                }
            }
            else if (mApp.IsArtChallenge())
            {
                SeedType artChallengeSeed = GetArtChallengeSeed(theGridX, theGridY);
                if (artChallengeSeed != SeedType.None && artChallengeSeed != theType && theType != SeedType.Lilypad && theType != SeedType.Pumpkinshell)
                {
                    return PlantingReason.NotOnArt;
                }
                if (mApp.mGameMode == GameMode.ChallengeArtChallenge1)
                {
                    if (theGridX == 4 && theGridY == 1)
                    {
                        return PlantingReason.NotHere;
                    }
                    if (theGridX == 6 && theGridY == 1)
                    {
                        return PlantingReason.NotHere;
                    }
                }
            }
            else if (mApp.IsFinalBossLevel() && theGridX >= 8)
            {
                return PlantingReason.NotHere;
            }
            return PlantingReason.Ok;
        }

        public void DrawBeghouled(Graphics g)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (mBeghouledEated[j, i])
                    {
                        int num = mBoard.GridToPixelX(j, i) - 8;
                        int num2 = mBoard.GridToPixelY(j, i) + 40;
                        g.DrawImageCel(AtlasResources.IMAGE_CRATER, (int)(num * Constants.S), (int)(num2 * Constants.S), 1, 0);
                    }
                }
            }
        }

        public bool BeghouledIsValidMove(int x1, int y1, int x2, int y2, BeghouledBoardState theBoardState)
        {
            if (x1 < 0 || x1 >= Constants.GRIDSIZEX || x2 < 0 || x2 >= Constants.GRIDSIZEX || y1 < 0 || y1 >= Constants.MAX_GRIDSIZEY || y2 < 0 || y2 >= Constants.MAX_GRIDSIZEY)
            {
                return false;
            }
            if (mBeghouledEated[x1, y1] || mBeghouledEated[x2, y2])
            {
                return false;
            }
            SeedType seedType = theBoardState.mSeedType[x1, y1];
            SeedType seedType2 = theBoardState.mSeedType[x2, y2];
            if (seedType == SeedType.None)
            {
                return false;
            }
            theBoardState.mSeedType[x1, y1] = seedType2;
            theBoardState.mSeedType[x2, y2] = seedType;
            bool result = BeghouledBoardHasMatch(theBoardState);
            theBoardState.mSeedType[x1, y1] = seedType;
            theBoardState.mSeedType[x2, y2] = seedType2;
            return result;
        }

        public bool BeghouledCheckForPossibleMoves(BeghouledBoardState theBoardState)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (mApp.mGameMode == GameMode.ChallengeBeghouled)
                    {
                        if (BeghouledIsValidMove(j, i, j + 1, i, theBoardState) || BeghouledIsValidMove(j, i, j, i + 1, theBoardState))
                        {
                            return true;
                        }
                    }
                    else if (mApp.mGameMode == GameMode.ChallengeBeghouledTwist && BeghouledTwistMoveCausesMatch(j, i, theBoardState))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void BeghouledCheckStuckState()
        {
            if (mChallengeState != ChallengeState.Normal)
            {
                return;
            }
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
            LoadBeghouledBoardState(newBeghouledBoardState);
            if (!BeghouledCheckForPossibleMoves(newBeghouledBoardState))
            {
                mChallengeState = ChallengeState.BeghouledNoMatches;
                mChallengeStateCounter = 500;
                mBoard.DisplayAdviceAgain("[ADVICE_BEGHOULED_NO_MOVES]", MessageStyle.HintFast, AdviceType.BeghouledNoMoves);
            }
            newBeghouledBoardState.PrepareForReuse();
        }

        public void InitZombieWavesSurvival()
        {
            mBoard.mZombieAllowed[0] = true;
            int levelRandSeed = mBoard.GetLevelRandSeed();
            RandomNumbers.Seed(levelRandSeed);
            if ((int)RandomNumbers.NextNumber(5f) == 0)
            {
                mBoard.mZombieAllowed[5] = true;
            }
            else
            {
                mBoard.mZombieAllowed[2] = true;
            }
            int i = Math.Min(mSurvivalStage + 1, 9);
            while (i > 0)
            {
                ZombieType zombieType = (ZombieType)RandomNumbers.NextNumber(33f);
                if (!mBoard.mZombieAllowed[(int)zombieType] && (!Board.IsZombieTypePoolOnly(zombieType) || mBoard.StageHasPool()) && (!mBoard.StageHasRoof() || (zombieType != ZombieType.Digger && zombieType != ZombieType.Dancer)) && (!mBoard.StageHasGraveStones() || zombieType != ZombieType.Zamboni) && (mBoard.StageHasRoof() || mApp.IsSurvivalEndless(mApp.mGameMode) || zombieType != ZombieType.Bungee) && (mBoard.GetSurvivalFlagsCompleted() >= 4 || (zombieType != ZombieType.Gargantuar && zombieType != ZombieType.Zamboni)) && (mBoard.GetSurvivalFlagsCompleted() >= 10 || zombieType != ZombieType.RedeyeGargantuar) && ((mApp.mGameMode != GameMode.SurvivalNormalStage1 && mApp.mGameMode != GameMode.SurvivalNormalStage2 && mApp.mGameMode != GameMode.SurvivalNormalStage3) || zombieType <= ZombieType.Snorkel) && zombieType != ZombieType.Bobsled && zombieType != ZombieType.BackupDancer && zombieType != ZombieType.Imp && zombieType != ZombieType.DuckyTube && zombieType != ZombieType.PeaHead && zombieType != ZombieType.WallnutHead && zombieType != ZombieType.TallnutHead && zombieType != ZombieType.JalapenoHead && zombieType != ZombieType.GatlingHead && zombieType != ZombieType.SquashHead && zombieType != ZombieType.Yeti)
                {
                    mBoard.mZombieAllowed[(int)zombieType] = true;
                    i--;
                }
            }
        }

        public void InitZombieWavesFromList(ZombieType[] theZombieList, int theListLength)
        {
            for (int i = 0; i < theListLength; i++)
            {
                ZombieType zombieType = theZombieList[i];
                mBoard.mZombieAllowed[(int)zombieType] = true;
            }
        }

        public void InitZombieWaves()
        {
            if (mApp.IsSurvivalMode())
            {
                if (mSurvivalStage == 0 && mApp.IsSurvivalNormal(mApp.mGameMode))
                {
                    ZombieType[] array = new ZombieType[]
                    {
                        ZombieType.Normal,
                        ZombieType.TrafficCone
                    };
                    InitZombieWavesFromList(array, array.Length);
                }
                else if (mSurvivalStage == 0)
                {
                    ZombieType[] array2 = new ZombieType[]
                    {
                        ZombieType.Normal,
                        ZombieType.TrafficCone,
                        ZombieType.Pail
                    };
                    InitZombieWavesFromList(array2, array2.Length);
                }
                else
                {
                    InitZombieWavesSurvival();
                }
            }
            else if (mApp.mGameMode == GameMode.ChallengeSpeed)
            {
                ZombieType[] array3 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.DolphinRider,
                    ZombieType.Polevaulter
                };
                InitZombieWavesFromList(array3, array3.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengePogoParty)
            {
                ZombieType[] array4 = new ZombieType[]
                {
                    ZombieType.Pogo
                };
                InitZombieWavesFromList(array4, array4.Length);
            }
            else if (mApp.IsBungeeBlitzLevel())
            {
                ZombieType[] array5 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.Pail,
                    ZombieType.TrafficCone,
                    ZombieType.Ladder
                };
                InitZombieWavesFromList(array5, array5.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeSunnyDay)
            {
                ZombieType[] array6 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.Pail,
                    ZombieType.TrafficCone,
                    ZombieType.Football,
                    ZombieType.Polevaulter,
                    ZombieType.JackInTheBox
                };
                InitZombieWavesFromList(array6, array6.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengePortalCombat)
            {
                ZombieType[] array7 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.Pail,
                    ZombieType.Football,
                    ZombieType.Balloon
                };
                InitZombieWavesFromList(array7, array7.Length);
            }
            else if (mApp.IsLittleTroubleLevel())
            {
                ZombieType[] array8 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Football,
                    ZombieType.Snorkel
                };
                InitZombieWavesFromList(array8, array8.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeBigTime)
            {
                ZombieType[] array9 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail,
                    ZombieType.Door,
                    ZombieType.Football,
                    ZombieType.JackInTheBox
                };
                InitZombieWavesFromList(array9, array9.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeRainingSeeds)
            {
                ZombieType[] array10 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail,
                    ZombieType.Door,
                    ZombieType.Football,
                    ZombieType.Newspaper,
                    ZombieType.JackInTheBox,
                    ZombieType.Bungee
                };
                InitZombieWavesFromList(array10, array10.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeHighGravity)
            {
                ZombieType[] array11 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail,
                    ZombieType.Door,
                    ZombieType.Balloon
                };
                InitZombieWavesFromList(array11, array11.Length);
            }
            else if (mApp.IsWhackAZombieLevel())
            {
                ZombieType[] array12 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail
                };
                InitZombieWavesFromList(array12, array12.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeBobsledBonanza)
            {
                ZombieType[] array13 = new ZombieType[]
                {
                    ZombieType.Bobsled,
                    ZombieType.Zamboni
                };
                InitZombieWavesFromList(array13, array13.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeAirRaid)
            {
                ZombieType[] array14 = new ZombieType[]
                {
                    ZombieType.Balloon
                };
                InitZombieWavesFromList(array14, array14.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeBeghouled || mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                ZombieType[] array15 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail,
                    ZombieType.Door,
                    ZombieType.Football,
                    ZombieType.Newspaper
                };
                InitZombieWavesFromList(array15, array15.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeLastStand)
            {
                ZombieType[] array16 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail,
                    ZombieType.Door,
                    ZombieType.Football,
                    ZombieType.Newspaper,
                    ZombieType.JackInTheBox,
                    ZombieType.Polevaulter,
                    ZombieType.DolphinRider,
                    ZombieType.Ladder
                };
                InitZombieWavesFromList(array16, array16.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeColumn)
            {
                ZombieType[] array17 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail,
                    ZombieType.Football
                };
                InitZombieWavesFromList(array17, array17.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeInvisighoul)
            {
                ZombieType[] array18 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail,
                    ZombieType.DolphinRider,
                    ZombieType.Zamboni,
                    ZombieType.JackInTheBox
                };
                InitZombieWavesFromList(array18, array18.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeWarAndPeas)
            {
                ZombieType[] array19 = new ZombieType[]
                {
                    ZombieType.PeaHead,
                    ZombieType.WallnutHead
                };
                InitZombieWavesFromList(array19, array19.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeWarAndPeas2)
            {
                ZombieType[] array20 = new ZombieType[]
                {
                    ZombieType.PeaHead,
                    ZombieType.WallnutHead,
                    ZombieType.JalapenoHead,
                    ZombieType.GatlingHead,
                    ZombieType.SquashHead,
                    ZombieType.TallnutHead
                };
                InitZombieWavesFromList(array20, array20.Length);
            }
            else if (mApp.IsShovelLevel())
            {
                ZombieType[] array21 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone
                };
                InitZombieWavesFromList(array21, array21.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeWallnutBowling || mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
            {
                ZombieType[] array22 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail,
                    ZombieType.Polevaulter,
                    ZombieType.Newspaper
                };
                InitZombieWavesFromList(array22, array22.Length);
            }
            else if (mApp.mGameMode == GameMode.ChallengeWallnutBowling2)
            {
                ZombieType[] array23 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail,
                    ZombieType.Polevaulter,
                    ZombieType.Newspaper,
                    ZombieType.Dancer,
                    ZombieType.Door
                };
                InitZombieWavesFromList(array23, array23.Length);
            }
            else if (mApp.IsStormyNightLevel())
            {
                ZombieType[] array24 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.DolphinRider,
                    ZombieType.Balloon
                };
                InitZombieWavesFromList(array24, array24.Length);
            }
            else
            {
                ZombieType[] array25 = new ZombieType[]
                {
                    ZombieType.Normal,
                    ZombieType.TrafficCone,
                    ZombieType.Pail
                };
                InitZombieWavesFromList(array25, array25.Length);
            }
            if (mApp.CanSpawnYetis() && !mApp.IsWhackAZombieLevel() && !mApp.IsLittleTroubleLevel())
            {
                mBoard.mZombieAllowed[19] = true;
            }
        }

        public void UpdateSlotMachine()
        {
            int num = TodCommon.ClampInt(mBoard.mSunMoney, 0, 2000);
            if (num >= 1900)
            {
                mBoard.DisplayAdvice("[ADVICE_ALMOST_THERE]", MessageStyle.HintFast, AdviceType.AlmostThere);
            }
            if (num >= 2000)
            {
                SpawnLevelAward(4, 2);
                mBoard.ClearAdvice(AdviceType.None);
            }
            mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 2000, num, 0, 150, TodCurves.Linear);
            if (!mBoard.mAdvice.IsBeingDisplayed())
            {
                if (slotMachineMessageCached != 2000)
                {
                    slotMachineMessage = TodCommon.TodReplaceNumberString("[ADVICE_SLOT_MACHINE_COLLECT_SUN]", "{SCORE}", 2000);
                    slotMachineMessageCached = 2000;
                }
                mBoard.DisplayAdvice(slotMachineMessage, MessageStyle.SlotMachine, AdviceType.SlotMachineCollectSun);
            }
            if (mChallengeState != ChallengeState.SlotMachineRolling)
            {
                if (!mBoard.mAdvice.IsBeingDisplayed() && !mBoard.HasLevelAwardDropped())
                {
                    mBoard.DisplayAdviceAgain("[ADVICE_SLOT_MACHINE_SPIN_AGAIN]", MessageStyle.SlotMachine, AdviceType.SlotMachineSpinAgain);
                }
                return;
            }
            if (mBoard.mSeedBank.mSeedPackets[0].mSlotMachineCountDown > 0)
            {
                return;
            }
            Reanimation reanimation = mApp.ReanimationGet(mReanimChallenge);
            reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_return, ReanimLoopType.PlayOnceAndHold, 0, 24f);
            mChallengeState = ChallengeState.Normal;
            SeedType aPacketType = mBoard.mSeedBank.mSeedPackets[0].mPacketType;
            SeedType aPacketType2 = mBoard.mSeedBank.mSeedPackets[1].mPacketType;
            SeedType aPacketType3 = mBoard.mSeedBank.mSeedPackets[2].mPacketType;
            if (aPacketType != aPacketType2 || aPacketType2 != aPacketType3)
            {
                if (aPacketType == aPacketType2 || aPacketType2 == aPacketType3 || aPacketType == aPacketType3)
                {
                    mApp.PlayFoley(FoleyType.ArtChallenge);
                    SeedType seedType;
                    if (aPacketType == aPacketType2 || aPacketType == aPacketType3)
                    {
                        seedType = aPacketType;
                    }
                    else
                    {
                        seedType = aPacketType2;
                    }
                    if (seedType == SeedType.SlotMachineDiamond)
                    {
                        mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_2_DIAMONDS]", MessageStyle.SlotMachine, AdviceType.None);
                        mBoard.AddCoin(360, 85, CoinType.Diamond, CoinMotion.Coin);
                        return;
                    }
                    if (seedType == SeedType.SlotMachineSun)
                    {
                        mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_2_SUNS]", MessageStyle.SlotMachine, AdviceType.None);
                        int num2 = 4;
                        for (int i = 0; i < num2; i++)
                        {
                            int theX = 320 + i * 60 / num2;
                            mBoard.AddCoin(theX, 85, CoinType.Sun, CoinMotion.Coin);
                        }
                        return;
                    }
                    mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_2_OF_A_KIND]", MessageStyle.SlotMachine, AdviceType.None);
                    Coin coin = mBoard.AddCoin(360, 85, CoinType.UsableSeedPacket, CoinMotion.Coin);
                    coin.mUsableSeedType = seedType;
                }
                return;
            }
            mApp.PlayFoley(FoleyType.ArtChallenge);
            if (aPacketType == SeedType.SlotMachineDiamond)
            {
                mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_DIAMOND_JACKPOT]", MessageStyle.SlotMachine, AdviceType.None);
                int num3 = 5;
                for (int j = 0; j < num3; j++)
                {
                    int theX2 = 320 + j * 60 / num3;
                    mBoard.AddCoin(theX2, 85, CoinType.Diamond, CoinMotion.Coin);
                }
                return;
            }
            if (aPacketType == SeedType.SlotMachineSun)
            {
                mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_SUN_JACKPOT]", MessageStyle.SlotMachine, AdviceType.None);
                int num4 = 20;
                for (int k = 0; k < num4; k++)
                {
                    int theX3 = 320 + k * 60 / num4;
                    mBoard.AddCoin(theX3, 85, CoinType.Sun, CoinMotion.Coin);
                }
                return;
            }
            mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_3_OF_A_KIND]", MessageStyle.SlotMachine, AdviceType.None);
            int num5 = 3;
            for (int l = 0; l < num5; l++)
            {
                int theX4 = 320 + l * 60 / num5;
                Coin coin2 = mBoard.AddCoin(theX4, 85, CoinType.UsableSeedPacket, CoinMotion.Coin);
                coin2.mUsableSeedType = aPacketType;
            }
        }

        public void DrawSlotMachine(Graphics g)
        {
            if (mApp.mGameScene == GameScenes.ZombiesWon)
            {
                return;
            }
            Graphics @new = Graphics.GetNew(g);
            @new.mTransX = mBoard.mX;
            @new.mTransY = mBoard.mY;
            TRect trect = SlotMachineRect();
            @new.DrawImage(Resources.IMAGE_SLOTMACHINE_OVERLAY, trect.mX, trect.mY);
            Reanimation reanimation = mApp.ReanimationGet(mReanimChallenge);
            reanimation.Draw(@new);
            if (mSlotMachineRollCount < 3 && mBoard.mCursorObject.mCursorType == CursorType.Normal && mChallengeState != ChallengeState.SlotMachineRolling && !mBoard.HasLevelAwardDropped())
            {
                byte b = (byte)(150.0 * Math.Sin(mBoard.mMainCounter % 150.0 / 150.0 * 3.1415927410125732));
                SexyColor aColor = new SexyColor(b, b, b, 255);
                @new.SetColorizeImages(true);
                @new.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                @new.SetColor(aColor);
                @new.DrawImage(Resources.IMAGE_SLOTMACHINE_OVERLAY, trect.mX, trect.mY);
                reanimation.Draw(@new);
            }
            @new.PrepareForReuse();
        }

        public TRect SlotMachineGetHandleRect()
        {
            return new TRect(Constants.Challenge_SlotMachineHandle_Pos.mX, Constants.Challenge_SlotMachineHandle_Pos.mY, Constants.Challenge_SlotMachineHandle_Pos.mWidth, Constants.Challenge_SlotMachineHandle_Pos.mHeight);
        }

        public TRect SlotMachineRect()
        {
            return new TRect(Constants.Challenge_SlotMachine_Pos.X, Constants.Challenge_SlotMachine_Pos.Y, Resources.IMAGE_SLOTMACHINE_OVERLAY.mWidth, Resources.IMAGE_SLOTMACHINE_OVERLAY.mHeight);
        }

		public void WhackAZombieSpawning()//3update
		{
			if (mBoard.mCurrentWave == mBoard.mNumWaves && mBoard.mZombieCountDown == 0)
			{
				return;
			}
			//mBoard.mZombieCountDown -= 3;
			mBoard.mZombieCountDown--;
			int num = 300;
			//if (mBoard.mZombieCountDown >= 100 && mBoard.mZombieCountDown < 103 && mBoard.mCurrentWave > 0)
			if (mBoard.mZombieCountDown == 100 && mBoard.mCurrentWave > 0)
			{
				int graveStoneCount = mBoard.GetGraveStoneCount();
				int num2 = 5;
				int theGraveCount = Math.Max(1, num2 - graveStoneCount);
				WhackAZombiePlaceGraves(theGraveCount);
			}
			//if (mBoard.mZombieCountDown >= 5 && mBoard.mZombieCountDown < 8)
			if (mBoard.mZombieCountDown == 5)
			{
				mBoard.NextWaveComing();
			}
			//if (mBoard.mZombieCountDown >= 0 && mBoard.mZombieCountDown < 3)
			if (mBoard.mZombieCountDown == 0)
			{
				mBoard.mZombieCountDown = 2000;
				mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
				mBoard.mCurrentWave++;
				if (mBoard.mCurrentWave == mBoard.mNumWaves)
				{
					//mChallengeStateCounter = 300;
					mChallengeStateCounter = 100;
				}
				else
				{
					//mChallengeStateCounter = 3;
					mChallengeStateCounter = 1;
				}
			}
			else if (mBoard.mZombieCountDown < 300)
			{
				return;
			}
			//mChallengeStateCounter -= 3;
			mChallengeStateCounter--;
			//if (mChallengeStateCounter < 0 || mChallengeStateCounter >= 3)
			if (mChallengeStateCounter != 0)
			{
				return;
			}
			int num3 = TodCommon.ClampInt((mBoard.mCurrentWave - 1) * 6 / 12, 0, 5);
			int num4 = 1;
			ZombieType theZombieType = ZombieType.Normal;
			int num5 = RandomNumbers.NextNumber(100);
			int num6 = RandomNumbers.NextNumber(100);
			bool flag = mBoard.mCurrentWave == mBoard.mNumWaves;
			if (flag)
			{
				num4 = 20;
			}
			else if (num5 < aTripleChance[num3])
			{
				num4 = 3;
			}
			else if (num5 < aTripleChance[num3] + aDoubleChance[num3])
			{
				num4 = 2;
			}
			if (num6 < aPailChance[num3] && num4 < 3)
			{
				theZombieType = ZombieType.Pail;
			}
			else if (num6 < aPailChance[num3] + aConeChance[num3])
			{
				theZombieType = ZombieType.TrafficCone;
			}
			int num7 = 0;
			for (int i = 0; i < Challenge.aGridPicks.Length; i++)
			{
				Challenge.aGridPicks[i].Reset();
			}
			int num8 = -1;
			GridItem gridItem = null;
			while (mBoard.IterateGridItems(ref gridItem, ref num8))
			{
				if (gridItem.mGridItemType == GridItemType.Gravestone)
				{
					Plant topPlantAt = mBoard.GetTopPlantAt(gridItem.mGridX, gridItem.mGridY, TopPlant.OnlyNormalPosition);
					if (topPlantAt == null || topPlantAt.mSeedType != SeedType.Gravebuster)
					{
						Challenge.aGridPickItemArray[num7] = gridItem;
						Challenge.aGridPicks[num7].mItem = num7;
						Challenge.aGridPicks[num7].mWeight = 1;
						num7++;
					}
				}
			}
			float theMax = TodCommon.TodAnimateCurve(1, 12, mBoard.mCurrentWave, 1, 3, TodCurves.EaseIn);
			if (num4 > num7)
			{
				num4 = num7;
			}
			for (int j = 0; j < num4; j++)
			{
				int num9 = (int)TodCommon.TodPickFromWeightedArray(Challenge.aGridPicks, num7);
				GridItem gridItem2 = Challenge.aGridPickItemArray[num9];
				Challenge.aGridPicks[num9].mWeight = 0;
				if (flag)
				{
					ZombieType[] array = new ZombieType[]
					{
						ZombieType.TrafficCone,
						ZombieType.Pail
					};
					theZombieType = array[RandomNumbers.NextNumber(array.Length)];
					theMax = 2f;
				}
				Zombie zombie = mBoard.AddZombie(theZombieType, mBoard.mCurrentWave);
				if (zombie == null)
				{
					break;
				}
				zombie.RiseFromGrave(gridItem2.mGridX, gridItem2.mGridY);
				zombie.mPhaseCounter = 50;
				zombie.mVelX = TodCommon.RandRangeFloat(0.5f, theMax);
				zombie.UpdateAnimSpeed();
			}
			int theMin = TodCommon.TodAnimateCurve(1, 12, mBoard.mCurrentWave, 100, 30, TodCurves.Linear);
			int theMax2 = TodCommon.TodAnimateCurve(1, 12, mBoard.mCurrentWave, 200, 60, TodCurves.Linear);
			mChallengeStateCounter = TodCommon.RandRangeInt(theMin, theMax2);
			if (flag)
			{
				mBoard.mZombieCountDown = 0;
				mChallengeStateCounter = 0;
			}
		}

        public bool UpdateZombieSpawning()//3update
        {
            if (mApp.IsWhackAZombieLevel())
            {
                WhackAZombieSpawning();
                return true;
            }
            return mApp.IsFinalBossLevel() || mApp.mGameMode == GameMode.ChallengeIce || mApp.mGameMode == GameMode.ChallengeZenGarden || mApp.mGameMode == GameMode.TreeOfWisdom || mApp.mGameMode == GameMode.ChallengeZombiquarium || mApp.IsIZombieLevel() || mApp.IsSquirrelLevel() || mApp.IsScaryPotterLevel() || (mApp.mGameMode == GameMode.ChallengeLastStand && mChallengeState != ChallengeState.LastStandOnslaught);
        }

        public void BeghouledClearCrater(int theCount)
        {
            mBoard.ClearAdvice(AdviceType.BeghouledUseCrater1);
            mBoard.ClearAdvice(AdviceType.BeghouledUseCrater2);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (mBeghouledEated[i, j])
                    {
                        mBeghouledEated[i, j] = false;
                        theCount--;
                        if (theCount == 0)
                        {
                            BeghouledUpdateCraters();
                            return;
                        }
                    }
                }
            }
        }

        public void MouseDownWhackAZombie(int x, int y)
        {
            x = (int)(x * Constants.IS);
            y = (int)(y * Constants.IS);
            Reanimation reanimation = mApp.ReanimationGet(mBoard.mCursorObject.mReanimCursorID);
            reanimation.mAnimTime = 0.2f;
            mApp.PlayFoley(FoleyType.Swing);
            Zombie zombie = null;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie2 = mBoard.mZombies[i];
                if (!zombie2.mDead && !zombie2.IsDeadOrDying())
                {
                    TRect zombieRect = zombie2.GetZombieRect();
                    zombieRect.mHeight -= 50;
                    if (GameConstants.GetCircleRectOverlap(x, y - 20, 45, zombieRect) && (zombie == null || zombie2.mRenderOrder >= zombie.mRenderOrder))
                    {
                        zombie = zombie2;
                    }
                }
            }
            if (zombie != null)
            {
                if (zombie.mHelmType != HelmType.None)
                {
                    if (zombie.mHelmType == HelmType.Pail)
                    {
                        mApp.PlayFoley(FoleyType.ShieldHit);
                    }
                    else if (zombie.mHelmType == HelmType.TrafficCone)
                    {
                        mApp.PlayFoley(FoleyType.PlasticHit);
                    }
                    zombie.TakeHelmDamage(900, 0U);
                    return;
                }
                mApp.PlayFoley(FoleyType.Bonk);
                mApp.AddTodParticle(TodCommon.PixelAligned(x - 3f), TodCommon.PixelAligned(y + 9f), 800000, ParticleEffect.Pow);
                zombie.DieWithLoot();
                mBoard.ClearCursor();
            }
        }

        public bool IsStormyNightPitchBlack()
        {
            return mApp.IsStormyNightLevel() && (mChallengeState != ChallengeState.StormFlash1 || mChallengeStateCounter >= 300) && (mChallengeState != ChallengeState.StormFlash2 || mChallengeStateCounter >= 300) && (mChallengeState != ChallengeState.StormFlash3 || mChallengeStateCounter >= 150);
        }

        public void DrawStormNight(Graphics g)
        {
            if (mChallengeState == ChallengeState.StormFlash1 && mChallengeStateCounter < 300)
            {
                if (mChallengeStateCounter > 150)
                {
                    DrawStormFlash(g, mChallengeStateCounter - 150, 255);
                }
                else
                {
                    DrawStormFlash(g, mChallengeStateCounter, 92);
                }
            }
            else if (mChallengeState == ChallengeState.StormFlash2 && mChallengeStateCounter < 300)
            {
                DrawStormFlash(g, mChallengeStateCounter / 2, 255);
            }
            else if (mChallengeState == ChallengeState.StormFlash3 && mChallengeStateCounter < 150)
            {
                DrawStormFlash(g, mChallengeStateCounter, 255);
            }
            else
            {
                g.SetColor(new SexyColor(0, 0, 0, 255));
                g.FillRect(-1000, -1000, Constants.BOARD_WIDTH + 2000, Constants.BOARD_HEIGHT + 2000);
            }
            mBoard.DrawUIBottom(g);
            mBoard.DrawTopRightUI(g);
        }

        public void UpdateStormyNight()
        {
            if (mBoard.mPaused)
            {
                if (mChallengeStateCounter == 1)
                {
                    return;
                }
                if (mChallengeStateCounter == 150 && mChallengeState == ChallengeState.StormFlash1)
                {
                    mChallengeStateCounter = 1;
                    return;
                }
            }
            mChallengeStateCounter--;
            if ((mChallengeStateCounter == 300 && mChallengeState == ChallengeState.StormFlash1) || (mChallengeStateCounter == 150 && mChallengeState == ChallengeState.StormFlash1) || (mChallengeStateCounter == 300 && mChallengeState == ChallengeState.StormFlash2) || (mChallengeStateCounter == 150 && mChallengeState == ChallengeState.StormFlash3))
            {
                mApp.PlayFoley(FoleyType.Thunder);
            }
            if (mChallengeStateCounter <= 0)
            {
                if (mApp.mGameScene == GameScenes.ZombiesWon)
                {
                    mChallengeStateCounter = 150 + TodCommon.RandRangeInt(-50, 50);
                    mChallengeState = ChallengeState.StormFlash3;
                    return;
                }
                if (mApp.mGameScene != GameScenes.Playing)
                {
                    mChallengeStateCounter = 0;
                    mChallengeState = ChallengeState.Normal;
                    return;
                }
                int count = mBoard.mZombies.Count;
                for (int i = 0; i < count; i++)
                {
                    Zombie zombie = mBoard.mZombies[i];
                    if (!zombie.mDead && zombie.mZombieType == ZombieType.Yeti)
                    {
                        mChallengeStateCounter = 150 + TodCommon.RandRangeInt(200, 300);
                        mChallengeState = (ChallengeState)TodCommon.RandRangeInt(5, 7);
                        return;
                    }
                }
                int theMax;
                if (RandomNumbers.NextNumber(2) == 0)
                {
                    theMax = 750;
                }
                else
                {
                    theMax = 400;
                }
                mChallengeStateCounter = 150 + TodCommon.RandRangeInt(300, theMax);
                mChallengeState = (ChallengeState)TodCommon.RandRangeInt(5, 7);
            }
        }

        public void InitLevel()
        {
            if (mApp.mGameMode == GameMode.ChallengeRainingSeeds)
            {
                mChallengeStateCounter = 100;
                mApp.PlayFoley(FoleyType.Rain);
            }
            if (mApp.IsStormyNightLevel())
            {
                mChallengeState = ChallengeState.StormFlash2;
                mChallengeStateCounter = 150;
                mApp.PlayFoley(FoleyType.Rain);
            }
            if (mApp.IsFinalBossLevel())
            {
                mBoard.mSeedBank.AddSeed(SeedType.Cabbagepult);
                mBoard.mSeedBank.AddSeed(SeedType.Jalapeno);
                mBoard.mSeedBank.AddSeed(SeedType.Cabbagepult);
                mBoard.mSeedBank.AddSeed(SeedType.Iceshroom);
                mConveyorBeltCounter = 1000;
            }
            if (mApp.mGameMode == GameMode.ChallengeZenGarden)
            {
                mApp.mZenGarden.mGardenType = GardenType.Main;
                mApp.mZenGarden.ZenGardenInitLevel(false);
            }
            if (mApp.mGameMode == GameMode.ChallengeColumn)
            {
                mBoard.mSeedBank.AddSeed(SeedType.Potatomine);
                mBoard.mSeedBank.AddSeed(SeedType.Tallnut);
                mBoard.mSeedBank.AddSeed(SeedType.Melonpult);
                mBoard.mSeedBank.AddSeed(SeedType.Magnetshroom);
                mBoard.mSeedBank.AddSeed(SeedType.InstantCoffee);
                mBoard.mSeedBank.AddSeed(SeedType.Melonpult);
                mConveyorBeltCounter = 1000;
            }
            if (mApp.mGameMode == GameMode.ChallengeInvisighoul)
            {
                mBoard.mSeedBank.AddSeed(SeedType.Peashooter);
                mBoard.mSeedBank.AddSeed(SeedType.Iceshroom);
                mConveyorBeltCounter = 1000;
            }
            if (mApp.IsIZombieLevel())
            {
                IZombieInitLevel();
            }
            if (mApp.IsScaryPotterLevel())
            {
                ScaryPotterPopulate();
            }
            if (mApp.IsFirstTimeAdventureMode() && mBoard.mLevel == 5)
            {
                mBoard.NewPlant(5, 1, SeedType.Peashooter, SeedType.None);
                mBoard.NewPlant(7, 2, SeedType.Peashooter, SeedType.None);
                mBoard.NewPlant(6, 3, SeedType.Peashooter, SeedType.None);
            }
            if (mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                mChallengeGridX = -1;
                mChallengeGridY = -1;
            }
        }

        public void SpawnZombieWave()
        {
            if (mApp.IsContinuousChallenge() && mBoard.mCurrentWave == mBoard.mNumWaves)
            {
                mBoard.mCurrentWave = mBoard.mNumWaves - 1;
                for (int i = 0; i < 50; i++)
                {
                    ZombieType zombieType = mBoard.mZombiesInWave[mBoard.mCurrentWave, i];
                    if (zombieType == ZombieType.Invalid)
                    {
                        break;
                    }
                    if (zombieType == ZombieType.Flag)
                    {
                        mBoard.mZombiesInWave[mBoard.mCurrentWave, i] = ZombieType.Normal;
                    }
                }
            }
            bool flag = mBoard.IsFlagWave(mBoard.mCurrentWave);
            if (mApp.mGameMode == GameMode.ChallengeGraveDanger && mBoard.mCurrentWave != mBoard.mNumWaves - 1)
            {
                if (flag)
                {
                    mBoard.SpawnZombiesFromGraves();
                }
                else if (mBoard.mCurrentWave > 5)
                {
                    GraveDangerSpawnRandomGrave();
                }
            }
            if (mApp.IsSurvivalMode() && mBoard.mBackground == BackgroundType.Num2Night && mBoard.mCurrentWave == mBoard.mNumWaves - 1)
            {
                int graveStoneCount = mBoard.GetGraveStoneCount();
                if (mApp.IsSurvivalNormal(mApp.mGameMode))
                {
                    if (graveStoneCount < 8)
                    {
                        GraveDangerSpawnRandomGrave();
                    }
                }
                else if (graveStoneCount < 12)
                {
                    GraveDangerSpawnRandomGrave();
                }
            }
            if (mApp.IsBungeeBlitzLevel() && flag)
            {
                mBoard.DisplayAdvice("[ADVICE_BUNGEES_INCOMING]", MessageStyle.HintFast, AdviceType.None);
            }
        }

        public void GraveDangerSpawnRandomGrave()
        {
            TodWeightedGridArray[] array = new TodWeightedGridArray[Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY];
            int num = 0;
            for (int i = 4; i < Constants.GRIDSIZEX; i++)
            {
                for (int j = 0; j < Constants.MAX_GRIDSIZEY; j++)
                {
                    if (mBoard.CanAddGraveStoneAt(i, j))
                    {
                        Plant topPlantAt = mBoard.GetTopPlantAt(i, j, TopPlant.Any);
                        if (topPlantAt != null)
                        {
                            array[num].mWeight = 1;
                        }
                        else
                        {
                            array[num].mWeight = 100000;
                        }
                        array[num].mX = i;
                        array[num].mY = j;
                        num++;
                    }
                }
            }
            if (num == 0)
            {
                return;
            }
            TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(array, num);
            GraveDangerSpawnGraveAt(todWeightedGridArray.mX, todWeightedGridArray.mY);
        }

        public void GraveDangerSpawnGraveAt(int x, int y)
        {
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && plant.mPlantCol == x && plant.mRow == y)
                {
                    plant.Die();
                }
            }
            mBoard.mEnableGraveStones = true;
            GridItem gridItem = mBoard.AddAGraveStone(x, y);
            if (gridItem != null)
            {
                gridItem.AddGraveStoneParticles();
            }
        }

        public void SpawnLevelAward(int theGridX, int theGridY)
        {
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            float num = mBoard.GridToPixelX(theGridX, theGridY) + 40;
            float num2 = mBoard.GridToPixelY(theGridX, theGridY) + 40;
            CoinType theCoinType;
            if (mApp.IsAdventureMode() && mApp.IsFirstTimeAdventureMode())
            {
                theCoinType = CoinType.FinalSeedPacket;
            }
            else if (mApp.IsAdventureMode() || mApp.HasBeatenChallenge(mApp.mGameMode))
            {
                theCoinType = CoinType.AwardMoneyBag;
            }
            else
            {
                theCoinType = CoinType.Trophy;
            }
            mBoard.mLevelAwardSpawned = true;
            mApp.mBoardResult = BoardResult.Won;
            mApp.PlayFoley(FoleyType.SpawnSun);
            mBoard.AddCoin((int)num, (int)num2, theCoinType, CoinMotion.Coin);
            mApp.AddTodParticle(400f, 300f, 400000, ParticleEffect.ScreenFlash);
            if (!mApp.IsIZombieLevel())
            {
                int count = mBoard.mZombies.Count;
                for (int i = 0; i < count; i++)
                {
                    Zombie zombie = mBoard.mZombies[i];
                    if (!zombie.mDead && !zombie.IsDeadOrDying())
                    {
                        zombie.TakeDamage(1800, 0U);
                    }
                }
            }
        }

        public void BeghouledScore(int x, int y, int theNumPlants, bool theIsHorizontal)
        {
            mApp.PlayFoley(FoleyType.ArtChallenge);
            float num = mBoard.GridToPixelX(x, y);
            float num2 = mBoard.GridToPixelY(x, y);
            if (theIsHorizontal)
            {
                if (theNumPlants == 3)
                {
                    num += 80f;
                }
                else if (theNumPlants == 4)
                {
                    num += 120f;
                }
                else
                {
                    num += 160f;
                }
            }
            else if (theNumPlants == 3)
            {
                num2 += 80f;
            }
            else if (theNumPlants == 4)
            {
                num2 += 120f;
            }
            else
            {
                num2 += 160f;
            }
            mChallengeScore++;
            if (mBoard.mSeedBank.mNumPackets == 0)
            {
                mBoard.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.Repeater, SeedType.None);
                mBoard.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.Fumeshroom, SeedType.None);
                mBoard.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.Tallnut, SeedType.None);
                mBoard.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.BeghouledButtonShuffle, SeedType.None);
                mBoard.mSeedBank.mNumPackets = 4;
                mBoard.DisplayAdvice("[ADVICE_BEGHOULED_SAVE_SUN]", MessageStyle.HintFast, AdviceType.BeghouledSaveSun);
                if (BeghouledCanClearCrater())
                {
                    mBoard.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.BeghouledButtonCrater, SeedType.None);
                    mBoard.mSeedBank.mNumPackets = 5;
                }
            }
            else
            {
                if (!mBoard.mAdvice.IsBeingDisplayed())
                {
                    string theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_BEGHOULED_MATCH_3]", "{SCORE}", 75);
                    mBoard.DisplayAdvice(theAdvice, MessageStyle.HintFast, AdviceType.BeghouledMatch3);
                }
                if (mChallengeScore >= 70)
                {
                    mBoard.DisplayAdvice("[ADVICE_BEGHOULED_ALMOST_THERE]", MessageStyle.HintFast, AdviceType.AlmostThere);
                }
            }
            if (mChallengeScore >= 75)
            {
                mChallengeScore = 75;
                SpawnLevelAward(x, y);
                mBoard.ClearAdvice(AdviceType.None);
            }
            else
            {
                int num3 = theNumPlants - 2 + mBeghouledMatchesThisMove;
                if (theNumPlants >= 5)
                {
                    num3 += 2;
                }
                num3 = TodCommon.ClampInt(num3, 1, 5);
                for (int i = 0; i < num3; i++)
                {
                    mBoard.AddCoin((int)(num - 10f + 20f * i), (int)num2, CoinType.Sun, CoinMotion.Coin);
                }
            }
            mBeghouledMatchesThisMove++;
        }

        public void DrawStormFlash(Graphics g, int theTime, int theMaxAmount)
        {
            RandomNumbers.Seed(mBoard.mMainCounter / 6);
            int num = TodCommon.TodAnimateCurve(150, 0, theTime, 255 - theMaxAmount, 255, TodCurves.Linear);
            int theAlpha = TodCommon.ClampInt((int)(num + RandomNumbers.NextNumber(64f) - 32f), 0, 255);
            g.SetColor(new SexyColor(0, 0, 0, theAlpha));
            g.FillRect(-1000, -1000, Constants.BOARD_WIDTH + 2000, Constants.BOARD_HEIGHT + 2000);
            int theAlpha2 = TodCommon.TodAnimateCurve(150, 75, theTime, theMaxAmount, 0, TodCurves.Linear);
            g.SetColor(new SexyColor(255, 255, 255, theAlpha2));
            g.FillRect(-1000, -1000, Constants.BOARD_WIDTH + 2000, Constants.BOARD_HEIGHT + 2000);
        }

        public void UpdateRainingSeeds()
        {
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            mChallengeStateCounter--;
            if (mChallengeStateCounter != 0)
            {
                return;
            }
            mChallengeStateCounter = 500 + RandomNumbers.NextNumber(500);
            int theX = 100 + RandomNumbers.NextNumber(550);
            Coin coin = mBoard.AddCoin(theX, 60, CoinType.UsableSeedPacket, CoinMotion.FromSkySlow);
            do
            {
                coin.mUsableSeedType = (SeedType)RandomNumbers.NextNumber(mApp.GetSeedsAvailable());
            }
            while (mBoard.SeedNotRecommendedForLevel(coin.mUsableSeedType) != 0U || !mApp.HasSeedType(coin.mUsableSeedType) || Plant.IsUpgrade(coin.mUsableSeedType) || coin.mUsableSeedType == SeedType.Sunflower || coin.mUsableSeedType == SeedType.Twinsunflower || coin.mUsableSeedType == SeedType.InstantCoffee || coin.mUsableSeedType == SeedType.Umbrella || coin.mUsableSeedType == SeedType.Sunshroom || coin.mUsableSeedType == SeedType.Imitater);
            int theTimeAge = mBoard.CountPlantByType(SeedType.Lilypad);
            int theTimeEnd = 18;
            int thePositionStart = 30;
            int num = TodCommon.TodAnimateCurve(0, theTimeEnd, theTimeAge, thePositionStart, 1, TodCurves.Linear);
            if (RandomNumbers.NextNumber(100) < num)
            {
                coin.mUsableSeedType = SeedType.Lilypad;
            }
        }

        public void PlayBossEnter()
        {
            mBoard.AddZombie(ZombieType.Boss, 0);
        }

        public void UpdateConveyorBelt()
        {
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            mBoard.mSeedBank.UpdateConveyorBelt();
            mConveyorBeltCounter--;
            if (mConveyorBeltCounter > 0)
            {
                return;
            }
            float num = 1f;
            if (mApp.IsFinalBossLevel())
            {
                num = 0.875f;
            }
            else if (mApp.IsShovelLevel() || mApp.mGameMode == GameMode.ChallengePortalCombat)
            {
                num = 1.5f;
            }
            else if (mApp.mGameMode == GameMode.ChallengeInvisighoul)
            {
                num = 2f;
            }
            else if (mApp.mGameMode == GameMode.ChallengeColumn)
            {
                num = 3f;
            }
            if (mBoard.mSeedBank.GetNumSeedsOnConveyorBelt() > 8)
            {
                mConveyorBeltCounter = 1000 * (int)num;
            }
            else if (mBoard.mSeedBank.GetNumSeedsOnConveyorBelt() > 6)
            {
                mConveyorBeltCounter = 500 * (int)num;
            }
            else if (mBoard.mSeedBank.GetNumSeedsOnConveyorBelt() > 4)
            {
                mConveyorBeltCounter = 425 * (int)num;
            }
            else
            {
                mConveyorBeltCounter = 400 * (int)num;
            }
            for (int i = 0; i < 20; i++)
            {
                Challenge.aSeedPickArray[i].Reset();
            }
            int num2 = 0;
            if (mBoard.mLevel == 10)
            {
                Challenge.aSeedPickArray[num2].mItem = 0;
                Challenge.aSeedPickArray[num2].mWeight = 20;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 2;
                Challenge.aSeedPickArray[num2].mWeight = 20;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 3;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 7;
                Challenge.aSeedPickArray[num2].mWeight = 20;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 5;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 6;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 4;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
            }
            else if (mBoard.mLevel == 20)
            {
                Challenge.aSeedPickArray[num2].mItem = 11;
                Challenge.aSeedPickArray[num2].mWeight = 20;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 14;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 15;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 12;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 13;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 10;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 8;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
            }
            else if (mBoard.mLevel == 30)
            {
                Challenge.aSeedPickArray[num2].mItem = 16;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 17;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 18;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 19;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 20;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 21;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 22;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 23;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
            }
            else if (mBoard.mLevel == 40)
            {
                Challenge.aSeedPickArray[num2].mItem = 16;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 24;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 31;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 27;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 26;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 29;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 28;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 30;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
            }
            else if (mApp.IsFinalBossLevel())
            {
                Challenge.aSeedPickArray[num2].mItem = 33;
                Challenge.aSeedPickArray[num2].mWeight = 55;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 39;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 20;
                Challenge.aSeedPickArray[num2].mWeight = 12;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 32;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 34;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 14;
                Challenge.aSeedPickArray[num2].mWeight = 8;
                num2++;
            }
            else if (mApp.IsShovelLevel())
            {
                Challenge.aSeedPickArray[num2].mItem = 0;
                Challenge.aSeedPickArray[num2].mWeight = 100;
                num2++;
            }
            else if (mApp.mGameMode == GameMode.ChallengeWallnutBowling2)
            {
                Challenge.aSeedPickArray[num2].mItem = 3;
                Challenge.aSeedPickArray[num2].mWeight = 85;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 49;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 50;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
            }
            else if (mApp.IsWallnutBowlingLevel())
            {
                Challenge.aSeedPickArray[num2].mItem = 3;
                Challenge.aSeedPickArray[num2].mWeight = 85;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 49;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
            }
            else if (mApp.IsLittleTroubleLevel())
            {
                Challenge.aSeedPickArray[num2].mItem = 16;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 3;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 0;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 2;
                Challenge.aSeedPickArray[num2].mWeight = 35;
                num2++;
            }
            else if (mApp.IsStormyNightLevel())
            {
                Challenge.aSeedPickArray[num2].mItem = 16;
                Challenge.aSeedPickArray[num2].mWeight = 30;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 26;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 0;
                Challenge.aSeedPickArray[num2].mWeight = 20;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 8;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 2;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
            }
            else if (mApp.IsBungeeBlitzLevel())
            {
                Challenge.aSeedPickArray[num2].mItem = 33;
                Challenge.aSeedPickArray[num2].mWeight = 50;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 6;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 30;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 2;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
            }
            else if (mApp.mGameMode == GameMode.ChallengePortalCombat)
            {
                Challenge.aSeedPickArray[num2].mItem = 0;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 7;
                Challenge.aSeedPickArray[num2].mWeight = 20;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 22;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 26;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 3;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 2;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
            }
            else if (mApp.mGameMode == GameMode.ChallengeColumn)
            {
                Challenge.aSeedPickArray[num2].mItem = 33;
                Challenge.aSeedPickArray[num2].mWeight = 155;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 39;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 6;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 30;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 20;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 17;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
            }
            else if (mApp.mGameMode == GameMode.ChallengeInvisighoul)
            {
                Challenge.aSeedPickArray[num2].mItem = 0;
                Challenge.aSeedPickArray[num2].mWeight = 25;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 3;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 34;
                Challenge.aSeedPickArray[num2].mWeight = 5;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 17;
                Challenge.aSeedPickArray[num2].mWeight = 15;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 16;
                Challenge.aSeedPickArray[num2].mWeight = 30;
                num2++;
                Challenge.aSeedPickArray[num2].mItem = 14;
                Challenge.aSeedPickArray[num2].mWeight = 10;
                num2++;
            }
            else
            {
                Debug.ASSERT(false);
            }
            Debug.ASSERT(num2 <= 20);
            int j = 0;
            SeedType seedType;
            while (j < num2)
            {
                TodWeightedArray todWeightedArray = Challenge.aSeedPickArray[j];
                seedType = (SeedType)todWeightedArray.mItem;
                int num3 = mBoard.mSeedBank.CountOfTypeOnConveyorBelt((SeedType)todWeightedArray.mItem);
                if (seedType != SeedType.Gravebuster)
                {
                    goto IL_D4E;
                }
                int graveStoneCount = mBoard.GetGraveStoneCount();
                int num4 = mBoard.CountPlantByType(seedType);
                if (graveStoneCount > num4 + num3)
                {
                    goto IL_D4E;
                }
                todWeightedArray.mWeight = 0;
                IL_E81:
                j++;
                continue;
                IL_D4E:
                if (seedType == SeedType.Lilypad)
                {
                    int num5 = mBoard.CountPlantByType(seedType);
                    int theTimeEnd = 18;
                    todWeightedArray.mWeight = TodCommon.TodAnimateCurve(0, theTimeEnd, num5 + num3, todWeightedArray.mWeight, 1, TodCurves.Linear);
                }
                if (seedType == SeedType.Flowerpot)
                {
                    int num6 = mBoard.CountPlantByType(seedType);
                    int theTimeEnd2 = 35;
                    if (mApp.mGameMode == GameMode.ChallengeColumn)
                    {
                        theTimeEnd2 = 45;
                    }
                    todWeightedArray.mWeight = TodCommon.TodAnimateCurve(0, theTimeEnd2, num6 + num3, todWeightedArray.mWeight, 1, TodCurves.Linear);
                }
                if (mApp.IsFinalBossLevel())
                {
                    if (seedType == SeedType.Melonpult || seedType == SeedType.Kernelpult || seedType == SeedType.Cabbagepult)
                    {
                        int num7 = mBoard.CountEmptyPotsOrLilies(SeedType.Flowerpot);
                        if (num7 <= 2)
                        {
                            todWeightedArray.mWeight /= 5;
                        }
                        else if (num7 <= 5)
                        {
                            todWeightedArray.mWeight /= 3;
                        }
                    }
                    if (seedType == SeedType.Flowerpot)
                    {
                        Zombie bossZombie = mBoard.GetBossZombie();
                        if (bossZombie.mZombiePhase == ZombiePhase.BossDropRv)
                        {
                            todWeightedArray.mWeight = 500;
                        }
                    }
                }
                if (num2 <= 2)
                {
                    goto IL_E81;
                }
                if (num3 >= 4)
                {
                    todWeightedArray.mWeight = 1;
                    goto IL_E81;
                }
                if (num3 >= 3)
                {
                    todWeightedArray.mWeight = 5;
                    goto IL_E81;
                }
                if (seedType == mLastConveyorSeedType)
                {
                    todWeightedArray.mWeight /= 2;
                    goto IL_E81;
                }
                goto IL_E81;
            }
            seedType = (SeedType)TodCommon.TodPickFromWeightedArray(Challenge.aSeedPickArray, num2);
            mBoard.mSeedBank.AddSeed(seedType);
            mLastConveyorSeedType = seedType;
        }

        public void PortalStart()
        {
            mChallengeStateCounter = 9000;
            GridItem newGridItem = GridItem.GetNewGridItem();
            newGridItem.mGridItemType = GridItemType.PortalSquare;
            newGridItem.mGridX = 2;
            newGridItem.mGridY = 0;
            newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, newGridItem.mGridY, 0);
            newGridItem.OpenPortal();
            mBoard.mGridItems.Add(newGridItem);
            GridItem newGridItem2 = GridItem.GetNewGridItem();
            newGridItem2.mGridItemType = GridItemType.PortalSquare;
            newGridItem2.mGridX = 9;
            newGridItem2.mGridY = 1;
            newGridItem2.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, newGridItem2.mGridY, 0);
            newGridItem2.OpenPortal();
            mBoard.mGridItems.Add(newGridItem2);
            GridItem newGridItem3 = GridItem.GetNewGridItem();
            newGridItem3.mGridItemType = GridItemType.PortalCircle;
            newGridItem3.mGridX = 9;
            newGridItem3.mGridY = 3;
            newGridItem3.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, newGridItem3.mGridY, 0);
            newGridItem3.OpenPortal();
            mBoard.mGridItems.Add(newGridItem3);
            GridItem newGridItem4 = GridItem.GetNewGridItem();
            newGridItem4.mGridItemType = GridItemType.PortalCircle;
            newGridItem4.mGridX = 2;
            newGridItem4.mGridY = 4;
            newGridItem4.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, newGridItem4.mGridY, 0);
            newGridItem4.OpenPortal();
            mBoard.mGridItems.Add(newGridItem4);
            mBoard.mZombieCountDown = 200;
            mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
            mConveyorBeltCounter = 200;
        }

        public void UpdatePortalCombat()
        {
            int num = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num))
            {
                if (gridItem.mGridItemState != GridItemState.PortalClosed && (gridItem.mGridItemType == GridItemType.PortalCircle || gridItem.mGridItemType == GridItemType.PortalSquare))
                {
                    UpdatePortal(gridItem);
                }
            }
            if (mBoard.HasLevelAwardDropped())
            {
                mBoard.ClearAdvice(AdviceType.PortalRelocating);
                return;
            }
            mChallengeStateCounter--;
            if (mChallengeStateCounter == 500)
            {
                mBoard.DisplayAdviceAgain("[ADVICE_PORTAL_RELOCATING]", MessageStyle.HintFast, AdviceType.PortalRelocating);
            }
            if (mChallengeStateCounter <= 0)
            {
                mBoard.ClearAdvice(AdviceType.PortalRelocating);
                mChallengeStateCounter = 6000;
                MoveAPortal();
            }
        }

        public GridItem GetOtherPortal(GridItem thePortal)
        {
            int num = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num))
            {
                if (thePortal != gridItem && gridItem.mGridItemState != GridItemState.PortalClosed && thePortal.mGridItemType == gridItem.mGridItemType)
                {
                    return gridItem;
                }
            }
            return null;
        }

        public void UpdatePortal(GridItem thePortal)
        {
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && zombie.mRow == thePortal.mGridY && zombie.mLastPortalX != thePortal.mGridX)
                {
                    TRect zombieRect = zombie.GetZombieRect();
                    int num = zombieRect.mX + zombieRect.mWidth / 2;
                    int num2 = thePortal.mGridX * 80 + 25;
                    int num3 = Math.Abs(num - num2);
                    if (num3 <= 45)
                    {
                        GridItem otherPortal = GetOtherPortal(thePortal);
                        if (otherPortal != null)
                        {
                            int num4 = num - zombie.mX;
                            num4 = (int)(num4 * 0.5f);
                            if (zombie.IsWalkingBackwards())
                            {
                                num4 -= 60;
                            }
                            zombie.mLastPortalX = otherPortal.mGridX;
                            zombie.mX = otherPortal.mGridX * 80 - num4;
                            zombie.mPosX = zombie.mX;
                            zombie.SetRow(otherPortal.mGridY);
                            zombie.mY = (int)zombie.GetPosYBasedOnRow(otherPortal.mGridY);
                            zombie.mPosY = zombie.mY;
                            zombie.cachedZombieRectUpToDate = false;
                        }
                    }
                }
            }
            int num5 = -1;
            Projectile projectile = null;
            while (mBoard.IterateProjectiles(ref projectile, ref num5))
            {
                if (projectile.mMotionType == ProjectileMotion.Straight && projectile.mRow == thePortal.mGridY && projectile.mLastPortalX != thePortal.mGridX)
                {
                    TRect projectileRect = projectile.GetProjectileRect();
                    int num6 = projectileRect.mX + projectileRect.mWidth / 2;
                    int num7 = thePortal.mGridX * 80 + 55;
                    int num8 = Math.Abs(num6 - num7);
                    if (num8 <= 40)
                    {
                        GridItem otherPortal2 = GetOtherPortal(thePortal);
                        if (otherPortal2 != null)
                        {
                            int num9 = num6 - projectile.mX;
                            int num10 = otherPortal2.mGridY - thePortal.mGridY;
                            int num11 = num10 * 100;
                            projectile.mX = otherPortal2.mGridX * 80 - num9 + 90;
                            projectile.mPosX = projectile.mX;
                            projectile.mRow = otherPortal2.mGridY;
                            projectile.mY += num11;
                            projectile.mPosY = projectile.mY;
                            projectile.mShadowY += num11;
                            projectile.mLastPortalX = otherPortal2.mGridX;
                            projectile.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Projectile, projectile.mRow, 0);
                        }
                    }
                }
            }
            LawnMower lawnMower = null;
            while (mBoard.IterateLawnMowers(ref lawnMower))
            {
                if (lawnMower.mMowerState == LawnMowerState.Triggered && lawnMower.mRow == thePortal.mGridY && lawnMower.mLastPortalX != thePortal.mGridX)
                {
                    int num12 = thePortal.mGridX * 80 + 25;
                    int num13 = (int)lawnMower.mPosX + 45;
                    int num14 = Math.Abs(num13 - num12);
                    if (num14 <= 20)
                    {
                        GridItem otherPortal3 = GetOtherPortal(thePortal);
                        if (otherPortal3 != null)
                        {
                            int num15 = otherPortal3.mGridY - thePortal.mGridY;
                            int num16 = num15 * 100;
                            lawnMower.mPosX = otherPortal3.mGridX * 80 + 25;
                            lawnMower.mRow = otherPortal3.mGridY;
                            lawnMower.mPosY = num16;
                            lawnMower.mLastPortalX = otherPortal3.mGridX;
                            lawnMower.mRenderOrder = Board.MakeRenderOrder(RenderLayer.LawnMower, lawnMower.mRow, 0);
                            lawnMower.Update();
                        }
                    }
                }
            }
        }

        public float PortalCombatRowSpawnWeight(int theGridY)
        {
            int portalDistanceToMower = GetPortalDistanceToMower(theGridY);
            if (portalDistanceToMower < 5)
            {
                return 0.01f;
            }
            bool flag = false;
            int num = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num))
            {
                if (gridItem.mGridItemState != GridItemState.PortalClosed && (gridItem.mGridItemType == GridItemType.PortalCircle || gridItem.mGridItemType == GridItemType.PortalSquare) && gridItem.mGridY == theGridY)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return 1f;
            }
            return 0.2f;
        }

        public bool CanTargetZombieWithPortals(Plant thePlant, Zombie theZombie)
        {
            int num = thePlant.mPlantCol + 1;
            int num2 = thePlant.mRow;
            for (int i = 0; i < 3; i++)
            {
                GridItem portalToRight = GetPortalToRight(num, num2);
                if (num2 == theZombie.mRow)
                {
                    int num3 = num * 80;
                    int num4 = 900;
                    if (portalToRight != null)
                    {
                        num4 = portalToRight.mGridX * 80;
                    }
                    if (theZombie.mX > num3 && theZombie.mX < num4)
                    {
                        return true;
                    }
                }
                if (portalToRight == null)
                {
                    break;
                }
                GridItem otherPortal = GetOtherPortal(portalToRight);
                if (otherPortal == null)
                {
                    break;
                }
                num = otherPortal.mGridX;
                num2 = otherPortal.mGridY;
            }
            return false;
        }

        public GridItem GetPortalToRight(int theGridX, int theGridY)
        {
            GridItem gridItem = null;
            int num = -1;
            GridItem gridItem2 = null;
            while (mBoard.IterateGridItems(ref gridItem2, ref num))
            {
                if (gridItem2.mGridItemState != GridItemState.PortalClosed && (gridItem2.mGridItemType == GridItemType.PortalCircle || gridItem2.mGridItemType == GridItemType.PortalSquare) && gridItem2.mGridX - 1 >= theGridX && gridItem2.mGridY == theGridY && (gridItem == null || gridItem.mGridX >= gridItem2.mGridX))
                {
                    gridItem = gridItem2;
                }
            }
            return gridItem;
        }

        public GridItem GetPortalAt(int theGridX, int theGridY)
        {
            int num = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num))
            {
                if (gridItem.mGridX == theGridX && gridItem.mGridY == theGridY && gridItem.mGridItemState != GridItemState.PortalClosed && (gridItem.mGridItemType == GridItemType.PortalCircle || gridItem.mGridItemType == GridItemType.PortalSquare))
                {
                    return gridItem;
                }
            }
            return null;
        }

        public void MoveAPortal()
        {
            TodWeightedArray[] array = new TodWeightedArray[4];
            int num = 0;
            int num2 = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num2))
            {
                if (gridItem.mGridItemState != GridItemState.PortalClosed && (gridItem.mGridItemType == GridItemType.PortalCircle || gridItem.mGridItemType == GridItemType.PortalSquare))
                {
                    Debug.ASSERT(num < 4);
                    array[num] = TodWeightedArray.GetNewTodWeightedArray();
                    array[num].mWeight = 1;
                    array[num].mItem = gridItem;
                    num++;
                }
            }
            Debug.ASSERT(num != 0);
            GridItem gridItem2 = (GridItem)TodCommon.TodPickFromWeightedArray(array, num);
            GridItem otherPortal = GetOtherPortal(gridItem2);
            Debug.ASSERT(otherPortal != null);
            TodWeightedGridArray[] array2 = new TodWeightedGridArray[50];
            int num3 = 0;
            for (int i = 0; i < array2.Length; i++)
            {
                array2[i] = TodWeightedGridArray.GetNewTodWeightedGridArray();
            }
            for (int j = 2; j < 10; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (GetPortalAt(j, k) == null && otherPortal.mGridY != k && gridItem2.mGridY != k)
                    {
                        Debug.ASSERT(num3 < 50);
                        array2[num3].mX = j;
                        array2[num3].mY = k;
                        array2[num3].mWeight = 1;
                        num3++;
                    }
                }
            }
            TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(array2, num3);
            GridItem newGridItem = GridItem.GetNewGridItem();
            newGridItem.mGridItemType = gridItem2.mGridItemType;
            newGridItem.mGridX = todWeightedGridArray.mX;
            newGridItem.mGridY = todWeightedGridArray.mY;
            newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Particle, newGridItem.mGridY, 0);
            newGridItem.OpenPortal();
            mBoard.mGridItems.Add(newGridItem);
            gridItem2.ClosePortal();
        }

        public int GetPortalDistanceToMower(int theGridY)
        {
            int theGridY2 = theGridY;
            int num = 10;
            int i = 0;
            while (i < 40)
            {
                GridItem portalToLeft = GetPortalToLeft(num, theGridY2);
                if (portalToLeft == null)
                {
                    i += num;
                    break;
                }
                GridItem otherPortal = GetOtherPortal(portalToLeft);
                Debug.ASSERT(otherPortal != null);
                i += num - portalToLeft.mGridX;
                num = otherPortal.mGridX;
                theGridY2 = otherPortal.mGridY;
            }
            return i;
        }

        public GridItem GetPortalToLeft(int theGridX, int theGridY)
        {
            GridItem gridItem = null;
            int num = -1;
            GridItem gridItem2 = null;
            while (mBoard.IterateGridItems(ref gridItem2, ref num))
            {
                if (gridItem2.mGridItemState != GridItemState.PortalClosed && (gridItem2.mGridItemType == GridItemType.PortalCircle || gridItem2.mGridItemType == GridItemType.PortalSquare) && gridItem2.mGridX < theGridX && gridItem2.mGridY == theGridY && (gridItem == null || gridItem.mGridX <= gridItem2.mGridX))
                {
                    gridItem = gridItem2;
                }
            }
            return gridItem;
        }

        public void BeghouledPacketClicked(SeedPacket theSeedPacket)
        {
            int currentPlantCost = mBoard.GetCurrentPlantCost(theSeedPacket.mPacketType, SeedType.None);
            if (!mBoard.CanTakeSunMoney(currentPlantCost))
            {
                return;
            }
            if (theSeedPacket.mPacketType == SeedType.Repeater && !mBoard.mChallenge.mBeghouledPurcasedUpgrade[0])
            {
                mBoard.mChallenge.mBeghouledPurcasedUpgrade[0] = true;
                int count = mBoard.mPlants.Count;
                for (int i = 0; i < count; i++)
                {
                    Plant plant = mBoard.mPlants[i];
                    if (!plant.mDead && plant.mSeedType == SeedType.Peashooter)
                    {
                        plant.Die();
                        mBoard.AddPlant(plant.mPlantCol, plant.mRow, SeedType.Repeater, SeedType.None);
                    }
                }
                theSeedPacket.Deactivate();
            }
            else if (theSeedPacket.mPacketType == SeedType.Fumeshroom && !mBoard.mChallenge.mBeghouledPurcasedUpgrade[1])
            {
                mBoard.mChallenge.mBeghouledPurcasedUpgrade[1] = true;
                int count2 = mBoard.mPlants.Count;
                for (int j = 0; j < count2; j++)
                {
                    Plant plant2 = mBoard.mPlants[j];
                    if (!plant2.mDead && plant2.mSeedType == SeedType.Puffshroom)
                    {
                        plant2.Die();
                        mBoard.AddPlant(plant2.mPlantCol, plant2.mRow, SeedType.Fumeshroom, SeedType.None);
                    }
                }
                theSeedPacket.Deactivate();
            }
            else if (theSeedPacket.mPacketType == SeedType.Tallnut && !mBoard.mChallenge.mBeghouledPurcasedUpgrade[2])
            {
                mBoard.mChallenge.mBeghouledPurcasedUpgrade[2] = true;
                int count3 = mBoard.mPlants.Count;
                for (int k = 0; k < count3; k++)
                {
                    Plant plant3 = mBoard.mPlants[k];
                    if (!plant3.mDead && plant3.mSeedType == SeedType.Wallnut)
                    {
                        plant3.Die();
                        mBoard.AddPlant(plant3.mPlantCol, plant3.mRow, SeedType.Tallnut, SeedType.None);
                    }
                }
                theSeedPacket.Deactivate();
            }
            else if (theSeedPacket.mPacketType == SeedType.BeghouledButtonShuffle)
            {
                if (mChallengeState == ChallengeState.BeghouledFalling || mChallengeState == ChallengeState.BeghouledMoving)
                {
                    return;
                }
                BeghouledShuffle();
            }
            else if (theSeedPacket.mPacketType == SeedType.BeghouledButtonCrater)
            {
                if (!BeghouledCanClearCrater())
                {
                    return;
                }
                if (mChallengeState == ChallengeState.BeghouledFalling || mChallengeState == ChallengeState.BeghouledMoving)
                {
                    return;
                }
                BeghouledClearCrater(1);
                BeghouledStartFalling(ChallengeState.BeghouledFalling);
            }
            mBoard.TakeSunMoney(currentPlantCost);
        }

        public void BeghouledShuffle()
        {
            mBoard.ClearAdvice(AdviceType.None);
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead)
                {
                    plant.Die();
                }
            }
            BeghouledStartFalling(ChallengeState.BeghouledFalling);
        }

        public bool BeghouledCanClearCrater()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (mBeghouledEated[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void BeghouledUpdateCraters()
        {
            if (mBoard.mSeedBank.mNumPackets != 5)
            {
                return;
            }
            SeedPacket seedPacket = mBoard.mSeedBank.mSeedPackets[4];
            Debug.ASSERT(seedPacket.mPacketType == SeedType.BeghouledButtonCrater);
            if (BeghouledCanClearCrater())
            {
                seedPacket.Activate();
                return;
            }
            seedPacket.Deactivate();
        }

        public Zombie ZombiquariumSpawnSnorkel()
        {
            Zombie zombie = mBoard.AddZombieInRow(ZombieType.Snorkel, 0, 0);
            zombie.mPosX = TodCommon.RandRangeFloat(50f, 650f);
            zombie.mPosY = TodCommon.RandRangeFloat(100f, 400f);
            return zombie;
        }

        public void ZombiquariumPacketClicked(SeedPacket theSeedPacket)
        {
            int currentPlantCost = mBoard.GetCurrentPlantCost(theSeedPacket.mPacketType, SeedType.None);
            if (!mBoard.CanTakeSunMoney(currentPlantCost))
            {
                return;
            }
            if (theSeedPacket.mPacketType == SeedType.ZombiquariumSnorkel)
            {
                int num = mBoard.CountZombiesOnScreen();
                if (num > 100)
                {
                    return;
                }
                if (mBoard.mTutorialState == TutorialState.ZombiquariumBuySnorkel)
                {
                    mBoard.ClearAdvice(AdviceType.ZombiquariumBuySnorkel);
                    mBoard.TutorialArrowRemove();
                    mBoard.mTutorialState = TutorialState.ZombiquariumBoughtSnorkel;
                }
                Zombie zombie = ZombiquariumSpawnSnorkel();
                mApp.PlayFoley(FoleyType.Zombiesplash);
                mApp.AddTodParticle(zombie.mPosX + 60f, zombie.mPosY + 20f, 400000, ParticleEffect.PlantingPool);
            }
            else if (theSeedPacket.mPacketType == SeedType.ZombiquariumTrophy)
            {
                SpawnLevelAward(2, 0);
                mBoard.ClearAdvice(AdviceType.None);
            }
            mBoard.TakeSunMoney(currentPlantCost);
        }

        public void ZombiquariumMouseDown(int x, int y)
        {
            x = (int)(x * Constants.IS);
            y = (int)(y * Constants.IS);
            if (x < 80 || x > 720 || y < 90 || y > 430)
            {
                return;
            }
            int num = 0;
            int num2 = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num2))
            {
                if (gridItem.mGridItemType == GridItemType.Brain)
                {
                    num++;
                }
            }
            if (num >= 3)
            {
                return;
            }
            if (!mBoard.TakeSunMoney(5))
            {
                return;
            }
            ZombiquariumDropBrain(x, y);
        }

        public void ZombiquariumDropBrain(int x, int y)
        {
            mBoard.ClearAdvice(AdviceType.ZombiquariumClickToFeed);
            GridItem newGridItem = GridItem.GetNewGridItem();
            newGridItem.mGridItemType = GridItemType.Brain;
            newGridItem.mRenderOrder = 400000;
            newGridItem.mGridX = 0;
            newGridItem.mGridY = 0;
            newGridItem.mGridItemCounter = 0;
            newGridItem.mPosX = x - 15f;
            newGridItem.mPosY = y - 15f;
            mApp.PlaySample(Resources.SOUND_TAP);
            mBoard.mGridItems.Add(newGridItem);
        }

        public void ZombiquariumUpdate()
        {
            if (mApp.mBoard.mZombies.Count == 0 && !mBoard.HasLevelAwardDropped())
            {
                mBoard.ZombiesWon(null);
                return;
            }
            if (!mBoard.mAdvice.IsBeingDisplayed() && !mBoard.mHelpDisplayed[49])
            {
                string theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_ZOMBIQUARIUM_COLLECT_SUN]", "{SCORE}", 1000);
                mBoard.DisplayAdvice(theAdvice, MessageStyle.HintTallFast, AdviceType.ZombiquariumCollectSun);
            }
            int num = TodCommon.ClampInt(mBoard.mSunMoney, 0, 1000);
            mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 1000, num, 0, 150, TodCurves.Linear);
            if (num >= 900)
            {
                mBoard.DisplayAdvice("[ADVICE_ALMOST_THERE]", MessageStyle.HintTallFast, AdviceType.AlmostThere);
            }
            if (num >= 110 && mBoard.mTutorialState == TutorialState.Off)
            {
                mBoard.mTutorialState = TutorialState.ZombiquariumBuySnorkel;
                float num2 = mBoard.mSeedBank.mX + mBoard.mSeedBank.mSeedPackets[0].mX;
                float num3 = mBoard.mSeedBank.mY + mBoard.mSeedBank.mSeedPackets[0].mY;
                mBoard.TutorialArrowShow((int)num2, (int)num3);
                mBoard.DisplayAdvice("[ADVICE_ZOMBIQUARIUM_BUY_SNORKEL]", MessageStyle.HintTallFast, AdviceType.ZombiquariumBuySnorkel);
            }
            else if (num < 100 && mBoard.mTutorialState == TutorialState.ZombiquariumBuySnorkel)
            {
                mBoard.TutorialArrowRemove();
                mBoard.ClearAdvice(AdviceType.ZombiquariumBuySnorkel);
                mBoard.mTutorialState = TutorialState.Off;
            }
            if (num >= 1000 && mBoard.mTutorialState == TutorialState.ZombiquariumBoughtSnorkel)
            {
                mBoard.mTutorialState = TutorialState.ZombiquariumClickTrophy;
                float num4 = mBoard.mSeedBank.mX + mBoard.mSeedBank.mSeedPackets[1].mX;
                float num5 = mBoard.mSeedBank.mY + mBoard.mSeedBank.mSeedPackets[1].mY;
                mBoard.TutorialArrowShow((int)num4, (int)num5);
                mBoard.DisplayAdvice("[ADVICE_ZOMBIQUARIUM_CLICK_TROPHY]", MessageStyle.HintTallFast, AdviceType.ZombiquariumClickTrophy);
            }
            else if (num < 1000 && mBoard.mTutorialState == TutorialState.ZombiquariumClickTrophy)
            {
                mBoard.TutorialArrowRemove();
                mBoard.ClearAdvice(AdviceType.ZombiquariumClickTrophy);
                mBoard.mTutorialState = TutorialState.ZombiquariumBoughtSnorkel;
            }
            int num6 = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num6))
            {
                if (gridItem.mGridItemType == GridItemType.Brain)
                {
                    gridItem.mGridItemCounter++;
                    gridItem.mPosY += 0.15f;
                    if (gridItem.mPosY >= 500f)
                    {
                        gridItem.GridItemDie();
                    }
                }
            }
        }

        public void ShovelAddWallnuts()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    mBoard.AddPlant(i, j, SeedType.Wallnut, SeedType.None);
                }
            }
        }

        public void ScaryPotterPlacePot(ScaryPotType theScaryPotType, ZombieType theZombieType, SeedType theSeedType, int theCount, TodWeightedGridArray[] theGridArray, int theGridArrayCount)
        {
            Debug.ASSERT(theScaryPotType != ScaryPotType.Seed || theSeedType != SeedType.None);
            Debug.ASSERT(theScaryPotType != ScaryPotType.Zombie || theZombieType != ZombieType.Invalid);
            for (int i = 0; i < theCount; i++)
            {
                TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(theGridArray, theGridArrayCount);
                todWeightedGridArray.mWeight = 0;
                GridItem newGridItem = GridItem.GetNewGridItem();
                newGridItem.mGridItemType = GridItemType.ScaryPot;
                newGridItem.mGridItemState = GridItemState.ScaryPotQuestion;
                newGridItem.mGridX = todWeightedGridArray.mX;
                newGridItem.mGridY = todWeightedGridArray.mY;
                newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Plant, newGridItem.mGridY, 0);
                newGridItem.mSeedType = theSeedType;
                newGridItem.mZombieType = theZombieType;
                newGridItem.mScaryPotType = theScaryPotType;
                mBoard.mGridItems.Add(newGridItem);
                if (theScaryPotType == ScaryPotType.Sun)
                {
                    newGridItem.mSunCount = TodCommon.RandRangeInt(1, 3);
                }
            }
        }

        public void ScaryPotterStart()
        {
            if (mApp.IsAdventureMode())
            {
                mBoard.DisplayAdvice("[ADVICE_USE_SHOVEL_ON_POTS]", MessageStyle.HintStay, AdviceType.UseShovelOnPots);
            }
        }

        public void ScaryPotterUpdate()
        {
            if (mChallengeState == ChallengeState.ScaryPotterMalleting)
            {
                Reanimation reanimation = mApp.ReanimationGet(mReanimChallenge);
                if (reanimation.mLoopCount > 0)
                {
                    GridItem scaryPotAt = mBoard.GetScaryPotAt(mChallengeGridX, mChallengeGridY);
                    if (scaryPotAt != null)
                    {
                        ScaryPotterOpenPot(scaryPotAt);
                    }
                    mChallengeGridX = 0;
                    mChallengeGridY = 0;
                    reanimation.ReanimationDie();
                    mReanimChallenge = null;
                    mChallengeState = ChallengeState.Normal;
                }
            }
        }

        public void ScaryPotterOpenPot(GridItem theScaryPot)
        {
            int num = mBoard.GridToPixelX(theScaryPot.mGridX, theScaryPot.mGridY);
            int num2 = mBoard.GridToPixelY(theScaryPot.mGridX, theScaryPot.mGridY);
            switch (theScaryPot.mScaryPotType)
            {
            case ScaryPotType.Seed:
            {
                Coin coin = mBoard.AddCoin(num + 20, num2, CoinType.UsableSeedPacket, CoinMotion.FromPlant);
                coin.mUsableSeedType = theScaryPot.mSeedType;
                break;
            }
            case ScaryPotType.Zombie:
            {
                Zombie zombie = mBoard.AddZombieInRow(theScaryPot.mZombieType, theScaryPot.mGridY, 0);
                zombie.mPosX = num;
                break;
            }
            case ScaryPotType.Sun:
            {
                int num3 = ScaryPotterCountSunInPot(theScaryPot);
                for (int i = 0; i < num3; i++)
                {
                    mBoard.AddCoin(num, num2, CoinType.Sun, CoinMotion.FromPlant);
                    num += 15;
                }
                break;
            }
            default:
                Debug.ASSERT(false);
                break;
            }
            theScaryPot.GridItemDie();
            if (mBoard.mHelpIndex == AdviceType.UseShovelOnPots)
            {
                mBoard.DisplayAdvice("[ADVICE_DESTROY_POTS_TO_FINISH_LEVEL]", MessageStyle.HintFast, AdviceType.DestroyPotsToFinisihLevel);
            }
            if (ScaryPotterIsCompleted())
            {
                if (mApp.IsScaryPotterLevel() && !mBoard.IsFinalScaryPotterStage())
                {
                    PuzzlePhaseComplete(theScaryPot.mGridX, theScaryPot.mGridY);
                }
                else
                {
                    SpawnLevelAward(theScaryPot.mGridX, theScaryPot.mGridY);
                }
            }
            mApp.PlaySample(Resources.SOUND_BONK);
            mApp.PlayFoley(FoleyType.VaseBreaking);
            if (theScaryPot.mGridItemState == GridItemState.ScaryPotLeaf)
            {
                mApp.AddTodParticle(num + 20, num2, 200000, ParticleEffect.VaseShatterLeaf);
                return;
            }
            if (theScaryPot.mGridItemState == GridItemState.ScaryPotZombie)
            {
                mApp.AddTodParticle(num + 20, num2, 200000, ParticleEffect.VaseShatterZombie);
                return;
            }
            mApp.AddTodParticle(num + 20, num2, 200000, ParticleEffect.VaseShatter);
        }

        public void ScaryPotterJackExplode(int aPosX, int aPosY)
        {
            int num = mBoard.PixelToGridX(aPosX, aPosY);
            int num2 = mBoard.PixelToGridY(aPosX, aPosY);
            int num3 = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num3))
            {
                if (gridItem.mGridItemType == GridItemType.ScaryPot && gridItem.mGridX >= num - 1 && gridItem.mGridX <= num + 1 && gridItem.mGridY >= num2 - 1 && gridItem.mGridY <= num2 + 1)
                {
                    ScaryPotterOpenPot(gridItem);
                }
            }
        }

        public bool ScaryPotterIsCompleted()
        {
            int num = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num))
            {
                if (gridItem.mGridItemType == GridItemType.ScaryPot)
                {
                    return false;
                }
            }
            return !mBoard.AreEnemyZombiesOnScreen();
        }

        public void ScaryPotterChangePotType(GridItemState thePotType, int theCount)
        {
            int num = 0;
            for (int i = 0; i < Challenge.aPotArray.Length; i++)
            {
                Challenge.aPotArray[i].Reset();
            }
            int num2 = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num2))
            {
                if (gridItem.mGridItemState == GridItemState.ScaryPotQuestion && (thePotType != GridItemState.ScaryPotLeaf || gridItem.mScaryPotType == ScaryPotType.Seed) && (thePotType != GridItemState.ScaryPotZombie || gridItem.mZombieType == ZombieType.Gargantuar))
                {
                    Challenge.aPotArray[num].mItem = gridItem;
                    Challenge.aPotArray[num].mWeight = 1;
                    num++;
                    Debug.ASSERT(num <= 54);
                }
            }
            if (theCount > num)
            {
                theCount = num;
            }
            for (int j = 0; j < theCount; j++)
            {
                TodWeightedArray todWeightedArray = TodCommon.TodPickArrayItemFromWeightedArray(Challenge.aPotArray, num);
                todWeightedArray.mWeight = 0;
                GridItem gridItem2 = (GridItem)todWeightedArray.mItem;
                gridItem2.mGridItemState = thePotType;
            }
        }

        public void ScaryPotterPopulate()
        {
            int num = 0;
            for (int i = 0; i < Challenge.aGridArray.Length; i++)
            {
                Challenge.aGridArray[i].Reset();
            }
            for (int j = 0; j < 9; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    Challenge.aGridArray[num].mX = j;
                    Challenge.aGridArray[num].mY = k;
                    Challenge.aGridArray[num].mWeight = 1;
                    num++;
                    Debug.ASSERT(num <= 54);
                }
            }
            if ((mApp.IsAdventureMode() && mBoard.mLevel == 35) || mApp.mGameMode == GameMode.Quickplay35)
            {
                if (mSurvivalStage == 0)
                {
                    ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(4, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(5, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Peashooter, 5, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 5, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 4, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 1, Challenge.aGridArray, num);
                }
                else if (mSurvivalStage == 1)
                {
                    ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(4, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Peashooter, 4, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Snowpea, 5, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 4, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 5, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 1, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Football, SeedType.None, 1, Challenge.aGridArray, num);
                    ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
                }
                else if (mSurvivalStage == 2)
                {
                    ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
                    ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Peashooter, 5, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Snowpea, 5, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Hypnoshroom, 5, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 6, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 2, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Dancer, SeedType.None, 1, Challenge.aGridArray, num);
                    ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 1, Challenge.aGridArray, num);
                    ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 3);
                }
            }
            else if (mApp.mGameMode == GameMode.ScaryPotter1)
            {
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Peashooter, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Snowpea, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 6, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else if (mApp.mGameMode == GameMode.ScaryPotter2)
            {
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(8, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Leftpeater, 7, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Snowpea, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Wallnut, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Potatomine, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 6, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else if (mApp.mGameMode == GameMode.ScaryPotter3)
            {
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Leftpeater, 6, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Snowpea, 4, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Hypnoshroom, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Wallnut, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 8, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Dancer, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else if (mApp.mGameMode == GameMode.ScaryPotter4)
            {
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Puffshroom, 11, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Hypnoshroom, 4, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Leftpeater, 4, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 8, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 7, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Football, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else if (mApp.mGameMode == GameMode.ScaryPotter5)
            {
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Leftpeater, 6, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Pumpkinshell, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 4, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Hypnoshroom, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Snowpea, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Magnetshroom, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 6, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Football, SeedType.None, 3, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else if (mApp.mGameMode == GameMode.ScaryPotter6)
            {
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Leftpeater, 7, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Tallnut, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Threepeater, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Torchwood, 4, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 7, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Polevaulter, SeedType.None, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Football, SeedType.None, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else if (mApp.mGameMode == GameMode.ScaryPotter7)
            {
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Spikeweed, 13, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Wallnut, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 10, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else if (mApp.mGameMode == GameMode.ScaryPotter8)
            {
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Puffshroom, 7, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Tallnut, 3, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Leftpeater, 4, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 8, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 4, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pogo, SeedType.None, 4, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else if (mApp.mGameMode == GameMode.ScaryPotter9)
            {
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Leftpeater, 6, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Snowpea, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Peashooter, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Threepeater, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Potatomine, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Wallnut, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Plantern, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 8, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Gargantuar, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else if (mApp.mGameMode == GameMode.ScaryPotterEndless)
            {
                int num2 = TodCommon.ClampInt(mSurvivalStage / 10, 0, 8);
                ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
                ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Leftpeater, 6, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Snowpea, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Peashooter, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Threepeater, 2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Squash, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Potatomine, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Wallnut, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Seed, ZombieType.Invalid, SeedType.Plantern, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Sun, ZombieType.Invalid, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Normal, SeedType.None, 8 - num2, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Pail, SeedType.None, 5, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.JackInTheBox, SeedType.None, 1, Challenge.aGridArray, num);
                ScaryPotterPlacePot(ScaryPotType.Zombie, ZombieType.Gargantuar, SeedType.None, 1 + num2, Challenge.aGridArray, num);
                ScaryPotterChangePotType(GridItemState.ScaryPotLeaf, 2);
            }
            else
            {
                Debug.ASSERT(false);
            }
            mScaryPotterPots = ScaryPotterCountPots();
        }

        public void ScaryPotterDontPlaceInCol(int theCol, TodWeightedGridArray[] theGridArray, int theGridArrayCount)
        {
            for (int i = 0; i < theGridArrayCount; i++)
            {
                if (theGridArray[i].mX == theCol)
                {
                    theGridArray[i].mWeight = 0;
                }
            }
        }

        public void ScaryPotterFillColumnWithPlant(int theCol, SeedType theSeedType, TodWeightedGridArray[] theGridArray, int theGridArrayCount)
        {
            ScaryPotterDontPlaceInCol(theCol, theGridArray, theGridArrayCount);
            for (int i = 0; i < 5; i++)
            {
                Plant plant = mBoard.NewPlant(theCol, i, theSeedType, SeedType.None);
                if (theSeedType == SeedType.Potatomine)
                {
                    plant.mStateCountdown = 10;
                }
            }
        }

        public void PuzzleNextStageClear()
        {
            mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
            mBoard.mNextSurvivalStageCounter = 0;
            mBoard.mProgressMeterWidth = 0;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && zombie.IsOnBoard())
                {
                    zombie.DieNoLoot(false);
                }
            }
            count = mBoard.mPlants.Count;
            for (int j = 0; j < count; j++)
            {
                Plant plant = mBoard.mPlants[j];
                if (!plant.mDead && plant.IsOnBoard())
                {
                    plant.Die();
                }
            }
            mBoard.RefreshSeedPacketFromCursor();
            Coin coin = null;
            while (mBoard.IterateCoins(ref coin))
            {
                if (coin.mType == CoinType.UsableSeedPacket)
                {
                    coin.Die();
                }
                else
                {
                    coin.TryAutoCollectAfterLevelAward();
                }
            }
            int num = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num))
            {
                gridItem.GridItemDie();
            }
            mSurvivalStage++;
            mBoard.ClearAdviceImmediately();
            mBoard.mLevelAwardSpawned = false;
            mApp.AddTodParticle(400f, 300f, 400000, ParticleEffect.ScreenFlash);
        }

        public void ScaryPotterMalletPot(GridItem theScaryPot)
        {
            mChallengeGridX = theScaryPot.mGridX;
            mChallengeGridY = theScaryPot.mGridY;
            int num = mBoard.GridToPixelX(theScaryPot.mGridX, theScaryPot.mGridY);
            int num2 = mBoard.GridToPixelY(theScaryPot.mGridX, theScaryPot.mGridY);
            Reanimation reanimation = mApp.AddReanimation(num, num2, 400000, ReanimationType.Hammer);
            reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_open_pot, ReanimLoopType.PlayOnceAndHold, 0, 40f);
            mReanimChallenge = mApp.ReanimationGetID(reanimation);
            mChallengeState = ChallengeState.ScaryPotterMalleting;
            mApp.PlayFoley(FoleyType.Swing);
        }

        public static ZombieType IZombieSeedTypeToZombieType(SeedType theSeedType)
        {
            if (theSeedType == SeedType.ZombieNormal)
            {
                return ZombieType.Normal;
            }
            if (theSeedType == SeedType.ZombieTrafficCone)
            {
                return ZombieType.TrafficCone;
            }
            if (theSeedType == SeedType.ZombiePolevaulter)
            {
                return ZombieType.Polevaulter;
            }
            if (theSeedType == SeedType.ZombiePail)
            {
                return ZombieType.Pail;
            }
            if (theSeedType == SeedType.ZombieLadder)
            {
                return ZombieType.Ladder;
            }
            if (theSeedType == SeedType.ZombieDigger)
            {
                return ZombieType.Digger;
            }
            if (theSeedType == SeedType.ZombieBungee)
            {
                return ZombieType.Bungee;
            }
            if (theSeedType == SeedType.ZombieFootball)
            {
                return ZombieType.Football;
            }
            if (theSeedType == SeedType.ZombieBalloon)
            {
                return ZombieType.Balloon;
            }
            if (theSeedType == SeedType.ZombieScreenDoor)
            {
                return ZombieType.Door;
            }
            if (theSeedType == SeedType.Zomboni)
            {
                return ZombieType.Zamboni;
            }
            if (theSeedType == SeedType.ZombiePogo)
            {
                return ZombieType.Pogo;
            }
            if (theSeedType == SeedType.ZombieDancer)
            {
                return ZombieType.Dancer;
            }
            if (theSeedType == SeedType.ZombieGargantuar)
            {
                return ZombieType.Gargantuar;
            }
            if (theSeedType == SeedType.ZombieImp)
            {
                return ZombieType.Imp;
            }
            Debug.ASSERT(false);
            return ZombieType.Normal;
        }

        public static bool IsZombieSeedType(SeedType theSeedType)
        {
            return theSeedType == SeedType.ZombieNormal || theSeedType == SeedType.ZombieTrafficCone || theSeedType == SeedType.ZombiePolevaulter || theSeedType == SeedType.ZombiePail || theSeedType == SeedType.ZombieLadder || theSeedType == SeedType.ZombieDigger || theSeedType == SeedType.ZombieBungee || theSeedType == SeedType.ZombieFootball || theSeedType == SeedType.ZombieBalloon || theSeedType == SeedType.ZombieScreenDoor || theSeedType == SeedType.Zomboni || theSeedType == SeedType.ZombiePogo || theSeedType == SeedType.ZombieDancer || theSeedType == SeedType.ZombieGargantuar || theSeedType == SeedType.ZombieImp;
        }

        public void IZombieMouseDownWithZombie(int x, int y, int theClickCount)
        {
            if (theClickCount < 0)
            {
                mBoard.RefreshSeedPacketFromCursor();
                mApp.PlayFoley(FoleyType.Drop);
                return;
            }
            int num = mBoard.PlantingPixelToGridX((int)(x * Constants.IS), (int)(y * Constants.IS), mBoard.mCursorObject.mType);
            int num2 = mBoard.PlantingPixelToGridY((int)(x * Constants.IS), (int)(y * Constants.IS), mBoard.mCursorObject.mType);
            if (num == -1 || num2 == -1)
            {
                mBoard.RefreshSeedPacketFromCursor();
                mApp.PlayFoley(FoleyType.Drop);
                return;
            }
            PlantingReason plantingReason = CanPlantAt(num, num2, mBoard.mCursorObject.mType);
            if (plantingReason == PlantingReason.Ok)
            {
                if (!mApp.mEasyPlantingCheat)
                {
                    int currentPlantCost = mBoard.GetCurrentPlantCost(mBoard.mCursorObject.mType, mBoard.mCursorObject.mImitaterType);
                    if (!mBoard.TakeSunMoney(currentPlantCost))
                    {
                        return;
                    }
                }
                mBoard.ClearAdvice(AdviceType.IZombieLeftOfLine);
                mBoard.ClearAdvice(AdviceType.IZombieNotPassedLine);
                ZombieType theZombieType = Challenge.IZombieSeedTypeToZombieType(mBoard.mCursorObject.mType);
                IZombiePlaceZombie(theZombieType, num, num2);
                Debug.ASSERT(mBoard.mCursorObject.mSeedBankIndex >= 0 && mBoard.mCursorObject.mSeedBankIndex < mBoard.mSeedBank.mNumPackets);
                SeedPacket seedPacket = mBoard.mSeedBank.mSeedPackets[mBoard.mCursorObject.mSeedBankIndex];
                seedPacket.WasPlanted();
                mApp.PlayFoley(FoleyType.Plant);
                mBoard.ClearCursor();
                return;
            }
            mBoard.ClearAdvice(AdviceType.None);
            if (mBoard.mCursorObject.mType == SeedType.ZombieBungee)
            {
                mBoard.DisplayAdvice("[ADVICE_I_ZOMBIE_LEFT_OF_LINE]", MessageStyle.HintLong, AdviceType.IZombieLeftOfLine);
                return;
            }
            mBoard.DisplayAdvice("[ADVICE_I_ZOMBIE_NOT_PASSED_LINE]", MessageStyle.HintLong, AdviceType.IZombieNotPassedLine);
        }

        public void IZombieStart()
        {
            mBoard.DisplayAdvice("[ADVICE_I_ZOMBIE_EAT_ALL_BRAINS]", MessageStyle.HintFast, AdviceType.IZombieEatAllBrains);
        }

        public void IZombiePlacePlants(SeedType theSeedType, int theCount, int theGridY)
        {
            int num = 0;
            int num2 = 6;
            if (mApp.mGameMode == GameMode.PuzzleIZombie1 || mApp.mGameMode == GameMode.PuzzleIZombie2 || mApp.mGameMode == GameMode.PuzzleIZombie3 || mApp.mGameMode == GameMode.PuzzleIZombie4 || mApp.mGameMode == GameMode.PuzzleIZombie5)
            {
                num2 = 4;
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombie6 || mApp.mGameMode == GameMode.PuzzleIZombie7 || mApp.mGameMode == GameMode.PuzzleIZombie8 || mApp.mGameMode == GameMode.PuzzleIZombieEndless)
            {
                num2 = 5;
            }
            int num3;
            int num4;
            if (theGridY == -1)
            {
                num3 = 0;
                num4 = 4;
            }
            else
            {
                num3 = theGridY;
                num4 = theGridY;
            }
            for (int i = num3; i <= num4; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    if (mBoard.CanPlantAt(j, i, theSeedType) == PlantingReason.Ok && ((theSeedType != SeedType.Wallnut && theSeedType != SeedType.Tallnut && theSeedType != SeedType.Torchwood) || num2 - j <= 3))
                    {
                        Challenge.aGridArray[num].mX = j;
                        Challenge.aGridArray[num].mY = i;
                        Challenge.aGridArray[num].mWeight = 1;
                        num++;
                    }
                }
            }
            if (theCount > num)
            {
                theCount = num;
            }
            for (int k = 0; k < theCount; k++)
            {
                TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(Challenge.aGridArray, num);
                Plant thePlant = mBoard.NewPlant(todWeightedGridArray.mX, todWeightedGridArray.mY, theSeedType, SeedType.None);
                todWeightedGridArray.mWeight = 0;
                IZombieSetupPlant(thePlant);
            }
        }

        public void IZombieUpdate()
        {
            int num = mBoard.mSunMoney;
            Coin coin = null;
            while (mBoard.IterateCoins(ref coin))
            {
                if (coin.IsSun())
                {
                    num += coin.GetSunValue();
                }
            }
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && !zombie.IsDeadOrDying() && zombie.mZombiePhase != ZombiePhase.PolevaulterInVault && !zombie.mIsEating && zombie.mJustGotShotCounter < -500)
                {
                    zombie.PickRandomSpeed();
                }
            }
            bool flag = false;
            count = mBoard.mPlants.Count;
            for (int j = 0; j < count; j++)
            {
                Plant plant = mBoard.mPlants[j];
                if (!plant.mDead && (plant.mState == PlantState.SquashFalling || plant.mState == PlantState.SquashDoneFalling || plant.mState == PlantState.ChomperBiting || plant.mState == PlantState.ChomperBitingGotOne))
                {
                    flag = true;
                    break;
                }
            }
            int num2 = -1;
            TodParticleSystem todParticleSystem = null;
            while (mBoard.IterateParticles(ref todParticleSystem, ref num2))
            {
                if (todParticleSystem.mEffectType == ParticleEffect.PotatoMine)
                {
                    flag = true;
                    break;
                }
            }
            if (mApp.mBoard.mZombies.Count == 0 && num < 50 && !mBoard.HasLevelAwardDropped() && !flag)
            {
                Coin coin2 = null;
                while (mBoard.IterateCoins(ref coin2))
                {
                    if (coin2.IsMoney())
                    {
                        coin2.Die();
                    }
                }
                mBoard.ZombiesWon(null);
            }
        }

        public void IZombieDrawPlant(Graphics g, Plant thePlant)
        {
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            Reanimation reanimation = mApp.ReanimationTryToGet(thePlant.mBodyReanimID);
            if (reanimation == null)
            {
                return;
            }
            float num = g.mTransX;
            float num2 = g.mTransY;
            g.SetColorizeImages(true);
            g.mTransX = (int)(num + Constants.S * 4f);
            g.mTransY = (int)(num2 + Constants.S * 4f);
            g.SetColor(SexyColor.Black);
            reanimation.DrawRenderGroup(g, 0);
            g.mTransX = (int)(num + Constants.S * 2f);
            g.mTransY = (int)(num2 + Constants.S * 2f);
            g.SetColor(SexyColor.Black);
            reanimation.DrawRenderGroup(g, 0);
            g.mTransX = (int)(num - Constants.S * 2f);
            g.mTransY = (int)(num2 - Constants.S * 2f);
            g.SetColor(SexyColor.Black);
            reanimation.DrawRenderGroup(g, 0);
            g.mTransX = (int)num;
            g.mTransY = (int)num2;
            g.SetColor(new SexyColor(255, 201, 160));
            IZombieSetPlantFilterEffect(thePlant, FilterEffectType.None);
            reanimation.DrawRenderGroup(g, 0);
            IZombieSetPlantFilterEffect(thePlant, FilterEffectType.None);
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            g.SetColorizeImages(false);
        }

        public void IZombieSetPlantFilterEffect(Plant thePlant, FilterEffectType theFilterEffect)
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(thePlant.mBodyReanimID);
            Reanimation reanimation2 = mApp.ReanimationTryToGet(thePlant.mHeadReanimID);
            Reanimation reanimation3 = mApp.ReanimationTryToGet(thePlant.mHeadReanimID2);
            Reanimation reanimation4 = mApp.ReanimationTryToGet(thePlant.mHeadReanimID3);
            if (reanimation != null)
            {
                reanimation.mFilterEffect = theFilterEffect;
            }
            if (reanimation2 != null)
            {
                reanimation2.mFilterEffect = theFilterEffect;
            }
            if (reanimation3 != null)
            {
                reanimation3.mFilterEffect = theFilterEffect;
            }
            if (reanimation4 != null)
            {
                reanimation4.mFilterEffect = theFilterEffect;
            }
        }

        public int ScaryPotterCountSunInPot(GridItem theScaryPot)
        {
            return theScaryPot.mSunCount;
        }

        public int ScaryPotterCountPots()
        {
            int num = 0;
            int num2 = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num2))
            {
                if (gridItem.mGridItemType == GridItemType.ScaryPot)
                {
                    num++;
                }
            }
            return num;
        }

        public void IZombieInitLevel()
        {
            mChallengeScore = 0;
            for (int i = 0; i < 5; i++)
            {
                GridItem newGridItem = GridItem.GetNewGridItem();
                newGridItem.mGridItemType = GridItemType.IzombieBrain;
                newGridItem.mGridX = 0;
                newGridItem.mGridY = i;
                newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.Plant, newGridItem.mGridY, 0);
                newGridItem.mGridItemCounter = 70;
                newGridItem.mPosX = mBoard.GridToPixelX(newGridItem.mGridX, newGridItem.mGridY) - 40f;
                newGridItem.mPosY = mBoard.GridToPixelY(newGridItem.mGridX, newGridItem.mGridY) + 40f;
                mBoard.mGridItems.Add(newGridItem);
            }
            if (mApp.mGameMode == GameMode.PuzzleIZombie1)
            {
                IZombiePlacePlantInSquare(SeedType.Sunflower, 3, 2);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 3, 3);
                IZombiePlacePlants(SeedType.Sunflower, 7, -1);
                IZombiePlacePlants(SeedType.Squash, 3, -1);
                IZombiePlacePlants(SeedType.Peashooter, 6, -1);
                IZombiePlacePlants(SeedType.Snowpea, 2, -1);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie2)
            {
                IZombiePlacePlantInSquare(SeedType.Spikeweed, 3, 0);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 2, 0);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 3, 3);
                IZombiePlacePlants(SeedType.Spikeweed, 1, 0);
                IZombiePlacePlants(SeedType.Peashooter, 1, 0);
                IZombiePlacePlants(SeedType.Snowpea, 2, 3);
                IZombiePlacePlants(SeedType.Sunflower, 1, 3);
                IZombiePlacePlants(SeedType.Sunflower, 4, -1);
                IZombiePlacePlants(SeedType.Spikeweed, 2, -1);
                IZombiePlacePlants(SeedType.Snowpea, 2, -1);
                IZombiePlacePlants(SeedType.Peashooter, 4, -1);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie3)
            {
                IZombiePlacePlantInSquare(SeedType.Potatomine, 3, 0);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 2, 0);
                IZombiePlacePlantInSquare(SeedType.Potatomine, 2, 2);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 2, 4);
                IZombiePlacePlantInSquare(SeedType.Torchwood, 3, 3);
                IZombiePlacePlants(SeedType.Torchwood, 2, -1);
                IZombiePlacePlants(SeedType.Sunflower, 5, -1);
                IZombiePlacePlants(SeedType.Peashooter, 7, -1);
                IZombiePlacePlants(SeedType.Splitpea, 1, -1);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie4)
            {
                IZombiePlacePlantInSquare(SeedType.Wallnut, 3, 0);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 2, 0);
                IZombiePlacePlantInSquare(SeedType.Wallnut, 3, 1);
                IZombiePlacePlantInSquare(SeedType.Wallnut, 3, 2);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 2, 2);
                IZombiePlacePlantInSquare(SeedType.Wallnut, 3, 3);
                IZombiePlacePlantInSquare(SeedType.Wallnut, 3, 4);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 2, 4);
                IZombiePlacePlants(SeedType.Peashooter, 1, 0);
                IZombiePlacePlants(SeedType.Snowpea, 1, 1);
                IZombiePlacePlants(SeedType.Fumeshroom, 2, 2);
                IZombiePlacePlants(SeedType.Snowpea, 1, 3);
                IZombiePlacePlants(SeedType.Peashooter, 1, 4);
                IZombiePlacePlants(SeedType.Peashooter, 2, -1);
                IZombiePlacePlants(SeedType.Sunflower, 4, -1);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie5)
            {
                IZombiePlacePlantInSquare(SeedType.Sunflower, 3, 2);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 3, 3);
                IZombiePlacePlants(SeedType.Cactus, 1, 1);
                IZombiePlacePlants(SeedType.Cactus, 1, 4);
                IZombiePlacePlants(SeedType.Magnetshroom, 1, -1);
                IZombiePlacePlants(SeedType.Sunflower, 5, -1);
                IZombiePlacePlants(SeedType.Peashooter, 8, -1);
                IZombiePlacePlants(SeedType.Snowpea, 2, -1);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie6)
            {
                IZombiePlacePlantInSquare(SeedType.Garlic, 4, 1);
                IZombiePlacePlantInSquare(SeedType.Garlic, 4, 3);
                IZombiePlacePlants(SeedType.Sunflower, 3, 1);
                IZombiePlacePlants(SeedType.Sunflower, 3, 3);
                IZombiePlacePlants(SeedType.Torchwood, 2, -1);
                IZombiePlacePlants(SeedType.Sunflower, 2, -1);
                IZombiePlacePlants(SeedType.Spikeweed, 3, -1);
                IZombiePlacePlants(SeedType.Snowpea, 1, -1);
                IZombiePlacePlants(SeedType.Peashooter, 5, -1);
                IZombiePlacePlants(SeedType.Squash, 2, -1);
                IZombiePlacePlants(SeedType.Kernelpult, 2, -1);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie7)
            {
                IZombiePlacePlantInSquare(SeedType.Sunflower, 4, 2);
                IZombiePlacePlantInSquare(SeedType.Sunflower, 4, 4);
                IZombiePlacePlants(SeedType.Sunflower, 6, -1);
                IZombiePlacePlants(SeedType.Potatomine, 9, -1);
                IZombiePlacePlants(SeedType.Chomper, 8, -1);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie8)
            {
                IZombiePlacePlants(SeedType.Wallnut, 3, -1);
                IZombiePlacePlants(SeedType.Magnetshroom, 2, -1);
                IZombiePlacePlants(SeedType.Peashooter, 8, -1);
                IZombiePlacePlants(SeedType.Squash, 2, -1);
                IZombiePlacePlants(SeedType.Potatomine, 2, -1);
                IZombiePlacePlants(SeedType.Sunflower, 8, -1);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombie9)
            {
                IZombiePlacePlantInSquare(SeedType.Tallnut, 5, 1);
                IZombiePlacePlantInSquare(SeedType.Torchwood, 5, 3);
                IZombiePlacePlants(SeedType.Potatomine, 4, 0);
                IZombiePlacePlants(SeedType.Sunflower, 2, 0);
                IZombiePlacePlants(SeedType.Sunflower, 2, 1);
                IZombiePlacePlants(SeedType.Threepeater, 1, 1);
                IZombiePlacePlants(SeedType.Snowpea, 1, 1);
                IZombiePlacePlants(SeedType.Splitpea, 1, 1);
                IZombiePlacePlants(SeedType.Chomper, 3, 2);
                IZombiePlacePlants(SeedType.Sunflower, 2, 2);
                IZombiePlacePlants(SeedType.Squash, 1, 2);
                IZombiePlacePlants(SeedType.Peashooter, 3, 3);
                IZombiePlacePlants(SeedType.Sunflower, 2, 3);
                IZombiePlacePlants(SeedType.Sunflower, 1, 4);
                IZombiePlacePlants(SeedType.Fumeshroom, 1, 4);
                IZombiePlacePlants(SeedType.Scaredyshroom, 1, 4);
                IZombiePlacePlants(SeedType.Starfruit, 1, 4);
                IZombiePlacePlants(SeedType.Splitpea, 1, 4);
                IZombiePlacePlants(SeedType.Magnetshroom, 1, 4);
            }
            else if (mApp.mGameMode == GameMode.PuzzleIZombieEndless)
            {
                int num = TodCommon.RandRangeInt(0, 4);
                int theMax = TodCommon.ClampInt(3 + mSurvivalStage / 2, 2, 6);
                int theMin = TodCommon.ClampInt(2 + mSurvivalStage / 3, 2, 4);
                int num2 = TodCommon.RandRangeInt(theMin, theMax);
                if (mSurvivalStage == 0)
                {
                    num2 = 0;
                }
                else if (mSurvivalStage == 1)
                {
                    num2 = 1;
                }
                else if (mSurvivalStage >= 10)
                {
                    theMax = TodCommon.ClampInt(3 + mSurvivalStage / 2, 2, 7);
                    theMin = TodCommon.ClampInt(2 + mSurvivalStage / 3, 2, 5);
                    num2 = TodCommon.RandRangeInt(theMin, theMax);
                }
                int theCount = 8 - num2;
                IZombiePlacePlants(SeedType.Sunflower, theCount, -1);
                IZombiePlacePlants(SeedType.Puffshroom, num2, -1);
                if (num == 0 && mSurvivalStage >= 1)
                {
                    int num3 = TodCommon.RandRangeInt(0, 4);
                    if (num3 == 0)
                    {
                        IZombiePlacePlants(SeedType.Snowpea, 9, -1);
                        IZombiePlacePlants(SeedType.Splitpea, 4, -1);
                        IZombiePlacePlants(SeedType.Repeater, 4, -1);
                    }
                    else if (num3 == 1)
                    {
                        IZombiePlacePlants(SeedType.Potatomine, 9, -1);
                        IZombiePlacePlants(SeedType.Chomper, 8, -1);
                    }
                    else if (num3 == 2)
                    {
                        IZombiePlacePlants(SeedType.Spikeweed, 9, -1);
                        IZombiePlacePlants(SeedType.Starfruit, 8, -1);
                    }
                    else if (num3 == 3)
                    {
                        IZombiePlacePlants(SeedType.Fumeshroom, 9, -1);
                        IZombiePlacePlants(SeedType.Magnetshroom, 8, -1);
                    }
                    else
                    {
                        IZombiePlacePlants(SeedType.Scaredyshroom, 12, -1);
                        IZombiePlacePlants(SeedType.Sunflower, 5, -1);
                    }
                }
                else
                {
                    int num3 = TodCommon.RandRangeInt(0, 5);
                    if (num3 == 0 || num3 == 1 || num3 == 2)
                    {
                        IZombiePlacePlants(SeedType.Wallnut, 1, -1);
                        IZombiePlacePlants(SeedType.Torchwood, 1, -1);
                        IZombiePlacePlants(SeedType.Potatomine, 1, -1);
                        IZombiePlacePlants(SeedType.Chomper, 2, -1);
                        IZombiePlacePlants(SeedType.Peashooter, 1, -1);
                        IZombiePlacePlants(SeedType.Splitpea, 1, -1);
                        IZombiePlacePlants(SeedType.Kernelpult, 1, -1);
                        IZombiePlacePlants(SeedType.Threepeater, 1, -1);
                        IZombiePlacePlants(SeedType.Snowpea, 1, -1);
                        IZombiePlacePlants(SeedType.Squash, 1, -1);
                        IZombiePlacePlants(SeedType.Fumeshroom, 1, -1);
                        IZombiePlacePlants(SeedType.Umbrella, 1, -1);
                        IZombiePlacePlants(SeedType.Starfruit, 1, -1);
                        IZombiePlacePlants(SeedType.Magnetshroom, 1, -1);
                        IZombiePlacePlants(SeedType.Spikeweed, 2, -1);
                    }
                    else if (num3 == 3 || num3 == 4)
                    {
                        IZombiePlacePlants(SeedType.Torchwood, 1, -1);
                        IZombiePlacePlants(SeedType.Splitpea, 3, -1);
                        IZombiePlacePlants(SeedType.Repeater, 1, -1);
                        IZombiePlacePlants(SeedType.Kernelpult, 3, -1);
                        IZombiePlacePlants(SeedType.Threepeater, 1, -1);
                        IZombiePlacePlants(SeedType.Snowpea, 3, -1);
                        IZombiePlacePlants(SeedType.Umbrella, 1, -1);
                        IZombiePlacePlants(SeedType.Magnetshroom, 1, -1);
                        IZombiePlacePlants(SeedType.Spikeweed, 3, -1);
                    }
                    else
                    {
                        IZombiePlacePlants(SeedType.Potatomine, 4, -1);
                        IZombiePlacePlants(SeedType.Chomper, 3, -1);
                        IZombiePlacePlants(SeedType.Squash, 3, -1);
                        IZombiePlacePlants(SeedType.Fumeshroom, 4, -1);
                        IZombiePlacePlants(SeedType.Spikeweed, 3, -1);
                    }
                }
            }
            mBoard.mBonusLawnMowersRemaining = 0;
        }

        public void DrawRain(Graphics g)
        {
            if (mBoard.mCutScene != null && mBoard.mCutScene.IsBeforePreloading())
            {
                return;
            }
            if (!mApp.Is3DAccelerated())
            {
                return;
            }
            int num;
            if (mBoard.mX > 0)
            {
                num = (mBoard.mX + 100) / 100 * -100;
            }
            else
            {
                num = mBoard.mX / 100 * -100;
            }
            int num2 = TodCommon.TodAnimateCurve(0, 100, mBoard.mEffectCounter % 100, 0, -100, TodCurves.Linear);
            int num3 = TodCommon.TodAnimateCurve(0, 20, mBoard.mEffectCounter % 20, -100, 0, TodCurves.Linear);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int theX = num2 + i * 100 + num;
                    int theY = num3 + j * 100;
                    g.DrawImage(AtlasResources.IMAGE_RAIN, theX, theY);
                }
            }
            int num4 = TodCommon.TodAnimateCurve(0, 161, mBoard.mEffectCounter % 161, 0, -100, TodCurves.Linear);
            int num5 = TodCommon.TodAnimateCurve(0, 33, mBoard.mEffectCounter % 33, -100, 0, TodCurves.Linear);
            for (int k = 0; k < 5; k++)
            {
                for (int l = 0; l < 4; l++)
                {
                    float num6 = 1.7f;
                    float thePosX = (num4 + k * 100f) * num6 + num;
                    float thePosY = (num5 + l * 100f) * num6;
                    TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_RAIN, thePosX, thePosY, num6, num6);
                }
            }
        }

        public void DrawWeather(Graphics g)
        {
            if (mApp.IsStormyNightLevel() || mApp.mGameMode == GameMode.ChallengeRainingSeeds)
            {
                DrawRain(g);
            }
            if (mApp.IsStormyNightLevel())
            {
                DrawStormNight(g);
            }
        }

        public void SquirrelUpdate()
        {
            int num = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num))
            {
                if (gridItem.mGridItemType == GridItemType.Squirrel)
                {
                    SquirrelUpdateOne(gridItem);
                }
            }
            mChallengeScore = 7 - SquirrelCountUncaught();
            mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 7, mChallengeScore, 0, 150, TodCurves.Linear);
        }

        public int SquirrelCountUncaught()
        {
            int num = 0;
            int num2 = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num2))
            {
                if (gridItem.mGridItemType == GridItemType.Squirrel && gridItem.mGridItemState != GridItemState.SquirrelCaught && gridItem.mGridItemState != GridItemState.SquirrelZombie)
                {
                    num++;
                }
            }
            return num;
        }

        public void SquirrelStart()
        {
            TodWeightedGridArray[] array = new TodWeightedGridArray[Constants.GRIDSIZEX * Constants.MAX_GRIDSIZEY];
            int num = 0;
            for (int i = 0; i < Constants.GRIDSIZEX; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    array[num] = TodWeightedGridArray.GetNewTodWeightedGridArray();
                    array[num].mX = i;
                    array[num].mY = j;
                    array[num].mWeight = 1;
                    num++;
                }
            }
            TodWeightedGridArray todWeightedGridArray;
            GridItem newGridItem;
            for (int k = 0; k < 7; k++)
            {
                todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(array, num);
                todWeightedGridArray.mWeight = 0;
                newGridItem = GridItem.GetNewGridItem();
                newGridItem.mGridItemType = GridItemType.Squirrel;
                newGridItem.mGridX = todWeightedGridArray.mX;
                newGridItem.mGridY = todWeightedGridArray.mY;
                newGridItem.mGridItemState = GridItemState.SquirrelWaiting;
                newGridItem.mGridItemCounter = TodCommon.RandRangeInt(100, 500);
                newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, newGridItem.mGridY, 1);
                mBoard.mGridItems.Add(newGridItem);
            }
            for (int l = 0; l < num; l++)
            {
                if (array[l].mX < 4)
                {
                    array[l].mWeight = 0;
                }
            }
            todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(array, num);
            newGridItem = GridItem.GetNewGridItem();
            newGridItem.mGridItemType = GridItemType.Squirrel;
            newGridItem.mGridX = todWeightedGridArray.mX;
            newGridItem.mGridY = todWeightedGridArray.mY;
            newGridItem.mGridItemState = GridItemState.SquirrelZombie;
            newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, newGridItem.mGridY, 1);
            mBoard.mGridItems.Add(newGridItem);
        }

        public void SquirrelFound(GridItem theSquirrel)
        {
            if (theSquirrel.mGridItemState == GridItemState.SquirrelZombie)
            {
                Zombie zombie = mBoard.AddZombieInRow(ZombieType.Normal, theSquirrel.mGridY, 0);
                zombie.mPosX = mBoard.GridToPixelX(theSquirrel.mGridX, theSquirrel.mGridY);
                theSquirrel.GridItemDie();
                mBoard.DisplayAdvice("[ADVICE_SQUIRREL_ZOMBIE]", MessageStyle.HintFast, AdviceType.None);
                return;
            }
            TodWeightedGridArray[] array = new TodWeightedGridArray[4];
            int num = 0;
            for (int i = 0; i < 4; i++)
            {
                int num2 = theSquirrel.mGridX;
                int num3 = theSquirrel.mGridY;
                switch (i)
                {
                case 0:
                    num2--;
                    break;
                case 1:
                    num2++;
                    break;
                case 2:
                    num3--;
                    break;
                case 3:
                    num3++;
                    break;
                }
                if (mBoard.GetSquirrelAt(num2, num3) == null)
                {
                    Plant topPlantAt = mBoard.GetTopPlantAt(num2, num3, TopPlant.EatingOrder);
                    if (topPlantAt != null)
                    {
                        array[num] = TodWeightedGridArray.GetNewTodWeightedGridArray();
                        array[num].mX = num2;
                        array[num].mY = num3;
                        array[num].mWeight = 1;
                        num++;
                    }
                }
            }
            if (num > 0)
            {
                TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(array, num);
                if (todWeightedGridArray.mX < theSquirrel.mGridX)
                {
                    theSquirrel.mGridItemState = GridItemState.SquirrelRunningLeft;
                }
                else if (todWeightedGridArray.mX > theSquirrel.mGridX)
                {
                    theSquirrel.mGridItemState = GridItemState.SquirrelRunningRight;
                }
                else if (todWeightedGridArray.mY < theSquirrel.mGridY)
                {
                    theSquirrel.mGridItemState = GridItemState.SquirrelRunningUp;
                }
                else
                {
                    theSquirrel.mGridItemState = GridItemState.SquirrelRunningDown;
                }
                theSquirrel.mGridItemCounter = 50;
                theSquirrel.mGridX = todWeightedGridArray.mX;
                theSquirrel.mGridY = todWeightedGridArray.mY;
                theSquirrel.mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, theSquirrel.mGridY, 1);
                return;
            }
            theSquirrel.mGridItemState = GridItemState.SquirrelCaught;
            theSquirrel.mGridItemCounter = 100;
            int num4 = SquirrelCountUncaught();
            if (num4 == 0)
            {
                mBoard.ClearAdvice(AdviceType.None);
                SpawnLevelAward(theSquirrel.mGridX, theSquirrel.mGridY);
                return;
            }
            string theAdvice = mApp.Pluralize(num4, "[ADVICE_SQUIRRELS_ONE_LEFT]", "[ADVICE_SQUIRRELS_LEFT]");
            mBoard.DisplayAdvice(theAdvice, MessageStyle.HintFast, AdviceType.None);
        }

        public void SquirrelPeek(GridItem theSquirrel)
        {
            theSquirrel.mGridItemCounter = 50;
            theSquirrel.mGridItemState = GridItemState.SquirrelPeeking;
        }

        public void SquirrelChew(GridItem theSquirrel)
        {
            theSquirrel.mGridItemCounter = TodCommon.RandRangeInt(100, 500);
            Plant topPlantAt = mBoard.GetTopPlantAt(theSquirrel.mGridX, theSquirrel.mGridY, TopPlant.EatingOrder);
            if (topPlantAt == null)
            {
                return;
            }
            float num = mBoard.GridToPixelX(theSquirrel.mGridX, theSquirrel.mGridY);
            float num2 = mBoard.GridToPixelY(theSquirrel.mGridX, theSquirrel.mGridY);
            mApp.AddTodParticle(num + 40f, num2 + 40f, topPlantAt.mRenderOrder + 1, ParticleEffect.WallnutEatSmall);
            topPlantAt.mBeghouledFlashCountdown = Math.Max(topPlantAt.mBeghouledFlashCountdown, 25);
        }

        public void SquirrelUpdateOne(GridItem theSquirrel)
        {
            if (theSquirrel.mGridItemCounter > 0)
            {
                theSquirrel.mGridItemCounter--;
            }
            if (theSquirrel.mGridItemState == GridItemState.SquirrelWaiting || theSquirrel.mGridItemState == GridItemState.SquirrelZombie)
            {
                if (mBoard.GetTopPlantAt(theSquirrel.mGridX, theSquirrel.mGridY, TopPlant.EatingOrder) == null)
                {
                    SquirrelFound(theSquirrel);
                }
                if (theSquirrel.mGridItemCounter == 0)
                {
                    int num = TodCommon.RandRangeInt(0, 1);
                    if (num == 0 || theSquirrel.mGridItemState == GridItemState.SquirrelZombie)
                    {
                        SquirrelChew(theSquirrel);
                    }
                    else
                    {
                        SquirrelPeek(theSquirrel);
                    }
                }
            }
            if ((theSquirrel.mGridItemState == GridItemState.SquirrelPeeking || theSquirrel.mGridItemState == GridItemState.SquirrelRunningUp || theSquirrel.mGridItemState == GridItemState.SquirrelRunningDown || theSquirrel.mGridItemState == GridItemState.SquirrelRunningLeft || theSquirrel.mGridItemState == GridItemState.SquirrelRunningRight) && theSquirrel.mGridItemCounter == 0)
            {
                theSquirrel.mGridItemState = GridItemState.SquirrelWaiting;
                theSquirrel.mGridItemCounter = TodCommon.RandRangeInt(100, 500);
            }
            if (theSquirrel.mGridItemState == GridItemState.SquirrelCaught && theSquirrel.mGridItemCounter == 0)
            {
                theSquirrel.GridItemDie();
            }
        }

        public void IZombieSetupPlant(Plant thePlant)
        {
            Reanimation reanimation = mApp.ReanimationTryToGet(thePlant.mBodyReanimID);
            Reanimation reanimation2 = mApp.ReanimationTryToGet(thePlant.mHeadReanimID);
            Reanimation reanimation3 = mApp.ReanimationTryToGet(thePlant.mHeadReanimID2);
            Reanimation reanimation4 = mApp.ReanimationTryToGet(thePlant.mHeadReanimID3);
            if (reanimation != null)
            {
                reanimation.mAnimRate = 0f;
            }
            if (reanimation2 != null)
            {
                reanimation2.mAnimRate = 0f;
            }
            if (reanimation3 != null)
            {
                reanimation3.mAnimRate = 0f;
            }
            if (reanimation4 != null)
            {
                reanimation4.mAnimRate = 0f;
            }
            if (thePlant.mSeedType == SeedType.Potatomine)
            {
                thePlant.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_armed, ReanimLoopType.Loop, 0, 0f);
                thePlant.mState = PlantState.PotatoArmed;
            }
            thePlant.mBlinkCountdown = 0;
            thePlant.UpdateReanim();
        }

        public void UpdateRain()
        {
            mRainCounter--;
            if (mRainCounter < 0 && !mBoard.mCutScene.IsBeforePreloading())
            {
                ReanimationType theReanimationType = ReanimationType.RainSplash;
                float theX = TodCommon.RandRangeFloat(40f, 740f);
                float theY = TodCommon.RandRangeFloat(90f, 240f);
                Reanimation reanimation = mApp.AddReanimation(theX, theY, 200000, theReanimationType);
                int theAlpha = TodCommon.RandRangeInt(100, 200);
                float num = TodCommon.RandRangeFloat(0.7f, 1.2f);
                reanimation.mColorOverride = new SexyColor(255, 255, 255, theAlpha);
                reanimation.OverrideScale(num, num);
                theReanimationType = ReanimationType.RainCircle;
                theX = TodCommon.RandRangeFloat(40f, 740f);
                theY = TodCommon.RandRangeFloat(290f, 410f);
                reanimation = mApp.AddReanimation(theX, theY, 200000, theReanimationType);
                theAlpha = TodCommon.RandRangeInt(50, 150);
                num = TodCommon.RandRangeFloat(0.7f, 1.1f);
                reanimation.mColorOverride = new SexyColor(255, 255, 255, theAlpha);
                reanimation.OverrideScale(num, num);
                theReanimationType = ReanimationType.RainSplash;
                theX = TodCommon.RandRangeFloat(40f, 740f);
                theY = TodCommon.RandRangeFloat(450f, 580f);
                reanimation = mApp.AddReanimation(theX, theY, 200000, theReanimationType);
                theAlpha = TodCommon.RandRangeInt(100, 200);
                num = TodCommon.RandRangeFloat(0.7f, 1.2f);
                reanimation.mColorOverride = new SexyColor(255, 255, 255, theAlpha);
                reanimation.OverrideScale(num, num);
                mRainCounter = TodCommon.RandRangeInt(10, 20);
            }
        }

        public bool IZombieEatBrain(Zombie theZombie)
        {
            GridItem gridItem = IZombieGetBrainTarget(theZombie);
            if (gridItem == null)
            {
                return false;
            }
            theZombie.StartEating();
            //gridItem.mGridItemCounter -= 3;
            gridItem.mGridItemCounter--;
            if (gridItem.mGridItemCounter <= 0)
            {
                mApp.PlaySample(Resources.SOUND_GULP);
                gridItem.GridItemDie();
                IZombieScoreBrain(gridItem);
            }
            return true;
        }

        public GridItem IZombieGetBrainTarget(Zombie theZombie)
        {
            if (theZombie.mZombieType == ZombieType.Digger && theZombie.mZombiePhase == ZombiePhase.DiggerWalking)
            {
                return null;
            }
            if (theZombie.mZombieType == ZombieType.Bungee)
            {
                return null;
            }
            if (theZombie.IsWalkingBackwards())
            {
                return null;
            }
            TRect zombieAttackRect = theZombie.GetZombieAttackRect();
            if (theZombie.mZombiePhase == ZombiePhase.PolevaulterPreVault)
            {
                zombieAttackRect = new TRect(50 + theZombie.mX, 0, 20, 115);
            }
            if (theZombie.mZombieType == ZombieType.Balloon)
            {
                zombieAttackRect.mX += 25;
            }
            if (zombieAttackRect.mX > Constants.IZombieBrainPosition)
            {
                return null;
            }
            GridItem gridItemAt = mBoard.GetGridItemAt(GridItemType.IzombieBrain, 0, theZombie.mRow);
            if (gridItemAt == null)
            {
                return null;
            }
            if (gridItemAt.mGridItemState == GridItemState.BrainSquished)
            {
                return null;
            }
            return gridItemAt;
        }

        public void IZombiePlacePlantInSquare(SeedType theSeedType, int theGridX, int theGridY)
        {
            if (mBoard.CanPlantAt(theGridX, theGridY, theSeedType) != PlantingReason.Ok)
            {
                return;
            }
            Plant thePlant = mBoard.NewPlant(theGridX, theGridY, theSeedType, SeedType.None);
            IZombieSetupPlant(thePlant);
        }

        public void AdvanceCrazyDaveDialog()
        {
            if (!mBoard.IsScaryPotterDaveTalking() || mApp.mCrazyDaveMessageIndex == -1)
            {
                return;
            }
            if (!mApp.AdvanceCrazyDaveText())
            {
                mApp.CrazyDaveLeave();
                return;
            }
            if (mApp.mCrazyDaveMessageIndex == 2702 || mApp.mCrazyDaveMessageIndex == 2801)
            {
                ScaryPotterPopulate();
                mApp.PlayFoley(FoleyType.Plant);
                mBoard.PlaceRake();
            }
        }

        public void BeghouledFlashPlant(int x, int y, int theSwap1X, int theSwap1Y, int theSwap2X, int theSwap2Y)
        {
            int num = x;
            int num2 = y;
            if (num == theSwap1X && num2 == theSwap1Y)
            {
                num = theSwap2X;
                num2 = theSwap2Y;
            }
            else if (num == theSwap2X && num2 == theSwap2Y)
            {
                num = theSwap1X;
                num2 = theSwap1Y;
            }
            Plant topPlantAt = mBoard.GetTopPlantAt(num, num2, TopPlant.OnlyNormalPosition);
            if (topPlantAt != null)
            {
                topPlantAt.mBeghouledFlashCountdown = Math.Max(topPlantAt.mBeghouledFlashCountdown, 300);
            }
        }

        public void BeghouledFlashAMatch()
        {
            BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
            LoadBeghouledBoardState(newBeghouledBoardState);
            if (mApp.mGameMode == GameMode.ChallengeBeghouled)
            {
                for (int i = 0; i <= 4; i++)
                {
                    for (int j = 0; j <= 7; j++)
                    {
                        if (j < 7 && BeghouledFlashFromBoardState(newBeghouledBoardState, j, i, j + 1, i))
                        {
                            newBeghouledBoardState.PrepareForReuse();
                            return;
                        }
                        if (i < 4 && BeghouledFlashFromBoardState(newBeghouledBoardState, j, i, j, i + 1))
                        {
                            newBeghouledBoardState.PrepareForReuse();
                            return;
                        }
                    }
                }
            }
            else if (mApp.mGameMode == GameMode.ChallengeBeghouledTwist)
            {
                for (int k = 0; k < 4; k++)
                {
                    for (int l = 0; l < 7; l++)
                    {
                        if (BeghouledTwistFlashMatch(newBeghouledBoardState, l, k))
                        {
                            newBeghouledBoardState.PrepareForReuse();
                            return;
                        }
                    }
                }
            }
            newBeghouledBoardState.PrepareForReuse();
        }

        public bool BeghouledFlashFromBoardState(BeghouledBoardState theBoardState, int theSwap1X, int theSwap1Y, int theSwap2X, int theSwap2Y)
        {
            Debug.ASSERT(theSwap1X >= 0 && theSwap1X < 8 && theSwap1Y >= 0 && theSwap1Y < 5);
            Debug.ASSERT(theSwap2X >= 0 && theSwap2X < 8 && theSwap2Y >= 0 && theSwap2Y < 5);
            if (mBeghouledEated[theSwap1X, theSwap1Y] || mBeghouledEated[theSwap2X, theSwap2Y])
            {
                return false;
            }
            SeedType seedType = theBoardState.mSeedType[theSwap1X, theSwap1Y];
            SeedType seedType2 = theBoardState.mSeedType[theSwap2X, theSwap2Y];
            theBoardState.mSeedType[theSwap1X, theSwap1Y] = seedType2;
            theBoardState.mSeedType[theSwap2X, theSwap2Y] = seedType;
            bool flag = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int num = BeghouledHorizontalMatchLength(j, i, theBoardState);
                    if (num >= 3)
                    {
                        BeghouledFlashPlant(j, i, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
                        BeghouledFlashPlant(j + 1, i, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
                        BeghouledFlashPlant(j + 2, i, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
                        flag = true;
                        break;
                    }
                    int num2 = BeghouledVerticalMatchLength(j, i, theBoardState);
                    if (num2 >= 3)
                    {
                        BeghouledFlashPlant(j, i, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
                        BeghouledFlashPlant(j, i + 1, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
                        BeghouledFlashPlant(j, i + 2, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            theBoardState.mSeedType[theSwap1X, theSwap1Y] = seedType;
            theBoardState.mSeedType[theSwap2X, theSwap2Y] = seedType2;
            return flag;
        }

        public void IZombiePlantDropRemainingSun(Plant thePlant)
        {
            if (thePlant.mSeedType != SeedType.Sunflower)
            {
                return;
            }
            int num = thePlant.mPlantHealth / 40 + 1;
            for (int i = 0; i < num; i++)
            {
                mBoard.AddCoin(thePlant.mX + 5 * i, thePlant.mY, CoinType.Sun, CoinMotion.FromPlant);
            }
        }

        public void IZombieSquishBrain(GridItem theBrain)
        {
            theBrain.mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, theBrain.mGridY, 0);
            theBrain.mGridItemState = GridItemState.BrainSquished;
            theBrain.mGridItemCounter = 500;
            theBrain.mApp.PlayFoley(FoleyType.Squish);
            IZombieScoreBrain(theBrain);
        }

        public void IZombieScoreBrain(GridItem theBrain)
        {
            mBoard.mChallenge.mChallengeScore++;
            mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 5, mBoard.mChallenge.mChallengeScore, 0, 150, TodCurves.Linear);
            if (mBoard.mChallenge.mChallengeScore == 5)
            {
                if (mApp.IsEndlessIZombie(mApp.mGameMode))
                {
                    PuzzlePhaseComplete(0, theBrain.mGridY);
                }
                else
                {
                    mBoard.mChallenge.SpawnLevelAward(0, theBrain.mGridY);
                }
            }
            if (mBoard.mChallenge.mChallengeScore != 5 || !PuzzleIsAwardStage())
            {
                mBoard.DropLootPiece((int)theBrain.mPosX + 40, (int)theBrain.mPosY - 50, 12);
            }
        }

        public void LastStandUpate()
        {
            if (mBoard.mNextSurvivalStageCounter == 0 && mChallengeState == ChallengeState.Normal && mBoard.mStoreButton.mBtnNoDraw)
            {
                if (mSurvivalStage == 0)
                {
                    mBoard.mStoreButton.mBtnNoDraw = false;
                    mBoard.mStoreButton.mDisabled = false;
                    mBoard.mStoreButton.SetLabel("[START_ONSLAUGHT]");
                    mBoard.mStoreButton.Resize(Constants.LastStandButtonRect.mX, Constants.LastStandButtonRect.mY, Constants.LastStandButtonRect.mWidth, Constants.LastStandButtonRect.mHeight);
                }
                else
                {
                    mBoard.mStoreButton.mBtnNoDraw = false;
                    mBoard.mStoreButton.mDisabled = false;
                    mBoard.mStoreButton.SetLabel("[CONTINUE_ONSLAUGHT]");
                    mBoard.mStoreButton.Resize(Constants.LastStandButtonRect.mX, Constants.LastStandButtonRect.mY, Constants.LastStandButtonRect.mWidth, Constants.LastStandButtonRect.mHeight);
                }
            }
            if (mChallengeState == ChallengeState.LastStandOnslaught && mApp.mGameScene == GameScenes.Playing)
            {
                mChallengeStateCounter++;
            }
        }

        public void WhackAZombiePlaceGraves(int theGraveCount)
        {
            int num = 0;
            for (int i = 0; i < Challenge.aPicks.Length; i++)
            {
                Challenge.aPicks[i].Reset();
            }
            for (int j = 3; j < Constants.GRIDSIZEX; j++)
            {
                for (int k = 0; k < Constants.MAX_GRIDSIZEY; k++)
                {
                    if (mBoard.CanAddGraveStoneAt(j, k))
                    {
                        Plant topPlantAt = mBoard.GetTopPlantAt(j, k, TopPlant.Any);
                        if (topPlantAt != null)
                        {
                            Challenge.aPicks[num].mWeight = 1;
                        }
                        else
                        {
                            Challenge.aPicks[num].mWeight = 100000;
                        }
                        Challenge.aPicks[num].mX = j;
                        Challenge.aPicks[num].mY = k;
                        num++;
                    }
                }
            }
            if (theGraveCount > num)
            {
                theGraveCount = num;
            }
            if (num == 0)
            {
                return;
            }
            for (int l = 0; l < theGraveCount; l++)
            {
                TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(Challenge.aPicks, num);
                int count = mBoard.mPlants.Count;
                for (int m = 0; m < count; m++)
                {
                    Plant plant = mBoard.mPlants[m];
                    if (!plant.mDead && plant.mPlantCol == todWeightedGridArray.mX && plant.mRow == todWeightedGridArray.mY)
                    {
                        plant.Die();
                    }
                }
                mBoard.AddAGraveStone(todWeightedGridArray.mX, todWeightedGridArray.mY);
                todWeightedGridArray.mWeight = 0;
            }
        }

        public bool BeghouledTwistSquareFromMouse(int theMouseX, int theMouseY, ref int theGridX, ref int theGridY)
        {
            theGridX = mBoard.PixelToGridX(theMouseX - 40, theMouseY - 40);
            theGridY = mBoard.PixelToGridY(theMouseX - 40, theMouseY - 40);
            if (theGridX == -1 || theGridY == -1 || theGridX > 6 || theGridY > 3)
            {
                theGridX = -1;
                theGridY = -1;
                return false;
            }
            return true;
        }

        public bool BeghouledTwistValidMove(int theGridX, int theGridY, BeghouledBoardState theBoardState)
        {
            if (theGridX == -1 || theGridY == -1 || theGridX > 6 || theGridY > 3)
            {
                return false;
            }
            SeedType seedType = theBoardState.mSeedType[theGridX, theGridY];
            SeedType seedType2 = theBoardState.mSeedType[theGridX + 1, theGridY];
            SeedType seedType3 = theBoardState.mSeedType[theGridX, theGridY + 1];
            SeedType seedType4 = theBoardState.mSeedType[theGridX + 1, theGridY + 1];
            return seedType != SeedType.None && seedType2 != SeedType.None && seedType3 != SeedType.None && seedType4 != SeedType.None;
        }

        public void BeghouledTwistMouseDown(int x, int y)
        {
            x = (int)(x * Constants.IS);
            y = (int)(y * Constants.IS);
            if (mBoard.HasLevelAwardDropped())
            {
                return;
            }
            BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
            LoadBeghouledBoardState(newBeghouledBoardState);
            int num = 0;
            int num2 = 0;
            if (!BeghouledTwistSquareFromMouse(x, y, ref num, ref num2) || !BeghouledTwistValidMove(num, num2, newBeghouledBoardState))
            {
                newBeghouledBoardState.PrepareForReuse();
                return;
            }
            Plant topPlantAt = mBoard.GetTopPlantAt(num, num2, TopPlant.Any);
            Plant topPlantAt2 = mBoard.GetTopPlantAt(num + 1, num2, TopPlant.Any);
            Plant topPlantAt3 = mBoard.GetTopPlantAt(num, num2 + 1, TopPlant.Any);
            Plant topPlantAt4 = mBoard.GetTopPlantAt(num + 1, num2 + 1, TopPlant.Any);
            if (!BeghouledTwistMoveCausesMatch(num, num2, newBeghouledBoardState))
            {
                topPlantAt.mX = mBoard.GridToPixelX(topPlantAt.mPlantCol, topPlantAt.mRow) + 20;
                topPlantAt2.mY = mBoard.GridToPixelY(topPlantAt2.mPlantCol, topPlantAt2.mRow) + 20;
                topPlantAt3.mY = mBoard.GridToPixelY(topPlantAt3.mPlantCol, topPlantAt3.mRow) - 20;
                topPlantAt4.mX = mBoard.GridToPixelX(topPlantAt4.mPlantCol, topPlantAt4.mRow) - 20;
                mApp.PlayFoley(FoleyType.Floop);
                newBeghouledBoardState.PrepareForReuse();
                return;
            }
            topPlantAt.mPlantCol++;
            topPlantAt.mRenderOrder = topPlantAt.CalcRenderOrder();
            topPlantAt2.mRow++;
            topPlantAt2.mRenderOrder = topPlantAt2.CalcRenderOrder();
            topPlantAt3.mRow--;
            topPlantAt3.mRenderOrder = topPlantAt3.CalcRenderOrder();
            topPlantAt4.mPlantCol--;
            topPlantAt4.mRenderOrder = topPlantAt4.CalcRenderOrder();
            BeghouledStartFalling(ChallengeState.BeghouledMoving);
            newBeghouledBoardState.PrepareForReuse();
        }

        public bool BeghouledTwistMoveCausesMatch(int theGridX, int theGridY, BeghouledBoardState theBoardState)
        {
            if (!BeghouledTwistValidMove(theGridX, theGridY, theBoardState))
            {
                return false;
            }
            SeedType seedType = theBoardState.mSeedType[theGridX, theGridY];
            SeedType seedType2 = theBoardState.mSeedType[theGridX + 1, theGridY];
            SeedType seedType3 = theBoardState.mSeedType[theGridX, theGridY + 1];
            SeedType seedType4 = theBoardState.mSeedType[theGridX + 1, theGridY + 1];
            theBoardState.mSeedType[theGridX, theGridY] = seedType3;
            theBoardState.mSeedType[theGridX + 1, theGridY] = seedType;
            theBoardState.mSeedType[theGridX, theGridY + 1] = seedType4;
            theBoardState.mSeedType[theGridX + 1, theGridY + 1] = seedType2;
            bool result = BeghouledBoardHasMatch(theBoardState);
            theBoardState.mSeedType[theGridX, theGridY] = seedType;
            theBoardState.mSeedType[theGridX + 1, theGridY] = seedType2;
            theBoardState.mSeedType[theGridX, theGridY + 1] = seedType3;
            theBoardState.mSeedType[theGridX + 1, theGridY + 1] = seedType4;
            return result;
        }

        public bool BeghouledTwistFlashMatch(BeghouledBoardState theBoardState, int theGridX, int theGridY)
        {
            if (!BeghouledTwistMoveCausesMatch(theGridX, theGridY, theBoardState))
            {
                return false;
            }
            Plant topPlantAt = mBoard.GetTopPlantAt(theGridX, theGridY, TopPlant.Any);
            Plant topPlantAt2 = mBoard.GetTopPlantAt(theGridX + 1, theGridY, TopPlant.Any);
            Plant topPlantAt3 = mBoard.GetTopPlantAt(theGridX, theGridY + 1, TopPlant.Any);
            Plant topPlantAt4 = mBoard.GetTopPlantAt(theGridX + 1, theGridY + 1, TopPlant.Any);
            topPlantAt.mBeghouledFlashCountdown = Math.Max(topPlantAt.mBeghouledFlashCountdown, 300);
            topPlantAt2.mBeghouledFlashCountdown = Math.Max(topPlantAt2.mBeghouledFlashCountdown, 300);
            topPlantAt3.mBeghouledFlashCountdown = Math.Max(topPlantAt3.mBeghouledFlashCountdown, 300);
            topPlantAt4.mBeghouledFlashCountdown = Math.Max(topPlantAt4.mBeghouledFlashCountdown, 300);
            return true;
        }

        public void BeghouledCancelMatchFlashing()
        {
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead)
                {
                    plant.mBeghouledFlashCountdown = Math.Min(plant.mBeghouledFlashCountdown, 25);
                }
            }
        }

        public void BeghouledStartFalling(ChallengeState theChallengeState)
        {
            mChallengeStateCounter = 100;
            mChallengeState = theChallengeState;
            BeghouledCancelMatchFlashing();
            mBoard.ClearAdvice(AdviceType.BeghouledNoMoves);
        }

        public void BeghouledFillHoles(BeghouledBoardState theBoardState, bool theAllowMatches)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (theBoardState.mSeedType[i, j] == SeedType.None && !mBeghouledEated[i, j])
                    {
                        SeedType seedType = BeghouledPickSeed(i, j, theBoardState, theAllowMatches);
                        theBoardState.mSeedType[i, j] = seedType;
                    }
                }
            }
        }

        public void BeghouledMakeStartBoard()
        {
            BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
            LoadBeghouledBoardState(newBeghouledBoardState);
            BeghouledBoardState newBeghouledBoardState2 = BeghouledBoardState.GetNewBeghouledBoardState();
            LoadBeghouledBoardState(newBeghouledBoardState2);
            BeghouledFillHoles(newBeghouledBoardState2, false);
            Debug.ASSERT(!BeghouledBoardHasMatch(newBeghouledBoardState2));
            BeghouledCreatePlants(newBeghouledBoardState, newBeghouledBoardState2);
            newBeghouledBoardState.PrepareForReuse();
            newBeghouledBoardState2.PrepareForReuse();
        }

        public void BeghouledCreatePlants(BeghouledBoardState theOldBoardState, BeghouledBoardState theNewBoardState)
        {
            for (int i = 0; i < 8; i++)
            {
                int num = 0;
                for (int j = 4; j >= 0; j--)
                {
                    if (theOldBoardState.mSeedType[i, j] == SeedType.None && theNewBoardState.mSeedType[i, j] != SeedType.None)
                    {
                        num++;
                        Plant plant = mBoard.NewPlant(i, j, theNewBoardState.mSeedType[i, j], SeedType.None);
                        plant.mY = 80 - num * 100;
                        BeghouledStartFalling(ChallengeState.BeghouledFalling);
                    }
                }
            }
        }

        public void PuzzlePhaseComplete(int theGridX, int theGridY)
        {
            if (PuzzleIsAwardStage())
            {
                int num = TodCommon.RandRangeInt(0, 99);
                CoinType theCoinType;
                if (num < 15)
                {
                    if (mApp.mZenGarden.CanDropPottedPlantLoot())
                    {
                        theCoinType = CoinType.AwardPresent;
                    }
                    else
                    {
                        theCoinType = CoinType.AwardMoneyBag;
                    }
                }
                else if (num < 30)
                {
                    if (mApp.mZenGarden.CanDropChocolate())
                    {
                        theCoinType = CoinType.AwardChocolate;
                    }
                    else
                    {
                        theCoinType = CoinType.AwardMoneyBag;
                    }
                }
                else
                {
                    theCoinType = CoinType.AwardBagDiamond;
                }
                float num2 = mBoard.GridToPixelX(theGridX, theGridY) + 40;
                float num3 = mBoard.GridToPixelY(theGridX, theGridY) + 40;
                mBoard.AddCoin((int)num2, (int)num3, theCoinType, CoinMotion.Coin);
                return;
            }
            mBoard.FadeOutLevel();
        }

        public void IZombiePlaceZombie(ZombieType theZombieType, int theGridX, int theGridY)
        {
            Zombie zombie = mBoard.AddZombieInRow(theZombieType, theGridY, 0);
            if (theZombieType == ZombieType.Bungee)
            {
                zombie.mTargetCol = theGridX;
                zombie.SetRow(theGridY);
                zombie.mPosX = mBoard.GridToPixelX(theGridX, theGridY);
                zombie.mPosY = zombie.GetPosYBasedOnRow(theGridY);
                zombie.mRenderOrder = Board.MakeRenderOrder(RenderLayer.GraveStone, theGridY, 7);
                return;
            }
            zombie.mPosX = mBoard.GridToPixelX(theGridX, theGridY) - 30f;
        }

        public void WhackAZombieUpdate()
        {
            if (mBoard.mSunMoney > 0 && mBoard.mTutorialState == TutorialState.Off)
            {
                mBoard.SetTutorialState(TutorialState.WhackAZombieBeforePickSeed);
                mBoard.mTutorialTimer = 1500;
            }
            if (mBoard.mTutorialState == TutorialState.WhackAZombieBeforePickSeed && mBoard.mTutorialTimer == 0)
            {
                mBoard.SetTutorialState(TutorialState.WhackAZombiePickSeed);
                mBoard.mTutorialTimer = 400;
            }
            if (mBoard.mTutorialState == TutorialState.WhackAZombiePickSeed && mBoard.mTutorialTimer == 0)
            {
                mBoard.SetTutorialState(TutorialState.WhackAZombieCompleted);
            }
        }

        public void LastStandCompletedStage()
        {
            mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
            mChallengeState = ChallengeState.Normal;
            mBoard.mSeedBank.RefreshAllPackets();
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead)
                {
                    if (plant.mState == PlantState.ChomperDigesting)
                    {
                        plant.mStateCountdown = Math.Min(plant.mStateCountdown, 200);
                    }
                    if (plant.mState == PlantState.CobcannonArming)
                    {
                        plant.mStateCountdown = Math.Min(plant.mStateCountdown, 200);
                    }
                    if (plant.mState == PlantState.MagnetshroomSucking || plant.mState == PlantState.MagnetshroomCharging)
                    {
                        plant.mStateCountdown = Math.Min(plant.mStateCountdown, 200);
                    }
                }
            }
            int survivalFlagsCompleted = mBoard.GetSurvivalFlagsCompleted();
            string theStringToSubstitute = mApp.Pluralize(survivalFlagsCompleted, "[ONE_FLAG]", "[COUNT_FLAGS]");
            string theAdvice = TodCommon.TodReplaceString("[SUCCESSFULLY_DEFENDED]", "{FLAGS}", theStringToSubstitute);
            mBoard.DisplayAdvice(theAdvice, MessageStyle.BigMiddleFast, AdviceType.None);
            mSurvivalStage++;
            mBoard.mLevelComplete = false;
            mBoard.InitZombieWaves();
        }

        public void TreeOfWisdomUpdate()
        {
        }

        public void TreeOfWisdomFertilize()
        {
        }

        public void TreeOfWisdomInit()
        {
        }

        public int TreeOfWisdomGetSize()
        {
            int currentChallengeIndex = mApp.GetCurrentChallengeIndex();
            return mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex];
        }

        public void TreeOfWisdomDraw(Graphics g)
        {
            bool flag = false;
            int x = mApp.mWidgetManager.mLastMouseX - mBoard.mX;
            int y = mApp.mWidgetManager.mLastMouseY - mBoard.mY;
            HitResult hitResult;
            mBoard.MouseHitTest(x, y, out hitResult, false);
            if (hitResult.mObjectType == GameObjectType.TreeOfWisdom && mBoard.mCursorObject.mCursorType == CursorType.TreeFood)
            {
                flag = true;
            }
            Reanimation reanimation = mApp.ReanimationGet(mReanimChallenge);
            reanimation.mEnableExtraAdditiveDraw = false;
            reanimation.DrawRenderGroup(g, 1);
            for (int i = 0; i < 6; i++)
            {
                Reanimation reanimation2 = mApp.ReanimationGet(mReanimCloud[i]);
                reanimation2.Draw(g);
            }
            int num = TreeOfWisdomGetSize();
            if (flag)
            {
                if (num < 18)
                {
                    reanimation.mExtraAdditiveColor = new SexyColor(255, 255, 255, 128);
                    reanimation.mEnableExtraAdditiveDraw = true;
                }
                else
                {
                    reanimation.mExtraAdditiveColor = new SexyColor(255, 255, 255, 48);
                    reanimation.mEnableExtraAdditiveDraw = true;
                }
            }
            else
            {
                reanimation.mEnableExtraAdditiveDraw = false;
            }
            reanimation.DrawRenderGroup(g, 2);
            reanimation.mEnableExtraAdditiveDraw = false;
            reanimation.DrawRenderGroup(g, 3);
            if (flag)
            {
                reanimation.mExtraAdditiveColor = new SexyColor(255, 255, 255, 32);
                reanimation.mEnableExtraAdditiveDraw = true;
            }
            else
            {
                reanimation.mEnableExtraAdditiveDraw = false;
            }
            reanimation.DrawRenderGroup(g, 4);
            if (mChallengeState == ChallengeState.TreeGiveWisdom || mChallengeState == ChallengeState.TreeBabbling)
            {
                int num2;
                int num3;
                if (num < 7)
                {
                    num2 = 400;
                    num3 = 152;
                }
                else if (num < 12)
                {
                    num2 = 395;
                    num3 = 60;
                }
                else
                {
                    num2 = 390;
                    num3 = 52;
                }
                string theText = Common.StrFormat_("[TREE_OF_WISDOM_%d]", mTreeOfWisdomTalkIndex);
                TRect theRect = new TRect((int)((num2 + 25) * Constants.S), (int)((num3 + 6) * Constants.S), 233, 144);
                TodStringFile.TodDrawStringWrapped(g, theText, theRect, Resources.FONT_BRIANNETOD16, SexyColor.Black, DrawStringJustification.CenterVerticalMiddle);
            }
            int num4 = num;
            float num5 = 1f;
            if (mChallengeState == ChallengeState.TreeJustGrew)
            {
                if (mChallengeStateCounter > 30)
                {
                    num4--;
                }
                num5 = TodCommon.TodAnimateCurveFloat(55, 20, mChallengeStateCounter, 1f, 1.2f, TodCurves.Bounce);
            }
            if (num4 >= 50)
            {
                string theString = TodCommon.TodReplaceNumberString("[TREE_OF_WISDOM_HIEGHT]", "{HEIGHT}", num4);
                float num6 = Resources.FONT_HOUSEOFTERROR16.StringWidth(theString) * num5;
                float num7 = Resources.FONT_HOUSEOFTERROR16.mAscent * num5;
                Matrix theMatrix = default(Matrix);
                TodCommon.TodScaleTransformMatrix(ref theMatrix, 400f - num6 * 0.5f, 20f + num7 * 0.5f, num5, num5);
                TodCommon.TodDrawStringMatrix(g, Resources.FONT_HOUSEOFTERROR16, theMatrix, theString, new SexyColor(255, 255, 255));
            }
        }

        public void TreeOfWisdomNextGarden()
        {
            TreeOfWisdomLeave();
            mApp.KillBoard();
            mApp.PreNewGame(GameMode.ChallengeZenGarden, false);
        }

        public void TreeOfWisdomToolUpdate(GridItem theZenTool)
        {
        }

        public void TreeOfWisdomOpenStore()
        {
        }

        public void TreeOfWisdomLeave()
        {
        }

        public void TreeOfWisdomGrow()
        {
        }

        public void TreeOfWisdomTool(int aMouseX, int aMouseY)
        {
        }

        public bool TreeOfWisdomHitTest(int theX, int theY, HitResult theHitResult)
        {
            return false;
        }

        public void TreeOfWisdomBabble()
        {
        }

        public void TreeOfWisdomGiveWisdom()
        {
        }

        public bool TreeOfWisdomCanFeed()
        {
            if (mChallengeState == ChallengeState.TreeJustGrew)
            {
                return false;
            }
            int num = -1;
            GridItem gridItem = null;
            while (mBoard.IterateGridItems(ref gridItem, ref num))
            {
                if (gridItem.mGridItemType == GridItemType.ZenTool)
                {
                    return false;
                }
            }
            return true;
        }

        public void TreeOfWisdomSayRepeat()
        {
        }

        public bool PuzzleIsAwardStage()
        {
            if (mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
            {
                return false;
            }
            int num = 1;
            if (mApp.mGameMode == GameMode.PuzzleIZombieEndless)
            {
                num = 3;
            }
            else if (mApp.mGameMode == GameMode.ScaryPotterEndless)
            {
                num = 10;
            }
            return (mSurvivalStage + 1) % num == 0;
        }

        public void BackFromStore()
        {
            mApp.KillDialog(4);
        }

        private const int MAX_SPAWNING_SEED_TYPES = 20;

        public static SeedType[,] gArtChallengeWallnut;

        public static SeedType[,] gArtChallengeSunFlower;

        public static SeedType[,] gArtChallengeStarfruit;

        public LawnApp mApp;

        public Board mBoard;

        public bool mBeghouledMouseCapture;

        public int mBeghouledMouseDownX;

        public int mBeghouledMouseDownY;

        public bool[,] mBeghouledEated = new bool[Constants.GRIDSIZEX, Constants.MAX_GRIDSIZEY];

        public bool[] mBeghouledPurcasedUpgrade = new bool[3];

        public int mBeghouledMatchesThisMove;

        public ChallengeState mChallengeState;

        public int mChallengeStateCounter;

        public int mConveyorBeltCounter;

        public int mChallengeScore;

        public bool mShowBowlingLine;

        public SeedType mLastConveyorSeedType;

        public int mSurvivalStage;

        public int mSlotMachineRollCount;

        public Reanimation mReanimChallenge;

        public Reanimation[] mReanimCloud = new Reanimation[6];

        public int[] mCloudCounter = new int[6];

        public int mChallengeGridX;

        public int mChallengeGridY;

        public int mScaryPotterPots;

        public int mRainCounter;

        public int mTreeOfWisdomTalkIndex;

        public string mName = "Put a name here";

        private static int MAX_PICKS;

        private static GridItem[] aGridPickItemArray;

        private static TodWeightedArray[] aGridPicks;

        private static TodWeightedArray[] aSeedPickArray;

        private static TodWeightedArray[] aPotArray;

        private static TodWeightedGridArray[] aGridArray;

        private static TodWeightedGridArray[] aPicks;

        private int speedBoardCounter;

        private string slotMachineMessage;

        private int slotMachineMessageCached = -1;

        private int[] aDoubleChance = new int[]
        {
            0,
            30,
            10,
            10,
            15,
            18
        };

        private int[] aTripleChance = new int[]
        {
            default(int),
            default(int),
            default(int),
            default(int),
            10,
            13
        };

        private int[] aPailChance = new int[]
        {
            0,
            0,
            0,
            10,
            15,
            15
        };

        private int[] aConeChance = new int[]
        {
            0,
            0,
            30,
            30,
            30,
            30
        };
    }
}
