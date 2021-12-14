using System;

namespace Lawn
{
	public/*internal*/ class LevelStats
	{
		public LevelStats()
		{
			Reset();
		}

		public void Reset()
		{
			mUnusedLawnMowers = 0;
		}

		public int mUnusedLawnMowers;
	}
}
