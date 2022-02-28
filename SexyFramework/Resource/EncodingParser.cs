using System;
using System.Collections.Generic;
using System.Text;

namespace Sexy.Resource
{
	public class EncodingParser : IDisposable
	{
		public EncodingParser()
		{
			this.mFile = null;
			this.mForcedEncodingType = false;
			this.mEncodingType = EncodingParser.EncodingType.UTF_8;
		}

		public virtual void Dispose()
		{
			this.mBufferedText.Clear();
			this.mBufferedText = null;
			if (this.mFile != null)
			{
				this.mFile.Close();
			}
		}

		public virtual void SetEncodingType(EncodingParser.EncodingType theEncoding)
		{
			this.mEncodingType = theEncoding;
		}

		public virtual void checkEncodingType(byte[] data)
		{
			this.mEncodingType = EncodingParser.EncodingType.ASCII;
			if (data.Length >= 2)
			{
				int num = (int)data[0];
				int num2 = (int)data[1];
				if ((num == 255 && num2 == 254) || (num == 254 && num2 == 255))
				{
					this.mEncodingType = EncodingParser.EncodingType.UTF_16;
				}
			}
			if (this.mEncodingType == EncodingParser.EncodingType.ASCII && data.Length >= 3)
			{
				int num3 = (int)data[0];
				int num4 = (int)data[1];
				int num5 = (int)data[2];
				if (num3 == 239 && num4 == 187 && num5 == 191)
				{
					this.mEncodingType = EncodingParser.EncodingType.UTF_8;
				}
			}
		}

		public virtual bool OpenFile(string theFilename)
		{
			this.mFile = new PFILE(theFilename, "rb");
			if (!this.mFile.Open())
			{
				this.mFile = null;
				return false;
			}
			byte[] data = this.mFile.GetData();
			if (data == null)
			{
				return false;
			}
			if (!this.mForcedEncodingType)
			{
				this.checkEncodingType(data);
			}
			this.SetBytes(data);
			return true;
		}

		public virtual bool CloseFile()
		{
			if (this.mFile != null)
			{
				this.mFile.Close();
			}
			return true;
		}

		public virtual bool EndOfFile()
		{
			return this.mBufferedText.Count <= 0 && (this.mFile == null || this.mFile.IsEndOfFile());
		}

		public virtual void SetStringSource(string theString)
		{
			int length = theString.Length;
			this.mBufferedText.Clear();
			for (int i = 0; i < length; i++)
			{
				this.mBufferedText.Push(theString[length - i - 1]);
			}
		}

		public virtual void SetBytes(byte[] data)
		{
			switch (this.mEncodingType)
			{
			case EncodingParser.EncodingType.ASCII:
			{
				char[] array = new char[data.Length];
				for (int i = 0; i < data.Length; i++)
				{
					array[i] = (char)data[i];
				}
				this.SetStringSource(new string(array));
				return;
			}
			case EncodingParser.EncodingType.UTF_8:
			{
				char[] chars = Encoding.UTF8.GetChars(data);
				this.SetStringSource(new string(chars));
				return;
			}
			case EncodingParser.EncodingType.UTF_16:
			{
				char[] chars2 = Encoding.Unicode.GetChars(data);
				this.SetStringSource(new string(chars2));
				return;
			}
			case EncodingParser.EncodingType.UTF_16_LE:
				throw new NotImplementedException();
			case EncodingParser.EncodingType.UTF_16_BE:
				throw new NotImplementedException();
			default:
				return;
			}
		}

		public virtual EncodingParser.GetCharReturnType GetChar(ref char theChar)
		{
			if (this.mBufferedText.Count != 0)
			{
				theChar = this.mBufferedText.Peek();
				this.mBufferedText.Pop();
				return EncodingParser.GetCharReturnType.SUCCESSFUL;
			}
			if (this.mFile == null || (this.mFile != null && !this.mFile.Open()))
			{
				return EncodingParser.GetCharReturnType.END_OF_FILE;
			}
			bool flag = false;
			if (flag)
			{
				return EncodingParser.GetCharReturnType.INVALID_CHARACTER;
			}
			return EncodingParser.GetCharReturnType.END_OF_FILE;
		}

		public virtual bool PutChar(char theChar)
		{
			this.mBufferedText.Push(theChar);
			return true;
		}

		public virtual bool PutString(string theString)
		{
			int length = theString.Length;
			for (int i = 0; i < length; i++)
			{
				this.mBufferedText.Push(theString[length - i - 1]);
			}
			return true;
		}

		protected PFILE mFile;

		private Stack<char> mBufferedText = new Stack<char>();

		private bool mForcedEncodingType;

		private EncodingParser.EncodingType mEncodingType;

		public enum EncodingType
		{
			ASCII,
			UTF_8,
			UTF_16,
			UTF_16_LE,
			UTF_16_BE
		}

		public enum GetCharReturnType
		{
			SUCCESSFUL,
			INVALID_CHARACTER,
			END_OF_FILE,
			FAILURE
		}

		private delegate bool GetCharFunc(char theChar, ref bool error);
	}
}
