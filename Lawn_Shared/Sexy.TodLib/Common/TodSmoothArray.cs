using System;

namespace Sexy.TodLib
{
	internal class TodSmoothArray
	{
		public bool SaveToFile(Buffer b)
		{
			b.WriteLong(this.mItem);
			b.WriteFloat(this.mWeight);
			b.WriteFloat(this.mLastPicked);
			b.WriteFloat(this.mSecondLastPicked);
			return true;
		}

		public bool LoadFromFile(Buffer b)
		{
			this.mItem = b.ReadLong();
			this.mWeight = b.ReadFloat();
			this.mLastPicked = b.ReadFloat();
			this.mSecondLastPicked = b.ReadFloat();
			return true;
		}

		public int mItem;

		public float mWeight;

		public float mLastPicked;

		public float mSecondLastPicked;
	}
}
