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
			array[0, 0] = SeedType.SEED_NONE;
			array[0, 1] = SeedType.SEED_NONE;
			array[0, 2] = SeedType.SEED_NONE;
			array[0, 3] = SeedType.SEED_NONE;
			array[0, 4] = SeedType.SEED_WALLNUT;
			array[0, 5] = SeedType.SEED_WALLNUT;
			array[0, 6] = SeedType.SEED_WALLNUT;
			array[0, 7] = SeedType.SEED_NONE;
			array[0, 8] = SeedType.SEED_NONE;
			array[1, 0] = SeedType.SEED_NONE;
			array[1, 1] = SeedType.SEED_NONE;
			array[1, 2] = SeedType.SEED_NONE;
			array[1, 3] = SeedType.SEED_WALLNUT;
			array[1, 4] = SeedType.SEED_NONE;
			array[1, 5] = SeedType.SEED_NONE;
			array[1, 6] = SeedType.SEED_NONE;
			array[1, 7] = SeedType.SEED_WALLNUT;
			array[1, 8] = SeedType.SEED_NONE;
			array[2, 0] = SeedType.SEED_NONE;
			array[2, 1] = SeedType.SEED_NONE;
			array[2, 2] = SeedType.SEED_NONE;
			array[2, 3] = SeedType.SEED_WALLNUT;
			array[2, 4] = SeedType.SEED_NONE;
			array[2, 5] = SeedType.SEED_NONE;
			array[2, 6] = SeedType.SEED_NONE;
			array[2, 7] = SeedType.SEED_WALLNUT;
			array[2, 8] = SeedType.SEED_NONE;
			array[3, 0] = SeedType.SEED_NONE;
			array[3, 1] = SeedType.SEED_NONE;
			array[3, 2] = SeedType.SEED_NONE;
			array[3, 3] = SeedType.SEED_WALLNUT;
			array[3, 4] = SeedType.SEED_NONE;
			array[3, 5] = SeedType.SEED_NONE;
			array[3, 6] = SeedType.SEED_NONE;
			array[3, 7] = SeedType.SEED_WALLNUT;
			array[3, 8] = SeedType.SEED_NONE;
			array[4, 0] = SeedType.SEED_NONE;
			array[4, 1] = SeedType.SEED_NONE;
			array[4, 2] = SeedType.SEED_NONE;
			array[4, 3] = SeedType.SEED_NONE;
			array[4, 4] = SeedType.SEED_WALLNUT;
			array[4, 5] = SeedType.SEED_WALLNUT;
			array[4, 6] = SeedType.SEED_WALLNUT;
			array[4, 7] = SeedType.SEED_NONE;
			array[4, 8] = SeedType.SEED_NONE;
			array[5, 0] = SeedType.SEED_NONE;
			array[5, 1] = SeedType.SEED_NONE;
			array[5, 2] = SeedType.SEED_NONE;
			array[5, 3] = SeedType.SEED_NONE;
			array[5, 4] = SeedType.SEED_NONE;
			array[5, 5] = SeedType.SEED_NONE;
			array[5, 6] = SeedType.SEED_NONE;
			array[5, 7] = SeedType.SEED_NONE;
			array[5, 8] = SeedType.SEED_NONE;
			Challenge.gArtChallengeWallnut = array;
			SeedType[,] array2 = new SeedType[6, 9];
			array2[0, 0] = SeedType.SEED_NONE;
			array2[0, 1] = SeedType.SEED_NONE;
			array2[0, 2] = SeedType.SEED_STARFRUIT;
			array2[0, 3] = SeedType.SEED_STARFRUIT;
			array2[0, 4] = SeedType.SEED_STARFRUIT;
			array2[0, 5] = SeedType.SEED_NONE;
			array2[0, 6] = SeedType.SEED_NONE;
			array2[0, 7] = SeedType.SEED_NONE;
			array2[0, 8] = SeedType.SEED_NONE;
			array2[1, 0] = SeedType.SEED_NONE;
			array2[1, 1] = SeedType.SEED_STARFRUIT;
			array2[1, 2] = SeedType.SEED_WALLNUT;
			array2[1, 3] = SeedType.SEED_WALLNUT;
			array2[1, 4] = SeedType.SEED_WALLNUT;
			array2[1, 5] = SeedType.SEED_STARFRUIT;
			array2[1, 6] = SeedType.SEED_NONE;
			array2[1, 7] = SeedType.SEED_NONE;
			array2[1, 8] = SeedType.SEED_NONE;
			array2[2, 0] = SeedType.SEED_NONE;
			array2[2, 1] = SeedType.SEED_NONE;
			array2[2, 2] = SeedType.SEED_STARFRUIT;
			array2[2, 3] = SeedType.SEED_STARFRUIT;
			array2[2, 4] = SeedType.SEED_STARFRUIT;
			array2[2, 5] = SeedType.SEED_NONE;
			array2[2, 6] = SeedType.SEED_NONE;
			array2[2, 7] = SeedType.SEED_NONE;
			array2[2, 8] = SeedType.SEED_NONE;
			array2[3, 0] = SeedType.SEED_NONE;
			array2[3, 1] = SeedType.SEED_NONE;
			array2[3, 2] = SeedType.SEED_NONE;
			array2[3, 3] = SeedType.SEED_UMBRELLA;
			array2[3, 4] = SeedType.SEED_NONE;
			array2[3, 5] = SeedType.SEED_NONE;
			array2[3, 6] = SeedType.SEED_NONE;
			array2[3, 7] = SeedType.SEED_NONE;
			array2[3, 8] = SeedType.SEED_NONE;
			array2[4, 0] = SeedType.SEED_NONE;
			array2[4, 1] = SeedType.SEED_NONE;
			array2[4, 2] = SeedType.SEED_UMBRELLA;
			array2[4, 3] = SeedType.SEED_UMBRELLA;
			array2[4, 4] = SeedType.SEED_UMBRELLA;
			array2[4, 5] = SeedType.SEED_NONE;
			array2[4, 6] = SeedType.SEED_NONE;
			array2[4, 7] = SeedType.SEED_NONE;
			array2[4, 8] = SeedType.SEED_NONE;
			array2[5, 0] = SeedType.SEED_NONE;
			array2[5, 1] = SeedType.SEED_NONE;
			array2[5, 2] = SeedType.SEED_NONE;
			array2[5, 3] = SeedType.SEED_NONE;
			array2[5, 4] = SeedType.SEED_NONE;
			array2[5, 5] = SeedType.SEED_NONE;
			array2[5, 6] = SeedType.SEED_NONE;
			array2[5, 7] = SeedType.SEED_NONE;
			array2[5, 8] = SeedType.SEED_NONE;
			Challenge.gArtChallengeSunFlower = array2;
			SeedType[,] array3 = new SeedType[6, 9];
			array3[0, 0] = SeedType.SEED_NONE;
			array3[0, 1] = SeedType.SEED_NONE;
			array3[0, 2] = SeedType.SEED_NONE;
			array3[0, 3] = SeedType.SEED_STARFRUIT;
			array3[0, 4] = SeedType.SEED_NONE;
			array3[0, 5] = SeedType.SEED_NONE;
			array3[0, 6] = SeedType.SEED_NONE;
			array3[0, 7] = SeedType.SEED_NONE;
			array3[0, 8] = SeedType.SEED_NONE;
			array3[1, 0] = SeedType.SEED_NONE;
			array3[1, 1] = SeedType.SEED_NONE;
			array3[1, 2] = SeedType.SEED_NONE;
			array3[1, 3] = SeedType.SEED_STARFRUIT;
			array3[1, 4] = SeedType.SEED_STARFRUIT;
			array3[1, 5] = SeedType.SEED_NONE;
			array3[1, 6] = SeedType.SEED_NONE;
			array3[1, 7] = SeedType.SEED_NONE;
			array3[1, 8] = SeedType.SEED_NONE;
			array3[2, 0] = SeedType.SEED_NONE;
			array3[2, 1] = SeedType.SEED_STARFRUIT;
			array3[2, 2] = SeedType.SEED_STARFRUIT;
			array3[2, 3] = SeedType.SEED_STARFRUIT;
			array3[2, 4] = SeedType.SEED_STARFRUIT;
			array3[2, 5] = SeedType.SEED_STARFRUIT;
			array3[2, 6] = SeedType.SEED_STARFRUIT;
			array3[2, 7] = SeedType.SEED_NONE;
			array3[2, 8] = SeedType.SEED_NONE;
			array3[3, 0] = SeedType.SEED_NONE;
			array3[3, 1] = SeedType.SEED_NONE;
			array3[3, 2] = SeedType.SEED_NONE;
			array3[3, 3] = SeedType.SEED_STARFRUIT;
			array3[3, 4] = SeedType.SEED_STARFRUIT;
			array3[3, 5] = SeedType.SEED_STARFRUIT;
			array3[3, 6] = SeedType.SEED_NONE;
			array3[3, 7] = SeedType.SEED_NONE;
			array3[3, 8] = SeedType.SEED_NONE;
			array3[4, 0] = SeedType.SEED_NONE;
			array3[4, 1] = SeedType.SEED_NONE;
			array3[4, 2] = SeedType.SEED_NONE;
			array3[4, 3] = SeedType.SEED_STARFRUIT;
			array3[4, 4] = SeedType.SEED_NONE;
			array3[4, 5] = SeedType.SEED_NONE;
			array3[4, 6] = SeedType.SEED_STARFRUIT;
			array3[4, 7] = SeedType.SEED_NONE;
			array3[4, 8] = SeedType.SEED_NONE;
			array3[5, 0] = SeedType.SEED_NONE;
			array3[5, 1] = SeedType.SEED_NONE;
			array3[5, 2] = SeedType.SEED_NONE;
			array3[5, 3] = SeedType.SEED_NONE;
			array3[5, 4] = SeedType.SEED_NONE;
			array3[5, 5] = SeedType.SEED_NONE;
			array3[5, 6] = SeedType.SEED_NONE;
			array3[5, 7] = SeedType.SEED_NONE;
			array3[5, 8] = SeedType.SEED_NONE;
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
			mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
			mShowBowlingLine = false;
			mLastConveyorSeedType = SeedType.SEED_NONE;
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
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_SLOT_MACHINE_HANDLE, true);
				Reanimation reanimation = mApp.AddReanimation(trect.mX - 243f, trect.mY + 50f, 0, ReanimationType.REANIM_SLOT_MACHINE_HANDLE);
				reanimation.mIsAttachment = true;
				reanimation.mAnimRate = 0f;
				mReanimChallenge = mApp.ReanimationGetID(reanimation);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				mName = TodStringFile.TodStringTranslate("[ZEN_GARDEN]");
				return;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				mName = TodStringFile.TodStringTranslate("[TREE_OF_WISDOM]");
				return;
			}
			if (mApp.mGameMode != GameMode.GAMEMODE_INTRO)
			{
				if (mApp.mGameMode == GameMode.GAMEMODE_ADVENTURE)
				{
					return;
				}
				mName = ChallengeScreen.gChallengeDefs[mApp.mGameMode - GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1].mChallengeName;
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
				mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_HAMMER;
				mBoard.mZombieCountDown = 200;
				mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
			}
			if (mApp.IsStormyNightLevel())
			{
				mChallengeState = ChallengeState.STATECHALLENGE_STORM_FLASH_1;
				mChallengeStateCounter = 400;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA)
			{
				for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
				{
					if (mBoard.mPlantRow[i] != PlantRowType.PLANTROW_POOL)
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
				mBoard.mSeedBank.AddSeed(SeedType.SEED_WALLNUT);
				mConveyorBeltCounter = 400;
			}
			if (mApp.IsWallnutBowlingLevel())
			{
				mShowBowlingLine = true;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SHOVEL || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SQUIRREL)
			{
				ShovelAddWallnuts();
			}
			if (mApp.IsScaryPotterLevel())
			{
				ScaryPotterStart();
			}
			if (mApp.IsLittleTroubleLevel() || mApp.IsStormyNightLevel() || mApp.IsBungeeBlitzLevel() || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
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
				mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_SURVIVE_FLAGS);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && mSurvivalStage == 0)
			{
				string theAdvice2 = TodCommon.TodReplaceNumberString("[ADVICE_SURVIVE_FLAGS]", "{FLAGS}", 5);
				mBoard.DisplayAdvice(theAdvice2, MessageStyle.MESSAGE_STYLE_BIG_MIDDLE_FAST, AdviceType.ADVICE_SURVIVE_FLAGS);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1)
			{
				mBoard.DisplayAdvice("[ADVICE_FILL_IN_WALLNUTS]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_2)
			{
				mBoard.DisplayAdvice("[ADVICE_FILL_IN_SPACES]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SEEING_STARS)
			{
				mBoard.DisplayAdvice("[ADVICE_FILL_IN_STARFRUIT]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
			}
			if (mApp.IsSlotMachineLevel())
			{
				mBoard.SetTutorialState(TutorialState.TUTORIAL_SLOT_MACHINE_PULL);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				mBoard.mZombieCountDown = 200;
				mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
				mChallengeStateCounter = 1500;
				BeghouledMakeStartBoard();
				BeghouledUpdateCraters();
				if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
				{
					mBoard.DisplayAdvice("[ADVICE_BEGHOULED_DRAG_TO_MATCH_3]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
				}
				else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
				{
					mBoard.DisplayAdvice("[ADVICE_BEGHOULED_TWIST_TO_MATCH_3]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
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
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
			{
				PortalStart();
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				mBoard.mCurrentWave = 9;
				mBoard.mZombieCountDown = 2400;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_AIR_RAID || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA)
			{
				mBoard.mZombieCountDown = 4500;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_POGO_PARTY)
			{
				mBoard.mZombieCountDown = 5500;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
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
					theBoardState.mSeedType[i, j] = SeedType.SEED_NONE;
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
			Debug.ASSERT(theBoardState.mSeedType[theGridX, theGridY] == SeedType.SEED_NONE);
			int[] array = new int[6];
			int num = 0;
			for (int i = 0; i < 6; i++)
			{
				SeedType seedType = SeedType.SEED_PEASHOOTER;
				switch (i)
				{
				case 0:
					seedType = SeedType.SEED_PUFFSHROOM;
					break;
				case 1:
					seedType = SeedType.SEED_STARFRUIT;
					break;
				case 2:
					seedType = SeedType.SEED_MAGNETSHROOM;
					break;
				case 3:
					seedType = SeedType.SEED_SNOWPEA;
					break;
				case 4:
					seedType = SeedType.SEED_WALLNUT;
					break;
				case 5:
					seedType = SeedType.SEED_PEASHOOTER;
					break;
				default:
					Debug.ASSERT(false);
					break;
				}
				if (mBeghouledPurcasedUpgrade[0] && seedType == SeedType.SEED_PEASHOOTER)
				{
					seedType = SeedType.SEED_REPEATER;
				}
				else if (mBeghouledPurcasedUpgrade[1] && seedType == SeedType.SEED_PUFFSHROOM)
				{
					seedType = SeedType.SEED_FUMESHROOM;
				}
				else if (mBeghouledPurcasedUpgrade[2] && seedType == SeedType.SEED_WALLNUT)
				{
					seedType = SeedType.SEED_TALLNUT;
				}
				theBoardState.mSeedType[theGridX, theGridY] = seedType;
				if (theAllowMatches || !BeghouledBoardHasMatch(theBoardState))
				{
					array[num] = (int)seedType;
					num++;
				}
			}
			theBoardState.mSeedType[theGridX, theGridY] = SeedType.SEED_NONE;
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
				return SeedType.SEED_NONE;
			}
			return theBoardState.mSeedType[x, y];
		}

		public int BeghouledVerticalMatchLength(int x, int y, BeghouledBoardState theBoardState)
		{
			SeedType seedType = BeghouledGetPlantAt(x, y, theBoardState);
			if (seedType == SeedType.SEED_NONE)
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
			if (seedType == SeedType.SEED_NONE)
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
			mBoard.ClearAdvice(AdviceType.ADVICE_BEGHOULED_DRAG_TO_MATCH_3);
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
			Plant topPlantAt = mBoard.GetTopPlantAt(num3, num4, PlantPriority.TOPPLANT_ANY);
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
					mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
				}
				newBeghouledBoardState.PrepareForReuse();
				return;
			}
			Plant topPlantAt2 = mBoard.GetTopPlantAt(num5, num6, PlantPriority.TOPPLANT_ANY);
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
			BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_MOVING);
			newBeghouledBoardState.PrepareForReuse();
		}

		public void BeghouledDragCancel()
		{
			mBeghouledMouseCapture = false;
		}

		public bool MouseMove(int x, int y)
		{
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED && !mBoard.HasLevelAwardDropped())
			{
				if (mBeghouledMouseCapture)
				{
					BeghouledDragUpdate(x, y);
					return true;
				}
				HitResult hitResult;
				mBoard.MouseHitTest(x, y, out hitResult, false);
				if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
				{
					return true;
				}
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				if (mChallengeState == ChallengeState.STATECHALLENGE_ZEN_FADING)
				{
					mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
				}
				mChallengeStateCounter = 3000;
			}
			return false;
		}

		public bool MouseDown(int x, int y, int theClickCount, HitResult theHitResult)
		{
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				return mApp.mZenGarden.MouseDownZenGarden(x, y, theClickCount, theHitResult);
			}
			if (mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				return false;
			}
			if (mBoard.IsScaryPotterDaveTalking() && mApp.mCrazyDaveMessageIndex != -1)
			{
				AdvanceCrazyDaveDialog();
				return true;
			}
			if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_COIN && theClickCount >= 0)
			{
				return false;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
			{
				if (mChallengeState != ChallengeState.STATECHALLENGE_NORMAL)
				{
					return false;
				}
				if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
				{
					BeghouledDragStart(x, y);
					return true;
				}
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				if (mChallengeState != ChallengeState.STATECHALLENGE_NORMAL)
				{
					return false;
				}
				BeghouledTwistMouseDown(x, y);
			}
			if (mApp.IsSlotMachineLevel() && theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_SLOT_MACHINE_HANDLE && mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL && mChallengeState == ChallengeState.STATECHALLENGE_NORMAL)
			{
				if (mBoard.TakeSunMoney(25))
				{
					for (int i = 0; i < 3; i++)
					{
						mBoard.mSeedBank.mSeedPackets[i].SlotMachineStart();
					}
					Reanimation reanimation = mApp.ReanimationGet(mReanimChallenge);
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_pull, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 36f);
					mChallengeState = ChallengeState.STATECHALLENGE_SLOT_MACHINE_ROLLING;
					mBoard.SetTutorialState(TutorialState.TUTORIAL_SLOT_MACHINE_COMPLETED);
					mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
					mSlotMachineRollCount++;
					mApp.PlaySample(Resources.SOUND_SLOT_MACHINE);
				}
				return true;
			}
			if (mApp.IsWhackAZombieLevel() && theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_NONE && mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_HAMMER && theClickCount >= 0)
			{
				MouseDownWhackAZombie(x, y);
				return true;
			}
			if (mApp.IsScaryPotterLevel() && theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_SCARY_POT)
			{
				ScaryPotterMalletPot((GridItem)theHitResult.mObject);
				return true;
			}
			return false;
		}

		public bool MouseUp(int theX, int theY)
		{
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
			{
				if (mBeghouledMouseCapture && !mBoard.mAdvice.IsBeingDisplayed() && mChallengeScore == 0)
				{
					mBoard.DisplayAdvice("[ADVICE_BEGHOULED_DRAG_TO_MATCH_3]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_DRAG_TO_MATCH_3);
				}
				BeghouledDragCancel();
			}
			return false;
		}

		public void ClearCursor()
		{
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
			{
				BeghouledDragCancel();
			}
			if (mApp.IsWhackAZombieLevel() && !mBoard.HasLevelAwardDropped())
			{
				mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_HAMMER;
			}
		}

		public void BeghouledRemoveHorizontalMatch(int x, int y, BeghouledBoardState theBoardState)
		{
			SeedType seedType = BeghouledGetPlantAt(x, y, theBoardState);
			do
			{
				Plant topPlantAt = mBoard.GetTopPlantAt(x, y, PlantPriority.TOPPLANT_ANY);
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
				Plant topPlantAt = mBoard.GetTopPlantAt(x, y, PlantPriority.TOPPLANT_ANY);
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

		public void Update()
		{
			if (mApp.IsStormyNightLevel())
			{
				UpdateStormyNight();
			}
			if (mBoard.mPaused)
			{
				if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
				{
					mChallengeGridX = -1;
					mChallengeGridY = -1;
				}
				return;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS || mApp.IsStormyNightLevel())
			{
				UpdateRain();
			}
			if (mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				return;
			}
			if (mBoard.HasConveyorBeltSeedBank())
			{
				UpdateConveyorBelt();
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
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
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SPEED && speedBoardCounter++ % 3 == 0)
			{
				mBoard.UpdateGame();
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
			{
				UpdateRainingSeeds();
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
			{
				UpdatePortalCombat();
			}
			if (mApp.IsSquirrelLevel())
			{
				SquirrelUpdate();
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE && mBoard.mMainCounter == 3000)
			{
				mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
				mApp.PlaySample(Resources.SOUND_LOSEMUSIC);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
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
			mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 75, mChallengeScore, 0, 150, TodCurves.CURVE_LINEAR);
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
				int currentPlantCost = mBoard.GetCurrentPlantCost(SeedType.SEED_BEGHOULED_BUTTON_CRATER, SeedType.SEED_NONE);
				if (mBoard.CanTakeSunMoney(currentPlantCost) && BeghouledCanClearCrater() && !mBoard.HasLevelAwardDropped())
				{
					mBoard.DisplayAdvice("[ADVICE_BEGHOULED_USE_CRATER_2]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_USE_CRATER_2);
				}
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && mChallengeState == ChallengeState.STATECHALLENGE_NORMAL)
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
			if (!flag && (mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_FALLING || mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_MOVING))
			{
				mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
				mChallengeStateCounter = 1500;
				BeghouledBoardState newBeghouledBoardState2 = BeghouledBoardState.GetNewBeghouledBoardState();
				LoadBeghouledBoardState(newBeghouledBoardState2);
				BeghouledRemoveMatches(newBeghouledBoardState2);
				LoadBeghouledBoardState(newBeghouledBoardState2);
				BeghouledMakePlantsFall(newBeghouledBoardState2);
				BeghouledPopulateBoard();
				if (mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_FALLING)
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
			if (mChallengeState == ChallengeState.STATECHALLENGE_NORMAL)
			{
				BeghouledFlashAMatch();
				mChallengeStateCounter = 1500;
				return;
			}
			if (mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_NO_MATCHES)
			{
				mApp.AddTodParticle(400f, 300f, 400000, ParticleEffect.PARTICLE_SCREEN_FLASH);
				BeghouledShuffle();
			}
		}

		public bool UpdateBeghouledPlant(Plant thePlant)
		{
			bool flag = false;
			int num = mBoard.GridToPixelX(thePlant.mPlantCol, thePlant.mRow) - thePlant.mX;
			int num2 = mBoard.GridToPixelY(thePlant.mPlantCol, thePlant.mRow) - thePlant.mY;
			int num3;
			if (mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_MOVING)
			{
				num3 = 3;
			}
			else
			{
				num3 = TodCommon.TodAnimateCurve(90, 30, mChallengeStateCounter, 1, 20, TodCurves.CURVE_EASE_IN);
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
			if (flag && (thePlant.mState == PlantState.STATE_MAGNETSHROOM_CHARGING || thePlant.mState == PlantState.STATE_MAGNETSHROOM_SUCKING))
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
				Plant topPlantAt = mBoard.GetTopPlantAt(x, i, PlantPriority.TOPPLANT_ANY);
				if (topPlantAt != null)
				{
					topPlantAt.mRow = y;
					topPlantAt.mRenderOrder = topPlantAt.CalcRenderOrder();
					theBoardState.mSeedType[x, y] = topPlantAt.mSeedType;
					theBoardState.mSeedType[x, i] = SeedType.SEED_NONE;
					BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_FALLING);
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
					if (seedType == SeedType.SEED_NONE)
					{
						BeghouledFallIntoSquare(j, i, theBoardState);
					}
				}
			}
		}

		public void ZombieAtePlant(Zombie theZombie, Plant thePlant)
		{
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				mBeghouledEated[thePlant.mPlantCol, thePlant.mRow] = true;
				if (mBoard.mSeedBank.mNumPackets == 4)
				{
					mBoard.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.SEED_BEGHOULED_BUTTON_CRATER, SeedType.SEED_NONE);
					mBoard.mSeedBank.mNumPackets = 5;
					mBoard.DisplayAdvice("[ADVICE_BEGHOULED_USE_CRATER_1]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_USE_CRATER_1);
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
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				DrawBeghouled(g);
			}
			if (mApp.IsWallnutBowlingLevel() && mShowBowlingLine)
			{
				g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(200f), 77f * Constants.S);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5)
			{
				g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(244f), 73f * Constants.S);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
			{
				g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(284f), 73f * Constants.S);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_9)
			{
				g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(330f), 73f * Constants.S);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE)
			{
				int aMainCounter = mBoard.mMainCounter;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
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
					if (artChallengeSeed != SeedType.SEED_NONE && mBoard.GetTopPlantAt(j, i, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION) == null)
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
						float thePosX = celPosition[0].x + (celPosition[2].x - celPosition[0].x) / 2;
						float thePosY = celPosition[2].y - Constants.Challenge_SeeingStars_StarfruitPreview_Offset_Y;
						Plant.DrawSeedType(g, artChallengeSeed, SeedType.SEED_NONE, DrawVariation.VARIATION_NORMAL, thePosX, thePosY);
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
					if (artChallengeSeed != SeedType.SEED_NONE)
					{
						Plant topPlantAt = mBoard.GetTopPlantAt(j, i, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
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
				return SeedType.SEED_NONE;
			}
			Debug.ASSERT(theGridX >= 0 && theGridX < 9 && theGridY >= 0);
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1)
			{
				return Challenge.gArtChallengeWallnut[theGridY, theGridX];
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_2)
			{
				return Challenge.gArtChallengeSunFlower[theGridY, theGridX];
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SEEING_STARS)
			{
				return Challenge.gArtChallengeStarfruit[theGridY, theGridX];
			}
			return SeedType.SEED_NONE;
		}

		public void PlantAdded(Plant thePlant)
		{
			if (mApp.IsArtChallenge())
			{
				SeedType artChallengeSeed = GetArtChallengeSeed(thePlant.mPlantCol, thePlant.mRow);
				if (artChallengeSeed != SeedType.SEED_NONE && artChallengeSeed == thePlant.mSeedType)
				{
					mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
					mApp.AddTodParticle(thePlant.mX + 40, thePlant.mY + 40, 400000, ParticleEffect.PARTICLE_PRESENT_PICKUP);
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
					return PlantingReason.PLANTING_NOT_PASSED_LINE;
				}
			}
			else if (mApp.IsIZombieLevel())
			{
				int num = 6;
				if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5)
				{
					num = 4;
				}
				if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
				{
					num = 5;
				}
				if (theType == SeedType.SEED_ZOMBIE_BUNGEE)
				{
					if (theGridX < num)
					{
						return PlantingReason.PLANTING_OK;
					}
					return PlantingReason.PLANTING_NOT_HERE;
				}
				else if (Challenge.IsZombieSeedType(theType))
				{
					if (theGridX >= num)
					{
						return PlantingReason.PLANTING_OK;
					}
					return PlantingReason.PLANTING_NOT_HERE;
				}
			}
			else if (mApp.IsArtChallenge())
			{
				SeedType artChallengeSeed = GetArtChallengeSeed(theGridX, theGridY);
				if (artChallengeSeed != SeedType.SEED_NONE && artChallengeSeed != theType && theType != SeedType.SEED_LILYPAD && theType != SeedType.SEED_PUMPKINSHELL)
				{
					return PlantingReason.PLANTING_NOT_ON_ART;
				}
				if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1)
				{
					if (theGridX == 4 && theGridY == 1)
					{
						return PlantingReason.PLANTING_NOT_HERE;
					}
					if (theGridX == 6 && theGridY == 1)
					{
						return PlantingReason.PLANTING_NOT_HERE;
					}
				}
			}
			else if (mApp.IsFinalBossLevel() && theGridX >= 8)
			{
				return PlantingReason.PLANTING_NOT_HERE;
			}
			return PlantingReason.PLANTING_OK;
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
			if (seedType == SeedType.SEED_NONE)
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
					if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
					{
						if (BeghouledIsValidMove(j, i, j + 1, i, theBoardState) || BeghouledIsValidMove(j, i, j, i + 1, theBoardState))
						{
							return true;
						}
					}
					else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && BeghouledTwistMoveCausesMatch(j, i, theBoardState))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void BeghouledCheckStuckState()
		{
			if (mChallengeState != ChallengeState.STATECHALLENGE_NORMAL)
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
				mChallengeState = ChallengeState.STATECHALLENGE_BEGHOULED_NO_MATCHES;
				mChallengeStateCounter = 500;
				mBoard.DisplayAdviceAgain("[ADVICE_BEGHOULED_NO_MOVES]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_NO_MOVES);
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
				if (!mBoard.mZombieAllowed[(int)zombieType] && (!Board.IsZombieTypePoolOnly(zombieType) || mBoard.StageHasPool()) && (!mBoard.StageHasRoof() || (zombieType != ZombieType.ZOMBIE_DIGGER && zombieType != ZombieType.ZOMBIE_DANCER)) && (!mBoard.StageHasGraveStones() || zombieType != ZombieType.ZOMBIE_ZAMBONI) && (mBoard.StageHasRoof() || mApp.IsSurvivalEndless(mApp.mGameMode) || zombieType != ZombieType.ZOMBIE_BUNGEE) && (mBoard.GetSurvivalFlagsCompleted() >= 4 || (zombieType != ZombieType.ZOMBIE_GARGANTUAR && zombieType != ZombieType.ZOMBIE_ZAMBONI)) && (mBoard.GetSurvivalFlagsCompleted() >= 10 || zombieType != ZombieType.ZOMBIE_REDEYE_GARGANTUAR) && ((mApp.mGameMode != GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1 && mApp.mGameMode != GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_2 && mApp.mGameMode != GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_3) || zombieType <= ZombieType.ZOMBIE_SNORKEL) && zombieType != ZombieType.ZOMBIE_BOBSLED && zombieType != ZombieType.ZOMBIE_BACKUP_DANCER && zombieType != ZombieType.ZOMBIE_IMP && zombieType != ZombieType.ZOMBIE_DUCKY_TUBE && zombieType != ZombieType.ZOMBIE_PEA_HEAD && zombieType != ZombieType.ZOMBIE_WALLNUT_HEAD && zombieType != ZombieType.ZOMBIE_TALLNUT_HEAD && zombieType != ZombieType.ZOMBIE_JALAPENO_HEAD && zombieType != ZombieType.ZOMBIE_GATLING_HEAD && zombieType != ZombieType.ZOMBIE_SQUASH_HEAD && zombieType != ZombieType.ZOMBIE_YETI)
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
						ZombieType.ZOMBIE_NORMAL,
						ZombieType.ZOMBIE_TRAFFIC_CONE
					};
					InitZombieWavesFromList(array, array.Length);
				}
				else if (mSurvivalStage == 0)
				{
					ZombieType[] array2 = new ZombieType[]
					{
						ZombieType.ZOMBIE_NORMAL,
						ZombieType.ZOMBIE_TRAFFIC_CONE,
						ZombieType.ZOMBIE_PAIL
					};
					InitZombieWavesFromList(array2, array2.Length);
				}
				else
				{
					InitZombieWavesSurvival();
				}
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SPEED)
			{
				ZombieType[] array3 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_DOLPHIN_RIDER,
					ZombieType.ZOMBIE_POLEVAULTER
				};
				InitZombieWavesFromList(array3, array3.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_POGO_PARTY)
			{
				ZombieType[] array4 = new ZombieType[]
				{
					ZombieType.ZOMBIE_POGO
				};
				InitZombieWavesFromList(array4, array4.Length);
			}
			else if (mApp.IsBungeeBlitzLevel())
			{
				ZombieType[] array5 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_LADDER
				};
				InitZombieWavesFromList(array5, array5.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SUNNY_DAY)
			{
				ZombieType[] array6 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_FOOTBALL,
					ZombieType.ZOMBIE_POLEVAULTER,
					ZombieType.ZOMBIE_JACK_IN_THE_BOX
				};
				InitZombieWavesFromList(array6, array6.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
			{
				ZombieType[] array7 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_FOOTBALL,
					ZombieType.ZOMBIE_BALLOON
				};
				InitZombieWavesFromList(array7, array7.Length);
			}
			else if (mApp.IsLittleTroubleLevel())
			{
				ZombieType[] array8 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_FOOTBALL,
					ZombieType.ZOMBIE_SNORKEL
				};
				InitZombieWavesFromList(array8, array8.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BIG_TIME)
			{
				ZombieType[] array9 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_DOOR,
					ZombieType.ZOMBIE_FOOTBALL,
					ZombieType.ZOMBIE_JACK_IN_THE_BOX
				};
				InitZombieWavesFromList(array9, array9.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
			{
				ZombieType[] array10 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_DOOR,
					ZombieType.ZOMBIE_FOOTBALL,
					ZombieType.ZOMBIE_NEWSPAPER,
					ZombieType.ZOMBIE_JACK_IN_THE_BOX,
					ZombieType.ZOMBIE_BUNGEE
				};
				InitZombieWavesFromList(array10, array10.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY)
			{
				ZombieType[] array11 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_DOOR,
					ZombieType.ZOMBIE_BALLOON
				};
				InitZombieWavesFromList(array11, array11.Length);
			}
			else if (mApp.IsWhackAZombieLevel())
			{
				ZombieType[] array12 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL
				};
				InitZombieWavesFromList(array12, array12.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA)
			{
				ZombieType[] array13 = new ZombieType[]
				{
					ZombieType.ZOMBIE_BOBSLED,
					ZombieType.ZOMBIE_ZAMBONI
				};
				InitZombieWavesFromList(array13, array13.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_AIR_RAID)
			{
				ZombieType[] array14 = new ZombieType[]
				{
					ZombieType.ZOMBIE_BALLOON
				};
				InitZombieWavesFromList(array14, array14.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				ZombieType[] array15 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_DOOR,
					ZombieType.ZOMBIE_FOOTBALL,
					ZombieType.ZOMBIE_NEWSPAPER
				};
				InitZombieWavesFromList(array15, array15.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				ZombieType[] array16 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_DOOR,
					ZombieType.ZOMBIE_FOOTBALL,
					ZombieType.ZOMBIE_NEWSPAPER,
					ZombieType.ZOMBIE_JACK_IN_THE_BOX,
					ZombieType.ZOMBIE_POLEVAULTER,
					ZombieType.ZOMBIE_DOLPHIN_RIDER,
					ZombieType.ZOMBIE_LADDER
				};
				InitZombieWavesFromList(array16, array16.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				ZombieType[] array17 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_FOOTBALL
				};
				InitZombieWavesFromList(array17, array17.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
			{
				ZombieType[] array18 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_DOLPHIN_RIDER,
					ZombieType.ZOMBIE_ZAMBONI,
					ZombieType.ZOMBIE_JACK_IN_THE_BOX
				};
				InitZombieWavesFromList(array18, array18.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS)
			{
				ZombieType[] array19 = new ZombieType[]
				{
					ZombieType.ZOMBIE_PEA_HEAD,
					ZombieType.ZOMBIE_WALLNUT_HEAD
				};
				InitZombieWavesFromList(array19, array19.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS_2)
			{
				ZombieType[] array20 = new ZombieType[]
				{
					ZombieType.ZOMBIE_PEA_HEAD,
					ZombieType.ZOMBIE_WALLNUT_HEAD,
					ZombieType.ZOMBIE_JALAPENO_HEAD,
					ZombieType.ZOMBIE_GATLING_HEAD,
					ZombieType.ZOMBIE_SQUASH_HEAD,
					ZombieType.ZOMBIE_TALLNUT_HEAD
				};
				InitZombieWavesFromList(array20, array20.Length);
			}
			else if (mApp.IsShovelLevel())
			{
				ZombieType[] array21 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE
				};
				InitZombieWavesFromList(array21, array21.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING || mApp.IsAdventureMode() || mApp.IsQuickPlayMode())
			{
				ZombieType[] array22 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_POLEVAULTER,
					ZombieType.ZOMBIE_NEWSPAPER
				};
				InitZombieWavesFromList(array22, array22.Length);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING_2)
			{
				ZombieType[] array23 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_POLEVAULTER,
					ZombieType.ZOMBIE_NEWSPAPER,
					ZombieType.ZOMBIE_DANCER,
					ZombieType.ZOMBIE_DOOR
				};
				InitZombieWavesFromList(array23, array23.Length);
			}
			else if (mApp.IsStormyNightLevel())
			{
				ZombieType[] array24 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_DOLPHIN_RIDER,
					ZombieType.ZOMBIE_BALLOON
				};
				InitZombieWavesFromList(array24, array24.Length);
			}
			else
			{
				ZombieType[] array25 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL
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
				mBoard.DisplayAdvice("[ADVICE_ALMOST_THERE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_ALMOST_THERE);
			}
			if (num >= 2000)
			{
				SpawnLevelAward(4, 2);
				mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
			}
			mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 2000, num, 0, 150, TodCurves.CURVE_LINEAR);
			if (!mBoard.mAdvice.IsBeingDisplayed())
			{
				if (slotMachineMessageCached != 2000)
				{
					slotMachineMessage = TodCommon.TodReplaceNumberString("[ADVICE_SLOT_MACHINE_COLLECT_SUN]", "{SCORE}", 2000);
					slotMachineMessageCached = 2000;
				}
				mBoard.DisplayAdvice(slotMachineMessage, MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_SLOT_MACHINE_COLLECT_SUN);
			}
			if (mChallengeState != ChallengeState.STATECHALLENGE_SLOT_MACHINE_ROLLING)
			{
				if (!mBoard.mAdvice.IsBeingDisplayed() && !mBoard.HasLevelAwardDropped())
				{
					mBoard.DisplayAdviceAgain("[ADVICE_SLOT_MACHINE_SPIN_AGAIN]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_SLOT_MACHINE_SPIN_AGAIN);
				}
				return;
			}
			if (mBoard.mSeedBank.mSeedPackets[0].mSlotMachineCountDown > 0)
			{
				return;
			}
			Reanimation reanimation = mApp.ReanimationGet(mReanimChallenge);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_return, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
			mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
			SeedType aPacketType = mBoard.mSeedBank.mSeedPackets[0].mPacketType;
			SeedType aPacketType2 = mBoard.mSeedBank.mSeedPackets[1].mPacketType;
			SeedType aPacketType3 = mBoard.mSeedBank.mSeedPackets[2].mPacketType;
			if (aPacketType != aPacketType2 || aPacketType2 != aPacketType3)
			{
				if (aPacketType == aPacketType2 || aPacketType2 == aPacketType3 || aPacketType == aPacketType3)
				{
					mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
					SeedType seedType;
					if (aPacketType == aPacketType2 || aPacketType == aPacketType3)
					{
						seedType = aPacketType;
					}
					else
					{
						seedType = aPacketType2;
					}
					if (seedType == SeedType.SEED_SLOT_MACHINE_DIAMOND)
					{
						mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_2_DIAMONDS]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
						mBoard.AddCoin(360, 85, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
						return;
					}
					if (seedType == SeedType.SEED_SLOT_MACHINE_SUN)
					{
						mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_2_SUNS]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
						int num2 = 4;
						for (int i = 0; i < num2; i++)
						{
							int theX = 320 + i * 60 / num2;
							mBoard.AddCoin(theX, 85, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
						}
						return;
					}
					mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_2_OF_A_KIND]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
					Coin coin = mBoard.AddCoin(360, 85, CoinType.COIN_USABLE_SEED_PACKET, CoinMotion.COIN_MOTION_COIN);
					coin.mUsableSeedType = seedType;
				}
				return;
			}
			mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
			if (aPacketType == SeedType.SEED_SLOT_MACHINE_DIAMOND)
			{
				mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_DIAMOND_JACKPOT]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
				int num3 = 5;
				for (int j = 0; j < num3; j++)
				{
					int theX2 = 320 + j * 60 / num3;
					mBoard.AddCoin(theX2, 85, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
				}
				return;
			}
			if (aPacketType == SeedType.SEED_SLOT_MACHINE_SUN)
			{
				mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_SUN_JACKPOT]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
				int num4 = 20;
				for (int k = 0; k < num4; k++)
				{
					int theX3 = 320 + k * 60 / num4;
					mBoard.AddCoin(theX3, 85, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
				}
				return;
			}
			mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_3_OF_A_KIND]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
			int num5 = 3;
			for (int l = 0; l < num5; l++)
			{
				int theX4 = 320 + l * 60 / num5;
				Coin coin2 = mBoard.AddCoin(theX4, 85, CoinType.COIN_USABLE_SEED_PACKET, CoinMotion.COIN_MOTION_COIN);
				coin2.mUsableSeedType = aPacketType;
			}
		}

		public void DrawSlotMachine(Graphics g)
		{
			if (mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
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
			if (mSlotMachineRollCount < 3 && mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL && mChallengeState != ChallengeState.STATECHALLENGE_SLOT_MACHINE_ROLLING && !mBoard.HasLevelAwardDropped())
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

		public void WhackAZombieSpawning()
		{
			if (mBoard.mCurrentWave == mBoard.mNumWaves && mBoard.mZombieCountDown == 0)
			{
				return;
			}
			mBoard.mZombieCountDown -= 3;
			int num = 300;
			if (mBoard.mZombieCountDown >= 100 && mBoard.mZombieCountDown < 103 && mBoard.mCurrentWave > 0)
			{
				int graveStoneCount = mBoard.GetGraveStoneCount();
				int num2 = 5;
				int theGraveCount = Math.Max(1, num2 - graveStoneCount);
				WhackAZombiePlaceGraves(theGraveCount);
			}
			if (mBoard.mZombieCountDown >= 5 && mBoard.mZombieCountDown < 8)
			{
				mBoard.NextWaveComing();
			}
			if (mBoard.mZombieCountDown >= 0 && mBoard.mZombieCountDown < 3)
			{
				mBoard.mZombieCountDown = 2000;
				mBoard.mZombieCountDownStart = mBoard.mZombieCountDown;
				mBoard.mCurrentWave++;
				if (mBoard.mCurrentWave == mBoard.mNumWaves)
				{
					mChallengeStateCounter = 300;
				}
				else
				{
					mChallengeStateCounter = 3;
				}
			}
			else if (mBoard.mZombieCountDown < num)
			{
				return;
			}
			mChallengeStateCounter -= 3;
			if (mChallengeStateCounter < 0 || mChallengeStateCounter >= 3)
			{
				return;
			}
			int num3 = TodCommon.ClampInt((mBoard.mCurrentWave - 1) * 6 / 12, 0, 5);
			int num4 = 1;
			ZombieType theZombieType = ZombieType.ZOMBIE_NORMAL;
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
				theZombieType = ZombieType.ZOMBIE_PAIL;
			}
			else if (num6 < aPailChance[num3] + aConeChance[num3])
			{
				theZombieType = ZombieType.ZOMBIE_TRAFFIC_CONE;
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
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_GRAVESTONE)
				{
					Plant topPlantAt = mBoard.GetTopPlantAt(gridItem.mGridX, gridItem.mGridY, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
					if (topPlantAt == null || topPlantAt.mSeedType != SeedType.SEED_GRAVEBUSTER)
					{
						Challenge.aGridPickItemArray[num7] = gridItem;
						Challenge.aGridPicks[num7].mItem = num7;
						Challenge.aGridPicks[num7].mWeight = 1;
						num7++;
					}
				}
			}
			float theMax = TodCommon.TodAnimateCurve(1, 12, mBoard.mCurrentWave, 1, 3, TodCurves.CURVE_EASE_IN);
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
						ZombieType.ZOMBIE_TRAFFIC_CONE,
						ZombieType.ZOMBIE_PAIL
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
			int theMin = TodCommon.TodAnimateCurve(1, 12, mBoard.mCurrentWave, 100, 30, TodCurves.CURVE_LINEAR);
			int theMax2 = TodCommon.TodAnimateCurve(1, 12, mBoard.mCurrentWave, 200, 60, TodCurves.CURVE_LINEAR);
			mChallengeStateCounter = TodCommon.RandRangeInt(theMin, theMax2);
			if (flag)
			{
				mBoard.mZombieCountDown = 0;
				mChallengeStateCounter = 0;
			}
		}

		public bool UpdateZombieSpawning()
		{
			if (mApp.IsWhackAZombieLevel())
			{
				WhackAZombieSpawning();
				return true;
			}
			return mApp.IsFinalBossLevel() || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM || mApp.IsIZombieLevel() || mApp.IsSquirrelLevel() || mApp.IsScaryPotterLevel() || (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && mChallengeState != ChallengeState.STATECHALLENGE_LAST_STAND_ONSLAUGHT);
		}

		public void BeghouledClearCrater(int theCount)
		{
			mBoard.ClearAdvice(AdviceType.ADVICE_BEGHOULED_USE_CRATER_1);
			mBoard.ClearAdvice(AdviceType.ADVICE_BEGHOULED_USE_CRATER_2);
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
			mApp.PlayFoley(FoleyType.FOLEY_SWING);
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
				if (zombie.mHelmType != HelmType.HELMTYPE_NONE)
				{
					if (zombie.mHelmType == HelmType.HELMTYPE_PAIL)
					{
						mApp.PlayFoley(FoleyType.FOLEY_SHIELD_HIT);
					}
					else if (zombie.mHelmType == HelmType.HELMTYPE_TRAFFIC_CONE)
					{
						mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
					}
					zombie.TakeHelmDamage(900, 0U);
					return;
				}
				mApp.PlayFoley(FoleyType.FOLEY_BONK);
				mApp.AddTodParticle(TodCommon.PixelAligned(x - 3f), TodCommon.PixelAligned(y + 9f), 800000, ParticleEffect.PARTICLE_POW);
				zombie.DieWithLoot();
				mBoard.ClearCursor();
			}
		}

		public bool IsStormyNightPitchBlack()
		{
			return mApp.IsStormyNightLevel() && (mChallengeState != ChallengeState.STATECHALLENGE_STORM_FLASH_1 || mChallengeStateCounter >= 300) && (mChallengeState != ChallengeState.STATECHALLENGE_STORM_FLASH_2 || mChallengeStateCounter >= 300) && (mChallengeState != ChallengeState.STATECHALLENGE_STORM_FLASH_3 || mChallengeStateCounter >= 150);
		}

		public void DrawStormNight(Graphics g)
		{
			if (mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_1 && mChallengeStateCounter < 300)
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
			else if (mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_2 && mChallengeStateCounter < 300)
			{
				DrawStormFlash(g, mChallengeStateCounter / 2, 255);
			}
			else if (mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_3 && mChallengeStateCounter < 150)
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
				if (mChallengeStateCounter == 150 && mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_1)
				{
					mChallengeStateCounter = 1;
					return;
				}
			}
			mChallengeStateCounter--;
			if ((mChallengeStateCounter == 300 && mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_1) || (mChallengeStateCounter == 150 && mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_1) || (mChallengeStateCounter == 300 && mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_2) || (mChallengeStateCounter == 150 && mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_3))
			{
				mApp.PlayFoley(FoleyType.FOLEY_THUNDER);
			}
			if (mChallengeStateCounter <= 0)
			{
				if (mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
				{
					mChallengeStateCounter = 150 + TodCommon.RandRangeInt(-50, 50);
					mChallengeState = ChallengeState.STATECHALLENGE_STORM_FLASH_3;
					return;
				}
				if (mApp.mGameScene != GameScenes.SCENE_PLAYING)
				{
					mChallengeStateCounter = 0;
					mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
					return;
				}
				int count = mBoard.mZombies.Count;
				for (int i = 0; i < count; i++)
				{
					Zombie zombie = mBoard.mZombies[i];
					if (!zombie.mDead && zombie.mZombieType == ZombieType.ZOMBIE_YETI)
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
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
			{
				mChallengeStateCounter = 100;
				mApp.PlayFoley(FoleyType.FOLEY_RAIN);
			}
			if (mApp.IsStormyNightLevel())
			{
				mChallengeState = ChallengeState.STATECHALLENGE_STORM_FLASH_2;
				mChallengeStateCounter = 150;
				mApp.PlayFoley(FoleyType.FOLEY_RAIN);
			}
			if (mApp.IsFinalBossLevel())
			{
				mBoard.mSeedBank.AddSeed(SeedType.SEED_CABBAGEPULT);
				mBoard.mSeedBank.AddSeed(SeedType.SEED_JALAPENO);
				mBoard.mSeedBank.AddSeed(SeedType.SEED_CABBAGEPULT);
				mBoard.mSeedBank.AddSeed(SeedType.SEED_ICESHROOM);
				mConveyorBeltCounter = 1000;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				mApp.mZenGarden.mGardenType = GardenType.GARDEN_MAIN;
				mApp.mZenGarden.ZenGardenInitLevel(false);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				mBoard.mSeedBank.AddSeed(SeedType.SEED_POTATOMINE);
				mBoard.mSeedBank.AddSeed(SeedType.SEED_TALLNUT);
				mBoard.mSeedBank.AddSeed(SeedType.SEED_MELONPULT);
				mBoard.mSeedBank.AddSeed(SeedType.SEED_MAGNETSHROOM);
				mBoard.mSeedBank.AddSeed(SeedType.SEED_INSTANT_COFFEE);
				mBoard.mSeedBank.AddSeed(SeedType.SEED_MELONPULT);
				mConveyorBeltCounter = 1000;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
			{
				mBoard.mSeedBank.AddSeed(SeedType.SEED_PEASHOOTER);
				mBoard.mSeedBank.AddSeed(SeedType.SEED_ICESHROOM);
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
				mBoard.NewPlant(5, 1, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
				mBoard.NewPlant(7, 2, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
				mBoard.NewPlant(6, 3, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
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
					if (zombieType == ZombieType.ZOMBIE_INVALID)
					{
						break;
					}
					if (zombieType == ZombieType.ZOMBIE_FLAG)
					{
						mBoard.mZombiesInWave[mBoard.mCurrentWave, i] = ZombieType.ZOMBIE_NORMAL;
					}
				}
			}
			bool flag = mBoard.IsFlagWave(mBoard.mCurrentWave);
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_GRAVE_DANGER && mBoard.mCurrentWave != mBoard.mNumWaves - 1)
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
			if (mApp.IsSurvivalMode() && mBoard.mBackground == BackgroundType.BACKGROUND_2_NIGHT && mBoard.mCurrentWave == mBoard.mNumWaves - 1)
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
				mBoard.DisplayAdvice("[ADVICE_BUNGEES_INCOMING]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
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
						Plant topPlantAt = mBoard.GetTopPlantAt(i, j, PlantPriority.TOPPLANT_ANY);
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
				theCoinType = CoinType.COIN_FINAL_SEED_PACKET;
			}
			else if (mApp.IsAdventureMode() || mApp.HasBeatenChallenge(mApp.mGameMode))
			{
				theCoinType = CoinType.COIN_AWARD_MONEY_BAG;
			}
			else
			{
				theCoinType = CoinType.COIN_TROPHY;
			}
			mBoard.mLevelAwardSpawned = true;
			mApp.mBoardResult = BoardResult.BOARDRESULT_WON;
			mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
			mBoard.AddCoin((int)num, (int)num2, theCoinType, CoinMotion.COIN_MOTION_COIN);
			mApp.AddTodParticle(400f, 300f, 400000, ParticleEffect.PARTICLE_SCREEN_FLASH);
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
			mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
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
				mBoard.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_REPEATER, SeedType.SEED_NONE);
				mBoard.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_FUMESHROOM, SeedType.SEED_NONE);
				mBoard.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_TALLNUT, SeedType.SEED_NONE);
				mBoard.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.SEED_BEGHOULED_BUTTON_SHUFFLE, SeedType.SEED_NONE);
				mBoard.mSeedBank.mNumPackets = 4;
				mBoard.DisplayAdvice("[ADVICE_BEGHOULED_SAVE_SUN]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_SAVE_SUN);
				if (BeghouledCanClearCrater())
				{
					mBoard.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.SEED_BEGHOULED_BUTTON_CRATER, SeedType.SEED_NONE);
					mBoard.mSeedBank.mNumPackets = 5;
				}
			}
			else
			{
				if (!mBoard.mAdvice.IsBeingDisplayed())
				{
					string theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_BEGHOULED_MATCH_3]", "{SCORE}", 75);
					mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_MATCH_3);
				}
				if (mChallengeScore >= 70)
				{
					mBoard.DisplayAdvice("[ADVICE_BEGHOULED_ALMOST_THERE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_ALMOST_THERE);
				}
			}
			if (mChallengeScore >= 75)
			{
				mChallengeScore = 75;
				SpawnLevelAward(x, y);
				mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
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
					mBoard.AddCoin((int)(num - 10f + 20f * i), (int)num2, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
				}
			}
			mBeghouledMatchesThisMove++;
		}

		public void DrawStormFlash(Graphics g, int theTime, int theMaxAmount)
		{
			RandomNumbers.Seed(mBoard.mMainCounter / 6);
			int num = TodCommon.TodAnimateCurve(150, 0, theTime, 255 - theMaxAmount, 255, TodCurves.CURVE_LINEAR);
			int theAlpha = TodCommon.ClampInt((int)(num + RandomNumbers.NextNumber(64f) - 32f), 0, 255);
			g.SetColor(new SexyColor(0, 0, 0, theAlpha));
			g.FillRect(-1000, -1000, Constants.BOARD_WIDTH + 2000, Constants.BOARD_HEIGHT + 2000);
			int theAlpha2 = TodCommon.TodAnimateCurve(150, 75, theTime, theMaxAmount, 0, TodCurves.CURVE_LINEAR);
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
			Coin coin = mBoard.AddCoin(theX, 60, CoinType.COIN_USABLE_SEED_PACKET, CoinMotion.COIN_MOTION_FROM_SKY_SLOW);
			do
			{
				coin.mUsableSeedType = (SeedType)RandomNumbers.NextNumber(mApp.GetSeedsAvailable());
			}
			while (mBoard.SeedNotRecommendedForLevel(coin.mUsableSeedType) != 0U || !mApp.HasSeedType(coin.mUsableSeedType) || Plant.IsUpgrade(coin.mUsableSeedType) || coin.mUsableSeedType == SeedType.SEED_SUNFLOWER || coin.mUsableSeedType == SeedType.SEED_TWINSUNFLOWER || coin.mUsableSeedType == SeedType.SEED_INSTANT_COFFEE || coin.mUsableSeedType == SeedType.SEED_UMBRELLA || coin.mUsableSeedType == SeedType.SEED_SUNSHROOM || coin.mUsableSeedType == SeedType.SEED_IMITATER);
			int theTimeAge = mBoard.CountPlantByType(SeedType.SEED_LILYPAD);
			int theTimeEnd = 18;
			int thePositionStart = 30;
			int num = TodCommon.TodAnimateCurve(0, theTimeEnd, theTimeAge, thePositionStart, 1, TodCurves.CURVE_LINEAR);
			if (RandomNumbers.NextNumber(100) < num)
			{
				coin.mUsableSeedType = SeedType.SEED_LILYPAD;
			}
		}

		public void PlayBossEnter()
		{
			mBoard.AddZombie(ZombieType.ZOMBIE_BOSS, 0);
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
			else if (mApp.IsShovelLevel() || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
			{
				num = 1.5f;
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
			{
				num = 2f;
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
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
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING_2)
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
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
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
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
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
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
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
				if (seedType != SeedType.SEED_GRAVEBUSTER)
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
				if (seedType == SeedType.SEED_LILYPAD)
				{
					int num5 = mBoard.CountPlantByType(seedType);
					int theTimeEnd = 18;
					todWeightedArray.mWeight = TodCommon.TodAnimateCurve(0, theTimeEnd, num5 + num3, todWeightedArray.mWeight, 1, TodCurves.CURVE_LINEAR);
				}
				if (seedType == SeedType.SEED_FLOWERPOT)
				{
					int num6 = mBoard.CountPlantByType(seedType);
					int theTimeEnd2 = 35;
					if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
					{
						theTimeEnd2 = 45;
					}
					todWeightedArray.mWeight = TodCommon.TodAnimateCurve(0, theTimeEnd2, num6 + num3, todWeightedArray.mWeight, 1, TodCurves.CURVE_LINEAR);
				}
				if (mApp.IsFinalBossLevel())
				{
					if (seedType == SeedType.SEED_MELONPULT || seedType == SeedType.SEED_KERNELPULT || seedType == SeedType.SEED_CABBAGEPULT)
					{
						int num7 = mBoard.CountEmptyPotsOrLilies(SeedType.SEED_FLOWERPOT);
						if (num7 <= 2)
						{
							todWeightedArray.mWeight /= 5;
						}
						else if (num7 <= 5)
						{
							todWeightedArray.mWeight /= 3;
						}
					}
					if (seedType == SeedType.SEED_FLOWERPOT)
					{
						Zombie bossZombie = mBoard.GetBossZombie();
						if (bossZombie.mZombiePhase == ZombiePhase.PHASE_BOSS_DROP_RV)
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
			newGridItem.mGridItemType = GridItemType.GRIDITEM_PORTAL_SQUARE;
			newGridItem.mGridX = 2;
			newGridItem.mGridY = 0;
			newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, newGridItem.mGridY, 0);
			newGridItem.OpenPortal();
			mBoard.mGridItems.Add(newGridItem);
			GridItem newGridItem2 = GridItem.GetNewGridItem();
			newGridItem2.mGridItemType = GridItemType.GRIDITEM_PORTAL_SQUARE;
			newGridItem2.mGridX = 9;
			newGridItem2.mGridY = 1;
			newGridItem2.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, newGridItem2.mGridY, 0);
			newGridItem2.OpenPortal();
			mBoard.mGridItems.Add(newGridItem2);
			GridItem newGridItem3 = GridItem.GetNewGridItem();
			newGridItem3.mGridItemType = GridItemType.GRIDITEM_PORTAL_CIRCLE;
			newGridItem3.mGridX = 9;
			newGridItem3.mGridY = 3;
			newGridItem3.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, newGridItem3.mGridY, 0);
			newGridItem3.OpenPortal();
			mBoard.mGridItems.Add(newGridItem3);
			GridItem newGridItem4 = GridItem.GetNewGridItem();
			newGridItem4.mGridItemType = GridItemType.GRIDITEM_PORTAL_CIRCLE;
			newGridItem4.mGridX = 2;
			newGridItem4.mGridY = 4;
			newGridItem4.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, newGridItem4.mGridY, 0);
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
				if (gridItem.mGridItemState != GridItemState.GRIDITEM_STATE_PORTAL_CLOSED && (gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_CIRCLE || gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE))
				{
					UpdatePortal(gridItem);
				}
			}
			if (mBoard.HasLevelAwardDropped())
			{
				mBoard.ClearAdvice(AdviceType.ADVICE_PORTAL_RELOCATING);
				return;
			}
			mChallengeStateCounter--;
			if (mChallengeStateCounter == 500)
			{
				mBoard.DisplayAdviceAgain("[ADVICE_PORTAL_RELOCATING]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PORTAL_RELOCATING);
			}
			if (mChallengeStateCounter <= 0)
			{
				mBoard.ClearAdvice(AdviceType.ADVICE_PORTAL_RELOCATING);
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
				if (thePortal != gridItem && gridItem.mGridItemState != GridItemState.GRIDITEM_STATE_PORTAL_CLOSED && thePortal.mGridItemType == gridItem.mGridItemType)
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
				if (projectile.mMotionType == ProjectileMotion.MOTION_STRAIGHT && projectile.mRow == thePortal.mGridY && projectile.mLastPortalX != thePortal.mGridX)
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
							projectile.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, projectile.mRow, 0);
						}
					}
				}
			}
			LawnMower lawnMower = null;
			while (mBoard.IterateLawnMowers(ref lawnMower))
			{
				if (lawnMower.mMowerState == LawnMowerState.MOWER_TRIGGERED && lawnMower.mRow == thePortal.mGridY && lawnMower.mLastPortalX != thePortal.mGridX)
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
							lawnMower.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_LAWN_MOWER, lawnMower.mRow, 0);
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
				if (gridItem.mGridItemState != GridItemState.GRIDITEM_STATE_PORTAL_CLOSED && (gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_CIRCLE || gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE) && gridItem.mGridY == theGridY)
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
				if (gridItem2.mGridItemState != GridItemState.GRIDITEM_STATE_PORTAL_CLOSED && (gridItem2.mGridItemType == GridItemType.GRIDITEM_PORTAL_CIRCLE || gridItem2.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE) && gridItem2.mGridX - 1 >= theGridX && gridItem2.mGridY == theGridY && (gridItem == null || gridItem.mGridX >= gridItem2.mGridX))
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
				if (gridItem.mGridX == theGridX && gridItem.mGridY == theGridY && gridItem.mGridItemState != GridItemState.GRIDITEM_STATE_PORTAL_CLOSED && (gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_CIRCLE || gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE))
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
				if (gridItem.mGridItemState != GridItemState.GRIDITEM_STATE_PORTAL_CLOSED && (gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_CIRCLE || gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE))
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
			newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, newGridItem.mGridY, 0);
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
				if (gridItem2.mGridItemState != GridItemState.GRIDITEM_STATE_PORTAL_CLOSED && (gridItem2.mGridItemType == GridItemType.GRIDITEM_PORTAL_CIRCLE || gridItem2.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE) && gridItem2.mGridX < theGridX && gridItem2.mGridY == theGridY && (gridItem == null || gridItem.mGridX <= gridItem2.mGridX))
				{
					gridItem = gridItem2;
				}
			}
			return gridItem;
		}

		public void BeghouledPacketClicked(SeedPacket theSeedPacket)
		{
			int currentPlantCost = mBoard.GetCurrentPlantCost(theSeedPacket.mPacketType, SeedType.SEED_NONE);
			if (!mBoard.CanTakeSunMoney(currentPlantCost))
			{
				return;
			}
			if (theSeedPacket.mPacketType == SeedType.SEED_REPEATER && !mBoard.mChallenge.mBeghouledPurcasedUpgrade[0])
			{
				mBoard.mChallenge.mBeghouledPurcasedUpgrade[0] = true;
				int count = mBoard.mPlants.Count;
				for (int i = 0; i < count; i++)
				{
					Plant plant = mBoard.mPlants[i];
					if (!plant.mDead && plant.mSeedType == SeedType.SEED_PEASHOOTER)
					{
						plant.Die();
						mBoard.AddPlant(plant.mPlantCol, plant.mRow, SeedType.SEED_REPEATER, SeedType.SEED_NONE);
					}
				}
				theSeedPacket.Deactivate();
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_FUMESHROOM && !mBoard.mChallenge.mBeghouledPurcasedUpgrade[1])
			{
				mBoard.mChallenge.mBeghouledPurcasedUpgrade[1] = true;
				int count2 = mBoard.mPlants.Count;
				for (int j = 0; j < count2; j++)
				{
					Plant plant2 = mBoard.mPlants[j];
					if (!plant2.mDead && plant2.mSeedType == SeedType.SEED_PUFFSHROOM)
					{
						plant2.Die();
						mBoard.AddPlant(plant2.mPlantCol, plant2.mRow, SeedType.SEED_FUMESHROOM, SeedType.SEED_NONE);
					}
				}
				theSeedPacket.Deactivate();
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_TALLNUT && !mBoard.mChallenge.mBeghouledPurcasedUpgrade[2])
			{
				mBoard.mChallenge.mBeghouledPurcasedUpgrade[2] = true;
				int count3 = mBoard.mPlants.Count;
				for (int k = 0; k < count3; k++)
				{
					Plant plant3 = mBoard.mPlants[k];
					if (!plant3.mDead && plant3.mSeedType == SeedType.SEED_WALLNUT)
					{
						plant3.Die();
						mBoard.AddPlant(plant3.mPlantCol, plant3.mRow, SeedType.SEED_TALLNUT, SeedType.SEED_NONE);
					}
				}
				theSeedPacket.Deactivate();
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_BEGHOULED_BUTTON_SHUFFLE)
			{
				if (mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_FALLING || mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_MOVING)
				{
					return;
				}
				BeghouledShuffle();
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_BEGHOULED_BUTTON_CRATER)
			{
				if (!BeghouledCanClearCrater())
				{
					return;
				}
				if (mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_FALLING || mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_MOVING)
				{
					return;
				}
				BeghouledClearCrater(1);
				BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_FALLING);
			}
			mBoard.TakeSunMoney(currentPlantCost);
		}

		public void BeghouledShuffle()
		{
			mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
			int count = mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = mBoard.mPlants[i];
				if (!plant.mDead)
				{
					plant.Die();
				}
			}
			BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_FALLING);
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
			Debug.ASSERT(seedPacket.mPacketType == SeedType.SEED_BEGHOULED_BUTTON_CRATER);
			if (BeghouledCanClearCrater())
			{
				seedPacket.Activate();
				return;
			}
			seedPacket.Deactivate();
		}

		public Zombie ZombiquariumSpawnSnorkel()
		{
			Zombie zombie = mBoard.AddZombieInRow(ZombieType.ZOMBIE_SNORKEL, 0, 0);
			zombie.mPosX = TodCommon.RandRangeFloat(50f, 650f);
			zombie.mPosY = TodCommon.RandRangeFloat(100f, 400f);
			return zombie;
		}

		public void ZombiquariumPacketClicked(SeedPacket theSeedPacket)
		{
			int currentPlantCost = mBoard.GetCurrentPlantCost(theSeedPacket.mPacketType, SeedType.SEED_NONE);
			if (!mBoard.CanTakeSunMoney(currentPlantCost))
			{
				return;
			}
			if (theSeedPacket.mPacketType == SeedType.SEED_ZOMBIQUARIUM_SNORKEL)
			{
				int num = mBoard.CountZombiesOnScreen();
				if (num > 100)
				{
					return;
				}
				if (mBoard.mTutorialState == TutorialState.TUTORIAL_ZOMBIQUARIUM_BUY_SNORKEL)
				{
					mBoard.ClearAdvice(AdviceType.ADVICE_ZOMBIQUARIUM_BUY_SNORKEL);
					mBoard.TutorialArrowRemove();
					mBoard.mTutorialState = TutorialState.TUTORIAL_ZOMBIQUARIUM_BOUGHT_SNORKEL;
				}
				Zombie zombie = ZombiquariumSpawnSnorkel();
				mApp.PlayFoley(FoleyType.FOLEY_ZOMBIESPLASH);
				mApp.AddTodParticle(zombie.mPosX + 60f, zombie.mPosY + 20f, 400000, ParticleEffect.PARTICLE_PLANTING_POOL);
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_ZOMBIQUARIUM_TROPHY)
			{
				SpawnLevelAward(2, 0);
				mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
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
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_BRAIN)
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
			mBoard.ClearAdvice(AdviceType.ADVICE_ZOMBIQUARIUM_CLICK_TO_FEED);
			GridItem newGridItem = GridItem.GetNewGridItem();
			newGridItem.mGridItemType = GridItemType.GRIDITEM_BRAIN;
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
				mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_ZOMBIQUARIUM_COLLECT_SUN);
			}
			int num = TodCommon.ClampInt(mBoard.mSunMoney, 0, 1000);
			mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 1000, num, 0, 150, TodCurves.CURVE_LINEAR);
			if (num >= 900)
			{
				mBoard.DisplayAdvice("[ADVICE_ALMOST_THERE]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_ALMOST_THERE);
			}
			if (num >= 110 && mBoard.mTutorialState == TutorialState.TUTORIAL_OFF)
			{
				mBoard.mTutorialState = TutorialState.TUTORIAL_ZOMBIQUARIUM_BUY_SNORKEL;
				float num2 = mBoard.mSeedBank.mX + mBoard.mSeedBank.mSeedPackets[0].mX;
				float num3 = mBoard.mSeedBank.mY + mBoard.mSeedBank.mSeedPackets[0].mY;
				mBoard.TutorialArrowShow((int)num2, (int)num3);
				mBoard.DisplayAdvice("[ADVICE_ZOMBIQUARIUM_BUY_SNORKEL]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_ZOMBIQUARIUM_BUY_SNORKEL);
			}
			else if (num < 100 && mBoard.mTutorialState == TutorialState.TUTORIAL_ZOMBIQUARIUM_BUY_SNORKEL)
			{
				mBoard.TutorialArrowRemove();
				mBoard.ClearAdvice(AdviceType.ADVICE_ZOMBIQUARIUM_BUY_SNORKEL);
				mBoard.mTutorialState = TutorialState.TUTORIAL_OFF;
			}
			if (num >= 1000 && mBoard.mTutorialState == TutorialState.TUTORIAL_ZOMBIQUARIUM_BOUGHT_SNORKEL)
			{
				mBoard.mTutorialState = TutorialState.TUTORIAL_ZOMBIQUARIUM_CLICK_TROPHY;
				float num4 = mBoard.mSeedBank.mX + mBoard.mSeedBank.mSeedPackets[1].mX;
				float num5 = mBoard.mSeedBank.mY + mBoard.mSeedBank.mSeedPackets[1].mY;
				mBoard.TutorialArrowShow((int)num4, (int)num5);
				mBoard.DisplayAdvice("[ADVICE_ZOMBIQUARIUM_CLICK_TROPHY]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_ZOMBIQUARIUM_CLICK_TROPHY);
			}
			else if (num < 1000 && mBoard.mTutorialState == TutorialState.TUTORIAL_ZOMBIQUARIUM_CLICK_TROPHY)
			{
				mBoard.TutorialArrowRemove();
				mBoard.ClearAdvice(AdviceType.ADVICE_ZOMBIQUARIUM_CLICK_TROPHY);
				mBoard.mTutorialState = TutorialState.TUTORIAL_ZOMBIQUARIUM_BOUGHT_SNORKEL;
			}
			int num6 = -1;
			GridItem gridItem = null;
			while (mBoard.IterateGridItems(ref gridItem, ref num6))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_BRAIN)
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
					mBoard.AddPlant(i, j, SeedType.SEED_WALLNUT, SeedType.SEED_NONE);
				}
			}
		}

		public void ScaryPotterPlacePot(ScaryPotType theScaryPotType, ZombieType theZombieType, SeedType theSeedType, int theCount, TodWeightedGridArray[] theGridArray, int theGridArrayCount)
		{
			Debug.ASSERT(theScaryPotType != ScaryPotType.SCARYPOT_SEED || theSeedType != SeedType.SEED_NONE);
			Debug.ASSERT(theScaryPotType != ScaryPotType.SCARYPOT_ZOMBIE || theZombieType != ZombieType.ZOMBIE_INVALID);
			for (int i = 0; i < theCount; i++)
			{
				TodWeightedGridArray todWeightedGridArray = TodCommon.TodPickFromWeightedGridArray(theGridArray, theGridArrayCount);
				todWeightedGridArray.mWeight = 0;
				GridItem newGridItem = GridItem.GetNewGridItem();
				newGridItem.mGridItemType = GridItemType.GRIDITEM_SCARY_POT;
				newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_SCARY_POT_QUESTION;
				newGridItem.mGridX = todWeightedGridArray.mX;
				newGridItem.mGridY = todWeightedGridArray.mY;
				newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PLANT, newGridItem.mGridY, 0);
				newGridItem.mSeedType = theSeedType;
				newGridItem.mZombieType = theZombieType;
				newGridItem.mScaryPotType = theScaryPotType;
				mBoard.mGridItems.Add(newGridItem);
				if (theScaryPotType == ScaryPotType.SCARYPOT_SUN)
				{
					newGridItem.mSunCount = TodCommon.RandRangeInt(1, 3);
				}
			}
		}

		public void ScaryPotterStart()
		{
			if (mApp.IsAdventureMode())
			{
				mBoard.DisplayAdvice("[ADVICE_USE_SHOVEL_ON_POTS]", MessageStyle.MESSAGE_STYLE_HINT_STAY, AdviceType.ADVICE_USE_SHOVEL_ON_POTS);
			}
		}

		public void ScaryPotterUpdate()
		{
			if (mChallengeState == ChallengeState.STATECHALLENGE_SCARY_POTTER_MALLETING)
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
					mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
				}
			}
		}

		public void ScaryPotterOpenPot(GridItem theScaryPot)
		{
			int num = mBoard.GridToPixelX(theScaryPot.mGridX, theScaryPot.mGridY);
			int num2 = mBoard.GridToPixelY(theScaryPot.mGridX, theScaryPot.mGridY);
			switch (theScaryPot.mScaryPotType)
			{
			case ScaryPotType.SCARYPOT_SEED:
			{
				Coin coin = mBoard.AddCoin(num + 20, num2, CoinType.COIN_USABLE_SEED_PACKET, CoinMotion.COIN_MOTION_FROM_PLANT);
				coin.mUsableSeedType = theScaryPot.mSeedType;
				break;
			}
			case ScaryPotType.SCARYPOT_ZOMBIE:
			{
				Zombie zombie = mBoard.AddZombieInRow(theScaryPot.mZombieType, theScaryPot.mGridY, 0);
				zombie.mPosX = num;
				break;
			}
			case ScaryPotType.SCARYPOT_SUN:
			{
				int num3 = ScaryPotterCountSunInPot(theScaryPot);
				for (int i = 0; i < num3; i++)
				{
					mBoard.AddCoin(num, num2, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
					num += 15;
				}
				break;
			}
			default:
				Debug.ASSERT(false);
				break;
			}
			theScaryPot.GridItemDie();
			if (mBoard.mHelpIndex == AdviceType.ADVICE_USE_SHOVEL_ON_POTS)
			{
				mBoard.DisplayAdvice("[ADVICE_DESTROY_POTS_TO_FINISH_LEVEL]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_DESTROY_POTS_TO_FINISIH_LEVEL);
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
			mApp.PlayFoley(FoleyType.FOLEY_VASE_BREAKING);
			if (theScaryPot.mGridItemState == GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF)
			{
				mApp.AddTodParticle(num + 20, num2, 200000, ParticleEffect.PARTICLE_VASE_SHATTER_LEAF);
				return;
			}
			if (theScaryPot.mGridItemState == GridItemState.GRIDITEM_STATE_SCARY_POT_ZOMBIE)
			{
				mApp.AddTodParticle(num + 20, num2, 200000, ParticleEffect.PARTICLE_VASE_SHATTER_ZOMBIE);
				return;
			}
			mApp.AddTodParticle(num + 20, num2, 200000, ParticleEffect.PARTICLE_VASE_SHATTER);
		}

		public void ScaryPotterJackExplode(int aPosX, int aPosY)
		{
			int num = mBoard.PixelToGridX(aPosX, aPosY);
			int num2 = mBoard.PixelToGridY(aPosX, aPosY);
			int num3 = -1;
			GridItem gridItem = null;
			while (mBoard.IterateGridItems(ref gridItem, ref num3))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_SCARY_POT && gridItem.mGridX >= num - 1 && gridItem.mGridX <= num + 1 && gridItem.mGridY >= num2 - 1 && gridItem.mGridY <= num2 + 1)
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
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_SCARY_POT)
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
				if (gridItem.mGridItemState == GridItemState.GRIDITEM_STATE_SCARY_POT_QUESTION && (thePotType != GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF || gridItem.mScaryPotType == ScaryPotType.SCARYPOT_SEED) && (thePotType != GridItemState.GRIDITEM_STATE_SCARY_POT_ZOMBIE || gridItem.mZombieType == ZombieType.ZOMBIE_GARGANTUAR))
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
			if ((mApp.IsAdventureMode() && mBoard.mLevel == 35) || mApp.mGameMode == GameMode.GAMEMODE_QUICKPLAY_35)
			{
				if (mSurvivalStage == 0)
				{
					ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(4, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(5, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 5, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 4, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				}
				else if (mSurvivalStage == 1)
				{
					ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(4, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 4, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 5, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 4, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_FOOTBALL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
					ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
				}
				else if (mSurvivalStage == 2)
				{
					ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
					ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 5, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 5, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_HYPNOSHROOM, 5, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 6, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 2, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_DANCER, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
					ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
					ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 3);
				}
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_1)
			{
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 6, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_2)
			{
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(8, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 7, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_POTATOMINE, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 6, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_3)
			{
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 6, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 4, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_HYPNOSHROOM, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 8, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_DANCER, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_4)
			{
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PUFFSHROOM, 11, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_HYPNOSHROOM, 4, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 4, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 8, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 7, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_FOOTBALL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_5)
			{
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 6, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PUMPKINSHELL, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 4, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_HYPNOSHROOM, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_MAGNETSHROOM, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 6, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_FOOTBALL, SeedType.SEED_NONE, 3, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_6)
			{
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 7, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_TALLNUT, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_THREEPEATER, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_TORCHWOOD, 4, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 7, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_POLEVAULTER, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_FOOTBALL, SeedType.SEED_NONE, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_7)
			{
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SPIKEWEED, 13, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 10, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_8)
			{
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PUFFSHROOM, 7, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_TALLNUT, 3, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 4, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 8, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 4, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_POGO, SeedType.SEED_NONE, 4, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_9)
			{
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 6, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_THREEPEATER, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_POTATOMINE, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PLANTERN, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 8, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_GARGANTUAR, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_ENDLESS)
			{
				int num2 = TodCommon.ClampInt(mSurvivalStage / 10, 0, 8);
				ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 6, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_THREEPEATER, 2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_POTATOMINE, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PLANTERN, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SUN, ZombieType.ZOMBIE_INVALID, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 8 - num2, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_GARGANTUAR, SeedType.SEED_NONE, 1 + num2, Challenge.aGridArray, num);
				ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
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
				Plant plant = mBoard.NewPlant(theCol, i, theSeedType, SeedType.SEED_NONE);
				if (theSeedType == SeedType.SEED_POTATOMINE)
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
				if (coin.mType == CoinType.COIN_USABLE_SEED_PACKET)
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
			mApp.AddTodParticle(400f, 300f, 400000, ParticleEffect.PARTICLE_SCREEN_FLASH);
		}

		public void ScaryPotterMalletPot(GridItem theScaryPot)
		{
			mChallengeGridX = theScaryPot.mGridX;
			mChallengeGridY = theScaryPot.mGridY;
			int num = mBoard.GridToPixelX(theScaryPot.mGridX, theScaryPot.mGridY);
			int num2 = mBoard.GridToPixelY(theScaryPot.mGridX, theScaryPot.mGridY);
			Reanimation reanimation = mApp.AddReanimation(num, num2, 400000, ReanimationType.REANIM_HAMMER);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_open_pot, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 40f);
			mReanimChallenge = mApp.ReanimationGetID(reanimation);
			mChallengeState = ChallengeState.STATECHALLENGE_SCARY_POTTER_MALLETING;
			mApp.PlayFoley(FoleyType.FOLEY_SWING);
		}

		public static ZombieType IZombieSeedTypeToZombieType(SeedType theSeedType)
		{
			if (theSeedType == SeedType.SEED_ZOMBIE_NORMAL)
			{
				return ZombieType.ZOMBIE_NORMAL;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_TRAFFIC_CONE)
			{
				return ZombieType.ZOMBIE_TRAFFIC_CONE;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_POLEVAULTER)
			{
				return ZombieType.ZOMBIE_POLEVAULTER;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_PAIL)
			{
				return ZombieType.ZOMBIE_PAIL;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_LADDER)
			{
				return ZombieType.ZOMBIE_LADDER;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_DIGGER)
			{
				return ZombieType.ZOMBIE_DIGGER;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_BUNGEE)
			{
				return ZombieType.ZOMBIE_BUNGEE;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_FOOTBALL)
			{
				return ZombieType.ZOMBIE_FOOTBALL;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_BALLOON)
			{
				return ZombieType.ZOMBIE_BALLOON;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_SCREEN_DOOR)
			{
				return ZombieType.ZOMBIE_DOOR;
			}
			if (theSeedType == SeedType.SEED_ZOMBONI)
			{
				return ZombieType.ZOMBIE_ZAMBONI;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_POGO)
			{
				return ZombieType.ZOMBIE_POGO;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_DANCER)
			{
				return ZombieType.ZOMBIE_DANCER;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_GARGANTUAR)
			{
				return ZombieType.ZOMBIE_GARGANTUAR;
			}
			if (theSeedType == SeedType.SEED_ZOMBIE_IMP)
			{
				return ZombieType.ZOMBIE_IMP;
			}
			Debug.ASSERT(false);
			return ZombieType.ZOMBIE_NORMAL;
		}

		public static bool IsZombieSeedType(SeedType theSeedType)
		{
			return theSeedType == SeedType.SEED_ZOMBIE_NORMAL || theSeedType == SeedType.SEED_ZOMBIE_TRAFFIC_CONE || theSeedType == SeedType.SEED_ZOMBIE_POLEVAULTER || theSeedType == SeedType.SEED_ZOMBIE_PAIL || theSeedType == SeedType.SEED_ZOMBIE_LADDER || theSeedType == SeedType.SEED_ZOMBIE_DIGGER || theSeedType == SeedType.SEED_ZOMBIE_BUNGEE || theSeedType == SeedType.SEED_ZOMBIE_FOOTBALL || theSeedType == SeedType.SEED_ZOMBIE_BALLOON || theSeedType == SeedType.SEED_ZOMBIE_SCREEN_DOOR || theSeedType == SeedType.SEED_ZOMBONI || theSeedType == SeedType.SEED_ZOMBIE_POGO || theSeedType == SeedType.SEED_ZOMBIE_DANCER || theSeedType == SeedType.SEED_ZOMBIE_GARGANTUAR || theSeedType == SeedType.SEED_ZOMBIE_IMP;
		}

		public void IZombieMouseDownWithZombie(int x, int y, int theClickCount)
		{
			if (theClickCount < 0)
			{
				mBoard.RefreshSeedPacketFromCursor();
				mApp.PlayFoley(FoleyType.FOLEY_DROP);
				return;
			}
			int num = mBoard.PlantingPixelToGridX((int)(x * Constants.IS), (int)(y * Constants.IS), mBoard.mCursorObject.mType);
			int num2 = mBoard.PlantingPixelToGridY((int)(x * Constants.IS), (int)(y * Constants.IS), mBoard.mCursorObject.mType);
			if (num == -1 || num2 == -1)
			{
				mBoard.RefreshSeedPacketFromCursor();
				mApp.PlayFoley(FoleyType.FOLEY_DROP);
				return;
			}
			PlantingReason plantingReason = CanPlantAt(num, num2, mBoard.mCursorObject.mType);
			if (plantingReason == PlantingReason.PLANTING_OK)
			{
				if (!mApp.mEasyPlantingCheat)
				{
					int currentPlantCost = mBoard.GetCurrentPlantCost(mBoard.mCursorObject.mType, mBoard.mCursorObject.mImitaterType);
					if (!mBoard.TakeSunMoney(currentPlantCost))
					{
						return;
					}
				}
				mBoard.ClearAdvice(AdviceType.ADVICE_I_ZOMBIE_LEFT_OF_LINE);
				mBoard.ClearAdvice(AdviceType.ADVICE_I_ZOMBIE_NOT_PASSED_LINE);
				ZombieType theZombieType = Challenge.IZombieSeedTypeToZombieType(mBoard.mCursorObject.mType);
				IZombiePlaceZombie(theZombieType, num, num2);
				Debug.ASSERT(mBoard.mCursorObject.mSeedBankIndex >= 0 && mBoard.mCursorObject.mSeedBankIndex < mBoard.mSeedBank.mNumPackets);
				SeedPacket seedPacket = mBoard.mSeedBank.mSeedPackets[mBoard.mCursorObject.mSeedBankIndex];
				seedPacket.WasPlanted();
				mApp.PlayFoley(FoleyType.FOLEY_PLANT);
				mBoard.ClearCursor();
				return;
			}
			mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
			if (mBoard.mCursorObject.mType == SeedType.SEED_ZOMBIE_BUNGEE)
			{
				mBoard.DisplayAdvice("[ADVICE_I_ZOMBIE_LEFT_OF_LINE]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_I_ZOMBIE_LEFT_OF_LINE);
				return;
			}
			mBoard.DisplayAdvice("[ADVICE_I_ZOMBIE_NOT_PASSED_LINE]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_I_ZOMBIE_NOT_PASSED_LINE);
		}

		public void IZombieStart()
		{
			mBoard.DisplayAdvice("[ADVICE_I_ZOMBIE_EAT_ALL_BRAINS]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_I_ZOMBIE_EAT_ALL_BRAINS);
		}

		public void IZombiePlacePlants(SeedType theSeedType, int theCount, int theGridY)
		{
			int num = 0;
			int num2 = 6;
			if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5)
			{
				num2 = 4;
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8 || mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
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
					if (mBoard.CanPlantAt(j, i, theSeedType) == PlantingReason.PLANTING_OK && ((theSeedType != SeedType.SEED_WALLNUT && theSeedType != SeedType.SEED_TALLNUT && theSeedType != SeedType.SEED_TORCHWOOD) || num2 - j <= 3))
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
				Plant thePlant = mBoard.NewPlant(todWeightedGridArray.mX, todWeightedGridArray.mY, theSeedType, SeedType.SEED_NONE);
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
				if (!zombie.mDead && !zombie.IsDeadOrDying() && zombie.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_IN_VAULT && !zombie.mIsEating && zombie.mJustGotShotCounter < -500)
				{
					zombie.PickRandomSpeed();
				}
			}
			bool flag = false;
			count = mBoard.mPlants.Count;
			for (int j = 0; j < count; j++)
			{
				Plant plant = mBoard.mPlants[j];
				if (!plant.mDead && (plant.mState == PlantState.STATE_SQUASH_FALLING || plant.mState == PlantState.STATE_SQUASH_DONE_FALLING || plant.mState == PlantState.STATE_CHOMPER_BITING || plant.mState == PlantState.STATE_CHOMPER_BITING_GOT_ONE))
				{
					flag = true;
					break;
				}
			}
			int num2 = -1;
			TodParticleSystem todParticleSystem = null;
			while (mBoard.IterateParticles(ref todParticleSystem, ref num2))
			{
				if (todParticleSystem.mEffectType == ParticleEffect.PARTICLE_POTATO_MINE)
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
			IZombieSetPlantFilterEffect(thePlant, FilterEffectType.FILTER_EFFECT_NONE);
			reanimation.DrawRenderGroup(g, 0);
			IZombieSetPlantFilterEffect(thePlant, FilterEffectType.FILTER_EFFECT_NONE);
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
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_SCARY_POT)
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
				newGridItem.mGridItemType = GridItemType.GRIDITEM_IZOMBIE_BRAIN;
				newGridItem.mGridX = 0;
				newGridItem.mGridY = i;
				newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PLANT, newGridItem.mGridY, 0);
				newGridItem.mGridItemCounter = 70;
				newGridItem.mPosX = mBoard.GridToPixelX(newGridItem.mGridX, newGridItem.mGridY) - 40f;
				newGridItem.mPosY = mBoard.GridToPixelY(newGridItem.mGridX, newGridItem.mGridY) + 40f;
				mBoard.mGridItems.Add(newGridItem);
			}
			if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1)
			{
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 2);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 3);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 7, -1);
				IZombiePlacePlants(SeedType.SEED_SQUASH, 3, -1);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 6, -1);
				IZombiePlacePlants(SeedType.SEED_SNOWPEA, 2, -1);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2)
			{
				IZombiePlacePlantInSquare(SeedType.SEED_SPIKEWEED, 3, 0);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 0);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 3);
				IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 1, 0);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 1, 0);
				IZombiePlacePlants(SeedType.SEED_SNOWPEA, 2, 3);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 1, 3);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 4, -1);
				IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 2, -1);
				IZombiePlacePlants(SeedType.SEED_SNOWPEA, 2, -1);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 4, -1);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3)
			{
				IZombiePlacePlantInSquare(SeedType.SEED_POTATOMINE, 3, 0);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 0);
				IZombiePlacePlantInSquare(SeedType.SEED_POTATOMINE, 2, 2);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 4);
				IZombiePlacePlantInSquare(SeedType.SEED_TORCHWOOD, 3, 3);
				IZombiePlacePlants(SeedType.SEED_TORCHWOOD, 2, -1);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 5, -1);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 7, -1);
				IZombiePlacePlants(SeedType.SEED_SPLITPEA, 1, -1);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4)
			{
				IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 0);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 0);
				IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 1);
				IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 2);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 2);
				IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 3);
				IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 4);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 4);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 1, 0);
				IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, 1);
				IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 2, 2);
				IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, 3);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 1, 4);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 2, -1);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 4, -1);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5)
			{
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 2);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 3);
				IZombiePlacePlants(SeedType.SEED_CACTUS, 1, 1);
				IZombiePlacePlants(SeedType.SEED_CACTUS, 1, 4);
				IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 1, -1);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 5, -1);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 8, -1);
				IZombiePlacePlants(SeedType.SEED_SNOWPEA, 2, -1);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6)
			{
				IZombiePlacePlantInSquare(SeedType.SEED_GARLIC, 4, 1);
				IZombiePlacePlantInSquare(SeedType.SEED_GARLIC, 4, 3);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 3, 1);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 3, 3);
				IZombiePlacePlants(SeedType.SEED_TORCHWOOD, 2, -1);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, -1);
				IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 3, -1);
				IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, -1);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 5, -1);
				IZombiePlacePlants(SeedType.SEED_SQUASH, 2, -1);
				IZombiePlacePlants(SeedType.SEED_KERNELPULT, 2, -1);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7)
			{
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 4, 2);
				IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 4, 4);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 6, -1);
				IZombiePlacePlants(SeedType.SEED_POTATOMINE, 9, -1);
				IZombiePlacePlants(SeedType.SEED_CHOMPER, 8, -1);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8)
			{
				IZombiePlacePlants(SeedType.SEED_WALLNUT, 3, -1);
				IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 2, -1);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 8, -1);
				IZombiePlacePlants(SeedType.SEED_SQUASH, 2, -1);
				IZombiePlacePlants(SeedType.SEED_POTATOMINE, 2, -1);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 8, -1);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_9)
			{
				IZombiePlacePlantInSquare(SeedType.SEED_TALLNUT, 5, 1);
				IZombiePlacePlantInSquare(SeedType.SEED_TORCHWOOD, 5, 3);
				IZombiePlacePlants(SeedType.SEED_POTATOMINE, 4, 0);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, 0);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, 1);
				IZombiePlacePlants(SeedType.SEED_THREEPEATER, 1, 1);
				IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, 1);
				IZombiePlacePlants(SeedType.SEED_SPLITPEA, 1, 1);
				IZombiePlacePlants(SeedType.SEED_CHOMPER, 3, 2);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, 2);
				IZombiePlacePlants(SeedType.SEED_SQUASH, 1, 2);
				IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 3, 3);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, 3);
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 1, 4);
				IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 1, 4);
				IZombiePlacePlants(SeedType.SEED_SCAREDYSHROOM, 1, 4);
				IZombiePlacePlants(SeedType.SEED_STARFRUIT, 1, 4);
				IZombiePlacePlants(SeedType.SEED_SPLITPEA, 1, 4);
				IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 1, 4);
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
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
				IZombiePlacePlants(SeedType.SEED_SUNFLOWER, theCount, -1);
				IZombiePlacePlants(SeedType.SEED_PUFFSHROOM, num2, -1);
				if (num == 0 && mSurvivalStage >= 1)
				{
					int num3 = TodCommon.RandRangeInt(0, 4);
					if (num3 == 0)
					{
						IZombiePlacePlants(SeedType.SEED_SNOWPEA, 9, -1);
						IZombiePlacePlants(SeedType.SEED_SPLITPEA, 4, -1);
						IZombiePlacePlants(SeedType.SEED_REPEATER, 4, -1);
					}
					else if (num3 == 1)
					{
						IZombiePlacePlants(SeedType.SEED_POTATOMINE, 9, -1);
						IZombiePlacePlants(SeedType.SEED_CHOMPER, 8, -1);
					}
					else if (num3 == 2)
					{
						IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 9, -1);
						IZombiePlacePlants(SeedType.SEED_STARFRUIT, 8, -1);
					}
					else if (num3 == 3)
					{
						IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 9, -1);
						IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 8, -1);
					}
					else
					{
						IZombiePlacePlants(SeedType.SEED_SCAREDYSHROOM, 12, -1);
						IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 5, -1);
					}
				}
				else
				{
					int num3 = TodCommon.RandRangeInt(0, 5);
					if (num3 == 0 || num3 == 1 || num3 == 2)
					{
						IZombiePlacePlants(SeedType.SEED_WALLNUT, 1, -1);
						IZombiePlacePlants(SeedType.SEED_TORCHWOOD, 1, -1);
						IZombiePlacePlants(SeedType.SEED_POTATOMINE, 1, -1);
						IZombiePlacePlants(SeedType.SEED_CHOMPER, 2, -1);
						IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 1, -1);
						IZombiePlacePlants(SeedType.SEED_SPLITPEA, 1, -1);
						IZombiePlacePlants(SeedType.SEED_KERNELPULT, 1, -1);
						IZombiePlacePlants(SeedType.SEED_THREEPEATER, 1, -1);
						IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, -1);
						IZombiePlacePlants(SeedType.SEED_SQUASH, 1, -1);
						IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 1, -1);
						IZombiePlacePlants(SeedType.SEED_UMBRELLA, 1, -1);
						IZombiePlacePlants(SeedType.SEED_STARFRUIT, 1, -1);
						IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 1, -1);
						IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 2, -1);
					}
					else if (num3 == 3 || num3 == 4)
					{
						IZombiePlacePlants(SeedType.SEED_TORCHWOOD, 1, -1);
						IZombiePlacePlants(SeedType.SEED_SPLITPEA, 3, -1);
						IZombiePlacePlants(SeedType.SEED_REPEATER, 1, -1);
						IZombiePlacePlants(SeedType.SEED_KERNELPULT, 3, -1);
						IZombiePlacePlants(SeedType.SEED_THREEPEATER, 1, -1);
						IZombiePlacePlants(SeedType.SEED_SNOWPEA, 3, -1);
						IZombiePlacePlants(SeedType.SEED_UMBRELLA, 1, -1);
						IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 1, -1);
						IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 3, -1);
					}
					else
					{
						IZombiePlacePlants(SeedType.SEED_POTATOMINE, 4, -1);
						IZombiePlacePlants(SeedType.SEED_CHOMPER, 3, -1);
						IZombiePlacePlants(SeedType.SEED_SQUASH, 3, -1);
						IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 4, -1);
						IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 3, -1);
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
			int num2 = TodCommon.TodAnimateCurve(0, 100, mBoard.mEffectCounter % 100, 0, -100, TodCurves.CURVE_LINEAR);
			int num3 = TodCommon.TodAnimateCurve(0, 20, mBoard.mEffectCounter % 20, -100, 0, TodCurves.CURVE_LINEAR);
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int theX = num2 + i * 100 + num;
					int theY = num3 + j * 100;
					g.DrawImage(AtlasResources.IMAGE_RAIN, theX, theY);
				}
			}
			int num4 = TodCommon.TodAnimateCurve(0, 161, mBoard.mEffectCounter % 161, 0, -100, TodCurves.CURVE_LINEAR);
			int num5 = TodCommon.TodAnimateCurve(0, 33, mBoard.mEffectCounter % 33, -100, 0, TodCurves.CURVE_LINEAR);
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
			if (mApp.IsStormyNightLevel() || mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
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
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_SQUIRREL)
				{
					SquirrelUpdateOne(gridItem);
				}
			}
			mChallengeScore = 7 - SquirrelCountUncaught();
			mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 7, mChallengeScore, 0, 150, TodCurves.CURVE_LINEAR);
		}

		public int SquirrelCountUncaught()
		{
			int num = 0;
			int num2 = -1;
			GridItem gridItem = null;
			while (mBoard.IterateGridItems(ref gridItem, ref num2))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_SQUIRREL && gridItem.mGridItemState != GridItemState.GRIDITEM_STATE_SQUIRREL_CAUGHT && gridItem.mGridItemState != GridItemState.GRIDITEM_STATE_SQUIRREL_ZOMBIE)
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
				newGridItem.mGridItemType = GridItemType.GRIDITEM_SQUIRREL;
				newGridItem.mGridX = todWeightedGridArray.mX;
				newGridItem.mGridY = todWeightedGridArray.mY;
				newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_WAITING;
				newGridItem.mGridItemCounter = TodCommon.RandRangeInt(100, 500);
				newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, newGridItem.mGridY, 1);
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
			newGridItem.mGridItemType = GridItemType.GRIDITEM_SQUIRREL;
			newGridItem.mGridX = todWeightedGridArray.mX;
			newGridItem.mGridY = todWeightedGridArray.mY;
			newGridItem.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_ZOMBIE;
			newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, newGridItem.mGridY, 1);
			mBoard.mGridItems.Add(newGridItem);
		}

		public void SquirrelFound(GridItem theSquirrel)
		{
			if (theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_ZOMBIE)
			{
				Zombie zombie = mBoard.AddZombieInRow(ZombieType.ZOMBIE_NORMAL, theSquirrel.mGridY, 0);
				zombie.mPosX = mBoard.GridToPixelX(theSquirrel.mGridX, theSquirrel.mGridY);
				theSquirrel.GridItemDie();
				mBoard.DisplayAdvice("[ADVICE_SQUIRREL_ZOMBIE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
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
					Plant topPlantAt = mBoard.GetTopPlantAt(num2, num3, PlantPriority.TOPPLANT_EATING_ORDER);
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
					theSquirrel.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_LEFT;
				}
				else if (todWeightedGridArray.mX > theSquirrel.mGridX)
				{
					theSquirrel.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_RIGHT;
				}
				else if (todWeightedGridArray.mY < theSquirrel.mGridY)
				{
					theSquirrel.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_UP;
				}
				else
				{
					theSquirrel.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_DOWN;
				}
				theSquirrel.mGridItemCounter = 50;
				theSquirrel.mGridX = todWeightedGridArray.mX;
				theSquirrel.mGridY = todWeightedGridArray.mY;
				theSquirrel.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, theSquirrel.mGridY, 1);
				return;
			}
			theSquirrel.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_CAUGHT;
			theSquirrel.mGridItemCounter = 100;
			int num4 = SquirrelCountUncaught();
			if (num4 == 0)
			{
				mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
				SpawnLevelAward(theSquirrel.mGridX, theSquirrel.mGridY);
				return;
			}
			string theAdvice = mApp.Pluralize(num4, "[ADVICE_SQUIRRELS_ONE_LEFT]", "[ADVICE_SQUIRRELS_LEFT]");
			mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
		}

		public void SquirrelPeek(GridItem theSquirrel)
		{
			theSquirrel.mGridItemCounter = 50;
			theSquirrel.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_PEEKING;
		}

		public void SquirrelChew(GridItem theSquirrel)
		{
			theSquirrel.mGridItemCounter = TodCommon.RandRangeInt(100, 500);
			Plant topPlantAt = mBoard.GetTopPlantAt(theSquirrel.mGridX, theSquirrel.mGridY, PlantPriority.TOPPLANT_EATING_ORDER);
			if (topPlantAt == null)
			{
				return;
			}
			float num = mBoard.GridToPixelX(theSquirrel.mGridX, theSquirrel.mGridY);
			float num2 = mBoard.GridToPixelY(theSquirrel.mGridX, theSquirrel.mGridY);
			mApp.AddTodParticle(num + 40f, num2 + 40f, topPlantAt.mRenderOrder + 1, ParticleEffect.PARTICLE_WALLNUT_EAT_SMALL);
			topPlantAt.mBeghouledFlashCountdown = Math.Max(topPlantAt.mBeghouledFlashCountdown, 25);
		}

		public void SquirrelUpdateOne(GridItem theSquirrel)
		{
			if (theSquirrel.mGridItemCounter > 0)
			{
				theSquirrel.mGridItemCounter--;
			}
			if (theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_WAITING || theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_ZOMBIE)
			{
				if (mBoard.GetTopPlantAt(theSquirrel.mGridX, theSquirrel.mGridY, PlantPriority.TOPPLANT_EATING_ORDER) == null)
				{
					SquirrelFound(theSquirrel);
				}
				if (theSquirrel.mGridItemCounter == 0)
				{
					int num = TodCommon.RandRangeInt(0, 1);
					if (num == 0 || theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_ZOMBIE)
					{
						SquirrelChew(theSquirrel);
					}
					else
					{
						SquirrelPeek(theSquirrel);
					}
				}
			}
			if ((theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_PEEKING || theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_UP || theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_DOWN || theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_LEFT || theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_RUNNING_RIGHT) && theSquirrel.mGridItemCounter == 0)
			{
				theSquirrel.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_WAITING;
				theSquirrel.mGridItemCounter = TodCommon.RandRangeInt(100, 500);
			}
			if (theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_CAUGHT && theSquirrel.mGridItemCounter == 0)
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
			if (thePlant.mSeedType == SeedType.SEED_POTATOMINE)
			{
				thePlant.PlayBodyReanim(GlobalMembersReanimIds.ReanimTrackId_anim_armed, ReanimLoopType.REANIM_LOOP, 0, 0f);
				thePlant.mState = PlantState.STATE_POTATO_ARMED;
			}
			thePlant.mBlinkCountdown = 0;
			thePlant.UpdateReanim();
		}

		public void UpdateRain()
		{
			mRainCounter--;
			if (mRainCounter < 0 && !mBoard.mCutScene.IsBeforePreloading())
			{
				ReanimationType theReanimationType = ReanimationType.REANIM_RAIN_SPLASH;
				float theX = TodCommon.RandRangeFloat(40f, 740f);
				float theY = TodCommon.RandRangeFloat(90f, 240f);
				Reanimation reanimation = mApp.AddReanimation(theX, theY, 200000, theReanimationType);
				int theAlpha = TodCommon.RandRangeInt(100, 200);
				float num = TodCommon.RandRangeFloat(0.7f, 1.2f);
				reanimation.mColorOverride = new SexyColor(255, 255, 255, theAlpha);
				reanimation.OverrideScale(num, num);
				theReanimationType = ReanimationType.REANIM_RAIN_CIRCLE;
				theX = TodCommon.RandRangeFloat(40f, 740f);
				theY = TodCommon.RandRangeFloat(290f, 410f);
				reanimation = mApp.AddReanimation(theX, theY, 200000, theReanimationType);
				theAlpha = TodCommon.RandRangeInt(50, 150);
				num = TodCommon.RandRangeFloat(0.7f, 1.1f);
				reanimation.mColorOverride = new SexyColor(255, 255, 255, theAlpha);
				reanimation.OverrideScale(num, num);
				theReanimationType = ReanimationType.REANIM_RAIN_SPLASH;
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
			gridItem.mGridItemCounter -= 3;
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
			if (theZombie.mZombieType == ZombieType.ZOMBIE_DIGGER && theZombie.mZombiePhase == ZombiePhase.PHASE_DIGGER_WALKING)
			{
				return null;
			}
			if (theZombie.mZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				return null;
			}
			if (theZombie.IsWalkingBackwards())
			{
				return null;
			}
			TRect zombieAttackRect = theZombie.GetZombieAttackRect();
			if (theZombie.mZombiePhase == ZombiePhase.PHASE_POLEVAULTER_PRE_VAULT)
			{
				zombieAttackRect = new TRect(50 + theZombie.mX, 0, 20, 115);
			}
			if (theZombie.mZombieType == ZombieType.ZOMBIE_BALLOON)
			{
				zombieAttackRect.mX += 25;
			}
			if (zombieAttackRect.mX > Constants.IZombieBrainPosition)
			{
				return null;
			}
			GridItem gridItemAt = mBoard.GetGridItemAt(GridItemType.GRIDITEM_IZOMBIE_BRAIN, 0, theZombie.mRow);
			if (gridItemAt == null)
			{
				return null;
			}
			if (gridItemAt.mGridItemState == GridItemState.GRIDITEM_STATE_BRAIN_SQUISHED)
			{
				return null;
			}
			return gridItemAt;
		}

		public void IZombiePlacePlantInSquare(SeedType theSeedType, int theGridX, int theGridY)
		{
			if (mBoard.CanPlantAt(theGridX, theGridY, theSeedType) != PlantingReason.PLANTING_OK)
			{
				return;
			}
			Plant thePlant = mBoard.NewPlant(theGridX, theGridY, theSeedType, SeedType.SEED_NONE);
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
				mApp.PlayFoley(FoleyType.FOLEY_PLANT);
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
			Plant topPlantAt = mBoard.GetTopPlantAt(num, num2, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
			if (topPlantAt != null)
			{
				topPlantAt.mBeghouledFlashCountdown = Math.Max(topPlantAt.mBeghouledFlashCountdown, 300);
			}
		}

		public void BeghouledFlashAMatch()
		{
			BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
			LoadBeghouledBoardState(newBeghouledBoardState);
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
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
			else if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
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
			if (thePlant.mSeedType != SeedType.SEED_SUNFLOWER)
			{
				return;
			}
			int num = thePlant.mPlantHealth / 40 + 1;
			for (int i = 0; i < num; i++)
			{
				mBoard.AddCoin(thePlant.mX + 5 * i, thePlant.mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
			}
		}

		public void IZombieSquishBrain(GridItem theBrain)
		{
			theBrain.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, theBrain.mGridY, 0);
			theBrain.mGridItemState = GridItemState.GRIDITEM_STATE_BRAIN_SQUISHED;
			theBrain.mGridItemCounter = 500;
			theBrain.mApp.PlayFoley(FoleyType.FOLEY_SQUISH);
			IZombieScoreBrain(theBrain);
		}

		public void IZombieScoreBrain(GridItem theBrain)
		{
			mBoard.mChallenge.mChallengeScore++;
			mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 5, mBoard.mChallenge.mChallengeScore, 0, 150, TodCurves.CURVE_LINEAR);
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
			if (mBoard.mNextSurvivalStageCounter == 0 && mChallengeState == ChallengeState.STATECHALLENGE_NORMAL && mBoard.mStoreButton.mBtnNoDraw)
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
			if (mChallengeState == ChallengeState.STATECHALLENGE_LAST_STAND_ONSLAUGHT && mApp.mGameScene == GameScenes.SCENE_PLAYING)
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
						Plant topPlantAt = mBoard.GetTopPlantAt(j, k, PlantPriority.TOPPLANT_ANY);
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
			return seedType != SeedType.SEED_NONE && seedType2 != SeedType.SEED_NONE && seedType3 != SeedType.SEED_NONE && seedType4 != SeedType.SEED_NONE;
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
			Plant topPlantAt = mBoard.GetTopPlantAt(num, num2, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt2 = mBoard.GetTopPlantAt(num + 1, num2, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt3 = mBoard.GetTopPlantAt(num, num2 + 1, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt4 = mBoard.GetTopPlantAt(num + 1, num2 + 1, PlantPriority.TOPPLANT_ANY);
			if (!BeghouledTwistMoveCausesMatch(num, num2, newBeghouledBoardState))
			{
				topPlantAt.mX = mBoard.GridToPixelX(topPlantAt.mPlantCol, topPlantAt.mRow) + 20;
				topPlantAt2.mY = mBoard.GridToPixelY(topPlantAt2.mPlantCol, topPlantAt2.mRow) + 20;
				topPlantAt3.mY = mBoard.GridToPixelY(topPlantAt3.mPlantCol, topPlantAt3.mRow) - 20;
				topPlantAt4.mX = mBoard.GridToPixelX(topPlantAt4.mPlantCol, topPlantAt4.mRow) - 20;
				mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
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
			BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_MOVING);
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
			Plant topPlantAt = mBoard.GetTopPlantAt(theGridX, theGridY, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt2 = mBoard.GetTopPlantAt(theGridX + 1, theGridY, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt3 = mBoard.GetTopPlantAt(theGridX, theGridY + 1, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt4 = mBoard.GetTopPlantAt(theGridX + 1, theGridY + 1, PlantPriority.TOPPLANT_ANY);
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
			mBoard.ClearAdvice(AdviceType.ADVICE_BEGHOULED_NO_MOVES);
		}

		public void BeghouledFillHoles(BeghouledBoardState theBoardState, bool theAllowMatches)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (theBoardState.mSeedType[i, j] == SeedType.SEED_NONE && !mBeghouledEated[i, j])
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
					if (theOldBoardState.mSeedType[i, j] == SeedType.SEED_NONE && theNewBoardState.mSeedType[i, j] != SeedType.SEED_NONE)
					{
						num++;
						Plant plant = mBoard.NewPlant(i, j, theNewBoardState.mSeedType[i, j], SeedType.SEED_NONE);
						plant.mY = 80 - num * 100;
						BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_FALLING);
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
						theCoinType = CoinType.COIN_AWARD_PRESENT;
					}
					else
					{
						theCoinType = CoinType.COIN_AWARD_MONEY_BAG;
					}
				}
				else if (num < 30)
				{
					if (mApp.mZenGarden.CanDropChocolate())
					{
						theCoinType = CoinType.COIN_AWARD_CHOCOLATE;
					}
					else
					{
						theCoinType = CoinType.COIN_AWARD_MONEY_BAG;
					}
				}
				else
				{
					theCoinType = CoinType.COIN_AWARD_BAG_DIAMOND;
				}
				float num2 = mBoard.GridToPixelX(theGridX, theGridY) + 40;
				float num3 = mBoard.GridToPixelY(theGridX, theGridY) + 40;
				mBoard.AddCoin((int)num2, (int)num3, theCoinType, CoinMotion.COIN_MOTION_COIN);
				return;
			}
			mBoard.FadeOutLevel();
		}

		public void IZombiePlaceZombie(ZombieType theZombieType, int theGridX, int theGridY)
		{
			Zombie zombie = mBoard.AddZombieInRow(theZombieType, theGridY, 0);
			if (theZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				zombie.mTargetCol = theGridX;
				zombie.SetRow(theGridY);
				zombie.mPosX = mBoard.GridToPixelX(theGridX, theGridY);
				zombie.mPosY = zombie.GetPosYBasedOnRow(theGridY);
				zombie.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, theGridY, 7);
				return;
			}
			zombie.mPosX = mBoard.GridToPixelX(theGridX, theGridY) - 30f;
		}

		public void WhackAZombieUpdate()
		{
			if (mBoard.mSunMoney > 0 && mBoard.mTutorialState == TutorialState.TUTORIAL_OFF)
			{
				mBoard.SetTutorialState(TutorialState.TUTORIAL_WHACK_A_ZOMBIE_BEFORE_PICK_SEED);
				mBoard.mTutorialTimer = 1500;
			}
			if (mBoard.mTutorialState == TutorialState.TUTORIAL_WHACK_A_ZOMBIE_BEFORE_PICK_SEED && mBoard.mTutorialTimer == 0)
			{
				mBoard.SetTutorialState(TutorialState.TUTORIAL_WHACK_A_ZOMBIE_PICK_SEED);
				mBoard.mTutorialTimer = 400;
			}
			if (mBoard.mTutorialState == TutorialState.TUTORIAL_WHACK_A_ZOMBIE_PICK_SEED && mBoard.mTutorialTimer == 0)
			{
				mBoard.SetTutorialState(TutorialState.TUTORIAL_WHACK_A_ZOMBIE_COMPLETED);
			}
		}

		public void LastStandCompletedStage()
		{
			mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
			mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
			mBoard.mSeedBank.RefreshAllPackets();
			int count = mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = mBoard.mPlants[i];
				if (!plant.mDead)
				{
					if (plant.mState == PlantState.STATE_CHOMPER_DIGESTING)
					{
						plant.mStateCountdown = Math.Min(plant.mStateCountdown, 200);
					}
					if (plant.mState == PlantState.STATE_COBCANNON_ARMING)
					{
						plant.mStateCountdown = Math.Min(plant.mStateCountdown, 200);
					}
					if (plant.mState == PlantState.STATE_MAGNETSHROOM_SUCKING || plant.mState == PlantState.STATE_MAGNETSHROOM_CHARGING)
					{
						plant.mStateCountdown = Math.Min(plant.mStateCountdown, 200);
					}
				}
			}
			int survivalFlagsCompleted = mBoard.GetSurvivalFlagsCompleted();
			string theStringToSubstitute = mApp.Pluralize(survivalFlagsCompleted, "[ONE_FLAG]", "[COUNT_FLAGS]");
			string theAdvice = TodCommon.TodReplaceString("[SUCCESSFULLY_DEFENDED]", "{FLAGS}", theStringToSubstitute);
			mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_BIG_MIDDLE_FAST, AdviceType.ADVICE_NONE);
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
			if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_TREE_OF_WISDOM && mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_TREE_FOOD)
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
			if (mChallengeState == ChallengeState.STATECHALLENGE_TREE_GIVE_WISDOM || mChallengeState == ChallengeState.STATECHALLENGE_TREE_BABBLING)
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
				TodStringFile.TodDrawStringWrapped(g, theText, theRect, Resources.FONT_BRIANNETOD16, SexyColor.Black, DrawStringJustification.DS_ALIGN_CENTER_VERTICAL_MIDDLE);
			}
			int num4 = num;
			float num5 = 1f;
			if (mChallengeState == ChallengeState.STATECHALLENGE_TREE_JUST_GREW)
			{
				if (mChallengeStateCounter > 30)
				{
					num4--;
				}
				num5 = TodCommon.TodAnimateCurveFloat(55, 20, mChallengeStateCounter, 1f, 1.2f, TodCurves.CURVE_BOUNCE);
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
			mApp.PreNewGame(GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN, false);
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
			if (mChallengeState == ChallengeState.STATECHALLENGE_TREE_JUST_GREW)
			{
				return false;
			}
			int num = -1;
			GridItem gridItem = null;
			while (mBoard.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_ZEN_TOOL)
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
			if (mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
			{
				num = 3;
			}
			else if (mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_ENDLESS)
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
