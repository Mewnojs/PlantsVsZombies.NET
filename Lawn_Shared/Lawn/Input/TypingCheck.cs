using System;
using Sexy;

namespace Lawn
{
	internal class TypingCheck
	{
		public TypingCheck()
		{
		}

		public TypingCheck(string thePhrase)
		{
			this.SetPhrase(thePhrase);
		}

		public void SetPhrase(string thePhrase)
		{
			for (int i = 0; i < thePhrase.Length; i++)
			{
				this.AddChar(thePhrase[i]);
			}
		}

		public void AddKeyCode(KeyCode theCode)
		{
			this.mPhrase += (sbyte)theCode;
		}

		public void AddChar(char theChar)
		{
		}

		public bool Check(KeyCode theCode)
		{
			this.mRecentTyping += (sbyte)theCode;
			int length = this.mPhrase.Length;
			if (length == 0)
			{
				return false;
			}
			if (this.mRecentTyping.Length > length)
			{
				this.mRecentTyping = this.mRecentTyping.Substring(1, length);
			}
			if (this.mRecentTyping == this.mPhrase)
			{
				this.mRecentTyping = string.Empty;
				return true;
			}
			return false;
		}

		public bool Check(sbyte theChar)
		{
			return false;
		}

		protected string mPhrase;

		protected string mRecentTyping;
	}
}
