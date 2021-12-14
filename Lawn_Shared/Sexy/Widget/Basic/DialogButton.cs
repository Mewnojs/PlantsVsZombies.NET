using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public/*internal*/ class DialogButton : ButtonWidget
	{
		public DialogButton(Image theComponentImage, int theId, ButtonListener theListener) : base(theId, theListener)
		{
			mComponentImage = theComponentImage;
			mTextOffsetX = (mTextOffsetY = 0);
			mTranslateX = (mTranslateY = 1);
			mDoFinger = true;
			SetColors(GlobalMembersDialogButton.gDialogButtonColors, 7);
		}

		public override void Draw(Graphics g)
		{
			if (mBtnNoDraw)
			{
				return;
			}
			if (mComponentImage == null)
			{
				base.Draw(g);
				return;
			}
			if (mFont == null && mLabel.length() > 0)
			{
				return;
			}
			bool flag = IsButtonDown();
			if (mNormalRect.mWidth == 0)
			{
				if (flag)
				{
					g.Translate(mTranslateX, mTranslateY);
				}
				g.DrawImageBox(new TRect(0, 0, mWidth, mHeight), mComponentImage);
			}
			else
			{
				if (mDisabled && mDisabledRect.mWidth > 0 && mDisabledRect.mHeight > 0)
				{
					g.DrawImageBox(mDisabledRect, new TRect(0, 0, mWidth, mHeight), mComponentImage);
				}
				else if (IsButtonDown())
				{
					g.DrawImageBox(mDownRect, new TRect(0, 0, mWidth, mHeight), mComponentImage);
				}
				else if (mOverAlpha > 0.0)
				{
					if (mOverAlpha < 1.0)
					{
						g.DrawImageBox(mNormalRect, new TRect(0, 0, mWidth, mHeight), mComponentImage);
					}
					g.SetColorizeImages(true);
					g.SetColor(new Color(255, 255, 255, (int)(mOverAlpha * 255.0)));
					g.DrawImageBox(mOverRect, new TRect(0, 0, mWidth, mHeight), mComponentImage);
					g.SetColorizeImages(false);
				}
				else if (mIsOver)
				{
					g.DrawImageBox(mOverRect, new TRect(0, 0, mWidth, mHeight), mComponentImage);
				}
				else
				{
					g.DrawImageBox(mNormalRect, new TRect(0, 0, mWidth, mHeight), mComponentImage);
				}
				if (flag)
				{
					g.Translate(mTranslateX, mTranslateY);
				}
			}
			if (mFont != null)
			{
				g.SetFont(mFont);
				if (mIsOver)
				{
					g.SetColor(mColors[1]);
				}
				else
				{
					g.SetColor(mColors[0]);
				}
				int num = (mWidth - mFont.StringWidth(mLabel)) / 2;
				int num2 = (mHeight + mFont.GetAscent() - mFont.GetAscentPadding() - mFont.GetAscent() / 6 - 1) / 2;
				g.DrawString(mLabel, num + mTextOffsetX, num2 + mTextOffsetY);
			}
			if (flag)
			{
				g.Translate(-mTranslateX, -mTranslateY);
			}
		}

		public Image mComponentImage;

		public int mTranslateX;

		public int mTranslateY;

		public int mTextOffsetX;

		public int mTextOffsetY;
	}
}
