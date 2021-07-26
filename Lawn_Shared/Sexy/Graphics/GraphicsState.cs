using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	internal class GraphicsState
	{
		public Color mColor { get; protected set; }

		public int mScreenWidth
		{
			get
			{
				if (GraphicsState.mGraphicsDeviceManager.GraphicsDevice == null)
				{
					return GraphicsState.mGraphicsDeviceManager.PreferredBackBufferWidth;
				}
				return GraphicsState.mGraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth;
			}
		}

		public int mScreenHeight
		{
			get
			{
				if (GraphicsState.mGraphicsDeviceManager.GraphicsDevice == null)
				{
					return GraphicsState.mGraphicsDeviceManager.PreferredBackBufferHeight;
				}
				return GraphicsState.mGraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight;
			}
		}

		public GraphicsState(Game game)
		{
			GraphicsState.mGraphicsDeviceManager = new GraphicsDeviceManager(game);
			this.Reset();
		}

		public GraphicsState()
		{
			this.Reset();
		}

		private void Reset()
		{
			this.mTransX = 0;
			this.mTransY = 0;
			this.mFastStretch = false;
			this.mWriteColoredString = false;
			this.mLinearBlend = false;
			this.mScaleX = 1f;
			this.mScaleY = 1f;
			this.mScaleOrigX = 0f;
			this.mScaleOrigY = 0f;
			this.mFont = null;
			this.mColor = default(SexyColor);
			this.mColorizeImages = false;
			this.WorldRotation = 0f;
			this.mDrawMode = Graphics.DrawMode.DRAWMODE_NORMAL;
			this.mClipRect = new TRect(0, 0, this.mScreenWidth, this.mScreenHeight);
		}

		public static void Init()
		{
			Texture2D texture2D = new Texture2D(GraphicsState.mGraphicsDeviceManager.GraphicsDevice, 1, 1);
			texture2D.SetData<Color>(new Color[]
			{
				Color.White
			});
			GraphicsState.dummy = new Image(texture2D);
		}

		public void CopyStateFrom(GraphicsState theState)
		{
			this.mTransX = theState.mTransX;
			this.mTransY = theState.mTransY;
			this.mFastStretch = theState.mFastStretch;
			this.mWriteColoredString = theState.mWriteColoredString;
			this.mLinearBlend = theState.mLinearBlend;
			this.mScaleX = theState.mScaleX;
			this.mScaleY = theState.mScaleY;
			this.mScaleOrigX = theState.mScaleOrigX;
			this.mScaleOrigY = theState.mScaleOrigY;
			this.mClipRect = theState.mClipRect;
			this.mFont = theState.mFont;
			this.mColor = theState.mColor;
			this.mDrawMode = theState.mDrawMode;
			this.mColorizeImages = theState.mColorizeImages;
			this.WorldRotation = theState.WorldRotation;
		}

		public void SetWorldRotation(float theRotation)
		{
			this.WorldRotation = theRotation;
			this.NeedToSetWorldRotation = true;
		}

		public void ApplyWorldRotation()
		{
		}

		public static int GetClosestPowerOf2Above(int theNum)
		{
			int i;
			for (i = 1; i < theNum; i <<= 1)
			{
			}
			return i;
		}

		public int mTransX;

		public int mTransY;

		public float mScaleX;

		public float mScaleY;

		public float mScaleOrigX;

		public float mScaleOrigY;

		public bool mFastStretch;

		public bool mWriteColoredString;

		public bool mLinearBlend;

		public TRect mClipRect;

		protected Font mFont;

		public Graphics.DrawMode mDrawMode;

		public bool mColorizeImages;

		protected static Image dummy;

		public float WorldRotation;

		public bool NeedToSetWorldRotation;

		public static GraphicsDeviceManager mGraphicsDeviceManager;
	}
}
