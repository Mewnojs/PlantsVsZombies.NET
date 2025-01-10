using System;

namespace Sexy
{
    public/*internal*/ class ScrollbarWidget : Widget, ButtonListener
    {
        public ScrollbarWidget(int theId, ScrollListener theScrollListener)
        {
            mId = theId;
            mScrollListener = theScrollListener;
            SetDisabled(true);
            mUpButton = new ScrollbuttonWidget(0, this);
            mUpButton.SetDisabled(true);
            mDownButton = new ScrollbuttonWidget(1, this);
            mDownButton.SetDisabled(true);
            mInvisIfNoScroll = false;
            mPressedOnThumb = false;
            mValue = 0.0;
            mMaxValue = 0.0;
            mPageSize = 0.0;
            mUpdateAcc = 0;
            mButtonAcc = 0;
            mUpdateMode = 0;
            mHorizontal = false;
            mThumbImage = null;
            mBarImage = null;
            mPagingImage = null;
            mButtonLength = 0;
            AddWidget(mUpButton);
            AddWidget(mDownButton);
            SetColors(ScrollbarWidget.gScrollbarWidgetWidgetColors, 6);
        }

        public override void Dispose()
        {
            if (mUpButton != null)
            {
                RemoveWidget(mUpButton);
            }
            if (mDownButton != null)
            {
                RemoveWidget(mDownButton);
            }
        }

        public virtual void SetInvisIfNoScroll(bool invisIfNoScroll)
        {
            mInvisIfNoScroll = invisIfNoScroll;
            if (mInvisIfNoScroll)
            {
                SetVisible(false);
                mDownButton.SetVisible(false);
                mUpButton.SetVisible(false);
            }
        }

        public virtual void SetMaxValue(double theNewMaxValue)
        {
            mMaxValue = theNewMaxValue;
            ClampValue();
            MarkDirty();
        }

        public virtual void SetPageSize(double theNewPageSize)
        {
            mPageSize = theNewPageSize;
            ClampValue();
            MarkDirty();
        }

        public virtual void SetValue(double theNewValue)
        {
            mValue = theNewValue;
            ClampValue();
            mScrollListener.ScrollPosition(mId, mValue);
            MarkDirty();
        }

        public virtual void SetHorizontal(bool isHorizontal)
        {
            mHorizontal = isHorizontal;
            mDownButton.mHorizontal = mHorizontal;
            mUpButton.mHorizontal = mHorizontal;
        }

        public virtual void SetButtonImages(Image button, Image down)
        {
            SetButtonImages(button, down, null);
        }

        public virtual void SetButtonImages(Image button, Image down, Image disabled)
        {
            mUpButton.mButtonImage = button;
            mUpButton.mDownImage = down;
            mUpButton.mDisabledImage = disabled;
            mDownButton.mButtonImage = button;
            mDownButton.mDownImage = down;
            mDownButton.mDisabledImage = disabled;
        }

        public virtual void SetThumbImage(Image img)
        {
            mThumbImage = img;
        }

        public virtual void SetBarImages(Image theBarImage)
        {
            SetBarImages(theBarImage, null);
        }

        public virtual void SetBarImages(Image theBarImage, Image thePagingImage)
        {
            mBarImage = theBarImage;
            mPagingImage = thePagingImage;
        }

        public virtual void SetButtonColors(int[,] theColors, int theNumColors)
        {
            mUpButton.SetColors(theColors, theNumColors);
            mDownButton.SetColors(theColors, theNumColors);
        }

        public virtual void SetButtonColor(int theIdx, SexyColor theColor)
        {
            mUpButton.SetColor(theIdx, theColor);
            mDownButton.SetColor(theIdx, theColor);
        }

        public virtual void ResizeScrollbarWidget(int theX, int theY, int theWidth, int theHeight)
        {
            Resize(theX, theY, theWidth, theHeight);
            int num;
            if (mHorizontal)
            {
                theY = (theX = 0);
                num = theHeight;
                if (mButtonLength > 0)
                {
                    num = mButtonLength;
                }
                mUpButton.Resize(theX, theY, num, theHeight);
                mDownButton.Resize(theX + theWidth - num, theY, num, theHeight);
                return;
            }
            theY = (theX = 0);
            num = theWidth;
            if (mButtonLength > 0)
            {
                num = mButtonLength;
            }
            mUpButton.Resize(theX, theY, theWidth, num);
            mDownButton.Resize(theX, theY + theHeight - num, theWidth, num);
        }

        public virtual bool AtBottom()
        {
            return mMaxValue - mPageSize - mValue <= 1.0;
        }

        public virtual void GoToBottom()
        {
            mValue = mMaxValue - mPageSize;
            ClampValue();
            SetValue(mValue);
        }

        public virtual void DrawThumb(Graphics g, int theX, int theY, int theWidth, int theHeight)
        {
            if (mThumbImage == null)
            {
                g.SetColor(GetColor(2));
                g.FillRect(theX, theY, theWidth, theHeight);
                g.SetColor(GetColor(5));
                g.FillRect(theX + 1, theY + 1, theWidth - 2, 1);
                g.FillRect(theX + 1, theY + 1, 1, theHeight - 2);
                g.SetColor(GetColor(3));
                g.FillRect(theX, theY + theHeight - 1, theWidth, 1);
                g.FillRect(theX + theWidth - 1, theY, 1, theHeight);
                g.SetColor(GetColor(4));
                g.FillRect(theX + 1, theY + theHeight - 2, theWidth - 2, 1);
                g.FillRect(theX + theWidth - 2, theY + 1, 1, theHeight - 2);
                return;
            }
            g.DrawImageBox(new TRect(theX, theY, theWidth, theHeight), mThumbImage);
        }

        public virtual void DrawThumb(Graphics g, TRect theRect)
        {
            DrawThumb(g, theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
        }

        public virtual int GetTrackSize()
        {
            if (mButtonLength > 0)
            {
                if (mHorizontal)
                {
                    return mWidth - 2 * mButtonLength;
                }
                return mHeight - 2 * mButtonLength;
            }
            else
            {
                if (mHorizontal)
                {
                    return mWidth - 2 * mUpButton.mWidth;
                }
                return mHeight - 2 * mUpButton.mWidth;
            }
        }

        public virtual int GetThumbSize()
        {
            if (mPageSize > mMaxValue)
            {
                return 0;
            }
            int num = (int)(GetTrackSize() * mPageSize / mMaxValue + 0.5);
            return Math.Max(8, num);
        }

        public virtual int GetThumbPosition()
        {
            if (mPageSize > mMaxValue)
            {
                return 0;
            }
            return (int)(mValue * (GetTrackSize() - GetThumbSize()) / (mMaxValue - mPageSize) + 0.5);
        }

        public override void Draw(Graphics g)
        {
            int thumbSize = GetThumbSize();
            int num;
            if (mHorizontal)
            {
                num = (mWidth - GetTrackSize()) / 2;
            }
            else
            {
                num = (mHeight - GetTrackSize()) / 2;
            }
            int thumbPosition = GetThumbPosition();
            TRect trect;
            TRect trect2;
            TRect theRect;
            if (mHorizontal)
            {
                trect = new TRect(num, 0, thumbPosition + thumbSize / 2, mHeight);
                trect2 = new TRect(thumbPosition + thumbSize / 2 + num, 0, GetTrackSize() - thumbPosition - thumbSize / 2, mHeight);
                theRect = new TRect(thumbPosition + num, 0, thumbSize, mHeight);
            }
            else
            {
                trect = new TRect(0, num, mWidth, thumbPosition + thumbSize / 2);
                trect2 = new TRect(0, thumbPosition + thumbSize / 2 + num, mWidth, GetTrackSize() - thumbPosition - thumbSize / 2);
                theRect = new TRect(0, thumbPosition + num, mWidth, thumbSize);
            }
            if (mUpdateMode == 1)
            {
                if (mPagingImage != null)
                {
                    g.DrawImageBox(trect, mPagingImage);
                }
                else
                {
                    g.SetColor(GetColor(1));
                    g.FillRect(trect);
                }
            }
            else if (mBarImage != null)
            {
                g.DrawImageBox(trect, mBarImage);
            }
            else
            {
                g.SetColor(GetColor(0));
                g.FillRect(trect);
            }
            if (mUpdateMode == 2)
            {
                if (mPagingImage != null)
                {
                    g.DrawImageBox(trect2, mPagingImage);
                }
                else
                {
                    g.SetColor(GetColor(1));
                    g.FillRect(trect2);
                }
            }
            else if (mBarImage != null)
            {
                g.DrawImageBox(trect2, mBarImage);
            }
            else
            {
                g.SetColor(GetColor(0));
                g.FillRect(trect2);
            }
            if (thumbSize > 0)
            {
                DrawThumb(g, theRect);
            }
        }

        public virtual void ClampValue()
        {
            double num = mValue;
            if (mValue > mMaxValue - mPageSize)
            {
                mValue = mMaxValue - mPageSize;
            }
            if (mValue < 0.0)
            {
                mValue = 0.0;
            }
            bool flag = mPageSize < mMaxValue;
            SetDisabled(!flag);
            mUpButton.SetDisabled(!flag);
            mDownButton.SetDisabled(!flag);
            if (mInvisIfNoScroll)
            {
                SetVisible(flag);
                mDownButton.SetVisible(flag);
                mUpButton.SetVisible(flag);
            }
            if (mValue != num)
            {
                mScrollListener.ScrollPosition(mId, mValue);
            }
        }

        public virtual void SetThumbPosition(int thePosition)
        {
            SetValue(thePosition * (mMaxValue - mPageSize) / (GetTrackSize() - GetThumbSize()));
        }

        public virtual void ButtonPress(int theId)
        {
            mButtonAcc = 0;
            if (theId == 0)
            {
                SetValue(mValue - 1.0);
                return;
            }
            SetValue(mValue + 1.0);
        }

        public virtual void ButtonDepress(int theId)
        {
        }

        public virtual void ButtonDownTick(int theId)
        {
            if (theId == 0)
            {
                if (++mButtonAcc >= 25)
                {
                    SetValue(mValue - 1.0);
                    mButtonAcc = 24;
                    return;
                }
            }
            else if (++mButtonAcc >= 25)
            {
                SetValue(mValue + 1.0);
                mButtonAcc = 24;
            }
        }

        public override void Update()
        {
            base.Update();
            switch (mUpdateMode)
            {
            case 1:
                if (ThumbCompare(mLastMouseX, mLastMouseY) != -1)
                {
                    mUpdateMode = 0;
                    MarkDirty();
                    return;
                }
                if (++mUpdateAcc >= 25)
                {
                    SetValue(mValue - mPageSize);
                    mUpdateAcc = 20;
                    return;
                }
                break;
            case 2:
                if (ThumbCompare(mLastMouseX, mLastMouseY) != 1)
                {
                    mUpdateMode = 0;
                    MarkDirty();
                    return;
                }
                if (++mUpdateAcc >= 25)
                {
                    SetValue(mValue + mPageSize);
                    mUpdateAcc = 20;
                }
                break;
            default:
                return;
            }
        }

        public virtual int ThumbCompare(int x, int y)
        {
            int num;
            if (mHorizontal)
            {
                num = x - (mWidth - GetTrackSize()) / 2;
            }
            else
            {
                num = y - (mHeight - GetTrackSize()) / 2;
            }
            if (num < GetThumbPosition())
            {
                return -1;
            }
            if (num >= GetThumbPosition() + GetThumbSize())
            {
                return 1;
            }
            return 0;
        }

        public override void MouseDown(int x, int y, int theClickCount)
        {
            base.MouseDown(x, y, theClickCount);
        }

        public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
        {
            base.MouseDown(x, y, theBtnNum, theClickCount);
            if (!mDisabled)
            {
                switch (ThumbCompare(x, y))
                {
                case -1:
                    SetValue(mValue - mPageSize);
                    mUpdateMode = 1;
                    mUpdateAcc = 0;
                    break;
                case 0:
                    mPressedOnThumb = true;
                    mMouseDownThumbPos = GetThumbPosition();
                    mMouseDownX = x;
                    mMouseDownY = y;
                    break;
                case 1:
                    SetValue(mValue + mPageSize);
                    mUpdateMode = 2;
                    mUpdateAcc = 0;
                    break;
                }
            }
            mLastMouseX = x;
            mLastMouseY = y;
        }

        public override void MouseUp(int x, int y, int theBtnNum, int theClickCount)
        {
            base.MouseUp(x, y, theBtnNum, theClickCount);
            mUpdateMode = 0;
            mPressedOnThumb = false;
            MarkDirty();
        }

        public override void MouseDrag(int x, int y)
        {
            base.MouseDrag(x, y);
            if (mPressedOnThumb)
            {
                if (mHorizontal)
                {
                    SetThumbPosition(mMouseDownThumbPos + x - mMouseDownX);
                }
                else
                {
                    SetThumbPosition(mMouseDownThumbPos + y - mMouseDownY);
                }
            }
            mLastMouseX = x;
            mLastMouseY = y;
        }

        public override void RemoveAllWidgets(bool doDelete, bool recursive)
        {
            base.RemoveAllWidgets(doDelete, recursive);
            if (doDelete)
            {
                mUpButton = null;
                mDownButton = null;
            }
        }

        public void ButtonPress(int theId, int theClickCount)
        {
            ButtonPress(theId);
        }

        public void ButtonMouseEnter(int theId)
        {
        }

        public void ButtonMouseLeave(int theId)
        {
        }

        public void ButtonMouseMove(int theId, int theX, int theY)
        {
        }

        public static int[,] gScrollbarWidgetWidgetColors = new int[,]
        {
            {
                232,
                232,
                232
            },
            {
                48,
                48,
                48
            },
            {
                212,
                212,
                212
            },
            {
                0,
                0,
                0
            },
            {
                132,
                132,
                132
            },
            {
                255,
                255,
                255
            }
        };

        public ScrollbuttonWidget mUpButton;

        public ScrollbuttonWidget mDownButton;

        public bool mInvisIfNoScroll;

        public int mId;

        public double mValue;

        public double mMaxValue;

        public double mPageSize;

        public bool mHorizontal;

        public int mButtonLength;

        public bool mPressedOnThumb;

        public int mMouseDownThumbPos;

        public int mMouseDownX;

        public int mMouseDownY;

        public int mUpdateMode;

        public int mUpdateAcc;

        public int mButtonAcc;

        public int mLastMouseX;

        public int mLastMouseY;

        public ScrollListener mScrollListener;

        public Image mThumbImage;

        public Image mBarImage;

        public Image mPagingImage;

        public enum UpdateMode
        {
            UPDATE_MODE_IDLE,
            UPDATE_MODE_PGUP,
            UPDATE_MODE_PGDN
        }

        public enum ScrollbarColors
        {
            Bar,
            BarPaging,
            Thumb,
            DarkOutline,
            MediumOutline,
            LightOutline,
            NUM_COLORS
        }
    }
}
