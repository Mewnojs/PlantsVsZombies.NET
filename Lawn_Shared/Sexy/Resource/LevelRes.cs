using System;

namespace Sexy
{
	internal class LevelRes : BaseRes
	{
		public LevelRes()
		{
			mLevelNumber = -1;
		}

		public override void DeleteResource()
		{
			base.DeleteResource();
		}

		public int mLevelNumber;
	}
}
