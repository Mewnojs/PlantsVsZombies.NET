using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class GameConstants
	{
		public static void Init()
		{
			GameConstants.AchievementInfo = new Achievement[]
			{
				new Achievement(10223, "[ACHIEVEMENT_HOME_SECURITY_TITLE]", "[ACHIEVEMENT_HOME_SECURITY_DESCRIPTION]"),
				new Achievement(10224, "[ACHIEVEMENT_SPUDOW_TITLE]", "[ACHIEVEMENT_SPUDOW_DESCRIPTION]"),
				new Achievement(10225, "[ACHIEVEMENT_EXPLODONATOR_TITLE]", "[ACHIEVEMENT_EXPLODONATOR_DESCRIPTION]"),
				new Achievement(10226, "[ACHIEVEMENT_MORTICULTURALIST_TITLE]", "[ACHIEVEMENT_MORTICULTURALIST_DESCRIPTION]"),
				new Achievement(10227, "[ACHIEVEMENT_DONT_PEA_IN_POOL_TITLE]", "[ACHIEVEMENT_DONT_PEA_IN_POOL_DESCRIPTION]"),
				new Achievement(10228, "[ACHIEVEMENT_ROLL_SOME_HEADS_TITLE]", "[ACHIEVEMENT_ROLL_SOME_HEADS_DESCRIPTION]"),
				new Achievement(10229, "[ACHIEVEMENT_GROUNDED_TITLE]", "[ACHIEVEMENT_GROUNDED_DESCRIPTION]"),
				new Achievement(10230, "[ACHIEVEMENT_ZOMBOLOGIST_TITLE]", "[ACHIEVEMENT_ZOMBOLOGIST_DESCRIPTION]"),
				new Achievement(10231, "[ACHIEVEMENT_PENNY_PINCHER_TITLE]", "[ACHIEVEMENT_PENNY_PINCHER_DESCRIPTION]"),
				new Achievement(10232, "[ACHIEVEMENT_SUNNY_DAYS_TITLE]", "[ACHIEVEMENT_SUNNY_DAYS_DESCRIPTION]"),
				new Achievement(10233, "[ACHIEVEMENT_POPCORN_PARTY_TITLE]", "[ACHIEVEMENT_POPCORN_PARTY_DESCRIPTION]"),
				new Achievement(10234, "[ACHIEVEMENT_GOOD_MORNING_TITLE]", "[ACHIEVEMENT_GOOD_MORNING_DESCRIPTION]"),
				new Achievement(10235, "[ACHIEVEMENT_NO_FUNGUS_AMONG_US_TITLE]", "[ACHIEVEMENT_NO_FUNGUS_AMONG_US_DESCRIPTION]")
			};
			GameConstants.gLawnDialogColors = new int[,]
			{
				{
					255,
					255,
					255
				},
				{
					255,
					255,
					0
				},
				{
					255,
					255,
					255
				},
				{
					255,
					255,
					255
				},
				{
					255,
					255,
					255
				},
				{
					80,
					80,
					80
				},
				{
					255,
					255,
					255
				}
			};
			GameConstants.gLawnParticleArray = new ParticleParams[]
			{
				new ParticleParams(ParticleEffect.PARTICLE_MELONSPLASH, "particles/melonimpact"),
				new ParticleParams(ParticleEffect.PARTICLE_WINTERMELON, "particles/WinterMelonImpact"),
				new ParticleParams(ParticleEffect.PARTICLE_FUMECLOUD, "particles/FumeCloud"),
				new ParticleParams(ParticleEffect.PARTICLE_POPCORNSPLASH, "particles/PopcornSplash"),
				new ParticleParams(ParticleEffect.PARTICLE_POWIE, "particles/Powie"),
				new ParticleParams(ParticleEffect.PARTICLE_JACKEXPLODE, "particles/JackExplode"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_HEAD, "particles/ZombieHead"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_ARM, "particles/ZombieArm"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_TRAFFIC_CONE, "particles/ZombieTrafficCone"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_PAIL, "particles/ZombiePail"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_HELMET, "particles/ZombieHelmet"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_FLAG, "particles/ZombieFlag"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_DOOR, "particles/ZombieDoor"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_NEWSPAPER, "particles/ZombieNewspaper"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_HEADLIGHT, "particles/ZombieHeadLight"),
				new ParticleParams(ParticleEffect.PARTICLE_POW, "particles/Pow"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_POGO, "particles/ZombiePogo"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_NEWSPAPER_HEAD, "particles/ZombieNewspaperHead"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_BALLOON_HEAD, "particles/ZombieBalloonHead"),
				new ParticleParams(ParticleEffect.PARTICLE_SOD_ROLL, "particles/SodRoll"),
				new ParticleParams(ParticleEffect.PARTICLE_GRAVE_STONE_RISE, "particles/GraveStoneRise"),
				new ParticleParams(ParticleEffect.PARTICLE_PLANTING, "particles/Planting"),
				new ParticleParams(ParticleEffect.PARTICLE_PLANTING_POOL, "particles/PlantingPool"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_RISE, "particles/ZombieRise"),
				new ParticleParams(ParticleEffect.PARTICLE_GRAVE_BUSTER, "particles/GraveBuster"),
				new ParticleParams(ParticleEffect.PARTICLE_GRAVE_BUSTER_DIE, "particles/GraveBusterDie"),
				new ParticleParams(ParticleEffect.PARTICLE_POOL_SPLASH, "particles/PoolSplash"),
				new ParticleParams(ParticleEffect.PARTICLE_ICE_SPARKLE, "particles/IceSparkle"),
				new ParticleParams(ParticleEffect.PARTICLE_SEED_PACKET, "particles/SeedPacket"),
				new ParticleParams(ParticleEffect.PARTICLE_TALL_NUT_BLOCK, "particles/TallNutBlock"),
				new ParticleParams(ParticleEffect.PARTICLE_DOOM, "particles/Doom"),
				new ParticleParams(ParticleEffect.PARTICLE_DIGGER_RISE, "particles/DiggerRise"),
				new ParticleParams(ParticleEffect.PARTICLE_DIGGER_TUNNEL, "particles/DiggerTunnel"),
				new ParticleParams(ParticleEffect.PARTICLE_DANCER_RISE, "particles/DancerRise"),
				new ParticleParams(ParticleEffect.PARTICLE_POOL_SPARKLY, "particles/PoolSparkly"),
				new ParticleParams(ParticleEffect.PARTICLE_WALLNUT_EAT_SMALL, "particles/WallnutEatSmall"),
				new ParticleParams(ParticleEffect.PARTICLE_WALLNUT_EAT_LARGE, "particles/WallnutEatLarge"),
				new ParticleParams(ParticleEffect.PARTICLE_PEA_SPLAT, "particles/PeaSplat"),
				new ParticleParams(ParticleEffect.PARTICLE_BUTTER_SPLAT, "particles/ButterSplat"),
				new ParticleParams(ParticleEffect.PARTICLE_CABBAGE_SPLAT, "particles/CabbageSplat"),
				new ParticleParams(ParticleEffect.PARTICLE_PUFF_SPLAT, "particles/PuffSplat"),
				new ParticleParams(ParticleEffect.PARTICLE_STAR_SPLAT, "particles/StarSplat"),
				new ParticleParams(ParticleEffect.PARTICLE_ICE_TRAP, "particles/IceTrap"),
				new ParticleParams(ParticleEffect.PARTICLE_SNOWPEA_SPLAT, "particles/SnowPeaSplat"),
				new ParticleParams(ParticleEffect.PARTICLE_SNOWPEA_PUFF, "particles/SnowPeaPuff"),
				new ParticleParams(ParticleEffect.PARTICLE_SNOWPEA_TRAIL, "particles/SnowPeaTrail"),
				new ParticleParams(ParticleEffect.PARTICLE_LANTERN_SHINE, "particles/LanternShine"),
				new ParticleParams(ParticleEffect.PARTICLE_SEED_PACKET_PICKUP, "particles/Award"),
				new ParticleParams(ParticleEffect.PARTICLE_POTATO_MINE, "particles/PotatoMine"),
				new ParticleParams(ParticleEffect.PARTICLE_POTATO_MINE_RISE, "particles/PotatoMineRise"),
				new ParticleParams(ParticleEffect.PARTICLE_PUFFSHROOM_TRAIL, "particles/PuffShroomTrail"),
				new ParticleParams(ParticleEffect.PARTICLE_PUFFSHROOM_MUZZLE, "particles/PuffShroomMuzzle"),
				new ParticleParams(ParticleEffect.PARTICLE_SEED_PACKET_FLASH, "particles/SeedPacketFlash"),
				new ParticleParams(ParticleEffect.PARTICLE_WHACK_A_ZOMBIE_RISE, "particles/WhackAZombieRise"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_LADDER, "particles/ZombieLadder"),
				new ParticleParams(ParticleEffect.PARTICLE_UMBRELLA_REFLECT, "particles/UmbrellaReflect"),
				new ParticleParams(ParticleEffect.PARTICLE_SEED_PACKET_PICK, "particles/SeedPacketPick"),
				new ParticleParams(ParticleEffect.PARTICLE_ICE_TRAP_ZOMBIE, "particles/IceTrapZombie"),
				new ParticleParams(ParticleEffect.PARTICLE_ICE_TRAP_RELEASE, "particles/IceTrapRelease"),
				new ParticleParams(ParticleEffect.PARTICLE_ZAMBONI_SMOKE, "particles/ZamboniSmoke"),
				new ParticleParams(ParticleEffect.PARTICLE_GLOOMCLOUD, "particles/GloomCloud"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_POGO_HEAD, "particles/ZombiePogoHead"),
				new ParticleParams(ParticleEffect.PARTICLE_ZAMBONI_TIRE, "particles/ZamboniTire"),
				new ParticleParams(ParticleEffect.PARTICLE_ZAMBONI_EXPLOSION, "particles/ZamboniExplosion"),
				new ParticleParams(ParticleEffect.PARTICLE_ZAMBONI_EXPLOSION2, "particles/ZamboniExplosion2"),
				new ParticleParams(ParticleEffect.PARTICLE_CATAPULT_EXPLOSION, "particles/CatapultExplosion"),
				new ParticleParams(ParticleEffect.PARTICLE_MOWER_CLOUD, "particles/MowerCloud"),
				new ParticleParams(ParticleEffect.PARTICLE_BOSS_ICE_BALL, "particles/BossIceBallTrail"),
				new ParticleParams(ParticleEffect.PARTICLE_BLASTMARK, "particles/BlastMark"),
				new ParticleParams(ParticleEffect.PARTICLE_COIN_PICKUP_ARROW, "particles/CoinPickupArrow"),
				new ParticleParams(ParticleEffect.PARTICLE_PRESENT_PICKUP, "particles/PresentPickup"),
				new ParticleParams(ParticleEffect.PARTICLE_IMITATER_MORPH, "particles/ImitaterMorph"),
				new ParticleParams(ParticleEffect.PARTICLE_MOWERED_ZOMBIE_HEAD, "particles/MoweredZombieHead"),
				new ParticleParams(ParticleEffect.PARTICLE_MOWERED_ZOMBIE_ARM, "particles/MoweredZombieArm"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_HEAD_POOL, "particles/ZombieHeadPool"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_BOSS_FIREBALL, "particles/Zombie_boss_fireball"),
				new ParticleParams(ParticleEffect.PARTICLE_FIREBALL_DEATH, "particles/FireBallDeath"),
				new ParticleParams(ParticleEffect.PARTICLE_ICEBALL_DEATH, "particles/IceBallDeath"),
				new ParticleParams(ParticleEffect.PARTICLE_ICEBALL_TRAIL, "particles/Iceball_Trail"),
				new ParticleParams(ParticleEffect.PARTICLE_FIREBALL_TRAIL, "particles/Fireball_Trail"),
				new ParticleParams(ParticleEffect.PARTICLE_BOSS_EXPLOSION, "particles/BossExplosion"),
				new ParticleParams(ParticleEffect.PARTICLE_SCREEN_FLASH, "particles/ScreenFlash"),
				new ParticleParams(ParticleEffect.PARTICLE_TROPHY_SPARKLE, "particles/TrophySparkle"),
				new ParticleParams(ParticleEffect.PARTICLE_PORTAL_CIRCLE, "particles/PortalCircle"),
				new ParticleParams(ParticleEffect.PARTICLE_PORTAL_SQUARE, "particles/PortalSquare"),
				new ParticleParams(ParticleEffect.PARTICLE_POTTED_PLANT_GLOW, "particles/PottedPlantGlow"),
				new ParticleParams(ParticleEffect.PARTICLE_POTTED_WATER_PLANT_GLOW, "particles/PottedWaterPlantGlow"),
				new ParticleParams(ParticleEffect.PARTICLE_POTTED_ZEN_GLOW, "particles/PottedZenGlow"),
				new ParticleParams(ParticleEffect.PARTICLE_MIND_CONTROL, "particles/MindControl"),
				new ParticleParams(ParticleEffect.PARTICLE_VASE_SHATTER, "particles/VaseShatter"),
				new ParticleParams(ParticleEffect.PARTICLE_VASE_SHATTER_LEAF, "particles/VaseShatterLeaf"),
				new ParticleParams(ParticleEffect.PARTICLE_VASE_SHATTER_ZOMBIE, "particles/VaseShatterZombie"),
				new ParticleParams(ParticleEffect.PARTICLE_AWARD_PICKUP_ARROW, "particles/AwardPickupArrow"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_SEAWEED, "particles/Zombie_seaweed"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_MUSTACHE, "particles/ZombieMustache"),
				new ParticleParams(ParticleEffect.PARTICLE_ZOMBIE_FUTURE_GLASSES, "particles/ZombieFutureGlasses"),
				new ParticleParams(ParticleEffect.PARTICLE_PINATA, "particles/Pinata"),
				new ParticleParams(ParticleEffect.PARTICLE_DUST_SQUASH, "particles/Dust_Squash"),
				new ParticleParams(ParticleEffect.PARTICLE_DUST_FOOT, "particles/Dust_Foot"),
				new ParticleParams(ParticleEffect.PARTICLE_DAISY, "particles/Daisy"),
				new ParticleParams(ParticleEffect.PARTICLE_STARBURST, "particles/Starburst"),
				new ParticleParams(ParticleEffect.PARTICLE_UPSELL_ARROW, "particles/UpsellArrow")
			};
			GameConstants.gLawnReanimationArray = new ReanimationParams[]
			{
				new ReanimationParams(ReanimationType.REANIM_LOADBAR_SPROUT, "reanim/LoadBar_sprout", 1),
				new ReanimationParams(ReanimationType.REANIM_LOADBAR_ZOMBIEHEAD, "reanim/LoadBar_Zombiehead", 1),
				new ReanimationParams(ReanimationType.REANIM_SODROLL, "reanim/SodRoll", 0),
				new ReanimationParams(ReanimationType.REANIM_FINAL_WAVE, "reanim/FinalWave", 1),
				new ReanimationParams(ReanimationType.REANIM_PEASHOOTER, "reanim/PeaShooterSingle", 0),
				new ReanimationParams(ReanimationType.REANIM_WALLNUT, "reanim/Wallnut", 0),
				new ReanimationParams(ReanimationType.REANIM_LILYPAD, "reanim/LilyPad", 0),
				new ReanimationParams(ReanimationType.REANIM_SUNFLOWER, "reanim/SunFlower", 0),
				new ReanimationParams(ReanimationType.REANIM_LAWNMOWER, "reanim/LawnMower", 0),
				new ReanimationParams(ReanimationType.REANIM_READYSETPLANT, "reanim/StartReadySetPlant", 1),
				new ReanimationParams(ReanimationType.REANIM_CHERRYBOMB, "reanim/CherryBomb", 0),
				new ReanimationParams(ReanimationType.REANIM_SQUASH, "reanim/Squash", 0),
				new ReanimationParams(ReanimationType.REANIM_DOOMSHROOM, "reanim/DoomShroom", 0),
				new ReanimationParams(ReanimationType.REANIM_SNOWPEA, "reanim/SnowPea", 0),
				new ReanimationParams(ReanimationType.REANIM_REPEATER, "reanim/PeaShooter", 0),
				new ReanimationParams(ReanimationType.REANIM_SUNSHROOM, "reanim/SunShroom", 0),
				new ReanimationParams(ReanimationType.REANIM_TALLNUT, "reanim/Tallnut", 0),
				new ReanimationParams(ReanimationType.REANIM_FUMESHROOM, "reanim/FumeShroom", 0),
				new ReanimationParams(ReanimationType.REANIM_PUFFSHROOM, "reanim/PuffShroom", 0),
				new ReanimationParams(ReanimationType.REANIM_HYPNOSHROOM, "reanim/HypnoShroom", 0),
				new ReanimationParams(ReanimationType.REANIM_CHOMPER, "reanim/Chomper", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE, "reanim/Zombie", 0),
				new ReanimationParams(ReanimationType.REANIM_SUN, "reanim/Sun", 0),
				new ReanimationParams(ReanimationType.REANIM_POTATOMINE, "reanim/PotatoMine", 0),
				new ReanimationParams(ReanimationType.REANIM_SPIKEWEED, "reanim/Caltrop", 0),
				new ReanimationParams(ReanimationType.REANIM_SPIKEROCK, "reanim/SpikeRock", 0),
				new ReanimationParams(ReanimationType.REANIM_THREEPEATER, "reanim/ThreePeater", 0),
				new ReanimationParams(ReanimationType.REANIM_MARIGOLD, "reanim/Marigold", 0),
				new ReanimationParams(ReanimationType.REANIM_ICESHROOM, "reanim/IceShroom", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_FOOTBALL, "reanim/Zombie_football", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_NEWSPAPER, "reanim/Zombie_paper", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_ZAMBONI, "reanim/Zombie_zamboni", 0),
				new ReanimationParams(ReanimationType.REANIM_SPLASH, "reanim/splash", 0),
				new ReanimationParams(ReanimationType.REANIM_JALAPENO, "reanim/Jalapeno", 0),
				new ReanimationParams(ReanimationType.REANIM_JALAPENO_FIRE, "reanim/fire", 0),
				new ReanimationParams(ReanimationType.REANIM_COIN_SILVER, "reanim/Coin_silver", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_CHARRED, "reanim/Zombie_charred", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_CHARRED_IMP, "reanim/Zombie_charred_imp", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_CHARRED_DIGGER, "reanim/Zombie_charred_digger", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_CHARRED_ZAMBONI, "reanim/Zombie_charred_zamboni", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_CHARRED_CATAPULT, "reanim/Zombie_charred_catapult", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_CHARRED_GARGANTUAR, "reanim/Zombie_charred_gargantuar", 0),
				new ReanimationParams(ReanimationType.REANIM_SCRAREYSHROOM, "reanim/ScaredyShroom", 0),
				new ReanimationParams(ReanimationType.REANIM_PUMPKIN, "reanim/Pumpkin", 0),
				new ReanimationParams(ReanimationType.REANIM_PLANTERN, "reanim/Plantern", 0),
				new ReanimationParams(ReanimationType.REANIM_TORCHWOOD, "reanim/Torchwood", 0),
				new ReanimationParams(ReanimationType.REANIM_SPLITPEA, "reanim/SplitPea", 0),
				new ReanimationParams(ReanimationType.REANIM_SEASHROOM, "reanim/SeaShroom", 0),
				new ReanimationParams(ReanimationType.REANIM_BLOVER, "reanim/Blover", 0),
				new ReanimationParams(ReanimationType.REANIM_FLOWER_POT, "reanim/Pot", 0),
				new ReanimationParams(ReanimationType.REANIM_CACTUS, "reanim/Cactus", 0),
				new ReanimationParams(ReanimationType.REANIM_DISCO, "reanim/Zombie_disco", 0),
				new ReanimationParams(ReanimationType.REANIM_TANGLEKELP, "reanim/Tanglekelp", 0),
				new ReanimationParams(ReanimationType.REANIM_STARFRUIT, "reanim/Starfruit", 0),
				new ReanimationParams(ReanimationType.REANIM_POLEVAULTER, "reanim/Zombie_polevaulter", 0),
				new ReanimationParams(ReanimationType.REANIM_BALLOON, "reanim/Zombie_balloon", 0),
				new ReanimationParams(ReanimationType.REANIM_GARGANTUAR, "reanim/Zombie_gargantuar", 0),
				new ReanimationParams(ReanimationType.REANIM_IMP, "reanim/Zombie_imp", 0),
				new ReanimationParams(ReanimationType.REANIM_DIGGER, "reanim/Zombie_digger", 0),
				new ReanimationParams(ReanimationType.REANIM_DIGGER_DIRT, "reanim/Digger_rising_dirt", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_DOLPHINRIDER, "reanim/Zombie_dolphinrider", 0),
				new ReanimationParams(ReanimationType.REANIM_POGO, "reanim/Zombie_pogo", 0),
				new ReanimationParams(ReanimationType.REANIM_BACKUP_DANCER, "reanim/Zombie_backup", 0),
				new ReanimationParams(ReanimationType.REANIM_BOBSLED, "reanim/Zombie_bobsled", 0),
				new ReanimationParams(ReanimationType.REANIM_JACKINTHEBOX, "reanim/Zombie_jackbox", 0),
				new ReanimationParams(ReanimationType.REANIM_SNORKEL, "reanim/Zombie_snorkle", 0),
				new ReanimationParams(ReanimationType.REANIM_BUNGEE, "reanim/Zombie_bungi", 0),
				new ReanimationParams(ReanimationType.REANIM_CATAPULT, "reanim/Zombie_catapult", 0),
				new ReanimationParams(ReanimationType.REANIM_LADDER, "reanim/Zombie_ladder", 0),
				new ReanimationParams(ReanimationType.REANIM_PUFF, "reanim/Puff", 0),
				new ReanimationParams(ReanimationType.REANIM_SLEEPING, "reanim/Z", 0),
				new ReanimationParams(ReanimationType.REANIM_GRAVE_BUSTER, "reanim/Gravebuster", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIES_WON, "reanim/ZombiesWon", 1),
				new ReanimationParams(ReanimationType.REANIM_MAGNETSHROOM, "reanim/Magnetshroom", 0),
				new ReanimationParams(ReanimationType.REANIM_BOSS, "reanim/Zombie_boss", 1),
				new ReanimationParams(ReanimationType.REANIM_CABBAGEPULT, "reanim/Cabbagepult", 0),
				new ReanimationParams(ReanimationType.REANIM_KERNELPULT, "reanim/Cornpult", 0),
				new ReanimationParams(ReanimationType.REANIM_MELONPULT, "reanim/Melonpult", 0),
				new ReanimationParams(ReanimationType.REANIM_COFFEEBEAN, "reanim/Coffeebean", 1),
				new ReanimationParams(ReanimationType.REANIM_UMBRELLALEAF, "reanim/Umbrellaleaf", 0),
				new ReanimationParams(ReanimationType.REANIM_GATLINGPEA, "reanim/GatlingPea", 0),
				new ReanimationParams(ReanimationType.REANIM_CATTAIL, "reanim/Cattail", 0),
				new ReanimationParams(ReanimationType.REANIM_GLOOMSHROOM, "reanim/GloomShroom", 0),
				new ReanimationParams(ReanimationType.REANIM_BOSS_ICEBALL, "reanim/Zombie_boss_iceball", 1),
				new ReanimationParams(ReanimationType.REANIM_BOSS_FIREBALL, "reanim/Zombie_boss_fireball", 1),
				new ReanimationParams(ReanimationType.REANIM_COBCANNON, "reanim/CobCannon", 0),
				new ReanimationParams(ReanimationType.REANIM_GARLIC, "reanim/Garlic", 0),
				new ReanimationParams(ReanimationType.REANIM_GOLD_MAGNET, "reanim/GoldMagnet", 0),
				new ReanimationParams(ReanimationType.REANIM_WINTER_MELON, "reanim/WinterMelon", 0),
				new ReanimationParams(ReanimationType.REANIM_TWIN_SUNFLOWER, "reanim/TwinSunflower", 0),
				new ReanimationParams(ReanimationType.REANIM_POOL_CLEANER, "reanim/PoolCleaner", 0),
				new ReanimationParams(ReanimationType.REANIM_ROOF_CLEANER, "reanim/RoofCleaner", 0),
				new ReanimationParams(ReanimationType.REANIM_FIRE_PEA, "reanim/FirePea", 0),
				new ReanimationParams(ReanimationType.REANIM_IMITATER, "reanim/Imitater", 0),
				new ReanimationParams(ReanimationType.REANIM_YETI, "reanim/Zombie_yeti", 0),
				new ReanimationParams(ReanimationType.REANIM_BOSS_DRIVER, "reanim/Zombie_Boss_driver", 0),
				new ReanimationParams(ReanimationType.REANIM_LAWN_MOWERED_ZOMBIE, "reanim/LawnMoweredZombie", 0),
				new ReanimationParams(ReanimationType.REANIM_CRAZY_DAVE, "reanim/CrazyDave", 1),
				new ReanimationParams(ReanimationType.REANIM_TEXT_FADE_ON, "reanim/TextFadeOn", 0),
				new ReanimationParams(ReanimationType.REANIM_HAMMER, "reanim/Hammer", 0),
				new ReanimationParams(ReanimationType.REANIM_SLOT_MACHINE_HANDLE, "reanim/SlotMachine", 0),
				new ReanimationParams(ReanimationType.REANIM_SELECTOR_SCREEN, "reanim/SelectorScreen", 3),
				new ReanimationParams(ReanimationType.REANIM_PORTAL_CIRCLE, "reanim/Portal_Circle", 0),
				new ReanimationParams(ReanimationType.REANIM_PORTAL_SQUARE, "reanim/Portal_Square", 0),
				new ReanimationParams(ReanimationType.REANIM_ZENGARDEN_SPROUT, "reanim/ZenGarden_sprout", 0),
				new ReanimationParams(ReanimationType.REANIM_ZENGARDEN_WATERINGCAN, "reanim/ZenGarden_wateringcan", 1),
				new ReanimationParams(ReanimationType.REANIM_ZENGARDEN_FERTILIZER, "reanim/ZenGarden_fertilizer", 1),
				new ReanimationParams(ReanimationType.REANIM_ZENGARDEN_BUGSPRAY, "reanim/ZenGarden_bugspray", 1),
				new ReanimationParams(ReanimationType.REANIM_ZENGARDEN_PHONOGRAPH, "reanim/ZenGarden_phonograph", 1),
				new ReanimationParams(ReanimationType.REANIM_DIAMOND, "reanim/Diamond", 0),
				new ReanimationParams(ReanimationType.REANIM_STINKY, "reanim/Stinky", 0),
				new ReanimationParams(ReanimationType.REANIM_RAKE, "reanim/Rake", 0),
				new ReanimationParams(ReanimationType.REANIM_RAIN_CIRCLE, "reanim/Rain_circle", 0),
				new ReanimationParams(ReanimationType.REANIM_RAIN_SPLASH, "reanim/Rain_splash", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_SURPRISE, "reanim/Zombie_surprise", 0),
				new ReanimationParams(ReanimationType.REANIM_COIN_GOLD, "reanim/Coin_gold", 0),
				new ReanimationParams(ReanimationType.REANIM_ZOMBIE_FLAGPOLE, "reanim/Zombie_flagpole"),
				new ReanimationParams(ReanimationType.REANIM_WOODSIGN, "reanim/woodsign"),
				new ReanimationParams(ReanimationType.REANIM_ASTRONAUT, "reanim/astronaut")
			};
			GameConstants.gLawnTrailArray = new TrailParams[]
			{
				new TrailParams(TrailType.TRAIL_ICE, "particles/IceTrail")
			};
			GameConstants.gLawnStringFormats = new TodStringListFormat[]
			{
				new TodStringListFormat("NORMAL", null, new SexyColor(40, 50, 90), 0, 0U),
				new TodStringListFormat("FLAVOR", null, new SexyColor(143, 67, 27), 0, 1U),
				new TodStringListFormat("KEYWORD", null, new SexyColor(143, 67, 27), 0, 0U),
				new TodStringListFormat("NOCTURNAL", null, new SexyColor(136, 50, 170), 0, 0U),
				new TodStringListFormat("AQUATIC", null, new SexyColor(10, 151, 216), 0, 0U),
				new TodStringListFormat("STAT", null, new SexyColor(204, 36, 29), 0, 0U),
				new TodStringListFormat("METAL", null, new SexyColor(204, 36, 29), 0, 2U),
				new TodStringListFormat("KEYMETAL", null, new SexyColor(143, 67, 27), 0, 2U),
				new TodStringListFormat("SHORTLINE", null, new SexyColor(0, 0, 0, 0), 0, 0U),
				new TodStringListFormat("EXTRASHORTLINE", null, new SexyColor(0, 0, 0, 0), -14, 0U),
				new TodStringListFormat("CREDITS1", null, new SexyColor(0, 0, 0, 0), 3, 0U),
				new TodStringListFormat("CREDITS2", null, new SexyColor(0, 0, 0, 0), 2, 0U)
			};
			GameConstants.gLawnStringFormatCount = GameConstants.gLawnStringFormats.Length;
		}

		public static int GetRectOverlap(TRect rect1, TRect rect2)
		{
			int num;
			int aX2;
			int num2;
			if (rect1.mX < rect2.mX)
			{
				int aX = rect1.mX;
				num = rect1.mX + rect1.mWidth;
				aX2 = rect2.mX;
				num2 = rect2.mX + rect2.mWidth;
			}
			else
			{
				int aX3 = rect2.mX;
				num = rect2.mX + rect2.mWidth;
				aX2 = rect1.mX;
				num2 = rect1.mX + rect1.mWidth;
			}
			if (num <= aX2)
			{
				return num - aX2;
			}
			if (num <= num2)
			{
				return num - aX2;
			}
			return num2 - aX2;
		}

		public static bool GetCircleRectOverlap(int theCircleX, int theCircleY, int theRadius, TRect theRect)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = 0;
			if (theCircleX < theRect.mX)
			{
				flag = true;
				num = theRect.mX - theCircleX;
			}
			else if (theCircleX > theRect.mX + theRect.mWidth)
			{
				flag = true;
				num = theCircleX - theRect.mX - theRect.mWidth;
			}
			if (theCircleY < theRect.mY)
			{
				flag2 = true;
				num2 = theRect.mY - theCircleY;
			}
			else if (theCircleY > theRect.mY + theRect.mHeight)
			{
				flag2 = true;
				num2 = theCircleY - theRect.mY - theRect.mHeight;
			}
			if (!flag2 && !flag)
			{
				return true;
			}
			if (flag && flag2)
			{
				int num3 = num * num + num2 * num2;
				return num3 <= theRadius * theRadius;
			}
			if (flag)
			{
				return num <= theRadius;
			}
			return num2 <= theRadius;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static GameConstants()
		{
			ZombieAllowedLevels[] array = new ZombieAllowedLevels[33];
			array[0] = new ZombieAllowedLevels(ZombieType.ZOMBIE_NORMAL, new int[]
			{
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1
			});
			array[1] = new ZombieAllowedLevels(ZombieType.ZOMBIE_FLAG, new int[]
			{
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1
			});
			array[2] = new ZombieAllowedLevels(ZombieType.ZOMBIE_TRAFFIC_CONE, new int[]
			{
				0,
				0,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				0,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1
			});
			array[3] = new ZombieAllowedLevels(ZombieType.ZOMBIE_POLEVAULTER, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				1,
				1,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[4] = new ZombieAllowedLevels(ZombieType.ZOMBIE_PAIL, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				0,
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				1,
				0,
				0,
				1,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				1,
				1,
				0,
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				1,
				1
			});
			array[5] = new ZombieAllowedLevels(ZombieType.ZOMBIE_NEWSPAPER, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[6] = new ZombieAllowedLevels(ZombieType.ZOMBIE_DOOR, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				1,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[7] = new ZombieAllowedLevels(ZombieType.ZOMBIE_FOOTBALL, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				1,
				0,
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[8] = new ZombieAllowedLevels(ZombieType.ZOMBIE_DANCER, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[9] = new ZombieAllowedLevels(ZombieType.ZOMBIE_BACKUP_DANCER, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			ZombieAllowedLevels[] array2 = array;
			int num = 10;
			ZombieType aZombieType = ZombieType.ZOMBIE_DUCKY_TUBE;
			int[] levels = new int[50];
			array2[num] = new ZombieAllowedLevels(aZombieType, levels);
			array[11] = new ZombieAllowedLevels(ZombieType.ZOMBIE_SNORKEL, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				0,
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[12] = new ZombieAllowedLevels(ZombieType.ZOMBIE_ZAMBONI, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[13] = new ZombieAllowedLevels(ZombieType.ZOMBIE_BOBSLED, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[14] = new ZombieAllowedLevels(ZombieType.ZOMBIE_DOLPHIN_RIDER, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[15] = new ZombieAllowedLevels(ZombieType.ZOMBIE_JACK_IN_THE_BOX, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1
			});
			array[16] = new ZombieAllowedLevels(ZombieType.ZOMBIE_BALLOON, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[17] = new ZombieAllowedLevels(ZombieType.ZOMBIE_DIGGER, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
			array[18] = new ZombieAllowedLevels(ZombieType.ZOMBIE_POGO, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0
			});
			ZombieAllowedLevels[] array3 = array;
			int num2 = 19;
			ZombieType aZombieType2 = ZombieType.ZOMBIE_YETI;
			int[] levels2 = new int[50];
			array3[num2] = new ZombieAllowedLevels(aZombieType2, levels2);
			array[20] = new ZombieAllowedLevels(ZombieType.ZOMBIE_BUNGEE, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				1,
				0,
				1,
				1
			});
			array[21] = new ZombieAllowedLevels(ZombieType.ZOMBIE_LADDER, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				0,
				1,
				0,
				1,
				1
			});
			array[22] = new ZombieAllowedLevels(ZombieType.ZOMBIE_CATAPULT, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				1,
				1
			});
			array[23] = new ZombieAllowedLevels(ZombieType.ZOMBIE_GARGANTUAR, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1
			});
			array[24] = new ZombieAllowedLevels(ZombieType.ZOMBIE_IMP, new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1
			});
			ZombieAllowedLevels[] array4 = array;
			int num3 = 25;
			ZombieType aZombieType3 = ZombieType.ZOMBIE_BOSS;
			int[] levels3 = new int[50];
			array4[num3] = new ZombieAllowedLevels(aZombieType3, levels3);
			ZombieAllowedLevels[] array5 = array;
			int num4 = 26;
			ZombieType aZombieType4 = ZombieType.ZOMBIE_PEA_HEAD;
			int[] levels4 = new int[50];
			array5[num4] = new ZombieAllowedLevels(aZombieType4, levels4);
			ZombieAllowedLevels[] array6 = array;
			int num5 = 27;
			ZombieType aZombieType5 = ZombieType.ZOMBIE_WALLNUT_HEAD;
			int[] levels5 = new int[50];
			array6[num5] = new ZombieAllowedLevels(aZombieType5, levels5);
			ZombieAllowedLevels[] array7 = array;
			int num6 = 28;
			ZombieType aZombieType6 = ZombieType.ZOMBIE_JALAPENO_HEAD;
			int[] levels6 = new int[50];
			array7[num6] = new ZombieAllowedLevels(aZombieType6, levels6);
			ZombieAllowedLevels[] array8 = array;
			int num7 = 29;
			ZombieType aZombieType7 = ZombieType.ZOMBIE_GATLING_HEAD;
			int[] levels7 = new int[50];
			array8[num7] = new ZombieAllowedLevels(aZombieType7, levels7);
			ZombieAllowedLevels[] array9 = array;
			int num8 = 30;
			ZombieType aZombieType8 = ZombieType.ZOMBIE_SQUASH_HEAD;
			int[] levels8 = new int[50];
			array9[num8] = new ZombieAllowedLevels(aZombieType8, levels8);
			ZombieAllowedLevels[] array10 = array;
			int num9 = 31;
			ZombieType aZombieType9 = ZombieType.ZOMBIE_TALLNUT_HEAD;
			int[] levels9 = new int[50];
			array10[num9] = new ZombieAllowedLevels(aZombieType9, levels9);
			ZombieAllowedLevels[] array11 = array;
			int num10 = 32;
			ZombieType aZombieType10 = ZombieType.ZOMBIE_REDEYE_GARGANTUAR;
			int[] levels10 = new int[50];
			array11[num10] = new ZombieAllowedLevels(aZombieType10, levels10);
			GameConstants.gZombieAllowedLevels = array;
			GameConstants.gPlantDefs = new PlantDefinition[]
			{
				new PlantDefinition(SeedType.SEED_PEASHOOTER, null, ReanimationType.REANIM_PEASHOOTER, 0, 100, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "PEASHOOTER"),
				new PlantDefinition(SeedType.SEED_SUNFLOWER, null, ReanimationType.REANIM_SUNFLOWER, 1, 50, 750, PlantSubClass.SUBCLASS_NORMAL, 2500, "SUNFLOWER"),
				new PlantDefinition(SeedType.SEED_CHERRYBOMB, null, ReanimationType.REANIM_CHERRYBOMB, 3, 150, 5000, PlantSubClass.SUBCLASS_NORMAL, 0, "CHERRY_BOMB"),
				new PlantDefinition(SeedType.SEED_WALLNUT, null, ReanimationType.REANIM_WALLNUT, 2, 50, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "WALL_NUT"),
				new PlantDefinition(SeedType.SEED_POTATOMINE, null, ReanimationType.REANIM_POTATOMINE, 37, 25, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "POTATO_MINE"),
				new PlantDefinition(SeedType.SEED_SNOWPEA, null, ReanimationType.REANIM_SNOWPEA, 4, 175, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "SNOW_PEA"),
				new PlantDefinition(SeedType.SEED_CHOMPER, null, ReanimationType.REANIM_CHOMPER, 31, 150, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "CHOMPER"),
				new PlantDefinition(SeedType.SEED_REPEATER, null, ReanimationType.REANIM_REPEATER, 5, 200, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "REPEATER"),
				new PlantDefinition(SeedType.SEED_PUFFSHROOM, null, ReanimationType.REANIM_PUFFSHROOM, 6, 0, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "PUFF_SHROOM"),
				new PlantDefinition(SeedType.SEED_SUNSHROOM, null, ReanimationType.REANIM_SUNSHROOM, 7, 25, 750, PlantSubClass.SUBCLASS_NORMAL, 2500, "SUN_SHROOM"),
				new PlantDefinition(SeedType.SEED_FUMESHROOM, null, ReanimationType.REANIM_FUMESHROOM, 9, 75, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "FUME_SHROOM"),
				new PlantDefinition(SeedType.SEED_GRAVEBUSTER, null, ReanimationType.REANIM_GRAVE_BUSTER, 40, 75, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "GRAVE_BUSTER"),
				new PlantDefinition(SeedType.SEED_HYPNOSHROOM, null, ReanimationType.REANIM_HYPNOSHROOM, 10, 75, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "HYPNO_SHROOM"),
				new PlantDefinition(SeedType.SEED_SCAREDYSHROOM, null, ReanimationType.REANIM_SCRAREYSHROOM, 33, 25, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "SCAREDY_SHROOM"),
				new PlantDefinition(SeedType.SEED_ICESHROOM, null, ReanimationType.REANIM_ICESHROOM, 36, 75, 5000, PlantSubClass.SUBCLASS_NORMAL, 0, "ICE_SHROOM"),
				new PlantDefinition(SeedType.SEED_DOOMSHROOM, null, ReanimationType.REANIM_DOOMSHROOM, 20, 125, 5000, PlantSubClass.SUBCLASS_NORMAL, 0, "DOOM_SHROOM"),
				new PlantDefinition(SeedType.SEED_LILYPAD, null, ReanimationType.REANIM_LILYPAD, 19, 25, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "LILY_PAD"),
				new PlantDefinition(SeedType.SEED_SQUASH, null, ReanimationType.REANIM_SQUASH, 21, 50, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "SQUASH"),
				new PlantDefinition(SeedType.SEED_THREEPEATER, null, ReanimationType.REANIM_THREEPEATER, 12, 325, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "THREEPEATER"),
				new PlantDefinition(SeedType.SEED_TANGLEKELP, null, ReanimationType.REANIM_TANGLEKELP, 17, 25, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "TANGLE_KELP"),
				new PlantDefinition(SeedType.SEED_JALAPENO, null, ReanimationType.REANIM_JALAPENO, 11, 125, 5000, PlantSubClass.SUBCLASS_NORMAL, 0, "JALAPENO"),
				new PlantDefinition(SeedType.SEED_SPIKEWEED, null, ReanimationType.REANIM_SPIKEWEED, 22, 100, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "SPIKEWEED"),
				new PlantDefinition(SeedType.SEED_TORCHWOOD, null, ReanimationType.REANIM_TORCHWOOD, 29, 175, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "TORCHWOOD"),
				new PlantDefinition(SeedType.SEED_TALLNUT, null, ReanimationType.REANIM_TALLNUT, 28, 125, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "TALL_NUT"),
				new PlantDefinition(SeedType.SEED_SEASHROOM, null, ReanimationType.REANIM_SEASHROOM, 39, 0, 3000, PlantSubClass.SUBCLASS_SHOOTER, 150, "SEA_SHROOM"),
				new PlantDefinition(SeedType.SEED_PLANTERN, null, ReanimationType.REANIM_PLANTERN, 38, 25, 3000, PlantSubClass.SUBCLASS_NORMAL, 2500, "PLANTERN"),
				new PlantDefinition(SeedType.SEED_CACTUS, null, ReanimationType.REANIM_CACTUS, 15, 125, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "CACTUS"),
				new PlantDefinition(SeedType.SEED_BLOVER, null, ReanimationType.REANIM_BLOVER, 18, 100, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "BLOVER"),
				new PlantDefinition(SeedType.SEED_SPLITPEA, null, ReanimationType.REANIM_SPLITPEA, 32, 125, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "SPLIT_PEA"),
				new PlantDefinition(SeedType.SEED_STARFRUIT, null, ReanimationType.REANIM_STARFRUIT, 30, 125, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "STARFRUIT"),
				new PlantDefinition(SeedType.SEED_PUMPKINSHELL, null, ReanimationType.REANIM_PUMPKIN, 25, 125, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "PUMPKIN"),
				new PlantDefinition(SeedType.SEED_MAGNETSHROOM, null, ReanimationType.REANIM_MAGNETSHROOM, 35, 100, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "MAGNET_SHROOM"),
				new PlantDefinition(SeedType.SEED_CABBAGEPULT, null, ReanimationType.REANIM_CABBAGEPULT, 13, 100, 750, PlantSubClass.SUBCLASS_SHOOTER, 300, "CABBAGE_PULT"),
				new PlantDefinition(SeedType.SEED_FLOWERPOT, null, ReanimationType.REANIM_FLOWER_POT, 33, 25, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "FLOWER_POT"),
				new PlantDefinition(SeedType.SEED_KERNELPULT, null, ReanimationType.REANIM_KERNELPULT, 13, 100, 750, PlantSubClass.SUBCLASS_SHOOTER, 300, "KERNEL_PULT"),
				new PlantDefinition(SeedType.SEED_INSTANT_COFFEE, null, ReanimationType.REANIM_COFFEEBEAN, 33, 75, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "COFFEE_BEAN"),
				new PlantDefinition(SeedType.SEED_GARLIC, null, ReanimationType.REANIM_GARLIC, 8, 50, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "GARLIC"),
				new PlantDefinition(SeedType.SEED_UMBRELLA, null, ReanimationType.REANIM_UMBRELLALEAF, 23, 100, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "UMBRELLA_LEAF"),
				new PlantDefinition(SeedType.SEED_MARIGOLD, null, ReanimationType.REANIM_MARIGOLD, 24, 50, 3000, PlantSubClass.SUBCLASS_NORMAL, 2500, "MARIGOLD"),
				new PlantDefinition(SeedType.SEED_MELONPULT, null, ReanimationType.REANIM_MELONPULT, 14, 300, 750, PlantSubClass.SUBCLASS_SHOOTER, 300, "MELON_PULT"),
				new PlantDefinition(SeedType.SEED_GATLINGPEA, null, ReanimationType.REANIM_GATLINGPEA, 5, 250, 5000, PlantSubClass.SUBCLASS_SHOOTER, 150, "GATLING_PEA"),
				new PlantDefinition(SeedType.SEED_TWINSUNFLOWER, null, ReanimationType.REANIM_TWIN_SUNFLOWER, 1, 150, 5000, PlantSubClass.SUBCLASS_NORMAL, 2500, "TWIN_SUNFLOWER"),
				new PlantDefinition(SeedType.SEED_GLOOMSHROOM, null, ReanimationType.REANIM_GLOOMSHROOM, 27, 150, 5000, PlantSubClass.SUBCLASS_SHOOTER, 200, "GLOOM_SHROOM"),
				new PlantDefinition(SeedType.SEED_CATTAIL, null, ReanimationType.REANIM_CATTAIL, 27, 225, 5000, PlantSubClass.SUBCLASS_SHOOTER, 150, "CATTAIL"),
				new PlantDefinition(SeedType.SEED_WINTERMELON, null, ReanimationType.REANIM_WINTER_MELON, 27, 200, 5000, PlantSubClass.SUBCLASS_SHOOTER, 300, "WINTER_MELON"),
				new PlantDefinition(SeedType.SEED_GOLD_MAGNET, null, ReanimationType.REANIM_GOLD_MAGNET, 27, 50, 5000, PlantSubClass.SUBCLASS_NORMAL, 0, "GOLD_MAGNET"),
				new PlantDefinition(SeedType.SEED_SPIKEROCK, null, ReanimationType.REANIM_SPIKEROCK, 27, 125, 5000, PlantSubClass.SUBCLASS_NORMAL, 0, "SPIKEROCK"),
				new PlantDefinition(SeedType.SEED_COBCANNON, null, ReanimationType.REANIM_COBCANNON, 16, 500, 5000, PlantSubClass.SUBCLASS_NORMAL, 600, "COB_CANNON"),
				new PlantDefinition(SeedType.SEED_IMITATER, null, ReanimationType.REANIM_IMITATER, 33, 0, 750, PlantSubClass.SUBCLASS_NORMAL, 0, "IMITATER"),
				new PlantDefinition(SeedType.SEED_EXPLODE_O_NUT, null, ReanimationType.REANIM_WALLNUT, 2, 0, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "EXPLODE_O_NUT"),
				new PlantDefinition(SeedType.SEED_GIANT_WALLNUT, null, ReanimationType.REANIM_WALLNUT, 2, 0, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "GIANT_WALLNUT"),
				new PlantDefinition(SeedType.SEED_SPROUT, null, ReanimationType.REANIM_NONE, 33, 0, 3000, PlantSubClass.SUBCLASS_NORMAL, 0, "SPROUT"),
				new PlantDefinition(SeedType.SEED_LEFTPEATER, null, ReanimationType.REANIM_REPEATER, 5, 200, 750, PlantSubClass.SUBCLASS_SHOOTER, 150, "REPEATER")
			};
			GameConstants.NUM_BACKUP_DANCERS = 4;
			GameConstants.MAX_ZOMBIE_FOLLOWERS = 4;
			GameConstants.ZOMBIE_WAVE_DEBUG = -1;
			GameConstants.ZOMBIE_WAVE_CUTSCENE = -2;
			GameConstants.ZOMBIE_WAVE_UI = -3;
			GameConstants.ZOMBIE_WAVE_WINNER = -4;
			GameConstants.RENDER_GROUP_SHIELD = 1;
			GameConstants.RENDER_GROUP_ARMS = 2;
			GameConstants.RENDER_GROUP_OVER_SHIELD = 3;
			GameConstants.RENDER_GROUP_BOSS_BACK_LEG = 4;
			GameConstants.RENDER_GROUP_BOSS_FRONT_LEG = 5;
			GameConstants.RENDER_GROUP_BOSS_BACK_ARM = 6;
			GameConstants.RENDER_GROUP_BOSS_FIREBALL_ADDITIVE = 7;
			GameConstants.RENDER_GROUP_BOSS_FIREBALL_TOP = 8;
			GameConstants.CHILLED_SPEED_FACTOR = 0.4f;
			GameConstants.ZOMBIE_LIMP_SPEED_FACTOR = 2;
			GameConstants.BUNGEE_ZOMBIE_HEIGHT = 3000;
			GameConstants.DOG_WALKING_DISTANCE = 31;
			GameConstants.POGO_BOUNCE_TIME = 80;
			GameConstants.DOLPHIN_JUMP_TIME = 120;
			GameConstants.THOWN_ZOMBIE_GRAVITY = 0.05f;
			GameConstants.TICKS_BETWEEN_EATS = 4;
			GameConstants.CLIP_HEIGHT_OFF = -200f;
			GameConstants.CLIP_HEIGHT_LIMIT = -100f;
			GameConstants.ZOMBIE_MINDCONTROLLED_COLOR = new SexyColor(128, 64, 192);
			GameConstants.ZOMBIE_BLINK_RATE = 400;
			GameConstants.BOSS_FLASH_HEALTH_FRACTION = 10;
			GameConstants.ZOMBIE_WALK_IN_FRONT_DOOR_Y = 290f;
			GameConstants.BOBSLED_CRASH_TIME = 150;
			GameConstants.YUCKI_PAUSE_TIME = 69;
			GameConstants.YUCKI_SHORT_PAUSE_TIME = 21;
			GameConstants.YUCKI_HOLD_TIME = GameConstants.YUCKI_PAUSE_TIME + 99;
			GameConstants.YUCKI_WALK_TIME = GameConstants.YUCKI_HOLD_TIME + 99;
			GameConstants.BOSS_BALL_OFFSET_Y = -90f;
			GameConstants.gZombieDefs = new ZombieDefinition[]
			{
				new ZombieDefinition(ZombieType.ZOMBIE_NORMAL, ReanimationType.REANIM_ZOMBIE, 1, 1, 1, 4000, "ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_FLAG, ReanimationType.REANIM_ZOMBIE, 1, 1, 1, 0, "FLAG_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_TRAFFIC_CONE, ReanimationType.REANIM_ZOMBIE, 2, 3, 1, 4000, "CONEHEAD_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_POLEVAULTER, ReanimationType.REANIM_POLEVAULTER, 2, 6, 5, 2000, "POLE_VAULTING_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_PAIL, ReanimationType.REANIM_ZOMBIE, 4, 8, 1, 3000, "BUCKETHEAD_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_NEWSPAPER, ReanimationType.REANIM_ZOMBIE_NEWSPAPER, 2, 11, 1, 1000, "NEWSPAPER_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_DOOR, ReanimationType.REANIM_ZOMBIE, 4, 13, 5, 3500, "SCREEN_DOOR_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_FOOTBALL, ReanimationType.REANIM_ZOMBIE_FOOTBALL, 7, 16, 5, 2000, "FOOTBALL_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_DANCER, ReanimationType.REANIM_DISCO, 5, 18, 5, 1000, "DANCING_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_BACKUP_DANCER, ReanimationType.REANIM_BACKUP_DANCER, 1, 18, 1, 0, "BACKUP_DANCER"),
				new ZombieDefinition(ZombieType.ZOMBIE_DUCKY_TUBE, ReanimationType.REANIM_ZOMBIE, 1, 21, 5, 0, "DUCKY_TUBE_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_SNORKEL, ReanimationType.REANIM_SNORKEL, 3, 23, 10, 2000, "SNORKEL_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_ZAMBONI, ReanimationType.REANIM_ZOMBIE_ZAMBONI, 7, 26, 10, 2000, "ZOMBONI"),
				new ZombieDefinition(ZombieType.ZOMBIE_BOBSLED, ReanimationType.REANIM_BOBSLED, 3, 26, 10, 2000, "ZOMBIE_BOBSLED_TEAM"),
				new ZombieDefinition(ZombieType.ZOMBIE_DOLPHIN_RIDER, ReanimationType.REANIM_ZOMBIE_DOLPHINRIDER, 3, 28, 10, 1500, "DOLPHIN_RIDER_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_JACK_IN_THE_BOX, ReanimationType.REANIM_JACKINTHEBOX, 3, 31, 10, 1000, "JACK_IN_THE_BOX_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_BALLOON, ReanimationType.REANIM_BALLOON, 2, 33, 10, 2000, "BALLOON_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_DIGGER, ReanimationType.REANIM_DIGGER, 4, 36, 10, 1000, "DIGGER_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_POGO, ReanimationType.REANIM_POGO, 4, 38, 10, 1000, "POGO_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_YETI, ReanimationType.REANIM_YETI, 4, 40, 1, 1, "ZOMBIE_YETI"),
				new ZombieDefinition(ZombieType.ZOMBIE_BUNGEE, ReanimationType.REANIM_BUNGEE, 3, 41, 10, 1000, "BUNGEE_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_LADDER, ReanimationType.REANIM_LADDER, 4, 43, 10, 1000, "LADDER_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_CATAPULT, ReanimationType.REANIM_CATAPULT, 5, 46, 10, 1500, "CATAPULT_ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_GARGANTUAR, ReanimationType.REANIM_GARGANTUAR, 10, 48, 15, 1500, "GARGANTUAR"),
				new ZombieDefinition(ZombieType.ZOMBIE_IMP, ReanimationType.REANIM_IMP, 10, 48, 1, 0, "IMP"),
				new ZombieDefinition(ZombieType.ZOMBIE_BOSS, ReanimationType.REANIM_BOSS, 10, 50, 1, 0, "BOSS"),
				new ZombieDefinition(ZombieType.ZOMBIE_PEA_HEAD, ReanimationType.REANIM_ZOMBIE, 1, 99, 1, 4000, "ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_WALLNUT_HEAD, ReanimationType.REANIM_ZOMBIE, 4, 99, 1, 3000, "ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_JALAPENO_HEAD, ReanimationType.REANIM_ZOMBIE, 3, 99, 10, 1000, "ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_GATLING_HEAD, ReanimationType.REANIM_ZOMBIE, 3, 99, 10, 2000, "ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_SQUASH_HEAD, ReanimationType.REANIM_ZOMBIE, 3, 99, 10, 2000, "ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_TALLNUT_HEAD, ReanimationType.REANIM_ZOMBIE, 4, 99, 10, 2000, "ZOMBIE"),
				new ZombieDefinition(ZombieType.ZOMBIE_REDEYE_GARGANTUAR, ReanimationType.REANIM_GARGANTUAR, 10, 48, 15, 6000, "REDEYED_GARGANTUAR")
			};
			GameConstants.gBossZombieList = new ZombieType[]
			{
				ZombieType.ZOMBIE_TRAFFIC_CONE,
				ZombieType.ZOMBIE_PAIL,
				ZombieType.ZOMBIE_FOOTBALL,
				ZombieType.ZOMBIE_POLEVAULTER,
				ZombieType.ZOMBIE_JACK_IN_THE_BOX,
				ZombieType.ZOMBIE_LADDER,
				ZombieType.ZOMBIE_ZAMBONI,
				ZombieType.ZOMBIE_CATAPULT,
				ZombieType.ZOMBIE_POGO,
				ZombieType.ZOMBIE_NEWSPAPER,
				ZombieType.ZOMBIE_DOOR,
				ZombieType.ZOMBIE_GARGANTUAR
			};
			GameConstants.TESTING_LOAD_BAR = false;
			GameConstants.BAR_FADE_TIME = 8;
			GameConstants.NUM_ALMANAC_REANIMS = 4;
			GameConstants.NUM_ALMANAC_SEEDS = 49;
			GameConstants.NUM_ALMANAC_ZOMBIES = 26;
			GameConstants.NUM_ALMANAC_ZOMBIES_PERF_TEST = 400;
			GameConstants.BUTTON_SIZE = 40;
			GameConstants.BUTTONS_PER_ROW = 10;
			GameConstants.gFlowerCenter = new LawnFPoint[]
			{
				new LawnFPoint(765f, 483f),
				new LawnFPoint(663f, 455f),
				new LawnFPoint(701f, 439f)
			};
			GameConstants.MUSIC_SLIDER_THRESHOLD = 0.01;
			GameConstants.gUserVersionTilJuly07 = 2;
			GameConstants.gUserVersionTilFeb08 = 3;
			GameConstants.gUserVersionTilApril08 = 4;
			GameConstants.gUserVersionTilMay08 = 5;
			GameConstants.gUserVersionTilSep08 = 8;
			GameConstants.gUserVersionTilNov08 = 9;
			GameConstants.gUserVersionTilJan09 = 10;
			GameConstants.gUserVersionTilApr09 = 11;
			GameConstants.gUserVersion = 13;
			GameConstants.gProjectileDefinition = new ProjectileDefinition[]
			{
				new ProjectileDefinition(ProjectileType.PROJECTILE_PEA, 0, 20),
				new ProjectileDefinition(ProjectileType.PROJECTILE_SNOWPEA, 0, 20),
				new ProjectileDefinition(ProjectileType.PROJECTILE_CABBAGE, 0, 40),
				new ProjectileDefinition(ProjectileType.PROJECTILE_MELON, 0, 80),
				new ProjectileDefinition(ProjectileType.PROJECTILE_PUFF, 0, 20),
				new ProjectileDefinition(ProjectileType.PROJECTILE_WINTERMELON, 0, 80),
				new ProjectileDefinition(ProjectileType.PROJECTILE_FIREBALL, 0, 40),
				new ProjectileDefinition(ProjectileType.PROJECTILE_STAR, 0, 20),
				new ProjectileDefinition(ProjectileType.PROJECTILE_SPIKE, 0, 20),
				new ProjectileDefinition(ProjectileType.PROJECTILE_BASKETBALL, 0, 75),
				new ProjectileDefinition(ProjectileType.PROJECTILE_KERNEL, 0, 20),
				new ProjectileDefinition(ProjectileType.PROJECTILE_COBBIG, 0, 300),
				new ProjectileDefinition(ProjectileType.PROJECTILE_BUTTER, 0, 40),
				new ProjectileDefinition(ProjectileType.PROJECTILE_ZOMBIE_PEA, 0, 20),
				new ProjectileDefinition(ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL, 0, 20)
			};
			GameConstants.SEED_BANK_OFFSET_X = 0;
			GameConstants.SEED_BANK_OFFSET_X_END = 0;
		}

		public const int SURVIVAL_NORMAL_FLAGS = 5;

		public const int SURVIVAL_HARD_FLAGS = 10;

		public const int LAST_STAND_FLAGS = 5;

		public const int FINAL_LEVEL = 50;

		public const int LEVELS_PER_AREA = 10;

		public const float SOUND_EFFECT_VOLUME_FACTOR = 0.65f;

		public const int NUM_CHALLENGE_MODES = 122;

		public const int PRICE_MULTIPLIER = 10;

		public const int MINI_GAME_COUNT = 19;

		public const int I_ZOMBIE_LEVEL_COUNT = 10;

		public const int VASEBREAKER_LEVEL_COUNT = 10;

		public const int VASEBREAKER_ACHIEVEMENT = 15;

		public const int IZOMBIE_ACHIEVEMENT = 10;

		public const int LAWN_IMAGE_AVE_MS_TO_LOAD = 9;

		public const int LAWN_SOUND_AVE_MS_TO_LOAD = 54;

		public const int LAWN_FONT_AVE_MS_TO_LOAD = 54;

		public const int LAWN_PARTICLE_AVE_MS_TO_LOAD = 6;

		public const int LAWN_REANIM_AVE_MS_TO_LOAD = 68;

		public const int CRAVE_DAVE_BLINK_RATE = 400;

		public const int MAX_ZOMBIE_WAVES = 100;

		public const int MAX_ZOMBIES_IN_WAVE = 50;

		public const int MAX_ZOMBIE_TYPES = 100;

		public const int FOG_BLOW_RETURN_TIME = 2000;

		public const int FOG_CUTSCENE_TIME = 200;

		public const int NUM_FLAMES_IN_FWOOSH = 12;

		public const int NUM_FOG_ROWS = 7;

		public const int PROGRESS_METER_COUNTER = 150;

		public const int NUM_LEVELS = 50;

		public const int BOARD_SHAKE_TIME = 12;

		public const int FLAG_RAISE_TIME = 100;

		public const int ZOMBIE_COUNTDOWN_FIRST_WAVE = 1800;

		public const int ZOMBIE_COUNTDOWN = 2500;

		public const int ZOMBIE_COUNTDOWN_RANGE = 600;

		public const int ZOMBIE_COUNTDOWN_BEFORE_FLAG = 4500;

		public const int ZOMBIE_COUNTDOWN_BEFORE_REPICK = 5499;

		public const int ZOMBIE_COUNTDOWN_MIN = 400;

		public const int SUN_COUNTDOWN = 425;

		public const int SUN_COUNTDOWN_RANGE = 275;

		public const int SUN_COUNTDOWN_MAX = 950;

		public const int LAWN_MOWER_COIN_START = 200;

		public const int LAWN_MOWER_COIN_DELAY = 40;

		public const int LAWN_MOWER_COIN_END = 150;

		public const int MAX_RENDER_ITEMS = 2048;

		public const int ROOF_SLOPE_PER_ROW = 20;

		public const int ROOF_SLOPE_PER_ROW_OFF = 10;

		public const int BLINK_RATE = 400;

		public const int BLINK_RATE_WALLNUT = 1000;

		public const int GRAVE_BUSTER_EAT_TIME = 400;

		public const int CRATER_TIME = 18000;

		public const int MAX_MAGNET_ITEMS = 5;

		public const int RENDER_GROUP_PUMPKIN_BACK = 1;

		public const int WAKE_UP_TIME = 100;

		public const int SEEDBANK_MAX = 9;

		public const int SLOT_MACHINE_TIME = 400;

		public const int CONVEYOR_SPEED = 4;

		public const int CONVEYOR_PACKET_OFFSET_Y = 35;

		public const int CONVEYOR_PACKET_MIN_OFFSET = 40;

		public const int TICKS_PER_SECOND = 100;

		public const int REANIMATOR_LOAD_TASK_FACTOR = 0;

		public const int TAP_TAP_TO_PLANT = 1;

		public const int PARTNER_PREVIEW_MAX_DRIFT = 300;

		public const int NO_IMAGE_ID = 999999;

		public const int PRESENT_OFFSET_Y = 60;

		public const int MAX_CREDIT_SECTIONS = 57;

		public const int NUM_CLOUDS = 6;

		public const int NUM_FLOWERS = 3;

		public const int CLOUD_MOVE_TIME = 6000;

		public const int CLOUD_WAIT_TIME = 2000;

		public const int LEAF_WAIT_TIME = 200;

		public const int QUICKPLAY_SLIDE_COUNT = 30;

		public const int SLIDE_COUNT = 75;

		public const int MORE_GAMES_LIST_OFFSET = 52;

		public const int NUM_MOTION_TRAIL_FRAMES = 12;

		public const int CHOMP_TIME = 50;

		public const int MAX_MESSAGE_LENGTH = 128;

		public const int SLIDE_OFF_TIME = 100;

		public const int MIN_MESSAGE_TIME = 100;

		public const int MUSIC_ROW_FACTOR = 4;

		public const int BURST_FADE_IN_TIME = 400;

		public const int BURST_TIME_AFTER_START_TO_QUEUE_DRUMS = 300;

		public const int BURST_MIN_TIME = 800;

		public const int BURST_FADE_OUT_TIME = 800;

		public const int BURST_FOG_FADE_OUT_DELAY = 300;

		public const int DRUMS_FOG_FADE_OUT_TIME = 800;

		public const int DRUMS_FADE_OUT_TIME = 50;

		public const int LAWN_SONG_AVE_MS_TO_LOAD = 3500;

		public const int MAX_CHALLENGE_MODES = 200;

		public const int MAX_PURCHASES = 80;

		public const int NUM_PLACEHOLDER_INTS = 1;

		public const int MAX_POTTED_PLANTS = 200;

		public const int ITEM_GAP = 10;

		public const int MENU_BUTTON_TOP_OFFSET = 2;

		public const int STREET_GRID_SIZE_X = 5;

		public const int STREET_GRID_SIZE_Y = 5;

		public const int BEGHOULED_WINNING_SCORE = 75;

		public const int NUM_SQUIRRELS = 7;

		public const int SLOT_MACHINE_WINNING_SCORE = 2000;

		public const int ZOMBIQUARIUM_WINNING_SCORE = 1000;

		public const int IZOMBIE_WINNING_SCORE = 5;

		public const int NUM_TREE_CLOUDS = 6;

		public const int SLOT_MACHINE_COST = 25;

		public const int ART_CHALLEGE_SIZE_X = 9;

		public const int ART_CHALLEGE_SIZE_Y = 6;

		public const int STORM_FLASH_TIME = 150;

		public const int ICE_CHALLANGE_DELAY = 3000;

		public const int BEGHOULED_DELAY_BEFORE_HINT_FLASH = 1500;

		public const int BEGHOULED_DELAY_BEFORE_HINT_FLASH_AGAIN = 1500;

		public const int MAX_SCARY_POTS = 54;

		public const int RENDER_GROUP_TREE_BACKGROUND = 1;

		public const int RENDER_GROUP_TREE_TRUNK = 2;

		public const int RENDER_GROUP_TREE_GRASS = 3;

		public const int RENDER_GROUP_TREE_TOP = 4;

		public const int TREE_CLOUD_MOVE_TIME = 6000;

		public const int TREE_CLOUD_WAIT_TIME = 2000;

		public const int ZEN_GARDEN_FADE_DELAY = 3000;

		public const int STINKY_RENDER_ORDER_OFFSET = 30;

		public const float STINKY_COIN_OFFSET_Y = 30f;

		public static Achievement[] AchievementInfo;

		public static bool gShownMoreSunTutorial = false;

		internal static int[,] gLawnDialogColors;

		public static SexyColor[] gGameButtonColors = new SexyColor[]
		{
			new SexyColor(0, 0, 0),
			new SexyColor(0, 0, 0),
			new SexyColor(0, 0, 0),
			new SexyColor(255, 255, 255),
			new SexyColor(132, 132, 132),
			new SexyColor(212, 212, 212)
		};

		public static ParticleParams[] gLawnParticleArray;

		public static ReanimationParams[] gLawnReanimationArray;

		public static TrailParams[] gLawnTrailArray;

		public static int gLawnStringFormatCount;

		public static TodStringListFormat[] gLawnStringFormats;

		public static int[] gZombieWaves = new int[]
		{
			4,
			6,
			8,
			10,
			8,
			10,
			20,
			10,
			20,
			20,
			10,
			20,
			10,
			20,
			10,
			10,
			20,
			10,
			20,
			20,
			10,
			20,
			20,
			30,
			20,
			20,
			30,
			20,
			30,
			30,
			10,
			20,
			10,
			20,
			20,
			10,
			20,
			10,
			20,
			20,
			10,
			20,
			20,
			30,
			20,
			20,
			30,
			20,
			30,
			30
		};

		public static ZombieAllowedLevels[] gZombieAllowedLevels;

		public static PlantDefinition[] gPlantDefs;

		public static int NUM_BACKUP_DANCERS;

		public static int MAX_ZOMBIE_FOLLOWERS;

		public static int ZOMBIE_WAVE_DEBUG;

		public static int ZOMBIE_WAVE_CUTSCENE;

		public static int ZOMBIE_WAVE_UI;

		public static int ZOMBIE_WAVE_WINNER;

		public static int RENDER_GROUP_SHIELD;

		public static int RENDER_GROUP_ARMS;

		public static int RENDER_GROUP_OVER_SHIELD;

		public static int RENDER_GROUP_BOSS_BACK_LEG;

		public static int RENDER_GROUP_BOSS_FRONT_LEG;

		public static int RENDER_GROUP_BOSS_BACK_ARM;

		public static int RENDER_GROUP_BOSS_FIREBALL_ADDITIVE;

		public static int RENDER_GROUP_BOSS_FIREBALL_TOP;

		public static float CHILLED_SPEED_FACTOR;

		public static int ZOMBIE_LIMP_SPEED_FACTOR;

		public static int BUNGEE_ZOMBIE_HEIGHT;

		public static int DOG_WALKING_DISTANCE;

		public static int POGO_BOUNCE_TIME;

		public static int DOLPHIN_JUMP_TIME;

		public static float THOWN_ZOMBIE_GRAVITY;

		public static int TICKS_BETWEEN_EATS;

		public static float CLIP_HEIGHT_OFF;

		public static float CLIP_HEIGHT_LIMIT;

		public static SexyColor ZOMBIE_MINDCONTROLLED_COLOR;

		public static int ZOMBIE_BLINK_RATE;

		public static int BOSS_FLASH_HEALTH_FRACTION;

		public static float ZOMBIE_WALK_IN_FRONT_DOOR_Y;

		public static int BOBSLED_CRASH_TIME;

		public static int YUCKI_PAUSE_TIME;

		public static int YUCKI_SHORT_PAUSE_TIME;

		public static int YUCKI_HOLD_TIME;

		public static int YUCKI_WALK_TIME;

		public static float BOSS_BALL_OFFSET_Y;

		public static ZombieDefinition[] gZombieDefs;

		public static ZombieType[] gBossZombieList;

		public static bool TESTING_LOAD_BAR;

		public static int BAR_FADE_TIME;

		public static int NUM_ALMANAC_REANIMS;

		public static int NUM_ALMANAC_SEEDS;

		public static int NUM_ALMANAC_ZOMBIES;

		public static int NUM_ALMANAC_ZOMBIES_PERF_TEST;

		public static int BUTTON_SIZE;

		public static int BUTTONS_PER_ROW;

		public static readonly LawnFPoint[] gFlowerCenter;

		public static double MUSIC_SLIDER_THRESHOLD;

		public static int gUserVersionTilJuly07;

		public static int gUserVersionTilFeb08;

		public static int gUserVersionTilApril08;

		public static int gUserVersionTilMay08;

		public static int gUserVersionTilSep08;

		public static int gUserVersionTilNov08;

		public static int gUserVersionTilJan09;

		public static int gUserVersionTilApr09;

		public static int gUserVersion;

		public static ProjectileDefinition[] gProjectileDefinition;

		internal static int SEED_BANK_OFFSET_X;

		internal static int SEED_BANK_OFFSET_X_END;
	}
}
