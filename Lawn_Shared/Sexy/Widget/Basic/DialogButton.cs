using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public/*internal*/ class DialogButton : ButtonWidget
	{
		public DialogButton(Image theComponentImage, int theId, ButtonListener theListener) : base(theId, theListener)
		{
			this.mComponentImage = theComponentImage;
			this.mTextOffsetX = (this.mTextOffsetY = 0);
			this.mTranslateX = (this.mTranslateY = 1);
			this.mDoFinger = true;
			this.SetColors(GlobalMembersDialogButton.gDialogButtonColors, 7);
		}

		public override void Draw(Graphics g)
		{
			if (this.mBtnNoDraw)
			{
				return;
			}
			if (this.mComponentImage == null)
			{
				base.Draw(g);
				return;
			}
			if (this.mFont == null && this.mLabel.length() > 0)
			{
				return;
			}
			bool flag = this.IsButtonDown();
			if (this.mNormalRect.mWidth == 0)
			{
				if (flag)
				{
					g.Translate(this.mTranslateX, this.mTranslateY);
				}
				g.DrawImageBox(new TRect(0, 0, this.mWidth, this.mHeight), this.mComponentImage);
			}
			else
			{
				if (this.mDisabled && this.mDisabledRect.mWidth > 0 && this.mDisabledRect.mHeight > 0)
				{
					g.DrawImageBox(this.mDisabledRect, new TRect(0, 0, this.mWidth, this.mHeight), this.mComponentImage);
				}
				else if (this.IsButtonDown())
				{
					g.DrawImageBox(this.mDownRect, new TRect(0, 0, this.mWidth, this.mHeight), this.mComponentImage);
				}
				else if (this.mOverAlpha > 0.0)
				{
					if (this.mOverAlpha < 1.0)
					{
						g.DrawImageBox(this.mNormalRect, new TRect(0, 0, this.mWidth, this.mHeight), this.mComponentImage);
					}
					g.SetColorizeImages(true);
					g.SetColor(new Color(255, 255, 255, (int)(this.mOverAlpha * 255.0)));
					g.DrawImageBox(this.mOverRect, new TRect(0, 0, this.mWidth, this.mHeight), this.mComponentImage);
					g.SetColorizeImages(false);
				}
				else if (this.mIsOver)
				{
					g.DrawImageBox(this.mOverRect, new TRect(0, 0, this.mWidth, this.mHeight), this.mComponentImage);
				}
				else
				{
					g.DrawImageBox(this.mNormalRect, new TRect(0, 0, this.mWidth, this.mHeight), this.mComponentImage);
				}
				if (flag)
				{
					g.Translate(this.mTranslateX, this.mTranslateY);
				}
			}
			if (this.mFont != null)
			{
				g.SetFont(this.mFont);
				if (this.mIsOver)
				{
					g.SetColor(this.mColors[1]);
				}
				else
				{
					g.SetColor(this.mColors[0]);
				}
				int num = (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2;
				int num2 = (this.mHeight + this.mFont.GetAscent() - this.mFont.GetAscentPadding() - this.mFont.GetAscent() / 6 - 1) / 2;
				g.DrawString(this.mLabel, num + this.mTextOffsetX, num2 + this.mTextOffsetY);
			}
			if (flag)
			{
				g.Translate(-this.mTranslateX, -this.mTranslateY);
			}
		}

		public Image mComponentImage;

		public int mTranslateX;

		public int mTranslateY;

		public int mTextOffsetX;

		public int mTextOffsetY;
	}
}
