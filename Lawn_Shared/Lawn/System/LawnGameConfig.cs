using Sexy;
using System;
using System.Text.Json.Serialization;

namespace Lawn
{
    [JsonSerializable(typeof(LawnGameConfig))]
    public class LawnGameConfig
    {
        public string mStoragePath;

        public string mCustomConfigPath;

        public TPoint? mScreenSize;

        public bool? mFullscreen;

        public Constants.LanguageIndex? mLocale;

        public bool? mIronpythonEnabled;

        public short? mIronpythonPort;

        public string mCurrentUser;

        public LawnGameConfig()
        {

        }

        public static LawnGameConfig operator +(LawnGameConfig configOriginal, LawnGameConfig configOverwrite)
        {
            return new LawnGameConfig
            {
                mStoragePath = configOverwrite.mStoragePath ?? configOriginal.mStoragePath,
                mCustomConfigPath = configOverwrite.mCustomConfigPath ?? configOriginal.mCustomConfigPath,
                mScreenSize = configOverwrite.mScreenSize ?? configOriginal.mScreenSize,
                mFullscreen = configOverwrite.mFullscreen ?? configOriginal.mFullscreen,
                mLocale = configOverwrite.mLocale ?? configOriginal.mLocale,
                mIronpythonEnabled = configOverwrite.mIronpythonEnabled ?? configOriginal.mIronpythonEnabled,
                mIronpythonPort = configOverwrite.mIronpythonPort ?? configOriginal.mIronpythonPort,
                mCurrentUser = configOverwrite.mCurrentUser ?? configOriginal.mCurrentUser
            };
        }

        public static LawnGameConfig operator -(LawnGameConfig configOriginal, LawnGameConfig configExclude)
        {
            return new LawnGameConfig
            {
                mStoragePath = configExclude.mStoragePath is null ? configOriginal.mStoragePath : null,
                mCustomConfigPath = configExclude.mCustomConfigPath is null ? configOriginal.mCustomConfigPath : null,
                mScreenSize = configExclude.mScreenSize is null ? configOriginal.mScreenSize : null,
                mFullscreen = configExclude.mFullscreen is null ? configOriginal.mFullscreen : null,
                mLocale = configExclude.mLocale is null ? configOriginal.mLocale : null,
                mIronpythonEnabled = configExclude.mIronpythonEnabled is null ? configOriginal.mIronpythonEnabled : null,
                mIronpythonPort = configExclude.mIronpythonPort is null ? configOriginal.mIronpythonPort : null,
                mCurrentUser = configExclude.mCurrentUser is null ? configOriginal.mCurrentUser : null
            };
        }
    }
}