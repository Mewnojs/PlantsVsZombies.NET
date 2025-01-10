using System;

namespace Sexy
{
    public/*internal*/ class Slider : Widget
    {
        public Slider(int theId, SliderListener theListener, Image trackMinImage, Image trackMaxImage, Image thumbImage)
        {
            mId = theId;
            mListener = theListener;
            mTrackMinImage = trackMinImage;
            mTrackMaxImage = trackMaxImage;
            mThumbImage = thumbImage;
            mTrackImage = null;
            mVal = 0.0;
            mDragging = false;
            mRelX = 0;
            mRelY = 0;
            Resize(0, 0, trackMinImage.GetWidth() + trackMaxImage.GetWidth(), thumbImage.GetHeight());
        }

        public Slider(Image theTrackImage, Image theThumbImage, int theId, SliderListener theListener)
        {
            mTrackImage = theTrackImage;
            mThumbImage = theThumbImage;
            mId = theId;
            mListener = theListener;
            mTrackMinImage = null;
            mTrackMaxImage = null;
            mVal = 0.0;
            mDragging = false;
            mRelX = 0;
            mRelY = 0;
        }

        public virtual void SetValue(double theValue)
        {
            mVal = theValue;
            if (mVal < 0.0)
            {
                mVal = 0.0;
            }
            else if (mVal > 1.0)
            {
                mVal = 1.0;
            }
            MarkDirtyFull();
        }

        public virtual bool HasTransparencies()
        {
            return true;
        }

        public override void Draw(Graphics g)
        {
            if (mTrackMinImage != null && mTrackMaxImage != null)
            {
                int width = mThumbImage.GetWidth();
                int height = mThumbImage.GetHeight();
                int num = width / 2;
                int num2 = mWidth - width;
                TPoint tpoint = new TPoint((int)(num + mVal * num2), mHeight / 2);
                TRect destRect = new TRect(0, 0, tpoint.mX, mHeight);
                TRect destRect2 = new TRect(tpoint.mX, 0, mWidth - tpoint.mX, mHeight);
                DrawStretchableImage(g, mTrackMinImage, destRect);
                DrawStretchableImage(g, mTrackMaxImage, destRect2);
                g.DrawImage(mThumbImage, tpoint.mX - width / 2, tpoint.mY - height / 2);
                return;
            }
            if (mTrackImage != null)
            {
                int num3 = mTrackImage.GetWidth() / 3;
                int height2 = mTrackImage.GetHeight();
                int theY = (mHeight - height2) / 2;
                g.DrawImage(mTrackImage, 0, theY, new TRect(0, 0, num3, height2));
                Graphics @new = Graphics.GetNew(g);
                @new.ClipRect(num3, theY, mWidth - num3 * 2, height2);
                for (int i = 0; i < (mWidth - num3 * 2 + num3 - 1) / num3; i++)
                {
                    @new.DrawImage(mTrackImage, num3 + i * num3, theY, new TRect(num3, 0, num3, height2));
                }
                g.DrawImage(mTrackImage, mWidth - num3, theY, new TRect(num3 * 2, 0, num3, height2));
                @new.PrepareForReuse();
            }
            if (mThumbImage != null)
            {
                g.DrawImage(mThumbImage, (int)(mVal * (mWidth - mThumbImage.GetWidth() + FrameworkConstants.InvertAndScale(1f))), (mHeight - mThumbImage.GetHeight()) / 2);
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
            int num = (int)(mVal * (mWidth - mThumbImage.GetWidth()));
            if (x >= num && x < num + mThumbImage.GetWidth())
            {
                mDragging = true;
                mRelX = x - num;
                return;
            }
            double value = x / (double)mWidth;
            SetValue(value);
        }

        public override void MouseDrag(int x, int y)
        {
            if (mDragging)
            {
                double num = mVal;
                mVal = (x - mRelX) / (double)(mWidth - mThumbImage.GetWidth());
                if (mVal < 0.0)
                {
                    mVal = 0.0;
                }
                if (mVal > 1.0)
                {
                    mVal = 1.0;
                }
                if (mVal != num)
                {
                    mListener.SliderVal(mId, mVal);
                    MarkDirtyFull();
                }
            }
        }

        public override void MouseUp(int x, int y, int theMagicCode)
        {
            mDragging = false;
            mListener.SliderVal(mId, mVal);
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
