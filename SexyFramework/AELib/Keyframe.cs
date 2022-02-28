using System;
using System.Collections.Generic;

namespace Sexy.AELib
{
	public class Keyframe : IComparable
	{
		public Keyframe()
		{
		}

		public Keyframe(Keyframe rhs)
		{
			this.mFrame = rhs.mFrame;
			this.mValue1 = rhs.mValue1;
			this.mValue2 = rhs.mValue2;
		}

		public Keyframe(int frame, float v1, float v2)
		{
			this.mFrame = frame;
			this.mValue1 = v1;
			this.mValue2 = v2;
		}

		public Keyframe(int frame, float v1)
		{
			this.mFrame = frame;
			this.mValue1 = v1;
			this.mValue2 = 0f;
		}

		public int CompareTo(object obj)
		{
			Keyframe keyframe = obj as Keyframe;
			if (keyframe != null)
			{
				return this.mFrame - keyframe.mFrame;
			}
			throw new ArgumentException("object is not a Keyframe.");
		}

		public int mFrame;

		public float mValue1;

		public float mValue2;

		public class KeyFrameSort : Comparer<Keyframe>
		{
			public override int Compare(Keyframe x, Keyframe y)
			{
				if (x.mFrame < y.mFrame)
				{
					return -1;
				}
				if (x.mFrame > y.mFrame)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
