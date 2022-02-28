using System;

namespace Sexy.Resource
{
	public class GenericResFileRes : BaseRes
	{
		public GenericResFileRes()
		{
			this.mType = ResType.ResType_GenericResFile;
			this.mGenericResFile = null;
		}

		public override void DeleteResource()
		{
			if (this.mResourceRef != null && this.mResourceRef.HasResource())
			{
				this.mResourceRef.Release();
			}
			this.mGenericResFile = null;
			if (this.mGlobalPtr != null)
			{
				this.mGlobalPtr.mResObject = null;
			}
		}

		public GenericResFile mGenericResFile;
	}
}
