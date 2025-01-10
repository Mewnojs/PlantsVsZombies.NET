using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
    internal static class GlobalMembersButtonWidget
    {
        internal static int[,] gButtonWidgetColors = new int[,]
        {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0},
            {255, 255, 255},
            {132, 132, 132},
            {212, 212, 212}
        };
    }

    public/*internal*/ class ButtonWidget : Widget
    {
        public virtual string mLabel
        {
            get
            {
                return mLabelP;
            }
            set
            {
                mLabelP = value;
            }
        }

        public bool HaveButtonImage(Image theImage, TRect theRect)
        {
            return theImage != null || theRect.mWidth != 0;
        }

        public virtual void DrawButtonImage(Graphics g, Image theImage, TRect theRect, int x, int y)
        {
            if (theRect.mWidth != 0)
            {
                g.DrawImage(mButtonImage, x, y, new TRect(theRect));
                return;
            }
            g.DrawImage(theImage, x, y);
        }

        protected virtual void Reset(int theId, ButtonListener theButtonListener)
        {
            Reset();
            mId = theId;
            mFont = null;
            mLabelJustify = 0;
            mButtonImage = null;
            mOverImage = null;
            mDownImage = null;
            mDisabledImage = null;
            mInverted = false;
            mBtnNoDraw = false;
            mFrameNoDraw = false;
            mButtonListener = theButtonListener;
            mHasAlpha = true;
            mOverAlpha = 0.0;
            mOverAlphaSpeed = 0.0;
            mOverAlphaFadeInSpeed = 0.0;
            SetColors(GlobalMembersButtonWidget.gButtonWidgetColors, 6);
        }

        public ButtonWidget(int theId, ButtonListener theButtonListener)
        {
            Reset(theId, theButtonListener);
        }

        public virtual void SetFont(Font theFont)
        {
            mFont = theFont.Duplicate();
        }

        public virtual bool IsButtonDown()
        {
            return mIsDown && mIsOver && !mDisabled;
        }

        public override void Draw(Graphics g)
        {
            if (mBtnNoDraw)
            {
                return;
            }
            bool flag = mIsDown && mIsOver && !mDisabled;
            flag ^= mInverted;
            int num = 0;
            int num2 = 0;
            int num3 = mTranslateWhenDown ? 1 : 0;
            if (mFont != null)
            {
                if (mLabelJustify == 0)
                {
                    num = (mWidth - mFont.StringWidth(mLabel)) / 2;
                }
                else if (mLabelJustify == 1)
                {
                    num = mWidth - mFont.StringWidth(mLabel);
                }
                num2 = (mHeight + mFont.GetAscent() - mFont.GetAscent() / 6 - 1) / 2;
            }
            g.SetFont(mFont);
            if (mButtonImage == null && mDownImage == null)
            {
                if (!mFrameNoDraw)
                {
                    g.SetColor(mColors[5]);
                    g.FillRect(0, 0, mWidth, mHeight);
                }
                if (flag)
                {
                    if (!mFrameNoDraw)
                    {
                        g.SetColor(mColors[2]);
                        g.FillRect(0, 0, mWidth - 1, 1);
                        g.FillRect(0, 0, 1, mHeight - 1);
                        g.SetColor(mColors[3]);
                        g.FillRect(0, mHeight - 1, mWidth, 1);
                        g.FillRect(mWidth - 1, 0, 1, mHeight);
                        g.SetColor(mColors[4]);
                        g.FillRect(1, 1, mWidth - 3, 1);
                        g.FillRect(1, 1, 1, mHeight - 3);
                    }
                    if (mIsOver)
                    {
                        g.SetColor(mColors[1]);
                    }
                    else
                    {
                        g.SetColor(mColors[0]);
                    }
                    g.DrawString(mLabel, num + num3, num2 + num3);
                    return;
                }
                if (!mFrameNoDraw)
                {
                    g.SetColor(mColors[3]);
                    g.FillRect(0, 0, mWidth - 1, 1);
                    g.FillRect(0, 0, 1, mHeight - 1);
                    g.SetColor(mColors[2]);
                    g.FillRect(0, mHeight - 1, mWidth, 1);
                    g.FillRect(mWidth - 1, 0, 1, mHeight);
                    g.SetColor(mColors[4]);
                    g.FillRect(1, mHeight - 2, mWidth - 2, 1);
                    g.FillRect(mWidth - 2, 1, 1, mHeight - 2);
                }
                if (mIsOver)
                {
                    g.SetColor(mColors[1]);
                }
                else
                {
                    g.SetColor(mColors[0]);
                }
                g.DrawString(mLabel, num, num2);
                return;
            }
            else
            {
                if (!flag)
                {
                    if (mDisabled && HaveButtonImage(mDisabledImage, new TRect(mDisabledRect)))
                    {
                        DrawButtonImage(g, mDisabledImage, new TRect(mDisabledRect), 0, 0);
                    }
                    else if (mOverAlpha > 0.0 && HaveButtonImage(mOverImage, new TRect(mOverRect)))
                    {
                        if (HaveButtonImage(mButtonImage, new TRect(mNormalRect)) && mOverAlpha < 1.0)
                        {
                            DrawButtonImage(g, mButtonImage, new TRect(mNormalRect), 0, 0);
                        }
                        g.SetColorizeImages(true);
                        g.SetColor(new Color(255, 255, 255, (int)(mOverAlpha * 255.0)));
                        DrawButtonImage(g, mOverImage, new TRect(mOverRect), 0, 0);
                        g.SetColorizeImages(false);
                    }
                    else if ((mIsOver || mIsDown) && HaveButtonImage(mOverImage, new TRect(mOverRect)))
                    {
                        DrawButtonImage(g, mOverImage, new TRect(mOverRect), 0, 0);
                    }
                    else if (HaveButtonImage(mButtonImage, new TRect(mNormalRect)))
                    {
                        DrawButtonImage(g, mButtonImage, new TRect(mNormalRect), 0, 0);
                    }
                    if (mIsOver)
                    {
                        g.SetColor(mColors[1]);
                    }
                    else
                    {
                        g.SetColor(mColors[0]);
                    }
                    g.DrawString(mLabel, num, num2);
                    return;
                }
                if (HaveButtonImage(mDownImage, new TRect(mDownRect)))
                {
                    DrawButtonImage(g, mDownImage, new TRect(mDownRect), 0, 0);
                }
                else if (HaveButtonImage(mOverImage, new TRect(mOverRect)))
                {
                    DrawButtonImage(g, mOverImage, new TRect(mOverRect), num3, num3);
                }
                else
                {
                    DrawButtonImage(g, mButtonImage, new TRect(mNormalRect), num3, num3);
                }
                g.SetColor(mColors[1]);
                g.DrawString(mLabel, num + num3, num2 + num3);
                return;
            }
        }

        public override void SetDisabled(bool isDisabled)
        {
            base.SetDisabled(isDisabled);
            if (HaveButtonImage(mDisabledImage, new TRect(mDisabledRect)))
            {
                MarkDirty();
            }
        }

        public override void MouseEnter()
        {
            base.MouseEnter();
            if (mOverAlphaFadeInSpeed == 0.0 && mOverAlpha > 0.0)
            {
                mOverAlpha = 0.0;
            }
            if (mIsDown || HaveButtonImage(mOverImage, new TRect(mOverRect)) || mColors[1] != mColors[0])
            {
                MarkDirty();
            }
            mButtonListener.ButtonMouseEnter(mId);
        }

        public override void MouseLeave()
        {
            base.MouseLeave();
            if (mOverAlphaSpeed == 0.0 && mOverAlpha > 0.0)
            {
                mOverAlpha = 0.0;
            }
            else if (mOverAlphaSpeed > 0.0 && mOverAlpha == 0.0)
            {
                mOverAlpha = 1.0;
            }
            if (mIsDown || HaveButtonImage(mOverImage, new TRect(mOverRect)) || mColors[1] != mColors[0])
            {
                MarkDirty();
            }
            mButtonListener.ButtonMouseLeave(mId);
        }

        public override void MouseMove(int theX, int theY)
        {
            base.MouseMove(theX, theY);
            mButtonListener.ButtonMouseMove(mId, theX, theY);
        }

        public override void MouseDown(int theX, int theY, int theClickCount)
        {
            base.MouseDown(theX, theY, theClickCount);
        }

        public override void MouseDown(int theX, int theY, int theBtnNum, int theClickCount)
        {
            base.MouseDown(theX, theY, theBtnNum, theClickCount);
            mButtonListener.ButtonPress(mId, theClickCount);
            MarkDirty();
        }

        public override void MouseUp(int theX, int theY, int theClickCount)
        {
            base.MouseUp(theX, theY, theClickCount);
        }

        public override void MouseUp(int theX, int theY, int theBtnNum, int theClickCount)
        {
            base.MouseUp(theX, theY, theBtnNum, theClickCount);
            if (mIsOver && mWidgetManager.mHasFocus)
            {
                mButtonListener.ButtonDepress(mId);
            }
            MarkDirty();
        }

        public override void Update()
        {
            base.Update();
            if (mIsDown && mIsOver)
            {
                mButtonListener.ButtonDownTick(mId);
            }
            if (!mIsDown && !mIsOver && mOverAlpha > 0.0)
            {
                if (mOverAlphaSpeed > 0.0)
                {
                    mOverAlpha -= mOverAlphaSpeed;
                    if (mOverAlpha < 0.0)
                    {
                        mOverAlpha = 0.0;
                    }
                }
                else
                {
                    mOverAlpha = 0.0;
                }
                MarkDirty();
                return;
            }
            if (mIsOver && mOverAlphaFadeInSpeed > 0.0 && mOverAlpha < 1.0)
            {
                mOverAlpha += mOverAlphaFadeInSpeed;
                if (mOverAlpha > 1.0)
                {
                    mOverAlpha = 1.0;
                }
                MarkDirty();
            }
        }

        public override void Dispose()
        {
            mFont = null;
        }

        public int mId;

        private string mLabelP = string.Empty;

        public int mLabelJustify;

        public Font mFont;

        public Image mButtonImage;

        public Image mOverImage;

        public Image mDownImage;

        public Image mDisabledImage;

        public TRect mNormalRect = default(TRect);

        public TRect mOverRect = default(TRect);

        public TRect mDownRect = default(TRect);

        public TRect mDisabledRect = default(TRect);

        public bool mInverted;

        public bool mBtnNoDraw;

        public bool mFrameNoDraw;

        public ButtonListener mButtonListener;

        public double mOverAlpha;

        public double mOverAlphaSpeed;

        public double mOverAlphaFadeInSpeed;

        public bool mTranslateWhenDown = true;

        public enum ButtonPosition
        {
            Left = -1,
            Center,
            Right
        }

        public enum ColorType
        {
            Label,
            LabelHilite,
            DarkOutline,
            LightOutline,
            MediumOutline,
            Bkg,
            NUM_COLORS
        }
    }
}
