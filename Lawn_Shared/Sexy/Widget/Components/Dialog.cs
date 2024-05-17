using System;
using Lawn;
using Microsoft.Xna.Framework;

namespace Sexy
{
    internal static class GlobalMembersDialog
    {
        internal static int[,] gDialogColors = new int[,]
        {
            {255, 255, 255},
            {255, 255, 0},
            {255, 255, 255},
            {255, 255, 255},
            {255, 255, 255},
            {80, 80, 80},
            {255, 255, 255},
        };
    }

    public/*internal*/ class Dialog : Widget, ButtonListener
    {
        public void EnsureFonts()
        {
        }

        public Dialog(Image theComponentImage, Image theButtonComponentImage, int theId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
        {
            mId = theId;
            mResult = int.MaxValue;
            mComponentImage = theComponentImage;
            mIsModal = isModal;
            int num = (int)Constants.InvertAndScale(24f);
            mContentInsets = new Insets(num, num, num, num);
            mTextAlign = 0;
            mLineSpacingOffset = 0;
            mSpaceAfterHeader = (int)Constants.InvertAndScale(10f);
            mButtonSidePadding = 0;
            mButtonHorzSpacing = (int)Constants.InvertAndScale(8f);
            mDialogListener = GlobalStaticVars.gSexyAppBase;
            mDialogHeader = theDialogHeader;
            mDialogFooter = theDialogFooter;
            mButtonMode = theButtonMode;
            if (mButtonMode == 1 || mButtonMode == 2)
            {
                mYesButton = new DialogButton(theButtonComponentImage, 1000, this);
                mNoButton = new DialogButton(theButtonComponentImage, 1001, this);
                if (mButtonMode == 1)
                {
                    mYesButton.mLabel = Strings.YES;
                    mNoButton.mLabel = Strings.NO;
                }
                else
                {
                    mYesButton.mLabel = Strings.OK;
                    mNoButton.mLabel = Strings.CANCEL;
                }
            }
            else if (mButtonMode == 3)
            {
                mYesButton = new DialogButton(theButtonComponentImage, 1000, this);
                mYesButton.mLabel = mDialogFooter;
                mNoButton = null;
            }
            else
            {
                mYesButton = null;
                mNoButton = null;
                mNumButtons = 0;
            }
            mDialogLines = theDialogLines;
            mButtonHeight = ((theButtonComponentImage == null) ? ((int)Constants.InvertAndScale(24f)) : theButtonComponentImage.mHeight);
            mHasTransparencies = true;
            mHasAlpha = true;
            mHeaderFont = Resources.FONT_BRIANNETOD16;
            mLinesFont = Resources.FONT_BRIANNETOD12;
            mDragging = false;
            mPriority = 1;
            if (theButtonComponentImage == null)
            {
                GlobalMembersDialog.gDialogColors[3, 0] = 0;
                GlobalMembersDialog.gDialogColors[3, 1] = 0;
                GlobalMembersDialog.gDialogColors[3, 2] = 0;
                GlobalMembersDialog.gDialogColors[4, 0] = 0;
                GlobalMembersDialog.gDialogColors[4, 1] = 0;
                GlobalMembersDialog.gDialogColors[4, 2] = 0;
            }
            SetColors(GlobalMembersDialog.gDialogColors, 7);
        }

        public override void Dispose()
        {
            mYesButton.Dispose();
            mNoButton.Dispose();
            mHeaderFont.Dispose();
            mLinesFont.Dispose();
            base.Dispose();
        }

        public virtual void SetButtonFont(Font theFont)
        {
            if (mYesButton != null)
            {
                mYesButton.SetFont(theFont);
            }
            if (mNoButton != null)
            {
                mNoButton.SetFont(theFont);
            }
        }

        public virtual void SetHeaderFont(Font theFont)
        {
            if (mHeaderFont != null)
            {
                mHeaderFont.Dispose();
            }
            mHeaderFont = theFont.Duplicate();
        }

        public virtual void SetLinesFont(Font theFont)
        {
            if (mLinesFont != null)
            {
                mLinesFont.Dispose();
            }
            mLinesFont = theFont.Duplicate();
        }

        public override void SetColor(int theIdx, SexyColor theColor)
        {
            base.SetColor(theIdx, theColor);
            if (theIdx == 3)
            {
                if (mYesButton != null)
                {
                    mYesButton.SetColor(ButtonWidget.ColorType.Label, theColor);
                }
                if (mNoButton != null)
                {
                    mNoButton.SetColor(ButtonWidget.ColorType.Label, theColor);
                    return;
                }
            }
            else if (theIdx == 4)
            {
                if (mYesButton != null)
                {
                    mYesButton.SetColor(ButtonWidget.ColorType.LabelHilite, theColor);
                }
                if (mNoButton != null)
                {
                    mNoButton.SetColor(ButtonWidget.ColorType.LabelHilite, theColor);
                }
            }
        }

        public virtual int GetPreferredHeight(int theWidth)
        {
            EnsureFonts();
            int num = mContentInsets.mTop + mContentInsets.mBottom + mBackgroundInsets.mTop + mBackgroundInsets.mBottom;
            bool flag = false;
            if (mDialogHeader.Length > 0)
            {
                num += mHeaderFont.GetHeight() - mHeaderFont.GetAscentPadding();
                flag = true;
            }
            if (mDialogLines.Length > 0)
            {
                if (flag)
                {
                    num += mSpaceAfterHeader;
                }
                Graphics g = GlobalStaticVars.g;
                g.SetFont(mLinesFont);
                num += GetWordWrappedHeight(g, theWidth - mContentInsets.mLeft - mContentInsets.mRight - mBackgroundInsets.mLeft - mBackgroundInsets.mRight - (int)Constants.InvertAndScale(4f), mDialogLines, mLinesFont.GetLineSpacing() + mLineSpacingOffset);
                flag = true;
                num += mSpaceAfterHeader;
            }
            if (mDialogFooter.Length != 0 && mButtonMode != 3)
            {
                if (flag)
                {
                    num += (int)Constants.InvertAndScale(8f);
                }
                num += mHeaderFont.GetLineSpacing();
            }
            return num + AtlasResources.IMAGE_DIALOG_BOTTOMMIDDLE.GetHeight();
        }

        public override void Draw(Graphics g)
        {
            EnsureFonts();
            TRect theDest = new TRect(mBackgroundInsets.mLeft, mBackgroundInsets.mTop, mWidth - mBackgroundInsets.mLeft - mBackgroundInsets.mRight, mHeight - mBackgroundInsets.mTop - mBackgroundInsets.mBottom);
            if (mComponentImage != null)
            {
                g.DrawImageBox(theDest, mComponentImage);
            }
            else
            {
                int num = 6;
                Color theDefaultColor = new Color(GlobalMembersDialog.gDialogColors[num, 0], GlobalMembersDialog.gDialogColors[num, 1], GlobalMembersDialog.gDialogColors[num, 2]);
                g.SetColor(GetColor(Dialog.DialogColour.Outline, theDefaultColor));
                g.DrawRect(12, 12, mWidth - 24 - 1, mHeight - 24 - 1);
                num = 5;
                theDefaultColor = new Color(GlobalMembersDialog.gDialogColors[num, 0], GlobalMembersDialog.gDialogColors[num, 1], GlobalMembersDialog.gDialogColors[num, 2]);
                g.SetColor(GetColor(Dialog.DialogColour.Bkg, theDefaultColor));
                g.FillRect(13, 13, mWidth - 24 - 2, mHeight - 24 - 2);
                g.SetColor(new Color(0, 0, 0, 128));
                g.FillRect(mWidth - 12, 24, 12, mHeight - 36);
                g.FillRect(24, mHeight - 12, mWidth - 24, 12);
            }
            int num2 = mContentInsets.mTop + mBackgroundInsets.mTop;
            if (mDialogHeader.Length > 0)
            {
                num2 += mHeaderFont.GetAscent() - mHeaderFont.GetAscentPadding();
                g.SetFont(mHeaderFont);
                g.SetColor(mColors[0]);
                WriteCenteredLine(g, num2, mDialogHeader);
                num2 += mHeaderFont.GetHeight() - mHeaderFont.GetAscent();
                num2 += mSpaceAfterHeader;
            }
            g.SetFont(mLinesFont);
            g.SetColor(mColors[1]);
            TRect theRect = new TRect(mBackgroundInsets.mLeft + mContentInsets.mLeft + 2, num2, mWidth - mContentInsets.mLeft - mContentInsets.mRight - mBackgroundInsets.mLeft - mBackgroundInsets.mRight - 4, 0);
            num2 += WriteWordWrapped(g, theRect, mDialogLines, mLinesFont.GetLineSpacing() + mLineSpacingOffset, mTextAlign);
            if (mDialogFooter.Length != 0 && mButtonMode != 3)
            {
                num2 += 8;
                num2 += mHeaderFont.GetLineSpacing();
                g.SetFont(mHeaderFont);
                g.SetColor(mColors[2]);
                WriteCenteredLine(g, num2, mDialogFooter);
            }
        }

        public override void AddedToManager(WidgetManager theWidgetManager)
        {
            base.AddedToManager(theWidgetManager);
            if (mYesButton != null)
            {
                theWidgetManager.AddWidget(mYesButton);
            }
            if (mNoButton != null)
            {
                theWidgetManager.AddWidget(mNoButton);
            }
        }

        public override void RemovedFromManager(WidgetManager theWidgetManager)
        {
            base.RemovedFromManager(theWidgetManager);
            if (mYesButton != null)
            {
                theWidgetManager.RemoveWidget(mYesButton);
            }
            if (mNoButton != null)
            {
                theWidgetManager.RemoveWidget(mNoButton);
            }
        }

        public override void OrderInManagerChanged()
        {
            base.OrderInManagerChanged();
            if (mYesButton != null)
            {
                mWidgetManager.PutInfront(mYesButton, this);
            }
            if (mNoButton != null)
            {
                mWidgetManager.PutInfront(mNoButton, this);
            }
        }

        public override void Resize(TRect frame)
        {
            base.Resize(frame);
        }

        public override void Resize(int theX, int theY, int theWidth, int theHeight)
        {
            base.Resize(theX, theY, theWidth, theHeight);
            if (mYesButton != null && mNoButton != null)
            {
                int num = (mWidth - mContentInsets.mLeft - mContentInsets.mRight - mBackgroundInsets.mLeft - mBackgroundInsets.mRight - mButtonSidePadding * 2 - mButtonHorzSpacing) / 2;
                int num2 = mButtonHeight;
                mYesButton.Resize(mX + mBackgroundInsets.mLeft + mContentInsets.mLeft + mButtonSidePadding, mY + mHeight - mContentInsets.mBottom - mBackgroundInsets.mBottom - num2, num, num2);
                mNoButton.Resize(mYesButton.mX + num + mButtonHorzSpacing, mYesButton.mY, num, num2);
                return;
            }
            if (mYesButton != null)
            {
                int num3 = mButtonHeight;
                mYesButton.Resize(mX + mContentInsets.mLeft + mBackgroundInsets.mLeft, mY + mHeight - mContentInsets.mBottom - mBackgroundInsets.mBottom - num3, mWidth - mContentInsets.mLeft - mContentInsets.mRight - mBackgroundInsets.mLeft - mBackgroundInsets.mRight, num3);
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
                mDragging = true;
                mDragMouseX = x;
                mDragMouseY = y;
            }
            base.MouseDown(x, y, theBtnNum, theClickCount);
        }

        public override void MouseDrag(int x, int y)
        {
            if (mDragging)
            {
                var ss = GlobalStaticVars.gSexyAppBase.mScreenScales;
                int edgeX1 = (int)(ss.mTransX / ss.mScaleFactor);
                int edgeX2 = (int)(ss.mWidth / ss.mScaleFactor) - mWidgetManager.mWidth - edgeX1;
                int edgeY1 = (int)(ss.mTransY / ss.mScaleFactor);
                int edgeY2 = (int)(ss.mHeight / ss.mScaleFactor) - mWidgetManager.mHeight - edgeY1;
                int num = mX + x - mDragMouseX;
                int num2 = mY + y - mDragMouseY;
                if (num < -8 - edgeX1)
                {
                    num = -8 - edgeX1;
                }
                else if (num + mWidth > mWidgetManager.mWidth + 8 + edgeX2)
                {
                    num = mWidgetManager.mWidth - mWidth + 8 + edgeX2;
                }
                if (num2 < -8 - edgeY1)
                {
                    num2 = -8 - edgeY1;
                }
                else if (num2 + mHeight > mWidgetManager.mHeight + 8 + edgeY2)
                {
                    num2 = mWidgetManager.mHeight - mHeight + 8 + edgeY2;
                }
                mDragMouseX = mX + x - num;
                mDragMouseY = mY + y - num2;
                if (mDragMouseX < 8)
                {
                    mDragMouseX = 8;
                }
                else if (mDragMouseX > mWidth - 9)
                {
                    mDragMouseX = mWidth - 9;
                }
                if (mDragMouseY < 8)
                {
                    mDragMouseY = 8;
                }
                else if (mDragMouseY > mHeight - 9)
                {
                    mDragMouseY = mHeight - 9;
                }
                Move(num, num2);
            }
        }

        public override void MouseUp(int x, int y, int theClickCount)
        {
            base.MouseUp(x, y, theClickCount);
        }

        public override void MouseUp(int x, int y, int theBtnNum, int theClickCount)
        {
            if (mDragging)
            {
                mDragging = false;
            }
            base.MouseUp(x, y, theBtnNum, theClickCount);
        }

        public override void Update()
        {
            base.Update();
        }

        public virtual bool IsModal()
        {
            return mIsModal;
        }

        public virtual int WaitForResult()
        {
            return WaitForResult(true);
        }

        public virtual int WaitForResult(bool autoKill)
        {
            while (GlobalStaticVars.gSexyAppBase.UpdateApp() && mWidgetManager != null && mResult == 2147483647)
            {
            }
            if (autoKill)
            {
                GlobalStaticVars.gSexyAppBase.KillDialog(mId);
            }
            return mResult;
        }

        public virtual void ButtonPress(int theId)
        {
            if (theId == 1000 || theId == 1001)
            {
                mDialogListener.DialogButtonPress(mId, theId);
            }
        }

        public virtual void ButtonDepress(int theId)
        {
            if (theId == 1000 || theId == 1001)
            {
                mResult = theId;
                mDialogListener.DialogButtonDepress(mId, theId);
            }
        }

        public void ButtonDownTick(int theId)
        {
        }

        public void ButtonPress(int theId, int theClickCount)
        {
            ButtonPress(theId);
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
            Header,
            Lines,
            Footer,
            ButtonText,
            ButtonTextHilite,
            Bkg,
            Outline,
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
