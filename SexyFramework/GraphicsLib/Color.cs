using System;

namespace Sexy.GraphicsLib
{
	public struct SexyColor
	{
		public SexyColor(SexyColor color)
		{
			this.mRed = color.mRed;
			this.mGreen = color.mGreen;
			this.mBlue = color.mBlue;
			this.mAlpha = color.mAlpha;
		}

		public SexyColor(int theColor)
		{
			this.mAlpha = (theColor >> 24) & 255;
			this.mRed = (theColor >> 16) & 255;
			this.mGreen = (theColor >> 8) & 255;
			this.mBlue = theColor & 255;
			if (this.mAlpha == 0)
			{
				this.mAlpha = 255;
			}
		}

		public SexyColor(int theColor, int theAlpha)
		{
			this.mRed = (theColor >> 16) & 255;
			this.mGreen = (theColor >> 8) & 255;
			this.mBlue = theColor & 255;
			this.mAlpha = theAlpha;
		}

		public SexyColor(int theRed, int theGreen, int theBlue)
		{
			this.mRed = theRed;
			this.mGreen = theGreen;
			this.mBlue = theBlue;
			this.mAlpha = 255;
		}

		public SexyColor(int theRed, int theGreen, int theBlue, int theAlpha)
		{
			this.mRed = theRed;
			this.mGreen = theGreen;
			this.mBlue = theBlue;
			this.mAlpha = theAlpha;
		}

		public SexyColor(SexyRGBA theColor)
		{
			this.mRed = (int)theColor.r;
			this.mGreen = (int)theColor.g;
			this.mBlue = (int)theColor.b;
			this.mAlpha = (int)theColor.a;
		}

		public SexyColor(int[] theElements)
		{
			this.mRed = theElements[0];
			this.mGreen = theElements[1];
			this.mBlue = theElements[2];
			this.mAlpha = 255;
		}

		public override string ToString()
		{
			return string.Concat(new object[] { "(", this.mRed, ",", this.mGreen, ",", this.mBlue, ",", this.mAlpha, ")" });
		}

		public SexyColor Clone()
		{
			return new SexyColor(this);
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is SexyColor)
			{
				SexyColor color = (SexyColor)obj;
				return this.mRed == color.mRed && this.mBlue == color.mBlue && this.mGreen == color.mGreen && this.mAlpha == color.mAlpha;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public int ToInt()
		{
			return (this.mAlpha << 24) | (this.mRed << 16) | (this.mGreen << 8) | this.mBlue;
		}

		public SexyRGBA ToRGBA()
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(SexyColor theColor1, SexyColor theColor2)
		{
			if (theColor1 == null)
			{
				return theColor2 == null;
			}
			return theColor1.Equals(theColor2);
		}

		public static bool operator !=(SexyColor theColor1, SexyColor theColor2)
		{
			return !(theColor1 == theColor2);
		}

		public static SexyColor operator *(SexyColor theColor1, SexyColor theColor2)
		{
			return new SexyColor(theColor1.mRed * theColor2.mRed / 255, theColor1.mGreen * theColor2.mGreen / 255, theColor1.mBlue * theColor2.mBlue / 255, theColor1.mAlpha * theColor2.mAlpha / 255);
		}

		public static SexyColor operator *(SexyColor theColor1, float theAlphaPct)
		{
			return new SexyColor(theColor1.mRed, theColor1.mGreen, theColor1.mBlue, (int)((float)theColor1.mAlpha * theAlphaPct) / 255);
		}

		public void SetColor(int p, int p_2, int p_3, int p_4)
		{
			this.mRed = p;
			this.mGreen = p_2;
			this.mBlue = p_3;
			this.mAlpha = p_4;
		}

		public int mRed;

		public int mGreen;

		public int mBlue;

		public int mAlpha;

		public static SexyColor Zero = new SexyColor(0, 0, 0, 0);

		public static readonly SexyColor Black = new SexyColor(0, 0, 0);

		public static readonly SexyColor White = new SexyColor(255, 255, 255);

		public static readonly SexyColor Red = new SexyColor(255, 0, 0);

		public static readonly SexyColor Green = new SexyColor(0, 255, 0);

		public static readonly SexyColor Blue = new SexyColor(0, 0, 255);

		public static readonly SexyColor Yellow = new SexyColor(255, 255, 0);
	}
}
