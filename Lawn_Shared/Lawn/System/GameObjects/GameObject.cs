using System;
using Sexy;

namespace Lawn
{
	internal abstract class GameObject
	{
		public virtual void PrepareForReuse()
		{
			this.Reset();
		}

		protected GameObject()
		{
			this.Reset();
		}

		protected virtual void Reset()
		{
			this.id = GameObject.nextId++;
			this.mX = 0;
			this.mY = 0;
			this.mWidth = 0;
			this.mHeight = 0;
			this.mVisible = true;
			this.mRow = -1;
			this.mRenderOrder = 400000;
			this.mApp = (LawnApp)GlobalStaticVars.gSexyAppBase;
			this.mBoard = this.mApp.mBoard;
			this.mPosScaled = true;
		}

		public virtual bool SaveToFile(Sexy.Buffer b)
		{
			b.WriteLong(this.id);
			b.WriteLong(this.mHeight);
			b.WriteBoolean(this.mPosScaled);
			b.WriteFloat(this.mPrevTransX);
			b.WriteFloat(this.mPrevTransY);
			b.WriteLong(this.mRenderOrder);
			b.WriteLong(this.mRow);
			b.WriteBoolean(this.mVisible);
			b.WriteLong(this.mWidth);
			b.WriteLong(this.mX);
			b.WriteLong(this.mY);
			return true;
		}

		public virtual bool LoadFromFile(Sexy.Buffer b)
		{
			this.id = b.ReadLong();
			this.mHeight = b.ReadLong();
			this.mPosScaled = b.ReadBoolean();
			this.mPrevTransX = b.ReadFloat();
			this.mPrevTransY = b.ReadFloat();
			this.mRenderOrder = b.ReadLong();
			this.mRow = b.ReadLong();
			this.mVisible = b.ReadBoolean();
			this.mWidth = b.ReadLong();
			this.mX = b.ReadLong();
			this.mY = b.ReadLong();
			return true;
		}

		public virtual void LoadingComplete()
		{
			this.mApp = GlobalStaticVars.gLawnApp;
			this.mBoard = this.mApp.mBoard;
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
			if (!this.mVisible)
			{
				return false;
			}
			this.mPrevTransX = (float)g.mTransX;
			this.mPrevTransY = (float)g.mTransY;
			if (this.mPosScaled)
			{
				g.mTransX += (int)((float)this.mX * Constants.S);
				g.mTransY += (int)((float)this.mY * Constants.S);
			}
			else
			{
				g.mTransX += this.mX;
				g.mTransY += this.mY;
			}
			return true;
		}

		public void EndDraw(Graphics g)
		{
			g.mTransX = (int)this.mPrevTransX;
			g.mTransY = (int)this.mPrevTransY;
		}

		public void MakeParentGraphicsFrame(Graphics g)
		{
			g.mTransX -= (int)((float)this.mX * Constants.S);
			g.mTransY -= (int)((float)this.mY * Constants.S);
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
