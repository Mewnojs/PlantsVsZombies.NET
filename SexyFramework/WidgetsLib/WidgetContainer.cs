using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class WidgetContainer
	{
		public WidgetContainer()
		{
			this.mX = 0;
			this.mY = 0;
			this.mWidth = 0;
			this.mHeight = 0;
			this.mParent = null;
			this.mWidgetManager = null;
			this.mUpdateIteratorModified = false;
			this.mLastWMUpdateCount = 0;
			this.mUpdateCnt = 0;
			this.mDirty = false;
			this.mHasAlpha = false;
			this.mClip = true;
			this.mPriority = 0;
			this.mZOrder = 0;
			this.mUpdateIterator = null;
		}

		public virtual void Dispose()
		{
		}

		public void CopyFrom(WidgetContainer rhs)
		{
			if (rhs == null)
			{
				return;
			}
			this.mWidgetManager = rhs.mWidgetManager;
			this.mParent = rhs.mParent;
			this.mUpdateIteratorModified = rhs.mUpdateIteratorModified;
			this.mLastWMUpdateCount = rhs.mLastWMUpdateCount;
			this.mUpdateCnt = rhs.mUpdateCnt;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mWidth = rhs.mWidth;
			this.mHeight = rhs.mHeight;
			this.mPriority = rhs.mPriority;
			this.mZOrder = rhs.mZOrder;
			this.mDirty = rhs.mDirty;
			this.mHasAlpha = rhs.mHasAlpha;
			this.mClip = rhs.mClip;
			this.mUpdateIterator = rhs.mUpdateIterator;
			this.mWidgetFlagsMod.mAddFlags = rhs.mWidgetFlagsMod.mAddFlags;
			this.mWidgetFlagsMod.mRemoveFlags = rhs.mWidgetFlagsMod.mRemoveFlags;
			this.mRect.SetValue(rhs.mRect.mX, rhs.mRect.mY, rhs.mRect.mWidth, rhs.mRect.mWidth);
			this.mHelperRect.SetValue(rhs.mHelperRect.mX, rhs.mHelperRect.mY, rhs.mHelperRect.mWidth, rhs.mHelperRect.mWidth);
			this.mWidgets.Clear();
			foreach (Widget widget in rhs.mWidgets)
			{
				this.mWidgets.AddLast(widget);
			}
		}

		public virtual Rect GetRect()
		{
			this.mRect.mX = this.mX;
			this.mRect.mY = this.mY;
			this.mRect.mWidth = this.mWidth;
			this.mRect.mHeight = this.mHeight;
			return this.mRect;
		}

		public virtual bool Intersects(WidgetContainer theWidget)
		{
			return this.GetRect().Intersects(theWidget.GetRect());
		}

		public virtual void AddWidget(Widget theWidget)
		{
			if (!this.mWidgets.Contains(theWidget))
			{
				this.InsertWidgetHelper(this.mWidgets.Last, theWidget);
				theWidget.mWidgetManager = this.mWidgetManager;
				theWidget.mParent = this;
				if (this.mWidgetManager != null)
				{
					theWidget.AddedToManager(this.mWidgetManager);
					theWidget.MarkDirtyFull();
					this.mWidgetManager.RehupMouse();
				}
				this.MarkDirty();
			}
		}

		public virtual void RemoveWidget(Widget theWidget)
		{
			if (this.mWidgets.Contains(theWidget))
			{
				LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
				theWidget.WidgetRemovedHelper();
				theWidget.mParent = null;
				if (linkedListNode == this.mUpdateIterator)
				{
					this.mUpdateIterator = linkedListNode.Next;
					this.mUpdateIteratorModified = true;
				}
				this.mWidgets.Remove(linkedListNode);
			}
		}

		public virtual bool HasWidget(Widget theWidget)
		{
			return this.mWidgets.Contains(theWidget);
		}

		public virtual void DisableWidget(Widget theWidget)
		{
		}

		public virtual void RemoveAllWidgets(bool doDelete, bool recursive)
		{
			while (this.mWidgets.Count > 0)
			{
				Widget value = this.mWidgets.First.Value;
				this.RemoveWidget(value);
				if (recursive)
				{
					value.RemoveAllWidgets(doDelete, recursive);
				}
				if (doDelete && value != null)
				{
					value.Dispose();
				}
			}
		}

		public virtual void SetFocus(Widget theWidget)
		{
		}

		public virtual bool IsBelow(Widget theWidget1, Widget theWidget2)
		{
			bool flag = false;
			return this.IsBelowHelper(theWidget1, theWidget2, ref flag);
		}

		public virtual void MarkAllDirty()
		{
			this.MarkDirty();
			foreach (Widget widget in this.mWidgets)
			{
				widget.mDirty = true;
				widget.MarkAllDirty();
			}
		}

		public virtual void BringToFront(Widget theWidget)
		{
			if (this.mWidgets.Contains(theWidget))
			{
				LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
				if (linkedListNode == this.mUpdateIterator)
				{
					this.mUpdateIterator = this.mUpdateIterator.Next;
					this.mUpdateIteratorModified = true;
				}
				this.mWidgets.Remove(linkedListNode);
				this.InsertWidgetHelper(null, theWidget);
				theWidget.OrderInManagerChanged();
			}
		}

		public virtual void BringToBack(Widget theWidget)
		{
			if (this.mWidgets.Contains(theWidget))
			{
				LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
				if (linkedListNode == this.mUpdateIterator)
				{
					this.mUpdateIterator = this.mUpdateIterator.Next;
					this.mUpdateIteratorModified = true;
				}
				this.mWidgets.Remove(linkedListNode);
				this.InsertWidgetHelper(this.mWidgets.First, theWidget);
				theWidget.OrderInManagerChanged();
			}
		}

		public virtual void PutBehind(Widget theWidget, Widget theRefWidget)
		{
			if (theRefWidget != null)
			{
				theWidget.mZOrder = theRefWidget.mZOrder;
			}
			if (this.mWidgets.Contains(theWidget))
			{
				LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
				if (linkedListNode == this.mUpdateIterator)
				{
					this.mUpdateIterator = this.mUpdateIterator.Next;
					this.mUpdateIteratorModified = true;
				}
				this.mWidgets.Remove(linkedListNode);
				LinkedListNode<Widget> where = this.mWidgets.Find(theRefWidget);
				this.InsertWidgetHelper(where, theWidget);
				theWidget.OrderInManagerChanged();
			}
		}

		public virtual void PutInfront(Widget theWidget, Widget theRefWidget)
		{
			if (theRefWidget != null)
			{
				theWidget.mZOrder = theRefWidget.mZOrder;
			}
			if (this.mWidgets.Contains(theWidget))
			{
				LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
				if (linkedListNode == this.mUpdateIterator)
				{
					this.mUpdateIterator = this.mUpdateIterator.Next;
					this.mUpdateIteratorModified = true;
				}
				this.mWidgets.Remove(linkedListNode);
				LinkedListNode<Widget> linkedListNode2 = this.mWidgets.Find(theRefWidget);
				linkedListNode2 = linkedListNode2.Next;
				this.InsertWidgetHelper(linkedListNode2, theWidget);
				theWidget.OrderInManagerChanged();
			}
		}

		public virtual SexyPoint GetAbsPos()
		{
			if (this.mParent == null)
			{
				return new SexyPoint(this.mX, this.mY);
			}
			return new SexyPoint(this.mX + this.mParent.GetAbsPos().mX, this.mY + this.mParent.GetAbsPos().mY);
		}

		public virtual void MarkDirty()
		{
			if (this.mParent != null)
			{
				this.mParent.MarkDirty(this);
				return;
			}
			this.mDirty = true;
		}

		public virtual void MarkDirty(WidgetContainer theWidget)
		{
			if (theWidget.mDirty)
			{
				return;
			}
			this.MarkDirty();
			theWidget.mDirty = true;
			if (this.mParent != null)
			{
				return;
			}
			if (theWidget.mHasAlpha)
			{
				this.MarkDirtyFull(theWidget);
				return;
			}
			bool flag = false;
			foreach (Widget widget in this.mWidgets)
			{
				if (widget == theWidget)
				{
					flag = true;
				}
				else if (flag && widget.mVisible && widget.Intersects(theWidget))
				{
					this.MarkDirty(widget);
				}
			}
		}

		public virtual void MarkDirtyFull()
		{
			if (this.mParent != null)
			{
				this.mParent.MarkDirtyFull(this);
				return;
			}
			this.mDirty = true;
		}

		public virtual void MarkDirtyFull(WidgetContainer theWidget)
		{
			this.MarkDirtyFull();
			theWidget.mDirty = true;
			if (this.mParent != null)
			{
				return;
			}
			LinkedList<Widget>.Enumerator enumerator = this.mWidgets.GetEnumerator();
			LinkedListNode<Widget> linkedListNode = null;
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == theWidget)
				{
					linkedListNode = this.mWidgets.Find(enumerator.Current);
					break;
				}
			}
			if (linkedListNode == null)
			{
				return;
			}
			LinkedListNode<Widget> linkedListNode2 = linkedListNode;
			for (linkedListNode2 = linkedListNode2.Previous; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Previous)
			{
				Widget value = linkedListNode2.Value;
				if (value.mVisible)
				{
					if (value.mHasTransparencies && value.mHasAlpha)
					{
						this.mHelperRect.setValue(0, 0, this.mWidth, this.mHeight);
						Rect rect = theWidget.GetRect().Intersection(this.mHelperRect);
						if (value.Contains(rect.mX, rect.mY) && value.Contains(rect.mX + rect.mWidth - 1, rect.mY + rect.mHeight - 1))
						{
							value.MarkDirty();
							break;
						}
					}
					if (value.Intersects(theWidget))
					{
						this.MarkDirty(value);
					}
				}
			}
			linkedListNode2 = linkedListNode;
			while (linkedListNode2.Next != null)
			{
				Widget value2 = linkedListNode2.Value;
				if (value2.mVisible && value2.Intersects(theWidget))
				{
					this.MarkDirty(value2);
				}
				linkedListNode2 = linkedListNode2.Next;
			}
		}

		public virtual void AddedToManager(WidgetManager theWidgetManager)
		{
			foreach (Widget widget in this.mWidgets)
			{
				widget.mWidgetManager = theWidgetManager;
				widget.AddedToManager(theWidgetManager);
				this.MarkDirty();
			}
		}

		public virtual void RemovedFromManager(WidgetManager theWidgetManager)
		{
			foreach (Widget widget in this.mWidgets)
			{
				theWidgetManager.DisableWidget(widget);
				widget.RemovedFromManager(theWidgetManager);
				widget.mWidgetManager = null;
			}
			if (theWidgetManager.mPopupCommandWidget == this)
			{
				theWidgetManager.mPopupCommandWidget = null;
			}
		}

        public virtual bool BackButtonPress()
        {
            return false;
        }

        public virtual void Update()
		{
			this.mUpdateCnt++;
		}

		public virtual void UpdateAll(ModalFlags theFlags)
		{
			new AutoModalFlags(theFlags, this.mWidgetFlagsMod);
			if ((theFlags.GetFlags() & 2) != 0)
			{
				this.MarkDirty();
			}
			WidgetManager widgetManager = this.mWidgetManager;
			if (widgetManager == null)
			{
				return;
			}
			if ((theFlags.GetFlags() & 1) != 0 && this.mLastWMUpdateCount != this.mWidgetManager.mUpdateCnt)
			{
				this.mLastWMUpdateCount = this.mWidgetManager.mUpdateCnt;
				this.Update();
			}
			this.mUpdateIterator = this.mWidgets.First;
			while (this.mUpdateIterator != null)
			{
				this.mUpdateIteratorModified = false;
				Widget value = this.mUpdateIterator.Value;
				if (value == widgetManager.mBaseModalWidget)
				{
					theFlags.mIsOver = true;
				}
				value.UpdateAll(theFlags);
				if (!this.mUpdateIteratorModified)
				{
					this.mUpdateIterator = this.mUpdateIterator.Next;
				}
			}
			this.mUpdateIteratorModified = true;
		}

		public virtual void UpdateF(float theFrac)
		{
		}

		public virtual void UpdateFAll(ModalFlags theFlags, float theFrac)
		{
			new AutoModalFlags(theFlags, this.mWidgetFlagsMod);
			WidgetManager widgetManager = this.mWidgetManager;
			if (widgetManager == null)
			{
				return;
			}
			if ((theFlags.GetFlags() & 1) != 0)
			{
				this.UpdateF(theFrac);
			}
			this.mUpdateIterator = this.mWidgets.First;
			while (this.mUpdateIterator != null)
			{
				this.mUpdateIteratorModified = false;
				Widget value = this.mUpdateIterator.Value;
				if (value == widgetManager.mBaseModalWidget)
				{
					theFlags.mIsOver = true;
				}
				value.UpdateFAll(theFlags, theFrac);
				if (!this.mUpdateIteratorModified)
				{
					this.mUpdateIterator = this.mUpdateIterator.Next;
				}
			}
			this.mUpdateIteratorModified = true;
		}

		public virtual void Draw(Graphics g)
		{
		}

		public virtual void DrawAll(ModalFlags theFlags, Graphics g)
		{
			if (this.mWidgetManager != null && this.mPriority > this.mWidgetManager.mMinDeferredOverlayPriority)
			{
				this.mWidgetManager.FlushDeferredOverlayWidgets(this.mPriority);
			}
			new AutoModalFlags(theFlags, this.mWidgetFlagsMod);
			if (this.mClip && (theFlags.GetFlags() & 8) != 0)
			{
				g.ClipRect(0, 0, this.mWidth, this.mHeight);
			}
			if (this.mWidgets.Count == 0)
			{
				if ((theFlags.GetFlags() & 4) != 0)
				{
					this.Draw(g);
				}
				return;
			}
			if ((theFlags.GetFlags() & 4) != 0)
			{
				g.PushState();
				this.Draw(g);
				g.PopState();
			}
			foreach (Widget widget in this.mWidgets)
			{
				if (widget.mVisible)
				{
					if (this.mWidgetManager != null && widget == this.mWidgetManager.mBaseModalWidget)
					{
						theFlags.mIsOver = true;
					}
					g.PushState();
					g.Translate(widget.mX, widget.mY);
					widget.DrawAll(theFlags, g);
					widget.mDirty = false;
					g.PopState();
				}
			}
		}

		public virtual void SysColorChangedAll()
		{
			this.SysColorChanged();
			if (this.mWidgets.Count > 0)
			{
				WidgetContainer.aDepthCount++;
			}
			foreach (Widget widget in this.mWidgets)
			{
				widget.SysColorChangedAll();
			}
		}

		public virtual void SysColorChanged()
		{
		}

		public Widget GetWidgetAtHelper(int x, int y, int theFlags, ref bool found, ref int theWidgetX, ref int theWidgetY)
		{
			bool flag = false;
			FlagsMod.ModFlags(ref theFlags, this.mWidgetFlagsMod);
			for (LinkedListNode<Widget> linkedListNode = this.mWidgets.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
			{
				Widget value = linkedListNode.Value;
				int num = theFlags;
				FlagsMod.ModFlags(ref num, value.mWidgetFlagsMod);
				if (flag)
				{
					FlagsMod.ModFlags(ref num, this.mWidgetManager.mBelowModalFlagsMod);
				}
				if ((num & 16) != 0 && value.mVisible)
				{
					bool flag2 = false;
					Widget widgetAtHelper = value.GetWidgetAtHelper(x - value.mX, y - value.mY, num, ref flag2, ref theWidgetX, ref theWidgetY);
					if (widgetAtHelper != null || flag2)
					{
						found = true;
						return widgetAtHelper;
					}
					if (value.mMouseVisible && value.GetInsetRect().Contains(x, y))
					{
						found = true;
						if (value.IsPointVisible(x - value.mX, y - value.mY))
						{
                            //if (theWidgetX != 0)
                            //{ //修复首次按下鼠标时坐标为(0, 0)的问题
                            theWidgetX = x - value.mX;
                            //}
                            //if (theWidgetY != 0)
                            //{
                            theWidgetY = y - value.mY;
                            //}
                            return value;
                        }
					}
				}
				flag |= value == this.mWidgetManager.mBaseModalWidget;
			}
			found = false;
			return null;
		}

		public bool IsBelowHelper(Widget theWidget1, Widget theWidget2, ref bool found)
		{
			foreach (Widget widget in this.mWidgets)
			{
				if (widget == theWidget1)
				{
					found = true;
					return true;
				}
				if (widget == theWidget2)
				{
					found = true;
					return false;
				}
				bool result = widget.IsBelowHelper(theWidget1, theWidget2, ref found);
				if (found)
				{
					return result;
				}
			}
			return false;
		}

		public void InsertWidgetHelper(LinkedListNode<Widget> where, Widget theWidget)
		{
			LinkedListNode<Widget> linkedListNode;
			for (linkedListNode = where; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				Widget value = linkedListNode.Value;
				if (value.mZOrder >= theWidget.mZOrder)
				{
					if (linkedListNode != this.mWidgets.First)
					{
						value = linkedListNode.Value;
						if (value.mZOrder > theWidget.mZOrder)
						{
							break;
						}
					}
					this.mWidgets.AddAfter(linkedListNode, theWidget);
					return;
				}
			}
			if (linkedListNode == null)
			{
				linkedListNode = this.mWidgets.Last;
			}
			while (linkedListNode != null)
			{
				Widget value2 = linkedListNode.Value;
				if (value2.mZOrder <= theWidget.mZOrder)
				{
					this.mWidgets.AddAfter(linkedListNode, theWidget);
					return;
				}
				linkedListNode = linkedListNode.Previous;
			}
			this.mWidgets.AddFirst(theWidget);
		}

		public LinkedList<Widget> mWidgets = new LinkedList<Widget>();

		public WidgetManager mWidgetManager;

		public WidgetContainer mParent;

		public bool mUpdateIteratorModified;

		public LinkedListNode<Widget> mUpdateIterator;

		public int mLastWMUpdateCount;

		public int mUpdateCnt;

		public int mX;

		public int mY;

		public int mWidth;

		public int mHeight;

		public Rect mRect = default(Rect);

		public Rect mHelperRect = default(Rect);

		public int mPriority;

		public int mZOrder;

		public bool mDirty;

		public bool mHasAlpha;

		public bool mClip;

		public FlagsMod mWidgetFlagsMod = new FlagsMod();

		private static int aDepthCount;
	}
}
