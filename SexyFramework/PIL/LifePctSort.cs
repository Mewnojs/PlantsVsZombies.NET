using System;
using System.Collections.Generic;

namespace Sexy.PIL
{
	public class LifePctSort : Comparer<LifetimeSettingPct>
	{
		public override int Compare(LifetimeSettingPct x, LifetimeSettingPct y)
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
