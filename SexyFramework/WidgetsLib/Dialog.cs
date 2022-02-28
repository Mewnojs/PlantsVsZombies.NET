using System;
using Sexy.Drivers;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class Dialog : Widget, ButtonListener
	{
		public Dialog()
		{
		}

		public Dialog(Image theComponentImage, Image theButtonComponentImage, int theId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
		{
			this.mId = theId;
			this.mResult = int.MaxValue;
			this.mComponentImage = theComponentImage;
			this.mStretchBG = false;
			this.mIsModal = isModal;
			this.mContentInsets = new Insets(24, 24, 24, 24);
			this.mTextAlign = 0;
			this.mLineSpacingOffset = 0;
			this.mSpaceAfterHeader = 10;
			this.mButtonSidePadding = 0;
			this.mButtonHorzSpacing = 8;
			this.mDialogListener = GlobalMembers.gSexyAppBase;
			this.mDialogHeader = theDialogHeader;
			this.mDialogFooter = theDialogFooter;
			this.mButtonMode = theButtonMode;
			SexyAppBase gSexyAppBase = GlobalMembers.gSexyAppBase;
			if (this.mButtonMode == 1 || this.mButtonMode == 2)
			{
				this.mYesButton = new DialogButton(theButtonComponentImage, 1000, this);
				this.AddWidget(this.mYesButton);
				this.mNoButton = new DialogButton(theButtonComponentImage, 1001, this);
				this.AddWidget(this.mNoButton);
				this.mYesButton.SetGamepadParent(this);
				this.mNoButton.SetGamepadParent(this);
				this.mYesButton.SetGamepadLinks(null, null, null, this.mNoButton);
				this.mNoButton.SetGamepadLinks(null, null, this.mYesButton, null);
				if (this.mButtonMode == 1)
				{
					this.mYesButton.mLabel = gSexyAppBase.GetString("DIALOG_BUTTON_YES", GlobalMembers.DIALOG_YES_STRING);
					this.mNoButton.mLabel = gSexyAppBase.GetString("DIALOG_BUTTON_NO", GlobalMembers.DIALOG_NO_STRING);
				}
				else
				{
					this.mYesButton.mLabel = gSexyAppBase.GetString("DIALOG_BUTTON_OK", GlobalMembers.DIALOG_OK_STRING);
					this.mNoButton.mLabel = gSexyAppBase.GetString("DIALOG_BUTTON_CANCEL", GlobalMembers.DIALOG_CANCEL_STRING);
				}
			}
			else if (this.mButtonMode == 3)
			{
				this.mYesButton = new DialogButton(theButtonComponentImage, 1000, this);
				this.mYesButton.mLabel = this.mDialogFooter;
				this.mYesButton.SetGamepadParent(this);
				this.AddWidget(this.mYesButton);
				this.mNoButton = null;
			}
			else
			{
				this.mYesButton = null;
				this.mNoButton = null;
				this.mNumButtons = 0;
			}
			this.mDialogLines = theDialogLines;
			this.mButtonHeight = ((theButtonComponentImage == null) ? 24 : theButtonComponentImage.GetCelHeight());
			this.mHasTransparencies = true;
			this.mHasAlpha = true;
			this.mHeaderFont = null;
			this.mLinesFont = null;
			this.mDragging = false;
			this.mPriority = 1;
			if (theButtonComponentImage == null)
			{
				GlobalMembers.gDialogColors[3, 0] = 0;
				GlobalMembers.gDialogColors[3, 1] = 0;
				GlobalMembers.gDialogColors[3, 2] = 0;
				GlobalMembers.gDialogColors[4, 0] = 0;
				GlobalMembers.gDialogColors[4, 1] = 0;
				GlobalMembers.gDialogColors[4, 2] = 0;
			}
			else
			{
				GlobalMembers.gDialogColors[3, 0] = 255;
				GlobalMembers.gDialogColors[3, 1] = 255;
				GlobalMembers.gDialogColors[3, 2] = 255;
				GlobalMembers.gDialogColors[4, 0] = 255;
				GlobalMembers.gDialogColors[4, 1] = 255;
				GlobalMembers.gDialogColors[4, 2] = 255;
			}
			this.SetColors3(GlobalMembers.gDialogColors, 7);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, false);
			if (this.mHeaderFont != null)
			{
				this.mHeaderFont.Dispose();
			}
			if (this.mLinesFont != null)
			{
				this.mLinesFont.Dispose();
			}
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
					this.mYesButton.SetColor(0, theColor);
				}
				if (this.mNoButton != null)
				{
					this.mNoButton.SetColor(0, theColor);
					return;
				}
			}
			else if (theIdx == 4)
			{
				if (this.mYesButton != null)
				{
					this.mYesButton.SetColor(1, theColor);
				}
				if (this.mNoButton != null)
				{
					this.mNoButton.SetColor(1, theColor);
				}
			}
		}

		public virtual int GetPreferredHeight(int theWidth)
		{
			this.EnsureFonts();
			int num = this.mContentInsets.mTop + this.mContentInsets.mBottom + this.mBackgroundInsets.mTop + this.mBackgroundInsets.mBottom;
			bool flag = false;
			if (this.mDialogHeader.Length > 0 && this.mHeaderFont != null)
			{
				num += this.mHeaderFont.GetHeight() - this.mHeaderFont.GetAscentPadding();
				flag = true;
			}
			if (this.mDialogLines.Length > 0 && this.mLinesFont != null)
			{
				if (flag)
				{
					num += this.mSpaceAfterHeader;
				}
				Graphics graphics = new Graphics();
				graphics.SetFont(this.mLinesFont);
				num += base.GetWordWrappedHeight(graphics, theWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight - 4, this.mDialogLines, this.mLinesFont.GetLineSpacing() + this.mLineSpacingOffset);
				flag = true;
			}
			if (this.mDialogFooter.Length != 0 && this.mButtonMode != 3 && this.mHeaderFont != null)
			{
				if (flag)
				{
					num += 8;
				}
				num += this.mHeaderFont.GetLineSpacing();
				flag = true;
			}
			if (this.mYesButton != null)
			{
				if (flag)
				{
					num += 8;
				}
				num += this.mButtonHeight + 8;
			}
			return num;
		}

		public override void Draw(Graphics g)
		{
			this.EnsureFonts();
			Rect rect = new Rect(this.mBackgroundInsets.mLeft, this.mBackgroundInsets.mTop, this.mWidth - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight, this.mHeight - this.mBackgroundInsets.mTop - this.mBackgroundInsets.mBottom);
			if (this.mComponentImage != null)
			{
				if (!this.mStretchBG)
				{
					g.DrawImageBox(rect, this.mComponentImage);
				}
				else
				{
					g.DrawImage(this.mComponentImage, rect, new Rect(0, 0, this.mComponentImage.mWidth, this.mComponentImage.mHeight));
				}
			}
			else
			{
				int theRed = GlobalMembers.gDialogColors[6, 0];
				int theGreen = GlobalMembers.gDialogColors[6, 1];
				int theBlue = GlobalMembers.gDialogColors[6, 1];
				g.SetColor(this.GetColor(6, new SexyColor(theRed, theGreen, theBlue)));
				g.DrawRect(12, 12, this.mWidth - 24 - 1, this.mHeight - 24 - 1);
				int theRed2 = GlobalMembers.gDialogColors[5, 0];
				int theGreen2 = GlobalMembers.gDialogColors[5, 1];
				int theBlue2 = GlobalMembers.gDialogColors[5, 1];
				g.SetColor(this.GetColor(5, new SexyColor(theRed2, theGreen2, theBlue2)));
				g.FillRect(13, 13, this.mWidth - 24 - 2, this.mHeight - 24 - 2);
				g.SetColor(0, 0, 0, 128);
				g.FillRect(this.mWidth - 12, 24, 12, this.mHeight - 36);
				g.FillRect(24, this.mHeight - 12, this.mWidth - 24, 12);
			}
			int num = this.mContentInsets.mTop + this.mBackgroundInsets.mTop;
			if (this.mDialogHeader.Length > 0)
			{
				num += this.mHeaderFont.GetAscent() - this.mHeaderFont.GetAscentPadding();
				g.SetFont(this.mHeaderFont);
				g.SetColor(this.mColors[0]);
				this.WriteCenteredLine(g, num, this.mDialogHeader);
				num += this.mHeaderFont.GetHeight() - this.mHeaderFont.GetAscent();
				num += this.mSpaceAfterHeader;
			}
			g.SetFont(this.mLinesFont);
			g.SetColor(this.mColors[1]);
			Rect theRect = new Rect(this.mBackgroundInsets.mLeft + this.mContentInsets.mLeft + 2, num, this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight - 4, 0);
			num += this.WriteWordWrapped(g, theRect, this.mDialogLines, this.mLinesFont.GetLineSpacing() + this.mLineSpacingOffset, this.mTextAlign);
			if (this.mDialogFooter.Length != 0 && this.mButtonMode != 3)
			{
				num += 8;
				num += this.mHeaderFont.GetLineSpacing();
				g.SetFont(this.mHeaderFont);
				g.SetColor(this.mColors[2]);
				this.WriteCenteredLine(g, num, this.mDialogFooter);
			}
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			if (this.mYesButton != null && this.mNoButton != null)
			{
				int num = (this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight - this.mButtonSidePadding * 2 - this.mButtonHorzSpacing) / 2;
				int num2 = this.mButtonHeight;
				this.mYesButton.Resize(this.mBackgroundInsets.mLeft + this.mContentInsets.mLeft + this.mButtonSidePadding, this.mHeight - this.mContentInsets.mBottom - this.mBackgroundInsets.mBottom - num2, num, num2);
				this.mNoButton.Resize(this.mYesButton.mX + num + this.mButtonHorzSpacing, this.mYesButton.mY, num, num2);
				return;
			}
			if (this.mYesButton != null)
			{
				int num3 = this.mButtonHeight;
				this.mYesButton.Resize(this.mContentInsets.mLeft + this.mBackgroundInsets.mLeft, this.mHeight - this.mContentInsets.mBottom - this.mBackgroundInsets.mBottom - num3, this.mWidth - this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mBackgroundInsets.mLeft - this.mBackgroundInsets.mRight, num3);
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
				this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_DRAGGING);
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

		public override void MouseUp(int x, int y)
		{
			base.MouseUp(x, y);
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			base.MouseUp(x, y, theClickCount);
		}

		public override void MouseUp(int x, int y, int theBtnNum, int theClickCount)
		{
			if (this.mDragging)
			{
				this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_POINTER);
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

		public virtual int WaitForResult(bool autoKill)
		{
			if (autoKill)
			{
				GlobalMembers.gSexyAppBase.KillDialog(this.mId);
			}
			return this.mResult;
		}

		public virtual void GameAxisMove(GamepadAxis theAxis, int theMovement, int player)
		{
		}

		public virtual void GameButtonDown(GamepadButton theButton, int player, uint flags)
		{
		}

		public virtual void GameButtonUp(GamepadButton theButton, int player, uint flags)
		{
		}

		public override void GotFocus()
		{
			base.GotFocus();
			if (this.mYesButton != null)
			{
				this.mWidgetManager.SetGamepadSelection(this.mYesButton, WidgetLinkDir.LINK_DIR_NONE);
				return;
			}
			if (this.mNoButton != null)
			{
				this.mWidgetManager.SetGamepadSelection(this.mNoButton, WidgetLinkDir.LINK_DIR_NONE);
			}
		}

		public void EnsureFonts()
		{
		}

		public virtual void ButtonPress(int theId)
		{
			this.mDialogListener.DialogButtonPress(this.mId, theId);
		}

		public virtual void ButtonDepress(int theId)
		{
			this.mResult = theId;
			this.mDialogListener.DialogButtonDepress(this.mId, theId);
		}

		public void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		public void ButtonDownTick(int theId)
		{
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

		public bool mStretchBG;

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

		public Insets mBackgroundInsets = new Insets();

		public Insets mContentInsets;

		public int mSpaceAfterHeader;

		public bool mDragging;

		public int mDragMouseX;

		public int mDragMouseY;

		public int mId;

		public bool mIsModal;

		public int mResult;

		public int mButtonHorzSpacing;

		public int mButtonSidePadding;

		public enum ButtonLabel
		{
			BUTTONS_NONE,
			BUTTONS_YES_NO,
			BUTTONS_OK_CANCEL,
			BUTTONS_FOOTER
		}

		public enum ButtonID
		{
			ID_YES = 1000,
			ID_NO,
			ID_OK = 1000,
			ID_CANCEL,
			ID_FOOTER = 1000
		}

		public enum ButtonColor
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
	}
}
