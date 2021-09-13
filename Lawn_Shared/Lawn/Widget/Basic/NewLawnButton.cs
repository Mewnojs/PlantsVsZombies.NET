using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class NewLawnButton : DialogButton
	{
		public NewLawnButton(Image theComponentImage, int theId, ButtonListener theListener) : base(theComponentImage, theId, theListener)
		{
			this.mHiliteFont = null;
			this.mTextDownOffsetX = 0;
			this.mTextDownOffsetY = 0;
			this.mButtonOffsetX = 0;
			this.mButtonOffsetY = 0;
			this.mUsePolygonShape = false;
			this.SetColor(5, SexyColor.White);
		}

		public override void Draw(Graphics g)
		{
			if (this.mBtnNoDraw)
			{
				return;
			}
			bool flag = this.mIsDown && this.mIsOver && !this.mDisabled;
			flag ^= this.mInverted;
			int num = this.mTextOffsetX + this.mTranslateX;
			int num2 = this.mTextOffsetY + this.mTranslateY;
			if (this.mFont != null)
			{
				if (this.mLabelJustify == 0)
				{
					num += (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2;
				}
				else if (this.mLabelJustify == 1)
				{
					num += this.mWidth - this.mFont.StringWidth(this.mLabel);
				}
				num2 += (this.mHeight + this.mFont.GetAscent() - this.mFont.GetAscent() / 6 - 1) / 2;
			}
			if (!flag)
			{
				g.SetColorizeImages(true);
				g.SetColor(this.mColors[5]);
				if (this.mDisabled && base.HaveButtonImage(this.mDisabledImage, this.mDisabledRect))
				{
					this.DrawButtonImage(g, this.mDisabledImage, this.mDisabledRect, this.mButtonOffsetX, this.mButtonOffsetY);
				}
				else if (this.mOverAlpha > 0.0 && base.HaveButtonImage(this.mOverImage, this.mOverRect))
				{
					if (base.HaveButtonImage(this.mButtonImage, this.mNormalRect) && this.mOverAlpha < 1.0)
					{
						this.DrawButtonImage(g, this.mButtonImage, this.mNormalRect, this.mButtonOffsetX, this.mButtonOffsetY);
					}
					SexyColor aColor = g.GetColor();
					aColor.mAlpha = (int)(this.mOverAlpha * 255.0);
					g.SetColor(aColor);
					this.DrawButtonImage(g, this.mOverImage, this.mOverRect, this.mButtonOffsetX, this.mButtonOffsetY);
				}
				else if ((this.mIsOver || this.mIsDown) && base.HaveButtonImage(this.mOverImage, this.mOverRect))
				{
					this.DrawButtonImage(g, this.mOverImage, this.mOverRect, this.mButtonOffsetX, this.mButtonOffsetY);
				}
				else if (base.HaveButtonImage(this.mButtonImage, this.mNormalRect))
				{
					this.DrawButtonImage(g, this.mButtonImage, this.mNormalRect, this.mButtonOffsetX, this.mButtonOffsetY);
				}
				g.SetColorizeImages(false);
				if (this.mIsOver)
				{
					if (this.mHiliteFont != null)
					{
						g.SetFont(this.mHiliteFont);
					}
					else
					{
						g.SetFont(this.mFont);
					}
					g.SetColor(this.mColors[1]);
				}
				else
				{
					g.SetFont(this.mFont);
					g.SetColor(this.mColors[0]);
				}
				g.DrawString(this.mLabel, num, num2);
				return;
			}
			g.SetColorizeImages(true);
			g.SetColor(this.mColors[5]);
			if (base.HaveButtonImage(this.mDownImage, this.mDownRect))
			{
				this.DrawButtonImage(g, this.mDownImage, this.mDownRect, this.mButtonOffsetX + this.mTranslateX, this.mButtonOffsetY + this.mTranslateY);
			}
			else if (base.HaveButtonImage(this.mOverImage, this.mOverRect))
			{
				this.DrawButtonImage(g, this.mOverImage, this.mOverRect, this.mButtonOffsetX + this.mTranslateX, this.mButtonOffsetY + this.mTranslateY);
			}
			else if (base.HaveButtonImage(this.mButtonImage, this.mNormalRect))
			{
				this.DrawButtonImage(g, this.mButtonImage, this.mNormalRect, this.mButtonOffsetX + this.mTranslateX, this.mButtonOffsetY + this.mTranslateY);
			}
			g.SetColorizeImages(false);
			if (this.mHiliteFont != null)
			{
				g.SetFont(this.mHiliteFont);
			}
			else
			{
				g.SetFont(this.mFont);
			}
			g.SetColor(this.mColors[1]);
			g.DrawString(this.mLabel, num + this.mTextDownOffsetX, num2 + this.mTextDownOffsetY);
		}

		public override bool IsPointVisible(int x, int y)
		{
			if (!this.mUsePolygonShape)
			{
				return base.IsPointVisible(x, y);
			}
			SexyVector2 theCheckPoint = new SexyVector2((float)x, (float)y);
			return TodCommon.TodIsPointInPolygon(this.mPolygonShape, 4, theCheckPoint);
		}

		public void SetLabel(string theLabel)
		{
			this.mLabel = TodStringFile.TodStringTranslate(theLabel);
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
