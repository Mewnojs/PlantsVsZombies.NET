using System;

namespace Sexy
{
	internal class ScrollbarWidget : Widget, ButtonListener
	{
		public ScrollbarWidget(int theId, ScrollListener theScrollListener)
		{
			this.mId = theId;
			this.mScrollListener = theScrollListener;
			this.SetDisabled(true);
			this.mUpButton = new ScrollbuttonWidget(0, this);
			this.mUpButton.SetDisabled(true);
			this.mDownButton = new ScrollbuttonWidget(1, this);
			this.mDownButton.SetDisabled(true);
			this.mInvisIfNoScroll = false;
			this.mPressedOnThumb = false;
			this.mValue = 0.0;
			this.mMaxValue = 0.0;
			this.mPageSize = 0.0;
			this.mUpdateAcc = 0;
			this.mButtonAcc = 0;
			this.mUpdateMode = 0;
			this.mHorizontal = false;
			this.mThumbImage = null;
			this.mBarImage = null;
			this.mPagingImage = null;
			this.mButtonLength = 0;
			this.AddWidget(this.mUpButton);
			this.AddWidget(this.mDownButton);
			this.SetColors(ScrollbarWidget.gScrollbarWidgetWidgetColors, 6);
		}

		public override void Dispose()
		{
			if (this.mUpButton != null)
			{
				this.RemoveWidget(this.mUpButton);
			}
			if (this.mDownButton != null)
			{
				this.RemoveWidget(this.mDownButton);
			}
		}

		public virtual void SetInvisIfNoScroll(bool invisIfNoScroll)
		{
			this.mInvisIfNoScroll = invisIfNoScroll;
			if (this.mInvisIfNoScroll)
			{
				this.SetVisible(false);
				this.mDownButton.SetVisible(false);
				this.mUpButton.SetVisible(false);
			}
		}

		public virtual void SetMaxValue(double theNewMaxValue)
		{
			this.mMaxValue = theNewMaxValue;
			this.ClampValue();
			this.MarkDirty();
		}

		public virtual void SetPageSize(double theNewPageSize)
		{
			this.mPageSize = theNewPageSize;
			this.ClampValue();
			this.MarkDirty();
		}

		public virtual void SetValue(double theNewValue)
		{
			this.mValue = theNewValue;
			this.ClampValue();
			this.mScrollListener.ScrollPosition(this.mId, this.mValue);
			this.MarkDirty();
		}

		public virtual void SetHorizontal(bool isHorizontal)
		{
			this.mHorizontal = isHorizontal;
			this.mDownButton.mHorizontal = this.mHorizontal;
			this.mUpButton.mHorizontal = this.mHorizontal;
		}

		public virtual void SetButtonImages(Image button, Image down)
		{
			this.SetButtonImages(button, down, null);
		}

		public virtual void SetButtonImages(Image button, Image down, Image disabled)
		{
			this.mUpButton.mButtonImage = button;
			this.mUpButton.mDownImage = down;
			this.mUpButton.mDisabledImage = disabled;
			this.mDownButton.mButtonImage = button;
			this.mDownButton.mDownImage = down;
			this.mDownButton.mDisabledImage = disabled;
		}

		public virtual void SetThumbImage(Image img)
		{
			this.mThumbImage = img;
		}

		public virtual void SetBarImages(Image theBarImage)
		{
			this.SetBarImages(theBarImage, null);
		}

		public virtual void SetBarImages(Image theBarImage, Image thePagingImage)
		{
			this.mBarImage = theBarImage;
			this.mPagingImage = thePagingImage;
		}

		public virtual void SetButtonColors(int[,] theColors, int theNumColors)
		{
			this.mUpButton.SetColors(theColors, theNumColors);
			this.mDownButton.SetColors(theColors, theNumColors);
		}

		public virtual void SetButtonColor(int theIdx, SexyColor theColor)
		{
			this.mUpButton.SetColor(theIdx, theColor);
			this.mDownButton.SetColor(theIdx, theColor);
		}

		public virtual void ResizeScrollbarWidget(int theX, int theY, int theWidth, int theHeight)
		{
			this.Resize(theX, theY, theWidth, theHeight);
			int num;
			if (this.mHorizontal)
			{
				theY = (theX = 0);
				num = theHeight;
				if (this.mButtonLength > 0)
				{
					num = this.mButtonLength;
				}
				this.mUpButton.Resize(theX, theY, num, theHeight);
				this.mDownButton.Resize(theX + theWidth - num, theY, num, theHeight);
				return;
			}
			theY = (theX = 0);
			num = theWidth;
			if (this.mButtonLength > 0)
			{
				num = this.mButtonLength;
			}
			this.mUpButton.Resize(theX, theY, theWidth, num);
			this.mDownButton.Resize(theX, theY + theHeight - num, theWidth, num);
		}

		public virtual bool AtBottom()
		{
			return this.mMaxValue - this.mPageSize - this.mValue <= 1.0;
		}

		public virtual void GoToBottom()
		{
			this.mValue = this.mMaxValue - this.mPageSize;
			this.ClampValue();
			this.SetValue(this.mValue);
		}

		public virtual void DrawThumb(Graphics g, int theX, int theY, int theWidth, int theHeight)
		{
			if (this.mThumbImage == null)
			{
				g.SetColor(this.GetColor(2));
				g.FillRect(theX, theY, theWidth, theHeight);
				g.SetColor(this.GetColor(5));
				g.FillRect(theX + 1, theY + 1, theWidth - 2, 1);
				g.FillRect(theX + 1, theY + 1, 1, theHeight - 2);
				g.SetColor(this.GetColor(3));
				g.FillRect(theX, theY + theHeight - 1, theWidth, 1);
				g.FillRect(theX + theWidth - 1, theY, 1, theHeight);
				g.SetColor(this.GetColor(4));
				g.FillRect(theX + 1, theY + theHeight - 2, theWidth - 2, 1);
				g.FillRect(theX + theWidth - 2, theY + 1, 1, theHeight - 2);
				return;
			}
			g.DrawImageBox(new TRect(theX, theY, theWidth, theHeight), this.mThumbImage);
		}

		public virtual void DrawThumb(Graphics g, TRect theRect)
		{
			this.DrawThumb(g, theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public virtual int GetTrackSize()
		{
			if (this.mButtonLength > 0)
			{
				if (this.mHorizontal)
				{
					return this.mWidth - 2 * this.mButtonLength;
				}
				return this.mHeight - 2 * this.mButtonLength;
			}
			else
			{
				if (this.mHorizontal)
				{
					return this.mWidth - 2 * this.mUpButton.mWidth;
				}
				return this.mHeight - 2 * this.mUpButton.mWidth;
			}
		}

		public virtual int GetThumbSize()
		{
			if (this.mPageSize > this.mMaxValue)
			{
				return 0;
			}
			int num = (int)((double)this.GetTrackSize() * this.mPageSize / this.mMaxValue + 0.5);
			return Math.Max(8, num);
		}

		public virtual int GetThumbPosition()
		{
			if (this.mPageSize > this.mMaxValue)
			{
				return 0;
			}
			return (int)(this.mValue * (double)(this.GetTrackSize() - this.GetThumbSize()) / (this.mMaxValue - this.mPageSize) + 0.5);
		}

		public override void Draw(Graphics g)
		{
			int thumbSize = this.GetThumbSize();
			int num;
			if (this.mHorizontal)
			{
				num = (this.mWidth - this.GetTrackSize()) / 2;
			}
			else
			{
				num = (this.mHeight - this.GetTrackSize()) / 2;
			}
			int thumbPosition = this.GetThumbPosition();
			TRect trect;
			TRect trect2;
			TRect theRect;
			if (this.mHorizontal)
			{
				trect = new TRect(num, 0, thumbPosition + thumbSize / 2, this.mHeight);
				trect2 = new TRect(thumbPosition + thumbSize / 2 + num, 0, this.GetTrackSize() - thumbPosition - thumbSize / 2, this.mHeight);
				theRect = new TRect(thumbPosition + num, 0, thumbSize, this.mHeight);
			}
			else
			{
				trect = new TRect(0, num, this.mWidth, thumbPosition + thumbSize / 2);
				trect2 = new TRect(0, thumbPosition + thumbSize / 2 + num, this.mWidth, this.GetTrackSize() - thumbPosition - thumbSize / 2);
				theRect = new TRect(0, thumbPosition + num, this.mWidth, thumbSize);
			}
			if (this.mUpdateMode == 1)
			{
				if (this.mPagingImage != null)
				{
					g.DrawImageBox(trect, this.mPagingImage);
				}
				else
				{
					g.SetColor(this.GetColor(1));
					g.FillRect(trect);
				}
			}
			else if (this.mBarImage != null)
			{
				g.DrawImageBox(trect, this.mBarImage);
			}
			else
			{
				g.SetColor(this.GetColor(0));
				g.FillRect(trect);
			}
			if (this.mUpdateMode == 2)
			{
				if (this.mPagingImage != null)
				{
					g.DrawImageBox(trect2, this.mPagingImage);
				}
				else
				{
					g.SetColor(this.GetColor(1));
					g.FillRect(trect2);
				}
			}
			else if (this.mBarImage != null)
			{
				g.DrawImageBox(trect2, this.mBarImage);
			}
			else
			{
				g.SetColor(this.GetColor(0));
				g.FillRect(trect2);
			}
			if (thumbSize > 0)
			{
				this.DrawThumb(g, theRect);
			}
		}

		public virtual void ClampValue()
		{
			double num = this.mValue;
			if (this.mValue > this.mMaxValue - this.mPageSize)
			{
				this.mValue = this.mMaxValue - this.mPageSize;
			}
			if (this.mValue < 0.0)
			{
				this.mValue = 0.0;
			}
			bool flag = this.mPageSize < this.mMaxValue;
			this.SetDisabled(!flag);
			this.mUpButton.SetDisabled(!flag);
			this.mDownButton.SetDisabled(!flag);
			if (this.mInvisIfNoScroll)
			{
				this.SetVisible(flag);
				this.mDownButton.SetVisible(flag);
				this.mUpButton.SetVisible(flag);
			}
			if (this.mValue != num)
			{
				this.mScrollListener.ScrollPosition(this.mId, this.mValue);
			}
		}

		public virtual void SetThumbPosition(int thePosition)
		{
			this.SetValue((double)thePosition * (this.mMaxValue - this.mPageSize) / (double)(this.GetTrackSize() - this.GetThumbSize()));
		}

		public virtual void ButtonPress(int theId)
		{
			this.mButtonAcc = 0;
			if (theId == 0)
			{
				this.SetValue(this.mValue - 1.0);
				return;
			}
			this.SetValue(this.mValue + 1.0);
		}

		public virtual void ButtonDepress(int theId)
		{
		}

		public virtual void ButtonDownTick(int theId)
		{
			if (theId == 0)
			{
				if (++this.mButtonAcc >= 25)
				{
					this.SetValue(this.mValue - 1.0);
					this.mButtonAcc = 24;
					return;
				}
			}
			else if (++this.mButtonAcc >= 25)
			{
				this.SetValue(this.mValue + 1.0);
				this.mButtonAcc = 24;
			}
		}

		public override void Update()
		{
			base.Update();
			switch (this.mUpdateMode)
			{
			case 1:
				if (this.ThumbCompare(this.mLastMouseX, this.mLastMouseY) != -1)
				{
					this.mUpdateMode = 0;
					this.MarkDirty();
					return;
				}
				if (++this.mUpdateAcc >= 25)
				{
					this.SetValue(this.mValue - this.mPageSize);
					this.mUpdateAcc = 20;
					return;
				}
				break;
			case 2:
				if (this.ThumbCompare(this.mLastMouseX, this.mLastMouseY) != 1)
				{
					this.mUpdateMode = 0;
					this.MarkDirty();
					return;
				}
				if (++this.mUpdateAcc >= 25)
				{
					this.SetValue(this.mValue + this.mPageSize);
					this.mUpdateAcc = 20;
				}
				break;
			default:
				return;
			}
		}

		public virtual int ThumbCompare(int x, int y)
		{
			int num;
			if (this.mHorizontal)
			{
				num = x - (this.mWidth - this.GetTrackSize()) / 2;
			}
			else
			{
				num = y - (this.mHeight - this.GetTrackSize()) / 2;
			}
			if (num < this.GetThumbPosition())
			{
				return -1;
			}
			if (num >= this.GetThumbPosition() + this.GetThumbSize())
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
			if (!this.mDisabled)
			{
				switch (this.ThumbCompare(x, y))
				{
				case -1:
					this.SetValue(this.mValue - this.mPageSize);
					this.mUpdateMode = 1;
					this.mUpdateAcc = 0;
					break;
				case 0:
					this.mPressedOnThumb = true;
					this.mMouseDownThumbPos = this.GetThumbPosition();
					this.mMouseDownX = x;
					this.mMouseDownY = y;
					break;
				case 1:
					this.SetValue(this.mValue + this.mPageSize);
					this.mUpdateMode = 2;
					this.mUpdateAcc = 0;
					break;
				}
			}
			this.mLastMouseX = x;
			this.mLastMouseY = y;
		}

		public override void MouseUp(int x, int y, int theBtnNum, int theClickCount)
		{
			base.MouseUp(x, y, theBtnNum, theClickCount);
			this.mUpdateMode = 0;
			this.mPressedOnThumb = false;
			this.MarkDirty();
		}

		public override void MouseDrag(int x, int y)
		{
			base.MouseDrag(x, y);
			if (this.mPressedOnThumb)
			{
				if (this.mHorizontal)
				{
					this.SetThumbPosition(this.mMouseDownThumbPos + x - this.mMouseDownX);
				}
				else
				{
					this.SetThumbPosition(this.mMouseDownThumbPos + y - this.mMouseDownY);
				}
			}
			this.mLastMouseX = x;
			this.mLastMouseY = y;
		}

		public override void RemoveAllWidgets(bool doDelete, bool recursive)
		{
			base.RemoveAllWidgets(doDelete, recursive);
			if (doDelete)
			{
				this.mUpButton = null;
				this.mDownButton = null;
			}
		}

		public void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
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
			COLOR_BAR,
			COLOR_BAR_PAGING,
			COLOR_THUMB,
			COLOR_DARK_OUTLINE,
			COLOR_MEDIUM_OUTLINE,
			COLOR_LIGHT_OUTLINE,
			NUM_COLORS
		}
	}
}
