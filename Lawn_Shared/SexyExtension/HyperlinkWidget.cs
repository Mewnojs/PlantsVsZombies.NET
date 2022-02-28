using System;
using Microsoft.Xna.Framework;
using Sexy.GraphicsLib;

namespace Sexy.WidgetsLib
{
    public/*internal*/ class HyperlinkWidget : ButtonWidget
    {
        public HyperlinkWidget(int theId, ButtonListener theButtonListener) : base(theId, theButtonListener)
        {
            mColor = new SexyColor(255, 255, 255);
            mOverColor = new SexyColor(255, 255, 255);
            mDoFinger = true;
            mUnderlineOffset = 3;
            mUnderlineSize = 1;
        }

        public override void Draw(Graphics g)
        {
            int theX = (mWidth - mFont.StringWidth(mLabel)) / 2;
            int num = (mHeight + mFont.GetAscent()) / 2 - 1;
            if (mIsOver)
            {
                g.SetColor(mOverColor);
            }
            else
            {
                g.SetColor(mColor);
            }
            g.SetFont(mFont);
            g.DrawString(mLabel, theX, num);
            mUnderlineOffset = (int)(mFont.GetHeight() + Constants.InvertAndScale(3f));
            for (int i = 0; i < mUnderlineSize; i++)
            {
                g.FillRect(theX, num + mUnderlineOffset + i, mFont.StringWidth(mLabel), 1);
            }
        }

        public override void MouseEnter()
        {
            base.MouseEnter();
            MarkDirtyFull();
        }

        public override void MouseLeave()
        {
            base.MouseLeave();
            MarkDirtyFull();
        }

        public SexyColor mColor = default(SexyColor);

        public SexyColor mOverColor = default(SexyColor);

        public int mUnderlineSize;

        public int mUnderlineOffset;
    }
}
