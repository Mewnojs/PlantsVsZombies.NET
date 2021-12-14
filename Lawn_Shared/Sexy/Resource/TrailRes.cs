using System;
using Sexy.TodLib;

namespace Sexy
{
	internal class TrailRes : BaseRes
	{
		public TrailRes()
		{
			mType = ResType.ResType_Trail;
		}

		public override void DeleteResource()
		{
			if (mTrail != null)
			{
				mTrail.Dispose();
				mTrail = null;
			}
			base.DeleteResource();
		}

		public TrailDefinition mTrail;
	}
}
