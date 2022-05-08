using System;

namespace Lawn
{
    public/*internal*/ class ZombieAllowedLevels
    {
        public ZombieAllowedLevels(ZombieType aZombieType, int[] levels)
        {
            mZombieType = aZombieType;
            mAllowedOnLevel = levels;
        }

        public ZombieType mZombieType;

        public int[] mAllowedOnLevel = new int[50];
    }
}
