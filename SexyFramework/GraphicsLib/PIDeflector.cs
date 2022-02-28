using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sexy.GraphicsLib
{
	public class PIDeflector : IDisposable
	{
		public virtual void Dispose()
		{
			this.mPos.Dispose();
			this.mActive.Dispose();
			this.mAngle.Dispose();
			foreach (PIValue2D pivalue2D in this.mPoints)
			{
				pivalue2D.Dispose();
			}
			this.mPoints.Clear();
			this.mCurPoints.Clear();
		}

		public string mName;

		public float mBounce;

		public float mHits;

		public float mThickness;

		public bool mVisible;

		public PIValue2D mPos = new PIValue2D();

		public PIValue mActive = new PIValue();

		public PIValue mAngle = new PIValue();

		public List<PIValue2D> mPoints = new List<PIValue2D>();

		public List<Vector2> mCurPoints = new List<Vector2>();
	}
}
