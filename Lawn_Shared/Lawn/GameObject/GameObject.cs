using System;
using Sexy;

namespace Lawn
{
    public/*internal*/ abstract class GameObject
    {
        public virtual void PrepareForReuse()
        {
            Reset();
        }

        protected GameObject()
        {
            Reset();
        }

        protected virtual void Reset()
        {
            id = GameObject.nextId++;
            mX = 0;
            mY = 0;
            mWidth = 0;
            mHeight = 0;
            mVisible = true;
            mRow = -1;
            mRenderOrder = 400000;
            mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
            mBoard = mApp.mBoard;
            mPosScaled = true;
        }

        public virtual bool SaveToFile(Sexy.Buffer b)
        {
            b.WriteLong(id);
            b.WriteLong(mHeight);
            b.WriteBoolean(mPosScaled);
            b.WriteFloat(mPrevTransX);
            b.WriteFloat(mPrevTransY);
            b.WriteLong(mRenderOrder);
            b.WriteLong(mRow);
            b.WriteBoolean(mVisible);
            b.WriteLong(mWidth);
            b.WriteLong(mX);
            b.WriteLong(mY);
            return true;
        }

        public virtual bool LoadFromFile(Sexy.Buffer b)
        {
            id = b.ReadLong();
            mHeight = b.ReadLong();
            mPosScaled = b.ReadBoolean();
            mPrevTransX = b.ReadFloat();
            mPrevTransY = b.ReadFloat();
            mRenderOrder = b.ReadLong();
            mRow = b.ReadLong();
            mVisible = b.ReadBoolean();
            mWidth = b.ReadLong();
            mX = b.ReadLong();
            mY = b.ReadLong();
            return true;
        }

        public virtual void LoadingComplete()
        {
            mApp = GlobalStaticVars.gLawnApp;
            mBoard = mApp.mBoard;
        }

        protected static void SaveId(GameObject obj, Sexy.Buffer b)
        {
            if (obj != null)
            {
                b.WriteLong(obj.id);
                return;
            }
            b.WriteLong(-1);
        }

        protected static int LoadId(Sexy.Buffer b)
        {
            return b.ReadLong();
        }

        protected static GameObject GetObjectById(int id)
        {
            if (id == -1)
            {
                return null;
            }
            for (int i = 0; i < GlobalStaticVars.gLawnApp.mBoard.mPlants.Count; i++)
            {
                if (GlobalStaticVars.gLawnApp.mBoard.mPlants[i].id == id)
                {
                    return GlobalStaticVars.gLawnApp.mBoard.mPlants[i];
                }
            }
            for (int j = 0; j < GlobalStaticVars.gLawnApp.mBoard.mZombies.Count; j++)
            {
                if (GlobalStaticVars.gLawnApp.mBoard.mZombies[j].id == id)
                {
                    return GlobalStaticVars.gLawnApp.mBoard.mZombies[j];
                }
            }
            return null;
        }

        public bool BeginDraw(Graphics g)
        {
            if (!mVisible)
            {
                return false;
            }
            mPrevTransX = g.mTransX;
            mPrevTransY = g.mTransY;
            if (mPosScaled)
            {
                g.mTransX += (int)(mX * Constants.S);
                g.mTransY += (int)(mY * Constants.S);
            }
            else
            {
                g.mTransX += mX;
                g.mTransY += mY;
            }
            return true;
        }

        public void EndDraw(Graphics g)
        {
            g.mTransX = (int)mPrevTransX;
            g.mTransY = (int)mPrevTransY;
        }

        public void MakeParentGraphicsFrame(Graphics g)
        {
            g.mTransX -= (int)(mX * Constants.S);
            g.mTransY -= (int)(mY * Constants.S);
        }

        public LawnApp mApp;

        public Board mBoard;

        public int mX;

        public int mY;

        public int mWidth;

        public int mHeight;

        public bool mVisible;

        public int mRow;

        public int mRenderOrder;

        public float mPrevTransX;

        public float mPrevTransY;

        public bool mPosScaled;

        protected int id;

        private static int nextId;
    }
}
