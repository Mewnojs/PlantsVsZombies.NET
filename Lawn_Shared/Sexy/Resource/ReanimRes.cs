using System;
using Sexy.TodLib;

namespace Sexy
{
	internal class ReanimRes : BaseRes
	{
		public ReanimRes()
		{
			this.mType = ResType.ResType_Image;
		}

		public override void DeleteResource()
		{
			if (this.mReanim != null)
			{
				this.mReanim = null;
			}
			base.DeleteResource();
		}

		public ReanimatorDefinition mReanim;
	}
}
