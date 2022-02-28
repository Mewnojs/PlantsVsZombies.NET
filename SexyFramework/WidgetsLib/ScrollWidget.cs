using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class ScrollWidget : Widget, ProxyWidgetListener, IDisposable
	{
		private static int ticksForSeconds(float seconds)
		{
			return (int)((float)ScrollWidget.FRAMEWORK_UPDATE_RATE * seconds);
		}

		private static float VectorNorm(FPoint v)
		{
			return v.mX * v.mX + v.mY * v.mY;
		}

		private static FPoint PointAddScaled(FPoint augend, FPoint addend, float factor, ref FPoint data)
		{
			float p = augend.mX + addend.mX * factor;
			float p_ = augend.mY + addend.mY * factor;
			data.SetValue(p, p_);
			return data;
		}

		public ScrollWidget(ScrollWidgetListener listener)
		{
			this.Init(listener);
		}

		public ScrollWidget()
		{
			this.Init(null);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public void SetPageControl(PageControl pageControl)
		{
			this.mPageControl = pageControl;
			if (this.mPagingEnabled)
			{
				this.mPageControl.SetNumberOfPages(this.mPageCountHorizontal);
			}
		}

		public void SetScrollMode(ScrollWidget.ScrollMode mode)
		{
			this.mScrollMode = mode;
			this.CacheDerivedValues();
		}

		public void SetScrollInsets(Insets insets)
		{
			this.mScrollInsets = new Insets(insets);
			this.CacheDerivedValues();
		}

		public void SetScrollOffset(FPoint anOffset, bool animated)
		{
			if (animated)
			{
				this.mScrollTarget.SetValue(anOffset.mX, anOffset.mY);
				this.mSeekScrollTarget = true;
				return;
			}
			this.mScrollOffset.SetValue(anOffset.mX, anOffset.mY);
			this.mScrollVelocity.SetValue(0f, 0f);
			if (this.mClient != null)
			{
				this.mClient.Move((int)this.mScrollOffset.mX, (int)this.mScrollOffset.mY);
			}
		}

		public void ScrollToMin(bool animated)
		{
			this.SetScrollOffset(new FPoint((float)this.mScrollInsets.mLeft, (float)this.mScrollInsets.mTop), animated);
		}

		public void ScrollToPoint(SexyPoint point, bool animated)
		{
			if (!this.mIsDown)
			{
				FPoint anOffset = new FPoint((float)(-(float)point.mX), (float)(-(float)point.mY));
				this.SetScrollOffset(anOffset, animated);
			}
		}

		public void ScrollRectIntoView(Rect rect, bool animated)
		{
			if (!this.mIsDown)
			{
				float num = (float)(rect.mX + rect.mWidth);
				float num2 = (float)(rect.mY + rect.mHeight);
				float num3 = Math.Max(Math.Min(0f, this.mScrollMin.mX), (float)(-(float)rect.mX));
				float num4 = Math.Max(Math.Min(0f, this.mScrollMin.mY), (float)(-(float)rect.mY));
				float num5 = Math.Min(this.mScrollMax.mX, (float)this.mWidth - num);
				float num6 = Math.Min(this.mScrollMax.mY, (float)this.mHeight - num2);
				FPoint anOffset = new FPoint(Math.Min(num5, Math.Max(num3, this.mScrollOffset.mX)), Math.Min(num6, Math.Max(num4, this.mScrollOffset.mY)));
				this.SetScrollOffset(anOffset, animated);
			}
		}

		public void EnableBounce(bool enable)
		{
			this.mBounceEnabled = enable;
		}

		public void EnablePaging(bool enable)
		{
			this.mPagingEnabled = enable;
		}

		public void EnableIndicators(Image indicatorsImage)
		{
			this.mIndicatorsImage = indicatorsImage;
			this.mIndicatorsEnabled = null != indicatorsImage;
			if (this.mIndicatorsEnabled && this.mIndicatorsProxy == null)
			{
				this.mIndicatorsProxy = new ProxyWidget(this);
				this.mIndicatorsProxy.mMouseVisible = false;
				this.mIndicatorsProxy.mZOrder = int.MaxValue;
				this.mIndicatorsProxy.Resize(0, 0, this.mWidth, this.mHeight);
				base.AddWidget(this.mIndicatorsProxy);
				return;
			}
			if (!this.mIndicatorsEnabled && this.mIndicatorsProxy != null)
			{
				base.RemoveWidget(this.mIndicatorsProxy);
				this.mIndicatorsProxy = null;
			}
		}

		public void SetIndicatorsInsets(Insets insets)
		{
			this.mIndicatorsInsets = new Insets(insets);
		}

		public void FlashIndicators()
		{
			this.mIndicatorsFlashTimer = ScrollWidget.SCROLL_INDICATORS_FLASH_TICKS;
		}

		public void SetPageHorizontal(int page, bool animated)
		{
			this.SetPage(page, this.mCurrentPageVertical, animated);
		}

		public void SetPageVertical(int page, bool animated)
		{
			this.SetPage(this.mCurrentPageHorizontal, page, animated);
		}

		public void SetPage(int hpage, int vpage, bool animated)
		{
			if (this.mPagingEnabled)
			{
				this.mCurrentPageHorizontal = Math.Max(0, Math.Min(hpage, this.mPageCountHorizontal - 1));
				this.mCurrentPageVertical = Math.Max(0, Math.Min(vpage, this.mPageCountVertical - 1));
				FPoint anOffset = new FPoint((float)this.mScrollInsets.mLeft - (float)this.mCurrentPageHorizontal * this.mPageSize.mX, (float)this.mScrollInsets.mTop - (float)this.mCurrentPageVertical * this.mPageSize.mY);
				this.SetScrollOffset(anOffset, animated);
			}
		}

		public int GetPageHorizontal()
		{
			return this.mCurrentPageHorizontal;
		}

		public int GetPageVertical()
		{
			return this.mCurrentPageVertical;
		}

		public void SetBackgroundImage(Image image)
		{
			this.mBackgroundImage = image;
		}

		public void EnableBackgroundFill(bool enable)
		{
			this.mFillBackground = enable;
		}

		public void AddOverlayImage(Image image, SexyPoint anOffset)
		{
			this.mDrawOverlays = true;
			foreach (ScrollWidget.Overlay overlay in this.mOverlays)
			{
				if (overlay.image == image)
				{
					overlay.offset = anOffset;
					return;
				}
			}
			ScrollWidget.Overlay overlay2 = new ScrollWidget.Overlay();
			overlay2.image = image;
			overlay2.offset = anOffset;
			this.mOverlays.Add(overlay2);
		}

		public void EnableOverlays(bool enable)
		{
			this.mDrawOverlays = enable;
		}

		public override void AddWidget(Widget theWidget)
		{
			if (this.mClient == null)
			{
				this.mClient = theWidget;
				this.mClient.mWidgetFlagsMod.mRemoveFlags |= 16;
				this.mClient.Move((int)this.mScrollOffset.mX, (int)this.mScrollOffset.mY);
				base.AddWidget(this.mClient);
				this.CacheDerivedValues();
			}
		}

		public override void RemoveWidget(Widget theWidget)
		{
			if (theWidget == this.mClient)
			{
				this.mClient = null;
			}
			base.RemoveWidget(theWidget);
		}

		public override void Resize(int x, int y, int width, int height)
		{
			base.Resize(x, y, width, height);
			if (this.mIndicatorsProxy != null)
			{
				this.mIndicatorsProxy.Resize(0, 0, width, height);
			}
			this.CacheDerivedValues();
		}

		public override void Resize(Rect frame)
		{
			base.Resize(frame);
		}

		public void ClientSizeChanged()
		{
			if (this.mClient != null)
			{
				this.CacheDerivedValues();
			}
		}

		public override void TouchBegan(SexyAppBase.Touch touch)
		{
			if (!this.mDragEnabled)
			{
				return;
			}
			if (this.mClient != null)
			{
				if (this.mSeekScrollTarget)
				{
					if (this.mListener != null)
					{
						this.mListener.ScrollTargetInterrupted(this);
					}
					if (this.mPagingEnabled && this.mPageControl != null)
					{
						this.mPageControl.SetCurrentPage(this.mCurrentPageHorizontal);
					}
				}
				this.mScrollTouchReference.SetValue((float)touch.location.mX, (float)touch.location.mY);
				this.mScrollOffsetReference.SetValue((float)this.mClient.mX, (float)this.mClient.mY);
				this.mScrollOffset.SetValue(this.mScrollOffsetReference.mX, this.mScrollOffsetReference.mY);
				this.mScrollLastTimestamp = touch.timestamp;
				this.mScrollTracking = false;
				this.mSeekScrollTarget = false;
				this.mClientLastDown = this.GetClientWidgetAt(touch);
				this.mClientLastDown.mIsDown = true;
				this.mClientLastDown.mIsOver = true;
				this.mClientLastDown.TouchBegan(touch);
			}
			this.MarkDirty();
		}

		public override void TouchMoved(SexyAppBase.Touch touch)
		{
			if (!this.mDragEnabled)
			{
				return;
			}
			FPoint fpoint = new FPoint((float)touch.location.mX, (float)touch.location.mY) - this.mScrollTouchReference;
			if (this.mClient != null)
			{
				if (!this.mScrollTracking && (this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED && Math.Abs(fpoint.mX) > ScrollWidget.SCROLL_DRAG_THRESHOLD)
				{
					this.mScrollTracking = true;
				}
				if (!this.mScrollTracking && (this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED && Math.Abs(fpoint.mY) > ScrollWidget.SCROLL_DRAG_THRESHOLD)
				{
					this.mScrollTracking = true;
				}
				if (this.mScrollTracking && this.mClientLastDown != null)
				{
					this.mClientLastDown.TouchesCanceled();
					this.mClientLastDown.mIsDown = false;
					this.mClientLastDown = null;
				}
			}
			if (this.mScrollTracking)
			{
				this.TouchMotion(touch);
			}
			else if (this.mClientLastDown != null)
			{
				SexyPoint point = this.GetAbsPos() - this.mClientLastDown.GetAbsPos();
				SexyPoint impliedObject = new SexyPoint(touch.location.mX, touch.location.mY);
				SexyPoint point2 = impliedObject + point;
				SexyPoint thePoint = new SexyPoint(point2.mX + this.mClientLastDown.mX, point2.mY + this.mClientLastDown.mY);
				bool flag = this.mClientLastDown.GetInsetRect().Contains(thePoint);
				if (flag && !this.mClientLastDown.mIsOver)
				{
					this.mClientLastDown.mIsOver = true;
					this.mClientLastDown.MouseEnter();
				}
				else if (!flag && this.mClientLastDown.mIsOver)
				{
					this.mClientLastDown.MouseLeave();
					this.mClientLastDown.mIsOver = false;
				}
				touch.location.mX += point.mX;
				touch.location.mY += point.mY;
				touch.previousLocation.mX += point.mX;
				touch.previousLocation.mY += point.mY;
				this.mClientLastDown.TouchMoved(touch);
			}
			this.MarkDirty();
		}

		public override void TouchEnded(SexyAppBase.Touch touch)
		{
			if (this.mScrollTracking)
			{
				this.TouchMotion(touch);
				this.mScrollTracking = false;
				if (this.mPagingEnabled)
				{
					this.SnapToPage();
				}
			}
			else if (this.mClientLastDown != null)
			{
				SexyPoint point = this.GetAbsPos() - this.mClientLastDown.GetAbsPos();
				SexyPoint impliedObject = new SexyPoint(touch.location.mX, touch.location.mY);
				touch.location.mX += point.mX;
				touch.location.mY += point.mY;
				touch.previousLocation.mX += point.mX;
				touch.previousLocation.mY += point.mY;
				this.mClientLastDown.TouchEnded(touch);
				this.mClientLastDown.mIsDown = false;
				this.mClientLastDown = null;
			}
			this.MarkDirty();
		}

		public override void TouchesCanceled()
		{
			if (this.mClient != null && this.mClientLastDown != null && !this.mScrollTracking)
			{
				this.mClientLastDown.TouchesCanceled();
				this.mClientLastDown.mIsDown = false;
				this.mClientLastDown = null;
			}
			this.mScrollTracking = false;
			this.MarkDirty();
		}

		public override void Update()
		{
			base.Update();
			if (this.mVisible && !this.mDisabled)
			{
				if (this.mIsDown)
				{
					this.mIndicatorsFlashTimer = ScrollWidget.SCROLL_INDICATORS_FLASH_TICKS;
				}
				else
				{
					float num = Math.Min(0f, this.mScrollMin.mX);
					float num2 = Math.Min(0f, this.mScrollMin.mY);
					float num3 = this.mScrollMax.mX;
					float num4 = this.mScrollMax.mY;
					if (this.mSeekScrollTarget)
					{
						float num5 = ScrollWidget.VectorNorm(this.mScrollTarget - this.mScrollOffset);
						if (num5 < ScrollWidget.SCROLL_TARGET_THRESHOLD_NORM)
						{
							this.mScrollOffset.SetValue(this.mScrollTarget.mX, this.mScrollTarget.mY);
							this.mSeekScrollTarget = false;
							if (this.mListener != null)
							{
								this.mListener.ScrollTargetReached(this);
							}
							if (this.mPagingEnabled && this.mPageControl != null)
							{
								this.mPageControl.SetCurrentPage(this.mCurrentPageHorizontal);
							}
						}
						else
						{
							num3 = (num = this.mScrollTarget.mX);
							num4 = (num2 = this.mScrollTarget.mY);
						}
					}
					float num6 = ScrollWidget.VectorNorm(this.mScrollVelocity);
					if (num6 < ScrollWidget.SCROLL_VELOCITY_THRESHOLD_NORM)
					{
						this.mScrollVelocity.SetValue(0f, 0f);
					}
					else
					{
						bool flag = this.mScrollOffset.mX < num || this.mScrollOffset.mX >= num3;
						bool flag2 = this.mScrollOffset.mY < num2 || this.mScrollOffset.mY >= num4;
						FPoint p = new FPoint(flag ? ScrollWidget.SCROLL_VELOCITY_DEVIATION_DAMPING : ScrollWidget.SCROLL_VELOCITY_DAMPING, flag2 ? ScrollWidget.SCROLL_VELOCITY_DEVIATION_DAMPING : ScrollWidget.SCROLL_VELOCITY_DAMPING);
						this.mScrollOffset = ScrollWidget.PointAddScaled(this.mScrollOffset, this.mScrollVelocity, 1f / (float)ScrollWidget.FRAMEWORK_UPDATE_RATE, ref this.mScrollOffset);
						FPoint fpoint = this.mScrollVelocity * p;
						this.mScrollVelocity.SetValue(fpoint.mX, fpoint.mY);
					}
					if (this.mScrollOffset.mX < num)
					{
						if (this.mBounceEnabled || this.mSeekScrollTarget)
						{
							this.mScrollOffset.mX += ScrollWidget.SCROLL_SPRINGBACK_TENSION * (num - this.mScrollOffset.mX);
						}
						else
						{
							this.mScrollOffset.mX = num;
							this.mScrollVelocity.mX = 0f;
						}
					}
					else if (this.mScrollOffset.mX > num3)
					{
						if (this.mBounceEnabled || this.mSeekScrollTarget)
						{
							this.mScrollOffset.mX += ScrollWidget.SCROLL_SPRINGBACK_TENSION * (num3 - this.mScrollOffset.mX);
						}
						else
						{
							this.mScrollOffset.mX = num3;
							this.mScrollVelocity.mX = 0f;
						}
					}
					if (this.mScrollOffset.mY < num2)
					{
						if (this.mBounceEnabled || this.mSeekScrollTarget)
						{
							this.mScrollOffset.mY += ScrollWidget.SCROLL_SPRINGBACK_TENSION * (num2 - this.mScrollOffset.mY);
						}
						else
						{
							this.mScrollOffset.mY = num2;
							this.mScrollVelocity.mY = 0f;
						}
					}
					else if (this.mScrollOffset.mY > num4)
					{
						if (this.mBounceEnabled || this.mSeekScrollTarget)
						{
							this.mScrollOffset.mY += ScrollWidget.SCROLL_SPRINGBACK_TENSION * (num4 - this.mScrollOffset.mY);
						}
						else
						{
							this.mScrollOffset.mY = num4;
							this.mScrollVelocity.mY = 0f;
						}
					}
					if (this.mClient != null)
					{
						this.mClient.Move((int)this.mScrollOffset.mX, (int)this.mScrollOffset.mY);
					}
					if (this.mIndicatorsFlashTimer > 0)
					{
						this.mIndicatorsFlashTimer--;
					}
				}
				if (this.mIndicatorsFlashTimer > 0 && this.mIndicatorsOpacity < 1f)
				{
					this.mIndicatorsOpacity = Math.Max(1f, this.mIndicatorsOpacity + ScrollWidget.SCROLL_INDICATORS_FADE_IN_RATE);
				}
				else if (this.mIndicatorsFlashTimer == 0 && this.mIndicatorsOpacity > 0f)
				{
					this.mIndicatorsOpacity = Math.Max(0f, this.mIndicatorsOpacity - ScrollWidget.SCROLL_INDICATORS_FADE_OUT_RATE);
				}
			}
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mBackgroundImage != null)
			{
				g.DrawImage(this.mBackgroundImage, 0, 0);
				return;
			}
			if (this.mFillBackground)
			{
				g.FillRect(0, 0, this.mWidth, this.mHeight);
			}
		}

		private static void DrawHorizontalStretchableImage(Graphics g, Image image, Rect destRect)
		{
			int width = image.GetWidth();
			int height = image.GetHeight();
			Rect theSrcRect = new Rect(0, 0, (width - 1) / 2, height);
			Rect theSrcRect2 = new Rect(theSrcRect.mWidth, 0, 1, height);
			Rect theSrcRect3 = new Rect(theSrcRect2.mX + theSrcRect2.mWidth, 0, width - theSrcRect.mWidth - theSrcRect2.mWidth, height);
			int theY = destRect.mY + (destRect.mHeight - height) / 2;
			Rect theDestRect = new Rect(destRect.mX + theSrcRect.mWidth, theY, destRect.mWidth - theSrcRect.mWidth - theSrcRect3.mWidth, height);
			g.DrawImage(image, destRect.mX, theY, theSrcRect);
			g.DrawImage(image, theDestRect, theSrcRect2);
			g.DrawImage(image, destRect.mX + destRect.mWidth - theSrcRect3.mWidth, theY, theSrcRect3);
		}

		private static void DrawVerticalStretchableImage(Graphics g, Image image, Rect destRect)
		{
			int width = image.GetWidth();
			int height = image.GetHeight();
			Rect theSrcRect = new Rect(0, 0, width, (height - 1) / 2);
			Rect theSrcRect2 = new Rect(0, theSrcRect.mHeight, width, 1);
			Rect theSrcRect3 = new Rect(0, theSrcRect2.mY + theSrcRect2.mHeight, width, height - theSrcRect.mHeight - theSrcRect2.mHeight);
			int theX = destRect.mX + (destRect.mWidth - width) / 2;
			Rect theDestRect = new Rect(theX, destRect.mY + theSrcRect.mHeight, width, destRect.mHeight - theSrcRect.mHeight - theSrcRect3.mHeight);
			g.DrawImage(image, theX, destRect.mY, theSrcRect);
			g.DrawImage(image, theDestRect, theSrcRect2);
			g.DrawImage(image, theX, destRect.mY + destRect.mHeight - theSrcRect3.mHeight, theSrcRect3);
		}

		public void DrawProxyWidget(Graphics g, ProxyWidget proxyWidget)
		{
			SexyColor color = new SexyColor(255, 255, 255, (int)(255f * this.mIndicatorsOpacity));
			if (color.mAlpha != 0)
			{
				int width = this.mIndicatorsImage.GetWidth();
				int height = this.mIndicatorsImage.GetHeight();
				Insets insets = this.mIndicatorsInsets;
				g.SetColor(color);
				g.SetColorizeImages(true);
				if ((this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
				{
					float num = (float)this.mWidth / (float)this.mClient.Width();
					int num2 = this.mWidth - insets.mLeft - insets.mRight - (((this.mScrollMode & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED) ? width : 0);
					int num3 = (int)((float)num2 * num);
					int num4 = num2 - num3;
					float num5 = (float)Math.Min(0, this.mWidth - this.mClient.mWidth - this.mScrollInsets.mRight);
					float num6 = (float)this.mScrollInsets.mLeft;
					float num7 = 1f - (this.mScrollOffset.mX - num5) / (num6 - num5);
					int num8 = (int)((float)num4 * num7);
					int num9 = num8 + num3;
					num8 = Math.Min(Math.Max(0, num8), num2 - width);
					num9 = Math.Min(Math.Max(width, num9), num2);
					Rect destRect = default(Rect);
					destRect.mX = insets.mLeft + num8;
					destRect.mY = this.mHeight - insets.mBottom - height;
					destRect.mWidth = num9 - num8;
					destRect.mHeight = height;
					ScrollWidget.DrawHorizontalStretchableImage(g, this.mIndicatorsImage, destRect);
				}
				if ((this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
				{
					float num10 = (float)this.mHeight / (float)this.mClient.Height();
					int num11 = this.mHeight - insets.mTop - insets.mBottom - (((this.mScrollMode & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED) ? height : 0);
					int num12 = (int)((float)num11 * num10);
					int num13 = num11 - num12;
					float num14 = (float)Math.Min(0, this.mHeight - this.mClient.mHeight - this.mScrollInsets.mBottom);
					float num15 = (float)this.mScrollInsets.mTop;
					float num16 = 1f - (this.mScrollOffset.mY - num14) / (num15 - num14);
					int num17 = (int)((float)num13 * num16);
					int num18 = num17 + num12;
					num17 = Math.Min(Math.Max(0, num17), num11 - height);
					num18 = Math.Min(Math.Max(height, num18), num11);
					Rect destRect2 = default(Rect);
					destRect2.mX = this.mWidth - insets.mRight - width;
					destRect2.mY = insets.mTop + num17;
					destRect2.mWidth = width;
					destRect2.mHeight = num18 - num17;
					ScrollWidget.DrawVerticalStretchableImage(g, this.mIndicatorsImage, destRect2);
				}
			}
			if (this.mDrawOverlays)
			{
				g.SetColorizeImages(false);
				foreach (ScrollWidget.Overlay overlay in this.mOverlays)
				{
					g.DrawImage(overlay.image, overlay.offset.mX, overlay.offset.mY);
				}
			}
		}

		protected void Init(ScrollWidgetListener listener)
		{
			this.mClient = null;
			this.mClientLastDown = null;
			this.mListener = listener;
			this.mPageControl = null;
			this.mIndicatorsProxy = null;
			this.mIndicatorsImage = null;
			this.mScrollMode = ScrollWidget.ScrollMode.SCROLL_VERTICAL;
			this.mScrollInsets = new Insets(0, 0, 0, 0);
			this.mScrollTracking = false;
			this.mSeekScrollTarget = false;
			this.mBounceEnabled = true;
			this.mPagingEnabled = false;
			this.mIndicatorsEnabled = false;
			this.mIndicatorsInsets = new Insets(0, 0, 0, 0);
			this.mIndicatorsFlashTimer = 0;
			this.mIndicatorsOpacity = 0f;
			this.mBackgroundImage = null;
			this.mFillBackground = false;
			this.mDrawOverlays = false;
			this.mScrollOffset.SetValue(0f, 0f);
			this.mScrollVelocity.SetValue(0f, 0f);
			this.mClip = true;
			this.mDragEnabled = true;
		}

		protected void SnapToPage()
		{
			FPoint impliedObject = new FPoint((float)this.mScrollInsets.mLeft + this.mPageSize.mX / 2f, (float)this.mScrollInsets.mTop + this.mPageSize.mY / 2f);
			FPoint fpoint = impliedObject - this.mScrollOffset;
			int num = (int)Math.Floor((double)(fpoint.mX / this.mPageSize.mX));
			int num2 = (int)Math.Floor((double)(fpoint.mY / this.mPageSize.mY));
			num = Math.Max(0, Math.Min(num, this.mPageCountHorizontal - 1));
			num2 = Math.Max(0, Math.Min(num2, this.mPageCountVertical - 1));
			FPoint fpoint2 = new FPoint((float)this.mScrollInsets.mLeft - (float)num * this.mPageSize.mX, (float)this.mScrollInsets.mTop - (float)num2 * this.mPageSize.mY);
			if (this.mScrollVelocity.mX > ScrollWidget.SCROLL_PAGE_FLICK_THRESHOLD && fpoint2.mX < this.mScrollOffset.mX)
			{
				num--;
			}
			else if (this.mScrollVelocity.mX < -ScrollWidget.SCROLL_PAGE_FLICK_THRESHOLD && fpoint2.mX > this.mScrollOffset.mX)
			{
				num++;
			}
			if (this.mScrollVelocity.mY > ScrollWidget.SCROLL_PAGE_FLICK_THRESHOLD && fpoint2.mY < this.mScrollOffset.mY)
			{
				num2--;
			}
			else if (this.mScrollVelocity.mY < -ScrollWidget.SCROLL_PAGE_FLICK_THRESHOLD && fpoint2.mY > this.mScrollOffset.mY)
			{
				num2++;
			}
			if (this.mLoadPage != null)
			{
				this.mLoadPage(num2);
				return;
			}
			this.SetPage(num, num2, true);
		}

		protected void TouchMotion(SexyAppBase.Touch touch)
		{
			if (!this.mDragEnabled)
			{
				return;
			}
			FPoint fpoint = new FPoint((float)touch.location.mX, (float)touch.location.mY) - this.mScrollTouchReference;
			FPoint fpoint2 = this.mScrollOffset;
			if ((this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
			{
				fpoint2.mX = this.mScrollOffsetReference.mX + fpoint.mX;
				float mX = this.mScrollMin.mX;
				float mX2 = this.mScrollMax.mX;
				if (fpoint2.mX < mX)
				{
					fpoint2.mX = (this.mBounceEnabled ? (fpoint2.mX + ScrollWidget.SCROLL_DEVIATION_DAMPING * (mX - fpoint2.mX)) : mX);
					this.mScrollVelocity.mX = 0f;
				}
				else if (fpoint2.mX > mX2)
				{
					fpoint2.mX = (this.mBounceEnabled ? (fpoint2.mX + ScrollWidget.SCROLL_DEVIATION_DAMPING * (mX2 - fpoint2.mX)) : mX2);
					this.mScrollVelocity.mX = 0f;
				}
				else
				{
					float num = fpoint2.mX - this.mScrollOffset.mX;
					double num2 = touch.timestamp - this.mScrollLastTimestamp;
					if (num2 > 0.0)
					{
						double num3 = (double)num / num2;
						double num4 = Math.Min(1.0, num2 / (double)ScrollWidget.SCROLL_VELOCITY_FILTER_WINDOW);
						this.mScrollVelocity.mX = (float)(num4 * num3 + (1.0 - num4) * (double)this.mScrollVelocity.mX);
					}
				}
			}
			if ((this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
			{
				fpoint2.mY = this.mScrollOffsetReference.mY + fpoint.mY;
				float mY = this.mScrollMin.mY;
				float mY2 = this.mScrollMax.mY;
				if (fpoint2.mY < mY)
				{
					fpoint2.mY = (this.mBounceEnabled ? (fpoint2.mY + ScrollWidget.SCROLL_DEVIATION_DAMPING * (mY - fpoint2.mY)) : mY);
					this.mScrollVelocity.mY = 0f;
				}
				else if (fpoint2.mY > mY2)
				{
					fpoint2.mY = (this.mBounceEnabled ? (fpoint2.mY + ScrollWidget.SCROLL_DEVIATION_DAMPING * (mY2 - fpoint2.mY)) : mY2);
					this.mScrollVelocity.mY = 0f;
				}
				else
				{
					float num5 = fpoint2.mY - this.mScrollOffset.mY;
					double num6 = touch.timestamp - this.mScrollLastTimestamp;
					double num7 = (double)num5 / num6;
					if (float.IsNaN((float)num7))
					{
						num7 = 0.0;
					}
					double num8 = Math.Min(1.0, num6 / (double)ScrollWidget.SCROLL_VELOCITY_FILTER_WINDOW);
					this.mScrollVelocity.mY = (float)(num8 * num7 + (1.0 - num8) * (double)this.mScrollVelocity.mY);
				}
			}
			this.mScrollOffset.SetValue(fpoint2.mX, fpoint2.mY);
			this.mScrollLastTimestamp = touch.timestamp;
			this.mClient.Move((int)this.mScrollOffset.mX, (int)this.mScrollOffset.mY);
		}

		protected Widget GetClientWidgetAt(SexyAppBase.Touch touch)
		{
			int num = touch.location.mX - this.mClient.mX;
			int num2 = touch.location.mY - this.mClient.mY;
			int num3 = 0;
			int num4 = 0;
			int theFlags = 16 | this.mWidgetManager.GetWidgetFlags();
			bool flag = false;
			Widget widgetAtHelper;
			if (this.mClientLastDown != null)
			{
				SexyPoint absPos = this.mClient.GetAbsPos();
				SexyPoint absPos2 = this.mClientLastDown.GetAbsPos();
				widgetAtHelper = this.mClientLastDown;
				num3 = touch.location.mX + absPos.mX - absPos2.mX;
				num4 = touch.location.mY + absPos.mY - absPos2.mY;
			}
			else
			{
				this.mClient.mWidgetFlagsMod.mRemoveFlags &= -17;
				widgetAtHelper = this.mClient.GetWidgetAtHelper(num, num2, theFlags, ref flag, ref num3, ref num4);
				this.mClient.mWidgetFlagsMod.mRemoveFlags |= 16;
			}
			if (widgetAtHelper == null || widgetAtHelper.mDisabled)
			{
				num3 = num;
				num4 = num2;
				widgetAtHelper = this.mClient;
			}
			touch.previousLocation.mX += num3 - touch.location.mX;
			touch.previousLocation.mY += num4 - touch.location.mY;
			touch.location.mX = num3;
			touch.location.mY = num4;
			return widgetAtHelper;
		}

		public bool mDragEnabled { get; set; }

		protected void CacheDerivedValues()
		{
			if (this.mClient != null)
			{
				this.mScrollMin.mX = (float)(this.mWidth - this.mClient.mWidth - this.mScrollInsets.mRight);
				this.mScrollMin.mY = (float)(this.mHeight - this.mClient.mHeight - this.mScrollInsets.mBottom);
				this.mScrollMax.mX = (float)this.mScrollInsets.mLeft;
				this.mScrollMax.mY = (float)this.mScrollInsets.mTop;
				int num = ((this.mScrollMin.mX < this.mScrollMax.mX) ? 1 : 0) | ((this.mScrollMin.mY < this.mScrollMax.mY) ? 2 : 0);
				this.mScrollPractical = this.mScrollMode & (ScrollWidget.ScrollMode)num;
			}
			else
			{
				this.mScrollMin.mX = (this.mScrollMax.mX = (this.mScrollMin.mY = (this.mScrollMax.mY = 0f)));
				this.mScrollPractical = ScrollWidget.ScrollMode.SCROLL_DISABLED;
			}
			if (this.mPagingEnabled)
			{
				this.mPageSize.mX = (float)(this.mWidth - this.mScrollInsets.mLeft - this.mScrollInsets.mRight);
				this.mPageSize.mY = (float)(this.mHeight - this.mScrollInsets.mTop - this.mScrollInsets.mBottom);
				if (this.mClient != null)
				{
					this.mPageCountHorizontal = (int)Math.Floor((double)((float)this.mClient.Width() / this.mPageSize.mX));
					this.mPageCountVertical = (int)Math.Floor((double)((float)this.mClient.Height() / this.mPageSize.mY));
					return;
				}
				this.mPageCountHorizontal = (this.mPageCountVertical = 0);
			}
		}

		public void NextVertPage()
		{
			if (1 + this.mCurrentPageVertical < this.mPageCountVertical - 1)
			{
				this.SetPage(0, 1 + this.mCurrentPageVertical, true);
			}
		}

		public void PreviousVertPage()
		{
			if (this.mCurrentPageVertical - 1 >= 0)
			{
				this.SetPage(0, this.mCurrentPageVertical - 1, true);
			}
		}

		private static readonly int FRAMEWORK_UPDATE_RATE = 30;

		public ScrollWidget.ExternalLoadPage mLoadPage;

		private static float SCROLL_TARGET_THRESHOLD_NORM = 2f;

		private static float SCROLL_VELOCITY_THRESHOLD_NORM = 0.0001f;

		private static float SCROLL_DEVIATION_DAMPING = 0.5f;

		private static float SCROLL_SPRINGBACK_TENSION = 0.1f;

		private static float SCROLL_VELOCITY_FILTER_WINDOW = 0.1f;

		private static float SCROLL_VELOCITY_DAMPING = 0.975f;

		private static float SCROLL_VELOCITY_DEVIATION_DAMPING = 0.85f;

		private static float SCROLL_DRAG_THRESHOLD = 4f;

		private static float SCROLL_PAGE_FLICK_THRESHOLD = 40f;

		private static float SCROLL_INDICATORS_FADE_IN_RATE = 1f / (0.2f * (float)ScrollWidget.FRAMEWORK_UPDATE_RATE);

		private static float SCROLL_INDICATORS_FADE_OUT_RATE = 1f / (0.5f * (float)ScrollWidget.FRAMEWORK_UPDATE_RATE);

		private static int SCROLL_INDICATORS_FLASH_TICKS = ScrollWidget.ticksForSeconds(1f);

		protected ScrollWidgetListener mListener;

		protected Widget mClient;

		protected Widget mClientLastDown;

		protected PageControl mPageControl;

		protected ProxyWidget mIndicatorsProxy;

		protected Image mIndicatorsImage;

		protected Image mBackgroundImage;

		protected bool mFillBackground;

		protected List<ScrollWidget.Overlay> mOverlays = new List<ScrollWidget.Overlay>();

		protected bool mDrawOverlays;

		protected ScrollWidget.ScrollMode mScrollMode;

		protected Insets mScrollInsets;

		protected FPoint mScrollTarget = new FPoint();

		protected FPoint mScrollOffset = new FPoint();

		protected FPoint mScrollVelocity = new FPoint();

		protected FPoint mScrollTouchReference = new FPoint();

		protected FPoint mScrollOffsetReference = new FPoint();

		protected bool mBounceEnabled;

		protected bool mPagingEnabled;

		protected bool mIndicatorsEnabled;

		protected Insets mIndicatorsInsets = new Insets();

		protected int mIndicatorsFlashTimer;

		protected float mIndicatorsOpacity;

		protected int mCurrentPageHorizontal;

		protected int mCurrentPageVertical;

		protected bool mSeekScrollTarget;

		protected bool mScrollTracking;

		protected double mScrollLastTimestamp;

		protected FPoint mScrollMin = new FPoint();

		protected FPoint mScrollMax = new FPoint();

		protected FPoint mPageSize = new FPoint();

		protected ScrollWidget.ScrollMode mScrollPractical;

		protected int mPageCountHorizontal;

		protected int mPageCountVertical;

		public delegate void ExternalLoadPage(int page);

		public enum ScrollMode
		{
			SCROLL_DISABLED,
			SCROLL_HORIZONTAL,
			SCROLL_VERTICAL,
			SCROLL_BOTH
		}

		public enum Colors
		{
			COLOR_BACKGROUND_COLOR
		}

		protected class Overlay
		{
			public Image image;

			public SexyPoint offset;
		}
	}
}
