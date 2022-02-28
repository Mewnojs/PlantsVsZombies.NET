using System;

namespace Sexy.GraphicsLib
{
	public class SexyVertex3D : SexyVertex
	{
		public SexyVertex3D()
		{
		}

		public SexyVertex3D(float theX, float theY, float theZ)
		{
			this.x = theX;
			this.y = theY;
			this.z = theZ;
			this.color = 0U;
		}

		public SexyVertex3D(float theX, float theY, float theZ, float theU, float theV)
		{
			this.x = theX;
			this.y = theY;
			this.z = theZ;
			this.u = theU;
			this.v = theV;
			this.color = 0U;
		}

		public SexyVertex3D(float theX, float theY, float theZ, float theU, float theV, uint theColor)
		{
			this.x = theX;
			this.y = theY;
			this.z = theZ;
			this.u = theU;
			this.v = theV;
			this.color = theColor;
		}

		public float x;

		public float y;

		public float z;

		public uint color;

		public float u;

		public float v;

		public static readonly int FVF = 322;
	}
}
