using System;
using System.Collections.Generic;

namespace Sexy.PIL
{
	public class LifeFrameSort : Comparer<LifetimeSettingKeyFrame>
	{
		public override int Compare(LifetimeSettingKeyFrame x, LifetimeSettingKeyFrame y)
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
