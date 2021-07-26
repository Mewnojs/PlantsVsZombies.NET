using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class WidgetContainer
	{
		public Widget GetWidgetAtHelper(int x, int y, int theFlags, out bool found, out int theWidgetX, out int theWidgetY)
		{
			bool flag = false;
			GlobalMembersFlags.ModFlags(ref theFlags, this.mWidgetFlagsMod);
			for (LinkedListNode<Widget> linkedListNode = this.mWidgets.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
			{
				Widget value = linkedListNode.Value;
				int num = theFlags;
				GlobalMembersFlags.ModFlags(ref num, value.mWidgetFlagsMod);
				if (flag)
				{
					GlobalMembersFlags.ModFlags(ref num, this.mWidgetManager.mBelowModalFlagsMod);
				}
				if ((num & 16) != 0 && value.mVisible)
				{
					bool flag2;
					Widget widgetAtHelper = value.GetWidgetAtHelper(x - value.mX, y - value.mY, num, out flag2, out theWidgetX, out theWidgetY);
					if (widgetAtHelper != null || flag2)
					{
						found = true;
						return widgetAtHelper;
					}
					if (value.mMouseVisible && (value.GetInsetRect().Contains(x, y) || value.FullRect.Contains(x, y)))
					{
						found = true;
						if (value.IsPointVisible(x - value.mX, y - value.mY))
						{
							if (theWidgetX != 0)
							{
								theWidgetX = x - value.mX;
							}
							if (theWidgetY != 0)
							{
								theWidgetY = y - value.mY;
							}
							return value;
						}
					}
				}
				flag |= (value == this.mWidgetManager.mBaseModalWidget);
			}
			found = false;
			theWidgetX = (theWidgetY = 0);
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

		public void InsertWidgetHelper(LinkedList<Widget>.Enumerator where, Widget theWidget)
		{
			LinkedListNode<Widget> anItr = this.mWidgets.Find(where.Current);
			this.InsertWidgetHelper(anItr, theWidget);
		}

		public void InsertWidgetHelper(LinkedListNode<Widget> anItr, Widget theWidget)
		{
			while (anItr != null)
			{
				Widget value = anItr.Value;
				if (value.mZOrder >= theWidget.mZOrder)
				{
					if (anItr.Value != this.mWidgets.First.Value)
					{
						value = anItr.Value;
						if (value.mZOrder > theWidget.mZOrder)
						{
							IL_94:
							while (anItr != null && anItr.Value != this.mWidgets.First.Value)
							{
								anItr = anItr.Previous;
								Widget value2 = anItr.Value;
								if (value2.mZOrder <= theWidget.mZOrder)
								{
									anItr = anItr.Next;
									this.mWidgets.AddBefore(anItr, theWidget);
									return;
								}
							}
							this.mWidgets.AddLast(theWidget);
							return;
						}
					}
					this.mWidgets.AddAfter(anItr, theWidget);
					return;
				}
				anItr = anItr.Next;
			}
			goto IL_94;
		}

		public WidgetContainer()
		{
			this.mX = 0;
			this.mY = 0;
			this.mWidth = 0;
			this.mHeight = 0;
			this.mParent = null;
			this.mWidgetManager = null;
			this.mUpdateIteratorModified = false;
			this.mUpdateIterator = this.mWidgets.Last;
			this.mLastWMUpdateCount = 0;
			this.mUpdateCnt = 0;
			this.mHasAlpha = false;
			this.mClip = true;
			this.mPriority = 0;
			this.mZOrder = 0;
		}

		public virtual void Dispose()
		{
			Debug.ASSERT(this.mParent == null);
			Debug.ASSERT(this.mWidgets.Count == 0);
		}

		public virtual TRect GetRect()
		{
			return new TRect(this.mX, this.mY, this.mWidth, this.mHeight);
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
			LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
			if (linkedListNode != null)
			{
				theWidget.WidgetRemovedHelper();
				theWidget.mParent = null;
				bool flag = linkedListNode.Value == ((this.mUpdateIterator != null) ? this.mUpdateIterator.Value : null);
				LinkedListNode<Widget> next = linkedListNode.Next;
				this.mWidgets.Remove(linkedListNode);
				if (flag)
				{
					this.mUpdateIterator = next;
					this.mUpdateIteratorModified = true;
				}
			}
		}

		public virtual bool HasWidget(Widget theWidget)
		{
			return this.mWidgets.Count > 0;
		}

		public virtual void DisableWidget(Widget theWidget)
		{
		}

		public virtual void RemoveAllWidgets(bool doDelete)
		{
			this.RemoveAllWidgets(doDelete, false);
		}

		public virtual void RemoveAllWidgets()
		{
			this.RemoveAllWidgets(false, false);
		}

		public virtual void RemoveAllWidgets(bool doDelete, bool recursive)
		{
			while (this.mWidgets.Count != 0)
			{
				Widget value = this.mWidgets.First.Value;
				this.RemoveWidget(value);
				if (recursive)
				{
					value.RemoveAllWidgets(doDelete, recursive);
				}
				if (doDelete)
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
		}

		public virtual void BringToFront(Widget theWidget)
		{
			LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
			if (linkedListNode != null)
			{
				if (linkedListNode == this.mUpdateIterator)
				{
					this.mUpdateIteratorModified = true;
				}
				this.mWidgets.Remove(linkedListNode);
				this.InsertWidgetHelper(this.mWidgets.Last, theWidget);
				theWidget.OrderInManagerChanged();
			}
		}

		public virtual void BringToBack(Widget theWidget)
		{
			LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
			if (linkedListNode != null)
			{
				if (linkedListNode == this.mUpdateIterator)
				{
					this.mUpdateIteratorModified = true;
				}
				this.mWidgets.Remove(linkedListNode);
				this.InsertWidgetHelper(this.mWidgets.First, theWidget);
				theWidget.OrderInManagerChanged();
			}
		}

		public virtual void PutBehind(Widget theWidget, Widget theRefWidget)
		{
			LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
			if (linkedListNode != null)
			{
				if (linkedListNode == this.mUpdateIterator)
				{
					this.mUpdateIteratorModified = true;
				}
				this.mWidgets.Remove(linkedListNode);
				linkedListNode = this.mWidgets.Find(theRefWidget);
				this.InsertWidgetHelper(linkedListNode, theWidget);
				theWidget.OrderInManagerChanged();
			}
		}

		public virtual void PutInfront(Widget theWidget, Widget theRefWidget)
		{
			LinkedListNode<Widget> linkedListNode = this.mWidgets.Find(theWidget);
			if (linkedListNode != null)
			{
				if (linkedListNode == this.mUpdateIterator)
				{
					this.mUpdateIteratorModified = true;
				}
				this.mWidgets.Remove(linkedListNode);
				linkedListNode = this.mWidgets.Find(theRefWidget);
				linkedListNode = linkedListNode.Next;
				this.InsertWidgetHelper(linkedListNode, theWidget);
				theWidget.OrderInManagerChanged();
			}
		}

		public virtual CGPoint GetAbsPos()
		{
			if (this.mParent == null)
			{
				return new CGPoint((float)this.mX, (float)this.mY);
			}
			return new CGPoint((float)this.mX, (float)this.mY) + this.mParent.GetAbsPos();
		}

		public virtual void MarkDirty()
		{
		}

		public virtual void MarkDirtyFull()
		{
		}

		public virtual void MarkDirtyFull(WidgetContainer theWidget)
		{
		}

		public virtual void MarkDirty(WidgetContainer theWidget)
		{
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

		public void UpdateFAll(ModalFlags theFlags, float theFrac)
		{
			new AutoModalFlags(theFlags, this.mWidgetFlagsMod);
			if ((theFlags.GetFlags() & 1) != 0)
			{
				this.UpdateF(theFrac);
			}
			this.mUpdateIterator = this.mWidgets.First;
			while (this.mUpdateIterator.Value != null)
			{
				this.mUpdateIteratorModified = false;
				Widget value = this.mUpdateIterator.Value;
				if (value == this.mWidgetManager.mBaseModalWidget)
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

		public void DrawAll(ModalFlags theFlags, Graphics g)
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
			for (LinkedListNode<Widget> linkedListNode = this.mWidgets.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				if (linkedListNode.Value.mVisible)
				{
					if (this.mWidgetManager != null && linkedListNode.Value == this.mWidgetManager.mBaseModalWidget)
					{
						theFlags.mIsOver = true;
					}
					Graphics @new = Graphics.GetNew(g);
					@new.Translate(linkedListNode.Value.mX, linkedListNode.Value.mY);
					linkedListNode.Value.DrawAll(theFlags, @new);
					@new.PrepareForReuse();
				}
			}
		}

		public void SysColorChanged()
		{
		}

		public void SysColorChangedAll()
		{
			this.SysColorChanged();
			foreach (Widget widget in this.mWidgets)
			{
				widget.SysColorChangedAll();
			}
		}

		public virtual void InterfaceOrientationChanged(UI_ORIENTATION toOrientation)
		{
			foreach (Widget widget in this.mWidgets)
			{
				widget.InterfaceOrientationChanged(toOrientation);
			}
		}

		public void KeyboardWillShow(ref TRect theRect)
		{
			foreach (Widget widget in this.mWidgets)
			{
				widget.KeyboardWillShow(ref theRect);
			}
		}

		public void KeyboardDidShow(ref TRect theRect)
		{
			foreach (Widget widget in this.mWidgets)
			{
				widget.KeyboardDidShow(ref theRect);
			}
		}

		public void KeyboardWillHide(ref TRect theRect)
		{
			foreach (Widget widget in this.mWidgets)
			{
				widget.KeyboardWillHide(ref theRect);
			}
		}

		public void KeyboardDidHide(ref TRect theRect)
		{
			foreach (Widget widget in this.mWidgets)
			{
				widget.KeyboardDidHide(ref theRect);
			}
		}

		protected int S(int i)
		{
			return (int)((float)i * Constants.S);
		}

		protected int M(int i)
		{
			return i;
		}

		protected int M1(int i)
		{
			return i;
		}

		protected int M2(int i)
		{
			return i;
		}

		protected int M3(int i)
		{
			return i;
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

		public bool mHasAlpha;

		public bool mClip;

		public FlagsMod mWidgetFlagsMod = default(FlagsMod);

		public int mPriority;

		public int mZOrder;

		public TRect FullRect;
	}
}
