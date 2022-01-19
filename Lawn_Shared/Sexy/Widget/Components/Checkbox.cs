using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
    internal class Checkbox : Widget
    {
        public virtual void SetChecked(bool isChecked)
        {
            SetChecked(isChecked, true);
        }

        public virtual void SetChecked(bool @checked, bool tellListener)
        {
            mChecked = @checked;
            if (tellListener && mListener != null)
            {
                mListener.CheckboxChecked(mId, mChecked);
            }
            MarkDirty();
        }

        public virtual bool IsChecked()
        {
            return mChecked;
        }

        public override void MouseDown(int x, int y, int theClickCount)
        {
            base.MouseDown(x, y, theClickCount);
        }

        public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
        {
            base.MouseDown(x, y, theBtnNum, theClickCount);
            mChecked = !mChecked;
            if (mListener != null)
            {
                mListener.CheckboxChecked(mId, mChecked);
            }
            MarkDirty();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            if (mCheckedRect.mWidth == 0 && mCheckedImage != null && mUncheckedImage != null)
            {
                if (mChecked)
                {
                    g.DrawImage(mCheckedImage, 0, 0);
                    return;
                }
                g.DrawImage(mUncheckedImage, 0, 0);
                return;
            }
            else
            {
                if (mCheckedRect.mWidth == 0 || mUncheckedImage == null)
                {
                    if (mUncheckedImage == null && mCheckedImage == null)
                    {
                        g.SetColor(new SexyColor(mOutlineColor));
                        g.FillRect(0, 0, mWidth, mHeight);
                        g.SetColor(new SexyColor(mBkgColor));
                        g.FillRect(1, 1, mWidth - 2, mHeight - 2);
                        if (mChecked)
                        {
                            g.SetColor(new SexyColor(mCheckColor));
                            g.DrawLine(1, 1, mWidth - 2, mHeight - 2);
                            g.DrawLine(mWidth - 1, 1, 1, mHeight - 2);
                        }
                    }
                    return;
                }
                if (mChecked)
                {
                    g.DrawImage(mUncheckedImage, 0, 0, new TRect(mCheckedRect));
                    return;
                }
                g.DrawImage(mUncheckedImage, 0, 0, new TRect(mUncheckedRect));
                return;
            }
        }

        public Checkbox(Image theUncheckedImage, Image theCheckedImage, int theId, CheckboxListener theCheckboxListener)
        {
            mUncheckedImage = theUncheckedImage;
            mCheckedImage = theCheckedImage;
            mId = theId;
            mListener = theCheckboxListener;
            mChecked = false;
            mOutlineColor = new SexyColor(Color.White);
            mBkgColor = new SexyColor(new Color(80, 80, 80));
            mCheckColor = new SexyColor(new Color(255, 255, 0));
            mDoFinger = true;
        }

        protected CheckboxListener mListener;

        public int mId;

        public bool mChecked;

        public Image mUncheckedImage;

        public Image mCheckedImage;

        public TRect mCheckedRect = default(TRect);

        public TRect mUncheckedRect = default(TRect);

        public Color mOutlineColor = default(Color);

        public Color mBkgColor = default(Color);

        public Color mCheckColor = default(Color);
    }
}
