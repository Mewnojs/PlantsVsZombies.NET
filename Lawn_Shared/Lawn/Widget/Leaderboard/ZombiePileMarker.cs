using System;
using Microsoft.Xna.Framework.GamerServices;

namespace Lawn
{
	internal class ZombiePileMarker
	{
		public ZombiePileMarker()
		{
			this.mHeight = 0;
			this.mGamer = null;
		}

		public Gamer mGamer;

		public int mHeight;
	}
}
