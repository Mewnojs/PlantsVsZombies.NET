using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public/*internal*/ class ScrollWidget : Widget, ProxyWidgetListener
	{
		public ScrollWidget(ScrollWidgetListener listener)
		{
			Init(listener);
		}

		public ScrollWidget()
		{
			Init(null);
		}

		public override void Dispose()
		{
			RemoveAllWidgets(true, true);
		}

		public void SetPageControl(PageControl pageControl)
		{
			mPageControl = pageControl;
			if (mPagingEnabled)
			{
				mPageControl.SetNumberOfPages(mPageCountHorizontal);
			}
		}

		public void SetScrollMode(ScrollWidget.ScrollMode mode)
		{
			mScrollMode = mode;
			CacheDerivedValues();
		}

		public void SetScrollInsets(Insets insets)
		{
			mScrollInsets = insets;
			CacheDerivedValues();
		}

		public void SetScrollOffset(CGPoint offset, bool animated)
		{
			if (animated)
			{
				mScrollTarget = offset;
				mSeekScrollTarget = true;
				return;
			}
			mScrollOffset = offset;
			mScrollVelocity = CGMaths.CGPointMake(0f, 0f);
			if (mClient != null)
			{
				mClient.Move((int)mScrollOffset.x, (int)mScrollOffset.y);
			}
		}

		public void ScrollToMin(bool animated)
		{
			SetScrollOffset(CGMaths.CGPointMake(mScrollInsets.mLeft, mScrollInsets.mTop), animated);
		}

		public void ScrollToBottom(bool animated)
		{
			SetScrollOffset(CGMaths.CGPointMake(mScrollMin.x, mScrollMin.y), animated);
		}

		public void ScrollToPoint(CGPoint point, bool animated)
		{
			if (!mIsDown)
			{
				SetScrollOffset(new CGPoint
				{
					x = -point.mX,
					y = -point.mY
				}, animated);
			}
		}

		public void ScrollRectIntoView(TRect rect, bool animated)
		{
			if (!mIsDown)
			{
				float num = rect.mX + rect.mWidth;
				float num2 = rect.mY + rect.mHeight;
				float num3 = Math.Max(Math.Min(0f, mScrollMin.x), (float)(-rect.mX));
				float num4 = Math.Max(Math.Min(0f, mScrollMin.y), (float)(-rect.mY));
				float num5 = Math.Min(mScrollMax.x, mWidth - num);
				float num6 = Math.Min(mScrollMax.y, mHeight - num2);
				SetScrollOffset(new CGPoint
				{
					x = Math.Min(num5, Math.Max(num3, mScrollOffset.x)),
					y = Math.Min(num6, Math.Max(num4, mScrollOffset.y))
				}, animated);
			}
		}

		public void EnableBounce(bool enable)
		{
			mBounceEnabled = enable;
		}

		public void EnablePaging(bool enable)
		{
			mPagingEnabled = enable;
		}

		public void EnableIndicators(Image indicatorsImage)
		{
			mIndicatorsImage = indicatorsImage;
			mIndicatorsEnabled = (null != indicatorsImage);
			if (mIndicatorsEnabled && mIndicatorsProxy == null)
			{
				mIndicatorsProxy = new ProxyWidget(this);
				mIndicatorsProxy.mMouseVisible = false;
				mIndicatorsProxy.mZOrder = int.MaxValue;
				mIndicatorsProxy.Resize(0, 0, mWidth, mHeight);
				base.AddWidget(mIndicatorsProxy);
				return;
			}
			if (!mIndicatorsEnabled && mIndicatorsProxy != null)
			{
				base.RemoveWidget(mIndicatorsProxy);
				mIndicatorsProxy.Dispose();
				mIndicatorsProxy = null;
			}
		}

		public void SetIndicatorsInsets(Insets insets)
		{
			mIndicatorsInsets = insets;
		}

		public void FlashIndicators()
		{
			mIndicatorsFlashTimer = ScrollWidget.SCROLL_INDICATORS_FLASH_TICKS;
		}

		public void SetPageHorizontal(int page, bool animated)
		{
			SetPage(page, mCurrentPageVertical, animated);
		}

		public void SetPageVertical(int page, bool animated)
		{
			SetPage(mCurrentPageHorizontal, page, animated);
		}

		public void SetPage(int hpage, int vpage, bool animated)
		{
			if (mPagingEnabled)
			{
				mCurrentPageHorizontal = Math.Max(0, Math.Min(hpage, mPageCountHorizontal - 1));
				mCurrentPageVertical = Math.Max(0, Math.Min(vpage, mPageCountVertical - 1));
				SetScrollOffset(new CGPoint
				{
					x = mScrollInsets.mLeft - mCurrentPageHorizontal * mPageSize.x,
					y = mScrollInsets.mTop - mCurrentPageVertical * mPageSize.y
				}, animated);
			}
		}

		public int GetPageHorizontal()
		{
			return mCurrentPageHorizontal;
		}

		public int GetPageVertical()
		{
			return mCurrentPageVertical;
		}

		public void SetBackgroundImage(Image image)
		{
			mBackgroundImage = image;
		}

		public void EnableBackgroundFill(bool enable)
		{
			mFillBackground = enable;
		}

		public void AddOverlayImage(Image image, CGPoint offset)
		{
			mDrawOverlays = true;
			foreach (ScrollWidget.Overlay overlay in mOverlays)
			{
				if (overlay.image == image)
				{
					overlay.offset = offset;
					return;
				}
			}
			ScrollWidget.Overlay overlay2 = new ScrollWidget.Overlay();
			overlay2.image = image;
			overlay2.offset = offset;
			mOverlays.Add(overlay2);
		}

		public void EnableOverlays(bool enable)
		{
			mDrawOverlays = enable;
		}

		public override void AddWidget(Widget theWidget)
		{
			if (mClient == null)
			{
				mClient = theWidget;
				Widget widget = mClient;
				widget.mWidgetFlagsMod.mRemoveFlags = (widget.mWidgetFlagsMod.mRemoveFlags | 16);
				mClient.Move((int)mScrollOffset.x, (int)mScrollOffset.y);
				base.AddWidget(mClient);
				CacheDerivedValues();
			}
		}

		public override void RemoveWidget(Widget theWidget)
		{
			if (theWidget == mClient)
			{
				mClient = null;
			}
			base.RemoveWidget(theWidget);
		}

		public override void Resize(int x, int y, int width, int height)
		{
			base.Resize(x, y, width, height);
			if (mIndicatorsProxy != null)
			{
				mIndicatorsProxy.Resize(0, 0, width, height);
			}
			CacheDerivedValues();
		}

		public override void Resize(TRect frame)
		{
			base.Resize(frame);
		}

		public void ClientSizeChanged()
		{
			if (mClient != null)
			{
				CacheDerivedValues();
			}
		}

        public override void MouseDown(int x, int y, int theMagicCode)
        {
			if (mClient != null)
			{
				clientAllowsScroll = mClient.DoScroll(x, y);
				if (mSeekScrollTarget)
				{
					if (mListener != null)
					{
						mListener.ScrollTargetInterrupted(this);
					}
					if (mPagingEnabled && mPageControl != null)
					{
						mPageControl.SetCurrentPage(mCurrentPageHorizontal);
					}
				}
				mScrollTouchReference = CGMaths.CGPointMake(x, y);
				mScrollOffsetReference = CGMaths.CGPointMake(mClient.mX, mClient.mY);
				mScrollOffset = mScrollOffsetReference;
				//mScrollLastTimestamp = touch.timestamp;
				mScrollTracking = false;
				mSeekScrollTarget = false;
				mClientLastDown = GetClientWidgetAt(x, y);
				mClientLastDown.mIsDown = true;
				mClientLastDown.mIsOver = true;
				mClientLastDown.MouseDown(x, y, theMagicCode);
			}
		}

		public override void MouseUp(int x, int y, int theMagicCode)
		{
			if (mScrollTracking)
			{
				/*TouchMotion(touch);
				mScrollTracking = false;
				if (mPagingEnabled)
				{
					SnapToPage();
					return;
				}*/
			}
			else if (mClientLastDown != null)
			{
				CGPoint b = GetAbsPos() - mClientLastDown.GetAbsPos();
				CGPoint a = new CGPoint(x, y);
				//a + b;
				CGMaths.CGPointTranslate(ref a, b.mX, b.mY);
				//CGMaths.CGPointTranslate(ref touch.previousLocation, b.mX, b.mY);
				mClientLastDown.MouseUp((int)a.x, (int)a.y, theMagicCode);
				mClientLastDown.mIsDown = false;
				mClientLastDown = null;
			}
		}

		public override void MouseDrag(int x, int y)
		{
			CGPoint cgpoint = CGMaths.CGPointSubtract(new CGPoint(x, y), mScrollTouchReference);
			if (mClient != null)
			{
				if (clientAllowsScroll)
				{
					if (!mScrollTracking
                        && (mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED
                        && Math.Abs(cgpoint.x) > 4f)
					{
						mScrollTracking = true;
					}
					if (!mScrollTracking
                        && (mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED
                        && Math.Abs(cgpoint.y) > 4f)
					{
						mScrollTracking = true;
					}
				}
				if (mScrollTracking && mClientLastDown != null)
				{
					mClientLastDown.TouchesCanceled();
					mClientLastDown.mIsDown = false;
					mClientLastDown = null;
				}
			}
			if (mScrollTracking)
			{
				_Touch touch = new _Touch();
				touch.location.x = x;
				touch.location.y = y;
				TouchMotion(touch);
				return;
			}
			if (mClientLastDown != null)
			{
				CGPoint b = GetAbsPos() - mClientLastDown.GetAbsPos();
				CGPoint a = new CGPoint(x, y);
				CGPoint cgpoint2 = a + b;
				CGPoint a2 = new CGPoint(cgpoint2.mX + mClientLastDown.mX, cgpoint2.mY + mClientLastDown.mY);
				bool flag = mClientLastDown.GetInsetRect().Contains(a2);
				if (flag && !mClientLastDown.mIsOver)
				{
					mClientLastDown.mIsOver = true;
					mClientLastDown.MouseEnter();
				}
				else if (!flag && mClientLastDown.mIsOver)
				{
					mClientLastDown.MouseLeave();
					mClientLastDown.mIsOver = false;
				}
				//CGMaths.CGPointTranslate(ref touch.location, b.mX, b.mY);
				//CGMaths.CGPointTranslate(ref touch.previousLocation, b.mX, b.mY);
				mClientLastDown.MouseDrag((int)(x + b.mX), (int)(y + b.mY));
			}
		}

        public override void MouseWheel(int theDelta)
        {
			if ((mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
			{
				//mScrollOffset.y += theDelta;
				mScrollVelocity.y += theDelta * 1.4f;
			}
			else if ((mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
			{
				//mScrollOffset.x += theDelta;
				mScrollVelocity.x += theDelta * 1.4f;
			}
			//mScrollOffset = cgpoint2;
			//mScrollLastTimestamp = touch.timestamp;
			mClient.Move((int)mScrollOffset.x, (int)mScrollOffset.y);
			//oldTouch = touch.location;
			//oldTouchTime = touch.timestamp;
		}

        public override void TouchBegan(_Touch touch)
		{
			if (mClient != null)
			{
				clientAllowsScroll = mClient.DoScroll(touch);
				if (mSeekScrollTarget)
				{
					if (mListener != null)
					{
						mListener.ScrollTargetInterrupted(this);
					}
					if (mPagingEnabled && mPageControl != null)
					{
						mPageControl.SetCurrentPage(mCurrentPageHorizontal);
					}
				}
				mScrollTouchReference = touch.location;
				mScrollOffsetReference = CGMaths.CGPointMake(mClient.mX, mClient.mY);
				mScrollOffset = mScrollOffsetReference;
				mScrollLastTimestamp = touch.timestamp;
				mScrollTracking = false;
				mSeekScrollTarget = false;
				mClientLastDown = GetClientWidgetAt(touch);
				mClientLastDown.mIsDown = true;
				mClientLastDown.mIsOver = true;
				mClientLastDown.TouchBegan(touch);
			}
		}

		public override void TouchMoved(_Touch touch)
		{
			CGPoint cgpoint = CGMaths.CGPointSubtract(touch.location, mScrollTouchReference);
			if (mClient != null)
			{
				if (clientAllowsScroll)
				{
					if (!mScrollTracking && (mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED && Math.Abs(cgpoint.x) > 4f)
					{
						mScrollTracking = true;
					}
					if (!mScrollTracking && (mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED && Math.Abs(cgpoint.y) > 4f)
					{
						mScrollTracking = true;
					}
				}
				if (mScrollTracking && mClientLastDown != null)
				{
					mClientLastDown.TouchesCanceled();
					mClientLastDown.mIsDown = false;
					mClientLastDown = null;
				}
			}
			if (mScrollTracking)
			{
				TouchMotion(touch);
				return;
			}
			if (mClientLastDown != null)
			{
				CGPoint b = GetAbsPos() - mClientLastDown.GetAbsPos();
				CGPoint a = new CGPoint(touch.location.X, touch.location.Y);
				CGPoint cgpoint2 = a + b;
				CGPoint a2 = new CGPoint(cgpoint2.mX + mClientLastDown.mX, cgpoint2.mY + mClientLastDown.mY);
				bool flag = mClientLastDown.GetInsetRect().Contains(a2);
				if (flag && !mClientLastDown.mIsOver)
				{
					mClientLastDown.mIsOver = true;
					mClientLastDown.MouseEnter();
				}
				else if (!flag && mClientLastDown.mIsOver)
				{
					mClientLastDown.MouseLeave();
					mClientLastDown.mIsOver = false;
				}
				CGMaths.CGPointTranslate(ref touch.location, b.mX, b.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, b.mX, b.mY);
				mClientLastDown.TouchMoved(touch);
			}
		}

		public override void TouchEnded(_Touch touch)
		{
			if (mScrollTracking)
			{
				TouchMotion(touch);
				mScrollTracking = false;
				if (mPagingEnabled)
				{
					SnapToPage();
					return;
				}
			}
			else if (mClientLastDown != null)
			{
				CGPoint b = GetAbsPos() - mClientLastDown.GetAbsPos();
				CGPoint a = new CGPoint(touch.location.X, touch.location.Y);
				//a + b;
				CGMaths.CGPointTranslate(ref touch.location, b.mX, b.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, b.mX, b.mY);
				mClientLastDown.TouchEnded(touch);
				mClientLastDown.mIsDown = false;
				mClientLastDown = null;
			}
		}

		public override void TouchesCanceled()
		{
			if (mClient != null && mClientLastDown != null && !mScrollTracking)
			{
				mClientLastDown.TouchesCanceled();
				mClientLastDown.mIsDown = false;
				mClientLastDown = null;
			}
			mScrollTracking = false;
		}

		public override void Update()
		{
			base.Update();
			DoScrollUpdate();
			DoScrollUpdate();
			DoScrollUpdate();
		}

		public void DoScrollUpdate()
		{
			if (mVisible && !mDisabled)
			{
				if (mIsDown)
				{
					mIndicatorsFlashTimer = ScrollWidget.SCROLL_INDICATORS_FLASH_TICKS;
				}
				else
				{
					float num = Math.Min(0f, mScrollMin.x);
					float num2 = Math.Min(0f, mScrollMin.y);
					float num3 = mScrollMax.x;
					float num4 = mScrollMax.y;
					if (mSeekScrollTarget)
					{
						float num5 = CGMaths.CGVectorNorm(CGMaths.CGPointSubtract(mScrollTarget, mScrollOffset));
						if (num5 < 0.01f)
						{
							mScrollOffset = mScrollTarget;
							mSeekScrollTarget = false;
							if (mListener != null)
							{
								mListener.ScrollTargetReached(this);
							}
							if (mPagingEnabled && mPageControl != null)
							{
								mPageControl.SetCurrentPage(mCurrentPageHorizontal);
							}
						}
						else
						{
							num3 = (num = mScrollTarget.x);
							num4 = (num2 = mScrollTarget.y);
						}
					}
					float num6 = CGMaths.CGVectorNorm(mScrollVelocity);
					if (num6 < 0.0001f)
					{
						mScrollVelocity = CGMaths.CGPointMake(0f, 0f);
					}
					else
					{
						bool flag = mScrollOffset.x < num || mScrollOffset.x >= num3;
						bool flag2 = mScrollOffset.y < num2 || mScrollOffset.y >= num4;
						CGPoint multiplier = default(CGPoint);
						multiplier.x = (flag ? 0.85f : 0.975f);
						multiplier.y = (flag2 ? 0.85f : 0.975f);
						mScrollOffset = CGMaths.CGPointAddScaled(mScrollOffset, mScrollVelocity, 0.01f);
						mScrollVelocity = CGMaths.CGPointMultiply(mScrollVelocity, multiplier);
					}
					if (mScrollOffset.x < num)
					{
						if (mBounceEnabled || mSeekScrollTarget)
						{
							float num7 = (mSpringOverride == 0f) ? 0.1f : mSpringOverride;
							mScrollOffset.x = mScrollOffset.x + num7 * (num - mScrollOffset.x);
						}
						else
						{
							mScrollOffset.x = num;
							mScrollVelocity.x = 0f;
						}
					}
					else if (mScrollOffset.x > num3)
					{
						if (mBounceEnabled || mSeekScrollTarget)
						{
							float num8 = (mSpringOverride == 0f) ? 0.1f : mSpringOverride;
							mScrollOffset.x = mScrollOffset.x + num8 * (num3 - mScrollOffset.x);
						}
						else
						{
							mScrollOffset.x = num3;
							mScrollVelocity.x = 0f;
						}
					}
					if (mScrollOffset.y < num2)
					{
						if (mBounceEnabled || mSeekScrollTarget)
						{
							float num9 = (mSpringOverride == 0f) ? 0.1f : mSpringOverride;
							mScrollOffset.y = mScrollOffset.y + num9 * (num2 - mScrollOffset.y);
						}
						else
						{
							mScrollOffset.y = num2;
							mScrollVelocity.y = 0f;
						}
					}
					else if (mScrollOffset.y > num4)
					{
						if (mBounceEnabled || mSeekScrollTarget)
						{
							float num10 = (mSpringOverride == 0f) ? 0.1f : mSpringOverride;
							mScrollOffset.y = mScrollOffset.y + num10 * (num4 - mScrollOffset.y);
						}
						else
						{
							mScrollOffset.y = num4;
							mScrollVelocity.y = 0f;
						}
					}
					if (mClient != null)
					{
						mClient.Move((int)mScrollOffset.x, (int)mScrollOffset.y);
					}
					if (mIndicatorsFlashTimer > 0)
					{
						mIndicatorsFlashTimer--;
					}
				}
				if (mIndicatorsFlashTimer > 0 && mIndicatorsOpacity < 1f)
				{
					mIndicatorsOpacity = Math.Min(1f, mIndicatorsOpacity + ScrollWidget.SCROLL_INDICATORS_FADE_IN_RATE);
					return;
				}
				if (mIndicatorsFlashTimer == 0 && mIndicatorsOpacity > 0f)
				{
					mIndicatorsOpacity = Math.Max(0f, mIndicatorsOpacity - ScrollWidget.SCROLL_INDICATORS_FADE_OUT_RATE);
				}
			}
		}

		public static void DrawHorizontalStretchableImage(Graphics g, Image image, TRect destRect)
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

		public static void DrawVerticalStretchableImage(Graphics g, Image image, TRect destRect)
		{
			int width = image.GetWidth();
			int height = image.GetHeight();
			TRect theSrcRect = new TRect(0, 0, width, (height - 1) / 2);
			TRect theSrcRect2 = new TRect(0, theSrcRect.mHeight, width, 1);
			TRect theSrcRect3 = new TRect(0, theSrcRect2.mY + theSrcRect2.mHeight, width, height - theSrcRect.mHeight - theSrcRect2.mHeight);
			int theX = destRect.mX + (destRect.mWidth - width) / 2;
			TRect theDestRect = new TRect(theX, destRect.mY + theSrcRect.mHeight, width, destRect.mHeight - theSrcRect.mHeight - theSrcRect3.mHeight);
			g.DrawImage(image, theX, destRect.mY, theSrcRect);
			g.DrawImage(image, theDestRect, theSrcRect2);
			g.DrawImage(image, theX, destRect.mY + destRect.mHeight - theSrcRect3.mHeight, theSrcRect3);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (mBackgroundImage != null)
			{
				g.DrawImage(mBackgroundImage, 0, 0);
				return;
			}
			if (mFillBackground)
			{
				g.SetColor(GetColor(0));
				g.FillRect(0, 0, mWidth, mHeight);
			}
		}

		public void DrawProxyWidget(Graphics g, ProxyWidget proxyWidget)
		{
			Color color = new Color(255, 255, 255, (int)(255f * mIndicatorsOpacity));
			if (color.A != 0)
			{
				int width = mIndicatorsImage.GetWidth();
				int height = mIndicatorsImage.GetHeight();
				Insets insets = mIndicatorsInsets;
				g.SetColor(color);
				g.SetColorizeImages(true);
				if ((mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
				{
					float num = mWidth / (float)mClient.Width();
					int num2 = mWidth - insets.mLeft - insets.mRight - (((mScrollMode & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED) ? width : 0);
					int num3 = (int)(num2 * num);
					int num4 = num2 - num3;
					float num5 = Math.Min(0, mWidth - mClient.mWidth - mScrollInsets.mRight);
					float num6 = mScrollInsets.mLeft;
					float num7 = 1f - (mScrollOffset.x - num5) / (num6 - num5);
					int num8 = (int)(num4 * num7);
					int num9 = num8 + num3;
					num8 = Math.Min(Math.Max(0, num8), num2 - width);
					num9 = Math.Min(Math.Max(width, num9), num2);
					TRect destRect = default(TRect);
					destRect.mX = insets.mLeft + num8;
					destRect.mY = mHeight - insets.mBottom - height;
					destRect.mWidth = num9 - num8;
					destRect.mHeight = height;
					ScrollWidget.DrawHorizontalStretchableImage(g, mIndicatorsImage, destRect);
				}
				if ((mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
				{
					float num10 = mHeight / (float)mClient.Height();
					int num11 = mHeight - insets.mTop - insets.mBottom - (((mScrollMode & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED) ? height : 0);
					int num12 = (int)(num11 * num10);
					int num13 = num11 - num12;
					float num14 = Math.Min(0, mHeight - mClient.mHeight - mScrollInsets.mBottom);
					float num15 = mScrollInsets.mTop;
					float num16 = 1f - (mScrollOffset.y - num14) / (num15 - num14);
					int num17 = (int)(num13 * num16);
					int num18 = num17 + num12;
					num17 = Math.Min(Math.Max(0, num17), num11 - height);
					num18 = Math.Min(Math.Max(height, num18), num11);
					TRect destRect2 = default(TRect);
					destRect2.mX = mWidth - insets.mRight - width;
					destRect2.mY = insets.mTop + num17;
					destRect2.mWidth = width;
					destRect2.mHeight = num18 - num17;
					ScrollWidget.DrawVerticalStretchableImage(g, mIndicatorsImage, destRect2);
				}
			}
			if (mDrawOverlays)
			{
				g.SetColorizeImages(false);
				foreach (ScrollWidget.Overlay overlay in mOverlays)
				{
					g.DrawImage(overlay.image, overlay.offset.mX, overlay.offset.mY);
				}
			}
		}

		protected void Init(ScrollWidgetListener listener)
		{
			mClient = null;
			mClientLastDown = null;
			mListener = listener;
			mPageControl = null;
			mIndicatorsProxy = null;
			mIndicatorsImage = null;
			mScrollMode = ScrollWidget.ScrollMode.SCROLL_VERTICAL;
			mScrollInsets = new Insets(0, 0, 0, 0);
			mScrollTracking = false;
			mSeekScrollTarget = false;
			mBounceEnabled = true;
			mPagingEnabled = false;
			mIndicatorsEnabled = false;
			mIndicatorsInsets = new Insets(0, 0, 0, 0);
			mIndicatorsFlashTimer = 0;
			mIndicatorsOpacity = 0f;
			mBackgroundImage = null;
			mFillBackground = false;
			mDrawOverlays = false;
			mScrollOffset = CGMaths.CGPointMake(0f, 0f);
			mScrollVelocity = CGMaths.CGPointMake(0f, 0f);
			mClip = true;
		}

		protected void SnapToPage()
		{
			CGPoint cgpoint = CGMaths.CGPointSubtract(new CGPoint
			{
				x = mScrollInsets.mLeft + mPageSize.x / 2f,
				y = mScrollInsets.mTop + mPageSize.y / 2f
			}, mScrollOffset);
			int num = (int)Math.Floor(cgpoint.x / mPageSize.x);
			int num2 = (int)Math.Floor(cgpoint.y / mPageSize.y);
			num = Math.Max(0, Math.Min(num, mPageCountHorizontal - 1));
			num2 = Math.Max(0, Math.Min(num2, mPageCountVertical - 1));
			CGPoint cgpoint2 = default(CGPoint);
			cgpoint2.x = mScrollInsets.mLeft - num * mPageSize.x;
			cgpoint2.y = mScrollInsets.mTop - num2 * mPageSize.y;
			if (mScrollVelocity.x > 40f && cgpoint2.x < mScrollOffset.x)
			{
				num--;
			}
			else if (mScrollVelocity.x < -40f && cgpoint2.x > mScrollOffset.x)
			{
				num++;
			}
			if (mScrollVelocity.y > 40f && cgpoint2.y < mScrollOffset.y)
			{
				num2--;
			}
			else if (mScrollVelocity.y < -40f && cgpoint2.y > mScrollOffset.y)
			{
				num2++;
			}
			SetPage(num, num2, true);
		}

		protected void TouchMotion(_Touch touch)
		{
			CGPoint cgpoint = CGMaths.CGPointSubtract(touch.location, mScrollTouchReference);
			CGPoint cgpoint2 = mScrollOffset;
			if ((mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
			{
				cgpoint2.x = mScrollOffsetReference.x + cgpoint.X;
				float x = mScrollMin.x;
				float x2 = mScrollMax.x;
				if (cgpoint2.x < x)
				{
					cgpoint2.x = (mBounceEnabled ? (cgpoint2.x + 0.5f * (x - cgpoint2.x)) : x);
					mScrollVelocity.x = 0f;
				}
				else if (cgpoint2.x > x2)
				{
					cgpoint2.x = (mBounceEnabled ? (cgpoint2.x + 0.5f * (x2 - cgpoint2.x)) : x2);
					mScrollVelocity.x = 0f;
				}
				else
				{
					float num = cgpoint2.x - mScrollOffset.x;
					double num2 = touch.timestamp - mScrollLastTimestamp;
					if (num2 > 0.0)
					{
						double num3 = num / num2;
						double num4 = Math.Min(1.0, num2 / 0.10000000149011612);
						mScrollVelocity.x = (float)(num4 * num3 + (1.0 - num4) * mScrollVelocity.x);
					}
				}
			}
			if ((mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
			{
				cgpoint2.y = mScrollOffsetReference.y + cgpoint.Y;
				float y = mScrollMin.y;
				float y2 = mScrollMax.y;
				if (cgpoint2.y < y)
				{
					cgpoint2.y = (mBounceEnabled ? (cgpoint2.y + 0.5f * (y - cgpoint2.y)) : y);
					mScrollVelocity.y = 0f;
				}
				else if (cgpoint2.y > y2)
				{
					cgpoint2.y = (mBounceEnabled ? (cgpoint2.y + 0.5f * (y2 - cgpoint2.y)) : y2);
					mScrollVelocity.y = 0f;
				}
				else
				{
					float num5 = cgpoint2.y - mScrollOffset.mY;
					double num6 = touch.timestamp - mScrollLastTimestamp;
					if (num6 > 0.0)
					{
						double num7 = num5 / num6;
						double num8 = Math.Min(1.0, num6 / 0.10000000149011612);
						mScrollVelocity.y = (float)(num8 * num7 + (1.0 - num8) * mScrollVelocity.y);
					}
				}
			}
			mScrollOffset = cgpoint2;
			mScrollLastTimestamp = touch.timestamp;
			mClient.Move((int)mScrollOffset.x, (int)mScrollOffset.y);
			oldTouch = touch.location;
			oldTouchTime = touch.timestamp;
		}

		protected Widget GetClientWidgetAt(_Touch touch)
		{
			int num = (int)touch.location.X - mClient.mX;
			int num2 = (int)touch.location.Y - mClient.mY;
			int theFlags = 16 | mWidgetManager.GetWidgetFlags();
			Widget widgetAtHelper;
			int num3;
			int num4;
			if (mClientLastDown != null)
			{
				CGPoint absPos = mClient.GetAbsPos();
				CGPoint absPos2 = mClientLastDown.GetAbsPos();
				widgetAtHelper = mClientLastDown;
				num3 = (int)(touch.location.X + absPos.mX - absPos2.mX);
				num4 = (int)(touch.location.Y + absPos.mY - absPos2.mY);
			}
			else
			{
				Widget widget = mClient;
				widget.mWidgetFlagsMod.mRemoveFlags = (widget.mWidgetFlagsMod.mRemoveFlags & -17);
				bool flag;
				widgetAtHelper = mClient.GetWidgetAtHelper(num, num2, theFlags, out flag, out num3, out num4);
				Widget widget2 = mClient;
				widget2.mWidgetFlagsMod.mRemoveFlags = (widget2.mWidgetFlagsMod.mRemoveFlags | 16);
			}
			if (widgetAtHelper == null || widgetAtHelper.mDisabled)
			{
				num3 = num;
				num4 = num2;
				widgetAtHelper = mClient;
			}
			touch.previousLocation.X = touch.previousLocation.X + (num3 - touch.location.X);
			touch.previousLocation.Y = touch.previousLocation.Y + (num4 - touch.location.Y);
			touch.location.X = num3;
			touch.location.Y = num4;
			return widgetAtHelper;
		}

		protected Widget GetClientWidgetAt(int x, int y)
		{
			int num = (int)x - mClient.mX;
			int num2 = (int)y - mClient.mY;
			int theFlags = 16 | mWidgetManager.GetWidgetFlags();
			Widget widgetAtHelper;
			int num3;
			int num4;
			if (mClientLastDown != null)
			{
				CGPoint absPos = mClient.GetAbsPos();
				CGPoint absPos2 = mClientLastDown.GetAbsPos();
				widgetAtHelper = mClientLastDown;
				num3 = (int)(x + absPos.mX - absPos2.mX);
				num4 = (int)(y + absPos.mY - absPos2.mY);
			}
			else
			{
				Widget widget = mClient;
				widget.mWidgetFlagsMod.mRemoveFlags = (widget.mWidgetFlagsMod.mRemoveFlags & -17);
				bool flag;
				widgetAtHelper = mClient.GetWidgetAtHelper(num, num2, theFlags, out flag, out num3, out num4);
				Widget widget2 = mClient;
				widget2.mWidgetFlagsMod.mRemoveFlags = (widget2.mWidgetFlagsMod.mRemoveFlags | 16);
			}
			if (widgetAtHelper == null || widgetAtHelper.mDisabled)
			{
				num3 = num;
				num4 = num2;
				widgetAtHelper = mClient;
			}
			//touch.previousLocation.X = touch.previousLocation.X + (num3 - touch.location.X);
			//touch.previousLocation.Y = touch.previousLocation.Y + (num4 - touch.location.Y);
			//touch.location.X = num3;
			//touch.location.Y = num4;
			return widgetAtHelper;
		}

		public CGPoint GetScrollOffset()
		{
			return mScrollOffset;
		}

		public void SetScrollOffset(float x, float y)
		{
			mScrollOffset.x = x;
			mScrollOffset.y = y;
		}

		protected void CacheDerivedValues()
		{
			if (mClient != null)
			{
				mScrollMin.x = mWidth - mClient.mWidth - mScrollInsets.mRight;
				mScrollMin.y = mHeight - mClient.mHeight - mScrollInsets.mBottom;
				mScrollMax.x = mScrollInsets.mLeft;
				mScrollMax.y = mScrollInsets.mTop;
				int num = ((mScrollMin.x < mScrollMax.x) ? 1 : 0) | ((mScrollMin.y < mScrollMax.y) ? 2 : 0);
				mScrollPractical = (mScrollMode & (ScrollWidget.ScrollMode)num);
			}
			else
			{
				mScrollMin.x = (mScrollMax.x = (mScrollMin.y = (mScrollMax.y = 0f)));
				mScrollPractical = ScrollWidget.ScrollMode.SCROLL_DISABLED;
			}
			if (mPagingEnabled)
			{
				mPageSize.x = mWidth - mScrollInsets.mLeft - mScrollInsets.mRight;
				mPageSize.y = mHeight - mScrollInsets.mTop - mScrollInsets.mBottom;
				if (mClient != null)
				{
					mPageCountHorizontal = (int)Math.Floor(mClient.Width() / mPageSize.x);
					mPageCountVertical = (int)Math.Floor(mClient.Height() / mPageSize.y);
					return;
				}
				mPageCountHorizontal = (mPageCountVertical = 0);
			}
		}

		internal const float SCROLL_TARGET_THRESHOLD_NORM = 0.01f;

		internal const float SCROLL_VELOCITY_THRESHOLD_NORM = 0.0001f;

		internal const float SCROLL_DEVIATION_DAMPING = 0.5f;

		internal const float SCROLL_SPRINGBACK_TENSION = 0.1f;

		internal const float SCROLL_VELOCITY_FILTER_WINDOW = 0.1f;

		internal const float SCROLL_VELOCITY_DAMPING = 0.975f;

		internal const float SCROLL_VELOCITY_DEVIATION_DAMPING = 0.85f;

		internal const float SCROLL_DRAG_THRESHOLD = 4f;

		internal const float SCROLL_PAGE_FLICK_THRESHOLD = 40f;

		internal static readonly int SCROLL_TAP_DELAY_TICKS = SexyAppFrameworkConstants.ticksForSeconds(0.1f);

		internal static readonly int SCROLL_INDICATORS_FLASH_TICKS = SexyAppFrameworkConstants.ticksForSeconds(1f);

		internal static readonly float SCROLL_INDICATORS_FADE_IN_RATE = 0.05f;

		internal static readonly float SCROLL_INDICATORS_FADE_OUT_RATE = 0.02f;

		private bool clientAllowsScroll;

		private CGPoint oldTouch;

		private double oldTouchTime;

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

		protected Insets mScrollInsets = default(Insets);

		protected CGPoint mScrollTarget = default(CGPoint);

		protected CGPoint mScrollOffset = default(CGPoint);

		protected CGPoint mScrollVelocity = default(CGPoint);

		protected CGPoint mScrollTouchReference = default(CGPoint);

		protected CGPoint mScrollOffsetReference = default(CGPoint);

		protected bool mBounceEnabled;

		protected bool mPagingEnabled;

		protected bool mIndicatorsEnabled;

		protected Insets mIndicatorsInsets = default(Insets);

		protected int mIndicatorsFlashTimer;

		protected float mIndicatorsOpacity;

		protected int mCurrentPageHorizontal;

		protected int mCurrentPageVertical;

		protected bool mSeekScrollTarget;

		protected bool mScrollTracking;

		protected double mScrollLastTimestamp;

		public float mSpringOverride;

		protected CGPoint mScrollMin = default(CGPoint);

		protected CGPoint mScrollMax = default(CGPoint);

		protected CGPoint mPageSize = default(CGPoint);

		protected ScrollWidget.ScrollMode mScrollPractical;

		protected int mPageCountHorizontal;

		protected int mPageCountVertical;

		public enum ScrollMode
		{
			SCROLL_DISABLED,
			SCROLL_HORIZONTAL,
			SCROLL_VERTICAL,
			SCROLL_BOTH
		}

		public enum Colors
		{
			COLOR_BACKGROUND
		}

		protected class Overlay
		{
			public Image image;

			public CGPoint offset = default(CGPoint);
		}
	}
}
