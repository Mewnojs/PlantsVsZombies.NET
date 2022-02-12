using System;
using Lawn;

namespace Sexy
{
    public /*internal*/ static partial class GlobalStaticVars
    {
        public static LawnApp gLawnApp
        {
            get
            {
                return (LawnApp)GlobalStaticVars.gSexyAppBase;
            }
        }

        public static string LawnGetCurrentLevelName()
        {
            if (GlobalStaticVars.gLawnApp == null)
            {
                return "Before App";
            }
            if (GlobalStaticVars.gLawnApp.mGameScene == GameScenes.Loading)
            {
                return "Game Loading";
            }
            if (GlobalStaticVars.gLawnApp.mGameScene == GameScenes.Menu)
            {
                return "Game Selector";
            }
            if (GlobalStaticVars.gLawnApp.mGameScene == GameScenes.Award)
            {
                return "Award Screen";
            }
            if (GlobalStaticVars.gLawnApp.mGameScene == GameScenes.Menu)
            {
                return "Game Selector";
            }
            if (GlobalStaticVars.gLawnApp.mGameScene == GameScenes.Challenge)
            {
                return "Challenge Screen";
            }
            if (GlobalStaticVars.gLawnApp.mGameScene == GameScenes.Credit)
            {
                return "Credits";
            }
            if (GlobalStaticVars.gLawnApp.mBoard == null)
            {
                return "Not Playing";
            }
            if (GlobalStaticVars.gLawnApp.IsFirstTimeAdventureMode())
            {
                return GlobalStaticVars.gLawnApp.GetStageString(GlobalStaticVars.gLawnApp.mBoard.mLevel);
            }
            return Common.StrFormat_("F{0}", GlobalStaticVars.gLawnApp.GetStageString(GlobalStaticVars.gLawnApp.mBoard.mLevel));
        }

        public static bool LawnGetCloseRequest()
        {
            return GlobalStaticVars.gLawnApp != null && GlobalStaticVars.gLawnApp.mCloseRequest;
        }

        public static bool LawnHasUsedCheatKeys()
        {
            return GlobalStaticVars.gLawnApp != null && GlobalStaticVars.gLawnApp.mPlayerInfo != null && !GlobalStaticVars.gLawnApp.mPlayerInfo.mHasUsedCheatKeys;
        }

        public static void initialize(Main main)
        {
            initializeReusables();
            GlobalStaticVars.g = Main.graphics;
            GlobalStaticVars.g.Init();
            GlobalStaticVars.mGlobalContent = new GlobalContentManager(main);
            GlobalStaticVars.gGetCurrentLevelName = GlobalStaticVars.LawnGetCurrentLevelName();
            GlobalStaticVars.gAppCloseRequest = GlobalStaticVars.LawnGetCloseRequest();
            GlobalStaticVars.gAppHasUsedCheatKeys = GlobalStaticVars.LawnHasUsedCheatKeys();
            GlobalStaticVars.gSexyAppBase = new LawnApp(main);
            GlobalStaticVars.gSexyAppBase.Init();
            GlobalStaticVars.gSexyAppBase.Start();
        }

        public static void initializeReusables() 
        {
            //LazyReusableObjectHelper<Graphics>.Initialize(20);
            Graphics.PreAllocateMemory();
        } 

        public static void shutdown()
        {
            GlobalStaticVars.gLawnApp.Shutdown();
            GlobalStaticVars.gLawnApp.Dispose();
        }

        internal static string GetResourceDir()
        {
            return "";
        }

        internal static string CommaSeperate_(int theDispPoints)
        {
            if (theDispPoints == 0)
            {
                return "0";
            }
            return string.Format("{0:#,#}", theDispPoints);
        }

        internal static string GetDocumentsDir()
        {
            return "docs/";
        }

        public static Graphics g;

        public static GlobalContentManager mGlobalContent;

        public static SexyAppBase gSexyAppBase;

        internal static int gProfileVersion = 14;

        public static bool gIsPartnerBuild = false;

        public static int gSlowMoCounter = 0;

        public static bool gSlowMo = false;

        public static bool gFastMo = false;

        public static bool gLowFramerate = false;

        public static string gGetCurrentLevelName;

        public static bool gAppCloseRequest;

        public static bool gAppHasUsedCheatKeys;

        public struct CGPoint
        {
            public float x;

            public float y;
        }

        public enum Phase
        {
            TOUCH_BEGAN,
            TOUCH_MOVED,
            TOUCH_STATIONARY,
            TOUCH_ENDED,
            TOUCH_CANCELLED
        }

        public struct Touch
        {
            public GlobalStaticVars.CGPoint location;
        }
    }
}
