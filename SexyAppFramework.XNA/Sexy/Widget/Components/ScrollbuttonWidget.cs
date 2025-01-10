using System;

namespace Sexy
{
    public/*internal*/ class ScrollbuttonWidget : ButtonWidget
    {
        public ScrollbuttonWidget(int theId, ButtonListener theButtonListener) : this(theId, theButtonListener, 0)
        {
        }

        public ScrollbuttonWidget(int theId, ButtonListener theButtonListener, int theType) : base(theId, theButtonListener)
        {
            mHorizontal = false;
            mType = theType;
        }

        public override void Draw(Graphics g)
        {
            if (mButtonImage == null && mDownImage == null)
            {
                int num = 0;
                g.SetColor(GetColor(5));
                g.FillRect(0, 0, mWidth, mHeight);
                if (mIsDown && mIsOver && !mDisabled)
                {
                    num = 1;
                    g.SetColor(GetColor(4));
                    g.DrawRect(0, 0, mWidth - 1, mHeight - 1);
                }
                else
                {
                    g.SetColor(GetColor(3));
                    g.FillRect(1, 1, mWidth - 2, 1);
                    g.FillRect(1, 1, 1, mHeight - 2);
                    g.SetColor(GetColor(2));
                    g.FillRect(0, mHeight - 1, mWidth, 1);
                    g.FillRect(mWidth - 1, 0, 1, mHeight);
                    g.SetColor(GetColor(4));
                    g.FillRect(1, mHeight - 2, mWidth - 2, 1);
                    g.FillRect(mWidth - 2, 1, 1, mHeight - 2);
                }
                if (!mDisabled)
                {
                    g.SetColor(GetColor(2));
                }
                else
                {
                    g.SetColor(GetColor(4));
                }
                if (mHorizontal || mType == 3 || mType == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (mId == 0 || mType == 3)
                        {
                            g.FillRect(i + (mWidth - 4) / 2 + num, mHeight / 2 - i - 1 + num, 1, 1 + i * 2);
                        }
                        else
                        {
                            g.FillRect(3 - i + (mWidth - 4) / 2 + num, mHeight / 2 - i - 1 + num, 1, 1 + i * 2);
                        }
                    }
                    return;
                }
                for (int j = 0; j < 4; j++)
                {
                    if (mId == 0 || mType == 1)
                    {
                        g.FillRect(mWidth / 2 - j - 1 + num, j + (mHeight - 4) / 2 + num, 1 + j * 2, 1);
                    }
                    else
                    {
                        g.FillRect(mWidth / 2 - j - 1 + num, 3 - j + (mHeight - 4) / 2 + num, 1 + j * 2, 1);
                    }
                }
                return;
            }
            else
            {
                int num2 = 0;
                if (mType > 0)
                {
                    num2 = mType - 1;
                    if (num2 > 2 && mButtonImage.mNumCols <= 2)
                    {
                        num2 -= 2;
                    }
                }
                else
                {
                    if (mHorizontal && mButtonImage.mNumCols > 2)
                    {
                        num2 += 2;
                    }
                    if (mId == 1)
                    {
                        num2++;
                    }
                }
                if (mIsDown && mIsOver && !mDisabled)
                {
                    g.DrawImageCel(mDownImage, 0, 0, num2);
                    return;
                }
                if (mDisabled && mDisabledImage != null)
                {
                    g.DrawImageCel(mDisabledImage, 0, 0, num2);
                    return;
                }
                g.DrawImageCel(mButtonImage, 0, 0, num2);
                return;
            }
        }

        public bool mHorizontal;

        public int mType;
    }
}
