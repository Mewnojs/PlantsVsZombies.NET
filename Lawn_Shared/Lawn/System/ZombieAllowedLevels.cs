using System;

namespace Lawn
{
	internal class ZombieAllowedLevels
	{
		public ZombieAllowedLevels(ZombieType aZombieType, int[] levels)
		{
			this.mZombieType = aZombieType;
			this.mAllowedOnLevel = levels;
		}

		public ZombieType mZombieType;

		public int[] mAllowedOnLevel = new int[50];
	}
}
