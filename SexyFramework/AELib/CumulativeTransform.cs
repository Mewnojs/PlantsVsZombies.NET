using System;
using Sexy.Misc;

namespace Sexy.AELib
{
	public class CumulativeTransform : IDisposable
	{
		public CumulativeTransform()
		{
			this.mTrans = new SexyTransform2D(true);
		}

		public CumulativeTransform(CumulativeTransform rhs)
		{
			this.CopyFrom(rhs);
		}

		public virtual void Dispose()
		{
		}

		public void CopyFrom(CumulativeTransform other)
		{
			this.mOpacity = other.mOpacity;
			this.mForceAdditive = other.mForceAdditive;
			this.mTrans = other.mTrans;
		}

		public void Reset()
		{
			this.mOpacity = 1f;
			this.mForceAdditive = false;
			this.mTrans.LoadIdentity();
		}

		public float mOpacity = 1f;

		public bool mForceAdditive;

		public SexyTransform2D mTrans;
	}
}
