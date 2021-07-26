using System;

namespace Lawn
{
	public struct ZombieDescriptor
	{
		public ZombieDescriptor(ZombieType theType, int aX, int aY)
		{
			this.type = theType;
			this.x = aX;
			this.y = aY;
		}

		public ZombieType type;

		public int x;

		public int y;
	}
}
