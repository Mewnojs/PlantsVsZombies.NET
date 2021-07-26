using System;
using Sexy;

namespace Lawn
{
	internal class MotionTrailFrame
	{
		public void SaveToFile(Buffer b)
		{
			b.WriteFloat(this.mPosX);
			b.WriteFloat(this.mPosY);
			b.WriteFloat(this.mAnimTime);
		}

		public void LoadFromFile(Buffer b)
		{
			this.mPosX = b.ReadFloat();
			this.mPosY = b.ReadFloat();
			this.mAnimTime = b.ReadFloat();
		}

		public float mPosX;

		public float mPosY;

		public float mAnimTime;
	}
}
