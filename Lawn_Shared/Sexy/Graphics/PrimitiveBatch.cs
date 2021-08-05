using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	internal class PrimitiveBatch : IDisposable
	{
		public PrimitiveBatch(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			this.device = graphicsDevice;
			this.basicEffect = new BasicEffect(graphicsDevice);
			this.basicEffect.VertexColorEnabled = true;
			this.basicEffect.LightingEnabled = false;
			this.basicEffect.FogEnabled = false;
		}

		public void SetupMatrices()
		{
			this.basicEffect.View = Matrix.CreateOrthographicOffCenter(0f, (float)this.device.PresentationParameters.BackBufferWidth, (float)this.device.PresentationParameters.BackBufferHeight, 0f, 0f, 1f);
			this.screenWidth = this.device.PresentationParameters.BackBufferWidth;
			this.screenHeight = this.device.PresentationParameters.BackBufferHeight;
		}

		public void SetupMatrices(int width, int height)
		{
			this.basicEffect.View = Matrix.CreateOrthographicOffCenter(0f, width, height, 0f, 0f, 1f);
			this.screenWidth = width;
			this.screenHeight = height;
		}

		public void Draw(Image img, TRect destination, TRect source, Color colour, bool extraOffset, bool sourceOffsetsUsed)
		{
			Matrix? matrix = default(Matrix?);
			this.Draw(img, destination, source, ref matrix, Vector2.Zero, colour, extraOffset, sourceOffsetsUsed, PrimitiveBatchEffects.None);
		}

		public void Draw(Image img, TRect destination, TRect source, Color colour, bool extraOffset, bool sourceOffsetsUsed, PrimitiveBatchEffects effects)
		{
			Matrix? matrix = default(Matrix?);
			this.Draw(img, destination, source, ref matrix, Vector2.Zero, colour, extraOffset, sourceOffsetsUsed, effects);
		}

		public void Draw(Image img, TRect destination, TRect source, ref Matrix transform, Vector2 center, Color colour, bool extraOffset, bool sourceOffsetsUsed)
		{
			Matrix? matrix = new Matrix?(transform);
			this.Draw(img, destination, source, ref matrix, center, colour, extraOffset, sourceOffsetsUsed, PrimitiveBatchEffects.None);
		}

		public void DrawRotatedScaled(Image img, TRect destination, TRect source, Vector2 center, float rotation, Vector2 scale, Color colour, bool extraOffset, bool sourceOffsetsUsed, PrimitiveBatchEffects effects)
		{
			Matrix? matrix = new Matrix?(Matrix.CreateTranslation((float)(-(float)this.OffsetX), (float)(-(float)this.OffsetY), 0f) * Matrix.CreateScale(scale.X, scale.Y, 1f) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation((float)destination.mX, (float)destination.mY, 0f));
			destination.mX = 0;
			destination.mY = 0;
			destination.mWidth = source.mWidth;
			destination.mHeight = source.mHeight;
			this.Draw(img, destination, source, ref matrix, center, colour, false, sourceOffsetsUsed, effects);
		}

		public void Draw(Image img, TRect destination, TRect source, ref Matrix? transform, Vector2 center, Color colour, bool extraOffset, bool sourceOffsetsUsed, PrimitiveBatchEffects effects)
		{
			if (destination.IsEmpty)
			{
				return;
			}
			if (transform != null)
			{
				Matrix value = transform.Value;
				value.M41 += value.M11 * -center.X + value.M21 * -center.Y;
				value.M42 += value.M12 * -center.X + value.M22 * -center.Y;
				this.Transform = value;
				this.mHasTransform = true;
			}
			else
			{
				this.mHasTransform = false;
			}
			this.Texture = img;
			this.vertex.Color = colour;
			if (extraOffset)
			{
				destination.mX -= this.OffsetX;
				destination.mY -= this.OffsetY;
			}
			if (sourceOffsetsUsed)
			{
				source.mX += img.mS;
				source.mY += img.mT;
				source.mWidth -= img.GetCelWidth() - source.mWidth;
				source.mHeight -= img.GetCelHeight() - source.mHeight;
			}
			bool flag = effects == PrimitiveBatchEffects.MirrorHorizontally;
			bool flag2 = effects == PrimitiveBatchEffects.MirrorVertically;
			float y = (float)(source.mY + (flag2 ? source.mHeight : 0)) / (float)img.Texture.Height;
			float y2 = (float)(source.mY + (flag2 ? 0 : source.mHeight)) / (float)img.Texture.Height;
			float x = (float)(source.mX + (flag ? source.mWidth : 0)) / (float)img.Texture.Width;
			float x2 = (float)(source.mX + (flag ? 0 : source.mWidth)) / (float)img.Texture.Width;
			float z = 0f;
			this.vertex.Position.X = (float)destination.mX;
			this.vertex.Position.Y = (float)destination.mY;
			this.vertex.Position.Z = z;
			this.vertex.TextureCoordinate.X = x;
			this.vertex.TextureCoordinate.Y = y;
			this.AddVertex(ref this.vertex);
			short num = (short)(this.positionInBuffer - 1);
			this.vertex.Position.X = (float)(destination.mX + destination.mWidth);
			this.vertex.Position.Y = (float)destination.mY;
			this.vertex.TextureCoordinate.X = x2;
			this.AddVertex(ref this.vertex);
			short num2 = (short)(this.positionInBuffer - 1);
			this.vertex.Position.X = (float)destination.mX;
			this.vertex.Position.Y = (float)(destination.mY + destination.mHeight);
			this.vertex.TextureCoordinate.X = x;
			this.vertex.TextureCoordinate.Y = y2;
			this.AddVertex(ref this.vertex);
			short num3 = (short)(this.positionInBuffer - 1);
			this.vertex.Position.X = (float)(destination.mX + destination.mWidth);
			this.vertex.Position.Y = (float)(destination.mY + destination.mHeight);
			this.vertex.TextureCoordinate.X = x2;
			this.AddVertex(ref this.vertex);
			short num4 = (short)(this.positionInBuffer - 1);
			if (this.positionInIndexBuffer + 6 >= this.indices.Length)
			{
				this.Flush();
			}
			this.indices[this.positionInIndexBuffer++] = num;
			this.indices[this.positionInIndexBuffer++] = num2;
			this.indices[this.positionInIndexBuffer++] = num3;
			this.indices[this.positionInIndexBuffer++] = num2;
			this.indices[this.positionInIndexBuffer++] = num4;
			this.indices[this.positionInIndexBuffer++] = num3;
			this.primitiveCount += 2;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.isDisposed)
			{
				if (this.basicEffect != null)
				{
					this.basicEffect.Dispose();
				}
				this.isDisposed = true;
			}
		}

		public void Restart()
		{
			this.Begin(this.primitiveType, this.OffsetX, this.OffsetY, new Matrix?(this.Transform), this.Texture, this.lastUsedSamplerState);
		}

		public void Begin(PrimitiveType primitiveType, int offsetX, int offsetY, Matrix? transform, Image texture, SamplerState st)
		{
			GlobalStaticVars.g.GraphicsDevice.RasterizerState = this.rasterizerState;
			if (this.hasBegun)
			{
				throw new InvalidOperationException("End must be called before Begin can be called again.");
			}
			if (this.screenWidth != this.device.PresentationParameters.BackBufferWidth || this.screenHeight != this.device.PresentationParameters.BackBufferHeight)
			{
				//this.SetupMatrices();
			}
			if (primitiveType == PrimitiveType.LineStrip || primitiveType == PrimitiveType.TriangleStrip)
			{
				throw new NotSupportedException("The specified primitiveType is not supported by PrimitiveBatch.");
			}
			this.hasBegun = true;
			this.lastUsedSamplerState = st;
			if (st != null)
			{
				this.device.SamplerStates[0] = st;
			}
			this.primitiveType = primitiveType;
			this.numVertsPerPrimitive = PrimitiveBatch.NumVertsPerPrimitive(primitiveType);
			this.Texture = texture;
			this.OffsetX = offsetX;
			this.OffsetY = offsetY;
			if (transform != null)
			{
				this.Transform = transform.Value;
			}
			else
			{
				this.Transform = Matrix.Identity;
			}
			this.basicEffect.CurrentTechnique.Passes[0].Apply();
		}

		public void AddTriVertices(TriVertex[,] vertices, int triangleCount, Color? theColor)
		{
			for (int i = 0; i < triangleCount; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (theColor != null)
					{
						vertices[i, j].color = theColor.Value;
					}
					this.AddVertex(ref vertices[i, j].mVert);
					this.indices[this.positionInIndexBuffer++] = (short)(this.positionInBuffer - 1);
					this.primitiveCount++;
				}
			}
		}

		public void AddVertex(Vector2 vertex, Color color)
		{
			if (!this.hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before AddVertex can be called.");
			}
			bool flag = this.positionInBuffer % this.numVertsPerPrimitive == 0;
			if (flag && this.positionInBuffer + this.numVertsPerPrimitive >= this.VerticesLength)
			{
				this.Flush();
			}
			vertex.X += (float)this.OffsetX;
			vertex.Y += (float)this.OffsetY;
			if (this.mHasTransform)
			{
				Vector2.Transform(ref vertex, ref this.Transform, out vertex);
			}
			if (this.texture == null)
			{
				this.vertices[this.positionInBuffer].Position = new Vector3(vertex.X, vertex.Y, 0f);
				this.vertices[this.positionInBuffer].Color = color;
			}
			else
			{
				this.texturedVertices[this.positionInBuffer].Position = new Vector3(vertex.X, vertex.Y, 0f);
				this.texturedVertices[this.positionInBuffer].Color = color;
				this.texturedVertices[this.positionInBuffer].TextureCoordinate = Vector2.Zero;
			}
			this.positionInBuffer++;
		}

		public void AddVertex(ref VertexPositionColorTexture vertex)
		{
			if (!this.hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before AddVertex can be called.");
			}
			bool flag = this.positionInBuffer % this.numVertsPerPrimitive == 0;
			if (flag && this.positionInBuffer + this.numVertsPerPrimitive >= this.VerticesLength)
			{
				this.Flush();
			}
			vertex.Position.X = vertex.Position.X + (float)this.OffsetX;
			vertex.Position.Y = vertex.Position.Y + (float)this.OffsetY;
			if (this.mHasTransform)
			{
				Vector3.Transform(ref vertex.Position, ref this.Transform, out vertex.Position);
			}
			vertex.Position.X = vertex.Position.X - 0.5f;
			vertex.Position.Y = vertex.Position.Y - 0.5f;
			if (this.texture == null)
			{
				this.vertices[this.positionInBuffer].Position = vertex.Position;
				this.vertices[this.positionInBuffer].Color = vertex.Color;
			}
			else
			{
				this.texturedVertices[this.positionInBuffer] = vertex;
			}
			this.positionInBuffer++;
		}

		private int VerticesLength
		{
			get
			{
				if (this.texture != null)
				{
					return this.texturedVertices.Length;
				}
				return this.vertices.Length;
			}
		}

		public void End()
		{
			if (!this.hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before End can be called.");
			}
			this.Flush();
			this.hasBegun = false;
		}

		private void Flush()
		{
			if (!this.hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before Flush can be called.");
			}
			if (this.positionInBuffer == 0)
			{
				return;
			}
			if (this.texture == null)
			{
				this.device.DrawUserIndexedPrimitives<VertexPositionColor>(this.primitiveType, this.vertices, 0, this.positionInBuffer, this.indices, 0, this.primitiveCount);
			}
			else
			{
				this.device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(this.primitiveType, this.texturedVertices, 0, this.positionInBuffer, this.indices, 0, this.primitiveCount);
			}
			this.positionInBuffer = 0;
			this.positionInIndexBuffer = 0;
			this.primitiveCount = 0;
		}

		public bool HasBegun
		{
			get
			{
				return this.hasBegun;
			}
		}

		public Image Texture
		{
			get
			{
				return this.texture;
			}
			set
			{
				if (this.texture == null && value == null)
				{
					return;
				}
				if (this.texture != null && value != null && this.texture.Texture == value.Texture)
				{
					return;
				}
				this.Flush();
				this.texture = value;
				if (this.texture != null)
				{
					this.basicEffect.TextureEnabled = true;
					this.basicEffect.Texture = this.texture.Texture;
				}
				else
				{
					this.basicEffect.TextureEnabled = false;
					this.basicEffect.Texture = null;
				}
				this.basicEffect.CurrentTechnique.Passes[0].Apply();
			}
		}

		private static int NumVertsPerPrimitive(PrimitiveType primitive)
		{
			switch (primitive)
			{
			case PrimitiveType.TriangleList:
				return 3;
			case PrimitiveType.LineList:
				return 2;
			}
			throw new InvalidOperationException("primitive is not valid");
		}

		private const float farZPlane = 1f;

		private VertexPositionColor[] vertices = new VertexPositionColor[1024];

		private VertexPositionColorTexture[] texturedVertices = new VertexPositionColorTexture[16384];

		private short[] indices = new short[16384];

		private int positionInBuffer;

		private int positionInIndexBuffer;

		public int primitiveCount;

		private BasicEffect basicEffect;

		private GraphicsDevice device;

		private PrimitiveType primitiveType;

		private int numVertsPerPrimitive;

		private bool hasBegun;

		private bool isDisposed;

		private Image texture;

		public int OffsetX;

		public int OffsetY;

		public Matrix Transform;

		private int screenWidth;

		private int screenHeight;

		private bool mHasTransform = true;

		private SamplerState lastUsedSamplerState;

		private VertexPositionColorTexture vertex;

		private Matrix t;

		private RasterizerState rasterizerState = new RasterizerState
		{
			CullMode = CullMode.None
		};
	}
}
