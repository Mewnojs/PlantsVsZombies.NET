using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sexy.GraphicsLib
{
	public class PILayerDef : IDisposable
	{
		public virtual void Dispose()
		{
			this.mOffset.Dispose();
			this.mAngle.Dispose();
			foreach (PIEmitterInstanceDef piemitterInstanceDef in this.mEmitterInstanceDefVector)
			{
				piemitterInstanceDef.Dispose();
			}
			this.mEmitterInstanceDefVector.Clear();
		}

		public string mName;

		public List<PIEmitterInstanceDef> mEmitterInstanceDefVector = new List<PIEmitterInstanceDef>();

		public List<PIDeflector> mDeflectorVector = new List<PIDeflector>();

		public List<PIBlocker> mBlockerVector = new List<PIBlocker>();

		public List<PIForce> mForceVector = new List<PIForce>();

		public PIValue2D mOffset = new PIValue2D();

		public Vector2 mOrigOffset = default(Vector2);

		public PIValue mAngle = new PIValue();
	}
}
