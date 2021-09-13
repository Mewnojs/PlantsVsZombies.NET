using System;
using Microsoft.Xna.Framework;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class LawnStoneButton : DialogButton
	{
		public LawnStoneButton(Image theComponentImage, int theId, ButtonListener theListener) : base(theComponentImage, theId, theListener)
		{
			this.mFont = Resources.FONT_DWARVENTODCRAFT15;
		}

		public override void Draw(Graphics g)
		{
			if (this.mBtnNoDraw)
			{
				return;
			}
			bool flag = this.mIsDown && this.mIsOver && !this.mDisabled;
			flag ^= this.mInverted;
			GameButton.DrawStoneButton(g, 0, 0, this.mWidth, this.mHeight, flag, this.mIsOver, this.mLabel, this.mFont, this.mFontScale);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.CalculateTextScale();
		}

		public override void Resize(TRect theRect)
		{
			base.Resize(theRect);
			this.CalculateTextScale();
		}

		private void CalculateTextScale()
		{
			this.mFontScale = 1f;
			if (this.mFont != null)
			{
				Vector2 vector = this.mFont.MeasureString(this.mLabel);
				int num = (int)((float)this.mWidth - Constants.S * 30f);
				if (vector.X > (float)num)
				{
					this.mFontScale = (float)num / vector.X;
				}
			}
		}

		public void SetLabel(string theLabel)
		{
			this.mLabel = TodStringFile.TodStringTranslate(theLabel);
		}

		public override string mLabel
		{
			get
			{
				return base.mLabel;
			}
			set
			{
				base.mLabel = value;
				this.CalculateTextScale();
			}
		}

		private float mFontScale = 1f;
	}
}
