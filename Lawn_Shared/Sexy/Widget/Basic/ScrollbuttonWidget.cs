using System;

namespace Sexy
{
	internal class ScrollbuttonWidget : ButtonWidget
	{
		public ScrollbuttonWidget(int theId, ButtonListener theButtonListener) : this(theId, theButtonListener, 0)
		{
		}

		public ScrollbuttonWidget(int theId, ButtonListener theButtonListener, int theType) : base(theId, theButtonListener)
		{
			this.mHorizontal = false;
			this.mType = theType;
		}

		public override void Draw(Graphics g)
		{
			if (this.mButtonImage == null && this.mDownImage == null)
			{
				int num = 0;
				g.SetColor(this.GetColor(5));
				g.FillRect(0, 0, this.mWidth, this.mHeight);
				if (this.mIsDown && this.mIsOver && !this.mDisabled)
				{
					num = 1;
					g.SetColor(this.GetColor(4));
					g.DrawRect(0, 0, this.mWidth - 1, this.mHeight - 1);
				}
				else
				{
					g.SetColor(this.GetColor(3));
					g.FillRect(1, 1, this.mWidth - 2, 1);
					g.FillRect(1, 1, 1, this.mHeight - 2);
					g.SetColor(this.GetColor(2));
					g.FillRect(0, this.mHeight - 1, this.mWidth, 1);
					g.FillRect(this.mWidth - 1, 0, 1, this.mHeight);
					g.SetColor(this.GetColor(4));
					g.FillRect(1, this.mHeight - 2, this.mWidth - 2, 1);
					g.FillRect(this.mWidth - 2, 1, 1, this.mHeight - 2);
				}
				if (!this.mDisabled)
				{
					g.SetColor(this.GetColor(2));
				}
				else
				{
					g.SetColor(this.GetColor(4));
				}
				if (this.mHorizontal || this.mType == 3 || this.mType == 4)
				{
					for (int i = 0; i < 4; i++)
					{
						if (this.mId == 0 || this.mType == 3)
						{
							g.FillRect(i + (this.mWidth - 4) / 2 + num, this.mHeight / 2 - i - 1 + num, 1, 1 + i * 2);
						}
						else
						{
							g.FillRect(3 - i + (this.mWidth - 4) / 2 + num, this.mHeight / 2 - i - 1 + num, 1, 1 + i * 2);
						}
					}
					return;
				}
				for (int j = 0; j < 4; j++)
				{
					if (this.mId == 0 || this.mType == 1)
					{
						g.FillRect(this.mWidth / 2 - j - 1 + num, j + (this.mHeight - 4) / 2 + num, 1 + j * 2, 1);
					}
					else
					{
						g.FillRect(this.mWidth / 2 - j - 1 + num, 3 - j + (this.mHeight - 4) / 2 + num, 1 + j * 2, 1);
					}
				}
				return;
			}
			else
			{
				int num2 = 0;
				if (this.mType > 0)
				{
					num2 = this.mType - 1;
					if (num2 > 2 && this.mButtonImage.mNumCols <= 2)
					{
						num2 -= 2;
					}
				}
				else
				{
					if (this.mHorizontal && this.mButtonImage.mNumCols > 2)
					{
						num2 += 2;
					}
					if (this.mId == 1)
					{
						num2++;
					}
				}
				if (this.mIsDown && this.mIsOver && !this.mDisabled)
				{
					g.DrawImageCel(this.mDownImage, 0, 0, num2);
					return;
				}
				if (this.mDisabled && this.mDisabledImage != null)
				{
					g.DrawImageCel(this.mDisabledImage, 0, 0, num2);
					return;
				}
				g.DrawImageCel(this.mButtonImage, 0, 0, num2);
				return;
			}
		}

		public bool mHorizontal;

		public int mType;
	}
}
