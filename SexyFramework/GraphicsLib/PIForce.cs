using System;
using Microsoft.Xna.Framework;

namespace Sexy.GraphicsLib
{
	public class PIForce : IDisposable
	{
		public virtual void Dispose()
		{
			this.mPos.Dispose();
			this.mActive.Dispose();
			this.mAngle.Dispose();
			this.mStrength.Dispose();
			this.mWidth.Dispose();
			this.mHeight.Dispose();
			this.mDirection.Dispose();
		}

		public string mName;

		public bool mVisible;

		public PIValue2D mPos = new PIValue2D();

		public PIValue mStrength = new PIValue();

		public PIValue mDirection = new PIValue();

		public PIValue mActive = new PIValue();

		public PIValue mAngle = new PIValue();

		public PIValue mWidth = new PIValue();

		public PIValue mHeight = new PIValue();

		public Vector2[] mCurPoints = new Vector2[5];
	}
}
