using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy.Drivers.Graphics;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class MemoryImage : Image
	{
		public MemoryImage()
		{
			this.mApp = GlobalMembers.gSexyAppBase;
		}

		public MemoryImage(MemoryImage rhs)
			: base(rhs)
		{
			this.mApp = rhs.mApp;
			this.mHasAlpha = rhs.mHasAlpha;
			this.mHasTrans = rhs.mHasTrans;
			this.mBitsChanged = rhs.mBitsChanged;
			this.mIsVolatile = rhs.mIsVolatile;
			this.mPurgeBits = rhs.mPurgeBits;
			this.mWantPal = rhs.mWantPal;
			this.mBitsChangedCount = rhs.mBitsChangedCount;
			if (rhs.mBits == null && rhs.mColorTable == null)
			{
				rhs.GetBits();
			}
			if (rhs.mBits == null)
			{
				this.mBits = null;
			}
			if (rhs.mColorTable == null)
			{
				this.mColorTable = null;
			}
			if (rhs.mColorIndices == null)
			{
				this.mColorIndices = null;
			}
			if (rhs.mNativeAlphaData != null)
			{
				if (rhs.mColorTable == null)
				{
				}
			}
			else
			{
				this.mNativeAlphaData = null;
			}
			if (rhs.mRLAlphaData != null)
			{
				this.mRLAlphaData = new byte[this.mWidth * this.mHeight];
			}
			else
			{
				this.mRLAlphaData = null;
			}
			if (rhs.mRLAdditiveData != null)
			{
				this.mRLAdditiveData = new byte[this.mWidth * this.mHeight];
			}
			else
			{
				this.mRLAdditiveData = null;
			}
			this.mApp.AddMemoryImage(this);
		}

		public override void Dispose()
		{
			base.Dispose();
			if (this.mRenderData != null)
			{
				GlobalMembers.gSexyAppBase.mGraphicsDriver.Remove3DData(this);
			}
			this.mRenderData = null;
		}

		private void Init()
		{
			this.mBits = null;
			this.mColorTable = null;
			this.mColorIndices = null;
			this.mNativeAlphaData = null;
			this.mRLAlphaData = null;
			this.mRLAdditiveData = null;
			this.mHasTrans = false;
			this.mHasAlpha = false;
			this.mBitsChanged = false;
			this.mForcedMode = false;
			this.mIsVolatile = false;
			this.mBitsChangedCount = 0;
			this.mPurgeBits = false;
			this.mWantPal = false;
			this.mDither16 = false;
			this.mApp.AddMemoryImage(this);
		}

		public virtual object GetNativeAlphaData(NativeDisplay theDisplay)
		{
			throw new NotImplementedException();
		}

		public virtual byte[] GetRLAlphaData()
		{
			throw new NotImplementedException();
		}

		public virtual byte[] GetRLAdditiveData(NativeDisplay theNative)
		{
			throw new NotImplementedException();
		}

		public virtual void PurgeBits()
		{
			this.mPurgeBits = true;
			if (this.mApp.Is3DAccelerated())
			{
				if (base.GetRenderData() == null)
				{
					return;
				}
			}
			else
			{
				if (this.mBits == null && this.mColorIndices == null)
				{
					return;
				}
				this.GetNativeAlphaData(GlobalMembers.gSexyAppBase.mGraphicsDriver.GetNativeDisplayInfo());
			}
			this.mBits = null;
			this.mBits = null;
			if (base.GetRenderData() != null)
			{
				this.mColorIndices = null;
				this.mColorIndices = null;
				this.mColorTable = null;
				this.mColorTable = null;
			}
		}

		public virtual void DeleteSWBuffers()
		{
			if (this.mNativeAlphaData == null && this.mRLAdditiveData == null && this.mRLAlphaData == null)
			{
				return;
			}
			if (this.mBits == null && this.mColorIndices == null)
			{
				this.GetBits();
			}
			this.mNativeAlphaData = null;
			this.mNativeAlphaData = null;
			this.mRLAdditiveData = null;
			this.mRLAdditiveData = null;
			this.mRLAlphaData = null;
			this.mRLAlphaData = null;
		}

		public virtual void Create(int theWidth, int theHeight)
		{
			this.mBits = null;
			this.mBits = null;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.mHasTrans = true;
			this.mHasAlpha = true;
		}

		public override MemoryImage AsMemoryImage()
		{
			return this;
		}

		public uint[] GetBits()
		{
			if (this.mBits == null)
			{
				int num = this.mWidth * this.mHeight;
				this.mBits = new uint[num];
				if (this.mColorTable != null)
				{
					for (int i = 0; i < num; i++)
					{
						this.mBits[i] = this.mColorTable[(int)this.mColorIndices[i]];
					}
					this.mColorIndices = null;
					this.mColorIndices = null;
					this.mColorTable = null;
					this.mColorTable = null;
					this.mNativeAlphaData = null;
					this.mNativeAlphaData = null;
				}
				else if (this.mNativeAlphaData == null)
				{
					if (base.GetRenderData() != null && (base.GetRenderData() as XNATextureData).mTextures[0].mTexture != null)
					{
						(base.GetRenderData() as XNATextureData).mTextures[0].mTexture.GetData<uint>(this.mBits);
					}
					else
					{
						MemoryImage memoryImage = ((this.mAtlasImage != null) ? this.mAtlasImage.AsMemoryImage() : null);
						if (memoryImage != null)
						{
							uint[] bits = memoryImage.GetBits();
							Array.Copy(bits, this.mAtlasStartY * memoryImage.mWidth + this.mAtlasStartX, this.mBits, 0, this.mBits.Length);
						}
					}
				}
			}
			return this.mBits;
		}

		public virtual void SetBits(uint[] theBits, int theWidth, int theHeight)
		{
			this.SetBits(theBits, theWidth, theHeight, true);
		}

		public virtual void SetBits(uint[] theBits, int theWidth, int theHeight, bool commitBits)
		{
			this.mColorIndices = null;
			this.mColorIndices = null;
			this.mColorTable = null;
			this.mColorTable = null;
			this.mBits = null;
			this.mBits = new uint[theWidth * theHeight];
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.mBits = theBits;
		}

		public virtual bool Palletize()
		{
			return true;
		}

		public static int GetWinding(int p0x, int p0y, int p1x, int p1y, int p2x, int p2y)
		{
			return (p1x - p0x) * (p2y - p0y) - (p1y - p0y) * (p2x - p0x);
		}

		public static void AddTri(ref List<MemoryImage.TriRep.Tri> outTris, Vector2[] inTri, int inWidth, int inHeight, int inGroup)
		{
		}

		public virtual void SetImageMode(bool hasTrans, bool hasAlpha)
		{
			this.mForcedMode = true;
			this.mHasTrans = hasTrans;
			this.mHasAlpha = hasAlpha;
		}

		public virtual void BitsChanged()
		{
		}

		internal void Clear()
		{
		}

		public uint[] mBits;

		public int mBitsChangedCount;

		public uint[] mColorTable;

		public byte[] mColorIndices;

		public bool mForcedMode;

		public bool mHasTrans;

		public bool mHasAlpha;

		public bool mIsVolatile;

		public bool mPurgeBits;

		public bool mWantPal;

		public bool mDither16;

		public uint[] mNativeAlphaData;

		public byte[] mRLAlphaData;

		public byte[] mRLAdditiveData;

		public bool mBitsChanged;

		public SexyAppBase mApp;

		public MemoryImage.TriRep mNormalTriRep = new MemoryImage.TriRep();

		public MemoryImage.TriRep mAdditiveTriRep = new MemoryImage.TriRep();

		public class TriRep
		{
			public MemoryImage.TriRep.Level GetMinLevel()
			{
				if (this.mLevels.Count != 0)
				{
					return this.mLevels[0];
				}
				return null;
			}

			public MemoryImage.TriRep.Level GetMaxLevel()
			{
				if (this.mLevels.Count != 0)
				{
					return this.mLevels[this.mLevels.Count - 1];
				}
				return null;
			}

			public MemoryImage.TriRep.Level GetLevelForScreenSpaceUsage(float inUsageFrac, int inAllowRotation)
			{
				if (this.mLevels.Count == 0)
				{
					return null;
				}
				for (int i = this.mLevels.Count - 1; i >= 0; i--)
				{
					MemoryImage.TriRep.Level result = this.mLevels[i];
					if (inUsageFrac > 0.001f)
					{
						return result;
					}
				}
				return null;
			}

			public List<MemoryImage.TriRep.Level> mLevels = new List<MemoryImage.TriRep.Level>();

			public class Tri
			{
				public Tri()
				{
				}

				public Tri(float inU0, float inV0, float inU1, float inV1, float inU2, float inV2)
				{
					this.p[0].u = inU0;
					this.p[0].v = inV0;
					this.p[1].u = inU1;
					this.p[1].v = inV1;
					this.p[2].u = inU2;
					this.p[2].v = inV2;
				}

				public MemoryImage.TriRep.Tri.Point[] p = new MemoryImage.TriRep.Tri.Point[3];

				public class Point
				{
					public float u;

					public float v;
				}
			}

			public class Level
			{
				public void GetRegionTris(ref List<MemoryImage.TriRep.Tri> outTris, MemoryImage inImage, Rect inSrcRect, int inAllowRotation)
				{
					if (this.mRegions.Count == 0)
					{
						return;
					}
					if (this.mRegionWidth != inImage.mNumCols || this.mRegionHeight != inImage.mNumRows)
					{
						return;
					}
					int num = inImage.mWidth / this.mRegionWidth;
					int num2 = inImage.mHeight / this.mRegionHeight;
					if (inSrcRect.mWidth != num || inSrcRect.mHeight != num2)
					{
						return;
					}
					int num3 = inSrcRect.mX / num;
					int num4 = inSrcRect.mY / num2;
					if (num3 < this.mRegionWidth && num4 < this.mRegionHeight)
					{
						MemoryImage.TriRep.Level.Region region = this.mRegions[num4 * this.mRegionWidth + num3];
						outTris = region.mTris;
					}
				}

				public MemoryImage.TriRep.Tri GetRegionTrisPtr(ref int outTriCount, MemoryImage inImage, Rect inSrcRect, int inAllowRotation)
				{
					if (this.mRegions.Count == 0)
					{
						return null;
					}
					if (this.mRegionWidth != inImage.mNumCols || this.mRegionHeight != inImage.mNumRows)
					{
						return null;
					}
					int num = inImage.mWidth / this.mRegionWidth;
					int num2 = inImage.mHeight / this.mRegionHeight;
					if (inSrcRect.mWidth != num || inSrcRect.mHeight != num2)
					{
						return null;
					}
					int num3 = inSrcRect.mX / num;
					int num4 = inSrcRect.mY / num2;
					if (num3 < this.mRegionWidth && num4 < this.mRegionHeight)
					{
						MemoryImage.TriRep.Level.Region region = this.mRegions[num4 * this.mRegionWidth + num3];
						outTriCount = region.mTris.Count;
						return region.mTris[0];
					}
					return null;
				}

				public int mDetailX;

				public int mDetailY;

				public int mRegionWidth;

				public int mRegionHeight;

				public List<MemoryImage.TriRep.Level.Region> mRegions = new List<MemoryImage.TriRep.Level.Region>();

				public class Region
				{
					public Rect mRect = default(Rect);

					public List<MemoryImage.TriRep.Tri> mTris = new List<MemoryImage.TriRep.Tri>();
				}
			}
		}
	}
}
