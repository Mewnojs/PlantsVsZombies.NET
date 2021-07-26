using System;

namespace Sexy
{
	internal class LevelRes : BaseRes
	{
		public LevelRes()
		{
			this.mLevelNumber = -1;
		}

		public override void DeleteResource()
		{
			base.DeleteResource();
		}

		public int mLevelNumber;
	}
}
