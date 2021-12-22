using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class ReanimatorCache
    {
        public void ReanimatorCacheInitialize()
        {
            for (int i = 0; i < (int)SeedType.NUM_SEED_TYPES; i++)
            {
                mPlantImages.Add(null);
            }
            for (int i = 0; i < (int)LawnMowerType.NUM_MOWER_TYPES; i++)
            {
                mLawnMowers.Add(null);
            }
            for (int i = 0; i < (int)ZombieType.NUM_CACHED_ZOMBIE_TYPES; i++)
            {
                mZombieImages.Add(null);
            }
            mApp = GlobalStaticVars.gLawnApp;
        }

        public void ReanimatorCacheDispose()
        {
            mLawnMowers = null;
            mPlantImages = null;
            mImageVariationList = null;
            mZombieImages = null;
        }

        /*public void DrawCachedPlant(Graphics g, float centerX, float btmY, SeedType theSeedType, DrawVariation theDrawVariation)
        {
            Debug.ASSERT(theSeedType >= SeedType.SEED_PEASHOOTER && theSeedType < SeedType.NUM_SEED_TYPES);
            Image image;
            if (theSeedType == SeedType.SEED_SPROUT)
            {
                image = AtlasResources.IMAGE_CACHED_MARIGOLD;
            }
            else
            {
                image = AtlasResources.GetImageInAtlasById((int)(10300 + theSeedType));
            }
            int num = (int)(centerX - (float)(image.mWidth / 2) * g.mScaleX);
            int num2 = (int)(btmY - (float)image.mHeight * g.mScaleY);
            TodCommon.TodDrawImageScaledF(g, image, (float)num, (float)num2, g.mScaleX, g.mScaleY);
        }*/
        public void DrawCachedPlant(Graphics g, float centerX, float btmY, SeedType theSeedType, DrawVariation theDrawVariation)
        {
            MemoryImage image = null;
            if (theDrawVariation == DrawVariation.VARIATION_NORMAL || theDrawVariation == DrawVariation.VARIATION_IMITATER || theDrawVariation == DrawVariation.VARIATION_IMITATER_LESS)
            {
                if (mPlantImages[(int)theSeedType] == null)
                {
                    Debug.OutputDebug("(!!) ReanimatorCache uninitialized plant variation (%d).\n", theSeedType);
                    return;
                }
                image = mPlantImages[(int)theSeedType];
            }
            else 
            {
                
                if (!mImageVariationList.TryGetValue(new CachedPlantVariation
                {
                    mSeedType = theSeedType,
                    mDrawVariation = theDrawVariation
                }, out image))
                {
                    Debug.OutputDebug("(!!) ReanimatorCache uninitialized plant variation (%d)-(%d).\n", theSeedType, theDrawVariation);
                    return;
                }
            }
            TRect drawRect = GetPlantImageSize(theSeedType);
            if (!mApp.Is3DAccelerated())
            {
                if (g.mScaleX == 1f && g.mScaleY == 1f)
                {
                    g.DrawImage(image, centerX + (Constants.S * ( drawRect.mX - drawRect.mWidth / 2)), btmY + (Constants.S * (-drawRect.mY - drawRect.mHeight)));
                    return;
                }
            }
            TodCommon.TodDrawImageScaledF(g, image, centerX + (Constants.S * ((drawRect.mX - drawRect.mWidth / 2) * g.mScaleX)), btmY + (Constants.S * ((-drawRect.mY - drawRect.mHeight) * g.mScaleY)), g.mScaleX, g.mScaleY);
            return;
        }

        public void DrawCachedZombie(Graphics g, float thePosX, float thePosY, ZombieType theZombieType)
        {
            Debug.ASSERT(theZombieType >= ZombieType.ZOMBIE_NORMAL && theZombieType < ZombieType.NUM_CACHED_ZOMBIE_TYPES);
            Image imageInAtlasById = AtlasResources.GetImageInAtlasById((int)(10349 + theZombieType));
            TodCommon.TodDrawImageScaledF(g, imageInAtlasById, thePosX, thePosY, g.mScaleX, g.mScaleY);
        }
        public void DrawCachedZombieNew(Graphics g, float thePosX, float thePosY, ZombieType theZombieType)
        {
            if (mZombieImages[(int)theZombieType] == null)
            {
                Debug.OutputDebug("(!!) ReanimatorCache uninitialized zombie variation (%d).\n", theZombieType);
                return;
            }
            TodCommon.TodDrawImageScaledF(g, mZombieImages[(int)theZombieType], thePosX + Constants.S * -20, thePosY + Constants.S * -20, g.mScaleX, g.mScaleY);
        }

        public void DrawCachedMower(Graphics g, float thePosX, float thePosY, LawnMowerType mowerType)
        {
            if (mLawnMowers[(int)mowerType] == null)
            {
                Debug.OutputDebug("(!!) ReanimatorCache uninitialized mower variation (%d).\n", mowerType);
                return;
            }
            TodCommon.TodDrawImageScaledF(g, mLawnMowers[(int)mowerType], thePosX - (Constants.S * 20), thePosY, g.mScaleX, g.mScaleY);
            
        }

        public TRect GetPlantImageSize(SeedType seedtype)
        {
            TRect result = new TRect(-20, -20, 120, 120);
            switch (seedtype)
            {
                case SeedType.SEED_TALLNUT:
                    result.mY -= 20;
                    result.mHeight += 40;
                    break;
                case SeedType.SEED_MELONPULT:
                case SeedType.SEED_WINTERMELON:
                    result.mX -= 20;
                    result.mWidth += 40;
                    break;
                case SeedType.SEED_COBCANNON:
                    result.mWidth += 80;
                    break;
            }
            return result;
        }

        public void DrawReanimatorFrame(Graphics g, float x, float y, ReanimationType reanimType, string trackName, DrawVariation variation)
        {
            Reanimation reanim = Reanimation.GetNewReanimation();
            reanim.ReanimationInitializeType(x, y, reanimType);
            if (trackName != null && reanim.TrackExists(trackName))
            {
                reanim.SetFramesForLayer(trackName);
            }
            if (reanimType == ReanimationType.REANIM_KERNELPULT)
            {
                reanim.AssignRenderGroupToTrack("Cornpult_butter", -1);
            }
            if (reanimType == ReanimationType.REANIM_SUNFLOWER)
            {
                reanim.mAnimTime = 0.15f;
            }
            reanim.AssignRenderGroupToTrack("zombie_butter", -1);
            reanim.AssignRenderGroupToTrack("anim_waterline", -1);
            if (g.mColorizeImages)
            {
                reanim.mColorOverride = g.mColor;   //TODO: waiting for validation of the field mColorOveride
            }
            reanim.OverrideScale(g.mScaleX, g.mScaleY);
            UpdateReanimationforVariation(reanim, variation);
            reanim.Draw(g);
            reanim.PrepareForReuse();
        }

        public void UpdateReanimationforVariation(Reanimation reanimation, DrawVariation variation)
        {
            if (variation != DrawVariation.VARIATION_NORMAL)
            {
                if (DrawVariation.VARIATION_MARIGOLD_WHITE <= variation && variation <= DrawVariation.VARIATION_MARIGOLD_LIGHT_GREEN)
                {
                    List<SexyColor> marigoldColors = new List<SexyColor> {
                        new SexyColor(255, 255, 255),
                        new SexyColor(230, 30, 195),
                        new SexyColor(250, 125, 5),
                        new SexyColor(255, 145, 215),
                        new SexyColor(160, 255, 245),
                        new SexyColor(230, 30, 30),
                        new SexyColor(5, 130, 255),
                        new SexyColor(195, 55, 235),
                        new SexyColor(235, 210, 255),
                        new SexyColor(255, 245, 55),
                        new SexyColor(180, 255, 105)
                    };
                    reanimation.GetTrackInstanceByName("Marigold_petals").mTrackColor = marigoldColors[(int)variation - 2];
                }
                switch (variation)
                {
                    case DrawVariation.VARIATION_IMITATER:
                        reanimation.mFilterEffect = FilterEffectType.FILTER_EFFECT_WASHED_OUT;
                        break;
                    case DrawVariation.VARIATION_IMITATER_LESS:
                        reanimation.mFilterEffect = FilterEffectType.FILTER_EFFECT_LESS_WASHED_OUT;
                        break;
                    case DrawVariation.VARIATION_ZEN_GARDEN:
                        reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_zengarden);
                        break;
                    case DrawVariation.VARIATION_ZEN_GARDEN_WATER:
                        reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_waterplants);
                        break;
                    case DrawVariation.VARIATION_AQUARIUM:
                        reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle_aquarium);
                        break;
                    case DrawVariation.VARIATION_BIGIDLE:
                        reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_bigidle);
                        break;
                    case DrawVariation.VARIATION_SPROUT_NO_FLOWER:
                        reanimation.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle_noflower);
                        break;

                }
            }
        }

        public MemoryImage MakeBlankCanvasImage(int width, int height)
        {
            MemoryImage result = new MemoryImage();
            result.Create(width, height);
            result.Clear();
            return result;
        }

        public MemoryImage MakeCachedPlantFrame(SeedType seedType, DrawVariation drawVariation)
        {
            MemoryImage result = null;
            if (drawVariation == DrawVariation.VARIATION_NORMAL)
            {
                if (mPlantImages[(int)seedType] != null)
                {
                    return mPlantImages[(int)seedType];
                }
            }
            else
            {
                if (mImageVariationList.TryGetValue(new CachedPlantVariation
                {
                    mSeedType = seedType,
                    mDrawVariation = drawVariation
                }, out result))
                {
                    return result;
                }
            }
            TRect drawRect = GetPlantImageSize(seedType);
            result = MakeBlankCanvasImage((int)(Constants.S * drawRect.mWidth), (int)(Constants.S * drawRect.mHeight));
            lock (ResourceManager.DrawLocker)
            {
                Graphics g = Graphics.GetNew(result);
                g.SetLinearBlend(true);

            
                PlantDefinition plantDef = Plant.GetPlantDefinition(seedType);
                ReanimationType reanimationType = plantDef.mReanimationType;
                int offset;
                switch (seedType)
                {
                    case SeedType.SEED_POTATOMINE:
                        offset = 12;
                        g.mScaleX = 0.8f;
                        g.mScaleY = 0.8f;
                        DrawReanimatorFrame(g, (Constants.S * -(drawRect.mX - offset)), (Constants.S * -(drawRect.mY - offset)), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_armed, drawVariation);
                        break;
                    case SeedType.SEED_INSTANT_COFFEE:
                        offset = 12;
                        g.mScaleX = 0.8f;
                        g.mScaleY = 0.8f;
                        DrawReanimatorFrame(g, (Constants.S * -(drawRect.mX - offset)), (Constants.S * -(drawRect.mY - offset)), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_idle, drawVariation);
                        break;
                    case SeedType.SEED_EXPLODE_O_NUT:
                        g.SetColorizeImages(true);
                        g.SetColor(new SexyColor(255, 64, 64));
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_idle, drawVariation);
                        break;
                    case SeedType.SEED_PEASHOOTER:
                    case SeedType.SEED_SNOWPEA:
                    case SeedType.SEED_REPEATER:
                    case SeedType.SEED_GATLINGPEA:
                    case SeedType.SEED_LEFTPEATER:
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_idle, drawVariation);
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, drawVariation);
                        break;
                    case SeedType.SEED_SPLITPEA:
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_idle, drawVariation);
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, drawVariation);
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_idle, drawVariation);
                        break;
                    case SeedType.SEED_THREEPEATER:
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_idle, drawVariation);
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_head_idle, drawVariation);
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_head_idle1, drawVariation);
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_head_idle3, drawVariation);
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_head_idle2, drawVariation);
                        break;
                    default:
                        DrawReanimatorFrame(g, (Constants.S * -drawRect.mX), (Constants.S * -drawRect.mY), reanimationType, GlobalMembersReanimIds.ReanimTrackId_anim_idle, drawVariation);
                        break;

                }
                if (drawVariation == DrawVariation.VARIATION_NORMAL)
                {
                    mPlantImages[(int)seedType] = result;
                }
                else
                {
                    mImageVariationList[new CachedPlantVariation
                    {
                        mSeedType = seedType,
                        mDrawVariation = drawVariation
                    }] = result;
                }
                g.EndFrame();
                g.SetRenderTarget(null);
                g.PrepareForReuse();
            }
            return result;
        }

        public MemoryImage MakeCachedMowerFrame(LawnMowerType mowerType)
        {
            if (mLawnMowers[(int)mowerType] != null)
            {
                return mLawnMowers[(int)mowerType];
            }
            MemoryImage result = MakeBlankCanvasImage((int)(Constants.S * 90), (int)(Constants.S * 100));
            lock (ResourceManager.DrawLocker)
            {
                Graphics g = Graphics.GetNew(result);
                g.SetLinearBlend(true);

                g.BeginFrame();
                ReanimationType reanimtype = ReanimationType.REANIM_LAWNMOWER;
                string trackname = null;
                g.mScaleX = 0.85f;
                g.mScaleY = 0.85f;
                float x = 10;
                float y = 0;
                switch (mowerType)
                {
                    case LawnMowerType.LAWNMOWER_LAWN:
                        trackname = GlobalMembersReanimIds.ReanimTrackId_anim_normal;
                        break;
                    case LawnMowerType.LAWNMOWER_POOL:
                        g.mScaleX = 0.80f;
                        g.mScaleY = 0.80f;
                        y = 25;
                        reanimtype = ReanimationType.REANIM_POOL_CLEANER;
                        break;
                    case LawnMowerType.LAWNMOWER_ROOF:
                        reanimtype = ReanimationType.REANIM_ROOF_CLEANER;
                        break;
                    case LawnMowerType.LAWNMOWER_SUPER_MOWER:
                        trackname = GlobalMembersReanimIds.ReanimTrackId_anim_tricked;
                        break;
                }
                DrawReanimatorFrame(g, (Constants.S * x), (Constants.S * y), reanimtype, trackname, DrawVariation.VARIATION_NORMAL);
                mLawnMowers[(int)mowerType] = result;

                g.EndFrame();
                g.SetRenderTarget(null);
                g.PrepareForReuse();
            }
            return result;
        }

        public MemoryImage MakeCachedZombieFrame(ZombieType zombieType)
        {
            if (mZombieImages[(int)zombieType] != null)
            {
                return mZombieImages[(int)zombieType];
            }
            MemoryImage result = null;
            if (zombieType == ZombieType.ZOMBIE_ZAMBONI)
            {
                result = MakeBlankCanvasImage((int)(Constants.S * 512), (int)(Constants.S * 256));
            }
            else 
            {
                result = MakeBlankCanvasImage((int)(Constants.S * 256), (int)(Constants.S * 256));
            }
            lock (ResourceManager.DrawLocker)
            {
                Graphics g = Graphics.GetNew(result);
                g.SetLinearBlend(true);

            
                g.BeginFrame();
                ZombieType zombieType_reanim = zombieType != ZombieType.ZOMBIE_CACHED_POLEVAULTER_WITH_POLE ? zombieType : ZombieType.ZOMBIE_POLEVAULTER;
                ZombieDefinition zombieDef = Zombie.GetZombieDefinition(zombieType_reanim);
                ReanimationType reanimationType = zombieDef.mReanimationType;
                float x = 40;
                float y = 40;
                if (zombieType == ZombieType.ZOMBIE_ZAMBONI)
                {
                    x += 100;
                }
                if (reanimationType == ReanimationType.REANIM_ZOMBIE)
                {
                    Reanimation reanim = Reanimation.GetNewReanimation();
                    reanim.ReanimationInitializeType(Constants.S * x, Constants.S * y, reanimationType);
                    reanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                    reanim.AssignRenderGroupToTrack("zombie_butter", -1);
                    Zombie.SetupReanimLayers(reanim, zombieType_reanim);
                    switch (zombieType)
                    {
                        case ZombieType.ZOMBIE_DOOR:
                            reanim.AssignRenderGroupToTrack(GlobalMembersReanimIds.ReanimTrackId_anim_screendoor, 0);
                            break;
                        case ZombieType.ZOMBIE_FLAG:
                            Reanimation flagReanim = Reanimation.GetNewReanimation();
                            flagReanim.ReanimationInitializeType(Constants.S * x, Constants.S * y, ReanimationType.REANIM_ZOMBIE_FLAGPOLE);
                            flagReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_zombie_flag);
                            flagReanim.Draw(g);
                            flagReanim.PrepareForReuse();
                            break;
                    }
                    reanim.Draw(g);
                    reanim.PrepareForReuse();
                }
                else if (reanimationType == ReanimationType.REANIM_BOSS)
                {
                    Reanimation reanim = Reanimation.GetNewReanimation();
                    reanim.ReanimationInitializeType(Constants.S * -524, Constants.S * -88, reanimationType);
                    reanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
                    reanim.AssignRenderGroupToTrack("zombie_butter", -1);
                    Reanimation bodyReanim = Reanimation.GetNewReanimation();
                    bodyReanim.ReanimationInitializeType(Constants.S * 46, Constants.S * 22, ReanimationType.REANIM_BOSS_DRIVER);
                    bodyReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                    reanim.Draw(g);
                    bodyReanim.Draw(g);
                    reanim.AssignRenderGroupToTrack("boss_body1", -1);
                    reanim.AssignRenderGroupToTrack("boss_neck", -1);
                    reanim.AssignRenderGroupToTrack("boss_head2", -1);
                    reanim.Draw(g);
                    reanim.PrepareForReuse();
                    bodyReanim.PrepareForReuse();
                }
                else
                {
                    string trackName = null;
                    switch (zombieType)
                    {
                        case ZombieType.ZOMBIE_POGO:
                            trackName = GlobalMembersReanimIds.ReanimTrackId_anim_pogo;
                            break;
                        case ZombieType.ZOMBIE_CACHED_POLEVAULTER_WITH_POLE:
                            trackName = GlobalMembersReanimIds.ReanimTrackId_anim_idle;
                            break;
                        case ZombieType.ZOMBIE_POLEVAULTER:
                            trackName = "anim_walk";
                            break;
                        case ZombieType.ZOMBIE_GARGANTUAR:
                            y = 60;
                            break;
                        default:
                            break;
                    }
                    DrawReanimatorFrame(g, Constants.S * x, Constants.S * y, reanimationType, trackName, DrawVariation.VARIATION_NORMAL);
                }

                mZombieImages[(int)zombieType] = result;
                g.EndFrame();
                g.SetRenderTarget(null);
                g.PrepareForReuse();
            }
            return result;
        }

        public void LoadCachedImages() { }

        public void SaveCachedImages() { }

        public Dictionary<CachedPlantVariation, MemoryImage> mImageVariationList = new Dictionary<CachedPlantVariation, MemoryImage>();

        public List<MemoryImage> mPlantImages = new List<MemoryImage>();

        public List<MemoryImage> mLawnMowers = new List<MemoryImage>();

        public List<MemoryImage> mZombieImages = new List<MemoryImage>();

        public LawnApp mApp;

        public class CachedPlantVariation
        {
            public SeedType mSeedType;

            public DrawVariation mDrawVariation;
        }
    }
}
