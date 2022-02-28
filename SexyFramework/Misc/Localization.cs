using System;

namespace Sexy.Misc
{
	public static class Localization
	{
		public static void InitLanguage()
		{
			SexyAppBase gSexyAppBase = GlobalMembers.gSexyAppBase;
			Localization.sCurrentLanguage = gSexyAppBase.mAppDriver.GetAppLanguage();
		}

		public static void ChangeLanguage(Localization.LanguageType lan)
		{
			Localization.sCurrentLanguage = lan;
		}

		public static Localization.LanguageType GetCurrentLanguage()
		{
			return Localization.sCurrentLanguage;
		}

		public static int GetCurrentFontOffsetY()
		{
			if (Localization.sCurrentLanguage == Localization.LanguageType.Language_CH || Localization.sCurrentLanguage == Localization.LanguageType.Language_CHT)
			{
				return 10;
			}
			return 0;
		}

		public static string GetLanguageSuffix(Localization.LanguageType lan)
		{
			return Localization.LanguageSuffix[(int)lan];
		}

		public static string GetCurrentThousandSep()
		{
			if (Localization.sCurrentLanguage == Localization.LanguageType.Language_FR || Localization.sCurrentLanguage == Localization.LanguageType.Language_RU)
			{
				return " ";
			}
			if (Localization.sCurrentLanguage == Localization.LanguageType.Language_GR || Localization.sCurrentLanguage == Localization.LanguageType.Language_SP || Localization.sCurrentLanguage == Localization.LanguageType.Language_SPC || Localization.sCurrentLanguage == Localization.LanguageType.Language_IT || Localization.sCurrentLanguage == Localization.LanguageType.Language_PG || Localization.sCurrentLanguage == Localization.LanguageType.Language_PGB)
			{
				return ".";
			}
			if (Localization.sCurrentLanguage == Localization.LanguageType.Language_PL)
			{
				return "";
			}
			return ",";
		}

		public static int GetCurrentSeperateCount()
		{
			return 3;
		}

		public static string[] LanguageSuffix = new string[]
		{
			"_EN", "_FR", "_IT", "_GR", "_SP", "_CH", "_RU", "_PL", "_PG", "_SPC",
			"_CHT", "_PGB", ""
		};

		private static Localization.LanguageType sCurrentLanguage = Localization.LanguageType.Language_EN;

		public enum LanguageType
		{
			Language_EN,
			Language_FR,
			Language_IT,
			Language_GR,
			Language_SP,
			Language_CH,
			Language_RU,
			Language_PL,
			Language_PG,
			Language_SPC,
			Language_CHT,
			Language_PGB,
			Language_UNKNOWN
		}
	}
}
