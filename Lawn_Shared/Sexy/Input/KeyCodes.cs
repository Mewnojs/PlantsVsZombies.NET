using System;

namespace Sexy
{
    public enum KeyCode
    {
        Unknown,
        Lbutton,
        Rbutton,
        Cancel,
        Mbutton,
        Back = 8,
        Tab,
        Clear = 12,
        Return,
        Shift = 16,
        Control,
        Menu,
        Pause,
        Capital,
        Kana,
        Hangeul = 21,
        Hangul = 21,
        Junja = 23,
        Final,
        Hanja,
        Kanji = 25,
        Escape = 27,
        Convert,
        Nonconvert,
        Accept,
        Modechange,
        Space,
        Prior,
        Next,
        End,
        Home,
        Left,
        Up,
        Right,
        Down,
        Select,
        Print,
        Execute,
        Snapshot,
        Insert,
        Delete,
        Help,
        Asciibegin,
        Asciiend = 90,
        Lwin,
        Rwin,
        Apps,
        Numpad0 = 96,
        Numpad1,
        Numpad2,
        Numpad3,
        Numpad4,
        Numpad5,
        Numpad6,
        Numpad7,
        Numpad8,
        Numpad9,
        Multiply,
        Add,
        Separator,
        Subtract,
        Decimal,
        Divide,
        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,
        F10,
        F11,
        F12,
        F13,
        F14,
        F15,
        F16,
        F17,
        F18,
        F19,
        F20,
        F21,
        F22,
        F23,
        F24,
        Numlock = 144,
        Scroll,
        Asciibegin2 = 179,
        Asciiend2 = 224
    }

    public static class KeyCodes
    {
        public static KeyCode GetKeyCodeFromName(string theKeyName)
        {
            string aKeyName;// = new string(new char[MAX_KEYNAME_LEN]);

            if (theKeyName.Length >= MAX_KEYNAME_LEN - 1)
            {
                return KeyCode.Unknown;
            }

            aKeyName = theKeyName.ToUpper();

            if (theKeyName.Length == 1)
            {
                byte aKeyNameChar = (byte)aKeyName[0];

                if ((aKeyNameChar >= (byte)KeyCode.Asciibegin) && (aKeyNameChar <= (byte)KeyCode.Asciiend))
                {
                    return (KeyCode)aKeyNameChar;
                }

                if ((aKeyNameChar >= ((byte)KeyCode.Asciibegin2) - 0x80) && (aKeyNameChar <= ((byte)KeyCode.Asciiend2) - 0x80))
                {
                    return (KeyCode)(aKeyNameChar + 0x80);
                }
            }

            for (int i = 0; i < aKeyCodeArray.Length; i++)
            {
                if (string.Compare(aKeyName, aKeyCodeArray[i].mKeyName) == 0)
                {
                    return aKeyCodeArray[i].mKeyCode;
                }
            }

            return KeyCode.Unknown;
        }
        public static string GetKeyNameFromCode(KeyCode theKeyCode)
        {
            if ((theKeyCode >= KeyCode.Asciibegin) && (theKeyCode <= KeyCode.Asciiend))
            {
                char[] aStr = { (char)theKeyCode, '\0' };
                return aStr.ToString();
            }

            if ((theKeyCode >= KeyCode.Asciibegin2) && (theKeyCode <= KeyCode.Asciiend2))
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
        new KeyNameEntry("UNKNOWN", KeyCode.Unknown),
        new KeyNameEntry("LBUTTON", KeyCode.Lbutton),
        new KeyNameEntry("RBUTTON", KeyCode.Rbutton),
        new KeyNameEntry("CANCEL", KeyCode.Cancel),
        new KeyNameEntry("MBUTTON", KeyCode.Mbutton),
        new KeyNameEntry("BACK", KeyCode.Back),
        new KeyNameEntry("TAB", KeyCode.Tab),
        new KeyNameEntry("CLEAR", KeyCode.Clear),
        new KeyNameEntry("RETURN", KeyCode.Return),
        new KeyNameEntry("SHIFT", KeyCode.Shift),
        new KeyNameEntry("CONTROL", KeyCode.Control),
        new KeyNameEntry("MENU", KeyCode.Menu),
        new KeyNameEntry("PAUSE", KeyCode.Pause),
        new KeyNameEntry("CAPITAL", KeyCode.Capital),
        new KeyNameEntry("KANA", KeyCode.Kana),
        new KeyNameEntry("HANGEUL", KeyCode.Hangeul),
        new KeyNameEntry("HANGUL", KeyCode.Hangul),
        new KeyNameEntry("JUNJA", KeyCode.Junja),
        new KeyNameEntry("FINAL", KeyCode.Final),
        new KeyNameEntry("HANJA", KeyCode.Hanja),
        new KeyNameEntry("KANJI", KeyCode.Kanji),
        new KeyNameEntry("ESCAPE", KeyCode.Escape),
        new KeyNameEntry("CONVERT", KeyCode.Convert),
        new KeyNameEntry("NONCONVERT", KeyCode.Nonconvert),
        new KeyNameEntry("ACCEPT", KeyCode.Accept),
        new KeyNameEntry("MODECHANGE", KeyCode.Modechange),
        new KeyNameEntry("SPACE", KeyCode.Space),
        new KeyNameEntry("PRIOR", KeyCode.Prior),
        new KeyNameEntry("NEXT", KeyCode.Next),
        new KeyNameEntry("END", KeyCode.End),
        new KeyNameEntry("HOME", KeyCode.Home),
        new KeyNameEntry("LEFT", KeyCode.Left),
        new KeyNameEntry("UP", KeyCode.Up),
        new KeyNameEntry("RIGHT", KeyCode.Right),
        new KeyNameEntry("DOWN", KeyCode.Down),
        new KeyNameEntry("SELECT", KeyCode.Select),
        new KeyNameEntry("PRINT", KeyCode.Print),
        new KeyNameEntry("EXECUTE", KeyCode.Execute),
        new KeyNameEntry("SNAPSHOT", KeyCode.Snapshot),
        new KeyNameEntry("INSERT", KeyCode.Insert),
        new KeyNameEntry("DELETE", KeyCode.Delete),
        new KeyNameEntry("HELP", KeyCode.Help),
        new KeyNameEntry("LWIN", KeyCode.Lwin),
        new KeyNameEntry("RWIN", KeyCode.Rwin),
        new KeyNameEntry("APPS", KeyCode.Apps),
        new KeyNameEntry("NUMPAD0", KeyCode.Numpad0),
        new KeyNameEntry("NUMPAD1", KeyCode.Numpad1),
        new KeyNameEntry("NUMPAD2", KeyCode.Numpad2),
        new KeyNameEntry("NUMPAD3", KeyCode.Numpad3),
        new KeyNameEntry("NUMPAD4", KeyCode.Numpad4),
        new KeyNameEntry("NUMPAD5", KeyCode.Numpad5),
        new KeyNameEntry("NUMPAD6", KeyCode.Numpad6),
        new KeyNameEntry("NUMPAD7", KeyCode.Numpad7),
        new KeyNameEntry("NUMPAD8", KeyCode.Numpad8),
        new KeyNameEntry("NUMPAD9", KeyCode.Numpad9),
        new KeyNameEntry("MULTIPLY", KeyCode.Multiply),
        new KeyNameEntry("ADD", KeyCode.Add),
        new KeyNameEntry("SEPARATOR", KeyCode.Separator),
        new KeyNameEntry("SUBTRACT", KeyCode.Subtract),
        new KeyNameEntry("DECIMAL", KeyCode.Decimal),
        new KeyNameEntry("DIVIDE", KeyCode.Divide),
        new KeyNameEntry("F1", KeyCode.F1),
        new KeyNameEntry("F2", KeyCode.F2),
        new KeyNameEntry("F3", KeyCode.F3),
        new KeyNameEntry("F4", KeyCode.F4),
        new KeyNameEntry("F5", KeyCode.F5),
        new KeyNameEntry("F6", KeyCode.F6),
        new KeyNameEntry("F7", KeyCode.F7),
        new KeyNameEntry("F8", KeyCode.F8),
        new KeyNameEntry("F9", KeyCode.F9),
        new KeyNameEntry("F10", KeyCode.F10),
        new KeyNameEntry("F11", KeyCode.F11),
        new KeyNameEntry("F12", KeyCode.F12),
        new KeyNameEntry("F13", KeyCode.F13),
        new KeyNameEntry("F14", KeyCode.F14),
        new KeyNameEntry("F15", KeyCode.F15),
        new KeyNameEntry("F16", KeyCode.F16),
        new KeyNameEntry("F17", KeyCode.F17),
        new KeyNameEntry("F18", KeyCode.F18),
        new KeyNameEntry("F19", KeyCode.F19),
        new KeyNameEntry("F20", KeyCode.F20),
        new KeyNameEntry("F21", KeyCode.F21),
        new KeyNameEntry("F22", KeyCode.F22),
        new KeyNameEntry("F23", KeyCode.F23),
        new KeyNameEntry("F24", KeyCode.F24),
        new KeyNameEntry("NUMLOCK", KeyCode.Numlock),
        new KeyNameEntry("SCROLL", KeyCode.Scroll)
    };
    }
}
