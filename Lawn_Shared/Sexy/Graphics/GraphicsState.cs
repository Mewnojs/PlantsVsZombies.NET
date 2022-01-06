using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	public/*internal*/ class GraphicsState
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
			Reset();
		}

		public GraphicsState()
		{
			Reset();
		}

		private void Reset()
		{
			mTransX = 0;
			mTransY = 0;
			mFastStretch = false;
			mWriteColoredString = false;
			mLinearBlend = false;
			mScaleX = 1f;
			mScaleY = 1f;
			mScaleOrigX = 0f;
			mScaleOrigY = 0f;
			mFont = null;
			mColor = default(SexyColor);
			mColorizeImages = false;
			WorldRotation = 0f;
			mDrawMode = Graphics.DrawMode.DRAWMODE_NORMAL;
			mClipRect = new TRect(0, 0, mScreenWidth, mScreenHeight);
			mIsOffscreen = false;
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
			mTransX = theState.mTransX;
			mTransY = theState.mTransY;
			mFastStretch = theState.mFastStretch;
			mWriteColoredString = theState.mWriteColoredString;
			mLinearBlend = theState.mLinearBlend;
			mScaleX = theState.mScaleX;
			mScaleY = theState.mScaleY;
			mScaleOrigX = theState.mScaleOrigX;
			mScaleOrigY = theState.mScaleOrigY;
			mClipRect = theState.mClipRect;
			mFont = theState.mFont;
			mColor = theState.mColor;
			mDrawMode = theState.mDrawMode;
			mColorizeImages = theState.mColorizeImages;
			WorldRotation = theState.WorldRotation;
			mIsOffscreen = theState.mIsOffscreen;
		}

		public void SetWorldRotation(float theRotation)
		{
			WorldRotation = theRotation;
			NeedToSetWorldRotation = true;
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

		protected bool mIsOffscreen;
	}
}
