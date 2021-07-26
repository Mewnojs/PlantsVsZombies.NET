using System;

namespace Sexy.TodLib
{
	internal class TodFoley
	{
		public TodFoley()
		{
			for (int i = 0; i < 110; i++)
			{
				this.mFoleyTypeData[i] = new FoleyTypeData();
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
			this.PlayFoleyPitch(theFoleyType, aPitch);
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
				if (TodCommon.TestBit(foleyParams.mFoleyFlags, 2))
				{
					FoleyTypeData foleyTypeData = this.mFoleyTypeData[i];
					for (int j = 0; j < 8; j++)
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
								bool looping = TodCommon.TestBit(foleyParams.mFoleyFlags, 0);
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
			if (TodCommon.TestBit(foleyParams.mFoleyFlags, 1))
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
			FoleyTypeData foleyTypeData = this.mFoleyTypeData[(int)theFoleyType];
			int num = 0;
			for (int i = 0; i < foleyParams.mSfxID.Length; i++)
			{
				if (!TodCommon.TestBit(foleyParams.mFoleyFlags, 4) || foleyTypeData.mLastVariationPlayed != i)
				{
					int num2 = foleyParams.mSfxID[i];
					this.aVariationsArray[num] = i;
					num++;
				}
			}
			int num3 = TodCommon.TodPickFromArray(this.aVariationsArray, num);
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
				soundInstance.AdjustPitch((double)(aPitch / 10f));
			}
			if (TodCommon.TestBit(foleyParams.mFoleyFlags, 3))
			{
				this.ApplyMusicVolume(foleyInstance2);
			}
			bool looping = TodCommon.TestBit(foleyParams.mFoleyFlags, 0);
			soundInstance.Play(looping);
		}

		public void CancelPausedFoley()
		{
			TodFoley.SoundSystemReleaseFinishedInstances(this);
			for (int i = 0; i < TodFoley.gFoleyParamArraySize; i++)
			{
				FoleyTypeData foleyTypeData = this.mFoleyTypeData[i];
				for (int j = 0; j < 8; j++)
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
				if (TodCommon.TestBit(foleyParams.mFoleyFlags, 3))
				{
					FoleyTypeData foleyTypeData = this.mFoleyTypeData[i];
					for (int j = 0; j < 8; j++)
					{
						FoleyInstance foleyInstance = foleyTypeData.mFoleyInstances[j];
						if (foleyInstance.mRefCount != 0)
						{
							this.ApplyMusicVolume(foleyInstance);
						}
					}
				}
			}
		}

		public static void TodFoleyInitialize(FoleyParams[] theFoleyParamArray, int theFoleyParamArraySize)
		{
			TodFoley.gFoleyParamArray = new FoleyParams[]
			{
				new FoleyParams(FoleyType.FOLEY_SUN, 10f, new int[]
				{
					Resources.SOUND_POINTS
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SPLAT, 10f, new int[]
				{
					Resources.SOUND_SPLAT,
					Resources.SOUND_SPLAT2,
					Resources.SOUND_SPLAT3
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_LAWNMOWER, 10f, new int[]
				{
					Resources.SOUND_LAWNMOWER
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_THROW, 10f, new int[]
				{
					Resources.SOUND_THROW,
					Resources.SOUND_THROW,
					Resources.SOUND_THROW,
					Resources.SOUND_THROW2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SPAWN_SUN, 10f, new int[]
				{
					Resources.SOUND_THROW
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_CHOMP, 0f, new int[]
				{
					Resources.SOUND_CHOMP,
					Resources.SOUND_CHOMP2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_CHOMP_SOFT, 4f, new int[]
				{
					Resources.SOUND_CHOMPSOFT
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_PLANT, 0f, new int[]
				{
					Resources.SOUND_PLANT,
					Resources.SOUND_PLANT2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_USE_SHOVEL, 0f, new int[]
				{
					Resources.SOUND_PLANT2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_DROP, 0f, new int[]
				{
					Resources.SOUND_TAP2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BLEEP, 0f, new int[]
				{
					Resources.SOUND_BLEEP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_GROAN, 0f, new int[]
				{
					Resources.SOUND_GROAN,
					Resources.SOUND_GROAN2,
					Resources.SOUND_GROAN3,
					Resources.SOUND_GROAN4,
					Resources.SOUND_GROAN5,
					Resources.SOUND_GROAN6
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BRAINS, 0f, new int[]
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
				new FoleyParams(FoleyType.FOLEY_JACKINTHEBOX, 0f, new int[]
				{
					Resources.SOUND_JACKINTHEBOX
				}, 7U),
				new FoleyParams(FoleyType.FOLEY_ART_CHALLENGE, 0f, new int[]
				{
					Resources.SOUND_DIAMOND
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_ZAMBONI, 5f, new int[]
				{
					Resources.SOUND_ZAMBONI
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_THUNDER, 10f, new int[]
				{
					Resources.SOUND_THUNDER
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_FROZEN, 0f, new int[]
				{
					Resources.SOUND_FROZEN
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_ZOMBIESPLASH, 10f, new int[]
				{
					Resources.SOUND_PLANT_WATER,
					Resources.SOUND_ZOMBIE_ENTERING_WATER
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BOWLINGIMPACT, -3f, new int[]
				{
					Resources.SOUND_BOWLINGIMPACT
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SQUISH, 0f, new int[]
				{
					Resources.SOUND_CHOMP,
					Resources.SOUND_CHOMP2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_TIRE_POP, 0f, new int[]
				{
					Resources.SOUND_BALLOON_POP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_EXPLOSION, 0f, new int[]
				{
					Resources.SOUND_EXPLOSION
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SLURP, 2f, new int[]
				{
					Resources.SOUND_SLURP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_LIMBS_POP, 10f, new int[]
				{
					Resources.SOUND_LIMBS_POP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_POGO_ZOMBIE, 4f, new int[]
				{
					Resources.SOUND_POGO_ZOMBIE
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SNOW_PEA_SPARKLES, 10f, new int[]
				{
					Resources.SOUND_SNOW_PEA_SPARKLES
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_ZOMBIE_FALLING, 10f, new int[]
				{
					Resources.SOUND_ZOMBIE_FALLING_1,
					Resources.SOUND_ZOMBIE_FALLING_2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_PUFF, 10f, new int[]
				{
					Resources.SOUND_PUFF
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_FUME, 10f, new int[]
				{
					Resources.SOUND_FUME
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_COIN, 10f, new int[]
				{
					Resources.SOUND_COIN
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_KERNEL_SPLAT, 10f, new int[]
				{
					Resources.SOUND_KERNELPULT,
					Resources.SOUND_KERNELPULT2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_DIGGER, 0f, new int[]
				{
					Resources.SOUND_DIGGER_ZOMBIE
				}, 7U),
				new FoleyParams(FoleyType.FOLEY_JACK_SURPRISE, 1f, new int[]
				{
					Resources.SOUND_JACK_SURPRISE,
					Resources.SOUND_JACK_SURPRISE,
					Resources.SOUND_JACK_SURPRISE2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_VASE_BREAKING, -5f, new int[]
				{
					Resources.SOUND_VASE_BREAKING
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_POOL_CLEANER, 2f, new int[]
				{
					Resources.SOUND_POOL_CLEANER
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BASKETBALL, 10f, new int[]
				{
					Resources.SOUND_BASKETBALL
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_IGNITE, 5f, new int[]
				{
					Resources.SOUND_IGNITE,
					Resources.SOUND_IGNITE,
					Resources.SOUND_IGNITE,
					Resources.SOUND_IGNITE2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_FIREPEA, 10f, new int[]
				{
					Resources.SOUND_FIREPEA
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_THUMP, 2f, new int[]
				{
					Resources.SOUND_GARGANTUAR_THUMP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SQUASH_HMM, 2f, new int[]
				{
					Resources.SOUND_SQUASH_HMM,
					Resources.SOUND_SQUASH_HMM,
					Resources.SOUND_SQUASH_HMM2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_MAGNETSHROOM, 2f, new int[]
				{
					Resources.SOUND_MAGNETSHROOM
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BUTTER, 2f, new int[]
				{
					Resources.SOUND_BUTTER
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BUNGEE_SCREAM, 2f, new int[]
				{
					Resources.SOUND_BUNGEE_SCREAM,
					Resources.SOUND_BUNGEE_SCREAM2,
					Resources.SOUND_BUNGEE_SCREAM3
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BOSS_EXPLOSION_SMALL, 2f, new int[]
				{
					Resources.SOUND_EXPLOSION
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SHIELD_HIT, 10f, new int[]
				{
					Resources.SOUND_SHIELDHIT,
					Resources.SOUND_SHIELDHIT2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SWING, 2f, new int[]
				{
					Resources.SOUND_SWING
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BONK, 2f, new int[]
				{
					Resources.SOUND_BONK
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_RAIN, 0f, new int[]
				{
					Resources.SOUND_RAIN
				}, 5U),
				new FoleyParams(FoleyType.FOLEY_DOLPHIN_BEFORE_JUMPING, 0f, new int[]
				{
					Resources.SOUND_DOLPHIN_BEFORE_JUMPING
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_DOLPHIN_APPEARS, 0f, new int[]
				{
					Resources.SOUND_DOLPHIN_APPEARS
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_PLANT_WATER, 0f, new int[]
				{
					Resources.SOUND_PLANT_WATER
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_ZOMBIE_ENTERING_WATER, 0f, new int[]
				{
					Resources.SOUND_ZOMBIE_ENTERING_WATER
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_GRAVEBUSTERCHOMP, 0f, new int[]
				{
					Resources.SOUND_GRAVEBUSTERCHOMP
				}, 4U),
				new FoleyParams(FoleyType.FOLEY_CHERRYBOMB, 0f, new int[]
				{
					Resources.SOUND_CHERRYBOMB
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_JALAPENO_IGNITE, 0f, new int[]
				{
					Resources.SOUND_JALAPENO
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_REVERSE_EXPLOSION, 0f, new int[]
				{
					Resources.SOUND_REVERSE_EXPLOSION
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_PLASTIC_HIT, 5f, new int[]
				{
					Resources.SOUND_PLASTICHIT,
					Resources.SOUND_PLASTICHIT2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_WINMUSIC, 0f, new int[]
				{
					Resources.SOUND_WINMUSIC
				}, 8U),
				new FoleyParams(FoleyType.FOLEY_BALLOONINFLATE, 10f, new int[]
				{
					Resources.SOUND_BALLOONINFLATE
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BIGCHOMP, -2f, new int[]
				{
					Resources.SOUND_BIGCHOMP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_MELONIMPACT, -5f, new int[]
				{
					Resources.SOUND_MELONIMPACT,
					Resources.SOUND_MELONIMPACT2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_PLANTGROW, -2f, new int[]
				{
					Resources.SOUND_PLANTGROW
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SHOOP, -5f, new int[]
				{
					Resources.SOUND_SHOOP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_JUICY, 2f, new int[]
				{
					Resources.SOUND_JUICY
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_NEWSPAPER_RARRGH, -2f, new int[]
				{
					Resources.SOUND_NEWSPAPER_RARRGH,
					Resources.SOUND_NEWSPAPER_RARRGH2,
					Resources.SOUND_NEWSPAPER_RARRGH2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_NEWSPAPER_RIP, -2f, new int[]
				{
					Resources.SOUND_NEWSPAPER_RIP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_FLOOP, 0f, new int[]
				{
					Resources.SOUND_FLOOP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_COFFEE, 0f, new int[]
				{
					Resources.SOUND_COFFEE
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_LOWGROAN, 2f, new int[]
				{
					Resources.SOUND_LOWGROAN,
					Resources.SOUND_LOWGROAN2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_PRIZE, 0f, new int[]
				{
					Resources.SOUND_PRIZE
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_YUCK, 1f, new int[]
				{
					Resources.SOUND_YUCK,
					Resources.SOUND_YUCK,
					Resources.SOUND_YUCK2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_UMBRELLA, 2f, new int[]
				{
					Resources.SOUND_THROW2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_GRASSSTEP, 2f, new int[]
				{
					Resources.SOUND_GRASSSTEP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SHOVEL, 5f, new int[]
				{
					Resources.SOUND_SHOVEL
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_COBLAUNCH, 10f, new int[]
				{
					Resources.SOUND_COBLAUNCH
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_WATERING, 10f, new int[]
				{
					Resources.SOUND_WATERING
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_POLEVAULT, 5f, new int[]
				{
					Resources.SOUND_POLEVAULT
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_GRAVESTONE_RUMBLE, 10f, new int[]
				{
					Resources.SOUND_GRAVESTONE_RUMBLE
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_DIRT_RISE, 5f, new int[]
				{
					Resources.SOUND_DIRT_RISE
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_FERTILIZER, 0f, new int[]
				{
					Resources.SOUND_FERTILIZER
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_PORTAL, 0f, new int[]
				{
					Resources.SOUND_PORTAL
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_WAKEUP, 0f, new int[]
				{
					Resources.SOUND_WAKEUP
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BUGSPRAY, 0f, new int[]
				{
					Resources.SOUND_BUGSPRAY
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_SCREAM, 0f, new int[]
				{
					Resources.SOUND_SCREAM
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_PAPER, 0f, new int[]
				{
					Resources.SOUND_PAPER
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_MONEYFALLS, 0f, new int[]
				{
					Resources.SOUND_MONEYFALLS
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_IMP, 5f, new int[]
				{
					Resources.SOUND_IMP,
					Resources.SOUND_IMP2
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_HYDRAULIC_SHORT, 3f, new int[]
				{
					Resources.SOUND_HYDRAULIC_SHORT
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_HYDRAULIC, 0f, new int[]
				{
					Resources.SOUND_HYDRAULIC
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_GARGANTUDEATH, 3f, new int[]
				{
					Resources.SOUND_GARGANTUDEATH
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_CERAMIC, 0f, new int[]
				{
					Resources.SOUND_CERAMIC
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_BOSSBOULDERATTACK, 0f, new int[]
				{
					Resources.SOUND_BOSSBOULDERATTACK
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_CHIME, 0f, new int[]
				{
					Resources.SOUND_CHIME
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_CRAZYDAVESHORT, 0f, new int[]
				{
					Resources.SOUND_CRAZYDAVESHORT1,
					Resources.SOUND_CRAZYDAVESHORT2,
					Resources.SOUND_CRAZYDAVESHORT3
				}, 16U),
				new FoleyParams(FoleyType.FOLEY_CRAZYDAVELONG, 0f, new int[]
				{
					Resources.SOUND_CRAZYDAVELONG1,
					Resources.SOUND_CRAZYDAVELONG2,
					Resources.SOUND_CRAZYDAVELONG3
				}, 16U),
				new FoleyParams(FoleyType.FOLEY_CRAZYDAVEEXTRALONG, 0f, new int[]
				{
					Resources.SOUND_CRAZYDAVEEXTRALONG1,
					Resources.SOUND_CRAZYDAVEEXTRALONG2,
					Resources.SOUND_CRAZYDAVEEXTRALONG3
				}, 16U),
				new FoleyParams(FoleyType.FOLEY_CRAZYDAVECRAZY, 0f, new int[]
				{
					Resources.SOUND_CRAZYDAVECRAZY
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_PHONOGRAPH, 0f, new int[]
				{
					Resources.SOUND_PHONOGRAPH
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_DANCER, 0f, new int[]
				{
					Resources.SOUND_DANCER
				}, 6U),
				new FoleyParams(FoleyType.FOLEY_FINALFANFARE, 0f, new int[]
				{
					Resources.SOUND_FINALFANFARE
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_CRAZYDAVESCREAM, 0f, new int[]
				{
					Resources.SOUND_CRAZYDAVESCREAM
				}, 0U),
				new FoleyParams(FoleyType.FOLEY_CRAZYDAVESCREAM2, 0f, new int[]
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
					for (int j = 0; j < 8; j++)
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
			for (int i = 0; i < 8; i++)
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
				for (int i = 0; i < 8; i++)
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
				for (int i = 0; i < 8; i++)
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

		public FoleyTypeData[] mFoleyTypeData = new FoleyTypeData[110];

		private int[] aVariationsArray = new int[10];

		public static FoleyParams[] gFoleyParamArray;

		public static int gFoleyParamArraySize;
	}
}
