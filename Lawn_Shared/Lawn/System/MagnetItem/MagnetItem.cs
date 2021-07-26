using System;
using Sexy;

namespace Lawn
{
	internal class MagnetItem
	{
		public void Reset()
		{
			this.mPosX = (this.mPosY = (this.mDestOffsetX = (this.mDestOffsetY = 0f)));
			this.mItemType = MagnetItemType.MAGNET_ITEM_NONE;
		}

		public bool SaveToFile(Buffer b)
		{
			b.WriteFloat(this.mDestOffsetX);
			b.WriteFloat(this.mDestOffsetY);
			b.WriteLong((int)this.mItemType);
			b.WriteFloat(this.mPosX);
			b.WriteFloat(this.mPosY);
			return true;
		}

		public bool LoadFromFile(Buffer b)
		{
			this.mDestOffsetX = b.ReadFloat();
			this.mDestOffsetY = b.ReadFloat();
			this.mItemType = (MagnetItemType)b.ReadLong();
			this.mPosX = b.ReadFloat();
			this.mPosY = b.ReadFloat();
			return true;
		}

		public float mPosX;

		public float mPosY;

		public float mDestOffsetX;

		public float mDestOffsetY;

		public MagnetItemType mItemType;
	}
}
