using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public class PIBlocker : IDisposable
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
		}

		public string mName;

		public int mUseLayersBelowForBg;

		public PIValue2D mPos = new PIValue2D();

		public PIValue mActive = new PIValue();

		public PIValue mAngle = new PIValue();

		public List<PIValue2D> mPoints = new List<PIValue2D>();
	}
}
