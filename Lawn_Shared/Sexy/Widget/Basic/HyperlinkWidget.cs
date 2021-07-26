using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	internal class HyperlinkWidget : ButtonWidget
	{
		public HyperlinkWidget(int theId, ButtonListener theButtonListener) : base(theId, theButtonListener)
		{
			this.mColor = new Color(255, 255, 255);
			this.mOverColor = new Color(255, 255, 255);
			this.mDoFinger = true;
			this.mUnderlineOffset = 3;
			this.mUnderlineSize = 1;
		}

		public override void Draw(Graphics g)
		{
			int theX = (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2;
			int num = (this.mHeight + this.mFont.GetAscent()) / 2 - 1;
			if (this.mIsOver)
			{
				g.SetColor(this.mOverColor);
			}
			else
			{
				g.SetColor(this.mColor);
			}
			g.SetFont(this.mFont);
			g.DrawString(this.mLabel, theX, num);
			this.mUnderlineOffset = (int)((float)this.mFont.GetHeight() + Constants.InvertAndScale(3f));
			for (int i = 0; i < this.mUnderlineSize; i++)
			{
				g.FillRect(theX, num + this.mUnderlineOffset + i, this.mFont.StringWidth(this.mLabel), 1);
			}
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.MarkDirtyFull();
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			this.MarkDirtyFull();
		}

		public Color mColor = default(Color);

		public Color mOverColor = default(Color);

		public int mUnderlineSize;

		public int mUnderlineOffset;
	}
}
