using System;
using Sexy;

namespace Lawn
{
	public/*internal*/ class TypingCheck
	{
		public TypingCheck()
		{
		}

		public TypingCheck(string thePhrase)
		{
			SetPhrase(thePhrase);
		}

		public void SetPhrase(string thePhrase)
		{
			for (int i = 0; i < thePhrase.Length; i++)
			{
				AddChar(thePhrase[i]);
			}
		}

		public void AddKeyCode(KeyCode theCode)
		{
			mPhrase += (sbyte)theCode;
		}

		public void AddChar(char theChar)
		{
		}

		public bool Check(KeyCode theCode)
		{
			mRecentTyping += (sbyte)theCode;
			int length = mPhrase.Length;
			if (length == 0)
			{
				return false;
			}
			if (mRecentTyping.Length > length)
			{
				mRecentTyping = mRecentTyping.Substring(1, length);
			}
			if (mRecentTyping == mPhrase)
			{
				mRecentTyping = string.Empty;
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
