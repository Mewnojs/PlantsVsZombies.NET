using System;

namespace Sexy.TodLib
{
    public/*internal*/ class FoleyInstance
    {
        public bool mPaused
        {
            get
            {
                return _paused && !mInstance.IsReleased();
            }
            set
            {
                _paused = value;
            }
        }

        public FoleyInstance()
        {
            mInstance = null;
            mRefCount = 0;
            mPaused = false;
            mStartTime = 0;
            mPauseOffset = 0;
        }

        public XNASoundInstance mInstance;

        public int mRefCount;

        private bool _paused;

        public int mStartTime;

        public int mPauseOffset;
    }

    public/*internal*/ class FoleyTypeData
    {
        public FoleyTypeData()
        {
            mLastVariationPlayed = -1;
            for (int i = 0; i < TodLibConstants.MAX_FOLEY_INSTANCES; i++)
            {
                mFoleyInstances[i] = new FoleyInstance();
            }
        }

        public FoleyInstance[] mFoleyInstances = new FoleyInstance[TodLibConstants.MAX_FOLEY_INSTANCES];

        public int mLastVariationPlayed;
    }

    public/*internal*/ class FoleyParams
    {
        public FoleyParams(FoleyType aFoleyType, float aPitchRange, int[] aIDs, uint aFoleyFlags)
        {
            mFoleyType = aFoleyType;
            mPitchRange = aPitchRange;
            mSfxID = aIDs;
            mFoleyFlags = aFoleyFlags;
        }

        public FoleyType mFoleyType;

        public float mPitchRange;

        public int[] mSfxID = new int[TodLibConstants.MAX_FOLEY_VARIATIONS];

        public uint mFoleyFlags;
    }

    public/*internal*/ class TodFoley
    {
        public TodFoley()
        {
            for (int i = 0; i < TodLibConstants.MAX_FOLEY_TYPES; i++)
            {
                mFoleyTypeData[i] = new FoleyTypeData();
            }
        }

        public void PlayFoley(FoleyType theFoleyType)
        {
            FoleyParams foleyParams = TodFoley.LookupFoley(theFoleyType);
            float aPitch = 0f;
            if (foleyParams.mPitchRange != 0f)
            {
                aPitch = TodCommon.RandRangeFloat(0f, foleyParams.mPitchRange);
            }
            PlayFoleyPitch(theFoleyType, aPitch);
        }

        public void StopFoley(FoleyType theFoleyType)
        {
            TodFoley.SoundSystemReleaseFinishedInstances(this);
            FoleyInstance foleyInstance = TodFoley.SoundSystemFindInstance(this, theFoleyType);
            if (foleyInstance == null)
            {
                return;
            }
            foleyInstance.mRefCount--;
            if (foleyInstance.mRefCount == 0)
            {
                foleyInstance.mInstance.Release();
                foleyInstance.mInstance = null;
                foleyInstance.mPaused = false;
            }
        }

        public bool IsFoleyPlaying(FoleyType theFoleyType)
        {
            TodFoley.SoundSystemReleaseFinishedInstances(this);
            return TodFoley.SoundSystemFindInstance(this, theFoleyType) != null;
        }

        public void GamePause(bool theEnteringPause)
        {
            TodFoley.SoundSystemReleaseFinishedInstances(this);
            for (int i = 0; i < TodFoley.gFoleyParamArraySize; i++)
            {
                FoleyParams foleyParams = TodFoley.LookupFoley((FoleyType)i);
                if (TodCommon.TestBit(foleyParams.mFoleyFlags, (int)FoleyFlags.MuteOnPause))
                {
                    FoleyTypeData foleyTypeData = mFoleyTypeData[i];
                    for (int j = 0; j < TodLibConstants.MAX_FOLEY_INSTANCES; j++)
                    {
                        FoleyInstance foleyInstance = foleyTypeData.mFoleyInstances[j];
                        if (foleyInstance.mRefCount != 0)
                        {
                            if (theEnteringPause)
                            {
                                foleyInstance.mPaused = true;
                                foleyInstance.mInstance.Stop();
                            }
                            else if (foleyInstance.mPaused)
                            {
                                foleyInstance.mPaused = false;
                                bool looping = TodCommon.TestBit(foleyParams.mFoleyFlags, (int)FoleyFlags.Loop);
                                foleyInstance.mInstance.Play(looping);
                            }
                        }
                    }
                }
            }
        }

        public void PlayFoleyPitch(FoleyType theFoleyType, float aPitch)
        {
            FoleyParams foleyParams = TodFoley.LookupFoley(theFoleyType);
            TodFoley.SoundSystemReleaseFinishedInstances(this);
            if (TodFoley.SoundSystemHasFoleyPlayedTooRecently(this, theFoleyType) && !TodCommon.TestBit(foleyParams.mFoleyFlags, 0))
            {
                return;
            }
            if (TodCommon.TestBit(foleyParams.mFoleyFlags, (int)FoleyFlags.OneAtATime))
            {
                FoleyInstance foleyInstance = TodFoley.SoundSystemFindInstance(this, theFoleyType);
                if (foleyInstance != null)
                {
                    foleyInstance.mRefCount++;
                    foleyInstance.mStartTime = GlobalStaticVars.gSexyAppBase.mUpdateCount;
                    return;
                }
            }
            FoleyInstance foleyInstance2 = TodFoley.SoundSystemGetFreeInstanceIndex(this, theFoleyType);
            if (foleyInstance2 == null)
            {
                return;
            }
            FoleyTypeData foleyTypeData = mFoleyTypeData[(int)theFoleyType];
            int num = 0;
            for (int i = 0; i < foleyParams.mSfxID.Length; i++)
            {
                if (!TodCommon.TestBit(foleyParams.mFoleyFlags, 4) || foleyTypeData.mLastVariationPlayed != i)
                {
                    int num2 = foleyParams.mSfxID[i];
                    aVariationsArray[num] = i;
                    num++;
                }
            }
            int num3 = TodCommon.TodPickFromArray(aVariationsArray, num);
            foleyTypeData.mLastVariationPlayed = num3;
            int theSfxID = foleyParams.mSfxID[num3];
            SoundInstance soundInstance = GlobalStaticVars.gSexyAppBase.mSoundManager.GetSoundInstance((uint)theSfxID);
            if (soundInstance == null)
            {
                return;
            }
            foleyInstance2.mInstance = (soundInstance as XNASoundInstance);
            foleyInstance2.mRefCount = 1;
            foleyInstance2.mStartTime = GlobalStaticVars.gSexyAppBase.mUpdateCount;
            foleyTypeData.mLastVariationPlayed = num3;
            if (aPitch != 0f)
            {
                soundInstance.AdjustPitch(aPitch / 10f);
            }
            if (TodCommon.TestBit(foleyParams.mFoleyFlags, (int)FoleyFlags.UsesMusicVolume))
            {
                ApplyMusicVolume(foleyInstance2);
            }
            bool looping = TodCommon.TestBit(foleyParams.mFoleyFlags, (int)FoleyFlags.Loop);
            soundInstance.Play(looping);
        }

        public void CancelPausedFoley()
        {
            TodFoley.SoundSystemReleaseFinishedInstances(this);
            for (int i = 0; i < TodFoley.gFoleyParamArraySize; i++)
            {
                FoleyTypeData foleyTypeData = mFoleyTypeData[i];
                for (int j = 0; j < TodLibConstants.MAX_FOLEY_INSTANCES; j++)
                {
                    FoleyInstance foleyInstance = foleyTypeData.mFoleyInstances[j];
                    if (foleyInstance.mRefCount != 0 && foleyInstance.mPaused)
                    {
                        foleyInstance.mRefCount = 0;
                        foleyInstance.mInstance.Release();
                        foleyInstance.mInstance = null;
                    }
                }
            }
        }

        public void ApplyMusicVolume(FoleyInstance theFoleyInstance)
        {
            if (GlobalStaticVars.gSexyAppBase.mSfxVolume < 9.999999974752427E-07)
            {
                theFoleyInstance.mInstance.SetVolume(0.0);
                return;
            }
            double volume = GlobalStaticVars.gSexyAppBase.mMusicVolume * GlobalStaticVars.gSexyAppBase.mSfxVolume;
            theFoleyInstance.mInstance.SetVolume(volume);
        }

        public void RehookupSoundWithMusicVolume()
        {
            TodFoley.SoundSystemReleaseFinishedInstances(this);
            for (int i = 0; i < TodFoley.gFoleyParamArraySize; i++)
            {
                FoleyParams foleyParams = TodFoley.LookupFoley((FoleyType)i);
                if (TodCommon.TestBit(foleyParams.mFoleyFlags, (int)FoleyFlags.UsesMusicVolume))
                {
                    FoleyTypeData foleyTypeData = mFoleyTypeData[i];
                    for (int j = 0; j < TodLibConstants.MAX_FOLEY_INSTANCES; j++)
                    {
                        FoleyInstance foleyInstance = foleyTypeData.mFoleyInstances[j];
                        if (foleyInstance.mRefCount != 0)
                        {
                            ApplyMusicVolume(foleyInstance);
                        }
                    }
                }
            }
        }

        public static void TodFoleyInitialize(FoleyParams[] theFoleyParamArray, int theFoleyParamArraySize)
        {
            TodFoley.gFoleyParamArray = new FoleyParams[]
            {
                new FoleyParams(FoleyType.Sun, 10f, new int[]
                {
                    Resources.SOUND_POINTS
                }, 0U),
                new FoleyParams(FoleyType.Splat, 10f, new int[]
                {
                    Resources.SOUND_SPLAT,
                    Resources.SOUND_SPLAT2,
                    Resources.SOUND_SPLAT3
                }, 0U),
                new FoleyParams(FoleyType.Lawnmower, 10f, new int[]
                {
                    Resources.SOUND_LAWNMOWER
                }, 0U),
                new FoleyParams(FoleyType.Throw, 10f, new int[]
                {
                    Resources.SOUND_THROW,
                    Resources.SOUND_THROW,
                    Resources.SOUND_THROW,
                    Resources.SOUND_THROW2
                }, 0U),
                new FoleyParams(FoleyType.SpawnSun, 10f, new int[]
                {
                    Resources.SOUND_THROW
                }, 0U),
                new FoleyParams(FoleyType.Chomp, 0f, new int[]
                {
                    Resources.SOUND_CHOMP,
                    Resources.SOUND_CHOMP2
                }, 0U),
                new FoleyParams(FoleyType.ChompSoft, 4f, new int[]
                {
                    Resources.SOUND_CHOMPSOFT
                }, 0U),
                new FoleyParams(FoleyType.Plant, 0f, new int[]
                {
                    Resources.SOUND_PLANT,
                    Resources.SOUND_PLANT2
                }, 0U),
                new FoleyParams(FoleyType.UseShovel, 0f, new int[]
                {
                    Resources.SOUND_PLANT2
                }, 0U),
                new FoleyParams(FoleyType.Drop, 0f, new int[]
                {
                    Resources.SOUND_TAP2
                }, 0U),
                new FoleyParams(FoleyType.Bleep, 0f, new int[]
                {
                    Resources.SOUND_BLEEP
                }, 0U),
                new FoleyParams(FoleyType.Groan, 0f, new int[]
                {
                    Resources.SOUND_GROAN,
                    Resources.SOUND_GROAN2,
                    Resources.SOUND_GROAN3,
                    Resources.SOUND_GROAN4,
                    Resources.SOUND_GROAN5,
                    Resources.SOUND_GROAN6
                }, 0U),
                new FoleyParams(FoleyType.Brains, 0f, new int[]
                {
                    Resources.SOUND_GROAN,
                    Resources.SOUND_GROAN2,
                    Resources.SOUND_GROAN3,
                    Resources.SOUND_GROAN4,
                    Resources.SOUND_GROAN5,
                    Resources.SOUND_GROAN6,
                    Resources.SOUND_SUKHBIR4,
                    Resources.SOUND_SUKHBIR5,
                    Resources.SOUND_SUKHBIR6
                }, 0U),
                new FoleyParams(FoleyType.Jackinthebox, 0f, new int[]
                {
                    Resources.SOUND_JACKINTHEBOX
                }, 7U),
                new FoleyParams(FoleyType.ArtChallenge, 0f, new int[]
                {
                    Resources.SOUND_DIAMOND
                }, 0U),
                new FoleyParams(FoleyType.Zamboni, 5f, new int[]
                {
                    Resources.SOUND_ZAMBONI
                }, 0U),
                new FoleyParams(FoleyType.Thunder, 10f, new int[]
                {
                    Resources.SOUND_THUNDER
                }, 0U),
                new FoleyParams(FoleyType.Frozen, 0f, new int[]
                {
                    Resources.SOUND_FROZEN
                }, 0U),
                new FoleyParams(FoleyType.Zombiesplash, 10f, new int[]
                {
                    Resources.SOUND_PLANT_WATER,
                    Resources.SOUND_ZOMBIE_ENTERING_WATER
                }, 0U),
                new FoleyParams(FoleyType.Bowlingimpact, -3f, new int[]
                {
                    Resources.SOUND_BOWLINGIMPACT
                }, 0U),
                new FoleyParams(FoleyType.Squish, 0f, new int[]
                {
                    Resources.SOUND_CHOMP,
                    Resources.SOUND_CHOMP2
                }, 0U),
                new FoleyParams(FoleyType.TirePop, 0f, new int[]
                {
                    Resources.SOUND_BALLOON_POP
                }, 0U),
                new FoleyParams(FoleyType.Explosion, 0f, new int[]
                {
                    Resources.SOUND_EXPLOSION
                }, 0U),
                new FoleyParams(FoleyType.Slurp, 2f, new int[]
                {
                    Resources.SOUND_SLURP
                }, 0U),
                new FoleyParams(FoleyType.LimbsPop, 10f, new int[]
                {
                    Resources.SOUND_LIMBS_POP
                }, 0U),
                new FoleyParams(FoleyType.PogoZombie, 4f, new int[]
                {
                    Resources.SOUND_POGO_ZOMBIE
                }, 0U),
                new FoleyParams(FoleyType.SnowPeaSparkles, 10f, new int[]
                {
                    Resources.SOUND_SNOW_PEA_SPARKLES
                }, 0U),
                new FoleyParams(FoleyType.ZombieFalling, 10f, new int[]
                {
                    Resources.SOUND_ZOMBIE_FALLING_1,
                    Resources.SOUND_ZOMBIE_FALLING_2
                }, 0U),
                new FoleyParams(FoleyType.Puff, 10f, new int[]
                {
                    Resources.SOUND_PUFF
                }, 0U),
                new FoleyParams(FoleyType.Fume, 10f, new int[]
                {
                    Resources.SOUND_FUME
                }, 0U),
                new FoleyParams(FoleyType.Coin, 10f, new int[]
                {
                    Resources.SOUND_COIN
                }, 0U),
                new FoleyParams(FoleyType.KernelSplat, 10f, new int[]
                {
                    Resources.SOUND_KERNELPULT,
                    Resources.SOUND_KERNELPULT2
                }, 0U),
                new FoleyParams(FoleyType.Digger, 0f, new int[]
                {
                    Resources.SOUND_DIGGER_ZOMBIE
                }, 7U),
                new FoleyParams(FoleyType.JackSurprise, 1f, new int[]
                {
                    Resources.SOUND_JACK_SURPRISE,
                    Resources.SOUND_JACK_SURPRISE,
                    Resources.SOUND_JACK_SURPRISE2
                }, 0U),
                new FoleyParams(FoleyType.VaseBreaking, -5f, new int[]
                {
                    Resources.SOUND_VASE_BREAKING
                }, 0U),
                new FoleyParams(FoleyType.PoolCleaner, 2f, new int[]
                {
                    Resources.SOUND_POOL_CLEANER
                }, 0U),
                new FoleyParams(FoleyType.Basketball, 10f, new int[]
                {
                    Resources.SOUND_BASKETBALL
                }, 0U),
                new FoleyParams(FoleyType.Ignite, 5f, new int[]
                {
                    Resources.SOUND_IGNITE,
                    Resources.SOUND_IGNITE,
                    Resources.SOUND_IGNITE,
                    Resources.SOUND_IGNITE2
                }, 0U),
                new FoleyParams(FoleyType.Firepea, 10f, new int[]
                {
                    Resources.SOUND_FIREPEA
                }, 0U),
                new FoleyParams(FoleyType.Thump, 2f, new int[]
                {
                    Resources.SOUND_GARGANTUAR_THUMP
                }, 0U),
                new FoleyParams(FoleyType.SquashHmm, 2f, new int[]
                {
                    Resources.SOUND_SQUASH_HMM,
                    Resources.SOUND_SQUASH_HMM,
                    Resources.SOUND_SQUASH_HMM2
                }, 0U),
                new FoleyParams(FoleyType.Magnetshroom, 2f, new int[]
                {
                    Resources.SOUND_MAGNETSHROOM
                }, 0U),
                new FoleyParams(FoleyType.Butter, 2f, new int[]
                {
                    Resources.SOUND_BUTTER
                }, 0U),
                new FoleyParams(FoleyType.BungeeScream, 2f, new int[]
                {
                    Resources.SOUND_BUNGEE_SCREAM,
                    Resources.SOUND_BUNGEE_SCREAM2,
                    Resources.SOUND_BUNGEE_SCREAM3
                }, 0U),
                new FoleyParams(FoleyType.BossExplosionSmall, 2f, new int[]
                {
                    Resources.SOUND_EXPLOSION
                }, 0U),
                new FoleyParams(FoleyType.ShieldHit, 10f, new int[]
                {
                    Resources.SOUND_SHIELDHIT,
                    Resources.SOUND_SHIELDHIT2
                }, 0U),
                new FoleyParams(FoleyType.Swing, 2f, new int[]
                {
                    Resources.SOUND_SWING
                }, 0U),
                new FoleyParams(FoleyType.Bonk, 2f, new int[]
                {
                    Resources.SOUND_BONK
                }, 0U),
                new FoleyParams(FoleyType.Rain, 0f, new int[]
                {
                    Resources.SOUND_RAIN
                }, 5U),
                new FoleyParams(FoleyType.DolphinBeforeJumping, 0f, new int[]
                {
                    Resources.SOUND_DOLPHIN_BEFORE_JUMPING
                }, 0U),
                new FoleyParams(FoleyType.DolphinAppears, 0f, new int[]
                {
                    Resources.SOUND_DOLPHIN_APPEARS
                }, 0U),
                new FoleyParams(FoleyType.PlantWater, 0f, new int[]
                {
                    Resources.SOUND_PLANT_WATER
                }, 0U),
                new FoleyParams(FoleyType.ZombieEnteringWater, 0f, new int[]
                {
                    Resources.SOUND_ZOMBIE_ENTERING_WATER
                }, 0U),
                new FoleyParams(FoleyType.Gravebusterchomp, 0f, new int[]
                {
                    Resources.SOUND_GRAVEBUSTERCHOMP
                }, 4U),
                new FoleyParams(FoleyType.Cherrybomb, 0f, new int[]
                {
                    Resources.SOUND_CHERRYBOMB
                }, 0U),
                new FoleyParams(FoleyType.JalapenoIgnite, 0f, new int[]
                {
                    Resources.SOUND_JALAPENO
                }, 0U),
                new FoleyParams(FoleyType.ReverseExplosion, 0f, new int[]
                {
                    Resources.SOUND_REVERSE_EXPLOSION
                }, 0U),
                new FoleyParams(FoleyType.PlasticHit, 5f, new int[]
                {
                    Resources.SOUND_PLASTICHIT,
                    Resources.SOUND_PLASTICHIT2
                }, 0U),
                new FoleyParams(FoleyType.Winmusic, 0f, new int[]
                {
                    Resources.SOUND_WINMUSIC
                }, 8U),
                new FoleyParams(FoleyType.Ballooninflate, 10f, new int[]
                {
                    Resources.SOUND_BALLOONINFLATE
                }, 0U),
                new FoleyParams(FoleyType.Bigchomp, -2f, new int[]
                {
                    Resources.SOUND_BIGCHOMP
                }, 0U),
                new FoleyParams(FoleyType.Melonimpact, -5f, new int[]
                {
                    Resources.SOUND_MELONIMPACT,
                    Resources.SOUND_MELONIMPACT2
                }, 0U),
                new FoleyParams(FoleyType.Plantgrow, -2f, new int[]
                {
                    Resources.SOUND_PLANTGROW
                }, 0U),
                new FoleyParams(FoleyType.Shoop, -5f, new int[]
                {
                    Resources.SOUND_SHOOP
                }, 0U),
                new FoleyParams(FoleyType.Juicy, 2f, new int[]
                {
                    Resources.SOUND_JUICY
                }, 0U),
                new FoleyParams(FoleyType.NewspaperRarrgh, -2f, new int[]
                {
                    Resources.SOUND_NEWSPAPER_RARRGH,
                    Resources.SOUND_NEWSPAPER_RARRGH2,
                    Resources.SOUND_NEWSPAPER_RARRGH2
                }, 0U),
                new FoleyParams(FoleyType.NewspaperRip, -2f, new int[]
                {
                    Resources.SOUND_NEWSPAPER_RIP
                }, 0U),
                new FoleyParams(FoleyType.Floop, 0f, new int[]
                {
                    Resources.SOUND_FLOOP
                }, 0U),
                new FoleyParams(FoleyType.Coffee, 0f, new int[]
                {
                    Resources.SOUND_COFFEE
                }, 0U),
                new FoleyParams(FoleyType.Lowgroan, 2f, new int[]
                {
                    Resources.SOUND_LOWGROAN,
                    Resources.SOUND_LOWGROAN2
                }, 0U),
                new FoleyParams(FoleyType.Prize, 0f, new int[]
                {
                    Resources.SOUND_PRIZE
                }, 0U),
                new FoleyParams(FoleyType.Yuck, 1f, new int[]
                {
                    Resources.SOUND_YUCK,
                    Resources.SOUND_YUCK,
                    Resources.SOUND_YUCK2
                }, 0U),
                new FoleyParams(FoleyType.Umbrella, 2f, new int[]
                {
                    Resources.SOUND_THROW2
                }, 0U),
                new FoleyParams(FoleyType.Grassstep, 2f, new int[]
                {
                    Resources.SOUND_GRASSSTEP
                }, 0U),
                new FoleyParams(FoleyType.Shovel, 5f, new int[]
                {
                    Resources.SOUND_SHOVEL
                }, 0U),
                new FoleyParams(FoleyType.Coblaunch, 10f, new int[]
                {
                    Resources.SOUND_COBLAUNCH
                }, 0U),
                new FoleyParams(FoleyType.Watering, 10f, new int[]
                {
                    Resources.SOUND_WATERING
                }, 0U),
                new FoleyParams(FoleyType.Polevault, 5f, new int[]
                {
                    Resources.SOUND_POLEVAULT
                }, 0U),
                new FoleyParams(FoleyType.GravestoneRumble, 10f, new int[]
                {
                    Resources.SOUND_GRAVESTONE_RUMBLE
                }, 0U),
                new FoleyParams(FoleyType.DirtRise, 5f, new int[]
                {
                    Resources.SOUND_DIRT_RISE
                }, 0U),
                new FoleyParams(FoleyType.Fertilizer, 0f, new int[]
                {
                    Resources.SOUND_FERTILIZER
                }, 0U),
                new FoleyParams(FoleyType.Portal, 0f, new int[]
                {
                    Resources.SOUND_PORTAL
                }, 0U),
                new FoleyParams(FoleyType.Wakeup, 0f, new int[]
                {
                    Resources.SOUND_WAKEUP
                }, 0U),
                new FoleyParams(FoleyType.Bugspray, 0f, new int[]
                {
                    Resources.SOUND_BUGSPRAY
                }, 0U),
                new FoleyParams(FoleyType.Scream, 0f, new int[]
                {
                    Resources.SOUND_SCREAM
                }, 0U),
                new FoleyParams(FoleyType.Paper, 0f, new int[]
                {
                    Resources.SOUND_PAPER
                }, 0U),
                new FoleyParams(FoleyType.Moneyfalls, 0f, new int[]
                {
                    Resources.SOUND_MONEYFALLS
                }, 0U),
                new FoleyParams(FoleyType.Imp, 5f, new int[]
                {
                    Resources.SOUND_IMP,
                    Resources.SOUND_IMP2
                }, 0U),
                new FoleyParams(FoleyType.HydraulicShort, 3f, new int[]
                {
                    Resources.SOUND_HYDRAULIC_SHORT
                }, 0U),
                new FoleyParams(FoleyType.Hydraulic, 0f, new int[]
                {
                    Resources.SOUND_HYDRAULIC
                }, 0U),
                new FoleyParams(FoleyType.Gargantudeath, 3f, new int[]
                {
                    Resources.SOUND_GARGANTUDEATH
                }, 0U),
                new FoleyParams(FoleyType.Ceramic, 0f, new int[]
                {
                    Resources.SOUND_CERAMIC
                }, 0U),
                new FoleyParams(FoleyType.Bossboulderattack, 0f, new int[]
                {
                    Resources.SOUND_BOSSBOULDERATTACK
                }, 0U),
                new FoleyParams(FoleyType.Chime, 0f, new int[]
                {
                    Resources.SOUND_CHIME
                }, 0U),
                new FoleyParams(FoleyType.Crazydaveshort, 0f, new int[]
                {
                    Resources.SOUND_CRAZYDAVESHORT1,
                    Resources.SOUND_CRAZYDAVESHORT2,
                    Resources.SOUND_CRAZYDAVESHORT3
                }, 16U),
                new FoleyParams(FoleyType.Crazydavelong, 0f, new int[]
                {
                    Resources.SOUND_CRAZYDAVELONG1,
                    Resources.SOUND_CRAZYDAVELONG2,
                    Resources.SOUND_CRAZYDAVELONG3
                }, 16U),
                new FoleyParams(FoleyType.Crazydaveextralong, 0f, new int[]
                {
                    Resources.SOUND_CRAZYDAVEEXTRALONG1,
                    Resources.SOUND_CRAZYDAVEEXTRALONG2,
                    Resources.SOUND_CRAZYDAVEEXTRALONG3
                }, 16U),
                new FoleyParams(FoleyType.Crazydavecrazy, 0f, new int[]
                {
                    Resources.SOUND_CRAZYDAVECRAZY
                }, 0U),
                new FoleyParams(FoleyType.Phonograph, 0f, new int[]
                {
                    Resources.SOUND_PHONOGRAPH
                }, 0U),
                new FoleyParams(FoleyType.Dancer, 0f, new int[]
                {
                    Resources.SOUND_DANCER
                }, 6U),
                new FoleyParams(FoleyType.Finalfanfare, 0f, new int[]
                {
                    Resources.SOUND_FINALFANFARE
                }, 0U),
                new FoleyParams(FoleyType.Crazydavescream, 0f, new int[]
                {
                    Resources.SOUND_CRAZYDAVESCREAM
                }, 0U),
                new FoleyParams(FoleyType.Crazydavescream2, 0f, new int[]
                {
                    Resources.SOUND_CRAZYDAVESCREAM2
                }, 0U)
            };
            TodFoley.gFoleyParamArraySize = theFoleyParamArraySize;
        }

        public static void TodFoleyDispose()
        {
            TodFoley.gFoleyParamArray = null;
            TodFoley.gFoleyParamArraySize = 0;
        }

        public static FoleyParams LookupFoley(FoleyType theFoleyType)
        {
            return TodFoley.gFoleyParamArray[(int)theFoleyType];
        }

        public static void SoundSystemReleaseFinishedInstances(TodFoley theSoundSystem)
        {
            for (int i = 0; i < TodFoley.gFoleyParamArraySize; i++)
            {
                FoleyTypeData foleyTypeData = theSoundSystem.mFoleyTypeData[i];
                if (foleyTypeData != null)
                {
                    for (int j = 0; j < TodLibConstants.MAX_FOLEY_INSTANCES; j++)
                    {
                        FoleyInstance foleyInstance = foleyTypeData.mFoleyInstances[j];
                        if (foleyInstance.mRefCount != 0 && !foleyInstance.mPaused && !foleyInstance.mInstance.IsPlaying())
                        {
                            foleyInstance.mInstance.Release();
                            foleyInstance.mInstance = null;
                            foleyInstance.mRefCount = 0;
                        }
                    }
                }
            }
        }

        public static FoleyInstance SoundSystemFindInstance(TodFoley theSoundSystem, FoleyType theFoleyType)
        {
            FoleyTypeData foleyTypeData = theSoundSystem.mFoleyTypeData[(int)theFoleyType];
            for (int i = 0; i < TodLibConstants.MAX_FOLEY_INSTANCES; i++)
            {
                FoleyInstance foleyInstance = foleyTypeData.mFoleyInstances[i];
                if (foleyInstance.mRefCount > 0)
                {
                    return foleyInstance;
                }
            }
            return null;
        }

        public static bool SoundSystemHasFoleyPlayedTooRecently(TodFoley theSoundSystem, FoleyType theFoleyType)
        {
            FoleyTypeData foleyTypeData = theSoundSystem.mFoleyTypeData[(int)theFoleyType];
            if (foleyTypeData != null)
            {
                for (int i = 0; i < TodLibConstants.MAX_FOLEY_INSTANCES; i++)
                {
                    FoleyInstance foleyInstance = foleyTypeData.mFoleyInstances[i];
                    if (foleyInstance.mRefCount != 0)
                    {
                        int num = GlobalStaticVars.gSexyAppBase.mUpdateCount - foleyInstance.mStartTime;
                        if (num < 2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static FoleyInstance SoundSystemGetFreeInstanceIndex(TodFoley theSoundSystem, FoleyType theFoleyType)
        {
            FoleyTypeData foleyTypeData = theSoundSystem.mFoleyTypeData[(int)theFoleyType];
            if (foleyTypeData != null)
            {
                for (int i = 0; i < TodLibConstants.MAX_FOLEY_INSTANCES; i++)
                {
                    FoleyInstance foleyInstance = foleyTypeData.mFoleyInstances[i];
                    if (foleyInstance.mRefCount == 0)
                    {
                        return foleyInstance;
                    }
                }
            }
            return null;
        }

        public FoleyTypeData[] mFoleyTypeData = new FoleyTypeData[TodLibConstants.MAX_FOLEY_TYPES];

        private int[] aVariationsArray = new int[TodLibConstants.MAX_FOLEY_VARIATIONS];

        public static FoleyParams[] gFoleyParamArray;

        public static int gFoleyParamArraySize;
    }

    public enum FoleyFlags
    {
        Loop,
        OneAtATime,
        MuteOnPause,
        UsesMusicVolume,
        DontRepeat
    }
}
