using System;
using Microsoft.Xna.Framework.GamerServices;
using Sexy;
using static Lawn.GlobalMembersSaveGame;

namespace Lawn
{
    public/*internal*/ class PlayerInfo
    {
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
            mPottedPlant = new PottedPlant[GameConstants.MAX_POTTED_PLANTS];
            for (int i = 0; i < mPottedPlant.Length; i++)
            {
                mPottedPlant[i] = new PottedPlant();
            }
            mShownAchievements = new bool[18];
            mEarnedAchievements = new bool[18];
            mPlantTypesUsed = new bool[(int)SeedType.SeedsInChooserCount];
            mChallengeRecords = new int[GameConstants.MAX_CHALLENGE_MODES];
            for (int j = 0; j < GameConstants.MAX_CHALLENGE_MODES; j++)
            {
                mChallengeRecords[j] = 0;
            }
            mPurchases = new int[GameConstants.MAX_PURCHASES];
            for (int k = 0; k < GameConstants.MAX_PURCHASES; k++)
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
            for (int l = 0; l < GameConstants.NUM_PLACEHOLDER_INTS; l++)
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

        public bool LoadDetailsOld()
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
                    if (buffer.ReadLong() != saveCheckNumber)
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
                result = false;
            }
            return result;
        }

        public bool LoadDetails()
        {
            string saveFileName = GetSaveFileName();
            BufferNew b = GlobalStaticVars.gSexyAppBase.ReadBufferNewFromFile(saveFileName, false);
            if (b == null)
            {
                return false;
            }
            SaveGameContext saveGameContext = new SaveGameContext();
            saveGameContext.mBuffer = b;
            saveGameContext.mReading = true;
            int version = -1;
            try
            {
                int magic = 0;
                saveGameContext.SyncInt(ref magic);
                saveGameContext.SyncInt(ref saveGameContext.mVersion);
                saveGameContext.SyncInt(ref saveGameContext.mVersionType);
                if (magic != SAVE_FILE_PLAYER_MAGIC_NUMBER)
                {
                    throw new Exception();
                }
                if (saveGameContext.mVersionType != SAVE_FILE_WP_RAW)
                {
                    throw new Exception();
                }
                saveGameContext.InitializePlayerEnum();
                SyncPlayer(saveGameContext);
                version = saveGameContext.mVersion;
            }
            catch (Exception)
            {
                Reset();
                if (!LoadDetailsOld())
                {
                    Reset();
                    mId = ProfileMgr.GetNewProfileId();
                    return false;
                }
            }
            HandleAfterLoading(version);
            return true;
        }

        public void HandleAfterLoading(int version)
        {
            SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(GamerSignedInCallback);
            Main.RunWhenLocked = mRunWhileLocked;
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
            string saveFileName = GetSaveFileName();
            SaveGameContext saveGameContext = new SaveGameContext();
            saveGameContext.mBuffer = new BufferNew();
            saveGameContext.mReading = false;
            int magic = SAVE_FILE_PLAYER_MAGIC_NUMBER;
            saveGameContext.mVersion = 0;
            saveGameContext.mVersionType = SAVE_FILE_WP_RAW;
            try
            {
                saveGameContext.SyncInt(ref magic);
                saveGameContext.SyncInt(ref saveGameContext.mVersion);
                saveGameContext.SyncInt(ref saveGameContext.mVersionType);
                saveGameContext.InitializePlayerEnum();
                SyncPlayer(saveGameContext);
                GlobalStaticVars.gLawnApp.WriteBufferNewToFile(saveFileName, saveGameContext.mBuffer);
            }
            catch (Exception)
            {

            }
        }

        internal void SyncPlayer(SaveGameContext theContext)
        {
            Sync(theContext);
            int check = SAVE_FILE_CHECK;
            theContext.SyncInt(ref check);
            if (check != SAVE_FILE_CHECK)
            {
                throw new Exception("Check error");
            }
        }

        internal void Sync(SaveGameContext theContext)
        {
            theContext.SyncString(ref mName);
            theContext.SyncUint(ref mUseSeq);
            theContext.SyncUint(ref mId);
            theContext.SyncInt(ref mLevel);
            theContext.SyncInt(ref mCoins);
            theContext.SyncInt(ref mFinishedAdventure);
            for (int i = 0; i < 200; i++)
            {
                theContext.SyncInt(ref mChallengeRecords[i]);
            }
            for (int i = 0; i < 80; i++)
            {
                theContext.SyncInt(ref mPurchases[i]);
            }
            theContext.SyncInt(ref mPlayTimeActivePlayer);
            theContext.SyncInt(ref mPlayTimeInactivePlayer);
            theContext.SyncBool(ref mHasUsedCheatKeys);
            theContext.SyncBool(ref mHasWokenStinky);
            theContext.SyncInt(ref mDidntPurchasePacketUpgrade);
            theContext.SyncDateTime(ref mLastStinkyChocolateTime);
            theContext.SyncInt(ref mStinkyPosX);
            theContext.SyncInt(ref mStinkyPosY);
            theContext.SyncBool(ref mHasUnlockedMinigames);
            theContext.SyncBool(ref mHasUnlockedPuzzleMode);
            theContext.SyncBool(ref mHasNewMiniGame);
            theContext.SyncBool(ref mHasNewVasebreaker);
            theContext.SyncBool(ref mHasNewIZombie);
            theContext.SyncBool(ref mHasNewSurvival);
            theContext.SyncBool(ref mHasUnlockedSurvivalMode);
            theContext.SyncBool(ref mNeedsMessageOnGameSelector);
            theContext.SyncBool(ref mNeedsMagicTacoReward);
            theContext.SyncBool(ref mNeedsMagicBaconReward);
            theContext.SyncBool(ref mNeedsTrialLevelReset);
            theContext.SyncBool(ref mHasSeenStinky);
            theContext.SyncBool(ref mHasSeenUpsell);
            for (int i = 0; i < 1; i++)
            {
                theContext.SyncInt(ref mPlaceHolderPlayerStats[i]);
            }
            theContext.SyncInt(ref mNumPottedPlants);
            for (int i = 0; i < 200; i++)
            {
                bool hasPottedPlant = mPottedPlant[i] != null;
                theContext.SyncBool(ref hasPottedPlant);
                if (hasPottedPlant)
                {
                    mPottedPlant[i] ??= new PottedPlant();
                    mPottedPlant[i].Sync(theContext);
                }
                else
                {
                    mPottedPlant[i] = null;
                }
            }
            for (int i = 0; i < 18; i++)
            {
                theContext.SyncBool(ref mShownAchievements[i]);
            }
            for (int i = 0; i < 18; i++)
            {
                theContext.SyncBool(ref mEarnedAchievements[i]);
            }
            theContext.SyncBool(ref mDoVibration);
            theContext.SyncBool(ref mRunWhileLocked);
            theContext.SyncDateTime(ref mLastSeenMoreGames);
            theContext.SyncBool(ref mNeedsGrayedPlantWarning);
            for (int i = 0; i < 49; i++)
            {
                theContext.SyncBool(ref mPlantTypesUsed[i]);
            }
            theContext.SyncBool(ref mZenGardenTutorialComplete);
            theContext.SyncBool(ref mIsDaveTalkingZenTutorial);
            theContext.SyncBool(ref mIsInZenTutorial);
            theContext.SyncInt(ref mZenTutorialMessage);
            theContext.SyncBool(ref mHasFinishedTutorial);
            theContext.SyncLong(ref mZombiesKilled);
            theContext.SyncLong(ref mVasebreakerScore);
            theContext.SyncLong(ref mIZombieScore);
            theContext.SyncInt(ref mMiniGamesUnlocked);
            theContext.SyncInt(ref mMiniGamesUnlockable);
            theContext.SyncInt(ref mVasebreakerUnlocked);
            theContext.SyncInt(ref mIZombieUnlocked);
            theContext.SyncBool(ref mSeenLeaderboardArrow);
            theContext.SyncBool(ref mHasSeenAchievementDialog);
            theContext.SyncDouble(ref mSoundVolume);
            theContext.SyncDouble(ref mMusicVolume);
            theContext.SyncInt(ref mMoneySpent);
            theContext.SyncBool(ref mFirstRun);
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

        public int mLevel;

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
