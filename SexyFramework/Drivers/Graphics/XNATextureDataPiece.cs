using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy.Drivers.Graphics
{
	public class XNATextureDataPiece
	{
		public XNATextureDataPiece()
		{
			this.mTexture = null;
			this.mCubeTexture = null;
			this.mVolumeTexture = null;
			this.mTexFormat = 0;
			this.mWidth = 0;
			this.mHeight = 0;
		}

		public Texture2D mTexture;

		public TextureCube mCubeTexture;

		public Texture3D mVolumeTexture;

		public int mTexFormat;

		public int mWidth;

		public int mHeight;
	}
}
