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
				return Color.R;
			}
			set
			{
				Color.R = (byte)value;
			}
		}

		public int mGreen
		{
			get
			{
				return Color.G;
			}
			set
			{
				Color.G = (byte)value;
			}
		}

		public int mBlue
		{
			get
			{
				return Color.B;
			}
			set
			{
				Color.B = (byte)value;
			}
		}

		public int mAlpha
		{
			get
			{
				return Color.A;
			}
			set
			{
				float scale = value / 255f;
				Color *= scale;
				Color.A = (byte)value;
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
			col.mRed = (int)(col.mRed * col.mAlpha / 255f);
			col.mGreen = (int)(col.mGreen * col.mAlpha / 255f);
			col.mBlue = (int)(col.mBlue * col.mAlpha / 255f);
			return col;
		}

		public void PremultiplyAlpha()
		{
			mRed = (int)(mRed * mAlpha / 255f);
			mGreen = (int)(mGreen * mAlpha / 255f);
			mBlue = (int)(mBlue * mAlpha / 255f);
		}

		public SexyColor(int theRed, int theGreen, int theBlue)
		{
			Color = new Color(theRed, theGreen, theBlue);
		}

		public SexyColor(int theRed, int theGreen, int theBlue, int theAlpha)
		{
			this = new SexyColor(theRed, theGreen, theBlue, theAlpha, true);
		}

		public SexyColor(int theRed, int theGreen, int theBlue, int theAlpha, bool premultiply)
		{
			Color = new Color(theRed, theGreen, theBlue, theAlpha);
			if (premultiply)
			{
				Color = Color.Multiply(Color, theAlpha / 255f);
				Color.A = (byte)theAlpha;
			}
		}

		public SexyColor(string theElements)
		{
			Color = new Color(theElements[0], theElements[1], theElements[2], 255);
		}

		public SexyColor(Color theColor)
		{
			Color = theColor;
		}

		public int this[int theIdx]
		{
			get
			{
				switch (theIdx)
				{
				case 0:
					return Color.R;
				case 1:
					return Color.G;
				case 2:
					return Color.B;
				case 3:
					return Color.A;
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
			return obj is SexyColor && Color == ((SexyColor)obj).Color;
		}

		public override int GetHashCode()
		{
			return Color.GetHashCode();
		}

		public override string ToString()
		{
			return Color.ToString();
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
			Color = c;
		}

		public Color Color;

		private static SexyColor mWhite = new SexyColor(Color.White);

		private static SexyColor mBlack = new SexyColor(Color.Black);
	}
}
