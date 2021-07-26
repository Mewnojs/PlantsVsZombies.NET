using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sexy
{
	internal class ScrollWidget : Widget, ProxyWidgetListener
	{
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
			this.mScrollInsets = insets;
			this.CacheDerivedValues();
		}

		public void SetScrollOffset(CGPoint offset, bool animated)
		{
			if (animated)
			{
				this.mScrollTarget = offset;
				this.mSeekScrollTarget = true;
				return;
			}
			this.mScrollOffset = offset;
			this.mScrollVelocity = CGMaths.CGPointMake(0f, 0f);
			if (this.mClient != null)
			{
				this.mClient.Move((int)this.mScrollOffset.x, (int)this.mScrollOffset.y);
			}
		}

		public void ScrollToMin(bool animated)
		{
			this.SetScrollOffset(CGMaths.CGPointMake((float)this.mScrollInsets.mLeft, (float)this.mScrollInsets.mTop), animated);
		}

		public void ScrollToBottom(bool animated)
		{
			this.SetScrollOffset(CGMaths.CGPointMake(this.mScrollMin.x, this.mScrollMin.y), animated);
		}

		public void ScrollToPoint(CGPoint point, bool animated)
		{
			if (!this.mIsDown)
			{
				this.SetScrollOffset(new CGPoint
				{
					x = -point.mX,
					y = -point.mY
				}, animated);
			}
		}

		public void ScrollRectIntoView(TRect rect, bool animated)
		{
			if (!this.mIsDown)
			{
				float num = (float)(rect.mX + rect.mWidth);
				float num2 = (float)(rect.mY + rect.mHeight);
				float num3 = Math.Max(Math.Min(0f, this.mScrollMin.x), (float)(-(float)rect.mX));
				float num4 = Math.Max(Math.Min(0f, this.mScrollMin.y), (float)(-(float)rect.mY));
				float num5 = Math.Min(this.mScrollMax.x, (float)this.mWidth - num);
				float num6 = Math.Min(this.mScrollMax.y, (float)this.mHeight - num2);
				this.SetScrollOffset(new CGPoint
				{
					x = Math.Min(num5, Math.Max(num3, this.mScrollOffset.x)),
					y = Math.Min(num6, Math.Max(num4, this.mScrollOffset.y))
				}, animated);
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
			this.mIndicatorsEnabled = (null != indicatorsImage);
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
				this.mIndicatorsProxy.Dispose();
				this.mIndicatorsProxy = null;
			}
		}

		public void SetIndicatorsInsets(Insets insets)
		{
			this.mIndicatorsInsets = insets;
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
				this.SetScrollOffset(new CGPoint
				{
					x = (float)this.mScrollInsets.mLeft - (float)this.mCurrentPageHorizontal * this.mPageSize.x,
					y = (float)this.mScrollInsets.mTop - (float)this.mCurrentPageVertical * this.mPageSize.y
				}, animated);
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

		public void AddOverlayImage(Image image, CGPoint offset)
		{
			this.mDrawOverlays = true;
			foreach (ScrollWidget.Overlay overlay in this.mOverlays)
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
				Widget widget = this.mClient;
				widget.mWidgetFlagsMod.mRemoveFlags = (widget.mWidgetFlagsMod.mRemoveFlags | 16);
				this.mClient.Move((int)this.mScrollOffset.x, (int)this.mScrollOffset.y);
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

		public override void Resize(TRect frame)
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

		public override void TouchBegan(_Touch touch)
		{
			if (this.mClient != null)
			{
				this.clientAllowsScroll = this.mClient.DoScroll(touch);
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
				this.mScrollTouchReference = touch.location;
				this.mScrollOffsetReference = CGMaths.CGPointMake((float)this.mClient.mX, (float)this.mClient.mY);
				this.mScrollOffset = this.mScrollOffsetReference;
				this.mScrollLastTimestamp = touch.timestamp;
				this.mScrollTracking = false;
				this.mSeekScrollTarget = false;
				this.mClientLastDown = this.GetClientWidgetAt(touch);
				this.mClientLastDown.mIsDown = true;
				this.mClientLastDown.mIsOver = true;
				this.mClientLastDown.TouchBegan(touch);
			}
		}

		public override void TouchMoved(_Touch touch)
		{
			CGPoint cgpoint = CGMaths.CGPointSubtract(touch.location, this.mScrollTouchReference);
			if (this.mClient != null)
			{
				if (this.clientAllowsScroll)
				{
					if (!this.mScrollTracking && (this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED && Math.Abs(cgpoint.x) > 4f)
					{
						this.mScrollTracking = true;
					}
					if (!this.mScrollTracking && (this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED && Math.Abs(cgpoint.y) > 4f)
					{
						this.mScrollTracking = true;
					}
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
				return;
			}
			if (this.mClientLastDown != null)
			{
				CGPoint b = this.GetAbsPos() - this.mClientLastDown.GetAbsPos();
				CGPoint a = new CGPoint(touch.location.X, touch.location.Y);
				CGPoint cgpoint2 = a + b;
				CGPoint a2 = new CGPoint(cgpoint2.mX + (float)this.mClientLastDown.mX, cgpoint2.mY + (float)this.mClientLastDown.mY);
				bool flag = this.mClientLastDown.GetInsetRect().Contains(a2);
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
				CGMaths.CGPointTranslate(ref touch.location, b.mX, b.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, b.mX, b.mY);
				this.mClientLastDown.TouchMoved(touch);
			}
		}

		public override void TouchEnded(_Touch touch)
		{
			if (this.mScrollTracking)
			{
				this.TouchMotion(touch);
				this.mScrollTracking = false;
				if (this.mPagingEnabled)
				{
					this.SnapToPage();
					return;
				}
			}
			else if (this.mClientLastDown != null)
			{
				CGPoint b = this.GetAbsPos() - this.mClientLastDown.GetAbsPos();
				CGPoint a = new CGPoint(touch.location.X, touch.location.Y);
				a + b;
				CGMaths.CGPointTranslate(ref touch.location, b.mX, b.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, b.mX, b.mY);
				this.mClientLastDown.TouchEnded(touch);
				this.mClientLastDown.mIsDown = false;
				this.mClientLastDown = null;
			}
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
		}

		public override void Update()
		{
			base.Update();
			this.DoScrollUpdate();
			this.DoScrollUpdate();
			this.DoScrollUpdate();
		}

		public void DoScrollUpdate()
		{
			if (this.mVisible && !this.mDisabled)
			{
				if (this.mIsDown)
				{
					this.mIndicatorsFlashTimer = ScrollWidget.SCROLL_INDICATORS_FLASH_TICKS;
				}
				else
				{
					float num = Math.Min(0f, this.mScrollMin.x);
					float num2 = Math.Min(0f, this.mScrollMin.y);
					float num3 = this.mScrollMax.x;
					float num4 = this.mScrollMax.y;
					if (this.mSeekScrollTarget)
					{
						float num5 = CGMaths.CGVectorNorm(CGMaths.CGPointSubtract(this.mScrollTarget, this.mScrollOffset));
						if (num5 < 0.01f)
						{
							this.mScrollOffset = this.mScrollTarget;
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
							num3 = (num = this.mScrollTarget.x);
							num4 = (num2 = this.mScrollTarget.y);
						}
					}
					float num6 = CGMaths.CGVectorNorm(this.mScrollVelocity);
					if (num6 < 0.0001f)
					{
						this.mScrollVelocity = CGMaths.CGPointMake(0f, 0f);
					}
					else
					{
						bool flag = this.mScrollOffset.x < num || this.mScrollOffset.x >= num3;
						bool flag2 = this.mScrollOffset.y < num2 || this.mScrollOffset.y >= num4;
						CGPoint multiplier = default(CGPoint);
						multiplier.x = (flag ? 0.85f : 0.975f);
						multiplier.y = (flag2 ? 0.85f : 0.975f);
						this.mScrollOffset = CGMaths.CGPointAddScaled(this.mScrollOffset, this.mScrollVelocity, 0.01f);
						this.mScrollVelocity = CGMaths.CGPointMultiply(this.mScrollVelocity, multiplier);
					}
					if (this.mScrollOffset.x < num)
					{
						if (this.mBounceEnabled || this.mSeekScrollTarget)
						{
							float num7 = (this.mSpringOverride == 0f) ? 0.1f : this.mSpringOverride;
							this.mScrollOffset.x = this.mScrollOffset.x + num7 * (num - this.mScrollOffset.x);
						}
						else
						{
							this.mScrollOffset.x = num;
							this.mScrollVelocity.x = 0f;
						}
					}
					else if (this.mScrollOffset.x > num3)
					{
						if (this.mBounceEnabled || this.mSeekScrollTarget)
						{
							float num8 = (this.mSpringOverride == 0f) ? 0.1f : this.mSpringOverride;
							this.mScrollOffset.x = this.mScrollOffset.x + num8 * (num3 - this.mScrollOffset.x);
						}
						else
						{
							this.mScrollOffset.x = num3;
							this.mScrollVelocity.x = 0f;
						}
					}
					if (this.mScrollOffset.y < num2)
					{
						if (this.mBounceEnabled || this.mSeekScrollTarget)
						{
							float num9 = (this.mSpringOverride == 0f) ? 0.1f : this.mSpringOverride;
							this.mScrollOffset.y = this.mScrollOffset.y + num9 * (num2 - this.mScrollOffset.y);
						}
						else
						{
							this.mScrollOffset.y = num2;
							this.mScrollVelocity.y = 0f;
						}
					}
					else if (this.mScrollOffset.y > num4)
					{
						if (this.mBounceEnabled || this.mSeekScrollTarget)
						{
							float num10 = (this.mSpringOverride == 0f) ? 0.1f : this.mSpringOverride;
							this.mScrollOffset.y = this.mScrollOffset.y + num10 * (num4 - this.mScrollOffset.y);
						}
						else
						{
							this.mScrollOffset.y = num4;
							this.mScrollVelocity.y = 0f;
						}
					}
					if (this.mClient != null)
					{
						this.mClient.Move((int)this.mScrollOffset.x, (int)this.mScrollOffset.y);
					}
					if (this.mIndicatorsFlashTimer > 0)
					{
						this.mIndicatorsFlashTimer--;
					}
				}
				if (this.mIndicatorsFlashTimer > 0 && this.mIndicatorsOpacity < 1f)
				{
					this.mIndicatorsOpacity = Math.Min(1f, this.mIndicatorsOpacity + ScrollWidget.SCROLL_INDICATORS_FADE_IN_RATE);
					return;
				}
				if (this.mIndicatorsFlashTimer == 0 && this.mIndicatorsOpacity > 0f)
				{
					this.mIndicatorsOpacity = Math.Max(0f, this.mIndicatorsOpacity - ScrollWidget.SCROLL_INDICATORS_FADE_OUT_RATE);
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
			if (this.mBackgroundImage != null)
			{
				g.DrawImage(this.mBackgroundImage, 0, 0);
				return;
			}
			if (this.mFillBackground)
			{
				g.SetColor(this.GetColor(0));
				g.FillRect(0, 0, this.mWidth, this.mHeight);
			}
		}

		public void DrawProxyWidget(Graphics g, ProxyWidget proxyWidget)
		{
			Color color = new Color(255, 255, 255, (int)(255f * this.mIndicatorsOpacity));
			if (color.A != 0)
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
					float num7 = 1f - (this.mScrollOffset.x - num5) / (num6 - num5);
					int num8 = (int)((float)num4 * num7);
					int num9 = num8 + num3;
					num8 = Math.Min(Math.Max(0, num8), num2 - width);
					num9 = Math.Min(Math.Max(width, num9), num2);
					TRect destRect = default(TRect);
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
					float num16 = 1f - (this.mScrollOffset.y - num14) / (num15 - num14);
					int num17 = (int)((float)num13 * num16);
					int num18 = num17 + num12;
					num17 = Math.Min(Math.Max(0, num17), num11 - height);
					num18 = Math.Min(Math.Max(height, num18), num11);
					TRect destRect2 = default(TRect);
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
			this.mScrollOffset = CGMaths.CGPointMake(0f, 0f);
			this.mScrollVelocity = CGMaths.CGPointMake(0f, 0f);
			this.mClip = true;
		}

		protected void SnapToPage()
		{
			CGPoint cgpoint = CGMaths.CGPointSubtract(new CGPoint
			{
				x = (float)this.mScrollInsets.mLeft + this.mPageSize.x / 2f,
				y = (float)this.mScrollInsets.mTop + this.mPageSize.y / 2f
			}, this.mScrollOffset);
			int num = (int)Math.Floor((double)(cgpoint.x / this.mPageSize.x));
			int num2 = (int)Math.Floor((double)(cgpoint.y / this.mPageSize.y));
			num = Math.Max(0, Math.Min(num, this.mPageCountHorizontal - 1));
			num2 = Math.Max(0, Math.Min(num2, this.mPageCountVertical - 1));
			CGPoint cgpoint2 = default(CGPoint);
			cgpoint2.x = (float)this.mScrollInsets.mLeft - (float)num * this.mPageSize.x;
			cgpoint2.y = (float)this.mScrollInsets.mTop - (float)num2 * this.mPageSize.y;
			if (this.mScrollVelocity.x > 40f && cgpoint2.x < this.mScrollOffset.x)
			{
				num--;
			}
			else if (this.mScrollVelocity.x < -40f && cgpoint2.x > this.mScrollOffset.x)
			{
				num++;
			}
			if (this.mScrollVelocity.y > 40f && cgpoint2.y < this.mScrollOffset.y)
			{
				num2--;
			}
			else if (this.mScrollVelocity.y < -40f && cgpoint2.y > this.mScrollOffset.y)
			{
				num2++;
			}
			this.SetPage(num, num2, true);
		}

		protected void TouchMotion(_Touch touch)
		{
			CGPoint cgpoint = CGMaths.CGPointSubtract(touch.location, this.mScrollTouchReference);
			CGPoint cgpoint2 = this.mScrollOffset;
			if ((this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_HORIZONTAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
			{
				cgpoint2.x = this.mScrollOffsetReference.x + cgpoint.X;
				float x = this.mScrollMin.x;
				float x2 = this.mScrollMax.x;
				if (cgpoint2.x < x)
				{
					cgpoint2.x = (this.mBounceEnabled ? (cgpoint2.x + 0.5f * (x - cgpoint2.x)) : x);
					this.mScrollVelocity.x = 0f;
				}
				else if (cgpoint2.x > x2)
				{
					cgpoint2.x = (this.mBounceEnabled ? (cgpoint2.x + 0.5f * (x2 - cgpoint2.x)) : x2);
					this.mScrollVelocity.x = 0f;
				}
				else
				{
					float num = cgpoint2.x - this.mScrollOffset.x;
					double num2 = touch.timestamp - this.mScrollLastTimestamp;
					if (num2 > 0.0)
					{
						double num3 = (double)num / num2;
						double num4 = Math.Min(1.0, num2 / 0.10000000149011612);
						this.mScrollVelocity.x = (float)(num4 * num3 + (1.0 - num4) * (double)this.mScrollVelocity.x);
					}
				}
			}
			if ((this.mScrollPractical & ScrollWidget.ScrollMode.SCROLL_VERTICAL) != ScrollWidget.ScrollMode.SCROLL_DISABLED)
			{
				cgpoint2.y = this.mScrollOffsetReference.y + cgpoint.Y;
				float y = this.mScrollMin.y;
				float y2 = this.mScrollMax.y;
				if (cgpoint2.y < y)
				{
					cgpoint2.y = (this.mBounceEnabled ? (cgpoint2.y + 0.5f * (y - cgpoint2.y)) : y);
					this.mScrollVelocity.y = 0f;
				}
				else if (cgpoint2.y > y2)
				{
					cgpoint2.y = (this.mBounceEnabled ? (cgpoint2.y + 0.5f * (y2 - cgpoint2.y)) : y2);
					this.mScrollVelocity.y = 0f;
				}
				else
				{
					float num5 = cgpoint2.y - this.mScrollOffset.mY;
					double num6 = touch.timestamp - this.mScrollLastTimestamp;
					if (num6 > 0.0)
					{
						double num7 = (double)num5 / num6;
						double num8 = Math.Min(1.0, num6 / 0.10000000149011612);
						this.mScrollVelocity.y = (float)(num8 * num7 + (1.0 - num8) * (double)this.mScrollVelocity.y);
					}
				}
			}
			this.mScrollOffset = cgpoint2;
			this.mScrollLastTimestamp = touch.timestamp;
			this.mClient.Move((int)this.mScrollOffset.x, (int)this.mScrollOffset.y);
			this.oldTouch = touch.location;
			this.oldTouchTime = touch.timestamp;
		}

		protected Widget GetClientWidgetAt(_Touch touch)
		{
			int num = (int)touch.location.X - this.mClient.mX;
			int num2 = (int)touch.location.Y - this.mClient.mY;
			int theFlags = 16 | this.mWidgetManager.GetWidgetFlags();
			Widget widgetAtHelper;
			int num3;
			int num4;
			if (this.mClientLastDown != null)
			{
				CGPoint absPos = this.mClient.GetAbsPos();
				CGPoint absPos2 = this.mClientLastDown.GetAbsPos();
				widgetAtHelper = this.mClientLastDown;
				num3 = (int)(touch.location.X + absPos.mX - absPos2.mX);
				num4 = (int)(touch.location.Y + absPos.mY - absPos2.mY);
			}
			else
			{
				Widget widget = this.mClient;
				widget.mWidgetFlagsMod.mRemoveFlags = (widget.mWidgetFlagsMod.mRemoveFlags & -17);
				bool flag;
				widgetAtHelper = this.mClient.GetWidgetAtHelper(num, num2, theFlags, out flag, out num3, out num4);
				Widget widget2 = this.mClient;
				widget2.mWidgetFlagsMod.mRemoveFlags = (widget2.mWidgetFlagsMod.mRemoveFlags | 16);
			}
			if (widgetAtHelper == null || widgetAtHelper.mDisabled)
			{
				num3 = num;
				num4 = num2;
				widgetAtHelper = this.mClient;
			}
			touch.previousLocation.X = touch.previousLocation.X + ((float)num3 - touch.location.X);
			touch.previousLocation.Y = touch.previousLocation.Y + ((float)num4 - touch.location.Y);
			touch.location.X = (float)num3;
			touch.location.Y = (float)num4;
			return widgetAtHelper;
		}

		public CGPoint GetScrollOffset()
		{
			return this.mScrollOffset;
		}

		public void SetScrollOffset(float x, float y)
		{
			this.mScrollOffset.x = x;
			this.mScrollOffset.y = y;
		}

		protected void CacheDerivedValues()
		{
			if (this.mClient != null)
			{
				this.mScrollMin.x = (float)(this.mWidth - this.mClient.mWidth - this.mScrollInsets.mRight);
				this.mScrollMin.y = (float)(this.mHeight - this.mClient.mHeight - this.mScrollInsets.mBottom);
				this.mScrollMax.x = (float)this.mScrollInsets.mLeft;
				this.mScrollMax.y = (float)this.mScrollInsets.mTop;
				int num = ((this.mScrollMin.x < this.mScrollMax.x) ? 1 : 0) | ((this.mScrollMin.y < this.mScrollMax.y) ? 2 : 0);
				this.mScrollPractical = (this.mScrollMode & (ScrollWidget.ScrollMode)num);
			}
			else
			{
				this.mScrollMin.x = (this.mScrollMax.x = (this.mScrollMin.y = (this.mScrollMax.y = 0f)));
				this.mScrollPractical = ScrollWidget.ScrollMode.SCROLL_DISABLED;
			}
			if (this.mPagingEnabled)
			{
				this.mPageSize.x = (float)(this.mWidth - this.mScrollInsets.mLeft - this.mScrollInsets.mRight);
				this.mPageSize.y = (float)(this.mHeight - this.mScrollInsets.mTop - this.mScrollInsets.mBottom);
				if (this.mClient != null)
				{
					this.mPageCountHorizontal = (int)Math.Floor((double)((float)this.mClient.Width() / this.mPageSize.x));
					this.mPageCountVertical = (int)Math.Floor((double)((float)this.mClient.Height() / this.mPageSize.y));
					return;
				}
				this.mPageCountHorizontal = (this.mPageCountVertical = 0);
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
