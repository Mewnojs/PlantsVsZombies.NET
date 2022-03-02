using System;
using System.Collections.Generic;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class LineEmitterPoint
	{
		public virtual void Serialize(SexyBuffer b)
		{
			b.WriteFloat(this.mCurX);
			b.WriteFloat(this.mCurY);
			b.WriteLong(this.mKeyFramePoints.Count);
			for (int i = 0; i < this.mKeyFramePoints.Count; i++)
			{
				b.WriteLong(this.mKeyFramePoints[i].first);
				b.WriteLong(this.mKeyFramePoints[i].second.mX);
				b.WriteLong(this.mKeyFramePoints[i].second.mY);
			}
		}

		public virtual void Deserialize(SexyBuffer b)
		{
			this.mCurX = b.ReadFloat();
			this.mCurY = b.ReadFloat();
			int num = (int)b.ReadLong();
			this.mKeyFramePoints.Clear();
			for (int i = 0; i < num; i++)
			{
				int f = (int)b.ReadLong();
				int theX = (int)b.ReadLong();
				int theY = (int)b.ReadLong();
				this.mKeyFramePoints.Add(new PointKeyFrame(f, new SexyPoint(theX, theY)));
			}
		}

		public List<PointKeyFrame> mKeyFramePoints = new List<PointKeyFrame>();

		public float mCurX;

		public float mCurY;
	}
}
