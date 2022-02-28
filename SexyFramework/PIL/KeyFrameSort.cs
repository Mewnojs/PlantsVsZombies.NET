using System;
using System.Collections.Generic;

namespace Sexy.PIL
{
	public class KeyFrameSort : Comparer<KeyFrame>
	{
		public override int Compare(KeyFrame x, KeyFrame y)
		{
			if (x.first < y.first)
			{
				return -1;
			}
			if (x.first > y.first)
			{
				return 1;
			}
			return 0;
		}
	}
}
