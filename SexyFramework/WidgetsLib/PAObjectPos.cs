using System;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class PAObjectPos
	{
		public string mName;

		public int mObjectNum;

		public bool mIsSprite;

		public bool mIsAdditive;

		public bool mHasSrcRect;

		public byte mResNum;

		public int mPreloadFrames;

		public int mAnimFrameNum;

		public float mTimeScale;

		public PATransform mTransform = new PATransform();

		public Rect mSrcRect = default(Rect);

		public int mColorInt;
	}
}
