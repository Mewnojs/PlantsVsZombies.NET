using System;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.Misc;
using Sexy.TodLib;
using Sexy.WidgetsLib;

namespace Lawn
{
    public/*internal*/ class NewLawnButton : DialogButton
    {
        public NewLawnButton(Image theComponentImage, int theId, ButtonListener theListener) : base(theComponentImage, theId, theListener)
        {
            mHiliteFont = null;
            mTextDownOffsetX = 0;
            mTextDownOffsetY = 0;
            mButtonOffsetX = 0;
            mButtonOffsetY = 0;
            mUsePolygonShape = false;
            SetColor(5, SexyColor.White);
        }

        public override void Draw(Graphics g)
        {
            if (mBtnNoDraw)
            {
                return;
            }
            bool flag = mIsDown && mIsOver && !mDisabled;
            flag ^= mInverted;
            int num = mTextOffsetX + mTranslateX;
            int num2 = mTextOffsetY + mTranslateY;
            if (mFont != null)
            {
                if (mLabelJustify == 0)
                {
                    num += (mWidth - mFont.StringWidth(mLabel)) / 2;
                }
                else if (mLabelJustify == 1)
                {
                    num += mWidth - mFont.StringWidth(mLabel);
                }
                num2 += (mHeight + mFont.GetAscent() - mFont.GetAscent() / 6 - 1) / 2;
            }
            if (!flag)
            {
                g.SetColorizeImages(true);
                g.SetColor(mColors[5]);
                if (mDisabled && base.HaveButtonImage(mDisabledImage, mDisabledRect))
                {
                    DrawButtonImage(g, mDisabledImage, mDisabledRect, mButtonOffsetX, mButtonOffsetY);
                }
                else if (mOverAlpha > 0.0 && base.HaveButtonImage(mOverImage, mOverRect))
                {
                    if (base.HaveButtonImage(mButtonImage, mNormalRect) && mOverAlpha < 1.0)
                    {
                        DrawButtonImage(g, mButtonImage, mNormalRect, mButtonOffsetX, mButtonOffsetY);
                    }
                    SexyColor aColor = g.GetColor();
                    aColor.mAlpha = (int)(mOverAlpha * 255.0);
                    g.SetColor(aColor);
                    DrawButtonImage(g, mOverImage, mOverRect, mButtonOffsetX, mButtonOffsetY);
                }
                else if ((mIsOver || mIsDown) && base.HaveButtonImage(mOverImage, mOverRect))
                {
                    DrawButtonImage(g, mOverImage, mOverRect, mButtonOffsetX, mButtonOffsetY);
                }
                else if (base.HaveButtonImage(mButtonImage, mNormalRect))
                {
                    DrawButtonImage(g, mButtonImage, mNormalRect, mButtonOffsetX, mButtonOffsetY);
                }
                g.SetColorizeImages(false);
                if (mIsOver)
                {
                    if (mHiliteFont != null)
                    {
                        g.SetFont(mHiliteFont);
                    }
                    else
                    {
                        g.SetFont(mFont);
                    }
                    g.SetColor(mColors[1]);
                }
                else
                {
                    g.SetFont(mFont);
                    g.SetColor(mColors[0]);
                }
                g.DrawString(mLabel, num, num2);
                return;
            }
            g.SetColorizeImages(true);
            g.SetColor(mColors[5]);
            if (base.HaveButtonImage(mDownImage, mDownRect))
            {
                DrawButtonImage(g, mDownImage, mDownRect, mButtonOffsetX + mTranslateX, mButtonOffsetY + mTranslateY);
            }
            else if (base.HaveButtonImage(mOverImage, mOverRect))
            {
                DrawButtonImage(g, mOverImage, mOverRect, mButtonOffsetX + mTranslateX, mButtonOffsetY + mTranslateY);
            }
            else if (base.HaveButtonImage(mButtonImage, mNormalRect))
            {
                DrawButtonImage(g, mButtonImage, mNormalRect, mButtonOffsetX + mTranslateX, mButtonOffsetY + mTranslateY);
            }
            g.SetColorizeImages(false);
            if (mHiliteFont != null)
            {
                g.SetFont(mHiliteFont);
            }
            else
            {
                g.SetFont(mFont);
            }
            g.SetColor(mColors[1]);
            g.DrawString(mLabel, num + mTextDownOffsetX, num2 + mTextDownOffsetY);
        }

        public override bool IsPointVisible(int x, int y)
        {
            if (!mUsePolygonShape)
            {
                return base.IsPointVisible(x, y);
            }
            SexyVector2 theCheckPoint = new SexyVector2(x, y);
            return TodCommon.TodIsPointInPolygon(mPolygonShape, 4, theCheckPoint);
        }

        public void SetLabel(string theLabel)
        {
            mLabel = TodStringFile.TodStringTranslate(theLabel);
        }

        public Font mHiliteFont;

        public int mTextDownOffsetX;

        public int mTextDownOffsetY;

        public int mButtonOffsetX;

        public int mButtonOffsetY;

        public bool mUsePolygonShape;

        public SexyVector2[] mPolygonShape = new SexyVector2[4];
    }
}
