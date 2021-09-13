using System;
using Microsoft.Xna.Framework.GamerServices;
using Sexy;

namespace Lawn
{
	public/*internal*/ class PlayerInfo
	{
		public int mLevel
		{
			get
			{
				return this._level;
			}
			private set
			{
				this._level = value;
			}
		}

		public bool FirstRun
		{
			get
			{
				return this.mFirstRun;
			}
			set
			{
				SexyAppBase.FirstRun = value;
				this.mFirstRun = false;
			}
		}

		public PlayerInfo()
		{
			this.Reset();
		}

		public PlayerInfo(uint id) : this()
		{
			this.mId = id;
		}

		public void Dispose()
		{
		}

		public void Reset()
		{
			this.mLevel = 1;
			this.mCoins = 0;
			this.mFinishedAdventure = 0;
			this.mPottedPlant = new PottedPlant[200];
			for (int i = 0; i < this.mPottedPlant.Length; i++)
			{
				this.mPottedPlant[i] = new PottedPlant();
			}
			this.mShownAchievements = new bool[18];
			this.mEarnedAchievements = new bool[18];
			this.mPlantTypesUsed = new bool[49];
			this.mChallengeRecords = new int[200];
			for (int j = 0; j < 200; j++)
			{
				this.mChallengeRecords[j] = 0;
			}
			this.mPurchases = new int[80];
			for (int k = 0; k < 80; k++)
			{
				this.mPurchases[k] = 0;
			}
			this.mZombiesKilled = 0L;
			this.mVasebreakerScore = 0L;
			this.mIZombieScore = 0L;
			this.mPlayTimeActivePlayer = 0;
			this.mPlayTimeInactivePlayer = 0;
			this.mHasUsedCheatKeys = false;
			this.mHasWokenStinky = false;
			this.mDidntPurchasePacketUpgrade = 0;
			this.mLastStinkyChocolateTime = DateTime.MinValue;
			this.mStinkyPosX = 0;
			this.mStinkyPosY = 0;
			this.mHasUnlockedMinigames = false;
			this.mHasUnlockedPuzzleMode = false;
			this.mHasNewMiniGame = false;
			this.mHasNewVasebreaker = false;
			this.mHasNewIZombie = false;
			this.mHasNewSurvival = false;
			this.mHasUnlockedSurvivalMode = false;
			this.mNeedsMessageOnGameSelector = false;
			this.mNeedsMagicTacoReward = false;
			this.mNeedsMagicBaconReward = false;
			this.mHasSeenStinky = false;
			this.mHasSeenUpsell = false;
			this.mLastSeenMoreGames = default(DateTime);
			this.mPlaceHolderPlayerStats = new int[1];
			for (int l = 0; l < 1; l++)
			{
				this.mPlaceHolderPlayerStats[l] = 0;
			}
			this.mNumPottedPlants = 0;
			for (int m = 0; m < 18; m++)
			{
				this.mEarnedAchievements[m] = false;
				this.mShownAchievements[m] = false;
			}
			this.mDoVibration = true;
			this.mRunWhileLocked = Main.RunWhenLocked;
			this.mNeedsGrayedPlantWarning = true;
			for (int n = 0; n < this.mPlantTypesUsed.Length; n++)
			{
				this.mPlantTypesUsed[n] = false;
			}
			this.mMiniGamesUnlocked = 0;
			this.mMiniGamesUnlockable = 0;
			this.mVasebreakerUnlocked = 0;
			this.mIZombieUnlocked = 0;
			this.FirstRun = true;
			this.mZenTutorialMessage = -1;
			this.mZenGardenTutorialComplete = false;
			this.mIsDaveTalkingZenTutorial = false;
			this.mIsInZenTutorial = false;
			this.mSeenLeaderboardArrow = false;
			this.mHasSeenAchievementDialog = false;
		}

		public void AddCoins(int theAmount)
		{
			this.mCoins += theAmount;
			if (this.mCoins > 99999)
			{
				this.mCoins = 99999;
				return;
			}
			if (this.mCoins < 0)
			{
				this.mCoins = 0;
			}
		}

		public void DeleteUserFiles()
		{
			string theFileName = GlobalStaticVars.GetDocumentsDir() + Common.StrFormat_("userdata/user{0}.dat", this.mId);
			GlobalStaticVars.gSexyAppBase.EraseFile(theFileName);
			for (int i = 0; i <= 123; i++)
			{
				theFileName = LawnCommon.GetSavedGameName((GameMode)i, (int)this.mId);
				GlobalStaticVars.gSexyAppBase.EraseFile(theFileName);
			}
		}

		public bool LoadDetails()
		{
			bool result;
			try
			{
				string saveFileName = this.GetSaveFileName();
				Sexy.Buffer buffer = new Sexy.Buffer();
				if (!GlobalStaticVars.gSexyAppBase.ReadBufferFromFile(saveFileName, ref buffer, false))
				{
					result = false;
				}
				else
				{
					this.FirstRun = buffer.ReadBoolean();
					this.mMoneySpent = buffer.ReadLong();
					this.mLastStinkyChocolateTime = buffer.ReadDateTime();
					this.mSoundVolume = buffer.ReadDouble();
					this.mMusicVolume = buffer.ReadDouble();
					this.mZenGardenTutorialComplete = buffer.ReadBoolean();
					this.mIsDaveTalkingZenTutorial = buffer.ReadBoolean();
					this.mIsInZenTutorial = buffer.ReadBoolean();
					this.mZenTutorialMessage = buffer.ReadLong();
					this.mChallengeRecords = buffer.ReadLongArray();
					this.mCoins = buffer.ReadLong();
					this.mDidntPurchasePacketUpgrade = buffer.ReadLong();
					this.mDoVibration = buffer.ReadBoolean();
					this.mRunWhileLocked = buffer.ReadBoolean();
					this.mFinishedAdventure = buffer.ReadLong();
					this.mHasNewIZombie = buffer.ReadBoolean();
					this.mHasNewMiniGame = buffer.ReadBoolean();
					this.mHasNewSurvival = buffer.ReadBoolean();
					this.mHasNewVasebreaker = buffer.ReadBoolean();
					this.mHasSeenStinky = buffer.ReadBoolean();
					this.mHasSeenUpsell = buffer.ReadBoolean();
					this.mHasUnlockedMinigames = buffer.ReadBoolean();
					this.mHasUnlockedPuzzleMode = buffer.ReadBoolean();
					this.mHasUnlockedSurvivalMode = buffer.ReadBoolean();
					this.mHasUsedCheatKeys = buffer.ReadBoolean();
					this.mHasWokenStinky = buffer.ReadBoolean();
					this.mHasFinishedTutorial = buffer.ReadBoolean();
					this.mId = (uint)buffer.ReadLong();
					this.mLevel = buffer.ReadLong();
					this.mName = buffer.ReadString();
					SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.GamerSignedInCallback);
					this.mNeedsGrayedPlantWarning = buffer.ReadBoolean();
					this.mNeedsMagicBaconReward = buffer.ReadBoolean();
					this.mNeedsTrialLevelReset = buffer.ReadBoolean();
					this.mNeedsMagicTacoReward = buffer.ReadBoolean();
					this.mNeedsMessageOnGameSelector = buffer.ReadBoolean();
					this.mPlaceHolderPlayerStats = buffer.ReadLongArray();
					this.mPlantTypesUsed = buffer.ReadBooleanArray();
					this.mPlayTimeActivePlayer = buffer.ReadLong();
					this.mPlayTimeInactivePlayer = buffer.ReadLong();
					this.mPurchases = buffer.ReadLongArray();
					this.mShownAchievements = buffer.ReadBooleanArray();
					this.mStinkyPosX = buffer.ReadLong();
					this.mStinkyPosY = buffer.ReadLong();
					this.mUseSeq = (uint)buffer.ReadLong();
					this.mZombiesKilled = (long)buffer.ReadLong();
					this.mVasebreakerScore = (long)buffer.ReadLong();
					this.mIZombieScore = (long)buffer.ReadLong();
					this.mMiniGamesUnlocked = buffer.ReadLong();
					this.mMiniGamesUnlockable = buffer.ReadLong();
					this.mVasebreakerUnlocked = buffer.ReadLong();
					this.mIZombieUnlocked = buffer.ReadLong();
					this.mSeenLeaderboardArrow = buffer.ReadBoolean();
					this.mHasSeenAchievementDialog = buffer.ReadBoolean();
					this.mNumPottedPlants = buffer.ReadLong();
					for (int i = 0; i < this.mNumPottedPlants; i++)
					{
						this.mPottedPlant[i].Load(buffer);
					}
					if (buffer.ReadLong() != 666)
					{
						throw new Exception("User profile: Save check number mismatch");
					}
					Main.RunWhenLocked = this.mRunWhileLocked;
					result = true;
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				this.Reset();
				this.mId = ProfileMgr.GetNewProfileId();
				result = false;
			}
			return result;
		}

		public void GamerSignedInCallback(object sender, SignedInEventArgs args)
		{
			SignedInGamer gamer = args.Gamer;
			this.mName = gamer.Gamertag;
		}

		private string GetSaveFileName()
		{
			return GlobalStaticVars.GetDocumentsDir() + Common.StrFormat_("userdata/user{0}.dat", this.mId);
		}

		public void UnlockFirstMiniGames()
		{
			this.mHasUnlockedMinigames = true;
			this.mMiniGamesUnlocked = 3;
			this.mMiniGamesUnlockable = 3;
		}

		public void UnlockPuzzleMode()
		{
			this.mHasUnlockedPuzzleMode = true;
			this.mIZombieUnlocked = 1;
			this.mVasebreakerUnlocked = 1;
		}

		public void UpdateAchievementInfo()
		{
			for (int i = 0; i < this.mEarnedAchievements.Length; i++)
			{
				AchievementItem achievementItem = Achievements.GetAchievementItem((AchievementId)i);
				if (achievementItem != null)
				{
					this.mEarnedAchievements[i] = achievementItem.IsEarned;
				}
				else
				{
					this.mEarnedAchievements[i] = false;
				}
			}
		}

		public void SaveDetails()
		{
			try
			{
				string saveFileName = this.GetSaveFileName();
				Sexy.Buffer buffer = new Sexy.Buffer();
				buffer.WriteBoolean(this.FirstRun);
				buffer.WriteLong(this.mMoneySpent);
				buffer.WriteDateTime(this.mLastStinkyChocolateTime);
				buffer.WriteDouble(this.mSoundVolume);
				buffer.WriteDouble(this.mMusicVolume);
				buffer.WriteBoolean(this.mZenGardenTutorialComplete);
				buffer.WriteBoolean(this.mIsDaveTalkingZenTutorial);
				buffer.WriteBoolean(this.mIsInZenTutorial);
				buffer.WriteLong(this.mZenTutorialMessage);
				buffer.WriteLongArray(this.mChallengeRecords);
				buffer.WriteLong(this.mCoins);
				buffer.WriteLong(this.mDidntPurchasePacketUpgrade);
				buffer.WriteBoolean(this.mDoVibration);
				buffer.WriteBoolean(this.mRunWhileLocked);
				buffer.WriteLong(this.mFinishedAdventure);
				buffer.WriteBoolean(this.mHasNewIZombie);
				buffer.WriteBoolean(this.mHasNewMiniGame);
				buffer.WriteBoolean(this.mHasNewSurvival);
				buffer.WriteBoolean(this.mHasNewVasebreaker);
				buffer.WriteBoolean(this.mHasSeenStinky);
				buffer.WriteBoolean(this.mHasSeenUpsell);
				buffer.WriteBoolean(this.mHasUnlockedMinigames);
				buffer.WriteBoolean(this.mHasUnlockedPuzzleMode);
				buffer.WriteBoolean(this.mHasUnlockedSurvivalMode);
				buffer.WriteBoolean(this.mHasUsedCheatKeys);
				buffer.WriteBoolean(this.mHasWokenStinky);
				buffer.WriteBoolean(this.mHasFinishedTutorial);
				buffer.WriteLong((int)this.mId);
				buffer.WriteLong(this.mLevel);
				buffer.WriteString(this.mName);
				buffer.WriteBoolean(this.mNeedsGrayedPlantWarning);
				buffer.WriteBoolean(this.mNeedsMagicBaconReward);
				buffer.WriteBoolean(this.mNeedsTrialLevelReset);
				buffer.WriteBoolean(this.mNeedsMagicTacoReward);
				buffer.WriteBoolean(this.mNeedsMessageOnGameSelector);
				buffer.WriteLongArray(this.mPlaceHolderPlayerStats);
				buffer.WriteBooleanArray(this.mPlantTypesUsed);
				buffer.WriteLong(this.mPlayTimeActivePlayer);
				buffer.WriteLong(this.mPlayTimeInactivePlayer);
				buffer.WriteLongArray(this.mPurchases);
				buffer.WriteBooleanArray(this.mShownAchievements);
				buffer.WriteLong(this.mStinkyPosX);
				buffer.WriteLong(this.mStinkyPosY);
				buffer.WriteLong((int)this.mUseSeq);
				buffer.WriteLong((int)this.mZombiesKilled);
				buffer.WriteLong((int)this.mVasebreakerScore);
				buffer.WriteLong((int)this.mIZombieScore);
				buffer.WriteLong(this.mMiniGamesUnlocked);
				buffer.WriteLong(this.mMiniGamesUnlockable);
				buffer.WriteLong(this.mVasebreakerUnlocked);
				buffer.WriteLong(this.mIZombieUnlocked);
				buffer.WriteBoolean(this.mSeenLeaderboardArrow);
				buffer.WriteBoolean(this.mHasSeenAchievementDialog);
				buffer.WriteLong(this.mNumPottedPlants);
				for (int i = 0; i < this.mNumPottedPlants; i++)
				{
					this.mPottedPlant[i].Save(buffer);
				}
				buffer.WriteLong(666);
				GlobalStaticVars.gSexyAppBase.WriteBufferToFile(saveFileName, buffer);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
			}
		}

		public int GetLevel()
		{
			return this.mLevel;
		}

		public void SetLevel(int theLevel)
		{
			this.mLevel = theLevel;
		}

		public void ResetChallengeRecord(GameMode theGameMode)
		{
			int num = theGameMode - GameMode.GAMEMODE_SURVIVAL_NORMAL_STAGE_1;
			Debug.ASSERT(num >= 0 && num < 122);
			this.mChallengeRecords[num] = 0;
		}

		private const int saveCheckNumber = 666;

		public string mName;

		public uint mUseSeq;

		public uint mId;

		private int _level;

		public int mCoins;

		public int mFinishedAdventure;

		public int[] mChallengeRecords;

		public int[] mPurchases;

		public int mPlayTimeActivePlayer;

		public int mPlayTimeInactivePlayer;

		public bool mHasUsedCheatKeys;

		public bool mHasWokenStinky;

		public int mDidntPurchasePacketUpgrade;

		public DateTime mLastStinkyChocolateTime = DateTime.MinValue;

		public int mStinkyPosX;

		public int mStinkyPosY;

		public bool mHasUnlockedMinigames;

		public bool mHasUnlockedPuzzleMode;

		public bool mHasNewMiniGame;

		public bool mHasNewVasebreaker;

		public bool mHasNewIZombie;

		public bool mHasNewSurvival;

		public bool mHasUnlockedSurvivalMode;

		public bool mNeedsMessageOnGameSelector;

		public bool mNeedsMagicTacoReward;

		public bool mNeedsMagicBaconReward;

		public bool mNeedsTrialLevelReset;

		public bool mHasSeenStinky;

		public bool mHasSeenUpsell;

		public int[] mPlaceHolderPlayerStats;

		public int mNumPottedPlants;

		public PottedPlant[] mPottedPlant;

		public bool[] mShownAchievements;

		public bool[] mEarnedAchievements;

		public bool mDoVibration;

		public bool mRunWhileLocked;

		public DateTime mLastSeenMoreGames = default(DateTime);

		public bool mNeedsGrayedPlantWarning;

		public bool[] mPlantTypesUsed;

		public bool mZenGardenTutorialComplete;

		public bool mIsDaveTalkingZenTutorial;

		public bool mIsInZenTutorial;

		public int mZenTutorialMessage;

		public bool mHasFinishedTutorial;

		public long mZombiesKilled;

		public long mVasebreakerScore;

		public long mIZombieScore;

		public int mMiniGamesUnlocked;

		public int mMiniGamesUnlockable;

		public int mVasebreakerUnlocked;

		public int mIZombieUnlocked;

		public bool mSeenLeaderboardArrow;

		public bool mHasSeenAchievementDialog;

		public double mSoundVolume = 0.8;

		public double mMusicVolume = 0.8;

		public int mMoneySpent;

		private bool mFirstRun;
	}
}
