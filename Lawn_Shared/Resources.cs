using System;
using Sexy;

internal static class Resources
{
	public static bool ExtractResourcesByName(ResourceManager theManager, string theName)
	{
		if (theName == "Init")
		{
			return Resources.ExtractInitResources(theManager);
		}
		if (theName == "InitRegistered")
		{
			return Resources.ExtractInitRegisteredResources(theManager);
		}
		if (theName == "LoaderBar")
		{
			return Resources.ExtractLoaderBarResources(theManager);
		}
		if (theName == "LoaderBarFont")
		{
			return Resources.ExtractLoaderBarFontResources(theManager);
		}
		if (theName == "LoadingFonts")
		{
			return Resources.ExtractLoadingFontsResources(theManager);
		}
		if (theName == "LoadingImages")
		{
			return Resources.ExtractLoadingImagesResources(theManager);
		}
		if (theName == "LoadingSounds")
		{
			return Resources.ExtractLoadingSoundsResources(theManager);
		}
		if (theName == "DelayLoad_Pile")
		{
			return Resources.ExtractDelayLoad_PileResources(theManager);
		}
		if (theName == "DelayLoad_GamePlay")
		{
			return Resources.ExtractDelayLoad_GamePlayResources(theManager);
		}
		if (theName == "DelayLoad_ZenGarden")
		{
			return Resources.ExtractDelayLoad_ZenGardenResources(theManager);
		}
		if (theName == "DelayLoad_Cached")
		{
			return Resources.ExtractDelayLoad_CachedResources(theManager);
		}
		if (theName == "DelayLoad_MainMenu")
		{
			return Resources.ExtractDelayLoad_MainMenuResources(theManager);
		}
		if (theName == "DelayLoad_Credits")
		{
			return Resources.ExtractDelayLoad_CreditsResources(theManager);
		}
		if (theName == "DelayLoad_Leaderboard_Background")
		{
			return Resources.ExtractDelayLoad_Leaderboard_BackgroundResources(theManager);
		}
		if (theName == "DelayLoad_Leaderboard")
		{
			return Resources.ExtractDelayLoad_LeaderboardResources(theManager);
		}
		if (theName == "DelayLoad_Stars")
		{
			return Resources.ExtractDelayLoad_StarsResources(theManager);
		}
		if (theName == "DelayLoad_GreenHouseGarden")
		{
			return Resources.ExtractDelayLoad_GreenHouseGardenResources(theManager);
		}
		if (theName == "DelayLoad_Zombiquarium")
		{
			return Resources.ExtractDelayLoad_ZombiquariumResources(theManager);
		}
		if (theName == "DelayLoad_MushroomGarden")
		{
			return Resources.ExtractDelayLoad_MushroomGardenResources(theManager);
		}
		if (theName == "DelayLoad_Background1")
		{
			return Resources.ExtractDelayLoad_Background1Resources(theManager);
		}
		if (theName == "DelayLoad_BackgroundUnsodded")
		{
			return Resources.ExtractDelayLoad_BackgroundUnsoddedResources(theManager);
		}
		if (theName == "DelayLoad_Background2")
		{
			return Resources.ExtractDelayLoad_Background2Resources(theManager);
		}
		if (theName == "DelayLoad_Background3")
		{
			return Resources.ExtractDelayLoad_Background3Resources(theManager);
		}
		if (theName == "DelayLoad_Background4")
		{
			return Resources.ExtractDelayLoad_Background4Resources(theManager);
		}
		if (theName == "DelayLoad_Background5")
		{
			return Resources.ExtractDelayLoad_Background5Resources(theManager);
		}
		if (theName == "DelayLoad_Background6")
		{
			return Resources.ExtractDelayLoad_Background6Resources(theManager);
		}
		if (theName == "DelayLoad_Almanac")
		{
			return Resources.ExtractDelayLoad_AlmanacResources(theManager);
		}
		if (theName == "DelayLoad_Store")
		{
			return Resources.ExtractDelayLoad_StoreResources(theManager);
		}
		if (theName == "DelayLoad_ZombieNote")
		{
			return Resources.ExtractDelayLoad_ZombieNoteResources(theManager);
		}
		if (theName == "DelayLoad_ZombieNote1")
		{
			return Resources.ExtractDelayLoad_ZombieNote1Resources(theManager);
		}
		if (theName == "DelayLoad_ZombieNote2")
		{
			return Resources.ExtractDelayLoad_ZombieNote2Resources(theManager);
		}
		if (theName == "DelayLoad_ZombieNote3")
		{
			return Resources.ExtractDelayLoad_ZombieNote3Resources(theManager);
		}
		if (theName == "DelayLoad_ZombieNote4")
		{
			return Resources.ExtractDelayLoad_ZombieNote4Resources(theManager);
		}
		if (theName == "DelayLoad_ZombieFinalNote")
		{
			return Resources.ExtractDelayLoad_ZombieFinalNoteResources(theManager);
		}
		return theName == "DelayLoad_ZombieNoteHelp" && Resources.ExtractDelayLoad_ZombieNoteHelpResources(theManager);
	}

	internal static void ExtractResources(ResourceManager theManager, AtlasResources theRes)
	{
		Resources.ExtractInitResources(theManager);
		Resources.ExtractInitRegisteredResources(theManager);
		Resources.ExtractLoaderBarResources(theManager);
		Resources.ExtractLoaderBarFontResources(theManager);
		Resources.ExtractLoadingFontsResources(theManager);
		Resources.ExtractLoadingImagesResources(theManager);
		Resources.ExtractLoadingSoundsResources(theManager);
		Resources.ExtractDelayLoad_PileResources(theManager);
		Resources.ExtractDelayLoad_GamePlayResources(theManager);
		Resources.ExtractDelayLoad_ZenGardenResources(theManager);
		Resources.ExtractDelayLoad_CachedResources(theManager);
		Resources.ExtractDelayLoad_MainMenuResources(theManager);
		Resources.ExtractDelayLoad_CreditsResources(theManager);
		Resources.ExtractDelayLoad_Leaderboard_BackgroundResources(theManager);
		Resources.ExtractDelayLoad_LeaderboardResources(theManager);
		Resources.ExtractDelayLoad_StarsResources(theManager);
		Resources.ExtractDelayLoad_GreenHouseGardenResources(theManager);
		Resources.ExtractDelayLoad_ZombiquariumResources(theManager);
		Resources.ExtractDelayLoad_MushroomGardenResources(theManager);
		Resources.ExtractDelayLoad_Background1Resources(theManager);
		Resources.ExtractDelayLoad_BackgroundUnsoddedResources(theManager);
		Resources.ExtractDelayLoad_Background2Resources(theManager);
		Resources.ExtractDelayLoad_Background3Resources(theManager);
		Resources.ExtractDelayLoad_Background4Resources(theManager);
		Resources.ExtractDelayLoad_Background5Resources(theManager);
		Resources.ExtractDelayLoad_Background6Resources(theManager);
		Resources.ExtractDelayLoad_AlmanacResources(theManager);
		Resources.ExtractDelayLoad_StoreResources(theManager);
		Resources.ExtractDelayLoad_ZombieNoteResources(theManager);
		Resources.ExtractDelayLoad_ZombieNote1Resources(theManager);
		Resources.ExtractDelayLoad_ZombieNote2Resources(theManager);
		Resources.ExtractDelayLoad_ZombieNote3Resources(theManager);
		Resources.ExtractDelayLoad_ZombieNote4Resources(theManager);
		Resources.ExtractDelayLoad_ZombieFinalNoteResources(theManager);
		Resources.ExtractDelayLoad_ZombieNoteHelpResources(theManager);
		theRes.ExtractResources();
	}

	public static bool ExtractInitResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_POPCAP_LOGO = theManager.GetImageThrow("IMAGE_POPCAP_LOGO");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractInitRegisteredResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_POPCAP_LOGO_REGISTERED = theManager.GetImageThrow("IMAGE_POPCAP_LOGO_REGISTERED");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractLoaderBarResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_TITLESCREEN = theManager.GetImageThrow("IMAGE_TITLESCREEN");
			Resources.IMAGE_LOADING = theManager.GetImageThrow("IMAGE_LOADING");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractLoaderBarFontResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_PVZ_LOGO = theManager.GetImageThrow("IMAGE_PVZ_LOGO");
			Resources.SOUND_BUTTONCLICK = theManager.GetSoundThrow("SOUND_BUTTONCLICK");
			Resources.SOUND_LOADINGBAR_FLOWER = theManager.GetSoundThrow("SOUND_LOADINGBAR_FLOWER");
			Resources.SOUND_LOADINGBAR_ZOMBIE = theManager.GetSoundThrow("SOUND_LOADINGBAR_ZOMBIE");
			Resources.FONT_BRIANNETOD16 = theManager.GetFontThrow("FONT_BRIANNETOD16");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractLoadingFontsResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.FONT_HOUSEOFTERROR16 = theManager.GetFontThrow("FONT_HOUSEOFTERROR16");
			Resources.FONT_CONTINUUMBOLD14 = theManager.GetFontThrow("FONT_CONTINUUMBOLD14");
			Resources.FONT_CONTINUUMBOLD14OUTLINE = theManager.GetFontThrow("FONT_CONTINUUMBOLD14OUTLINE");
			Resources.FONT_DWARVENTODCRAFT12 = theManager.GetFontThrow("FONT_DWARVENTODCRAFT12");
			Resources.FONT_DWARVENTODCRAFT15 = theManager.GetFontThrow("FONT_DWARVENTODCRAFT15");
			Resources.FONT_DWARVENTODCRAFT18 = theManager.GetFontThrow("FONT_DWARVENTODCRAFT18");
			Resources.FONT_PICO129 = theManager.GetFontThrow("FONT_PICO129");
			Resources.FONT_BRIANNETOD12 = theManager.GetFontThrow("FONT_BRIANNETOD12");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractLoadingImagesResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_CHARREDZOMBIES = theManager.GetImageThrow("IMAGE_CHARREDZOMBIES");
			Resources.IMAGE_ALMANACUI = theManager.GetImageThrow("IMAGE_ALMANACUI");
			Resources.IMAGE_SEEDATLAS = theManager.GetImageThrow("IMAGE_SEEDATLAS");
			Resources.IMAGE_DAVE = theManager.GetImageThrow("IMAGE_DAVE");
			Resources.IMAGE_DIALOG = theManager.GetImageThrow("IMAGE_DIALOG");
			Resources.IMAGE_CONVEYORBELT_BACKDROP = theManager.GetImageThrow("IMAGE_CONVEYORBELT_BACKDROP");
			Resources.IMAGE_CONVEYORBELT_BELT = theManager.GetImageThrow("IMAGE_CONVEYORBELT_BELT");
			Resources.IMAGE_SPEECHBUBBLE = theManager.GetImageThrow("IMAGE_SPEECHBUBBLE");
			Resources.IMAGE_LOC_EN = theManager.GetImageThrow("IMAGE_LOC_EN");
			Resources.IMAGE_ZOMBIE_NOTE_SMALL = theManager.GetImageThrow("IMAGE_ZOMBIE_NOTE_SMALL");
			Resources.IMAGE_REANIM_ZOMBIESWON = theManager.GetImageThrow("IMAGE_REANIM_ZOMBIESWON");
			Resources.IMAGE_SCARY_POT = theManager.GetImageThrow("IMAGE_SCARY_POT");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractLoadingSoundsResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.SOUND_AWOOGA = theManager.GetSoundThrow("SOUND_AWOOGA");
			Resources.SOUND_BLEEP = theManager.GetSoundThrow("SOUND_BLEEP");
			Resources.SOUND_BUZZER = theManager.GetSoundThrow("SOUND_BUZZER");
			Resources.SOUND_CHOMP = theManager.GetSoundThrow("SOUND_CHOMP");
			Resources.SOUND_CHOMP2 = theManager.GetSoundThrow("SOUND_CHOMP2");
			Resources.SOUND_CHOMPSOFT = theManager.GetSoundThrow("SOUND_CHOMPSOFT");
			Resources.SOUND_FLOOP = theManager.GetSoundThrow("SOUND_FLOOP");
			Resources.SOUND_FROZEN = theManager.GetSoundThrow("SOUND_FROZEN");
			Resources.SOUND_GULP = theManager.GetSoundThrow("SOUND_GULP");
			Resources.SOUND_GROAN = theManager.GetSoundThrow("SOUND_GROAN");
			Resources.SOUND_GROAN2 = theManager.GetSoundThrow("SOUND_GROAN2");
			Resources.SOUND_GROAN3 = theManager.GetSoundThrow("SOUND_GROAN3");
			Resources.SOUND_GROAN4 = theManager.GetSoundThrow("SOUND_GROAN4");
			Resources.SOUND_GROAN5 = theManager.GetSoundThrow("SOUND_GROAN5");
			Resources.SOUND_GROAN6 = theManager.GetSoundThrow("SOUND_GROAN6");
			Resources.SOUND_LOSEMUSIC = theManager.GetSoundThrow("SOUND_LOSEMUSIC");
			Resources.SOUND_MINDCONTROLLED = theManager.GetSoundThrow("SOUND_MINDCONTROLLED");
			Resources.SOUND_PAUSE = theManager.GetSoundThrow("SOUND_PAUSE");
			Resources.SOUND_PLANT = theManager.GetSoundThrow("SOUND_PLANT");
			Resources.SOUND_PLANT2 = theManager.GetSoundThrow("SOUND_PLANT2");
			Resources.SOUND_POINTS = theManager.GetSoundThrow("SOUND_POINTS");
			Resources.SOUND_SEEDLIFT = theManager.GetSoundThrow("SOUND_SEEDLIFT");
			Resources.SOUND_SIREN = theManager.GetSoundThrow("SOUND_SIREN");
			Resources.SOUND_SLURP = theManager.GetSoundThrow("SOUND_SLURP");
			Resources.SOUND_SPLAT = theManager.GetSoundThrow("SOUND_SPLAT");
			Resources.SOUND_SPLAT2 = theManager.GetSoundThrow("SOUND_SPLAT2");
			Resources.SOUND_SPLAT3 = theManager.GetSoundThrow("SOUND_SPLAT3");
			Resources.SOUND_SUKHBIR4 = theManager.GetSoundThrow("SOUND_SUKHBIR4");
			Resources.SOUND_SUKHBIR5 = theManager.GetSoundThrow("SOUND_SUKHBIR5");
			Resources.SOUND_SUKHBIR6 = theManager.GetSoundThrow("SOUND_SUKHBIR6");
			Resources.SOUND_TAP = theManager.GetSoundThrow("SOUND_TAP");
			Resources.SOUND_TAP2 = theManager.GetSoundThrow("SOUND_TAP2");
			Resources.SOUND_THROW = theManager.GetSoundThrow("SOUND_THROW");
			Resources.SOUND_THROW2 = theManager.GetSoundThrow("SOUND_THROW2");
			Resources.SOUND_BLOVER = theManager.GetSoundThrow("SOUND_BLOVER");
			Resources.SOUND_WINMUSIC = theManager.GetSoundThrow("SOUND_WINMUSIC");
			Resources.SOUND_LAWNMOWER = theManager.GetSoundThrow("SOUND_LAWNMOWER");
			Resources.SOUND_BOING = theManager.GetSoundThrow("SOUND_BOING");
			Resources.SOUND_JACKINTHEBOX = theManager.GetSoundThrow("SOUND_JACKINTHEBOX");
			Resources.SOUND_DIAMOND = theManager.GetSoundThrow("SOUND_DIAMOND");
			Resources.SOUND_DOLPHIN_APPEARS = theManager.GetSoundThrow("SOUND_DOLPHIN_APPEARS");
			Resources.SOUND_DOLPHIN_BEFORE_JUMPING = theManager.GetSoundThrow("SOUND_DOLPHIN_BEFORE_JUMPING");
			Resources.SOUND_POTATO_MINE = theManager.GetSoundThrow("SOUND_POTATO_MINE");
			Resources.SOUND_ZAMBONI = theManager.GetSoundThrow("SOUND_ZAMBONI");
			Resources.SOUND_BALLOON_POP = theManager.GetSoundThrow("SOUND_BALLOON_POP");
			Resources.SOUND_THUNDER = theManager.GetSoundThrow("SOUND_THUNDER");
			Resources.SOUND_ZOMBIESPLASH = theManager.GetSoundThrow("SOUND_ZOMBIESPLASH");
			Resources.SOUND_BOWLING = theManager.GetSoundThrow("SOUND_BOWLING");
			Resources.SOUND_BOWLINGIMPACT = theManager.GetSoundThrow("SOUND_BOWLINGIMPACT");
			Resources.SOUND_BOWLINGIMPACT2 = theManager.GetSoundThrow("SOUND_BOWLINGIMPACT2");
			Resources.SOUND_GRAVEBUSTERCHOMP = theManager.GetSoundThrow("SOUND_GRAVEBUSTERCHOMP");
			Resources.SOUND_GRAVEBUTTON = theManager.GetSoundThrow("SOUND_GRAVEBUTTON");
			Resources.SOUND_LIMBS_POP = theManager.GetSoundThrow("SOUND_LIMBS_POP");
			Resources.SOUND_PLANTERN = theManager.GetSoundThrow("SOUND_PLANTERN");
			Resources.SOUND_POGO_ZOMBIE = theManager.GetSoundThrow("SOUND_POGO_ZOMBIE");
			Resources.SOUND_SNOW_PEA_SPARKLES = theManager.GetSoundThrow("SOUND_SNOW_PEA_SPARKLES");
			Resources.SOUND_PLANT_WATER = theManager.GetSoundThrow("SOUND_PLANT_WATER");
			Resources.SOUND_ZOMBIE_ENTERING_WATER = theManager.GetSoundThrow("SOUND_ZOMBIE_ENTERING_WATER");
			Resources.SOUND_ZOMBIE_FALLING_1 = theManager.GetSoundThrow("SOUND_ZOMBIE_FALLING_1");
			Resources.SOUND_ZOMBIE_FALLING_2 = theManager.GetSoundThrow("SOUND_ZOMBIE_FALLING_2");
			Resources.SOUND_PUFF = theManager.GetSoundThrow("SOUND_PUFF");
			Resources.SOUND_FUME = theManager.GetSoundThrow("SOUND_FUME");
			Resources.SOUND_HUGE_WAVE = theManager.GetSoundThrow("SOUND_HUGE_WAVE");
			Resources.SOUND_SLOT_MACHINE = theManager.GetSoundThrow("SOUND_SLOT_MACHINE");
			Resources.SOUND_COIN = theManager.GetSoundThrow("SOUND_COIN");
			Resources.SOUND_ROLL_IN = theManager.GetSoundThrow("SOUND_ROLL_IN");
			Resources.SOUND_DIGGER_ZOMBIE = theManager.GetSoundThrow("SOUND_DIGGER_ZOMBIE");
			Resources.SOUND_HATCHBACK_CLOSE = theManager.GetSoundThrow("SOUND_HATCHBACK_CLOSE");
			Resources.SOUND_HATCHBACK_OPEN = theManager.GetSoundThrow("SOUND_HATCHBACK_OPEN");
			Resources.SOUND_KERNELPULT = theManager.GetSoundThrow("SOUND_KERNELPULT");
			Resources.SOUND_KERNELPULT2 = theManager.GetSoundThrow("SOUND_KERNELPULT2");
			Resources.SOUND_ZOMBAQUARIUM_DIE = theManager.GetSoundThrow("SOUND_ZOMBAQUARIUM_DIE");
			Resources.SOUND_BUNGEE_SCREAM = theManager.GetSoundThrow("SOUND_BUNGEE_SCREAM");
			Resources.SOUND_BUNGEE_SCREAM2 = theManager.GetSoundThrow("SOUND_BUNGEE_SCREAM2");
			Resources.SOUND_BUNGEE_SCREAM3 = theManager.GetSoundThrow("SOUND_BUNGEE_SCREAM3");
			Resources.SOUND_BUTTER = theManager.GetSoundThrow("SOUND_BUTTER");
			Resources.SOUND_JACK_SURPRISE = theManager.GetSoundThrow("SOUND_JACK_SURPRISE");
			Resources.SOUND_JACK_SURPRISE2 = theManager.GetSoundThrow("SOUND_JACK_SURPRISE2");
			Resources.SOUND_NEWSPAPER_RARRGH = theManager.GetSoundThrow("SOUND_NEWSPAPER_RARRGH");
			Resources.SOUND_NEWSPAPER_RARRGH2 = theManager.GetSoundThrow("SOUND_NEWSPAPER_RARRGH2");
			Resources.SOUND_NEWSPAPER_RIP = theManager.GetSoundThrow("SOUND_NEWSPAPER_RIP");
			Resources.SOUND_SQUASH_HMM = theManager.GetSoundThrow("SOUND_SQUASH_HMM");
			Resources.SOUND_SQUASH_HMM2 = theManager.GetSoundThrow("SOUND_SQUASH_HMM2");
			Resources.SOUND_VASE_BREAKING = theManager.GetSoundThrow("SOUND_VASE_BREAKING");
			Resources.SOUND_POOL_CLEANER = theManager.GetSoundThrow("SOUND_POOL_CLEANER");
			Resources.SOUND_MAGNETSHROOM = theManager.GetSoundThrow("SOUND_MAGNETSHROOM");
			Resources.SOUND_LADDER_ZOMBIE = theManager.GetSoundThrow("SOUND_LADDER_ZOMBIE");
			Resources.SOUND_GARGANTUAR_THUMP = theManager.GetSoundThrow("SOUND_GARGANTUAR_THUMP");
			Resources.SOUND_BASKETBALL = theManager.GetSoundThrow("SOUND_BASKETBALL");
			Resources.SOUND_FIREPEA = theManager.GetSoundThrow("SOUND_FIREPEA");
			Resources.SOUND_IGNITE = theManager.GetSoundThrow("SOUND_IGNITE");
			Resources.SOUND_IGNITE2 = theManager.GetSoundThrow("SOUND_IGNITE2");
			Resources.SOUND_READYSETPLANT = theManager.GetSoundThrow("SOUND_READYSETPLANT");
			Resources.SOUND_DOOMSHROOM = theManager.GetSoundThrow("SOUND_DOOMSHROOM");
			Resources.SOUND_EXPLOSION = theManager.GetSoundThrow("SOUND_EXPLOSION");
			Resources.SOUND_FINALWAVE = theManager.GetSoundThrow("SOUND_FINALWAVE");
			Resources.SOUND_REVERSE_EXPLOSION = theManager.GetSoundThrow("SOUND_REVERSE_EXPLOSION");
			Resources.SOUND_RVTHROW = theManager.GetSoundThrow("SOUND_RVTHROW");
			Resources.SOUND_SHIELDHIT = theManager.GetSoundThrow("SOUND_SHIELDHIT");
			Resources.SOUND_SHIELDHIT2 = theManager.GetSoundThrow("SOUND_SHIELDHIT2");
			Resources.SOUND_BOSSEXPLOSION = theManager.GetSoundThrow("SOUND_BOSSEXPLOSION");
			Resources.SOUND_CHERRYBOMB = theManager.GetSoundThrow("SOUND_CHERRYBOMB");
			Resources.SOUND_BONK = theManager.GetSoundThrow("SOUND_BONK");
			Resources.SOUND_SWING = theManager.GetSoundThrow("SOUND_SWING");
			Resources.SOUND_RAIN = theManager.GetSoundThrow("SOUND_RAIN");
			Resources.SOUND_LIGHTFILL = theManager.GetSoundThrow("SOUND_LIGHTFILL");
			Resources.SOUND_PLASTICHIT = theManager.GetSoundThrow("SOUND_PLASTICHIT");
			Resources.SOUND_PLASTICHIT2 = theManager.GetSoundThrow("SOUND_PLASTICHIT2");
			Resources.SOUND_JALAPENO = theManager.GetSoundThrow("SOUND_JALAPENO");
			Resources.SOUND_BALLOONINFLATE = theManager.GetSoundThrow("SOUND_BALLOONINFLATE");
			Resources.SOUND_BIGCHOMP = theManager.GetSoundThrow("SOUND_BIGCHOMP");
			Resources.SOUND_MELONIMPACT = theManager.GetSoundThrow("SOUND_MELONIMPACT");
			Resources.SOUND_MELONIMPACT2 = theManager.GetSoundThrow("SOUND_MELONIMPACT2");
			Resources.SOUND_PLANTGROW = theManager.GetSoundThrow("SOUND_PLANTGROW");
			Resources.SOUND_SHOOP = theManager.GetSoundThrow("SOUND_SHOOP");
			Resources.SOUND_JUICY = theManager.GetSoundThrow("SOUND_JUICY");
			Resources.SOUND_COFFEE = theManager.GetSoundThrow("SOUND_COFFEE");
			Resources.SOUND_WAKEUP = theManager.GetSoundThrow("SOUND_WAKEUP");
			Resources.SOUND_LOWGROAN = theManager.GetSoundThrow("SOUND_LOWGROAN");
			Resources.SOUND_LOWGROAN2 = theManager.GetSoundThrow("SOUND_LOWGROAN2");
			Resources.SOUND_PRIZE = theManager.GetSoundThrow("SOUND_PRIZE");
			Resources.SOUND_YUCK = theManager.GetSoundThrow("SOUND_YUCK");
			Resources.SOUND_YUCK2 = theManager.GetSoundThrow("SOUND_YUCK2");
			Resources.SOUND_GRASSSTEP = theManager.GetSoundThrow("SOUND_GRASSSTEP");
			Resources.SOUND_SHOVEL = theManager.GetSoundThrow("SOUND_SHOVEL");
			Resources.SOUND_COBLAUNCH = theManager.GetSoundThrow("SOUND_COBLAUNCH");
			Resources.SOUND_WATERING = theManager.GetSoundThrow("SOUND_WATERING");
			Resources.SOUND_POLEVAULT = theManager.GetSoundThrow("SOUND_POLEVAULT");
			Resources.SOUND_GRAVESTONE_RUMBLE = theManager.GetSoundThrow("SOUND_GRAVESTONE_RUMBLE");
			Resources.SOUND_DIRT_RISE = theManager.GetSoundThrow("SOUND_DIRT_RISE");
			Resources.SOUND_FERTILIZER = theManager.GetSoundThrow("SOUND_FERTILIZER");
			Resources.SOUND_PORTAL = theManager.GetSoundThrow("SOUND_PORTAL");
			Resources.SOUND_SCREAM = theManager.GetSoundThrow("SOUND_SCREAM");
			Resources.SOUND_PAPER = theManager.GetSoundThrow("SOUND_PAPER");
			Resources.SOUND_MONEYFALLS = theManager.GetSoundThrow("SOUND_MONEYFALLS");
			Resources.SOUND_IMP = theManager.GetSoundThrow("SOUND_IMP");
			Resources.SOUND_IMP2 = theManager.GetSoundThrow("SOUND_IMP2");
			Resources.SOUND_HYDRAULIC_SHORT = theManager.GetSoundThrow("SOUND_HYDRAULIC_SHORT");
			Resources.SOUND_HYDRAULIC = theManager.GetSoundThrow("SOUND_HYDRAULIC");
			Resources.SOUND_GARGANTUDEATH = theManager.GetSoundThrow("SOUND_GARGANTUDEATH");
			Resources.SOUND_CERAMIC = theManager.GetSoundThrow("SOUND_CERAMIC");
			Resources.SOUND_BOSSBOULDERATTACK = theManager.GetSoundThrow("SOUND_BOSSBOULDERATTACK");
			Resources.SOUND_CHIME = theManager.GetSoundThrow("SOUND_CHIME");
			Resources.SOUND_CRAZYDAVESHORT1 = theManager.GetSoundThrow("SOUND_CRAZYDAVESHORT1");
			Resources.SOUND_CRAZYDAVESHORT2 = theManager.GetSoundThrow("SOUND_CRAZYDAVESHORT2");
			Resources.SOUND_CRAZYDAVESHORT3 = theManager.GetSoundThrow("SOUND_CRAZYDAVESHORT3");
			Resources.SOUND_CRAZYDAVELONG1 = theManager.GetSoundThrow("SOUND_CRAZYDAVELONG1");
			Resources.SOUND_CRAZYDAVELONG2 = theManager.GetSoundThrow("SOUND_CRAZYDAVELONG2");
			Resources.SOUND_CRAZYDAVELONG3 = theManager.GetSoundThrow("SOUND_CRAZYDAVELONG3");
			Resources.SOUND_CRAZYDAVEEXTRALONG1 = theManager.GetSoundThrow("SOUND_CRAZYDAVEEXTRALONG1");
			Resources.SOUND_CRAZYDAVEEXTRALONG2 = theManager.GetSoundThrow("SOUND_CRAZYDAVEEXTRALONG2");
			Resources.SOUND_CRAZYDAVEEXTRALONG3 = theManager.GetSoundThrow("SOUND_CRAZYDAVEEXTRALONG3");
			Resources.SOUND_CRAZYDAVECRAZY = theManager.GetSoundThrow("SOUND_CRAZYDAVECRAZY");
			Resources.SOUND_DANCER = theManager.GetSoundThrow("SOUND_DANCER");
			Resources.SOUND_FINALFANFARE = theManager.GetSoundThrow("SOUND_FINALFANFARE");
			Resources.SOUND_CRAZYDAVESCREAM = theManager.GetSoundThrow("SOUND_CRAZYDAVESCREAM");
			Resources.SOUND_CRAZYDAVESCREAM2 = theManager.GetSoundThrow("SOUND_CRAZYDAVESCREAM2");
			Resources.SOUND_ACHIEVEMENT = theManager.GetSoundThrow("SOUND_ACHIEVEMENT");
			Resources.SOUND_BUGSPRAY = theManager.GetSoundThrow("SOUND_BUGSPRAY");
			Resources.SOUND_FERTILISER = theManager.GetSoundThrow("SOUND_FERTILISER");
			Resources.SOUND_PHONOGRAPH = theManager.GetSoundThrow("SOUND_PHONOGRAPH");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_PileResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_PILE = theManager.GetImageThrow("IMAGE_PILE");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_GamePlayResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_PLANTSZOMBIES = theManager.GetImageThrow("IMAGE_PLANTSZOMBIES");
			Resources.IMAGE_PARTICLES = theManager.GetImageThrow("IMAGE_PARTICLES");
			Resources.IMAGE_SLOTMACHINE_OVERLAY = theManager.GetImageThrow("IMAGE_SLOTMACHINE_OVERLAY");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_ZenGardenResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_ZENGARDEN = theManager.GetImageThrow("IMAGE_ZENGARDEN");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_CachedResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_CACHED = theManager.GetImageThrow("IMAGE_CACHED");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_MainMenuResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND = theManager.GetImageThrow("IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND");
			Resources.IMAGE_SELECTORSCREEN_MAIN_BACKGROUND = theManager.GetImageThrow("IMAGE_SELECTORSCREEN_MAIN_BACKGROUND");
			Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE = theManager.GetImageThrow("IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE");
			Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA = theManager.GetImageThrow("IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA");
			Resources.IMAGE_SELECTORSCREEN_QUICKPLAY_BACKGROUND = theManager.GetImageThrow("IMAGE_SELECTORSCREEN_QUICKPLAY_BACKGROUND");
			Resources.IMAGE_GOODIES = theManager.GetImageThrow("IMAGE_GOODIES");
			Resources.IMAGE_QUICKPLAY = theManager.GetImageThrow("IMAGE_QUICKPLAY");
			Resources.IMAGE_ACHIEVEMENT_GNOME = theManager.GetImageThrow("IMAGE_ACHIEVEMENT_GNOME");
			Resources.IMAGE_MINIGAMES = theManager.GetImageThrow("IMAGE_MINIGAMES");
			Resources.IMAGE_SELECTORSCREEN_MOREGAMES_BACKGROUND = theManager.GetImageThrow("IMAGE_SELECTORSCREEN_MOREGAMES_BACKGROUND");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_CreditsResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_CREDITS_ZOMBIENOTE = theManager.GetImageThrow("IMAGE_CREDITS_ZOMBIENOTE");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_Leaderboard_BackgroundResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_LEADERBOARDSCREEN_BACKGROUND = theManager.GetImageThrow("IMAGE_LEADERBOARDSCREEN_BACKGROUND");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_LeaderboardResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BLACKHOLE = theManager.GetImageThrow("IMAGE_BLACKHOLE");
			Resources.IMAGE_EDGE_OF_SPACE = theManager.GetImageThrow("IMAGE_EDGE_OF_SPACE");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_StarsResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_STARS_1 = theManager.GetImageThrow("IMAGE_STARS_1");
			Resources.IMAGE_STARS_2 = theManager.GetImageThrow("IMAGE_STARS_2");
			Resources.IMAGE_STARS_3 = theManager.GetImageThrow("IMAGE_STARS_3");
			Resources.IMAGE_STARS_4 = theManager.GetImageThrow("IMAGE_STARS_4");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_GreenHouseGardenResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BACKGROUND_GREENHOUSE = theManager.GetImageThrow("IMAGE_BACKGROUND_GREENHOUSE");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_ZombiquariumResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_AQUARIUM1 = theManager.GetImageThrow("IMAGE_AQUARIUM1");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_MushroomGardenResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BACKGROUND_MUSHROOMGARDEN = theManager.GetImageThrow("IMAGE_BACKGROUND_MUSHROOMGARDEN");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_Background1Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BACKGROUND1 = theManager.GetImageThrow("IMAGE_BACKGROUND1");
			Resources.IMAGE_BACKGROUND1_GAMEOVER_INTERIOR_OVERLAY = theManager.GetImageThrow("IMAGE_BACKGROUND1_GAMEOVER_INTERIOR_OVERLAY");
			Resources.IMAGE_BACKGROUND1_GAMEOVER_MASK = theManager.GetImageThrow("IMAGE_BACKGROUND1_GAMEOVER_MASK");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_BackgroundUnsoddedResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BACKGROUND1UNSODDED = theManager.GetImageThrow("IMAGE_BACKGROUND1UNSODDED");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_Background2Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BACKGROUND2 = theManager.GetImageThrow("IMAGE_BACKGROUND2");
			Resources.IMAGE_BACKGROUND2_GAMEOVER_INTERIOR_OVERLAY = theManager.GetImageThrow("IMAGE_BACKGROUND2_GAMEOVER_INTERIOR_OVERLAY");
			Resources.IMAGE_BACKGROUND2_GAMEOVER_MASK = theManager.GetImageThrow("IMAGE_BACKGROUND2_GAMEOVER_MASK");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_Background3Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BACKGROUND3 = theManager.GetImageThrow("IMAGE_BACKGROUND3");
			Resources.IMAGE_BACKGROUND3_GAMEOVER_INTERIOR_OVERLAY = theManager.GetImageThrow("IMAGE_BACKGROUND3_GAMEOVER_INTERIOR_OVERLAY");
			Resources.IMAGE_BACKGROUND3_GAMEOVER_MASK = theManager.GetImageThrow("IMAGE_BACKGROUND3_GAMEOVER_MASK");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_Background4Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BACKGROUND4 = theManager.GetImageThrow("IMAGE_BACKGROUND4");
			Resources.IMAGE_BACKGROUND4_GAMEOVER_INTERIOR_OVERLAY = theManager.GetImageThrow("IMAGE_BACKGROUND4_GAMEOVER_INTERIOR_OVERLAY");
			Resources.IMAGE_BACKGROUND4_GAMEOVER_MASK = theManager.GetImageThrow("IMAGE_BACKGROUND4_GAMEOVER_MASK");
			Resources.IMAGE_FOG = theManager.GetImageThrow("IMAGE_FOG");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_Background5Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BACKGROUND5 = theManager.GetImageThrow("IMAGE_BACKGROUND5");
			Resources.IMAGE_BACKGROUND5_GAMEOVER_MASK = theManager.GetImageThrow("IMAGE_BACKGROUND5_GAMEOVER_MASK");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_Background6Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_BACKGROUND6BOSS = theManager.GetImageThrow("IMAGE_BACKGROUND6BOSS");
			Resources.IMAGE_BACKGROUND6_GAMEOVER_MASK = theManager.GetImageThrow("IMAGE_BACKGROUND6_GAMEOVER_MASK");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_AlmanacResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		return true;
	}

	public static bool ExtractDelayLoad_StoreResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_STORE_BACKGROUND = theManager.GetImageThrow("IMAGE_STORE_BACKGROUND");
			Resources.IMAGE_STORE_BACKGROUNDNIGHT = theManager.GetImageThrow("IMAGE_STORE_BACKGROUNDNIGHT");
			Resources.IMAGE_STORE_CAR = theManager.GetImageThrow("IMAGE_STORE_CAR");
			Resources.IMAGE_STORE_CAR_NIGHT = theManager.GetImageThrow("IMAGE_STORE_CAR_NIGHT");
			Resources.IMAGE_STORE_CARCLOSED = theManager.GetImageThrow("IMAGE_STORE_CARCLOSED");
			Resources.IMAGE_STORE_CARCLOSED_NIGHT = theManager.GetImageThrow("IMAGE_STORE_CARCLOSED_NIGHT");
			Resources.IMAGE_STORE_HATCHBACKOPEN = theManager.GetImageThrow("IMAGE_STORE_HATCHBACKOPEN");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_ZombieNoteResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_ZOMBIE_NOTE = theManager.GetImageThrow("IMAGE_ZOMBIE_NOTE");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_ZombieNote1Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_ZOMBIE_NOTE1 = theManager.GetImageThrow("IMAGE_ZOMBIE_NOTE1");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_ZombieNote2Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_ZOMBIE_NOTE2 = theManager.GetImageThrow("IMAGE_ZOMBIE_NOTE2");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_ZombieNote3Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_ZOMBIE_NOTE3 = theManager.GetImageThrow("IMAGE_ZOMBIE_NOTE3");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_ZombieNote4Resources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_ZOMBIE_NOTE4 = theManager.GetImageThrow("IMAGE_ZOMBIE_NOTE4");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_ZombieFinalNoteResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_ZOMBIE_FINAL_NOTE = theManager.GetImageThrow("IMAGE_ZOMBIE_FINAL_NOTE");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static bool ExtractDelayLoad_ZombieNoteHelpResources(ResourceManager theManager)
	{
		Resources.gNeedRecalcVariableToIdMap = true;
		try
		{
			Resources.IMAGE_ZOMBIE_NOTE_HELP = theManager.GetImageThrow("IMAGE_ZOMBIE_NOTE_HELP");
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static Image GetImageById(int theId)
	{
		return (Image)Resources.gResources[theId];
	}

	public static Font GetFontById(int theId)
	{
		return (Font)Resources.gResources[theId];
	}

	public static int GetSoundById(int theId)
	{
		return (int)Resources.gResources[theId];
	}

	public static Image GetImageRefById(int theId)
	{
		return (Image)Resources.gResources[theId];
	}

	public static Font GetFontRefById(int theId)
	{
		return (Font)Resources.gResources[theId];
	}

	public static int GetSoundRefById(int theId)
	{
		return (int)Resources.gResources[theId];
	}

	public static Resources.ResourceId GetIdByImage(Image theImage)
	{
		return Resources.GetIdByVariable(theImage);
	}

	public static Resources.ResourceId GetIdByFont(Font theFont)
	{
		return Resources.GetIdByVariable(theFont);
	}

	public static Resources.ResourceId GetIdBySound(int theSound)
	{
		return Resources.GetIdByVariable((IntPtr)theSound);
	}

	public static Resources.ResourceId GetIdByStringId(string theStringId)
	{
		return Resources.ResourceId.RESOURCE_ID_MAX;
	}

	public static Resources.ResourceId GetIdByVariable(object theVariable)
	{
		return Resources.ResourceId.RESOURCE_ID_MAX;
	}

	public static void LinkUpResArray()
	{
		object[] array = new object[250];
		array[0] = Resources.IMAGE_POPCAP_LOGO;
		array[1] = Resources.IMAGE_POPCAP_LOGO_REGISTERED;
		array[2] = Resources.IMAGE_TITLESCREEN;
		array[3] = Resources.IMAGE_LOADING;
		array[4] = Resources.IMAGE_PVZ_LOGO;
		array[5] = Resources.FONT_BRIANNETOD16;
		array[6] = Resources.SOUND_BUTTONCLICK;
		array[7] = Resources.SOUND_LOADINGBAR_FLOWER;
		array[8] = Resources.SOUND_LOADINGBAR_ZOMBIE;
		array[9] = Resources.FONT_HOUSEOFTERROR16;
		array[10] = Resources.FONT_CONTINUUMBOLD14;
		array[11] = Resources.FONT_CONTINUUMBOLD14OUTLINE;
		array[12] = Resources.FONT_DWARVENTODCRAFT12;
		array[13] = Resources.FONT_DWARVENTODCRAFT15;
		array[14] = Resources.FONT_DWARVENTODCRAFT18;
		array[15] = Resources.FONT_PICO129;
		array[16] = Resources.FONT_BRIANNETOD12;
		array[17] = Resources.IMAGE_CHARREDZOMBIES;
		array[18] = Resources.IMAGE_ALMANACUI;
		array[19] = Resources.IMAGE_SEEDATLAS;
		array[20] = Resources.IMAGE_DAVE;
		array[21] = Resources.IMAGE_DIALOG;
		array[22] = Resources.IMAGE_CONVEYORBELT_BACKDROP;
		array[23] = Resources.IMAGE_CONVEYORBELT_BELT;
		array[24] = Resources.IMAGE_SPEECHBUBBLE;
		array[25] = Resources.IMAGE_LOC_EN;
		array[26] = Resources.IMAGE_ZOMBIE_NOTE_SMALL;
		array[27] = Resources.IMAGE_REANIM_ZOMBIESWON;
		array[28] = Resources.IMAGE_SCARY_POT;
		array[29] = Resources.SOUND_AWOOGA;
		array[30] = Resources.SOUND_BLEEP;
		array[31] = Resources.SOUND_BUZZER;
		array[32] = Resources.SOUND_CHOMP;
		array[33] = Resources.SOUND_CHOMP2;
		array[34] = Resources.SOUND_CHOMPSOFT;
		array[35] = Resources.SOUND_FLOOP;
		array[36] = Resources.SOUND_FROZEN;
		array[37] = Resources.SOUND_GULP;
		array[38] = Resources.SOUND_GROAN;
		array[39] = Resources.SOUND_GROAN2;
		array[40] = Resources.SOUND_GROAN3;
		array[41] = Resources.SOUND_GROAN4;
		array[42] = Resources.SOUND_GROAN5;
		array[43] = Resources.SOUND_GROAN6;
		array[44] = Resources.SOUND_LOSEMUSIC;
		array[45] = Resources.SOUND_MINDCONTROLLED;
		array[46] = Resources.SOUND_PAUSE;
		array[47] = Resources.SOUND_PLANT;
		array[48] = Resources.SOUND_PLANT2;
		array[49] = Resources.SOUND_POINTS;
		array[50] = Resources.SOUND_SEEDLIFT;
		array[51] = Resources.SOUND_SIREN;
		array[52] = Resources.SOUND_SLURP;
		array[53] = Resources.SOUND_SPLAT;
		array[54] = Resources.SOUND_SPLAT2;
		array[55] = Resources.SOUND_SPLAT3;
		array[56] = Resources.SOUND_SUKHBIR4;
		array[57] = Resources.SOUND_SUKHBIR5;
		array[58] = Resources.SOUND_SUKHBIR6;
		array[59] = Resources.SOUND_TAP;
		array[60] = Resources.SOUND_TAP2;
		array[61] = Resources.SOUND_THROW;
		array[62] = Resources.SOUND_THROW2;
		array[63] = Resources.SOUND_BLOVER;
		array[64] = Resources.SOUND_WINMUSIC;
		array[65] = Resources.SOUND_LAWNMOWER;
		array[66] = Resources.SOUND_BOING;
		array[67] = Resources.SOUND_JACKINTHEBOX;
		array[68] = Resources.SOUND_DIAMOND;
		array[69] = Resources.SOUND_DOLPHIN_APPEARS;
		array[70] = Resources.SOUND_DOLPHIN_BEFORE_JUMPING;
		array[71] = Resources.SOUND_POTATO_MINE;
		array[72] = Resources.SOUND_ZAMBONI;
		array[73] = Resources.SOUND_BALLOON_POP;
		array[74] = Resources.SOUND_THUNDER;
		array[75] = Resources.SOUND_ZOMBIESPLASH;
		array[76] = Resources.SOUND_BOWLING;
		array[77] = Resources.SOUND_BOWLINGIMPACT;
		array[78] = Resources.SOUND_BOWLINGIMPACT2;
		array[79] = Resources.SOUND_GRAVEBUSTERCHOMP;
		array[80] = Resources.SOUND_GRAVEBUTTON;
		array[81] = Resources.SOUND_LIMBS_POP;
		array[82] = Resources.SOUND_PLANTERN;
		array[83] = Resources.SOUND_POGO_ZOMBIE;
		array[84] = Resources.SOUND_SNOW_PEA_SPARKLES;
		array[85] = Resources.SOUND_PLANT_WATER;
		array[86] = Resources.SOUND_ZOMBIE_ENTERING_WATER;
		array[87] = Resources.SOUND_ZOMBIE_FALLING_1;
		array[88] = Resources.SOUND_ZOMBIE_FALLING_2;
		array[89] = Resources.SOUND_PUFF;
		array[90] = Resources.SOUND_FUME;
		array[91] = Resources.SOUND_HUGE_WAVE;
		array[92] = Resources.SOUND_SLOT_MACHINE;
		array[93] = Resources.SOUND_COIN;
		array[94] = Resources.SOUND_ROLL_IN;
		array[95] = Resources.SOUND_DIGGER_ZOMBIE;
		array[96] = Resources.SOUND_HATCHBACK_CLOSE;
		array[97] = Resources.SOUND_HATCHBACK_OPEN;
		array[98] = Resources.SOUND_KERNELPULT;
		array[99] = Resources.SOUND_KERNELPULT2;
		array[100] = Resources.SOUND_ZOMBAQUARIUM_DIE;
		array[101] = Resources.SOUND_BUNGEE_SCREAM;
		array[102] = Resources.SOUND_BUNGEE_SCREAM2;
		array[103] = Resources.SOUND_BUNGEE_SCREAM3;
		array[104] = Resources.SOUND_BUTTER;
		array[105] = Resources.SOUND_JACK_SURPRISE;
		array[106] = Resources.SOUND_JACK_SURPRISE2;
		array[107] = Resources.SOUND_NEWSPAPER_RARRGH;
		array[108] = Resources.SOUND_NEWSPAPER_RARRGH2;
		array[109] = Resources.SOUND_NEWSPAPER_RIP;
		array[110] = Resources.SOUND_SQUASH_HMM;
		array[111] = Resources.SOUND_SQUASH_HMM2;
		array[112] = Resources.SOUND_VASE_BREAKING;
		array[113] = Resources.SOUND_POOL_CLEANER;
		array[114] = Resources.SOUND_MAGNETSHROOM;
		array[115] = Resources.SOUND_LADDER_ZOMBIE;
		array[116] = Resources.SOUND_GARGANTUAR_THUMP;
		array[117] = Resources.SOUND_BASKETBALL;
		array[118] = Resources.SOUND_FIREPEA;
		array[119] = Resources.SOUND_IGNITE;
		array[120] = Resources.SOUND_IGNITE2;
		array[121] = Resources.SOUND_READYSETPLANT;
		array[122] = Resources.SOUND_DOOMSHROOM;
		array[123] = Resources.SOUND_EXPLOSION;
		array[124] = Resources.SOUND_FINALWAVE;
		array[125] = Resources.SOUND_REVERSE_EXPLOSION;
		array[126] = Resources.SOUND_RVTHROW;
		array[127] = Resources.SOUND_SHIELDHIT;
		array[128] = Resources.SOUND_SHIELDHIT2;
		array[129] = Resources.SOUND_BOSSEXPLOSION;
		array[130] = Resources.SOUND_CHERRYBOMB;
		array[131] = Resources.SOUND_BONK;
		array[132] = Resources.SOUND_SWING;
		array[133] = Resources.SOUND_RAIN;
		array[134] = Resources.SOUND_LIGHTFILL;
		array[135] = Resources.SOUND_PLASTICHIT;
		array[136] = Resources.SOUND_PLASTICHIT2;
		array[137] = Resources.SOUND_JALAPENO;
		array[138] = Resources.SOUND_BALLOONINFLATE;
		array[139] = Resources.SOUND_BIGCHOMP;
		array[140] = Resources.SOUND_MELONIMPACT;
		array[141] = Resources.SOUND_MELONIMPACT2;
		array[142] = Resources.SOUND_PLANTGROW;
		array[143] = Resources.SOUND_SHOOP;
		array[144] = Resources.SOUND_JUICY;
		array[145] = Resources.SOUND_COFFEE;
		array[146] = Resources.SOUND_WAKEUP;
		array[147] = Resources.SOUND_LOWGROAN;
		array[148] = Resources.SOUND_LOWGROAN2;
		array[149] = Resources.SOUND_PRIZE;
		array[150] = Resources.SOUND_YUCK;
		array[151] = Resources.SOUND_YUCK2;
		array[152] = Resources.SOUND_GRASSSTEP;
		array[153] = Resources.SOUND_SHOVEL;
		array[154] = Resources.SOUND_COBLAUNCH;
		array[155] = Resources.SOUND_WATERING;
		array[156] = Resources.SOUND_POLEVAULT;
		array[157] = Resources.SOUND_GRAVESTONE_RUMBLE;
		array[158] = Resources.SOUND_DIRT_RISE;
		array[159] = Resources.SOUND_FERTILIZER;
		array[160] = Resources.SOUND_PORTAL;
		array[161] = Resources.SOUND_SCREAM;
		array[162] = Resources.SOUND_PAPER;
		array[163] = Resources.SOUND_MONEYFALLS;
		array[164] = Resources.SOUND_IMP;
		array[165] = Resources.SOUND_IMP2;
		array[166] = Resources.SOUND_HYDRAULIC_SHORT;
		array[167] = Resources.SOUND_HYDRAULIC;
		array[168] = Resources.SOUND_GARGANTUDEATH;
		array[169] = Resources.SOUND_CERAMIC;
		array[170] = Resources.SOUND_BOSSBOULDERATTACK;
		array[171] = Resources.SOUND_CHIME;
		array[172] = Resources.SOUND_CRAZYDAVESHORT1;
		array[173] = Resources.SOUND_CRAZYDAVESHORT2;
		array[174] = Resources.SOUND_CRAZYDAVESHORT3;
		array[175] = Resources.SOUND_CRAZYDAVELONG1;
		array[176] = Resources.SOUND_CRAZYDAVELONG2;
		array[177] = Resources.SOUND_CRAZYDAVELONG3;
		array[178] = Resources.SOUND_CRAZYDAVEEXTRALONG1;
		array[179] = Resources.SOUND_CRAZYDAVEEXTRALONG2;
		array[180] = Resources.SOUND_CRAZYDAVEEXTRALONG3;
		array[181] = Resources.SOUND_CRAZYDAVECRAZY;
		array[182] = Resources.SOUND_DANCER;
		array[183] = Resources.SOUND_FINALFANFARE;
		array[184] = Resources.SOUND_CRAZYDAVESCREAM;
		array[185] = Resources.SOUND_CRAZYDAVESCREAM2;
		array[186] = Resources.SOUND_ACHIEVEMENT;
		array[187] = Resources.SOUND_BUGSPRAY;
		array[188] = Resources.SOUND_FERTILISER;
		array[189] = Resources.SOUND_PHONOGRAPH;
		array[190] = Resources.IMAGE_PILE;
		array[191] = Resources.IMAGE_PLANTSZOMBIES;
		array[192] = Resources.IMAGE_PARTICLES;
		array[193] = Resources.IMAGE_SLOTMACHINE_OVERLAY;
		array[194] = Resources.IMAGE_ZENGARDEN;
		array[195] = Resources.IMAGE_CACHED;
		array[196] = Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND;
		array[197] = Resources.IMAGE_SELECTORSCREEN_MAIN_BACKGROUND;
		array[198] = Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE;
		array[199] = Resources.IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA;
		array[200] = Resources.IMAGE_SELECTORSCREEN_QUICKPLAY_BACKGROUND;
		array[201] = Resources.IMAGE_GOODIES;
		array[202] = Resources.IMAGE_QUICKPLAY;
		array[203] = Resources.IMAGE_ACHIEVEMENT_GNOME;
		array[204] = Resources.IMAGE_MINIGAMES;
		array[205] = Resources.IMAGE_SELECTORSCREEN_MOREGAMES_BACKGROUND;
		array[206] = Resources.IMAGE_CREDITS_ZOMBIENOTE;
		array[207] = Resources.IMAGE_LEADERBOARDSCREEN_BACKGROUND;
		array[208] = Resources.IMAGE_BLACKHOLE;
		array[209] = Resources.IMAGE_EDGE_OF_SPACE;
		array[210] = Resources.IMAGE_STARS_1;
		array[211] = Resources.IMAGE_STARS_2;
		array[212] = Resources.IMAGE_STARS_3;
		array[213] = Resources.IMAGE_STARS_4;
		array[214] = Resources.IMAGE_BACKGROUND_GREENHOUSE;
		array[215] = Resources.IMAGE_AQUARIUM1;
		array[216] = Resources.IMAGE_BACKGROUND_MUSHROOMGARDEN;
		array[217] = Resources.IMAGE_BACKGROUND1;
		array[218] = Resources.IMAGE_BACKGROUND1_GAMEOVER_INTERIOR_OVERLAY;
		array[219] = Resources.IMAGE_BACKGROUND1_GAMEOVER_MASK;
		array[220] = Resources.IMAGE_BACKGROUND1UNSODDED;
		array[221] = Resources.IMAGE_BACKGROUND2;
		array[222] = Resources.IMAGE_BACKGROUND2_GAMEOVER_INTERIOR_OVERLAY;
		array[223] = Resources.IMAGE_BACKGROUND2_GAMEOVER_MASK;
		array[224] = Resources.IMAGE_BACKGROUND3;
		array[225] = Resources.IMAGE_BACKGROUND3_GAMEOVER_INTERIOR_OVERLAY;
		array[226] = Resources.IMAGE_BACKGROUND3_GAMEOVER_MASK;
		array[227] = Resources.IMAGE_BACKGROUND4;
		array[228] = Resources.IMAGE_BACKGROUND4_GAMEOVER_INTERIOR_OVERLAY;
		array[229] = Resources.IMAGE_BACKGROUND4_GAMEOVER_MASK;
		array[230] = Resources.IMAGE_FOG;
		array[231] = Resources.IMAGE_BACKGROUND5;
		array[232] = Resources.IMAGE_BACKGROUND5_GAMEOVER_MASK;
		array[233] = Resources.IMAGE_BACKGROUND6BOSS;
		array[234] = Resources.IMAGE_BACKGROUND6_GAMEOVER_MASK;
		array[235] = Resources.IMAGE_STORE_BACKGROUND;
		array[236] = Resources.IMAGE_STORE_BACKGROUNDNIGHT;
		array[237] = Resources.IMAGE_STORE_CAR;
		array[238] = Resources.IMAGE_STORE_CAR_NIGHT;
		array[239] = Resources.IMAGE_STORE_CARCLOSED;
		array[240] = Resources.IMAGE_STORE_CARCLOSED_NIGHT;
		array[241] = Resources.IMAGE_STORE_HATCHBACKOPEN;
		array[242] = Resources.IMAGE_ZOMBIE_NOTE;
		array[243] = Resources.IMAGE_ZOMBIE_NOTE1;
		array[244] = Resources.IMAGE_ZOMBIE_NOTE2;
		array[245] = Resources.IMAGE_ZOMBIE_NOTE3;
		array[246] = Resources.IMAGE_ZOMBIE_NOTE4;
		array[247] = Resources.IMAGE_ZOMBIE_FINAL_NOTE;
		array[248] = Resources.IMAGE_ZOMBIE_NOTE_HELP;
		Resources.gResources = array;
	}

	public static string GetStringIdById(int theId)
	{
		switch (theId)
		{
		case 0:
			return "IMAGE_POPCAP_LOGO";
		case 1:
			return "IMAGE_POPCAP_LOGO_REGISTERED";
		case 2:
			return "IMAGE_TITLESCREEN";
		case 3:
			return "IMAGE_LOADING";
		case 4:
			return "IMAGE_PVZ_LOGO";
		case 5:
			return "SOUND_BUTTONCLICK";
		case 6:
			return "SOUND_LOADINGBAR_FLOWER";
		case 7:
			return "SOUND_LOADINGBAR_ZOMBIE";
		case 8:
			return "FONT_BRIANNETOD16";
		case 9:
			return "FONT_HOUSEOFTERROR16";
		case 10:
			return "FONT_CONTINUUMBOLD14";
		case 11:
			return "FONT_CONTINUUMBOLD14OUTLINE";
		case 12:
			return "FONT_DWARVENTODCRAFT12";
		case 13:
			return "FONT_DWARVENTODCRAFT15";
		case 14:
			return "FONT_DWARVENTODCRAFT18";
		case 15:
			return "FONT_PICO129";
		case 16:
			return "FONT_BRIANNETOD12";
		case 17:
			return "IMAGE_CHARREDZOMBIES";
		case 18:
			return "IMAGE_ALMANACUI";
		case 19:
			return "IMAGE_SEEDATLAS";
		case 20:
			return "IMAGE_DAVE";
		case 21:
			return "IMAGE_DIALOG";
		case 22:
			return "IMAGE_CONVEYORBELT_BACKDROP";
		case 23:
			return "IMAGE_CONVEYORBELT_BELT";
		case 24:
			return "IMAGE_SPEECHBUBBLE";
		case 25:
			return "IMAGE_LOC_EN";
		case 26:
			return "IMAGE_ZOMBIE_NOTE_SMALL";
		case 27:
			return "IMAGE_REANIM_ZOMBIESWON";
		case 28:
			return "IMAGE_SCARY_POT";
		case 29:
			return "SOUND_AWOOGA";
		case 30:
			return "SOUND_BLEEP";
		case 31:
			return "SOUND_BUZZER";
		case 32:
			return "SOUND_CHOMP";
		case 33:
			return "SOUND_CHOMP2";
		case 34:
			return "SOUND_CHOMPSOFT";
		case 35:
			return "SOUND_FLOOP";
		case 36:
			return "SOUND_FROZEN";
		case 37:
			return "SOUND_GULP";
		case 38:
			return "SOUND_GROAN";
		case 39:
			return "SOUND_GROAN2";
		case 40:
			return "SOUND_GROAN3";
		case 41:
			return "SOUND_GROAN4";
		case 42:
			return "SOUND_GROAN5";
		case 43:
			return "SOUND_GROAN6";
		case 44:
			return "SOUND_LOSEMUSIC";
		case 45:
			return "SOUND_MINDCONTROLLED";
		case 46:
			return "SOUND_PAUSE";
		case 47:
			return "SOUND_PLANT";
		case 48:
			return "SOUND_PLANT2";
		case 49:
			return "SOUND_POINTS";
		case 50:
			return "SOUND_SEEDLIFT";
		case 51:
			return "SOUND_SIREN";
		case 52:
			return "SOUND_SLURP";
		case 53:
			return "SOUND_SPLAT";
		case 54:
			return "SOUND_SPLAT2";
		case 55:
			return "SOUND_SPLAT3";
		case 56:
			return "SOUND_SUKHBIR4";
		case 57:
			return "SOUND_SUKHBIR5";
		case 58:
			return "SOUND_SUKHBIR6";
		case 59:
			return "SOUND_TAP";
		case 60:
			return "SOUND_TAP2";
		case 61:
			return "SOUND_THROW";
		case 62:
			return "SOUND_THROW2";
		case 63:
			return "SOUND_BLOVER";
		case 64:
			return "SOUND_WINMUSIC";
		case 65:
			return "SOUND_LAWNMOWER";
		case 66:
			return "SOUND_BOING";
		case 67:
			return "SOUND_JACKINTHEBOX";
		case 68:
			return "SOUND_DIAMOND";
		case 69:
			return "SOUND_DOLPHIN_APPEARS";
		case 70:
			return "SOUND_DOLPHIN_BEFORE_JUMPING";
		case 71:
			return "SOUND_POTATO_MINE";
		case 72:
			return "SOUND_ZAMBONI";
		case 73:
			return "SOUND_BALLOON_POP";
		case 74:
			return "SOUND_THUNDER";
		case 75:
			return "SOUND_ZOMBIESPLASH";
		case 76:
			return "SOUND_BOWLING";
		case 77:
			return "SOUND_BOWLINGIMPACT";
		case 78:
			return "SOUND_BOWLINGIMPACT2";
		case 79:
			return "SOUND_GRAVEBUSTERCHOMP";
		case 80:
			return "SOUND_GRAVEBUTTON";
		case 81:
			return "SOUND_LIMBS_POP";
		case 82:
			return "SOUND_PLANTERN";
		case 83:
			return "SOUND_POGO_ZOMBIE";
		case 84:
			return "SOUND_SNOW_PEA_SPARKLES";
		case 85:
			return "SOUND_PLANT_WATER";
		case 86:
			return "SOUND_ZOMBIE_ENTERING_WATER";
		case 87:
			return "SOUND_ZOMBIE_FALLING_1";
		case 88:
			return "SOUND_ZOMBIE_FALLING_2";
		case 89:
			return "SOUND_PUFF";
		case 90:
			return "SOUND_FUME";
		case 91:
			return "SOUND_HUGE_WAVE";
		case 92:
			return "SOUND_SLOT_MACHINE";
		case 93:
			return "SOUND_COIN";
		case 94:
			return "SOUND_ROLL_IN";
		case 95:
			return "SOUND_DIGGER_ZOMBIE";
		case 96:
			return "SOUND_HATCHBACK_CLOSE";
		case 97:
			return "SOUND_HATCHBACK_OPEN";
		case 98:
			return "SOUND_KERNELPULT";
		case 99:
			return "SOUND_KERNELPULT2";
		case 100:
			return "SOUND_ZOMBAQUARIUM_DIE";
		case 101:
			return "SOUND_BUNGEE_SCREAM";
		case 102:
			return "SOUND_BUNGEE_SCREAM2";
		case 103:
			return "SOUND_BUNGEE_SCREAM3";
		case 104:
			return "SOUND_BUTTER";
		case 105:
			return "SOUND_JACK_SURPRISE";
		case 106:
			return "SOUND_JACK_SURPRISE2";
		case 107:
			return "SOUND_NEWSPAPER_RARRGH";
		case 108:
			return "SOUND_NEWSPAPER_RARRGH2";
		case 109:
			return "SOUND_NEWSPAPER_RIP";
		case 110:
			return "SOUND_SQUASH_HMM";
		case 111:
			return "SOUND_SQUASH_HMM2";
		case 112:
			return "SOUND_VASE_BREAKING";
		case 113:
			return "SOUND_POOL_CLEANER";
		case 114:
			return "SOUND_MAGNETSHROOM";
		case 115:
			return "SOUND_LADDER_ZOMBIE";
		case 116:
			return "SOUND_GARGANTUAR_THUMP";
		case 117:
			return "SOUND_BASKETBALL";
		case 118:
			return "SOUND_FIREPEA";
		case 119:
			return "SOUND_IGNITE";
		case 120:
			return "SOUND_IGNITE2";
		case 121:
			return "SOUND_READYSETPLANT";
		case 122:
			return "SOUND_DOOMSHROOM";
		case 123:
			return "SOUND_EXPLOSION";
		case 124:
			return "SOUND_FINALWAVE";
		case 125:
			return "SOUND_REVERSE_EXPLOSION";
		case 126:
			return "SOUND_RVTHROW";
		case 127:
			return "SOUND_SHIELDHIT";
		case 128:
			return "SOUND_SHIELDHIT2";
		case 129:
			return "SOUND_BOSSEXPLOSION";
		case 130:
			return "SOUND_CHERRYBOMB";
		case 131:
			return "SOUND_BONK";
		case 132:
			return "SOUND_SWING";
		case 133:
			return "SOUND_RAIN";
		case 134:
			return "SOUND_LIGHTFILL";
		case 135:
			return "SOUND_PLASTICHIT";
		case 136:
			return "SOUND_PLASTICHIT2";
		case 137:
			return "SOUND_JALAPENO";
		case 138:
			return "SOUND_BALLOONINFLATE";
		case 139:
			return "SOUND_BIGCHOMP";
		case 140:
			return "SOUND_MELONIMPACT";
		case 141:
			return "SOUND_MELONIMPACT2";
		case 142:
			return "SOUND_PLANTGROW";
		case 143:
			return "SOUND_SHOOP";
		case 144:
			return "SOUND_JUICY";
		case 145:
			return "SOUND_COFFEE";
		case 146:
			return "SOUND_WAKEUP";
		case 147:
			return "SOUND_LOWGROAN";
		case 148:
			return "SOUND_LOWGROAN2";
		case 149:
			return "SOUND_PRIZE";
		case 150:
			return "SOUND_YUCK";
		case 151:
			return "SOUND_YUCK2";
		case 152:
			return "SOUND_GRASSSTEP";
		case 153:
			return "SOUND_SHOVEL";
		case 154:
			return "SOUND_COBLAUNCH";
		case 155:
			return "SOUND_WATERING";
		case 156:
			return "SOUND_POLEVAULT";
		case 157:
			return "SOUND_GRAVESTONE_RUMBLE";
		case 158:
			return "SOUND_DIRT_RISE";
		case 159:
			return "SOUND_FERTILIZER";
		case 160:
			return "SOUND_PORTAL";
		case 161:
			return "SOUND_SCREAM";
		case 162:
			return "SOUND_PAPER";
		case 163:
			return "SOUND_MONEYFALLS";
		case 164:
			return "SOUND_IMP";
		case 165:
			return "SOUND_IMP2";
		case 166:
			return "SOUND_HYDRAULIC_SHORT";
		case 167:
			return "SOUND_HYDRAULIC";
		case 168:
			return "SOUND_GARGANTUDEATH";
		case 169:
			return "SOUND_CERAMIC";
		case 170:
			return "SOUND_BOSSBOULDERATTACK";
		case 171:
			return "SOUND_CHIME";
		case 172:
			return "SOUND_CRAZYDAVESHORT1";
		case 173:
			return "SOUND_CRAZYDAVESHORT2";
		case 174:
			return "SOUND_CRAZYDAVESHORT3";
		case 175:
			return "SOUND_CRAZYDAVELONG1";
		case 176:
			return "SOUND_CRAZYDAVELONG2";
		case 177:
			return "SOUND_CRAZYDAVELONG3";
		case 178:
			return "SOUND_CRAZYDAVEEXTRALONG1";
		case 179:
			return "SOUND_CRAZYDAVEEXTRALONG2";
		case 180:
			return "SOUND_CRAZYDAVEEXTRALONG3";
		case 181:
			return "SOUND_CRAZYDAVECRAZY";
		case 182:
			return "SOUND_DANCER";
		case 183:
			return "SOUND_FINALFANFARE";
		case 184:
			return "SOUND_CRAZYDAVESCREAM";
		case 185:
			return "SOUND_CRAZYDAVESCREAM2";
		case 186:
			return "SOUND_ACHIEVEMENT";
		case 187:
			return "SOUND_BUGSPRAY";
		case 188:
			return "SOUND_FERTILISER";
		case 189:
			return "SOUND_PHONOGRAPH";
		case 190:
			return "IMAGE_PILE";
		case 191:
			return "IMAGE_PLANTSZOMBIES";
		case 192:
			return "IMAGE_PARTICLES";
		case 193:
			return "IMAGE_SLOTMACHINE_OVERLAY";
		case 194:
			return "IMAGE_ZENGARDEN";
		case 195:
			return "IMAGE_CACHED";
		case 196:
			return "IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND";
		case 197:
			return "IMAGE_SELECTORSCREEN_MAIN_BACKGROUND";
		case 198:
			return "IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE";
		case 199:
			return "IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA";
		case 200:
			return "IMAGE_SELECTORSCREEN_QUICKPLAY_BACKGROUND";
		case 201:
			return "IMAGE_GOODIES";
		case 202:
			return "IMAGE_QUICKPLAY";
		case 203:
			return "IMAGE_ACHIEVEMENT_GNOME";
		case 204:
			return "IMAGE_MINIGAMES";
		case 205:
			return "IMAGE_SELECTORSCREEN_MOREGAMES_BACKGROUND";
		case 206:
			return "IMAGE_CREDITS_ZOMBIENOTE";
		case 207:
			return "IMAGE_LEADERBOARDSCREEN_BACKGROUND";
		case 208:
			return "IMAGE_BLACKHOLE";
		case 209:
			return "IMAGE_EDGE_OF_SPACE";
		case 210:
			return "IMAGE_STARS_1";
		case 211:
			return "IMAGE_STARS_2";
		case 212:
			return "IMAGE_STARS_3";
		case 213:
			return "IMAGE_STARS_4";
		case 214:
			return "IMAGE_BACKGROUND_GREENHOUSE";
		case 215:
			return "IMAGE_AQUARIUM1";
		case 216:
			return "IMAGE_BACKGROUND_MUSHROOMGARDEN";
		case 217:
			return "IMAGE_BACKGROUND1";
		case 218:
			return "IMAGE_BACKGROUND1_GAMEOVER_INTERIOR_OVERLAY";
		case 219:
			return "IMAGE_BACKGROUND1_GAMEOVER_MASK";
		case 220:
			return "IMAGE_BACKGROUND1UNSODDED";
		case 221:
			return "IMAGE_BACKGROUND2";
		case 222:
			return "IMAGE_BACKGROUND2_GAMEOVER_INTERIOR_OVERLAY";
		case 223:
			return "IMAGE_BACKGROUND2_GAMEOVER_MASK";
		case 224:
			return "IMAGE_BACKGROUND3";
		case 225:
			return "IMAGE_BACKGROUND3_GAMEOVER_INTERIOR_OVERLAY";
		case 226:
			return "IMAGE_BACKGROUND3_GAMEOVER_MASK";
		case 227:
			return "IMAGE_BACKGROUND4";
		case 228:
			return "IMAGE_BACKGROUND4_GAMEOVER_INTERIOR_OVERLAY";
		case 229:
			return "IMAGE_BACKGROUND4_GAMEOVER_MASK";
		case 230:
			return "IMAGE_FOG";
		case 231:
			return "IMAGE_BACKGROUND5";
		case 232:
			return "IMAGE_BACKGROUND5_GAMEOVER_MASK";
		case 233:
			return "IMAGE_BACKGROUND6BOSS";
		case 234:
			return "IMAGE_BACKGROUND6_GAMEOVER_MASK";
		case 235:
			return "IMAGE_STORE_BACKGROUND";
		case 236:
			return "IMAGE_STORE_BACKGROUNDNIGHT";
		case 237:
			return "IMAGE_STORE_CAR";
		case 238:
			return "IMAGE_STORE_CAR_NIGHT";
		case 239:
			return "IMAGE_STORE_CARCLOSED";
		case 240:
			return "IMAGE_STORE_CARCLOSED_NIGHT";
		case 241:
			return "IMAGE_STORE_HATCHBACKOPEN";
		case 242:
			return "IMAGE_ZOMBIE_NOTE";
		case 243:
			return "IMAGE_ZOMBIE_NOTE1";
		case 244:
			return "IMAGE_ZOMBIE_NOTE2";
		case 245:
			return "IMAGE_ZOMBIE_NOTE3";
		case 246:
			return "IMAGE_ZOMBIE_NOTE4";
		case 247:
			return "IMAGE_ZOMBIE_FINAL_NOTE";
		case 248:
			return "IMAGE_ZOMBIE_NOTE_HELP";
		default:
			return "";
		}
	}

	public static readonly Image LOAD_LOGO_IMAGE_DATA;

	public static Image IMAGE_POPCAP_LOGO;

	public static Image IMAGE_POPCAP_LOGO_REGISTERED;

	public static Image IMAGE_TITLESCREEN;

	public static Image IMAGE_LOADING;

	public static Image IMAGE_PVZ_LOGO;

	public static int SOUND_BUTTONCLICK;

	public static int SOUND_LOADINGBAR_FLOWER;

	public static int SOUND_LOADINGBAR_ZOMBIE;

	public static Font FONT_BRIANNETOD16;

	public static Font FONT_HOUSEOFTERROR16;

	public static Font FONT_CONTINUUMBOLD14;

	public static Font FONT_CONTINUUMBOLD14OUTLINE;

	public static Font FONT_DWARVENTODCRAFT12;

	public static Font FONT_DWARVENTODCRAFT15;

	public static Font FONT_DWARVENTODCRAFT18;

	public static Font FONT_PICO129;

	public static Font FONT_BRIANNETOD12;

	public static Image IMAGE_CHARREDZOMBIES;

	public static Image IMAGE_ALMANACUI;

	public static Image IMAGE_SEEDATLAS;

	public static Image IMAGE_DAVE;

	public static Image IMAGE_DIALOG;

	public static Image IMAGE_CONVEYORBELT_BACKDROP;

	public static Image IMAGE_CONVEYORBELT_BELT;

	public static Image IMAGE_SPEECHBUBBLE;

	public static Image IMAGE_LOC_EN;

	public static Image IMAGE_ZOMBIE_NOTE_SMALL;

	public static Image IMAGE_REANIM_ZOMBIESWON;

	public static Image IMAGE_SCARY_POT;

	public static int SOUND_AWOOGA;

	public static int SOUND_BLEEP;

	public static int SOUND_BUZZER;

	public static int SOUND_CHOMP;

	public static int SOUND_CHOMP2;

	public static int SOUND_CHOMPSOFT;

	public static int SOUND_FLOOP;

	public static int SOUND_FROZEN;

	public static int SOUND_GULP;

	public static int SOUND_GROAN;

	public static int SOUND_GROAN2;

	public static int SOUND_GROAN3;

	public static int SOUND_GROAN4;

	public static int SOUND_GROAN5;

	public static int SOUND_GROAN6;

	public static int SOUND_LOSEMUSIC;

	public static int SOUND_MINDCONTROLLED;

	public static int SOUND_PAUSE;

	public static int SOUND_PLANT;

	public static int SOUND_PLANT2;

	public static int SOUND_POINTS;

	public static int SOUND_SEEDLIFT;

	public static int SOUND_SIREN;

	public static int SOUND_SLURP;

	public static int SOUND_SPLAT;

	public static int SOUND_SPLAT2;

	public static int SOUND_SPLAT3;

	public static int SOUND_SUKHBIR4;

	public static int SOUND_SUKHBIR5;

	public static int SOUND_SUKHBIR6;

	public static int SOUND_TAP;

	public static int SOUND_TAP2;

	public static int SOUND_THROW;

	public static int SOUND_THROW2;

	public static int SOUND_BLOVER;

	public static int SOUND_WINMUSIC;

	public static int SOUND_LAWNMOWER;

	public static int SOUND_BOING;

	public static int SOUND_JACKINTHEBOX;

	public static int SOUND_DIAMOND;

	public static int SOUND_DOLPHIN_APPEARS;

	public static int SOUND_DOLPHIN_BEFORE_JUMPING;

	public static int SOUND_POTATO_MINE;

	public static int SOUND_ZAMBONI;

	public static int SOUND_BALLOON_POP;

	public static int SOUND_THUNDER;

	public static int SOUND_ZOMBIESPLASH;

	public static int SOUND_BOWLING;

	public static int SOUND_BOWLINGIMPACT;

	public static int SOUND_BOWLINGIMPACT2;

	public static int SOUND_GRAVEBUSTERCHOMP;

	public static int SOUND_GRAVEBUTTON;

	public static int SOUND_LIMBS_POP;

	public static int SOUND_PLANTERN;

	public static int SOUND_POGO_ZOMBIE;

	public static int SOUND_SNOW_PEA_SPARKLES;

	public static int SOUND_PLANT_WATER;

	public static int SOUND_ZOMBIE_ENTERING_WATER;

	public static int SOUND_ZOMBIE_FALLING_1;

	public static int SOUND_ZOMBIE_FALLING_2;

	public static int SOUND_PUFF;

	public static int SOUND_FUME;

	public static int SOUND_HUGE_WAVE;

	public static int SOUND_SLOT_MACHINE;

	public static int SOUND_COIN;

	public static int SOUND_ROLL_IN;

	public static int SOUND_DIGGER_ZOMBIE;

	public static int SOUND_HATCHBACK_CLOSE;

	public static int SOUND_HATCHBACK_OPEN;

	public static int SOUND_KERNELPULT;

	public static int SOUND_KERNELPULT2;

	public static int SOUND_ZOMBAQUARIUM_DIE;

	public static int SOUND_BUNGEE_SCREAM;

	public static int SOUND_BUNGEE_SCREAM2;

	public static int SOUND_BUNGEE_SCREAM3;

	public static int SOUND_BUTTER;

	public static int SOUND_JACK_SURPRISE;

	public static int SOUND_JACK_SURPRISE2;

	public static int SOUND_NEWSPAPER_RARRGH;

	public static int SOUND_NEWSPAPER_RARRGH2;

	public static int SOUND_NEWSPAPER_RIP;

	public static int SOUND_SQUASH_HMM;

	public static int SOUND_SQUASH_HMM2;

	public static int SOUND_VASE_BREAKING;

	public static int SOUND_POOL_CLEANER;

	public static int SOUND_MAGNETSHROOM;

	public static int SOUND_LADDER_ZOMBIE;

	public static int SOUND_GARGANTUAR_THUMP;

	public static int SOUND_BASKETBALL;

	public static int SOUND_FIREPEA;

	public static int SOUND_IGNITE;

	public static int SOUND_IGNITE2;

	public static int SOUND_READYSETPLANT;

	public static int SOUND_DOOMSHROOM;

	public static int SOUND_EXPLOSION;

	public static int SOUND_FINALWAVE;

	public static int SOUND_REVERSE_EXPLOSION;

	public static int SOUND_RVTHROW;

	public static int SOUND_SHIELDHIT;

	public static int SOUND_SHIELDHIT2;

	public static int SOUND_BOSSEXPLOSION;

	public static int SOUND_CHERRYBOMB;

	public static int SOUND_BONK;

	public static int SOUND_SWING;

	public static int SOUND_RAIN;

	public static int SOUND_LIGHTFILL;

	public static int SOUND_PLASTICHIT;

	public static int SOUND_PLASTICHIT2;

	public static int SOUND_JALAPENO;

	public static int SOUND_BALLOONINFLATE;

	public static int SOUND_BIGCHOMP;

	public static int SOUND_MELONIMPACT;

	public static int SOUND_MELONIMPACT2;

	public static int SOUND_PLANTGROW;

	public static int SOUND_SHOOP;

	public static int SOUND_JUICY;

	public static int SOUND_COFFEE;

	public static int SOUND_WAKEUP;

	public static int SOUND_LOWGROAN;

	public static int SOUND_LOWGROAN2;

	public static int SOUND_PRIZE;

	public static int SOUND_YUCK;

	public static int SOUND_YUCK2;

	public static int SOUND_GRASSSTEP;

	public static int SOUND_SHOVEL;

	public static int SOUND_COBLAUNCH;

	public static int SOUND_WATERING;

	public static int SOUND_POLEVAULT;

	public static int SOUND_GRAVESTONE_RUMBLE;

	public static int SOUND_DIRT_RISE;

	public static int SOUND_FERTILIZER;

	public static int SOUND_PORTAL;

	public static int SOUND_SCREAM;

	public static int SOUND_PAPER;

	public static int SOUND_MONEYFALLS;

	public static int SOUND_IMP;

	public static int SOUND_IMP2;

	public static int SOUND_HYDRAULIC_SHORT;

	public static int SOUND_HYDRAULIC;

	public static int SOUND_GARGANTUDEATH;

	public static int SOUND_CERAMIC;

	public static int SOUND_BOSSBOULDERATTACK;

	public static int SOUND_CHIME;

	public static int SOUND_CRAZYDAVESHORT1;

	public static int SOUND_CRAZYDAVESHORT2;

	public static int SOUND_CRAZYDAVESHORT3;

	public static int SOUND_CRAZYDAVELONG1;

	public static int SOUND_CRAZYDAVELONG2;

	public static int SOUND_CRAZYDAVELONG3;

	public static int SOUND_CRAZYDAVEEXTRALONG1;

	public static int SOUND_CRAZYDAVEEXTRALONG2;

	public static int SOUND_CRAZYDAVEEXTRALONG3;

	public static int SOUND_CRAZYDAVECRAZY;

	public static int SOUND_DANCER;

	public static int SOUND_FINALFANFARE;

	public static int SOUND_CRAZYDAVESCREAM;

	public static int SOUND_CRAZYDAVESCREAM2;

	public static int SOUND_ACHIEVEMENT;

	public static int SOUND_BUGSPRAY;

	public static int SOUND_FERTILISER;

	public static int SOUND_PHONOGRAPH;

	public static Image IMAGE_PILE;

	public static Image IMAGE_PLANTSZOMBIES;

	public static Image IMAGE_PARTICLES;

	public static Image IMAGE_SLOTMACHINE_OVERLAY;

	public static Image IMAGE_ZENGARDEN;

	public static Image IMAGE_CACHED;

	public static Image IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND;

	public static Image IMAGE_SELECTORSCREEN_MAIN_BACKGROUND;

	public static Image IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE;

	public static Image IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA;

	public static Image IMAGE_SELECTORSCREEN_QUICKPLAY_BACKGROUND;

	public static Image IMAGE_GOODIES;

	public static Image IMAGE_QUICKPLAY;

	public static Image IMAGE_ACHIEVEMENT_GNOME;

	public static Image IMAGE_MINIGAMES;

	public static Image IMAGE_SELECTORSCREEN_MOREGAMES_BACKGROUND;

	public static Image IMAGE_CREDITS_ZOMBIENOTE;

	public static Image IMAGE_LEADERBOARDSCREEN_BACKGROUND;

	public static Image IMAGE_BLACKHOLE;

	public static Image IMAGE_EDGE_OF_SPACE;

	public static Image IMAGE_STARS_1;

	public static Image IMAGE_STARS_2;

	public static Image IMAGE_STARS_3;

	public static Image IMAGE_STARS_4;

	public static Image IMAGE_BACKGROUND_GREENHOUSE;

	public static Image IMAGE_AQUARIUM1;

	public static Image IMAGE_BACKGROUND_MUSHROOMGARDEN;

	public static Image IMAGE_BACKGROUND1;

	public static Image IMAGE_BACKGROUND1_GAMEOVER_INTERIOR_OVERLAY;

	public static Image IMAGE_BACKGROUND1_GAMEOVER_MASK;

	public static Image IMAGE_BACKGROUND1UNSODDED;

	public static Image IMAGE_BACKGROUND2;

	public static Image IMAGE_BACKGROUND2_GAMEOVER_INTERIOR_OVERLAY;

	public static Image IMAGE_BACKGROUND2_GAMEOVER_MASK;

	public static Image IMAGE_BACKGROUND3;

	public static Image IMAGE_BACKGROUND3_GAMEOVER_INTERIOR_OVERLAY;

	public static Image IMAGE_BACKGROUND3_GAMEOVER_MASK;

	public static Image IMAGE_BACKGROUND4;

	public static Image IMAGE_BACKGROUND4_GAMEOVER_INTERIOR_OVERLAY;

	public static Image IMAGE_BACKGROUND4_GAMEOVER_MASK;

	public static Image IMAGE_FOG;

	public static Image IMAGE_BACKGROUND5;

	public static Image IMAGE_BACKGROUND5_GAMEOVER_MASK;

	public static Image IMAGE_BACKGROUND6BOSS;

	public static Image IMAGE_BACKGROUND6_GAMEOVER_MASK;

	public static Image IMAGE_STORE_BACKGROUND;

	public static Image IMAGE_STORE_BACKGROUNDNIGHT;

	public static Image IMAGE_STORE_CAR;

	public static Image IMAGE_STORE_CAR_NIGHT;

	public static Image IMAGE_STORE_CARCLOSED;

	public static Image IMAGE_STORE_CARCLOSED_NIGHT;

	public static Image IMAGE_STORE_HATCHBACKOPEN;

	public static Image IMAGE_ZOMBIE_NOTE;

	public static Image IMAGE_ZOMBIE_NOTE1;

	public static Image IMAGE_ZOMBIE_NOTE2;

	public static Image IMAGE_ZOMBIE_NOTE3;

	public static Image IMAGE_ZOMBIE_NOTE4;

	public static Image IMAGE_ZOMBIE_FINAL_NOTE;

	public static Image IMAGE_ZOMBIE_NOTE_HELP;

	public static bool gNeedRecalcVariableToIdMap = false;

	public static object[] gResources;

	public enum ResourceId
	{
		IMAGE_POPCAP_LOGO_ID,
		IMAGE_POPCAP_LOGO_REGISTERED_ID,
		IMAGE_TITLESCREEN_ID,
		IMAGE_LOADING_ID,
		IMAGE_PVZ_LOGO_ID,
		SOUND_BUTTONCLICK_ID,
		SOUND_LOADINGBAR_FLOWER_ID,
		SOUND_LOADINGBAR_ZOMBIE_ID,
		FONT_BRIANNETOD16_ID,
		FONT_HOUSEOFTERROR16_ID,
		FONT_CONTINUUMBOLD14_ID,
		FONT_CONTINUUMBOLD14OUTLINE_ID,
		FONT_DWARVENTODCRAFT12_ID,
		FONT_DWARVENTODCRAFT15_ID,
		FONT_DWARVENTODCRAFT18_ID,
		FONT_PICO129_ID,
		FONT_BRIANNETOD12_ID,
		IMAGE_CHARREDZOMBIES_ID,
		IMAGE_ALMANACUI_ID,
		IMAGE_SEEDATLAS_ID,
		IMAGE_DAVE_ID,
		IMAGE_DIALOG_ID,
		IMAGE_CONVEYORBELT_BACKDROP_ID,
		IMAGE_CONVEYORBELT_BELT_ID,
		IMAGE_SPEECHBUBBLE_ID,
		IMAGE_LOC_EN_ID,
		IMAGE_ZOMBIE_NOTE_SMALL_ID,
		IMAGE_REANIM_ZOMBIESWON_ID,
		IMAGE_SCARY_POT_ID,
		SOUND_AWOOGA_ID,
		SOUND_BLEEP_ID,
		SOUND_BUZZER_ID,
		SOUND_CHOMP_ID,
		SOUND_CHOMP2_ID,
		SOUND_CHOMPSOFT_ID,
		SOUND_FLOOP_ID,
		SOUND_FROZEN_ID,
		SOUND_GULP_ID,
		SOUND_GROAN_ID,
		SOUND_GROAN2_ID,
		SOUND_GROAN3_ID,
		SOUND_GROAN4_ID,
		SOUND_GROAN5_ID,
		SOUND_GROAN6_ID,
		SOUND_LOSEMUSIC_ID,
		SOUND_MINDCONTROLLED_ID,
		SOUND_PAUSE_ID,
		SOUND_PLANT_ID,
		SOUND_PLANT2_ID,
		SOUND_POINTS_ID,
		SOUND_SEEDLIFT_ID,
		SOUND_SIREN_ID,
		SOUND_SLURP_ID,
		SOUND_SPLAT_ID,
		SOUND_SPLAT2_ID,
		SOUND_SPLAT3_ID,
		SOUND_SUKHBIR4_ID,
		SOUND_SUKHBIR5_ID,
		SOUND_SUKHBIR6_ID,
		SOUND_TAP_ID,
		SOUND_TAP2_ID,
		SOUND_THROW_ID,
		SOUND_THROW2_ID,
		SOUND_BLOVER_ID,
		SOUND_WINMUSIC_ID,
		SOUND_LAWNMOWER_ID,
		SOUND_BOING_ID,
		SOUND_JACKINTHEBOX_ID,
		SOUND_DIAMOND_ID,
		SOUND_DOLPHIN_APPEARS_ID,
		SOUND_DOLPHIN_BEFORE_JUMPING_ID,
		SOUND_POTATO_MINE_ID,
		SOUND_ZAMBONI_ID,
		SOUND_BALLOON_POP_ID,
		SOUND_THUNDER_ID,
		SOUND_ZOMBIESPLASH_ID,
		SOUND_BOWLING_ID,
		SOUND_BOWLINGIMPACT_ID,
		SOUND_BOWLINGIMPACT2_ID,
		SOUND_GRAVEBUSTERCHOMP_ID,
		SOUND_GRAVEBUTTON_ID,
		SOUND_LIMBS_POP_ID,
		SOUND_PLANTERN_ID,
		SOUND_POGO_ZOMBIE_ID,
		SOUND_SNOW_PEA_SPARKLES_ID,
		SOUND_PLANT_WATER_ID,
		SOUND_ZOMBIE_ENTERING_WATER_ID,
		SOUND_ZOMBIE_FALLING_1_ID,
		SOUND_ZOMBIE_FALLING_2_ID,
		SOUND_PUFF_ID,
		SOUND_FUME_ID,
		SOUND_HUGE_WAVE_ID,
		SOUND_SLOT_MACHINE_ID,
		SOUND_COIN_ID,
		SOUND_ROLL_IN_ID,
		SOUND_DIGGER_ZOMBIE_ID,
		SOUND_HATCHBACK_CLOSE_ID,
		SOUND_HATCHBACK_OPEN_ID,
		SOUND_KERNELPULT_ID,
		SOUND_KERNELPULT2_ID,
		SOUND_ZOMBAQUARIUM_DIE_ID,
		SOUND_BUNGEE_SCREAM_ID,
		SOUND_BUNGEE_SCREAM2_ID,
		SOUND_BUNGEE_SCREAM3_ID,
		SOUND_BUTTER_ID,
		SOUND_JACK_SURPRISE_ID,
		SOUND_JACK_SURPRISE2_ID,
		SOUND_NEWSPAPER_RARRGH_ID,
		SOUND_NEWSPAPER_RARRGH2_ID,
		SOUND_NEWSPAPER_RIP_ID,
		SOUND_SQUASH_HMM_ID,
		SOUND_SQUASH_HMM2_ID,
		SOUND_VASE_BREAKING_ID,
		SOUND_POOL_CLEANER_ID,
		SOUND_MAGNETSHROOM_ID,
		SOUND_LADDER_ZOMBIE_ID,
		SOUND_GARGANTUAR_THUMP_ID,
		SOUND_BASKETBALL_ID,
		SOUND_FIREPEA_ID,
		SOUND_IGNITE_ID,
		SOUND_IGNITE2_ID,
		SOUND_READYSETPLANT_ID,
		SOUND_DOOMSHROOM_ID,
		SOUND_EXPLOSION_ID,
		SOUND_FINALWAVE_ID,
		SOUND_REVERSE_EXPLOSION_ID,
		SOUND_RVTHROW_ID,
		SOUND_SHIELDHIT_ID,
		SOUND_SHIELDHIT2_ID,
		SOUND_BOSSEXPLOSION_ID,
		SOUND_CHERRYBOMB_ID,
		SOUND_BONK_ID,
		SOUND_SWING_ID,
		SOUND_RAIN_ID,
		SOUND_LIGHTFILL_ID,
		SOUND_PLASTICHIT_ID,
		SOUND_PLASTICHIT2_ID,
		SOUND_JALAPENO_ID,
		SOUND_BALLOONINFLATE_ID,
		SOUND_BIGCHOMP_ID,
		SOUND_MELONIMPACT_ID,
		SOUND_MELONIMPACT2_ID,
		SOUND_PLANTGROW_ID,
		SOUND_SHOOP_ID,
		SOUND_JUICY_ID,
		SOUND_COFFEE_ID,
		SOUND_WAKEUP_ID,
		SOUND_LOWGROAN_ID,
		SOUND_LOWGROAN2_ID,
		SOUND_PRIZE_ID,
		SOUND_YUCK_ID,
		SOUND_YUCK2_ID,
		SOUND_GRASSSTEP_ID,
		SOUND_SHOVEL_ID,
		SOUND_COBLAUNCH_ID,
		SOUND_WATERING_ID,
		SOUND_POLEVAULT_ID,
		SOUND_GRAVESTONE_RUMBLE_ID,
		SOUND_DIRT_RISE_ID,
		SOUND_FERTILIZER_ID,
		SOUND_PORTAL_ID,
		SOUND_SCREAM_ID,
		SOUND_PAPER_ID,
		SOUND_MONEYFALLS_ID,
		SOUND_IMP_ID,
		SOUND_IMP2_ID,
		SOUND_HYDRAULIC_SHORT_ID,
		SOUND_HYDRAULIC_ID,
		SOUND_GARGANTUDEATH_ID,
		SOUND_CERAMIC_ID,
		SOUND_BOSSBOULDERATTACK_ID,
		SOUND_CHIME_ID,
		SOUND_CRAZYDAVESHORT1_ID,
		SOUND_CRAZYDAVESHORT2_ID,
		SOUND_CRAZYDAVESHORT3_ID,
		SOUND_CRAZYDAVELONG1_ID,
		SOUND_CRAZYDAVELONG2_ID,
		SOUND_CRAZYDAVELONG3_ID,
		SOUND_CRAZYDAVEEXTRALONG1_ID,
		SOUND_CRAZYDAVEEXTRALONG2_ID,
		SOUND_CRAZYDAVEEXTRALONG3_ID,
		SOUND_CRAZYDAVECRAZY_ID,
		SOUND_DANCER_ID,
		SOUND_FINALFANFARE_ID,
		SOUND_CRAZYDAVESCREAM_ID,
		SOUND_CRAZYDAVESCREAM2_ID,
		SOUND_ACHIEVEMENT_ID,
		SOUND_BUGSPRAY_ID,
		SOUND_FERTILISER_ID,
		SOUND_PHONOGRAPH_ID,
		IMAGE_PILE_ID,
		IMAGE_PLANTSZOMBIES_ID,
		IMAGE_PARTICLES_ID,
		IMAGE_SLOTMACHINE_OVERLAY_ID,
		IMAGE_ZENGARDEN_ID,
		IMAGE_CACHED_ID,
		IMAGE_SELECTORSCREEN_ACHIEVEMENTS_TOP_BACKGROUND_ID,
		IMAGE_SELECTORSCREEN_MAIN_BACKGROUND_ID,
		IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_ID,
		IMAGE_SELECTORSCREEN_ACHIEVEMENTS_HOLE_CHINA_ID,
		IMAGE_SELECTORSCREEN_QUICKPLAY_BACKGROUND_ID,
		IMAGE_GOODIES_ID,
		IMAGE_QUICKPLAY_ID,
		IMAGE_ACHIEVEMENT_GNOME_ID,
		IMAGE_MINIGAMES_ID,
		IMAGE_SELECTORSCREEN_MOREGAMES_BACKGROUND_ID,
		IMAGE_CREDITS_ZOMBIENOTE_ID,
		IMAGE_LEADERBOARDSCREEN_BACKGROUND_ID,
		IMAGE_BLACKHOLE_ID,
		IMAGE_EDGE_OF_SPACE_ID,
		IMAGE_STARS_1_ID,
		IMAGE_STARS_2_ID,
		IMAGE_STARS_3_ID,
		IMAGE_STARS_4_ID,
		IMAGE_BACKGROUND_GREENHOUSE_ID,
		IMAGE_AQUARIUM1_ID,
		IMAGE_BACKGROUND_MUSHROOMGARDEN_ID,
		IMAGE_BACKGROUND1_ID,
		IMAGE_BACKGROUND1_GAMEOVER_INTERIOR_OVERLAY_ID,
		IMAGE_BACKGROUND1_GAMEOVER_MASK_ID,
		IMAGE_BACKGROUND1UNSODDED_ID,
		IMAGE_BACKGROUND2_ID,
		IMAGE_BACKGROUND2_GAMEOVER_INTERIOR_OVERLAY_ID,
		IMAGE_BACKGROUND2_GAMEOVER_MASK_ID,
		IMAGE_BACKGROUND3_ID,
		IMAGE_BACKGROUND3_GAMEOVER_INTERIOR_OVERLAY_ID,
		IMAGE_BACKGROUND3_GAMEOVER_MASK_ID,
		IMAGE_BACKGROUND4_ID,
		IMAGE_BACKGROUND4_GAMEOVER_INTERIOR_OVERLAY_ID,
		IMAGE_BACKGROUND4_GAMEOVER_MASK_ID,
		IMAGE_FOG_ID,
		IMAGE_BACKGROUND5_ID,
		IMAGE_BACKGROUND5_GAMEOVER_MASK_ID,
		IMAGE_BACKGROUND6BOSS_ID,
		IMAGE_BACKGROUND6_GAMEOVER_MASK_ID,
		IMAGE_STORE_BACKGROUND_ID,
		IMAGE_STORE_BACKGROUNDNIGHT_ID,
		IMAGE_STORE_CAR_ID,
		IMAGE_STORE_CAR_NIGHT_ID,
		IMAGE_STORE_CARCLOSED_ID,
		IMAGE_STORE_CARCLOSED_NIGHT_ID,
		IMAGE_STORE_HATCHBACKOPEN_ID,
		IMAGE_ZOMBIE_NOTE_ID,
		IMAGE_ZOMBIE_NOTE1_ID,
		IMAGE_ZOMBIE_NOTE2_ID,
		IMAGE_ZOMBIE_NOTE3_ID,
		IMAGE_ZOMBIE_NOTE4_ID,
		IMAGE_ZOMBIE_FINAL_NOTE_ID,
		IMAGE_ZOMBIE_NOTE_HELP_ID,
		RESOURCE_ID_MAX
	}
}
