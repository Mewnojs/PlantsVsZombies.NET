using System;
using Microsoft.Xna.Framework;

namespace Sexy.TodLib
{
	internal struct DrawCall
	{
		public void SetTransform(ReanimatorTransform transform)
		{
			this.mPosition = new Vector2(transform.mTransX, transform.mTransY);
			this.mRotation = 0.0;
			this.mScale = new Vector2(transform.mScaleX, transform.mScaleY);
		}

		public TRect mClipRect;

		public SexyColor mColor;

		public TRect mSrcRect;

		public Vector2 mPosition;

		public double mRotation;

		public Vector2 mScale;
	}
}
