using System;

namespace Lawn
{
    public/*internal*/ class ZombiePicker
    {
        public int mZombieCount;

        public int mZombiePoints;

        public int[] mZombieTypeCount = new int[(int)ZombieType.ZombieTypesCount];

        public int[] mAllWavesZombieTypeCount = new int[(int)ZombieType.ZombieTypesCount];
    }
}
