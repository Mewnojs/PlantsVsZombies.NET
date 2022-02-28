using System;
using System.Collections.Generic;
using Sexy.Drivers;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class WidgetManager : WidgetContainer
	{
		public WidgetManager(SexyAppBase theApp)
		{
			this.mApp = theApp;
			this.mMinDeferredOverlayPriority = int.MaxValue;
			this.mWidgetManager = this;
			this.mMouseIn = false;
			this.mDefaultTab = null;
			this.mImage = null;
			this.mLastHadTransients = false;
			this.mPopupCommandWidget = null;
			this.mFocusWidget = null;
			this.mLastDownWidget = null;
			this.mOverWidget = null;
			this.mBaseModalWidget = null;
			this.mGamepadSelectionWidget = null;
			this.mDefaultBelowModalFlagsMod.mRemoveFlags = 48;
			this.mWidth = 0;
			this.mHeight = 0;
			this.mHasFocus = true;
			this.mUpdateCnt = 0;
			this.mLastDownButtonId = 0;
			this.mDownButtons = 0;
			this.mActualDownButtons = 0;
			this.mWidgetFlags = 61;
			for (int i = 0; i < 255; i++)
			{
				this.mKeyDown[i] = false;
			}
		}

		public override void Dispose()
		{
			this.FreeResources();
			base.Dispose();
		}

		public void FreeResources()
		{
		}

		public void AddBaseModal(Widget theWidget, FlagsMod theBelowFlagsMod)
		{
			PreModalInfo preModalInfo = new PreModalInfo();
			preModalInfo.mBaseModalWidget = theWidget;
			preModalInfo.mPrevBaseModalWidget = this.mBaseModalWidget;
			preModalInfo.mPrevFocusWidget = this.mFocusWidget;
			preModalInfo.mPrevBelowModalFlagsMod = this.mBelowModalFlagsMod;
			this.mPreModalInfoList.AddLast(preModalInfo);
			this.SetBaseModal(theWidget, theBelowFlagsMod);
		}

		public void AddBaseModal(Widget theWidget)
		{
			this.AddBaseModal(theWidget, this.mDefaultBelowModalFlagsMod);
		}

		public void RemoveBaseModal(Widget theWidget)
		{
			bool flag = true;
			while (this.mPreModalInfoList.Count > 0)
			{
				PreModalInfo value = this.mPreModalInfoList.Last.Value;
				if (flag && value.mBaseModalWidget != theWidget)
				{
					return;
				}
				bool flag2 = value.mPrevBaseModalWidget != null || this.mPreModalInfoList.Count == 1;
				this.SetBaseModal(value.mPrevBaseModalWidget, value.mPrevBelowModalFlagsMod);
				if (this.mFocusWidget == null)
				{
					this.mFocusWidget = value.mPrevFocusWidget;
					if (this.mFocusWidget != null)
					{
						this.mFocusWidget.GotFocus();
					}
				}
				this.mPreModalInfoList.RemoveLast();
				if (flag2)
				{
					return;
				}
				flag = false;
			}
		}

		public void Resize(Rect theMouseDestRect, Rect theMouseSourceRect)
		{
			this.mWidth = theMouseDestRect.mWidth + 2 * theMouseDestRect.mX;
			this.mHeight = theMouseDestRect.mHeight + 2 * theMouseDestRect.mY;
			this.mMouseDestRect = theMouseDestRect;
			this.mMouseSourceRect = theMouseSourceRect;
		}

		public new void DisableWidget(Widget theWidget)
		{
			if (this.mOverWidget == theWidget)
			{
				Widget theWidget2 = this.mOverWidget;
				this.mOverWidget = null;
				this.MouseLeave(theWidget2);
			}
			if (this.mLastDownWidget == theWidget)
			{
				Widget theWidget3 = this.mLastDownWidget;
				this.mLastDownWidget = null;
				this.DoMouseUps(theWidget3, this.mDownButtons);
				this.mDownButtons = 0;
			}
			if (this.mFocusWidget == theWidget)
			{
				Widget widget = this.mFocusWidget;
				this.mFocusWidget = null;
				widget.LostFocus();
			}
			if (this.mBaseModalWidget == theWidget)
			{
				this.mBaseModalWidget = null;
			}
		}

		public Widget GetAnyWidgetAt(int x, int y, ref int theWidgetX, ref int theWidgetY)
		{
			bool flag = false;
			return base.GetWidgetAtHelper(x, y, this.GetWidgetFlags(), ref flag, ref theWidgetX, ref theWidgetY);
		}

		public Widget GetWidgetAt(int x, int y, ref int theWidgetX, ref int theWidgetY)
		{
			Widget widget = this.GetAnyWidgetAt(x, y, ref theWidgetX, ref theWidgetY);
			if (widget != null && widget.mDisabled)
			{
				widget = null;
			}
			return widget;
		}

		public new void SetFocus(Widget aWidget)
		{
			if (aWidget == this.mFocusWidget)
			{
				return;
			}
			if (this.mFocusWidget != null)
			{
				this.mFocusWidget.LostFocus();
			}
			if (aWidget != null && aWidget.mWidgetManager == this)
			{
				this.mFocusWidget = aWidget;
				if (this.mHasFocus && this.mFocusWidget != null)
				{
					this.mFocusWidget.GotFocus();
					return;
				}
			}
			else
			{
				this.mFocusWidget = null;
			}
		}

		public void GotFocus()
		{
			if (!this.mHasFocus)
			{
				this.mHasFocus = true;
				if (this.mFocusWidget != null)
				{
					this.mFocusWidget.GotFocus();
				}
			}
		}

		public void LostFocus()
		{
			if (!this.mHasFocus)
			{
				this.mActualDownButtons = 0;
				for (int i = 0; i < 255; i++)
				{
					if (this.mKeyDown[i])
					{
						this.KeyUp((KeyCode)i);
					}
				}
				this.mHasFocus = false;
				if (this.mFocusWidget != null)
				{
					this.mFocusWidget.LostFocus();
				}
			}
		}

		public void InitModalFlags(ModalFlags theModalFlags)
		{
			theModalFlags.mIsOver = this.mBaseModalWidget == null;
			theModalFlags.mOverFlags = this.GetWidgetFlags();
			theModalFlags.mUnderFlags = FlagsMod.GetModFlags(theModalFlags.mOverFlags, this.mBelowModalFlagsMod);
		}

		public void DrawWidgetsTo(Graphics g)
		{
			g.Translate(this.mMouseDestRect.mX, this.mMouseDestRect.mY);
			this.mCurG = new Graphics(g);
			List<KeyValuePair<Widget, int>> list = this.mDeferredOverlayWidgets;
			this.mDeferredOverlayWidgets.Clear();
			ModalFlags modalFlags = new ModalFlags();
			this.InitModalFlags(modalFlags);
			foreach (Widget widget in this.mWidgets)
			{
				if (widget.mVisible)
				{
					g.PushState();
					g.SetFastStretch(!g.Is3D());
					g.SetLinearBlend(g.Is3D());
					g.Translate(-this.mMouseDestRect.mX, -this.mMouseDestRect.mY);
					g.Translate(widget.mX, widget.mY);
					widget.DrawAll(modalFlags, g);
					g.PopState();
				}
			}
			this.FlushDeferredOverlayWidgets(int.MaxValue);
			this.mDeferredOverlayWidgets = list;
			this.mCurG = null;
		}

		public void DoMouseUps(Widget theWidget, int theDownCode)
		{
			int[] array = new int[] { 1, -1, 3 };
			for (int i = 0; i < 3; i++)
			{
				if ((theDownCode & (1 << i)) != 0)
				{
					theWidget.mIsDown = false;
					theWidget.MouseUp(this.mLastMouseX - theWidget.mX, this.mLastMouseY - theWidget.mY, array[i]);
				}
			}
		}

		public void DoMouseUps()
		{
			if (this.mLastDownWidget != null && this.mDownButtons != 0)
			{
				this.DoMouseUps(this.mLastDownWidget, this.mDownButtons);
				this.mDownButtons = 0;
				this.mLastDownWidget = null;
			}
		}

		public void DeferOverlay(Widget theWidget, int thePriority)
		{
			theWidget.mIsFinishDrawOverlay = false;
			this.mDeferredOverlayWidgets.Add(new KeyValuePair<Widget, int>(theWidget, thePriority));
			if (thePriority < this.mMinDeferredOverlayPriority)
			{
				this.mMinDeferredOverlayPriority = thePriority;
			}
		}

		public void FlushDeferredOverlayWidgets(int theMaxPriority)
		{
			if (this.mCurG == null)
			{
				return;
			}
			Graphics graphics = new Graphics(this.mCurG);
			while (this.mMinDeferredOverlayPriority <= theMaxPriority)
			{
				int num = int.MaxValue;
				for (int i = 0; i < this.mDeferredOverlayWidgets.Count; i++)
				{
					Widget key = this.mDeferredOverlayWidgets[i].Key;
					if (key != null && !key.mIsFinishDrawOverlay)
					{
						int value = this.mDeferredOverlayWidgets[i].Value;
						if (value == this.mMinDeferredOverlayPriority)
						{
							graphics.PushState();
							graphics.Translate(-this.mMouseDestRect.mX, -this.mMouseDestRect.mY);
							graphics.Translate(key.mX, key.mY);
							graphics.SetFastStretch(graphics.Is3D());
							graphics.SetLinearBlend(graphics.Is3D());
							this.mDeferredOverlayWidgets[i].Key.mIsFinishDrawOverlay = true;
							key.DrawOverlay(graphics, value);
							graphics.PopState();
						}
						else if (value < num)
						{
							num = value;
						}
					}
				}
				this.mMinDeferredOverlayPriority = num;
				if (num == 2147483647)
				{
					this.mDeferredOverlayWidgets.Clear();
					return;
				}
			}
		}

		public bool DrawScreen()
		{
			ModalFlags modalFlags = new ModalFlags();
			this.InitModalFlags(modalFlags);
			bool result = false;
			this.mMinDeferredOverlayPriority = int.MaxValue;
			this.mDeferredOverlayWidgets.Clear();
			Graphics theGraphics = new Graphics(this.mImage);
			this.mCurG = theGraphics;
			DeviceImage deviceImage = null;
			bool flag = false;
			if (this.mImage != null)
			{
				deviceImage = this.mImage.AsDeviceImage();
				if (deviceImage != null)
				{
					flag = deviceImage.LockSurface();
				}
			}
			Graphics graphics = new Graphics(theGraphics);
			graphics.Translate(-this.mMouseDestRect.mX, -this.mMouseDestRect.mY);
			bool flag2 = this.mApp.Is3DAccelerated();
			foreach (Widget widget in this.mWidgets)
			{
				if (widget == this.mWidgetManager.mBaseModalWidget)
				{
					modalFlags.mIsOver = true;
				}
				if (widget.mVisible)
				{
					graphics.PushState();
					graphics.SetFastStretch(!flag2);
					graphics.SetLinearBlend(flag2);
					graphics.Translate(widget.mX, widget.mY);
					widget.DrawAll(modalFlags, graphics);
					result = true;
					widget.mDirty = false;
					graphics.PopState();
				}
			}
			this.FlushDeferredOverlayWidgets(int.MaxValue);
			if (deviceImage != null && flag)
			{
				deviceImage.UnlockSurface();
			}
			this.mCurG = null;
			return result;
		}

		public bool UpdateFrame()
		{
			ModalFlags modalFlags = new ModalFlags();
			this.InitModalFlags(modalFlags);
			this.mUpdateCnt++;
			this.mLastWMUpdateCount = this.mUpdateCnt;
			this.UpdateAll(modalFlags);
			return this.mDirty;
		}

		public bool UpdateFrameF(float theFrac)
		{
			ModalFlags modalFlags = new ModalFlags();
			this.InitModalFlags(modalFlags);
			this.UpdateFAll(modalFlags, theFrac);
			return this.mDirty;
		}

		public void SetPopupCommandWidget(Widget theList)
		{
			this.mPopupCommandWidget = theList;
			this.AddWidget(this.mPopupCommandWidget);
		}

		public void RemovePopupCommandWidget()
		{
			if (this.mPopupCommandWidget != null)
			{
				Widget theWidget = this.mPopupCommandWidget;
				this.mPopupCommandWidget = null;
				this.RemoveWidget(theWidget);
			}
		}

		public void MousePosition(int x, int y)
		{
			int num = this.mLastMouseX;
			int num2 = this.mLastMouseY;
			this.mLastMouseX = x;
			this.mLastMouseY = y;
			if (this.mLastMouseX == -1 && this.mLastMouseY == -1)
			{
				return;
			}
			int x2 = 0;
			int y2 = 0;
			Widget widgetAt = this.GetWidgetAt(x, y, ref x2, ref y2);
			if (widgetAt != this.mOverWidget)
			{
				Widget widget = this.mOverWidget;
				this.mOverWidget = null;
				if (widget != null)
				{
					this.MouseLeave(widget);
				}
				this.mOverWidget = widgetAt;
				if (widgetAt != null)
				{
					this.MouseEnter(widgetAt);
					widgetAt.MouseMove(x2, y2);
					return;
				}
			}
			else if ((num != x || num2 != y) && widgetAt != null)
			{
				widgetAt.MouseMove(x2, y2);
			}
		}

		public void RehupMouse()
		{
			if (this.mLastDownWidget != null)
			{
				if (this.mOverWidget != null)
				{
					int num = 0;
					int num2 = 0;
					Widget widgetAt = this.GetWidgetAt(this.mLastMouseX, this.mLastMouseY, ref num, ref num2);
					if (widgetAt != this.mLastDownWidget)
					{
						Widget theWidget = this.mOverWidget;
						this.mOverWidget = null;
						this.MouseLeave(theWidget);
						return;
					}
				}
			}
			else if (this.mMouseIn)
			{
				this.MousePosition(this.mLastMouseX, this.mLastMouseY);
			}
		}

		public void RemapMouse(ref int theX, ref int theY)
		{
			if (this.mMouseSourceRect.mWidth == 0 || this.mMouseSourceRect.mHeight == 0)
			{
				return;
			}
			theX = (theX - this.mMouseSourceRect.mX) * this.mMouseDestRect.mWidth / this.mMouseSourceRect.mWidth + this.mMouseDestRect.mX;
			theY = (theY - this.mMouseSourceRect.mY) * this.mMouseDestRect.mHeight / this.mMouseSourceRect.mHeight + this.mMouseDestRect.mY;
		}

		public bool MouseUp(int x, int y, int theClickCount)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			int num;
			if (theClickCount < 0)
			{
				num = 2;
			}
			else if (theClickCount == 3)
			{
				num = 4;
			}
			else
			{
				num = 1;
			}
			this.mActualDownButtons &= ~num;
			if (this.mLastDownWidget != null && (this.mDownButtons & num) != 0)
			{
				Widget widget = this.mLastDownWidget;
				this.mDownButtons &= ~num;
				if (this.mDownButtons == 0)
				{
					this.mLastDownWidget = null;
				}
				widget.mIsDown = false;
				SexyPoint absPos = widget.GetAbsPos();
				widget.MouseUp(x - absPos.mX, y - absPos.mY, theClickCount);
			}
			else
			{
				this.mDownButtons &= ~num;
			}
			this.MousePosition(x, y);
			return true;
		}

		public bool MouseDown(int x, int y, int theClickCount)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			if (theClickCount < 0)
			{
				this.mActualDownButtons |= 2;
			}
			else if (theClickCount == 3)
			{
				this.mActualDownButtons |= 4;
			}
			else
			{
				this.mActualDownButtons |= 1;
			}
			this.MousePosition(x, y);
			if (this.mPopupCommandWidget != null && this.mPopupCommandWidget.Contains(x, y))
			{
				this.RemovePopupCommandWidget();
			}
			int x2 = 0;
			int y2 = 0;
			Widget widgetAt = this.GetWidgetAt(x, y, ref x2, ref y2);
			if (this.mLastDownWidget != null)
			{
				widgetAt = this.mLastDownWidget;
			}
			if (theClickCount < 0)
			{
				this.mLastDownButtonId = -1;
				this.mDownButtons |= 2;
			}
			else if (theClickCount == 3)
			{
				this.mLastDownButtonId = 2;
				this.mDownButtons |= 4;
			}
			else
			{
				this.mLastDownButtonId = 1;
				this.mDownButtons |= 1;
			}
			this.mLastDownWidget = widgetAt;
			if (widgetAt != null)
			{
				if (widgetAt.WantsFocus())
				{
					this.SetFocus(widgetAt);
				}
				widgetAt.mIsDown = true;
				widgetAt.MouseDown(x2, y2, theClickCount);
			}
			return true;
		}

		public bool MouseMove(int x, int y)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			if (this.mDownButtons != 0)
			{
				return this.MouseDrag(x, y);
			}
			this.mMouseIn = true;
			this.MousePosition(x, y);
			return true;
		}

		public bool MouseDrag(int x, int y)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			this.mMouseIn = true;
			this.mLastMouseX = x;
			this.mLastMouseY = y;
			if (this.mOverWidget != null && this.mOverWidget != this.mLastDownWidget)
			{
				Widget theWidget = this.mOverWidget;
				this.mOverWidget = null;
				this.MouseLeave(theWidget);
			}
			if (this.mLastDownWidget != null)
			{
				SexyPoint absPos = this.mLastDownWidget.GetAbsPos();
				int x2 = x - absPos.mX;
				int y2 = y - absPos.mY;
				this.mLastDownWidget.MouseDrag(x2, y2);
				int num = 0;
				int num2 = 0;
				Widget widgetAt = this.GetWidgetAt(x, y, ref num, ref num2);
				if (widgetAt == this.mLastDownWidget && widgetAt != null)
				{
					if (this.mOverWidget == null)
					{
						this.mOverWidget = this.mLastDownWidget;
						this.MouseEnter(this.mOverWidget);
					}
				}
				else if (this.mOverWidget != null)
				{
					Widget theWidget2 = this.mOverWidget;
					this.mOverWidget = null;
					this.MouseLeave(theWidget2);
				}
			}
			return true;
		}

		public bool MouseExit(int x, int y)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			this.mMouseIn = false;
			if (this.mOverWidget != null)
			{
				this.MouseLeave(this.mOverWidget);
				this.mOverWidget = null;
			}
			return true;
		}

		public void MouseWheel(int theDelta)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			if (this.mFocusWidget != null)
			{
				this.mFocusWidget.MouseWheel(theDelta);
			}
		}

		public int KeyChar(sbyte theChar)
		{
			return 0;
		}

		public bool KeyDown(KeyCode key)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			if (key >= KeyCode.KEYCODE_UNKNOWN && key < (KeyCode)255)
			{
				this.mKeyDown[(int)key] = true;
			}
			if (this.mFocusWidget != null)
			{
				this.mFocusWidget.KeyDown(key);
			}
			return true;
		}

		public bool KeyUp(KeyCode key)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			if (key >= KeyCode.KEYCODE_UNKNOWN && key < (KeyCode)255)
			{
				this.mKeyDown[(int)key] = false;
			}
			if (key == KeyCode.KEYCODE_TAB && this.mKeyDown[17])
			{
				return true;
			}
			if (this.mFocusWidget != null)
			{
				this.mFocusWidget.KeyUp(key);
			}
			return true;
		}

		public bool IsLeftButtonDown()
		{
			return false;
		}

		public bool IsMiddleButtonDown()
		{
			return false;
		}

		public bool IsRightButtonDown()
		{
			return false;
		}

		public void TouchBegan(SexyAppBase.Touch touch)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			this.mActualDownButtons |= 1;
			this.MousePosition(touch.location.mX, touch.location.mY);
			int num = 0;
			int num2 = 0;
			Widget widgetAt = this.GetWidgetAt(touch.location.mX, touch.location.mY, ref num, ref num2);
			if (this.mLastDownWidget != null)
			{
				widgetAt = this.mLastDownWidget;
			}
			if (widgetAt != null)
			{
				SexyPoint absPos = widgetAt.GetAbsPos();
				touch.location.mX -= absPos.mX;
				touch.location.mY -= absPos.mY;
				touch.previousLocation.mX -= absPos.mX;
				touch.previousLocation.mY -= absPos.mY;
			}
			this.mLastDownButtonId = 1;
			this.mDownButtons |= 1;
			this.mLastDownWidget = widgetAt;
			if (widgetAt != null)
			{
				if (widgetAt.WantsFocus())
				{
					this.SetFocus(widgetAt);
				}
				widgetAt.mIsDown = true;
				widgetAt.TouchBegan(touch);
			}
		}

		public void TouchMoved(SexyAppBase.Touch touch)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			this.mMouseIn = true;
			this.mLastMouseX = touch.location.mX;
			this.mLastMouseY = touch.location.mY;
			if (this.mLastDownWidget != null)
			{
				int num = 0;
				int num2 = 0;
				Widget widgetAt = this.GetWidgetAt(touch.location.mX, touch.location.mY, ref num, ref num2);
				SexyPoint absPos = this.mLastDownWidget.GetAbsPos();
				touch.location.mX -= absPos.mX;
				touch.location.mY -= absPos.mY;
				touch.previousLocation.mX -= absPos.mX;
				touch.previousLocation.mY -= absPos.mY;
				this.mLastDownWidget.TouchMoved(touch);
				if (widgetAt == this.mLastDownWidget && widgetAt != null)
				{
					if (this.mOverWidget == null)
					{
						this.mOverWidget = this.mLastDownWidget;
						this.MouseEnter(this.mOverWidget);
						return;
					}
				}
				else if (this.mOverWidget != null)
				{
					Widget theWidget = this.mOverWidget;
					this.mOverWidget = null;
					this.MouseLeave(theWidget);
				}
			}
		}

		public void TouchEnded(SexyAppBase.Touch touch)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			int num = 1;
			this.mActualDownButtons &= ~num;
			if (this.mLastDownWidget != null && (this.mDownButtons & num) != 0)
			{
				Widget widget = this.mLastDownWidget;
				this.mDownButtons &= ~num;
				if (this.mDownButtons == 0)
				{
					this.mLastDownWidget = null;
				}
				SexyPoint absPos = widget.GetAbsPos();
				touch.location.mX -= absPos.mX;
				touch.location.mY -= absPos.mY;
				touch.previousLocation.mX -= absPos.mX;
				touch.previousLocation.mY -= absPos.mY;
				widget.mIsDown = false;
				widget.TouchEnded(touch);
			}
			else
			{
				this.mDownButtons &= ~num;
			}
			this.MousePosition((int)GlobalMembers.NO_TOUCH_MOUSE_POS.X, (int)GlobalMembers.NO_TOUCH_MOUSE_POS.Y);
		}

		public void TouchesCanceled()
		{
		}

		public Widget GetGamepadSelection()
		{
			return this.mGamepadSelectionWidget;
		}

		public void SetGamepadSelection(Widget theSelectedWidget, WidgetLinkDir theDirection)
		{
			Widget widget = this.mGamepadSelectionWidget;
		}

		public void GamepadButtonDown(GamepadButton theButton, int thePlayer, uint theFlags)
		{
		}

		public void GamepadButtonUp(GamepadButton theButton, int thePlayer, uint theFlags)
		{
		}

		public void GamepadAxisMove(GamepadAxis theAxis, int thePlayer, float theAxisValue)
		{
		}

		public IGamepad GetGamepadForPlayer(int thePlayer)
		{
			return null;
		}

		public int GetWidgetFlags()
		{
			if (!this.mHasFocus)
			{
				return FlagsMod.GetModFlags(this.mWidgetFlags, this.mLostFocusFlagsMod);
			}
			return this.mWidgetFlags;
		}

		protected void MouseEnter(Widget theWidget)
		{
			theWidget.mIsOver = true;
			theWidget.MouseEnter();
			if (theWidget.mDoFinger)
			{
				theWidget.ShowFinger(true);
			}
		}

		protected void MouseLeave(Widget theWidget)
		{
			theWidget.mIsOver = false;
			theWidget.MouseLeave();
			if (theWidget.mDoFinger)
			{
				theWidget.ShowFinger(false);
			}
		}

		protected void SetBaseModal(Widget theWidget, FlagsMod theBelowFlagsMod)
		{
			this.mBaseModalWidget = theWidget;
			this.mBelowModalFlagsMod = theBelowFlagsMod;
			if (this.mOverWidget != null && (this.mBelowModalFlagsMod.mRemoveFlags & 16) != 0 && this.IsBelow(this.mOverWidget, this.mBaseModalWidget))
			{
				Widget theWidget2 = this.mOverWidget;
				this.mOverWidget = null;
				this.MouseLeave(theWidget2);
			}
			if (this.mLastDownWidget != null && (this.mBelowModalFlagsMod.mRemoveFlags & 16) != 0 && this.IsBelow(this.mLastDownWidget, this.mBaseModalWidget))
			{
				Widget theWidget3 = this.mLastDownWidget;
				int theDownCode = this.mDownButtons;
				this.mDownButtons = 0;
				this.mLastDownWidget = null;
				this.DoMouseUps(theWidget3, theDownCode);
			}
			if (this.mFocusWidget != null && (this.mBelowModalFlagsMod.mRemoveFlags & 32) != 0 && this.IsBelow(this.mFocusWidget, this.mBaseModalWidget))
			{
				Widget widget = this.mFocusWidget;
				this.mFocusWidget = null;
				widget.LostFocus();
			}
		}

		public Widget mDefaultTab;

		public Graphics mCurG;

		public SexyAppBase mApp;

		public MemoryImage mImage;

		public MemoryImage mTransientImage;

		public bool mLastHadTransients;

		public Widget mPopupCommandWidget;

		public List<KeyValuePair<Widget, int>> mDeferredOverlayWidgets = new List<KeyValuePair<Widget, int>>();

		public int mMinDeferredOverlayPriority;

		public bool mHasFocus;

		public Widget mFocusWidget;

		public Widget mLastDownWidget;

		public Widget mOverWidget;

		public Widget mBaseModalWidget;

		public Widget mGamepadSelectionWidget;

		public FlagsMod mLostFocusFlagsMod = new FlagsMod();

		public FlagsMod mBelowModalFlagsMod = new FlagsMod();

		public FlagsMod mDefaultBelowModalFlagsMod = new FlagsMod();

		public LinkedList<PreModalInfo> mPreModalInfoList = new LinkedList<PreModalInfo>();

		public Rect mMouseDestRect = default(Rect);

		public Rect mMouseSourceRect = default(Rect);

		public bool mMouseIn;

		public int mLastMouseX;

		public int mLastMouseY;

		public int mDownButtons;

		public int mActualDownButtons;

		public int mLastInputUpdateCnt;

		public bool[] mKeyDown = new bool[255];

		public int mLastDownButtonId;

		public int mWidgetFlags;
	}
}
