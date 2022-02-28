using System;

namespace Sexy.GraphicsLib
{
	public struct SexyVertex2D : SexyVertex
	{
		public SexyVertex2D(float theX, float theY)
		{
			this.x = theX;
			this.y = theY;
			this.z = 0f;
			this.u = 0f;
			this.v = 0f;
			this.rhw = 1f;
			this.color = SexyColor.Zero;
			this.specular = 0U;
		}

		public SexyVertex2D(float theX, float theY, float theU, float theV)
		{
			this.x = theX;
			this.y = theY;
			this.u = theU;
			this.v = theV;
			this.z = 0f;
			this.rhw = 1f;
			this.color = SexyColor.Zero;
			this.specular = 0U;
		}

		public SexyVertex2D(float theX, float theY, float theU, float theV, uint theColor)
		{
			this.x = theX;
			this.y = theY;
			this.u = theU;
			this.v = theV;
			this.color = new SexyColor((int)theColor);
			this.z = 0f;
			this.rhw = 1f;
			this.specular = 0U;
		}

		public SexyVertex2D(float theX, float theY, float theZ, float theU, float theV, uint theColor)
		{
			this.x = theX;
			this.y = theY;
			this.z = theZ;
			this.u = theU;
			this.v = theV;
			this.color = new SexyColor((int)theColor);
			this.z = 0f;
			this.rhw = 1f;
			this.specular = 0U;
		}

		public float x;

		public float y;

		public float z;

		public float rhw;

		public SexyColor color;

		public uint specular;

		public float u;

		public float v;

		public static readonly int FVF = 452;
	}
}
