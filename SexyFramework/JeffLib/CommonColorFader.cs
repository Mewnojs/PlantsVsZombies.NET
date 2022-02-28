using System;

namespace JeffLib
{
	public class CommonColorFader
	{
		public CommonColorFader()
		{
			this.mForward = true;
			this.mEnabled = true;
			this.mDuration = -1;
			this.mRedChange = (this.mGreenChange = (this.mBlueChange = (this.mAlphaChange = 0f)));
		}

		public bool Update()
		{
			if (!this.mEnabled)
			{
				return false;
			}
			int num = 0;
			if (this.mForward)
			{
				this.mColor.mRed += this.mRedChange;
				this.mColor.mGreen += this.mGreenChange;
				this.mColor.mBlue += this.mBlueChange;
				this.mColor.mAlpha += this.mAlphaChange;
				if (Common._ATLIMIT(this.mColor.mRed, this.mMaxColor.mRed, this.mRedChange))
				{
					num++;
					this.mColor.mRed = this.mMaxColor.mRed;
				}
				if (Common._ATLIMIT(this.mColor.mGreen, this.mMaxColor.mGreen, this.mGreenChange))
				{
					num++;
					this.mColor.mGreen = this.mMaxColor.mGreen;
				}
				if (Common._ATLIMIT(this.mColor.mBlue, this.mMaxColor.mBlue, this.mBlueChange))
				{
					num++;
					this.mColor.mBlue = this.mMaxColor.mBlue;
				}
				if (Common._ATLIMIT(this.mColor.mAlpha, this.mMaxColor.mAlpha, this.mAlphaChange))
				{
					num++;
					this.mColor.mAlpha = this.mMaxColor.mAlpha;
				}
			}
			else
			{
				this.mColor.mRed -= this.mRedChange;
				this.mColor.mGreen -= this.mGreenChange;
				this.mColor.mBlue -= this.mBlueChange;
				this.mColor.mAlpha -= this.mAlphaChange;
				if (Common._ATLIMIT(this.mColor.mRed, this.mMinColor.mRed, -this.mRedChange))
				{
					num++;
					this.mColor.mRed = this.mMinColor.mRed;
				}
				if (Common._ATLIMIT(this.mColor.mGreen, this.mMinColor.mGreen, -this.mGreenChange))
				{
					num++;
					this.mColor.mGreen = this.mMinColor.mGreen;
				}
				if (Common._ATLIMIT(this.mColor.mBlue, this.mMinColor.mBlue, -this.mBlueChange))
				{
					num++;
					this.mColor.mBlue = this.mMinColor.mBlue;
				}
				if (Common._ATLIMIT(this.mColor.mAlpha, this.mMinColor.mAlpha, -this.mAlphaChange))
				{
					num++;
					this.mColor.mAlpha = this.mMinColor.mAlpha;
				}
			}
			if (num != 4)
			{
				return false;
			}
			if (this.mDuration > 0 && --this.mDuration <= 0)
			{
				this.mEnabled = false;
				return true;
			}
			this.mForward = !this.mForward;
			return true;
		}

		public void SetSpeed(int s)
		{
			this.mRedChange = (this.mGreenChange = (this.mBlueChange = (this.mAlphaChange = (float)s)));
		}

		public void FadeOverTime(int frames)
		{
			this.mRedChange = (this.mMaxColor.mRed - this.mMinColor.mRed) / (float)frames;
			this.mGreenChange = (this.mMaxColor.mGreen - this.mMinColor.mGreen) / (float)frames;
			this.mBlueChange = (this.mMaxColor.mBlue - this.mMinColor.mBlue) / (float)frames;
			this.mAlphaChange = (this.mMaxColor.mAlpha - this.mMinColor.mAlpha) / (float)frames;
		}

		public void AlphaFadeIn(int arate)
		{
			this.mEnabled = true;
			this.mForward = true;
			this.mDuration = 1;
			this.mAlphaChange = (float)Math.Abs(arate);
			this.mRedChange = (this.mGreenChange = (this.mBlueChange = 0f));
			this.mColor.mRed = (this.mColor.mGreen = (this.mColor.mBlue = 255f));
			this.mColor.mAlpha = 0f;
			this.mMinColor = this.mColor;
			this.mMaxColor.mRed = (this.mMaxColor.mGreen = (this.mMaxColor.mBlue = (this.mMaxColor.mAlpha = 255f)));
		}

		public void AlphaFadeOut(int arate)
		{
			this.mEnabled = true;
			this.mForward = false;
			this.mDuration = 1;
			this.mAlphaChange = (float)Math.Abs(arate);
			this.mRedChange = (this.mGreenChange = (this.mBlueChange = 0f));
			this.mColor.mRed = (this.mColor.mGreen = (this.mColor.mBlue = (this.mColor.mAlpha = 255f)));
			this.mMaxColor = this.mColor;
			this.mMinColor.mRed = (this.mMinColor.mGreen = (this.mMinColor.mBlue = 255f));
			this.mMinColor.mAlpha = 0f;
		}

		public FColor mColor = new FColor();

		public FColor mMaxColor = new FColor();

		public FColor mMinColor = new FColor();

		public bool mForward;

		public bool mEnabled;

		public float mRedChange;

		public float mGreenChange;

		public float mBlueChange;

		public float mAlphaChange;

		public int mDuration;
	}
}
