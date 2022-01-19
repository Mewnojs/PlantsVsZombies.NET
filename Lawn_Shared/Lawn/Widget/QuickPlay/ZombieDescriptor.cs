using System;

namespace Lawn
{
    public struct ZombieDescriptor
    {
        public ZombieDescriptor(ZombieType theType, int aX, int aY)
        {
            type = theType;
            x = aX;
            y = aY;
        }

        public ZombieType type;

        public int x;

        public int y;
    }
}
