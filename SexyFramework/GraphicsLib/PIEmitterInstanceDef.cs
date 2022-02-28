using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public class PIEmitterInstanceDef : IDisposable
	{
		public PIEmitterInstanceDef()
		{
			this.mPosition = new PIValue2D();
			this.mValues = new PIValue[19];
			this.mPoints = new List<PIValue2D>();
			this.mFreeEmitterIndices = new List<int>();
			for (int i = 0; i < 19; i++)
			{
				this.mValues[i] = new PIValue();
			}
		}

		public virtual void Dispose()
		{
			this.mFreeEmitterIndices.Clear();
			this.mPosition.Dispose();
			for (int i = 0; i < 19; i++)
			{
				this.mValues[i].Dispose();
			}
			foreach (PIValue2D pivalue2D in this.mPoints)
			{
				pivalue2D.Dispose();
			}
		}

		public string mName;

		public int mFramesToPreload;

		public int mEmitterDefIdx;

		public int mEmitterGeom;

		public bool mEmitIn;

		public bool mEmitOut;

		public int mEmitAtPointsNum;

		public int mEmitAtPointsNum2;

		public bool mIsSuperEmitter;

		public List<int> mFreeEmitterIndices;

		public bool mInvertMask;

		public PIValue2D mPosition;

		public PIValue[] mValues;

		public List<PIValue2D> mPoints;

		public enum PIEmitterValue
		{
			VALUE_LIFE,
			VALUE_NUMBER,
			VALUE_SIZE_X,
			VALUE_VELOCITY,
			VALUE_WEIGHT,
			VALUE_SPIN,
			VALUE_MOTION_RAND,
			VALUE_BOUNCE,
			VALUE_ZOOM,
			VALUE_VISIBILITY,
			VALUE_TINT_STRENGTH,
			VALUE_EMISSION_ANGLE,
			VALUE_EMISSION_RANGE,
			VALUE_ACTIVE,
			VALUE_ANGLE,
			VALUE_XRADIUS,
			VALUE_YRADIUS,
			VALUE_SIZE_Y,
			VALUE_UNKNOWN4,
			NUM_VALUES
		}

		public enum PIEmitterGEOM
		{
			GEOM_POINT,
			GEOM_LINE,
			GEOM_ECLIPSE,
			GEOM_AREA,
			GEOM_CIRCLE
		}
	}
}
