using System;

namespace Sexy.AELib
{
	public class CompLayer
	{
		public CompLayer()
		{
		}

		public CompLayer(Layer l, int start_frame, int dur, int offs)
		{
			this.mSource = l;
			this.mStartFrameOnComp = start_frame;
			this.mDuration = dur;
			this.mLayerOffsetStart = offs;
		}

		public CompLayer(CompLayer other)
		{
			this.CopyFrom(other);
		}

		public void CopyFrom(CompLayer other)
		{
			this.mSource = other.mSource.Duplicate();
			this.mStartFrameOnComp = other.mStartFrameOnComp;
			this.mDuration = other.mDuration;
			this.mLayerOffsetStart = other.mLayerOffsetStart;
		}

		public Layer mSource;

		public int mStartFrameOnComp;

		public int mDuration;

		public int mLayerOffsetStart;
	}
}
