using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    internal static class GlobalMembersSaveGame
    {
        public static bool LawnLoadGame(Board mBoard, string theFilePath)
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

        public static bool LawnSaveGame(Board mBoard, string theFilePath)
        {
            LawnApp app = mBoard.mApp;
            GlobalMembersSaveGame.SaveGameContext saveGameContext = new GlobalMembersSaveGame.SaveGameContext();
            saveGameContext.mFailed = false;
            saveGameContext.mReading = false;
            GlobalMembersSaveGame.SyncBoard(ref saveGameContext, mBoard);
            if (!app.WriteBufferToFile(theFilePath, saveGameContext.mBuffer))
            {
                return false;
            }
            try
            {
                Sexy.Buffer buffer = new Sexy.Buffer();
                mBoard.SaveToFile(buffer);
                GlobalStaticVars.gSexyAppBase.WriteBufferToFile(theFilePath, buffer);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return true;
        }

        public static void SyncParticleEmitter(TodParticleSystem theParticleSystem, ref TodParticleEmitter theParticleEmitter, ref GlobalMembersSaveGame.SaveGameContext theContext)
        {
        }

        public static void SyncParticleSystem(Board theBoard, TodParticleSystem theParticleSystem, ref GlobalMembersSaveGame.SaveGameContext theContext)
        {
        }

        public static void SyncReanimation(Board theBoard, Reanimation theReanimation, ref GlobalMembersSaveGame.SaveGameContext theContext)
        {
        }

        public static void SyncBoard(ref GlobalMembersSaveGame.SaveGameContext theContext, Board mBoard)
        {
        }

        public static void FixBoardAfterLoad(Board mBoard)
        {
        }

        public const uint SAVE_FILE_VERSION = 2U;

        internal struct SaveFileHeader
        {
            public uint mMagicNumber;

            public uint mBuildVersion;

            public uint mBuildDate;
        }

        internal class SaveGameContext
        {
            public void SyncInt(ref int theInt)
            {
            }

            public void SyncUint(ref uint theUint)
            {
            }

            public void SyncReanimationDef(ref ReanimatorDefinition theDefinition)
            {
            }

            public void SyncParticleDef(ref TodParticleDefinition theDefinition)
            {
            }

            public void SyncTrailDef(ref TrailDefinition theDefinition)
            {
            }

            public void SyncImage(ref Image theImage)
            {
            }

            public Sexy.Buffer mBuffer = new Sexy.Buffer();

            public bool mFailed;

            public bool mReading;
        }
    }
}
