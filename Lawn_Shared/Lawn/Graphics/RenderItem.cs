using System;
using System.Collections.Generic;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class RenderItem : IComparable
	{
		public static void PreallocateMemory()
		{
			for (int i = 0; i < 2048; i++)
			{
				new RenderItem().PrepareForReuse();
			}
		}

		public static RenderItem GetNewRenderItem()
		{
			if (RenderItem.unusedObjects.Count > 0)
			{
				return RenderItem.unusedObjects.Pop();
			}
			return new RenderItem();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			RenderItem.unusedObjects.Push(this);
		}

		private RenderItem()
		{
			this.Reset();
		}

		private void Reset()
		{
			long num = RenderItem.nextId;
			RenderItem.nextId = num + 1L;
			this.id = num;
			if (RenderItem.nextId == 9223372036854775807L)
			{
				RenderItem.nextId = 0L;
			}
			this.mRenderObjectType = (RenderObjectType)0;
			this.mZPos = 0;
			this.mGameObject = null;
			this.mPlant = null;
			this.mZombie = null;
			this.mCoin = null;
			this.mProjectile = null;
			this.mCursorPreview = null;
			this.mParticleSytem = null;
			this.mReanimation = null;
			this.mGridItem = null;
			this.mMower = null;
			this.mGameObject = null;
			this.mPlant = null;
			this.mZombie = null;
			this.mCoin = null;
			this.mProjectile = null;
			this.mCursorPreview = null;
			this.mParticleSytem = null;
			this.mReanimation = null;
			this.mGridItem = null;
			this.mMower = null;
			this.mBossPart = BossPart.BOSS_PART_BACK_LEG;
			this.mBoardGridY = 0;
		}

		public static int CompareByZ(RenderItem a, RenderItem b)
		{
			if (a == null && b == null)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (a.mZPos == b.mZPos)
			{
				return (int)(a.id - b.id);
			}
			return a.mZPos - b.mZPos;
		}

		int IComparable.CompareTo(object toCompare)
		{
			RenderItem renderItem = (RenderItem)toCompare;
			if (this.mZPos == renderItem.mZPos)
			{
				return this.id.CompareTo(renderItem.id);
			}
			return this.mZPos.CompareTo(renderItem.mZPos);
		}

		public RenderObjectType mRenderObjectType;

		public int mZPos;

		public GameObject mGameObject;

		public Plant mPlant;

		public Zombie mZombie;

		public Coin mCoin;

		public Projectile mProjectile;

		public CursorPreview mCursorPreview;

		public TodParticleSystem mParticleSytem;

		public Reanimation mReanimation;

		public GridItem mGridItem;

		public LawnMower mMower;

		public BossPart mBossPart;

		public int mBoardGridY;

		private static long nextId;

		public long id;

		private static Stack<RenderItem> unusedObjects = new Stack<RenderItem>(2048);
	}
}
