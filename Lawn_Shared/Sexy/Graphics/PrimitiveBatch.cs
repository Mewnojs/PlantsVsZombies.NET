using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	public/*internal*/ class PrimitiveBatch : IDisposable
	{
		public PrimitiveBatch(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			device = graphicsDevice;
			basicEffect = new BasicEffect(graphicsDevice);
			basicEffect.VertexColorEnabled = true;
			basicEffect.LightingEnabled = false;
			basicEffect.FogEnabled = false;
		}

		public void SetupMatrices()
		{
			basicEffect.View = Matrix.CreateOrthographicOffCenter(0f, (float)device.PresentationParameters.BackBufferWidth, (float)device.PresentationParameters.BackBufferHeight, 0f, 0f, 1f);
			screenWidth = device.PresentationParameters.BackBufferWidth;
			screenHeight = device.PresentationParameters.BackBufferHeight;
		}

		public void SetupMatrices(int width, int height)
		{
			basicEffect.View = Matrix.CreateOrthographicOffCenter(0f, width, height, 0f, 0f, 1f);
			screenWidth = width;
			screenHeight = height;
		}

		public void Draw(Image img, TRect destination, TRect source, Color colour, bool extraOffset, bool sourceOffsetsUsed)
		{
			Matrix? matrix = default(Matrix?);
			Draw(img, destination, source, ref matrix, Vector2.Zero, colour, extraOffset, sourceOffsetsUsed, PrimitiveBatchEffects.None);
		}

		public void Draw(Image img, TRect destination, TRect source, Color colour, bool extraOffset, bool sourceOffsetsUsed, PrimitiveBatchEffects effects)
		{
			Matrix? matrix = default(Matrix?);
			Draw(img, destination, source, ref matrix, Vector2.Zero, colour, extraOffset, sourceOffsetsUsed, effects);
		}

		public void Draw(Image img, TRect destination, TRect source, ref Matrix transform, Vector2 center, Color colour, bool extraOffset, bool sourceOffsetsUsed)
		{
			Matrix? matrix = new Matrix?(transform);
			Draw(img, destination, source, ref matrix, center, colour, extraOffset, sourceOffsetsUsed, PrimitiveBatchEffects.None);
		}

		public void DrawRotatedScaled(Image img, TRect destination, TRect source, Vector2 center, float rotation, Vector2 scale, Color colour, bool extraOffset, bool sourceOffsetsUsed, PrimitiveBatchEffects effects)
		{
			Matrix? matrix = new Matrix?(Matrix.CreateTranslation((float)(-(float)OffsetX), (float)(-(float)OffsetY), 0f) * Matrix.CreateScale(scale.X, scale.Y, 1f) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation((float)destination.mX, (float)destination.mY, 0f));
			destination.mX = 0;
			destination.mY = 0;
			destination.mWidth = source.mWidth;
			destination.mHeight = source.mHeight;
			Draw(img, destination, source, ref matrix, center, colour, false, sourceOffsetsUsed, effects);
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
				Transform = value;
				mHasTransform = true;
			}
			else
			{
				mHasTransform = false;
			}
			Texture = img;
			vertex.Color = colour;
			if (extraOffset)
			{
				destination.mX -= OffsetX;
				destination.mY -= OffsetY;
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
			vertex.Position.X = (float)destination.mX;
			vertex.Position.Y = (float)destination.mY;
			vertex.Position.Z = z;
			vertex.TextureCoordinate.X = x;
			vertex.TextureCoordinate.Y = y;
			AddVertex(ref vertex);
			short num = (short)(positionInBuffer - 1);
			vertex.Position.X = (float)(destination.mX + destination.mWidth);
			vertex.Position.Y = (float)destination.mY;
			vertex.TextureCoordinate.X = x2;
			AddVertex(ref vertex);
			short num2 = (short)(positionInBuffer - 1);
			vertex.Position.X = (float)destination.mX;
			vertex.Position.Y = (float)(destination.mY + destination.mHeight);
			vertex.TextureCoordinate.X = x;
			vertex.TextureCoordinate.Y = y2;
			AddVertex(ref vertex);
			short num3 = (short)(positionInBuffer - 1);
			vertex.Position.X = (float)(destination.mX + destination.mWidth);
			vertex.Position.Y = (float)(destination.mY + destination.mHeight);
			vertex.TextureCoordinate.X = x2;
			AddVertex(ref vertex);
			short num4 = (short)(positionInBuffer - 1);
			if (positionInIndexBuffer + 6 >= indices.Length)
			{
				Flush();
			}
			indices[positionInIndexBuffer++] = num;
			indices[positionInIndexBuffer++] = num2;
			indices[positionInIndexBuffer++] = num3;
			indices[positionInIndexBuffer++] = num2;
			indices[positionInIndexBuffer++] = num4;
			indices[positionInIndexBuffer++] = num3;
			primitiveCount += 2;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !isDisposed)
			{
				if (basicEffect != null)
				{
					basicEffect.Dispose();
				}
				isDisposed = true;
			}
		}

		public void Restart()
		{
			Begin(primitiveType, OffsetX, OffsetY, new Matrix?(Transform), Texture, lastUsedSamplerState);
		}

		public void Begin(PrimitiveType primitiveType, int offsetX, int offsetY, Matrix? transform, Image texture, SamplerState st)
		{
			GlobalStaticVars.g.GraphicsDevice.RasterizerState = rasterizerState;
			if (hasBegun)
			{
				throw new InvalidOperationException("End must be called before Begin can be called again.");
			}
			if (screenWidth != device.PresentationParameters.BackBufferWidth || screenHeight != device.PresentationParameters.BackBufferHeight)
			{
				//this.SetupMatrices();
			}
			if (primitiveType == PrimitiveType.LineStrip || primitiveType == PrimitiveType.TriangleStrip)
			{
				throw new NotSupportedException("The specified primitiveType is not supported by PrimitiveBatch.");
			}
			hasBegun = true;
			lastUsedSamplerState = st;
			if (st != null)
			{
				device.SamplerStates[0] = st;
			}
			this.primitiveType = primitiveType;
			numVertsPerPrimitive = PrimitiveBatch.NumVertsPerPrimitive(primitiveType);
			Texture = texture;
			OffsetX = offsetX;
			OffsetY = offsetY;
			if (transform != null)
			{
				Transform = transform.Value;
			}
			else
			{
				Transform = Matrix.Identity;
			}
			basicEffect.CurrentTechnique.Passes[0].Apply();
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
					AddVertex(ref vertices[i, j].mVert);
					indices[positionInIndexBuffer++] = (short)(positionInBuffer - 1);
					primitiveCount++;
				}
			}
		}

		public void AddVertex(Vector2 vertex, Color color)
		{
			if (!hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before AddVertex can be called.");
			}
			bool flag = positionInBuffer % numVertsPerPrimitive == 0;
			if (flag && positionInBuffer + numVertsPerPrimitive >= VerticesLength)
			{
				Flush();
			}
			vertex.X += (float)OffsetX;
			vertex.Y += (float)OffsetY;
			if (mHasTransform)
			{
				Vector2.Transform(ref vertex, ref Transform, out vertex);
			}
			if (texture == null)
			{
				vertices[positionInBuffer].Position = new Vector3(vertex.X, vertex.Y, 0f);
				vertices[positionInBuffer].Color = color;
			}
			else
			{
				texturedVertices[positionInBuffer].Position = new Vector3(vertex.X, vertex.Y, 0f);
				texturedVertices[positionInBuffer].Color = color;
				texturedVertices[positionInBuffer].TextureCoordinate = Vector2.Zero;
			}
			positionInBuffer++;
		}

		public void AddVertex(ref VertexPositionColorTexture vertex)
		{
			if (!hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before AddVertex can be called.");
			}
			bool flag = positionInBuffer % numVertsPerPrimitive == 0;
			if (flag && positionInBuffer + numVertsPerPrimitive >= VerticesLength)
			{
				Flush();
			}
			vertex.Position.X = vertex.Position.X + (float)OffsetX;
			vertex.Position.Y = vertex.Position.Y + (float)OffsetY;
			if (mHasTransform)
			{
				Vector3.Transform(ref vertex.Position, ref Transform, out vertex.Position);
			}
			vertex.Position.X = vertex.Position.X - 0.5f;
			vertex.Position.Y = vertex.Position.Y - 0.5f;
			if (texture == null)
			{
				vertices[positionInBuffer].Position = vertex.Position;
				vertices[positionInBuffer].Color = vertex.Color;
			}
			else
			{
				texturedVertices[positionInBuffer] = vertex;
			}
			positionInBuffer++;
		}

		private int VerticesLength
		{
			get
			{
				if (texture != null)
				{
					return texturedVertices.Length;
				}
				return vertices.Length;
			}
		}

		public void End()
		{
			if (!hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before End can be called.");
			}
			Flush();
			hasBegun = false;
		}

		private void Flush()
		{
			if (!hasBegun)
			{
				throw new InvalidOperationException("Begin must be called before Flush can be called.");
			}
			if (positionInBuffer == 0)
			{
				return;
			}
			if (texture == null)
			{
				device.DrawUserIndexedPrimitives<VertexPositionColor>(primitiveType, vertices, 0, positionInBuffer, indices, 0, primitiveCount);
			}
			else
			{
				device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(primitiveType, texturedVertices, 0, positionInBuffer, indices, 0, primitiveCount);
			}
			positionInBuffer = 0;
			positionInIndexBuffer = 0;
			primitiveCount = 0;
		}

		public bool HasBegun
		{
			get
			{
				return hasBegun;
			}
		}

		public Image Texture
		{
			get
			{
				return texture;
			}
			set
			{
				if (texture == null && value == null)
				{
					return;
				}
				if (texture != null && value != null && texture.Texture == value.Texture)
				{
					return;
				}
				Flush();
				texture = value;
				if (texture != null)
				{
					basicEffect.TextureEnabled = true;
					basicEffect.Texture = texture.Texture;
				}
				else
				{
					basicEffect.TextureEnabled = false;
					basicEffect.Texture = null;
				}
				basicEffect.CurrentTechnique.Passes[0].Apply();
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
