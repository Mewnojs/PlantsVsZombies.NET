using System;

namespace Sexy.Resource
{
	public class CharStack
	{
		public int Count
		{
			get
			{
				return this.mCount;
			}
		}

		public char Peek()
		{
			return this.mCharStack[this.mStart];
		}

		public void Pop()
		{
			this.mStart--;
			this.mCount--;
		}

		public void Push(char c)
		{
			this.mStart++;
			this.mCharStack[this.mStart] = c;
			this.mCount++;
		}

		public void Clear()
		{
			this.mCount = 0;
			this.mStart = 0;
			for (int i = 0; i < 2097152; i++)
			{
				this.mCharStack[i] = '0';
			}
		}

		private char[] mCharStack = new char[2097152];

		private int mCount;

		private int mStart;
	}
}
