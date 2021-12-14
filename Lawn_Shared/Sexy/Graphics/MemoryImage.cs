using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	public/*internal*/ class MemoryImage : Image, IDisposable
	{
		public RenderTarget2D RenderTarget
		{
			get
			{
				return renderTarget;
			}
		}

		public MemoryImage()
		{
			renderTarget = null;
			mId = 0U;
		}

		public void Clear()
		{
			GlobalStaticVars.g.Clear(new Color(0, 0, 0, 0));
		}

		public override void Dispose()
		{
			if (renderTarget != null)
			{
				renderTarget.Dispose();
			}
			base.Dispose();
		}

		public void Create(int theWidth, int theHeight)
		{
			Create(theWidth, theHeight, PixelFormat.kPixelFormat_RGBA8888);
		}

		public void Create(int theWidth, int theHeight, PixelFormat thePixelFormat)
		{
			mId = MemoryImage.nextId++;
			mWidth = theWidth;
			mParentWidth = theWidth;
			mHeight = theHeight;
			mParentHeight = theHeight;
			int num = mWidth;//GraphicsState.GetClosestPowerOf2Above(this.mWidth);
			int num2 = mHeight;//GraphicsState.GetClosestPowerOf2Above(this.mHeight);
			//num = Math.Max(16, num);
			//num2 = Math.Max(16, num2);
			mOwnsTexture = true;
			mMaxS = (float)mWidth / (float)num;
			mMaxT = (float)mHeight / (float)num2;
			renderTarget = new RenderTarget2D(GlobalStaticVars.g.GraphicsDevice, num, num2);
			Texture = renderTarget;
		}

		private static uint nextId;

		private RenderTarget2D renderTarget;

		private uint mId;
	}
}
