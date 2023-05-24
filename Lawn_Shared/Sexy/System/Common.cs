using Lawn;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sexy
{
    public/*internal*/ struct SexyChar
    {
        public SexyChar(char c)
        {
            value_type = c;
        }

        public static bool operator ==(SexyChar a, KeyCode b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(SexyChar a, KeyCode b)
        {
            return !(a == b);
        }

        public static bool operator ==(SexyChar a, uint b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(SexyChar a, uint b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is SexyChar)
            {
                return value_type == ((SexyChar)obj).value_type;
            }
            if (obj is KeyCode)
            {
                KeyCode keyCode = (KeyCode)obj;
                return false;
            }
            if (obj is char || obj is uint) 
            {
                return value_type.Equals(obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return value_type.GetHashCode();
        }

        public override string ToString()
        {
            return value_type.ToString();
        }

        public static implicit operator char(SexyChar value) 
        {
            return value.value_type;
        }

        public char value_type;
    }

    internal static class Common
    {
        public static double _wtof(string str)
        {
            return Convert.ToDouble(str);
        }

        public static double _wtof(char str)
        {
            return Convert.ToDouble(str);
        }

        public static int stricmp(string s1, string s2)
        {
            return string.Compare(s1, s2);
        }

        public static void SetCommaSeparator(char theSeparator)
        {
            Common.CommaChar = theSeparator;
        }

        public static string CommaSeperate(int theValue)
        {
            return GlobalStaticVars.CommaSeperate_(theValue);
        }

        public static void inlineUpper(ref string theData)
        {
            theData = theData.ToUpper();
        }

        public static void inlineLower(ref string theData)
        {
            theData = theData.ToLower();
        }

        public static string URLEncode(string theString)
        {
            char[] array = new char[]
            {
                '0',
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                'A',
                'B',
                'C',
                'D',
                'E',
                'F'
            };
            string text = theString;
            int i = 0;
            while (i < theString.Length)
            {
                char c = theString[i];
                char c2 = c;
                if (c2 <= ' ')
                {
                    switch (c2)
                    {
                    case '\t':
                    case '\n':
                    case '\r':
                        goto IL_8D;
                    case '\v':
                    case '\f':
                        goto IL_D3;
                    default:
                        if (c2 != ' ')
                        {
                            goto IL_D3;
                        }
                        text.Insert(theString.Length - 1, "+");
                        break;
                    }
                }
                else
                {
                    switch (c2)
                    {
                    case '%':
                    case '&':
                        goto IL_8D;
                    default:
                        if (c2 != '+' && c2 != '?')
                        {
                            goto IL_D3;
                        }
                        goto IL_8D;
                    }
                }
            IL_E6:
                i++;
                continue;
            IL_8D:
                text = text.Insert(theString.Length, "%");
                text = text.Insert(theString.Length, Convert.ToString(array[c >> 4 & '\u000f']));
                text = text.Insert(theString.Length, Convert.ToString(array[c & '\u000f']));
                goto IL_E6;
            IL_D3:
                text = text.Insert(theString.Length, Convert.ToString(c));
                goto IL_E6;
            }
            return text;
        }

        public static string StringToUpper(ref string theString)
        {
            string text;
            if (!Common.StringToUpperCache.TryGetValue(theString, out text))
            {
                text = theString.ToUpper();
                Common.StringToUpperCache.Add(theString, text);
            }
            return text;
        }

        public static string StringToLower(ref string theString)
        {
            string text;
            if (!Common.StringToLowerCache.TryGetValue(theString, out text))
            {
                text = theString.ToLower();
                Common.StringToLowerCache.Add(theString, text);
            }
            return text;
        }

        public static string Upper(string theString)
        {
            return Common.StringToUpper(ref theString);
        }

        public static string Lower(string theString)
        {
            return Common.StringToLower(ref theString);
        }

        public static string Trim(string theString)
        {
            int num = 0;
            while (num < theString.Length && char.IsWhiteSpace(theString[num]))
            {
                num++;
            }
            int num2 = theString.Length - 1;
            while (num2 >= 0 && char.IsWhiteSpace(theString[num2]))
            {
                num2--;
            }
            return theString.Substring(num, num2 - num + 1);
        }

        public static string ToString(string theString)
        {
            return theString;
        }

        public static char CharAtStringIndex(string theString, int theIndex)
        {
            Debug.ASSERT(theIndex <= theString.Length);
            int num = 0;
            for (int i = 0; i < theString.Length; i++)
            {
                char result = theString[i];
                if (num == theIndex)
                {
                    return result;
                }
                num++;
            }
            return '\0';
        }

        public static bool StringToInt(string theString, ref int theIntVal)
        {
            theIntVal = Convert.ToInt32(theString, 10);
            return true;
        }

        public static bool StringToDouble(string theString, ref double theDoubleVal)
        {
            theDoubleVal = Convert.ToDouble(theString);
            return true;
        }

        public static string XMLDecodeString(string theString)
        {
            string text = "";
            for (int i = 0; i < theString.Length; i++)
            {
                sbyte b = (sbyte)Common.CharAtStringIndex(theString, i);
                if (b == 38)
                {
                    int num = theString.IndexOf(';', i);
                    if (num != -1)
                    {
                        string text2 = theString.Substring(i + 1, num - i - 1);
                        i = num;
                        if (text2 == "lt")
                        {
                            b = 60;
                        }
                        else if (text2 == "amp")
                        {
                            b = 38;
                        }
                        else if (text2 == "gt")
                        {
                            b = 62;
                        }
                        else if (text2 == "quot")
                        {
                            b = 34;
                        }
                        else if (text2 == "apos")
                        {
                            b = 39;
                        }
                        else if (text2 == "nbsp")
                        {
                            b = 32;
                        }
                        else if (text2 == "cr")
                        {
                            b = 10;
                        }
                    }
                }
                text.Insert(text.Length, Convert.ToString(b));
            }
            return text;
        }

        public static string XMLEncodeString(string theString)
        {
            string text = "";
            bool flag = false;
            int i = 0;
            while (i < theString.Length)
            {
                sbyte b = (sbyte)Common.CharAtStringIndex(theString, i);
                if (b != 32)
                {
                    flag = false;
                    goto IL_37;
                }
                if (!flag)
                {
                    flag = true;
                    goto IL_37;
                }
                text += "&nbsp;";
            IL_D9:
                i++;
                continue;
            IL_37:
                sbyte b2 = b;
                if (b2 <= 34)
                {
                    if (b2 == 10)
                    {
                        text += "&cr;";
                        goto IL_D9;
                    }
                    if (b2 == 34)
                    {
                        text += "&quot;";
                        goto IL_D9;
                    }
                }
                else
                {
                    switch (b2)
                    {
                    case 38:
                        text += "&amp;";
                        goto IL_D9;
                    case 39:
                        text += "&apos;";
                        goto IL_D9;
                    default:
                        switch (b2)
                        {
                        case 60:
                            text += "&lt;";
                            goto IL_D9;
                        case 62:
                            text += "&gt;";
                            goto IL_D9;
                        }
                        break;
                    }
                }
                text += b;
                goto IL_D9;
            }
            return text;
        }

        public static string GetFileName(string thePath)
        {
            return Common.GetFileName(thePath, false);
        }

        public static string GetFileName(string thePath, bool noExtension)
        {
            if (noExtension)
            {
                return Path.GetFileNameWithoutExtension(thePath);
            }
            return Path.GetFileName(thePath);
        }

        public static string GetFileDir(string thePath)
        {
            return Common.GetFileDir(thePath, false);
        }

        public static string GetFileDir(string thePath, bool withSlash)
        {
            int num = thePath.LastIndexOf('/');
            string text = thePath.Substring(0, num);
            if (withSlash)
            {
                text = text.Substring(0, text.Length - 1);
            }
            return text;
        }

        public static string GetPathFrom(string theRelPath, string theDir)
        {
            return Path.GetFullPath(theRelPath);
        }

        public static string GetFullPath(string theRelPath)
        {
            return Common.GetPathFrom(theRelPath, Common.GetCurDir());
        }

        public static string GetCurDir()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string RemoveTrailingSlash(string theDirectory)
        {
            int length = theDirectory.Length;
            if (length > 0 && theDirectory[length - 1] == '/')
            {
                return theDirectory.Substring(0, length - 1);
            }
            return theDirectory;
        }

        public static string AddTrailingSlash(string theDirectory)
        {
            if (string.IsNullOrEmpty(theDirectory))
            {
                return "";
            }
            sbyte b = (sbyte)theDirectory[theDirectory.Length - 1];
            if (b != 47)
            {
                return theDirectory + '/';
            }
            return theDirectory;
        }

        public static bool FileExists(string theFileName)
        {
            string applicationStoragePath = GlobalStaticVars.gSexyAppBase.applicationStoragePath;
            return File.Exists(Path.Combine(applicationStoragePath, theFileName));
        }

        public static long GetFileDate(string theFileName)
        {
            Debug.ASSERT(Common.FileExists(theFileName));
            string applicationStoragePath = GlobalStaticVars.gSexyAppBase.applicationStoragePath;
            return File.GetLastWriteTime(Path.Combine(applicationStoragePath, theFileName)).ToFileTime();
        }

        public static void Sleep(uint inTime)
        {
        }

        public static void MkDir(string theDir)
        {
            int num = 0;
            //IsolatedStorageFile userStoreForApplication;  // Storage location prmanantly moved to LawnApp.applicationStoragePath
            string applicationStoragePath = GlobalStaticVars.gSexyAppBase.applicationStoragePath;  
            for (; ; )
            {
                int num2 = theDir.IndexOf('/', num);
                //userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
                if (num2 == -1)
                {
                    break;
                }
                num = num2 + 1;
                theDir.Substring(0, num2);
                //userStoreForApplication.CreateDirectory(theDir);
                Directory.CreateDirectory(Path.Combine(applicationStoragePath, theDir));
            }
            //userStoreForApplication.CreateDirectory(theDir);
            Directory.CreateDirectory(Path.Combine(applicationStoragePath, theDir));
        }

        public static bool Deltree(string thePath)
        {
            string applicationStoragePath = GlobalStaticVars.gSexyAppBase.applicationStoragePath;
            string aPathCombined = Path.Combine(applicationStoragePath, thePath);
            if (Directory.Exists(aPathCombined))
            {
                Directory.Delete(aPathCombined, true);
                return true;
            }
            return false;
        }

        public static bool DeleteFile(string lpFileName)
        {
            //IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
            string applicationStoragePath = GlobalStaticVars.gSexyAppBase.applicationStoragePath;
            //if (userStoreForApplication.FileExists(lpFileName))
            string aFileNameCombined = Path.Combine(applicationStoragePath, lpFileName);
            if (File.Exists(aFileNameCombined))
            {
                //userStoreForApplication.DeleteFile(lpFileName);
                File.Delete(aFileNameCombined);
                return true;
            }
            return false;
        }

        public static string GetAppDataFolder()
        {
            return Common.gAppDataFolder;
        }

        public static void SetAppDataFolder(string thePath)
        {
            Common.gAppDataFolder = Common.AddTrailingSlash(thePath);
        }

        public static string StrFormat_(string fmt, params object[] LegacyParamArray)
        {
            return string.Format(fmt, LegacyParamArray);
        }

        public static string StrFormat_(string fmt, object a)
        {
            return string.Format(fmt, a.ToString());
        }

        public static string StrFormat_(string fmt, object a, object b)
        {
            return string.Format(fmt, a, b);
        }

        public static string StrFormat_(string fmt, object a, object b, object c)
        {
            return string.Format(fmt, a, b, c);
        }

        public const uint SEXY_RAND_MAX = 2147483647U;

        private static string gAppDataFolder = "boogers";

        internal static char CommaChar = ',';

        private static Dictionary<string, string> StringToUpperCache = new Dictionary<string, string>(50);

        private static Dictionary<string, string> StringToLowerCache = new Dictionary<string, string>(50);

        internal class StringLessNoCase
        {
            public bool Equals(string s1, string s2)
            {
                return Common.stricmp(s1, s2) < 0;
            }
        }

        internal class StringEqualNoCase
        {
            public bool Equals(string s1, string s2)
            {
                return Common.stricmp(s1, s2) == 0;
            }
        }

        internal class StringGreaterNoCase
        {
            public bool Equals(string s1, string s2)
            {
                return Common.stricmp(s1, s2) > 0;
            }
        }

        internal class CharToCharFunc
        {
            public static string Str(string theStr)
            {
                return theStr;
            }

            public static sbyte Char(sbyte theChar)
            {
                return theChar;
            }
        }
    }
}

