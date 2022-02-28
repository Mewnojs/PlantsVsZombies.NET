using System;

namespace Sexy.GraphicsLib
{
	public class HRenderContext
	{
		public HRenderContext()
		{
		}

		public HRenderContext(object inHandlePtr)
		{
			this.mHandlePtr = inHandlePtr;
		}

		public bool IsValid()
		{
			return this.mHandlePtr != null;
		}

		public object GetPointer()
		{
			return this.mHandlePtr;
		}

		public object mHandlePtr;
	}
}
