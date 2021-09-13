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
				return this.renderTarget;
			}
		}

		public MemoryImage()
		{
			this.renderTarget = null;
			this.mId = 0U;
		}

		public void Clear()
		{
			GlobalStaticVars.g.Clear(new Color(0, 0, 0, 0));
		}

		public override void Dispose()
		{
			if (this.renderTarget != null)
			{
				this.renderTarget.Dispose();
			}
			base.Dispose();
		}

		public void Create(int theWidth, int theHeight)
		{
			this.Create(theWidth, theHeight, PixelFormat.kPixelFormat_RGBA8888);
		}

		public void Create(int theWidth, int theHeight, PixelFormat thePixelFormat)
		{
			this.mId = MemoryImage.nextId++;
			this.mWidth = theWidth;
			this.mParentWidth = theWidth;
			this.mHeight = theHeight;
			this.mParentHeight = theHeight;
			int num = this.mWidth;//GraphicsState.GetClosestPowerOf2Above(this.mWidth);
			int num2 = this.mHeight;//GraphicsState.GetClosestPowerOf2Above(this.mHeight);
			//num = Math.Max(16, num);
			//num2 = Math.Max(16, num2);
			this.mOwnsTexture = true;
			this.mMaxS = (float)this.mWidth / (float)num;
			this.mMaxT = (float)this.mHeight / (float)num2;
			this.renderTarget = new RenderTarget2D(GlobalStaticVars.g.GraphicsDevice, num, num2);
			this.Texture = this.renderTarget;
		}

		private static uint nextId;

		private RenderTarget2D renderTarget;

		private uint mId;
	}
}
