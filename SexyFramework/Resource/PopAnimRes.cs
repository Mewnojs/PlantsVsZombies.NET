using System;
using Sexy.WidgetsLib;

namespace Sexy.Resource
{
	public class PopAnimRes : BaseRes
	{
		public PopAnimRes()
		{
			this.mType = ResType.ResType_PopAnim;
			this.mPopAnim = null;
		}

		public override void DeleteResource()
		{
			if (this.mResourceRef != null && this.mResourceRef.HasResource())
			{
				this.mResourceRef.Release();
			}
			else if (this.mPopAnim != null)
			{
				this.mPopAnim.Dispose();
			}
			this.mPopAnim = null;
			if (this.mGlobalPtr != null)
			{
				this.mGlobalPtr.mResObject = null;
			}
		}

		public PopAnim mPopAnim;
	}
}
