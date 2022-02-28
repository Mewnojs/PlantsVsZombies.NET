using System;

namespace Sexy.GraphicsLib
{
	public class RenderCommand : IDisposable
	{
		public RenderCommand()
		{
			this.mFontLayer = null;
		}

		public virtual void Dispose()
		{
			this.mFontLayer = null;
			this.mDest = null;
			this.mSrc = null;
		}

		public SexyColor mColor = default(SexyColor);

		public ActiveFontLayer mFontLayer;

		public int[] mDest = new int[2];

		public int[] mSrc = new int[4];

		public int mMode;
	}
}
