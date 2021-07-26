using System;

namespace Lawn
{
	internal class LevelStats
	{
		public LevelStats()
		{
			this.Reset();
		}

		public void Reset()
		{
			this.mUnusedLawnMowers = 0;
		}

		public int mUnusedLawnMowers;
	}
}
