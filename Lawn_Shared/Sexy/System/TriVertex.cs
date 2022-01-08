using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	public struct TriVertex
	{
		public Color color
		{
			get
			{
				return mVert.Color;
			}
			set
			{
				mVert.Color = value;
			}
		}

		public float x
		{
			get
			{
				return mVert.Position.X;
			}
			set
			{
				mVert.Position.X = value;
			}
		}

		public float y
		{
			get
			{
				return mVert.Position.Y;
			}
			set
			{
				mVert.Position.Y = value;
			}
		}

		public float u
		{
			get
			{
				return mVert.TextureCoordinate.X;
			}
			set
			{
				mVert.TextureCoordinate.X = value;
			}
		}

		public float v
		{
			get
			{
				return mVert.TextureCoordinate.Y;
			}
			set
			{
				mVert.TextureCoordinate.Y = value;
			}
		}

		public TriVertex(float theX, float theY)
		{
			mVert.Position = new Vector3(theX, theY, 0f);
			mVert.Color = Color.White;
			mVert.TextureCoordinate = Vector2.Zero;
		}

		public TriVertex(float theX, float theY, float theU, float theV)
		{
			mVert.Position = new Vector3(theX, theY, 0f);
			mVert.TextureCoordinate = new Vector2(theU, theV);
			mVert.Color = Color.White;
		}

		public TriVertex(float theX, float theY, float theU, float theV, Color theColor)
		{
			mVert.Position = new Vector3(theX, theY, 0f);
			mVert.TextureCoordinate = new Vector2(theU, theV);
			mVert.Color = theColor;
		}

		public VertexPositionColorTexture mVert;
	}
}
