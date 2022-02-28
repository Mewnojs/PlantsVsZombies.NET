using System;
using Microsoft.Xna.Framework;
using Sexy.GraphicsLib;

namespace Sexy.WidgetsLib
{
	public class CursorWidget : Widget
	{
		public CursorWidget()
		{
			this.mImage = null;
			this.mMouseVisible = false;
		}

		public override void Draw(Graphics g)
		{
			if (this.mImage != null)
			{
				g.DrawImage(this.mImage, 0, 0);
			}
		}

		public void SetImage(Image theImage)
		{
			this.mImage = theImage;
			if (this.mImage != null)
			{
				this.Resize(this.mX, this.mY, theImage.mWidth, theImage.mHeight);
			}
		}

		public Vector2 GetHotspot()
		{
			if (this.mImage == null)
			{
				return new Vector2(0f, 0f);
			}
			return new Vector2((float)(this.mImage.GetWidth() / 2), (float)(this.mImage.GetHeight() / 2));
		}

		public Image mImage;
	}
}
