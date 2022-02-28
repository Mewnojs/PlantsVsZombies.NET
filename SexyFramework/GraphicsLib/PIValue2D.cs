using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class PIValue2D : IDisposable
	{
		public PIValue2D()
		{
			this.mLastTime = -1f;
		}

		public virtual void Dispose()
		{
			this.mBezier.Dispose();
			this.mValuePoint2DVector.Clear();
		}

		public Vector2 GetValueAt(float theTime)
		{
			if (this.mLastTime == theTime)
			{
				return this.mLastPoint;
			}
			this.mLastTime = theTime;
			if (this.mValuePoint2DVector.Count == 1)
			{
				return this.mLastPoint = this.mValuePoint2DVector[0].mValue;
			}
			if (this.mBezier.IsInitialized())
			{
				return this.mLastPoint = this.mBezier.Evaluate(theTime);
			}
			for (int i = 1; i < this.mValuePoint2DVector.Count; i++)
			{
				PIValuePoint2D pivaluePoint2D = this.mValuePoint2DVector[i - 1];
				PIValuePoint2D pivaluePoint2D2 = this.mValuePoint2DVector[i];
				if ((theTime >= pivaluePoint2D.mTime && theTime <= pivaluePoint2D2.mTime) || i == this.mValuePoint2DVector.Count - 1)
				{
					return this.mLastPoint = pivaluePoint2D.mValue + (pivaluePoint2D2.mValue - pivaluePoint2D.mValue) * Math.Min(1f, (theTime - pivaluePoint2D.mTime) / (pivaluePoint2D2.mTime - pivaluePoint2D.mTime));
				}
			}
			return this.mLastPoint = new Vector2(0f, 0f);
		}

		public Vector2 GetVelocityAt(float theTime)
		{
			if (this.mLastVelocityTime == theTime)
			{
				return this.mLastVelocity;
			}
			this.mLastVelocityTime = theTime;
			if (this.mValuePoint2DVector.Count <= 1)
			{
				return new Vector2(0f, 0f);
			}
			if (this.mBezier.IsInitialized())
			{
				return this.mLastVelocity = this.mBezier.Velocity(theTime, false);
			}
			for (int i = 1; i < this.mValuePoint2DVector.Count; i++)
			{
				PIValuePoint2D pivaluePoint2D = this.mValuePoint2DVector[i - 1];
				PIValuePoint2D pivaluePoint2D2 = this.mValuePoint2DVector[i];
				if ((theTime >= pivaluePoint2D.mTime && theTime <= pivaluePoint2D2.mTime) || i == this.mValuePoint2DVector.Count - 1)
				{
					return this.mLastVelocity = pivaluePoint2D2.mValue - pivaluePoint2D.mValue;
				}
			}
			return this.mLastVelocity = new Vector2(0f, 0f);
		}

		public List<PIValuePoint2D> mValuePoint2DVector = new List<PIValuePoint2D>();

		public Bezier mBezier = new Bezier();

		public float mLastTime;

		public Vector2 mLastPoint = default(Vector2);

		public float mLastVelocityTime;

		public Vector2 mLastVelocity = default(Vector2);
	}
}
