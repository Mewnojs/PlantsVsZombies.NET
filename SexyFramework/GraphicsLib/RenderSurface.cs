using System;

namespace Sexy.GraphicsLib
{
	public class RenderSurface
	{
		public RenderSurface()
		{
			this.mRefCount = 0U;
			this.mData = 0;
			this.mPtr = null;
		}

		public virtual void Dispose()
		{
		}

		public void AddRef()
		{
			this.mRefCount += 1U;
		}

		public void Release()
		{
			this.mRefCount -= 1U;
			uint num = this.mRefCount;
		}

		public int mData;

		public object mPtr;

		private uint mRefCount;
	}
}
