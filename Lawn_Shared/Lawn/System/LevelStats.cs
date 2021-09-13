using System;

namespace Lawn
{
	public/*internal*/ class LevelStats
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
