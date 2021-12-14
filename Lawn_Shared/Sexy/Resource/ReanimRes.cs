using System;
using Sexy.TodLib;

namespace Sexy
{
	internal class ReanimRes : BaseRes
	{
		public ReanimRes()
		{
			mType = ResType.ResType_Image;
		}

		public override void DeleteResource()
		{
			if (mReanim != null)
			{
				mReanim = null;
			}
			base.DeleteResource();
		}

		public ReanimatorDefinition mReanim;
	}
}
