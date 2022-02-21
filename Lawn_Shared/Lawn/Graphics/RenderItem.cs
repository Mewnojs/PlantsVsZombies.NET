using System;
using System.Collections.Generic;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class RenderItem : IComparable
    {
        public static void PreallocateMemory()
        {
            for (int i = 0; i < GameConstants.MAX_RENDER_ITEMS; i++)
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
            Reset();
            RenderItem.unusedObjects.Push(this);
        }

        private RenderItem()
        {
            Reset();
        }

        private void Reset()
        {
            long num = RenderItem.nextId;
            RenderItem.nextId = num + 1L;
            id = num;
            if (RenderItem.nextId == Int64.MaxValue)
            {
                RenderItem.nextId = 0L;
            }
            mRenderObjectType = 0;
            mZPos = 0;
            mGameObject = null;
            mPlant = null;
            mZombie = null;
            mCoin = null;
            mProjectile = null;
            mCursorPreview = null;
            mParticleSytem = null;
            mReanimation = null;
            mGridItem = null;
            mMower = null;
            mGameObject = null;
            mPlant = null;
            mZombie = null;
            mCoin = null;
            mProjectile = null;
            mCursorPreview = null;
            mParticleSytem = null;
            mReanimation = null;
            mGridItem = null;
            mMower = null;
            mBossPart = BossPart.BackLeg;
            mBoardGridY = 0;
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
            if (mZPos == renderItem.mZPos)
            {
                return id.CompareTo(renderItem.id);
            }
            return mZPos.CompareTo(renderItem.mZPos);
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

        private static Stack<RenderItem> unusedObjects = new Stack<RenderItem>(GameConstants.MAX_RENDER_ITEMS);
    }
}
