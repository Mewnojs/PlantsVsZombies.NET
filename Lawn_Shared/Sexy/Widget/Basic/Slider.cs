using System;

namespace Sexy
{
	internal class Slider : Widget
	{
		public Slider(int theId, SliderListener theListener, Image trackMinImage, Image trackMaxImage, Image thumbImage)
		{
			this.mId = theId;
			this.mListener = theListener;
			this.mTrackMinImage = trackMinImage;
			this.mTrackMaxImage = trackMaxImage;
			this.mThumbImage = thumbImage;
			this.mTrackImage = null;
			this.mVal = 0.0;
			this.mDragging = false;
			this.mRelX = 0;
			this.mRelY = 0;
			this.Resize(0, 0, trackMinImage.GetWidth() + trackMaxImage.GetWidth(), thumbImage.GetHeight());
		}

		public Slider(Image theTrackImage, Image theThumbImage, int theId, SliderListener theListener)
		{
			this.mTrackImage = theTrackImage;
			this.mThumbImage = theThumbImage;
			this.mId = theId;
			this.mListener = theListener;
			this.mTrackMinImage = null;
			this.mTrackMaxImage = null;
			this.mVal = 0.0;
			this.mDragging = false;
			this.mRelX = 0;
			this.mRelY = 0;
		}

		public virtual void SetValue(double theValue)
		{
			this.mVal = theValue;
			if (this.mVal < 0.0)
			{
				this.mVal = 0.0;
			}
			else if (this.mVal > 1.0)
			{
				this.mVal = 1.0;
			}
			this.MarkDirtyFull();
		}

		public virtual bool HasTransparencies()
		{
			return true;
		}

		public override void Draw(Graphics g)
		{
			if (this.mTrackMinImage != null && this.mTrackMaxImage != null)
			{
				int width = this.mThumbImage.GetWidth();
				int height = this.mThumbImage.GetHeight();
				int num = width / 2;
				int num2 = this.mWidth - width;
				TPoint tpoint = new TPoint((int)((double)num + this.mVal * (double)num2), this.mHeight / 2);
				TRect destRect = new TRect(0, 0, tpoint.mX, this.mHeight);
				TRect destRect2 = new TRect(tpoint.mX, 0, this.mWidth - tpoint.mX, this.mHeight);
				this.DrawStretchableImage(g, this.mTrackMinImage, destRect);
				this.DrawStretchableImage(g, this.mTrackMaxImage, destRect2);
				g.DrawImage(this.mThumbImage, tpoint.mX - width / 2, tpoint.mY - height / 2);
				return;
			}
			if (this.mTrackImage != null)
			{
				int num3 = this.mTrackImage.GetWidth() / 3;
				int height2 = this.mTrackImage.GetHeight();
				int theY = (this.mHeight - height2) / 2;
				g.DrawImage(this.mTrackImage, 0, theY, new TRect(0, 0, num3, height2));
				Graphics @new = Graphics.GetNew(g);
				@new.ClipRect(num3, theY, this.mWidth - num3 * 2, height2);
				for (int i = 0; i < (this.mWidth - num3 * 2 + num3 - 1) / num3; i++)
				{
					@new.DrawImage(this.mTrackImage, num3 + i * num3, theY, new TRect(num3, 0, num3, height2));
				}
				g.DrawImage(this.mTrackImage, this.mWidth - num3, theY, new TRect(num3 * 2, 0, num3, height2));
				@new.PrepareForReuse();
			}
			if (this.mThumbImage != null)
			{
				g.DrawImage(this.mThumbImage, (int)(this.mVal * (double)((float)(this.mWidth - this.mThumbImage.GetWidth()) + Constants.InvertAndScale(1f))), (this.mHeight - this.mThumbImage.GetHeight()) / 2);
			}
		}

		public void DrawStretchableImage(Graphics g, Image image, TRect destRect)
		{
			int width = image.GetWidth();
			int height = image.GetHeight();
			TRect theSrcRect = new TRect(0, 0, (width - 1) / 2, height);
			TRect theSrcRect2 = new TRect(theSrcRect.mWidth, 0, 1, height);
			TRect theSrcRect3 = new TRect(theSrcRect2.mX + theSrcRect2.mWidth, 0, width - theSrcRect.mWidth - theSrcRect2.mWidth, height);
			int theY = destRect.mY + (destRect.mHeight - height) / 2;
			TRect theDestRect = new TRect(destRect.mX + theSrcRect.mWidth, theY, destRect.mWidth - theSrcRect.mWidth - theSrcRect3.mWidth, height);
			g.DrawImage(image, destRect.mX, theY, theSrcRect);
			g.DrawImage(image, theDestRect, theSrcRect2);
			g.DrawImage(image, destRect.mX + destRect.mWidth - theSrcRect3.mWidth, theY, theSrcRect3);
		}

		public override void MouseDown(int x, int y, int theMagicCode)
		{
			int num = (int)(this.mVal * (double)(this.mWidth - this.mThumbImage.GetWidth()));
			if (x >= num && x < num + this.mThumbImage.GetWidth())
			{
				this.mDragging = true;
				this.mRelX = x - num;
				return;
			}
			double value = (double)x / (double)this.mWidth;
			this.SetValue(value);
		}

		public override void MouseDrag(int x, int y)
		{
			if (this.mDragging)
			{
				double num = this.mVal;
				this.mVal = (double)(x - this.mRelX) / (double)(this.mWidth - this.mThumbImage.GetWidth());
				if (this.mVal < 0.0)
				{
					this.mVal = 0.0;
				}
				if (this.mVal > 1.0)
				{
					this.mVal = 1.0;
				}
				if (this.mVal != num)
				{
					this.mListener.SliderVal(this.mId, this.mVal);
					this.MarkDirtyFull();
				}
			}
		}

		public override void MouseUp(int x, int y, int theMagicCode)
		{
			this.mDragging = false;
			this.mListener.SliderVal(this.mId, this.mVal);
		}

		public override void MouseLeave()
		{
		}

		public SliderListener mListener;

		public double mVal;

		public int mId;

		public Image mTrackMinImage;

		public Image mTrackMaxImage;

		public Image mTrackImage;

		public Image mThumbImage;

		public bool mDragging;

		public int mRelX;

		public int mRelY;
	}
}
