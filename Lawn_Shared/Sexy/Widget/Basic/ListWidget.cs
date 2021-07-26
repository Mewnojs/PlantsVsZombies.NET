using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class ListWidget : Widget, ScrollListener
	{
		public ListWidget(int theId, Font theFont, ListListener theListListener)
		{
			this.mJustify = 0;
			this.mHiliteIdx = -1;
			this.mSelectIdx = -1;
			if (theFont != null)
			{
				this.mItemHeight = theFont.GetHeight();
			}
			else
			{
				this.mItemHeight = -1;
			}
			this.SetColors(ListWidget.gInitialListWidgetColors, 6);
			this.mId = theId;
			this.mFont = theFont;
			this.mListListener = theListListener;
			this.mParent = null;
			this.mChild = null;
			this.mScrollbar = null;
			this.mPosition = 0.0;
			this.mPageSize = 0.0;
			this.mSortFromChild = false;
			this.mDrawOutline = true;
			this.mMaxNumericPlaces = 0;
			this.mDrawSelectWhenHilited = false;
			this.mDoFingerWhenHilited = true;
		}

		public override void Dispose()
		{
		}

		public override void RemovedFromManager(WidgetManager theManager)
		{
			base.RemovedFromManager(theManager);
			if (this.mListListener != null)
			{
				this.mListListener.ListClosed(this.mId);
			}
		}

		public virtual string GetSortKey(int theIdx)
		{
			string text = this.mLines[theIdx];
			while (text.Length < this.mMaxNumericPlaces)
			{
				text = "0" + text;
			}
			if (this.mSortFromChild)
			{
				return this.mChild.GetSortKey(theIdx) + text;
			}
			if (this.mChild == null)
			{
				return text;
			}
			return text + this.mChild.GetSortKey(theIdx);
		}

		public void Sort(bool ascending)
		{
			this.mLines.Sort();
		}

		public virtual string GetStringAt(int theIdx)
		{
			return this.mLines[theIdx];
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			double pageSize = 1.0;
			int num = (this.mItemHeight != -1) ? this.mItemHeight : this.mFont.GetHeight();
			if (this.mHeight > num + 8)
			{
				pageSize = ((double)this.mHeight - 8.0) / (double)num;
			}
			this.mPageSize = pageSize;
			if (this.mScrollbar != null)
			{
				this.mScrollbar.SetPageSize(pageSize);
			}
		}

		public virtual int AddLine(string theString, bool alphabetical)
		{
			int result = -1;
			bool flag = false;
			if (alphabetical)
			{
				for (int i = 0; i < this.mLines.Count; i++)
				{
					if (string.Compare(theString, this.mLines[i]) < 0)
					{
						result = i;
						ListWidget listWidget = this;
						while (listWidget.mParent != null)
						{
							listWidget = listWidget.mParent;
						}
						while (listWidget != null)
						{
							if (listWidget == this)
							{
								listWidget.mLines.Insert(i, theString);
							}
							else
							{
								listWidget.mLines.Insert(i, "-");
							}
							listWidget.mLineColors.Insert(i, this.mColors[2]);
							listWidget.MarkDirty();
							listWidget = listWidget.mChild;
						}
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				result = this.mLines.Count;
				ListWidget listWidget2 = this;
				while (listWidget2.mParent != null)
				{
					listWidget2 = listWidget2.mParent;
				}
				while (listWidget2 != null)
				{
					if (listWidget2 == this)
					{
						listWidget2.mLines.Add(theString);
					}
					else
					{
						listWidget2.mLines.Add("-");
					}
					listWidget2.mLineColors.Add(this.mColors[2]);
					listWidget2.MarkDirty();
					listWidget2 = listWidget2.mChild;
				}
			}
			if (this.mScrollbar != null)
			{
				this.mScrollbar.SetMaxValue((double)this.mLines.Count);
			}
			return result;
		}

		public virtual void SetLine(int theIdx, string theString)
		{
			this.mLines[theIdx] = theString;
			this.MarkDirty();
		}

		public virtual int GetLineCount()
		{
			return this.mLines.Count;
		}

		public virtual int GetLineIdx(string theLine)
		{
			for (int i = 0; i < this.mLines.Count; i++)
			{
				if (string.Compare(this.mLines[i], theLine) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		public virtual void SetColor(string theLine, SexyColor theColor)
		{
			int lineIdx = this.GetLineIdx(theLine);
			this.SetLineColor(lineIdx, theColor);
		}

		public override void SetColor(int theIdx, SexyColor theColor)
		{
			base.SetColor(theIdx, theColor);
		}

		public virtual void SetLineColor(int theIdx, SexyColor theColor)
		{
			if (theIdx >= 0 && theIdx < this.mLines.Count)
			{
				ListWidget listWidget = this;
				while (listWidget.mParent != null)
				{
					listWidget = listWidget.mParent;
				}
				while (listWidget != null)
				{
					listWidget.mLineColors[theIdx] = theColor;
					listWidget.MarkDirty();
					listWidget = listWidget.mChild;
				}
			}
		}

		public virtual void RemoveLine(int theIdx)
		{
			if (theIdx != -1)
			{
				ListWidget listWidget = this;
				while (listWidget.mParent != null)
				{
					listWidget = listWidget.mParent;
				}
				while (listWidget != null)
				{
					listWidget.mLines.RemoveAt(theIdx);
					listWidget.mLineColors.RemoveAt(theIdx);
					listWidget.MarkAllDirty();
					listWidget = listWidget.mChild;
				}
			}
			if (this.mScrollbar != null)
			{
				this.mScrollbar.SetMaxValue((double)this.mLines.Count);
			}
		}

		public virtual void RemoveAll()
		{
			ListWidget listWidget = this;
			while (listWidget.mParent != null)
			{
				listWidget = listWidget.mParent;
			}
			while (listWidget != null)
			{
				listWidget.mLines.Clear();
				listWidget.mLineColors.Clear();
				listWidget.mSelectIdx = -1;
				listWidget.mHiliteIdx = -1;
				listWidget.MarkDirty();
				listWidget = listWidget.mChild;
			}
			if (this.mScrollbar != null)
			{
				this.mScrollbar.SetMaxValue((double)this.mLines.Count);
			}
		}

		public virtual int GetOptimalWidth()
		{
			int num = 0;
			for (int i = 0; i < this.mLines.Count; i++)
			{
				num = Math.Max(num, this.mFont.StringWidth(this.mLines[i]));
			}
			return num + 16;
		}

		public virtual int GetOptimalHeight()
		{
			int num = (this.mItemHeight != -1) ? this.mItemHeight : this.mFont.GetHeight();
			return num * this.mLines.Count + 8;
		}

		public override void OrderInManagerChanged()
		{
			base.OrderInManagerChanged();
			if (this.mChild != null)
			{
				GlobalStaticVars.gSexyAppBase.mWidgetManager.PutInfront(this.mChild, this);
			}
			if (this.mScrollbar != null)
			{
				GlobalStaticVars.gSexyAppBase.mWidgetManager.PutInfront(this.mScrollbar, this);
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColor(this.mColors[0]);
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			Graphics @new = Graphics.GetNew(g);
			@new.ClipRect((int)Constants.InvertAndScale(4f), (int)Constants.InvertAndScale(4f), this.mWidth - (int)Constants.InvertAndScale(8f), this.mHeight - (int)Constants.InvertAndScale(8f));
			Graphics new2 = Graphics.GetNew(g);
			new2.ClipRect(0, (int)Constants.InvertAndScale(4f), this.mWidth, this.mHeight - (int)Constants.InvertAndScale(8f));
			@new.SetFont(this.mFont);
			int num = (int)this.mPosition;
			int num2 = Math.Min(this.mLines.Count - 1, (int)this.mPosition + (int)this.mPageSize + 1);
			int height;
			int num3;
			if (this.mItemHeight != -1)
			{
				height = this.mItemHeight;
				num3 = (height - this.mFont.GetHeight()) / 2;
			}
			else
			{
				height = this.mFont.GetHeight();
				num3 = 0;
			}
			for (int i = num; i <= num2; i++)
			{
				int num4 = (int)Constants.InvertAndScale(4f) + (int)(((double)i - this.mPosition) * (double)height);
				if (i == this.mSelectIdx || (i == this.mHiliteIdx && this.mDrawSelectWhenHilited))
				{
					new2.SetColor(this.mColors[4]);
					new2.FillRect(0, num4, this.mWidth, height);
				}
				if (i == this.mHiliteIdx)
				{
					@new.SetColor(this.mColors[3]);
				}
				else if (i == this.mSelectIdx && this.mColors.Count > 5)
				{
					@new.SetColor(this.mColors[5]);
				}
				else
				{
					@new.SetColor(this.mLineColors[i]);
				}
				string theString = this.mLines[i];
				int theX;
				switch (this.mJustify)
				{
				case 0:
					theX = (int)Constants.InvertAndScale(4f);
					break;
				case 1:
					theX = (this.mWidth - this.mFont.StringWidth(theString)) / 2;
					break;
				default:
					theX = this.mWidth - this.mFont.StringWidth(theString) - (int)Constants.InvertAndScale(4f);
					break;
				}
				@new.DrawString(theString, theX, num4 + num3 - (int)Constants.InvertAndScale(5f));
			}
			if (this.mDrawOutline)
			{
				g.SetColor(this.mColors[1]);
				g.DrawRect(0, 0, this.mWidth - 1, this.mHeight - 1);
			}
			@new.PrepareForReuse();
			new2.PrepareForReuse();
		}

		public virtual void ScrollPosition(int theId, double thePosition)
		{
			if (this.mChild != null)
			{
				this.mChild.ScrollPosition(theId, thePosition);
			}
			this.mPosition = thePosition;
			this.MarkDirty();
		}

		public override void MouseMove(int x, int y)
		{
			CGPoint absPos = this.GetAbsPos();
			x -= (int)absPos.x;
			y -= (int)absPos.y;
			y -= (int)Constants.InvertAndScale(4f);
			int num = (this.mItemHeight != -1) ? this.mItemHeight : this.mFont.GetHeight();
			int num2 = (int)((double)y / (double)num + this.mPosition);
			if (num2 < 0 || num2 >= this.mLines.Count)
			{
				num2 = -1;
			}
			if (num2 != this.mHiliteIdx)
			{
				ListWidget listWidget = this;
				while (listWidget.mParent != null)
				{
					listWidget = listWidget.mParent;
				}
				while (listWidget != null)
				{
					listWidget.SetHilite(num2, true);
					listWidget.MarkDirty();
					listWidget = listWidget.mChild;
				}
			}
		}

		public override void MouseWheel(int theDelta)
		{
			if (this.mScrollbar != null)
			{
				int num = 5;
				if (theDelta > 0)
				{
					this.mScrollbar.SetValue(this.mScrollbar.mValue - (double)num);
					return;
				}
				if (theDelta < 0)
				{
					this.mScrollbar.SetValue(this.mScrollbar.mValue + (double)num);
				}
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			base.MouseDown(x, y, theClickCount);
		}

		public override void MouseDown(int x, int y, int theBitNum, int theClickCount)
		{
			if (this.mHiliteIdx != -1 && this.mListListener != null)
			{
				this.mListListener.ListClicked(this.mId, this.mHiliteIdx, theClickCount);
			}
		}

		public override void MouseLeave()
		{
			ListWidget listWidget = this;
			while (listWidget.mParent != null)
			{
				listWidget = listWidget.mParent;
			}
			while (listWidget != null)
			{
				listWidget.SetHilite(-1, true);
				listWidget.MarkDirty();
				listWidget = listWidget.mChild;
			}
		}

		public virtual void SetSelect(int theSelectIdx)
		{
			ListWidget listWidget = this;
			while (listWidget.mParent != null)
			{
				listWidget = listWidget.mParent;
			}
			while (listWidget != null)
			{
				listWidget.mSelectIdx = theSelectIdx;
				listWidget.MarkDirty();
				listWidget = listWidget.mChild;
			}
		}

		public void SetHilite(int theHiliteIdx)
		{
			this.SetHilite(theHiliteIdx, false);
		}

		public void SetHilite(int theHiliteIdx, bool notifyListener)
		{
			int num = this.mHiliteIdx;
			this.mHiliteIdx = theHiliteIdx;
			if (num != this.mHiliteIdx && notifyListener && this.mListListener != null)
			{
				this.mListListener.ListHiliteChanged(this.mId, num, this.mHiliteIdx);
			}
		}

		public static int[,] gInitialListWidgetColors = new int[,]
		{
			{
				255,
				255,
				255
			},
			{
				255,
				255,
				255
			},
			{
				0,
				0,
				0
			},
			{
				0,
				192,
				0
			},
			{
				0,
				0,
				128
			},
			{
				255,
				255,
				255
			}
		};

		public int mId;

		public Font mFont;

		public ScrollbarWidget mScrollbar;

		public int mJustify;

		public List<string> mLines = new List<string>();

		public List<SexyColor> mLineColors = new List<SexyColor>();

		public double mPosition;

		public double mPageSize;

		public int mHiliteIdx;

		public int mSelectIdx;

		public ListListener mListListener;

		public new ListWidget mParent;

		public ListWidget mChild;

		public bool mSortFromChild;

		public bool mDrawOutline;

		public int mMaxNumericPlaces;

		public int mItemHeight;

		public bool mDrawSelectWhenHilited;

		public bool mDoFingerWhenHilited;

		public enum Justification
		{
			JUSTIFY_LEFT,
			JUSTIFY_CENTER,
			JUSTIFY_RIGHT
		}

		public enum ListColors
		{
			COLOR_BKG,
			COLOR_OUTLINE,
			COLOR_TEXT,
			COLOR_HILITE,
			COLOR_SELECT,
			COLOR_SELECT_TEXT
		}
	}
}
