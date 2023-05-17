using System;

namespace Sexy
{
    public/*internal*/ static class StringFunctions
    {
        internal static string ChangeCharacter(string sourcestring, int charindex, char changechar)
        {
            return ((charindex > 0) ? sourcestring.Substring(0, charindex) : "") + changechar.ToString() + ((charindex < sourcestring.Length - 1) ? sourcestring.Substring(charindex + 1) : "");
        }

        internal static bool IsXDigit(char character)
        {
            return char.IsDigit(character) || "ABCDEFabcdef".IndexOf(character) > -1;
        }

        internal static string StrChr(string stringtosearch, char chartofind)
        {
            int num = stringtosearch.IndexOf(chartofind);
            if (num > -1)
            {
                return stringtosearch.Substring(num);
            }
            return null;
        }

        internal static string StrRChr(string stringtosearch, char chartofind)
        {
            int num = stringtosearch.LastIndexOf(chartofind);
            if (num > -1)
            {
                return stringtosearch.Substring(num);
            }
            return null;
        }

        internal static string StrStr(string stringtosearch, string stringtofind)
        {
            int num = stringtosearch.IndexOf(stringtofind);
            if (num > -1)
            {
                return stringtosearch.Substring(num);
            }
            return null;
        }

        internal static string StrTok(string stringtotokenize, string delimiters)
        {
            if (stringtotokenize != null)
            {
                StringFunctions.activestring = stringtotokenize;
                StringFunctions.activeposition = -1;
            }
            if (StringFunctions.activestring == null)
            {
                return null;
            }
            if (StringFunctions.activeposition == StringFunctions.activestring.Length)
            {
                return null;
            }
            StringFunctions.activeposition++;
            while (StringFunctions.activeposition < StringFunctions.activestring.Length && delimiters.IndexOf(StringFunctions.activestring[StringFunctions.activeposition]) > -1)
            {
                StringFunctions.activeposition++;
            }
            if (StringFunctions.activeposition == StringFunctions.activestring.Length)
            {
                return null;
            }
            int num = StringFunctions.activeposition;
            do
            {
                StringFunctions.activeposition++;
            }
            while (StringFunctions.activeposition < StringFunctions.activestring.Length && delimiters.IndexOf(StringFunctions.activestring[StringFunctions.activeposition]) == -1);
            return StringFunctions.activestring.Substring(num, StringFunctions.activeposition - num);
        }

        private static string activestring;

        private static int activeposition;
    }
}
