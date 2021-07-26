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
				return this.mVert.Color;
			}
			set
			{
				this.mVert.Color = value;
			}
		}

		public float x
		{
			get
			{
				return this.mVert.Position.X;
			}
			set
			{
				this.mVert.Position.X = value;
			}
		}

		public float y
		{
			get
			{
				return this.mVert.Position.Y;
			}
			set
			{
				this.mVert.Position.Y = value;
			}
		}

		public float u
		{
			get
			{
				return this.mVert.TextureCoordinate.X;
			}
			set
			{
				this.mVert.TextureCoordinate.X = value;
			}
		}

		public float v
		{
			get
			{
				return this.mVert.TextureCoordinate.Y;
			}
			set
			{
				this.mVert.TextureCoordinate.Y = value;
			}
		}

		public TriVertex(float theX, float theY)
		{
			this.mVert.Position = new Vector3(theX, theY, 0f);
			this.mVert.Color = Color.White;
			this.mVert.TextureCoordinate = Vector2.Zero;
		}

		public TriVertex(float theX, float theY, float theU, float theV)
		{
			this.mVert.Position = new Vector3(theX, theY, 0f);
			this.mVert.TextureCoordinate = new Vector2(theU, theV);
			this.mVert.Color = Color.White;
		}

		public TriVertex(float theX, float theY, float theU, float theV, Color theColor)
		{
			this.mVert.Position = new Vector3(theX, theY, 0f);
			this.mVert.TextureCoordinate = new Vector2(theU, theV);
			this.mVert.Color = theColor;
		}

		public VertexPositionColorTexture mVert;
	}
}
