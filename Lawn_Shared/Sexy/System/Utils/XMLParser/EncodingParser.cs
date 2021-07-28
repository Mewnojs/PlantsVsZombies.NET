using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace Sexy
{
	internal class EncodingParser
	{
		public EncodingParser()
		{
			this.mStream = null;
			this.mFirstChar = false;
		}

		public virtual void Dispose()
		{
			if (this.mStream != null)
			{
				this.mStream.Close();
				this.mStream = null;
			}
		}

		public virtual bool OpenFile(string theFileName)
		{
			try
			{
				this.mStream = new StreamReader(TitleContainer.OpenStream(theFileName));
			}
			catch
			{
				this.mStream = null;
				return false;
			}
			this.mFirstChar = true;
			return true;
		}

		public virtual bool CloseFile()
		{
			if (this.mStream != null)
			{
				this.mStream.Close();
				this.mStream = null;
				return true;
			}
			return false;
		}

		public virtual bool EndOfFile()
		{
			return this.mBufferedText.Count <= 0 && (this.mStream == null || this.mStream.EndOfStream);
		}

		public virtual void SetStringSource(string theString)
		{
			int length = theString.Length;
			this.mBufferedText.Capacity = length;
			for (int i = 0; i < length; i++)
			{
				this.mBufferedText[i] = theString[length - i - 1];
			}
		}

		public virtual EncodingParser.GetCharReturnType GetChar(ref char theChar)
		{
			if (this.mBufferedText.Count != 0)
			{
				theChar = this.mBufferedText[0];
				this.mBufferedText.RemoveAt(0);
				return EncodingParser.GetCharReturnType.SUCCESSFUL;
			}
			if (this.mStream == null || this.mStream.EndOfStream)
			{
				return EncodingParser.GetCharReturnType.END_OF_FILE;
			}
			bool flag = false;
			if (this.GetNextChar(ref theChar))
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
			int num = this.mStream.Read();
			if (num == -1)
			{
				return false;
			}
			theChar = (char)num;
			return true;
		}

		public virtual bool PutChar(char theChar)
		{
			this.mBufferedText.Add(theChar);
			return true;
		}

		public virtual bool PutString(string theString)
		{
			this.mBufferedText.AddRange(theString);
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
