using Microsoft.Xna.Framework;
using System;

namespace Sexy
{
    public /*internal*/ class FrameworkConstants
    {
        public static float Font_Scale = 1f;

        public static bool Loaded;

        public static string ImageSubPath;

        public static DisplayOrientation SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

        private static FrameworkConstants.LanguageIndex mLanguage;

        public static string LanguageSubDir;

        public static Point BackBufferSize;

        public static int BOARD_WIDTH;

        public static int BOARD_HEIGHT;

        public static int BOARD_EDGE;

        public static int BOARD_OFFSET;

        public static int BOARD_EXTRA_ROOM;

        public static float S;

        public static float IS;

        public static FrameworkConstants.LanguageIndex Language
        {
            get
            {
                return FrameworkConstants.mLanguage;
            }
            set
            {
                FrameworkConstants.mLanguage = value;
                FrameworkConstants.LanguageSubDir = FrameworkConstants.mLanguage.ToString();
            }
        }

        public static float InvertLowResValue(float x)
        {
            return 1.875f * x;
        }

        public static float InvertAndScale(float x)
        {
            return FrameworkConstants.InvertLowResValue(x) * FrameworkConstants.S;
        }

        public static float ScaleFrom480(float x)
        {
            return FrameworkConstants.Font_Scale * x;
        }

        public enum LanguageIndex
        {
            en = 1,
            fr,
            de,
            es,
            it,
            zh_cn,
        }
    }
}
