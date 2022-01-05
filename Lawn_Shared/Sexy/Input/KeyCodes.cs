using System;

namespace Sexy
{
	public enum KeyCode
	{
		KEYCODE_UNKNOWN,
		KEYCODE_LBUTTON,
		KEYCODE_RBUTTON,
		KEYCODE_CANCEL,
		KEYCODE_MBUTTON,
		KEYCODE_BACK = 8,
		KEYCODE_TAB,
		KEYCODE_CLEAR = 12,
		KEYCODE_RETURN,
		KEYCODE_SHIFT = 16,
		KEYCODE_CONTROL,
		KEYCODE_MENU,
		KEYCODE_PAUSE,
		KEYCODE_CAPITAL,
		KEYCODE_KANA,
		KEYCODE_HANGEUL = 21,
		KEYCODE_HANGUL = 21,
		KEYCODE_JUNJA = 23,
		KEYCODE_FINAL,
		KEYCODE_HANJA,
		KEYCODE_KANJI = 25,
		KEYCODE_ESCAPE = 27,
		KEYCODE_CONVERT,
		KEYCODE_NONCONVERT,
		KEYCODE_ACCEPT,
		KEYCODE_MODECHANGE,
		KEYCODE_SPACE,
		KEYCODE_PRIOR,
		KEYCODE_NEXT,
		KEYCODE_END,
		KEYCODE_HOME,
		KEYCODE_LEFT,
		KEYCODE_UP,
		KEYCODE_RIGHT,
		KEYCODE_DOWN,
		KEYCODE_SELECT,
		KEYCODE_PRINT,
		KEYCODE_EXECUTE,
		KEYCODE_SNAPSHOT,
		KEYCODE_INSERT,
		KEYCODE_DELETE,
		KEYCODE_HELP,
		KEYCODE_ASCIIBEGIN,
		KEYCODE_ASCIIEND = 90,
		KEYCODE_LWIN,
		KEYCODE_RWIN,
		KEYCODE_APPS,
		KEYCODE_NUMPAD0 = 96,
		KEYCODE_NUMPAD1,
		KEYCODE_NUMPAD2,
		KEYCODE_NUMPAD3,
		KEYCODE_NUMPAD4,
		KEYCODE_NUMPAD5,
		KEYCODE_NUMPAD6,
		KEYCODE_NUMPAD7,
		KEYCODE_NUMPAD8,
		KEYCODE_NUMPAD9,
		KEYCODE_MULTIPLY,
		KEYCODE_ADD,
		KEYCODE_SEPARATOR,
		KEYCODE_SUBTRACT,
		KEYCODE_DECIMAL,
		KEYCODE_DIVIDE,
		KEYCODE_F1,
		KEYCODE_F2,
		KEYCODE_F3,
		KEYCODE_F4,
		KEYCODE_F5,
		KEYCODE_F6,
		KEYCODE_F7,
		KEYCODE_F8,
		KEYCODE_F9,
		KEYCODE_F10,
		KEYCODE_F11,
		KEYCODE_F12,
		KEYCODE_F13,
		KEYCODE_F14,
		KEYCODE_F15,
		KEYCODE_F16,
		KEYCODE_F17,
		KEYCODE_F18,
		KEYCODE_F19,
		KEYCODE_F20,
		KEYCODE_F21,
		KEYCODE_F22,
		KEYCODE_F23,
		KEYCODE_F24,
		KEYCODE_NUMLOCK = 144,
		KEYCODE_SCROLL,
		KEYCODE_ASCIIBEGIN2 = 179,
		KEYCODE_ASCIIEND2 = 224
	}

	public static class KeyCodes
	{
		public static KeyCode GetKeyCodeFromName(string theKeyName)
		{
			string aKeyName = new string(new char[MAX_KEYNAME_LEN]);

			if (theKeyName.Length >= MAX_KEYNAME_LEN - 1)
			{
				return KeyCode.KEYCODE_UNKNOWN;
			}

			aKeyName = theKeyName;
			aKeyName.ToUpper();

			if (theKeyName.Length == 1)
			{
				byte aKeyNameChar = (byte)aKeyName[0];

				if ((aKeyNameChar >= (byte)KeyCode.KEYCODE_ASCIIBEGIN) && (aKeyNameChar <= (byte)KeyCode.KEYCODE_ASCIIEND))
				{
					return (KeyCode)aKeyNameChar;
				}

				if ((aKeyNameChar >= ((byte)KeyCode.KEYCODE_ASCIIBEGIN2) - 0x80) && (aKeyNameChar <= ((byte)KeyCode.KEYCODE_ASCIIEND2) - 0x80))
				{
					return (KeyCode)(aKeyNameChar + 0x80);
				}
			}

			//C++ TO C# CONVERTER WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
			//ORIGINAL LINE: for (int i = 0; i < sizeof(aKeyCodeArray)/sizeof(aKeyCodeArray[0]); i++)
			for (int i = 0; i < aKeyCodeArray.Length; i++)
			{
				if (string.Compare(aKeyName, aKeyCodeArray[i].mKeyName) == 0)
				{
					return aKeyCodeArray[i].mKeyCode;
				}
			}

			return KeyCode.KEYCODE_UNKNOWN;
		}
		public static string GetKeyNameFromCode(KeyCode theKeyCode)
		{
			if ((theKeyCode >= KeyCode.KEYCODE_ASCIIBEGIN) && (theKeyCode <= KeyCode.KEYCODE_ASCIIEND))
			{
				char[] aStr = { (char)theKeyCode, '\0' };
				return aStr.ToString();
			}

			if ((theKeyCode >= KeyCode.KEYCODE_ASCIIBEGIN2) && (theKeyCode <= KeyCode.KEYCODE_ASCIIEND2))
			{
				char[] aStr = { (char)(((byte)theKeyCode) - 0x80), '\0' };
				return aStr.ToString();
			}

			//C++ TO C# CONVERTER WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
			//ORIGINAL LINE: for (int i = 0; i < sizeof(aKeyCodeArray)/sizeof(aKeyCodeArray[0]); i++)
			for (int i = 0; i < aKeyCodeArray.Length; i++)
			{
				if (theKeyCode == aKeyCodeArray[i].mKeyCode)
				{
					return aKeyCodeArray[i].mKeyName;
				}
			}

			return "UNKNOWN";
		}
		public class KeyNameEntry
		{
			public string mKeyName = new string(new char[MAX_KEYNAME_LEN]);
			public KeyCode mKeyCode;

            public KeyNameEntry(string theKeyName, KeyCode theKeyCode)
            {
				mKeyName = theKeyName;
				mKeyCode = theKeyCode;
            }
        }
		public const int MAX_KEYNAME_LEN = 12;
		public static KeyNameEntry[] aKeyCodeArray =
	{
		new KeyNameEntry("UNKNOWN", KeyCode.KEYCODE_UNKNOWN),
		new KeyNameEntry("LBUTTON", KeyCode.KEYCODE_LBUTTON),
		new KeyNameEntry("RBUTTON", KeyCode.KEYCODE_RBUTTON),
		new KeyNameEntry("CANCEL", KeyCode.KEYCODE_CANCEL),
		new KeyNameEntry("MBUTTON", KeyCode.KEYCODE_MBUTTON),
		new KeyNameEntry("BACK", KeyCode.KEYCODE_BACK),
		new KeyNameEntry("TAB", KeyCode.KEYCODE_TAB),
		new KeyNameEntry("CLEAR", KeyCode.KEYCODE_CLEAR),
		new KeyNameEntry("RETURN", KeyCode.KEYCODE_RETURN),
		new KeyNameEntry("SHIFT", KeyCode.KEYCODE_SHIFT),
		new KeyNameEntry("CONTROL", KeyCode.KEYCODE_CONTROL),
		new KeyNameEntry("MENU", KeyCode.KEYCODE_MENU),
		new KeyNameEntry("PAUSE", KeyCode.KEYCODE_PAUSE),
		new KeyNameEntry("CAPITAL", KeyCode.KEYCODE_CAPITAL),
		new KeyNameEntry("KANA", KeyCode.KEYCODE_KANA),
		new KeyNameEntry("HANGEUL", KeyCode.KEYCODE_HANGEUL),
		new KeyNameEntry("HANGUL", KeyCode.KEYCODE_HANGUL),
		new KeyNameEntry("JUNJA", KeyCode.KEYCODE_JUNJA),
		new KeyNameEntry("FINAL", KeyCode.KEYCODE_FINAL),
		new KeyNameEntry("HANJA", KeyCode.KEYCODE_HANJA),
		new KeyNameEntry("KANJI", KeyCode.KEYCODE_KANJI),
		new KeyNameEntry("ESCAPE", KeyCode.KEYCODE_ESCAPE),
		new KeyNameEntry("CONVERT", KeyCode.KEYCODE_CONVERT),
		new KeyNameEntry("NONCONVERT", KeyCode.KEYCODE_NONCONVERT),
		new KeyNameEntry("ACCEPT", KeyCode.KEYCODE_ACCEPT),
		new KeyNameEntry("MODECHANGE", KeyCode.KEYCODE_MODECHANGE),
		new KeyNameEntry("SPACE", KeyCode.KEYCODE_SPACE),
		new KeyNameEntry("PRIOR", KeyCode.KEYCODE_PRIOR),
		new KeyNameEntry("NEXT", KeyCode.KEYCODE_NEXT),
		new KeyNameEntry("END", KeyCode.KEYCODE_END),
		new KeyNameEntry("HOME", KeyCode.KEYCODE_HOME),
		new KeyNameEntry("LEFT", KeyCode.KEYCODE_LEFT),
		new KeyNameEntry("UP", KeyCode.KEYCODE_UP),
		new KeyNameEntry("RIGHT", KeyCode.KEYCODE_RIGHT),
		new KeyNameEntry("DOWN", KeyCode.KEYCODE_DOWN),
		new KeyNameEntry("SELECT", KeyCode.KEYCODE_SELECT),
		new KeyNameEntry("PRINT", KeyCode.KEYCODE_PRINT),
		new KeyNameEntry("EXECUTE", KeyCode.KEYCODE_EXECUTE),
		new KeyNameEntry("SNAPSHOT", KeyCode.KEYCODE_SNAPSHOT),
		new KeyNameEntry("INSERT", KeyCode.KEYCODE_INSERT),
		new KeyNameEntry("DELETE", KeyCode.KEYCODE_DELETE),
		new KeyNameEntry("HELP", KeyCode.KEYCODE_HELP),
		new KeyNameEntry("LWIN", KeyCode.KEYCODE_LWIN),
		new KeyNameEntry("RWIN", KeyCode.KEYCODE_RWIN),
		new KeyNameEntry("APPS", KeyCode.KEYCODE_APPS),
		new KeyNameEntry("NUMPAD0", KeyCode.KEYCODE_NUMPAD0),
		new KeyNameEntry("NUMPAD1", KeyCode.KEYCODE_NUMPAD1),
		new KeyNameEntry("NUMPAD2", KeyCode.KEYCODE_NUMPAD2),
		new KeyNameEntry("NUMPAD3", KeyCode.KEYCODE_NUMPAD3),
		new KeyNameEntry("NUMPAD4", KeyCode.KEYCODE_NUMPAD4),
		new KeyNameEntry("NUMPAD5", KeyCode.KEYCODE_NUMPAD5),
		new KeyNameEntry("NUMPAD6", KeyCode.KEYCODE_NUMPAD6),
		new KeyNameEntry("NUMPAD7", KeyCode.KEYCODE_NUMPAD7),
		new KeyNameEntry("NUMPAD8", KeyCode.KEYCODE_NUMPAD8),
		new KeyNameEntry("NUMPAD9", KeyCode.KEYCODE_NUMPAD9),
		new KeyNameEntry("MULTIPLY", KeyCode.KEYCODE_MULTIPLY),
		new KeyNameEntry("ADD", KeyCode.KEYCODE_ADD),
		new KeyNameEntry("SEPARATOR", KeyCode.KEYCODE_SEPARATOR),
		new KeyNameEntry("SUBTRACT", KeyCode.KEYCODE_SUBTRACT),
		new KeyNameEntry("DECIMAL", KeyCode.KEYCODE_DECIMAL),
		new KeyNameEntry("DIVIDE", KeyCode.KEYCODE_DIVIDE),
		new KeyNameEntry("F1", KeyCode.KEYCODE_F1),
		new KeyNameEntry("F2", KeyCode.KEYCODE_F2),
		new KeyNameEntry("F3", KeyCode.KEYCODE_F3),
		new KeyNameEntry("F4", KeyCode.KEYCODE_F4),
		new KeyNameEntry("F5", KeyCode.KEYCODE_F5),
		new KeyNameEntry("F6", KeyCode.KEYCODE_F6),
		new KeyNameEntry("F7", KeyCode.KEYCODE_F7),
		new KeyNameEntry("F8", KeyCode.KEYCODE_F8),
		new KeyNameEntry("F9", KeyCode.KEYCODE_F9),
		new KeyNameEntry("F10", KeyCode.KEYCODE_F10),
		new KeyNameEntry("F11", KeyCode.KEYCODE_F11),
		new KeyNameEntry("F12", KeyCode.KEYCODE_F12),
		new KeyNameEntry("F13", KeyCode.KEYCODE_F13),
		new KeyNameEntry("F14", KeyCode.KEYCODE_F14),
		new KeyNameEntry("F15", KeyCode.KEYCODE_F15),
		new KeyNameEntry("F16", KeyCode.KEYCODE_F16),
		new KeyNameEntry("F17", KeyCode.KEYCODE_F17),
		new KeyNameEntry("F18", KeyCode.KEYCODE_F18),
		new KeyNameEntry("F19", KeyCode.KEYCODE_F19),
		new KeyNameEntry("F20", KeyCode.KEYCODE_F20),
		new KeyNameEntry("F21", KeyCode.KEYCODE_F21),
		new KeyNameEntry("F22", KeyCode.KEYCODE_F22),
		new KeyNameEntry("F23", KeyCode.KEYCODE_F23),
		new KeyNameEntry("F24", KeyCode.KEYCODE_F24),
		new KeyNameEntry("NUMLOCK", KeyCode.KEYCODE_NUMLOCK),
		new KeyNameEntry("SCROLL", KeyCode.KEYCODE_SCROLL)
	};
	}
}
