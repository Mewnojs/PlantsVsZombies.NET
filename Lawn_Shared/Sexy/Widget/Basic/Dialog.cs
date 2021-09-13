using System;
using Lawn;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public/*internal*/ class Dialog : Widget, ButtonListener
	{
		public void EnsureFonts()
		{
		}

		public Dialog(Image theComponentImage, Image theButtonComponentImage, int theId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
		{
			this.mId = theId;
			this.mResult = int.MaxValue;
			this.mComponentImage = theComponentImage;
			this.mIsModal = isModal;
			int num = (int)Constants.InvertAndScale(24f);
			this.mContentInsets = new Insets(num, num, num, num);
			this.mTextAlign = 0;
			this.mLineSpacingOffset = 0;
			this.mSpaceAfterHeader = (int)Constants.InvertAndScale(10f);
			this.mButtonSidePadding = 0;
			this.mButtonHorzSpacing = (int)Constants.InvertAndScale(8f);
			this.mDialogListener = GlobalStaticVars.gSexyAppBase;
			this.mDialogHeader = theDialogHeader;
			this.mDialogFooter = theDialogFooter;
			this.mButtonMode = theButtonMode;
			if (this.mButtonMode == 1 || this.mButtonMode == 2)
			{
				this.mYesButton = new DialogButton(theButtonComponentImage, 1000, this);
				this.mNoButton = new DialogButton(theButtonComponentImage, 1001, this);
				if (this.mButtonMode == 1)
				{
					this.mYesButton.mLabel = Strings.YES;
					this.mNoButton.mLabel = Strings.NO;
				}
				else
				{
					this.mYesButton.mLabel = Strings.OK;
					this.mNoButton.mLabel = Strings.CANCEL;
				}
			}
			else if (this.mButtonMode == 3)
			{
				this.mYesButton = new DialogButton(theButtonComponentImage, 1000, this);
				this.mYesButton.mLabel = this.mDialogFooter;
				this.mNoButton = null;
			}
			else
			{
				this.mYesButton = null;
				this.mNoButton = null;
				this.mNumButtons = 0;
			}
			this.mDialogLines = theDialogLines;
			this.mButtonHeight = ((theButtonComponentImage == null) ? ((int)Constants.InvertAndScale(24f)) : theButtonComponentImage.mHeight);
			this.mHasTransparencies = true;
			this.mHasAlpha = true;
			this.mHeaderFont = Resources.FONT_BRIANNETOD16;
			this.mLinesFont = Resources.FONT_BRIANNETOD12;
			this.mDragging = false;
			this.mPriority = 1;
			if (theButtonComponentImage == null)
			{
				GlobalMembersDialog.gDialogColors[3, 0] = 0;
				GlobalMembersDialog.gDialogColors[3, 1] = 0;
				GlobalMembersDialog.gDialogColors[3, 2] = 0;
				GlobalMembersDialog.gDialogColors[4, 0] = 0;
				GlobalMembersDialog.gDialogColors[4, 1] = 0;
				GlobalMembersDialog.gDialogColors[4, 2] = 0;
			}
			this.SetColors(GlobalMembersDialog.gDialogColors, 7);
		}

		public override void Dispose()
		{
			this.mYesButton.Dispose();
			this.mNoButton.Dispose();
			this.mHeaderFont.Dispose();
			this.mLinesFont.Dispose();
			base.Dispose();
		}

		public virtual void SetButtonFont(Font theFont)
		{
			if (this.mYesButton != null)
			{
				this.mYesButton.SetFont(theFont);
			}
			if (this.mNoButton != null)
			{
				this.mNoButton.SetFont(theFont);
			}
		}

		public virtual void SetHeaderFont(Font theFont)
		{
			if (this.mHeaderFont != null)
			{
				this.mHeaderFont.Dispose();
			}
			this.mHeaderFont = theFont.Duplicate();
		}

		public virtual void SetLinesFont(Font theFont)
		{
			if (this.mLinesFont != null)
			{
				this.mLinesFont.Dispose();
			}
			this.mLinesFont = theFont.Duplicate();
		}

		public override void SetColor(int theIdx, SexyColor theColor)
		{
			base.SetColor(theIdx, theColor);
			if (theIdx == 3)
			{
				if (this.mYesButton != null)
				{
					this.mYesButton.SetColor(ButtonWidget.ColorType.COLOR_LABEL, theColor);
				}
				if (this.mNoButton != null)
				{
					this.mNoButton.SetColor(ButtonWidget.ColorType.COLOR_LABEL, theColor);
					return;
				}
			}
			else if (theIdx == 4)
			{
				if (this.mYesButton != null)
				{
					this.mYesButton.SetColor(ButtonWidget.ColorType.COLOR_LABEL_HILITE, theColor);
				}
				if (this.mNoButton != null)
				{
					this.mNoButton.SetColor(ButtonWidget.ColorType.COLOR_LABEL_HILITE, theColor);
				}
			}
		}

		public virtual int GetPreferredHeight(int theWidth)
		{
			this.EnsureFonts();
			int num = this.mContentInsets.mTop + this.mContentInsets.mBottom + this.mBackgroundInsets.mTop + this.mBackgroundInsets.mBottom;
			bool flag = false;
			if (this.mDialogHeader.Length > 0)
			{
				num += this.mHeaderFont.GetHeight() - this.mHeaderFont.GetAscentPadding();
				flag = true;
			}
			if (this.mDialogLines.Length > 0)
			{
				if (flag)
				{
					num += this.mSpaceAfterHeader;
				}
				Graphics g = GlobalStaticVars.g;
				g.SetFont(this.mLinesFont);
				num += this.GetWordWrappedHeight(g, theWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight - (int)Constants.InvertAndScale(4f), this.mDialogLines, this.mLinesFont.GetLineSpacing() + this.mLineSpacingOffset);
				flag = true;
				num += this.mSpaceAfterHeader;
			}
			if (this.mDialogFooter.Length != 0 && this.mButtonMode != 3)
			{
				if (flag)
				{
					num += (int)Constants.InvertAndScale(8f);
				}
				num += this.mHeaderFont.GetLineSpacing();
			}
			return num + AtlasResources.IMAGE_DIALOG_BOTTOMMIDDLE.GetHeight();
		}

		public override void Draw(Graphics g)
		{
			this.EnsureFonts();
			TRect theDest = new TRect(this.mBackgroundInsets.mLeft, this.mBackgroundInsets.mTop, this.mWidth - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight, this.mHeight - this.mBackgroundInsets.mTop - this.mBackgroundInsets.mBottom);
			if (this.mComponentImage != null)
			{
				g.DrawImageBox(theDest, this.mComponentImage);
			}
			else
			{
				int num = 6;
				Color theDefaultColor = new Color(GlobalMembersDialog.gDialogColors[num, 0], GlobalMembersDialog.gDialogColors[num, 1], GlobalMembersDialog.gDialogColors[num, 2]);
				g.SetColor(this.GetColor(Dialog.DialogColour.COLOR_OUTLINE, theDefaultColor));
				g.DrawRect(12, 12, this.mWidth - 24 - 1, this.mHeight - 24 - 1);
				num = 5;
				theDefaultColor = new Color(GlobalMembersDialog.gDialogColors[num, 0], GlobalMembersDialog.gDialogColors[num, 1], GlobalMembersDialog.gDialogColors[num, 2]);
				g.SetColor(this.GetColor(Dialog.DialogColour.COLOR_BKG, theDefaultColor));
				g.FillRect(13, 13, this.mWidth - 24 - 2, this.mHeight - 24 - 2);
				g.SetColor(new Color(0, 0, 0, 128));
				g.FillRect(this.mWidth - 12, 24, 12, this.mHeight - 36);
				g.FillRect(24, this.mHeight - 12, this.mWidth - 24, 12);
			}
			int num2 = this.mContentInsets.mTop + this.mBackgroundInsets.mTop;
			if (this.mDialogHeader.Length > 0)
			{
				num2 += this.mHeaderFont.GetAscent() - this.mHeaderFont.GetAscentPadding();
				g.SetFont(this.mHeaderFont);
				g.SetColor(this.mColors[0]);
				this.WriteCenteredLine(g, num2, this.mDialogHeader);
				num2 += this.mHeaderFont.GetHeight() - this.mHeaderFont.GetAscent();
				num2 += this.mSpaceAfterHeader;
			}
			g.SetFont(this.mLinesFont);
			g.SetColor(this.mColors[1]);
			TRect theRect = new TRect(this.mBackgroundInsets.mLeft + this.mContentInsets.mLeft + 2, num2, this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight - 4, 0);
			num2 += this.WriteWordWrapped(g, theRect, this.mDialogLines, this.mLinesFont.GetLineSpacing() + this.mLineSpacingOffset, this.mTextAlign);
			if (this.mDialogFooter.Length != 0 && this.mButtonMode != 3)
			{
				num2 += 8;
				num2 += this.mHeaderFont.GetLineSpacing();
				g.SetFont(this.mHeaderFont);
				g.SetColor(this.mColors[2]);
				this.WriteCenteredLine(g, num2, this.mDialogFooter);
			}
		}

		public override void AddedToManager(WidgetManager theWidgetManager)
		{
			base.AddedToManager(theWidgetManager);
			if (this.mYesButton != null)
			{
				theWidgetManager.AddWidget(this.mYesButton);
			}
			if (this.mNoButton != null)
			{
				theWidgetManager.AddWidget(this.mNoButton);
			}
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			if (this.mYesButton != null)
			{
				theWidgetManager.RemoveWidget(this.mYesButton);
			}
			if (this.mNoButton != null)
			{
				theWidgetManager.RemoveWidget(this.mNoButton);
			}
		}

		public override void OrderInManagerChanged()
		{
			base.OrderInManagerChanged();
			if (this.mYesButton != null)
			{
				this.mWidgetManager.PutInfront(this.mYesButton, this);
			}
			if (this.mNoButton != null)
			{
				this.mWidgetManager.PutInfront(this.mNoButton, this);
			}
		}

		public override void Resize(TRect frame)
		{
			base.Resize(frame);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			if (this.mYesButton != null && this.mNoButton != null)
			{
				int num = (this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight - this.mButtonSidePadding * 2 - this.mButtonHorzSpacing) / 2;
				int num2 = this.mButtonHeight;
				this.mYesButton.Resize(this.mX + this.mBackgroundInsets.mLeft + this.mContentInsets.mLeft + this.mButtonSidePadding, this.mY + this.mHeight - this.mContentInsets.mBottom - this.mBackgroundInsets.mBottom - num2, num, num2);
				this.mNoButton.Resize(this.mYesButton.mX + num + this.mButtonHorzSpacing, this.mYesButton.mY, num, num2);
				return;
			}
			if (this.mYesButton != null)
			{
				int num3 = this.mButtonHeight;
				this.mYesButton.Resize(this.mX + this.mContentInsets.mLeft + this.mBackgroundInsets.mLeft, this.mY + this.mHeight - this.mContentInsets.mBottom - this.mBackgroundInsets.mBottom - num3, this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight, num3);
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			base.MouseDown(x, y, theClickCount);
		}

		public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
			if (theClickCount == 1)
			{
				this.mDragging = true;
				this.mDragMouseX = x;
				this.mDragMouseY = y;
			}
			base.MouseDown(x, y, theBtnNum, theClickCount);
		}

		public override void MouseDrag(int x, int y)
		{
			if (this.mDragging)
			{
				int num = this.mX + x - this.mDragMouseX;
				int num2 = this.mY + y - this.mDragMouseY;
				if (num < -8)
				{
					num = -8;
				}
				else if (num + this.mWidth > this.mWidgetManager.mWidth + 8)
				{
					num = this.mWidgetManager.mWidth - this.mWidth + 8;
				}
				if (num2 < -8)
				{
					num2 = -8;
				}
				else if (num2 + this.mHeight > this.mWidgetManager.mHeight + 8)
				{
					num2 = this.mWidgetManager.mHeight - this.mHeight + 8;
				}
				this.mDragMouseX = this.mX + x - num;
				this.mDragMouseY = this.mY + y - num2;
				if (this.mDragMouseX < 8)
				{
					this.mDragMouseX = 8;
				}
				else if (this.mDragMouseX > this.mWidth - 9)
				{
					this.mDragMouseX = this.mWidth - 9;
				}
				if (this.mDragMouseY < 8)
				{
					this.mDragMouseY = 8;
				}
				else if (this.mDragMouseY > this.mHeight - 9)
				{
					this.mDragMouseY = this.mHeight - 9;
				}
				this.Move(num, num2);
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			base.MouseUp(x, y, theClickCount);
		}

		public override void MouseUp(int x, int y, int theBtnNum, int theClickCount)
		{
			if (this.mDragging)
			{
				this.mDragging = false;
			}
			base.MouseUp(x, y, theBtnNum, theClickCount);
		}

		public override void Update()
		{
			base.Update();
		}

		public virtual bool IsModal()
		{
			return this.mIsModal;
		}

		public virtual int WaitForResult()
		{
			return this.WaitForResult(true);
		}

		public virtual int WaitForResult(bool autoKill)
		{
			while (GlobalStaticVars.gSexyAppBase.UpdateApp() && this.mWidgetManager != null && this.mResult == 2147483647)
			{
			}
			if (autoKill)
			{
				GlobalStaticVars.gSexyAppBase.KillDialog(this.mId);
			}
			return this.mResult;
		}

		public virtual void ButtonPress(int theId)
		{
			if (theId == 1000 || theId == 1001)
			{
				this.mDialogListener.DialogButtonPress(this.mId, theId);
			}
		}

		public virtual void ButtonDepress(int theId)
		{
			if (theId == 1000 || theId == 1001)
			{
				this.mResult = theId;
				this.mDialogListener.DialogButtonDepress(this.mId, theId);
			}
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public DialogListener mDialogListener;

		public Image mComponentImage;

		public DialogButton mYesButton;

		public DialogButton mNoButton;

		public int mNumButtons;

		public string mDialogHeader;

		public string mDialogFooter;

		public string mDialogLines;

		public int mButtonMode;

		public Font mHeaderFont;

		public Font mLinesFont;

		public int mTextAlign;

		public int mLineSpacingOffset;

		public int mButtonHeight;

		public Insets mBackgroundInsets = default(Insets);

		public Insets mContentInsets = default(Insets);

		public int mSpaceAfterHeader;

		public bool mDragging;

		public int mDragMouseX;

		public int mDragMouseY;

		public int mId;

		public bool mIsModal;

		public int mResult;

		public int mButtonHorzSpacing;

		public int mButtonSidePadding;

		public enum DialogColour
		{
			COLOR_HEADER,
			COLOR_LINES,
			COLOR_FOOTER,
			COLOR_BUTTON_TEXT,
			COLOR_BUTTON_TEXT_HILITE,
			COLOR_BKG,
			COLOR_OUTLINE,
			NUM_COLORS
		}

		public enum DialogButtons
		{
			BUTTONS_NONE,
			BUTTONS_YES_NO,
			BUTTONS_OK_CANCEL,
			BUTTONS_FOOTER
		}

		public enum DialogResult
		{
			ID_YES = 1000,
			ID_NO,
			ID_OK = 1000,
			ID_CANCEL,
			ID_FOOTER = 1000
		}
	}
}
