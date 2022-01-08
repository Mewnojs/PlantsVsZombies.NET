using System;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class GameButton
    {
        public string mLabel
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                CalculateTextScale();
            }
        }

        public GameButton(int theId, Widget parent)
        {
            mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
            mId = theId;
            mFont = null;
            mLabelJustify = 0;
            mButtonImage = null;
            mOverImage = null;
            mOverOverlayImage = null;
            mDownImage = null;
            mDisabledImage = null;
            mInverted = false;
            mBtnNoDraw = false;
            mFrameNoDraw = false;
            mDisabled = false;
            mIsDown = false;
            mIsOver = false;
            mX = 0;
            mY = 0;
            mWidth = 0;
            mHeight = 0;
            mParentWidget = parent;
            mDrawStoneButton = false;
            mTextOffsetX = 0;
            mTextOffsetY = 0;
            mButtonOffsetX = 0;
            mButtonOffsetY = 0;
            mOverAlpha = 0.0;
            mOverAlphaSpeed = 0.0;
            mOverAlphaFadeInSpeed = 0.0;
            for (int i = 0; i < 6; i++)
            {
                mColors[i] = GameConstants.gGameButtonColors[i];
            }
            mFont = Resources.FONT_DWARVENTODCRAFT15;
        }

        public void Dispose()
        {
            if (mFont != null)
            {
                mFont.Dispose();
            }
        }

        public bool HaveButtonImage(Image theImage, TRect theRect)
        {
            return theImage != null || theRect.mWidth != 0;
        }

        public void DrawButtonImage(Graphics g, Image theImage, TRect theRect, int x, int y)
        {
            if (theRect.mWidth != 0)
            {
                g.DrawImage(mButtonImage, x + mButtonOffsetX, y + mButtonOffsetY, theRect);
                return;
            }
            g.DrawImage(theImage, x + mButtonOffsetX, y + mButtonOffsetY);
        }

        public void SetFont(Font theFont)
        {
            if (mFont != null)
            {
                mFont.Dispose();
            }
            mFont = theFont.Duplicate();
        }

        public bool IsButtonDown()
        {
            return mIsDown && mIsOver && !mDisabled && !mBtnNoDraw;
        }

        public void Draw(Graphics g)
        {
            if (mBtnNoDraw)
            {
                return;
            }
            bool flag = mIsDown && mIsOver && !mDisabled;
            flag ^= mInverted;
            bool flag2 = mIsOver && !mDisabled;
            if (mDrawStoneButton)
            {
                GameButton.DrawStoneButton(g, mX, mY, mWidth, mHeight, flag, flag2, mLabel, mFont, mFontScale);
                return;
            }
            g.mTransX += mX;
            g.mTransY += mY;
            int num = mTextOffsetX;
            int num2 = mTextOffsetY;
            if (mFont != null && mLabelJustify == 1)
            {
                num += mWidth - mFont.StringWidth(mLabel);
            }
            g.SetFont(mFont);
            if (!flag)
            {
                if (mDisabled && HaveButtonImage(mDisabledImage, mDisabledRect))
                {
                    DrawButtonImage(g, mDisabledImage, mDisabledRect, 0, 0);
                }
                else if (mOverAlpha > 0.0 && HaveButtonImage(mOverImage, mOverRect))
                {
                    if (HaveButtonImage(mButtonImage, mNormalRect) && mOverAlpha < 1.0)
                    {
                        DrawButtonImage(g, mButtonImage, mNormalRect, 0, 0);
                    }
                    g.SetColorizeImages(true);
                    g.SetColor(new SexyColor(255, 255, 255, (int)(mOverAlpha * 255.0)));
                    DrawButtonImage(g, mOverImage, mOverRect, 0, 0);
                    g.SetColorizeImages(false);
                }
                else if (flag2 && HaveButtonImage(mOverImage, mOverRect))
                {
                    DrawButtonImage(g, mOverImage, mOverRect, 0, 0);
                }
                else if (HaveButtonImage(mButtonImage, mNormalRect))
                {
                    DrawButtonImage(g, mButtonImage, mNormalRect, 0, 0);
                }
                if (flag2)
                {
                    g.SetColor(mColors[1]);
                }
                else
                {
                    g.SetColor(mColors[0]);
                }
                g.SetScale(mFontScale);
                if (mLabelJustify == 0)
                {
                    num += (mWidth - mFont.StringWidth(mLabel)) / 2 + 1;
                }
                num2 += (mHeight + mFont.GetAscent()) / 2;
                g.DrawString(mLabel, num, num2);
                g.SetScale(1f);
                if (flag2 && mOverOverlayImage != null)
                {
                    g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                    DrawButtonImage(g, mOverOverlayImage, mNormalRect, 0, 0);
                    g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
                }
            }
            else
            {
                if (HaveButtonImage(mDownImage, mDownRect))
                {
                    DrawButtonImage(g, mDownImage, mDownRect, 0, 0);
                }
                else if (HaveButtonImage(mOverImage, mOverRect))
                {
                    DrawButtonImage(g, mOverImage, mOverRect, 1, 1);
                }
                else
                {
                    DrawButtonImage(g, mButtonImage, mNormalRect, 1, 1);
                }
                g.SetColor(mColors[1]);
                g.SetScale(mFontScale);
                if (mLabelJustify == 0)
                {
                    num += (mWidth - mFont.StringWidth(mLabel)) / 2 + 1;
                }
                num2 += (mHeight + mFont.GetAscent()) / 2;
                g.DrawString(mLabel, num + mTextPushOffsetX, num2 + mTextPushOffsetY);
                g.SetScale(1f);
                if (flag2 && mOverOverlayImage != null)
                {
                    g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
                    DrawButtonImage(g, mOverOverlayImage, mNormalRect, 0, 0);
                    g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
                }
            }
            g.mTransX -= mX;
            g.mTransY -= mY;
        }

        public void SetDisabled(bool isDisabled)
        {
            mDisabled = isDisabled;
        }

        public bool IsMouseOver()
        {
            return mIsOver && !mDisabled && !mBtnNoDraw;
        }

        public void Update()//13update
        {
            int num = mApp.mWidgetManager.mLastMouseX;
            int num2 = mApp.mWidgetManager.mLastMouseY;
            if (mParentWidget != null)
            {
                CGPoint absPos = mParentWidget.GetAbsPos();
                num -= (int)absPos.mX;
                num2 -= (int)absPos.mY;
            }
            if (num >= mX && num < mX + mWidth && num2 >= mY && num2 < mY + mHeight)
            {
                mIsOver = true;
            }
            else
            {
                mIsOver = false;
            }
            if ((mApp.mWidgetManager.mDownButtons & 5) != 0)
            {
                mIsDown = true;
            }
            else
            {
                mIsDown = false;
            }
            if ((mApp.mWidgetManager.mFocusWidget == null || mApp.mWidgetManager.mFocusWidget != mParentWidget) && mApp.GetDialogCount() > 0)
            {
                mIsDown = false;
                mIsOver = false;
            }
            if (!mIsDown && !mIsOver && mOverAlpha > 0.0)
            {
                if (mOverAlphaSpeed <= 0.0)
                {
                    mOverAlpha = 0.0;
                    return;
                }
                mOverAlpha -= mOverAlphaSpeed;
                if (mOverAlpha < 0.0)
                {
                    mOverAlpha = 0.0;
                    return;
                }
            }
            else if (mIsOver && mOverAlphaFadeInSpeed > 0.0 && mOverAlpha < 1.0)
            {
                if (mOverAlphaFadeInSpeed > 0.0)
                {
                    mOverAlpha += mOverAlphaFadeInSpeed;
                    if (mOverAlpha > 1.0)
                    {
                        mOverAlpha = 1.0;
                        return;
                    }
                }
                else
                {
                    mOverAlpha = 1.0;
                }
            }
        }

        public void Resize(int theX, int theY, int theWidth, int theHeight)
        {
            mX = theX;
            mY = theY;
            mWidth = theWidth;
            mHeight = theHeight;
            CalculateTextScale();
        }

        private void CalculateTextScale()
        {
            mFontScale = 1f;
            if (mFont != null)
            {
                Vector2 vector = mFont.MeasureString(mLabel);
                mTextHeight = vector.Y;
                if (mWidth > 0)
                {
                    int num = (int)(mWidth - Constants.S * 30f);
                    if (vector.X > num)
                    {
                        mFontScale = num / vector.X;
                    }
                }
            }
        }

        public void SetLabel(string theLabel)
        {
            mLabel = TodStringFile.TodStringTranslate(theLabel);
            CalculateTextScale();
        }

        public static NewLawnButton MakeNewButton(int theId, ButtonListener theListener, string theText, Font theFont, Image theImageNormal, Image theImageOver, Image theImageDown)
        {
            NewLawnButton newLawnButton = new NewLawnButton(null, theId, theListener);
            newLawnButton.SetFont((theFont != null) ? theFont : Resources.FONT_BRIANNETOD12);
            newLawnButton.SetLabel(theText);
            if (theImageNormal != null)
            {
                newLawnButton.mWidth = theImageNormal.mWidth;
                newLawnButton.mHeight = theImageNormal.mHeight;
            }
            else
            {
                newLawnButton.mWidth = theImageDown.mWidth;
                newLawnButton.mHeight = theImageDown.mHeight;
            }
            newLawnButton.mButtonImage = theImageNormal;
            newLawnButton.mOverImage = theImageOver;
            newLawnButton.mDownImage = theImageDown;
            newLawnButton.mHasAlpha = true;
            newLawnButton.mHasTransparencies = true;
            if (theImageDown == theImageOver)
            {
                newLawnButton.mTranslateX = 1;
                newLawnButton.mTranslateY = 1;
            }
            else
            {
                newLawnButton.mTranslateX = 1;
                newLawnButton.mTranslateY = 1;
            }
            return newLawnButton;
        }

        public static void DrawStoneButton(Graphics g, int x, int y, int theWidth, int theHeight, bool isDown, bool isHighLighted, string theLabel)
        {
            GameButton.DrawStoneButton(g, x, y, theWidth, theHeight, isDown, isHighLighted, theLabel, Resources.FONT_DWARVENTODCRAFT15, 1f);
        }

        public static void DrawStoneButton(Graphics g, int x, int y, int theWidth, int theHeight, bool isDown, bool isHighLighted, string theLabel, Font theFont, float fontScale)
        {
            int num = x;
            int num2 = y;
            int num3 = x;
            Image image = AtlasResources.IMAGE_BUTTON_LEFT;
            Image theImage = AtlasResources.IMAGE_BUTTON_MIDDLE;
            Image image2 = AtlasResources.IMAGE_BUTTON_RIGHT;
            if (isDown)
            {
                image = AtlasResources.IMAGE_BUTTON_DOWN_LEFT;
                theImage = AtlasResources.IMAGE_BUTTON_DOWN_MIDDLE;
                image2 = AtlasResources.IMAGE_BUTTON_DOWN_RIGHT;
                num++;
                num2++;
                num3++;
            }
            int num4 = theWidth - image.mWidth - image2.mWidth;
            g.DrawImage(image, num3, y);
            num3 += image.mWidth;
            LawnCommon.TileImageHorizontally(g, theImage, num3, y, num4);
            num3 += num4;
            g.DrawImage(image2, num3, y);
            g.SetFont(theFont);
            g.SetScale(fontScale);
            g.SetColor(new SexyColor(21, 175, 0));
            num += (theWidth - theFont.StringWidth(theLabel)) / 2 + 1;
            num2 += (theHeight + theFont.GetAscent()) / 2;
            g.DrawString(theLabel, num, num2);
            g.SetColor(SexyColor.White);
            g.SetScale(1f);
        }

        public static LawnStoneButton MakeButton(int theId, ButtonListener theListener, string theText)
        {
            LawnStoneButton lawnStoneButton = new LawnStoneButton(null, theId, theListener);
            lawnStoneButton.SetLabel(theText);
            lawnStoneButton.mHeight = (int)Constants.InvertAndScale(33f);
            lawnStoneButton.mTranslateX = 1;
            lawnStoneButton.mTranslateY = 1;
            lawnStoneButton.mHasAlpha = true;
            lawnStoneButton.mHasTransparencies = true;
            return lawnStoneButton;
        }

        public LawnApp mApp;

        public Widget mParentWidget;

        public int mX;

        public int mY;

        public int mWidth;

        public int mHeight;

        public bool mIsOver;

        public bool mIsDown;

        public bool mDisabled;

        public SexyColor[] mColors = new SexyColor[6];

        public int mId;

        private string _label = string.Empty;

        private float mFontScale;

        private float mTextHeight;

        private Font _font;

        public int mLabelJustify;

        public Font mFont;

        public Image mButtonImage;

        public Image mOverImage;

        public Image mDownImage;

        public Image mDisabledImage;

        public Image mOverOverlayImage;

        public TRect mNormalRect = default(TRect);

        public TRect mOverRect = default(TRect);

        public TRect mDownRect = default(TRect);

        public TRect mDisabledRect = default(TRect);

        public bool mInverted;

        public bool mBtnNoDraw;

        public bool mFrameNoDraw;

        public double mOverAlpha;

        public double mOverAlphaSpeed;

        public double mOverAlphaFadeInSpeed;

        public bool mDrawStoneButton;

        public int mTextOffsetX;

        public int mTextOffsetY;

        public int mButtonOffsetX;

        public int mButtonOffsetY;

        public int mTextPushOffsetX = 1;

        public int mTextPushOffsetY = 1;

        private string label = string.Empty;

        public enum ButtonLabelJustify
        {
            Left = -1,
            Center,
            Right
        }

        public enum ButtonColours
        {
            Label,
            LabelHilite,
            DarkOutline,
            LightOutline,
            MediumOutline,
            Bkg,
            ColorCount
        }
    }
}
