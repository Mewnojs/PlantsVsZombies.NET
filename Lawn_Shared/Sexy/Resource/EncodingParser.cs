using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace Sexy
{
    public/*internal*/ class EncodingParser
    {
        public EncodingParser()
        {
            mStream = null;
            mFirstChar = false;
        }

        public virtual void Dispose()
        {
            if (mStream != null)
            {
                mStream.Close();
                mStream = null;
            }
        }

        public virtual bool OpenFile(string theFileName)
        {
            try
            {
                mStream = new StreamReader(TitleContainer.OpenStream(theFileName));
            }
            catch
            {
                mStream = null;
                return false;
            }
            mFirstChar = true;
            return true;
        }

        public virtual bool CloseFile()
        {
            if (mStream != null)
            {
                mStream.Close();
                mStream = null;
                return true;
            }
            return false;
        }

        public virtual bool EndOfFile()
        {
            return mBufferedText.Count <= 0 && (mStream == null || mStream.EndOfStream);
        }

        public virtual void SetStringSource(string theString)
        {
            int length = theString.Length;
            mBufferedText.Capacity = length;
            for (int i = 0; i < length; i++)
            {
                mBufferedText[i] = theString[length - i - 1];
            }
        }

        public virtual EncodingParser.GetCharReturnType GetChar(ref char theChar)
        {
            if (mBufferedText.Count != 0)
            {
                theChar = mBufferedText[0];
                mBufferedText.RemoveAt(0);
                return EncodingParser.GetCharReturnType.SUCCESSFUL;
            }
            if (mStream == null || mStream.EndOfStream)
            {
                return EncodingParser.GetCharReturnType.END_OF_FILE;
            }
            bool flag = false;
            if (GetNextChar(ref theChar))
            {
                return EncodingParser.GetCharReturnType.SUCCESSFUL;
            }
            if (flag)
            {
                return EncodingParser.GetCharReturnType.INVALID_CHARACTER;
            }
            return EncodingParser.GetCharReturnType.END_OF_FILE;
        }

        private bool GetNextChar(ref char theChar)
        {
            int num = mStream.Read();
            if (num == -1)
            {
                return false;
            }
            theChar = (char)num;
            return true;
        }

        public virtual bool PutChar(char theChar)
        {
            mBufferedText.Add(theChar);
            return true;
        }

        public virtual bool PutString(string theString)
        {
            mBufferedText.AddRange(theString);
            return true;
        }

        private StreamReader mStream;

        private List<char> mBufferedText = new List<char>();

        private bool mFirstChar;

        public enum GetCharReturnType
        {
            SUCCESSFUL,
            INVALID_CHARACTER,
            END_OF_FILE,
            FAILURE
        }
    }
}
