using System;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class Challenge : StoreListener
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
			this.Init();
		}

		private void Init()
		{
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mBoard = this.mApp.mBoard;
			this.mBeghouledMouseCapture = false;
			this.mBeghouledMouseDownX = 0;
			this.mBeghouledMouseDownY = 0;
			this.mChallengeStateCounter = 0;
			this.mConveyorBeltCounter = 0;
			this.mChallengeScore = 0;
			this.mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
			this.mShowBowlingLine = false;
			this.mLastConveyorSeedType = SeedType.SEED_NONE;
			this.mSurvivalStage = 0;
			this.mSlotMachineRollCount = 0;
			this.mReanimChallenge = null;
			this.mChallengeGridX = 0;
			this.mChallengeGridY = 0;
			this.mScaryPotterPots = 0;
			this.mBeghouledMatchesThisMove = 0;
			this.mRainCounter = 0;
			this.mTreeOfWisdomTalkIndex = 0;
			for (int i = 0; i < 6; i++)
			{
				this.mReanimCloud[i] = null;
			}
			for (int j = 0; j < Constants.GRIDSIZEX; j++)
			{
				for (int k = 0; k < Constants.MAX_GRIDSIZEY; k++)
				{
					this.mBeghouledEated[j, k] = false;
				}
			}
			for (int l = 0; l < 3; l++)
			{
				this.mBeghouledPurcasedUpgrade[l] = false;
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				TRect trect = this.SlotMachineGetHandleRect();
				ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_SLOT_MACHINE_HANDLE, true);
				Reanimation reanimation = this.mApp.AddReanimation((float)trect.mX - 243f, (float)trect.mY + 50f, 0, ReanimationType.REANIM_SLOT_MACHINE_HANDLE);
				reanimation.mIsAttachment = true;
				reanimation.mAnimRate = 0f;
				this.mReanimChallenge = this.mApp.ReanimationGetID(reanimation);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mName = TodStringFile.TodStringTranslate("[ZEN_GARDEN]");
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM)
			{
				this.mName = TodStringFile.TodStringTranslate("[TREE_OF_WISDOM]");
				return;
			}
			if (this.mApp.mGameMode != GameMode.GAMEMODE_INTRO)
			{
				if (this.mApp.mGameMode == GameMode.GAMEMODE_ADVENTURE)
				{
					return;
				}
				this.mName = ChallengeScreen.gChallengeDefs[this.mApp.mGameMode - GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1].mChallengeName;
			}
		}

		public bool SaveToFile(Buffer b)
		{
			b.WriteBoolean2DArray(this.mBeghouledEated);
			b.WriteLong(this.mBeghouledMatchesThisMove);
			b.WriteBoolean(this.mBeghouledMouseCapture);
			b.WriteLong(this.mBeghouledMouseDownX);
			b.WriteLong(this.mBeghouledMouseDownY);
			b.WriteBooleanArray(this.mBeghouledPurcasedUpgrade);
			b.WriteLong(this.mChallengeGridX);
			b.WriteLong(this.mChallengeGridY);
			b.WriteLong(this.mChallengeScore);
			b.WriteLong((int)this.mChallengeState);
			b.WriteLong(this.mChallengeStateCounter);
			b.WriteLongArray(this.mCloudCounter);
			b.WriteLong(this.mConveyorBeltCounter);
			b.WriteLong((int)this.mLastConveyorSeedType);
			b.WriteLong(this.mRainCounter);
			b.WriteLong(this.mScaryPotterPots);
			b.WriteBoolean(this.mShowBowlingLine);
			b.WriteLong(this.mSlotMachineRollCount);
			b.WriteLong(this.mSurvivalStage);
			b.WriteLong(this.mTreeOfWisdomTalkIndex);
			return true;
		}

		public bool LoadFromFile(Buffer b)
		{
			this.mApp = GlobalStaticVars.gLawnApp;
			this.mBoard = this.mApp.mBoard;
			this.Init();
			this.mBeghouledEated = b.ReadBoolean2DArray();
			this.mBeghouledMatchesThisMove = b.ReadLong();
			this.mBeghouledMouseCapture = b.ReadBoolean();
			this.mBeghouledMouseDownX = b.ReadLong();
			this.mBeghouledMouseDownY = b.ReadLong();
			this.mBeghouledPurcasedUpgrade = b.ReadBooleanArray();
			this.mChallengeGridX = b.ReadLong();
			this.mChallengeGridY = b.ReadLong();
			this.mChallengeScore = b.ReadLong();
			this.mChallengeState = (ChallengeState)b.ReadLong();
			this.mChallengeStateCounter = b.ReadLong();
			this.mCloudCounter = b.ReadLongArray();
			this.mConveyorBeltCounter = b.ReadLong();
			this.mLastConveyorSeedType = (SeedType)b.ReadLong();
			this.mRainCounter = b.ReadLong();
			this.mScaryPotterPots = b.ReadLong();
			this.mShowBowlingLine = b.ReadBoolean();
			this.mSlotMachineRollCount = b.ReadLong();
			this.mSurvivalStage = b.ReadLong();
			this.mTreeOfWisdomTalkIndex = b.ReadLong();
			return true;
		}

		public void StartLevel()
		{
			if (this.mApp.IsWhackAZombieLevel())
			{
				this.mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_HAMMER;
				this.mBoard.mZombieCountDown = 200;
				this.mBoard.mZombieCountDownStart = this.mBoard.mZombieCountDown;
			}
			if (this.mApp.IsStormyNightLevel())
			{
				this.mChallengeState = ChallengeState.STATECHALLENGE_STORM_FLASH_1;
				this.mChallengeStateCounter = 400;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA)
			{
				for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++)
				{
					if (this.mBoard.mPlantRow[i] != PlantRowType.PLANTROW_POOL)
					{
						this.mBoard.mIceMinX[i] = 400;
						this.mBoard.mIceTimer[i] = int.MaxValue;
					}
				}
			}
			if (this.mApp.IsWallnutBowlingLevel())
			{
				this.mBoard.mZombieCountDown = 200;
				this.mBoard.mZombieCountDownStart = this.mBoard.mZombieCountDown;
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_WALLNUT);
				this.mConveyorBeltCounter = 400;
			}
			if (this.mApp.IsWallnutBowlingLevel())
			{
				this.mShowBowlingLine = true;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SHOVEL || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SQUIRREL)
			{
				this.ShovelAddWallnuts();
			}
			if (this.mApp.IsScaryPotterLevel())
			{
				this.ScaryPotterStart();
			}
			if (this.mApp.IsLittleTroubleLevel() || this.mApp.IsStormyNightLevel() || this.mApp.IsBungeeBlitzLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
			{
				this.mBoard.mZombieCountDown = 200;
				this.mBoard.mZombieCountDownStart = this.mBoard.mZombieCountDown;
				this.mConveyorBeltCounter = 200;
			}
			if (this.mApp.IsSurvivalMode() && this.mSurvivalStage == 0)
			{
				string theAdvice = string.Empty;
				if (this.mApp.IsSurvivalNormal(this.mApp.mGameMode))
				{
					theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_SURVIVE_FLAGS]", "{FLAGS}", 5);
				}
				else if (this.mApp.IsSurvivalHard(this.mApp.mGameMode))
				{
					theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_SURVIVE_FLAGS]", "{FLAGS}", 10);
				}
				else
				{
					theAdvice = "[ADVICE_SURVIVE_ENDLESS]";
				}
				this.mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_SURVIVE_FLAGS);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && this.mSurvivalStage == 0)
			{
				string theAdvice2 = TodCommon.TodReplaceNumberString("[ADVICE_SURVIVE_FLAGS]", "{FLAGS}", 5);
				this.mBoard.DisplayAdvice(theAdvice2, MessageStyle.MESSAGE_STYLE_BIG_MIDDLE_FAST, AdviceType.ADVICE_SURVIVE_FLAGS);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1)
			{
				this.mBoard.DisplayAdvice("[ADVICE_FILL_IN_WALLNUTS]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_2)
			{
				this.mBoard.DisplayAdvice("[ADVICE_FILL_IN_SPACES]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SEEING_STARS)
			{
				this.mBoard.DisplayAdvice("[ADVICE_FILL_IN_STARFRUIT]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_SLOT_MACHINE_PULL);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				this.mBoard.mZombieCountDown = 200;
				this.mBoard.mZombieCountDownStart = this.mBoard.mZombieCountDown;
				this.mChallengeStateCounter = 1500;
				this.BeghouledMakeStartBoard();
				this.BeghouledUpdateCraters();
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
				{
					this.mBoard.DisplayAdvice("[ADVICE_BEGHOULED_DRAG_TO_MATCH_3]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
				}
				else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
				{
					this.mBoard.DisplayAdvice("[ADVICE_BEGHOULED_TWIST_TO_MATCH_3]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
				}
			}
			if (this.mApp.IsFirstTimeAdventureMode())
			{
				this.mApp.IsSquirrelLevel();
			}
			if (this.mApp.IsMiniBossLevel())
			{
				this.mBoard.mZombieCountDown = 100;
				this.mBoard.mZombieCountDownStart = this.mBoard.mZombieCountDown;
				this.mConveyorBeltCounter = 200;
			}
			if (!this.mApp.IsFinalBossLevel() && !this.mApp.IsWhackAZombieLevel())
			{
				GameMode mGameMode = this.mApp.mGameMode;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
			{
				this.PortalStart();
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				this.mBoard.mCurrentWave = 9;
				this.mBoard.mZombieCountDown = 2400;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_AIR_RAID || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA)
			{
				this.mBoard.mZombieCountDown = 4500;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_POGO_PARTY)
			{
				this.mBoard.mZombieCountDown = 5500;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mApp.mZenGarden.ZenGardenStart();
			}
			if (this.mApp.IsIZombieLevel())
			{
				this.IZombieStart();
			}
			if (this.mApp.IsSquirrelLevel())
			{
				this.SquirrelStart();
			}
		}

		public void BeghouledPopulateBoard()
		{
			BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
			this.LoadBeghouledBoardState(newBeghouledBoardState);
			bool theAllowMatches = false;
			if (this.BeghouledBoardHasMatch(newBeghouledBoardState))
			{
				theAllowMatches = true;
			}
			BeghouledBoardState newBeghouledBoardState2 = BeghouledBoardState.GetNewBeghouledBoardState();
			for (int i = 0; i < 2; i++)
			{
				this.LoadBeghouledBoardState(newBeghouledBoardState2);
				this.BeghouledFillHoles(newBeghouledBoardState2, theAllowMatches);
				if (this.BeghouledCheckForPossibleMoves(newBeghouledBoardState2))
				{
					break;
				}
			}
			this.BeghouledCreatePlants(newBeghouledBoardState, newBeghouledBoardState2);
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
			int count = this.mBoard.mPlants.Count;
			for (int k = 0; k < count; k++)
			{
				Plant plant = this.mBoard.mPlants[k];
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
				if (this.mBeghouledPurcasedUpgrade[0] && seedType == SeedType.SEED_PEASHOOTER)
				{
					seedType = SeedType.SEED_REPEATER;
				}
				else if (this.mBeghouledPurcasedUpgrade[1] && seedType == SeedType.SEED_PUFFSHROOM)
				{
					seedType = SeedType.SEED_FUMESHROOM;
				}
				else if (this.mBeghouledPurcasedUpgrade[2] && seedType == SeedType.SEED_WALLNUT)
				{
					seedType = SeedType.SEED_TALLNUT;
				}
				theBoardState.mSeedType[theGridX, theGridY] = seedType;
				if (theAllowMatches || !this.BeghouledBoardHasMatch(theBoardState))
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
					if (this.BeghouledHorizontalMatchLength(j, i, theBoardState) >= 3)
					{
						return true;
					}
					if (this.BeghouledVerticalMatchLength(j, i, theBoardState) >= 3)
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
			SeedType seedType = this.BeghouledGetPlantAt(x, y, theBoardState);
			if (seedType == SeedType.SEED_NONE)
			{
				return 0;
			}
			if (this.BeghouledGetPlantAt(x, y - 1, theBoardState) == seedType)
			{
				return 0;
			}
			int num = 1;
			while (this.BeghouledGetPlantAt(x, y + num, theBoardState) == seedType)
			{
				num++;
			}
			return num;
		}

		public int BeghouledHorizontalMatchLength(int x, int y, BeghouledBoardState theBoardState)
		{
			SeedType seedType = this.BeghouledGetPlantAt(x, y, theBoardState);
			if (seedType == SeedType.SEED_NONE)
			{
				return 0;
			}
			if (this.BeghouledGetPlantAt(x - 1, y, theBoardState) == seedType)
			{
				return 0;
			}
			int num = 1;
			while (this.BeghouledGetPlantAt(x + num, y, theBoardState) == seedType)
			{
				num++;
			}
			return num;
		}

		public void BeghouledDragStart(int x, int y)
		{
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			this.mBeghouledMouseCapture = true;
			this.mBeghouledMouseDownX = (int)((float)x * Constants.IS);
			this.mBeghouledMouseDownY = (int)((float)y * Constants.IS);
		}

		public void BeghouledDragUpdate(int x, int y)
		{
			x = (int)((float)x * Constants.IS);
			y = (int)((float)y * Constants.IS);
			int num = x - this.mBeghouledMouseDownX;
			int num2 = y - this.mBeghouledMouseDownY;
			if (Math.Abs(num) < 10 && Math.Abs(num2) < 10)
			{
				return;
			}
			this.mBoard.ClearAdvice(AdviceType.ADVICE_BEGHOULED_DRAG_TO_MATCH_3);
			this.mBeghouledMouseCapture = false;
			int num3 = this.mBoard.PixelToGridX(this.mBeghouledMouseDownX, this.mBeghouledMouseDownY);
			int num4 = this.mBoard.PixelToGridY(this.mBeghouledMouseDownX, this.mBeghouledMouseDownY);
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
			this.LoadBeghouledBoardState(newBeghouledBoardState);
			Plant topPlantAt = this.mBoard.GetTopPlantAt(num3, num4, PlantPriority.TOPPLANT_ANY);
			if (!this.BeghouledIsValidMove(num3, num4, num5, num6, newBeghouledBoardState))
			{
				this.BeghouledDragCancel();
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
					this.mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
				}
				newBeghouledBoardState.PrepareForReuse();
				return;
			}
			Plant topPlantAt2 = this.mBoard.GetTopPlantAt(num5, num6, PlantPriority.TOPPLANT_ANY);
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
			this.BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_MOVING);
			newBeghouledBoardState.PrepareForReuse();
		}

		public void BeghouledDragCancel()
		{
			this.mBeghouledMouseCapture = false;
		}

		public bool MouseMove(int x, int y)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED && !this.mBoard.HasLevelAwardDropped())
			{
				if (this.mBeghouledMouseCapture)
				{
					this.BeghouledDragUpdate(x, y);
					return true;
				}
				HitResult hitResult;
				this.mBoard.MouseHitTest(x, y, out hitResult, false);
				if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
				{
					return true;
				}
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				if (this.mChallengeState == ChallengeState.STATECHALLENGE_ZEN_FADING)
				{
					this.mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
				}
				this.mChallengeStateCounter = 3000;
			}
			return false;
		}

		public bool MouseDown(int x, int y, int theClickCount, HitResult theHitResult)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				return this.mApp.mZenGarden.MouseDownZenGarden(x, y, theClickCount, theHitResult);
			}
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				return false;
			}
			if (this.mBoard.IsScaryPotterDaveTalking() && this.mApp.mCrazyDaveMessageIndex != -1)
			{
				this.AdvanceCrazyDaveDialog();
				return true;
			}
			if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_COIN && theClickCount >= 0)
			{
				return false;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
			{
				if (this.mChallengeState != ChallengeState.STATECHALLENGE_NORMAL)
				{
					return false;
				}
				if (theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_PLANT)
				{
					this.BeghouledDragStart(x, y);
					return true;
				}
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				if (this.mChallengeState != ChallengeState.STATECHALLENGE_NORMAL)
				{
					return false;
				}
				this.BeghouledTwistMouseDown(x, y);
			}
			if (this.mApp.IsSlotMachineLevel() && theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_SLOT_MACHINE_HANDLE && this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL && this.mChallengeState == ChallengeState.STATECHALLENGE_NORMAL)
			{
				if (this.mBoard.TakeSunMoney(25))
				{
					for (int i = 0; i < 3; i++)
					{
						this.mBoard.mSeedBank.mSeedPackets[i].SlotMachineStart();
					}
					Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimChallenge);
					reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_pull, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 36f);
					this.mChallengeState = ChallengeState.STATECHALLENGE_SLOT_MACHINE_ROLLING;
					this.mBoard.SetTutorialState(TutorialState.TUTORIAL_SLOT_MACHINE_COMPLETED);
					this.mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
					this.mSlotMachineRollCount++;
					this.mApp.PlaySample(Resources.SOUND_SLOT_MACHINE);
				}
				return true;
			}
			if (this.mApp.IsWhackAZombieLevel() && theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_NONE && this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_HAMMER && theClickCount >= 0)
			{
				this.MouseDownWhackAZombie(x, y);
				return true;
			}
			if (this.mApp.IsScaryPotterLevel() && theHitResult.mObjectType == GameObjectType.OBJECT_TYPE_SCARY_POT)
			{
				this.ScaryPotterMalletPot((GridItem)theHitResult.mObject);
				return true;
			}
			return false;
		}

		public bool MouseUp(int x, int y)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
			{
				if (this.mBeghouledMouseCapture && !this.mBoard.mAdvice.IsBeingDisplayed() && this.mChallengeScore == 0)
				{
					this.mBoard.DisplayAdvice("[ADVICE_BEGHOULED_DRAG_TO_MATCH_3]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_DRAG_TO_MATCH_3);
				}
				this.BeghouledDragCancel();
			}
			return false;
		}

		public void ClearCursor()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
			{
				this.BeghouledDragCancel();
			}
			if (this.mApp.IsWhackAZombieLevel() && !this.mBoard.HasLevelAwardDropped())
			{
				this.mBoard.mCursorObject.mCursorType = CursorType.CURSOR_TYPE_HAMMER;
			}
		}

		public void BeghouledRemoveHorizontalMatch(int x, int y, BeghouledBoardState theBoardState)
		{
			SeedType seedType = this.BeghouledGetPlantAt(x, y, theBoardState);
			do
			{
				Plant topPlantAt = this.mBoard.GetTopPlantAt(x, y, PlantPriority.TOPPLANT_ANY);
				if (topPlantAt != null)
				{
					topPlantAt.Die();
				}
				x++;
			}
			while (this.BeghouledGetPlantAt(x, y, theBoardState) == seedType);
		}

		public void BeghouledRemoveVerticalMatch(int x, int y, BeghouledBoardState theBoardState)
		{
			SeedType seedType = this.BeghouledGetPlantAt(x, y, theBoardState);
			do
			{
				Plant topPlantAt = this.mBoard.GetTopPlantAt(x, y, PlantPriority.TOPPLANT_ANY);
				if (topPlantAt != null)
				{
					topPlantAt.Die();
				}
				y++;
			}
			while (this.BeghouledGetPlantAt(x, y, theBoardState) == seedType);
		}

		public void BeghouledRemoveMatches(BeghouledBoardState theBoardState)
		{
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					int num = this.BeghouledHorizontalMatchLength(j, i, theBoardState);
					if (num >= 3)
					{
						this.BeghouledRemoveHorizontalMatch(j, i, theBoardState);
						this.BeghouledScore(j, i, num, true);
					}
					int num2 = this.BeghouledVerticalMatchLength(j, i, theBoardState);
					if (num2 >= 3)
					{
						this.BeghouledRemoveVerticalMatch(j, i, theBoardState);
						this.BeghouledScore(j, i, num2, false);
					}
				}
			}
		}

		public void Update()
		{
			if (this.mApp.IsStormyNightLevel())
			{
				this.UpdateStormyNight();
			}
			if (this.mBoard.mPaused)
			{
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
				{
					this.mChallengeGridX = -1;
					this.mChallengeGridY = -1;
				}
				return;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS || this.mApp.IsStormyNightLevel())
			{
				this.UpdateRain();
			}
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
			{
				return;
			}
			if (this.mBoard.HasConveyorBeltSeedBank())
			{
				this.UpdateConveyorBelt();
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				this.UpdateBeghouled();
			}
			if (this.mApp.IsScaryPotterLevel())
			{
				this.ScaryPotterUpdate();
			}
			if ((this.mApp.IsScaryPotterLevel() || this.mApp.IsWhackAZombieLevel()) && this.mBoard.mSeedBank.mX < 0)
			{
				int num = this.mBoard.mSunMoney + this.mBoard.CountSunBeingCollected();
				if (num > 0 || this.mBoard.mSeedBank.mX > -this.mBoard.mSeedBank.mWidth)
				{
					this.mBoard.mSeedBank.mX += 2;
					if (this.mBoard.mSeedBank.mX > 0)
					{
						this.mBoard.mSeedBank.mX = 0;
					}
				}
			}
			if (this.mApp.IsWhackAZombieLevel())
			{
				this.WhackAZombieUpdate();
			}
			if (this.mApp.IsIZombieLevel())
			{
				this.IZombieUpdate();
			}
			if (this.mApp.IsSlotMachineLevel())
			{
				this.UpdateSlotMachine();
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SPEED && this.speedBoardCounter++ % 3 == 0)
			{
				this.mBoard.UpdateGame();
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
			{
				this.UpdateRainingSeeds();
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
			{
				this.UpdatePortalCombat();
			}
			if (this.mApp.IsSquirrelLevel())
			{
				this.SquirrelUpdate();
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE && this.mBoard.mMainCounter == 3000)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
				this.mApp.PlaySample(Resources.SOUND_LOSEMUSIC);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
			{
				this.LastStandUpate();
			}
			Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mReanimChallenge);
			if (reanimation != null && reanimation.mIsAttachment)
			{
				reanimation.Update();
			}
		}

		public void UpdateBeghouled()
		{
			this.mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 75, this.mChallengeScore, 0, 150, TodCurves.CURVE_LINEAR);
			bool flag = false;
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && this.UpdateBeghouledPlant(plant))
				{
					flag = true;
				}
			}
			if (this.mBoard.mSeedBank.mNumPackets > 4 && !this.mBoard.mAdvice.IsBeingDisplayed() && !this.mBoard.mHelpDisplayed[22])
			{
				int currentPlantCost = this.mBoard.GetCurrentPlantCost(SeedType.SEED_BEGHOULED_BUTTON_CRATER, SeedType.SEED_NONE);
				if (this.mBoard.CanTakeSunMoney(currentPlantCost) && this.BeghouledCanClearCrater() && !this.mBoard.HasLevelAwardDropped())
				{
					this.mBoard.DisplayAdvice("[ADVICE_BEGHOULED_USE_CRATER_2]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_USE_CRATER_2);
				}
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && this.mChallengeState == ChallengeState.STATECHALLENGE_NORMAL)
			{
				if (this.BeghouledTwistSquareFromMouse(this.mApp.mWidgetManager.mLastMouseX, this.mApp.mWidgetManager.mLastMouseY, ref this.mChallengeGridX, ref this.mChallengeGridY))
				{
					BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
					this.LoadBeghouledBoardState(newBeghouledBoardState);
					if (!this.BeghouledTwistValidMove(this.mChallengeGridX, this.mChallengeGridY, newBeghouledBoardState))
					{
						this.mChallengeGridX = -1;
						this.mChallengeGridY = -1;
					}
					newBeghouledBoardState.PrepareForReuse();
				}
			}
			else
			{
				this.mChallengeGridX = -1;
				this.mChallengeGridY = -1;
			}
			if (!flag && (this.mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_FALLING || this.mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_MOVING))
			{
				this.mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
				this.mChallengeStateCounter = 1500;
				BeghouledBoardState newBeghouledBoardState2 = BeghouledBoardState.GetNewBeghouledBoardState();
				this.LoadBeghouledBoardState(newBeghouledBoardState2);
				this.BeghouledRemoveMatches(newBeghouledBoardState2);
				this.LoadBeghouledBoardState(newBeghouledBoardState2);
				this.BeghouledMakePlantsFall(newBeghouledBoardState2);
				this.BeghouledPopulateBoard();
				if (this.mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_FALLING)
				{
					newBeghouledBoardState2.PrepareForReuse();
					return;
				}
				this.mChallengeStateCounter = 1500;
				this.mBeghouledMatchesThisMove = 0;
				this.BeghouledCheckStuckState();
				newBeghouledBoardState2.PrepareForReuse();
			}
			if (this.mChallengeStateCounter == 0)
			{
				return;
			}
			this.mChallengeStateCounter--;
			if (this.mChallengeStateCounter > 0)
			{
				return;
			}
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			if (this.mChallengeState == ChallengeState.STATECHALLENGE_NORMAL)
			{
				this.BeghouledFlashAMatch();
				this.mChallengeStateCounter = 1500;
				return;
			}
			if (this.mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_NO_MATCHES)
			{
				this.mApp.AddTodParticle(400f, 300f, 400000, ParticleEffect.PARTICLE_SCREEN_FLASH);
				this.BeghouledShuffle();
			}
		}

		public bool UpdateBeghouledPlant(Plant thePlant)
		{
			bool flag = false;
			int num = this.mBoard.GridToPixelX(thePlant.mPlantCol, thePlant.mRow) - thePlant.mX;
			int num2 = this.mBoard.GridToPixelY(thePlant.mPlantCol, thePlant.mRow) - thePlant.mY;
			int num3;
			if (this.mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_MOVING)
			{
				num3 = 3;
			}
			else
			{
				num3 = TodCommon.TodAnimateCurve(90, 30, this.mChallengeStateCounter, 1, 20, TodCurves.CURVE_EASE_IN);
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
				magnetItem.mPosX += (float)num4;
				magnetItem.mPosY += (float)num5;
			}
			return flag;
		}

		public void BeghouledFallIntoSquare(int x, int y, BeghouledBoardState theBoardState)
		{
			if (this.mBeghouledEated[x, y])
			{
				return;
			}
			for (int i = y - 1; i >= 0; i--)
			{
				Plant topPlantAt = this.mBoard.GetTopPlantAt(x, i, PlantPriority.TOPPLANT_ANY);
				if (topPlantAt != null)
				{
					topPlantAt.mRow = y;
					topPlantAt.mRenderOrder = topPlantAt.CalcRenderOrder();
					theBoardState.mSeedType[x, y] = topPlantAt.mSeedType;
					theBoardState.mSeedType[x, i] = SeedType.SEED_NONE;
					this.BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_FALLING);
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
					SeedType seedType = this.BeghouledGetPlantAt(j, i, theBoardState);
					if (seedType == SeedType.SEED_NONE)
					{
						this.BeghouledFallIntoSquare(j, i, theBoardState);
					}
				}
			}
		}

		public void ZombieAtePlant(Zombie theZombie, Plant thePlant)
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				this.mBeghouledEated[thePlant.mPlantCol, thePlant.mRow] = true;
				if (this.mBoard.mSeedBank.mNumPackets == 4)
				{
					this.mBoard.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.SEED_BEGHOULED_BUTTON_CRATER, SeedType.SEED_NONE);
					this.mBoard.mSeedBank.mNumPackets = 5;
					this.mBoard.DisplayAdvice("[ADVICE_BEGHOULED_USE_CRATER_1]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_USE_CRATER_1);
				}
				this.BeghouledCheckStuckState();
				this.BeghouledUpdateCraters();
			}
		}

		public void DrawBackdrop(Graphics g)
		{
			if (this.mApp.IsArtChallenge())
			{
				this.DrawArtChallenge(g);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				this.DrawBeghouled(g);
			}
			if (this.mApp.IsWallnutBowlingLevel() && this.mShowBowlingLine)
			{
				g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(200f), 77f * Constants.S);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5)
			{
				g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(244f), 73f * Constants.S);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
			{
				g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(284f), 73f * Constants.S);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_9)
			{
				g.DrawImage(AtlasResources.IMAGE_WALLNUT_BOWLINGSTRIPE, Constants.InvertAndScale(330f), 73f * Constants.S);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE)
			{
				int mMainCounter = this.mBoard.mMainCounter;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mApp.mZenGarden.DrawBackdrop(g);
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
					SeedType artChallengeSeed = this.GetArtChallengeSeed(j, i);
					if (artChallengeSeed != SeedType.SEED_NONE && this.mBoard.GetTopPlantAt(j, i, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION) == null)
					{
						TPoint[] celPosition = this.mBoard.GetCelPosition(j, i);
						celPosition[0].mX = (int)((float)celPosition[0].mX * Constants.S);
						celPosition[0].mY = (int)((float)celPosition[0].mY * Constants.S);
						celPosition[1].mX = (int)((float)celPosition[1].mX * Constants.S);
						celPosition[1].mY = (int)((float)celPosition[1].mY * Constants.S);
						celPosition[2].mX = (int)((float)celPosition[2].mX * Constants.S);
						celPosition[2].mY = (int)((float)celPosition[2].mY * Constants.S);
						celPosition[3].mX = (int)((float)celPosition[3].mX * Constants.S);
						celPosition[3].mY = (int)((float)celPosition[3].mY * Constants.S);
						float thePosX = (float)(celPosition[0].x + (celPosition[2].x - celPosition[0].x) / 2);
						float thePosY = (float)(celPosition[2].y - Constants.Challenge_SeeingStars_StarfruitPreview_Offset_Y);
						Plant.DrawSeedType(g, artChallengeSeed, SeedType.SEED_NONE, DrawVariation.VARIATION_NORMAL, thePosX, thePosY);
					}
				}
			}
			g.SetColorizeImages(false);
			GameMode mGameMode = this.mApp.mGameMode;
		}

		public void CheckForCompleteArtChallenge(int theGridX, int theGridY)
		{
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					SeedType artChallengeSeed = this.GetArtChallengeSeed(j, i);
					if (artChallengeSeed != SeedType.SEED_NONE)
					{
						Plant topPlantAt = this.mBoard.GetTopPlantAt(j, i, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
						if (topPlantAt == null || topPlantAt.mSeedType != artChallengeSeed)
						{
							return;
						}
					}
				}
			}
			this.SpawnLevelAward(theGridX, theGridY);
		}

		public SeedType GetArtChallengeSeed(int theGridX, int theGridY)
		{
			if (theGridY >= 6)
			{
				return SeedType.SEED_NONE;
			}
			Debug.ASSERT(theGridX >= 0 && theGridX < 9 && theGridY >= 0);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1)
			{
				return Challenge.gArtChallengeWallnut[theGridY, theGridX];
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_2)
			{
				return Challenge.gArtChallengeSunFlower[theGridY, theGridX];
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SEEING_STARS)
			{
				return Challenge.gArtChallengeStarfruit[theGridY, theGridX];
			}
			return SeedType.SEED_NONE;
		}

		public void PlantAdded(Plant thePlant)
		{
			if (this.mApp.IsArtChallenge())
			{
				SeedType artChallengeSeed = this.GetArtChallengeSeed(thePlant.mPlantCol, thePlant.mRow);
				if (artChallengeSeed != SeedType.SEED_NONE && artChallengeSeed == thePlant.mSeedType)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
					this.mApp.AddTodParticle((float)(thePlant.mX + 40), (float)(thePlant.mY + 40), 400000, ParticleEffect.PARTICLE_PRESENT_PICKUP);
					this.CheckForCompleteArtChallenge(thePlant.mPlantCol, thePlant.mRow);
				}
			}
		}

		public PlantingReason CanPlantAt(int theGridX, int theGridY, SeedType theType)
		{
			if (this.mApp.IsWallnutBowlingLevel())
			{
				if (theGridX > 2)
				{
					return PlantingReason.PLANTING_NOT_PASSED_LINE;
				}
			}
			else if (this.mApp.IsIZombieLevel())
			{
				int num = 6;
				if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5)
				{
					num = 4;
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
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
			else if (this.mApp.IsArtChallenge())
			{
				SeedType artChallengeSeed = this.GetArtChallengeSeed(theGridX, theGridY);
				if (artChallengeSeed != SeedType.SEED_NONE && artChallengeSeed != theType && theType != SeedType.SEED_LILYPAD && theType != SeedType.SEED_PUMPKINSHELL)
				{
					return PlantingReason.PLANTING_NOT_ON_ART;
				}
				if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ART_CHALLENGE_1)
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
			else if (this.mApp.IsFinalBossLevel() && theGridX >= 8)
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
					if (this.mBeghouledEated[j, i])
					{
						int num = this.mBoard.GridToPixelX(j, i) - 8;
						int num2 = this.mBoard.GridToPixelY(j, i) + 40;
						g.DrawImageCel(AtlasResources.IMAGE_CRATER, (int)((float)num * Constants.S), (int)((float)num2 * Constants.S), 1, 0);
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
			if (this.mBeghouledEated[x1, y1] || this.mBeghouledEated[x2, y2])
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
			bool result = this.BeghouledBoardHasMatch(theBoardState);
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
					if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
					{
						if (this.BeghouledIsValidMove(j, i, j + 1, i, theBoardState) || this.BeghouledIsValidMove(j, i, j, i + 1, theBoardState))
						{
							return true;
						}
					}
					else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST && this.BeghouledTwistMoveCausesMatch(j, i, theBoardState))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void BeghouledCheckStuckState()
		{
			if (this.mChallengeState != ChallengeState.STATECHALLENGE_NORMAL)
			{
				return;
			}
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
			this.LoadBeghouledBoardState(newBeghouledBoardState);
			if (!this.BeghouledCheckForPossibleMoves(newBeghouledBoardState))
			{
				this.mChallengeState = ChallengeState.STATECHALLENGE_BEGHOULED_NO_MATCHES;
				this.mChallengeStateCounter = 500;
				this.mBoard.DisplayAdviceAgain("[ADVICE_BEGHOULED_NO_MOVES]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_NO_MOVES);
			}
			newBeghouledBoardState.PrepareForReuse();
		}

		public void InitZombieWavesSurvival()
		{
			this.mBoard.mZombieAllowed[0] = true;
			int levelRandSeed = this.mBoard.GetLevelRandSeed();
			RandomNumbers.Seed(levelRandSeed);
			if ((int)RandomNumbers.NextNumber(5f) == 0)
			{
				this.mBoard.mZombieAllowed[5] = true;
			}
			else
			{
				this.mBoard.mZombieAllowed[2] = true;
			}
			int i = Math.Min(this.mSurvivalStage + 1, 9);
			while (i > 0)
			{
				ZombieType zombieType = (ZombieType)RandomNumbers.NextNumber(33f);
				if (!this.mBoard.mZombieAllowed[(int)zombieType] && (!Board.IsZombieTypePoolOnly(zombieType) || this.mBoard.StageHasPool()) && (!this.mBoard.StageHasRoof() || (zombieType != ZombieType.ZOMBIE_DIGGER && zombieType != ZombieType.ZOMBIE_DANCER)) && (!this.mBoard.StageHasGraveStones() || zombieType != ZombieType.ZOMBIE_ZAMBONI) && (this.mBoard.StageHasRoof() || this.mApp.IsSurvivalEndless(this.mApp.mGameMode) || zombieType != ZombieType.ZOMBIE_BUNGEE) && (this.mBoard.GetSurvivalFlagsCompleted() >= 4 || (zombieType != ZombieType.ZOMBIE_GARGANTUAR && zombieType != ZombieType.ZOMBIE_ZAMBONI)) && (this.mBoard.GetSurvivalFlagsCompleted() >= 10 || zombieType != ZombieType.ZOMBIE_REDEYE_GARGANTUAR) && ((this.mApp.mGameMode != GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1 && this.mApp.mGameMode != GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_2 && this.mApp.mGameMode != GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_3) || zombieType <= ZombieType.ZOMBIE_SNORKEL) && zombieType != ZombieType.ZOMBIE_BOBSLED && zombieType != ZombieType.ZOMBIE_BACKUP_DANCER && zombieType != ZombieType.ZOMBIE_IMP && zombieType != ZombieType.ZOMBIE_DUCKY_TUBE && zombieType != ZombieType.ZOMBIE_PEA_HEAD && zombieType != ZombieType.ZOMBIE_WALLNUT_HEAD && zombieType != ZombieType.ZOMBIE_TALLNUT_HEAD && zombieType != ZombieType.ZOMBIE_JALAPENO_HEAD && zombieType != ZombieType.ZOMBIE_GATLING_HEAD && zombieType != ZombieType.ZOMBIE_SQUASH_HEAD && zombieType != ZombieType.ZOMBIE_YETI)
				{
					this.mBoard.mZombieAllowed[(int)zombieType] = true;
					i--;
				}
			}
		}

		public void InitZombieWavesFromList(ZombieType[] theZombieList, int theListLength)
		{
			for (int i = 0; i < theListLength; i++)
			{
				ZombieType zombieType = theZombieList[i];
				this.mBoard.mZombieAllowed[(int)zombieType] = true;
			}
		}

		public void InitZombieWaves()
		{
			if (this.mApp.IsSurvivalMode())
			{
				if (this.mSurvivalStage == 0 && this.mApp.IsSurvivalNormal(this.mApp.mGameMode))
				{
					ZombieType[] array = new ZombieType[]
					{
						ZombieType.ZOMBIE_NORMAL,
						ZombieType.ZOMBIE_TRAFFIC_CONE
					};
					this.InitZombieWavesFromList(array, array.Length);
				}
				else if (this.mSurvivalStage == 0)
				{
					ZombieType[] array2 = new ZombieType[]
					{
						ZombieType.ZOMBIE_NORMAL,
						ZombieType.ZOMBIE_TRAFFIC_CONE,
						ZombieType.ZOMBIE_PAIL
					};
					this.InitZombieWavesFromList(array2, array2.Length);
				}
				else
				{
					this.InitZombieWavesSurvival();
				}
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SPEED)
			{
				ZombieType[] array3 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_DOLPHIN_RIDER,
					ZombieType.ZOMBIE_POLEVAULTER
				};
				this.InitZombieWavesFromList(array3, array3.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_POGO_PARTY)
			{
				ZombieType[] array4 = new ZombieType[]
				{
					ZombieType.ZOMBIE_POGO
				};
				this.InitZombieWavesFromList(array4, array4.Length);
			}
			else if (this.mApp.IsBungeeBlitzLevel())
			{
				ZombieType[] array5 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_LADDER
				};
				this.InitZombieWavesFromList(array5, array5.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_SUNNY_DAY)
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
				this.InitZombieWavesFromList(array6, array6.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
			{
				ZombieType[] array7 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_FOOTBALL,
					ZombieType.ZOMBIE_BALLOON
				};
				this.InitZombieWavesFromList(array7, array7.Length);
			}
			else if (this.mApp.IsLittleTroubleLevel())
			{
				ZombieType[] array8 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_FOOTBALL,
					ZombieType.ZOMBIE_SNORKEL
				};
				this.InitZombieWavesFromList(array8, array8.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BIG_TIME)
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
				this.InitZombieWavesFromList(array9, array9.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
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
				this.InitZombieWavesFromList(array10, array10.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY)
			{
				ZombieType[] array11 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_DOOR,
					ZombieType.ZOMBIE_BALLOON
				};
				this.InitZombieWavesFromList(array11, array11.Length);
			}
			else if (this.mApp.IsWhackAZombieLevel())
			{
				ZombieType[] array12 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL
				};
				this.InitZombieWavesFromList(array12, array12.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BOBSLED_BONANZA)
			{
				ZombieType[] array13 = new ZombieType[]
				{
					ZombieType.ZOMBIE_BOBSLED,
					ZombieType.ZOMBIE_ZAMBONI
				};
				this.InitZombieWavesFromList(array13, array13.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_AIR_RAID)
			{
				ZombieType[] array14 = new ZombieType[]
				{
					ZombieType.ZOMBIE_BALLOON
				};
				this.InitZombieWavesFromList(array14, array14.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
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
				this.InitZombieWavesFromList(array15, array15.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND)
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
				this.InitZombieWavesFromList(array16, array16.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				ZombieType[] array17 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_FOOTBALL
				};
				this.InitZombieWavesFromList(array17, array17.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
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
				this.InitZombieWavesFromList(array18, array18.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS)
			{
				ZombieType[] array19 = new ZombieType[]
				{
					ZombieType.ZOMBIE_PEA_HEAD,
					ZombieType.ZOMBIE_WALLNUT_HEAD
				};
				this.InitZombieWavesFromList(array19, array19.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WAR_AND_PEAS_2)
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
				this.InitZombieWavesFromList(array20, array20.Length);
			}
			else if (this.mApp.IsShovelLevel())
			{
				ZombieType[] array21 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE
				};
				this.InitZombieWavesFromList(array21, array21.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING || this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
			{
				ZombieType[] array22 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL,
					ZombieType.ZOMBIE_POLEVAULTER,
					ZombieType.ZOMBIE_NEWSPAPER
				};
				this.InitZombieWavesFromList(array22, array22.Length);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING_2)
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
				this.InitZombieWavesFromList(array23, array23.Length);
			}
			else if (this.mApp.IsStormyNightLevel())
			{
				ZombieType[] array24 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_DOLPHIN_RIDER,
					ZombieType.ZOMBIE_BALLOON
				};
				this.InitZombieWavesFromList(array24, array24.Length);
			}
			else
			{
				ZombieType[] array25 = new ZombieType[]
				{
					ZombieType.ZOMBIE_NORMAL,
					ZombieType.ZOMBIE_TRAFFIC_CONE,
					ZombieType.ZOMBIE_PAIL
				};
				this.InitZombieWavesFromList(array25, array25.Length);
			}
			if (this.mApp.CanSpawnYetis() && !this.mApp.IsWhackAZombieLevel() && !this.mApp.IsLittleTroubleLevel())
			{
				this.mBoard.mZombieAllowed[19] = true;
			}
		}

		public void UpdateSlotMachine()
		{
			int num = TodCommon.ClampInt(this.mBoard.mSunMoney, 0, 2000);
			if (num >= 1900)
			{
				this.mBoard.DisplayAdvice("[ADVICE_ALMOST_THERE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_ALMOST_THERE);
			}
			if (num >= 2000)
			{
				this.SpawnLevelAward(4, 2);
				this.mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
			}
			this.mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 2000, num, 0, 150, TodCurves.CURVE_LINEAR);
			if (!this.mBoard.mAdvice.IsBeingDisplayed())
			{
				if (this.slotMachineMessageCached != 2000)
				{
					this.slotMachineMessage = TodCommon.TodReplaceNumberString("[ADVICE_SLOT_MACHINE_COLLECT_SUN]", "{SCORE}", 2000);
					this.slotMachineMessageCached = 2000;
				}
				this.mBoard.DisplayAdvice(this.slotMachineMessage, MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_SLOT_MACHINE_COLLECT_SUN);
			}
			if (this.mChallengeState != ChallengeState.STATECHALLENGE_SLOT_MACHINE_ROLLING)
			{
				if (!this.mBoard.mAdvice.IsBeingDisplayed() && !this.mBoard.HasLevelAwardDropped())
				{
					this.mBoard.DisplayAdviceAgain("[ADVICE_SLOT_MACHINE_SPIN_AGAIN]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_SLOT_MACHINE_SPIN_AGAIN);
				}
				return;
			}
			if (this.mBoard.mSeedBank.mSeedPackets[0].mSlotMachineCountDown > 0)
			{
				return;
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimChallenge);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_return, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
			this.mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
			SeedType mPacketType = this.mBoard.mSeedBank.mSeedPackets[0].mPacketType;
			SeedType mPacketType2 = this.mBoard.mSeedBank.mSeedPackets[1].mPacketType;
			SeedType mPacketType3 = this.mBoard.mSeedBank.mSeedPackets[2].mPacketType;
			if (mPacketType != mPacketType2 || mPacketType2 != mPacketType3)
			{
				if (mPacketType == mPacketType2 || mPacketType2 == mPacketType3 || mPacketType == mPacketType3)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
					SeedType seedType;
					if (mPacketType == mPacketType2 || mPacketType == mPacketType3)
					{
						seedType = mPacketType;
					}
					else
					{
						seedType = mPacketType2;
					}
					if (seedType == SeedType.SEED_SLOT_MACHINE_DIAMOND)
					{
						this.mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_2_DIAMONDS]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
						this.mBoard.AddCoin(360, 85, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
						return;
					}
					if (seedType == SeedType.SEED_SLOT_MACHINE_SUN)
					{
						this.mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_2_SUNS]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
						int num2 = 4;
						for (int i = 0; i < num2; i++)
						{
							int theX = 320 + i * 60 / num2;
							this.mBoard.AddCoin(theX, 85, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
						}
						return;
					}
					this.mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_2_OF_A_KIND]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
					Coin coin = this.mBoard.AddCoin(360, 85, CoinType.COIN_USABLE_SEED_PACKET, CoinMotion.COIN_MOTION_COIN);
					coin.mUsableSeedType = seedType;
				}
				return;
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
			if (mPacketType == SeedType.SEED_SLOT_MACHINE_DIAMOND)
			{
				this.mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_DIAMOND_JACKPOT]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
				int num3 = 5;
				for (int j = 0; j < num3; j++)
				{
					int theX2 = 320 + j * 60 / num3;
					this.mBoard.AddCoin(theX2, 85, CoinType.COIN_DIAMOND, CoinMotion.COIN_MOTION_COIN);
				}
				return;
			}
			if (mPacketType == SeedType.SEED_SLOT_MACHINE_SUN)
			{
				this.mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_SUN_JACKPOT]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
				int num4 = 20;
				for (int k = 0; k < num4; k++)
				{
					int theX3 = 320 + k * 60 / num4;
					this.mBoard.AddCoin(theX3, 85, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
				}
				return;
			}
			this.mBoard.DisplayAdvice("[ADVICE_SLOT_MACHINE_3_OF_A_KIND]", MessageStyle.MESSAGE_STYLE_SLOT_MACHINE, AdviceType.ADVICE_NONE);
			int num5 = 3;
			for (int l = 0; l < num5; l++)
			{
				int theX4 = 320 + l * 60 / num5;
				Coin coin2 = this.mBoard.AddCoin(theX4, 85, CoinType.COIN_USABLE_SEED_PACKET, CoinMotion.COIN_MOTION_COIN);
				coin2.mUsableSeedType = mPacketType;
			}
		}

		public void DrawSlotMachine(Graphics g)
		{
			if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
			{
				return;
			}
			Graphics @new = Graphics.GetNew(g);
			@new.mTransX = this.mBoard.mX;
			@new.mTransY = this.mBoard.mY;
			TRect trect = this.SlotMachineRect();
			@new.DrawImage(Resources.IMAGE_SLOTMACHINE_OVERLAY, trect.mX, trect.mY);
			Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimChallenge);
			reanimation.Draw(@new);
			if (this.mSlotMachineRollCount < 3 && this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_NORMAL && this.mChallengeState != ChallengeState.STATECHALLENGE_SLOT_MACHINE_ROLLING && !this.mBoard.HasLevelAwardDropped())
			{
				byte b = (byte)(150.0 * Math.Sin((double)this.mBoard.mMainCounter % 150.0 / 150.0 * 3.1415927410125732));
				SexyColor aColor = new SexyColor((int)b, (int)b, (int)b, 255);
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
			if (this.mBoard.mCurrentWave == this.mBoard.mNumWaves && this.mBoard.mZombieCountDown == 0)
			{
				return;
			}
			this.mBoard.mZombieCountDown -= 3;
			int num = 300;
			if (this.mBoard.mZombieCountDown >= 100 && this.mBoard.mZombieCountDown < 103 && this.mBoard.mCurrentWave > 0)
			{
				int graveStoneCount = this.mBoard.GetGraveStoneCount();
				int num2 = 5;
				int theGraveCount = Math.Max(1, num2 - graveStoneCount);
				this.WhackAZombiePlaceGraves(theGraveCount);
			}
			if (this.mBoard.mZombieCountDown >= 5 && this.mBoard.mZombieCountDown < 8)
			{
				this.mBoard.NextWaveComing();
			}
			if (this.mBoard.mZombieCountDown >= 0 && this.mBoard.mZombieCountDown < 3)
			{
				this.mBoard.mZombieCountDown = 2000;
				this.mBoard.mZombieCountDownStart = this.mBoard.mZombieCountDown;
				this.mBoard.mCurrentWave++;
				if (this.mBoard.mCurrentWave == this.mBoard.mNumWaves)
				{
					this.mChallengeStateCounter = 300;
				}
				else
				{
					this.mChallengeStateCounter = 3;
				}
			}
			else if (this.mBoard.mZombieCountDown < num)
			{
				return;
			}
			this.mChallengeStateCounter -= 3;
			if (this.mChallengeStateCounter < 0 || this.mChallengeStateCounter >= 3)
			{
				return;
			}
			int num3 = TodCommon.ClampInt((this.mBoard.mCurrentWave - 1) * 6 / 12, 0, 5);
			int num4 = 1;
			ZombieType theZombieType = ZombieType.ZOMBIE_NORMAL;
			int num5 = RandomNumbers.NextNumber(100);
			int num6 = RandomNumbers.NextNumber(100);
			bool flag = this.mBoard.mCurrentWave == this.mBoard.mNumWaves;
			if (flag)
			{
				num4 = 20;
			}
			else if (num5 < this.aTripleChance[num3])
			{
				num4 = 3;
			}
			else if (num5 < this.aTripleChance[num3] + this.aDoubleChance[num3])
			{
				num4 = 2;
			}
			if (num6 < this.aPailChance[num3] && num4 < 3)
			{
				theZombieType = ZombieType.ZOMBIE_PAIL;
			}
			else if (num6 < this.aPailChance[num3] + this.aConeChance[num3])
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
			while (this.mBoard.IterateGridItems(ref gridItem, ref num8))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_GRAVESTONE)
				{
					Plant topPlantAt = this.mBoard.GetTopPlantAt(gridItem.mGridX, gridItem.mGridY, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
					if (topPlantAt == null || topPlantAt.mSeedType != SeedType.SEED_GRAVEBUSTER)
					{
						Challenge.aGridPickItemArray[num7] = gridItem;
						Challenge.aGridPicks[num7].mItem = num7;
						Challenge.aGridPicks[num7].mWeight = 1;
						num7++;
					}
				}
			}
			float theMax = (float)TodCommon.TodAnimateCurve(1, 12, this.mBoard.mCurrentWave, 1, 3, TodCurves.CURVE_EASE_IN);
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
				Zombie zombie = this.mBoard.AddZombie(theZombieType, this.mBoard.mCurrentWave);
				if (zombie == null)
				{
					break;
				}
				zombie.RiseFromGrave(gridItem2.mGridX, gridItem2.mGridY);
				zombie.mPhaseCounter = 50;
				zombie.mVelX = TodCommon.RandRangeFloat(0.5f, theMax);
				zombie.UpdateAnimSpeed();
			}
			int theMin = TodCommon.TodAnimateCurve(1, 12, this.mBoard.mCurrentWave, 100, 30, TodCurves.CURVE_LINEAR);
			int theMax2 = TodCommon.TodAnimateCurve(1, 12, this.mBoard.mCurrentWave, 200, 60, TodCurves.CURVE_LINEAR);
			this.mChallengeStateCounter = TodCommon.RandRangeInt(theMin, theMax2);
			if (flag)
			{
				this.mBoard.mZombieCountDown = 0;
				this.mChallengeStateCounter = 0;
			}
		}

		public bool UpdateZombieSpawning()
		{
			if (this.mApp.IsWhackAZombieLevel())
			{
				this.WhackAZombieSpawning();
				return true;
			}
			return this.mApp.IsFinalBossLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ICE || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN || this.mApp.mGameMode == GameMode.GAMEMODE_TREE_OF_WISDOM || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZOMBIQUARIUM || this.mApp.IsIZombieLevel() || this.mApp.IsSquirrelLevel() || this.mApp.IsScaryPotterLevel() || (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_LAST_STAND && this.mChallengeState != ChallengeState.STATECHALLENGE_LAST_STAND_ONSLAUGHT);
		}

		public void BeghouledClearCrater(int theCount)
		{
			this.mBoard.ClearAdvice(AdviceType.ADVICE_BEGHOULED_USE_CRATER_1);
			this.mBoard.ClearAdvice(AdviceType.ADVICE_BEGHOULED_USE_CRATER_2);
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (this.mBeghouledEated[i, j])
					{
						this.mBeghouledEated[i, j] = false;
						theCount--;
						if (theCount == 0)
						{
							this.BeghouledUpdateCraters();
							return;
						}
					}
				}
			}
		}

		public void MouseDownWhackAZombie(int x, int y)
		{
			x = (int)((float)x * Constants.IS);
			y = (int)((float)y * Constants.IS);
			Reanimation reanimation = this.mApp.ReanimationGet(this.mBoard.mCursorObject.mReanimCursorID);
			reanimation.mAnimTime = 0.2f;
			this.mApp.PlayFoley(FoleyType.FOLEY_SWING);
			Zombie zombie = null;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie2 = this.mBoard.mZombies[i];
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
						this.mApp.PlayFoley(FoleyType.FOLEY_SHIELD_HIT);
					}
					else if (zombie.mHelmType == HelmType.HELMTYPE_TRAFFIC_CONE)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
					}
					zombie.TakeHelmDamage(900, 0U);
					return;
				}
				this.mApp.PlayFoley(FoleyType.FOLEY_BONK);
				this.mApp.AddTodParticle(TodCommon.PixelAligned((float)x - 3f), TodCommon.PixelAligned((float)y + 9f), 800000, ParticleEffect.PARTICLE_POW);
				zombie.DieWithLoot();
				this.mBoard.ClearCursor();
			}
		}

		public bool IsStormyNightPitchBlack()
		{
			return this.mApp.IsStormyNightLevel() && (this.mChallengeState != ChallengeState.STATECHALLENGE_STORM_FLASH_1 || this.mChallengeStateCounter >= 300) && (this.mChallengeState != ChallengeState.STATECHALLENGE_STORM_FLASH_2 || this.mChallengeStateCounter >= 300) && (this.mChallengeState != ChallengeState.STATECHALLENGE_STORM_FLASH_3 || this.mChallengeStateCounter >= 150);
		}

		public void DrawStormNight(Graphics g)
		{
			if (this.mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_1 && this.mChallengeStateCounter < 300)
			{
				if (this.mChallengeStateCounter > 150)
				{
					this.DrawStormFlash(g, this.mChallengeStateCounter - 150, 255);
				}
				else
				{
					this.DrawStormFlash(g, this.mChallengeStateCounter, 92);
				}
			}
			else if (this.mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_2 && this.mChallengeStateCounter < 300)
			{
				this.DrawStormFlash(g, this.mChallengeStateCounter / 2, 255);
			}
			else if (this.mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_3 && this.mChallengeStateCounter < 150)
			{
				this.DrawStormFlash(g, this.mChallengeStateCounter, 255);
			}
			else
			{
				g.SetColor(new SexyColor(0, 0, 0, 255));
				g.FillRect(-1000, -1000, Constants.BOARD_WIDTH + 2000, Constants.BOARD_HEIGHT + 2000);
			}
			this.mBoard.DrawUIBottom(g);
			this.mBoard.DrawTopRightUI(g);
		}

		public void UpdateStormyNight()
		{
			if (this.mBoard.mPaused)
			{
				if (this.mChallengeStateCounter == 1)
				{
					return;
				}
				if (this.mChallengeStateCounter == 150 && this.mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_1)
				{
					this.mChallengeStateCounter = 1;
					return;
				}
			}
			this.mChallengeStateCounter--;
			if ((this.mChallengeStateCounter == 300 && this.mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_1) || (this.mChallengeStateCounter == 150 && this.mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_1) || (this.mChallengeStateCounter == 300 && this.mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_2) || (this.mChallengeStateCounter == 150 && this.mChallengeState == ChallengeState.STATECHALLENGE_STORM_FLASH_3))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_THUNDER);
			}
			if (this.mChallengeStateCounter <= 0)
			{
				if (this.mApp.mGameScene == GameScenes.SCENE_ZOMBIES_WON)
				{
					this.mChallengeStateCounter = 150 + TodCommon.RandRangeInt(-50, 50);
					this.mChallengeState = ChallengeState.STATECHALLENGE_STORM_FLASH_3;
					return;
				}
				if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING)
				{
					this.mChallengeStateCounter = 0;
					this.mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
					return;
				}
				int count = this.mBoard.mZombies.Count;
				for (int i = 0; i < count; i++)
				{
					Zombie zombie = this.mBoard.mZombies[i];
					if (!zombie.mDead && zombie.mZombieType == ZombieType.ZOMBIE_YETI)
					{
						this.mChallengeStateCounter = 150 + TodCommon.RandRangeInt(200, 300);
						this.mChallengeState = (ChallengeState)TodCommon.RandRangeInt(5, 7);
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
				this.mChallengeStateCounter = 150 + TodCommon.RandRangeInt(300, theMax);
				this.mChallengeState = (ChallengeState)TodCommon.RandRangeInt(5, 7);
			}
		}

		public void InitLevel()
		{
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
			{
				this.mChallengeStateCounter = 100;
				this.mApp.PlayFoley(FoleyType.FOLEY_RAIN);
			}
			if (this.mApp.IsStormyNightLevel())
			{
				this.mChallengeState = ChallengeState.STATECHALLENGE_STORM_FLASH_2;
				this.mChallengeStateCounter = 150;
				this.mApp.PlayFoley(FoleyType.FOLEY_RAIN);
			}
			if (this.mApp.IsFinalBossLevel())
			{
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_CABBAGEPULT);
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_JALAPENO);
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_CABBAGEPULT);
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_ICESHROOM);
				this.mConveyorBeltCounter = 1000;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN)
			{
				this.mApp.mZenGarden.mGardenType = GardenType.GARDEN_MAIN;
				this.mApp.mZenGarden.ZenGardenInitLevel(false);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_POTATOMINE);
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_TALLNUT);
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_MELONPULT);
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_MAGNETSHROOM);
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_INSTANT_COFFEE);
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_MELONPULT);
				this.mConveyorBeltCounter = 1000;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
			{
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_PEASHOOTER);
				this.mBoard.mSeedBank.AddSeed(SeedType.SEED_ICESHROOM);
				this.mConveyorBeltCounter = 1000;
			}
			if (this.mApp.IsIZombieLevel())
			{
				this.IZombieInitLevel();
			}
			if (this.mApp.IsScaryPotterLevel())
			{
				this.ScaryPotterPopulate();
			}
			if (this.mApp.IsFirstTimeAdventureMode() && this.mBoard.mLevel == 5)
			{
				this.mBoard.NewPlant(5, 1, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
				this.mBoard.NewPlant(7, 2, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
				this.mBoard.NewPlant(6, 3, SeedType.SEED_PEASHOOTER, SeedType.SEED_NONE);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				this.mChallengeGridX = -1;
				this.mChallengeGridY = -1;
			}
		}

		public void SpawnZombieWave()
		{
			if (this.mApp.IsContinuousChallenge() && this.mBoard.mCurrentWave == this.mBoard.mNumWaves)
			{
				this.mBoard.mCurrentWave = this.mBoard.mNumWaves - 1;
				for (int i = 0; i < 50; i++)
				{
					ZombieType zombieType = this.mBoard.mZombiesInWave[this.mBoard.mCurrentWave, i];
					if (zombieType == ZombieType.ZOMBIE_INVALID)
					{
						break;
					}
					if (zombieType == ZombieType.ZOMBIE_FLAG)
					{
						this.mBoard.mZombiesInWave[this.mBoard.mCurrentWave, i] = ZombieType.ZOMBIE_NORMAL;
					}
				}
			}
			bool flag = this.mBoard.IsFlagWave(this.mBoard.mCurrentWave);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_GRAVE_DANGER && this.mBoard.mCurrentWave != this.mBoard.mNumWaves - 1)
			{
				if (flag)
				{
					this.mBoard.SpawnZombiesFromGraves();
				}
				else if (this.mBoard.mCurrentWave > 5)
				{
					this.GraveDangerSpawnRandomGrave();
				}
			}
			if (this.mApp.IsSurvivalMode() && this.mBoard.mBackground == BackgroundType.BACKGROUND_2_NIGHT && this.mBoard.mCurrentWave == this.mBoard.mNumWaves - 1)
			{
				int graveStoneCount = this.mBoard.GetGraveStoneCount();
				if (this.mApp.IsSurvivalNormal(this.mApp.mGameMode))
				{
					if (graveStoneCount < 8)
					{
						this.GraveDangerSpawnRandomGrave();
					}
				}
				else if (graveStoneCount < 12)
				{
					this.GraveDangerSpawnRandomGrave();
				}
			}
			if (this.mApp.IsBungeeBlitzLevel() && flag)
			{
				this.mBoard.DisplayAdvice("[ADVICE_BUNGEES_INCOMING]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
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
					if (this.mBoard.CanAddGraveStoneAt(i, j))
					{
						Plant topPlantAt = this.mBoard.GetTopPlantAt(i, j, PlantPriority.TOPPLANT_ANY);
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
			this.GraveDangerSpawnGraveAt(todWeightedGridArray.mX, todWeightedGridArray.mY);
		}

		public void GraveDangerSpawnGraveAt(int x, int y)
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant.mPlantCol == x && plant.mRow == y)
				{
					plant.Die();
				}
			}
			this.mBoard.mEnableGraveStones = true;
			GridItem gridItem = this.mBoard.AddAGraveStone(x, y);
			if (gridItem != null)
			{
				gridItem.AddGraveStoneParticles();
			}
		}

		public void SpawnLevelAward(int theGridX, int theGridY)
		{
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			float num = (float)(this.mBoard.GridToPixelX(theGridX, theGridY) + 40);
			float num2 = (float)(this.mBoard.GridToPixelY(theGridX, theGridY) + 40);
			CoinType theCoinType;
			if (this.mApp.IsAdventureMode() && this.mApp.IsFirstTimeAdventureMode())
			{
				theCoinType = CoinType.COIN_FINAL_SEED_PACKET;
			}
			else if (this.mApp.IsAdventureMode() || this.mApp.HasBeatenChallenge(this.mApp.mGameMode))
			{
				theCoinType = CoinType.COIN_AWARD_MONEY_BAG;
			}
			else
			{
				theCoinType = CoinType.COIN_TROPHY;
			}
			this.mBoard.mLevelAwardSpawned = true;
			this.mApp.mBoardResult = BoardResult.BOARDRESULT_WON;
			this.mApp.PlayFoley(FoleyType.FOLEY_SPAWN_SUN);
			this.mBoard.AddCoin((int)num, (int)num2, theCoinType, CoinMotion.COIN_MOTION_COIN);
			this.mApp.AddTodParticle(400f, 300f, 400000, ParticleEffect.PARTICLE_SCREEN_FLASH);
			if (!this.mApp.IsIZombieLevel())
			{
				int count = this.mBoard.mZombies.Count;
				for (int i = 0; i < count; i++)
				{
					Zombie zombie = this.mBoard.mZombies[i];
					if (!zombie.mDead && !zombie.IsDeadOrDying())
					{
						zombie.TakeDamage(1800, 0U);
					}
				}
			}
		}

		public void BeghouledScore(int x, int y, int theNumPlants, bool theIsHorizontal)
		{
			this.mApp.PlayFoley(FoleyType.FOLEY_ART_CHALLENGE);
			float num = (float)this.mBoard.GridToPixelX(x, y);
			float num2 = (float)this.mBoard.GridToPixelY(x, y);
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
			this.mChallengeScore++;
			if (this.mBoard.mSeedBank.mNumPackets == 0)
			{
				this.mBoard.mSeedBank.mSeedPackets[0].SetPacketType(SeedType.SEED_REPEATER, SeedType.SEED_NONE);
				this.mBoard.mSeedBank.mSeedPackets[1].SetPacketType(SeedType.SEED_FUMESHROOM, SeedType.SEED_NONE);
				this.mBoard.mSeedBank.mSeedPackets[2].SetPacketType(SeedType.SEED_TALLNUT, SeedType.SEED_NONE);
				this.mBoard.mSeedBank.mSeedPackets[3].SetPacketType(SeedType.SEED_BEGHOULED_BUTTON_SHUFFLE, SeedType.SEED_NONE);
				this.mBoard.mSeedBank.mNumPackets = 4;
				this.mBoard.DisplayAdvice("[ADVICE_BEGHOULED_SAVE_SUN]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_SAVE_SUN);
				if (this.BeghouledCanClearCrater())
				{
					this.mBoard.mSeedBank.mSeedPackets[4].SetPacketType(SeedType.SEED_BEGHOULED_BUTTON_CRATER, SeedType.SEED_NONE);
					this.mBoard.mSeedBank.mNumPackets = 5;
				}
			}
			else
			{
				if (!this.mBoard.mAdvice.IsBeingDisplayed())
				{
					string theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_BEGHOULED_MATCH_3]", "{SCORE}", 75);
					this.mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_BEGHOULED_MATCH_3);
				}
				if (this.mChallengeScore >= 70)
				{
					this.mBoard.DisplayAdvice("[ADVICE_BEGHOULED_ALMOST_THERE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_ALMOST_THERE);
				}
			}
			if (this.mChallengeScore >= 75)
			{
				this.mChallengeScore = 75;
				this.SpawnLevelAward(x, y);
				this.mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
			}
			else
			{
				int num3 = theNumPlants - 2 + this.mBeghouledMatchesThisMove;
				if (theNumPlants >= 5)
				{
					num3 += 2;
				}
				num3 = TodCommon.ClampInt(num3, 1, 5);
				for (int i = 0; i < num3; i++)
				{
					this.mBoard.AddCoin((int)(num - 10f + 20f * (float)i), (int)num2, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_COIN);
				}
			}
			this.mBeghouledMatchesThisMove++;
		}

		public void DrawStormFlash(Graphics g, int theTime, int theMaxAmount)
		{
			RandomNumbers.Seed(this.mBoard.mMainCounter / 6);
			int num = TodCommon.TodAnimateCurve(150, 0, theTime, 255 - theMaxAmount, 255, TodCurves.CURVE_LINEAR);
			int theAlpha = TodCommon.ClampInt((int)((float)num + RandomNumbers.NextNumber(64f) - 32f), 0, 255);
			g.SetColor(new SexyColor(0, 0, 0, theAlpha));
			g.FillRect(-1000, -1000, Constants.BOARD_WIDTH + 2000, Constants.BOARD_HEIGHT + 2000);
			int theAlpha2 = TodCommon.TodAnimateCurve(150, 75, theTime, theMaxAmount, 0, TodCurves.CURVE_LINEAR);
			g.SetColor(new SexyColor(255, 255, 255, theAlpha2));
			g.FillRect(-1000, -1000, Constants.BOARD_WIDTH + 2000, Constants.BOARD_HEIGHT + 2000);
		}

		public void UpdateRainingSeeds()
		{
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			this.mChallengeStateCounter--;
			if (this.mChallengeStateCounter != 0)
			{
				return;
			}
			this.mChallengeStateCounter = 500 + RandomNumbers.NextNumber(500);
			int theX = 100 + RandomNumbers.NextNumber(550);
			Coin coin = this.mBoard.AddCoin(theX, 60, CoinType.COIN_USABLE_SEED_PACKET, CoinMotion.COIN_MOTION_FROM_SKY_SLOW);
			do
			{
				coin.mUsableSeedType = (SeedType)RandomNumbers.NextNumber(this.mApp.GetSeedsAvailable());
			}
			while (this.mBoard.SeedNotRecommendedForLevel(coin.mUsableSeedType) != 0U || !this.mApp.HasSeedType(coin.mUsableSeedType) || Plant.IsUpgrade(coin.mUsableSeedType) || coin.mUsableSeedType == SeedType.SEED_SUNFLOWER || coin.mUsableSeedType == SeedType.SEED_TWINSUNFLOWER || coin.mUsableSeedType == SeedType.SEED_INSTANT_COFFEE || coin.mUsableSeedType == SeedType.SEED_UMBRELLA || coin.mUsableSeedType == SeedType.SEED_SUNSHROOM || coin.mUsableSeedType == SeedType.SEED_IMITATER);
			int theTimeAge = this.mBoard.CountPlantByType(SeedType.SEED_LILYPAD);
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
			this.mBoard.AddZombie(ZombieType.ZOMBIE_BOSS, 0);
		}

		public void UpdateConveyorBelt()
		{
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			this.mBoard.mSeedBank.UpdateConveyorBelt();
			this.mConveyorBeltCounter--;
			if (this.mConveyorBeltCounter > 0)
			{
				return;
			}
			float num = 1f;
			if (this.mApp.IsFinalBossLevel())
			{
				num = 0.875f;
			}
			else if (this.mApp.IsShovelLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
			{
				num = 1.5f;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
			{
				num = 2f;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
			{
				num = 3f;
			}
			if (this.mBoard.mSeedBank.GetNumSeedsOnConveyorBelt() > 8)
			{
				this.mConveyorBeltCounter = 1000 * (int)num;
			}
			else if (this.mBoard.mSeedBank.GetNumSeedsOnConveyorBelt() > 6)
			{
				this.mConveyorBeltCounter = 500 * (int)num;
			}
			else if (this.mBoard.mSeedBank.GetNumSeedsOnConveyorBelt() > 4)
			{
				this.mConveyorBeltCounter = 425 * (int)num;
			}
			else
			{
				this.mConveyorBeltCounter = 400 * (int)num;
			}
			for (int i = 0; i < 20; i++)
			{
				Challenge.aSeedPickArray[i].Reset();
			}
			int num2 = 0;
			if (this.mBoard.mLevel == 10)
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
			else if (this.mBoard.mLevel == 20)
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
			else if (this.mBoard.mLevel == 30)
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
			else if (this.mBoard.mLevel == 40)
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
			else if (this.mApp.IsFinalBossLevel())
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
			else if (this.mApp.IsShovelLevel())
			{
				Challenge.aSeedPickArray[num2].mItem = 0;
				Challenge.aSeedPickArray[num2].mWeight = 100;
				num2++;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_WALLNUT_BOWLING_2)
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
			else if (this.mApp.IsWallnutBowlingLevel())
			{
				Challenge.aSeedPickArray[num2].mItem = 3;
				Challenge.aSeedPickArray[num2].mWeight = 85;
				num2++;
				Challenge.aSeedPickArray[num2].mItem = 49;
				Challenge.aSeedPickArray[num2].mWeight = 15;
				num2++;
			}
			else if (this.mApp.IsLittleTroubleLevel())
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
			else if (this.mApp.IsStormyNightLevel())
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
			else if (this.mApp.IsBungeeBlitzLevel())
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
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_PORTAL_COMBAT)
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
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
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
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_INVISIGHOUL)
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
				int num3 = this.mBoard.mSeedBank.CountOfTypeOnConveyorBelt((SeedType)todWeightedArray.mItem);
				if (seedType != SeedType.SEED_GRAVEBUSTER)
				{
					goto IL_D4E;
				}
				int graveStoneCount = this.mBoard.GetGraveStoneCount();
				int num4 = this.mBoard.CountPlantByType(seedType);
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
					int num5 = this.mBoard.CountPlantByType(seedType);
					int theTimeEnd = 18;
					todWeightedArray.mWeight = TodCommon.TodAnimateCurve(0, theTimeEnd, num5 + num3, todWeightedArray.mWeight, 1, TodCurves.CURVE_LINEAR);
				}
				if (seedType == SeedType.SEED_FLOWERPOT)
				{
					int num6 = this.mBoard.CountPlantByType(seedType);
					int theTimeEnd2 = 35;
					if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_COLUMN)
					{
						theTimeEnd2 = 45;
					}
					todWeightedArray.mWeight = TodCommon.TodAnimateCurve(0, theTimeEnd2, num6 + num3, todWeightedArray.mWeight, 1, TodCurves.CURVE_LINEAR);
				}
				if (this.mApp.IsFinalBossLevel())
				{
					if (seedType == SeedType.SEED_MELONPULT || seedType == SeedType.SEED_KERNELPULT || seedType == SeedType.SEED_CABBAGEPULT)
					{
						int num7 = this.mBoard.CountEmptyPotsOrLilies(SeedType.SEED_FLOWERPOT);
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
						Zombie bossZombie = this.mBoard.GetBossZombie();
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
				if (seedType == this.mLastConveyorSeedType)
				{
					todWeightedArray.mWeight /= 2;
					goto IL_E81;
				}
				goto IL_E81;
			}
			seedType = (SeedType)TodCommon.TodPickFromWeightedArray(Challenge.aSeedPickArray, num2);
			this.mBoard.mSeedBank.AddSeed(seedType);
			this.mLastConveyorSeedType = seedType;
		}

		public void PortalStart()
		{
			this.mChallengeStateCounter = 9000;
			GridItem newGridItem = GridItem.GetNewGridItem();
			newGridItem.mGridItemType = GridItemType.GRIDITEM_PORTAL_SQUARE;
			newGridItem.mGridX = 2;
			newGridItem.mGridY = 0;
			newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, newGridItem.mGridY, 0);
			newGridItem.OpenPortal();
			this.mBoard.mGridItems.Add(newGridItem);
			GridItem newGridItem2 = GridItem.GetNewGridItem();
			newGridItem2.mGridItemType = GridItemType.GRIDITEM_PORTAL_SQUARE;
			newGridItem2.mGridX = 9;
			newGridItem2.mGridY = 1;
			newGridItem2.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, newGridItem2.mGridY, 0);
			newGridItem2.OpenPortal();
			this.mBoard.mGridItems.Add(newGridItem2);
			GridItem newGridItem3 = GridItem.GetNewGridItem();
			newGridItem3.mGridItemType = GridItemType.GRIDITEM_PORTAL_CIRCLE;
			newGridItem3.mGridX = 9;
			newGridItem3.mGridY = 3;
			newGridItem3.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, newGridItem3.mGridY, 0);
			newGridItem3.OpenPortal();
			this.mBoard.mGridItems.Add(newGridItem3);
			GridItem newGridItem4 = GridItem.GetNewGridItem();
			newGridItem4.mGridItemType = GridItemType.GRIDITEM_PORTAL_CIRCLE;
			newGridItem4.mGridX = 2;
			newGridItem4.mGridY = 4;
			newGridItem4.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PARTICLE, newGridItem4.mGridY, 0);
			newGridItem4.OpenPortal();
			this.mBoard.mGridItems.Add(newGridItem4);
			this.mBoard.mZombieCountDown = 200;
			this.mBoard.mZombieCountDownStart = this.mBoard.mZombieCountDown;
			this.mConveyorBeltCounter = 200;
		}

		public void UpdatePortalCombat()
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridItemState != GridItemState.GRIDITEM_STATE_PORTAL_CLOSED && (gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_CIRCLE || gridItem.mGridItemType == GridItemType.GRIDITEM_PORTAL_SQUARE))
				{
					this.UpdatePortal(gridItem);
				}
			}
			if (this.mBoard.HasLevelAwardDropped())
			{
				this.mBoard.ClearAdvice(AdviceType.ADVICE_PORTAL_RELOCATING);
				return;
			}
			this.mChallengeStateCounter--;
			if (this.mChallengeStateCounter == 500)
			{
				this.mBoard.DisplayAdviceAgain("[ADVICE_PORTAL_RELOCATING]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_PORTAL_RELOCATING);
			}
			if (this.mChallengeStateCounter <= 0)
			{
				this.mBoard.ClearAdvice(AdviceType.ADVICE_PORTAL_RELOCATING);
				this.mChallengeStateCounter = 6000;
				this.MoveAPortal();
			}
		}

		public GridItem GetOtherPortal(GridItem thePortal)
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
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
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && zombie.mRow == thePortal.mGridY && zombie.mLastPortalX != thePortal.mGridX)
				{
					TRect zombieRect = zombie.GetZombieRect();
					int num = zombieRect.mX + zombieRect.mWidth / 2;
					int num2 = thePortal.mGridX * 80 + 25;
					int num3 = Math.Abs(num - num2);
					if (num3 <= 45)
					{
						GridItem otherPortal = this.GetOtherPortal(thePortal);
						if (otherPortal != null)
						{
							int num4 = num - zombie.mX;
							num4 = (int)((float)num4 * 0.5f);
							if (zombie.IsWalkingBackwards())
							{
								num4 -= 60;
							}
							zombie.mLastPortalX = otherPortal.mGridX;
							zombie.mX = otherPortal.mGridX * 80 - num4;
							zombie.mPosX = (float)zombie.mX;
							zombie.SetRow(otherPortal.mGridY);
							zombie.mY = (int)zombie.GetPosYBasedOnRow(otherPortal.mGridY);
							zombie.mPosY = (float)zombie.mY;
							zombie.cachedZombieRectUpToDate = false;
						}
					}
				}
			}
			int num5 = -1;
			Projectile projectile = null;
			while (this.mBoard.IterateProjectiles(ref projectile, ref num5))
			{
				if (projectile.mMotionType == ProjectileMotion.MOTION_STRAIGHT && projectile.mRow == thePortal.mGridY && projectile.mLastPortalX != thePortal.mGridX)
				{
					TRect projectileRect = projectile.GetProjectileRect();
					int num6 = projectileRect.mX + projectileRect.mWidth / 2;
					int num7 = thePortal.mGridX * 80 + 55;
					int num8 = Math.Abs(num6 - num7);
					if (num8 <= 40)
					{
						GridItem otherPortal2 = this.GetOtherPortal(thePortal);
						if (otherPortal2 != null)
						{
							int num9 = num6 - projectile.mX;
							int num10 = otherPortal2.mGridY - thePortal.mGridY;
							int num11 = num10 * 100;
							projectile.mX = otherPortal2.mGridX * 80 - num9 + 90;
							projectile.mPosX = (float)projectile.mX;
							projectile.mRow = otherPortal2.mGridY;
							projectile.mY += num11;
							projectile.mPosY = (float)projectile.mY;
							projectile.mShadowY += (float)num11;
							projectile.mLastPortalX = otherPortal2.mGridX;
							projectile.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, projectile.mRow, 0);
						}
					}
				}
			}
			LawnMower lawnMower = null;
			while (this.mBoard.IterateLawnMowers(ref lawnMower))
			{
				if (lawnMower.mMowerState == LawnMowerState.MOWER_TRIGGERED && lawnMower.mRow == thePortal.mGridY && lawnMower.mLastPortalX != thePortal.mGridX)
				{
					int num12 = thePortal.mGridX * 80 + 25;
					int num13 = (int)lawnMower.mPosX + 45;
					int num14 = Math.Abs(num13 - num12);
					if (num14 <= 20)
					{
						GridItem otherPortal3 = this.GetOtherPortal(thePortal);
						if (otherPortal3 != null)
						{
							int num15 = otherPortal3.mGridY - thePortal.mGridY;
							int num16 = num15 * 100;
							lawnMower.mPosX = (float)(otherPortal3.mGridX * 80 + 25);
							lawnMower.mRow = otherPortal3.mGridY;
							lawnMower.mPosY = (float)num16;
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
			int portalDistanceToMower = this.GetPortalDistanceToMower(theGridY);
			if (portalDistanceToMower < 5)
			{
				return 0.01f;
			}
			bool flag = false;
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
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
				GridItem portalToRight = this.GetPortalToRight(num, num2);
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
				GridItem otherPortal = this.GetOtherPortal(portalToRight);
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
			while (this.mBoard.IterateGridItems(ref gridItem2, ref num))
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
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
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
			while (this.mBoard.IterateGridItems(ref gridItem, ref num2))
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
			GridItem otherPortal = this.GetOtherPortal(gridItem2);
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
					if (this.GetPortalAt(j, k) == null && otherPortal.mGridY != k && gridItem2.mGridY != k)
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
			this.mBoard.mGridItems.Add(newGridItem);
			gridItem2.ClosePortal();
		}

		public int GetPortalDistanceToMower(int theGridY)
		{
			int theGridY2 = theGridY;
			int num = 10;
			int i = 0;
			while (i < 40)
			{
				GridItem portalToLeft = this.GetPortalToLeft(num, theGridY2);
				if (portalToLeft == null)
				{
					i += num;
					break;
				}
				GridItem otherPortal = this.GetOtherPortal(portalToLeft);
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
			while (this.mBoard.IterateGridItems(ref gridItem2, ref num))
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
			int currentPlantCost = this.mBoard.GetCurrentPlantCost(theSeedPacket.mPacketType, SeedType.SEED_NONE);
			if (!this.mBoard.CanTakeSunMoney(currentPlantCost))
			{
				return;
			}
			if (theSeedPacket.mPacketType == SeedType.SEED_REPEATER && !this.mBoard.mChallenge.mBeghouledPurcasedUpgrade[0])
			{
				this.mBoard.mChallenge.mBeghouledPurcasedUpgrade[0] = true;
				int count = this.mBoard.mPlants.Count;
				for (int i = 0; i < count; i++)
				{
					Plant plant = this.mBoard.mPlants[i];
					if (!plant.mDead && plant.mSeedType == SeedType.SEED_PEASHOOTER)
					{
						plant.Die();
						this.mBoard.AddPlant(plant.mPlantCol, plant.mRow, SeedType.SEED_REPEATER, SeedType.SEED_NONE);
					}
				}
				theSeedPacket.Deactivate();
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_FUMESHROOM && !this.mBoard.mChallenge.mBeghouledPurcasedUpgrade[1])
			{
				this.mBoard.mChallenge.mBeghouledPurcasedUpgrade[1] = true;
				int count2 = this.mBoard.mPlants.Count;
				for (int j = 0; j < count2; j++)
				{
					Plant plant2 = this.mBoard.mPlants[j];
					if (!plant2.mDead && plant2.mSeedType == SeedType.SEED_PUFFSHROOM)
					{
						plant2.Die();
						this.mBoard.AddPlant(plant2.mPlantCol, plant2.mRow, SeedType.SEED_FUMESHROOM, SeedType.SEED_NONE);
					}
				}
				theSeedPacket.Deactivate();
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_TALLNUT && !this.mBoard.mChallenge.mBeghouledPurcasedUpgrade[2])
			{
				this.mBoard.mChallenge.mBeghouledPurcasedUpgrade[2] = true;
				int count3 = this.mBoard.mPlants.Count;
				for (int k = 0; k < count3; k++)
				{
					Plant plant3 = this.mBoard.mPlants[k];
					if (!plant3.mDead && plant3.mSeedType == SeedType.SEED_WALLNUT)
					{
						plant3.Die();
						this.mBoard.AddPlant(plant3.mPlantCol, plant3.mRow, SeedType.SEED_TALLNUT, SeedType.SEED_NONE);
					}
				}
				theSeedPacket.Deactivate();
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_BEGHOULED_BUTTON_SHUFFLE)
			{
				if (this.mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_FALLING || this.mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_MOVING)
				{
					return;
				}
				this.BeghouledShuffle();
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_BEGHOULED_BUTTON_CRATER)
			{
				if (!this.BeghouledCanClearCrater())
				{
					return;
				}
				if (this.mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_FALLING || this.mChallengeState == ChallengeState.STATECHALLENGE_BEGHOULED_MOVING)
				{
					return;
				}
				this.BeghouledClearCrater(1);
				this.BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_FALLING);
			}
			this.mBoard.TakeSunMoney(currentPlantCost);
		}

		public void BeghouledShuffle()
		{
			this.mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead)
				{
					plant.Die();
				}
			}
			this.BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_FALLING);
		}

		public bool BeghouledCanClearCrater()
		{
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (this.mBeghouledEated[i, j])
					{
						return true;
					}
				}
			}
			return false;
		}

		public void BeghouledUpdateCraters()
		{
			if (this.mBoard.mSeedBank.mNumPackets != 5)
			{
				return;
			}
			SeedPacket seedPacket = this.mBoard.mSeedBank.mSeedPackets[4];
			Debug.ASSERT(seedPacket.mPacketType == SeedType.SEED_BEGHOULED_BUTTON_CRATER);
			if (this.BeghouledCanClearCrater())
			{
				seedPacket.Activate();
				return;
			}
			seedPacket.Deactivate();
		}

		public Zombie ZombiquariumSpawnSnorkel()
		{
			Zombie zombie = this.mBoard.AddZombieInRow(ZombieType.ZOMBIE_SNORKEL, 0, 0);
			zombie.mPosX = TodCommon.RandRangeFloat(50f, 650f);
			zombie.mPosY = TodCommon.RandRangeFloat(100f, 400f);
			return zombie;
		}

		public void ZombiquariumPacketClicked(SeedPacket theSeedPacket)
		{
			int currentPlantCost = this.mBoard.GetCurrentPlantCost(theSeedPacket.mPacketType, SeedType.SEED_NONE);
			if (!this.mBoard.CanTakeSunMoney(currentPlantCost))
			{
				return;
			}
			if (theSeedPacket.mPacketType == SeedType.SEED_ZOMBIQUARIUM_SNORKEL)
			{
				int num = this.mBoard.CountZombiesOnScreen();
				if (num > 100)
				{
					return;
				}
				if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZOMBIQUARIUM_BUY_SNORKEL)
				{
					this.mBoard.ClearAdvice(AdviceType.ADVICE_ZOMBIQUARIUM_BUY_SNORKEL);
					this.mBoard.TutorialArrowRemove();
					this.mBoard.mTutorialState = TutorialState.TUTORIAL_ZOMBIQUARIUM_BOUGHT_SNORKEL;
				}
				Zombie zombie = this.ZombiquariumSpawnSnorkel();
				this.mApp.PlayFoley(FoleyType.FOLEY_ZOMBIESPLASH);
				this.mApp.AddTodParticle(zombie.mPosX + 60f, zombie.mPosY + 20f, 400000, ParticleEffect.PARTICLE_PLANTING_POOL);
			}
			else if (theSeedPacket.mPacketType == SeedType.SEED_ZOMBIQUARIUM_TROPHY)
			{
				this.SpawnLevelAward(2, 0);
				this.mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
			}
			this.mBoard.TakeSunMoney(currentPlantCost);
		}

		public void ZombiquariumMouseDown(int x, int y)
		{
			x = (int)((float)x * Constants.IS);
			y = (int)((float)y * Constants.IS);
			if (x < 80 || x > 720 || y < 90 || y > 430)
			{
				return;
			}
			int num = 0;
			int num2 = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num2))
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
			if (!this.mBoard.TakeSunMoney(5))
			{
				return;
			}
			this.ZombiquariumDropBrain(x, y);
		}

		public void ZombiquariumDropBrain(int x, int y)
		{
			this.mBoard.ClearAdvice(AdviceType.ADVICE_ZOMBIQUARIUM_CLICK_TO_FEED);
			GridItem newGridItem = GridItem.GetNewGridItem();
			newGridItem.mGridItemType = GridItemType.GRIDITEM_BRAIN;
			newGridItem.mRenderOrder = 400000;
			newGridItem.mGridX = 0;
			newGridItem.mGridY = 0;
			newGridItem.mGridItemCounter = 0;
			newGridItem.mPosX = (float)x - 15f;
			newGridItem.mPosY = (float)y - 15f;
			this.mApp.PlaySample(Resources.SOUND_TAP);
			this.mBoard.mGridItems.Add(newGridItem);
		}

		public void ZombiquariumUpdate()
		{
			if (this.mApp.mBoard.mZombies.Count == 0 && !this.mBoard.HasLevelAwardDropped())
			{
				this.mBoard.ZombiesWon(null);
				return;
			}
			if (!this.mBoard.mAdvice.IsBeingDisplayed() && !this.mBoard.mHelpDisplayed[49])
			{
				string theAdvice = TodCommon.TodReplaceNumberString("[ADVICE_ZOMBIQUARIUM_COLLECT_SUN]", "{SCORE}", 1000);
				this.mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_ZOMBIQUARIUM_COLLECT_SUN);
			}
			int num = TodCommon.ClampInt(this.mBoard.mSunMoney, 0, 1000);
			this.mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 1000, num, 0, 150, TodCurves.CURVE_LINEAR);
			if (num >= 900)
			{
				this.mBoard.DisplayAdvice("[ADVICE_ALMOST_THERE]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_ALMOST_THERE);
			}
			if (num >= 110 && this.mBoard.mTutorialState == TutorialState.TUTORIAL_OFF)
			{
				this.mBoard.mTutorialState = TutorialState.TUTORIAL_ZOMBIQUARIUM_BUY_SNORKEL;
				float num2 = (float)(this.mBoard.mSeedBank.mX + this.mBoard.mSeedBank.mSeedPackets[0].mX);
				float num3 = (float)(this.mBoard.mSeedBank.mY + this.mBoard.mSeedBank.mSeedPackets[0].mY);
				this.mBoard.TutorialArrowShow((int)num2, (int)num3);
				this.mBoard.DisplayAdvice("[ADVICE_ZOMBIQUARIUM_BUY_SNORKEL]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_ZOMBIQUARIUM_BUY_SNORKEL);
			}
			else if (num < 100 && this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZOMBIQUARIUM_BUY_SNORKEL)
			{
				this.mBoard.TutorialArrowRemove();
				this.mBoard.ClearAdvice(AdviceType.ADVICE_ZOMBIQUARIUM_BUY_SNORKEL);
				this.mBoard.mTutorialState = TutorialState.TUTORIAL_OFF;
			}
			if (num >= 1000 && this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZOMBIQUARIUM_BOUGHT_SNORKEL)
			{
				this.mBoard.mTutorialState = TutorialState.TUTORIAL_ZOMBIQUARIUM_CLICK_TROPHY;
				float num4 = (float)(this.mBoard.mSeedBank.mX + this.mBoard.mSeedBank.mSeedPackets[1].mX);
				float num5 = (float)(this.mBoard.mSeedBank.mY + this.mBoard.mSeedBank.mSeedPackets[1].mY);
				this.mBoard.TutorialArrowShow((int)num4, (int)num5);
				this.mBoard.DisplayAdvice("[ADVICE_ZOMBIQUARIUM_CLICK_TROPHY]", MessageStyle.MESSAGE_STYLE_HINT_TALL_FAST, AdviceType.ADVICE_ZOMBIQUARIUM_CLICK_TROPHY);
			}
			else if (num < 1000 && this.mBoard.mTutorialState == TutorialState.TUTORIAL_ZOMBIQUARIUM_CLICK_TROPHY)
			{
				this.mBoard.TutorialArrowRemove();
				this.mBoard.ClearAdvice(AdviceType.ADVICE_ZOMBIQUARIUM_CLICK_TROPHY);
				this.mBoard.mTutorialState = TutorialState.TUTORIAL_ZOMBIQUARIUM_BOUGHT_SNORKEL;
			}
			int num6 = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num6))
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
					this.mBoard.AddPlant(i, j, SeedType.SEED_WALLNUT, SeedType.SEED_NONE);
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
				this.mBoard.mGridItems.Add(newGridItem);
				if (theScaryPotType == ScaryPotType.SCARYPOT_SUN)
				{
					newGridItem.mSunCount = TodCommon.RandRangeInt(1, 3);
				}
			}
		}

		public void ScaryPotterStart()
		{
			if (this.mApp.IsAdventureMode())
			{
				this.mBoard.DisplayAdvice("[ADVICE_USE_SHOVEL_ON_POTS]", MessageStyle.MESSAGE_STYLE_HINT_STAY, AdviceType.ADVICE_USE_SHOVEL_ON_POTS);
			}
		}

		public void ScaryPotterUpdate()
		{
			if (this.mChallengeState == ChallengeState.STATECHALLENGE_SCARY_POTTER_MALLETING)
			{
				Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimChallenge);
				if (reanimation.mLoopCount > 0)
				{
					GridItem scaryPotAt = this.mBoard.GetScaryPotAt(this.mChallengeGridX, this.mChallengeGridY);
					if (scaryPotAt != null)
					{
						this.ScaryPotterOpenPot(scaryPotAt);
					}
					this.mChallengeGridX = 0;
					this.mChallengeGridY = 0;
					reanimation.ReanimationDie();
					this.mReanimChallenge = null;
					this.mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
				}
			}
		}

		public void ScaryPotterOpenPot(GridItem theScaryPot)
		{
			int num = this.mBoard.GridToPixelX(theScaryPot.mGridX, theScaryPot.mGridY);
			int num2 = this.mBoard.GridToPixelY(theScaryPot.mGridX, theScaryPot.mGridY);
			switch (theScaryPot.mScaryPotType)
			{
			case ScaryPotType.SCARYPOT_SEED:
			{
				Coin coin = this.mBoard.AddCoin(num + 20, num2, CoinType.COIN_USABLE_SEED_PACKET, CoinMotion.COIN_MOTION_FROM_PLANT);
				coin.mUsableSeedType = theScaryPot.mSeedType;
				break;
			}
			case ScaryPotType.SCARYPOT_ZOMBIE:
			{
				Zombie zombie = this.mBoard.AddZombieInRow(theScaryPot.mZombieType, theScaryPot.mGridY, 0);
				zombie.mPosX = (float)num;
				break;
			}
			case ScaryPotType.SCARYPOT_SUN:
			{
				int num3 = this.ScaryPotterCountSunInPot(theScaryPot);
				for (int i = 0; i < num3; i++)
				{
					this.mBoard.AddCoin(num, num2, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
					num += 15;
				}
				break;
			}
			default:
				Debug.ASSERT(false);
				break;
			}
			theScaryPot.GridItemDie();
			if (this.mBoard.mHelpIndex == AdviceType.ADVICE_USE_SHOVEL_ON_POTS)
			{
				this.mBoard.DisplayAdvice("[ADVICE_DESTROY_POTS_TO_FINISH_LEVEL]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_DESTROY_POTS_TO_FINISIH_LEVEL);
			}
			if (this.ScaryPotterIsCompleted())
			{
				if (this.mApp.IsScaryPotterLevel() && !this.mBoard.IsFinalScaryPotterStage())
				{
					this.PuzzlePhaseComplete(theScaryPot.mGridX, theScaryPot.mGridY);
				}
				else
				{
					this.SpawnLevelAward(theScaryPot.mGridX, theScaryPot.mGridY);
				}
			}
			this.mApp.PlaySample(Resources.SOUND_BONK);
			this.mApp.PlayFoley(FoleyType.FOLEY_VASE_BREAKING);
			if (theScaryPot.mGridItemState == GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF)
			{
				this.mApp.AddTodParticle((float)(num + 20), (float)num2, 200000, ParticleEffect.PARTICLE_VASE_SHATTER_LEAF);
				return;
			}
			if (theScaryPot.mGridItemState == GridItemState.GRIDITEM_STATE_SCARY_POT_ZOMBIE)
			{
				this.mApp.AddTodParticle((float)(num + 20), (float)num2, 200000, ParticleEffect.PARTICLE_VASE_SHATTER_ZOMBIE);
				return;
			}
			this.mApp.AddTodParticle((float)(num + 20), (float)num2, 200000, ParticleEffect.PARTICLE_VASE_SHATTER);
		}

		public void ScaryPotterJackExplode(int aPosX, int aPosY)
		{
			int num = this.mBoard.PixelToGridX(aPosX, aPosY);
			int num2 = this.mBoard.PixelToGridY(aPosX, aPosY);
			int num3 = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num3))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_SCARY_POT && gridItem.mGridX >= num - 1 && gridItem.mGridX <= num + 1 && gridItem.mGridY >= num2 - 1 && gridItem.mGridY <= num2 + 1)
				{
					this.ScaryPotterOpenPot(gridItem);
				}
			}
		}

		public bool ScaryPotterIsCompleted()
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_SCARY_POT)
				{
					return false;
				}
			}
			return !this.mBoard.AreEnemyZombiesOnScreen();
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
			while (this.mBoard.IterateGridItems(ref gridItem, ref num2))
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
			if ((this.mApp.IsAdventureMode() && this.mBoard.mLevel == 35) || this.mApp.mGameMode == GameMode.GAMEMODE_QUICKPLAY_35)
			{
				if (this.mSurvivalStage == 0)
				{
					this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(4, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(5, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 5, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 4, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				}
				else if (this.mSurvivalStage == 1)
				{
					this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(4, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 4, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 5, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 4, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_FOOTBALL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
					this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
				}
				else if (this.mSurvivalStage == 2)
				{
					this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
					this.ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 5, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 5, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_HYPNOSHROOM, 5, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 6, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 2, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_DANCER, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
					this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
					this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 3);
				}
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_1)
			{
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 6, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_2)
			{
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(8, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 7, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_POTATOMINE, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 6, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_3)
			{
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 6, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 4, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_HYPNOSHROOM, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 8, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_DANCER, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_4)
			{
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PUFFSHROOM, 11, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_HYPNOSHROOM, 4, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 4, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 8, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 7, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_FOOTBALL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_5)
			{
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 6, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PUMPKINSHELL, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 4, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_HYPNOSHROOM, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_MAGNETSHROOM, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 6, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_FOOTBALL, SeedType.SEED_NONE, 3, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_6)
			{
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 7, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_TALLNUT, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_THREEPEATER, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_TORCHWOOD, 4, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 7, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_POLEVAULTER, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_FOOTBALL, SeedType.SEED_NONE, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_7)
			{
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SPIKEWEED, 13, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 10, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_8)
			{
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PUFFSHROOM, 7, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_TALLNUT, 3, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 4, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 8, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 4, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_POGO, SeedType.SEED_NONE, 4, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_9)
			{
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 6, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_THREEPEATER, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_POTATOMINE, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PLANTERN, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 8, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_GARGANTUAR, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_ENDLESS)
			{
				int num2 = TodCommon.ClampInt(this.mSurvivalStage / 10, 0, 8);
				this.ScaryPotterDontPlaceInCol(0, Challenge.aGridArray, num);
				this.ScaryPotterDontPlaceInCol(1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_LEFTPEATER, 6, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SNOWPEA, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PEASHOOTER, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_THREEPEATER, 2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_SQUASH, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_POTATOMINE, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_WALLNUT, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SEED, ZombieType.ZOMBIE_INVALID, SeedType.SEED_PLANTERN, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_SUN, ZombieType.ZOMBIE_INVALID, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_NORMAL, SeedType.SEED_NONE, 8 - num2, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_PAIL, SeedType.SEED_NONE, 5, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_JACK_IN_THE_BOX, SeedType.SEED_NONE, 1, Challenge.aGridArray, num);
				this.ScaryPotterPlacePot(ScaryPotType.SCARYPOT_ZOMBIE, ZombieType.ZOMBIE_GARGANTUAR, SeedType.SEED_NONE, 1 + num2, Challenge.aGridArray, num);
				this.ScaryPotterChangePotType(GridItemState.GRIDITEM_STATE_SCARY_POT_LEAF, 2);
			}
			else
			{
				Debug.ASSERT(false);
			}
			this.mScaryPotterPots = this.ScaryPotterCountPots();
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
			this.ScaryPotterDontPlaceInCol(theCol, theGridArray, theGridArrayCount);
			for (int i = 0; i < 5; i++)
			{
				Plant plant = this.mBoard.NewPlant(theCol, i, theSeedType, SeedType.SEED_NONE);
				if (theSeedType == SeedType.SEED_POTATOMINE)
				{
					plant.mStateCountdown = 10;
				}
			}
		}

		public void PuzzleNextStageClear()
		{
			this.mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
			this.mBoard.mNextSurvivalStageCounter = 0;
			this.mBoard.mProgressMeterWidth = 0;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && zombie.IsOnBoard())
				{
					zombie.DieNoLoot(false);
				}
			}
			count = this.mBoard.mPlants.Count;
			for (int j = 0; j < count; j++)
			{
				Plant plant = this.mBoard.mPlants[j];
				if (!plant.mDead && plant.IsOnBoard())
				{
					plant.Die();
				}
			}
			this.mBoard.RefreshSeedPacketFromCursor();
			Coin coin = null;
			while (this.mBoard.IterateCoins(ref coin))
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
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
			{
				gridItem.GridItemDie();
			}
			this.mSurvivalStage++;
			this.mBoard.ClearAdviceImmediately();
			this.mBoard.mLevelAwardSpawned = false;
			this.mApp.AddTodParticle(400f, 300f, 400000, ParticleEffect.PARTICLE_SCREEN_FLASH);
		}

		public void ScaryPotterMalletPot(GridItem theScaryPot)
		{
			this.mChallengeGridX = theScaryPot.mGridX;
			this.mChallengeGridY = theScaryPot.mGridY;
			int num = this.mBoard.GridToPixelX(theScaryPot.mGridX, theScaryPot.mGridY);
			int num2 = this.mBoard.GridToPixelY(theScaryPot.mGridX, theScaryPot.mGridY);
			Reanimation reanimation = this.mApp.AddReanimation((float)num, (float)num2, 400000, ReanimationType.REANIM_HAMMER);
			reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_open_pot, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 40f);
			this.mReanimChallenge = this.mApp.ReanimationGetID(reanimation);
			this.mChallengeState = ChallengeState.STATECHALLENGE_SCARY_POTTER_MALLETING;
			this.mApp.PlayFoley(FoleyType.FOLEY_SWING);
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
				this.mBoard.RefreshSeedPacketFromCursor();
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				return;
			}
			int num = this.mBoard.PlantingPixelToGridX((int)((float)x * Constants.IS), (int)((float)y * Constants.IS), this.mBoard.mCursorObject.mType);
			int num2 = this.mBoard.PlantingPixelToGridY((int)((float)x * Constants.IS), (int)((float)y * Constants.IS), this.mBoard.mCursorObject.mType);
			if (num == -1 || num2 == -1)
			{
				this.mBoard.RefreshSeedPacketFromCursor();
				this.mApp.PlayFoley(FoleyType.FOLEY_DROP);
				return;
			}
			PlantingReason plantingReason = this.CanPlantAt(num, num2, this.mBoard.mCursorObject.mType);
			if (plantingReason == PlantingReason.PLANTING_OK)
			{
				if (!this.mApp.mEasyPlantingCheat)
				{
					int currentPlantCost = this.mBoard.GetCurrentPlantCost(this.mBoard.mCursorObject.mType, this.mBoard.mCursorObject.mImitaterType);
					if (!this.mBoard.TakeSunMoney(currentPlantCost))
					{
						return;
					}
				}
				this.mBoard.ClearAdvice(AdviceType.ADVICE_I_ZOMBIE_LEFT_OF_LINE);
				this.mBoard.ClearAdvice(AdviceType.ADVICE_I_ZOMBIE_NOT_PASSED_LINE);
				ZombieType theZombieType = Challenge.IZombieSeedTypeToZombieType(this.mBoard.mCursorObject.mType);
				this.IZombiePlaceZombie(theZombieType, num, num2);
				Debug.ASSERT(this.mBoard.mCursorObject.mSeedBankIndex >= 0 && this.mBoard.mCursorObject.mSeedBankIndex < this.mBoard.mSeedBank.mNumPackets);
				SeedPacket seedPacket = this.mBoard.mSeedBank.mSeedPackets[this.mBoard.mCursorObject.mSeedBankIndex];
				seedPacket.WasPlanted();
				this.mApp.PlayFoley(FoleyType.FOLEY_PLANT);
				this.mBoard.ClearCursor();
				return;
			}
			this.mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
			if (this.mBoard.mCursorObject.mType == SeedType.SEED_ZOMBIE_BUNGEE)
			{
				this.mBoard.DisplayAdvice("[ADVICE_I_ZOMBIE_LEFT_OF_LINE]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_I_ZOMBIE_LEFT_OF_LINE);
				return;
			}
			this.mBoard.DisplayAdvice("[ADVICE_I_ZOMBIE_NOT_PASSED_LINE]", MessageStyle.MESSAGE_STYLE_HINT_LONG, AdviceType.ADVICE_I_ZOMBIE_NOT_PASSED_LINE);
		}

		public void IZombieStart()
		{
			this.mBoard.DisplayAdvice("[ADVICE_I_ZOMBIE_EAT_ALL_BRAINS]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_I_ZOMBIE_EAT_ALL_BRAINS);
		}

		public void IZombiePlacePlants(SeedType theSeedType, int theCount, int theGridY)
		{
			int num = 0;
			int num2 = 6;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5)
			{
				num2 = 4;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8 || this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
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
					if (this.mBoard.CanPlantAt(j, i, theSeedType) == PlantingReason.PLANTING_OK && ((theSeedType != SeedType.SEED_WALLNUT && theSeedType != SeedType.SEED_TALLNUT && theSeedType != SeedType.SEED_TORCHWOOD) || num2 - j <= 3))
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
				Plant thePlant = this.mBoard.NewPlant(todWeightedGridArray.mX, todWeightedGridArray.mY, theSeedType, SeedType.SEED_NONE);
				todWeightedGridArray.mWeight = 0;
				this.IZombieSetupPlant(thePlant);
			}
		}

		public void IZombieUpdate()
		{
			int num = this.mBoard.mSunMoney;
			Coin coin = null;
			while (this.mBoard.IterateCoins(ref coin))
			{
				if (coin.IsSun())
				{
					num += coin.GetSunValue();
				}
			}
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && !zombie.IsDeadOrDying() && zombie.mZombiePhase != ZombiePhase.PHASE_POLEVAULTER_IN_VAULT && !zombie.mIsEating && zombie.mJustGotShotCounter < -500)
				{
					zombie.PickRandomSpeed();
				}
			}
			bool flag = false;
			count = this.mBoard.mPlants.Count;
			for (int j = 0; j < count; j++)
			{
				Plant plant = this.mBoard.mPlants[j];
				if (!plant.mDead && (plant.mState == PlantState.STATE_SQUASH_FALLING || plant.mState == PlantState.STATE_SQUASH_DONE_FALLING || plant.mState == PlantState.STATE_CHOMPER_BITING || plant.mState == PlantState.STATE_CHOMPER_BITING_GOT_ONE))
				{
					flag = true;
					break;
				}
			}
			int num2 = -1;
			TodParticleSystem todParticleSystem = null;
			while (this.mBoard.IterateParticles(ref todParticleSystem, ref num2))
			{
				if (todParticleSystem.mEffectType == ParticleEffect.PARTICLE_POTATO_MINE)
				{
					flag = true;
					break;
				}
			}
			if (this.mApp.mBoard.mZombies.Count == 0 && num < 50 && !this.mBoard.HasLevelAwardDropped() && !flag)
			{
				Coin coin2 = null;
				while (this.mBoard.IterateCoins(ref coin2))
				{
					if (coin2.IsMoney())
					{
						coin2.Die();
					}
				}
				this.mBoard.ZombiesWon(null);
			}
		}

		public void IZombieDrawPlant(Graphics g, Plant thePlant)
		{
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			Reanimation reanimation = this.mApp.ReanimationTryToGet(thePlant.mBodyReanimID);
			if (reanimation == null)
			{
				return;
			}
			float num = (float)g.mTransX;
			float num2 = (float)g.mTransY;
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
			this.IZombieSetPlantFilterEffect(thePlant, FilterEffectType.FILTER_EFFECT_NONE);
			reanimation.DrawRenderGroup(g, 0);
			this.IZombieSetPlantFilterEffect(thePlant, FilterEffectType.FILTER_EFFECT_NONE);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.SetColorizeImages(false);
		}

		public void IZombieSetPlantFilterEffect(Plant thePlant, FilterEffectType theFilterEffect)
		{
			Reanimation reanimation = this.mApp.ReanimationTryToGet(thePlant.mBodyReanimID);
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(thePlant.mHeadReanimID);
			Reanimation reanimation3 = this.mApp.ReanimationTryToGet(thePlant.mHeadReanimID2);
			Reanimation reanimation4 = this.mApp.ReanimationTryToGet(thePlant.mHeadReanimID3);
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
			while (this.mBoard.IterateGridItems(ref gridItem, ref num2))
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
			this.mChallengeScore = 0;
			for (int i = 0; i < 5; i++)
			{
				GridItem newGridItem = GridItem.GetNewGridItem();
				newGridItem.mGridItemType = GridItemType.GRIDITEM_IZOMBIE_BRAIN;
				newGridItem.mGridX = 0;
				newGridItem.mGridY = i;
				newGridItem.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PLANT, newGridItem.mGridY, 0);
				newGridItem.mGridItemCounter = 70;
				newGridItem.mPosX = (float)this.mBoard.GridToPixelX(newGridItem.mGridX, newGridItem.mGridY) - 40f;
				newGridItem.mPosY = (float)this.mBoard.GridToPixelY(newGridItem.mGridX, newGridItem.mGridY) + 40f;
				this.mBoard.mGridItems.Add(newGridItem);
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_1)
			{
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 2);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 3);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 7, -1);
				this.IZombiePlacePlants(SeedType.SEED_SQUASH, 3, -1);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 6, -1);
				this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 2, -1);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_2)
			{
				this.IZombiePlacePlantInSquare(SeedType.SEED_SPIKEWEED, 3, 0);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 0);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 3);
				this.IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 1, 0);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 1, 0);
				this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 2, 3);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 1, 3);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 4, -1);
				this.IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 4, -1);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_3)
			{
				this.IZombiePlacePlantInSquare(SeedType.SEED_POTATOMINE, 3, 0);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 0);
				this.IZombiePlacePlantInSquare(SeedType.SEED_POTATOMINE, 2, 2);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 4);
				this.IZombiePlacePlantInSquare(SeedType.SEED_TORCHWOOD, 3, 3);
				this.IZombiePlacePlants(SeedType.SEED_TORCHWOOD, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 5, -1);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 7, -1);
				this.IZombiePlacePlants(SeedType.SEED_SPLITPEA, 1, -1);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_4)
			{
				this.IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 0);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 0);
				this.IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 1);
				this.IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 2);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 2);
				this.IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 3);
				this.IZombiePlacePlantInSquare(SeedType.SEED_WALLNUT, 3, 4);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 2, 4);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 1, 0);
				this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, 1);
				this.IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 2, 2);
				this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, 3);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 1, 4);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 4, -1);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_5)
			{
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 2);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 3, 3);
				this.IZombiePlacePlants(SeedType.SEED_CACTUS, 1, 1);
				this.IZombiePlacePlants(SeedType.SEED_CACTUS, 1, 4);
				this.IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 1, -1);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 5, -1);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 8, -1);
				this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 2, -1);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_6)
			{
				this.IZombiePlacePlantInSquare(SeedType.SEED_GARLIC, 4, 1);
				this.IZombiePlacePlantInSquare(SeedType.SEED_GARLIC, 4, 3);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 3, 1);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 3, 3);
				this.IZombiePlacePlants(SeedType.SEED_TORCHWOOD, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 3, -1);
				this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, -1);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 5, -1);
				this.IZombiePlacePlants(SeedType.SEED_SQUASH, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_KERNELPULT, 2, -1);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_7)
			{
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 4, 2);
				this.IZombiePlacePlantInSquare(SeedType.SEED_SUNFLOWER, 4, 4);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 6, -1);
				this.IZombiePlacePlants(SeedType.SEED_POTATOMINE, 9, -1);
				this.IZombiePlacePlants(SeedType.SEED_CHOMPER, 8, -1);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_8)
			{
				this.IZombiePlacePlants(SeedType.SEED_WALLNUT, 3, -1);
				this.IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 8, -1);
				this.IZombiePlacePlants(SeedType.SEED_SQUASH, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_POTATOMINE, 2, -1);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 8, -1);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_9)
			{
				this.IZombiePlacePlantInSquare(SeedType.SEED_TALLNUT, 5, 1);
				this.IZombiePlacePlantInSquare(SeedType.SEED_TORCHWOOD, 5, 3);
				this.IZombiePlacePlants(SeedType.SEED_POTATOMINE, 4, 0);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, 0);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, 1);
				this.IZombiePlacePlants(SeedType.SEED_THREEPEATER, 1, 1);
				this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, 1);
				this.IZombiePlacePlants(SeedType.SEED_SPLITPEA, 1, 1);
				this.IZombiePlacePlants(SeedType.SEED_CHOMPER, 3, 2);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, 2);
				this.IZombiePlacePlants(SeedType.SEED_SQUASH, 1, 2);
				this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 3, 3);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 2, 3);
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 1, 4);
				this.IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 1, 4);
				this.IZombiePlacePlants(SeedType.SEED_SCAREDYSHROOM, 1, 4);
				this.IZombiePlacePlants(SeedType.SEED_STARFRUIT, 1, 4);
				this.IZombiePlacePlants(SeedType.SEED_SPLITPEA, 1, 4);
				this.IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 1, 4);
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
			{
				int num = TodCommon.RandRangeInt(0, 4);
				int theMax = TodCommon.ClampInt(3 + this.mSurvivalStage / 2, 2, 6);
				int theMin = TodCommon.ClampInt(2 + this.mSurvivalStage / 3, 2, 4);
				int num2 = TodCommon.RandRangeInt(theMin, theMax);
				if (this.mSurvivalStage == 0)
				{
					num2 = 0;
				}
				else if (this.mSurvivalStage == 1)
				{
					num2 = 1;
				}
				else if (this.mSurvivalStage >= 10)
				{
					theMax = TodCommon.ClampInt(3 + this.mSurvivalStage / 2, 2, 7);
					theMin = TodCommon.ClampInt(2 + this.mSurvivalStage / 3, 2, 5);
					num2 = TodCommon.RandRangeInt(theMin, theMax);
				}
				int theCount = 8 - num2;
				this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, theCount, -1);
				this.IZombiePlacePlants(SeedType.SEED_PUFFSHROOM, num2, -1);
				if (num == 0 && this.mSurvivalStage >= 1)
				{
					int num3 = TodCommon.RandRangeInt(0, 4);
					if (num3 == 0)
					{
						this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 9, -1);
						this.IZombiePlacePlants(SeedType.SEED_SPLITPEA, 4, -1);
						this.IZombiePlacePlants(SeedType.SEED_REPEATER, 4, -1);
					}
					else if (num3 == 1)
					{
						this.IZombiePlacePlants(SeedType.SEED_POTATOMINE, 9, -1);
						this.IZombiePlacePlants(SeedType.SEED_CHOMPER, 8, -1);
					}
					else if (num3 == 2)
					{
						this.IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 9, -1);
						this.IZombiePlacePlants(SeedType.SEED_STARFRUIT, 8, -1);
					}
					else if (num3 == 3)
					{
						this.IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 9, -1);
						this.IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 8, -1);
					}
					else
					{
						this.IZombiePlacePlants(SeedType.SEED_SCAREDYSHROOM, 12, -1);
						this.IZombiePlacePlants(SeedType.SEED_SUNFLOWER, 5, -1);
					}
				}
				else
				{
					int num3 = TodCommon.RandRangeInt(0, 5);
					if (num3 == 0 || num3 == 1 || num3 == 2)
					{
						this.IZombiePlacePlants(SeedType.SEED_WALLNUT, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_TORCHWOOD, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_POTATOMINE, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_CHOMPER, 2, -1);
						this.IZombiePlacePlants(SeedType.SEED_PEASHOOTER, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_SPLITPEA, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_KERNELPULT, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_THREEPEATER, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_SQUASH, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_UMBRELLA, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_STARFRUIT, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 2, -1);
					}
					else if (num3 == 3 || num3 == 4)
					{
						this.IZombiePlacePlants(SeedType.SEED_TORCHWOOD, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_SPLITPEA, 3, -1);
						this.IZombiePlacePlants(SeedType.SEED_REPEATER, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_KERNELPULT, 3, -1);
						this.IZombiePlacePlants(SeedType.SEED_THREEPEATER, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_SNOWPEA, 3, -1);
						this.IZombiePlacePlants(SeedType.SEED_UMBRELLA, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_MAGNETSHROOM, 1, -1);
						this.IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 3, -1);
					}
					else
					{
						this.IZombiePlacePlants(SeedType.SEED_POTATOMINE, 4, -1);
						this.IZombiePlacePlants(SeedType.SEED_CHOMPER, 3, -1);
						this.IZombiePlacePlants(SeedType.SEED_SQUASH, 3, -1);
						this.IZombiePlacePlants(SeedType.SEED_FUMESHROOM, 4, -1);
						this.IZombiePlacePlants(SeedType.SEED_SPIKEWEED, 3, -1);
					}
				}
			}
			this.mBoard.mBonusLawnMowersRemaining = 0;
		}

		public void DrawRain(Graphics g)
		{
			if (this.mBoard.mCutScene != null && this.mBoard.mCutScene.IsBeforePreloading())
			{
				return;
			}
			if (!this.mApp.Is3DAccelerated())
			{
				return;
			}
			int num;
			if (this.mBoard.mX > 0)
			{
				num = (this.mBoard.mX + 100) / 100 * -100;
			}
			else
			{
				num = this.mBoard.mX / 100 * -100;
			}
			int num2 = TodCommon.TodAnimateCurve(0, 100, this.mBoard.mEffectCounter % 100, 0, -100, TodCurves.CURVE_LINEAR);
			int num3 = TodCommon.TodAnimateCurve(0, 20, this.mBoard.mEffectCounter % 20, -100, 0, TodCurves.CURVE_LINEAR);
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int theX = num2 + i * 100 + num;
					int theY = num3 + j * 100;
					g.DrawImage(AtlasResources.IMAGE_RAIN, theX, theY);
				}
			}
			int num4 = TodCommon.TodAnimateCurve(0, 161, this.mBoard.mEffectCounter % 161, 0, -100, TodCurves.CURVE_LINEAR);
			int num5 = TodCommon.TodAnimateCurve(0, 33, this.mBoard.mEffectCounter % 33, -100, 0, TodCurves.CURVE_LINEAR);
			for (int k = 0; k < 5; k++)
			{
				for (int l = 0; l < 4; l++)
				{
					float num6 = 1.7f;
					float thePosX = ((float)num4 + (float)k * 100f) * num6 + (float)num;
					float thePosY = ((float)num5 + (float)l * 100f) * num6;
					TodCommon.TodDrawImageScaledF(g, AtlasResources.IMAGE_RAIN, thePosX, thePosY, num6, num6);
				}
			}
		}

		public void DrawWeather(Graphics g)
		{
			if (this.mApp.IsStormyNightLevel() || this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_RAINING_SEEDS)
			{
				this.DrawRain(g);
			}
			if (this.mApp.IsStormyNightLevel())
			{
				this.DrawStormNight(g);
			}
		}

		public void SquirrelUpdate()
		{
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
			{
				if (gridItem.mGridItemType == GridItemType.GRIDITEM_SQUIRREL)
				{
					this.SquirrelUpdateOne(gridItem);
				}
			}
			this.mChallengeScore = 7 - this.SquirrelCountUncaught();
			this.mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 7, this.mChallengeScore, 0, 150, TodCurves.CURVE_LINEAR);
		}

		public int SquirrelCountUncaught()
		{
			int num = 0;
			int num2 = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num2))
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
				this.mBoard.mGridItems.Add(newGridItem);
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
			this.mBoard.mGridItems.Add(newGridItem);
		}

		public void SquirrelFound(GridItem theSquirrel)
		{
			if (theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_ZOMBIE)
			{
				Zombie zombie = this.mBoard.AddZombieInRow(ZombieType.ZOMBIE_NORMAL, theSquirrel.mGridY, 0);
				zombie.mPosX = (float)this.mBoard.GridToPixelX(theSquirrel.mGridX, theSquirrel.mGridY);
				theSquirrel.GridItemDie();
				this.mBoard.DisplayAdvice("[ADVICE_SQUIRREL_ZOMBIE]", MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
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
				if (this.mBoard.GetSquirrelAt(num2, num3) == null)
				{
					Plant topPlantAt = this.mBoard.GetTopPlantAt(num2, num3, PlantPriority.TOPPLANT_EATING_ORDER);
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
			int num4 = this.SquirrelCountUncaught();
			if (num4 == 0)
			{
				this.mBoard.ClearAdvice(AdviceType.ADVICE_NONE);
				this.SpawnLevelAward(theSquirrel.mGridX, theSquirrel.mGridY);
				return;
			}
			string theAdvice = this.mApp.Pluralize(num4, "[ADVICE_SQUIRRELS_ONE_LEFT]", "[ADVICE_SQUIRRELS_LEFT]");
			this.mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_HINT_FAST, AdviceType.ADVICE_NONE);
		}

		public void SquirrelPeek(GridItem theSquirrel)
		{
			theSquirrel.mGridItemCounter = 50;
			theSquirrel.mGridItemState = GridItemState.GRIDITEM_STATE_SQUIRREL_PEEKING;
		}

		public void SquirrelChew(GridItem theSquirrel)
		{
			theSquirrel.mGridItemCounter = TodCommon.RandRangeInt(100, 500);
			Plant topPlantAt = this.mBoard.GetTopPlantAt(theSquirrel.mGridX, theSquirrel.mGridY, PlantPriority.TOPPLANT_EATING_ORDER);
			if (topPlantAt == null)
			{
				return;
			}
			float num = (float)this.mBoard.GridToPixelX(theSquirrel.mGridX, theSquirrel.mGridY);
			float num2 = (float)this.mBoard.GridToPixelY(theSquirrel.mGridX, theSquirrel.mGridY);
			this.mApp.AddTodParticle(num + 40f, num2 + 40f, topPlantAt.mRenderOrder + 1, ParticleEffect.PARTICLE_WALLNUT_EAT_SMALL);
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
				if (this.mBoard.GetTopPlantAt(theSquirrel.mGridX, theSquirrel.mGridY, PlantPriority.TOPPLANT_EATING_ORDER) == null)
				{
					this.SquirrelFound(theSquirrel);
				}
				if (theSquirrel.mGridItemCounter == 0)
				{
					int num = TodCommon.RandRangeInt(0, 1);
					if (num == 0 || theSquirrel.mGridItemState == GridItemState.GRIDITEM_STATE_SQUIRREL_ZOMBIE)
					{
						this.SquirrelChew(theSquirrel);
					}
					else
					{
						this.SquirrelPeek(theSquirrel);
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
			Reanimation reanimation = this.mApp.ReanimationTryToGet(thePlant.mBodyReanimID);
			Reanimation reanimation2 = this.mApp.ReanimationTryToGet(thePlant.mHeadReanimID);
			Reanimation reanimation3 = this.mApp.ReanimationTryToGet(thePlant.mHeadReanimID2);
			Reanimation reanimation4 = this.mApp.ReanimationTryToGet(thePlant.mHeadReanimID3);
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
			this.mRainCounter--;
			if (this.mRainCounter < 0 && !this.mBoard.mCutScene.IsBeforePreloading())
			{
				ReanimationType theReanimationType = ReanimationType.REANIM_RAIN_SPLASH;
				float theX = TodCommon.RandRangeFloat(40f, 740f);
				float theY = TodCommon.RandRangeFloat(90f, 240f);
				Reanimation reanimation = this.mApp.AddReanimation(theX, theY, 200000, theReanimationType);
				int theAlpha = TodCommon.RandRangeInt(100, 200);
				float num = TodCommon.RandRangeFloat(0.7f, 1.2f);
				reanimation.mColorOverride = new SexyColor(255, 255, 255, theAlpha);
				reanimation.OverrideScale(num, num);
				theReanimationType = ReanimationType.REANIM_RAIN_CIRCLE;
				theX = TodCommon.RandRangeFloat(40f, 740f);
				theY = TodCommon.RandRangeFloat(290f, 410f);
				reanimation = this.mApp.AddReanimation(theX, theY, 200000, theReanimationType);
				theAlpha = TodCommon.RandRangeInt(50, 150);
				num = TodCommon.RandRangeFloat(0.7f, 1.1f);
				reanimation.mColorOverride = new SexyColor(255, 255, 255, theAlpha);
				reanimation.OverrideScale(num, num);
				theReanimationType = ReanimationType.REANIM_RAIN_SPLASH;
				theX = TodCommon.RandRangeFloat(40f, 740f);
				theY = TodCommon.RandRangeFloat(450f, 580f);
				reanimation = this.mApp.AddReanimation(theX, theY, 200000, theReanimationType);
				theAlpha = TodCommon.RandRangeInt(100, 200);
				num = TodCommon.RandRangeFloat(0.7f, 1.2f);
				reanimation.mColorOverride = new SexyColor(255, 255, 255, theAlpha);
				reanimation.OverrideScale(num, num);
				this.mRainCounter = TodCommon.RandRangeInt(10, 20);
			}
		}

		public bool IZombieEatBrain(Zombie theZombie)
		{
			GridItem gridItem = this.IZombieGetBrainTarget(theZombie);
			if (gridItem == null)
			{
				return false;
			}
			theZombie.StartEating();
			gridItem.mGridItemCounter -= 3;
			if (gridItem.mGridItemCounter <= 0)
			{
				this.mApp.PlaySample(Resources.SOUND_GULP);
				gridItem.GridItemDie();
				this.IZombieScoreBrain(gridItem);
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
			GridItem gridItemAt = this.mBoard.GetGridItemAt(GridItemType.GRIDITEM_IZOMBIE_BRAIN, 0, theZombie.mRow);
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
			if (this.mBoard.CanPlantAt(theGridX, theGridY, theSeedType) != PlantingReason.PLANTING_OK)
			{
				return;
			}
			Plant thePlant = this.mBoard.NewPlant(theGridX, theGridY, theSeedType, SeedType.SEED_NONE);
			this.IZombieSetupPlant(thePlant);
		}

		public void AdvanceCrazyDaveDialog()
		{
			if (!this.mBoard.IsScaryPotterDaveTalking() || this.mApp.mCrazyDaveMessageIndex == -1)
			{
				return;
			}
			if (!this.mApp.AdvanceCrazyDaveText())
			{
				this.mApp.CrazyDaveLeave();
				return;
			}
			if (this.mApp.mCrazyDaveMessageIndex == 2702 || this.mApp.mCrazyDaveMessageIndex == 2801)
			{
				this.ScaryPotterPopulate();
				this.mApp.PlayFoley(FoleyType.FOLEY_PLANT);
				this.mBoard.PlaceRake();
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
			Plant topPlantAt = this.mBoard.GetTopPlantAt(num, num2, PlantPriority.TOPPLANT_ONLY_NORMAL_POSITION);
			if (topPlantAt != null)
			{
				topPlantAt.mBeghouledFlashCountdown = Math.Max(topPlantAt.mBeghouledFlashCountdown, 300);
			}
		}

		public void BeghouledFlashAMatch()
		{
			BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
			this.LoadBeghouledBoardState(newBeghouledBoardState);
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED)
			{
				for (int i = 0; i <= 4; i++)
				{
					for (int j = 0; j <= 7; j++)
					{
						if (j < 7 && this.BeghouledFlashFromBoardState(newBeghouledBoardState, j, i, j + 1, i))
						{
							newBeghouledBoardState.PrepareForReuse();
							return;
						}
						if (i < 4 && this.BeghouledFlashFromBoardState(newBeghouledBoardState, j, i, j, i + 1))
						{
							newBeghouledBoardState.PrepareForReuse();
							return;
						}
					}
				}
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_BEGHOULED_TWIST)
			{
				for (int k = 0; k < 4; k++)
				{
					for (int l = 0; l < 7; l++)
					{
						if (this.BeghouledTwistFlashMatch(newBeghouledBoardState, l, k))
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
			if (this.mBeghouledEated[theSwap1X, theSwap1Y] || this.mBeghouledEated[theSwap2X, theSwap2Y])
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
					int num = this.BeghouledHorizontalMatchLength(j, i, theBoardState);
					if (num >= 3)
					{
						this.BeghouledFlashPlant(j, i, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
						this.BeghouledFlashPlant(j + 1, i, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
						this.BeghouledFlashPlant(j + 2, i, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
						flag = true;
						break;
					}
					int num2 = this.BeghouledVerticalMatchLength(j, i, theBoardState);
					if (num2 >= 3)
					{
						this.BeghouledFlashPlant(j, i, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
						this.BeghouledFlashPlant(j, i + 1, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
						this.BeghouledFlashPlant(j, i + 2, theSwap1X, theSwap1Y, theSwap2X, theSwap2Y);
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
				this.mBoard.AddCoin(thePlant.mX + 5 * i, thePlant.mY, CoinType.COIN_SUN, CoinMotion.COIN_MOTION_FROM_PLANT);
			}
		}

		public void IZombieSquishBrain(GridItem theBrain)
		{
			theBrain.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, theBrain.mGridY, 0);
			theBrain.mGridItemState = GridItemState.GRIDITEM_STATE_BRAIN_SQUISHED;
			theBrain.mGridItemCounter = 500;
			theBrain.mApp.PlayFoley(FoleyType.FOLEY_SQUISH);
			this.IZombieScoreBrain(theBrain);
		}

		public void IZombieScoreBrain(GridItem theBrain)
		{
			this.mBoard.mChallenge.mChallengeScore++;
			this.mBoard.mProgressMeterWidth = TodCommon.TodAnimateCurve(0, 5, this.mBoard.mChallenge.mChallengeScore, 0, 150, TodCurves.CURVE_LINEAR);
			if (this.mBoard.mChallenge.mChallengeScore == 5)
			{
				if (this.mApp.IsEndlessIZombie(this.mApp.mGameMode))
				{
					this.PuzzlePhaseComplete(0, theBrain.mGridY);
				}
				else
				{
					this.mBoard.mChallenge.SpawnLevelAward(0, theBrain.mGridY);
				}
			}
			if (this.mBoard.mChallenge.mChallengeScore != 5 || !this.PuzzleIsAwardStage())
			{
				this.mBoard.DropLootPiece((int)theBrain.mPosX + 40, (int)theBrain.mPosY - 50, 12);
			}
		}

		public void LastStandUpate()
		{
			if (this.mBoard.mNextSurvivalStageCounter == 0 && this.mChallengeState == ChallengeState.STATECHALLENGE_NORMAL && this.mBoard.mStoreButton.mBtnNoDraw)
			{
				if (this.mSurvivalStage == 0)
				{
					this.mBoard.mStoreButton.mBtnNoDraw = false;
					this.mBoard.mStoreButton.mDisabled = false;
					this.mBoard.mStoreButton.SetLabel("[START_ONSLAUGHT]");
					this.mBoard.mStoreButton.Resize(Constants.LastStandButtonRect.mX, Constants.LastStandButtonRect.mY, Constants.LastStandButtonRect.mWidth, Constants.LastStandButtonRect.mHeight);
				}
				else
				{
					this.mBoard.mStoreButton.mBtnNoDraw = false;
					this.mBoard.mStoreButton.mDisabled = false;
					this.mBoard.mStoreButton.SetLabel("[CONTINUE_ONSLAUGHT]");
					this.mBoard.mStoreButton.Resize(Constants.LastStandButtonRect.mX, Constants.LastStandButtonRect.mY, Constants.LastStandButtonRect.mWidth, Constants.LastStandButtonRect.mHeight);
				}
			}
			if (this.mChallengeState == ChallengeState.STATECHALLENGE_LAST_STAND_ONSLAUGHT && this.mApp.mGameScene == GameScenes.SCENE_PLAYING)
			{
				this.mChallengeStateCounter++;
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
					if (this.mBoard.CanAddGraveStoneAt(j, k))
					{
						Plant topPlantAt = this.mBoard.GetTopPlantAt(j, k, PlantPriority.TOPPLANT_ANY);
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
				int count = this.mBoard.mPlants.Count;
				for (int m = 0; m < count; m++)
				{
					Plant plant = this.mBoard.mPlants[m];
					if (!plant.mDead && plant.mPlantCol == todWeightedGridArray.mX && plant.mRow == todWeightedGridArray.mY)
					{
						plant.Die();
					}
				}
				this.mBoard.AddAGraveStone(todWeightedGridArray.mX, todWeightedGridArray.mY);
				todWeightedGridArray.mWeight = 0;
			}
		}

		public bool BeghouledTwistSquareFromMouse(int theMouseX, int theMouseY, ref int theGridX, ref int theGridY)
		{
			theGridX = this.mBoard.PixelToGridX(theMouseX - 40, theMouseY - 40);
			theGridY = this.mBoard.PixelToGridY(theMouseX - 40, theMouseY - 40);
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
			x = (int)((float)x * Constants.IS);
			y = (int)((float)y * Constants.IS);
			if (this.mBoard.HasLevelAwardDropped())
			{
				return;
			}
			BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
			this.LoadBeghouledBoardState(newBeghouledBoardState);
			int num = 0;
			int num2 = 0;
			if (!this.BeghouledTwistSquareFromMouse(x, y, ref num, ref num2) || !this.BeghouledTwistValidMove(num, num2, newBeghouledBoardState))
			{
				newBeghouledBoardState.PrepareForReuse();
				return;
			}
			Plant topPlantAt = this.mBoard.GetTopPlantAt(num, num2, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt2 = this.mBoard.GetTopPlantAt(num + 1, num2, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt3 = this.mBoard.GetTopPlantAt(num, num2 + 1, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt4 = this.mBoard.GetTopPlantAt(num + 1, num2 + 1, PlantPriority.TOPPLANT_ANY);
			if (!this.BeghouledTwistMoveCausesMatch(num, num2, newBeghouledBoardState))
			{
				topPlantAt.mX = this.mBoard.GridToPixelX(topPlantAt.mPlantCol, topPlantAt.mRow) + 20;
				topPlantAt2.mY = this.mBoard.GridToPixelY(topPlantAt2.mPlantCol, topPlantAt2.mRow) + 20;
				topPlantAt3.mY = this.mBoard.GridToPixelY(topPlantAt3.mPlantCol, topPlantAt3.mRow) - 20;
				topPlantAt4.mX = this.mBoard.GridToPixelX(topPlantAt4.mPlantCol, topPlantAt4.mRow) - 20;
				this.mApp.PlayFoley(FoleyType.FOLEY_FLOOP);
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
			this.BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_MOVING);
			newBeghouledBoardState.PrepareForReuse();
		}

		public bool BeghouledTwistMoveCausesMatch(int theGridX, int theGridY, BeghouledBoardState theBoardState)
		{
			if (!this.BeghouledTwistValidMove(theGridX, theGridY, theBoardState))
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
			bool result = this.BeghouledBoardHasMatch(theBoardState);
			theBoardState.mSeedType[theGridX, theGridY] = seedType;
			theBoardState.mSeedType[theGridX + 1, theGridY] = seedType2;
			theBoardState.mSeedType[theGridX, theGridY + 1] = seedType3;
			theBoardState.mSeedType[theGridX + 1, theGridY + 1] = seedType4;
			return result;
		}

		public bool BeghouledTwistFlashMatch(BeghouledBoardState theBoardState, int theGridX, int theGridY)
		{
			if (!this.BeghouledTwistMoveCausesMatch(theGridX, theGridY, theBoardState))
			{
				return false;
			}
			Plant topPlantAt = this.mBoard.GetTopPlantAt(theGridX, theGridY, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt2 = this.mBoard.GetTopPlantAt(theGridX + 1, theGridY, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt3 = this.mBoard.GetTopPlantAt(theGridX, theGridY + 1, PlantPriority.TOPPLANT_ANY);
			Plant topPlantAt4 = this.mBoard.GetTopPlantAt(theGridX + 1, theGridY + 1, PlantPriority.TOPPLANT_ANY);
			topPlantAt.mBeghouledFlashCountdown = Math.Max(topPlantAt.mBeghouledFlashCountdown, 300);
			topPlantAt2.mBeghouledFlashCountdown = Math.Max(topPlantAt2.mBeghouledFlashCountdown, 300);
			topPlantAt3.mBeghouledFlashCountdown = Math.Max(topPlantAt3.mBeghouledFlashCountdown, 300);
			topPlantAt4.mBeghouledFlashCountdown = Math.Max(topPlantAt4.mBeghouledFlashCountdown, 300);
			return true;
		}

		public void BeghouledCancelMatchFlashing()
		{
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead)
				{
					plant.mBeghouledFlashCountdown = Math.Min(plant.mBeghouledFlashCountdown, 25);
				}
			}
		}

		public void BeghouledStartFalling(ChallengeState theChallengeState)
		{
			this.mChallengeStateCounter = 100;
			this.mChallengeState = theChallengeState;
			this.BeghouledCancelMatchFlashing();
			this.mBoard.ClearAdvice(AdviceType.ADVICE_BEGHOULED_NO_MOVES);
		}

		public void BeghouledFillHoles(BeghouledBoardState theBoardState, bool theAllowMatches)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (theBoardState.mSeedType[i, j] == SeedType.SEED_NONE && !this.mBeghouledEated[i, j])
					{
						SeedType seedType = this.BeghouledPickSeed(i, j, theBoardState, theAllowMatches);
						theBoardState.mSeedType[i, j] = seedType;
					}
				}
			}
		}

		public void BeghouledMakeStartBoard()
		{
			BeghouledBoardState newBeghouledBoardState = BeghouledBoardState.GetNewBeghouledBoardState();
			this.LoadBeghouledBoardState(newBeghouledBoardState);
			BeghouledBoardState newBeghouledBoardState2 = BeghouledBoardState.GetNewBeghouledBoardState();
			this.LoadBeghouledBoardState(newBeghouledBoardState2);
			this.BeghouledFillHoles(newBeghouledBoardState2, false);
			Debug.ASSERT(!this.BeghouledBoardHasMatch(newBeghouledBoardState2));
			this.BeghouledCreatePlants(newBeghouledBoardState, newBeghouledBoardState2);
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
						Plant plant = this.mBoard.NewPlant(i, j, theNewBoardState.mSeedType[i, j], SeedType.SEED_NONE);
						plant.mY = 80 - num * 100;
						this.BeghouledStartFalling(ChallengeState.STATECHALLENGE_BEGHOULED_FALLING);
					}
				}
			}
		}

		public void PuzzlePhaseComplete(int theGridX, int theGridY)
		{
			if (this.PuzzleIsAwardStage())
			{
				int num = TodCommon.RandRangeInt(0, 99);
				CoinType theCoinType;
				if (num < 15)
				{
					if (this.mApp.mZenGarden.CanDropPottedPlantLoot())
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
					if (this.mApp.mZenGarden.CanDropChocolate())
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
				float num2 = (float)(this.mBoard.GridToPixelX(theGridX, theGridY) + 40);
				float num3 = (float)(this.mBoard.GridToPixelY(theGridX, theGridY) + 40);
				this.mBoard.AddCoin((int)num2, (int)num3, theCoinType, CoinMotion.COIN_MOTION_COIN);
				return;
			}
			this.mBoard.FadeOutLevel();
		}

		public void IZombiePlaceZombie(ZombieType theZombieType, int theGridX, int theGridY)
		{
			Zombie zombie = this.mBoard.AddZombieInRow(theZombieType, theGridY, 0);
			if (theZombieType == ZombieType.ZOMBIE_BUNGEE)
			{
				zombie.mTargetCol = theGridX;
				zombie.SetRow(theGridY);
				zombie.mPosX = (float)this.mBoard.GridToPixelX(theGridX, theGridY);
				zombie.mPosY = zombie.GetPosYBasedOnRow(theGridY);
				zombie.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GRAVE_STONE, theGridY, 7);
				return;
			}
			zombie.mPosX = (float)this.mBoard.GridToPixelX(theGridX, theGridY) - 30f;
		}

		public void WhackAZombieUpdate()
		{
			if (this.mBoard.mSunMoney > 0 && this.mBoard.mTutorialState == TutorialState.TUTORIAL_OFF)
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_WHACK_A_ZOMBIE_BEFORE_PICK_SEED);
				this.mBoard.mTutorialTimer = 1500;
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_WHACK_A_ZOMBIE_BEFORE_PICK_SEED && this.mBoard.mTutorialTimer == 0)
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_WHACK_A_ZOMBIE_PICK_SEED);
				this.mBoard.mTutorialTimer = 400;
			}
			if (this.mBoard.mTutorialState == TutorialState.TUTORIAL_WHACK_A_ZOMBIE_PICK_SEED && this.mBoard.mTutorialTimer == 0)
			{
				this.mBoard.SetTutorialState(TutorialState.TUTORIAL_WHACK_A_ZOMBIE_COMPLETED);
			}
		}

		public void LastStandCompletedStage()
		{
			this.mApp.PlaySample(Resources.SOUND_HUGE_WAVE);
			this.mChallengeState = ChallengeState.STATECHALLENGE_NORMAL;
			this.mBoard.mSeedBank.RefreshAllPackets();
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
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
			int survivalFlagsCompleted = this.mBoard.GetSurvivalFlagsCompleted();
			string theStringToSubstitute = this.mApp.Pluralize(survivalFlagsCompleted, "[ONE_FLAG]", "[COUNT_FLAGS]");
			string theAdvice = TodCommon.TodReplaceString("[SUCCESSFULLY_DEFENDED]", "{FLAGS}", theStringToSubstitute);
			this.mBoard.DisplayAdvice(theAdvice, MessageStyle.MESSAGE_STYLE_BIG_MIDDLE_FAST, AdviceType.ADVICE_NONE);
			this.mSurvivalStage++;
			this.mBoard.mLevelComplete = false;
			this.mBoard.InitZombieWaves();
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
			int currentChallengeIndex = this.mApp.GetCurrentChallengeIndex();
			return this.mApp.mPlayerInfo.mChallengeRecords[currentChallengeIndex];
		}

		public void TreeOfWisdomDraw(Graphics g)
		{
			bool flag = false;
			int x = this.mApp.mWidgetManager.mLastMouseX - this.mBoard.mX;
			int y = this.mApp.mWidgetManager.mLastMouseY - this.mBoard.mY;
			HitResult hitResult;
			this.mBoard.MouseHitTest(x, y, out hitResult, false);
			if (hitResult.mObjectType == GameObjectType.OBJECT_TYPE_TREE_OF_WISDOM && this.mBoard.mCursorObject.mCursorType == CursorType.CURSOR_TYPE_TREE_FOOD)
			{
				flag = true;
			}
			Reanimation reanimation = this.mApp.ReanimationGet(this.mReanimChallenge);
			reanimation.mEnableExtraAdditiveDraw = false;
			reanimation.DrawRenderGroup(g, 1);
			for (int i = 0; i < 6; i++)
			{
				Reanimation reanimation2 = this.mApp.ReanimationGet(this.mReanimCloud[i]);
				reanimation2.Draw(g);
			}
			int num = this.TreeOfWisdomGetSize();
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
			if (this.mChallengeState == ChallengeState.STATECHALLENGE_TREE_GIVE_WISDOM || this.mChallengeState == ChallengeState.STATECHALLENGE_TREE_BABBLING)
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
				string theText = Common.StrFormat_("[TREE_OF_WISDOM_%d]", this.mTreeOfWisdomTalkIndex);
				TRect theRect = new TRect((int)((float)(num2 + 25) * Constants.S), (int)((float)(num3 + 6) * Constants.S), 233, 144);
				TodStringFile.TodDrawStringWrapped(g, theText, theRect, Resources.FONT_BRIANNETOD16, SexyColor.Black, DrawStringJustification.DS_ALIGN_CENTER_VERTICAL_MIDDLE);
			}
			int num4 = num;
			float num5 = 1f;
			if (this.mChallengeState == ChallengeState.STATECHALLENGE_TREE_JUST_GREW)
			{
				if (this.mChallengeStateCounter > 30)
				{
					num4--;
				}
				num5 = TodCommon.TodAnimateCurveFloat(55, 20, this.mChallengeStateCounter, 1f, 1.2f, TodCurves.CURVE_BOUNCE);
			}
			if (num4 >= 50)
			{
				string theString = TodCommon.TodReplaceNumberString("[TREE_OF_WISDOM_HIEGHT]", "{HEIGHT}", num4);
				float num6 = (float)Resources.FONT_HOUSEOFTERROR16.StringWidth(theString) * num5;
				float num7 = (float)Resources.FONT_HOUSEOFTERROR16.mAscent * num5;
				Matrix theMatrix = default(Matrix);
				TodCommon.TodScaleTransformMatrix(ref theMatrix, 400f - num6 * 0.5f, 20f + num7 * 0.5f, num5, num5);
				TodCommon.TodDrawStringMatrix(g, Resources.FONT_HOUSEOFTERROR16, theMatrix, theString, new SexyColor(255, 255, 255));
			}
		}

		public void TreeOfWisdomNextGarden()
		{
			this.TreeOfWisdomLeave();
			this.mApp.KillBoard();
			this.mApp.PreNewGame(GameMode.GAMEMODE_CHALLENGE_ZEN_GARDEN, false);
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
			if (this.mChallengeState == ChallengeState.STATECHALLENGE_TREE_JUST_GREW)
			{
				return false;
			}
			int num = -1;
			GridItem gridItem = null;
			while (this.mBoard.IterateGridItems(ref gridItem, ref num))
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
			if (this.mApp.IsAdventureMode() || this.mApp.IsQuickPlayMode())
			{
				return false;
			}
			int num = 1;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_PUZZLE_I_ZOMBIE_ENDLESS)
			{
				num = 3;
			}
			else if (this.mApp.mGameMode == GameMode.GAMEMODE_SCARY_POTTER_ENDLESS)
			{
				num = 10;
			}
			return (this.mSurvivalStage + 1) % num == 0;
		}

		public void BackFromStore()
		{
			this.mApp.KillDialog(4);
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
