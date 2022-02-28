using System;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class SexyVertex3DLit : SexyVertex
	{
		public void MakeDefaultNormal()
		{
			SexyVector3 sexyVector = new SexyVector3(this.x, this.y, this.z);
			sexyVector = sexyVector.Normalize();
			this.nx = sexyVector.x;
			this.ny = sexyVector.y;
			this.nz = sexyVector.z;
		}

		public SexyVertex3DLit()
		{
		}

		public SexyVertex3DLit(SexyVector3 thePos, SexyVector3 theNormal)
		{
			this.x = thePos.x;
			this.y = thePos.y;
			this.z = thePos.z;
			this.nx = theNormal.x;
			this.ny = theNormal.y;
			this.nz = theNormal.z;
			this.color = 0U;
		}

		public SexyVertex3DLit(SexyVector3 thePos, SexyVector3 theNormal, float theU, float theV)
		{
			this.x = thePos.x;
			this.y = thePos.y;
			this.z = thePos.z;
			this.nx = theNormal.x;
			this.ny = theNormal.y;
			this.nz = theNormal.z;
			this.u = theU;
			this.v = theV;
			this.color = 0U;
		}

		public SexyVertex3DLit(SexyVector3 thePos, SexyVector3 theNormal, float theU, float theV, uint theColor)
		{
			this.x = thePos.x;
			this.y = thePos.y;
			this.z = thePos.z;
			this.nx = theNormal.x;
			this.ny = theNormal.y;
			this.nz = theNormal.z;
			this.u = theU;
			this.v = theV;
			this.color = theColor;
		}

		public SexyVertex3DLit(SexyVector3 thePos)
		{
			this.x = thePos.x;
			this.y = thePos.y;
			this.z = thePos.z;
			this.MakeDefaultNormal();
			this.color = 0U;
		}

		public SexyVertex3DLit(SexyVector3 thePos, float theU, float theV)
		{
			this.x = thePos.x;
			this.y = thePos.y;
			this.z = thePos.z;
			this.u = theU;
			this.v = theV;
			this.MakeDefaultNormal();
			this.color = 0U;
		}

		public SexyVertex3DLit(SexyVector3 thePos, float theU, float theV, uint theColor)
		{
			this.x = thePos.x;
			this.y = thePos.y;
			this.z = thePos.z;
			this.u = theU;
			this.v = theV;
			this.color = theColor;
			this.MakeDefaultNormal();
		}

		public float x;

		public float y;

		public float z;

		public float nx;

		public float ny;

		public float nz;

		public uint color;

		public float u;

		public float v;

		public static readonly int FVF = 338;
	}
}
