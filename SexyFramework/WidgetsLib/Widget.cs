using System;
using System.Collections.Generic;
using Sexy.Drivers;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public abstract class Widget : WidgetContainer
	{
		public Widget()
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
			this.mIsGamepadSelection = false;
			this.mGamepadParent = null;
			this.mGamepadLinkUp = null;
			this.mGamepadLinkDown = null;
			this.mGamepadLinkLeft = null;
			this.mGamepadLinkRight = null;
			this.mDataMenuId = -1;
			this.mColors = new List<SexyColor>();
			this.mMouseInsets = new Insets();
		}

		public override void Dispose()
		{
			this.mColors.Clear();
			base.Dispose();
		}

		public void CopyFrom(Widget rhs)
		{
			base.CopyFrom(rhs);
			this.mVisible = rhs.mVisible;
			this.mMouseVisible = rhs.mMouseVisible;
			this.mDisabled = rhs.mDisabled;
			this.mHasFocus = rhs.mHasFocus;
			this.mIsDown = rhs.mIsDown;
			this.mIsOver = rhs.mIsOver;
			this.mHasTransparencies = rhs.mHasTransparencies;
			this.mDoFinger = rhs.mDoFinger;
			this.mWantsFocus = rhs.mWantsFocus;
			this.mIsGamepadSelection = rhs.mIsGamepadSelection;
			this.mDataMenuId = rhs.mDataMenuId;
			this.mTabPrev = rhs.mTabPrev;
			this.mTabNext = rhs.mTabNext;
			this.mGamepadParent = rhs.mGamepadParent;
			this.mGamepadLinkUp = rhs.mGamepadLinkUp;
			this.mGamepadLinkDown = rhs.mGamepadLinkDown;
			this.mGamepadLinkLeft = rhs.mGamepadLinkLeft;
			this.mGamepadLinkRight = rhs.mGamepadLinkRight;
			this.mMouseInsets.mLeft = rhs.mMouseInsets.mLeft;
			this.mMouseInsets.mRight = rhs.mMouseInsets.mRight;
			this.mMouseInsets.mBottom = rhs.mMouseInsets.mBottom;
			this.mMouseInsets.mTop = rhs.mMouseInsets.mTop;
			this.mColors.Clear();
			for (int i = 0; i < rhs.mColors.Count; i++)
			{
				this.mColors.Add(new SexyColor(rhs.mColors[i]));
			}
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

		public virtual void SetColors3(int[,] theColors, int theNumColors)
		{
			this.mColors.Clear();
			for (int i = 0; i < theNumColors; i++)
			{
				this.SetColor(i, new SexyColor(theColors[i, 0], theColors[i, 1], theColors[i, 2]));
			}
			this.MarkDirty();
		}

		public virtual void SetColors4(int[,] theColors, int theNumColors)
		{
			this.mColors.Clear();
			for (int i = 0; i < theNumColors; i++)
			{
				this.SetColor(i, new SexyColor(theColors[i, 0], theColors[i, 1], theColors[i, 2], theColors[i, 3]));
			}
			this.MarkDirty();
		}

		public virtual void SetColor(int theIdx, SexyColor theColor)
		{
			if (theIdx >= this.mColors.Count)
			{
				this.mColors.Capacity = theIdx + 1;
			}
			this.mColors.Add(theColor);
			this.MarkDirty();
		}

		public virtual SexyColor GetColor(int theIdx)
		{
			if (theIdx < this.mColors.Count)
			{
				return this.mColors[theIdx];
			}
			return this.GetColor_aColor;
		}

		public virtual SexyColor GetColor(int theIdx, SexyColor theDefaultColor)
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
			WidgetManager mWidgetManager = this.mWidgetManager;
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

		public virtual void Resize(Rect theRect)
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

		public virtual bool IsPointVisible(int x, int y)
		{
			return true;
		}

		public virtual void KeyChar(char theChar)
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

		public virtual void MouseDown(int x, int y, int theClickCount)
		{
			if (theClickCount == 3)
			{
				this.MouseDown(x, y, 2, 1);
				return;
			}
			if (theClickCount >= 0)
			{
				this.MouseDown(x, y, 0, theClickCount);
				return;
			}
			this.MouseDown(x, y, 1, -theClickCount);
		}

		public virtual void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
		}

		public virtual void MouseUp(int x, int y)
		{
		}

		public virtual void MouseUp(int x, int y, int theLastDownButtonId)
		{
			this.MouseUp(x, y);
			if (theLastDownButtonId == 3)
			{
				this.MouseUp(x, y, 2, 1);
				return;
			}
			if (theLastDownButtonId >= 0)
			{
				this.MouseUp(x, y, 0, theLastDownButtonId);
				return;
			}
			this.MouseUp(x, y, 1, -theLastDownButtonId);
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

		public virtual void TouchBegan(SexyAppBase.Touch touch)
		{
			int mX = touch.location.mX;
			int mY = touch.location.mY;
			this.MouseDown(mX, mY, 1);
		}

		public virtual void TouchMoved(SexyAppBase.Touch touch)
		{
			int mX = touch.location.mX;
			int mY = touch.location.mY;
			this.MouseDrag(mX, mY);
		}

		public virtual void TouchEnded(SexyAppBase.Touch touch)
		{
			int mX = touch.location.mX;
			int mY = touch.location.mY;
			this.MouseUp(mX, mY, 1);
		}

		public virtual void TouchesCanceled()
		{
		}

		public virtual void SetGamepadLinks(Widget up, Widget down, Widget left, Widget right)
		{
			this.mGamepadLinkUp = up;
			this.mGamepadLinkDown = down;
			this.mGamepadLinkLeft = left;
			this.mGamepadLinkRight = right;
		}

		public virtual void SetGamepadParent(Widget theParent)
		{
			this.mGamepadParent = theParent;
		}

		public virtual void GotGamepadSelection(WidgetLinkDir theDirection)
		{
			this.mIsGamepadSelection = true;
		}

		public virtual void LostGamepadSelection()
		{
			this.mIsGamepadSelection = false;
		}

		public virtual void GamepadButtonDown(GamepadButton theButton, int thePlayer, uint theFlags)
		{
			switch (theButton)
			{
			case GamepadButton.GAMEPAD_BUTTON_UP:
				if (this.mGamepadLinkUp != null && this.mWidgetManager != null)
				{
					Widget widget = this.mGamepadLinkUp;
					while (widget != null && !widget.mVisible)
					{
						widget = widget.mGamepadLinkUp;
					}
					if (widget != null)
					{
						this.mWidgetManager.SetGamepadSelection(widget, WidgetLinkDir.LINK_DIR_UP);
					}
				}
				break;
			case GamepadButton.GAMEPAD_BUTTON_DOWN:
				if (this.mGamepadLinkDown != null && this.mWidgetManager != null)
				{
					Widget widget2 = this.mGamepadLinkDown;
					while (widget2 != null && !widget2.mVisible)
					{
						widget2 = widget2.mGamepadLinkDown;
					}
					if (widget2 != null)
					{
						this.mWidgetManager.SetGamepadSelection(widget2, WidgetLinkDir.LINK_DIR_DOWN);
					}
				}
				break;
			case GamepadButton.GAMEPAD_BUTTON_LEFT:
				if (this.mGamepadLinkLeft != null && this.mWidgetManager != null)
				{
					Widget widget3 = this.mGamepadLinkLeft;
					while (widget3 != null && !widget3.mVisible)
					{
						widget3 = widget3.mGamepadLinkLeft;
					}
					if (widget3 != null)
					{
						this.mWidgetManager.SetGamepadSelection(widget3, WidgetLinkDir.LINK_DIR_LEFT);
					}
				}
				break;
			case GamepadButton.GAMEPAD_BUTTON_RIGHT:
				if (this.mGamepadLinkRight != null && this.mWidgetManager != null)
				{
					Widget widget4 = this.mGamepadLinkRight;
					while (widget4 != null && !widget4.mVisible)
					{
						widget4 = widget4.mGamepadLinkRight;
					}
					if (widget4 != null)
					{
						this.mWidgetManager.SetGamepadSelection(widget4, WidgetLinkDir.LINK_DIR_RIGHT);
					}
				}
				break;
			}
			if (this.mGamepadParent != null)
			{
				this.mGamepadParent.GamepadButtonDown(theButton, thePlayer, theFlags);
			}
		}

		public virtual void GamepadButtonUp(GamepadButton theButton, int thePlayer, uint theFlags)
		{
			if (this.mGamepadParent != null)
			{
				this.mGamepadParent.GamepadButtonUp(theButton, thePlayer, theFlags);
			}
		}

		public virtual void GamepadAxisMove(GamepadAxis theAxis, int thePlayer, float theAxisValue)
		{
			if (this.mGamepadParent != null)
			{
				this.mGamepadParent.GamepadAxisMove(theAxis, thePlayer, theAxisValue);
			}
		}

		public virtual Rect WriteCenteredLine(Graphics g, int anOffset, string theLine)
		{
			Font font = g.GetFont();
			int num = font.StringWidth(theLine);
			int theX = (this.mWidth - num) / 2;
			g.DrawString(theLine, theX, anOffset);
			return new Rect(theX, anOffset - font.GetAscent(), num, font.GetHeight());
		}

		public virtual int WriteString(Graphics g, string theString, int theX, int theY, int theWidth, int theJustification, bool drawString, int theOffset, int theLength)
		{
			bool flag = g.mWriteColoredString;
			g.mWriteColoredString = Widget.mWriteColoredString;
			int result = g.WriteString(theString, theX, theY, theWidth, theJustification, drawString, theOffset, theLength);
			g.mWriteColoredString = flag;
			return result;
		}

		public virtual int WriteWordWrapped(Graphics g, Rect theRect, string theLine, int theLineSpacing, int theJustification)
		{
			bool flag = g.mWriteColoredString;
			g.mWriteColoredString = Widget.mWriteColoredString;
			int result = g.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification);
			g.mWriteColoredString = flag;
			return result;
		}

		public int GetWordWrappedHeight(Graphics g, int theWidth, string theLine, int aLineSpacing)
		{
			int num = 0;
			int num2 = 0;
			return g.GetWordWrappedHeight(theWidth, theLine, aLineSpacing, ref num, ref num2);
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
				g.PushState();
				g.ClipRect(theX + i * (num3 + aSpacing), theY, num3, theNumberStrip.GetHeight());
				g.DrawImage(theNumberStrip, theX + i * (num3 + aSpacing) - num4 * num3, theY);
				g.PopState();
			}
		}

		public virtual bool Contains(int theX, int theY)
		{
			return theX >= this.mX && theX < this.mX + this.mWidth && theY >= this.mY && theY < this.mY + this.mHeight;
		}

		public virtual Rect GetInsetRect()
		{
			return new Rect(this.mX + this.mMouseInsets.mLeft, this.mY + this.mMouseInsets.mTop, this.mWidth - this.mMouseInsets.mLeft - this.mMouseInsets.mRight, this.mHeight - this.mMouseInsets.mTop - this.mMouseInsets.mBottom);
		}

		public void DeferOverlay(int thePriority)
		{
			this.mWidgetManager.DeferOverlay(this, thePriority);
		}

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
			if (this.mWidgetManager.GetGamepadSelection() == this)
			{
				this.mWidgetManager.SetGamepadSelection(null, WidgetLinkDir.LINK_DIR_NONE);
			}
			this.mWidgetManager = null;
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

		public bool mDoFinger;

		public bool mWantsFocus;

		public bool mIsGamepadSelection;

		public int mDataMenuId;

		public List<SexyColor> mColors = new List<SexyColor>();

		public Insets mMouseInsets = new Insets();

		public Widget mTabPrev;

		public Widget mTabNext;

		public Widget mGamepadParent;

		public Widget mGamepadLinkUp;

		public Widget mGamepadLinkDown;

		public Widget mGamepadLinkLeft;

		public Widget mGamepadLinkRight;

		public static bool mWriteColoredString = true;

		private SexyColor GetColor_aColor = default(SexyColor);

		public bool mIsFinishDrawOverlay;
	}
}
