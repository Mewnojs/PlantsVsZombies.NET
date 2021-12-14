using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class ListWidget : Widget, ScrollListener
	{
		public ListWidget(int theId, Font theFont, ListListener theListListener)
		{
			mJustify = 0;
			mHiliteIdx = -1;
			mSelectIdx = -1;
			if (theFont != null)
			{
				mItemHeight = theFont.GetHeight();
			}
			else
			{
				mItemHeight = -1;
			}
			SetColors(ListWidget.gInitialListWidgetColors, 6);
			mId = theId;
			mFont = theFont;
			mListListener = theListListener;
			mParent = null;
			mChild = null;
			mScrollbar = null;
			mPosition = 0.0;
			mPageSize = 0.0;
			mSortFromChild = false;
			mDrawOutline = true;
			mMaxNumericPlaces = 0;
			mDrawSelectWhenHilited = false;
			mDoFingerWhenHilited = true;
		}

		public override void Dispose()
		{
		}

		public override void RemovedFromManager(WidgetManager theManager)
		{
			base.RemovedFromManager(theManager);
			if (mListListener != null)
			{
				mListListener.ListClosed(mId);
			}
		}

		public virtual string GetSortKey(int theIdx)
		{
			string text = mLines[theIdx];
			while (text.Length < mMaxNumericPlaces)
			{
				text = "0" + text;
			}
			if (mSortFromChild)
			{
				return mChild.GetSortKey(theIdx) + text;
			}
			if (mChild == null)
			{
				return text;
			}
			return text + mChild.GetSortKey(theIdx);
		}

		public void Sort(bool ascending)
		{
			mLines.Sort();
		}

		public virtual string GetStringAt(int theIdx)
		{
			return mLines[theIdx];
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			double pageSize = 1.0;
			int num = (mItemHeight != -1) ? mItemHeight : mFont.GetHeight();
			if (mHeight > num + 8)
			{
				pageSize = (mHeight - 8.0) / num;
			}
			mPageSize = pageSize;
			if (mScrollbar != null)
			{
				mScrollbar.SetPageSize(pageSize);
			}
		}

		public virtual int AddLine(string theString, bool alphabetical)
		{
			int result = -1;
			bool flag = false;
			if (alphabetical)
			{
				for (int i = 0; i < mLines.Count; i++)
				{
					if (string.Compare(theString, mLines[i]) < 0)
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
							listWidget.mLineColors.Insert(i, mColors[2]);
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
				result = mLines.Count;
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
					listWidget2.mLineColors.Add(mColors[2]);
					listWidget2.MarkDirty();
					listWidget2 = listWidget2.mChild;
				}
			}
			if (mScrollbar != null)
			{
				mScrollbar.SetMaxValue(mLines.Count);
			}
			return result;
		}

		public virtual void SetLine(int theIdx, string theString)
		{
			mLines[theIdx] = theString;
			MarkDirty();
		}

		public virtual int GetLineCount()
		{
			return mLines.Count;
		}

		public virtual int GetLineIdx(string theLine)
		{
			for (int i = 0; i < mLines.Count; i++)
			{
				if (string.Compare(mLines[i], theLine) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		public virtual void SetColor(string theLine, SexyColor theColor)
		{
			int lineIdx = GetLineIdx(theLine);
			SetLineColor(lineIdx, theColor);
		}

		public override void SetColor(int theIdx, SexyColor theColor)
		{
			base.SetColor(theIdx, theColor);
		}

		public virtual void SetLineColor(int theIdx, SexyColor theColor)
		{
			if (theIdx >= 0 && theIdx < mLines.Count)
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
			if (mScrollbar != null)
			{
				mScrollbar.SetMaxValue(mLines.Count);
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
			if (mScrollbar != null)
			{
				mScrollbar.SetMaxValue(mLines.Count);
			}
		}

		public virtual int GetOptimalWidth()
		{
			int num = 0;
			for (int i = 0; i < mLines.Count; i++)
			{
				num = Math.Max(num, mFont.StringWidth(mLines[i]));
			}
			return num + 16;
		}

		public virtual int GetOptimalHeight()
		{
			int num = (mItemHeight != -1) ? mItemHeight : mFont.GetHeight();
			return num * mLines.Count + 8;
		}

		public override void OrderInManagerChanged()
		{
			base.OrderInManagerChanged();
			if (mChild != null)
			{
				GlobalStaticVars.gSexyAppBase.mWidgetManager.PutInfront(mChild, this);
			}
			if (mScrollbar != null)
			{
				GlobalStaticVars.gSexyAppBase.mWidgetManager.PutInfront(mScrollbar, this);
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColor(mColors[0]);
			g.FillRect(0, 0, mWidth, mHeight);
			Graphics @new = Graphics.GetNew(g);
			@new.ClipRect((int)Constants.InvertAndScale(4f), (int)Constants.InvertAndScale(4f), mWidth - (int)Constants.InvertAndScale(8f), mHeight - (int)Constants.InvertAndScale(8f));
			Graphics new2 = Graphics.GetNew(g);
			new2.ClipRect(0, (int)Constants.InvertAndScale(4f), mWidth, mHeight - (int)Constants.InvertAndScale(8f));
			@new.SetFont(mFont);
			int num = (int)mPosition;
			int num2 = Math.Min(mLines.Count - 1, (int)mPosition + (int)mPageSize + 1);
			int height;
			int num3;
			if (mItemHeight != -1)
			{
				height = mItemHeight;
				num3 = (height - mFont.GetHeight()) / 2;
			}
			else
			{
				height = mFont.GetHeight();
				num3 = 0;
			}
			for (int i = num; i <= num2; i++)
			{
				int num4 = (int)Constants.InvertAndScale(4f) + (int)((i - mPosition) * height);
				if (i == mSelectIdx || (i == mHiliteIdx && mDrawSelectWhenHilited))
				{
					new2.SetColor(mColors[4]);
					new2.FillRect(0, num4, mWidth, height);
				}
				if (i == mHiliteIdx)
				{
					@new.SetColor(mColors[3]);
				}
				else if (i == mSelectIdx && mColors.Count > 5)
				{
					@new.SetColor(mColors[5]);
				}
				else
				{
					@new.SetColor(mLineColors[i]);
				}
				string theString = mLines[i];
				int theX;
				switch (mJustify)
				{
				case 0:
					theX = (int)Constants.InvertAndScale(4f);
					break;
				case 1:
					theX = (mWidth - mFont.StringWidth(theString)) / 2;
					break;
				default:
					theX = mWidth - mFont.StringWidth(theString) - (int)Constants.InvertAndScale(4f);
					break;
				}
				@new.DrawString(theString, theX, num4 + num3 - (int)Constants.InvertAndScale(5f));
			}
			if (mDrawOutline)
			{
				g.SetColor(mColors[1]);
				g.DrawRect(0, 0, mWidth - 1, mHeight - 1);
			}
			@new.PrepareForReuse();
			new2.PrepareForReuse();
		}

		public virtual void ScrollPosition(int theId, double thePosition)
		{
			if (mChild != null)
			{
				mChild.ScrollPosition(theId, thePosition);
			}
			mPosition = thePosition;
			MarkDirty();
		}

		public override void MouseMove(int x, int y)
		{
			CGPoint absPos = GetAbsPos();
			x -= (int)absPos.x;
			y -= (int)absPos.y;
			y -= (int)Constants.InvertAndScale(4f);
			int num = (mItemHeight != -1) ? mItemHeight : mFont.GetHeight();
			int num2 = (int)(y / (double)num + mPosition);
			if (num2 < 0 || num2 >= mLines.Count)
			{
				num2 = -1;
			}
			if (num2 != mHiliteIdx)
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
			if (mScrollbar != null)
			{
				int num = 5;
				if (theDelta > 0)
				{
					mScrollbar.SetValue(mScrollbar.mValue - num);
					return;
				}
				if (theDelta < 0)
				{
					mScrollbar.SetValue(mScrollbar.mValue + num);
				}
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			base.MouseDown(x, y, theClickCount);
		}

		public override void MouseDown(int x, int y, int theBitNum, int theClickCount)
		{
			if (mHiliteIdx != -1 && mListListener != null)
			{
				mListListener.ListClicked(mId, mHiliteIdx, theClickCount);
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
			SetHilite(theHiliteIdx, false);
		}

		public void SetHilite(int theHiliteIdx, bool notifyListener)
		{
			int num = mHiliteIdx;
			mHiliteIdx = theHiliteIdx;
			if (num != mHiliteIdx && notifyListener && mListListener != null)
			{
				mListListener.ListHiliteChanged(mId, num, mHiliteIdx);
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
