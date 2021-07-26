using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct SexyColor
	{
		public int mRed
		{
			get
			{
				return (int)this.Color.R;
			}
			set
			{
				this.Color.R = (byte)value;
			}
		}

		public int mGreen
		{
			get
			{
				return (int)this.Color.G;
			}
			set
			{
				this.Color.G = (byte)value;
			}
		}

		public int mBlue
		{
			get
			{
				return (int)this.Color.B;
			}
			set
			{
				this.Color.B = (byte)value;
			}
		}

		public int mAlpha
		{
			get
			{
				return (int)this.Color.A;
			}
			set
			{
				float scale = (float)value / 255f;
				this.Color *= scale;
				this.Color.A = (byte)value;
			}
		}

		public static SexyColor Black
		{
			get
			{
				return SexyColor.mBlack;
			}
		}

		public static SexyColor White
		{
			get
			{
				return SexyColor.mWhite;
			}
		}

		public static SexyColor Premultiply(SexyColor col)
		{
			col.mRed = (int)((float)(col.mRed * col.mAlpha) / 255f);
			col.mGreen = (int)((float)(col.mGreen * col.mAlpha) / 255f);
			col.mBlue = (int)((float)(col.mBlue * col.mAlpha) / 255f);
			return col;
		}

		public void PremultiplyAlpha()
		{
			this.mRed = (int)((float)(this.mRed * this.mAlpha) / 255f);
			this.mGreen = (int)((float)(this.mGreen * this.mAlpha) / 255f);
			this.mBlue = (int)((float)(this.mBlue * this.mAlpha) / 255f);
		}

		public SexyColor(int theRed, int theGreen, int theBlue)
		{
			this.Color = new Color(theRed, theGreen, theBlue);
		}

		public SexyColor(int theRed, int theGreen, int theBlue, int theAlpha)
		{
			this = new SexyColor(theRed, theGreen, theBlue, theAlpha, true);
		}

		public SexyColor(int theRed, int theGreen, int theBlue, int theAlpha, bool premultiply)
		{
			this.Color = new Color(theRed, theGreen, theBlue, theAlpha);
			if (premultiply)
			{
				this.Color = Color.Multiply(this.Color, (float)theAlpha / 255f);
				this.Color.A = (byte)theAlpha;
			}
		}

		public SexyColor(string theElements)
		{
			this.Color = new Color((int)theElements.get_Chars(0), (int)theElements.get_Chars(1), (int)theElements.get_Chars(2), 255);
		}

		public SexyColor(Color theColor)
		{
			this.Color = theColor;
		}

		public int this[int theIdx]
		{
			get
			{
				switch (theIdx)
				{
				case 0:
					return (int)this.Color.R;
				case 1:
					return (int)this.Color.G;
				case 2:
					return (int)this.Color.B;
				case 3:
					return (int)this.Color.A;
				default:
					return 0;
				}
			}
		}

		public static bool operator ==(SexyColor a, SexyColor b)
		{
			return a.Color == b.Color;
		}

		public static bool operator !=(SexyColor a, SexyColor b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			return obj is SexyColor && this.Color == ((SexyColor)obj).Color;
		}

		public override int GetHashCode()
		{
			return this.Color.GetHashCode();
		}

		public override string ToString()
		{
			return this.Color.ToString();
		}

		public static implicit operator SexyColor(Color color)
		{
			return new SexyColor
			{
				Color = color
			};
		}

		public static implicit operator Color(SexyColor aColor)
		{
			return aColor.Color;
		}

		public static SexyColor FromColor(Color c)
		{
			return new SexyColor(c);
		}

		internal void CopyFrom(Color c)
		{
			this.Color = c;
		}

		public Color Color;

		private static SexyColor mWhite = new SexyColor(Color.White);

		private static SexyColor mBlack = new SexyColor(Color.Black);
	}
}
