using System;
using Sexy;

namespace Lawn
{
	public/*internal*/ class MotionTrailFrame
	{
		public void SaveToFile(Sexy.Buffer b)
		{
			b.WriteFloat(this.mPosX);
			b.WriteFloat(this.mPosY);
			b.WriteFloat(this.mAnimTime);
		}

		public void LoadFromFile(Sexy.Buffer b)
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
