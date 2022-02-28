using System;
using System.Collections.Generic;

namespace Sexy.PIL
{
	public class SortOldestParticles : Comparer<Particle>
	{
		public override int Compare(Particle x, Particle y)
		{
			if (x.mUpdateCount < y.mUpdateCount)
			{
				return 1;
			}
			if (x.mUpdateCount > y.mUpdateCount)
			{
				return -1;
			}
			return 0;
		}
	}
}
