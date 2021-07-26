using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sexy
{
	internal class Widget : WidgetContainer
	{
		public void WidgetRemovedHelper()
		{
			if (this.mWidgetManager == null)
			{
				return;
			}
			foreach (Widget widget in this.mWidgets)
			{
				widget.WidgetRemovedHelper();
			}
			this.mWidgetManager.DisableWidget(this);
			foreach (PreModalInfo preModalInfo in this.mWidgetManager.mPreModalInfoList)
			{
				if (preModalInfo.mPrevBaseModalWidget == this)
				{
					preModalInfo.mPrevBaseModalWidget = null;
				}
				if (preModalInfo.mPrevFocusWidget == this)
				{
					preModalInfo.mPrevFocusWidget = null;
				}
			}
			this.RemovedFromManager(this.mWidgetManager);
			this.MarkDirtyFull(this);
			this.mWidgetManager = null;
		}

		public Widget()
		{
			this.Reset();
		}

		public virtual bool DoScroll(_Touch touch)
		{
			return true;
		}

		protected virtual void Reset()
		{
			this.mWidgetManager = null;
			this.mVisible = true;
			this.mDisabled = false;
			this.mIsDown = false;
			this.mIsOver = false;
			this.mDoFinger = false;
			this.mMouseVisible = true;
			this.mHasFocus = false;
			this.mHasTransparencies = false;
			this.mWantsFocus = false;
			this.mTabPrev = null;
			this.mTabNext = null;
		}

		public override void Dispose()
		{
			this.mColors.Clear();
		}

		public virtual void OrderInManagerChanged()
		{
		}

		public virtual void SetVisible(bool isVisible)
		{
			if (this.mVisible == isVisible)
			{
				return;
			}
			this.mVisible = isVisible;
			if (this.mVisible)
			{
				this.MarkDirty();
			}
			else
			{
				this.MarkDirtyFull();
			}
			if (this.mWidgetManager != null)
			{
				this.mWidgetManager.RehupMouse();
			}
		}

		public virtual void SetColors(int[,] theColors, int theNumColors)
		{
			this.mColors.Clear();
			for (int i = 0; i < theNumColors; i++)
			{
				this.SetColor(i, new Color(theColors[i, 0], theColors[i, 1], theColors[i, 2]));
			}
			this.MarkDirty();
		}

		public virtual void SetColor(int theIdx, SexyColor theColor)
		{
			this.SetColor(theIdx, theColor.Color);
		}

		public virtual void SetColor(ButtonWidget.ColorType theIdx, Color theColor)
		{
			this.SetColor((int)theIdx, theColor);
		}

		public virtual void SetColor(int theIdx, Color theColor)
		{
			if (theIdx >= this.mColors.Count)
			{
				this.mColors.Add(theColor);
			}
			else
			{
				this.mColors[theIdx] = theColor;
			}
			this.MarkDirty();
		}

		public virtual Color GetColor(Dialog.DialogColour theIdx)
		{
			return this.GetColor((int)theIdx);
		}

		public virtual Color GetColor(int theIdx)
		{
			Color result = default(Color);
			if (theIdx < this.mColors.Count)
			{
				return this.mColors[theIdx];
			}
			return result;
		}

		public virtual Color GetColor(Dialog.DialogColour theIdx, Color theDefaultColor)
		{
			return this.GetColor((int)theIdx, theDefaultColor);
		}

		public virtual Color GetColor(int theIdx, Color theDefaultColor)
		{
			if (theIdx < this.mColors.Count)
			{
				return this.mColors[theIdx];
			}
			return theDefaultColor;
		}

		public virtual void SetDisabled(bool isDisabled)
		{
			if (this.mDisabled == isDisabled)
			{
				return;
			}
			this.mDisabled = isDisabled;
			if (isDisabled && this.mWidgetManager != null)
			{
				this.mWidgetManager.DisableWidget(this);
			}
			this.MarkDirty();
			if (!isDisabled && this.mWidgetManager != null && this.Contains(this.mWidgetManager.mLastMouseX, this.mWidgetManager.mLastMouseY))
			{
				this.mWidgetManager.MousePosition(this.mWidgetManager.mLastMouseX, this.mWidgetManager.mLastMouseY);
			}
		}

		public virtual void ShowFinger(bool on)
		{
		}

		public virtual void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			if (this.mX == theX && this.mY == theY && this.mWidth == theWidth && this.mHeight == theHeight)
			{
				return;
			}
			this.MarkDirtyFull();
			this.mX = theX;
			this.mY = theY;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.MarkDirty();
			if (this.mWidgetManager != null)
			{
				this.mWidgetManager.RehupMouse();
			}
		}

		public virtual void Resize(TRect theRect)
		{
			this.Resize(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public virtual void Move(int theNewX, int theNewY)
		{
			this.Resize(theNewX, theNewY, this.mWidth, this.mHeight);
		}

		public virtual bool WantsFocus()
		{
			return this.mWantsFocus;
		}

		public override void Draw(Graphics g)
		{
		}

		public virtual void DrawOverlay(Graphics g)
		{
		}

		public virtual void DrawOverlay(Graphics g, int thePriority)
		{
			this.DrawOverlay(g);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void UpdateF(float theFrac)
		{
		}

		public virtual void GotFocus()
		{
			this.mHasFocus = true;
		}

		public virtual void LostFocus()
		{
			this.mHasFocus = false;
		}

		public virtual void KeyChar(SexyChar theChar)
		{
		}

		public virtual void KeyDown(KeyCode theKey)
		{
			if (theKey == KeyCode.KEYCODE_TAB)
			{
				if (this.mWidgetManager.mKeyDown[16])
				{
					if (this.mTabPrev != null)
					{
						this.mWidgetManager.SetFocus(this.mTabPrev);
						return;
					}
				}
				else if (this.mTabNext != null)
				{
					this.mWidgetManager.SetFocus(this.mTabNext);
				}
			}
		}

		public virtual void KeyUp(KeyCode theKey)
		{
		}

		public virtual void MouseEnter()
		{
		}

		public virtual void MouseLeave()
		{
		}

		public virtual void MouseMove(int x, int y)
		{
		}

		public virtual void MouseDown(int x, int y, int theMagicCode)
		{
			if (theMagicCode == 3)
			{
				this.MouseDown(x, y, 2, 1);
				return;
			}
			if (theMagicCode >= 0)
			{
				this.MouseDown(x, y, 0, theMagicCode);
				return;
			}
			this.MouseDown(x, y, 1, -theMagicCode);
		}

		public virtual void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
		}

		public virtual void MouseUp(int x, int y, int theMagicCode)
		{
			if (theMagicCode == 3)
			{
				this.MouseUp(x, y, 2, 1);
				return;
			}
			if (theMagicCode >= 0)
			{
				this.MouseUp(x, y, 0, theMagicCode);
				return;
			}
			this.MouseUp(x, y, 1, -theMagicCode);
		}

		public virtual void MouseUp(int x, int y, int theBtnNum, int theClickCount)
		{
		}

		public virtual void MouseDrag(int x, int y)
		{
		}

		public virtual void MouseWheel(int theDelta)
		{
		}

		public virtual void TouchBegan(_Touch touch)
		{
			int x = (int)touch.location.X;
			int y = (int)touch.location.Y;
			this.MouseDown(x, y, 1);
		}

		public virtual void TouchMoved(_Touch touch)
		{
			int x = (int)touch.location.X;
			int y = (int)touch.location.Y;
			this.MouseDrag(x, y);
		}

		public virtual void TouchEnded(_Touch touch)
		{
			int x = (int)touch.location.X;
			int y = (int)touch.location.Y;
			this.MouseUp(x, y, 1);
		}

		public virtual void TouchesCanceled()
		{
		}

		public virtual bool IsPointVisible(int x, int y)
		{
			return true;
		}

		public virtual TRect WriteCenteredLine(Graphics g, int anOffset, string theLine)
		{
			Font font = g.GetFont();
			int num = font.StringWidth(theLine);
			int theX = (this.mWidth - num) / 2;
			g.DrawString(theLine, theX, anOffset);
			return new TRect(theX, anOffset - font.GetAscent(), num, font.GetHeight());
		}

		public virtual TRect WriteCenteredLine(Graphics g, int anOffset, string theLine, Color theColor1, Color theColor2)
		{
			return this.WriteCenteredLine(g, anOffset, theLine, theColor1, theColor2, new TPoint(1, 2));
		}

		public virtual TRect WriteCenteredLine(Graphics g, int anOffset, string theLine, Color theColor1, Color theColor2, TPoint theShadowOffset)
		{
			Font font = g.GetFont();
			int num = font.StringWidth(theLine);
			int num2 = (this.mWidth - num) / 2;
			g.SetColor(theColor2);
			g.DrawString(theLine, (this.mWidth - num) / 2 + theShadowOffset.mX, anOffset + theShadowOffset.mY);
			g.SetColor(theColor1);
			g.DrawString(theLine, (this.mWidth - num) / 2, anOffset);
			return new TRect(num2 + Math.Min(0, theShadowOffset.mX), anOffset - font.GetAscent() + Math.Min(0, theShadowOffset.mY), num + Math.Abs(theShadowOffset.mX), font.GetHeight() + Math.Abs(theShadowOffset.mY));
		}

		public virtual int WriteString(Graphics g, string theString, int theX, int theY, int theWidth, int theJustification, bool drawString, int theOffset)
		{
			return this.WriteString(g, theString, theX, theY, theWidth, theJustification, drawString, theOffset, -1);
		}

		public virtual int WriteString(Graphics g, string theString, int theX, int theY, int theWidth, int theJustification, bool drawString)
		{
			return this.WriteString(g, theString, theX, theY, theWidth, theJustification, drawString, 0, -1);
		}

		public virtual int WriteString(Graphics g, string theString, int theX, int theY, int theWidth, int theJustification)
		{
			return this.WriteString(g, theString, theX, theY, theWidth, theJustification, true, 0, -1);
		}

		public virtual int WriteString(Graphics g, string theString, int theX, int theY, int theWidth)
		{
			return this.WriteString(g, theString, theX, theY, theWidth, -1, true, 0, -1);
		}

		public virtual int WriteString(Graphics g, string theString, int theX, int theY)
		{
			return this.WriteString(g, theString, theX, theY, -1, -1, true, 0, -1);
		}

		public virtual int WriteString(Graphics g, string theString, int theX, int theY, int theWidth, int theJustification, bool drawString, int theOffset, int theLength)
		{
			bool flag = g.mWriteColoredString;
			g.mWriteColoredString = Widget.mWriteColoredString;
			int result = g.WriteString(theString, theX, theY, theWidth, theJustification, drawString, theOffset, theLength);
			g.mWriteColoredString = flag;
			return result;
		}

		public virtual int WriteWordWrapped(Graphics g, TRect theRect, string theLine, int theLineSpacing, int theJustification)
		{
			bool flag = g.mWriteColoredString;
			g.mWriteColoredString = Widget.mWriteColoredString;
			int result = g.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification);
			g.mWriteColoredString = flag;
			return result;
		}

		public virtual int GetWordWrappedHeight(Graphics g, int theWidth, string theLine, int aLineSpacing)
		{
			int num = 0;
			int theMaxChars = 0;
			return g.GetWordWrappedHeight(theWidth, theLine, aLineSpacing, ref num, theMaxChars);
		}

		public virtual int GetNumDigits(int theNumber)
		{
			int num = 10;
			int num2 = 1;
			while (theNumber >= num)
			{
				num2++;
				num *= 10;
			}
			return num2;
		}

		public virtual void WriteNumberFromStrip(Graphics g, int theNumber, int theX, int theY, Image theNumberStrip, int aSpacing)
		{
			int num = 10;
			int num2 = 1;
			while (theNumber >= num)
			{
				num2++;
				num *= 10;
			}
			if (theNumber == 0)
			{
				num = 10;
			}
			int num3 = theNumberStrip.GetWidth() / 10;
			for (int i = 0; i < num2; i++)
			{
				num /= 10;
				int num4 = theNumber / num % 10;
				Graphics graphics = g.Create();
				graphics.ClipRect(theX + i * (num3 + aSpacing), theY, num3, theNumberStrip.GetHeight());
				graphics.DrawImage(theNumberStrip, theX + i * (num3 + aSpacing) - num4 * num3, theY);
				graphics.Dispose();
			}
		}

		public virtual bool Contains(int theX, int theY)
		{
			return theX >= this.mX && theX < this.mX + this.mWidth && theY >= this.mY && theY < this.mY + this.mHeight;
		}

		public virtual TRect GetInsetRect()
		{
			return new TRect(this.mX + this.mMouseInsets.mLeft, this.mY + this.mMouseInsets.mTop, this.mWidth - this.mMouseInsets.mLeft - this.mMouseInsets.mRight, this.mHeight - this.mMouseInsets.mTop - this.mMouseInsets.mBottom);
		}

		public void DeferOverlay()
		{
			this.DeferOverlay(0);
		}

		public void DeferOverlay(int thePriority)
		{
			this.mWidgetManager.DeferOverlay(this, thePriority);
		}

		public int Left()
		{
			return this.mX;
		}

		public int Top()
		{
			return this.mY;
		}

		public int Right()
		{
			return this.mX + this.mWidth;
		}

		public int Bottom()
		{
			return this.mY + this.mHeight;
		}

		public int Width()
		{
			return this.mWidth;
		}

		public int Height()
		{
			return this.mHeight;
		}

		public void Layout(int theLayoutFlags, Widget theRelativeWidget, int theLeftPad, int theTopPad, int theWidthPad)
		{
			this.Layout(theLayoutFlags, theRelativeWidget, theLeftPad, theTopPad, theWidthPad, 0);
		}

		public void Layout(int theLayoutFlags, Widget theRelativeWidget, int theLeftPad, int theTopPad)
		{
			this.Layout(theLayoutFlags, theRelativeWidget, theLeftPad, theTopPad, 0, 0);
		}

		public void Layout(int theLayoutFlags, Widget theRelativeWidget, int theLeftPad)
		{
			this.Layout(theLayoutFlags, theRelativeWidget, theLeftPad, 0, 0, 0);
		}

		public void Layout(int theLayoutFlags, Widget theRelativeWidget)
		{
			this.Layout(theLayoutFlags, theRelativeWidget, 0, 0, 0, 0);
		}

		public void Layout(int theLayoutFlags, Widget theRelativeWidget, int theLeftPad, int theTopPad, int theWidthPad, int theHeightPad)
		{
			int num = theRelativeWidget.Left();
			int num2 = theRelativeWidget.Top();
			if (theRelativeWidget == this.mParent)
			{
				num = 0;
				num2 = 0;
			}
			int num3 = theRelativeWidget.Width();
			int num4 = theRelativeWidget.Height();
			int num5 = num + num3;
			int num6 = num2 + num4;
			int num7 = this.Left();
			int num8 = this.Top();
			int num9 = this.Width();
			int num10 = this.Height();
			for (int i = 1; i < 4194304; i <<= 1)
			{
				if ((theLayoutFlags & i) != 0)
				{
					LayoutFlags layoutFlags = (LayoutFlags)i;
					if (layoutFlags <= LayoutFlags.LAY_Left)
					{
						if (layoutFlags <= LayoutFlags.LAY_SetWidth)
						{
							if (layoutFlags <= LayoutFlags.LAY_SetLeft)
							{
								switch (layoutFlags)
								{
								case LayoutFlags.LAY_SameWidth:
									num9 = num3 + theWidthPad;
									break;
								case LayoutFlags.LAY_SameHeight:
									num10 = num4 + theHeightPad;
									break;
								default:
									if (layoutFlags == LayoutFlags.LAY_SetLeft)
									{
										num7 = theLeftPad;
									}
									break;
								}
							}
							else if (layoutFlags != LayoutFlags.LAY_SetTop)
							{
								if (layoutFlags == LayoutFlags.LAY_SetWidth)
								{
									num9 = theWidthPad;
								}
							}
							else
							{
								num8 = theTopPad;
							}
						}
						else if (layoutFlags <= LayoutFlags.LAY_Above)
						{
							if (layoutFlags != LayoutFlags.LAY_SetHeight)
							{
								if (layoutFlags == LayoutFlags.LAY_Above)
								{
									num8 = num2 - num10 + theTopPad;
								}
							}
							else
							{
								num10 = theHeightPad;
							}
						}
						else if (layoutFlags != LayoutFlags.LAY_Below)
						{
							if (layoutFlags != LayoutFlags.LAY_Right)
							{
								if (layoutFlags == LayoutFlags.LAY_Left)
								{
									num7 = num - num9 + theLeftPad;
								}
							}
							else
							{
								num7 = num5 + theLeftPad;
							}
						}
						else
						{
							num8 = num6 + theTopPad;
						}
					}
					else if (layoutFlags <= LayoutFlags.LAY_GrowToRight)
					{
						if (layoutFlags <= LayoutFlags.LAY_SameRight)
						{
							if (layoutFlags != LayoutFlags.LAY_SameLeft)
							{
								if (layoutFlags == LayoutFlags.LAY_SameRight)
								{
									num7 = num5 - num9 + theLeftPad;
								}
							}
							else
							{
								num7 = num + theLeftPad;
							}
						}
						else if (layoutFlags != LayoutFlags.LAY_SameTop)
						{
							if (layoutFlags != LayoutFlags.LAY_SameBottom)
							{
								if (layoutFlags == LayoutFlags.LAY_GrowToRight)
								{
									num9 = num5 - num7 + theWidthPad;
								}
							}
							else
							{
								num8 = num6 - num10 + theTopPad;
							}
						}
						else
						{
							num8 = num2 + theTopPad;
						}
					}
					else if (layoutFlags <= LayoutFlags.LAY_GrowToTop)
					{
						if (layoutFlags != LayoutFlags.LAY_GrowToLeft)
						{
							if (layoutFlags == LayoutFlags.LAY_GrowToTop)
							{
								num10 = num2 - num8 + theHeightPad;
							}
						}
						else
						{
							num9 = num - num7 + theWidthPad;
						}
					}
					else if (layoutFlags != LayoutFlags.LAY_GrowToBottom)
					{
						if (layoutFlags != LayoutFlags.LAY_HCenter)
						{
							if (layoutFlags == LayoutFlags.LAY_VCenter)
							{
								num8 = num2 + (num4 - num10) / 2 + theTopPad;
							}
						}
						else
						{
							num7 = num + (num3 - num9) / 2 + theLeftPad;
						}
					}
					else
					{
						num10 = num6 - num8 + theHeightPad;
					}
				}
			}
			this.Resize(num7, num8, num9, num10);
		}

		public bool mVisible;

		public bool mMouseVisible;

		public bool mDisabled;

		public bool mHasFocus;

		public bool mIsDown;

		public bool mIsOver;

		public bool mHasTransparencies;

		public List<Color> mColors = new List<Color>();

		public Insets mMouseInsets = default(Insets);

		public bool mDoFinger;

		public bool mWantsFocus;

		public Widget mTabPrev;

		public Widget mTabNext;

		public static bool mWriteColoredString = true;
	}
}
