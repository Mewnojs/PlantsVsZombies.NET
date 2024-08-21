using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;
using System;
using System.Collections.Generic;

namespace Lawn
{
    internal static class GlobalMembersSaveGame
    {
        public const int SAVE_FILE_GAME_MAGIC_NUMBER = 14372508;
        public const int SAVE_FILE_PLAYER_MAGIC_NUMBER = 99810000;
        public const int SAVE_FILE_WP_RAW = 20200000;
        public const int SAVE_FILE_CHECK = 777;

        public static bool LawnCreateCrashBackup(string theFilePath)
        {
            BufferNew b = GlobalStaticVars.gSexyAppBase.ReadBufferNewFromFile(theFilePath, false);
            if (b == null)
            {
                return false;
            }
            return GlobalStaticVars.gSexyAppBase.WriteBufferNewToFile(theFilePath + ".crash", b);
        }

        public static bool LawnLoadGame(Board theBoard, string theFilePath)
        {
            BufferNew b = GlobalStaticVars.gSexyAppBase.ReadBufferNewFromFile(theFilePath, false);
            if (b == null)
            {
                return false;
            }
            SaveGameContext saveGameContext = new SaveGameContext();
            saveGameContext.mBuffer = b;
            saveGameContext.mReading = true;
            //try
            {
                int magic = 0;
                saveGameContext.SyncInt(ref magic);
                saveGameContext.SyncInt(ref saveGameContext.mVersion);
                saveGameContext.SyncInt(ref saveGameContext.mVersionType);
                if (magic != SAVE_FILE_GAME_MAGIC_NUMBER)
                {
                    return false;
                }
                if (saveGameContext.mVersionType != SAVE_FILE_WP_RAW
                   && saveGameContext.mVersionType != SAVE_FILE_WP_RAW)
                {
                    return false;
                }
                saveGameContext.InitializeGameEnum();
                ClearObjGame(theBoard);
                SyncGame(saveGameContext, theBoard);
                SyncObjGame(theBoard, saveGameContext);
                theBoard.mApp.mSoundManager.StopAllSounds();
                theBoard.mApp.mGameScene = GameScenes.Playing;
                return true;
            }
            //catch (Exception)
            {
                ClearObjGame(theBoard);
                return false;
            }
        }

        public static bool LawnSaveGame(Board theBoard, string theFilePath)
        {
            LawnApp app = theBoard.mApp;
            SaveGameContext saveGameContext = new SaveGameContext();
            saveGameContext.mBuffer = new BufferNew();
            saveGameContext.mReading = false;
            int magic = SAVE_FILE_GAME_MAGIC_NUMBER;
            saveGameContext.mVersion = 0;
            saveGameContext.mVersionType = SAVE_FILE_WP_RAW;
            //try
            {
                saveGameContext.SyncInt(ref magic);
                saveGameContext.SyncInt(ref saveGameContext.mVersion);
                saveGameContext.SyncInt(ref saveGameContext.mVersionType);
                saveGameContext.InitializeGameEnum();
                SyncObjGame(theBoard, saveGameContext);
                SyncGame(saveGameContext, theBoard);
                return app.WriteBufferNewToFile(theFilePath, saveGameContext.mBuffer);
            }
            //catch (Exception)
            {
                return false;
            }
        }

        public static bool LawnLoadGameOld(Board mBoard, string theFilePath)
        {
            try
            {
                Sexy.Buffer b = new Sexy.Buffer();
                GlobalStaticVars.gSexyAppBase.ReadBufferFromFile(theFilePath, ref b, false);
                mBoard.LoadFromFile(b);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                EffectSystem.gEffectSystem.EffectSystemFreeAll();
                return false;
            }
            return true;
        }

        public static void ClearObjGame(Board theBoard)
        {
            theBoard.mApp.mEffectSystem.ClearObj();
            theBoard.ClearObj();
        }

        public static void SyncGame(SaveGameContext theContext, Board theBoard)
        {
            theBoard.mApp.mEffectSystem.Sync(theContext);
            theBoard.Sync(theContext);
            int check = SAVE_FILE_CHECK;
            theContext.SyncInt(ref check);
            if (check != SAVE_FILE_CHECK)
            {
                throw new Exception("Check error");
            }
        }

        private static void SyncObjGame(Board theBoard, SaveGameContext theContext)
        {
            theBoard.mApp.mEffectSystem.SyncObj(theContext);
            theBoard.SyncObj(theContext);
        }

        public class SaveGameContext
        {
            private void InitializeSaver<T>(EnumSaver<T> saver)
                where T : struct, Enum
            {
                if (mReading)
                {
                    saver.Sync(this);
                }
                else
                {
                    saver.RegistAllByDefault();
                    saver.Sync(this);
                }
            }

            public void SyncBool(ref bool theBool, int version = 0, bool defaultValue = false)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theBool = defaultValue;
                        return;
                    }
                    bool value = mBuffer.ReadBoolean();
                    theBool = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteBoolean(theBool);
                }
            }

            public void SyncEnum<T>(ref T theEnum, EnumSaver<T> saver, int version = 0, T defaultValue = default)
                where T : struct, Enum
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theEnum = defaultValue;
                        return;
                    }
                    T value = saver.Convert(mBuffer.ReadInt32LE());
                    theEnum = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    int value = saver.Convert(theEnum);
                    mBuffer.WriteInt32LE(value);
                }
            }

            public void SyncDateTime(ref DateTime theDateTime, int version = 0, DateTime defaultValue = default)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theDateTime = defaultValue;
                        return;
                    }
                    long tick = mBuffer.ReadInt64LE();
                    theDateTime = new DateTime(tick);
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteInt64LE(theDateTime.Ticks);
                }
            }

            public void SyncTRect(ref TRect theTRect, int version = 0, TRect defaultValue = default)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theTRect = defaultValue;
                        return;
                    }
                    int X = mBuffer.ReadInt32LE();
                    int Y = mBuffer.ReadInt32LE();
                    int Width = mBuffer.ReadInt32LE();
                    int Height = mBuffer.ReadInt32LE();
                    theTRect.mX = X;
                    theTRect.mY = Y;
                    theTRect.mWidth = Width;
                    theTRect.mHeight = Height;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteInt32LE(theTRect.mX);
                    mBuffer.WriteInt32LE(theTRect.mY);
                    mBuffer.WriteInt32LE(theTRect.mWidth);
                    mBuffer.WriteInt32LE(theTRect.mHeight);
                }
            }

            public void SyncSexyColor(ref SexyColor theColor, int version = 0, SexyColor defaultValue = default)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theColor = defaultValue;
                        return;
                    }
                    int Red = mBuffer.ReadInt32LE();
                    int Green = mBuffer.ReadInt32LE();
                    int Blue = mBuffer.ReadInt32LE();
                    int Alpha = mBuffer.ReadInt32LE();
                    theColor.mRed = Red;
                    theColor.mGreen = Green;
                    theColor.mBlue = Blue;
                    theColor.mAlpha = Alpha;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteInt32LE(theColor.mRed);
                    mBuffer.WriteInt32LE(theColor.mGreen);
                    mBuffer.WriteInt32LE(theColor.mBlue);
                    mBuffer.WriteInt32LE(theColor.mAlpha);
                }
            }

            public void SyncSexyTransform2D(ref SexyTransform2D theTransform, int version = 0, SexyTransform2D defaultValue = default)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theTransform = defaultValue;
                        return;
                    }
                    Matrix matrix = new Matrix();
                    matrix.M11 = mBuffer.ReadFloat32LE();
                    matrix.M21 = mBuffer.ReadFloat32LE();
                    matrix.M31 = mBuffer.ReadFloat32LE();
                    matrix.M41 = mBuffer.ReadFloat32LE();
                    matrix.M12 = mBuffer.ReadFloat32LE();
                    matrix.M22 = mBuffer.ReadFloat32LE();
                    matrix.M32 = mBuffer.ReadFloat32LE();
                    matrix.M42 = mBuffer.ReadFloat32LE();
                    matrix.M13 = mBuffer.ReadFloat32LE();
                    matrix.M23 = mBuffer.ReadFloat32LE();
                    matrix.M33 = mBuffer.ReadFloat32LE();
                    matrix.M43 = mBuffer.ReadFloat32LE();
                    matrix.M14 = mBuffer.ReadFloat32LE();
                    matrix.M24 = mBuffer.ReadFloat32LE();
                    matrix.M34 = mBuffer.ReadFloat32LE();
                    matrix.M44 = mBuffer.ReadFloat32LE();
                    theTransform = new SexyTransform2D(matrix);
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M11);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M21);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M31);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M41);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M12);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M22);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M32);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M42);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M13);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M23);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M33);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M43);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M14);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M24);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M34);
                    mBuffer.WriteFloat32LE(theTransform.mMatrix.M44);
                }
            }

            public void SyncMatrix(ref Matrix matrix, int version = 0, Matrix defaultValue = default)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        matrix = defaultValue;
                        return;
                    }
                    float M11 = mBuffer.ReadFloat32LE();
                    float M21 = mBuffer.ReadFloat32LE();
                    float M31 = mBuffer.ReadFloat32LE();
                    float M41 = mBuffer.ReadFloat32LE();
                    float M12 = mBuffer.ReadFloat32LE();
                    float M22 = mBuffer.ReadFloat32LE();
                    float M32 = mBuffer.ReadFloat32LE();
                    float M42 = mBuffer.ReadFloat32LE();
                    float M13 = mBuffer.ReadFloat32LE();
                    float M23 = mBuffer.ReadFloat32LE();
                    float M33 = mBuffer.ReadFloat32LE();
                    float M43 = mBuffer.ReadFloat32LE();
                    float M14 = mBuffer.ReadFloat32LE();
                    float M24 = mBuffer.ReadFloat32LE();
                    float M34 = mBuffer.ReadFloat32LE();
                    float M44 = mBuffer.ReadFloat32LE();
                    matrix.M11 = M11;
                    matrix.M21 = M21;
                    matrix.M31 = M31;
                    matrix.M41 = M41;
                    matrix.M12 = M12;
                    matrix.M22 = M22;
                    matrix.M32 = M32;
                    matrix.M42 = M42;
                    matrix.M13 = M13;
                    matrix.M23 = M23;
                    matrix.M33 = M33;
                    matrix.M43 = M43;
                    matrix.M14 = M14;
                    matrix.M24 = M24;
                    matrix.M34 = M34;
                    matrix.M44 = M44;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteFloat32LE(matrix.M11);
                    mBuffer.WriteFloat32LE(matrix.M21);
                    mBuffer.WriteFloat32LE(matrix.M31);
                    mBuffer.WriteFloat32LE(matrix.M41);
                    mBuffer.WriteFloat32LE(matrix.M12);
                    mBuffer.WriteFloat32LE(matrix.M22);
                    mBuffer.WriteFloat32LE(matrix.M32);
                    mBuffer.WriteFloat32LE(matrix.M42);
                    mBuffer.WriteFloat32LE(matrix.M13);
                    mBuffer.WriteFloat32LE(matrix.M23);
                    mBuffer.WriteFloat32LE(matrix.M33);
                    mBuffer.WriteFloat32LE(matrix.M43);
                    mBuffer.WriteFloat32LE(matrix.M14);
                    mBuffer.WriteFloat32LE(matrix.M24);
                    mBuffer.WriteFloat32LE(matrix.M34);
                    mBuffer.WriteFloat32LE(matrix.M44);
                }
            }

            public void SyncSexyVector2(ref SexyVector2 theVector, int version = 0, SexyVector2 defaultValue = default)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theVector = defaultValue;
                        return;
                    }
                    float x = mBuffer.ReadFloat32LE();
                    float y = mBuffer.ReadFloat32LE();
                    theVector.x = x;
                    theVector.y = y;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteFloat32LE(theVector.x);
                    mBuffer.WriteFloat32LE(theVector.y);
                }
            }

            public void SyncListLength(ref int theInt, int theMaxCount = short.MaxValue, int version = 0, int defaultValue = 0)
            {
                const int SAVE_FILE_LIST_LEN_MAGIC = 1437666;
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theInt = defaultValue;
                        return;
                    }
                    int magic = mBuffer.ReadInt32LE();
                    if (magic != SAVE_FILE_LIST_LEN_MAGIC)
                    {
                        throw new Exception($"List count magic mismatches!");
                    }
                    int value = mBuffer.ReadInt32LE();
                    if (value > theMaxCount)
                    {
                        throw new Exception($"List count {value} can not be bigger than {theMaxCount}");
                    }
                    magic = mBuffer.ReadInt32LE();
                    if (magic != SAVE_FILE_LIST_LEN_MAGIC)
                    {
                        throw new Exception($"List count magic mismatches!");
                    }
                    theInt = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    if (theInt > theMaxCount)
                    {
                        throw new Exception($"List count {theInt} can not be bigger than {theMaxCount}");
                    }
                    mBuffer.WriteInt32LE(SAVE_FILE_LIST_LEN_MAGIC);
                    mBuffer.WriteInt32LE(theInt);
                    mBuffer.WriteInt32LE(SAVE_FILE_LIST_LEN_MAGIC);
                }
            }

            public void SyncFloat(ref float theFloat, int version = 0, float defaultValue = 0.0f)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theFloat = defaultValue;
                        return;
                    }
                    float value = mBuffer.ReadFloat32LE();
                    theFloat = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteFloat32LE(theFloat);
                }
            }

            public void SyncDouble(ref double theDouble, int version = 0, double defaultValue = 0.0)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theDouble = defaultValue;
                        return;
                    }
                    double value = mBuffer.ReadFloat64LE();
                    theDouble = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteFloat64LE(theDouble);
                }
            }

            public void SyncString(ref string theStr, int version = 0, string defaultValue = "")
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theStr = defaultValue;
                        return;
                    }
                    string value = mBuffer.ReadString();
                    theStr = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteString(theStr);
                }
            }

            public void SyncByte(ref byte theByte, int version = 0, byte defaultValue = 0)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theByte = defaultValue;
                        return;
                    }
                    byte value = mBuffer.ReadUInt8();
                    theByte = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteUInt8(theByte);
                }
            }

            public void SyncShort(ref short theShort, int version = 0, short defaultValue = 0)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theShort = defaultValue;
                        return;
                    }
                    short value = mBuffer.ReadInt16LE();
                    theShort = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteInt16LE(theShort);
                }
            }

            public void SyncInt(ref int theInt, int version = 0, int defaultValue = 0)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theInt = defaultValue;
                        return;
                    }
                    int value = mBuffer.ReadInt32LE();
                    theInt = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteInt32LE(theInt);
                }
            }

            public void SyncUint(ref uint theUint, int version = 0, uint defaultValue = 0u)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theUint = defaultValue;
                        return;
                    }
                    uint value = mBuffer.ReadUInt32LE();
                    theUint = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteUInt32LE(theUint);
                }
            }

            public void SyncLong(ref long theLong, int version = 0, long defaultValue = 0L)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theLong = defaultValue;
                        return;
                    }
                    long value = mBuffer.ReadInt64LE();
                    theLong = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    mBuffer.WriteInt64LE(theLong);
                }
            }

            public void SyncImage(ref Image theImage, int version = 0, Image defaultValue = null)
            {
                if (mReading)
                {
                    if (mVersion < version)
                    {
                        theImage = defaultValue;
                        return;
                    }
                    byte isAtlas = mBuffer.ReadUInt8();
                    string imageId = mBuffer.ReadString();
                    int atlasId;
                    if (isAtlas == 2)
                    {
                        theImage = null;
                        return;
                    }
                    else if (isAtlas == 1)
                    {
                        atlasId = (int)Enum.Parse<AtlasResources.AtlasImageId>(imageId);
                    }
                    else
                    {
                        atlasId = (int)Enum.Parse<Resources.ResourceId>(imageId);
                    }
                    Image value = AtlasResources.GetImageInAtlasById(atlasId);
                    theImage = value;
                }
                else
                {
                    if (mVersion < version)
                    {
                        return;
                    }
                    int atlasId = AtlasResources.GetIdByImageInAtlas(theImage);
                    string imageId;
                    byte isAtlas;
                    if (theImage == null)
                    {
                        isAtlas = 2;
                        imageId = null;
                    }
                    else if (atlasId >= 10000)
                    {
                        isAtlas = 1;
                        imageId = Enum.GetName((AtlasResources.AtlasImageId)atlasId);
                    }
                    else
                    {
                        isAtlas = 0;
                        imageId = Enum.GetName((Resources.ResourceId)atlasId);
                    }
                    mBuffer.WriteUInt8(isAtlas);
                    mBuffer.WriteString(imageId);
                }
            }

            public EnumSaver<SeedType> aSeedTypeSaver = new EnumSaver<SeedType>(SeedType.None);
            public EnumSaver<ZombieType> aZombieTypeSaver = new EnumSaver<ZombieType>(ZombieType.Invalid);
            public EnumSaver<BackgroundType> aBackgroundTypeSaver = new EnumSaver<BackgroundType>(BackgroundType.Num1Day);
            public EnumSaver<GridSquareType> aGridSquareTypeSaver = new EnumSaver<GridSquareType>(GridSquareType.None);
            public EnumSaver<AdviceType> aAdviceTypeSaver = new EnumSaver<AdviceType>(AdviceType.None);
            public EnumSaver<ChallengeState> aChallengeStateSaver = new EnumSaver<ChallengeState>(ChallengeState.Normal);
            public EnumSaver<CoinMotion> aCoinMotionSaver = new EnumSaver<CoinMotion>(CoinMotion.FromSky);
            public EnumSaver<GardenType> aGardenTypeSaver = new EnumSaver<GardenType>(GardenType.Main);
            public EnumSaver<PottedPlant.FacingDirection> aFacingDirectionSaver = new EnumSaver<PottedPlant.FacingDirection>(PottedPlant.FacingDirection.Left);
            public EnumSaver<DrawVariation> aDrawVariationSaver = new EnumSaver<DrawVariation>(DrawVariation.Normal);
            public EnumSaver<PottedPlantAge> aPottedPlantAgeSaver = new EnumSaver<PottedPlantAge>(PottedPlantAge.Sprout);
            public EnumSaver<PottedPlantNeed> aPottedPlantNeedSaver = new EnumSaver<PottedPlantNeed>(PottedPlantNeed.None);
            public EnumSaver<CoinType> aCoinTypeSaver = new EnumSaver<CoinType>(CoinType.None);
            public EnumSaver<CursorType> aCursorTypeSaver = new EnumSaver<CursorType>(CursorType.Normal);
            public EnumSaver<GridItemType> aGridItemTypeSaver = new EnumSaver<GridItemType>(GridItemType.None);
            public EnumSaver<GridItemState> aGridItemStateSaver = new EnumSaver<GridItemState>(GridItemState.Normal);
            public EnumSaver<ScaryPotType> aScaryPotTypeSaver = new EnumSaver<ScaryPotType>(ScaryPotType.None);
            public EnumSaver<LawnMowerState> aLawnMowerStateSaver = new EnumSaver<LawnMowerState>(LawnMowerState.Ready);
            public EnumSaver<LawnMowerType> aLawnMowerTypeSaver = new EnumSaver<LawnMowerType>(LawnMowerType.Lawn);
            public EnumSaver<MowerHeight> aMowerHeightSaver = new EnumSaver<MowerHeight>(MowerHeight.Land);
            public EnumSaver<TutorialState> aTutorialStateSaver = new EnumSaver<TutorialState>(TutorialState.Off);
            public EnumSaver<ParticleEffect> aParticleEffectSaver = new EnumSaver<ParticleEffect>(ParticleEffect.None);
            public EnumSaver<ReanimationType> aReanimationTypeSaver = new EnumSaver<ReanimationType>(ReanimationType.None);
            public EnumSaver<ReanimLoopType> aReanimLoopTypeSaver = new EnumSaver<ReanimLoopType>(ReanimLoopType.Loop);
            public EnumSaver<FilterEffectType> aFilterEffectTypeSaver = new EnumSaver<FilterEffectType>(FilterEffectType.None);
            public EnumSaver<EffectType> aEffectTypeSaver = new EnumSaver<EffectType>(EffectType.Other);
            public EnumSaver<PlantState> aPlantStateSaver = new EnumSaver<PlantState>(PlantState.Notready);
            public EnumSaver<PlantOnBungeeState> aPlantOnBungeeStateSaver = new EnumSaver<PlantOnBungeeState>(PlantOnBungeeState.NotOnBungee);
            public EnumSaver<MagnetItemType> aMagnetItemTypeSaver = new EnumSaver<MagnetItemType>(MagnetItemType.None);
            public EnumSaver<ProjectileMotion> aProjectileMotionSaver = new EnumSaver<ProjectileMotion>(ProjectileMotion.Straight);
            public EnumSaver<ProjectileType> aProjectileTypeSaver = new EnumSaver<ProjectileType>(ProjectileType.Pea);
            public EnumSaver<ZombiePhase> aZombiePhaseSaver = new EnumSaver<ZombiePhase>(ZombiePhase.ZombieNormal);
            public EnumSaver<ZombieHeight> aZombieHeightSaver = new EnumSaver<ZombieHeight>(ZombieHeight.ZombieNormal);
            public EnumSaver<HelmType> aHelmTypeSaver = new EnumSaver<HelmType>(HelmType.None);
            public EnumSaver<ShieldType> aShieldTypeSaver = new EnumSaver<ShieldType>(ShieldType.None);
            public EnumSaver<PlantRowType> aPlantRowTypeSaver = new EnumSaver<PlantRowType>(PlantRowType.Normal);
            public EnumSaver<TrailType> aTrailTypeSaver = new EnumSaver<TrailType>(TrailType.None);

            public void InitializePlayerEnum()
            {
                if (mVersion < 0)
                {
                    return;
                }
                InitializeSaver(aSeedTypeSaver);
                InitializeSaver(aGardenTypeSaver);
                InitializeSaver(aFacingDirectionSaver);
                InitializeSaver(aDrawVariationSaver);
                InitializeSaver(aPottedPlantAgeSaver);
                InitializeSaver(aPottedPlantNeedSaver);
            }

            public void InitializeGameEnum()
            {
                if (mVersion < 0)
                {
                    return;
                }
                InitializeSaver(aSeedTypeSaver);
                InitializeSaver(aZombieTypeSaver);
                InitializeSaver(aBackgroundTypeSaver);
                InitializeSaver(aGridSquareTypeSaver);
                InitializeSaver(aAdviceTypeSaver);
                InitializeSaver(aChallengeStateSaver);
                InitializeSaver(aCoinMotionSaver);
                InitializeSaver(aGardenTypeSaver);
                InitializeSaver(aFacingDirectionSaver);
                InitializeSaver(aDrawVariationSaver);
                InitializeSaver(aPottedPlantAgeSaver);
                InitializeSaver(aPottedPlantNeedSaver);
                InitializeSaver(aCoinTypeSaver);
                InitializeSaver(aCursorTypeSaver);
                InitializeSaver(aGridItemTypeSaver);
                InitializeSaver(aGridItemStateSaver);
                InitializeSaver(aScaryPotTypeSaver);
                InitializeSaver(aLawnMowerStateSaver);
                InitializeSaver(aLawnMowerTypeSaver);
                InitializeSaver(aMowerHeightSaver);
                InitializeSaver(aTutorialStateSaver);
                InitializeSaver(aParticleEffectSaver);
                InitializeSaver(aReanimationTypeSaver);
                InitializeSaver(aReanimLoopTypeSaver);
                InitializeSaver(aFilterEffectTypeSaver);
                InitializeSaver(aEffectTypeSaver);
                InitializeSaver(aPlantStateSaver);
                InitializeSaver(aPlantOnBungeeStateSaver);
                InitializeSaver(aMagnetItemTypeSaver);
                InitializeSaver(aProjectileMotionSaver);
                InitializeSaver(aProjectileTypeSaver);
                InitializeSaver(aZombiePhaseSaver);
                InitializeSaver(aZombieHeightSaver);
                InitializeSaver(aHelmTypeSaver);
                InitializeSaver(aShieldTypeSaver);
                InitializeSaver(aPlantRowTypeSaver);
                InitializeSaver(aTrailTypeSaver);
            }

            public BufferNew mBuffer;

            public bool mReading;

            public int mVersion;
            
            public int mVersionType;
        }

        public class EnumSaver<T> where T : struct, Enum
        {
            private const int DEFAULT_CAPACITY = 128;

            private readonly Dictionary<T, int> _enumToIntDic = new(DEFAULT_CAPACITY);
            private readonly List<T> _intToEnumDic = new(DEFAULT_CAPACITY);

            private readonly T _defaultValueEnum;

            public EnumSaver(T defaultValueEnum)
            {
                _defaultValueEnum = defaultValueEnum;
            }

            public void Sync(SaveGameContext theContext)
            {
                if (theContext.mReading)
                {
                    int count = theContext.mBuffer.ReadInt32LE();
                    for (int i = 0; i < count; i++)
                    {
                        string name = theContext.mBuffer.ReadString();
                        T e;
                        try
                        {
                            e = Enum.Parse<T>(name);
                        }
                        catch (Exception)
                        {
                            e = _defaultValueEnum;
                        }
                        Regist(e, true);
                    }
                }
                else
                {
                    theContext.mBuffer.WriteInt32LE(_intToEnumDic.Count);
                    for (int i = 0; i < _intToEnumDic.Count; i++)
                    {
                        T e = _intToEnumDic[i];
                        string name = Enum.GetName(e);
                        theContext.mBuffer.WriteString(name);
                    }
                }
            }

            public int Convert(T e)
            {
                if (_enumToIntDic.TryGetValue(e, out int value))
                {
                    return value;
                }
                return -1;
            }

            public T Convert(int value)
            {
                if (value >= 0 && value < _intToEnumDic.Count)
                {
                    return _intToEnumDic[value];
                }
                return _defaultValueEnum;
            }

            public bool Regist(T e, bool force)
            {
                if (!force && _enumToIntDic.ContainsKey(e))
                {
                    return false;
                }
                _enumToIntDic.TryAdd(e, _intToEnumDic.Count);
                _intToEnumDic.Add(e);
                return true;
            }

            public void RegistAllByDefault()
            {
                T[] enumArray = Enum.GetValues<T>();
                for (int i = 0; i < enumArray.Length; i++)
                {
                    Regist(enumArray[i], false);
                }
            }
        }
    }
}
