using System;

namespace Sexy.GraphicsLib
{
	public class SortedKern : IComparable
	{
		public SortedKern()
		{
			this.mKey = '0';
			this.mValue = '0';
			this.mOffset = 0;
		}

		public SortedKern(char inKey, char inValue, int inOffset)
		{
			this.mKey = inKey;
			this.mValue = inValue;
			this.mOffset = inOffset;
		}

		public int CompareTo(object obj)
		{
			SortedKern sortedKern = obj as SortedKern;
			if (this.mKey < sortedKern.mKey)
			{
				return -1;
			}
			if (this.mKey > sortedKern.mKey)
			{
				return 1;
			}
			if (this.mValue < sortedKern.mValue)
			{
				return -1;
			}
			if (this.mValue > sortedKern.mValue)
			{
				return 1;
			}
			return 0;
		}

		private static int Compare(SortedKern a, SortedKern b)
		{
			if (a.mKey < b.mKey)
			{
				return -1;
			}
			if (a.mKey > b.mKey)
			{
				return 1;
			}
			if (a.mValue < b.mValue)
			{
				return -1;
			}
			if (a.mValue > b.mValue)
			{
				return 1;
			}
			return 0;
		}

		public char mKey;

		public char mValue;

		public int mOffset;
	}
}
