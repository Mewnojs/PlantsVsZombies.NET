using System;
using System.Collections.Generic;
using System.Linq;

namespace Sexy.GraphicsLib
{
	public class CharDataHashTable
	{
		public CharDataHashTable()
		{
			this.mOrderedHash = false;
		}

		public CharData GetCharData(char inChar, bool inAllowAdd)
		{
			CharData result;
			if (this.mCharDataHash.TryGetValue(inChar, out result))
			{
				return result;
			}
			if (!inAllowAdd)
			{
				return null;
			}
			CharData charData = new CharData();
			this.mCharDataHash[inChar] = charData;
			charData.mChar = (ushort)inChar;
			return charData;
		}

		public int CharCount()
		{
			return this.mCharDataHash.Count;
		}

		public CharData[] ToArray()
		{
			return Enumerable.ToArray<CharData>(this.mCharDataHash.Values);
		}

		private Dictionary<char, CharData> mCharDataHash = new Dictionary<char, CharData>();

		public bool mOrderedHash;
	}
}
