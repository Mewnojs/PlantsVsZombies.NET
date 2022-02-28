using System;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class PAObjectInst
	{
		public PAObjectInst()
		{
			this.mName = null;
			this.mSpriteInst = null;
			this.mPredrawCallback = true;
			this.mPostdrawCallback = true;
			this.mImagePredrawCallback = true;
			this.mIsBlending = false;
		}

		public string mName;

		public PASpriteInst mSpriteInst;

		public PATransform mBlendSrcTransform = new PATransform();

		public SexyColor mBlendSrcColor = default(SexyColor);

		public bool mIsBlending;

		public SexyTransform2D mTransform = new SexyTransform2D(false);

		public SexyColor mColorMult = default(SexyColor);

		public bool mPredrawCallback;

		public bool mImagePredrawCallback;

		public bool mPostdrawCallback;
	}
}
