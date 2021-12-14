using System;
using System.Collections.Generic;

namespace Sexy
{
	public/*internal*/ class WidgetManager : WidgetContainer
	{
		public int GetWidgetFlags()
		{
			if (!mHasFocus)
			{
				return GlobalMembersFlags.GetModFlags(mWidgetFlags, mLostFocusFlagsMod);
			}
			return mWidgetFlags;
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
			mBaseModalWidget = theWidget;
			mBelowModalFlagsMod.CopyFrom(theBelowFlagsMod);
			if (mOverWidget != null && (mBelowModalFlagsMod.mRemoveFlags & 16) != 0 && IsBelow(mOverWidget, mBaseModalWidget))
			{
				Widget theWidget2 = mOverWidget;
				mOverWidget = null;
				MouseLeave(theWidget2);
			}
			if (mLastDownWidget != null && (mBelowModalFlagsMod.mRemoveFlags & 16) != 0 && IsBelow(mLastDownWidget, mBaseModalWidget))
			{
				Widget theWidget3 = mLastDownWidget;
				int theDownCode = mDownButtons;
				mDownButtons = 0;
				mLastDownWidget = null;
				DoMouseUps(theWidget3, theDownCode);
			}
			if (mFocusWidget != null && (mBelowModalFlagsMod.mRemoveFlags & 32) != 0 && IsBelow(mFocusWidget, mBaseModalWidget))
			{
				Widget widget = mFocusWidget;
				mFocusWidget = null;
				widget.LostFocus();
			}
		}

		public WidgetManager(SexyAppBase theApp)
		{
			mApp = theApp;
			mMinDeferredOverlayPriority = int.MaxValue;
			mWidgetManager = this;
			mMouseIn = false;
			mDefaultTab = null;
			mImage = null;
			mLastHadTransients = false;
			mFocusWidget = null;
			mLastDownWidget = null;
			mOverWidget = null;
			mBaseModalWidget = null;
			mDefaultBelowModalFlagsMod.mRemoveFlags = 48;
			mWidth = 0;
			mHeight = 0;
			mHasFocus = true;
			mUpdateCnt = 0;
			mFrameCnt = 0U;
			mLastMouseX = WidgetManager.NO_TOUCH_MOUSE_POS.mX;
			mLastMouseY = WidgetManager.NO_TOUCH_MOUSE_POS.mY;
			mLastDownButtonId = 0;
			mDownButtons = 0;
			mActualDownButtons = 0;
			mWidgetFlags = 61;
			for (int i = 0; i < 255; i++)
			{
				mKeyDown[i] = false;
			}
		}

		public override void Dispose()
		{
			FreeResources();
			base.Dispose();
		}

		public void FreeResources()
		{
		}

		public void AddBaseModal(Widget theWidget, FlagsMod theBelowFlagsMod)
		{
			PreModalInfo newPreModalInfo = PreModalInfo.GetNewPreModalInfo();
			newPreModalInfo.mBaseModalWidget = theWidget;
			newPreModalInfo.mPrevBaseModalWidget = mBaseModalWidget;
			newPreModalInfo.mPrevFocusWidget = mFocusWidget;
			newPreModalInfo.mPrevBelowModalFlagsMod.CopyFrom(mBelowModalFlagsMod);
			mPreModalInfoList.Add(newPreModalInfo);
			SetBaseModal(theWidget, theBelowFlagsMod);
		}

		public void AddBaseModal(Widget theWidget)
		{
			AddBaseModal(theWidget, mDefaultBelowModalFlagsMod);
		}

		public void RemoveBaseModal(Widget theWidget)
		{
			Debug.ASSERT(mPreModalInfoList.Count > 0);
			bool flag = true;
			while (mPreModalInfoList.Count > 0)
			{
				PreModalInfo preModalInfo = mPreModalInfoList.Last<PreModalInfo>();
				if (flag && preModalInfo.mBaseModalWidget != theWidget)
				{
					return;
				}
				bool flag2 = preModalInfo.mPrevBaseModalWidget != null || mPreModalInfoList.Count == 1;
				SetBaseModal(preModalInfo.mPrevBaseModalWidget, preModalInfo.mPrevBelowModalFlagsMod);
				if (mFocusWidget == null)
				{
					mFocusWidget = preModalInfo.mPrevFocusWidget;
					if (mFocusWidget != null)
					{
						mFocusWidget.GotFocus();
					}
				}
				mPreModalInfoList.Last<PreModalInfo>().PrepareForReuse();
				mPreModalInfoList.RemoveLast<PreModalInfo>();
				if (flag2)
				{
					return;
				}
				flag = false;
			}
		}

		public void Resize(int theWidth, int theHeight)
		{
			mWidth = theWidth;
			mHeight = theHeight;
		}

		public override void DisableWidget(Widget theWidget)
		{
			if (mOverWidget == theWidget)
			{
				Widget theWidget2 = mOverWidget;
				mOverWidget = null;
				MouseLeave(theWidget2);
			}
			if (mLastDownWidget == theWidget)
			{
				Widget theWidget3 = mLastDownWidget;
				mLastDownWidget = null;
				DoMouseUps(theWidget3, mDownButtons);
				mDownButtons = 0;
			}
			if (mFocusWidget == theWidget)
			{
				Widget widget = mFocusWidget;
				mFocusWidget = null;
				widget.LostFocus();
			}
			if (mBaseModalWidget == theWidget)
			{
				mBaseModalWidget = null;
			}
		}

		public Widget GetAnyWidgetAt(int x, int y, out int theWidgetX, out int theWidgetY)
		{
			bool flag;
			return base.GetWidgetAtHelper(x, y, GetWidgetFlags(), out flag, out theWidgetX, out theWidgetY);
		}

		public Widget GetWidgetAt(float x, float y, out int theWidgetX, out int theWidgetY)
		{
			return GetWidgetAt((int)x, (int)y, out theWidgetX, out theWidgetY);
		}

		public Widget GetWidgetAt(int x, int y, out int theWidgetX, out int theWidgetY)
		{
			Widget widget = GetAnyWidgetAt(x, y, out theWidgetX, out theWidgetY);
			if (widget != null && widget.mDisabled)
			{
				widget = null;
			}
			return widget;
		}

		public Widget GetWidgetAt(float x, float y)
		{
			return GetWidgetAt((int)x, (int)y);
		}

		public Widget GetWidgetAt(int x, int y)
		{
			int num;
			int num2;
			return GetWidgetAt(x, y, out num, out num2);
		}

		public override void SetFocus(Widget aWidget)
		{
			if (aWidget == mFocusWidget)
			{
				return;
			}
			if (mFocusWidget != null)
			{
				mFocusWidget.LostFocus();
			}
			if (aWidget != null && aWidget.mWidgetManager == this)
			{
				mFocusWidget = aWidget;
				if (mHasFocus && mFocusWidget != null)
				{
					mFocusWidget.GotFocus();
					return;
				}
			}
			else
			{
				mFocusWidget = null;
			}
		}

		public void GotFocus()
		{
			if (!mHasFocus)
			{
				mHasFocus = true;
				if (mFocusWidget != null)
				{
					mFocusWidget.GotFocus();
				}
			}
		}

		public void LostFocus()
		{
			if (mHasFocus)
			{
				mActualDownButtons = 0;
				for (int i = 0; i < 255; i++)
				{
					if (mKeyDown[i])
					{
						KeyUp((KeyCode)i);
					}
				}
				mHasFocus = false;
				if (mFocusWidget != null)
				{
					mFocusWidget.LostFocus();
				}
			}
		}

		public void InitModalFlags(ref ModalFlags theModalFlags)
		{
			theModalFlags.mIsOver = (mBaseModalWidget == null);
			theModalFlags.mOverFlags = GetWidgetFlags();
			theModalFlags.mUnderFlags = GlobalMembersFlags.GetModFlags(theModalFlags.mOverFlags, mBelowModalFlagsMod);
		}

		public void DrawWidgetsTo(Graphics g)
		{
			mCurG = g;
			ModalFlags theFlags = default(ModalFlags);
			InitModalFlags(ref theFlags);
			foreach (Widget widget in mWidgets)
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
			mCurG = null;
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
					theWidget.MouseUp(mLastMouseX - theWidget.mX, mLastMouseY - theWidget.mY, array[i]);
				}
			}
		}

		public void DoMouseUps()
		{
			if (mLastDownWidget != null && mDownButtons != 0)
			{
				DoMouseUps(mLastDownWidget, mDownButtons);
				mDownButtons = 0;
				mLastDownWidget = null;
			}
		}

		public void DeferOverlay(Widget theWidget, int thePriority)
		{
			mDeferredOverlayWidgets.Add(WidgetOverlayPair.GetNewWidgetOverlayPair(theWidget, thePriority));
			if (thePriority < mMinDeferredOverlayPriority)
			{
				mMinDeferredOverlayPriority = thePriority;
			}
		}

		public void FlushDeferredOverlayWidgets(int theMaxPriority)
		{
			for (;;)
			{
				int num = int.MaxValue;
				for (int i = 0; i < mDeferredOverlayWidgets.Count; i++)
				{
					Widget aWidget = mDeferredOverlayWidgets[i].aWidget;
					if (aWidget != null)
					{
						int aPriority = mDeferredOverlayWidgets[i].aPriority;
						if (aPriority == mMinDeferredOverlayPriority)
						{
							Graphics @new = Graphics.GetNew(mCurG);
							@new.Translate(aWidget.mX, aWidget.mY);
							@new.SetFastStretch(!@new.Is3D());
							@new.SetLinearBlend(@new.Is3D());
							aWidget.DrawOverlay(@new, aPriority);
							mDeferredOverlayWidgets[i].Clear();
							@new.PrepareForReuse();
						}
						else if (aPriority < num)
						{
							num = aPriority;
						}
					}
				}
				mMinDeferredOverlayPriority = num;
				if (num == 2147483647)
				{
					break;
				}
				if (num >= theMaxPriority)
				{
					return;
				}
			}
			for (int j = 0; j < mDeferredOverlayWidgets.Count; j++)
			{
				mDeferredOverlayWidgets[j].PrepareForReuse();
			}
			mDeferredOverlayWidgets.Clear();
		}

		public bool DrawScreen()
		{
			ModalFlags theFlags = default(ModalFlags);
			InitModalFlags(ref theFlags);
			bool result = true;
			mMinDeferredOverlayPriority = int.MaxValue;
			Graphics @new = Graphics.GetNew();
			mCurG = @new;
			Graphics new2 = Graphics.GetNew(@new);
			new2.Reset();
			bool flag = mApp.Is3DAccelerated();
			for (LinkedListNode<Widget> linkedListNode = mWidgets.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value == mWidgetManager.mBaseModalWidget)
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
			FlushDeferredOverlayWidgets(int.MaxValue);
			mCurG = null;
			@new.PrepareForReuse();
			new2.PrepareForReuse();
			return result;
		}

		public override bool BackButtonPress()
		{
			for (LinkedListNode<Widget> linkedListNode = mWidgets.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
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
			InitModalFlags(ref theFlags);
			mUpdateCnt++;
			mLastWMUpdateCount = mUpdateCnt;
			UpdateAll(theFlags);
			return true;
		}

		public bool UpdateFrameF(float theFrac)
		{
			ModalFlags theFlags = default(ModalFlags);
			InitModalFlags(ref theFlags);
			base.UpdateFAll(theFlags, theFrac);
			return true;
		}

		public void SetPopupCommandWidget(Widget theList)
		{
			mPopupCommandWidget = theList;
			AddWidget(mPopupCommandWidget);
		}

		public void RemovePopupCommandWidget()
		{
			if (mPopupCommandWidget != null)
			{
				Widget theWidget = mPopupCommandWidget;
				mPopupCommandWidget = null;
				RemoveWidget(theWidget);
			}
		}

		public void MousePosition(float x, float y)
		{
			MousePosition((int)x, (int)y);
		}

		public void MousePosition(int x, int y)
		{
			int num = mLastMouseX;
			int num2 = mLastMouseY;
			mLastMouseX = x;
			mLastMouseY = y;
			int x2;
			int y2;
			Widget widgetAt = GetWidgetAt(x, y, out x2, out y2);
			if (widgetAt != mOverWidget)
			{
				Widget widget = mOverWidget;
				mOverWidget = null;
				if (widget != null)
				{
					MouseLeave(widget);
				}
				mOverWidget = widgetAt;
				if (widgetAt != null)
				{
					MouseEnter(widgetAt);
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
			if (mLastDownWidget != null)
			{
				if (mOverWidget != null)
				{
					Widget widgetAt = GetWidgetAt(mLastMouseX, mLastMouseY);
					if (widgetAt != mLastDownWidget)
					{
						Widget theWidget = mOverWidget;
						mOverWidget = null;
						MouseLeave(theWidget);
						return;
					}
				}
			}
			else if (mMouseIn)
			{
				MousePosition(mLastMouseX, mLastMouseY);
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
			mLastInputUpdateCnt = mUpdateCnt;
			mActualDownButtons |= 1;
			MousePosition(touch.location.X, touch.location.Y);
			Widget widgetAt = GetWidgetAt(touch.location.X, touch.location.Y);
			if (mLastDownWidget != null)
			{
				widgetAt = mLastDownWidget;
			}
			if (widgetAt != null)
			{
				TPoint tpoint = widgetAt.GetAbsPos();
				CGMaths.CGPointTranslate(ref touch.location, -tpoint.mX, -tpoint.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, -tpoint.mX, -tpoint.mY);
			}
			mLastDownButtonId = 1;
			mDownButtons |= 1;
			mLastDownWidget = widgetAt;
			if (widgetAt != null)
			{
				if (widgetAt.WantsFocus())
				{
					SetFocus(widgetAt);
				}
				widgetAt.mIsDown = true;
				widgetAt.TouchBegan(touch);
			}
		}

		public void TouchMoved(_Touch touch)
		{
			mLastInputUpdateCnt = mUpdateCnt;
			mMouseIn = true;
			mLastMouseX = (int)touch.location.X;
			mLastMouseY = (int)touch.location.Y;
			if (mLastDownWidget != null)
			{
				Widget widgetAt = GetWidgetAt(touch.location.X, touch.location.Y);
				TPoint tpoint = mLastDownWidget.GetAbsPos();
				CGMaths.CGPointTranslate(ref touch.location, -tpoint.mX, -tpoint.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, -tpoint.mX, -tpoint.mY);
				mLastDownWidget.TouchMoved(touch);
				if (widgetAt == mLastDownWidget && widgetAt != null)
				{
					if (mOverWidget == null)
					{
						mOverWidget = mLastDownWidget;
						MouseEnter(mOverWidget);
						return;
					}
				}
				else if (mOverWidget != null)
				{
					Widget theWidget = mOverWidget;
					mOverWidget = null;
					MouseLeave(theWidget);
				}
			}
		}

		public void TouchEnded(_Touch touch)
		{
			mLastInputUpdateCnt = mUpdateCnt;
			int num = 1;
			mActualDownButtons &= ~num;
			if (mLastDownWidget != null && (mDownButtons & num) != 0)
			{
				Widget widget = mLastDownWidget;
				mDownButtons &= ~num;
				if (mDownButtons == 0)
				{
					mLastDownWidget = null;
				}
				TPoint tpoint = widget.GetAbsPos();
				CGMaths.CGPointTranslate(ref touch.location, -tpoint.mX, -tpoint.mY);
				CGMaths.CGPointTranslate(ref touch.previousLocation, -tpoint.mX, -tpoint.mY);
				widget.mIsDown = false;
				widget.TouchEnded(touch);
			}
			else
			{
				mDownButtons &= ~num;
			}
			MousePosition(WidgetManager.NO_TOUCH_MOUSE_POS.mX, WidgetManager.NO_TOUCH_MOUSE_POS.mY);
		}

		public void TouchesCanceled()
		{
		}

		public bool KeyChar(SexyChar theChar)
		{
			mLastInputUpdateCnt = mUpdateCnt;
			if (theChar == KeyCode.KEYCODE_TAB && mKeyDown[17])
			{
				if (mDefaultTab != null)
				{
					mDefaultTab.KeyChar(theChar);
				}
				return true;
			}
			if (mFocusWidget != null)
			{
				mFocusWidget.KeyChar(theChar);
			}
			return true;
		}

		public bool KeyDown(KeyCode key)
		{
			mLastInputUpdateCnt = mUpdateCnt;
			if (key >= KeyCode.KEYCODE_UNKNOWN && key < (KeyCode)255)
			{
				mKeyDown[(int)key] = true;
			}
			if (mFocusWidget != null)
			{
				mFocusWidget.KeyDown(key);
			}
			return true;
		}

		public bool KeyUp(KeyCode key)
		{
			mLastInputUpdateCnt = mUpdateCnt;
			if (key >= KeyCode.KEYCODE_UNKNOWN && key < (KeyCode)255)
			{
				mKeyDown[(int)key] = false;
			}
			if (key == KeyCode.KEYCODE_TAB && mKeyDown[17])
			{
				return true;
			}
			if (mFocusWidget != null)
			{
				mFocusWidget.KeyUp(key);
			}
			return true;
		}

		public bool IsLeftButtonDown()
		{
			return (mActualDownButtons & 1) != 0;
		}

		public bool IsMiddleButtonDown()
		{
			return (mActualDownButtons & 4) != 0;
		}

		public bool IsRightButtonDown()
		{
			return (mActualDownButtons & 2) != 0;
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
