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
                return _level;
            }
            private set
            {
                _level = value;
            }
        }

        public bool FirstRun
        {
            get
            {
                return mFirstRun;
            }
            set
            {
                SexyAppBase.FirstRun = value;
                mFirstRun = false;
            }
        }

        public PlayerInfo()
        {
            Reset();
        }

        public PlayerInfo(uint id) : this()
        {
            mId = id;
        }

        public void Dispose()
        {
        }

        public void Reset()
        {
            mLevel = 1;
            mCoins = 0;
            mFinishedAdventure = 0;
            mPottedPlant = new PottedPlant[200];
            for (int i = 0; i < mPottedPlant.Length; i++)
            {
                mPottedPlant[i] = new PottedPlant();
            }
            mShownAchievements = new bool[18];
            mEarnedAchievements = new bool[18];
            mPlantTypesUsed = new bool[49];
            mChallengeRecords = new int[200];
            for (int j = 0; j < 200; j++)
            {
                mChallengeRecords[j] = 0;
            }
            mPurchases = new int[80];
            for (int k = 0; k < 80; k++)
            {
                mPurchases[k] = 0;
            }
            mZombiesKilled = 0L;
            mVasebreakerScore = 0L;
            mIZombieScore = 0L;
            mPlayTimeActivePlayer = 0;
            mPlayTimeInactivePlayer = 0;
            mHasUsedCheatKeys = false;
            mHasWokenStinky = false;
            mDidntPurchasePacketUpgrade = 0;
            mLastStinkyChocolateTime = DateTime.MinValue;
            mStinkyPosX = 0;
            mStinkyPosY = 0;
            mHasUnlockedMinigames = false;
            mHasUnlockedPuzzleMode = false;
            mHasNewMiniGame = false;
            mHasNewVasebreaker = false;
            mHasNewIZombie = false;
            mHasNewSurvival = false;
            mHasUnlockedSurvivalMode = false;
            mNeedsMessageOnGameSelector = false;
            mNeedsMagicTacoReward = false;
            mNeedsMagicBaconReward = false;
            mHasSeenStinky = false;
            mHasSeenUpsell = false;
            mLastSeenMoreGames = default(DateTime);
            mPlaceHolderPlayerStats = new int[1];
            for (int l = 0; l < 1; l++)
            {
                mPlaceHolderPlayerStats[l] = 0;
            }
            mNumPottedPlants = 0;
            for (int m = 0; m < 18; m++)
            {
                mEarnedAchievements[m] = false;
                mShownAchievements[m] = false;
            }
            mDoVibration = true;
            mRunWhileLocked = Main.RunWhenLocked;
            mNeedsGrayedPlantWarning = true;
            for (int n = 0; n < mPlantTypesUsed.Length; n++)
            {
                mPlantTypesUsed[n] = false;
            }
            mMiniGamesUnlocked = 0;
            mMiniGamesUnlockable = 0;
            mVasebreakerUnlocked = 0;
            mIZombieUnlocked = 0;
            FirstRun = true;
            mZenTutorialMessage = -1;
            mZenGardenTutorialComplete = false;
            mIsDaveTalkingZenTutorial = false;
            mIsInZenTutorial = false;
            mSeenLeaderboardArrow = false;
            mHasSeenAchievementDialog = false;
        }

        public void AddCoins(int theAmount)
        {
            mCoins += theAmount;
            if (mCoins > 99999)
            {
                mCoins = 99999;
                return;
            }
            if (mCoins < 0)
            {
                mCoins = 0;
            }
        }

        public void DeleteUserFiles()
        {
            string theFileName = GlobalStaticVars.GetDocumentsDir() + Common.StrFormat_("userdata/user{0}.dat", mId);
            GlobalStaticVars.gSexyAppBase.EraseFile(theFileName);
            for (int i = 0; i <= 123; i++)
            {
                theFileName = LawnCommon.GetSavedGameName((GameMode)i, (int)mId);
                GlobalStaticVars.gSexyAppBase.EraseFile(theFileName);
            }
        }

        public bool LoadDetails()
        {
            bool result;
            try
            {
                string saveFileName = GetSaveFileName();
                Sexy.Buffer buffer = new Sexy.Buffer();
                if (!GlobalStaticVars.gSexyAppBase.ReadBufferFromFile(saveFileName, ref buffer, false))
                {
                    result = false;
                }
                else
                {
                    FirstRun = buffer.ReadBoolean();
                    mMoneySpent = buffer.ReadLong();
                    mLastStinkyChocolateTime = buffer.ReadDateTime();
                    mSoundVolume = buffer.ReadDouble();
                    mMusicVolume = buffer.ReadDouble();
                    mZenGardenTutorialComplete = buffer.ReadBoolean();
                    mIsDaveTalkingZenTutorial = buffer.ReadBoolean();
                    mIsInZenTutorial = buffer.ReadBoolean();
                    mZenTutorialMessage = buffer.ReadLong();
                    mChallengeRecords = buffer.ReadLongArray();
                    mCoins = buffer.ReadLong();
                    mDidntPurchasePacketUpgrade = buffer.ReadLong();
                    mDoVibration = buffer.ReadBoolean();
                    mRunWhileLocked = buffer.ReadBoolean();
                    mFinishedAdventure = buffer.ReadLong();
                    mHasNewIZombie = buffer.ReadBoolean();
                    mHasNewMiniGame = buffer.ReadBoolean();
                    mHasNewSurvival = buffer.ReadBoolean();
                    mHasNewVasebreaker = buffer.ReadBoolean();
                    mHasSeenStinky = buffer.ReadBoolean();
                    mHasSeenUpsell = buffer.ReadBoolean();
                    mHasUnlockedMinigames = buffer.ReadBoolean();
                    mHasUnlockedPuzzleMode = buffer.ReadBoolean();
                    mHasUnlockedSurvivalMode = buffer.ReadBoolean();
                    mHasUsedCheatKeys = buffer.ReadBoolean();
                    mHasWokenStinky = buffer.ReadBoolean();
                    mHasFinishedTutorial = buffer.ReadBoolean();
                    mId = (uint)buffer.ReadLong();
                    mLevel = buffer.ReadLong();
                    mName = buffer.ReadString();
                    SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(GamerSignedInCallback);
                    mNeedsGrayedPlantWarning = buffer.ReadBoolean();
                    mNeedsMagicBaconReward = buffer.ReadBoolean();
                    mNeedsTrialLevelReset = buffer.ReadBoolean();
                    mNeedsMagicTacoReward = buffer.ReadBoolean();
                    mNeedsMessageOnGameSelector = buffer.ReadBoolean();
                    mPlaceHolderPlayerStats = buffer.ReadLongArray();
                    mPlantTypesUsed = buffer.ReadBooleanArray();
                    mPlayTimeActivePlayer = buffer.ReadLong();
                    mPlayTimeInactivePlayer = buffer.ReadLong();
                    mPurchases = buffer.ReadLongArray();
                    mShownAchievements = buffer.ReadBooleanArray();
                    mStinkyPosX = buffer.ReadLong();
                    mStinkyPosY = buffer.ReadLong();
                    mUseSeq = (uint)buffer.ReadLong();
                    mZombiesKilled = buffer.ReadLong();
                    mVasebreakerScore = buffer.ReadLong();
                    mIZombieScore = buffer.ReadLong();
                    mMiniGamesUnlocked = buffer.ReadLong();
                    mMiniGamesUnlockable = buffer.ReadLong();
                    mVasebreakerUnlocked = buffer.ReadLong();
                    mIZombieUnlocked = buffer.ReadLong();
                    mSeenLeaderboardArrow = buffer.ReadBoolean();
                    mHasSeenAchievementDialog = buffer.ReadBoolean();
                    mNumPottedPlants = buffer.ReadLong();
                    for (int i = 0; i < mNumPottedPlants; i++)
                    {
                        mPottedPlant[i].Load(buffer);
                    }
                    if (buffer.ReadLong() != 666)
                    {
                        throw new Exception("User profile: Save check number mismatch");
                    }
                    Main.RunWhenLocked = mRunWhileLocked;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Reset();
                mId = ProfileMgr.GetNewProfileId();
                result = false;
            }
            return result;
        }

        public void GamerSignedInCallback(object sender, SignedInEventArgs args)
        {
            SignedInGamer gamer = args.Gamer;
            mName = gamer.Gamertag;
        }

        private string GetSaveFileName()
        {
            return GlobalStaticVars.GetDocumentsDir() + Common.StrFormat_("userdata/user{0}.dat", mId);
        }

        public void UnlockFirstMiniGames()
        {
            mHasUnlockedMinigames = true;
            mMiniGamesUnlocked = 3;
            mMiniGamesUnlockable = 3;
        }

        public void UnlockPuzzleMode()
        {
            mHasUnlockedPuzzleMode = true;
            mIZombieUnlocked = 1;
            mVasebreakerUnlocked = 1;
        }

        public void UpdateAchievementInfo()
        {
            for (int i = 0; i < mEarnedAchievements.Length; i++)
            {
                AchievementItem achievementItem = Achievements.GetAchievementItem((AchievementId)i);
                if (achievementItem != null)
                {
                    mEarnedAchievements[i] = achievementItem.IsEarned;
                }
                else
                {
                    mEarnedAchievements[i] = false;
                }
            }
        }

        public void SaveDetails()
        {
            try
            {
                string saveFileName = GetSaveFileName();
                Sexy.Buffer buffer = new Sexy.Buffer();
                buffer.WriteBoolean(FirstRun);
                buffer.WriteLong(mMoneySpent);
                buffer.WriteDateTime(mLastStinkyChocolateTime);
                buffer.WriteDouble(mSoundVolume);
                buffer.WriteDouble(mMusicVolume);
                buffer.WriteBoolean(mZenGardenTutorialComplete);
                buffer.WriteBoolean(mIsDaveTalkingZenTutorial);
                buffer.WriteBoolean(mIsInZenTutorial);
                buffer.WriteLong(mZenTutorialMessage);
                buffer.WriteLongArray(mChallengeRecords);
                buffer.WriteLong(mCoins);
                buffer.WriteLong(mDidntPurchasePacketUpgrade);
                buffer.WriteBoolean(mDoVibration);
                buffer.WriteBoolean(mRunWhileLocked);
                buffer.WriteLong(mFinishedAdventure);
                buffer.WriteBoolean(mHasNewIZombie);
                buffer.WriteBoolean(mHasNewMiniGame);
                buffer.WriteBoolean(mHasNewSurvival);
                buffer.WriteBoolean(mHasNewVasebreaker);
                buffer.WriteBoolean(mHasSeenStinky);
                buffer.WriteBoolean(mHasSeenUpsell);
                buffer.WriteBoolean(mHasUnlockedMinigames);
                buffer.WriteBoolean(mHasUnlockedPuzzleMode);
                buffer.WriteBoolean(mHasUnlockedSurvivalMode);
                buffer.WriteBoolean(mHasUsedCheatKeys);
                buffer.WriteBoolean(mHasWokenStinky);
                buffer.WriteBoolean(mHasFinishedTutorial);
                buffer.WriteLong((int)mId);
                buffer.WriteLong(mLevel);
                buffer.WriteString(mName);
                buffer.WriteBoolean(mNeedsGrayedPlantWarning);
                buffer.WriteBoolean(mNeedsMagicBaconReward);
                buffer.WriteBoolean(mNeedsTrialLevelReset);
                buffer.WriteBoolean(mNeedsMagicTacoReward);
                buffer.WriteBoolean(mNeedsMessageOnGameSelector);
                buffer.WriteLongArray(mPlaceHolderPlayerStats);
                buffer.WriteBooleanArray(mPlantTypesUsed);
                buffer.WriteLong(mPlayTimeActivePlayer);
                buffer.WriteLong(mPlayTimeInactivePlayer);
                buffer.WriteLongArray(mPurchases);
                buffer.WriteBooleanArray(mShownAchievements);
                buffer.WriteLong(mStinkyPosX);
                buffer.WriteLong(mStinkyPosY);
                buffer.WriteLong((int)mUseSeq);
                buffer.WriteLong((int)mZombiesKilled);
                buffer.WriteLong((int)mVasebreakerScore);
                buffer.WriteLong((int)mIZombieScore);
                buffer.WriteLong(mMiniGamesUnlocked);
                buffer.WriteLong(mMiniGamesUnlockable);
                buffer.WriteLong(mVasebreakerUnlocked);
                buffer.WriteLong(mIZombieUnlocked);
                buffer.WriteBoolean(mSeenLeaderboardArrow);
                buffer.WriteBoolean(mHasSeenAchievementDialog);
                buffer.WriteLong(mNumPottedPlants);
                for (int i = 0; i < mNumPottedPlants; i++)
                {
                    mPottedPlant[i].Save(buffer);
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
            return mLevel;
        }

        public void SetLevel(int theLevel)
        {
            mLevel = theLevel;
        }

        public void ResetChallengeRecord(GameMode theGameMode)
        {
            int num = theGameMode - GameMode.SurvivalNormalStage1;
            Debug.ASSERT(num >= 0 && num < 122);
            mChallengeRecords[num] = 0;
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
