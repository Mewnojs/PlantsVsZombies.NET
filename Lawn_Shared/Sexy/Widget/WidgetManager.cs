using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class WidgetManager : WidgetContainer
	{
		public int GetWidgetFlags()
		{
			if (!this.mHasFocus)
			{
				return GlobalMembersFlags.GetModFlags(this.mWidgetFlags, this.mLostFocusFlagsMod);
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
			this.mBelowModalFlagsMod.CopyFrom(theBelowFlagsMod);
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

		public WidgetManager(SexyAppBase theApp)
		{
			this.mApp = theApp;
			this.mMinDeferredOverlayPriority = int.MaxValue;
			this.mWidgetManager = this;
			this.mMouseIn = false;
			this.mDefaultTab = null;
			this.mImage = null;
			this.mLastHadTransients = false;
			this.mFocusWidget = null;
			this.mLastDownWidget = null;
			this.mOverWidget = null;
			this.mBaseModalWidget = null;
			this.mDefaultBelowModalFlagsMod.mRemoveFlags = 48;
			this.mWidth = 0;
			this.mHeight = 0;
			this.mHasFocus = true;
			this.mUpdateCnt = 0;
			this.mFrameCnt = 0U;
			this.mLastMouseX = WidgetManager.NO_TOUCH_MOUSE_POS.mX;
			this.mLastMouseY = WidgetManager.NO_TOUCH_MOUSE_POS.mY;
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
			PreModalInfo newPreModalInfo = PreModalInfo.GetNewPreModalInfo();
			newPreModalInfo.mBaseModalWidget = theWidget;
			newPreModalInfo.mPrevBaseModalWidget = this.mBaseModalWidget;
			newPreModalInfo.mPrevFocusWidget = this.mFocusWidget;
			newPreModalInfo.mPrevBelowModalFlagsMod.CopyFrom(this.mBelowModalFlagsMod);
			this.mPreModalInfoList.Add(newPreModalInfo);
			this.SetBaseModal(theWidget, theBelowFlagsMod);
		}

		public void AddBaseModal(Widget theWidget)
		{
			this.AddBaseModal(theWidget, this.mDefaultBelowModalFlagsMod);
		}

		public void RemoveBaseModal(Widget theWidget)
		{
			Debug.ASSERT(this.mPreModalInfoList.Count > 0);
			bool flag = true;
			while (this.mPreModalInfoList.Count > 0)
			{
				PreModalInfo preModalInfo = this.mPreModalInfoList.Last<PreModalInfo>();
				if (flag && preModalInfo.mBaseModalWidget != theWidget)
				{
					return;
				}
				bool flag2 = preModalInfo.mPrevBaseModalWidget != null || this.mPreModalInfoList.Count == 1;
				this.SetBaseModal(preModalInfo.mPrevBaseModalWidget, preModalInfo.mPrevBelowModalFlagsMod);
				if (this.mFocusWidget == null)
				{
					this.mFocusWidget = preModalInfo.mPrevFocusWidget;
					if (this.mFocusWidget != null)
					{
						this.mFocusWidget.GotFocus();
					}
				}
				this.mPreModalInfoList.Last<PreModalInfo>().PrepareForReuse();
				this.mPreModalInfoList.RemoveLast<PreModalInfo>();
				if (flag2)
				{
					return;
				}
				flag = false;
			}
		}

		public void Resize(int theWidth, int theHeight)
		{
			this.mWidth = theWidth;
			this.mHeight = theHeight;
		}

		public override void DisableWidget(Widget theWidget)
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

		public Widget GetAnyWidgetAt(int x, int y, out int theWidgetX, out int theWidgetY)
		{
			bool flag;
			return base.GetWidgetAtHelper(x, y, this.GetWidgetFlags(), out flag, out theWidgetX, out theWidgetY);
		}

		public Widget GetWidgetAt(float x, float y, out int theWidgetX, out int theWidgetY)
		{
			return this.GetWidgetAt((int)x, (int)y, out theWidgetX, out theWidgetY);
		}

		public Widget GetWidgetAt(int x, int y, out int theWidgetX, out int theWidgetY)
		{
			Widget widget = this.GetAnyWidgetAt(x, y, out theWidgetX, out theWidgetY);
			if (widget != null && widget.mDisabled)
			{
				widget = null;
			}
			return widget;
		}

		public Widget GetWidgetAt(float x, float y)
		{
			return this.GetWidgetAt((int)x, (int)y);
		}

		public Widget GetWidgetAt(int x, int y)
		{
			int num;
			int num2;
			return this.GetWidgetAt(x, y, out num, out num2);
		}

		public override void SetFocus(Widget aWidget)
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
			if (this.mHasFocus)
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

		public void InitModalFlags(ref ModalFlags theModalFlags)
		{
			theModalFlags.mIsOver = (this.mBaseModalWidget == null);
			theModalFlags.mOverFlags = this.GetWidgetFlags();
			theModalFlags.mUnderFlags = GlobalMembersFlags.GetModFlags(theModalFlags.mOverFlags, this.mBelowModalFlagsMod);
		}

		public void DrawWidgetsTo(Graphics g)
		{
			this.mCurG = g;
			ModalFlags theFlags = default(ModalFlags);
			this.InitModalFlags(ref theFlags);
			foreach (Widget widget in this.mWidgets)
			{
				if (widget.mVisible)
				{
					Graphics @new = Graphics.GetNew(g);
					@new.SetFastStretch(true);
					@new.Translate(widget.mX, widget.mY);
					widget.DrawAll(theFlags, @new);
					@new.PrepareForReuse();
				}
			}
			this.mCurG = null;
		}

		public void DoMouseUps(Widget theWidget, int theDownCode)
		{
			int[] array = new int[]
			{
				1,
				-1,
				3
			};
			for (int i = 0; i < 3; i++)
			{
				if ((theDownCode & 1 << i) != 0)
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
			this.mDeferredOverlayWidgets.Add(WidgetOverlayPair.GetNewWidgetOverlayPair(theWidget, thePriority));
			if (thePriority < this.mMinDeferredOverlayPriority)
			{
				this.mMinDeferredOverlayPriority = thePriority;
			}
		}

		public void FlushDeferredOverlayWidgets(int theMaxPriority)
		{
			for (;;)
			{
				int num = int.MaxValue;
				for (int i = 0; i < this.mDeferredOverlayWidgets.Count; i++)
				{
					Widget aWidget = this.mDeferredOverlayWidgets[i].aWidget;
					if (aWidget != null)
					{
						int aPriority = this.mDeferredOverlayWidgets[i].aPriority;
						if (aPriority == this.mMinDeferredOverlayPriority)
						{
							Graphics @new = Graphics.GetNew(this.mCurG);
							@new.Translate(aWidget.mX, aWidget.mY);
							@new.SetFastStretch(!@new.Is3D());
							@new.SetLinearBlend(@new.Is3D());
							aWidget.DrawOverlay(@new, aPriority);
							this.mDeferredOverlayWidgets[i].Clear();
							@new.PrepareForReuse();
						}
						else if (aPriority < num)
						{
							num = aPriority;
						}
					}
				}
				this.mMinDeferredOverlayPriority = num;
				if (num == 2147483647)
				{
					break;
				}
				if (num >= theMaxPriority)
				{
					return;
				}
			}
			for (int j = 0; j < this.mDeferredOverlayWidgets.Count; j++)
			{
				this.mDeferredOverlayWidgets[j].PrepareForReuse();
			}
			this.mDeferredOverlayWidgets.Clear();
		}

		public bool DrawScreen()
		{
			ModalFlags theFlags = default(ModalFlags);
			this.InitModalFlags(ref theFlags);
			bool result = true;
			this.mMinDeferredOverlayPriority = int.MaxValue;
			Graphics @new = Graphics.GetNew();
			this.mCurG = @new;
			Graphics new2 = Graphics.GetNew(@new);
			new2.Reset();
			bool flag = this.mApp.Is3DAccelerated();
			for (LinkedListNode<Widget> linkedListNode = this.mWidgets.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value == this.mWidgetManager.mBaseModalWidget)
				{
					theFlags.mIsOver = true;
				}
				if (linkedListNode.Value.mVisible)
				{
					Graphics new3 = Graphics.GetNew(new2);
					new3.SetFastStretch(!flag);
					new3.SetLinearBlend(flag);
					new3.Translate(linkedListNode.Value.mX, linkedListNode.Value.mY);
					linkedListNode.Value.DrawAll(theFlags, new3);
					result = true;
					new3.PrepareForReuse();
				}
			}
			this.FlushDeferredOverlayWidgets(int.MaxValue);
			this.mCurG = null;
			@new.PrepareForReuse();
			new2.PrepareForReuse();
			return result;
		}

		public override bool BackButtonPress()
		{
			for (LinkedListNode<Widget> linkedListNode = this.mWidgets.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
			{
				Widget value = linkedListNode.Value;
				if (!value.mDisabled && value.BackButtonPress())
				{
					return true;
				}
			}
			return false;
		}

		public bool UpdateFrame()
		{
			ModalFlags theFlags = default(ModalFlags);
			this.InitModalFlags(ref theFlags);
			this.mUpdateCnt++;
			this.mLastWMUpdateCount = this.mUpdateCnt;
			this.UpdateAll(theFlags);
			return true;
		}

		public bool UpdateFrameF(float theFrac)
		{
			ModalFlags theFlags = default(ModalFlags);
			this.InitModalFlags(ref theFlags);
			base.UpdateFAll(theFlags, theFrac);
			return true;
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

		public void MousePosition(float x, float y)
		{
			this.MousePosition((int)x, (int)y);
		}

		public void MousePosition(int x, int y)
		{
			int num = this.mLastMouseX;
			int num2 = this.mLastMouseY;
			this.mLastMouseX = x;
			this.mLastMouseY = y;
			int x2;
			int y2;
			Widget widgetAt = this.GetWidgetAt(x, y, out x2, out y2);
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
					widgetAt.MouseMove(x, y);
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
					Widget widgetAt = this.GetWidgetAt(this.mLastMouseX, this.mLastMouseY);
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

		public bool MouseUp(int x, int y, int theClickCount)
		{
			Debug.ASSERT(false);
			return true;
		}

		public bool MouseDown(int x, int y, int theClickCount)
		{
			Debug.ASSERT(false);
			return true;
		}

		public bool MouseMove(int x, int y)
		{
			Debug.ASSERT(false);
			return true;
		}

		public bool MouseDrag(int x, int y)
		{
			Debug.ASSERT(false);
			return true;
		}

		public bool MouseExit(int x, int y)
		{
			Debug.ASSERT(false);
			return true;
		}

		public void MouseWheel(int theDelta)
		{
		}

		public void TouchBegan(_Touch touch)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			this.mActualDownButtons |= 1;
			this.MousePosition(touch.location.X, touch.location.Y);
			Widget widgetAt = this.GetWidgetAt(touch.location.X, touch.location.Y);
			if (this.mLastDownWidget != null)
			{
				widgetAt = this.mLastDownWidget;
			}
			if (widgetAt != null)
			{
				TPoint tpoint = widgetAt.GetAbsPos();
				CGMaths.CGPointTranslate(ref touch.location, -tpoint.mX, -tpoint.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, -tpoint.mX, -tpoint.mY);
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

		public void TouchMoved(_Touch touch)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			this.mMouseIn = true;
			this.mLastMouseX = (int)touch.location.X;
			this.mLastMouseY = (int)touch.location.Y;
			if (this.mLastDownWidget != null)
			{
				Widget widgetAt = this.GetWidgetAt(touch.location.X, touch.location.Y);
				TPoint tpoint = this.mLastDownWidget.GetAbsPos();
				CGMaths.CGPointTranslate(ref touch.location, -tpoint.mX, -tpoint.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, -tpoint.mX, -tpoint.mY);
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

		public void TouchEnded(_Touch touch)
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
				TPoint tpoint = widget.GetAbsPos();
				CGMaths.CGPointTranslate(ref touch.location, -tpoint.mX, -tpoint.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, -tpoint.mX, -tpoint.mY);
				widget.mIsDown = false;
				widget.TouchEnded(touch);
			}
			else
			{
				this.mDownButtons &= ~num;
			}
			this.MousePosition(WidgetManager.NO_TOUCH_MOUSE_POS.mX, WidgetManager.NO_TOUCH_MOUSE_POS.mY);
		}

		public void TouchesCanceled()
		{
		}

		public bool KeyChar(SexyChar theChar)
		{
			this.mLastInputUpdateCnt = this.mUpdateCnt;
			if (theChar == KeyCode.KEYCODE_TAB && this.mKeyDown[17])
			{
				if (this.mDefaultTab != null)
				{
					this.mDefaultTab.KeyChar(theChar);
				}
				return true;
			}
			if (this.mFocusWidget != null)
			{
				this.mFocusWidget.KeyChar(theChar);
			}
			return true;
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
			return (this.mActualDownButtons & 1) != 0;
		}

		public bool IsMiddleButtonDown()
		{
			return (this.mActualDownButtons & 4) != 0;
		}

		public bool IsRightButtonDown()
		{
			return (this.mActualDownButtons & 2) != 0;
		}

		public static TPoint NO_TOUCH_MOUSE_POS = new TPoint(-1, -1);

		public Widget mDefaultTab;

		public Graphics mCurG;

		public SexyAppBase mApp;

		public MemoryImage mImage;

		public MemoryImage mTransientImage;

		public bool mLastHadTransients;

		public Widget mPopupCommandWidget;

		public List<WidgetOverlayPair> mDeferredOverlayWidgets = new List<WidgetOverlayPair>(10);

		public int mMinDeferredOverlayPriority;

		public bool mHasFocus;

		public Widget mFocusWidget;

		public Widget mLastDownWidget;

		public Widget mOverWidget;

		public Widget mBaseModalWidget;

		public FlagsMod mLostFocusFlagsMod = default(FlagsMod);

		public FlagsMod mBelowModalFlagsMod = default(FlagsMod);

		public FlagsMod mDefaultBelowModalFlagsMod = default(FlagsMod);

		public List<PreModalInfo> mPreModalInfoList = new List<PreModalInfo>();

		public bool mMouseIn;

		public int mLastMouseX;

		public int mLastMouseY;

		public int mDownButtons;

		public int mActualDownButtons;

		public int mLastInputUpdateCnt;

		public bool[] mKeyDown = new bool[255];

		public int mLastDownButtonId;

		public int mWidgetFlags;

		protected uint mFrameCnt;

		private List<WidgetOverlayPair> debugList;
	}
}
