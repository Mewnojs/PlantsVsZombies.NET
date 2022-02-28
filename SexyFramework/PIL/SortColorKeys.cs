using System;
using System.Collections.Generic;

namespace Sexy.PIL
{
	public class SortColorKeys : Comparer<ColorKeyTimeEntry>
	{
		public override int Compare(ColorKeyTimeEntry x, ColorKeyTimeEntry y)
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
