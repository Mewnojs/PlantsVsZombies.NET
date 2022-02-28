using System;
using Sexy.GraphicsLib;

namespace Sexy.Resource
{
	public class RenderEffectRes : BaseRes
	{
		public RenderEffectRes()
		{
			this.mType = ResType.ResType_RenderEffect;
			this.mRenderEffectDefinition = null;
		}

		public override void DeleteResource()
		{
			if (this.mResourceRef != null && this.mResourceRef.HasResource())
			{
				this.mResourceRef.Release();
			}
			else if (this.mRenderEffectDefinition != null)
			{
				this.mRenderEffectDefinition.Dispose();
			}
			this.mRenderEffectDefinition = null;
			if (this.mGlobalPtr != null)
			{
				this.mGlobalPtr.mResObject = null;
			}
		}

		public RenderEffectDefinition mRenderEffectDefinition;

		public string mSrcFilePath;
	}
}
