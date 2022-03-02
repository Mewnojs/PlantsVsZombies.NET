using System;
using Microsoft.Xna.Framework;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class Waypoint
	{
		public void CopyFrom(Waypoint rhs)
		{
			this.mLinear = rhs.mLinear;
			this.mControl1 = rhs.mControl1;
			this.mControl2 = rhs.mControl2;
			this.mPoint = rhs.mPoint;
			this.mTime = rhs.mTime;
			this.mFrame = rhs.mFrame;
		}

		public void Serialize(SexyBuffer b)
		{
			b.WriteBoolean(this.mLinear);
			b.WriteFloat(this.mTime);
			b.WriteLong(this.mFrame);
			b.WriteFloat(this.mControl1.X);
			b.WriteFloat(this.mControl1.Y);
			b.WriteFloat(this.mControl2.X);
			b.WriteFloat(this.mControl2.Y);
			b.WriteFloat(this.mPoint.X);
			b.WriteFloat(this.mPoint.Y);
		}

		public void Deserialize(SexyBuffer b)
		{
			this.mLinear = b.ReadBoolean();
			this.mTime = b.ReadFloat();
			this.mFrame = (int)b.ReadLong();
			this.mControl1.X = b.ReadFloat();
			this.mControl1.Y = b.ReadFloat();
			this.mControl2.X = b.ReadFloat();
			this.mControl2.Y = b.ReadFloat();
			this.mPoint.X = b.ReadFloat();
			this.mPoint.Y = b.ReadFloat();
		}

		public bool mLinear;

		public Vector2 mControl1 = default(Vector2);

		public Vector2 mControl2 = default(Vector2);

		public Vector2 mPoint = default(Vector2);

		public float mTime;

		public int mFrame;
	}
}
