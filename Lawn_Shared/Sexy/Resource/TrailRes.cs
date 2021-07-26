using System;
using Sexy.TodLib;

namespace Sexy
{
	internal class TrailRes : BaseRes
	{
		public TrailRes()
		{
			this.mType = ResType.ResType_Trail;
		}

		public override void DeleteResource()
		{
			if (this.mTrail != null)
			{
				this.mTrail.Dispose();
				this.mTrail = null;
			}
			base.DeleteResource();
		}

		public TrailDefinition mTrail;
	}
}
