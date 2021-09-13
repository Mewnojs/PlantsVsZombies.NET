using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ struct ZombiePileObject
	{
		public ZombiePileObject(int aHeight, ZombiePileObjectType aType)
		{
			this.mHeight = aHeight;
			this.mType = aType;
			this.mOffsetX = (this.mOffsetY = 0f);
			this.mCounter = 0f;
			this.mY = -1;
			this.gemSpeedX = ((float)ZombiePileObject.rand.NextDouble() - 0.5f) * 5f;
			if (Math.Abs(this.gemSpeedX) < 0.7f)
			{
				this.gemSpeedX = 0.7f;
			}
			this.gemSpeedY = ((float)ZombiePileObject.rand.NextDouble() - 0.5f) * 0.5f;
			this.gemRotationSpeed = ((float)ZombiePileObject.rand.NextDouble() - 0.5f) * 0.1f;
			switch (aType)
			{
			case ZombiePileObjectType.OBJECT_GEM0:
				this.gemImage = AtlasResources.IMAGE_PILE_GEM0;
				break;
			case ZombiePileObjectType.OBJECT_GEM1:
				this.gemImage = AtlasResources.IMAGE_PILE_GEM1;
				break;
			case ZombiePileObjectType.OBJECT_GEM2:
				this.gemImage = AtlasResources.IMAGE_PILE_GEM2;
				break;
			case ZombiePileObjectType.OBJECT_GEM3:
				this.gemImage = AtlasResources.IMAGE_PILE_GEM3;
				break;
			case ZombiePileObjectType.OBJECT_GEM4:
				this.gemImage = AtlasResources.IMAGE_PILE_GEM4;
				break;
			case ZombiePileObjectType.OBJECT_GEM5:
				this.gemImage = AtlasResources.IMAGE_PILE_GEM5;
				break;
			case ZombiePileObjectType.OBJECT_GEM6:
				this.gemImage = AtlasResources.IMAGE_PILE_GEM6;
				break;
			default:
				this.gemImage = null;
				break;
			}
			if (aType == ZombiePileObjectType.OBJECT_YELLOW_CLOUD)
			{
				this.mCounter = (float)ZombiePileObject.rand.Next(Constants.BOARD_WIDTH);
			}
			if (aType == ZombiePileObjectType.OBJECT_ASTRONAUT)
			{
				this.mReanim = Reanimation.GetNewReanimation();
				this.mReanim.ReanimationInitializeType(0f, 0f, ReanimationType.REANIM_ASTRONAUT);
				this.mReanim.mLoopType = ReanimLoopType.REANIM_LOOP;
				this.mReanim.mOverlayMatrix.mMatrix.M11 = 0.85f;
				this.mReanim.mOverlayMatrix.mMatrix.M22 = 0.85f;
				this.mOffsetX = 400f;
				return;
			}
			this.mReanim = null;
			this.mOffsetX = (float)ZombiePileObject.rand.Next(Constants.BOARD_WIDTH);
		}

		public const float maxGemSpeedX = 5f;

		public int mHeight;

		public ZombiePileObjectType mType;

		public float mOffsetX;

		public float mOffsetY;

		public float mCounter;

		public int mY;

		public Reanimation mReanim;

		public float gemSpeedX;

		public float gemSpeedY;

		public float gemRotationSpeed;

		public Image gemImage;

		private static Random rand = new Random();
	}
}
