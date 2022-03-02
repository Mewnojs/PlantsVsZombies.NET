using System;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class ColorKey
	{
		public ColorKey()
		{
		}

		public ColorKey(SexyColor c)
		{
			this.mColor = c;
		}

		public virtual void Dispose()
		{
		}

		public SexyColor GetInterpolatedColor(ColorKey next_color, float pct)
		{
			SexyColor result = new SexyColor(this.mColor);
			result.mRed += (int)((float)(next_color.mColor.mRed - this.mColor.mRed) * pct);
			result.mGreen += (int)((float)(next_color.mColor.mGreen - this.mColor.mGreen) * pct);
			result.mBlue += (int)((float)(next_color.mColor.mBlue - this.mColor.mBlue) * pct);
			result.mAlpha += (int)((float)(next_color.mColor.mAlpha - this.mColor.mAlpha) * pct);
			result.mRed = Math.Max(Math.Min(255, result.mRed), 0);
			result.mGreen = Math.Max(Math.Min(255, result.mGreen), 0);
			result.mBlue = Math.Max(Math.Min(255, result.mBlue), 0);
			result.mAlpha = Math.Max(Math.Min(255, result.mAlpha), 0);
			return result;
		}

		public SexyColor GetColor()
		{
			return this.mColor;
		}

		public void SetColor(SexyColor c)
		{
			this.mColor = c;
		}

		public void Serialize(SexyBuffer b)
		{
			b.WriteLong(this.mColor.ToInt());
		}

		public void Deserialize(SexyBuffer b)
		{
			this.mColor = new SexyColor((int)b.ReadLong());
		}

		protected SexyColor mColor;
	}
}
